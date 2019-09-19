using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchSample.xaml 的交互逻辑
    /// </summary>
    public partial class SearchSample : Window
    {
        public SearchSample()
        {
            InitializeComponent();
        }

        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        public string _projectName = "";
        private bool _IsFirst = true;
        private List<clsttStandardDecide> _Items;
        private string logType = "SearchSample-error";

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (Global.IsProject)
                SearchkProjectName();
            else
                SearchSampleByName();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsProject)
                {
                    this.Title = "查找项目";
                    this.label1.Content = "项目名称";
                    this.label1.Margin = new Thickness(21, 12, 0, 0);
                    this.Search.Margin = new Thickness(265, 5, 143, 0);
                    this.btnChoose.Margin = new Thickness(365, 5, 43, 0);
                    this.btnAdd.Visibility = Visibility.Collapsed;
                    this.DataGridRecord.Columns[0].Visibility = Visibility.Collapsed;
                    this.DataGridRecord.Columns[1].Visibility = Visibility.Collapsed;
                    this.DataGridRecord.Columns[2].Visibility = Visibility.Collapsed;
                    this.DataGridRecord.Columns[3].Header = "项目名称";
                    this.DataGridRecord.Columns[3].Width = 250;
                    this.DataGridRecord.Columns[4].Width = 110;
                    this.DataGridRecord.Columns[5].Width = 110;
                    this.SearchkProjectName();
                }
                else
                {
                    this.DataGridRecord.Columns[4].Visibility = Visibility.Collapsed;
                    this.DataGridRecord.Columns[5].Visibility = Visibility.Collapsed;
                    this.SearchSampleByName();
                }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            this.textBoxName.Focus();
        }

        /// <summary>
        /// 查询样品
        /// </summary>
        private void SearchSampleByName()
        {
            try
            {
                DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(textBoxName.Text.Trim(), _projectName, true, _IsFirst, 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    _Items = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    this.DataGridRecord.ItemsSource = dataTable.DefaultView;
                    _IsFirst = false;
                }
                else
                {
                    this.DataGridRecord.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 查询检测项目
        /// </summary>
        private void SearchkProjectName()
        {
            try
            {
                DataTable dataTable = _clsTaskOpr.GetProjectByName(textBoxName.Text.Trim());
                this.DataGridRecord.ItemsSource = (dataTable != null && dataTable.Rows.Count > 0) ? dataTable.DefaultView : null;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 双击获取DataGrid中的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (Global.IsProject)
                    {
                        Global.projectName = (DataGridRecord.SelectedItem as DataRowView).Row["ItemDes"].ToString();
                        Global.projectUnit = (DataGridRecord.SelectedItem as DataRowView).Row["Unit"].ToString();
                    }
                    else
                        Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["FtypeNmae"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
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
                    if (Global.IsProject)
                    {
                        Global.projectName = (DataGridRecord.SelectedItem as DataRowView).Row["ItemDes"].ToString();
                        Global.projectUnit = (DataGridRecord.SelectedItem as DataRowView).Row["Unit"].ToString();
                    }
                    else
                        Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["FtypeNmae"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Global.IsProject ? "未选择任何检测项目！" : "未选择任何样品！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (Global.IsProject)
                    this.SearchkProjectName();
                else
                    this.SearchSampleByName();
            }
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (Global.IsProject)
                this.SearchkProjectName();
            else
                this.SearchSampleByName();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateSample addWindow = new AddOrUpdateSample();
            try
            {
                addWindow.textBoxName.Text = addWindow._projectName = _projectName;
                addWindow.textBoxName.IsReadOnly = true;
                addWindow._ItemNames = _Items;
                addWindow._addOrUpdate = "ADD";
                addWindow.ShowDialog();
                this.SearchSampleByName();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectDataGrid();
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}