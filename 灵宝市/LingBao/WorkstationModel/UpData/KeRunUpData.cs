using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using WorkstationDAL.Model;
using System.Web;
using System.Security.Policy;

namespace WorkstationModel.UpData
{
    /// <summary>
    /// 山东科润接口  
    /// 数据上传
    /// </summary>
    public class KeRunUpData
    {
        /// <summary>
        /// GET方式上传数据
        /// </summary>
        /// <param name="udata"></param>
        /// <returns></returns>
        public static string UpData(clsUpLoadData udata)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(Global.ServerAdd);
            strb.Append("?did=");
            strb.Append(udata.shebeiID);
            strb.Append("&jczbh=");
            strb.Append(Global.DetectUnitNo); //检测站编号
            strb.Append("&jcdw=");
            strb.Append(udata.detectunit); //检测站
            strb.Append("&rwbh=");
            strb.Append("06");
            strb.Append("&bjdw=");
            strb.Append(udata.checkunit);
            strb.Append("&jcxm=");
            strb.Append(udata.checkitem);
            strb.Append("&jcdt=");
            strb.Append(udata.ttime);
            strb.Append("&jcz=");
            strb.Append(udata.chkdata);
            strb.Append("&szdw=");
            strb.Append(udata.unit);
            strb.Append("&jgpd=");
            strb.Append(udata.Conclusion);
            strb.Append("&ybbh=");
            strb.Append(udata.samplenumber);
            strb.Append("&ybmc=");
            strb.Append(udata.samplename);
            strb.Append("&ybcd=");
            strb.Append(udata.sampleOrigin);
            strb.Append("&xlbz=");
            strb.Append(udata.testbase);
            strb.Append("&sbbh=");
            strb.Append(udata.shebeiID);
            strb.Append("&xlz=");
            strb.Append(udata.standvalue);
            strb.Append("&jcy=");
            strb.Append(udata.chker);
            strb.Append("&type=");
            strb.Append(udata.uptype);

            //string url = System.Web.HttpUtility.UrlEncode(strb.ToString());
            
            Uri uri = new Uri(strb.ToString());

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentType = "text/html;charset=UTF-8";
            webRequest.Timeout = 5000;
            string d = "";
            //获取响应
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                d = sr.ReadToEnd();
            }
            return d;

        }
        /// <summary>
        /// 服务器返回信息
        /// </summary>
        public struct ReturnInfo
        {
            public string status { get; set; }
            public string info { get; set; }
 
        }
    }
}
