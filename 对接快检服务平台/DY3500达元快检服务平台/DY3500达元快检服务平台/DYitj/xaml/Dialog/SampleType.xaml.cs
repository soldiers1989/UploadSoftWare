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
using System.Data;
using DYSeriesDataSet.DataSentence;
using com.lvrenyang;
using DYSeriesDataSet;
using AIO.src;
using DYSeriesDataSet.DataModel;
using DY.Process;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SampleType.xaml 的交互逻辑
    /// </summary>
    public partial class SampleType : Window
    {
        public SampleType()
        {
            InitializeComponent();
        }
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ResetSampleType == true)
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }
            SearchSample();
        }
        private void SearchSample()
        {
            dt = _kdb.GetSample(" checked='1' and delete_flag='0' ", "", 1, out err);
            this.DataGridRecord.DataContext = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                List<samples> ItemNames = (List<samples>)IListDataSet.DataTableToIList<samples>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.ItemsSource  = ItemNames;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 
        /// 食品种类下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownSampleType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnDownSampleType.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SAMPLETYPE,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };
             
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
                btnDownSampleType.IsEnabled = true;
            }

        }

        class MsgThread : ChildThread
        {
            SampleType wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(SampleType wnd)
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
                            wnd.downdata(msg.responseInfo);
                            string err = "";
                            wnd._bll.Deletefoodtype(" delete_flag='1' ", "", out err);
                            MessageBox.Show(wnd,"样品种类下载成功!");                 
                        }
                        wnd.btnDownSampleType.IsEnabled = true;
                        wnd.SearchSample();
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
        private string itemstr = "";
        private void downdata(string data)
        {
            if (data == "") return;
            itemstr = data;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = analyse,
                MessageInfo = "正在下载数据"
            };
            process.Start();


        }

        private void analyse(Action<int> percent)
        {
            float p = 0;
            float sp = 0;
            int showp = 0;

            percent(0);
            sampletype obj = JsonHelper.JsonToEntity<sampletype>(itemstr);
            if (obj.food.Count > 0)
            {
                p = 0;
                p = (float)30 / (float)obj.food.Count;

                foodtype ft = new foodtype();
                for (int i = 0; i < obj.food.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);

                    int rt = 0;
                    sb.Length = 0;
                    sb.AppendFormat("fid='{0}' ", obj.food[i].id);
                 
                    dt = _bll.Getfoodtype(sb.ToString(), "", out err);
                    ft = obj.food[i];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rt = _bll.Updatefoodtype(ft);
                        if (rt == 1)
                        {
                            Global.Gitem = Global.Gitem + 1;
                        }
                    }
                    else
                    {
                        rt = _bll.Insertfoodtype(ft);
                        if (rt == 1)
                        {
                            Global.Gitem = Global.Gitem + 1;
                        }
                    }
                }
            }

            percent(100);
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
                this.DataGridRecord.ItemsSource = null; 
                sb.Length = 0;
                if (textBoxSampleName.Text.Trim() != "")
                {
                    sb.AppendFormat(" food_name like '%{0}%' and checked='1' and delete_flag='0' ", textBoxSampleName.Text.Trim());
                }
                else
                {
                    sb.AppendFormat(" checked='1' and delete_flag='0' ");
                }

                dt = _kdb.GetSample(sb.ToString(), "", 1, out err);
               
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<samples> ItemNames = (List<samples>)IListDataSet.DataTableToIList<samples>(dt, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.ItemsSource  = ItemNames;
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            try
            {
                string lasttime = "";
                string str1 = Global.samplenameadapter[0].url;
                string saddr = InterfaceHelper.GetServiceURL(str1, 8);//地址
                _bll.Deletefoodtype("","",out err);
                DataGridRecord.DataContext = null;
               
                int Gitem = 0;
                sb.Length = 0;
                sb.Append(saddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "food");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                FileUtils.KLog(sb.ToString(), "发送", 8);
                string stype = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(stype, "接收", 8);
                if (stype.Length > 0)
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                    if(Jresult !=null && Jresult .success )
                    {
                        downdata(Jresult.obj.ToString());
                    }
                    //sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
                    //if (obj.food.Count > 0)
                    //{
                    //    foodtype ft = new foodtype();
                    //    for (int i = 0; i < obj.food.Count; i++)
                    //    {
                    //        int rt = 0;
                    //        sb.Length = 0;
                    //        sb.AppendFormat("fid='{0}' ", obj.food[i].id);

                    //        dt = _bll.Getfoodtype(sb.ToString(), "", out err);

                    //        ft = obj.food[i];
                    //        if (dt != null && dt.Rows.Count > 0)
                    //        {
                    //            rt = _bll.Updatefoodtype(ft);
                    //            if (rt == 1)
                    //            {
                    //                Gitem = Gitem + 1;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            rt = _bll.Insertfoodtype(ft);
                    //            if (rt == 1)
                    //            {
                    //                Gitem = Gitem + 1;
                    //            }
                    //        }
                    //    }
                    //}
                }
                _bll.Deletefoodtype(" delete_flag='1' ", "", out err);
                SearchSample();
                MessageBox.Show("样品种类下载成功！");
                btnDelete.IsEnabled = true ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDelete.IsEnabled = true;
            }
        }
    }
}
