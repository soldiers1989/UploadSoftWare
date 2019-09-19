using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("?did={0}","达元");
            sb.AppendFormat("&jcdw={0}","达元");

            string url = textBox1.Text.Trim() + "?did=001&jcdw=达元&bjdw=果菜市场&jcxm=农药残留菊酯类&jcdt=2019-03-01 09:32:22&jcz=0.05&jgpd=合格&ybmc=青菜&xlbz=GB/T 5009.199-2003&sbbh=LZ4000-1001&xlz=50&jcy=张三";
            string rtn = HttpsPost(url);
            textBox2.Text = rtn;
        }
        public static string HttpsPost(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";    //使用get方式发送数据
                request.Accept = "*/*";
                //request.ContentType 
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";

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
}
