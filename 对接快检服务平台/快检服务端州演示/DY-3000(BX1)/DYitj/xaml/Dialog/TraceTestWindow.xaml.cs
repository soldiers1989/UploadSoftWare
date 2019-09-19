using System;
using System.Windows;
using System.Windows.Threading;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TraceTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TraceTestWindow : Window
    {
        public TraceTestWindow()
        {
            InitializeComponent();
        }

        public String title = String.Empty;
        private int _stopNum = 0;
        private int len = 0;
        private DispatcherTimer _DataTimer = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.Height = 36;
            if (_DataTimer == null)
                _DataTimer = new DispatcherTimer();
            if (title.Length == 0)
            {
                _stopNum = ran.Next(4, 10);
                label1.Content = "请稍等，正在查询溯源信息·";
                _DataTimer.Interval = TimeSpan.FromSeconds(0.5);
                _DataTimer.Tick += new EventHandler(wart);
                _DataTimer.Start();
            }
            else if (title.Equals("gps"))
            {
                _stopNum = ran.Next(4, 10);
                label1.Content = "请稍等，正在定位中·";
                _DataTimer.Interval = TimeSpan.FromSeconds(0.5);
                _DataTimer.Tick += new EventHandler(wartGps);
                _DataTimer.Start();
            }
            else if (title.Equals("upgrade"))
            {
                _stopNum = ran.Next(4, 10);
                label1.Content = "请稍等，正在检查更新·";
                _DataTimer.Interval = TimeSpan.FromSeconds(0.5);
                _DataTimer.Tick += new EventHandler(wartGps);
                _DataTimer.Start();
            }
        }

        private void wartGps(object sender, EventArgs e)
        {
            len++;
            label1.Content += "·";
            if (len == _stopNum)
            {
                _DataTimer.Stop();
                this.Close();
            }
        }

        private void wart(object sender, EventArgs e)
        {
            len++;
            label1.Content += "·";
            if (len == _stopNum)
            {
                _DataTimer.Stop();
                this.Close();
            }
        }

    }
}