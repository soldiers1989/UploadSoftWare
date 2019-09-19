using System;
using System.Windows;
using System.Windows.Threading;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TimerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimerWindow : Window
    {
        public TimerWindow()
        {
            InitializeComponent();
        }
        private DispatcherTimer dtimer = null;
        private int num = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtimer = new System.Windows.Threading.DispatcherTimer();
            dtimer.Interval = TimeSpan.FromSeconds(1);
            dtimer.Tick += dtimer_Tick;
            dtimer.Start();
            Random rd = new Random();
            num = rd.Next(1, 5);
        }

        void dtimer_Tick(object sender, EventArgs e)
        {
            if (num == 0)
            {
                dtimer.Stop();
                this.Close();
            }
            num--;
        }

    }
}