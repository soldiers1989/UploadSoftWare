using System;

namespace AIO
{
    [Serializable]
    public class DYFGDItemPara
    {
        public static string[] MethodToString = { "抑制率法", "标准曲线法", "子项目法", "动力学法", "系数法" };
        public int Index;//坐标
        public string Name;
        public string testPro;
        public string Unit;
        public string HintStr; // 提示字符串
        public int Method = -1;
        public HoleSettings[] Hole; // 检测孔信息
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_HOLECOUNT;// 最多48通道
        public int Wave; // 波长，410, 440, 520, 540, 595, 660，等。同一个项目，使用一种波长，不同孔位的波长可能不同，所以要记录一个索引。
        //public int SecendWave; // 0 表示没有次波长，没有用到
        public string Password = "";
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
            // 检测时间
            public int DetTime, PreHeatTime;
            // 预热时间
            // 阳性下限 上限 阴性下限 上限
            public int PlusL, PlusH, MinusL, MinusH;
            // 是否使用阴阳性判定
            public bool UsePlusMinus;

            public double RefDeltaA = double.MinValue; // 农残有个对照deltaA
        }
        // 标准曲线法的参数
        [Serializable]
        public class DYSCPara
        {
            // 曲线A的参数
            public double A0, A1, A2, A3, AFrom, ATo;
            // 曲线B的参数
            public double B0, B1, B2, B3, BFrom, BTo;
            //曲线C(自动生成)的参数
            public double C0, C1, C2, C3, CFrom, CTo;
            /// <summary>
            /// 是否使用自动生成的曲线
            /// </summary>
            public bool IsEnabledCalcCurve = false;

            // 预热时间 检测时间
            public int DetTime, PreHeatTime;
            // 校正曲线（calibration curve）参数a b
            public double CCA, CCB;
            // 褪色法
            public bool IsReverse;
            // 国标值下限 上限
            public double LLevel, HLevel;
            public double RefA = double.MinValue;// 标准曲线有个对照A。
        }
        // 子项目法的参数
        [Serializable]
        public class DYCIPara
        {
            // 子项目1
            public string Item1Name;
            // 子项目1系数
            public double Item1Rate;
            // 子项目1From To
            public double Item1From, Item1To;
            // 子项目2
            public string Item2Name;
            // 子项目2系数
            public double Item2Rate;
            // 子项目2From To
            public double Item2From, Item2To;
            // 国标值下限 上限
            public double LLevel, HLevel;
        }
        // 动力学法的参数
        [Serializable]
        public class DYDNPara
        {
            // 曲线A的参数
            public double A0, A1, A2, A3, AFrom, ATo;
            // 曲线B的参数
            public double B0, B1, B2, B3, BFrom, BTo;
            // 预热时间 检测时间
            public int DetTime, PreHeatTime;
            // 校正曲线（calibration curve）参数a b
            public double CCA, CCB;
            // 褪色法
            public bool IsReverse;
            // 国标值下限 上限
            public double LLevel, HLevel;
        }
        // 系数法 coefficient
        [Serializable]
        public class DYCOPara
        {
            // 系数法四个参数
            public double A0, A1, A2, A3;
            // 国标值下限 上限
            public double LLevel, HLevel;
        }
        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public bool IsTest = false;
            public string SampleName;
            public string SampleSource;
            public string TaskName;
            public string CompanyName;
            public string ProduceCompany;
            public string SampleTypeName;
            public string SampleTypeCode;
            // 硬件上孔位有4种波长，WaveIndex表示项目要求的波长在对应孔位上的索引。孔位选择之后，这些就要确定下来。
            public int WaveIndex;
            /// <summary>
            /// 反应液滴数OR稀释倍数
            /// </summary>
            public double DishuOrBeishu;
            public string TaskCode;
            public string SampleId;
        }
    }
    [Serializable]
    public class DYJTJItemPara
    {
        //2018年1月19日新增type元素，区别胶体金0、薄层色谱1、荧光免疫层析检测模块2，食源性微生物检测模块3等
        public int Type = 0;
        public static string[] MethodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)" };
        public int Index;
        public string Name;
        public string testPro;
        public string CheckName;
        public string Unit;
        public string HintStr; // 提示字符串
        /// <summary>
        /// 0 定性消线 ； 1 定性比色 ；2 定量（T） ； 3 定量（T/C）
        /// </summary>
        public int Method;
        public int SampleNum = 1;
        public string Password = "";
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_SXTCOUNT;// 最多8通道
        public double InvalidC, newInvalidC;
        public const int METHOD_DXXX = 0;
        public const int METHOD_DXBS = 1;
        public const int METHOD_DLT = 2;
        public const int METHOD_DLTC = 3;
        /// <summary>
        /// [旧模块]胶体金判定C值需要的连续下降沿点数
        /// </summary>
        public int pointCNum;
        /// <summary>
        /// [旧模块]胶体金判定T值需要的连续下降沿点数
        /// </summary>
        public int pointTNum;
        /// <summary>
        /// [旧模块]胶体金最大连续点数
        /// </summary>
        public int maxPoint;
        
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
        }
        [Serializable]
        public class DYLINEPara
        {
            public double A0, A1, A2, A3;
            public double B0, B1;
            public double Limit;
        }
        [Serializable]
        public class DYCALPara
        {
            public double[] MaxT, newMaxT;
            public double[] MinT, newMinT;
            //public double[] Abs;
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
            public string SampleName;
            public string TaskName;
            public string CompanyName;
            public string ProduceCompany;
            public string TaskCode;
            public string SampleTypeName;
            public string SampleTypeCode;
            public string SampleId;
        }
    }

    [Serializable]
    public class DYGSZItemPara
    {
        public static string[] MethodToString = { "定性法", "定量法" };
        public int Index;
        public string Name;
        public string testPro;
        public string Unit;
        public string HintStr; // 提示字符串
        public int Method;
        public int SampleNum = 1;
        public string Password = "";
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = DeviceProp.DeviceHole.MAX_SXTCOUNT;// 最多8通道
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
        }
        [Serializable]
        public class DYLINEPara
        {
            public int RGBSel;
            public double A0, A1, A2, A3;
            public double B0, B1;
            public double LimitL, LimitH;
        }
        [Serializable]
        public class DYCALPara
        {
            public double[] DeltaA;
            public double[] Min;
            public double[] Max;
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
            public string SampleName;
            public string SampleSource;
            public string TaskName;
            public string CompanyName;
            public string ProduceCompany;
            public string TaskCode;
            public string SampleTypeName;
            public string SampleTypeCode;
            public string SampleId;
        }
    }

    // 重金属检测项目
    [Serializable]
    public class DYHMItemPara
    {
        public static string[] MethodToString = { "电极处理", "样品测试", "加标测试", "标液校准" };
        public int Index;
        public string Name;
        public string HintStr;
        public int ItemID;
        public int Method;
        public int SampleNum;
        public int DilutionRatio;
        public int Concentration;
        public int LiquidVolume;
        public int Requirements;
        public int a0, a1, a2;
        public string Unit;
        public string Password = "";
        public HoleSettings[] Hole;
        private const int MAX_HOLECOUNT = 12;// 最多12通道

        public DYHMItemPara()
        {
            Hole = new HoleSettings[MAX_HOLECOUNT];
            for (int i = 0; i < Hole.Length; ++i)
                Hole[i] = new HoleSettings();
        }

        [Serializable]
        public class HoleSettings
        {
            public bool Use = false;
            public string SampleName;
            public string TaskName;
            public string CompanyName;
            public string TaskCode;
        }

    }
}