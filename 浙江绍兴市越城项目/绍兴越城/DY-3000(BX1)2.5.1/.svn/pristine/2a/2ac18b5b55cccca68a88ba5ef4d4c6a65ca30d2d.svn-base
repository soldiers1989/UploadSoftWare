using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AIO.src;
using AIO.xaml.Print;
using com.lvrenyang;
using DY.Process;
using DYSeriesDataSet;
using DyInterfaceHelper;
using System.Text;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// StandardRepositoryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StandardRepositoryWindow : Window
    {
        public StandardRepositoryWindow()
        {
            InitializeComponent();
        }
        public tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private List<FoodStandard> StandardLists = null;
        private List<FoodStandard> _SelectStandards = null;
        private string errMsg = string.Empty;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int _datasum = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageSum = 0;
        /// <summary>
        /// 每页显示数
        /// </summary>
        private int _pageSize = 100;
        /// <summary>
        /// 是否重新计算分页
        /// </summary>
        private bool _IsCalcPage = true;
        /// <summary>
        /// 初次进入界面事件不执行查询
        /// </summary>
        private bool _IsFirstLoad = true;
        private MsgThread _msgThread;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();

                buttonKjServerTest();//快检服务通信测试
                GetStandard();
                //读取Excel中的数据
                //DataTable dtbl = ExcelHelper.ImportExcel(string.Format("{0}\\Data\\Data.xls", Environment.CurrentDirectory), 0);
                
                
                //LoadStandard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("出现异常！\r\n\r\n异常信息：{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void GetStandard()
        {
            string whereSql = string.Empty, val = string.Empty;
            val = TxtStandard.Text.Trim();
            if (val.Length > 0)
            {
                whereSql += whereSql.Length == 0 ? string.Format("std_name Like '%{0}%'", val) : string.Format(" And std_name Like '%{0}%'", val);
            }
            GetPage(whereSql);
            DataTable dtbl = SqlHelper.GetDtblByPages("StandardList", whereSql, "imp_time DESC", _pageSize, _pageIndex);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                DataGridRecord.DataContext = dtbl;
                //StandardLists = (List<FoodStandard>)IListDataSet.DataTableToIList<FoodStandard>(dtbl);
                //DataGridRecord.DataContext = StandardLists;
                labelCount.Content = string.Format("共{0}条数据,当前第{1}页,共{2}页", _datasum, _pageIndex, _pageSum);
            }
            else
            {
                DataGridRecord.DataContext = null;
                labelCount.Content = "";
            }
            _IsFirstLoad = false;
        }

        private void LoadStandard()
        {
            string whereSql = string.Empty, val = string.Empty;

            if (!(bool)CboxGBStandard.IsChecked)
            {
                val = CboxGBStandard.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxHBStandard.IsChecked)
            {
                val = CboxHBStandard.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxDBStandard.IsChecked)
            {
                val = CboxDBStandard.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxNewUse.IsChecked)
            {
                val = CboxNewUse.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxInvalid.IsChecked)
            {
                val = CboxInvalid.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxTheUse.IsChecked)
            {
                val = CboxTheUse.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxReplaced.IsChecked)
            {
                val = CboxReplaced.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxBfReplaced.IsChecked)
            {
                val = CboxBfReplaced.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            whereSql = whereSql.Length > 0 ? string.Format("({0})", whereSql) : "";

            val = TxtStandard.Text.Trim();
            if (val.Length > 0)
            {
                whereSql += whereSql.Length == 0 ? string.Format("Name Like '%{0}%'", val) : string.Format(" And Name Like '%{0}%'", val);
            }

            //获取分页信息
            GetPageInfo(whereSql);

            DataTable dtbl = SqlHelper.GetDtblByPage("FoodStandard", whereSql, "ReleaseTime DESC", _pageSize, _pageIndex);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                DataGridRecord.DataContext = dtbl;
                //StandardLists = (List<FoodStandard>)IListDataSet.DataTableToIList<FoodStandard>(dtbl);
                //DataGridRecord.DataContext = StandardLists;
                labelCount.Content = string.Format("共{0}条数据,当前第{1}页,共{2}页", _datasum, _pageIndex, _pageSum);
            }
            else
            {
                DataGridRecord.DataContext = null;
                labelCount.Content = "";
            }
            _IsFirstLoad = false;
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        private void GetPageInfo(string sql)
        {
            if (!_IsCalcPage) return;

            //获取分页信息
            DataTable dtbl = SqlHelper.GetDataTable("", "", out errMsg, "Select Count(ID) AS PageSun From FoodStandard Where " + (sql.Length == 0 ? "1=1" : sql));
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                _datasum = int.Parse(dtbl.Rows[0]["PageSun"].ToString());
                //判定总页数
                if (_datasum % _pageSize == 0)
                    _pageSum = _datasum / _pageSize;
                else
                    _pageSum = _datasum / _pageSize + 1;
            }
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        private void GetPage(string sql)
        {
            if (!_IsCalcPage) return;

            //获取分页信息
            DataTable dtbl = SqlHelper.GetDataTable("", "", out errMsg, "Select Count(ID) AS PageSun From StandardList Where " + (sql.Length == 0 ? "1=1" : sql));
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                _datasum = int.Parse(dtbl.Rows[0]["PageSun"].ToString());
                //判定总页数
                if (_datasum % _pageSize == 0)
                    _pageSum = _datasum / _pageSize;
                else
                    _pageSum = _datasum / _pageSize + 1;
            }
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (pdfwindow != null) pdfwindow.Close();
            this.Close();
        }

        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            ShowContent();
        }

        private void miExport_Click(object sender, RoutedEventArgs e)
        {
            if (_SelectStandards.Count == 0)
            {
                MessageBox.Show("请选择需要导出的标准！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            try
            {
                //SaveFileDialog saveFileDialog = new SaveFileDialog()
                //    {
                //        //设置导出路径
                //        CheckPathExists = true,
                //        DefaultExt = "xls",
                //        FileName = string.Format("{0}.pdf", _Standards[0].Name),
                //        //Filter = "Excel文件(*.xls)|*.xls|All files (*.*)|*.*"
                //    };
                //if ((bool)(saveFileDialog.ShowDialog()))
                //{
                System.IO.File.Copy(string.Format("{0}\\{1}", Environment.CurrentDirectory, _SelectStandards[0].Path), "C:\\" + _SelectStandards[0].Name + ".pdf", true);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出标准时出现异常！\r\n\r\n" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowContent();
        }

        private string pdfUrl = "http://fst.chinafst.cn:9002/laws/pdf/";
        private CheckReportForm pdfwindow = null;
        private void ShowContent()
        {
            if(DataGridRecord.SelectedItems.Count ==0)
            {
                MessageBox.Show("请选择需要查看的标准！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (_SelectStandards.Count == 0)
            {
                MessageBox.Show("请选择需要查看的标准！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            try
            {
                //string path = string.Format(@"{0}\KnowledgeBbase\食品标准\{1}", Environment.CurrentDirectory, _SelectStandards[0].Path);
                //Process.Start(path);

                string path = pdfUrl + "食品标准/" + _SelectStandards[0].Path;
                if (pdfwindow != null) { pdfwindow.Close(); }
                pdfwindow = new CheckReportForm();
                pdfwindow.HtmlUrl = path;
                pdfwindow.IsLoadPdf = true;
                pdfwindow.ShowInTaskbar = false;
                pdfwindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载食品标准时出现异常！\r\n异常信息：{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtStandard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (TxtStandard.Text.Trim().Length > 0)
            {
                _IsCalcPage = true;
                LoadStandard();
            }
        }

        private void CboxGBStandard_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxHBStandard_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxDBStandard_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxNewUse_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxTheUse_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxInvalid_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxReplaced_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxBfReplaced_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count == 0) return;
            _SelectStandards = new List<FoodStandard>();

            for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
            {
                FoodStandard model = new FoodStandard();
                model.Name = (DataGridRecord.SelectedItem as DataRowView).Row["std_name"].ToString();
                model.ReleaseUnit = (DataGridRecord.SelectedItem as DataRowView).Row["std_unit"].ToString();
                model.ImplementationTime = (DataGridRecord.SelectedItem as DataRowView).Row["imp_time"].ToString();
                model.Type = (DataGridRecord.SelectedItem as DataRowView).Row["std_type"].ToString();
                model.Path = (DataGridRecord.SelectedItem as DataRowView).Row["url_path"].ToString();
                model.ReleaseTime = (DataGridRecord.SelectedItem as DataRowView).Row["rel_time"].ToString();
                model.State = (DataGridRecord.SelectedItem as DataRowView).Row["std_status"].ToString();
                model.Title = (DataGridRecord.SelectedItem as DataRowView).Row["std_title"].ToString();
                model.StandardID = (DataGridRecord.SelectedItem as DataRowView).Row["sid"].ToString();

                //model.Name = (DataGridRecord.SelectedItem as DataRowView).Row["Name"].ToString();
                //model.ReleaseUnit = (DataGridRecord.SelectedItem as DataRowView).Row["ReleaseUnit"].ToString();
                //model.ImplementationTime = (DataGridRecord.SelectedItem as DataRowView).Row["ImplementationTime"].ToString();
                //model.Type = (DataGridRecord.SelectedItem as DataRowView).Row["Type"].ToString();
                //model.Path = (DataGridRecord.SelectedItem as DataRowView).Row["Path"].ToString();
                //model.ReleaseTime = (DataGridRecord.SelectedItem as DataRowView).Row["ReleaseTime"].ToString();
                //model.State = (DataGridRecord.SelectedItem as DataRowView).Row["State"].ToString();
                //model.Title = (DataGridRecord.SelectedItem as DataRowView).Row["Title"].ToString();
                //model.StandardID = (DataGridRecord.SelectedItem as DataRowView).Row["StandardID"].ToString();
                _SelectStandards.Add(model);
            }
        }
        /// <summary>
        /// 快件服务通讯通讯测试（用户登录）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonKjServerTest()
        {
            if (!Global.IsConnectInternet())
            {
                //MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Global.KjServer.KjServerAddr = "http://fc.chinafst.cn:9002/fc";

            Message _msg = new Message
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 = "sa001",
                str2 = "123456",
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }

        /// <summary>
        /// 仪器注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKjregisterDevice()
        {
            if (!Global.IsConnectInternet())
            {
                //MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Global.KjServer.Kjuser_name = "sa001";
            Global.KjServer.Kjpassword = "123456";
            DyInterfaceHelper.KjService.Url_Server = "http://fc.chinafst.cn:9002/fc";

            Message _msg = new Message
            {
                what = MsgCode.MSG_REGISTERDEVICE,
                str1 = "DY-3000(BX1)",//仪器型号
                str2 = "7C:B0:C2:B1:F5:8F"//MAC地址
            };
            Global.workThread.SendMessage(_msg, _msgThread);
        }
        private int stdcount = 0;
        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            btnKjregisterDevice();//仪器注册

            stdcount = 0;
            Btn_Update.IsEnabled = false;
            TimerWindow window = new TimerWindow();
            window.lb_NTTimer.Content = "正在获取更新数据，请稍等···";
            window.ShowInTaskbar = false;
            window.ShowDialog();

            Message msg = new Message()
            {
                what = MsgCode.MSG_STANDARD,
                str1 = "http://fc.chinafst.cn:9002/fc",
               
            };

            Global.workThread.SendMessage(msg, _msgThread);
            //Thread.Sleep(2000);
            //window.Close();
            //MessageBox.Show("已更新为最新数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //Btn_Update.IsEnabled = true;
        }

        private void Btn_ShowPdf_Click(object sender, RoutedEventArgs e)
        {
            ShowContent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StandardLists != null && StandardLists.Count > 0)
            {
                string path = string.Empty;
                int count = 0;
                for (int i = 0; i < StandardLists.Count; i++)
                {
                    try
                    {
                        path = string.Format(@"{0}\KnowledgeBbase\食品标准\{1}", Environment.CurrentDirectory, StandardLists[i].Path);
                        if (!File.Exists(path))
                        {
                            count++;
                            FileUtils.ErrorLog("食品标准", string.Format("{0}:{1}", StandardLists[i].StandardID, StandardLists[i].Path));
                            Label.Content = count.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(6, "食品标准", string.Format("Error:{0}\r\nPath:{1}", ex.Message, path));
                        count++;
                        Label.Content = count.ToString();
                    }
                }
            }
        }

        private void TxtPageSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (!int.TryParse(TxtPageSize.Text.Trim(), out _pageSize))
            {
                TxtPageSize.Text = "100";
                return;
            }

            _pageIndex = 1;
            textBoxPage.Text = "1";
            _IsCalcPage = true;
            LoadStandard();
        }

        private void textBoxPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (!int.TryParse(textBoxPage.Text.Trim(), out _pageIndex))
            {
                textBoxPage.Text = "1";
                return;
            }
            if (_pageIndex > _pageSum)
            {
                _pageIndex = _pageSum;
                textBoxPage.Text = _pageIndex + "";
            }
            else if (_pageIndex < 0)
            {
                _pageIndex = 0;
                textBoxPage.Text = "0";
            }
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _pageIndex = 1;
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        //末页
        private void btnEndPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _pageIndex = _pageSum;
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (_pageIndex > 1)
            {
                _pageIndex--;
            }
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (_pageIndex < _pageSum)
            {
                _pageIndex++;
            }
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }
        private string lawlists = "";


      
        private string standlist = "";
        private void DownStandard(string standards)
        {
            standlist = standards;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = this.DownLoadStandProcess,
                MessageInfo = "正在下载食品标准"
            };
            process.Start();
        }
        private void DownLoadStandProcess(Action<int> percent)
        {
            percent(1);
            try
            {
                if (standlist != null && standlist.Length > 0)
                {

                    standardlist zdata = JsonHelper.JsonToEntity<standardlist>(standlist);
                    if (zdata.standard.Count > 0)
                    {
                        int icount = 0;

                        int rt = 0;
                        float p = 0;
                        float sp = 0;
                        int showp = 0;
                        p = (float)100 / (float)zdata.standard.Count;
                        string err = "";
                        standard sd = new standard();

                        for (int i = 0; i < zdata.standard.Count; i++)
                        {
                            sp = sp + p;
                            showp = (int)sp;
                            percent(showp);
                            stdcount = stdcount+1;
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("sid='{0}'", zdata.standard[i].id);

                            DataTable dt = _bll.GetStandard(sb.ToString(), "", out err);
                            sd = zdata.standard[i];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                rt = _bll.UpdateStandard(sd);
                                if (rt == 1)
                                {
                                    icount = icount + 1;
                                }
                            }
                            else
                            {
                                rt = _bll.InsertStandard(sd);
                                if (rt == 1)
                                {
                                    icount = icount + 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        stdcount = 0;
                    }

                }
                else
                {
                    stdcount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
                percent(100);
            }
            finally
            {
                percent(100);
            }
        }

        class MsgThread : ChildThread
        {
            StandardRepositoryWindow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(StandardRepositoryWindow wnd)
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
                            wnd.DownStandard(msg.responseInfo);
                        }
                        if (wnd.stdcount == 0)
                        {
                            MessageBox.Show("已更新为最新数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("食品标准更新成功！共成功下载" + wnd.stdcount + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        wnd.GetStandard();
                        wnd.Btn_Update.IsEnabled = true;
                        //wnd.SearchStand();
                        //wnd.btnStandard.IsEnabled = true;
                        break;
                  
                    default:
                        break;
                }
            }
        }

    }


    #region 标准entity
    public class FoodStandard
    {
        private string _StandardID = string.Empty;
        /// <summary>
        /// 文件下载对应ID
        /// </summary>
        public string StandardID
        {
            get { return _StandardID; }
            set { _StandardID = value; }
        }

        private string _Name = string.Empty;
        /// <summary>
        /// 标准名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _State = string.Empty;
        /// <summary>
        /// 标准状态
        /// </summary>
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _Type = string.Empty;
        /// <summary>
        /// 标准类型
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _ReleaseTime = string.Empty;
        /// <summary>
        /// 发布时间
        /// </summary>
        public string ReleaseTime
        {
            get
            {
                try
                {
                    return _ReleaseTime.Length > 0 ? DateTime.Parse(_ReleaseTime).ToString("yyyy-MM-dd") : _ReleaseTime;
                }
                catch (Exception)
                {
                    return _ReleaseTime;
                }
            }
            set { _ReleaseTime = value; }
        }

        private string _ImplementationTime = string.Empty;
        /// <summary>
        /// 实施时间
        /// </summary>
        public string ImplementationTime
        {
            get
            {
                try
                {
                    return _ImplementationTime.Length > 0 ? DateTime.Parse(_ImplementationTime).ToString("yyyy-MM-dd") : _ImplementationTime;
                }
                catch (Exception)
                {
                    return _ImplementationTime;
                }
            }
            set { _ImplementationTime = value; }
        }

        private string _TypeNum = string.Empty;
        /// <summary>
        /// 国家分类号
        /// </summary>
        public string TypeNum
        {
            get { return _TypeNum; }
            set { _TypeNum = value; }
        }

        private string _ICSType = string.Empty;
        /// <summary>
        /// ICS分类
        /// </summary>
        public string ICSType
        {
            get { return _ICSType; }
            set { _ICSType = value; }
        }

        private string _BeReplacedNum = string.Empty;
        /// <summary>
        /// 被替代标准号
        /// </summary>
        public string BeReplacedNum
        {
            get { return _BeReplacedNum; }
            set { _BeReplacedNum = value; }
        }

        private string _InsteadNum = string.Empty;
        /// <summary>
        /// 代替标准号
        /// </summary>
        public string InsteadNum
        {
            get { return _InsteadNum; }
            set { _InsteadNum = value; }
        }

        private string _AttributionUnit = string.Empty;
        /// <summary>
        /// 归口单位
        /// </summary>
        public string AttributionUnit
        {
            get { return _AttributionUnit; }
            set { _AttributionUnit = value; }
        }

        private string _ReleaseUnit = string.Empty;
        /// <summary>
        /// 发布单位
        /// </summary>
        public string ReleaseUnit
        {
            get { return _ReleaseUnit; }
            set { _ReleaseUnit = value; }
        }

        private string _Introduction = string.Empty;
        /// <summary>
        /// 标准简介
        /// </summary>
        public string Introduction
        {
            get { return _Introduction; }
            set { _Introduction = value; }
        }

        private string _Title = string.Empty;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Notes = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private string _Path = string.Empty;
        /// <summary>
        /// pdf文件路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    #endregion

}