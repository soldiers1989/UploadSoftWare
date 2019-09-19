using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using com.lvrenyang;
using DYSeriesDataSet;

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
        private string logType = "ManagementCompany-error";

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
            try
            {
                this.DataGridRecord.DataContext = null;
                DataTable dataTable = _clsCompanyOprbll.GetAsDataTable("FULLNAME Like '%" + textBoxCompanyName.Text.Trim().ToString() + "%'");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsCompany> ItemNames = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dataTable, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("查询数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
                        AddOrUpdate("UPDATE", _clsCompany);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何被检单位！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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
            if (DataGridRecord != null && DataGridRecord.SelectedItems.Count > 0)
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
                            MessageBox.Show("成功删除 " + result + " 条数据！", "操作提示");
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(6, logType, ex.ToString());
                        MessageBox.Show("删除数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                    }
                    finally
                    {
                        SearchCompany();
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择任何被检单位！", "操作提示");
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
                        AddOrUpdate("UPDATE", _decide);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何被检单位！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("修改数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 新增||修改
        /// </summary>
        /// <param name="type">add为新增||update为修改</param>
        private void AddOrUpdate(string type, clsCompany model)
        {
            AddOrUpdateCompany window = new AddOrUpdateCompany();
            try
            {
                if (type.Equals("ADD"))
                {
                    window.Title = "新增 - 被检单位";
                }
                else
                {
                    window.Title = "编辑 - 被检单位";
                    window._clsCompany = model;
                }
                window._type = type;
                window.ShowDialog();
                SearchCompany();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("新增|修改数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdate("ADD", null);
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
                        AddOrUpdate("UPDATE", _decide);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = "";
            if (MessageBox.Show("确定要清空所有被检单位吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _clsCompanyOprbll.Delete("", out sErr);
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("删除数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                }
                str = sErr.Equals("") ? "已成功清理所有被检单位！" : ("清理被检单位时出现错误！\n异常：" + sErr);
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