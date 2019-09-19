using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    public class TestResultConserve : Window
    {
        private MsgThread msgThread;
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private static tlsTtResultSecond _model = new tlsTtResultSecond();
        private static DateTime _nowTime;
        private static object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
         
        /// <summary>
        /// All New Lee
        /// </summary>
        public TestResultConserve()
        {

        }

        public static int ResultConserveAH(string[,] result, out string[] syscodes)
        {
            string ourEgg = string.Empty;
            syscodes = null;
            int CountLength = 0;
            if (result == null)
            {
                return CountLength = 0;
            }

            _model = new tlsTtResultSecond();
            try
            {
                syscodes = new string[result.GetLength(0)];
                _nowTime = DateTime.Now;
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]))
                    {
                        _model.SysCode = Global.GETGUID(null);
                        _model.Hole = result[i, 0];//检测孔
                        _model.ResultType = result[i, 1]; //检测类别=检测手段
                        _model.CheckTotalItem = result[i, 2];//检测项目
                        _model.CheckValueInfo = result[i, 4];//检测结果
                        _model.ResultInfo = result[i, 5];//检测值单位
                        _model.CheckStartDate = result[i, 6]; //检测日期=检测时间

                        _model.CheckUnitInfo = result[i, 7]; //检测人员=检测人姓名
                        _model.Organizer = result[i, 7];//抽样人
                        _model.UpLoader = result[i, 7];//基层上传人

                        _model.CheckMethod = result[i, 3]; //检测方法
                        _model.Result = result[i, 9];//检测结论 （合格、不合格）
                        _model.FoodName = result[i, 8];//样品名称

                        //string sanpleNo = Global.GetSpellCode(_model.FoodName);
                        _model.StandValue = result[i, 10];//检测标准值
                        _model.SampleCode = _nowTime.ToString("yyyyMMddHHmmss") + result[i, 11];//样品编号
                        _model.Standard = result[i, 12];//检测依据
                        _model.CheckPlanCode = result[i, 13]; //任务编号
                        _model.CheckedCompany = result[i, 14];//被检对象名称
                        _model.CheckedCompanyCode = !string.IsNullOrEmpty(result[i, 14]) ? clsCompanyOpr.NameFromCode(result[i, 14]) : string.Empty;

                        _model.CheckPlaceCode = string.Empty; //行政机构代码
                        _model.CheckPlace = string.Empty; //检测设备型号
                        _model.CheckUnitName = string.Empty; //
                        _model.APRACategory = string.Empty;

                        _model.CheckType = "抽检";
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;  //检测设备
                        _model.MachineCompany = ((AssemblyCompanyAttribute)attributes[0]).Company; //检测设备生产厂家
                        _model.TakeDate = _nowTime.ToString("yyyy-MM-dd HH:mm:ss");//抽样日期
                        _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmss") + result[i, 11];//检测编号
                        _model.fTpye = result[i, 16];
                        _model.testPro = result[i, 17];
                        _model.checkedUnit = _model.CheckedCompanyCode;
                        _model.quanOrQual = result[i, 18];
                        _model.dataNum = _model.CheckNo;

                        _bll.InsertAh(_model, out ourEgg);
                        if (ourEgg.Equals(string.Empty))
                        {
                            syscodes[i] = _model.SysCode;
                            //如果有曲线数据的话，需要保存曲线数据
                            if (result[i, 15].Length > 0)
                            {
                                clsCurveDatas cmodel = new clsCurveDatas();
                                cmodel.SysCode = _model.SysCode;
                                cmodel.CData = result[i, 15];
                                _bll.InsertCurveData(cmodel, out ourEgg);
                            }
                            CountLength++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                MessageBox.Show(ex.Message);
            }
            return CountLength;
        }

        /// <summary>
        /// ResultSave
        /// </summary>
        /// <param name="Result"></param>
        public static int ResultConserve(string[,] Result,out string[] syscodes)
        {
            syscodes = null;
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (Result == null)
            {
                return CountLength = 0;
            }
            _model = new tlsTtResultSecond();
            _nowTime = DateTime.Now;
            try
            {
                syscodes = new string[Result.GetLength(0)];
                for (int i = 0; i < Result.GetLength(0); i++)
                {
                    if (!Result[i, 4].ToString().Contains("Abs") && !Result[i, 4].ToString().Contains("NA") && !Result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(Result[i, 4]))
                    {
                        _model.SysCode = Global.GETGUID(null, 1);
                        _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff") + Result[i, 11];//检测编号
                        _model.Hole = Result[i, 0];//检测孔
                        _model.ResultType = Result[i, 1]; //检测类别=检测手段
                        _model.CheckTotalItem = Result[i, 2];//检测项目
                        _model.CheckValueInfo = Result[i, 4];//检测结果
                        _model.ResultInfo = Result[i, 5];//检测值单位
                        _model.CheckStartDate = Result[i, 6]; //检测日期=检测时间

                        _model.CheckUnitInfo = Result[i, 7]; //检测人员=检测人姓名
                        _model.Organizer = Result[i, 7];//抽样人
                        _model.UpLoader = Result[i, 7];//基层上传人

                        _model.CheckMethod = Result[i, 3]; //检测方法
                        _model.Result = Result[i, 9];//检测结论 （合格、不合格）
                        _model.FoodName = Result[i, 8];//样品名称
                        string name = _model.FoodName;
                        if (name.Length > 0)
                        {
                            DataTable dt = _bll.GetAsDataTable(string.Format("FtypeNmae Like '{0}'", name), string.Empty, 7, 0);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                name = dt.Rows[0]["SampleNum"].ToString();//样品编号
                                if (name.Length <= 5)
                                {
                                    if (name.Length == 5)
                                    {
                                        _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();
                                    }
                                    else
                                    {
                                        _model.FoodType = "自建样品";
                                    }
                                }
                                else
                                {
                                    dt = _bll.GetAsDataTable(string.Format("SampleNum Like '{0}'", name.Substring(0, name.Length - 5)), string.Empty, 7, 0);
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();//样品编号
                                    }
                                }
                            }
                        }
                        _model.StandValue = Result[i, 10];//检测标准值
                        _model.SampleCode = Result[i, 11];//样品编号
                        _model.Standard = Result[i, 12];//检测依据
                        _model.CheckPlanCode = Result[i, 13]; //任务编号
                        _model.CheckedCompany = Result[i, 14];//被检对象名称
                        if (!string.IsNullOrEmpty(Result[i, 14]))
                            _model.CheckedCompanyCode = clsCompanyOpr.NameFromCode(Result[i, 14]);
                        else
                            _model.CheckedCompanyCode = string.Empty;
                        _model.CheckPlaceCode = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPlaceCode : string.Empty; //行政机构代码
                        _model.CheckPlace = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].Organization : string.Empty; //检测设备型号
                        _model.CheckUnitName = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointName : string.Empty; //
                        _model.APRACategory = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointType : string.Empty;

                        _model.CheckType = "抽检";
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName; //检测设备
                        _model.MachineCompany = ((AssemblyCompanyAttribute)attributes[0]).Company; //检测设备生产厂家
                        _model.TakeDate = _nowTime.ToString("yyyy-MM-dd HH:mm:ss");//抽样日期
                        _model.ContrastValue = "NULL";
                        _model.SampleId = Result[i, 20];
                        _model.DeviceId = Global.DeviceID;
                        _model.ProduceCompany = Result[i, 21];

                        _bll.Insert(_model, out ourEgg);
                        if (ourEgg.Equals(string.Empty))
                        {
                            syscodes[i] = _model.SysCode;
                            CountLength++;
                            //如果有曲线数据的话，需要保存曲线数据
                            if (Result[i, 15]!=null && Result[i, 15].Length > 0)
                            {
                                clsCurveDatas cmodel = new clsCurveDatas();
                                cmodel.SysCode = _model.SysCode;
                                cmodel.CData = Result[i, 15];
                                _bll.InsertCurveData(cmodel, out ourEgg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                MessageBox.Show(ex.Message,"数据保存");
            }
            return CountLength;
        }

        /// <summary>
        /// 胶体金3.0模块，重新检测后更新检测结果
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static int UpdateValues(string[,] Result, string[] codes, out string[] syscodes)
        {

            syscodes = codes;
            int count = 0;
            if (Result == null)
            {
                return count = 0;
            }

            for (int i = 0; i < Result.GetLength(0); i++)
            {
                string errMsg = string.Empty;
                _model = new tlsTtResultSecond();
                _model.SysCode = codes[i];
                _model.Result = Result[i, 9];//检测结论 （合格、不合格）
                _model.CheckValueInfo = Result[i, 4];//检测结果
                _bll.UpdateCheckValue(_model, out errMsg);
                if (errMsg.Length == 0)
                {
                    clsCurveDatas cmodel = new clsCurveDatas();
                    cmodel.SysCode = _model.SysCode;
                    cmodel.CData = Result[i, 15];
                    _bll.InsertCurveData(cmodel, out errMsg);
                    count++;
                }
            }
            return count;
        }

        public static int AhResultConserve(string[,] result, bool contrast, string ContrastValue)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (result == null)
            {
                return CountLength = 0;
            }
            _model = new tlsTtResultSecond();
            _nowTime = DateTime.Now;
            int forLenght = contrast ? result.GetLength(0) - 1 : result.GetLength(0);
            for (int i = 0; i < forLenght; i++)
            {
                if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]))
                {
                    _model.SysCode = Global.GETGUID(null);
                    _model.Hole = result[i, 0];//检测孔
                    _model.ResultType = result[i, 1]; //检测类别=检测手段
                    _model.CheckTotalItem = result[i, 2];//检测项目
                    _model.CheckValueInfo = result[i, 4];//检测结果
                    _model.ResultInfo = result[i, 5];//检测值单位
                    _model.CheckStartDate = result[i, 6]; //检测日期=检测时间

                    _model.CheckUnitInfo = result[i, 7]; //检测人员=检测人姓名
                    _model.Organizer = result[i, 7];//抽样人
                    _model.UpLoader = result[i, 7];//基层上传人

                    _model.CheckMethod = result[i, 3]; //检测方法
                    _model.Result = result[i, 9];//检测结论 （合格、不合格）
                    _model.FoodName = result[i, 8];//样品名称

                    //string sanpleNo = Global.GetSpellCode(_model.FoodName);
                    //if (sanpleNo.IndexOf("?") >= 0) sanpleNo = string.Empty;

                    _model.StandValue = result[i, 10];//检测标准值
                    _model.SampleCode = _nowTime.ToString("yyyyMMddHHmmss") + result[i, 11];//样品编号
                    _model.Standard = result[i, 12];//检测依据
                    _model.CheckPlanCode = result[i, 13]; //任务编号
                    _model.CheckedCompany = result[i, 14];//被检对象名称

                    _model.CheckedCompanyCode = !string.IsNullOrEmpty(result[i, 14]) ? clsCompanyOpr.NameFromCode(result[i, 14]) : string.Empty;
                    _model.CheckPlaceCode = string.Empty; //行政机构代码
                    _model.CheckPlace = string.Empty; //检测设备型号
                    _model.CheckUnitName = string.Empty; //
                    _model.APRACategory = string.Empty;

                    _model.CheckType = "抽检";
                    _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                    _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;  //检测设备
                    _model.MachineCompany = ((AssemblyCompanyAttribute)attributes[0]).Company; //检测设备生产厂家
                    _model.TakeDate = _nowTime.ToString("yyyy-MM-dd HH:mm:ss");//抽样日期
                    _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmss") + result[i, 11];//检测编号
                    _model.ContrastValue = ContrastValue;
                    _model.fTpye = result[i, 15];
                    _model.testPro = result[i, 16];
                    _model.checkedUnit = _model.CheckedCompanyCode;
                    _model.quanOrQual = result[i, 17];
                    _model.dataNum = _model.CheckNo;

                    _bll.InsertAh(_model, out ourEgg);
                    if (ourEgg.Equals(string.Empty))
                    {
                        CountLength++;
                    }
                }
            }
            return CountLength;
        }

        /// <summary>
        /// ResultSave
        /// </summary>
        /// <param name="Result"></param>
        public static int ResultConserve(string[,] Result, bool contrast, string ContrastValue)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (Result == null)
            {
                return CountLength = 0;
            }
            _model = new tlsTtResultSecond();
            _nowTime = DateTime.Now;
            int forLenght = contrast ? Result.GetLength(0) - 1 : Result.GetLength(0);
            for (int i = 0; i < forLenght; i++)
            {
                if (!Result[i, 4].ToString().Contains("NA") && !Result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(Result[i, 4]))
                {
                    _model.SysCode = Global.GETGUID(null, 1);
                    _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff") + Result[i, 11];//检测编号
                    _model.Hole = Result[i, 0];//检测孔
                    _model.ResultType = Result[i, 1]; //检测类别=检测手段
                    _model.CheckTotalItem = Result[i, 2];//检测项目
                    _model.CheckValueInfo = Result[i, 4];//检测结果
                    _model.ResultInfo = Result[i, 5];//检测值单位
                    _model.CheckStartDate = Result[i, 6]; //检测日期=检测时间

                    _model.CheckUnitInfo = Result[i, 7]; //检测人员=检测人姓名
                    _model.Organizer = Result[i, 7];//抽样人
                    _model.UpLoader = Result[i, 7];//基层上传人

                    _model.CheckMethod = Result[i, 3]; //检测方法
                    _model.Result = Result[i, 9];//检测结论 （合格、不合格）
                    _model.FoodName = Result[i, 8];//样品名称
                    string name = _model.FoodName;
                    if (name.Length > 0)
                    {
                        DataTable dt = _bll.GetAsDataTable(string.Format("FtypeNmae Like '{0}'", name), string.Empty, 7, 0);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            name = dt.Rows[0]["SampleNum"].ToString();//样品编号
                            if (name.Length <= 5)
                            {
                                if (name.Length == 5)
                                {
                                    _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();
                                }
                                else
                                {
                                    _model.FoodType = "自建样品";
                                }
                            }
                            else
                            {
                                dt = _bll.GetAsDataTable(string.Format("SampleNum Like '{0}'", name.Substring(0, name.Length - 5)), string.Empty, 7, 0);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();//样品编号
                                }
                            }
                        }
                    }
                    _model.StandValue = Result[i, 10];//检测标准值
                    _model.SampleCode = Result[i, 11];//样品编号
                    _model.Standard = Result[i, 12];//检测依据
                    _model.CheckPlanCode = Result[i, 13]; //任务编号
                    _model.CheckedCompany = Result[i, 14];//被检对象名称
                    if (!string.IsNullOrEmpty(Result[i, 14]))
                        _model.CheckedCompanyCode = clsCompanyOpr.NameFromCode(Result[i, 14]);
                    else
                        _model.CheckedCompanyCode = string.Empty;
                    _model.CheckPlaceCode = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPlaceCode : string.Empty; //行政机构代码
                    _model.CheckPlace = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].Organization : string.Empty; //检测设备型号
                    _model.CheckUnitName = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointName : string.Empty; //
                    _model.APRACategory = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointType : string.Empty;

                    _model.CheckType = "抽检";
                    _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                    _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName; //检测设备
                    _model.MachineCompany = ((AssemblyCompanyAttribute)attributes[0]).Company; //检测设备生产厂家
                    _model.TakeDate = _nowTime.ToString("yyyy-MM-dd HH:mm:ss");//抽样日期
                    _model.ContrastValue = ContrastValue;
                    _model.SampleId = Result[i, 19];
                    _model.DeviceId = Global.DeviceID;
                    _model.ProduceCompany = Result[i, 20];
                    _bll.Insert(_model, out ourEgg);
                    if (ourEgg.Equals(string.Empty))
                    {
                        CountLength++;
                    }
                }
            }
            return CountLength;
        }

        /// <summary>
        /// 结果判定
        /// </summary>
        /// <param name="str"></param>
        /// <param name="SampleName"></param>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public static string[] UnqualifiedOrQualified(string str, string SampleName, string ItemName)
        {
            string[] Qualifide = new string[5];
            try
            {
                if (str.Contains("%"))
                {
                    str = str.Replace("%", string.Empty);
                }
                if (str == "NA" || str.Contains("*"))
                {
                    Qualifide[0] = "无效值";
                    Qualifide[1] = string.Empty;
                    Qualifide[2] = string.Empty;
                    Qualifide[3] = string.Empty;
                    Qualifide[4] = string.Empty;
                    return Qualifide;
                }
                if (SampleName == string.Empty)
                {
                    //无法判定
                    Qualifide[0] = "参考国标";
                    Qualifide[1] = string.Empty;
                    Qualifide[2] = string.Empty;
                    Qualifide[3] = string.Empty;
                    Qualifide[4] = string.Empty;
                    return Qualifide;
                }
                if (SampleName != string.Empty && str != "NA" && !str.Contains("*"))
                {
                    double strInt = Convert.ToDouble(str);
                    clsttStandardDecideOpr bll = new clsttStandardDecideOpr();
                    DataTable dt = new DataTable();
                    StringBuilder strb = new StringBuilder();
                    strb.AppendFormat(" FtypeNmae = '{0}' and Name = '{1}'", SampleName, ItemName);
                    dt = bll.GetAsDataTable(strb.ToString(), string.Empty, 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string FtypeNmae = dt.Rows[0]["FtypeNmae"].ToString();
                        string Name = dt.Rows[0]["Name"].ToString();
                        string ItemDes = dt.Rows[0]["ItemDes"].ToString();
                        string sign = dt.Rows[0]["Demarcate"].ToString();
                        string StandardValue = dt.Rows[0]["StandardValue"].ToString();
                        string Unit = dt.Rows[0]["Unit"].ToString();
                        double ValueInt = Convert.ToDouble(dt.Rows[0]["StandardValue"].ToString());
                        if (SampleName.Trim() == FtypeNmae.Trim() && ItemName.Trim() == Name.Trim())
                        {
                            Qualifide[1] = ItemDes;
                            Qualifide[2] = StandardValue;
                            Qualifide[3] = sign;
                            Qualifide[4] = Unit;
                            switch (sign)
                            {
                                case "≤":
                                    if (strInt <= ValueInt)
                                    {
                                        Qualifide[0] = "合格";
                                    }
                                    else
                                    {
                                        Qualifide[0] = "不合格";
                                    }
                                    break;
                                case "<":
                                    if (strInt < ValueInt)
                                    {
                                        Qualifide[0] = "合格";
                                    }
                                    else
                                    {
                                        Qualifide[0] = "不合格";
                                    }
                                    break;
                                case "≥":
                                    if (strInt >= ValueInt)
                                    {
                                        Qualifide[0] = "合格";
                                    }
                                    if (strInt < ValueInt)
                                    {
                                        Qualifide[0] = "不合格";
                                    }
                                    break;
                                case ">":
                                    if (strInt > ValueInt)
                                    {
                                        Qualifide[0] = "合格";
                                    }
                                    if (strInt <= ValueInt)
                                    {
                                        Qualifide[0] = "不合格";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    else
                    {
                        Qualifide[0] = "参考国标";
                        Qualifide[1] = string.Empty;
                        Qualifide[2] = string.Empty;
                        Qualifide[3] = string.Empty;
                        return Qualifide;
                    }
                }
            }
            catch (Exception)
            {
                Qualifide[0] = "参考国标";
                Qualifide[1] = string.Empty;
                Qualifide[2] = string.Empty;
                Qualifide[3] = string.Empty;
                return Qualifide;
            }
            return Qualifide;
        }


        public void DownTaskMain()
        {

            msgThread = new MsgThread(this);
            msgThread.Start();
            Message msg = new Message();
            msg.what = MsgCode.MSG_DownTask;
            msg.obj1 = Global.samplenameadapter[0];
            Global.workThread.SendMessage(msg, msgThread);
        }

        class MsgThread : ChildThread
        {
            TestResultConserve wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(TestResultConserve wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DownTask:
                        if (!string.IsNullOrEmpty(msg.DownLoadTask))
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("下载数据错误,或者服务链接不正常，请联系管理员！");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        private string DownloadTask(string TaskTemp)
        {
            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();

            DataSet dataSet = new DataSet();
            DataTable dtbl = new DataTable();
            using (StringReader sr = new StringReader(TaskTemp))
            {
                dataSet.ReadXml(sr);
            }
            int len = 0;
            if (dataSet != null)
            {
                len = dataSet.Tables[0].Rows.Count;
                dtbl = dataSet.Tables[0];
            }
            //任务
            clsTaskOpr bll = new clsTaskOpr();
            //bll.Delete(string.Empty, out delErr);
            sb.Append(delErr);

            if (len == 0)
            {
                return string.Empty;
            }
            clsTask Tst = new clsTask();
            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                Tst.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                Tst.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                Tst.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                Tst.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                Tst.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                Tst.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                Tst.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                Tst.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                Tst.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                Tst.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                Tst.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                Tst.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                Tst.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                Tst.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();

                bll.Insert(Tst, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.Append(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        }

    }
}