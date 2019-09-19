using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace New_DY_3500_I_.Logon
{
    /// <summary>
    /// LogonWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogonWindow : Window
    {
        public LogonWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordboxLogon.Focus();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogon_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FenGuangDu.FGDWindow window = new FenGuangDu.FGDWindow();
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否退出登录？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 按回车键登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordboxLogon_KeyDown(object sender, KeyEventArgs e)
        {
            this.Hide();
            FenGuangDu.FGDWindow window = new FenGuangDu.FGDWindow();
            window.Owner = this;
            window.ShowDialog();
            
        }
        /// <summary>
        /// 鼠标进入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogon_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("进入");
        }
        /// <summary>
        /// 鼠标离开按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogon_MouseLeave(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("离开");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            MainWindow window = new MainWindow();
            //FenGuangDu.FGDWindow window = new FenGuangDu.FGDWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void ImageExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("是否退出登录？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}
