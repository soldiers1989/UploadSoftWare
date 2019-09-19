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
using Newtonsoft.Json;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// CountryDataSearch.xaml 的交互逻辑
    /// </summary>
    public partial class CountryDataSearch : Window
    {
        public CountryDataSearch()
        {
            InitializeComponent();
            _queryThread = new QueryThread(this);
            _queryThread.Start();
        }

        private QueryThread _queryThread = null;

        /// <summary>
        /// 条目获取URL
        /// </summary>
        private string QuickSearchUrl = "http://appcfda.drugwebcn.com/datasearch/QueryList?tableId={0}&searchF=Quick Search&pageIndex={1}&pageSize={2}&searchK={3}";
        /// <summary>
        /// 详细内容获取URL
        /// </summary>
        private string QueryRecordUrl = "http://appcfda.drugwebcn.com/datasearch/QueryRecord?tableId={0}&searchF=ID&pageIndex={1}&pageSize={2}&searchK={3}";
        /// <summary>
        /// 条目id
        /// </summary>
        private int tableId = 110;

        private int pageIndex = 1;

        private int pageSize = 100;

        private string type = "0";

        private void SettingCobx(CheckBox box)
        {
            CboxGCYP.IsChecked = false;
            CboxJKYP.IsChecked = false;
            CboxGCBJSP.IsChecked = false;
            CboxJKBJSP.IsChecked = false;
            CboxGCHZP.IsChecked = false;
            CboxJKHZP.IsChecked = false;
            CboxHGSP.IsChecked = false;
            CboxBHGSP.IsChecked = false;
            CboxQC.IsChecked = false;
            CboxSC.IsChecked = false;
            box.IsChecked = true;
        }

        /// <summary>
        /// 国产药品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxGCYP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 25;
            CheckBox cb = CboxGCYP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 进口药品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxJKYP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 36;
            CheckBox cb = CboxJKYP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 国产保健食品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxGCBJSP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 30;
            CheckBox cb = CboxGCBJSP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 进口保健食品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxJKBJSP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 31;
            CheckBox cb = CboxJKBJSP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 国产化妆品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxGCHZP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 68;
            CheckBox cb = CboxGCHZP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 进口化妆品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxJKHZP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 69;
            CheckBox cb = CboxJKHZP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex == 1)
            {
                MessageBox.Show("已经是第一页了！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            pageIndex--;
            QuickSearch();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndex++;
            QuickSearch();
        }

        /// <summary>
        /// 跳转页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuickSearch();
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPageSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int size = 0;
            if (!int.TryParse(TxtPageSize.Text.Trim(), out size))
            {
                TxtPageSize.Text = "100";
            }

            pageSize = size;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Cursor = Cursors.Wait;
                type = "1";
                QuickSearch model = (QuickSearch)DataGridRecord.SelectedItems[0];

                Message msg = new Message()
                {
                    what = MsgCode.MSG_GETCOUNTRYDATA,
                    str1 = type,
                    str2 = tableId.ToString(),
                    str3 = pageIndex.ToString(),
                    str4 = pageSize.ToString(),
                    str5 = model.ID
                };
                Global.updateThread.SendMessage(msg, _queryThread);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Cursor = Cursors.Wait;
                type = "1";
                QuickSearch model = (QuickSearch)DataGridRecord.SelectedItems[0];

                Message msg = new Message()
                {
                    what = MsgCode.MSG_GETCOUNTRYDATA,
                    str1 = type,
                    str2 = tableId.ToString(),
                    str3 = pageIndex.ToString(),
                    str4 = pageSize.ToString(),
                    str5 = model.ID
                };
                Global.updateThread.SendMessage(msg, _queryThread);
            }
        }

        private class QueryThread : ChildThread
        {
            CountryDataSearch wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public QueryThread(CountryDataSearch wnd)
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // 这个函数是通过代理调用的。根据消息类别，将数据更新到UI。这里处理的是不费时的操作。
            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_GETCOUNTRYDATA:
                        if (msg.Other.Length > 0)
                        {
                            if (wnd.type == "0")
                            {
                                List<QuickSearch> QuickSearchs = (List<QuickSearch>)JsonConvert.DeserializeObject(msg.Other, typeof(List<QuickSearch>));
                                if (QuickSearchs != null && QuickSearchs.Count > 0)
                                {
                                    wnd.DataGridRecord.ItemsSource = QuickSearchs;
                                }
                                else
                                {
                                    wnd.DataGridRecord.ItemsSource = null;
                                }
                            }
                            else
                            {
                                List<QuickContent> QuickContents = (List<QuickContent>)JsonConvert.DeserializeObject(msg.Other, typeof(List<QuickContent>));
                                if (QuickContents != null && QuickContents.Count > 0)
                                {
                                    CountryDataSearchShow window = new CountryDataSearchShow();
                                    window.models = QuickContents;
                                    window.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("暂时没有需要查看的信息！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                        wnd.Cursor = Cursors.Arrow;
                        break;


                    default:
                        break;
                }
            }
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            QuickSearch();
        }

        private void QuickSearch()
        {
            string val = TxtName.Text.Trim();
            if (val.Length == 0) return;
            type = "0";
            Cursor = Cursors.Wait;
            Message msg = new Message()
            {
                what = MsgCode.MSG_GETCOUNTRYDATA,
                str1 = type,
                str2 = tableId.ToString(),
                str3 = pageIndex.ToString(),
                str4 = pageSize.ToString(),
                str5 = val
            };
            Global.updateThread.SendMessage(msg, _queryThread);
        }

        /// <summary>
        /// 合格食品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxHGSP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 110;
            CheckBox cb = CboxHGSP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// 不合格食品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxBHGSP_Click(object sender, RoutedEventArgs e)
        {
            tableId = 114;
            CheckBox cb = CboxBHGSP as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// QC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxQC_Click(object sender, RoutedEventArgs e)
        {
            tableId = 91;
            CheckBox cb = CboxQC as CheckBox;
            SettingCobx(cb);
        }

        /// <summary>
        /// SC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSC_Click(object sender, RoutedEventArgs e)
        {
            tableId = 120;
            CheckBox cb = CboxSC as CheckBox;
            SettingCobx(cb);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class QuickSearch
    {
        public string COUNT { get; set; }

        public string CONTENT { get; set; }

        public string ID { get; set; }
    }

    public class QuickContent
    {
        public string NAME { get; set; }

        public string CONTENT { get; set; }
    }

}