using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Data.OleDb;

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
        public string _projectName = string.Empty;
        private bool _IsFirst = true;
        private List<clsttStandardDecide> _Items;

        #region 使用Win32函数隐藏Window关闭按钮
        //private const int GWL_STYLE = -16;
        //private const int WS_SYSMENU = 0x80000;
        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        //[DllImport("user32.dll")]
        //private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// 使窗口始终无法关闭
        /// 结合上面的Win32函数可使窗口无法关闭
        /// </summary>
        /// <param name="e">为true时无法强制关闭</param>
        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    e.Cancel = true;
        //}
        #endregion

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
                MessageBox.Show("异常:\n" + ex.Message);
            }
            this.textBoxName.Focus();
        }

        /// <summary>
        /// 查询样品
        /// </summary>
        private void SearchSampleByName()
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

        /// <summary>
        /// 查询检测项目
        /// </summary>
        private void SearchkProjectName()
        {
            DataTable dataTable = _clsTaskOpr.GetProjectByName(textBoxName.Text.Trim());
            this.DataGridRecord.ItemsSource = (dataTable != null && dataTable.Rows.Count > 0) ? dataTable.DefaultView : null;
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
                MessageBox.Show("异常:\n" + ex.Message);
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
                addWindow.Title = "新增国家检测标准";
                addWindow.textBoxName.Text = addWindow._projectName = _projectName;
                addWindow.textBoxName.IsReadOnly = true;
                addWindow._ItemNames = _Items;
                addWindow.ShowDialog();
                this.SearchSampleByName();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
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

        private void DataGridRecord_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }
}
