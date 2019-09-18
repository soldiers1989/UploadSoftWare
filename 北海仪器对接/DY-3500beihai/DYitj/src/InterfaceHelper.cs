using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace AIO.src
{
    /// <summary>
    /// 接口帮助类
    /// Create wenj
    /// Time 2017年4月27日
    /// </summary>
    public static class InterfaceHelper
    {
        private static string responseInfo = string.Empty;

        /// <summary>
        /// HTTP通用接口;
        /// 如果 ContentType  "application/json"  json类型那么参数就为"{a:1,b:2}" 格式;
        /// 如果 ContentType  "application/x-www-form-urlencoded" 表单类型，那么参数为 a=1&b=2 形式;
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="paramsContent">报文内容</param>
        /// <param name="methodtype">接口提交类型 1 POST 2 GET 3 SET</param>
        /// <param name="contenttype"></param>
        /// <returns>返回值</returns>
        public static string HttpMath(string url, string paramsContent = "", int methodtype = 1, int contenttype = 1)
        {
            string methodType = string.Empty, contentType = string.Empty;
            responseInfo = string.Empty;
            methodType = methodtype == 1 ? "POST" : methodtype == 2 ? "GET" : "SET";
            contentType = contenttype == 1 ? "application/json" : "application/x-www-form-urlencoded";
            try
            {
                //报文需要转义
                byte[] byteArray = Encoding.UTF8.GetBytes(HttpUtility.UrlEncode(paramsContent, Encoding.UTF8));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = methodType;
                request.ContentType = contentType;
                request.ContentLength = byteArray.Length;
                //添加header
                //request.Headers.Add("userName", "admin");
                //添加cookie
                //CookieContainer cookie = new CookieContainer();
                //cookie.Add(new Cookie("", "", "", ""));
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseInfo = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseInfo;
        }
        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string HttpPost(string postdata,out string errMsg)
        {
            errMsg = "";
            responseInfo = string.Empty;
            try
            {
                HttpWebRequest request = null;
                request = (HttpWebRequest)WebRequest.Create(postdata);
                request.Method = "POST";    
                request.Accept = "*/*";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";
                //获取网页响应结果
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream))
                {
                    responseInfo = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return responseInfo;
        }


        /// <summary>
        /// 北海通信测试
        /// </summary>
        /// <returns></returns>
        public static string BeihaiCommunicateTest(string url, string user, string pwd, int type, out string errMsg)
        {
            errMsg = "";
            responseInfo = string.Empty;
            try
            {
                if(type ==1)//通信测试
                {
                    string password = Encrypt(pwd);
                    string HttpAddr = BeiHaiURL(url, 1);
                    string data = HttpAddr + "?username=" + user + "&password=" + password;
                    responseInfo = HttpPost(data, out errMsg);
                }
                else if (type == 2)//待检数据下载
                {
                    string HttpAddr = BeiHaiURL(url, 2);
                    string data = HttpAddr + "?username=" + user + "&unit=" + Global.UnitID;
                    responseInfo = HttpPost(data, out errMsg);
                }
                else if (type == 3)//数据上传
                {
                    responseInfo = HttpPost(url, out errMsg);
                }

            }
            catch (Exception ex)
            {
                errMsg= ex.Message;
            }
            return responseInfo;
        }

        /// <summary>
        /// 服务器通讯验证
        /// </summary>
        /// <param name="errMsg">异常信息</param>
        /// <returns>返回通讯结果</returns>
        public static string CheckUserCommunication(string url, string user, string pwd)
        {
            responseInfo = string.Empty;
            try
            {
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}", GetServiceURL(url, 1), user, Global.MD5(pwd)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseInfo;
        }
        public static string ToJson(object obj)
        {
            JavaScriptSerializer jl = new JavaScriptSerializer();
            return jl.Serialize(obj);
        }
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="json">上传Json数据</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string UploadData(string url, string user, string pwd, string json)
        {
            responseInfo = string.Empty;
            try
            {
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}&mode={3}", GetServiceURL(url, 3), user, Global.MD5(pwd), "DY-3000(BX1)"), json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseInfo;
        }

        /// <summary>
        /// 数据字典下载通用接口
        /// </summary>
        /// <param name="type">下载类型</param>
        /// <param name="errMsg">异常信息</param>
        /// <param name="model">仪器型号</param>
        /// <returns>返回值</returns>
        public static string DownloadBasicData(string url, string user, string pwd, string type, string model = "")
        {
            responseInfo = string.Empty;
            try
            {
                //如果是获取抽样任务，则要加上机器码
                //if (type.Equals(Global.GetSampleTask))
                //{
                //    model = "LZ4000-170904001";
                //}
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}&mode={3}&type={4}", GetServiceURL(url, 2), user, Global.MD5(pwd), model, type));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseInfo;
        }
        /// <summary>
        /// 北海接口获取URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string BeiHaiURL(string url, int type)
        {
            int index = url.LastIndexOf('/');
            //登录验证
            if (type == 1)
            {
                url += (index == url.Length - 1) ? "bhzhgz/VmSamplingCheckController/login.do" : "/bhzhgz/VmSamplingCheckController/login.do";
            }
            //用户待检数据下载
            else if (type == 2)
            {
                url += (index == url.Length - 1) ? "bhzhgz/VmSamplingCheckController/getAllSamplings.do" : "/bhzhgz/VmSamplingCheckController/getAllSamplings.do";
            }
            //数据上传
            else if (type == 3)
            {
                url += (index == url.Length - 1) ? "bhzhgz/VmSamplingCheckController/doQuickInfo.do" : "/bhzhgz/VmSamplingCheckController/doQuickInfo.do";
            }
            return url;
        }

        /// <summary>
        /// 获取服务器url 适用于快检车系统
        /// 1、服务器通讯验证；2、基础数据下载；3、数据上传
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetServiceURL(string url, int type)
        {
            int index = url.LastIndexOf('/');
            //服务器通讯验证
            if (type == 1)
            {
                url += (index == url.Length - 1) ? "os/java/pub/user/checkUserConnect.shtml" : "/os/java/pub/user/checkUserConnect.shtml";
            }
            //基础数据下载
            else if (type == 2)
            {
                url += (index == url.Length - 1) ? "os/java/pub/data/downloadBasicData.shtml" : "/os/java/pub/data/downloadBasicData.shtml";
            }
            //数据上传
            else if (type == 3)
            {
                url += (index == url.Length - 1) ? "os/java/pub/data/upLoadCheckData.shtml" : "/os/java/pub/data/upLoadCheckData.shtml";
            }
            return url;
        }
        /// <summary>
        /// 向量
        /// </summary>
        private const string IV_64 = "EacicDpc";
        /// <summary>
        /// 密钥
        /// </summary>
        private const string KEY_64 = "EacicDpc";
        /// <summary>
        /// DES加密数据
        /// </summary>
        /// <param name="data">加密内容</param>
        /// <returns>加密后的数据</returns>
        public static string Encrypt(string data)
        {
            if (data != "")
            {
                byte[] bytes = Encoding.ASCII.GetBytes(KEY_64);
                byte[] rgbIV = Encoding.ASCII.GetBytes(IV_64);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                int keySize = provider.KeySize;
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(stream2);
                writer.Write(data);
                writer.Flush();
                stream2.FlushFinalBlock();
                writer.Flush();
                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }
            return data;
        }
    }
}