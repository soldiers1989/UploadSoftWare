using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using AIO.AnHui;
using AIO.src;
using AIO.xaml.Dialog;
using AIO.xaml.Main;
using AIO.xaml.Record;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;
using Microsoft.Win32;

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
            CFGUtils.SaveConfig("FgIsStart", "1");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetMac();
                btn_JtjJZ.Visibility = btn_JtjJZ2.Visibility = Visibility.Hidden;
                //开启了临界值测试功能，并且是新模块
                if (Global.IsShowValue && (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20))
                {
                    btn_Hole1Test.Visibility = Visibility.Visible;
                    btn_Hole2Test.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_Hole1Test.Visibility = Visibility.Collapsed;
                    btn_Hole2Test.Visibility = Visibility.Collapsed;
                }
                if ((Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x30))
                {
                    btn_JtjJZ.Visibility = btn_JtjJZ2.Visibility = Visibility.Collapsed;
                    lbJZ1.Visibility = lbJZ2.Visibility = Visibility.Collapsed;
                    btn_jtjFirmwareUpdate1.Visibility = btn_jtjFirmwareUpdate2.Visibility = Visibility.Visible;
                }

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
                    KjServiceLayer.Visibility = Visibility.Collapsed;
                }
                else if (Global.InterfaceType.Equals("ZH"))
                {
                    DyServiceLayer.Visibility = Visibility.Collapsed;
                    AhServiceLayer.Visibility = Visibility.Collapsed;
                    ZhServiceLayer.Visibility = Visibility.Visible;
                    KjServiceLayer.Visibility = Visibility.Collapsed;
                }
                else if (Global.InterfaceType.Equals("AH"))
                {
                    DyServiceLayer.Visibility = Visibility.Collapsed;
                    AhServiceLayer.Visibility = Visibility.Visible;
                    ZhServiceLayer.Visibility = Visibility.Collapsed;
                    KjServiceLayer.Visibility = Visibility.Collapsed;
                }
                else if (Global.InterfaceType.Equals("KJ"))
                {
                    DyServiceLayer.Visibility = Visibility.Collapsed;
                    AhServiceLayer.Visibility = Visibility.Collapsed;
                    ZhServiceLayer.Visibility = Visibility.Collapsed;
                    KjServiceLayer.Visibility = Visibility.Visible;
                    textBoxKjServerAddr.Text = Global.KjServer.KjServerAddr;
                    textBoxKjuser_name.Text = Global.KjServer.Kjuser_name;
                    textBoxKjpassword.Password = Global.KjServer.Kjpassword;
                    textKjrealname.Text = Global.KjServer.Kjrealname;
                    textBoxKjd_depart_name.Text = Global.KjServer.Kjd_depart_name;
                    textBoxKjp_point_name.Text = Global.KjServer.Kjp_point_name;
                    textBoxKjseries.Text = Global.KjServer.Kjseries;
                    textBoxKjserial_number.Text = Global.DeviceID;
                    //if (Global.Mac.Length > 0)
                    //    CbKjMac.Text = Global.Mac;
                    CbKjMac.Text = Global.GetMACComputer();
                }

                ////新模块显示放大倍数
                //if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20)
                //{
                //    lbFdbs1.Visibility = Visibility.Visible;
                //    lbFdbs2.Visibility = Visibility.Visible;
                //    tb_Fdbs1.Visibility = Visibility.Visible;
                //    tb_Fdbs2.Visibility = Visibility.Visible;
                //}
                //else
                //{
                //    lbFdbs1.Visibility = Visibility.Collapsed;
                //    lbFdbs2.Visibility = Visibility.Collapsed;
                //    tb_Fdbs1.Visibility = Visibility.Collapsed;
                //    tb_Fdbs2.Visibility = Visibility.Collapsed;
                //}

                if (Global.InterfaceType.Equals("AH"))
                {
                    //安徽项目相关
                    TxtAhRegisterID.Text = Global.AnHuiInterface.userName;
                    TxtAhPwd.Text = Global.AnHuiInterface.passWord;
                    TxtAhCheckOrg.Text = Global.AnHuiInterface.interfaceVersion;
                    TxtAhCheckNumber.Text = Global.AnHuiInterface.instrument;
                    TxtAhCheckName.Text = Global.AnHuiInterface.instrumentNo;
                    TxtAhServerAddr.Text = Global.AnHuiInterface.ServerAddr;
                    if (Global.Mac.Length > 0)
                        CbAhMac.Text = Global.Mac;
                }

                //广东省智慧云平台
                if (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL"))
                {
                    //唯一机器码为空时可保存
                    TxtZhDeviceId.IsReadOnly = Global.DeviceID.Equals(string.Empty) ? false : true;
                    TxtZhUserID.Text = Wisdom.USER;
                    TxtZhPwd.Password = Wisdom.PASSWORD;
                    if (Global.DeviceID.Equals(string.Empty) && DeviceIdisNull)
                    {
                        if (MessageBox.Show(this, "唯一机器码未设置!是否立即设置？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            TxtZhDeviceId.Focus();
                        }
                    }
                    else
                        TxtZhDeviceId.Text = Global.DeviceID;
                }

                //薄层色谱 启用薄层色谱时，1通道为胶体金，2通道为薄层色谱
                if (Global.IsEnableBcsp)
                {
                    lbJtj1.Content = "胶体金端口:";
                    lbJtj2.Content = "薄层色谱端口:";
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
                btn_printSetting.Visibility = Global.PrintQrCode ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void GetMac()
        {
            //目前只有安徽和快检服务用到了mac地址
            if (!Global.InterfaceType.Equals("AH") && !Global.InterfaceType.Equals("KJ"))
            {
                return;
            }

            //加载MAC地址
            TestMoreMethodGetMac.MoreMethodGetMAC getmac = new TestMoreMethodGetMac.MoreMethodGetMAC();
            List<string> rtnList = new List<string>
            {
                getmac.GetMacAddressByNetworkInformation()
            };
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
                CbKjMac.ItemsSource = CbAhMac.ItemsSource = macList;
                if (CbAhMac.ItemsSource != null)
                    CbAhMac.SelectedIndex = 0;
                if (CbKjMac.ItemsSource != null)
                    CbKjMac.SelectedIndex = 0;
            }
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
                CPoint.UploadUserUUID = Global.userUUID;
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
                    Global.Mac = CbAhMac.Text.Trim();
                    Global.AnHuiInterface.ServerAddr = TxtAhServerAddr.Text.Trim();

                    CFGUtils.SaveConfig("ah_userName", Global.AnHuiInterface.userName);
                    CFGUtils.SaveConfig("ah_passWord", Global.AnHuiInterface.passWord);
                    CFGUtils.SaveConfig("ah_interfaceVersion", Global.AnHuiInterface.interfaceVersion);
                    CFGUtils.SaveConfig("ah_instrument", Global.AnHuiInterface.instrument);
                    CFGUtils.SaveConfig("ah_instrumentNo", Global.AnHuiInterface.instrumentNo);
                    CFGUtils.SaveConfig("ah_mac", Global.Mac);
                    CFGUtils.SaveConfig("ah_ServerAddr", Global.AnHuiInterface.ServerAddr);
                }
                else if (Global.InterfaceType.Equals("KJ"))
                {
                    Global.KjServer.KjServerAddr = textBoxKjServerAddr.Text.Trim();
                    Global.KjServer.Kjuser_name = textBoxKjuser_name.Text.Trim();
                    Global.KjServer.Kjpassword = textBoxKjpassword.Password;
                    if (Global.KjServer.userLoginEntity != null)
                    {
                        Global.KjServer.Kjrealname = Global.KjServer.userLoginEntity.user.realname;
                        Global.KjServer.Kjd_depart_name = Global.KjServer.userLoginEntity.user.d_depart_name;
                        Global.KjServer.Kjp_point_name = Global.KjServer.userLoginEntity.user.p_point_name;
                    }
                    Global.KjServer.Kjseries = textBoxKjseries.Text.Trim();
                    Global.Mac = CbKjMac.Text.Trim();
                    Global.DeviceID = textBoxKjserial_number.Text.Trim();

                    CFGUtils.SaveConfig("KjServerAddr", Global.KjServer.KjServerAddr);
                    CFGUtils.SaveConfig("Kjuser_name", Global.KjServer.Kjuser_name);
                    CFGUtils.SaveConfig("Kjpassword", Global.KjServer.Kjpassword);
                    CFGUtils.SaveConfig("Kjrealname", Global.KjServer.Kjrealname);
                    CFGUtils.SaveConfig("Kjd_depart_name", Global.KjServer.Kjd_depart_name);
                    CFGUtils.SaveConfig("Kjp_point_name", Global.KjServer.Kjp_point_name);
                    CFGUtils.SaveConfig("Mac", Global.Mac);
                    CFGUtils.SaveConfig("DeviceId", Global.DeviceID);

                    textKjrealname.Text = Global.KjServer.Kjrealname;
                    textBoxKjd_depart_name.Text = Global.orgName;
                    textBoxKjp_point_name.Text = Global.pointName;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSetting();
            CFGUtils.SaveConfig("FgIsStart", "0");
            _msgThread.Stop();
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
                this.textCheckPlaceCode.Text = Global.orgNum;
                this.textBoxCheckNumber.Text = Global.pointNum;
                this.textBoxCheckName.Text = Global.ponitName;
                this.textBoxCheckType.Text = Global.pointType;
                this.textBoxCheckOrg.Text = Global.orgName;
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
            buttonKjServerTest.Content = "通讯测试";
            buttonKjServerTest.IsEnabled = true;
            btnKjregisterDevice.Content = "仪器注册";
            btnKjregisterDevice.IsEnabled = true;
            if (_countDownNum < 0)
            {
                BtnCalibration.Content = "AD校准";
                BtnCalibration.FontSize = 20;
                tb_Values.IsEnabled = BtnCalibration.IsEnabled = true;
            }

            //如果是胶体金新摄像头模块，则不需要校准功能
            btn_JtjJZ.Visibility = btn_JtjJZ2.Visibility =
                Global.JtjVersion == 3 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ButtonShowAD_Click(object sender, RoutedEventArgs e)
        {
            FgdShowADWindow window = new FgdShowADWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void jtjIn(string port)
        {
            _msg = new Message
            {
                what = MsgCode.MSG_JBK_IN,
                str1 = port
            };
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
                            wnd.SaveSetting();
                            ////新模块显示放大倍数
                            //if (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20)
                            //{
                            //    wnd.lbFdbs1.Visibility = Visibility.Visible;
                            //    wnd.lbFdbs2.Visibility = Visibility.Visible;
                            //    wnd.tb_Fdbs1.Visibility = Visibility.Visible;
                            //    wnd.tb_Fdbs2.Visibility = Visibility.Visible;
                            //}
                            //else
                            //{
                            //    wnd.lbFdbs1.Visibility = Visibility.Collapsed;
                            //    wnd.lbFdbs2.Visibility = Visibility.Collapsed;
                            //    wnd.tb_Fdbs1.Visibility = Visibility.Collapsed;
                            //    wnd.tb_Fdbs2.Visibility = Visibility.Collapsed;
                            //}
                            MessageBox.Show(wnd, "测试连接成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "测试连接失败!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        wnd.IsEnabledTrue();
                        break;

                    case MsgCode.MSG_CHECK_CONNECTION:
                        wnd.IS_SERVICE_TEST = true;
                        if (msg.result)
                        {
                            wnd.SaveSetting();
                            //wnd.BtnCompany_Click(null, null);
                            MessageBox.Show("链接成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            MessageBox.Show(wnd, "测试连接失败!\r\n\r\n异常提示：" + msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.IsEnabledTrue();
                        break;

                    case MsgCode.MSG_REGISTERDEVICE:
                        if (msg.result)
                        {
                            wnd.textBoxKjserial_number.Text = Global.MachineNum;
                            //if (Global.KjServer.registerDeviceEntity != null)
                            //{
                            //    wnd.textBoxKjserial_number.Text = Global.KjServer.registerDeviceEntity.serial_number;
                            //}
                            MessageBox.Show("仪器注册成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "仪器注册失败!\r\n\r\n异常提示：" + msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.IsEnabledTrue();
                        break;

                    case MsgCode.MSG_COMM_CABT:
                        if (msg.result)
                        {
                            MessageBox.Show(wnd, "AD校准成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "AD校准失败！请重试！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        wnd.IsEnabledTrue();
                        break;
                    case MsgCode.MSG_JBK_OUT:
                        if (msg.result)
                        {
                            //是否是在测试临界值
                            if (wnd.IsTest)
                            {
                                if (wnd.IsCard)
                                {
                                    if (MessageBox.Show("是否立即开始测试通道临界值?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        wnd.IsCard = false;
                                    }
                                    else
                                    {
                                        wnd.jtjIn(msg.str1);
                                        wnd.IsTest = false;
                                        wnd.btn_Hole1Test.Content = "通道1临界值";
                                        wnd.btn_Hole2Test.Content = "通道2临界值";
                                        return;
                                    }
                                }
                                Thread.Sleep(3000);
                                wnd.JbkInAndTest(msg.str1);
                                return;
                            }
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

                    case MsgCode.MSG_JBK_InAndTest:
                        if (msg.result)
                        {
                            try
                            {
                                msg.result = false;
                                byte[] dt = msg.datas[0] != null ? msg.datas[0] : msg.datas[1];
                                int len = dt.Length / 2, index = 0;
                                double[] data = new double[len];
                                for (int i = 0; i < len; i++)
                                {
                                    data[i] = dt[index + 1] * 256 + dt[index];
                                    index += 2;
                                }

                                double[] a = new double[data.Length];
                                double[] b = new double[data.Length];
                                for (int i = 0; i < data.Length; i++)
                                {
                                    a[i] = data[i];
                                    b[i] = 0;
                                }
                                MyFFT.fft(data.Length, a, b, 1);
                                int ij = data.Length / 6;
                                int ii = data.Length - ij;
                                for (int j = ij; j < ii; j++)
                                {
                                    a[j] = 0;
                                    b[j] = 0;
                                }
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
                                double[] val = DyUtils.dyMath(data);
                                wnd.datas.Add(val[1]);

                                if (wnd.datas.Count == 10)
                                {
                                    double min = wnd.datas[0], max = wnd.datas[0], sum = 0, avg = 0;
                                    for (int i = 0; i < wnd.datas.Count; i++)
                                    {
                                        sum += wnd.datas[i];
                                        if (wnd.datas[i] < min) min = wnd.datas[i];
                                        if (wnd.datas[i] > max) max = wnd.datas[i];
                                    }

                                    //去掉一个最大值、一个最小值
                                    sum = sum - max - min;
                                    avg = sum / 8.0;
                                    max = avg + 0.015;
                                    min = avg - 0.015;
                                    //min = min < 0.01 ? (min >= 0.005 ? 0.01 : min + 0.005) : min;

                                    //2018年5月18日 如果最小值小于0.02
                                    min = min < 0.02 ? 0.02 : min;

                                    wnd.IsTest = false;
                                    wnd.btn_Hole1Test.Content = "通道1临界值";
                                    wnd.btn_Hole2Test.Content = "通道2临界值";
                                    List<DYJTJItemPara> jtjItems = Global.jtjItems;
                                    if (jtjItems != null && jtjItems.Count > 0)
                                    {
                                        DYJTJItemPara item = null;
                                        for (int i = 0; i < jtjItems.Count; i++)
                                        {
                                            item = Global.jtjItems[i];
                                            if (item.Method == 0)//定性消线
                                            {
                                                if (item.dxxx.newMaxT == null) item.dxxx.newMaxT = new double[2];
                                                if (item.dxxx.newMinT == null) item.dxxx.newMinT = new double[2];

                                                if (wnd.TestHole == 1)
                                                {
                                                    item.dxxx.newMaxT[0] = Math.Round(max, 4);
                                                    item.dxxx.newMinT[0] = Math.Round(min, 4);
                                                }
                                                else
                                                {
                                                    item.dxxx.newMaxT[1] = Math.Round(max, 4);
                                                    item.dxxx.newMinT[1] = Math.Round(min, 4);
                                                }
                                            }
                                        }

                                        Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                                    }
                                    MessageBox.Show(string.Format("已为您自动设置Min：{0}；Max：{1} \r\n\r\nTips：仅限胶体金定性消线法[通道 {2}]", min, max, wnd.TestHole));
                                    return;
                                }

                                wnd.JbkOut(msg.str1);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;

                    case MsgCode.MSG_SXT_UPDATE:
                        if (msg.result)
                        {
                            MessageBox.Show(wnd, "胶体金3.0固件升级成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "胶体金3.0固件升级失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case MsgCode.MSG_JBK_CKC:
                        if (msg.result)
                        {
                            wnd.jtjCalibration(msg.str1);
                        }
                        else
                        {
                            MessageBox.Show(wnd, "请放置校准金标卡！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                            MessageBox.Show(wnd, string.Format("数据下载失败!\r\n\r\n异常信息：{0}", msg.errMsg), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.changeCehckSync(true, "全部数据下载", 20);
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
                            MessageBox.Show(wnd, string.Format("被检单位下载失败!\r\n\r\n异常信息：{0}", msg.errMsg), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        wnd.changeCompany(true, "被检单位下载");
                        break;
                    case MsgCode.MSG_DownCheckItems:
                        if (!string.IsNullOrEmpty(msg.CheckItemsTempList))
                        {
                            try
                            {
                                wnd.UpdateStatus(msg.CheckItemsTempList);
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                Console.WriteLine(e.Message);
                                wnd.changeCheckItems(true, "检测项目下载");
                                MessageBox.Show("下载失败！请联系管理员！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            finally
                            {
                                wnd.changeCheckItems(true, "检测项目下载");
                                if (_checkedDown)
                                {
                                    MessageBox.Show("检测项目下载成功！共下载：" + wnd._yqcheckItemsNum + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                        else
                        {
                            wnd.changeCheckItems(true, "检测项目下载");
                            MessageBox.Show("下载数据错误,或者服务链接不正常，请联系管理员！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
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
            _msg = new Message
            {
                what = MsgCode.MSG_COMM_CABT,
                str1 = port,
                calibrationValue = calibrationValue
            };
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
            _msg = new Message
            {
                what = MsgCode.MSG_COMM_TEST,
                str1 = port
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void jbkTest(string port)
        {
            _msgThread.Stop();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
            _msg = new Message
            {
                what = MsgCode.MSG_JBK_TEST,
                str1 = port
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        /// <summary>
        /// 胶体金校准
        /// </summary>
        private void jtjCalibration(string port)
        {
            _msg = new Message
            {
                what = MsgCode.MSG_JBK_CBT,
                str1 = port
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }


        /// <summary>
        /// 打印机通讯测试
        /// </summary>
        /// <param name="port"></param>
        private void TestP(string port)
        {
            _msg = new Message
            {
                what = MsgCode.MSG_COMP_TEST,
                str1 = port
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void Test(string port)
        {
            _msg = new Message
            {
                what = MsgCode.MSG_COMM_TEST,
                str1 = port
            };
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
                Label labelHoleSetting = new Label
                {
                    Width = 140,
                    Height = 40,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Content = String.Format("检测孔{0:D2}配置:", (nHole + 1)),
                    FontSize = 20
                };
                stackPanel.Children.Add(labelHoleSetting);
                for (int i = 0; i < Global.deviceHole.LedCount; ++i)
                {
                    Label labelLEDWave = new Label
                    {
                        Width = 50,
                        Height = 40,
                        Margin = new Thickness(10, 0, 0, 0),
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                        Content = "LED" + (i + 1),
                        FontSize = 20
                    };
                    ComboBox comboBoxLEDWave = new ComboBox
                    {
                        Name = GetName(nHole, i),
                        Width = 80,
                        Height = 40,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                        FontSize = 20,
                        ItemsSource = DeviceProp.DeviceHole.TotalWaves,
                        IsEditable = true,
                        Text = "" + Global.deviceHole.LEDWave[nHole][i]
                    };
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
            _msg = new Message
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 = textBoxServerAddr.Text,
                str2 = textBoxRegisterID.Text,
                str3 = textBoxRegisterPassword.Password
            };
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
                _msg = new Message
                {
                    what = MsgCode.MSG_CHECK_SYNC,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
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
                clsttStandardDecide model = new clsttStandardDecide
                {
                    UDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
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
            PercentProcess process = new PercentProcess
            {
                BackgroundWork = this.DownStandardDecideTempProcess,
                MessageInfo = "正在下载样品判定标准"
            };
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
                _msg = new Message
                {
                    what = MsgCode.MSG_DownCompany,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
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
            PercentProcess process = new PercentProcess
            {
                BackgroundWork = this.DownLoadCheckItemsProcess,
                MessageInfo = "正在下载检测项目"
            };
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
            PercentProcess process = new PercentProcess
            {
                BackgroundWork = this.DownloadCompanyProcess,
                MessageInfo = "通讯成功，正在下载被检单位"
            };
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
            PercentProcess process = new PercentProcess
            {
                BackgroundWork = this.DownStandardProcess,
                MessageInfo = "正在下载检测标准"
            };
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
            PercentProcess process = new PercentProcess
            {
                BackgroundWork = this.DownCheckItemsProcess,
                MessageInfo = "正在下载检测项目标准"
            };
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
                _msg = new Message
                {
                    what = MsgCode.MSG_DownCheckItems,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
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
                if (data != null && !data.Equals("<NewDataSet>\r\n没有相关的数据下载!\r\n</NewDataSet>"))
                {
                    _yqcheckItemsNum = 0;
                    MainWindow._TempItemNames = _serialArry;
                    using (StringReader sr = new StringReader(data))
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.ReadXml(sr);
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
                            }
                        }
                    }
                }
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
                DYFGDItemPara item = new DYFGDItemPara
                {
                    Name = ArryTemp[0].ToString(),//项目名称
                    Unit = ArryTemp[3].ToString(),//项目检测单位
                    HintStr = ArryTemp[4].ToString(),//简要操作:新添加实验--注释
                    Password = ArryTemp[5].ToString()//密码
                };
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
                DYJTJItemPara item = new DYJTJItemPara
                {
                    Name = ArryTemp[0].ToString(),//项目名称
                    Unit = ArryTemp[3].ToString(),//项目检测单位
                    HintStr = ArryTemp[4].ToString(),//简要操作：新添加实验--注释
                    Password = ArryTemp[5].ToString()//密码
                };
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
                DYGSZItemPara item = new DYGSZItemPara
                {
                    Name = ArryTemp[0].ToString(),//项目名称
                    Unit = ArryTemp[3].ToString(),//项目检测单位
                    HintStr = ArryTemp[4].ToString(),//简要提示:新添加实验--注释
                    Password = ArryTemp[5].ToString()//密码
                };
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
                DYHMItemPara item = new DYHMItemPara
                {
                    Name = ArryTemp[0].ToString(),//项目名称
                    Unit = ArryTemp[3].ToString(),//项目检测单位
                    HintStr = ArryTemp[4].ToString(),//简要操作：新添加实验--注释
                    Password = ArryTemp[5].ToString()//密码
                };
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
            ButtonHMPortTest.Content = "正在测试";
            ButtonHMPortTest.IsEnabled = false;
            _msg = new Message
            {
                what = MsgCode.MSG_COMM_TEST_HM,
                str1 = ComboBoxHMPort.Text
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        private void BtnCalibration_Click(object sender, RoutedEventArgs e)
        {
            bool isStart = true;
            tb_Values.IsEnabled = BtnCalibration.IsEnabled = false;
            string str = tb_Values.Text.Trim();
            try
            {
                if (str.Equals(""))
                {
                    MessageBox.Show("请输入校准值(0~500)！");
                    tb_Values.Focus();
                    isStart = false;
                }
                else if (int.Parse(str) < 0 || int.Parse(str) > 500)
                {
                    MessageBox.Show("超出校准值范围！请输入:0~500！");
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
                CalibrationAD(ComboBoxADPort.Text, str);
            }
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
            Message msg1 = new Message
            {
                what = MsgCode.MSG_JBK_CKC,
                str1 = port
            };
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        /// <summary>
        /// 胶体金校准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_JtjJZ_Click(object sender, RoutedEventArgs e)
        {
            Message msg1 = new Message
            {
                what = MsgCode.MSG_JBK_OUT,
                str1 = ComboBoxSXT1Port.Text.Trim()
            };
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        private void btn_JtjJZ2_Click(object sender, RoutedEventArgs e)
        {
            Message msg1 = new Message
            {
                what = MsgCode.MSG_JBK_OUT,
                str1 = ComboBoxSXT2Port.Text.Trim()
            };
            Global.workThread.SendMessage(msg1, _msgThread);
        }

        private void btn_autoUpdater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Global.IsConnectInternet())
                {
                    MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");
                //标记分光在用
                CFGUtils.SaveConfig("FgIsStart", "1");
                //杀掉电池应用
                Process[] ps = Process.GetProcesses();
                for (int i = 0; i < ps.Length; i++)
                {
                    if (ps[i].ProcessName.StartsWith("BatteryManage"))
                    {
                        ps[i].Kill();
                        break;
                    }
                }
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
            Global.Mac = CbAhMac.Text;
        }

        private TimerWindow TimerWindow;
        private void loadTimerWindow(bool isLoad)
        {
            try
            {
                if (isLoad)
                {
                    TimerWindow = new TimerWindow
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
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
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                {
                    interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                    userName = Global.AnHuiInterface.userName,
                    instrument = Global.AnHuiInterface.instrument,
                    passWord = Global.AnHuiInterface.passWord
                };
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.Mac;
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
                            clsCompany company = new clsCompany
                            {
                                FullName = item.unitName,
                                SysCode = Global.GETGUID(null),
                                StdCode = item.id,
                                CAllow = item.bussinessId,
                                CompanyID = item.id,
                                ShortName = item.unitName,
                                DisplayName = item.unitName,
                                Property = item.busScope,
                                Address = item.address,
                                LinkMan = item.linkName,
                                LinkInfo = item.tel
                            };
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
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                {
                    interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                    userName = Global.AnHuiInterface.userName,
                    instrument = Global.AnHuiInterface.instrument,
                    passWord = Global.AnHuiInterface.passWord
                };
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.Mac;
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
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                {
                    interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                    userName = Global.AnHuiInterface.userName,
                    instrument = Global.AnHuiInterface.instrument,
                    passWord = Global.AnHuiInterface.passWord
                };
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.Mac;
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
                            clsCompany company = new clsCompany
                            {
                                FullName = item.unitName,
                                SysCode = DateTime.Now.Millisecond.ToString(),
                                StdCode = item.id,
                                CAllow = item.bussinessId,
                                CompanyID = item.id,
                                ShortName = item.unitName,
                                DisplayName = item.unitName,
                                Property = item.busScope,
                                Address = item.address,
                                LinkMan = item.linkName,
                                LinkInfo = item.tel
                            };
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
            Global.DeviceID = val;

            if (MessageBox.Show(string.Format("设备唯一码[{0}] 绑定后将可能无法更改！\r\n\r\n确定信息无误，立即绑定设备唯一码？",
                Global.DeviceID), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            CFGUtils.SaveConfig("DeviceId", Global.DeviceID);
            CFGUtils.SaveConfig("USER", Wisdom.USER);
            CFGUtils.SaveConfig("PASSWORD", Wisdom.PASSWORD);
            MessageBox.Show(this, "服务器链接信息绑定成功!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            TxtZhDeviceId.IsReadOnly = true;
        }

        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VersionInfo_Click(object sender, RoutedEventArgs e)
        {
            VersionInfo window = new VersionInfo
            {
                ShowInTaskbar = true,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 是否正在测试临界值
        /// </summary>
        private bool IsTest = false;

        private bool IsCard = false;

        private int TestHole = 0;
        /// <summary>
        /// 存储20次的T值
        /// </summary>
        private List<double> datas = null;


        private void JbkInAndTest(string port)
        {
            IsTest = true;
            Message msg = new Message
            {
                what = MsgCode.MSG_JBK_InAndTest,
                str1 = port,
                ports = new List<string>
                {
                    port,
                    null
                }
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        private void JbkOut(string port)
        {
            if (TestHole == 1)
            {
                btn_Hole1Test.Content = string.Format("第{0}次测试", datas.Count + 1);
            }
            else
            {
                btn_Hole2Test.Content = string.Format("第{0}次测试", datas.Count + 1);
            }
            Message msg = new Message
            {
                what = MsgCode.MSG_JBK_OUT,
                str1 = port,
                ports = new List<string>()
            };
            msg.ports.Add(port);
            msg.ports.Add(null);
            Global.workThread.SendMessage(msg, _msgThread);
        }

        /// <summary>
        /// 自动获取通道临界值1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Hole1Test_Click(object sender, RoutedEventArgs e)
        {
            if (IsTest)
            {
                MessageBox.Show("正在处理···");
                return;
            }
            IsCard = true;
            TestHole = 1;
            IsTest = true;
            datas = new List<double>();
            JbkOut(ComboBoxSXT1Port.Text);
        }

        /// <summary>
        /// 自动获取通道临界值2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Hole2Test_Click(object sender, RoutedEventArgs e)
        {
            if (IsTest)
            {
                MessageBox.Show("正在处理···");
                return;
            }
            IsCard = true;
            TestHole = 2;
            IsTest = true;
            datas = new List<double>();
            JbkOut(ComboBoxSXT2Port.Text);
        }

        private void btn_printSetting_Click(object sender, RoutedEventArgs e)
        {
            PrintPreview window = new PrintPreview
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 快件服务通讯通讯测试（用户登录）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonKjServerTest_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Global.KjServer.KjServerAddr = textBoxKjServerAddr.Text;
            buttonKjServerTest.Content = "正在测试";
            buttonKjServerTest.IsEnabled = false;
            _msg = new Message
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 =textBoxKjServerAddr.Text.Trim(),
                str2 = textBoxKjuser_name.Text,
                str3 = textBoxKjpassword.Password.Trim(),
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        /// <summary>
        /// 仪器注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKjregisterDevice_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Global.KjServer.Kjuser_name = textBoxKjuser_name.Text.Trim();
            Global.KjServer.Kjpassword = textBoxKjpassword.Password.Trim();
            DyInterfaceHelper.KjService.Url_Server = textBoxKjServerAddr.Text.Trim();
            btnKjregisterDevice.Content = "正在注册";
            btnKjregisterDevice.IsEnabled = false;
            Global.MachineModel = textBoxKjseries.Text.Trim();////仪器型号

            _msg = new Message
            {
                what = MsgCode.MSG_REGISTERDEVICE,
                //str1 = textBoxKjseries.Text,//仪器型号
                //str2 = CbKjMac.Text//MAC地址
                str1 = textBoxKjServerAddr.Text.Trim(),
                //str2 = textBoxKjuser_name.Text,
                //str3 = textBoxKjpassword.Password.Trim(),
            };
            Global.workThread.SendMessage(_msg, _msgThread);

        }

        #region 胶体金3.0固件升级
        /// <summary>
        /// 胶体金3.0固件升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_jtjFirmwareUpdate1_Click(object sender, RoutedEventArgs e)
        {
            //暂时使用本地固件，以后配合平台更新为网络源固件
            string filePath = FilePath();
            if (filePath.Length == 0) return;

            byte[] fileDatas = IOData(filePath);
            if (fileDatas != null)
            {
                //还需要优化为同时升级两个模块
                _msg = new Message
                {
                    what = MsgCode.MSG_SXT_UPDATE,
                    str1 = ComboBoxSXT1Port.Text.Trim(),
                    obj1 = fileDatas
                };
                Global.workThread.SendMessage(_msg, _msgThread);
            }
        }

        /// <summary>
        /// 胶体金3.0固件升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_jtjFirmwareUpdate2_Click(object sender, RoutedEventArgs e)
        {
            //暂时使用本地固件，以后配合平台更新为网络源固件
            string filePath = FilePath();
            if (filePath.Length == 0) return;

            byte[] fileDatas = IOData(filePath);
            if (fileDatas != null)
            {
                //还需要优化为同时升级两个模块
                _msg = new Message
                {
                    what = MsgCode.MSG_SXT_UPDATE,
                    str1 = ComboBoxSXT2Port.Text.Trim(),
                    obj1 = fileDatas
                };
                Global.workThread.SendMessage(_msg, _msgThread);
            }
        }

        private string FilePath()
        {
            string path = string.Empty;
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Files (*.bin)|*.bin"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

        private byte[] IOData(string path)
        {
            try
            {
                FileStream stream = new FileInfo(path).OpenRead();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                return buffer;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        #endregion

        private void Btn_DataBaseRepair_Click(object sender, RoutedEventArgs e)
        {
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.RepairProjects,
                MessageInfo = "程序正在自检，请稍后"
            };
            process.Start();
        }
        private void RepairProjects(Action<int> percent)
        {
            percent(0);
            try
            {
                string err = string.Empty;
                tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
                List<string> sqlList = new List<string>();
                //ttResultSecond
                sqlList.Add("alter table ttResultSecond add SysCode varchar(255)");
                sqlList.Add("alter table ttResultSecond add shoudong varchar(255)");
                sqlList.Add("alter table ttResultSecond add taskid varchar(255)");
                sqlList.Add("alter table ttResultSecond add Sdid  varchar(255)");
                sqlList.Add("alter table ttResultSecond add OperatorName  varchar(255)");
                sqlList.Add("alter table ttResultSecond add SampleFoodCode varchar(255)");
                sqlList.Add("alter table ttResultSecond add CKCKNAMEUSID  varchar(255)");
                sqlList.Add("alter table ttResultSecond add ProduceCompany  varchar(255)");
                sqlList.Add("alter table ttResultSecond add SampleId varchar(255)");
                sqlList.Add("alter table ttResultSecond add DeviceId varchar(255)");
                sqlList.Add("alter table ttResultSecond add ContrastValue varchar(255)");
                sqlList.Add("alter table ttResultSecond add IsReport varchar(255)");
                sqlList.Add("alter table ttResultSecond add IsShow varchar(255)");
                sqlList.Add("alter table ttResultSecond add IsUpload varchar(255)");
                sqlList.Add("alter table ttResultSecond add SampleId varchar(255)");
                sqlList.Add("alter table ttResultSecond add ProduceCompany varchar(255)");
                sqlList.Add("alter table ttResultSecond add CKCKNAMEUSID varchar(255)");
                sqlList.Add("alter table ttResultSecond add FoodType varchar(255)");
                sqlList.Add("alter table ttResultSecond add OperatorID varchar(255)");//经营户ID

                //KTask
                sqlList.Add("alter table KTask add dataType varchar(255)");
                sqlList.Add("alter table KTask add IsReceive varchar(255)");
                sqlList.Add("alter table KTask add UserName varchar(255)");
                sqlList.Add("alter table KTask add Checktype varchar(255)");
                sqlList.Add("alter table KTask add mokuai varchar(255)");
                sqlList.Add("alter table KTask add td_remark varchar(255)");

                percent(1);
                int len = sqlList.Count;

                float percentage1 = (float)99 / (float)len, percentage2 = 0;
                int count = (int)percentage1 + 1;

                for (int i = 0; i < sqlList.Count; i++)
                {
                    bll.DataBaseRepair(sqlList[i], out err);
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
            catch (Exception)
            {
                percent(100);
            }
            finally
            {
                percent(100);
                MessageBox.Show("程序自检成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

    }
}