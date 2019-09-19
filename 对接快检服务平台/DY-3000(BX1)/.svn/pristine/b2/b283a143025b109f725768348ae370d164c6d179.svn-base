using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        #region 新摄像头模块
        // 定时工作线程，只需要循环读取摄像头图片
        private TimerThread _timerThread;
        private bool _bTimerWork; // 需要初始化
        private bool _msgReadCAMReplyed;// 需要初始化
        private static List<ComPort> ports = null;
        bool sxtOut = false;
        bool sxtIn = false;
        bool sxtQr = false;
        bool sxtTest = false;
        #endregion

        #region 全局变量
        public DYJTJItemPara _item = null;
        private UpdateCAMThread _updateCAMThread;
        private string[] _strSXTPORT = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };
        private List<TextBox> _listSampleNum;
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private DispatcherTimer _DataTimer = null;
        private List<byte[]> _datas = null;
        private List<double[]> _curveDatas = null;
        private List<bool> _IsThereCard = new List<bool>();
        private int TestNum = 0;
        private string logType = "JtjMeasureWindow-error";
        #endregion

        public JtjMeasureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item) return;
            try
            {
                btnINorOUT.Content = "进卡/测试";
                btnPlayer.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
                ButtonBirefDescription_Copy.Visibility = Global.IsShowValue ? Visibility.Visible : Visibility.Collapsed;
                _listSampleNum = new List<TextBox>();
                int sampleNum = _item.SampleNum;
                //记录选择了几个检测通道
                int holeUse = 0;
                UIElement element = null;
                ports = new List<ComPort>();
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        holeUse += 1;
                        sampleNum++;
                        sv_panle.Width += 245;
                        _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
                        //新模块摄像头
                        if (Global.JtjVersion == 3)
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
                        element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        element.Visibility = Visibility.Collapsed;
                        WrapPanelChannel.Children.Add(element);
                        _listSampleNum.Add(null);
                        ports.Add(null);
                    }
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
            //if (Global.JtjVersion == 3)
            //{
            //    Message msg = new Message
            //    {
            //        what = MsgCode.MSG_READ_SXT_CYCLE,
            //        obj1 = ports
            //    };
            //    Global.workThread.StartWhileRead(msg, _updateCAMThread);
            //}
            //else
            //{
            _bTimerWork = true;
            _msgReadCAMReplyed = true;
            _timerThread = new TimerThread(this);
            if (Global.JtjVersion == 3)
            {
                _timerThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_TIMER_WORK
                };
                _timerThread.SendMessage(msg, null);
            }
            //进入测试界面后先出卡操作
            jbkOUT();
            countdownStart(3);
            //}
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
            //新模块
            if (Global.JtjVersion == 3)
            {
                btnINCard.Content = "进  卡";
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
            btnINorOUT.Content = "正在检测";
            //_IsFirstChannel = _item.Hole[0].Use && !_item.Hole[1].Use ? true : false;
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

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            sxtIn = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                _bTimerWork = false;
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                _timerThread.Stop();
                _updateCAMThread.Stop();
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
                    if (newTop + item.Height > rootCanvas.Height)
                        newTop = rootCanvas.Height - item.Height;
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
                    else
                    {
                        Global.nHelpBoxLeft2 = newLeft;
                        Global.nHelpBoxTop2 = newTop; 
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
                else
                {
                    CFGUtils.SaveConfig("nHelpBoxLeft2", Global.nHelpBoxLeft2.ToString());
                    CFGUtils.SaveConfig("nHelpBoxTop2", Global.nHelpBoxTop2.ToString());
                }
            }
        }

        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border
            {
                Width = 240,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "border"
            };

            StackPanel stackPanel = new StackPanel
            {
                Width = 240,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };

            Canvas rootCanvas = new Canvas()
            {
                Width = 240,
                Height = 180,
                Background = Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Visibility = Global.JtjVersion == 3 ? Visibility.Visible : Visibility.Collapsed,
                Name = "rootCanvas"
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
            rect.SetValue(Canvas.LeftProperty, channel == 0 ? Global.nHelpBoxLeft1 : Global.nHelpBoxLeft2);
            rect.SetValue(Canvas.TopProperty, channel == 0 ? Global.nHelpBoxTop1 : Global.nHelpBoxTop2);
            rect.MouseLeftButtonDown += Handle_MouseDown;
            rect.MouseMove += Handle_MouseMove;
            rect.MouseLeftButtonUp += Handle_MouseUp;

            Grid grid = new Grid
            {
                Width = 240,
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
                Width = 240,
                Height = 30
            };

            Label labelDetectPeople = new Label()
            {
                Width = 235,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = string.Format(" 检测人员:   {0}", LoginWindow._userAccount != null ? LoginWindow._userAccount.UserName : string.Empty),
                VerticalContentAlignment = VerticalAlignment.Center
            };

            WrapPanel wrapPannelSampleNum = new WrapPanel
            {
                Width = 240,
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
                Width = 135,
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
                Width = 240,
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
                Width = 135,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };

            grid.Children.Add(label);
            stackPanel.Children.Add(grid);
            rootCanvas.Children.Add(rect);
            stackPanel.Children.Add(rootCanvas);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);

            border.Child = stackPanel;
            return border;
        }

        private void ShowResultDlg()
        {
            try
            {
                JtjReportWindow window = new JtjReportWindow
                {
                    _item = _item,
                    _datas = _datas,
                    _newJtjDatas = dtList,
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
                _datas = null;
                Global.jbkIsOut = false;
                //FileUtils.Log(string.Format("JtjReportWindow - datas：{0}", datas.Count));
                btnINorOUT.IsEnabled = true;
                JbkTest();
                //如果是胶体金3.0模块，需要更新取色框的位置
                if (Global.JtjVersion == 3)
                {
                    List<Rectangle> rects = GetChildObjects<Rectangle>(WrapPanelChannel, "rect");
                    if (rects != null)
                    {
                        if (rects[0] != null)
                        {
                            rects[0].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft1);
                            rects[0].SetValue(Canvas.TopProperty, Global.nHelpBoxTop1);
                        }
                        if (rects[1] != null)
                        {
                            rects[1].SetValue(Canvas.LeftProperty, Global.nHelpBoxLeft2);
                            rects[1].SetValue(Canvas.TopProperty, Global.nHelpBoxTop2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, "ShowResultDlg-error", ex.ToString());
            }
        }

        /// <summary>
        /// 进卡or出卡倒计时
        /// </summary>
        /// <param name="num">时间 s</param>
        private void countdownStart(int s)
        {
            btnINorOUT.IsEnabled = false;
            btnINorOUT.Content = "进卡/测试";
            if (Global.JtjVersion == 3)
            {
                btnINCard.Visibility = Visibility.Visible;
                btnINCard.IsEnabled = false;
            }
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
            if (Global.JtjVersion == 3)
            {
                btnINCard.IsEnabled = true;
            }
        }

        /// <summary>
        /// 记录点击测试时的准确时间
        /// </summary>
        DateTime testDate;
        private void btnINorOUT_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string val = "126,22,0,180,255,34,46,38,237,40,113,43,149,45,59,47,63,48,172,48,201,48,202,48,219,48,243,48,181,48,223,47,158,46,105,45,145,44,250,43,167,43,117,43,68,43,43,43,53,43,80,43,80,43,69,43,31,43,138,42,104,41,214,39,1,38,104,36,162,35,214,35,17,37,224,38,144,40,216,41,180,42,51,43,108,43,131,43,144,43,144,43,87,43,207,42,224,41,125,40,208,38,56,37,20,36,241,35,236,36,130,38,63,40,169,41,107,42,208,42,38,43,101,43,152,43,222,43,61,44,184,44,95,45,17,46,188,46,188,47,245,49,187,54,169,61,221,67,159,70,133,68,171,61,164,52,246,44,172,39,107,35,89,31,91,27,68,23,21,19,6,15,80,11,80,8,230,5,233,3,169,2,10,2,92,126";
            //    string[] vals = val.Split(',');
            //    byte[] dts = new byte[vals.Length];
            //    for (int i = 0; i < vals.Length; i++)
            //    {
            //        dts[i] = byte.Parse(vals[i]);
            //    }
            //    datas = new List<byte[]>();
            //    datas.Add(dts);
            //    datas.Add(null);
            //    JtjReportWindow window = new JtjReportWindow();
            //    window._item = _item;
            //    window._datas = datas;
            //    window.ShowInTaskbar = false;
            //    window.Owner = this;
            //    window.ShowDialog();
            //    datas = null;
            //    Global.jbkIsOut = false;
            //    //FileUtils.Log(string.Format("JtjReportWindow - datas：{0}", datas.Count));
            //    JbkTest();
            //}
            //catch (Exception ex)
            //{
            //    FileUtils.OprLog(2, "ShowResultDlg-error", ex.ToString());
            //}
            //return;

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
            Global.JbkCheckCalc._jtjItem = _item;
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
            countdownStart(Global.jbkIsOut ? 30 : 4);
            jbkINorOUT();
        }

        /// <summary>
        /// 进出卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnINCard_Click(object sender, RoutedEventArgs e)
        {
            //新模块
            if (btnINCard.Content.ToString().Equals("进  卡"))
            {
                sxtIn = true;
                btnINCard.Content = "出  卡";
            }
            else
            {
                sxtOut = true;
                btnINCard.Content = "进  卡";
            }
            countdownStart(5);
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

        DispatcherTimer _DTimer = null;
        int count = 0;
        bool isIN = true;
        private void ButtonBirefDescription_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonBirefDescription_Copy.Content.Equals("通道老化"))
            {
                ButtonBirefDescription_Copy.Content = "停止老化";
                _DTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                _DTimer.Tick += new EventHandler(lhTimerTick);
                _DTimer.Start();
            }
            else
            {
                ButtonBirefDescription_Copy.Content = "通道老化";
                _DTimer.Stop();
                _DTimer = null;
            }
        }


        private void lhTimerTick(object sender, EventArgs e)
        {
            count--;
            if (count > 0) return;
            if (isIN)
            {
                jbkIN();
                isIN = false;
                count = 5;
            }
            else
            {
                jbkOUT();
                isIN = true;
                count = 5;
            }
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
            catch (Exception)
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

        //旧扫描模块工作线程
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
                    //检测是否有卡
                    case MsgCode.MSG_JBK_CKC:
                        wnd._IsThereCard.Add(msg.result);
                        if (wnd.TestNum == wnd._IsThereCard.Count)
                        {
                            bool isTest = true;
                            for (int i = 0; i < wnd._IsThereCard.Count; i++)
                            {
                                //如果启用了薄层色谱，则二通道不验证是否有放卡
                                if (Global.IsEnableBcsp && i == 1)
                                {
                                    isTest = true;
                                    break;
                                }

                                if (!wnd._IsThereCard[i])
                                {
                                    isTest = false;
                                    break;
                                }
                            }
                            if (isTest)
                            {
                                //胶体金测试
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
                            wnd._datas = msg.datas;
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
                            }
                            else
                            {
                                wnd.isSxtTest = true;
                                wnd.dtList = new List<byte[]>[Global.deviceHole.SxtCount];
                                for (int i = 0; i < Global.deviceHole.SxtCount; i++)
                                {
                                    List<byte[]> dt = new List<byte[]>();
                                    wnd.dtList[i] = dt;
                                }
                            }
                        }

                        //绘图
                        List<byte[]> datas = msg.obj2 as List<byte[]>;
                        for (int i = 0; i < datas.Count; i++)
                        {
                            if (wnd.isSxtTest)
                            {
                                 //点击测试后，需要等待进卡5s后才开始采集图像
                                if ((DateTime.Now - wnd.testDate).TotalSeconds >= 5)
                                {
                                    wnd.dtList[i].Add(datas[i]);
                                    //采集十张图片，算CT值
                                    if (wnd.dtList[0].Count >= Global.LineCount && wnd.dtList[1].Count >= Global.LineCount)
                                    {
                                        wnd.ShowResultDlg();
                                        wnd.isSxtTest = false;
                                        for (int j = 0; j < wnd.dtList.Length; j++)
                                        {
                                            wnd.dtList[i].Clear();
                                        }
                                    }
                                }
                            }
                            if (datas[i] == null) continue;
                            wnd.CreateImg(datas[i], i);
                        }

                        DateUtils.WaitMs(10);
                        wnd._msgReadCAMReplyed = true;
                        break;

                    //新摄像头模组(胶体金3.0)
                    case MsgCode.MSG_READ_SXT_CYCLE:
                        //绘图
                        //List<byte[]> dts = msg.obj2 as List<byte[]>;
                        //for (int i = 0; i < dts.Count; i++)
                        //{
                        //    if (wnd.isSxtTest)
                        //    {
                        //        wnd.dtList[i].Add(dts[i]);
                        //        //采集十张图片，算CT值
                        //        if (wnd.dtList[0].Count >= Global.LineCount && wnd.dtList[1].Count >= Global.LineCount)
                        //        {
                        //            wnd.ShowResultDlg();
                        //            wnd.isSxtTest = false;
                        //            for (int j = 0; j < wnd.dtList.Length; j++)
                        //            {
                        //                wnd.dtList[i].Clear();
                        //            }
                        //        }
                        //    }
                        //    if (dts[i] == null) continue;
                        //    wnd.CreateImg(dts[i], i);
                        //}
                        break;

                    default:
                        break;
                }
            }
        }

        class TimerThread : ChildThread
        {
            JtjMeasureWindow wnd;
            private delegate void ReadCAMDelegate(Message msg);
            private ReadCAMDelegate readCAMDelegate;

            public TimerThread(JtjMeasureWindow wnd)
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
                            if (wnd._msgReadCAMReplyed)
                            {
                                wnd._msgReadCAMReplyed = false;
                                wnd.Dispatcher.Invoke(readCAMDelegate, msg);
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

        bool isSxtTest = false;
        List<byte[]>[] dtList = new List<byte[]>[Global.deviceHole.SxtCount];
    }
}