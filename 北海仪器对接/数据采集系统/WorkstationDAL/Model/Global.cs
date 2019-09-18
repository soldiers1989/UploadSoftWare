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
        /// 测试仪器
        /// </summary>
        public static string ChkManchine = string.Empty;//测试仪器
        /// <summary>
        /// 检测仪器编号
        /// </summary>
        public static string MachineNum = "";

        public static bool ComON = false;    //判断COM口是否打开
        #region 记录按钮单击事件,切换主页背景突出显示
        public static bool MainPage=false ;    //主页
        public static bool SearchData = false; //数据采集
        public static bool Dairypb = false;    //操作日记
        public static bool SysSet = false;     //系统设计
        public static bool Shop = false;//商城

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
        /// 北海采样时间
        /// </summary>
        public static string Bgettime = "";
        /// <summary>
        /// 北海采样地点
        /// </summary>
        public static string Bgetplace = "";
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string BcheckCommpany = "";

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
        public static string BCheckUnitName = "";
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

        #region 阳谷县企业信息
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
    }
}
