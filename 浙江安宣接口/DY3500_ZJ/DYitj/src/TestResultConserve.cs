using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace AIO
{
    public class TestResultConserve : Window
    {
        private MsgThread msgThread;
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private static tlsTtResultSecond _model = new tlsTtResultSecond();
        private static DateTime _nowTime = DateTime.Now;

        /// <summary>
        /// All New Lee
        /// </summary>
        public TestResultConserve()
        {

        }

        /// <summary>
        /// ResultSave
        /// </summary>
        /// <param name="result"></param>
        public static int ResultConserve(string[,] result)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (result == null)
                return CountLength;
            _model = new tlsTtResultSecond();
            _nowTime = DateTime.Now;
            try
            {
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]))
                    {
                        _model.SysCode = Global.GETGUID(null, 1);
                        _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff") + result[i, 11];//检测编号
                        _model.CheckType = "抽检";
                        _model.SampleCode = result[i, 11];//样品编号

                        _model.CheckedCompany = result[i, 14];//被检对象名称
                        _model.CheckedCompanyCode = !string.IsNullOrEmpty(result[i, 14]) ? clsCompanyOpr.NameFromCode(result[i, 14]) : string.Empty;
                        _model.FoodName = result[i, 8];//样品名称
                        //_model.FoodType = string.Empty;//样品种类
                        //string name = _model.FoodName;
                        //if (name.Length > 0)
                        //{
                        //    DataTable dt = _bll.GetAsDataTable(string.Format("FtypeNmae Like '{0}'", name), string.Empty, 7, 0);
                        //    if (dt != null && dt.Rows.Count > 0)
                        //    {
                        //        name = dt.Rows[0]["SampleNum"].ToString();//样品编号
                        //        if (name.Length == 5)
                        //        {
                        //            _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();
                        //        }
                        //        else
                        //        {
                        //            dt = _bll.GetAsDataTable(string.Format("SampleNum Like '{0}'", name.Substring(0, name.Length - 5)), string.Empty, 7, 0);
                        //            if (dt != null && dt.Rows.Count > 0)
                        //            {
                        //                _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();//样品编号
                        //            }
                        //        }
                        //    }
                        //}

                        _model.CheckTotalItem = result[i, 2];//检测项目
                        _model.CheckValueInfo = result[i, 4];
                        _model.StandValue = result[i, 10];//检测标准值
                        _model.Result = result[i, 9];//检测结论 （合格、不合格）

                        _model.ResultInfo = result[i, 5];//检测值单位
                        //当判定符号为%时，对检测值进行数字转换
                        if (_model.ResultInfo != null && _model.ResultInfo.Equals("%"))
                            _model.CheckValueInfo = Convert.ToDouble(Global.StringConvertDouble(_model.CheckValueInfo) * 100).ToString("F3");
                        else if (!_model.CheckValueInfo.Equals("0"))
                        {
                            if (Global.StringConvertDouble(_model.CheckValueInfo) != 0)
                                _model.CheckValueInfo = Global.StringConvertDouble(_model.CheckValueInfo).ToString("F3");
                        }

                        _model.CheckPlace = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].Organization : string.Empty; //检测设备型号
                        _model.CheckPlaceCode = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPlaceCode : string.Empty; //行政机构代码
                        _model.CheckUnitName = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointName : string.Empty; //

                        _model.UpLoader = result[i, 7];//基层上传人
                        _model.TakeDate = _nowTime.ToString("yyyy-MM-dd");//抽样日期
                        _model.Standard = result[i, 12];//检测依据

                        _model.ResultType = result[i, 1]; //检测类别=检测手段
                        _model.Hole = result[i, 0];//检测孔
                        _model.CheckStartDate = result[i, 6]; //检测日期=检测时间
                        _model.CheckUnitInfo = result[i, 7]; //检测人员=检测人姓名
                        _model.Organizer = result[i, 7];//抽样人
                        _model.CheckMethod = result[i, 3]; //检测方法
                        _model.CheckPlanCode = result[i, 13]; //任务编号
                        _model.APRACategory = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointType : string.Empty;
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.MachineCompany = "广东达元绿洲食品安全科技股份有限公司"; //检测设备生产厂家
                        _model.ContrastValue = string.Empty;
                        _model.SampleId = result[i, 15];
                        _model.DeviceId = Wisdom.DeviceID;
                        _model.ProduceCompany = result[i, 16];
                        _model.ItemCode = Global.itemCode;//浙江检定项目编号

                        _bll.Insert(_model, out ourEgg);
                        if (ourEgg.Equals(string.Empty))
                            CountLength++;
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
        /// <param name="result"></param>
        public static int ResultConserve(string[,] result, bool contrast, string ContrastValue)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (result == null)
                return CountLength;
            try
            {
                _model = new tlsTtResultSecond();
                _nowTime = DateTime.Now;
                int forLenght = contrast ? result.GetLength(0)-1  : result.GetLength(0);
                for (int i = 0; i < forLenght; i++)
                {
                    if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]))
                    {
                        _model.SysCode = Global.GETGUID(null, 1);
                        _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff") + result[i, 11];//检测编号
                        _model.CheckType = "抽检";
                        _model.SampleCode = result[i, 11];//样品编号

                        _model.CheckedCompany = result[i, 14];//被检对象名称
                        _model.CheckedCompanyCode = !string.IsNullOrEmpty(result[i, 14]) ? clsCompanyOpr.NameFromCode(result[i, 14]) : string.Empty;
                        _model.FoodName = result[i, 8];//样品名称
                        //string name = _model.FoodName;
                        //if (name.Length > 0)
                        //{
                        //    DataTable dt = _bll.GetAsDataTable(string.Format("FtypeNmae Like '{0}'", name), string.Empty, 7, 0);
                        //    if (dt != null && dt.Rows.Count > 0)
                        //    {
                        //        name = dt.Rows[0]["SampleNum"].ToString();//样品编号
                        //        if (name.Length <= 5)
                        //        {
                        //            if (name.Length == 5)
                        //            {
                        //                _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();
                        //            }
                        //            else
                        //            {
                        //                _model.FoodType = "自建样品";
                        //            }
                        //        }
                        //        else
                        //        {
                        //            dt = _bll.GetAsDataTable(string.Format("SampleNum Like '{0}'", name.Substring(0, name.Length - 5)), string.Empty, 7, 0);
                        //            if (dt != null && dt.Rows.Count > 0)
                        //            {
                        //                _model.FoodType = dt.Rows[0]["FtypeNmae"].ToString();//样品编号
                        //            }
                        //        }
                        //    }
                        //}

                        _model.CheckTotalItem = result[i, 2];//检测项目
                        _model.CheckValueInfo = result[i, 4];
                        _model.StandValue = result[i, 10];//检测标准值
                        _model.Result = result[i, 9];//检测结论 （合格、不合格）

                        _model.ResultInfo = result[i, 5];//检测值单位
                        //当判定符号为%时，对检测值进行数字转换
                        if (_model.ResultInfo != null && _model.ResultInfo.Equals("%"))
                            _model.CheckValueInfo = Convert.ToDouble(Global.StringConvertDouble(_model.CheckValueInfo) * 100).ToString("F3");
                        else if (!_model.CheckValueInfo.Equals("0"))
                        {
                            if (Global.StringConvertDouble(_model.CheckValueInfo) != 0)
                                _model.CheckValueInfo = Global.StringConvertDouble(_model.CheckValueInfo).ToString("F3");
                        }

                        _model.CheckPlace = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].Organization : string.Empty;
                        _model.CheckPlaceCode = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPlaceCode : string.Empty; 
                        _model.CheckUnitName = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointName : string.Empty;

                        _model.UpLoader = result[i, 7];//基层上传人
                        _model.TakeDate = _nowTime.ToString("yyyy-MM-dd");//抽样日期
                        _model.Standard = result[i, 12];//检测依据

                        _model.ResultType = result[i, 1]; //检测类别=检测手段
                        _model.Hole = result[i, 0];//检测孔
                        _model.CheckStartDate = result[i, 6]; //检测日期=检测时间
                        _model.CheckUnitInfo = result[i, 7]; //检测人员=检测人姓名
                        _model.Organizer = result[i, 7];//抽样人
                        _model.CheckMethod = result[i, 3]; //检测方法
                        _model.CheckPlanCode = result[i, 13]; //任务编号
                        _model.APRACategory = Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0].CheckPointType : string.Empty;
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.MachineCompany = "广东达元绿洲食品安全科技股份有限公司"; //检测设备生产厂家
                        _model.ContrastValue = ContrastValue;
                        _model.SampleId = result[i, 15];
                        _model.DeviceId = Wisdom.DeviceID;
                        _model.ProduceCompany = result[i, 16];
                        _model.ItemCode =Global.itemCode ;
                        _bll.Insert(_model, out ourEgg);
                        if (ourEgg.Equals(string.Empty))
                            CountLength++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存数据时出现异常!请联系管理员!\n\t" + ex.Message);
            }
            return CountLength;
        }

        /// <summary>
        /// ReadResultSave
        /// </summary>
        /// <param name="result"></param>
        public static int ReadResultSave(string[,] result)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            if (result == null)
                return CountLength;
            try
            {
                _model = new tlsTtResultSecond();
                _nowTime = DateTime.Now;
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]))
                    {
                        _model.ResultType = result[i, 0];
                        if (!string.IsNullOrEmpty(result[i, 1]))
                            _model.CheckNo = result[i, 1];
                        else
                            _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff");//检测编号
                        _model.SampleCode = result[i, 2];
                        _model.CheckPlaceCode = result[i, 3];
                        _model.CheckPlace = result[i, 4];
                        _model.FoodName = result[i, 5];
                        _model.TakeDate = result[i, 6];
                        _model.CheckStartDate = result[i, 7];
                        _model.Standard = result[i, 8];
                        _model.CheckMachine = result[i, 9];
                        _model.CheckMachineModel = result[i, 10];
                        _model.MachineCompany = result[i, 11];
                        _model.CheckTotalItem = result[i, 12];
                        _model.CheckValueInfo = result[i, 13];
                        _model.StandValue = result[i, 14];
                        _model.Result = result[i, 15];
                        _model.ResultInfo = result[i, 16];
                        _model.CheckUnitName = result[i, 17];
                        _model.CheckUnitInfo = result[i, 18];
                        _model.Organizer = result[i, 19];
                        _model.UpLoader = result[i, 20];
                        _model.ReportDeliTime = result[i, 21];
                        _model.IsReconsider = Convert.ToBoolean(result[i, 22]);
                        _model.ReconsiderTime = result[i, 23];
                        _model.ProceResults = result[i, 24];
                        _model.CheckedCompanyCode = result[i, 25];
                        _model.CheckedCompany = result[i, 26];
                        _model.CheckedComDis = result[i, 27];
                        _model.CheckPlanCode = result[i, 28];
                        _model.DateManufacture = result[i, 29];
                        _model.CheckMethod = result[i, 30];
                        _model.APRACategory = result[i, 31];
                        _model.Hole = result[i, 32];
                        _model.SamplingPlace = result[i, 33];
                        _model.CheckType = result[i, 34];

                        _bll.Insert(_model, out ourEgg);
                        if (ourEgg.Equals(string.Empty))
                            CountLength++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存数据时出现异常!请联系管理员!\n\t" + ex.Message);
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
            string[] Qualifide = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            try
            {
                if (str.Contains("%"))
                    str = str.Replace("%", string.Empty);

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
                    double strInt = Global.StringConvertDouble(str);
                    clsttStandardDecideOpr bll = new clsttStandardDecideOpr();
                    DataTable dt = new DataTable();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(" FtypeNmae like '{0}' and Name='{1}'", SampleName, ItemName);
                    dt = bll.GetAsDataTable(sb.ToString(), string.Empty, 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string strData = string.Empty;
                        strData = dt.Rows[0]["FtypeNmae"] != null ? dt.Rows[0]["FtypeNmae"].ToString() : string.Empty;
                        string FtypeNmae = strData;
                        strData = dt.Rows[0]["Name"] != null ? dt.Rows[0]["Name"].ToString() : string.Empty;
                        string Name = strData;
                        strData = dt.Rows[0]["ItemDes"] != null ? dt.Rows[0]["ItemDes"].ToString() : string.Empty;
                        string ItemDes = strData;
                        strData = dt.Rows[0]["Demarcate"] != null ? dt.Rows[0]["Demarcate"].ToString() : string.Empty;
                        string sign = strData;
                        strData = dt.Rows[0]["StandardValue"] != null ? dt.Rows[0]["StandardValue"].ToString() : string.Empty;
                        string StandardValue = strData;
                        strData = dt.Rows[0]["Unit"] != null ? dt.Rows[0]["Unit"].ToString() : string.Empty;
                        string Unit = strData;
                        double ValueInt = Global.StringConvertDouble(StandardValue);
                        if (SampleName.Trim() == FtypeNmae.Trim() && ItemName.Trim() == Name.Trim())
                        {
                            Qualifide[1] = ItemDes ?? string.Empty;
                            Qualifide[2] = StandardValue ?? string.Empty;
                            Qualifide[3] = sign ?? string.Empty;
                            Qualifide[4] = Unit ?? string.Empty;
                            switch (sign)
                            {
                                case "≤":
                                    Qualifide[0] = strInt <= ValueInt ? "合格" : "不合格";
                                    break;
                                case "<":
                                    Qualifide[0] = strInt < ValueInt ? "合格" : "不合格";
                                    break;
                                case "≥":
                                    Qualifide[0] = strInt >= ValueInt ? "合格" : "不合格";
                                    break;
                                case ">":
                                    Qualifide[0] = strInt > ValueInt ? "合格" : "不合格";
                                    break;
                                default:
                                    Qualifide[0] = "参考国标";
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
                        Qualifide[4] = string.Empty;
                        return Qualifide;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常，请联系管理员!\n错误信息：" + ex.Message);
            }
            return Qualifide;
        }

        public void DownTaskMain()
        {
            msgThread = new MsgThread(this);
            msgThread.Start();
            Message msg = new Message()
            {
                what = MsgCode.MSG_DownTask,
                obj1 = Global.samplenameadapter[0]
            };
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
                            Console.WriteLine("下载数据错误,或者服务链接不正常，请联系管理员!");
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
                //MessageBox.Show("暂无任务!");
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
                    //continue;
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