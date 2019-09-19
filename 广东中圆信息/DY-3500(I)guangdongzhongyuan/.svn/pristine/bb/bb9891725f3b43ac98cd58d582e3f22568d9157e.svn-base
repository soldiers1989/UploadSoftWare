using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO.xaml.KjService
{
    /// <summary>
    /// SearchReceiveTasks.xaml 的交互逻辑
    /// </summary>
    public partial class SearchReceiveTasks : Window
    {
        public SearchReceiveTasks()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }
        List<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem> models = null;
        DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem _selectedModel = null;
        public string itemName = string.Empty;

        private MsgThread _msgThread;
        class MsgThread : ChildThread
        {
            SearchReceiveTasks wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(SearchReceiveTasks wnd)
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
                        wnd.RefreshInfo(false);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search_Click(null, null);
        }

        /// <summary>
        /// 查询 OR 在线加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string val = txtVal.Text.Trim();
            if (val.Length == 0)
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
            //如果输入查询条件，则查询已加载的数据
            else
            {
                if (Global.KjServer.receiveTasksEntity != null && Global.KjServer.receiveTasksEntity.tasks.Count > 0)
                {
                    models = new List<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem>();
                    for (int i = 0; i < Global.KjServer.receiveTasksEntity.tasks.Count; i++)
                    {
                        int index = Global.KjServer.receiveTasksEntity.tasks[i].t_task_title.IndexOf(val);
                        if (index >= 0 && itemName.Equals(Global.KjServer.receiveTasksEntity.tasks[i].d_item))
                        {
                            models.Add(Global.KjServer.receiveTasksEntity.tasks[i]);
                        }
                    }
                    ShowTasks(models);
                }
            }
        }

        private void ShowTasks(List<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem> models = null)
        {
            //本地查询
            if (models != null)
            {
                DataGridRecord.DataContext = models;
                return;
            }

            //在线加载数据
            if (Global.KjServer.receiveTasksEntity != null && Global.KjServer.receiveTasksEntity.tasks.Count > 0)
            {
                models = new List<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem>();
                for (int i = 0; i < Global.KjServer.receiveTasksEntity.tasks.Count; i++)
                {
                    if (itemName.Equals(Global.KjServer.receiveTasksEntity.tasks[i].d_item))
                    {
                        models.Add(Global.KjServer.receiveTasksEntity.tasks[i]);
                    }
                }
                DataGridRecord.DataContext = Global.ListSort<DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem>(models, "t_task_cdate", "desc");
            }
            else
            {
                DataGridRecord.DataContext = null;
            }
        }

        private void RefreshInfo(bool request = true)
        {
            if (request)
            {
                this.Title = "正在获取数据···";
                Btn_Search.Content = "正在加载";
                miSearch.IsEnabled = Btn_Search.IsEnabled = false;
                miSelected.IsEnabled = Btn_Selected.IsEnabled = false;
            }
            else
            {
                this.Title = "任务查询选择";
                Btn_Search.Content = "查询";
                miSearch.IsEnabled = Btn_Search.IsEnabled = true;
                miSelected.IsEnabled = Btn_Selected.IsEnabled = true;
            }
        }

        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selected_Click(null, null);
        }

        /// <summary>
        /// 选择任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedModel == null)
            {
                MessageBox.Show("没有可操作的数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Global.KjServer._selectReceiveTasks = _selectedModel;
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                _selectedModel = (DyInterfaceHelper.KjService.ReceiveTasksEntity.TasksItem)DataGridRecord.SelectedItem;
            }
            else
            {
                _selectedModel = null;
            }
        }

    }
}