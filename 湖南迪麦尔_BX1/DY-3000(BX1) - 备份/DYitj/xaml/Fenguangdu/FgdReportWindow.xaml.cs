using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using System.Text;
using AIO.src.xprinter;

namespace AIO
{
    /// <summary>
    /// FgdReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdReportWindow : Window
    {

        #region 全局变量
        public DYFGDItemPara _item = null;
        public FgdCaculate.AT[] _firstATs, _lastATs;
        public bool[] _use;
        public double _preHeatms, _realHeatms;
        public bool _contrast = false;
        public bool _sample = false;
        private static string _yizhilvfajg = "yizhilvfajg";
        private static string _bzquxianjg = "bzquxianjg";
        private static string _zxmfajg = "zxmfajg";
        private static string _dlxfajg = "dlxfajg";
        private static string _xishufajg = "xishufajg";
        private string[] _methodNames = { _yizhilvfajg, _bzquxianjg, _zxmfajg, _dlxfajg, _xishufajg };
        private string[] _methodToString = { "抑制率法", "标准曲线法", "子项目法", "动力学法", "系数法" };
        private static string _strchannel = "channel";
        private static string _strsamplenum = "samplenum";
        private static string _strsamplename = "samplename";
        //private static string _strdetecttime = "detecttime";
        private static string _strdetectresult = "detectresult";
        private static string _strJudgmentValueTemp = "JudgmentValueTemp";
        private static string _strStandardNameTemp = "StandardNameTemp";
        private static string _strStandValue = "StandValue";
        private List<Label> _listDetectResult = null;
        private List<Label> _listJudmentValue = null;
        private List<Label> _listStandardName = null;
        private List<Label> _StandValue = null;
        private List<double> _dishuOrbeishuList = new List<double>();
        private string[,] _CheckValue;
        private int _HoleNumber = 1;
        private int _AllNumber = 0;
        private List<Label> _listUnit = null;
        private List<string> _listStrRecord = null;
        private DateTime _date = DateTime.Now;
        private MsgThread _msgThread;
        public string _ContrastValue = "";
        private DispatcherTimer _DataTimer = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private bool IsUpLoad = false;
        private string logType = "FgdReportWindow-error";
        #endregion

        public FgdReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更新对照值label
        /// </summary>
        private void UpdateLable()
        {
            try
            {
                if (_item.Method == 0 && _item.ir.RefDeltaA != Double.MinValue)
                    this.label1.Content = "对照值：" + String.Format("{0:F3}", _item.ir.RefDeltaA);
                else if (_item.Method == 1 && _item.sc.RefA != Double.MinValue)
                    this.label1.Content = "对照值：" + String.Format("{0:F3}", _item.sc.RefA);
                else
                    this.LabelInfo.Content = "";
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_item == null)
                    return;
                LabelDate.Content = _date.ToString("yyyy-MM-dd HH:mm:ss");
                _listDetectResult = new List<Label>();
                _listJudmentValue = new List<Label>();
                _listStandardName = new List<Label>();
                _StandValue = new List<Label>();
                _listUnit = new List<Label>();
                _listStrRecord = new List<string>();
                _use = new bool[Global.deviceHole.HoleCount];
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    _dishuOrbeishuList.Add(_item.Hole[i].DishuOrBeishu > 0 ? _item.Hole[i].DishuOrBeishu : 1);
                    if (_item.Method == 0 || _item.Method == 3)
                    {
                        if (_item.Hole[i].Use && _item.Hole[i].IsTest)
                        {
                            _use[i] = true;
                            _CheckValue = new string[_HoleNumber, 21];
                            _HoleNumber++;
                        }
                        else
                            _use[i] = false;
                    }
                    else
                    {
                        if (_item.Hole[i].Use)
                        {
                            _use[i] = true;
                            _CheckValue = new string[_HoleNumber, 21];
                            _HoleNumber++;
                        }
                        else
                            _use[i] = false;
                    }
                }
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_use[i])
                    {
                        UIElement element = GenerateReportLine(String.Format("{0:D2}", (i + 1)), String.Format("{0:D5}", sampleNum++), _item.Hole[i].SampleName, string.Empty + _realHeatms / 1000, _item.Unit, string.Empty, string.Empty, string.Empty);
                        StackPanelReport.Children.Add(element);
                        _listDetectResult.Add(UIUtils.GetChildObject<Label>(element, _strdetectresult));
                        _listJudmentValue.Add(UIUtils.GetChildObject<Label>(element, _strJudgmentValueTemp));
                        _listStandardName.Add(UIUtils.GetChildObject<Label>(element, _strStandardNameTemp));
                        _StandValue.Add(UIUtils.GetChildObject<Label>(element, _strStandValue));
                        _listUnit.Add(UIUtils.GetChildObject<Label>(element, "labelUnit"));
                    }
                    else
                    {
                        _listDetectResult.Add(null);
                        _listJudmentValue.Add(null);
                        _listStandardName.Add(null);
                        _StandValue.Add(null);
                        _listUnit.Add(null);
                    }
                }

                ShowResult();

                if (_DataTimer == null)
                {
                    _DataTimer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromSeconds(1.5)
                    };
                    _DataTimer.Tick += new EventHandler(SaveAndUpload);
                    _DataTimer.Start();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 延迟一秒保存和上传数据
        /// 非强制上传权限的用户给予提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAndUpload(object sender, EventArgs e)
        {
            UpdateItem();
            UpdateLable();
            Save();
            if (LoginWindow._userAccount.UpDateNowing)
                Upload();
            else Global.IsStartUploadTimer = true;
            ButtonPrint.IsEnabled = true;
            ButtonUpdate.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _msgThread.Stop();
        }

        private void Save()
        {
            if (Global.InterfaceType.Equals("AH"))
            {
                _AllNumber = TestResultConserve.AhResultConserve(_CheckValue, _contrast, _ContrastValue);
            }
            //else if (Global.InterfaceType.Length == 0 || Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("GS"))
            else
            {
                _AllNumber = TestResultConserve.ResultConserve(_CheckValue, _contrast, _ContrastValue);
            }
        }

        private List<PrintHelper.Report> GenerateReports()
        {
            List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
            try
            {
                DataTable dt = _resultTable.GetAsDataTable("", "", 6, _AllNumber);
                if (dt != null && dt.Rows.Count > 0)
                {
                    IDictionary<string, List<tlsTtResultSecond>> dic = new Dictionary<string, List<tlsTtResultSecond>>();
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    if (dtList.Count > 0)
                    {
                        string key = string.Empty;
                        for (int i = 0; i < dtList.Count; i++)
                        {
                            key = dtList[i].CheckStartDate + "-" + dtList[i].CheckedCompany + "-" + dtList[i].ResultType + "-" + dtList[i].CheckTotalItem;
                            if (!dic.ContainsKey(key))
                            {
                                List<tlsTtResultSecond> rs = new List<tlsTtResultSecond>();
                                rs.Add(dtList[i]);
                                dic.Add(key, rs);
                            }
                            else
                            {
                                dic[key].Add(dtList[i]);
                            }
                        }
                        foreach (var item in dic)
                        {
                            List<tlsTtResultSecond> models = item.Value;
                            PrintHelper.Report model = new PrintHelper.Report
                            {
                                ItemName = models[0].CheckTotalItem,
                                Standard = models[0].Standard,
                                ItemCategory = models[0].ResultType,
                                User = LoginWindow._userAccount.UserName,
                                Unit = models[0].ResultInfo,
                                Judgment = models[0].FoodName,
                                Date = models[0].CheckStartDate,
                                Company = models[0].CheckedCompany,
                                //分光部分项目需要打印对照值
                                ContrastValue = _ContrastValue
                            };

                            if(Global.Is3500I )
                            {
                                for (int i = models.Count - 1; i >= 0; i--)
                                {
                                    model.SampleName.Add(models[i].FoodName);
                                    model.SampleNum.Add(String.Format("{0:D5}", models[i].SampleCode));
                                    model.JudgmentTemp.Add(models[i].Result);
                                    model.Result.Add(models[i].CheckValueInfo);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < models.Count; i++)
                                {
                                    model.SampleName.Add(models[i].FoodName);
                                    model.SampleNum.Add(String.Format("{0:D5}", models[i].SampleCode));
                                    model.JudgmentTemp.Add(models[i].Result);
                                    model.Result.Add(models[i].CheckValueInfo);
                                }
                            }

                            reports.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            return reports;
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //热干胶打印机
                if (Global.Printing[0].XPrinter)
                {
                    DataTable dt = _resultTable.GetAsDataTable("", "", 6, _AllNumber);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        IDictionary<string, List<tlsTtResultSecond>> dic = new Dictionary<string, List<tlsTtResultSecond>>();
                        List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                        if (dtList.Count > 0)
                        {
                           StringBuilder sb = new StringBuilder();
                           for(int i=0;i<dtList.Count;i++)
                           {
                                sb.Clear();
                                sb.AppendFormat("             {0}\n", dtList[i].CheckTotalItem);//检测项目
                                sb.AppendFormat("   检测设备：{0}\n", Global.InstrumentName);
                                sb.AppendFormat("   检测类型：{0}\n", dtList[i].ResultType );
                                sb.AppendFormat("   检测依据：{0}\r\n", dtList[i].Standard);
                                sb.AppendFormat("   检测日期：{0}\r\n", dtList[i].CheckStartDate);
                              
                                sb.AppendFormat("   被检单位：{0}\r\n", dtList[i].CheckedCompany);
                                

                                if (dtList[i].ResultType == "分光光度")//分光光度 数值加单位
                                {
                                    sb.AppendFormat("   编号  样品   结果   判定\r\n", "");
                                    sb.AppendFormat("   {0} {1} {2} {3}\r\n", dtList[i].SampleCode, dtList[i].FoodName, dtList[i].CheckValueInfo + dtList[i].ResultInfo , dtList[i].Result);
                                }
                                else
                                {
                                    sb.AppendFormat("   编号  样品  判定\r\n", "");
                                    sb.AppendFormat("   {0} {1} {2}\r\n", dtList[i].SampleCode, dtList[i].FoodName, dtList[i].Result);
                                }
                                sb.AppendFormat("   检测单位：{0}\r\n", dtList[i].CheckUnitName);
                                sb.AppendFormat("   检测人员：{0}", dtList[i].CheckUnitInfo);

                                PrintModel pmContent2 = new PrintModel();
                                pmContent2.FontFamily = "宋体";
                                pmContent2.FontSize = 9;
                                pmContent2.IsBold = false;
                                pmContent2.Text = new System.IO.StringReader(sb.ToString());

                                //PrintModel[] pms = new PrintModel[] { pmTitle, pmContent1, pmContent2 };
                                PrintModel[] pms = new PrintModel[] { pmContent2 };

                                if(TicketPrinterHelper.Print(pms, sb.ToString()))
                                {
                                    LabelInfo.Content = "提示：打印成功！";
                                }
                                else
                                {
                                    LabelInfo.Content = "提示：打印失败！\r\n请检查驱动！";
                                }
                           }
                        }
                    }
                    else
                    {
                        LabelInfo.Content = "打印失败，请检查驱动。";
                    }
                }
                else
                {
                    //热敏打印机串口的
                    List<byte> data = new List<byte>();
                    List<PrintHelper.Report> reports = GenerateReports();
                    foreach (PrintHelper.Report report in reports)
                        data.AddRange(Global.Is3500I ? report.GeneratePrintBytes_3500I() : report.GeneratePrintBytes());
                    byte[] buffer = new byte[data.Count];
                    data.CopyTo(buffer);
                    Message msg = new Message
                    {
                        what = MsgCode.MSG_PRINT,
                        str1 = Global.strPRINTPORT,
                        data = buffer,
                        arg1 = 0,
                        arg2 = buffer.Length
                    };
                    Global.printThread.SendMessage(msg, _msgThread);
                }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Upload()
        {
            string info = string.Empty;
            if (!Global.UploadCheck(this, out info))
            {
                LabelInfo.Content = info;
                return;
            }

            try
            {
                LabelInfo.Content = "正在上传...";
                tlsttResultSecondOpr Rs = new tlsttResultSecondOpr();
                DataTable dt = Rs.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                dt = Global.CheckDtblValue(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Global.InterfaceType.Equals("DY"))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].UploadUserUUID;
                        }
                    }
                }
                else
                {
                    LabelInfo.Content = "暂无需要上传的数据";
                    Global.IsStartUploadTimer = false;
                    return;
                }

                Message msg = new Message
                {
                    what = MsgCode.MSG_UPLOAD,
                    obj1 = Global.samplenameadapter[0],
                    table = dt
                };
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    msg.selectedRecords = dtList;
                }
                Global.updateThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }

            Upload();
        }

        private UIElement GenerateReportLine(string channel, string sampleNum, string sampleName, string detectTime, string unit, string JudgmentValue, string StandardName, string StandValue)
        {
            StackPanel stackPanel = new StackPanel();
            try
            {
                stackPanel.Orientation = Orientation.Horizontal;

                Label labelChannel = new Label
                {
                    Content = channel,
                    FontSize = 20,
                    Width = 50,
                    Name = _strchannel
                };

                Label labelSampleNum = new Label
                {
                    Content = sampleNum,
                    FontSize = 20,
                    Width = 88,
                    Name = _strsamplenum
                };

                Label labelSampleName = new Label
                {
                    Content = sampleName,
                    FontSize = 20,
                    Width = 170,
                    Name = _strsamplename
                };

                Label labelDetectResult = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 88,
                    Name = _strdetectresult
                };

                Label labelUnit = new Label
                {
                    Content = unit,
                    FontSize = 20,
                    Width = 88,
                    Name = "labelUnit"
                };

                Label labeJudgemtValue = new Label
                {
                    Content = JudgmentValue,
                    FontSize = 20,
                    Width = 88,
                    Name = _strJudgmentValueTemp
                };

                Label labeStandardName = new Label
                {
                    Content = StandardName,
                    FontSize = 20,
                    Width = 100,
                    Name = _strStandardNameTemp
                };
                Label labeStandardValue = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Content = StandValue,
                    FontSize = 20,
                    Width = 90,
                    Name = _strStandValue
                };
                stackPanel.Children.Add(labelChannel);
                stackPanel.Children.Add(labelSampleNum);
                stackPanel.Children.Add(labelSampleName);
                stackPanel.Children.Add(labelDetectResult);
                stackPanel.Children.Add(labelUnit);
                stackPanel.Children.Add(labeJudgemtValue);
                stackPanel.Children.Add(labeStandardName);
                stackPanel.Children.Add(labeStandardValue);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
            return stackPanel;
        }

        private void ShowResult()
        {
            try
            {
                int sampleNum = _item.SampleNum, contrastIdx = 0;
                if (_contrast)
                    sampleNum += 1;
                string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                #region 农药残留，计算抑制率
                if (0 == _item.Method)
                {
                    FgdCaculate.DeltaA[] deltaA = FgdCaculate.CaculateDeltaA(_firstATs, _lastATs);
                    if (_contrast)
                    {
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_use[i])
                            {
                                contrastIdx = i;
                                _item.ir.RefDeltaA = deltaA[i].deltaA;
                                if (!FgdCaculate.IsValid(_item.ir.RefDeltaA))
                                    _listDetectResult[i].Content = "NA";
                                else
                                    _listDetectResult[i].Content = String.Format("{0:F3}", _item.ir.RefDeltaA);
                                _listUnit[i].Content = "ABS";
                                break;
                            }
                        }

                        //2015年10月29日 对照的同时进行样品测试
                        int num = 0;
                        double[] izhilv = FgdCaculate.CaculateIzhilv(deltaA, _item.ir.RefDeltaA);
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_use[i] && i != contrastIdx)
                            {
                                if (FgdCaculate.IsValid(izhilv[i]))
                                {
                                    // 合法，但是是负值，这样的话要归零。
                                    if (izhilv[i] < 0)
                                        izhilv[i] = 0;
                                }
                                string str = "";
                                string[] UnqualifiedValue = new string[4];

                                if (!FgdCaculate.IsValid(izhilv[i]))
                                {
                                    str = "NA";
                                }
                                else
                                {
                                    int izl = (int)(izhilv[i] * 100);
                                    if (izl >= _item.ir.MinusL && izl <= _item.ir.MinusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else if (izl >= _item.ir.PlusL && izl <= _item.ir.PlusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else
                                        str = "NA";
                                }
                                _listDetectResult[i].Content = str;
                                //从本地数据库中拿取数据进行对比，计算出判定结果
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]).Equals("无法判定") ? "" : Convert.ToString(UnqualifiedValue[0]);
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str.Contains("%") ? str.Replace("%", string.Empty) : str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                if (_item.Hole[i].TaskName != null)
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(null);
                            }
                        }
                    }
                    else if (_sample)
                    {
                        int num = 0;
                        double[] izhilv = FgdCaculate.CaculateIzhilv(deltaA, _item.ir.RefDeltaA);
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(izhilv[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (izhilv[i] < 0)
                                    izhilv[i] = 0;
                            }
                            if (_use[i])
                            {
                                string str = "";
                                string[] UnqualifiedValue = new string[4];

                                if (!FgdCaculate.IsValid(izhilv[i]))
                                {
                                    str = "NA";
                                }
                                else
                                {
                                    int izl = (int)(izhilv[i] * 100);
                                    if (izl >= _item.ir.MinusL && izl <= _item.ir.MinusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else if (izl >= _item.ir.PlusL && izl <= _item.ir.PlusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else
                                        str = "NA";
                                }
                                _listDetectResult[i].Content = str;
                                //从本地数据库中拿取数据进行对比，计算出判定结果
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]).Equals("无法判定") ? "" : Convert.ToString(UnqualifiedValue[0]);
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str.Contains("%") ? str.Replace("%", string.Empty) : str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                if (_item.Hole[i].TaskName != null)
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(null);
                            }
                        }
                    }
                    _ContrastValue = String.Format("{0:F3}", _item.ir.RefDeltaA);
                }
                #endregion

                #region 标准曲线， 计算浓度
                else if (1 == _item.Method)
                {
                    if (_contrast)
                    {
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_use[i])
                            {
                                contrastIdx = i;
                                _item.sc.RefA = _lastATs[i].a;
                                if (FgdCaculate.IsValid(_item.sc.RefA))
                                    _listDetectResult[i].Content = String.Format("{0:F3}", _item.sc.RefA);
                                else
                                    if (i == 0)
                                    {
                                        //listDetectResult[i].Content = String.Format("{0:F3}", item.sc.RefA);
                                        _listDetectResult[i].Content = "0.000";
                                    }
                                    else
                                    {
                                        _listDetectResult[i].Content = "NA";
                                    }
                                _listUnit[i].Content = "ABS";
                                break;
                            }
                        }

                        //2015年10月29日 对照的同时进行样品测试
                        if (!FgdCaculate.IsValid(_item.sc.RefA))
                            _item.sc.RefA = 0;
                        int num = 0;
                        double[] x = FgdCaculate.HoleStatusToSCA(_lastATs, _item.sc.RefA);
                        //吸光度的计算，根据项目设置的曲线区间的不同进行不同的曲线计算
                        double[] xfix = new double[x.Length];
                        for (int i = 0; i < xfix.Length; i++)
                        {
                            //使用自动生成曲线
                            if (_item.sc.IsEnabledCalcCurve)
                            {
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.C0, _item.sc.C1, _item.sc.C2, _item.sc.C3, _dishuOrbeishuList[i],_item.sc.CCA ,_item.sc.CCB );
                            }
                            else
                            {
                                if (x[i] <= _item.sc.ATo)
                                    xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList[i], _item.sc.CCA, _item.sc.CCB);
                                else
                                    xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.B0, _item.sc.B1, _item.sc.B2, _item.sc.B3, _dishuOrbeishuList[i], _item.sc.CCA, _item.sc.CCB);
                            }
                        }

                        //double[] xfix = FgdCaculate.WeiTiaoX(x, _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList);
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(xfix[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (xfix[i] < 0)
                                    xfix[i] = 0;
                            }
                            if (_use[i] && i != contrastIdx)
                            {
                                string str;
                                string[] UnqualifiedValue = new string[4];
                                if (FgdCaculate.IsValid(x[i]))
                                    str = String.Format("{0:F3}", xfix[i]);
                                else
                                    str = "NA";
                                _listDetectResult[i].Content = str;
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                if (_item.Hole[i].TaskName != null)
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(null);
                            }
                        }
                    }
                    else if (_sample)
                    {
                        if (!FgdCaculate.IsValid(_item.sc.RefA))
                        {
                            _item.sc.RefA = 0;
                        }
                        int num = 0;
                        double[] x = FgdCaculate.HoleStatusToSCA(_lastATs, _item.sc.RefA);
                        double[] xfix = new double[x.Length];
                        for (int i = 0; i < xfix.Length; i++)
                        {
                            if (x[i] <= _item.sc.ATo)
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList[i], _item.sc.CCA, _item.sc.CCB);
                            else
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.B0, _item.sc.B1, _item.sc.B2, _item.sc.B3, _dishuOrbeishuList[i], _item.sc.CCA, _item.sc.CCB);
                        }
                        //double[] xfix = FgdCaculate.WeiTiaoX(x, _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList);
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(xfix[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (xfix[i] < 0)
                                    xfix[i] = 0;
                            }
                            if (_item.Hole[i].Use)
                            {
                                if (_use[i])
                                {
                                    string str;
                                    string[] UnqualifiedValue = new string[5];
                                    if (FgdCaculate.IsValid(x[i]))
                                        str = String.Format("{0:F3}", xfix[i]);
                                    else
                                        str = "NA";
                                    _listDetectResult[i].Content = str;
                                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                    _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                    _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                    _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                    _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                    _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                    //单位直接从样品表中拿，不在项目配置文件中拿
                                    //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                    _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                    _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                    _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                    if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                        _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                    else
                                        _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                    _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                    _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                    if (_item.Hole[i].TaskName != null)
                                        _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                    else
                                        _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                    if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                        _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                    else
                                        _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                    _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                    _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                    _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                    _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                    _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                    _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                                }
                                else
                                {
                                    num += 1;
                                    _listStrRecord.Add(null);
                                }
                            }
                            else
                            {
                                num += 1;
                            }
                        }
                    }
                    _ContrastValue = String.Format("{0:F3}", _item.sc.RefA);
                }
                #endregion

                #region 动力学法
                else if (3 == _item.Method)
                {
                    FgdCaculate.DeltaA[] deltaA = FgdCaculate.CaculateDeltaA(_firstATs, _lastATs);
                    if (_sample)
                    {
                        int num = 0;
                        double[] aPercent = FgdCaculate.CaculateChangeA(deltaA, _item.dn.DetTime);
                        double[] aPercentFix = FgdCaculate.WeiTiaoXnew(aPercent, _item.dn.CCA, _dishuOrbeishuList);
                        //原微调方法
                        //double[] aPercentFix = FgdCaculate.WeiTiaoX(aPercent, item.dn.A0, item.dn.A1, item.dn.A2, item.dn.A3);
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(aPercentFix[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (aPercentFix[i] < 0)
                                    aPercentFix[i] = 0;
                            }
                            if (_use[i])
                            {
                                string str;
                                string[] UnqualifiedValue = new string[4];
                                if (FgdCaculate.IsValid(aPercent[i]))
                                    str = String.Format("{0:F3}", aPercentFix[i]);
                                else
                                    str = "NA";
                                _listDetectResult[i].Content = str;
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                if (_item.Hole[i].TaskName != null)
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(null);
                            }
                        }
                    }
                    _ContrastValue = "NULL";
                }
                #endregion

                #region 系数法
                else if (4 == _item.Method)
                {
                    //double[] koh = FgdCaculate.CaculateKOH(_lastATs.Length, _dishuOrbeishuList, _item.co.A0, _item.co.A1, _item.co.A2, _item.co.A3);
                    double[] koh = FgdCaculate.CaculateKOH(_dishuOrbeishuList.Count, _dishuOrbeishuList, _item.co.A0, _item.co.A1, _item.co.A2, _item.co.A3);
                    if (_sample)
                    {
                        //记录当前有多少个未选择的检查项
                        int num = 0;
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(koh[i]))
                            {
                                if (koh[i] < 0)
                                    koh[i] = 0;
                            }
                            if (_use[i])
                            {
                                string str;
                                string[] UnqualifiedValue = new string[4];
                                str = String.Format("{0:F3}", koh[i]);
                                _listDetectResult[i].Content = str;
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                if (_item.Hole[i].TaskName != null)
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                                if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                                else
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = _item.Hole[i].SampleTypeCode;//样品种类编号
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.testPro;//检测项目编号
                                _CheckValue[(num > 0 ? (i - num) : i), 17] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                                _CheckValue[(num > 0 ? (i - num) : i), 18] = ""; //检测结果编号 dataNum
                                _CheckValue[(num > 0 ? (i - num) : i), 19] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 20] = _item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(null);
                            }
                        }
                    }
                    _ContrastValue = "NULL";
                }
                #endregion
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void UpdateItem()
        {
            try
            {
                // 更新对照和样品编号
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_use[i])
                    {
                        _item.SampleNum++;
                    }
                }
                new XmlSerialize().SerializeXMLToFile<List<DYFGDItemPara>>(Global.fgdItems, Global.ItemsDirectory + "\\" + "fgdItems.xml");
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        class MsgThread : ChildThread
        {
            FgdReportWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public MsgThread(FgdReportWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(1, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_PRINT:
                        if (!msg.result)
                            wnd.LabelInfo.Content = "打印失败，请检查打印端口是否正确。";
                        break;
                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                        {
                            wnd.LabelInfo.Content = "成功上传 " + Global.UploadSCount + " 条数据!";
                            wnd.IsUpLoad = true;
                            Global.IsStartUploadTimer = false;
                        }
                        else
                        {
                            if (Global.UploadSCount > 0 || Global.UploadFCount > 0)
                            {
                                wnd.LabelInfo.Content = "数据上传:成功" + Global.UploadSCount + "条；失败" + Global.UploadFCount + "条";
                                wnd.IsUpLoad = false;
                            }
                            else
                            {
                                wnd.LabelInfo.Content = "数据上传失败！";
                            }
                            if (msg.outError.Length > 0)
                            {
                                MessageBox.Show(wnd, msg.outError, "系统提示");
                            }
                            Global.IsStartUploadTimer = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void Btn_ShowDatas_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow()
            {
                ShowInTaskbar = false,
                Owner = this,
                _CheckItemName = _item.Name
            };
            window.Show();
        }

    }
}