using System;
using System.Collections;
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
        /// <summary>
        /// 在本地数据库中未找到时新增的样品名称
        /// </summary>
        public string _sampleName = string.Empty;
        public string _addOrUpdate = string.Empty;
        public clsttStandardDecide _decide = new clsttStandardDecide();
        public List<clsttStandardDecide> _ItemNames = new List<clsttStandardDecide>();
        private clsttStandardDecideOpr _bll = new clsttStandardDecideOpr();
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private bool _isUpdate = false;
        private bool _isClose = true;
        private List<string> values = new List<string>();
        private List<clsttStandardDecide> _itemList = null;
        private string logType = "AddOrUpdateSample-error";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sampleName.Length > 0)
                {
                    this.textBoxFtypeNmae.Text = _sampleName;
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
                this.Title = _addOrUpdate.Equals("ADD") ? "新增 - 样品" : "编辑 - 样品";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 保存样品
        /// </summary>
        private void Save()
        {
            clsttStandardDecide model = new clsttStandardDecide();
            string err = string.Empty;
            if (checkSave())
            {
                try
                {
                    if (_decide != null && _decide.ID > 0)
                    {
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
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("保存数据时发生异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                }
                finally
                {
                    if (err.Equals("") && _addOrUpdate.Equals("ADD"))
                    {
                        _isUpdate = false;
                        if (MessageBox.Show("保存成功!\n是否继续添加样品?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            _isClose = false;
                            textBoxFtypeNmae.Text = "";
                            textBoxSampleNum.Text = "";
                            textBoxName.Text = _projectName.Equals("") ? "" : _projectName;
                            textBoxItemDes.Text = "";
                            textBoxStandardValue.Text = "";
                            textBoxDemarcate.Text = "";
                            textBoxUnit.Text = "";
                            _decide = new clsttStandardDecide();
                        }
                        else
                        {
                            _isClose = true;
                            _isUpdate = false;
                            try { this.Close(); }
                            catch (Exception) { }
                        }
                    }
                    else
                    {
                        MessageBox.Show("编辑成功！", "操作提示");
                        _isUpdate = false;
                        try { this.Close(); }
                        catch (Exception ex) { FileUtils.OprLog(6, logType, ex.ToString()); }
                    }
                }
            }
        }

        /// <summary>
        /// 验证信息完整性
        /// </summary>
        /// <returns></returns>
        private bool checkSave()
        {
            try
            {
                string str = "", strT = "请选择或输入";
                str = textBoxFtypeNmae.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("样品名称不能为空！", "操作提示");
                    textBoxFtypeNmae.Text = "";
                    textBoxFtypeNmae.Focus();
                    return false;
                }
                str = textBoxName.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("项目名称不能为空！", "操作提示");
                    textBoxName.Text = "";
                    textBoxName.Focus();
                    return false;
                }
                str = textBoxItemDes.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("标准名称不能为空！", "操作提示");
                    textBoxItemDes.Text = "";
                    textBoxItemDes.Focus();
                    return false;
                }
                str = textBoxStandardValue.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("检测标准值不能为空！", "操作提示");
                    textBoxStandardValue.Text = "";
                    textBoxStandardValue.Focus();
                    return false;
                }
                str = textBoxDemarcate.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("判定符号不能为空！", "操作提示");
                    textBoxDemarcate.Text = "";
                    textBoxDemarcate.Focus();
                    return false;
                }
                str = textBoxUnit.Text.Trim();
                if (str.Equals("") || str.Equals(strT))
                {
                    MessageBox.Show("单位不能为空！", "操作提示");
                    textBoxUnit.Text = "";
                    textBoxUnit.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            return true;
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
                DataTable dataTable = new clsTaskOpr().GetSampleByNameOrCode("", "", false, false, 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    _itemList = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    if (_itemList != null && _itemList.Count > 0)
                    {
                        for (int i = 0; i < _itemList.Count; i++)
                        {
                            string str = _itemList[i].FtypeNmae;
                            if (!dicSampleName.ContainsKey(str) && !str.Equals(""))
                            {
                                dicSampleName.Add(str, str);
                                SampleNameList.Add(str);
                            }
                            str = _itemList[i].Unit;
                            if (!str.Equals("") && !dicUnit.ContainsKey(str))
                            {
                                dicUnit.Add(str, str);
                                UnitList.Add(str);
                            }
                            str = _itemList[i].Demarcate;
                            if (!str.Equals("") && !dicDemarcate.ContainsKey(str))
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
                    if (!dicItemDes.ContainsKey(str) && !str.Equals(""))
                    {
                        dicItemDes.Add(str, str);
                        ItemDesList.Add(str);
                    }
                    str = _ItemNames[i].Name;
                    if (!dicItems.ContainsKey(str) && !str.Equals(""))
                    {
                        dicItems.Add(str, str);
                        ItemsList.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            finally
            {
                this.textBoxFtypeNmae.ItemsSource = SampleNameList;
                this.textBoxName.ItemsSource = ItemsList;
                this.textBoxItemDes.ItemsSource = ItemDesList;
                this.textBoxDemarcate.ItemsSource = DemarcateList;
                this.textBoxUnit.ItemsSource = UnitList;
                if (_projectName.Length > 0)
                {
                    this.textBoxDemarcate.Text = _ItemNames[0].Demarcate;
                    this.textBoxItemDes.Text = _ItemNames[0].ItemDes;
                    this.textBoxStandardValue.Text = _ItemNames[0].StandardValue;
                    this.textBoxUnit.Text = _ItemNames[0].Unit;
                }
            }
        }

        private void textBoxStandardValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            _isUpdate = true;
        }

        #region
        private void textBoxFtypeNmae_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFtypeNmae.Text.Trim().Equals(""))
            {
                textBoxFtypeNmae.Text = "请选择或输入";
            }
        }

        private void textBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            string val = textBoxName.Text.Trim();
            if (val.Length == 0)
            {
                textBoxName.Text = "请选择或输入";
                return;
            }

            if (_itemList == null || _itemList.Count == 0) return;

            try
            {
                for (int i = 0; i < _itemList.Count; i++)
                {
                    if (_itemList[i].Name.Equals(val))
                    {
                        this.textBoxItemDes.Text = _itemList[i].ItemDes;
                        this.textBoxStandardValue.Text = _itemList[i].StandardValue;
                        this.textBoxDemarcate.Text = _itemList[i].Demarcate;
                        this.textBoxUnit.Text = _itemList[i].Unit;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void textBoxItemDes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxItemDes.Text.Trim().Equals(""))
            {
                textBoxItemDes.Text = "请选择或输入";
            }
        }

        private void textBoxUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxUnit.Text.Trim().Equals(""))
            {
                textBoxUnit.Text = "请选择或输入";
            }
        }

        private void textBoxFtypeNmae_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxFtypeNmae.Text.Trim().Equals("请选择或输入"))
            {
                textBoxFtypeNmae.Text = "";
            }
        }

        private void textBoxName_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxName.Text.Equals("请选择或输入"))
            {
                textBoxName.Text = "";
            }
        }

        private void textBoxItemDes_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxItemDes.Text.Equals("请选择或输入"))
            {
                textBoxItemDes.Text = "";
            }
        }

        private void textBoxUnit_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (textBoxUnit.Text.Equals("请选择或输入"))
            {
                textBoxUnit.Text = "";
            }
        }
        #endregion

        private bool checkUpdate()
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
            _isUpdate = !_isClose ? checkUpdate() : false;
            if (_isUpdate)
            {
                if (MessageBox.Show("是否保存当前内容?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    btnSave_Click(null, null);
                    _isUpdate = false;
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

        private void textBoxFtypeNmae_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxItemDes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxDemarcate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isUpdate = true;
        }

    }
}