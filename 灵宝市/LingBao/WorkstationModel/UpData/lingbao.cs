using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WorkstationModel.UpData
{
    public class lingbao
    {
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">数据</param>
        /// <returns></returns>
        public static string HttpsPost(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";    //使用get方式发送数据
                request.Accept = "*/*";
                //request.ContentType = "";
                //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                //request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";
                //获取网页响应结果
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                //client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
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
    public class resultData
    {
        //{"status":1,"info":"success"}
        public int status { get; set; }
        public string info { get; set; }
    }
}
