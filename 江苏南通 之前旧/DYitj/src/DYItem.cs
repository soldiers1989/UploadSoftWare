using System;
using System.Collections.Generic;

namespace AIO
{
    [Serializable]

    public class DYFGDItemPara
    {
        //2016年3月3日 wenj 赋予item默认值防止出现空指针 
        public static string[] MethodToString = { "抑制率法", "标准曲线法", "子项目法", "动力学法", "系数法" };
        public int Index = 0;//坐标
        public string Name = string.Empty;
        public string Unit = string.Empty;
        public string HintStr = string.Empty; // 提示字符串
        public int Method = -1;
        public HoleSettings[] Hole; // 检测孔信息
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_HOLECOUNT;// 最多48通道
        public int Wave = 0; // 波长，410, 440, 520, 540, 595, 660，等。同一个项目，使用一种波长，不同孔位的波长可能不同，所以要记录一个索引。
        //public int SecendWave; // 0 表示没有次波长，没有用到
        public string Password = string.Empty;
        public int SampleNum = 1; // 样品编号
        public const int METHOD_IR = 0;
        public const int METHOD_SC = 1;
        public const int METHOD_CI = 2;
        public const int METHOD_ND = 3;
        public const int METHOD_CO = 4;
        // 抑制率法
        public DYIRPara ir;
        // 标准曲线法
        public DYSCPara sc;
        // 子项目法
        public DYCIPara ci;
        // 动力学法
        public DYDNPara dn;
        // 系数法
        public DYCOPara co;
        public DYFGDItemPara()
        {
            Hole = new HoleSettings[MAX_HOLECOUNT];
            for (int i = 0; i < Hole.Length; ++i)
                Hole[i] = new HoleSettings();
            ir = new DYIRPara();
            sc = new DYSCPara();
            ci = new DYCIPara();
            dn = new DYDNPara();
            co = new DYCOPara();
        }
        [Serializable]
        public class DYIRPara
        {
            /// <summary>
            /// DetTime检测时间；PreHeatTime预热时间
            /// </summary>
            public int DetTime = 0, PreHeatTime = 0;
            /// <summary>
            /// 阳性下限 上限 阴性下限 上限 M阴性范围；P阳性范围
            /// </summary>
            public int PlusL = 0, PlusH = 0, MinusL = 0, MinusH = 0;
            /// <summary>
            /// 是否使用阴阳性判定
            /// </summary>
            public bool UsePlusMinus = false;
            /// <summary>
            /// 对照值
            /// </summary>
            public double RefDeltaA = Double.MinValue;
        }
        // 标准曲线法的参数
        [Serializable]
        public class DYSCPara
        {
            // 曲线A的参数
            public double A0 = 0, A1 = 0, A2 = 0, A3 = 0, AFrom = 0, ATo = 0;
            // 曲线B的参数
            public double B0 = 0, B1 = 0, B2 = 0, B3 = 0, BFrom = 0, BTo = 0;

