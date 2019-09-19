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
        private string tokens = "";

        private void btnlink_Click(object sender, EventArgs e)
        {
            string result = HttpXML("POST", "http://47.107.234.134:8080/p/v1/login", "username=xiaopeng&password=123456");
            returnList rtndata = Json.JsonToEntity<returnList>(result);
            if(rtndata!=null )
            {
                if (rtndata.ok == "True")
                {
                    returnBody bdy = Json.JsonToEntity<returnBody>(rtndata.body.ToString());
                    if (bdy != null)
                    {
                        tokens = bdy.token;
                        MessageBox.Show("通信测试成功！");
                    }
                }
            }

        }
        public static string HttpXML(string type, string url, string paramsValue)
        {
            string result = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
            
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
               
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
        public static string HttpXML1(string type, string url, string paramsValue)
        {
            string result = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;

                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

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

        private void btnUpdata_Click(object sender, EventArgs e)
        {
            result rtl = new result();
            rtl.userToken = tokens;


            Bodys bds = new Bodys();
            bds.samplename="白菜";
            bds.samplenum ="100011";
            bds.stand_num="GB2013";
            //时间要转换
            DateTime dt = DateTime.Parse("2019-05-11 13:30:20");
            string dd = ((dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();

            bds.testingtime = dd;
            bds.inspector ="达元";
            bds.controlvalue="50";//对照值
            bds.cov_unit="%";
            bds .decisionoutcome="合格";
            bds.deviceNumber="DY-3000(BX1)";
            bds.testresult="0.5";
            bds.testproject ="农药残留";
            bds.site = "广州";

            string data = "{\"samplename\":\"" + "白菜" + "\","
                + "\"controlvalue\":\"" + "2.34" + "\","
                + "\"testproject\":\"" + "农药残留" + "\","
                + "\"testingtime\":\"" + dd + "\","
                + "\"testresult\":\"" + "0.5" + "\","
                + "\"inspector\":\"" + "达元" + "\","
                + "\"samplenum\":\"" + "100011" + "\","
                + "\"stand_num\":\"" + "GB2003" + "\","
                + "\"deviceNumber\":\"" + "DY-3000(BX1)" + "\","
                + "\"decisionoutcome\":\"" + "合格" + "\","
                + "\"cov_unit\":\"" + "50" + "\","
                + "\"testsite\":\"" + "广州" + "\"}";
            //string data = "{\"samplename\":\"白菜\",\"controlvalue\":\"2.34\",\"testproject\":\"农药残留\",\"testingtime\":\"1557552620\",\"testresult\":\"0.5\",\"inspector\":\"达元\",\"samplenum\":\"100011\",\"stand_num\":\"GB2003\",\"deviceNumber\":\"DY-3000(BX1)\",\"decisionoutcome\":\"合格\",\"cov_unit\":\"<50%\",\"testsite\":\"-----\"}\"}";
          
            rtl.body = data;

            //rtl.body = bds;

            string json = Json.EntityToJson(rtl);

            //string jsons = "{\"userToken\":\"eyJhbGciOiJIUzUxMiJ9.eyJpZCI6ImJiNmVlNzFiLTZkYzItNGUwMy1iYzVmLWIxYTcxMDgwYjA1ZCIsInVzZXJuYW1lIjoiaGdodXNlciIsImVuYWJsZWQiOnRydWUsIm5pY2tuYW1lIjoi5YiY5Li75Lu7IiwibW9iaWxlIjoiMTMxNzA0MTcyNzAiLCJvcmdhbklkIjoiYjcxODAzNDMtMzkwNC00MzRlLWJkYzEtYTI0OGViMjYyMTI0Iiwib3JnYW5Db2RlIjoiNDMwOTAzMTA0YWFhYSIsIm9yZ2FuTmFtZSI6Ium7hOadhua5liIsImV4cCI6MTU2MTc5MDU4M30.5hWS6g3MUbfNVOJcSsFrel50_65Jij5uWo1m70hXGA8OFlmT_BYXyoPFQIKGMRBJ9MWBZZejEn4oUVYS6XgnZw\",\"body\":\"{\"samplename\": \"洞庭碧螺春\",\"controlvalue\":\"0.869\",\"testproject\":\"农药残留\",\"testingtime\":1530498771208,\"testresult\":\"70.90%\",\"inspector\":\"admin\",\"samplenum\":\"A00003\",\"stand_num\":\"GB/T 5009.199-2003\",\"deviceNumber\":\"DY-1000-123321\",\"decisionoutcome\":\"不合格\",\"cov_unit\":\"<50%\",\"testsite\":\"-----\"}\"}";

            string result = HttpXML1("POST", "http://47.107.234.134:8080/p/v1/produce", json);
            if (result.Contains("ok"))
            {
                returnList rtndata = Json.JsonToEntity<returnList>(result);
                if (rtndata != null)
                {
                    if(rtndata.ok =="True")
                    {
                        MessageBox.Show("数据上传成功！");
                    }
                    //returnBody bdy = Json.JsonToEntity<returnBody>(rtndata.body.ToString());
                    //if (bdy != null)
                    //{
                    //    tokens = bdy.access_token;
                    //}
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dd = string.Format("{0}123","达元");
            Console.WriteLine(dd);
        }
    }
}
