﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.lvrenyang;
using DYSeriesDataSet;
using AIO.xaml.HeavyMetal;
using AIO.xaml.Dialog;
using System.Windows.Threading;

namespace AIO
{
    /// <summary>
    /// HmMeasureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmMeasureWindow : Window
    {

        #region 全局变量
        public DYHMItemPara _item;
        UpdateThread _updateThread;
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        List<TextBox> _listSampleNum;
        List<Label> _listTime;
        List<Label> _listUA;
        List<Label> _listMV;
        List<Label> _listDensity;
        List<Label> _listStage;
        List<Canvas> _canvases;
        MsgThread _msgThread;
        DateTime _date = DateTime.Now;
        private string _UnqualifiedValue = string.Empty;
        private string _Value = string.Empty;
        private string[] _StandardValue = new string[4];
        private string[,] _CheckValue = new string[1, 15];
        int _AllNumber = 0;
        List<TextBox> _listJudgmentReult;
        List<TextBox> _listStandardValue;
        List<TextBox> _listtextNongdu;
        CurveWindow _cruveWindow = null;
        private DispatcherTimer _DataTimer = null;
        #endregion
        public HmMeasureWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;
            _listSampleNum = new List<TextBox>();
            _listJudgmentReult = new List<TextBox>();
            _listStandardValue = new List<TextBox>();
            _listtextNongdu = new List<TextBox>();

            int sampleNum = _item.SampleNum;
            int holeUse = 0;
            // 添加布局
            for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                UIElement element = GenerateChannelLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                WrapPanelChannel.Children.Add(element);
                if (_item.Hole[i].Use)
                {
                    holeUse += 1;
                    sampleNum++;
                    _listSampleNum.Add(UIUtils.GetChildObject<TextBox>(element, "sampleNum"));
                    _listJudgmentReult.Add(UIUtils.GetChildObject<TextBox>(element, "textJugmentResult"));
                    _listStandardValue.Add(UIUtils.GetChildObject<TextBox>(element, "textStandValue"));
                    _listtextNongdu.Add(UIUtils.GetChildObject<TextBox>(element, "textNongdu"));
                }
                else
                {
                    element.Visibility = System.Windows.Visibility.Collapsed;
                    _listSampleNum.Add(null);
                    _listJudgmentReult.Add(null);
                    _listStandardValue.Add(null);
                    _listtextNongdu.Add(null);
                }
            }

            if (holeUse < 4)
            {
                if (holeUse == 1)
                    this.WrapPanelChannel.Width = 190;
                else if (holeUse == 2)
                    this.WrapPanelChannel.Width = 380;
                else if (holeUse == 3)
                    this.WrapPanelChannel.Width = 570;
            }

