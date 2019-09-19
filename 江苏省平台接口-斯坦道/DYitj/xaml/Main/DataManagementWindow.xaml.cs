using AIO.xaml.Dialog;
using AIO.xaml.Print;
using DYSeriesDataSet;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace AIO.xaml.Main
{
    /// <summary>
    /// DataManagementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagementWindow : Window
    {
        public DataManagementWindow()
        {
            InitializeComponent();
        }

        private MsgThread _msgThread;

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 样品管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementSample_Click(object sender, RoutedEventArgs e)
        {
            ManagementSample window = new ManagementSample()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 被检单位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementCompany_Click(object sender, RoutedEventArgs e)
        {
            ManagementCompany window = new ManagementCompany()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 检测记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            window.ShowInTaskbar = false; window.Owner = this; window.Show();
        }

        /// <summary>
        /// 任务查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            //Task();
            TaskDisplay window = new TaskDisplay()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
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
            {
                dataSet.ReadXml(sr);
            }
            int len = 0;
            if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            {
                if (dataSet != null)
                {
                    len = dataSet.Tables[0].Rows.Count;
                    dtbl = dataSet.Tables[0];
                }
                //任务
                clsTaskOpr bll = new clsTaskOpr();
                //bll.Delete(string.Empty, out delErr);
                sb.Append(delErr);
                if (len == 0)
                {
                    return string.Empty;
                }
                clsTask Tst = new clsTask();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    Tst.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                    Tst.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                    Tst.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                    Tst.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                    Tst.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                    Tst.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                    Tst.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                    Tst.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                    Tst.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                    Tst.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                    Tst.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                    Tst.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                    Tst.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                    Tst.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
                    bll.Insert(Tst, out err);
                    if (!err.Equals(string.Empty))
                    {
                        sb.Append(err);
                    }
                }
                if (sb.Length > 0)
                {
                    return sb.ToString();
                }
            }
            return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        }

        #region  检测任务下载线程
        class MsgThread : ChildThread
        {
            DataManagementWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(DataManagementWindow wnd)
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
                        if (!string.IsNullOrEmpty(msg.DownLoadTask))
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("下载数据错误,或者服务链接不正常，请联系管理员!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        private void BtnReportSearch_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 食品安全法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFalvFagui_Click(object sender, RoutedEventArgs e)
        {
            FalvFaguiWindow window = new FalvFaguiWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //如果接口类型为上传到广东智慧平台的话，则不显示任务更新模块
            if (Global.InterfaceType.Equals("ZH"))
            {
                Zjs.Visibility = Visibility.Collapsed;
            }
        }

    }
}