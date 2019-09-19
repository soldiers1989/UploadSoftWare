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
        private bool _isUpdate;
        private bool _isClose = true;
        private string logType = "JtjEditItemWindow-error";
        public int ModelType = 0;

        public JtjEditItemWindow()
        {
            InitializeComponent();
            Global.GetJtjVersion();
        }

        /// <summary>
        /// 根据胶体金模块信息显示不同的界面
        /// </summary>
        private void SettingJtjVer()
        {
            //获取到了胶体金模块信息
            if (Global.JtjVersionInfo != null && Global.JtjVersionInfo.Length == 3)
            {
                string ver = string.Empty;
                string val = Global.JtjVersionInfo[1].ToString("X");
                if (val.Length > 0)
                {
                    for (int i = 0; i < val.Length; i++)
                    {
                        ver += ver.Length > 0 ? "." + val[i] : val[i].ToString();
                    }
                }
                Lb_VersionInfo.Content = string.Format("胶体金模块版本号:{0}", ver);
                //新模块，不显示连续点数和趋势点数
                if (Global.JtjVersionInfo[1] >= 0x20)
                {
                    //TextBoxInvalidC.Visibility = lb_ic.Visibility = lb_c.Visibility = Visibility.Collapsed;
                    StackPanelHole.Visibility = Visibility.Collapsed;
                }
            }
            //未获取到胶体金模块信息默认显示旧模块
            else
            {
                Lb_VersionInfo.Content = "未获取到胶体金模块版本信息！";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.JtjVersionInfo == null)
            {
                if (MessageBox.Show("无法确定胶体金模块版本信息，是否进入设置界面进行通讯测试?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SettingsWindow window = new SettingsWindow
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
                }
            }
            SettingJtjVer();
            try
            {
                GridHole.Children.Add(GenerateHoleUIElement("wrapPanelHole", "checkBoxHole", Global.deviceHole.SxtCount, 55));
                //是否是新模块
                bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20);
                TabControlJTJMethod.SelectedIndex = -1;
                if (null != _item)
                {
                    // 已经有项目了，要提前显示这些项目。
                    TextBoxItemName.Text = _item.Name;
                    TextBoxPassword.Text = _item.Password;
                    TextBoxSampleNum.Text = _item.SampleNum.ToString();
                    TextBoxHintStr.Text = _item.HintStr;
                    TextBoxInvalidC.Text = IsNewModel ? _item.newInvalidC.ToString() : _item.InvalidC.ToString();
                    TextBoxUnit.Text = _item.Unit;

                    for (int method = 0; method < 4; ++method)
                    {
                        tb_pointCNum.Text = _item.pointCNum <= 0 ? "5" : _item.pointCNum.ToString();
                        tb_pointTNum.Text = _item.pointTNum <= 0 ? "5" : _item.pointTNum.ToString();
                        txtMaxPoint.Text = _item.maxPoint <= 0 ? "8" : _item.maxPoint.ToString();
                        if (0 == method)//消线法
                        {
                            if (IsNewModel)
                            {
                                if (_item.dxxx.newMaxT == null || _item.dxxx.newMinT == null)
                                {
                                    _item.dxxx.newMaxT = new double[2];
                                    _item.dxxx.newMinT = new double[2];
                                }
                                txtMax1.Text = _item.dxxx.newMaxT[0].ToString();
                                txtMin1.Text = _item.dxxx.newMinT[0].ToString();
                                txtMax2.Text = _item.dxxx.newMaxT[1].ToString();
                                txtMin2.Text = _item.dxxx.newMinT[1].ToString();
                            }
                            else
                            {
                                if (_item.dxxx.MaxT == null || _item.dxxx.MinT == null)
                                {
                                    _item.dxxx.MaxT = new double[2];
                                    _item.dxxx.MinT = new double[2];
                                }
                                txtMax1.Text = _item.dxxx.MaxT[0].ToString();
                                txtMin1.Text = _item.dxxx.MinT[0].ToString();
                                txtMax2.Text = _item.dxxx.MaxT[1].ToString();
                                txtMin2.Text = _item.dxxx.MinT[1].ToString();
                            }
                        }
                        else if (1 == method)//比色法
                        {
                            if (IsNewModel)
                            {
                                if (_item.dxbs.newMaxT == null || _item.dxbs.newMinT == null)
                                {
                                    _item.dxbs.newMaxT = new double[2];
                                    _item.dxbs.newMinT = new double[2];
                                }

                                txtBSMax1.Text = _item.dxbs.newMaxT[0].ToString();
                                txtBSMin1.Text = _item.dxbs.newMinT[0].ToString();
                                txtBSMax2.Text = _item.dxbs.newMaxT[1].ToString();
                                txtBSMin2.Text = _item.dxbs.newMinT[1].ToString();
                            }
                            else
                            {
                                if (_item.dxbs.MaxT == null || _item.dxbs.MinT == null)
                                {
                                    _item.dxbs.MaxT = new double[2];
                                    _item.dxbs.MinT = new double[2];
                                }

                                txtBSMax1.Text = _item.dxbs.MaxT[0].ToString();
                                txtBSMin1.Text = _item.dxbs.MinT[0].ToString();
                                txtBSMax2.Text = _item.dxbs.MaxT[1].ToString();
                                txtBSMin2.Text = _item.dxbs.MinT[1].ToString();
                                //comboBox_DXBSSelected.SelectedIndex = _item.dxbs.SetIdx;
                            }
                        }
                        else if (2 == method)
                        {
                            TextBoxDLTA0.Text = "" + _item.dlt.A0;
                            TextBoxDLTA1.Text = "" + _item.dlt.A1;
                            TextBoxDLTA2.Text = "" + _item.dlt.A2;
                            TextBoxDLTA3.Text = "" + _item.dlt.A3;
                            TextBoxDLTB0.Text = "" + _item.dlt.B0;
                            TextBoxDLTB1.Text = "" + _item.dlt.B1;
                            TextBoxDLTPPB.Text = "" + _item.dlt.Limit;
                        }
                        else if (3 == method)
                        {
                            TextBoxDLTCA0.Text = "" + _item.dltc.A0;
                            TextBoxDLTCA1.Text = "" + _item.dltc.A1;
                            TextBoxDLTCA2.Text = "" + _item.dltc.A2;
                            TextBoxDLTCA3.Text = "" + _item.dltc.A3;
                            TextBoxDLTCB0.Text = "" + _item.dltc.B0;
                            TextBoxDLTCB1.Text = "" + _item.dltc.B1;
                            TextBoxDLTCPPB.Text = "" + _item.dltc.Limit;
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
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            finally
            {
                _isUpdate = false;
            }
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

        /// <summary>
        /// 验证信息完整性
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            try
            {
                String str = String.Empty;
                str = TextBoxItemName.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【项目名称】！", "操作提示");
                    TextBoxItemName.Focus();
                    return false;
                }
                if (ComboBoxMethod.SelectedIndex < 0)
                {
                    MessageBox.Show("请设置【检测方法】！", "操作提示");
                    ComboBoxMethod.Focus();
                    return false;
                }
                str = TextBoxSampleNum.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【样品编号】！", "操作提示");
                    TextBoxSampleNum.Focus();
                    return false;
                }
                str = TextBoxInvalidC.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【C值】最小值！", "操作提示");
                    TextBoxInvalidC.Focus();
                    return false;
                }
                str = TextBoxUnit.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("请设置【单位】！", "操作提示");
                    TextBoxUnit.Focus();
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
                //List<CheckBox> listCheckBoxes = UIUtils.GetChildObjects<CheckBox>(StackPanelHole, typeof(CheckBox));
                //for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                //{
                //    if ((bool)listCheckBoxes[i].IsChecked)
                //    {
                //        str = "true";
                //        break;
                //    }
                //    str = string.Empty;
                //}
                //if (str.Length == 0)
                //{
                //    MessageBox.Show("请至少选择一个【检测孔】!", "操作提示");
                //    return false;
                //}

                double min, max;
                if (_item.Method == 0)
                {
                    if (!double.TryParse(txtMin1.Text.Trim(), out min))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtMin1.Focus();
                        return false;
                    }
                    else if (!double.TryParse(txtMax1.Text.Trim(), out max))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtMax1.Focus();
                        return false;
                    }
                    else if (max < min)
                    {
                        MessageBox.Show("参数范围设置不正确！", "操作提示");
                        return false;
                    }

                    if (!double.TryParse(txtMin2.Text.Trim(), out min))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtMin2.Focus();
                        return false;
                    }
                    else if (!double.TryParse(txtMax2.Text.Trim(), out max))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtMax2.Focus();
                        return false;
                    }
                    else if (max < min)
                    {
                        MessageBox.Show("参数范围设置不正确！", "操作提示");
                        return false;
                    }
                }
                else if (_item.Method == 1)
                {
                    if (!double.TryParse(txtBSMin1.Text.Trim(), out min))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtBSMin1.Focus();
                        return false;
                    }
                    else if (!double.TryParse(txtBSMax1.Text.Trim(), out max))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtBSMax1.Focus();
                        return false;
                    }
                    else if (max < min)
                    {
                        MessageBox.Show("参数范围设置不正确！", "操作提示");
                        return false;
                    }

                    if (!double.TryParse(txtBSMin2.Text.Trim(), out min))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtBSMin2.Focus();
                        return false;
                    }
                    else if (!double.TryParse(txtBSMax2.Text.Trim(), out max))
                    {
                        MessageBox.Show("格式不正确！", "操作提示");
                        txtBSMax2.Focus();
                        return false;
                    }
                    else if (max < min)
                    {
                        MessageBox.Show("参数范围设置不正确！", "操作提示");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
            }
            return true;
        }

        public void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //是否是新模块
                bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20);
                if (Check())
                {
                    if (null == _item)
                    {
                        // 这属于新添加
                        _item = new DYJTJItemPara
                        {
                            Type = ModelType
                        };
                        Global.jtjItems.Add(_item);
                    }
                    //胶体金计算条件
                    int.TryParse(tb_pointCNum.Text, out _item.pointCNum);
                    int.TryParse(tb_pointTNum.Text, out _item.pointTNum);
                    int.TryParse(txtMaxPoint.Text, out _item.maxPoint);

                    if (sender == null && e == null)
                    {
                        _item.Name = _JTJItemNameType[0].ToString();//项目名称
                        _item.Unit = _JTJItemNameType[3].ToString();//项目检测单位
                        _item.HintStr = _JTJItemNameType[4].ToString();//简要操作：新添加实验--注释
                        _item.Password = _JTJItemNameType[5].ToString();//密码
                        int.TryParse(_JTJItemNameType[6].ToString(), out _item.SampleNum);//样品编号
                        //检测方法选择
                        _item.Method = Convert.ToInt32(_JTJItemNameType[12].ToString());
                        _item.Hole[0].Use = Convert.ToBoolean(_JTJItemNameType[8].ToString());
                        _item.Hole[1].Use = Convert.ToBoolean(_JTJItemNameType[9].ToString());
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;

                        if (IsNewModel)
                        {
                            _item.newInvalidC = Convert.ToInt32(_JTJItemNameType[7].ToString());
                        }
                        else
                        {
                            _item.InvalidC = Convert.ToInt32(_JTJItemNameType[7].ToString());
                        }
                    }
                    else
                    {
                        _item.Name = TextBoxItemName.Text;
                        _item.Password = TextBoxPassword.Text;
                        _item.HintStr = TextBoxHintStr.Text;
                        int.TryParse(TextBoxSampleNum.Text, out _item.SampleNum);
                        _item.Unit = TextBoxUnit.Text;
                        if (IsNewModel)
                        {
                            double.TryParse(TextBoxInvalidC.Text, out _item.newInvalidC);
                        }
                        else
                        {
                            double.TryParse(TextBoxInvalidC.Text, out _item.InvalidC);
                        }
                        _item.Method = ComboBoxMethod.SelectedIndex;
                    }
                    //定性消线
                    if (0 == _item.Method)
                    {
                        if (IsNewModel)
                        {
                            _item.dxxx.newMaxT = new double[2];
                            _item.dxxx.newMinT = new double[2];
                            double.TryParse(txtMax1.Text, out _item.dxxx.newMaxT[0]);
                            double.TryParse(txtMin1.Text, out _item.dxxx.newMinT[0]);
                            double.TryParse(txtMax2.Text, out _item.dxxx.newMaxT[1]);
                            double.TryParse(txtMin2.Text, out _item.dxxx.newMinT[1]);
                        }
                        else
                        {
                            _item.dxxx.MaxT = new double[2];
                            _item.dxxx.MinT = new double[2];
                            double.TryParse(txtMax1.Text, out _item.dxxx.MaxT[0]);
                            double.TryParse(txtMin1.Text, out _item.dxxx.MinT[0]);
                            double.TryParse(txtMax2.Text, out _item.dxxx.MaxT[1]);
                            double.TryParse(txtMin2.Text, out _item.dxxx.MinT[1]);
                        }
                    }
                    //定性比色
                    else if (1 == _item.Method)
                    {
                        if (IsNewModel)
                        {
                            _item.dxbs.newMaxT = new double[2];
                            _item.dxbs.newMinT = new double[2];
                            double.TryParse(txtBSMax1.Text, out _item.dxbs.newMaxT[0]);
                            double.TryParse(txtBSMin1.Text, out _item.dxbs.newMinT[0]);
                            double.TryParse(txtBSMax2.Text, out _item.dxbs.newMaxT[1]);
                            double.TryParse(txtBSMin2.Text, out _item.dxbs.newMinT[1]);
                        }
                        else
                        {
                            _item.dxbs.MaxT = new double[2];
                            _item.dxbs.MinT = new double[2];
                            double.TryParse(txtBSMax1.Text, out _item.dxbs.MaxT[0]);
                            double.TryParse(txtBSMin1.Text, out _item.dxbs.MinT[0]);
                            double.TryParse(txtBSMax2.Text, out _item.dxbs.MaxT[1]);
                            double.TryParse(txtBSMin2.Text, out _item.dxbs.MinT[1]);
                            //_item.dxbs.SetIdx = comboBox_DXBSSelected.SelectedIndex;
                        }
                    }
                    else if (2 == _item.Method)
                    {
                        double.TryParse(TextBoxDLTA0.Text, out _item.dlt.A0);
                        double.TryParse(TextBoxDLTA1.Text, out _item.dlt.A1);
                        double.TryParse(TextBoxDLTA2.Text, out _item.dlt.A2);
                        double.TryParse(TextBoxDLTA3.Text, out _item.dlt.A3);
                        double.TryParse(TextBoxDLTB0.Text, out _item.dlt.B0);
                        double.TryParse(TextBoxDLTB1.Text, out _item.dlt.B1);
                        double.TryParse(TextBoxDLTPPB.Text, out _item.dlt.Limit);
                    }
                    else if (3 == _item.Method)
                    {
                        double.TryParse(TextBoxDLTCA0.Text, out _item.dltc.A0);
                        double.TryParse(TextBoxDLTCA1.Text, out _item.dltc.A1);
                        double.TryParse(TextBoxDLTCA2.Text, out _item.dltc.A2);
                        double.TryParse(TextBoxDLTCA3.Text, out _item.dltc.A3);
                        double.TryParse(TextBoxDLTCB0.Text, out _item.dltc.B0);
                        double.TryParse(TextBoxDLTCB1.Text, out _item.dltc.B1);
                        double.TryParse(TextBoxDLTCPPB.Text, out _item.dltc.Limit);
                    }

                    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                    if (sender != null && e != null)
                    {
                        _isClose = true;
                        _isUpdate = false;
                        try { this.Close(); }
                        catch (Exception ex) { FileUtils.OprLog(2, logType, ex.ToString()); }
                    }
                }
                else
                {
                    _isClose = false;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("保存失败!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try { this.Close(); }
            catch (Exception ex) { FileUtils.OprLog(2, logType, ex.ToString()); }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControlJTJMethod.SelectedIndex = ComboBoxMethod.SelectedIndex;
            _isUpdate = true;
            //if (TabControlJTJMethod.SelectedIndex == 1)
            //{
            //    TextBoxInvalidC.Visibility = Visibility.Visible;
            //    lb_c.Visibility = lb_ic.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    TextBoxInvalidC.Visibility = Visibility.Collapsed;
            //    lb_c.Visibility = lb_ic.Visibility = Visibility.Collapsed;
            //}
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

        private void TextBoxDXXXPlusT_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxDXXXMinusT_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void TextBoxHintStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void comboBox_DXBSSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void tb_DXBSAbs_TextChanged(object sender, TextChangedEventArgs e)
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
        /// 配置全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettingAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要将当前曲线配置应用到所有相同类型的检测项目吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                DYJTJItemPara item = null;
                List<DYJTJItemPara> jtjItems = Global.jtjItems;

                //是否是新模块
                bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20);
                int count = 0;
                for (int i = 0; i < jtjItems.Count; i++)
                {
                    item = Global.jtjItems[i];
                    if (item.Method != _item.Method) continue;
                    item.pointCNum = int.Parse(tb_pointCNum.Text.Trim());
                    item.pointTNum = int.Parse(tb_pointTNum.Text.Trim());
                    item.maxPoint = int.Parse(txtMaxPoint.Text.Trim());
                    if (IsNewModel)
                    {
                        item.newInvalidC = double.Parse(TextBoxInvalidC.Text.Trim());
                    }
                    else
                    {
                        item.InvalidC = double.Parse(TextBoxInvalidC.Text.Trim());
                    }
                    item.SampleNum = int.Parse(TextBoxSampleNum.Text.Trim());
                    item.Unit = TextBoxUnit.Text.Trim();
                    item.HintStr = TextBoxHintStr.Text;
                    if (item.Method == 0)//定性消线
                    {
                        if (IsNewModel)
                        {
                            item.dxxx.newMaxT = new double[2];
                            item.dxxx.newMinT = new double[2];
                            item.dxxx.newMaxT[0] = double.Parse(txtMax1.Text.Trim());
                            item.dxxx.newMinT[0] = double.Parse(txtMin1.Text.Trim());
                            item.dxxx.newMaxT[1] = double.Parse(txtMax2.Text.Trim());
                            item.dxxx.newMinT[1] = double.Parse(txtMin2.Text.Trim());
                        }
                        else
                        {
                            item.dxxx.MaxT = new double[2];
                            item.dxxx.MinT = new double[2];
                            item.dxxx.MaxT[0] = double.Parse(txtMax1.Text.Trim());
                            item.dxxx.MinT[0] = double.Parse(txtMin1.Text.Trim());
                            item.dxxx.MaxT[1] = double.Parse(txtMax2.Text.Trim());
                            item.dxxx.MinT[1] = double.Parse(txtMin2.Text.Trim());
                        }
                    }
                    else if (item.Method == 1)//定性比色
                    {
                        if (IsNewModel)
                        {
                            item.dxbs.newMaxT = new double[2];
                            item.dxbs.newMinT = new double[2];
                            item.dxbs.newMaxT[0] = double.Parse(txtBSMax1.Text.Trim());
                            item.dxbs.newMinT[0] = double.Parse(txtBSMin1.Text.Trim());
                            item.dxbs.newMaxT[1] = double.Parse(txtBSMax2.Text.Trim());
                            item.dxbs.newMinT[1] = double.Parse(txtBSMin2.Text.Trim());
                        }
                        else
                        {
                            item.dxbs.MaxT = new double[2];
                            item.dxbs.MinT = new double[2];
                            item.dxbs.MaxT[0] = double.Parse(txtBSMax1.Text.Trim());
                            item.dxbs.MinT[0] = double.Parse(txtBSMin1.Text.Trim());
                            item.dxbs.MaxT[1] = double.Parse(txtBSMax2.Text.Trim());
                            item.dxbs.MinT[1] = double.Parse(txtBSMin2.Text.Trim());
                        }
                    }
                    else if (item.Method == 2)//定量T
                    {
                        item.dlt.A0 = double.Parse(TextBoxDLTA0.Text.Trim());
                        item.dlt.A1 = double.Parse(TextBoxDLTA1.Text.Trim());
                        item.dlt.A2 = double.Parse(TextBoxDLTA2.Text.Trim());
                        item.dlt.A3 = double.Parse(TextBoxDLTA3.Text.Trim());
                        item.dlt.B0 = double.Parse(TextBoxDLTB0.Text.Trim());
                        item.dlt.B1 = double.Parse(TextBoxDLTB1.Text.Trim());
                        item.dlt.Limit = double.Parse(TextBoxDLTPPB.Text.Trim());
                    }
                    else if (item.Method == 3)//定量T/C
                    {
                        item.dltc.A0 = double.Parse(TextBoxDLTCA0.Text.Trim());
                        item.dltc.A1 = double.Parse(TextBoxDLTCA1.Text.Trim());
                        item.dltc.A2 = double.Parse(TextBoxDLTCA2.Text.Trim());
                        item.dltc.A3 = double.Parse(TextBoxDLTCA3.Text.Trim());
                        item.dltc.B0 = double.Parse(TextBoxDLTCB0.Text.Trim());
                        item.dltc.B1 = double.Parse(TextBoxDLTCB1.Text.Trim());
                        item.dltc.Limit = double.Parse(TextBoxDLTCPPB.Text.Trim());
                    }
                    count++;
                }
                _isUpdate = false;
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                MessageBox.Show(string.Format("本次成功配置 {0} 个检测项目参数！", count), "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\r\n异常信息：" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 配置全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (TextBoxPassword.Text.Equals("260905"))
                {
                    BtnSettingAll.Visibility = Visibility.Visible;
                    TextBoxPassword.Text = _item.Password;
                }
                else
                {
                    BtnSettingAll.Visibility = Visibility.Collapsed;
                    TextBoxPassword.Text = _item.Password;
                }
            }
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            double val1 = 0;
            if (!double.TryParse(txt.Text, out val1))
            {
                txt.Text = "0";
                MessageBox.Show("请输入数字！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            double val2 = 0;
            switch (txt.Name)
            {
                //case "txtMin1":
                //    if (double.TryParse(txtMax1.Text.Trim(), out val2))
                //    {
                //        if (val1 > val2)
                //        {
                //            MessageBox.Show("范围设置不合理，请重新输入！");
                //            txt.Text = "0";
                //            return;
                //        }
                //    }
                //    break;

                case "txtMax1":
                    if (double.TryParse(txtMin1.Text.Trim(), out val2))
                    {
                        if (val1 < val2)
                        {
                            MessageBox.Show("范围设置不合理，请重新输入！");
                            txt.Text = "0";
                            return;
                        }
                    }
                    break;

                //case "txtMin2":
                //    if (double.TryParse(txtMax2.Text.Trim(), out val2))
                //    {
                //        if (val1 > val2)
                //        {
                //            MessageBox.Show("范围设置不合理，请重新输入！");
                //            txt.Text = "0";
                //            return;
                //        }
                //    }
                //    break;

                case "txtMax2":
                    if (double.TryParse(txtMin2.Text.Trim(), out val2))
                    {
                        if (val1 < val2)
                        {
                            MessageBox.Show("范围设置不合理，请重新输入！");
                            txt.Text = "0";
                            return;
                        }
                    }
                    break;

                //case "txtBSMin1":
                //    if (double.TryParse(txtBSMax1.Text.Trim(), out val2))
                //    {
                //        if (val1 > val2)
                //        {
                //            MessageBox.Show("范围设置不合理，请重新输入！");
                //            txt.Text = "0";
                //            return;
                //        }
                //    }
                //    break;

                //case "txtBSMax1":
                //    if (double.TryParse(txtBSMin1.Text.Trim(), out val2))
                //    {
                //        if (val1 < val2)
                //        {
                //            MessageBox.Show("范围设置不合理，请重新输入！");
                //            txt.Text = "0";
                //            return;
                //        }
                //    }
                //    break;

                case "txtBSMin2":
                    if (double.TryParse(txtBSMax2.Text.Trim(), out val2))
                    {
                        if (val1 > val2)
                        {
                            MessageBox.Show("范围设置不合理，请重新输入！");
                            txt.Text = "0";
                            return;
                        }
                    }
                    break;

                case "txtBSMax2":
                    if (double.TryParse(txtBSMin2.Text.Trim(), out val2))
                    {
                        if (val1 < val2)
                        {
                            MessageBox.Show("范围设置不合理，请重新输入！");
                            txt.Text = "0";
                            return;
                        }
                    }
                    break;
            }
        }

    }
}