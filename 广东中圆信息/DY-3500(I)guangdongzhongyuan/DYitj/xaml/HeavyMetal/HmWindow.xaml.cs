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
using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// hmWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmWindow : Window
    {

        #region 全局变量
        private clsCompanyOpr _company = new clsCompanyOpr();
        private clsTaskOpr _taskcheck = new clsTaskOpr();
        private DataTable _Cp = new DataTable();
        private DataTable _Tc = new DataTable();
        int _SelIndex = -1;
        Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        VideoPlayback _video = null;
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
            this.Image.Visibility = Global.EnableVideo ? (Global.IsPlayer ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
            //_Cp = _company.GetAsDataTable(string.Empty, string.Empty, 4);
            //_Tc = _taskcheck.GetAsDataTable(string.Empty, string.Empty, 2);
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            HmEditItemWindow window = new HmEditItemWindow();
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
            ShowAllItems(Global.hmItems);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目！", "操作提示");
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
                            MessageBox.Show("密码错误");
                            i--;
                        }
                    }
                }
            }
            if (canEdit)
            {
                HmEditItemWindow window = new HmEditItemWindow();
                window._item = Global.hmItems[_SelIndex];
                window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                ShowAllItems(Global.hmItems, _SelIndex);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("未选择任何项目！", "操作提示");
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
                            MessageBox.Show("密码错误");
                            i--;
                        }
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
            TextBox textBox = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "sampleName");
            TextBox tsk = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "TaskName");
            TextBox Cop = UIUtils.GetChildObject<TextBox>(borderList[_SelIndex], "Company");
            DYHMItemPara item = Global.hmItems[_SelIndex];
            for (int i = 0; i < Global.deviceHole.HmCount; ++i)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                    item.Hole[i].SampleName = textBox.Text;
                else
                {
                    MessageBox.Show("请输入样品名称！", "操作提示");
                    return;
                }
                item.Hole[i].TaskName = tsk.Text.Trim().ToString();
                item.Hole[i].TaskCode = tsk.DataContext.ToString();
                item.Hole[i].CompanyName = Cop.Text;
            }

            HmSelChannelWindow window = new HmSelChannelWindow();
            window._item = item;
            window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
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
            window.ComboBoxCategory.Text = "重金属";
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            if (_SelIndex >= 0)
            {
                window.ComboBoxItem.Text = Global.hmItems[_SelIndex].Name;
            }
            List<string> sList = new List<string>();
            sList.Add("全部");
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
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            List<TextBox> textBoxSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "sampleName");
            List<TextBox> textBoxTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "TaskName");
            List<TextBox> textBoxCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "Company");
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
                    if (sender == textBoxSampleNameList[i])
                    {
                        _SelIndex = i;
                        borderList[i].BorderBrush = _borderBrushSelected;
                        if (!Global.sampleName.Equals(string.Empty))
                            textBoxSampleNameList[i].Text = Global.sampleName;
                    }
                    else
                        borderList[i].BorderBrush = _borderBrushNormal;
                }
                else if (type.Equals("task"))
                {
                    if (sender == textBoxTaskList[i])
                    {
                        _SelIndex = i;
                        borderList[i].BorderBrush = _borderBrushSelected;
                        if (!Global.TaskName.Equals(string.Empty))
                        {
                            textBoxTaskList[i].Text = Global.TaskName;
                            textBoxTaskList[i].DataContext = Global.TaskCode;
                        }
                    }
                    else
                        borderList[i].BorderBrush = _borderBrushNormal;
                }
                else if (type.Equals("company"))
                {
                    if (sender == textBoxCompanyList[i])
                    {
                        _SelIndex = i;
                        borderList[i].BorderBrush = _borderBrushSelected;
                        if (!Global.CompanyName.Equals(string.Empty))
                            textBoxCompanyList[i].Text = Global.CompanyName;
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
                    ShowHmSelChannelWindow();
                }
            }
        }

        private UIElement GenerateItemBriefLayout(DYHMItemPara item, string name, bool FalseOrTrue)
        {
            Border border = new Border();
            border.Width = 185;
            border.Height = Global.IsSetIndex ? 180 : 150;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.CornerRadius = new CornerRadius(10);
            border.BorderBrush = name.Equals(string.Empty) ? _borderBrushNormal : _borderBrushSelected;
            if (FalseOrTrue)
                border.BorderBrush = _borderBrushSelected;
            border.MouseDown += Border_MouseDown;
            border.Background = Brushes.AliceBlue;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 185;
            stackPanel.Height = Global.IsSetIndex ? 180 : 150;
            stackPanel.Name = "stackPanel";

            Grid gridLabelName = new Grid();
            gridLabelName.Width = 185;
            gridLabelName.Height = 40;

            Label labelName = new Label();
            labelName.FontSize = 20;
            labelName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            labelName.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            labelName.Content = item.Name;
            labelName.Name = "labelName";

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
            //labelSampleHole.Content = "检测孔";
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
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = name.Trim().Equals(string.Empty) ? item.Hole[0].SampleName : name.Trim().ToString();
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.Name = "sampleName";
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
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
            textBoxTaskName.Text = string.Empty;
            textBoxTaskName.DataContext = string.Empty;
            textBoxTaskName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
            textBoxTaskName.Name = "TaskName";
            textBoxTaskName.ToolTip = "双击可查询所有任务";

            //被检单位
            WrapPanel wrapCompany = new WrapPanel();
            wrapCompany.Width = 185;
            wrapCompany.Height = 30;

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
            textBoxCompany.Text = string.Empty;
            textBoxCompany.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.Name = "Company";
            textBoxCompany.ToolTip = "双击可查询所有被检单位";

            //项目顺序
            WrapPanel wrapIndex = null;
            Label labelIndex = null;
            TextBox textIndex = null;
            if (Global.IsSetIndex)
            {
                wrapIndex = new WrapPanel();
                wrapIndex.Width = 185;
                wrapIndex.Height = 30;

                labelIndex = new Label();
                labelIndex.Width = 75;
                labelIndex.Height = 26;
                labelIndex.Margin = new Thickness(0, 2, 0, 0);
                labelIndex.FontSize = 15;
                labelIndex.Content = "项目顺序";
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
            //wrapPannelHole.Children.Add(labelSampleHole);
            //wrapPannelHole.Children.Add(textBoxSampleHole);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if (Global.IsSetIndex)
            {
                wrapIndex.Children.Add(labelIndex);
                wrapIndex.Children.Add(textIndex);
            }

            wrapPannelTask.Children.Add(labelTaskName);
            wrapPannelTask.Children.Add(textBoxTaskName);
            wrapCompany.Children.Add(labelCompany);
            wrapCompany.Children.Add(textBoxCompany);
            stackPanel.Children.Add(gridLabelName);
            //stackPanel.Children.Add(wrapPannelHole);
            stackPanel.Children.Add(wrapPannelSampleName);
            stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapCompany);
            if (Global.IsSetIndex)
                stackPanel.Children.Add(wrapIndex);

            border.Child = stackPanel;
            return border;
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
            Global.sampleName = string.Empty;
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
                searchSample._projectName = Global.hmItems[_SelIndex].Name;
                searchSample.ShowDialog();
                //ShowItems(Global.hmItems, Global.sampleName, _SelIndex);
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
            ShowBorder(sender, e, "task");
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
            ShowBorder(sender, e, "company");
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
