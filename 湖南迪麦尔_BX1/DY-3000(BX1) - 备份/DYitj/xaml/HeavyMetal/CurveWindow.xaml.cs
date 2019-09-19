using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.HeavyMetal
{
    /// <summary>
    /// CurveWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CurveWindow : Window
    {
        public CurveWindow()
        {
            InitializeComponent();
        }

        private ObservableDataSource<Point> _dataSource = new ObservableDataSource<Point>();
        private PerformanceCounter _performance = new PerformanceCounter();
        private DispatcherTimer _timer = new DispatcherTimer();
        public double i = 0;
        List<clsCurve> _dlList = new List<clsCurve>();

        private void AnimatedPlot(object sender, EventArgs e)
        {
            Point point = new Point(Global.xValue + i, Global.yValue);
            _dataSource.AppendAsync(base.Dispatcher, point);
            i += 0.001;
            this.dianliu.Content = "电流:" + Global.yValue + "(uA)";
            this.dianwei.Content = "电位:" + Global.xValue + "(mV)";
            clsCurve model = new clsCurve();
            model.Time = Global.timeValue.ToString() + "s";
            model.Dianliu = Global.yValue;
            model.Dianwei = Global.xValue;
            _dlList.Add(model);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.plotter.AddLineGraph(_dataSource, Colors.Red, 1, "Color");
            _timer.Interval = TimeSpan.FromSeconds(0.2);
            _timer.Tick += new EventHandler(AnimatedPlot);
            _timer.IsEnabled = true;
            this.plotter.Viewport.FitToView();
        }
    }
}
