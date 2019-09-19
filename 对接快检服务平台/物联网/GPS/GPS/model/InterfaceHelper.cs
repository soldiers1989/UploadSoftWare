using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GPS.model
{
    public class InterfaceHelper
    {
        /// <summary>
        /// entity转json字符串
        /// </summary>
        /// <param name="T">entity</param>
        /// <returns>返回转换后的json字符串</returns>
        public static string EntityToJson(object T)
        {
            return JsonConvert.SerializeObject(T);
        }
        /// <summary>
        /// json字符串转entity
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>返回转换后的entity</returns>
        public static T JsonToEntity<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
        }
        public static object lockpost = new object();
        public static string HttpsPost(string url)
        {
            lock (lockpost)
            {
                string result = string.Empty;
                try
                {
                    HttpWebRequest request = null;

                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        request = WebRequest.Create(url) as HttpWebRequest;
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                 
                        request.ProtocolVersion = HttpVersion.Version10;
                        // 这里设置了协议类型。
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;// (SecurityProtocolType)3072;//SecurityProtocolType.Tls1.2; 
                        request.KeepAlive = false;
                        ServicePointManager.CheckCertificateRevocationList = true;
                        ServicePointManager.DefaultConnectionLimit = 100;
                        ServicePointManager.Expect100Continue = false;
                    }
                    else
                    {
                        request = (HttpWebRequest)WebRequest.Create(url);
                    }

                    request.Method = "POST";    //使用get方式发送数据
                    request.Accept = "*/*";
                    //request.ContentType 
                    request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";

                  
                    //获取网页响应结果
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                   
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return result;
            }
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}
