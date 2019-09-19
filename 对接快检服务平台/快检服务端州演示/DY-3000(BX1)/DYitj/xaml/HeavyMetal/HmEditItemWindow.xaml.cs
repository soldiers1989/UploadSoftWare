using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// HmEditItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HmEditItemWindow : Window
    {
        public string[] _HmMetailArry;
        public DYHMItemPara _item = null;
        private bool _isUpdate = true;
        private bool _isClose = true;

        public HmEditItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GridHole.Children.Add(generateHoleUIElement("wrapPanelHole", "checkBoxHole", Global.deviceHole.HmCount, 55));
                if (null != _item)
                {
                    // 已经有项目了，要提前显示这些项目。
                    ComboBoxItemName.Text = _item.Name;
                    TextBoxPassword.Text = _item.Password;
                    TextBoxSampleNum.Text = "" + _item.SampleNum;
                    TextBoxHintStr.Text = _item.HintStr;
                    TextBoxUnit.Text = _item.Unit;
                    TextBoxItemID.Text = "" + _item.ItemID;

                    List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                    for (int i = 0; i < Global.deviceHole.HmCount; ++i)
                    {
                        listCheckBoxes[i].IsChecked = _item.Hole[i].Use;
                    }
                    TextBoxDilutionRatio.Text = "" + _item.DilutionRatio;
                    TextBoxConcentration.Text = "" + _item.Concentration;
                    TextBoxLiquidVolume.Text = "" + _item.LiquidVolume;
                    TextBoxRequirements.Text = "" + _item.Requirements;
                    TextBoxA0.Text = "" + _item.a0;
                    TextBoxA1.Text = "" + _item.a1;
                    TextBoxA2.Text = "" + _item.a2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\r\b异常信息：" + ex.Message, "系统提示");
            }
            finally { _isUpdate = false; }
        }


        /// <summary>
        /// 验证信息完整性
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            String str = String.Empty;
            str = ComboBoxItemName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【项目名称】!", "操作提示");
                ComboBoxItemName.Focus();
                return false;
            }
            str = TextBoxUnit.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【单位】!", "操作提示");
                TextBoxUnit.Focus();
                return false;
            }
            str = TextBoxPassword.Text.Trim();
            if (str.Length == 0)
            {
                if (MessageBox.Show("[未设置密码] 确定保存无密码的检测项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    this.TextBoxPassword.Focus();
                    return false;
                }
            }
            return true;
        }

        public void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            // 将数据保存到文件
            try
            {
                if (check())
                {
                    if (null == _item)
                    {// 这属于新添加
                        _item = new DYHMItemPara();
                        Global.hmItems.Add(_item);
                    }
                    if (sender == null && e == null)
                    {
                        _item.Name = _HmMetailArry[0].ToString();//项目名称
                        _item.Unit = _HmMetailArry[3].ToString();//项目检测单位
                        _item.HintStr = _HmMetailArry[4].ToString();//简要操作：新添加实验--注释
                        _item.Password = _HmMetailArry[5].ToString();//密码
                        Int32.TryParse(_HmMetailArry[6].ToString(), out _item.SampleNum);//样品编号
                        //检测方法选择
                        _item.Method = Convert.ToInt32(_HmMetailArry[12].ToString());
                        _item.Hole[0].Use = Convert.ToBoolean(_HmMetailArry[8].ToString());
                        _item.Hole[1].Use = Convert.ToBoolean(_HmMetailArry[9].ToString());
                    }
                    else
                    {
                        _item.Name = ComboBoxItemName.Text;
                        _item.Password = TextBoxPassword.Text;
                        _item.HintStr = TextBoxHintStr.Text;
                        Int32.TryParse(TextBoxSampleNum.Text, out _item.SampleNum);
                        _item.Unit = TextBoxUnit.Text;
                        List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                        for (int i = 0; i < Global.deviceHole.HmCount; ++i)
                        {
                            _item.Hole[i].Use = (bool)listCheckBoxes[i].IsChecked;
                        }
                    }
                    try
                    {
                        Int32.TryParse(TextBoxDilutionRatio.Text, out _item.DilutionRatio);
                        Int32.TryParse(TextBoxConcentration.Text, out _item.Concentration);
                        Int32.TryParse(TextBoxLiquidVolume.Text, out _item.LiquidVolume);
                        Int32.TryParse(TextBoxRequirements.Text, out _item.Requirements);
                        Int32.TryParse(TextBoxA0.Text, out _item.a0);
                        Int32.TryParse(TextBoxA1.Text, out _item.a1);
                        Int32.TryParse(TextBoxA2.Text, out _item.a2);
                        Int32.TryParse(TextBoxItemID.Text, out _item.ItemID);
                    }
                    catch (Exception ex)
                    {
                        FileUtils.Log(ex.ToString());
                        return;
                    }
                    Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
                    _isClose = true;
                    _isUpdate = false;
                    try { this.Close(); }
                    catch (Exception) { }
                }
                else
                {
                    _isClose = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception) { }
        }

        private UIElement generateHoleUIElement(string wrapPanelName, string checkBoxName, int checkBoxCount, int checkBoxWidth)
        {
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Name = wrapPanelName;
            for (int i = 0; i < checkBoxCount; ++i)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Width = checkBoxWidth;
                checkBox.Name = checkBoxName;
                checkBox.Content = "" + (i + 1);
                wrapPanel.Children.Add(checkBox);
            }
            return wrapPanel;
        }

        private void ComboBoxItemName_DropDownClosed(object sender, EventArgs e)
        {
            TextBoxItemID.Text = "" + (ComboBoxItemName.SelectedIndex + 1);
        }

        private void ComboBoxItemName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxItemID_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxSampleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxConcentration_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxLiquidVolume_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxDilutionRatio_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxRequirements_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxA0_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxA1_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxA2_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxHintStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isUpdate)
            {
                if (MessageBox.Show("是否保存当前内容?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ButtonNext_Click(null, new RoutedEventArgs());
                }
                else
                {
                    _isClose = true;
                }
            }
            else
            {
                _isClose = true;
            }

            if (!_isClose)
            {
                e.Cancel = true;
            }
        }

    }
}
