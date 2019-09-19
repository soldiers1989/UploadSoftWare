using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    [Serializable]
    public class KJFWJTItem
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
        public KJFWJTItem()
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
            public string tID = string.Empty;
            public string HoleNumber = string.Empty;
            public string Operator = "";
            public string OperatorID = "";
        }

        public string id { get; set; }
        public string device_type_id { get; set; }
        public string item_id { get; set; }
        public string project_type { get; set; }
        public string detect_method { get; set; }
        public string detect_unit { get; set; }
        public string operation_password { get; set; }
        public string food_code { get; set; }
        public string invalid_value { get; set; }
        public string check_hole1 { get; set; }
        public string check_hole2 { get; set; }
        public string wavelength { get; set; }
        public string pre_time { get; set; }
        public string dec_time { get; set; }
        public string stdA0 { get; set; }
        public string stdA1 { get; set; }
        public string stdA2 { get; set; }
        public string stdA3 { get; set; }
        public string stdB0 { get; set; }
        public string stdB1 { get; set; }
        public string stdB2 { get; set; }
        public string stdB3 { get; set; }
        public string stdA { get; set; }
        public string stdB { get; set; }
        public string national_stdmin { get; set; }
        public string national_stdmax { get; set; }
        public string yin_min { get; set; }
        public string yin_max { get; set; }
        public string yang_min { get; set; }
        public string yang_max { get; set; }
        public string yinT { get; set; }
        public string yangT { get; set; }
        public string absX { get; set; }
        public string ctAbsX { get; set; }
        public string division { get; set; }
        public string parameter { get; set; }
        public string trailingEdgeC { get; set; }
        public string trailingEdgeT { get; set; }
        public string suspiciousMin { get; set; }
        public string suspiciousMax { get; set; }
        public string reserved1 { get; set; }
        public string reserved2 { get; set; }
        public string reserved3 { get; set; }
        public string reserved4 { get; set; }
        public string reserved5 { get; set; }
        public string remark { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }

        public string item { get; set; }
    }
    [Serializable]
    public class KJFWGHXItem
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
        public KJFWGHXItem()
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
            public string tID = string.Empty;
            public string HoleNumber = string.Empty;
            public string Operator = "";
            public string OperatorID = "";
        }
        public string id { get; set; }
        public string device_type_id { get; set; }
        public string item_id { get; set; }
        public string project_type { get; set; }
        public string detect_method { get; set; }
        public string detect_unit { get; set; }
        public string operation_password { get; set; }
        public string food_code { get; set; }
        public string invalid_value { get; set; }
        public string check_hole1 { get; set; }
        public string check_hole2 { get; set; }
        public string wavelength { get; set; }
        public string pre_time { get; set; }
        public string dec_time { get; set; }
        public string stdA0 { get; set; }
        public string stdA1 { get; set; }
        public string stdA2 { get; set; }
        public string stdA3 { get; set; }
        public string stdB0 { get; set; }
        public string stdB1 { get; set; }
        public string stdB2 { get; set; }
        public string stdB3 { get; set; }
        public string stdA { get; set; }
        public string stdB { get; set; }
        public string national_stdmin { get; set; }
        public string national_stdmax { get; set; }
        public string yin_min { get; set; }
        public string yin_max { get; set; }
        public string yang_min { get; set; }
        public string yang_max { get; set; }
        public string yinT { get; set; }
        public string yangT { get; set; }
        public string absX { get; set; }
        public string ctAbsX { get; set; }
        public string division { get; set; }
        public string parameter { get; set; }
        public string trailingEdgeC { get; set; }
        public string trailingEdgeT { get; set; }
        public string suspiciousMin { get; set; }
        public string suspiciousMax { get; set; }
        public string reserved1 { get; set; }
        public string reserved2 { get; set; }
        public string reserved3 { get; set; }
        public string reserved4 { get; set; }
        public string reserved5 { get; set; }
        public string remark { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }

        public string item { get; set; }
    }

}
