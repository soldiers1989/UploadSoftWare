using System;
using System.Windows;

namespace AIO.xaml.KjService
{
    /// <summary>
    /// TaskMsg.xaml 的交互逻辑
    /// </summary>
    public partial class TaskMsg : Window
    {

        /// <summary>
        /// 公告
        /// </summary>
        public TaskMsg()
        {
            InitializeComponent();
        }

        private MsgThread _msgThread;
        class MsgThread : ChildThread
        {
            TaskMsg wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(TaskMsg wnd)
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
                    case MsgCode.MSG_DOWNLOADTASKMSG:
                        wnd.ShowTaskMsg();
                        wnd.RefreshInfo(false);
                        break;

                    //更新公告查看
                    case MsgCode.MSG_VIEWTASKMSG:
                        if (msg.result)
                        {
                            //wnd.ShowSamplelingTasks(msg.str1, msg.str3);
                        }
                        else
                        {
                            //MessageBox.Show(wnd, msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridRecord.Height = SystemParameters.WorkArea.Height - 270;
            Btn_Refresh_Click(null, null);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            _msgThread = _msgThread == null ? new MsgThread(this) : _msgThread;
            _msgThread.Start();
            Message msg = new Message()
            {
                what = MsgCode.MSG_DOWNLOADTASKMSG,
                str1 = "",//时间
                str2 = ""//页码
            };
            Global.workThread.SendMessage(msg, _msgThread);
            RefreshInfo();
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

        private void ShowTaskMsg() 
        {
            if (Global.KjServer.taskMsgEntitys != null && Global.KjServer.taskMsgEntitys.Count > 0)
            {
                DataGridRecord.DataContext = Global.ListSort<DyInterfaceHelper.KjService.DownloadTaskMsgEntity>(Global.KjServer.taskMsgEntitys, "sendtime", "desc");
            }
            else
            {
                DataGridRecord.DataContext = null;
            }
        }

        private void DataGridRecord_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 双击查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}