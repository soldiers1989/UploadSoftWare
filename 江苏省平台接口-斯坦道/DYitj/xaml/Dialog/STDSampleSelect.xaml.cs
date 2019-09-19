using System;
using System.Collections.Generic;
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
using System.Data;
using DYSeriesDataSet;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// STDSample.xaml 的交互逻辑
    /// </summary>
    public partial class STDSampleSelect : Window
    {
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        public string _projectName = string.Empty;
        public int _sel = 0;
        //private bool _IsFirst = true;
        private List<clsttStandardDecide> _Items;
        public STDSampleSelect()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode("", _projectName, false, false, 1);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                _Items = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
            }
            SearchSampleByName();
        }
        /// <summary>
        /// 查询样品
        /// </summary>
        private void SearchSampleByName()
        {
            DataTable dataTable = _clsTaskOpr.GetSTDSampleCode(textBoxName.Text.Trim(), _projectName, false, false , 1);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //_Items = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                this.DataGridRecord.ItemsSource = dataTable.DefaultView;
                //_IsFirst = false;
            }
            else
            {
                this.DataGridRecord.ItemsSource = null;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchSampleByName();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectDataGrid();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectDataGrid();
        }
        /// <summary>
        /// 选择样品或项目
        /// </summary>
        private void SelectDataGrid()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["productName"].ToString();

                    DataTable dt = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, _projectName, false, false, 1);

                    this.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        if (MessageBox.Show("本地不存在‘" + Global.sampleName + "’样品的对应检测标准，是否新增？", "提示", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                        {
                           
                            AddOrUpdateSample addWindow = new AddOrUpdateSample();
                            try
                            {
                                addWindow.Title = "新增国家检测标准";
                                addWindow.textBoxName.Text = addWindow._projectName = _projectName;
                                addWindow.textBoxName.IsReadOnly = true;
                                addWindow._ItemNames = _Items;
                                addWindow._sampleName = Global.sampleName;
                                if (_Items != null)
                                {
                                    addWindow._standardvalue = _Items[_sel].StandardValue;
                                }
                                addWindow.ShowDialog();
                                //this.SearchSampleByName();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("异常:\n" + ex.Message);
                            }

                        }
                        else
                        {
                            Global.sampleName = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Global.IsProject ? "未选择任何检测项目!" : "未选择任何样品!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
