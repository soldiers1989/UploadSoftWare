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
    /// KsBusinessMsg.xaml 的交互逻辑
    /// </summary>
    public partial class KsBusinessMsg : Window
    {
        public KsBusinessMsg()
        {
            InitializeComponent();
        }

        string errMsg = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.KsVersion.Equals("2"))
            {
                btnChoose.Content = "查看经营户";
                DataGridRecord_AreaMarket.Visibility = Visibility.Visible;
                DataGridRecord.Visibility = Visibility.Collapsed;
            }
            else
            {
                DataGridRecord_AreaMarket.Visibility = Visibility.Collapsed;
                DataGridRecord.Visibility = Visibility.Visible;
            }
            SearchBusiness();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SelectDataGrid();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBusiness();
        }

        string LicenseNo = string.Empty;
        string temp = string.Empty;
        private void SearchBusiness()
        {
            string where = txtName.Text.Trim();
            //显示单位主体信息
            if (Global.KsVersion.Equals("2") && DataGridRecord_AreaMarket.Visibility == Visibility.Visible)
            {
                if (where.Length > 0)
                    where = string.Format(" MarketName like '%{0}%' Or Abbreviation like '%{0}%'", where);
                else
                    where = string.Empty;
                DataTable dtbl = opr.GetAsDataTable("ks_AreaMarket", where, out errMsg);
                DataGridRecord_AreaMarket.ItemsSource = (dtbl != null && dtbl.Rows.Count > 0) ? dtbl.DefaultView : null;
            }
            //显示经营户信息
            else
            {
                //如果是分局版本，则需要加上单位主体编号条件
                if (where.Length > 0)
                    where = string.Format(" (TWQY like '%{0}%' Or TWNum like '%{0}%' Or TWNume like '%{0}%') Or BusinessScope like '%{0}%' {1}",
                        where, Global.KsVersion.Equals("2") ? string.Format(" And LicenseNo = '{0}'", LicenseNo) : "");
                else if (Global.KsVersion.Equals("2"))
                    where = string.Format(" LicenseNo = '{0}'", LicenseNo);
                DataTable dtbl = opr.GetAsDataTable("Ks_Business", where, out errMsg);
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    DataGridRecord.ItemsSource = dtbl.DefaultView;
                }
                else
                {
                    DataGridRecord.ItemsSource = null;
                }
            }
        }

        private void SettingDgv(bool isAreaMarket)
        {
            if (isAreaMarket)
            {
                //btnChoose.Content = "查看经营户";
                btnImport.Content = "更新";
                DataGridRecord_AreaMarket.Visibility = Visibility.Visible;
                DataGridRecord.Visibility = Visibility.Collapsed;
                if (temp.Length > 0)
                {
                    txtName.Text = temp;
                    temp = string.Empty;
                }
            }
            else
            {
                if (txtName.Text.Trim().Length > 0)
                {
                    temp = txtName.Text.Trim();
                    txtName.Text = string.Empty;
                }
                //btnChoose.Content = "选择";
                btnImport.Content = "返回";
                DataGridRecord_AreaMarket.Visibility = Visibility.Collapsed;
                DataGridRecord.Visibility = Visibility.Visible;
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (btnImport.Content.Equals("返回"))
            {
                SettingDgv(true);
                return;
            }

            if (MessageBox.Show("是否立即进入设置界面进行基础数据字典同步？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SettingsWindow window = new SettingsWindow();
                window.ShowDialog();
                SearchBusiness();
            }
            return;
            if (MessageBox.Show("是否立即进入设置界面进行基础数据字典同步？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SettingsWindow window = new SettingsWindow();
                window.ShowDialog();
                SearchBusiness();
            }
            return;
            if (MessageBox.Show("是否删除历史数据，进行全新导入?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string del = "DELETE FROM Ks_Business";
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
                            //判定是否存在，若存在则不导入
                            string where = string.Format("TWQY = '{0}' And TWNum = '{1}' And TWNume = '{2}' And IdentityCard = '{3}'",
                                dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                            DataTable dtbl = opr.GetAsDataTable("Ks_Business", where, out errMsg);
                            if (dtbl != null && dtbl.Rows.Count > 0)
                            {
                                continue;
                            }

                            sql = new StringBuilder();
                            sql.Append("Insert Into Ks_Business (");
                            sql.Append("TWQY,TWNum,TWNume,IdentityCard,BusinessLicense,LinkPhone,FamilyAddress,BusinessScope,");
                            sql.Append("InboundChannel,ProtocolcODE,ContractDate,Contractarea,ContractAmount,IsCharge,Issigned,ID) ");
                            sql.Append("Values (");
                            sql.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',",
                                dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(),
                                dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(),
                                dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString());
                            sql.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                                dt.Rows[i][10].ToString(), dt.Rows[i][11].ToString(), dt.Rows[i][12].ToString(),
                                dt.Rows[i][13].ToString(), dt.Rows[i][14].ToString(), dt.Rows[i][15].ToString(), Global.GETGUID());
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
                    SearchBusiness();
                }
            }
        }

        /// <summary>
        /// 将Excel科学记数转换成常规数值
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private string ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
                dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            else
                return strData;
            return dData.ToString();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            //if (btnChoose.Content.Equals("查看经营户"))
            //{
                miShowBusiness_Click(null, null);
            //    return;
            //}
            //SelectDataGrid();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
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
                    Global.vals = new string[2];
                    Global.vals[0] = (DataGridRecord.SelectedItem as DataRowView).Row["TWNume"].ToString();
                    Global.vals[1] = (DataGridRecord.SelectedItem as DataRowView).Row["ID"].ToString();
                    this.Close();
                }
                else if (DataGridRecord_AreaMarket.SelectedItems.Count > 0)
                {
                    Global.vals = new string[2];
                    Global.vals[0] = (DataGridRecord_AreaMarket.SelectedItem as DataRowView).Row["MarketName"].ToString();
                    Global.vals[1] = (DataGridRecord_AreaMarket.SelectedItem as DataRowView).Row["LicenseNo"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择任何项目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void miShowBusiness_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord_AreaMarket.SelectedItems.Count > 0)
            {
                LicenseNo = (DataGridRecord_AreaMarket.SelectedItem as DataRowView).Row["LicenseNo"].ToString();
                Global.KsMarketInfo = (DataGridRecord_AreaMarket.SelectedItem as DataRowView).Row["MarketName"].ToString() + "|" + LicenseNo;
                //如果是分局版本，则需要加上单位主体编号条件
                string where = string.Format(" LicenseNo = '{0}'", LicenseNo);
                DataTable dtbl = opr.GetAsDataTable("Ks_Business", where, out errMsg);
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    DataGridRecord.ItemsSource = dtbl.DefaultView;
                    SettingDgv(false);
                }
                else
                {
                    DataGridRecord.ItemsSource = null;
                    if (Global.KsVersion.Equals("2"))
                    {
                        MessageBox.Show("当前所选单位主体下暂时没有经营户信息！", "暂无数据", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            }
        }

        private void DataGridRecord_AreaMarket_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            miShowBusiness_Click(null, null);
        }

        private void DataGridRecord_AreaMarket_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}