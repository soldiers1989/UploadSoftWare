using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Text;

namespace AIO
{
    /// <summary>
    /// GszReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszReportWindow : Window
    {

        #region 全局变量
        /// <summary>
        /// 胶体金新摄像头模块数据
        /// </summary>
        public List<byte[]>[] _newJtjDatas = null;

        /// <summary>
        /// 接收到的硬件数据
        /// </summary>
        public List<byte[]> _datas;
        /// <summary>
        /// 曲线数据
        /// </summary>
        private static List<double[]> _curveDatas;
        /// <summary>
        /// 存储曲线数据
        /// </summary>
        private List<string> curveDatas = new List<string>();
        private List<string> imgDatas = new List<string>();
        private List<double> _dataCT = null;
        public DYGSZItemPara _item = null;
        public static List<string> _listDetectResult = null;
        public static List<string> _listStrRecord;
        private string[] _methodToString = { "定性法", "定量法" };
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        MsgThread _msgThread;
        private string[,] _CheckValue;
        private int _HoleNumber = 1;
        int _AllNumber = 0;
        public List<TextBox> _RecordValue = null;
        private DispatcherTimer _DataTimer = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private bool IsUpLoad = false;
        private List<ChartPlotter> _plotters = null;
        private List<HorizontalDateTimeAxis> _dateAxis = null;
        private string logType = "GszReportWindow-error";
        private List<TextBox> _listJudmentValue = null;
        private List<TextBox> _listCheckResult = null;
        private List<TextBox> _listResult = null;
        private String _textBoxDetectResult = "textBoxDetectResult";
        private String _textJugmentResult = "textJugmentResult";
        /// <summary>
        /// 存储新模块的CT值
        /// </summary>
        private List<double[]> _ctValues = null;

        #endregion

        public GszReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;
            try
            {
                _listDetectResult = new List<String>();
                _listStrRecord = new List<String>();
                _listJudmentValue = new List<TextBox>();
                _listCheckResult = new List<TextBox>();
                _listResult = new List<TextBox>();
                _plotters = new List<ChartPlotter>();
                _dateAxis = new List<HorizontalDateTimeAxis>();
                int sampleNum = _item.SampleNum;
                WrapPanelChannel.Width = 0;
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        _plotters.Add(UIUtils.GetChildObject<ChartPlotter>(element, "chartPlotter"));
                        _dateAxis.Add(UIUtils.GetChildObject<HorizontalDateTimeAxis>(element, "dateAxis"));
                        _listCheckResult.Add(UIUtils.GetChildObject<TextBox>(element, _textBoxDetectResult));
                        _listResult.Add(UIUtils.GetChildObject<TextBox>(element, _textJugmentResult));
                        WrapPanelChannel.Width += 450;
                        sampleNum++;
                        _CheckValue = new String[_HoleNumber, 22];
                        _HoleNumber++;
                    }
                    else
                    {
                        _listCheckResult.Add(null);
                        _listResult.Add(null);
                        _plotters.Add(new ChartPlotter());
                        _dateAxis.Add(new HorizontalDateTimeAxis());
                    }
                }

                _plotters.Add(new ChartPlotter());
                _dateAxis.Add(new HorizontalDateTimeAxis());

