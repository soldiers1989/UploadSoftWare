using System;
using System.Windows;
using System.Windows.Controls;
using AIO.xaml.Dialog;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// FgdEditItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdEditItemWindow : Window
    {
        public DYFGDItemPara _item = null;
        public string[] _FGDItemNameType;
        private static int[] _IndexToMethod = { 0, 1, 3, 4 };
        private static int[] _MethodToIndex = { 0, 1, -1, 2, 3 };
        private bool _isUpdate = false, _isClose = true;

        public FgdEditItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TabControlFgdMethod.SelectedIndex = -1;
                foreach (DYFGDItemPara fgditem in Global.fgdItems)
                {
                    ComboBoxCIItem1Name.Items.Add(fgditem.Name);
                    ComboBoxCIItem2Name.Items.Add(fgditem.Name);
                }
                // 根据DeviceProp的波长，来决定这里面的选项。
                ComboBoxWave.ItemsSource = Global.deviceHole.GetWaveList();
                if (null != _item)
                {
                    // 已经有项目了，要提前显示这些项目。
                    TextBoxItemName.Text = _item.Name;
                    TextBoxUnit.Text = _item.Unit;
                    TextBoxHintStr.Text = _item.HintStr;
                    TextBoxPassword.Text = _item.Password;
                    TextBoxSampleNum.Text = string.Empty + _item.SampleNum;
                    ComboBoxWave.Text = string.Empty + _item.Wave;
                    TextBoxValidHole.Text = Global.deviceHole.GetWaveHoleString(_item.Wave);
                    TabControlFgdMethod.SelectedIndex = ComboBoxMethod.SelectedIndex = _MethodToIndex[_item.Method]; // 去掉了子项目法，所以这样。
                    if (0 == _MethodToIndex[_item.Method])
                    {
                        //抑制率法
                        TextBoxIRPreHeatTime.Text = string.Empty + _item.ir.PreHeatTime;
                        TextBoxIRDetTime.Text = string.Empty + _item.ir.DetTime;
                        TextBoxIRMinusL.Text = string.Empty + _item.ir.MinusL;
                        TextBoxIRMinusH.Text = string.Empty + _item.ir.MinusH;
                        TextBoxIRPlusL.Text = string.Empty + _item.ir.PlusL;
                        TextBoxIRPlusH.Text = string.Empty + _item.ir.PlusH;
                    }
                    else if (1 == _MethodToIndex[_item.Method])
                    {
                        //标准曲线法
                        TextBoxSCPreHeatTime.Text = string.Empty + _item.sc.PreHeatTime;
                        TextBoxSCDetTime.Text = string.Empty + _item.sc.DetTime;
                        TextBoxSCA0.Text = string.Empty + _item.sc.A0;
                        TextBoxSCA1.Text = string.Empty + _item.sc.A1;
                        TextBoxSCA2.Text = string.Empty + _item.sc.A2;
                        TextBoxSCA3.Text = string.Empty + _item.sc.A3;
                        TextBoxSCAFROM.Text = string.Empty + _item.sc.AFrom;
                        TextBoxSCATO.Text = string.Empty + _item.sc.ATo;
                        TextBoxSCB0.Text = string.Empty + _item.sc.B0;
                        TextBoxSCB1.Text = string.Empty + _item.sc.B1;
                        TextBoxSCB2.Text = string.Empty + _item.sc.B2;
                        TextBoxSCB3.Text = string.Empty + _item.sc.B3;
                        TextBoxSCBFROM.Text = string.Empty + _item.sc.BFrom;
                        TextBoxSCBTO.Text = string.Empty + _item.sc.BTo;
                        TextBoxSCCCA.Text = string.Empty + _item.sc.CCA;
                        TextBoxSCCCB.Text = string.Empty + _item.sc.CCB;
                        CheckBoxSCIsReverse.IsChecked = _item.sc.IsReverse;
                        TextBoxSCHLevel.Text = string.Empty + _item.sc.HLevel;
                        TextBoxSCLLevel.Text = string.Empty + _item.sc.LLevel;
                    }
                    else if (2 == _item.Method)
                    {
                        ComboBoxCIItem1Name.Text = _item.ci.Item1Name;
                        TextBoxCIItem1Rate.Text = string.Empty + _item.ci.Item1Rate;
                        TextBoxCIItem1From.Text = string.Empty + _item.ci.Item1From;
                        TextBoxCIItem1To.Text = string.Empty + _item.ci.Item1To;
                        ComboBoxCIItem2Name.Text = _item.ci.Item2Name;
                        TextBoxCIItem2Rate.Text = string.Empty + _item.ci.Item2Rate;
                        TextBoxCIItem2From.Text = string.Empty + _item.ci.Item2From;
                        TextBoxCIItem2To.Text = string.Empty + _item.ci.Item2To;
                        TextBoxCILLevel.Text = string.Empty + _item.ci.LLevel;
                        TextBoxCIHLevel.Text = string.Empty + _item.ci.HLevel;
                    }
                    else if (3 == _item.Method)
                    {
                        //动力学法
                        TextBoxDNPreHeatTime.Text = string.Empty + _item.dn.PreHeatTime;
                        TextBoxDNDetTime.Text = string.Empty + _item.dn.DetTime;
                        TextBoxDNA0.Text = string.Empty + _item.dn.A0;
                        TextBoxDNA1.Text = string.Empty + _item.dn.A1;
                        TextBoxDNA2.Text = string.Empty + _item.dn.A2;
                        TextBoxDNA3.Text = string.Empty + _item.dn.A3;
                        TextBoxDNAFROM.Text = string.Empty + _item.dn.AFrom;
                        TextBoxDNATO.Text = string.Empty + _item.dn.ATo;
                        TextBoxDNB0.Text = string.Empty + _item.dn.B0;
                        TextBoxDNB1.Text = string.Empty + _item.dn.B1;
                        TextBoxDNB2.Text = string.Empty + _item.dn.B2;
                        TextBoxDNB3.Text = string.Empty + _item.dn.B3;
                        TextBoxDNBFROM.Text = string.Empty + _item.dn.BFrom;
                        TextBoxDNBTO.Text = string.Empty + _item.dn.BTo;
                        TextBoxDNCCA.Text = string.Empty + _item.dn.CCA;
                        TextBoxDNCCB.Text = string.Empty + _item.dn.CCB;
                        CheckBoxDNIsReverse.IsChecked = _item.dn.IsReverse;
                        TextBoxDNHLevel.Text = string.Empty + _item.dn.HLevel;
                        TextBoxDNLLevel.Text = string.Empty + _item.dn.LLevel;
                    }
                    else if (4 == _item.Method)
                    {
                        //系数法
                        TextBoxCOA0.Text = string.Empty + _item.co.A0;
                        TextBoxCOA1.Text = string.Empty + _item.co.A1;
                        TextBoxCOA2.Text = string.Empty + _item.co.A2;
                        TextBoxCOA3.Text = string.Empty + _item.co.A3;
                        TextBoxCOLLevel.Text = string.Empty + _item.co.LLevel;
                        TextBoxCOHLevel.Text = string.Empty + _item.co.HLevel;
                    }
                    TabControlFgdMethod.SelectedIndex = _IndexToMethod[ComboBoxMethod.SelectedIndex];
                }
                else
                {
                    ProjectName.Content = "新建项目";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(this, "请设置【项目名称】!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxItemName.Focus();
                return false;
            }
            if (ComboBoxMethod.SelectedIndex < 0)
            {
                MessageBox.Show(this, "请设置【检测方法】!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                ComboBoxMethod.Focus();
                return false;
            }
            str = TextBoxUnit.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show(this, "请设置【单位】!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxUnit.Focus();
                return false;
            }
            str = ComboBoxWave.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show(this, "请设置【波长】!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                ComboBoxWave.Focus();
                return false;
            }
            str = TextBoxPassword.Text.Trim();
            if (str.Length == 0)
            {
                if (MessageBox.Show("【未设置密码】 确定保存无密码的检测项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    TextBoxPassword.Focus();
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
                        _item = new DYFGDItemPara();
                        Global.fgdItems.Add(_item);
                    }
                    if (sender == null && e == null)
                    {
                        _item.Name = _FGDItemNameType[0].ToString();//项目名称
                        _item.Unit = _FGDItemNameType[3].ToString();//项目检测单位
                        _item.HintStr = _FGDItemNameType[4].ToString();//简要操作:新添加实验--注释
                        _item.Password = _FGDItemNameType[5].ToString();//密码
                        int.TryParse(_FGDItemNameType[6].ToString(), out _item.SampleNum);//样品编号
                        int.TryParse(_FGDItemNameType[11].ToString(), out _item.Wave);//波长
                        //检测方法选择
                        _item.Method = Convert.ToInt32(_FGDItemNameType[12].ToString());
                    }
                    else
                    {
                        _item.Name = TextBoxItemName.Text.Trim();//项目名称
                        _item.Unit = TextBoxUnit.Text.Trim();//项目检测单位
                        _item.HintStr = TextBoxHintStr.Text.Trim();//简要操作:新添加实验--注释
                        _item.Password = TextBoxPassword.Text.Trim();//密码
                        int.TryParse(TextBoxSampleNum.Text.Trim(), out _item.SampleNum);//样品编号
                        int.TryParse(ComboBoxWave.Text.Trim(), out _item.Wave);//波长
                        //检测方法选择
                        _item.Method = _IndexToMethod[ComboBoxMethod.SelectedIndex];
                    }

                    if (0 == _item.Method)
                    {
                        int.TryParse(TextBoxIRPreHeatTime.Text.Trim(), out _item.ir.PreHeatTime);
                        int.TryParse(TextBoxIRDetTime.Text.Trim(), out _item.ir.DetTime);
                        int.TryParse(TextBoxIRMinusL.Text.Trim(), out _item.ir.MinusL);
                        int.TryParse(TextBoxIRMinusH.Text.Trim(), out _item.ir.MinusH);
                        int.TryParse(TextBoxIRPlusL.Text.Trim(), out _item.ir.PlusL);
                        int.TryParse(TextBoxIRPlusH.Text.Trim(), out _item.ir.PlusH);
                    }
                    else if (1 == _item.Method)
                    {
                        int.TryParse(TextBoxSCPreHeatTime.Text.Trim(), out _item.sc.PreHeatTime);
                        int.TryParse(TextBoxSCDetTime.Text.Trim(), out _item.sc.DetTime);
                        double.TryParse(TextBoxSCA0.Text.Trim(), out _item.sc.A0);//A曲线
                        double.TryParse(TextBoxSCA1.Text.Trim(), out _item.sc.A1);//
                        double.TryParse(TextBoxSCA2.Text.Trim(), out _item.sc.A2);//
                        double.TryParse(TextBoxSCA3.Text.Trim(), out _item.sc.A3);//
                        double.TryParse(TextBoxSCAFROM.Text.Trim(), out _item.sc.AFrom);//
                        double.TryParse(TextBoxSCATO.Text.Trim(), out _item.sc.ATo);//
                        double.TryParse(TextBoxSCB0.Text.Trim(), out _item.sc.B0);//B曲线
                        double.TryParse(TextBoxSCB1.Text.Trim(), out _item.sc.B1);//
                        double.TryParse(TextBoxSCB2.Text.Trim(), out _item.sc.B2);//
                        double.TryParse(TextBoxSCB3.Text.Trim(), out _item.sc.B3);//
                        double.TryParse(TextBoxSCBFROM.Text.Trim(), out _item.sc.BFrom);//
                        double.TryParse(TextBoxSCBTO.Text.Trim(), out _item.sc.BTo);//
                        double.TryParse(TextBoxSCCCA.Text.Trim(), out _item.sc.CCA);//国标上下限
                        double.TryParse(TextBoxSCCCB.Text.Trim(), out _item.sc.CCB);//
                        _item.sc.IsReverse = (bool)CheckBoxSCIsReverse.IsChecked;//
                        double.TryParse(TextBoxSCHLevel.Text.Trim(), out _item.sc.HLevel);//
                        double.TryParse(TextBoxSCLLevel.Text.Trim(), out _item.sc.LLevel);
                    }
                    else if (2 == _item.Method)
                    {
                        _item.ci.Item1Name = ComboBoxCIItem1Name.Text.Trim();
                        double.TryParse(TextBoxCIItem1Rate.Text.Trim(), out _item.ci.Item1Rate);//
                        double.TryParse(TextBoxCIItem1From.Text.Trim(), out _item.ci.Item1From);//
                        double.TryParse(TextBoxCIItem1To.Text.Trim(), out _item.ci.Item1To);//
                        _item.ci.Item2Name = ComboBoxCIItem2Name.Text.Trim();//
                        double.TryParse(TextBoxCIItem2Rate.Text.Trim(), out _item.ci.Item2Rate);//
                        double.TryParse(TextBoxCIItem2From.Text.Trim(), out _item.ci.Item2From);//
                        double.TryParse(TextBoxCIItem2To.Text.Trim(), out _item.ci.Item2To);//
                        double.TryParse(TextBoxCILLevel.Text.Trim(), out _item.ci.LLevel);//
                        double.TryParse(TextBoxCIHLevel.Text.Trim(), out _item.ci.HLevel);
                    }
                    else if (3 == _item.Method)
                    {
                        int.TryParse(TextBoxDNPreHeatTime.Text.Trim(), out _item.dn.PreHeatTime);
                        int.TryParse(TextBoxDNDetTime.Text.Trim(), out _item.dn.DetTime);//
                        double.TryParse(TextBoxDNA0.Text.Trim(), out _item.dn.A0);//
                        double.TryParse(TextBoxDNA1.Text.Trim(), out _item.dn.A1);//
                        double.TryParse(TextBoxDNA2.Text.Trim(), out _item.dn.A2);//
                        double.TryParse(TextBoxDNA3.Text.Trim(), out _item.dn.A3);//
                        double.TryParse(TextBoxDNAFROM.Text.Trim(), out _item.dn.AFrom);//
                        double.TryParse(TextBoxDNATO.Text.Trim(), out _item.dn.ATo);//
                        double.TryParse(TextBoxDNB0.Text.Trim(), out _item.dn.B0);//
                        double.TryParse(TextBoxDNB1.Text.Trim(), out _item.dn.B1);//
                        double.TryParse(TextBoxDNB2.Text.Trim(), out _item.dn.B2);//
                        double.TryParse(TextBoxDNB3.Text.Trim(), out _item.dn.B3);//
                        double.TryParse(TextBoxDNBFROM.Text.Trim(), out _item.dn.BFrom);//
                        double.TryParse(TextBoxDNBTO.Text.Trim(), out _item.dn.BTo);//
                        double.TryParse(TextBoxDNCCA.Text.Trim(), out _item.dn.CCA);//
                        double.TryParse(TextBoxDNCCB.Text.Trim(), out _item.dn.CCB);//
                        _item.dn.IsReverse = (bool)CheckBoxDNIsReverse.IsChecked;//
                        double.TryParse(TextBoxDNHLevel.Text.Trim(), out _item.dn.HLevel);//
                        double.TryParse(TextBoxDNLLevel.Text.Trim(), out _item.dn.LLevel);
                    }
                    else if (4 == _item.Method)
                    {
                        double.TryParse(TextBoxCOA0.Text.Trim(), out _item.co.A0);
                        double.TryParse(TextBoxCOA1.Text.Trim(), out _item.co.A1);//
                        double.TryParse(TextBoxCOA2.Text.Trim(), out _item.co.A2);//
                        double.TryParse(TextBoxCOA3.Text.Trim(), out _item.co.A3);//
                        double.TryParse(TextBoxCOLLevel.Text.Trim(), out _item.co.LLevel);//
                        double.TryParse(TextBoxCOHLevel.Text.Trim(), out _item.co.HLevel);
                    }
                    Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
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
                MessageBox.Show(this, "异常:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception) { }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControlFgdMethod.SelectedIndex = _IndexToMethod[ComboBoxMethod.SelectedIndex];
            _isUpdate = true;
        }

        private void ComboBoxWave_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int wave = 0;
                int.TryParse(ComboBoxWave.Text.Trim(), out wave);
                TextBoxValidHole.Text = Global.deviceHole.GetWaveHoleString(wave);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        /// <summary>
        /// 双击文本框弹出查询界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxItemName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Global.projectName = string.Empty;
            Global.IsProject = true;
            KsSearchWindow window = new KsSearchWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
            TextBoxItemName.Text = Global.projectName;
            //if (Global.projectName.Length > 0)
            //{
            //    string[] items = Global.projectName.Split(',');
            //    if (items != null && items.Length > 0)
            //    {
            //        TextBoxItemName.Text = items[1];
            //    }
            //}
        }

        private void TextBoxItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
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

        private void ComboBoxWave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxValidHole_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRPreHeatTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRDetTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRMinusL_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRMinusH_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRPlusL_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxIRPlusH_TextChanged(object sender, TextChangedEventArgs e)
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