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
using com.lvrenyang;
using DYSeriesDataSet.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// MessageBulletinWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBulletinWindow : Window
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        private string errMsg = "";
        private string err = "";
        private string rtndata = string.Empty;
        private ResultData resultd = null;

        public MessageBulletinWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchGongGao();
        }

        /// <summary>
        /// 下载公告
        /// </summary>
        private void DownGongGao()
        {
            try
            {
                string str1 = Global.samplenameadapter[0].url;
                string username = Global.samplenameadapter[0].user;
                string reqtime = string.Empty;

                dt = _bll.GetRequestTime("RequestName='BulletMagageTime'", "", out errMsg);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    //reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='BulletMagageTime'", "", 1, out err);
                }
                else
                {
                    _bll.InsertResquestTime("'BulletMagageTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                }
                int count = 0;
                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 14);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&pageNumber={0}", "");
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);

                FileUtils.KLog(sb.ToString(), "发送", 21);
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtndata, "接收", 21);
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    if (resultd.success == true && resultd.msg == "操作成功")
                    {
                        JArray jArray = (JArray)JsonConvert.DeserializeObject(resultd.obj.ToString());
                        if (jArray != null && jArray.Count > 0)
                        {
                            clsGongGao mtt = new clsGongGao();
                            for (int i = 0; i < jArray.Count; i++)
                            {
                                mtt.bid = jArray[i]["id"].ToString();
                                mtt.from_user_name = jArray[i]["from_user_name"].ToString();
                                mtt.to_user_id = jArray[i]["to_user_id"].ToString();
                                mtt.to_user_type = jArray[i]["to_user_type"].ToString();
                                mtt.title = jArray[i]["title"].ToString();
                                mtt.content = jArray[i]["content"].ToString();
                                mtt.file_path = jArray[i]["file_path"].ToString();
                                mtt.file_name = jArray[i]["file_name"].ToString();
                                mtt.sendtime = jArray[i]["sendtime"].ToString();
                                mtt.group_id = jArray[i]["group_id"].ToString();
                                mtt.group_point_id = jArray[i]["group_point_id"].ToString();
                                mtt.log_id = jArray[i]["log_id"].ToString();
                                mtt.log_read_status = jArray[i]["log_read_status"].ToString();
                                mtt.log_read_time = jArray[i]["log_read_time"].ToString();

                                _bll.InertGongGao(mtt, username, out err);

                                count = count + 1;
                                //更新公报状态
                                sb.Length = 0;
                                url = InterfaceHelper.GetServiceURL(str1, 15);
                                sb.Append(url);
                                sb.AppendFormat("?userToken={0}", Global.Token);
                                sb.AppendFormat("&taskMsgId={0}", mtt.bid);
                                FileUtils.KLog(sb.ToString(), "发送", 22);
                                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                                FileUtils.KLog(rtndata, "接收", 22);
                            }
                        }

                        //string str = jArray[0]["a"].ToString();
   
                        //ManageGonggao mt = JsonHelper.JsonToEntity<ManageGonggao>(resultd.obj.ToString());
                        //if (mt != null && mt.obj.Count > 0)
                        //{
                        //    for (int i = 0; i < mt.obj.Count; i++)
                        //    {
                        //        MessageBullite mtt = mt.obj[i];
                        //        _bll.InertGongGao(mtt, username, out err);
                        //        count = count + 1;

                        //        //sb.Length = 0;
                        //        //string urlt = InterfaceHelper.GetServiceURL(str1, 12);
                        //        //sb.Append(url);
                        //        //sb.AppendFormat("?userToken={0}", Global.Token);
                        //        //sb.AppendFormat("&detailId={0}", mt.tasks[i].t_id);
                        //        //FileUtils.KLog(sb.ToString(), "发送", 19);
                        //        //rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                        //        //FileUtils.KLog(rtndata, "接收", 19);
                        //    }
                        //}
                    }
                    MessageBox.Show("消息公告更新完成，共成功下载 " + count + " 条数据！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 查询公告
        /// </summary>
        private void SearchGongGao()
        {
            try
            {
               
                DataGridRecord.ItemsSource = null;
                sb.Length = 0;
                sb.AppendFormat("users='{0}'", Global.samplenameadapter[0].user);
                dt = _bll.GetGongGao(sb.ToString(), 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsGongGao> Items = (List<clsGongGao>)IListDataSet.DataTableToIList<clsGongGao>(dt, 1);
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxSampleName.Text.Trim() != "")
                {
                    DataGridRecord.ItemsSource = null;
                    sb.Length = 0;
                    sb.AppendFormat("users='{0}' and title like '%{1}%'", Global.samplenameadapter[0].user, textBoxSampleName.Text.Trim());
                    dt = _bll.GetGongGao(sb.ToString(), 1, out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsGongGao> Items = (List<clsGongGao>)IListDataSet.DataTableToIList<clsGongGao>(dt, 1);
                        if (Items.Count > 0)
                            DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
                    }
                }
                else
                {
                    DataGridRecord.ItemsSource = null;
                    sb.Length = 0;
                    sb.AppendFormat("users='{0}'", Global.samplenameadapter[0].user);
                    dt = _bll.GetGongGao(sb.ToString(), 1, out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsGongGao> Items = (List<clsGongGao>)IListDataSet.DataTableToIList<clsGongGao>(dt, 1);
                        if (Items.Count > 0)
                            DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
                    }
                }

            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAllData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str1 = Global.samplenameadapter[0].url;
                string username = Global.samplenameadapter[0].user;
                string reqtime = string.Empty;

                _bll.DeleteGongGao("", 0, out err);//删除所有数据重新下载

                //dt = _bll.GetRequestTime("RequestName='BulletMagageTime'", "", out errMsg);//获取请求时间
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    reqtime = dt.Rows[0]["UpdateTime"].ToString();
                //    _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='BulletMagageTime'", "", 1, out err);
                //}
                //else
                //{
                //    _bll.InsertResquestTime("'BulletMagageTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                //}
                int count = 0;
                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(str1, 14);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&pageNumber={0}", "");
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);

                FileUtils.KLog(sb.ToString(), "发送", 21);
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtndata, "接收", 21);
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    if (resultd.success == true && resultd.msg == "操作成功")
                    {
                        JArray jArray = (JArray)JsonConvert.DeserializeObject(resultd.obj.ToString());
                        if (jArray != null && jArray.Count > 0)
                        {
                            clsGongGao mtt = new clsGongGao();
                            for (int i = 0; i < jArray.Count; i++)
                            {
                                mtt.bid = jArray[i]["id"].ToString();
                                mtt.from_user_name = jArray[i]["from_user_name"].ToString();
                                mtt.to_user_id = jArray[i]["to_user_id"].ToString();
                                mtt.to_user_type = jArray[i]["to_user_type"].ToString();
                                mtt.title = jArray[i]["title"].ToString();
                                mtt.content = jArray[i]["content"].ToString();
                                mtt.file_path = jArray[i]["file_path"].ToString();
                                mtt.file_name = jArray[i]["file_name"].ToString();
                                mtt.sendtime = jArray[i]["sendtime"].ToString();
                                mtt.group_id = jArray[i]["group_id"].ToString();
                                mtt.group_point_id = jArray[i]["group_point_id"].ToString();
                                mtt.log_id = jArray[i]["log_id"].ToString();
                                mtt.log_read_status = jArray[i]["log_read_status"].ToString();
                                mtt.log_read_time = jArray[i]["log_read_time"].ToString();

                                _bll.InertGongGao(mtt, username, out err);

                                count = count + 1;
                                //更新公报状态
                                sb.Length = 0;
                                url = InterfaceHelper.GetServiceURL(str1, 15);
                                sb.Append(url);
                                sb.AppendFormat("?userToken={0}", Global.Token);
                                sb.AppendFormat("&taskMsgId={0}", mtt.bid);
                                FileUtils.KLog(sb.ToString(), "发送", 22);
                                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                                FileUtils.KLog(rtndata, "接收", 22);
                            }
                        }
                    }
                    MessageBox.Show("消息公告更新完成，共成功下载 " + count + " 条数据！");
                    SearchGongGao();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count < 1)
                {
                    MessageBox.Show("请选择需要删除的记录","系统提示");
                    return;
                }
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    sb.Length = 0;
                    clsGongGao mmt = (clsGongGao)DataGridRecord.SelectedItems[i];
                    sb.AppendFormat("ID={0}", mmt.ID);
                    _bll.DeleteGongGao(sb.ToString(), 0, out err);
                }
                MessageBox.Show("删除成功！", "系统提示");
                SearchGongGao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DownGongGao();
            SearchGongGao();
        }
    }
}
