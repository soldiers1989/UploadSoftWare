﻿using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        public TaskDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataGridRecord.LoadingRow += new EventHandler<DataGridRowEventArgs>(this.DataGridRecord_LoadingRow);
            btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            //SearchTask();
            uploadTask();
        }

        private void SearchTask()
        {
            this.DataGridRecord.ItemsSource = null;
            DataTable dt = _Tskbll.GetAsDataTable(string.Empty, string.Empty, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                this.DataGridRecord.ItemsSource = (Items != null && Items.Count > 0) ? Items : null;
            }
            else
                this.DataGridRecord.ItemsSource = null;
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
                MessageBox.Show(ex.Message);
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
                    MessageBox.Show("未选择任何任务!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 任务更新 2017年2月25日 wenj 新版本接口
        /// </summary>
        private void uploadTask()
        {
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

        private void btnTaskUpdate_Click(object sender, RoutedEventArgs e)
        {
            uploadTask();
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        private string DownloadTask(string TaskTemp)
        {
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DownTask:
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
                            //Console.WriteLine("任务更新失败,或者服务链接不正常，请联系管理员!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有任务吗?\n注意：任务一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _Tskbll.Delete(string.Empty, out sErr);
                str = sErr.Equals(string.Empty) ? "已成功清理所有任务!" : ("清理任务时出现错误!\n异常：" + sErr);
                SearchTask();
                MessageBox.Show(str, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

    }
}