using System;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AIO.src;
using Microsoft.Win32;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchKsItem.xaml 的交互逻辑
    /// </summary>
    public partial class KsSearchWindow : Window
    {
        public KsSearchWindow()
        {
            InitializeComponent();
        }

        string errMsg = string.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Global.IsProject)
            {
                DataGridRecord.Columns[0].Visibility = Visibility.Collapsed;
                DataGridRecord.Columns[1].Visibility = Visibility.Collapsed;
                DataGridRecord.Columns[2].Visibility = Visibility.Visible;
                DataGridRecord.Columns[3].Visibility = Visibility.Visible;
            }
            SearchItem();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchItem();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchItem();
        }

        private void SearchItem()
        {
            string where = txtName.Text.Trim();
            //项目
            if (Global.IsProject)
            {
                if (where.Length > 0)
                    where = string.Format("ItemName like '%{0}%' Or ParentName like '%{0}%'", where);
                DataTable dtbl = opr.GetAsDataTable("Ks_CheckItem", where, out errMsg);
                DataGridRecord.ItemsSource = (dtbl != null && dtbl.Rows.Count > 0) ? dtbl.DefaultView : null;
                Title = "检测项目检索";
            }
            //样品
            else
            {
                Title = "样品信息检索";
                if (where.Length > 0)
                    where = string.Format("FoodName like '%{0}%' Or ParentName like '%{0}%'", where);
                DataTable dtbl = opr.GetAsDataTable("Ks_FoodClass", where, out errMsg);
                DataGridRecord.ItemsSource = (dtbl != null && dtbl.Rows.Count > 0) ? dtbl.DefaultView : null;
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectDataGrid();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectDataGrid();
        }

        private void SelectDataGrid()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (Global.IsProject)
                    {
                        
                        Global.projectName = (DataGridRecord.SelectedItem as DataRowView).Row["ItemName"].ToString();
                    }
                    else
                    {
                        Global.vals = new string[2];
                        Global.vals[0] = (DataGridRecord.SelectedItem as DataRowView).Row["FoodName"].ToString();
                        Global.vals[1] = (DataGridRecord.SelectedItem as DataRowView).Row["FoodCode"].ToString();
                        //Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["FoodName"].ToString();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Global.IsProject ? "未选择任何检测项目!" : "未选择任何样品!", "操作提示");
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
        }

        /// <summary>
        /// 导入样品信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否立即进入设置界面进行基础数据字典同步？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SettingsWindow window = new SettingsWindow();
                window.ShowDialog();
                SearchItem();
            }
            return;
            if (MessageBox.Show("是否删除历史数据，进行全新导入?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string del = "DELETE FROM Ks_FoodClass";
                opr.OtherOpr(del, out errMsg);
                if (errMsg.Length != 0)
                {
                    MessageBox.Show("删除历史数据时出现异常！\r\n异常详情：" + errMsg);
                }
            }
            OpenFileDialog op = new OpenFileDialog();
            bool isImport = true;
            string error = string.Empty, repeatSample = string.Empty;
            int importNum = 0, datasNum = 0;
            try
            {
                op.Filter = "Excel (*.xls)|*.*";
                if ((bool)(op.ShowDialog()))
                {
                    DataTable dt = ExcelHelper.ImportExcel(op.FileName.Trim(), 0);
                    datasNum = dt.Rows.Count;
                    if (dt != null && datasNum > 0)
                    {
                        StringBuilder sql = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sql = new StringBuilder();
                            //判定是否存在，若存在则不导入
                            string where = string.Format("ParentCode = '{0}' And ParentName = '{1}' And FoodCode = '{2}' And FoodName = '{3}'",
                                dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            DataTable dtbl = opr.GetAsDataTable("Ks_FoodClass", where, out errMsg);
                            if (dtbl != null && dtbl.Rows.Count > 0)
                            {
                                //如果已经存在，则进行更新
                                sql.AppendFormat("ParentCode = '{0}',ParentName = '{1}',FoodName = '{2}'", dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
                                opr.Update("Ks_FoodClass", sql.ToString(), string.Format(" FoodCode = '{0}'", dt.Rows[i][2].ToString()), out errMsg);
                                importNum = error.Equals(string.Empty) ? importNum + 1 : importNum - 1;
                                continue;
                            }

                            sql.Append("Insert Into Ks_FoodClass (");
                            sql.Append("ParentCode,ParentName,FoodCode,FoodName) Values (");
                            sql.AppendFormat("'{0}','{1}','{2}','{3}')",
                                dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            opr.OtherOpr(sql.ToString(), out errMsg);
                            importNum = error.Equals(string.Empty) ? importNum + 1 : importNum - 1;
                        }
                    }
                    else
                    {
                        isImport = false;
                        MessageBox.Show(this, "没有需要导入的数据！", "系统提示");
                    }
                }
                else
                {
                    isImport = false;
                }
            }
            catch (Exception ex)
            {
                isImport = false;
                MessageBox.Show(this, "导入数据异常：\r\n" + ex.Message);
            }
            finally
            {
                if (isImport)
                {
                    if (importNum > 0)
                    {
                        if (datasNum == importNum)
                        {
                            MessageBox.Show(this, "成功导入 " + importNum + " 条数据!", "系统提示");
                        }
                        else if (repeatSample.Length > 0)
                        {
                            MessageBox.Show(this, "成功导入 " + importNum + " 条数据!\r\n\r\n其中部分数据已存在，不作导入操作！\r\n" + repeatSample, "系统提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "导入成功！\r\n\r\n其中部分数据已存在，不作导入操作！\r\n" + repeatSample, "系统提示");
                    }
                    SearchItem();
                }
            }
        }

    }
}