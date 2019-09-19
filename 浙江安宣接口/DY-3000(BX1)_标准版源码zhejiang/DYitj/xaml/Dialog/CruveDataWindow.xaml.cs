using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DYSeriesDataSet;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// CruveDataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CruveDataWindow : Window
    {
        public CruveDataWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Topmost = true;
        }

        public void SettingCruve(tlsTtResultSecond model, string cruveDatas)
        {
            this.txtFoodName.Text = model.FoodName;
            this.txtCheckTotalItem.Text = model.CheckTotalItem;
            this.txtCheckValueInfo.Text = model.CheckValueInfo;
            this.txtResult.Text = model.Result;

            //绘制曲线
            string[] datas = cruveDatas.Split('|');
            int length = datas.Length;
            DateTime[] dates = new DateTime[length];
            int[] numberOpen = new int[length];
            for (int i = 0; i < length; i++)
            {
                DateTime dt = Convert.ToDateTime("01/01/000" + (i + 1));
                dates[i] = dt;
                numberOpen[i] = int.Parse(datas[i]);
            }

            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

            var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);
            CompositeDataSource compositeDataSource = new CompositeDataSource(datesDataSource, numberOpenDataSource);

            plotter.AddLineGraph(compositeDataSource,
                                            new Pen(Brushes.Blue, 1),
                                            new CirclePointMarker { Size = 2, Fill = Brushes.Red },
                                            null);
            plotter.Viewport.FitToView();
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
            //Hide();
        }

        public void CloseWindow() 
        {
            this.Close();
        }

    }
}