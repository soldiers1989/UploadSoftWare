using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using AIO.src;
using System.Windows.Input;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Text;
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
        public KJFWJTItem _item = null;
        public DYJTJItemPara _itemo = null;
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
        public int selHole = 0;
        /// <summary>
        /// 是否上传到广东省智慧平台 或者 同时上传至达元平台和智慧平台
        /// </summary>
        private bool IsUploadZHorALL = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        //3.0
        private List<ChartPlotter> _plotters = null;
        private List<HorizontalDateTimeAxis> _dateAxis = null;
        private List<TextBox> _listCheckResult = null;
        private List<TextBox> _listResult = null;
        private String _textBoxDetectResult = "textBoxDetectResult";
        private String _textJugmentResult = "textJugmentResult";
        /// <summary>
        /// 曲线数据
        /// </summary>
        private static List<double[]> _curveDatas;
        /// <summary>
        /// 存储新模块的CT值
        /// </summary>
        private List<double[]> _ctValues = null;
        private List<string> imgDatas = new List<string>();
        /// <summary>
        /// 胶体金新摄像头模块数据
        /// </summary>
        public List<byte[]>[] _newJtjDatas = null;
        /// <summary>
        /// 存储曲线数据
        /// </summary>
        private List<string> curveDatas = new List<string>();
        private List<double> _ValueC = new List<double>(), _ValueT = new List<double>();
        private List<double[]> oldData = new List<double[]>();
        private string[] syscodes = null;
        #endregion

        public JtjReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            _listDetectResult = new List<String>();
            _listStrRecord = new List<String>();
            _listJudmentValue = new List<TextBox>();
            _listCheckResult = new List<TextBox>();
            _listResult = new List<TextBox>();
            _plotters = new List<ChartPlotter>();
            _dateAxis = new List<HorizontalDateTimeAxis>();
            if(Global.JtjVersion==3)
                WrapPanelChannel.Width = 0;
           
            
            if (Global.ManuTest == true)
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
                   if(Global.JtjVersion ==3)
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
                           _CheckValue = new string[_HoleNumber, 21];
                           _HoleNumber++;
                       }
                       else
                       {
                           _plotters.Add(new ChartPlotter());
                           _dateAxis.Add(new HorizontalDateTimeAxis());
                           //element.Visibility = System.Windows.Visibility.Collapsed;
                           _listCheckResult.Add(null);
                           _listResult.Add(null);
                       }
                   }
                   else
                   {
                       UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                       WrapPanelChannel.Children.Add(element);
                       if (_item.Hole[i].Use)
                       {
                           sampleNum++;
                           _CheckValue = new string[_HoleNumber, 21];
                           _HoleNumber++;
                       }
                       else
                       {
                           element.Visibility = System.Windows.Visibility.Collapsed;
                       }
                   }
                    
                }
            }
            else
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
                    if (Global.JtjVersion == 3)
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
                            _CheckValue = new string[_HoleNumber, 19];
                            _HoleNumber++;
                        }
                        else
                        {
                            _plotters.Add(new ChartPlotter());
                            _dateAxis.Add(new HorizontalDateTimeAxis());
                            //element.Visibility = System.Windows.Visibility.Collapsed;
                            _listCheckResult.Add(null);
                            _listResult.Add(null);
                        }

                    }
                    else
                    {
                        UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        if (_item.Hole[i].Use)
                        {
                            sampleNum++;
                            _CheckValue = new string[_HoleNumber, 19];
                            _HoleNumber++;
                        }
                        else
                        {
               
                            element.Visibility = System.Windows.Visibility.Collapsed;
                        }

                    }
                }
                
                if (LoginWindow._userAccount.CheckSampleID == false)
                {
                    if (LoginWindow._userAccount.UpDateNowing)
                    {
                        btn_upload.IsEnabled = false;
                    }
                    else
                    {
                        btn_upload.IsEnabled = true;
                    }
                }
            }

            if(Global.JtjVersion ==3)
            {
                NewFormattingDatas();//格式化数据
                DrawingCurve();//绘制曲线
                CalcCurve();//曲线计算
                CalcResult();//计算结果并显示

            }
            else
            {
                if (Global.ManuTest == true)
                {
                    oShowResult();
                }
                else
                {
                    // 显示结果的时候，把字符串生成
                    ShowResult();
                }
            }

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

                    if (Global.JtjVersion == 3 )
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
                            xd[j] = j + 1;
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
                        _plotters[i].Viewport.FitToView();
                     
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "显示曲线错误", ex.ToString());
                MessageBox.Show(ex.Message, "显示曲线");
            }
        }
        public void CalcCurve()
        {
            try
            {
                  if (_curveDatas == null) return;

                  for (int i = 0; i < _curveDatas.Count; i++)
                  {
                      _ValueC.Add(0); _ValueT.Add(0);
                      if (_curveDatas[i] != null)
                      {
                          //胶体金扫描新模块
                            _ValueC[i] = _ctValues[i][0];
                            _ValueT[i] = _ctValues[i][1];
                            continue;
                      }
                  }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "曲线计算异常", ex.ToString());
                MessageBox.Show(ex.Message, "曲线计算");
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
                        //if (_ValueT[i] == null || _ValueC[i] == null)
                        //{
                        //    continue;
                        //}
                        string[] UnqualifiedValue = new string[4];
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.item );
                        //是否是新模块
                        bool IsNewModel = true;

                        if (Global.IsShowValue) txtCalcCT.AppendText(string.Format("\r\n是否新模块:{0};\r\n", IsNewModel));

                        #region 定性消线
                        if ("定性消线" == _item.detect_method)
                        {
                            double InvalidC = 0, MaxT = 0, MinT = 0;

                            InvalidC = _item.InvalidC;
                            MaxT = _item.dxxx.PlusT = double.Parse(_item.yangT); ;// _item.dxxx.threeMaxT[i];
                            MinT = _item.dxxx.MinusT = double.Parse(_item.yinT);// _item.dxxx.threeMinT[i];

                            if (Global.IsShowValue)
                            {
                                txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));

                                //double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxxx.MaxT[i] * 2;
                                testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}\r\n", (i + 1), _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4")));
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
                        #endregion

                        #region 定性比色

                        //新版本判定方法：Abs（T/C）≥Abs时 C＞T阳性 C＜T阴性；Abs（C/T）＜Abs时 SexIdx=0阴性 else 阳性
                        else if ("定性比色"== _item.detect_method)
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
                            float ctAbs = (float)(_ValueT[i] / _ValueC[i]);// (float)(_ValueC[i] / _ValueT[i]);

                            if (Global.JtjVersion == 3)//3.0
                            {
                                InvalidC = _item.InvalidC;
                                MaxT =double.Parse( _item.absX);
                                MinT =double.Parse( _item.absX);
                            }

                            if (Global.IsShowValue)
                            {
                                txtCalcCT.AppendText(string.Format("判定值C:{0},MaxT:{1},MinT:{2};\r\n", InvalidC, MaxT, MinT));
                                testInfo = string.Format("Value:{0},MaxT:{1},MinT:{2}", Math.Round(_ValueT[i], 4), MaxT, MinT);
                                //double InvalidC = _item.InvalidC > 0 ? _item.InvalidC : _item.dxbs.MaxT[i] * 2;
                                txtCalcCT.AppendText(string.Format("C{0}:{1} T{0}:{2}  T:C:{3} \r\n", (i + 1),
                                    _ValueC[i].ToString("F4"), _ValueT[i].ToString("F4"), ctAbs.ToString("F4")));
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

                        int number = num > 0 ? (i - num) : i;
                        _CheckValue[number, 0] = String.Format("{0:D2}", (i + 1));
                        _CheckValue[number, 1] = "胶体金";
                        _CheckValue[number, 2] = _item.item ;
                        _CheckValue[number, 3] = _item.detect_method;//_methodToString[_item.Method];
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
                        if (Global.ManuTest == true)
                        {
                            _CheckValue[number, 13] = _item.Hole[i].TaskName ?? string.Empty;
                            _CheckValue[number, 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                            _CheckValue[number, 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                            _CheckValue[number, 16] = _item.Hole[i].ProduceCompany;
                            _CheckValue[number, 18] = _item.Hole[i].Operator;
                            _CheckValue[number, 19] = _item.Hole[i].OperatorID;

                        }
                        else
                        {

                            _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskName ?? string.Empty;
                            _CheckValue[(num > 0 ? (i - num) : i), 14] = string.IsNullOrEmpty(_item.Hole[i].CompanyName) ? string.Empty : _item.Hole[i].CompanyName;
                            _CheckValue[(num > 0 ? (i - num) : i), 15] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                            _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].ProduceCompany;
                            _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].tID;
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
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "计算结果", ex.ToString());
                //MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "计算结果");
                MessageBox.Show("请重新检测", "结果提示");
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
                if (LoginWindow._userAccount.CheckSampleID == false)
                {
                    if (LoginWindow._userAccount.UpDateNowing)
                        Upload();
                }
            }
            else if (Global.EachDistrict.Equals("GS"))
            {
                //2016年3月7日 对于没有强制上传的用户采用提示方式引导用户上传，若不上传则不保存当前检测数据
                if (LoginWindow._userAccount.UpDateNowing)
                {
                    Save();
                    if (LoginWindow._userAccount.CheckSampleID == false)
                        if (LoginWindow._userAccount.UpDateNowing)
                             Upload();
                }
                else
                {
                    //if (MessageBox.Show("是否上传数据!？\n注意：若不上传数据，则此次检测的所有数据都将不做保存处理!\n请知悉!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //{
                        Save();
                        if (LoginWindow._userAccount.CheckSampleID == false)
                            if (LoginWindow._userAccount.UpDateNowing)
                                  Upload();
                    //}
                }
            }
            UpdateItem();
            ButtonPrint.IsEnabled = true;
            //btn_upload.IsEnabled = true;
            if (LoginWindow._userAccount.CheckSampleID == false)
            {
                btn_upload.IsEnabled = false;

            }
            else
            {
                if (LoginWindow._userAccount.UpDateNowing == false)
                {
                    btn_upload.IsEnabled = true;
                }

            }
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
            _AllNumber = TestResultConserve.ResultConserve(_CheckValue, out syscodes);
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
                report.ItemName = _item.item;
                report.ItemCategory = "胶体金";
                if (DYJTJItemPara.METHOD_DXBS == _item.Method)
                    report.TestMethod = "定性比色";
                report.User = LoginWindow._userAccount.UserName;
                report.Unit = _item.detect_unit;
                report.Date = _date.ToString("yyyy-MM-dd HH:mm:ss");
                report.Judgment = _item.Hole[0].SampleName;

                if (Global.ManuTest == false)//抽样单
                {
                    for (int j = 0; j < _AllNumber; j++)
                    {
                        DataTable dt = _resultTable.GetAsDataTable("taskid='" + Global.Testsample[j, 1] + "'", string.Empty, 8, 0);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<tlsTtResultSecond> recod = Global.TableToEntity<tlsTtResultSecond>(dt);
                            for (int k = 0; k < recod.Count; k++)
                            {
                                report.SampleName.Add(recod[k].FoodName);
                                report.SampleNum.Add(String.Format("{0:D5}", recod[k].SampleCode));
                                report.JudgmentTemp.Add(recod[k].Result);
                                report.Result.Add(recod[k].CheckValueInfo);
                                if (DYJTJItemPara.METHOD_DXBS == _item.Method)
                                report.CBTvalue.Add(recod[k].TBCValue);
                            }
                        }
                    }
                }
                else
                {

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
                                if (DYJTJItemPara.METHOD_DXBS == _item.Method)
                                report.CBTvalue.Add(dtList[i].TBCValue);
                            }
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
                if (_AllNumber > 0)
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
                else
                {
                    LabelInfo.Content = "打印失败，无检测数据。";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ButtonPrint_Click):\n" + ex.Message);
            }
        }

        private UIElement  GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border()
            {
                Width =Global.JtjVersion==3?445: 185,
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
                Width = Global.JtjVersion == 3 ? 445 : 185,
                Height = 420,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = Global.JtjVersion == 3 ? 445 : 185,
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
                Width = Global.JtjVersion == 3 ? 440 : 185,
                Height = Global.JtjVersion == 3 ? 220: 200,
                Background = Brushes.Gray  ,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "canvas"
            };
            if(Global.JtjVersion ==3)
            {
                ChartPlotter plotter = new ChartPlotter
                {
                    Width = 440,
                    Height = 220,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Name = "chartPlotter"
                };
                //plotter.MouseDoubleClick += new MouseButtonEventHandler(plotter_MouseDoubleClick);

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
            }

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
                //Text = string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            if (Global.ManuTest == true)
            {
                textBoxSampleName.Text = _item.Hole[channel].SampleName;
            }
            else
            {
                textBoxSampleName.Text = _item.Hole[channel].SampleName;
            }
            WrapPanel wrapPannelGrayValue = new WrapPanel()
            {
                Width = 180,
                Height = 30,
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
            //3.0模块
            WrapPanel wrapPanel = new WrapPanel
            {
                Width = 445,
                Height = 180,
                
            };
            Canvas rootCanvas = new Canvas()
            {
                Width = 240,
                Height = 180,
                Background = Brushes.Gray ,
        
                Visibility = Global.JtjVersion == 3 ? Visibility.Visible : Visibility.Collapsed,
                Name = "rootCanvas",
                Tag = channel,
            };
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

            StackPanel spContent = new StackPanel
            {
                Width = 200 ,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Name = "spContent",
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
            //新模块摄像头
            if(Global.JtjVersion ==3)
            {
                wrapPanel.Children.Add(rootCanvas);

                spContent.Children.Add(wrapPannelSampleNum);
                spContent.Children.Add(wrapPannelSampleName);
                spContent.Children.Add(wrapPannelDetectResult);
                spContent.Children.Add(wrapJudgemtn);
                wrapPanel.Children.Add(spContent);
                stackPanel.Children.Add(wrapPanel);

            }
            else
            {
                stackPanel.Children.Add(wrapPannelSampleNum);
                stackPanel.Children.Add(wrapPannelSampleName);
                stackPanel.Children.Add(wrapPannelGrayValue);
                stackPanel.Children.Add(wrapPannelDetectResult);
                stackPanel.Children.Add(wrapJudgemtn);
                stackPanel.Children.Add(wrapStandValue);
            }
            border.Child = stackPanel;
            return border;
        }
        private void oShowResult()
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
                    JTJResult jtjResult = oCalJTJFResult((byte)CValue, (byte)TValue, _item);
                    listTextBoxGray[i].Text = "C(" + CValue + ") T(" + TValue + ")";
                    string str = string.Empty;

                    string TBCValue = string.Empty;

                    string[] UnqualifiedValue = new string[4];
                    str = JTJFResultStatToStr(jtjResult);
                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.item);
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
                            if (TValue == 0 || TValue < 0)
                            {
                                _listJudmentValue[i].Text = str = "阳性";
                                listJudgmentRes[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";

                            }
                            else
                            {
                                double ctAbs = CValue / TValue;
                                //存放T/C的值
                                double tc = double.Parse( TValue.ToString ())/ double.Parse( CValue.ToString ());
                                TBCValue = Math.Round(tc,2).ToString();

                                if (ctAbs <= _item.dxbs.Abs_X)
                                {
                                    if(Global.showCT )
                                        _listJudmentValue[i].Text = str = TBCValue;// "阴性";
                                    else
                                        _listJudmentValue[i].Text = str = "阴性";
                                    listJudgmentRes[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else
                                {
                                    if (Global.showCT)
                                        _listJudmentValue[i].Text = str = TBCValue;// "阳性";
                                    else
                                        _listJudmentValue[i].Text = str = "阳性";
                                    listJudgmentRes[i].Text = "不合格";
                                    UnqualifiedValue[0] = "不合格";
                                }
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
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.item);
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
                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.item;
                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _item.detect_method;
                    
                    _CheckValue[(num > 0 ? (i - num) : i), 4] = str;

                    
                    _CheckValue[(num > 0 ? (i - num) : i), 5] = _item.detect_unit;
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
                    _CheckValue[(num > 0 ? (i - num) : i), 18] = _item.Hole[i].Operator ;
                    _CheckValue[(num > 0 ? (i - num) : i), 19] = _item.Hole[i].OperatorID;
                    //_CheckValue[(num > 0 ? (i - num) : i), 20]=TBCValue;

                }
                else
                {
                    num += 1;
                    _listStrRecord.Add(null);
                    _listDetectResult.Add(null);
                }
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
                    string TBCValue = string.Empty;

                    string[] UnqualifiedValue = new string[4];
                    str = JTJFResultStatToStr(jtjResult);
                    UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.item);
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
                            if (TValue == 0 || TValue < 0)
                            {
                              
                                _listJudmentValue[i].Text = str = "阳性";
                                listJudgmentRes[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";

                            }
                            else
                            {
                                double ctAbs = CValue / TValue;
                                //存放T/C的值
                                double tc =double.Parse ( TValue.ToString() ) /double.Parse ( CValue.ToString ());
                                TBCValue = Math.Round(tc, 2).ToString();

                                if (ctAbs <= _item.dxbs.Abs_X)
                                {
                                    if(Global.showCT )
                                        _listJudmentValue[i].Text = str = TBCValue;
                                    else
                                        _listJudmentValue[i].Text = str = "阴性";

                                    listJudgmentRes[i].Text = "合格";
                                    UnqualifiedValue[0] = "合格";
                                }
                                else
                                {
                                    if (Global.showCT)
                                        _listJudmentValue[i].Text = str = TBCValue;
                                    else
                                        _listJudmentValue[i].Text = str = "阳性";
                                    listJudgmentRes[i].Text = "不合格";
                                    UnqualifiedValue[0] = "不合格";
                                }
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
                    _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.item;
                    _CheckValue[(num > 0 ? (i - num) : i), 3] = _item.detect_method;//_methodToString[_item.Method];
                    if (_item.Method == DYJTJItemPara.METHOD_DXBS)
                    {
                        if(Global.showCT )
                            _CheckValue[(num > 0 ? (i - num) : i), 4] = TBCValue;
                        else
                            _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                    }
                    else
                    {
                        _CheckValue[(num > 0 ? (i - num) : i), 4] = str;
                    }
                    
                    _CheckValue[(num > 0 ? (i - num) : i), 5] = _item.detect_unit;
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
                    _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.Hole[i].tID;
                    //_CheckValue[(num > 0 ? (i - num) : i), 18] = TBCValue;

                }
                else
                {
                    num += 1;
                    _listStrRecord.Add(null);
                    _listDetectResult.Add(null);
                }
            }
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
                ToDetect();
            }
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
            NewFormattingDatas();

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
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "胶体金3.0模块");
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
                if (Global.ManuTest == true)
                {
                    if (_item.Hole[i].Use)
                    {
                        _item.SampleNum++;
                    }
                }
                else
                {
                    if (_item.Hole[i].Use)
                    {
                        _item.SampleNum++;
                    }
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

        private JTJResult oCalJTJFResult(byte CValue, byte TValue, KJFWJTItem mItem)
        {
            mItem.InvalidC = int.Parse(mItem.invalid_value);
            if (mItem.detect_method == "定性比色")
            {
                mItem.Method = 1;
            }
            else if (mItem.detect_method == "定性消线")
            {
                mItem.Method = 0;
            }
            else if (mItem.detect_method == "定量法(T)")
            {
                mItem.Method = 2;
            }
            else if (mItem.detect_method == "定量法(T/C)")
            {
                mItem.Method = 3;
            }

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

        private JTJResult CalJTJFResult(byte CValue, byte TValue, KJFWJTItem mItem)//DYJTJItemPara
        {
            mItem.InvalidC =int.Parse ( mItem.invalid_value);
            if (mItem.detect_method == "定性比色")
            {
                mItem.Method = 1;
            }
            else if(mItem.detect_method == "定性消线")
            {
                mItem.Method = 0;
            }
            else if (mItem.detect_method == "定量法(T)")
            {
                mItem.Method = 2;
            }
            else if (mItem.detect_method == "定量法(T/C)")
            {
                mItem.Method = 3;
            }

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
                            mItem.dxxx.MinusT = double.Parse(_item.yangT);
                            mItem.dxxx.PlusT =double.Parse(_item.yinT);

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
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }
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
                DataTable dt = null;
                List<tlsTtResultSecond> dtList = new List<tlsTtResultSecond>();
                if (Global.ManuTest == false)
                {
                    if (_AllNumber < 1)
                    {
                        LabelInfo.Content = "暂无需要上传的数据";
                        return;
                    }

                    for (int j = 0; j < _AllNumber; j++)
                    {
                        dt = _resultTable.GetAsDataTable("taskid='" + Global.Testsample[j, 1] + "'", string.Empty, 8, 0);
                        List<tlsTtResultSecond> recod = Global.TableToEntity<tlsTtResultSecond>(dt);
                        for (int k = 0; k < recod.Count; k++)
                        {
                            dtList.Add(recod[k]);
                        }
                    }
                }
                else if (Global.ManuTest == true)
                {
                    dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
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
                    dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                }
                Message msg = new Message()
                {
                    what = MsgCode.MSG_UPLOAD,
                    obj1 = Global.samplenameadapter[0],
                    table = dt,
                    selectedRecords = dtList,
                };

                //if (Global.InterfaceType.Equals("ZH"))
                //{
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        //List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                //        msg.selectedRecords = dtList;
                //    }
                //}
                Global.updateThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("上传数据时出现异常", "上传失败", ex.Message);
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