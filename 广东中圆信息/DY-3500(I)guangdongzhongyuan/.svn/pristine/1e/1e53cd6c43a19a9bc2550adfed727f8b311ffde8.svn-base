using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.lvrenyang;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// FgdTest.xaml 的交互逻辑
    /// </summary>
    public partial class FgdTest : Window
    {
        public FgdTest()
        {
            InitializeComponent();
        }

        #region 全局变量
        private UpdateADThread _updateADThread;
        private List<Label> _list0 = null;
        private List<Label> _list1 = null;
        private List<Label> _list2 = null;
        private List<Label> _list3 = null;
        private List<Label> _list4 = null;
        private List<Label> _list5 = null;
        private List<Label> _list6 = null;
        private List<Label> _list7 = null;
        private List<Label> _list8 = null;
        private FgdCaculate.HolesAD _HolesFullAD, _HolesCurAD;
        //FgdCaculate.AT[] _firstATs, _lastATs, _curATs;
        private int _adClear = 0;
        //一个通道四个灯的最大值和最小值
        private double _Max1 = 0, _Max2 = 0, _Max3 = 0, _Max4 = 0, _Min1 = 0, _Min2 = 0, _Min3 = 0, _Min4 = 0;
        private double _num1 = 0, _num2 = 0, _num3 = 0, _num4 = 0;
        private int _tglMax1 = 0, _tglMax2 = 0, _tglMax3 = 0, _tglMax4 = 0;
        private int _tglMin1 = 0, _tglMin2 = 0, _tglMin3 = 0, _tglMin4 = 0;
        private List<bool> _boolL = new List<bool>();
        private List<double> adList = new List<double>();
        private List<double> tgList = new List<double>();
        private int _numbool = 0;
        private bool isRead = false;
        double Max1 = 0, Max2 = 0, Max3 = 0, Max4 = 0, Min1 = 0, Min2 = 0, Min3 = 0, Min4 = 0;
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.IsClear = true;
            try
            {
                if (!Global.IsTest)
                {
                    Read.Visibility = Visibility.Hidden;
                    Conclusion.Visibility = Visibility.Hidden;
                    label1.Visibility = Visibility.Hidden;
                    label2.Visibility = Visibility.Hidden;
                    label3.Visibility = Visibility.Hidden;
                    label4.Visibility = Visibility.Hidden;
                }
                this.WindowState = System.Windows.WindowState.Normal; this.WindowStyle = System.Windows.WindowStyle.None; this.ResizeMode = System.Windows.ResizeMode.NoResize; this.Left = 0.0; this.Top = 0.0; this.Width = System.Windows.SystemParameters.PrimaryScreenWidth; this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                _list0 = new List<Label>();
                _list1 = new List<Label>();
                _list2 = new List<Label>();
                _list3 = new List<Label>();
                _list4 = new List<Label>();

                _list5 = new List<Label>();
                _list6 = new List<Label>();
                _list7 = new List<Label>();
                _list8 = new List<Label>();
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    UIElement element = GenerateChannelADForm("" + (i + 1));
                    StackPanelShowAD.Children.Add(element);
                }
                _updateADThread = new UpdateADThread(this);
                _updateADThread.Start();
                Message msg = new Message
                {
                    what = MsgCode.MSG_READ_AD_CYCLE,
                    str1 = Global.strADPORT
                };
                Global.workThread.StartWhileRead(msg, _updateADThread);
                Read.Focus();
            }
            catch (Exception ex)
            {
                FileUtils.Log("FgdTest-Window_Loaded:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private UIElement GenerateChannelADForm(string channel)
        {
            StackPanel stackPanel = new StackPanel();
            try
            {
                stackPanel.Orientation = Orientation.Horizontal;

                Label label0 = new Label
                {
                    Content = "通道" + channel,
                    FontSize = 20,
                    Width = 80
                };

                Label label1 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label2 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label3 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label4 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label5 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label6 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label7 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                Label label8 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 80
                };

                _boolL.Add(false);

                stackPanel.Children.Add(label0);
                stackPanel.Children.Add(label1);
                stackPanel.Children.Add(label2);
                stackPanel.Children.Add(label3);
                stackPanel.Children.Add(label4);
                stackPanel.Children.Add(label5);
                stackPanel.Children.Add(label6);
                stackPanel.Children.Add(label7);
                stackPanel.Children.Add(label8);

                _list0.Add(label0);
                _list1.Add(label1);
                _list2.Add(label2);
                _list3.Add(label3);
                _list4.Add(label4);
                _list5.Add(label5);
                _list6.Add(label6);
                _list7.Add(label7);
                _list8.Add(label8);
            }
            catch (Exception ex)
            {
                FileUtils.Log("FgdTest-GenerateChannelADForm:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            }

            return stackPanel;
        }

        // 一直读取AD值
        class UpdateADThread : ChildThread
        {
            FgdTest wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public UpdateADThread(FgdTest wnd)
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

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_READ_AD_CYCLE:
                        if (msg.result)
                        {
                            byte[] data = msg.data;
                            wnd.Update(data);
                        }
                        else
                        {
                            Console.WriteLine("读取AD值错误");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        // 根据给定的data，更新界面
        private void Update(byte[] data)
        {
            try
            {
                HandleAd(data);
                //更新满值AD
                if (0 == _adClear)
                {
                    _HolesFullAD = _HolesCurAD;
                    --_adClear;
                    ButtonSampleClear.Content = "清零";
                    ButtonSampleClear.IsEnabled = true;
                }
                else if (_adClear > 0)
                {
                    --_adClear;
                    ButtonSampleClear.Content = "" + _adClear;
                }
                int num = 0;
                // 更新吸光度和透光率 dbValues
                if (Global.deviceHole.HoleCount * 4 <= adList.Count && Global.deviceHole.HmCount * 4 <= tgList.Count && Global.IsTest)
                {
                    //单通道判定标准
                    double decisionCriteria = Global.DecisionCriteria1;
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        num += 1;
                        _list1[i].Content = FgdCaculate.A_To_String(adList[i * 4]);
                        if (_boolL[i])
                        {
                            _list0[i].Foreground = new SolidColorBrush(Colors.Goldenrod);
                        }
                        _list2[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 1]);
                        _list3[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 2]);
                        _list4[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 3]);
                        if (!_list5[i].Content.ToString().Equals("100.00%") && _list6[i].Content.ToString() != "100.00%" && _list7[i].Content.ToString() != "100.00%" && _list8[i].Content.ToString() != "100.00%")
                        {
                            _numbool = i;
                        }
                        if (tgList[i * 4] != 1)
                            _num1 = tgList[i * 4];
                        if (tgList[i * 4 + 1] != 1)
                            _num2 = tgList[i * 4 + 1];
                        if (tgList[i * 4 + 2] != 1)
                            _num3 = tgList[i * 4 + 2];
                        if (tgList[i * 4 + 3] != 1)
                            _num4 = tgList[i * 4 + 3];
                        if (num <= 4)//1-4灯 0.2994
                        {
                            double wc1 = 0, wc2 = 0, wc3 = 0, wc4 = 0;
                            wc1 = Global.Standard1 - tgList[i * 4];
                            if (tgList[i * 4] != 1 && (wc1 < 0 ? wc1 = wc1 * -1 : wc1) > decisionCriteria)
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc2 = Global.Standard2 - tgList[i * 4 + 1];
                            if (tgList[i * 4 + 1] != 1 && (wc2 < 0 ? wc2 = wc2 * -1 : wc2) > decisionCriteria)
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc3 = Global.Standard3 - tgList[i * 4 + 2];
                            if (tgList[i * 4 + 2] != 1 && (wc3 < 0 ? wc3 = wc3 * -1 : wc3) > decisionCriteria)
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc4 = Global.Standard4 - tgList[i * 4 + 3];
                            if (tgList[i * 4 + 3] != 1 && (wc4 < 0 ? wc4 = wc4 * -1 : wc4) > decisionCriteria)
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        if (num >= 5 && num <= 8)//5-8灯 0.3008
                        {
                            double wc1 = 0, wc2 = 0, wc3 = 0, wc4 = 0;
                            wc1 = Global.Standard1 - tgList[i * 4];
                            if (tgList[i * 4] != 1 && (wc1 < 0 ? wc1 = wc1 * -1 : wc1) > decisionCriteria)
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc2 = Global.Standard2 - tgList[i * 4 + 1];
                            if (tgList[i * 4 + 1] != 1 && (wc2 < 0 ? wc2 = wc2 * -1 : wc2) > decisionCriteria)
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc3 = Global.Standard3 - tgList[i * 4 + 2];
                            if (tgList[i * 4 + 2] != 1 && (wc3 < 0 ? wc3 = wc3 * -1 : wc3) > decisionCriteria)
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc4 = Global.Standard4 - tgList[i * 4 + 3];
                            if (tgList[i * 4 + 3] != 1 && (wc4 < 0 ? wc4 = wc4 * -1 : wc4) > decisionCriteria)
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        if (num >= 9 && num <= 12)//9-12灯 0.2609
                        {
                            double wc1 = 0, wc2 = 0, wc3 = 0, wc4 = 0;
                            wc1 = Global.Standard1 - tgList[i * 4];
                            if (tgList[i * 4] != 1 && (wc1 < 0 ? wc1 = wc1 * -1 : wc1) > decisionCriteria)
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc2 = Global.Standard2 - tgList[i * 4 + 1];
                            if (tgList[i * 4 + 1] != 1 && (wc2 < 0 ? wc2 = wc2 * -1 : wc2) > decisionCriteria)
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc3 = Global.Standard3 - tgList[i * 4 + 2];
                            if (tgList[i * 4 + 2] != 1 && (wc3 < 0 ? wc3 = wc3 * -1 : wc3) > decisionCriteria)
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc4 = Global.Standard4 - tgList[i * 4 + 3];
                            if (tgList[i * 4 + 3] != 1 && (wc4 < 0 ? wc4 = wc4 * -1 : wc4) > decisionCriteria)
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        if (num >= 13 && num <= 16)//13-16灯 0.2534
                        {
                            double wc1 = 0, wc2 = 0, wc3 = 0, wc4 = 0;
                            wc1 = Global.Standard1 - tgList[i * 4];
                            if (tgList[i * 4] != 1 && (wc1 < 0 ? wc1 = wc1 * -1 : wc1) > decisionCriteria)
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                                _list5[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc2 = Global.Standard2 - tgList[i * 4 + 1];
                            if (tgList[i * 4 + 1] != 1 && (wc2 < 0 ? wc2 = wc2 * -1 : wc2) > decisionCriteria)
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                                _list6[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc3 = Global.Standard3 - tgList[i * 4 + 2];
                            if (tgList[i * 4 + 2] != 1 && (wc3 < 0 ? wc3 = wc3 * -1 : wc3) > decisionCriteria)
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                                _list7[i].Foreground = new SolidColorBrush(Colors.Black);
                            }

                            wc4 = Global.Standard4 - tgList[i * 4 + 3];
                            if (tgList[i * 4 + 3] != 1 && (wc4 < 0 ? wc4 = wc4 * -1 : wc4) > decisionCriteria)
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                                _list8[i].Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                    }
                }
                else if (Global.deviceHole.HoleCount * 4 <= adList.Count && Global.deviceHole.HmCount * 4 <= tgList.Count)
                {
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        _list1[i].Content = FgdCaculate.A_To_String(adList[i * 4]);
                        _list2[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 1]);
                        _list3[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 2]);
                        _list4[i].Content = FgdCaculate.A_To_String(adList[i * 4 + 3]);
                        _list5[i].Content = FgdCaculate.T_To_String(tgList[i * 4]);
                        _list6[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 1]);
                        _list7[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 2]);
                        _list8[i].Content = FgdCaculate.T_To_String(tgList[i * 4 + 3]);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("FgdTest-Update:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 读取data，处理AD，根据item，把t值和a值
        private void HandleAd(byte[] data)
        {
            _HolesCurAD = FgdCaculate.NewRawDataToAD(data);
            if (null == _HolesFullAD)
                _HolesFullAD = _HolesCurAD;
            FgdCaculate.TLimit tLimit = new FgdCaculate.TLimit(1.2, 0.95, 1.05);
            FgdCaculate.ALimit aLimit = new FgdCaculate.ALimit(5);
            FgdCaculate.HolesT t = FgdCaculate.CaculateT(_HolesCurAD, _HolesFullAD, tLimit);
            tgList = FgdCaculate.CaculateT(_HolesCurAD, _HolesFullAD, tLimit, "");
            //FgdCaculate.HolesA a = FgdCaculate.CaculateA(HolesCurAD, HolesFullAD, t, aLimit);
            adList = FgdCaculate.CaculateA(_HolesCurAD, _HolesFullAD, t, aLimit, "");
        }

        class HoleADValues
        {
            public int LED_ROW = 0;
            public int LED_COL = 0;
            public int LED_NUMS = 0;
            public long[][][] orginADValues; // 原始AD值
            public long[][][] adValues; // 减去暗电流的AD值
            public long[] darkAdValues; // 暗电流
            public HoleADValues(int row, int col, int nums)
            {
                LED_ROW = row;
                LED_COL = col;
                LED_NUMS = nums;
                adValues = new long[LED_ROW][][];
                orginADValues = new long[LED_ROW][][];
                for (int i = 0; i < LED_ROW; ++i)
                {
                    adValues[i] = new long[LED_COL][];
                    orginADValues[i] = new long[LED_COL][];
                    for (int j = 0; j < LED_COL; ++j)
                    {
                        adValues[i][j] = new long[LED_NUMS];
                        orginADValues[i][j] = new long[LED_NUMS];
                    }
                }
                darkAdValues = new long[LED_ROW];
            }
        }

        HoleADValues RawDataToAD(byte[] data)
        {
            int idx = 1;
            int LED_ROW = data[idx];
            int LED_COL = 8;
            int LED_NUMS = 4;
            HoleADValues ad = new HoleADValues(LED_ROW, LED_COL, LED_NUMS);
            idx = 2;
            for (int i = 0; i < LED_ROW; ++i)
            {
                ++idx;// 1个byte的长度
                // 灯全灭，暗电流
                ad.darkAdValues[i] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
                idx += 4;

                for (int j = 0; j < LED_COL; ++j)
                {
                    for (int k = 0; k < LED_NUMS; ++k)
                    {
                        ad.orginADValues[i][j][k] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
                        ad.adValues[i][j][k] = ad.orginADValues[i][j][k];
                        idx += 4;

                        if (ad.adValues[i][j][k] < ad.darkAdValues[i])
                            ad.adValues[i][j][k] = 0;
                        else
                            ad.adValues[i][j][k] -= ad.darkAdValues[i];
                    }
                }
                ++idx;// 1个byte的校验
            }
            return ad;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.IsClear = false;
            this.Close();
        }

        private void ButtonSampleClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Read.Focus();
                _HolesFullAD = null;
                _adClear = 10;
                ButtonSampleClear.Content = "" + _adClear;
                ButtonSampleClear.IsEnabled = false;
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    _list1[i].Content = "";
                    _list2[i].Content = "";
                    _list3[i].Content = "";
                    _list4[i].Content = "";
                    _list5[i].Content = "";
                    _list6[i].Content = "";
                    _list7[i].Content = "";
                    _list8[i].Content = "";
                }

                _Max1 = 0; _Max2 = 0; _Max3 = 0; _Max4 = 0;
                _Min1 = 0; _Min2 = 0; _Min3 = 0; _Min4 = 0;
                label1.Content = ""; label2.Content = ""; label3.Content = ""; label4.Content = "";
                for (int i = 0; i < _boolL.Count; i++)
                {
                    _boolL[i] = false;
                    _list0[i].Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("FgdTest-ButtonSampleClear_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 读取并记录此刻的AD值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Click(object sender, RoutedEventArgs e)
        {
            isRead = true;
            read();
        }

        private void read()
        {
            //记录前记录上一次测试的结果
            if (isRead)
            {
                Max1 = _Max1; Max2 = _Max2; Max3 = _Max3; Max4 = _Max4;
                Min1 = _Min1; Min2 = _Min2; Min3 = _Min3; Min4 = _Min4;
            }
            else
            {
                _Max1 = Max1; _Max2 = Max2; _Max3 = Max3; _Max4 = Max4;
                _Min1 = Min1; _Min2 = Min2; _Min3 = Min3; _Min4 = Min4;
            }
            //记录最大值
            if (_num1 > _Max1 && isRead)
            {
                _Max1 = _num1;
                _tglMax1 = _numbool + 1;
            }
            if (_num2 > _Max2 && isRead)
            {
                _Max2 = _num2;
                _tglMax2 = _numbool + 1;
            }
            if (_num3 > _Max3 && isRead)
            {
                _Max3 = _num3;
                _tglMax3 = _numbool + 1;
            }
            if (_num4 > _Max4 && isRead)
            {
                _Max4 = _num4;
                _tglMax4 = _numbool + 1;
            }

            //给最小值赋值，最小值不能为0
            if (_Min1 == 0)
            {
                _Min1 = _num1;
                _tglMin1 = _numbool + 1;
            }
            if (_Min2 == 0)
            {
                _Min2 = _num2;
                _tglMin2 = _numbool + 1;
            }
            if (_Min3 == 0)
            {
                _Min3 = _num3;
                _tglMin3 = _numbool + 1;
            }
            if (_Min4 == 0)
            {
                _Min4 = _num4;
                _tglMin4 = _numbool + 1;
            }

            //记录最小值
            if (_num1 < _Min1 && isRead)
            {
                _Min1 = _num1;
                _tglMin1 = _numbool + 1;
            }
            if (_num2 < _Min2 && isRead)
            {
                _Min2 = _num2;
                _tglMin2 = _numbool + 1;
            }
            if (_num3 < _Min3 && isRead)
            {
                _Min3 = _num3;
                _tglMin3 = _numbool + 1;
            }
            if (_num4 < _Min4 && isRead)
            {
                _Min4 = _num4;
                _tglMin4 = _numbool + 1;
            }
            switch (_numbool)
            {
                case 0:
                    _boolL[_numbool] = true;
                    break;
                case 1:
                    _boolL[_numbool] = true;
                    break;
                case 2:
                    _boolL[_numbool] = true;
                    break;
                case 3:
                    _boolL[_numbool] = true;
                    break;
                case 4:
                    _boolL[_numbool] = true;
                    break;
                case 5:
                    _boolL[_numbool] = true;
                    break;
                case 6:
                    _boolL[_numbool] = true;
                    break;
                case 7:
                    _boolL[_numbool] = true;
                    break;
                case 8:
                    _boolL[_numbool] = true;
                    break;
                case 9:
                    _boolL[_numbool] = true;
                    break;
                case 10:
                    _boolL[_numbool] = true;
                    break;
                case 11:
                    _boolL[_numbool] = true;
                    break;
                case 12:
                    _boolL[_numbool] = true;
                    break;
                case 13:
                    _boolL[_numbool] = true;
                    break;
                case 14:
                    _boolL[_numbool] = true;
                    break;
                case 15:
                    _boolL[_numbool] = true;
                    break;
            }
            ConclusionClick();
        }

        private void ConclusionClick()
        {
            double decisionCriteria = Global.DecisionCriteria2;
            if (_Max1 != 0 && _Max2 != 0 && _Max3 != 0 && _Max4 != 0 && _Min1 != 0 && _Min2 != 0 && _Min3 != 0 && _Min4 != 0)
            {
                string str1 = "", str2 = "", str3 = "", str4 = "";
                if ((_Max1 - _Min1) < decisionCriteria)
                    str1 = "合格";
                else
                    str1 = "不合格";
                if ((_Max2 - _Min2) < decisionCriteria)
                    str2 = "合格";
                else
                    str2 = "不合格";
                if ((_Max3 - _Min3) < decisionCriteria)
                    str3 = "合格";
                else
                    str3 = "不合格";
                if ((_Max4 - _Min4) < decisionCriteria)
                    str4 = "合格";
                else
                    str4 = "不合格";

                if (str1.Equals("合格"))
                {
                    label1.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max1) + "(" + _tglMax1
                        + "),Min:" + FgdCaculate.T_To_String(_Min1) + "(" + _tglMin1 + "),通道间差:" + FgdCaculate.T_To_String(_Max1 - _Min1) + ",单项结论:" + str1;
                    label1.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    label1.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max1) + "(" + _tglMax1
                        + "),Min:" + FgdCaculate.T_To_String(_Min1) + "(" + _tglMin1 + "),通道间差:" + FgdCaculate.T_To_String(_Max1 - _Min1) + ",单项结论:" + str1;
                    label1.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (str2.Equals("合格"))
                {
                    label2.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max2) + "(" + _tglMax2
                        + "),Min:" + FgdCaculate.T_To_String(_Min2) + "(" + _tglMin2 + "),通道间差:" + FgdCaculate.T_To_String(_Max2 - _Min2) + ",单项结论:" + str2;
                    label2.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    label2.Foreground = new SolidColorBrush(Colors.Red);
                    label2.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max2) + "(" + _tglMax2
                        + "),Min:" + FgdCaculate.T_To_String(_Min2) + "(" + _tglMin2 + "),通道间差:" + FgdCaculate.T_To_String(_Max2 - _Min2) + ",单项结论:" + str2;
                }

                if (str3.Equals("合格"))
                {
                    label3.Foreground = new SolidColorBrush(Colors.Black);
                    label3.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard3) + ",Max:" + FgdCaculate.T_To_String(_Max3) + "(" + _tglMax3
                        + "),Min:" + FgdCaculate.T_To_String(_Min3) + "(" + _tglMin3 + "),通道间差:" + FgdCaculate.T_To_String(_Max3 - _Min3) + ",单项结论:" + str3;
                }
                else
                {
                    label3.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard3) + ",Max:" + FgdCaculate.T_To_String(_Max3) + "(" + _tglMax3
                        + "),Min:" + FgdCaculate.T_To_String(_Min3) + "(" + _tglMin3 + "),通道间差:" + FgdCaculate.T_To_String(_Max3 - _Min3) + ",单项结论:" + str3;
                    label3.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (str4.Equals("合格"))
                {
                    label4.Foreground = new SolidColorBrush(Colors.Black);
                    label4.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard4) + ",Max:" + FgdCaculate.T_To_String(_Max4) + "(" + _tglMax4
                        + "),Min:" + FgdCaculate.T_To_String(_Min4) + "(" + _tglMin4 + "),通道间差:" + FgdCaculate.T_To_String(_Max4 - _Min4) + ",单项结论:" + str4;
                }
                else
                {
                    label4.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard4) + ",Max:" + FgdCaculate.T_To_String(_Max4) + "(" + _tglMax4
                        + "),Min:" + FgdCaculate.T_To_String(_Min4) + "(" + _tglMin4 + "),通道间差:" + FgdCaculate.T_To_String(_Max4 - _Min4) + ",单项结论:" + str4;
                    label4.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            Read.Focus();
        }

        private void Conclusion_Click(object sender, RoutedEventArgs e)
        {
            //通道间差判定标准
            try
            {
                double decisionCriteria = Global.DecisionCriteria2;
                if (_Max1 != 0 && _Max2 != 0 && _Max3 != 0 && _Max4 != 0 && _Min1 != 0 && _Min2 != 0 && _Min3 != 0 && _Min4 != 0)
                {
                    string str1 = "", str2 = "", str3 = "", str4 = "";
                    if ((_Max1 - _Min1) < decisionCriteria)
                        str1 = "合格";
                    else
                        str1 = "不合格";
                    if ((_Max2 - _Min2) < decisionCriteria)
                        str2 = "合格";
                    else
                        str2 = "不合格";
                    if ((_Max3 - _Min3) < decisionCriteria)
                        str3 = "合格";
                    else
                        str3 = "不合格";
                    if ((_Max4 - _Min4) < decisionCriteria)
                        str4 = "合格";
                    else
                        str4 = "不合格";

                    if (str1.Equals("合格"))
                    {
                        label1.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max1) + "(" + _tglMax1
                            + "),Min:" + FgdCaculate.T_To_String(_Min1) + "(" + _tglMin1 + "),通道间差:" + FgdCaculate.T_To_String(_Max1 - _Min1) + ",单项结论:" + str1;
                        label1.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        label1.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max1) + "(" + _tglMax1
                            + "),Min:" + FgdCaculate.T_To_String(_Min1) + "(" + _tglMin1 + "),通道间差:" + FgdCaculate.T_To_String(_Max1 - _Min1) + ",单项结论:" + str1;
                        label1.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (str2.Equals("合格"))
                    {
                        label2.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max2) + "(" + _tglMax2
                            + "),Min:" + FgdCaculate.T_To_String(_Min2) + "(" + _tglMin2 + "),通道间差:" + FgdCaculate.T_To_String(_Max2 - _Min2) + ",单项结论:" + str2;
                        label2.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        label2.Foreground = new SolidColorBrush(Colors.Red);
                        label2.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard1) + ",Max:" + FgdCaculate.T_To_String(_Max2) + "(" + _tglMax2
                            + "),Min:" + FgdCaculate.T_To_String(_Min2) + "(" + _tglMin2 + "),通道间差:" + FgdCaculate.T_To_String(_Max2 - _Min2) + ",单项结论:" + str2;
                    }

                    if (str3.Equals("合格"))
                    {
                        label3.Foreground = new SolidColorBrush(Colors.Black);
                        label3.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard3) + ",Max:" + FgdCaculate.T_To_String(_Max3) + "(" + _tglMax3
                            + "),Min:" + FgdCaculate.T_To_String(_Min3) + "(" + _tglMin3 + "),通道间差:" + FgdCaculate.T_To_String(_Max3 - _Min3) + ",单项结论:" + str3;
                    }
                    else
                    {
                        label3.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard3) + ",Max:" + FgdCaculate.T_To_String(_Max3) + "(" + _tglMax3
                            + "),Min:" + FgdCaculate.T_To_String(_Min3) + "(" + _tglMin3 + "),通道间差:" + FgdCaculate.T_To_String(_Max3 - _Min3) + ",单项结论:" + str3;
                        label3.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (str4.Equals("合格"))
                    {
                        label4.Foreground = new SolidColorBrush(Colors.Black);
                        label4.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard4) + ",Max:" + FgdCaculate.T_To_String(_Max4) + "(" + _tglMax4
                            + "),Min:" + FgdCaculate.T_To_String(_Min4) + "(" + _tglMin4 + "),通道间差:" + FgdCaculate.T_To_String(_Max4 - _Min4) + ",单项结论:" + str4;
                    }
                    else
                    {
                        label4.Content = "标准值:" + FgdCaculate.T_To_String(Global.Standard4) + ",Max:" + FgdCaculate.T_To_String(_Max4) + "(" + _tglMax4
                            + "),Min:" + FgdCaculate.T_To_String(_Min4) + "(" + _tglMin4 + "),通道间差:" + FgdCaculate.T_To_String(_Max4 - _Min4) + ",单项结论:" + str4;
                        label4.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                Read.Focus();
            }
            catch (Exception ex)
            {
                FileUtils.Log("FgdTest-Conclusion_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void undo_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要回退到上一次测试结果吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                isRead = false;
                read();
            }
        }

    }
}