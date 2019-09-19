using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            string[] cruves = cruveDatas.Split(',');
            //绘制曲线
            string[] data = cruves[0].Split('|');
            int len = data.Length;
            DateTime[] dates = new DateTime[len];
            double[] numberOpen = new double[len];
            for (int i = 0; i < len; i++)
            {
                dates[i] = Convert.ToDateTime("01/01/000" + (i + 1));
                numberOpen[i] = double.Parse(data[i]);
            }

            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

            var numberOpenDataSource = new EnumerableDataSource<double>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);
            CompositeDataSource compositeDataSource = new CompositeDataSource(datesDataSource, numberOpenDataSource);

            plotter.AddLineGraph(compositeDataSource,
                                            new Pen(Brushes.Blue, 1),
                                            new CirclePointMarker { Size = 3, Fill = Brushes.Red },
                                            null);
            plotter.Viewport.FitToView();

            if (cruves.Length == 2)
            {
                string[] dts = cruves[1].Split('|');
                if (dts.Length >= 86405)
                {
                    Img.Visibility = Visibility.Visible;
                    this.Width = 740;
                    List<byte> dtList = new List<byte>();
                    for (int i = 0; i < dts.Length; i++)
                    {
                        if (dts[i].Length <= 0) continue;
                        dtList.Add(byte.Parse(dts[i]));
                    }
                    byte[] datas = new byte[dtList.Count];
                    dtList.CopyTo(datas);

                    BitmapSource img = BufferToBitmap(datas);
                    ImageBrush ib = new ImageBrush()
                    {
                        ImageSource = img,
                        TileMode = TileMode.None,
                        Stretch = Stretch.None,
                        AlignmentX = AlignmentX.Left,
                        AlignmentY = AlignmentY.Top
                    };
                    Img.Background = ib;
                    return;
                }
            }


        }

        private BitmapSource BufferToBitmap(byte[] buffer)
        {
            try
            {
                BitmapSource img = null;
                if (buffer != null)
                {
                    int HDR_LEN = 5;
                    int WIDTH = (int)((uint)buffer[1] + (uint)(buffer[2] << 8));
                    int HEIGHT = (int)((uint)buffer[3] + (uint)(buffer[4] << 8));
                    int BITDEPTH = 2;
                    int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);

                    byte[] pixels = new byte[DATA_LEN];
                    Array.Copy(buffer, HDR_LEN, pixels, 0, DATA_LEN);
                    byte[] pixelsrgb = new byte[WIDTH * HEIGHT * 3];

                    for (int j = 0; j < HEIGHT; ++j)
                    {
                        for (int i = 0; i < WIDTH; ++i)
                        {
                            int offset16 = (j * WIDTH + i) * 2;
                            int offset24 = (j * WIDTH + i) * 3;
                            // rgb565 低字节在前
                            int rgb565 = (int)(pixels[offset16] + ((uint)pixels[offset16 + 1] << 8));
                            int red = ((rgb565 >> 11) & 0x1F) << 3;
                            int green = ((rgb565 >> 5) & 0x3F) << 2;
                            int blue = ((rgb565) & 0x1F) << 3;
                            pixelsrgb[offset24] = (byte)blue;
                            pixelsrgb[offset24 + 1] = (byte)green;
                            pixelsrgb[offset24 + 2] = (byte)red;
                        }
                    }
                    img = BitmapSource.Create(WIDTH, HEIGHT, 96, 96, PixelFormats.Bgr24, BitmapPalettes.WebPalette, pixelsrgb, WIDTH * 3);
                }
                return img;
            }
            catch (Exception)
            {
                return null;
            }
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

        private void BtnShowCruves_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnShowImg_Click(object sender, RoutedEventArgs e)
        {
            plotter.Visibility = Visibility.Collapsed;

        }

    }
}