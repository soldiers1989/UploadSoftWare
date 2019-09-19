﻿using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AIO
{
    /// <summary>
    /// JtjReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjReportWindow : Window
    {

        #region 全局变量
        public List<byte[]> _listGrayValues;
        public DYJTJItemPara _item = null;
        public JtjMeasureWindow.HelpBox[] _helpBoxes = null;
        public List<string> _listDetectResult = null;
        public List<string> _listStrRecord = null;
        public List<TextBox> _RecordValue = null;
        private string[] _methodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)", "定性比色法(T/C)" };
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private DateTime _date = DateTime.Now;
        private MsgThread _msgThread;
        private List<TextBox> _listJudmentValue = null;
        private int _HoleNumber = 1;
        private string[,] _CheckValue;
        private int _AllNumber = 0;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private DispatcherTimer _DataTimer = null;
        public List<int> cValues;
        public List<int> tValues;
        private bool IsUpLoad = false;
        /// <summary>
        /// 是否上传到广东省智慧平台 或者 同时上传至达元平台和智慧平台
        /// </summary>
        private bool IsUploadZHorALL = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        #endregion

        public JtjReportWindow()
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

            _msgThread = new MsgThread(this);
            _msgThread.Start();
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
                report.ItemCategory = "胶体金";
                report.User = LoginWindow._userAccount.UserName;
                report.Unit = _item.Unit;
                report.Date = _date.ToString("yyyy-MM-dd HH:mm:ss");
                report.Judgment = _item.Hole[0].SampleName;
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
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
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
                Width = 95,
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
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            WrapPanel wrapPannelGrayValue = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelGrayValue = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 灰度值:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxGrayValue = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxGrayValue"
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
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
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
                Width = 95,
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
                Width = 95,
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
            wrapPannelGrayValue.Children.Add(labelGrayValue);
            wrapPannelGrayValue.Children.Add(textBoxGrayValue);
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
            stackPanel.Children.Add(wrapPannelGrayValue);
            stackPanel.Children.Add(wrapPannelDetectResult);
            stackPanel.Children.Add(wrapJudgemtn);
            stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;
            return border;
        }

        private void ShowResult()
        {
            int sampleNum = _item.SampleNum;
            // 画出灰度曲线
            List<Canvas> canvases = UIUtils.GetChildObjects<Canvas>(WrapPanelChannel, "canvas");
            List<TextBox> listTextBoxGray = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxGrayValue");
            List<TextBox> listTextBoxDetectResult = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxDetectResult");
            List<TextBox> listJudgmentRes = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textJugmentResult");
            List<TextBox> listStandValue = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textStandValue");
            _RecordValue = listTextBoxDetectResult;
            _listJudmentValue = listJudgmentRes;
            int num = 0;
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    int nLineWidth = 5;
                    int TOffset = WeitiaoCT(_listGrayValues[i], _helpBoxes[i].TLineOffset, nLineWidth);
                    int COffset = WeitiaoCT(_listGrayValues[i], _helpBoxes[i].CLineOffset, nLineWidth);

                    int TValue = 0;
                    // 向前数5格
                    for (int n = 0; n < nLineWidth; ++n)
                        TValue += _listGrayValues[i][TOffset - n];
                    TValue /= nLineWidth;
                    int CValue = 0;
                    for (int n = 0; n < nLineWidth; ++n)
                        CValue += _listGrayValues[i][COffset - n];
                    CValue /= nLineWidth;
                    DrawGrayCurve(canvases[i], _listGrayValues[i], COffset, TOffset);
                    if (cValues[i] > 0 || tValues[i] > 0)
                    {
                        CValue = cValues[i] / nLineWidth;
                        TValue = tValues[i] / nLineWidth;
                    }
                    JTJResult jtjResult = CalJTJFResult((byte)CValue, (byte)TValue, _item);
                    listTextBoxGray[i].Text = "C(" + CValue + ") T(" + TValue + ")";
                    string str = string.Empty;
                    string[] UnqualifiedValue = new string[4];
                    str = JTJFResultStatToStr(jtjResult);
                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);
                    if (_item.Method == DYJTJItemPara.METHOD_DXXX)
                    {
                        if (str == "阳性")
                        {
                            _listJudmentValue[i].Text = "阳性";
                            listJudgmentRes[i].Text = "不合格";
                            UnqualifiedValue[0] = "不合格";
                        }
                        else if (str == "阴性")
                        {
                            _listJudmentValue[i].Text = "阴性";
                            listJudgmentRes[i].Text = "合格";
                            UnqualifiedValue[0] = "合格";
                        }
                        else
                        {
                            _listJudmentValue[i].Text = "无效";
                            listJudgmentRes[i].Text = "无效";
                            UnqualifiedValue[0] = "无效";
                        }
                    }
                    //2016年10月13日 wenj 
                    //新版本判定方法：Abs（C-T）≥Abs_X时 C≥T阳性 C＜T阴性；Abs（C-T）＜Abs_X时 SexIdx=0阴性 =1阳性 =2可疑
                    //2017年10月12日 wenj 最新版比色法判定方法：C/T≤Abs_X 阴性
                    else if (_item.Method == DYJTJItemPara.METHOD_DXBS)
                    {
                        //综合C值<15则检测无效
                        //if (CValue > 15)
                        if (CValue > _item.InvalidC)
                        {
                            double ctAbs = CValue / TValue;
                            if (ctAbs <= _item.dxbs.Abs_X)
                            {
                                _listJudmentValue[i].Text = str = "阴性";
                                listJudgmentRes[i].Text = "合格";
                                UnqualifiedValue[0] = "合格";
                            }
                            else
                            {
                                _listJudmentValue[i].Text = str = "阳性";
                                listJudgmentRes[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";
                            }
                            //double ctAbs = System.Math.Abs(CValue - TValue);
                            //if (ctAbs >= _item.dxbs.Abs_X)
                            //{
                            //    if (CValue >= TValue)
                            //    {
                            //        _listJudmentValue[i].Text = str = "阳性";
                            //        listJudgmentRes[i].Text = "不合格";
                            //        UnqualifiedValue[0] = "不合格";
                            //    }
                            //    else
                            //    {
                            //        _listJudmentValue[i].Text = str = "阴性";
                            //        listJudgmentRes[i].Text = "合格";
                            //        UnqualifiedValue[0] = "合格";
                            //    }
                            //}
                            //else
                            //{
                            //    if (_item.dxbs.SetIdx == 0)
                            //    {
                            //        _listJudmentValue[i].Text = str = "阴性";
                            //        listJudgmentRes[i].Text = "合格";
                            //        UnqualifiedValue[0] = "合格";
                            //    }
                            //    else if (_item.dxbs.SetIdx == 1)
                            //    {
                            //        _listJudmentValue[i].Text = str = "阳性";
                            //        listJudgmentRes[i].Text = "不合格";
                            //        UnqualifiedValue[0] = "不合格";
                            //    }
                            //    else if (_item.dxbs.SetIdx == 2)
                            //    {
                            //        _listJudmentValue[i].Text = str = "可疑";
                            //        listJudgmentRes[i].Text = "可疑";
                            //        UnqualifiedValue[0] = "可疑";
                            //    }
                            //}
                        }
                        else
                        {
                            _listJudmentValue[i].Text = str = "检测无效";
                            _listJudmentValue[i].FontWeight = FontWeights.Bold;
                            _listJudmentValue[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            listJudgmentRes[i].Text = "检测无效";
                            listJudgmentRes[i].FontWeight = FontWeights.Bold;
                            listJudgmentRes[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            UnqualifiedValue[0] = "检测无效";
                        }
                    }
                    //2017年6月25日 wenj 新增 定性比色法（T/C） T/C≤Abs_X为阳性，T/C＞Abs_X为阴性
                    else if (_item.Method == DYJTJItemPara.METHOD_DXBS_TC)
                    {
                        if (CValue > _item.InvalidC)
                        {
                            float ctAbs = (float)TValue / CValue;
                            if (ctAbs <= _item.dxbs.Abs_X)
                            {
                                _listJudmentValue[i].Text = str = "阳性";
                                listJudgmentRes[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";
                            }
                            else
                            {
                                _listJudmentValue[i].Text = str = "阴性";
                                listJudgmentRes[i].Text = "合格";
                                UnqualifiedValue[0] = "合格";
                            }
                        }
                        else
                        {
                            _listJudmentValue[i].Text = str = "检测无效";
                            listJudgmentRes[i].Text = "检测无效";
                            UnqualifiedValue[0] = "检测无效";
                        }
                    }
                    else
                    {
                        str = String.Format("{0:F3}", jtjResult.density);
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                        _listJudmentValue[i].Text = str;
                        listJudgmentRes[i].Text = str;
                        listStandValue[i].Text = Convert.ToString(UnqualifiedValue[2]);
                    }

                    if (!_listJudmentValue[i].Text.Trim().Equals("合格"))
                    {
                        _listJudmentValue[i].Foreground = new SolidColorBrush(Colors.Red);
                    }

                    //判定合格不合格
                    listTextBoxDetectResult[i].Text = str;
                    _listDetectResult.Add(str); // 检测结果，要么是阴阳性，要么是浓度值。

                    _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                    _CheckValue[(num > 0 ? (i - num) : i), 1] = "胶体金";
                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                    _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
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
                    //_CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].ProduceCompany;

                    if (Global.ismore == true)
                    {
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].TaskName;
                       
                    }
                    else
                    {
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].TaskName;//_item.Hole[i].ProduceCompany;
                       
                    }
                }
                else
                {
                    num += 1;
                    _listStrRecord.Add(null);
                    _listDetectResult.Add(null);
                }
            }
        }

        private void DrawGrayCurve(Canvas canvas, byte[] grayValues, int cOffset, int tOffset)
        {
            double yOffset = 20;
            double width = canvas.Width;
            double height = canvas.Height - yOffset;
            double max = GetMaxByte(grayValues);
            double min = GetMinByte(grayValues);
            double curveHeight = max - min;
            if (0 == curveHeight)
                return;
            double curveWidth = grayValues.Length;

            Polyline polyline = new Polyline()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            PointCollection points = new PointCollection();
            for (int i = 0; i < grayValues.Length; ++i)
            {
                Point point = new Point(i * width / curveWidth, height - (grayValues[i] - min) * height / curveHeight);
                points.Add(point);
            }
            polyline.Points = points;
            canvas.Children.Add(polyline);

            for (int n = 0; n < 5; ++n)
            {
                Line c = new Line()
                {
                    X1 = (cOffset - n) * width / curveWidth,
                    Y1 = height + yOffset,
                    X2 = (cOffset - n) * width / curveWidth,
                    Y2 = height - (grayValues[cOffset - n] - min) * height / curveHeight,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                Line t = new Line()
                {
                    X1 = (tOffset - n) * width / curveWidth,
                    Y1 = height + yOffset,
                    X2 = (tOffset - n) * width / curveWidth,
                    Y2 = height - (grayValues[tOffset - n] - min) * height / curveHeight,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                canvas.Children.Add(c);
                canvas.Children.Add(t);
            }
        }

        private void UpdateItem()
        {
            // 更新样品编号
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    _item.SampleNum++;
                }
            }
            Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
        }

        class MsgThread : ChildThread
        {
            JtjReportWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(JtjReportWindow wnd)
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
        class JTJResult
        {
            public const int JTJF_STAT_UNKNOWN = 0x00;
            public const int JTJF_STAT_PLUS = 0x01;
            public const int JTJF_STAT_MINUS = 0x02;
            public const int JTJF_STAT_INVALID = 0x03;
            public const int JTJF_STAT_Lee = 0x04;
            public int result = JTJF_STAT_INVALID;
            public double density = 0; //如果是定量，需要用到浓度值
        }

        private JTJResult CalJTJFResult(byte CValue, byte TValue, DYJTJItemPara mItem)
        {
            JTJResult jtjResult = new JTJResult();
            if (CValue <= mItem.InvalidC)
            {   // C 线不显色为无效
                jtjResult.result = JTJResult.JTJF_STAT_INVALID;
            }
            else
            {
                switch (mItem.Method)
                {   // 定量
                    case DYJTJItemPara.METHOD_DXXX:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;

                            if (TValue <= mItem.dxxx.PlusT)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else if (TValue > mItem.dxxx.MinusT)
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;
                    case DYJTJItemPara.METHOD_DXBS:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;
                            if (TValue < CValue)// 阳性
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;
                    case DYJTJItemPara.METHOD_DLT:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;
                            if (f > mItem.dlt.Limit)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;

                    case DYJTJItemPara.METHOD_DLTC:
                        {
                            double a = CValue == 0 ? 1 : TValue * 1.0 / CValue;
                            double f = mItem.dltc.A0 + mItem.dltc.A1 * a + mItem.dltc.A2 * a * a + mItem.dltc.A3 * a * a * a;
                            jtjResult.density = f = mItem.dltc.B0 * f + mItem.dltc.B1;
                            if (f > mItem.dltc.Limit)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }

                        break;
                }
            }
            return jtjResult;
        }

        string JTJFResultStatToStr(JTJResult jtjResult)
        {
            string str = string.Empty;
            switch (jtjResult.result)
            {
                case JTJResult.JTJF_STAT_PLUS:
                    str = "阳性";
                    break;
                case JTJResult.JTJF_STAT_MINUS:
                    str = "阴性";
                    break;
                case JTJResult.JTJF_STAT_INVALID:
                    str = "无效";
                    break;
                //case JTJResult.JTJF_STAT_Lee:
                //    str = "有效";
                //    break;
                default:
                    str = "错误";
                    break;
            }
            return str;
        }

        private byte GetMinByte(byte[] data)
        {
            byte b = 255;
            for (int i = 0; i < data.Length; ++i)
                if (b > data[i])
                    b = data[i];
            return b;
        }

        private byte GetMaxByte(byte[] data)
        {
            byte b = 0;
            for (int i = 0; i < data.Length; ++i)
                if (b < data[i])
                    b = data[i];
            return b;
        }

        private int WeitiaoCT(byte[] data, int offset, int nWidth)
        {
            int lastOffset = offset;
            int startOffset = offset - nWidth / 2;
            int endOffset = offset + nWidth / 2;
            if (startOffset <= 0 || endOffset >= data.Length)
                return lastOffset;
            int maxValueIdx = startOffset;
            int maxValue = data[maxValueIdx];
            for (int i = startOffset; i < endOffset; ++i)
            {
                if (maxValue < data[i])
                {
                    maxValue = data[i];
                    maxValueIdx = i;
                }
            }
            lastOffset = maxValueIdx + nWidth / 2;

            return lastOffset;
        }

        #region-- UpDataNowing
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            //if (IsUpLoad)
            //{
            //    MessageBox.Show("当前数据已上传!", "系统提示");
            //    return;
            //}
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

            //LabelInfo.Content = "正在上传...";
            //try
            //{
            //    tlsttResultSecondOpr Rs = new tlsttResultSecondOpr();
            //    DataTable dt = Rs.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
            //    if (dt != null || dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            dt.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].UploadUserUUID;
            //        }
            //    }
            //    Message msg = new Message()
            //    {
            //        what = MsgCode.MSG_UPLOAD,
            //        obj1 = Global.samplenameadapter[0],
            //        table = dt
            //    };
            //    if (IsUploadZHorALL)
            //    {
            //        if (Wisdom.DeviceID.Length == 0)
            //        {
            //            if (MessageBox.Show("【无法上传】 - 设备唯一码未设置，是否立即设置仪器唯一码?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //            {
            //                SettingsWindow window = new SettingsWindow()
            //                {
            //                    DeviceIdisNull = false
            //                };
            //                window.ShowDialog();
            //            }
            //        }

            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
            //            msg.selectedRecords = dtList;
            //        }
            //    }
            //    Global.updateThread.SendMessage(msg, _msgThread);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "上传数据时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            //}
        }

        #endregion

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