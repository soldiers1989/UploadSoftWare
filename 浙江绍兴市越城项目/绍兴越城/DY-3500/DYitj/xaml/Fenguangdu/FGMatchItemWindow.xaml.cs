using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIO.xaml.Fenguangdu
{
    /// <summary>
    /// FGMatchItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FGMatchItemWindow : Window
    {
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private clsCompanyOpr ttResult = new clsCompanyOpr();


        public FGMatchItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllItems(Global.fgdItems);
        }
        private void ShowAllItems(List<DYFGDItemPara> items)
        {
            // 将fgdItems的内容项添加到主界面显示出来。
            try
            {
                WrapPanelItem.Children.Clear();

                foreach (DYFGDItemPara item in items)
                {
                    UIElement element = GenerateItemBriefLayout(item, string.Empty, false);
                    WrapPanelItem.Children.Add(element);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ShowAllItems):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public UIElement GenerateItemBriefLayout(DYFGDItemPara item, string name, bool IsSelected)
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
            border.MouseDown += border_MouseDown;

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
            gridLabelName.Children.Add(labelName);

            //匹配项目
            WrapPanel wrapPannelItem = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Item = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItem.Children.Add(labelX_Item);
            Label labelItem = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "匹配项目",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItem.Children.Add(labelItem);
            TextBox textBoxItem = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = item.Mitem == null ? string.Empty : item.Mitem.Trim(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "itemName",
                ToolTip = "双击选择项目",
                IsReadOnly = true,
            };
            textBoxItem.MouseLeftButtonDown += textBoxItem_MouseLeftButtonDown;
            textBoxItem.MouseDoubleClick += textBoxItem_MouseDoubleClick;
            wrapPannelItem.Children.Add(textBoxItem);
            //匹配项目编号
            WrapPanel wrapPannelItemCode = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_ItemCode = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItemCode.Children.Add(labelX_ItemCode);
            Label labelItemCode = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "项目编号",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItemCode.Children.Add(labelItemCode);
            TextBox textBoxItemCode = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = item.MCode == null? string.Empty : item.MCode.Trim(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "itemCode",
                IsReadOnly = true,
            };
            wrapPannelItemCode.Children.Add(textBoxItemCode);
            //项目类型
            WrapPanel wrapPannelItemType = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_ItemType = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItemType.Children.Add(labelX_ItemType);
            Label labelItemType = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "项目类型",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelItemType.Children.Add(labelItemType);
            TextBox textBoxItemType = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = item.MType == null ? string.Empty : item.MType.Trim(),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "itemType",
                IsReadOnly = true,
            };

            wrapPannelItemType.Children.Add(textBoxItemType);

            stackPanel.Children.Add(gridLabelName);
            stackPanel.Children.Add(wrapPannelItem);
            stackPanel.Children.Add(wrapPannelItemCode);
            stackPanel.Children.Add(wrapPannelItemType);

            border.Child = stackPanel;
            return border;
        }

        private  void textBoxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "itemName");
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "border");
        }

        private void textBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemWindow window = new ItemWindow();
            window.ShowDialog();
            ShowBorder(sender, e, "itemName");
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                List<TextBox> tbItemList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemName");
                List<TextBox> tbCodeList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemCode");
                List<TextBox> tbTypeList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemType");

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
                        if (borderList[i] == sender)
                        {
                            borderList[i].BorderBrush = _borderBrushSelected;
                              //浙江查询当前选中检测项目是否在平台上有
                            string CurrentItem = Global.fgdItems[i].Name;
                           
                            DataTable itemname = ttResult.GetYCItemTable(" itemName='" + CurrentItem + "'");
                          
                            if (itemname != null && itemname.Rows.Count > 0)
                            {
                                tbItemList[i].Text = itemname.Rows[0]["itemName"].ToString();
                                tbCodeList[i].Text= itemname.Rows[0]["itemCode"].ToString();
                                tbTypeList[i].Text = itemname.Rows[0]["itemType"].ToString();
                            }
                            else
                            {
                                Global.projectName = "";
                                ItemWindow window = new ItemWindow();//弹框
                                window.ShowDialog();
                                if (Global.itemName != "")
                                {
                                    tbItemList[i].Text = Global.itemName;
                                    tbCodeList[i].Text = Global.itemCode;
                                    tbTypeList[i].Text = Global.itemType;
                                    Global.itemName = "";
                                }
                            }

                            break;
                        }
                    }
                    else if (type.Equals("itemName"))
                    {
                        if (tbItemList[i] == sender)
                        {
                            borderList[i].BorderBrush = _borderBrushSelected;
                            if(Global.itemName !="")
                            {
                                tbItemList[i].Text = Global.itemName;
                                tbCodeList[i].Text = Global.itemCode;
                                tbTypeList[i].Text = Global.itemType;
                                Global.itemName = "";
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ShowBorder):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelItem, "border");
                List<TextBox> tbItemList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemName");
                List<TextBox> tbCodeList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemCode");
                List<TextBox> tbTypeList = UIUtils.GetChildObjects<TextBox>(WrapPanelItem, "itemType");
                if (null == borderList) return;

                for (int i = 0; i < borderList.Count;i++ )
                {
                    Global.fgdItems[i].Mitem = tbItemList[i].Text.Trim();
                    Global.fgdItems[i].MCode = tbCodeList[i].Text.Trim();
                    Global.fgdItems[i].MType = tbTypeList[i].Text.Trim();
                }
                Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                MessageBox.Show("保存成功！","操作提示");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }
        //退出
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
