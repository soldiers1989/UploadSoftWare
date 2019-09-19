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
using DYSeriesDataSet.DataModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Runtime.InteropServices;

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
            chekcItmes = string.Empty, StandardDecideTemp = string.Empty, register = string.Empty;
        /// <summary>
        /// 服务器通讯测试
        /// </summary>
        private bool IS_SERVICE_TEST = false;
        /// <summary>
        /// 是否已注册，为True是未注册
        /// </summary>
        public bool Noregister = false;
        /// <summary>
        /// 服务器地址、用户名、密码为空标准系统设置
        /// </summary>
        public bool IsServerNull = false;


        #endregion

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //没注册变灰返回按钮
                //if (Noregister == true)
                //{
                //    ButtonPrev.IsEnabled = false;
                //}
                if (Noregister == true)
                {
                    Global.MachineNum = "";
                    btn_autoUpdater.IsEnabled = false;
                }
                else
                {
                    if (Global.sysupdatebtn == true)
                    {
                        btn_autoUpdater.IsEnabled = true;
                    }
                    else
                    {
                        btn_autoUpdater.IsEnabled = false;
                    }
                }
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
                    //textBoxCheckNumber.Text = CPoint.pointNum;
                    textBoxCheckName.Text = CPoint.pointName;
                    //textBoxCheckType.Text = CPoint.pointType;
                    textBoxCheckOrg.Text = CPoint.orgName;
                    if (CPoint.IsAutoSweed)
                    {
                        ChkAutoDownTask.IsChecked = false;
                        ChkSweepCode.IsChecked = true;
                    }
                    else
                    {
                        ChkAutoDownTask.IsChecked = true ;
                        ChkSweepCode.IsChecked = false ;
                    }
                    //textCheckPlaceCode.Text = CPoint.orgNum;
                }
                ComboBoxADPort.Text = Global.strADPORT;
                ComboBoxSXT1Port.Text = Global.strSXT1PORT;
                ComboBoxSXT2Port.Text = Global.strSXT2PORT;
                ComboBoxSXT3Port.Text = Global.strSXT3PORT;
                ComboBoxSXT4Port.Text = Global.strSXT4PORT;
                ComboBoxPRINTPort.Text = Global.strPRINTPORT;
                ComboBoxHMPort.Text = Global.strHMPORT;

                Global.MachineModel = CFGUtils.GetConfig("InstrumentNameModel", "DY3500(I)");
                textBoxMachineModel.Text = Global.MachineModel;//仪器型号
                textBoxSerialNum.Text = Global.MachineNum;//仪器系列号

                //textBoxCheckNumber.Text = Global.pointNum;
                textBoxCheckName.Text = Global.pointName;
                //textBoxCheckType.Text = Global.pointType;
                textBoxCheckOrg.Text = Global.orgName;
                //textCheckPlaceCode.Text = Global.orgID;

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
                textFactoryCode.Text = CFGUtils.GetConfig("FactoryNumber", "DY3500(I)2019001");
                
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
                //CPoint.pointNum = textBoxCheckNumber.Text;
                CPoint.pointName = textBoxCheckName.Text;
                //CPoint.pointType = textBoxCheckType.Text;
                CPoint.orgName = textBoxCheckOrg.Text;
                //CPoint.orgNum = textCheckPlaceCode.Text;
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
                Global.MachineModel = textBoxMachineModel.Text.Trim();
                CFGUtils.SaveConfig("InstrumentNameModel", Global.MachineModel);
                //出厂编号
                Global.FactoryNumber = textFactoryCode.Text.Trim();
                CFGUtils.SaveConfig("FactoryNumber", Global.FactoryNumber);

                Global.SerializeToFile(Global.deviceHole, Global.deviceHoleFile);
                // CFGUtils.SaveConfig("QToken", Global.Token);

                textBoxCheckName.Text = Global.pointName;//检测点名称
                textBoxCheckOrg.Text = Global.orgName;//所属机构
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
            if (Noregister == true || IsServerNull == true)
            {
                if (MessageBox.Show("是否退出系统", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                    //    string url = InterfaceHelper.GetServiceURL("http://dyxx.chinafst.cn:8898/UpdateTool/", 16);
                    //    StringBuilder sb = new StringBuilder();
                    //    string message = AES.Encrypt(Global.FactoryNumber, "DYXX@)!(QPMZA2-5");
                    //    message = message.Replace('+', '-').Replace('/', '_').Replace("=", "");
                    //    Console.WriteLine(message);
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

                    Environment.Exit(0);
                }
            }
            else
            {
                this.Close();
            }

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
                //this.textCheckPlaceCode.Text = Global.orgNum;
                //this.textBoxCheckNumber.Text = Global.pointNum;
                this.textBoxCheckName.Text = Global.pointName;
                //this.textBoxCheckType.Text = Global.pointType;
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
                    //通信测试
                    case MsgCode.MSG_CHECK_CONNECTION:
                        try
                        {
                            wnd.IS_SERVICE_TEST = true;
                            if (msg.result)
                            {
                                wnd.IsEnabledTrue();
                                wnd.SaveSetting();
                                //wnd.BtnCompany_Click(null, null);
                                Global.IsServerTest = false;
                                wnd.IsServerNull = false;
                                MessageBox.Show(wnd, "测试连接成功!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                //if (msg.errMsg.Length > 0)
                                //{
                                //    MessageBox.Show(msg.errMsg);
                                //}

                                //Global.samplenameadapter = null;
                                wnd.IsEnabledTrue();
                                Global.IsServerTest = true;
                                MessageBox.Show(wnd, "测试连接失败!\r\n\r\n失败原因：" + msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                    case MsgCode.MSG_REGISTE_MACHINE:
                        if (msg.result == true && msg.responseInfo == "注册成功")
                        {
                            MessageBox.Show("仪器注册成功\r\n\r\n" + "仪器注册号：" + Global.MachineNum);
                            Global.isresige = true;
                            wnd.Noregister = false;
                            wnd.ButtonPrev.IsEnabled = true;
                            wnd.RegisterMachine(msg.responseInfo);
                            wnd.deleteTaskData(Global.MachineNum);
                        }
                        else
                        {
                            wnd.bun();
                            MessageBox.Show("仪器注册失败！\r\n\r\n"+msg.responseInfo);
                            Global.isresige = false;
                            wnd.btnRegisteMachine.IsEnabled = true;
                            //wnd.RegisterMachine(msg.responseInfo);
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

            if (textBoxServerAddr.Text.Trim() == "")
            {
                MessageBox.Show("服务器地址不能为空", "操作提示");
                return;
            }
            if (textBoxRegisterID.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能为空", "操作提示");
                return;
            }
            if (textBoxRegisterPassword.Password.Trim() == "")
            {
                MessageBox.Show("密码不能为空", "操作提示");
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

            //textBoxCheckNumber.Text=string.Empty;
            //textBoxCheckName.Text = string.Empty;
            //textBoxCheckType.Text = string.Empty;
            //textBoxCheckOrg.Text = string.Empty;
            //textCheckPlaceCode.Text = string.Empty;
            //Global.MachineModel = textBoxMachineModel.Text.Trim();//仪器型号
            buttonServerTest.Content = "正在测试";
            buttonServerTest.IsEnabled = false;

            Message msg = new Message()
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 = textBoxServerAddr.Text.Trim(),
                str2 = textBoxRegisterID.Text.Trim(),
                str3 = textBoxRegisterPassword.Password.Trim()
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
                    str1 = textBoxServerAddr.Text.Trim(),
                    str2 = textBoxRegisterID.Text.Trim(),
                    str3 = textBoxRegisterPassword.Password.Trim()
                };
                //msg.args.Enqueue(textBoxCheckNumber.Text);
                //msg.args.Enqueue(textBoxCheckName.Text);
                //msg.args.Enqueue(textBoxCheckType.Text);
                //msg.args.Enqueue(textBoxCheckOrg.Text);
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
                    str1 = textBoxServerAddr.Text.Trim(),
                    str2 = textBoxRegisterID.Text.Trim(),
                    str3 = textBoxRegisterPassword.Password.Trim()
                };
                //msg.args.Enqueue(textBoxCheckNumber.Text);
                //msg.args.Enqueue(textBoxCheckName.Text);
                //msg.args.Enqueue(textBoxCheckType.Text);
                //msg.args.Enqueue(textBoxCheckOrg.Text);
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
        string errMsg = string.Empty;
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
                    str1 = textBoxServerAddr.Text.Trim(),
                    str2 = textBoxRegisterID.Text.Trim(),
                    str3 = textBoxRegisterPassword.Password.Trim()
                };
                //msg.args.Enqueue(textBoxCheckNumber.Text);
                //msg.args.Enqueue(textBoxCheckName.Text);
                //msg.args.Enqueue(textBoxCheckType.Text);
                //msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                MessageBox.Show(this, "检测项目下载失败!请联系管理员!\n错误信息如下：" + ex.Message, "错误提示");
            }
        }

        private int _YQCheckItemCount = 0;

        private void Registermachinedata(Action<int> percent)
        {
            _YQCheckItemCount = 0;
            percent(0);
            if (register.Length > 0)
            {
                btnRegisteMachine.IsEnabled = true;
            }

        }

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
        private void bun()
        {
            btnRegisteMachine.IsEnabled = false;
            //ButtonPrev.IsEnabled = false;
        }
        private void RegisterMachine(string data)
        {
            CFGUtils.SaveConfig("IntrumentSeriersNum", Global.MachineNum);
            textBoxSerialNum.Text = Global.MachineNum;
            btnRegisteMachine.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            //register = data;
            //PercentProcess process = new PercentProcess()
            //{
            //    BackgroundWork = this.Registermachinedata,
            //    MessageInfo = "正在注册"
            //};
            ////process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            //process.Start();
        }
        /// <summary>
        /// 删除不用检测任务
        /// </summary>
        /// <param name="machine"></param>
        public void deleteTaskData(string machine)
        {
            int rtn= _clsCompanyOprBLL.DeleteTasks(machine,out errMsg );
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

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string path = Environment.CurrentDirectory + "\\Updateload.ini";
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_autoUpdater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Global.IsConnectInternet())
                {
                    MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                //ttResultSecond
                sqlList.Add("alter table ttResultSecond add SysCode varchar(255)");
                sqlList.Add("alter table ttResultSecond add shoudong varchar(255)");
                sqlList.Add("alter table ttResultSecond add taskid varchar(255)");
                sqlList.Add("alter table ttResultSecond add Sdid  varchar(255)");
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
                sqlList.Add("alter table ttResultSecond add OpertorName varchar(255)");
                sqlList.Add("alter table ttResultSecond add OpertorID varchar(255)");
                //扫码任务表
                sqlList.Add("create table BarTask(ID integer identity(1,1) PRIMARY KEY)");
                sqlList.Add("alter table BarTask add bid varchar(255)");
                sqlList.Add("alter table BarTask add bagCode varchar(255)");
                sqlList.Add("alter table BarTask add foodName varchar(255)");
                sqlList.Add("alter table BarTask add itemName varchar(255)");
                sqlList.Add("alter table BarTask add mokuai varchar(255)");
                sqlList.Add("alter table BarTask add IsTest varchar(255)");
                sqlList.Add("alter table BarTask add getSampleTime varchar(255)");
                sqlList.Add("alter table BarTask add InBarCode varchar(255)");
                
                //KTask
                sqlList.Add("alter table KTask add dataType varchar(255)");
                sqlList.Add("alter table KTask add IsReceive varchar(255)");
                sqlList.Add("alter table KTask add UserName varchar(255)");
                sqlList.Add("alter table KTask add Checktype varchar(255)");
                sqlList.Add("alter table KTask add mokuai varchar(255)");
                sqlList.Add("alter table KTask add td_remark varchar(255)");

                //新增仪器检测项目
                sqlList.Add("alter table MachineItem add SaveTime varchar(255)");
                sqlList.Add("alter table MachineItem add RefDeltaA varchar(255)");
                //保存曲线数据
                sqlList.Add("create table CurveDatas(ID integer identity(1,1) PRIMARY KEY)");
                sqlList.Add("alter table CurveDatas add SysCode varchar(255)");
                sqlList.Add("alter table CurveDatas add CData memo");

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

          
        }
        /// <summary>
        /// 仪器注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisteMachine_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Global.MachineModel = textBoxMachineModel.Text.Trim();//仪器型号

                CFGUtils.SaveConfig("InstrumentNameModel", Global.MachineModel);

                btnRegisteMachine.IsEnabled = false;
                //ChangeCheckItems(false, "正在下载···");
                // _checkedDown = true;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_REGISTE_MACHINE,
                    str1 = textBoxServerAddr.Text.Trim(),
                    str2 = textBoxRegisterID.Text.Trim(),
                    str3 = textBoxRegisterPassword.Password.Trim()
                };
                //msg.args.Enqueue(textBoxCheckNumber.Text);
                //msg.args.Enqueue(textBoxCheckName.Text);
                //msg.args.Enqueue(textBoxCheckType.Text);
                //msg.args.Enqueue(textBoxCheckOrg.Text);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
                MessageBox.Show(this, "仪器注册失败!请联系管理员!\n错误信息如下：" + ex.Message, "错误提示");
                btnRegisteMachine.IsEnabled = true;
            }
        }

        private void ChkAutoDownTask_Click(object sender, RoutedEventArgs e)
        {
            if (ChkAutoDownTask.IsChecked ==true )
            {
                Global.IsSweepCode = false;
                ChkSweepCode.IsChecked = false;
            }
            else 
            {
                Global.IsSweepCode = true ;
                ChkAutoDownTask.IsChecked = true;
            }
           

            CheckPointInfo CPoint;
            Global.samplenameadapter = Global.samplenameadapter ?? new List<CheckPointInfo>();
            CPoint = Global.samplenameadapter.Count == 0 ? new CheckPointInfo() : Global.samplenameadapter[0];
            CPoint.IsAutoSweed = Global.IsSweepCode;
            if (Global.samplenameadapter.Count == 0)
                Global.samplenameadapter.Add(CPoint);
            Global.SerializeToFile(Global.samplenameadapter, Global.samplenameadapterFile);

        }

        private void ChkSweepCode_Click(object sender, RoutedEventArgs e)
        {
            if (ChkSweepCode.IsChecked == true)
            {

                Global.IsSweepCode = true;
                ChkAutoDownTask.IsChecked = false;
            }
            else
            {
                Global.IsSweepCode = false ;
                ChkSweepCode.IsChecked = true ;
            }
          
            CheckPointInfo CPoint;
            Global.samplenameadapter = Global.samplenameadapter ?? new List<CheckPointInfo>();
            CPoint = Global.samplenameadapter.Count == 0 ? new CheckPointInfo() : Global.samplenameadapter[0];
            CPoint.IsAutoSweed = Global.IsSweepCode;
            if (Global.samplenameadapter.Count == 0)
                Global.samplenameadapter.Add(CPoint);
            Global.SerializeToFile(Global.samplenameadapter, Global.samplenameadapterFile);
        }

    }
}