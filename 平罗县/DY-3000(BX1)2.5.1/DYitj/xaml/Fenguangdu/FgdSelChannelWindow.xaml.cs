using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.src;
using AIO.xaml.Dialog;
using AIO.xaml.KjService;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// SelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdSelChannelWindow : Window
    {
        public DYFGDItemPara _item = null;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Tc = new DataTable();
        private string logType = "FgdSelChannelWindow-error";

        public FgdSelChannelWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
            {
                MessageBox.Show("项目异常");
                this.Close();
            }
            try
            {
                //进入检测界面前停止电池状态获取
                CFGUtils.SaveConfig("FgIsStart", "1");
                System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));

                labelTitle.Content = _item.Name + "  检测通道选择";
                UpdateHoleWaveIdx();
                ShowAllChannel();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            try
            {
                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use) break;
                }
                if (Global.deviceHole.HoleCount == i)
                {
                    MessageBox.Show("请至少选择一个检测孔", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                //进入检测界面前停止电池状态获取
                CFGUtils.SaveConfig("FgIsStart", "1");
                //System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));

                List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> listDiShuOrBeiShu = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                List<TextBox> listTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> listCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> listProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> listSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                bool IsContrast = false;

                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        //如果检测项目需要对照且还没有对照时
                        if (!IsContrast)
                        {
                            if ((0 == _item.Method && Double.MinValue == _item.ir.RefDeltaA) || (1 == _item.Method && Double.MinValue == _item.sc.RefA))
                                listSampleName[i].Text = "对照样";
                            else
                                IsContrast = true;
                        }

                        if (IsContrast)
                        {
                            if (listSampleName[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "样品名称不能为空!\n\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listSampleName[i].Focus();
                                return;
                            }

                            if (Global.InterfaceType.Equals("ZH"))// && LoginWindow._userAccount.CheckSampleID)
                            {
                                if (listSampleid != null && listSampleid[i].Text.Trim().Length == 0)
                                {
                                    if (MessageBox.Show("快检单号为空将不能上传到平台！\r\n是否继续？\r\n可双击文本框选择快检单号", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                                    {
                                        listSampleid[i].Focus();
                                        return;
                                    }
                                }
                            }
                        }

                        //if (Global.InterfaceType.Equals("KJ") && listTask[i].Text.Trim().Length == 0)
                        //{
                        //    MessageBox.Show("检测任务不能为空!\r\n可手工输入或双击选择检测任务", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    listTask[i].Focus();
                        //    return;
                        //}

                        //甘肃 被检单位&生产单位不能为空
                        if (Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ") || Global.EachDistrict.Equals("GS"))
                        {
                            if (listCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listCompany[i].Focus();
                                return;
                            }
                            if (Global.EachDistrict.Equals("GS") && listProduceCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "生产单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listProduceCompany[i].Focus();
                                return;
                            }
                        }

                        IsContrast = true;

                        _item.Hole[i].SampleName = listSampleName[i].Text.Trim();
                        _item.Hole[i].SampleId = listSampleid != null && listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : string.Empty;
                        //反应液滴数OR稀释倍数
                        if ((_item.Method == 1) || (_item.Method == 4))
                            _item.Hole[i].DishuOrBeishu = Global.StringConvertDouble(listDiShuOrBeiShu[i].Text.Trim());
                        //任务主题
                        if (listTask != null && listTask.Count > 0)
                        {
                            _item.Hole[i].TaskCode = listTask[i].DataContext.ToString();
                            _item.Hole[i].TaskName = listTask[i].Text.Trim();
                        }
                        else
                        {
                            _item.Hole[i].TaskName = string.Empty;
                            _item.Hole[i].TaskCode = string.Empty;
                        }
                        //被检单位&生产单位
                        _item.Hole[i].CompanyName = listCompany != null && listCompany.Count > 0 ? listCompany[i].Text.Trim() : "";
                        _item.Hole[i].ProduceCompany = listProduceCompany != null && listProduceCompany.Count > 0 ? listProduceCompany[i].Text.Trim() : string.Empty;

                        if (!listSampleName[i].Text.Trim().Equals("对照样") && Global.CheckItemAndFoodIsNull(_item.Name, listSampleName[i].Text.Trim()))
                        {
                            if (MessageBox.Show(string.Format("检测到样品[{0}]没有对应检测标准，是否立即添加检测标准？\r\n\r\n备注：没有对应检测标准时，可能无法得到准确的检测结果！", _item.Hole[i].SampleName), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                AddOrUpdateSample swindow = new AddOrUpdateSample();
                                try
                                {
                                    swindow.textBoxName.Text = swindow._projectName = _item.Name;
                                    swindow._sampleName = _item.Hole[i].SampleName;
                                    swindow._addOrUpdate = "ADD";
                                    swindow.ShowDialog();
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(string.Format("新增样品时出现异常：{0}", ex.Message));
                                    return;
                                }
                            }
                        }
                    }
                }

                //Global.stopwatch.Start();//启动代码运行计数器计算运行时间
                //Global.stopwatch.Stop();
                //string runTime = Global.stopwatch.ElapsedMilliseconds.ToString();
                //Console.Write(runTime);

                FgdMeasureWindow window = new FgdMeasureWindow
                {
                    _item = _item
                };
                if (0 == _item.Method)
                {
                    if (Global.EachDistrict.Equals("GS"))
                    {
                        if (Double.MinValue == _item.ir.RefDeltaA)
                        {
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if (_item.ir.RefDeltaA<0.15)
                        {
                            MessageBox.Show(this, "当前检测项目对照值过低，请重新做对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        if (Double.MinValue == _item.ir.RefDeltaA)
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                   
                }
                else if (1 == _item.Method)
                {
                    if (Double.MinValue == _item.sc.RefA)
                        MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                window.ShowInTaskbar = false;
                window.Owner = this;
                window.ShowDialog();

                //检测完后如果已经对照则清除第一个孔的
                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        listSampleName[i].Text = listSampleName[i].Text.Trim().Equals("对照样") ? listSampleName[i + 1].Text : listSampleName[i].Text;
                    }
                }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            //标记COM3未使用
            CFGUtils.SaveConfig("FgIsStart", "0");

            System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));
            this.Close();
        }

        private void UpdateHoleWaveIdx()
        {
            try
            {
                List<int> waveIdx = Global.deviceHole.GetHoleWaveIdx(_item.Wave);
                for (int i = 0; i < waveIdx.Count; ++i)
                {
                    _item.Hole[i].WaveIndex = waveIdx[i];
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowAllChannel()
        {
            try
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    UIElement element = GenerateChlBriefLayout(i, string.Empty);
                    WrapPanelChannel.Children.Add(element);
                    if (_item.Hole[i].WaveIndex >= 0)
                    {
                        //2015年9月10日 wenj 修改进入检查通道是全部不选
                        _item.Hole[i].Use = false;
                    }
                    else
                    {
                        _item.Hole[i].Use = false;
                        element.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }

                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                if (null == borderList)
                    return;

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (_item.Hole[i].Use)
                        borderList[i].BorderBrush = _borderBrushSelected;
                    else
                        borderList[i].BorderBrush = _borderBrushNormal;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                List<TextBox> tbProIndexList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProIndex");
                List<TextBox> tbDiShuOrBeiShuList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (type.Equals("border"))
                    {
                        if (borderList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                                _checkNum += 1;
                            }
                            else
                            {
                                borderList[i].BorderBrush = _borderBrushNormal;
                                _item.Hole[i].Use = false;
                                _checkNum -= 1;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("SampleName"))
                    {
                        if (tbSampleNameList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (!Global.sampleName.Equals(string.Empty))
                            {
                                tbSampleNameList[i].Text = Global.sampleName;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("TaskName"))
                    {
                        if (tbTaskList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (Global.InterfaceType.Equals("KJ"))
                            {
                                if (Global.KjServer._selectReceiveTasks != null)
                                {
                                    tbTaskList[i].Text = Global.KjServer._selectReceiveTasks.t_task_title;
                                    tbTaskList[i].DataContext = Global.KjServer._selectReceiveTasks.t_id;
                                    tbSampleNameList[i].Text = Global.KjServer._selectReceiveTasks.d_sample;
                                }
                            }
                            else if (!Global.TaskName.Equals(string.Empty))
                            {
                                tbTaskList[i].Text = Global.TaskName;
                                tbTaskList[i].DataContext = Global.TaskCode;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("Company"))
                    {
                        if (tbCompanyList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (Global.InterfaceType.Equals("KJ"))
                            {
                                if (Global.KjServer._selectBusiness != null)
                                {
                                    tbCompanyList[i].Text = Global.KjServer._selectCompany.reg_name + "-" + Global.KjServer._selectBusiness.ope_shop_code;
                                }
                                else if (Global.KjServer._selectCompany != null)
                                {
                                    tbCompanyList[i].Text = Global.KjServer._selectCompany.reg_name;
                                }
                            }
                            else if (!Global.CompanyName.Equals(string.Empty))
                                tbCompanyList[i].Text = Global.CompanyName;
                            break;
                        }
                    }
                    else if (type.Equals("ProduceCompany"))
                    {
                        if (sender == tbProduceCompanyList[i])
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("DiShuOrBeiShu"))
                    {
                        if (sender == tbDiShuOrBeiShuList[i])
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("Sampleid"))
                    {
                        if (tbSampleidList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (Wisdom.GETSAMPLECODE.Length > 0)
                            {
                                tbSampleidList[i].Text = Wisdom.GETSAMPLECODE;
                                tbCompanyList[i].Text = Global.CompanyName;
                                if (Global.IsSelectSampleName)
                                {
                                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, _item.Name, true, true, 1);
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                    else
                                    {
                                        if (MessageBox.Show(this, "当前样品名称在本地数据库中未匹配，是否新建该样品？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            AddOrUpdateSample addWindow = new AddOrUpdateSample();
                                            try
                                            {
                                                addWindow.textBoxName.Text = addWindow._projectName = _item.Name;
                                                addWindow._sampleName = Global.sampleName;
                                                addWindow.ShowDialog();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                }
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE = string.Empty;
                            }
                            break;
                        }
                    }
                }
                if (_checkNum == Global.deviceHole.HoleCount)
                {
                    this.CheckBoxSelAll.IsChecked = true;
                    this.cb_SelAll.IsChecked = true;
                }
                else if (_checkNum < Global.deviceHole.HoleCount)
                {
                    this.CheckBoxSelAll.IsChecked = false;
                    this.cb_SelAll.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "border");
        }

        private UIElement GenerateChlBriefLayout(int channel, string name)
        {
            Border border = new Border()
            {
                Width = 205,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = _borderBrushNormal,
                Background = Brushes.AliceBlue,
                Name = "border"
            };
            border.MouseDown += Border_MouseDown;

            StackPanel stackPanel = new StackPanel()
            {
                Width = 205,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 205,
                Height = 40
            };
            Label labelChannel = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = " 检测通道 " + (channel + 1)
            };

            //检测人员
            WrapPanel wrapPannelDetectPeople = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelDetectPeople = new Label()
            {
                Width = 93,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测人员",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Label textBoxDetectPeople = new Label()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = LoginWindow._userAccount != null ? LoginWindow._userAccount.UserName : string.Empty,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_SampleName = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelSampleName.Children.Add(labelX_SampleName);

            Label labelSampleName = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = name.Equals(string.Empty) ? _item.Hole[channel].SampleName : name,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "SampleName",
                Tag = channel,
                ToolTip = "双击可查询样品名称"
            };
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;

            //稀释倍数|反应液滴数
            WrapPanel wrapPanelCount = null;
            Label labelCount = null;
            TextBox textBoxDiShuOrBeiShu = null;
            if ((_item.Method == 1) || (_item.Method == 4))
            {
                wrapPanelCount = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };

                labelCount = new Label()
                {
                    Width = 93,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = (_item.Method == 1) ? "稀释倍数" : "反应液滴数",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxDiShuOrBeiShu = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = (_item.Method == 1) ? "1" : string.Empty,//_item.sc.CCA.ToString()
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "DiShuOrBeiShu"
                };
                textBoxDiShuOrBeiShu.TextChanged += textBoxDiShuOrBeiShu_TextChanged;
                textBoxDiShuOrBeiShu.PreviewMouseDown += textBoxDiShuOrBeiShu_PreviewMouseDown;
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };

                Label labelX_TaskName = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 18,
                    FontSize = 15,
                    Content = "",//Global.InterfaceType.Equals("KJ") ? "*" : "",
                    Foreground = new SolidColorBrush(Colors.Red),
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapPannelTask.Children.Add(labelX_TaskName);

                labelTaskName = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "任务主题",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxTaskName = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[channel].TaskName,
                    DataContext = _item.Hole[channel].TaskCode,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "TaskName",
                    ToolTip = "双击可查询检测任务"
                };
                textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
                textBoxTaskName.PreviewMouseDown += textBoxTaskName_PreviewMouseDown;
            }

            //被检单位
            WrapPanel wrapPannelCompany = new WrapPanel()
            {
                Width = 205,
                Height = 30,
                Visibility = Global.InterfaceType.Equals("PL") ? Visibility.Collapsed : Visibility.Visible,
            };
            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content =Global.EachDistrict.Equals("GS")|| Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ") ? "*" : "",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelCompany.Children.Add(labelX_Company);

            Label labelCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "被检单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].CompanyName,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位",
                IsReadOnly = Global.EachDistrict.Equals("GS") || Global.InterfaceType.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;

            //生产单位
            WrapPanel wrapPannelProduceCompany = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };

            Label labelX_ProduceCompany = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelProduceCompany.Children.Add(labelX_ProduceCompany);

            Label labelProduceCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "生产单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxProduceCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].ProduceCompany,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "ProduceCompany"
            };
            textBoxProduceCompany.PreviewMouseDown += textBoxProduceCompany_PreviewMouseDown;

            //快检单号
            WrapPanel wrapSampleid = null;
            if (Global.InterfaceType.Equals("ZH"))
            {
                wrapSampleid = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };
                Label labelX_Sampleid = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 18,
                    FontSize = 15,
                    Content = " ",
                    Foreground = new SolidColorBrush(Colors.Red),
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelX_Sampleid);

                Label labelSampleid = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "快检单号",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelSampleid);

                TextBox tbSampleid = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[channel].SampleId,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                tbSampleid.PreviewMouseDown += tbSampleid_PreviewMouseDown;
                wrapSampleid.Children.Add(tbSampleid);
            }

            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);

            if ((_item.Method == 1) || (_item.Method == 4))
            {
                wrapPanelCount.Children.Add(labelCount);
                wrapPanelCount.Children.Add(textBoxDiShuOrBeiShu);
                stackPanel.Children.Add(wrapPanelCount);
            }

            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
                stackPanel.Children.Add(wrapPannelTask);
            }
            
            stackPanel.Children.Add(wrapPannelCompany);
            
            

            if (Global.InterfaceType.Equals("GS") || Global.EachDistrict.Equals("GS"))
            {
                wrapPannelProduceCompany.Children.Add(labelProduceCompany);
                wrapPannelProduceCompany.Children.Add(textBoxProduceCompany);
                stackPanel.Children.Add(wrapPannelProduceCompany);
            }
            if (Global.InterfaceType.Equals("ZH")) stackPanel.Children.Add(wrapSampleid);

            border.Child = stackPanel;
            return border;
        }

        private void textBoxProduceCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "ProduceCompany");
        }

        private void textBoxDiShuOrBeiShu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "DiShuOrBeiShu");
        }

        private void tbSampleid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Sampleid");
        }

        private void tbSampleid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Wisdom.GETSAMPLECODE = String.Empty;
            Global.sampleName = String.Empty;
            Global.CompanyName = String.Empty;
            SearchSampleidWindow windowCompany = new SearchSampleidWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Sampleid");
        }

        private void textBoxCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Company");
        }

        private void textBoxTaskName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "TaskName");
        }

        private void textBoxSampleName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 样子当前样品和检测项目是否在本地数据库中有对应标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                string val = textBox.Text.Trim();
                if (val.Length == 0) return;

                if (Global.CheckItemAndFoodIsNull(_item.Name, val))
                {
                    if (MessageBox.Show(string.Format("检测到样品[{0}]没有对应检测标准，是否立即添加检测标准？\r\n\r\n备注：没有对应检测标准时，可能无法得到准确的检测结果！",
                        val), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AddOrUpdateSample swindow = new AddOrUpdateSample();
                        try
                        {
                            swindow.textBoxName.Text = swindow._projectName = _item.Name;
                            swindow._sampleName = val;
                            swindow._addOrUpdate = "ADD";
                            swindow.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
        }

        private void CheckBoxSelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked = (bool)CheckBoxSelAll.IsChecked;
                List<Border> listBorder = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                _checkNum = isChecked ? listBorder.Count : 0;
                for (int i = 0; i < listBorder.Count; ++i)
                {
                    if (isChecked)
                    {
                        listBorder[i].BorderBrush = _borderBrushSelected;
                        _item.Hole[i].Use = true;
                    }
                    else
                    {
                        listBorder[i].BorderBrush = _borderBrushNormal;
                        _item.Hole[i].Use = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void textBoxDiShuOrBeiShu_TextChanged(object sender, TextChangedEventArgs e)
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
            }
        }

        /// <summary>
        /// 双击样品名称文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //弹出样品小精灵
            ShowSample();
            //先选中当前border
            ShowBorder(sender, e, "SampleName");
            Global.sampleName = string.Empty;
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            PLSampleWindow SampleWindow = new PLSampleWindow();
            SampleWindow.ShowDialog();
            //SearchSample searchSample = new SearchSample
            //{
            //    _projectName = this._item.Name
            //};
            //searchSample.ShowDialog();
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出检测任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                SearchReceiveTasks window = new SearchReceiveTasks
                {
                    itemName = _item.Name
                };
                window.ShowDialog();
            }
            else
            {
                SearchTaskWindow searchTaskWindow = new SearchTaskWindow();
                searchTaskWindow.ShowDialog();
            }
            ShowBorder(sender, e, "TaskName");
            Global.TaskName = string.Empty;
            Global.TaskCode = string.Empty;
            Global.KjServer._selectReceiveTasks = null;
        }

        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                SearchCompanys window = new SearchCompanys();
                window.ShowDialog();
            }
            else
            {
                SearchCompanyWindow windowCompany = new SearchCompanyWindow();
                windowCompany.ShowDialog();
            }
            ShowBorder(sender, e, "Company");
            Global.CompanyName = string.Empty;
            Global.KjServer._selectCompany = null;
            Global.KjServer._selectBusiness = null;
        }

        private void cb_SelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked = (bool)cb_SelAll.IsChecked;
                if (lb_selAll.Content.Equals("全选"))
                {
                    lb_selAll.Content = "全不选";
                }
                else
                {
                    lb_selAll.Content = "全选";
                }
                List<Border> listBorder = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                _checkNum = isChecked ? listBorder.Count : 0;
                for (int i = 0; i < listBorder.Count; ++i)
                {
                    if (isChecked)
                    {
                        listBorder[i].BorderBrush = _borderBrushSelected;
                        _item.Hole[i].Use = true;
                    }
                    else
                    {
                        listBorder[i].BorderBrush = _borderBrushNormal;
                        _item.Hole[i].Use = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

    }
}