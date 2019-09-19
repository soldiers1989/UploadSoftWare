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
using DYSeriesDataSet.DataSentence;
using System.Data;
using com.lvrenyang;
using DYSeriesDataSet;
using AIO.src;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// LawsShow.xaml 的交互逻辑
    /// </summary>
    public partial class LawsShow : Window
    {
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private string ftppath = CFGUtils.GetConfig("Lawdownfile", string.Empty);// @"ftp://120.24.239.96:54321/pdf/法律法规/";    //目标路径
        //private string ftpip = "120.24.239.96";    //ftp IP地址
        private string username = CFGUtils.GetConfig("ftpuser", string.Empty);// "xiaoyl";   //ftp用户名
        private string password = CFGUtils.GetConfig("ftppassword", string.Empty);// "123456";   //ftp密码
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();

        public LawsShow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ResetLaws == true)
            {
                btnUpdateAllData.Visibility = Visibility.Visible;
            }
            else
            {
                btnUpdateAllData.Visibility = Visibility.Collapsed;
            }
            SearchLaws();
        }
        private void SearchLaws()
        {
            dt = _kdb.GetLaws("", "", 1, out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<laws> ItemNames = (List<laws>)IListDataSet.DataTableToIList<laws>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 下载法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownlaw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnDownlaw.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownLaw,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };

                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        class MsgThread : ChildThread
        {
            LawsShow  wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(LawsShow wnd)
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
                    case MsgCode.MSG_DownLaw:
                        if (msg.result == true)
                        {
                            MessageBox.Show("法律法规下载成功，共下载 "+Global.Gitem +" 条记录");
                        }
                        wnd.SearchLaws();
                        wnd.btnDownlaw.IsEnabled = true;
                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownfile_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要下载的法规", "提示");
                btnDownfile.IsEnabled = true;
                return;
            }
            laws stdl = null;
            stdl = (laws)DataGridRecord.SelectedItems[0];
            string localpath = System.AppDomain.CurrentDomain.BaseDirectory + "DownFile\\";
            string filename = stdl.url_path;
            FTPDownFile fd = new FTPDownFile();
            bool down = fd.DownFtpToLocation(ftppath + filename, filename, localpath,username ,password);
            if (down == true)
            {
                MessageBox.Show("文件下载成功");
                btnDownfile.IsEnabled = true;
                System.Diagnostics.Process.Start(localpath + filename);
            }
            btnDownfile.IsEnabled = true;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sb.Length = 0;
                sb.AppendFormat(" law_name like '%{0}%'", textBoxSampleName.Text.Trim());

                dt = _kdb.GetLaws(sb.ToString(), "", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<laws> ItemNames = (List<laws>)IListDataSet.DataTableToIList<laws>(dt, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex()+1;
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAllData_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateAllData.IsEnabled = false;
            try
            {
                string lasttime = "";
                string str1 = Global.samplenameadapter[0].url;
                string lw = InterfaceHelper.GetServiceURL(str1, 8);//地址 msg.str1 + "iSampling/updateStatus.do";
                Global.Gitem = 0;
                _bll.DeleteLaws("", "", out err);
                DataGridRecord.DataContext = null;
                //dt = _bll.GetRequestTime("", "", out err);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    lasttime = dt.Rows[0]["Laws"].ToString();//获取上一次获取时间
                //}
                sb.Length = 0;
                sb.Append(lw);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "laws");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                sb.AppendFormat("&pageNumber={0}", "");

                FileUtils.KLog(sb.ToString(), "发送", 5);
                string law = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(law, "发送", 5);

                ResultData lawlist = JsonHelper.JsonToEntity<ResultData>(law);

                if (lawlist.msg == "操作成功" && lawlist.success == true)
                {
                    Lawlist zdata = JsonHelper.JsonToEntity<Lawlist>(lawlist.obj.ToString());
                    if (zdata.laws.Count > 0)
                    {
                        law lws = new law();
                        for (int i = 0; i < zdata.laws.Count; i++)
                        {
                            int w = 0;
                            sb.Length = 0;
                            sb.AppendFormat("wid='{0}' ", zdata.laws[i].id);
                        
                            dt = _bll.GetLaws(sb.ToString(), "", out err);

                            lws = zdata.laws[i];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                w = _bll.UpdateLaws(lws);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                w = _bll.InsertLaws(lws);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
             
                SearchLaws();
                MessageBox.Show("共成功下载"+Global.Gitem +"条数据");

                btnUpdateAllData.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnUpdateAllData.IsEnabled = true;
            }

            
        }
    }
}
