using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ManagementCompany.xaml 的交互逻辑
    /// </summary>
    public partial class ManagementCompany : Window
    {
        public ManagementCompany()
        {
            InitializeComponent();
        }

        private clsCompanyOpr _clsCompanyOprbll = new clsCompanyOpr();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            SearchCompany();
        }

        /// <summary>
        /// 查询被检单位
        /// </summary>
        private void SearchCompany()
        {
            this.DataGridRecord.DataContext = null;
            if (textBoxCompanyName.Text.Trim().ToString() != "")
            {
                DataTable dataTable = _clsCompanyOprbll.GetAsDataTable("name Like '%" + textBoxCompanyName.Text.Trim().ToString() + "%'");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsCompany> ItemNames = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dataTable, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            else 
            {
                DataTable dataTable = _clsCompanyOprbll.GetAsDataTable("");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsCompany> ItemNames = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dataTable, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询按钮-查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchCompany();
        }

        /// <summary>
        /// 鼠标右键-修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsCompany _clsCompany = (clsCompany)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("update", _clsCompany);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何被检单位!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 鼠标右键-删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Deleted()
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确定要删除所选的被检单位吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsCompany record;
                    int result = 0;
                    string err = string.Empty;
                    try
                    {
                        for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                        {
                            if (DataGridRecord.SelectedItems[i].ToString().Equals("{NewItemPlaceholder}"))
                            {
                                break;
                            }
                            record = (clsCompany)DataGridRecord.SelectedItems[i];
                            if (record.ID > 0)
                            {
                                if (_clsCompanyOprbll.DeleteByID(record.ID, out err) == 1)
                                    result += 1;
                            }
                        }
                        if (result > 0)
                        {
                            MessageBox.Show("成功删除 " + result + " 条数据!", "操作提示");
                        }
                    }
                    catch (Exception Exception)
                    {
                        MessageBox.Show("删除失败!出现异常\n" + Exception.ToString());
                    }
                    finally
                    {
                        SearchCompany();
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择任何被检单位!", "操作提示");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsCompany _decide = (clsCompany)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("update", _decide);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何被检单位!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 新增||修改
        /// </summary>
        /// <param name="type">add为新增||update为修改</param>
        void AddOrUpdate(string type, clsCompany model)
        {
            AddOrUpdateCompany window = new AddOrUpdateCompany();
            try
            {
                if (type.Equals("add"))
                {
                    window.Title = "新增被检单位";
                }
                else
                {
                    window.Title = "修改被检单位";
                    window.btnSave.Content = "修改";
                    window._clsCompany = model;
                }
                window.ShowDialog();
                SearchCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdate("add", null);
        }

        private void DataGridRecord_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsCompany _decide = (clsCompany)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("update", _decide);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有被检单位吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _clsCompanyOprbll.Delete(string.Empty, out sErr, "TCOMPANY");
                str = sErr.Equals(string.Empty) ? "已成功清理所有被检单位!" : ("清理被检单位时出现错误!\n异常：" + sErr);
                SearchCompany();
                MessageBox.Show(str, "操作提示");
            }
        }

        private void textBoxCompanyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchCompany();
        }
    }
}
