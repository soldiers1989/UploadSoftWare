using GPS.model;
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
    /// SystemInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SystemInfoWindow : Window
    {
        /// <summary>
        /// 显示坐标
        /// </summary>
        private double xPoint, yPoint;

        public SystemInfoWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //设置窗体的显示位置
                xPoint = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
                yPoint = SystemParameters.WorkArea.Size.Height - this.Height;
                this.Top = yPoint;
                this.Left = xPoint;
                txtFactoryNo.Text = Global.FactoryNumber;
                txtServerce.Text = Global.ServerAddr;
                txtSoftware.Text = Global.SoftwareVer;
                txtHareware.Text = Global.HardwareVer;


            }
            catch(Exception ex)
            {
                
            }
        }
        
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void labelclose_MouseLeave(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.White);
        }

        private void labelclose_MouseEnter(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void labelsave_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = new SolidColorBrush(Colors.Red);
        }

        private void labelsave_MouseLeave(object sender, MouseEventArgs e)
        {

            ((Label)sender).Foreground = new SolidColorBrush(Colors.BlueViolet);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelsave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Global.FactoryNumber = txtFactoryNo.Text.Trim();
                Global.ServerAddr = txtServerce.Text.Trim();
                Global.SoftwareVer= txtSoftware.Text.Trim();
                Global.HardwareVer = txtHareware.Text.Trim();
                CFGUtils.SaveConfig("ServerAddr", Global.ServerAddr );
                CFGUtils.SaveConfig("factoryNo", Global.FactoryNumber );
                CFGUtils.SaveConfig("SoftwareVer", Global.SoftwareVer );
                CFGUtils.SaveConfig("HardwareVer", Global.HardwareVer );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void labelReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
