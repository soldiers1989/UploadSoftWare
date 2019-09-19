using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    /// <summary>
    /// TaskDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class TaskDisplay : Window
    {
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private MsgThread _msgThread;
        private bool _isShowBox = false;
        private string logType = "TaskDisplay-error";
        /// <summary>
        /// 是否更新检测任务
        /// </summary>
        public bool IsUpdataTask = true;

        public TaskDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            if (IsUpdataTask) btnTaskUpdate_Click(null, null);
            else SearchTask();
        }

        private void SearchTask()
        {
            try
            {
                this.DataGridRecord.ItemsSource = null;
                DataTable dt = _Tskbll.GetAsDataTable("", "", 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                    this.DataGridRecord.ItemsSource = (Items != null && Items.Count > 0) ? Items : null;
                }
                else
                    this.DataGridRecord.ItemsSource = null;
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 双击查看任务详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tlsTrTask tlsTrTask;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
                    TaskDetailedWindow window = new TaskDetailedWindow();
                    window.GetValues(tlsTrTask);
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btnViewDetailed_Click(object sender, RoutedEventArgs e)
        {
            ShowTask();
        }

        private void ShowTask()
        {
            tlsTrTask tlsTrTask;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
                    TaskDetailedWindow window = new TaskDetailedWindow();
                    window.GetValues(tlsTrTask);
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("未选择任何任务！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void btnTaskUpdate_Click(object sender, RoutedEventArgs e)
        {
            //this.LabelInfo.Content = "信息:正在更新任务";
            //this.btnTaskUpdate.Content = "正在更新···";
            //this.btnTaskUpdate.IsEnabled = false;
            //_isShowBox = true;
            //this.btnTaskUpdate.FontSize = 16;
            //try
            //{
            //    _msgThread = new MsgThread(this);
            //    _msgThread.Start();
            //    Message msg = new Message();
            //    msg.what = MsgCode.MSG_DownTask;
            //    msg.obj1 = Global.samplenameadapter[0];
            //    Global.workThread.SendMessage(msg, _msgThread);
            //}
            //catch (Exception ex)
            //{
            //    FileUtils.OprLog(6, logType, ex.ToString());
            //    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            //}
            this.LabelInfo.Content = "信息:正在更新任务";
            this.btnTaskUpdate.Content = "正在更新···";
            this.btnTaskUpdate.IsEnabled = false;
            _isShowBox = true;
            this.btnTaskUpdate.FontSize = 16;
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownTask,
                    str1 = Global.samplenameadapter[0].ServerAddr,
                    str2 = Global.samplenameadapter[0].RegisterID,
                    str3 = Global.samplenameadapter[0].RegisterPassword
                };
                msg.args.Enqueue(Global.samplenameadapter[0].CheckPointID);
                msg.args.Enqueue(Global.samplenameadapter[0].CheckPointName);
                msg.args.Enqueue(Global.samplenameadapter[0].CheckPointType);
                msg.args.Enqueue(Global.samplenameadapter[0].Organization);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        private string DownloadTask(string TaskTemp)
        {
            //string delErr = string.Empty;
            //string err = string.Empty;
            //StringBuilder sb = new StringBuilder();
            //DataSet dataSet = new DataSet();
            //DataTable dtbl = new DataTable();
            //int len = 0;
            //try
            //{
            //    using (StringReader sr = new StringReader(TaskTemp))
            //        dataSet.ReadXml(sr);
            //    if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            //    {
            //        if (dataSet != null)
            //        {
            //            len = dataSet.Tables[0].Rows.Count;
            //            dtbl = dataSet.Tables[0];
            //        }
            //        //bll.Delete("", out delErr);
            //        sb.Append(delErr);
            //        if (len == 0)
            //            return "";
            //        clsTask model = new clsTask();
            //        for (int i = 0; i < len; i++)
            //        {
            //            err = string.Empty;
            //            model.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
            //            model.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
            //            model.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
            //            model.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
            //            model.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
            //            model.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
            //            model.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
            //            model.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
            //            model.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
            //            model.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
            //            model.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
            //            model.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
            //            model.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
            //            model.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
            //            model.UDate = dtbl.Rows[i]["UDATE"].ToString();
            //            _Tskbll.InsertOrUpdate(model, out err);
            //            if (!err.Equals(string.Empty))
            //                sb.Append(err);
            //        }
            //        if (sb.Length > 0)
            //            return sb.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    FileUtils.OprLog(0, "downloadTask-error", ex.ToString());
            //    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            //}
            //this.btnTaskUpdate.FontSize = 24;
            //this.btnTaskUpdate.Content = "任务更新";
            //this.btnTaskUpdate.IsEnabled = true;
            //SearchTask();
            //return string.Format("已经成功下载{0}条样品种类数据", len.ToString());

            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            DataSet dataSet = new DataSet();
            DataTable dtbl = new DataTable();
            using (StringReader sr = new StringReader(TaskTemp))
                dataSet.ReadXml(sr);
            int len = 0;
            if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            {
                if (dataSet != null)
                {
                    len = dataSet.Tables[0].Rows.Count;
                    dtbl = dataSet.Tables[0];
                }
                //bll.Delete(string.Empty, out delErr);
                sb.Append(delErr);
                if (len == 0)
                    return string.Empty;
                clsTask model = new clsTask();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    try
                    {
                        model.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                        model.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                        model.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                        model.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                        model.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                        model.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                        model.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                        model.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                        model.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                        model.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                        model.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                        model.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                        model.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                        model.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
                        model.UDate = dtbl.Rows[i]["UDATE"].ToString();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    _Tskbll.InsertOrUpdate(model, out err);
                    if (!err.Equals(string.Empty))
                        sb.Append(err);
                }
                if (sb.Length > 0)
                    return sb.ToString();
            }
            this.btnTaskUpdate.FontSize = 24;
            this.btnTaskUpdate.Content = "任务更新";
            this.btnTaskUpdate.IsEnabled = true;
            SearchTask();
            return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        }

        class MsgThread : ChildThread
        {
            TaskDisplay wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(TaskDisplay wnd)
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
                    FileUtils.OprLog(6, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DownTask:
                        //if (msg.result)
                        //{
                        //    if (!string.IsNullOrEmpty(msg.DownLoadTask) && wnd._isShowBox)
                        //    {
                        //        try
                        //        {
                        //            wnd.DownloadTask(msg.DownLoadTask);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            _checkedDown = false;
                        //            wnd.LabelInfo.Content = "信息:任务更新失败";
                        //            MessageBox.Show("任务更新失败！请联系管理员！\n" + ex.Message);
                        //        }
                        //        finally
                        //        {
                        //            if (_checkedDown)
                        //            {
                        //                wnd._isShowBox = false;
                        //                wnd.LabelInfo.Content = "信息:任务更新完成";
                        //                MessageBox.Show("任务更新完成！", "操作提示");
                        //            }
                        //        }
                        //    }
                        //    else if (wnd._isShowBox)
                        //    {
                        //        MessageBox.Show("任务更新失败,或者服务链接不正常，请联系管理员！");
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show(wnd, "任务更新失败！\r\n\r\n异常信息：" + msg.error, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        //}
                        //wnd._isShowBox = _checkedDown = false;
                        //wnd.LabelInfo.Content = "信息:任务更新失败";
                        //wnd.btnTaskUpdate.FontSize = 24;
                        //wnd.btnTaskUpdate.Content = "任务更新";
                        //wnd.btnTaskUpdate.IsEnabled = true;
                        if (!string.IsNullOrEmpty(msg.DownLoadTask) && wnd._isShowBox)
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                wnd.btnTaskUpdate.FontSize = 24;
                                wnd.btnTaskUpdate.Content = "任务更新";
                                wnd.btnTaskUpdate.IsEnabled = true;
                                wnd.LabelInfo.Content = "信息:任务更新失败";
                                MessageBox.Show("任务更新失败!请联系管理员!\n" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                            finally
                            {
                                if (_checkedDown)
                                {
                                    wnd._isShowBox = false;
                                    wnd.LabelInfo.Content = "信息:任务更新完成";
                                    MessageBox.Show("任务更新完成!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                        else if (wnd._isShowBox)
                        {
                            wnd._isShowBox = _checkedDown = false;
                            wnd.LabelInfo.Content = "信息:任务更新失败";
                            wnd.btnTaskUpdate.FontSize = 24;
                            wnd.btnTaskUpdate.Content = "任务更新";
                            wnd.btnTaskUpdate.IsEnabled = true;
                            MessageBox.Show("任务更新失败,或者服务链接不正常，请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = "";
            if (MessageBox.Show("确定要清空所有任务吗?\n注意：任务一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _Tskbll.Delete("", out sErr);
                }
                catch (Exception ex)
                {
                    FileUtils.Log("TaskDisplay-btnDeleted_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                }
                str = sErr.Equals("") ? "已成功清理所有任务！" : ("清理任务时出现错误！\n异常：" + sErr);
                SearchTask();
                MessageBox.Show(str, "操作提示");
            }
        }

    }
}