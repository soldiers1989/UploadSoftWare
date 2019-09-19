using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows;
using com.lvrenyang;
using DYSeriesDataSet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataSentence;

namespace AIO
{
    public class WorkThread : ChildThread, IDisposable
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
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

                        byte[] value = ReadCTGrayValue(_port, msg.arg1, msg.arg2);
                        if (value != null)
                        {
                            msg.cValue = value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
                            msg.tValue = value[4] | (value[5] << 8) | (value[6] << 16) | (value[7] << 24); ;
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
                //浙江宣仙居县http通信
                case MsgCode.MSG_CHECK_CONNECTION:
                    msg.result = false;
                    try
                    {
                        //string url = msg.str1, json = "username=" + msg.str2 + "&password=" + msg.str3;
                        string url = Global.geturl(msg.str1, 1), json = "username=" + msg.str2 + "&password=" + msg.str3;
                        string postRecdata=Global.HttpMath("POST", url, json);       　　　　　　 
                        JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                        Global.ToJsonLOG list = js.Deserialize<Global.ToJsonLOG>(postRecdata);    //将json数据转化为对象类型并赋值给list
                        string resultCode = list.resultCode;
                        string message = list.message ;
                        Global.PostToken  = list.token ;
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
                        //    Global.setPointNum = string.Empty; Global.setPonitName = string.Empty;
                        //    Global.setPointType = string.Empty; Global.setOrgNum = string.Empty; Global.setOrgName = string.Empty;
                        //    Global.setUserUUID = string.Empty;
                        //}
                    }
                    catch (Exception ex)
                    {
                        msg.error = ex.Message;
                    }
                    finally
                    {
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    #region
                    //try
                    //{
                    //    JObject json = (JObject)JsonConvert.DeserializeObject(rtnXml);
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
                    //catch (Exception)
                    //{
                    //    msg.error = rtnXml;
                    //}
                    #endregion
                    break;

                case MsgCode.MSG_UPLOAD:      //数据上传

                    string err = string.Empty;
                    Global.UploadSCount = Global.UploadFCount = 0;
                    msg.result = false;
                    //通信测试重新读取身份认证信息，以防漏读导致无法上传
                    string comurl = Global.geturl(msg.str1, 1);
                    string comAyy = "username=" + msg.str2 + "&password=" + msg.str3;
                    Global.CommunicationTest(comurl, comAyy);  

                    //浙江宣仙居县数据上传http通信post
                    string rtn = string.Empty;
                    string UrlA = Global.geturl(msg.str1,3);
                    for (int i = 0; i < msg.selectedRecords.Count; i++)
                    {                        
                        string xml = GetXMLstr(msg.selectedRecords,i);
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
                   

                    if (null != this.target)
                        target.SendMessage(msg, null);

                    break;

                    //检测项目下载
                case MsgCode.MSG_CHECK_SYNC:
                    msg.result = false;
                  
                    int itemOk = 0, itemNG = 0;
                    
                    string GetItemData = Global.DownChkItem("GET", Global.geturl(msg.str1, 2));
                   
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    clsItems deviceStatus = jss.Deserialize<clsItems>(GetItemData);
                    if (deviceStatus.resultCode == "10100")
                    {

                        //删除表中的全部内容，再重新下载
                        clsDownChkItem downchk = new clsDownChkItem();
                        downchk.DelDownItem(out err);//删除原来的数据
                        //DataBase.DeleTableData("", "", "ZJCheckitem");

                        for (int i = 0; i < deviceStatus.datalist.Count; i++)
                        {
                            //string Wr = DataBase.WriteData(i, deviceStatus.datalist[i].itemname, deviceStatus.datalist[i].itemcode);
                            int Wr= downchk.InDownItem(i+1, deviceStatus.datalist[i].itemname, deviceStatus.datalist[i].itemcode, out err);
                            if (Wr == 1)
                            {
                                itemOk = itemOk + 1;
                            }
                            else
                            {
                                itemNG = itemNG + 1;
                            }
                        }
                    }
                    
                    msg.result = true;
                    if (null != this.target)
                    {
                        //msg.SampleStandardName = _SampleStandardAgainDisplay;
                        msg.CheckItemsTempList = itemNG.ToString();
                        msg.CheckItems = itemOk.ToString();
                        //msg.Standard = _Standard;
                        //msg.CheckItemsAdapter = _CheckItemStandard;
                        //msg.DownLoadCompany = _DownCompany;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownCompany:   //下载检定单位
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
            //byte[] cmd = { 0x1b, 0x1c, (byte)camID };
            byte[] cmd = { 0x1b, 0x1c, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            int HDR_LEN = 5;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, timeout))
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
        /// 浙江数据上传
        /// 获取http协议接口的XML上传数据
        /// </summary>
        /// <returns></returns>
        public static string GetXMLstr(List<tlsTtResultSecond> selectedRecords,int i)
        {
            Global.UpY = selectedRecords[i].ID;
            //查询数据库获取项目编号
            //string checkname = DataBase.ReadData(0, (selectedRecords[i].CheckTotalItem), ""); //selectedRecords[i].FoodName
            if (selectedRecords[i].Result == "参考国标")
            {
                return "参考国标";
            }
            if (selectedRecords[i].Result == "无效")
            {
                return "无效";
            }
            string httpxml = "<?xml version='1.0' encoding='utf-8'?>"
                           + "<detectionData version='1.0'>"
                            + "<detectionRecords>"
                             + "<detectionRecord>"
                              + "<entitycode>' '</entitycode>"                                                      //<!--（注册号或身份证号） -->暂时不用传
                              + "<booth>"+"A1"+"</booth>"                                                           //<!--摊位号 -->
                              + " <sampletime>"+ (selectedRecords[i].TakeDate ) + "</sampletime>"                   //<!--（抽样时间） -->
                              + "<detectiontime>" + (selectedRecords[i].CheckStartDate) + "</detectiontime>"        //<!--（检测时间） -->
                              + "<deviceno>" + (selectedRecords[i].CheckMachineModel) + "</deviceno>"               //<!--（检测设备内部编号） -->
                              + "<channelno>" + (selectedRecords[i].Hole)  + "</channelno>"                         //<!--（检测通道编号） -->      
                              + "<prodcode>" + (selectedRecords[i].CheckedCompanyCode) + "</prodcode>"              //<!--（被检产品编号） -->
                              + "<prodamount>" +"1"+ "</prodamount >"                                               //<!--（被检产品数量） -->
                              + "<useunit>' '</useunit>"                                                             //<!--（数量单位） --> 
                              + "<detectItem>"
                                + "<itemcode>" + (selectedRecords[i].ItemCode) + "</itemcode>"                                          //<!--（检测指标编号） -->
                                + " <result>" + (selectedRecords[i].Result=="合格"? "0":"1" ) + "</result>"         //<!--（检测结果0=阴性合格，1=阳性不合格） -->
                                + "<extradata>" + (selectedRecords[i].StandValue) + "</extradata>"              //<!--（抑制率数据） -->
                                + "<data>" + (selectedRecords[i].CheckValueInfo) + "</data>"                        //<!--（数据） -->
                             + "</detectItem>"
                            + "</detectionRecord>"
                         + "</detectionRecords>"
                       + "</detectionData>";

            return httpxml;
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
                            model.username = LoginWindow._curAccount.UserName;
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
                                Global.UploadSCount += upModel.detailsList.Count;
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
                            if (dtValue == null || dtValue.Length == 0)
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
                            upDtbl.Rows[j]["CKCKNAMEUSID"] = Global.samplenameadapter[0].RegisterID;
                            //基层上传人 2017年2月27日修改为上传“设置”界面的用户名
                            //dtValue = upDtbl.Rows[j]["UpLoader"].ToString();
                            //if (dtValue == null || dtValue.Length == 0)
                            //{
                            upDtbl.Rows[j]["UpLoader"] = Global.samplenameadapter[0].RegisterID;
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
                    rtn = Global.Upload(str, server.RegisterID, server.RegisterPassword, server.CheckPointID, server.ServerAddr);
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
            catch (Exception)
            {
                //rtn = ex.Message;
            }
            return (Global.UploadSCount > 0 && Global.UploadFCount == 0) ? "true" : rtn;
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

            #region 旧版本接口
            //string rtn = string.Empty;
            //try
            //{
            //    FoodClient.localhost.DataSyncService localhost = new FoodClient.localhost.DataSyncService();
            //    localhost.Url = url;
            //    rtn = localhost.checkUserConnection(user, password);
            //}
            //catch (Exception ex)
            //{
            //    rtn = ex.Message;
            //}
            //return rtn;
            #endregion
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
                ws.Url = server.ServerAddr;
                rtn = ws.CheckChePoint(server.CheckPointID, server.CheckPointName, server.CheckPointType, server.Organization, server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5").ToString());
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
                    _CheckItemStandard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", server.RegisterID, server.RegisterPassword, "CheckPlan", UDate);
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