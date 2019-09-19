using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AIO
{
    /// <summary>
    /// FgdShowADWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdShowADWindow : Window
    {

        #region 全局变量
        private UpdateADThread _updateADThread;
        private DYFGDItemPara _item = new DYFGDItemPara();
        private List<Label> _listAD0 = null;
        private List<Label> _listAD1 = null;
        private List<Label> _listAD2 = null;
        private List<Label> _listAD3 = null;
        private List<Label> _listADDark = null;
        #endregion

        public FgdShowADWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _listAD0 = new List<Label>();
                _listAD1 = new List<Label>();
                _listAD2 = new List<Label>();
                _listAD3 = new List<Label>();
                _listADDark = new List<Label>();
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    UIElement element = GenerateChannelADForm(string.Empty + (i + 1));
                    StackPanelShowAD.Children.Add(element);
                }
                _updateADThread = new UpdateADThread(this);
                _updateADThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_READ_AD_CYCLE,
                    str1 = Global.strADPORT
                };
                Global.workThread.BeginStartReadADCycle(msg, _updateADThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Window_Loaded):\n" + ex.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.workThread.BeginStopReadADCycle();
            _updateADThread.Stop();
        }

        private UIElement GenerateChannelADForm(string channel)
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Label label1 = new Label()
            {
                Content = "通道" + channel,
                FontSize = 20,
                Width = 100
            };
            Label label2 = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100
            };
            Label label3 = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100
            };
            Label label4 = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100
            };
            Label label5 = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100
            };
            Label label6 = new Label()
            {
                Content = string.Empty,
                FontSize = 20,
                Width = 100
            };
            stackPanel.Children.Add(label1);
            stackPanel.Children.Add(label2);
            stackPanel.Children.Add(label3);
            stackPanel.Children.Add(label4);
            stackPanel.Children.Add(label5);
            stackPanel.Children.Add(label6);

            _listAD0.Add(label2);
            _listAD1.Add(label3);
            _listAD2.Add(label4);
            _listAD3.Add(label5);
            _listADDark.Add(label6);

            return stackPanel;
        }

        private void Update(byte[] data)
        {
            try
            {
                HoleADValues ad = RawDataToAD(data);
                for (int i = 0; i < _listAD0.Count; ++i)
                {
                    _listAD0[i].Content = string.Empty + (int)(ad.adValues[i / 8][i % 8][0] * 1000.0 / 0xFFFF);
                    _listAD1[i].Content = string.Empty + (int)(ad.adValues[i / 8][i % 8][1] * 1000.0 / 0xFFFF);
                    _listAD2[i].Content = string.Empty + (int)(ad.adValues[i / 8][i % 8][2] * 1000.0 / 0xFFFF);
                    _listAD3[i].Content = string.Empty + (int)(ad.adValues[i / 8][i % 8][3] * 1000.0 / 0xFFFF);
                    if (0 == (i % 8))
                        _listADDark[i].Content = string.Empty + (int)(ad.darkAdValues[i / 8] * 1000.0 / 0xFFFF);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Update):\n" + ex.Message);
            }
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
                        // adValues[offset] * 1000 / 0x10000 ,将AD转换成3.3V的千分之几。直观显示。
                    }
                }
                ++idx;// 1个byte的校验
            }
            return ad;
        }

        // 一直读取AD值
        class UpdateADThread : ChildThread
        {
            FgdShowADWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public UpdateADThread(FgdShowADWindow wnd)
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

    }
}