using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO.Ports;
using System.Threading;
using System.Security;
using System.Web.Security;
using WorkstationDAL.UpLoadData;
using System.Runtime.InteropServices;
using System.Management;
using WorkstationDAL.AnHui;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace WorkstationDAL.Model
{
    public class Global
    {
        //导入判断网络是否连接的 .dll  
       [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]  
       //判断网络状况的方法,返回值true为连接，false为未连接  
       public extern static bool InternetGetConnectedState(out int conState, int reder);
        /// <summary>
        /// 网络连接
        /// </summary>
        /// <returns></returns>
       public static bool linkNet()
       {
           bool islink = false;
           int n = 0;
           if (InternetGetConnectedState(out n, 0))
           {
               islink=true;
           }
           else
           {
               islink =false ;
           }
           return islink;
       }
        /// <summary>
        /// 测试项目
        /// </summary>
        public static string TestItem = string.Empty;
        /// <summary>
        /// 选择样品名称
        /// </summary>
        public static string iSampleName = string.Empty;
        /// <summary>
        /// 记录发送数据
        /// </summary>
        public static string SendData = string.Empty;
        /// <summary>
        /// 测试仪器
        /// </summary>
        public static string ChkManchine = string.Empty;//测试仪器
       
        /// <summary>
        /// 检测仪器编号
        /// </summary>
        public static string MachineNum = "";
        /// <summary>
        /// 仪器系列号
        /// </summary>
        public static string MachineSerialCode = "";

        public static bool ComON = false;    //判断COM口是否打开
        #region 记录按钮单击事件,切换主页背景突出显示
        public static bool MainPage=false ;    //主页
        public static bool SearchData = false; //数据采集
        public static bool Dairypb = false;    //操作日记
        public static bool SysSet = false;     //系统设计
        public static bool Shop = false;//商城
        public static bool Task = false;//任务管理

        #endregion
        /// <summary>
        /// 设备类型
        /// </summary>
        public static string deviceType = string.Empty;
        /// <summary>
        /// 查询数据起始时间
        /// </summary>
        public static string firsttime = string.Empty;
        /// <summary>
        /// 查询数据结束时间
        /// </summary>
        public static string lasttime = string.Empty;

        /// <summary>
        /// 检测编号
        /// </summary>
        public static string bianhao = string.Empty;
        /// <summary>
        /// 检测项目
        /// </summary>
        public static string Chkxiangmu = string.Empty;
        /// <summary>
        /// 检测时间
        /// </summary>
        public static DateTime ChkTime;
        /// <summary>
        /// 样品名称
        /// </summary>
        public static string ChkSample = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public static string GetSampTime = string.Empty;
        /// <summary>
        ///  采样地点
        /// </summary>
        public static string GetSampPlace = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public static string plannumber = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        public static string Chktestbase = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public static string ChklimitData = string.Empty;
        /// <summary>
        /// 检测人员
        /// </summary>
        public static string ChkPeople = string.Empty;
        public static string CloseCOM = "";
        /// <summary>
        /// 北海样品
        /// </summary>
        public static string Bsample = "";
        /// <summary>
        /// 北海样品ID
        /// </summary>
        public static string BsampleID="";
        /// <summary>
        /// 记录ID
        /// </summary>
        public static string BID = "";
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string BcheckCommpany = "";
        /// <summary>
        /// 快检服务通信测试返回的TOken
        /// </summary>
        public static string Token = "";

        public static string d_depart_name = "";
        public static string depart_id = "";
        public static string p_point_name = "";
        public static string point_id = "";
        public static string user_name = "";
        public static string id = "";
        public static string realname = "";
        public static string pointNum = string.Empty, pointName = string.Empty;
        /// <summary>
        /// 仪器型号
        /// </summary>
        public static string MachineModel = "";
        /// <summary>
        /// 食品种类
        /// </summary>
        public static string AH_FoodType = "";

   
        /// <summary>
        /// 快检服务获取MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMACComputer()
        {
            //string rtn="";
            string mac = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac += mo["MacAddress"].ToString() + " ";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return mac;
        }
        //串口设置
        public static string SerialCOM = string.Empty;//串口号
        public static string SerialBaud = string.Empty;//波特率
        public static string SerialData = string.Empty;//数据位
        public static string SerialStop = string.Empty; //停止位
        public static string SerialParity = string.Empty;//校验位
        public static string tagName = string.Empty;
        public static IList<clsCheckData> _checkDatas = null;
        public delegate void InvokeDelegate(DataTable dtbl);
        public static string _strData = string.Empty, _settingType = string.Empty;
        //public static SerialPort  sp = new SerialPort ();
        public static byte btDeviceTime = 0x20, btSN = 0x18, btCheckTime = 0x1A, btProductRead = 0x14, btProductSetting = 0x16,
           btPrint = 0x1C, btWiFi = 0x22, btBluetooth = 0x18, btServer = 0x26, btEthernet = 0x24, getDeviceModel = 0x00, btCheckedUnitRead = 0x40;
        public static bool _IsReadOver = false;
        public static IList<clsProduct> _products = null;
        public static IList<clsCheckedUnit> _checkedUnits = null;
        public static clsProduct _newProduct = null;
        public static clsCheckedUnit _newCheckedUnit = null;
        public static int _selIndex = -1;
        //串口通信
        public static SerialPort port_com = new SerialPort();
        /// <summary>
        /// HID通信的VID
        /// </summary>
        public static string hid_vid = string.Empty;
        /// <summary>
        /// HID通信的PID
        /// </summary>
        public static string hid_pid = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public static string getsampletime = string.Empty;
        /// <summary>
        /// 采样地点
        /// </summary>
        public static string getsampleaddress = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public static string plannum = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        public static string testbase = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public static string limitdata = string.Empty;
        /// <summary>
        /// 检测员
        /// </summary>
        public static string tester = string.Empty;
        /// <summary>
        /// 审查员
        /// </summary>
        public static string retester = string.Empty;
        /// <summary>
        /// 主管
        /// </summary>
        public static string Manage = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string CheckedUnit = string.Empty;
        /// <summary>
        /// 读取数据后表的记录数
        /// </summary>
        public static int TableRowNum = 0;
        /// <summary>
        /// 新添加的数据
        /// </summary>
        public static string[,] AddItem = null;
        /// <summary>
        /// 保存时编辑数据
        /// </summary>
        public static string[,] EditorSave = null;
        /// <summary>
        /// 测试仪器
        /// </summary>
        public static string[,] TestInstrument;
        /// <summary>
        /// 任务管理菜单
        /// </summary>
        public static string[,] ReceiveTask = { { "SampleTask", "抽样任务" }, { "DetectTask", "检测任务" } };
        /// <summary>
        /// 数据中心菜单
        /// </summary>
        public static string[,] dataCenter = { { "DataSearch", "数据查询" }, { "DataStatic", "统计分析" }, { "UnitCompany", "单位/企业" } ,
        { "SampleInfo", "样品信息" }  ,{ "DataBackup", "数据备份" }  ,       { "DataReturn", "数据恢复" }};

        public static string[,] SystemSet = { { "MachineManage", "仪器管理" }, { "InternetSet", "网络设置" }, { "SysDairy", "操作日记" }, { "UserManage", "用户管理" }
                                            , { "SysHelp", "系统帮助" }, { "SysAbout", "关于系统" }, { "SysUpdate", "系统升级" }, { "SysCancel", "注销用户" }};
        /// <summary>
        /// 登录用户
        /// </summary>
        public static string[,] edituser = new string[1,4];
        /// <summary>
        /// 仪器管理
        /// </summary>
        public static string[,] ediIntrument=new string[1,6];
        /// <summary>
        /// 窗体最大化
        /// </summary>
        public static bool maxwindow = false;
        /// <summary>
        /// 当前窗体
        /// </summary>
        public static string currentform = string.Empty;
        /// <summary>
        ///判断是修改哪里的数据
        /// </summary>
        public static string newdata = string.Empty;
        /// <summary>
        /// 当前登录用户名称
        /// </summary>
        public static string userlog = string.Empty;

        //机构信息
        /// <summary>
        /// 检测单位名称
        /// </summary>
        public static string TestUnitName = string.Empty;
        /// <summary>
        /// 检测单位地址
        /// </summary>
        public static string TestUnitAddr = string.Empty;
        /// <summary>
        /// 被检单位地址
        /// </summary>
        public static string DetectUnitName = string.Empty;
        /// <summary>
        /// 采样地址
        /// </summary>
        public static string SampleAddress = string.Empty;
        /// <summary>
        /// 进货数量
        /// </summary>
        public static string StockInNum = string.Empty;
        /// <summary>
        /// 采样数量
        /// </summary>
        public static string SampleNum = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public static string GetTime = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        public static string CheckBase = string.Empty;
        /// <summary>
        /// 修改检测单位信息的数组
        /// </summary>
        public static string[,] repairunit = null;
        /// <summary>
        /// 修改样品数组
        /// </summary>
        public static string[,] repairSample = null;
        /// <summary>
        /// 打开串口
        /// </summary>
        public static bool IsConnect = false;
        /// <summary>
        /// 加载被检单位、地址等信息
        /// </summary>
        public static string[,] ChkInfo = new string[1,3];
        /// <summary>
        /// 发送给泰扬设备的数据
        /// </summary>
        public static string SendToTYData = "";
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string ServerAdd = "";
        /// <summary>
        /// 服务器登录用户名
        /// </summary>
        public static string ServerName = "";
        /// <summary>
        /// 服务登录密码
        /// </summary>
        public static string ServerPassword = "";
        /// <summary>
        /// 数据上传   企业编码
        /// </summary>
        public static string CompanyCode = "";
        /// <summary>
        /// 数据上传   用户编码
        /// </summary>
        public static string UserCode = "";
        /// <summary>
        /// 检测单位
        /// </summary>
        public static string DetectUnit = "";
        /// <summary>
        /// 检测站编号
        /// </summary>
        public static string DetectUnitNo = "";
        public static string CheckUnitcode = "";
        /// <summary>
        /// 上传类型
        /// </summary>
        public static string UploadType = "";
        /// <summary>
        /// 机构名称
        /// </summary>
        public static string OrganizeName = "";
        /// <summary>
        /// 机构编号
        /// </summary>
        public static string OrganizeNo = "";
        /// <summary>
        /// 检测站类型
        /// </summary>
        public static string DetectUnitType = "";
        /// <summary>
        /// 用户昵称
        /// </summary>
        public static string NickName = "";
        /// <summary>
        /// 检测点ID
        /// </summary>
        public static string pointID = "";
        /// <summary>
        /// 设备ID
        /// </summary>
        public static string IntrumentNum = "";
        /// <summary>
        /// 获取抽样任务
        /// </summary>
        public static string GetSampleTask = "recevieSample";
        /// <summary>
        /// 服务器验证信息
        /// </summary>
        public static checkUserConnect ServiceConnect = null;
        /// <summary>
        /// 下载样品检测项目和对应检测标准
        /// </summary>
        public static string DownLoadItemAndStandard = "itemAndStandard";
        /// <summary>
        /// 仪器制造厂家
        /// </summary>
        public static string IntrumManifacture = "";
        /// <summary>
        /// 平台厂家
        /// </summary>
        public static string Platform = "";
        /// <summary>
        /// 仪器类型
        /// </summary>
        public static string InstrumentType = "";
        /// <summary>
        /// 仪器编号
        /// </summary>
        public static string IntrumentNums = "";
        /// <summary>
        /// MAC地址
        /// </summary>
        public static string MacAddr = "";
        /// <summary>
        /// 接口版本
        /// </summary>
        public static string InterfaceVer = "";

        #region 阳谷县企业信息
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string tCompany = "";
        /// <summary>
        /// 上传返回信息
        /// </summary>
        public static string ReturnMessage = "";
        /// <summary>
        /// 生产单位
        /// </summary>
        public static string shengchandanwei = "";
        /// <summary>
        /// 产地地址
        /// </summary>
        public static string chandidizhi = "";
        /// <summary>
        /// 生产企业
        /// </summary>
        public static string shengchanqiye = "";
        /// <summary>
        /// 产地
        /// </summary>
        public static string chandi = "";
        /// <summary>
        /// 检测员
        /// </summary>
        public static string jianceyuan = "";
        #endregion 

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5(string password)
        {
            try
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString();
            }
            catch (Exception)
            {
                return password;
            }
        }
        /// <summary>
        /// 生成GUID
        /// type为null时返回格式：9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// type为"N"时返回格式：e0a953c3ee6040eaa9fae2b667060e09
        /// type为"D"时返回格式：9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// type为"B"时返回格式：{734fd453-a4f8-4c5d-9c98-3fe2d7079760}
        /// type为"P"时返回格式：(ade24d16-db0f-40af-8794-1e08e2040df3)
        /// type为"X"时返回格式：{0x3fa412e3,0x8356,0x428f,{0xaa,0x34,0xb7,0x40,0xda,0xaf,0x45,0x6f}}
        /// </summary>
        /// <param name="format">GUID格式</param>
        /// <param name="strCase">大小写，0为小写，1为大写，不为0或1时默认小写</param>
        /// <returns>返回GUID</returns>
        public static string GUID(string format = null, int strCase = 1)
        {
            string guid = string.Empty;
            try
            {
                guid = format != null && ((format.Equals("N") || format.Equals("D") || format.Equals("B") || format.Equals("P") || format.Equals("X"))) ? Guid.NewGuid().ToString(format) : Guid.NewGuid().ToString();
            }
            catch (Exception)
            {
                guid = string.Empty;
            }
            return strCase == 1 ? guid.ToUpper() : guid.ToLower();
        }
        /// <summary>
        /// 生成GUID
        /// type为null时返回格式：9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// type为"N"时返回格式：e0a953c3ee6040eaa9fae2b667060e09
        /// type为"D"时返回格式：9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// type为"B"时返回格式：{734fd453-a4f8-4c5d-9c98-3fe2d7079760}
        /// type为"P"时返回格式：(ade24d16-db0f-40af-8794-1e08e2040df3)
        /// type为"X"时返回格式：{0x3fa412e3,0x8356,0x428f,{0xaa,0x34,0xb7,0x40,0xda,0xaf,0x45,0x6f}}
        /// </summary>
        /// <param name="format">GUID格式</param>
        /// <param name="strCase">大小写，0为小写，1为大写，不为0或1时默认小写</param>
        /// <returns></returns>
        public static string GETGUID(string format, int strCase = 1)
        {
            string guid = string.Empty;
            if (format != null)
            {
                if (format.Equals("N") || format.Equals("D") || format.Equals("B") || format.Equals("P") || format.Equals("X"))
                {
                    guid = Guid.NewGuid().ToString(format);
                }
                else
                {
                    guid = Guid.NewGuid().ToString();
                }
            }
            else
            {
                guid = Guid.NewGuid().ToString();
            }

            if (strCase == 1)
                guid = guid.ToUpper();
            else
                guid = guid.ToLower();

            return guid;
        }

        /// <summary>
        /// 下载被检单位
        /// </summary>
        public static string DownLoadCompany = "company";
        /// <summary>
        /// 下载检测样品数据
        /// </summary>
        public static string DownLoadSample = "simple";
        /// <summary>
        /// 定时40S恢复数据读取按钮
        /// </summary>
        public static System.Timers.Timer  Rdt=new System.Timers.Timer(40000);
        /// <summary>
        /// 发送时为True，判断第一个字节的数据是否为帧头
        /// </summary>
        public static bool SendFirst = false;
        /// <summary>
        /// 发送通信方式
        /// </summary>
        public static string communicatType = "";

        public static class AnHuiInterface
        {
            public static List<data_dictionary> data_dictionaryList = null;
            public static List<checked_unit> checked_unitList = null;
            public static List<standard_limit> standard_limitList = null;
            public static string userName = string.Empty;
            public static string passWord = string.Empty;
            public static string interfaceVersion = string.Empty;
            public static string instrument = string.Empty;
            public static string instrumentNo = string.Empty;
            public static string ServerAddr = string.Empty;

            /// <summary>
            /// 安徽省项目 - 字典接口 
            /// </summary>
            /// <returns></returns>
            public static string instrumentDictionaryHandle(clsInstrumentInfoHandle model)
            {
                StringBuilder soap = new StringBuilder();
                soap.AppendFormat("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ins=\"http://www.zhiyunda.com/service/instrumentDockingService\" xmlns:zyd=\"http://www.zhiyunda.com/zydjcy\">");
                soap.AppendFormat("<soapenv:Header>");
                soap.AppendFormat("<ins:interfaceVersion>{0}</ins:interfaceVersion>", model.interfaceVersion);
                soap.AppendFormat("<ins:key>{0}</ins:key>", model.key);
                soap.AppendFormat("<ins:userName>{0}</ins:userName>", model.userName);
                soap.AppendFormat("</soapenv:Header>");
                soap.AppendFormat("<soapenv:Body>");
                soap.AppendFormat("<zyd:instrumentDictionaryRequest>");
                soap.AppendFormat("<zyd:instrument>{0}</zyd:instrument>", model.instrument);
                soap.AppendFormat("<zyd:instrumentNo>{0}</zyd:instrumentNo>", model.instrumentNo);
                soap.AppendFormat("<zyd:mac>{0}</zyd:mac>", model.mac);
                soap.AppendFormat("<zyd:tableData>data_dictionary:{0};checked_unit:{1};standard_limit:{2};</zyd:tableData>", model.tableData, model.tableData, model.tableData);
                soap.AppendFormat("<zyd:reqType>{0}</zyd:reqType>", "all");
                soap.AppendFormat("</zyd:instrumentDictionaryRequest>");
                soap.AppendFormat("</soapenv:Body>");
                soap.AppendFormat("</soapenv:Envelope>");

                //发起请求
                Uri uri = new Uri(AnHuiInterface.ServerAddr);
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                //响应
                string rtnStr = string.Empty;
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    rtnStr = myStreamReader.ReadToEnd();
                }

                return rtnStr;
            }

            /// <summary>
            /// 安徽省项目 - 采集接口
            /// </summary>
            /// <returns></returns>
            public static string instrumentInfoHandle(clsInstrumentInfoHandle model)
            {
                StringBuilder soap = new StringBuilder();
                soap.AppendFormat("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ins=\"http://www.zhiyunda.com/service/instrumentDockingService\" xmlns:zyd=\"http://www.zhiyunda.com/zydjcy\">");
                soap.AppendFormat("<soapenv:Header>");
                soap.AppendFormat("<ins:interfaceVersion>{0}</ins:interfaceVersion>", model.interfaceVersion);
                soap.AppendFormat("<ins:key>{0}</ins:key>", model.key);
                soap.AppendFormat("<ins:userName>{0}</ins:userName>", model.userName);
                soap.AppendFormat("</soapenv:Header>");
                soap.AppendFormat("<soapenv:Body>");
                soap.AppendFormat("<zyd:instrumentInfoRequest>");
                soap.AppendFormat("<zyd:instrumentDockingInfoList>");
                soap.AppendFormat("<zyd:instrument>{0}</zyd:instrument>", model.instrument);
                soap.AppendFormat("<zyd:instrumentNo>{0}</zyd:instrumentNo>", model.instrumentNo);
                soap.AppendFormat("<zyd:gps>{0}</zyd:gps>", model.gps);
                soap.AppendFormat("<zyd:mac>{0}</zyd:mac>", model.mac);
                soap.AppendFormat("<zyd:fTpye>{0}</zyd:fTpye>", model.fTpye);
                soap.AppendFormat("<zyd:fName>{0}</zyd:fName>", model.fName);
                soap.AppendFormat("<zyd:tradeMark>{0}</zyd:tradeMark>", model.tradeMark);
                soap.AppendFormat("<zyd:foodcode>{0}</zyd:foodcode>", model.foodcode);
                soap.AppendFormat("<zyd:proBatch>{0}</zyd:proBatch>", model.proBatch);
                soap.AppendFormat("<zyd:proDate>{0}</zyd:proDate>", model.proDate);
                soap.AppendFormat("<zyd:proSpecifications>{0}</zyd:proSpecifications>", model.proSpecifications);
                soap.AppendFormat("<zyd:manuUnit>{0}</zyd:manuUnit>", model.manuUnit);
                soap.AppendFormat("<zyd:checkedNo>{0}</zyd:checkedNo>", model.checkedNo);
                soap.AppendFormat("<zyd:sampleNo>{0}</zyd:sampleNo>", model.sampleNo);
                soap.AppendFormat("<zyd:checkedUnit>{0}</zyd:checkedUnit>", model.checkedUnit);
                soap.AppendFormat("<zyd:dataNum>{0}</zyd:dataNum>", model.dataNum);
                soap.AppendFormat("<zyd:testPro>{0}</zyd:testPro>", model.testPro);
                soap.AppendFormat("<zyd:quanOrQual>{0}</zyd:quanOrQual>", model.quanOrQual);
                soap.AppendFormat("<zyd:contents>{0}</zyd:contents>", model.contents);
                soap.AppendFormat("<zyd:unit>{0}</zyd:unit>", model.unit);
                soap.AppendFormat("<zyd:testResult>{0}</zyd:testResult>", model.testResult);
                soap.AppendFormat("<zyd:dilutionRa>{0}</zyd:dilutionRa>", model.dilutionRa);
                soap.AppendFormat("<zyd:testRange>{0}</zyd:testRange>", model.testRange);
                soap.AppendFormat("<zyd:standardLimit>{0}</zyd:standardLimit>", model.standardLimit);
                soap.AppendFormat("<zyd:basedStandard>{0}</zyd:basedStandard>", model.basedStandard);
                soap.AppendFormat("<zyd:testPerson>{0}</zyd:testPerson>", model.testPerson);
                soap.AppendFormat("<zyd:testTime>{0}</zyd:testTime>", model.testTime);
                soap.AppendFormat("<zyd:sampleTime>{0}</zyd:sampleTime>", model.sampleTime);
                soap.AppendFormat("<zyd:remark>{0}</zyd:remark>", model.remark);
                soap.AppendFormat("<zyd:reserve1>{0}</zyd:reserve1>", "");
                soap.AppendFormat("<zyd:reserve2>{0}</zyd:reserve2>", "");
                soap.AppendFormat("<zyd:reserve3>{0}</zyd:reserve3>", "");
                soap.AppendFormat("<zyd:reserve4>{0}</zyd:reserve4>", "");
                soap.AppendFormat("<zyd:reserve5>{0}</zyd:reserve5>", "");
                soap.AppendFormat("</zyd:instrumentDockingInfoList>");
                soap.AppendFormat("</zyd:instrumentInfoRequest>");
                soap.AppendFormat("</soapenv:Body>");
                soap.AppendFormat("</soapenv:Envelope>");

                //发起请求
                Uri uri = new Uri(AnHuiInterface.ServerAddr);
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                //响应
                string rtnStr = string.Empty;
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    rtnStr = myStreamReader.ReadToEnd();
                }
                return rtnStr;
            }

            /// <summary>
            /// MD5加密
            /// </summary>
            /// <param name="password"></param>
            /// <returns></returns>
            public static string md5(string password)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                bytes = md5.ComputeHash(bytes);
                md5.Clear();

                string ret = "";
                for (int i = 0; i < bytes.Length; i++)
                {
                    ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
                }

                return ret.PadLeft(32, '0');
            }

            /// <summary>
            /// 安徽省项目 - 解析数据上传返回的XML
            /// </summary>
            /// <param name="xml"></param>
            /// <returns></returns>
            public static List<string> ParsingUploadXML(string xml)
            {
                List<string> rtnStr = new List<string>();
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xml);
                string status = string.Empty, description = string.Empty;
                XmlNodeList statusList = xd.GetElementsByTagName("status");
                foreach (XmlNode item in statusList)
                    status = item.InnerText;
                XmlNodeList descriptionList = xd.GetElementsByTagName("description");
                foreach (XmlNode item in descriptionList)
                    description = item.InnerText;
                rtnStr.Add(status);
                rtnStr.Add(description);

                return rtnStr;
            }

            /// <summary>
            /// 安徽省项目 - 解析XML
            /// </summary>
            /// <param name="xml"></param>
            /// <returns></returns>
            public static string ParsingXML(string xml)
            {
                try
                {
                    data_dictionaryList = new List<data_dictionary>();
                    checked_unitList = new List<checked_unit>();
                    standard_limitList = new List<standard_limit>();
                    string status = string.Empty, description = string.Empty, tableUpdateTime = string.Empty;
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(xml);
                    XmlNodeList statusList = xd.GetElementsByTagName("status");
                    foreach (XmlNode item in statusList)
                        status = item.InnerText;
                    XmlNodeList descriptionList = xd.GetElementsByTagName("description");
                    foreach (XmlNode item in descriptionList)
                        description = item.InnerText;
                    if (status.Equals("1") && description.Equals("成功"))
                    {
                        //获取更新时间tableUpdateTime
                        XmlNodeList tableUpdateTimeList = xd.GetElementsByTagName("tableUpdateTime");
                        foreach (XmlNode item in tableUpdateTimeList)
                            tableUpdateTime = item.InnerText;
                        #region 更新时间
                        //string[] strTime = tableUpdateTime.Split(';');
                        //if (strTime != null && strTime.Length > 0)
                        //{
                        //    foreach (string item in strTime)
                        //    {
                        //        if (item.Length > 0)
                        //        {
                        //            string str = ":" + DateTime.Now.Year;
                        //            string[] sArray = Regex.Split(item, str, RegexOptions.IgnoreCase);
                        //            if (sArray != null && sArray.Length > 0)
                        //            {
                        //                if (sArray[0].Equals("data_dictionary"))
                        //                {
                        //                    string data_dictionary = DateTime.Now.Year + sArray[1];
                        //                }
                        //                else if (sArray[0].Equals("checked_unit"))
                        //                {
                        //                    string checked_unit = DateTime.Now.Year + sArray[1];
                        //                }
                        //                else if (sArray[0].Equals("standard_limit"))
                        //                {
                        //                    string standard_limit = DateTime.Now.Year + sArray[1];
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                        XmlNodeList updatedataList = xd.GetElementsByTagName("updateData");
                        foreach (XmlNode items in updatedataList)
                        {
                            if (items.Name.Equals("updateData"))
                            {
                                //此处将updateData中的串重新加载到XML来解析
                                string str = items.InnerText;
                                xd = new XmlDocument();
                                xd.LoadXml(str);
                                XmlNodeList table = xd.GetElementsByTagName("table");
                                foreach (XmlNode item in table)
                                {
                                    XmlNodeList data = item.ChildNodes;
                                    string type = data[0].InnerText;
                                    foreach (XmlNode dts in data[2])
                                    {
                                        string strData = dts.InnerText;
                                        if (!strData.Equals(""))
                                        {
                                            string[] strDataList = strData.Split('|');
                                            if (strDataList != null && strDataList.Length > 0)
                                            {
                                                if (type.Equals("data_dictionary"))
                                                {
                                                    data_dictionary data_dictionary = new data_dictionary
                                                    {
                                                        id = strDataList[0],
                                                        codeId = strDataList[1],
                                                        name = strDataList[2],
                                                        pid = strDataList[3],
                                                        remark = strDataList[4],
                                                        inputdate = strDataList[5],
                                                        modifydate = strDataList[6],
                                                        status = strDataList[7],
                                                        typeNum = strDataList[8]
                                                    };
                                                    data_dictionaryList.Add(data_dictionary);
                                                }
                                                else if (type.Equals("checked_unit"))
                                                {
                                                    checked_unit checked_unit = new checked_unit
                                                    {
                                                        id = strDataList[0],
                                                        inputdate = strDataList[1],
                                                        modifydate = strDataList[2],
                                                        address = strDataList[3],
                                                        busScope = strDataList[4],
                                                        bussinessId = strDataList[5],
                                                        idCard = strDataList[6],
                                                        linkName = strDataList[7],
                                                        tel = strDataList[8],
                                                        unitName = strDataList[9],
                                                        status = strDataList[10]
                                                    };
                                                    checked_unitList.Add(checked_unit);
                                                }
                                                else if (type.Equals("standard_limit"))
                                                {
                                                    standard_limit standard_limit = new standard_limit
                                                    {
                                                        id = strDataList[0],
                                                        inputdate = strDataList[1],
                                                        modifydate = strDataList[2],
                                                        decisionBasis = strDataList[3]
                                                    };
                                                    double limit = 0;
                                                    if (double.TryParse(strDataList[4], out limit)) standard_limit.maxLimit = limit;
                                                    else standard_limit.maxLimit = 0;
                                                    if (double.TryParse(strDataList[5], out limit)) standard_limit.minLimit = limit;
                                                    else standard_limit.minLimit = 0;
                                                    standard_limit.testBasis = strDataList[6];
                                                    standard_limit.unit = strDataList[7];
                                                    standard_limit.foodType = strDataList[8];
                                                    standard_limit.testItem = strDataList[9];
                                                    standard_limitList.Add(standard_limit);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        data_dictionaryList = null;
                        checked_unitList = null;
                        standard_limitList = null;
                        return description;
                    }
                    return status;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
