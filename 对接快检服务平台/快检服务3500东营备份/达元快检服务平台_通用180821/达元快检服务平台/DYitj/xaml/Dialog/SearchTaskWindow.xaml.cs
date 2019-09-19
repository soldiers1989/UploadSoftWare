using DYSeriesDataSet;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTaskWindow : Window
    {

        private clsTaskOpr _Tskbll = new clsTaskOpr();

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
            Search();
        }

        private void Search()
        {
            //DataTable dt = _Tskbll.GetAsDataTable("CPTITLE Like '%" + textBoxName.Text.Trim() + "%'", string.Empty, 1);
            DataTable dt = _Tskbll.GetQtask("", "", 0);
            this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["taskname"].ToString();
                    Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["taskid"].ToString();
                    //Global.Other = (DataGridRecord.SelectedItem as DataRowView).Row["PLANDETAIL"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择任何检测任务!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["taskname"].ToString();
                    Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["taskid"].ToString();
                    //Global.Other = (DataGridRecord.SelectedItem as DataRowView).Row["PLANDETAIL"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            TaskDisplay window = new TaskDisplay();
            window.ShowDialog();
            textBoxName.Text = String.Empty;
            Search();
        }

    }
}