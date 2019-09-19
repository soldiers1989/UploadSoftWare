using New_DY_3500_I_.FenGuangDu;
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

namespace New_DY_3500_I_
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("是否退出系统", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            
        }
        /// <summary>
        /// 快速检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageTest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FGDWindow window = new FGDWindow();
            window.ShowDialog();
        }
        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageManage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
