using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AIO.src;
using AIO.xaml.Fenguangdu;
using AIO.xaml.KjService;
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
        public static UserAccount _userAccount;
        public static CheckPointInfo _samplenameStandard;
        public static DataSet _dsAllTemp;
        private string logType = "login-error";
        private MsgThread _msgThread;
        /// <summary>
        /// 设备运行状态
        /// </summary>
        public static String deviceStatus = string.Empty;

        public LoginWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
            try
            {
                if (Global.userAccounts == null || Global.userAccounts.Count == 0)
                {
                    UserAccount admin = new UserAccount
                    {
                        UserName = "admin",
                        UserPassword = "admin",
                        Create = true
                    };
                    Global.userAccounts = new List<UserAccount>
                    {
                        admin
                    };
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                }
                TextBoxUserName.Focus();
                CFGUtils.SaveConfig("FgIsStart", "0");
              
                bool isCunzai = false;
                Process[] p = Process.GetProcesses();
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        if (p[i].ProcessName.Equals("BatteryManage"))
                        {
                            isCunzai = true;
                        }
                    }
                }
                if (isCunzai == false)
                {
                    if(Global.InstrumentNameModel.Contains ("DY-3500(I)") ==false )
                        System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\BatteryManage.exe");
                }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        /// <summary>
        /// 同步数据库
        /// </summary>
        private void CheckData() 
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
                List<string> sqlList = DataBaseOpr.getSql();
                percent(1);
                int len = sqlList.Count;
                float percentage1 = (float)99 / (float)len, percentage2 = 0;
                float count = percentage1 + 1;
                for (int i = 0; i < sqlList.Count; i++)
                {
                    bll.DataBaseRepair(sqlList[i], out err);
                    if (err.Length > 0)
                    {
                        //string sql = sqlList[i];
                        //MessageBox.Show(sql + ":" + err);
                    }
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
                    //进入分光测试界面前停止电池状态获取
                    CFGUtils.SaveConfig("FgIsStart", "1");
                    //分光度测试界面
                    FgdTest window = new FgdTest
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
                    //退出分光测试界面前开始电池状态获取
                    CFGUtils.SaveConfig("FgIsStart", "0");
                    return false;
                }

                if (Global.EnableValidCode)
                {
                    if (txt_ValidCode.Text.Length == 0)
                    {
                        txt_ValidCode.Focus();
                        MessageBox.Show(this, "请输入验证码!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                    //忽略大小写
                    else if (!String.Equals(checkCode, txt_ValidCode.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        txt_ValidCode.Focus();
                        MessageBox.Show(this, "验证码不正确!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
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
                                    LoginDisplay window = new LoginDisplay
                                    {
                                        ShowInTaskbar = false,
                                        Owner = this
                                    };
                                    window.ShowDialog();
                                }
                                else
                                {
                                    ////快检服务中心，以检测任务为主界面
                                    //if (Global.InterfaceType.Equals("KJ"))
                                    //{
                                    //    CheckTasks window = new CheckTasks();
                                    //    window.ShowInTaskbar = false;
                                    //    window.Owner = this;
                                    //    window.ShowDialog();
                                    //}
                                    //else
                                    //{
                                    MainWindow window = new MainWindow
                                    {
                                        ShowInTaskbar = false,
                                        Owner = this
                                    };
                                    window.ShowDialog();
                                    //}
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
                CreateVaildCode();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        ValidCode validCode = null;
        string checkCode = string.Empty;
        private void CreateVaildCode()
        {
            try
            {
                if (Global.EnableValidCode)
                {
                    ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
                    image_ValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
                    checkCode = validCode.CheckCode;
                    txt_ValidCode.Text = "";
                }
                else
                {
                    sp_ValidCode.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            CFGUtils.SaveConfig("FgIsStart", "0");
           

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
                    str1 = System.Windows.Forms.Application.StartupPath + "\\UpdateList.xml",
                };

                Global.workThread.SendMessage(msg, _msgThread);
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
                GetVers();//获取版本号提示用户
                CreateVaildCode();
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
                //重置分光对照值
                List<DYFGDItemPara> fgitems = Global.fgdItems;
                if (fgitems != null && fgitems.Count > 0)
                {
                    for (int i = 0; i < fgitems.Count; i++)
                    {
                        fgitems[i].sc.RefA = fgitems[i].ir.RefDeltaA = Double.MinValue;
                    }
                }
                new XmlSerialize().SerializeXMLToFile<List<DYFGDItemPara>>(fgitems, Global.ItemsDirectory + "\\" + "fgdItems.xml");
                //重置干化学对照值
                if (Global.gszItems != null && Global.gszItems.Count > 0)
                {
                    for (int i = 0; i < Global.gszItems.Count; i++)
                    {
                        Global.gszItems[i].dx.DeltaA = new double[Global.deviceHole.SxtCount];
                        Global.gszItems[i].dx.DeltaA[0] = Double.MinValue;
                        Global.gszItems[i].dx.DeltaA[1] = Double.MinValue;
                    }
                }
                Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            finally
            {
                //是否需要更新数据库
                if (!CFGUtils.GetConfig("DataBaseVersion", "0").Equals("1"))
                {
                    CheckData();
                    CFGUtils.SaveConfig("DataBaseVersion", "1");
                }
                if (Global.EnableBattery)
                {
                    TextBoxUserName.Focus();
                }
#if DEBUG
              
#else
                StartBatteryMsg();
                //DataTeest();
#endif
            }
        }

        private void DataTeest()
        {
            string val = "5758|57857|15206|16514|16381|16188|16004|15797|15602|15459|15256|15080|14900|14672|14510|14327|14135|13987|13815|13626|13467|13310|13153|13024|12869|12781|12712|12583|12509|12395|12286|12220|12071|11937|11817|11636|11457|11288|11078|10884|10721|10564|10476|10375|10311|10333|10278|10275|10287|10207|10168|10086|9982|9921|9841|9752|9683|9598|9513|9491|9450|9432|9434|9367|9339|9271|9161|9093|8955|8853|8789|8697|8647|8633|8630|8664|8732|8758|8807|8852|8857|8916|8913|8889|8862|8736|8592|8373|8074|7764|7419|7113|6928|6849|6861|7023|7248|7544|7920|8198|8472|8679|8781|8883|8881|8854|8838|8764|8729|8703|8664|8678|8726|8769|8843|8942|8997|9084|9111|9111|9159|9144|9173|9188|9165|9190|9181|9177|9204|9214|9214|9231|9225|9211|9234|9213|9226|9261|9232|9248|9216|9189|9216|9187|9184|9200|9187|9182|9214|9229|9249|9301|9315|9349|9370|9372|9418|9414|9450|9488|9459|9485|9486|9477|9509|9526|9545|9590|9632|9648|9704|9724|9736|9758|9724|9771|9792|9806|9898|9937|10019|10108|10161|10239|10299|10338|10396|10477|10507|10543|10560|10523|10528|10501|10520|10592|10642|10765|10895|11006|11169|11313|11422|11528|11595|11643|11720|11751|11787|11861|11873|11933|11992|12013|12106|12161|12228|12348|12439|12556|12686|12805|12922|13046|13146|13283|13444|13573|13781|13969|14182|14477|14727|15044|15404|15756|16169|16584|16998|17434|17882|18308|18762|32506";
            string[] vals = val.Split('|');
            double[] data = new double[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                data[i] = double.Parse(vals[i]);
            }

            double[] a = data;
            double[] b = new double[data.Length];

            //滤波
            MyFFT.fft(data.Length, a, b, 1);
            int ij = data.Length / 6;
            int ii = data.Length - ij;
            for (int j = ij; j < ii; j++) a[j] = b[j] = 0;
            MyFFT.fft(data.Length, a, b, -1);

            //平滑处理
            data = wave.DB4DWT(data);
            data = wave.DBFLT(data);
            data = wave.DB4DWT(data);
            data = wave.DBFLT(data);
            double[] td = new double[data.Length * 2];
            Array.Copy(data, 0, td, 0, data.Length);
            data = wave.DB4IDWT(td);
            td = new double[data.Length * 2];
            Array.Copy(data, 0, td, 0, data.Length);
            data = wave.DB4IDWT(td);

            double[] valCT = DyUtils.dyMath(data);
            if (valCT.Length > 0)
            {

            }
        }

        /// <summary>
        /// 如果电池管理系统未启动，则进行启动
        /// </summary>
        private void StartBatteryMsg()
        {
            if (!Global.InstrumentName.Contains("DY-3500(I)"))
            {
                //判定程序路径是否存在
                string path = Environment.CurrentDirectory + "\\BatteryManage.exe";
                if (!File.Exists(path))
                {
                    MessageBox.Show("系统检测到[达元电池管理系统]缺失，可能导致无法查看电池电量等信息。\r\n\r\n可在设置界面进行升级操作或联系管理员。");
                    return;
                }

                //判定程序是否已经运行
                Process[] p = Process.GetProcessesByName("BatteryManage");
                if (p.Length == 0)
                {
                    System.Diagnostics.Process.Start(path);
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
                if (Global.DeviceID.Length == 0)
                    return;

                Wisdom.DEVICESTATUS_REQUEST.deviceid = Global.DeviceID;
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

        private void image_ValidCode_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CreateVaildCode();
        }

        private void UpdateVersion()
        {
            if (MessageBox.Show("温馨提示：\r\n检测到系统新版本,是否现在更新", "版本更新提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                    FileUtils.OprLog(6, wnd.logType, ex.ToString());
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

    }
}