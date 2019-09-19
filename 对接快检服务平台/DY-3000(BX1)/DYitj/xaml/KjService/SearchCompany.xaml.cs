using DYSeriesDataSet;
using DYSeriesDataSet.DataSentence;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AIO.xaml.KjService
{
    /// <summary>
    /// SearchCompany.xaml 的交互逻辑
    /// </summary>
    public partial class SearchCompany : Window
    {
        private clsCompanyOpr _company = new clsCompanyOpr();
        private StringBuilder sb = new StringBuilder();
        public SearchCompany()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }
        private void Search()
        {
            string errMsg = "";
            DataGridRecord_kjc.ItemsSource = null;
            DataTable dt = _company.GetAsDataTable(" checked='1' and delete_flag='0' and depart_id='"+Global.depart_id +"'", string.Empty, 13);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    List<regulator> ItemNames = (List<regulator>)IListDataSet.DataTableToIList<regulator>(dt, 1);
            //    if (ItemNames.Count > 0)
            //        this.DataGridRecord_kjc.DataContext = ItemNames;
            //}
            this.DataGridRecord_kjc.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
           
        }
        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            //Search();
            sb.Length = 0;
            sb.AppendFormat(" r.reg_name like '%{0}%' and checked='1' and delete_flag='0' and depart_id='" + Global.depart_id + "'", textBoxName.Text.Trim());
            DataTable dt = _company.GetAsDataTable(sb.ToString(), string.Empty, 14);
            this.DataGridRecord_kjc.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
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
            KCompany window = new KCompany();
            //SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
            textBoxName.Text = String.Empty;
            Search();
        }
        
    }
}
