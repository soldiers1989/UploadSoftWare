using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.CodeDom;
using System.Reflection;
using System.Web.Services;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using FoodClient.AnHui;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FoodClient.shandong
{
    public class ShanDongUpData
    {
        #region updata
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
        /// <summary>
        /// 条形码
        /// </summary>
        public string barcode = "";
        /// <summary>
        /// 样品数量
        /// </summary>
        public string samplenum = "";
        /// <summary>
        /// 数量单位
        /// </summary>
        public string numunit = "";
        #endregion

        private static string code = "";
        private static string data = "";
        //名孚调用Web函数上传数据
        public static bool  InvokeAndCallWebService(ShanDongUpData sdd)
        {
            bool sendre = false;
            string prodate = "";            
            int numunitcode = GetChkUnitCode(sdd.numunit == "" ? "3" : sdd.numunit);

            if (sdd.GoodsDate.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                prodate = sdd.Chktime;
            }
            else 
            {
                prodate = sdd.GoodsDate.ToString();
            }
            code = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString("yyyyMMdd"), "MD5").ToLower();
            string xmldata = "<NewDataSet> " +
               "<Table>" +
               "<VResultCollectID>" + Global.RID + "</VResultCollectID>"
               + "<SPECIALSName>" + sdd.SampleName + "</SPECIALSName>"
               + "<USERS>" + sdd.Operator + "</USERS>"
               + "<RATE>" + sdd.InhibitionRatio + "</RATE> "
               + "<TIMEDO>" +sdd.Chktime + "</TIMEDO>"
               + "<UpLoadDate>" + DateTime.Now.ToString().Replace('/','-') + "</UpLoadDate>"
               + "<Producer>" + sdd.GoodsUnit + "</Producer>"
               + "<ProduceAddress>" + sdd.GoodsUnit + "</ProduceAddress>"
               + "<ProduceDate>" + prodate + "</ProduceDate>"
               + "<BarCode>" + sdd.barcode + "</BarCode>"
               + "<Company>" + sdd.SendUnit + "</Company>"
               + "<VRESULTID>" + (sdd.Conclusion =="合格"? 1 : 2) + "</VRESULTID>"
               + "<VegetableTypeName>" + sdd.SampleType + "</VegetableTypeName>"
               + "<HabitatName>" + sdd.GoodsUnit  + "</HabitatName>"
               + "<SONGJIANDATE>" + sdd.Chktime + "</SONGJIANDATE>"
               + "<SONGJIANBASENUM>" + float.Parse(sdd.samplenum==""?"1":sdd.samplenum) + "</SONGJIANBASENUM>"
               + "<UseUnitID>" + numunitcode + "</UseUnitID>"
               + "<SampleNo>" +sdd.SampleCode + "</SampleNo>"
               + "<DealWithResult>" + "已处理" + "</DealWithResult>"
               +"<ProviderType>"+"dayuan"+"</ProviderType>"
               + "</Table>"            
               + "</NewDataSet>";

            data = xmldata;
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode newNode = doc.DocumentElement;
            //XElement xElemen = XElement.Parse(data);
            //object[] args = new object[4];
            //args.SetValue("040090030001002835", 0);
            //args.SetValue("111111", 1);
            //args.SetValue(code, 2);
            //args.SetValue(newNode, 3);
            //引用服务
            //ServiceReferenceSD.MServiceSoapClient client = new ServiceReferenceSD.MServiceSoapClient();
            //string rt = client.SyncVResultCollectValidate(FoodClient.AnHui.Global.AnHuiInterface.userName, FoodClient.AnHui.Global.AnHuiInterface.passWord, code, xElemen);

            object[] args = new object[4];
            args.SetValue(FoodClient.AnHui.Global.AnHuiInterface.userName, 0);
            args.SetValue(FoodClient.AnHui.Global.AnHuiInterface.passWord, 1);
            args.SetValue(code, 2);
            args.SetValue(newNode, 3);
            //完全动态调用
            object ob = DynamicWeb.InvokeWebService(FoodClient.AnHui.Global.AnHuiInterface.ServerAddr, "SyncVResultCollectValidate", args);//2017/8/17修改
            string rt = ob.ToString();
            if (rt == "4")
            {
                Global.UploadCount = Global.UploadCount + 1;
                sendre = true;
            }
            //InvokeWebService(url, null, methodname, args);
            return sendre;
        }

        /// <summary>
        /// POST方式请求上传数据
        /// </summary>
        /// <param name="sdd"></param>
        /// <returns></returns>
        public static string PostWebService(ShanDongUpData sdd,out string errMsg)
        {
            errMsg = "";
            string sendre = "";
            string prodate = "";
            try
            { 
                int numunitcode = GetChkUnitCode(sdd.numunit == "" ? "3" : sdd.numunit);

                if (sdd.GoodsDate.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    prodate = sdd.Chktime;
                }
                else
                {
                    prodate = sdd.GoodsDate.ToString();
                }
                code = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString("yyyyMMdd"), "MD5").ToLower();
                string xmldata = "<NewDataSet> " +
                   "<Table>" +
                   "<VResultCollectID>" + Global.RID + "</VResultCollectID>"
                   + "<FarmID>"+Global.UserName+"</FarmID>"
                   + "<SPECIALSName>" + sdd.SampleName + "</SPECIALSName>"
                   + "<USERS>" + sdd.Operator + "</USERS>"
                   + "<RATE>" + sdd.InhibitionRatio + "</RATE> "
                   + "<TIMEDO>" + sdd.Chktime + "</TIMEDO>"
                   + "<UpLoadDate>" + DateTime.Now.ToString().Replace('/', '-') + "</UpLoadDate>"
                   + "<Producer>" + sdd.GoodsUnit + "</Producer>"
                   + "<ProduceAddress>" + sdd.GoodsUnit + "</ProduceAddress>"
                   + "<ProduceDate>" + prodate + "</ProduceDate>"
                   + "<BarCode>" + sdd.barcode + "</BarCode>"
                   + "<Company>" + sdd.SendUnit + "</Company>"
                   + "<VRESULTID>" + (sdd.Conclusion == "合格" ? 1 : 2) + "</VRESULTID> "
                   + "<VegetableTypeName>" + sdd.SampleType + "</VegetableTypeName>"
                   + "<HabitatName>" + sdd.GoodsUnit + "</HabitatName>"
                   + "<SONGJIANDATE>" + sdd.Chktime + "</SONGJIANDATE>"
                   + "<SONGJIANBASENUM>" + float.Parse(sdd.samplenum == "" ? "1" : sdd.samplenum) + "</SONGJIANBASENUM>"
                   + "<UseUnitID>" + numunitcode + "</UseUnitID>"
                   + "<SampleNo>" + sdd.SampleCode + "</SampleNo>"
                   + "<DealWithResult>" + "已处理" + "</DealWithResult>"
                   //+ "<ProviderType>" + "dayuan" + "</ProviderType>"
                   + "</Table>"
                   + "</NewDataSet>";
                string postdata = "";
                //data =gb2312_utf8(xmldata);
                for (int i = 0; i < xmldata.Length; i++)
                {
                    string t = xmldata.Substring(i, 1);
                    postdata = postdata + CheckStringChinese(t);
                }

                string urll = Global.WebAddr + "SyncMyzlCheckFoodData2005.php?" + "FarmID=" + Global.UserName + "&Pwd=" + Global.Password + "&Code=" + code + "&XmlCheck=" + postdata;
                //发起请求
                Uri uri = new Uri(urll);//     
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Timeout = 5000;
                //webRequest.ContentLength = paramBytes.Length;
                //StreamWriter requestStream = new StreamWriter(webRequest.GetRequestStream());
                //Stream requestStream = webRequest.GetRequestStream();
                //requestStream.Write(doc.InnerXml);
                string d = "";
                //响应
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    d = sr.ReadToEnd();
                }
                sendre = d;
            }
            catch (Exception ex)
            {
                errMsg =ex.Message;
            }
           
            return sendre;
        }
        /// <summary>
        /// POST请求验证用户及通信测试
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string PostResquestHttp(string RequestData,out string errMsg)
        {
            errMsg = "";
            string rtn = "";
            code = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString("yyyyMMdd"), "MD5").ToLower();
            try
            {
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(RequestData);
                //XmlNode newNode = doc.DocumentElement;
                //发起请求
                //string urr = Global.WebAddr + "SyncMyzlCheckFoodData2005.php?" + "FarmID=" + Global.UserName + "&Pwd=" + Global.Password + "&Code=" + code;
                string postdata = "";
                for (int i = 0; i < RequestData.Length; i++)
                {
                    string t = RequestData.Substring(i,1);
                    postdata=postdata+ CheckStringChinese(t);
                }
                string urr = Global.WebAddr + "SyncFarmsInfoList2005.php?" + "FarmsXMl=" + postdata;
                //Uri uri = new Uri(urr);     
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(urr);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Timeout = 5000;
                //webRequest.ContentLength = paramBytes.Length;
                 //StreamWriter requestStream = new StreamWriter(webRequest.GetRequestStream());
                //Stream requestStream = webRequest.GetRequestStream();
                //requestStream.Write(doc.InnerXml);
                string d = "";
                //响应 
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    d = sr.ReadToEnd();
                }
                rtn = d;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }
        public static string get_uft8(string unicodeString)
        {
            byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(unicodeString);
            string str = "";
            foreach (byte b in buffer) str += string.Format("%{0:X}", b);
            return str;
        }

        public static string CheckStringChinese(string text)
        {
            string rtn = "";
            bool res = false;
            if ( Regex.IsMatch(text,@"[\u4e00-\u9fbb]+$"))
            {
                res = true;
            }
            if (res == true)
            {
                rtn = get_uft8(text);
            }
            else
            {
                rtn = text;
            }
            return rtn;

 
        }

        /// <summary> 
        /// 山东名孚获取数量单位编号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetChkUnitCode(string str)
        {
            int td = 0;
            if (str == "斤")
            {
                td = 3;
            }
            else if (str == "两")
            {
                td = 4;
            }
            else if (str == "克")
            {
                td = 5;
            }
            else if (str == "毫克")
            {
                td = 6;
            }
            else if (str == "公斤")
            {
                td = 2;
            }
            else if (str == "吨")
            {
                td = 1;
            }
            else if (str == "立方米")
            {
                td = 7;
            }
            else if (str == "升")
            {
                td = 8;
            }
            else if (str == "立方分米")
            {
                td = 9;
            }
            else if (str == "毫升")
            {
                td = 10;
            }
            else if (str == "立方厘米")
            {
                td = 11;
            }
            else if (str == "英加仑")
            {
                td = 12;
            }
            else if (str == "美加仑")
            {
                td = 13;
            }

            return td;
        }
        /// <summary>
        /// 请求调用web函数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="classname"></param>
        /// <param name="methodname"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            if (classname == null || classname == "")
            {
                classname = GetClassName(url);
            }
            //获取服务描述语言(WSDL)   
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //生成客户端代理类代码   
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider icc = new CSharpCodeProvider();
            //CodeDomProvider icc = CodeDomProvider.CreateProvider("CSharp");  
            //ICodeCompiler icc = csc.CreateCompiler();
            //设定编译器的参数   
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类   
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            //生成代理实例,并调用方法   
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);
            var d = mi.Invoke(obj, args);
            return d;

        }

        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        /// <summary>
        /// json字符串转entity
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>返回转换后的entity</returns>
        public static T JsonToEntity<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
        }
        /// <summary>
        /// 打包XML数据
        /// </summary>
        /// <returns></returns>
        public static string getxml( ShanDongUpData sdu)
        {
            string xmldata = "<NewDataSet> " +
                "<Table>" +
                "<VResultCollectID>" + 1 + "</VResultCollectID>"
                + "<SPECIALSName>" + sdu.SampleName + "</SPECIALSName>"
                + " <USERS>" + "食安科技" + "</USERS>"
                + "<RATE>" + 5.2 + "</RATE> "
                + "<TIMEDO>" + "2017-07-18" + "</TIMEDO>"
                + "<UpLoadDate>" + "2017-07-18" + "</UpLoadDate>"
                + "<Producer>" + "广东达元" + "</Producer>"
                + "<ProduceAddress>" + "广州开源大道" + "</ProduceAddress>"
                + "<ProduceDate>" + "2017-05-18" + "</ProduceDate>"
                + "<BarCode>" + "201707181134" + " </BarCode>"
                + "<Company>" + "广州农场" + "</Company>"
                + "<VRESULTID>" + 1 + "</VRESULTID> "
                + "<VegetableTypeName>" + "蔬菜" + "</VegetableTypeName>"
                + "<HabitatName>" + "广东" + "</HabitatName>"
                + "<SONGJIANDATE>" + "2017-05-15" + "</SONGJIANDATE>"
                + "<SONGJIANBASENUM>" + 30 + "</SONGJIANBASENUM>"
                + "<UseUnitID>" + 1 + "</UseUnitID>"
                + "<SampleNo>" + "01234567" + "</SampleNo>"
                + "<DealWithResult>" + "已处理" + "</DealWithResult>"
                + "</Table>"
                  + "<Table>"
                  + "<VResultCollectID>" + "{" + code + "}" + "</VResultCollectID>"
                  + "<FarmID>" + "040090030001002835" + "</FarmID>"
                  + "</Table>"
                + "</NewDataSet>";
            return xmldata;
        }
    }
    public class ResultData
    {
        private string _message = "";
        public string message
        {
            set
            {
                _message = value;
            }
            get
            {
                return _message;
            } 
        }
        private string _status = "";
        public string status
        {
            set
            {
                _status = value;
            }
            get
            {
                return _status;
            } 
        }
        private string _data = "";
        public string data 
        {
            set
            {
                _data = value;
            }
            get
            {
                return _data;
            }
        }
    }
}
