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
    /// selectTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class selectTaskWindow : Window
    {
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        public string _projectName = "";
 
       
        public selectTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.sampleName = "";
                Global.sampleCode = "";
                Global.TaskID = "";
                if (_projectName!="")
                {
                    SearchTask("checkItem='" + _projectName+"'");
                }
                else
                {
                    SearchTask("");
                }
               
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxName.Text.Trim()!="")
                {
                    SearchTask("foodName like '%" + textBoxName.Text.Trim() + "%'");
                }
                else
                {
                    SearchTask("");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SearchTask(string where)
        {
            this.DataGridRecord.ItemsSource = null;

            DataTable dt = _Tskbll.GetAsDataTable(where, string.Empty, 4);
            if (dt != null && dt.Rows.Count > 0)
            {
                //List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                List<downsample> Items = (List<downsample>)IListDataSet.DataTableToIList<downsample>(dt, 1);
                this.DataGridRecord.ItemsSource = Items;
                if (DataGridRecord.Items.Count > 0)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(ProcessRows));

                }
            }
            else
                this.DataGridRecord.ItemsSource = null;
        }
        private void ProcessRows()
        {

            List<downsample> Items = new List<downsample>();
            //获取单元行
            try
            {
                for (int i = 0; i < DataGridRecord.Items.Count; i++)
                {
                    Items.Add((downsample)DataGridRecord.Items[i]);
                    var row = DataGridRecord.ItemContainerGenerator.ContainerFromItem(DataGridRecord.Items[i]) as DataGridRow;
                    if (row != null)
                    {
                        if (Items[i].isTest.Equals("是"))
                            row.Background = new SolidColorBrush(Colors.Red);

                        ////获取单元格
                        //DataRowView drv = DataGridRecord.Items[i] as DataRowView;
                        //if (Items[i].Result != null && Items[i].Result.Equals("不合格"))
                        //{
                        //    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                        //    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(9);
                        //    cell.Background = new SolidColorBrush(Colors.Red);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(DataGridRecord.SelectedItems.Count ==0)
                {
                    MessageBox.Show("请选择需要的记录再单击选择！","提示",MessageBoxButton.OK ,MessageBoxImage.Information );
                    return;
                }
                downsample  ds=(downsample)DataGridRecord.SelectedItems[0];
                Global.sampleName = ds.foodName;
                Global.sampleCode  = ds.sampleNO;
                Global.CompanyName = ds.cdName;
                Global.TaskID = ds.ID.ToString ();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskDisplay window = new TaskDisplay();
                window.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count == 0)
                {
                    MessageBox.Show("请选择需要的记录再单击选择！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                downsample ds = (downsample)DataGridRecord.SelectedItems[0];
                Global.sampleName = ds.foodName;
                Global.sampleCode = ds.sampleNO;
                Global.CompanyName = ds.cdName;
                Global.TaskID = ds.ID.ToString();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
