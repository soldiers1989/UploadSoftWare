using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AIO
{
    /// <summary>
    /// hmWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmWindow : Window
    {

        #region 全局变量
        private clsCompanyOpr _company = new clsCompanyOpr();
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Cp = new DataTable();
        private DataTable _Tc = new DataTable();
        private int _SelIndex = -1;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private VideoPlayback _video = null;
        //接口若为省智慧平台则显示快检单号
        private Boolean InterfaceType = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        //是否是甘肃地区
        private Boolean EachDistrict = Global.EachDistrict.Equals("GS") ? true : false;
        #endregion

        public HmWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllItems(Global.hmItems);
            _SelIndex = -1;
            this.btnSaveProjects.Visibility = Global.IsSetIndex ? Visibility.Visible : Visibility.Collapsed;
            this.Image.Visibility = Global.IsEnableVideo ? (Global.IsPlayer ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            HmEditItemWindow window = new HmEditItemWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
            ShowAllItems(Global.hmItems);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目!", "操作提示");
                return;
            }

            Global.WaitingWindowIsClose = true;
            DYHMItemPara item = Global.hmItems[_SelIndex];
            bool canEdit = false;
            if (string.Empty.Equals(item.Password))
            {
                canEdit = true;
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.GetResult())
                    {
                        if (dialog.GetInput().Equals(item.Password))
                        {
                            canEdit = true;
                            break;
                        }
                        else
                        {
                            canEdit = false;
                            MessageBox.Show("密码错误");
                        }
                        i--;
                    }
                }
            }
            if (canEdit)
            {
                HmEditItemWindow window = new HmEditItemWindow()
                {
                    _item = Global.hmItems[_SelIndex],
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
                ShowAllItems(Global.hmItems, _SelIndex);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目!", "操作提示");
                return;
            }
            DYHMItemPara item = Global.hmItems[_SelIndex];
            bool canEdit = false;
            if (string.Empty.Equals(item.Password))
            {
                canEdit = true;
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.GetResult())
                    {
                        if (dialog.GetInput().Equals(item.Password))
                        {
                            canEdit = true;
                            break;
                        }
                        else
                        {
                            canEdit = false;
                            MessageBox.Show("密码错误");
                        }
                        i--;
                    }
                }
            }
            if (canEdit)
            {
                Global.hmItems.RemoveAt(_SelIndex);
                Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
                ShowAllItems(Global.hmItems);
                _SelIndex = -1;
            }
        }

        private void ButtonStartWork_Click(object sender, RoutedEventArgs e)
        {
            if (Global.IsServerTest && Global.InterfaceType.Equals("DY"))
            {
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
            ShowHmSelChannelWindow();
        }

        private void ShowHmSelChannelWindow()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请选择点选一个检测项目，再按检测按钮");
                return;
            }
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            TextBox tbSampleName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "sampleName");
            TextBox tbTaskName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "TaskName");
            TextBox tbCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Company");
            TextBox tbProduceCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "ProduceCompany");
            TextBox tbSampleid = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Sampleid");
            DYHMItemPara item = Global.hmItems[_SelIndex];
            #region 2016年12月30日 检测项目选择界面不验证必填项，通道选择界面再验证
            //if (!InterfaceType)
            //{
            //    if (tbSampleName.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("样品名称不能为空!\r\n可手工输入或双击选择样品", "操作提示");
            //        tbSampleName.Focus();
            //        return;
            //    }
            //}
            //if (EachDistrict)
            //{
            //    if (tbCompany.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("被检单位不能为空!\r\n可手工输入或双击选择被检单位", "操作提示");
            //        tbCompany.Focus();
            //        return;
            //    }
            //    if (tbProduceCompany.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("生产单位不能为空!", "操作提示");
            //        tbProduceCompany.Focus();
            //        return;
            //    }
            //}
            //if (InterfaceType)
            //{
            //    if (tbSampleid.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("快检单号不能为空!\r\n可双击文本框选择快检单号", "操作提示");
            //        tbSampleid.Focus();
            //        return;
            //    }
            //}
            #endregion
            for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                item.Hole[i].SampleName = tbSampleName.Text.Trim();
                if (tbTaskName != null)
                {
                    item.Hole[i].TaskName = tbTaskName.Text.Trim();
                    item.Hole[i].TaskCode = tbTaskName.DataContext.ToString();
                }
                else
                {
                    item.Hole[i].TaskName = string.Empty;
                    item.Hole[i].TaskCode = string.Empty;
                }
                item.Hole[i].CompanyName = tbCompany.Text.Trim();
                item.Hole[i].ProduceCompany = tbProduceCompany != null ? tbProduceCompany.Text.Trim() : string.Empty;
                item.Hole[i].SampleId = tbSampleid != null ? tbSampleid.Text.Trim() : string.Empty;
            }

            HmSelChannelWindow window = new HmSelChannelWindow()
            {
                _item = item,
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            if (_video != null)
            {
                _video.Close();
            }
            this.Close();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxCategory.Text = "重金属";
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            if (_SelIndex >= 0)
            {
                window.ComboBoxItem.Text = Global.hmItems[_SelIndex].Name;
            }
            List<string> sList = new List<string>
            {
                "全部"
            };
            window.ComboBoxMethod.ItemsSource = sList;
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
        }

        private void ShowAllItems(List<DYHMItemPara> items)
        {
            // 将jtjItems的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            foreach (DYHMItemPara item in items)
            {
                _SelIndex += 1;
                UIElement element = GenerateItemBriefLayout(item, string.Empty, false);
                WrapPanelItem.Children.Add(element);
            }
            _SelIndex = -1;
        }

        private void ShowAllItems(List<DYHMItemPara> items, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            int num = _SelIndex;
            bool FalseOrTrue = false;
            foreach (DYHMItemPara item in items)
            {
                _SelIndex += 1;
                if (index == _SelIndex)
                    FalseOrTrue = true;
                else
                    FalseOrTrue = false;
                UIElement element = GenerateItemBriefLayout(item, string.Empty, FalseOrTrue);
                WrapPanelItem.Children.Add(element);
            }
            _SelIndex = index;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "border");
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "sampleName");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Sampleid");
                if (null == borderList)
                    return;
                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (type.Equals("border"))
                    {
                        if (sender == borderList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("sample"))
                    {
                        if (sender == tbSampleNameList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.sampleName.Equals(string.Empty))
                            {
                                tbSampleNameList[i].Text = Global.sampleName;
                                Global.sampleName = string.Empty;
                            }
                            //e=null时为扫描枪设备或其他方式录入需要将被检单位自动关联
                            if (e == null)
                            {
                                if (!Global.CompanyName.Equals(string.Empty))
                                {
                                    tbCompanyList[i].Text = Global.CompanyName;
                                    Global.CompanyName = string.Empty;
                                }
                            }
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("task"))
                    {
                        if (sender == tbTaskList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.TaskName.Equals(string.Empty))
                            {
                                tbTaskList[i].Text = Global.TaskName;
                                tbTaskList[i].DataContext = Global.TaskCode;
                                Global.TaskName = Global.TaskCode = string.Empty;
                            }
                            if (Global.Other.Length > 0)
                            {
                                tbSampleNameList[i].Text = Global.GetTaskSampleName();
                                Global.Other = string.Empty;
                            }
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("company"))
                    {
                        if (sender == tbCompanyList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.CompanyName.Equals(string.Empty))
                            {
                                tbCompanyList[i].Text = Global.CompanyName;
                                Global.CompanyName = string.Empty;
                            }
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("getsampleCode"))
                    {
                        if (sender == tbSampleidList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (Wisdom.GETSAMPLECODE.Length > 0)
                            {
                                tbSampleidList[i].Text = Wisdom.GETSAMPLECODE;
                                tbCompanyList[i].Text = Global.CompanyName;
                                if (Global.IsSelectSampleName)
                                {
                                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, Global.hmItems[_SelIndex].Name, true, true, 1);
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("当前样品名称在本地数据库中未匹配，是否新建该样品？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            AddOrUpdateSample addWindow = new AddOrUpdateSample();
                                            try
                                            {
                                                addWindow.textBoxName.Text = addWindow._projectName = Global.hmItems[_SelIndex].Name;
                                                addWindow._sampleName = Global.sampleName;
                                                addWindow.ShowDialog();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                }
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE;
                            }
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                }

                if (e == null) return;
                //双击进行检测
                var element = (FrameworkElement)sender;
                if (e.ClickCount == 1)
                {
                    var timer = new System.Timers.Timer(500)
                    {
                        AutoReset = false
                    };
                    timer.Elapsed += new ElapsedEventHandler((o, ex) => element.Dispatcher.Invoke(new Action(() =>
                    {
                        var timer2 = (System.Timers.Timer)element.Tag;
                        timer2.Stop();
                        timer2.Dispose();
                        //UIElement_Click(element, e);
                    })));
                    timer.Start();
                    element.Tag = timer;
                }
                if (e.ClickCount == 2)
                {
                    var timer = element.Tag as System.Timers.Timer;
                    if (timer != null)
                    {
                        timer.Stop();
                        timer.Dispose();
                        ShowHmSelChannelWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ShowBorder):\n" + ex.Message);
            }
        }

        private UIElement GenerateItemBriefLayout(DYHMItemPara item, string name, bool FalseOrTrue)
        {
            //是否显示调整项目顺序
            Boolean IsSetIndex = Global.IsSetIndex ? true : false;

            Border border = new Border()
            {
                Width = 185,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = name.Equals(string.Empty) ? _borderBrushNormal : _borderBrushSelected,
                Background = Brushes.AliceBlue,
                Name = "border"
            };
            if (FalseOrTrue) border.BorderBrush = _borderBrushSelected;
            border.MouseDown += Border_MouseDown;

            StackPanel stackPanel = new StackPanel()
            {
                Width = 185,
                Name = "stackPanel"
            };
            Grid gridLabelName = new Grid()
            {
                Width = 185,
                Height = 40
            };
            Label labelName = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = item.Name,
                Name = "labelName"
            };

            #region 
            //检测孔 隐藏
            //WrapPanel wrapPannelHole = new WrapPanel();
            //wrapPannelHole.Width = 185;
            //wrapPannelHole.Height = 30;
            //wrapPannelHole.Visibility = System.Windows.Visibility.Collapsed;

            //Label labelSampleHole = new Label();
            //labelSampleHole.Width = 75;
            //labelSampleHole.Height = 26;
            //labelSampleHole.Margin = new Thickness(0, 2, 0, 0);
            //labelSampleHole.FontSize = 15;
            //labelSampleHole.Content = "检测孔：";
            //labelSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //TextBox textBoxSampleHole = new TextBox();
            //textBoxSampleHole.Width = 95;
            //textBoxSampleHole.Height = 26;
            //textBoxSampleHole.Margin = new Thickness(0, 2, 0, 2);
            //textBoxSampleHole.FontSize = 15;
            //string strhole = string.Empty;
            //for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            //{
            //    if (item.Hole[i].Use)
            //    {
            //        if (string.Empty.Equals(strhole))
            //            strhole += (i + 1);
            //        else
            //            strhole += "," + (i + 1);
            //    }
            //}
            //textBoxSampleHole.Text = strhole;
            //textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            //textBoxSampleHole.IsReadOnly = true;
            #endregion

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };

            //Label labelX = new Label();
            //labelX.Width = 16;
            //labelX.Height = 26;
            //labelX.Margin = new Thickness(0, 0, 0, 0);
            //labelX.FontSize = 15;
            //labelX.Content = "*";
            //labelX.Foreground = new SolidColorBrush(Colors.Red);
            //labelX.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            //wrapPannelSampleName.Children.Add(labelX);

            Label labelSampleName = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                Text = name.Trim().Equals(string.Empty) ? string.Empty : name.Trim().ToString(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "sampleName",
                ToolTip = "双击可查询样品名称",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelTaskName = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "任务主题",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textBoxTaskName = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = string.Empty,
                    DataContext = string.Empty,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Name = "TaskName",
                    ToolTip = "双击可查询所有任务"
                };
                textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
            }
            //被检单位
            WrapPanel wrapCompany = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };

            //Label labelY = new Label();
            //labelY.Width = 16;
            //labelY.Height = 26;
            //labelY.Margin = new Thickness(0, 0, 0, 0);
            //labelY.FontSize = 15;
            //labelY.Content = "*";
            //labelY.Foreground = new SolidColorBrush(Colors.Red);
            //labelY.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            Label labelCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "被检单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            //if (EachDistrict)
            //{
            //    wrapCompany.Children.Add(labelY);
            //    labelCompany.Width = 75;
            //}

            TextBox textBoxCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询所有被检单位",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;

            //生产单位
            WrapPanel wrapProduceCompany = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };

            //Label labelProduceCompanyX = new Label();
            //labelProduceCompanyX.Width = 16;
            //labelProduceCompanyX.Height = 26;
            //labelProduceCompanyX.Margin = new Thickness(0, 0, 0, 0);
            //labelProduceCompanyX.FontSize = 15;
            //labelProduceCompanyX.Content = "*";
            //labelProduceCompanyX.Foreground = new SolidColorBrush(Colors.Red);
            //labelProduceCompanyX.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            Label labelProduceCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "生产单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            //wrapProduceCompany.Children.Add(labelProduceCompanyX);

            TextBox textBoxProduceCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "ProduceCompany"
            };

            //快检单号
            WrapPanel wrapSampleid = null;
            if (InterfaceType)
            {
                wrapSampleid = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };

                //Label labelKJ = new Label();
                //labelKJ.Width = 16;
                //labelKJ.Height = 26;
                //labelKJ.Margin = new Thickness(0, 0, 0, 0);
                //labelKJ.FontSize = 15;
                //labelKJ.Content = "*";
                //labelKJ.Foreground = new SolidColorBrush(Colors.Red);
                //labelKJ.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                //wrapSampleid.Children.Add(labelKJ);

                Label labelSampleid = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "快检单号",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelSampleid);

                TextBox tbSampleid = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = string.Empty,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号"
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                wrapSampleid.Children.Add(tbSampleid);
            }

            //项目顺序
            WrapPanel wrapIndex = null;
            Label labelIndex = null;
            TextBox textIndex = null;
            if (IsSetIndex)
            {
                wrapIndex = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelIndex = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "项目顺序",
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textIndex = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = (_SelIndex + 1).ToString(),
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Name = "ProIndex"
                };
                textIndex.TextChanged += textIndex_TextChanged;
                wrapIndex.Children.Add(labelIndex);
                wrapIndex.Children.Add(textIndex);
            }

            gridLabelName.Children.Add(labelName);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
            }
            wrapCompany.Children.Add(labelCompany);
            wrapCompany.Children.Add(textBoxCompany);
            wrapProduceCompany.Children.Add(labelProduceCompany);
            wrapProduceCompany.Children.Add(textBoxProduceCompany);

            stackPanel.Children.Add(gridLabelName);
            stackPanel.Children.Add(wrapPannelSampleName);
            if (Global.InterfaceType.Equals("DY"))
            {
                stackPanel.Children.Add(wrapPannelTask);
            }
            stackPanel.Children.Add(wrapCompany);
            if (EachDistrict)
                stackPanel.Children.Add(wrapProduceCompany);
            if (InterfaceType)
                stackPanel.Children.Add(wrapSampleid);
            if (IsSetIndex)
                stackPanel.Children.Add(wrapIndex);

            border.Child = stackPanel;
            return border;
        }

        /// <summary>
        /// 双击快检单号文本框先选中当前border
        /// 然后弹出快检单号选择界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSampleid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Wisdom.GETSAMPLECODE = String.Empty;
            Global.sampleName = String.Empty;
            Global.CompanyName = String.Empty;
            ShowBorder(sender, e, "getsampleCode");
            SearchSampleidWindow windowCompany = new SearchSampleidWindow();
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "getsampleCode");
        }

        private void ShowWaitingWindow()
        {
            WaitingWindow window = new WaitingWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private string sampleCode = string.Empty;
        private void textBoxSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbs = sender as TextBox;
            try
            {
                string[] sampleCodes = tbs.Text.Trim().Split('：');
                if (sampleCodes != null && sampleCodes.Length == 1) sampleCodes = tbs.Text.Trim().Split(':');
                if (sampleCodes != null && sampleCodes.Length >= 3)
                {
                    int index = sampleCodes.Length;
                    if (sampleCodes[index - 2].Equals("ID"))
                    {
                        string[] strs = sampleCodes[index - 1].Split('.');
                        if (strs != null && strs.Length == 2)
                        {
                            try
                            {
                                ShowWaitingWindow();

                                tbs.Text = strs[0];
                                sampleCode = tbs.Text;

                                List<clsTB_SAMPLING> models = null;
                                if (sampleCode.Length > 0)
                                {
                                    models = Global.Checkcar.GetSampling(sampleCode);
                                    if (models != null && models.Count > 0)
                                    {
                                        Global.sampleName = tbs.Text = models[0].SAMPLENAME;
                                        Global.CompanyName = models[0].CKONAME;
                                        ShowBorder(sender, null, "sample");
                                        return;
                                    }
                                }
                                sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].user,
                                        Global.samplenameadapter[0].pwd, sampleCode, Global.samplenameadapter[0].url);
                                if (sampleCode.Length > 0)
                                {
                                    DataSet dataSet = new DataSet();
                                    DataTable dataTable = new DataTable();
                                    using (StringReader sr = new StringReader(sampleCode))
                                    {
                                        dataSet.ReadXml(sr);
                                    }
                                    dataTable = dataSet.Tables["SamplingClass"];
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        Global.sampleName = tbs.Text = dataTable.Rows[0]["SAMPLENAME"].ToString();
                                        Global.CompanyName = dataTable.Rows[0]["CKONAME"].ToString();
                                        ShowBorder(sender, null, "sample");
                                        int rtn = Global.Checkcar.Insert(dataTable);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                Global.WaitingWindowIsClose = true;
                            }
                            finally { Global.WaitingWindowIsClose = true; }
                        }
                    }
                }
            }
            catch (Exception)
            {
                tbs.Text = string.Empty;
            }
        }

        private void textBoxSampleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    ShowWaitingWindow();

                    TextBox tbs = sender as TextBox;
                    sampleCode = tbs.Text.Trim();

                    List<clsTB_SAMPLING> models = null;
                    if (sampleCode.Length > 0)
                    {
                        models = Global.Checkcar.GetSampling(sampleCode);
                        if (models != null && models.Count > 0)
                        {
                            Global.sampleName = tbs.Text = models[0].SAMPLENAME;
                            Global.CompanyName = models[0].CKONAME;
                            ShowBorder(sender, null, "sample");
                            return;
                        }
                    }
                    sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].user,
                            Global.samplenameadapter[0].pwd, sampleCode, Global.samplenameadapter[0].url);
                    if (sampleCode.Length > 0)
                    {
                        DataSet dataSet = new DataSet();
                        DataTable dataTable = new DataTable();
                        using (StringReader sr = new StringReader(sampleCode))
                        {
                            dataSet.ReadXml(sr);
                        }
                        dataTable = dataSet.Tables["SamplingClass"];
                        if (dataTable != null && dataTable.Rows.Count > 0)
                        {
                            Global.sampleName = tbs.Text = dataTable.Rows[0]["SAMPLENAME"].ToString();
                            Global.CompanyName = dataTable.Rows[0]["CKONAME"].ToString();
                            ShowBorder(sender, null, "sample");
                            int rtn = Global.Checkcar.Insert(dataTable);
                        }
                    }
                }
                catch (Exception)
                {
                    Global.WaitingWindowIsClose = true;
                }
                finally { Global.WaitingWindowIsClose = true; }
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
            //双击样品名称文本框先选中当前border
            ShowBorder(sender, e, "sample");
            //弹出样品小精灵
            ShowSample();
            ShowBorder(sender, e, "sample");
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请先选择一个项目!", "操作提示");
            }
            else
            {
                Global.IsProject = false;
                SearchSample searchSample = new SearchSample()
                {
                    _projectName = Global.hmItems[_SelIndex].Name
                };
                searchSample.ShowDialog();
            }
        }

        /// <summary>
        /// 样品小精灵专用
        /// </summary>
        /// <param name="items">检查项</param>
        /// <param name="name">样品名称</param>
        /// <param name="index">当前选中的下标</param>
        private void ShowItems(List<DYHMItemPara> items, string name, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            foreach (DYHMItemPara item in items)
            {
                _SelIndex += 1;
                if (index == _SelIndex)
                {
                    List<string> sNameList = new List<string>
                    {
                        name
                    };
                    item.SampleName = sNameList;
                    List<string> sampleList = new List<string>
                    {
                        string.Empty
                    };
                }
                UIElement element = GenerateItemBriefLayout(item, index == _SelIndex ? name : string.Empty, false);
                WrapPanelItem.Children.Add(element);
            }
            _SelIndex = index;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string error = string.Empty;
            try
            {
                string[] files = Directory.GetFiles(Global.VideoAddress);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                MessageBox.Show("视频存储路径不正确!", "操作提示");
            }
            finally
            {
                if (error.Equals(string.Empty))
                {
                    Global.IsPlayer = true;
                    _video = new VideoPlayback();
                    _video.Show();
                }
            }
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出任务小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "task");
            SearchTaskWindow searchTaskWindow = new SearchTaskWindow();
            searchTaskWindow.ShowDialog();
            //双击任务主题文本框先选中当前border
            ShowBorder(sender, e, "task");
        }

        /// <summary>
        /// 双击被检单位文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "company");
            SearchCompanyWindow windowCompany = new SearchCompanyWindow();
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "company");
        }

        private void textIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);
            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                int num = 0;
                if (!int.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        private void btnSaveProjects_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < 2; j++)
            {
                List<DYHMItemPara> itemList = new List<DYHMItemPara>();
                itemList = Global.hmItems;
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                for (int i = 0; i < borderList.Count; i++)
                {
                    TextBox textBox = UIUtils.GetChildObject<TextBox>(borderList[i], "ProIndex");
                    itemList[i].Index = int.Parse(textBox.Text.Trim());
                }
                itemList.Sort(delegate(DYHMItemPara a, DYHMItemPara b) { return a.Index.CompareTo(b.Index); });
                Global.SerializeToFile(itemList, Global.hmItemsFile);
                ShowAllItems(Global.hmItems);
            }
        }

    }
}
