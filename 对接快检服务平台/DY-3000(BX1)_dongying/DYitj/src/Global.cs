﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Windows;
using System.Xml;
using AIO.AnHui;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    public class Global
    {
        public static int num1 = 0;
        public static int num2 = 0;
        public static int num3 = 0;
        public static int num4 = 0;
        /// <summary>
        /// 上传成功的数据条数
        /// </summary>
        public static int UploadSCount = 0;
        /// <summary>
        /// 上传失败的数据条数
        /// </summary>
        public static int UploadFCount = 0;
        /// <summary>
        /// 存放和项目相关的全局变量
        /// </summary>
        public static string ItemsDirectory = Environment.CurrentDirectory + "\\Items";
        public static string AccountsDirectory = Environment.CurrentDirectory + "\\Accounts";
        public static string OthersDirectory = Environment.CurrentDirectory + "\\Others";
        public static string LogDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DY-Detector\\log";
        public static string TxtItemsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DY-Detector\\检测项目";
        public static List<DYFGDItemPara> fgdItems;
        public static List<DYFGDItemPara> NewfgdItems;
        public static string fgdItemsFile = ItemsDirectory + "\\" + "fgdItems.dat";
        public static List<DYJTJItemPara> jtjItems;
        public static string jtjItemsFile = ItemsDirectory + "\\" + "jtjItems.dat";
        public static List<DYJTJItemPara> bcspItems;
        public static string bcspItemsFile = ItemsDirectory + "\\" + "bcspItems.dat";
        public static List<DYGSZItemPara> gszItems;
        public static string gszItemsFile = ItemsDirectory + "\\" + "gszItems.dat";
        public static List<DYHMItemPara> hmItems;
        public static string hmItemsFile = ItemsDirectory + "\\" + "hmItems.dat";
        public static List<UserAccount> userAccounts;
        public static string userAccountsFile = AccountsDirectory + "\\" + "userAccounts.dat";
        #region
        //2015-06-18 revised by lee Sample Standard Adapter to choice and save
        public static List<CheckPointInfo> samplenameadapter;//tFoodClass
        public static string samplenameadapterFile = AccountsDirectory + "\\" + "FoodClass.dat";
        public static List<ItemsAdapter> adapteritem;//tCheckItem-Standard
        public static string adapteritemFile = AccountsDirectory + "\\" + "CheckItemStandard.dat";
        public static List<DisPlayItems> displayItems;
        public static string displayItemsFile = AccountsDirectory + "\\" + "DisPlayItems.dat";
        public static DataSet DsAllTemp;
        #endregion
        // 检测孔配置
        public static DeviceProp.DeviceHole deviceHole;
        public static string deviceHoleFile = OthersDirectory + "\\" + "deviceHole.dat";
        public static string strSERVERADDR;
        public static string REGISTERID;
        public static string REGISTERPASSWORD;
        public static string CHECKPOINTID;
        public static string CHECKPOINTNAME;
        public static string CHECKPOINTTYPE;
        public static string ORGANIZATION;
        public static string strADPORT;
        public static string strSXT1PORT;
        public static string strSXT2PORT;
        //-NewAdd2016-06-20
        public static string strSXT3PORT;
        public static string strSXT4PORT;
        public static string strPRINTPORT;
        public static string strHMPORT;
        public static string strWSWPATH;
        public static string strGZZPATH;
        /// <summary>
        /// 是否进行测试，Y为是 
        /// </summary>
        public static bool IsTest = false;
        public static Double Standard1;
        public static Double Standard2;
        public static Double Standard3;
        public static Double Standard4;
        /// <summary>
        /// 单通道判定标准
        /// </summary>
        public static Double DecisionCriteria1;
        /// <summary>
        /// 通道间差判定标准
        /// </summary>
        public static Double DecisionCriteria2;
        public static UpdateServer updateServer;
        public static WorkThread workThread = null;
        public static WorkThread printThread = null;
        public static WorkThread updateThread = null;
        /// <summary>huowu.
        /// 存储分光度、干化学、重金属、胶体金是否有启用，用于设置界面的控件是否启用判断
        /// </summary>banlang
        public static bool set_IsOpenFgd = true, set_IsOpenSxt = true, set_IsOpenZjs = true;
        public static string sampleName = string.Empty, projectName = string.Empty, projectUnit = string.Empty;
        /// <summary>
        /// 存储样品小精灵是否从项目名称弹出
        /// </summary>
        public static bool IsProject = false;
        /// <summary>
        /// 干试纸|gsz，重金属|zjs，分光度|fgd，胶体金|jtj
        /// </summary>
        //public static string typeName = string.Empty;
        /// <summary>
        /// 是否启用微生物
        /// </summary>
        public static bool IsEnableWswOrAtp = false;
        /// <summary>
        /// 是微生物还是ATP
        /// </summary>
        public static string IsWswOrAtp = "WSW";
        /// <summary>
        /// 是否启用薄层色谱
        /// </summary>
        public static bool IsEnableBcsp = false;
        /// <summary>
        /// 是否启用荧光免疫
        /// </summary>
        public static bool IsEnableYgmy = false;
        /// <summary>
        /// 是否启用食源性微生物
        /// </summary>
        public static bool IsEnableSyxwsw = false;
        /// <summary>
        /// 微生物软件存放地址
        /// </summary>
        public static string MicrobialAddress = string.Empty;
        /// <summary>
        /// ATP是否打开指定文件 Y为文件；N为文件夹
        /// </summary>
        public static bool IsOpenFile = false;
        /// <summary>
        /// 企业版 QY | 行政版 XZ
        /// </summary>
        public static string Version = string.Empty;
        /// <summary>
        /// CCA的值
        /// </summary>
        //public static double aValue = 1;
        public static int SelIndex = 0;
        /// <summary>
        /// 任务名称
        /// </summary>
        public static string TaskName = string.Empty;
        /// <summary>
        /// 任务编号
        /// </summary>
        public static string TaskCode = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string CompanyName = string.Empty;
        /// <summary>
        /// 是否启用视频播放功能
        /// </summary>
        public static bool EnableVideo = false;
        /// <summary>
        /// 是否启用电池显示功能
        /// </summary>
        public static bool EnableBattery = false;
        /// <summary>
        /// 视频播放地址
        /// </summary>
        public static string VideoAddress = string.Empty;
        /// <summary>
        /// PDF文件存放路径
        /// </summary>
        public static string PdfAddress = string.Empty;
        /// <summary>
        /// 是否已经在播放
        /// </summary>
        public static bool IsPlayer = false;
        /// <summary>
        /// 是否使用迅雷看看播放组件
        /// </summary>
        public static bool IsAPlayer = false;
        /// <summary>
        /// 是否启用设置界面【故障检测】按钮，1为启用|非1为不启用
        /// </summary>
        public static bool set_FaultDetection = false;
        /// <summary>
        /// 是否启用设置界面【分光光度检测通道配置】，1为启用|非1为不启用
        /// </summary>
        public static bool set_ShowFgd = false;
        /// <summary>
        /// 新的验证连接地址有效性方法
        /// </summary>
        public static string pointNum = string.Empty, ponitName = string.Empty, pointType = string.Empty, orgNum = string.Empty, orgName = string.Empty, userUUID = string.Empty;
        /// <summary>
        /// 是否调整项目坐标
        /// </summary>
        public static bool IsSetIndex = false;

        /// <summary>
        /// 是否启用样品管理界面的清理重复样品按钮
        /// </summary>
        public static bool IsDELETED = false;

        /// <summary>
        /// 打印类型，"" 原始模板，GS 甘肃打印模板
        /// </summary>
        public static string PrintType = string.Empty;
        /// <summary>
        /// 是否允许修改检测值 Y为允许，N为不允许 
        /// </summary>
        public static bool IsUpdateChekcedValue = false;

        /// <summary>
        /// true为已出卡 false为未出卡
        /// </summary>
        public static bool jbkIsOut = true;

        public static double xValue = 0;
        public static double yValue = 0;
        public static double timeValue = 0;

        public static bool IsContrast = false;

        public static bool IsClear = false;
        /// <summary>
        /// 帮助文档存放路径
        /// </summary>
        public static string helpDocumentAddress = string.Empty;

        /// <summary>
        /// 硬件编号;
        /// 硬件版本号; 0x20:胶体金扫描新模块 0x30胶体金新摄像头模块
        /// 软件版本号;
        /// </summary>
        public static byte[] JtjVersionInfo = null;

        /// <summary>
        /// 是否是摄像头胶体金新模块
        /// </summary>
        //public static bool IsNewSxtJtj= (JtjVersionInfo != null && JtjVersionInfo[1] >= 0x30) ? true : false;
        /// <summary>
        /// 胶体金版本，1为第一代，2第二代，3第三代(新摄像头模组)
        /// </summary>
        public static int JtjVersion = 1;

        /// <summary>
        /// 仪器名称
        /// </summary>
        public static string InstrumentName = string.Empty;
        /// <summary>
        /// 仪器型号
        /// </summary>
        public static string InstrumentNameModel = string.Empty;
        /// <summary>
        /// 是否已经打开了简要提示界面
        /// </summary>
        public static bool IsOpenPrompt = false;
        /// <summary>
        /// 是否显示测试值
        /// </summary>
        public static bool IsShowValue = false;

        public static bool IsSettingCurve = false;
        /// <summary>
        /// 接口类型，默认达元接口
        /// AH=安徽
        /// </summary>
        public static string InterfaceType = string.Empty;
        /// <summary>
        /// 是否启用数据管理中的知识库模块
        /// </summary>
        public static bool EnableKnowledgeBase = false;
        /// <summary>
        /// 是否启用演示专用章
        /// </summary>
        public static bool EnableChapter = false;
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public static bool EnableValidCode = false;
        /// <summary>
        /// 是否打印二维码
        /// </summary>
        public static bool PrintQrCode = false;
        /// <summary>
        /// 是否启用GPS
        /// </summary>
        public static bool EnableGPS = false;
        /// <summary>
        /// 曲线值存储，逗号隔开
        /// 自动生成曲线时用到
        /// </summary>
        public static string CurveValue = string.Empty;
        /// <summary>
        /// 广东省智慧云平台 通过选择快检单号时是否自动选择对应样品名称
        /// </summary>
        public static bool IsSelectSampleName = false;
        /// <summary>
        /// 是否启动定时检测数据上传
        /// </summary>
        public static bool IsStartUploadTimer = true;
        /// <summary>
        /// MAC地址
        /// </summary>
        public static string Mac = string.Empty;
        /// <summary>
        /// 设备唯一码
        /// </summary>
        public static string DeviceID = string.Empty;
        internal static double nHelpBoxWidth = double.Parse(CFGUtils.GetConfig("nHelpBoxWidth", "15"));
        internal static double nHelpBoxHeight = double.Parse(CFGUtils.GetConfig("nHelpBoxHeight", "100"));
        internal static double nHelpBoxLineWidth = double.Parse(CFGUtils.GetConfig("nHelpBoxLineWidth", "3"));
        internal static double nHelpBoxLeft1 = double.Parse(CFGUtils.GetConfig("nHelpBoxLeft1", "118"));
        internal static double nHelpBoxTop1 = double.Parse(CFGUtils.GetConfig("nHelpBoxTop1", "32"));
        internal static double nHelpBoxLeft2 = double.Parse(CFGUtils.GetConfig("nHelpBoxLeft2", "118"));
        internal static double nHelpBoxTop2 = double.Parse(CFGUtils.GetConfig("nHelpBoxTop2", "32"));
        internal static int LineCount = int.Parse(CFGUtils.GetConfig("LineCount", "10"));

        #region 将序列化Items进行简单的封装
        /// <summary>
        /// 将序列化Items进行了简单的封装
        /// 
        /// 一般来说，刚启动程序的时候，用SerializeFromFile把文件读取到内存。
        /// 
        /// 然后一切操作都在内存中进行，如果内存数据修改了，就SerializeToFile。
        /// </summary>
        public static void SerializeToFile(List<DYFGDItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYFGDItemPara>> serialize = new BinarySerialize<List<DYFGDItemPara>>();
                serialize.Serialize(items, filePath);
                GenerateItemTxt();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public static void SerializeToFile(List<DYJTJItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYJTJItemPara>> serialize = new BinarySerialize<List<DYJTJItemPara>>();
                serialize.Serialize(items, filePath);
                GenerateItemTxt();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public static void SerializeToFile(List<DYGSZItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYGSZItemPara>> serialize = new BinarySerialize<List<DYGSZItemPara>>();
                serialize.Serialize(items, filePath);
                GenerateItemTxt();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public static void SerializeToFile(List<DYHMItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYHMItemPara>> serialize = new BinarySerialize<List<DYHMItemPara>>();
                serialize.Serialize(items, filePath);
                GenerateItemTxt();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public static void SerializeToFile(List<UserAccount> accounts, string filePath)
        {
            try
            {
                BinarySerialize<List<UserAccount>> serialize = new BinarySerialize<List<UserAccount>>();
                serialize.Serialize(accounts, filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }
        /// <summary>
        /// CheckItems Display Lee
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="filePath"></param>
        public static void SerializeToFile(List<DisPlayItems> accounts, string filePath)
        {
            try
            {
                BinarySerialize<List<DisPlayItems>> serialize = new BinarySerialize<List<DisPlayItems>>();
                serialize.Serialize(accounts, filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }
        /// <summary>
        /// foodclass
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="filePath"></param>
        public static void SerializeToFile(List<CheckPointInfo> accounts, string filePath)
        {
            try
            {
                BinarySerialize<List<CheckPointInfo>> serialize = new BinarySerialize<List<CheckPointInfo>>();
                serialize.Serialize(accounts, filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }
        /// <summary>
        /// CheckItemStandard
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="filePath"></param>
        public static void SerializeToFile(List<ItemsAdapter> accounts, string filePath)
        {
            try
            {
                BinarySerialize<List<ItemsAdapter>> serialize = new BinarySerialize<List<ItemsAdapter>>();
                serialize.Serialize(accounts, filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public static void SerializeToFile(DeviceProp.DeviceHole ledwaves, string filePath)
        {
            try
            {
                BinarySerialize<DeviceProp.DeviceHole> serialize = new BinarySerialize<DeviceProp.DeviceHole>();
                serialize.Serialize(ledwaves, filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        private static void GenerateItemTxt()
        {
            string filePath = TxtItemsDirectory + "\\检测项目对应项.txt";
            if (File.Exists(filePath))
                File.Delete(filePath);
            for (int i = 0; i < fgdItems.Count; ++i)
            {
                DYFGDItemPara item = fgdItems[i];
                FileUtils.AddToFile(filePath, "{分光光度#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
            }

            for (int i = 0; i < jtjItems.Count; ++i)
            {
                DYJTJItemPara item = jtjItems[i];
                FileUtils.AddToFile(filePath, "{胶体金#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
            }

            for (int i = 0; i < gszItems.Count; ++i)
            {
                DYGSZItemPara item = gszItems[i];
                FileUtils.AddToFile(filePath, "{胶体金#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
            }
        }
        #endregion

        #region 将反序列化进行简单的封装
        /// <summary>
        /// 将反序列化进行了简单的封装
        /// </summary>
        public static void DeSerializeFromFile(out List<DYFGDItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYFGDItemPara>> serialize = new BinarySerialize<List<DYFGDItemPara>>();
                items = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                items = new List<DYFGDItemPara>();
            }
        }

        public static void DeSerializeFromFile(out List<DYJTJItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYJTJItemPara>> serialize = new BinarySerialize<List<DYJTJItemPara>>();
                items = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                items = new List<DYJTJItemPara>();
            }
        }

        public static void DeSerializeFromFile(out List<DYGSZItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYGSZItemPara>> serialize = new BinarySerialize<List<DYGSZItemPara>>();
                items = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                items = new List<DYGSZItemPara>();
            }
        }

        public static void DeSerializeFromFile(out List<DYHMItemPara> items, string filePath)
        {
            try
            {
                BinarySerialize<List<DYHMItemPara>> serialize = new BinarySerialize<List<DYHMItemPara>>();
                items = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                items = new List<DYHMItemPara>();
            }
        }

        public static void DeSerializeFromFile(out List<UserAccount> accounts, string filePath)
        {
            try
            {
                BinarySerialize<List<UserAccount>> serialize = new BinarySerialize<List<UserAccount>>();
                accounts = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                accounts = new List<UserAccount>();
            }
        }

        /// <summary>
        ///  Adaper Items display
        /// </summary>
        /// <param name="DisplayNumberList"></param>
        /// <param name="filePath"></param>
        public static void DeSerializeFromFile(out List<DisPlayItems> DisplayNumberList, string filePath)
        {
            try
            {
                BinarySerialize<List<DisPlayItems>> serialize = new BinarySerialize<List<DisPlayItems>>();
                DisplayNumberList = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                DisplayNumberList = new List<DisPlayItems>();
            }
        }
        /// <summary>
        /// Adapter Value to Sample result
        /// </summary>
        /// <param name="AdapterNumberList"></param>
        /// <param name="filePath"></param>
        public static void DeSerializeFromFile(out List<ItemsAdapter> AdapterNumberList, string filePath)
        {
            try
            {
                BinarySerialize<List<ItemsAdapter>> serialize = new BinarySerialize<List<ItemsAdapter>>();
                AdapterNumberList = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                AdapterNumberList = new List<ItemsAdapter>();
            }
        }
        /// <summary>
        /// Local data -requirement standard Sample adapter standard
        /// </summary>
        /// <param name="SampleNameList"></param>
        /// <param name="filePath"></param>
        public static void DeSerializeFromFile(out List<CheckPointInfo> SampleNameList, string filePath)
        {
            try
            {
                BinarySerialize<List<CheckPointInfo>> serialize = new BinarySerialize<List<CheckPointInfo>>();
                SampleNameList = serialize.DeSerialize(filePath);

                DataSet DsTempViews = new DataSet();
                DsTempViews = IListDataSet.ToDataSet<CheckPointInfo>(SampleNameList);
                DsAllTemp = DsTempViews;
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                SampleNameList = new List<CheckPointInfo>();
            }
        }

        #endregion

        public static void DeSerializeFromFile(out DeviceProp.DeviceHole ledwaves, string filePath)
        {
            try
            {
                BinarySerialize<DeviceProp.DeviceHole> serialize = new BinarySerialize<DeviceProp.DeviceHole>();
                ledwaves = serialize.DeSerialize(filePath);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                ledwaves = new DeviceProp.DeviceHole();
            }
        }

        /// <summary>
        /// 将DataTable转换成实体类
        /// 2016年6月24日 wenj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> TableToEntity<T>(DataTable dt) where T : new()
        {
            // 定义集合 
            List<T> ts = new List<T>();

            // 获得此模型的类型 
            Type type = typeof(T);
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量   
                    //检查DataTable是否包含此列（列名==对象的属性名）     
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter   
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出   
                        //取值   
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性   
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }

        public static class ATP
        {
            private static Synoxo.USBHidDevice.DeviceManagement MyDeviceManagement = new Synoxo.USBHidDevice.DeviceManagement();

            ///  <summary>
            ///  用VID和PID查找HID设备
            ///  </summary>
            ///  <returns>True： 找到设备</returns>
            public static bool FindTheHid()
            {
                try
                {
                    string strvid = "0483", strpid = "5750";
                    int myVendorID = 0x03EB;
                    int myProductID = 0x2013;
                    int vid = 0;
                    int pid = 0;
                    try
                    {
                        vid = Convert.ToInt32(strvid, 16);
                        pid = Convert.ToInt32(strpid, 16);
                        myVendorID = vid;
                        myProductID = pid;
                    }
                    catch (SystemException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    if (MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                    {
                        getCommunication();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return false;
            }

            /// <summary>
            /// 获取通讯
            /// </summary>
            /// <returns></returns>
            public static void getCommunication()
            {
                //建立通讯
                string cmd = "0xFF 0x08 0x30 0x00 0x00 0x00 0x38 0xFE";
                byte[] bt = ReadAndWriteToDevice(cmd);
            }

            /// <summary>
            /// 获取数据指令
            /// </summary>
            /// <returns></returns>
            public static string getCmd(int index)
            {
                string str = "0x08 0x31 0x00 0x00 0x" + index.ToString("X2");
                byte crc = 0;
                byte[] btList = StringToBytes(str, new string[] { ",", " " }, 16);
                for (int i = 0; i < btList.Length; i++)
                    crc += btList[i];

                str = "0xFF 0x08 0x31 0x00 0x00 0x" + index.ToString("X2") + " 0x" + crc.ToString("X2") + " 0xFE";

                return str;
            }

            /// <summary>
            /// 读取下位机返回的数据
            /// </summary>
            /// <returns></returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
            public static byte[] ReadAndWriteToDevice(string cmd)
            {
                int len = 64;
                byte[] inputdatas = new byte[len];
                try
                {
                    byte[] outdatas = new byte[len];
                    outdatas[0] = 0x55;
                    outdatas[1] = 0x2;
                    outdatas[2] = 0x1;
                    outdatas[3] = 0x00;
                    byte[] inputs = StringToBytes(cmd, new string[] { ",", " " }, 16);
                    if (inputs != null && inputs.Length > 0)
                        outdatas = inputs;
                    System.Windows.Forms.Application.DoEvents();
                    //MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, 100);
                    if (MyDeviceManagement.WriteReport(0, false, outdatas, ref inputdatas, 100))
                    {
                        int length = 0;
                        while (!MyDeviceManagement.ReadReport(0, false, outdatas, ref inputdatas, 100))
                        {
                            length++;
                            if (length == 20)
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return inputdatas;
            }

            public static List<byte[]> getByteList(byte[] data)
            {
                List<byte[]> dataList = new List<byte[]>();
                int index = 3;
                for (int i = 0; i < 3; i++)
                {
                    byte[] bt = new byte[18];
                    for (int j = 0; j < bt.Length; j++)
                    {
                        index++;
                        bt[j] = data[index];
                        if (index == 21 || index == 39 || index == 57)
                            dataList.Add(bt);
                    }
                }
                return dataList;
            }

            /// <summary>
            /// 将给定的字符串按照给定的分隔符和进制转换成字节数组
            /// </summary>
            /// <param name="str">给定的字符串</param>
            /// <param name="splitString">分隔符</param>
            /// <param name="fromBase">给定的进制</param>
            /// <returns>转换后的字节数组</returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
            public static byte[] StringToBytes(string str, string[] splitString, int fromBase)
            {
                string[] strBytes = str.Split(splitString, StringSplitOptions.RemoveEmptyEntries);
                if (strBytes == null || strBytes.Length == 0)
                    return null;
                byte[] _return = new byte[strBytes.Length];
                for (int i = 0; i < strBytes.Length; i++)
                {
                    try
                    {
                        _return[i] = Convert.ToByte(strBytes[i], fromBase);
                    }
                    catch (SystemException ex)
                    {
                        throw ex;
                        //MessageBox.Show("发现不可转换的字符串->" + strBytes[i]);
                    }
                }
                return _return;
            }

        }

        /// <summary> 
        /// 16进制[字符串]转16进制[字节数组] 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static void soapWebservice()
        {
            StringBuilder soap = new StringBuilder();
            soap.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ins=\"http://www.zhiyunda.com/service/instrumentDockingService\" xmlns:zyd=\"http://www.zhiyunda.com/zydjcy\">");
            soap.Append("<soapenv:Header>");
            soap.AppendFormat("<ins:interfaceVersion>{0}</ins:interfaceVersion>", "ss");
            soap.AppendFormat("<ins:key>{0}</ins:key>", "ss");
            soap.AppendFormat("<ins:userName>{0}</ins:userName>", "ss");
            soap.Append("</soapenv:Header>");
            soap.Append("<soapenv:Body>");
            soap.Append("<zyd:instrumentDictionaryRequest>");
            soap.AppendFormat("<zyd:instrument>{0}</zyd:instrument>", "ss");
            soap.AppendFormat("<zyd:instrumentNo>{0}</zyd:instrumentNo>", "ss");
            soap.AppendFormat("<zyd:mac>{0}</zyd:mac>", "ss");
            soap.AppendFormat("<zyd:tableData>{0}</zyd:tableData>", "ss");
            soap.AppendFormat("<zyd:reqType>{0}</zyd:reqType>", "ss");
            soap.Append("</zyd:instrumentDictionaryRequest>");
            soap.Append("</soapenv:Body>");
            soap.Append("</soapenv:Envelope>");

            Uri uri = new Uri("http://118.26.158.182:9880/kjsys_new/webservice/instrumentDockingServiceProviderService?wsdl");
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                requestStream.Write(paramBytes, 0, paramBytes.Length);
            }

            //响应
            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                string str = myStreamReader.ReadToEnd();
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// string转Int
        /// 2016年3月11日 wenj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringConvertInt(string str)
        {
            int valueInt = 0;
            if (!int.TryParse(str, out valueInt))
                return 0;
            return valueInt;
        }

        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;
            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }
            return strTemp;
        }

        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;
            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);
            //如果是字母，则直接返回
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else
                return ("?");
        }

        /// <summary>
        /// 胶体金模块检测计算代码
        /// </summary>
        public static class JbkCheckCalc
        {
            public static DYJTJItemPara _jtjItem = null;
            public static DYGSZItemPara _gszItem = null;

            /// <summary>
            /// 胶体金获取有效点
            /// </summary>
            /// <param name="datas">数据</param>
            /// <param name="index">原点</param>
            /// <param name="scope">有效点范围</param>
            /// <returns></returns>
            public static int GetEfficientPoint(double[] datas, int index, int[] scope)
            {
                if (datas == null) return 0;
                int efficientPoint = 0;
                for (int i = index; i < datas.Length; i--)
                {
                    try
                    {
                        if (datas[i] >= datas[i - 1] || efficientPoint >= scope[1]) break;
                        efficientPoint++;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if (i < index) break;
                }

                return efficientPoint < scope[0] ? 0 : efficientPoint;
            }

            /// <summary>
            /// 胶体金计算差值
            /// </summary>
            /// <param name="datas">数据</param>
            /// <param name="index">原点</param>
            /// <returns></returns>
            public static double GetDifference(double[] datas, int index, string type)
            {
                if (datas == null) return 0;
                double difference = 0;
                if (type.Equals("C"))
                {
                    for (int i = index; i < index + 23; i++)
                    {
                        if (datas[i] >= datas[i + 1])
                        {
                            difference = datas[i] - datas[index];
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = index; i > 0; i--)
                    {
                        try
                        {
                            if (datas[i - 1] <= datas[i])
                            {
                                difference = datas[i] - datas[index];
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            return 0;
                        }
                    }
                }
                return difference;
            }

            /// <summary>
            /// 胶体金计算斜率
            /// </summary>
            /// <param name="datas">数据</param>
            /// <param name="index">原点</param>
            /// <param name="scope">有效范围</param>
            /// <returns></returns>
            public static double GetSlope(double[] datas, int index, int[] scope)
            {
                if (datas == null) return 0;
                double slope = 0;
                int TMaxIndex = 0;
                for (int j = index; j > 0; j--)
                {
                    try
                    {
                        if ((datas[j - 1] - datas[j]) < 0)
                        {
                            TMaxIndex = j;
                            slope = 0;
                            //①当T_i-T_MAX≥8，则直接取T_8坐标的X_8值，计算斜率K=(X_8-X_MIN)/8值
                            if ((index - TMaxIndex) >= scope[1])
                            {
                                slope = (datas[index - scope[1]] - datas[index]) / scope[1];
                            }
                            //②当T_i-T_MAX＜8，则直接取T_i坐标的X_i值
                            else if ((index - TMaxIndex) < scope[1])
                            {
                                slope = (datas[j] - datas[index]) / (index - TMaxIndex);
                            }
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        slope = 0;
                    }
                }
                return slope;
            }

            /// <summary>
            /// 胶体金结果值计算
            /// </summary>
            /// <param name="datas"></param>
            /// <param name="MinIndex">波谷坐标</param>
            /// <param name="scope">前后两个波峰坐标</param>
            /// <param name="type"></param>
            /// <returns></returns>
            public static float CalcCTVal(double[] datas, int MinIndex, int[] scope, string type)
            {
                if (datas == null) return 0;
                float val = 0, val1 = 0, val2 = 0;
                //计算面积
                if (type.Equals("A"))
                {
                    //原始值曲线面积
                    //totalArea总面积，upArea上部分面积，beforeArea前面部分面积，afterArea后面部分面积
                    double totalArea = 0, upArea = 0, beforeArea = 0, afterArea = 0;

                    //总面积宽度
                    int totalWidth = System.Math.Abs(scope[0] - scope[1]);

                    //总面积
                    totalArea = (datas[scope[0]] > datas[scope[1]] ?
                        (datas[scope[0]] - datas[MinIndex]) : (datas[scope[1]] - datas[MinIndex])) * totalWidth;

                    //上部分面积
                    upArea = System.Math.Abs(datas[scope[0]] - datas[scope[1]]) * totalWidth * 0.5;

                    //前面部分面积
                    for (int j = scope[0]; j < MinIndex; j++)
                    {
                        try
                        {
                            beforeArea += (datas[j] - datas[j + 1]) * 0.5 + (datas[j + 1] - datas[MinIndex]);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    //后面部分面积
                    for (int j = MinIndex; j < scope[1]; j++)
                    {
                        try
                        {
                            afterArea += (datas[j + 1] - datas[j]) * 0.5 + (datas[j] - datas[MinIndex]);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    val = (float)(totalArea - upArea - beforeArea - afterArea);
                }
                //计算斜率，不计算波谷点和波峰点
                else if (type.Equals("X"))
                {
                    //向前计算
                    val1 = (float)(Math.Abs(datas[scope[0] + 1] - datas[MinIndex - 1]));
                    //向后计算
                    val2 = (float)(Math.Abs(datas[scope[1] - 1] - datas[MinIndex + 1]));
                    //平均
                    val = (val1 + val2);// / 2;
                }
                //计算差值
                else if (type.Equals("C"))
                {
                    //向前计算
                    val1 = (float)(Math.Abs(datas[scope[0] + 1] - datas[MinIndex - 1]));
                    //向后计算
                    val2 = (float)(Math.Abs(datas[scope[1] - 1] - datas[MinIndex + 1]));
                    //平均
                    val = (val1 + val2) / 2;
                }
                //计算有效点
                else if (type.Equals("P"))
                {
                    //向前计算
                    val1 = (float)(Math.Abs(scope[0] - MinIndex));
                    //向后计算
                    val2 = (float)(Math.Abs(scope[1] - MinIndex));
                    //平均
                    val = (val1 + val2) / 2;
                }

                return val;
            }

            /// <summary>
            /// 计算原始值曲线的面积
            /// </summary>
            /// <param name="datas">数据</param>
            /// <param name="index">波谷T坐标</param>
            /// <param name="maxIndex1">往前推的波峰坐标</param>
            /// <param name="maxIndex2">往后推的波峰坐标</param>
            /// <returns></returns>
            public static double GetArea(double[] datas, int index, int maxIndex1, int maxIndex2)
            {
                //原始值曲线面积
                //totalArea总面积，upArea上部分面积，beforeArea前面部分面积，afterArea后面部分面积
                double area = 0, totalArea = 0, upArea = 0, beforeArea = 0, afterArea = 0;

                //总面积宽度
                int totalWidth = System.Math.Abs(maxIndex1 - maxIndex2);

                //总面积
                totalArea = (datas[maxIndex1] > datas[maxIndex2] ?
                    (datas[maxIndex1] - datas[index]) : (datas[maxIndex2] - datas[index])) * totalWidth;

                //上部分面积
                upArea = System.Math.Abs(datas[maxIndex1] - datas[maxIndex2]) * totalWidth * 0.5;

                //前面部分面积
                for (int j = maxIndex1; j < index; j++)
                {
                    try
                    {
                        beforeArea += (datas[j] - datas[j + 1]) * 0.5 + (datas[j + 1] - datas[index]);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                //后面部分面积
                for (int j = index; j < maxIndex2; j++)
                {
                    try
                    {
                        afterArea += (datas[j + 1] - datas[j]) * 0.5 + (datas[j] - datas[index]);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                area = totalArea - upArea - beforeArea - afterArea;

                return area;
            }

            /// <summary>
            /// 计算强拉成规则波形的面积
            /// </summary>
            /// <param name="datas">数据</param>
            /// <param name="index">波谷T坐标</param>
            /// <param name="maxIndex1">往前推的波峰坐标</param>
            /// <param name="maxIndex2">往后推的波峰坐标</param>
            /// <returns></returns>
            public static double GetRulesArea(double[] datas, int index, int maxIndex1, int maxIndex2)
            {
                double area = 0, totalArea = 0, upArea = 0, beforeArea = 0, afterArea = 0;
                int totalWidth = System.Math.Abs(maxIndex1 - maxIndex2) / 2;
                totalArea = Math.Abs((datas[maxIndex1] + datas[maxIndex2]) / 2 - datas[index]) * totalWidth;
                int idx = 1;
                for (int j = index; j < maxIndex2; j++)
                {
                    try
                    {
                        beforeArea += ((datas[index + idx] + datas[index - idx]) / 2 - datas[index]) / 2;
                        beforeArea += (datas[index + idx - 1] + datas[index - idx + 1]) / 2 - datas[index];
                        idx++;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                area = (totalArea - upArea - beforeArea - afterArea) * 2;

                return area;
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
        /// <summary>
        /// 检查网络是否可以连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 统一代码定义描述
        /// </summary>
        /// <param name="code">传入的code</param>
        /// <returns>返回内容</returns>
        public static string GetResultByCode(string code)
        {
            string rtn = string.Empty;
            if (code.Equals("000000"))
            {
                rtn = "服务器程序出错了。";
            }
            else if (code.Equals("000001"))
            {
                rtn = "传入的xml数据项不能为空。";
            }
            else if (code.Equals("000002"))
            {
                rtn = "传入的用户名不能为空。";
            }
            else if (code.Equals("000003"))
            {
                rtn = "传入的用户密码不能为空。";
            }
            else if (code.Equals("000004"))
            {
                rtn = "传入的检测单位编号不能为空。";
            }
            else if (code.Equals("000005"))
            {
                rtn = "用户名或密码错误。";
            }
            else if (code.Equals("000006"))
            {
                rtn = "与当前用户所属检测点编号不符。";
            }
            else if (code.Equals("000007"))
            {
                rtn = "数据解析入库出错。";
            }
            else if (code.Equals("000008"))
            {
                rtn = "未知错误，请联系技术人员进行处理。";
            }
            else if (code.Equals("000009"))
            {
                rtn = "当前用户不存在检测点或组织机构。";
            }
            else if (code.Equals("000010"))
            {
                rtn = "版本参数不能为空。";
            }
            else if (code.Equals("000011"))
            {
                rtn = "版本参数不正确。";
            }
            else if (code.Equals("000012"))
            {
                rtn = "下载数据标志不能为空。";
            }
            else if (code.Equals("000013"))
            {
                rtn = "下载标志为sign的基础数据下载出错。";
            }
            else if (code.Equals("000014"))
            {
                rtn = "传入的下载标志不存在。";
            }
            else if (code.Equals("001000"))
            {
                rtn = "1";//用户验证成功
            }
            else if (code.Equals("001001"))
            {
                rtn = "1";//一共处理了n条数据,全部上传成功
            }
            else if (code.Equals("001002"))
            {
                rtn = "2";//一共处理了n条数据,x条数据上传成功,y条数据更新成功,z条数据不符合要求。
            }
            else if (code.Equals("001003"))
            {
                rtn = "1";//下载标志为sign的基础数据下载成功。
            }
            else
            {
                rtn = "发生未知错误，请联系管理员。";
            }
            return rtn;
        }

        /// <summary>
        /// 新版本接口-数据下载 2016年12月8日 wenj
        /// </summary>
        /// <param name="version">版本：“行政版”或“企业版”</param>
        /// <param name="url">服务器地址</param>
        /// <param name="username">操作用户名</param>
        /// <param name="pwd">md5加密后的用户密码</param>
        /// <param name="sign">下载数据标志，all（全部），FoodClass（样品类别），CheckComTypeOpr（检测点类型），StandardType（检测标准类型），
        ///                     Standard（检测标准），CheckItem（检测项目），CompanyKind（被监管对象，单位类别） District（行政机构），
        ///                     ProduceArea（产品产地），Company（被监管对象），Dealer（被检经营户），CheckPlan（检测点检测任务下载），
        ///                     SelectItem（样品、检测项目和对应标准）</param>
        /// <param name="udate">更新时间，时间格式为：“2016-01-01”或 “2016-01-01 12：00：01”</param>
        /// <returns>XML字符串</returns>
        public static string GetXmlByService(string version, string url, string username, string pwd, string sign, string udate)
        {
            string rtnStr = string.Empty;
            try
            {
                NewInterface.dataSync ds = new NewInterface.dataSync
                {
                    Url = url
                };
                rtnStr = ds.downLoadBaseData(version, username,
                    FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), sign, udate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtnStr;
        }

        /// <summary>
        /// 新版本接口-数据上传 2016年12月12日 wenj
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="username">操作用户名</param>
        /// <param name="pwd">加密后操作密码</param>
        /// <param name="companyCode">当前检测点单位编号</param>
        /// <param name="url">上传URL</param>
        /// <returns>XML字符串</returns>
        public static string Upload(string xml, string username, string pwd, string companyCode, string url)
        {
            string rtn = string.Empty;
            try
            {
                NewInterface.dataSync ds = new NewInterface.dataSync
                {
                    Url = url
                };
                rtn = ds.uploadCheckData(xml, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), companyCode);
            }
            catch (Exception ex)
            {
                rtn = ex.Message;
            }
            return rtn;
        }

        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>分好页的DataTable数据</returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newDtbl = dt.Copy();
            newDtbl.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newDtbl; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newDtbl.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newDtbl.Rows.Add(newdr);
            }
            return newDtbl;
        }

        /// <summary>
        /// 返回分页的页数
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <returns>如果结尾为0：则返回1</returns>
        public static int PageCount(int count, int pageSize)
        {
            int pageCount = 0;
            if (count % pageSize == 0) { pageCount = count / pageSize; }
            else { pageCount = (count / pageSize) + 1; }
            if (pageCount == 0) { pageCount += 1; }
            return pageCount;
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
        /// 对List的某一元素进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="field"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static List<T> ListSort<T>(List<T> list, string field, string rule = "asc")
        {
            if (!string.IsNullOrEmpty(rule) && (!rule.ToLower().Equals("desc") || !rule.ToLower().Equals("asc")))
            {
                try
                {
                    list.Sort(
                        delegate(T info1, T info2)
                        {
                            Type t1 = info1.GetType();
                            Type t2 = info2.GetType();
                            PropertyInfo pro1 = t1.GetProperty(field);
                            PropertyInfo pro2 = t2.GetProperty(field);
                            return rule.ToLower().Equals("asc") ?
                                pro1.GetValue(info1, null).ToString().CompareTo(pro2.GetValue(info2, null).ToString()) :
                                pro2.GetValue(info2, null).ToString().CompareTo(pro1.GetValue(info1, null).ToString());
                        });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return list;
        }

        /// <summary>
        /// 验证当前样品和检测项目，在标准库中是否有值
        /// </summary>
        /// <param name="item">检测项目名称</param>
        /// <param name="food">样品名称</param>
        /// <returns>true 为空，false 不为空</returns>
        public static bool CheckItemAndFoodIsNull(string item, string food)
        {
            bool IsNull = true;
            try
            {
                DataTable dtbl = new clsttStandardDecideOpr().GetAsDataTable(string.Format("FtypeNmae = '{0}' and Name = '{1}'", food, item), string.Empty, 0);
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    IsNull = false;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return IsNull;
        }

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

        /// <summary>
        /// 快检服务相关
        /// </summary>
        public static class KjServer
        {
            /// <summary>
            /// 服务器地址
            /// </summary>
            public static string KjServerAddr = string.Empty;
            /// <summary>
            /// 用户名
            /// </summary>
            public static string Kjuser_name = string.Empty;
            /// <summary>
            /// 密码
            /// </summary>
            public static string Kjpassword = string.Empty;
            /// <summary>
            /// 昵称
            /// </summary>
            public static string Kjrealname = string.Empty;
            /// <summary>
            /// 所属机构
            /// </summary>
            public static string Kjd_depart_name = string.Empty;
            /// <summary>
            /// 检测点名称
            /// </summary>
            public static string Kjp_point_name = string.Empty;
            /// <summary>
            /// 仪器型号
            /// </summary>
            public static string Kjseries = string.Empty;
            /// <summary>
            /// 接口返回结果集
            /// </summary>
            public static DyInterfaceHelper.KjService.Response response = null;
            /// <summary>
            /// 用户登录
            /// </summary>
            public static DyInterfaceHelper.KjService.UserLoginEntity.Obj userLoginEntity = null;
            /// <summary>
            /// 仪器注册
            /// </summary>
            public static DyInterfaceHelper.KjService.RegisterDeviceEntity registerDeviceEntity = null;
            /// <summary>
            /// 抽样任务
            /// </summary>
            public static DyInterfaceHelper.KjService.ReceiveTasksEntity.Obj receiveTasksEntity = null;
            /// <summary>
            /// 选择的ReceiveTask任务
            /// </summary>
            public static DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem _selectReceiveTasks = null;
            /// <summary>
            /// 检测任务
            /// </summary>
            public static DyInterfaceHelper.KjService.DownloadSamplingEntity.Obj samplingEntity = null;
            /// <summary>
            /// Tasks界面选中的检测任务，在检测中需要用到
            /// </summary>
            public static List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem> _selectCheckTasks = null;
            /// <summary>
            /// 公告
            /// </summary>
            public static List<DyInterfaceHelper.KjService.DownloadTaskMsgEntity> taskMsgEntitys = null;
            /// <summary>
            /// 监管对象
            /// </summary>
            public static List<DyInterfaceHelper.KjService.Regulatory.RegulatoryItem> companys = null;
            /// <summary>
            /// 经营户
            /// </summary>
            public static List<DyInterfaceHelper.KjService.BusinessEntity.Business> business = null;
            public static List<DyInterfaceHelper.KjService.BusinessEntity.Business> bcount = null;
            
            /// <summary>
            /// 选择的监管对象
            /// </summary>
            public static DyInterfaceHelper.KjService.Regulatory.RegulatoryItem _selectCompany = null;
            /// <summary>
            /// 选择的经营户
            /// </summary>
            public static DyInterfaceHelper.KjService.BusinessEntity.Business _selectBusiness = null;

            /// <summary>
            /// 根据项目名称查找当前模块是否支持
            /// </summary>
            /// <param name="item">项目名称</param>
            /// <param name="type">1为分光；2为胶体金；3为干化学</param>
            /// <returns></returns>
            public static string GetResultType(string item, int type)
            {
                string resultType = string.Empty;
                switch (type)
                {
                    case 1://分光
                        for (int i = 0; i < Global.fgdItems.Count; i++)
                        {
                            DYFGDItemPara FGDitem = Global.fgdItems[i];
                            if (item.Equals(FGDitem.Name))
                            {
                                resultType = "分光光度";
                                break;
                            }
                        }
                        break;

                    case 2://胶体金
                        //和胶体金模块的检测项目对比
                        for (int i = 0; i < Global.jtjItems.Count; i++)
                        {
                            DYJTJItemPara JTJitem = Global.jtjItems[i];
                            if (item.Equals(JTJitem.Name))
                            {
                                resultType = "胶体金";
                                break;
                            }
                        }
                        break;

                    case 3://干化学
                        //和干化学模块的检测项目对比
                        for (int i = 0; i < Global.gszItems.Count; i++)
                        {
                            DYGSZItemPara GSZitem = Global.gszItems[i];
                            if (item.Equals(GSZitem.Name))
                            {
                                resultType = "干化学";
                                break;
                            }
                        }
                        break;

                    default://在上述三个模块中找到第一个适配的模块
                        //先从分光光度模块中对比检测项目，如果没有继续和胶体金和干化学模块对比
                        for (int i = 0; i < Global.fgdItems.Count; i++)
                        {
                            DYFGDItemPara FGDitem = Global.fgdItems[i];
                            if (item.Equals(FGDitem.Name))
                            {
                                resultType = "分光光度";
                                break;
                            }
                        }

                        if (resultType.Length > 0) break;

                        //和胶体金模块的检测项目对比
                        for (int i = 0; i < Global.jtjItems.Count; i++)
                        {
                            DYJTJItemPara JTJitem = Global.jtjItems[i];
                            if (item.Equals(JTJitem.Name))
                            {
                                resultType = "胶体金";
                                break;
                            }
                        }

                        if (resultType.Length > 0) break;

                        //和干化学模块的检测项目对比
                        for (int i = 0; i < Global.gszItems.Count; i++)
                        {
                            DYGSZItemPara GSZitem = Global.gszItems[i];
                            if (item.Equals(GSZitem.Name))
                            {
                                resultType = "干化学";
                                break;
                            }
                        }
                        break;
                }

                return resultType;
            }

        }

        /// <summary>
        /// string转Double
        /// 2016年3月11日 wenj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double StringConvertDouble(string str)
        {
            double valueInt = 0;
            if (!double.TryParse(str, out  valueInt))
                return 0;
            return valueInt;
        }

        public static string HttpMath(string type, string url, string paramsValue, Encoding encoding)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                //自定义header
                //request.Headers.Add("userName", "admin");
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

                using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
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

        /// <summary>
        /// 移除表中检测无效的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable CheckDtblValue(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0) return null;
            string val = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                val = dt.Rows[i]["Result"].ToString();
                if (val.Equals("检测无效") || val.Equals("NA") || val.Equals("*"))
                {
                    dt.Rows.RemoveAt(i);
                    CheckDtblValue(dt);
                    break;
                }
            }
            return dt;
        }

        public static bool UploadCheck(Window wnd, out string Info)
        {
            Info = string.Empty;
            if (!IsConnectInternet())//检测网络
            {
                IsStartUploadTimer = true;
                MessageBox.Show(wnd, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Info = "无网络连接";
                return false;
            }

            if (InterfaceType.Equals("DY") || InterfaceType.Equals("GS"))//达元||甘肃 平台
            {
                if (samplenameadapter == null || samplenameadapter.Count == 0 || samplenameadapter[0].UploadUserUUID == null || samplenameadapter[0].UploadUserUUID.Length == 0)
                {
                    if (MessageBox.Show(wnd, "还未进行服务器通讯测试，可能导致数据上传失败！\r\n是否前往【设置】界面进行通讯测试？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow();
                        window.ShowDialog();
                    }
                    else
                    {
                        Info = "取消上传";
                    }
                    return false;
                }
            }

            if (InterfaceType.Equals("ZH"))//广东省智慧云平台
            {
                if (Global.DeviceID.Length == 0 || Wisdom.USER.Length == 0 || Wisdom.PASSWORD.Length == 0)
                {
                    if (MessageBox.Show(wnd, "【无法上传】 - 服务器链接配置异常，是否立即前往【设置】界面进行配置？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow()
                        {
                            DeviceIdisNull = false
                        };
                        window.ShowDialog();
                    }
                    else
                    {
                        Info = "取消上传";
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 日志打印
        /// </summary>
        /// <param name="title"></param>
        /// <param name="conotent"></param>
        public static void Log(string title, string content)
        {
            System.Console.WriteLine(string.Format("{0}\r\n--------------------{1}--------------------\r\n{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), title, content));
        }


        public static string pointId { get; set; }

        public static string pointName { get; set; }

        public static string nickName { get; set; }

        public static string userId { get; set; }

        public static void GetJtjVersion()
        {
            //获取胶体金硬件版本号
            if (JtjVersionInfo == null )
            {
                Message msg = new Message
                {
                    what = MsgCode.MSG_GET_VERSION,
                    str1 = strSXT1PORT,
                   
                };
                workThread.SendMessage(msg, null);
            }
         
        }

    }
}