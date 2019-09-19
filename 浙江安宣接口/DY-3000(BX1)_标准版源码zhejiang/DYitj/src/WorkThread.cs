using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows;
using AIO.AnHui;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel.Wisdom;
using Newtonsoft.Json;
using DYSeriesDataSet.DataSentence;

namespace AIO
{
    public class WorkThread : ChildThread, IDisposable
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
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
        private agreement _agreement = new agreement();
        private int _timeout = 0;
        private int _length = 0;

        protected override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            switch (msg.what)
            {
                //打印机通讯测试
                case MsgCode.MSG_COMP_TEST:
                    msg.result = false;
                    if (_port.NewOpen(msg.str1))
                    {
                        msg.result = true;
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;
                case MsgCode.MSG_COMM_TEST:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (ADTest(_port, 3))
                        {
                            msg.result = true;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;
                //校准
                case MsgCode.MSG_COMM_CABT:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (CalibrationAD(_port, 3, msg.calibrationValue))
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
                //新的读取AD值方法 2016年2月28日 wenj
                case MsgCode.MSG_READ_AD_CYCLE:
                    if (_port.Open(msg.str1))
                    {
                        if (ADTest(_port, 3))
                        {
                            while (bWhileReadADCycle)
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
                                //一秒钟读取一次数据,清零维持50毫秒一次
                                DateUtils.WaitMs(Global.IsClear ? 50 : 1000);
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

                //金标卡进出卡通讯测试
                case MsgCode.MSG_JBK_TEST:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] dataOut = jbkOUT(_port);
                        if (dataOut != null)
                        {
                            DateUtils.WaitMs(1500);
                            byte[] dataIn = jbkIN(_port, false);
                            if (dataIn != null)
                            {
                                DateUtils.WaitMs(1500);
                                msg.result = true;
                            }
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //2016年10月13日 新增校准前判定是否有卡
                case MsgCode.MSG_JBK_CKC:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        msg.result = jbkCheckCard(_port);
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //金标卡校准
                case MsgCode.MSG_JBK_CBT:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] dataOut = jbkCBT(_port);
                        if (dataOut != null)
                        {
                            msg.result = true;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //金标卡 出卡
                case MsgCode.MSG_JBK_OUT:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = jbkOUT(_port);
                        if (null != data)
                        {
                            msg.result = true;
                            _port.Close();
                        }
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    else
                    {
                        _port.Close();
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    break;

                //金标卡 进卡
                case MsgCode.MSG_JBK_IN:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = jbkIN(_port, false);
                        if (null != data)
                        {
                            msg.result = true;
                            _port.Close();
                        }
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    break;

                //金标卡 进卡and测试
                case MsgCode.MSG_JBK_InAndTest:
                    msg.result = false;
                    msg.datas = new List<byte[]>();
                    for (int i = 0; i < msg.ports.Count; i++)
                    {
                        if (msg.ports[i] != null && _port.Open(msg.ports[i]))
                        {
                            //同时进卡并测试
                            byte[] rtnBt = jbkIN(_port, true);
                            _port.Close();
                        }
                    }

                    DateUtils.WaitMs(2500);
                    //进卡后一直读取数据
                    for (int i = 0; i < msg.ports.Count; i++)
                    {
                        if (msg.ports[i] != null && _port.Open(msg.ports[i]))
                        {
                            #region
                            _timeout = 200;
                            _length = 256;
                            int index = 0;
                            byte[] receiveData = new byte[_length];
                            byte[] dataList = new byte[_length];
                            //数据请求
                            _agreement.cmd = 0x15;
                            _agreement.len[1] = 0x00;
                            byte[] btData = _agreement.getAgreement();
                            //返回数据length个字节
                            receiveData = new byte[_length];
                            //下位机返回206个字节
                            bool isBreak = false;
                            System.Console.WriteLine(string.Format("------------------------------------------{0}开始读取数据------------------------------------------", msg.ports[i]));
                            while (true)
                            {
                                DateUtils.WaitMs(100);
                                receiveData = new byte[_length];
                                _port.Clear();
                                _port.Write(btData, 0, btData.Length);
                                if (_port.Read(receiveData, 0, _length, _timeout) == 0) break;

                                System.Console.WriteLine("");
                                for (int x = 0; x < _length; x++)
                                {
                                    System.Console.Write(receiveData[x] + " ");
                                }


                                int len = 0;
                                bool isOK = false;
                                for (int j = 0; j < receiveData.Length; j++)
                                {
                                    //头和命令编号相符时开始记录crc
                                    if (receiveData[j] == 0x7e && receiveData[j + 1] == 0x16 && !isOK)
                                    {
                                        len = (receiveData[j + 2] * 256) + receiveData[j + 3];
                                        if (len == 0)
                                        {
                                            //当data长度为0时数据接收完毕
                                            isBreak = true;
                                            break;
                                        }
                                        if (receiveData[j + len + 5] == 0x7e)
                                        {
                                            isOK = true;
                                            j += 4;
                                        }
                                    }

                                    if (len > 0)
                                    {
                                        dataList[index] = receiveData[j];
                                        index++;
                                        len--;
                                        if (len <= 0) break;
                                    }
                                }

                                if (isBreak)
                                    break;
                            }
                            System.Console.WriteLine("");
                            System.Console.WriteLine(string.Format("------------------------------------------{0}读取结束------------------------------------------", msg.ports[i]));

                            if (index > 0)
                            {
                                byte[] rtnData = new byte[index];
                                for (int j = 0; j < rtnData.Length; j++)
                                {
                                    rtnData[j] = dataList[j];
                                }
                                msg.datas.Add(rtnData);
                            }
                            _port.Close();
                            msg.result = true;
                            #endregion
                        }
                        else
                        {
                            msg.datas.Add(null);
                        }
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //获取版本号
                case MsgCode.MSG_GET_VERSION:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = jbkInAndTest(_port);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                            if (null != this.target)
                                target.SendMessage(msg, null);
                            _port.Close();
                        }
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    break;

                // 设置TC线没有返回，这个最好是放在读取摄像头之前发送。
                case MsgCode.MSG_SET_TCLINE:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
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
                    if (_port.NewOpen(msg.str1))
                    {
                        _port.Write(msg.data, msg.arg1, msg.arg2);
                        msg.result = true;
                        _port.Close();
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //2017年8月14日 浙江安宣仙居接口
                case MsgCode.MSG_CHECK_CONNECTION:
                    msg.result = false;
                    try
                    {
                        //浙江数据上传
                        string url =Global.geturl(msg.str1,1), json = "username=" + msg.str2 + "&password=" + msg.str3;
                        string postRecdata = Global.HttpMath("POST", url, json);
                        JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                        Global.ToJsonLOG list = js.Deserialize<Global.ToJsonLOG>(postRecdata);    //将json数据转化为对象类型并赋值给list
                        string resultCode = list.resultCode;
                        string message = list.message;
                        Global.PostToken = list.token;
                        if (resultCode == "10000")
                        {
                            msg.result = true;
                        }
                        //string rtnXml = checkUserConnection(msg.str1, msg.str2, msg.str3);
                        //DataSet dst = new DataSet();
                        //if (rtnXml.Length > 0)
                        //{
                        //    using (StringReader sr = new StringReader(rtnXml))
                        //    {
                        //        dst.ReadXml(sr);
                        //    }
                        //}
                        //DataTable dtbl = dst.Tables["Result"];
                        //string result = Global.GetResultByCode(dtbl.Rows[0]["ResultCode"].ToString());
                        //if (result.Equals("1"))
                        //{
                        //    result = dtbl.Rows[0]["ResultInfo"].ToString();
                        //    string[] user = result.Split(',');
                        //    if (user != null)
                        //    {
                        //        //failure
                        //        Global.setPointNum = user[0];
                        //        Global.setPonitName = user[1];
                        //        Global.setPointType = user[2];
                        //        Global.setOrgNum = user[3];
                        //        Global.setOrgName = user[4];
                        //        Global.setUserUUID = user[5];
                        //        msg.result = true;
                        //    }
                        //}
                        //else
                        //{
                        //    msg.error = dtbl.Rows[0]["ResultDesc"].ToString();
                        //    FileUtils.OprLog(0, "checkUser-failure", msg.error);
                        //    Global.setPointNum = string.Empty; Global.setPonitName = string.Empty;
                        //    Global.setPointType = string.Empty; Global.setOrgNum = string.Empty; Global.setOrgName = string.Empty;
                        //    Global.setUserUUID = string.Empty;
                        //}
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "checkUser-error", ex.ToString());
                        msg.error = ex.Message;
                    }
                    finally
                    {
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }

                    #region 旧版本接口
                    //string checkStr = checkUserConnection(msg.str1, msg.str2, msg.str3);
                    //if (!checkStr.Equals(string.Empty))
                    //{
                    //    JObject json = (JObject)JsonConvert.DeserializeObject(checkStr);
                    //    string strResult = json["Result"][0]["ResultCode"].ToString();
                    //    if (strResult.Equals("1"))
                    //    {
                    //        Global.setPointNum = json["Result"][0]["PointNum"].ToString();
                    //        Global.setPonitName = json["Result"][0]["PonitName"].ToString();
                    //        Global.setPointType = json["Result"][0]["PointType"].ToString();
                    //        Global.setOrgNum = json["Result"][0]["OrgNum"].ToString();
                    //        Global.setOrgName = json["Result"][0]["OrgName"].ToString();
                    //        msg.result = true;
                    //    }
                    //    else
                    //    {
                    //        msg.error = json["Result"][0]["ResultDescripe"].ToString();
                    //        Global.setPointNum = string.Empty; Global.setPonitName = string.Empty;
                    //        Global.setPointType = string.Empty; Global.setOrgNum = string.Empty; Global.setOrgName = string.Empty;
                    //    }
                    //}
                    //else
                    //{
                    //    Global.setPointNum = string.Empty; Global.setPonitName = string.Empty;
                    //    Global.setPointType = string.Empty; Global.setOrgNum = string.Empty; Global.setOrgName = string.Empty;
                    //}
                    //if (null != this.target)
                    //    target.SendMessage(msg, null);
                    #endregion
                    break;

                //获取电池状态
                case MsgCode.MSG_GET_BATTERY:
                    msg.result = false;
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
                                    if (null != this.target)
                                        target.SendMessage(msg, null);
                                    _port.Close();
                                }
                            }
                            else
                            {
                                msg.batteryData.Add(null);
                            }
                        }
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    else
                    {
                        msg.batteryData = null;
                        if (null != this.target)
                            target.SendMessage(msg, null);
                        _port.Close();
                    }
                    break;
                //数据上传
                case MsgCode.MSG_UPLOAD:
                    string err = string.Empty;
                    msg.result = false;
                    Global.UploadSCount = 0;
                    //通信测试重新读取身份认证信息，以防漏读导致无法上传
                    string comurl=Global.geturl(msg.str1 ,1);
                    string comAyy="username=" + msg.str2 + "&password=" + msg.str3;
                    Global.CommunicationTest(comurl,comAyy);  

                    //浙江宣仙居县数据上传http通信post
                    string rtn = string.Empty;
                    string UrlA =Global.geturl(msg.str1,3) ;
                    for (int i = 0; i < msg.selectedRecords.Count; i++)
                    {
                        string xml = GetXMLstr(msg.selectedRecords, i);
                        if (xml == "参考国标")
                        {
                            continue;
                        }
                        if (xml == "无效")
                        {
                            continue;
                        }

                        string UpRec = Global.HttpXML("POST", UrlA, xml);
                        if (UpRec == "")
                        {
                            break;
                        }
                        JavaScriptSerializer Up = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                        Global.ToJsonLOG Uplist = Up.Deserialize<Global.ToJsonLOG>(UpRec);    //将json数据转化为对象类型并赋值给list
                        string UPresultCode = Uplist.resultCode;
                        string UPmessage = Uplist.message;
                        //Global.PostToken = Uplist.token;
                        if (UPresultCode == "10200")
                        {
                            _bll.UpdateResult("Y", Global.UpY, out err);
                            Global.UploadSCount = Global.UploadSCount + 1;
                        }
                        else
                        {
                            Global.UploadFCount = Global.UploadFCount + 1;
                        }
                    }
                    msg.result = true;
                    //if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Length == 0)
                    //{
                    //    msg.outError = UploadResult((CheckPointInfo)msg.obj1, (msg.table));
                    //}
                    //else if (Global.InterfaceType.Equals("AH"))
                    //{
                    //    msg.outError = UploadResultAH(msg.selectedRecords);
                    //}
                    //else if (Global.InterfaceType.Equals("ZH"))
                    //{
                    //    msg.outError = UploadResult(msg.selectedRecords);
                    //}

                    //msg.result = msg.outError.Length == 0 ? true : false;

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                //全部数据下载
                case MsgCode.MSG_CHECK_SYNC:
                    msg.result = false;
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
                        msg.error = ex.Message;
                        msg.result = false;
                    }
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    break;

                //被检单位下载
                case MsgCode.MSG_DownCompany:
                    msg.result = false;
                    try
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "Company")))
                            msg.result = true;
                        msg.DownLoadCompany = _DownCompany;
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(0, "downLoadCompany-error", ex.ToString());
                        msg.error = ex.Message;
                        msg.result = false;
                    }
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    break;
               //检测项目下载
                case MsgCode.MSG_DownCheckItems:
                    msg.result = false;
                    int s = 0;
                    _CheckItemSecondDisplay= Global.DownChkItem("GET",Global.geturl(msg.str1,2)); 
                
                    Json a = JsonConvert.DeserializeObject<Json>(_CheckItemSecondDisplay);
                    //Console.WriteLine(a.type);
                    //Console.WriteLine(a.resultCode);
                    //Console.WriteLine(a.message);
                    if (a.resultCode == "10100")//下载成功
                    {
                        try
                        {
                            clsDownChkItem downchk = new clsDownChkItem();
                            downchk.DelDownItem(out err);//删除原来的数据
                            foreach (Json.ItemDetail detail in a.datalist)
                            {
                                s = s + 1;
                                //string d = detail.itemcode;
                                //string f = detail.itemname;
                                //保存数据到本地数据库
                                downchk.InDownItem(s, detail.itemname, detail.itemcode, out err);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message,"Error");
                        }
                    }
                    //string url2 = msg.str1;
                    //string username2 = msg.str2;
                    //string pwd2 = msg.str3;
                    //string checkNumber2 = msg.args.Dequeue();
                    //string checkName2 = msg.args.Dequeue();
                    //string checkType2 = msg.args.Dequeue();
                    //string checkOrg2 = msg.args.Dequeue();
                    //if ("true".Equals(DownItems(url2, checkNumber2, checkName2, checkType2, checkOrg2, username2, pwd2)))
                    msg.result = true;
                    if (null != this.target)
                    {
                        msg.CheckItemsTempList = s.ToString();
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownTask:
                    msg.result = false;
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
                        msg.error = ex.Message;
                        FileUtils.OprLog(0, "downloadTask-error", ex.ToString());
                        msg.result = false;
                    }

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

                //获取国家局数据库数据
                case MsgCode.MSG_GETCOUNTRYDATA:
                    msg.result = false;
                    msg.Other = GetCountryData(msg.str1, msg.str2, msg.str3, msg.str4, msg.str5);
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
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

        private bool NewTest(ComPort comPort, int retry)
        {
            for (int i = 0; i <= retry; ++i)
            {
                if (NewTest(comPort))
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
        /// 通讯测试
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private bool NewTest(ComPort comPort)
        {
            _timeout = 1000;
            _length = 70;
            int red = 0;
            //下位机返回数据
            byte[] receiveData = new byte[_length];
            //临时储存数据
            byte[] storageData = new byte[_length];
            bool successful1 = false, successful2 = false;
            // 校验1号板 帧头0xAA 板号01 命令01 和校验 结束 0x0D 0x0A
            //cdm={0xAA,01,01,(01+01),0x0D,0x0A}
            byte[] btData = { 0x7E, 01, 01, 02, 0x0D, 0x7E };
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据70个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) >= 70)
            {
                for (int i = 0; i < receiveData.Length; i++)
                {
                    //帧头 Int 170 结束 Int 13 10
                    if ((i + 69) < receiveData.Length)
                    {
                        if (receiveData[i].ToString().Equals("170") && receiveData[(i + 68)].ToString().Equals("13") && receiveData[(i + 69)].ToString().Equals("10"))
                        {
                            if (receiveData.Length == _length)
                            {
                                storageData = receiveData;
                            }
                            else
                            {
                                for (int j = 0; j < 70; j++)
                                    storageData[j] = receiveData[(i + j)];
                            }

                            if (storageData[0].ToString().Equals("170") && storageData[(i + 68)].ToString().Equals("13") && storageData[(i + 69)].ToString().Equals("10"))
                            {
                                for (int k = 0; k < storageData.Length; k++)
                                {
                                    if (k > 0 && k < 67)
                                        red += storageData[k];
                                }
                                //和校验 坐标1-坐标66的和取余256=坐标67的值
                                successful1 = ((red % 256) == storageData[67]) ? true : false;
                                if (successful1)
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                successful1 = false;
            }

            // 校验2号板 帧头0xAA 板号 02 命令 01 和校验 结束 0x0D 0x0A
            //cdm={0xAA,02,01,(02+01),0x0D,0x0A}
            //receiveData = new byte[length];
            //storageData = new byte[length];
            //btData[1] = 02;
            //btData[3] = 03;
            //comPort.Clear();
            //comPort.Write(btData, 0, btData.Length);
            ////返回数据70个字节
            //if (comPort.Read(receiveData, 0, length, timeout) >= 70)
            //{
            //    for (int i = 0; i < receiveData.Length; i++)
            //    {
            //        //帧头 Int 170 结束 Int 13 10
            //        if ((i + 69) < receiveData.Length)
            //        {
            //            if (receiveData[i].ToString().Equals("170") && receiveData[(i + 68)].ToString().Equals("13") && receiveData[(i + 69)].ToString().Equals("10"))
            //            {
            //                if (receiveData.Length == length)
            //                {
            //                    storageData = receiveData;
            //                }
            //                else
            //                {
            //                    for (int j = 0; j < 70; j++)
            //                        storageData[j] = receiveData[(i + j)];
            //                }

            //                if (storageData[0].ToString().Equals("170") && storageData[(i + 68)].ToString().Equals("13") && storageData[(i + 69)].ToString().Equals("10"))
            //                {
            //                    red = 0;
            //                    for (int k = 0; k < storageData.Length; k++)
            //                    {
            //                        if (k > 0 && k < 67)
            //                            red += storageData[k];
            //                    }
            //                    //和校验 坐标1~坐标66的和取余256=坐标67的值
            //                    successful2 = ((red % 256) == storageData[67]) ? true : false;
            //                    if (successful1)
            //                        break;
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    successful2 = false;
            //}
            successful2 = successful1;
            return (successful1 && successful2) ? true : false;
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

        private bool Test(ComPort comPort)
        {
            _timeout = 2000;
            byte[] cmd = { 0x03, 0xFF, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0xD4, 0x18, 0x44, 0x45, 0x56, 0x49, 0x43, 0x45, 0x3F, 0x3F };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            // 接收2个字节的头
            int rec_len = 12;
            byte[] rec = new byte[rec_len];
            if (rec_len == comPort.Read(rec, 0, rec_len, _timeout))
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

        private byte[] fujian(ComPort _port)
        {
            _timeout = 1000;
            _length = 4096;
            int index = 0;
            byte[] receiveData = new byte[_length];
            byte[] dataList = new byte[_length];
            //数据请求
            _agreement.cmd = 0x15;
            _agreement.len[1] = 0x00;
            byte[] btData = _agreement.getAgreement();
            //返回数据length个字节
            receiveData = new byte[_length];
            //下位机返回206个字节
            bool isBreak = false;
            while (true)
            {
                //DateUtils.WaitMs(100);
                _port.Clear();
                _port.Write(btData, 0, btData.Length);
                _port.Read(receiveData, 0, _length, _timeout);

                int len = 0;
                bool isOK = false;
                for (int j = 0; j < receiveData.Length; j++)
                {
                    //头和命令编号相符时开始记录crc
                    if (receiveData[j] == 0x7e && receiveData[j + 1] == 0x16 && !isOK)
                    {
                        len = (receiveData[j + 2] * 256) + receiveData[j + 3];
                        if (len == 0)
                        {
                            //当data长度为0时数据接收完毕
                            isBreak = true;
                            break;
                        }
                        if (receiveData[j + len + 5] == 0x7e)
                        {
                            isOK = true;
                            j += 4;
                        }
                    }

                    if (len > 0)
                    {
                        dataList[index] = receiveData[j];
                        index++;
                        len--;
                    }
                }

                if (isBreak)
                    break;
            }

            if (index > 0)
            {
                byte[] rtnData = new byte[index];
                for (int j = 0; j < rtnData.Length; j++)
                {
                    rtnData[j] = dataList[j];
                }
                return rtnData;
            }
            _port.Close();
            return null;
        }

        /// <summary>
        /// 检测是否有卡
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="isTest"></param>
        /// <returns></returns>
        private bool jbkCheckCard(ComPort comPort)
        {
            _timeout = 1000;
            _length = 7;
            byte[] receiveData = new byte[_length];
            //验证是否有卡
            _agreement.cmd = 0x13;
            _agreement.len[1] = 0x00;
            byte[] btData = _agreement.getAgreement();
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据len个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData))
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
        private byte[] jbkCBT(ComPort comPort)
        {
            _timeout = 1000;
            _length = 6;
            byte[] receiveData = new byte[_length];
            _agreement.cmd = 0x17;//0x17金标卡校准
            _agreement.len[1] = 0x00;
            _agreement.data = 0x00;
            byte[] btData = _agreement.getAgreement();
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData))
                    return receiveData;
            }
            return null;
        }

        /// <summary>
        /// 金标卡出卡
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] jbkOUT(ComPort comPort)
        {
            _timeout = 1000;
            _length = 6;
            //下位机返回数据
            byte[] receiveData = new byte[_length];
            _agreement.cmd = 0x11;
            _agreement.data = 0x02; //0x02出卡
            _agreement.len[1] = 0x01;
            byte[] btData = _agreement.getAgreement();
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData))
                    return receiveData;
            }

            return null;
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
        /// 校验数据是否有效
        /// 2016年3月7日 wenj
        /// 7E 命令 Length 内容 CRC8 7E
        /// 0X01 请求版本信息|0X02 响应版本信息是否正确|0X03 进卡/出卡|0X04 响应是否进卡/出卡成功
        /// 0X05 验证是否有卡|0X06 响应是否有卡|0X07 请求数据|0X08 0X08
        /// </summary>
        /// <param name="receiveData">下位机返回的数据</param>
        /// <param name="btData">发送给下位机的数据</param>
        /// <returns></returns>
        private bool checkData(byte[] receiveData, byte[] btData)
        {
            int length = receiveData.Length;
            //0X01 请求验证版本信息
            if (btData[1] == 0X01)
            {
                //0X02 响应版本信息
                if (receiveData[1] == 0X02 && receiveData[0] == btData[0] && receiveData[4] == btData[5])
                {
                    //和校验
                    if ((receiveData[1] + receiveData[2] + receiveData[3]) == receiveData[4])
                    {
                        return true;
                    }
                }
            }
            else if (btData[1] == 0X03)//0X03 进卡01/出卡02
            {
                if (receiveData[1] == 0X04 && receiveData[0] == btData[0] && receiveData[5] == btData[5])
                {
                    if ((receiveData[1] + receiveData[2] + receiveData[3]) == receiveData[4])
                    {
                        return true;
                    }
                }
            }
            else if (btData[1] == 0X05)//0X05 验证是否有卡
            {
                if (receiveData[1] == 0X06 && receiveData[0] == btData[0] && receiveData[4] == btData[5])
                {
                    if ((receiveData[1] + receiveData[2] + receiveData[3]) == receiveData[4])
                    {
                        return true;
                    }
                }
            }
            else if (btData[1] == 0X07)//请求数据
            {
                if (receiveData[1] == 0X08 && receiveData[0] == btData[0] && receiveData[receiveData.Length - 1] == btData[5])
                {
                    //去掉首、尾、和校验三个byte
                    byte Sum = 0;
                    for (int i = 0; i < receiveData.Length; i++)
                    {
                        if (i == 0)
                            i++;
                        if (receiveData.Length - 2 > i)
                            break;
                        Sum += receiveData[i];
                    }
                    return (Sum == receiveData[receiveData.Length - 2]) ? true : false;
                }
            }

            return false;
        }

        /// <summary>
        /// 金标卡出卡-TEST
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] jbkOutTest(ComPort comPort)
        {
            _timeout = 2000;
            _length = 4;
            //下位机返回数据
            byte[] receiveData = new byte[_length];
            byte[] btData = { 0xAA, 02, 0x0D, 0x0A };
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
                return receiveData;
            else
                return null;
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
            //验证是否有卡
            _agreement.cmd = 0x13;
            _agreement.len[1] = 0x00;
            byte[] btData = _agreement.getAgreement();
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据len个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
            {
                if (ValidatedLegal(receiveData))
                {
                    _agreement.cmd = 0x11;
                    _agreement.data = (byte)((isTest && receiveData[4] == 0x01) ? 0x01 : 0x03);
                    _agreement.len[1] = 0x01;
                    byte[] inData = _agreement.getAgreement();
                    comPort.Clear();
                    comPort.Write(inData, 0, inData.Length);
                    receiveData = new byte[_length];
                    _length = 6;
                    receiveData = new byte[_length];
                    if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
                    {
                        if (ValidatedLegal(receiveData))
                            return receiveData;
                    }
                }
            }

            return null;
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
        /// 金标卡进卡and测试
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] jbkInAndTest(ComPort comPort)
        {
            //获取卡状态(有卡|无卡)
            byte[] rtnBt = jbkIN(comPort, true);
            if (rtnBt != null)
            {
                _timeout = 1000;
                _length = 4096;
                int index = 0;
                byte[] receiveData = new byte[_length];
                byte[] dataList = new byte[_length];
                //数据请求
                _agreement.cmd = 0x15;
                _agreement.len[1] = 0x00;
                byte[] btData = _agreement.getAgreement();
                //返回数据length个字节
                receiveData = new byte[_length];
                //下位机返回206个字节
                bool isBreak = false;
                DateUtils.WaitMs(2500);
                while (true)
                {
                    DateUtils.WaitMs(100);
                    comPort.Clear();
                    comPort.Write(btData, 0, btData.Length);
                    comPort.Read(receiveData, 0, _length, _timeout);

                    int len = 0;
                    bool isOK = false;
                    for (int i = 0; i < receiveData.Length; i++)
                    {
                        //头和命令编号相符时开始记录crc
                        if (receiveData[i] == 0x7e && receiveData[i + 1] == 0x16 && !isOK)
                        {
                            len = (receiveData[i + 2] * 256) + receiveData[i + 3];
                            if (len == 0)
                            {
                                //当data长度为0时数据接收完毕
                                isBreak = true;
                                break;
                            }
                            if (receiveData[i + len + 5] == 0x7e)
                            {
                                isOK = true;
                                i += 4;
                            }
                        }

                        if (len > 0)
                        {
                            dataList[index] = receiveData[i];
                            index++;
                            len--;
                        }
                    }

                    if (isBreak)
                        break;
                }

                if (index > 0)
                {
                    byte[] rtnData = new byte[index];
                    for (int i = 0; i < rtnData.Length; i++)
                    {
                        rtnData[i] = dataList[i];
                    }
                    return rtnData;
                }
            }

            return null;
        }

        private byte[] jbkInAndTests(ComPort comPort)
        {
            //获取卡状态(有卡|无卡)
            byte[] rtnBt = jbkIN(comPort, true);
            if (rtnBt != null)
            {
                _timeout = 1000;
                _length = 4096;
                int index = 0;
                byte[] receiveData = new byte[_length];
                byte[] dataList = new byte[_length];
                //数据请求
                _agreement.cmd = 0x15;
                _agreement.len[1] = 0x00;
                byte[] btData = _agreement.getAgreement();
                //返回数据length个字节
                receiveData = new byte[_length];
                //下位机返回206个字节
                bool isBreak = false;
                DateUtils.WaitMs(2500);
                while (true)
                {
                    DateUtils.WaitMs(100);
                    comPort.Clear();
                    comPort.Write(btData, 0, btData.Length);
                    comPort.Read(receiveData, 0, _length, _timeout);

                    int len = 0;
                    bool isOK = false;
                    for (int i = 0; i < receiveData.Length; i++)
                    {
                        //头和命令编号相符时开始记录crc
                        if (receiveData[i] == 0x7e && receiveData[i + 1] == 0x16 && !isOK)
                        {
                            len = (receiveData[i + 2] * 256) + receiveData[i + 3];
                            if (len == 0)
                            {
                                //当data长度为0时数据接收完毕
                                isBreak = true;
                                break;
                            }
                            if (receiveData[i + len + 5] == 0x7e)
                            {
                                isOK = true;
                                i += 4;
                            }
                        }

                        if (len > 0)
                        {
                            dataList[index] = receiveData[i];
                            index++;
                            len--;
                        }
                    }

                    if (isBreak)
                        break;
                }

                if (index > 0)
                {
                    byte[] rtnData = new byte[index];
                    for (int i = 0; i < rtnData.Length; i++)
                    {
                        rtnData[i] = dataList[i];
                    }
                    return rtnData;
                }
            }

            return null;
        }

        /// <summary>
        /// 金标卡进卡-TEST
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] jbkInTest(ComPort comPort)
        {
            _timeout = 2000;
            _length = 4;
            //下位机返回数据
            byte[] receiveData = new byte[_length];
            byte[] btData = { 0xAA, 01, 0x0D, 0x0A };
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //返回数据length个字节
            if (comPort.Read(receiveData, 0, _length, _timeout) == _length)
                return receiveData;
            else
                return null;
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        private byte[] getVersionNumber()
        {

            return Global.VersionNumber;
        }

        /// <summary>
        /// 读取AD值
        /// AD返回的格式是这样的
        /// 返回0x1B LED_ROW（多少排）
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] NewReadADValue(ComPort comPort)
        {
            // 1号板
            byte[] btData = { 0xAA, 01, 01, 02, 0x0D, 0x0A };
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            _timeout = 4000;
            // 接收2个字节的头
            int HDR_LEN = 70;
            //从下位机接收到的数据
            byte[] receiveData = new byte[HDR_LEN];
            //返回的数据，剔除6个校验相关的值，剩下的就是AD数据*2
            byte[] returnData = new byte[(HDR_LEN - 6) * 2];
            if (comPort.Read(receiveData, 0, HDR_LEN, _timeout) >= HDR_LEN)
            {
                for (int i = 0; i < receiveData.Length; i++)
                {
                    //帧头 Int 170 结束 Int 13 10
                    if ((i + 69) < receiveData.Length)
                    {
                        if (receiveData[i].ToString().Equals("170") && receiveData[(i + 68)].ToString().Equals("13") && receiveData[(i + 69)].ToString().Equals("10"))
                        {
                            if (receiveData.Length == HDR_LEN)
                            {
                                for (int k = 0; k < HDR_LEN; k++)
                                {
                                    if (k > 2 && k < 67)
                                    {
                                        returnData[k - 3] = receiveData[k];
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 70; j++)
                                    returnData[j - 3] = receiveData[(i + j)];
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            // 2号板
            btData[1] = 02;
            btData[3] = 03;
            comPort.Clear();
            comPort.Write(btData, 0, btData.Length);
            //从下位机接收到的数据
            receiveData = new byte[HDR_LEN];
            //返回的数据，剔除6个校验相关的值，剩下的就是AD数据*2
            if (comPort.Read(receiveData, 0, HDR_LEN, _timeout) >= HDR_LEN)
            {
                for (int i = 0; i < receiveData.Length; i++)
                {
                    //帧头 Int 170 结束 Int 13 10
                    if ((i + 69) < receiveData.Length)
                    {
                        if (receiveData[i].ToString().Equals("170") && receiveData[(i + 68)].ToString().Equals("13") && receiveData[(i + 69)].ToString().Equals("10"))
                        {
                            if (receiveData.Length == HDR_LEN)
                            {
                                for (int k = 0; k < HDR_LEN; k++)
                                {
                                    if (k > 2 && k < 67)
                                        returnData[64 + (k - 3)] = receiveData[k];
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 70; j++)
                                    returnData[64 + (j - 3)] = receiveData[(i + j)];
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            return returnData;
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

        private byte[] ReadCam(ComPort comPort, int camID)
        {
            //byte[] cmd = { 0x1b, 0x1c, (byte)camID };
            byte[] cmd = { 0x1b, 0x1c, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            _timeout = 2000;
            int HDR_LEN = 5;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, _timeout))
            {
                try
                {
                    if (0x1b != header[0])
                        return null;
                    long WIDTH = (UInt32)header[1] + (UInt32)(header[2] << 8);
                    long HEIGHT = (UInt32)header[3] + (UInt32)(header[4] << 8);
                    long BITDEPTH = 2;
                    int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);
                    byte[] buffer = new byte[HDR_LEN + DATA_LEN];
                    Array.Copy(header, buffer, HDR_LEN);
                    if (DATA_LEN == comPort.Read(buffer, HDR_LEN, DATA_LEN, 10000))
                    {
                        return buffer;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
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
            _timeout = 2000;
            int DATA_LEN = height;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, _timeout))
            {
                return buffer;
            }
            return null;
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
            _timeout = 2000;
            int DATA_LEN = 12;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, _timeout))
            {
                return buffer;
            }

            return null;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="server">网络路径</param>
        /// <param name="results">值</param>
        /// <returns>结果</returns>
        //private bool UpdateResult(CheckPointInfo server, DataTable results)
        //{
        //    FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
        //    ws.Url = server.ServerAddr;
        //    // 检查连接有效性
        //    try
        //    {
        //        if (!"true".Equals(ws.CheckConnection(server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5"))))
        //            return false;
        //        DataTable dtResult = results.Copy();
        //        DataSet dst = new DataSet("UpdateResult");
        //        dst.Tables.Add(dtResult.Copy());
        //        string xml=dst.GetXml();

        //        string ret = ws.SetDataDriver(xml, server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5"), server.CheckPointName, 3);
        //        int UploadCount = 0;
        //        int.TryParse(ret, out UploadCount);
        //        Global.UploadSCount = UploadCount;
        //        if (ret.IndexOf("errofInfo") >= 0)//存在错误
        //        {
        //            MessageBox.Show("上传数据失败!" + ret);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

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
            NewInterface.dataSync ds = new NewInterface.dataSync();
            ds.Url = url;
            rtn = ds.checkUserConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString());
            return rtn;
        }

        /// <summary>
        /// 旧方法 验证连接地址，用户和密码的正确性
        /// 旧验证方法 password需要MD5加密
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>true|false</returns>
        private string CheckConnection(string url, string user, string password)
        {
            string str = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                str = ws.CheckConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"));
            }
            catch (Exception)
            {
                return "false";
            }
            return str;
        }

        /// <summary>
        /// 全部数据下载
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns>结果</returns>
        private string CheckChePoint(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd)
        {
            string UDate = string.Empty, rtn = string.Empty;
            DataTable dtUDate = null;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    #region
                    //_CheckItemSecondDisplay = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", checkNumber.Substring(0, checkNumber.Length - 3), checkNumber, user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString(), "Instrumen,DY3000");
                    //_SampleStandardAgainDisplay = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", checkNumber.Substring(0, checkNumber.Length - 3), checkNumber, user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString(), "SELECTITME");
                    //_DownCompany = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", checkNumber.Substring(0, checkNumber.Length - 3), checkNumber, user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString(), "Company");
                    #endregion
                    //被检单位下载
                    dtUDate = _clsCompanyOpr.GetAsDataTable("", "", 7);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dtUDate, 1);
                        UDate = listCompany[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _DownCompany = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "Company", UDate);

                    //样品检测项目和对应检测标准数据下载
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable("", "", 2);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _SampleStandardAgainDisplay = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "SELECTITME", UDate);

                    //检测标准下载
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable("", "", 4);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsStandard> listSample = (List<clsStandard>)IListDataSet.DataTableToIList<clsStandard>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _Standard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "Standard", UDate);

                    //检测项目下载
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable("", "", 3);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItems = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "CheckItem", UDate);

                    //仪器检测项目下载
                    _CheckItemSecondDisplay = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "Instrumen,DY3000", "");
                    using (StringReader sr = new StringReader(_SampleStandardAgainDisplay))
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.ReadXml(sr);
                        List<FoodClass> foodClasses = (List<FoodClass>)IListDataSet.DataSetToIList<FoodClass>(dataSet, 0);
                    }
                }
                return rtn;
            }
            catch (Exception)
            {
                return "false";
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
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable("", "", 4);
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
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable("", "", 3);
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
                        rtn = Global.Upload(xml, server.RegisterID, server.RegisterPassword, server.CheckPointID, server.ServerAddr);
                        dst = new DataSet();
                        using (StringReader sr = new StringReader(rtn))
                        {
                            dst.ReadXml(sr);
                        }
                        string error = string.Empty;
                        DataTable rtnDtbl = dst.Tables["Result"];
                        string result = Global.GetResultByCode(rtnDtbl.Rows[0]["ResultCode"].ToString());
                        if (result.Equals("1"))
                        {
                            if (upDtbl != null && upDtbl.Rows.Count > 0)
                            {
                                for (int j = 0; j < upDtbl.Rows.Count; j++)
                                {
                                    _resultTable.UpdateUpload(upDtbl.Rows[j]["SysCode"].ToString(), out error);
                                    if (upDtbl.Rows[j]["CheckPlanCode"].ToString().Length > 0)
                                    {
                                        _resultTable.UpdateUploadTask(upDtbl.Rows[j]["CheckPlanCode"].ToString(), out error);
                                    }
                                    //更新任务状态
                                    if (error.Length == 0) Global.UploadSCount++;
                                }
                            }
                        }
                        else if (result.Equals("2"))
                        {
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
                                    for (int j = 0; j < updateResult.Length; j++)
                                    {
                                        if (addResult[j].Length > 0)
                                        {
                                            _resultTable.UpdateUpload(addResult[j], out error);
                                            if (error.Length == 0) Global.UploadSCount++;
                                        }
                                    }
                                }
                            }
                            //update成功的部分
                            if (ResultInfo[1].Length > 0)
                            {
                                updateResult = ResultInfo[1].Split(',');
                                if (updateResult.Length > 0)
                                {
                                    for (int j = 0; j < updateResult.Length; j++)
                                    {
                                        if (updateResult[j].Length > 0)
                                        {
                                            _resultTable.UpdateUpload(updateResult[j], out error);
                                            if (error.Length == 0) Global.UploadSCount++;
                                        }
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
                        FileUtils.OprLog(0, "upload-error", ex.ToString());
                        return ex.Message;
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
            Global.UploadSCount = 0;
            Global.UploadFCount = 0;
            string outErr = string.Empty;
            try
            {
                foreach (tlsTtResultSecond data in dataList)
                {
                    clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                    model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                    model.userName = Global.AnHuiInterface.userName;
                    model.instrument = Global.AnHuiInterface.instrument;
                    model.passWord = Global.AnHuiInterface.passWord;
                    model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                    model.mac = Global.AnHuiInterface.mac;

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
                        _resultTable.UpdateUpload(data.SysCode, out str);
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
                                _resultTable.UpdateUpload(selectedRecords[i].SysCode, out result);
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
                ws.Url = server.ServerAddr;
                rtn = ws.CheckChePoint(server.CheckPointID, server.CheckPointName, server.CheckPointType, server.Organization, server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    DataTable dt = _clsCompanyOpr.GetAsDataTable("", "", 12);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
                        UDate = listCompany[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItemStandard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", server.RegisterID, server.RegisterPassword, "CheckPlan", UDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        private class agreement
        {
            public byte head = 0x7e;
            public byte tail = 0x7e;
            public byte cmd = 0x00;
            public byte[] len = { 0x00, 0x01 };
            public byte data = 0x00;
            public byte crc = 0x00;

            /// <summary>
            /// 获取协议
            /// </summary>
            /// <returns></returns>
            public byte[] getAgreement()
            {
                //如果数据长度为0，则忽略data
                if (cmd == 0x12 || cmd == 0x13 || cmd == 0x15)
                {
                    len[0] = len[1] = 0x00;
                    crc = (byte)(cmd + len[0] + len[1]);
                }
                else
                {
                    crc = (byte)(cmd + len[0] + len[1] + data);
                }

                if (len[0] == 0x00 && len[1] == 0x00)
                {
                    byte[] bt = { head, cmd, len[0], len[1], crc, tail };
                    return bt;
                }
                else
                {
                    byte[] bt = { head, cmd, len[0], len[1], data, crc, tail };
                    return bt;
                }
            }
        }
        private static clsDownChkItem downItem = new clsDownChkItem();
        private static  StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 浙江数据上传
        /// 获取http协议接口的XML上传数据
        /// </summary>
        /// <returns></returns>
        public static string GetXMLstr(List<tlsTtResultSecond> selectedRecords, int i)
        {
            Global.UpY = selectedRecords[i].ID;
            //查询数据库获取项目编号
            string err = "";
            sb.Clear ();
            //sb.Append("itemName='");
            ////sb.Append("农药残留");
            //sb.Append(selectedRecords[i].CheckTotalItem);
            //sb.Append("'");
            //DataTable dt = downItem.GetDownItem(sb.ToString(), "", out err);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        selectedRecords[i].ItemCode = dt.Rows[0][2].ToString();
            //    }
            //    else//找不到检测项目，默认传农药残留
            //    {
            //        selectedRecords[i].ItemCode = "IT-004";
            //    } 
            //}
            //else 
            //{
            //    selectedRecords[i].ItemCode = "IT-004";
            //} 
            if (selectedRecords[i].CheckValueInfo == "参考国标")
            {
                return "参考国标";
            }
            if (selectedRecords[i].CheckValueInfo == "检测无效")
            {
                return "无效";
            }

            string chkvalue = selectedRecords[i].CheckValueInfo;
            if (chkvalue.Contains("%"))
            {
                chkvalue = chkvalue.Substring(0,chkvalue.Length -1);
            }

            string httpxml = "<?xml version='1.0' encoding='utf-8'?>"
                           + "<detectionData version='1.0'>"
                            + "<detectionRecords>"
                             + "<detectionRecord>"
                              + "<entitycode></entitycode>"                                                      //<!--（注册号或身份证号） -->暂时不用传
                              + "<booth>" + "A1" + "</booth>"                                                           //<!--摊位号 -->
                              + "<sampletime>" + (selectedRecords[i].TakeDate) + "</sampletime>"                   //<!--（抽样时间） -->
                              + "<detectiontime>" + (selectedRecords[i].CheckStartDate) + "</detectiontime>"        //<!--（检测时间） -->
                              + "<deviceno>" + (selectedRecords[i].CheckMachineModel) + "</deviceno>"               //<!--（检测设备内部编号） -->
                              + "<channelno>" + (selectedRecords[i].Hole) + "</channelno>"                         //<!--（检测通道编号） -->      
                              + "<prodcode>" + (selectedRecords[i].CheckedCompanyCode) + "</prodcode>"              //<!--（被检产品编号） -->
                              + "<prodamount>" + "1" + "</prodamount >"                                               //<!--（被检产品数量） -->
                              + "<useunit></useunit>"                                                             //<!--（数量单位） --> 
                              + "<detectItem>"
                              + "<itemcode>" + (selectedRecords[i].ItemCode) + "</itemcode>"                                          //<!--（检测指标编号） -->
                              + " <result>" + (selectedRecords[i].Result == "合格" ? "0" : "1") + "</result>"         //<!--（检测结果0=阴性合格，1=阳性不合格） -->
                              + "<extradata>" + (selectedRecords[i].StandValue) + "</extradata>"              //<!--（抑制率数据） -->
                              + "<data>" + chkvalue + "</data>"                        //<!--（数据） -->
                             + "</detectItem>"
                            + "</detectionRecord>"
                         + "</detectionRecords>"
                       + "</detectionData>";

            return httpxml;
        }

    }
}