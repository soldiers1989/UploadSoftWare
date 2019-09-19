﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows.Media.Imaging;
using AIO.AnHui;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel.Wisdom;
using DYSeriesDataSet.KJC;
using DYSeriesDataSet.KjService;
using DyInterfaceHelper;

namespace AIO
{
    public class WorkThread : ChildThread, IDisposable
    {

        private bool _disposed = false;
        private ComPort _port = new ComPort();
        /// <summary>
        /// 仪器检测项目
        /// </summary>
        private string _CheckItemSecondDisplay;
        /// <summary>
        /// 检测项目
        /// </summary>
        private string _CheckItems;
        /// <summary>
        /// 检测标准
        /// </summary>
        private string _Standard;
        private string _SampleStandardAgainDisplay;
        private string _CheckItemStandard;
        /// <summary>
        /// 被检单位
        /// </summary>
        private string _DownCompany;
        private clsCompanyOpr _clsCompanyOpr = new clsCompanyOpr();
        private clsttStandardDecideOpr _clsttStandardDecideOpr = new clsttStandardDecideOpr();
        private  tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
        //private agreement _agreement = new agreement();
        private int _timeout = 0;
        private int _length = 0;

        protected override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            msg.result = false;
            ////获取硬件版本信息
            //JtjGetVersion(_port);
            switch (msg.what)
            {
                #region 打印机通讯测试
                case MsgCode.MSG_COMP_TEST:
                    msg.result = _port.NewOpen(msg.str1);
                    break;
                #endregion

                #region 通讯测试
                case MsgCode.MSG_COMM_TEST:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = ADTest(_port, 3);
                    }
                    break;
                #endregion

                #region 校准
                case MsgCode.MSG_COMM_CABT:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = CalibrationAD(_port, 3, msg.calibrationValue);
                    }
                    break;
                #endregion

                #region 新的读取AD值方法 2016年2月28日 wenj
                case MsgCode.MSG_READ_AD_CYCLE:
                    if (_port.Open(msg.str1))
                    {
                        if (ADTest(_port, 3))
                        {
                            while (WhileRead)
                            {
                                //byte[] data = NewReadADValue(_port);
                                byte[] data = ReadADValue(_port);
                                if (null != data)
                                {
                                    msg.result = true;
                                    msg.data = data;
                                    if (null != this.target)
                                        target.SendMessage(msg, null);
                                }
                                //0.5秒读取一次数据,清零维持50毫秒一次
                                DateUtils.WaitMs(Global.IsClear ? 50 : 500);
                            }
                        }
                        _port.Close();
                    }
                    else
                    {
                        msg.result = false;
                        msg.data = null;
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    break;
                #endregion

                #region 金标卡通讯测试
                case MsgCode.MSG_JBK_TEST:
                    try
                    {
                        if (_port.Open(msg.str1))
                        {
                            Global.JtjVersionInfo = null;
                            JtjGetVersion(_port);
                            ////胶体金摄像头新模块
                            //if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x30)
                            //{
                            //    msg.result = true;
                            //}
                            //else
                            //{
                            //胶体金扫描模块
                            if (jbkOUT(_port))
                            {
                                DateUtils.WaitMs(1500);
                                byte[] dataIn = jbkIN(_port, false);
                                if (dataIn != null)
                                {
                                    DateUtils.WaitMs(1500);
                                    msg.result = true;
                                }
                            }
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                #endregion

                #region 2016年10月13日 新增校准前判定是否有卡
                case MsgCode.MSG_JBK_CKC:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = jbkCheckCard(_port);
                    }
                    break;
                #endregion

                #region 金标卡校准
                case MsgCode.MSG_JBK_CBT:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = jbkCBT(_port);
                    }
                    break;
                #endregion

                #region 金标卡 出卡
                case MsgCode.MSG_JBK_OUT:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = jbkOUT(_port);
                    }
                    break;
                #endregion

