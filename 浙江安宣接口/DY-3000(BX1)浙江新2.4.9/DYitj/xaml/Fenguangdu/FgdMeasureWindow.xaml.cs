using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AIO.xaml.Dialog;
using AIO.xaml.Fenguangdu;
using com.lvrenyang;

namespace AIO
{
    public partial class FgdMeasureWindow : Window
    {

        #region 全局变量

        public DYFGDItemPara _item = null;
        private UpdateADThread _updateADThread;
        private List<Label> _listDetectResult = null;
        private List<Label> _listXiguangdu = null;
        private List<Label> _listTouguanglv = null;
        private List<Label> _listSampleNum = null;

        /// <summary>
        /// 查看曲线
        /// </summary>
        private List<Image> _listBtnCruve = null;
        /// <summary>
        /// 单独启动按钮List
        /// </summary>
        private List<Button> _listBtnSeparate = null;
        /// <summary>
        /// 单通道记录每个通道的单独启动时间
        /// </summary>
        private List<DateTime> _liststartTime = null;
        /// <summary>
        /// 单通道过去多少ms
        /// </summary>
        private List<double> _listpassms = null;
        /// <summary>
        /// 是否所有通道都已检测完毕
        /// </summary>
        //List<bool> _listIsTestOk = null;
        /// <summary>
        /// 单通道是否开始测试
        /// </summary>
        private List<bool> _listIsStart = null;
        /// <summary>
        /// 单通道当前选择的通道坐标
        /// </summary>
        private int _currentChoose = -1;
        /// <summary>
        /// 单通道编号
        /// </summary>
        private int _testNum = 0;
        /// <summary>
        /// 单通道显示动态曲线
        /// </summary>
        private int _curveChoose = 0;
        /// <summary>
        /// 单通道曲线编号
        /// </summary>
        private int _testCurveNum = 0;
        /// <summary>
        /// 是否从第一通道的对照启动对照方法
        /// </summary>
        private bool _isSeparateContrast = false;

        private int _adClear = 0;
        /// <summary>
        ///  对照
        /// </summary>
        private bool _contrast = false;
        /// <summary>
        /// 样品
        /// </summary>
        private bool _sample = false;
        /// <summary>
        /// 是否有点击过单通道测试按钮
        /// </summary>
        private bool _IsSeparateTest = false;
        private FgdCaculate.HolesAD _HolesFullAD, _HolesCurAD;
        /// <summary>
        /// _firstATs初始值 _lastATs最后取值 _curATs当前检测值
        /// </summary>
        private FgdCaculate.AT[] _firstATs, _lastATs, _curATs;
        /// <summary>
        /// 记录启动时间
        /// </summary>
        private DateTime _startTime;
        /// <summary>
        /// 预热毫秒 检测毫秒
        /// </summary>
        private double _preHeatms, _realHeatms;
        /// <summary>
        /// 过去多少ms
        /// </summary>
        private double _passms;
        private int _num = 0;
        private string logType = "FgdMeasureWindow-error";

        /// <summary>
        /// 实时曲线
        /// </summary>
        //private ShowCurve _showCurve = new ShowCurve();

        private RealTimeCurve _showTimeCurve = new RealTimeCurve();
        private int _waveIndex = 0;
        #endregion

