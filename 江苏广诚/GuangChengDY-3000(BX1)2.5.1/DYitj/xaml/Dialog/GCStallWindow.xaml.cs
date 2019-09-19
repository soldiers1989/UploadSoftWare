﻿using com.lvrenyang;
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
    /// GCStallWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GCStallWindow : Window
    {
        private clsCompanyOpr _clsCompanyOprBLL = new clsCompanyOpr();
        private string err = "";
        public string Companyname = "";
        public GCStallWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.stallNum = "";
            Global.stallIDCode = "";

            GetStall("comany='"+Companyname +"'");
        }
        private void GetStall(string name )
        {
            DataGridRecord.ItemsSource = null;
            if(name !="")
            {
                DataTable dt = _clsCompanyOprBLL.GetStall(name , "", out err);
                if(dt!=null && dt.Rows.Count >0)
                {
                    List<GCStall> items = (List<GCStall>)IListDataSet.DataTableToIList<GCStall>(dt, 1);
                    if (items != null)
                        DataGridRecord.ItemsSource = items;
                        
                }
            }
            else
            {
                DataTable dt = _clsCompanyOprBLL.GetStall("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<GCStall> items = (List<GCStall>)IListDataSet.DataTableToIList<GCStall>(dt, 1);
                    if (items != null)
                        DataGridRecord.ItemsSource = items;
                }
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (tb_stall.Text.Trim ()!="")
            {
                GetStall("comany='"+Companyname +"' and twhmc like '%" + tb_stall.Text.Trim() + "%'");
            }
            else
            {
                GetStall("comany='" + Companyname + "'");
            }
            
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridRecord.SelectedItems.Count ==0)
            {
                MessageBox.Show("请选中需要的记录！","提示",MessageBoxButton.OK ,MessageBoxImage.Warning );
                return;
            }
            GCStall Pitem = (GCStall)DataGridRecord.SelectedItems[0];
            if (Pitem != null)
            {
                Global.stallNum = Pitem.twhmc;
                Global.stallIDCode = Pitem.sid;
            }
                
            this.Close();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中需要的记录！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GCStall Pitem = (GCStall)DataGridRecord.SelectedItems[0];
            if (Pitem != null)
            {
                Global.stallNum = Pitem.twhmc;
                Global.stallIDCode = Pitem.sid;
            }
            this.Close();
        }

        private void tb_stall_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_stall.Text.Trim() != "")
            {
                GetStall("comany='" + Companyname + "' and twhmc like '%" + tb_stall.Text.Trim() + "%'");
            }
            else
            {
                GetStall("comany='" + Companyname + "'");
            }
        }
    }
}
