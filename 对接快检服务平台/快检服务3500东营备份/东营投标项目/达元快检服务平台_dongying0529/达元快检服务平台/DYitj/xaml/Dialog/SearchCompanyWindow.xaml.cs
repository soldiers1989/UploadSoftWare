using DYSeriesDataSet;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DYSeriesDataSet.DataSentence.Kjc;
using System.Collections.Generic;
using DYSeriesDataSet.DataSentence;
using com.lvrenyang;
using System.Text;

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
            //if (Global.InterfaceType.Equals("KJC"))
            //{
            //    DataGridRecord_kjc.Visibility = Visibility.Visible;
            //    DataGridRecord.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    DataGridRecord.Visibility = Visibility.Visible;
            //    DataGridRecord_kjc.Visibility = Visibility.Collapsed;
            //}
        }
        private clsCompanyOpr _company = new clsCompanyOpr();
        private StringBuilder sb = new StringBuilder();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            //Search();
            sb.Length = 0;
            sb.AppendFormat(" r.reg_name like '%{0}%'",textBoxName.Text.Trim());
            DataTable dt = _company.GetAsDataTable(sb.ToString(), string.Empty, 14);
            this.DataGridRecord_kjc.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Search();
        }

        string errMsg = string.Empty;
        private void Search()
        {
            if (Global.InterfaceType.Equals("KJC"))
            {
                this.DataGridRecord_kjc.DataContext = null;
                string where = string.Empty;
                if (textBoxName.Text.Trim().Length > 0)
                {
                    where = string.Format("regName like '%{0}%'", textBoxName.Text.Trim());
                }
                DataTable dtbl = kjcCompanyBLL.GetDataTable(out errMsg, 0, where);
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    DataGridRecord_kjc.DataContext = dtbl;
                }
            }
            else
            {
                DataTable dt = _company.GetAsDataTable("", string.Empty, 13);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    List<regulator> ItemNames = (List<regulator>)IListDataSet.DataTableToIList<regulator>(dt, 1);
                //    if (ItemNames.Count > 0)
                //        this.DataGridRecord_kjc.DataContext = ItemNames;
                //}
                this.DataGridRecord_kjc.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.InterfaceType.Equals("KJC"))
                {
                    if (DataGridRecord_kjc.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord_kjc.SelectedItem as DataRowView).Row["regName"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何被检单位!", "操作提示");
                    }
                }
                else
                {
                    if (DataGridRecord_kjc.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord_kjc.SelectedItem as DataRowView).Row["reg_name"].ToString();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择任何被检单位!", "操作提示");
                    }
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
                if (Global.InterfaceType.Equals("KJC"))
                {
                    if (DataGridRecord_kjc.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord_kjc.SelectedItem as DataRowView).Row["reg_name"].ToString();
                        this.Close();
                    }
                }
                else
                {
                    if (DataGridRecord_kjc.SelectedItems.Count > 0)
                    {
                        Global.CompanyName = (DataGridRecord_kjc.SelectedItem as DataRowView).Row["reg_name"].ToString();
                        this.Close();
                    }
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
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
            textBoxName.Text = String.Empty;
            Search();
        }

    }
}