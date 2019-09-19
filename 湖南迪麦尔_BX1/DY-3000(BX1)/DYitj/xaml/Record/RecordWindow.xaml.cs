using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.src;
using AIO.xaml.Dialog;
using AIO.xaml.Print;
using AIO.xaml.Record;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using Microsoft.Win32;
using AIO.src.xprinter;

namespace AIO
{
    /// <summary>
    /// RecordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RecordWindow : Window
    {

        #region 全局变量

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
        private List<Record> _records = null;
        private List<Record> _filteredRecords = null;
        private List<tlsTtResultSecond> _selectedRecords = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private QueryThread _queryThread = null;
        private List<int> _idList = null;
        private clsCompanyOpr _clsCompanyOprbll = new clsCompanyOpr();
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private static object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        private bool _IsShowAll = false;
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
        private int _pageSize = 20;
        /// <summary>
        /// 首次进入界面时不执行下拉框中的查询方法
        /// </summary>
        private bool _IsFirst = false;
        private object _optionalValue = Missing.Value;
        /// <summary>
        /// 数据分析统计集合
        /// </summary>
        private DataTable _statisticalDT = null;
        private static string logType = "result-error";
        /// <summary>
        /// 用于检测结果界面链接过来直接查看当前检测项目的数据
        /// </summary>
        public string _CheckItemName = string.Empty;
        #endregion

        public RecordWindow()
        {
            InitializeComponent();
            this.DatePickerDateFrom.DisplayDateEnd = DateTime.Now;
            this.DatePickerDateTo.DisplayDateStart = DateTime.Now;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxItem.Text = _CheckItemName;
            ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            //btnDataAnalysis.Margin = btn_ReportPrint.Margin;
            //btn_ReportPrint.Margin = btnImport.Margin;
            //btnImport.Margin = btnExport.Margin;
            //btnExport.Margin = btnEdit.Margin;

            btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            SetComboBox();
            SearchRecord();
            _queryThread = new QueryThread(this);
            _queryThread.Start();
        }

        private void SetComboBox()
        {
            List<string> comboList = new List<string>();
            comboList.Add("全部"); comboList.Add("分光光度"); comboList.Add("胶体金"); comboList.Add("干化学"); comboList.Add("重金属");
            ComboBoxCategory.ItemsSource = comboList;
            setItems("all");
        }

        /// <summary>
        /// fgd|jtj|ghx|zjs
        /// </summary>
        /// <param name="type"></param>
        private void setItems(String type)
        {
            IList<string> comboList = new List<string>();
            comboList.Add("全部");

            //分光光度
            if (Global.set_IsOpenFgd && (type.Equals("all") || type.Equals("fgd")))
            {
                comboList.Add("------分光光度------");
                comboList.Add("抑制率法"); comboList.Add("标准曲线法"); comboList.Add("动力学法"); comboList.Add("系数法");
            }
            //胶体金
            if (Global.set_IsOpenSxt && (type.Equals("all") || type.Equals("jtj")))
            {
                comboList.Add("------胶体金------");
                comboList.Add("定性消线法"); comboList.Add("定性比色法"); comboList.Add("定量法(T)"); comboList.Add("定量法(T/C)");
            }
            //干化学
            if (Global.set_IsOpenSxt && (type.Equals("all") || type.Equals("ghx")))
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

        private DataTable _dts = null;
        /// <summary>
        /// 查询条件改变时需要重新计算分页
        /// </summary>
        private bool _IsCalcPage = true;
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
                        List<tlsTtResultSecond> items = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(dt, 1);
                        DataGridRecord.DataContext = items;
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
            labelCount.Content = "当前为第" + _pageIndex + "页,共" + _pageSum + "页";
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
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T childContent = default(T);
            try
            {
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
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
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
            try
            {
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    tlsTtResultSecond record = (tlsTtResultSecond)DataGridRecord.SelectedItems[i];
                    _selectedRecords.Add(record);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
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
            try
            {
                if (null == _selectedRecords || _selectedRecords.Count == 0)
                {
                    MessageBox.Show("请选择打印条目！", "操作提示");
                    return;
                }
                LabelInfo.Content = "正在打印...";
                //热干胶打印机 直接控制驱动打印
                if (Global.Printing[0].XPrinter)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (tlsTtResultSecond record in _selectedRecords)
                    {
                        sb.Clear ();
                        if (Global.IsGoverTest==false )//企业版
                        {
                            sb.AppendFormat("      {0}\n", record.CheckTotalItem);//检测项目
                            sb.AppendFormat("检测设备：{0}\n", Global.InstrumentName);
                            sb.AppendFormat("检测类型：{0}\n", record.ResultType);
                            sb.AppendFormat("检测依据：{0}\r\n", record.Standard);
                            sb.AppendFormat("检测日期：{0}\r\n", record.CheckStartDate);

                            sb.AppendFormat("被检单位：{0}\r\n", record.CheckedCompany);

                            if (record.ResultType == "分光光度")//分光光度 数值加单位
                            {
                                sb.AppendFormat("编号  样品   结果   判定\r\n", "");
                                sb.AppendFormat("{0} {1} {2} {3}\r\n", record.SampleCode, record.FoodName, record.CheckValueInfo + record.ResultInfo, record.Result);
                            }
                            else
                            {
                                sb.AppendFormat("编号  样品  判定\r\n", "");
                                sb.AppendFormat("{0} {1} {2}\r\n", record.SampleCode, record.FoodName, record.Result);
                            }
                            sb.AppendFormat("检测单位：{0}\r\n", record.CheckUnitName);
                            sb.AppendFormat("检测人员：{0}", record.CheckUnitInfo);

                            PrintModel pmContent2 = new PrintModel();
                            pmContent2.FontFamily = "宋体";
                            pmContent2.FontSize = 9;
                            pmContent2.IsBold = false;
                            pmContent2.Text = new System.IO.StringReader(sb.ToString());

                            //PrintModel[] pms = new PrintModel[] { pmTitle, pmContent1, pmContent2 };
                            PrintModel[] pms = new PrintModel[] { pmContent2 };

                            if (TicketPrinterHelper.Print(pms, sb.ToString()))
                            {
                                LabelInfo.Content = "提示：打印成功！";
                            }
                            else
                            {
                                LabelInfo.Content = "提示：打印失败！请检查驱动！";
                            }
                        }
                        else//政府版 合格证
                        {
                            sb.AppendFormat("名称：{0}\r\n", record.FoodName);

                            string[] timestr = record.CheckStartDate.Split(' ');

                            if (timestr != null && timestr.Length == 2)
                            {
                                sb.AppendFormat("日期：{0}\r\n", timestr[0]);
                            }
                            else
                            {
                                sb.AppendFormat("日期：{0}\r\n", record.CheckStartDate);
                            }

                            sb.AppendFormat("重量：{0} 电话：{1}\r\n", record.Weights, record.PhoneNumber );
                            sb.AppendFormat("经营者：{0}\r\n", record.Operators );
                            sb.AppendFormat("产地：{0}\r\n", record.ProductAddr );
                            sb.Append("- - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                            sb.Append("方式：\r\n");
                            sb.Append("□ 自检合格\r\n");
                            sb.Append("□ 委托检测合格\r\n");
                            sb.Append("□ 自我承诺合格\r\n");
                            sb.Append("□ 内部质量控制合格\r\n");

                            PrintModel pmTitle = new PrintModel();
                            pmTitle.FontFamily = "宋体";
                            pmTitle.FontSize = Convert.ToInt32("14");
                            pmTitle.IsBold = true ;
                            pmTitle.Text = new System.IO.StringReader("\n" + "食用农产品合格证");//首行需要空行,某些打印机首行不为空时会出现“首行乱码问题”

                            PrintModel pmContent2 = new PrintModel();
                            pmContent2.FontFamily = "宋体";
                            pmContent2.FontSize = Convert.ToInt32("9");
                            pmContent2.IsBold = false;
                            pmContent2.Text = new System.IO.StringReader(sb.ToString());

                            PrintModel[] pms = new PrintModel[] { pmTitle, pmContent2 };
                            //二维码
                            string QrCode = "检测结果：" + record.Result + "\r\n检测人：" + record.CheckUnitInfo + "\r\n检测时间：" +record.CheckStartDate;
                            if (TicketPrinterHelper.Print(pms, QrCode))
                            {
                                LabelInfo.Content = "提示：打印成功！";
                            }
                            else
                            {
                                LabelInfo.Content = "提示：打印失败！请检查驱动！";
                            }
                        }
                    }
                   
                }
                else
                {
                    //热敏串口打印机

                    //二维码打印
                    if (PrintHelper.PrintQR && Global.PrintQrCode)
                    {
                        List<byte[]> buffers = new List<byte[]>();
                        List<byte> data = null;
                        byte[] buffer = null;
                        foreach (tlsTtResultSecond record in _selectedRecords)
                        {
                            PrintHelper.Report model = new PrintHelper.Report
                            {
                                ItemName = record.CheckTotalItem,
                                Standard = record.Standard,
                                ItemCategory = record.ResultType,
                                User = LoginWindow._userAccount.UserName,
                                Unit = record.ResultInfo,
                                Judgment = record.FoodName,
                                Date = record.CheckStartDate,
                                Company = record.CheckedCompany,
                                //分光部分项目需要打印对照值
                                ContrastValue = record.ContrastValue
                            };
                            model.SampleName.Add(record.FoodName);
                            model.SampleNum.Add(string.Format("{0:D5}", record.SampleCode));
                            model.JudgmentTemp.Add(record.Result);
                            model.Result.Add(record.CheckValueInfo);

                            //打印条形码的数据
                            //model.samplecode = record.SampleCode;
                            //data = new List<byte>();
                            //data.AddRange(model.GetBarcode());
                            //buffer = new byte[data.Count];
                            //data.CopyTo(buffer);
                            //buffers.Add(buffer);

                            //二维码数据
                            data = new List<byte>();
                            data.AddRange(model.GetQrCode());
                            buffer = new byte[data.Count];
                            data.CopyTo(buffer);
                            buffers.Add(buffer);

                            string bfrstr = string.Empty;
                            for (int i = 0; i < buffer.Length; i++)
                            {
                                bfrstr += buffer[i].ToString("X2") + ",";
                            }
                            Console.WriteLine(bfrstr);

                            //检测数据
                            data = new List<byte>();
                            data.AddRange(Global.Is3500I ? model.GeneratePrintBytesEvery() : model.GeneratePrintBytes());
                            buffer = new byte[data.Count];
                            data.CopyTo(buffer);
                            buffers.Add(buffer);

                            bfrstr = string.Empty;
                            for (int i = 0; i < buffer.Length; i++)
                            {
                                bfrstr += buffer[i].ToString("X2") + ",";
                            }
                            Console.WriteLine(bfrstr);
                        }
                        Message msg = new Message
                        {
                            what = MsgCode.MSG_PRINT,
                            str1 = Global.strPRINTPORT,
                            data = null,
                            datas = buffers
                        };
                        Global.printThread.SendMessage(msg, _queryThread);
                    }
                    else
                    {
                        // 将时间相同，相同被检单位，相同检测手段的项合并打印
                        List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
                        List<byte> data = new List<byte>();
                        IDictionary<string, List<tlsTtResultSecond>> dic = new Dictionary<string, List<tlsTtResultSecond>>();
                        string key = string.Empty;
                        foreach (tlsTtResultSecond record in _selectedRecords)
                        {
                            key = record.CheckStartDate + "-" + record.CheckedCompany + "-" + record.ResultType + "-" + record.CheckTotalItem;
                            if (!dic.ContainsKey(key))
                            {
                                List<tlsTtResultSecond> rs = new List<tlsTtResultSecond>();
                                rs.Add(record);
                                dic.Add(key, rs);
                            }
                            else
                            {
                                dic[key].Add(record);
                            }
                        }
                        foreach (var item in dic)
                        {
                            List<tlsTtResultSecond> models = item.Value;
                            PrintHelper.Report model = new PrintHelper.Report
                            {
                                ItemName = models[0].CheckTotalItem,
                                Standard = models[0].Standard,
                                ItemCategory = models[0].ResultType,
                                User = LoginWindow._userAccount.UserName,
                                Unit = models[0].ResultInfo,
                                Judgment = models[0].FoodName,
                                Date = models[0].CheckStartDate,
                                Company = models[0].CheckedCompany,
                                //分光部分项目需要打印对照值
                                ContrastValue = models[0].ContrastValue
                            };
                            for (int i = 0; i < models.Count; i++)
                            {
                                model.SampleName.Add(models[i].FoodName);
                                model.SampleNum.Add(string.Format("{0:D5}", models[i].SampleCode));
                                model.JudgmentTemp.Add(models[i].Result);
                                model.Result.Add(models[i].CheckValueInfo);
                            }
                            reports.Add(model);
                        }

                        foreach (PrintHelper.Report report in reports)
                            data.AddRange(Global.Is3500I ? report.GeneratePrintBytes_3500I() : report.GeneratePrintBytes());
                        byte[] buffer = new byte[data.Count];
                        data.CopyTo(buffer);
                        Message msg = new Message
                        {
                            what = MsgCode.MSG_PRINT,
                            str1 = Global.strPRINTPORT,
                            data = buffer,
                            arg1 = 0,
                            arg2 = buffer.Length
                        };
                        Global.printThread.SendMessage(msg, _queryThread);
                    }
                }

            }
            catch (Exception ex)
            {
                LabelInfo.Content = "提示：打印失败！";
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("打印失败!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonQuery_Click(object sender, RoutedEventArgs e)
        {
            SearchRecord();
        }

        public void SearchRecord()
        {
            try
            {
                StringBuilder where = new StringBuilder();
                _bFilterUser = ComboBoxUser.Text.Trim().Equals("") ? false : true;
                _bFilterCategory = ComboBoxCategory.Text.Trim().Equals("全部") ? false : true;
                _bFilterItem = ComboBoxItem.Text.Trim().Equals("") ? false : true;
                _bFilterMethod = ComboBoxMethod.Text.Trim().Equals("全部") ? false : true;
                if (DatePickerDateTo.SelectedDate != null || DatePickerDateFrom.SelectedDate != null)
                    _bFilterDate = true;
                if (_bFilterUser)
                    where.AppendFormat(" CheckUnitInfo='{0}' and", ComboBoxUser.Text.Trim());
                if (_bFilterCategory)
                    where.AppendFormat(" ResultType='{0}' and", ComboBoxCategory.Text.Trim());
                if (_bFilterItem)
                    where.AppendFormat(" CheckTotalItem Like '%{0}%' and", ComboBoxItem.Text.Trim());
                if (TxtSampleName.Text.Trim().Length > 0)
                    where.AppendFormat(" FoodName Like '%{0}%' and", TxtSampleName.Text.Trim());
                if (_bFilterMethod)
                    where.AppendFormat(" CheckMethod='{0}' and", ComboBoxMethod.Text.Trim());
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
                            where.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", _strDateFrom, _strDateTo);
                        }
                        else if (DatePickerDateFrom.SelectedDate != null && DatePickerDateTo.SelectedDate == null)
                        {
                            _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                            where.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", _strDateFrom, "9999-01-01 00:00:00");
                        }
                        else if (DatePickerDateFrom.SelectedDate == null && DatePickerDateTo.SelectedDate != null)
                        {
                            _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                            where.AppendFormat(" CheckStartDate between '{0}' and  '{1}' and", "1000-01-01 00:00:00", _strDateTo);
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(6, logType, ex.ToString());
                        MessageBox.Show("查询出现异常！\n错误信息：" + ex.Message);
                    }
                }
                if (where.Length > 0)
                    ShowResult(where.ToString(0, where.Length - 3), " ID Desc", 1);
                else
                    ShowResult("", "  ID Desc", 1);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
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
                MessageBox.Show(this, "请选择上传条目!", "操作提示");
                return;
            }
            string info = string.Empty;
            if (!Global.UploadCheck(this, out info))
            {
                LabelInfo.Content = info;
                return;
            }

            try
            {
                List<CheckPointInfo> service = Global.samplenameadapter;
                LabelInfo.Content = "正在上传...";
                DataTable dt = ListToDataTable();
                dt = Global.CheckDtblValue(dt);
                if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Length == 0)
                {
                    if (dt != null || dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["CKCKNAMEUSID"] = service[0].UploadUserUUID;
                        }
                    }
                }

                Message msg = new Message
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
                FileUtils.OprLog(0, "upload-error", ex.ToString());
                MessageBox.Show(this, "上传数据时出现异常!\r\n异常信息：" + ex.Message, "Error");
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
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            // 这个函数是通过代理调用的。根据消息类别，将数据更新到UI。这里处理的是不费时的操作。
            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_RECORD_INIT:
                        wnd.DataGridRecord.DataContext = wnd._records;
                        wnd.ButtonQuery_Click(null, null); // 初始化之后，调用一次查询。
                        break;

                    case MsgCode.MSG_RECORD_QUERY:
                        break;

                    case MsgCode.MSG_PRINT:
                        if (msg.result)
                        {
                            wnd.LabelInfo.Content = "提示：打印成功！";
                        }
                        else
                        {
                            wnd.LabelInfo.Content = "提示：打印失败！";
                            MessageBox.Show("打印失败，请检查打印端口是否正确。");
                        }
                        break;
                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                        {
                            if (Global.UploadSCount > 0)
                            {
                                wnd.LabelInfo.Content = string.Format("提示：成功上传 {0} 条数据!", Global.UploadSCount);
                            }
                            else
                            {
                                wnd.LabelInfo.Content = "上传未成功";
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
                                MessageBox.Show(wnd, "数据上传失败!\r\n\r\n异常信息：" + msg.outError, "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("请选择编辑条目！", "操作提示");
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        #region ExportData To Excel

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            Mouse.SetCursor(Cursors.Wait);
            ExportToExcel();
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
                //设置导出路径
                //InitialDirectory = Environment.CurrentDirectory,
                CheckPathExists = true,
                DefaultExt = "xls",
                FileName = string.Format("{0} 检测数据 - {1}", Global.InstrumentNameModel, DateTime.Now.ToString("yyyy-MM-dd")),
                Filter = "Excel文件(*.xls)|*.xls|All files (*.*)|*.*"
            };
            if ((bool)(saveFileDialog.ShowDialog()))
            {
                try
                {
                    DataTable dataTable = null;
                    //查询全部
                    if (null == _selectedRecords || _selectedRecords.Count == 0)
                        dataTable = _resultTable.ExportData(WhereSql(false), Global.InterfaceType);
                    else
                        dataTable = _resultTable.ExportData(WhereSql(true), Global.InterfaceType);

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
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                }
            }
        }

        /// <summary>
        /// 导出查询条件
        /// </summary>
        /// <param name="isSearchAll">false 查询全部|true 根据ID查询</param>
        /// <returns>结果</returns>
        private string WhereSql(bool isSearchAll)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                //stringBuilder.AppendFormat(" CheckUnitInfo='{0}' ", ComboBoxUser.Text.Trim());
                if (_selectedRecords != null && _selectedRecords.Count != 0 && isSearchAll)
                {
                    string strId = "";
                    foreach (tlsTtResultSecond record in _selectedRecords)
                        strId += record.ID + ",";
                    strId += "0)";
                    stringBuilder.AppendFormat("ID IN({0}", strId);
                }
                else
                {
                    List<tlsTtResultSecond> models = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(_dts, 1);
                    string strId = string.Empty;
                    foreach (tlsTtResultSecond record in models)
                        strId += record.ID + ",";
                    strId += "0)";
                    stringBuilder.AppendFormat("ID IN({0}", strId);
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            return stringBuilder.ToString();
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

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ImportToExcel();//从Excel导入数据
        }

        /// <summary>
        /// 从Excel导入数据
        /// </summary>
        private void ImportToExcel()
        {
            OpenFileDialog op = new OpenFileDialog();
            bool isImport = false;
            string error = string.Empty, repeatSample = string.Empty;
            int importNum = 0;
            DataTable dt = null;
            try
            {
                op.Filter = "Excel (*.xls)|*.xls";
                if ((bool)(op.ShowDialog()))
                {
                    this.LabelInfo.Content = "提示：正在导入数据！";
                    dt = ExcelHelper.ImportExcel(op.FileName.Trim(), 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        isImport = true;
                        int num = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string val = string.Empty;
                            tlsTtResultSecond model = new tlsTtResultSecond
                            {
                                ResultType = dt.Rows[i][0].ToString(),//检测手段
                                CheckNo = ChangeDataToD(dt.Rows[i][1].ToString()),//检测编号
                                SampleCode = dt.Rows[i][2].ToString(),//样品编号
                                CheckPlaceCode = dt.Rows[i][3].ToString(),//行政机构编号
                                                                          //val = dt.Rows[i][3].ToString();
                                                                          //if (val.Length > 0)
                                                                          //{
                                                                          //    if (val.Length < 9)
                                                                          //        model.CheckPlaceCode = string.Format("{0:000000000}", int.Parse(val, 0));//行政机构编号
                                                                          //    else if (val.Length > 9 && val.Length < 12)
                                                                          //        model.CheckPlaceCode = string.Format("{0:000000000000}", int.Parse(val, 0));
                                                                          //}
                                                                          //else
                                                                          //model.CheckPlaceCode = val;
                                CheckPlace = dt.Rows[i][4].ToString(),//行政机构名称
                                FoodName = dt.Rows[i][5].ToString(),//样品名称
                                FoodType = dt.Rows[i][6].ToString(),//样品种类
                                TakeDate = dt.Rows[i][7].ToString(),//抽检日期
                                CheckStartDate = dt.Rows[i][8].ToString(),//检测时间
                                Standard = dt.Rows[i][9].ToString(),//检测依据
                                CheckMachine = Global.InstrumentNameModel + Global.InstrumentName, //dt.Rows[i][10].ToString().Length == 0 ? "DY-3000(BX1) 便携式食品综合分析仪" :
                                                                                                   //dt.Rows[i][10].ToString();//检测设备
                                CheckMachineModel = Global.InstrumentNameModel,//dt.Rows[i][11].ToString().Length == 0 ? "DY-3000(BX1)" :
                                                                               //dt.Rows[i][11].ToString();//检测设备型号
                                MachineCompany = "广东达元绿洲食品安全科技股份有限公司",//dt.Rows[i][12].ToString().Length == 0 ? "广东达元绿洲食品安全科技股份有限公司" :
                                                                      //dt.Rows[i][12].ToString();//检测设备厂商
                                CheckTotalItem = dt.Rows[i][13].ToString()//检测项目
                            };
                            double douCheckValue = 0;
                            bool isInt = double.TryParse(dt.Rows[i][14].ToString(), out douCheckValue);
                            model.CheckValueInfo = isInt ? douCheckValue.ToString("0.000") : dt.Rows[i][14].ToString();//检测值
                            model.StandValue = dt.Rows[i][15].ToString();//检测标准值
                            model.Result = dt.Rows[i][16].ToString();//检测结论
                            model.ResultInfo = dt.Rows[i][17].ToString();//检测值单位
                            model.CheckUnitName = dt.Rows[i][18].ToString();//检测单位
                            model.CheckUnitInfo = dt.Rows[i][19].ToString();//检测人姓名
                            model.Organizer = dt.Rows[i][20].ToString();//抽样人
                            model.UpLoader = dt.Rows[i][21].ToString();//基层上传人
                            model.ReportDeliTime = dt.Rows[i][22].ToString();//检测报告送达时间
                            model.IsReconsider = dt.Rows[i][23].ToString().Equals("True") ? true : false;//是否提出复议
                            model.ReconsiderTime = dt.Rows[i][24].ToString();//提出复议时间
                            model.ProceResults = dt.Rows[i][25].ToString();//处理结果
                            model.CheckedCompanyCode = dt.Rows[i][26].ToString();//被检对象编号
                            model.CheckedCompany = dt.Rows[i][27].ToString();//被检对象名称
                            model.CheckedComDis = dt.Rows[i][28].ToString();//档口门牌号
                            model.CheckPlanCode = dt.Rows[i][29].ToString();//任务编号
                            model.DateManufacture = dt.Rows[i][30].ToString();//生产日期
                            model.CheckMethod = dt.Rows[i][31].ToString();//检测方法
                            model.APRACategory = dt.Rows[i][32].ToString();//单位类别
                            model.Hole = string.Format("{0:D5}", dt.Rows[i][33].ToString());//检测孔
                            model.SamplingPlace = dt.Rows[i][34].ToString();//抽样地点
                            model.CheckType = dt.Rows[i][35].ToString();//检测类型
                            model.ContrastValue = dt.Rows[i][36].ToString();//对照值
                            model.IsReport = model.IsShow = model.IsUpload = "N";
                            model.CheckMachine = Global.InstrumentNameModel + Global.InstrumentName;//检测设备
                            model.CheckMachineModel = Global.InstrumentNameModel; //检测设备型号
                            model.MachineCompany = ((AssemblyCompanyAttribute)attributes[0]).Company; //检测设备生产厂家
                            try
                            {
                                //此次try模块为兼容旧版本软件数据
                                model.CKCKNAMEUSID = dt.Rows[i][37].ToString();
                                model.SysCode = dt.Rows[i][38].ToString();
                                if (Global.InterfaceType.Equals("GS"))
                                {
                                    model.ProduceCompany = dt.Rows[i][39].ToString();
                                }
                                else if (Global.InterfaceType.Equals("AH"))
                                {
                                    model.fTpye = dt.Rows[i][39].ToString();
                                    model.testPro = dt.Rows[i][40].ToString();
                                    model.quanOrQual = dt.Rows[i][41].ToString();
                                    model.dataNum = dt.Rows[i][42].ToString();
                                    model.checkedUnit = dt.Rows[i][43].ToString();
                                }
                                else if (Global.InterfaceType.Equals("ZH"))
                                {
                                    model.DeviceId = dt.Rows[i][39].ToString();
                                    model.SampleId = dt.Rows[i][40].ToString();
                                }
                            }
                            catch (Exception)
                            {

                            }

                            //判定当前样品是否已经存在，如果已经存在则不进行导入
                            val = string.Format("FoodName='{0}' And CheckStartDate='{1}' And CheckNo='{2}'",
                                model.FoodName, model.CheckStartDate, model.CheckNo);
                            DataTable dtbl = SqlHelper.GetDataTable("ttResultSecond", val, out error);
                            if (dtbl != null && dtbl.Rows.Count > 0)
                            {

                                num++;
                                repeatSample += "【" + model.FoodName + "】";
                                if (num == 4)
                                {
                                    num = 0;
                                    repeatSample += "\r\n";
                                }
                                continue;
                            }
                            if (Global.InterfaceType.Equals("AH"))
                            {
                                _resultTable.InsertAh(model, out error);
                            }
                            else
                            {
                                _resultTable.Insert(model, out error);
                            }
                            importNum = error.Equals("") ? importNum + 1 : importNum - 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isImport = false;
                this.LabelInfo.Content = "提示：导入失败！";
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            finally
            {
                if (isImport)
                {
                    this.LabelInfo.Content = "提示：导入成功!";
                    if (importNum > 0)
                    {
                        if (repeatSample.Length == 0)
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

        #endregion

        private Int32 cbIndex = 0;
        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxMethod != null)
            {
                if (ComboBoxMethod.SelectedValue == null)
                    return;

                if (!ComboBoxMethod.SelectedValue.ToString().Equals("") && _IsFirst)
                {
                    cbIndex = ComboBoxMethod.SelectedIndex;
                    ComboBoxMethod.Text = ComboBoxMethod.SelectedValue.ToString();
                    ComboBoxMethod.SelectedIndex = cbIndex;
                    _pageIndex = 1;
                    _IsCalcPage = true;
                    SearchRecord();
                }
            }
        }

        /// <summary>
        /// 双击 进入当前行的编辑界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
        //private void ComboBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    Global.IsProject = true;
        //    SearchSample searchSample = new SearchSample();
        //    searchSample.ShowDialog();
        //    if (!Global.projectName.Equals(""))
        //    {
        //        this.ComboBoxItem.Text = Global.projectName;
        //        _pageIndex = 1;
        //        _IsFirstLoad = true;
        //        SearchRecord();
        //    }
        //    Global.projectName = "";
        //}

        private void DatePickerDateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDateFrom.SelectedDate != null)
            {
                this.DatePickerDateTo.DisplayDateStart = (DateTime)DatePickerDateFrom.SelectedDate;
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
                MessageBox.Show("请选择需要删除的条目！", "操作提示");
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
                    LabelInfo.Content = "提示：删除成功！";
                    MessageBox.Show("成功删除 " + delNum + " 条数据！", "操作提示");
                }
                else
                {
                    if (delNum > 1)
                        MessageBox.Show("删除过程中出现了小问题，但还是成功删除了 " + delNum + " 条数据！", "操作提示");
                    else
                        MessageBox.Show("删除失败，请联系管理员！", "操作提示");
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
                    ComboBoxItem.Text = "";
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
            if (ComboBoxCategory != null)
            {

                if (!ComboBoxCategory.SelectedValue.ToString().Equals("") && _IsFirst)
                {
                    ComboBoxCategory.Text = ComboBoxCategory.SelectedValue.ToString();
                    _pageIndex = 1;
                    _IsCalcPage = true;
                    String str = string.Empty;
                    if (ComboBoxCategory.SelectedIndex == 1)
                    {
                        str = "fgd";
                    }
                    else if (ComboBoxCategory.SelectedIndex == 2)
                    {
                        str = "jtj";
                    }
                    else if (ComboBoxCategory.SelectedIndex == 3)
                    {
                        str = "ghx";
                    }
                    else if (ComboBoxCategory.SelectedIndex == 4)
                    {
                        str = "zjs";
                    }
                    else
                    {
                        str = "all";
                    }
                    setItems(str);
                    SearchRecord();
                }
            }
        }

        /// <summary>
        /// 根据ID修改报表状态
        /// </summary>
        /// <param name="id">id</param>
        public void UpdateReport()
        {
            string error = "";
            try
            {
                _resultTable.UpdateReport(_idList, out error);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SearchRecord();
                if (!error.Equals(""))
                    MessageBox.Show("更新报表生成状态时出现异常!\n" + error);
                _idList = null;
            }
        }

        /// <summary>
        /// 右键 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        /// <summary>
        /// 右键 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miUpload_Click(object sender, RoutedEventArgs e)
        {
            Upload();
        }

        #region 分页

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpPage_Click(object sender, RoutedEventArgs e)
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
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
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
        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex = 1;
            SearchRecord();
        }

        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEndPage_Click(object sender, RoutedEventArgs e)
        {
            _pageIndex = _pageSum;
            SearchRecord();
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = textBoxPage.Text.Trim();
                if (!str.Equals(""))
                {
                    if (int.Parse(str) < 1 || int.Parse(str) > _pageSum)
                    {
                        textBoxPage.Text = "";
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
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string str = textBoxPage.Text.Trim();
                if (!str.Equals(""))
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
                    if (int.Parse(str) <= _pageSum && int.Parse(str) > 0)
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
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 右键导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExport_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        /// <summary>
        /// 右键导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miImport_Click(object sender, RoutedEventArgs e)
        {
            ImportToExcel();
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintCheckedReport();
        }

        /// <summary>
        /// 曲线查看界面
        /// </summary>
        private CruveDataWindow cruveWindow = new CruveDataWindow();
        /// <summary>
        /// 查看曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miShowCruve_Click(object sender, RoutedEventArgs e)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择需要查看曲线的检测数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            cruveWindow.CloseWindow();
            cruveWindow = new CruveDataWindow();

            if (_selectedRecords != null && _selectedRecords.Count > 0)
            {
                try
                {
                    string errMsg = string.Empty;
                    tlsTtResultSecond md = _selectedRecords[0];
                    DataTable dtbl = SqlHelper.GetDataTable("CurveDatas", string.Format("SysCode='{0}'", md.SysCode), out errMsg);
                    if (dtbl == null || dtbl.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "没有曲线数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }

                    errMsg = dtbl.Rows[0]["CData"].ToString();
                    if (errMsg.Length > 0)
                    {
                        cruveWindow.SettingCruve(md, errMsg);
                        cruveWindow.Show();
                    }
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    cruveWindow.CloseWindow();
                    cruveWindow = new CruveDataWindow();
                    MessageBox.Show("查看曲线出现异常，请联系管理员", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 查看检测标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miShowStandard_Click(object sender, RoutedEventArgs e)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择需要查看检测标准的检测数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (_selectedRecords != null && _selectedRecords.Count > 0)
            {
                try
                {
                    string errMsg = string.Empty;
                    tlsTtResultSecond md = _selectedRecords[0];
                    if (md.Standard.Length == 0)
                    {
                        DataTable dtbl = SqlHelper.GetDataTable("ttStandDecide", string.Format("Name='{0}'", md.CheckTotalItem), out errMsg);
                        if (dtbl == null || dtbl.Rows.Count == 0)
                        {
                            MessageBox.Show(this, "没有检索到对应检测标准，可以尝试去知识库检索!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                        //string val = dtbl.Rows[0]["ItemDes"].ToString();
                        //if (val.Length > 0)
                        //{

                        //}
                    }
                    else
                    {
                        string[] files = Directory.GetFiles(Environment.CurrentDirectory + "\\食品标准");
                        if (files.Length > 0)
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                if (files[i].IndexOf(md.Standard) > 0)
                                {
                                    System.Diagnostics.Process.Start(files[i]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("查看检测标准出现异常！\r\n异常信息" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miReportPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Global.PrintType.Equals(""))
            {
                GenerateReports();
            }
            else if (Global.PrintType.Equals("GS"))
            {
                GenerateReportsGS();
            }
        }

        /// <summary>
        /// 甘肃报表
        /// key=样品名称+被检单位+抽样日期
        /// </summary>
        private void GenerateReportsGS()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show("未选择任何条目！", "操作提示");
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
                    if (record.IsReport.Equals(""))
                        _idList.Add(record.ID);
                    string key = record.FoodName + "_" + record.CheckedCompany + "_" + record.TakeDate;
                    if (!dicReprot.ContainsKey(key))
                    {
                        report = new clsReportGS
                        {
                            Title = "食品快速检验报告",
                            FoodName = record.FoodName,//食品名称
                            FoodType = "",//食品类型
                            ProductionDate = record.DateManufacture,//生产/加工/购进日期
                            CheckedCompanyName = record.CheckedCompany,//被抽样单位名称
                            CheckedCompanyAddress = "",//被抽样单位地址
                            CheckedCompanyPhone = "",//被抽样单位联系电话
                            LabelProducerName = "",//标示生产者名称
                            LabelProducerAddress = "",//标示生产者地址
                            LabelProducerPhone = "",//标示生产者电话
                            SamplingData = record.TakeDate,//抽样日期
                            SamplingPerson = record.Organizer,//抽样人
                            SampleNum = "",//样品数量
                            SamplingBase = "", //抽样基数
                            SamplingAddress = record.SamplingPlace,//抽样地点
                            SamplingOrderCode = record.SampleCode,//抽样单编号
                            Standard = record.Standard,//检验依据
                            InspectionConclusion = "合格",//检验结论
                            Notes = "",//备注
                            Audit = "",//审核
                            Surveyor = record.CheckUnitInfo//检验人
                        };

                        DYSeriesDataSet.DataModel.clsReportGS.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReportGS.ReportDetail
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
                        DYSeriesDataSet.DataModel.clsReportGS.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReportGS.ReportDetail
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
                                    clsReportGSDetail reportDetail = new clsReportGSDetail
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
                    if (!error.Equals(""))
                    {
                        MessageBox.Show("生成报表时发生异常！\n" + error.ToString());
                    }
                    else
                    {
                        //修改生成报表状态
                        if (_idList != null && _idList.Count > 0)
                            UpdateReport();
                        if (MessageBox.Show("成功生成 " + InsertNum + " 张报表！是否立即查看报表？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ReportWindow window = new ReportWindow
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
                FileUtils.OprLog(6, logType, ex.ToString());
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
                MessageBox.Show("未选择任何条目！", "操作提示");
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
                    if (record.IsReport.Equals(""))
                        _idList.Add(record.ID);
                    string key = record.CheckUnitName + "_" + record.CheckedCompany + "_" + record.TakeDate;
                    if (!dicReprot.ContainsKey(key))
                    {
                        report = new clsReport
                        {
                            CheckUnitName = record.CheckUnitName,
                            Trademark = "",
                            Specifications = "",
                            ProductionDate = record.DateManufacture,
                            QualityGrade = "",
                            CheckedCompanyName = record.CheckedCompany
                        };

                        DataTable dtCompany = _clsCompanyOprbll.GetAsDataTable("FULLNAME Like '" + record.CheckedCompany + "'");
                        if (dtCompany != null && dtCompany.Rows.Count > 0)
                        {
                            List<clsCompany> Item = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dtCompany, 1);
                            report.CheckedCompanyPhone = Item[0].LinkInfo;
                        }
                        else
                            report.CheckedCompanyPhone = "";
                        report.ProductionUnitsName = "";
                        report.ProductionUnitsPhone = "";

                        DataTable dtTask = _Tskbll.GetAsDataTable("CPTITLE Like '" + record.CheckPlanCode + "'", "", 1);
                        if (dtTask != null && dtTask.Rows.Count > 0)
                        {
                            List<clsTask> ItemTask = (List<clsTask>)IListDataSet.DataTableToIList<clsTask>(dtTask, 1);
                            report.TaskSource = ItemTask[0].CPFROM;
                        }
                        else
                            report.TaskSource = "";

                        report.Standard = record.Standard;
                        report.SamplingData = record.TakeDate;
                        report.SampleNum = "";
                        report.SamplingCode = "";
                        report.SampleArrivalData = "";
                        report.Notes = "";
                        report.IsDeleted = "N";
                        report.CreateData = DateTime.Now.ToString();

                        clsReport.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReport.ReportDetail
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
                        DYSeriesDataSet.DataModel.clsReport.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReport.ReportDetail
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
                    string error = "";
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
                                    clsReportDetail reportDetail = new clsReportDetail
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
                    if (!error.Equals(""))
                    {
                        MessageBox.Show("生成报表时发生异常！\n" + error.ToString());
                    }
                    else
                    {
                        //修改生成报表状态
                        if (_idList != null && _idList.Count > 0)
                            UpdateReport();
                        if (MessageBox.Show("成功生成 " + InsertNum + " 张报表！是否立即查看报表？!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ReportWindow window = new ReportWindow
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
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 分页显示 Or 显示全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxSelAll_Click(object sender, RoutedEventArgs e)
        {
            _IsShowAll = (bool)CheckBoxSelAll.IsChecked;
            if (_IsShowAll)
            {
                CheckBoxSelAll.Content = "分页显示";
                this.DataGridRecord.Height = 450;
                this.DataGridRecord.Columns[13].Visibility = Visibility.Visible;
                this.wrapPanel1.Visibility = Visibility.Collapsed;
                this.CheckBoxSelAll.ToolTip = "去勾选:显示全部,颜色标识上传状态<绿色:已上传,黄色:上传后有修改>";
            }
            else
            {
                CheckBoxSelAll.Content = "显示全部";
                this.DataGridRecord.Height = 425;
                this.DataGridRecord.Columns[13].Visibility = Visibility.Collapsed;
                this.wrapPanel1.Visibility = Visibility.Visible;
                this.CheckBoxSelAll.ToolTip = "勾选:分页显示，且取消上传颜色标识";
            }
            SearchRecord();
        }

        private void btnDataAnalysis_Click(object sender, RoutedEventArgs e)
        {
            //DataAnalysis window = new DataAnalysis();
            //window.ShowInTaskbar = false; window.Owner = this;
            //window.InitializeData(_statisticalDT);
            //window.ShowDialog();
            StatisticalAnalysisWindow window = new StatisticalAnalysisWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有检测记录吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _resultTable.Delete("", out sErr);
                str = sErr.Equals("") ? "已成功清理所有检测记录！" : ("清理检测记录时出现错误！\n异常：" + sErr);
                SearchRecord();
                MessageBox.Show(str, "操作提示");
            }
        }

        private void btn_ReportPrint_Click(object sender, RoutedEventArgs e)
        {
            miPrint_Click(null, null);
        }

        private void ComboBoxItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchRecord();
        }

        #region 打印检测报告

        /// <summary>
        /// 打印检测报告
        /// </summary>
        private void PrintCheckedReport()
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择打印条目!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (_selectedRecords.Count > 10)
            {
                MessageBox.Show(this, "选择打印条目大于10条，本报告单次只能打印10条!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            string company = "";
            for (int j = 0; j < _selectedRecords.Count; j++)
            {

                if (company == "") company = _selectedRecords[j].CheckedCompany;
                if (company != _selectedRecords[j].CheckedCompany)
                {
                    MessageBox.Show("请选择相同被检单位的记录！");
                    return;
                }
            }


            try
            {
                SaveFile(GetHtmlReport());

                CheckReportForm window = new CheckReportForm()
                {
                    HtmlUrl = path + "\\Others\\export.html"
                };
                window.ShowDialog();
                //SaveFile(GetHtmlDoc());
                ////打印检测报告
                //CheckReportForm window = new CheckReportForm()
                //{
                //    HtmlUrl = path + "\\Others\\CheckedReportModel.html"
                //};
                //window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("打印检测报告时出现异常！\r\n\r\n异常信息：\r\n{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFile(string str)
        {
            if (!(Directory.Exists(path + "\\Others")))
            {
                Directory.CreateDirectory(path + "\\Others");
            }
            //string filePath = path + "\\Others\\CheckedReportModel.html";
            string filePath = path + "\\Others\\export.html";
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

        private string path = Environment.CurrentDirectory;
        private string GetHtmlReport()
        {
            string htmlDoc = string.Empty;
            try
            {
                string Organization = Global.samplenameadapter[0].Organization;
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
                    htmlDoc = System.IO.File.ReadAllText(path + "\\Others\\exreport.txt", System.Text.Encoding.Default);
                    htmlDoc += string.Format("<th width=\"180\" height=\"60\" class=\"as\">检测仪器</th><td width=\"180\" height=\"60\" align=\"center\" class=\"as\">{0}</td>", md.CheckMachine);
                    htmlDoc += string.Format("<th width=\"190\" height=\"76\" class=\"as\">被检单位</th><td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td>", md.CheckedCompany);
                    htmlDoc += "</tr></tbody></table><table width=\"800\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: collapse;\">";
                    htmlDoc += "<tbody><tr><th height=\"54\" width=\"152\" class=\"ds\">样品编号</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">样品名称</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测项目</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测方法</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测依据</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">限量值</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测结果</th>";
                    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">结论</th>";
                    htmlDoc += "<th height=\"54\" width=\"182\" class=\"ds\">检测时间</th></tr>";
                    //一张表最多显示10条就
                    if (_selectedRecords.Count > 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            htmlDoc += "<tr>";
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].SampleCode);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].FoodName);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckTotalItem);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckMethod);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].Standard);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].StandValue);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckValueInfo);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].Result);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckStartDate);
                            htmlDoc += "</tr>";
                        }
                    }
                    else//按实际显示
                    {
                        for (int i = 0; i < _selectedRecords.Count; i++)
                        {
                            htmlDoc += "<tr>";
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].SampleCode);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].FoodName);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckTotalItem);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckMethod);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].Standard);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].StandValue);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckValueInfo);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].Result);
                            htmlDoc += string.Format("<td height=\"54\" width=\"152\" align=\"center\" class=\"ds\">{0}</td>", _selectedRecords[i].CheckStartDate);
                            htmlDoc += "</tr>";
                        }
                        //补齐10条数据
                        //for(int i = 0; i < _selectedRecords.Count-10; i++)
                        //{
                        //    htmlDoc += "<tr>";
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>");
                        //    htmlDoc += "</tr>";
                        //}
                    }

                    htmlDoc += "</tbody></table><div class=\"forming\">";
                    htmlDoc += "<table class=\"cs-pos cs-pos2\" width=\"800\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-54px;font-size:16px;\">";
                    htmlDoc += string.Format("<tbody><tr><td height=\"54\" width=\"380\"></td><td height=\"54\" width=\"380\" style=\"padding-left:40px;\">检测单位：{0}</td></tr></tbody></table>", _selectedRecords[0].CheckUnitName);
                    htmlDoc += "<table class=\"cs-pos\" width=\"800\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"float:left; margin-top:-20px;font-size:16px;\">";
                    htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                    htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">报告时间：{0}</td></tr>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    htmlDoc += "</tbody></table></div><div class=\"forming\" style=\"height:54px; margin-top:0; float:left;\">";
                    htmlDoc += "<div class=\"note\"><span class=\"NT\">备注：检测结论为不合格时，应该及时上报。</span></div></div></div>";
                    htmlDoc += "<table class=\"bottm-line\" width=\"800\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                    htmlDoc += "<tbody><tr>";
                    htmlDoc += string.Format("<td>检测人：{0}</td>", md.CheckUnitInfo);
                    htmlDoc += "<td>审核：</td><td>批准：</td></tr></tbody></table></div></body></html>";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return htmlDoc;
        }
        private string GetHtmlDoc()
        {
            string htmlDoc = string.Empty;
            try
            {
                string Organization = Global.samplenameadapter[0].Organization;
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
                    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td></tr>", md.SampleCode.Length > 0 ? md.SampleCode : "/");

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
                    //htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckValueInfo.Length > 0 ? md.CheckValueInfo + (Result.Equals("阴性") || Result.Equals("阳性") ? string.Empty : (" " + md.ResultInfo)) : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckValueInfo.Length > 0 ? md.CheckValueInfo : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.StandValue.Length > 0 ? md.StandValue + " " + md.ResultInfo : "/");
                    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\" id=\"unique\">{0}</td></tr>", Result.Equals("可疑") ? "可疑" : (Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格"));

                    htmlDoc += "</tbody></table>";

                    htmlDoc += string.Format("<div class=\"forming\"><div class=\"conclusion\">结论：{0}</div>", Result.Equals("可疑") ? "可疑" : (Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格"));
                    if (Global.EnableChapter)
                    {
                        htmlDoc += "<div class=\"img\"><img src=\"CheckedReportOfficialSeal.gif\"></div>";
                    }
                    else
                    {
                        htmlDoc += "<div class=\"img\"><img src=\"CheckedReportOfficialSeal.png\"></div>";
                    }
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


        #endregion

        /// <summary>
        /// 重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_Reset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtSampleName.Text = "";
            ComboBoxItem.Text = "";
            DatePickerDateFrom.Text = "";
            DatePickerDateTo.Text = "";
            ComboBoxMethod.SelectedIndex = 0;
            ComboBoxCategory.SelectedIndex = 0;
            _pageIndex = 1;
            _IsCalcPage = true;
            SearchRecord();
        }

        private void miShowReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        private void TxtSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchRecord();
        }

        /// <summary>
        /// 调试模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void midebug_Click(object sender, RoutedEventArgs e)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show(this, "请选择需要调试的检测数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (_selectedRecords != null && _selectedRecords.Count > 0)
            {
                try
                {
                    string errMsg = string.Empty;
                    tlsTtResultSecond md = _selectedRecords[0];
                    DataTable dtbl = SqlHelper.GetDataTable("CurveDatas", string.Format("SysCode='{0}'", md.SysCode), out errMsg);
                    if (dtbl == null || dtbl.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "没有曲线数据!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }

                    errMsg = dtbl.Rows[0]["CData"].ToString();

                    //曲线数据
                    string[] data = errMsg.Split('|');
                    double[] debugData = new double[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        debugData[i] = double.Parse(data[i]);
                    }
                    JtjReportWindow window = new JtjReportWindow
                    {
                        IsDebug = true,
                        debugData = debugData
                    };
                    foreach (DYJTJItemPara item in Global.jtjItems)
                    {
                        if (item.Name == md.CheckTotalItem)
                        {
                            window._item = item;
                            break;
                        }
                    }
                    if (window._item == null)
                    {
                        window._item = new DYJTJItemPara
                        {
                            pointCNum = 6
                        };
                    }
                    window.CalcCurve();
                    window._item.Hole[0].Use = true;
                    window.CalcResult();
                    //new JtjReportWindow().CalcCurve();
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("查看曲线出现异常，请联系管理员", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}