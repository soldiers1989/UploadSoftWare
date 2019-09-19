﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    /// <summary>
    /// GszSelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszSelChannelWindow : Window
    {
        public DYGSZItemPara _item = null;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        /// <summary>
        /// 接口若为省智慧平台则显示快检单号
        /// </summary>
        private Boolean InterfaceType = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        /// <summary>
        /// 是否是甘肃地区
        /// </summary>
        private Boolean EachDistrict = Global.EachDistrict.Equals("GS") ? true : false;

        public GszSelChannelWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
            {
                MessageBox.Show("项目异常", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            labelTitle.Content = _item.Name + "  检测通道选择";
            ShowAllChannel();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            // 是否选择了检测孔
            int i = 0;
            for (i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                    break;
            }
            if (Global.deviceHole.SxtCount == i)
            {
                MessageBox.Show(this, "请至少选择一个检测孔!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<TextBox> listTextBoxSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
            List<TextBox> listTextBoxTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> listTextBoxCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> listTextBoxProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
            List<TextBox> listTextBoxSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
            List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");
          
            
            for (i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    #region 必填验证
                    if (listTextBoxSampleName[i].Text.Trim().Length == 0)
                    {
                        MessageBox.Show("样品名称不能为空!\r\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        listTextBoxSampleName[i].Focus();
                        return;
                    }
                 
                    if (listTextBoxCompany[i].Text.Trim().Length == 0)
                    {
                        MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        listTextBoxCompany[i].Focus();
                        return;
                    }
                    
                    if (OpertorList[i].Text.Trim().Length == 0)
                    {
                        MessageBox.Show("经营户名称不能为空!\r\n双击选择输入", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        OpertorList[i].Focus();
                        return;
                    }

                    
                    #endregion
                    List<string> sNameList = new List<string>
                    {
                        listTextBoxSampleName[i].Text.Trim()
                    };
                    _item.SampleName = sNameList;
                    _item.Hole[i].SampleName = listTextBoxSampleName[i].Text.Trim();
                    _item.Hole[i].SampleId = listTextBoxSampleid.Count > 0 ? listTextBoxSampleid[i].Text.Trim() : string.Empty;
                    if (listTextBoxTask != null && listTextBoxTask.Count > 0)
                    {
                        _item.Hole[i].TaskCode = listTextBoxTask[i].DataContext.ToString();
                        _item.Hole[i].TaskName = listTextBoxTask[i].Text.Trim();
                    }
                    else
                    {
                        _item.Hole[i].TaskCode = _item.Hole[i].TaskName = string.Empty;
                    }
                    _item.Hole[i].CompanyName = listTextBoxCompany[i].Text.Trim();
                    _item.Hole[i].OpertorNames = OpertorList[i].Text.Trim();
                  
                    //生产单位
                    if (EachDistrict)
                        _item.Hole[i].ProduceCompany = listTextBoxProduceCompany[i].Text.Trim();
                }
            }
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            GszMeasureWindow window = new GszMeasureWindow()
            {
                _item = _item,
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            this.Close();
        }

        private void ShowAllChannel()
        {
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                UIElement element = GenerateChlBriefLayout(i, string.Empty);
                //广州投标用，123通道为干化学，最后一个通道为荧光和食源性微生物
                //if (i < 3)
                //{
                //    element.Visibility = Visibility.Visible;
                //}
                //else
                //{
                //    element.Visibility = Visibility.Collapsed;
                //}
                WrapPanelChannel.Children.Add(element);
            }
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            if (null == borderList)
                return;
            for (int i = 0; i < borderList.Count; ++i)
            {
                //2015年11月10日 wenj 修改进入检测通道后默认全部不选
                _item.Hole[i].Use = false;
                borderList[i].BorderBrush = _borderBrushNormal;
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
                ToolTip = "双击可查询样品名称",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;

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
                Height = 30
            };
            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content =  "*" ,
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
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;
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
                Text = _item.Hole[0].OpertorNames,
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
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("KJC"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
            }
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);
            wrapPannelProduceCompany.Children.Add(labelProduceCompany);
            wrapPannelProduceCompany.Children.Add(textBoxProduceCompany);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("KJC")) stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapPannelCompany);
            if (EachDistrict) stackPanel.Children.Add(wrapPannelProduceCompany);
            if (InterfaceType) stackPanel.Children.Add(wrapSampleid);
            stackPanel.Children.Add(wrapOperator);

            border.Child = stackPanel;
            return border;
        }

        private void textBoxOperator_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Operator");
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
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");
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
            ShowSample();
            //先选中当前border
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            //SearchSample searchSample = new SearchSample()
            //{
            //    ShowInTaskbar = false,
            //    Owner = this,
            //    _projectName = this._item.Name
            //};
            //searchSample.ShowDialog();
            SampleWindow window = new SampleWindow();
            window.ShowDialog();
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出检测任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchTaskWindow searchTask = new SearchTaskWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            searchTask.ShowDialog();
            //先选中当前border
            ShowBorder(sender, e, "TaskName");
        }

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

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
            List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
            List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
            List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");

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
                        break;
                    }
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
                        break;
                    }
                }
                else if (type.Equals("Company"))
                {
                    if (tbCompanyList[i] == sender)
                    {
                        OpertorList[i].Text = "";
                        if (!_item.Hole[i].Use)
                        {
                            Global.SelIndex = _SelIndex = i;
                            borderList[i].BorderBrush = _borderBrushSelected;
                            _item.Hole[i].Use = true;
                        }
                        if (!Global.CompanyName.Equals(string.Empty))
                        {
                            tbCompanyList[i].Text = Global.CompanyName;
                            Global.CompanyName = string.Empty;
                        }
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
                                    if (MessageBox.Show("当前样品名称在本地数据库中未匹配，是否新建该样品？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                    tbSampleNameList[i].Text = Global.sampleName;
                                }
                            }
                            Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE;
                        }
                        break;
                    }
                }
            }
            if (_checkNum == Global.deviceHole.SxtCount)
                this.CheckBoxSelAll.IsChecked = true;
            else if (_checkNum < Global.deviceHole.SxtCount)
                this.CheckBoxSelAll.IsChecked = false;
        }

        private void CheckBoxSelAll_Click(object sender, RoutedEventArgs e)
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

    }
}
