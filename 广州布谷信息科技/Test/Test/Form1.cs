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

        private void button1_Click(object sender, EventArgs e)
        {
            string json = "";
        
            Result rt = new Result();
            
            Framsds fras = new Framsds();
            fras.UserName = "gzbugu";
            fras.UserPassWord = "gzbugu";

            Farmsdata dd = new Farmsdata();
            dd.shebei_code = "TL31020190401";
            dd.Area = "广州萝岗";
            dd.BoothCode = "1232";
            dd.Stations = "达元检测室";
            dd.BoothName = "达元";
            dd.BoothNum = "B10";
            dd.SampleCode = "009";
            dd.SampleName = "上海青";
            dd.DetectionName = "农药残留";
            dd.DetectionValue = "0.1";
            dd.Range = "50";
            dd.Result = "合格";
            dd.Unit = "%";
            dd.DetectionDate = "2019-04-26 14:14:30";

            List<Farmsdata> framsds = new List<Farmsdata>();
            framsds.Add(dd);

            fras.Farms = framsds;


           rt.FarmsDS = fras;
           json = JsonClass.EntityToJson(rt);
          string rtn=  HttpPOST(@"http://119.29.22.167:8117/api/FoodSafety/Tran", json);

        }
        public static string HttpPOST( string url, string paramsValue)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                //自定义header
                //CookieContainer cc = new CookieContainer();     //post发送数据携带Cookie身份认证信息
                //cc.Add(new Uri(url), new Cookie("token", PostToken));
                //request.CookieContainer = cc;
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
                return ex.Message;
            }
            return result;

        }


    }
}
