using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using WorkstationDAL.Model;
using System.IO;
using WorkstationModel.UpData;

namespace WorkstationModel.UpData
{
    public class TYUpData
    {      
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static string Logon()
        {
            string addr = Global.ServerAdd+"?username=" +Global.ServerName+ "&password="+Global.ServerPassword+"&cmd=LOGIN";
            Uri uri = new Uri(addr);      
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            //webRequest.ContentType = "text/html;charset=UTF-8"; 
            webRequest.Timeout = 5000;
            string d = "";
            //响应
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                d = sr.ReadToEnd();
            }

            return d;
        }
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <returns></returns>
        public static string UpData(clsUpLoadData ud)
        {
            string data =Global.ServerAdd+"?username="+Global.ServerName
                +"&password="+Global.ServerPassword+"&cmd=GET_GCGL_LIST&userid="+Global.UserCode+"&qycode='"+Global.CompanyCode+"'";
             
            StringBuilder strb = new StringBuilder();

            strb.Append(data);
            strb.Append("&TestData=");
            strb.Append("[{");
            strb.Append("\"InspectionQuantity\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(ud.shuliang); //穿2条数据
            strb.Append("\"");
            strb.Append(",");
            strb.Append("\"DateTime\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(ud.ttime);//检测时间
            strb.Append("\"");
            strb.Append(",");
            strb.Append(" \"Operator\":");
            strb.Append("\"");
            strb.Append(ud.chker);//检测员
            strb.Append("\"");
            strb.Append(",");
            strb.Append("\"QualifiedRate\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(ud.hegelv);    
            strb.Append("\",");
            strb.Append("\"DetectionObject\":");
            strb.Append("\"");
            strb.Append(ud.duixiang);//检测对象
            strb.Append("\",");
            strb.Append("\"InhibitoryRate\":");
            strb.Append("\"");
            strb.Append(ud.chkdata);//抑制率
            strb.Append("\",");
            strb.Append("\"Qycode\":");
            strb.Append("\"");
            strb.Append(Global.CompanyCode); 
            strb.Append("\",");
            strb.Append("\"QualifiedIs\":");//是否合格
            strb.Append("\"");
            strb.Append(ud.Conclusion);
             strb.Append("\"");
            strb.Append("}]");

            Uri uri = new Uri(strb.ToString());
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentType = "text/html;charset=UTF-8"; 
            webRequest.Timeout = 5000;
            string d = "";
            //响应
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                d = sr.ReadToEnd();
            }

            return d;
        }
        public  struct  login
        {
            public string userid { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
            public string qycode { get; set; }
            //public string token { get; set; }      
        }
        public struct Upload
        {
            public string isSucess { get; set; }
        }
    }
}
