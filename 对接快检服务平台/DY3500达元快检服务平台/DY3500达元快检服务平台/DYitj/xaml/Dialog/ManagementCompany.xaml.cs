using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using DYSeriesDataSet.DataSentence.Kjc;
using AIO.src;
using System.Text;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataSentence;

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
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private clsCompanyOpr _clsCompanyOprbll = new clsCompanyOpr();
        private StringBuilder sb = new StringBuilder();
        private string rtndata = string.Empty;
        private ResultData resultd = null;
        private DataTable dt = null;
        


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchCompany();
            //btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            //if (Global.InterfaceType.Equals("KJC"))
            //{
            //    DataGridRecord.Visibility = Visibility.Collapsed;
            //    DataGridRecord_kjc.Visibility = Visibility.Visible;

            //    btnUpdate.Visibility = btnDelete.Visibility = btnAdd.Visibility = Visibility.Collapsed;
            //    SearchKjcCompany();
            //}
            //else
            //{
            //    DataGridRecord.Visibility = Visibility.Visible;
            //    DataGridRecord_kjc.Visibility = Visibility.Collapsed;
            //    SearchCompany();
            //}
        }

        string errMsg = string.Empty;

        //private void SearchKjcCompany()
        //{
        //    this.DataGridRecord_kjc.DataContext = null;
        //    string where = string.Empty;
        //    if (textBoxCompanyName.Text.Trim().Length > 0)
        //    {
        //        where = string.Format("regName like '%{0}%'", textBoxCompanyName.Text.Trim());
        //    }
        //    DataTable dtbl = kjcCompanyBLL.GetDataTable(out errMsg, 0, where);
        //    if (dtbl != null && dtbl.Rows.Count > 0)
        //    {
        //        DataGridRecord_kjc.DataContext = dtbl;
        //    }
        //}

        /// <summary>
        /// 查询被检单位
        /// </summary>
        private void SearchCompany()
        {
            this.DataGridRecord.DataContext = null;
            DataTable dataTable = _clsCompanyOprbll.GetRegulator("", 1);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<regulator> ItemNames = (List<regulator>)IListDataSet.DataTableToIList<regulator>(dataTable, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord_kjc.DataContext = ItemNames;
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
            //if (Global.InterfaceType.Equals("KJC"))
            //{
            //    SearchKjcCompany();
            //}
            //else
            //{
            //    SearchCompany();
            //}
            //SearchCompany();
            try
            {
                StringBuilder sb = new StringBuilder();
                if (textBoxCompanyName.Text.Trim() != "")
                {
                    sb.AppendFormat(" where r.reg_name like '%{0}%'", textBoxCompanyName.Text.Trim());
                }

                this.DataGridRecord.DataContext = null;
                DataTable dataTable = _clsCompanyOprbll.GetRegulator(sb.ToString(), 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<regulator> ItemNames = (List<regulator>)IListDataSet.DataTableToIList<regulator>(dataTable, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord_kjc.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void DataGridRecord_kjc_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DataGridRecord_kjc_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void miUpload_kjc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miDelete_kjc_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 信息更新下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnterprise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int item = 0;
                btnEnterprise.IsEnabled = false;
                string str1 = Global.samplenameadapter[0].url;
                string reqtime = string.Empty;

                dt = _bll.GetRequestTime("", "", out errMsg);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    reqtime = dt.Rows[0]["BuinessTime"].ToString();
                }

                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 8);
                //下载监管对象
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "regulatory");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    regulatorylist regu = JsonHelper.JsonToEntity<regulatorylist>(resultd.obj.ToString());
                    if (regu.regulatory.Count > 0)
                    {
                        for (int i = 0; i < regu.regulatory.Count; i++)
                        {
                            regulator rg = regu.regulatory[i];
                            sb.Length = 0;
                            sb.AppendFormat("rid='{0}' ", rg.id);
                        
                            dt = _bll.getregulation(sb.ToString(), "", out errMsg);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdateRegulation(rg, out errMsg);
                                item = item + 1;
                            }
                            else
                            {
                                _bll.InsertRegulation(rg, out errMsg);
                                 item = item + 1;
                            }
                        }
                    }
                }
                //经营户下载
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "business");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    businesslist regu = JsonHelper.JsonToEntity<businesslist>(resultd.obj.ToString());
                    if (regu.business.Count > 0)
                    {
                        for (int i = 0; i < regu.business.Count; i++)
                        {
                            Manbusiness rg = regu.business[i];

                            sb.Length = 0;
                            sb.AppendFormat("bid='{0}' ", rg.id);

                            dt = _bll.getbusiness(sb.ToString(), "", out errMsg);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.Updatebusiness(rg, out errMsg);
                            }
                            else
                            {
                                _bll.Insertbusiness(rg, out errMsg);
                            }
                        }
                    }
                }
                //监管对象人员
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "personnel");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    personlist regu = JsonHelper.JsonToEntity<personlist>(resultd.obj.ToString());
                    if (regu.personnel.Count > 0)
                    {
                        for (int i = 0; i < regu.personnel.Count; i++)
                        {
                            persons rg = regu.personnel[i];
                            sb.Length = 0;
                            sb.AppendFormat("pid='{0}' ", rg.id);
                            dt = _bll.getPerson(sb.ToString(), "", out errMsg);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdatePerson(rg, out errMsg);
                            }
                            else
                            {
                                _bll.InsertPerson(rg, out errMsg);
                            }

                           
                        }
                    }
                }

                MessageBox.Show("经营户信息下载成功,共成功下载 "+item.ToString() +" 条数据", "提示");

                //_bll.UpdateRequestTime(System.DateTime.Now.AddSeconds(-5).ToString("yyyy-MM-dd HH:mm:ss"), 4, out errMsg);//更新请求时间

                SearchCompany();
                btnEnterprise.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
