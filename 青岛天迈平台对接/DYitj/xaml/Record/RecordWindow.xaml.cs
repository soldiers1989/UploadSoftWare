using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using AIO.src;
using AIO.xaml.Dialog;
using AIO.xaml.Print;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace AIO
{
    /// <summary>
    /// RecordWindow.xaml 的交互逻辑
    /// 
    /// </summary>
    public partial class RecordWindow : Window
    {

        #region 全局变量

        /// <summary>
        /// 首次进入界面时不执行下拉框中的查询方法
        /// </summary>
        private bool _IsFirst = false;
        /// <summary>
        /// 查询条件改变时需要重新计算分页
        /// </summary>
        private bool _IsCalcPage = true;
        private bool _bFilterUser = false;
        private bool _bFilterCategory = false;
        private bool _bFilterItem = false;
        private bool _bFilterMethod = false;
        private bool _bFilterDate = false;
        private string _strUser = null;
        private string _strCategory = null;
        private string _strItem = null;
        private string _strMethod = null;
        private string _strDateFrom = null, _strDateTo = null;
        private string _strRecordsDir = RecordHelper.RecordsDirectory;
        //查询的总结果在这里
        private List<Record> _records = null;
        private List<Record> _filteredRecords = null;
        private List<tlsTtResultSecond> _selectedRecords = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private QueryThread _queryThread = null;
        private List<int> _idList = null;
        private clsCompanyOpr _clsCompanyOprbll = new clsCompanyOpr();
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        bool _IsShowAll = false;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int _recordsum = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageSum = 0;
        /// <summary>
        /// 每页显示数 固定每页显示15条数据
        /// </summary>
        private int _pageSize = 15;
        //导出到Excel相关全局
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// 数据分析统计集合
        /// </summary>
        private DataTable _statisticalDT = null;

        private DataTable _dts = null;
        /// <summary>
        /// 用于检测结果界面链接过来直接查看当前检测项目的数据
        /// </summary>
        public string _CheckItemName = string.Empty;

        #endregion

        public RecordWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxItem.Text = _CheckItemName;
            Global.IsContrast = false;
            btnDeleted.Visibility = Global.IsDELETED.Equals("Y") ? Visibility.Visible : Visibility.Collapsed;
            SetComboBox();
            SearchRecord();
            _queryThread = new QueryThread(this);
            _queryThread.Start();
        }

        private void SetComboBox()
        {
            List<string> comboList = new List<string>
            {
                "全部",
                "分光光度",
                "胶体金",
                "干化学",
                "重金属"
            };
            ComboBoxCategory.ItemsSource = comboList;
            SetItems("all");
        }

        /// <summary>
        /// fgd|jtj|ghx|zjs
        /// </summary>
        /// <param name="type"></param>
        private void SetItems(String type)
        {
            IList<string> comboList = new List<string>
            {
                "全部"
            };

            //分光光度
            //if (Global.set_IsOpenFgd && (type.Equals("all") || type.Equals("fgd")))
            if (type.Equals("all") || type.Equals("fgd"))
            {
                comboList.Add("------分光光度------");
                comboList.Add("抑制率法"); comboList.Add("标准曲线法"); comboList.Add("动力学法"); comboList.Add("系数法");
            }
            //胶体金
            //if (Global.set_IsOpenJtj && (type.Equals("all") || type.Equals("jtj")))
            if (type.Equals("all") || type.Equals("jtj"))
            {
                comboList.Add("------胶体金------");
                comboList.Add("定性消线法"); comboList.Add("定性比色法"); comboList.Add("定量法(T)"); comboList.Add("定量法(T/C)"); comboList.Add("定性比色法(T/C)");
            }
            //干化学
            //if (Global.set_IsOpenGhx && (type.Equals("all") || type.Equals("ghx")))
            if (type.Equals("all") || type.Equals("ghx"))
            {
                comboList.Add("------干化学------");
                comboList.Add("定性法"); comboList.Add("定量法");
            }
            //重金属
            //if (Global.set_IsOpenZjs && (type.Equals("all") || type.Equals("zjs")))
            //{
            //    comboList.Add("------重金属------");
            //}
            ComboBoxMethod.ItemsSource = comboList;
        }

        private void ShowResult(string WhereSql, string OrderBy, int index)
        {
            this.DataGridRecord.DataContext = null;
            DataTable dt = null;
            try
            {
                if (_IsShowAll)
                {
                    _dts = _resultTable.GetAsDataTable(WhereSql, OrderBy, 4, 0);
                    if (_dts != null && _dts.Rows.Count > 0)
                        DataGridRecord.DataContext = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(_dts, 1);
                    else
                        DataGridRecord.DataContext = null;
                }
                else
                {
                    if (_IsCalcPage)
                    {
                        _dts = _resultTable.GetAsDataTable(WhereSql, OrderBy, 4, 5);
                        if (_dts != null && _dts.Rows.Count > 0)
                        {
                            _recordsum = _dts.Rows.Count;
                            //计算分页
                            _pageSum = Global.PageCount(_recordsum, _pageSize);
                            _IsCalcPage = false;
                        }
                        else
                        {
                            labelCount.Content = "当前为第*页,共*页";
                            btnHomePage.IsEnabled = false;
                            btnEndPage.IsEnabled = false;
                            btnUpPage.IsEnabled = false;
                            btnNextPage.IsEnabled = false;
                            labelCount.IsEnabled = false;
                        }
                    }
                    dt = Global.GetPagedTable(_dts, _pageIndex, _pageSize);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataGridRecord.DataContext = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(dt, 1);
                        if (DataGridRecord.Items.Count > 0)
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(ProcessRows));
                            _IsFirst = true;
                        }
                    }
                    else
                        DataGridRecord.DataContext = null;
                }
                _statisticalDT = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessRows()
        {
            labelCount.Content = string.Format("当前为第{0}页，共{1}页", _pageIndex, _pageSum);
            btnHomePage.IsEnabled = btnUpPage.IsEnabled = _pageIndex == 1 ? false : true;
            btnEndPage.IsEnabled = btnNextPage.IsEnabled = _pageIndex == _pageSum ? false : true;
            textBoxPage.Text = _pageIndex.ToString();
            List<tlsTtResultSecond> Items = new List<tlsTtResultSecond>();
            //获取单元行
            try
            {
                for (int i = 0; i < DataGridRecord.Items.Count; i++)
                {
                    Items.Add((tlsTtResultSecond)DataGridRecord.Items[i]);
                    var row = DataGridRecord.ItemContainerGenerator.ContainerFromItem(DataGridRecord.Items[i]) as DataGridRow;
                    if (row != null)
                    {
                        if (Items[i].IsUpload.Equals("已上传"))
                            row.Background = new SolidColorBrush(Colors.Green);
                        else if (Items[i].IsUpload.Equals("上传后有修改"))
                            row.Background = new SolidColorBrush(Colors.Yellow);
                        //获取单元格
                        DataRowView drv = DataGridRecord.Items[i] as DataRowView;
                        if (Items[i].Result != null && Items[i].Result.Equals("不合格"))
                        {
                            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(9);
                            cell.Background = new SolidColorBrush(Colors.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T childContent = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                childContent = v as T;
                if (childContent == null)
                {
                    childContent = GetVisualChild<T>(v);
                }
                if (childContent != null)
                {
                    break;
                }
            }
            return childContent;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _queryThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRecords = new List<tlsTtResultSecond>();
            for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
            {
                tlsTtResultSecond record = (tlsTtResultSecond)DataGridRecord.SelectedItems[i];
                _selectedRecords.Add(record);
            }
        }

        private void SaveAnother()
        {
            string path = string.Empty; ;
            string content = string.Empty;
            for (int i = 0; i < DataGridRecord.Items.Count; i++)
            {
                tlsTtResultSecond record = (tlsTtResultSecond)DataGridRecord.Items[i];
                content += record.ToString() + "\r\n";
            }
            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog()
            {
                Filter = "txt files(*.txt)|*.txt"
            };
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                path = sf.FileName;
            if (!string.IsNullOrEmpty(path))
                FileUtils.AddToFile(path, content);
            else
            {
                MessageBox.Show(this, "路径无效!请选择正确的路劲!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择打印条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            LabelInfo.Content = "正在打印...";
            // 将时间相同的项合并打印
            List<string> dates = new List<string>();
            List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
            List<byte> data = new List<byte>();
            try
            {
                foreach (tlsTtResultSecond record in _selectedRecords)
                {
                    if (dates.Contains(record.CheckStartDate))
                    {
                        int idx = dates.IndexOf(record.CheckStartDate);
                        reports[idx].SampleName.Add(record.FoodName);
                        reports[idx].SampleNum.Add(record.SampleCode);
                        reports[idx].JudgmentTemp.Add(record.Result);
                        reports[idx].Result.Add(record.CheckValueInfo);
                    }
                    else
                    {
                        dates.Add(record.CheckStartDate);
                        PrintHelper.Report report = new PrintHelper.Report()
                        {
                            ItemName = record.CheckTotalItem,
                            ItemCategory = record.ResultType,
                            User = record.CheckUnitInfo,
                            Unit = record.ResultInfo,
                            Date = record.CheckStartDate,
                            Judgment = record.FoodName
                        };
                        report.SampleName.Add(record.FoodName);
                        report.SampleNum.Add(record.SampleCode);
                        report.JudgmentTemp.Add(record.Result);
                        report.Result.Add(record.CheckValueInfo);
                        if (!record.ContrastValue.Equals("NULL") && record.ContrastValue.Length > 0)
                        {
                            double ContrastValue = 0;
                            if (double.TryParse(record.ContrastValue, out ContrastValue))
                                report.ContrastValue = ContrastValue.ToString("F3");
                        }
                        reports.Add(report);
                    }
                }
                foreach (PrintHelper.Report report in reports)
                    data.AddRange(report.GeneratePrintBytes());
                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                Message msg = new Message()
                {
                    what = MsgCode.MSG_PRINT,
                    str1 = Global.strPRINTPORT,
                    data = buffer,
                    arg1 = 0,
                    arg2 = buffer.Length
                };
                Global.printThread.SendMessage(msg, _queryThread);
            }
            catch (Exception ex)
            {
                LabelInfo.Content = "提示：打印失败!";
                FileUtils.Log(ex.ToString());
                MessageBox.Show(this, "打印出现异常!\n错误信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonQuery_Click(object sender, RoutedEventArgs e)
        {
            SearchRecord();
        }

        public void SearchRecord()
        {
            StringBuilder sb = new StringBuilder();
            ButtonQuery.IsEnabled = false;
            _bFilterUser = ComboBoxUser.Text.Trim().Equals(string.Empty) ? false : true;
            _bFilterCategory = ComboBoxCategory.Text.Trim().Equals("全部") ? false : true;
            _bFilterItem = ComboBoxItem.Text.Trim().Equals(string.Empty) ? false : true;
            _bFilterMethod = ComboBoxMethod.Text.Trim().Equals("全部") ? false : true;
            if (DatePickerDateTo.SelectedDate != null || DatePickerDateFrom.SelectedDate != null)
                _bFilterDate = true;
            if (_bFilterUser)
                sb.AppendFormat(" CheckUnitInfo='{0}' and", ComboBoxUser.Text.Trim());
            if (_bFilterCategory)
                sb.AppendFormat(" ResultType='{0}' and", ComboBoxCategory.Text.Trim());
            if (_bFilterItem)
                sb.AppendFormat(" CheckTotalItem Like '%{0}%' and", ComboBoxItem.Text.Trim());
            if (_bFilterMethod)
                sb.AppendFormat(" CheckMethod='{0}' and", ComboBoxMethod.Text.Trim());
            if (_bFilterDate)
            {
                try
                {
                    DateTime from = DatePickerDateFrom.SelectedDate != null ? (DateTime)DatePickerDateFrom.SelectedDate : new DateTime();
                    DateTime to = DatePickerDateTo.SelectedDate != null ? (DateTime)DatePickerDateTo.SelectedDate : new DateTime();
                    if (DatePickerDateFrom.SelectedDate != null && DatePickerDateTo.SelectedDate != null)
                    {
                        _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                        _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                        sb.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", _strDateFrom, _strDateTo);
                    }
                    else if (DatePickerDateFrom.SelectedDate != null && DatePickerDateTo.SelectedDate == null)
                    {
                        _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                        sb.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", _strDateFrom, "9999-01-01 00:00:00");
                    }
                    else if (DatePickerDateFrom.SelectedDate == null && DatePickerDateTo.SelectedDate != null)
                    {
                        _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                        sb.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", "1000-01-01 00:00:00", _strDateTo);
                    }
                }
                catch (Exception ex)
                {
                    FileUtils.Log(ex.ToString());
                    MessageBox.Show(this, "查询出现异常!\n错误信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (sb.Length > 0)
                ShowResult(sb.ToString(0, sb.Length - 3), " ID DESC", 1);
            else
                ShowResult(string.Empty, "  ID DESC", 1);
            ButtonQuery.IsEnabled = true;
            ButtonQuery.Content = "查询";
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            Upload();
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择上传条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            //if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("ALL"))
            //{
            //    if (Global.samplenameadapter == null || Global.samplenameadapter.Count == 0 ||
            //        Global.samplenameadapter[0].pointName.Length == 0)
            //    {
            //        MessageBox.Show(this, "请先进入设置界面进行【服务器通讯测试】!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //        return;
            //    }
            //}
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                LabelInfo.Content = "正在上传...";
                DataTable dt = ListToDataTable();
                //筛选无法上传的数据
                //1、未进行服务器通讯测试的检测数据，判定为无效数据不给上传；
                //2、如果当前用户通讯测试的检测点编号和历史数据编号不一致，不给上传；
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(this, "暂无需要上传的数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("ALL"))
                {
                    string RemoveList = string.Empty, foodName = string.Empty;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    //验证：所属行政机构、行政机构编号，检测点名称、检测点类型 是否一致
                    //    //不一致的话将数据从dt中移除并提示
                    //    if (dt.Rows[i]["CheckPlaceCode"].ToString() != Global.samplenameadapter[0].orgNum ||
                    //        dt.Rows[i]["CheckPlace"].ToString() != Global.samplenameadapter[0].orgName ||
                    //        dt.Rows[i]["CheckUnitName"].ToString() != Global.samplenameadapter[0].pointName ||
                    //        dt.Rows[i]["APRACategory"].ToString() != Global.samplenameadapter[0].pointType)
                    //    {
                    //        foodName = dt.Rows[i]["FoodName"].ToString();
                    //        RemoveList += RemoveList.Length == 0 ?
                    //            string.Format("以下样品因检测点信息不一致，将不作上传处理，请知悉：\r\n[{0}]", foodName) :
                    //            string.Format("[{0}]", foodName);
                    //        dt.Rows.RemoveAt(i);
                    //        i--;
                    //    }
                    //}

                    if (dt.Rows.Count == 0)
                    {
                        string msgb = RemoveList.Length > 0 ? string.Format("暂时没有需要上传的数据！\r\n\r\n参考信息：{0}", RemoveList) : "暂时没有需要上传的数据！";
                        LabelInfo.Content = "提示:暂时没有需要上传的数据!";
                        MessageBox.Show(this, msgb, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else if (RemoveList.Length > 0)
                    {
                        MessageBox.Show(this, RemoveList, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
                Message msg = new Message()
                {
                    what = MsgCode.MSG_UPLOAD,
                    obj1 = Global.samplenameadapter[0],
                    table = dt,
                    selectedRecords = _selectedRecords
                };
                Global.updateThread.SendMessage(msg, _queryThread);
            }
            catch (Exception ex)
            {
                LabelInfo.Content = "提示:上传失败!";
                FileUtils.Log(ex.ToString());
                MessageBox.Show(this, "上传数据时出现异常!\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DataTable ListToDataTable()
        {
            tlsTtResultSecond Rs = new tlsTtResultSecond();
            Type t = Rs.GetType();
            string[] PropertyName = new string[t.GetProperties().Length];
            int PropertyNum = 0;
            foreach (PropertyInfo pi in t.GetProperties())
            {
                PropertyName[PropertyNum] = pi.Name;
                PropertyNum++;
            }
            DataSet dto = IListDataSet.ToDataSet<tlsTtResultSecond>(_selectedRecords, PropertyName);
            return dto.Tables[0];
        }

        #region // 将records的子项添加到ComboBox
        private void AddToComboBox()
        {
            //foreach (Record record in records)
            //{
            //if (!ComboBoxUser.Items.Contains(record.User))
            //    ComboBoxUser.Items.Add(record.User);
            //if (!ComboBoxCategory.Items.Contains(record.Category))
            //    ComboBoxCategory.Items.Add(record.Category);
            //if (!ComboBoxItem.Items.Contains(record.Item))
            //    ComboBoxItem.Items.Add(record.Item);
            //if (!ComboBoxMethod.Items.Contains(record.Method))
            //    ComboBoxMethod.Items.Add(record.Method);
            //}
        }
        #endregion

        private void UpdateFilterFromUI()
        {
            _strUser = ComboBoxUser.Text;
            _strCategory = ComboBoxCategory.Text;
            _strItem = ComboBoxItem.Text;
            _strMethod = ComboBoxMethod.Text;
            try
            {
                if (DatePickerDateFrom.SelectedDate != null && DatePickerDateTo.SelectedDate != null)
                {
                    DateTime from = (DateTime)DatePickerDateFrom.SelectedDate;
                    DateTime to = (DateTime)DatePickerDateTo.SelectedDate;
                    _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                    _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        private List<Record> FilterRecords()
        {
            List<Record> fRecords = new List<Record>();
            foreach (Record record in _records)
            {
                // 筛选的意思就是，如果启用筛选，但不符合要求的，就剔除掉。
                if (_bFilterUser && !_strUser.Equals(record.User))
                    continue;
                if (_bFilterCategory && !_strCategory.Equals(record.Category))
                    continue;
                if (_bFilterItem && !_strItem.Equals(record.Item))
                    continue;
                if (_bFilterMethod && !_strMethod.Equals(record.Method))
                    continue;
                if (_bFilterDate && (!((record.Date.CompareTo(_strDateFrom) >= 0) && (record.Date.CompareTo(_strDateTo) <= 0))))
                    continue; // 日期不在限定的范围之内
                fRecords.Add(record);
            }
            return fRecords;
        }

        #region 线程

        // 开一个子线程，专门做记录查询的事情，需要查询都发给子线程去做。查询结果出来之后，调用UI线程开始更新界面。
        // 一直读取AD值
        private class QueryThread : ChildThread
        {
            RecordWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public QueryThread(RecordWindow wnd)
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
                    case MsgCode.MSG_RECORD_INIT:
                        wnd._records = RecordHelper.ReadRecord(wnd._strRecordsDir);
                        break;

                    case MsgCode.MSG_RECORD_QUERY:
                        // 查询记录
                        wnd._filteredRecords = wnd.FilterRecords();
                        break;
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
                    case MsgCode.MSG_RECORD_INIT:
                        wnd.AddToComboBox();
                        wnd.DataGridRecord.DataContext = wnd._records;
                        wnd.ButtonQuery.Content = "查  询";
                        wnd.ButtonQuery.IsEnabled = true;
                        wnd.ButtonQuery_Click(null, null); // 初始化之后，调用一次查询。
                        break;

                    case MsgCode.MSG_RECORD_QUERY:
                        wnd.ButtonQuery.Content = "查  询";
                        wnd.ButtonQuery.IsEnabled = true;
                        break;

                    case MsgCode.MSG_PRINT:
                        if (msg.result)
                        {
                            wnd.LabelInfo.Content = "提示：打印成功!";
                        }
                        else
                        {
                            wnd.LabelInfo.Content = "提示：打印失败!";
                            MessageBox.Show(wnd, "打印失败，请检查打印端口是否正确。");
                        }
                        break;
                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                        {
                            if (Global.UploadSCount > 0 && Global.UploadFCount == 0)
                            {
                                wnd.LabelInfo.Content = string.Format("提示：成功上传 {0} 条数据!", Global.UploadSCount);
                            }
                            else
                            {
                                wnd.LabelInfo.Content = string.Format("提示：成功上传 {0} 条数据!", Global.UploadSCount);
                                MessageBox.Show(wnd, string.Format("成功上传了 {0} 条数据，但有部分数据上传失败！\r\n参考信息：{1}", Global.UploadSCount, msg.errMsg), "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            if (Global.UploadSCount > 0)
                            {
                                wnd.LabelInfo.Content = string.Format("提示：成功上传 {0} 条数据!", Global.UploadSCount);
                                MessageBox.Show(wnd, string.Format("成功上传了 {0} 条数据，但有部分数据上传失败！", Global.UploadSCount), "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                wnd.LabelInfo.Content = "提示：上传失败!";
                                MessageBox.Show(wnd, "数据上传失败!\r\n\r\n异常信息：" + msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        wnd._IsCalcPage = true;
                        wnd.SearchRecord();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit()
        {
            tlsTtResultSecond record;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    record = (tlsTtResultSecond)DataGridRecord.SelectedItems[0];
                    ItemResultEdit NemWindow = new ItemResultEdit();
                    NemWindow.GetValue(record);
                    NemWindow.ShowInTaskbar = false;
                    NemWindow.Owner = this;
                    NemWindow.ShowDialog();
                    _IsCalcPage = true;
                    SearchRecord();
                }
                else
                {
                    MessageBox.Show(this, "请选择编辑条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region ExportData To Excel|Txt

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            Mouse.SetCursor(Cursors.Wait);
            if (Global.ExportType.Equals("Excel"))
            {
                ExportToExcel();
            }
            else if (Global.ExportType.Equals("Txt"))
            {
                ExportToTxt();
            }
            else
            {
                ExportToExcel();
            }
            Mouse.SetCursor(Cursors.Arrow);
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void ExportToExcel()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                if (MessageBox.Show("您未选择任何条目，点击【是】导出所有检测数据?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel (*.xls)|*.xls"
            };
            if ((bool)(saveFileDialog.ShowDialog()))
            {
                try
                {
                    DataTable dataTable = null;
                    //查询全部
                    if (null == _selectedRecords || _selectedRecords.Count == 0)
                        dataTable = _resultTable.ExportData(WhereSql(false), Global.InterfaceType, Global.EachDistrict);
                    else
                        dataTable = _resultTable.ExportData(WhereSql(true), Global.InterfaceType, Global.EachDistrict);

                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        if (ExcelHelper.CreateExcel(dataTable, saveFileDialog.FileName))
                        {
                            LabelInfo.Content = "提示：导出成功！";
                            MessageBox.Show("导出成功！本次共导出 " + dataTable.Rows.Count + " 条数据！", "系统提示");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LabelInfo.Content = "提示：导出失败！";
                    MessageBox.Show("导出异常：\n" + ex.Message);
                }
            }

            #region 2016年8月15日 wenj 使用新方法导出到Excel，新方法不依赖office组件
            //if (null == _selectedRecords || _selectedRecords.Count == 0)
            //{
            //    if (MessageBox.Show("您未选择任何条目，点击【是】导出所有检测数据?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //        return;
            //}

            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            //if ((bool)(saveFileDialog.ShowDialog()))
            //{
            //    try
            //    {
            //        ExportToExcel(saveFileDialog.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        LabelInfo.Content = "提示：导出失败!";
            //        MessageBox.Show("导出异常：\n" + ex.Message);
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// 导出查询条件
        /// </summary>
        /// <param name="isSearchAll">false 查询全部|true 根据ID查询</param>
        /// <returns>结果</returns>
        private string WhereSql(bool isSearchAll)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(" CheckUnitInfo='{0}' ", ComboBoxUser.Text.Trim());
            if (_selectedRecords != null && _selectedRecords.Count != 0 && isSearchAll)
            {
                string strId = string.Empty;
                foreach (tlsTtResultSecond record in _selectedRecords)
                    strId += record.ID + ",";
                strId += "0)";
                stringBuilder.AppendFormat("And ID IN({0}", strId);
            }
            return stringBuilder.ToString();
        }

        private void ExportToExcel(string name)
        {
            bool IsExportOK = true;
            DataTable dataTable = null;
            try
            {
                //查询全部
                if (null == _selectedRecords || _selectedRecords.Count == 0)
                    dataTable = _resultTable.ExportData(WhereSql(false), Global.InterfaceType, Global.EachDistrict);
                else
                    dataTable = _resultTable.ExportData(WhereSql(true), Global.InterfaceType, Global.EachDistrict);

                if (dataTable != null && dataTable.Rows.Count != 0)
                {
                    CreateExcelRef();
                    FillSheet(dataTable);
                    SaveExcel(name);
                }
                else
                {
                    IsExportOK = false;
                    LabelInfo.Content = "提示：暂无数据导出!";
                    MessageBox.Show("暂无任何数据可导出!", "操作提示");
                }
            }
            catch (Exception)
            {
                IsExportOK = false;
                LabelInfo.Content = "提示：导出失败!";
            }
            finally
            {
                if (IsExportOK)
                {
                    ReleaseCOM(_sheet);
                    ReleaseCOM(_sheets);
                    ReleaseCOM(_book);
                    ReleaseCOM(_books);
                    ReleaseCOM(_excelApp);
                    LabelInfo.Content = "提示：导出成功!";
                    if (MessageBox.Show("成功导出 " + dataTable.Rows.Count + " 条数据!\n点击【是】立即查看", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        Process.Start(name);
                }
                else
                {
                    MessageBox.Show("提示：导出失败!");
                }
            }
        }

        /// <summary>
        /// 释放COM对象
        /// </summary>
        /// <param name="pObj"></param>
        private void ReleaseCOM(object pObj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch (Exception ex)
            {
                LabelInfo.Content = "提示：释放资源异常!";
                MessageBox.Show("释放资源时发生错误:\n" + ex.Message);
            }
            finally
            {
                pObj = null;
            }
        }

        /// <summary>
        /// 创建一个Excel程序实例
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _books = (Microsoft.Office.Interop.Excel.Workbooks)_excelApp.Workbooks;
            _book = (Microsoft.Office.Interop.Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Microsoft.Office.Interop.Excel.Sheets)_book.Worksheets;
            _sheet = (Microsoft.Office.Interop.Excel._Worksheet)(_sheets.get_Item(1));
        }

        /// <summary>
        /// 将数据填充到内存Excel的工作表
        /// </summary>
        /// <param name="dataTable"></param>
        private void FillSheet(DataTable dataTable)
        {
            object[] header = CreateHeader(dataTable);
            WriteData(header, dataTable);
        }

        private object[] CreateHeader(DataTable dataTable)
        {
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < dataTable.Columns.Count; n++)
                objHeaders.Add(dataTable.Columns[n].ColumnName);
            var headerToAdd = objHeaders.ToArray();
            //工作表的单元是从“A1”开始
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();
            return headerToAdd;
        }

        /// <summary>
        /// 将表头加粗显示
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }

        private void WriteData(object[] header, DataTable dataTable)
        {
            object[,] objData = new object[dataTable.Rows.Count, header.Length];
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                var item = dataTable.Rows[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = dataTable.Rows[j][i];
                    objData[j, i] = (y == null) ? string.Empty : y.ToString();
                }
            }
            AddExcelRows("A2", dataTable.Rows.Count, header.Length, objData);
            AutoFitColumns("A1", dataTable.Rows.Count + 1, header.Length);
        }

        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }

        /// <summary>
        /// 将数据填充到Excel工作表的单元格中
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }

        /// <summary>
        /// 将内存中Excel保存到本地路径
        /// </summary>
        /// <param name="excelName"></param>
        private void SaveExcel(string excelName)
        {
            _excelApp.Visible = false;
            //保存为Office2003和Office2007都兼容的格式
            //if (Environment.Is64BitOperatingSystem)//获取当前windows操作系统是否是64位
            //    _book.SaveAs(excelName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //else
            _book.SaveAs(excelName, Excel.XlFileFormat.xlExcel7, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            _excelApp.Quit();
        }

        /// <summary>
        /// 导出到TXT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExportToTxt()
        {
            bool IsSuccess = false;
            try
            {
                string path = string.Empty; ;
                _selectedRecords = new List<tlsTtResultSecond>();
                string content = string.Empty;
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 1, 0);
                tlsTtResultSecond ts = new tlsTtResultSecond();
                Type t = ts.GetType();
                foreach (DataRow rw in dt.Rows)
                {
                    for (int k = 0; k < 35; k++)
                    {
                        if (k != 34)
                            content += rw[k].ToString() + "#";
                        if (k == 34)
                            content += rw[k].ToString() + "\r\n";
                    }
                }
                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog()
                {
                    Filter = "txt files(*.txt)|*.txt"
                };
                if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    path = sf.FileName;
                if (!string.IsNullOrEmpty(path))
                {
                    FileUtils.AddToFile(path, content);
                    IsSuccess = true;
                }
                else
                {
                    IsSuccess = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                FileUtils.Log(ex.ToString());
                MessageBox.Show("出错啦!" + ex.Message, "系统提示");
            }
            finally
            {
                if (IsSuccess)
                {
                    LabelInfo.Content = "提示：导出成功!";
                    MessageBox.Show("导出成功!", "操作提示");
                }
            }
        }

        #endregion

        /// <summary>
        /// 将Excel科学记数转换成常规数值
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private string ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
                dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            else
                return strData;
            return dData.ToString();
        }

        #region  Import From Excel|Txt

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            //Import();//从Txt导入数据
            ImportToExcel();//从Excel导入数据
        }

        /// <summary>
        /// 从Excel导入数据
        /// </summary>
        private void ImportToExcel()
        {
            OpenFileDialog op = new OpenFileDialog();
            bool isImport = true;
            string error = string.Empty, repeatSample = string.Empty;
            int importNum = 0, datasNum = 0;
            try
            {
                op.Filter = "Excel (*.xls)|*.*";
                if ((bool)(op.ShowDialog()))
                {
                    this.LabelInfo.Content = "提示：正在导入数据!";
                    //DataTable dt = ReadExcel(op.FileName.Trim());
                    DataTable dt = ExcelHelper.ImportExcel(op.FileName.Trim(), 0);
                    datasNum = dt.Rows.Count;
                    if (dt != null && datasNum > 0)
                    {
                        int num = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string str = ChangeDataToD(dt.Rows[i][1].ToString());//检测编号
                            //判定检测编号是否存在，若存在则不导入
                            if (_resultTable.GetRepeatCheckNo(str))
                            {
                                num++;
                                repeatSample += "【" + dt.Rows[i][5].ToString() + "】";
                                if (num == 4)
                                {
                                    num = 0;
                                    repeatSample += "\r\n";
                                }
                                continue;
                            }

                            tlsTtResultSecond model = new tlsTtResultSecond()
                            {
                                FoodName = dt.Rows[i][5].ToString(),//样品名称
                                ResultType = dt.Rows[i][0].ToString(),//项目类别
                                CheckNo = str,
                                SampleCode = String.Format("{0:D5}", int.Parse(dt.Rows[i][2].ToString()))//样品编号
                            };
                            str = dt.Rows[i][3].ToString();
                            if (str.Length < 9 && str.Length > 0)
                                model.CheckPlaceCode = string.Format("{0:000000000}", int.Parse(str));//行政机构编号
                            else if (str.Length > 9 && dt.Rows[i][3].ToString().Length < 12)
                                model.CheckPlaceCode = string.Format("{0:000000000000}", int.Parse(str));
                            else
                                model.CheckPlaceCode = str;
                            model.CheckPlace = dt.Rows[i][4].ToString();//行政机构名称
                            model.TakeDate = dt.Rows[i][6].ToString();//抽检日期
                            model.CheckStartDate = dt.Rows[i][7].ToString();//检测时间
                            model.Standard = dt.Rows[i][8].ToString();//检测依据
                            model.CheckTotalItem = dt.Rows[i][9].ToString();//检测项目
                            str = dt.Rows[i][10].ToString();
                            double CheckValue = 0;
                            bool isInt = double.TryParse(str, out CheckValue);
                            model.CheckValueInfo = isInt ? CheckValue.ToString("0.000") : str;//检测值
                            model.StandValue = dt.Rows[i][11].ToString();//检测标准值
                            model.Result = dt.Rows[i][12].ToString();//检测结论
                            model.ResultInfo = dt.Rows[i][13].ToString();//检测值单位
                            model.CheckUnitName = dt.Rows[i][14].ToString();//检测单位
                            model.CheckUnitInfo = dt.Rows[i][15].ToString();//检测人姓名
                            model.Organizer = dt.Rows[i][16].ToString();//抽样人
                            model.UpLoader = dt.Rows[i][17].ToString();//基层上传人
                            model.ReportDeliTime = dt.Rows[i][18].ToString();//检测报告送达时间
                            model.IsReconsider = dt.Rows[i][19].ToString().Equals("True") ? true : false;//是否提出复议
                            model.ReconsiderTime = dt.Rows[i][20].ToString();//提出复议时间
                            model.ProceResults = dt.Rows[i][21].ToString();//处理结果
                            model.CheckedCompanyCode = dt.Rows[i][22].ToString();//被检对象编号
                            model.CheckedCompany = dt.Rows[i][23].ToString();//被检对象名称
                            model.CheckedComDis = dt.Rows[i][24].ToString();//档口门牌号
                            model.CheckPlanCode = dt.Rows[i][25].ToString();//任务编号
                            model.DateManufacture = dt.Rows[i][26].ToString();//生产日期
                            model.CheckMethod = dt.Rows[i][27].ToString();//检测方法
                            model.APRACategory = dt.Rows[i][28].ToString();//单位类别
                            model.Hole = String.Format("{0:D5}", dt.Rows[i][29].ToString());//检测孔
                            model.SamplingPlace = dt.Rows[i][30].ToString();//抽样地点
                            model.CheckType = dt.Rows[i][31].ToString();//检测类型
                            model.IsReport = model.IsShow = model.IsUpload = "N";
                            model.ContrastValue = dt.Rows[i][32].ToString();//对照值

                            //此处trycatch的作用是兼容旧版本
                            if (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL"))
                            {
                                try
                                {
                                    model.DeviceId = dt.Rows[i][33].ToString();//唯一机器码
                                    model.SampleId = dt.Rows[i][34].ToString();//快检单号
                                }
                                catch (Exception)
                                {
                                    model.DeviceId = model.DeviceId.Length > 0 ? model.DeviceId : string.Empty;
                                    model.SampleId = model.SampleId.Length > 0 ? model.SampleId : string.Empty;
                                }
                            }
                            if (Global.EachDistrict.Equals("GS"))
                            {
                                try
                                {
                                    model.ProduceCompany = dt.Rows[i][33].ToString();//生产单位
                                }
                                catch (Exception)
                                {
                                    model.ProduceCompany = model.ProduceCompany.Length > 0 ? model.ProduceCompany : string.Empty;
                                }
                            }
                            _resultTable.Insert(model, out error);
                            importNum = error.Equals(string.Empty) ? importNum + 1 : importNum - 1;
                        }
                    }
                    else
                    {
                        isImport = false;
                        this.LabelInfo.Content = string.Empty;
                        MessageBox.Show(this, "没有需要导入的数据！", "系统提示");
                    }
                }
                else
                {
                    isImport = false;
                }
            }
            catch (Exception ex)
            {
                isImport = false;
                this.LabelInfo.Content = "提示：导入失败!";
                MessageBox.Show(this, "导入数据异常：\r\n" + ex.Message);
            }
            finally
            {
                if (isImport)
                {
                    this.LabelInfo.Content = "提示：导入成功!";
                    if (importNum > 0)
                    {
                        if (datasNum == importNum)
                        {
                            MessageBox.Show(this, "成功导入 " + importNum + " 条数据!", "系统提示");
                        }
                        else if (repeatSample.Length > 0)
                        {
                            MessageBox.Show(this, "成功导入 " + importNum + " 条数据!\r\n\r\n其中部分样品已存在，不作导入操作！\r\n" + repeatSample, "系统提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "导入成功！\r\n\r\n其中部分样品已存在，不作导入操作！\r\n" + repeatSample, "系统提示");
                    }
                    //有新数据导入后重新计算分页信息
                    _IsCalcPage = true;
                    SearchRecord();
                }
            }
        }

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public DataTable ReadExcel(string pPath)
        {
            string connString = "Driver={Driver do Microsoft Excel(*.xls)};DriverId=790;SafeTransactions=0;ReadOnly=1;MaxScanRows=16;Threads=3;MaxBufferSize=2024;UserCommitSync=Yes;FIL=excel 8.0;PageTimeout=5;";
            connString += "DBQ=" + pPath;
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand cmd = new OdbcCommand()
            {
                Connection = conn
            };
            //获取Excel中第一个Sheet名称，作为查询时的表名
            string sheetName = this.GetExcelSheetName(pPath);
            string sql = "select * from [" + sheetName.Replace('.', '#') + "$]";
            cmd.CommandText = sql;
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                ds = null;
                throw new Exception("从Excel文件中获取数据时发生错误!");
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                da.Dispose();
                da = null;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;
            }
        }

        private string GetExcelSheetName(string pPath)
        {
            //打开一个Excel应用
            _excelApp = new Excel.Application();
            if (_excelApp == null)
            {
                throw new Exception("打开Excel应用时发生错误!");
            }
            _books = _excelApp.Workbooks;
            //打开一个现有的工作薄
            _book = _books.Add(pPath);
            _sheets = _book.Sheets;
            //选择第一个Sheet页
            _sheet = (Excel._Worksheet)_sheets.get_Item(1);
            string sheetName = _sheet.Name;

            ReleaseCOM(_sheet);
            ReleaseCOM(_sheets);
            ReleaseCOM(_book);
            ReleaseCOM(_books);
            _excelApp.Quit();
            ReleaseCOM(_excelApp);
            return sheetName;
        }

        /// <summary>
        /// 从Txt导入数据
        /// </summary>
        void Import()
        {
            string path = string.Empty;
            List<tlsTtResultSecond> ts = new List<tlsTtResultSecond>();
            System.Windows.Forms.OpenFileDialog sv = new System.Windows.Forms.OpenFileDialog();
            string s = string.Empty;
            int count = 0, result = 0;
            if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = sv.FileName;
                try
                {
                    string str = File.ReadAllText(path);
                    string[] oot = str.Split('\n');
                    count = oot.Length;
                    string[] oot2;
                    string[,] TempResult = new string[count - 1, 35];
                    for (int i = 0; i < oot.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(oot[i]))
                        {
                            oot2 = oot[i].Split('#');
                            for (int k = 0; k < 35; k++)
                            {
                                if (!oot2[k].Contains("\r"))
                                    TempResult[i, k] = oot2[k];
                                if (oot2[k].Contains("\r"))
                                    TempResult[i, k] = oot2[k].Replace("\r", string.Empty);
                            }
                        }
                    }
                    result = TestResultConserve.ReadResultSave(TempResult);
                }
                catch (Exception ex)
                {
                    FileUtils.Log(ex.ToString());
                    LabelInfo.Content = "提示：导入失败!";
                    MessageBox.Show("导入时出现异常!\n错误信息：" + ex.Message);
                }
                finally
                {
                    if (result > 0)
                    {
                        LabelInfo.Content = "提示：导入成功!";
                        MessageBox.Show("成功导入 " + result + " 条数据!", "操作提示");
                    }
                    else
                    {
                        LabelInfo.Content = "提示：导入失败!";
                    }
                }
            }
        }

        #endregion

        private Int32 cbIndex = 0;
        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxMethod != null)
            {
                if (ComboBoxMethod.SelectedValue == null)
                    return;

                if (!ComboBoxMethod.SelectedValue.ToString().Equals(string.Empty) && _IsFirst)
                {
                    cbIndex = ComboBoxMethod.SelectedIndex;
                    ComboBoxMethod.Text = ComboBoxMethod.SelectedValue.ToString();
                    ComboBoxMethod.SelectedIndex = cbIndex;
                    _pageIndex = 1;
                    _IsCalcPage = true;
                    if (!IsReset) SearchRecord();
                }
            }
        }

        /// <summary>
        /// 是否是重置查询条件，重置条件时事件方法不进行查询操作以提高效率
        /// </summary>
        private bool IsReset = false;
        /// <summary>
        /// 将所有查询条件重置为最初始状态，并查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            IsReset = true;
            ComboBoxItem.Text = string.Empty;
            DatePickerDateFrom.Text = string.Empty;
            DatePickerDateTo.Text = string.Empty;
            ComboBoxMethod.SelectedIndex = 0;
            ComboBoxCategory.SelectedIndex = 0;
            _pageIndex = 1;
            _IsCalcPage = true;
            SearchRecord();
            IsReset = false;
        }

        /// <summary>
        /// 双击 进入当前行的编辑界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //双击时获取当前行的检测项目名称，然后根据名称自动查询
            //ComboBoxItem.Text = (DataGridRecord.SelectedItem as tlsTtResultSecond).CheckTotalItem;
            //Search();
            tlsTtResultSecond record;
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                record = (tlsTtResultSecond)DataGridRecord.SelectedItems[0];
                ItemResultEdit NemWindow = new ItemResultEdit();
                NemWindow.GetValue(record);
                NemWindow.ShowInTaskbar = false;
                NemWindow.Owner = this;
                NemWindow.ShowDialog();
                _IsCalcPage = true;
                SearchRecord();
            }
        }

        /// <summary>
        /// 查询项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            SearchSample searchSample = new SearchSample();
            searchSample.ShowDialog();
            if (!Global.projectName.Equals(string.Empty))
            {
                _pageIndex = 1;
                this.ComboBoxItem.Text = Global.projectName;
            }
            Global.projectName = string.Empty;
        }

        private void DatePickerDateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDateFrom.SelectedDate != null)
            {
                _IsCalcPage = true;
                _pageIndex = 1;
                SearchRecord();
            }
        }

        private void DatePickerDateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDateTo.SelectedDate != null)
            {
                _IsCalcPage = true;
                _pageIndex = 1;
                SearchRecord();
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
            if (!_IsShowAll)
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(ProcessRows));
        }

        /// <summary>
        /// 删除检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择需要删除的条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (MessageBox.Show("确定要删除所选择的内容吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int result = 0, delNum = 0;
                foreach (tlsTtResultSecond record in _selectedRecords)
                {
                    result = _resultTable.Deleted(record.ID);
                    if (result == 1)
                        delNum += 1;
                }
                if (result == 1)
                {
                    LabelInfo.Content = "提示：删除成功!";
                    MessageBox.Show(this, "成功删除 " + delNum + " 条数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    if (delNum > 1)
                        MessageBox.Show(this, "删除过程中出现了小问题，但还是成功删除了 " + delNum + " 条数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    else
                        MessageBox.Show(this, "删除失败，请联系管理员!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //删除数据后重新计算分页数据
                _IsCalcPage = true;
                SearchRecord();
            }
        }

        /// <summary>
        /// 检测记录为敏感数据，删除功能不对客户开放
        /// 在项目名称中输入260905按回车可显示删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (ComboBoxItem.Text.Equals("260905"))
                {
                    Deleted.Visibility = Visibility.Visible;
                    ComboBoxItem.Text = string.Empty;
                }
                else
                {
                    _pageIndex = 1;
                    SearchRecord();
                }
            }
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ComboBoxCategory.SelectedValue.ToString().Equals(string.Empty) && _IsFirst)
            {
                ComboBoxCategory.Text = ComboBoxCategory.SelectedValue.ToString();
                _pageIndex = 1;
                _IsCalcPage = true;
                String type = String.Empty;
                if (ComboBoxCategory.SelectedIndex == 1)
                {
                    type = "fgd";
                }
                else if (ComboBoxCategory.SelectedIndex == 2)
                {
                    type = "jtj";
                }
                else if (ComboBoxCategory.SelectedIndex == 3)
                {
                    type = "ghx";
                }
                else if (ComboBoxCategory.SelectedIndex == 4)
                {
                    type = "zjs";
                }
                else
                {
                    type = "all";
                }
                SetItems(type);
                if (!IsReset) SearchRecord();
            }
        }

        /// <summary>
        /// 根据ID修改报表状态
        /// </summary>
        /// <param name="id">id</param>
        public void UpdateReport()
        {
            string error = string.Empty;
            try
            {
                _resultTable.UpdateReport(_idList, out error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _IsCalcPage = true;
                SearchRecord();
                if (!error.Equals(string.Empty))
                    MessageBox.Show("更新报表生成状态时出现异常!\n" + error);
                _idList = null;
            }
        }

        /// <summary>
        /// 右键 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        /// <summary>
        /// 右键 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiUpload_Click(object sender, RoutedEventArgs e)
        {
            Upload();
        }

        #region 分页

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (_pageIndex > 1)
            {
                _pageIndex -= 1;
                SearchRecord();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_pageIndex < _pageSum)
            {
                _pageIndex += 1;
                SearchRecord();
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHomePage_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex = 1;
            SearchRecord();
        }

        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEndPage_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex = _pageSum;
            SearchRecord();
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = textBoxPage.Text.Trim();
                if (!str.Equals(string.Empty))
                {
                    if (int.Parse(str) < 1 || int.Parse(str) > _pageSum)
                    {
                        textBoxPage.Text = string.Empty;
                        textBoxPage.Focus();
                        return;
                    }
                    _pageIndex = int.Parse(str);
                    SearchRecord();
                }
                else
                {
                    _pageIndex = 1;
                    SearchRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string str = textBoxPage.Text.Trim();
                if (!str.Equals(string.Empty))
                {
                    TextBox textBox = sender as TextBox;
                    TextChange[] change = new TextChange[e.Changes.Count];
                    e.Changes.CopyTo(change, 0);
                    int offset = change[0].Offset;
                    if (change[0].AddedLength > 0)
                    {
                        int num = 0;
                        if (!int.TryParse(textBox.Text, out num))
                        {
                            textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                            textBox.Select(offset, 0);
                        }
                    }
                    _pageIndex = int.Parse(str);
                    if (_pageIndex <= _pageSum && _pageIndex > 0)
                        SearchRecord();
                }
                else
                {
                    _pageIndex = 1;
                    SearchRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 是否展示
        /// </summary>
        /// <param name="str">"已展示" 表示展示 string.Empty表示不展示</param>
        private void IsShow(string str)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择展示条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            string ourEgg = string.Empty;
            try
            {
                foreach (tlsTtResultSecond record in _selectedRecords)
                {
                    tlsTtResultSecond model = new tlsTtResultSecond()
                    {
                        ID = record.ID,
                        IsShow = str
                    };
                    _resultTable.UpdateShow(model, out ourEgg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _IsCalcPage = true;
            SearchRecord();
        }

        /// <summary>
        /// 右键展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiShow_Click(object sender, RoutedEventArgs e)
        {
            IsShow("已展示");
        }

        /// <summary>
        /// 右键隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiNotShow_Click(object sender, RoutedEventArgs e)
        {
            IsShow(string.Empty);
        }

        /// <summary>
        /// 右键导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiExport_Click(object sender, RoutedEventArgs e)
        {
            //ExportToTxt();
            ExportToExcel();
        }

        /// <summary>
        /// 右键导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiImport_Click(object sender, RoutedEventArgs e)
        {
            Import();
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        private string path = Environment.CurrentDirectory;

        private string GetHtmlDoc()
        {
            string htmlDoc = string.Empty;
            try
            {
                string Organization = Global.samplenameadapter[0].orgName;
                string Result = string.Empty;
                if (_selectedRecords != null && _selectedRecords.Count > 0)
                {
                    tlsTtResultSecond md = _selectedRecords[0];
                    string CheckPlanCode = md.CheckPlanCode;
                    if (CheckPlanCode.Length > 0)
                    {
                        string[] str = CheckPlanCode.Split('-');
                        CheckPlanCode = str[0];
                    }
                    else
                    {
                        CheckPlanCode = "/";
                    }
                    Result = md.Result;
                    htmlDoc = System.IO.File.ReadAllText(path + "\\Others\\CheckedReportTitle.txt", System.Text.Encoding.Default);
                    htmlDoc += string.Format("<div class=\"number\">编号：{0}</div>", md.CheckNo.Length > 0 ? md.CheckNo : "/");
                    htmlDoc += "<div style=\"float:left; margin-left:40px; margin-top:20px;\">";
                    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

                    htmlDoc += "<tbody><tr><th width=\"190\" height=\"76\" class=\"as\">样品名称</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as\">{0}</td>", md.FoodName.Length > 0 ? md.FoodName : "/");
                    htmlDoc += "<th width=\"190\" height=\"76\" class=\"as\">唯一性单号或样品单号</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td></tr>", md.SampleId.Length > 0 ? md.SampleId : "/");

                    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）时间</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", md.CheckStartDate.Length > 0 ? DateTime.Parse(md.CheckStartDate).AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）地点</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", md.CheckedCompany.Length > 0 ? md.CheckedCompany.ToString() : "/");

                    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">区县所</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", Organization.Length > 0 ? Organization : "/");
                    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">计划编号</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckPlanCode);

                    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">检测时间</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", md.CheckStartDate.Length > 0 ? md.CheckStartDate : "/");
                    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">检测依据</th>";
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", md.Standard.Length > 0 ? md.Standard : "/");
                    htmlDoc += "</tbody></table>";

                    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测项目</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测仪器</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测结果</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">限定值</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\" id=\"unique\">检测结论</th></tr>";

                    htmlDoc += string.Format("<tr><td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckTotalItem.Length > 0 ? md.CheckTotalItem : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckMachine.Length > 0 ? md.CheckMachine : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckValueInfo.Length > 0 ? md.CheckValueInfo + (Result.Equals("阴性") || Result.Equals("阴性") ? string.Empty : (" " + md.ResultInfo)) : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.StandValue.Length > 0 ? md.StandValue + " " + md.ResultInfo : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\" id=\"unique\">{0}</td></tr>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");

                    htmlDoc += "</tbody></table>";

                    htmlDoc += string.Format("<div class=\"forming\"><div class=\"conclusion\">结论：{0}</div>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");
                    htmlDoc += "<div class=\"img\"><img src=\"CheckedReportOfficialSeal.gif\"></div>";
                    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-54px;font-size:16px;\">";
                    htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                    htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">检测单位：{0}</td>", md.CheckUnitName.Length > 0 ? md.CheckUnitName : "/");
                    htmlDoc += "</tr></tbody></table>";

                    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-20px;font-size:16px;\">";
                    htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                    htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">报告时间：{0}</td>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    htmlDoc += "</tr></tbody></table></div>";

                    htmlDoc += "<div class=\"forming\" style=\"height:54px; margin-top:0; float:left;\"><div class=\"note\"><span class=\"NT\">备注：检测结论为不合格时，应该及时上报。</span></div></div></div>";
                    htmlDoc += string.Format("<div class=\"personnel\">检测人员：{0}</div>", md.CheckUnitInfo.Length > 0 ? md.CheckUnitInfo : "/");
                    htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">审核：</div>";
                    htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">批准：</div>";
                    htmlDoc += "<div style=\" float:left; width:800px; height:60px;\"></div></div></body></html>";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return htmlDoc;
        }

        private void SaveFile(string str)
        {
            if (!(Directory.Exists(path + "\\Others")))
            {
                Directory.CreateDirectory(path + "\\Others");
            }
            string filePath = path + "\\Others\\CheckedReportModel.html";
            //如果存在则删除
            //if (File.Exists(filePath)) System.IO.File.Delete(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void PrintCheckedReport()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择打印条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            try
            {
                SaveFile(GetHtmlDoc());
                //打印检测报告
                dlReportForm window = new dlReportForm()
                {
                    HtmlUrl = path + "\\Others\\CheckedReportModel.html"
                };
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("打印检测报告时出现异常！\r\n\r\n异常信息：\r\n{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 打印检测报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiPrintCheckedReport_Click(object sender, RoutedEventArgs e)
        {
            PrintCheckedReport();
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiReportPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
            {
                GenerateReports();
            }
            else if (Global.EachDistrict.Equals("GS"))
            {
                GenerateReportsGS();
            }
        }

        private void miShowReportPrint_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 甘肃报表
        /// key=样品名称+被检单位+抽样日期
        /// </summary>
        private void GenerateReportsGS()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "未选择任何条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            string error = string.Empty, strErr = string.Empty;
            if (_idList == null)
                _idList = new List<int>();

            clsReportGS report = new clsReportGS();
            Dictionary<string, clsReportGS> dicReprot = new Dictionary<string, clsReportGS>();
            List<string> strKey = new List<string>();
            try
            {
                foreach (tlsTtResultSecond record in _selectedRecords)
                {
                    if (record.IsReport.Equals(string.Empty))
                        _idList.Add(record.ID);
                    string key = record.FoodName + "_" + record.CheckedCompany + "_" + record.TakeDate;
                    if (!dicReprot.ContainsKey(key))
                    {
                        report = new clsReportGS()
                        {
                            Title = "食品快速检验报告",
                            FoodName = record.FoodName,//食品名称
                            FoodType = string.Empty,//食品类型
                            ProductionDate = record.DateManufacture,//生产/加工/购进日期
                            CheckedCompanyName = record.CheckedCompany,//被抽样单位名称
                            CheckedCompanyAddress = string.Empty,//被抽样单位地址
                            CheckedCompanyPhone = string.Empty,//被抽样单位联系电话
                            LabelProducerName = string.Empty,//标示生产者名称
                            LabelProducerAddress = string.Empty,//标示生产者地址
                            LabelProducerPhone = string.Empty,//标示生产者电话
                            SamplingData = record.TakeDate,//抽样日期
                            SamplingPerson = record.Organizer,//抽样人
                            SampleNum = string.Empty,//样品数量
                            SamplingBase = string.Empty, //抽样基数
                            SamplingAddress = record.SamplingPlace,//抽样地点
                            SamplingOrderCode = record.SampleCode,//抽样单编号
                            Standard = record.Standard,//检验依据
                            InspectionConclusion = "合格",//检验结论
                            Notes = string.Empty,//备注
                            Audit = string.Empty,//审核
                            Surveyor = record.CheckUnitInfo//检验人
                        };
                        clsReportGS.ReportDetail reportDetail = new clsReportGS.ReportDetail()
                        {
                            ProjectName = record.CheckTotalItem,
                            Unit = record.ResultInfo,
                            InspectionStandard = record.StandValue,
                            IndividualResults = record.CheckValueInfo,
                            IndividualDecision = record.Result
                        };
                        if (!record.Result.Equals("合格"))
                            report.InspectionConclusion = "不合格";
                        report.reportList.Add(reportDetail);
                        dicReprot.Add(key, report);
                        strKey.Add(key);
                    }
                    else
                    {
                        clsReportGS.ReportDetail reportDetail = new clsReportGS.ReportDetail()
                        {
                            ProjectName = record.CheckTotalItem,
                            Unit = record.ResultInfo,
                            InspectionStandard = record.StandValue,
                            IndividualResults = record.CheckValueInfo,
                            IndividualDecision = record.Result
                        };
                        if (!record.Result.Equals("合格"))
                            report.InspectionConclusion = "不合格";
                        report.reportList.Add(reportDetail);
                    }
                }

                if (dicReprot.Count > 0)
                {
                    int InsertNum = 0;
                    for (int i = 0; i < dicReprot.Count; i++)
                    {
                        report = new clsReportGS();
                        report = dicReprot[strKey[i]];
                        DataTable dt = _resultTable.Insert(report, out error);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            InsertNum += 1;
                            //获取最新一条数据的ID作为子表的关联ID
                            List<clsReportGS> Items = (List<clsReportGS>)IListDataSet.DataTableToIList<clsReportGS>(dt, 1);
                            if (Items != null && Items.Count > 0)
                            {
                                for (int j = 0; j < report.reportList.Count; j++)
                                {
                                    clsReportGSDetail reportDetail = new clsReportGSDetail()
                                    {
                                        ReportGSID = Items[0].ID,
                                        ProjectName = report.reportList[j].ProjectName,
                                        Unit = report.reportList[j].Unit,
                                        InspectionStandard = report.reportList[j].InspectionStandard,
                                        IndividualResults = report.reportList[j].IndividualResults,
                                        IndividualDecision = report.reportList[j].IndividualDecision
                                    };
                                    _resultTable.Insert(reportDetail, out error);
                                }
                            }
                        }
                    }
                    if (!error.Equals(string.Empty))
                    {
                        MessageBox.Show(this, "生成报表时发生异常!\n" + error.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        //修改生成报表状态
                        if (_idList != null && _idList.Count > 0)
                            UpdateReport();
                        if (MessageBox.Show("成功生成 " + InsertNum + " 张报表!是否立即查看报表？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ReportWindow window = new ReportWindow()
                            {
                                ShowInTaskbar = false,
                                Owner = this
                            };
                            window.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 原始报表
        /// 相同的key合并到同一张报表
        /// key=检测单位+被检单位+抽样日期
        /// </summary>
        private void GenerateReports()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "未选择任何条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (_idList == null)
                _idList = new List<int>();

            clsReport report = new clsReport();
            Dictionary<string, clsReport> dicReprot = new Dictionary<string, clsReport>();
            List<string> strKey = new List<string>();
            try
            {
                foreach (tlsTtResultSecond record in _selectedRecords)
                {
                    if (record.IsReport.Equals(string.Empty))
                        _idList.Add(record.ID);
                    string key = record.CheckUnitName + "_" + record.CheckedCompany + "_" + record.TakeDate;
                    if (!dicReprot.ContainsKey(key))
                    {
                        report = new clsReport()
                        {
                            CheckUnitName = record.CheckUnitName,
                            Trademark = string.Empty,
                            Specifications = string.Empty,
                            ProductionDate = record.DateManufacture,
                            QualityGrade = string.Empty,
                            CheckedCompanyName = record.CheckedCompany
                        };
                        DataTable dtCompany = _clsCompanyOprbll.GetAsDataTable("FULLNAME Like '" + record.CheckedCompany + "'");
                        if (dtCompany != null && dtCompany.Rows.Count > 0)
                        {
                            List<clsCompany> Item = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dtCompany, 1);
                            report.CheckedCompanyPhone = Item[0].LinkInfo;
                        }
                        else
                            report.CheckedCompanyPhone = string.Empty;
                        report.ProductionUnitsName = string.Empty;
                        report.ProductionUnitsPhone = string.Empty;

                        DataTable dtTask = _Tskbll.GetAsDataTable("CPTITLE Like '" + record.CheckPlanCode + "'", string.Empty, 1);
                        if (dtTask != null && dtTask.Rows.Count > 0)
                        {
                            List<clsTask> ItemTask = (List<clsTask>)IListDataSet.DataTableToIList<clsTask>(dtTask, 1);
                            report.TaskSource = ItemTask[0].CPFROM;
                        }
                        else
                            report.TaskSource = string.Empty;

                        report.Standard = record.Standard;
                        report.SamplingData = record.TakeDate;
                        report.SampleNum = string.Empty;
                        report.SamplingCode = string.Empty;
                        report.SampleArrivalData = string.Empty;
                        report.Notes = string.Empty;
                        report.IsDeleted = "N";
                        report.CreateData = DateTime.Now.ToString();

                        clsReport.ReportDetail reportDetail = new clsReport.ReportDetail()
                        {
                            FoodName = record.FoodName,
                            ProjectName = record.CheckTotalItem,
                            Unit = record.ResultInfo,
                            CheckData = record.CheckValueInfo,
                            IsDeleted = "N"
                        };
                        report.reportList.Add(reportDetail);
                        dicReprot.Add(key, report);
                        strKey.Add(key);
                    }
                    else
                    {
                        clsReport.ReportDetail reportDetail = new clsReport.ReportDetail()
                        {
                            FoodName = record.FoodName,
                            ProjectName = record.CheckTotalItem,
                            Unit = record.ResultInfo,
                            CheckData = record.CheckValueInfo,
                            IsDeleted = "N"
                        };
                        report.reportList.Add(reportDetail);
                    }
                }

                if (dicReprot.Count > 0)
                {
                    int InsertNum = 0;
                    string error = string.Empty;
                    for (int i = 0; i < dicReprot.Count; i++)
                    {
                        report = new clsReport();
                        report = dicReprot[strKey[i]];
                        DataTable dt = _resultTable.Insert(report, out error);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            InsertNum += 1;
                            //获取最新一条数据的ID作为子表的关联ID
                            List<clsReport> ItemNames = (List<clsReport>)IListDataSet.DataTableToIList<clsReport>(dt, 1);
                            if (ItemNames != null && ItemNames.Count > 0)
                            {
                                for (int j = 0; j < report.reportList.Count; j++)
                                {
                                    clsReportDetail reportDetail = new clsReportDetail()
                                    {
                                        ReportID = ItemNames[0].ID,
                                        FoodName = report.reportList[j].FoodName,
                                        ProjectName = report.reportList[j].ProjectName,
                                        Unit = report.reportList[j].Unit,
                                        CheckData = report.reportList[j].CheckData,
                                        IsDeleted = report.reportList[j].IsDeleted
                                    };
                                    int rtn = _resultTable.Insert(reportDetail, out error);
                                }
                            }
                        }
                    }
                    if (!error.Equals(string.Empty))
                    {
                        MessageBox.Show("生成报表时发生异常!\n" + error.ToString());
                    }
                    else
                    {
                        //修改生成报表状态
                        if (_idList != null && _idList.Count > 0)
                            UpdateReport();
                        if (MessageBox.Show("成功生成 " + InsertNum + " 张报表!是否立即查看报表？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ReportWindow window = new ReportWindow()
                            {
                                ShowInTaskbar = false,
                                Owner = this
                            };
                            window.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 检测报告
        /// 2017年2月27日 修改 查看报表 为 检测报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReportShow_Click(object sender, RoutedEventArgs e)
        {
            PrintCheckedReport();
        }

        /// <summary>
        /// 全部显示 Or 分页显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxSelAll_Click(object sender, RoutedEventArgs e)
        {
            _IsShowAll = (bool)CheckBoxSelAll.IsChecked;
            if (_IsShowAll)
            {
                CheckBoxSelAll.Content = "显示全部";
                this.DataGridRecord.Height = 450;
                this.DataGridRecord.Columns[13].Visibility = Visibility.Visible;
                this.wrapPanel1.Visibility = Visibility.Collapsed;
                this.CheckBoxSelAll.ToolTip = "去勾选:分页显示,颜色标识上传状态<绿色:已上传,黄色:上传后有修改>";
            }
            else
            {
                CheckBoxSelAll.Content = "分页显示";
                this.DataGridRecord.Height = 425;
                this.DataGridRecord.Columns[13].Visibility = Visibility.Collapsed;
                this.wrapPanel1.Visibility = Visibility.Visible;
                this.CheckBoxSelAll.ToolTip = "勾选:不分页显示，且取消上传颜色标识";
            }
            _IsCalcPage = true;
            SearchRecord();
        }

        private void BtnDataAnalysis_Click(object sender, RoutedEventArgs e)
        {
            DataAnalysis window = new DataAnalysis()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.InitializeData(_statisticalDT);
            window.ShowDialog();
        }

        private void BtnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有检测记录吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _resultTable.Delete(string.Empty, out sErr);
                str = sErr.Equals(string.Empty) ? "已成功清理所有检测记录!" : ("清理检测记录时出现错误!\n异常：" + sErr);
                SearchRecord();
                MessageBox.Show(str, "操作提示");
            }
        }

        private void Btn_ReportPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
            {
                GenerateReports();
            }
            else if (Global.EachDistrict.Equals("GS"))
            {
                GenerateReportsGS();
            }
        }

        private void ComboBoxItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            _IsCalcPage = true;
            if (!IsReset) SearchRecord();
        }

    }
}