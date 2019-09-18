using AIO.xaml.Dialog;
using com.lvrenyang;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace AIO
{
    /// <summary>
    /// GszMeasureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszMeasureWindow : Window
    {

        #region 全局变量
        public DYGSZItemPara _item = null;
        UpdateCAMThread _updateCAMThread;
        // 定时工作线程，只需要循环读取摄像头图片
        // 底层把图片读到之后会返回给FgdMeasureThread
        TimerThread _timerThread;
        bool _bTimerWork = true; // 需要初始化
        bool _msgReadCAMReplyed = true;// 需要初始化
        int _MAX_CAM = 0;
        int _CUR_CAM = 0;
        // 设置TC线有几个检测通道
        bool[] _bTCLineNeedSetting;
        HelpBox[] _helpBoxes;
        // 读取灰阶值并保存起来
        bool[] _bRGBValuesNeedRead; // 当bGrayValuesNeedRead都是false的时候，开始启动新窗口
        List<byte[]> _listRGBValues;
        private string[] _strSXTPORT = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };
        List<TextBox> _listSampleNum;
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        #endregion

        public GszMeasureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;
            _MAX_CAM = Global.deviceHole.SxtCount - 1;
            _listSampleNum = new List<TextBox>();
            int sampleNum = _item.SampleNum;
            int holeUse = 0;
            // 添加布局
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                UIElement element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                WrapPanelChannel.Children.Add(element);
                if (_item.Hole[i].Use)
                {
                    holeUse += 1;
                    sampleNum++;
                    _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
                }
                else
                {
                    element.Visibility = System.Windows.Visibility.Collapsed;
                    _listSampleNum.Add(null);
                }
            }
            if (holeUse < 4)
            {
                if (holeUse == 1)
                    this.WrapPanelChannel.Width = 190;
                else if (holeUse == 2)
                    this.WrapPanelChannel.Width = 380;
                else if (holeUse == 3)
                    this.WrapPanelChannel.Width = 570;
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

            // 启动线程
            _updateCAMThread = new UpdateCAMThread(this);
            _updateCAMThread.Start();
            _bTimerWork = true;
            _msgReadCAMReplyed = true;
            _timerThread = new TimerThread(this);
            _timerThread.Start();
            Message msg = new Message()
            {
                what = MsgCode.MSG_TIMER_WORK
            };
            _timerThread.SendMessage(msg, null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            _timerThread.Stop();
            _updateCAMThread.Stop();
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, _item.Name.Replace("*", ""));
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow()
                {
                    _HintStr = _item.HintStr
                };
                window.Show();
            }
            else
            {
                TechnologeDocument window = new TechnologeDocument();
                window.path = path;
                window.ShowInTaskbar = false;
                window.Topmost = true;
                window.Owner = this;
                window.Show();
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSampleTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSampleTest.Content = "正在测试";
            ButtonSampleTest.IsEnabled = false;
            // 发送命令，读取灰阶值
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (!_item.Hole[i].Use)
                {
                    _bRGBValuesNeedRead[i] = false;
                    continue;
                }
                else
                {
                    _bRGBValuesNeedRead[i] = true;
                }
                Message msg = new Message()
                {
                    what = MsgCode.MSG_READ_RGBVALUE,
                    str1 = _strSXTPORT[i],
                    arg1 = i,
                    arg2 = _helpBoxes[i].Height
                };
                Global.workThread.SendMessage(msg, _updateCAMThread);
            }
        }

        private void UpdateSampleNumUI()
        {
            // 更新样品编号
            int sampleNum = _item.SampleNum;
            for (int i = 0; i < _listSampleNum.Count; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    _listSampleNum[i].Text = String.Format("{0:D5}", (sampleNum++));
                }
            }
        }

        bool isMouseCaptured;
        double mouseVerticalPosition;
        double mouseHorizontalPosition;

        public void Handle_MouseDown(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");

            if ((lineT == sender) || (lineC == sender))
            {
                Line item = sender as Line;
                mouseVerticalPosition = args.GetPosition(null).Y;
                mouseHorizontalPosition = args.GetPosition(null).X;
                isMouseCaptured = true;
                item.CaptureMouse();
            }
            else if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                mouseVerticalPosition = args.GetPosition(null).Y;
                mouseHorizontalPosition = args.GetPosition(null).X;
                isMouseCaptured = true;
                item.CaptureMouse();
            }
        }

        public void Handle_MouseMove(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
            Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");

            // C线在上面
            if (lineC == sender)
            {
                Line item = sender as Line;
                if (isMouseCaptured)
                {
                    double deltaV = args.GetPosition(null).Y - mouseVerticalPosition;
                    double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                    double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
                    double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
                    double rectTop = (double)rect.GetValue(Canvas.TopProperty);

                    if (newTop < rectTop)
                        newTop = rectTop;
                    if (newTop > lineTTop)
                        newTop = lineTTop;
                    item.SetValue(Canvas.TopProperty, newTop);
                    mouseVerticalPosition = args.GetPosition(null).Y;
                    mouseHorizontalPosition = args.GetPosition(null).X;
                }
            }
            else if (lineT == sender)
            {   // T线在下面
                Line item = sender as Line;
                if (isMouseCaptured)
                {
                    double deltaV = args.GetPosition(null).Y - mouseVerticalPosition;
                    double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                    double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
                    double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
                    double rectTop = (double)rect.GetValue(Canvas.TopProperty);

                    if (newTop < lineCTop)
                        newTop = lineCTop;
                    if (newTop > rectTop + rect.Height)
                        newTop = rectTop + rect.Height;
                    item.SetValue(Canvas.TopProperty, newTop);
                    mouseVerticalPosition = args.GetPosition(null).Y;
                    mouseHorizontalPosition = args.GetPosition(null).X;
                }
            }
            else if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                if (isMouseCaptured)
                {
                    double deltaV = args.GetPosition(null).Y - mouseVerticalPosition;
                    double deltaH = args.GetPosition(null).X - mouseHorizontalPosition;
                    double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                    double newLeft = deltaH + (double)item.GetValue(Canvas.LeftProperty);
                    double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
                    double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
                    double rectTop = (double)rect.GetValue(Canvas.TopProperty);
                    double distanceT = lineTTop - rectTop;
                    double distanceC = lineCTop - rectTop;

                    if (newTop < 0)
                        newTop = 0;
                    if (newLeft < 0)
                        newLeft = 0;
                    if (newTop + item.Height > rootCanvas.Height)
                        newTop = rootCanvas.Height - item.Height;
                    if (newLeft + item.Width > rootCanvas.Width)
                        newLeft = rootCanvas.Width - item.Width;
                    item.SetValue(Canvas.TopProperty, newTop);
                    item.SetValue(Canvas.LeftProperty, newLeft);
                    lineT.SetValue(Canvas.TopProperty, newTop + distanceT);
                    lineT.SetValue(Canvas.LeftProperty, newLeft);
                    lineC.SetValue(Canvas.TopProperty, newTop + distanceC);
                    lineC.SetValue(Canvas.LeftProperty, newLeft);
                    mouseVerticalPosition = args.GetPosition(null).Y;
                    mouseHorizontalPosition = args.GetPosition(null).X;
                }
            }

        }

        public void Handle_MouseUp(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");

            if ((lineT == sender) || (lineC == sender))
            {
                Line item = sender as Line;
                isMouseCaptured = false;
                item.ReleaseMouseCapture();
                mouseVerticalPosition = -1;
                mouseHorizontalPosition = -1;
            }
            else if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                isMouseCaptured = false;
                item.ReleaseMouseCapture();
                mouseVerticalPosition = -1;
                mouseHorizontalPosition = -1;
            }

        }

        // 干试纸法CT线距离上边框为0，并且隐藏起来。
        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border()
            {
                Width = 185,
                Height = 320,
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
                Height = 300,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 185,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Height = 40
            };
            Label label = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = "检测通道" + (channel + 1)
            };
            Canvas rootCanvas = new Canvas()
            {
                Width = 185,
                Height = 200,
                Background = Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "rootCanvas"
            };
            double nHelpBoxWidth = _item.Hole[channel].RegionWidth;
            double nHelpBoxHeight = _item.Hole[channel].RegionHeight;
            double nHelpBoxLeft = _item.Hole[channel].RegionLeft;
            double nHelpBoxTop = _item.Hole[channel].RegionTop;
            double nHelpBoxCOffset = _item.Hole[channel].COffset;
            double nHelpBoxTOffset = _item.Hole[channel].TOffset;

            GradientStopCollection gradientStops = new GradientStopCollection
            {
                new GradientStop(Colors.Red, 0.00),
                new GradientStop(Colors.Red, 0.39),
                new GradientStop(Colors.Transparent, 0.40),
                new GradientStop(Colors.Transparent, 0.60),
                new GradientStop(Colors.Red, 0.61),
                new GradientStop(Colors.Red, 1.00)
            };
            LinearGradientBrush lineBrush = new LinearGradientBrush(gradientStops, new Point(0, 0), new Point(1, 0));

            Line lineC = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = nHelpBoxWidth,
                Y2 = 0
            };
            lineC.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            lineC.SetValue(Canvas.TopProperty, nHelpBoxTop + nHelpBoxCOffset);
            lineC.Stroke = lineBrush;
            lineC.StrokeThickness = 5;
            lineC.MouseLeftButtonDown += Handle_MouseDown;
            lineC.MouseMove += Handle_MouseMove;
            lineC.MouseLeftButtonUp += Handle_MouseUp;
            lineC.Name = "lineC";
            lineC.Visibility = System.Windows.Visibility.Hidden;

            Line lineT = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = nHelpBoxWidth,
                Y2 = 0
            };
            lineT.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            lineT.SetValue(Canvas.TopProperty, nHelpBoxTop + nHelpBoxTOffset);
            lineT.Stroke = lineBrush;
            lineT.StrokeThickness = 5;
            lineT.MouseLeftButtonDown += Handle_MouseDown;
            lineT.MouseMove += Handle_MouseMove;
            lineT.MouseLeftButtonUp += Handle_MouseUp;
            lineT.Name = "lineT";
            lineT.Visibility = System.Windows.Visibility.Hidden;

            Rectangle rect = new Rectangle()
            {
                Width = nHelpBoxWidth,
                Height = nHelpBoxHeight
            };
            rect.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            rect.SetValue(Canvas.TopProperty, nHelpBoxTop);
            rect.Stroke = Brushes.Red;
            rect.StrokeThickness = 4;
            rect.MouseLeftButtonDown += Handle_MouseDown;
            rect.MouseMove += Handle_MouseMove;
            rect.MouseLeftButtonUp += Handle_MouseUp;
            rect.Name = "rect";

            WrapPanel wrapPannelSampleNum = new WrapPanel()
            {
                Width = 185,
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
                IsReadOnly = true,
                Name = "sampleNum"
            };
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 185,
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
            rootCanvas.Children.Add(lineC);
            rootCanvas.Children.Add(lineT);
            rootCanvas.Children.Add(rect);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            grid.Children.Add(label);

            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(rootCanvas);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);

            border.Child = stackPanel;
            return border;
        }

        private void UpdateStatus(byte[] data, int camID)
        {
            List<Canvas> canvases = UIUtils.GetChildObjects<Canvas>(WrapPanelChannel, "rootCanvas");
            if (null == canvases)
                return;

            ImageBrush ib = new ImageBrush()
            {
                ImageSource = BufferToBitmap(data),
                TileMode = TileMode.None,
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top
            };
            canvases[camID].Background = ib;

        }

        private BitmapSource BufferToBitmap(byte[] buffer)
        {
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
                for (int i = 0; i < WIDTH; ++i)
                {
                    int offset16 = (j * WIDTH + i) * 2;
                    int offset24 = (j * WIDTH + i) * 3;
                    // rgb565 低字节在前
                    int rgb565 = (int)(pixels[offset16] + ((UInt32)pixels[offset16 + 1] << 8));
                    int red = ((rgb565 >> 11) & 0x1F) << 3;
                    int green = ((rgb565 >> 5) & 0x3F) << 2;
                    int blue = ((rgb565) & 0x1F) << 3;
                    pixelsrgb[offset24] = (byte)blue;
                    pixelsrgb[offset24 + 1] = (byte)green;
                    pixelsrgb[offset24 + 2] = (byte)red;
                }
            }
            return BitmapSource.Create(WIDTH, HEIGHT, 96, 96, PixelFormats.Bgr24, BitmapPalettes.WebPalette, pixelsrgb, WIDTH * 3);
        }

        private void ShowResultDlg()
        {
            GszReportWindow window = new GszReportWindow();
            GszReportWindow._listRGBValues = _listRGBValues;
            GszReportWindow._item = _item;
            GszReportWindow._helpBoxes = _helpBoxes;
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
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
                Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
                Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
                Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
                Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");

                _item.Hole[i].RegionLeft = _helpBoxes[i].Left = (int)(Double)rect.GetValue(Canvas.LeftProperty);
                _item.Hole[i].RegionTop = _helpBoxes[i].Top = (int)(Double)rect.GetValue(Canvas.TopProperty);
                _item.Hole[i].RegionWidth = _helpBoxes[i].Width = (int)rect.Width;
                _item.Hole[i].RegionHeight = _helpBoxes[i].Height = (int)rect.Height;
                _item.Hole[i].TOffset = _helpBoxes[i].TLineOffset = (int)(Double)lineT.GetValue(Canvas.TopProperty) - _helpBoxes[i].Top;
                _item.Hole[i].COffset = _helpBoxes[i].CLineOffset = (int)(Double)lineC.GetValue(Canvas.TopProperty) - _helpBoxes[i].Top;
            }
            for (int i = 0; i < _bTCLineNeedSetting.Length; ++i)
            {
                _bTCLineNeedSetting[i] = true;
            }
        }

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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_READ_CAM:
                        wnd._msgReadCAMReplyed = true;
                        if (msg.result)
                        {
                            byte[] data = msg.data;
                            try
                            {
                                wnd.UpdateStatus(data, msg.arg1);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("Result:" + msg.result + " CAM:" + msg.arg1);
                        }
                        else
                        {
                            Console.WriteLine("读取CAM值错误");
                            //MessageBox.Show("设备连接异常，请检查设备连接情况!", "提示");
                        }
                        break;
                    case MsgCode.MSG_READ_RGBVALUE:
                        if (msg.result)
                        {
                            wnd._bRGBValuesNeedRead[msg.arg1] = false;
                            wnd._listRGBValues[msg.arg1] = msg.data;

                            int i = 0;
                            for (i = 0; i < wnd._bRGBValuesNeedRead.Length; ++i)
                            {
                                if (wnd._bRGBValuesNeedRead[i])
                                    break;
                            }

                            if (wnd._bRGBValuesNeedRead.Length == i)
                            {
                                wnd.ButtonSampleTest.Content = "测  试";
                                wnd.ButtonSampleTest.IsEnabled = true;
                                wnd.ShowResultDlg();
                                wnd.UpdateSampleNumUI();
                            }
                        }
                        else
                        {
                            Console.WriteLine("读取RGB值错误");
                            //MessageBox.Show("设备连接异常，请检查设备连接情况!", "提示");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

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
                        while (wnd._bTimerWork)
                        {
                            // 读摄像头是一直都需要进行的。但是定时工作线程，定时设置TC线并读取摄像头。这个任务不能太复杂。
                            if (wnd._msgReadCAMReplyed)
                            {
                                wnd._msgReadCAMReplyed = false;
                                wnd.Dispatcher.Invoke(readCAMDelegate, msg);
                            }
                            else
                            {
                                Thread.Sleep(200);
                            }
                        }
                        break;
                }
            }

            private void ReadCAM(Message msg)
            {
                wnd.GetTCLine();

                // 对于不需要用到的孔位，不予显示
                while (!wnd._item.Hole[wnd._CUR_CAM].Use)
                {
                    wnd._CUR_CAM++;
                    if (wnd._CUR_CAM > wnd._MAX_CAM)
                        wnd._CUR_CAM = 0;
                }

                // 设置TC线
                if (wnd._bTCLineNeedSetting[wnd._CUR_CAM])
                {
                    wnd._bTCLineNeedSetting[wnd._CUR_CAM] = false;
                    Message msgSetTC = new Message()
                    {
                        what = MsgCode.MSG_SET_TCLINE,
                        str1 = wnd._strSXTPORT[wnd._CUR_CAM],
                        data = wnd.GenerateHelpBoxParam(wnd._helpBoxes[wnd._CUR_CAM], 0/*wnd.CUR_CAM*/)
                    };
                    Global.workThread.SendMessage(msgSetTC, null);
                }

                // 读摄像头
                Message m = new Message()
                {
                    what = MsgCode.MSG_READ_CAM,
                    str1 = wnd._strSXTPORT[wnd._CUR_CAM],
                    arg1 = wnd._CUR_CAM
                };
                Console.WriteLine("Start Read Cam " + wnd._CUR_CAM);
                Global.workThread.SendMessage(m, wnd._updateCAMThread);

                if (wnd._CUR_CAM >= wnd._MAX_CAM)
                    wnd._CUR_CAM = 0;
                else
                    ++wnd._CUR_CAM;
            }
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
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
                    MainWindow._aPlayer = new APlayerForm();
                    MainWindow._aPlayer._ItemName = _item.Name;
                    MainWindow._aPlayer.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "播放视频教程时出现异常!\r\n\r\n异常信息：" + ex.Message, "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}