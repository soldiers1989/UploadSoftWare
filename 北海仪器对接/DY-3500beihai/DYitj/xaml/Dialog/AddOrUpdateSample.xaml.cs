using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddOrUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrUpdateSample : Window
    {
        public AddOrUpdateSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 存储从样品小精灵传过来的项目名称
        /// </summary>
        public string _projectName = string.Empty;
        public string _stand = "";
        public string _Standsymbol = "";
        public string _StandValue = "";
        public string _Unit = "";

        public clsttStandardDecide _decide = new clsttStandardDecide();
        public List<clsttStandardDecide> _ItemNames = new List<clsttStandardDecide>();
        /// <summary>
        /// 在本地数据库中未找到时新增的样品名称
        /// </summary>
        public string _sampleName = string.Empty;
        private clsttStandardDecideOpr _bll = new clsttStandardDecideOpr();
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private bool _isUpdate = false;
        private bool _isClose = true;
        private List<string> values = new List<string>();
        public bool IsSavedata = false;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            IsSavedata = true;
            Save();
        }

        /// <summary>
        /// 保存样品
        /// </summary>
        private void Save()
        {
            clsttStandardDecide model = new clsttStandardDecide();
            string err = string.Empty;
            bool request = true;
            if (CheckSave())
            {
                try
                {
                    if (_decide != null && _decide.ID > 0)
                    {
                        btnSave.Content = "修改";
                        model.ID = _decide.ID;
                    }
                    model.FtypeNmae = textBoxFtypeNmae.Text;
                    model.SampleNum = DateTime.Now.Millisecond.ToString();
                    model.Name = textBoxName.Text;
                    model.ItemDes = textBoxItemDes.Text;
                    model.StandardValue = textBoxStandardValue.Text;
                    model.Demarcate = textBoxDemarcate.Text;
                    model.Unit = textBoxUnit.Text;
                    err = string.Empty;
                    _bll.Insert(model, out err);
                }
                catch (Exception exception)
                {
                    request = false;
                    MessageBox.Show("保存失败!\n出现异常：" + exception.ToString());
                }
                finally
                {
                    if (request && btnSave.Content.Equals("保存") && _sampleName.Length == 0)
                    {
                        SettingValues();
                        if (MessageBox.Show("保存成功!\n是否继续添加国家检测标准?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            textBoxFtypeNmae.Text = string.Empty;
                            textBoxSampleNum.Text = string.Empty;
                            textBoxName.Text = _projectName.Equals(string.Empty) ? string.Empty : _projectName;
                            textBoxItemDes.Text = string.Empty;
                            textBoxStandardValue.Text = string.Empty;
                            textBoxDemarcate.Text = string.Empty;
                            textBoxUnit.Text = string.Empty;
                            btnSave.Content = "保存";
                            this.Title = "新增国家检测标准";
                            _decide = new clsttStandardDecide();
                            _isUpdate = false;
                            _isClose = false;
                        }
                        else
                        {
                            _isUpdate = false;
                            _isClose = true;
                            try { this.Close(); }
                            catch (Exception) { }
                        }
                    }
                    else
                    {
                        MessageBox.Show("保存成功!", "操作提示");
                        SettingValues();
                        _isClose = true;
                        try { this.Close(); }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                _isClose = false;
            }
        }

        private void SettingValues()
        {
            values = new List<string>
            {
                textBoxFtypeNmae.Text.Trim(),
                textBoxSampleNum.Text.Trim(),
                textBoxName.Text.Trim(),
                textBoxItemDes.Text.Trim(),
                textBoxStandardValue.Text,
                textBoxDemarcate.Text,
                textBoxUnit.Text
            };
            _isUpdate = false;
        }

        /// <summary>
        /// 验证信息完整性
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            string str = string.Empty, strT = "请选择或输入";
            str = textBoxFtypeNmae.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("样品名称不能为空!", "操作提示");
                textBoxFtypeNmae.Text = string.Empty;
                textBoxFtypeNmae.Focus();
                return false;
            }
            str = textBoxName.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("项目名称不能为空!", "操作提示");
                textBoxName.Text = string.Empty;
                textBoxName.Focus();
                return false;
            }
            str = textBoxItemDes.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("标准名称不能为空!", "操作提示");
                textBoxItemDes.Text = string.Empty;
                textBoxItemDes.Focus();
                return false;
            }
            str = textBoxStandardValue.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("检测标准值不能为空!", "操作提示");
                textBoxStandardValue.Text = string.Empty;
                textBoxStandardValue.Focus();
                return false;
            }
            str = textBoxDemarcate.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("判定符号不能为空!", "操作提示");
                textBoxDemarcate.Text = string.Empty;
                textBoxDemarcate.Focus();
                return false;
            }
            str = textBoxUnit.Text.Trim();
            if (str.Equals(string.Empty) || str.Equals(strT))
            {
                MessageBox.Show("单位不能为空!", "操作提示");
                textBoxUnit.Text = string.Empty;
                textBoxUnit.Focus();
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sampleName.Length > 0)
                {
                    this.textBoxFtypeNmae.Text = _sampleName;
                    this.textBoxFtypeNmae.IsReadOnly = false;
                }

                if (_projectName.Length > 0 && _ItemNames != null && _ItemNames.Count == 0)
                {
                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(string.Empty, _projectName, true, true, 1);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        _ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    }
                }

                if (_ItemNames != null && _ItemNames.Count > 0)
                    SetCheckBox();
                else
                {
                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(string.Empty, string.Empty, false, false, 1);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        _ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                        SetCheckBox();
                    }
                }
                if (_decide != null && _decide.ID > 0)
                {
                    textBoxFtypeNmae.Text = _decide.FtypeNmae;
                    textBoxSampleNum.Text = _decide.SampleNum;
                    textBoxName.Text = _decide.Name;
                    textBoxItemDes.Text = _decide.ItemDes;
                    textBoxStandardValue.Text = _decide.StandardValue;
                    textBoxDemarcate.Text = _decide.Demarcate;
                    textBoxUnit.Text = _decide.Unit;
                }
                else
                {
                    this.Title = "新增国家检测标准";
                    //textBoxFtypeNmae.Text = _decide.FtypeNmae;
                    //textBoxSampleNum.Text = _decide.SampleNum;
                    textBoxName.Text = _projectName;
                    //public string _Standsymbol = "";
                    //public string _StandValue = "";
                    //public string _Unit = "";
                    //textBoxItemDes.Text = _stand ;
                    //textBoxStandardValue.Text = _StandValue;
                    //textBoxDemarcate.Text = _Standsymbol;
                    if (_Unit.Length >0)
                       textBoxUnit.Text = _Unit;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message);
            }
            finally 
            {
                values.Add(textBoxFtypeNmae.Text.Trim());
                values.Add(textBoxSampleNum.Text.Trim());
                values.Add(textBoxName.Text.Trim());
                values.Add(textBoxItemDes.Text.Trim());
                values.Add(textBoxStandardValue.Text);
                values.Add(textBoxDemarcate.Text);
                values.Add(textBoxUnit.Text);
                _isUpdate = false;
            }
        }

        /// <summary>
        /// 给控件赋值
        /// </summary>
        private void SetCheckBox()
        {
            //标准名称
            IList<string> ItemDesList = new List<string>();
            IDictionary<string, string> dicItemDes = new Dictionary<string, string>();
            //样品名称
            IList<string> SampleNameList = new List<string>();
            IDictionary<string, string> dicSampleName = new Dictionary<string, string>();
            //项目名称
            IList<string> ItemsList = new List<string>();
            IDictionary<string, string> dicItems = new Dictionary<string, string>();
            //判定符号
            IList<string> DemarcateList = new List<string>();
            IDictionary<string, string> dicDemarcate = new Dictionary<string, string>();
            //单位
            IList<string> UnitList = new List<string>();
            IDictionary<string, string> dicUnit = new Dictionary<string, string>();
            try
            {
                //样品、单位、判定符号需单独从数据库中获取
                DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(string.Empty, string.Empty, false, false, 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsttStandardDecide> ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    if (ItemNames != null && ItemNames.Count > 0)
                    {
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            string str = ItemNames[i].FtypeNmae;
                            if (!dicSampleName.ContainsKey(str) && !str.Equals(string.Empty))
                            {
                                dicSampleName.Add(str, str);
                                SampleNameList.Add(str);
                            }
                            str = ItemNames[i].Unit;
                            if (!str.Equals(string.Empty) && !dicUnit.ContainsKey(str))
                            {
                                dicUnit.Add(str, str);
                                UnitList.Add(str);
                            }
                            str = ItemNames[i].Demarcate;
                            if (!str.Equals(string.Empty) && !dicDemarcate.ContainsKey(str))
                            {
                                dicDemarcate.Add(str, str);
                                DemarcateList.Add(str);
                            }
                        }
                    }
                }

                for (int i = 0; i < _ItemNames.Count; i++)
                {
                    string str = _ItemNames[i].ItemDes;
                    if (!dicItemDes.ContainsKey(str) && !str.Equals(string.Empty))
                    {
                        dicItemDes.Add(str, str);
                        ItemDesList.Add(str);
                    }
                    str = _ItemNames[i].Name;
                    if (!dicItems.ContainsKey(str) && !str.Equals(string.Empty))
                    {
                        dicItems.Add(str, str);
                        ItemsList.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message);
            }
            finally
            {
                this.textBoxFtypeNmae.ItemsSource = SampleNameList;
                this.textBoxName.ItemsSource = ItemsList;
                this.textBoxItemDes.ItemsSource = ItemDesList;
                this.textBoxItemDes.Text = _ItemNames[0].ItemDes;
                this.textBoxDemarcate.ItemsSource = DemarcateList;
                this.textBoxDemarcate.Text = _ItemNames[0].Demarcate;
                this.textBoxUnit.ItemsSource = UnitList;
                this.textBoxUnit.Text = _ItemNames[0].Unit;
            }
        }

        private void textBoxStandardValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);
            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        #region
        private void textBoxFtypeNmae_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFtypeNmae.Text.Trim().Equals(string.Empty))
            {
                textBoxFtypeNmae.Text = "请选择或输入";
            }
        }

        private void textBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Trim().Equals(string.Empty))
            {
                textBoxName.Text = "请选择或输入";
            }
        }

        private void textBoxItemDes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxItemDes.Text.Trim().Equals(string.Empty))
            {
                textBoxItemDes.Text = "请选择或输入";
            }
        }

        private void textBoxUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxUnit.Text.Trim().Equals(string.Empty))
            {
                textBoxUnit.Text = "请选择或输入";
            }
        }

        private void textBoxFtypeNmae_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxFtypeNmae.Text.Trim().Equals("请选择或输入"))
            {
                textBoxFtypeNmae.Text = string.Empty;
            }
        }

        private void textBoxName_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxName.Text.Equals("请选择或输入"))
            {
                textBoxName.Text = string.Empty;
            }
        }

        private void textBoxItemDes_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxItemDes.Text.Equals("请选择或输入"))
            {
                textBoxItemDes.Text = string.Empty;
            }
        }

        private void textBoxUnit_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxUnit.Text.Equals("请选择或输入"))
            {
                textBoxUnit.Text = string.Empty;
            }
        }
        #endregion

        private bool CheckUpdate()
        {
            if (_isUpdate) return true;
            if (!values[0].Equals(textBoxFtypeNmae.Text.Trim()))
            {
                return true;
            }
            if (!values[1].Equals(textBoxSampleNum.Text.Trim()))
            {
                return true;
            }
            if (!values[2].Equals(textBoxName.Text.Trim()))
            {
                return true;
            }
            if (!values[3].Equals(textBoxItemDes.Text.Trim()))
            {
                return true;
            }
            if (!values[4].Equals(textBoxStandardValue.Text.Trim()))
            {
                return true;
            }
            if (!values[5].Equals(textBoxDemarcate.Text.Trim()))
            {
                return true;
            }
            if (!values[6].Equals(textBoxUnit.Text.Trim()))
            {
                return true;
            }
            return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _isUpdate = CheckUpdate();
            if (_isUpdate)
            {
                if (MessageBox.Show("是否保存当前内容?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    btnSave_Click(null, null);
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