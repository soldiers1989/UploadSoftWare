using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text;
using com.lvrenyang;
using AIO.src;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet;
using System.Data;
using DyInterfaceHelper;
using System.Collections.Generic;

namespace AIO.xaml.KjService
{
    /// <summary>
    /// ReceiveTasks.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiveTasks : Window
    {
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        /// <summary>
        /// 任务查看
        /// </summary>
        public ReceiveTasks()
        {
            InitializeComponent();
            //_msgThread = new MsgThread(this);
            //_msgThread.Start();
        }

        private MsgThread _msgThread;
        class MsgThread : ChildThread
        {
            ReceiveTasks wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(ReceiveTasks wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_RECEIVETASKS:
                        if (msg.result)
                        {
                            wnd.ShowTasks();
                        }
                        else
                        {

                        }
                        wnd.RefreshInfo(false);
                        break;
                    default:
                        break;
                }
            }
        }

        string errMsg = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchTestTask();
            //DataGridRecord.Height = SystemParameters.WorkArea.Height - 270;
            //Btn_Refresh_Click(null, null);
        }

        private void ShowTasks()
        {
            if (Global.KjServer.receiveTasksEntity != null)
            {
                DataGridRecord.DataContext = Global.ListSort<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem>(Global.KjServer.receiveTasksEntity.tasks, "t_task_cdate", "desc");
            }
            else
            {
                DataGridRecord.DataContext = null;
            }
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        private void SearchTestTask()
        {
            try
            {
                string err = "";
                DataGridRecord.ItemsSource = null;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("username='{0}'", Global.KjServer.Kjuser_name);
                DataTable dt = _bll.GetTestTask(sb.ToString(), 1, out err);
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
                string str1 = Global.KjServer.KjServerAddr;
                string username = Global.KjServer.Kjuser_name;
                string reqtime = string.Empty;
                string err = "";

                DataTable  dt = _bll.GetRequestTime("RequestName='TaskManageTime'", "", out errMsg);//获取请求时间
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
                StringBuilder sb = new StringBuilder();
                string url = InterfaceHelper.GetServiceURL(str1, 11);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");

                FileUtils.KLog(sb.ToString(), "发送", 18);
                string  rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtndata, "接收", 18);
                if (rtndata.Contains("success") && rtndata.Contains("msg"))
                {
                    ResultData resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    if (resultd.success == true && resultd.msg == "操作成功")
                    {
                        ManageTaskn mt = JsonHelper.JsonToEntity<ManageTaskn>(resultd.obj.ToString());
                        if (mt != null && mt.tasks.Count > 0)
                        {
                            for (int i = 0; i < mt.tasks.Count; i++)
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
                    MessageBox.Show("任务更新完成，共成功下载 " + count + " 条数据！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DownTask();
                SearchTestTask();
                //Message msg = new Message()
                //{
                //    what = MsgCode.MSG_RECEIVETASKS
                //};
                //Global.workThread.SendMessage(msg, _msgThread);
                //RefreshInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        private void RefreshInfo(bool request = true)
        {
            if (request)
            {
                LabelInfo.Content = "正在获取数据···";
                Btn_Refresh.Content = "正在加载";
                Btn_Refresh.IsEnabled = false;
            }
            else
            {
                LabelInfo.Content = "更新成功！";
                Btn_Refresh.Content = "刷  新";
                Btn_Refresh.IsEnabled = true;
            }
        }

        /// <summary>
        /// 查看详情,标记已读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ViewTaskAndShow();
        }

        /// <summary>
        /// 双击查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewTaskAndShow();
        }

        private void ViewTaskAndShow()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    //标记已读
                    DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem model = (DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem)DataGridRecord.SelectedItem;
                    string detailId = model.d_id;
                    _msgThread = _msgThread == null ? new MsgThread(this) : _msgThread;
                    _msgThread.Start();
                    Message msg = new Message()
                    {
                        what = MsgCode.MSG_VIEWTASK,
                        str1 = detailId
                    };
                    Global.workThread.SendMessage(msg, _msgThread);

                    //查看详情
                    ShowReceiveTasks window = new ShowReceiveTasks();
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("未选择任何数据！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 开始检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_StartCheck_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem model = (DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem)DataGridRecord.SelectedItem;
            }
            else
            {
                MessageBox.Show("未选择任何数据！");
            }
        }

        /// <summary>
        /// 返回主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}