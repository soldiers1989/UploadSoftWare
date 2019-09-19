using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WorkstationModel.UpData;
using WorkstationDAL.Model;
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

namespace WorkstationModel.shandong
{
    public class SDUpdata
    {
        #region updata
        /// <summary>
        /// 检测编号
        /// </summary>
        public string CheckNumber = string.Empty;
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
        /// 生产企业
        /// </summary>
        public string ProductionUnit = "";
        /// <summary>
        /// 产地
        /// </summary>
        public string ProductPlace = "";
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
        public DateTime Chktime ;
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
        /// <summary>
        /// 产地地址
        /// </summary>
        public string ProductAddr = "";
        /// <summary>
        /// 送检日期
        /// </summary>
        public DateTime  SendTime ;
        #endregion

        private static string code = "";
        private static string data = "";

        public static bool InvokeAndCallWebService(SDUpdata sdd)
        {
            bool sendre = false;
            Global.ReturnMessage = "";
            int numunitcode = GetChkUnitCode(sdd.numunit == "" ? "3" : sdd.numunit);

            code = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString("yyyyMMdd"), "MD5").ToLower();
    
            //测试

            string xmldata = "<NewDataSet> " +
               "<Table>" +
               "<VResultCollectID>" + sdd.CheckNumber + "</VResultCollectID>"
               + "<FarmID>" + Global.ServerName + "</FarmID>"
               + " <SPECIALSName>" + sdd.SampleName + "</SPECIALSName>"
               + "<USERS>" + sdd.Operator + "</USERS> "
               + "<RATE >" + sdd.InhibitionRatio + "</RATE >"
               + "<TIMEDO >" + sdd.Chktime.ToString("yyyy-MM-dd HH:mm:ss") + "</TIMEDO >"
               + "<UpLoadDate >" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</UpLoadDate >"
               + "<Producer>" + sdd.GoodsUnit  + "</Producer>"
               + "<ProduceAddress>" + sdd.ProductAddr + "</ProduceAddress>"
               + "<ProduceDate>" + sdd.ProductionDate.ToString("yyyy-MM-dd HH:mm:ss") + " </ProduceDate>"
               + "<BarCode>" + sdd.barcode + "</BarCode>"
               + "<Company >" + sdd.ProductionUnit + "</Company > "
               + "<VRESULTID >" + (sdd.Conclusion == "合格" ? 1 : 2) + "</VRESULTID >"
               + "<VegetableTypeName>" + sdd.SampleType + "</VegetableTypeName>"
               + "<HabitatName>" + sdd.ProductPlace  + "</HabitatName>"
               + "<SONGJIANDATE>" + sdd.SendTime.ToString("yyyy-MM-dd HH:mm:ss") + "</SONGJIANDATE>"
               + "<SONGJIANBASENUM>" + sdd.samplenum + "</SONGJIANBASENUM>"
               + "<UseUnitID>" + numunitcode + "</UseUnitID>"
               + "<SampleNo>" + sdd.SampleCode + "</SampleNo>"
               + "<DealWithResult>" + "已处理" + "</DealWithResult>"
               + "</Table>"
               + "</NewDataSet>";

            data = xmldata;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode newNode = doc.DocumentElement;

            object[] args = new object[4];

            args.SetValue(Global.ServerName, 0);//用户名
            args.SetValue(Global.ServerPassword, 1);//密码
            args.SetValue(code, 2);
            args.SetValue(newNode, 3);
            //args.SetValue("", 4);
            //完全动态调用
            object ob = DynamicWeb.InvokeWebService(Global.ServerAdd, "SyncMyzlCheckFoodData2005", args);
            string rt = ob.ToString();
            if (rt == "4")
            {
                sendre = true;
            }
            else
            {
                Global.ReturnMessage = rt;
            }
            //InvokeWebService(url, null, methodname, args);
            return sendre;
        }
        /// <summary>
        /// 山东名孚获取数量单位编号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetChkUnitCode(string str)
        {
            int td = 0;
            if (str == "吨")
            {
                td = 1;
            }
            else if (str == "公斤")
            {
                td = 2;
            }
            else if (str == "斤")
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
        /// 同步检测单位信息
        /// </summary>
        /// <returns></returns>
        public static bool UpUnit(string unitinfo)
        {
            bool rtnu = false;
            object[] argsu = new object[1];
            argsu.SetValue(unitinfo, 0);
            object ob = DynamicWeb.InvokeWebService(Global.ServerAdd, "SyncFarmsInfoList2005", argsu);
            string rt = ob.ToString();
            if (rt == "success")
            {
                rtnu = true;
            }
            return rtnu;
        }
    }
}
