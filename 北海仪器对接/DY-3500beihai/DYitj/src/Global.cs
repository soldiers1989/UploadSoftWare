using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Security;
using System.Windows;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    /// <summary>
    /// 存放和项目相关的全局变量
    /// </summary>
    public class Global
    {
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
        public static string strSERVERADDR = string.Empty;
        public static string REGISTERID = string.Empty;
        public static string REGISTERPASSWORD = string.Empty;
        public static string CHECKPOINTID = string.Empty;
        public static string CHECKPOINTNAME = string.Empty;
        public static string CHECKPOINTTYPE = string.Empty;
        public static string ORGANIZATION = string.Empty;
        public static string strADPORT = string.Empty;
        public static string strSXT1PORT = string.Empty;
        public static string strSXT2PORT = string.Empty;
        //-NewAdd2016-06-20
        public static string strSXT3PORT = string.Empty;
        public static string strSXT4PORT = string.Empty;
        public static string strPRINTPORT = string.Empty;
        public static string strHMPORT = string.Empty;
        public static string strWSWPATH = string.Empty;
        public static string strGZZPATH = string.Empty;
        /// <summary>
        /// 是否进行测试，Y为是 
        /// </summary>
        public static Boolean IsTest = false;
        public static Double Standard1 = 0;
        public static Double Standard2 = 0;
        public static Double Standard3 = 0;
        public static Double Standard4 = 0;
        /// <summary>
        /// 单通道判定标准
        /// </summary>
        public static Double DecisionCriteria1 = 0;
        /// <summary>
        /// 通道间差判定标准
        /// </summary>
        public static Double DecisionCriteria2 = 0;
        public static UpdateServer updateServer;
        public static WorkThread workThread = null;
        public static WorkThread printThread = null;
        public static WorkThread updateThread = null;
        /// <summary>huowu.
        /// 存储分光度、干化学、重金属、胶体金是否有启用，用于设置界面的控件是否启用判断
        /// </summary>banlang
        public static bool set_IsOpenFgd = true, set_IsOpenJtj = true, set_IsOpenGhx = true, set_IsOpenZjs = true;
        public static string sampleName = string.Empty, projectName = string.Empty, projectUnit = string.Empty, Bcompany="",BproductID="",BID="";
        /// <summary>
        /// 存储样品小精灵是否从项目名称弹出
        /// </summary>
        public static bool IsProject = false;
        /// <summary>
        /// 干试纸|gsz，重金属|zjs，分光度|fgd，胶体金|jtj
        /// </summary>
        public static string typeName = string.Empty;
        /// <summary>
        /// 是否启用微生物
        /// </summary>
        public static Boolean IsEnableWswOrAtp = false;
        /// <summary>
        /// 是微生物还是ATP
        /// </summary>
        public static string IsWswOrAtp = "WSW";
        /// <summary>
        /// 微生物软件存放地址
        /// </summary>
        public static string MicrobialAddress = string.Empty;
        /// <summary>
        /// ATP是否打开指定文件 true为文件；false为文件夹
        /// </summary>
        public static Boolean IsOpenFile = false;
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
        public static string Other = string.Empty;
        /// <summary>
        /// 等待界面是否关闭
        /// </summary>
        public static bool WaitingWindowIsClose = false;
        /// <summary>
        /// 是否进行服务器通讯测试
        /// </summary>
        public static bool IsServerTest = true;
        public static string GetTaskSampleName() 
        {
            try
            {
                string[] strList = Other.Split('：');
                if (strList != null && strList.Length > 0)
                {
                    string[] sampleList = strList[1].Split('，');
                    if (sampleList != null && sampleList.Length > 0)
                    {
                        Other = sampleList[0];
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return Other;
        }
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string CompanyName = string.Empty;
        /// <summary>
        /// 是否启用视频播放功能
        /// </summary>
        public static Boolean IsEnableVideo = false;
        /// <summary>
        /// 视频类型 all|fgd|jtj|ghx|zjs
        /// </summary>
        public static string videoType = string.Empty;
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
        public static Boolean IsPlayer = false;
        /// <summary>
        /// 是否启用设置界面【故障检测】按钮
        /// </summary>
        public static Boolean set_FaultDetection = false;
        /// <summary>
        /// 是否启用设置界面【分光光度检测通道配置】
        /// </summary>
        public static Boolean set_ShowFgd = false;
        /// <summary>
        /// setPointNum 检测点编号|setPonitName 检测点名称|
        /// setOrgNum 行政机关编号|setOrgName 所属机构
        /// </summary>
        public static string pointNum = string.Empty, pointName = string.Empty, pointType = string.Empty, orgNum = string.Empty, orgName = string.Empty, userId = string.Empty, nickName = string.Empty, pointId = string.Empty;
        /// <summary>
        /// 是否调整项目坐标
        /// </summary>
        public static Boolean IsSetIndex = false;
        /// <summary>
        /// 是否已经打开了简要提示界面
        /// </summary>
        public static Boolean IsOpenPrompt = false;
        /// <summary>
        /// 是否启用删除功能
        /// </summary>
        public static Boolean IsDELETED = false;

        /// <summary>
        /// 所属地区:GS 甘肃|string.Empty 为通用
        /// </summary>
        public static string EachDistrict = string.Empty;
        public static string GSType = string.Empty;
        /// <summary>
        /// 检测数据导出类型，目前有两种方式：Excle|Txt
        /// </summary>
        public static string ExportType = string.Empty;
        /// <summary>
        /// 是否允许修改检测值 
        /// </summary>
        public static Boolean IsUpdateChekcedValue = false;
        public static double xValue = 0;
        public static double yValue = 0;
        public static double timeValue = 0;
        public static bool IsContrast = false;
     
        /// <summary>
        /// 上传成功的数据条数
        /// </summary>
        public static int UploadSCount = 0;
        /// <summary>
        /// 上传失败的数据条数
        /// </summary>
        public static int UploadFCount = 0;
        /// <summary>
        /// 仪器名称
        /// </summary>
        public static string InstrumentName = string.Empty;
        /// <summary>
        /// 仪器型号
        /// </summary>
        public static string InstrumentNameModel = string.Empty;
        /// <summary>
        /// DY 达元平台；ZH 广东省智慧云平台；KJC 快检车平台
        /// </summary>
        public static string InterfaceType = string.Empty;
        /// <summary>
        /// 通过选择快检单号时是否自动选择对应样品名称
        /// </summary>
        public static Boolean IsSelectSampleName = false;
        /// <summary>
        /// 北海仪器通信测试返回的用户单位标识
        /// </summary>
        public static string UnitID = string.Empty;
        public static string UnitName = string.Empty;

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
            string filePath = Global.TxtItemsDirectory + "\\检测项目对应项.txt";
            if (File.Exists(filePath))
                File.Delete(filePath);
            for (int i = 0; i < Global.fgdItems.Count; ++i)
            {
                DYFGDItemPara item = Global.fgdItems[i];
                FileUtils.AddToFile(filePath, "{分光光度#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
            }

            for (int i = 0; i < Global.jtjItems.Count; ++i)
            {
                DYJTJItemPara item = Global.jtjItems[i];
                FileUtils.AddToFile(filePath, "{胶体金#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
            }

            for (int i = 0; i < Global.gszItems.Count; ++i)
            {
                DYGSZItemPara item = Global.gszItems[i];
                FileUtils.AddToFile(filePath, "{干化学#" + item.Name + ":-1:1:" + item.Unit + "}\r\n");
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
        /// String转Int
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
        /// String转Double
        /// 2016年3月11日 wenj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double StringConvertDouble(string str)
        {
            double valueInt=0;
            if (!double.TryParse(str, out  valueInt))
                return 0;
            return valueInt;
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
            public static Boolean FindTheHid()
            {
                try
                {
                    String strvid = "0483", strpid = "5750";
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
                    catch (SystemException)
                    {
                        throw;
                    }
                    if (MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                    {
                        GetCommunication();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return false;
            }

            /// <summary>
            /// 获取通讯
            /// </summary>
            /// <returns></returns>
            public static void GetCommunication()
            {
                //建立通讯
                String cmd = "0xFF 0x08 0x30 0x00 0x00 0x00 0x38 0xFE";
                byte[] bt = ReadAndWriteToDevice(cmd);
            }

            /// <summary>
            /// 获取数据指令
            /// </summary>
            /// <returns></returns>
            public static String GetCmd(int index)
            {
                String str = "0x08 0x31 0x00 0x00 0x" + index.ToString("X2");
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
            public static byte[] ReadAndWriteToDevice(String cmd)
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
                catch (Exception)
                {
                    throw;
                }
                return inputdatas;
            }

            public static List<byte[]> GetByteList(byte[] data)
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
                    catch (SystemException)
                    {
                        throw;
                        //MessageBox.Show("发现不可转换的字符串->" + strBytes[i]);
                    }
                }
                return _return;
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
        /// ping一个IP或域名是否能ping通
        /// </summary>
        /// <param name="strIpOrDName"></param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions()
                {
                    DontFragment = true
                };
                string data = string.Empty;
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 5000;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success") return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
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
        /// <returns></returns>
        public static string GETGUID(string format = null, int strCase = 1)
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
        /// 判定字符串是否是GUID
        /// </summary>
        /// <param name="strSrc"></param>
        /// <returns></returns>
        public static bool IsGuidByReg(string strSrc)
        {
            try
            {
                Guid guid = new Guid(strSrc);
                return true;
            }
            catch
            {
                return false;
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
        /// 根据不同的标识，从服务器获取不同的数据
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="sign">下载标识</param>
        /// <param name="udate">最后更新时间</param>
        /// <param name="version">版本信息</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetDataTableByService(string url, string user, string pwd, string sign, string udate, string version)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            try
            {
                string rtnXml = GetXmlByService(version, url, user, pwd, sign, udate);
                using (StringReader sr = new StringReader(rtnXml)) dataSet.ReadXml(sr);

                dataTable = dataSet.Tables["Result"];
                string result = GetResultByCode(dataTable.Rows[0]["ResultCode"].ToString());
                if (result.Equals("1")) dataTable = dataSet.Tables[sign];
            }
            catch (Exception)
            {
                return new DataTable();
            }

            return dataTable ?? new DataTable();
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
        public static string GetXmlByService(String version, String url, String username, String pwd, String sign, String udate)
        {
            string rtnStr = string.Empty;
            try
            {
                NewInterface.dataSync ds = new NewInterface.dataSync()
                {
                    Url = url
                };
                rtnStr = ds.downLoadBaseData(version, username,
                    FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), sign, udate);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return rtnStr;
        }

        /// <summary>
        /// 根据样品编号获取样品信息
        /// Create wenj 2017年2月28日
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="sampleCode">样品编号</param>
        /// <param name="url">url</param>
        /// <returns>返回结果</returns>
        public static string LoadSamplingData(string user, string pwd, string sampleCode, string url)
        {
            string rtn = string.Empty;
            //try
            //{
            //    NewInterface.dataSync ds = new NewInterface.dataSync()
            //    {
            //        Url = url
            //    };
            //    rtn = ds.loadSamplingData(user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), sampleCode);
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
            return rtn;
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
        public static string Upload(String xml, String username, String pwd, String companyCode, String url)
        {
            string rtn = string.Empty;
            try
            {
                NewInterface.dataSync ds = new NewInterface.dataSync()
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
        /// 快检车项目相关
        /// </summary>
        public static class Checkcar 
        {
            /// <summary>
            /// 根据样品编号获取样品信息
            /// </summary>
            /// <param name="sampleCode"></param>
            /// <returns></returns>
            public static List<clsTB_SAMPLING> GetSampling(string sampleCode) 
            {
                List<clsTB_SAMPLING> models = null;
                DataTable dt = null;
                try
                {
                    dt = WisdomCls.GetSample(string.Format("SAMPLENO = '{0}'", sampleCode), string.Empty, 0);
                    if (dt != null && dt.Rows.Count > 0)
                        models = (List<clsTB_SAMPLING>)IListDataSet.DataTableToIList<clsTB_SAMPLING>(dt, 1);
                }
                catch (Exception)
                {
                    return null;
                }
                return models;
            }

            public static int Insert(DataTable dataTable) 
            {
                int rtn = 0;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    try
                    {
                        clsTB_SAMPLING model = new clsTB_SAMPLING();
                        model.SAMPLINGNO = dataTable.Rows[i]["SAMPLINGNO"].ToString();
                        model.SAMPLINGDATE = dataTable.Rows[i]["SAMPLINGDATE"].ToString();
                        model.CKOCODE = dataTable.Rows[i]["CKOCODE"].ToString();
                        model.CKONAME = dataTable.Rows[i]["CKONAME"].ToString();
                        model.CDCODE = dataTable.Rows[i]["CDCODE"].ToString();
                        model.CDNAME = dataTable.Rows[i]["CDNAME"].ToString();
                        model.CDBUSLICENCE = dataTable.Rows[i]["CDBUSLICENCE"].ToString();
                        model.CONTACTPERSON = dataTable.Rows[i]["CONTACTPERSON"].ToString();
                        model.PHONE = dataTable.Rows[i]["PHONE"].ToString();
                        model.USERNAME = dataTable.Rows[i]["USERNAME"].ToString();
                        model.USERID = dataTable.Rows[i]["USERID"].ToString();
                        model.SAMPLENO = dataTable.Rows[i]["SAMPLENO"].ToString();
                        model.SAMPLENAME = dataTable.Rows[i]["SAMPLENAME"].ToString();
                        model.CKICOUNT = dataTable.Rows[i]["CKICOUNT"].ToString();
                        model.CKTAKECOUNT = dataTable.Rows[i]["CKTAKECOUNT"].ToString();
                        model.CKTAKEDATE = dataTable.Rows[i]["CKTAKEDATE"].ToString();
                        model.CKPCONAME = dataTable.Rows[i]["CKPCONAME"].ToString();
                        model.SUPPLIER = dataTable.Rows[i]["SUPPLIER"].ToString();
                        model.SUPPADDR = dataTable.Rows[i]["SUPPADDR"].ToString();
                        model.SUPPCONTACT = dataTable.Rows[i]["SUPPCONTACT"].ToString();
                        model.SUPPPHONE = dataTable.Rows[i]["SUPPPHONE"].ToString();
                        model.UDATE = dataTable.Rows[i]["UDATE"].ToString();
                        WisdomCls.InsertSample(model);
                    }
                    catch (Exception)
                    {

                    }
                }
                return rtn;
            }
        }

        /// <summary>
        /// HTTP接口测试
        /// 如果 contentype  "application/x-www-form-urlencoded" 表单类型，那么参数为 a=1&b=2 形式
        /// 如果 contentype  "application/json"  json类型那么参数就为"{a:1,b:2}" 格式
        /// </summary>
        /// <param name="type">接口类型 POST GET SET</param>
        /// <param name="url"></param>
        /// <param name="paramsValue">报文内容</param>
        /// <returns></returns>
        public static string HttpMath(string type,string url, string paramsValue)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                //自定义header
                //request.Headers.Add("password", "test_dy");
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
                throw ex;
            }
            return result;

        }

        public static bool UploadCheck(Window wnd, out string Info)
        {
            Info = string.Empty;
            if (!IsConnectInternet())//检测网络
            {
                MessageBox.Show(wnd, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Info = "无网络连接";
                return false;
            }

            if (InterfaceType.Equals("DY") || InterfaceType.Equals("GS"))//达元||甘肃 平台
            {
                if (samplenameadapter == null || samplenameadapter.Count == 0 || samplenameadapter[0].userId == null || samplenameadapter[0].userId.Length == 0)
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
                if (Wisdom.DeviceID.Length == 0 || Wisdom.USER.Length == 0 || Wisdom.PASSWORD.Length == 0)
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

        public static string DeviceID { get; set; }
    }
}