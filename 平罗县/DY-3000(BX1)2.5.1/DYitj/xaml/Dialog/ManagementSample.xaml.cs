using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddSample.xaml 的交互逻辑
    /// </summary>
    public partial class ManagementSample : Window
    {
        public ManagementSample()
        {
            InitializeComponent();
        }

        private bool _IsFirst = true;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private clsttStandardDecideOpr _clsttStandardDecideOpr = new clsttStandardDecideOpr();
        private List<clsttStandardDecide> _ItemNames = new List<clsttStandardDecide>();
        private string logType = "ManagementSample-error";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDeleted.Visibility = btnCleanRepeatSample.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            SearchSample();
        }

        /// <summary>
        /// 查询样品
        /// </summary>
        private void SearchSample()
        {
            this.DataGridRecord.DataContext = null;
            try
            {
                DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(textBoxSampleName.Text.Trim(), textBoxName.Text.Trim(), false, _IsFirst, 1);
                if (dataTable != null)
                {
                    List<clsttStandardDecide> ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    if (ItemNames != null && ItemNames.Count > 0)
                    {
                        this.DataGridRecord.DataContext = ItemNames;
                        _IsFirst = false;
                        if (_ItemNames.Count <= 0)
                            _ItemNames = ItemNames;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("查询数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 删除数据||删除表格列
        /// </summary>
        private void Deleted()
        {
            if (DataGridRecord == null)
            {
                MessageBox.Show("未选择任何样品！", "操作提示");
                return;
            }

            if (DataGridRecord.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确定要删除所选的样品吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsttStandardDecide record;
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
                            record = (clsttStandardDecide)DataGridRecord.SelectedItems[i];
                            if (record.ID > 0)
                            {
                                if (_clsttStandardDecideOpr.DeleteByID(record.ID, out err) == 1)
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
                        SearchSample();
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择任何样品！", "操作提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            Update();
        }

        /// <summary>
        /// 修改样品
        /// </summary>
        private void Update()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsttStandardDecide _decide = (clsttStandardDecide)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("UPDATE", _decide);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何样品！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("修改数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 删除选中行的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 修改选中行的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miUpload_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdate("ADD", null);
        }

        /// <summary>
        /// 新增||修改样品
        /// </summary>
        /// <param name="type">add为新增||update为修改</param>
        private void AddOrUpdate(String type, clsttStandardDecide model)
        {
            try
            {
                AddOrUpdateSample addWindow = new AddOrUpdateSample();
                if (!type.Equals("ADD"))
                {
                    addWindow._decide = model;
                }
                addWindow._addOrUpdate = type;
                addWindow._ItemNames = _ItemNames;
                addWindow.ShowDialog();
                SearchSample();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("新增|删除数据时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void DataGridRecord_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsttStandardDecide _decide = (clsttStandardDecide)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("UPDATE", _decide);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void textBoxSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSample();
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSample();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchSample();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void textBoxName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            SearchSample searchSample = new SearchSample();
            try
            {
                searchSample.ShowDialog();
                if (!Global.projectName.Equals(""))
                {
                    this.textBoxName.Text = Global.projectName;
                    SearchSample();
                }
                Global.projectName = "";//还原项目名称的值
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void btnCleanRepeatSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode("", "", false, false, 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsttStandardDecide> ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    if (ItemNames != null && ItemNames.Count > 0)
                    {
                        int delNum = 0;
                        IDictionary<string, clsttStandardDecide> dicItems = new Dictionary<string, clsttStandardDecide>();
                        string err = String.Empty;
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            //key=样品名称+项目名称+标准名称     //+标准值+判定符号+单位
                            string key = ItemNames[i].FtypeNmae + "_" + ItemNames[i].Name + "_" + ItemNames[i].ItemDes;
                            //+ "_" + ItemNames[i].StandardValue + "_" + ItemNames[i].Demarcate + "_" + ItemNames[i].Unit;
                            if (!dicItems.ContainsKey(key))
                            {
                                dicItems.Add(key, ItemNames[i]);
                            }
                            else
                            {
                                if (_clsttStandardDecideOpr.DeleteByID(ItemNames[i].ID, out err) == 1)
                                {
                                    delNum += 1;
                                }
                            }
                        }
                        if (delNum > 0)
                        {
                            MessageBox.Show("成功清理 " + delNum + " 条重复样品！");
                        }
                        else
                        {
                            MessageBox.Show("暂无重复样品！");
                        }
                        SearchSample();
                    }
                }
                else
                {
                    MessageBox.Show("暂无样品需要清理!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            try
            {
                if (MessageBox.Show("确定要清空所有样品吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _clsttStandardDecideOpr.Delete("", out sErr);
                    str = sErr.Equals("") ? "已成功清理所有样品！" : ("清理样品时出错！\n异常：" + sErr);
                    SearchSample();
                    MessageBox.Show(str, "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 食品种类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FoodClass_Click(object sender, RoutedEventArgs e)
        {
            FoodCategoriesWindow window = new FoodCategoriesWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

    }
}