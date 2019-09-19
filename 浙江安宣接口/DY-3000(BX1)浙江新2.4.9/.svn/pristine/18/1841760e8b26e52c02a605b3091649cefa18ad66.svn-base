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
using System.Windows.Threading;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// WarnTipWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WarnTipWindow : Window
    {
        private DispatcherTimer _GetTimer = null;
        private int t = 5;
        public WarnTipWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _GetTimer = new DispatcherTimer();
            _GetTimer.Interval = TimeSpan.FromSeconds(1);
            _GetTimer.Tick += _GetTimer_Tick;
            _GetTimer.Start();
        }
        private void _GetTimer_Tick(object sender, EventArgs e)
        {
            t--;
            labelTime.Content = "倒计时" + t + "S后系统自动关闭";
            if (t < 0)
            {
                t = 10;
                _GetTimer.Stop();
                this.Close();
            }
        }

        private void labelclose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _GetTimer.Stop();
            this.Close();
        }

        private void labelclose_MouseEnter(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void labelclose_MouseLeave(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff0000"));
        }
    }
}
