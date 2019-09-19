using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DYSeriesDataSet;

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
            try
            {
                if (model.ResultType.Equals("胶体金"))
                {
                    string[] data = cruveDatas.Split(',');
                    string data1 = data[0];
                    string data2 = data[1];

                    string[] datas1 = data1.Split('|');
                    string[] datas2 = data2.Split('|');
                    byte[] grayValues = new byte[datas1.Length];
                    for (int i = 0; i < grayValues.Length; i++)
                    {
                        grayValues[i] = Convert.ToByte(datas1[i]);
                    }
                    int cOffset = Convert.ToInt32(datas2[0]);
                    int tOffset = Convert.ToInt32(datas2[1]);
                    DrawGrayCurve(grayValues, cOffset, tOffset);
                }
                else
                {
                    string[] data = cruveDatas.Split('|');
                    byte r, g, b;
                    r = (byte)Convert.ToDouble(data[0]);
                    g = (byte)Convert.ToDouble(data[1]);
                    b = (byte)Convert.ToDouble(data[2]);
                    plotter.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("绘制曲线时出现异常！\r\n异常信息：" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawGrayCurve(byte[] grayValues, int cOffset, int tOffset)
        {
            double yOffset = 20;
            double width = plotter.Width;
            double height = plotter.Height - yOffset;
            double max = GetMaxByte(grayValues);
            double min = GetMinByte(grayValues);
            double curveHeight = max - min;
            if (0 == curveHeight)
                return;
            double curveWidth = grayValues.Length;

            Polyline polyline = new Polyline()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            PointCollection points = new PointCollection();
            for (int i = 0; i < grayValues.Length; ++i)
            {
                Point point = new Point(i * width / curveWidth, height - (grayValues[i] - min) * height / curveHeight);
                points.Add(point);
            }
            polyline.Points = points;
            plotter.Children.Add(polyline);

            for (int n = 0; n < 5; ++n)
            {
                Line c = new Line()
                {
                    X1 = (cOffset - n) * width / curveWidth,
                    Y1 = height + yOffset,
                    X2 = (cOffset - n) * width / curveWidth,
                    Y2 = height - (grayValues[cOffset - n] - min) * height / curveHeight,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                Line t = new Line()
                {
                    X1 = (tOffset - n) * width / curveWidth,
                    Y1 = height + yOffset,
                    X2 = (tOffset - n) * width / curveWidth,
                    Y2 = height - (grayValues[tOffset - n] - min) * height / curveHeight,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                plotter.Children.Add(c);
                plotter.Children.Add(t);
            }
        }

        private byte GetMaxByte(byte[] data)
        {
            byte b = 0;
            for (int i = 0; i < data.Length; ++i)
                if (b < data[i])
                    b = data[i];
            return b;
        }

        private byte GetMinByte(byte[] data)
        {
            byte b = 255;
            for (int i = 0; i < data.Length; ++i)
                if (b > data[i])
                    b = data[i];
            return b;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
