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
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// LogonMessage.xaml 的交互逻辑
    /// </summary>
    public partial class LogonMessage : Window
    {
        public LogonMessage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Global.samplenameadapter.Count > 0)
            {
                CheckPointInfo CPoint = Global.samplenameadapter[0];
                webaddr.Text = CPoint.url;
                username.Text = CPoint.user;
                password.Password = CPoint.pwd;
      
  
            }
        }

        private void btn_autoUpdater_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //if (webaddr.Text.Trim() == "")
            //{
            //    MessageBox.Show("服务器地址不能为空", "提示");
            //    return;
            //}
            //if (username.Text.Trim() == "")
            //{
            //    MessageBox.Show("用户名不能为空", "提示");
            //    return;
            //}
            //if (password.Password.Trim() == "")
            //{
            //    MessageBox.Show("密码不能为空", "提示");
            //    return;
            //}
            CheckPointInfo CPoint;
            Global.samplenameadapter = Global.samplenameadapter ?? new List<CheckPointInfo>();
            CPoint = Global.samplenameadapter.Count == 0 ? new CheckPointInfo() : Global.samplenameadapter[0];
            CPoint.url = webaddr.Text.Trim();
            CPoint.user = username.Text.Trim();
            CPoint.pwd = password.Password;

            if (Global.samplenameadapter.Count == 0)
                Global.samplenameadapter.Add(CPoint);
            Global.SerializeToFile(Global.samplenameadapter, Global.samplenameadapterFile);

            this.Close();
        }
        /// <summary>
        /// 是否自动测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNet_Click(object sender, RoutedEventArgs e)
        {
            //if (CheckNet.IsChecked == true)
            //{
            //    Global.IsAutoTest = "自动";
               
            //    CFGUtils.SaveConfig("isAutoTest", Global.IsAutoTest);
            //}
            //else
            //{
            //    Global.IsAutoTest = "手动";
                
            //    CFGUtils.SaveConfig("isAutoTest", Global.strADPORT);
            //}
        }
    }
}
