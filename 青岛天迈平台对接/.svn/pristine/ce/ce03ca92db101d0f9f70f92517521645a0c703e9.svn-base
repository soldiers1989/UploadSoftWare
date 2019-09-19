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

        private int _SelIndex = -1;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private IDictionary<String, String> _strDic = null;

        public LoginDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllItems(Global.userAccounts);
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            LoginNameEdit window = new LoginNameEdit()
            {
                _strDic = _strDic,
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
            _strDic = null;
            ShowAllItems(Global.userAccounts);
        }

        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请选择一个用户！", "系统提示");
                return;
            }
            UserAccount item = Global.userAccounts[_SelIndex];
            bool canEdit = false;
            if (string.Empty.Equals(item.UserPassword) || LoginWindow._userAccount.UserName.Equals("admin"))
            {
                canEdit = true;
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    InputDialog dialog = new InputDialog("请输入密码!");
                    dialog.ShowDialog();
                    if (dialog.GetResult())
                    {
                        if (dialog.GetInput().Equals(item.UserPassword))
                        {
                            canEdit = true;
                            break;
                        }
                        else
                        {
                            canEdit = false;
                            MessageBox.Show("密码错误!", "系统提示");
                            i--;
                        }
                    }
                }
            }
            if (canEdit)
            {
                LoginNameEdit window = new LoginNameEdit()
                {
                    _strDic = this._strDic,
                    _item = Global.userAccounts[_SelIndex],
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
                this._strDic = null;
                ShowAllItems(Global.userAccounts);
            }
        }

        private void ButtonDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (_SelIndex < 0)
            {
                MessageBox.Show("请选择一个用户！", "系统提示");
                return;
            }
            UserAccount item = Global.userAccounts[_SelIndex];
            bool canEdit = false;
            if (item.UserName.Equals("admin"))
            {
                MessageBox.Show("不能删除此管理员用户!", "操作提示"); return;
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
                    if (string.Empty.Equals(item.UserPassword))
                        canEdit = true;
                    else
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            InputDialog dialog = new InputDialog("请输入密码!");
                            dialog.ShowDialog();
                            if (dialog.GetResult())
                            {
                                if (dialog.GetInput().Equals(item.UserPassword))
                                {
                                    canEdit = true;
                                    break;
                                }
                                else
                                {
                                    canEdit = false;
                                    MessageBox.Show("密码错误!", "系统提示");
                                    i--;
                                }
                            }
                        }
                    }
                }
                if (canEdit)
                {
                    _strDic = null;
                    Global.userAccounts.RemoveAt(_SelIndex);
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                    ShowAllItems(Global.userAccounts);
                }
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
            if (null == borderList)
                return;
            for (int i = 0; i < borderList.Count; ++i)
            {
                if (sender == borderList[i])
                {
                    _SelIndex = i;
                    borderList[i].BorderBrush = _borderBrushSelected;
                }
                else
                    borderList[i].BorderBrush = _borderBrushNormal;
            }
        }

        private int RemoveAt(List<UserAccount> items)
        {
            _SelIndex = -1;
            _strDic = new Dictionary<String, String>();
            foreach (UserAccount item in items)
            {
                _SelIndex++;
                if (!_strDic.ContainsKey(item.UserName))
                    _strDic.Add(item.UserName, item.UserName);
                else
                    return _SelIndex;
            }
            return -1;
        }

        private void ShowAllItems(List<UserAccount> items)
        {
            // 将userAccounts的内容项添加到主界面显示出来。
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
            _SelIndex = -1;
        }

        private UIElement GenerateItemBriefLayout(UserAccount item)
        {
            Border border = new Border()
            {
                Width = 210,
                Margin = new Thickness(10),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = _borderBrushNormal
            };
            border.MouseDown += Border_MouseDown;
            border.Background = Brushes.AliceBlue;
            border.Name = "border";
            /// stackpanel
            StackPanel stackPanel = new StackPanel()
            {
                Width = 185
            };
            stackPanel.SetResourceReference(StackPanel.StyleProperty, "stackPanelStyle1");
            stackPanel.Name = "stackPanel";

            //新建模块生成方法
            //登陆人
            Grid gridLabelName = new Grid()
            {
                Width = 185,
                Height = 40
            };
            Label labelName = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = item.UserName,
                Name = "labelName"
            };

            /// hole 分光光度检测模块
            WrapPanel wrapPannelHole = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelSampleHole = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "分光光度模块",
                VerticalContentAlignment = VerticalAlignment.Center
            };
            CheckBox textBoxSampleHole = new CheckBox()
            {
                Width = 82,
                Height = 14,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                IsChecked = item.UIFaceOne,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                IsEnabled = false
            };

            //胶体金检测模块
            WrapPanel wrapPannelMethod = null;
            Label labelMethod = null;
            CheckBox textBoxMethod = null;
            if (Global.deviceHole.SxtCount != 0)
            {
                wrapPannelMethod = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelMethod = new Label()
                {
                    Width = 100,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "胶体金模块",
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxMethod = new CheckBox()
                {
                    Width = 82,
                    Height = 14,
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 15,
                    IsChecked = item.UIFaceTwo,
                    IsEnabled = false,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
            }

            //干化学检测模块
            WrapPanel wrapPannelPreHeatTime = null;
            Label labelPreHeatTime = null;
            CheckBox textBoxPreHeatTime = null;
            if (Global.deviceHole.SxtCount != 0)
            {
                wrapPannelPreHeatTime = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelPreHeatTime = new Label()
                {
                    Width = 100,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "干化学模块",
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textBoxPreHeatTime = new CheckBox()
                {
                    Width = 82,
                    Height = 14,
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 15,
                    IsChecked = item.UIFaceThree,
                    IsEnabled = false,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
            }

            //重金属检测模块
            WrapPanel wrapPannelDetTime = null;
            Label labelDetTime = null;
            CheckBox textBoxDetTime = null;
            if (Global.deviceHole.HmCount == 1)
            {
                wrapPannelDetTime = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelDetTime = new Label()
                {
                    Width = 100,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "重金属模块",
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textBoxDetTime = new CheckBox()
                {
                    Width = 82,
                    Height = 14,
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 15,
                    IsChecked = item.UIFaceFour,
                    IsEnabled = false
                };
                textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            }

            //微生物模块
            WrapPanel wrapPannelMicrobial = null;
            Label labelMicrobial = null;
            CheckBox textBoxMicrobial = null;
            if (Global.IsEnableWswOrAtp)
            {
                wrapPannelMicrobial = new WrapPanel()
                {
                    Width = 185,
                    Height = 30
                };
                labelMicrobial = new Label()
                {
                    Width = 100,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = Global.IsWswOrAtp.Equals("WSW") ? "微生物模块" : "ATP模块",
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                textBoxMicrobial = new CheckBox()
                {
                    Width = 82,
                    Height = 14,
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 15,
                    IsChecked = item.UIFaceFive,
                    IsEnabled = false
                };
                textBoxSampleHole.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            }
            //是否强制上传
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelSampleName = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "是否强制上传",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            CheckBox textBoxSampleName = new CheckBox()
            {
                Width = 82,
                Height = 14,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                IsChecked = item.UpDateNowing,
                IsEnabled = false,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "UpData"
            };

            //是否验证快检单号
            WrapPanel wrapPannelkjdh = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelkjdh = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "验证快检单号",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            CheckBox textBoxkjdh = new CheckBox()
            {
                Width = 82,
                Height = 14,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                IsChecked = item.CheckSampleID,
                IsEnabled = false,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "yzkjdh"
            };

            //Create是否添加管理权限
            WrapPanel wrapPannelTask = new WrapPanel()
            {
                Width = 185,
                Height = 30
            };
            Label labelTaskName = new Label()
            {
                Width = 100,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "管理权限",
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            CheckBox textBoxTaskName = new CheckBox()
            {
                Width = 82,
                Height = 14,
                Margin = new Thickness(0, 2, 0, 2),
                FontSize = 15,
                IsChecked = item.Create,
                IsEnabled = false,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "TaskName"
            };
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
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            wrapPannelkjdh.Children.Add(labelkjdh);
            wrapPannelkjdh.Children.Add(textBoxkjdh);
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
            stackPanel.Children.Add(wrapPannelSampleName);
            if (Global.InterfaceType.Equals("ZH"))
                stackPanel.Children.Add(wrapPannelkjdh);
            stackPanel.Children.Add(wrapPannelTask);
            border.Child = stackPanel;
            return border;
        }

    }
}