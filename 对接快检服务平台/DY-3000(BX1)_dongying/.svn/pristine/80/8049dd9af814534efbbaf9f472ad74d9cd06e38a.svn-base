using AIO.src;
using com.lvrenyang;
using DyInterfaceHelper;
using DYSeriesDataSet;
using DYSeriesDataSet.KjService;
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
    /// SampleTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SampleTypeWindow : Window
    {
        tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
        private MsgThread _msgThread;
        public SampleTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchSample();
        }

        private void SearchSample()
        {
            try
            {
                string err = "";
                DataTable dt = bll.GetSample("", "", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<samples> ItemNames = (List<samples>)IListDataSet.DataTableToIList<samples>(dt, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
           
        }
        /// <summary>
        /// 查询样品名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("food_name like '%{0}%'", Txt_name.Text.Trim());
                string err = "";
                DataTable dt = bll.GetSample(sb.ToString(), "", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<samples> ItemNames = (List<samples>)IListDataSet.DataTableToIList<samples>(dt, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Global.KjServer.registerDeviceEntity == null)
            {
                MessageBox.Show("请到设置界面注册后再重置");
                return;
            }

            if (Global.KjServer.userLoginEntity == null)
            {
                MessageBox.Show("请到设置界面通信测试后再重置");
                return;
            }
            
            Btn_ReUpdate.IsEnabled = false;
            try
            {
                string lasttime = "";
                string err = "";
                int icount = 0;
                string str1 = Global.KjServer.KjServerAddr;
                string saddr = InterfaceHelper.GetServiceURL(str1, 8);//地址

                bll.Deletefoodtype("", "", out err);
                DataGridRecord.DataContext = null;
                //dt = _bll.GetRequestTime("", "", out err);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    lasttime = dt.Rows[0]["Foodtype"].ToString();
                //} int Gitem = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append(saddr);
                sb.AppendFormat("?userToken={0}", Global.KjServer.userLoginEntity.token);
                sb.AppendFormat("&type={0}", "food");
                sb.AppendFormat("&serialNumber={0}", Global.KjServer.registerDeviceEntity.serial_number);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                //FileUtils.KLog(sb.ToString(), "发送", 8);
                string stype = InterfaceHelper.HttpsPost(sb.ToString());
                //FileUtils.KLog(stype, "接收", 8);
               
                ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
                if (obj.food.Count > 0)
                {
                    foodtype ft = new foodtype();
                    for (int i = 0; i < obj.food.Count; i++)
                    {
                        int rt = 0;
                        sb.Length = 0;
                        sb.AppendFormat("fid='{0}' ", obj.food[i].id);

                        DataTable dt = bll.Getfoodtype(sb.ToString(), "", out err);

                        ft = obj.food[i];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            rt = bll.Updatefoodtype(ft);
                            if (rt == 1)
                            {
                                icount = icount + 1;
                            }
                        }
                        else
                        {
                            rt = bll.Insertfoodtype(ft);
                            if (rt == 1)
                            {
                                icount = icount + 1;
                            }
                        }
                    }
                }

                SearchSample();
                MessageBox.Show("共成功下载" + icount + "条数据");
                Btn_ReUpdate.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Btn_ReUpdate.IsEnabled = true;
            }
        }

        class MsgThread : ChildThread
        {
            SampleTypeWindow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(SampleTypeWindow wnd)
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
                    case MsgCode.MSG_SAMPLETYPE:
                        if (msg.result == true)
                        {
                            MessageBox.Show("样品种类下载成功，共成功下载 " + msg.str6 + " 条数据");
                        }
                        wnd.BtnSearch.IsEnabled = true;
                        wnd.SearchSample();
                        break;
                  
                    default:
                        break;
                }
            }
        }

        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (Global.KjServer.userLoginEntity==null  )
            {
                MessageBox.Show("请到设置界面通信测试后再更新");
                return;
            }
            if (Global.KjServer.registerDeviceEntity== null )
            {
                MessageBox.Show("请到设置界面注册后再更新");
                return;
            }
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Btn_Refresh.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SAMPLETYPE,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                };

                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
                Btn_Refresh.IsEnabled = true;
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
