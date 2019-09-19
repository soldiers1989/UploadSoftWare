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
using System.Data ;
using com.lvrenyang;
using DYSeriesDataSet;
using AIO.src;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// MachineItem.xaml 的交互逻辑
    /// </summary>
    public partial class MachineItem : Window
    {
        public MachineItem()
        {
            InitializeComponent();
        }
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private  StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private clsTaskOpr _Tskbll = new clsTaskOpr();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ResetMachineItem == true)
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }
            SearchItem();
        }
        public  void SearchItem()
        {
            dt = _kdb.GetMachineItem("", "", 2, out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Smachineiten> ItemNames = (List<Smachineiten>)IListDataSet.DataTableToIList<Smachineiten>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }

        /// <summary>
        /// 查询检测项目
        /// </summary>
        public void SearchMachineItem()
        {
            DataTable dt = null;
            //分光度
            dt = _Tskbll.GetCheckItem("project_type='分光光度'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.FGItem = (List<deviceItem>)IListDataSet.DataTableToIList<deviceItem>(dt, 1);
            }
            //胶体金
            dt = _Tskbll.GetCheckItem("project_type='胶体金'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.JTItem = (List<KJFWJTItem>)IListDataSet.DataTableToIList<KJFWJTItem>(dt, 1);
            }
            //干化学
            dt = _Tskbll.GetCheckItem("project_type='干化学'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.GHItem = (List<KJFWGHXItem>)IListDataSet.DataTableToIList<KJFWGHXItem>(dt, 1);
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
                    what = MsgCode.MSG_DownMachineItem,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };
              
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
                btnDownItem.IsEnabled = true;
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
                if (textBoxName.Text.Trim() != "" )
                {
                    sb.AppendFormat(" and d.detect_item_name like '%{0}%'", textBoxName.Text.Trim());
                }

                dt = _kdb.GetMachineItem(sb.ToString(), "", 2, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Smachineiten> ItemNames = (List<Smachineiten>)IListDataSet.DataTableToIList<Smachineiten>(dt, 1);
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
            this.DataGridRecord.DataContext = null;
            try
            {
                string lasttime = "";
                string url3 = Global.samplenameadapter[0].url; 

                string CItemAddr = InterfaceHelper.GetServiceURL(url3, 5);//地址
                //dt = _bll.GetRequestTime("", "", out err);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    lasttime = dt.Rows[0]["MachineItem"].ToString();
                //}

                _bll.DeleteMachineItem("", "", out err);

                sb.Length = 0;
                Global.Gitem = 0;
                sb.Append(CItemAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "deviceItem");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);//lastDateTime 全部数据 lastUpdateTime
                FileUtils.KLog(sb.ToString(), "发送", 10);
                string Citemlist = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(Citemlist, "接收", 10);
                if (Citemlist.Length > 0)
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(Citemlist);
                    DeviceCheckItem obj = JsonHelper.JsonToEntity<DeviceCheckItem>(Jresult.obj.ToString());
                    if (obj.deviceItem.Count > 0)
                    {
                        deviceItem DI = new deviceItem();
                        for (int i = 0; i < obj.deviceItem.Count; i++)
                        {
                            int rt = 0;
                            DataTable mdt = _bll.GetMachineSave("mid='" + obj.deviceItem[i].id + "'", "", out err);
                            if (mdt != null && mdt.Rows.Count > 0)//记录存在就更新
                            {
                                DI = obj.deviceItem[i];

                                rt = _bll.UpdateMachineItem(DI, out err);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                DI = obj.deviceItem[i];

                                rt = _bll.IsertMachineItem(DI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
                //更新请求时间
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, out err);
                SearchItem();
                SearchMachineItem();
                MessageBox.Show("共成功下载"+Global.Gitem +"条数据");
                btnDelete.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
                btnDelete.IsEnabled = true;
            }
        }
    }
    class MsgThread : ChildThread
    {
        MachineItem wnd;
        bool _checkedDown = true;
        private delegate void UIHandleMessageDelegate(Message msg);
        private UIHandleMessageDelegate uiHandleMessageDelegate;

        public MsgThread(MachineItem wnd)
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
                case MsgCode.MSG_DownMachineItem:  
                    if (msg.result == true)
                    {
                        MessageBox.Show("共成功下载"+Global.Gitem +"仪器检测项目");
                    }
                    wnd.SearchItem();
                    wnd.SearchMachineItem();
                    
                    wnd.btnDownItem.IsEnabled = true;
                    break;
                //case MsgCode.MSG_DownTask:
                    //if (msg.responseInfo != "0" && wnd._isShowBox)
                    //{
                    //    wnd._isShowBox = false;
                    //    wnd.LabelInfo.Content = "信息:任务更新完成";
                    //    wnd.btnTaskUpdate.Content = "任务更新";
                    //    wnd.btnTaskUpdate.FontSize = 24;
                    //    MessageBox.Show("任务更新完成,共下载" + msg.responseInfo + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    //    wnd.btnTaskUpdate.IsEnabled = true;
                    //}
                    //wnd.btnTaskUpdate.IsEnabled = true;
                    //wnd.btnTaskUpdate.Content = "任务更新";
                    //wnd.LabelInfo.Content = "信息:任务更新完成";
                    //wnd.btnTaskUpdate.FontSize = 24;
                    //wnd.SearchTask();
                    //MessageBox.Show("任务更新完成,共下载" + msg.responseInfo + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                   
                    //break;
                default:
                    break;
            }
        }
    }
}