                #region 金标卡 进卡
                case MsgCode.MSG_JBK_IN:
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = jbkIN(_port, false);
                        if (null != data)
                        {
                            msg.result = true;
                        }
                    }
                    break;
                #endregion

                #region 金标卡 进卡并测试
                case MsgCode.MSG_JBK_InAndTest:
                    FileUtils.Log("金标卡开始测试");
                    msg.datas = new List<byte[]>();
                    for (int i = 0; i < msg.ports.Count; i++)
                    {
                        if (msg.ports[i] != null && _port.Open(msg.ports[i]))
                        {
                            //同时进卡并测试
                            jbkIN(_port, Global.JtjVersion != 3);
                            _port.Close();
                        }
                    }

                    //旧扫描模块进卡大概需要八秒时间
                    int waitMs = 7500;
                    //新扫描模块需要十秒
                    if (Global.JtjVersionInfo != null)
                    {
                        if (Global.JtjVersionInfo[1] == 0x20)
                        {
                            waitMs = 9500;
                        }
                        else if (Global.JtjVersion == 3)
                        {
                            //新摄像头模块进卡仅需4秒
                            waitMs = 4000;
                        }
                    }

                    DateUtils.WaitMs(waitMs);

                    if (Global.JtjVersion == 3)
                    {
                        //胶体金摄像头新模块
                        break;
                    }

                    //胶体金扫描模块
                    for (int i = 0; i < msg.ports.Count; i++)
                    {
                        if (msg.ports[i] != null && _port.Open(msg.ports[i]))
                        {
                            FileUtils.Log(string.Format("端口：{0} 打开成功", msg.ports[i]));
                            #region
                            _timeout = 200;
                            _length = 512;
                            byte[] receiveData = new byte[_length];
                            byte[] sendBuffer = null;
                            byte[] dataList = null;
                            //数据请求
                            //if (Global.JtjVersion != 3)
                            //{
                            //    _agreement.cmd = 0x15;
                            //    _agreement.len[1] = 0x00;
                            //    sendBuffer = _agreement.getAgreement();
                            //}
                            //else
                            //{
                            sendBuffer = CMD.GetBuffer(0x15);
                            //}
                            //返回数据length个字节
                            receiveData = new byte[_length];
                            //下位机返回206个字节
                            bool isBreak = true;
                            System.Console.WriteLine(string.Format("------------------------------------------{0}开始读取数据------------------------------------------", msg.ports[i]));
                            FileUtils.Log(string.Format("------------------------------------------{0}开始读取数据------------------------------------------", msg.ports[i]));
                            while (isBreak)
                            {
                                DateUtils.WaitMs(100);
                                receiveData = new byte[_length];
                                _port.Clear();
                                _port.Write(sendBuffer, 0, sendBuffer.Length);
                                if (_port.Read(receiveData, 0, _length, _timeout) == 0) break;

#if DEBUG
                                System.Console.WriteLine("");
                                for (int x = 0; x < _length; x++)
                                {
                                    System.Console.Write(receiveData[x] + " ");
                                }
#endif

                                int len = 0;
                                for (int j = 0; j < receiveData.Length; j++)
                                {
                                    //头和命令编号相符时开始记录crc
                                    if (receiveData[j] == 0x7e && receiveData[j + 1] == 0x16)
                                    {
                                        //获取数据总长度
                                        len = (receiveData[j + 2] * 256) + receiveData[j + 3];
                                        //验证尾
                                        if (receiveData[j + len + 5] == 0x7e)
                                        {
                                            len = len + 6;
                                            dataList = new byte[len];
                                            Array.ConstrainedCopy(receiveData, j, dataList, 0, len);
                                            msg.datas.Add(dataList);
                                            isBreak = false;
                                            break;
                                        }
                                    }
                                }
                            }
#if DEBUG
                            System.Console.WriteLine("");
                            FileUtils.Log(string.Format("------------------------------------------{0}读取结束------------------------------------------", msg.ports[i]));
                            System.Console.WriteLine(string.Format("------------------------------------------{0}读取结束------------------------------------------", msg.ports[i]));
#endif
                            _port.Close();
                            msg.result = true;
                            #endregion
                        }
                        else
                        {
                            msg.datas.Add(null);
                        }
                    }

                    //for (int i = 0; i < msg.datas.Count; i++)
                    //{
                    //    if (msg.datas != null)
                    //    {
                    //        string val = string.Empty;
                    //        for (int j = 0; j < msg.datas[i].Length; j++)
                    //        {
                    //            val += msg.datas[i][j].ToString() + ",";
                    //        }
                    //        FileUtils.OprLog(6, "模块数据", val);
                    //    }
                    //}
                    break;
                #endregion

                #region 新摄像头胶体金模块
                case MsgCode.MSG_READ_CAM:
                    List<ComPort> ports = null;
                    List<byte[]> datas = null;
                    msg.result = true;
                    if (msg.obj1 != null)
                    {
                        ports = msg.obj1 as List<ComPort>;
                        datas = new List<byte[]>();

                        //其他操作指令
                        for (int i = 0; i < ports.Count; i++)
                        {
                            if (ports[i] == null || msg.obj3.ToString().Length == 0) continue;
                            byte cmd = 0x00;
                            switch (msg.obj3.ToString())
                            {
                                case "in"://进卡
                                    cmd = 0x01;
                                    break;

                                case "out"://出卡
                                    cmd = 0x02;
                                    break;

                                case "qr"://二维码位置
                                    cmd = 0x04;
                                    break;

                                case "test"://检测时，判定是否有卡
                                    cmd = 0x13;
                                    break;

                                default:
                                    break;
                            }
                            if (msg.obj3.ToString().Equals("test"))
                            {
                                //检测是否有卡
                                //暂时先不验证是否有卡，需要验证时取消注释即可
                                //if (msg.result) msg.result = jbkCheckCard(ports[i]);
                                continue;
                            }
                            InOrOutCard(ports[i], cmd);
                        }

                        if (msg.result && msg.obj3.ToString().Equals("test"))
                        {
                            //测试的同时进卡
                            for (int i = 0; i < ports.Count; i++)
                            {
                                if (ports[i] == null) continue;
                                InOrOutCard(ports[i], 0x01);
                            }
                        }

                        ////测试时采集的次数
                        //int readCount = 1;
                        //List<List<byte[]>> list = null;
                        //if (msg.obj3.ToString().Equals("test") && msg.result)
                        //{
                        //    readCount = 10;
                        //    list = new List<List<byte[]>>();
                        //}

                        //for (int i = 0; i < readCount; i++)
                        //{
                        //    datas = new List<byte[]>();
                        //采集数据
                        for (int j = 0; j < ports.Count; j++)
                        {
                            datas.Add(null);
                            if (ports[j] == null) continue;

                            byte[] data = ReadImg(ports[j]);
                            if (data != null)
                            {
                                datas[j] = data;
                            }
                        }
                        //    if (readCount > 1 && datas != null) list.Add(datas);
                        //}

                        msg.obj2 = datas;
                    }
                    break;
                #endregion

                #region 新摄像头模组胶体金3.0
                //case MsgCode.MSG_READ_SXT_CYCLE:
                //    List<ComPort> pts = null;
                //    List<byte[]> dts = null;
                //    pts = msg.obj1 as List<ComPort>;
                //    bool[] isOpen = new bool[2];
                //    for (int i = 0; i < isOpen.Length; i++)
                //    {
                //        isOpen[i] = pts[i] != null;
                //    }

                //    while (WhileRead)
                //    {
                //        dts = new List<byte[]>();
                //        for (int i = 0; i < isOpen.Length; i++)
                //        {
                //            dts.Add(null);
                //            if (isOpen[i])
                //            {
                //                byte[] bts = ReadImg(pts[i]);
                //                if (bts != null)
                //                {
                //                    dts[i] = bts;
                //                }
                //            }
                //        }
                //        msg.obj2 = dts;
                //        if (null != this.target)
                //            target.SendMessage(msg, null);
                //    }

                //    break;
                #endregion

                #region 新摄像头模组胶体金3.0固件更新
                case MsgCode.MSG_SXT_UPDATE:
                    if (_port.Open(msg.str1) && msg.obj1 != null)
                    {
                        byte[] fileDatas = msg.obj1 as byte[];
                        int LEN = 4096;
                        //计算下载次数，下载的内容最大长度为LEN
                        int downLoadCount = fileDatas.Length / LEN;
                        //余数
                        int remainder = fileDatas.Length % LEN;
                        if (remainder > 0) downLoadCount++;

                        int index = 0;
                        bool successful = true;
                        Console.WriteLine(string.Format("固件总长度:{0} ,固件下载次数:{1}", fileDatas.Length, downLoadCount));
                        for (int i = 1; i <= downLoadCount; i++)
                        {
                            //DATA
                            List<byte> dataList = new List<byte>();
                            //Total：总共下载的次数
                            dataList.Add((byte)downLoadCount);
                            //CNT：当前的次数，从1开始
                            dataList.Add((byte)i);
                            //DAT：下载的内容。最大长度为LEN
                            byte[] dat = new byte[LEN];
                            //确定最后一次下载长度
                            if (i == downLoadCount && remainder > 0)
                            {
                                dat = new byte[remainder];
                            }
                            Array.Copy(fileDatas, index, dat, 0, dat.Length);
                            dataList.AddRange(dat);
                            //数据包长度
                            byte[] data = new byte[dataList.Count];
                            dataList.CopyTo(data);
                            byte[] buffer = CMD.GetBuffer(0x22, data);
                            index += LEN;
                            if (!DownLoadFirmware(buffer))
                            {
                                successful = false;
                                Console.WriteLine(string.Format("固件下载:{0} 失败", i));
                                break;
                            }

                            Console.WriteLine(string.Format("固件下载:{0} 成功,数据长度:{1}\r\n", i, buffer.Length));
                        }

                        //固件下载成功后，下发更新命令
                        if (successful && Update(CMD.CRC8(fileDatas), (uint)fileDatas.Length))
                        {
                            msg.result = true;
                        }
                    }
                    break;
                #endregion

                #region 获取版本号
                case MsgCode.MSG_GET_VERSION:
                    if (_port.Open(msg.str1))
                    {
                        JtjGetVersion(_port);
                    }
                    break;
                #endregion

                #region 打印
                case MsgCode.MSG_PRINT:
                    if (_port.NewOpen(msg.str1))
                    {
                        if (msg.data != null)
                        {
                            _port.Write(msg.data, msg.arg1, msg.arg2);
                        }
                        //二维码打印
                        else if (msg.datas != null && msg.datas.Count > 0)
                        {
                            for (int i = 0; i < msg.datas.Count; i++)
                            {
                                _port.Write(msg.datas[i], 0, msg.datas[i].Length);
                                DateUtils.WaitMs((i % 2 == 0) ? 6000 : 3000);
                            }
                        }
                        msg.result = true;
                    }
                    break;
                #endregion

                #region 2016年10月8日 wenj 通讯测试失败时仅保留URL用户名和密码；
                case MsgCode.MSG_CHECK_CONNECTION:
                    try
                    {
                        if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals(""))
                        {
                            string rtnXml = checkUserConnection(msg.str1, msg.str2, msg.str3);
                            DataSet dst = new DataSet();
                            if (rtnXml.Length > 0)
                            {
                                using (StringReader sr = new StringReader(rtnXml))
                                {
                                    dst.ReadXml(sr);
                                }
                            }
                            DataTable dtbl = dst.Tables["Result"];
                            string result = Global.GetResultByCode(dtbl.Rows[0]["ResultCode"].ToString());
                            if (result.Equals("1"))
                            {
                                result = dtbl.Rows[0]["ResultInfo"].ToString();
                                string[] user = result.Split(',');
                                if (user != null)
                                {
                                    Global.pointNum = user[0];
                                    Global.ponitName = user[1];
                                    Global.pointType = user[2];
                                    Global.orgNum = user[3];
                                    Global.orgName = user[4];
                                    Global.userUUID = user[5];
                                    msg.result = true;
                                }
                            }
                            else
                            {
                                msg.errMsg = dtbl.Rows[0]["ResultDesc"].ToString();
                                FileUtils.OprLog(0, "checkUser-failure", msg.errMsg);
                                Global.pointNum = string.Empty; Global.ponitName = string.Empty;
                                Global.pointType = string.Empty; Global.orgNum = string.Empty; Global.orgName = string.Empty;
                                Global.userUUID = string.Empty;
                            }
                        }
                        else if (Global.InterfaceType.Equals("KJC"))//快检车接口
                        {
                            msg.responseInfo = InterfaceHelper.CheckUserCommunication(msg.str1, msg.str2, msg.str3);
                            ResultMsg msgResult = Json.JsonToEntity<ResultMsg>(msg.responseInfo);
                            if (msgResult.resultCode.Equals("success1"))
                            {
                                CheckUserConnect userInfo = Json.JsonToEntity<CheckUserConnect>(msgResult.result.ToString());
                                Global.pointNum = userInfo.pointNum;
                                Global.pointId = userInfo.pointId;
                                Global.pointName = userInfo.pointName;
                                Global.pointType = userInfo.pointType;
                                Global.orgNum = userInfo.orgNum;
                                Global.orgName = userInfo.orgName;
                                Global.nickName = userInfo.nickName;
                                Global.userId = userInfo.userId.ToString();
                                msg.result = true;
                            }
                            else
                            {
                                msg.errMsg = msgResult.resultDescripe;
                                Global.pointNum = string.Empty;
                                Global.pointId = string.Empty;
                                Global.pointName = string.Empty;
                                Global.pointType = string.Empty;
                                Global.orgNum = string.Empty;
                                Global.orgName = string.Empty;
                                Global.nickName = string.Empty;
                                Global.userId = string.Empty;
                            }
                        }
                        else if (Global.InterfaceType.Equals("KJ"))//快检服务
                        {
                            msg.result = KjLogin(msg.str1, msg.str2, out msg.errMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "checkUser-error", ex.ToString());
                        msg.errMsg = ex.Message;
                    }
                    break;
                #endregion

                #region 快检服务中心-仪器注册
                case MsgCode.MSG_REGISTERDEVICE:
                    try
                    {
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            Global.KjServer.registerDeviceEntity = DyInterfaceHelper.KjService.RegisterDevice(Global.KjServer.userLoginEntity.token, msg.str1, msg.str2, out msg.errMsg);
                            if (Global.KjServer.registerDeviceEntity != null)
                            {
                                Global.DeviceID = Global.KjServer.registerDeviceEntity.serial_number;
                                CFGUtils.SaveConfig("Mac", Global.Mac);
                                CFGUtils.SaveConfig("DeviceId", Global.DeviceID);
                                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);
                                msg.result = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-获取检测任务数量
                case MsgCode.MSG_RECEIVETASKS_COUNT:

                    break;
                #endregion

                #region 快检服务中心-检测任务获取
                case MsgCode.MSG_RECEIVETASKS:
                    try
                    {
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            Global.KjServer.receiveTasksEntity = DyInterfaceHelper.KjService.ReceiveTasks(Global.KjServer.userLoginEntity.token, "", out msg.errMsg);
                            if (Global.KjServer.receiveTasksEntity != null)
                            {
                                msg.result = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-标记检测任务已读
                case MsgCode.MSG_VIEWTASK:
                    try
                    {
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            msg.result = DyInterfaceHelper.KjService.ViewTask(Global.KjServer.userLoginEntity.token, msg.str1, out msg.errMsg);
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-获取仪器检测任务(APP抽样单)数量
                case MsgCode.MSG_DOWNLOADSAMPLING_COUNT:

                    break;
                #endregion

                #region 快检服务中心-仪器检测任务(APP抽样单)获取
                case MsgCode.MSG_DOWNLOADSAMPLING:
                    try
                    {
                        Global.KjServer.samplingEntity = new DyInterfaceHelper.KjService.DownloadSamplingEntity.Obj();
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);

                        //获取本地数据
                        DataTable dtbl = KjTasksOpr.GetAsDataTable(out msg.errMsg);
                        if (dtbl != null && dtbl.Rows.Count > 0)
                        {
                            Global.KjServer.samplingEntity.result = (List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>)IListDataSet.DataTableToIList<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>(dtbl, 1);
                        }

                        if (Global.KjServer.userLoginEntity != null)
                        {
                            //获取平台数据
                            DyInterfaceHelper.KjService.DownloadSamplingEntity.Obj obj = DyInterfaceHelper.KjService.DownloadSampling(Global.KjServer.userLoginEntity.token, null, Global.DeviceID, null, null, out msg.errMsg);
                            if (obj != null && obj.result.Count > 0)
                            {
                                for (int i = 0; i < obj.result.Count; i++)
                                {
                                    dtbl = KjTasksOpr.GetAsDataTable(out msg.errMsg, string.Format("id='{0}'", obj.result[i].id));
                                    //将新的任务写入数据库
                                    if (dtbl == null || dtbl.Rows.Count == 0)
                                    {
                                        //需要判断当前项目可以支持哪些模块检测
                                        obj.result[i].resultType = Global.KjServer.GetResultType(obj.result[i].item_name, 0);
                                        KjTasksOpr.Insert(obj.result[i], out msg.errMsg);
                                    }
                                }

                                //重新获取本地数据
                                dtbl = KjTasksOpr.GetAsDataTable(out msg.errMsg);
                                if (dtbl != null && dtbl.Rows.Count > 0)
                                {
                                    Global.KjServer.samplingEntity.result = (List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>)IListDataSet.DataTableToIList<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>(dtbl, 1);
                                }
                                else
                                {
                                    Global.KjServer.samplingEntity.result = obj.result;
                                    msg.errMsg = "本地数据获取失败";
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-更新仪器检测任务(APP抽样单)接收状态
                case MsgCode.MSG_UPDATESTATUS:
                    try
                    {
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            string[] ids = msg.str1.Split(',');
                            for (int i = 0; i < ids.Length; i++)
                            {
                                if (DyInterfaceHelper.KjService.UpdateStatus(Global.KjServer.userLoginEntity.token, ids[i], msg.str2, msg.str3, out msg.errMsg))
                                {
                                    //平台操作成功后
                                    DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem model = null;
                                    for (int j = 0; j < Global.KjServer.samplingEntity.result.Count; j++)
                                    {
                                        if (ids[i].Equals(Global.KjServer.samplingEntity.result[j].id))
                                        {
                                            Global.KjServer.samplingEntity.result[j].isReceive = true;
                                            if (Global.KjServer.samplingEntity.result[j].resultType == null || Global.KjServer.samplingEntity.result[j].resultType.Length == 0)
                                            {
                                                Global.KjServer.samplingEntity.result[j].resultType = Global.KjServer.GetResultType(Global.KjServer.samplingEntity.result[i].item_name, 0);
                                            }
                                            model = Global.KjServer.samplingEntity.result[j];
                                            break;
                                        }
                                    }
                                    DataTable dtbl = null;
                                    //接收，更新状态
                                    if (msg.str3.Equals("1"))
                                    {
                                        KjTasksOpr.Update(model, out msg.errMsg);
                                    }
                                    //拒收，删除数据
                                    else
                                    {
                                        KjTasksOpr.Delete(string.Format("id='{0}'", model.id), out msg.errMsg);
                                        if (msg.errMsg.Length == 0)
                                        {
                                            //重新获取本地数据
                                            dtbl = KjTasksOpr.GetAsDataTable(out msg.errMsg);
                                            if (dtbl != null && dtbl.Rows.Count > 0)
                                            {
                                                Global.KjServer.samplingEntity.result = (List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>)IListDataSet.DataTableToIList<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>(dtbl, 1);
                                            }
                                        }
                                    }

                                    //重新获取本地数据
                                    dtbl = KjTasksOpr.GetAsDataTable(out msg.errMsg);
                                    if (dtbl != null && dtbl.Rows.Count > 0)
                                    {
                                        Global.KjServer.samplingEntity.result = (List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>)IListDataSet.DataTableToIList<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>(dtbl, 1);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-公告
                case MsgCode.MSG_DOWNLOADTASKMSG:
                    try
                    {
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            Global.KjServer.taskMsgEntitys = DyInterfaceHelper.KjService.DownloadTaskMsg(Global.KjServer.userLoginEntity.token, msg.str1, msg.str2, out msg.errMsg);
                            if (Global.KjServer.taskMsgEntitys != null)
                            {
                                for (int i = 0; i < Global.KjServer.taskMsgEntitys.Count; i++)
                                {
                                    Global.KjServer.taskMsgEntitys[i].content = Global.KjServer.taskMsgEntitys[i].content.Split('<')[0];
                                }
                                msg.result = true;
                                return;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion

                #region 快检服务中心-下载基础数据
                case MsgCode.MSG_KJ_DOWNLOAD_BASICDATA:
                    try
                    {
                        DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                        KjLogin(Global.KjServer.Kjuser_name, Global.KjServer.Kjpassword, out msg.errMsg);
                        if (Global.KjServer.userLoginEntity != null)
                        {
                            switch (msg.str1)
                            {
                                //下载监管对象
                                case "regulatory":
                                    Global.KjServer.companys = DyInterfaceHelper.KjService.GetRegulatory(Global.KjServer.userLoginEntity.token);
                                    break;

                                //下载经营户
                                case "business":
                                    Global.KjServer.business = DyInterfaceHelper.KjService.GetBusiness(Global.KjServer.userLoginEntity.token);
                                    break;

                                default:
                                    break;
                            }
                            msg.result = true;
                        }
                    }
                    catch (Exception)
                    {
                        msg.errMsg = Global.KjServer.response.msg;
                    }
                    break;
                #endregion
                #region 快检服务样品种类下载
                case MsgCode.MSG_SAMPLETYPE:
                    msg.result = false;
                    string err = "";
                    int icount = 0;
                    string saddr= InterfaceHelper.GetServiceURL(msg.str1,8);//地址
                    string  lasttime = "";
                  
                    DataTable dt = bll.GetRequestTime("RequestName='foodTypes'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='foodTypes'", "", 1, out err);
                    }
                    else
                    {
                        bll.InsertResquestTime("'foodTypes','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }
                    StringBuilder sb = new StringBuilder();
                   
                    sb.Append(saddr);
                    sb.AppendFormat("?userToken={0}", Global.KjServer.userLoginEntity.token);
                    sb.AppendFormat("&type={0}", "food");
                    sb.AppendFormat("&serialNumber={0}", Global.KjServer.registerDeviceEntity.serial_number);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    //FileUtils.KLog(sb.ToString(), "发送", 8);
                    string stype= InterfaceHelper.HttpsPost(sb.ToString());
                    //FileUtils.KLog(stype, "接收", 8);
                    if (stype.Length > 0)
                    {
                        ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                        sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
                        if (obj.food.Count > 0)
                        {
                            foodtype ft = new foodtype();
                            for (int i = 0; i < obj.food.Count;i++ )
                            {
                                int rt = 0;
                                sb.Length = 0;
                                sb.AppendFormat("fid='{0}' ", obj.food[i].id);
                                dt = bll.Getfoodtype(sb.ToString(), "", out err);
                                ft = obj.food[i];
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    rt = bll.Updatefoodtype(ft);
                                    if (rt == 1)
                                    {
                                        icount = icount + 1;
                                    }
                                }
                                else
                                {
                                    rt = bll.Insertfoodtype(ft);
                                    if (rt == 1)
                                    {
                                        icount = icount + 1;
                                    }
                                }
                            }   
                        }
                    }
                    msg.str6 = icount.ToString();
                    msg.result = true ;
                    break;

                #endregion

                #region 获取电池状态
                case MsgCode.MSG_GET_BATTERY:
                    if (_port.OpenBattery(msg.str1))
                    {
                        byte[] data;
                        msg.batteryData = new List<byte[]>();
                        //1、获取电池充电放电状态
                        data = getBatteryState(_port);
                        if (data != null)
                        {
                            msg.batteryData.Add(data);
                            //2、0x00则表示没有电池，就不进行电量获取
                            if (data[4] != 0x00)
                            {
                                //3、获取电池电量
                                data = getBattery(_port);
                                if (data != null)
                                {
                                    msg.result = true;
                                    msg.batteryData.Add(data);
                                }
                            }
                            else
                            {
                                msg.batteryData.Add(null);
                            }
                        }
                    }
                    else
                    {
                        msg.batteryData = null;
                    }
                    break;
                #endregion

                #region 数据上传
                case MsgCode.MSG_UPLOAD:
                    Global.UploadSCount = Global.UploadFCount = 0;
                    if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Length == 0)
                    {
                        msg.outError = UploadResult((CheckPointInfo)msg.obj1, (msg.table));
                    }
                    else if (Global.InterfaceType.Equals("AH"))
                    {
                        msg.outError = UploadResultAH(msg.selectedRecords);
                    }
                    else if (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("KJ"))
                    {
                        msg.outError = UploadResult(msg.selectedRecords);
                    }

                    msg.result = msg.outError.Length == 0 ? true : false;
                    break;
                #endregion

                #region 全部数据下载
                case MsgCode.MSG_CHECK_SYNC:
                    try
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "all")))
                            msg.result = true;
                        msg.SampleStandardName = _SampleStandardAgainDisplay;
                        msg.CheckItemsTempList = _CheckItemSecondDisplay;
                        msg.CheckItems = _CheckItems;
                        msg.Standard = _Standard;
                        msg.CheckItemsAdapter = _CheckItemStandard;
                        msg.DownLoadCompany = _DownCompany;
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "downLoadAllData-error", ex.ToString());
                        msg.errMsg = ex.Message;
                        msg.result = false;
                    }
                    break;
                #endregion

                #region 被检单位下载
                case MsgCode.MSG_DownCompany:
                    try
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "Company")))
                            msg.result = true;
                        msg.DownLoadCompany = _DownCompany;
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "downLoadCompany-error", ex.ToString());
                        msg.errMsg = ex.Message;
                        msg.result = false;
                    }
                    break;
                #endregion

                #region 检测项目下载
                case MsgCode.MSG_DownCheckItems:
                    //string url2 = msg.str1;
                    //string username2 = msg.str2;
                    //string pwd2 = msg.str3;
                    //string checkNumber2 = msg.args.Dequeue();
                    //string checkName2 = msg.args.Dequeue();
                    //string checkType2 = msg.args.Dequeue();
                    //string checkOrg2 = msg.args.Dequeue();
                    //if ("true".Equals(DownItems(url2, checkNumber2, checkName2, checkType2, checkOrg2, username2, pwd2)))
                    //    msg.result = true;
                    //msg.CheckItemsTempList = _CheckItemSecondDisplay;
                    msg.result = false;
                    lasttime = "";
                    dt = bll.GetRequestTime("RequestName='CheckItem'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='CheckItem'", "", 1, out err);
                    }
                    else
                    {
                        bll.InsertResquestTime("'CheckItem','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }

                    string url2 = msg.str1;
                   
                    string ItemAddr= InterfaceHelper.GetServiceURL(url2, 5);//地址
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append(ItemAddr);
                    sb1.AppendFormat("?userToken={0}", Global.KjServer.userLoginEntity.token);
                    sb1.AppendFormat("&type={0}", "item");
                    sb1.AppendFormat("&serialName={0}", "");
                    sb1.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    //FileUtils.KLog(sb.ToString(), "发送", 7);
                    string itemlist = InterfaceHelper.HttpsPost(sb1.ToString());
                    //FileUtils.KLog(itemlist, "接收", 7);
                    int items = 0;
                    if (itemlist.Length > 0)
                    {
                        ResultData Jitem = JsonHelper.JsonToEntity<ResultData>(itemlist);
                        detectitem obj = JsonHelper.JsonToEntity<detectitem>(Jitem.obj.ToString());
                        if (obj.detectItem.Count > 0)
                        {
                            CheckItem CI = new CheckItem();
                            for (int i = 0; i < obj.detectItem.Count; i++)
                            {
                                int rt = 0;
                                sb1.Length = 0;
                                sb1.AppendFormat("cid='{0}' ", obj.detectItem[i].id);
                               
                                dt = bll.GetDetectItem(sb1.ToString(), "", out err);

                                CI = obj.detectItem[i];
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    rt = bll.UpdateDetectItem(CI);
                                    if (rt == 1)
                                    {
                                        items = items + 1;
                                    }
                                }
                                else
                                {
                                    rt = bll.InsertDetectItem(CI);
                                    if (rt == 1)
                                    {
                                        items = items + 1;
                                    }
                                }
                            }    
                        }
                    }
                    msg.str6 = items.ToString();
                    msg.result = true;
                    break;
                #endregion

                #region 任务下载
                case MsgCode.MSG_DownTask:
                    try
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "CheckPlan")))
                            msg.result = true;
                        //if ("true".Equals(DownLoadTask((CheckPointInfo)msg.obj1)))
                        //    msg.result = true;
                        //msg.DownLoadTask = _CheckItemStandard;
                    }
                    catch (Exception ex)
                    {
                        msg.errMsg = ex.Message;
                        FileUtils.OprLog(0, "downloadTask-error", ex.ToString());
                    }
                    msg.DownLoadTask = _CheckItemStandard;
                    break;
                #endregion

                #region 重金属测试
                case MsgCode.MSG_COMM_TEST_HM:
                    if (_port.Open(msg.str1))
                    {
                        msg.result = TestHM(_port);
                    }
                    break;
                #endregion

                #region 获取国家局数据库数据
                case MsgCode.MSG_GETCOUNTRYDATA:
                    msg.Other = GetCountryData(msg.str1, msg.str2, msg.str3, msg.str4, msg.str5);
                    break;
                #endregion

                #region 重金属检测
                case MsgCode.MSG_DETECTE_START_HEAVYMETAL:
                    if (_port.Open(msg.str1))
                    {
                        HMProto hmProto = new HMProto(_port);
                        byte[] hand = ASCIIEncoding.ASCII.GetBytes("DY6300-2--201503");
                        if (hmProto.ProtocolPackage(0x1483, 0x010008, hand, 4000))
                        {
                            // 停止测量
                            if (hmProto.ProtocolPackage(0x0683, 0x01f801, new byte[] { 0x00, 0x0f }, 4000))
                            {
                                // 启动测量
                                if (hmProto.ProtocolPackage(msg.arg1, msg.arg2, msg.data, 4000))
                                {
                                    byte[] buffer = new byte[22];
                                    while (hmProto.ReceveProtocolPackage(15000)) // 10s没有数据认为已经结束测量了。
                                    {
                                        Array.Copy(hmProto.buffer, 0, buffer, 0, 22);
                                        Message msg2 = new Message
                                        {
                                            what = MsgCode.MSG_READ_HEAVYMETAL,
                                            //New Add byte
                                            data = new byte[22]
                                        };
                                        buffer.CopyTo(msg2.data, 0);
                                        msg2.result = true;
                                    }
                                    msg.result = true;
                                }
                            }
                        }
                    }
                    break;
                #endregion

                default:
                    msg.result = false;
                    break;
            }

            if (_port != null) _port.Close();
            if (this.target != null) target.SendMessage(msg, null);
        }

        /// <summary>
        /// 图像采集
        /// </summary>
        /// <returns></returns>
        private byte[] ReadImg(ComPort port)
        {  
            try
            {
                port.Clear();
                byte[] cmd = { 0x1b, 0x1c, 0x00 };
                port.Write(cmd, 0, cmd.Length);
                int timeout = 200;

                int HDR_LEN = 5;
                byte[] header = new byte[HDR_LEN];
                if (HDR_LEN == port.Read(header, 0, HDR_LEN, timeout))
                {
                    if (0x1b != header[0])
                        return null;
                    try
                    {
                        long WIDTH = (UInt32)header[1] + (UInt32)(header[2] << 8);
                        long HEIGHT = (UInt32)header[3] + (UInt32)(header[4] << 8);
                        long BITDEPTH = 2;
                        int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);
                        byte[] buffer = new byte[HDR_LEN + DATA_LEN];
                        Array.Copy(header, buffer, HDR_LEN);
                        if (DATA_LEN == port.Read(buffer, HDR_LEN, DATA_LEN, timeout * 5))
                        {
                            return buffer;
                        }
                        else
                        {
                            Console.WriteLine("图像采集错误，数据长度：" + buffer.Length);
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 快检服务中心-通讯测试
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool KjLogin(string username, string password, out string errMsg)
        {
            if (username.Length == 0) username = Global.KjServer.Kjuser_name;
            if (password.Length == 0) password = Global.KjServer.Kjpassword;

            errMsg = string.Empty;
            DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
            Global.KjServer.userLoginEntity = DyInterfaceHelper.KjService.UserLogin(username, password, out errMsg);
            if (Global.KjServer.userLoginEntity != null && Global.KjServer.userLoginEntity.token.Length > 0)
            {
                Global.KjServer.Kjrealname = Global.KjServer.userLoginEntity.user.realname;
                Global.KjServer.Kjd_depart_name = Global.KjServer.userLoginEntity.user.d_depart_name;
                Global.KjServer.Kjp_point_name = Global.KjServer.userLoginEntity.user.p_point_name;

                CFGUtils.SaveConfig("KjServerAddr", Global.KjServer.KjServerAddr);
                CFGUtils.SaveConfig("Kjuser_name", Global.KjServer.Kjuser_name);
                CFGUtils.SaveConfig("Kjpassword", Global.KjServer.Kjpassword);
                CFGUtils.SaveConfig("Kjrealname", Global.KjServer.Kjrealname);
                CFGUtils.SaveConfig("Kjd_depart_name", Global.KjServer.Kjd_depart_name);
                CFGUtils.SaveConfig("Kjp_point_name", Global.KjServer.Kjp_point_name);
                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);
                return true;
            }

            Global.KjServer.userLoginEntity = null;
            return false;
        }

        /// <summary>
        /// 校准AD
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        private bool CalibrationAD(ComPort comPort, int retry, string strValue)
        {
            for (int i = 0; i <= retry; ++i)
            {
                if (CalibrationAD(comPort, strValue))
                    return true;
                else
                    continue;
            }
            return false;
        }

        /// <summary>
        /// 分光光度通讯测试
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        private bool ADTest(ComPort comPort, int retry)
        {
            for (int i = 0; i <= retry; ++i)
            {
                if (ADTest(comPort))
                    return true;
                else
                    continue;
            }
            return false;
        }

        /// <summary>
        /// AD校准
        /// </summary>
        /// <param name="comPort">COM</param>
        /// <param name="strValue">校准值</param>
        /// <returns></returns>
        private bool CalibrationAD(ComPort comPort, string strValue)
        {
            //cdm={0x7E,0x11,0x00,0x02,strValue,(01+02+strValue),crc8,0x7E}
            _timeout = 4000;
            _length = 6;
            String str = Convert.ToString(long.Parse(strValue, 0), 16);
            str = str.Length == 3 ? "0" + str : str;
            byte[] data = Global.strToToHexByte(str);
            byte[] dt = new byte[2];
            if (data.Length == 1)
            {
                dt[0] = 0x00;
                dt[1] = data[0];
                data = dt;
            }
            byte[] writeData = { 0x7E, 0x11, 0x00, 0x02, data[0], data[1], 
                                (byte)(0x11 + 0x00 + 0x02 + data[0] + data[1]), 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            byte[] rtnData = new byte[_length];
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 分光光度通讯测试
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private bool ADTest(ComPort comPort)
        {
            _timeout = 1000;
            _length = 9;
            byte[] writeData = { 0x7E, 0x01, 0x00, 0x00, 0x01, 0x7E };
            byte[] rtnData = new byte[_length];
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            //返回数据9个字节
            //箱仪一体机分光光度和电池是一个模块，返回硬件编号是06就是OK的
            if (comPort.Read(rtnData, 0, _length, _timeout) >= 9)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="writeData">上位机发送的数据</param>
        /// <param name="rtnData">下位机返回的数据</param>
        /// <returns></returns>
        private bool checkCRC8(byte[] writeData, byte[] rtnData)
        {
            _length = rtnData.Length - 1;
            //校验标识头、尾
            if (rtnData[0] == rtnData[_length] && rtnData[_length] == 0x7E)
            {
                //0x01 0x02 AD通讯测试
                if (writeData[1] == 0x01 && rtnData[1] == 0x02)
                {
                    if (rtnData[4] == 0x06)
                    {
                        return true;
                    }
                }
                //0x11 0x12 AD校准
                else if (writeData[1] == 0x11 && rtnData[1] == 0x12)
                {
                    if (rtnData[4] == 0x12)
                    {
                        return true;
                    }
                }
                //0x15 0x16 读取AD数据
                else if (writeData[1] == 0x15 && rtnData[1] == 0x16)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    byte crc = 0x00;
                    //和校验
                    for (int i = 1; i <= _length + 3; i++)
                    {
                        crc += rtnData[i];
                    }
                    if (crc == rtnData[132])
                    {
                        return true;
                    }
                }
                //0x1B 0x1C 电池电量获取
                else if (writeData[1] == 0x1B && rtnData[1] == 0x1C)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    if (_length > 0)
                    {
                        return true;
                    }
                }
                //0x19 0x1A 电池充电放电状态获取
                else if (writeData[1] == 0x19 && rtnData[1] == 0x1A)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    if (_length > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取国家局数据库条目信息
        /// </summary>
        /// <param name="type">0条目 1详情</param>
        /// <param name="tableId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchK"></param>
        /// <returns></returns>
        private string GetCountryData(string type, string tableId, string pageIndex, string pageSize, string searchK)
        {
            string rtnJson = string.Empty, url = string.Empty;
            if (type == "0")
            {
                url = "http://appcfda.drugwebcn.com/datasearch/QueryList?tableId={0}&searchF=Quick Search&pageIndex={1}&pageSize={2}&searchK={3}";
                url = string.Format(url, tableId, pageIndex, pageSize, searchK);
                rtnJson = Global.HttpMath("POST", url, "", Encoding.UTF8);
            }
            else
            {
                url = "http://appcfda.drugwebcn.com/datasearch/QueryRecord?tableId={0}&searchF=ID&pageIndex={1}&pageSize={2}&searchK={3}";
                url = string.Format(url, tableId, pageIndex, pageSize, searchK);
                rtnJson = Global.HttpMath("POST", url, "", Encoding.Default);
            }

            return rtnJson;
        }

        private bool TestHM(ComPort comPort)
        {
            byte[] cmd = { 0xAA, 0xBB, 0x14, 0x83, 0x01, 0x00, 0x08 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            byte[] buffer = new byte[256];
            byte[] chs = new byte[1];
            byte ch;
            int KcCmd, KcPara;
            int count = 0;
            int MaxSize = 256;
            _timeout = 5000;
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > _timeout)
                    break;

                if (comPort.Read(chs, 0, 1, 100) > 0)
                    ch = chs[0];
                else
                {
                    continue;
                }
                if (count == 0)
                {
                    if (ch == 0xAA)
                    {
                        buffer[count++] = 0xAA;
                    }
                }
                else
                {
                    if (count >= MaxSize)
                    {
                        count = 0;
                    }
                    else
                    {
                        buffer[count++] = ch;
                    }
                    if (buffer[1] != 0xBB)
                        count = 0;
                    if (count >= 7)
                    {
                        KcCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                        KcPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                | ((buffer[4] & 0xFF) << 16);
                        if ((KcCmd == 0x0483) && (KcPara == 0x010008))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public class HMProto
        {
            public int RecCmd;
            public int RecPara;
            public int MaxSize = 256;
            public byte[] buffer = new byte[256];
            public ComPort comPort;
            public HMProto(ComPort comPort)
            {
                this.comPort = comPort;
            }

            public bool ProtocolPackage(int cmd, int para, byte[] senddata, int timeout)
            {
                //int datalen = null == senddata ? 7 : 7 + senddata.Length;
                //byte[] data = new byte[datalen];
                byte[] data = new byte[64];
                data[0] = 0xaa;
                data[1] = 0xbb;
                data[2] = (byte)(cmd >> 8);
                data[3] = (byte)(cmd);
                data[4] = (byte)(para >> 16);
                data[5] = (byte)(para >> 8);
                data[6] = (byte)(para);
                if (null != senddata)
                {
                    senddata.CopyTo(data, 7);
                }
                comPort.Clear();
                comPort.Write(data, 0, data.Length);
                byte[] chs = new byte[1];
                byte ch;
                int count = 0;
                RecCmd = 0;
                RecPara = 0;
                DateTime beginTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                        break;

                    if (comPort.Read(chs, 0, 1, 100) > 0)
                    {
                        ch = chs[0];
                    }
                    else
                    {
                        continue;
                    }
                    if (count == 0)
                    {
                        if (ch == 0xAA)
                        {
                            buffer[count++] = 0xAA;
                        }
                    }
                    else
                    {
                        if (count >= MaxSize)
                        {
                            count = 0;
                        }
                        else
                        {
                            buffer[count++] = ch;
                        }
                        if (buffer[1] != 0xBB)
                            count = 0;
                        if (count >= 56)
                        {
                            RecCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                            RecPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                    | ((buffer[4] & 0xFF) << 16);
                            return true;
                        }
                    }
                }
                return false;
            }

            public bool ReceveProtocolPackage(int timeout)
            {
                byte[] chs = new byte[1];
                byte ch;
                int count = 0;
                RecCmd = 0;
                RecPara = 0;
                DateTime beginTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                        break;
                    if (comPort.Read(chs, 0, 1, 100) > 0)
                    {
                        ch = chs[0];
                    }
                    else
                    {
                        continue;
                    }

                    if (count == 0)
                    {
                        if (ch == 0xAA)
                        {
                            buffer[count++] = 0xAA;
                        }
                    }
                    else
                    {
                        if (count >= MaxSize)
                        {
                            count = 0;
                        }
                        else
                        {
                            buffer[count++] = ch;
                        }
                        if (buffer[1] != 0xBB)
                            count = 0;
                        if (count >= 56)
                        {
                            RecCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                            RecPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                    | ((buffer[4] & 0xFF) << 16);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 检测是否有卡
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="isTest"></param>
        /// <returns></returns>
        private bool jbkCheckCard(ComPort comPort)
        {
            comPort.Clear();
            _timeout = 1000;
            _length = 7;
            byte[] receiveData = new byte[_length];
            byte[] sendBuffer = null;
            //验证是否有卡
            //if (Global.JtjVersion != 3)
            //{
            //    _agreement.cmd = 0x13;
            //    _agreement.len[1] = 0x00;
            //    sendBuffer = _agreement.getAgreement();
            //}
            //else
            //{
            sendBuffer = CMD.GetBuffer(0x13);
            //}
            comPort.Write(sendBuffer, 0, sendBuffer.Length);
            //返回数据len个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData) || CMD.Validation(receiveData))
                {
                    if (receiveData[4] == 0x01)//有卡
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary> 
        /// 金标卡校准
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private bool jbkCBT(ComPort comPort)
        {
            _timeout = 1000;
            _length = 6;
            byte[] receiveData = new byte[_length];
            byte[] sendBuffer = null;
            //if (Global.JtjVersion != 3)
            //{
            //    _agreement.cmd = 0x17;//0x17金标卡校准
            //    _agreement.len[1] = 0x00;
            //    _agreement.data = 0x00;
            //    sendBuffer = _agreement.getAgreement();
            //}
            //else
            //{
            sendBuffer = CMD.GetBuffer(0x17);
            //}
            comPort.Clear();
            comPort.Write(sendBuffer, 0, sendBuffer.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 金标卡进卡
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] jbkIN(ComPort comPort, bool isTest)
        {
            _timeout = 1000;
            _length = 7;
            byte[] receiveData = new byte[_length];
            byte[] sendBuffer = null;
            //验证是否有卡
            //if (Global.JtjVersion != 3)
            //{
            //    _agreement.cmd = 0x13;
            //    _agreement.len[1] = 0x00;
            //    sendBuffer = _agreement.getAgreement();
            //}
            //else
            //{
            sendBuffer = CMD.GetBuffer(0x13);
            //}
            comPort.Clear();
            comPort.Write(sendBuffer, 0, sendBuffer.Length);
            //返回数据len个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData) || CMD.Validation(receiveData))
                {
                    byte[] InSendBuffer = null;
                    //if (Global.JtjVersion != 3)
                    //{
                    //    _agreement.cmd = 0x11;
                    //    _agreement.data = (byte)((isTest && receiveData[4] == 0x01) ? 0x01 : 0x03);
                    //    _agreement.len[1] = 0x01;
                    //    InSendBuffer = _agreement.getAgreement();
                    //}
                    //else
                    //{
                    byte[] data = new byte[1];
                    data[0] = (byte)((isTest && receiveData[4] == 0x01) ? 0x01 : 0x03);
                    InSendBuffer = CMD.GetBuffer(0x11, data);
                    //}
                    comPort.Clear();
                    comPort.Write(InSendBuffer, 0, InSendBuffer.Length);
                    receiveData = new byte[_length];
                    _length = 6;
                    receiveData = new byte[_length];
                    if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
                    {
                        if (ValidatedLegal(receiveData) || CMD.Validation(receiveData))
                            return receiveData;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 金标卡出卡 出卡时获取版本号
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private bool jbkOUT(ComPort comPort)
        {
            _timeout = 1000;
            _length = 6;
            //下位机返回数据
            byte[] receiveData = new byte[_length];
            byte[] sendBuffer = null;
            //if (Global.JtjVersion != 3)
            //{
            //    _agreement.cmd = 0x11;
            //    _agreement.data = 0x02; //0x02出卡
            //    _agreement.len[1] = 0x01;
            //    sendBuffer = _agreement.getAgreement();
            //}
            //else
            //{
            byte[] data = new byte[1];
            data[0] = 0x02;//0x02出卡
            sendBuffer = CMD.GetBuffer(0x11, data);
            //}

            comPort.Clear();
            comPort.Write(sendBuffer, 0, sendBuffer.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData) || CMD.Validation(receiveData))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 验证金标卡数据合法性
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private bool ValidatedLegal(byte[] bytes)
        {
            int len = bytes.Length;
            if (bytes[0] == 0x7E && bytes[len - 1] == 0x7E)
            {
                //进/出卡响应
                if (bytes[1] == 0x12 && bytes[3] == 0x00)
                {
                    //和校验
                    if ((bytes[1] + bytes[2] + bytes[3]) == bytes[4])
                        return true;
                }
                //卡状态响应 0x01有卡
                else if (bytes[1] == 0x14)
                {
                    if ((bytes[1] + bytes[2] + bytes[3] + bytes[4]) == bytes[5])
                        return true;
                }
                //校准响应
                else if (bytes[1] == 0x18)
                {
                    if ((bytes[1] + bytes[2] + bytes[3]) == bytes[4])
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 胶体金新摄像头 进卡/出卡(0x01|0x03：进卡;0x02：出卡|0x04: 二维码位置)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool InOrOutCard(ComPort port, byte dt)
        {
            try
            {
                port.Clear();
                byte[] data = new byte[1];
                data[0] = dt;
                byte[] buffer = CMD.GetBuffer(0x11, data);
                port.Write(buffer, 0, buffer.Length);
                int timeout = 1000;

                int HDR_LEN = 6;
                byte[] receiveData = new byte[HDR_LEN];
                if (HDR_LEN == port.Read(receiveData, 0, HDR_LEN, timeout))
                {
                    if (CMD.Validation(receiveData))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally { port.Clear(); }
            return false;
        }

        /// <summary>
        /// 获取电池充电放电状态
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] getBatteryState(ComPort comPort)
        {
            _timeout = 100;
            _length = 7;
            byte[] rtnData = new byte[_length];
            byte[] writeData = { 0x7E, 0x19, 0x00, 0x00, 0x19, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return rtnData;
                }
            }
            return null;
        }

        /// <summary>
        /// 电池电量请求
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] getBattery(ComPort comPort)
        {
            _timeout = 100;
            _length = 7;
            byte[] rtnData = new byte[_length];
            byte[] writeData = { 0x7E, 0x1B, 0x00, 0x00, 0x1B, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            //返回数据len个字节
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return rtnData;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private void JtjGetVersion(ComPort comPort)
        {
            if (Global.JtjVersionInfo == null)
            {
                try
                {
                    Global.JtjVersion = 1;
                    _timeout = 500;
                    _length = 9;
                    byte[] receiveData = new byte[_length];
                    byte[] sendBuffer = null;
                    //获取硬件版本信息协议使用旧模块的和校验，新模块不做校验
                    //_agreement.cmd = 0x01;
                    //_agreement.len[0] = 0x00;
                    //sendBuffer = _agreement.getAgreement();
                    sendBuffer = CMD.GetBuffer(0x01);//胶体金3.0版本协议获取
                    comPort.Clear();
                    comPort.Write(sendBuffer, 0, sendBuffer.Length);
                    comPort.Read(receiveData, 0, _length, _timeout);
                    //头和命令编号相符时开始记录crc
                    if (receiveData[0] == 0x7e && receiveData[1] == 0x02 && receiveData[8] == 0x7e)
                    {
                        //兼容旧模块
                        if (/*receiveData[7] == (receiveData[1] + receiveData[2] + receiveData[3] + receiveData[4] + receiveData[5] + receiveData[6]) ||
                            */CMD.Validation(receiveData))//新摄像头校验
                        {
                            Global.JtjVersionInfo = new byte[3];
                            Global.JtjVersionInfo[0] = receiveData[4];
                            Global.JtjVersionInfo[1] = receiveData[5];
                            Global.JtjVersionInfo[2] = receiveData[6];
                            Global.JtjVersion = receiveData[5] == 0x30 ? 3 : 2;
                            //System.Windows.Forms.MessageBox.Show("版本信息获取成功");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { CFGUtils.SaveConfig("JtjVersion", Global.JtjVersion.ToString()); }
            }
        }

        /// <summary>
        /// 胶体金3.0固件下载（下发到模块）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DownLoadFirmware(byte[] data)
        {
            try
            {
                _port.Clear();
                _port.Write(data, 0, data.Length);
                int timeout = 1000;

                int HDR_LEN = 7;
                byte[] receiveData = new byte[HDR_LEN];
                if (HDR_LEN == _port.Read(receiveData, 0, HDR_LEN, timeout))
                {
                    if (CMD.Validation(receiveData))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 胶体金3.0更新固件(同时验证固件的长度和CRC8)
        /// </summary>
        /// <param name="crc8"></param>
        /// <returns></returns>
        private bool Update(byte crc8, uint length)
        {
            try
            {
                _port.Clear();
                byte[] data = new byte[5];
                data[0] = crc8;
                //固件长度为uint32类型
                data[1] = (byte)(length & 0xFF);
                data[2] = (byte)((length >> 8) & 0xFF);
                data[3] = (byte)((length >> 16) & 0xFF);
                data[4] = (byte)((length >> 24) & 0xFF);
                byte[] buffer = CMD.GetBuffer(0x24, data);

                _port.Write(buffer, 0, buffer.Length);
                int timeout = 10000;

                int HDR_LEN = 7;
                byte[] receiveData = new byte[HDR_LEN];
                if (HDR_LEN == _port.Read(receiveData, 0, HDR_LEN, timeout))
                {
                    if (CMD.Validation(receiveData))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 读取AD值 2016年5月9日 wenj
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] ReadADValue(ComPort comPort)
        {
            _timeout = 1000;
            _length = 134;
            byte[] data = new byte[128];
            // 发送0x7E 0x15 0x00 0x00 crc8 0x7E
            byte[] writeData = { 0x7E, 0x15, 0x00, 0x00, 0x15, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            //返回134个字节 两个字节的标识头和尾、1byte CMD、2byte LEN、1byte CRC  data(一个板64个字节*2)
            byte[] rtnData = new byte[_length];
            if (comPort.Read(rtnData, 0, _length, _timeout) >= _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    for (int i = 0; i < rtnData.Length; i++)
                    {
                        data[i] = rtnData[i + 4];
                        if (i == 127)
                            break;
                    }
                    return data;
                }
            }
            return null;
        }

        private bool WhileRead = false;
        public void StartWhileRead(Message m, ChildThread target)
        {
            WhileRead = true;
            this.SendMessage(m, target);
        }

        public void StopWhileRead()
        {
            WhileRead = false;
        }

        /// <summary>
        /// 新方法 验证连接地址，用户和密码的正确性
        /// 新方法不需要MD5加密pwd
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>json</returns>
        public String checkUserConnection(string url, String user, String password)
        {
            string rtn = string.Empty;
            NewInterface.dataSync ds = new NewInterface.dataSync
            {
                Url = url
            };
            rtn = ds.checkUserConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString());
            return rtn;
        }

        /// <summary>
        /// 仪器检测项目下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string DownItems(string url, string checkNumber, string checkName, string checkType, string checkOrg, string username, string pwd)
        {
            string rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService
                {
                    Url = url
                };
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    //string data_Items = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", checkNumber.Substring(0, checkNumber.Length - 3), checkNumber, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), "Instrumen,DY3000");
                    //_CheckItemSecondDisplay = data_Items;
                    _CheckItemSecondDisplay = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", username, pwd, "Instrumen,DY3000", "");
                }
                return rtn;
            }
            catch (Exception)
            {
                return "false";
            }
        }

        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        /// <summary>
        /// 上传至食安科技监管平台
        /// </summary>
        /// <param name="server">服务器相关</param>
        /// <param name="results">上传的数据</param>
        /// <returns>结果</returns>
        private string UploadResult(CheckPointInfo server, DataTable results)
        {
            string rtn = string.Empty, upStr = string.Empty;
            try
            {
                int dataLen = results.Rows.Count, pageSize = 10, pageCount = Global.PageCount(dataLen, pageSize);
                //分页索引从1开始
                DataTable upDtbl = null;
                DataSet dst = null;

                for (int i = 1; i <= pageCount; i++)
                {
                    try
                    {
                        upDtbl = Global.GetPagedTable(results, i, pageSize);
                        #region 验证必填
                        string dtValue = string.Empty;
                        for (int j = 0; j < upDtbl.Rows.Count; j++)
                        {
                            //记录编号
                            try
                            {
                                dtValue = upDtbl.Rows[j]["SysCode"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["SysCode"] = Global.GETGUID(null, 1);
                                }
                                //检测编号
                                dtValue = upDtbl.Rows[j]["CheckNo"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckNo"] = DateTime.Now.ToString("yyyyMMddHHmmssff") + j;
                                }
                                //被检对象名称
                                dtValue = upDtbl.Rows[j]["CheckedCompany"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckedCompany"] = "默认被检单位";
                                }
                                //被样品名称
                                dtValue = upDtbl.Rows[j]["FoodName"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["FoodName"] = "未知样";
                                }
                                //被检样品种类
                                dtValue = upDtbl.Rows[j]["FoodType"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["FoodType"] = "样品";
                                }
                                //检测项目
                                dtValue = upDtbl.Rows[j]["CheckTotalItem"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckTotalItem"] = "未知项目";
                                }
                                //检测值
                                dtValue = upDtbl.Rows[j]["CheckValueInfo"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckValueInfo"] = "0";
                                }
                                //检测标准值
                                dtValue = upDtbl.Rows[j]["StandValue"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["StandValue"] = "0";
                                }
                                //检测结论 （合格、不合格）
                                dtValue = upDtbl.Rows[j]["Result"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["Result"] = "不合格";
                                }
                                //检测值单位
                                dtValue = upDtbl.Rows[j]["ResultInfo"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["ResultInfo"] = "NA";
                                }
                                //检测单位所属行政机构名称
                                dtValue = upDtbl.Rows[j]["CheckPlace"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckPlace"] = Global.samplenameadapter[0].Organization;
                                }
                                //检测单位所属行政机构编号
                                dtValue = upDtbl.Rows[j]["CheckPlaceCode"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckPlaceCode"] = Global.samplenameadapter[0].CheckPlaceCode;
                                }
                                //检测单位名称
                                dtValue = upDtbl.Rows[j]["CheckUnitName"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["CheckUnitName"] = Global.samplenameadapter[0].CheckPointName;
                                }
                                //基层上传人
                                dtValue = upDtbl.Rows[j]["UpLoader"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["UpLoader"] = LoginWindow._userAccount.UserName;
                                }
                                //抽检日期
                                dtValue = upDtbl.Rows[j]["TakeDate"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["TakeDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                                }
                                //检测依据或标准
                                dtValue = upDtbl.Rows[j]["Standard"].ToString();
                                if (dtValue == null || dtValue.Length == 0)
                                {
                                    upDtbl.Rows[j]["Standard"] = "NA";
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        #endregion
                        dst = new DataSet("UpdateResult");
                        dst.Tables.Add(upDtbl.Copy());
                        string xml = dst.GetXml();
                        Global.Log("数据上传", xml);
                        rtn = Global.Upload(xml, server.RegisterID, server.RegisterPassword, server.CheckPointID, server.ServerAddr);
                        Global.Log("上传响应", rtn);
                        dst = new DataSet();
                        using (StringReader sr = new StringReader(rtn))
                        {
                            dst.ReadXml(sr);
                        }
                        string error = string.Empty;
                        DataTable rtnDtbl = dst.Tables["Result"];
                        string result = Global.GetResultByCode(rtnDtbl.Rows[0]["ResultCode"].ToString());
                        string where = string.Empty;
                        if (result.Equals("1"))
                        {
                            if (upDtbl != null && upDtbl.Rows.Count > 0)
                            {
                                for (int j = 0; j < upDtbl.Rows.Count; j++)
                                {
                                    where += where.Length > 0 ? "," + string.Format("'{0}'", upDtbl.Rows[j]["SysCode"]) : string.Format("'{0}'", upDtbl.Rows[j]["SysCode"]);
                                    Global.UploadSCount++;

                                    //_resultTable.UpdateUploads(upDtbl.Rows[j]["SysCode"].ToString(), out error);
                                    //if (upDtbl.Rows[j]["CheckPlanCode"].ToString().Length > 0)
                                    //{
                                    //    _resultTable.UpdateUploadTask(upDtbl.Rows[j]["CheckPlanCode"].ToString(), out error);
                                    //}
                                    ////更新任务状态
                                    //if (error.Length == 0) Global.UploadSCount++;
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUploads(where, out error);
                                    _resultTable.UpdateUploadTasks(where, out error);
                                    //if (error.Length == 0) Global.UploadSCount++;
                                }
                            }
                        }
                        else if (result.Equals("2"))
                        {
                            where = string.Empty;
                            result = rtnDtbl.Rows[0]["ResultInfo"].ToString();
                            //数组下标 0为add成功的部分；1为update成功的部分；2为不成功的部分
                            string[] ResultInfo = result.Split('|');
                            string[] addResult = null, updateResult = null, otherResult = null;

                            //add成功的部分
                            if (ResultInfo[0].Length > 0)
                            {
                                addResult = ResultInfo[0].Split(',');
                                if (addResult.Length > 0)
                                {
                                    for (int j = 0; j < addResult.Length; j++)
                                    {
                                        if (addResult[j].Length > 0)
                                        {
                                            where += where.Length > 0 ? "," + string.Format("'{0}'", addResult[j]) : string.Format("'{0}'", addResult[j]);
                                            Global.UploadSCount++;
                                        }
                                    }
                                    if (where.Length > 0)
                                    {
                                        where = string.Format("({0})", where);
                                        _resultTable.UpdateUploads(where, out error);
                                        _resultTable.UpdateUploadTasks(where, out error);
                                        //if (error.Length == 0) Global.UploadSCount++;
                                    }
                                }
                            }
                            //update成功的部分
                            if (ResultInfo[1].Length > 0)
                            {
                                where = string.Empty;
                                updateResult = ResultInfo[1].Split(',');
                                if (updateResult.Length > 0)
                                {
                                    for (int j = 0; j < updateResult.Length; j++)
                                    {
                                        if (updateResult[j].Length > 0)
                                        {
                                            where += where.Length > 0 ? "," + string.Format("'{0}'", updateResult[j]) : string.Format("'{0}'", updateResult[j]);
                                            Global.UploadSCount++;
                                        }
                                    }
                                    if (where.Length > 0)
                                    {
                                        where = string.Format("({0})", where);
                                        _resultTable.UpdateUploads(where, out error);
                                        //if (error.Length == 0) Global.UploadSCount++;
                                    }
                                }
                            }
                            //失败的部分
                            if (ResultInfo[2].Length > 0)
                            {
                                otherResult = ResultInfo[2].Split(',');
                                if (otherResult.Length > 0)
                                {
                                    for (int j = 0; j < otherResult.Length; j++)
                                    {
                                        if (otherResult[j].Length > 0)
                                        {
                                            Global.UploadFCount++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            FileUtils.OprLog(0, "upload-error", dst.GetXml());
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "upload-error", rtn.Length > 0 ? rtn : ex.Message);
                        return rtn.Length > 0 ? rtn : ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(0, "upload-error", ex.ToString());
                return ex.Message;
            }
            return "";
        }

        /// <summary>
        /// 上传至安徽平台
        /// </summary>
        /// <param name="server">服务器相关</param>
        /// <param name="results">上传的数据</param>
        /// <returns>结果</returns>
        private string UploadResultAH(List<tlsTtResultSecond> dataList)
        {
            string outErr = string.Empty;
            try
            {
                foreach (tlsTtResultSecond data in dataList)
                {
                    clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                    {
                        interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                        userName = Global.AnHuiInterface.userName,
                        instrument = Global.AnHuiInterface.instrument,
                        passWord = Global.AnHuiInterface.passWord
                    };
                    model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                    model.mac = Global.Mac;

                    model.gps = string.Empty;
                    model.fTpye = data.fTpye.Trim().Length == 0 ? "1214" : data.fTpye;
                    model.fName = data.FoodName.Trim().Length == 0 ? "样品" : data.FoodName;
                    model.tradeMark = string.Empty;
                    model.foodcode = string.Empty;
                    model.proBatch = string.Empty;
                    model.proDate = data.DateManufacture;
                    model.proSpecifications = string.Empty;
                    model.manuUnit = string.Empty;
                    model.checkedNo = string.Empty;
                    model.sampleNo = data.SampleCode.Trim().Length == 0 ? DateTime.Now.ToString("yyyyMMddHHmmss") : data.SampleCode;
                    model.checkedUnit = data.CheckedCompanyCode.Length > 0 ? data.CheckedCompanyCode : "0";
                    model.dataNum = data.CheckNo.Trim().Length == 0 ? model.sampleNo : data.CheckNo;
                    model.testPro = data.testPro.Trim().Length == 0 ? "默认检测项目" : data.testPro;
                    model.quanOrQual = data.quanOrQual.Trim().Length == 0 ? "1" : data.quanOrQual;
                    model.contents = data.CheckValueInfo.Trim().Length == 0 ? "0" : data.CheckValueInfo;
                    model.unit = data.ResultInfo;
                    model.testResult = data.Result.Trim().Length == 0 ? "不合格" : data.Result;
                    model.dilutionRa = string.Empty;
                    model.testRange = string.Empty;
                    model.standardLimit = data.StandValue;
                    model.basedStandard = data.Standard;
                    model.testPerson = data.CheckUnitInfo.Trim().Length == 0 ? LoginWindow._userAccount.UserName : data.CheckUnitInfo;
                    try
                    {
                        DateTime dt = DateTime.Parse(data.CheckStartDate);
                        model.testTime = data.CheckStartDate;
                    }
                    catch (Exception)
                    {
                        model.testTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    model.sampleTime = data.TakeDate.Trim().Length == 0 ? model.testTime : data.TakeDate;
                    model.remark = data.ProceResults;
                    model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.testTime +
                        model.instrumentNo + model.contents + model.testResult);

                    string str = Global.AnHuiInterface.instrumentInfoHandle(model);
                    List<string> rtnList = Global.AnHuiInterface.ParsingUploadXML(str);
                    if (rtnList[0].Equals("1"))
                    {
                        Global.UploadSCount++;
                        _resultTable.UpdateUploads(string.Format("('{0}')", data.SysCode), out str);
                    }
                    else
                    {
                        Global.UploadFCount++;
                        outErr += "样品名称：[" + model.fName + "] 上传失败！\r\n异常信息：" + rtnList[1] + "；\r\n\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                outErr = ex.Message;
                Global.UploadSCount = 0;
            }
            return outErr;
        }

        /// <summary>
        /// 上传
        /// 目标地址：广东省智慧食药监管平台 || 快检服务
        /// </summary>
        /// <param name="selectedRecords"></param>
        /// <returns></returns>
        private String UploadResult(List<tlsTtResultSecond> selectedRecords)
        {
            if (selectedRecords == null || selectedRecords.Count == 0)
                return "暂无需要上传的数据";

            String outStr = String.Empty;
            Global.UploadSCount = 0;
            if (Global.InterfaceType.Equals("ZH"))
            {
                #region 上传至广东省智慧云平台
                uploadResult.Request model = null;
                IDictionary<String, uploadResult.Request> dicModel = null;
                try
                {
                    if (selectedRecords.Count > 0)
                    {
                        dicModel = new Dictionary<String, uploadResult.Request>();
                        foreach (tlsTtResultSecond item in selectedRecords)
                        {
                            if (!dicModel.ContainsKey(item.CheckTotalItem))
                            {
                                model = new uploadResult.Request
                                {
                                    username = LoginWindow._userAccount.UserName,
                                    itemid = item.CheckTotalItem,
                                    deviceid = Global.DeviceID,
                                    totalnum = selectedRecords.Count,
                                    longitude = string.Empty,
                                    latitude = string.Empty
                                };
                                dicModel.Add(item.CheckTotalItem, model);
                            }
                            uploadResult.Request.details details = new uploadResult.Request.details();
                            if (item.SampleId.Length > 2)
                                details.sampleid = item.SampleId.Substring(2, item.SampleId.Length - 2);
                            details.doublevalue = item.CheckValueInfo;
                            details.unit = item.ResultInfo;
                            if (item.Result.Equals("合格"))
                                details.stringvalue = "0";
                            else if (item.Result.Equals("不合格"))
                                details.stringvalue = "1";
                            else
                                details.stringvalue = "2";
                            details.time = item.CheckStartDate;
                            model.detailsList.Add(details);
                        }
                    }
                    if (dicModel.Count == 0)
                        return "暂无需要上传的数据";
                    List<string> list = new List<string>(dicModel.Keys);
                    for (int i = 0; i < list.Count; i++)
                    {
                        uploadResult.Request upModel = dicModel[list[i]];
                        Wisdom.UPLOADRESULT_REQUEST = upModel;
                        String result = Wisdom.HttpPostRequest(Wisdom.UPLOADRESULT);
                        if (result.Length > 0)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            uploadResult.Response json = js.Deserialize<uploadResult.Response>(result);
                            if (json != null)
                            {
                                if (json.code.Equals("0"))
                                {
                                    Global.UploadSCount += upModel.detailsList.Count;
                                    _resultTable.UpdateUploads(string.Format("('{0}')", selectedRecords[i].SysCode), out result);
                                }
                                else
                                {
                                    if (json.code.Equals("1"))
                                    {
                                        if (json.message.Length > 0)
                                            outStr += json.message + ";";
                                        else
                                            outStr = "上传参数缺失!";
                                    }
                                    else if (json.code.Equals("2"))
                                    {
                                        if (json.message.Length > 0)
                                            outStr += json.message + ";";
                                        else
                                            outStr = "json字符串格式不正确!";
                                    }
                                    else if (json.code.Equals("3"))
                                    {
                                        if (json.message.Length > 0)
                                            outStr += json.message + ";";
                                        else
                                            outStr = "系统处理失败，请稍后再试!";
                                    }
                                }
                            }
                        }
                    }

                    if (Global.UploadSCount == 0)
                    {
                        return outStr;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                #endregion
            }
            else if (Global.InterfaceType.Equals("KJ"))
            {
                #region 上传至快件服务云平台
                Global.UploadSCount = 0;
                string errMsg = string.Empty;
                DyInterfaceHelper.KjService.Result model = null;
                DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem taskItem = null;
                DyInterfaceHelper.KjService.Regulatory.RegulatoryItem company = null;
                DyInterfaceHelper.KjService.BusinessEntity.Business business = null;
                if (Global.KjServer.receiveTasksEntity == null)
                {
                    try
                    {
                        Global.KjServer.receiveTasksEntity = DyInterfaceHelper.KjService.ReceiveTasks(Global.KjServer.userLoginEntity.token, "", out errMsg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                //常规模式
                foreach (tlsTtResultSecond item in selectedRecords)
                {
                    if (Global.KjServer.receiveTasksEntity != null && Global.KjServer.receiveTasksEntity.tasks.Count > 0)
                    {
                        for (int i = 0; i < Global.KjServer.receiveTasksEntity.tasks.Count; i++)
                        {
                            if (Global.KjServer.receiveTasksEntity.tasks[i].t_id.Equals(item.CheckPlanCode))
                            {
                                taskItem = Global.KjServer.receiveTasksEntity.tasks[i];
                                break;
                            }
                        }
                    }

                    //允许任务为空
                    //if (taskItem == null)
                    //{
                    //    continue;
                    //}

                    string[] companys = item.CheckedCompany.Split('-');
                    if (companys.Length >= 2)
                    {
                        //平台录入的市场名称一定不能有"-"符号，否则将导致上传失败。
                        item.CheckedCompany = companys[0];
                        for (int i = 1; i < companys.Length; i++)
                        {
                            //此处是解决经营户名称中有"-"特殊字符串导致无法上传的bug
                            item.CheckedComDis += (item.CheckedComDis.Length > 0 ? "-" : "") + companys[i];
                        }
                    }

                    if (Global.KjServer.companys == null)
                    {
                        Global.KjServer.companys = DyInterfaceHelper.KjService.GetRegulatory(Global.KjServer.userLoginEntity.token);
                    }

                    if (Global.KjServer.companys != null)
                    {
                        for (int i = 0; i < Global.KjServer.companys.Count; i++)
                        {
                            if (item.CheckedCompany.Equals(Global.KjServer.companys[i].reg_name))
                            {
                                company = Global.KjServer.companys[i];
                                break;
                            }
                        }
                    }

                    if (company == null)
                    {
                        continue;
                    }

                    if (Global.KjServer.business == null)
                    {
                        Global.KjServer.business = DyInterfaceHelper.KjService.GetBusiness(Global.KjServer.userLoginEntity.token);
                    }
                    //20180725 改上传档口号不对
                    Global.KjServer.bcount = DyInterfaceHelper.KjService.GetBusiness(Global.KjServer.userLoginEntity.token);
                    Global.KjServer.bcount.Clear();

                    if (Global.KjServer.business != null)
                    {
                        for (int i = 0; i < Global.KjServer.business.Count; i++)
                        {
                            //if (item.CheckedComDis.Equals(Global.KjServer.business[i].ope_shop_code))
                            //{
                            //    business = Global.KjServer.business[i];
                            //    break;
                            //}
                            if (company.id.Equals(Global.KjServer.business[i].reg_id))
                            {
                                Global.KjServer.bcount.Add(Global.KjServer.business[i]);
                            }
                        }
                    }
                    for (int k = 0; k < Global.KjServer.bcount.Count; k++)
                    {
                        if (item.CheckedComDis.Equals(Global.KjServer.bcount[k].ope_shop_code))
                        {
                            business = Global.KjServer.bcount[k];
                            break;
                        }

                    }

                    model = new DyInterfaceHelper.KjService.Result
                    {
                        id = item.SysCode.Replace("-", "").ToLower(),
                        taskId = taskItem == null ? "" : taskItem.d_task_id,//任务ID,检测任务下载中获取(必填)
                        taskName = taskItem == null ? "" : taskItem.t_task_title,//任务主题,检测任务下载中获取(必填)
                        samplingId = "",// tasks.sampling_id;//抽样单ID,检测任务下载中获取(必填)
                        samplingDetailId = "",// tasks.id;//抽样单明细ID,检测任务下载中获取(必填)
                        foodId = taskItem == null ? "" : taskItem.d_sample_id,//样品ID（必填）
                        foodName = taskItem == null ? item.FoodName : taskItem.d_sample,//样品名称（必填）
                        itemId = taskItem == null ? "" : taskItem.d_item_id,//检测项目ID
                        itemName = taskItem == null ? item.CheckTotalItem : taskItem.d_item,//检测项目
                        checkResult = item.CheckValueInfo,//检测值
                        limitValue = item.StandValue,//限定值
                        checkAccordId = "",//检测标准ID
                        checkAccord = item.Standard,//检测标准
                        checkUnit = item.ResultInfo,//检测结果单位
                        conclusion = item.Result,//检测结论
                        regId = company.id,//被检单位ID
                        regName = company.reg_name,//被检单位
                        regUserId = business == null ? "" : business.id,//经营户ID
                        regUserName = business == null ? "" : business.ope_shop_code,//tasks.s_ope_shop_code;//经营户编号
                        checkUserid = Global.KjServer.userLoginEntity.user.id,//检测人员ID
                        checkUsername = Global.KjServer.userLoginEntity.user.realname,//检测人员姓名
                        checkDate = item.CheckStartDate,//检测时间
                        uploadId = Global.KjServer.userLoginEntity.user.id,//上报人员ID
                        uploadName = Global.KjServer.userLoginEntity.user.realname,//上报人员姓名
                        statusFalg = "0",//状态：0为未审核，1为已审核
                        auditorId = "",//审核人员ID
                        auditorName = "",//审核人名称
                        uploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),//上报时间
                        deviceId = Global.DeviceID,//检测设备唯一标识(必填)
                        deviceName = Global.InstrumentNameModel + Global.InstrumentName,//检测设备名称
                        deviceModel = item.ResultType,//检测模块
                        deviceMethod = item.CheckMethod,//检测方法
                        deviceCompany = item.MachineCompany,//仪器生产厂家
                        dataSource = "1",//数据来源:0检测工作站,1仪器上传,2.监管通app,3平台录入,4导入(必填)
                        departId = Global.KjServer.userLoginEntity.user.depart_id,//所属机构ID(departId与pointId二选一必填)
                        departName = Global.KjServer.userLoginEntity.user.d_depart_name,//所属机构名称
                        pointId = Global.KjServer.userLoginEntity.user.point_id,//检测点ID(departId与pointId二选一必填)
                        pointName = Global.KjServer.userLoginEntity.user.p_point_name,//检测点名称
                        foodTypeId = "",//样品父类ID
                        foodTypeName = "",//样品父类名称
                        remark = "",//备注
                        checkCode = "",//
                        param1 = taskItem == null ? "" : taskItem.d_id,//任务明细ID
                        param2 = "",//
                        param3 = ""//
                    };
                    string err = "";
                    //查询食品ID
                    if (model.foodId == "")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("food_name='{0}'", model.foodName );
                       
                        tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
                        DataTable dt = bll.GetSample(sb.ToString(), "", 1, out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            model.foodId = dt.Rows[0]["fid"].ToString();
                        }
                    }
                    if(model.itemId =="")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("detect_item_name='{0}'", model.itemName);
                        DataTable dt = bll.GetDetectItem(sb.ToString(), "", out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            model.itemId = dt.Rows[0]["cid"].ToString();
                        }
                    }

                    string json = Json.ObjectToJson(model);
                    DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                    DyInterfaceHelper.KjService.UploadData(Global.KjServer.userLoginEntity.token, json, out errMsg);

                    //上传成功，更新上传状态
                    if (errMsg.Length == 0)
                    {
                        _resultTable.UpdateUploads(string.Format("('{0}')", item.SysCode), out errMsg);
                        Global.UploadSCount++;
                    }
                }

                //DataTable dtbl = KjTasksOpr.GetAsDataTable(out errMsg);
                //Global.KjServer.samplingEntity.result = (List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>)IListDataSet.DataTableToIList<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>(dtbl, 1);
                //DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem tasks = null;
                //foreach (tlsTtResultSecond item in selectedRecords)
                //{
                //    for (int i = 0; i < Global.KjServer.samplingEntity.result.Count; i++)
                //    {
                //        tasks = Global.KjServer.samplingEntity.result[i];
                //        if (item.CheckPlanCode.Equals(tasks.id))
                //        {
                //            break;
                //        }
                //        else
                //        {
                //            tasks = null;
                //        }
                //    }

                //    if (tasks == null)
                //    {
                //        continue;
                //    }

                //    model = new DyInterfaceHelper.KjService.Result();
                //    model.id = item.SysCode.Replace("-", "").ToLower();
                //    model.taskId = tasks.t_id;//任务ID,检测任务下载中获取(必填)
                //    model.taskName = tasks.t_task_title;//任务主题,检测任务下载中获取(必填)
                //    model.samplingId = tasks.sampling_id;//抽样单ID,检测任务下载中获取(必填)
                //    model.samplingDetailId = tasks.id;//抽样单明细ID,检测任务下载中获取(必填)
                //    model.foodId = tasks.food_id;//样品ID（必填）
                //    model.foodName = tasks.food_name;//样品名称（必填）
                //    model.itemId = tasks.item_id;//检测项目ID
                //    model.itemName = tasks.item_name;//检测项目
                //    model.checkResult = item.CheckValueInfo;//检测值
                //    model.limitValue = item.StandValue;//限定值
                //    model.checkAccordId = "";//检测标准ID
                //    model.checkAccord = item.Standard;//检测标准
                //    model.checkUnit = item.ResultInfo;//检测结果单位
                //    model.conclusion = item.Result;//检测结论
                //    model.regId = tasks.s_reg_id;//被检单位ID
                //    model.regName = tasks.s_reg_name;//被检单位
                //    model.regUserId = "";//经营户ID
                //    model.regUserName = tasks.s_ope_shop_code;//经营户编号
                //    model.checkUserid = Global.KjServer.userLoginEntity.user.id;//检测人员ID
                //    model.checkUsername = Global.KjServer.userLoginEntity.user.realname;//检测人员姓名
                //    model.checkDate = item.CheckStartDate;//检测时间
                //    model.uploadId = Global.KjServer.userLoginEntity.user.id;//上报人员ID
                //    model.uploadName = Global.KjServer.userLoginEntity.user.realname;//上报人员姓名
                //    model.statusFalg = "0";//状态：0为未审核，1为已审核
                //    model.auditorId = "";//审核人员ID
                //    model.auditorName = "";//审核人名称
                //    model.uploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//上报时间
                //    model.deviceId = Global.DeviceID;//检测设备唯一标识(必填)
                //    model.deviceName = Global.InstrumentNameModel + Global.InstrumentName;//检测设备名称
                //    model.deviceModel = item.ResultType;//检测模块
                //    model.deviceMethod = item.CheckMethod;//检测方法
                //    model.deviceCompany = item.MachineCompany;//仪器生产厂家
                //    model.dataSource = "1";//数据来源:0检测工作站,1仪器上传,2.监管通app,3平台录入,4导入(必填)
                //    model.departId = Global.KjServer.userLoginEntity.user.depart_id;//所属机构ID(departId与pointId二选一必填)
                //    model.departName = Global.KjServer.userLoginEntity.user.d_depart_name;//所属机构名称
                //    model.pointId = Global.KjServer.userLoginEntity.user.point_id;//检测点ID(departId与pointId二选一必填)
                //    model.pointName = Global.KjServer.userLoginEntity.user.p_point_name;//检测点名称
                //    model.foodTypeId = "";//样品父类ID
                //    model.foodTypeName = "";//样品父类名称
                //    model.remark = "";//备注
                //    model.checkCode = "";//
                //    model.param1 = "";//
                //    model.param2 = "";//
                //    model.param3 = "";//
                //    string json = Json.ObjectToJson(model);
                //    DyInterfaceHelper.KjService.Url_Server = Global.KjServer.KjServerAddr;
                //    DyInterfaceHelper.KjService.UploadData(Global.KjServer.userLoginEntity.token, json, out errMsg);

                //    //上传成功，更新上传状态
                //    if (errMsg.Length == 0)
                //    {
                //        _resultTable.UpdateUploads(string.Format("('{0}')", item.SysCode), out errMsg);
                //        Global.UploadSCount++;
                //    }
                //    //上传失败，返回异常信息
                //    else
                //    {

                //    }
                //}
                #endregion
            }

            return outStr;
        }

        /// <summary>
        /// 全部数据下载
        /// 2016年12月22日 wenj update 新版本接口
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="downType">下载类型</param>
        /// <returns>结果</returns>
        private string DownLoadAllData(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd, string downType)
        {
            string UDate = string.Empty, rtn = string.Empty, sign = string.Empty,
                version = Global.Version.Equals("XZ") ? "行政版" : "企业版", outErr = string.Empty;
            DataTable dtUDate = null;
            try
            {
                //被检单位下载
                sign = "Company";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    //dtUDate = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 7);
                    //if (dtUDate != null && dtUDate.Rows.Count > 0)
                    //{
                    //    List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dtUDate, 1);
                    //    UDate = listCompany[0].UDate;
                    //}

                    //被检单位需每次重新下载，无需增量下载，且每次都需要将数据清空
                    _clsCompanyOpr.Delete(string.Empty, out outErr);
                    UDate = string.Empty;
                    _DownCompany = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //样品检测项目和对应检测标准数据下载
                sign = "SelectItem";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 2);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _SampleStandardAgainDisplay = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }
                //检测标准下载
                sign = "Standard";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 4);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsStandard> listSample = (List<clsStandard>)IListDataSet.DataTableToIList<clsStandard>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _Standard = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }
                //检测项目下载
                sign = "CheckItem";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 3);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItems = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //检测任务下载
                sign = "CheckPlan";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    ////检测任务需要实时更新，每次下载时先清空数据库
                    //_clsCompanyOpr.Delete(string.Empty, out outErr, "tTask");
                    UDate = string.Empty;
                    _CheckItemStandard = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }
                return "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _port.Dispose();
            }
            _disposed = true;
        }

        private class CMD
        {
            /// <summary>
            /// 获取通讯协议数据包(如果长度为0，直接传入CMD即可)
            /// </summary>
            /// <param name="cmd">CMD</param>
            /// <param name="len">DATA长度</param>
            /// <param name="data">数据包</param>
            /// <returns>返回完整数据包</returns>
            public static byte[] GetBuffer(byte cmd, byte[] data = null)
            {
                try
                {
                    bool isCrc8 = Global.JtjVersion == 3;
                    List<byte> dataList = new List<byte>();
                    dataList.Add(0x7E);//标识头
                    dataList.Add(cmd);//CMD命令
                    dataList.Add(0x00);//len
                    dataList.Add(0x00);//len
                    //将数据包追加到List后面
                    if (data != null && data.Length > 0)
                    {
                        if (isCrc8)//胶体金3.0是低位在前
                        {
                            dataList[2] = (byte)(data.Length & 0xFF);
                            dataList[3] = (byte)((data.Length >> 8) & 0xFF);
                        }
                        else//1.0和2.0是高位在前
                        {
                            dataList[3] = (byte)(data.Length & 0xFF);
                            dataList[2] = (byte)((data.Length >> 8) & 0xFF);
                        }
                        dataList.AddRange(data);
                    }
                    //CRC8数据包长度为当前List长度-1（去掉标识头）
                    byte[] crc = new byte[dataList.Count - 1];
                    //拷贝到CRC8数据包
                    dataList.CopyTo(1, crc, 0, crc.Length);
                    dataList.Add(isCrc8 ? CRC8(crc) : CRCSUM(crc));//CRC8校验值
                    dataList.Add(0x7E);//标识尾
                    byte[] buffer = new byte[dataList.Count];
                    dataList.CopyTo(buffer);
                    return buffer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            /// <summary>
            /// 数据校验（标识头标识尾，和CRC8校验）
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static bool Validation(byte[] data)
            {
                if (data == null || data.Length == 0) return false;

                //标识头标识尾校验
                if (data[0] != 0x7E || data[data.Length - 1] != 0x7E)
                {
                    return false;
                }

                byte cmd = data[1];
                switch (cmd)
                {
                    case 0x18://通讯测试响应
                    case 0x23://固件下载响应
                    case 0x25://固件升级响应
                        //0x00失败，0x01成功，另外通讯测试还有个0x02是需要提示进行固件升级的
                        return data[4] == 0x01 ? true : false;
                    default:
                        break;
                }

                //CRC8数组的长度为总长度-3（去掉标识头尾和CRC8）
                byte[] crcDt = new byte[data.Length - 3];
                Array.Copy(data, 1, crcDt, 0, crcDt.Length);
                byte crc8 = CMD.CRC8(crcDt);
                byte crcsum = CMD.CRCSUM(crcDt);
                //兼容CRC8与和校验
                if (data[data.Length - 2] == crc8 || data[data.Length - 2] == crcsum)
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// CRC8校验
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static byte CRC8(byte[] data)
            {
                byte crc = 0;
                for (int j = 0; j < data.Length; j++)
                {
                    crc = (byte)(crc ^ data[j]);
                    for (int i = 8; i > 0; i--)
                    {
                        if ((crc & 0x80) == 0x80)
                        {
                            crc = (byte)((crc << 1) ^ 0x31);
                        }
                        else
                        {
                            crc = (byte)(crc << 1);
                        }
                    }
                }

                return crc;
            }

            /// <summary>
            /// CRC和校验
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static byte CRCSUM(byte[] data)
            {
                byte crc = 0;
                if (data != null && data.Length > 0)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        crc += data[i];
                    }
                }

                return crc;
            }
        }

        //private class agreement
        //{
        //    public byte head = 0x7e;
        //    public byte tail = 0x7e;
        //    public byte cmd = 0x00;
        //    public byte[] len = { 0x00, 0x01 };
        //    public byte data = 0x00;
        //    public byte crc = 0x00;

        //    /// <summary>
        //    /// 获取协议
        //    /// </summary>
        //    /// <returns></returns>
        //    public byte[] getAgreement()
        //    {
        //        //如果数据长度为0，则忽略data
        //        if (cmd == 0x01 || cmd == 0x12 || cmd == 0x13 || cmd == 0x15)
        //        {
        //            len[0] = len[1] = 0x00;
        //            crc = (byte)(cmd + len[0] + len[1]);
        //        }
        //        else
        //        {
        //            crc = (byte)(cmd + len[0] + len[1] + data);
        //        }

        //        if (len[0] == 0x00 && len[1] == 0x00)
        //        {
        //            byte[] bt = { head, cmd, len[0], len[1], crc, tail };
        //            return bt;
        //        }
        //        else
        //        {
        //            byte[] bt = { head, cmd, len[0], len[1], data, crc, tail };
        //            return bt;
        //        }
        //    }
        //}

    }
}