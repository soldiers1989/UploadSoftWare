using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.lvrenyang;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// CalculatedCurve.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatedCurve : Window
    {
        public CalculatedCurve()
        {
            InitializeComponent();
            _updateADThread = new UpdateADThread(this);
            _updateADThread.Start();
            Message msg = new Message();
            msg.what = MsgCode.MSG_READ_AD_CYCLE;
            msg.str1 = Global.strADPORT;
            Global.workThread.BeginStartReadADCycle(msg, _updateADThread);
        }
        private UpdateADThread _updateADThread;

        private string logType = "CalculatedCurve";

        public int WaveIndex = 0;

        private FgdCaculate.HolesAD _HolesFullAD, _HolesCurAD;
        /// <summary>
        /// _firstATs初始值 _lastATs最后取值 _curATs当前检测值
        /// </summary>
        private FgdCaculate.AT[] _curATs;

        /// <summary>
        /// 更新吸光度的值
        /// </summary>
        /// <param name="data"></param>
        private void Update(byte[] data)
        {
            HandleAd(data);
            // 更新吸光度
            xgd1.Content = _curATs[0].a.ToString("F3");
            xgd2.Content = _curATs[1].a.ToString("F3");
            xgd3.Content = _curATs[2].a.ToString("F3");
            xgd4.Content = _curATs[3].a.ToString("F3");
            xgd5.Content = _curATs[4].a.ToString("F3");
            xgd6.Content = _curATs[5].a.ToString("F3");
        }

        private void HandleAd(byte[] data)
        {
            try
            {
                _HolesCurAD = FgdCaculate.NewRawDataToAD(data);
                if (null == _HolesFullAD)
                    _HolesFullAD = _HolesCurAD;
                FgdCaculate.TLimit tLimit = new FgdCaculate.TLimit(1.2, 0.99, 1.01);
                FgdCaculate.ALimit aLimit = new FgdCaculate.ALimit(5);
                FgdCaculate.HolesT t = FgdCaculate.CaculateT(_HolesCurAD, _HolesFullAD, tLimit);
                FgdCaculate.HolesA a = FgdCaculate.CaculateA(_HolesCurAD, _HolesFullAD, t, aLimit);
                FgdCaculate.AT[] holeATs = new FgdCaculate.AT[Global.deviceHole.HoleCount];
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    int row = i / 8;
                    int col = i % 8;
                    holeATs[i].t = t.tValues[row][col][WaveIndex]; // 波长对应的孔位索引
                    holeATs[i].a = a.aValues[row][col][WaveIndex]; // 波长对应的孔位索引
                }
                _curATs = holeATs;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        class UpdateADThread : ChildThread
        {
            CalculatedCurve wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public UpdateADThread(CalculatedCurve wnd)
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
                    FileUtils.OprLog(6, wnd.logType, ex.ToString());
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
                        break;
                    default:
                        break;
                }
            }
        }

        private void nd1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void nd2_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void nd3_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void nd4_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void nd5_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void nd6_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 生成曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ClacCurve_Click(object sender, RoutedEventArgs e)
        {
            Global.CurveValue = string.Empty;
            if (nd1.Text.Trim().Length == 0 || nd2.Text.Trim().Length == 0 || nd3.Text.Trim().Length == 0 ||
                nd4.Text.Trim().Length == 0 || nd5.Text.Trim().Length == 0 || nd6.Text.Trim().Length == 0)
            {
                MessageBox.Show("条件不足无法生成曲线！", "系统提示");
                return;
            }

            double[] x = new double[6];
            x[0] = double.Parse(xgd1.Content.ToString());
            x[1] = double.Parse(xgd2.Content.ToString());
            x[2] = double.Parse(xgd3.Content.ToString());
            x[3] = double.Parse(xgd4.Content.ToString());
            x[4] = double.Parse(xgd5.Content.ToString());
            x[5] = double.Parse(xgd6.Content.ToString());

            double[] y = new double[6];
            y[0] = double.Parse(nd1.Text.Trim());
            y[1] = double.Parse(nd2.Text.Trim());
            y[2] = double.Parse(nd3.Text.Trim());
            y[3] = double.Parse(nd4.Text.Trim());
            y[4] = double.Parse(nd5.Text.Trim());
            y[5] = double.Parse(nd6.Text.Trim());

            //通道2和通道4的斜率
            double slope = (y[0] - y[1]) / (x[0] - x[1]);
            Global.CurveValue = slope + "," + Math.Log10(Math.Abs(slope));
            //y=ax+b;
            ObservableDataSource<Point> dataSource_XGD = new ObservableDataSource<Point>();
            for (int i = 0; i < x.Length; i++)
            {
                Point point = new Point(x[i], y[i]);
                dataSource_XGD.AppendAsync(base.Dispatcher, point);
            }

            plotter_xgd.AddLineGraph(dataSource_XGD, new Pen(Brushes.Blue, 1),
               new CirclePointMarker { Size = 2, Fill = Brushes.Red }, null);
            plotter_xgd.Viewport.FitToView();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}