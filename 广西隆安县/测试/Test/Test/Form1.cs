using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// HTTP接口测试
        /// 如果 contentype  "application/x-www-form-urlencoded" 表单类型，那么参数为 a=1&b=2 形式
        /// 如果 contentype  "application/json"  json类型那么参数就为"{a:1,b:2}" 格式
        /// </summary>
        /// <param name="type">接口类型 POST GET SET</param>
        /// <param name="url"></param>
        /// <param name="paramsValue">报文内容</param>
        /// <returns></returns>
        public string HttpMath(string type, string url, string paramsValue)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                //自定义header
                //request.Headers.Add("userName", "admin");
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public string newCheckUserConnection(string url, string user, string password)
        {
            string parameter = string.Empty;
            int index = url.LastIndexOf('/');
            if (index == url.Length - 1)
                url += "checkUser.do";
            else
                url += "/checkUser.do";
            url += string.Format("?userName={0}&passWord={1}", user,MD5(password));
            url += "&serialNumber=dy3500-123456";
            url += "&deviceType=3";
            string rtn = HttpMath("POST", url, parameter);
            return rtn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://app.gxfda.gov.cn/ncptest/tool/";
            string rtn = newCheckUserConnection(url, "sckbf00888", "sckbf00888");
        }
        public static string MD5(string pwd)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString();
        }
    }
}
