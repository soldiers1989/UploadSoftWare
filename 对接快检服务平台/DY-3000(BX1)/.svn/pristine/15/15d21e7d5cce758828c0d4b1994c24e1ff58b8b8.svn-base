using AIO.src;
using com.lvrenyang;
using DY.Process;
using DyInterfaceHelper;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataSentence;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TestStandard.xaml 的交互逻辑
    /// </summary>
    public partial class TestStandard : Window
    {
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private string ftppath = CFGUtils.GetConfig("standlownfile", string.Empty);// @"ftp://120.24.239.96:54321/pdf/食品标准/";    //目标路径
        //private string ftpip = "120.24.239.96";    //ftp IP地址
        private string username = CFGUtils.GetConfig("ftpuser", string.Empty);// "xiaoyl";   //ftp用户名
        private string password = CFGUtils.GetConfig("ftppassword", string.Empty);// "123456";   //ftp密码
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        public TestStandard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchStand();
        }

        private void SearchStand()
        {
            this.DataGridRecord.DataContext = null;
            dt = _kdb.GetStandlist("delete_flag='0'", "", 1, out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<standlist> ItemNames = (List<standlist>)IListDataSet.DataTableToIList<standlist>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
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
        /// 检测标准下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStandard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnStandard.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_STANDARD,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                };
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        class MsgThread : ChildThread
        {
            TestStandard wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(TestStandard wnd)
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
                    case MsgCode.MSG_STANDARD:
                        if (msg.result == true)
                        {
                            wnd.DownStand(msg.responseInfo );
                            MessageBox.Show("检测标准下载成功！");
                        }
                        wnd.SearchStand();
                        wnd.btnStandard.IsEnabled = true;
                        break;
                  
                    default:
                        break;
                }
            }
        }
        private string itemstr = "";
        private void DownStand(string data)
        {
            itemstr = data;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = downdata,
                MessageInfo = "正在更新数据"
            };
            process.Start();
            SearchStand();
        }
        private void downdata(Action<int> percent)
        {
            float p = 0;
            float sp = 0;
            int showp = 0;

            percent(0);

            standardlist obj = JsonHelper.JsonToEntity<standardlist>(itemstr);
            if (obj.standard.Count > 0)
            {
                p = 0;
                p = (float)100 / (float)obj.standard.Count;

                standard sd = new standard();
                for (int i = 0; i < obj.standard.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);

                    int rt = 0;
                    sb.Length = 0;
                    sb.AppendFormat("sid='{0}'", obj.standard[i].id);
                 
                    dt = _bll.GetStandard(sb.ToString(), "", out err);
                    sd = obj.standard[i];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //if(sd.delete_flag =="1")
                        //{
                        //    rt = _bll.DeleteStandard("sid='"+sd.id +"'", "", out err);
                        //}
                        //else
                        //{
                            rt = _bll.UpdateStandard(sd);
                        //}
                        
                        
                        if (rt == 1)
                        {
                            Global.Gitem = Global.Gitem + 1;
                        }
                    }
                    else
                    {
                        rt = _bll.InsertStandard(sd);
                        if (rt == 1)
                        {
                            Global.Gitem = Global.Gitem + 1;
                        }
                    }
                }
                _bll.DeleteStandard(" delete_flag='1'", "", out err);
                percent(100);
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownfile_Click(object sender, RoutedEventArgs e)
        {
            btnDownfile.IsEnabled = false;
            if (DataGridRecord.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要下载的标准", "提示");
                btnDownfile.IsEnabled = true;
                return;
            }
            standlist stdl = null;
            stdl = (standlist)DataGridRecord.SelectedItems[0];
            string filename = stdl.url_path;//下载路径
            string localpath = System.AppDomain.CurrentDomain.BaseDirectory + "Downfile\\";//保存文件路径
            FTPDownFile fd = new FTPDownFile();
            bool down = fd.DownFtpToLocation(ftppath + filename, filename, localpath, username, password);
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
                this.DataGridRecord.DataContext = null;
                if (textBoxStdName.Text.Trim() != "" && textBoxCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" std_name like '%{0}%' and std_code like '%{1}%'", textBoxStdName.Text.Trim(), textBoxCode.Text.Trim());
                }
                else if (textBoxStdName.Text.Trim() != "")
                {
                    sb.AppendFormat(" std_name like '%{0}%'", textBoxStdName.Text.Trim());
                }
                else if (textBoxCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" std_code like '%{0}%'", textBoxCode.Text.Trim());
                }
                if (sb.Length == 0)
                {
                    sb.Append(" delete_flag='0'");
                }
                else
                {
                    sb.Append(" and delete_flag='0'");
                }
                dt = _kdb.GetStandlist(sb.ToString(), "", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<standlist> ItemNames = (List<standlist>)IListDataSet.DataTableToIList<standlist>(dt, 1);
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
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        /// <summary>
        /// 删除并更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateAllData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRecord.ItemsSource = null;
                UpdateAllData.IsEnabled = false;
                string err = "";
               
                DataGridRecord.DataContext = null;
                //int Gitem = 0;
                string lasttime = "";
              
                string str1 = Global.KjServer.KjServerAddr;
                string stdaddr = InterfaceHelper.GetServiceURL(str1, 8);//地址
                sb.Length = 0;
                sb.Append(stdaddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "standard");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                FileUtils.KLog(sb.ToString(), "发送", 6);
                string stdtype = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(stdtype, "接收", 6);
                Global.Gitem = 0;

                ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stdtype);
                if (Jresult!=null && Jresult.success)
                {
                    _bll.DeleteStandard("", "", out err);
                    DownStand(Jresult.obj.ToString());
                }

                //standardlist obj = JsonHelper.JsonToEntity<standardlist>(Jresult.obj.ToString());
                //if (obj.standard.Count > 0)
                //{
                //    standard sd = new standard();
                //    for (int i = 0; i < obj.standard.Count; i++)
                //    {
                //        int rt = 0;
                //        sb.Length = 0;
                //        sb.AppendFormat("sid='{0}'", obj.standard[i].id);

                //        dt = _bll.GetStandard(sb.ToString(), "", out err);
                //        sd = obj.standard[i];

                //        if (dt != null && dt.Rows.Count > 0)
                //        {
                //            rt = _bll.UpdateStandard(sd);
                //            if (rt == 1)
                //            {
                //                Gitem = Gitem + 1;
                //            }
                //        }
                //        else
                //        {
                //            rt = _bll.InsertStandard(sd);
                //            if (rt == 1)
                //            {
                //                Gitem = Gitem + 1;
                //            }
                //        }
                //    }
                //}
                _bll.DeleteStandard(" delete_flag='1'", "", out err);
                SearchStand();
                MessageBox.Show("数据下载成功！");
                UpdateAllData.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                UpdateAllData.IsEnabled = true;
            }

        }
    }
}
