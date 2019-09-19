using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            _Tc = _taskcheck.GetAsDataTable(string.Empty, string.Empty, 3);
            if (_Tc != null && _Tc.Rows.Count > 0)
                ShowAll(_Tc);
            //ShowAllItems(Global.userAccounts);
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginNameEdit window = new LoginNameEdit()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
                ShowAllItems(Global.userAccounts);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
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
                if (string.Empty.Equals(item.UserPassword))
                {
                    canEdit = true;
                }
                else
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.GetResult())
                    {
                        if (dialog.GetInput().Equals(item.UserPassword))
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
                    LoginNameEdit window = new LoginNameEdit()
                    {
                        _item = Global.userAccounts[_SelIndex],
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
                    ShowAllItems(Global.userAccounts);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
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
                if (string.Empty.Equals(item.UserPassword))
                {
                    canEdit = true;
                }
                else
                {
                    InputDialog dialog = new InputDialog("请输入密码");
                    dialog.ShowDialog();
                    if (dialog.GetResult())
                    {
                        if (dialog.GetInput().Equals(item.UserPassword))
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
                MessageBox.Show("异常:\n" + ex.Message);
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

        private void ShowAll(DataTable dt)
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

        private UIElement GenerateItemBriefLayout(DataRow item)
        {
            Border border = new Border()
            {
                Width = 210,
                Height = 270,
                Margin = new Thickness(10),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = _borderBrushNormal
            };
            border.MouseDown += Border_MouseDown;
            border.Background = Brushes.PaleVioletRed;
            border.Name = "border";
            /// stackpanel
            StackPanel stackPanel = new StackPanel()
            {
                Width = 200,
                Height = 300
            };
            stackPanel.SetResourceReference(StackPanel.StyleProperty, "stackPanelStyle1");
            stackPanel.Name = "stackPanel";
            ///新建模块生成方法
            /// //登陆人
            Grid gridLabelName = new Grid()
            {
                Width = 200,
                Height = 40
            };
            Label labelName = new Label()
            {
                FontSize = 13,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = "“" + item.ItemArray[1].ToString() + "”未完成!",
                Name = "labelName"
            };
            /// hole 分光光度检测模块
            WrapPanel wrapPannelHole = new WrapPanel()
            {
                Width = 200,
                Height = 30
            };
            Label labelSampleHole = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(5, 2, 5, 2),
                FontSize = 13,
                Content = "开始时间",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxSampleHole = new TextBox()
            {
                Width = 82,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 13,
                Text = item.ItemArray[2].ToString(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsEnabled = false
            };
            //胶体金检测模块
            WrapPanel wrapPannelMethod = new WrapPanel()
            {
                Width = 200,
                Height = 30
            };
            Label labelMethod = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(5, 2, 5, 2),
                FontSize = 13,
                Content = "结束时间",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            TextBox textBoxMethod = new TextBox()
            {
                Width = 82,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 13,
                Text = item.ItemArray[3].ToString(),
                IsEnabled = false,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            WrapPanel wrapPannelPreHeatTime = new WrapPanel()
            {
                Width = 200,
                Height = 30
            };
            Label labelPreHeatTime = new Label()
            {
                Width = 100,
                Height = 80,
                Margin = new Thickness(5, 2, 5, 2),
                FontSize = 13,
                Content = "信息内容"
            };
            //labelPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            WrapPanel wrapPannelDetTime = new WrapPanel()
            {
                Width = 200,
                Height = 90
            };
            TextBox textBoxDetTime = new TextBox()
            {
                Width = 198,
                Height = 80,
                Margin = new Thickness(5, 2, 5, 2),
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap,
                Text = "“" + item.ItemArray[1].ToString() + "”未能按" + item.ItemArray[9].ToString() + "的检测计划完成检测,请及时跟进处理!",
                VerticalContentAlignment = VerticalAlignment.Center
            };
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
            return border;
        }

    }
}