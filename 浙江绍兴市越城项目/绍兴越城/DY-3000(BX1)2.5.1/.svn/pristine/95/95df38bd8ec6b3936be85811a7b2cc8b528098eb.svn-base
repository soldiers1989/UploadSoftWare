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
                if (Global.marketCheck)
                {
                    DataGridSelf.Visibility = Visibility.Visible;
                    DataGridRecord.Visibility = Visibility.Collapsed;

                    if (textBoxName.Text.Trim() != "")
                    {
                        DataTable dt = _company.GetAsDataTable("marketName Like '%" + textBoxName.Text.Trim().ToString() + "%'", string.Empty, 14);
                        this.DataGridSelf.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
                    }
                    else
                    {
                        DataTable dt = _company.GetAsDataTable("", string.Empty, 14);
                        this.DataGridSelf.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
                    }
                }
                else
                {
                    DataGridSelf.Visibility = Visibility.Collapsed ;
                    DataGridRecord.Visibility = Visibility.Visible;

                    if (textBoxName.Text.Trim() != "")
                    {
                        DataTable dt = _company.GetAsDataTable("marketName Like '%" + textBoxName.Text.Trim().ToString() + "%'", string.Empty, 13);
                        this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
                    }
                    else
                    {
                        DataTable dt = _company.GetAsDataTable("", string.Empty, 13);
                        this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
                    }
                }
               
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
                if(Global.marketCheck )
                {
                    if (DataGridSelf.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridSelf.SelectedItem as DataRowView).Row["marketName"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何任务！", "操作提示");
                    }
                }
                else
                {
                    if (DataGridRecord.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["marketName"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何任务！", "操作提示");
                    }
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
                if (Global.marketCheck)
                {
                    if (DataGridSelf.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridSelf.SelectedItem as DataRowView).Row["marketName"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何任务！", "操作提示");
                    }
                }
                else
                {
                    if (DataGridRecord.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["marketName"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何任务！", "操作提示");
                    }
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