            // 预热时间 检测时间
            public int DetTime = 0, PreHeatTime = 0;
            // 校正曲线（calibration curve）参数a b
            public double CCA = 0, CCB = 0;
            // 褪色法
            public bool IsReverse = false;
            // 国标值下限 上限
            public double LLevel = 0, HLevel = 0;
            public double RefA = Double.MinValue;// 标准曲线有个对照A。
        }
        // 子项目法的参数
        [Serializable]
        public class DYCIPara
        {
            // 子项目1
            public string Item1Name = string.Empty;
            // 子项目1系数
            public double Item1Rate = 0;
            // 子项目1From To
            public double Item1From = 0, Item1To = 0;
            // 子项目2
            public string Item2Name = string.Empty;
            // 子项目2系数
            public double Item2Rate = 0;
            // 子项目2From To
            public double Item2From = 0, Item2To = 0;
            // 国标值下限 上限
            public double LLevel = 0, HLevel = 0;
        }
        // 动力学法的参数
        [Serializable]
        public class DYDNPara
        {
            // 曲线A的参数
            public double A0 = 0, A1 = 0, A2 = 0, A3 = 0, AFrom = 0, ATo = 0;
            // 曲线B的参数
            public double B0 = 0, B1 = 0, B2 = 0, B3 = 0, BFrom = 0, BTo = 0;
            // 预热时间 检测时间
            public int DetTime = 0, PreHeatTime = 0;
            // 校正曲线（calibration curve）参数a b
            public double CCA = 0, CCB = 0;
            // 褪色法
            public bool IsReverse = false;
            // 国标值下限 上限
            public double LLevel = 0, HLevel = 0;
        }
        // 系数法 coefficient
        [Serializable]
        public class DYCOPara
        {
            // 系数法四个参数
            public double A0 = 0, A1 = 0, A2 = 0, A3 = 0;
            // 国标值下限 上限
            public double LLevel = 0, HLevel = 0;
        }
        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public bool IsTest = false;
            public string SampleName = string.Empty,SAMPLEUUID = string.Empty;
            public string TaskName = string.Empty;
            public string CompanyName = string.Empty;
            public string ProduceCompany = string.Empty;
            // 硬件上孔位有4种波长，WaveIndex表示项目要求的波长在对应孔位上的索引。孔位选择之后，这些就要确定下来。
            public int WaveIndex = 0;
            /// <summary>
            /// 反应液滴数OR稀释倍数
            /// </summary>
            public double DishuOrBeishu = 0;
            public string TaskCode = string.Empty;
            /// <summary>
            /// 抽检单号
            /// </summary>
            public string SampleId = string.Empty;
        }
    }
    [Serializable]
    public class DYJTJItemPara
    {
        public static string[] MethodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)", "定性比色法(T/C)" };
        public int Index = 0;
        public string Name = string.Empty;
        public string CheckName = string.Empty;
        public string Unit = string.Empty;
        public string HintStr = string.Empty; // 提示字符串
        public int Method = 0;
        public int SampleNum = 1;
        public string Password = string.Empty;
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_SXTCOUNT;// 最多8通道
        public List<string> SampleName; // 样品名称 集合
        //Lee
        public List<string> TaskName; // 任务 集合
        public List<string> CompanyName; // 被检单位 集合
        public int InvalidC = 0;
        public const int METHOD_DXXX = 0;
        public const int METHOD_DXBS = 1;
        public const int METHOD_DLT = 2;
        public const int METHOD_DLTC = 3;
        /// <summary>
        /// 定性比色（T/C）
        /// </summary>
        public const int METHOD_DXBS_TC = 4;
        public DYCALPara dxxx;
        public DYCALPara dxbs;
        public DYLINEPara dlt;
        public DYLINEPara dltc;
        public DYJTJItemPara()
        {
            Hole = new HoleSettings[MAX_HOLECOUNT];
            for (int i = 0; i < Hole.Length; ++i)
                Hole[i] = new HoleSettings();
            dxxx = new DYCALPara();
            dxbs = new DYCALPara();
            dlt = new DYLINEPara();
            dltc = new DYLINEPara();
            SampleName = new List<string>();
            TaskName = new List<string>();
            CompanyName = new List<string>();
        }
        [Serializable]
        public class DYLINEPara
        {
            public double A0 = 0, A1 = 0, A2 = 0, A3 = 0;
            public double B0 = 0, B1 = 0;
            public double Limit;
        }
        [Serializable]
        public class DYCALPara
        {
            public double PlusT = 0;
            public double MinusT = 0;
            /// <summary>
            /// 研发试验得出数值
            /// </summary>
            public double Abs_X = 0;
            public int SetIdx = 0;
        }
        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public int RegionLeft = 70;
            public int RegionTop = 20;
            public int RegionWidth = 20;
            public int RegionHeight = 100;
            public int COffset = 25;
            public int TOffset = 75;
            public string SampleName = string.Empty;
            public string SampleId = string.Empty;
            public string TaskName = string.Empty;
            public string CompanyName = string.Empty;
            public string ProduceCompany = string.Empty;
            public string TaskCode = string.Empty;
        }
    }

    [Serializable]
    public class DYGSZItemPara
    {
        public static string[] MethodToString = { "定性法", "定量法" };
        public int Index = 0;
        public string Name = string.Empty;
        public string Unit = string.Empty;
        public string HintStr = string.Empty; // 提示字符串
        public int Method = 0;
        public int SampleNum = 1;
        public string Password = string.Empty;
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_SXTCOUNT;// 最多8通道
        public List<string> SampleName; // 样品名称 集合
        //Lee
        public List<string> TaskName; // 任务 集合
        public List<string> CompanyName; // 被检单位 集合
        public const int METHOD_DX = 0;
        public const int METHOD_DL = 1;
        public DYLINEPara dl;
        public DYCALPara dx;
        public DYGSZItemPara()
        {
            Hole = new HoleSettings[MAX_HOLECOUNT];
            for (int i = 0; i < Hole.Length; ++i)
                Hole[i] = new HoleSettings();
            dl = new DYLINEPara();
            dx = new DYCALPara();
            SampleName = new List<string>();
            TaskName = new List<string>();
            CompanyName = new List<string>();
        }
        [Serializable]
        public class DYLINEPara
        {
            public int RGBSel = 0;
            public double A0 = 0, A1 = 0, A2 = 0, A3 = 0;
            public double B0 = 0, B1 = 0;
            public double LimitL = 0, LimitH = 0;
        }
        [Serializable]
        public class DYCALPara
        {
            public int RGBSelPlus = 0;
            public int RGBSelMinus = 0;
            public int ComparePlus = 0;
            public int CompareMinus = 0;
            public double PlusT = 0;
            public double MinusT = 0;
        }
        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public int RegionLeft = 70;
            public int RegionTop = 20;
            public int RegionWidth = 20;
            public int RegionHeight = 100;
            public int COffset = 0;
            public int TOffset = 0;
            public string SampleName = string.Empty;
            public string SampleId = string.Empty;
            public string TaskName = string.Empty;
            public string CompanyName = string.Empty;
            public string ProduceCompany = string.Empty;
            public string TaskCode = string.Empty;
        }
    }

    // 重金属检测项目
    [Serializable]
    public class DYHMItemPara
    {
        public static string[] MethodToString = { "电极处理", "样品测试", "加标测试", "标液校准" };
        public int Index = 0;

        public string Name = string.Empty;

        public string HintStr = string.Empty;

        public int ItemID = 0;

        public int Method = 0;

        public int SampleNum = 0;

        public int DilutionRatio = 0;

        public int Concentration = 0;

        public int LiquidVolume = 0;

        public int Requirements = 0;

        public int a0 = 0, a1 = 0, a2 = 0;

        public string Unit = string.Empty;

        public List<string> SampleName; // 样品名称 集合
        //Lee
        public List<string> TaskName; // 任务 集合
        public List<string> CompanyName; // 被检单位 集合

        public string Password = string.Empty;
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = 12;// 最多12通道

        public DYHMItemPara()
        {
            TaskName = new List<string>();
            CompanyName = new List<string>();

            Hole = new HoleSettings[MAX_HOLECOUNT];
            for (int i = 0; i < Hole.Length; ++i)
                Hole[i] = new HoleSettings();
        }

        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public string SampleName = string.Empty;
            public string SampleId = string.Empty;
            public string TaskName = string.Empty;
            public string CompanyName = string.Empty;
            public string ProduceCompany = string.Empty;
            public string TaskCode = string.Empty;
        }
    }
}