        public FgdMeasureWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void Initial()
        {
            try
            {
                btnPlayer.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
                this.LabelTitle.Content = "正在检测  " + _item.Name;
                switch (_item.Wave)
                {
                    case 410:
                        _waveIndex = 0;
                        break;
                    case 536:
                        _waveIndex = 1;
                        break;
                    case 595:
                        _waveIndex = 2;
                        break;
                    case 620:
                        _waveIndex = 3;
                        break;
                    default:
                        break;
                }
                if (2 == _item.Method)
                {
                    MessageBox.Show("暂不支持子项目法!");
                    this.Close();
                }
                else if (3 == _item.Method)
                {
                    this.ButtonSampleContrast.Visibility = System.Windows.Visibility.Visible;
                    this.btnPlayer.Margin = ButtonBirefDescription.Margin;
                    this.ButtonBirefDescription.Margin = ButtonSampleContrast.Margin;
                }
                else if (4 == _item.Method)
                {
                    this.ButtonSampleContrast.Visibility = System.Windows.Visibility.Visible;
                    this.btnPlayer.Margin = ButtonBirefDescription.Margin;
                    this.ButtonBirefDescription.Margin = ButtonSampleContrast.Margin;
                }
                //抑制率法和标准曲线法时 若没有进行对照测试则禁用样品按钮；
                this.ButtonSampleTest.IsEnabled = (_item.Method == 0) ? ((_item.ir.RefDeltaA == Double.MinValue) ? false : true) : ((_item.Method == 1 && _item.sc.RefA == Double.MinValue) ? false : true);
                _listDetectResult = new List<Label>();
                _listXiguangdu = new List<Label>();
                _listTouguanglv = new List<Label>();
                _listSampleNum = new List<Label>();
                _listBtnSeparate = new List<Button>();
                _listBtnCruve = new List<Image>();
                //抑制率法和动力学法时 启用单通道测试功能；
                if (_item.Method == 0 || _item.Method == 3)
                {
                    _liststartTime = new List<DateTime>();
                    _listpassms = new List<double>();
                    //_listIsTestOk = new List<bool>();
                    _listIsStart = new List<bool>();
                    for (int i = 0; i < Global.deviceHole.HoleCount; i++)
                    {
                        //_listIsTestOk.Add(false);
                        _liststartTime.Add(new DateTime());
                        _listpassms.Add(0);
                        _listIsStart.Add(false);
                    }
                    this.LabelSeparateTest.Visibility = Visibility.Visible;
                }
                else
                    this.LabelSeparateTest.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 更新对照值label
        /// </summary>
        private void UpdateLable()
        {
            try
            {
                if (_item.Method == 0 && _item.ir.RefDeltaA != Double.MinValue)
                    this.LabelInfo.Content = "对照值：" + String.Format("{0:F3}", _item.ir.RefDeltaA);
                else if (_item.Method == 1 && _item.sc.RefA != Double.MinValue)
                    this.LabelInfo.Content = "对照值：" + String.Format("{0:F3}", _item.sc.RefA);
                else
                    this.LabelInfo.Content = "";
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initial();
                UpdateLable();
                Global.IsContrast = false;
                string o = string.Empty;
                if (null == _item)
                    return;
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        _num += 1;
                        UIElement element = GenerateSampleMeasureForm(String.Format("{0:D2}", (i + 1)), String.Format("{0:D5}", (sampleNum++)), _item.Hole[i].SampleName);
                        this.StackPanelMeasure.Children.Add(element);
                        _listDetectResult.Add(UIUtils.GetChildObject<Label>(element, "detectresult"));
                        _listXiguangdu.Add(UIUtils.GetChildObject<Label>(element, "xiguangdu"));
                        _listTouguanglv.Add(UIUtils.GetChildObject<Label>(element, "touguanglv"));
                        _listSampleNum.Add(UIUtils.GetChildObject<Label>(element, "sampleNum"));
                        _listBtnSeparate.Add(UIUtils.GetChildObject<Button>(element, "separateTest"));
                        _listBtnCruve.Add(UIUtils.GetChildObject<Image>(element, "showCurveTest"));
                    }
                    else
                    {
                        _listTouguanglv.Add(null);
                        _listXiguangdu.Add(null);
                        _listDetectResult.Add(null);
                        _listSampleNum.Add(null);
                        _listBtnSeparate.Add(null);
                        _listBtnCruve.Add(null);
                    }
                }
                _updateADThread = new UpdateADThread(this);
                _updateADThread.Start();
                Message msg = new Message
                {
                    what = MsgCode.MSG_READ_AD_CYCLE,
                    str1 = Global.strADPORT
                };
                Global.workThread.StartWhileRead(msg, _updateADThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Global.workThread.StopWhileRead();
                if (null != _updateADThread)
                    _updateADThread.Stop();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, _item.Name);
            try
            {
                if (File.Exists(path))
                {
                    TechnologeDocument window = new TechnologeDocument
                    {
                        path = path,
                        ShowInTaskbar = false,
                        Topmost = true,
                        Owner = this
                    };
                    window.Show();
                }
                else
                {
                    Global.IsOpenPrompt = true;
                    PromptWindow window = new PromptWindow
                    {
                        _HintStr = _item.HintStr
                    };
                    window.Show();
                }
            }
            catch (Exception)
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow
                {
                    _HintStr = _item.HintStr
                };
                window.Show();
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            //标记COM3未使用
            CFGUtils.SaveConfig("FgIsStart", "0");
            System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));
            //_showCurve.CloseWindow();
            _showTimeCurve.CloseWindow();
            this.Close();
        }

