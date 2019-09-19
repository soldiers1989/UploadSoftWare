﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using AIO.xaml.Dialog;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// GszEditItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszEditItemWindow : Window
    {
        public string[] _ganHuaxue;
        public string[] _GSZItemNameType;
        public DYGSZItemPara _item = null;
        private bool _isUpdate = false;
        private bool _isClose = true;
        private string logType = "GszEditItemWindow-error";

        public GszEditItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TabControlGSZMethod.SelectedIndex = -1;
                GridHole.Children.Add(generateHoleUIElement("wrapPanelHole", "checkBoxHole", Global.deviceHole.SxtCount, 55));

                if (null != _item)
                {
                    // 已经有项目了，要提前显示这些项目。
                    TextBoxItemName.Text = _item.Name;
                    TextBoxPassword.Text = _item.Password;
                    TextBoxHintStr.Text = _item.HintStr;
                    TextBoxSampleNum.Text = "" + _item.SampleNum;
                    TextBoxUnit.Text = "" + _item.Unit;
                    List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                    for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                    {
                        listCheckBoxes[i].IsChecked = _item.Hole[i].Use;
                    }

                    for (int method = 0; method < 2; ++method)
                    {
                        if (0 == method)
                        {
                            TextBoxDXPlusT.Text = "" + _item.dx.PlusT;
                            TextBoxDXMinusT.Text = "" + _item.dx.MinusT;
                            ComboBoxDXRGBSelPlus.SelectedIndex = _item.dx.RGBSelPlus;
                            ComboBoxDXRGBSelMinus.SelectedIndex = _item.dx.RGBSelMinus;
                            ComboBoxDXComparePlus.SelectedIndex = _item.dx.ComparePlus;
                            ComboBoxDXCompareMinus.SelectedIndex = _item.dx.CompareMinus;
                        }
                        else if (1 == method)
                        {
                            ComboBoxDLRGBSel.SelectedIndex = _item.dl.RGBSel;
                            TextBoxDLA0.Text = "" + _item.dl.A0;
                            TextBoxDLA1.Text = "" + _item.dl.A1;
                            TextBoxDLA2.Text = "" + _item.dl.A2;
                            TextBoxDLA3.Text = "" + _item.dl.A3;
                            TextBoxDLB0.Text = "" + _item.dl.B0;
                            TextBoxDLB1.Text = "" + _item.dl.B1;
                            TextBoxDLLimitL.Text = "" + _item.dl.LimitL;
                            TextBoxDLLimitH.Text = "" + _item.dl.LimitH;
                        }
                    }
                    TabControlGSZMethod.SelectedIndex = ComboBoxMethod.SelectedIndex = _item.Method;
                }
                else
                {
                    ProjectName.Content = "新建项目";
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
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
            try
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
                str = TextBoxUnit.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【单位】!", "操作提示");
                    TextBoxUnit.Focus();
                    return false;
                }
                str = TextBoxSampleNum.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【样品编号】!", "操作提示");
                    TextBoxSampleNum.Focus();
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
                    MessageBox.Show("请至少选择一个【检测孔】！", "操作提示");
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
                return false;
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
                        _item = new DYGSZItemPara();
                        Global.gszItems.Add(_item);
                    }
                    if (sender == null && e == null)
                    {
                        _item.Name = _GSZItemNameType[0].ToString();//项目名称
                        _item.Unit = _GSZItemNameType[3].ToString();//项目检测单位
                        _item.HintStr = _GSZItemNameType[4].ToString();//简要提示:新添加实验--注释
                        _item.Password = _GSZItemNameType[5].ToString();//密码
                        Int32.TryParse(_GSZItemNameType[6].ToString(), out _item.SampleNum);//样品编号
                        //Int32.TryParse(JTJItemNameType[9].ToString(), out item.Wave);//波长

                        //检测方法选择
                        _item.Method = Convert.ToInt32(_GSZItemNameType[12].ToString());
                        _item.Hole[0].Use = Convert.ToBoolean(_GSZItemNameType[8].ToString());
                        _item.Hole[1].Use = Convert.ToBoolean(_GSZItemNameType[9].ToString());
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;

                        //item.InvalidC = Convert.ToInt32(JTJItemNameType[7].ToString());
                    }
                    else
                    {
                        _item.Name = TextBoxItemName.Text;
                        _item.Password = TextBoxPassword.Text;
                        _item.HintStr = TextBoxHintStr.Text;
                        Int32.TryParse(TextBoxSampleNum.Text, out _item.SampleNum);
                        _item.Unit = TextBoxUnit.Text;

                        List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                        for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                        {
                            _item.Hole[i].Use = (bool)listCheckBoxes[i].IsChecked;
                        }

                        _item.Method = ComboBoxMethod.SelectedIndex;
                    }
                    if (0 == _item.Method)
                    {
                        Double.TryParse(TextBoxDXPlusT.Text, out _item.dx.PlusT);
                        Double.TryParse(TextBoxDXMinusT.Text, out _item.dx.MinusT);
                        _item.dx.RGBSelPlus = ComboBoxDXRGBSelPlus.SelectedIndex;
                        _item.dx.RGBSelMinus = ComboBoxDXRGBSelMinus.SelectedIndex;
                        _item.dx.ComparePlus = ComboBoxDXComparePlus.SelectedIndex;
                        _item.dx.CompareMinus = ComboBoxDXCompareMinus.SelectedIndex;
                    }
                    else if (1 == _item.Method)
                    {
                        _item.dl.RGBSel = ComboBoxDLRGBSel.SelectedIndex;
                        Double.TryParse(TextBoxDLA0.Text, out _item.dl.A0);
                        Double.TryParse(TextBoxDLA1.Text, out _item.dl.A1);
                        Double.TryParse(TextBoxDLA2.Text, out _item.dl.A2);
                        Double.TryParse(TextBoxDLA3.Text, out _item.dl.A3);
                        Double.TryParse(TextBoxDLB0.Text, out _item.dl.B0);
                        Double.TryParse(TextBoxDLB1.Text, out _item.dl.B1);
                        Double.TryParse(TextBoxDLLimitL.Text, out _item.dl.LimitL);
                        Double.TryParse(TextBoxDLLimitH.Text, out _item.dl.LimitH);
                    }
                    Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
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
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControlGSZMethod.SelectedIndex = ComboBoxMethod.SelectedIndex;
            _isUpdate = true;
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

        private void TextBoxItemName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            SearchSample searchSample = new SearchSample();
            searchSample.ShowDialog();
            if (!Global.projectName.Equals(""))
                this.TextBoxItemName.Text = Global.projectName;
            Global.projectName = "";//还原项目名称的值
        }

        private void TextBoxItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
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

        private void ComboBoxDXRGBSelPlus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void ComboBoxDXComparePlus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxDXPlusT_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void ComboBoxDXRGBSelMinus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void ComboBoxDXCompareMinus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxDXMinusT_TextChanged(object sender, TextChangedEventArgs e)
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