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

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ManageStall.xaml 的交互逻辑
    /// </summary>
    public partial class ManageStall : Window
    {
        private clsCompanyOpr _company = new clsCompanyOpr();
        public ManageStall()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void Search()
        {
            if (textBoxName.Text.Trim().ToString() != "")
            {
                DataTable dt = _company.GetAsDataTable("enterpriseName='" + Global.Selectcompany + "' and stallNumber" + " Like '%" + textBoxName.Text.Trim().ToString() + "%'", string.Empty, 13);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
            else
            {
                DataTable dt = _company.GetAsDataTable("enterpriseName='" + Global.Selectcompany + "'", string.Empty, 13);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }

        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.companystallNum = (DataGridRecord.SelectedItem as DataRowView).Row["stallNumber"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择任何被检单位!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.companystallNum = (DataGridRecord.SelectedItem as DataRowView).Row["stallNumber"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
