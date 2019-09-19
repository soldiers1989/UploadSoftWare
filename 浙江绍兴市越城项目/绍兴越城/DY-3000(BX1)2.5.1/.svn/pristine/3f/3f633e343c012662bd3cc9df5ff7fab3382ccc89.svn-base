using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;

namespace BatteryManage
{
    public partial class BatteryManage : Form
    {
        private Message msg = new Message();
        private GetBatteryInfo getBatteryInfoThread;

        /// <summary>
        /// 显示坐标
        /// </summary>
        private int x, y;
        /// <summary>
        /// 获取电池信息任务
        /// </summary>
        private System.Timers.Timer getBatteryTimer = new System.Timers.Timer(1000);
        /// <summary>
        /// 是否运行获取电池任务
        /// </summary>
        private bool isStartGetTask = true;
        /// <summary>
        /// 是否正在执行分光任务
        /// </summary>
        private bool isStartFgTask = true;
        /// <summary>
        /// 是否已经显示窗口
        /// </summary>
        private bool isShowWindow = false;
        /// <summary>
        /// 是否正在充电
        /// </summary>
        private bool isCharge = true;
        /// <summary>
        /// 是否已经执行了关机任务
        /// </summary>
        private bool isStartShutdown = false;
        /// <summary>
        /// 图标文件路径
        /// </summary>
        private string iconPath = Application.StartupPath;
        /// <summary>
        /// 是否标红显示，标红显示时表示出现了异常
        /// </summary>
        private bool isRed = false;
        /// <summary>
        /// 低电量弹窗是否已经显示
        /// </summary>
        private bool isShow = false;
        /// <summary>
        /// 当前电量
        /// </summary>
        private int batteryNum;
        /// <summary>
        /// 电量最低值
        /// </summary>
        private int batteryMin = 10;
        /// <summary>
        /// 是否显示电池电量
        /// </summary>
        private bool isShowNum = Global.GetConfig("IsShowBatteryNum", "0").Equals("1") ? true : false;
        private ContextMenu notifyiconMnu;

        public BatteryManage()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //点击窗体外隐藏电量显示
            Deactivate += BatteryManage_Deactivate;

            Global.workThread = new WorkThread();
            Global.workThread.Start();

