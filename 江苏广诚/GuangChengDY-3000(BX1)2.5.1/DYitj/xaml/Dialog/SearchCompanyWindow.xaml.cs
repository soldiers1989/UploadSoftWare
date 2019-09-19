﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using com.lvrenyang;
using DYSeriesDataSet;
using System.Collections.Generic;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchCompanyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchCompanyWindow : Window
    {
        public SearchCompanyWindow()
        {
            InitializeComponent();
        }
        private clsCompanyOpr _company = new clsCompanyOpr();
        private string logType = "SearchCompanyWindow-error";
        private string err = "";

        public string projectName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (projectName != "")
                Search("proect='"+projectName +"'");
            //else
            //    Search("");
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            Search( "proect='"+projectName +"' and bjcdwmc like '%" + textBoxName.Text.Trim()+"'%");
        }

        private void textBoxName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Search( "proect='"+projectName +"' and bjcdwmc like '%" + textBoxName.Text.Trim() + "%'");
        }

        private void Search(string name)
        {
            try
            {
                DataGridRecord.ItemsSource = null;
                //DataTable dt = _company.GetAsDataTable("FULLNAME Like '%" + textBoxName.Text.Trim().ToString() + "%'", "", 6);
                if(name !="")
                {
                    DataTable dt = _company.GetCompany(name, "", out err);
                    if(dt!=null && dt.Rows.Count >0)
                    {
                        List<GCCompany> items = (List<GCCompany>)IListDataSet.DataTableToIList<GCCompany>(dt, 1);
                        if (items != null)
                            DataGridRecord.ItemsSource = items;
                    }
                  
                }
                else
                {
                    DataTable dt = _company.GetCompany("", "", out err);
                    List<GCCompany> items = (List<GCCompany>)IListDataSet.DataTableToIList<GCCompany>(dt, 1);
                    if (items != null)
                        DataGridRecord.ItemsSource = items;
                }
              
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("查询数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    GCCompany selcompany = (GCCompany)DataGridRecord.SelectedItems[0];
                    if (selcompany != null)
                    {
                        Global.CompanyName = selcompany.bjcdwmc;
                        Global.CompanyID = selcompany.cid;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请选择需要的记录再单击选择！", "操作提示",MessageBoxButton.OK ,MessageBoxImage.Warning );
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                 if (DataGridRecord.SelectedItems.Count == 0)
                 {
                     MessageBox.Show("请选择需要的记录再单击选择！", "操作提示",MessageBoxButton.OK ,MessageBoxImage.Warning );
                 }
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    GCCompany selcompany = (GCCompany)DataGridRecord.SelectedItems[0];
                    if (selcompany != null )
                    {
                        Global.CompanyName = selcompany.bjcdwmc;
                        Global.CompanyID = selcompany.cid;
                    }

                  
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示",MessageBoxButton.OK ,MessageBoxImage.Warning);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}