                //格式化数据
                if (Global.JtjVersion == 3 || Global.Is3500I )
                {
                    NewFormattingDatas();
                }
                else
                {
                    FormattingDatas();
                }
                //绘制曲线并定位C.T值
                DrawingCurve();
                //曲线计算
                CalcCurve();
                //计算结果
                CalcResult();
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
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void CalcResult()
        {
            int num = 0;
            try
            {
                int sampleNum = _item.SampleNum;
                string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        string[] UnqualifiedValue = new string[4];
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);
                        if (_item.Method == 0)
                        {
                            double ctAbs = 0;
                            if (Double.MinValue != _item.dx.DeltaA[i])
                            {
                                ctAbs = Math.Log(_dataCT[i] / _item.dx.DeltaA[i]);
                                if (ctAbs > _item.dx.Max[i])
                                {
                                    _listCheckResult[i].Text = "阴性" + (Global.IsShowValue ? string.Format("(V={0},L={1})", _dataCT[i].ToString("F3"), ctAbs.ToString("F3")) : "");
                                    _listResult[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else if (ctAbs < _item.dx.Min[i])
                                {
                                    _listCheckResult[i].Text = "阳性" + (Global.IsShowValue ? string.Format("(V={0},L={1})", _dataCT[i].ToString("F2"), ctAbs.ToString("F3")) : "");
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "不合格";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "不合格";
                                }
                                else
                                {
                                    _listCheckResult[i].Text = "可疑" + (Global.IsShowValue ? string.Format("(V={0},L={1})", _dataCT[i].ToString("F2"), ctAbs.ToString("F3")) : "");
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "可疑";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "可疑";
                                }
                            }
                            else
                            {
                                _item.dx.DeltaA[i] = _dataCT[i] > 0 ? _dataCT[i] : double.MinValue;
                                _listCheckResult[i].Text = (_dataCT[i] > 0 ? "Abs" : "无效对照") + (Global.IsShowValue ? string.Format("(V={0},L={1})", _dataCT[i].ToString("F2"), ctAbs.ToString("F3")) : "");
                                _listCheckResult[i].FontWeight = FontWeights.Bold;
                                _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                _listResult[i].Text = _dataCT[i] > 0 ? "Abs" : "无效对照"; ;
                                _listResult[i].FontWeight = FontWeights.Bold;
                                _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                UnqualifiedValue[0] = "Abs";
                            }
                        }
                        UnqualifiedValue[2] = _item.dx.Max[i].ToString();
                        int number = num > 0 ? (i - num) : i;
                        _CheckValue[number, 0] = String.Format("{0:D2}", (i + 1));
                        _CheckValue[number, 1] = "干化学";
                        _CheckValue[number, 2] = _item.Name;
                        _CheckValue[number, 3] = _methodToString[_item.Method];
                        _CheckValue[number, 4] = _listCheckResult[i].Text;
                        _CheckValue[number, 5] = _item.Unit.Length == 0 ? UnqualifiedValue[4] : _item.Unit;
                        _CheckValue[number, 6] = NowDateTime;
                        _CheckValue[number, 7] = LoginWindow._userAccount.UserName;
                        if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                            _CheckValue[number, 8] = _item.Hole[i].SampleName;
                        else
                            _CheckValue[number, 8] = string.Empty;

                        _CheckValue[number, 9] = Convert.ToString(UnqualifiedValue[0]);
                        _CheckValue[number, 10] = Convert.ToString(UnqualifiedValue[2]);
                        _CheckValue[number, 11] = String.Format("{0:D5}", sampleNum++);
                        _CheckValue[number, 12] = Convert.ToString(UnqualifiedValue[1]);
                        if (_item.Hole[i].TaskName != null)
                            _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskCode;//_item.Hole[i].TaskName;
                        else
                            _CheckValue[(num > 0 ? (i - num) : i), 13] = string.Empty;
                        if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                            _CheckValue[number, 14] = _item.Hole[i].CompanyName;
                        else
                            _CheckValue[number, 14] = string.Empty;
                        _CheckValue[number, 15] = curveDatas[i];
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].SampleTypeCode;//样品种类编号
                        _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.testPro;//检测项目编号
                        _CheckValue[(num > 0 ? (i - num) : i), 18] = "2";//检测结果类型 1，定量，2定性 分光光度都为1
                        _CheckValue[(num > 0 ? (i - num) : i), 19] = string.Empty; //检测结果编号 dataNum
                        _CheckValue[(num > 0 ? (i - num) : i), 20] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                        _CheckValue[(num > 0 ? (i - num) : i), 21] = _item.Hole[i].ProduceCompany;//生产单位
                    }
                    else
                    {
                        num += 1;
                        _listStrRecord.Add(null);
                        _listDetectResult.Add(null);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
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
            Save();
            if (LoginWindow._userAccount.UpDateNowing)
                Upload();
            else Global.IsStartUploadTimer = true;
            ButtonPrint.IsEnabled = true;
            btn_upload.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateItem();
            _msgThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        string[] syscodes = null;
        private void Save()
        {
            if (Global.InterfaceType.Equals("AH"))
            {
                _AllNumber = TestResultConserve.ResultConserveAH(_CheckValue, out syscodes);
            }
            else
            {
                _AllNumber = TestResultConserve.ResultConserve(_CheckValue, out syscodes);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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
                                ItemCategory = models[0].ResultType,
                                User = LoginWindow._userAccount.UserName,
                                Unit = models[0].ResultInfo,
                                Judgment = models[0].FoodName,
                                Date = models[0].CheckStartDate,
                                Company = models[0].CheckedCompany
                            };

                            if(Global.Is3500I)
                            {
                                for (int i = models.Count-1; i >=0; i--)
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
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private UIElement GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {

            Border border = new Border
            {
                Width = 445,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "border"
            };

            StackPanel stackPanel = new StackPanel
            {
                Width = 445,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "stackPanel"
            };

            Grid grid = new Grid
            {
                Width = 445,
                Height = 40,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

            Label label = new Label
            {
                FontWeight = FontWeights.Bold,
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = " 检测通道 " + (channel + 1)
            };

            Canvas canvas = new Canvas
            {
                Width = 440,
                Height = 220,
                Background = Brushes.Gray,
                Name = "canvas",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

            ChartPlotter plotter = new ChartPlotter
            {
                Width = 440,
                Height = 220,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "chartPlotter"
            };
            plotter.MouseDoubleClick += new MouseButtonEventHandler(plotter_MouseDoubleClick);

            HorizontalAxis horizontalAxis = new HorizontalAxis
            {
                Name = "horizontalAxis"
            };

            HorizontalDateTimeAxis dateAxis = new HorizontalDateTimeAxis
            {
                Name = "dateAxis"
            };

            VerticalAxis verticalAxis = new VerticalAxis
            {
                Name = "verticalAxis"
            };

            VerticalIntegerAxis countAxis = new VerticalIntegerAxis
            {
                Name = "countAxis"
            };

            VerticalAxisTitle arialy = new VerticalAxisTitle
            {
                Content = "y"
            };

            HorizontalAxisTitle arialx = new HorizontalAxisTitle
            {
                Content = "x"
            };

            canvas.Children.Add(plotter);
            canvas.Children.Add(dateAxis);
            canvas.Children.Add(verticalAxis);
            canvas.Children.Add(countAxis);
            canvas.Children.Add(arialy);
            canvas.Children.Add(arialx);

            WrapPanel wrapPanel = new WrapPanel
            {
                Width = 445,
                Height = 180
            };

            Canvas rootCanvas = new Canvas()
            {
                Width = 240,
                Height = 180,
                Background = Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Visibility = Global.JtjVersion == 3 ? Visibility.Visible : Visibility.Collapsed,
                Name = "rootCanvas",
                Tag = channel
            };
            //只有胶体金3.0版本才显示图像
            rootCanvas.Visibility = Global.JtjVersion == 3 ? Visibility.Visible : Visibility.Collapsed;

            Rectangle rect = new Rectangle()
            {
                Width = Global.nHelpBoxWidth,
                Height = Global.nHelpBoxHeight,
                StrokeThickness = Global.nHelpBoxLineWidth,
                Stroke = Brushes.Red,
                Visibility = Global.JtjVersion == 3 ? Visibility.Visible : Visibility.Collapsed,
                Name = "rect",
                Tag = channel
            };
            if (channel == 0)
            {
                rect.SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft1);
                rect.SetValue(Canvas.TopProperty, Global.nHelpBoxTop1);
            }
            else if (channel == 1)
            {
                rect.SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft2);
                rect.SetValue(Canvas.TopProperty, Global.nHelpBoxTop2);
            }
            else if (channel == 2)
            {
                rect.SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft3);
                rect.SetValue(Canvas.TopProperty, Global.nHelpBoxTop3);
            }
            else if (channel == 3)
            {
                rect.SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft4);
                rect.SetValue(Canvas.TopProperty, Global.nHelpBoxTop4);
            }

            rect.MouseLeftButtonDown += Handle_MouseDown;
            rect.MouseMove += Handle_MouseMove;
            rect.MouseLeftButtonUp += Handle_MouseUp;
            rootCanvas.Children.Add(rect);
            wrapPanel.Children.Add(rootCanvas);

            StackPanel spContent = new StackPanel
            {
                Width = Global.JtjVersion == 3 ? 200 : 445,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "spContent"
            };

            WrapPanel wrapPannelSample = new WrapPanel
            {
                Width = Global.JtjVersion == 3 ? 200 : 445,
                Height = Global.JtjVersion == 3 ? 45 : 30
            };

            Label labelSampleName = new Label
            {
                Width = Global.JtjVersion == 3 ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleName = new TextBox
            {
                Width = Global.JtjVersion == 3 ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            wrapPannelSample.Children.Add(labelSampleName);
            wrapPannelSample.Children.Add(textBoxSampleName);
            spContent.Children.Add(wrapPannelSample);

            WrapPanel wrapPannelSampleNum = new WrapPanel
            {
                Width = Global.JtjVersion == 3 ? 200 : 445,
                Height = Global.JtjVersion == 3 ? 45 : 30
            };

            Label labelSampleNum = new Label
            {
                Width = Global.JtjVersion == 3 ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品编号:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleNum = new TextBox
            {
                Width = Global.JtjVersion == 3 ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            spContent.Children.Add(wrapPannelSampleNum);

            WrapPanel wrapPannelDetectResult = new WrapPanel
            {
                Width = Global.JtjVersion == 3 ? 200 : 445,
                Height = Global.JtjVersion == 3 ? 45 : 30
            };

            Label labelDetectResult = new Label
            {
                Width = Global.JtjVersion == 3 ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxDetectResult = new TextBox
            {
                Width = Global.JtjVersion == 3 ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxDetectResult"
            };
            wrapPannelDetectResult.Children.Add(labelDetectResult);
            wrapPannelDetectResult.Children.Add(textBoxDetectResult);
            spContent.Children.Add(wrapPannelDetectResult);

            WrapPanel wrapPannelJudgment = new WrapPanel
            {
                Width = Global.JtjVersion == 3 ? 200 : 445,
                Height = Global.JtjVersion == 3 ? 45 : 30
            };

            Label labelJudgment = new Label
            {
                Width = Global.JtjVersion == 3 ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "判定结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textJugmentResult = new TextBox
            {
                Width = Global.JtjVersion == 3 ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty,
                IsReadOnly = true,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textJugmentResult"
            };
            wrapPannelJudgment.Children.Add(labelJudgment);
            wrapPannelJudgment.Children.Add(textJugmentResult);
            spContent.Children.Add(wrapPannelJudgment);
            wrapPanel.Children.Add(spContent);

            grid.Children.Add(label);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(canvas);
            stackPanel.Children.Add(wrapPanel);
            //stackPanel.Children.Add(wrapPannelSample);
            //stackPanel.Children.Add(wrapPannelSampleNum);
            //stackPanel.Children.Add(wrapPannelDetectResult);
            //stackPanel.Children.Add(wrapPannelJudgment);

            border.Child = stackPanel;
            return border;
        }

        bool isMouseCaptured;
        double mouseVerticalPosition;
        double mouseHorizontalPosition;
        private void Handle_MouseDown(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");

            if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                mouseVerticalPosition = args.GetPosition(null).Y;
                mouseHorizontalPosition = args.GetPosition(null).X;
                isMouseCaptured = true;
                item.CaptureMouse();
            }
        }

        int helpLineLeft = 0, helpLineTop = 0;
        private void Handle_MouseMove(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
            Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");

            if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                if (isMouseCaptured)
                {
                    double deltaV = args.GetPosition(null).Y - mouseVerticalPosition;
                    double deltaH = args.GetPosition(null).X - mouseHorizontalPosition;
                    double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                    double newLeft = deltaH + (double)item.GetValue(Canvas.LeftProperty);
                    double rectTop = (double)rect.GetValue(Canvas.TopProperty);

                    if (newTop < 0)
                        newTop = 0;
                    if (newLeft < 0)
                        newLeft = 0;
                    if (newTop + item.Height > rootCanvas.Height || newTop + item.Height == rootCanvas.Height)
                        newTop = rootCanvas.Height - item.Height - 10;
                    if (newLeft + item.Width > rootCanvas.Width)
                        newLeft = rootCanvas.Width - item.Width;
                    item.SetValue(Canvas.TopProperty, newTop);
                    item.SetValue(Canvas.LeftProperty, newLeft);
                    mouseVerticalPosition = args.GetPosition(null).Y;
                    mouseHorizontalPosition = args.GetPosition(null).X;
                    helpLineLeft = (int)newLeft;
                    helpLineTop = (int)newTop;
                    if (rect.Tag.ToString().Equals("0"))
                    {
                        Global.nHelpBoxLeft1 = newLeft;
                        Global.nHelpBoxTop1 = newTop;
                    }
                    else if (rect.Tag.ToString().Equals("1"))
                    {
                        Global.nHelpBoxLeft2 = newLeft;
                        Global.nHelpBoxTop2 = newTop;
                    }
                    else if (rect.Tag.ToString().Equals("2"))
                    {
                        Global.nHelpBoxLeft3 = newLeft;
                        Global.nHelpBoxTop3 = newTop;
                    }
                    else if (rect.Tag.ToString().Equals("3"))
                    {
                        Global.nHelpBoxLeft4 = newLeft;
                        Global.nHelpBoxTop4 = newTop;
                    }
                }
            }
        }

        private void Handle_MouseUp(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");

            if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                isMouseCaptured = false;
                item.ReleaseMouseCapture();
                mouseVerticalPosition = -1;
                mouseHorizontalPosition = -1;
                if (rect.Tag.ToString().Equals("0"))
                {
                    CFGUtils.SaveConfig("nHelpBoxLeft1", Global.nHelpBoxLeft1.ToString());
                    CFGUtils.SaveConfig("nHelpBoxTop1", Global.nHelpBoxTop1.ToString());
                }
                else if (rect.Tag.ToString().Equals("1"))
                {
                    CFGUtils.SaveConfig("nHelpBoxLeft2", Global.nHelpBoxLeft2.ToString());
                    CFGUtils.SaveConfig("nHelpBoxTop2", Global.nHelpBoxTop2.ToString());
                }
                else if (rect.Tag.ToString().Equals("2"))
                {
                    CFGUtils.SaveConfig("nHelpBoxLeft3", Global.nHelpBoxLeft3.ToString());
                    CFGUtils.SaveConfig("nHelpBoxTop3", Global.nHelpBoxTop3.ToString());
                }
                else if (rect.Tag.ToString().Equals("3"))
                {
                    CFGUtils.SaveConfig("nHelpBoxLeft4", Global.nHelpBoxLeft4.ToString());
                    CFGUtils.SaveConfig("nHelpBoxTop4", Global.nHelpBoxTop4.ToString());
                }
                //如果移动了CT线的取色框，则显示重新检测按钮
                //BtnToDetect.Visibility = Visibility.Visible;
                BtnToDetect_Click(null, null);
            }
        }

        #region 曲线
        internal class BugInfo
        {
            public DateTime date;
            public int numberOpen;
            public int numberClosed;

            public BugInfo(DateTime date, int numberOpen, int numberClosed)
            {
                this.date = date;
                this.numberOpen = numberOpen;
                this.numberClosed = numberClosed;
            }
        }

        private static List<BugInfo> LoadBugInfo(string fileName)
        {
            var result = new List<BugInfo>();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] pieces = line.Split(':');
                DateTime d = DateTime.Parse(pieces[0]);
                int numopen = int.Parse(pieces[1]);
                int numclosed = int.Parse(pieces[2]);
                BugInfo bi = new BugInfo(d, numopen, numclosed);
                result.Add(bi);
            }
            sr.Close();
            fs.Close();
            return result;
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }
        #endregion

        private void FormattingDatas()
        {
            try
            {
                _curveDatas = new List<double[]>();
                for (int z = 0; z < _datas.Count; z++)
                {
                    byte[] dt = _datas[z];
                    if (dt == null)
                    {
                        _curveDatas.Add(null);
                        continue;
                    }

                    int len = dt.Length / 2, index = 0;
                    double[] data = new double[len];

                    string strdt = string.Empty;
                    for (int i = 0; i < len; i++)
                    {
                        data[i] = dt[index + 1] * 256 + dt[index];
                        strdt += strdt.Length == 0 ? data[i].ToString() : "|" + data[i];
                        index += 2;
                    }
                    double[] db = null;
                    //新模块不截取数据，直接使用原始曲线数据进行计算
                    if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20)
                    {
                        db = new double[60];
                        Array.Copy(data, (data.Length - 60) / 2, db, 0, db.Length);
                        _curveDatas.Add(db);
                        continue;
                    }

                    //旧模块返回曲线数据
                    db = new double[40];
                    Array.Copy(data, (data.Length - 40) / 2, db, 0, db.Length);
                    _curveDatas.Add(db);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "胶体金格式化数据", ex.ToString());
                MessageBox.Show("胶体金格式化数据时出现异常！\r\n" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private void DrawingCurve()
        {
            if (_curveDatas == null) return;

            string curveData = string.Empty;
            for (int i = 0; i < _curveDatas.Count; i++)
            {
                curveData = string.Empty;
                if (_curveDatas[i] == null)
                {
                    curveDatas.Add(string.Empty);
                    continue;
                }

                double[] data = _curveDatas[i];
                try
                {
                    data = _curveDatas[i];
                    int len = data.Length;
                    DateTime[] dates = new DateTime[len];
                    double[] numberOpen = new double[len];
                    for (int j = 0; j < len; ++j)
                    {
                        dates[j] = Convert.ToDateTime("01/01/000" + (j + 1));
                        numberOpen[j] = data[j];
                        curveData += curveData.Length == 0 ? data[j].ToString() : string.Format("|{0}", data[j]);
                    }
                    curveDatas.Add(curveData);
                    var datesDataSource = new EnumerableDataSource<DateTime>(dates);
                    datesDataSource.SetXMapping(x => _dateAxis[i].ConvertToDouble(x));
                    //datesDataSource.SetXMapping(x => _dateAxis[i < 2 ? i : 2].ConvertToDouble(x));
                    var numberOpenDataSource = new EnumerableDataSource<double>(numberOpen);
                    numberOpenDataSource.SetYMapping(y => y);
                    CompositeDataSource compositeDataSource = new CompositeDataSource(datesDataSource, numberOpenDataSource);
                    _plotters[i].AddLineGraph(compositeDataSource,
                                        new Pen(Brushes.Blue, 1),
                                        new CirclePointMarker { Size = 3, Fill = Brushes.Red },
                                        null);
                    _plotters[i].Viewport.FitToView();
                    //_plotters[i < 2 ? i : 2].AddLineGraph(compositeDataSource,
                    //                    new Pen(Brushes.Blue, 1),
                    //                    new CirclePointMarker { Size = 3, Fill = Brushes.Red },
                    //                    null);
                    //_plotters[i < 2 ? i : 2].Viewport.FitToView();
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(2, logType, ex.ToString());
                }
            }
        }

        /// <summary>
        /// 曲线计算
        /// </summary>
        private void CalcCurve()
        {
            if (_curveDatas == null) return;
            _dataCT = new List<double>();
            for (int i = 0; i < _curveDatas.Count; i++)
            {
                _dataCT.Add(0);
                int[] idxs = new int[2];
                if (_curveDatas[i] != null)
                {
                    for (int j = 0; j < _curveDatas[i].Length; j++)
                    {
                        _dataCT[i] += _curveDatas[i][j];
                    }
                    _dataCT[i] = _dataCT[i] / _curveDatas[i].Length;
                }
            }
        }
        List<double> GLIST = null;
        /// <summary>
        /// 胶体金新摄像头模块格式化数据
        /// </summary>
        private void NewFormattingDatas()
        {
            try
            {
                int lineIndex = 0;
                int linendexs = 0;
                _curveDatas = new List<double[]>();
                _ctValues = new List<double[]>();

                List<Canvas> canvases = GetChildObjects<Canvas>(WrapPanelChannel, "rootCanvas");
                for (int i = 0; i < canvases.Count; i++)
                {
                    if (canvases[i] != null)
                    {
                        canvases[i].Background = null;
                    }
                }

                imgDatas.Clear();
                foreach (var datas in _newJtjDatas)
                {
                    if (datas == null || datas.Count == 0)
                    {
                        double[] ctVal = new double[2];
                        _curveDatas.Add(null);

                        _ctValues.Add(ctVal);
                        imgDatas.Add("");
                        linendexs++;
                        continue;//判断是否为空，实现跨通道测试
                    }

                    double nHelpBoxTop = 0;
                    double nHelpBoxLeft = 0;
                    if (linendexs == 0)
                    {
                        nHelpBoxTop = Global.nHelpBoxTop1;
                        nHelpBoxLeft = Global.nHelpBoxLeft1;
                    }
                    else if (linendexs == 1)
                    {
                        nHelpBoxTop = Global.nHelpBoxTop2;
                        nHelpBoxLeft = Global.nHelpBoxLeft2;
                    }
                    else if (linendexs == 2)
                    {
                        nHelpBoxTop = Global.nHelpBoxTop3;
                        nHelpBoxLeft = Global.nHelpBoxLeft3;
                    }
                    else if (linendexs == 3)
                    {
                        nHelpBoxTop = Global.nHelpBoxTop4;
                        nHelpBoxLeft = Global.nHelpBoxLeft4;
                    }

                    imgDatas.Add("");
                    if (GLIST == null) GLIST = new List<double>();
                    _curveDatas.Add(null);
                    _ctValues.Add(null);
                    if (datas == null) continue;

                    if (Global.JtjVersion == 3 || Global.Is3500I )
                    {
                        for (int i = datas.Count - 1; i >= 0; i--)
                        {
                            if (datas[i] != null)
                            {
                                StringBuilder val = new StringBuilder();
                                for (int j = 0; j < datas[i].Length; j++)
                                {
                                    val.AppendFormat("{0}|", datas[i][j]);
                                }
                                imgDatas[lineIndex] = val.ToString();
                                break;
                            }
                        }
                    }

                    foreach (var buffer in datas)
                    {
                        if (buffer == null) continue;
                        int HDR_LEN = 5;
                        int WIDTH = (int)((UInt32)buffer[1] + (UInt32)(buffer[2] << 8));
                        int HEIGHT = (int)((UInt32)buffer[3] + (UInt32)(buffer[4] << 8));
                        int BITDEPTH = 2;
                        int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);
                        byte[] pixels = new byte[DATA_LEN];
                        Array.Copy(buffer, HDR_LEN, pixels, 0, DATA_LEN);
                        byte[] pixelsrgb = new byte[WIDTH * HEIGHT * 3];

                        for (int j = 0; j < HEIGHT; ++j)
                        {
                            double greens = 0;
                            bool isSave = false;

                            for (int i = 0; i < WIDTH; ++i)
                            {
                                int offset16 = (j * WIDTH + i) * 2;
                                int offset24 = (j * WIDTH + i) * 3;
                                // rgb565 低字节在前
                                int rgb565 = (int)(pixels[offset16] + ((uint)pixels[offset16 + 1] << 8));
                                int red = ((rgb565 >> 11) & 0x1F) << 3;
                                int green = ((rgb565 >> 5) & 0x3F) << 2;
                                int blue = ((rgb565) & 0x1F) << 3;
                                pixelsrgb[offset24] = (byte)blue;
                                pixelsrgb[offset24 + 1] = (byte)green;
                                pixelsrgb[offset24 + 2] = (byte)red;

                                if ((j > (int)nHelpBoxTop && j <= (int)nHelpBoxTop + (int)Global.nHelpBoxHeight) &&
                                   (i > (int)nHelpBoxLeft && i <= (int)nHelpBoxLeft + (int)Global.nHelpBoxWidth))
                                {
                                    greens += ((rgb565 >> 5) & 0x3F) << 2;
                                    isSave = true;
                                }
                            }

                            //横向 Global.nHelpBoxWidth 个点的g值
                            if (isSave)
                            {
                                GLIST.Add(greens / Global.nHelpBoxWidth);
                            }
                        }
                        if (canvases != null && canvases[lineIndex] != null && canvases[lineIndex].Background == null)
                        {
                            BitmapSource img = BitmapSource.Create(WIDTH, HEIGHT, 96, 96, PixelFormats.Bgr24, BitmapPalettes.WebPalette, pixelsrgb, WIDTH * 3);
                            if (img != null)
                            {
                                ImageBrush ib = new ImageBrush()
                                {
                                    ImageSource = img,
                                    TileMode = TileMode.None,
                                    Stretch = Stretch.None,
                                    AlignmentX = AlignmentX.Left,
                                    AlignmentY = AlignmentY.Top
                                };
                                canvases[lineIndex].Background = ib;
                            }
                        }
                    }
                    double[] line = null;
                    if (GLIST.Count > 0)
                    {
                        //存储当前通道的曲线集合
                        List<double[]> data = new List<double[]>();
                        //一条曲线 Global.nHelpBoxHeight 个点
                        for (int i = 0; i < GLIST.Count; i++)
                        {
                            double[] gs = new double[(int)Global.nHelpBoxHeight];
                            GLIST.CopyTo(i, gs, 0, gs.Length);
                            data.Add(gs);
                            i += (int)(Global.nHelpBoxHeight - 1);
                        }

                        //将当前通道曲线集合，合成为一条曲线
                        double[] oldline = new double[(int)Global.nHelpBoxHeight];
                        for (int i = 0; i < Global.nHelpBoxHeight; i++)
                        {
                            double db = 0;
                            for (int j = 0; j < data.Count; j++)
                            {
                                db += data[j][i];
                            }
                            oldline[i] = db / data.Count;
                        }

                        //曲线反转
                        int index = 0;
                        int len = oldline.Length;
                        //反转后的曲线
                        line = new double[len];
                        for (int i = len - 1; i >= 0; i--)
                        {
                            line[index] = oldline[i];
                            index++;
                        }

                        //line = DB4DWT(line);
                        _ctValues[linendexs] = GetArea(line);
                    }

                    _curveDatas[linendexs] = line;
                    GLIST.Clear();
                    lineIndex++;
                    linendexs++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "摄像头格式化数据");
            }
        }
        /// <summary>
        /// 胶体金新摄像头模块曲线面积计算
        /// </summary>
        /// <param name="data"></param>
        private double[] GetArea(double[] data)
        {
            double[] ctVal = new double[2];
            int pointNum = 15;
            List<double> areas = new List<double>();
            for (int i = 0; i < data.Length - pointNum; i++)
            {
                double area = 0;
                //面积点数渐变平均值，比如十个点的面积，取第一个点和最后一个点差值/8
                double avg = (data[(int)(i + pointNum - 1)] - data[i]) / (pointNum - 2);
                ////第一个点的面积(第一个点-第二个点)
                //area += data[i] - data[i + 1];
                ////最后一个点的面积(最后一个点-倒数第二个点)
                //area += data[(int)(Global.PointNum - 1)] - data[i + (int)(Global.PointNum - 2)];
                double temp = 0;
                //如果是算10个点的面积，则去掉头和尾的点，算中间的面积
                for (int j = 1; j <= pointNum - 2; j++)
                {
                    temp = data[i] + avg * j;
                    area += temp - data[i + j];
                }
                areas.Add(area);
            }

            int indexC = 0, indexT = 0;
            for (int i = 0; i < areas.Count / 2; i++)
            {
                if (areas[i] > ctVal[0])
                {
                    ctVal[0] = areas[i];
                    indexC = i;
                }
                if (areas[(areas.Count / 2) + i] > ctVal[1])
                {
                    ctVal[1] = areas[(areas.Count / 2) + i];
                    indexT = (areas.Count / 2) + i;
                }
            }

            return ctVal;
        }
        public static List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); ++i)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T)
                {
                    if (((T)child).Name == name)
                        childList.Add((T)child);
                }
                else
                {
                    List<T> tmpChildList = GetChildObjects<T>(child, name);
                    if (tmpChildList.Count > 0)
                        childList.AddRange(tmpChildList);
                }
            }
            return childList;
        }
        /// <summary>
        /// 胶体金3.0重新检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToDetect_Click(object sender, RoutedEventArgs e)
        {
            ToDetect();
        }
        private void ToDetect()
        {
            //重新计算结果，需要先清空UI上的数据
            if (_listDetectResult != null) _listDetectResult.Clear();
            if (_listStrRecord != null) _listStrRecord.Clear();
            if (_listJudmentValue != null) _listJudmentValue.Clear();
            if (_listCheckResult != null) _listCheckResult.Clear();
            if (_listResult != null) _listResult.Clear();
            if (_plotters != null) _plotters.Clear();
            if (_dateAxis != null) _dateAxis.Clear();
            WrapPanelChannel.Children.Clear();
            int sampleNum = _item.SampleNum;
            WrapPanelChannel.Width = 0;
            _HoleNumber = 1;
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum++), _item.Hole[i].SampleName);
                    WrapPanelChannel.Children.Add(element);
                    _plotters.Add(UIUtils.GetChildObject<ChartPlotter>(element, "chartPlotter"));
                    _dateAxis.Add(UIUtils.GetChildObject<HorizontalDateTimeAxis>(element, "dateAxis"));
                    _listCheckResult.Add(UIUtils.GetChildObject<TextBox>(element, _textBoxDetectResult));
                    _listResult.Add(UIUtils.GetChildObject<TextBox>(element, _textJugmentResult));
                    WrapPanelChannel.Width += 450;
                    _CheckValue = new String[_HoleNumber, 22];
                    _HoleNumber++;
                }
                else
                {
                    _listCheckResult.Add(null);
                    _listResult.Add(null);
                    _plotters.Add(new ChartPlotter());
                    _dateAxis.Add(new HorizontalDateTimeAxis());
                }
            }

            _plotters.Add(new ChartPlotter());
            _dateAxis.Add(new HorizontalDateTimeAxis());

            //格式化数据
            if (Global.JtjVersion == 3)
            {
                NewFormattingDatas();
            }
            else
            {
                FormattingDatas();
            }
            //绘制曲线
            DrawingCurve();
            //曲线计算
            CalcCurve();
            //计算结果
            CalcResult();

            //计算完成后，需要对之前保存的数据进行更新操作。
            if (syscodes != null && syscodes.Length > 0)
            {
                try
                {
                    int count = TestResultConserve.UpdateValues(_CheckValue, syscodes, out syscodes);
                    //if (count == syscodes.Length)
                    //{
                    //    MessageBox.Show("检测结果更新成功！", "更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                    //else if (count > 0 && count < syscodes.Length)
                    //{
                    //    MessageBox.Show("检测结果部分更新成功！", "更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("检测结果更新失败！", "更新提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "胶体金3.0模块");
                }
            }
        }

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateItem()
        {
            try
            {
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        _item.SampleNum++;
                    }
                }
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        class MsgThread : ChildThread
        {
            GszReportWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public MsgThread(GszReportWindow wnd)
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
                    FileUtils.OprLog(3, wnd.logType, ex.ToString());
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

        class GSZResult
        {
            public const int GSZF_STAT_UNKNOWN = 0x00;
            public const int GSZF_STAT_PLUS = 0x01;
            public const int GSZF_STAT_MINUS = 0x02;
            public const int GSZF_STAT_INVALID = 0x03;
            public int result = GSZF_STAT_INVALID;
            public double density = 0;
        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }
            Upload();
        }

        private bool UploadCheck()
        {
            if (!Global.IsConnectInternet())
            {
                Global.IsStartUploadTimer = true;
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

            if (Global.InterfaceType.Equals("ZH"))
            {
                if (Global.DeviceID.Length == 0 || Wisdom.USER.Length == 0 || Wisdom.PASSWORD.Length == 0)
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
            }
            return true;
        }

        private void Upload()
        {
            if (!UploadCheck()) return;

            try
            {
                LabelInfo.Content = "正在上传...";
                DataTable dt = _resultTable.GetAsDataTable("", "", 6, _AllNumber);
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
                    Global.IsStartUploadTimer = false;
                    LabelInfo.Content = "暂无需要上传的数据";
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
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show("上传时出现异常！\r\n异常信息：" + ex.Message, "数据上传");
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