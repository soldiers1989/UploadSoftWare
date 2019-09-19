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
using AIO.AnHui;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchSampleType.xaml 的交互逻辑
    /// </summary>
    public partial class SearchSampleType : Window
    {
        public SearchSampleType()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            searchData();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            searchData();
        }

        private void searchData()
        {
            DataTable dataTable = DataOperation.GetSampleTypeByName(textBoxName.Text.Trim());
            this.DataGridRecord.ItemsSource = (dataTable != null && dataTable.Rows.Count > 0) ? dataTable.DefaultView : null;
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectDataGrid();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectDataGrid();
        }

        private void SelectDataGrid()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.projectName = (DataGridRecord.SelectedItem as DataRowView).Row["name"].ToString();
                    Global.projectUnit = (DataGridRecord.SelectedItem as DataRowView).Row["codeId"].ToString();
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

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchData();
        }
    }
}
