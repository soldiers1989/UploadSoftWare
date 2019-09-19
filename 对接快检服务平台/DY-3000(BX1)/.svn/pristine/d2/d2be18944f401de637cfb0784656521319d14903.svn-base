using AIO.src;
using com.lvrenyang;
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

namespace AIO.xaml.KjService
{
    /// <summary>
    /// KJItemshow.xaml 的交互逻辑
    /// </summary>
    public partial class KJItemshow : Window
    {
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        public KJItemshow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Global.ResetItem == true)
            //{
            //    btnDelete.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btnDelete.Visibility = Visibility.Collapsed;
            //}
            btnDelete.Visibility = Visibility.Visible;
            SearchItem();
        }
        private void SearchItem()
        {
            dt = _kdb.GetItem("", "", 1, out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Items> ItemNames = (List<Items>)IListDataSet.DataTableToIList<Items>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 下载检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnDownItem.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownCheckItems,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                };
               
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
                btnDownItem.IsEnabled = true;
            }
        }
        class MsgThread : ChildThread
        {
            KJItemshow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(KJItemshow wnd)
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
                    case MsgCode.MSG_DownCheckItems:
                        if (msg.result == true)
                        {
                            MessageBox.Show("检测项目下载完成，共下载 " + Global.Gitem + " 条检测项目");

                        }
                        wnd.SearchItem();
                        wnd.btnDownItem.IsEnabled = true;
                        break;

                    default:
                        break;
                }
            }
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
                if (textBoxName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and d.detect_item_name like '%{0}%'", textBoxName.Text.Trim());
                }

                dt = _kdb.GetItem(sb.ToString(), "", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Items> ItemNames = (List<Items>)IListDataSet.DataTableToIList<Items>(dt, 1);
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
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            try
            {
                string lasttime = "";
                _bll.DeleteDetectItem("", "", out err);
                DataGridRecord.DataContext = null;

                //dt = _bll.GetRequestTime("", "", out err);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    lasttime = dt.Rows[0]["CheckItem"].ToString();
                //}
                string url2 = Global.KjServer.KjServerAddr;

                string ItemAddr = InterfaceHelper.GetServiceURL(url2, 5);//地址
                sb.Length = 0;
                sb.Append(ItemAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "item");
                sb.AppendFormat("&serialName={0}", "");
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);

                FileUtils.KLog(sb.ToString(), "发送", 7);
                string itemlist = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(itemlist, "接收", 7);
                Global.Gitem = 0;
                if (itemlist.Length > 0)
                {
                    ResultData Jitem = JsonHelper.JsonToEntity<ResultData>(itemlist);
                    detectitem obj = JsonHelper.JsonToEntity<detectitem>(Jitem.obj.ToString());
                    if (obj.detectItem.Count > 0)
                    {

                        CheckItem CI = new CheckItem();
                        for (int i = 0; i < obj.detectItem.Count; i++)
                        {
                            int rt = 0;
                            sb.ToString();
                            sb.AppendFormat("cid='{0}' ", obj.detectItem[i].id);

                            dt = _bll.GetDetectItem(sb.ToString(), "", out err);

                            CI = obj.detectItem[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                rt = _bll.UpdateDetectItem(CI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                rt = _bll.InsertDetectItem(CI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
                //更新请求时间
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 9, out err);
                SearchItem();
                MessageBox.Show("共成功下载" + Global.Gitem + "条数据");
                btnDelete.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDelete.IsEnabled = true;
            }
        }

    }
}
