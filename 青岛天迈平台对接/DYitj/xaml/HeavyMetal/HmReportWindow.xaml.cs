using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
                    _CheckValue = new string[_HoleNumber, 17];
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
            Border border = new Border()
            {
                Width = 600,
                Height = 440,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                BorderBrush = _borderBrushNormal,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "border"
            };
            StackPanel stackPanel = new StackPanel()
            {
                Width = 600,
                Height = 420,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 600,
                Height = 40,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };
            Label label = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = " 检测通道" + (channel + 1)
            };
            Canvas canvas = new Canvas()
            {
                Width = 600,
                Height = 400,
                Background = Brushes.Gray,
                Name = "canvas"
            };
            plotter = new ChartPlotter()
            {
                Width = 600,
                Height = 380,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch
            };
            plotter.MouseDoubleClick += new MouseButtonEventHandler(plotter_MouseDoubleClick);
            plotter.Name = "chartPlotter";

            HorizontalAxis horizontalAxis = new HorizontalAxis()
            {
                Name = "horizontalAxis"
            };
            dateAxis = new HorizontalDateTimeAxis()
            {
                //dateAxis = new HorizontalIntegerAxis();
                Name = "dateAxis"
            };
            VerticalAxis verticalAxis = new VerticalAxis()
            {
                Name = "verticalAxis"
            };
            VerticalIntegerAxis countAxis = new VerticalIntegerAxis()
            {
                Name = "countAxis"
            };
            VerticalAxisTitle arialy = new VerticalAxisTitle()
            {
                Content = "y"
            };
            HorizontalAxisTitle arialx = new HorizontalAxisTitle()
            {
                Content = "x"
            };
            canvas.Children.Add(plotter);
            canvas.Children.Add(dateAxis);
            canvas.Children.Add(verticalAxis);
            canvas.Children.Add(countAxis);
            canvas.Children.Add(arialy);
            canvas.Children.Add(arialx);

            WrapPanel wrapPannelSampleNum = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelSampleNum = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品编号:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleNum = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + sampleNum,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelSampleName = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 样品名称:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty + _item.Hole[channel].SampleName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            WrapPanel wrapPannelRGBValue = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelRGBValue = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 灰度值:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxRGBValue = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxRGBValue"
            };
            WrapPanel wrapPannelDetectResult = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelDetectResult = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 检测结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxDetectResult = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = "0.00",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true,
                Name = "textBoxDetectResult"
            };

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelJudgment = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 判定结果:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textJugmentResult = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = "合格",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textJugmentResult"
            };
            //判定标准值
            WrapPanel wrapStandValue = new WrapPanel()
            {
                Width = 180,
                Height = 30
            };
            Label labelStandValue = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = " 标准值:",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textStandValue = new TextBox()
            {
                Width = 90,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = "1.00",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textStandValue"
            };
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
