﻿using System;
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
using System.Xml;

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

                case MsgCode.MSG_UPLOAD:
                    msg.result = false;
                    string err = string.Empty;
                    Global.UploadSCount = Global.UploadFCount = 0;
                    if (msg.selectedRecords.Count > 0)
                    {
                        for (int i = 0; i < msg.selectedRecords.Count; i++)
                        {
                            //msg.selectedRecords[i].SAMPLEUUID = "1000021381460175620171031001";//1000021381460175620171023002
                            Global.UpY = msg.selectedRecords[i].ID;
                            StringBuilder sb = new StringBuilder();
                            if (msg.selectedRecords[i].ResultType == "分光光度")
                            {
                                sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><RESULT><BODY><TESTRESULTS>");
                                string JCJGZ = (msg.selectedRecords[i].Result.Equals("合格") || msg.selectedRecords[i].Result.Equals("阴性")) ? "1" : "0";//<REMARK>{5}</REMARK
                                sb.AppendFormat("<TESTRESULT><SAMPLEUUID>{0}</SAMPLEUUID><JCJGZ>{1}</JCJGZ><JCVALUE>{2}</JCVALUE><STANDARD>{3}</STANDARD><UNIT>{4}</UNIT>",
                                    msg.selectedRecords[i].SAMPLEUUID, JCJGZ, msg.selectedRecords[i].CheckValueInfo.Length == 0 ? "0" + msg.selectedRecords[i].ResultInfo : msg.selectedRecords[i].CheckValueInfo + msg.selectedRecords[i].ResultInfo,
                                    msg.selectedRecords[i].StandValue + msg.selectedRecords[i].ResultInfo, msg.selectedRecords[i].ResultInfo);
                                sb.AppendFormat("<JCSB>{0}</JCSB>", msg.selectedRecords[i].MachineCompany + "-" + msg.selectedRecords[i].DeviceId);
                                sb.Append("</TESTRESULT></TESTRESULTS></BODY></RESULT>");
                            }
                            else
                            {
                                sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><RESULT><BODY><TESTRESULTS>");
                                string JCJGZ = (msg.selectedRecords[i].Result.Equals("合格") || msg.selectedRecords[i].Result.Equals("阴性")) ? "1" : "0";//<REMARK>{5}</REMARK
                                sb.AppendFormat("<TESTRESULT><SAMPLEUUID>{0}</SAMPLEUUID><JCJGZ>{1}</JCJGZ><JCVALUE>{2}</JCVALUE><STANDARD>{3}</STANDARD><UNIT>{4}</UNIT>",
                                    msg.selectedRecords[i].SAMPLEUUID, JCJGZ, msg.selectedRecords[i].CheckValueInfo.Length == 0 ? "" : msg.selectedRecords[i].CheckValueInfo,
                                    msg.selectedRecords[i].StandValue , msg.selectedRecords[i].ResultInfo);
                                sb.AppendFormat("<JCSB>{0}</JCSB>", msg.selectedRecords[i].MachineCompany + "-" + msg.selectedRecords[i].DeviceId);
                                sb.Append("</TESTRESULT></TESTRESULTS></BODY></RESULT>");
                            }
                            //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><RESULT><BODY><TESTRESULTS>");
                            //string JCJGZ = (msg.selectedRecords[i].Result.Equals("合格") || msg.selectedRecords[i].Result.Equals("阴性")) ? "1" : "0";//<REMARK>{5}</REMARK
                            //sb.AppendFormat("<TESTRESULT><SAMPLEUUID>{0}</SAMPLEUUID><JCJGZ>{1}</JCJGZ><JCVALUE>{2}</JCVALUE><STANDARD>{3}</STANDARD><UNIT>{4}</UNIT></TESTRESULT>",
                            //    msg.selectedRecords[i].SAMPLEUUID, JCJGZ, msg.selectedRecords[i].CheckValueInfo.Length == 0 ? "0" : msg.selectedRecords[i].CheckValueInfo,
                            //    msg.selectedRecords[i].StandValue, msg.selectedRecords[i].ResultInfo);
                            //sb.Append("</TESTRESULTS></BODY></RESULT>");
                            string dd = sb.ToString();

                            WebReference.Testplaninfo web = new WebReference.Testplaninfo();
                            string uploadreturn = web.submitTestResultNew(sb.ToString());
                            string result = "", upmesage = string.Empty, uptime = string.Empty;
                            if (uploadreturn.Length > 0)
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(uploadreturn);

                                string tasktime = string.Empty;
                                XmlNodeList xList = xd.GetElementsByTagName("XMLRESULT");
                                foreach (XmlNode item in xList)
                                {
                                    if (item.Name.Equals("XMLRESULT"))
                                    {
                                        result = item.InnerText;
                                    }
                                    else if (item.Name.Equals("XMLMESSAGE"))
                                    {
                                        upmesage = item.InnerText;
                                    }
                                    else if (item.Name.Equals("SENDDATE"))
                                    {
                                        uptime = item.InnerText;
                                    }
                                }
                                //XmlNodeList xList = xd.GetElementsByTagName("HEAD");

                                //foreach (XmlNode item in xList)
                                //{
                                //    if (item.Name.Equals("XMLRESULT"))
                                //    {
                                //        result =  item.InnerText;
                                //    }
                                //    else if (item.Name.Equals("XMLMESSAGE"))
                                //    {
                                //        upmesage = item.InnerText;
                                //    }
                                //    else if (item.Name.Equals("SENDDATE"))
                                //    {
                                //        uptime = item.InnerText;
                                //    }
                                //}
                            }
                            if (result == "1")
                            {
                                Global.UploadSCount = Global.UploadSCount + 1;
                                _bll.UpdateResult("Y", Global.UpY, out err);
                               
                            }
                            else
                            {
                                Global.UploadFCount = Global.UploadFCount + 1;
                            }

                           
                        }
                        msg.result = true;
                        //msg.result = msg.errMsg.Length == 0 ? true : false;
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
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