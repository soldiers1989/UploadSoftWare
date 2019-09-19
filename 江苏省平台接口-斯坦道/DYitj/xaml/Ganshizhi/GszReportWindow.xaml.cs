using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AIO
{
    /// <summary>
    /// GszReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszReportWindow : Window
    {

        #region 全局变量
        public static List<byte[]> _listRGBValues;
        private static double[][] _rgbValues;
        public static DYGSZItemPara _item = null;
        public static GszMeasureWindow.HelpBox[] _helpBoxes = null;
        public static List<string> _listDetectResult = null;
        public static List<string> _listStrRecord;
        private string[] _methodToString = { "定性法", "定量法" };
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private DateTime _date = DateTime.Now;
        private MsgThread _msgThread;
        private List<TextBox> _listJudmentValue = null;
        private string[,] _CheckValue;
        private int _HoleNumber = 1;
        private int _AllNumber = 0;
        public List<TextBox> _RecordValue = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private DispatcherTimer _DataTimer = null;
        private bool IsUpLoad = false;
        /// <summary>
        /// 是否上传到广东省智慧平台 或者 同时上传至达元平台和智慧平台
        /// </summary>
        private bool IsUploadZHorALL = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
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
            _listDetectResult = new List<string>();
            _listStrRecord = new List<string>();
            //listJudmentValue = new List<string>();
            int sampleNum = _item.SampleNum;
            // 添加布局
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                WrapPanelChannel.Children.Add(element);
                if (_item.Hole[i].Use)
                {
                    sampleNum++;
                    _CheckValue = new string[_HoleNumber, 17];
                    _HoleNumber++;
                }
                else
                    element.Visibility = System.Windows.Visibility.Collapsed;
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
                Save();
                //2016年3月7日 对于没有强制上传的用户采用提示方式引导用户上传，若不上传则不保存当前检测数据
                if (LoginWindow._userAccount.UpDateNowing)
                {               
                    Upload();
                }
                //else
                //{
                //    if (MessageBox.Show("是否上传数据!？\n注意：若不上传数据，则此次检测的所有数据都将不做保存处理!\n请知悉!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //    {
                //        Save();
                //        Upload();
                //    }
                //}
            }
            UpdateItem();
            ButtonPrint.IsEnabled = true;
            btn_upload.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _msgThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save()
        {
            _AllNumber = TestResultConserve.ResultConserve(_CheckValue);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private PrintHelper.Report GenerateReport()
        {
            PrintHelper.Report report = new PrintHelper.Report();
            try
            {
                report.ItemName = _item.Name;
                report.ItemCategory = "干化学";
                report.User = LoginWindow._userAccount.UserName;
                report.Unit = _item.Unit;
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
                MessageBox.Show("打印时出现异常：" + ex.Message, "系统提示");
            }
        }

        private UIElement GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border()
            {
                Width = 185,
                Height = 440,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "border"
            };
            StackPanel stackPanel = new StackPanel()
            {
                Width = 185,
                Height = 420,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 185,
                Height = 40,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };
            Label label = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = " 检测通道" + (channel + 1)
            };
            Canvas canvas = new Canvas()
            {
                Width = 185,
                Height = 200,
                Background = Brushes.Gray,
                Name = "canvas"
            };
            WrapPanel wrapPannelSampleNum = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelSampleNum = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品编号:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleNum = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelSampleName = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品名称:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            WrapPanel wrapPannelRGBValue = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelRGBValue = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " RGB值:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxRGBValue = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxRGBValue"
            };
            WrapPanel wrapPannelDetectResult = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelDetectResult = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 检测结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxDetectResult = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxDetectResult"
            };

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelJudgment = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 判定结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textJugmentResult = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textJugmentResult"
            };
            //判定标准值
            WrapPanel wrapStandValue = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelStandValue = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 标准值:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textStandValue = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textStandValue"
            };
            grid.Children.Add(label);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapPannelRGBValue.Children.Add(labelRGBValue);
            wrapPannelRGBValue.Children.Add(textBoxRGBValue);
            wrapPannelDetectResult.Children.Add(labelDetectResult);
            wrapPannelDetectResult.Children.Add(textBoxDetectResult);
            wrapJudgemtn.Children.Add(labelJudgment);
            wrapJudgemtn.Children.Add(textJugmentResult);
            wrapStandValue.Children.Add(labelStandValue);
            wrapStandValue.Children.Add(textStandValue);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(canvas);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);
            stackPanel.Children.Add(wrapPannelRGBValue);
            stackPanel.Children.Add(wrapPannelDetectResult);
            stackPanel.Children.Add(wrapJudgemtn);
            stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;

            return border;
        }

        private void ShowResult()
        {
            // 计算RGB值
            _rgbValues = new double[_listRGBValues.Count][];
            for (int i = 0; i < _listRGBValues.Count; ++i)
            {
                if (null != _listRGBValues[i])
                    _rgbValues[i] = RawRGBToDouble(_listRGBValues[i]);
                else
                    _rgbValues[i] = null;
            }
            int sampleNum = _item.SampleNum;
            _listStrRecord = new List<string>();
            List<Canvas> listCanvases = UIUtils.GetChildObjects<Canvas>(WrapPanelChannel, "canvas");
            List<TextBox> listRGB = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxRGBValue");
            List<TextBox> listResult = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxDetectResult");
            List<TextBox> listJudgmentRes = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textJugmentResult");
            List<TextBox> listStandValue = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textStandValue");
            int num = 0;
            _RecordValue = listResult;
            _listJudmentValue = listJudgmentRes;
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    byte r, g, b;
                    r = (byte)_rgbValues[i][0];
                    g = (byte)_rgbValues[i][1];
                    b = (byte)_rgbValues[i][2];

                    listCanvases[i].Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                    listRGB[i].Text = "(" + r + "," + g + "," + b + ")";
                    GSZResult gszResult = CalGSZFResult(_rgbValues[i], _item);

                    string str = string.Empty;
                    string[] UnqualifiedValue = new string[4];
                    if (DYGSZItemPara.METHOD_DX == _item.Method)
                    {
                        str = GSZFResultStatToStr(gszResult);
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);
                        UnqualifiedValue[0] = str;
                    }
                    else
                    {
                        str = String.Format("{0:F3}", gszResult.density);
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                    }

                    //str = Convert.ToString(UnqualifiedValue[0]);
                    UnqualifiedValue[0] = str;
                    _listJudmentValue[i].Text = str;
                    if (!_listJudmentValue[i].Text.Trim().Equals("合格"))
                    {
                        _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                    }
                    listJudgmentRes[i].Text = str;
                    listStandValue[i].Text = Convert.ToString(UnqualifiedValue[2]);
                    listResult[i].Text = str;
                    _listDetectResult.Add(str);

                    _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                    _CheckValue[(num > 0 ? (i - num) : i), 1] = "干化学";
                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                    _CheckValue[(num > 0 ? (i - num) : i), 4] = UnqualifiedValue[0];
                    _CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit;
                    _CheckValue[(num > 0 ? (i - num) : i), 6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                    _CheckValue[(num > 0 ? (i - num) : i), 8] = string.IsNullOrEmpty(_item.Hole[i].SampleName) ? string.Empty : _item.Hole[i].SampleName;
                    _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                    _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                    _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                    _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskName ?? string.Empty;
                    _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                    _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                    _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].ProduceCompany;
                }
                else
                {
                    num += 1;
                    _listStrRecord.Add(null);
                    _listDetectResult.Add(null);
                }
            }
        }

        /// <summary>
        /// 判定合格不合格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="SampleName"></param>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        //private string[] UnqualifiedOrQualified(string str, string SampleName, string ItemName)
        //{
        //    string[] Qualifide = new string[2];
        //    if (str == "NA" || str.Contains("*"))
        //    {
        //        Qualifide[0] = "无法判定";
        //        Qualifide[1] = string.Empty;
        //    }
        //    else
        //    {
        //        double strInt = Convert.ToDouble(str);
        //        if (Global.samplenameadapter != null)
        //        {
        //            foreach (SampleNameStandard SumTemp in Global.samplenameadapter)
        //            {
        //                if (SampleName == string.Empty)
        //                {
        //                    Qualifide[0] = "无法判定";
        //                    Qualifide[1] = string.Empty;
        //                }
        //                else
        //                {
        //                    if (SampleName == SumTemp.FtypeNmae && ItemName == SumTemp.Name)
        //                    {
        //                        double ValueInt = Convert.ToDouble(SumTemp.StandardValue);
        //                        string sign = SumTemp.Demarcate;
        //                        if (sign == "≤")
        //                        {
        //                            if (strInt <= ValueInt)
        //                            {
        //                                Qualifide[0] = "合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                            else
        //                            {
        //                                Qualifide[0] = "不合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                        }
        //                        if (sign == "<")
        //                        {
        //                            if (strInt < ValueInt)
        //                            {
        //                                Qualifide[0] = "合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                            if (strInt >= ValueInt)
        //                            {
        //                                Qualifide[0] = "不合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                        }
        //                        if (sign == "≥")
        //                        {
        //                            if (strInt >= ValueInt)
        //                            {
        //                                Qualifide[0] = "合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                            if (strInt < ValueInt)
        //                            {
        //                                Qualifide[0] = "不合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                        }
        //                        if (sign == ">")
        //                        {
        //                            if (strInt > ValueInt)
        //                            {
        //                                Qualifide[0] = "合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                            if (strInt <= ValueInt)
        //                            {
        //                                Qualifide[0] = "不合格";
        //                                Qualifide[1] = SumTemp.ItemDes;
        //                            }
        //                        }
        //                    }

        //                }
        //            }
        //        }

        //    }
        //    return Qualifide;
        //}

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateItem()
        {
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    _item.SampleNum++;
                }
            }
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
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

        class GSZResult
        {
            public const int GSZF_STAT_UNKNOWN = 0x00;
            public const int GSZF_STAT_PLUS = 0x01;
            public const int GSZF_STAT_MINUS = 0x02;
            public const int GSZF_STAT_INVALID = 0x03;
            public int result = GSZF_STAT_INVALID;
            public double density = 0;
        }

        string GSZFResultStatToStr(GSZResult gszResult)
        {
            string str = string.Empty;
            switch (gszResult.result)
            {
                case GSZResult.GSZF_STAT_PLUS:
                    str = "不合格";
                    break;
                case GSZResult.GSZF_STAT_MINUS:
                    str = "合格";
                    break;
                case GSZResult.GSZF_STAT_INVALID:
                    str = "无效";
                    break;
                default:
                    str = "错误";
                    break;
            }
            return str;
        }


        private double[] RawRGBToDouble(byte[] data)
        {
            if (null == data)
                return null;

            double[] rgb = new double[3];

            int idx = 0;
            rgb[0] = data[idx] + data[idx + 1] * 0x100 + data[idx + 2] * 0x10000 + data[idx + 3] * 0x1000000;
            rgb[0] /= 1000.0;

            idx += 4;
            rgb[1] = data[idx] + data[idx + 1] * 0x100 + data[idx + 2] * 0x10000 + data[idx + 3] * 0x1000000;
            rgb[1] /= 1000.0;

            idx += 4;
            rgb[2] = data[idx] + data[idx + 1] * 0x100 + data[idx + 2] * 0x10000 + data[idx + 3] * 0x1000000;
            rgb[2] /= 1000.0;

            return rgb;
        }

        private GSZResult CalGSZFResult(double[] rgb, DYGSZItemPara mItem)
        {
            GSZResult gszResult = new GSZResult();
            double a = 0, v = 0, f, GSZFppb;
            double r, g, b;
            r = rgb[0];
            g = rgb[1];
            b = rgb[2];

            if (DYGSZItemPara.METHOD_DL == mItem.Method)
            {   // 定量
                switch (mItem.dl.RGBSel)
                {
                    case 0:
                        a = r;
                        break;
                    case 1:
                        a = g;
                        break;
                    default:
                        a = b;
                        break;
                }

                f = mItem.dl.A0 + mItem.dl.A1 * a + mItem.dl.A2 * a * a + mItem.dl.A3 * a * a * a;
                f = mItem.dl.B0 * f + mItem.dl.B1;
                GSZFppb = f;
                gszResult.density = GSZFppb;
                if ((GSZFppb >= mItem.dl.LimitH) && (0 != mItem.dl.LimitH))
                    gszResult.result = GSZResult.GSZF_STAT_PLUS;
                else if ((GSZFppb <= mItem.dl.LimitL) && (0 != mItem.dl.LimitL))
                    gszResult.result = GSZResult.GSZF_STAT_PLUS;
                else
                    gszResult.result = GSZResult.GSZF_STAT_MINUS;

                return gszResult;
            }
            else
            {   // 定性 阳性
                switch (mItem.dx.RGBSelPlus)
                {
                    case 0:
                        v = r;
                        break;
                    case 1:
                        v = g;
                        break;
                    case 2:
                        v = b;
                        break;
                }
                if ((0 == mItem.dx.ComparePlus) && (v >= mItem.dx.PlusT))
                {
                    gszResult.result = GSZResult.GSZF_STAT_PLUS;
                    return gszResult;
                }
                else if ((1 == mItem.dx.ComparePlus) && (v < mItem.dx.PlusT))
                {
                    gszResult.result = GSZResult.GSZF_STAT_PLUS;
                    return gszResult;
                }

                // 定性 阴性
                switch (mItem.dx.RGBSelMinus)
                {
                    case 0:
                        v = r;
                        break;
                    case 1:
                        v = g;
                        break;
                    case 2:
                        v = b;
                        break;
                }
                if ((0 == mItem.dx.CompareMinus) && (v >= mItem.dx.MinusT))
                {
                    gszResult.result = GSZResult.GSZF_STAT_MINUS;
                    return gszResult;
                }
                else if ((1 == mItem.dx.CompareMinus) && (v < mItem.dx.MinusT))
                {
                    gszResult.result = GSZResult.GSZF_STAT_MINUS;
                    return gszResult;
                }
            }
            return gszResult;
        }

        #region-- UpDataNowing
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Upload();
        }

        /// <summary>
        /// 30s上传超时
        /// </summary>
        private void UploadTimeOut(object sender, EventArgs e)
        {
            LabelInfo.Content = "正在上传...";
            if (LabelInfo.Content.Equals("正在上传..."))
            {
                LabelInfo.Content = "上传超时，请检测连接设置!";
                _DataTimer.Stop();
                _msgThread.Stop();
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        private void Upload()
        {
            if (Global.InterfaceType.Equals("DY"))
            {
                if (Global.samplenameadapter == null || Global.samplenameadapter.Count == 0)
                {
                    if (MessageBox.Show("还未进行服务器通讯测试，可能导致数据上传失败！\r\n是否前往【设置】界面进行通讯测试？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow();
                        window.ShowDialog();
                    }
                }
            }
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LabelInfo.Content = "正在上传...";
            try
            {
                tlsttResultSecondOpr Rs = new tlsttResultSecondOpr();
                DataTable dt = Rs.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                if (dt != null || dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].UploadUserUUID;
                    }
                }
                Message msg = new Message()
                {
                    what = MsgCode.MSG_UPLOAD,
                    obj1 = Global.samplenameadapter[0],
                    table = dt
                };
                if (IsUploadZHorALL)
                {
                    if (Wisdom.DeviceID.Length == 0)
                    {
                        if (MessageBox.Show("【无法上传】 - 设备唯一码未设置，是否立即设置仪器唯一码?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            SettingsWindow window = new SettingsWindow()
                            {
                                DeviceIdisNull = false
                            };
                            window.ShowDialog();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                        msg.selectedRecords = dtList;
                    }
                }
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

        #endregion

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }

            Upload();
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
