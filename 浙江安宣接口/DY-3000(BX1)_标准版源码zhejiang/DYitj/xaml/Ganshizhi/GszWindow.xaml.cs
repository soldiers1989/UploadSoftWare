using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.AnHui;
using AIO.src;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataSentence;

namespace AIO
{
    /// <summary>
    /// GszWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszWindow : Window
    {

        #region 全局变量
        private clsCompanyOpr _company = new clsCompanyOpr();
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Cp = new DataTable();
        private DataTable _Tc = new DataTable();
        int _SelIndex = -1;
        Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        VideoPlayback _video = null;
        private string logType = "GszWindow-error";
        #endregion

        public GszWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllItems(Global.gszItems);
                _SelIndex = -1;
                this.btnSaveProjects.Visibility = Global.IsSetIndex ? Visibility.Visible : Visibility.Collapsed;
                //btnPlayer.Visibility = ButtonBirefDescription.Visibility = Global.EnableVideo ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            GszEditItemWindow window = new GszEditItemWindow();
            window.ShowInTaskbar = false; 
            window.Owner = this; 
            window.ShowDialog();
            ShowAllItems(Global.gszItems);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目！", "操作提示");
                return;
            }
            try
            {
                DYGSZItemPara item = Global.gszItems[_SelIndex];
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
                        if (dialog.getResult())
                        {
                            if (dialog.getInput().Equals(item.Password))
                            {
                                canEdit = true;
                                break;
                            }
                            else
                            {
                                canEdit = false;
                                MessageBox.Show("密码错误！", "系统提示");
                                i--;
                            }
                        }
                    }
                }
                if (canEdit)
                {
                    GszEditItemWindow window = new GszEditItemWindow();
                    window._item = Global.gszItems[_SelIndex];
                    window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                    ShowAllItems(Global.gszItems, _SelIndex);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目！", "操作提示");
                return;
            }
            try
            {
                DYGSZItemPara item = Global.gszItems[_SelIndex];
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
                        if (dialog.getResult())
                        {
                            if (dialog.getInput().Equals(item.Password))
                            {
                                canEdit = true;
                                break;
                            }
                            else
                            {
                                canEdit = false;
                                MessageBox.Show("密码错误！", "系统提示");
                                i--;
                            }
                        }
                    }
                }
                if (canEdit)
                {
                    if (MessageBox.Show("确定要删除该项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Global.gszItems.RemoveAt(_SelIndex);
                        Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                        ShowAllItems(Global.gszItems);
                        _SelIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonStartWork_Click(object sender, RoutedEventArgs e)
        {
            ShowGszSelChannelWindow();
        }

        private void ShowGszSelChannelWindow()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请选择检测项目！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            //if (Global.IsServerTest)
            //{
            //    if (MessageBox.Show("检测到当前系统还未进行服务器通讯测试！\r\n\r\n为保证数据上传，请立即进行通讯测试", "操作提示",
            //         MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //    {
            //        SettingsWindow comwindow = new SettingsWindow()
            //        {
            //            ShowInTaskbar = false,
            //            Owner = this
            //        };
            //        comwindow.ShowDialog();
            //    }
            //}
            DYGSZItemPara item = Global.gszItems[_SelIndex];
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                TextBox tbSampleName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "SampleName");
                TextBox tbSampleType = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "SampleType");
                TextBox tbTaskName = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "TaskName");
                TextBox tbCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Company");
                TextBox tbProduceCompany = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "ProduceCompany");
                TextBox tbSampleid = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Sampleid");
                TextBox tbProject = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "project");

                if (tbSampleName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("样品名称不能为空!\r\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    tbSampleName.Focus();
                    return;
                }
                if (tbProject.Text.Length == 0)
                {
                    MessageBox.Show("项目指标不能为空!\r\n请选择选择项目指标", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                if (Global.InterfaceType.Equals("ZH"))// && LoginWindow._userAccount.CheckSampleID)
                {
                    if (tbSampleid.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("快检单号不能为空!\r\n可双击文本框选择快检单号", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tbSampleid.Focus();
                        return;
                    }
                }
                
                if (Global.InterfaceType.Equals("AH"))
                {
                    DataTable dataTable = DataOperation.GetCheckProjectByName(item.Name);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        List<data_dictionary> dataList = (List<data_dictionary>)IListDataSet.DataTableToIList<data_dictionary>(dataTable, 1);
                        item.testPro = dataList[0].codeId;
                    }
                    else
                    {
                        MessageBox.Show("检测项目 [" + item.Name + "] 在后台查询不到数据，请联系平台管理员添加！", "操作提示");
                        return;
                    }

                    if (tbSampleType.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("样品种类不能为空!\r\n可手工输入或双击选择种类", "操作提示");
                        tbSampleType.Focus();
                        return;
                    }
                }

                //甘肃 被检单位&生产单位必填
                if (Global.InterfaceType.Equals("GS"))
                {
                    if (tbCompany.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("被检单位不能为空!\r\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tbCompany.Focus();
                        return;
                    }

                    if (tbProduceCompany.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("生产单位不能为空!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tbProduceCompany.Focus();
                        return;
                    }
                }

                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
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
                    if (Global.InterfaceType.Equals("AH"))
                    {
                        item.Hole[i].SampleTypeName = tbSampleType.Text.Trim();
                        item.Hole[i].SampleTypeCode = tbSampleType.DataContext == null ? string.Empty : tbSampleType.DataContext.ToString();
                    }
                    item.Hole[i].SampleId = tbSampleid != null ? tbSampleid.Text.Trim() : string.Empty;
                }
                if (!string.IsNullOrEmpty(tbSampleName.Text))
                {
                    if (Global.CheckItemAndFoodIsNull(item.Name, item.Hole[0].SampleName))
                    {
                        if (MessageBox.Show(string.Format("检测到样品[{0}]没有对应检测标准，是否立即添加检测标准？\r\n\r\n备注：没有对应检测标准时，可能无法得到准确的检测结果！", item.Hole[0].SampleName), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AddOrUpdateSample swindow = new AddOrUpdateSample();
                            swindow.textBoxName.Text = swindow._projectName = item.Name;
                            swindow._sampleName = item.Hole[0].SampleName;
                            swindow._addOrUpdate = "ADD";
                            swindow.ShowDialog();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
            GszSelChannelWindow window = new GszSelChannelWindow();
            window._item = item;
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_video != null)
            {
                _video.Close();
            }
            this.Close();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxCategory.Text = "干化学";
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            if (_SelIndex >= 0)
            {
                window.ComboBoxItem.Text = Global.gszItems[_SelIndex].Name;
            }
            List<string> sList = new List<string>();
            sList.Add("全部"); sList.Add("定性"); sList.Add("定量");
            window.ComboBoxMethod.ItemsSource = sList;
            window.ShowInTaskbar = false; 
            window.Owner = this; 
            window.ShowDialog();
        }

        private void ShowAllItems(List<DYGSZItemPara> items)
        {
            // 将gszItems的内容项添加到主界面显示出来。
            try
            {
                WrapPanelItem.Children.Clear();
                _SelIndex = -1;
                foreach (DYGSZItemPara item in items)
                {
                    _SelIndex += 1;
                    UIElement element = GenerateItemBriefLayout(item, string.Empty, false);
                    WrapPanelItem.Children.Add(element);
                }
                _SelIndex = -1;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowAllItems(List<DYGSZItemPara> items, int index)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            try
            {
                WrapPanelItem.Children.Clear();
                _SelIndex = -1;
                int num = _SelIndex;
                bool FalseOrTrue = false;
                foreach (DYGSZItemPara item in items)
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
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
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
                List<TextBox> tbpoject = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "project");  //检测项目
                List<TextBox> tbSampleTypeList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "SampleType");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "ProduceCompany");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Sampleid");
                if (null == borderList) return;

                //先将所有的项目不选中
                for (int i = 0; i < borderList.Count; i++)
                {
                    borderList[i].BorderBrush = _borderBrushNormal;
                }
                clsDownChkItem downitem = new clsDownChkItem();
                string err = "";
                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (type.Equals("border"))
                    {
                        if (sender == borderList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            //浙江查询当前选中检测项目是否在平台上有
                            string CurrentItem = Global.gszItems[_SelIndex].Name;

                            DataTable itemname = downitem.GetDownItem(" itemName='" + CurrentItem + "'", "", out err);
                            if (itemname != null)
                            {
                                if (itemname.Rows.Count > 0)
                                {
                                    tbpoject[i].Text = Global.fgdItems[_SelIndex].Name;
                                    Global.itemCode = itemname.Rows[0][2].ToString();
                                    Global.itenName = Global.fgdItems[_SelIndex].Name;//浙江记录项目名称
                                }
                                else
                                {
                                    Global.projectName = "";
                                    ShowDownItem showitem = new ShowDownItem();
                                    showitem.ShowDialog();
                                    if (Global.IsTestC == 1)
                                    {
                                        tbpoject[i].Text = Global.itenName;//浙江记录项目名称
                                    }
                                }
                            }
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
                                tbSampleNameList[i].Text = Global.sampleName;
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("SampleType"))
                    {
                        if (sender == tbSampleTypeList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.projectName.Equals(string.Empty))
                                tbSampleTypeList[i].Text = Global.projectName;
                            if (!Global.projectUnit.Equals(string.Empty))
                                tbSampleTypeList[i].DataContext = Global.projectUnit;
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
                            }
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("project"))//检测项目
                    {
                        if (sender == tbpoject[i])
                        {
                            _SelIndex = i;
                            ShowDownItem showitem = new ShowDownItem();
                            showitem.ShowDialog();
                            if (Global.IsTestC == 1)
                            {
                                tbpoject[i].Text = Global.itenName;
                            }
                        }
                    }
                    else if (type.Equals("Company"))
                    {
                        if (sender == tbCompanyList[i])
                        {
                            _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if (!Global.CompanyName.Equals(string.Empty))
                                tbCompanyList[i].Text = Global.CompanyName;
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
                                tbSampleNameList[i].Text = Global.sampleName;
                                //if (Global.IsSelectSampleName)
                                //{
                                //    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, Global.fgdItems[_SelIndex].Name, true, true, 1);
                                //    if (dataTable != null && dataTable.Rows.Count > 0)
                                //    {
                                //        tbSampleNameList[i].Text = Global.sampleName;
                                //    }
                                //    else
                                //    {
                                //        if (MessageBox.Show("当前样品名称在本地数据库中未匹配，是否新建该样品？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //        {
                                //            AddOrUpdateSample addWindow = new AddOrUpdateSample();
                                //            try
                                //            {
                                //                addWindow.textBoxName.Text = addWindow._projectName = Global.fgdItems[_SelIndex].Name;
                                //                addWindow._sampleName = Global.sampleName;
                                //                addWindow.ShowDialog();
                                //            }
                                //            catch (Exception ex)
                                //            {
                                //                MessageBox.Show(ex.Message);
                                //            }
                                //        }
                                //        tbSampleNameList[i].Text = Global.sampleName;
                                //    }
                                //}
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE = string.Empty;
                            }
                            return;
                        }
                        else
                            borderList[i].BorderBrush = _borderBrushNormal;
                    }
                }

                //双击进行检测
                var element = (FrameworkElement)sender;
                if (e.ClickCount == 1)
                {
                    var timer = new System.Timers.Timer(500);
                    timer.AutoReset = false;
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
                        ShowGszSelChannelWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        public UIElement GenerateItemBriefLayout(DYGSZItemPara item, string name, bool FalseOrTrue)
        {
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
            if (FalseOrTrue) border.BorderBrush = _borderBrushSelected;
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
                Text = DYGSZItemPara.MethodToString[item.Method],
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsReadOnly = true
            };

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 205;
            wrapPannelSampleName.Height = 30;

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

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 95;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 0);
            textBoxSampleName.FontSize = 15;
            //textBoxSampleName.Text = name.Trim().Equals(string.Empty) ? item.Hole[0].SampleName : name.Trim().ToString();
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.Name = "SampleName";
            textBoxSampleName.ToolTip = "双击可查询样品名称";
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;

            //项目指标
            WrapPanel wrapPannelProject = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };

            Label labelProject = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(17, 2, 0, 0),
                FontSize = 15,
                Content = "项目指标",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxProject = new TextBox()
            {
                IsReadOnly = true,
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = name.Trim().Equals(string.Empty) ? string.Empty : name.Trim().ToString(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "project"
            };
            textBoxProject.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(textBoxProject_PreviewMouseLeftButtonDown);
            textBoxProject.ToolTip = "单击选择检测项目";


            //样品种类
            WrapPanel wrapPannelSampleType = null;
            Label labelSampleType = null;
            TextBox textBoxSampleType = null;
            if (Global.InterfaceType.Equals("AH"))
            {
                wrapPannelSampleType = new WrapPanel();
                wrapPannelSampleType.Width = 205;
                wrapPannelSampleType.Height = 30;

                Label labelX_SampleType = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 18,
                    FontSize = 15,
                    Content = "*",
                    Foreground = new SolidColorBrush(Colors.Red),
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapPannelSampleType.Children.Add(labelX_SampleType);

                labelSampleType = new Label();
                labelSampleType.Width = 75;
                labelSampleType.Height = 26;
                labelSampleType.Margin = new Thickness(0, 2, 0, 0);
                labelSampleType.FontSize = 15;
                labelSampleType.Content = "样品种类";
                labelSampleType.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                labelSampleType.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

                textBoxSampleType = new TextBox();
                textBoxSampleType.Width = 95;
                textBoxSampleType.Height = 26;
                textBoxSampleType.Margin = new Thickness(0, 2, 0, 0);
                textBoxSampleType.FontSize = 15;
                textBoxSampleType.Text = string.Empty;
                textBoxSampleType.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textBoxSampleType.Name = "SampleType";
                textBoxSampleType.MouseDoubleClick += textBoxSampleType_MouseDoubleClick;
                textBoxSampleType.ToolTip = "双击可查询样品种类";
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS"))
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
                Content = Global.InterfaceType.Equals("GS") ? "*" : "",
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
                IsReadOnly = Global.InterfaceType.Equals("GS") ? true : false
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
            if (Global.IsSetIndex)
            {
                wrapIndex = new WrapPanel();
                wrapIndex.Width = 205;
                wrapIndex.Height = 30;

                labelIndex = new Label();
                labelIndex.Width = 93;
                labelIndex.Height = 26;
                labelIndex.Margin = new Thickness(0, 2, 0, 0);
                labelIndex.FontSize = 15;
                labelIndex.Content = "项目顺序";
                labelIndex.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                labelIndex.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

                textIndex = new TextBox();
                textIndex.Width = 95;
                textIndex.Height = 26;
                textIndex.Margin = new Thickness(0, 2, 0, 0);
                textIndex.FontSize = 15;
                textIndex.Text = (_SelIndex + 1).ToString();
                textIndex.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textIndex.Name = "ProIndex";
                textIndex.TextChanged += textIndex_TextChanged;
            }

            gridLabelName.Children.Add(labelName);
            wrapPannelMethod.Children.Add(labelMethod);
            wrapPannelMethod.Children.Add(textBoxMethod);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapPannelProject.Children.Add(labelProject);  //检定项目
            wrapPannelProject.Children.Add(textBoxProject);
            wrapCompany.Children.Add(labelCompany);
            wrapCompany.Children.Add(textBoxCompany);
            stackPanel.Children.Add(gridLabelName);
            stackPanel.Children.Add(wrapPannelMethod);
            stackPanel.Children.Add(wrapPannelProject);
            stackPanel.Children.Add(wrapPannelSampleName);

            if (Global.InterfaceType.Equals("AH"))
            {
                wrapPannelSampleType.Children.Add(labelSampleType);
                wrapPannelSampleType.Children.Add(textBoxSampleType);
                stackPanel.Children.Add(wrapPannelSampleType);
            }

            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
                //stackPanel.Children.Add(wrapPannelTask);
            }

            //stackPanel.Children.Add(wrapCompany);

            if (Global.InterfaceType.Equals("GS"))
            {
                wrapProduceCompany.Children.Add(labelProduceCompany);
                wrapProduceCompany.Children.Add(textBoxProduceCompany);
                stackPanel.Children.Add(wrapProduceCompany);
            }

            if (Global.InterfaceType.Equals("ZH")) stackPanel.Children.Add(wrapSampleid);
            if (Global.IsSetIndex)
            {
                wrapIndex.Children.Add(labelIndex);
                wrapIndex.Children.Add(textIndex);
                stackPanel.Children.Add(wrapIndex);
            }

            border.Child = stackPanel;
            return border;
        }
        /// <summary>
        /// 检测项目单击项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void textBoxProject_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "project");
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
            Wisdom.GETSAMPLECODE = string.Empty;
            Global.sampleName = string.Empty;
            Global.CompanyName = string.Empty;
            SearchSampleidWindow windowCompany = new SearchSampleidWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Sampleid");
        }

        /// <summary>
        /// 双击样品种类文本框先选中当前border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Global.projectName = Global.projectUnit = string.Empty;
            SearchFoodTypeWindow window = new SearchFoodTypeWindow();
            window.ShowDialog();
            ShowBorder(sender, e, "SampleType");
            Global.projectName = Global.projectUnit = string.Empty;
        }

        private void textBoxCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e,  "Company");
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
        /// 单击样品名称文本框弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowSample();
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请先选择一个项目！", "操作提示");
            }
            else
            {
                Global.IsProject = false;
                SearchSample searchSample = new SearchSample();
                searchSample._projectName = Global.gszItems[_SelIndex].Name;
                searchSample.ShowDialog();
                //ShowItems(Global.gszItems, Global.sampleName, _SelIndex);
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
            Global.sampleName = string.Empty;
        }

        /// <summary>
        /// 鼠标右键点击了一个检查项时可直接进入测试方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowGszSelChannelWindow();
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
                FileUtils.OprLog(3, logType, ex.ToString());
                error = ex.Message;
                MessageBox.Show("视频存储路径不正确！", "操作提示");
            }
            finally
            {
                if (error.Equals(string.Empty))
                {
                    Global.IsPlayer = true;
                    _video = new VideoPlayback();
                    _video.playIndex = -1;
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
            SearchTaskWindow searchTaskWindow = new SearchTaskWindow();
            searchTaskWindow.ShowDialog();
            //双击任务主题文本框先选中当前border
            ShowBorder(sender, e, "TaskName");
            Global.TaskName = string.Empty;
            Global.TaskCode = string.Empty;
        }

        /// <summary>
        /// 双击被检单位文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchCompanyWindow windowCompany = new SearchCompanyWindow();
            windowCompany.ShowDialog();
            ShowBorder(sender, e,  "Company");
            Global.CompanyName = string.Empty;
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
                    List<DYGSZItemPara> itemList = new List<DYGSZItemPara>();
                    itemList = Global.gszItems;
                    List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                    for (int i = 0; i < borderList.Count; i++)
                    {
                        TextBox textBox = UIUtils.GetChildObject<TextBox>(borderList[i], "ProIndex");
                        itemList[i].Index = int.Parse(textBox.Text.Trim());
                    }
                    itemList.Sort(delegate(DYGSZItemPara a, DYGSZItemPara b) { return a.Index.CompareTo(b.Index); });
                    Global.SerializeToFile(itemList, Global.gszItemsFile);
                    ShowAllItems(Global.gszItems);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

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
                    MainWindow._aPlayer._ItemName = Global.gszItems[_SelIndex].Name;
                    MainWindow._aPlayer.LoadPlayer();
                }
                else
                {
                    MainWindow._aPlayer = new APlayerForm();
                    MainWindow._aPlayer._ItemName = Global.gszItems[_SelIndex].Name;
                    MainWindow._aPlayer.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "播放视频教程时出现异常!\r\n\r\n异常信息：" + ex.Message, "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBirefDescription_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Format(@"{0}\KnowledgeBbase\检测项目操作说明\{1}.rtf", Environment.CurrentDirectory, Global.gszItems[_SelIndex].Name);
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Global.IsOpenPrompt = true;
                PromptWindow window = new PromptWindow();
                window._HintStr = Global.gszItems[_SelIndex].HintStr;
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