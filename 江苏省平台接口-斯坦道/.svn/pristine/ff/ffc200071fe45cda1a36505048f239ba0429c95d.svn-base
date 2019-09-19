using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Xml;
using DYSeriesDataSet;
using System.Web.Script.Serialization;
using System.Net.Security;

namespace AIO
{
    public class Wisdom
    {
        #region 智慧食药监 - 全局相关

        /// <summary>
        /// 获取快检单号
        /// </summary>
        public static string GETSAMPLE_URL = string.Empty;
        /// <summary>
        /// TYPE 获取快检单号
        /// </summary>
        public static string GETSAMPLE = "GETSAMPLE";
        /// <summary>
        /// 快检单号
        /// </summary>
        public static string GETSAMPLECODE = string.Empty;

        /// <summary>
        /// 快检单上传
        /// </summary>
        public static string UPLOADSAMPLE_URL = string.Empty;
        /// <summary>
        /// TYPE 快检单上传
        /// </summary>
        public static string UPLOADSAMPLE = "UPLOADSAMPLE";
        public static bool ISUPLOADSAMPLE = false;

        /// <summary>
        /// 快检结果上报
        /// </summary>
        public static string UPLOADRESULT_URL = string.Empty;
        /// <summary>
        /// TYPE 快检结果上报
        /// </summary>
        public static string UPLOADRESULT = "UPLOADRESULTURL";
        public static bool ISUPLOADRESULT = false;

        /// <summary>
        /// 快检结果下载
        /// </summary>
        public static string DOWNLOADRESULT_URL = string.Empty;
        /// <summary>
        /// TYPE 快检结果下载
        /// </summary>
        public static string DOWNLOADRESULT = "DOWNLOADRESULT";

        /// <summary>
        /// 快检设备开关机状态上报
        /// </summary>
        public static string DEVICESTATUS_URL = string.Empty;
        /// <summary>
        /// TYPE 快检设备开关机状态上报
        /// </summary>
        public static string DEVICESTATUS = "DEVICESTATUS";

        /// <summary>
        /// 唯一机器码
        /// </summary>
        public static string DeviceID = string.Empty;
        public static string USER = string.Empty;
        public static string PASSWORD = string.Empty;

        /// <summary>
        /// 获取快检单 - 请求
        /// </summary>
        public static getsample.Request GETSAMPLE_REQUEST = null;
        /// <summary>
        /// 获取快检单 - 响应
        /// </summary>
        public static getsample.Response GETSAMPLE_RESPONSE = null;
        /// <summary>
        /// 快检单号上传 - 请求
        /// </summary>
        public static uploadSample.Request UPLOADSAMPLE_REQUEST = null;
        /// <summary>
        /// 快检单号上传 - 响应
        /// </summary>
        public static uploadSample.Response UPLOADSAMPLE_RESPONSE = null;
        /// <summary>
        /// 快检设备开关机状态上报 - 上传
        /// </summary>
        public static deviceStatus.Request DEVICESTATUS_REQUEST = null;
        /// <summary>
        /// 快检设备开关机状态上报 - 响应
        /// </summary>
        public static deviceStatus.Response DEVICESTATUS_RESPONSE = null;
        /// <summary>
        /// 快检结果上报 - 请求
        /// </summary>
        public static uploadResult.Request UPLOADRESULT_REQUEST = null;
        /// <summary>
        /// 快检结果上报 - 响应
        /// </summary>
        public static uploadResult.Response UPLOADRESULT_RESPONSE = null;
        /// <summary>
        /// 快检结果下载 - 请求
        /// </summary>
        public static downloadResult.Request DOWNLOADRESULT_REQUEST = null;
        /// <summary>
        /// 快检结果下载 - 响应
        /// </summary>
        public static downloadResult.Response DOWNLOADRESULT_RESPONSE = null;

        public static string gpsAddress = string.Empty;
        /// <summary>
        /// 经度 -180~180
        /// </summary>
        public static string gpsValueJD = string.Empty;
        /// <summary>
        /// 纬度 -90~90
        /// </summary>
        public static string gpsValueWD = string.Empty;

        public static string gpsJD = string.Empty;
        public static string gpsWD = string.Empty;

        #endregion

        ///  <summary>
        ///  用VID和PID查找HID设备
        ///  </summary>
        ///  <returns>True： 找到设备</returns>
        public static bool FindTheHid()
        {
            //try
            //{
            //    string strvid = "0483", strpid = "5750";
            //    int myVendorID = 0x03EB;
            //    int myProductID = 0x2013;
            //    int vid = 0;
            //    int pid = 0;
            //    try
            //    {
            //        vid = Convert.ToInt32(strvid, 16);
            //        pid = Convert.ToInt32(strpid, 16);
            //        myVendorID = vid;
            //        myProductID = pid;
            //    }
            //    catch (SystemException e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }
            //    if (MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
            //    {
            //        getCommunication();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return false;
        }

        /// <summary>
        /// 获取通讯
        /// </summary>
        /// <returns></returns>
        public static void getCommunication()
        {
            //建立通讯
            string cmd = "0xFF 0x08 0x30 0x00 0x00 0x00 0x38 0xFE";
            byte[] bt = ReadAndWriteToDevice(cmd);
        }

