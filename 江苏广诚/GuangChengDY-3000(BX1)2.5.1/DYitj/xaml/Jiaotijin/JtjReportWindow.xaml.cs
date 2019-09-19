﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Windows.Media.Imaging;

namespace AIO
{
    /// <summary>
    /// JtjReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjReportWindow : Window
    {

        #region 全局变量
        public List<byte[]> _listGrayValues;
        public JtjMeasureWindow.HelpBox[] _helpBoxes = null;
        public List<int> cValues;
        public List<int> tValues;

        /// <summary>
        /// 接收到的硬件数据
        /// </summary>
        public List<byte[]> _datas;
        /// <summary>
        /// 曲线数据
        /// </summary>
        private static List<double[]> _curveDatas;
        /// <summary>
        /// 胶体金新摄像头模块数据
        /// </summary>
        public List<byte[]>[] _newJtjDatas = null;
        /// <summary>
        /// 存储新模块的CT值
        /// </summary>
        private List<double[]> _ctValues = null;
        public DYJTJItemPara _item = null;
        public List<String> _listDetectResult = null;
        public List<String> _listStrRecord = null;
        public List<TextBox> _RecordValue = null;
        private String[] _methodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)" };
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private MsgThread _msgThread;
        private List<TextBox> _listJudmentValue = null;
        private Int32 _HoleNumber = 1;
        private String[,] _CheckValue;
        private Int32 _AllNumber = 0;
        private List<ChartPlotter> _plotters = null;
        private List<HorizontalDateTimeAxis> _dateAxis = null;
        private List<double> _ValueC = new List<double>(), _ValueT = new List<double>();
        private List<TextBox> _listCheckResult = null;
        private List<TextBox> _listResult = null;
        private String _textBoxDetectResult = "textBoxDetectResult";
        private String _textJugmentResult = "textJugmentResult";
        private DispatcherTimer _DataTimer = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private bool IsUpLoad = false;
        private string logType = "JtjReportWindow-error";
        /// <summary>
        /// 存储曲线数据
        /// </summary>
        private List<string> curveDatas = new List<string>();
        private List<string> imgDatas = new List<string>();
        /// <summary>
        /// 是否是debug模式
        /// </summary>
        public bool IsDebug = false;
        public double[] debugData = null;
        #endregion

