using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// RealTimeCurve.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeCurve : Window
    {
        public RealTimeCurve()
        {
            InitializeComponent();
            this.Topmost = true;
            plotter_xgd.AddLineGraph(dataSource_XGD, new Pen(Brushes.Blue, 1),
                new CirclePointMarker { Size = 2, Fill = Brushes.Red }, null);
            plotter_xgd.Viewport.FitToView();
            plotter_tgl.AddLineGraph(dataSource_TGL, new Pen(Brushes.Blue, 1),
                new CirclePointMarker { Size = 2, Fill = Brushes.Red }, null);
            plotter_tgl.Viewport.FitToView();
            plotter_gdnl.AddLineGraph(dataSource_GDNL, new Pen(Brushes.Blue, 1),
                new CirclePointMarker { Size = 2, Fill = Brushes.Red }, null);
            plotter_gdnl.Viewport.FitToView();
        }
        public double[] xgdDatas = null;
        private ObservableDataSource<Point> dataSource_XGD = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> dataSource_TGL = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> dataSource_GDNL = new ObservableDataSource<Point>();
        public int _x = 0;
        private bool isClose = false;

        public void SettingValue(double _xgd, double _tgl,int _gdnl)
        {
            //吸光度
            Point point = new Point(_x, _xgd);
            dataSource_XGD.AppendAsync(base.Dispatcher, point);
            xgdValue.Content = string.Format("吸光度实时曲线：{0:F3}", _xgd);

            //透光率
            point = new Point(_x, _tgl * 100);
            dataSource_TGL.AppendAsync(base.Dispatcher, point);
            tglValue.Content = string.Format("透光率实时曲线：{0:P2}", _tgl);

            //光度能量
            point = new Point(_x, _gdnl);
            dataSource_GDNL.AppendAsync(base.Dispatcher, point);
            gdnlValue.Content = string.Format("光度能量实时曲线：{0}", _gdnl);
            _x++;
        }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.IsSettingCurve = false;
        }

    }
}