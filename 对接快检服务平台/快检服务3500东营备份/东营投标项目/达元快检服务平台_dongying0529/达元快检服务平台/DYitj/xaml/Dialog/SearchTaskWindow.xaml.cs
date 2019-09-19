using DYSeriesDataSet;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Text;
using DYSeriesDataSet.DataModel;
using com.lvrenyang;
using System.Collections.Generic;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTaskWindow : Window
    {
        public string testitem = "";
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        private string errMsg = "";
        private string err = "";


        public SearchTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Search();
            SearchTestTask();
        }

        private void SearchTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sb.Length = 0;
                DataGridRecord.ItemsSource = null;
                if (textBoxName.Text.Trim() != "")
                {
                    sb.AppendFormat("t_task_title like '%{0}%' ", textBoxName.Text.Trim());
                    sb.AppendFormat(" and username='{0}'", Global.samplenameadapter[0].user);
                    sb.AppendFormat(" and d_item='{0}'", testitem);
                }

                dt = _bll.GetTestTask(sb.ToString(), 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsManageTask> Items = (List<clsManageTask>)IListDataSet.DataTableToIList<clsManageTask>(dt, 1);
                    if (Items.Count > 0)
                        DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Search();
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
                    clsManageTask mmt = (clsManageTask)DataGridRecord.SelectedItems[0];
                    Global.TaskName = mmt.t_task_title;
                    Global.TaskCode = mmt.t_id;
                    Global.sampleName = mmt.d_sample;
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
                    clsManageTask mmt = (clsManageTask)DataGridRecord.SelectedItems[0];
                    Global.TaskName = mmt.t_task_title;
                    Global.TaskCode = mmt.t_id;
                    Global.sampleName = mmt.d_sample;
                    //Global.TaskName = (DataGridRecord.SelectedItem as DataRowView).Row["taskname"].ToString();
                    //Global.TaskCode = (DataGridRecord.SelectedItem as DataRowView).Row["taskid"].ToString();
                    //Global.Other = (DataGridRecord.SelectedItem as DataRowView).Row["PLANDETAIL"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请选择需要检测的任务记录","系统提示");
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
            TaskManageWindow window = new TaskManageWindow();
            //TaskDisplay window = new TaskDisplay();
            window.ShowDialog();
            textBoxName.Text = String.Empty;
            //Search();
            SearchTestTask();
        }
        /// <summary>
        /// 查询任务
        /// </summary>
        private void SearchTestTask()
        {
            try
            {
                DataGridRecord.ItemsSource = null;
                sb.Length = 0;
                sb.AppendFormat("username='{0}'", Global.samplenameadapter[0].user);
                sb.AppendFormat(" and d_item='{0}'", testitem);
                dt = _bll.GetTestTask(sb.ToString(), 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsManageTask> Items = (List<clsManageTask>)IListDataSet.DataTableToIList<clsManageTask>(dt, 1);
                    if (Items.Count > 0)
                        DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}