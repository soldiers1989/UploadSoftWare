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
using test.model;

namespace test
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
        /// 市场信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 1);
            string data = getJson.getMarketInfo(); 
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }
        private string HttpPost(string Url,string postDataStr) //post读取
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method ="POST";

            request.ContentType = "application/json";

            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //CookieContainer cc = new CookieContainer();
            ////cc.SetCookies()
            ////Cookie c = new Cookie();
            ////c.Value = "123";
            //request.CookieContainer = cc;
            
            Stream myRequestStream = request.GetRequestStream();

            StreamWriter myStreamWriter =new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));

            myStreamWriter.Write(postDataStr);

            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Cookie C=new Cookie ();

            //response.Cookies = Cookie.GetCookies(response.ResponseUri);

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader =new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();

            return retString;
        }
        public static string HttpMath(string type, string url, string paramsValue)
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
                //request.Headers.Add("password", "test_dy");
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
                MessageBox.Show(ex.Message);
            }
            return result;

        }
        /// <summary>
        /// 经营户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 2);
            string data = getJson.getOperator ();
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }
        /// <summary>
        /// 菜品信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 3);
            string data = getJson.getMarketInfo();
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }
        /// <summary>
        /// 检测信息下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 4);
            string data = getJson.getMarketInfo();
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 5);
            string data = getJson.updateDevice();
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            string url = getUrl.GetUrl("http://www.sritnb.cn/sxMarket/", 6);
            string data = getJson.uploadData();
            //string result= HttpPost(url, data);
            string result = HttpMath("POST", url, data);
            textBox1.Text = result;
        }
    }
}
