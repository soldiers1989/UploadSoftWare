using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace AIO.xaml.HeavyMetal
{
    /// <summary>
    /// HmReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmReportWindow : Window
    {

        public HmReportWindow()
        {
            InitializeComponent();
        }

        public DYHMItemPara _item = null;
        public List<Double[]> _dataList = new List<Double[]>();
        /// <summary>
        /// 电位
        /// </summary>
        private static Double[] _vValues;
        /// <summary>
        /// 电流
        /// </summary>
        private static Double[] _aValues;
        private List<String> _listDetectResult = null;
        private List<String> _listStrRecord = null;
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private String[,] _CheckValue;
        private Int32 _HoleNumber = 1;
        private ChartPlotter plotter = null;
        private HorizontalDateTimeAxis dateAxis = null;
        //private HorizontalIntegerAxis dateAxis = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;

            _listDetectResult = new List<string>();
            _listStrRecord = new List<string>();
            int sampleNum = _item.SampleNum;
            // 添加布局
            for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                    WrapPanelChannel.Children.Add(element);
                    WrapPanelChannel.Width += 355;
                    sampleNum++;
                    _CheckValue = new string[_HoleNumber, 15];
                    _HoleNumber++;
                }
            }
            ShowResult();
        }

        private void ShowResult()
        {
            Formatting();
            Curve();
        }

        /// <summary>
        /// 格式化数据
        /// </summary>
        private void Formatting()
        {
            try
            {
                int length = 500;
                if (_dataList.Count > 0)
                {
                    _vValues = new double[length];
                    _aValues = new double[length];
                    for (int i = 0; i < length; i++)
                    {
                        _vValues[i] = _dataList[0][i];
                        _aValues[i] = _dataList[1][i];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private void Curve()
        {
            try
            {
                int len = _aValues.Length / 2, index = 0;
                DateTime[] dates = new DateTime[len];
                Double[] numberOpen = new Double[len];
                Double[] intDate = new Double[len];
                for (int i = 0; i < _aValues.Length; ++i)
                {
                    if (_vValues[i] > 64436 && _aValues[i] > 0)
                    {
                        if (i > 0 && _vValues[i] == _vValues[i - 1])
                            continue;

                        //DateTime dt = Convert.ToDateTime("01/01/000" + (_vValues[i] - 64436));
                        DateTime dt = Convert.ToDateTime("01/01/000" + (i + 1));
                        dates[index] = dt;

                        //intDate[index] = (int)_vValues[i];
                        numberOpen[index] = (Double)_aValues[i];
                        index++;
                    }
                }

                var datesDataSource = new EnumerableDataSource<DateTime>(dates);
                datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

                //var datesDataSource = new EnumerableDataSource<Double>(intDate);
                //datesDataSource.SetXMapping(x => dateAxis.ConvertFromDouble(x));
                var numberOpenDataSource = new EnumerableDataSource<Double>(numberOpen);
                numberOpenDataSource.SetYMapping(y => y);

                CompositeDataSource compositeDataSource1 = new CompositeDataSource(datesDataSource, numberOpenDataSource);

                plotter.AddLineGraph(compositeDataSource1,
                new Pen(Brushes.Red, 2),
                new CirclePointMarker { Size = 2.0, Fill = Brushes.Blue },
                new PenDescription("Number bugs open"));
                plotter.Viewport.FitToView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private UIElement GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border();
            border.Width = 600;
            border.Height = 440;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = _borderBrushNormal;
            border.CornerRadius = new CornerRadius(10);
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 600;
            stackPanel.Height = 420;
            stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 600;
            grid.Height = 40;
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            Label label = new Label();
            label.FontSize = 20;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.Content = " 检测通道" + (channel + 1);

            Canvas canvas = new Canvas();
            canvas.Width = 600;
            canvas.Height = 400;
            canvas.Background = Brushes.Gray;
            canvas.Name = "canvas";

            plotter = new ChartPlotter();
            plotter.Width = 600;
            plotter.Height = 380;
            plotter.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            plotter.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            plotter.MouseDoubleClick += new MouseButtonEventHandler(plotter_MouseDoubleClick);
            plotter.Name = "chartPlotter";

            HorizontalAxis horizontalAxis = new HorizontalAxis();
            horizontalAxis.Name = "horizontalAxis";

            dateAxis = new HorizontalDateTimeAxis();
            //dateAxis = new HorizontalIntegerAxis();
            dateAxis.Name = "dateAxis";

            VerticalAxis verticalAxis = new VerticalAxis();
            verticalAxis.Name = "verticalAxis";

            VerticalIntegerAxis countAxis = new VerticalIntegerAxis();
            countAxis.Name = "countAxis";

            VerticalAxisTitle arialy = new VerticalAxisTitle();
            arialy.Content = "y";

            HorizontalAxisTitle arialx = new HorizontalAxisTitle();
            arialx.Content = "x";

            canvas.Children.Add(plotter);
            canvas.Children.Add(dateAxis);
            canvas.Children.Add(verticalAxis);
            canvas.Children.Add(countAxis);
            canvas.Children.Add(arialy);
            canvas.Children.Add(arialx);

            WrapPanel wrapPannelSampleNum = new WrapPanel();
            wrapPannelSampleNum.Width = 180;
            wrapPannelSampleNum.Height = 30;

            Label labelSampleNum = new Label();
            labelSampleNum.Width = 85;
            labelSampleNum.Height = 26;
            labelSampleNum.Margin = new Thickness(0, 2, 0, 0);
            labelSampleNum.FontSize = 15;
            labelSampleNum.Content = " 样品编号:";
            labelSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleNum = new TextBox();
            textBoxSampleNum.Width = 90;
            textBoxSampleNum.Height = 26;
            textBoxSampleNum.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleNum.FontSize = 15;
            textBoxSampleNum.Text = "" + sampleNum;
            textBoxSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleNum.IsReadOnly = true;

            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 180;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 85;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = " 样品名称:";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 90;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = "" + _item.Hole[channel].SampleName;
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.IsReadOnly = true;

            WrapPanel wrapPannelRGBValue = new WrapPanel();
            wrapPannelRGBValue.Width = 180;
            wrapPannelRGBValue.Height = 30;

            Label labelRGBValue = new Label();
            labelRGBValue.Width = 85;
            labelRGBValue.Height = 26;
            labelRGBValue.Margin = new Thickness(0, 2, 0, 0);
            labelRGBValue.FontSize = 15;
            labelRGBValue.Content = " 灰度值:";
            labelRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxRGBValue = new TextBox();
            textBoxRGBValue.Width = 90;
            textBoxRGBValue.Height = 26;
            textBoxRGBValue.Margin = new Thickness(0, 2, 0, 2);
            textBoxRGBValue.FontSize = 15;
            textBoxRGBValue.Text = "";
            textBoxRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxRGBValue.IsReadOnly = true;
            textBoxRGBValue.Name = "textBoxRGBValue";

            WrapPanel wrapPannelDetectResult = new WrapPanel();
            wrapPannelDetectResult.Width = 180;
            wrapPannelDetectResult.Height = 30;

            Label labelDetectResult = new Label();
            labelDetectResult.Width = 85;
            labelDetectResult.Height = 26;
            labelDetectResult.Margin = new Thickness(0, 2, 0, 0);
            labelDetectResult.FontSize = 15;
            labelDetectResult.Content = " 检测结果:";
            labelDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxDetectResult = new TextBox();
            textBoxDetectResult.Width = 90;
            textBoxDetectResult.Height = 26;
            textBoxDetectResult.Margin = new Thickness(0, 2, 0, 2);
            textBoxDetectResult.FontSize = 15;
            textBoxDetectResult.Text = "0.00";
            textBoxDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxDetectResult.IsReadOnly = true;
            textBoxDetectResult.Name = "textBoxDetectResult";

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel();
            wrapJudgemtn.Width = 180;
            wrapJudgemtn.Height = 30;

            Label labelJudgment = new Label();
            labelJudgment.Width = 85;
            labelJudgment.Height = 26;
            labelJudgment.Margin = new Thickness(0, 2, 0, 0);
            labelJudgment.FontSize = 15;
            labelJudgment.Content = " 判定结果:";
            labelJudgment.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textJugmentResult = new TextBox();
            textJugmentResult.Width = 90;
            textJugmentResult.Height = 26;
            textJugmentResult.Margin = new Thickness(0, 2, 0, 2);
            textJugmentResult.FontSize = 15;
            textJugmentResult.Text = "合格";
            textJugmentResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textJugmentResult.Name = "textJugmentResult";
            //判定标准值
            WrapPanel wrapStandValue = new WrapPanel();
            wrapStandValue.Width = 180;
            wrapStandValue.Height = 30;

            Label labelStandValue = new Label();
            labelStandValue.Width = 85;
            labelStandValue.Height = 26;
            labelStandValue.Margin = new Thickness(0, 2, 0, 0);
            labelStandValue.FontSize = 15;
            labelStandValue.Content = " 标准值:";
            labelStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textStandValue = new TextBox();
            textStandValue.Width = 90;
            textStandValue.Height = 26;
            textStandValue.Margin = new Thickness(0, 2, 0, 2);
            textStandValue.FontSize = 15;
            textStandValue.Text = "1.00";
            textStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textStandValue.Name = "textStandValue";

            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapPannelDetectResult.Children.Add(labelDetectResult);
            wrapPannelDetectResult.Children.Add(textBoxDetectResult);
            wrapJudgemtn.Children.Add(labelJudgment);
            wrapJudgemtn.Children.Add(textJugmentResult);
            wrapStandValue.Children.Add(labelStandValue);
            wrapStandValue.Children.Add(textStandValue);

            grid.Children.Add(label);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(canvas);
            //stackPanel.Children.Add(wrapPannelSampleNum);
            //stackPanel.Children.Add(wrapPannelSampleName);
            //stackPanel.Children.Add(wrapPannelDetectResult);
            //stackPanel.Children.Add(wrapJudgemtn);
            //stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;
            return border;
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
