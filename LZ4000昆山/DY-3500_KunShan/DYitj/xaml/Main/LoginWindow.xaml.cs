using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using AIO.src;
using AIO.xaml.Fenguangdu;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {

        #region 全局变量

        public static UserAccount _userAccount;
        public static CheckPointInfo _samplenameStandard;
        public static DataSet _dsAllTemp;
        /// <summary>
        /// 设备运行状态
        /// </summary>
        public static String deviceStatus = string.Empty;
        #endregion
        public LoginWindow()
        {
            InitializeComponent();
            if (0 == Global.userAccounts.Count)
            {
                UserAccount admin = new UserAccount()
                {
                    UserName = "admin",
                    UserPassword = "admin",
                    Create = true
                };
                Global.userAccounts.Add(admin);
                Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
            }
            TextBoxUserName.Focus();
        }

        private bool CheckedUser()
        {
            if (TextBoxUserName.Text.Trim().Equals(string.Empty))
            {
                TextBoxUserName.Focus();
                MessageBox.Show(this, "用户名不能为空!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (TextBoxUserPassword.Password.Trim().Equals(string.Empty))
            {
                TextBoxUserPassword.Focus();
                MessageBox.Show(this, "密码不能为空!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (TextBoxUserName.Text.Trim().Equals("260905") && TextBoxUserPassword.Password.Trim().Equals("260905"))
            {
                //分光度测试界面
                FgdTest window = new FgdTest()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
                return false;
            }
            return true;
        }

        public void Login()
        {
            if (CheckedUser())
            {
                foreach (UserAccount account in Global.userAccounts)
                {
                    if (account.UserName.Equals(TextBoxUserName.Text.Trim()))
                    {
                        if (account.UserPassword.Equals(TextBoxUserPassword.Password))
                        {
                            _userAccount = account;
                            if (account.Create)
                            {
                                LoginDisplay window = new LoginDisplay()
                                {
                                    ShowInTaskbar = false,
                                    Owner = this
                                };
                                window.ShowDialog();
                            }
                            else
                            {
                                MainWindow window = new MainWindow()
                                {
                                    _userconfig = account,
                                    ShowInTaskbar = false,
                                    Owner = this
                                };
                                window.ShowDialog();
                            }
                            ClearAccount();
                        }
                        else
                        {
                            MessageBox.Show(this, "密码错误!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                            TextBoxUserPassword.Password = string.Empty;
                            TextBoxUserPassword.Focus();
                        }
                        return;
                    }
                }
                MessageBox.Show(this, "用户名不存在!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearAccount();
            }
        }

        /// <summary>
        /// 清空账号和密码信息
        /// </summary>
        private void ClearAccount()
        {
            TextBoxUserName.Text = string.Empty;
            TextBoxUserPassword.Password = string.Empty;
            TextBoxUserName.Focus();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (Global.InterfaceType.Equals("ZH"))
                {
                    if (Wisdom.DEVICESTATUS_REQUEST == null)
                    {
                        Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.deviceStatus.Request();
                    }
                    Wisdom.DEVICESTATUS_REQUEST.deviceStatus = "0";
                    UploadDeviceStatus();
                }
                Environment.Exit(0);
            }
        }

        //新增回车事件，输入完密码之后按回车直接验证账号和密码
        private void TextBoxUserPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Login();
            }
        }

        //账号输入框回车事件,输入完账号后按Enter键即可自动跳转到密码输入框
        private void TextBoxUserName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBoxUserPassword.Password = string.Empty;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (TextBoxUserName.Text.Trim().Equals(string.Empty))
                {
                    TextBoxUserName.Clear();
                    TextBoxUserName.Focus();
                    MessageBox.Show(this, "用户名不能为空!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    TextBoxUserPassword.Focus();
                }
            }
        }

        /// <summary>
        /// 进入登陆界面时初始化分光光度所有项目的对照值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.InterfaceType.Equals("ZH"))
            {
                if (Wisdom.DEVICESTATUS_REQUEST == null)
                    Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.deviceStatus.Request();
                Wisdom.DEVICESTATUS_REQUEST.deviceStatus = "1";
                UploadDeviceStatus();
            }

            labelName.Content = Global.InstrumentNameModel + Global.InstrumentName;
            List<DYFGDItemPara> items = Global.fgdItems;
            if (items != null && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].sc.RefA = items[i].ir.RefDeltaA = Double.MinValue;
                }
            }
            new XmlSerialize().SerializeXMLToFile<List<DYFGDItemPara>>(items, Global.ItemsDirectory + "\\" + "fgdItems.xml");
            if (Global.IsTest)
            {
                TextBoxUserName.Text = "260905";
                TextBoxUserPassword.Password = "260905";
            }
            if (Global.KsVersion.Equals("0"))
            {
                lb_version.Content = "昆山专用(市场) Ver 2.4.3.180308";
            }
            else if (Global.KsVersion.Equals("1"))
            {
                lb_version.Content = "昆山专用(超市) Ver 2.4.3.180308";
            }
            else
            {
                lb_version.Content = "昆山专用(分局) Ver 2.4.3.180308";
            }

            //是否需要更新数据库
            if (!CFGUtils.GetConfig("DataBaseVersion", "0").Equals("3"))
            {
                UpgradeData();
                CFGUtils.SaveConfig("DataBaseVersion", "3");
            }
        }

        /// <summary>
        /// 同步数据库
        /// </summary>
        private void UpgradeData()
        {
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.RepairProjects,
                MessageInfo = "数据更新，请稍后···"
            };
            process.Start();
        }

        private void RepairProjects(Action<int> percent)
        {
            try
            {
                percent(0);
                string err = string.Empty;
                tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
                List<string> sqlList = DataBaseUpgrade.getSql();
                percent(1);
                int len = sqlList.Count;
                float percentage1 = (float)99 / (float)len, percentage2 = 0;
                float count = percentage1 + 1;
                for (int i = 0; i < sqlList.Count; i++)
                {
                    bll.DataBaseRepair(sqlList[i], out err);

                    if (count < 100)
                    {
                        percent((int)count);
                        percentage2 += percentage1;
                        if (percentage2 > 1)
                        {
                            count += percentage2;
                            percentage2 = 0;
                        }
                    }
                    else
                    {
                        count = 100;
                    }
                }
            }
            catch (Exception)
            {
                percent(100);
            }
        }

        private void TextBoxUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String str = TextBoxUserName.Text.Trim();
            if (str.Length > 12)
            {
                TextBoxUserName.Text = str.Remove(12);
                MessageBox.Show(this, "用户名长度不能超过12个字符!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxUserName.Select(str.Length, 0);
            }
        }

        private void TextBoxUserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            String str = TextBoxUserPassword.Password.Trim();
            if (str.Length > 18)
            {
                TextBoxUserPassword.Password = str.Remove(18);
                MessageBox.Show(this, "密码长度不能超过18个字符!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxUserPassword.SelectAll();
            }
        }

        /// <summary>
        /// 上报设备运行状态
        /// </summary>
        private void UploadDeviceStatus()
        {
            try
            {
                if (Wisdom.DeviceID.Length == 0)
                    return;

                Wisdom.DEVICESTATUS_REQUEST.deviceid = Wisdom.DeviceID;
                Wisdom.DEVICESTATUS_REQUEST.longitude = Wisdom.gpsJD;
                Wisdom.DEVICESTATUS_REQUEST.latitude = Wisdom.gpsWD;
                deviceStatus = (Wisdom.UploadDeviceStatus() ? "设备运行状态上报成功：" : "设备运行状态上报失败：")
                    + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            catch (Exception ex)
            {
                deviceStatus = "设备运行状态上报时出现异常:" +
                        ex.Message + "\r\n" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            //lb_DeviceStatus.Content = deviceStatus;
        }

    }
}