        public JtjReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsShowValue)
                {
                    txtC.Visibility = Visibility.Visible;
                    txtCZ.Visibility = Visibility.Visible;
                    txtCalcCT.Visibility = Visibility.Visible;
                }
                if (null == _item)
                    return;
                ButtonPrev.IsEnabled = true;
                _listDetectResult = new List<String>();
                _listStrRecord = new List<String>();
                _listJudmentValue = new List<TextBox>();
                _listCheckResult = new List<TextBox>();
                _listResult = new List<TextBox>();
                _plotters = new List<ChartPlotter>();
                _dateAxis = new List<HorizontalDateTimeAxis>();
                int sampleNum = _item.SampleNum;
                WrapPanelChannel.Width = 0;

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
                        _CheckValue = new String[_HoleNumber, 26];
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            //if(Global.Is3500I )
            //{
            //    // 显示结果的时候，把字符串生成
            //    ShowResult();
            //}
            //else
            //{
                //格式化数据
                if (Global.JtjVersion == 3 || Global.Is3500I )
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
            //}
            
            if (_DataTimer == null)
            {
                _DataTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(0.5)
                };
                _DataTimer.Tick += new EventHandler(SaveAndUpload);
                _DataTimer.Start();
            }
            if (_msgThread == null)
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
            }
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

                    string str = "检测无效";
                    string[] UnqualifiedValue = new string[4];
                    string canvasVal = string.Empty;

                    if (_listGrayValues[i] != null)
                    {
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
                        for (int j = 0; j < _listGrayValues[i].Length; j++)
                        {
                            canvasVal += canvasVal.Length == 0 ? _listGrayValues[i][j].ToString() : "|" + _listGrayValues[i][j];
                        }
                        canvasVal += "," + COffset + "|" + TOffset;

                        if (cValues[i] > 0 || tValues[i] > 0)
                        {
                            CValue = cValues[i] / nLineWidth;
                            TValue = tValues[i] / nLineWidth;
                        }
                        JTJResult jtjResult = CalJTJFResult((byte)CValue, (byte)TValue, _item);
                        listTextBoxGray[i].Text = "C(" + CValue + ") T(" + TValue + ")";

                        str = JTJFResultStatToStr(jtjResult);
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);

                        //2017年12月14日 wenj 消线法修改判定方法 取检测项目中配置好的临界值来判定结果
                        if (_item.Method == DYJTJItemPara.METHOD_DXXX)
                        {
                            if (CValue > _item.InvalidC)
                            {
                                if (TValue > _item.dxxx.Abs_Max)
                                {
                                    _listJudmentValue[i].Text = str = "阴性";
                                    listJudgmentRes[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else if (TValue < _item.dxxx.Abs_Min)
                                {
                                    _listJudmentValue[i].Text = str = "阳性";
                                    _listJudmentValue[i].FontWeight = FontWeights.Bold;
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    listJudgmentRes[i].Text = "不合格";
                                    listJudgmentRes[i].FontWeight = FontWeights.Bold;
                                    listJudgmentRes[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "不合格";
                                }
                                else
                                {
                                    _listJudmentValue[i].Text = str = "可疑";
                                    _listJudmentValue[i].FontWeight = FontWeights.Bold;
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    listJudgmentRes[i].Text = "可疑";
                                    listJudgmentRes[i].FontWeight = FontWeights.Bold;
                                    listJudgmentRes[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "可疑";
                                }
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
                        //2017年12月14日 wenj 比色法修改判定方法：结果判定(阳性：T/C＜Min  阴性：T/C＞Max)；可疑：Min≤ T/C ≤Max
                        else if (_item.Method == DYJTJItemPara.METHOD_DXBS)
                        {
                            if (CValue > _item.InvalidC)
                            {
                                double tcAbs = (double)TValue / (double)CValue;
                                if (tcAbs > _item.dxbs.Abs_Max)
                                {
                                    _listJudmentValue[i].Text = str = "阴性";
                                    listJudgmentRes[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else if (tcAbs < _item.dxbs.Abs_Min)
                                {
                                    _listJudmentValue[i].Text = str = "阳性";
                                    _listJudmentValue[i].FontWeight = FontWeights.Bold;
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    listJudgmentRes[i].Text = "不合格";
                                    listJudgmentRes[i].FontWeight = FontWeights.Bold;
                                    listJudgmentRes[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "不合格";
                                }
                                else
                                {
                                    _listJudmentValue[i].Text = str = "可疑";
                                    _listJudmentValue[i].FontWeight = FontWeights.Bold;
                                    _listJudmentValue[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    listJudgmentRes[i].Text = "可疑";
                                    listJudgmentRes[i].FontWeight = FontWeights.Bold;
                                    listJudgmentRes[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "可疑";
                                }
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
                        else
                        {
                            str = String.Format("{0:F3}", jtjResult.density);
                            UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                            _listJudmentValue[i].Text = str;
                            listJudgmentRes[i].Text = str;
                            listStandValue[i].Text = Convert.ToString(UnqualifiedValue[2]);
                        }
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
                    _CheckValue[(num > 0 ? (i - num) : i), 9] = UnqualifiedValue[0];
                    _CheckValue[(num > 0 ? (i - num) : i), 10] = UnqualifiedValue[2];
                    _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                    _CheckValue[(num > 0 ? (i - num) : i), 12] = UnqualifiedValue[1];
                    _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskName ?? string.Empty;
                    _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                    _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                    _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].ProduceCompany;
                    _CheckValue[(num > 0 ? (i - num) : i), 17] = canvasVal;
                    //MessageBox.Show(_CheckValue[(num > 0 ? (i - num) : i), 17]);
                }
                else
                {
                    num += 1;
                    _listStrRecord.Add(null);
                    _listDetectResult.Add(null);
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

                            if (TValue <= mItem.dxxx.Abs_Max)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else if (TValue > mItem.dxxx.Abs_Min)
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
            if (data == null) return 0;

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
                    _CheckValue = new String[_HoleNumber, 26];
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

        private List<double[]> oldData = new List<double[]>();
        private void FormattingDatas()
        {
            FileUtils.Log(string.Format("格式化数据开始"));
            try
            {
                _curveDatas = new List<double[]>();
                _ctValues = new List<double[]>();
                double[] val = null;
                double[] originalData = null;
                for (int z = 0; z < _datas.Count; z++)
                {
                    byte[] dt = _datas[z];
                    if (dt == null)
                    {
                        _curveDatas.Add(null);
                        _ctValues.Add(null);
                        oldData.Add(null);
                        continue;
                    }

                    int len = dt.Length / 2, index = 0;
                    double[] data = new double[len];
                    originalData = new double[len];
                    string strdt = string.Empty;

                    for (int i = 0; i < len; i++)
                    {
                        originalData[i] = data[i] = dt[index + 1] * 256 + dt[index];
                        strdt += strdt.Length == 0 ? data[i].ToString() : "|" + data[i];
                        index += 2;
                    }

                    FileUtils.OprLog(2, "所有胶体金返回原始数据", strdt);//写入配置文件

                    FileUtils.Log(string.Format("数据总长度：{0}", data.Length));

                    //新模块不截取数据，直接使用原始曲线数据进行计算
                    //直接返回CT值
                    if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20)
                    {
                        FileUtils.Log(string.Format("新模块数据解析开始"));

                        double[] a = data;
                        double[] b = new double[data.Length];

                        //滤波
                        MyFFT.fft(data.Length, a, b, 1);
                        int ij = data.Length / 6;
                        int ii = data.Length - ij;
                        for (int j = ij; j < ii; j++) a[j] = b[j] = 0;
                        MyFFT.fft(data.Length, a, b, -1);
                        FileUtils.Log(string.Format("滤波完成"));

                        //平滑处理
                        data = wave.DB4DWT(data);
                        data = wave.DBFLT(data);
                        data = wave.DB4DWT(data);
                        data = wave.DBFLT(data);
                        double[] td = new double[data.Length * 2];
                        Array.Copy(data, 0, td, 0, data.Length);
                        data = wave.DB4IDWT(td);
                        td = new double[data.Length * 2];
                        Array.Copy(data, 0, td, 0, data.Length);
                        data = wave.DB4IDWT(td);
                        FileUtils.Log(string.Format("平滑处理完成"));

                        val = DyUtils.dyMath(data);
                        _ctValues.Add(val);
                        DyUtils.doubles[DyUtils.doubles.Length - 1] = 0;
                        DyUtils.doubles[DyUtils.doubles.Length - 2] = 1;
                        _curveDatas.Add(Global.IsShowValue ? originalData : DyUtils.doubles);

                        //输出有效波
                        int[][] indexs = DyUtils.indexs;
                        string str = string.Format("总点数：{0}", data.Length);
                        FileUtils.Log(string.Format("输出有效波：" + str));

                        for (int i = 0; i < indexs.Length; i++)
                        {
                            str += string.Format("\r\n第{0}个波：", i + 1);
                            for (int j = 0; j < indexs[i].Length; j++)
                            {
                                str += " " + indexs[i][j];
                            }
                        }
                        if (Global.IsShowValue) txtCalcCT.AppendText(str + "\r\n");
                        continue;
                    }

                    //旧模块返回曲线数据
                    int[] idx = new int[2];
                    //idx[0] = 25; //idx[1] = 70;
                    //if (idx != null && data.Length > 80)
                    //{
                    //    //len = idx[1] - idx[0] + 2;
                    //    len = data.Length - idx[0] - 10;
                    //    val = new double[len];
                    //    Array.ConstrainedCopy(data, idx[0], val, 0, len);//len - 2
                    //}
                    //else
                    //{
                    //    val = new double[45];
                    //    for (int i = 0; i < val.Length; i++)
                    //    {
                    //        val[i] = 10000;
                    //    }
                    //}

               //-----旧模块数据改算法 20180725---------------
                    double[] ConvertD = new double[data.Length];
                    idx[0] = 25;
                    len = data.Length - idx[0] - 20;
                    double[] ConvertR = new double[len];

                    ConvertD = DyUtils.getMovingAverage(data, 10);
                    if (idx != null && ConvertD.Length > 50)
                    {
                        Array.ConstrainedCopy(ConvertD, idx[0], ConvertR, 0, len);//len - 2
                    }

                    string showdata = "";
                    for (int k = 0; k < ConvertR.Length; k++)
                    {
                        showdata += showdata == "" ? ConvertR[k].ToString() : "," + ConvertR[k].ToString();
                    }

                    FileUtils.OprLog(2, "截取新算法胶体金返回原始数据", showdata);//写入配置文件
                    showdata = "";
                    for (int k = 0; k < ConvertD.Length; k++)
                    {
                        showdata += showdata == "" ? ConvertD[k].ToString() : "," + ConvertD[k].ToString();
                    }

                    FileUtils.OprLog(2, "新算法胶体金转换后所有数据", showdata);//写入配置文件

                    //string oldStr = string.Empty;
                    //for (int i = 0; i < val.Length; i++)
                    //{
                    //    oldStr += oldStr.Length == 0 ? val[i].ToString() : "," + val[i].ToString();
                    //}
                    //FileUtils.OprLog(2, "胶体金曲线原始数据", oldStr);

                    ////多项式找趋势线
                    //double[] dxs = DyUtils.duoXiangShi(val, 2);
                    //oldStr = string.Empty;
                    //for (int i = 0; i < dxs.Length; i++)
                    //{
                    //    oldStr += oldStr.Length == 0 ? dxs[i].ToString() : "," + dxs[i].ToString();
                    //}
                    //FileUtils.OprLog(2, "胶体金多项式数据", oldStr);
                    ////用原始曲线-趋势线等到纠正后的线
                    //oldStr = string.Empty;
                    //for (int i = 0; i < val.Length; i++)
                    //{
                    //    val[i] -= dxs[i];
                    //    oldStr += oldStr.Length == 0 ? val[i].ToString() : "," + val[i].ToString();
                    //}
                    //FileUtils.OprLog(2, "胶体金多项式计算后数据", oldStr);
                    //double max = 0, min = 0;
                    //for (int i = 0; i < len - 2; i++)
                    //{
                    //    if (i == 0) max = min = val[i];
                    //    if (val[i] > max) max = val[i];
                    //    if (val[i] < min) min = val[i];
                    //}
                    //val[len - 1] = max + 2500;
                    //val[len - 2] = min - 2500;
                    _curveDatas.Add(Global.IsShowValue ? ConvertR : ConvertR);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "胶体金格式化数据", ex.ToString());
                MessageBox.Show("胶体金格式化数据时出现异常！\r\n" + ex.Message, "格式化数据", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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


        List<double> GLIST = null;
        //List<List<double>> GLISTS = new List<List<double>>();
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
                        
                        //DY3500I不用翻转
                        if(Global.Is3500I )
                        {
                            line = oldline;
                        }
                        else
                        {
                            for (int i = len - 1; i >= 0; i--)
                            {
                                line[index] = oldline[i];
                                index++;
                            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"摄像头格式化数据");
            }
        }

        /// <summary>
        /// 曲线平滑处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[] DB4DWT(double[] data)
        {
            data = wave.DB4DWT(data);
            data = wave.DBFLT(data);
            data = wave.DB4DWT(data);
            data = wave.DBFLT(data);
            double[] td = new double[data.Length * 2];
            Array.Copy(data, 0, td, 0, data.Length);
            data = wave.DB4IDWT(td);
            td = new double[data.Length * 2];
            Array.Copy(data, 0, td, 0, data.Length);
            data = wave.DB4IDWT(td);
            return data;
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
            ButtonUpdate.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private string msgStr = string.Empty;
        /// <summary>
        /// 计算结果
        /// </summary>
        public void CalcResult()
        {
            int num = 0;
            int sampleNum = _item.SampleNum;
            string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    string testInfo = string.Empty;
                    if (_item.Hole[i].Use)
                    {
                        if (i >= _ValueT.Count || i >= _ValueC.Count)
                        {
                            continue;
                        }
                        if (_ValueT[i] == null || _ValueC[i] == null)
                        {
                            continue;
                        }
                        string[] UnqualifiedValue = new string[4];
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);
                        //是否是新模块
                        bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20) || (Global.JtjVerTwo != null && Global.JtjVerTwo[1] >= 0x20) || (Global.JtjVerThree != null && Global.JtjVerThree[1] >= 0x20) || (Global.JtjVerFour != null && Global.JtjVerFour[1] >= 0x20);
                        
                        if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n是否新模块:{0};\r\n", IsNewModel));

                        #region 定性消线
                        if (_item.Method == 0)
                        {
                            double InvalidC = 0, MaxT = 0, MinT = 0;
                            if (Global.Is3500I)
                            {
                                InvalidC = _item.fourInvalidC ;
                                MaxT = _item.dxxx.fourMaxT [i];
                                MinT = _item.dxxx.fourMinT [i];
                            }
                            else if (IsNewModel)
                            {
                                //InvalidC = _item.newInvalidC > 0 ? _item.newInvalidC : _item.dxxx.newMaxT[i] * 2;
                                //2017年11月29日 修改内容：如果C值小于等于0，C值就取Min
                                if (Global.JtjVersion==2)//2.0模块
                                {
                                    InvalidC = _item.newInvalidC;//> 0 ? _item.newInvalidC : 0.05;//_item.dxxx.newMinT[i];//_item.dxxx.newMaxT[i] * 2;
                                    MaxT = _item.dxxx.newMaxT[i];
                                    MinT = _item.dxxx.newMinT[i];
                                }
                                else if(Global.JtjVersion==3)//3.0,模块
                                {
                                    InvalidC = _item.threeInvalidC ;
                                    MaxT = _item.dxxx.threeMaxT [i];
                                    MinT = _item.dxxx.threeMinT [i];
                                }
                               
                            }
                            else
                            {
                                //InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxxx.MaxT[i] * 2;
                                //2017年11月29日 修改内容：如果C值小于等于0，C值就取Min
                                InvalidC = _item.InvalidC;//> 0 ? _item.InvalidC : _item.dxxx.MinT[i];//_item.dxxx.newMaxT[i] * 2;
                                MaxT = _item.dxxx.MaxT[i];
                                MinT = _item.dxxx.MinT[i];
                                
                            }
                           
                            if (Global.IsShowValue)
                            {
                                txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));
                                if (Global.Is3500I)
                                {
                                    testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                    txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}\r\n", (i + 1),_ValueC[i].ToString("F4"), _ValueT[i].ToString("F4") ));
                                }
                                else
                                {
                                   
                                    //double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxxx.MaxT[i] * 2;
                                    testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                    txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}\r\n", (i + 1), _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4")));
                                }
                             
                            }

                            //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}\r\n", (i + 1), _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4")));
                            if (Global.Is3500I)//
                            {
                                if (_ValueC[i] <= InvalidC)
                                {
                                    _listCheckResult[i].Text = "检测无效";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "检测无效";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "检测无效";
                                }
                                else if (_ValueT[i] > MaxT)
                                {
                                    _listCheckResult[i].Text = "阴性";
                                    _listResult[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else if (_ValueT[i] < MinT)
                                {
                                    _listCheckResult[i].Text = "阳性";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "不合格";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "不合格";
                                }
                                else
                                {
                                    _listCheckResult[i].Text = "可疑";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "可疑";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "可疑";
                                }
                                if (Global.IsShowValue) testInfo += string.Format(";检测结果:{0};", UnqualifiedValue[0]);
                            }
                            else if (IsNewModel)
                            {
                                if (_ValueC[i] <= InvalidC)
                                {
                                    _listCheckResult[i].Text = "检测无效";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "检测无效";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "检测无效";
                                }
                                else if (_ValueT[i] > MaxT)
                                {
                                    _listCheckResult[i].Text = "阴性";
                                    _listResult[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else if (_ValueT[i] < MinT)
                                {
                                    _listCheckResult[i].Text = "阳性";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "不合格";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "不合格";
                                }
                                else
                                {
                                    _listCheckResult[i].Text = "可疑";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "可疑";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "可疑";
                                }
                                if (Global.IsShowValue) testInfo += string.Format(";检测结果:{0};", UnqualifiedValue[0]);
                                
                                
                            }
                            else
                            {
                                if (_ValueT[i] == null || _ValueC[i] == null)
                                {
                                    continue;
                                }
                                //新算法判定
                                if (_ValueC[i] <= InvalidC)
                                {
                                    _listCheckResult[i].Text = "检测无效";
                                    _listCheckResult[i].FontWeight = FontWeights.Bold;
                                    _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    _listResult[i].Text = "检测无效";
                                    _listResult[i].FontWeight = FontWeights.Bold;
                                    _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                    UnqualifiedValue[0] = "检测无效";
                                }
                                else
                                {
                                    if (_ValueT[i] > MaxT)
                                    {
                                        _listCheckResult[i].Text = "阴性";
                                        _listResult[i].Text = "合格";
                                        UnqualifiedValue[0] = "合格";
                                    }
                                    else if (_ValueT[i] < MinT)
                                    {
                                        _listCheckResult[i].Text = "阳性";
                                        _listCheckResult[i].FontWeight = FontWeights.Bold;
                                        _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                        _listResult[i].Text = "不合格";
                                        _listResult[i].FontWeight = FontWeights.Bold;
                                        _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                        UnqualifiedValue[0] = "不合格";
                                    }
                                    else
                                    {
                                        _listCheckResult[i].Text = "可疑";
                                        _listCheckResult[i].FontWeight = FontWeights.Bold;
                                        _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                        _listResult[i].Text = "可疑";
                                        _listResult[i].FontWeight = FontWeights.Bold;
                                        _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                        UnqualifiedValue[0] = "可疑";
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 定性比色
                        //2016年10月13日 wenj 
                        //新版本判定方法：Abs（T/C）≥Abs时 C＞T阳性 C＜T阴性；Abs（C/T）＜Abs时 SexIdx=0阴性 else 阳性
                        else if (_item.Method == 1)
                        {
                            if (_ValueT[i] == null || _ValueC[i] == null)
                            {
                                _listCheckResult[i].Text = "可疑";
                                _listCheckResult[i].FontWeight = FontWeights.Bold;
                                _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                _listResult[i].Text = "可疑";
                                _listResult[i].FontWeight = FontWeights.Bold;
                                _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                UnqualifiedValue[0] = "可疑";

                                continue;
                            }
                            double InvalidC = 0, MaxT = 0, MinT = 0;
                            //2017年11月30日 C:T改为T:C
                            float ctAbs;
                            if (Global.Is3500I)
                            {
                                ctAbs = (float)(_ValueT[i] / _ValueC[i]);
                            }
                            else
                            {
                                ctAbs = (float)(_ValueT[i] / _ValueC[i]);// (float)(_ValueC[i] / _ValueT[i]);
                            }

                            if (Global.Is3500I )//3500I
                            {
                                InvalidC = _item.fourInvalidC ;
                                MaxT = _item.dxbs.fourMaxT [i];
                                MinT = _item.dxbs.fourMinT [i];
                            }
                            else if (IsNewModel)
                            {
                                //InvalidC = _item.newInvalidC > 0 ? _item.newInvalidC : _item.dxbs.newMaxT[i] * 2;
                                //2017年11月29日 修改内容：如果C值小于等于0，C值就取Min
                                if (Global.JtjVersion == 2)//2.0模块
                                {
                                    InvalidC = _item.newInvalidC;// > 0 ? _item.newInvalidC : 0.05;//_item.dxbs.newMinT[i];//_item.dxxx.newMaxT[i] * 2;
                                    MaxT = _item.dxbs.newMaxT[i];
                                    MinT = _item.dxbs.newMinT[i];
                                }
                                else if(Global.JtjVersion == 3)//3.0
                                {
                                    InvalidC = _item.threeInvalidC;
                                    MaxT = _item.dxbs.threeMaxT[i];
                                    MinT = _item.dxbs.threeMinT[i];
                                }
                               
                            }
                             else
                             {
                                 //InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxxx.MaxT[i] * 2;
                                 //2017年11月29日 修改内容：如果C值小于等于0，C值就取Min
                                 InvalidC = _item.InvalidC;//> 0 ? _item.InvalidC : _item.dxxx.MinT[i];//_item.dxxx.newMaxT[i] * 2;
                                 MaxT = _item.dxbs .MaxT[i];
                                 MinT = _item.dxbs .MinT[i];

                             }
                            if (Global.IsShowValue)
                            {
                                if (Global.Is3500I)
                                {
                                    txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));
                                    testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                    //double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxbs.MaxT[i] * 2;
                                    txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}  T:C:{3} \r\n", (i + 1),
                                        _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4"), ctAbs.ToString("F4")));
                                    //txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));
                                    //testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                    ////double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxbs.MaxT[i] * 2;
                                    //txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}  T:C:{3} \r\n", (i + 1),
                                    //    _ValueT[i].ToString("F4"), _ValueC[i].ToString("F4"), ctAbs.ToString("F4")));
                                }
                                else
                                {
                                    txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));
                                    testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                    //double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxbs.MaxT[i] * 2;
                                    txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}  T:C:{3} \r\n", (i + 1),
                                        _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4"), ctAbs.ToString("F4")));
                                }
                               
                            }

                            if (_ValueC[i] <= InvalidC)
                            {
                                _listCheckResult[i].Text = "检测无效";
                                _listCheckResult[i].FontWeight = FontWeights.Bold;
                                _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                _listResult[i].Text = "检测无效";
                                _listResult[i].FontWeight = FontWeights.Bold;
                                _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                UnqualifiedValue[0] = "检测无效";
                            }
                            else if (ctAbs < MinT)
                            {
                                _listCheckResult[i].Text = "阳性";
                                _listCheckResult[i].FontWeight = FontWeights.Bold;
                                _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                _listResult[i].Text = "不合格";
                                _listResult[i].FontWeight = FontWeights.Bold;
                                _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                UnqualifiedValue[0] = "不合格";
                            }
                            else if (ctAbs > MaxT)
                            {
                                _listCheckResult[i].Text = "阴性";
                                _listResult[i].Text = "合格";
                                UnqualifiedValue[0] = "合格";
                            }
                            else
                            {
                                _listCheckResult[i].Text = "可疑";
                                _listCheckResult[i].FontWeight = FontWeights.Bold;
                                _listCheckResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                _listResult[i].Text = "可疑";
                                _listResult[i].FontWeight = FontWeights.Bold;
                                _listResult[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                UnqualifiedValue[0] = "可疑";
                            }
                           
                            if (Global.IsShowValue) testInfo += string.Format(";检测结果:{0};", UnqualifiedValue[0]);
                        }
                        #endregion

                        #region 定量（T）
                        //a+bx+cxx+dxxx  < PPB 阴性 x=T
                        //else if (_item.Method == 2)
                        //{
                        //    if (_ValueC[i] <= _item.InvalidC)
                        //    {
                        //        _listCheckResult[i].Text = "检测无效";
                        //        _listResult[i].Text = "检测无效";
                        //        UnqualifiedValue[0] = "检测无效";
                        //    }
                        //    else if (value < _item.dlt.Limit)
                        //    {
                        //        _listCheckResult[i].Text = "阴性";
                        //        _listResult[i].Text = "合格";
                        //        UnqualifiedValue[0] = "合格";
                        //    }
                        //    else
                        //    {
                        //        _listCheckResult[i].Text = "阳性";
                        //        _listResult[i].Text = "不合格";
                        //        UnqualifiedValue[0] = "不合格";
                        //    }
                        //}
                        #endregion

                        #region 定量（T/C）
                        //a+bx+cxx+dxxx  < PPB 阴性 x=T/C
                        //else if (_item.Method == 3)
                        //{
                        //    if (_ValueC[i] <= _item.InvalidC)
                        //    {
                        //        _listCheckResult[i].Text = "检测无效";
                        //        _listResult[i].Text = "检测无效";
                        //        UnqualifiedValue[0] = "检测无效";
                        //    }
                        //    else if (value < _item.dltc.Limit)
                        //    {
                        //        _listCheckResult[i].Text = "阴性";
                        //        _listResult[i].Text = "合格";
                        //        UnqualifiedValue[0] = "合格";
                        //    }
                        //    else
                        //    {
                        //        _listCheckResult[i].Text = "阳性";
                        //        _listResult[i].Text = "不合格";
                        //        UnqualifiedValue[0] = "不合格";
                        //    }
                        //}
                        #endregion

                        int number = num > 0 ? (i - num) : i;
                        _CheckValue[number, 0] = String.Format("{0:D2}", (i + 1));
                        _CheckValue[number, 1] = "胶体金";
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
                        //if (Global.IsShowValue) _CheckValue[number, 14] = testInfo;
                        if (curveDatas[i] != null)
                        {
                            _CheckValue[number, 15] = curveDatas[i] + (Global.JtjVersion == 3 ? "," + imgDatas[i] : "");
                        }
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].SampleTypeName;//样品种类编号
                        _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.testPro;//检测项目编号
                        _CheckValue[(num > 0 ? (i - num) : i), 18] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                        _CheckValue[(num > 0 ? (i - num) : i), 19] = _item.Hole[i].CompanyCode ; //检测结果编号 dataNum
                        _CheckValue[(num > 0 ? (i - num) : i), 20] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                        _CheckValue[(num > 0 ? (i - num) : i), 21] = _item.Hole[i].ProduceCompany;//生产单位

                        _CheckValue[(num > 0 ? (i - num) : i), 22] = _item.Hole[i].projectCode ;
                        _CheckValue[(num > 0 ? (i - num) : i), 23] = _item.Hole[i].projectName ;
                        _CheckValue[(num > 0 ? (i - num) : i), 24] = _item.Hole[i].StallCode ;
                        _CheckValue[(num > 0 ? (i - num) : i), 25] = _item.Hole[i].stall ;
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
                FileUtils.OprLog(2, "计算结果", ex.ToString());
                //MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "计算结果");
                MessageBox.Show("请重新检测" , "结果提示");
            }
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private void DrawingCurve()
        {
            if (_curveDatas == null) return;

            try
            {
                curveDatas.Clear();
                string curveData = string.Empty;
                for (int i = 0; i < _curveDatas.Count; i++)
                {
                    curveData = string.Empty;
                    _ValueC.Add(0);
                    _ValueT.Add(0);
                    if (_curveDatas[i] == null)
                    {
                        curveDatas.Add(string.Empty);
                        continue;
                    }
                    double[] data = _curveDatas[i];

                    for (int k = 0; k < 2; k++)//0为处理后的曲线，1为原始曲线
                    {
                        if (k == 1) continue;
                        data = k == 0 ? _curveDatas[i] : oldData[i];
                        int len = data.Length;
                        DateTime[] dates = new DateTime[len];
                        double[] numberOpen = new double[len];

                        int[] xd = new int[len];//横坐标

                        for (int j = 0; j < len; ++j)
                        {
                            xd[j] = j+1;
                            dates[j] = Convert.ToDateTime("01/01/000" + (j + 1));
                            numberOpen[j] = k == 0 ? data[j] : (double)data[j] / 10000;
                            curveData += curveData.Length == 0 ? data[j].ToString() : string.Format("|{0}", data[j]);
                        }

                        curveDatas.Add(curveData);
                        //var datesDataSource = new EnumerableDataSource<DateTime>(dates);
                        //datesDataSource.SetXMapping(x => _dateAxis[i < 2 ? i : 2].ConvertToDouble(x));

                        var datesDataSource = new EnumerableDataSource<int>(xd);
                        datesDataSource.SetXMapping(x => x);

                        var numberOpenDataSource = new EnumerableDataSource<double>(numberOpen);
                        numberOpenDataSource.SetYMapping(y => y);
                        CompositeDataSource compositeDataSource = new CompositeDataSource(datesDataSource, numberOpenDataSource);
                        _plotters[i].AddLineGraph(compositeDataSource,
                                           new Pen(Brushes.Blue, 1),
                                           new CirclePointMarker { Size = 3, Fill = k == 0 ? Brushes.Red : Brushes.Blue },
                                           null);
                        _plotters[i ].Viewport.FitToView();
                        //_plotters[i < 2 ? i : 2].AddLineGraph(compositeDataSource,
                        //                    new Pen(Brushes.Blue, 1),
                        //                    new CirclePointMarker { Size = 3, Fill = k == 0 ? Brushes.Red : Brushes.Blue },
                        //                    null);
                        //_plotters[i < 2 ? i : 2].Viewport.FitToView();
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "显示曲线错误", ex.ToString());
                MessageBox.Show(ex.Message,"显示曲线");
            }
        }

        /// <summary>
        /// 曲线计算
        /// </summary>
        public void CalcCurve()
        {
            try
            {
                bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20) || (Global.JtjVerTwo != null && Global.JtjVerTwo[1] >= 0x20) || (Global.JtjVerThree != null && Global.JtjVerThree[1] >= 0x20) || (Global.JtjVerFour != null && Global.JtjVerFour[1] >= 0x20);

                if (IsDebug && debugData != null)
                {
                    _curveDatas = new List<double[]>();
                    _curveDatas.Add(debugData);
                }

                if (_curveDatas == null) return;

                for (int i = 0; i < _curveDatas.Count; i++)
                {
                    _ValueC.Add(0); _ValueT.Add(0);
                    if (_curveDatas[i] != null)
                    {
                        //胶体金扫描新模块
                        if (IsNewModel)
                        {
                            _ValueC[i] = _ctValues[i][0];
                            _ValueT[i] = _ctValues[i][1];
                            continue;
                        }
                        else if( Global.Is3500I)
                        {
                            _ValueC[i] = _ctValues[i][0];
                            _ValueT[i] = _ctValues[i][1];
                            continue;
                        }

                        double[] datas = _curveDatas[i];
                        int CMinIndex = 0, CMaxIndex1 = 0, CMaxIndex2 = 0,
                            TMinIndex = 0, TMaxIndex1 = 0, TMaxIndex2 = 0,
                            len = datas.Length;
                        double area = 0, slope = 0, difference = 0, efficientPoint = 0;

                        #region 旧模块新算法 20180725
                        int[][] ctIdxs = DyUtils.getWaveInfo(datas, 0, 5);//返回波谷信息
                        int strartC = -1;
                        int C = -1;
                        int endC = -1;
                        int strartT = -1;
                        int T = -1;
                        int endT = -1;
                        if (ctIdxs != null && ctIdxs.GetLength(0) == 2)//有C/T
                        {
                            //找出C波谷值坐标
                            strartC = ctIdxs[0][0];
                            C = ctIdxs[0][1];
                            endC = ctIdxs[0][2];

                            //找出T值坐标
                            strartT = ctIdxs[1][0];
                            T = ctIdxs[1][1];
                            endT = ctIdxs[1][2];
                        }
                        else if (ctIdxs != null && ctIdxs.GetLength(0) == 1)//只有C或T
                        {
                            if (ctIdxs[0][2] < 24)//在0~28前面的的是C坐标
                            {
                                strartC = ctIdxs[0][0];
                                C = ctIdxs[0][1];
                                endC = ctIdxs[0][2];
                            }
                            else//40后面的是T坐标
                            {
                                strartT = ctIdxs[0][0];
                                T = ctIdxs[0][1];
                                endT = ctIdxs[0][2];
                            }
                        }

                        #endregion

                        //旧模块
                        #region 找波谷最低点新方法

                        //确定C的坐标，用6个点来确定C的位置
                        //int[][] ctIdxs = DyUtils.getWaveInfo(datas, 0, _item.pointCNum);
                        //int cnum = 0;
                        //if (ctIdxs != null && ctIdxs.Length > 0)
                        //{
                        //    //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("C-点数:{0};波谷数:{1};", _item.pointCNum, ctIdxs.Length));
                        //    for (int j = 0; j < ctIdxs.Length; j++)
                        //    {
                        //        if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("{0}  ", ctIdxs[j][1]));
                        //        if (ctIdxs[j][1] < datas.Length / 2) cnum++;
                        //    }
                        //    if (cnum == 0 || (cnum == 1 && ctIdxs[0][1] < 8)) return;//如果一个都没有或者不在有效范围内，为无效卡
                        //    else if (cnum == 1)
                        //    {
                        //        CMinIndex = ctIdxs[0][1] - 1;
                        //        CMaxIndex1 = ctIdxs[0][0] - 1 < 0 ? 0 : ctIdxs[0][0] - 1;
                        //        CMaxIndex2 = ctIdxs[0][2] - 1;
                        //        if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("C-Index:{0};", CMinIndex));
                        //    }
                        //}

                        ////找T的波谷
                        //ctIdxs = DyUtils.getWaveInfo(datas, 0, _item.pointTNum);
                        //if (ctIdxs != null && ctIdxs.Length > 0)
                        //{
                        //    if (Global.IsShowValue)
                        //    {
                        //        //txtCalcCT.AppendText(string.Format("\r\nT-点数:{0};波谷数{1};", _item.pointTNum, ctIdxs.Length));
                        //        for (int j = 0; j < ctIdxs.Length; j++)
                        //            txtCalcCT.AppendText(string.Format("{0}  ", ctIdxs[j][1]));
                        //    }

                        //    if (CMinIndex > 0)//已经确定C波谷坐标
                        //    {
                        //        for (int j = 0; j < ctIdxs.Length; j++)
                        //        {
                        //            int tidx = ctIdxs[j][1];
                        //            if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20)
                        //            {
                        //                if (tidx - CMinIndex <= 35) continue;
                        //            }
                        //            else
                        //            {
                        //                if (tidx < datas.Length / 2) continue;
                        //            }
                        //            if (16 < System.Math.Abs(CMinIndex - tidx) && 25 > System.Math.Abs(CMinIndex - tidx))
                        //            {
                        //                TMinIndex = tidx - 1;
                        //                TMaxIndex1 = ctIdxs[j][0] - 1;
                        //                TMaxIndex2 = ctIdxs[j][2] - 1;
                        //                //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("T-Index:{0};", TMinIndex));
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    else//找到两个或以上波谷坐标时
                        //    {
                        //        for (int j = 0; j < ctIdxs.Length; j++)
                        //        {
                        //            int cidx = ctIdxs[j][1];
                        //            if (cidx > datas.Length / 2 || cidx < 8) continue;//C坐标超过一半的值将不比较
                        //            for (int k = 0; k < ctIdxs.Length; k++)
                        //            {
                        //                int tidx = ctIdxs[k][1];
                        //                if (tidx < datas.Length / 2) continue;//T坐标超过一半的值才比较
                        //                if (16 <= System.Math.Abs(cidx - tidx) && 25 >= System.Math.Abs(cidx - tidx))
                        //                {
                        //                    CMinIndex = ctIdxs[j][1] - 1;
                        //                    CMaxIndex1 = ctIdxs[j][0] - 1 < 0 ? 0 : ctIdxs[j][0] - 1;
                        //                    CMaxIndex2 = ctIdxs[j][2] - 1;
                        //                    TMinIndex = ctIdxs[k][1] - 1;
                        //                    TMaxIndex1 = ctIdxs[k][0] - 1;
                        //                    TMaxIndex2 = ctIdxs[k][2] - 1;
                        //                    if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("C-Index:{0};T-Index:{1};", CMinIndex, TMinIndex));
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #region C线计算
                        //面积6个点(3~6)，斜率8个点(3~8)，有效点8个(连续上升/下降)，差值（波峰(C往左边推取最大值)-波谷）
                        //if (CMinIndex > 0)
                        //{
                        //    //scope为有效范围，坐标0为最小范围，坐标1为最大范围
                        //    int[] scope = new int[2];
                        //    scope[0] = CMaxIndex1 = CMinIndex - CMaxIndex1 > _item.maxPoint ? CMinIndex - _item.maxPoint : CMaxIndex1;
                        //    scope[1] = CMaxIndex2 = CMaxIndex2 - CMinIndex > _item.maxPoint ? CMinIndex + _item.maxPoint : CMaxIndex2;
                        //    //double standard = 0, proportion = 0;

                        //    ////曲线面积
                        //    //standard = 5000;//面积标准为5000
                        //    //proportion = 0.4;//面积权重40%
                        //    //double area = Global.JtjCheck.CalcCTVal(datas, CMinIndex, scope, "A");
                        //    //txtCalcCT.AppendText(string.Format("C面积：{0}；", Math.Round(area, 2)));
                        //    ////area = area > standard ? standard : area;
                        //    //area = area / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                        //    //斜率 以波谷T为原点往C值方向遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                        //    //standard = 200;//斜率标准为100
                        //    //proportion = 1;//斜率权重25%
                        //    slope = Global.JbkCheckCalc.CalcCTVal(datas, CMinIndex, scope, "X");
                        //    //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\nC-斜率:{0}；", Math.Round(slope, 2)));

                        //    //slope = slope > standard ? standard : slope;
                        //    //slope = slope / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                        //    //差值 以波谷T为原点往C值方向遍历，找到波峰的值
                        //    //standard = 2000;//差值标准为2000
                        //    //proportion = 5;//差值权重25%
                        //    //difference = Global.JtjCheck.CalcCTVal(datas, CMinIndex, scope, "C");
                        //    //txtCalcCT.AppendText(string.Format(" 差值:{0}；", Math.Round(difference, 2)));
                        //    ////difference = difference > standard ? standard : difference;
                        //    //difference = difference / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                        //    ////有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                        //    //standard = _item.maxPoint;//有效点标准为8
                        //    //proportion = 0.1;//有效点权重10%
                        //    //efficientPoint = Global.JtjCheck.CalcCTVal(datas, CMinIndex, scope, "P");
                        //    //txtCalcCT.AppendText(string.Format(" 有效点:{0}；", Math.Round(efficientPoint, 2)));
                        //    ////efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                        //    //efficientPoint = efficientPoint / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));

                        //    _ValueC[i] = (area + slope + difference + efficientPoint);
                        //    //txtCalcCT.AppendText(string.Format(" 结果:{0}%\r\n", Math.Round(_ValueC[i], 2)));
                        //}
                        //新算法
                        if (endC != -1 && C != -1)
                        {
                            string rdata = string.Format("datas[C + 1] - datas[C]的值{0}，datas[endC] - datas[C]的值{1},datas[endC + 1] - datas[C]的值{2}", datas[C + 1] - datas[C], datas[endC] - datas[C], datas[endC + 1] - datas[C]);
                            FileUtils.OprLog(2, "C值", rdata);
                            if ((datas[C + 1] - datas[C]) < 5 && datas[endC + 1] > datas[endC])
                            {
                                _ValueC[i] = datas[endC + 1] - datas[C];//C值取波谷结束点与波谷的差值
                            }
                            else
                            {
                                _ValueC[i] = datas[endC] - datas[C];
                            }
                        }
                        else
                        {
                            _ValueC[i] = 0;
                        }
                        if (endC != -1 && C != -1)
                        {
                            //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} C波谷起始值:{1} ；C波谷起始坐标{2}；",i+1, datas[strartC], strartC+1));
                            if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} C波谷值:{1} ；C波谷坐标{2}；", i + 1, datas[C], C));
                            //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} C波谷结束值:{1} ；C波谷结束值坐标{2}；", i+1,datas[endC], endC+1));
                            if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} C差值:{1} ", i + 1, _ValueC[i]));
                        }

                        #endregion

                        #region T线计算
                        //面积6个点(3~6)，斜率8个点(3~8)，有效点8个(连续上升/下降)，差值（波峰(T往C方向推取最大值)-波谷）
                        //if (TMinIndex > 0)
                        //{
                        //    //scope为有效范围，坐标0为最小范围，坐标1为最大范围
                        //    int[] scope = new int[2];
                        //    scope[0] = TMaxIndex1 = TMinIndex - TMaxIndex1 > _item.maxPoint ? TMinIndex - _item.maxPoint : TMaxIndex1;
                        //    scope[1] = TMaxIndex2 = TMaxIndex2 - TMinIndex > _item.maxPoint ? TMinIndex + _item.maxPoint : TMaxIndex2;
                        //    //double standard = 0, proportion = 0;

                        //    ////原始曲线面积
                        //    //standard = 5000;//面积标准为5000
                        //    //proportion = 0.4;//面积权重40%
                        //    //double area = Global.JtjCheck.CalcCTVal(datas, TMinIndex, scope, "A");
                        //    //txtCalcCT.AppendText(string.Format("T面积：{0}；", Math.Round(area, 2)));
                        //    ////area = area > standard ? standard : area;
                        //    //area = area / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                        //    //规则曲线面积
                        //    //double rulesArea = Global.JtjCheck.GetRulesArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);

                        //    //斜率 以波谷C为原点往前遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                        //    //standard = 200;//斜率
                        //    //proportion = 0.5;//斜率权重25%
                        //    slope = Global.JbkCheckCalc.CalcCTVal(datas, TMinIndex, scope, "X");
                        //    //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("T-斜率:{0}；", Math.Round(slope, 2)));

                        //    //slope = slope > standard ? standard : slope;
                        //    //slope = slope / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                        //    ////差值 以波谷T为原点往C值方向遍历，找到波峰的值
                        //    //standard = 2000;//差值标准为2000
                        //    //proportion = 0.5;//差值权重25%
                        //    //difference = Global.JtjCheck.CalcCTVal(datas, TMinIndex, scope, "C");
                        //    //txtCalcCT.AppendText(string.Format(" 差值:{0}；", Math.Round(difference, 2)));
                        //    ////difference = difference > standard ? standard : difference;
                        //    //difference = difference / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                        //    ////有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                        //    //standard = _item.maxPoint;//有效点标准为8
                        //    //proportion = 0.1;//有效点权重10%
                        //    //efficientPoint = Global.JtjCheck.CalcCTVal(datas, TMinIndex, scope, "P");
                        //    //txtCalcCT.AppendText(string.Format(" 有效点:{0}；", Math.Round(efficientPoint, 2)));
                        //    ////efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                        //    //efficientPoint = efficientPoint / standard * proportion;
                        //    //txtCalcCT.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));

                        //    _ValueT[i] = (area + slope + difference + efficientPoint);

                        //    //txtCalcCT.AppendText(string.Format(" 结果:{0}%\r\n\r\n", Math.Round(_ValueT[i], 2)));
                        //    if (Global.IsShowValue) txtCalcCT.AppendText("\r\n");
                        //}  
                        //新算法
                        if (strartT != -1 && T != -1)
                        {
                            string rdata = string.Format("datas[T + 1] - datas[T]的值{0}，datas[strartT] - datas[T]的值{1},datas[strartT+1] - datas[T]的值{2}", datas[T + 1] - datas[T], datas[strartT] - datas[T], datas[strartT + 1] - datas[T]);
                            FileUtils.OprLog(2, "T值", rdata);
                            if ((datas[T + 1] - datas[T]) < 5 && datas[strartT + 1] > datas[strartT])
                            {
                                _ValueT[i] = datas[strartT + 1] - datas[T];//T值取波谷开始点与波谷的差值
                            }
                            else
                            {
                                _ValueT[i] = datas[strartT] - datas[T];//T值取波谷开始点与波谷的差值
                            }
                        }
                        else
                        {
                            _ValueT[i] = 0;
                        }
                        if (strartT != -1 && T != -1)
                        {
                            //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} T波谷起始值:{1} ；T波谷起始坐标{2}；", i+1,datas[strartT], strartT+1));
                            if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} T波谷值:{1} ；T波谷坐标{2}；", i + 1, datas[T], T));
                            //if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} T波谷结束值:{1} ；T波谷结束值坐标{2}；",i+1, datas[endT], endT+1));
                            if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n 通道{0} T差值:{1} ", i + 1, _ValueT[i]));
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "曲线计算异常", ex.ToString());
                MessageBox.Show(ex.Message,"曲线计算");
            }
        }

        private void CalcCT(int point, int cz)
        {
            for (int i = 0; i < _curveDatas.Count; i++)
            {
                if (_curveDatas[i] != null)
                {
                    double[] datas = _curveDatas[i];
                    int[][] indexList = DyUtils.getWaveInfo(datas, cz, point);
                    if (indexList != null && indexList.Length > 0)
                    {
                        txtCalcCT.AppendText(string.Format("\r\n找到{0}个波谷：", indexList.Length));
                        for (int j = 0; j < indexList.Length; j++)
                        {
                            txtCalcCT.AppendText(string.Format("{0}  ", indexList[j][1]));
                        }
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _curveDatas = null;
            UpdateItem();
            _msgThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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

        private List<PrintHelper.Report> GenerateReports()
        {
            List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
            try
            {
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
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
                                Company = models[0].CheckedCompany
                            };
                            if(Global.Is3500I )
                            {
                                for (int i = models.Count-1; i >=0; i--)
                                {
                                    model.SampleName.Add(models[i].FoodName);
                                    model.SampleNum.Add(String.Format("{0:D5}", models[i].SampleCode.Length > 20 ? models[i].SampleCode.Substring(models[i].SampleCode.Length - 5, 5) : models[i].SampleCode));
                                    model.JudgmentTemp.Add(models[i].Result);
                                    model.Result.Add(models[i].CheckValueInfo);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < models.Count; i++)
                                {
                                    model.SampleName.Add(models[i].FoodName);
                                    model.SampleNum.Add(String.Format("{0:D5}", models[i].SampleCode.Length > 20 ? models[i].SampleCode.Substring(models[i].SampleCode.Length - 5, 5) : models[i].SampleCode));
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
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("打印失败!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
            rootCanvas.Visibility = Global.JtjVersion == 3 || Global.Is3500I  ? Visibility.Visible : Visibility.Collapsed;

            Rectangle rect = new Rectangle()
            {
                Width = Global.nHelpBoxWidth,
                Height = Global.nHelpBoxHeight,
                StrokeThickness = Global.nHelpBoxLineWidth,
                Stroke = Brushes.Red,
                Visibility = Global.JtjVersion == 3 || Global.Is3500I ? Visibility.Visible : Visibility.Collapsed,
                Name = "rect",
                Tag = channel
            };
            if (channel == 0)
            {
                rect.SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft1);
                rect.SetValue(Canvas.TopProperty, Global.nHelpBoxTop1 );
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
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 200 : 445,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "spContent"
            };

            WrapPanel wrapPannelSample = new WrapPanel
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 200 : 445,
                Height = Global.JtjVersion == 3 || Global.Is3500I ? 45 : 30
            };

            Label labelSampleName = new Label
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleName = new TextBox
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text =string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            wrapPannelSample.Children.Add(labelSampleName);
            wrapPannelSample.Children.Add(textBoxSampleName);
            spContent.Children.Add(wrapPannelSample);

            WrapPanel wrapPannelSampleNum = new WrapPanel
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 200 : 445,
                Height = Global.JtjVersion == 3 || Global.Is3500I ? 45 : 30
            };

            Label labelSampleNum = new Label
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品编号:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleNum = new TextBox
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
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
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 200 : 445,
                Height = Global.JtjVersion == 3 || Global.Is3500I ? 45 : 30
            };

            Label labelDetectResult = new Label
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxDetectResult = new TextBox
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
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
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 200 : 445,
                Height = Global.JtjVersion == 3 || Global.Is3500I ? 45 : 30
            };

            Label labelJudgment = new Label
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 80 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "判定结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textJugmentResult = new TextBox
            {
                Width = Global.JtjVersion == 3 || Global.Is3500I ? 115 : 125,
                Height = 26,
                Margin = Global.JtjVersion == 3 || Global.Is3500I ? new Thickness(0, 5, 0, 5) : new Thickness(0, 2, 0, 0),
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

        private UIElement GenerateResultLayout_3500I(int channel, string sampleNum, string sampleName)
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
            if (Global.Is3500I)
                return;
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
            if (Global.Is3500I) return;
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
                        newTop = rootCanvas.Height - item.Height-10;
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
            if (Global.Is3500I) return;
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

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }

        private void UpdateItem()
        {
            // 更新样品编号
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
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
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
                catch (Exception ex)
                {
                    FileUtils.OprLog(2, wnd.logType, ex.ToString());
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
                            FileUtils.OprLog(6, "UploadData", msg.outError);

                            Global.IsStartUploadTimer = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #region-- UpDataNowing
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }
            Upload();
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
                LabelInfo.Content = "正在上传...";
                _resultTable = new tlsttResultSecondOpr();
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
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
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("上传时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            }
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

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int point = 0, cz = 0;
            string val = txtC.Text.Trim();
            int.TryParse(val, out point);
            val = txtCZ.Text.Trim();
            int.TryParse(val, out cz);

            CalcCT(point, cz);
        }

        /// <summary>
        /// 双击显示T值临界值设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            JtjEditItemWindow window = new JtjEditItemWindow
            {
                _item = _item
            };
            window.BtnSettingAll.Visibility = Visibility;
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

    }
}