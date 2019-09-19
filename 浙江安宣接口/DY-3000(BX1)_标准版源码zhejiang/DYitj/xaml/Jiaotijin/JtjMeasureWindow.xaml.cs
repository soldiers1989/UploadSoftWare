using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using AIO.xaml.Dialog;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// JtjMeasureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjMeasureWindow : Window
    {

        #region 全局变量
        public DYJTJItemPara _item = null;
        private UpdateCAMThread _updateCAMThread;
        // 定时工作线程，只需要循环读取摄像头图片
        //private TimerThread _timerThread;
        //private bool _bTimerWork; // 需要初始化
        //private bool _msgReadCAMReplyed;// 需要初始化
        private int _MAX_CAM = 0;
        //private int _CUR_CAM = 0;
        // 设置TC线有几个检测通道
        private bool[] _bTCLineNeedSetting;
        private HelpBox[] _helpBoxes;
        // 读取灰阶值并保存起来
        private bool[] _bGrayValuesNeedRead; // 当bGrayValuesNeedRead都是false的时候，开始启动新窗口
        private List<byte[]> _listGrayValues;
        private string[] _strSXTPORT = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };
        private List<TextBox> _listSampleNum;
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        //private bool _isMouseCaptured;
        private double _mouseVerticalPosition;
        private double _mouseHorizontalPosition;
        private DispatcherTimer _DataTimer = null;
        private List<byte[]> datas = null;
        /// <summary>
        /// 是否进行复检，初始为true需要复检，判定结果为阴性后则直接false
        /// </summary>
        //private List<bool> jtjFJ = new List<bool>();
        /// <summary>
        /// 复检次数
        /// </summary>
        //private List<int> jtjFJCount = new List<int>();
        //private bool _IsFirstChannel = false;
        private List<bool> _IsThereCard = new List<bool>();
        private int TestNum = 0;
        /// <summary>
        /// 版本信息验证是否通过
        /// </summary>
        //private bool _versionIsOk = false;
        private string logType = "JtjMeasureWindow-error";
        #endregion

        public JtjMeasureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;
            try
            {
                WrapPanelChannel.Width = Global.deviceHole.SxtCount * 190;
                _MAX_CAM = Global.deviceHole.SxtCount - 1;
                _listSampleNum = new List<TextBox>();
                int sampleNum = _item.SampleNum;
                //记录选择了几个检测通道
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
                // 初始化辅助方框及TC线的参数
                _bTCLineNeedSetting = new bool[Global.deviceHole.SxtCount];
                _helpBoxes = new HelpBox[Global.deviceHole.SxtCount];
                for (int i = 0; i < _bTCLineNeedSetting.Length; ++i)
                {
                    _bTCLineNeedSetting[i] = false;
                    _helpBoxes[i] = new HelpBox();
                }

                // 初始化灰阶值存储位置
                _bGrayValuesNeedRead = new bool[Global.deviceHole.SxtCount];
                _listGrayValues = new List<byte[]>();
                for (int i = 0; i < _bGrayValuesNeedRead.Length; ++i)
                {
                    _bGrayValuesNeedRead[i] = false;
                    _listGrayValues.Add(null);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            // 启动线程
            _updateCAMThread = new UpdateCAMThread(this);
            _updateCAMThread.Start();
            //_bTimerWork = true;
            //_msgReadCAMReplyed = true;
            //_timerThread = new TimerThread(this);
            //_timerThread.Start();
            //进入测试界面后先出卡操作
            jbkOUT();
            countdownStart(3);
        }

        private void jbkINorOUT()
        {
            if (Global.jbkIsOut)
                jbkInAndTest();
            else
                jbkOUT();
        }

        /// <summary>
        /// 出卡
        /// </summary>
        private void jbkOUT()
        {
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    Message msg = new Message();
                    msg.what = MsgCode.MSG_JBK_OUT;
                    msg.str1 = _strSXTPORT[i];
                    Global.workThread.SendMessage(msg, _updateCAMThread);
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
            //_IsFirstChannel = _item.Hole[0].Use && !_item.Hole[1].Use ? true : false;
            Message msg = new Message();
            msg.what = MsgCode.MSG_JBK_InAndTest;
            msg.ports = new List<string>();
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
                Message msg = new Message();
                msg.what = MsgCode.MSG_JBK_IN;
                msg.str1 = _strSXTPORT[i];
                Global.workThread.SendMessage(msg, null);
            }
            Global.jbkIsOut = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            //_timerThread.Stop();
            _updateCAMThread.Stop();
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, _item.Name);
            try
            {
                if (File.Exists(path))
                {
                    TechnologeDocument window = new TechnologeDocument();
                    window.path = path;
                    window.ShowInTaskbar = false;
                    window.Topmost = true;
                    window.Owner = this;
                    window.Show();
                }
                else
                {
                    Global.IsOpenPrompt = true;
                    PromptWindow window = new PromptWindow();
                    window._HintStr = _item.HintStr;
                    window.Show();
                }
            }
            catch (Exception)
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow();
                window._HintStr = _item.HintStr;
                window.Show();
            }
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            jbkIN();
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
                    _bGrayValuesNeedRead[i] = false;
                    continue;
                }
                else
                {
                    _bGrayValuesNeedRead[i] = true;
                }

                Message msg = new Message();
                msg.what = MsgCode.MSG_READ_GRAYVALUES;
                msg.str1 = _strSXTPORT[i];
                msg.arg1 = i;
                msg.arg2 = _helpBoxes[i].Height;
                Global.workThread.SendMessage(msg, _updateCAMThread);
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
            btnINorOUT.Content = "出卡/放卡";
        }

        public void Handle_MouseDown(object sender, MouseEventArgs args)
        {
            StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
            if ((lineT == sender) || (lineC == sender))
            {
                Line item = sender as Line;
                _mouseVerticalPosition = args.GetPosition(null).Y;
                _mouseHorizontalPosition = args.GetPosition(null).X;
                //_isMouseCaptured = true;
                item.CaptureMouse();
            }
            else if (rect == sender)
            {
                Rectangle item = sender as Rectangle;
                _mouseVerticalPosition = args.GetPosition(null).Y;
                _mouseHorizontalPosition = args.GetPosition(null).X;
                //_isMouseCaptured = true;
                item.CaptureMouse();
            }
        }

        public void Handle_MouseMove(object sender, MouseEventArgs args)
        {
            //StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            //Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            //Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            //Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
            //Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");

            //// C线在上面
            //if (lineC == sender)
            //{
            //    Line item = sender as Line;
            //    if (_isMouseCaptured)
            //    {
            //        double deltaV = args.GetPosition(null).Y - _mouseVerticalPosition;
            //        double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
            //        double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
            //        double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
            //        double rectTop = (double)rect.GetValue(Canvas.TopProperty);
            //        if (newTop < rectTop + 10)
            //            newTop = rectTop + 10;
            //        if (newTop > lineTTop - 10)
            //            newTop = lineTTop - 10;
            //        item.SetValue(Canvas.TopProperty, newTop);
            //        _mouseVerticalPosition = args.GetPosition(null).Y;
            //        _mouseHorizontalPosition = args.GetPosition(null).X;
            //    }
            //}
            //else if (lineT == sender)
            //{   
            //    // T线在下面
            //    Line item = sender as Line;
            //    if (_isMouseCaptured)
            //    {

            //        double deltaV = args.GetPosition(null).Y - _mouseVerticalPosition;
            //        double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);

            //        double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
            //        double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
            //        double rectTop = (double)rect.GetValue(Canvas.TopProperty);

            //        if (newTop < lineCTop + 10)
            //            newTop = lineCTop + 10;
            //        if (newTop > rectTop + rect.Height - 10)
            //            newTop = rectTop + rect.Height - 10;

            //        item.SetValue(Canvas.TopProperty, newTop);

            //        _mouseVerticalPosition = args.GetPosition(null).Y;
            //        _mouseHorizontalPosition = args.GetPosition(null).X;
            //    }
            //}
            //else if (rect == sender)
            //{
            //    Rectangle item = sender as Rectangle;
            //    if (_isMouseCaptured)
            //    {
            //        double deltaV = args.GetPosition(null).Y - _mouseVerticalPosition;
            //        double deltaH = args.GetPosition(null).X - _mouseHorizontalPosition;
            //        double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
            //        double newLeft = deltaH + (double)item.GetValue(Canvas.LeftProperty);

            //        double lineTTop = (double)lineT.GetValue(Canvas.TopProperty);
            //        double lineCTop = (double)lineC.GetValue(Canvas.TopProperty);
            //        double rectTop = (double)rect.GetValue(Canvas.TopProperty);

            //        double distanceT = lineTTop - rectTop;
            //        double distanceC = lineCTop - rectTop;

            //        if (newTop < 0)
            //            newTop = 0;
            //        if (newLeft < 0)
            //            newLeft = 0;
            //        if (newTop + item.Height > rootCanvas.Height)
            //            newTop = rootCanvas.Height - item.Height;
            //        if (newLeft + item.Width > rootCanvas.Width)
            //            newLeft = rootCanvas.Width - item.Width;

            //        item.SetValue(Canvas.TopProperty, newTop);
            //        item.SetValue(Canvas.LeftProperty, newLeft);
            //        lineT.SetValue(Canvas.TopProperty, newTop + distanceT);
            //        lineT.SetValue(Canvas.LeftProperty, newLeft);
            //        lineC.SetValue(Canvas.TopProperty, newTop + distanceC);
            //        lineC.SetValue(Canvas.LeftProperty, newLeft);

            //        _mouseVerticalPosition = args.GetPosition(null).Y;
            //        _mouseHorizontalPosition = args.GetPosition(null).X;
            //    }
            //}
        }

        public void Handle_MouseUp(object sender, MouseEventArgs args)
        {
            //StackPanel stackPanel = UIUtils.GetParentObject<StackPanel>((DependencyObject)sender, "stackPanel");
            //Line lineT = UIUtils.GetChildObject<Line>(stackPanel, "lineT");
            //Line lineC = UIUtils.GetChildObject<Line>(stackPanel, "lineC");
            //Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");

            //if ((lineT == sender) || (lineC == sender))
            //{
            //    Line item = sender as Line;
            //    _isMouseCaptured = false;
            //    item.ReleaseMouseCapture();
            //    _mouseVerticalPosition = -1;
            //    _mouseHorizontalPosition = -1;
            //}
            //else if (rect == sender)
            //{
            //    Rectangle item = sender as Rectangle;
            //    _isMouseCaptured = false;
            //    item.ReleaseMouseCapture();
            //    _mouseVerticalPosition = -1;
            //    _mouseHorizontalPosition = -1;
            //}
        }

        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border();
            border.Width = 185;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = _borderBrushNormal;
            border.CornerRadius = new CornerRadius(10);
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 185;
            stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 185;
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            grid.Height = 40;

            Label label = new Label();
            label.FontSize = 20;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.Content = "检测通道" + (channel + 1);

            Canvas rootCanvas = new Canvas();
            rootCanvas.Width = 185;
            rootCanvas.Height = 200;
            rootCanvas.Background = Brushes.Gray;
            rootCanvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            rootCanvas.Name = "rootCanvas";

            double nHelpBoxWidth = _item.Hole[channel].RegionWidth;
            double nHelpBoxHeight = _item.Hole[channel].RegionHeight;
            double nHelpBoxLeft = _item.Hole[channel].RegionLeft;
            double nHelpBoxTop = _item.Hole[channel].RegionTop;
            double nHelpBoxCOffset = _item.Hole[channel].COffset;
            double nHelpBoxTOffset = _item.Hole[channel].TOffset;

            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop(Colors.Red, 0.00));
            gradientStops.Add(new GradientStop(Colors.Red, 0.39));
            gradientStops.Add(new GradientStop(Colors.Transparent, 0.40));
            gradientStops.Add(new GradientStop(Colors.Transparent, 0.60));
            gradientStops.Add(new GradientStop(Colors.Red, 0.61));
            gradientStops.Add(new GradientStop(Colors.Red, 1.00));
            LinearGradientBrush lineBrush = new LinearGradientBrush(gradientStops, new Point(0, 0), new Point(1, 0));

            WrapPanel wrapPannelSampleNum = new WrapPanel();
            wrapPannelSampleNum.Width = 180;
            wrapPannelSampleNum.Height = 30;

            Label labelSampleNum = new Label();
            labelSampleNum.Width = 85;
            labelSampleNum.Height = 26;
            labelSampleNum.Margin = new Thickness(0, 2, 0, 0);
            labelSampleNum.FontSize = 15;
            labelSampleNum.Content = " 样品编号:";
            labelSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleNum = new TextBox();
            textBoxSampleNum.Width = 95;
            textBoxSampleNum.Height = 26;
            textBoxSampleNum.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleNum.FontSize = 15;
            textBoxSampleNum.Text = "" + sampleNum;
            textBoxSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleNum.IsReadOnly = true;
            textBoxSampleNum.Name = "sampleNum";

            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 180;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 85;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = " 样品名称:";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 95;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = "" + _item.Hole[channel].SampleName;
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.IsReadOnly = true;

            //rootCanvas.Children.Add(lineC);
            //rootCanvas.Children.Add(lineT);
            //rootCanvas.Children.Add(rect);
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

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = BufferToBitmap(data);
            ib.TileMode = TileMode.None;
            ib.Stretch = Stretch.None;
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
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
            JtjReportWindow window = new JtjReportWindow();
            window._item = _item;
            window._datas = datas;
            window._helpBoxes = _helpBoxes;
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
            datas = null;
            Global.jbkIsOut = false;
            JbkTest();
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
                Rectangle rect = UIUtils.GetChildObject<Rectangle>(stackPanel, "rect");
                Canvas rootCanvas = UIUtils.GetChildObject<Canvas>(stackPanel, "rootCanvas");
                _item.Hole[i].RegionLeft = _helpBoxes[i].Left = (int)(Double)rect.GetValue(Canvas.LeftProperty);
                _item.Hole[i].RegionTop = _helpBoxes[i].Top = (int)(Double)rect.GetValue(Canvas.TopProperty);
                _item.Hole[i].RegionWidth = _helpBoxes[i].Width = (int)rect.Width;
                _item.Hole[i].RegionHeight = _helpBoxes[i].Height = (int)rect.Height;
            }
            for (int i = 0; i < _bTCLineNeedSetting.Length; ++i)
            {
                _bTCLineNeedSetting[i] = true;
            }
        }

        class UpdateCAMThread : ChildThread
        {
            JtjMeasureWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public UpdateCAMThread(JtjMeasureWindow wnd)
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
                    FileUtils.OprLog(2, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    #region
                    //case MsgCode.MSG_READ_CAM:
                    //    wnd._msgReadCAMReplyed = true;
                    //    if (msg.result)
                    //    {
                    //        byte[] data = msg.data;
                    //        try
                    //        {
                    //            wnd.UpdateStatus(data, msg.arg1);
                    //        }
                    //        catch (Exception e)
                    //        {
                    //            Console.WriteLine(e.Message);
                    //        }
                    //        Console.WriteLine("Result:" + msg.result + " CAM:" + msg.arg1);
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("读取CAM值错误");
                    //    }
                    //    break;
                    //case MsgCode.MSG_READ_GRAYVALUES:
                    //    if (msg.result)
                    //    {
                    //        wnd._bGrayValuesNeedRead[msg.arg1] = false;
                    //        wnd._listGrayValues[msg.arg1] = msg.data;

                    //        int i = 0;
                    //        for (i = 0; i < wnd._bGrayValuesNeedRead.Length; ++i)
                    //        {
                    //            if (wnd._bGrayValuesNeedRead[i])
                    //                break;
                    //        }
                    //        if (wnd._bGrayValuesNeedRead.Length == i)
                    //        {
                    //            wnd.ButtonSampleTest.Content = "测  试";
                    //            wnd.ButtonSampleTest.IsEnabled = true;
                    //            wnd.ShowResultDlg(msg.data);
                    //            wnd.UpdateSampleNumUI();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("读取灰阶值错误");
                    //    }
                    //    break;

                    //case MsgCode.MSG_JBK_OUT:
                    //    if (!msg.result)
                    //    {
                    //        MessageBox.Show("设备连接异常，请检查设备连接情况！", "提示");
                    //    }
                    //    break;
                    #endregion
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
                            //for (int i = 0; i < msg.datas.Count; i++)
                            //{
                            //    if (wnd.jtjFJ[i] && wnd.jtjFJCount[i] > 0)//如果当前卡为阴性则直接赋值
                            //    {
                            //        //验证是否阴性
                            //        wnd.datas[i] = msg.datas[i];
                            //        wnd.jtjFJ[i] = false;
                            //        wnd.jtjFJCount[i] = 0;
                            //    }
                            //    else//如果当前值为阳性或可疑，则复检一次
                            //    {

                            //    }
                            //}
                            wnd.ShowResultDlg();
                            wnd.UpdateSampleCode();
                            wnd.btnINorOUT.IsEnabled = true;
                        }
                        break;
                    case MsgCode.MSG_GET_VERSION:
                        if (msg.result)
                        {
                            msg.result = false;
                        }
                        else
                        {
                            Console.WriteLine("版本信息不正确");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #region
        // 要循环处理的任务，不能派给UI线程做
        //class TimerThread : ChildThread
        //{
        //    JtjMeasureWindow wnd;
        //    private delegate void ReadCAMDelegate(Message msg);
        //    private ReadCAMDelegate readCAMDelegate;

        //    public TimerThread(JtjMeasureWindow wnd)
        //    {
        //        this.wnd = wnd;
        //        readCAMDelegate = new ReadCAMDelegate(readCAM);
        //    }

        //    protected override void HandleMessage(Message msg)
        //    {
        //        base.HandleMessage(msg);
        //        switch (msg.what)
        //        {
        //            case MsgCode.MSG_TIMER_WORK:
        //                while (wnd._bTimerWork)
        //                {
        //                    if (wnd._msgReadCAMReplyed)
        //                    {
        //                        wnd._msgReadCAMReplyed = false;
        //                        wnd.Dispatcher.Invoke(readCAMDelegate, msg);
        //                    }
        //                    else
        //                    {
        //                        Thread.Sleep(200);
        //                    }
        //                };
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    private void readCAM(Message msg)
        //    {
        //        wnd.GetTCLine();

        //        // 对于不需要用到的孔位，不予显示
        //        while (!wnd._item.Hole[wnd._CUR_CAM].Use)
        //        {
        //            wnd._CUR_CAM++;
        //            if (wnd._CUR_CAM > wnd._MAX_CAM)
        //                wnd._CUR_CAM = 0;
        //        }

        //        // 设置TC线
        //        if (wnd._bTCLineNeedSetting[wnd._CUR_CAM])
        //        {
        //            wnd._bTCLineNeedSetting[wnd._CUR_CAM] = false;
        //            Message msgSetTC = new Message();
        //            msgSetTC.what = MsgCode.MSG_SET_TCLINE;
        //            msgSetTC.str1 = wnd._strSXTPORT[wnd._CUR_CAM];
        //            msgSetTC.data = wnd.GenerateHelpBoxParam(wnd._helpBoxes[wnd._CUR_CAM], 0/*wnd.CUR_CAM*/);
        //            Global.workThread.SendMessage(msgSetTC, null);
        //        }

        //        // 读摄像头
        //        Message m = new Message();
        //        m.what = MsgCode.MSG_READ_CAM;
        //        m.str1 = wnd._strSXTPORT[wnd._CUR_CAM];
        //        m.arg1 = wnd._CUR_CAM;
        //        Console.WriteLine("Start Read Cam " + wnd._CUR_CAM);
        //        Global.workThread.SendMessage(m, wnd._updateCAMThread);

        //        if (wnd._CUR_CAM >= wnd._MAX_CAM)
        //            wnd._CUR_CAM = 0;
        //        else
        //            ++wnd._CUR_CAM;
        //    }
        //}
        #endregion

        /// <summary>
        /// 进卡or出卡倒计时
        /// </summary>
        /// <param name="num">时间 s</param>
        private void countdownStart(int s)
        {
            btnINorOUT.IsEnabled = false;
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
            btnINorOUT.IsEnabled = true;
            btnINorOUT.Content = "进卡/测试";
        }

        private void btnINorOUT_Click(object sender, RoutedEventArgs e)
        {
            TestNum = 0;
            _IsThereCard = new List<bool>();
            //检测是否有卡
            Global.IsStartGetBattery = false;
            Global.JtjCheck._item = _item;
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    TestNum++;
                    Message msg = new Message();
                    msg.what = MsgCode.MSG_JBK_CKC;
                    msg.str1 = _strSXTPORT[i];
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