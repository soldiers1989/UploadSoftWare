using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using AIO.xaml.Fenguangdu;
using AIO.xaml.Dialog;
using com.lvrenyang;
using AIO.src;
using System.Text;

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
        private MsgThread _msgThread;
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
                                //MainWindow window = new MainWindow()
                                //{
                                //    _userconfig = account,
                                //    ShowInTaskbar = false,
                                //    Owner = this
                                //};
                                //window.ShowDialog();
                                TaskDisplay window = new TaskDisplay()
                                {
                                    _userconfig = account,
                                    ShowInTaskbar = false,
                                    Owner = this,
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
                //有网络时
                string err = "";
                //if (Global.UploadCheck(this, out err))
                //{
                //    string x = "";
                //    string y = "";
                //    AddressInfo address = AddressInfo.GetAddressByBaiduAPI();
                //    if (address != null)
                //    {
                //        x = address.content.point.x; //"113.459024";//;
                //        y = address.content.point.y;// "23.104707"; //
                //    }

                //    string url = InterfaceHelper.GetServiceURL(Global.GPSAddress, 16);
                //    StringBuilder sb = new StringBuilder();
                //    string message = AES.Encrypt(Global.FactoryNumber, "DYXX@)!(QPMZA2-5");
                //    message = message.Replace('+', '-').Replace('/', '_').Replace("=", "");
                //    //Console.WriteLine(message);

                //    string token = message;
                //    heartbeat ht = new heartbeat();
                //    ht.status = "0";//关机机
                //    ht.onlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //    ht.softwareVersion = "DY-3500(I)食品综合分析仪 2.4.6.2 KJFW";
                //    ht.offlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //    ht.handwareVersion = "1.0";
                //    ht.longitude = x;
                //    ht.latitude = y;
                //    string json = InterfaceHelper.EntityToJson(ht);
                //    sb.AppendFormat("{0}?userToken={1}", url, token);
                //    sb.AppendFormat("&results={0}", json);

                //    FileUtils.KLog(sb.ToString(), "发送", 23);
                //    string result = InterfaceHelper.HttpsPost(sb.ToString());
                //    FileUtils.KLog(result, "接收", 23);

                //    if (result.Contains("msg") || result.Contains("resultCode"))
                //    {
                //        resultdata rtn = InterfaceHelper.JsonToEntity<resultdata>(result);
                //        if (rtn != null)
                //        {
                //            if (rtn.resultCode == "0X00000")
                //            {
                //                Console.WriteLine("数据同步成功！" + rtn.msg);
                //            }
                //            else
                //            {
                //                Console.WriteLine("数据同步失败！失败原因：" + rtn.msg);
                //            }
                //        }
                //        //else
                //        //{
                //        //    Console.WriteLine("数据同步失败！失败原因：" + result);
                //        //}
                //    }
                //}

                //if (Global.InterfaceType.Equals("ZH"))
                //{
                //    if (Wisdom.DEVICESTATUS_REQUEST == null)
                //    {
                //        Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.deviceStatus.Request();
                //    }
                //    Wisdom.DEVICESTATUS_REQUEST.deviceStatus = "0";
                //    UploadDeviceStatus();
                //}
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
            _msgThread = new MsgThread(this);
            _msgThread.Start();

            GetVers();

            if (Global.InterfaceType.Equals("ZH"))
            {
                if (Wisdom.DEVICESTATUS_REQUEST == null)
                    Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.deviceStatus.Request();
                Wisdom.DEVICESTATUS_REQUEST.deviceStatus = "1";
                UploadDeviceStatus();
            }
            labelName.Content = Global.SDdongying;
            //labelTitle.Content = "DY-3500Plus(I)"+ Global.InstrumentName;
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
            //有网络时
            string err = "";
            if (Global.UploadCheck(this, out err))
            {
                //string x = "";
                //string y = "";
                //AddressInfo address =AddressInfo.GetAddressByBaiduAPI();
                //if (address != null)
                //{
                //    x = address.content.point.x; //"113.459024";//;
                //    y = address.content.point.y;// "23.104707"; //
                //}

                //string url = InterfaceHelper.GetServiceURL(Global.GPSAddress, 16);
                //StringBuilder sb = new StringBuilder();
                //string message = AES.Encrypt("DY3500I_1234", "DYXX@)!(QPMZA2-5");
                //message = message.Replace('+', '-').Replace('/', '_').Replace("=", "");
                //Console.WriteLine(message);

                //string token = message;
                //heartbeat ht = new heartbeat();
                //ht.status = "1";//开机
                //ht.onlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //ht.softwareVersion = "DY-3500(I)食品综合分析仪 2.4.6.2 KJFW";
                //ht.offlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //ht.handwareVersion = "1.0";
                //ht.longitude = x;
                //ht.latitude = y;
                //string json = InterfaceHelper.EntityToJson(ht);
                //sb.AppendFormat("{0}?userToken={1}", url, token);
                
                //sb.AppendFormat("&results={0}", json);

                //FileUtils.KLog(sb.ToString(), "发送", 23);
                //string result = InterfaceHelper.HttpsPost(sb.ToString());
                //FileUtils.KLog(result, "接收", 23);

                //if (result.Contains("msg") || result.Contains("resultCode"))
                //{
                //    resultdata rtn = InterfaceHelper.JsonToEntity<resultdata>(result);
                //    if (rtn != null)
                //    {
                //        if (rtn.resultCode == "0X00000")
                //        {
                //            Console.WriteLine("数据同步成功！" + rtn.msg);
                //        }
                //        else
                //        {
                //            Console.WriteLine("数据同步失败！失败原因：" + rtn.msg);
                //        }
                //    }
                //    else
                //    {
                //        Console.WriteLine("数据同步失败！失败原因：" + result);
                //    }
                //}
            }
        }
        /// <summary>
        /// 获取版本号
        /// </summary>
        private void GetVers()
        {
            if (Global.IsConnectInternet())
            {
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SoftwareVersion,
                    str1 = Environment.CurrentDirectory + "\\UpdateList.xml",
                };

                Global.workThread.SendMessage(msg, _msgThread);
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

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Image_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("单击鼠标右键");
        }

        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("双击鼠标");
            LogonMessage window = new LogonMessage();
            window.ShowDialog();
        }


        class MsgThread : ChildThread
        {
            LoginWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(LoginWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, "版本升级", ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_SoftwareVersion:
                        if (msg.result == true)
                        {
                            wnd.UpdateVersion();
                        }

                        break;
                    default:
                        break;
                }
            }
        }
        private void UpdateVersion()
        {
            if (MessageBox.Show("温馨提示：\r\n检测到系统新版本,是否现在更新？", "版本更新提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //标记分光在用
                CFGUtils.SaveConfig("FgIsStart", "1");
                //杀掉电池应用
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcesses();
                for (int i = 0; i < ps.Length; i++)
                {
                    if (ps[i].ProcessName.StartsWith("BatteryManage"))
                    {
                        ps[i].Kill();
                        break;
                    }
                }

                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");

            }
        }
    }
}
