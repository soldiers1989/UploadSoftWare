﻿using DYSeriesDataSet;
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
    /// OperatorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private clsCompanyOpr _clscompany = new clsCompanyOpr();
        public string marketname = "";
        public OperatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string where = "marketName ='" + marketname + "'"; 
            DataTable dt = _clscompany.GetYCOpertorTable(where);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataGridRecord.DataContext = dt;
            }
        }
        private void Searchitem()
        {
            try
            {
                DataGridRecord.DataContext = null;
                string where = "";
                if(textBoxName.Text.Trim ()!="")
                {
                    where = "operatorName like '%" + textBoxName.Text.Trim()+"%'";
                }
                if(where .Length >0)
                {
                    where += " and operatorName like '%" + textBoxName.Text.Trim() + "%'";
                }
                else
                {
                    where = "marketName ='" + marketname + "'"; 
                }
                DataTable dt = _clscompany.GetYCOpertorTable(where);
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

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Global.Opertorname = (DataGridRecord.SelectedItem as DataRowView).Row["operatorName"].ToString();
               
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择需要的记录再单击选择!", "操作提示");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Searchitem();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Global.Opertorname = (DataGridRecord.SelectedItem as DataRowView).Row["operatorName"].ToString();
               
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择需要的记录再单击选择!", "操作提示");
            }
       
        }
    }
}
