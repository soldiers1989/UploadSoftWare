using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.xaml.Dialog;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// HmSelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmSelChannelWindow : Window
    {
        public DYHMItemPara _item = null;
        Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        int _SelIndex = -1;
        int _checkNum = 0;

        public HmSelChannelWindow()
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
            List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxSampleName");
            List<ComboBox> listSampleSource = UIUtils.GetChildObjects<ComboBox>(WrapPanelChannel, "comboBoxSampleSource");
            List<TextBox> listTextBoxTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> listTextBoxCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");

            for (i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (_item.Hole[i].Use)
                {
                    _item.Hole[i].SampleName = listSampleName[i].Text;
                    //任务主题
                    _item.Hole[i].TaskCode = listTextBoxTask[i].DataContext.ToString();
                    _item.Hole[i].TaskName = listTextBoxTask[i].Text.ToString();
                    //被检单位
                    _item.Hole[i].CompanyName = listTextBoxCompany[i].Text.ToString();
                }
            }

            Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
            HmMeasureWindow window = new HmMeasureWindow();
            window._item = _item;
            window.ShowInTaskbar = false; 
            window.Owner = this; 
            window.ShowDialog();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
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
                UIElement element = GenerateChlBriefLayout(i, "");
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
            Border border = new Border();
            border.Width = 185;
            border.Height = 180;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.CornerRadius = new CornerRadius(10);
            border.BorderBrush = _borderBrushNormal;
            border.Background = Brushes.AliceBlue;
            border.MouseDown += Border_MouseDown;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 185;
            stackPanel.Height = 180;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 185;
            grid.Height = 40;

            Label labelChannel = new Label();
            labelChannel.FontSize = 20;
            labelChannel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            labelChannel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            labelChannel.Content = "检测通道 " + (channel + 1);

            //检测人员
            WrapPanel wrapPannelDetectPeople = new WrapPanel();
            wrapPannelDetectPeople.Width = 185;
            wrapPannelDetectPeople.Height = 30;

            Label labelDetectPeople = new Label();
            labelDetectPeople.Width = 75;
            labelDetectPeople.Height = 26;
            labelDetectPeople.Margin = new Thickness(0, 2, 0, 0);
            labelDetectPeople.FontSize = 15;
            labelDetectPeople.Content = "检测人员";
            labelDetectPeople.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            Label textBoxDetectPeople = new Label();
            textBoxDetectPeople.Width = 95;
            textBoxDetectPeople.Height = 26;
            textBoxDetectPeople.Margin = new Thickness(0, 2, 0, 0);
            textBoxDetectPeople.FontSize = 15;
            textBoxDetectPeople.Content = LoginWindow._userAccount.UserName;
            textBoxDetectPeople.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 185;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 75;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = "样品名称";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 95;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 0);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = name.Equals("") ? _item.Hole[channel].SampleName : name;
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
            textBoxSampleName.Name = "textBoxSampleName";
            textBoxSampleName.Tag = channel;
            textBoxSampleName.ToolTip = "双击可查询样品名称";

            //任务主题
            WrapPanel wrapPannelTask = new WrapPanel();
            wrapPannelTask.Width = 185;
            wrapPannelTask.Height = 30;

            Label labelTaskName = new Label();
            labelTaskName.Width = 75;
            labelTaskName.Height = 26;
            labelTaskName.Margin = new Thickness(0, 2, 0, 0);
            labelTaskName.FontSize = 15;
            labelTaskName.Content = "任务主题";
            labelTaskName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxTaskName = new TextBox();
            textBoxTaskName.Width = 95;
            textBoxTaskName.Height = 26;
            textBoxTaskName.Margin = new Thickness(0, 2, 0, 0);
            textBoxTaskName.FontSize = 15;
            textBoxTaskName.Text = _item.Hole[0].TaskName;
            textBoxTaskName.DataContext = _item.Hole[0].TaskCode;
            textBoxTaskName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
            textBoxTaskName.Name = "TaskName";
            textBoxTaskName.ToolTip = "双击可查询检测任务";

            //被检单位
            WrapPanel wrapPannelCompany = new WrapPanel();
            wrapPannelCompany.Width = 185;
            wrapPannelCompany.Height = 30;

            Label labelCompany = new Label();
            labelCompany.Width = 75;
            labelCompany.Height = 26;
            labelCompany.Margin = new Thickness(0, 2, 0, 0);
            labelCompany.FontSize = 15;
            labelCompany.Content = "被检单位";
            labelCompany.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxCompany = new TextBox();
            textBoxCompany.Width = 95;
            textBoxCompany.Height = 26;
            textBoxCompany.Margin = new Thickness(0, 2, 0, 0);
            textBoxCompany.FontSize = 15;
            textBoxCompany.Text = _item.Hole[0].CompanyName;
            textBoxCompany.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.Name = "Company";
            textBoxCompany.ToolTip = "双击可查询被检单位";

            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapPannelTask.Children.Add(labelTaskName);
            wrapPannelTask.Children.Add(textBoxTaskName);
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);

            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);
            stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapPannelCompany);

            border.Child = stackPanel;
            return border;
        }

        private void textBoxSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                string val = textBox.Text.Trim();
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
            catch (Exception)
            {

            }
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

            if (!"".Equals(content.Trim()))
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
            SearchTaskWindow searchTask = new SearchTaskWindow();
            searchTask.ShowDialog();
            //先选中当前border
            ShowBorder(sender, e, "task");
            Global.TaskCode = "";
            Global.TaskName = "";
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
            ShowBorder(sender, e, "sample");
            Global.sampleName = "";
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            SearchSample searchSample = new SearchSample();
            searchSample._projectName = this._item.Name;
            searchSample.ShowDialog();
        }

        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchCompanyWindow windowCompany = new SearchCompanyWindow();
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "company");
            Global.CompanyName = "";
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            List<Border> borderlist = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> textBoxSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxSampleName");
            List<TextBox> textBoxTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
            List<TextBox> textBoxCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            for (int i = 0; i < borderlist.Count; ++i)
            {
                if (type.Equals("border"))
                {
                    if (borderlist[i] == sender)
                    {
                        if (!_item.Hole[i].Use)
                        {
                            Global.SelIndex = _SelIndex = i;
                            borderlist[i].BorderBrush = _borderBrushSelected;
                            _item.Hole[i].Use = true;
                            _checkNum += 1;
                        }
                        else
                        {
                            borderlist[i].BorderBrush = _borderBrushNormal;
                            _item.Hole[i].Use = false;
                            _checkNum -= 1;
                        }
                        break;
                    }
                }
                else if (type.Equals("sample"))
                {
                    if (textBoxSampleNameList[i] == sender)
                    {
                        if (!_item.Hole[i].Use)
                        {
                            Global.SelIndex = _SelIndex = i;
                            borderlist[i].BorderBrush = _borderBrushSelected;
                            _item.Hole[i].Use = true;
                        }
                        if (!Global.sampleName.Equals(""))
                            textBoxSampleNameList[i].Text = Global.sampleName;
                        break;
                    }
                }
                else if (type.Equals("task"))
                {
                    if (textBoxTaskList[i] == sender)
                    {
                        if (!_item.Hole[i].Use)
                        {
                            Global.SelIndex = _SelIndex = i;
                            borderlist[i].BorderBrush = _borderBrushSelected;
                            _item.Hole[i].Use = true;
                        }
                        if (!Global.TaskName.Equals(""))
                        {
                            textBoxTaskList[i].Text = Global.TaskName;
                            textBoxTaskList[i].DataContext = Global.TaskCode;
                        }
                        break;
                    }
                }
                else if (type.Equals("company"))
                {
                    if (textBoxCompanyList[i] == sender)
                    {
                        if (!_item.Hole[i].Use)
                        {
                            Global.SelIndex = _SelIndex = i;
                            borderlist[i].BorderBrush = _borderBrushSelected;
                            _item.Hole[i].Use = true;
                        }
                        if (!Global.CompanyName.Equals(""))
                            textBoxCompanyList[i].Text = Global.CompanyName;
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