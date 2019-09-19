using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using com.lvrenyang;

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
        private string logType = "FgdShowADWindow-error";
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.workThread.StopWhileRead();
            _updateADThread.Stop();
        }

        private UIElement GenerateChannelADForm(string channel)
        {
            StackPanel stackPanel = new StackPanel();
            try
            {
                stackPanel.Orientation = Orientation.Horizontal;

                Label label1 = new Label
                {
                    Content = "通道" + channel,
                    FontSize = 20,
                    Width = 100
                };

                Label label2 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 100
                };

                Label label3 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 100
                };

                Label label4 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 100
                };

                Label label5 = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 100
                };

                Label label6 = new Label
                {
                    Content = "",
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }

            return stackPanel;
        }

        private void Update(byte[] data)
        {
            try
            {
                if (data != null)
                {
                    List<List<double>> dbList = new List<List<double>>();
                    double[] rtnData = new double[64];
                    int length = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        rtnData[length] = (data[i + 1] * 256 + data[i]) * 1000.0 / 0xFFFF;
                        i++;
                        length++;
                    }
                    for (int i = 0; i < rtnData.Length; i++)
                    {
                        List<double> db = new List<double>();
                        db.Add(rtnData[i]);
                        db.Add(rtnData[i + 1]);
                        db.Add(rtnData[i + 2]);
                        db.Add(rtnData[i + 3]);
                        i += 3;
                        dbList.Add(db);
                    }
                    for (int i = 0; i < dbList.Count; ++i)
                    {
                        _listAD0[i].Content = "" + (int)dbList[i][0];
                        _listAD1[i].Content = "" + (int)dbList[i][1];
                        _listAD2[i].Content = "" + (int)dbList[i][2];
                        _listAD3[i].Content = "" + (int)dbList[i][3];
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
                catch (Exception ex)
                {
                    FileUtils.OprLog(1, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
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