        // 样品
        private void ButtonSampleTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_sample)
                {
                    Global.IsContrast = _contrast = false;
                    if (_item.Method == 4)
                    {
                        _sample = true;
                        _startTime = DateTime.Now;
                        _firstATs = null;
                        _lastATs = null;
                        Updates();
                    }
                    else
                    {
                        _sample = true;
                        this.ButtonSampleClear.IsEnabled = false;
                        this.ButtonSampleContrast.IsEnabled = false;
                        if (0 == _item.Method)
                        {
                            _preHeatms = _item.ir.PreHeatTime * 1000;
                            _realHeatms = _item.ir.DetTime * 1000;
                        }
                        else if (1 == _item.Method)
                        {
                            _preHeatms = _item.sc.PreHeatTime * 1000;
                            _realHeatms = _item.sc.DetTime * 1000;
                        }
                        else if (3 == _item.Method)
                        {
                            _preHeatms = _item.dn.PreHeatTime * 1000;
                            _realHeatms = _item.dn.DetTime * 1000;
                        }
                        else if (4 == _item.Method)
                        {
                            _preHeatms = 0;
                            _realHeatms = 0;
                        }
                        _startTime = DateTime.Now;
                        _firstATs = null;
                        _lastATs = null;

                        for (int i = 0; i < Global.deviceHole.HoleCount; i++)
                        {
                            if (_liststartTime == null || _listIsStart == null)
                                break;
                            if (_liststartTime[i] == new DateTime() && _listIsStart[i] == false)
                            {
                                _liststartTime[i] = DateTime.Now;
                                _listIsStart[i] = true;
                            }
                        }
                    }
                }
                else if (_item.Method == 4)
                {
                    _sample = true;
                    _startTime = DateTime.Now;
                    _firstATs = null;
                    _lastATs = null;
                    Updates();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 对照 控制按键，如果检测方法不是这三个，就没有对照按钮。
        private void ButtonSampleContrast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_contrast)
                {
                    //if (_item.Method == 0)
                    //    _item.ir.RefDeltaA = Double.MinValue;
                    //else if (_item.Method == 1)
                    //    _item.sc.RefA = Double.MinValue;
                    Global.IsContrast = _contrast = true;
                    this.ButtonSampleClear.IsEnabled = false;
                    this.ButtonSampleTest.IsEnabled = false;
                    if (0 == _item.Method)
                    {
                        _preHeatms = _item.ir.PreHeatTime * 1000;
                        _realHeatms = _item.ir.DetTime * 1000;
                    }
                    else if (1 == _item.Method)
                    {
                        _preHeatms = _item.sc.PreHeatTime * 1000;
                        _realHeatms = _item.sc.DetTime * 1000;
                    }
                    else if (3 == _item.Method)
                    {
                        _preHeatms = _item.dn.PreHeatTime * 1000;
                        _realHeatms = _item.dn.DetTime * 1000;
                    }
                    _startTime = DateTime.Now;
                    _firstATs = null;
                    _lastATs = null;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 清零
        private void ButtonSampleClear_Click(object sender, RoutedEventArgs e)
        {
            Global.IsClear = true;
            Global.IsContrast = _contrast = false;
            _adClear = 10;
            try
            {
                this.ButtonSampleClear.Content = "" + _adClear;
                this.ButtonSampleClear.IsEnabled = false;
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                        _listDetectResult[i].Content = "";
                }
                ButtonSampleTest.IsEnabled = false;
                ButtonSampleContrast.IsEnabled = false;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 根据给定的通道，样品编号，样品名称，生成UIElement
        private UIElement GenerateSampleMeasureForm(string channel, string sampleNum, string sampleName)
        {
            StackPanel stackPanel = new StackPanel();
            try
            {
                stackPanel.Orientation = Orientation.Horizontal;
                //通道
                Label labelChannel = new Label
                {
                    Content = channel,
                    FontSize = 20,
                    Width = 60,
                    Name = "channel",
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                //样品编号
                Label labelSampleNum = new Label
                {
                    Content = sampleNum,
                    FontSize = 20,
                    Width = 100,
                    Name = "sampleNum",
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                //样品名称
                Label labelSampleName = new Label
                {
                    Content = sampleName,
                    FontSize = 20,
                    Width = 100,
                    Name = "sampleName",
                    ToolTip = sampleName,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                //吸光度
                Label labelXgd = new Label
                {
                    Content = "0",
                    FontSize = 20,
                    Width = 100,
                    Name = "xiguangdu",
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                //透光率
                Label labelTgl = new Label
                {
                    Content = "100%",
                    FontSize = 20,
                    Width = 100,
                    Name = "touguanglv",
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                //检测时间
                Label labelResult = new Label
                {
                    Content = "",
                    FontSize = 20,
                    Width = 100,
                    Name = "detectresult",
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };
                Grid.SetColumn(labelResult, 5);

                //单通道检测
                Button separateTest = new Button
                {
                    Content = "样   品 " + channel,
                    FontSize = 20,
                    Width = 120,
                    Name = "separateTest"
                };
                separateTest.Click += SeparateStart;
                separateTest.Visibility = (_item.Method == 0 || _item.Method == 3) ? Visibility.Visible : Visibility.Collapsed;
                if (separateTest.Visibility == Visibility.Collapsed) labelResult.Width = 105;

                Image showCurve = new Image
                {
                    Tag = channel,
                    Name = "showCurveTest"
                };
                showCurve.MouseDown += ShowCurve_MouseDown;
                showCurve.ToolTip = "点击查看实时曲线";
                //绑定图片
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"/DY-Detector;component/assets/Curve.png", UriKind.Relative);
                bi.EndInit();
                showCurve.Source = bi;
                showCurve.Height = 24;

                stackPanel.Children.Add(labelChannel);
                stackPanel.Children.Add(labelSampleNum);
                stackPanel.Children.Add(labelSampleName);
                stackPanel.Children.Add(labelXgd);
                stackPanel.Children.Add(labelTgl);
                stackPanel.Children.Add(labelResult);
                stackPanel.Children.Add(separateTest);
                stackPanel.Children.Add(showCurve);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
            return stackPanel;
        }

        private void ShowCurve_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //获取当前通道
            Image img = sender as Image;
            _testCurveNum = int.Parse(img.Tag.ToString());
            for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
            {
                if (_listBtnCruve == null) break;
                if (!_item.Hole[i].Use) continue;
                if (i == _testCurveNum - 1)
                {
                    _curveChoose = i;
                    break;
                }
            }

            List<Label> SampleNameList = UIUtils.GetChildObjects<Label>(StackPanelMeasure, "sampleName");
            //if (!Global.IsSettingCurve)
            //    _showTimeCurve = new RealTimeCurve();//_showCurve = new ShowCurve();
            //else
            //    _showTimeCurve.Hide();//_showCurve.Hide();
            //_showCurve.Title = string.Format("通道:{0} 样品:{1} 实时曲线图谱", _testCurveNum, SampleNameList[_testCurveNum - 1].Content);
            //_showCurve.x = 0;
            //_showCurve.Show();
            try
            {
                if (!Global.IsSettingCurve)
                    _showTimeCurve = new RealTimeCurve();
                else
                    _showTimeCurve.Close();
                _showTimeCurve = new RealTimeCurve
                {
                    //_showTimeCurve.Title = string.Format("通道:{0} 样品:{1} 实时曲线图谱", _testCurveNum, SampleNameList[_testCurveNum - 1].Content);
                    _x = 0
                };
                _showTimeCurve.Show();
                Global.IsSettingCurve = true;
            }
            catch (Exception)
            {
                _showTimeCurve = new RealTimeCurve
                {
                    //_showTimeCurve.Title = string.Format("通道:{0} 样品:{1} 实时曲线图谱", _testCurveNum, SampleNameList[_testCurveNum - 1].Content);
                    _x = 0
                };
                _showTimeCurve.Show();
                Global.IsSettingCurve = true;
            }
        }

        private void SeparateStart(object sender, RoutedEventArgs e)
        { 
            try
            {
                string str = sender.ToString().Substring(32, 1);
                if (str.Equals("对"))
                {
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_listBtnSeparate == null || _liststartTime == null || _listIsStart == null)
                            break;
                        if (_item.Hole[i].Use)
                        {
                            _listBtnSeparate[i].IsEnabled = false;
                            _liststartTime[i] = DateTime.Now;
                            _currentChoose = i;
                            _item.Hole[i].IsTest = _listIsStart[i] = true;//当前通道开始检测
                            break;
                        }
                    }
                    Global.IsContrast = _contrast = _isSeparateContrast = true;
                    ButtonSampleContrast.IsEnabled = false;
                    _preHeatms = _item.ir.PreHeatTime * 1000;
                    _realHeatms = _item.ir.DetTime * 1000;
                    _startTime = DateTime.Now;
                    _firstATs = null;
                    _lastATs = null;
                }
                else
                {
                    // 1：获取预热时间和检测时间
                    if (0 == _item.Method)
                    {
                        _preHeatms = _item.ir.PreHeatTime * 1000;
                        _realHeatms = _item.ir.DetTime * 1000;
                    }
                    else if (3 == _item.Method)
                    {
                        _preHeatms = _item.dn.PreHeatTime * 1000;
                        _realHeatms = _item.dn.DetTime * 1000;
                    }
                    // 2：获取单通道启动时间
                    string strBtnNum = sender.ToString().Substring(sender.ToString().Length - 2);
                    _testNum = int.Parse(strBtnNum);
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_listBtnSeparate == null || _liststartTime == null || _listIsStart == null)
                            break;
                        if (i == _testNum - 1)
                        {
                            _listBtnSeparate[i].IsEnabled = false;
                            _liststartTime[i] = DateTime.Now;
                            _currentChoose = i;
                            _item.Hole[i].IsTest = _listIsStart[i] = true;//当前通道开始检测
                            break;
                        }
                    }
                    Global.IsContrast = false;
                }
                _IsSeparateTest = true;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 仅用于没有设备连接时的系数法
        /// </summary>
        /// <param name="data"></param>
        private void Updates()
        {
            try
            {
                // 更新吸光度和透光率
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        _listXiguangdu[i].Content = "0.000";
                        _listTouguanglv[i].Content = "100%";
                    }
                }
                // 点了样品，并且不需要预热时间和检测时间，就是系数法，需要专门的处理
                if (DYFGDItemPara.METHOD_CO == _item.Method)
                {
                    _firstATs = _lastATs = _curATs;
                    _sample = false;
                    ButtonSampleClear.IsEnabled = true;
                    ButtonSampleTest.IsEnabled = true;
                    ButtonSampleContrast.IsEnabled = true;
                    JumpToResult(2);
                    UpdateSampleNumUI();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 根据给定的data，更新界面
        private void Update(byte[] data)
        {
            try
            {
                //计算光度能量
                List<List<double>> dbList = new List<List<double>>();
                double[] rtnData = new double[64];
                int length = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    rtnData[length] = (data[i + 1] * 256 + data[i]) * 1000.0 / 0xFFFF;
                    i++;
                    length++;
                }
                for (int i = 0; i < rtnData.Length; i++)
                {
                    List<double> db = new List<double>();
                    db.Add(rtnData[i]);
                    db.Add(rtnData[i + 1]);
                    db.Add(rtnData[i + 2]);
                    db.Add(rtnData[i + 3]);
                    i += 3;
                    dbList.Add(db);
                }

                TimeSpan ts = DateTime.Now - _startTime;
                _passms = ts.TotalMilliseconds;

                //记录已过去多少秒
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_listIsStart == null || _liststartTime == null || _listpassms == null)
                        break;
                    if (_listIsStart[i])
                        _listpassms[i] = (DateTime.Now - _liststartTime[i]).TotalMilliseconds;
                }

                // 更新数据 HolesCurAD curATs
                HandleAd(data);

                // 更新满值AD
                if (0 == _adClear)
                {
                    Global.IsClear = false;
                    _HolesFullAD = _HolesCurAD;
                    --_adClear;
                    this.ButtonSampleClear.Content = "清  零";
                    this.ButtonSampleClear.IsEnabled = true;
                    if (_item.Method == 0 || _item.Method == 1)
                        this.ButtonSampleTest.IsEnabled = (_item.Method == 0) ? ((_item.ir.RefDeltaA == Double.MinValue) ? false : true) : ((_item.Method == 1 && _item.sc.RefA == Double.MinValue) ? false : true);
                    this.ButtonSampleContrast.IsEnabled = true;
                }
                else if (_adClear > 0)
                {
                    --_adClear;
                    this.ButtonSampleClear.Content = "" + _adClear;
                }
                if (_item.Method != 0 && _item.Method != 1)
                    this.ButtonSampleTest.IsEnabled = _adClear <= 0 ? true : false;

                bool isContrast = true, contrast = true;//未对照、第一次进入检测界面第一通道显示为对照
                // 更新吸光度和透光率
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        //对照后单独启动按钮才可用
                        if (_listBtnSeparate != null && _listIsStart != null)
                        {
                            if (isContrast && _item.Method == 0 && _item.ir.RefDeltaA == Double.MinValue)
                            {
                                _listBtnSeparate[i].IsEnabled = _isSeparateContrast ? false : true;
                                _listBtnSeparate[i].Content = "对    照 " + String.Format("{0:D2}", (i + 1));
                                isContrast = false;
                            }
                            else if (_isSeparateContrast && _item.Method == 0)
                                _listBtnSeparate[i].IsEnabled = !_listIsStart[i];
                            else
                            {
                                _listBtnSeparate[i].Content = "样    品 " + String.Format("{0:D2}", (i + 1));
                                _listBtnSeparate[i].IsEnabled = (_item.Method == 0) ? ((_item.ir.RefDeltaA == Double.MinValue) ? false : !_listIsStart[i]) : !_listIsStart[i];
                            }
                        }
                        if (_listpassms != null)
                        {
                            if (_listpassms[i] <= _preHeatms + _realHeatms)
                            {
                                _listXiguangdu[i].Content = FgdCaculate.A_To_String(_curATs[i].a);
                                _listTouguanglv[i].Content = FgdCaculate.T_To_String(_curATs[i].t);
                                //更新曲线
                                if (Global.IsSettingCurve && _curveChoose == i)
                                {
                                    _showTimeCurve.SettingValue(_curATs[i].a, _curATs[i].t, (int)dbList[i][_waveIndex]);
                                    //_showCurve.UpdateCurve(_curATs[i].a, _curATs[i].t, i);
                                }
                            }
                        }
                        else
                        {
                            _listXiguangdu[i].Content = FgdCaculate.A_To_String(_curATs[i].a);
                            _listTouguanglv[i].Content = FgdCaculate.T_To_String(_curATs[i].t);
                            //更新曲线
                            if (Global.IsSettingCurve && _curveChoose == i)
                            {
                                _showTimeCurve.SettingValue(_curATs[i].a, _curATs[i].t, (int)dbList[i][_waveIndex]);
                                //_showCurve.UpdateCurve(_curATs[i].a, _curATs[i].t, i);
                            }
                        }

                        if (_contrast && contrast)
                        {
                            contrast = false;
                            _listBtnSeparate[i].Content = "对    照 " + String.Format("{0:D2}", (i + 1));
                        }
                    }
                }
                // 更新firstATs和lastATs
                if ((_passms > _preHeatms) && (null == _firstATs))
                    _firstATs = _curATs;
                if ((_passms > (_preHeatms + _realHeatms)) & (null == _lastATs))
                    _lastATs = _curATs;
                if (_contrast)
                {
                    // 抑制率法 从第一通道进入对照方法
                    if (_isSeparateContrast && _item.Method == 0 && _item.ir.RefDeltaA == Double.MinValue)
                    {
                        //MessageBox.Show("!!");
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_item.Hole[i].Use && _listIsStart[i])
                            {
                                _item.Hole[i].IsTest = true;
                                if (_listpassms[i] < _preHeatms)
                                {
                                    _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _listpassms[i]) / 1000) + "s";
                                    if (_firstATs == null)
                                        _firstATs = _curATs; // 取最后一项
                                    else
                                        _firstATs[i] = _curATs[i];
                                }
                                else if (_listpassms[i] < (_preHeatms + _realHeatms))
                                {
                                    _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + _realHeatms - _listpassms[i]) / 1000) + "s";
                                    if (_lastATs == null)
                                        _lastATs = _curATs;
                                    else
                                        _lastATs[i] = _curATs[i];
                                }
                                else
                                    _listDetectResult[i].Content = "";
                                if (_listBtnSeparate != null)
                                    _listBtnSeparate[i].IsEnabled = false;
                            }
                        }
                        if (IsTestOk())
                        {
                            _contrast = false;
                            ButtonSampleClear.IsEnabled = true;
                            ButtonSampleTest.IsEnabled = true;
                            ButtonSampleContrast.IsEnabled = true;
                            JumpToResult(1);
                            Reset();
                            UpdateSampleNumUI();
                        }
                    }
                    else
                    {
                        // 对照时除第一个通道外其他的进行样品测试
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_item.Hole[i].Use)
                            {
                                _item.Hole[i].IsTest = true;
                                if (_passms < _preHeatms)
                                {
                                    _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _passms) / 1000) + "s";
                                    if (_firstATs == null)
                                        _firstATs = _curATs; // 取最后一项
                                    else
                                        _firstATs[i] = _curATs[i];
                                }
                                else if (_passms < (_preHeatms + _realHeatms))
                                {
                                    _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + _realHeatms - _passms) / 1000) + "s";
                                    if (_lastATs == null)
                                        _lastATs = _curATs;
                                    else
                                        _lastATs[i] = _curATs[i];
                                }
                                else
                                    _listDetectResult[i].Content = "";
                                if (_listBtnSeparate != null)
                                    _listBtnSeparate[i].IsEnabled = false;
                            }
                        }
                        if (_passms >= _preHeatms + _realHeatms)
                        {
                            _contrast = false;
                            ButtonSampleClear.IsEnabled = true;
                            ButtonSampleTest.IsEnabled = true;
                            ButtonSampleContrast.IsEnabled = true;
                            JumpToResult(1);
                            UpdateSampleNumUI();
                        }
                    }
                    #region 旧的对照显示
                    //int i = 0;
                    //for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                    //{
                    //    if (_item.Hole[i].Use)
                    //        break;
                    //}
                    //if (_passms < _preHeatms)
                    //{
                    //    _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _passms) / 1000) + "s";
                    //    _firstATs = _curATs; // 取最后一项
                    //}
                    //else if (_passms < (_preHeatms + realHeatms))
                    //{
                    //    _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + realHeatms - _passms) / 1000) + "s";
                    //    _lastATs = _curATs;
                    //}
                    //else
                    //{
                    //    _listDetectResult[i].Content = "";
                    //}
                    #endregion
                }
                else if (_sample && !_IsSeparateTest)
                {
                    // 点了样品，并且不需要预热时间和检测时间，就是系数法，需要专门的处理
                    if (DYFGDItemPara.METHOD_CO == _item.Method)
                    {
                        _firstATs = _lastATs = _curATs;
                        _sample = false;
                        ButtonSampleClear.IsEnabled = true;
                        ButtonSampleTest.IsEnabled = true;
                        ButtonSampleContrast.IsEnabled = true;
                        JumpToResult(2);
                        UpdateSampleNumUI();
                    }
                    else
                    {
                        // 样品 所有通道
                        for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                        {
                            if (_item.Hole[i].Use)
                            {
                                _item.Hole[i].IsTest = true;
                                if (_passms < _preHeatms)
                                {
                                    _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _passms) / 1000) + "s";
                                    if (_firstATs == null)
                                        _firstATs = _curATs; // 取最后一项
                                    else
                                        _firstATs[i] = _curATs[i];
                                }
                                else if (_passms < (_preHeatms + _realHeatms))
                                {
                                    _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + _realHeatms - _passms) / 1000) + "s";
                                    if (_lastATs == null)
                                        _lastATs = _curATs;
                                    else
                                        _lastATs[i] = _curATs[i];
                                }
                                else
                                    _listDetectResult[i].Content = "";
                                _listBtnSeparate[i].IsEnabled = false;
                            }
                        }
                        if (_passms >= _preHeatms + _realHeatms)
                        {
                            _sample = false;
                            ButtonSampleClear.IsEnabled = true;
                            ButtonSampleTest.IsEnabled = true;
                            ButtonSampleContrast.IsEnabled = true;
                            JumpToResult(2);
                            Reset();
                            UpdateSampleNumUI();
                        }
                    }
                }
                else if (_IsSeparateTest && !_sample)
                {
                    // 样品 单通道检测
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_item.Hole[i].Use && _listIsStart[i])
                        {
                            if (_listpassms[i] < _preHeatms)
                            {
                                _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _listpassms[i]) / 1000) + "s";
                                if (_firstATs == null)
                                    _firstATs = _curATs; // 取最后一项
                                else
                                    _firstATs[i] = _curATs[i];
                            }
                            else if (_listpassms[i] < (_preHeatms + _realHeatms))
                            {
                                _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + _realHeatms - _listpassms[i]) / 1000) + "s";
                                if (_lastATs == null)
                                    _lastATs = _curATs;
                                else
                                    _lastATs[i] = _curATs[i];
                            }
                            else
                                _listDetectResult[i].Content = "";
                        }
                    }
                    if (IsTestOk())
                    {
                        _sample = false;
                        ButtonSampleClear.IsEnabled = true;
                        ButtonSampleTest.IsEnabled = true;
                        ButtonSampleContrast.IsEnabled = true;
                        JumpToResult(2);
                        Reset();
                        UpdateSampleNumUI();
                    }
                }
                else if (_IsSeparateTest && _sample)
                {
                    //有单通道测试和样品按钮测试
                    for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_item.Hole[i].Use && _listIsStart[i])
                        {
                            _item.Hole[i].IsTest = true;
                            if (_listpassms[i] < _preHeatms)
                            {
                                _listDetectResult[i].Content = "预热：" + (int)((_preHeatms - _listpassms[i]) / 1000) + "s";
                                if (_firstATs == null)
                                    _firstATs = _curATs; // 取最后一项
                                else
                                    _firstATs[i] = _curATs[i];
                            }
                            else if (_listpassms[i] < (_preHeatms + _realHeatms))
                            {
                                _listDetectResult[i].Content = "检测：" + (int)((_preHeatms + _realHeatms - _listpassms[i]) / 1000) + "s";
                                if (_lastATs == null)
                                    _lastATs = _curATs;
                                else
                                    _lastATs[i] = _curATs[i];
                            }
                            else
                                _listDetectResult[i].Content = "";
                        }
                    }

                    if (IsTestOk())
                    {
                        _sample = false;
                        ButtonSampleClear.IsEnabled = true;
                        ButtonSampleTest.IsEnabled = true;
                        ButtonSampleContrast.IsEnabled = true;
                        JumpToResult(2);
                        Reset();
                        UpdateSampleNumUI();
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private bool IsTestOk()
        {
            bool isTestOK = false;
            try
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; i++)
                {
                    if (_item.Hole[i].Use)
                    {
                        if (_listIsStart[i])
                        {
                            if (_listpassms[i] >= _preHeatms + _realHeatms)
                                isTestOK = true;
                            else
                            {
                                isTestOK = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            return isTestOK;
        }

        /// <summary>
        /// 使用了单通道测试后需要重置检测界面
        /// </summary>
        private void Reset()
        {
            try
            {
                _isSeparateContrast = _IsSeparateTest = false;
                for (int i = 0; i < Global.deviceHole.HoleCount; i++)
                {
                    _item.Hole[i].IsTest = false;
                    if (_listIsStart == null || _liststartTime == null)
                        break;
                    _listIsStart[i] = false;
                    _liststartTime[i] = new DateTime();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }
        /// <summary>
        /// 跳转结果，回来的时候需要更新样品编号。
        /// </summary>
        private void UpdateSampleNumUI()
        {
            try
            {
                UpdateLable();
                // 更新样品编号
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < _listSampleNum.Count; ++i)
                {
                    //单通道测试重置时间
                    if (_listpassms != null)
                        _listpassms[i] = 0;
                    if (_item.Hole[i].Use)
                        _listSampleNum[i].Content = String.Format("{0:D5}", (sampleNum++));
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 读取data，处理AD，根据item，把t值和a值
        private void HandleAd(byte[] data)
        {
            try
            {
                _HolesCurAD = FgdCaculate.NewRawDataToAD(data);
                if (null == _HolesFullAD)
                    _HolesFullAD = _HolesCurAD;
                FgdCaculate.TLimit tLimit = new FgdCaculate.TLimit(1.2, 0.99, 1.01);
                FgdCaculate.ALimit aLimit = new FgdCaculate.ALimit(5);
                FgdCaculate.HolesT t = FgdCaculate.CaculateT(_HolesCurAD, _HolesFullAD, tLimit);
                FgdCaculate.HolesA a = FgdCaculate.CaculateA(_HolesCurAD, _HolesFullAD, t, aLimit);
                FgdCaculate.AT[] holeATs = new FgdCaculate.AT[Global.deviceHole.HoleCount];
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    int row = i / 8;
                    int col = i % 8;
                    if (_item.Hole[i].Use)
                    {
                        holeATs[i].t = t.tValues[row][col][_item.Hole[i].WaveIndex]; // 波长对应的孔位索引
                        holeATs[i].a = a.aValues[row][col][_item.Hole[i].WaveIndex]; // 波长对应的孔位索引
                    }
                }
                _curATs = holeATs;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 跳转显示结果, resultType 1 对照 2 样品
        private void JumpToResult(int resultType)
        {
            try
            {
                if (_showTimeCurve != null)
                {
                    _showTimeCurve.Hide();
                    Global.IsSettingCurve = false;
                }
                //if (_showCurve != null)
                //{
                //    _showCurve.Hide();
                //    Global.IsSettingCurve = false;
                //}
                //_showCurve.Hide();

                CFGUtils.SaveConfig("FgIsStart", "0");
                System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));

                FgdReportWindow window = new FgdReportWindow
                {
                    _item = _item,
                    _firstATs = _firstATs,
                    _lastATs = _lastATs,
                    _preHeatms = _preHeatms,
                    _realHeatms = _realHeatms
                };
                if (resultType == 1)
                    window._contrast = true;
                else
                    window._sample = true;
                window.ShowInTaskbar = false; 
                window.Owner = this; 
                window.ShowDialog();
                //_showCurve.CloseWindow();

                CFGUtils.SaveConfig("FgIsStart", "1");
                System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        // 一直读取AD值
        class UpdateADThread : ChildThread
        {
            FgdMeasureWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public UpdateADThread(FgdMeasureWindow wnd)
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
                    FileUtils.OprLog(1, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_READ_AD_CYCLE:
                        if (msg.result)
                        {
                            byte[] data = msg.data;
                            wnd.Update(data);
                        }
                        else
                        {
                            wnd.ButtonSampleClear.IsEnabled = false;
                            wnd.ButtonSampleContrast.IsEnabled = false;
                            if (wnd._item.Method != 4)
                            {
                                MessageBox.Show("未连接设备！", "操作提示");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 屏蔽中文输入和非法字符粘贴输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxDishu_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (Global.IsAPlayer)
            {
                try
                {
                    if (Global.IsPlayer)
                    {
                        MainWindow._aPlayer._ItemName = _item.Name;
                        MainWindow._aPlayer.LoadPlayer();
                    }
                    else
                    {
                        MainWindow._aPlayer = new APlayerForm
                        {
                            _ItemName = _item.Name
                        };
                        MainWindow._aPlayer.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format("播放视频教程时出现异常!\r\n\r\n异常信息：{0}\r\n\r\n解决方法：{1}", ex.Message, "请安装[迅雷看看]播放器！"), "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    Global.IsPlayer = true;
                    VideoPlayback video = new VideoPlayback
                    {
                        _ItemName = _item.Name
                    };
                    video.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("播放视频时出现异常！\r\n\r\n异常信息：" + ex.Message, "操作提示");
                }
            }
        }

    }
}