            getBatteryInfoThread = new GetBatteryInfo(this);
            getBatteryInfoThread.Start();

            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem
            {
                Text = "显示电量详情"
            };
            mnuItms[0].Click += new System.EventHandler(this.NotifyIcon_showfrom);
            mnuItms[1] = new MenuItem("-");
            mnuItms[2] = new MenuItem
            {
                Text = "退出"
            };
            mnuItms[2].Click += new System.EventHandler(this.NotifyIcon_ExitSelect);
            mnuItms[2].DefaultItem = true;
            notifyiconMnu = new ContextMenu(mnuItms);
            notifyIcon.ContextMenu = notifyiconMnu;

        }
        /// <summary>
        /// windows串口打开信息通信
        /// </summary>
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }



        private void NotifyIcon_showfrom(object sender, EventArgs e)
        {
            isShowWindow = false;
            ShowWindow();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_ExitSelect(object sender, EventArgs e)
        {
            try
            {
                Global._port.Close();
                getBatteryTimer.Stop();
                Global.workThread.Stop();
                getBatteryInfoThread.Stop();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally 
            {
                notifyIcon.Dispose();
                Environment.Exit(Environment.ExitCode); 
            } 
        }

        private void BatteryManage_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                //设置开机自启动    
                string path = Application.ExecutablePath;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("SKR", path);
                rk2.Close();
                rk.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置开机启动时出现异常，建议使用管理员身份运行。"+ex.Message,"设置启动" );
            }

            Location = new Point(x, y);
            isShowWindow = true;
            getBatteryTimer.Elapsed += new System.Timers.ElapsedEventHandler(timerTask);
            getBatteryTimer.AutoReset = true;
            getBatteryTimer.Enabled = true;
            //FileUtils.Log("电池管理系统启动成功");
            //FileUtils.Log("---------------------------------------------");
            //FileUtils.Log(string.Format("Environment.CurrentDirectory：{0}", Environment.CurrentDirectory));
            //FileUtils.Log(string.Format("Application.ExecutablePath：{0}", Application.ExecutablePath));
            //FileUtils.Log(string.Format("Application.StartupPath：{0}", Application.StartupPath));
            //FileUtils.Log(string.Format("System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName：{0}", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            //FileUtils.Log(string.Format("System.Environment.CurrentDirectory：{0}", System.Environment.CurrentDirectory));
            //FileUtils.Log(string.Format("System.AppDomain.CurrentDomain.BaseDirectory：{0}", System.AppDomain.CurrentDomain.BaseDirectory));
            //FileUtils.Log(string.Format("System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase：{0}", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase));
            //FileUtils.Log(string.Format("System.Windows.Forms.Application.StartupPath：{0}", System.Windows.Forms.Application.StartupPath));
            //FileUtils.Log(string.Format("System.Windows.Forms.Application.ExecutablePath：{0}", System.Windows.Forms.Application.ExecutablePath));
            //FileUtils.Log(string.Format("System.IO.Directory.GetCurrentDirectory();：{0}", System.IO.Directory.GetCurrentDirectory()));
            //FileUtils.Log("---------------------------------------------");
        }

        private void timerTask(object sender, System.Timers.ElapsedEventArgs e)
        {
            //每次任务进来默认分光模块未运行
            isStartFgTask = false;
            //1 分光模块正在运行，0分光模块未运行
            string fgType = Global.GetConfig("FgIsStart", "0");
            //FileUtils.Log(string.Format("isStart:{0}", fgType));
            //如果BX1分析仪软件已经启动，则需要获取分光模块是否在运行
            //Process[] p = Process.GetProcessesByName("DY-Detector");
            //if (p.Length > 0) isStartFgTask = fgType.Equals("1") ? true : false;

            if (fgType.Equals("1"))
            {
                isStartFgTask = true;
            }
            FileUtils.Log(string.Format("是否发送数据的状态isStartGetTask：{0},isStartFgTask:{1}", isStartGetTask, isStartFgTask));
            //当分光模块为运行，且可运行获取电池任务时
            if (!isStartFgTask)//isStartGetTask && !isStartFgTask 
            {
                string port = Global.GetConfig("ADPORT", "COM3");
                if (!Global._port.IsOpen())
                {
                    Global._port.OpenBattery(port);
                }
                //FileUtils.Log(port);
                FileUtils.Log("----------------------------------------发送命令----------------------------------------");
                //System.Console.WriteLine("");
                //System.Console.WriteLine("----------------------------------------发送命令----------------------------------------");
                msg.what = MsgCode.MSG_GET_BATTERY;
                msg.str1 = port;
                Global.workThread.SendMessage(msg, getBatteryInfoThread);
                isStartGetTask = false;
                Thread.Sleep(5);
            }
            else
            {
                if (Global._port.IsOpen())//如果串口打开就关闭
                    Global._port.Close();
                    
                Thread.Sleep(5);
            }
           
        }

        /// <summary>
        /// 点击窗体外的区域隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatteryManage_Deactivate(object sender, EventArgs e)
        {
            x = Screen.PrimaryScreen.WorkingArea.Width + this.Width;
            y = Screen.PrimaryScreen.WorkingArea.Height + this.Height;
            Location = new Point(x, y);
            isShowWindow = false;
        }

        private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            ShowWindow();
        }

        private void ShowWindow() 
        {
            if (isShowWindow)
            {
                //显示
                x = Screen.PrimaryScreen.WorkingArea.Width + this.Width;
                y = Screen.PrimaryScreen.WorkingArea.Height + this.Height;
                Location = new Point(x, y);
                isShowWindow = false;
                this.Activate();
            }
            else
            {
                //隐藏
                x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
                Location = new Point(x, y);
                isShowWindow = true;
            }
        }

        private void SettingIcon(int batteryNum)
        {
            //如果在执行分光任务，则不改变当前图标
            if (isStartFgTask) return;

            if (batteryNum == 0XFF)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\batteryError.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.batteryError;
            }
            else if (batteryNum > 90)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery5.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery5;
            }
            else if (batteryNum >= 80)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery4.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery4;
            }
            else if (batteryNum >= 60)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery3.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery3;
            }
            else if (batteryNum >= 40)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery2.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery2;
            }
            else if (batteryNum >= 20)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery1.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery1;
                if (isCharge) notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery.ico");
            }
            else if (batteryNum >= 5)
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery0.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.battery0;
                if (isCharge) notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\battery.ico");
            }
            else
            {
                notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\batteryError.ico");
                panel_ElectricQuantity.BackgroundImage = Properties.Resources.batteryError;
            }

            //如果已经执行了关机任务，但在充电或者电池电量已经大于batteryMin 则取消关机
            if (isStartShutdown && (batteryNum > batteryMin || isCharge))
            {
                Process.Start("shutdown.exe", "-a");//取消定时关机
                isStartShutdown = false;
                return;
            }

            //如果当前电量大于电量最低值或者正在充电，或在已经执行了关机任务
            if (batteryNum > batteryMin || isCharge || isStartShutdown) return;

            isStartShutdown = true;
            Process.Start("shutdown.exe", "-s -t " + 180);//定时关机
            MessageBox.Show("检测到电量过低，请立即充电！\r\n\r\nTips1：已为您设置3分钟后自动关机任务！\r\nTips2：接上电源可取消关机任务。", "电量过低", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void SettingBatteryInformation(List<byte[]> datas)
        {
            if (isStartFgTask) return;
            if (datas == null) return;
            if (datas.Count < 2) return;
            if (datas[1] == null) return;


            lb_BatteryContent.Visible = isShowNum;
            string info = string.Empty;
            batteryNum = 0XFF;
            FileUtils.Log("--------------------解析命令--------------------");
            //System.Console.WriteLine("--------------------解析命令--------------------");
            try
            {
                byte[] data;
                if (datas != null)
                {
                    string dtstring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //记录电池原始数据
                    if (datas.Count > 2 && datas[2] != null)
                    {
                        FileUtils.LogType1(string.Format("{0}：{1}", dtstring, datas[2][4]));
                        FileUtils.LogType2(string.Format("{0}：{1}", dtstring, (datas[2][5] + (datas[2][6] * 256))));
                    }
                    //电池电量
                    data = datas[1];
                    if (data != null)
                    {
                        FileUtils.Log(string.Format("原始电量数值：{0}", data[4]));
                        batteryNum = data[4] - 50 <= 0 ? 1 : (data[4] - 50) * 2;
                        FileUtils.Log(string.Format("(原始电量数值-50)*2：{0}", batteryNum));
                        lb_BatteryContent.Text = batteryNum + "%";
                        //根据电量来显示不同的图片，并判断是否需要提升关机
                        SettingIcon(batteryNum);
                        if (!isShow && batteryNum <= 10)
                        {
                            getBatteryTimer.Stop();
                            isShow = true;
                            InfoShowForm window = new InfoShowForm
                            {
                                Owner = this
                            };
                            window.ShowDialog();
                            getBatteryTimer.Start();
                        }
                        if (batteryNum > 20) isShow = false;
                        System.Console.WriteLine(string.Format("电量：{0}", lb_BatteryContent.Text));
                        FileUtils.Log(string.Format("电量：{0}", lb_BatteryContent.Text));
                    }
                    else
                    {
                        isRed = true;
                        FileUtils.Log("电量：N/A");
                        System.Console.WriteLine("电量：N/A");
                    }

                    data = datas[0];
                    //电池状态
                    if (data != null)
                    {
                        //没有电池 0x00
                        if (data[4] == 0x00)
                        {
                            isRed = true;
                            info = "异常：没有电池";
                        }
                        //已充满电 0x01
                        else if (data[4] == 0x01)
                        {
                            isRed = false;
                            isCharge = true;
                            info = "电量已充满";
                        }
                        //正在充电 0x02
                        else if (data[4] == 0x02)
                        {
                            isRed = false;
                            isCharge = true;
                            info = "正在充电中···";
                            //为避免误会，如果电量已经是100且状态为正在充电，则电量显示为99
                            if (batteryNum >= 99) lb_BatteryContent.Text = "99%";
                        }
                        //放电 0x03
                        else if (data[4] == 0x03)
                        {
                            isRed = false;
                            isCharge = false;
                            info = "电源已断开";
                        }
                        //温度异常 0x04
                        else if (data[4] == 0x04)
                        {
                            isRed = true;
                            info = "电池温度异常";
                        }
                        //其他故障 0x05
                        else if (data[4] == 0x05)
                        {
                            isRed = true;
                            info = "电池状态异常";
                        }
                        System.Console.WriteLine(string.Format("电池状态：{0}", info));
                        FileUtils.Log(string.Format("电池状态：{0}", info));
                    }
                    else
                    {
                        isRed = true;
                        System.Console.WriteLine("电池状态获取失败");
                        FileUtils.Log("电池状态获取失败");
                    }
                }
                else
                {
                    isRed = true;
                    info = "电池信息获取失败";
                    System.Console.WriteLine(info);
                    FileUtils.Log(info);
                }
            }
            catch (Exception ex)
            {
                isRed = true;
                byte[] bt = datas[1];
                string dds = "";
                for (int i = 0; i < bt.Length; i++)
                {
                    dds = dds + bt[i].ToString("X2");
                }
                info = "电池管理系统异常：" + ex.Message+" 数据："+dds;
                System.Console.WriteLine(info);
                FileUtils.Log(info);
            }
            finally
            {
                lb_BatteryType.Text = info;
                notifyIcon.Text = isShowNum ? string.Format("{0}({1})", info, lb_BatteryContent.Text) : info;
                //如果有异常，电池标红色图标
                lb_BatteryType.ForeColor = isRed ? System.Drawing.Color.Red : System.Drawing.Color.White;
                if (isRed)
                {
                    isCharge = false;
                    lb_BatteryContent.Text = "N/A";
                    notifyIcon.Icon = new Icon(iconPath + "\\BatteryIcon\\batteryError.ico");
                    panel_ElectricQuantity.BackgroundImage = Properties.Resources.batteryError;
                }
                isStartGetTask = true;
                panel_Charging.Visible = isCharge;
            }
        }

        private class GetBatteryInfo : ChildThread
        {
            BatteryManage wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public GetBatteryInfo(BatteryManage wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }
            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);

                switch (msg.what)
                {
                    case MsgCode.MSG_GET_BATTERY:
                        wnd.SettingBatteryInformation(msg.batteryData);
                        break;
                    default:
                        break;
                }
            }

            private void UIHandleMessage(Message msg) { }

        }

    }
}