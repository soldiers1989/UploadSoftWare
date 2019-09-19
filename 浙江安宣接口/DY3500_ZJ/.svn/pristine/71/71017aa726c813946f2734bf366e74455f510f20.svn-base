using AIO.xaml.Dialog;
using com.lvrenyang;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO
{
    /// <summary>
    /// JtjEditItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjEditItemWindow : Window
    {
        public string[] _JTJItemNameType;
        public DYJTJItemPara _item = null;
        private bool _isUpdate = false, _isClose = true;

        public JtjEditItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TabControlJTJMethod.SelectedIndex = -1;
                GridHole.Children.Add(GenerateHoleUIElement("wrapPanelHole", "checkBoxHole", Global.deviceHole.SxtCount, 55));
                if (null != _item)
                {
                    // 已经有项目了，要提前显示这些项目。
                    TextBoxItemName.Text = _item.Name;
                    TextBoxPassword.Text = _item.Password;
                    TextBoxSampleNum.Text = string.Empty + _item.SampleNum;
                    TextBoxHintStr.Text = _item.HintStr;
                    TextBoxInvalidC.Text = string.Empty + _item.InvalidC;
                    TextBoxUnit.Text = _item.Unit;
                    List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                    for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                    {
                        listCheckBoxes[i].IsChecked = _item.Hole[i].Use;
                    }
                    for (int method = 0; method < 4; ++method)
                    {
                        if (0 == method)
                        {
                            TextBoxDXXXPlusT.Text = string.Empty + _item.dxxx.PlusT;
                            TextBoxDXXXMinusT.Text = string.Empty + _item.dxxx.MinusT;
                        }
                        else if (1 == method)
                        {

                        }
                        else if (2 == method)
                        {
                            TextBoxDLTA0.Text = string.Empty + _item.dlt.A0;
                            TextBoxDLTA1.Text = string.Empty + _item.dlt.A1;
                            TextBoxDLTA2.Text = string.Empty + _item.dlt.A2;
                            TextBoxDLTA3.Text = string.Empty + _item.dlt.A3;
                            TextBoxDLTB0.Text = string.Empty + _item.dlt.B0;
                            TextBoxDLTB1.Text = string.Empty + _item.dlt.B1;
                            TextBoxDLTPPB.Text = string.Empty + _item.dlt.Limit;
                        }
                        else if (3 == method)
                        {
                            TextBoxDLTCA0.Text = string.Empty + _item.dltc.A0;
                            TextBoxDLTCA1.Text = string.Empty + _item.dltc.A1;
                            TextBoxDLTCA2.Text = string.Empty + _item.dltc.A2;
                            TextBoxDLTCA3.Text = string.Empty + _item.dltc.A3;
                            TextBoxDLTCB0.Text = string.Empty + _item.dltc.B0;
                            TextBoxDLTCB1.Text = string.Empty + _item.dltc.B1;
                            TextBoxDLTCPPB.Text = string.Empty + _item.dltc.Limit;
                        }
                    }
                    TabControlJTJMethod.SelectedIndex = ComboBoxMethod.SelectedIndex = _item.Method;
                }
                else
                {
                    ProjectName.Content = "新建项目";
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
        private bool Check()
        {
            String str = String.Empty;
            str = TextBoxItemName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【项目名称】!", "操作提示");
                TextBoxItemName.Focus();
                return false;
            }
            if (ComboBoxMethod.SelectedIndex < 0)
            {
                MessageBox.Show("请设置【检测方法】!", "操作提示");
                ComboBoxMethod.Focus();
                return false;
            }
            str = TextBoxSampleNum.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【样品编号】!", "操作提示");
                TextBoxSampleNum.Focus();
                return false;
            }
            str = TextBoxInvalidC.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【C值】最小值!", "操作提示");
                TextBoxInvalidC.Focus();
                return false;
            }
            str = TextBoxUnit.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请设置【检测单位】!", "操作提示");
                TextBoxUnit.Focus();
                return false;
            }
            List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
            for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
            {
                if ((bool)listCheckBoxes[i].IsChecked)
                {
                    str = "true";
                    break;
                }
                str = String.Empty;
            }
            if (str.Length == 0)
            {
                MessageBox.Show("请至少选择一个【检测孔】!", "操作提示");
                return false;
            }
            str = TextBoxPassword.Text.Trim();
            if (str.Length == 0)
            {
                if (MessageBox.Show("【未设置密码】 确定保存无密码的检测项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
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
                if (Check())
                {
                    if (null == _item)
                    {
                        // 这属于新添加
                        _item = new DYJTJItemPara();
                        Global.jtjItems.Add(_item);
                    }
                    if (sender == null && e == null)
                    {
                        _item.Name = _JTJItemNameType[0].ToString();//项目名称
                        _item.Unit = _JTJItemNameType[3].ToString();//项目检测单位
                        _item.HintStr = _JTJItemNameType[4].ToString();//简要操作：新添加实验--注释
                        _item.Password = _JTJItemNameType[5].ToString();//密码
                        Int32.TryParse(_JTJItemNameType[6].ToString(), out _item.SampleNum);//样品编号
                        //Int32.TryParse(JTJItemNameType[9].ToString(), out item.Wave);//波长
                        //检测方法选择
                        _item.Method = Convert.ToInt32(_JTJItemNameType[12].ToString());
                        _item.Hole[0].Use = Convert.ToBoolean(_JTJItemNameType[8].ToString());
                        _item.Hole[1].Use = Convert.ToBoolean(_JTJItemNameType[9].ToString());
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;
                        _item.InvalidC = Convert.ToInt32(_JTJItemNameType[7].ToString());
                    }
                    else
                    {
                        _item.Name = TextBoxItemName.Text;
                        _item.Password = TextBoxPassword.Text;
                        _item.HintStr = TextBoxHintStr.Text;
                        Int32.TryParse(TextBoxSampleNum.Text, out _item.SampleNum);
                        _item.Unit = TextBoxUnit.Text;
                        _item.InvalidC = Convert.ToInt32(TextBoxInvalidC.Text);
                        List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                        for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                        {
                            _item.Hole[i].Use = (bool)listCheckBoxes[i].IsChecked;
                        }
                        _item.Method = ComboBoxMethod.SelectedIndex;
                    }
                    if (0 == _item.Method)
                    {
                        Double.TryParse(TextBoxDXXXPlusT.Text, out _item.dxxx.PlusT);
                        Double.TryParse(TextBoxDXXXMinusT.Text, out _item.dxxx.MinusT);
                    }
                    else if (1 == _item.Method)
                    {

                    }
                    else if (2 == _item.Method)
                    {
                        Double.TryParse(TextBoxDLTA0.Text, out _item.dlt.A0);
                        Double.TryParse(TextBoxDLTA1.Text, out _item.dlt.A1);
                        Double.TryParse(TextBoxDLTA2.Text, out _item.dlt.A2);
                        Double.TryParse(TextBoxDLTA3.Text, out _item.dlt.A3);
                        Double.TryParse(TextBoxDLTB0.Text, out _item.dlt.B0);
                        Double.TryParse(TextBoxDLTB1.Text, out _item.dlt.B1);
                        Double.TryParse(TextBoxDLTPPB.Text, out _item.dlt.Limit);
                    }
                    else if (3 == _item.Method)
                    {
                        Double.TryParse(TextBoxDLTCA0.Text, out _item.dltc.A0);
                        Double.TryParse(TextBoxDLTCA1.Text, out _item.dltc.A1);
                        Double.TryParse(TextBoxDLTCA2.Text, out _item.dltc.A2);
                        Double.TryParse(TextBoxDLTCA3.Text, out _item.dltc.A3);
                        Double.TryParse(TextBoxDLTCB0.Text, out _item.dltc.B0);
                        Double.TryParse(TextBoxDLTCB1.Text, out _item.dltc.B1);
                        Double.TryParse(TextBoxDLTCPPB.Text, out _item.dltc.Limit);
                    }
                    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                    if (sender != null && e != null)
                    {
                        _isClose = true;
                        _isUpdate = false;
                        try { this.Close(); }
                        catch (Exception) { }
                    }
                }
                else
                {
                    _isClose = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!\n错误信息：" + ex.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception) { }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
            TabControlJTJMethod.SelectedIndex = ComboBoxMethod.SelectedIndex;
        }

        private UIElement GenerateHoleUIElement(string wrapPanelName, string checkBoxName, int checkBoxCount, int checkBoxWidth)
        {
            WrapPanel wrapPanel = new WrapPanel()
            {
                Name = wrapPanelName
            };
            for (int i = 0; i < checkBoxCount; ++i)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Width = checkBoxWidth,
                    Name = checkBoxName,
                    Content = string.Empty + (i + 1)
                };
                wrapPanel.Children.Add(checkBox);
            }
            return wrapPanel;
        }

        private void TextBoxItemName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            SearchSample searchSample = new SearchSample();
            searchSample.ShowDialog();
            if (!Global.projectName.Equals(string.Empty))
                this.TextBoxItemName.Text = Global.projectName;
            Global.projectName = string.Empty;//还原项目名称的值
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

        private void TextBoxItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxInvalidC_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxSampleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxHintStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

    }
}
