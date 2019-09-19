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

namespace AIO.xaml.KjService
{
    /// <summary>
    /// SampleItemStandard.xaml 的交互逻辑
    /// </summary>
    public partial class SampleItemStandard : Window
    {
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        public SampleItemStandard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Global.ResetSampleStd == true)
            //{
            //    btnUpdateAllData.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btnUpdateAllData.Visibility = Visibility.Collapsed;
            //}
            btnUpdateAllData.Visibility = Visibility.Visible;
            getItemStand("");
           
        }

        private void getItemStand(string where)
        {
            this.DataGridRecord.DataContext = null;
            dt = _kdb.GetSamplestd("", "", 1, out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Ssamplestd> ItemNames = (List<Ssamplestd>)IListDataSet.DataTableToIList<Ssamplestd>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string itemstr = "";
        private void analyse(string stdlist)
        {
            itemstr = stdlist;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = downdata,
                MessageInfo = "正在更新数据"
            };
            process.Start();
            
        }
       

        class MsgThread : ChildThread
        {
            SampleItemStandard wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(SampleItemStandard wnd)
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
                    case MsgCode.MSG_SampleStand:
                        if (msg.result == true)
                        {
                            Global.Gitem = 0;
                            wnd.analyse(msg.responseInfo);
                            string err = "";
                            wnd._bll.DeleteBaseData("delete_flag='1'", "", 4, out err);//删除原来所有数据
                            MessageBox.Show("检测样品标准下载成功！");
                        }
                        wnd.getItemStand("");
                        wnd.btnDownlaw.IsEnabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnDownlaw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnDownlaw.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SampleStand,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                };
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                this.DataGridRecord.DataContext = null;
                if (textBoxName.Text.Trim() != "" && textBoxSampleName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and f.food_name like '%{0}%' and d.detect_item_name like '%{1}%'", textBoxSampleName.Text.Trim(), textBoxName.Text.Trim());
                }
                else if (textBoxName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and d.detect_item_name like '%{0}%'", textBoxName.Text.Trim());
                }
                else if (textBoxSampleName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and f.food_name like '%{0}%'", textBoxSampleName.Text.Trim());
                }

                dt = _kdb.GetSamplestd(sb.ToString(), "", 1, out err);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Ssamplestd> ItemNames = (List<Ssamplestd>)IListDataSet.DataTableToIList<Ssamplestd>(dt, 1);
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
        /// 数据重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAllData_Click(object sender, RoutedEventArgs e)
        {
            DataGridRecord.ItemsSource = null;
            btnUpdateAllData.IsEnabled = false;

            string  lasttime = "";

            Global.Gitem = 0;
           
            string stdaddr = InterfaceHelper.GetServiceURL(Global.KjServer.KjServerAddr, 8);//地址
            StringBuilder sbs = new StringBuilder();
            sbs.Append(stdaddr);
            sbs.AppendFormat("?userToken={0}", Global.Token);
            sbs.AppendFormat("&type={0}", "foodItem");
            sbs.AppendFormat("&serialNumber={0}", Global.MachineNum);
            sbs.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
            FileUtils.KLog(sbs.ToString(), "发送", 9);
            string stdtype = InterfaceHelper.HttpsPost(sbs.ToString());
            FileUtils.KLog(stdtype, "接收", 9);
           
            ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stdtype);
            if (Jresult != null && Jresult.success)
            {
                _bll.DeleteBaseData("", "", 4, out err);//删除原来所有数据
                analyse(Jresult.obj.ToString());   
            }
            _bll.DeleteBaseData("delete_flag='1'", "", 4, out err);//删除原来所有数据
            MessageBox.Show("数据下载成功！");
            btnUpdateAllData.IsEnabled = true;

        }
        private void downdata(Action<int> percent)
        {
            try
            {
                Global.Gitem = 0;
                float p = 0;
                float sp = 0;
                int showp = 0;
                percent(0);
                fooditemlist obj = JsonHelper.JsonToEntity<fooditemlist>(itemstr);
                if (obj != null && obj.foodItem !=null)
                {
                    if (obj.foodItem.Count > 0)
                    {
                        p = 0;
                        p = (float)100 / (float)obj.foodItem.Count;
                        fooditem fi = new fooditem();
                        for (int i = 0; i < obj.foodItem.Count; i++)
                        {
                            sp = sp + p;
                            showp = (int)sp;
                            percent(showp);
                            int w = 0;

                           
                            sb.Length = 0;
                            sb.AppendFormat("sid='{0}' ", obj.foodItem[i].id);
                           
                            dt = _bll.GetSampleStand(sb.ToString(), "", out err);

                            fi = obj.foodItem[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                //if (obj.foodItem[i].delete_flag =="1")
                                //{
                                //    w = _bll.DeleteBaseData("sid='" + obj.foodItem[i].id +"'", "", 4, out err);
                                //}
                                //else
                                //{
                                    w = _bll.UpdateSampleStandard(fi);
                                //}
                               
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                w = _bll.InsertSampleStandard(fi);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }

                        }
                        _bll.DeleteBaseData(" delete_flag='1'", "", 4, out err);
                    }
                }
                percent(100);
            }
            catch (Exception ex)
            {
                percent(100);
                MessageBox.Show(ex.Message);
               
            }
        }
      
    }
}
