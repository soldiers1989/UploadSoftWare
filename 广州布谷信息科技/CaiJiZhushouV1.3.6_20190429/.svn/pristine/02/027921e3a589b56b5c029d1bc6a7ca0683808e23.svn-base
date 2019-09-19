using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using WorkstationDAL.Model;

namespace WorkstationModel.beihai
{
    public class clsHpptPost
    {
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
        public static string BeihaiCommunicateTest(string url, string user, string pwd, int type, out string errMsg)
        {
            errMsg = "";
            string responseInfo = string.Empty;
            try
            {
                if (type == 1)//通信测试
                {
                    string password = Encrypt(pwd);
                    string HttpAddr = BeiHaiURL(url, 1);
                    string data = HttpAddr + "?username=" + user + "&password=" + password;
                    responseInfo = HttpPost(data, out errMsg);
                }
                else if (type == 2)//待检数据下载
                {
                    string HttpAddr = BeiHaiURL(url, 2);
                    string data = HttpAddr + "?username=" + user + "&unit=" + Global.CheckUnitcode;
                    responseInfo = HttpPost(data, out errMsg);
                }
                else if (type == 3)//数据上传
                {
                    responseInfo = HttpPost(url, out errMsg);
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return responseInfo;
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

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string HttpPost(string postdata, out string errMsg)
        {
            errMsg = "";
            string responseInfo = string.Empty;
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


    }
}
