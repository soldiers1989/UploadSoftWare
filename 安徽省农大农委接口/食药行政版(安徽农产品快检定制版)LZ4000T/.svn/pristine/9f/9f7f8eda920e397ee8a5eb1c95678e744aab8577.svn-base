using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// 公共配置信息类
    /// 原来作者没有加任何注释。
    /// 修改部分代码并另注释，可能会存在理解不正确的地方
	/// </summary>
    public class ShareOption
    {
        /// <summary>
        ///静态类防止外部构造
        /// </summary>
        private ShareOption()
        {
   
        }

        #region 环境参数
        /// <summary>
        /// 温度,编号为010101
        /// </summary>
        public static decimal SysTemperature = 25;

        /// <summary>
        /// 湿度，编号为010102
        /// </summary>
        public static decimal SysHumidity = 40;

        /// <summary>
        /// 单位, 编号为010103
        /// </summary>
        public static string SysUnit = "公斤";
        #endregion

        /// <summary>
        /// 系统名称,2011-06-22新增
        /// </summary>
        public static string SystemTitle = "达元食品安全检测工作站系统";

        /// <summary>
        /// 自动登录,编号为020101
        /// </summary>
        public static bool SysAutoLogin = false;

        /// <summary>
        /// 系统退出时是否弹出提示,对应数据库编号为020103
        /// </summary>
        public static bool SysExitPrompt = false;

        /// <summary>
        ///允许手工录入被检单位中的经营户,对应数据库编号为020105
        /// </summary>
        public static bool AllowHandInputCheckUint = false;

        /// <summary>
        /// 自动生成检测编号,对应数据库编号为020106
        /// </summary>
        public static bool SysStdCodeSame = true;

        /// <summary>
        /// 计时表示1分钟,系统设置 对应数据库编号为020301
        /// </summary>
        public static decimal SysTimer1 = 1;
        //系统设置 编号为020302
        public static decimal SysTimer2 = 3;
        //系统设置 编号为020303
        public static decimal SysTimer3 = 10;
        //系统设置 编号为020304
        public static decimal SysTimer4 = 15;

        /// <summary>
        /// 系统配置声音,对应数据库编号为020305
        /// </summary>
        public static string SysTimerEndPlayWav = "awoke.WAV";

        /// <summary>
        /// 树的最大节点数
        /// </summary>
        public static int MaxLevel = 10;

        /// <summary>
        /// 指示是否只读操作
        /// 固定值只读字段
        /// </summary>
        public static bool DefaultIsReadOnly = false;

        /// <summary>
        /// 是否运行缓存,其实不是缓存，是内存存放持久大对象，有待优化
        /// </summary>
        public static bool IsRunCache = false;

        /// <summary>
        /// 区域数据集
        /// </summary>
        public static DataTable DtblDistrict ;

        /// <summary>
        /// 用户单位名称数据集
        /// </summary>
        public static DataTable DtblCheckCompany ;

        /// <summary>
        /// 检测人数据集
        /// </summary>
        public static DataTable DtblChecker ;

        /// <summary>
        /// 即是生产单位和被检单位,固定值
        /// </summary>
        public static string CompanyProperty0 = "两者都是";

        /// <summary>
        /// 设置单位属性,固定值表示被检单位
        /// </summary>
        public static string CompanyProperty1 = "被检单位";

        /// <summary>
        /// 代表生产单位标签,固定值用于表示生产单位
        /// </summary>
        public static string CompanyProperty2 = "生产单位";

        /// <summary>
        /// 分隔字符,固定值
        /// </summary>
        public static string SplitStr = "/";

        /// <summary>
        ///仪器检测编码格式.可通过数据库配置
        /// </summary>
        public static string FormatStrMachineCode = string.Empty;

        /// <summary>
        /// 手工输入标准检测编码格式。可通过数据库配置
        /// </summary>
        public static string FormatStandardCode = string.Empty;

        /// <summary>
        /// 食品类别编码层次
        /// </summary>
        public static int FoodCodeLevel = 5;
        public static int CompanyCodeLen = 5;
        public static int CompanyKindCodeLen = 3;

        /// <summary>
        /// 行政机构编码层次
        /// </summary>
        public static int DistrictCodeLevel = 3;

        /// <summary>
        /// 检测项目代码长度,固定值
        /// </summary>
        public static int CheckItemCodeLen = 5;

        /// <summary>
        /// 标准码长度,固定值
        /// </summary>
        public static int StandardCodeLen = 4;

        /// <summary>
        /// 仪器代码长度,固定值
        /// </summary>
        public static int MachineCodeLen = 3;

        /// <summary>
        /// 用户单位编码长度
        /// </summary>
        public static int UserUnitCodeLevel = 4;

        /// <summary>
        /// 用户编码长度
        /// </summary>
        public static int UserCodeLen = 2;

        /// <summary>
        /// 被检单位代码长度,固定值6
        /// </summary>
        public static int CompanyStdCodeLen = 6;

        /// <summary>
        /// 
        /// </summary>
        public static int RecordCodeLen = 9;

        /// <summary>
        /// 单位标准码长度
        /// </summary>
        public static int CompanyStdCodeLevel = 6;

        /// <summary>
        /// 检测结果不合格,固定值 Failure
        /// </summary>
        public static string ResultFailure = "不合格";

        /// <summary>
        /// 检测结果合格,固定值 eligibility
        /// </summary>
        public static string ResultEligi = "合格";

        /// <summary>
        /// 用户默认单位代码
        /// </summary>
        public static string DefaultUserUnitCode = "0001";

        /// <summary>
        /// 食品编码类型1
        /// </summary>
        public static string FoodType1 = "0000100001";

        /// <summary>
        /// 食品编码类型2
        /// </summary>
        public static string FoodType2 = "0000100003";

        /// <summary>
        /// 食品编码类型3
        /// </summary>
        public static string FoodType3 = "00001";

        /// <summary>
        /// 是否存数据链接，即标识网络和单机版, 默认true为单机版,false为网络版
        /// </summary>
        public static bool IsDataLink = true;

        /// <summary>
        /// 当前版本,可通过数据库设置,修改为行政版/单机版，相对数据字段也要相同
        /// </summary>
        public static string SystemVersion = "行政版"; //"行政版"或"单机版";

        /// <summary>
        /// 标识行政版标签,固定值。
        /// 应该保持与SystemVersion相同。
        /// </summary>
        public static string LocalBaseVersion = "行政版";// "单机版";

        /// <summary>
        /// 企业版本标签,固定值
        /// </summary>
        public static string EnterpriseVersion = "企业版";

        /// <summary>
        /// 系统应用对象类型，是：工商,食药,药监,农业.可以通过数据库配置
        /// </summary>
        public static string ApplicationTag="工商";

        /// <summary>
        /// 系统应用为工商类型标签
        /// Industry and commerce Application Tag
        /// </summary>
        public static string ICAppTag = "工商";

        /// <summary>
        /// 系统应用为食药类型标签
        /// Food and Drug Application Tag
        /// </summary>
        public static string FDAppTag = "食药";

        /// <summary>
        ///  系统应用为农检版
        /// Agriculture Inspection
        /// </summary>
        public static string AGRInsAppTag = "农检";

        #region 服务器连接相关参数

        /// <summary>
        /// 数据库连接字符串前半部分，为了数据上传或者下载不同数据库之间的切换
        /// </summary>
        public static string DBConnStrPref = @"Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source =";

        /// <summary>
        /// 未发送标签,固定值
        /// </summary>
        public static string SendState0 = "未发送";

        /// <summary>
        /// 已经发送标签,固定值
        /// </summary>
        public static string SendState1 = "已发送";

        /// <summary>
        ///连网服务器IP 编号为020201
        /// </summary>
        public static string SysServerIP = string.Empty;

        /// <summary>
        /// 连网用户账号,编号为020202
        /// </summary>
        public static string SysServerID = string.Empty;

        /// <summary>
        /// 连网用户密码,系统设置编号为020203
        /// </summary>
        public static string SysServerPass = string.Empty;

        /// <summary>
        /// 当前webservice连接接口类型,可以通过数据库配置
        /// </summary>
        public static string InterfaceType = "J2EE";

        /// <summary>
        /// J2EE版本接口标签,固定值
        /// </summary>
        public static string InterfaceJ2EE = "J2EE";

        /// <summary>
        /// NET版本接口标签,固定值
        /// </summary>
        public static string InterfaceDotNET = "NET";

        #endregion

        /// <summary>
        /// 所属组织 或者 所属市场.
        /// 可以通过数据库配置
        /// </summary>
        public static string AreaTitle = string.Empty;

        /// <summary>
        /// 受检人/单位  或者 单位名称.
        /// 可以通过数据库配置
        /// </summary>
        public static string NameTitle = string.Empty;

        /// <summary>
        /// 档口/店面/车牌号  或者 位置.
        /// 可以通过数据库配置
        /// </summary>
        public static string DomainTitle = string.Empty;

        /// <summary>
        /// 样品名称 或者 样品名称 标题.可以通过数据库配置
        /// 不需要写"名称"两字
        /// </summary>
        public static string SampleTitle = string.Empty;

        #region 仪器相关参数

        /// <summary>
        /// 手动输入法,固定值
        /// </summary>
        public static string ResultType1 = "检测仪手动";

        /// <summary>
        /// 检测卡方法,固定值
        /// </summary>
        public static string ResultType2 = "检测卡";

        /// <summary>
        /// 其他检测法,固定值
        /// </summary>
        public static string ResultType3 = "其他检测法";

        /// <summary>
        /// 定性定量法,固定值
        /// </summary>
        public static string ResultType4 = "定性定量法";

        /// <summary>
        /// 仪器自动检测法,固定值
        /// </summary>
        public static string ResultType5 = "检测仪自动";

        /// <summary>
        ///检测项目拼按字符串代码
        ///对应tMachine表中LinkStdCode字段
        /// </summary>
        public static string DefaultCheckItemCode = "0001";

        /// <summary>
        /// 当前仪器编号
        /// 对应tMachine表中SysCode字段
        /// </summary>
        public static string DefaultMachineCode = "001";

        /// <summary>
        /// 仪器串口号,对应tMachine
        /// </summary>
        public static string ComPort = "COM1:";

        /// <summary>
        /// 标准检测值，对应tMachine表中TestValue字段
        /// </summary>
        public static decimal DefaultLimitValue = 50;
     
        /// <summary>
        /// 标识不同DY5000版本名称
        /// 类型名称,带LD表示雷度版.
        /// 可以通过数据库配置
        /// </summary>
        public static string DY5000Name = "DY5000LD";

        /// <summary>
        /// DY3000版本，新版(DY3000DY)或者旧版DY3000.
        /// 可以通过数据库配置
        /// </summary>
        public static string CurDY3000Tag = "DY3000";

        #endregion

        /// <summary>
        /// 生产单位标签名称,根据不同应用场合配置不同标签名称
        /// </summary>
        public static string ProductionUnitNameTag = "供应商";

        /// <summary>
        /// 加密狗扫描时间间隔，单位为分钟
        /// </summary>
        public static int RockeyScanInterval = 10;

        //用来标识雷度的DY5000和DY3000，已经不起作用 
        //修改人:  2011-6-16

        //public static string frmDY3000Tag = "DY3000";
        //以下两个字段没有用
        //public static bool IsExistDY5000 = false;
        //public static bool IsExistDY5000LD = false;

        /////以下代码没有引用的地方，已经注掉。
        /////修改人:,2011-06-22

        //public static int ResultTypeCodeLen = 3;
        //public static int ResultCodeLen = 5;
        //public static int MMI_MarketRegionCodeLen = 4;
        //public static int MMI_StallCodeLen = 10;

        //以下字段各代表什么意思？？？？？？？没有地方用到，用到的地方无任何意义，现去掉
        //public static string ResultTypeCode1 = "001";
        //public static string ResultTypeCode2 = "002";
        //public static string ResultTypeCode3 = "003";
        //public static string ResultTypeCode4 = "004";
        //public static string ResultTypeCode5 = "005";
        ///// <summary>
        /////默认符号
        ///// </summary>
        //public static string DefaultSign = "≤";
        ///// <summary>
        ///// 默认单位
        ///// </summary>
        //public static string DefaultUnit = string.Empty;
        ///// <summary>
        ///// 系统当前仪器
        ///// </summary>
        //public static string SysCurMachine = string.Empty;
        ///// <summary>
        ///// 是否默认
        ///// </summary>
        //public static bool IsDefault = false;
    }
}
