using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using AIO.xaml.Dialog;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// GszMeasureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszMeasureWindow : Window
    {

        #region 全局变量
        public DYGSZItemPara _item = null;
        private UpdateCAMThread _updateCAMThread;
        // 定时工作线程，只需要循环读取摄像头图片
        // 底层把图片读到之后会返回给FgdMeasureThread
        private TimerThread _timerThread;
        private bool _bTimerWork = true; // 需要初始化
        private bool _msgReadCAMReplyed = true;// 需要初始化
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
        #endregion

        public GszMeasureWindow()
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
                int holeUse = 0;
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);

                        holeUse += 1;
                        sampleNum++;
                        _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
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
                _updateCAMThread = new UpdateCAMThread(this);
                _updateCAMThread.Start();
                _bTimerWork = true;
                _msgReadCAMReplyed = true;
                _timerThread = new TimerThread(this);
                _timerThread.Start();
                jbkOUT();
                countdownStart(3);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            _timerThread.Stop();
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
            btnINorOUT.IsEnabled = ButtonSampleTest.IsEnabled = false;
            //如果未进卡则先进卡
            if (Global.jbkIsOut)
            {
                jbkIN();
                DateUtils.WaitMs(4500);
            }
            ButtonSampleTest.Content = "正在测试";
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
                Message msg = new Message();
                msg.what = MsgCode.MSG_READ_RGBVALUE;
                msg.str1 = _strSXTPORT[i];
                msg.arg1 = i;
                msg.arg2 = _helpBoxes[i].Height;
                Global.workThread.SendMessage(msg, _updateCAMThread);
            }
        }

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateSampleNumUI()
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
            btnINorOUT.IsEnabled = true;
        }

        // 干试纸法CT线距离上边框为0，并且隐藏起来。
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
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
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

            //Line lineC = new Line();
            //lineC.X1 = 0;
            //lineC.Y1 = 0;
            //lineC.X2 = nHelpBoxWidth;
            //lineC.Y2 = 0;
            //lineC.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            //lineC.SetValue(Canvas.TopProperty, nHelpBoxTop + nHelpBoxCOffset);
            //lineC.Stroke = lineBrush;
            //lineC.StrokeThickness = 5;
            //lineC.MouseLeftButtonDown += Handle_MouseDown;
            //lineC.MouseMove += Handle_MouseMove;
            //lineC.MouseLeftButtonUp += Handle_MouseUp;
            //lineC.Name = "lineC";
            //lineC.Visibility = System.Windows.Visibility.Hidden;

            //Line lineT = new Line();
            //lineT.X1 = 0;
            //lineT.Y1 = 0;
            //lineT.X2 = nHelpBoxWidth;
            //lineT.Y2 = 0;
            //lineT.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            //lineT.SetValue(Canvas.TopProperty, nHelpBoxTop + nHelpBoxTOffset);
            //lineT.Stroke = lineBrush;
            //lineT.StrokeThickness = 5;
            //lineT.MouseLeftButtonDown += Handle_MouseDown;
            //lineT.MouseMove += Handle_MouseMove;
            //lineT.MouseLeftButtonUp += Handle_MouseUp;
            //lineT.Name = "lineT";
            //lineT.Visibility = System.Windows.Visibility.Hidden;

            //Rectangle rect = new Rectangle();
            //rect.Width = nHelpBoxWidth;
            //rect.Height = nHelpBoxHeight;
            //rect.SetValue(Canvas.LeftProperty, nHelpBoxLeft);
            //rect.SetValue(Canvas.TopProperty, nHelpBoxTop);
            //rect.Stroke = Brushes.Red;
            //rect.StrokeThickness = 4;
            //rect.MouseLeftButtonDown += Handle_MouseDown;
            //rect.MouseMove += Handle_MouseMove;
            //rect.MouseLeftButtonUp += Handle_MouseUp;
            //rect.Name = "rect";

            WrapPanel wrapPannelSampleNum = new WrapPanel();
            wrapPannelSampleNum.Width = 185;
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
            wrapPannelSampleName.Width = 185;
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

        private void ShowResultDlg(List<byte[]> datas)
        {
            GszReportWindow window = new GszReportWindow();
            window._listRGBValues = _listRGBValues;
            window._item = _item;
            window._datas = datas;
            window._helpBoxes = _helpBoxes;
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
            datas = null;
            //btnINorOUT.Content = "出卡/放卡";
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

        private void GetTCLine()
        {
            try
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
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
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
                    //case MsgCode.MSG_READ_RGBVALUE:
                    //    if (msg.result)
                    //    {
                    //        wnd._bRGBValuesNeedRead[msg.arg1] = false;
                    //        wnd._listRGBValues[msg.arg1] = msg.data;

                    //        int i = 0;
                    //        for (i = 0; i < wnd._bRGBValuesNeedRead.Length; ++i)
                    //        {
                    //            if (wnd._bRGBValuesNeedRead[i])
                    //                break;
                    //        }

                    //        if (wnd._bRGBValuesNeedRead.Length == i)
                    //        {
                    //            wnd.ButtonSampleTest.Content = "测  试";
                    //            wnd.ButtonSampleTest.IsEnabled = true;
                    //            wnd.ShowResultDlg(msg.data);
                    //            wnd.UpdateSampleNumUI();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("读取RGB值错误");
                    //    }
                    //    break;
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
                                MessageBox.Show("请放入金标卡！", "系统提示");
                            }
                        }
                        break;

                    case MsgCode.MSG_JBK_InAndTest:
                        if (msg.result)
                        {
                            msg.result = false;
                            wnd.ShowResultDlg(msg.datas);
                            wnd.UpdateSampleNumUI();
                            wnd.btnINorOUT.IsEnabled = true;
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
                readCAMDelegate = new ReadCAMDelegate(readCAM);
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

            private void readCAM(Message msg)
            {
                wnd.GetTCLine();
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
            for (int i = 0; i < Global.deviceHole.SxtCount; i++)
            {
                if (_item.Hole[i].Use)
                {
                    Message msg = new Message();
                    msg.what = MsgCode.MSG_JBK_OUT;
                    msg.str1 = _strSXTPORT[i];
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

        private void btnINorOUT_Click(object sender, RoutedEventArgs e)
        {
            TestNum = 0;
            //检测是否有卡
            _IsThereCard = new List<bool>();
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
            //countdownStart(Global.jbkIsOut ? 30 : 3);
            //jbkINorOUT();
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