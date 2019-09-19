using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using AIO.xaml.Dialog;
using com.lvrenyang;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;

namespace AIO
{
    /// <summary>
    /// GszMeasureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszMeasureWindow : Window
    {

        #region 全局变量
        #region 新摄像头模块
 
        // 定时工作线程，只需要循环读取摄像头图片
        // 底层把图片读到之后会返回给FgdMeasureThread
        private TimerThread _timerThread;
        private bool _bTimerWork; // 需要初始化
        private bool _msgReadCAMReplyed;// 需要初始化
        private static List<ComPort> ports = null;
        bool sxtOut = false;
        bool sxtIn = false;
        bool sxtQr = false;
        bool sxtTest = false;

        #endregion

        public DYGSZItemPara _item = null;
        private UpdateCAMThread _updateCAMThread;
       
        private int _MAX_CAM = 0;
        //int _CUR_CAM = 0;
        // 设置TC线有几个检测通道
        private bool[] _bTCLineNeedSetting;
        private HelpBox[] _helpBoxes;
        // 读取灰阶值并保存起来
        private bool[] _bRGBValuesNeedRead; // 当bGrayValuesNeedRead都是false的时候，开始启动新窗口
        private List<byte[]> _listRGBValues;
        private string[] _strSXTPORT = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };

        private List<TextBox> _listSampleNum;
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private DispatcherTimer _DataTimer = null;
        private List<bool> _IsThereCard = new List<bool>();
        private int TestNum = 0;
        //private List<byte[]> datas = null;
        //private bool _IsFirstChannel = false;
        private string logType = "GszMeasureWindow-error";
        private List<byte[]> datas = null;
        private List<double[]> _curveDatas = null;

        #endregion