        /// <summary>
        /// 获取数据指令
        /// </summary>
        /// <returns></returns>
        public static string getCmd(int index)
        {
            string str = "0x08 0x31 0x00 0x00 0x" + index.ToString("X2");
            byte crc = 0;
            byte[] btList = Wisdom.StringToBytes(str, new string[] { ",", " " }, 16);
            for (int i = 0; i < btList.Length; i++)
                crc += btList[i];

            str = "0xFF 0x08 0x31 0x00 0x00 0x" + index.ToString("X2") + " 0x" + crc.ToString("X2") + " 0xFE";

            return str;
        }

        public static List<byte[]> getByteList(byte[] data)
        {
            List<byte[]> dataList = new List<byte[]>();
            int index = 3;
            for (int i = 0; i < 3; i++)
            {
                byte[] bt = new byte[18];
                for (int j = 0; j < bt.Length; j++)
                {
                    index++;
                    bt[j] = data[index];
                    if (index == 21 || index == 39 || index == 57)
                        dataList.Add(bt);
                }
            }
            return dataList;
        }

        /// <summary>
        /// 读取下位机返回的数据
        /// </summary>
        /// <returns></returns>
        public static byte[] ReadAndWriteToDevice(string cmd)
        {
            int len = 64;
            byte[] inputdatas = new byte[len];
            //try
            //{
            //    byte[] outdatas = new byte[len];
            //    outdatas[0] = 0x55;
            //    outdatas[1] = 0x2;
            //    outdatas[2] = 0x1;
            //    outdatas[3] = 0x00;
            //    byte[] inputs = StringToBytes(cmd, new string[] { ",", " " }, 16);
            //    if (inputs != null && inputs.Length > 0)
            //        outdatas = inputs;
            //    System.Windows.Forms.Application.DoEvents();
            //    //MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, 100);
            //    if (MyDeviceManagement.WriteReport(0, false, outdatas, ref inputdatas, 100))
            //    {
            //        int length = 0;
            //        while (!MyDeviceManagement.ReadReport(0, false, outdatas, ref inputdatas, 100))
            //        {
            //            length++;
            //            if (length == 20)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return inputdatas;
        }

        /// <summary>
        /// 将给定的字符串按照给定的分隔符和进制转换成字节数组
        /// </summary>
        /// <param name="str">给定的字符串</param>
        /// <param name="splitString">分隔符</param>
        /// <param name="fromBase">给定的进制</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] StringToBytes(string str, string[] splitString, int fromBase)
        {
            string[] strBytes = str.Split(splitString, StringSplitOptions.RemoveEmptyEntries);
            if (strBytes == null || strBytes.Length == 0)
                return null;
            byte[] _return = new byte[strBytes.Length];
            for (int i = 0; i < strBytes.Length; i++)
            {
                try
                {
                    _return[i] = Convert.ToByte(strBytes[i], fromBase);
                }
                catch (SystemException)
                {
                    throw;
                    //MessageBox.Show("发现不可转换的字符串->" + strBytes[i]);
                }
            }
            return _return;
        }

