using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
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
        public static int ResultConserve(string[,] result,out string[] syscodes)
        {
            string ourEgg = string.Empty;
            int CountLength = 0;
            syscodes = null;

            if (result == null)
                return CountLength;
            _model = new tlsTtResultSecond();
            _nowTime = DateTime.Now;
            try
            {
                syscodes = new string[result.GetLength(0)];
                CheckPointInfo Cpoint = Global.samplenameadapter != null && Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0] : null;
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
                        _model.FoodType = string.Empty;//样品种类
                        string name = _model.FoodName;
                        try
                        {
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
                        }
                        catch (Exception)
                        {
                            _model.FoodType = "样品";
                        }

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

                        if (Cpoint != null)
                        {
                            _model.CheckPlace = Cpoint.orgName;
                            _model.CheckPlaceCode = Cpoint.orgNum;
                            _model.CheckUnitName = Cpoint.pointName;
                            _model.APRACategory = Cpoint.pointType;
                        }
                        else
                        {
                            _model.CheckPlace = "食品药品监督管理局";
                            _model.CheckPlaceCode = "0010010001";
                            _model.CheckUnitName = "食品药品监督管理局检测中心";
                            _model.APRACategory = "检测中心";
                        }
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
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.MachineCompany = "广东达元绿洲食品安全科技股份有限公司"; //检测设备生产厂家
                        _model.ContrastValue = string.Empty;
                        _model.SampleId = result[i, 15];
                        _model.DeviceId = Wisdom.DeviceID;
                        _model.ProduceCompany = result[i, 16];
                        _model.taskID = result[i, 17];

                        if (Global.ManuTest == true)
                        {
                            _model.shoudong = "是";
                            _model.Opertor = result[i, 18];
                            _model.OpertorID  = result[i, 19];
                            //_model.TBCValue = result[i, 20];

                            _bll.Insert(_model, out ourEgg);
                            if (ourEgg.Equals(string.Empty))
                            {
                                CountLength++;
                                syscodes[i] = _model.SysCode;
                            }
                                
                        }
                        else
                        {
                            //_model.TBCValue = result[i, 18];
                            //根据检测任务ID判断是否重检，重检就跟新检测结果
                            DataTable idt = _bll.SearchSaveResult("taskid='" + result[i, 17] + "'", "", "", out ourEgg);
                            if (idt != null && idt.Rows.Count > 0)
                            {
                                _bll.UpdateResult(_model, out ourEgg);
                            }
                            else
                            {
                                _bll.Insert(_model, out ourEgg);
                            }
                            if (ourEgg.Equals(string.Empty))
                            {
                                CountLength++;
                                syscodes[i] = _model.SysCode;
                            }
                               
                            //_bll.UpdateTaskTest(result[i, 17], out ourEgg);
                            if (Global.IsSweepCode)
                            {
                                _bll.UpdateBarTask(result[i, 17], out ourEgg);
                            }
                            else
                            {
                                _bll.UpdateTaskTest(result[i, 17], out ourEgg);
                            }
                        }
                        //_bll.Insert(_model, out ourEgg);
                        //if (ourEgg.Equals(string.Empty))
                        //    CountLength++;
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
                int forLenght = contrast ? result.GetLength(0) - 1 : result.GetLength(0);
                CheckPointInfo Cpoint = Global.samplenameadapter != null && Global.samplenameadapter.Count > 0 ? Global.samplenameadapter[0] : null;
                for (int i = 0; i < forLenght; i++)
                {
                    if (!result[i, 4].ToString().Contains("NA") && !result[i, 4].ToString().Contains("*") && !string.IsNullOrEmpty(result[i, 4]) && !result[i, 4].ToString().Contains("对照样"))
                    {
                        _model.SysCode = Global.GETGUID(null, 1);
                        _model.CheckNo = _nowTime.ToString("yyyyMMddHHmmssff") + result[i, 11];//检测编号
                        _model.CheckType = "抽检";
                        _model.SampleCode = result[i, 11];//样品编号

                        _model.CheckedCompany = result[i, 14];//被检对象名称
                        _model.CheckedCompanyCode = !string.IsNullOrEmpty(result[i, 14]) ? clsCompanyOpr.NameFromCode(result[i, 14]) : string.Empty;
                        _model.FoodName = result[i, 8];//样品名称
                        string name = _model.FoodName;
                        try
                        {
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
                        }
                        catch (Exception)
                        {
                            _model.FoodType = "样品";
                        }

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

                        if (Cpoint != null)
                        {
                            _model.CheckPlace = Cpoint.orgName;
                            _model.CheckPlaceCode = Cpoint.orgNum;
                            _model.CheckUnitName = Cpoint.pointName;
                            _model.APRACategory = Cpoint.pointType;
                        }
                        else
                        {
                            _model.CheckPlace = "食品药品监督管理局";
                            _model.CheckPlaceCode = "0010010001";
                            _model.CheckUnitName = "食品药品监督管理局检测中心";
                            _model.APRACategory = "检测中心";
                        }

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
                        _model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                        _model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                        _model.MachineCompany = "广东达元绿洲食品安全科技股份有限公司"; //检测设备生产厂家
                        _model.ContrastValue = ContrastValue;
                        _model.SampleId = result[i, 15];
                        _model.DeviceId = Wisdom.DeviceID;
                        _model.ProduceCompany = result[i, 16];

                        if (Global.ManuTest == true)
                        {
                            _model.shoudong  = result[i, 17];
                            _model.Opertor = result[i, 18];
                            _model.OpertorID  = result[i, 19];
                        }
                        else
                        {
                            _model.taskID = result[i, 17];
                        }
                        
                        if (Global.ManuTest == true)
                        {
                            _model.shoudong = "是";
                            _bll.Insert(_model, out ourEgg);
                            if (ourEgg.Equals(string.Empty))
                                CountLength++;
                        }
                        else 
                        {
                            //根据检测任务ID判断是否重检，重检就跟新检测结果
                            DataTable idt = _bll.SearchSaveResult("taskid='" + result[i, 17] + "'", "", "", out ourEgg);
                            if (idt != null && idt.Rows.Count > 0)
                            {
                                _bll.UpdateResult(_model, out ourEgg);
                            }
                            else
                            {
                                _bll.Insert(_model, out ourEgg);
                            }
                            if (ourEgg.Equals(string.Empty))
                                CountLength++;
                            if (Global.IsSweepCode)
                            {
                                _bll.UpdateBarTask(result[i, 17], out ourEgg);
                            }
                            else
                            {
                                _bll.UpdateTaskTest(result[i, 17], out ourEgg);
                            }
                           
                           
                        }
                       
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
                    //获去快检服务的标准
                    //sb.Append("s.item_id=item.cid and s.food_id=f.fid ");
                    //sb.AppendFormat("and item.detect_item_name='{0}' and f.food_name='{1}'", ItemName, SampleName);
                    //sb.Append(" and d.sid=item.standard_id");

                    sb.Append("s.item_id=item.cid and s.food_id=f.fid ");
                    sb.AppendFormat("and item.detect_item_name='{0}' and f.food_name='{1}'", ItemName, SampleName);
                    sb.Append(" and s.delete_flag='0' and s.checked='1' and item.checked='1' and item.delete_flag='0' and f.delete_flag='0' and f.checked='1' ");
                    
                    dt = bll.GetAsDataTable(sb.ToString(), string.Empty, 5);
                 
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                        string strData = string.Empty;
                        strData = dt.Rows[0]["food_name"] != null ? dt.Rows[0]["food_name"].ToString() : string.Empty;
                        string FtypeNmae = strData;
                        strData = dt.Rows[0]["detect_item_name"] != null ? dt.Rows[0]["detect_item_name"].ToString() : string.Empty;
                        string Name = strData;

                        DataTable dtsd = bll.GetAsDataTable("sid=" + "'" + dt.Rows[0]["standard_id"].ToString() + "'" + " and delete_flag='0'", string.Empty, 10);
                        strData = "";
                        if (dtsd != null && dtsd.Rows.Count > 0)
                        {
                            strData = dtsd.Rows[0]["std_code"] != null ? dtsd.Rows[0]["std_code"].ToString() : string.Empty;
                        }
                        string ItemDes = strData;
                        strData = dt.Rows[0]["detect_sign"] != null ? dt.Rows[0]["detect_sign"].ToString() : string.Empty;
                        string sign = strData;
                        strData = dt.Rows[0]["detect_value"] != null ? dt.Rows[0]["detect_value"].ToString() : string.Empty;
                        string StandardValue = strData;
                        strData = dt.Rows[0]["detect_value_unit"] != null ? dt.Rows[0]["detect_value_unit"].ToString() : string.Empty;
                        string Unit = strData;
                        //如果标准库查不到值就根据检测项目查
                        if (dt.Rows[0]["detect_value"].ToString().Trim() == "" || dt.Rows[0]["detect_sign"].ToString().Trim() == "")
                        {
                            sb.Length = 0;
                            sb.AppendFormat(" d.detect_item_name='{0}' and s.sid=d.standard_id and d.checked='1' and d.delete_flag='0' and s.delete_flag='0' and s.checked='1' ", ItemName);
                            DataTable dts = bll.GetAsDataTable(sb.ToString(), string.Empty, 9);
                            if (dts != null && dts.Rows.Count > 0)
                            {
                                ItemDes = dts.Rows[0]["std_code"].ToString();
                                sign = dts.Rows[0]["detect_sign"].ToString();
                                StandardValue = dts.Rows[0]["detect_value"].ToString();
                                Unit = dts.Rows[0]["detect_value_unit"].ToString();
                            }
                        }

                        if (StandardValue.Contains("%"))
                            StandardValue = StandardValue.Replace("%", string.Empty);
                        double ValueInt = Global.StringConvertDouble(StandardValue);
                  
                        Qualifide[1] = ItemDes ?? string.Empty;//检测依据
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
                    else
                    {
                        sb.Length = 0;
                        string fparentid = "";
                        //查询出样品的父ID
                        sb.AppendFormat("food_name='{0}' and delete_flag='0' and checked='1'", SampleName);
                        dt = bll.GetAsDataTable(sb.ToString(), string.Empty, 6);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            fparentid = dt.Rows[0]["parent_id"].ToString();
                            sb.Length = 0;
                            sb.AppendFormat("s.item_id=item.cid and s.food_id=f.fid and item.detect_item_name='{0}' and f.fid='{1}' and d.sid=item.standard_id", ItemName, fparentid);
                            sb.Append(" and s.delete_flag='0' and s.checked='1' and item.checked='1' and item.delete_flag='0' and f.delete_flag='0' and f.checked='1' and d.delete_flag='0' ");
                            dt = bll.GetAsDataTable(sb.ToString(), string.Empty, 7);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string strData = string.Empty;
                                strData = dt.Rows[0]["food_name"] != null ? dt.Rows[0]["food_name"].ToString() : string.Empty;
                                string FtypeNmae = strData;
                                strData = dt.Rows[0]["detect_item_name"] != null ? dt.Rows[0]["detect_item_name"].ToString() : string.Empty;
                                string Name = strData;
                                strData = dt.Rows[0]["std_code"] != null ? dt.Rows[0]["std_code"].ToString() : string.Empty;
                                string ItemDes = strData;
                                strData = dt.Rows[0]["detect_sign"] != null ? dt.Rows[0]["detect_sign"].ToString() : string.Empty;
                                string sign = strData;
                                strData = dt.Rows[0]["detect_value"] != null ? dt.Rows[0]["detect_value"].ToString() : string.Empty;
                                string StandardValue = strData;
                                strData = dt.Rows[0]["detect_value_unit"] != null ? dt.Rows[0]["detect_value_unit"].ToString() : string.Empty;
                                string Unit = strData;

                                if (StandardValue.Contains("%"))
                                    StandardValue = StandardValue.Replace("%", string.Empty);
                                double ValueInt = Global.StringConvertDouble(StandardValue);

                                Qualifide[1] = ItemDes ?? string.Empty;//检测依据
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
                            else
                            {
                                sb.Length = 0;
                                sb.AppendFormat(" d.detect_item_name='{0}' and s.sid=d.standard_id and d.checked='1' and d.delete_flag='0' and s.delete_flag='0' and s.checked='1' ", ItemName);
                                dt = bll.GetAsDataTable(sb.ToString(), string.Empty, 8);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string strData = string.Empty;
                                    //strData = dt.Rows[0]["food_name"] != null ? dt.Rows[0]["food_name"].ToString() : string.Empty;
                                    string FtypeNmae = SampleName;
                                    strData = dt.Rows[0]["detect_item_name"] != null ? dt.Rows[0]["detect_item_name"].ToString() : string.Empty;
                                    string Name = strData;
                                    strData = dt.Rows[0]["std_code"] != null ? dt.Rows[0]["std_code"].ToString() : string.Empty;
                                    string ItemDes = strData;
                                    strData = dt.Rows[0]["detect_sign"] != null ? dt.Rows[0]["detect_sign"].ToString() : string.Empty;
                                    string sign = strData;
                                    strData = dt.Rows[0]["detect_value"] != null ? dt.Rows[0]["detect_value"].ToString() : string.Empty;
                                    string StandardValue = strData;
                                    strData = dt.Rows[0]["detect_value_unit"] != null ? dt.Rows[0]["detect_value_unit"].ToString() : string.Empty;
                                    string Unit = strData;

                                    if (StandardValue.Contains("%"))
                                        StandardValue = StandardValue.Replace("%", string.Empty);
                                    double ValueInt = Global.StringConvertDouble(StandardValue);

                                    Qualifide[1] = ItemDes ?? string.Empty;//检测依据
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
                                else
                                {
                                    Qualifide[0] = "参考国标";
                                    Qualifide[1] = string.Empty;
                                    Qualifide[2] = string.Empty;
                                    Qualifide[3] = string.Empty;
                                    Qualifide[4] = string.Empty;
                                }
                            }
                        }
                        else 
                        {
                            sb.Length = 0;
                            sb.AppendFormat(" d.detect_item_name='{0}' and s.sid=d.standard_id and d.checked='1' and d.delete_flag='0' and s.delete_flag='0' and s.checked='1' ", ItemName);
                            dt = bll.GetAsDataTable(sb.ToString(), string.Empty,8);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string strData = string.Empty;
                                //strData = dt.Rows[0]["food_name"] != null ? dt.Rows[0]["food_name"].ToString() : string.Empty;
                                string FtypeNmae = SampleName;
                                strData = dt.Rows[0]["detect_item_name"] != null ? dt.Rows[0]["detect_item_name"].ToString() : string.Empty;
                                string Name = strData;
                                strData = dt.Rows[0]["std_code"] != null ? dt.Rows[0]["std_code"].ToString() : string.Empty;
                                string ItemDes = strData;
                                strData = dt.Rows[0]["detect_sign"] != null ? dt.Rows[0]["detect_sign"].ToString() : string.Empty;
                                string sign = strData;
                                strData = dt.Rows[0]["detect_value"] != null ? dt.Rows[0]["detect_value"].ToString() : string.Empty;
                                string StandardValue = strData;
                                strData = dt.Rows[0]["detect_value_unit"] != null ? dt.Rows[0]["detect_value_unit"].ToString() : string.Empty;
                                string Unit = strData;

                                if (StandardValue.Contains("%"))
                                    StandardValue = StandardValue.Replace("%", string.Empty);
                                double ValueInt = Global.StringConvertDouble(StandardValue);

                                Qualifide[1] = ItemDes ?? string.Empty;//检测依据
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
                            else
                            {
                                Qualifide[0] = "参考国标";
                                Qualifide[1] = string.Empty;
                                Qualifide[2] = string.Empty;
                                Qualifide[3] = string.Empty;
                                Qualifide[4] = string.Empty;
                            }
                        }
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
    }
}