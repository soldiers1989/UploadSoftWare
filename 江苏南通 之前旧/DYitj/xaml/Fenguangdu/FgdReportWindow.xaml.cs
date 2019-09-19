using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// FgdReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdReportWindow : Window
    {

        #region 全局变量
        public DYFGDItemPara _item = null;
        public FgdCaculate.FAT[] firstATs;
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
        /// <summary>
        /// 通道
        /// </summary>
        private static string _strchannel = "channel";
        /// <summary>
        /// 样品编号
        /// </summary>
        private static string _strsamplenum = "samplenum";
        /// <summary>
        /// 样品名称
        /// </summary>
        private static string _strsamplename = "samplename";
        private static string _strdetectresult = "detectresult";
        private static string _strJudgmentValueTemp = "JudgmentValueTemp";
        private static string _strStandardNameTemp = "StandardNameTemp";
        private static string _strStandValue = "StandValue";
        private List<Label> _listDetectResult = null;
        /// <summary>
        /// 判定结果
        /// </summary>
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
        public string _ContrastValue = string.Empty;
        private string _Null = string.Empty;
        private DispatcherTimer _DataTimer = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private int HoleCount = Global.deviceHole.HoleCount;private bool IsUpLoad = false;
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
                    this.label1.Content = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(UpdateLable):\n" + ex.Message);
            }
        }

        // 先计算，再显示
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _firstATs = new FgdCaculate.AT[HoleCount];
            for (int i = 0; i < firstATs.Length; i++)
            {
                _firstATs[i].a = firstATs[i].a;
                _firstATs[i].t = firstATs[i].t;
            }
            ButtonPrev.IsEnabled = false;
            try
            {
                if (_item == null)
                    return;
                int num = 0;
                LabelDate.Content = _date.ToString("yyyy-MM-dd HH:mm:ss");
                _listDetectResult = new List<Label>();
                _listJudmentValue = new List<Label>();
                _listStandardName = new List<Label>();
                _StandValue = new List<Label>();
                _listUnit = new List<Label>();
                _listStrRecord = new List<string>();
                _use = new bool[HoleCount];
                for (int i = 0; i < HoleCount; ++i)
                {
                    _dishuOrbeishuList.Add(_item.Hole[i].DishuOrBeishu > 0 ? _item.Hole[i].DishuOrBeishu : 1);
                    if (_item.Method == 0 || _item.Method == 3)
                    {
                        if (_item.Hole[i].Use && _item.Hole[i].IsTest)
                        {
                            _use[i] = true;
                            _CheckValue = new string[_HoleNumber, 17];
                            _HoleNumber++;
                            num++;
                        }
                        else
                            _use[i] = false;
                    }
                    else
                    {
                        if (_item.Hole[i].Use)
                        {
                            _use[i] = true;
                            _CheckValue = new string[_HoleNumber, 17];
                            _HoleNumber++;
                            num++;
                        }
                        else
                            _use[i] = false;
                    }
                }

                spTitle.Width = (num < 15) ? 880 : 920;
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < HoleCount; ++i)
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
                // 显示结果的时候，把字符串生成
                ShowResult();

                if (_DataTimer == null)
                {
                    _DataTimer = new DispatcherTimer()
                    {
                        Interval = TimeSpan.FromSeconds(1.5)
                    };
                    _DataTimer.Tick += new EventHandler(SaveAndUpload);
                    _DataTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Window_Loaded):\n" + ex.Message);
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
            if (!Global.EachDistrict.Equals("GS"))
            {
                //2015年10月29日 对照的时候对后面的数据进行上传
                Save();
                if (LoginWindow._userAccount.UpDateNowing)
                    Upload();
            }
            else if (Global.EachDistrict.Equals("GS"))
            {
                //2016年3月7日 对于没有强制上传的用户采用提示方式引导用户上传，若不上传则不保存当前检测数据
                if (LoginWindow._userAccount.UpDateNowing)
                {
                    Save();
                    Upload();
                }
                else
                {
                    if (MessageBox.Show("是否上传数据!？\n注意：若不上传数据，则此次检测的所有数据都将不做保存处理!\n请知悉!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Save();
                        Upload();
                    }
                }
            }
            UpdateLable();
            UpdateItem();
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
            _AllNumber = TestResultConserve.ResultConserve(_CheckValue, _contrast, _ContrastValue);
        }

        private PrintHelper.Report GenerateReport()
        {
            PrintHelper.Report report = new PrintHelper.Report();
            try
            {
                report.ItemName = _item.Name;
                report.ItemCategory = "分光光度";
                report.User = LoginWindow._userAccount.UserName;
                report.Unit = _item.Unit;
                report.Judgment = _item.Hole[0].SampleName;
                report.ContrastValue = _ContrastValue;
                report.Date = _date.ToString("yyyy-MM-dd HH:mm:ss");
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    if (dtList.Count > 0)
                    {
                        for (int i = dtList.Count - 1; i >= 0; i--)
                        {
                            report.SampleName.Add(dtList[i].FoodName);
                            report.SampleNum.Add(String.Format("{0:D5}", dtList[i].SampleCode));
                            report.JudgmentTemp.Add(dtList[i].Result);
                            report.Result.Add(dtList[i].CheckValueInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(GenerateReport):\n" + ex.Message);
            }
            return report;
        }


        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintHelper.Report report = GenerateReport();
                byte[] buffer = report.GeneratePrintBytes();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_PRINT,
                    str1 = Global.strPRINTPORT,
                    data = buffer,
                    arg1 = 0,
                    arg2 = buffer.Length
                };
                Global.printThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ButtonPrint_Click):\n" + ex.Message);
            }
        }

        private bool UploadCheck() 
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                this.LabelInfo.Content = "无网络连接";
                return false;
            }

            if (Global.InterfaceType.Equals("DY"))
            {
                if (Global.samplenameadapter == null || Global.samplenameadapter.Count == 0)
                {
                    if (MessageBox.Show("还未进行服务器通讯测试，可能导致数据上传失败！\r\n是否前往【设置】界面进行通讯测试？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow();
                        window.ShowDialog();
                    }
                    else
                    {
                        LabelInfo.Content = "取消上传";
                        return false;
                    }
                }
            }

            if (Wisdom.DeviceID.Length == 0 || Wisdom.USER.Length == 0 || Wisdom.PASSWORD.Length == 0)
            {
                if (MessageBox.Show("【无法上传】 - 服务器链接配置异常，是否立即前往【设置】界面进行配置？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SettingsWindow window = new SettingsWindow()
                    {
                        DeviceIdisNull = false
                    };
                    window.ShowDialog();
                }
                else
                {
                    LabelInfo.Content = "取消上传";
                    return false;
                }
            }
            return true;
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
                this.LabelInfo.Content = "正在上传...";
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Global.InterfaceType.Equals("DY"))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].userId;
                        }
                    }
                }
                else
                {
                    LabelInfo.Content = "暂无需要上传的数据";
                    return;
                }

                Message msg = new Message()
                {
                    what = MsgCode.MSG_UPLOAD,
                    obj1 = Global.samplenameadapter[0],
                    table = dt,
                };

                //if (Global.InterfaceType.Equals("ZH"))
                //{
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                //        msg.selectedRecords = dtList;
                //    }
                //}
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    msg.selectedRecords = dtList;
                }
                Global.updateThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "上传数据时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Label labelChannel = new Label()
            {
                Content = channel,
                FontSize = 20,
                Width = 80,
                Name = _strchannel,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Label labelSampleNum = new Label()
            {
                Content = sampleNum,
                FontSize = 20,
                Width = 100,
                Name = _strsamplenum,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Label labelSampleName = new Label()
            {
                Content = sampleName,
                FontSize = 20,
                Width = 200,
                Name = _strsamplename,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Label labelDetectResult = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100,
                Name = _strdetectresult,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Label labelUnit = new Label()
            {
                Content = unit,
                FontSize = 20,
                Width = 100,
                Name = "labelUnit",
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Label labeJudgemtValue = new Label()
            {
                Content = JudgmentValue,
                FontSize = 20,
                Width = 100,
                Name = _strJudgmentValueTemp,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            //判定标准
            Label labeStandardName = new Label()
            {
                Content = StandardName,
                FontSize = 20,
                Width = 100,
                Name = _strStandardNameTemp,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            //标准值
            Label labeStandardValue = new Label()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = StandValue,
                FontSize = 20,
                Width = 100,
                Name = _strStandValue
            };
            labeStandardValue.HorizontalContentAlignment = HorizontalAlignment.Center;

            stackPanel.Children.Add(labelChannel);
            stackPanel.Children.Add(labelSampleNum);
            stackPanel.Children.Add(labelSampleName);
            stackPanel.Children.Add(labelDetectResult);
            stackPanel.Children.Add(labelUnit);
            stackPanel.Children.Add(labeJudgemtValue);
            stackPanel.Children.Add(labeStandardName);
            stackPanel.Children.Add(labeStandardValue);
            return stackPanel;
        }


        private string[] GetResult(string[] Qualifide, string strValue)
        {
            //if (!Qualifide[0].Equals("参考国标")) return Qualifide;
            double value = Global.StringConvertDouble(strValue.TrimEnd('%'));
            if (0 == _item.Method)
            {
                if (Qualifide[0].Equals("参考国标"))
                {
                    if (value <= _item.ir.MinusH)
                    {
                        Qualifide[0] = "合格";
                    }
                    else if (true)
                    {
                        Qualifide[0] = "不合格";
                    }
                }
                Qualifide[1] = Qualifide[1].Length == 0 ? "GB/T 5009.199-2003" : Qualifide[1];
                Qualifide[2] = Qualifide[2].Length == 0 ? _item.ir.MinusH.ToString("F2") : Qualifide[2];
                Qualifide[3] = "≤";
                Qualifide[4] = "%";
            }
            return Qualifide;
        }

        private void ShowResult()
        {
            try
            {
                int sampleNum = _item.SampleNum, contrastIdx = 0;
                if (_contrast)
                    sampleNum += 1;
                string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (0 == _item.Method)
                {
                    // 农残，计算deltaA
                    FgdCaculate.DeltaA[] deltaA = FgdCaculate.CaculateDeltaA(_firstATs, _lastATs, _contrast);
                    if (_contrast)
                    {
                        for (int i = 0; i < HoleCount; ++i)
                        {
                            if (_use[i])
                            {
                                contrastIdx = i;
                                // 1、deltaA > 0 有效，否则直接判定对照为NA，若同时有样品测试，也判定为NA，不保存不打印；
                                _item.ir.RefDeltaA = deltaA[i].deltaA;
                                if (_item.ir.RefDeltaA <= 0 || (!FgdCaculate.IsValid(_item.ir.RefDeltaA)))
                                {
                                    _listDetectResult[i].Content = "NA";
                                    _item.ir.RefDeltaA = FgdCaculate._VALUE_INVALID;
                                }
                                else
                                {
                                    _listDetectResult[i].Content = String.Format("{0:F3}", _item.ir.RefDeltaA);
                                }
                                _listUnit[i].Content = "ABS";
                                _listDetectResult[i].ToolTip = _listDetectResult[i];
                                break;
                            }
                        }

                        //2015年10月29日 对照的同时进行样品测试
                        int num = 0;
                        double[] izhilv = FgdCaculate.CaculateIzhilv(deltaA, _item.ir.RefDeltaA);
                        for (int i = 0; i < HoleCount; ++i)
                        {
                            if (_use[i] && i != contrastIdx)
                            {
                                string str = string.Empty, strCheckValue = string.Empty;
                                string[] UnqualifiedValue = new string[4];

                                if (!FgdCaculate.IsValid(izhilv[i]))
                                    str = "NA";
                                else
                                {
                                    int izl = (int)(izhilv[i] * 100);
                                    if (izl >= _item.ir.MinusL && izl <= _item.ir.MinusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else if (izl >= _item.ir.PlusL && izl <= _item.ir.PlusH)
                                        str = String.Format("{0:P1}", izhilv[i]);
                                    else
                                        str = "NA";
                                    strCheckValue = izhilv[i].ToString();
                                }
                                //从本地数据库中拿取数据进行对比，计算出判定结果
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                UnqualifiedValue = GetResult(UnqualifiedValue, str);
                                if (str == "NA")
                                {
                                    UnqualifiedValue[0] = string.Empty;
                                    UnqualifiedValue[1] = UnqualifiedValue[1].Length == 0 ? "GB/T 5009.199-2003" : UnqualifiedValue[1];
                                    UnqualifiedValue[2] = string.Empty;
                                    UnqualifiedValue[3] = "≤";
                                    UnqualifiedValue[4] = "%";
                                    strCheckValue = str;
                                }
                                _listDetectResult[i].Content = str;
                                _listDetectResult[i].ToolTip = str;
                                _listJudmentValue[i].Content = UnqualifiedValue[0];
                                _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                {
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                }
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _StandValue[i].ToolTip = _StandValue[i].Content;

                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = strCheckValue;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample;//_item.Hole[i].ProduceCompany;
                                //_CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].SAMPLEUUID;

                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(_Null);
                            }
                        }
                    }
                    else if (_sample)
                    {
                        int num = 0;
                        double[] izhilv = FgdCaculate.CaculateIzhilv(deltaA, _item.ir.RefDeltaA);
                        for (int i = 0; i < HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(izhilv[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (izhilv[i] < 0)
                                    izhilv[i] = 0;
                            }
                            if (_use[i])
                            {
                                string str = string.Empty, strCheckValue = string.Empty;
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
                                    strCheckValue = izhilv[i].ToString();
                                }
                                //从本地数据库中拿取数据进行对比，计算出判定结果
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                UnqualifiedValue = GetResult(UnqualifiedValue, str);
                                _listDetectResult[i].Content = str;
                                _listDetectResult[i].ToolTip = str;
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]).Equals("无法判定") ? string.Empty : Convert.ToString(UnqualifiedValue[0]);
                                _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                {
                                    //Brushes.Red
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                }
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _StandValue[i].ToolTip = _StandValue[i].Content;

                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = strCheckValue;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample;//_item.Hole[i].ProduceCompany;
                                //_CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].SAMPLEUUID;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(_Null);
                            }
                        }
                    }
                    _ContrastValue = String.Format("{0:F3}", _item.ir.RefDeltaA);
                }
                else if (1 == _item.Method)
                {
                    // 标准曲线， 计算浓度
                    if (_contrast)
                    {
                        for (int i = 0; i < HoleCount; ++i)
                        {
                            if (_use[i])
                            {
                                contrastIdx = i;
                                _item.sc.RefA = _lastATs[i].a;
                                if (FgdCaculate.IsValid(_item.sc.RefA))
                                    _listDetectResult[i].Content = String.Format("{0:F3}", _item.sc.RefA);
                                else
                                    if (i == 0) _listDetectResult[i].Content = "0.000";
                                    else _listDetectResult[i].Content = "NA";
                                _listUnit[i].Content = "ABS";
                                _listUnit[i].ToolTip = _listUnit[i].Content;
                                _listDetectResult[i].ToolTip = _listDetectResult[i];
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
                            if (x[i] <= _item.sc.ATo)
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList[i]);
                            else
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.B0, _item.sc.B1, _item.sc.B2, _item.sc.B3, _dishuOrbeishuList[i]);
                        }

                        //double[] xfix = FgdCaculate.WeiTiaoX(x, _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList);
                        for (int i = 0; i < HoleCount; ++i)
                        {
                            if (FgdCaculate.IsValid(xfix[i]))
                            {
                                // 合法，但是是负值，这样的话要归零。
                                if (xfix[i] < 0)
                                    xfix[i] = 0;
                            }
                            if (_item.Hole[i].Use)
                            {
                                if (_use[i] && i != contrastIdx)
                                {
                                    string str;
                                    string[] UnqualifiedValue = new string[4];
                                    if (FgdCaculate.IsValid(x[i]))
                                        str = String.Format("{0:F3}", xfix[i]);
                                    else
                                        str = "NA";

                                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                    //UnqualifiedValue = getResult(UnqualifiedValue, str);
                                    _listDetectResult[i].Content = str;
                                    _listDetectResult[i].ToolTip = str;
                                    _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                    _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                    if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                    {
                                        //Brushes.Red
                                        _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                    }
                                    _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                    _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                    _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                    _StandValue[i].ToolTip = _StandValue[i].Content;

                                    _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                    _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                    _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                    //单位直接从样品表中拿，不在项目配置文件中拿
                                    //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                    _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                    _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                    _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                    _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                    _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample;//_item.Hole[i].ProduceCompany;
                                    //_CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].SAMPLEUUID;
                                }
                                else
                                {
                                    num += 1;
                                    _listStrRecord.Add(_Null);
                                }
                            }
                            else
                            {
                                num += 1;
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
                        //吸光度的计算，根据项目设置的曲线区间的不同进行不同的曲线计算
                        double[] xfix = new double[x.Length];
                        for (int i = 0; i < xfix.Length; i++)
                        {
                            if (x[i] <= _item.sc.ATo)
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList[i]);
                            else
                                xfix[i] = FgdCaculate.WeiTiaoNew(x[i], _item.sc.B0, _item.sc.B1, _item.sc.B2, _item.sc.B3, _dishuOrbeishuList[i]);
                        }

                        //double[] xfix = FgdCaculate.WeiTiaoX(x, _item.sc.A0, _item.sc.A1, _item.sc.A2, _item.sc.A3, _dishuOrbeishuList);
                        for (int i = 0; i < HoleCount; ++i)
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
                                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                    _listDetectResult[i].Content = str;
                                    _listDetectResult[i].ToolTip = str;
                                    _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                    _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                    if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                    {
                                        //Brushes.Red
                                        _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                    }
                                    _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                    _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                    _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                    _StandValue[i].ToolTip = _StandValue[i].Content;

                                    _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                    _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                    _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                    //单位直接从样品表中拿，不在项目配置文件中拿
                                    //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                    _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                    _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                    _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                    _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                    _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                    _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                    _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                    _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample; //_item.Hole[i].ProduceCompany;
                                    //_CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].SAMPLEUUID;
                                }
                                else
                                {
                                    num += 1;
                                    _listStrRecord.Add(_Null);
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
                else if (3 == _item.Method)
                {
                    //动力学法
                    FgdCaculate.DeltaA[] deltaA = FgdCaculate.CaculateDeltaA(_firstATs, _lastATs, _contrast);
                    if (_sample)
                    {
                        int num = 0;
                        double[] aPercent = FgdCaculate.CaculateChangeA(deltaA, _item.dn.DetTime);
                        double[] aPercentFix = FgdCaculate.WeiTiaoXnew(aPercent, _item.dn.CCA, _dishuOrbeishuList);
                        //原微调方法
                        //double[] aPercentFix = FgdCaculate.WeiTiaoX(aPercent, item.dn.A0, item.dn.A1, item.dn.A2, item.dn.A3);
                        for (int i = 0; i < HoleCount; ++i)
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
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listDetectResult[i].Content = str;
                                _listDetectResult[i].ToolTip = _listDetectResult[i];
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                {
                                    //Brushes.Red
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                }
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _StandValue[i].ToolTip = _StandValue[i].Content;

                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample;// _item.Hole[i].ProduceCompany;
                                //_CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].SAMPLEUUID;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(_Null);
                            }
                        }
                    }
                    _ContrastValue = "NULL";
                }
                else if (4 == _item.Method)
                {
                    //系数法
                    //double[] koh = FgdCaculate.CaculateKOH(_lastATs.Length, _dishuOrbeishuList, _item.co.A0, _item.co.A1, _item.co.A2, _item.co.A3);
                    double[] koh = FgdCaculate.CaculateKOH(_dishuOrbeishuList.Count, _dishuOrbeishuList, _item.co.A0, _item.co.A1, _item.co.A2, _item.co.A3);
                    if (_sample)
                    {
                        //记录当前有多少个未选择的检查项
                        int num = 0;
                        for (int i = 0; i < HoleCount; ++i)
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
                                _listDetectResult[i].ToolTip = str;
                                UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                                _listJudmentValue[i].Content = Convert.ToString(UnqualifiedValue[0]);
                                _listJudmentValue[i].ToolTip = _listJudmentValue[i].Content;
                                if (!_listJudmentValue[i].Content.ToString().Equals("合格"))
                                {
                                    //Brushes.Red
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                                }
                                _listStandardName[i].Content = Convert.ToString(UnqualifiedValue[1]);
                                _listStandardName[i].ToolTip = _listStandardName[i].Content;
                                _StandValue[i].Content = Convert.ToString(UnqualifiedValue[3]) + Convert.ToString(UnqualifiedValue[2] + UnqualifiedValue[4]);
                                _StandValue[i].ToolTip = _StandValue[i].Content;

                                _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                                _CheckValue[(num > 0 ? (i - num) : i), 1] = "分光光度";
                                _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                                _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                                _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                                //单位直接从样品表中拿，不在项目配置文件中拿
                                //_CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                                _CheckValue[(num > 0 ? (i - num) : i), 5] = UnqualifiedValue[4].Equals(string.Empty) ? _item.Unit : UnqualifiedValue[4];
                                _CheckValue[(num > 0 ? (i - num) : i), 6] = NowDateTime;
                                _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                                _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                                _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                                _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                                _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                                _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                                _CheckValue[(num > 0 ? (i - num) : i), 13] = string.IsNullOrEmpty(_item.Hole[i].TaskName) ? string.Empty : _item.Hole[i].TaskCode;
                                _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                                _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                                _CheckValue[(num > 0 ? (i - num) : i), 16] = Global.NTsample;//_item.Hole[i].ProduceCompany;
                            }
                            else
                            {
                                num += 1;
                                _listStrRecord.Add(_Null);
                            }
                        }
                    }
                    _ContrastValue = "NULL";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ShowResult):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 更新对照和样品编号
        /// </summary>
        private void UpdateItem()
        {
            try
            {
                for (int i = 0; i < HoleCount; ++i)
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
                MessageBox.Show("异常(UpdateItem):\n" + ex.Message);
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 查看当前检测项目检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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