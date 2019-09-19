﻿using System;
using System.IO;
using System.Net;
using System.Text;


namespace FoodClient.HeFei
{
    public class HeFeiUploadDataInfo
    {
        #region Entity
        /// <summary>
        /// 样品类别
        /// </summary>
        public string SampleType = "";
        /// <summary>
        /// 样品名称
        /// </summary>
        public string SampleName = "";
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SampleCode = "";
        /// <summary>
        /// 送检单位
        /// </summary>
        public string SendUnit = "";
        /// <summary>
        /// 生产单位
        /// </summary>
        public string ProductionUnit = "";
        /// <summary>
        /// 产品生产单位
        /// </summary>
        public string GoodsUnit = "";
        /// <summary>
        /// 检测单位
        /// </summary>
        public string CheckUnit = "";
        /// <summary>
        /// 检测单位编号
        /// </summary>
        public string CheckUnitCode = "";
        /// <summary>
        /// 产品生产日期
        /// </summary>
        public DateTime GoodsDate { get; set; }
        /// <summary>
        /// 抑制率
        /// </summary>
        public float InhibitionRatio = 0.0f;
        /// <summary>
        /// 检测员
        /// </summary>
        public string Operator = "";
        /// <summary>
        /// 检测类别
        /// </summary>
        public string leibie = "";
        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime ProductionDate { get; set; }
        /// <summary>
        /// 产品是否合格
        /// </summary>
        public bool IsOk = false;
        /// <summary>
        /// 检测时间
        /// </summary>
        public string Chktime = "";
        /// <summary>
        /// 检测项目
        /// </summary>
        public string ChkItem = "";
        /// <summary>
        /// 检测结论
        /// </summary>
        public string Conclusion = "";
       
        #endregion

        public static string uperr = "";

        public static bool Upload(HeFeiUploadDataInfo data)
        {
            bool isseend = false;
            //发起请求
            Uri uri = new Uri(FoodClient.AnHui.Global.AnHuiInterface.ServerAddr);//FoodClient.AnHui.Global.AnHuiInterface.ServerAddr        
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Timeout = 5000;
            StringBuilder strb = new StringBuilder();
            string chkdate = string.Empty;
            chkdate = data.Chktime.ToString();
            string[] cd = chkdate.Split(' ');
            string dt = cd[0];
            if (!dt.Contains("-"))
            {
                dt.Replace('/', '-');
                //string[] t = dt.Split('/');
                //dt = t[0] + "-" + t[1] + "-" + t[2];
            }

            strb.Append("{");
            strb.Append("\"username\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.userName);
            strb.Append("\"");
            strb.Append(",");
            strb.Append("\"password\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.passWord);
            strb.Append("\"");
            strb.Append(",");
            strb.Append(" \"dwbh\":");
            strb.Append("\"");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.instrumentNo);//单位编号
            strb.Append("\"");
            strb.Append(",");
            strb.Append("\"dwmc\"");
            strb.Append(":");
            strb.Append("\"");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.instrument);//单位名称
            strb.Append("\"");
            strb.Append(",");
            strb.Append("\"details\"");
            strb.Append(":");
            strb.Append("[{");
            strb.Append("\"yangpinbianhao\":");
            strb.Append("\"");
            strb.Append(data.SampleCode);
            strb.Append("\"");
            strb.Append(",\"yangpinmingcheng\":");
            strb.Append("\"");
            strb.Append(data.SampleName);
            strb.Append("\",");
            strb.Append("\"shoujiandanwei\":");
            strb.Append("\"");
            strb.Append(data.GoodsUnit);//受检单位
            strb.Append("\",");
            strb.Append("\"jianceriqi\":");
            strb.Append("\"");
            strb.Append(dt);//data.Chktime
            strb.Append("\",");
            strb.Append("\"jiancexiangmu\":");
            strb.Append("\"");
            strb.Append(data.ChkItem);
            strb.Append("\",");
            strb.Append("\"jiancejieguo\":");
            strb.Append("\"");
            strb.Append(data.InhibitionRatio);
            strb.Append("\",");
            strb.Append("\"jianyanjielun\":");
            strb.Append("\"");
            strb.Append(data.Conclusion);
            strb.Append("\",");
            strb.Append("\"jianceren\":");
            strb.Append("\"");
            strb.Append(data.Operator);
            strb.Append("\",");
            strb.AppendFormat("\"jianyanleibie\":\"{0}\"", data.leibie );
            strb.Append("}]}");

            //string str = "{ \"username\":\"test\", \"password\":\"bozhou\", \"dwbh\":\"341602100001\",\"dwmc\":\"测试单位\",\"details\":[{\"yangpinbianhao\":\"201504081349114\",\"yangpinmingcheng\":\"白菜\",\"shoujiandanwei\":\"超市\",\"jianceriqi\":\"2017-3-8\",\"jiancexiangmu\":\"农药残留\",\"jiancejieguo\":\"4.8%\",\"jianyanjielun\":\"合格\",\"jianceren\":\"黄灯\"}]}";
            //string str = "{ \"username\":\"test\", \"password\":\"bozhou\", \"dwbh\":\"341602100001\",\"dwmc\":\"测试单位\",\"details\":[{\"yangpinbianhao\":\"201504081349114\",\"yangpinmingcheng\":\"白菜\",\"shoujiandanwei\":\"超市\",\"jianceriqi\":\"2017-3-8\",\"jiancexiangmu\":\"农药残留\",\"jiancejieguo\":\"4.8%\",\"jianyanjielun\":\"合格\",\"jianceren\":\"黄灯\"}]}";
            byte[] paramBytes = Encoding.UTF8.GetBytes(strb.ToString());
            webRequest.ContentLength = paramBytes.Length;
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(paramBytes, 0, paramBytes.Length);
            string d = "";
            //响应
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                d = sr.ReadToEnd();
            }
            if (d == "{flag:1}")
            {
                FoodClient.AnHui.Global.AnHuiInterface.isok = FoodClient.AnHui.Global.AnHuiInterface.isok + 1;
                isseend = true;
            }
            else
            {
                if (d == "{flag:5}")
                {
                    uperr = "用户名或密码错误";
                }
                else if (d == "{flag:6}")
                {
                    uperr = "其它错误，可能是数字、日期错误";
                }
                else
                {
                    uperr =d;
                }
                
            }
            return isseend;
        }

        [Serializable]

        public class JSONHelper
        {
            //public static string Serialize<T>(T obj)
            //{
            //    System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            //    MemoryStream ms = new MemoryStream();
            //    serializer.WriteObject(ms, obj);
            //    string retVal = Encoding.UTF8.GetString(ms.ToArray());
            //    ms.Dispose();
            //    return retVal;
            //}
            //Dictionary<string, object> dt = Resolve.DataRowFromJSON(requestbuffer, context);
            //public static T Deserialize<T>(string json)
            //{
            //    T obj = Activator.CreateInstance<T>();
            //    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            //    System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            //    obj = (T)serializer.ReadObject(ms);
            //    ms.Close();
            //    ms.Dispose();
            //    return obj;
            //}
        }
        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string decode = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                decode = Convert.ToBase64String(bytes);
            }
            catch
            {
                return decode;
            }
            return decode;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }

    }
}