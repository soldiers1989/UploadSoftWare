using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        #region 全局
        Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        CompositeDataSource compositeDataSource1;
        CompositeDataSource compositeDataSource2;
        EnumerableDataSource<DateTime> datesDataSource = null;
        EnumerableDataSource<int> numberOpenDataSource = null;
        EnumerableDataSource<int> numberClosedDataSource = null;
        List<DateTime> vardatetime = new List<DateTime>();
        int i = 0;
        List<int> numberOpen = new List<int>();
        List<int> numberClosed = new List<int>();
        #endregion

        private void Window1_Loaded(object sender, EventArgs e)
        {
            DateTime tempDateTime = new DateTime();
            tempDateTime = DateTime.Now;
            vardatetime.Add(tempDateTime);

            numberOpen.Add(random.Next(40));
            numberClosed.Add(random.Next(100));

            datesDataSource.RaiseDataChanged();
            numberOpenDataSource.RaiseDataChanged();
            numberClosedDataSource.RaiseDataChanged();
            i++;
        } 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime tempDateTime = new DateTime();
            tempDateTime = DateTime.Now;
            vardatetime.Add(tempDateTime);
            numberOpen.Add(random.Next(40));
            numberClosed.Add(random.Next(100));
            i++;

            datesDataSource = new EnumerableDataSource<DateTime>(vardatetime);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

            numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);

            numberClosedDataSource = new EnumerableDataSource<int>(numberClosed);
            numberClosedDataSource.SetYMapping(y => y);

            compositeDataSource1 = new CompositeDataSource(datesDataSource, numberOpenDataSource);
            compositeDataSource2 = new CompositeDataSource(datesDataSource, numberClosedDataSource);

            plotter.AddLineGraph(compositeDataSource2, Colors.Green, 1, "Percentage2");
            plotter.Viewport.FitToView();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Window1_Loaded);
            timer.IsEnabled = true;
        }
    }
}