        public GszMeasureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item) return;
            try
            {
                //ComPort.Ver = Global.JtjVersion;
                btnPlayer.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
                _MAX_CAM = Global.deviceHole.SxtCount - 1;
                _listSampleNum = new List<TextBox>();
                int sampleNum = _item.SampleNum;
                int holeUse = 0;
                //WrapPanelChannel.Width = 0;
                ports = new List<ComPort>();
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        //WrapPanelChannel.Width += 300;
                        holeUse += 1;
                        sampleNum++;
                        sv_panle.Width += 245;
                        _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
                        if (_item.dx.DeltaA[i] != double.MinValue) btnContrast.Visibility = Visibility.Visible;

                        //新模块摄像头
                        if (Global.JtjVersion == 3 || Global.Is3500I)
                        {
                            ComPort port = new ComPort();
                            if (port.Open(_strSXTPORT[i], true))
                                ports.Add(port);
                            else
                                ports.Add(null);
                        }
                    }
                    else
                    {
                        ports.Add(null);
                    }
                    
                }
                // 初始化辅助方框及TC线的参数
                _bTCLineNeedSetting = new bool[Global.deviceHole.SxtCount];
                _helpBoxes = new HelpBox[Global.deviceHole.SxtCount];
                for (int i = 0; i < _bTCLineNeedSetting.Length; ++i)
                {
                    _bTCLineNeedSetting[i] = false;
                    _helpBoxes[i] = new HelpBox();
                }

                // 初始化灰阶值存储位置
                _bRGBValuesNeedRead = new bool[Global.deviceHole.SxtCount];
                _listRGBValues = new List<byte[]>();
                for (int i = 0; i < _bRGBValuesNeedRead.Length; ++i)
                {
                    _bRGBValuesNeedRead[i] = false;
                    _listRGBValues.Add(null);
                }
                //启动线程
                _updateCAMThread = new UpdateCAMThread(this)  ;
                _updateCAMThread.Start();

                _bTimerWork = true;
                _msgReadCAMReplyed = true;
                //摄像头定时扫描线程
                _timerThread = new TimerThread(this);
                if (Global.JtjVersion == 3 || Global.Is3500I)
                {
                    _timerThread.Start();//摄像头定时扫描线程
                    Message msg = new Message()
                    {
                        what = MsgCode.MSG_TIMER_WORK
                    };
                    _timerThread.SendMessage(msg, null);
                }

                 //进入测试界面后先出卡操作
                if (!Global.InstrumentNameModel.Contains("DY-3500"))//DY-3500不用等进卡
                {
                    jbkOUT();
                    countdownStart(3);
                }
                else
                {
                    btnINorOUT.Content = "测试";
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 进卡or出卡倒计时
        /// </summary>
        /// <param name="num">时间 s</param>
        private void countdownStart(int s)
        {
            btnContrast.IsEnabled = btnINorOUT.IsEnabled = false;
            btnINorOUT.Content = "出卡/放卡";
            if (_DataTimer == null)
                _DataTimer = new DispatcherTimer();
            _DataTimer.Interval = TimeSpan.FromSeconds(s);
            _DataTimer.Tick += new EventHandler(countDown);
            _DataTimer.Start();
        }

        private void countDown(object sender, EventArgs e)
        {
            if (_DataTimer != null)
            {
                _DataTimer.Stop();
                _DataTimer = null;
            }
            btnINorOUT.Content = "进卡/测试";
            btnContrast.IsEnabled = btnINorOUT.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                _timerThread.Stop();
                //if(_updateCAMThread!=null)
                //    _updateCAMThread.Stop();
                for (int i = 0; i < ports.Count; i++)
                {
                    if (ports[i] == null) continue;
                    ports[i].Close();
                    ports[i].Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                jbkIN();
                GC.Collect();
            }

        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, _item.Name);
            try
            {
                if (File.Exists(path))
                {
                    TechnologeDocument window = new TechnologeDocument
                    {
                        path = path,
                        ShowInTaskbar = false,
                        Topmost = true,
                        Owner = this
                    };
                    window.Show();
                }
                else
                {
                    Global.IsOpenPrompt = true;
                    PromptWindow window = new PromptWindow
                    {
                        _HintStr = _item.HintStr
                    };
                    window.Show();
                }
            }
            catch (Exception)
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow
                {
                    _HintStr = _item.HintStr
                };
                window.Show();
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            sxtIn = true;
            Global.SXTInOut = false;
            _msgReadCAMReplyed = false;
            _bTimerWork = false;
            this.Close();
        }

        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border
            {
                Width = Global.Is3500I ? 202 : 240,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "border"
            };

            StackPanel stackPanel = new StackPanel
            {
                Width = Global.Is3500I ? 202 : 240,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };
            Canvas rootCanvas = new Canvas()
            {
                Width = Global.Is3500I ? 202 : 240,
                Height = 180,
                Background = Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Visibility = Global.JtjVersion == 3 || Global.Is3500I ? Visibility.Visible : Visibility.Collapsed,
                Name = "rootCanvas"
            };

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
            rect.SetValue(Canvas.LeftProperty, channel == 0 ? Global.nHelpBoxLeft1 : Global.nHelpBoxLeft2);
            rect.SetValue(Canvas.TopProperty, channel == 0 ? Global.nHelpBoxTop1 : Global.nHelpBoxTop2);
            rect.MouseLeftButtonDown += Handle_MouseDown;
            rect.MouseMove += Handle_MouseMove;
            rect.MouseLeftButtonUp += Handle_MouseUp;
            Grid grid = new Grid
            {
                Width = 250,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Height = 40
            };

            Label label = new Label
            {
                FontSize = 20,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Content = "检测通道 " + (channel + 1)
            };

            //检测人员
            WrapPanel wrapPannelDetectPeople = new WrapPanel()
            {
                Width = 250,
                Height = 30
            };
            Label labelDetectPeople = new Label()
            {
                Width = 245,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = string.Format(" 检测人员:   {0}", LoginWindow._userAccount != null ? LoginWindow._userAccount.UserName : string.Empty),
                VerticalContentAlignment = VerticalAlignment.Center
            };

            WrapPanel wrapPannelSampleNum = new WrapPanel
            {
                Width = 250,
                Height = 30
            };

            Label labelSampleNum = new Label
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品编号:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleNum = new TextBox
            {
                Width = 145,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = sampleNum.ToString(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "sampleNum"
            };

            WrapPanel wrapPannelSampleName = new WrapPanel
            {
                Width = 250,
                Height = 30
            };

            Label labelSampleName = new Label
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品名称:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBox textBoxSampleName = new TextBox
            {
                Width = 145,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = "" + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };

            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            grid.Children.Add(label);

            stackPanel.Children.Add(grid);
            rootCanvas.Children.Add(rect);
            stackPanel.Children.Add(rootCanvas);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);

            border.Child = stackPanel;
            return border;
        }
        bool isMouseCaptured;
        double mouseVerticalPosition;
        double mouseHorizontalPosition;
        public void Handle_MouseDown(object sender, MouseEventArgs args)
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
        public void Handle_MouseMove(object sender, MouseEventArgs args)
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

        public void Handle_MouseUp(object sender, MouseEventArgs args)
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
            }
        }
        private void ShowResultDlg()
        {
            try
            {
                GszReportWindow window = new GszReportWindow
                {
                    _item = _item,
                    _datas = datas,
                    _newJtjDatas = dtList,
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        if (_item.dx.DeltaA[i] != double.MinValue) btnContrast.Visibility = Visibility.Visible;
                    }
                }
                datas = null;
                Global.jbkIsOut = false;
                JbkTest();

                //如果是胶体金3.0模块，需要更新取色框的位置
                if (Global.JtjVersion == 3 || Global.Is3500I )
                {
                    List<Rectangle> rects = GetChildObjects<Rectangle>(WrapPanelChannel, "rect");
                    if (rects != null && rects.Count >0)//单通道会抛异常
                    {
                        if (rects[0] != null)
                        {
                            rects[0].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft1);
                            rects[0].SetValue(Canvas.TopProperty, Global.nHelpBoxTop1);
                        }
                        if (rects.Count > 1)
                        {
                            if (rects[1] != null)
                            {
                                rects[1].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft2);
                                rects[1].SetValue(Canvas.TopProperty, Global.nHelpBoxTop2);
                            }
                        }
                        if (rects.Count > 2)
                        {
                            if (rects[2] != null)
                            {
                                rects[2].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft3);
                                rects[2].SetValue(Canvas.TopProperty, Global.nHelpBoxTop3);
                            }   
                        }
                        if (rects.Count > 3)
                        {
                            if (rects[3] != null)
                            {
                                rects[3].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft4);
                                rects[3].SetValue(Canvas.TopProperty, Global.nHelpBoxTop4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateSampleCode()
        {
            int sampleNum = _item.SampleNum;
            for (int i = 0; i < _listSampleNum.Count; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    _listSampleNum[i].Text = String.Format("{0:D5}", (sampleNum++));
                }
            }
            btnINorOUT.Content = "正在出卡";
        }

        public class HelpBox
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public int TLineOffset;
            public int CLineOffset;
        }

        List<Canvas> canvases = null;
        private void CreateImg(byte[] data, int camID)
        {
            try
            {
                BitmapSource img = BufferToBitmap(data, camID);
                if (img == null) return;
                if (canvases == null)
                    canvases = GetChildObjects<Canvas>(WrapPanelChannel, "rootCanvas");
                if (canvases == null || camID >= canvases.Count)
                    return;

                ImageBrush ib = new ImageBrush()
                {
                    ImageSource = img,
                    TileMode = TileMode.None,
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Top
                };
                canvases[camID].Background = ib;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private BitmapSource BufferToBitmap(byte[] buffer, int camID)
        {
            try
            {
                BitmapSource img = null;
                if (buffer != null)
                {
                    int HDR_LEN = 5;
                    int WIDTH = (int)((uint)buffer[1] + (uint)(buffer[2] << 8));
                    int HEIGHT = (int)((uint)buffer[3] + (uint)(buffer[4] << 8));
                    int BITDEPTH = 2;
                    int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);

                    byte[] pixels = new byte[DATA_LEN];
                    Array.Copy(buffer, HDR_LEN, pixels, 0, DATA_LEN);
                    byte[] pixelsrgb = new byte[WIDTH * HEIGHT * 3];

                    for (int j = 0; j < HEIGHT; ++j)
                    {
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
                        }
                    }
                    img = BitmapSource.Create(WIDTH, HEIGHT, 96, 96, PixelFormats.Bgr24, BitmapPalettes.WebPalette, pixelsrgb, WIDTH * 3);
                }
                return img;
            }
            catch (Exception ex)
            {
                return null;
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
       

        public byte[] GenerateHelpBoxParam(HelpBox helpBox, int n)
        {
            byte[] data = new byte[15];

            data[0] = 0x1B;
            data[1] = 0x1E;
            data[2] = (byte)n;

            int idx = 3;
            data[idx++] = (byte)(helpBox.Top & 0xFF);
            data[idx++] = (byte)((helpBox.Top >> 8) & 0xFF);

            data[idx++] = (byte)(helpBox.Left & 0xFF);
            data[idx++] = (byte)((helpBox.Left >> 8) & 0xFF);

            data[idx++] = (byte)(helpBox.Height & 0xFF);
            data[idx++] = (byte)((helpBox.Height >> 8) & 0xFF);

            data[idx++] = (byte)(helpBox.Width & 0xFF);
            data[idx++] = (byte)((helpBox.Width >> 8) & 0xFF);

            data[idx++] = (byte)(helpBox.CLineOffset & 0xFF);
            data[idx++] = (byte)((helpBox.CLineOffset >> 8) & 0xFF);

            data[idx++] = (byte)(helpBox.TLineOffset & 0xFF);
            data[idx++] = (byte)((helpBox.TLineOffset >> 8) & 0xFF);

            return data;
        }
        private void GetTCLine()
        {
            // 设置TC线
            List<StackPanel> stackPanels = UIUtils.GetChildObjects<StackPanel>(WrapPanelChannel, "stackPanel");

            for (int i = 0; i < stackPanels.Count; ++i)
            {
                StackPanel stackPanel = stackPanels[i];
                //Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
                //Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
                Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
                Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");

                _item.Hole[i].RegionLeft = _helpBoxes[i].Left = (int)(Double)rect.GetValue(Canvas.LeftProperty);
                _item.Hole[i].RegionTop = _helpBoxes[i].Top = (int)(Double)rect.GetValue(Canvas.TopProperty);
                _item.Hole[i].RegionWidth = _helpBoxes[i].Width = (int)rect.Width;
                _item.Hole[i].RegionHeight = _helpBoxes[i].Height = (int)rect.Height;
                //_item.Hole[i].TOffset = _helpBoxes[i].TLineOffset = (int)(Double)lineT.GetValue(Canvas.TopProperty) - _helpBoxes[i].Top;
                //_item.Hole[i].COffset = _helpBoxes[i].CLineOffset = (int)(Double)lineC.GetValue(Canvas.TopProperty) - _helpBoxes[i].Top;
            }
            for (int i = 0; i < _bTCLineNeedSetting.Length; ++i)
            {
                _bTCLineNeedSetting[i] = true;
            }
        }

        /// <summary>
        /// 记录点击测试时的准确时间
        /// </summary>
        DateTime testDate;

        // 和主线程绑定，收到消息后进行更新界面。
        class UpdateCAMThread : ChildThread
        {
            GszMeasureWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public UpdateCAMThread(GszMeasureWindow wnd)
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
                catch (Exception ex)
                {
                    FileUtils.OprLog(3, wnd.logType, ex.ToString());
                    MessageBox.Show(ex.Message);
                }
            }

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    //检测是否有卡
                    case MsgCode.MSG_JBK_CKC:
                       wnd._IsThereCard.Add(msg.result);
                       if (wnd.TestNum == wnd._IsThereCard.Count)
                       {
                           bool isTest = true;
                           for (int i = 0; i < wnd._IsThereCard.Count; i++)
                           {
                               if (!wnd._IsThereCard[i])
                               {
                                   isTest = false;
                                   break;
                               }
                           }
                           if (isTest)
                           {
                               wnd.JbkTest();
                           }
                           else
                           {
                               MessageBox.Show(wnd, "请放入金标卡！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                           }
                       }
                       break;

                    case MsgCode.MSG_JBK_InAndTest:
                       if (msg.result)
                        {
                            msg.result = false;
                            wnd.datas = msg.datas;
                            wnd.ShowResultDlg();
                            wnd.UpdateSampleCode();
                        }
                        break;
                    //胶体金新摄像头模块
                    case MsgCode.MSG_READ_CAM:
                        if (msg.obj3.ToString().Equals("test"))
                        {
                            //测试时先判断是否有卡
                            if (!msg.result)
                            {
                                MessageBox.Show(wnd, "请放入金标卡！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                wnd.btnINorOUT.IsEnabled = true;
                            }
                            else
                            {
                                wnd.isSxtTest = true;
                            }
                            wnd.dtList = new List<byte[]>[Global.deviceHole.SxtCount];
                            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
                            {
                                List<byte[]> dt = new List<byte[]>();
                                wnd.dtList[i] = dt;
                            }
                        }

                        //绘图
                        List<byte[]> datas = msg.obj2 as List<byte[]>;
                        int isdata = 0;//记录有多少个通道
                        for (int j = 0; j < datas.Count; j++)
                        {
                            if (datas[j] == null) continue;
                            if (wnd.isSxtTest)
                                isdata++;
                        }

                        for (int i = 0; i < datas.Count; i++)
                        {
                            if (wnd.isSxtTest)
                            {
                                if (datas[i] == null) continue;
                                isdata--;
                                if (Global.InstrumentNameModel.Contains("DY-3500"))//DY-3500不用等进卡
                                {
                                    wnd.dtList[i].Add(datas[i]);
                                    //采集十张图片，算CT值，改成采集5张图片计算，提高检测速度
                                    if (wnd.dtList[i].Count >= 1 && wnd.dtList[i].Count >= 1 && isdata == 0)
                                    {
                                        wnd.ShowResultDlg();
                                        wnd.UpdateSampleCode();
                                        wnd.isSxtTest = false;
                                        Global.SXTInOut = false;
                                        for (int j = 0; j < wnd.dtList.Length; j++)
                                        {
                                            wnd.dtList[i].Clear();
                                        }
                                    }
                                }
                                else
                                {
                                    //点击测试后，需要等待进卡5s后才开始采集图像
                                    if ((DateTime.Now - wnd.testDate).TotalSeconds >= 5)
                                    {
                                        wnd.dtList[i].Add(datas[i]);
                                        //采集十张图片，算CT值，改成采集5张图片计算，提高检测速度
                                        if (wnd.dtList[i].Count >= Global.LineCount && wnd.dtList[i].Count >= Global.LineCount && isdata == 0)
                                        {
                                            wnd.ShowResultDlg();
                                            wnd.UpdateSampleCode();
                                            wnd.isSxtTest = false;
                                            Global.SXTInOut = false;
                                            for (int j = 0; j < wnd.dtList.Length; j++)
                                            {
                                                wnd.dtList[i].Clear();
                                            }
                                        }
                                    }
                                }
                            }

                            wnd.CreateImg(datas[i], i);
                        }
                        //Thread.Sleep(5);
                        //DateUtils.WaitMs(10);
                        wnd._msgReadCAMReplyed = true;
                        break;
                    default:
                        break;
                }
            }
        }
        bool isSxtTest = false;
        List<byte[]>[] dtList = new List<byte[]>[Global.deviceHole.SxtCount];
        class TimerThread : ChildThread
        {
            GszMeasureWindow wnd;
            private delegate void ReadCAMDelegate(Message msg);
            private ReadCAMDelegate readCAMDelegate;

            public TimerThread(GszMeasureWindow wnd)
            {
                this.wnd = wnd;
                readCAMDelegate = new ReadCAMDelegate(ReadCAM);
            }
            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                switch (msg.what)
                {
                    case MsgCode.MSG_TIMER_WORK:
                        while (wnd._bTimerWork)//要初始化
                        {
                            if (wnd._msgReadCAMReplyed)
                            {
                                wnd._msgReadCAMReplyed = false;
                                wnd.Dispatcher.Invoke(readCAMDelegate, msg);
                            }
                            else
                            {
                                if(Global.Is3500I )
                                    Thread.Sleep(100);
                                else
                                    Thread.Sleep(1);
                            }
                        };

                        break;
                    default:
                        break;
                }
            }
            private void ReadCAM(Message msg)
            {
                string cmd = string.Empty;
                if(Global.Is3500I )
                {
                    ////DY-3500(I)读取摄像头
                    wnd.GetTCLine();
                    // 设置TC线
                    List<byte[]> ctdata = new List<byte[]>();
                    for (int i = 0; i <= wnd._MAX_CAM; i++)
                    {
                        if (wnd._item.Hole[i].Use)
                        {
                            ctdata.Add(wnd.GenerateHelpBoxParam(wnd._helpBoxes[i], 0/*wnd.CUR_CAM*/));
                        }
                        else
                        {
                            ctdata.Add(null);
                        }
                    }

                    if (wnd.sxtTest)
                    {
                        cmd = "test";
                        wnd.sxtTest = false;
                    }

                    Message m = new Message()
                    {
                        what = MsgCode.MSG_READ_CAM,
                        obj1 = ports,
                        obj3 = cmd,
                        CTdatas = ctdata,
                    };
                    Global.workThread.SendMessage(m, wnd._updateCAMThread);
                    ctdata = null;
                }
                else
                {
                    if (wnd.sxtIn)
                    {
                        cmd = "in";
                        wnd.sxtIn = false;
                    }
                    else if (wnd.sxtOut)
                    {
                        cmd = "out";
                        wnd.sxtOut = false;
                    }
                    else if (wnd.sxtQr)
                    {
                        cmd = "qr";
                        wnd.sxtQr = false;
                    }
                    else if (wnd.sxtTest)
                    {
                        cmd = "test";
                        Global.SXTInOut = true;//检测
                        wnd.sxtTest = false;
                    }
                    // 读摄像头
                    Message m = new Message()
                    {
                        what = MsgCode.MSG_READ_CAM,
                        obj1 = ports,
                        obj3 = cmd
                    };
                    Global.workThread.SendMessage(m, wnd._updateCAMThread);
                }
            }
        }
    
        private void jbkINorOUT()
        {
            if (Global.jbkIsOut)
                jbkInAndTest();
            else
                jbkOUT();
        }

        private void jbkOUT()
        {
            if (Global.JtjVersion == 3)
            {
                if (Global.InstrumentNameModel.Contains("DY-3500"))//DY-3500不用等进卡
                {
                    btnINorOUT.Content = "测试";
                }
                else
                {
                    btnINorOUT.Content = "进  卡";
                }
                //btnINorOUT.Content = "进  卡";
                Global.SXTInOut = false;
                sxtOut = true;
                return;
            }

            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    Message msg = new Message
                    {
                        what = MsgCode.MSG_JBK_OUT,
                        str1 = _strSXTPORT[i]
                    };
                    Global.workThread.SendMessage(msg, null);
                }
            }
            Global.jbkIsOut = true;
        }

        /// <summary>
        /// 进卡后继续测试
        /// </summary>
        private void jbkInAndTest()
        {
            btnINorOUT.Content = "正在测试";
            Message msg = new Message
            {
                what = MsgCode.MSG_JBK_InAndTest,
                ports = new List<string>()
            };
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    msg.ports.Add(_strSXTPORT[i]);
                    msg.str1 = _strSXTPORT[i];
                }
                else
                {
                    msg.ports.Add(null);
                }
            }
            Global.workThread.SendMessage(msg, _updateCAMThread);
            Global.jbkIsOut = false;
        }

        private void jbkIN()
        {
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                Message msg = new Message
                {
                    what = MsgCode.MSG_JBK_IN,
                    str1 = _strSXTPORT[i]
                };
                Global.workThread.SendMessage(msg, null);
            }
            Global.jbkIsOut = false;
        }

        private void btnINorOUT_Click(object sender, RoutedEventArgs e)
        {
            //新模块
            if (Global.JtjVersion == 3 || Global.Is3500I )
            {
                testDate = DateTime.Now;
                _curveDatas = new List<double[]>();
                for (int i = 0; i < Global.deviceHole.SxtCount; i++)
                {
                    _curveDatas.Add(null);
                }
                sxtTest = true;
                btnINorOUT.IsEnabled = false;
                return;
            }

            TestNum = 0;
            _IsThereCard = new List<bool>();
            //检测是否有卡，同时获取模块版本信息
            Global.JbkCheckCalc._gszItem = _item;
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    TestNum++;
                    Message msg = new Message
                    {
                        what = MsgCode.MSG_JBK_CKC,
                        str1 = _strSXTPORT[i]
                    };
                    Global.workThread.SendMessage(msg, _updateCAMThread);
                }
            }
        }

        /// <summary>
        /// 金标卡检测
        /// </summary>
        private void JbkTest()
        {
            countdownStart(Global.jbkIsOut ? 30 : 3);
            jbkINorOUT();
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (Global.IsAPlayer)
            {
                try
                {
                    if (Global.IsPlayer)
                    {
                        MainWindow._aPlayer._ItemName = _item.Name;
                        MainWindow._aPlayer.LoadPlayer();
                    }
                    else
                    {
                        MainWindow._aPlayer = new APlayerForm
                        {
                            _ItemName = _item.Name
                        };
                        MainWindow._aPlayer.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format("播放视频教程时出现异常!\r\n\r\n异常信息：{0}\r\n\r\n解决方法：{1}", ex.Message, "请安装[迅雷看看]播放器！"), "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    Global.IsPlayer = true;
                    VideoPlayback video = new VideoPlayback
                    {
                        _ItemName = _item.Name
                    };
                    video.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("播放视频时出现异常！\r\n\r\n异常信息：" + ex.Message, "操作提示");
                }
            }
        }

        private void btnContrast_Click(object sender, RoutedEventArgs e)
        {
            //新模块
            if (Global.JtjVersion == 3)
            {
                testDate = DateTime.Now;
                _curveDatas = new List<double[]>();
                for (int i = 0; i < Global.deviceHole.SxtCount; i++)
                {
                    _curveDatas.Add(null);
                }
                sxtTest = true;
                btnINorOUT.IsEnabled = false;
                return;
            }

            TestNum = 0;
            _IsThereCard = new List<bool>();
            //检测是否有卡，同时获取模块版本信息
            Global.JbkCheckCalc._gszItem = _item;
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    _item.dx.DeltaA[i] = double.MinValue;
                    TestNum++;
                    Message msg = new Message
                    {
                        what = MsgCode.MSG_JBK_CKC,
                        str1 = _strSXTPORT[i]
                    };
                    Global.workThread.SendMessage(msg, _updateCAMThread);
                }
            }
        }

    }
}