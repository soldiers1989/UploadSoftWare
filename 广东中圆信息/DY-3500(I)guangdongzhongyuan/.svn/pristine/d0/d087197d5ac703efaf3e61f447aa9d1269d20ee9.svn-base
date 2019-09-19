using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// LoginDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDisplay : Window
    {
        private int _selIndex = -1;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private IDictionary<String, String> _strDic = null;
        private string logType = "LoginDisplay-error";

        public LoginDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllItems(Global.userAccounts);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginNameEdit window = new LoginNameEdit();
                window._type = "add";
                window._strDic = _strDic;
                window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                _strDic = null;
                ShowAllItems(Global.userAccounts);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_selIndex < 0)
            {
                MessageBox.Show("请选择一个用户！", "系统提示");
                return;
            }
            try
            {
                UserAccount item = Global.userAccounts[_selIndex];
                bool canEdit = false;
                if ("".Equals(item.UserPassword) || LoginWindow._userAccount.UserName.Equals("admin"))
                {
                    canEdit = true;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        InputDialog dialog = new InputDialog("请输入密码!");
                        dialog.ShowDialog();
                        if (dialog.getResult())
                        {
                            if (dialog.getInput().Equals(item.UserPassword))
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
                    LoginNameEdit window = new LoginNameEdit();
                    window._strDic = this._strDic;
                    window._item = Global.userAccounts[_selIndex];
                    window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                    this._strDic = null;
                    ShowAllItems(Global.userAccounts);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_selIndex < 0)
            {
                MessageBox.Show("请选择一个用户！", "系统提示");
                return;
            }
            try
            {
                UserAccount item = Global.userAccounts[_selIndex];
                bool canEdit = false;
                if (item.UserName.Equals("admin"))
                {
                    MessageBox.Show("不能删除此管理员用户！", "操作提示"); return;
                }
                else
                {
                    if (LoginWindow._userAccount.UserName.Equals("admin"))
                    {
                        if (MessageBox.Show("确定要删除此用户吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            canEdit = true;
                    }
                    else
                    {
                        if ("".Equals(item.UserPassword))
                            canEdit = true;
                        else
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                InputDialog dialog = new InputDialog("请输入密码！");
                                dialog.ShowDialog();
                                if (dialog.getResult())
                                {
                                    if (dialog.getInput().Equals(item.UserPassword))
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
                    }
                    if (canEdit)
                    {
                        _strDic = null;
                        Global.userAccounts.RemoveAt(_selIndex);
                        Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                        ShowAllItems(Global.userAccounts);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                if (null == borderList)
                    return;
                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (sender == borderList[i])
                    {
                        _selIndex = i;
                        borderList[i].BorderBrush = _borderBrushSelected;
                    }
                    else
                        borderList[i].BorderBrush = _borderBrushNormal;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ShowAllItems(List<UserAccount> items)
        {
            // 将userAccounts的内容项添加到主界面显示出来。
            try
            {
                WrapPanelItem.Children.Clear();
                while (RemoveAt(items) >= 0)
                {
                    Global.userAccounts.RemoveAt(RemoveAt(items));
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                }

                foreach (UserAccount item in items)
                {
                    UIElement element = GenerateItemBriefLayout(item);
                    WrapPanelItem.Children.Add(element);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            finally
            {
                _selIndex = -1;
            }
        }

        private int RemoveAt(List<UserAccount> items)
        {
            _selIndex = -1;
            try
            {
                _strDic = new Dictionary<String, String>();
                foreach (UserAccount item in items)
                {
                    _selIndex++;
                    if (!_strDic.ContainsKey(item.UserName))
                        _strDic.Add(item.UserName, item.UserName);
                    else
                        return _selIndex;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            return -1;
        }

        private UIElement GenerateItemBriefLayout(UserAccount item)
        {
            try
            {
                Border border = new Border();
                border.Width = 210;
                border.Margin = new Thickness(10);
                border.BorderThickness = new Thickness(5);
                border.CornerRadius = new CornerRadius(10);
                border.BorderBrush = _borderBrushNormal;
                border.MouseDown += Border_MouseDown;
                border.Background = Brushes.AliceBlue;
                border.Name = "border";
                /// stackpanel
                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 185;
                stackPanel.SetResourceReference(StackPanel.StyleProperty, "stackPanelStyle1");
                stackPanel.Name = "stackPanel";

                //新建模块生成方法
                //登陆人
                Grid gridLabelName = new Grid();
                gridLabelName.Width = 185;
                Label labelName = new Label();
                labelName.FontSize = 20;
                labelName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                labelName.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                labelName.Content = item.UserName;
                labelName.Name = "labelName";

                /// hole 分光光度检测模块
                WrapPanel wrapPannelHole = new WrapPanel();
                wrapPannelHole.Width = 185;
                Label labelSampleHole = new Label();
                labelSampleHole.Width = 100;
                labelSampleHole.Margin = new Thickness(0, 2, 0, 0);
                labelSampleHole.FontSize = 15;
                labelSampleHole.Content = "分光光度模块";
                labelSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                CheckBox textBoxSampleHole = new CheckBox();
                textBoxSampleHole.Width = 82;
                textBoxSampleHole.Margin = new Thickness(0, 2, 0, 2);
                textBoxSampleHole.FontSize = 15;
                textBoxSampleHole.IsChecked = item.UIFaceOne;
                textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textBoxSampleHole.IsEnabled = false;

                //胶体金检测模块
                WrapPanel wrapPannelMethod = null;
                Label labelMethod = null;
                CheckBox textBoxMethod = null;
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelMethod = new WrapPanel();
                    wrapPannelMethod.Width = 185;
                    labelMethod = new Label();
                    labelMethod.Width = 100;
                    labelMethod.Margin = new Thickness(0, 2, 0, 0);
                    labelMethod.FontSize = 15;
                    labelMethod.Content = "胶体金模块";
                    labelMethod.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    textBoxMethod = new CheckBox();
                    textBoxMethod.Width = 82;
                    textBoxMethod.Margin = new Thickness(0, 2, 0, 2);
                    textBoxMethod.FontSize = 15;
                    textBoxMethod.IsChecked = item.UIFaceTwo;
                    textBoxMethod.IsEnabled = false;
                    textBoxMethod.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                }

                //干化学检测模块
                WrapPanel wrapPannelPreHeatTime = null;
                Label labelPreHeatTime = null;
                CheckBox textBoxPreHeatTime = null;
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelPreHeatTime = new WrapPanel();
                    wrapPannelPreHeatTime.Width = 185;
                    labelPreHeatTime = new Label();
                    labelPreHeatTime.Width = 100;
                    labelPreHeatTime.Margin = new Thickness(0, 2, 0, 0);
                    labelPreHeatTime.FontSize = 15;
                    labelPreHeatTime.Content = "干化学模块";
                    labelPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    textBoxPreHeatTime = new CheckBox();
                    textBoxPreHeatTime.Width = 82;
                    textBoxPreHeatTime.Margin = new Thickness(0, 2, 0, 2);
                    textBoxPreHeatTime.FontSize = 15;
                    textBoxPreHeatTime.IsChecked = item.UIFaceThree;
                    textBoxPreHeatTime.IsEnabled = false;
                    textBoxPreHeatTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                }

                //重金属检测模块
                WrapPanel wrapPannelDetTime = null;
                Label labelDetTime = null;
                CheckBox textBoxDetTime = null;
                if (Global.deviceHole.HmCount == 1)
                {
                    wrapPannelDetTime = new WrapPanel();
                    wrapPannelDetTime.Width = 185;
                    labelDetTime = new Label();
                    labelDetTime.Width = 100;
                    labelDetTime.Margin = new Thickness(0, 2, 0, 0);
                    labelDetTime.FontSize = 15;
                    labelDetTime.Content = "重金属模块";
                    labelDetTime.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    textBoxDetTime = new CheckBox();
                    textBoxDetTime.Width = 82;
                    textBoxDetTime.Margin = new Thickness(0, 2, 0, 2);
                    textBoxDetTime.FontSize = 15;
                    textBoxDetTime.IsChecked = item.UIFaceFour;
                    textBoxDetTime.IsEnabled = false;
                    textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                }

                //微生物模块
                WrapPanel wrapPannelMicrobial = null;
                Label labelMicrobial = null;
                CheckBox textBoxMicrobial = null;
                if (Global.IsEnableWswOrAtp)
                {
                    wrapPannelMicrobial = new WrapPanel();
                    wrapPannelMicrobial.Width = 185;
                    labelMicrobial = new Label();
                    labelMicrobial.Width = 100;
                    labelMicrobial.Margin = new Thickness(0, 2, 0, 0);
                    labelMicrobial.FontSize = 15;
                    labelMicrobial.Content = Global.IsWswOrAtp.Equals("WSW") ? "微生物模块" : "ATP模块";
                    labelMicrobial.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    textBoxMicrobial = new CheckBox();
                    textBoxMicrobial.Width = 82;
                    textBoxMicrobial.Margin = new Thickness(0, 2, 0, 2);
                    textBoxMicrobial.FontSize = 15;
                    textBoxMicrobial.IsChecked = item.UIFaceFive;
                    textBoxMicrobial.IsEnabled = false;
                    textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                }

                //薄层色谱检测模块
                WrapPanel wrapPannelBcsp = null;
                Label labelBcsp = null;
                CheckBox textBoxBcsp = null;
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelBcsp = new WrapPanel()
                    {
                        Width = 185
                    };
                    labelBcsp = new Label()
                    {
                        Width = 100,
                        Margin = new Thickness(0, 2, 0, 0),
                        FontSize = 15,
                        Content = "薄层色谱模块",
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    textBoxBcsp = new CheckBox()
                    {
                        Width = 82,
                        Margin = new Thickness(0, 2, 0, 2),
                        FontSize = 15,
                        IsChecked = item.UIFaceBcsp,
                        IsEnabled = false,
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                    };
                }

                //荧光免疫检测模块
                WrapPanel wrapPannelYgmy = null;
                Label labelYgmy = null;
                CheckBox textBoxYgmy = null;
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelYgmy = new WrapPanel()
                    {
                        Width = 185
                    };
                    labelYgmy = new Label()
                    {
                        Width = 100,
                        Margin = new Thickness(0, 2, 0, 0),
                        FontSize = 15,
                        Content = "荧光免疫模块",
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    textBoxYgmy = new CheckBox()
                    {
                        Width = 82,
                        Margin = new Thickness(0, 2, 0, 2),
                        FontSize = 15,
                        IsChecked = item.UIFaceYgmy,
                        IsEnabled = false,
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                    };
                }

                //食源性微生物检测模块
                WrapPanel wrapPannelSyxwsw = null;
                Label labelSyxwsw = null;
                CheckBox textBoxSyxwsw = null;
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelSyxwsw = new WrapPanel()
                    {
                        Width = 185,
                        Height = 30
                    };
                    labelSyxwsw = new Label()
                    {
                        Width = 100,
                        Margin = new Thickness(0, 2, 0, 0),
                        FontSize = 15,
                        Content = "食源性微生物模块",
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    textBoxSyxwsw = new CheckBox()
                    {
                        Width = 82,
                        Margin = new Thickness(0, 2, 0, 2),
                        FontSize = 15,
                        IsChecked = item.UIFaceSyxwsw,
                        IsEnabled = false,
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                    };
                }

                //是否强制上传
                WrapPanel wrapPannelSampleName = new WrapPanel();
                wrapPannelSampleName.Width = 185;
                Label labelSampleName = new Label();
                labelSampleName.Width = 100;
                labelSampleName.Margin = new Thickness(0, 2, 0, 0);
                labelSampleName.FontSize = 15;
                labelSampleName.Content = "是否强制上传";
                labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                CheckBox textBoxSampleName = new CheckBox();
                textBoxSampleName.Width = 82;
                textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
                textBoxSampleName.FontSize = 15;
                textBoxSampleName.IsChecked = item.UpDateNowing;
                textBoxSampleName.IsEnabled = false;
                textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textBoxSampleName.Name = "UpData";

                //Create是否添加管理权限
                WrapPanel wrapPannelTask = new WrapPanel();
                wrapPannelTask.Width = 185;
                Label labelTaskName = new Label();
                labelTaskName.Width = 100;
                labelTaskName.Margin = new Thickness(0, 2, 0, 0);
                labelTaskName.FontSize = 15;
                labelTaskName.Content = "管理权限";
                labelTaskName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                CheckBox textBoxTaskName = new CheckBox();
                textBoxTaskName.Width = 82;
                textBoxTaskName.Margin = new Thickness(0, 2, 0, 2);
                textBoxTaskName.FontSize = 15;
                textBoxTaskName.IsChecked = item.Create;
                textBoxTaskName.IsEnabled = false;
                textBoxTaskName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                textBoxTaskName.Name = "TaskName";

                gridLabelName.Children.Add(labelName);
                wrapPannelHole.Children.Add(labelSampleHole);
                wrapPannelHole.Children.Add(textBoxSampleHole);
                //胶体金
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelMethod.Children.Add(labelMethod);
                    wrapPannelMethod.Children.Add(textBoxMethod);
                }
                //干化学
                if (Global.deviceHole.SxtCount != 0)
                {
                    wrapPannelPreHeatTime.Children.Add(labelPreHeatTime);
                    wrapPannelPreHeatTime.Children.Add(textBoxPreHeatTime);
                }
                //重金属
                if (Global.deviceHole.HmCount == 1)
                {
                    wrapPannelDetTime.Children.Add(labelDetTime);
                    wrapPannelDetTime.Children.Add(textBoxDetTime);
                }
                //微生物
                if (Global.IsEnableWswOrAtp)
                {
                    wrapPannelMicrobial.Children.Add(labelMicrobial);
                    wrapPannelMicrobial.Children.Add(textBoxMicrobial);
                }
                //薄层色谱
                if (Global.IsEnableBcsp)
                {
                    wrapPannelBcsp.Children.Add(labelBcsp);
                    wrapPannelBcsp.Children.Add(textBoxBcsp);
                }
                //荧光免疫
                if (Global.IsEnableYgmy)
                {
                    wrapPannelYgmy.Children.Add(labelYgmy);
                    wrapPannelYgmy.Children.Add(textBoxYgmy);
                }
                //食源性微生物
                if (Global.IsEnableSyxwsw)
                {
                    wrapPannelSyxwsw.Children.Add(labelSyxwsw);
                    wrapPannelSyxwsw.Children.Add(textBoxSyxwsw);
                }
                wrapPannelSampleName.Children.Add(labelSampleName);
                wrapPannelSampleName.Children.Add(textBoxSampleName);
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
                stackPanel.Children.Add(gridLabelName);
                stackPanel.Children.Add(wrapPannelHole);
                if (Global.deviceHole.SxtCount != 0)
                    stackPanel.Children.Add(wrapPannelMethod);
                if (Global.deviceHole.SxtCount != 0)
                    stackPanel.Children.Add(wrapPannelPreHeatTime);
                if (Global.deviceHole.HmCount == 1)
                    stackPanel.Children.Add(wrapPannelDetTime);
                if (Global.IsEnableWswOrAtp)
                    stackPanel.Children.Add(wrapPannelMicrobial);
                if (Global.IsEnableBcsp)
                    stackPanel.Children.Add(wrapPannelBcsp);
                if (Global.IsEnableYgmy)
                    stackPanel.Children.Add(wrapPannelYgmy);
                if (Global.IsEnableSyxwsw)
                    stackPanel.Children.Add(wrapPannelSyxwsw);
                stackPanel.Children.Add(wrapPannelSampleName);
                stackPanel.Children.Add(wrapPannelTask);
                border.Child = stackPanel;
                return border;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
        }

    }
}