        /// <summary>
        /// 将给定的数组转换成符合格式要求的字符串
        /// </summary>
        /// <param name="data">给定的数组</param>
        /// <param name="startIndex">起始字节序号</param>
        /// <param name="length">转换长度</param>
        /// <param name="prefix">前缀，例如 0x </param>
        /// <param name="splitString">分隔符</param>
        /// <param name="fromBase">进制10或16,其它值一律按16进制处理</param>
        /// <returns>字符串</returns>
        public static string BytesToString(byte[] data, int startIndex, int length, string prefix, string splitString, int fromBase)
        {
            string _return = string.Empty;
            if (data == null)
                return _return;
            for (int i = 0; i < length; i++)
            {
                if (startIndex + i < data.Length)
                {
                    switch (fromBase)
                    {
                        case 10:
                            _return += string.Format("{0}{1:d3}", prefix, data[i + startIndex]);
                            break;
                        default:
                            _return += string.Format("{0}{1:X2}", prefix, data[i + startIndex]);
                            break;
                    }
                    if (i < data.Length - 1)
                        _return += splitString;
                }
            }
            return _return;
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        private static int getCount()
        {
            string cmd = "0xFF 0x08 0x31 0x01 0x00 0x00 0x3A 0xFE";
            byte[] data = ReadAndWriteToDevice(cmd);
            if (data[4] == 0x00 && data[5] != 0x00)
                return data[5];
            return 0;
        }

        /// <summary>
        /// 获取Url
        /// </summary>
        /// <param name="UrlType"></param>
        /// <returns></returns>
        private static string getUrl(string UrlType)
        {
            string url = string.Empty;
            if (UrlType.Equals(GETSAMPLE))
            {
                //快检单获取
                url = GETSAMPLE_URL;
            }
            else if (UrlType.Equals(UPLOADSAMPLE))
            {
                //快检单上传
                url = UPLOADSAMPLE_URL;
                Wisdom.ISUPLOADSAMPLE = true;
            }
            else if (UrlType.Equals(UPLOADRESULT))
            {
                //快检结果上传
                url = UPLOADRESULT_URL;
                Wisdom.ISUPLOADRESULT = true;
            }
            else if (UrlType.Equals(DOWNLOADRESULT))
            {
                //快检结果下载
                url = DOWNLOADRESULT_URL;
            }
            else if (UrlType.Equals(DEVICESTATUS))
            {
                //快检设备开关机状态上报
                url = DEVICESTATUS_URL;
            }
            return url;
        }

        /// <summary>
        /// 获取参数字符串
        /// </summary>
        /// <param name="PostType"></param>
        /// <returns></returns>
        private static string getParamaters(string PostType)
        {
            string strUSER = USER.Length > 0 ? USER 
                : (LoginWindow._userAccount == null ? "admin" : LoginWindow._userAccount.UserName);
            string strPWD = PASSWORD.Length > 0 ? PASSWORD 
                : (LoginWindow._userAccount == null ? "admin" : LoginWindow._userAccount.UserPassword);

            Encoding myEncoding = Encoding.GetEncoding("UTF-8");
            string paramaters = HttpUtility.UrlEncode("usr", myEncoding) + "=" + HttpUtility.UrlEncode(strUSER, myEncoding)
                + "&" + HttpUtility.UrlEncode("pwd", myEncoding) + "=" + HttpUtility.UrlEncode(strPWD, myEncoding)
                + "&" + HttpUtility.UrlEncode("result", myEncoding) + "=";

            string json = string.Empty;
            if (PostType.Equals(GETSAMPLE))
            {
                //快检单获取
                //sampleid|快检单号；deviceid|唯一机器码；
                if (GETSAMPLE_REQUEST != null)
                {
                    json = Json.ObjectToJson(GETSAMPLE_REQUEST);
                    paramaters += HttpUtility.UrlEncode(json, myEncoding);
                }
            }
            else if (PostType.Equals(UPLOADSAMPLE))
            {
                //快检单上传
                if (UPLOADSAMPLE_REQUEST != null)
                {
                    json = Json.ObjectToJson(UPLOADSAMPLE_REQUEST);
                    paramaters += HttpUtility.UrlEncode(json, myEncoding);
                }
            }
            else if (PostType.Equals(UPLOADRESULT))
            {
                //快检结果上传
                if (UPLOADRESULT_REQUEST != null)
                {
                    json = Json.ObjectToJson(UPLOADRESULT_REQUEST);
                    paramaters += HttpUtility.UrlEncode(json, myEncoding);
                }
            }
            else if (PostType.Equals(DOWNLOADRESULT))
            {
                //快检结果下载
                //deviceid|唯一机器码；dateStart|开始时间；dateEnd|结束时间；
                if (DOWNLOADRESULT_REQUEST != null)
                {
                    json = Json.ObjectToJson(DOWNLOADRESULT_REQUEST);
                    paramaters += HttpUtility.UrlEncode(json, myEncoding);
                }
            }
            else if (PostType.Equals(DEVICESTATUS))
            {
                //快检设备开关机状态上报 
                //deviceid|唯一机器码；longitude|经度；latitude|纬度；deviceStatus|1代表开机，2代表保持运行，0代表关机；
                if (DEVICESTATUS_REQUEST != null)
                {
                    json = Json.ObjectToJson(DEVICESTATUS_REQUEST);
                    paramaters += HttpUtility.UrlEncode(json, myEncoding);
                }
            }
            return paramaters;
        }

        /// <summary>
        /// 广东食药监管平台项目 - 公共接口
        /// </summary>
        /// <param name="TYPE">类型</param>
        /// <returns></returns>
        public static string HttpPostRequest(string TYPE)
        {
            string URL = getUrl(TYPE), paramaters = getParamaters(TYPE),
                result = string.Empty, json = string.Empty;
            Wisdom.ISUPLOADSAMPLE = false;
            Wisdom.ISUPLOADRESULT = false;

            //解决.NET调用HTTP接口时的异常：底层连接被关闭:不能建立信任关系的SSL / TLS安全的通道
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(URL);
            objRequest.Method = "POST";
            objRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            try
            {
                //将Json字符串转化为字节  
                byte[] postData = System.Text.Encoding.UTF8.GetBytes(paramaters);
                //设置请求的ContentLength   
                objRequest.ContentLength = postData.Length;
                //写入流
                using (Stream reqStream = objRequest.GetRequestStream())
                    reqStream.Write(postData, 0, postData.Length);
                
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    if (!result.Equals(string.Empty))
                        return result;
                    sr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 上传设备运行状态
        /// 1代表开机，2代表保持运行，0代表关机
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool UploadDeviceStatus()
        {
            try
            {
                string result = Wisdom.HttpPostRequest(Wisdom.DEVICESTATUS);
                if (result.Length > 0)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    deviceStatus.Response deviceStatus = js.Deserialize<deviceStatus.Response>(result);
                    if (deviceStatus.code.Equals("0"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

    }
}
