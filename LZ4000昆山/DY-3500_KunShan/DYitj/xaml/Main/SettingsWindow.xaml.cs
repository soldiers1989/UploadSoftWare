﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AIO.src;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;
using DYSeriesDataSet.DataSentence.Kjc;

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
        public Boolean DeviceIdisNull = true;
        private string CompanyTemp = string.Empty, CheckItems = string.Empty, Standard = string.Empty,
            chekcItmes = string.Empty, StandardDecideTemp = string.Empty;
        /// <summary>
        /// 服务器通讯测试
        /// </summary>
        private bool IS_SERVICE_TEST = false;
        #endregion

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.EachDistrict.Equals("GS"))
                {
                    btnCheckItems.Visibility = btnCompany.Visibility = Visibility.Collapsed;
                    buttonCehckSync.Content = "国家检测标准下载";
                    buttonCehckSync.Width = 200;
                    buttonCehckSync.Margin = new Thickness(265, 0, 0, 0);
                }

                if (!Global.set_IsOpenFgd)
                {
                    this.skpFgdTop.Height = 0;
                    this.skpFgdC.Height = 0;
                }
                if (!Global.set_IsOpenZjs)
                    this.skpZjs.Height = 0;
                if (!Global.set_FaultDetection)
                    this.BtnCheckError.Visibility = Visibility.Collapsed;
                if (!Global.set_ShowFgd)
                    this.skpFgdC.Height = this.StackPanelLEDSettings.Height = 0;
                //配置文件处摄像头个数一般为4或2，若为其他数则视为全部摄像头都显示
                if (Global.deviceHole.SxtCount == 2)
                {
                    this.skpSxtLine2.Height = 0;
                }
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
                    textBoxServerAddr.Text = CPoint.url;
                    textBoxRegisterID.Text = CPoint.user;
                    textBoxRegisterPassword.Password = CPoint.pwd;
                    textBoxCheckNumber.Text = CPoint.pointNum;
                    textBoxCheckName.Text = CPoint.pointName;
                    textBoxCheckType.Text = CPoint.pointType;
                    textBoxCheckOrg.Text = CPoint.orgName;
                    textCheckPlaceCode.Text = CPoint.orgNum;
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
                _msgThread = new MsgThread(this);
                _msgThread.Start();

                if (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL"))
                {
                    ZhServiceLayer.Visibility = Visibility.Visible;
                    if (Global.InterfaceType.Equals("ZH")) DyServiceLayer.Visibility = Visibility.Collapsed;
                    //唯一机器码为空时可保存
                    //btn_SaveDeviceId.IsEnabled = Wisdom.DeviceID.Equals(string.Empty) ? true : false;
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
                else
                    ZhServiceLayer.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(Window_Loaded):\n" + ex.Message);
            }
            finally 
            {
                txt_KsUser.Text = Global.KsUser;
                txt_KsPwd.Password = Global.KsPwd;
                txt_KsMarketName.Text = Global.KsMarketName;
                txt_KsMarketCode.Text = Global.KsMarketCode;
                txt_KsJCSN.Text = Global.KsJCSN;
                if (Global.KsVersion.Equals("1"))
                {
                    btnQuerySignContact.IsEnabled = false;
                }
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
                Global.samplenameadapter = Global.samplenameadapter ?? new List<CheckPointInfo>();
                CPoint = Global.samplenameadapter.Count == 0 ? new CheckPointInfo() : Global.samplenameadapter[0];
                CPoint.url = textBoxServerAddr.Text;
                CPoint.user = textBoxRegisterID.Text;
                CPoint.pwd = textBoxRegisterPassword.Password;
                CPoint.pointNum = textBoxCheckNumber.Text;
                CPoint.pointName = textBoxCheckName.Text;
                CPoint.pointType = textBoxCheckType.Text;
                CPoint.orgName = textBoxCheckOrg.Text;
                CPoint.orgNum = textCheckPlaceCode.Text;
                CPoint.nickName = textBoxRegisterID.Text.Trim();
                CPoint.userId = Global.userId;
                CPoint.pointId = Global.pointId;
                if (Global.samplenameadapter.Count == 0)
                    Global.samplenameadapter.Add(CPoint);
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

                //昆山项目相关
                Global.KsUser = txt_KsUser.Text.Trim();
                Global.KsPwd = txt_KsPwd.Password.Trim();
                Global.KsMarketName = txt_KsMarketName.Text.Trim();
                Global.KsMarketCode = txt_KsMarketCode.Text.Trim();
                Global.KsJCSN = txt_KsJCSN.Text.Trim();
                CFGUtils.SaveConfig("KsUser", Global.KsUser);
                CFGUtils.SaveConfig("KsPwd", Global.KsPwd);
                CFGUtils.SaveConfig("KsMarketName", Global.KsMarketName);
                CFGUtils.SaveConfig("KsMarketCode", Global.KsMarketCode);
                CFGUtils.SaveConfig("KsJCSN", Global.KsJCSN);
                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(SaveSetting):\n" + ex.Message);
            }
        }

        private void ComboBoxPort_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            try
            {
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
                MessageBox.Show(this, "异常(ComboBoxPort_DropDownOpened):\n" + ex.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            //new MainWindow().Task();
            this.Close();
        }

        private void ComboBoxADPort_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strADPORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("ADPORT", Global.strADPORT);
        }

        private void ComboBoxSXT1Port_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strSXT1PORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("SXT1PORT", Global.strSXT1PORT);
        }

        private void ComboBoxSXT2Port_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strSXT2PORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("SXT2PORT", Global.strSXT2PORT);
        }

        private void ComboBoxSXT3Port_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strSXT3PORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("SXT3PORT", Global.strSXT3PORT);
        }

        private void ComboBoxSXT4Port_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strSXT4PORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("SXT4PORT", Global.strSXT4PORT);
        }

        private void ComboBoxPRINTPort_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBoxPort = (ComboBox)sender;
            Global.strPRINTPORT = comboBoxPort.Text;
            CFGUtils.SaveConfig("PRINTPORT", Global.strPRINTPORT);
        }

        private void ButtonADPortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonADPortTest.Content = "正在测试";
            ButtonADPortTest.IsEnabled = false;
            Test(ComboBoxADPort.Text);
        }

        private void ButtonSXT1PortTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT1PortTest.Content = "正在测试";
            ButtonSXT1PortTest.IsEnabled = false;
            Test(ComboBoxSXT1Port.Text);
        }

        private void ButtonSXT2PorTest_Click(object sender, RoutedEventArgs e)
        {
            ButtonSXT2PorTest.Content = "正在测试";
            ButtonSXT2PorTest.IsEnabled = false;
            Test(ComboBoxSXT2Port.Text);
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
            Test(ComboBoxPRINTPort.Text);
        }

        //通讯测试后还原按钮
        private void IsEnabledTrue()
        {
            if (IS_SERVICE_TEST)
            {
                this.textCheckPlaceCode.Text = Global.orgNum;
                this.textBoxCheckNumber.Text = Global.pointNum;
                this.textBoxCheckName.Text = Global.pointName;
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
        }

        private void ButtonShowAD_Click(object sender, RoutedEventArgs e)
        {
            FgdShowADWindow window = new FgdShowADWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        class MsgThread : ChildThread
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            SettingsWindow settingWindow = new SettingsWindow();
            public void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_COMM_TEST:
                    case MsgCode.MSG_COMM_TEST_HM:
                        if (msg.result)
                        {
                            wnd.IsEnabledTrue();
                            wnd.SaveSetting();
                            MessageBox.Show(wnd, "测试连接成功!", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            wnd.IsEnabledTrue();
                            MessageBox.Show(wnd, "测试连接失败!请重试!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    //连接成功后，下载被检单位
                    case MsgCode.MSG_CHECK_CONNECTION:
                        wnd.IS_SERVICE_TEST = true;
                        if (msg.result)
                        {
                            wnd.IsEnabledTrue();
                            wnd.SaveSetting();
                            //wnd.BtnCompany_Click(null, null);
                            Global.IsServerTest = false;
                            MessageBox.Show(wnd, "测试连接成功!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            Global.samplenameadapter = null;
                            wnd.IsEnabledTrue();
                            Global.IsServerTest = true;
                            MessageBox.Show(wnd, "测试连接失败!\r\n\r\n异常提示：" + msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    case MsgCode.MSG_CHECK_SYNC:
                        try
                        {
                            if (Global.InterfaceType.Equals("DY"))
                            {
                                wnd.DownStandard(msg.Standard);
                                wnd.DownCheckItems(msg.CheckItems);
                                wnd.DownloadCompany(msg.DownLoadCompany);
                                wnd.DownloadStandDecide(msg.SampleStandardName);
                            }
                            else if (Global.InterfaceType.Equals("KJC"))
                            {
                                wnd.KjcDownloadCompany(msg.DownLoadCompany);
                            }
                        }
                        catch (Exception e)
                        {
                            _checkedDown = false;
                            Console.WriteLine(e.Message);
                            wnd.ChangeCehckSync(true, "全部数据下载", 20);
                            MessageBox.Show(wnd, "下载失败!请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        finally
                        {
                            wnd.ChangeCehckSync(true, "全部数据下载", 20);
                            if (_checkedDown)
                            {
                                MessageBox.Show(wnd, "全部数据下载成功！本次共下载：" +
                                    (wnd._DownCheckItemCount + wnd._DownStandardCount +
                                    wnd._YQCheckItemCount + wnd._DownCompanyCount +
                                    wnd._DownStandDecideCount) + "条数据！\r\n\r\n" +
                                    "检测标准：" + wnd._DownStandardCount + "条！\r\n" +
                                    "样品标准：" + wnd._DownStandDecideCount + "条！\r\n" +
                                    "检测项目：" + wnd._YQCheckItemCount + "条！\r\n" +
                                    "被检单位：" + wnd._DownCompanyCount + "条！\r\n" +
                                    "检测项目标准：" + wnd._DownCheckItemCount + "条！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                        }
                        break;
                    case MsgCode.MSG_DownCompany:
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
                                wnd.ChangeCompany(true, "被检单位下载");
                                MessageBox.Show(wnd, "下载时出现异常！\r\n异常信息：" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            finally
                            {
                                if (_checkedDown)
                                {
                                    wnd.ChangeCompany(true, "被检单位下载");
                                    MessageBox.Show(wnd, "通讯成功！\r\n\r\n且成功同步" + wnd._DownCompanyCount + " 条被检单位数据!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                        else
                        {
                            wnd.ChangeCompany(true, "被检单位下载");
                            MessageBox.Show(wnd, "下载数据错误,或者服务链接不正常，请联系管理员!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    case MsgCode.MSG_DownCheckItems:
                        if (!string.IsNullOrEmpty(msg.CheckItemsTempList))
                        {
                            try
                            {
                                wnd.DownLoadCheckItems(msg.CheckItemsTempList);
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                Console.WriteLine(e.Message);
                                wnd.ChangeCheckItems(true, "检测项目下载");
                                MessageBox.Show(wnd, "下载失败!请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            finally
                            {
                                wnd.ChangeCheckItems(true, "检测项目下载");
                                if (_checkedDown)
                                {
                                    string str = wnd._YQCheckItemCount > 0 ?
                                        string.Format("检测项目下载成功!\r\n\r\n本次共下载：{0} 条数据!", wnd._YQCheckItemCount) :
                                        "暂时没有可下载的数据！";
                                    MessageBox.Show(wnd, str, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                        else
                        {
                            wnd.ChangeCheckItems(true, "检测项目下载");
                            MessageBox.Show(wnd, "下载数据错误,或者服务链接不正常，请联系管理员!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case MsgCode.KS_QUERYMARKET:
                        wnd.SettingKsInfo(msg.outError);
                        break;

                    case MsgCode.KS_QuerySignContact:
                        if (msg.result)
                        {
                            PercentProcess process = new PercentProcess()
                            {
                                BackgroundWork = wnd.DownSignContact,
                                MessageInfo = "正在同步经营户主体信息"
                            };
                            process.Start();
                        }
                        else
                        {
                            MessageBox.Show("经营户主体信息下载失败！\r\n异常信息：" + msg.outError, "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        }
                        break;

                        //单位主体信息
                    case MsgCode.KS_GetAreaMarket:
                        if (msg.result)
                        {
                            try
                            {
                                List<KunShanEntity.AreaMarket> models = JsonHelper.JsonToEntity<List<KunShanEntity.AreaMarket>>(KunShanHelper.AreaMarket);
                                if (models != null && models.Count > 0)
                                {
                                    wnd.QuerySignContactByAM(models);
                                }
                                else
                                {
                                    MessageBox.Show(KunShanHelper.AreaMarket + "\r\n请确保输入的用户名与密码无误！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                                }
                            }
                            catch (Exception)
                            {
                                 MessageBox.Show(KunShanHelper.AreaMarket + "\r\n请确保输入的用户名与密码无误！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                            }
                        }
                        else
                        {
                            MessageBox.Show("单位主体信息下载失败！\r\n异常信息：" + msg.outError, "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        }
                        break;
                        
                        //主体信息对应经营户信息
                    case MsgCode.KS_QuerySignContactByAM:
                        if (msg.result)
                        {
                            wnd._models = msg.obj1 as List<KunShanEntity.AreaMarket>;
                            wnd._vals = msg.obj2 as string[,];
                            PercentProcess process = new PercentProcess()
                            {
                                BackgroundWork = wnd.DownAreaMarket,
                                MessageInfo = "正在同步单位主体信息"
                            };
                            process.Start();
                        }
                        else
                        {
                            MessageBox.Show("单位主体信息下载失败！\r\n异常信息：" + msg.outError, "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        }
                        break;

                    case MsgCode.KS_QueryCheckItem:
                        if (msg.result)
                        {
                            PercentProcess process = new PercentProcess()
                            {
                                BackgroundWork = wnd.DownCheckItem,
                                MessageInfo = "正在同步检测项目信息"
                            };
                            process.Start();
                        }
                        else
                        {
                            MessageBox.Show("检测项目信息下载失败！\r\n异常信息：" + msg.outError, "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        }
                        break;

                    case MsgCode.KS_QuerySalesItem:
                        if (msg.result)
                        {
                            PercentProcess process = new PercentProcess()
                            {
                                BackgroundWork = wnd.DownSalesItem,
                                MessageInfo = "正在同步检测品种信息"
                            };
                            process.Start();
                        }
                        else
                        {
                            MessageBox.Show("检测品种信息下载失败！\r\n异常信息：" + msg.outError, "下载提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
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
        private void ChangeCheckItems(bool b, string str)
        {
            btnCheckItems.Content = str;
            btnCheckItems.IsEnabled = b;
        }

        /// <summary>
        /// 被检单位下载
        /// </summary>
        /// <param name="b"></param>
        /// <param name="str"></param>
        private void ChangeCompany(bool b, string str)
        {
            btnCompany.IsEnabled = b;
            btnCompany.Content = str;
        }

        private void ChangeCehckSync(bool b, string str, int size)
        {
            buttonCehckSync.Content = Global.EachDistrict.Equals("GS") ? "国家检测标准下载" : str;
            buttonCehckSync.IsEnabled = b;
            buttonCehckSync.FontSize = size;
        }

        private void Test(string port)
        {
            Message msg = new Message()
            {
                what = MsgCode.MSG_COMM_TEST,
                str1 = port
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        // 根据已有的检测孔的波长信息，生成检测孔波长配置。
        private UIElement GenerateHoleLEDSettingUI(int nHole)
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            };
            Label labelHoleSetting = new Label()
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
                Label labelLEDWave = new Label()
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
                ComboBox comboBoxLEDWave = new ComboBox()
                {
                    Name = GetName(nHole, i),
                    Width = 80,
                    Height = 40,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    FontSize = 20,
                    ItemsSource = DeviceProp.DeviceHole.TotalWaves,
                    IsEditable = true,
                    Text = string.Empty + Global.deviceHole.LEDWave[nHole][i]
                };
                stackPanel.Children.Add(labelLEDWave);
                stackPanel.Children.Add(comboBoxLEDWave);
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

        // 寻找所有的combobox，按名称，将其内容保存。
        private void SaveHoleWaveSettings()
        {
            List<ComboBox> comboBoxes = UIUtils.GetChildObjects<ComboBox>(StackPanelLEDSettings, typeof(ComboBox));
            if (null == comboBoxes)
                return;
            foreach (ComboBox comboBox in comboBoxes)
            {
                int nHole = 0, nLed = 0;
                GetLED(out nHole, out  nLed, comboBox.Name);
                try
                {
                    Int32.TryParse(comboBox.Text, out Global.deviceHole.LEDWave[nHole][nLed]);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    FileUtils.Log(ex.ToString());
                }
            }
        }

        private void ButtonServerTest_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Global.samplenameadapter != null && Global.samplenameadapter.Count > 0)
            {
                if (!Global.samplenameadapter[0].user.Equals(textBoxRegisterID.Text.Trim()))
                {
                    if (MessageBox.Show(string.Format("账号更换重要提醒：\r\n\r\n系统检测到您将更换服务器账号[{0}]为[{1}]！\r\n{2}",
                        Global.samplenameadapter[0].user, textBoxRegisterID.Text.Trim(),
                        "更换账号后将可能无法使用新账号上传历史数据，建议先确认当前账号是否还有历史数据未上传！\r\n\r\n是否继续服务器通讯测试？"), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }
            }

            textBoxCheckNumber.Text=string.Empty;
            textBoxCheckName.Text = string.Empty;
            textBoxCheckType.Text = string.Empty;
            textBoxCheckOrg.Text = string.Empty;
            textCheckPlaceCode.Text = string.Empty;

            buttonServerTest.Content = "正在测试";
            buttonServerTest.IsEnabled = false;
            Message msg = new Message()
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 = textBoxServerAddr.Text,
                str2 = textBoxRegisterID.Text,
                str3 = textBoxRegisterPassword.Password
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        /// <summary>
        /// 全部数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonCehckSync_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ChangeCehckSync(false, "正在下载···", 20);
                _checkedDown = true;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_CHECK_SYNC,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
                msg.args.Enqueue(textBoxCheckNumber.Text);
                msg.args.Enqueue(textBoxCheckName.Text);
                msg.args.Enqueue(textBoxCheckType.Text);
                msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "数据下载失败!请联系管理员!\n错误信息如下：" + ex.Message, "错误提示");
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
                clsttStandardDecide model = new clsttStandardDecide()
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
            }
            catch (Exception)
            {
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
            PercentProcess process = new PercentProcess()
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
                ChangeCompany(false, "正在下载···");
                _checkedDown = true;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownCompany,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
                msg.args.Enqueue(textBoxCheckNumber.Text);
                msg.args.Enqueue(textBoxCheckName.Text);
                msg.args.Enqueue(textBoxCheckType.Text);
                msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(msg, _msgThread);
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

                if (sb.Length > 0) percent(100);
            }
            catch (Exception ex)
            {
                percent(100);
                MessageBox.Show(this, "下载被检单位时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            }
            finally
            {
                percent(100);
            }
        }

        private void KjcDownloadCompany(string data)
        {
            CompanyTemp = data;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = KjcDownloadCompanyProcess,
                MessageInfo = "正在下载被检单位"
            };
            process.Start();
        }
        private List<kjcCompany> downLoadCompanyList = null;
        private void KjcDownloadCompanyProcess(Action<int> percent)
        {
            try
            {
                _DownCompanyCount = 0;
                ResultMsg msgResult = JsonHelper.JsonToEntity<ResultMsg>(CompanyTemp);
                if (msgResult.resultCode.Equals("success1"))
                {
                    downLoadCompanyList = JsonHelper.JsonToEntity<List<kjcCompany>>(msgResult.result.ToString());
                    if (downLoadCompanyList == null || downLoadCompanyList.Count == 0)
                    {
                        percent(100);
                        MessageBox.Show("暂时没有数据可更新！");
                        return;
                    }
                    kjcCompanyBLL.Deleted("", out errMsg);
                    percent(0);
                    float percentage1 = (float)100 / (float)downLoadCompanyList.Count, percentage2 = 0;
                    int count = (int)percentage1;

                    for (int i = 0; i < downLoadCompanyList.Count; i++)
                    {
                        _DownCompanyCount += kjcCompanyBLL.Insert(downLoadCompanyList[i], out errMsg);
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据下载失败！\r\n" + ex.Message);
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
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.DownloadCompanyProcess,
                MessageInfo = "正在下载被检单位"
            };
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
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
                    #region 旧版本代码
                    //model.SysCode = dataTable.Rows[i]["SysCode"].ToString();
                    //model.StdCode = dataTable.Rows[i]["StdCode"].ToString();
                    //model.StdDes = dataTable.Rows[i]["StdDes"].ToString();
                    //model.ShortCut = dataTable.Rows[i]["ShortCut"].ToString();
                    //model.StdInfo = dataTable.Rows[i]["StdInfo"].ToString();
                    //model.StdType = dataTable.Rows[i]["StdType"].ToString();
                    //model.IsReadOnly = Convert.ToBoolean(dataTable.Rows[i]["IsReadOnly"]);
                    //model.IsLock = Convert.ToBoolean(dataTable.Rows[i]["IsLock"]);
                    //model.Remark = dataTable.Rows[i]["Remark"].ToString();
                    //model.UDate = dataTable.Rows[i]["UDate"].ToString();
                    #endregion
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
            }
            catch (Exception)
            {
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
            PercentProcess process = new PercentProcess()
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
                //if (!chekcItmes.Equals("<NewDataSet>\r\n</NewDataSet>"))
                //{
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
                //}
            }
            catch (Exception)
            {
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        /// <summary>
        /// 检测项目标准下载
        /// </summary>
        /// <param name="chekcItmes"></param>
        private void DownCheckItems(string data)
        {
            chekcItmes = data;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.DownCheckItemsProcess,
                MessageInfo = "正在下载检测项目标准"
            };
            process.Start();
        }

        /// <summary>
        /// 仪器检测项目下载
        /// 2016年12月22日wenj update 新版本接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ChangeCheckItems(false, "正在下载···");
                _checkedDown = true;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownCheckItems,
                    str1 = textBoxServerAddr.Text,
                    str2 = textBoxRegisterID.Text,
                    str3 = textBoxRegisterPassword.Password
                };
                msg.args.Enqueue(textBoxCheckNumber.Text);
                msg.args.Enqueue(textBoxCheckName.Text);
                msg.args.Enqueue(textBoxCheckType.Text);
                msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                MessageBox.Show(this, "检测项目下载失败!请联系管理员!\n错误信息如下：" + ex.Message, "错误提示");
            }
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
            catch (Exception)
            {
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
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.DownLoadCheckItemsProcess,
                MessageInfo = "正在下载检测项目"
            };
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();
        }

        /// <summary>
        /// 分光光度检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void SaveFgdResult(string[] ArryTemp)
        {
            try
            {
                DYFGDItemPara item = new DYFGDItemPara()
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
            catch (Exception)
            {
                return;
            }
            _YQCheckItemCount++;
        }

        /// <summary>
        /// 胶体金检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void SaveJtjResult(string[] ArryTemp)
        {
            try
            {
                DYJTJItemPara item = new DYJTJItemPara()
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
            catch (Exception)
            {
                return;
            }
            _YQCheckItemCount++;
        }

        /// <summary>
        /// 干化学检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void SaveGszResult(string[] ArryTemp)
        {
            try
            {
                DYGSZItemPara item = new DYGSZItemPara()
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
            catch (Exception)
            {
                return;
            }
            _YQCheckItemCount++;
        }

        /// <summary>
        /// 重金属检测项目保存
        /// </summary>
        /// <param name="ArryTemp"></param>
        private void SaveZjsResult(string[] ArryTemp)
        {
            try
            {
                DYHMItemPara item = new DYHMItemPara()
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
            catch (Exception)
            {
                return;
            }
            _YQCheckItemCount++;
        }

        public void SaveResultValue(string ChoiceAndSelect, string[] ArryTemp)
        {
            switch (ChoiceAndSelect)
            {
                case "分光光度":
                    SaveFgdResult(ArryTemp);
                    //FgdEditItemWindow FGD = new FgdEditItemWindow();
                    //FGD._FGDItemNameType = ArryTemp;
                    //FGD.ButtonNext_Click(null, null);
                    break;
                case "胶体金":
                    SaveJtjResult(ArryTemp);
                    //JtjEditItemWindow JTJ = new JtjEditItemWindow();
                    //JTJ._JTJItemNameType = ArryTemp;
                    //JTJ.ButtonNext_Click(null, null);
                    break;
                case "干化学法":
                    SaveGszResult(ArryTemp);
                    //GszEditItemWindow GSZ = new GszEditItemWindow();
                    //GSZ._GSZItemNameType = ArryTemp;
                    //GSZ.ButtonNext_Click(null, null);
                    break;
                case "重金属":
                    SaveZjsResult(ArryTemp);
                    //HmEditItemWindow ZJS = new HmEditItemWindow();
                    //ZJS._HmMetailArry = ArryTemp;
                    //ZJS.ButtonNext_Click(null, null);
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
            Message msg = new Message()
            {
                what = MsgCode.MSG_COMM_TEST_HM,
                str1 = ComboBoxHMPort.Text
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        private void BtnCheckError_Click(object sender, RoutedEventArgs e)
        {
            ShowError window = new ShowError()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 绑定广东省智慧云平台服务器配置
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
            MessageBox.Show(this, "服务器链接信息绑定成功!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            TxtZhDeviceId.IsReadOnly = true;
        }

        private void Btn_autoUpdater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");
            }
            catch (Exception)
            {
                MessageBox.Show("未找到系统自动升级服务！\r\n\r\n请联系软件供应商提供系统自动升级服务程序！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RepairProjects(Action<int> percent)
        {
            percent(0);
            try
            {
                string err = string.Empty;
                tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
                List<string> sqlList = new List<string>();
                sqlList.Add("create table tAtps(ID integer identity(1,1) PRIMARY KEY)");
                sqlList.Add("alter table ttResultSecond add SysCode varchar(255)");
                sqlList.Add("alter table ttResultSecond add DeviceId varchar(255)");
                sqlList.Add("alter table ttResultSecond add SampleId varchar(255)");
                sqlList.Add("alter table ttResultSecond add ProduceCompany varchar(255)");
                sqlList.Add("alter table ttResultSecond add CKCKNAMEUSID varchar(255)");
                sqlList.Add("alter table ttResultSecond add FoodType varchar(255)");

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

        /// <summary>
        /// 数据库修复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DataBaseRepair_Click(object sender, RoutedEventArgs e)
        {
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.RepairProjects,
                MessageInfo = "程序正在自检，请稍后"
            };
            process.Start();

            return;

            tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
            List<string> sqlList = new List<string>();
            string sql = string.Empty, err = string.Empty;
            int rtn = 0;

            //create tAtp
            sql = "create table tAtp(ID integer identity(1,1) PRIMARY KEY)";
            rtn = bll.DataBaseRepair(sql, out err);
            sqlList.Add("alter table tAtp add ATP_CHECKNAME varchar(255)");
            sqlList.Add("alter table tAtp add ATP_RLU varchar(255)");
            sqlList.Add("alter table tAtp add ATP_RESULT varchar(255)");
            sqlList.Add("alter table tAtp add ATP_CHECKDATA varchar(255)");
            sqlList.Add("alter table tAtp add ATP_CHECKTIME varchar(255)");
            sqlList.Add("alter table tAtp add ATP_UPPER varchar(255)");
            sqlList.Add("alter table tAtp add ATP_LOWER varchar(255)");
            rtn = 0;

            //create TB_SAMPLE
            sql = string.Format("create table TB_SAMPLE(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table TB_SAMPLE add SAMPLENUM varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPDATE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PTYPE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SLINK varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SOURCE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPCONT varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPPERSON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add FOODNAME varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BARCODE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSAMPCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPCONT varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BRAND varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PRODATE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add MODEL varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BATCHNUM varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SHELFLIFE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add DEVICEID varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add UDATE varchar(255)");

            //create tCheckComType
            sql = string.Format("create table tCheckComType(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tCheckComType add TypeName varchar(255)");
            sqlList.Add("alter table tCheckComType add NameCall varchar(255)");
            sqlList.Add("alter table tCheckComType add AreaCall varchar(255)");
            sqlList.Add("alter table tCheckComType add VerType varchar(255)");
            sqlList.Add("alter table tCheckComType add IsReadOnly bit default no");
            sqlList.Add("alter table tCheckComType add IsLock bit default no");
            sqlList.Add("alter table tCheckComType add ComKind varchar(255)");
            sqlList.Add("alter table tCheckComType add AreaTitle varchar(255)");
            sqlList.Add("alter table tCheckComType add NameTitle varchar(255)");
            sqlList.Add("alter table tCheckComType add DomainTitle varchar(255)");
            sqlList.Add("alter table tCheckComType add SampleTitle varchar(255)");

            //create tCheckItem
            sql = string.Format("create table tCheckItem(SysCode varchar(50) PRIMARY KEY)");
            sqlList.Add("alter table tCheckItem add StdCode varchar(255)");
            sqlList.Add("alter table tCheckItem add ItemDes varchar(255)");
            sqlList.Add("alter table tCheckItem add CheckType varchar(255)");
            sqlList.Add("alter table tCheckItem add StandardCode varchar(255)");
            sqlList.Add("alter table tCheckItem add StandardValue varchar(255)");
            sqlList.Add("alter table tCheckItem add Unit varchar(255)");
            sqlList.Add("alter table tCheckItem add DemarcateInfo varchar(255)");
            sqlList.Add("alter table tCheckItem add TestValue varchar(255)");
            sqlList.Add("alter table tCheckItem add OperateHelp varchar(255)");
            sqlList.Add("alter table tCheckItem add CheckLevel varchar(255)");
            sqlList.Add("alter table tCheckItem add IsReadOnly bit default no");
            sqlList.Add("alter table tCheckItem add IsLock bit default no");
            sqlList.Add("alter table tCheckItem add Remark varchar(255)");
            sqlList.Add("alter table tCheckItem add UDate varchar(255)");

            //create tCheckLevel
            sql = string.Format("create table tCheckLevel(CheckLevel varchar(50) PRIMARY KEY)");
            sqlList.Add("alter table tCheckComType add IsReadOnly bit default no");
            sqlList.Add("alter table tCheckComType add IsLock bit default no");
            sqlList.Add("alter table tCheckComType add Remark varchar(255)");

            //create tCheckType
            sql = string.Format("create table tCheckType(Name varchar(50) PRIMARY KEY)");
            sqlList.Add("alter table tCheckType add IsReadOnly bit default no");
            sqlList.Add("alter table tCheckType add IsLock bit default no");
            sqlList.Add("alter table tCheckType add Remark varchar(255)");

            //检测结果表[ttResultSecond]
            sqlList.Add("alter table ttResultSecond add test varchar(255)");

            if (sqlList != null && sqlList.Count > 0)
            {
                for (int i = 0; i < sqlList.Count; i++)
                {
                    bll.DataBaseRepair(sqlList[i], out err);
                }
            }
        }

        string errMsg = string.Empty;

        private bool CheckKsInfo()
        {
            string val = txt_KsUser.Text.Trim();
            if (val.Length == 0)
            {
                MessageBox.Show("注册用户不能为空！", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_KsUser.Focus();
                return false;
            }
            Global.KsUser = val;

            val = txt_KsPwd.Password;
            if (val.Length == 0)
            {
                MessageBox.Show("密码不能为空！", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_KsPwd.Focus();
                return false;
            }
            Global.KsPwd = val;

            return true;
        }

        /// <summary>
        /// 昆山单位主体|经营户下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuerySignContact_Click(object sender, RoutedEventArgs e)
        {
            if (CheckKsInfo())
            {
                Message msg = new Message();
                msg.what = Global.KsVersion.Equals("2") ? MsgCode.KS_GetAreaMarket : MsgCode.KS_QuerySignContact;
                Global.updateThread.SendMessage(msg, _msgThread);
            }
        }


        private void QuerySignContactByAM(List<KunShanEntity.AreaMarket> models)
        {
            //获取到主体信息后，后台再去获取对应的经营户信息
            Message msg = new Message();
            msg.what = MsgCode.KS_QuerySignContactByAM;
            msg.obj1 = models;
            Global.updateThread.SendMessage(msg, _msgThread);
        }

        List<KunShanEntity.AreaMarket> _models;
        string[,] _vals;
        private void DownAreaMarket(Action<int> percent)
        {
            try
            {
                percent(0);
                if (_models != null)
                {
                    percent(1);
                    //获取到数据后先删除单位主体信息
                    string where = "DELETE FROM ks_AreaMarket";
                    opr.OtherOpr(where, out errMsg);
                    if (errMsg.Length == 0)
                    {
                        //主体信息删除后再删除经营户信息
                        where = "DELETE FROM Ks_Business";
                        opr.OtherOpr(where, out errMsg);
                    }
                    percent(2);

                    StringBuilder sql = null;
                    int uploadNum = 0, len = _models.Count;
                    List<KunShanEntity.SignContact> signContact = null;
                    List<KunShanEntity.SignContact> signContacts = new List<KunShanEntity.SignContact>();
                    for (int i = 0; i < _vals.Length / 2; i++)
                    {
                        try
                        {
                            if (_vals[1, i] == null) continue;

                            signContact = JsonHelper.JsonToEntity<List<KunShanEntity.SignContact>>(_vals[1, i]);
                            if (signContact != null && signContact.Count > 0)
                            {
                                len += signContact.Count;
                                for (int j = 0; j < signContact.Count; j++)
                                {
                                    signContact[j].LicenseNo = _vals[0, i];
                                    signContacts.Add(signContact[j]);
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                    percent(3);

                    float percentage = (float)98 / (float)len, count = percentage + 3;

                    //添加主体信息内容
                    for (int i = 0; i < _models.Count; i++)
                    {
                        sql = new StringBuilder();
                        sql.Append("Insert Into ks_AreaMarket (SysCode,LicenseNo,MarketName,Abbreviation,MarketRef) ");
                        sql.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')",
                            Global.GETGUID(), _models[i].LicenseNo, _models[i].MarketName, _models[i].Abbreviation, _models[i].MarketRef);
                        opr.OtherOpr(sql.ToString(), out errMsg);
                        if (errMsg.Length == 0)
                            uploadNum += 1;

                        if (count < 100)
                        {
                            percent((int)count);
                            count += percentage;
                        }
                        else
                        {
                            count = 100;
                        }
                    }

                    //添加主体对应经营户信息
                    for (int i = 0; i < signContacts.Count; i++)
                    {
                        sql = new StringBuilder();
                        sql.Append("Insert Into Ks_Business (TWQY,TWNum,IdentityCard,TWNume,LinkPhone,ID,LicenseNo) ");
                        sql.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            signContacts[i].PositionDistrictName, signContacts[i].PositionNo, signContacts[i].DABH, signContacts[i].Contactor, signContacts[i].ContactTel, Global.GETGUID(), signContacts[i].LicenseNo);
                        opr.OtherOpr(sql.ToString(), out errMsg);
                        if (errMsg.Length == 0)
                            uploadNum += 1;

                        if (count < 100)
                        {
                            percent((int)count);
                            count += percentage;
                        }
                        else
                        {
                            count = 100;
                        }
                    }

                    percent(100);
                    MessageBox.Show("本次成功下载 " + uploadNum + " 条单位主体信息(含对应经营户)！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(KunShanHelper.SignContact, "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {
                if (KunShanHelper.SignContact.Length > 0)
                {
                    MessageBox.Show(KunShanHelper.SignContact, "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            finally { percent(100); }
        }

        private void DownSignContact(Action<int> percent)
        {
            try
            {
                percent(0);
                List<KunShanEntity.SignContact> models = JsonHelper.JsonToEntity<List<KunShanEntity.SignContact>>(KunShanHelper.SignContact);
                if (models != null)
                {
                    percent(1);
                    //获取到数据后先删除本地数据，再插入
                    string where = "DELETE FROM Ks_Business";
                    opr.OtherOpr(where, out errMsg);
                    percent(2);

                    StringBuilder sql = null;
                    int uploadNum = 0;
                    float percentage = (float)98 / (float)models.Count, count = percentage + 2;

                    for (int i = 0; i < models.Count; i++)
                    {
                        sql = new StringBuilder();
                        sql.Append("Insert Into Ks_Business (TWQY,TWNum,IdentityCard,TWNume,LinkPhone,ID) ");
                        sql.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')",
                            models[i].PositionDistrictName, models[i].PositionNo, models[i].DABH, models[i].Contactor, models[i].ContactTel, Global.GETGUID());
                        opr.OtherOpr(sql.ToString(), out errMsg);
                        if (errMsg.Length == 0)
                            uploadNum += 1;

                        if (count < 100)
                        {
                            percent((int)count);
                            count += percentage;
                        }
                        else
                        {
                            count = 100;
                        }
                    }

                    MessageBox.Show("本次成功下载 " + uploadNum + " 条经营户信息！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(KunShanHelper.SignContact, "下载失败", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {
                if (KunShanHelper.SignContact.Length > 0)
                {
                    MessageBox.Show(KunShanHelper.SignContact, "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            finally { percent(100); }
        }

        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryCheckItem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckKsInfo())
            {
                Message msg = new Message();
                msg.what = MsgCode.KS_QueryCheckItem;
                Global.updateThread.SendMessage(msg, _msgThread);
            }
        }

        private void DownCheckItem(Action<int> percent)
        {
            try
            {
                percent(0);
                List<KunShanEntity.CheckItem> models = JsonHelper.JsonToEntity<List<KunShanEntity.CheckItem>>(KunShanHelper.CheckItem);
                if (models != null)
                {
                    percent(1);
                    //获取到数据后先删除本地数据，再插入
                    string where = "DELETE FROM KS_CheckItem";
                    opr.OtherOpr(where, out errMsg);
                    percent(2);

                    StringBuilder sql = null;
                    int uploadNum = 0;
                    float percentage = (float)98 / (float)models.Count, count = percentage + 2;

                    for (int i = 0; i < models.Count; i++)
                    {
                        sql = new StringBuilder();
                        sql.Append("Insert Into KS_CheckItem (ItemCode,ItemName,ParentCode,ParentName) ");
                        sql.AppendFormat("Values('{0}','{1}','{2}','{3}')",
                           models[i].SubItemCode, models[i].SubItemName, models[i].ItemCode, models[i].ItemName);
                        opr.OtherOpr(sql.ToString(), out errMsg);
                        if (errMsg.Length == 0)
                            uploadNum += 1;

                        if (count < 100)
                        {
                            percent((int)count);
                            count += percentage;
                        }
                        else
                        {
                            count = 100;
                        }
                    }

                    MessageBox.Show("本次成功下载 " + uploadNum + " 条检测项目信息！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(KunShanHelper.CheckItem + "\r\n请确保输入的用户名与密码无误！", "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {
                if (KunShanHelper.CheckItem.Length > 0)
                {
                    MessageBox.Show(KunShanHelper.CheckItem, "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            finally { percent(100); }
        }

        /// <summary>
        /// 检测品种下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuerySalesItem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckKsInfo())
            {
                Message msg = new Message();
                msg.what = MsgCode.KS_QuerySalesItem;
                Global.updateThread.SendMessage(msg, _msgThread);
            }
        }

        private void DownSalesItem(Action<int> percent)
        {
            string log = string.Empty;
            try
            {
                percent(0);
                FileUtils.Log(KunShanHelper.SalesItem);
                List<KunShanEntity.SalesItem> models = JsonHelper.JsonToEntity<List<KunShanEntity.SalesItem>>(KunShanHelper.SalesItem);
                if (models != null)
                {
                    percent(1);
                    //获取到数据后先删除本地数据，再插入
                    string where = "DELETE FROM Ks_FoodClass";
                    opr.OtherOpr(where, out errMsg);
                    percent(2);

                    StringBuilder sql = null;
                    int uploadNum = 0;
                    float percentage = (float)98 / (float)models.Count, count = percentage + 2;

                    for (int i = 0; i < models.Count; i++)
                    {
                        sql = new StringBuilder();
                        sql.Append("Insert Into Ks_FoodClass (ParentCode,ParentName,FoodCode,FoodName) ");
                        sql.AppendFormat("Values('{0}','{1}','{2}','{3}')",
                            models[i].ItemCode, models[i].ItemName, models[i].SubItemCode, models[i].SubItemName);
                        opr.OtherOpr(sql.ToString(), out errMsg);
                        if (errMsg.Length == 0)
                            uploadNum += 1;

                        if (count < 100)
                        {
                            percent((int)count);
                            count += percentage;
                        }
                        else
                        {
                            count = 100;
                        }
                    }

                    MessageBox.Show("本次成功下载 " + uploadNum + " 条检测品种信息！", "下载提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    log = KunShanHelper.SalesItem + "\r\n请确保输入的用户名与密码无误！";
                    FileUtils.Log(log);
                    MessageBox.Show(log, "下载失败", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 0)
                {
                    FileUtils.Log(ex.Message);
                    MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    log = KunShanHelper.SalesItem;
                    FileUtils.Log(log);
                    MessageBox.Show(KunShanHelper.SalesItem, "下载错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            finally { percent(100); }
        }

        /// <summary>
        /// 昆山通讯测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKsServiceTest_Click(object sender, RoutedEventArgs e)
        {
            if (CheckKsInfo())
            {
                Message msg = new Message();
                msg.what = MsgCode.KS_QUERYMARKET;
                Global.updateThread.SendMessage(msg, _msgThread);
            }
        }

        private void SettingKsInfo(string json)
        {
            KunShanEntity.QueryMarketResponse.Response market = null;
            try
            {
                market = JsonHelper.JsonToEntity<KunShanEntity.QueryMarketResponse.Response>(json.Replace("[", "").Replace("]", ""));
            }
            catch (Exception) { }

            if (market != null)
            {
                txt_KsMarketName.Text = market.MarketName;
                txt_KsMarketCode.Text = market.LicenseNo;
                SaveSetting();
                MessageBox.Show("通讯成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                txt_KsMarketName.Text = txt_KsMarketCode.Text = "";
                MessageBox.Show("通讯失败！\r\n" + json, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_KsUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_KsMarketName.Text = txt_KsMarketCode.Text = "";
        }

        private void txt_KsPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txt_KsMarketName.Text = txt_KsMarketCode.Text = "";
        }

    }
}