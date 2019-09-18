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
using DYSeriesDataSet.DataSentence.Kjc;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// BeiHaiSampleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BeiHaiSampleWindow : Window
    {
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private string err = "";
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        public BeiHaiSampleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BSearchSample("", "IsTest='否'");
        }
        private void BSearchSample(string where,string orderby)
        {
            try
            {
                dt = _Tskbll.GetTestData(where, orderby, 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<TestSamples> Items = (List<TestSamples>)IListDataSet.DataTableToIList<TestSamples>(dt, 1);
                    DataGridRecord.ItemsSource = Items;
                }
                else
                {
                    DataGridRecord.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSample_Click(object sender, RoutedEventArgs e)
        {
            sb.Length = 0;
            sb.AppendFormat("goodsName like '%{0}%'", TextBox_Sample.Text.Trim());
            BSearchSample(sb.ToString(), "IsTest='否'");
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    TestSamples tsample = (TestSamples)DataGridRecord.SelectedItems[0];

                    Global.sampleName = tsample.goodsName; //(DataGridRecord.SelectedItem as DataRowView).Row["goodsName"].ToString();//样品名称
                    Global.Bcompany = tsample.marketName;//(DataGridRecord.SelectedItem as DataRowView).Row["marketName"].ToString();//被检单位
                    Global.BproductID = tsample.productId;// (DataGridRecord.SelectedItem as DataRowView).Row["productId"].ToString();//样品ID
                    Global.BID = tsample.ID.ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请选择需要检测的样品","操作提示");
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
                    TestSamples tsample = (TestSamples)DataGridRecord.SelectedItems[0];

                    Global.sampleName = tsample.goodsName; //(DataGridRecord.SelectedItem as DataRowView).Row["goodsName"].ToString();//样品名称
                    Global.Bcompany = tsample.marketName;//(DataGridRecord.SelectedItem as DataRowView).Row["marketName"].ToString();//被检单位
                    Global.BproductID = tsample.productId;// (DataGridRecord.SelectedItem as DataRowView).Row["productId"].ToString();//样品ID
                    Global.BID = tsample.ID.ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请选择需要检测的样品", "操作提示");
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

            DataGridRow dataGridRow = e.Row;
            TestSamples dataRow = e.Row.Item as TestSamples;
            if (dataRow.IsTest == "是")
            {
                dataGridRow.Background = Brushes.YellowGreen;
            }
            else
            {
                dataGridRow.Background = Brushes.White;
            }
        }
    }
}
