using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using AIO.AnHui;
using AIO.src;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// SetPortWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {

        #region 全局变量
        private clsttStandardDecideOpr _clsttStandardDecideOprBLL = new clsttStandardDecideOpr();
        private clsCompanyOpr _clsCompanyOprBLL = new clsCompanyOpr();
        private MsgThread _msgThread;
        private Message _msg = new Message();
        //分光光度
        public DYFGDItemPara _itemFG = null;
        //胶体金
        public DYJTJItemPara _itemJT = null;
        //干化学
        public DYGSZItemPara _itemGS = null;
        //重金属
        public DYHMItemPara _itemHM = null;
        public static string[] _serialArry = new string[3];
        public static bool _checkedDown = true;
        /// <summary>
        /// _companyNum 被检单位；_yqcheckItemsNum 仪器检测项目；_checkItemsNum 检测项目；_StandardNum 检测标准；
        /// </summary>
        private int _companyNum = 0, _yqcheckItemsNum = 0, _checkItemsNum = 0, _cehckSyncNum = 0, _StandardNum = 0;
        private DispatcherTimer _DataTimer = null;
        private int _countDownNum = 3;

        private string CompanyTemp = string.Empty, CheckItems = string.Empty, Standard = string.Empty,
            chekcItmes = string.Empty, StandardDecideTemp = string.Empty;
        /// <summary>
        /// 服务器通讯测试
        /// </summary>
        private bool IS_SERVICE_TEST = false;
        private string logType = "settingWindow-error";
        public bool DeviceIdisNull = true;
        #endregion

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Global.set_IsOpenFgd)
                {
                    this.skpFgdTop.Height = 0;
                    this.skpFgdC.Height = 0;
                }
                if (!Global.set_IsOpenZjs)
                    this.skpZjs.Height = 0;
                //if (!Global.set_FaultDetection)
                //    this.BtnCalibration.Visibility = Visibility.Collapsed;
                if (!Global.set_ShowFgd)
                    this.skpFgdC.Height = this.StackPanelLEDSettings.Height = 0;
                //配置文件处摄像头个数一般为4或2，若为其他数则视为全部摄像头都显示
                if (Global.deviceHole.SxtCount == 2)
                    this.skpSxtLine2.Height = 0;
                else if (Global.deviceHole.SxtCount == 0)
                {
                    //配置文件为零则表示所有摄像头都隐藏
                    this.skpSxtLine1.Height = 0;
                    this.skpSxtLine2.Height = 0;
                }
                //1表示重金属启用，0表示未启用
                if (Global.deviceHole.HmCount == 0)
                    this.skpZjs.Height = 0;
                if (Global.samplenameadapter.Count > 0)
                {
                    CheckPointInfo CPoint = Global.samplenameadapter[0];
                    textBoxServerAddr.Text = CPoint.ServerAddr;
                    textBoxRegisterID.Text = CPoint.RegisterID;
                    textBoxRegisterPassword.Password = CPoint.RegisterPassword;
                    textBoxCheckNumber.Text = CPoint.CheckPointID;
                    textBoxCheckName.Text = CPoint.CheckPointName;
                    textBoxCheckType.Text = CPoint.CheckPointType;
                    textBoxCheckOrg.Text = CPoint.Organization;
                    textCheckPlaceCode.Text = CPoint.CheckPlaceCode;
                }
                ComboBoxADPort.Text = Global.strADPORT;
                ComboBoxSXT1Port.Text = Global.strSXT1PORT;
                ComboBoxSXT2Port.Text = Global.strSXT2PORT;
                ComboBoxSXT3Port.Text = Global.strSXT3PORT;
                ComboBoxSXT4Port.Text = Global.strSXT4PORT;
                ComboBoxPRINTPort.Text = Global.strPRINTPORT;
                ComboBoxHMPort.Text = Global.strHMPORT;
                if (Global.set_IsOpenFgd)
                {
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        UIElement element = GenerateHoleLEDSettingUI(i);
                        StackPanelLEDSettings.Children.Add(element);
                    }
                }

                if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Length == 0)
                {
                    DyServiceLayer.Visibility = Visibility.Visible;
                    AhServiceLayer.Visibility = Visibility.Collapsed;
                    ZhServiceLayer.Visibility = Visibility.Collapsed;
                }
                else if (Global.InterfaceType.Equals("ZH"))
                {
                    DyServiceLayer.Visibility = Visibility.Collapsed;
                    AhServiceLayer.Visibility = Visibility.Collapsed;
                    ZhServiceLayer.Visibility = Visibility.Visible;
                }
                else if (Global.InterfaceType.Equals("AH"))
                {
                    DyServiceLayer.Visibility = Visibility.Collapsed;
                    AhServiceLayer.Visibility = Visibility.Visible;
                    ZhServiceLayer.Visibility = Visibility.Collapsed;
                }

                if (Global.InterfaceType.Equals("AH"))
                {
                    //安徽项目相关
                    TxtAhRegisterID.Text = Global.AnHuiInterface.userName;
                    TxtAhPwd.Text = Global.AnHuiInterface.passWord;
                    TxtAhCheckOrg.Text = Global.AnHuiInterface.interfaceVersion;
                    TxtAhCheckNumber.Text = Global.AnHuiInterface.instrument;
                    TxtAhCheckName.Text = Global.AnHuiInterface.instrumentNo;
                    TxtAhServerAddr.Text = Global.AnHuiInterface.ServerAddr;
                    //加载MAC地址
                    TestMoreMethodGetMac.MoreMethodGetMAC getmac = new TestMoreMethodGetMac.MoreMethodGetMAC();
                    List<string> rtnList = new List<string>();
                    rtnList.Add(getmac.GetMacAddressByNetworkInformation());
                    if (rtnList != null && rtnList.Count > 0)
                    {
                        Dictionary<string, string> dicMac = new Dictionary<string, string>();
                        List<string> macList = new List<string>();
                        foreach (string mac in rtnList)
                        {
                            if (!dicMac.ContainsKey(mac))
                            {
                                dicMac.Add(mac, mac);
                                macList.Add(mac);
                            }
                        }
                        CbAhMac.ItemsSource = macList;
                        if (CbAhMac.ItemsSource != null)
                            CbAhMac.SelectedIndex = 0;
                    }
                    if (Global.AnHuiInterface.mac.Length > 0)
                        CbAhMac.Text = Global.AnHuiInterface.mac;
                }

                //广东省智慧云平台
                if (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL"))
                {
                    //唯一机器码为空时可保存
                    TxtZhDeviceId.IsReadOnly = Wisdom.DeviceID.Equals(string.Empty) ? false : true;
                    TxtZhUserID.Text = Wisdom.USER;
                    TxtZhPwd.Password = Wisdom.PASSWORD;
                    if (Wisdom.DeviceID.Equals(string.Empty) && DeviceIdisNull)
                    {
                        if (MessageBox.Show(this, "唯一机器码未设置!是否立即设置？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            TxtZhDeviceId.Focus();
                        }
                    }
                    else
                        TxtZhDeviceId.Text = Wisdom.DeviceID;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI界面时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            finally 
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSetting();
            _msgThread.Stop();
        }

        private void SaveSetting()
        {
            try
            {
                SaveHoleWaveSettings();
                CheckPointInfo CPoint;
                if (Global.samplenameadapter.Count == 0)
                    CPoint = new CheckPointInfo();
                else
                    CPoint = Global.samplenameadapter[0];
                CPoint.ServerAddr = textBoxServerAddr.Text;
                CPoint.RegisterID = textBoxRegisterID.Text;
                CPoint.RegisterPassword = textBoxRegisterPassword.Password;
                CPoint.CheckPointID = textBoxCheckNumber.Text;
                CPoint.CheckPointName = textBoxCheckName.Text;
                CPoint.CheckPointType = textBoxCheckType.Text;
                CPoint.Organization = textBoxCheckOrg.Text;
                CPoint.CheckPlaceCode = textCheckPlaceCode.Text;
                CPoint.UploadUser = textBoxRegisterID.Text.Trim();
                CPoint.UploadUserUUID = Global.setUserUUID;
                if (Global.samplenameadapter.Count == 0) Global.samplenameadapter.Add(CPoint);
                Global.SerializeToFile(Global.samplenameadapter, Global.samplenameadapterFile);
                Global.strSERVERADDR = textBoxServerAddr.Text;
                Global.strHMPORT = ComboBoxHMPort.Text;
                CFGUtils.SaveConfig("SERVERADDR", Global.strSERVERADDR);
                CFGUtils.SaveConfig("ADPORT", Global.strADPORT);
                CFGUtils.SaveConfig("SXT1PORT", Global.strSXT1PORT);
                CFGUtils.SaveConfig("SXT2PORT", Global.strSXT2PORT);
                CFGUtils.SaveConfig("SXT3PORT", Global.strSXT3PORT);
                CFGUtils.SaveConfig("SXT4PORT", Global.strSXT4PORT);
                CFGUtils.SaveConfig("PRINTPORT", Global.strPRINTPORT);
                CFGUtils.SaveConfig("HMPORT", Global.strHMPORT);

                if (Global.InterfaceType.Equals("AH"))
                {
                    //安徽省接口相关
                    Global.AnHuiInterface.userName = TxtAhRegisterID.Text.Trim();
                    Global.AnHuiInterface.passWord = TxtAhPwd.Text.Trim();
                    Global.AnHuiInterface.interfaceVersion = TxtAhCheckOrg.Text.Trim();
                    Global.AnHuiInterface.instrument = TxtAhCheckNumber.Text.Trim();
                    Global.AnHuiInterface.instrumentNo = TxtAhCheckName.Text.Trim();
                    Global.AnHuiInterface.mac = CbAhMac.Text.Trim();
                    Global.AnHuiInterface.ServerAddr = TxtAhServerAddr.Text.Trim();

                    CFGUtils.SaveConfig("ah_userName", Global.AnHuiInterface.userName);
                    CFGUtils.SaveConfig("ah_passWord", Global.AnHuiInterface.passWord);
                    CFGUtils.SaveConfig("ah_interfaceVersion", Global.AnHuiInterface.interfaceVersion);
                    CFGUtils.SaveConfig("ah_instrument", Global.AnHuiInterface.instrument);
                    CFGUtils.SaveConfig("ah_instrumentNo", Global.AnHuiInterface.instrumentNo);
                    CFGUtils.SaveConfig("ah_mac", Global.AnHuiInterface.mac);
                    CFGUtils.SaveConfig("ah_ServerAddr", Global.AnHuiInterface.ServerAddr);
                }

                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ComboBoxPort_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                comboBoxPort.Items.Clear();
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                if (null != ports)
                {
                    for (int i = 0; i < ports.Length; ++i)
                        comboBoxPort.Items.Add(ports[i]);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxADPort_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strADPORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("ADPORT", Global.strADPORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ComboBoxSXT1Port_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strSXT1PORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("SXT1PORT", Global.strSXT1PORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ComboBoxSXT2Port_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strSXT2PORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("SXT2PORT", Global.strSXT2PORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ComboBoxSXT3Port_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strSXT3PORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("SXT3PORT", Global.strSXT3PORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ComboBoxSXT4Port_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strSXT4PORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("SXT4PORT", Global.strSXT4PORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ComboBoxPRINTPort_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                Global.strPRINTPORT = comboBoxPort.Text;
                CFGUtils.SaveConfig("PRINTPORT", Global.strPRINTPORT);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ButtonADPortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonADPortTest.Content = "正在测试";
            ButtonADPortTest.IsEnabled = false;
            NewTest(ComboBoxADPort.Text);
        }

        private void ButtonSXT1PortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT1PortTest.Content = "正在测试";
            ButtonSXT1PortTest.IsEnabled = false;
            Global.strSXT1PORT = ComboBoxSXT1Port.Text.Trim();
            jbkTest(Global.strSXT1PORT);
        }

        private void ButtonSXT2PorTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT2PorTest.Content = "正在测试";
            ButtonSXT2PorTest.IsEnabled = false;
            Global.strSXT2PORT = ComboBoxSXT2Port.Text.Trim();
            jbkTest(Global.strSXT2PORT);
        }

        private void ButtonSXT3PortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT3PortTest.Content = "正在测试";
            ButtonSXT3PortTest.IsEnabled = false;
            Test(ComboBoxSXT3Port.Text);
        }

        private void ButtonSXT4PorTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT4PorTest.Content = "正在测试";
            ButtonSXT4PorTest.IsEnabled = false;
            Test(ComboBoxSXT4Port.Text);
        }

        private void ButtonPRINTPortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonPRINTPortTest.Content = "正在测试";
            ButtonPRINTPortTest.IsEnabled = false;
            TestP(ComboBoxPRINTPort.Text);
        }

        //通讯测试后还原按钮
        private void IsEnabledTrue()
        {
            if (IS_SERVICE_TEST)
            {
                this.textCheckPlaceCode.Text = Global.setOrgNum;
                this.textBoxCheckNumber.Text = Global.setPointNum;
                this.textBoxCheckName.Text = Global.setPonitName;
                this.textBoxCheckType.Text = Global.setPointType;
                this.textBoxCheckOrg.Text = Global.setOrgName;
            }

            ButtonADPortTest.Content = "通讯测试";
            ButtonADPortTest.IsEnabled = true;
            ButtonSXT1PortTest.Content = "通讯测试";
            ButtonSXT1PortTest.IsEnabled = true;
            ButtonSXT2PorTest.Content = "通讯测试";
            ButtonSXT2PorTest.IsEnabled = true;
            ButtonSXT3PortTest.Content = "通讯测试";
            ButtonSXT3PortTest.IsEnabled = true;
            ButtonSXT4PorTest.Content = "通讯测试";
            ButtonSXT4PorTest.IsEnabled = true;
            ButtonPRINTPortTest.Content = "通讯测试";
            ButtonPRINTPortTest.IsEnabled = true;
            ButtonHMPortTest.Content = "通讯测试";
            ButtonHMPortTest.IsEnabled = true;
            buttonServerTest.Content = "通讯测试";
            buttonServerTest.IsEnabled = true;
            if (_countDownNum < 0)
            {
                BtnCalibration.Content = "AD校准";
                BtnCalibration.FontSize = 20;
                tb_Values.IsEnabled = BtnCalibration.IsEnabled = true;
            }
        }

        private void ButtonShowAD_Click(object sender, RoutedEventArgs e)
        {
            Global.IsStartGetBattery = false;
            FgdShowADWindow window = new FgdShowADWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
            Global.IsStartGetBattery = true;
        }

        private void jtjIn(string port)
        {
            _msg = new Message();
            _msg.what = MsgCode.MSG_JBK_IN;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, null);
        }

        private class MsgThread : ChildThread
        {
            SettingsWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(SettingsWindow wnd)
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
            SettingsWindow settingWindow = new SettingsWindow();
            public void UIHandleMessage(Message msg)
            {
                int count = 0;
                switch (msg.what)
                {
                    case MsgCode.MSG_COMM_TEST:
                    case MsgCode.MSG_COMP_TEST:
                    case MsgCode.MSG_COMM_TEST_HM:
                    case MsgCode.MSG_JBK_TEST:
                        if (msg.result)
                        {
                            wnd.IsEnabledTrue();
                            wnd.SaveSetting();
                            MessageBox.Show(wnd, "测试连接成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            wnd.IsEnabledTrue();
                            MessageBox.Show(wnd, "测试连接失败!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_CHECK_CONNECTION:
                        wnd.IS_SERVICE_TEST = true;
                        if (msg.result)
                        {
                            wnd.IsEnabledTrue();
                            wnd.SaveSetting();
                            Global.IsServerTest = false;
                            //wnd.BtnCompany_Click(null, null);
                            // 浙江不用下载被检单位
                            MessageBox.Show("通信测试成功", "通信测试", MessageBoxButton.OK, MessageBoxImage.Question);
                        }
                        else
                        {
                            CheckPointInfo CPoint;
                            if (Global.samplenameadapter.Count == 0)
                                CPoint = new CheckPointInfo();
                            else
                                CPoint = Global.samplenameadapter[0];
                            CPoint.UploadUser = "";
                            CPoint.UploadUserUUID = "";
                            if (Global.samplenameadapter.Count == 0)
                                Global.samplenameadapter.Add(CPoint);
                            Global.SerializeToFile(Global.samplenameadapter, Global.samplenameadapterFile);
                            wnd.IsEnabledTrue();
                            MessageBox.Show(wnd, "测试连接失败!\r\n\r\n异常提示：" + msg.error, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_COMM_CABT:
                        if (msg.result)
                        {
                            wnd.IsEnabledTrue();
                            MessageBox.Show(wnd, "AD校准成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            wnd.IsEnabledTrue();
                            MessageBox.Show(wnd, "AD校准失败！请重试！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_JBK_OUT:
                        if (msg.result)
                        {
                            if (MessageBox.Show("是否立即开始校准?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                wnd.CheckCard(msg.str1);
                            }
                            else
                            {
                                wnd.jtjIn(msg.str1);
                            }
                        }
                        break;
                    case MsgCode.MSG_JBK_CKC:
                        if (msg.result)
                        {
                            wnd.jtjCalibration(msg.str1);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "请放置校准白色金标卡！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    case MsgCode.MSG_JBK_CBT:
                        bool b1 = false, b2 = false;
                        string str = string.Empty;
                        if (msg.str1 == wnd.ComboBoxSXT1Port.Text)
                        {
                            count += 1;
                            if (msg.result)
                                b1 = msg.result;
                            else
                                b2 = msg.result;
                        }
                        if (b1 && b2)
                        {
                            str = "胶体金通道[1,2]校准命令已发送成功！请耐心等待校准结束!";
                        }
                        else if (b1 && !b2)
                        {
                            str = "胶体金通道[1]校准命令已发送成功！\r\n胶体金通道[2]校准命令发送失败！";
                        }
                        else if (!b1 && b2)
                        {
                            str = "胶体金通道[2]校准命令已发送成功！\r\n胶体金通道[1]校准命令发送失败！";
                        }
                        else
                        {
                            str = "校准命令发送失败！请重试！";
                        }
                        if (count == 2)
                        {
                            MessageBox.Show(wnd, str, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_CHECK_SYNC:
                        if (msg.result)
                        {
                            wnd.DownStandard(msg.Standard);
                            wnd.DownCheckItems(msg.CheckItems);
                            wnd.DownloadCompany(msg.DownLoadCompany);
                            wnd.DownloadStandDecide(msg.SampleStandardName);
                            //if (_checkedDown)
                            //{
                            MessageBox.Show(wnd, "全部数据下载成功！本次共下载：" +
                                (wnd._DownCheckItemCount + wnd._DownStandardCount +
                                wnd._YQCheckItemCount + wnd._DownCompanyCount +
                                wnd._DownStandDecideCount) + "条数据！\r\n\r\n" +
                                "检测标准：" + wnd._DownStandardCount + "条！\r\n" +
                                "样品标准：" + wnd._DownStandDecideCount + "条！\r\n" +
                                "检测项目：" + wnd._YQCheckItemCount + "条！\r\n" +
                                "被检单位：" + wnd._DownCompanyCount + "条！\r\n" +
                                "检测项目标准：" + wnd._DownCheckItemCount + "条！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //}
                            //else
                            //{
                            //    MessageBox.Show(wnd, "下载完成，但下载过程中出现了异常！\r\n\r\n建议将此情况反馈管理员！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //}
                        }
                        else
                        {
                            MessageBox.Show(wnd, string.Format("数据下载失败!\r\n\r\n异常信息：{0}", msg.error), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.changeCehckSync(true, "全部数据下载", 20);
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_DownCompany:
                        if (msg.result)
                        {
                            if (!string.IsNullOrEmpty(msg.DownLoadCompany))
                            {
                                try
                                {
                                    wnd.DownloadCompany(msg.DownLoadCompany);
                                }
                                catch (Exception e)
                                {
                                    _checkedDown = false;
                                    Console.WriteLine(e.Message);
                                    wnd.changeCompany(true, "被检单位下载");
                                    MessageBox.Show(wnd, "下载时出现异常！\r\n异常信息：" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                finally
                                {
                                    if (_checkedDown)
                                    {
                                        wnd.changeCompany(true, "被检单位下载");
                                        MessageBox.Show(wnd, "通讯成功！\r\n\r\n且成功同步" + wnd._DownCompanyCount + " 条被检单位数据!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(wnd, "下载数据错误,或者服务链接不正常，请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(wnd, string.Format("被检单位下载失败!\r\n\r\n异常信息：{0}", msg.error), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.changeCompany(true, "被检单位下载");
                        Global.IsStartGetBattery = true;
                        break;
                    case MsgCode.MSG_DownCheckItems:
                        int tiao = 0;
                        if (!string.IsNullOrEmpty(msg.CheckItemsTempList))
                        {
                            try
                            {
                                tiao = Convert.ToInt16(msg.CheckItemsTempList);
                                if (tiao > 0)
                                {
                                    wnd.changeCheckItems(true, "检测项目下载");
                                    MessageBox.Show("检测项目下载成功！共成功下载" + tiao + "条数据");
                                }
                                //wnd.UpdateStatus(msg.CheckItemsTempList);
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                Console.WriteLine(e.Message);
                                wnd.changeCheckItems(true, "检测项目下载");
                                MessageBox.Show("下载失败！请联系管理员！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            //finally
                            //{
                            //    wnd.changeCheckItems(true, "检测项目下载");
                            //    if (_checkedDown)
                            //    {
                            //        MessageBox.Show("检测项目下载成功！共下载：" + wnd._yqcheckItemsNum + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //    }
                            //}
                        }
                        else
                        {
                            wnd.changeCheckItems(true, "检测项目下载");
                            MessageBox.Show("下载数据错误,或者服务链接不正常，请联系管理员！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        Global.IsStartGetBattery = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="b"></param>
        /// <param name="str"></param>
        private void changeCheckItems(bool b, string str)
        {
            btnCheckItems.Content = str;
            btnCheckItems.IsEnabled = b;
        }

        /// <summary>
        /// 被检单位下载
        /// </summary>
        /// <param name="b"></param>
        /// <param name="str"></param>
        private void changeCompany(bool b, string str)
        {
            btnCompany.IsEnabled = b;
            btnCompany.Content = str;
        }

        private void changeCehckSync(bool b, string str, int size)
        {
            buttonCehckSync.Content = str;
            buttonCehckSync.IsEnabled = b;
            buttonCehckSync.FontSize = size;
        }

        /// <summary>
        /// 校准AD
        /// </summary>
        /// <param name="port"></param>
        private void CalibrationAD(string port, string calibrationValue)
        {
            _msg = new Message();
            _msg.what = MsgCode.MSG_COMM_CABT;
            _msg.str1 = port;
            _msg.calibrationValue = calibrationValue;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void countDown(object sender, EventArgs e)
        {
            BtnCalibration.Content = "正在校准" + _countDownNum;
            _countDownNum--;
            if (_countDownNum < 0)
            {
                _DataTimer.Stop();
                _DataTimer = null;
                if (BtnCalibration.IsEnabled == false || BtnCalibration.Content.Equals("正在校准"))
                {
                    BtnCalibration.Content = "AD校准";
                    BtnCalibration.FontSize = 20;
                    tb_Values.IsEnabled = BtnCalibration.IsEnabled = true;
                }
            }
        }

        private void NewTest(string port)
        {
            Global.IsStartGetBattery = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_COMM_TEST;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void jbkTest(string port)
        {
            _msgThread.Stop();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
            Global.IsStartGetBattery = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_JBK_TEST;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        /// <summary>
        /// 胶体金校准
        /// </summary>
        private void jtjCalibration(string port)
        {
            _msg = new Message();
            _msg.what = MsgCode.MSG_JBK_CBT;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, _msgThread);
        }


        /// <summary>
        /// 打印机通讯测试
        /// </summary>
        /// <param name="port"></param>
        private void TestP(string port)
        {
            Global.IsStartGetBattery = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_COMP_TEST;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void Test(string port)
        {
            Global.IsStartGetBattery = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_COMM_TEST;
            _msg.str1 = port;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        // 根据已有的检测孔的波长信息，生成检测孔波长配置。
        private UIElement GenerateHoleLEDSettingUI(int nHole)
        {
            StackPanel stackPanel = new StackPanel();
            try
            {
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                Label labelHoleSetting = new Label();
                labelHoleSetting.Width = 140;
                labelHoleSetting.Height = 40;
                labelHoleSetting.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                labelHoleSetting.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                labelHoleSetting.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                labelHoleSetting.Content = String.Format("检测孔{0:D2}配置:", (nHole + 1));
                labelHoleSetting.FontSize = 20;
                stackPanel.Children.Add(labelHoleSetting);
                for (int i = 0; i < Global.deviceHole.LedCount; ++i)
                {
                    Label labelLEDWave = new Label();
                    labelLEDWave.Width = 50;
                    labelLEDWave.Height = 40;
                    labelLEDWave.Margin = new Thickness(10, 0, 0, 0);
                    labelLEDWave.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    labelLEDWave.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelLEDWave.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    labelLEDWave.Content = "LED" + (i + 1);
                    labelLEDWave.FontSize = 20;
                    ComboBox comboBoxLEDWave = new ComboBox();
                    comboBoxLEDWave.Name = GetName(nHole, i);
                    comboBoxLEDWave.Width = 80;
                    comboBoxLEDWave.Height = 40;
                    comboBoxLEDWave.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    comboBoxLEDWave.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    comboBoxLEDWave.FontSize = 20;
                    comboBoxLEDWave.ItemsSource = DeviceProp.DeviceHole.TotalWaves;
                    comboBoxLEDWave.IsEditable = true;
                    comboBoxLEDWave.Text = "" + Global.deviceHole.LEDWave[nHole][i];
                    stackPanel.Children.Add(labelLEDWave);
                    stackPanel.Children.Add(comboBoxLEDWave);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            return stackPanel;
        }

        private string GetName(int nHole, int nLed)
        {
            return "Hole" + nHole + "Led" + nLed;
        }

        private void GetLED(out int nHole, out int nLed, string name)
        {
            string[] strs = name.Split(new string[] { "Hole", "Led" }, StringSplitOptions.RemoveEmptyEntries);
            Int32.TryParse(strs[0], out nHole);
            Int32.TryParse(strs[1], out nLed);
        }

        /// <summary>
        /// 寻找所有的combobox，按名称，将其内容保存。
        /// </summary>
        private void SaveHoleWaveSettings()
        {
            try
            {
                List<ComboBox> comboBoxes = UIUtils.GetChildObjects<ComboBox>(StackPanelLEDSettings, typeof(ComboBox));
                if (null == comboBoxes)
                    return;
                foreach (ComboBox comboBox in comboBoxes)
                {
                    int nHole, nLed;
                    GetLED(out nHole, out nLed, comboBox.Name);
                    try
                    {
                        Int32.TryParse(comboBox.Text, out Global.deviceHole.LEDWave[nHole][nLed]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        FileUtils.Log(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void buttonServerTest_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            buttonServerTest.Content = "正在测试";
            buttonServerTest.IsEnabled = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_CHECK_CONNECTION;
            _msg.str1 = textBoxServerAddr.Text;
            _msg.str2 = textBoxRegisterID.Text;
            _msg.str3 = textBoxRegisterPassword.Password;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        /// <summary>
        /// 全部数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonCehckSync_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("全部基础数据同步需要较长时间，真要执行同步吗？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                _cehckSyncNum = 0;
                _checkedDown = true;
                _msg = new Message();
                _msg.what = MsgCode.MSG_CHECK_SYNC;
                _msg.str1 = textBoxServerAddr.Text;
                _msg.str2 = textBoxRegisterID.Text;
                _msg.str3 = textBoxRegisterPassword.Password;
                _msg.args.Enqueue(textBoxCheckNumber.Text);
                _msg.args.Enqueue(textBoxCheckName.Text);
                _msg.args.Enqueue(textBoxCheckType.Text);
                _msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(_msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("全部数据下载失败！\r\n异常信息：" + ex.Message, "错误提示");
            }
        }

        private int _DownStandDecideCount = 0;
        private void DownStandardDecideTempProcess(Action<int> percent)
        {
            if (StandardDecideTemp == null || StandardDecideTemp.Length == 0)
                return;

            _DownStandDecideCount = 0;
            percent(0);
            try
            {
                string delErr = string.Empty;
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                using (StringReader sr = new StringReader(StandardDecideTemp))
                {
                    dataSet.ReadXml(sr);
                }

                int len = 0;
                if (dataSet != null)
                {
                    dataTable = dataSet.Tables["Result"];
                    string result = Global.GetResultByCode(dataTable.Rows[0]["ResultCode"].ToString());
                    if (result.Equals("1"))
                    {
                        dataTable = dataSet.Tables["SelectItem"];
                        if (dataTable != null) len = dataTable.Rows.Count;
                    }
                }

                if (len == 0)
                {
                    return;
                }

                percent(5);
                float percentage1 = (float)95 / (float)len, percentage2 = 0;
                int count = (int)percentage1 + 5;
                clsttStandardDecide model = new clsttStandardDecide();
                model.UDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    model.FtypeNmae = dataTable.Rows[i]["FtypeNmae"].ToString();
                    model.SampleNum = dataTable.Rows[i]["SampleNum"].ToString();
                    model.Name = dataTable.Rows[i]["Name"].ToString();
                    model.ItemDes = dataTable.Rows[i]["ItemDes"].ToString();
                    model.StandardValue = dataTable.Rows[i]["StandardValue"].ToString();
                    model.Demarcate = dataTable.Rows[i]["Demarcate"].ToString();
                    model.Unit = dataTable.Rows[i]["Unit"].ToString();
                    _clsttStandardDecideOprBLL.InsertOrUpdate(model, out err);
                    if (!err.Equals(string.Empty)) sb.Append(err);
                    else _DownStandDecideCount++;

                    if (count < 100)
                    {
                        percent(count);
                        percentage2 += percentage1;
                        if (percentage2 > 1)
                        {
                            count += (int)percentage2;
                            percentage2 = 0;
                        }
                    }
                    else
                    {
                        count = 100;
                    }
                }
                if (sb.Length > 0)
                {
                    return;
                }
                _checkedDown = true;
            }
            catch (Exception ex)
            {
                _checkedDown = false;
                FileUtils.OprLog(0, "downLoadStandardDecide-error", ex.ToString());
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 下载样品和检测项目关联判断标准值
        /// </summary>
        /// <param name="StandardDecideTemp"></param>
        /// <returns></returns>
        private void DownloadStandDecide(string data)
        {
            StandardDecideTemp = data;
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownStandardDecideTempProcess;
            process.MessageInfo = "正在下载样品判定标准";
            process.Start();
        }

        /// <summary>
        /// 被检单位下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnCompany_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                changeCompany(false, "正在下载···");
                _checkedDown = true;
                _msg = new Message();
                _msg.what = MsgCode.MSG_DownCompany;
                _msg.str1 = textBoxServerAddr.Text;
                _msg.str2 = textBoxRegisterID.Text;
                _msg.str3 = textBoxRegisterPassword.Password;
                _msg.args.Enqueue(textBoxCheckNumber.Text);
                _msg.args.Enqueue(textBoxCheckName.Text);
                _msg.args.Enqueue(textBoxCheckType.Text);
                _msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(_msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.Log("SettingsWindow-BtnCompany_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("下载被检单位失败！\r\n异常信息：" + ex.Message, "错误提示");
            }
        }

        private int _DownCompanyCount = 0;
        private void DownloadCompanyProcess(Action<int> percent)
        {
            if (CompanyTemp == null || CompanyTemp.Length == 0)
                return;

            _DownCompanyCount = 0;
            percent(0);
            try
            {
                int len = 0;
                string delErr = string.Empty, err = string.Empty;
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                using (StringReader sr = new StringReader(CompanyTemp)) dataSet.ReadXml(sr);
                if (dataSet != null)
                {
                    dataTable = dataSet.Tables["Result"];
                    string result = Global.GetResultByCode(dataTable.Rows[0]["ResultCode"].ToString());
                    if (result.Equals("1"))
                    {
                        dataTable = dataSet.Tables["Company"];
                        if (dataTable != null) len = dataTable.Rows.Count;
                    }
                }

                if (len == 0)
                {
                    percent(100);
                    return;
                }

                percent(3);
                StringBuilder sb = new StringBuilder();
                clsCompany model = new clsCompany();
                float percentage1 = (float)97 / (float)len, percentage2 = 0;
                int count = (int)percentage1 + 3;
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    try
                    {
                        model.SysCode = dataTable.Rows[i]["SysCode"].ToString();
                        model.StdCode = model.SysCode;
                        model.CompanyID = dataTable.Rows[i]["CompanyID"].ToString();
                        model.FullName = dataTable.Rows[i]["FullName"].ToString();
                        model.DisplayName = dataTable.Rows[i]["DisplayName"].ToString();

                        model.Property = "无";//该字段必须有值
                        model.KindCode = dataTable.Rows[i]["KindCode"].ToString();
                        model.RegCapital = Convert.ToInt64(dataTable.Rows[i]["RegCapital"]);
                        model.Unit = dataTable.Rows[i]["Unit"].ToString();
                        model.Incorporator = dataTable.Rows[i]["Incorporator"].ToString();
                        if (!string.IsNullOrEmpty(dataTable.Rows[i]["RegDate"].ToString()))
                            model.RegDate = Convert.ToDateTime(dataTable.Rows[i]["RegDate"]);

                        model.DistrictCode = dataTable.Rows[i]["DistrictCode"].ToString();
                        model.PostCode = dataTable.Rows[i]["PostCode"].ToString();
                        model.Address = dataTable.Rows[i]["Address"].ToString();
                        model.LinkMan = dataTable.Rows[i]["LinkMan"].ToString();
                        model.LinkInfo = dataTable.Rows[i]["LinkInfo"].ToString();
                        model.CreditLevel = dataTable.Rows[i]["CreditLevel"].ToString();
                        model.CreditRecord = dataTable.Rows[i]["CreditRecord"].ToString();
                        model.ProductInfo = dataTable.Rows[i]["ProductInfo"].ToString();
                        model.OtherInfo = dataTable.Rows[i]["OtherInfo"].ToString();
                        model.FoodSafeRecord = dataTable.Rows[i]["FoodSafeRecord"].ToString();
                        model.CheckLevel = dataTable.Rows[i]["CheckLevel"].ToString();
                        model.IsReadOnly = Convert.ToBoolean(dataTable.Rows[i]["IsReadOnly"]);
                        model.IsLock = Convert.ToBoolean(dataTable.Rows[i]["IsLock"]);
                        model.Remark = dataTable.Rows[i]["Remark"].ToString();
                        model.TSign = dataTable.Rows[i]["Sign"].ToString();
                        model.UDate = dataTable.Rows[i]["UDate"].ToString();
                        _clsCompanyOprBLL.InsertOrUpdate(model, out err);
                        if (!err.Equals(string.Empty)) sb.Append(err);
                        else _DownCompanyCount++;

                        if (count < 100)
                        {
                            percent(count);
                            percentage2 += percentage1;
                            if (percentage2 > 1)
                            {
                                count += (int)percentage2;
                                percentage2 = 0;
                            }
                        }
                        else
                        {
                            count = 100;
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(6, logType, ex.ToString());
                        sb.Append(ex.Message);
                    }
                }
                if (sb.Length > 0)
                {
                    percent(100);
                }
                _checkedDown = true;
            }
            catch (Exception ex)
            {
                _checkedDown = false;
                FileUtils.OprLog(0, "downloadCompany-error", ex.ToString());
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 下载检测项目
        /// </summary>
        /// <param name="data"></param>
        private void DownLoadCheckItems(string data)
        {
            CheckItems = data;
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownLoadCheckItemsProcess;
            process.MessageInfo = "正在下载检测项目";
            process.Start();
        }

        private int _YQCheckItemCount = 0;
        private void DownLoadCheckItemsProcess(Action<int> percent)
        {
            _YQCheckItemCount = 0;
            percent(0);
            try
            {
                if (CheckItems != null && !CheckItems.Equals("<NewDataSet>\r\n没有相关的数据下载!\r\n</NewDataSet>"))
                {
                    MainWindow._TempItemNames = _serialArry;
                    percent(5);
                    using (StringReader sr = new StringReader(CheckItems))
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.ReadXml(sr);
                        int len = dataSet.Tables[0].Rows.Count;
                        float percentage1 = (float)95 / (float)len, percentage2 = 0;
                        int count = (int)percentage1 + 5;
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<CHECKITEMS> ItemNames = (List<CHECKITEMS>)IListDataSet.DataSetToIList<CHECKITEMS>(dataSet, 0);
                            foreach (DataRow CheckType in dataSet.Tables[0].Rows)
                            {
                                string[] Provisional = new string[15];
                                for (int Q = 0; Q <= 13; Q++)
                                {
                                    Provisional[Q] = CheckType[Q].ToString();
                                }
                                string TypeName = CheckType[10].ToString();
                                SaveResultValue(TypeName, Provisional);

                                if (count < 100)
                                {
                                    percent(count);
                                    percentage2 += percentage1;
                                    if (percentage2 > 1)
                                    {
                                        count += (int)percentage2;
                                        percentage2 = 0;
                                    }
                                }
                                else
                                {
                                    count = 100;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 下载被检单位
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        private void DownloadCompany(string data)
        {
            CompanyTemp = data;
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadCompanyProcess;
            process.MessageInfo = "通讯成功，正在下载被检单位";
            process.Start();
        }

        private int _DownStandardCount = 0;
        private void DownStandardProcess(Action<int> percent)
        {
            if (Standard == null || Standard.Length == 0)
                return;

            _DownStandardCount = 0;
            percent(0);
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                using (StringReader sr = new StringReader(Standard))
                {
                    dataSet.ReadXml(sr);
                }

                int len = 0;
                if (dataSet != null)
                {
                    dataTable = dataSet.Tables["Result"];
                    if (dataTable != null)
                    {
                        string result = Global.GetResultByCode(dataTable.Rows[0]["ResultCode"].ToString());
                        if (result.Equals("1"))
                        {
                            dataTable = dataSet.Tables["Standard"];
                            len = dataTable.Rows.Count;
                        }
                    }
                }

                if (len == 0) return;

                percent(5);
                float percentage1 = (float)95 / (float)len, percentage2 = 0;
                int count = (int)percentage1 + 5;
                clsStandard model = new clsStandard();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    model.SysCode = dataTable.Rows[i]["SysCode"].ToString();
                    model.StdCode = model.SysCode;
                    model.StdDes = dataTable.Rows[i]["StdDes"].ToString();
                    model.ShortCut = dataTable.Rows[i]["ShortCut"].ToString();
                    model.StdInfo = dataTable.Rows[i]["StdInfo"].ToString();
                    model.StdType = dataTable.Rows[i]["StdType"].ToString();
                    model.IsReadOnly = Convert.ToBoolean(dataTable.Rows[i]["IsReadOnly"]);
                    model.IsLock = Convert.ToBoolean(dataTable.Rows[i]["IsLock"]);
                    model.Remark = string.Empty;
                    model.UDate = dataTable.Rows[i]["UDate"].ToString();
                    if (_clsCompanyOprBLL.InsertOrUpdate(model, out err) == 1)
                        _DownStandardCount++;
                    else if (!err.Equals(string.Empty))
                        sb.Append(err);

                    if (count < 100)
                    {
                        percent(count);
                        percentage2 += percentage1;
                        if (percentage2 > 1)
                        {
                            count += (int)percentage2;
                            percentage2 = 0;
                        }
                    }
                    else
                    {
                        count = 100;
                    }
                }
                _checkedDown = true;
            }
            catch (Exception ex)
            {
                _checkedDown = false;
                FileUtils.OprLog(0, "downLoadStandard-error", ex.ToString());
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 检测标准下载
        /// </summary>
        /// <param name="data"></param>
        private void DownStandard(string data)
        {
            Standard = data;
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownStandardProcess;
            process.MessageInfo = "正在下载检测标准";
            process.Start();
        }

        private int _DownCheckItemCount = 0;
        private void DownCheckItemsProcess(Action<int> percent)
        {
            if (chekcItmes == null || chekcItmes.Length == 0)
                return;

            _DownCheckItemCount = 0;
            percent(0);
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                using (StringReader sr = new StringReader(chekcItmes))
                {
                    dataSet.ReadXml(sr);
                }
                int len = 0;
                if (dataSet != null)
                {
                    dataTable = dataSet.Tables["Result"];
                    if (dataTable != null)
                    {
                        string result = Global.GetResultByCode(dataTable.Rows[0]["ResultCode"].ToString());
                        if (result.Equals("1"))
                        {
                            dataTable = dataSet.Tables["CheckItem"];
                            len = dataTable.Rows.Count;
                        }
                    }
                }

                percent(5);
                float percentage1 = (float)95 / (float)len, percentage2 = 0;
                int count = (int)percentage1 + 5;
                if (len == 0)
                {
                    return;
                }

                clsCheckItem model = new clsCheckItem();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    model.SysCode = dataTable.Rows[i]["Code"].ToString();
                    model.StdCode = model.SysCode;
                    model.ItemDes = dataTable.Rows[i]["ItemDes"].ToString();
                    model.CheckType = dataTable.Rows[i]["CheckType"].ToString();
                    model.StandardCode = dataTable.Rows[i]["StandardCode"].ToString();
                    model.StandardValue = dataTable.Rows[i]["StandardValue"].ToString();
                    model.Unit = dataTable.Rows[i]["Unit"].ToString();
                    model.DemarcateInfo = dataTable.Rows[i]["DemarcateInfo"].ToString();
                    model.TestValue = string.Empty;//dataTable.Rows[i]["TestValue"].ToString();
                    model.OperateHelp = string.Empty;//dataTable.Rows[i]["OperateHelp"].ToString();
                    model.CheckLevel = string.Empty;//dataTable.Rows[i]["CheckLevel"].ToString();
                    model.IsReadOnly = Convert.ToBoolean(dataTable.Rows[i]["IsReadOnly"]);
                    model.IsLock = Convert.ToBoolean(dataTable.Rows[i]["IsLock"]);
                    model.Remark = string.Empty;// dataTable.Rows[i]["Remark"].ToString();
                    model.UDate = dataTable.Rows[i]["UDate"].ToString();
                    if (_clsCompanyOprBLL.InsertOrUpdate(model, out err) == 1)
                        _DownCheckItemCount += 1;
                    else if (!err.Equals(string.Empty))
                        sb.Append(err);

                    if (count < 100)
                    {
                        percent(count);
                        percentage2 += percentage1;
                        if (percentage2 > 1)
                        {
                            count += (int)percentage2;
                            percentage2 = 0;
                        }
                    }
                    else
                    {
                        count = 100;
                    }
                }
                _checkedDown = true;
            }
            catch (Exception ex)
            {
                _checkedDown = false;
                FileUtils.OprLog(0, "downLoadCheckItems-error", ex.ToString());
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="data"></param>
        private void DownCheckItems(string data)
        {
            chekcItmes = data;
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownCheckItemsProcess;
            process.MessageInfo = "正在下载检测项目标准";
            process.Start();
        }

        /// <summary>
        /// 仪器检测项目下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                changeCheckItems(false, "正在下载···");
                _checkedDown = true;
                _msg = new Message();
                _msg.what = MsgCode.MSG_DownCheckItems;
                _msg.str1 = textBoxServerAddr.Text;
                _msg.str2 = textBoxRegisterID.Text;
                _msg.str3 = textBoxRegisterPassword.Password;
                _msg.args.Enqueue(textBoxCheckNumber.Text);
                _msg.args.Enqueue(textBoxCheckName.Text);
                _msg.args.Enqueue(textBoxCheckType.Text);
                _msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(_msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.Log("SettingsWindow-DownCheckItems:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("检测项目下载失败！\r\n异常信息：" + ex.Message, "错误提示");
            }
        }

        //修改显示界面
        private void UpdateStatus(string data)
        {
            try
            {

                MessageBox.Show("检测项目下载完成","提示");
                //if (data != null && !data.Equals("<NewDataSet>\r\n没有相关的数据下载!\r\n</NewDataSet>"))
                //{
                //    _yqcheckItemsNum = 0;
                //    MainWindow._TempItemNames = _serialArry;
                //    using (StringReader sr = new StringReader(data))
                //    {
                //        DataSet dataSet = new DataSet();
                //        dataSet.ReadXml(sr);
                //        if (dataSet.Tables[0].Rows.Count > 0)
                //        {
                //            List<CHECKITEMS> ItemNames = (List<CHECKITEMS>)IListDataSet.DataSetToIList<CHECKITEMS>(dataSet, 0);
                //            foreach (DataRow CheckType in dataSet.Tables[0].Rows)
                //            {
                //                string[] Provisional = new string[15];
                //                for (int Q = 0; Q <= 13; Q++)
                //                {
                //                    Provisional[Q] = CheckType[Q].ToString();
                //                }
                //                string TypeName = CheckType[10].ToString();
                //                SaveResultValue(TypeName, Provisional);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 分光光度检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void saveFgdResult(string[] ArryTemp)
        {
            try
            {
                DYFGDItemPara item = new DYFGDItemPara();
                item.Name = ArryTemp[0].ToString();//项目名称
                item.Unit = ArryTemp[3].ToString();//项目检测单位
                item.HintStr = ArryTemp[4].ToString();//简要操作:新添加实验--注释
                item.Password = ArryTemp[5].ToString();//密码
                Int32.TryParse(ArryTemp[6].ToString(), out item.SampleNum);//样品编号
                Int32.TryParse(ArryTemp[11].ToString(), out item.Wave);//波长
                //检测方法选择
                item.Method = Convert.ToInt32(ArryTemp[12].ToString());
                Global.fgdItems.Add(item);
                Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                return;
            }
            _yqcheckItemsNum += 1;
        }

        /// <summary>
        /// 胶体金检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void saveJtjResult(string[] ArryTemp)
        {
            try
            {
                DYJTJItemPara item = new DYJTJItemPara();
                item.Name = ArryTemp[0].ToString();//项目名称
                item.Unit = ArryTemp[3].ToString();//项目检测单位
                item.HintStr = ArryTemp[4].ToString();//简要操作：新添加实验--注释
                item.Password = ArryTemp[5].ToString();//密码
                Int32.TryParse(ArryTemp[6].ToString(), out item.SampleNum);//样品编号
                //Int32.TryParse(JTJItemNameType[9].ToString(), out item.Wave);//波长
                //检测方法选择
                item.Method = Convert.ToInt32(ArryTemp[12].ToString());
                item.Hole[0].Use = Convert.ToBoolean(ArryTemp[8].ToString());
                item.Hole[1].Use = Convert.ToBoolean(ArryTemp[9].ToString());
                item.Hole[2].Use = true;
                item.Hole[3].Use = true;
                item.InvalidC = Convert.ToInt32(ArryTemp[7].ToString());
                Global.jtjItems.Add(item);
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                return;
            }
            _yqcheckItemsNum += 1;
        }

        /// <summary>
        /// 干化学检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void saveGszResult(string[] ArryTemp)
        {
            try
            {
                DYGSZItemPara item = new DYGSZItemPara();
                item.Name = ArryTemp[0].ToString();//项目名称
                item.Unit = ArryTemp[3].ToString();//项目检测单位
                item.HintStr = ArryTemp[4].ToString();//简要提示:新添加实验--注释
                item.Password = ArryTemp[5].ToString();//密码
                Int32.TryParse(ArryTemp[6].ToString(), out item.SampleNum);//样品编号
                //检测方法选择
                item.Method = Convert.ToInt32(ArryTemp[12].ToString());
                item.Hole[0].Use = Convert.ToBoolean(ArryTemp[8].ToString());
                item.Hole[1].Use = Convert.ToBoolean(ArryTemp[9].ToString());
                item.Hole[2].Use = true;
                item.Hole[3].Use = true;
                Global.gszItems.Add(item);
                Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                return;
            }
            _yqcheckItemsNum += 1;
        }

        /// <summary>
        /// 重金属检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void saveZjsResult(string[] ArryTemp)
        {
            try
            {
                DYHMItemPara item = new DYHMItemPara();
                item.Name = ArryTemp[0].ToString();//项目名称
                item.Unit = ArryTemp[3].ToString();//项目检测单位
                item.HintStr = ArryTemp[4].ToString();//简要操作：新添加实验--注释
                item.Password = ArryTemp[5].ToString();//密码
                Int32.TryParse(ArryTemp[6].ToString(), out item.SampleNum);//样品编号
                //检测方法选择
                item.Method = Convert.ToInt32(ArryTemp[12].ToString());
                item.Hole[0].Use = Convert.ToBoolean(ArryTemp[8].ToString());
                item.Hole[1].Use = Convert.ToBoolean(ArryTemp[9].ToString());
                Global.hmItems.Add(item);
                Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                return;
            }
            _yqcheckItemsNum += 1;
        }

        public void SaveResultValue(string ChoiceAndSelect, string[] ArryTemp)
        {
            switch (ChoiceAndSelect)
            {
                case "分光光度":
                    saveFgdResult(ArryTemp);
                    break;
                case "胶体金":
                    saveJtjResult(ArryTemp);
                    break;
                case "干化学法":
                    saveGszResult(ArryTemp);
                    break;
                case "重金属":
                    saveZjsResult(ArryTemp);
                    break;
            }
        }

        private void ComboBoxHMPort_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strHMPORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("HMPORT", Global.strHMPORT);
        }

        private void ButtonHMPortTest_Click(object sender, RoutedEventArgs e)
        {
            Global.IsStartGetBattery = false;
            ButtonHMPortTest.Content = "正在测试";
            ButtonHMPortTest.IsEnabled = false;
            _msg = new Message();
            _msg.what = MsgCode.MSG_COMM_TEST_HM;
            _msg.str1 = ComboBoxHMPort.Text;
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void BtnCalibration_Click(object sender, RoutedEventArgs e)
        {
            Global.IsStartGetBattery = false;
            bool isStart = true;
            tb_Values.IsEnabled = BtnCalibration.IsEnabled = false;
            string str = tb_Values.Text.Trim();
            try
            {
                if (str.Equals(""))
                {
                    MessageBox.Show("请输入校准值(0~1000)！");
                    tb_Values.Focus();
                    isStart = false;
                }
                else if (int.Parse(str) < 0 || int.Parse(str) > 1000)
                {
                    MessageBox.Show("超出校准值范围！请输入:0~1000！");
                    tb_Values.Text = "";
                    tb_Values.Focus();
                    isStart = false;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show(ex.Message);
                tb_Values.Text = "";
                tb_Values.Focus();
                isStart = false;
            }
            if (isStart)
            {
                countdownStart();
            }
            CalibrationAD(ComboBoxADPort.Text, str);
        }

        /// <summary>
        /// 校准按钮开始倒计时
        /// </summary>
        private void countdownStart()
        {
            if (_DataTimer == null)
                _DataTimer = new DispatcherTimer();
            _countDownNum = 15;
            _DataTimer.Interval = TimeSpan.FromSeconds(1);
            _DataTimer.Tick += new EventHandler(countDown);
            _DataTimer.Start();
        }

        private void tb_Values_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);
            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                Int32 num = 0;
                if (!Int32.TryParse(textBox.Text, out num) || num > 500 || num < 0)
                {
                    MessageBox.Show("请输入0~500的AD校准值！", "操作提示");
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        /// <summary>
        /// 检测是否有卡
        /// </summary>
        private void CheckCard(string port)
        {
            Global.IsStartGetBattery = false;
            Message msg1 = new Message();
            msg1.what = MsgCode.MSG_JBK_CKC;
            msg1.str1 = port;
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        /// <summary>
        /// 胶体金校准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_JtjJZ_Click(object sender, RoutedEventArgs e)
        {
            Global.IsStartGetBattery = false;
            Message msg1 = new Message();
            msg1.what = MsgCode.MSG_JBK_OUT;
            msg1.str1 = ComboBoxSXT1Port.Text.Trim();
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        private void btn_JtjJZ2_Click(object sender, RoutedEventArgs e)
        {
            Global.IsStartGetBattery = false;
            Message msg1 = new Message();
            msg1.what = MsgCode.MSG_JBK_OUT;
            msg1.str1 = ComboBoxSXT2Port.Text.Trim();
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        private void btn_autoUpdater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("未找到系统自动升级服务！\r\n\r\n请联系软件供应商提供系统自动升级服务程序！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 安徽项目下载前先赋值
        /// </summary>
        private void startDownload()
        {
            Global.AnHuiInterface.userName = TxtAhRegisterID.Text;
            Global.AnHuiInterface.passWord = TxtAhPwd.Text;
            Global.AnHuiInterface.interfaceVersion = TxtAhCheckOrg.Text;
            Global.AnHuiInterface.instrument = TxtAhCheckNumber.Text;
            Global.AnHuiInterface.instrumentNo = TxtAhCheckName.Text;
            Global.AnHuiInterface.ServerAddr = TxtAhServerAddr.Text;
            Global.AnHuiInterface.mac = CbAhMac.Text;
        }

        private TimerWindow TimerWindow;
        private void loadTimerWindow(bool isLoad)
        {
            try
            {
                if (isLoad)
                {
                    TimerWindow = new TimerWindow();
                    TimerWindow.ShowInTaskbar = false;
                    TimerWindow.Owner = this;
                    TimerWindow.Show();
                }
                else
                {
                    TimerWindow.Close();
                }
            }
            catch (Exception) { }
        }

        private void BtnAhDownCompany_Click(object sender, RoutedEventArgs e)
        {
            loadTimerWindow(true);
            startDownload();
            //被检单位下载 - 安徽
            Global.UploadSCount = 0;
            string error = string.Empty;
            //字典下载
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);
                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                if (strRequest.Equals("1"))
                {
                    //被检企业表
                    List<checked_unit> checked_unitList = Global.AnHuiInterface.checked_unitList;
                    if (checked_unitList != null && checked_unitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("Company", out err);
                        err = string.Empty;
                        foreach (checked_unit item in checked_unitList)
                        {
                            clsCompany company = new clsCompany();
                            company.FullName = item.unitName;
                            company.SysCode = Global.GETGUID(null);
                            company.StdCode = item.id;
                            company.CAllow = item.bussinessId;
                            company.CompanyID = item.id;
                            company.ShortName = item.unitName;
                            company.DisplayName = item.unitName;
                            company.Property = item.busScope;
                            company.Address = item.address;
                            company.LinkMan = item.linkName;
                            company.LinkInfo = item.tel;
                            err = string.Empty;
                            new clsCompanyOpr().Insert(company, out err);
                            Global.UploadSCount += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "被检企业下载异常信息：" + err : err;
                                }
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
            }
            finally
            {
                loadTimerWindow(false);
            }
            if (Global.UploadSCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadSCount + " 条数据！", "操作提示");
            }
            else if (Global.UploadSCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadSCount + " 条数据！\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadSCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        private void BtnAhDownCheckItems_Click(object sender, RoutedEventArgs e)
        {
            loadTimerWindow(true);
            //数据字典下载- 安徽
            Global.UploadSCount = 0;
            string error = string.Empty;
            //字典下载
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);

                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                if (strRequest.Equals("1"))
                {
                    //数据字典表下载
                    List<data_dictionary> data_dictionaryList = Global.AnHuiInterface.data_dictionaryList;
                    if (data_dictionaryList != null && data_dictionaryList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("data_dictionary", out err);
                        err = string.Empty;
                        foreach (data_dictionary item in data_dictionaryList)
                        {
                            DataOperation.Insert(item, out err);
                            Global.UploadSCount += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "数据字典下载异常信息：" + err : err;
                                }
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
            }
            finally
            {
                loadTimerWindow(false);
            }
            if (Global.UploadSCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadSCount + " 条数据！", "操作提示");
            }
            else if (Global.UploadSCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadSCount + " 条数据！\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadSCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        private void BtnAhDownCehckSync_Click(object sender, RoutedEventArgs e)
        {
            loadTimerWindow(true);
            Global.UploadSCount = 0;
            int data_dictionary = 0, checked_unit = 0, standard_limit = 0;
            string error = string.Empty;
            //字典下载
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);

                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                if (strRequest.Equals("1"))
                {
                    //数据字典表下载
                    List<data_dictionary> data_dictionaryList = Global.AnHuiInterface.data_dictionaryList;
                    if (data_dictionaryList != null && data_dictionaryList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("data_dictionary", out err);
                        err = string.Empty;
                        foreach (data_dictionary item in data_dictionaryList)
                        {
                            DataOperation.Insert(item, out err);
                            data_dictionary += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "数据字典下载异常信息：" + err : err;
                                }
                            }
                        }
                        error += strErr;
                    }
                    //被检企业表
                    List<checked_unit> checked_unitList = Global.AnHuiInterface.checked_unitList;
                    if (checked_unitList != null && checked_unitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("Company", out err);
                        err = string.Empty;
                        foreach (checked_unit item in checked_unitList)
                        {
                            clsCompany company = new clsCompany();
                            company.FullName = item.unitName;
                            company.SysCode = DateTime.Now.Millisecond.ToString();
                            company.StdCode = item.id;
                            company.CAllow = item.bussinessId;
                            company.CompanyID = item.id;
                            company.ShortName = item.unitName;
                            company.DisplayName = item.unitName;
                            company.Property = item.busScope;
                            company.Address = item.address;
                            company.LinkMan = item.linkName;
                            company.LinkInfo = item.tel;
                            err = string.Empty;
                            new clsCompanyOpr().Insert(company, out err);
                            checked_unit += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "被检企业下载异常信息：" + err : err;
                                }
                            }
                        }
                        error += strErr;
                    }
                    //标准限量表
                    List<standard_limit> standard_limitList = Global.AnHuiInterface.standard_limitList;
                    if (standard_limitList != null && standard_limitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("standard_limit", out err);
                        err = string.Empty;
                        foreach (standard_limit item in standard_limitList)
                        {
                            DataOperation.Insert(item, out err);
                            standard_limit += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "标准限量下载异常信息：" + err : err;
                                }
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
                Global.UploadSCount = data_dictionary + checked_unit + standard_limit;
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
            }
            finally
            {
                loadTimerWindow(false);
            }
            if (Global.UploadSCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadSCount + " 条数据！\r\n\r\n" +
                "数据字典 " + data_dictionary + " 条；\r\n被检企业 " + checked_unit + " 条；\r\n检测标准 " +
                standard_limit + " 条；", "操作提示");
            }
            else if (Global.UploadSCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadSCount + " 条数据！\r\n\r\n " +
                    "数据字典 " + data_dictionary + " 条；\r\n被检企业 " + checked_unit + " 条；\r\n检测标准 " +
                standard_limit + " 条；\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadSCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        /// <summary>
        /// 东莞智慧平台保存设备唯一码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SaveDeviceId_Click(object sender, RoutedEventArgs e)
        {
            string val = TxtZhUserID.Text.Trim();
            if (val.Length == 0)
            {
                MessageBox.Show(this, "用户名不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                TxtZhUserID.Focus();
                return;
            }
            Wisdom.USER = val;

            val = TxtZhPwd.Password.Trim();
            if (val.Length == 0)
            {
                MessageBox.Show(this, "密码不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                TxtZhPwd.Focus();
                return;
            }
            Wisdom.PASSWORD = val;

            val = TxtZhDeviceId.Text.Trim();
            if (val.Length == 0)
            {
                MessageBox.Show(this, "设备唯一码不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                TxtZhDeviceId.Focus();
                return;
            }
            Wisdom.DeviceID = val;

            if (MessageBox.Show(string.Format("设备唯一码[{0}] 绑定后将可能无法更改！\r\n\r\n确定信息无误，立即绑定设备唯一码？", 
                Wisdom.DeviceID), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            CFGUtils.SaveConfig("DeviceId", Wisdom.DeviceID);
            CFGUtils.SaveConfig("USER", Wisdom.USER);
            CFGUtils.SaveConfig("PASSWORD", Wisdom.PASSWORD);
            MessageBox.Show(this, "服务器链接信息绑定成功!","系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            TxtZhDeviceId.IsReadOnly = true;
        }

    }
}