            _canvases = UIUtils.GetChildObjects<Canvas>(WrapPanelChannel, "rootCanvas");
            _listTime = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "labelTime");
            _listUA = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "labelUA");
            _listMV = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "labelMV");
            _listStage = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "labelStage");
            _listDensity = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "labelDensity");

            _updateThread = new UpdateThread(this);
            _updateThread.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
            _updateThread.Stop();
        }

        string[] syscodes = null;
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_UnqualifiedValue))
            {
                _AllNumber = TestResultConserve.ResultConserve(_CheckValue, out syscodes);
                if (_AllNumber > 0)
                {
                    LabelInfo.Content = "成功保存 " + _AllNumber + " 条数据！";
                    button3.IsEnabled = false;
                    button1.IsEnabled = true;
                }
                else
                {
                    LabelInfo.Content = "保存失败或是未检出！";
                }
            }
            else
            {
                MessageBox.Show("未检出");
            }
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsOpenPrompt)
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow();
                window._HintStr = _item.HintStr;
                window.Show();
            }
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void ButtonSampleTest_Click(object sender, RoutedEventArgs e)
        {
            _item.Method = ComboBoxMethod.SelectedIndex;

            byte[] data = new byte[56];
            data[0] = (byte)(_item.ItemID >> 8);
            data[1] = (byte)(_item.ItemID);

            data[2] = (byte)(_item.Method >> 8);
            data[3] = (byte)(_item.Method);

            data[4] = 0x00;
            data[5] = 0x01;
            data[6] = 0x00;
            data[7] = 0x00;

            data[8] = 0x30;
            data[9] = 0x31;

            data[10] = (byte)(_item.DilutionRatio * 10 >> 8);
            data[11] = (byte)(_item.DilutionRatio * 10);

            data[12] = (byte)(_item.Concentration * 1000 >> 8);
            data[13] = (byte)(_item.Concentration * 1000);

            data[14] = (byte)(_item.LiquidVolume * 100 >> 8);
            data[15] = (byte)(_item.LiquidVolume * 100);

            data[16] = (byte)(_item.Requirements * 10000 >> 24);
            data[17] = (byte)(_item.Requirements * 10000 >> 16);
            data[18] = (byte)(_item.Requirements * 10000 >> 8);
            data[19] = (byte)(_item.Requirements * 10000);

            data[20] = (byte)(_item.a0 * 10000 >> 24);
            data[21] = (byte)(_item.a0 * 10000 >> 16);
            data[22] = (byte)(_item.a0 * 10000 >> 8);
            data[23] = (byte)(_item.a0 * 10000);

            data[24] = (byte)(_item.a1 * 10000 >> 24);
            data[25] = (byte)(_item.a1 * 10000 >> 16);
            data[26] = (byte)(_item.a1 * 10000 >> 8);
            data[27] = (byte)(_item.a1 * 10000);

            data[28] = (byte)(_item.a2 * 10000 >> 24);
            data[29] = (byte)(_item.a2 * 10000 >> 16);
            data[30] = (byte)(_item.a2 * 10000 >> 8);
            data[31] = (byte)(_item.a2 * 10000);

            data[32] = (byte)'A';
            data[33] = (byte)'I';
            data[34] = (byte)'O';
            data[35] = 0x00;

            data[48] = 0x00;

            Message msg = new Message();
            msg.what = MsgCode.MSG_DETECTE_START_HEAVYMETAL;
            msg.str1 = Global.strHMPORT;
            msg.arg1 = 0x3c83;
            msg.arg2 = 0x01101c;
            msg.data = data;
            Global.workThread.SendMessage(msg, _updateThread);

            ButtonPrev.IsEnabled = false;
            ButtonSampleTest.IsEnabled = false;
        }

        private byte[] GenCmd(int cmd, int para, byte[] data)
        {
            byte[] buffer = new byte[7 + data.Length];
            buffer[0] = 0xaa;
            buffer[1] = 0xbb;
            buffer[2] = (byte)(cmd >> 8);
            buffer[3] = (byte)(cmd);
            buffer[4] = (byte)(para >> 16);
            buffer[5] = (byte)(para >> 8);
            buffer[6] = (byte)(para);
            Array.Copy(data, 0, buffer, 7, data.Length);
            return buffer;
        }

        private List<Double[]> dataList = new List<Double[]>();
        private Double[] dobv = new Double[2200];
        private Double[] doba = new Double[2200];
        private int index = 0;
        private double maxA = 0;

        private void UpdateStatus(byte[] data)
        {
            string[] types = { "数据测试中", "测试完成", "未检出", "加标测试" };
            int count = _canvases.Count;
            for (int i = 0; i < count; i++)
            {
                int offset = i * 16;
                int type = (data[offset + 8] << 8) | (data[offset + 9]);
                int time = (data[offset + 10] << 8) | (data[offset + 11]);
                int v = (data[offset + 12] << 8) | (data[offset + 13]);
                int a = (data[offset + 14] << 24 | data[offset + 15] << 16 | data[offset + 16] << 8 | data[offset + 17]);
                int density = (data[offset + 18] << 24 | data[offset + 19] << 16 | data[offset + 20] << 8 | data[offset + 21]);
                _listTime[i].Content = "时间：" + time * 0.1 + "S";
                _listMV[i].Content = "电位：" + v + "mV";
                _listUA[i].Content = "电流：" + a * (0.0001) + "uA";
                //取最大电流
                if (maxA < a * (0.0001))
                    maxA = a * (0.0001);

                if (time > 0 && time < 2200 && v > 64436)
                {
                    if (_cruveWindow == null)
                    {
                        _cruveWindow = new CurveWindow();
                        _cruveWindow.Show();
                        Global.timeValue = time * 0.1;
                        Global.xValue = v;
                        Global.yValue = a * (0.0001);
                    }
                    else
                    {
                        Global.timeValue = time * 0.1;
                        Global.xValue = v;
                        Global.yValue = a * (0.0001);
                    }
                    dobv[index] = v;
                    doba[index] = a * 0.0001;
                    index++;
                }
                else
                {
                    _cruveWindow = null;
                }
                _listDensity[i].Content = "浓度：" + density * 0.0001 + "ug/l";
                _Value = Convert.ToString(density * 0.0001);
                if (type < 4)
                    _listStage[i].Content = types[type];
                else
                    _listStage[i].Content = "检测到未知数据";

                if (type != 0)
                {
                    //计算浓度
                    //x=(b*b-4*a(c-y)-b)/2a
                    //a=A2,b=A1,c=A0,y为电流(取最高值)
                    double xV = Math.Pow(((_item.a1 * _item.a1) - (4 * _item.a2) * (_item.a0 - maxA)), 1f / 2);
                    double x = (xV - _item.a1) / (2 * _item.a2);
                    _listJudgmentReult[i].Text = (x * _item.DilutionRatio > _item.Requirements) ? "不合格" : "合格";
                    _listtextNongdu[i].Text = (x * _item.DilutionRatio).ToString("F4") + "ug/l";
                    btn_result.IsEnabled = true;
                }

                if (type == 1)
                {
                    _StandardValue = TestResultConserve.UnqualifiedOrQualified((density * 0.0001).ToString(), _item.Hole[i].SampleName, _item.Name);
                    _UnqualifiedValue = Convert.ToString(_StandardValue[0]);
                    _listStandardValue[i].Text = Convert.ToString(_StandardValue[2]);
                    _listJudgmentReult[i].Text = Convert.ToString(_StandardValue[1]);

                    _CheckValue[i, 0] = String.Format("{0:D2}", (i + 1));
                    _CheckValue[i, 1] = "重金属";
                    _CheckValue[i, 2] = _item.Name;
                    _CheckValue[i, 3] = "";// methodToString[item.Method];
                    _CheckValue[i, 4] = _Value;
                    _CheckValue[i, 5] = _item.Unit;
                    _CheckValue[i, 6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _CheckValue[i, 7] = LoginWindow._userAccount.UserName;

                    if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                        _CheckValue[i, 8] = _item.Hole[i].SampleName;
                    else
                        _CheckValue[i, 8] = "";

                    _CheckValue[i, 9] = Convert.ToString(_StandardValue[0]);
                    _CheckValue[i, 10] = Convert.ToString(_StandardValue[2]);
                    _CheckValue[i, 11] = String.Format("{0:D5}", _item.SampleNum);
                    _CheckValue[i, 12] = Convert.ToString(_StandardValue[1]);

                    if (_item.Hole[i].TaskName != null)
                        _CheckValue[i, 13] = _item.Hole[i].TaskName;
                    else
                        _CheckValue[i, 13] = "";
                    if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                        _CheckValue[i, 14] = _item.Hole[i].CompanyName;
                    else
                        _CheckValue[i, 14] = "";

                    _AllNumber = TestResultConserve.ResultConserve(_CheckValue, out syscodes);
                    if (LoginWindow._userAccount.UpDateNowing)
                    {
                        ButtonUpdate_Click(null, null);
                    }
                }
            }
        }

        class UpdateThread : ChildThread
        {
            HmMeasureWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public UpdateThread(HmMeasureWindow wnd)
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

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_READ_HEAVYMETAL:
                        if (msg.result)
                        {
                            byte[] data = msg.data;
                            try
                            {
                                wnd.UpdateStatus(data);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("Result:" + msg.result);
                        }
                        else
                        {
                            Console.WriteLine("读取检测值错误");
                        }
                        break;
                    case MsgCode.MSG_DETECTE_START_HEAVYMETAL:

                        if (msg.result)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }
                        wnd.ButtonPrev.IsEnabled = true;
                        wnd.ButtonSampleTest.IsEnabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private UIElement GenerateChannelLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border();
            border.Width = 185;
            border.Height = 410;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = _borderBrushNormal;
            border.CornerRadius = new CornerRadius(10);
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 185;
            stackPanel.Height = 380;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 185;
            grid.Height = 40;

            Label label = new Label();
            label.FontSize = 20;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.Content = "检测通道" + (channel + 1);

            Canvas rootCanvas = new Canvas();
            rootCanvas.Width = 185;
            rootCanvas.Height = 180;
            rootCanvas.Background = Brushes.Gray;
            rootCanvas.Name = "rootCanvas";

            Label labelTime = new Label();
            labelTime.Width = 185;
            labelTime.Height = 35;
            labelTime.Name = "labelTime";
            labelTime.Content = "检测时间：";
            labelTime.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            labelTime.Margin = new Thickness(0, 0, 0, 0);

            Label labelMV = new Label();
            labelMV.Width = 185;
            labelMV.Height = 35;
            labelMV.Name = "labelMV";
            labelMV.Content = "电位：";
            labelMV.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            labelMV.Margin = new Thickness(0, 40, 0, 0);

            Label labelUA = new Label();
            labelUA.Width = 185;
            labelUA.Height = 35;
            labelUA.Name = "labelUA";
            labelUA.Content = "电流：";
            labelUA.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            labelUA.Margin = new Thickness(0, 80, 0, 0);

            Label labelDensity = new Label();
            labelDensity.Width = 185;
            labelDensity.Height = 35;
            labelDensity.Name = "labelDensity";
            labelDensity.Content = "浓度值：";
            labelDensity.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            labelDensity.Margin = new Thickness(0, 120, 0, 0);

            Label labelStage = new Label();
            labelStage.Width = 185;
            labelStage.Height = 35;
            labelStage.Name = "labelStage";
            labelStage.Content = "数据";
            labelStage.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            labelStage.Margin = new Thickness(0, 160, 0, 0);

            WrapPanel wrapPannelSampleNum = new WrapPanel();
            wrapPannelSampleNum.Width = 185;
            wrapPannelSampleNum.Height = 30;

            Label labelSampleNum = new Label();
            labelSampleNum.Width = 75;
            labelSampleNum.Height = 26;
            labelSampleNum.Margin = new Thickness(0, 2, 0, 0);
            labelSampleNum.FontSize = 15;
            labelSampleNum.Content = "样品编号：";
            labelSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleNum = new TextBox();
            textBoxSampleNum.Width = 95;
            textBoxSampleNum.Height = 26;
            textBoxSampleNum.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleNum.FontSize = 15;
            textBoxSampleNum.Text = "" + sampleNum;
            textBoxSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleNum.IsReadOnly = true;
            textBoxSampleNum.Name = "sampleNum";

            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 185;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 75;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = "样品名称：";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 95;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = "" + _item.Hole[channel].SampleName;
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.IsReadOnly = true;

            //浓度
            WrapPanel wrapNongdu = new WrapPanel();
            wrapNongdu.Width = 185;
            wrapNongdu.Height = 30;

            Label labelNongdu = new Label();
            labelNongdu.Width = 75;
            labelNongdu.Height = 26;
            labelNongdu.Margin = new Thickness(0, 2, 0, 0);
            labelNongdu.FontSize = 15;
            labelNongdu.Content = "浓度：";
            labelNongdu.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textNongdu = new TextBox();
            textNongdu.Width = 95;
            textNongdu.Height = 26;
            textNongdu.Margin = new Thickness(0, 2, 0, 2);
            textNongdu.FontSize = 15;
            textNongdu.Text = "";
            textNongdu.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textNongdu.Name = "textNongdu";

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel();
            wrapJudgemtn.Width = 185;
            wrapJudgemtn.Height = 30;

            Label labelJudgment = new Label();
            labelJudgment.Width = 75;
            labelJudgment.Height = 26;
            labelJudgment.Margin = new Thickness(0, 2, 0, 0);
            labelJudgment.FontSize = 15;
            labelJudgment.Content = "判定结果：";
            labelJudgment.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textJugmentResult = new TextBox();
            textJugmentResult.Width = 95;
            textJugmentResult.Height = 26;
            textJugmentResult.Margin = new Thickness(0, 2, 0, 2);
            textJugmentResult.FontSize = 15;
            textJugmentResult.Text = "";
            textJugmentResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textJugmentResult.Name = "textJugmentResult";
            //判定标准值
            WrapPanel wrapStandValue = new WrapPanel();
            wrapStandValue.Width = 185;
            wrapStandValue.Height = 30;

            Label labelStandValue = new Label();
            labelStandValue.Width = 75;
            labelStandValue.Height = 26;
            labelStandValue.Margin = new Thickness(0, 2, 0, 0);
            labelStandValue.FontSize = 15;
            labelStandValue.Content = "标准值：";
            labelStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textStandValue = new TextBox();
            textStandValue.Width = 95;
            textStandValue.Height = 26;
            textStandValue.Margin = new Thickness(0, 2, 0, 2);
            textStandValue.FontSize = 15;
            textStandValue.Text = _item.Requirements.ToString();
            textStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textStandValue.Name = "textStandValue";
            rootCanvas.Children.Add(labelTime);
            rootCanvas.Children.Add(labelMV);
            rootCanvas.Children.Add(labelUA);
            rootCanvas.Children.Add(labelDensity);
            rootCanvas.Children.Add(labelStage);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapJudgemtn.Children.Add(labelJudgment);
            wrapJudgemtn.Children.Add(textJugmentResult);
            wrapNongdu.Children.Add(labelNongdu);
            wrapNongdu.Children.Add(textNongdu);
            wrapStandValue.Children.Add(labelStandValue);
            wrapStandValue.Children.Add(textStandValue);
            grid.Children.Add(label);

            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(rootCanvas);
            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);
            stackPanel.Children.Add(wrapJudgemtn);
            stackPanel.Children.Add(wrapNongdu);
            stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;
            return border;
        }

        #region-- UpDataNowing

        /// <summary>
        /// 30s上传超时
        /// </summary>
        private void UploadTimeOut(object sender, EventArgs e)
        {
            if (LabelInfo.Content.Equals("正在上传..."))
            {
                LabelInfo.Content = "上传超时，请检测连接设置！";
                _DataTimer.Stop();
                _msgThread.Stop();
            }
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_DataTimer == null)
            {
                _DataTimer = new DispatcherTimer();
                _DataTimer.Interval = TimeSpan.FromSeconds(30);
                _DataTimer.Tick += new EventHandler(UploadTimeOut);
                _DataTimer.Start();
            }
            LabelInfo.Content = "正在上传...";
            tlsttResultSecondOpr Rs = new tlsttResultSecondOpr();
            try
            {
                DataTable dt = Rs.GetAsDataTable("", "", 3, _AllNumber);
                Message msg = new Message();
                msg.what = MsgCode.MSG_UPLOAD;
                msg.obj1 = Global.samplenameadapter[0];
                msg.table = dt;
                Global.updateThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region-- PrintNowing
        private PrintHelper.Report GenerateReport()
        {
            PrintHelper.Report report = new PrintHelper.Report();
            report.ItemName = _item.Name;
            report.ItemCategory = "重金属";
            report.User = LoginWindow._userAccount.UserName;
            report.Unit = _item.Unit;
            report.Date = _date.ToString("yyyy-MM-dd HH:mm:ss");
            //report.Judgment = item.Hole[0].SampleName;
            int sampleNum = _item.SampleNum;
            for (int i = 0; i < 1; ++i)
            {
                report.SampleName.Add(_item.Hole[i].SampleName);
                report.SampleNum.Add(String.Format("{0:D5}", sampleNum++));

                report.JudgmentTemp.Add("" + _UnqualifiedValue);
                report.Result.Add("" + _Value);
            }
            return report;
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintHelper.Report report = GenerateReport();
                byte[] buffer = report.GeneratePrintBytes();
                Message msg = new Message();
                msg.what = MsgCode.MSG_PRINT;
                msg.str1 = Global.strPRINTPORT;
                msg.data = buffer;
                msg.arg1 = 0;
                msg.arg2 = buffer.Length;
                Global.printThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        class MsgThread : ChildThread
        {
            HmMeasureWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(HmMeasureWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_PRINT:
                        if (!msg.result)
                            wnd.LabelInfo.Content = "打印失败，请检查打印端口是否正确。";
                        break;

                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                            wnd.LabelInfo.Content = "上传成功";
                        else
                            wnd.LabelInfo.Content = "上传失败";
                        break;

                    default:
                        break;
                }
            }
        }

        private void btn_result_Click(object sender, RoutedEventArgs e)
        {
            dataList.Add(dobv);
            dataList.Add(doba);
            HmReportWindow window = new HmReportWindow();
            window._item = _item;
            window._dataList = dataList;
            window.Owner = this;
            window.ShowInTaskbar = false;
            window.ShowDialog();
        }

    }
}
