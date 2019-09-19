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
    /// SampleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SampleWindow : Window
    {
        private clsCompanyOpr _clsCompanyOprBLL = new clsCompanyOpr();
        public SampleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.sampleName = "";
            try 
            {
                DataTable dt= _clsCompanyOprBLL.GetYCSampleTable("");
                if(dt!=null && dt.Rows.Count >0)
                {
                    DataGridRecord.DataContext = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["goodsName"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRecord.DataContext = "";
                string where = "";
                if(textBoxName.Text.Trim ()!="")
                {
                    where = "goodsName like '%" + textBoxName.Text.Trim()+"%'";
                }
                DataTable dt = _clsCompanyOprBLL.GetYCSampleTable(where);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataGridRecord.DataContext = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["goodsName"].ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            this.Close();
        }
    }
}
