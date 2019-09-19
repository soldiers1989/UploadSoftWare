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

namespace GPS.window
{
    /// <summary>
    /// InputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputWindow : Window
    {
        public bool IsPassWord = false;
        /// <summary>
        /// 显示坐标
        /// </summary>
        private double xPoint, yPoint;
        public InputWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //设置窗体的显示位置
            xPoint = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            yPoint = SystemParameters.WorkArea.Size.Height - this.Height;
            this.Top = yPoint;
            this.Left = xPoint;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (pwb.Password .Trim() == "sakj")
            {
                IsPassWord = true;
                this.Close();
            }
            else
            {
                IsPassWord = false;
                pwb.Password = "";
                labelInfo.Content = "密码错误！";
            }
        }

        private void btnCance_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
