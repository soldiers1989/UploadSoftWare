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
using System.Data.OleDb;
using System.Net;
using System.Data;
using com.lvrenyang;
using DYSeriesDataSet;
using System.Collections.Generic;
using System.Windows.Input;

using System.Collections.Generic;

using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ShowItem.xaml 的交互逻辑
    /// </summary>
    public partial class ShowItem : Window
    {
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        public string _projectName = string.Empty;
        private bool _IsFirst = true;
        private List<clsttStandardDecide> _Items;
        private StringBuilder sb = new StringBuilder();

        private static readonly string password = string.Empty;
        private static readonly string dataName = "local.Mdb";
        private static readonly string getLocalDBPathString = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        private static readonly string defCnnString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source ={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", getLocalDBPathString, password);
        private static OleDbConnection conn = null;
        private static OleDbCommand cmd = null;
       
        public ShowItem()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SearchItemName();


        }
        private void SearchItemName()
        {
            Global.itenName = "";
            string errMsg = string.Empty;
            sb.Append("Select ID,itemName,itemCode From ZJCheckitem ");
            this.dataGrid1.DataContext = null;
            string[] cmd = new string[1] { sb.ToString() };
            string[] names = new string[1] { "ZJCheckitem" };
            DataTable dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ZJCheckitem"];
            if (dtbl != null)
            {
                dataGrid1.ItemsSource = dtbl.DefaultView;
                //List<clsZJItmes> ItemNames = (List<clsZJItmes>)IListDataSet.DataTableToIList<clsZJItmes>(dtbl, 1);
                //if (ItemNames != null && ItemNames.Count > 0)
                //{
                //    this.dataGrid1.ItemsSource = ItemNames;
                //    _IsFirst = false;
                //    //if (_ItemNames.Count <= 0)
                //    //    _ItemNames = ItemNames;
                //}
            }
            
        }
        
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            try
            {
                if (dataGrid1.SelectedItems.Count > 0)
                {
                    //if (Global.IsProject)
                    //{
                    Global.itenName = (dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();
                    Global.itemCode = (dataGrid1.SelectedItem as DataRowView).Row["itemCode"].ToString();
                    Global.IsTestC = 1;
                    //string d = (dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();
                    //MessageBox.Show(d);
                    //}
                    //else
                    //    Global.sampleName = (dataGrid1.SelectedItem as DataRowView).Row["FtypeNmae"].ToString();
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

        private void dataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        //单击确定选择检测项目
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
                //if (Global.IsProject)
                //{
                DataRowView dr = this.dataGrid1.SelectedItem as DataRowView;
                if (dr!=null)
                {
                    
                }
                Global.itenName = (this.dataGrid1.SelectedItem as DataRowView).Row["itemName"].ToString();

                Global.itemCode = (this.dataGrid1.SelectedItem as DataRowView).Row["itemCode"].ToString();
                Global.IsTestC = 1;
                //}
                //else
                //    Global.sampleName = (dataGrid1.SelectedItem as DataRowView).Row["FtypeNmae"].ToString();
                this.Close();
            }

        }
        
    }
}
