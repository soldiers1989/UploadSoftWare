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
    /// GCProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GCProjectWindow : Window
    {
        private clsCompanyOpr _clsCompanyOprBLL = new clsCompanyOpr();
        private string err = "";
        

        public GCProjectWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.departName = "";
            Global.departIDCode = "";
            GetDepart("");
        }

        private void GetDepart(string name)
        {
            if(name !="")
            {
                DataTable dt= _clsCompanyOprBLL.GetProject(name, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<GCProject> items = (List<GCProject>)IListDataSet.DataTableToIList<GCProject>(dt, 1);
                    DataGridRecord.ItemsSource = items;
                }
            }
            else
            {
                DataTable dt = _clsCompanyOprBLL.GetProject("", "", out err);
                if(dt!=null && dt.Rows.Count >0)
                {
                    List<GCProject> items = (List<GCProject>)IListDataSet.DataTableToIList<GCProject>(dt, 1);
                    DataGridRecord.ItemsSource = items;
                }
            }
 
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(DataGridRecord.SelectedItems.Count ==0)
            {
                MessageBox.Show("请选中需要的项目再单击选择！","操作提示",MessageBoxButton.OK ,MessageBoxImage.Warning );
                return;
            }
            GCProject Pitem = (GCProject)DataGridRecord.SelectedItems[0];
            if (Pitem != null)
            {
                Global.departIDCode = Pitem.did;
                Global.departName = Pitem.jgmc;
            }
                
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(tb_project.Text.Trim ()!="")
            {
                GetDepart("jgmc like '%" + tb_project.Text.Trim() + "%'");
            }
            else
            {
                GetDepart("");
            }
            
        }
   

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中需要的项目再单击选择！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GCProject Pitem = (GCProject)DataGridRecord.SelectedItems[0];
            if (Pitem!=null )
            {
                Global.departIDCode = Pitem.did;
                Global.departName = Pitem.jgmc;
            }
            this.Close();
        }
    }
}
