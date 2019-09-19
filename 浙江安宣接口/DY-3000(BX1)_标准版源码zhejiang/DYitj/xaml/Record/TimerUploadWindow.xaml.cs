using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using com.lvrenyang;

namespace AIO.xaml.Record
{
    /// <summary>
    /// TimerUploadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimerUploadWindow : Window
    {
        public DataTable dtbl = null;
        private DispatcherTimer _DataTimer = null;
        private int count = 10;
        private QueryThread _queryThread = null;
        private string logType = "timerUpload-error";

        public TimerUploadWindow()
        {
            InitializeComponent();
            _DataTimer = new DispatcherTimer();
            _DataTimer.Interval = TimeSpan.FromSeconds(1);
            _DataTimer.Tick += new EventHandler(TimerTick);
            _DataTimer.Start();

            this.Topmost = true;
            double y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            double x = this.Width;
            x = x > y ? 0 : y - x;
            this.Left = x;
            this.Top = 0;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            LbUploadInfo.Content = string.Format("{0} 有检测数据未上传，即将进行自动上传操作！", count);
            if (0 == count)
            {
                _DataTimer.Stop();
                LbUploadInfo.Content = "正在上传···";
                if (dtbl != null || dtbl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].UploadUserUUID;
                    }
                }
                else
                {
                    LbUploadInfo.Content = "暂时没有需要上传的数据！";
                    this.Close();
                    return;
                }
                Message msg = new Message();
                msg.what = MsgCode.MSG_UPLOAD;
                msg.obj1 = Global.samplenameadapter[0];
                msg.table = dtbl;
                Global.updateThread.SendMessage(msg, _queryThread);
            }
            count--;
        }

        private void WindowClose() 
        {
            count = 5;
            _DataTimer = new DispatcherTimer();
            _DataTimer.Interval = TimeSpan.FromSeconds(1);
            _DataTimer.Tick += new EventHandler(CloseTick);
            _DataTimer.Start();
        }

        private void CloseTick(object sender, EventArgs e)
        {
            LbUploadInfo.Content = string.Format("{0} 数据上传成功，窗口即将自动关闭！", count);
            if (0 == count)
            {
                _DataTimer.Stop();
                this.Close();
            }
            count--;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.IsStartUploadTimer = false;
            _queryThread = new QueryThread(this);
            _queryThread.Start();
        }

        private void LbUploadInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void LbUploadInfo_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        // 开一个子线程，专门做记录查询的事情，需要查询都发给子线程去做。查询结果出来之后，调用UI线程开始更新界面。
        // 一直读取AD值
        private class QueryThread : ChildThread
        {
            TimerUploadWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);

            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public QueryThread(TimerUploadWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                // 接收到消息之后进行处理，这里属于子线程，处理一些费时的查询操作
                switch (msg.what)
                {
                    default:
                        break;
                }
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

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                        {
                            if (Global.UploadSCount > 0)
                            {
                                wnd.WindowClose();
                            }
                        }
                        else
                        {
                            if (Global.UploadSCount > 0)
                            {
                                wnd.WindowClose();
                            }
                            else
                            {
                                wnd.LbUploadInfo.Content = "提示：上传失败!";
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}