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
using System.Data ;
using DYSeriesDataSet;
using AIO.src;
using DYSeriesDataSet.DataSentence.Kjc;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// InspectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InspectWindow : Window
    {
        private MsgThread _msgThread;
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private string err = "";
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        public InspectWindow()
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
                dt = _Tskbll.GetTestData("", "IsTest='否'", 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<TestSamples> Items = (List<TestSamples>)IListDataSet.DataTableToIList<TestSamples>(dt, 1);
                    DataGridRecord.ItemsSource = Items;
                }
                else
                {
                    DataGridRecord.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// 清空数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否清空被检数据", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _Tskbll.DeleteBeiHaiData("","",out err);
                MessageBox.Show("数据删除成功！");
                SearchSample();
            }
        }
        /// <summary>
        /// 下载待检数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownTestData_Click(object sender, RoutedEventArgs e)
        {
            this.LabelInfo.Content = "信息:正在更新任务";
            this.btnDownTestData.Content = "正在更新···";
            this.btnDownTestData.IsEnabled = false;

            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownTask,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };
           
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }
        private string SaveSample(string data)
        {
            string scount = "";
            string err = "";
            int count = 0;
            try
            { 
                Beihaisamples sample = JsonHelper.JsonToEntity<Beihaisamples>(data);
                if (sample.samplings.Count > 0)
                {
                    TestSamples model = new TestSamples();
                    for (int i = 0; i < sample.samplings.Count; i++)
                    {
                        sb.Length = 0;
                        sb.AppendFormat("productId='{0}' and ", sample.samplings[i].productId);
                        sb.AppendFormat("goodsName='{0}' and ", sample.samplings[i].goodsName);
                        sb.AppendFormat("operateId='{0}' and ", sample.samplings[i].operateId );
                        sb.AppendFormat("operateName='{0}' and ", sample.samplings[i].operateName);
                        sb.AppendFormat("marketId='{0}' and ", sample.samplings[i].marketId);
                        sb.AppendFormat("marketName='{0}' and ", sample.samplings[i].marketName);
                        sb.AppendFormat("samplingPerson='{0}' and ", sample.samplings[i].samplingPerson);
                        sb.AppendFormat("samplingTime='{0}' and ", sample.samplings[i].samplingTime);
                        sb.AppendFormat("positionAddress='{0}' and ", sample.samplings[i].positionAddress);
                        sb.AppendFormat("goodsId='{0}'", sample.samplings[i].goodsId);

                        dt = _Tskbll.GetTestData(sb.ToString(), "", 1, out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            continue;
                        }
                        model.productId = sample.samplings[i].productId;
                        model.goodsName = sample.samplings[i].goodsName;
                        model.operateId = sample.samplings[i].operateId;
                        model.operateName = sample.samplings[i].operateName;
                        model.marketId = sample.samplings[i].marketId;
                        model.marketName = sample.samplings[i].marketName;
                        model.samplingPerson = sample.samplings[i].samplingPerson;
                        model.samplingTime = sample.samplings[i].samplingTime;
                        model.positionAddress = sample.samplings[i].positionAddress;
                        model.goodsId = sample.samplings[i].goodsId;
                        
                        _Tskbll.InsertTestSample(model, out err);
                        count = count + 1;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return scount = count.ToString();
        }


        class MsgThread : ChildThread
        {
            InspectWindow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(InspectWindow wnd)
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
                        if (msg.result == true)
                        {
                            string c = "";
                            try
                            {
                                c= wnd.SaveSample(msg.DownLoadTask);
                                MessageBox.Show("共成功下载"+c+"条数据！","提示",MessageBoxButton.OK ,MessageBoxImage.Information);
                                wnd.SearchSample();//查询数据显示
                                wnd.btnDownTestData.FontSize = 24;
                                wnd.btnDownTestData.Content = "下载数据";
                                wnd.btnDownTestData.IsEnabled = true;
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                wnd.btnDownTestData.FontSize = 24;
                                wnd.btnDownTestData.Content = "下载数据";
                                wnd.btnDownTestData.IsEnabled = true;
                                wnd.LabelInfo.Content = "信息:任务更新失败";
                                MessageBox.Show("任务更新失败!请联系管理员!\n" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                            finally
                            {
                                wnd.LabelInfo.Content = "信息:共成功下载"+c+"条数据！";
                            }
                        }
                        else
                        {
                            wnd.LabelInfo.Content = "信息:任务更新失败";
                            wnd.btnDownTestData.FontSize = 24;
                            wnd.btnDownTestData.Content = "任务更新";
                            wnd.btnDownTestData.IsEnabled = true;
                            MessageBox.Show("任务更新失败,或者服务链接不正常，请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                        }

                        break;
                    default:
                        break;
                }
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;

            DataGridRow dataGridRow = e.Row;
            TestSamples dataRow = e.Row.Item as TestSamples;
            if (dataRow.IsTest == "是")
            {
                dataGridRow.Background = Brushes.YellowGreen;
            }
            else
            {
                dataGridRow.Background = Brushes.White;
            }
        }

    }
}
