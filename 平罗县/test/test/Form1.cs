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

        private void btnDownSample_Click(object sender, EventArgs e)
        {
            txtShowSample.Text = "";
            string addr = txtAddr.Text.Trim();
            string url= GetUrl(addr, 1);
            txtShowSample.Text = HttpMath("POST", url,"");
            
        }
        public static string GetUrl(string url, int type)
        {
            string rtn = "";
            int index = url.LastIndexOf('/');
            if (type == 1)
            {
                if (index == url.Length - 1)
                {
                    rtn = url + "api/Index/get_goods?token=plx_plx123";
                }
                else
                {
                    rtn = url + "/api/Index/get_goods?token=plx_plx123";
                }
            }
            else if (type == 2)
            {
                if (index == url.Length - 1)
                {
                    rtn = url + "api/Index/save_goods?token=plx_plx123&goods_arr={\"\":\"5%\",\"0\":\"8%\"}";
                }
                else
                {
                    rtn = url + "/api/Index/save_goods?token=plx_plx123&goods_arr={\"\":\"5%\",\"0\":\"8%\"}";
                }
            }

            return rtn;
        }
        private void btnUploadData_Click(object sender, EventArgs e)
        {
            txtShowUpData.Text = "";
            string addr = txtAddr.Text.Trim();
            string url = GetUrl(addr, 2);
            txtShowUpData.Text = HttpMath("POST", url, "");
        }
        public static string HttpMath(string type, string url, string paramsValue)
        {
            string result = string.Empty;

            try
            {
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.ContentType = "application/json";
                if(paramsValue!="")
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                    request.ContentLength = byteArray.Length;
                    //自定义header
                    //request.Headers.Add("password", "test_dy");

                    using (Stream newStream = request.GetRequestStream())
                    {
                        newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                    }
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

    }
}
