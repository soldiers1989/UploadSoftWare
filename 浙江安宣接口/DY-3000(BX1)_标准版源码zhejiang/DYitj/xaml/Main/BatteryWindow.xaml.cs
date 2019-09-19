using System.Windows;
using System.Windows.Forms;

namespace AIO.xaml.Main
{
    /// <summary>
    /// BatteryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryWindow : Window
    {
        public string _battery = string.Empty;
        public bool _isRed = false;

        public BatteryWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            coordinates();
        }

        public void settingBattery()
        {
            coordinates();
            this.lb_battery.Content = _battery;
        }

        private void coordinates()
        {
            this.Topmost = true;
            double x = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            double y = SystemInformation.PrimaryMonitorSize.Height - this.Height;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = x;
            this.Top = 0;
        }

    }
}