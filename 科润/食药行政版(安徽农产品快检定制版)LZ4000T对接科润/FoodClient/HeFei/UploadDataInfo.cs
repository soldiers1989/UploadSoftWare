using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

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
        /// 检测编号
        /// </summary>
        public string CHknum = "";
        /// <summary>
        /// 抑制率
        /// </summary>
        public float InhibitionRatio = 0.0f;
        /// <summary>
        /// 数值单位
        /// </summary>
        public string unit = "";
        /// <summary>
        /// 限定值
        /// </summary>
        public string limitdata = "";
        /// <summary>
        /// 检测员
        /// </summary>
        public string Operator = "";
        /// <summary>
        /// 检测仪器
        /// </summary>
        public string ChkMachine = "";
        /// <summary>
        /// 检测依据
        /// </summary>
        public string testbase = "";
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

        public static bool Upload(HeFeiUploadDataInfo data)
        {
            int type = 0;
            if (FoodClient.AnHui.Global.AnHuiInterface.UploadType == "基地上传")
            {
                type = 1;
            }
            else 
            {
                type = 2;
            }

            StringBuilder strb = new StringBuilder();
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.ServerAddr);
            strb.Append("?did=");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.SheBeiID);
            strb.Append("&jczbh=");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.Chkbianhao);//检测站编号
            strb.Append("&jcdw=");
            strb.Append(FoodClient.AnHui.Global.AnHuiInterface.Chkdanwei);
            strb.Append("&rwbh=");
            strb.Append("100011");
            strb.Append("&bjdw=");
            strb.Append(data.GoodsUnit);
            strb.Append("&jcxm=");
            strb.Append(data.ChkItem);
            strb.Append("&jcdt=");
            strb.Append(data.Chktime);
            strb.Append("&jcz=");
            strb.Append(data.InhibitionRatio);
            strb.Append("&szdw=");
            strb.Append(data.unit);
            strb.Append("&jgpd=");
            strb.Append(data.Conclusion);
            strb.Append("&ybbh=");
            strb.Append(data.SampleCode);
            strb.Append("&ybmc=");
            strb.Append(data.SampleName);
            strb.Append("&ybcd=");
            strb.Append(data.ProductionUnit);
            strb.Append("&xlbz=");
            strb.Append(data.testbase);
            strb.Append("&sbbh=");
            strb.Append("LZ4000T");
            strb.Append("&xlz=");
            strb.Append(data.limitdata);
            strb.Append("&jcy=");
            strb.Append(data.Operator);
            strb.Append("&type=");
            strb.Append(type);

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
            JavaScriptSerializer jsup = new JavaScriptSerializer();
            ReturnInfo retu = jsup.Deserialize<ReturnInfo>(d); //将json数据转化为对象类型并赋值给list

            if (retu.status == "1" && retu.info == "success")
            {
                FoodClient.AnHui.Global.UploadCount = FoodClient.AnHui.Global.UploadCount + 1;//记录上传成功的数据
            }
            else 
            {
                FoodClient.AnHui.Global.errInfo = FoodClient.AnHui.Global.errInfo + retu.info;
            }
            return webResponse.StatusCode.Equals(HttpStatusCode.OK);
        }

        /// <summary>
        /// 服务器返回信息
        /// </summary>
        public struct ReturnInfo
        {
            public string status { get; set; }
            public string info { get; set; }

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