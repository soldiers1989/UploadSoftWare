using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTaskWindow : Window
    {

        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private string logType = "SearchTaskWindow-error";

        public SearchTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SearchTask_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Search();
        }

        private void Search()
        {
            try
            {
                DataTable dt = _Tskbll.GetAsDataTable("CPTITLE Like '%" + textBoxName.Text.Trim() + "%'", "", 1);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["CPTITLE"].ToString();
                    Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["CPCODE"].ToString();
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
                    Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["CPTITLE"].ToString();
                    Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["CPCODE"].ToString();
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