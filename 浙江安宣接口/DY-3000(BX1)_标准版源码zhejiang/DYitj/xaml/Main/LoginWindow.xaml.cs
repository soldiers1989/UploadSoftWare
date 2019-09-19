using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using AIO.src;
using AIO.xaml.Fenguangdu;
using AIO.xaml.Main;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static UserAccount _userAccount;
        public static CheckPointInfo _samplenameStandard;
        public static DataSet _dsAllTemp;
        public DispatcherTimer _getBatteryServices;
        private UpdateADThread _updateBatteryThread;
        private BatteryWindow _batteryWindow = new BatteryWindow();
        private bool _isRed = false;
        private string logType = "login-error";
        /// <summary>
        /// 设备运行状态
        /// </summary>
        public static String deviceStatus = string.Empty;

        public LoginWindow()
        {
            InitializeComponent();
            try
            {
                if (Global.userAccounts == null || Global.userAccounts.Count == 0)
                {
                    UserAccount admin = new UserAccount();
                    admin.UserName = "admin";
                    admin.UserPassword = "admin";
                    admin.Create = true;
                    Global.userAccounts = new List<UserAccount>();
                    Global.userAccounts.Add(admin);
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                }
                TextBoxUserName.Focus();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private bool CheckedUser()
        {
            try
            {
                if (TextBoxUserName.Text.Trim().Equals(""))
                {
                    TextBoxUserName.Focus();
                    MessageBox.Show("用户名不能为空！", "操作提示");
                    return false;
                }
                else if (TextBoxUserPassword.Password.Trim().Equals(""))
                {
                    TextBoxUserPassword.Focus();
                    MessageBox.Show("密码不能为空！", "操作提示");
                    return false;
                }
                else if (TextBoxUserName.Text.Trim().Equals("260905") && TextBoxUserPassword.Password.Trim().Equals("260905"))
                {
                    //分光度测试界面
                    FgdTest window = new FgdTest();
                    window.ShowInTaskbar = false; window.Owner = this; window.Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            return true;
        }

        public void Login()
        {
            try
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
                                    LoginDisplay window = new LoginDisplay();
                                    window.ShowInTaskbar = false;
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                else
                                {
                                    MainWindow window = new MainWindow();
                                    window._userconfig = account;
                                    window.ShowInTaskbar = false;
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                ClearAccount();
                            }
                            else
                            {
                                MessageBox.Show("密码错误!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                                TextBoxUserPassword.Password = string.Empty;
                                TextBoxUserPassword.Focus();
                            }
                            return;
                        }
                    }
                    MessageBox.Show("用户名不存在!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearAccount();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("登录出现异常！\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 清空账号和密码信息
        /// </summary>
        private void ClearAccount()
        {
            try
            {
                TextBoxUserName.Text = string.Empty;
                TextBoxUserPassword.Password = string.Empty;
                TextBoxUserName.Focus();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
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
                        Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.DataModel.Wisdom.deviceStatus.Request();
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
            try
            {
                TextBoxUserPassword.Password = string.Empty;
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (TextBoxUserName.Text.Trim().Equals(string.Empty))
                    {
                        TextBoxUserName.Clear();
                        TextBoxUserName.Focus();
                        MessageBox.Show("用户名不能为空！", "操作提示");
                    }
                    else
                    {
                        TextBoxUserPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void updateJtjItems()
        {
            List<DYJTJItemPara> items = Global.jtjItems;
            if (items != null && items.Count > 0)
            {
                for (int i = 1; i < items.Count; i++)
                {
                    items[i].InvalidC = items[0].InvalidC;
                    items[i].dxxx.PlusT = items[0].dxxx.PlusT;
                    items[i].dxxx.MinusT = items[0].dxxx.MinusT;
                    items[i].dxxx.SuspiciousMin = items[0].dxxx.SuspiciousMin;
                    items[i].dxxx.SuspiciousMax = items[0].dxxx.SuspiciousMax;
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
            try
            {
                //广东省智慧云平台上传设备开关机状态
                if (Global.InterfaceType.Equals("ZH"))
                {
                    if (Wisdom.DEVICESTATUS_REQUEST == null)
                        Wisdom.DEVICESTATUS_REQUEST = new DYSeriesDataSet.DataModel.Wisdom.deviceStatus.Request();
                    Wisdom.DEVICESTATUS_REQUEST.deviceStatus = "1";
                    UploadDeviceStatus();
                }

                if (Global.IsTest)
                {
                    TextBoxUserName.Text = "260905";
                    TextBoxUserPassword.Password = "260905";
                }
                labelName.Content = Global.InstrumentNameModel + Global.InstrumentName;
                List<DYFGDItemPara> items = Global.fgdItems;
                if (items != null && items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        items[i].sc.RefA = items[i].ir.RefDeltaA = items[i].co.coeff = items[i] .dn.dnn= Double.MinValue;
                    }
                }
                new XmlSerialize().SerializeXMLToFile<List<DYFGDItemPara>>(items, Global.ItemsDirectory + "\\" + "fgdItems.xml");
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            finally
            {
                if (Global.EnableBattery)
                {
                    //窗口初始化时立刻获取电池状态
                    _batteryWindow.ShowInTaskbar = false;
                    _batteryWindow.Show();
                    _updateBatteryThread = new UpdateADThread(this);
                    _updateBatteryThread.Start();
                    getBattery(null, null);

                    //定时任务
                    _getBatteryServices = new DispatcherTimer();
                    _getBatteryServices.Interval = TimeSpan.FromSeconds(5);
                    _getBatteryServices.Tick += new EventHandler(getBattery);
                    _getBatteryServices.Start();
                    TextBoxUserName.Focus();
                }
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

        /// <summary>
        /// 获取电池状态
        /// MSG_GET_BATTERY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getBattery(object sender, EventArgs e)
        {
            if (Global.IsStartGetBattery)
            {
                Message msg = new Message();
                msg.what = MsgCode.MSG_GET_BATTERY;
                msg.str1 = Global.strADPORT;
                Global.workThread.SendMessage(msg, _updateBatteryThread);
            }
        }

        private void TextBoxUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                //禁止输入空格
                TextBox tb = sender as TextBox;
                tb.Text = tb.Text.Replace(" ", "");
                String str = TextBoxUserName.Text.Trim();
                if (str.Length > 12)
                {
                    TextBoxUserName.Text = str.Remove(12);
                    MessageBox.Show("用户名长度不能超过12个字符！", "操作提示");
                    TextBoxUserName.Select(str.Length, 0);
                }
                TextBoxUserName.Focus();
            }
            catch (Exception)
            {
                //无需记录日志
            }
        }

        private void TextBoxUserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                //禁止输入空格
                TextBox tb = sender as TextBox;
                tb.Text = tb.Text.Replace(" ", "");
                String str = TextBoxUserPassword.Password.Trim();
                if (str.Length > 18)
                {
                    TextBoxUserPassword.Password = str.Remove(18);
                    MessageBox.Show("密码长度不能超过18个字符！", "操作提示");
                    TextBoxUserPassword.SelectAll();
                }
                TextBoxUserPassword.Focus();
            }
            catch (Exception)
            {
                //无需记录日志
            }
        }

        /// <summary>
        /// 设置电池信息
        /// </summary>
        /// <param name="datas"></param>
        private void settingBatteryInformation(List<byte[]> datas)
        {
            string str = string.Empty;
            try
            {
                byte[] data;
                if (datas != null)
                {
                    data = datas[0];
                    //电池状态
                    if (data != null)
                    {
                        //没有电池 0x00
                        if (data[4] == 0x00)
                        {
                            str = "没有电池！";
                            _isRed = true;
                        }
                        //已充满电 0x01
                        else if (data[4] == 0x01)
                        {
                            str = "已充满";
                            _isRed = false;
                        }
                        //正在充电 0x02
                        else if (data[4] == 0x02)
                        {
                            str = "正在充电";
                            _isRed = false;
                        }
                        //放电 0x03
                        else if (data[4] == 0x03)
                        {
                            str = "未充电";
                            _isRed = false;
                        }
                        //温度异常 0x04
                        else if (data[4] == 0x04)
                        {
                            str = "温度异常";
                            _isRed = true;
                        }
                        //其他故障 0x05
                        else if (data[4] == 0x05)
                        {
                            str = "电池异常！";
                            _isRed = true;
                        }
                        else
                        {
                            str = "";
                        }
                    }

                    //电池电量
                    data = datas[1];
                    if (data != null)
                    {
                        str += " 当前电量:" + data[4].ToString() + "%";
                    }
                }
                else
                {
                    str = "未获取到电池信息！";
                    _isRed = true;
                }
            }
            catch (Exception ex)
            {
                str = "获取电池信息出现异常！";
                _isRed = true;
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            finally
            {
                _batteryWindow._battery = str;
                _batteryWindow._isRed = _isRed;
                _batteryWindow.settingBattery();
            }
        }

        class UpdateADThread : ChildThread
        {
            LoginWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public UpdateADThread(LoginWindow wnd)
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
                    FileUtils.OprLog(6, wnd.logType, ex.ToString());
                }
            }

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_GET_BATTERY:
                        if (msg.result)
                            wnd.settingBatteryInformation(msg.batteryData);
                        else
                            wnd.settingBatteryInformation(null);
                        wnd._updateBatteryThread.Stop();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}