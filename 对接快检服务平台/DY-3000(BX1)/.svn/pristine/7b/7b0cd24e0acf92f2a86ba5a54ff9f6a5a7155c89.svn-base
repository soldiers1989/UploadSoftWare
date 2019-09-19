using DYSeriesDataSet;
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

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchOperatorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchOperatorWindow : Window
    {
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private string errMsg = "";

        public string marketname = "";
        public SearchOperatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.OpertorName = "";
            Global.OpertorID = "";
            SearchOperator(marketname,"");
        }

        private void SearchOperator(string name,string opr)
        {
            DataGridRecord.ItemsSource = null;
            if (opr != "")
            {
                DataTable dt = _bll.GetCompany(" r.rid=b.reg_id and r.checked='1' and r.delete_flag='0' and b.checked='1' and b.delete_flag='0' and r.reg_name='" + marketname + "'" + " and ope_name like '%" + opr+"%'", "", 1, out errMsg);
                DataGridRecord.ItemsSource = dt.DefaultView;
            }
            else
            {
                DataTable dt = _bll.GetCompany(" r.rid=b.reg_id and r.checked='1' and r.delete_flag='0' and b.checked='1' and b.delete_flag='0' and r.reg_name='" + marketname + "'", "", 1, out errMsg);
                DataGridRecord.ItemsSource = dt.DefaultView;
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex()+1;
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(DataGridRecord.SelectedItems.Count >0)
            {
                Global.OpertorName = (DataGridRecord.SelectedItem as DataRowView).Row["ope_name"].ToString();
                Global.OpertorID = (DataGridRecord.SelectedItem as DataRowView).Row["bid"].ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择需要的记录再单击选择！");
            }
            
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Global.OpertorName = (DataGridRecord.SelectedItem as DataRowView).Row["ope_name"].ToString();
                Global.OpertorID  = (DataGridRecord.SelectedItem as DataRowView).Row["bid"].ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择需要的记录再单击选择！");
            }
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxName.Text.Trim()!=null )
            {
                SearchOperator(marketname, textBoxName.Text.Trim());
            }
            else
            {
                SearchOperator(marketname, "");
            }
           
        }
    }
}
