
using GPS.GPSModel;
using GPS.model;
using GPS.window;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GPS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon;
        private bool isShowWindow = false;
        private string path = Environment.CurrentDirectory;
        private Thread thread = null;
        private bool isStart = false;
       
        /// <summary>
        /// 显示坐标
        /// </summary>
        private double xPoint, yPoint;
        public MainWindow()
        {
            InitializeComponent();
            notifyIco();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Global.getFilePath();
            //设置窗体的显示位置
            xPoint = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            yPoint = SystemParameters.WorkArea.Size.Height - this.Height;
            this.Top = yPoint;
            this.Left = xPoint;

            try
            {
                AddressInfo address = AddressInfo.GetAddressByBaiduAPI();
                if (address!=null )
                {
                    labelProvince.Content = address.content.address_detail.province;
                    labelCity.Content = address.content.address_detail.city ;
                }

                if(UpHeat.UpHeartBeat("0"))//开机
                {
                    imageOnline.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image\\sgnalfour.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    imageOnline.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image\\nosignal.png", UriKind.RelativeOrAbsolute));
                }
                
                //设置开机自启动
                string path =System.Windows.Forms .Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("DYGPS", path);
                rk2.Close();
                rk.Close();

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("设置开机启动时出现异常，建议使用管理员身份运行。" + ex.Message, "设置启动");
            }

            //使用委托隐藏窗体
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Hide();
                //this.Opacity = 1;
            }));

            isStart = true;

            thread = new Thread(UpdataGPSInfo);
            thread.IsBackground=true ;
            thread.Start ();
            
        }
        /// <summary>
        /// 后台线程更新心跳包状态
        /// </summary>
        private void UpdataGPSInfo()
        {
            try
            {
                while (isStart)
                {
                    //Thread.Sleep(1000 * 10);//10秒钟更新
                    Thread.Sleep(1000 *60* Global.times);//10分钟更新
                    if (isStart)
                    {
                        if(UpHeat.UpHeartBeat("2"))//开机
                        {
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                 imageOnline.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image\\sgnalfour.png", UriKind.RelativeOrAbsolute));
                            }));
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                imageOnline.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image\\nosignal.png", UriKind.RelativeOrAbsolute));
                            }));
                        }
                    }
                        
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void notifyIco()
        {
            try
            {
                //设置托盘的各个属性
                notifyIcon = new System.Windows.Forms.NotifyIcon();
                notifyIcon.BalloonTipText = "定位系统开始运行......";
                notifyIcon.Text = "达元定位系统";
                notifyIcon.Icon = new System.Drawing.Icon(AppDomain.CurrentDomain.BaseDirectory + "image\\local.ico");
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(2000);
                notifyIcon.MouseClick += notifyIcon_MouseClick;

                //设置菜单项
                //System.Windows.Forms.MenuItem menu1 = new System.Windows.Forms.MenuItem("菜单项1");
                //System.Windows.Forms.MenuItem menu2 = new System.Windows.Forms.MenuItem("菜单项2");
                //System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem("菜单", new System.Windows.Forms.MenuItem[] { menu1, menu2 });
                System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem("详细地址信息");
                menu.Click += menu_Click;
                //退出菜单项
                System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
                exit.Click += exit_Click;

                //关联托盘控件
                System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit };
                notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

                //窗体状态改变时候触发
                this.StateChanged += MainWindow_StateChanged;
            }
            catch (Exception ex)
            {

            }
        }

        private void menu_Click(object sender, EventArgs e)
        {
            try
            {
                //Console.WriteLine("打开");
                //使用  次API报错用不了
                //DY_GPS.model.GaoJingDuGPS.DetailAddress detail = DY_GPS.model.GaoJingDuGPS.GetDetailAddressByBaiduAPI("192.168.19.241");
                //查看街道
                //System.Console.WriteLine(detail.Content.Location.Lat);

                //只能获取到城市

                AddressInfo address = AddressInfo.GetAddressByBaiduAPI();
                if (address != null)
                {
                    string[] htmldata = System.IO.File.ReadAllLines(path + "\\report\\GPSModel.html");
                    string html = string.Empty;
                    foreach (string s in htmldata)
                    {
                        html = html + s + "\r\n";
                    }

                    string Xpattern = @"Xdat";
                    string Ypattern = @"Ydat";
                    string x = address.content.point.x; //"113.459024";//;
                    string y = address.content.point.y;// "23.104707"; //
                    html = Regex.Replace(html, Xpattern, x);
                    html = Regex.Replace(html, Ypattern, y);

                    SaveFile(html);
                    System.Diagnostics.Process.Start(path + "\\report\\GPSModel.html");
                    //webBrowser1.Navigate(path + "\\report\\GPSModel.html");
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private void SaveFile(string str)
        {
            if (!(Directory.Exists(path + "\\report")))
            {
                Directory.CreateDirectory(path + "\\report");
            }
            string filePath = path + "\\report\\GPSModel.html";
            //如果存在则删除
            //if (File.Exists(filePath)) System.IO.File.Delete(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
        private  void MainWindow_StateChanged(object sender, EventArgs e)
        {
            
        }

        private void exit_Click(object sender, EventArgs e)
        {
            try
            {
                isStart = false;
                thread.Abort();
                thread = null;
                UpHeat.UpHeartBeat("1");//关机
                this.Close();
                notifyIcon.Dispose();
            }
            catch (Exception ex)
            {

            }
            
           
        }

        private  void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (isShowWindow == false)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            this.Visibility = Visibility.Visible;
                            this.Activate();
                        }
                        catch (Exception)
                        { }
                        //this.Opacity = 1;
                    }));
                    isShowWindow = true;
                }
                else if (isShowWindow)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            this.Hide();
                        }
                        catch (Exception)
                        { }
                        //this.Opacity = 1;
                    }));
                    isShowWindow = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Hide();
                //this.Opacity = 1;
            }));
           
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try 
            {
                isStart = false;
                thread.Abort();
                thread = null;
                UpHeat.UpHeartBeat("1");//关机
                this.Close();
                notifyIcon.Dispose();
            }
           catch (Exception ex)
            { }
        }

        private void Label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.Red);
            //labelclose.Background = new SolidColorBrush(Colors.Violet  );
        }

        private void labelclose_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.White);
            //labelclose.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void labelDetail_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((System.Windows.Controls.Label)sender).Foreground = new SolidColorBrush(Colors.Red);
            //labelDetail.Foreground = new SolidColorBrush(Colors.Red );
        }

        private void labelDetail_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((System.Windows.Controls.Label)sender).Foreground = new SolidColorBrush(Colors.BlueViolet);
            //labelDetail.Foreground = new SolidColorBrush(Colors.BlueViolet );
        }

        private void labelDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                //只能获取到城市
                AddressInfo address = AddressInfo.GetAddressByBaiduAPI();
                if (address != null)
                {
                    string[] htmldata = System.IO.File.ReadAllLines(path + "\\report\\html.html");
                    string html = string.Empty;
                    foreach (string s in htmldata)
                    {
                        html = html + s + "\r\n";
                    }

                    string Xpattern = @"Xdat";
                    string Ypattern = @"Ydat";
                    string x = address.content.point.x; //"113.459024";//;
                    string y = address.content.point.y;// "23.104707"; //
                    if (Global.lat != "" && Global.lon != "")
                    {
                        x = Global.lat;
                        y = Global.lon;
                    }
                    html = Regex.Replace(html, Xpattern, x);
                    html = Regex.Replace(html, Ypattern, y);

                    SaveFile(html);
                    System.Diagnostics.Process.Start(path + "\\report\\GPSModel.html");
                    //webBrowser1.Navigate(path + "\\report\\GPSModel.html");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void labelSet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                InputWindow Inputwindow = new InputWindow();
                Inputwindow.ShowDialog();
                if (Inputwindow.IsPassWord)
                {
                    SystemInfoWindow window = new SystemInfoWindow();
                    window.ShowDialog();
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            this.Visibility = Visibility.Visible;
                            this.Activate();
                        }
                        catch (Exception)
                        { }
                        //this.Opacity = 1;
                    }));
                    isShowWindow = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void labelHide_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    this.Hide();
                }
                catch (Exception)
                { }
                //this.Opacity = 1;
            }));
            isShowWindow = false;
        }
    }
}
