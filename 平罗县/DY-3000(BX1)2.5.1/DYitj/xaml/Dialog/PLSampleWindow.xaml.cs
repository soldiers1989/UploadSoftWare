using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
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
    /// PLSampleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PLSampleWindow : Window
    {
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private List<PLSample> _Items;
        public PLSampleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchSample("");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void SearchSample(string name)
        {
            DataGridRecord.ItemsSource = null;
            string where = "";
            if(name !="")
            {
                where = string.Format("samplename like '%{0}%'", name);
            }
            DataTable dt = _clsTaskOpr.GetAsDataTable(where, "", 4);
            if(dt!=null && dt.Rows.Count >0)
            {
                _Items = (List<PLSample>)IListDataSet.DataTableToIList<PLSample>(dt, 1);
                DataGridRecord.ItemsSource = _Items;
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchSample(textBoxName.Text.Trim());
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
                Global.sampleName = "";
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    PLSample psamle = (PLSample)DataGridRecord.SelectedItems[0];
                    if(psamle !=null )
                    {
                        Global.sampleName = psamle.samplename;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何样品！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SearchSample(textBoxName.Text.Trim());
            }
            catch (Exception ex)
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
                Global.sampleName = "";
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    PLSample psamle = (PLSample)DataGridRecord.SelectedItems[0];
                    if (psamle != null)
                    {
                        Global.sampleName = psamle.samplename;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何样品！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
