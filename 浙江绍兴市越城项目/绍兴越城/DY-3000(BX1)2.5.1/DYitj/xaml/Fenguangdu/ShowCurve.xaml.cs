using System;
using System.ComponentModel;
using System.Windows;
using com.lvrenyang;
using Righthand.RealtimeGraph;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// ShowCurve.xaml 的交互逻辑
    /// </summary>
    public partial class ShowCurve : Window
    {
        public int x = 0;
        private BindingList<RealtimeGraphItem> xgdItems = new BindingList<RealtimeGraphItem>();
        private RealtimeGraphItem newxgdItems = null;
        private BindingList<RealtimeGraphItem> tglItems = new BindingList<RealtimeGraphItem>();
        private RealtimeGraphItem newtglItems = null;
        public class RealtimeGraphItem : IGraphItem
        {
            public int Time { get; set; }
            public double Value { get; set; }
        }

        public ShowCurve()
        {
            InitializeComponent();
            xgdGraph.SeriesSource = xgdItems;
            tglGraph.SeriesSource = tglItems;
            this.Topmost = true;
        }

        /// <summary>
        /// 更新吸光度和透光率的曲线
        /// </summary>
        /// <param name="xgd"></param>
        /// <param name="tgl"></param>
        public void UpdateCurve(double xgd, double tgl, int index)
        {
            isClose = false;
            try
            {
                //吸光度曲线
                newxgdItems = new RealtimeGraphItem
                {
                    Time = x,
                    Value = xgd
                };
                lb_xgd.Content = string.Format("{0:F3}", xgd);
                xgdItems.Add(newxgdItems);

                //透光率曲线
                newtglItems = new RealtimeGraphItem
                {
                    Time = x,
                    Value = tgl * 100
                };
                lb_tgl.Content = string.Format("{0:P2}", tgl);
                tglItems.Add(newtglItems);

                x += 20;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, "ShowCurve-error", ex.ToString());
                MessageBox.Show("绘制实时曲线时出现异常！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool isClose = false;
        public void CloseWindow()
        {
            try
            {
                this.Close();
                Global.IsSettingCurve = false;
                isClose = true;
            }
            catch (Exception) { }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();
            Global.IsSettingCurve = isClose ? false : true;
            e.Cancel = isClose ? false : true;
        }

    }
}