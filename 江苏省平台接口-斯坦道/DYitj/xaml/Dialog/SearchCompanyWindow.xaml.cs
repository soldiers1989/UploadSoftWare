using DYSeriesDataSet;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

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
            if (textBoxName.Text.Trim().ToString() != "")
            {
                DataTable dt = _company.GetAsDataTable("name Like '%" + textBoxName.Text.Trim().ToString() + "%'", string.Empty, 6);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
            else 
            {
                DataTable dt = _company.GetAsDataTable("", string.Empty, 6);
                this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            }
            
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["name"].ToString();
                    Global.Selectcompany = Global.CompanyName;
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

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["name"].ToString();
                    Global.Selectcompany = Global.CompanyName;
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
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
            textBoxName.Text = String.Empty;
            Search();
        }

    }
}