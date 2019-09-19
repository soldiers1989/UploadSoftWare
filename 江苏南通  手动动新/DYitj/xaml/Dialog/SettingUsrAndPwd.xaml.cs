using com.lvrenyang;
using System;
using System.Windows;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SettingUsrAndPwd.xaml 的交互逻辑
    /// </summary>
    public partial class SettingUsrAndPwd : Window
    {
        public SettingUsrAndPwd()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.tb_usr.Text = Wisdom.USER;
            this.tb_pwd.Text = Wisdom.PASSWORD;
        }

        private void tb_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = tb_usr.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("账号信息不能为空!", "操作提示");
                    tb_usr.Focus();
                    return;
                }
                Wisdom.USER = str;

                str = tb_pwd.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("密码不能为空!", "操作提示");
                    tb_pwd.Focus();
                    return;
                }
                Wisdom.PASSWORD = str;
                CFGUtils.SaveConfig("USER", Wisdom.USER);
                CFGUtils.SaveConfig("PASSWORD", Wisdom.PASSWORD);
                MessageBox.Show("智慧食药监 - 账号信息设置成功!", "操作提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tb_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}