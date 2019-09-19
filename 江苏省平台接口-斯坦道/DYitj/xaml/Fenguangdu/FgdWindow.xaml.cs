﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AIO.src;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    /// <summary>
    /// FenguangduWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdWindow : Window
    {

        #region 全局变量
        private int _SelIndex = 0;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        #endregion

        public FgdWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllItems(Global.fgdItems);
            _SelIndex = -1;
            this.btnSaveProjects.Visibility = Global.IsSetIndex ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            FgdEditItemWindow window = new FgdEditItemWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
            ShowAllItems(Global.fgdItems);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show(this, "未选择任何项目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Global.WaitingWindowIsClose = true;
            try
            {
                bool canEdit = false;
                DYFGDItemPara item = Global.fgdItems[_SelIndex];
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
                                MessageBox.Show(this, "密码错误", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            i--;
                        }
                    }
                }
                if (canEdit)
                {
                    FgdEditItemWindow window = new FgdEditItemWindow()
                    {
                        _item = Global.fgdItems[_SelIndex],
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
                    ShowAllItems(Global.fgdItems, _SelIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ButtonEditItem_Click):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show(this, "未选择任何项目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool canEdit = false;
            try
            {
                DYFGDItemPara item = Global.fgdItems[_SelIndex];
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
                                MessageBox.Show(this, "密码错误", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            i--;
                        }
                    }
                }
                if (canEdit)
                {
                    if (MessageBox.Show("确定要删除该项目吗!？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Global.fgdItems.RemoveAt(_SelIndex);
                        Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                        ShowAllItems(Global.fgdItems);
                        _SelIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ButtonDelItem_Click):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonStartWork_Click(object sender, RoutedEventArgs e)
        {
            ShowFgdSelChannelWindow();
        }

        private void ShowFgdSelChannelWindow()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show(this, "请选择检测项目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                TextBox tbSampleName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "SampleName");
                TextBox tbTaskName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "TaskName");
                TextBox tbCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Company");
                TextBox tbProduceCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "ProduceCompany");
                TextBox tbSampleid = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Sampleid");

                DYFGDItemPara item = Global.fgdItems[_SelIndex];
                //if (!Global.InterfaceType.Equals("ZH"))
                //{
                if (tbSampleName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("样品名称不能为空!\r\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    tbSampleName.Focus();
                    return;
                }
                //}
                //甘肃和禅城区 被检单位为必填项
                if (Global.EachDistrict.Equals("GS") || Global.EachDistrict.Equals("CC"))
                {
                    if (tbCompany.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("被检单位不能为空!\r\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tbCompany.Focus();
                        return;
                    }
                }

                //甘肃 生产单位必填
                if (Global.EachDistrict.Equals("GS"))
                {
                    //if (tbProduceCompany.Text.Trim().Length == 0)
                    //{
                    //    MessageBox.Show("摊位号不能为空!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    //    tbProduceCompany.Focus();
                    //    return;
                    //}
                }

                if (Global.InterfaceType.Equals("ZH") && LoginWindow._userAccount.CheckSampleID)
                {
                    if (tbSampleid.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("快检单号不能为空!\r\n可双击文本框选择快检单号", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tbSampleid.Focus();
                        return;
                    }
                }

                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    item.Hole[i].SampleName = tbSampleName != null ? tbSampleName.Text.Trim() : string.Empty;
                    item.Hole[i].CompanyName = tbCompany != null ? tbCompany.Text.Trim() : string.Empty;
                    item.Hole[i].ProduceCompany = tbProduceCompany != null ? tbProduceCompany.Text.Trim() : string.Empty;
                    if (tbTaskName != null)
                    {
                        item.Hole[i].TaskName = tbTaskName.Text.Trim();
                        item.Hole[i].TaskCode = tbTaskName.DataContext != null ? tbTaskName.DataContext.ToString().Trim() : string.Empty;
                    }
                    else
                    {
                        item.Hole[i].TaskName = string.Empty;
                        item.Hole[i].TaskCode = string.Empty;
                    }
                    item.Hole[i].SampleId = tbSampleid != null ? tbSampleid.Text.Trim() : string.Empty;
                }
                FgdSelChannelWindow window = new FgdSelChannelWindow()
                {
                    _item = item,
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonShowAD_Click(object sender, RoutedEventArgs e)
        {
            FgdShowADWindow window = new FgdShowADWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            if (MainWindow._aPlayer != null)
            {
                MainWindow._aPlayer.Close();
                MainWindow._aPlayer = null;
            }
            this.Close();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            try
            {
                window.ComboBoxCategory.Text = "分光光度";
                window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
                if (_SelIndex >= 0)
                {
                    window.ComboBoxItem.Text = Global.fgdItems[_SelIndex].Name;
                }
                List<string> sList = new List<string>
                {
                    "全部",
                    "抑制率法",
                    "标准曲线法",
                    "动力学法",
                    "系数法"
                };
                window.ComboBoxMethod.ItemsSource = sList;
                window.ShowInTaskbar = false; window.Owner = this; window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(ButtonRecord_Click):\n" + ex.Message);
            }
        }

        private void ShowAllItems(List<DYFGDItemPara> items)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            try
            {
                WrapPanelItem.Children.Clear();
                _SelIndex = -1;
                int num = _SelIndex;
                foreach (DYFGDItemPara item in items)
                {
                    _SelIndex += 1;
                    UIElement element = GenerateItemBriefLayout(item, string.Empty, false);
                    WrapPanelItem.Children.Add(element);
                }
                _SelIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ShowAllItems):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowAllItems(List<DYFGDItemPara> items, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            int num = _SelIndex;
            bool FalseOrTrue = false;
            foreach (DYFGDItemPara item in items)
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

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show(this, "请先选择一个项目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Global.IsProject = false;
                //STDSampleSelect stdselect = new STDSampleSelect()
                //{
                //    ShowInTaskbar = false,
                //    Owner = this,
                //    _projectName =Global.fgdItems[_SelIndex].Name,  
                //    //_sel=_SelIndex
                //};
                //stdselect.ShowDialog();

                SearchSample searchSample = new SearchSample()
                {
                    ShowInTaskbar = false,
                    Owner = this,
                    _projectName = Global.fgdItems[_SelIndex].Name
                };
                searchSample.ShowDialog();
            }
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
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.CompanyName.Equals(string.Empty))
                            {
                                tbCompanyList[i].Text = Global.CompanyName;
                                Global.CompanyName = string.Empty;
                                tbProduceCompanyList[i].Text = string.Empty;
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
                            if (!Global.companystallNum.Equals(string.Empty))
                            {
                                tbProduceCompanyList[i].Text = Global.companystallNum;
                                
                                Global.companystallNum = string.Empty;
                            }
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
                                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, Global.fgdItems[_SelIndex].Name, true, true, 1);
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
                                                addWindow.textBoxName.Text = addWindow._projectName = Global.fgdItems[_SelIndex].Name;
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
                    var timer = element.Tag as Timer;
                    if (timer != null)
                    {
                        timer.Stop();
                        timer.Dispose();
                        ShowFgdSelChannelWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ShowBorder):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 检查项目实例化
        /// </summary>
        /// <param name="item">检查项</param>
        /// <param name="index">当前选中下标</param>
        /// <param name="name">样品名称</param>
        /// <param name="IsSelected">是否选中Border</param>
        /// <param name="SelIndex">当前下标</param>
        /// <returns></returns>
        public UIElement GenerateItemBriefLayout(DYFGDItemPara item, string name, bool IsSelected)
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
                Background = Brushes.AliceBlue
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
            // hole 检测通道 隐藏
            //WrapPanel wrapPannelHole = new WrapPanel();
            //wrapPannelHole.Width = 205;
            //wrapPannelHole.Height = 30;
            //wrapPannelHole.Visibility = System.Windows.Visibility.Collapsed;

            //Label labelSampleHole = new Label();
            //labelSampleHole.Width = 75;
            //labelSampleHole.Height = 26;
            //labelSampleHole.Margin = new Thickness(0, 2, 0, 0);
            //labelSampleHole.FontSize = 15;
            //labelSampleHole.Content = "检测通道:";
            //labelSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //TextBox textBoxSampleHole = new TextBox();
            //textBoxSampleHole.Width = 95;
            //textBoxSampleHole.Height = 26;
            //textBoxSampleHole.Margin = new Thickness(0, 2, 0, 0);
            //textBoxSampleHole.FontSize = 15;
            //textBoxSampleHole.Text = Global.deviceHole.GetWaveHoleString(item.Wave);
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
                Text = DYFGDItemPara.MethodToString[item.Method],
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };
            string strPreHeatTime = string.Empty;
            string strDetTime = string.Empty;
            if (0 == item.Method)
            {
                strPreHeatTime = string.Empty + item.ir.PreHeatTime;
                strDetTime = string.Empty + item.ir.DetTime;
            }
            else if (1 == item.Method)
            {
                strPreHeatTime = string.Empty + item.sc.PreHeatTime;
                strDetTime = string.Empty + item.sc.DetTime;
            }
            else if (2 == item.Method)
            {
                strPreHeatTime = "无";
                strDetTime = "无";
            }
            else if (3 == item.Method)
            {
                strPreHeatTime = string.Empty + item.dn.PreHeatTime;
                strDetTime = string.Empty + item.dn.DetTime;
            }
            else if (4 == item.Method)
            {
                strPreHeatTime = "无";
                strDetTime = "无";
            }

            #region 预热时间 隐藏
            //预热时间 隐藏
            //WrapPanel wrapPannelPreHeatTime = new WrapPanel();
            //wrapPannelPreHeatTime.Width = 205;
            //wrapPannelPreHeatTime.Height = 30;
            //wrapPannelPreHeatTime.Visibility = System.Windows.Visibility.Collapsed;

            //Label labelPreHeatTime = new Label();
            //labelPreHeatTime.Width = 75;
            //labelPreHeatTime.Height = 26;
            //labelPreHeatTime.Margin = new Thickness(0, 2, 0, 0);
            //labelPreHeatTime.FontSize = 15;
            //labelPreHeatTime.Content = "预热时间:";
            //labelPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //TextBox textBoxPreHeatTime = new TextBox();
            //textBoxPreHeatTime.Width = 95;
            //textBoxPreHeatTime.Height = 26;
            //textBoxPreHeatTime.Margin = new Thickness(0, 2, 0, 0);
            //textBoxPreHeatTime.FontSize = 15;
            //textBoxPreHeatTime.Text = strPreHeatTime;
            //textBoxPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            //textBoxPreHeatTime.IsReadOnly = true;

            //检测时间 隐藏
            //WrapPanel wrapPannelDetTime = new WrapPanel();
            //wrapPannelDetTime.Width = 205;
            //wrapPannelDetTime.Height = 30;
            //wrapPannelDetTime.Visibility = System.Windows.Visibility.Collapsed;

            //Label labelDetTime = new Label();
            //labelDetTime.Width = 75;
            //labelDetTime.Height = 26;
            //labelDetTime.Margin = new Thickness(0, 2, 0, 0);
            //labelDetTime.FontSize = 15;
            //labelDetTime.Content = "检测时间:";
            //labelDetTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //TextBox textBoxDetTime = new TextBox();
            //textBoxDetTime.Width = 95;
            //textBoxDetTime.Height = 26;
            //textBoxDetTime.Margin = new Thickness(0, 2, 0, 0);
            //textBoxDetTime.FontSize = 15;
            //textBoxDetTime.Text = strDetTime;
            //textBoxDetTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            //textBoxDetTime.IsReadOnly = true;
            #endregion

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
                Name = "SampleName",
                ToolTip = "双击可查询样品名称",
                IsReadOnly =true //Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;
            if (Global.IsSelectSampleName) textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY"))
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
                Content = Global.EachDistrict.Equals("GS") || Global.EachDistrict.Equals("CC") ? "*" : "",
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
                IsReadOnly = true //Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;

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
                Content = " ",
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
                Content = "摊位编号",
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
                Name = "ProduceCompany",
                IsReadOnly =true 
            };
            textBoxProduceCompany.MouseDoubleClick += textBoxProduceCompany_MouseDoubleClick;
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
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true,
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

                //项目顺序
                labelIndex = new Label()
                {
                    Width = 93,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "项目顺序",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
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
            stackPanel.Children.Add(wrapPannelMethod);
            stackPanel.Children.Add(wrapPannelSampleName);
            if (Global.InterfaceType.Equals("DY")) stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapCompany);
            if (Global.EachDistrict.Equals("GS")) stackPanel.Children.Add(wrapProduceCompany);
            if (Global.InterfaceType.Equals("ZH")) stackPanel.Children.Add(wrapSampleid);
            if (IsSetIndex) stackPanel.Children.Add(wrapIndex);

            border.Child = stackPanel;
            return border;
        }
        /// <summary>
        /// 摊位编号单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxProduceCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
            List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "ProduceCompany");
            for (int i = 0; i < borderList.Count; ++i)
            {
                if (sender == tbProduceCompanyList[i])
                {
                    if (tbCompanyList[i].Text.Trim() == "")
                    {
                        MessageBox.Show("请选择被检单位后再选择摊位编号","操作提示");
                        return;
                    }
                }
            }

             ManageStall windowStall = new ManageStall()
            {
                ShowInTaskbar = false,
                Owner = this
            };
             windowStall.ShowDialog();
             ShowBorder(sender, e, "ProduceCompany");
            
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
                            ShowBorder(sender, null, "SampleName");
                            return;
                        }
                    }

                    sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].RegisterID,
                                    Global.samplenameadapter[0].RegisterPassword, sampleCode, Global.samplenameadapter[0].ServerAddr);
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
                                sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].RegisterID,
                                        Global.samplenameadapter[0].RegisterPassword, sampleCode, Global.samplenameadapter[0].ServerAddr);
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
        /// 双击样品名称文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //弹出样品小精灵
            ShowSample();
            //双击样品名称文本框先选中当前border
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 鼠标右键点击了一个检查项时可直接进入测试方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowFgdSelChannelWindow();
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出任务小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchTaskWindow searchTaskWindow = new SearchTaskWindow();
            searchTaskWindow.ShowDialog();
            //双击任务主题文本框先选中当前border
            ShowBorder(sender, e, "TaskName");
        }

        /// <summary>
        /// 双击被检单位文本框先选中当前border
        /// 然后弹出被检单位选择界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchCompanyWindow windowCompany = new SearchCompanyWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Company");
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
            try
            {
                for (int j = 0; j < 2; j++)
                {
                    List<DYFGDItemPara> itemList = new List<DYFGDItemPara>();
                    itemList = Global.fgdItems;
                    List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                    for (int i = 0; i < borderList.Count; i++)
                    {
                        TextBox textBox = UIUtils.GetChildObject<TextBox>(borderList[i], "ProIndex");
                        itemList[i].Index = Global.StringConvertInt(textBox.Text.Trim());
                    }
                    itemList.Sort(delegate(DYFGDItemPara a, DYFGDItemPara b) { return a.Index.CompareTo(b.Index); });
                    Global.SerializeToFile(itemList, Global.fgdItemsFile);
                    ShowAllItems(Global.fgdItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(btnSaveProjects_Click):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 视频教程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex <= -1)
            {
                MessageBox.Show("未选择任何检测项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            try
            {
                if (Global.IsPlayer)
                {
                    MainWindow._aPlayer._ItemName = Global.fgdItems[_SelIndex].Name;
                    MainWindow._aPlayer.LoadPlayer();
                }
                else
                {
                    MainWindow._aPlayer = new APlayerForm();
                    MainWindow._aPlayer._ItemName = Global.fgdItems[_SelIndex].Name;
                    MainWindow._aPlayer.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "播放视频教程时出现异常!\r\n\r\n异常信息：" + ex.Message, "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 操作说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

    }
}