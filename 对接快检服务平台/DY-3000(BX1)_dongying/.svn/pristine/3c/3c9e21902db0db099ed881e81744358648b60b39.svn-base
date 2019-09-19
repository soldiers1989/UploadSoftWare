using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchCompanyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchCompanyWindow : Window
    {
        public SearchCompanyWindow()
        {
            InitializeComponent();
        }
        private clsCompanyOpr _company = new clsCompanyOpr();
        private string logType = "SearchCompanyWindow-error";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                DataTable dt = _company.GetAsDataTable("FULLNAME Like '%" + textBoxName.Text.Trim().ToString() + "%'", "", 6);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("查询数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["FullName"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择任何任务！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["FullName"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}