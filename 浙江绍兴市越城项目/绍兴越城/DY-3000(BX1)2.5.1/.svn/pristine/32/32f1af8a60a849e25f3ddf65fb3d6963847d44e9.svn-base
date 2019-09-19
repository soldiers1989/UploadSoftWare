using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
                btnPlayer.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
                _MAX_CAM = Global.deviceHole.SxtCount - 1;
                _listSampleNum = new List<TextBox>();
                int sampleNum = _item.SampleNum;
                int holeUse = 0;
                WrapPanelChannel.Width = 0;
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        WrapPanelChannel.Width += 300;
                        holeUse += 1;
                        sampleNum++;
                        _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
                        if (_item.dx.DeltaA[i] != double.MinValue) btnContrast.Visibility = Visibility.Visible;
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
            jbkIN();
            this.Close();
        }

        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border
            {
                Width = 250,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "border"
            };

            StackPanel stackPanel = new StackPanel
            {
                Width = 250,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };

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
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);

            border.Child = stackPanel;
            return border;
        }

        private void ShowResultDlg()
        {
            GszReportWindow window = new GszReportWindow
            {
                _item = _item,
                _datas = datas,
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
            }
            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
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