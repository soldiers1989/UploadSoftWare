using System;
using System.Windows;
using System.Windows.Controls;
using AIO.xaml.Dialog;
using AIO.xaml.Fenguangdu;
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
        private bool _isUpdate = false;
        private bool _isClose = true;
        private string logType = "FgdEditItemWindow-error";

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
                    TextBoxSampleNum.Text = "" + _item.SampleNum;
                    ComboBoxWave.Text = "" + _item.Wave;
                    TextBoxValidHole.Text = Global.deviceHole.GetWaveHoleString(_item.Wave);
                    TabControlFgdMethod.SelectedIndex = ComboBoxMethod.SelectedIndex = _MethodToIndex[_item.Method]; // 去掉了子项目法，所以这样。
                    if (0 == _MethodToIndex[_item.Method])
                    {
                        //抑制率法
                        TextBoxIRPreHeatTime.Text = "" + _item.ir.PreHeatTime;
                        TextBoxIRDetTime.Text = "" + _item.ir.DetTime;
                        TextBoxIRMinusL.Text = "" + _item.ir.MinusL;
                        TextBoxIRMinusH.Text = "" + _item.ir.MinusH;
                        TextBoxIRPlusL.Text = "" + _item.ir.PlusL;
                        TextBoxIRPlusH.Text = "" + _item.ir.PlusH;
                    }
                    else if (1 == _MethodToIndex[_item.Method])
                    {
                        //标准曲线法
                        TextBoxSCPreHeatTime.Text = "" + _item.sc.PreHeatTime;
                        TextBoxSCDetTime.Text = "" + _item.sc.DetTime;
                        TextBoxSCA0.Text = "" + _item.sc.A0;
                        TextBoxSCA1.Text = "" + _item.sc.A1;
                        TextBoxSCA2.Text = "" + _item.sc.A2;
                        TextBoxSCA3.Text = "" + _item.sc.A3;
                        TextBoxSCAFROM.Text = "" + _item.sc.AFrom;
                        TextBoxSCATO.Text = "" + _item.sc.ATo;
                        TextBoxSCB0.Text = "" + _item.sc.B0;
                        TextBoxSCB1.Text = "" + _item.sc.B1;
                        TextBoxSCB2.Text = "" + _item.sc.B2;
                        TextBoxSCB3.Text = "" + _item.sc.B3;
                        TextBoxSCBFROM.Text = "" + _item.sc.BFrom;
                        TextBoxSCBTO.Text = "" + _item.sc.BTo;
                        TextBoxSCC0.Text = "" + _item.sc.C0;
                        TextBoxSCC1.Text = "" + _item.sc.C1;
                        TextBoxSCC2.Text = "" + _item.sc.C2;
                        TextBoxSCC3.Text = "" + _item.sc.C3;
                        TextBoxSCCFROM.Text = "" + _item.sc.CFrom;
                        TextBoxSCCTO.Text = "" + _item.sc.CTo;
                        Cb_CalcCurve.IsChecked = _item.sc.IsEnabledCalcCurve;
                        //Btn_CalculatedCurve.Visibility = Visibility.Visible;
                        TextBoxSCCCA.Text = "" + _item.sc.CCA;
                        TextBoxSCCCB.Text = "" + _item.sc.CCB;
                        CheckBoxSCIsReverse.IsChecked = _item.sc.IsReverse;
                        TextBoxSCHLevel.Text = "" + _item.sc.HLevel;
                        TextBoxSCLLevel.Text = "" + _item.sc.LLevel;
                    }
                    else if (2 == _item.Method)
                    {
                        ComboBoxCIItem1Name.Text = _item.ci.Item1Name;
                        TextBoxCIItem1Rate.Text = "" + _item.ci.Item1Rate;
                        TextBoxCIItem1From.Text = "" + _item.ci.Item1From;
                        TextBoxCIItem1To.Text = "" + _item.ci.Item1To;
                        ComboBoxCIItem2Name.Text = _item.ci.Item2Name;
                        TextBoxCIItem2Rate.Text = "" + _item.ci.Item2Rate;
                        TextBoxCIItem2From.Text = "" + _item.ci.Item2From;
                        TextBoxCIItem2To.Text = "" + _item.ci.Item2To;
                        TextBoxCILLevel.Text = "" + _item.ci.LLevel;
                        TextBoxCIHLevel.Text = "" + _item.ci.HLevel;
                    }
                    else if (3 == _item.Method)
                    {
                        //动力学法
                        TextBoxDNPreHeatTime.Text = "" + _item.dn.PreHeatTime;
                        TextBoxDNDetTime.Text = "" + _item.dn.DetTime;
                        TextBoxDNA0.Text = "" + _item.dn.A0;
                        TextBoxDNA1.Text = "" + _item.dn.A1;
                        TextBoxDNA2.Text = "" + _item.dn.A2;
                        TextBoxDNA3.Text = "" + _item.dn.A3;
                        TextBoxDNAFROM.Text = "" + _item.dn.AFrom;
                        TextBoxDNATO.Text = "" + _item.dn.ATo;
                        TextBoxDNB0.Text = "" + _item.dn.B0;
                        TextBoxDNB1.Text = "" + _item.dn.B1;
                        TextBoxDNB2.Text = "" + _item.dn.B2;
                        TextBoxDNB3.Text = "" + _item.dn.B3;
                        TextBoxDNBFROM.Text = "" + _item.dn.BFrom;
                        TextBoxDNBTO.Text = "" + _item.dn.BTo;
                        TextBoxDNCCA.Text = "" + _item.dn.CCA;
                        TextBoxDNCCB.Text = "" + _item.dn.CCB;
                        CheckBoxDNIsReverse.IsChecked = _item.dn.IsReverse;
                        TextBoxDNHLevel.Text = "" + _item.dn.HLevel;
                        TextBoxDNLLevel.Text = "" + _item.dn.LLevel;
                    }
                    else if (4 == _item.Method)
                    {
                        //系数法
                        TextBoxCOA0.Text = "" + _item.co.A0;
                        TextBoxCOA1.Text = "" + _item.co.A1;
                        TextBoxCOA2.Text = "" + _item.co.A2;
                        TextBoxCOA3.Text = "" + _item.co.A3;
                        TextBoxCOLLevel.Text = "" + _item.co.LLevel;
                        TextBoxCOHLevel.Text = "" + _item.co.HLevel;
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
                FileUtils.OprLog(1, logType, ex.ToString());
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
            try
            {
                str = TextBoxItemName.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("【项目名称】不能为空!", "操作提示");
                    TextBoxItemName.Focus();
                    //_IsCheckOK = false;
                    return false;
                }
                if (ComboBoxMethod.SelectedIndex < 0)
                {
                    MessageBox.Show("请设置【检测方法】!", "操作提示");
                    ComboBoxMethod.Focus();
                    //_IsCheckOK = false;
                    return false;
                }
                str = TextBoxUnit.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【单位】!", "操作提示");
                    TextBoxUnit.Focus();
                    //_IsCheckOK = false;
                    return false;
                }
                str = ComboBoxWave.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【波长】!", "操作提示");
                    ComboBoxWave.Focus();
                    //_IsCheckOK = false;
                    return false;
                }
                str = TextBoxPassword.Text.Trim();
                if (str.Length == 0)
                {
                    if (MessageBox.Show("【未设置密码】 确定保存无密码的检测项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        TextBoxPassword.Focus();
                        //_IsCheckOK = false;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
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
                        Int32.TryParse(_FGDItemNameType[6].ToString(), out _item.SampleNum);//样品编号
                        Int32.TryParse(_FGDItemNameType[11].ToString(), out _item.Wave);//波长
                        //检测方法选择
                        _item.Method = Convert.ToInt32(_FGDItemNameType[12].ToString());
                    }
                    else
                    {
                        _item.Name = TextBoxItemName.Text.Trim();//项目名称
                        _item.Unit = TextBoxUnit.Text.Trim();//项目检测单位
                        _item.HintStr = TextBoxHintStr.Text.Trim();//简要操作:新添加实验--注释
                        _item.Password = TextBoxPassword.Text.Trim();//密码
                        Int32.TryParse(TextBoxSampleNum.Text.Trim(), out _item.SampleNum);//样品编号
                        Int32.TryParse(ComboBoxWave.Text.Trim(), out _item.Wave);//波长
                        //检测方法选择
                        _item.Method = _IndexToMethod[ComboBoxMethod.SelectedIndex];
                    }

                    if (0 == _item.Method)
                    {
                        Int32.TryParse(TextBoxIRPreHeatTime.Text.Trim(), out _item.ir.PreHeatTime);//
                        Int32.TryParse(TextBoxIRDetTime.Text.Trim(), out _item.ir.DetTime);//
                        Int32.TryParse(TextBoxIRMinusL.Text.Trim(), out _item.ir.MinusL);//
                        Int32.TryParse(TextBoxIRMinusH.Text.Trim(), out _item.ir.MinusH);//
                        Int32.TryParse(TextBoxIRPlusL.Text.Trim(), out _item.ir.PlusL);//
                        Int32.TryParse(TextBoxIRPlusH.Text.Trim(), out _item.ir.PlusH);
                    }
                    else if (1 == _item.Method)
                    {
                        Int32.TryParse(TextBoxSCPreHeatTime.Text.Trim(), out _item.sc.PreHeatTime);
                        Int32.TryParse(TextBoxSCDetTime.Text.Trim(), out _item.sc.DetTime);
                        Double.TryParse(TextBoxSCA0.Text.Trim(), out _item.sc.A0);//A曲线
                        Double.TryParse(TextBoxSCA1.Text.Trim(), out _item.sc.A1);//
                        Double.TryParse(TextBoxSCA2.Text.Trim(), out _item.sc.A2);//
                        Double.TryParse(TextBoxSCA3.Text.Trim(), out _item.sc.A3);//
                        Double.TryParse(TextBoxSCAFROM.Text.Trim(), out _item.sc.AFrom);//
                        Double.TryParse(TextBoxSCATO.Text.Trim(), out _item.sc.ATo);//
                        Double.TryParse(TextBoxSCB0.Text.Trim(), out _item.sc.B0);//B曲线
                        Double.TryParse(TextBoxSCB1.Text.Trim(), out _item.sc.B1);//
                        Double.TryParse(TextBoxSCB2.Text.Trim(), out _item.sc.B2);//
                        Double.TryParse(TextBoxSCB3.Text.Trim(), out _item.sc.B3);//
                        Double.TryParse(TextBoxSCBFROM.Text.Trim(), out _item.sc.BFrom);//
                        Double.TryParse(TextBoxSCBTO.Text.Trim(), out _item.sc.BTo);//
                        Double.TryParse(TextBoxSCC0.Text.Trim(), out _item.sc.C0);//C曲线
                        Double.TryParse(TextBoxSCC1.Text.Trim(), out _item.sc.C1);//
                        Double.TryParse(TextBoxSCC2.Text.Trim(), out _item.sc.C2);//
                        Double.TryParse(TextBoxSCC3.Text.Trim(), out _item.sc.C3);//
                        Double.TryParse(TextBoxSCCFROM.Text.Trim(), out _item.sc.CFrom);//
                        Double.TryParse(TextBoxSCCTO.Text.Trim(), out _item.sc.CTo);//
                        _item.sc.IsEnabledCalcCurve = (bool)Cb_CalcCurve.IsChecked;//是否启用自动生成曲线
                        Double.TryParse(TextBoxSCCCA.Text.Trim(), out _item.sc.CCA);//国标上下限
                        Double.TryParse(TextBoxSCCCB.Text.Trim(), out _item.sc.CCB);//
                        _item.sc.IsReverse = (bool)CheckBoxSCIsReverse.IsChecked;//
                        Double.TryParse(TextBoxSCHLevel.Text.Trim(), out _item.sc.HLevel);//
                        Double.TryParse(TextBoxSCLLevel.Text.Trim(), out _item.sc.LLevel);
                    }
                    else if (2 == _item.Method)
                    {
                        _item.ci.Item1Name = ComboBoxCIItem1Name.Text.Trim();
                        Double.TryParse(TextBoxCIItem1Rate.Text.Trim(), out _item.ci.Item1Rate);//
                        Double.TryParse(TextBoxCIItem1From.Text.Trim(), out _item.ci.Item1From);//
                        Double.TryParse(TextBoxCIItem1To.Text.Trim(), out _item.ci.Item1To);//
                        _item.ci.Item2Name = ComboBoxCIItem2Name.Text.Trim();//
                        Double.TryParse(TextBoxCIItem2Rate.Text.Trim(), out _item.ci.Item2Rate);//
                        Double.TryParse(TextBoxCIItem2From.Text.Trim(), out _item.ci.Item2From);//
                        Double.TryParse(TextBoxCIItem2To.Text.Trim(), out _item.ci.Item2To);//
                        Double.TryParse(TextBoxCILLevel.Text.Trim(), out _item.ci.LLevel);//
                        Double.TryParse(TextBoxCIHLevel.Text.Trim(), out _item.ci.HLevel);

                    }
                    else if (3 == _item.Method)
                    {
                        Int32.TryParse(TextBoxDNPreHeatTime.Text.Trim(), out _item.dn.PreHeatTime);
                        Int32.TryParse(TextBoxDNDetTime.Text.Trim(), out _item.dn.DetTime);//
                        Double.TryParse(TextBoxDNA0.Text.Trim(), out _item.dn.A0);//
                        Double.TryParse(TextBoxDNA1.Text.Trim(), out _item.dn.A1);//
                        Double.TryParse(TextBoxDNA2.Text.Trim(), out _item.dn.A2);//
                        Double.TryParse(TextBoxDNA3.Text.Trim(), out _item.dn.A3);//
                        Double.TryParse(TextBoxDNAFROM.Text.Trim(), out _item.dn.AFrom);//
                        Double.TryParse(TextBoxDNATO.Text.Trim(), out _item.dn.ATo);//
                        Double.TryParse(TextBoxDNB0.Text.Trim(), out _item.dn.B0);//
                        Double.TryParse(TextBoxDNB1.Text.Trim(), out _item.dn.B1);//
                        Double.TryParse(TextBoxDNB2.Text.Trim(), out _item.dn.B2);//
                        Double.TryParse(TextBoxDNB3.Text.Trim(), out _item.dn.B3);//
                        Double.TryParse(TextBoxDNBFROM.Text.Trim(), out _item.dn.BFrom);//
                        Double.TryParse(TextBoxDNBTO.Text.Trim(), out _item.dn.BTo);//
                        Double.TryParse(TextBoxDNCCA.Text.Trim(), out _item.dn.CCA);//
                        Double.TryParse(TextBoxDNCCB.Text.Trim(), out _item.dn.CCB);//
                        _item.dn.IsReverse = (bool)CheckBoxDNIsReverse.IsChecked;//
                        Double.TryParse(TextBoxDNHLevel.Text.Trim(), out _item.dn.HLevel);//
                        Double.TryParse(TextBoxDNLLevel.Text.Trim(), out _item.dn.LLevel);
                    }
                    else if (4 == _item.Method)
                    {
                        Double.TryParse(TextBoxCOA0.Text.Trim(), out _item.co.A0);
                        Double.TryParse(TextBoxCOA1.Text.Trim(), out _item.co.A1);//
                        Double.TryParse(TextBoxCOA2.Text.Trim(), out _item.co.A2);//
                        Double.TryParse(TextBoxCOA3.Text.Trim(), out _item.co.A3);//
                        Double.TryParse(TextBoxCOLLevel.Text.Trim(), out _item.co.LLevel);//
                        Double.TryParse(TextBoxCOHLevel.Text.Trim(), out _item.co.HLevel);
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
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControlFgdMethod.SelectedIndex = _IndexToMethod[ComboBoxMethod.SelectedIndex];
            _isUpdate = true;
            //标准曲线法
            //Btn_CalculatedCurve.Visibility = ComboBoxMethod.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ComboBoxWave_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int wave = 0;
                Int32.TryParse(ComboBoxWave.Text.Trim(), out wave);
                TextBoxValidHole.Text = Global.deviceHole.GetWaveHoleString(wave);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
        }

        /// <summary>
        /// 双击文本框弹出查询界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxItemName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            try
            {
                new SearchSample().ShowDialog();
                if (!Global.projectName.Equals(""))
                {
                    this.TextBoxItemName.Text = Global.projectName;
                    this.TextBoxUnit.Text = Global.projectUnit;
                }
                Global.projectName = "";//还原项目名称的值
                Global.projectUnit = "";
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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

        private void TextBoxIRPlusH_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
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

        /// <summary>
        /// 自动生成曲线 仅标准曲线法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CalculatedCurve_Click(object sender, RoutedEventArgs e)
        {
            CalculatedCurve window = new CalculatedCurve();
            window.WaveIndex = ComboBoxWave.SelectedIndex;
            window.ShowDialog();
            if (Global.CurveValue.Length > 0 && Global.CurveValue.IndexOf(',') > 0)
            {
                string[] vals = Global.CurveValue.Split(',');
                TextBoxSCC0.Text = vals[0];
                TextBoxSCC1.Text = vals[1];
            }
        }

    }
}