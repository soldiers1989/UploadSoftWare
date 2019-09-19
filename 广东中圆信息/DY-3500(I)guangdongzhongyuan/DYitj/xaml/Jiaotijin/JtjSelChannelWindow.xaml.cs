using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
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
    /// JtjSelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjSelChannelWindow : Window
    {
        public DYJTJItemPara _item = null;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private string logType = "JtjSelChannelWindow-error";

        public JtjSelChannelWindow()
        {
            InitializeComponent();
            //Global.GetJtjVersion();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
            {
                MessageBox.Show("项目异常");
                this.Close();
            }
            labelTitle.Content = _item.Name + "  检测通道选择";
            ShowAllChannel();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            // 是否选择了检测孔
            int i = 0;
            try
            {
                for (i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                        break;
                }
                if (Global.deviceHole.SxtCount == i)
                {
                    MessageBox.Show("请至少选择一个检测孔", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

                List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> listTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> listCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> listProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> listSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");

                for (i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
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
                                MessageBox.Show(this, "快检单号不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listSampleid[i].Focus();
                                return;
                            }
                        }

                        //if (Global.InterfaceType.Equals("KJ") && listTask[i].Text.Trim().Length == 0)
                        //{
                        //    MessageBox.Show("检测任务不能为空!\r\n可手工输入或双击选择检测任务", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    listTask[i].Focus();
                        //    return;
                        //}

                        //甘肃 被检单位&生产单位不能为空
                        if (Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
                        {
                            if (listCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listCompany[i].Focus();
                                return;
                            }
                            if (Global.InterfaceType.Equals("GS") && listProduceCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "生产单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listProduceCompany[i].Focus();
                                return;
                            }
                        }

                        _item.Hole[i].SampleName = listSampleName[i].Text.Trim();
                        _item.Hole[i].SampleId = listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : string.Empty;
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
                        _item.Hole[i].CompanyName = listCompany[i].Text.Trim();
                        _item.Hole[i].ProduceCompany = listProduceCompany != null && listProduceCompany.Count > 0 ? listProduceCompany[i].Text.Trim() : string.Empty;

                        if (Global.CheckItemAndFoodIsNull(_item.Name, listSampleName[i].Text.Trim()))
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
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            JtjMeasureWindow window = new JtjMeasureWindow
            {
                _item = _item,
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
            if (Global.JtjVersion == 3)
            {
                //胶体金3.0模块进卡(偶有进卡失败的情况)
                string[] _strSXTPORT = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };
                for (int j = 0; j < Global.deviceHole.SxtCount; j++)
                {
                    Message msg = new Message
                    {
                        what = MsgCode.MSG_JBK_IN,
                        str1 = _strSXTPORT[j]
                    };
                    Global.workThread.SendMessage(msg, null);
                }
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAllChannel()
        {
            int sampleNum = _item.SampleNum;
            try
            {
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    UIElement element = GenerateChlBriefLayout(i, string.Empty);
                    WrapPanelChannel.Children.Add(element);
                }

                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");

                if (null == borderList)
                    return;
                for (int i = 0; i < borderList.Count; ++i)
                {
                    //2015年11月10日 wenj 修改进入检测通道选择后默认全部不选
                    //if (_item.Hole[i].Use)
                    //    borderList[i].BorderBrush = _borderBrushSelected;
                    //else
                    _item.Hole[i].Use = false;
                    borderList[i].BorderBrush = _borderBrushNormal;
                }

                //如果是启用了薄层色谱，胶体金使用1通道，薄层色谱使用2通道
                if (Global.IsEnableBcsp)
                {
                    //if (Global.typeName.Equals("bcsp"))
                    //{
                    //    borderList[0].Visibility = Visibility.Collapsed;
                    //}
                    //else
                    //{
                    //    borderList[1].Visibility = Visibility.Collapsed;
                    //}
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
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
                ToolTip = "双击可查询样品名称"//,
                //IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            //if (Global.IsSelectSampleName)
            //{
            //    textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
            //    textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            //}
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;

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
                    Text = _item.Hole[0].TaskName,
                    DataContext = _item.Hole[0].TaskCode,
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
                
            };
            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*" ,
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
                Text = _item.Hole[0].CompanyName,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位",
                IsReadOnly = Global.InterfaceType.Equals("CC") ? true : false
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
                Text = _item.Hole[0].ProduceCompany,
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
                    Content = "*",
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
                    Text = _item.Hole[0].SampleId,
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

            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
                stackPanel.Children.Add(wrapPannelTask);
            }

            stackPanel.Children.Add(wrapPannelCompany);
            if (Global.InterfaceType.Equals("GS"))
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
                        swindow.textBoxName.Text = swindow._projectName = _item.Name;
                        swindow._sampleName = val;
                        swindow._addOrUpdate = "ADD";
                        swindow.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
            }
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
            SearchSample searchSample = new SearchSample
            {
                _projectName = this._item.Name
            };
            searchSample.ShowDialog();
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
            List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
            List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");

            try
            {
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
                                tbSampleNameList[i].Text = Global.sampleName;
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
                if (Global.deviceHole.SxtCount == _checkNum)
                    this.CheckBoxSelAll.IsChecked = true;
                else if (Global.deviceHole.SxtCount > _checkNum)
                    this.CheckBoxSelAll.IsChecked = false;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private List<string> AddStringToList(List<string> list, string content)
        {
            if (null == list)
            {
                list = new List<string>();
            }

            if (!string.Empty.Equals(content.Trim()))
            {
                if (!list.Contains(content))
                {
                    list.Add(content);
                }
            }
            return list;
        }
    }
}