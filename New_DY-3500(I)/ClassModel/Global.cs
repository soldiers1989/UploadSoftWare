using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using OtherModel;
using DataSetModel.DataModel;
using System.Management;

namespace ClassModel
{
    /// <summary>
    /// 全局变量类
    /// </summary>
    public class Global
    {
        #region 接快检服务平台的变量
        public static string TasknumTime = string.Empty;

        public static int Gitem = 0;

        public static string d_depart_name = "";
        public static string depart_id = "";
        public static string p_point_name = "";
        public static string point_id = "";
        public static string user_name = "";
        public static string id = "";
        public static string realname = "";

        public static string samplingnumRecive = "";

        /// <summary>
        /// setPointNum 检测点编号|setPonitName 检测点名称|
        /// setOrgNum 行政机关编号|setOrgName 所属机构
        /// </summary>
        public static string pointNum = string.Empty, pointName = string.Empty, pointType = string.Empty, orgNum = string.Empty, orgName = string.Empty, userId = string.Empty, nickName = string.Empty, pointId = string.Empty, orgID = string.Empty;
        /// <summary>
        /// 快检服务返回的Token
        /// </summary>
        public static string Token = string.Empty;
        /// <summary>
        /// 仪器系列号
        /// </summary>
        public static string MachineNum = string.Empty;
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
    
        public static string userAccountsFile = AccountsDirectory + "\\" + "userAccounts.dat";
        public static List<deviceItem> FGItem;//分光光度检测项目
        public static List<KJFWJTItem> JTItem;//胶体金检测项目
        public static List<KJFWGHXItem> GHItem;//干化学检测项目
        /// <summary>
        /// 仪器型号
        /// </summary>
        public static string MachineModel = string.Empty;

        #endregion

        #region  快检服务权限管理变量
        /// <summary>
        /// 检测
        /// </summary>
        public static bool Detectionbtn = false;
        /// <summary>
        /// 重检
        /// </summary>
        public static bool redetectionbtn = false;
        /// <summary>
        /// 数据中心
        /// </summary>
        public static bool DataCenterbtn = false;
        /// <summary>
        /// 检测项目
        /// </summary>
        public static bool testitembtn = false;
        /// <summary>
        /// 检测标准
        /// </summary>
        public static bool teststandbtn = false;
        /// <summary>
        /// 食品种类
        /// </summary>
        public static bool foodtypebtn = false;
        /// <summary>
        /// 仪器检测项目
        /// </summary>
        public static bool machineitembtn = false;
        /// <summary>
        /// 样品检测标准
        /// </summary>
        public static bool samplestdbtn = false;
        /// <summary>
        /// 法律法规
        /// </summary>
        public static bool lawsbtn = false;
        /// <summary>
        /// 检测数据、检测记录
        /// </summary>
        public static bool testdatabtn = false;
        /// <summary>
        /// 设置
        /// </summary>
        public static bool Setting = false;
        /// <summary>
        /// 系统更新
        /// </summary>
        public static bool sysupdatebtn = false;
        /// <summary>
        /// 接收任务
        /// </summary>
        public static bool Receivetask = false;
        /// <summary>
        /// 拒收任务
        /// </summary>
        public static bool Refusetask = false;
        /// <summary>
        /// 被检单位管理
        /// </summary>
        public static bool checkcompanuy = false;
        /// <summary>
        /// 手动检测
        /// </summary>
        public static bool ShoudongTest = false;
        /// <summary>
        /// 打印
        /// </summary>
        public static bool print = false;
        /// <summary>
        /// 编辑
        /// </summary>
        public static bool edition = false;
        /// <summary>
        /// 上传
        /// </summary>
        public static bool Uploadd = false;
        /// <summary>
        /// 导出
        /// </summary>
        public static bool output = false;
        /// <summary>
        /// 导入
        /// </summary>
        public static bool Input = false;
        /// <summary>
        /// 生成报告
        /// </summary>
        public static bool GenerateReport = false;
        /// <summary>
        /// 打印报告
        /// </summary>
        public static bool PrintReport = false;
        /// <summary>
        /// 是否已注册
        /// </summary>
        public static bool isresige = false;
        /// <summary>
        /// 食品教程
        /// </summary>
        public static bool vedioTV = false;
        /// <summary>
        /// 食品教程
        /// </summary>
        public static bool Instruction = false;
        /// <summary>
        /// 被检单位重置
        /// </summary>
        public static bool ResetCompany = false;
        /// <summary>
        /// 检测项目重置
        /// </summary>
        public static bool ResetItem = false;
        /// <summary>
        /// 检测标准重置
        /// </summary>
        public static bool ResetStandard = false;
        /// <summary>
        /// 食品种类重置
        /// </summary>
        public static bool ResetSampleType = false;
        /// <summary>
        /// 仪器检测项目重置
        /// </summary>
        public static bool ResetMachineItem = false;
        /// <summary>
        /// 样品检测标准重置
        /// </summary>
        public static bool ResetSampleStd = false;
        /// <summary>
        /// 法律法规重置
        /// </summary>
        public static bool ResetLaws = false;
        /// <summary>
        /// 修改的通道号
        /// </summary>
        public static string RepairHole = "";

        #endregion

        /// <summary>
        /// DY 达元平台；ZH 广东省智慧云平台；KJC 快检车平台
        /// </summary>
        public static string InterfaceType = string.Empty;
        /// <summary>
        /// 上传成功的数据条数
        /// </summary>
        public static int UploadSCount = 0;
        /// <summary>
        /// 上传失败的数据条数
        /// </summary>
        public static int UploadFCount = 0;

        public static int UpY = 0;

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
        /// 获取MAC地址
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
    }
}
