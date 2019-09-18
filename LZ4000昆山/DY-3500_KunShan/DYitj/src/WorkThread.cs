using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataSentence.Kjc;

namespace AIO
{
    public class WorkThread : ChildThread, IDisposable
    {

        bool _disposed = false;
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
        /// <summary>
        /// 检测任务
        /// </summary>
        private string _CheckItemStandard;
        /// <summary>
        /// 被检单位
        /// </summary>
        private string _DownCompany;
        private clsCompanyOpr _clsCompanyOpr = new clsCompanyOpr();
        private clsttStandardDecideOpr _clsttStandardDecideOpr = new clsttStandardDecideOpr();

        protected override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            switch (msg.what)
            {
                case MsgCode.MSG_COMM_TEST:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            msg.result = true;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_AD:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            byte[] data = ReadADValue(_port);
                            if (null != data)
                            {
                                msg.result = true;
                                msg.data = data;
                            }
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_AD_CYCLE:
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            while (bWhileReadADCycle)
                            {
                                byte[] data = ReadADValue(_port);
                                if (null != data)
                                {
                                    msg.result = true;
                                    msg.data = data;
                                    if (null != this.target)
                                        target.SendMessage(msg, null);
                                }
                                DateUtils.WaitMs(20);
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

                // 设置TC线没有返回，这个最好是放在读取摄像头之前发送。
                case MsgCode.MSG_SET_TCLINE:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        string val = string.Empty;
                        for (int i = 0; i < msg.data.Length; i++)
                        {
                            val += msg.data[i] + ",";
                        }
                        System.Console.WriteLine(val);
                        WriteData(_port, msg.data, 0, msg.data.Length);
                        msg.result = true;
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_CAM:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = ReadCam(_port, msg.arg1);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                        }
                        else
                        {
                            DateUtils.WaitMs(3000);
                            FileUtils.ErrorLog("SXT ERROR", msg.str1, "NULL");
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_GRAYVALUES:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = ReadGrayValue(_port, msg.arg1, msg.arg2);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                        }

                        //byte[] value = ReadCTGrayValue(_port, msg.arg1, msg.arg2);
                        //if (value != null)
                        //{
                        //    msg.cValue = value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
                        //    msg.tValue = value[4] | (value[5] << 8) | (value[6] << 16) | (value[7] << 24); ;
                        //}
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_RGBVALUE:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = ReadRGBValue(_port, msg.arg1);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_PRINT:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            _port.Write(msg.data, msg.arg1, msg.arg2);
                            msg.result = true;
                        }
                        _port.Close();
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //2016年10月8日 wenj 通讯测试失败时仅保留URL用户名和密码；
                case MsgCode.MSG_CHECK_CONNECTION:
                    msg.result = false;
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
                                    Global.pointName = user[1];
                                    Global.pointType = user[2];
                                    Global.orgNum = user[3];
                                    Global.orgName = user[4];
                                    Global.userId = user[5];
                                    msg.result = true;
                                }
                            }
                            else
                            {
                                msg.errMsg = dtbl.Rows[0]["ResultDesc"].ToString();
                                Global.pointNum = string.Empty; Global.pointName = string.Empty;
                                Global.pointType = string.Empty; Global.orgNum = string.Empty; Global.orgName = string.Empty;
                                Global.userId = string.Empty;
                            }
                        }
                        else if (Global.InterfaceType.Equals("KJC"))//快检车接口
                        {
                            msg.responseInfo = InterfaceHelper.CheckUserCommunication(msg.str1, msg.str2, msg.str3);
                            ResultMsg msgResult = JsonHelper.JsonToEntity<ResultMsg>(msg.responseInfo);
                            if (msgResult.resultCode.Equals("success1"))
                            {
                                CheckUserConnect userInfo = JsonHelper.JsonToEntity<CheckUserConnect>(msgResult.result.ToString());
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
                    }
                    catch (Exception ex)
                    {
                        msg.errMsg = ex.Message;
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                ////昆山项目获取token信息
                //case MsgCode.KS_GETTOKEN:
                //    msg.result = false;
                //    KunShanHelper.GetToken(out msg.outError);
                //    msg.result = msg.outError.Length == 0 ? true : false;
                //    if (null != this.target)
                //        target.SendMessage(msg, null);
                //    break;

                //昆山项目获取市场信息
                case MsgCode.KS_QUERYMARKET:
                    KunShanHelper.GetToken(out msg.outError);
                    if (msg.outError.Length == 0)
                    {
                        KunShanHelper.GetMarket(out msg.outError);
                    }
                    msg.outError = getKsInfo(msg.outError);
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //获取分局单位主体信息
                case MsgCode.KS_GetAreaMarket:
                    msg.result = false;
                    KunShanHelper.GetAreaMarket(out msg.outError);
                    msg.result = msg.outError.Length == 0 ? true : false;
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //根据单位主体信息获取经营户
                case MsgCode.KS_QuerySignContactByAM:
                    msg.result = false;
                    if (msg.obj1 != null)
                    {
                        List<KunShanEntity.AreaMarket> models = msg.obj1 as List<KunShanEntity.AreaMarket>;
                        if (models != null)
                        {
                            string[,] AreaSignContacts = new string[2, models.Count];
                            string val = string.Empty;
                            for (int i = 0; i < models.Count; i++)
                            {
                                //if (!models[i].MarketName.Equals("昆山柏庐市场建设发展有限公司"))
                                //{
                                //    continue;
                                //}
                                //目前MarketRef只有为1或者2的时候才有经营户数据
                                if (!models[i].MarketRef.Equals("批发市场") && !models[i].MarketRef.Equals("农贸市场"))
                                {
                                    continue;
                                }
                                
                                val = KunShanHelper.GetAreaSignContact(models[i].LicenseNo, out msg.outError);
                                AreaSignContacts[0, i] = models[i].LicenseNo;
                                AreaSignContacts[1, i] = val;
                            }
                            msg.obj2 = AreaSignContacts;
                        }
                    }
                    msg.result = msg.outError.Length == 0 ? true : false;
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //昆山经营户下载
                case MsgCode.KS_QuerySignContact:
                    msg.result = false;
                    KunShanHelper.QuerySignContact(out msg.outError);
                    msg.result = msg.outError.Length == 0 ? true : false;
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //昆山检测项目下载
                case MsgCode.KS_QueryCheckItem:
                    msg.result = false;
                    KunShanHelper.QueryCheckItem(out msg.outError);
                    msg.result = msg.outError.Length == 0 ? true : false;
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //昆山检测品种下载
                case MsgCode.KS_QuerySalesItem:
                    msg.result = false;
                    KunShanHelper.QuerySalesItem(out msg.outError);
                    msg.result = msg.outError.Length == 0 ? true : false;
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;


                case MsgCode.MSG_UPLOAD:
                    msg.result = false;
                    Global.UploadSCount = Global.UploadFCount = 0;
                    msg.outError = UploadResult(msg.table);

                    msg.result = msg.outError.Length == 0 ? true : false;

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_CHECK_SYNC:
                    msg.result = false;
                    if (Global.InterfaceType.Equals("KJC"))
                    {
                        msg.DownLoadCompany = InterfaceHelper.DownloadBasicData(msg.str1, msg.str2, msg.str3, "company");
                        msg.result = true;
                    }
                    else if (Global.InterfaceType.Equals("DY"))
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "all")))
                            msg.result = true;
                        if (null != this.target)
                        {
                            msg.SampleStandardName = _SampleStandardAgainDisplay;
                            msg.CheckItemsTempList = _CheckItemSecondDisplay;
                            msg.CheckItems = _CheckItems;
                            msg.Standard = _Standard;
                            msg.CheckItemsAdapter = _CheckItemStandard;
                            msg.DownLoadCompany = _DownCompany;
                        }
                    }

                    target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_DownCompany:
                    msg.result = false;
                    if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "Company")))
                        msg.result = true;
                    if (null != this.target)
                    {
                        msg.DownLoadCompany = _DownCompany;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownCheckItems:
                    msg.result = false;
                    string url2 = msg.str1;
                    string username2 = msg.str2;
                    string pwd2 = msg.str3;
                    string checkNumber2 = msg.args.Dequeue();
                    string checkName2 = msg.args.Dequeue();
                    string checkType2 = msg.args.Dequeue();
                    string checkOrg2 = msg.args.Dequeue();
                    if ("true".Equals(DownItems(url2, checkNumber2, checkName2, checkType2, checkOrg2, username2, pwd2)))
                        msg.result = true;
                    if (null != this.target)
                    {
                        msg.CheckItemsTempList = _CheckItemSecondDisplay;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownTask:
                    msg.result = false;

                    if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "CheckPlan")))
                        msg.result = true;
                    #region 旧版本接口
                    //if ("true".Equals(DownLoadTask((CheckPointInfo)msg.obj1)))
                    //    msg.result = true;
                    #endregion

                    if (null != this.target)
                    {
                        msg.DownLoadTask = _CheckItemStandard;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_COMM_TEST_HM:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (TestHM(_port))
                            msg.result = true;
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                // 重金属检测
                case MsgCode.MSG_DETECTE_START_HEAVYMETAL:
                    msg.result = false;
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
                                        Message msg2 = new Message();
                                        msg2.what = MsgCode.MSG_READ_HEAVYMETAL;
                                        //New Add byte
                                        msg2.data = new byte[22];
                                        buffer.CopyTo(msg2.data, 0);
                                        msg2.result = true;
                                        if (null != this.target)
                                        {
                                            target.SendMessage(msg2, null);
                                        }
                                    }

                                    msg.result = true;
                                }
                            }
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    break;
                default:
                    msg.result = false;
                    break;
            }
        }

        private bool Test(ComPort comPort, int retry)
        {
            for (int i = 0; i <= retry; ++i)
            {
                if (Test(comPort))
                    return true;
                else
                    continue;
            }
            return false;
        }

        private bool Test(ComPort comPort)
        {
            byte[] cmd = { 0x03, 0xFF, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0xD4, 0x18, 0x44, 0x45, 0x56, 0x49, 0x43, 0x45, 0x3F, 0x3F };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            // 接收2个字节的头
            int rec_len = 12;
            byte[] rec = new byte[rec_len];
            if (rec_len == comPort.Read(rec, 0, rec_len, timeout))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            int timeout = 5000;
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
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
        /// 读取AD值
        /// AD返回的格式是这样的
        /// 返回0x1B LED_ROW（多少排）
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] ReadADValue(ComPort comPort)
        {
            // 发送0x1B 0x1B
            byte[] cmd = { 0x1b, 0x1b };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 4000;
            // 接收2个字节的头
            int HDR_LEN = 2;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, timeout))
            {
                if (0x1b != header[0])
                    return null;
                int LED_ROW = header[1];
                int LED_NUMS = 4;
                int LED_COL = 8;
                int DATA_LEN = LED_ROW * (1 + 4 * (LED_NUMS * LED_COL + 1) + 1);
                byte[] buffer = new byte[HDR_LEN + DATA_LEN];
                Array.Copy(header, buffer, HDR_LEN);
                if (DATA_LEN == comPort.Read(buffer, HDR_LEN, DATA_LEN, timeout))
                {
                    return buffer;
                }
            }
            return null;
        }

        private byte[] ReadCam(ComPort comPort, int camID)
        {
            //最开始的命令，根据ID来读取数据，后面厂家优化后全部改为0
            //byte[] cmd = { 0x1b, 0x1c, (byte)camID };
            byte[] cmd = { 0x1b, 0x1c, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 200;

            int HDR_LEN = 5;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, timeout))
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
                    //3秒钟超时
                    if (DATA_LEN == comPort.Read(buffer, HDR_LEN, DATA_LEN, timeout * 15))
                    {
                        return buffer;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private void WriteData(ComPort comPort, byte[] data, int offset, int count)
        {
            comPort.Clear();
            comPort.Write(data, offset, count);
        }

        private byte[] ReadGrayValue(ComPort comPort, int camID, int height)
        {
            //byte[] cmd = { 0x1b, 0x1d, (byte)camID };
            byte[] cmd = { 0x1b, 0x1d, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            int DATA_LEN = height;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                return buffer;
            }
            return null;
        }

        /// <summary>
        /// 胶体金 - 获取C/T灰阶值
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="camID"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private byte[] ReadCTGrayValue(ComPort comPort, int camID, int height)
        {
            byte[] rdCvalueCmd = { 0x1b, 0x21, 0x00 };
            byte[] rdTvalueCmd = { 0x1b, 0x21, 0x01 };
            int timeout = 1000;
            int DATA_LEN = 4;
            byte[] buffer = new byte[DATA_LEN];
            byte[] rtnValue = new byte[DATA_LEN * 2];

            comPort.Clear();
            comPort.Write(rdCvalueCmd, 0, rdCvalueCmd.Length);

            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                Array.Copy(buffer, rtnValue, 4);
            }
            else
            {
                return null;
            }

            comPort.Write(rdTvalueCmd, 0, rdTvalueCmd.Length);
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                Array.ConstrainedCopy(buffer, 0, rtnValue, 4, 4);
            }
            else
            {
                return null;
            }

            return rtnValue;
        }

        private bool bWhileReadADCycle = false;
        public void BeginStartReadADCycle(Message m, ChildThread target)
        {
            bWhileReadADCycle = true; ;
            this.SendMessage(m, target);
        }

        public void BeginStopReadADCycle()
        {
            bWhileReadADCycle = false;
        }

        private byte[] ReadRGBValue(ComPort comPort, int camID)
        {
            //byte[] cmd = { 0x1b, 0x20, (byte)camID };
            byte[] cmd = { 0x1b, 0x20, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            int DATA_LEN = 12;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                return buffer;
            }

            return null;
        }

        /// <summary>
        /// 上传
        /// 目标地址：广东省智慧食药监管平台
        /// </summary>
        /// <param name="selectedRecords"></param>
        /// <returns></returns>
        private String UploadResult(List<tlsTtResultSecond> selectedRecords)
        {
            if (selectedRecords == null || selectedRecords.Count == 0)
                return "暂无需要上传的数据";

            String outStr = String.Empty;
            Global.UploadSCount = 0;
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
                            model = new uploadResult.Request();
                            model.username = LoginWindow._userAccount.UserName;
                            model.itemid = item.CheckTotalItem;
                            model.deviceid = Wisdom.DeviceID;
                            model.totalnum = selectedRecords.Count;
                            model.longitude = string.Empty;
                            model.latitude = string.Empty;
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
                                string where = string.Empty;
                                for (int j = 0; j < selectedRecords.Count; j++)
                                {
                                    where += where.Length > 0 ? "," + string.Format("'{0}'", selectedRecords[j].SysCode) : string.Format("'{0}'", selectedRecords[j].SysCode);
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out result);
                                }
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

            return outStr;
        }

        /// <summary>
        /// 上传至快检车平台
        /// </summary>
        /// <param name="server"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private string KjcUpload(CheckPointInfo server, DataTable results)
        {
            Global.UploadSCount = 0;
            string rtn = string.Empty, upStr = string.Empty;
            kjcUploadEntity upDatas = new kjcUploadEntity();
            upDatas.result = new List<kjcUploadEntity.results>();
            kjcUploadEntity.results model = new kjcUploadEntity.results();
            tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
            DataTable dtbl = null;
            string dtValue = string.Empty, deviceModel = string.Empty;

            for (int i = 0; i < results.Rows.Count; i++)
            {
                dtbl = null;
                model = new kjcUploadEntity.results();
                //记录编号
                dtValue = results.Rows[i]["SysCode"].ToString();
                if (string.IsNullOrEmpty(dtValue) || !Global.IsGuidByReg(dtValue))
                {
                    model.sysCode = dtValue = Global.GETGUID();
                    _bll.InsertSysCode(dtValue, int.Parse(results.Rows[i]["ID"].ToString()), out dtValue);
                }
                model.sysCode = string.IsNullOrEmpty(model.sysCode) ? dtValue : model.sysCode;
                //被检样品种类
                dtValue = results.Rows[i]["FoodType"].ToString();
                model.foodType = string.IsNullOrEmpty(dtValue) ? "样品" : dtValue;
                //被样品名称
                dtValue = results.Rows[i]["FoodName"].ToString();
                model.foodName = string.IsNullOrEmpty(dtValue) ? "未知样" : dtValue;
                //抽样单号
                dtValue = results.Rows[i]["SampleId"].ToString();
                model.sampleNo = string.IsNullOrEmpty(dtValue) ? "" : dtValue;
                dtValue = "";//results.Rows[i]["Sdid"].ToString();
                if (!string.IsNullOrEmpty(dtValue)) dtbl = opr.GetAsDataTable("SampleTask", string.Format("sdid = '{0}'", dtValue), out dtValue);
                bool isSampleid = dtbl != null && dtbl.Rows.Count > 0 ? true : false;
                model.sampleNo = model.sampleNo.Length == 0 && isSampleid ? dtbl.Rows[0]["sampingNO"].ToString() : model.sampleNo;
                //样品编号(对应抽样单明细中的样品编号，当sampingNO有值时必填)
                model.foodCode = isSampleid ? dtbl.Rows[0]["SampleNO"].ToString() : results.Rows[i]["SampleFoodCode"].ToString();
                //任务编号
                model.planCode = isSampleid ? dtbl.Rows[0]["planCode"].ToString() : results.Rows[i]["CheckPlanCode"].ToString();
                //经营户ID (关联被检单位子集的cdId)
                model.cdId = isSampleid ? dtbl.Rows[0]["cdId"].ToString() : "";
                //经营户名称
                model.cdName = isSampleid ? dtbl.Rows[0]["cdName"].ToString() : "";
                //检测单位（必填）(接口1的 pointId)
                model.checkPId = Global.samplenameadapter[0].pointId;
                //检测日期
                dtValue = results.Rows[i]["CheckStartDate"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                model.checkDate = dtValue;
                //检测依据
                model.checkAccord = results.Rows[i]["Standard"].ToString();
                //检测项目
                dtValue = results.Rows[i]["CheckTotalItem"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "未知项目";
                }
                model.checkItemName = dtValue;
                //检测仪器唯一编码
                model.checkDevice = Global.DeviceID;
                //被检对象名称
                dtValue = isSampleid ? dtbl.Rows[0]["regName"].ToString() : results.Rows[i]["CheckedCompany"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "默认被检单位";
                }
                model.ckcName = dtValue;
                //监管对象ID(关联被检单位的regId)
                dtValue = isSampleid ? dtbl.Rows[0]["regId"].ToString() : "";
                model.ckcCode = isSampleid ? dtbl.Rows[0]["ckoCode"].ToString() : "";
                if (!string.IsNullOrEmpty(dtValue))
                {
                    model.regId = dtValue;
                }
                else
                {
                    dtbl = opr.GetAsDataTable("kjcCompany", string.Format("regName = '{0}'", model.ckcName), out dtValue);
                    //如果查询被检单位没有的话，则是经营户信息
                    if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                    {
                        model.regId = dtbl.Rows[0]["regId"].ToString();
                        //所属行政机构编号（必填）（被检单位的organizationCode）
                        model.ckcCode = dtbl.Rows[0]["organizationCode"].ToString();
                    }
                    else
                    {
                        dtbl = opr.GetAsDataTable("kjcCompanyDeteails", string.Format("cdName = '{0}'", model.ckcName), out dtValue);
                        if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                        {
                            //经营户ID (关联被检单位子集的cdId)
                            model.cdId = dtbl.Rows[0]["cdId"].ToString();
                            //经营户名称
                            model.cdName = dtbl.Rows[0]["cdName"].ToString();
                            //通过经营户信息查询被检单位
                            dtbl = opr.GetAsDataTable("kjcCompany", string.Format("regId = '{0}'", dtbl.Rows[0]["regId"].ToString()), out dtValue);
                            if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                            {
                                model.ckcName = dtbl.Rows[0]["regName"].ToString();
                                model.regId = dtbl.Rows[0]["regId"].ToString();
                                //所属行政机构编号（必填）（被检单位的organizationCode）
                                model.ckcCode = dtbl.Rows[0]["organizationCode"].ToString();
                            }
                        }
                    }
                }
                //检测值单位
                dtValue = results.Rows[i]["ResultInfo"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "";
                }
                model.checkUnit = dtValue;
                //检测结果值(12%)（必填）
                dtValue = results.Rows[i]["CheckValueInfo"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "0";
                }
                deviceModel = deviceModel.Length == 0 ? results.Rows[i]["CheckMachineModel"].ToString() : deviceModel;
                model.checkResult = dtValue;
                //限定值
                dtValue = results.Rows[i]["StandValue"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "0";
                }
                model.limitValue = dtValue;
                //检测结论 （合格、不合格）
                dtValue = results.Rows[i]["Result"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "不合格";
                }
                model.checkConclusion = dtValue;
                //检测数据状态 0未审核、1已审核
                model.dataStatus = 1;
                //数据来源 数据来源0检测工作站，1监管通app，2.仪器上传，3平台上传，4黑金刚上传
                model.dataSource = 2;
                //检测人
                model.checkUser = LoginWindow._userAccount.UserName;
                //数据上传人
                model.dataUploadUser = LoginWindow._userAccount.UserName;

                upDatas.result.Add(model);
            }
            return rtn;
        }

        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();

        /// <summary>
        /// 上传至昆山 - 苏州平台
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private string UploadResult(DataTable results)
        {
            string Info = string.Empty;
            if (Global.KsUser.Length == 0 || Global.KsPwd.Length == 0 ||
                Global.KsMarketCode.Length == 0 || Global.KsMarketName.Length == 0)
            {
                MessageBox.Show("检测到数据上传所需的信息不完整，请前往[设置]界面进行设置！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Info = "数据上传所需的信息不完整";
                return Info;
            }

            if (Global.KsTokenNo.Length == 0)
            {
                if (!KunShanHelper.GetToken(out Info))
                {
                    MessageBox.Show("令牌获取失败，请核对服务器链接信息是否正确！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    Info = "令牌获取失败";
                    return Info;
                }
            }

            tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
            string rtn = string.Empty, upStr = string.Empty;
            string sysCodes = string.Empty, errMsg = string.Empty;
            for (int i = 0; i < results.Rows.Count; i++)
            {
                sysCodes += sysCodes.Length == 0 ?
                    string.Format("'{0}'", results.Rows[i]["SysCode"].ToString()) :
                    string.Format(",'{0}'", results.Rows[i]["SysCode"].ToString());
            }
            if (sysCodes.Length > 0)
            {
                sysCodes = string.Format("({0})", sysCodes);
                results = _bll.GetDtblByCodes(sysCodes, out errMsg);
                if (errMsg.Length == 0 && results != null && results.Rows.Count > 0)
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        errMsg = string.Empty;
                        try
                        {
                            KunShanEntity.UploadRequest.webService webService = new KunShanEntity.UploadRequest.webService();
                            KunShanEntity.UploadRequest.Head head = new KunShanEntity.UploadRequest.Head();
                            head.marketCode = Global.KsVersion.Equals("2") ? results.Rows[i]["CheckedCompanyCode"].ToString() : Global.KsMarketCode;//接入单位编号
                            
                            //暂时不开放给用户
                            ////通过单位编号重新在数据库中获取一次实时的单位数据信息
                            //DataTable dtblB = opr.GetAsDataTable("Ks_Business", string.Format(" ID = '{0}'", head.marketCode), out errMsg);
                            //if (dtblB != null && dtblB.Rows.Count > 0)
                            //{
                            //    results.Rows[i]["DABH"] = dtblB.Rows[0]["IdentityCard"].ToString();
                            //    results.Rows[i]["PositionNo"] = dtblB.Rows[0]["TWNum"].ToString();
                            //    results.Rows[i]["DABHName"] = dtblB.Rows[0]["TWNume"].ToString();
                            //    if (dtblB != null && dtblB.Rows.Count > 0)
                            //    {
                            //        results.Rows[i]["CheckedCompany"] = dtblB.Rows[0]["MarketName"].ToString();
                            //        results.Rows[i]["CheckedCompanyCode"] = dtblB.Rows[0]["LicenseNo"].ToString();
                            //        results.Rows[i]["MarketType"] = GetMarketType(dtblB.Rows[0]["MarketRef"].ToString());
                            //    }
                            //}
                            //else
                            //{
                            //    dtblB = opr.GetAsDataTable("ks_AreaMarket", string.Format(" LicenseNo = '{0}'", head.marketCode), out errMsg);
                            //    if (dtblB != null && dtblB.Rows.Count > 0)
                            //    {
                            //        results.Rows[i]["CheckedCompany"] = dtblB.Rows[0]["MarketName"].ToString();
                            //        results.Rows[i]["CheckedCompanyCode"] = dtblB.Rows[0]["LicenseNo"].ToString();
                            //        results.Rows[i]["MarketType"] = GetMarketType(dtblB.Rows[0]["MarketRef"].ToString());
                            //    }
                            //}
                            head.marketName = Global.KsVersion.Equals("2") ? results.Rows[i]["CheckedCompany"].ToString() : Global.KsMarketName;//接入单位名称
                            head.tokenNo = Global.KsTokenNo;
                            webService.head = head;
                            KunShanEntity.UploadRequest.Request request = new KunShanEntity.UploadRequest.Request();
                            KunShanEntity.UploadRequest.DataList dataList = new KunShanEntity.UploadRequest.DataList();
                            KunShanEntity.UploadRequest.QuickCheckItemJC quickCheckItemJC = new KunShanEntity.UploadRequest.QuickCheckItemJC();
                            string xml = string.Empty;

                            quickCheckItemJC.JCCode = results.Rows[i]["CheckNo"].ToString();//检测编号
                            quickCheckItemJC.MarketType = Global.KsVersion.Equals("2") ? results.Rows[i]["MarketType"].ToString() : Global.KsVersion;
                            quickCheckItemJC.DABH = !Global.KsVersion.Equals("1") ? results.Rows[i]["DABH"].ToString() : "";//经营户身份证号码
                            quickCheckItemJC.PositionNo = !Global.KsVersion.Equals("1") ? results.Rows[i]["PositionNo"].ToString() : "";//经营户摊位编号
                            quickCheckItemJC.Name = !Global.KsVersion.Equals("1") ? results.Rows[i]["DABHName"].ToString() : "";//经营户姓名
                            quickCheckItemJC.SubItemCode = results.Rows[i]["SubItemCode"].ToString();//抽检的品种编码
                            quickCheckItemJC.SubItemName = results.Rows[i]["SubItemName"].ToString();//抽检的品种名称
                            quickCheckItemJC.QuickCheckItemCode = results.Rows[i]["QuickCheckItemCode"].ToString();//抽检项目大类编号
                            quickCheckItemJC.QuickCheckSubItemCode = results.Rows[i]["QuickCheckSubItemCode"].ToString();//抽检项目小类编号
                            quickCheckItemJC.QuickCheckDate = results.Rows[i]["CheckStartDate"].ToString();
                            quickCheckItemJC.QuickCheckResult = results.Rows[i]["Result"].ToString().Equals("合格") || results.Rows[i]["Result"].ToString().Equals("阴性") ? "-" : "+";//定性结果
                            quickCheckItemJC.QuickCheckResultValue = results.Rows[i]["CheckValueInfo"].ToString();//定量结果值
                            quickCheckItemJC.QuickCheckResultValueUnit = results.Rows[i]["ResultInfo"].ToString();//单位
                            quickCheckItemJC.QuickCheckResultDependOn = results.Rows[i]["Standard"].ToString();//检测依据
                            quickCheckItemJC.QuickCheckResultDependOn = quickCheckItemJC.QuickCheckResultDependOn.Length == 0 ? "参考国标" : quickCheckItemJC.QuickCheckResultDependOn;
                            quickCheckItemJC.QuickCheckResultValueCKarea = results.Rows[i]["StandValue"].ToString();//参考范围
                            quickCheckItemJC.QuickCheckRemarks = "暂无备注说明";//备注
                            quickCheckItemJC.QuickChecker = LoginWindow._userAccount.UserName;//检测人
                            quickCheckItemJC.QuickReChecker = LoginWindow._userAccount.UserName;//复核人
                            quickCheckItemJC.QuickCheckUnitName = Global.KsMarketName;//检测机构
                            quickCheckItemJC.QuickCheckUnitId = Global.KsMarketCode;//检测机构编号
                            quickCheckItemJC.JCManufactor = "广州达元食品安全技术有限公司";//检测设备厂家
                            quickCheckItemJC.JCModel = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                            quickCheckItemJC.JCSN = Global.KsJCSN;//检测设备编号
                            quickCheckItemJC.ReviewIs = results.Rows[i]["IsUpload"].ToString().Equals("N") ? "0" : "1";
                            dataList.QuickCheckItemJC = quickCheckItemJC;
                            request.dataList = dataList;
                            webService.request = request;
                            string uploadXml = XmlHelper.EntityToXml<KunShanEntity.UploadRequest.webService>(webService);
                            FileUtils.UploadLog(string.Format("数据上传 - 请求：\r\n{0}", uploadXml));
                            //System.Console.WriteLine(string.Format("数据上传 - 请求：\r\n{0}", xml));
                            xml = KunShanHelper.Upload(uploadXml, out errMsg);
                            FileUtils.UploadLog(string.Format("数据上传 - 响应：\r\n{0}", xml));
                            //System.Console.WriteLine(string.Format("数据上传 - 响应：\r\n{0}", xml));

                            if (xml.Equals("1000"))
                            {
                                sysCodes = string.Format("('{0}')", results.Rows[i]["SysCode"].ToString());
                                _resultTable.UpdateUpload(sysCodes, out errMsg);
                                Global.UploadSCount++;
                            }
                            else
                            {
                                if (errMsg.Length > 0)
                                {
                                    rtn += getKsInfo(errMsg);
                                }
                                else
                                {
                                    KunShanEntity.UploadResponse.webService uploadResponse = XmlHelper.XmlToEntity<KunShanEntity.UploadResponse.webService>(xml);
                                    if (uploadResponse != null)
                                    {
                                        //如果编号重复，则重新生成编码后再上传一次
                                        bool isUploadXml = false;
                                        if ("-1001|该编码已存在！".Equals(uploadResponse.response.errorList.error))
                                        {
                                            for (int z = 0; z < 3; z++)
                                            {
                                                quickCheckItemJC.JCCode = DateTime.Now.ToString("yyyyMMddHHmmssfff") + DateTime.Now.ToString("fff") + Global.GetRandom();
                                                dataList.QuickCheckItemJC = quickCheckItemJC;
                                                request.dataList = dataList;
                                                webService.request = request;
                                                uploadXml = XmlHelper.EntityToXml<KunShanEntity.UploadRequest.webService>(webService);
                                                isUploadXml = UploadXml(uploadXml);
                                                if (isUploadXml)
                                                {
                                                    sysCodes = string.Format("('{0}')", results.Rows[i]["SysCode"].ToString());
                                                    _resultTable.UpdateUpload(sysCodes, out errMsg);
                                                    Global.UploadSCount++;
                                                    break;
                                                }
                                            }
                                        }
                                        else if ("-1001|单位编码或名称不对".Equals(uploadResponse.response.errorList.error))
                                        {
                                            //重新获取主体信息
                                            KunShanHelper.GetMarket(out errMsg);
                                            KunShanEntity.QueryMarketResponse.Response market = null;
                                            try
                                            {
                                                market = JsonHelper.JsonToEntity<KunShanEntity.QueryMarketResponse.Response>(errMsg.Replace("[", "").Replace("]", ""));
                                            }
                                            catch (Exception) { }
                                            if (market != null)
                                            {
                                                Global.KsMarketName = market.MarketName;
                                                Global.KsMarketCode = market.LicenseNo;
                                                CFGUtils.SaveConfig("KsMarketName", Global.KsMarketName);
                                                CFGUtils.SaveConfig("KsMarketCode", Global.KsMarketCode);
                                                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);

                                                head.marketCode = Global.KsVersion.Equals("2") ? results.Rows[i]["CheckedCompanyCode"].ToString() : Global.KsMarketCode;//接入单位编号
                                                head.marketName = Global.KsVersion.Equals("2") ? results.Rows[i]["CheckedCompany"].ToString() : Global.KsMarketName;//接入单位名称
                                                dataList.QuickCheckItemJC.MarketType = Global.KsVersion.Equals("2") ? results.Rows[i]["MarketType"].ToString() : Global.KsVersion;
                                                request.dataList = dataList;
                                                webService.head = head;
                                                webService.request = request;
                                                uploadXml = XmlHelper.EntityToXml<KunShanEntity.UploadRequest.webService>(webService);
                                                isUploadXml = UploadXml(uploadXml);
                                                if (isUploadXml)
                                                {
                                                    sysCodes = string.Format("('{0}')", results.Rows[i]["SysCode"].ToString());
                                                    _resultTable.UpdateUpload(sysCodes, out errMsg);
                                                    Global.UploadSCount++;
                                                }
                                            }
                                        }
                                        if (!isUploadXml)
                                        {
                                            rtn += rtn.Length == 0 ? string.Format("样品：{0}[{1}]；", results.Rows[i]["FoodName"].ToString(), uploadResponse.response.errorList.error) :
                                            string.Format("\r\n样品：{0}[{1}]；", results.Rows[i]["FoodName"].ToString(), uploadResponse.response.errorList.error);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            FileUtils.UploadLog(string.Format("数据上传 - 异常：\r\n{0}", ex.ToString()));
                            rtn += rtn.Length == 0 ? string.Format("样品：{0}[{1}]；", results.Rows[i]["FoodName"].ToString(), ex.Message) :
                                        string.Format("\r\n样品：{0}[{1}]；", results.Rows[i]["FoodName"].ToString(), ex.Message);
                            continue;
                        }
                    }
                }
                else
                {
                    return "暂时没有需要上传的数据";
                }
            }
            else
            {
                return "暂时没有需要上传的数据";
            }

            return (Global.UploadSCount > 0 && Global.UploadFCount == 0) ? string.Empty : rtn;
        }

        private bool UploadXml(string xml)
        {
            string errMsg = string.Empty;
            xml = KunShanHelper.Upload(xml, out errMsg);
            FileUtils.UploadLog(string.Format("[重复提交]数据上传 - 响应：\r\n{0}", xml));
            if (xml.Equals("1000"))
            {
                return true;
            }
            return false;
        }

        private string GetMarketType(string name)
        {
            switch (name)
            {
                case "批发市场":
                    return "1";

                case "农贸市场":
                    return "2";

                case "检测机构":
                    return "3";

                case "餐饮单位":
                    return "4";

                case "食品生产企业":
                    return "5";

                case "商场超市":
                    return "6";

                case "个体工商户":
                    return "7";

                case "食材配送企业":
                    return "8";

                case "单位食堂":
                    return "9";

                case "集体用餐配送和中央厨房":
                    return "10";

                case "农产品基地":
                    return "11";

                default:
                    return name;
            }
        }

        private string getKsInfo(string errMsg)
        {
            string info = string.Empty;
            switch (errMsg)
            {
                case "-1001|该检测设备编码未注册，请厂商联系我们公司。":
                    info = "检测设备编码未注册，请联系管理员！";
                    break;
                case "-1001|该编码已存在！":
                    info = "该数据已上传到平台，不支持重复上传。";
                    break;
                case "-1001|请求失败！":
                    info = "令牌获取失败！\r\n\r\nTips：请尝试进入[设置]界面进行[通讯测试]！";
                    break;
                case "-1001|用户名密码不正确":
                    info = "用户名或密码不正确！\r\n\r\nTips1：请核对用户名和密码是否填写正确！\r\nTips2：如果该账号可以正常登录平台，却通讯失败，可以尝试联系亿通技术员协助处理！";
                    break;
                case "-1001|单位编码或名称不对":
                    info = "上传时提交的单位编码或名称与平台登记信息不匹配！\r\n\r\nTips：请联系亿通公司技术员协助处理！";
                    break;
                case "-1001|令牌号已过期，请重新获取":
                    Global.KsTokenNo = string.Empty;
                    info = "令牌已过期，需要重新获取！\r\n\r\nTips1：请稍后重新上传！\r\nTips2：可以尝试进入[设置]界面进行[通讯测试]提示成功后，然后重新上传！";
                    break;
                case "-1001|经营者摊位号或者身份证号不能同时为空！":
                    info = "经营户签约已到期！\r\n\r\nTips：请登录[苏州监管平台]重新签约！";
                    break;
                case "-1001|抽检的品种编码不正确!":
                    info = "检测品种编码不存在！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|抽检项目大类编号不正确!":
                    info = "抽检项目大类编号不正确！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|抽检项目小类编号不正确!":
                    info = "抽检项目小类编号不正确！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|经营者摊位号或身份证号不对！多个摊位号只需要上传一个即可！":
                    info = "经营户签约过期，建议前往平台续签后再尝试上传。";
                    break;
                default:
                    info = errMsg;
                    break;
            }
            return info;
        }

        /// <summary>
        /// 上传至食安科技监管平台
        /// </summary>
        /// <param name="server">服务器相关</param>
        /// <param name="results">上传的数据</param>
        /// <returns>结果</returns>
        private string UploadResult(CheckPointInfo server, DataTable results)
        {
            //强制执行数据完善操作
            //bool IsMandatory = true;

            string rtn = string.Empty, upStr = string.Empty;
            try
            {
                int dataLen = results.Rows.Count, pageSize = 10, pageCount = Global.PageCount(dataLen, pageSize);
                //分页索引从1开始
                DataTable upDtbl = null;
                DataSet dst = null;
                tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
                for (int i = 1; i <= pageCount; i++)
                {
                    upDtbl = Global.GetPagedTable(results, i, pageSize);
                    #region 验证必填
                    string dtValue = string.Empty;
                    for (int j = 0; j < upDtbl.Rows.Count; j++)
                    {
                        try
                        {
                            //记录编号
                            dtValue = upDtbl.Rows[j]["SysCode"].ToString();
                            if (dtValue == null || dtValue.Length == 0 || !Global.IsGuidByReg(dtValue))
                            {
                                upDtbl.Rows[j]["SysCode"] = Global.GETGUID(null, 1);
                                _bll.InsertSysCode(upDtbl.Rows[j]["SysCode"].ToString(), int.Parse(upDtbl.Rows[j]["ID"].ToString()), out dtValue);
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
                            ////检测单位所属行政机构名称
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckPlace"] = Global.samplenameadapter[0].Organization;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckPlace"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckPlace"] = Global.samplenameadapter[0].Organization;
                            //    }
                            //}

                            ////检测单位所属行政机构编号
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckPlaceCode"] = Global.samplenameadapter[0].CheckPlaceCode;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckPlaceCode"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckPlaceCode"] = Global.samplenameadapter[0].CheckPlaceCode;
                            //    }
                            //}
                            ////检测单位名称
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckUnitName"] = Global.samplenameadapter[0].CheckPointName;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckUnitName"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckUnitName"] = Global.samplenameadapter[0].CheckPointName;
                            //    }
                            //}
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["APRACategory"] = Global.samplenameadapter[0].CheckPointType;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["APRACategory"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["APRACategory"] = Global.samplenameadapter[0].CheckPointType;
                            //    }
                            //}
                            upDtbl.Rows[j]["CKCKNAMEUSID"] = Global.samplenameadapter[0].user;
                            //基层上传人 2017年2月27日修改为上传“设置”界面的用户名
                            //dtValue = upDtbl.Rows[j]["UpLoader"].ToString();
                            //if (dtValue == null || dtValue.Length == 0)
                            //{
                            upDtbl.Rows[j]["UpLoader"] = Global.samplenameadapter[0].user;
                            //}
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
                    string str = dst.GetXml();
                    rtn = Global.Upload(str, server.user, server.pwd, server.pointNum, server.url);
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
                            }
                            if (where.Length > 0)
                            {
                                where = string.Format("({0})", where);
                                _resultTable.UpdateUpload(where, out error);
                                if (error.Length == 0) Global.UploadSCount++;
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
                                    }
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out error);
                                    if (error.Length == 0) Global.UploadSCount++;
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
                                    }
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out error);
                                    if (error.Length == 0) Global.UploadSCount++;
                                }
                            }
                        }
                        //失败的部分
                        if (ResultInfo[2].Length > 0)
                        {
                            rtn = rtnDtbl.Rows[0]["ResultDesc"].ToString();
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
                        rtn = rtnDtbl.Rows[0]["ResultDesc"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return (Global.UploadSCount > 0 && Global.UploadFCount == 0) ? string.Empty : rtn;
        }

        /// <summary>
        /// 新方法 验证连接地址，用户和密码的正确性
        /// 2016年12月22日 wenj update
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码,需要MD5加密</param>
        /// <returns>json</returns>
        public String checkUserConnection(string url, String user, String password)
        {
            string rtn = string.Empty;
            NewInterface.dataSync ds = new NewInterface.dataSync();
            ds.Url = url;
            rtn = ds.checkUserConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString());
            return rtn;
        }

        /// <summary>
        /// 全部数据下载
        /// 2016年12月22日 wenj update 新版本接口
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber">检测点编号</param>
        /// <param name="checkName">检测点名称</param>
        /// <param name="checkType">检测点类型</param>
        /// <param name="checkOrg">所属行政机构</param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns>结果</returns>
        private string DownLoadAllData(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd, string downType)
        {
            string UDate = string.Empty, rtn = string.Empty, sign = string.Empty,
                version = Global.Version.Equals("XZ") ? "行政版" : "企业版", outErr = string.Empty;
            DataTable dtUDate = null;
            try
            {
                //检测任务下载
                sign = "CheckPlan";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    //检测任务需要实时更新，每次下载时先清空数据库
                    _clsCompanyOpr.Delete(string.Empty, out outErr, "tTask");
                    UDate = string.Empty;
                    _CheckItemStandard = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //被检单位下载
                sign = "Company";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    //被检单位需每次重新下载，无需增量下载，且每次都需要将数据清空
                    _clsCompanyOpr.Delete(string.Empty, out outErr, "TCOMPANY");
                    UDate = string.Empty;
                    _DownCompany = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                    //FileUtils.ErrorLog("SettingWindow", "DownLoadDatas", _DownCompany);
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

                return "true";
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("SettingWindow", "DownLoadDatas", ex.ToString());
                return ex.Message;
            }
        }

        /// <summary>
        /// 检测标准下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string DownStandard(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 4);
                    if (dtSample != null && dtSample.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtSample, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _Standard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "Standard", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
            return rtn;
        }

        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private string DownCheckItem(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 3);
                    if (dtSample != null && dtSample.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtSample, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItems = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "CheckItem", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
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
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    _CheckItemSecondDisplay = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", username, pwd, "Instrumen,DY3000", string.Empty);
                }
                return rtn;
            }
            catch (Exception)
            {
                return "false";
            }
        }

        /// <summary>
        /// 被检单位下载
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="UDate">更新时间</param>
        /// <returns></returns>
        private string DownCopany(string url, string checkNumber, string checkName, string checkType, string checkOrg, string username, string pwd)
        {
            string rtn = string.Empty, UDate = string.Empty, sign = "Company";
            DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 7);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
                UDate = listCompany[0].UDate;
            }
            rtn = Global.GetXmlByService(Global.Version.Equals("XZ") ? "行政版" : "企业版", url, username, pwd, sign, UDate);
            return rtn;
            #region 旧版本接口
            //string UDate = string.Empty, rtn = string.Empty;
            //try
            //{
            //    FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
            //    ws.Url = url;
            //    rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
            //    if ("true".Equals(rtn))
            //    {
            //        DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 7);
            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
            //            UDate = listCompany[0].UDate;
            //        }
            //        _DownCompany = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", username, pwd, "Company", UDate);
            //    }
            //    return rtn;
            //}
            //catch (Exception)
            //{
            //    return "false";
            //}
            #endregion
        }

        /// <summary>
        /// 任务下载
        /// </summary>
        /// <param name="server"></param>
        /// <returns>结果</returns>
        private string DownLoadTask(CheckPointInfo server)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = server.url;
                rtn = ws.CheckChePoint(server.pointNum, server.pointName, server.pointType, server.orgName, server.user, FormsAuthentication.HashPasswordForStoringInConfigFile(server.pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    //string data_Task = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", server.CheckPlaceCode, server.CheckPointID, server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5").ToString(), "CheckPlan");
                    DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 12);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
                        UDate = listCompany[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItemStandard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", server.user, server.pwd, "CheckPlan", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
            return rtn;
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

    }
}