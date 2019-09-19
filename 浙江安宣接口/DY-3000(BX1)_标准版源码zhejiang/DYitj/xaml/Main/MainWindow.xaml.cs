using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AIO.src;
using AIO.xaml.ATP;
using AIO.xaml.Dialog;
using AIO.xaml.Main;
using AIO.xaml.Record;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataModel.Wisdom;

namespace AIO
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserAccount _userconfig = null;
        public string _UpDataED;
        public static string[] _TempItemNames;
        private string logType = "mainWindow-error";
        private DispatcherTimer _DataTimer = null;
        private deviceStatus.Request deviceStatus = new deviceStatus.Request();
        private string errMsg = string.Empty;
        private MsgThread _msgThread;
        public static APlayerForm _aPlayer;

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0.0;
            this.Top = 0.0;
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.labelName.Content = Global.InstrumentNameModel + Global.InstrumentName;

            //广东省智慧云平台，定时上报设备运行状态
            if (Global.InterfaceType.Equals("ZH"))
            {
                if (_DataTimer == null)
                    _DataTimer = new DispatcherTimer();
                _DataTimer.Interval = TimeSpan.FromMinutes(30);
                _DataTimer.Tick += new EventHandler(ZHTimerTick);
                _DataTimer.Start();
            }
            //甘肃项目 定时检测是否有未上传的数据
            else if (Global.InterfaceType.Equals("GS"))
            {
                //自动上传检测数据&自动接收检测任务
                if (_DataTimer == null)
                    _DataTimer = new DispatcherTimer();
                _DataTimer.Interval = TimeSpan.FromSeconds(5);
                _DataTimer.Tick += new EventHandler(UploadAndTaskTimerTick);
                _DataTimer.Start();

                _msgThread = new MsgThread(this);
                _msgThread.Start();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = 5;
                if (!_userconfig.UIFaceOne)
                {
                    //分光度
                    num -= 1;
                    Fgd.Visibility = Visibility.Collapsed;
                    fgdLabel1.Visibility = Visibility.Collapsed;
                    fgdLabel2.Visibility = Visibility.Collapsed;
                    Global.set_IsOpenFgd = false;
                }
                if (!_userconfig.UIFaceTwo || Global.deviceHole.SxtCount == 0)
                {
                    //胶体金
                    num -= 1;
                    Jtj.Visibility = Visibility.Collapsed;
                    jtjLabel1.Visibility = Visibility.Collapsed;
                    jtjLabel2.Visibility = Visibility.Collapsed;
                    Global.set_IsOpenJtj = false;
                }
                if (!_userconfig.UIFaceThree || Global.deviceHole.SxtCount == 0)
                {
                    //干化学
                    num -= 1;
                    Ghx.Visibility = Visibility.Collapsed;
                    ghxLabel1.Visibility = Visibility.Collapsed;
                    ghxLabel2.Visibility = Visibility.Collapsed;
                    Global.set_IsOpenGhx = false;
                }
                if (!_userconfig.UIFaceFour || Global.deviceHole.HmCount == 0)
                {
                    //重金属
                    num -= 1;
                    Zjs.Visibility = Visibility.Collapsed;
                    zjsLabel1.Visibility = Visibility.Collapsed;
                    zjsLabel2.Visibility = Visibility.Collapsed;
                    Global.set_IsOpenZjs = false;
                }
                Lb_WswOrAtp.Content = Global.IsWswOrAtp.Equals("WSW") ? "微 生 物" : "A T P";
                if (!_userconfig.UIFaceFive || !Global.IsEnableWswOrAtp)
                {
                    //微生物OrATP
                    num -= 1;
                    WswOrAtp.Visibility = Visibility.Collapsed;
                    wswLabel1.Visibility = Visibility.Collapsed;
                    wswlLabel2.Visibility = Visibility.Collapsed;
                }
                if (num == 1)
                {
                    WraPanel.Width = 160;
                    WraPanel.Height = 130;
                }
                else if (num == 2) WraPanel.Width = 320;
                else if (num == 3) WraPanel.Width = 480;
                else if (num == 4) WraPanel.Width = 640;
                else if (num == 5) WraPanel.Width = 800;
                else WraPanel.Width = 0;

                //若分辨率低于1024*768 则提示
                if (SystemParameters.WorkArea.Width < 1024)
                {
                    MessageBox.Show("本系统最佳分辨率为1024*768；若低于此分辨率可能部分内容会溢出屏幕!请设置分辨率为1024*768或以上！", "系统提示");
                }
                //this.btnVideo.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI界面时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonFenguangdu_Click(object sender, RoutedEventArgs e)
        {
            Global.videoType = "fgd";
            Global.typeName = "fgd";
            FgdWindow window = new FgdWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void ButtonJiaotijin_Click(object sender, RoutedEventArgs e)
        {
            Global.videoType = "jtj";
            Global.typeName = "jtj";
            JtjWindow window = new JtjWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void ButtonGanhuaxue_Click(object sender, RoutedEventArgs e)
        {
            Global.videoType = "ghx";
            Global.typeName = "gsz";
            GszWindow window = new GszWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void ButtonWeishengwu_Click(object sender, RoutedEventArgs e)
        {
            Global.videoType = "zjs";
            Global.typeName = "zjs";
            HmWindow window = new HmWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsPlayer)
                {
                    _aPlayer.LoadPlayer();
                }
                else
                {
                    _aPlayer = new APlayerForm();
                    _aPlayer.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "播放视频教程时出现异常!\r\n\r\n异常信息：" + ex.Message, "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDataManagement_Click(object sender, RoutedEventArgs e)
        {
            DataManagementWindow window = new DataManagementWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void btnWswOrAtp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsWswOrAtp.Equals("WSW"))
                {
                    string path = @"" + Global.MicrobialAddress;
                    if (Global.IsOpenFile.Equals("Y"))
                    {
                        //打开指定路径的文件
                        System.Diagnostics.Process.Start(path);
                    }
                    else
                    {
                        //打开指定路径的文件夹
                        System.Diagnostics.Process.Start("explorer.exe", path);
                    }
                }
                else
                {
                    if (Global.ATP.FindTheHid())
                    {
                        Global.ATP.getCommunication();
                    }
                    AtpWindow window = new AtpWindow();
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (Global.InterfaceType.Equals("ZH"))
                {
                    deviceStatus.deviceStatus = "0";
                    UploadDeviceStatus();
                    _DataTimer.Stop();
                }
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 定时检测是否有未上传的数据 以及检测任务实时接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadAndTaskTimerTick(object sender, EventArgs e)
        {
            if (Global.IsConnectInternet())
            {
                //自动上传数据
                if (Global.IsStartUploadTimer)
                {
                    DataTable dtbl = SqlHelper.GetDataTable("ttResultSecond", "IsUpload = 'N'", out errMsg);
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        TimerUploadWindow window = new TimerUploadWindow();
                        window.dtbl = dtbl;
                        window.ShowInTaskbar = false;
                        window.Owner = this;
                        window.Show();
                    }
                    else
                    {
                        Global.IsStartUploadTimer = false;
                    }
                }

                //自动接收检测任务
                if (Global.samplenameadapter != null && Global.samplenameadapter.Count > 0)
                {
                    Message msg = new Message()
                    {
                        what = MsgCode.MSG_DownTask,
                        str1 = Global.samplenameadapter[0].ServerAddr,
                        str2 = Global.samplenameadapter[0].RegisterID,
                        str3 = Global.samplenameadapter[0].RegisterPassword
                    };
                    msg.args.Enqueue(Global.samplenameadapter[0].CheckPointID);
                    msg.args.Enqueue(Global.samplenameadapter[0].CheckPointName);
                    msg.args.Enqueue(Global.samplenameadapter[0].CheckPointType);
                    msg.args.Enqueue(Global.samplenameadapter[0].Organization);
                    Global.workThread.SendMessage(msg, _msgThread);
                }

                //任务告警
                DataTable dt = new clsTaskOpr().GetAsDataTable("", "", 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                    if (Items != null && Items.Count > 0)
                    {
                        for (int i = 0; i < Items.Count; i++)
                        {
                            DateTime bjsj = DateTime.Now;
                            //有报警时间
                            if (DateTime.TryParse(Items[i].BAOJINGTIME, out bjsj))
                            {
                                //超时
                                if (bjsj > DateTime.Now)
                                {
                                    if (int.Parse(Items[i].PLANDCOUNT) > Items[i].CompleteNum)
                                    {
                                        Btn_Task.Content = "任务告警";
                                        Btn_Task.Foreground = new SolidColorBrush(Colors.Red);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                //Img_Alarm.Visibility = Visibility.Collapsed;
                                Btn_Task.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 智慧云平台 定时上报设备运行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZHTimerTick(object sender, EventArgs e)
        {
            deviceStatus.deviceStatus = "2";
            UploadDeviceStatus();
        }

        private void UploadDeviceStatus()
        {
            try
            {
                deviceStatus.deviceid = Wisdom.DeviceID;
                deviceStatus.longitude = "";
                deviceStatus.latitude = "";
                Wisdom.DEVICESTATUS_REQUEST = deviceStatus;
                LoginWindow.deviceStatus = (Wisdom.UploadDeviceStatus() ? "设备运行状态上报成功：" : "设备运行状态上报失败：")
                    + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            catch (Exception ex)
            {
                LoginWindow.deviceStatus = "设备运行状态上报时出现异常:" +
                    ex.Message + "\r\n" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        /// <summary>
        /// 检测任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Task_Click(object sender, RoutedEventArgs e)
        {
            TaskDisplay window = new TaskDisplay();
            window.IsUpdataTask = false;
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();

            Lb_SampleidCount.Content = "0";
            Img_SampleidInfo.Visibility = Visibility.Collapsed;
            Lb_SampleidCount.Visibility = Visibility.Collapsed;
        }

        class MsgThread : ChildThread
        {
            MainWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(MainWindow wnd)
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
                    case MsgCode.MSG_DownTask:
                        if (!string.IsNullOrEmpty(msg.DownLoadTask))
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception) { }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 下载检测任务并更新状态
        /// </summary>
        /// <param name="TaskTemp"></param>
        /// <returns></returns>
        private void DownloadTask(string TaskTemp)
        {
            clsTaskOpr _bll = new clsTaskOpr();
            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            DataSet dataSet = new DataSet();
            DataTable dtbl = new DataTable();
            using (StringReader sr = new StringReader(TaskTemp))
                dataSet.ReadXml(sr);
            int len = 0, taskNum = 0;
            if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            {
                if (dataSet != null)
                {
                    len = dataSet.Tables[0].Rows.Count;
                    dtbl = dataSet.Tables[0];
                }

                sb.Append(delErr);
                if (len == 0) return;

                //检测任务需要实时更新，每次下载时先清空数据库
                //new clsCompanyOpr().Delete(string.Empty, out delErr, "tTask");

                clsTask model = new clsTask();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    try
                    {
                        model.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                        model.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                        model.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                        model.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                        model.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                        model.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                        model.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                        model.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                        model.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                        model.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                        model.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                        model.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                        model.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                        model.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
                        model.UDate = dtbl.Rows[i]["UDATE"].ToString();
                        _bll.InsertOrUpdate(model, out err);
                        if (err.Length == 0)
                        {
                            taskNum++;
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                //显示提示信息
                if (taskNum > 0)
                {
                    Img_SampleidInfo.Visibility = Visibility.Visible;
                    Lb_SampleidCount.Visibility = Visibility.Visible;
                    Lb_SampleidCount.Content = taskNum.ToString();
                }
            }
        }

    }
}