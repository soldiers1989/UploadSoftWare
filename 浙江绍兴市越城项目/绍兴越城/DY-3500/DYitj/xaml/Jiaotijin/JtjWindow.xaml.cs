using AIO.xaml.Dialog;
using AIO.xaml.Jiaotijin;
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
    /// JtjWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjWindow : Window
    {
        #region 全局变量
        private clsCompanyOpr _company = new clsCompanyOpr();
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Cp = new DataTable();
        private DataTable _Tc = new DataTable();
        private APlayerForm _aPlayer;
        private int _SelIndex = -1;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private clsCompanyOpr ttResult = new clsCompanyOpr();
        /// <summary>
        /// 接口若为省智慧平台则显示快检单号
        /// </summary>
        private Boolean InterfaceType = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        /// <summary>
        /// 是否是甘肃地区
        /// </summary>
        private Boolean EachDistrict = Global.EachDistrict.Equals("GS") ? true : false;
        /// <summary>
        /// 胶体金0、薄层色谱1、荧光免疫层析检测模块2，食源性微生物检测模块3
        /// </summary>
        public int ModelType = 0;
        public static string[] GetTypeNames = { "胶体金", "薄层色谱", "荧光免疫", "食源性微生物" };
        #endregion

        public JtjWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lb_title.Content = GetTypeNames[ModelType] + "项目选择界面";
            ShowAllItems(Global.jtjItems);
            _SelIndex = -1;
            this.btnSaveProjects.Visibility = Global.IsSetIndex ? Visibility.Visible : Visibility.Collapsed;
            this.btnPlayer.Visibility = Global.IsEnableVideo ? Visibility.Visible : Visibility.Collapsed;
        }

        #region 检测项目操作
        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            JtjEditItemWindow window = new JtjEditItemWindow()
            {
                ShowInTaskbar = false,
                Owner = this,
                ModelType = ModelType
            };
            window.ShowDialog();
            ShowAllItems(Global.jtjItems);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目!", "操作提示");
                return;
            }

            Global.WaitingWindowIsClose = true;
            DYJTJItemPara item = Global.jtjItems[_SelIndex];
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
                JtjEditItemWindow window = new JtjEditItemWindow()
                {
                    _item = Global.jtjItems[_SelIndex],
                    ShowInTaskbar = false,
                    Owner = this,
                    ModelType = ModelType
                };
                window.ShowDialog();
                ShowAllItems(Global.jtjItems, _SelIndex);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目!", "操作提示");
                return;
            }
            DYJTJItemPara item = Global.jtjItems[_SelIndex];
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
                if (MessageBox.Show("确定要删除该项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Global.jtjItems.RemoveAt(_SelIndex);
                    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                    ShowAllItems(Global.jtjItems);
                    _SelIndex = -1;
                }
            }
        }
        #endregion

        private void ButtonStartWork_Click(object sender, RoutedEventArgs e)
        {
            ShowJtjSelChannelWindow();
        }

        private void ShowJtjSelChannelWindow()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请选择检测项目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            TextBox tbSampleName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "SampleName");
            TextBox tbTaskName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "TaskName");
            TextBox tbCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Company");
            TextBox tbProduceCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "ProduceCompany");
            TextBox tbSampleid = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Sampleid");
            TextBox tbOpteror = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Operator");
            TextBox tbItem = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "itemName");

            DYJTJItemPara item = Global.jtjItems[_SelIndex];

            if (!InterfaceType)
            {
                if (tbSampleName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("样品名称不能为空!\r\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    tbSampleName.Focus();
                    return;
                }
            }
          
            if (tbCompany.Text.Trim().Length == 0)
            {
                MessageBox.Show("被检单位不能为空!\r\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                tbCompany.Focus();
                return;
            }
           
            if (tbOpteror.Text.Trim().Length == 0)
            {
                MessageBox.Show("经营户名称不能为空!\r\n双击选择输入", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                tbOpteror.Focus();
                return;
            }
            if (tbItem.Text.Trim().Length ==0)
            {
                if (MessageBox.Show("匹配项目未匹配,请匹配！\r\n继续检测数据将无法上传！！！\r\n是否继续？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }
            }

            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
               
                item.Hole[i].OpertorNames = tbOpteror != null ? tbOpteror.Text.Trim() : "";
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
            JtjSelChannelWindow window = new JtjSelChannelWindow()
            {
                _item = Global.jtjItems[_SelIndex],
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            if (_aPlayer != null) _aPlayer.Close();
            this.Close();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxCategory.Text = "胶体金";
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            if (_SelIndex >= 0)
            {
                window.ComboBoxItem.Text = Global.jtjItems[_SelIndex].Name;
            }
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
        }

        private void ShowAllItems(List<DYJTJItemPara> items)
        {
            // 将jtjItems的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            foreach (DYJTJItemPara item in items)
            {
                _SelIndex += 1;
                UIElement element = GenerateItemBriefLayout(item, string.Empty, false);
                WrapPanelItem.Children.Add(element);
            }
            _SelIndex = -1;
        }

        private void ShowAllItems(List<DYJTJItemPara> items, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            int num = _SelIndex;
            bool FalseOrTrue = false;
            foreach (DYJTJItemPara item in items)
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
                List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "SampleName");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Sampleid");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "ProduceCompany");
                List<TextBox> tbProIndexList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "ProIndex");
                List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Operator");
                List<TextBox> ItemList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemName");

                if (null == borderList) return;

                //先将所有的项目不选中
                for (int i = 0; i < borderList.Count; i++)
                {
                    borderList[i].BorderBrush = _borderBrushNormal;
                }

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (type.Equals("border"))
                    {
                        if (sender == borderList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;

                           
                            ////浙江查询当前选中检测项目是否在平台上有
                            //string CurrentItem = Global.jtjItems [_SelIndex].Name;
                            //DataTable itemname = ttResult.GetYCItemTable(" itemName='" + CurrentItem + "'");

                            //if (itemname != null && itemname.Rows.Count > 0)
                            //{
                            //    ItemList[i].Text = Global.jtjItems[_SelIndex].Name;
                            //    Global.itemName = itemname.Rows[0]["itemName"].ToString();
                            //    Global.itemCode = itemname.Rows[0]["itemCode"].ToString();
                            //    Global.itemType = itemname.Rows[0]["itemCode"].ToString();
                            //}
                            //else
                            //{
                            //    Global.projectName = "";
                            //    ItemWindow window = new ItemWindow();
                            //    window.ShowDialog();
                            //    if (Global.itemName != "")
                            //    {
                            //        ItemList[i].Text = Global.itemName;//浙江记录项目名称
                            //    }
                            //    else
                            //    {
                            //        ItemList[i].Text = "";
                            //    }
                            //}
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("SampleName"))
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
                            if (e == null)
                            {
                                if (!Global.CompanyName.Equals(string.Empty))
                                {
                                    tbCompanyList[i].Text = Global.CompanyName;
                                    Global.CompanyName = string.Empty;
                                }
                            }
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("Operator"))//经营户
                    {
                        if (sender == OpertorList[i])
                        {
                            if (tbCompanyList[i].Text.Trim() == "")
                            {
                                MessageBox.Show("请选择被检单位再选经营户", "系统提示", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                return;
                            }
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.Opertorname.Equals(string.Empty))
                            {
                                OpertorList[i].Text = Global.Opertorname;
                                Global.Opertorname = string.Empty;
                            }
                            if (e == null)
                            {
                                if (!Global.Opertorname.Equals(string.Empty))
                                {
                                    OpertorList[i].Text = Global.Opertorname;
                                    Global.Opertorname = string.Empty;
                                }
                            }
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("itemName"))//检测项目
                    {
                        if (sender == ItemList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            //if (!Global.itemName.Equals(string.Empty))
                            //{
                            //    ItemList[i].Text = Global.itemName;
                            //}
                            //else
                            //    ItemList[i].Text = "";
                          
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("TaskName"))
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
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("Company"))
                    {
                        if (sender == tbCompanyList[i])
                        {
                            OpertorList[i].Text = "";
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.CompanyName.Equals(string.Empty))
                            {
                                tbCompanyList[i].Text = Global.CompanyName;
                                Global.CompanyName = string.Empty;
                            }
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("ProduceCompany"))
                    {
                        if (sender == tbProduceCompanyList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("ProIndex"))
                    {
                        if (sender == tbProIndexList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("Sampleid"))
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
                                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, Global.jtjItems[_SelIndex].Name, true, true, 1);
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
                                                addWindow.textBoxName.Text = addWindow._projectName = Global.jtjItems[_SelIndex].Name;
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
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE = string.Empty;
                            }
                            return;
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
                        ShowJtjSelChannelWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ShowBorder):\n" + ex.Message);
            }
        }

        private void textBoxTaskName_DropDownOpened(object sender, SelectionChangedEventArgs e)
        {
            _Tc = _clsTaskOpr.GetAsDataTable(string.Empty, string.Empty, 2);
        }

        private UIElement GenerateItemBriefLayout(DYJTJItemPara item, string name, bool IsSelected)
        {
            //是否显示调整项目顺序
            Boolean IsSetIndex = Global.IsSetIndex ? true : false;

            Border border = new Border()
            {
                Width = 205,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = name.Equals(string.Empty) ? _borderBrushNormal : _borderBrushSelected,
                Name = "border",
                Background = Brushes.AliceBlue,
            };
            if (IsSelected) border.BorderBrush = _borderBrushSelected;
            border.MouseDown += Border_MouseDown;

            StackPanel stackPanel = new StackPanel()
            {
                Width = 205,
                Name = "stackPanel"
            };

            //检测项目显示
            Grid gridLabelName = new Grid()
            {
                Width = 205,
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
            //wrapPannelHole.Width = 205;
            //wrapPannelHole.Height = 30;
            //wrapPannelHole.Visibility = System.Windows.Visibility.Collapsed;

            //Label labelSampleHole = new Label();
            //labelSampleHole.Width = 75;
            //labelSampleHole.Height = 26;
            //labelSampleHole.Margin = new Thickness(0, 2, 0, 0);
            //labelSampleHole.FontSize = 15;
            //labelSampleHole.Content = "检测孔:";
            //labelSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //TextBox textBoxSampleHole = new TextBox();
            //textBoxSampleHole.Width = 95;
            //textBoxSampleHole.Height = 26;
            //textBoxSampleHole.Margin = new Thickness(0, 2, 0, 0);
            //textBoxSampleHole.FontSize = 15;
            //string strhole = string.Empty;
            //for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
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

            //检测方法
            WrapPanel wrapPannelMethod = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelMethod = new Label()
            {
                Width = 93,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测方法",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxMethod = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = DYJTJItemPara.MethodToString[item.Method],
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            //匹配项目
            WrapPanel wrapPannelItem = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Item = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItem.Children.Add(labelX_Item);
            Label labelItem = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "匹配项目",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxItem = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = item.Mitem == null ? string.Empty : item.Mitem,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "itemName",
                ToolTip = "双击选择项目",
                IsReadOnly = true,
            };
            textBoxItem.MouseDoubleClick += textBoxItem_MouseDoubleClick;
            wrapPannelItem.Children.Add(labelItem);
            wrapPannelItem.Children.Add(textBoxItem);
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
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = name.Trim().Equals(string.Empty) ? string.Empty : name.Trim().ToString(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Tag = item.Name,
                Name = "SampleName",
                ToolTip = "双击可查询样品名称",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("KJC"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };
                labelTaskName = new Label()
                {
                    Width = 93,
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
                textBoxTaskName.PreviewMouseDown += textBoxTaskName_PreviewMouseDown;
            }

            //被检单位
            WrapPanel wrapCompany = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };

            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*" ,
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapCompany.Children.Add(labelX_Company);

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
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            //经营户
            WrapPanel wrapOperator = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Operator = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            Label labelOperator = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "经营户",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxOperator = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = string.Empty,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "Operator",
                ToolTip = "双击可选择经营户",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxOperator.MouseDoubleClick += textBoxOperator_MouseDoubleClick;
            textBoxOperator.PreviewMouseDown += textBoxOperator_PreviewMouseDown;
            wrapOperator.Children.Add(labelX_Operator);
            wrapOperator.Children.Add(labelOperator);
            wrapOperator.Children.Add(textBoxOperator);
            //生产单位
            WrapPanel wrapProduceCompany = new WrapPanel()
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
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapProduceCompany.Children.Add(labelX_ProduceCompany);

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
            textBoxProduceCompany.PreviewMouseDown += textBoxProduceCompany_PreviewMouseDown;

            //快检单号
            WrapPanel wrapSampleid = null;
            if (InterfaceType)
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
                tbSampleid.PreviewMouseDown += tbSampleid_PreviewMouseDown;
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
                    Width = 205,
                    Height = 30
                };
                labelIndex = new Label()
                {
                    Width = 93,
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
                textIndex.PreviewMouseDown += textIndex_PreviewMouseDown;
                wrapIndex.Children.Add(labelIndex);
                wrapIndex.Children.Add(textIndex);
            }

            gridLabelName.Children.Add(labelName);
            wrapPannelMethod.Children.Add(labelMethod);
            wrapPannelMethod.Children.Add(textBoxMethod);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("KJC"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
            }
            wrapCompany.Children.Add(labelCompany);
            wrapCompany.Children.Add(textBoxCompany);
            wrapProduceCompany.Children.Add(labelProduceCompany);
            wrapProduceCompany.Children.Add(textBoxProduceCompany);

            stackPanel.Children.Add(gridLabelName);
            stackPanel.Children.Add(wrapPannelMethod);
            stackPanel.Children.Add(wrapPannelItem);
            stackPanel.Children.Add(wrapPannelSampleName);
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("KJC")) stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapCompany);
            if (EachDistrict) stackPanel.Children.Add(wrapProduceCompany);
            if (InterfaceType) stackPanel.Children.Add(wrapSampleid);
            if (IsSetIndex) stackPanel.Children.Add(wrapIndex);
            stackPanel.Children.Add(wrapOperator);
            //根据类型不同隐藏不相关的项目
            if (item.Type != ModelType)
            {
                border.Visibility = Visibility.Collapsed;
            }

            border.Child = stackPanel;
            return border;
        }

        private  void textBoxOperator_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Operator");
        }

        private void textIndex_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "ProIndex");
        }

        private void tbSampleid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Sampleid");
        }

        private void textBoxProduceCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "ProduceCompany");
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
        private void textBoxOperator_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string marketname = "";
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
            List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Operator");
            if (borderList == null)
                return;
            for (int i = 0; i < borderList.Count; ++i)
            {
                if (sender == OpertorList[i])
                {
                    marketname = tbCompanyList[i].Text.Trim();
                }
            }
            OperatorWindow window = new OperatorWindow();
            window.marketname = marketname;
            window.ShowDialog();
            ShowBorder(sender, e, "Operator");
        }

        private void textBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ItemWindow window = new ItemWindow();
            //window.ShowDialog();
            //ShowBorder(sender, e, "itemName");
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
            SearchSampleidWindow windowCompany = new SearchSampleidWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Sampleid");
        }

        /// <summary>
        /// 双击被检单位文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchCompanyWindow windowCompany = new SearchCompanyWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Company");
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出任务小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchTaskWindow searchTaskWindow = new SearchTaskWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            searchTaskWindow.ShowDialog();
            //双击任务主题文本框先选中当前border
            ShowBorder(sender, e, "TaskName");
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
                //SearchSample searchSample = new SearchSample()
                //{
                //    ShowInTaskbar = false,
                //    Owner = this,
                //    _projectName = Global.jtjItems[_SelIndex].Name
                //};
                //searchSample.ShowDialog();
                SampleWindow window = new SampleWindow();
                window.ShowDialog();
            }
        }

        /// <summary>
        /// 样品小精灵专用
        /// </summary>
        /// <param name="items">检查项</param>
        /// <param name="name">样品名称</param>
        /// <param name="index">当前选中的下标</param>
        private void ShowItems(List<DYJTJItemPara> items, string name, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            //DYJTJItemPara itemss = null;
            foreach (DYJTJItemPara item in items)
            {
                _SelIndex += 1;
                if (index == _SelIndex)
                {
                    List<string> sNameList = new List<string>
                    {
                        name
                    };
                    item.SampleName = sNameList;
                    for (int i = 0; i < item.Hole.Length; i++)
                    {
                        item.Hole[i].SampleName = name;
                    }
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
                                        ShowBorder(sender, null, "SampleName");
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
                                        ShowBorder(sender, null, "SampleName");
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
                            int rtn = Global.Checkcar.Insert(dataTable);
                        }
                    }
                    ShowBorder(sender, null, "SampleName");
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
            //弹出样品小精灵
            ShowSample();
            ShowBorder(sender, e, "SampleName");
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
                List<DYJTJItemPara> itemList = new List<DYJTJItemPara>();
                itemList = Global.jtjItems;
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                for (int i = 0; i < borderList.Count; i++)
                {
                    TextBox textBox = UIUtils.GetChildObject<TextBox>(borderList[i], "ProIndex");
                    itemList[i].Index = int.Parse(textBox.Text.Trim());
                }
                itemList.Sort(delegate(DYJTJItemPara a, DYJTJItemPara b) { return a.Index.CompareTo(b.Index); });
                Global.SerializeToFile(itemList, Global.jtjItemsFile);
                ShowAllItems(Global.jtjItems);
            }
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex <= -1)
            {
                MessageBox.Show("未选择任何检测项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (Global.IsAPlayer)
            {
                try
                {
                    if (Global.IsPlayer)
                    {
                        MainWindow._aPlayer._ItemName = Global.jtjItems[_SelIndex].Name;
                        MainWindow._aPlayer.LoadPlayer();
                    }
                    else
                    {
                        MainWindow._aPlayer = new APlayerForm();
                        MainWindow._aPlayer._ItemName = Global.jtjItems[_SelIndex].Name;
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
                    VideoPlayback video = new VideoPlayback();
                    video._ItemName = Global.jtjItems[_SelIndex].Name;
                    video.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("播放视频时出现异常！\r\n\r\n异常信息：" + ex.Message, "操作提示");
                }
            }
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex == -1)
            {
                MessageBox.Show("未选择任何检测项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, Global.fgdItems[_SelIndex].Name);
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow();
                window._HintStr = Global.fgdItems[_SelIndex].HintStr;
                window.Show();
            }
            else
            {
                TechnologeDocument window = new TechnologeDocument();
                window.path = path;
                window.ShowInTaskbar = false;
                window.Topmost = true;
                window.Owner = this;
                window.Show();
            }
        }

        /// <summary>
        /// 检索项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtItems.Visibility != Visibility.Visible)
            {
                txtItems.Visibility = Visibility.Visible;
                return;
            }

            string val = txtItems.Text.Trim();
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            List<Label> lbItems = UIUtils.GetChildObjects<Label>(WrapPanelItem, "labelName");
            for (int i = 0; i < lbItems.Count; i++)
            {
                if (val.Length == 0)
                {
                    borderList[i].Visibility = Visibility.Visible;
                    continue;
                }

                if (lbItems[i].Content.ToString().IndexOf(val) >= 0)
                {
                    borderList[i].Visibility = Visibility.Visible;
                }
                else
                {
                    borderList[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        /// <summary>
        /// 匹配项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatchItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JtjMathItemWindow window = new JtjMathItemWindow();
                window.ShowDialog();
                ShowAllItems(Global.jtjItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
