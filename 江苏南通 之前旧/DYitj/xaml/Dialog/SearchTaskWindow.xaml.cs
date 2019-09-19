using DYSeriesDataSet;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DYSeriesDataSet.DataModel;
using System.Collections.Generic;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTaskWindow : Window
    {

        private clsTaskOpr _Tskbll = new clsTaskOpr();
        public string _item = string.Empty;
        public SearchTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Search();
            searchNTtask();
        }

        private void searchNTtask()
        {
            this.DataGridRecord.ItemsSource = null;
            DataTable dt = _Tskbll.GetNTtask("istest='否' and ItemName='"+_item +"'", string.Empty, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<clsNTtaskresult> Items = (List<clsNTtaskresult>)IListDataSet.DataTableToIList<clsNTtaskresult>(dt, 1);
                this.DataGridRecord.ItemsSource = (Items != null && Items.Count > 0) ? Items : null;
            }
            else
                this.DataGridRecord.ItemsSource = null;
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
            //this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            searchNTtask();
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    //Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["CPTITLE"].ToString();
                    //Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["CPCODE"].ToString();
                    //Global.Other = (DataGridRecord.SelectedItem as DataRowView).Row["PLANDETAIL"].ToString();
                    Global.NTsample = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleName;
                    Global.NTitem = (DataGridRecord.SelectedItem as clsNTtaskresult).ItemName;
                    Global.NTsamplecode = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleID;

                    Global._selSampleID = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleID;
                    Global._selSamplename = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleName;
                    Global._itemname = (DataGridRecord.SelectedItem as clsNTtaskresult).ItemName;
                    Global._tasktime = (DataGridRecord.SelectedItem as clsNTtaskresult).tasktime;

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
                    Global.NTsample = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleName;
                    Global.NTitem = (DataGridRecord.SelectedItem as clsNTtaskresult).ItemName;
                    Global.NTsamplecode = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleID;

                    Global._selSampleID = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleID;
                    Global._selSamplename = (DataGridRecord.SelectedItem as clsNTtaskresult).SampleName;
                    Global._itemname = (DataGridRecord.SelectedItem as clsNTtaskresult).ItemName;
                    Global._tasktime = (DataGridRecord.SelectedItem as clsNTtaskresult).tasktime;
                    //Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["CPTITLE"].ToString();
                    //Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["CPCODE"].ToString();
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