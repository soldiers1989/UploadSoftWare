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
using DYSeriesDataSet;
using System.Data;
using AIO.src;
using DYSeriesDataSet.DataModel;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TaskManageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskManageWindow : Window
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        private string errMsg = "";
        private string err = "";
        private string rtndata = string.Empty;
        private ResultData resultd = null;

        public TaskManageWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DownTask();
            SearchTestTask();
        }
        /// <summary>
        /// 查询任务
        /// </summary>
        private void SearchTestTask()
        {
            try
            {
                DataGridRecord.ItemsSource = null;
                sb.Length = 0;
                sb.AppendFormat("username='{0}'", Global.samplenameadapter[0].user);
                dt = _bll.GetTestTask(sb.ToString(), 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsManageTask> Items = (List<clsManageTask>)IListDataSet.DataTableToIList<clsManageTask>(dt, 1);
                    if (Items.Count > 0)
                        DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 下载任务
        /// </summary>
        private void DownTask()
        {
            try
            {
                string str1 = Global.samplenameadapter[0].url;
                string username = Global.samplenameadapter[0].user;
                string reqtime = string.Empty;

                dt = _bll.GetRequestTime("RequestName='TaskManageTime'", "", out errMsg);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='TaskManageTime'", "", 1, out err);
                }
                else
                {
                    _bll.InsertResquestTime("'TaskManageTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                }
                int count = 0;
                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 11);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");

                FileUtils.KLog(sb.ToString(), "发送", 18);
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtndata, "接收", 18);
                if (rtndata.Contains("success") && rtndata.Contains("msg"))
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    if (resultd.success == true && resultd.msg == "操作成功")
                    {
                        ManageTaskn mt = JsonHelper.JsonToEntity<ManageTaskn>(resultd.obj.ToString());
                        if (mt != null && mt.tasks.Count > 0)
                        {
                            for(int i=0;i<mt.tasks.Count;i++)
                            {
                                ManageTaskTest mtt = mt.tasks[i];
                                _bll.InsertTask(mtt, username, out err);
                                count = count + 1;

                                //sb.Length = 0;
                                //string urlt = InterfaceHelper.GetServiceURL(str1, 12);
                                //sb.Append(url);
                                //sb.AppendFormat("?userToken={0}", Global.Token);
                                //sb.AppendFormat("&detailId={0}", mt.tasks[i].t_id);
                                //FileUtils.KLog(sb.ToString(), "发送", 19);
                                //rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                                //FileUtils.KLog(rtndata, "接收", 19);
                            }
                        }
                    }
                    MessageBox.Show("任务更新完成，共成功下载 "+count+" 条数据！" );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 查询任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sb.Length = 0;
                DataGridRecord.ItemsSource = null;
                if (textBoxCompanyName.Text.Trim() != "")
                {
                    sb.AppendFormat("t_task_title like '%{0}%' ", textBoxCompanyName.Text.Trim());
                    sb.AppendFormat(" and username='{0}'", Global.samplenameadapter[0].user);
                }

                dt = _bll.GetTestTask(sb.ToString(), 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsManageTask> Items = (List<clsManageTask>)IListDataSet.DataTableToIList<clsManageTask>(dt, 1);
                    if (Items.Count > 0)
                        DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
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
            DataGridRow dataGridRow = e.Row;
        }
        /// <summary>
        /// 任务更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnterprise_Click(object sender, RoutedEventArgs e)
        {
            DownTask();
            SearchTestTask();
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRecord.ItemsSource = null;
                _bll.DeleteTestTask("",1,out err);//删除所有数据重新下

                string str1 = Global.samplenameadapter[0].url;
                string username = Global.samplenameadapter[0].user;
                string reqtime = string.Empty;
                int count = 0;
                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 11);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                FileUtils.KLog(sb.ToString(), "发送", 18);
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtndata, "接收", 18);
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    if (resultd.success == true && resultd.msg == "操作成功")
                    {
                        ManageTaskn mt = JsonHelper.JsonToEntity<ManageTaskn>(resultd.obj.ToString());
                        if (mt != null && mt.tasks.Count > 0)
                        {
                            for (int i = 0; i < mt.tasks.Count; i++)
                            {
                                ManageTaskTest mtt = mt.tasks[i];
                                _bll.InsertTask(mtt,username, out err);
                                count = count + 1;

                                //sb.Length = 0;
                                //string urlt = InterfaceHelper.GetServiceURL(str1, 12);
                                //sb.Append(url);
                                //sb.AppendFormat("?userToken={0}", Global.Token);
                                //sb.AppendFormat("&detailId={0}", mt.tasks[i].t_id);
                                //FileUtils.KLog(sb.ToString(), "发送", 19);
                                //rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                                //FileUtils.KLog(rtndata, "接收", 19);
                            }
                        }
                    }
                    MessageBox.Show("数据重置成功，共成功下载 " + count + " 条数据！","系统提示");
                }
                SearchTestTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count < 1)
                {
                    MessageBox.Show("请选择需要删除的记录");
                    return;
                }
                for(int i=0;i<DataGridRecord.SelectedItems.Count;i++)
                {
                    sb.Length = 0;
                    clsManageTask mmt = (clsManageTask)DataGridRecord.SelectedItems[i];
                    sb.AppendFormat("ID={0}", mmt.ID);
                    _bll.DeleteTestTask(sb.ToString(), 1, out err);
                }
                MessageBox.Show("删除成功！","系统提示");
                SearchTestTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
