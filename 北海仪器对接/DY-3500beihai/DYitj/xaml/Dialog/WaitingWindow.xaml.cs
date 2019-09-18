using System;
using System.Windows;
using System.Windows.Threading;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// WaitingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingWindow : Window
    {
        public WaitingWindow()
        {
            InitializeComponent();
        }
        private DispatcherTimer _DataTimer = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = false;
            this.Topmost = true;
            _DataTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            _DataTimer.Tick += new EventHandler(timer);
            _DataTimer.Start();
        }

        private void timer(object sender, EventArgs e)
        {
            if (Global.WaitingWindowIsClose)
            {
                _DataTimer.Stop();
                _DataTimer = null;
                this.Close();
            }
        }

    }
}