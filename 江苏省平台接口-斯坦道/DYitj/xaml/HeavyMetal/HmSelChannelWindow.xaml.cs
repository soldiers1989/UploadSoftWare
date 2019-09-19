using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AIO
{
    /// <summary>
    /// HmSelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmSelChannelWindow : Window
    {
        public DYHMItemPara _item = null;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        //接口若为省智慧平台则显示快检单号
        private Boolean InterfaceType = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        /// <summary>
        /// 是否是甘肃地区
        /// </summary>
        private Boolean EachDistrict = Global.EachDistrict.Equals("GS") ? true : false;

        public HmSelChannelWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (InterfaceType)
            {
                sp_01.Height += 30;
                sv_01.Height += 30;
            }
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
            for (i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (_item.Hole[i].Use)
                    break;
            }
            if (Global.deviceHole.HmCount == i)
            {
                MessageBox.Show("请至少选择一个检测孔");
                return;
            }

            // 下一步的时候，遍历所有的comboBoxSampleName 和 comboBoxSampleSource
            List<TextBox> listTextBoxSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxSampleName");
            List<ComboBox> listComboBoxSampleSource = UIUtils.GetChildObjects<ComboBox>(WrapPanelChannel, "comboBoxSampleSource");
            List<TextBox> listTextBoxTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> listTextBoxCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> listTextBoxProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
            List<TextBox> listTextBoxSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
            for (i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    #region 必填验证
                    if (listTextBoxSampleName[i].Text.Trim().Length == 0)
                    {
                        MessageBox.Show("请输入样品名称!\n\n可手工输入或双击选择样品", "操作提示");
                        listTextBoxSampleName[i].Focus();
                        return;
                    }
                    //禅城区被检单位为必填
                    if (Global.EachDistrict.Equals("CC"))
                    {
                        if (listTextBoxCompany[i].Text.Trim().Length == 0)
                        {
                            MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            listTextBoxCompany[i].Focus();
                            return;
                        }
                    }
                    if (EachDistrict)
                    {
                        if (listTextBoxCompany[i].Text.Trim().Length == 0)
                        {
                            MessageBox.Show("请输入被检单位!\n\n可手工输入或双击选择被检单位", "操作提示");
                            listTextBoxCompany[i].Focus();
                            return;
                        } if (listTextBoxProduceCompany[i].Text.Trim().Length == 0)
                        {
                            MessageBox.Show("生产单位不能为空!", "操作提示");
                            listTextBoxProduceCompany[i].Focus();
                            return;
                        }
                    }
                    if (InterfaceType && LoginWindow._userAccount.CheckSampleID)
                    {
                        if (listTextBoxSampleid[i].Text.Trim().Length == 0)
                        {
                            MessageBox.Show("快检单号不能为空!\n\n可手工输入或双击选择被检单位", "操作提示");
                            listTextBoxSampleid[i].Focus();
                            return;
                        }
                    }
                    #endregion
                    List<string> sNameList = new List<string>
                    {
                        listTextBoxSampleName[i].Text.Trim()
                    };
                    _item.SampleName = sNameList;
                    _item.Hole[i].SampleName = listTextBoxSampleName[i].Text.Trim();
                    _item.Hole[i].SampleId = listTextBoxSampleid.Count > 0 ? listTextBoxSampleid[i].Text.Trim() : string.Empty;
                    List<string> sampleList = new List<string>
                    {
                        listComboBoxSampleSource[i].Text.Trim()
                    };
                    //任务主题
                    if (listTextBoxTask != null && listTextBoxTask.Count > 0)
                    {
                        _item.Hole[i].TaskCode = listTextBoxTask[i].DataContext.ToString();
                        _item.Hole[i].TaskName = listTextBoxTask[i].Text.Trim();
                    }
                    else
                    {
                        _item.Hole[i].TaskName = string.Empty;
                        _item.Hole[i].TaskCode = string.Empty;
                    }
                    //被检单位
                    _item.Hole[i].CompanyName = listTextBoxCompany[i].Text.Trim();
                    //生产单位
                    if (EachDistrict)
                        _item.Hole[i].ProduceCompany = listTextBoxProduceCompany[i].Text.Trim();
                }
            }

            Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
            HmMeasureWindow window = new HmMeasureWindow()
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
            int sampleNum = _item.SampleNum;
            int holeUse = 0;
            for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (_item.Hole[i].Use || _item.Hole[i].SampleName != null)
                    holeUse += 1;
                UIElement element = GenerateChlBriefLayout(i, string.Empty);
                WrapPanelChannel.Children.Add(element);
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

            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");

            if (null == borderList)
                return;
            for (int i = 0; i < borderList.Count; ++i)
            {
                //2015年11月10日 wenj 修改进入检查通道后默认全部不选
                //if (_item.Hole[i].Use)
                //    borderList[i].BorderBrush = _borderBrushSelected;
                //else
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
                Width = 185,
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
                Width = 185,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 185,
                Height = 40
            };
            Label labelChannel = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = " 检测通道 " + (channel + 1)
            };

            //检测人员
            WrapPanel wrapPannelDetectPeople = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelDetectPeople = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测人员",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            Label textBoxDetectPeople = new Label()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = LoginWindow._userAccount.UserName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelSampleName = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = name.Equals(string.Empty) ? _item.Hole[channel].SampleName : name,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "textBoxSampleName",
                Tag = channel,
                ToolTip = "双击可查询样品名称"
            };
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            if (Global.EachDistrict.Equals("CC"))
            {
                textBoxSampleName.IsReadOnly = true;
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
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textBoxTaskName = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[0].TaskName,
                    DataContext = _item.Hole[0].TaskCode,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Name = "TaskName",
                    ToolTip = "双击可查询检测任务"
                };
                textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
            }

            //被检单位
            WrapPanel wrapPannelCompany = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "被检单位",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[0].CompanyName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位"
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            if (Global.EachDistrict.Equals("CC"))
            {
                textBoxCompany.IsReadOnly = true;
            }

            //生产单位
            WrapPanel wrapPannelProduceCompany = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelProduceCompany = new Label()
            {
                Width = 85,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "生产单位",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
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

            //快检单号
            WrapPanel wrapSampleid = null;
            if (InterfaceType)
            {
                wrapSampleid = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                Label labelSampleid = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "快检单号",
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelSampleid);

                TextBox tbSampleid = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[0].SampleId,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                wrapSampleid.Children.Add(tbSampleid);
            }

            
            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if (Global.InterfaceType.Equals("DY"))
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
            if (Global.InterfaceType.Equals("DY"))
            {
                stackPanel.Children.Add(wrapPannelTask);
            }
            stackPanel.Children.Add(wrapPannelCompany);
            if (EachDistrict)
                stackPanel.Children.Add(wrapPannelProduceCompany);
            if (InterfaceType)
                stackPanel.Children.Add(wrapSampleid);

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

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出检测任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "task");
            SearchTaskWindow searchTask = new SearchTaskWindow();
            searchTask.ShowDialog();
            //先选中当前border
            ShowBorder(sender, e, "task");
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
            ShowBorder(sender, e, "sample");
            //弹出样品小精灵
            ShowSample();
            //先选中当前border
            ShowBorder(sender, e, "sample");
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            SearchSample searchSample = new SearchSample()
            {
                _projectName = this._item.Name
            };
            searchSample.ShowDialog();
        }

        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "company");
            SearchCompanyWindow windowCompany = new SearchCompanyWindow();
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "company");
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxSampleName");
            List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
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
                else if (type.Equals("sample"))
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
                else if (type.Equals("task"))
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
                else if (type.Equals("company"))
                {
                    if (tbCompanyList[i] == sender)
                    {
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
                else if (type.Equals("getsampleCode"))
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
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE = string.Empty;
                            }
                        }
                        break;
                    }
                }
            }
            if (_checkNum == Global.deviceHole.HmCount)
                this.CheckBoxSelAll.IsChecked = true;
            else if (_checkNum < Global.deviceHole.HmCount)
                this.CheckBoxSelAll.IsChecked = false;
        }

    }
}