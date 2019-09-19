using System;
using System.Collections.Generic;
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
using AIO.src;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataSentence;
using System.Data;
using DYSeriesDataSet;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// Kcompany.xaml 的交互逻辑
    /// </summary>
    public partial class Kcompany : Window
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private clsCompanyOpr _clsCompanyOprbll = new clsCompanyOpr();
        private StringBuilder sb = new StringBuilder();
        private string rtndata = string.Empty;
        private ResultData resultd = null;
        private DataTable dt = null;
        private string errMsg = "";
        private string err = "";

        public Kcompany()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ResetCompany == true)
            {
                btnDeleted.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleted.Visibility = Visibility.Collapsed;
            }
            SearchCompany();
        }
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
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
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
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex()+1;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEnterprise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int item = 0;
                btnEnterprise.IsEnabled = false;
                string str1 = Global.samplenameadapter[0].url;
                string reqtime = string.Empty;

                dt = _bll.GetRequestTime("RequestName='BuinessTime'", "", out errMsg);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='BuinessTime'", "", 1, out err);
                }
                else
                {
                    _bll.InsertResquestTime("'BuinessTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
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
                            //string dd = regu.regulatory[i].remark;//2018-05-07 改0无经营户、1有经营户
                            sb.Length = 0;
                            sb.AppendFormat("rid='{0}' ", rg.id);

                            dt = _bll.getregulation(sb.ToString(), "", out errMsg);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (rg.delete_flag == "1")
                                {
                                    sb.Length = 0;
                                    sb.AppendFormat("rid='{0}' ", rg.id);
                                    _bll.DeleteRegulation(sb.ToString(), "", out errMsg);
                                }
                                else
                                {
                                    _bll.UpdateRegulation(rg, out errMsg);
                                    item = item + 1;
                                }
                            }
                            else
                            {
                                if (rg.delete_flag == "0")
                                {
                                    _bll.InsertRegulation(rg, out errMsg);
                                    item = item + 1;
                                }
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

                MessageBox.Show("经营户信息下载成功,共成功下载 " + item.ToString() + " 条数据", "提示");

                SearchCompany();
                btnEnterprise.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除数据并下载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            btnDeleted.IsEnabled = false;
            this.DataGridRecord.DataContext = null;
            try
            {
                _bll.DeleteRegulation(out errMsg);

                int item = 0;
                
                string str1 = Global.samplenameadapter[0].url;
                string reqtime = string.Empty;

                //dt = _bll.GetRequestTime("", "", out errMsg);//获取请求时间
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    reqtime = dt.Rows[0]["BuinessTime"].ToString();
                //}

                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 8);
                //下载监管对象
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "regulatory");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
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
                                if (rg.delete_flag == "1")
                                {
                                    sb.Length = 0;
                                    sb.AppendFormat("rid='{0}' ", rg.id);
                                    _bll.DeleteRegulation(sb.ToString(), "", out errMsg);
                                }
                                else
                                {
                                    _bll.UpdateRegulation(rg, out errMsg);
                                    item = item + 1;
                                }
                            }
                            else
                            {
                                if (rg.delete_flag == "0")
                                {
                                    _bll.InsertRegulation(rg, out errMsg);
                                    item = item + 1;
                                }
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
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
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
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
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

                SearchCompany();
                MessageBox.Show("经营户信息下载成功,共成功下载 " + item.ToString() + " 条数据", "提示");
                btnDeleted.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDeleted.IsEnabled = true;
            }
        }
    }
}
