using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// WarnTaskShow.xaml 的交互逻辑
    /// </summary>
    public partial class WarnTaskShow : Window
    {

        private clsCompanyOpr _company = new clsCompanyOpr();
        private clsTaskOpr _taskcheck = new clsTaskOpr();
        private DataTable _Cp = new DataTable();
        private DataTable _Tc = new DataTable();
        private int _SelIndex = -1;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));

        public WarnTaskShow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _Tc = _taskcheck.GetAsDataTable("", "", 3);
                if (_Tc != null && _Tc.Rows.Count > 0)
                    ShowAll(_Tc);
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-Window_Loaded:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            //ShowAllItems(Global.userAccounts);
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginNameEdit window = new LoginNameEdit();
                window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                ShowAllItems(Global.userAccounts);
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-ButtonAddItem_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }


        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
                return;

            bool canEdit = false;
            try
            {
                UserAccount item = Global.userAccounts[_SelIndex];
                if ("".Equals(item.UserPassword))
                {
                    canEdit = true;
                }
                else
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.getResult())
                    {
                        if (dialog.getInput().Equals(item.UserPassword))
                        {
                            canEdit = true;
                        }
                        else
                        {
                            canEdit = false;
                            MessageBox.Show("密码错误");
                            return;
                        }
                    }
                }
                if (canEdit)
                {
                    LoginNameEdit window = new LoginNameEdit();
                    window._item = Global.userAccounts[_SelIndex];
                    window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                    ShowAllItems(Global.userAccounts);
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-ButtonEditItem_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
                return;

            bool canEdit = false;
            try
            {
                UserAccount item = Global.userAccounts[_SelIndex];
                if ("".Equals(item.UserPassword))
                {
                    canEdit = true;
                }
                else
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.getResult())
                    {
                        if (dialog.getInput().Equals(item.UserPassword))
                        {
                            canEdit = true;
                        }
                        else
                        {
                            canEdit = false;
                            MessageBox.Show("密码错误");
                            return;
                        }
                    }
                }
                if (canEdit)
                {
                    Global.userAccounts.RemoveAt(_SelIndex);
                    _SelIndex = -1;
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                    ShowAllItems(Global.userAccounts);
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-ButtonDelItem_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAllItems(List<UserAccount> items)
        {
            // 将userAccounts的内容项添加到主界面显示出来。
            WrapPanelItem.Children.Clear();
            _SelIndex = -1;
            //foreach (UserAccount item in items)
            //{
            //UIElement element = GenerateItemBriefLayout(item);
            //WrapPanelItem.Children.Add(element);
            //}
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                if (borderList == null || borderList.Count <= 0)
                    return;

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (sender == borderList[i])
                    {
                        _SelIndex = i;
                        borderList[i].BorderBrush = _borderBrushSelected;
                    }
                    else
                    {
                        borderList[i].BorderBrush = _borderBrushNormal;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-Border_MouseDown:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowAll(DataTable dt)
        {
            try
            {
                WrapPanelItem.Children.Clear();
                _SelIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["v30"].ToString()) < Convert.ToInt32(dt.Rows[i]["PLANDCOUNT"].ToString()))
                    {
                        UIElement element = GenerateItemBriefLayout(dt.Rows[i]);
                        WrapPanelItem.Children.Add(element);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-ShowAll:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private UIElement GenerateItemBriefLayout(DataRow item)
        {
            Border border = new Border();
            try
            {
                border.Width = 210;
                border.Height = 270;
                border.Margin = new Thickness(10);
                border.BorderThickness = new Thickness(5);
                border.CornerRadius = new CornerRadius(10);
                border.BorderBrush = _borderBrushNormal;
                border.MouseDown += Border_MouseDown;
                border.Background = Brushes.PaleVioletRed;
                border.Name = "border";
                /// stackpanel
                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 200;
                stackPanel.Height = 300;
                stackPanel.SetResourceReference(StackPanel.StyleProperty, "stackPanelStyle1");
                stackPanel.Name = "stackPanel";
                ///新建模块生成方法
                /// //登陆人
                Grid gridLabelName = new Grid();
                gridLabelName.Width = 200;
                gridLabelName.Height = 40;
                Label labelName = new Label();
                labelName.FontSize = 13;
                labelName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                labelName.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                labelName.Content = "“" + item.ItemArray[1].ToString() + "”未完成！";
                labelName.Name = "labelName";
                /// hole 分光光度检测模块
                WrapPanel wrapPannelHole = new WrapPanel();
                wrapPannelHole.Width = 200;
                wrapPannelHole.Height = 30;
                Label labelSampleHole = new Label();
                labelSampleHole.Width = 100;
                labelSampleHole.Height = 26;
                labelSampleHole.Margin = new Thickness(5, 2, 5, 2);
                labelSampleHole.FontSize = 13;
                labelSampleHole.Content = "开始时间";
                labelSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                TextBox textBoxSampleHole = new TextBox();
                textBoxSampleHole.Width = 82;
                textBoxSampleHole.Height = 26;
                textBoxSampleHole.Margin = new Thickness(0, 2, 0, 2);
                textBoxSampleHole.FontSize = 13;
                textBoxSampleHole.Text = item.ItemArray[2].ToString();
                textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textBoxSampleHole.IsEnabled = false;
                //胶体金检测模块
                WrapPanel wrapPannelMethod = new WrapPanel();
                wrapPannelMethod.Width = 200;
                wrapPannelMethod.Height = 30;
                Label labelMethod = new Label();
                labelMethod.Width = 100;
                labelMethod.Height = 26;
                labelMethod.Margin = new Thickness(5, 2, 5, 2);
                labelMethod.FontSize = 13;
                labelMethod.Content = "结束时间";
                labelMethod.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                TextBox textBoxMethod = new TextBox();
                textBoxMethod.Width = 82;
                textBoxMethod.Height = 26;
                textBoxMethod.Margin = new Thickness(0, 2, 0, 2);
                textBoxMethod.FontSize = 13;
                textBoxMethod.Text = item.ItemArray[3].ToString();
                textBoxMethod.IsEnabled = false;
                textBoxMethod.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                WrapPanel wrapPannelPreHeatTime = new WrapPanel();
                wrapPannelPreHeatTime.Width = 200;
                wrapPannelPreHeatTime.Height = 30;
                Label labelPreHeatTime = new Label();
                labelPreHeatTime.Width = 100;
                labelPreHeatTime.Height = 80;
                labelPreHeatTime.Margin = new Thickness(5, 2, 5, 2);
                labelPreHeatTime.FontSize = 13;
                labelPreHeatTime.Content = "信息内容";
                //labelPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                WrapPanel wrapPannelDetTime = new WrapPanel();
                wrapPannelDetTime.Width = 200;
                wrapPannelDetTime.Height = 90;
                TextBox textBoxDetTime = new TextBox();
                textBoxDetTime.Width = 198;
                textBoxDetTime.Height = 80;
                textBoxDetTime.Margin = new Thickness(5, 2, 5, 2);
                textBoxDetTime.FontSize = 13;
                textBoxDetTime.TextWrapping = TextWrapping.Wrap;
                textBoxDetTime.Text = "“" + item.ItemArray[1].ToString() + "”未能按" + item.ItemArray[9].ToString() + "的检测计划完成检测,请及时跟进处理!";
                textBoxDetTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                //检测项目显示    
                gridLabelName.Children.Add(labelName);
                //检测通道
                wrapPannelHole.Children.Add(labelSampleHole);
                wrapPannelHole.Children.Add(textBoxSampleHole);
                //检测方法
                wrapPannelMethod.Children.Add(labelMethod);
                wrapPannelMethod.Children.Add(textBoxMethod);
                //预热时间
                wrapPannelPreHeatTime.Children.Add(labelPreHeatTime);
                wrapPannelDetTime.Children.Add(textBoxDetTime);
                stackPanel.Children.Add(gridLabelName);
                stackPanel.Children.Add(wrapPannelHole);
                stackPanel.Children.Add(wrapPannelMethod);
                stackPanel.Children.Add(wrapPannelPreHeatTime);
                stackPanel.Children.Add(wrapPannelDetTime);
                border.Child = stackPanel;
            }
            catch (Exception ex)
            {
                FileUtils.Log("WarnTaskShow-GenerateItemBriefLayout:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            }
            return border;
        }

    }
}