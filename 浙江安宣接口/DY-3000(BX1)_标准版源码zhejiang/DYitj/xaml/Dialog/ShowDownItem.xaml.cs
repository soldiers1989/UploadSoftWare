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
using DYSeriesDataSet.DataSentence;
using System.Data;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ShowDownItem.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDownItem : Window
    {
        private clsDownChkItem downItem = new clsDownChkItem();
        private StringBuilder sb = new StringBuilder();
        public ShowDownItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sb.Length =0;
            string err="";

            DataTable dt = downItem.GetDownItem("", "", out err);
            if (dt != null)
            {
                dataGrid1.ItemsSource = dt.DefaultView;
            }
        }
        /// <summary>
        /// GridView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Datagrid选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
               
                DataRowView dr = this.dataGrid1.SelectedItem as DataRowView;
                if (dr != null)
                {

                }
                Global.itenName = (this.dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();

                Global.itemCode = (this.dataGrid1.SelectedItem as DataRowView).Row["itemCode"].ToString();
                Global.IsTestC = 1;
              
                this.Close();
            }

        }
    }
}
