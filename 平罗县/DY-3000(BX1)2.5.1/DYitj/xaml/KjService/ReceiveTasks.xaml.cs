using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO.xaml.KjService
{
    /// <summary>
    /// ReceiveTasks.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiveTasks : Window
    {
        /// <summary>
        /// 任务查看
        /// </summary>
        public ReceiveTasks()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
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
            DataGridRecord.Height = SystemParameters.WorkArea.Height - 270;
            Btn_Refresh_Click(null, null);
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
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Message msg = new Message()
                {
                    what = MsgCode.MSG_RECEIVETASKS
                };
                Global.workThread.SendMessage(msg, _msgThread);
                RefreshInfo();
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