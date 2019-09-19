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
using com.lvrenyang;
using DYSeriesDataSet.DataSentence;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ShowDownItem.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDownItem : Window
    {
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private StringBuilder sb = new StringBuilder();
        public ShowDownItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sb.Length = 0;
            string err = "";
            Global.itenName = "";
            Global.itemCode = "";
            Global.IsTestC = 0;

            DataTable dt = _resultTable.GetDownItem("", "", out err);
            if (dt != null)
            {
                dataGrid1.ItemsSource = dt.DefaultView;
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
                DataRowView dr = this.dataGrid1.SelectedItem as DataRowView;
               
                Global.itenName = (this.dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();

                Global.itemCode = (this.dataGrid1.SelectedItem as DataRowView).Row["itemCode"].ToString();
                Global.IsTestC = 1;

                this.Close();
            }
            else
            {
                MessageBox.Show("请选择需要的项目再单击选择","系统提示");
            }

        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataGrid1.SelectedItems.Count > 0)
                {
                    Global.itenName = (dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();
                    Global.itemCode = (dataGrid1.SelectedItem as DataRowView).Row["itemCode"].ToString();
                    Global.IsTestC = 1;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            sb.Length = 0;
            string err = "";
            dataGrid1.ItemsSource = null;
            if (TxtItemName.Text.Trim() != "")
            {
                sb.AppendFormat("itemName like '%{0}%' ", TxtItemName.Text.Trim());
                DataTable dt = _resultTable.GetDownItem(sb.ToString(), "", out err);
               
                if (dt != null && dt.Rows.Count>0)
                {
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
            }
            else
            {
                DataTable dt = _resultTable.GetDownItem("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
            }

           
        }
    }
}
