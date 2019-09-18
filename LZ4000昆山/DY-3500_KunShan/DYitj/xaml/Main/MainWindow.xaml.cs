using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Threading;
using AIO.src;
using AIO.xaml.ATP;
using AIO.xaml.Dialog;
using AIO.xaml.Main;
using AIO.xaml.Print;
using DYSeriesDataSet;
using Xfrog.Net;

namespace AIO
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        #region 全局变量

        private DispatcherTimer _DataTimer = null;
        private deviceStatus.Request deviceStatus = new deviceStatus.Request();
        public UserAccount _userconfig = null;
        public string _UpDataED;
        public static string[] _TempItemNames;
        public static APlayerForm _aPlayer;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Normal; this.WindowStyle = System.Windows.WindowStyle.None; this.ResizeMode = System.Windows.ResizeMode.NoResize; this.Left = 0.0; this.Top = 0.0; this.Width = System.Windows.SystemParameters.PrimaryScreenWidth; this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
        }
        #region
        //public  void PerformClick(ButtonBase button)
        //{
        //    var method = button.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (method != null)
        //    {
        //        method.Invoke(button, new object[] { null });
        //    }
        //    button.Focus();
        //}
        // void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    PerformClick(button);
        //}

        #endregion

        private void ButtonFenguangdu_Click(object sender, RoutedEventArgs e)
        {
            Global.vals = new string[2];
            Global.videoType = "fgd";
            Global.typeName = "fgd";
            FgdWindow window = new FgdWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonJiaotijin_Click(object sender, RoutedEventArgs e)
        {
            Global.vals = new string[2];
            Global.videoType = "jtj";
            Global.typeName = "jtj";
            JtjWindow window = new JtjWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonGanhuaxue_Click(object sender, RoutedEventArgs e)
        {
            Global.vals = new string[2];
            Global.videoType = "ghx";
            Global.typeName = "gsz";
            GszWindow window = new GszWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonWeishengwu_Click(object sender, RoutedEventArgs e)
        {
            Global.vals = new string[2];
            Global.videoType = "zjs";
            Global.typeName = "zjs";
            HmWindow window = new HmWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要注销系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.InterfaceType.Equals("ZH"))
            {
                if (_DataTimer == null)
                    _DataTimer = new DispatcherTimer();
                _DataTimer.Interval = TimeSpan.FromMinutes(30);
                _DataTimer.Tick += new EventHandler(TimerTick);
                _DataTimer.Start();
            }

            labelName.Content = Global.InstrumentNameModel + Global.InstrumentName;
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
            //微生物
            Lb_WswOrAtp.Content = Global.IsWswOrAtp.Equals("WSW") ? "微 生 物" : "A T P";
            if (!_userconfig.UIFaceFive || !Global.IsEnableWswOrAtp)
            {
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
            else if (num == 2)
                WraPanel.Width = 320;
            else if (num == 3)
                WraPanel.Width = 480;
            else if (num == 4)
                WraPanel.Width = 640;
            else if (num == 5)
                WraPanel.Width = 800;
            else
                WraPanel.Width = 0;
            //若分辨率低于1024*768 则提示
            if (SystemParameters.WorkArea.Width < 1024)
            {
                MessageBox.Show(this, "本系统最佳分辨率为1024*768，若低于此分辨率可能部分内容会溢出屏幕！\r\n请设置分辨率为1024*768或以上分辨率，以获取更好的视觉体验！", "系统提示",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.btnVideo.Visibility = Global.IsEnableVideo ? Visibility.Visible : Visibility.Collapsed;
            
            //如果还未进行服务器通讯测试，则提示需要先进行通讯测试。
            if (Global.InterfaceType.Equals("DY"))
            {
                if (Global.samplenameadapter == null || Global.samplenameadapter.Count == 0 ||
                    Global.samplenameadapter[0].pointName.Length == 0)
                {
                    Global.IsServerTest = true;
                    if (MessageBox.Show("检测到当前系统还未进行服务器通讯测试！\r\n\r\n为保证数据完整性，是否立即进行通讯测试?", "操作提示",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow()
                        {
                            ShowInTaskbar = false,
                            Owner = this
                        };
                        window.ShowDialog();
                    }
                }
                else
                {
                    Global.IsServerTest = false;
                }
            }
        }

        /// <summary>
        /// 每半小时上传一次设备运行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
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

        private void Btn_TaskShow_Click(object sender, RoutedEventArgs e)
        {
            WarnTaskShow show = new WarnTaskShow();
            show.Show();
        }

        private void BtnTask_Click(object sender, RoutedEventArgs e)
        {
            TaskDisplay dis = new TaskDisplay()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            dis.Show();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            window.ShowInTaskbar = false; window.Owner = this; window.Show();
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// 样品管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSampleManagement_Click(object sender, RoutedEventArgs e)
        {
            ManagementSample addSample = new ManagementSample()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            addSample.Show();
        }

        private void ComboBoxMethod_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!ComboBoxMethod.SelectedValue.ToString().Equals("系统管理"))
            {
                if (ComboBoxMethod.SelectedValue.ToString().Equals("样品管理"))
                {
                    ManagementSample managementSample = new ManagementSample()
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    managementSample.Show();
                    ComboBoxMethod.SelectedIndex = 0;
                }
                else if (ComboBoxMethod.SelectedValue.ToString().Equals("被检单位管理"))
                {
                    ManagementCompany managementCompany = new ManagementCompany()
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    managementCompany.Show();
                    ComboBoxMethod.SelectedIndex = 0;
                }
            }
        }

        private void BtnDataManagement_Click(object sender, RoutedEventArgs e)
        {
            DataManagementWindow window = new DataManagementWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void BtnWswOrAtp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsWswOrAtp.Equals("WSW"))
                {
                    string path = string.Format(@"{0}", Global.MicrobialAddress);
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("微生物软件路径配置不正确，请重新配置！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (Global.ATP.FindTheHid())
                    {
                        Global.ATP.GetCommunication();
                    }
                    AtpWindow window = new AtpWindow()
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //if (Global.InterfaceType.Equals("ZH"))
                //{
                //    deviceStatus.deviceStatus = "0";
                //    UploadDeviceStatus();
                //    _DataTimer.Stop();
                //}
                if (Global.KsTokenNo.Length > 0)
                {
                    string errMsg = string.Empty;
                    KunShanHelper.LogoutToken(out errMsg);
                }
                Environment.Exit(0);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            GetLocationByBaiDuAPI();
        }

        #region 通过百度api获取当前位置信息


        private void GetLocationByBaiDuAPI()
        {
            //通过IP定位当前大概位置
            string ipAddress = "14.146.24.115";
            AddressForQueryIPFromBaidu addressData = GetAddressFromIP(ipAddress);
            if (addressData != null)
            {

            }
        }

        public static AddressForQueryIPFromBaidu GetAddressFromIP(string ipAddress)
        {
            string baiduKey = "71uvZYtsYej6XlhkpA7BmQ50owd59pz1";
            string url = "http://api.map.baidu.com/location/ip?ak=" + baiduKey + "&ip=" + ipAddress + "&coor=bd09ll";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string responseText = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            responseStream.Close();
            string jsonData = responseText;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            AddressForQueryIPFromBaidu addressForQueryIPFromBaidu = jss.Deserialize<AddressForQueryIPFromBaidu>(jsonData);
            return addressForQueryIPFromBaidu;
        }


        [Serializable]
        public class AddressForQueryIPFromBaidu
        {
            public string Address { get; set; }
            public Content Content { get; set; }
            public string Status { get; set; }
        }
        [Serializable]
        public class Address_Detail
        {
            public string City { get; set; }
            public string City_Code { get; set; }
            public string District { get; set; }
            public string Province { get; set; }
            public string Street { get; set; }
            public string Street_Number { get; set; }
        }
        [Serializable]
        public class Point
        {
            public string X { get; set; }
            public string Y { get; set; }
        }

        #endregion

        #region 聚合数据定位
        private void GetLocationByJuHe()
        {
            string appkey = "2dcccabcb3c7ed93f0d1de8d753a0922"; //配置您申请的appkey


            //1.电信基站定位
            string url1 = "http://v.juhe.cn/cdma/";

            var parameters1 = new Dictionary<string, string>
            {
                { "sid", "13828" }, //SID系统识别码（各地区一个）
                { "nid", "1" }, //NID网络识别码（各地区1-3个）
                { "cellid", "" }, //基站号(bid)
                { "hex", "10" }, //进制类型，16或10，默认：10
                { "dtype", "json" }, //返回的数据格式：json/xml/jsonp
                { "callback", "" }, //当选择jsonp格式时必须传递
                { "key", appkey }//你申请的key
            };
            string result1 = SendPost(url1, parameters1, "get");

            JsonObject newObj1 = new JsonObject(result1);
            String errorCode1 = newObj1["error_code"].Value;

            if (errorCode1 == "0")
            {
                Debug.WriteLine("成功");
                Debug.WriteLine(newObj1);
            }
            else
            {
                //Debug.WriteLine("失败");
                Debug.WriteLine(newObj1["error_code"].Value + ":" + newObj1["reason"].Value);
            }
        }

        /// <summary>
        /// Http (GET/POST)
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="method">请求方法</param>
        /// <returns>响应内容</returns>
        static string SendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        #endregion

        #region 生成唯一机器码
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < 10000000; i++)
            {
                str = GetUniqueString();
                if (!dict.ContainsKey(str))
                {
                    dict.Add(str, str);
                }
            }
            if (str.Equals(""))
            {

            }
        }
        static Random r = new Random();
        public static string GetUniqueString()
        {
            //24位唯一编码
            //string str = CRC32Helper.GetCRC32(Guid.NewGuid().ToString()) + CRC32Helper.GetCRC32(DateTime.Now.ToString()) + CRC32Helper.GetCRC32(r.Next(99999999).ToString());
            //if (str.Length < 24)  //包含长度小于24位的结果，调整到24位。
            //    str = GetUniqueString().PadRight(24, str[0]);
            //16位唯一编码
            string str = CRC32Helper.GetCRC32(Guid.NewGuid().ToString()) + CRC32Helper.GetCRC32((DateTime.Now.Ticks - r.Next(99999999)).ToString());
            if (str.Length < 16)  //包含长度小于16位的结果，调整到16位。
                str = GetUniqueString().PadRight(16, str[0]);
            return str;
        }

        class CRC32Helper
        {
            private static ICSharpCode.SharpZipLib.Checksums.Crc32 crc = new ICSharpCode.SharpZipLib.Checksums.Crc32();
            public static string GetCRC32(string str)
            {
                crc.Reset();
                crc.Update(System.Text.Encoding.UTF8.GetBytes(str));
                return crc.Value.ToString("X");
            }
        }

        public sealed class Crc32
        {
            const uint CrcSeed = 0xFFFFFFFF;

            readonly static uint[] CrcTable = new uint[] {
		        0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
		        0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
		        0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
		        0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
		        0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856,
		        0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
		        0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
		        0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
		        0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3,
		        0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC, 0x51DE003A,
		        0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599,
		        0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
		        0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190,
		        0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
		        0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E,
		        0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
		        0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED,
		        0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
		        0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3,
		        0xFBD44C65, 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
		        0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
		        0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5,
		        0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA, 0xBE0B1010,
		        0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
		        0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17,
		        0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6,
		        0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615,
		        0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
		        0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344,
		        0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
		        0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A,
		        0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
		        0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1,
		        0xA6BC5767, 0x3FB506DD, 0x48B2364B, 0xD80D2BDA, 0xAF0A1B4C,
		        0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
		        0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
		        0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE,
		        0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31,
		        0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C,
		        0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
		        0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B,
		        0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
		        0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1,
		        0x18B74777, 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
		        0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45, 0xA00AE278,
		        0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7,
		        0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66,
		        0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
		        0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
		        0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8,
		        0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B,
		        0x2D02EF8D
	        };

            internal static uint ComputeCrc32(uint oldCrc, byte value)
            {
                return (uint)(Crc32.CrcTable[(oldCrc ^ value) & 0xFF] ^ (oldCrc >> 8));
            }

            /// <summary>
            /// The crc data checksum so far.
            /// </summary>
            uint crc;

            /// <summary>
            /// Returns the CRC32 data checksum computed so far.
            /// </summary>
            public long Value
            {
                get
                {
                    return (long)crc;
                }
                set
                {
                    crc = (uint)value;
                }
            }

            /// <summary>
            /// Resets the CRC32 data checksum as if no update was ever called.
            /// </summary>
            public void Reset()
            {
                crc = 0;
            }

            /// <summary>
            /// Updates the checksum with the int bval.
            /// </summary>
            /// <param name = "value">
            /// the byte is taken as the lower 8 bits of value
            /// </param>
            public void Update(int value)
            {
                crc ^= CrcSeed;
                crc = CrcTable[(crc ^ value) & 0xFF] ^ (crc >> 8);
                crc ^= CrcSeed;
            }

            /// <summary>
            /// Updates the checksum with the bytes taken from the array.
            /// </summary>
            /// <param name="buffer">
            /// buffer an array of bytes
            /// </param>
            public void Update(byte[] buffer)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer");
                }

                Update(buffer, 0, buffer.Length);
            }

            /// <summary>
            /// Adds the byte array to the data checksum.
            /// </summary>
            /// <param name = "buffer">
            /// The buffer which contains the data
            /// </param>
            /// <param name = "offset">
            /// The offset in the buffer where the data starts
            /// </param>
            /// <param name = "count">
            /// The number of data bytes to update the CRC with.
            /// </param>
            public void Update(byte[] buffer, int offset, int count)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer");
                }

                if (count < 0)
                {
#if NETCF_1_0
			throw new ArgumentOutOfRangeException("count");
#else
                    throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero");
#endif
                }

                if (offset < 0 || offset + count > buffer.Length)
                {
                    throw new ArgumentOutOfRangeException("offset");
                }

                crc ^= CrcSeed;

                while (--count >= 0)
                {
                    crc = CrcTable[(crc ^ buffer[offset++]) & 0xFF] ^ (crc >> 8);
                }

                crc ^= CrcSeed;
            }
        }
        #endregion

        #region 任务下载 2015年11月25日已将该方法放至任务查看界面
        //public void Task()
        //{
        //    _msgThread = new MsgThread(this);
        //    _msgThread.Start();
        //    Message msg = new Message();
        //    msg.what = MsgCode.MSG_DownTask;
        //    msg.obj1 = Global.samplenameadapter[0];
        //    Global.workThread.SendMessage(msg, _msgThread);
        //}

        ///// <summary>
        ///// 下载任务
        ///// </summary>
        ///// <param name="stdCode">标准代码</param>
        ///// <param name="districtCode">区域编码</param>
        //private string DownloadTask(string TaskTemp)
        //{
        //    string delErr = string.Empty;
        //    string err = string.Empty;
        //    StringBuilder sb = new StringBuilder();
        //    DataSet dataSet = new DataSet();
        //    DataTable dtbl = new DataTable();
        //    using (StringReader sr = new StringReader(TaskTemp))
        //        dataSet.ReadXml(sr);
        //    int len = 0;
        //    if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
        //    {
        //        if (dataSet != null)
        //        {
        //            len = dataSet.Tables[0].Rows.Count;
        //            dtbl = dataSet.Tables[0];
        //        }
        //        //任务
        //        clsTaskOpr bll = new clsTaskOpr();
        //        bll.Delete("", out delErr);
        //        sb.Append(delErr);

        //        if (len == 0)
        //            return "";

        //        clsTask Tst = new clsTask();
        //        for (int i = 0; i < len; i++)
        //        {
        //            err = string.Empty;
        //            Tst.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
        //            Tst.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
        //            Tst.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
        //            Tst.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
        //            Tst.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
        //            Tst.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
        //            Tst.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
        //            Tst.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
        //            Tst.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
        //            Tst.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
        //            Tst.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
        //            Tst.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
        //            Tst.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
        //            Tst.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
        //            bll.Insert(Tst, out err);
        //            if (!err.Equals(string.Empty))
        //                sb.Append(err);
        //        }
        //        if (sb.Length > 0)
        //            return sb.ToString();
        //    }
        //    return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        //}

        //class MsgThread : ChildThread
        //{
        //    MainWindow wnd;
        //    private delegate void UIHandleMessageDelegate(Message msg);
        //    private UIHandleMessageDelegate uiHandleMessageDelegate;

        //    public MsgThread(MainWindow wnd)
        //    {
        //        this.wnd = wnd;
        //        uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
        //    }

        //    protected override void HandleMessage(Message msg)
        //    {
        //        base.HandleMessage(msg);
        //        try
        //        {
        //            wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //    protected void UIHandleMessage(Message msg)
        //    {
        //        switch (msg.what)
        //        {
        //            case MsgCode.MSG_DownTask:
        //                if (!string.IsNullOrEmpty(msg.DownLoadTask))
        //                {
        //                    try
        //                    {
        //                        wnd.DownloadTask(msg.DownLoadTask);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine("下载数据错误,或者服务链接不正常，请联系管理员!");
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
        #endregion

    }
}