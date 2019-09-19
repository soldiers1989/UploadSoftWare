using DYSeriesDataSet;
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
    /// SearchCompanys.xaml 的交互逻辑
    /// </summary>
    public partial class SearchCompanys : Window
    {
        private clsCompanyOpr _company = new clsCompanyOpr();
        private StringBuilder sb = new StringBuilder();
        public SearchCompanys()
        {
            InitializeComponent();
            //_msgThread = new MsgThread(this);
            //_msgThread.Start();
        }

        //List<DyInterfaceHelper.KjService.Regulatory.RegulatoryItem> models = null;
        //DyInterfaceHelper.KjService.Regulatory.RegulatoryItem _selectedModel = null;
        //DyInterfaceHelper.KjService.BusinessEntity.Business _selectedModel_B = null;
        //private MsgThread _msgThread;
        //class MsgThread : ChildThread
        //{
        //    SearchCompanys wnd;
        //    private delegate void UIHandleMessageDelegate(Message msg);
        //    private UIHandleMessageDelegate uiHandleMessageDelegate;

        //    public MsgThread(SearchCompanys wnd)
        //    {
        //        this.wnd = wnd;
        //        uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
        //    }

        //    protected override void HandleMessage(Message msg)
        //    {
        //        base.HandleMessage(msg);
        //        try
        //        {
        //            wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }

        //    protected void UIHandleMessage(Message msg)
        //    {
        //        switch (msg.what)
        //        {
        //            case MsgCode.MSG_KJ_DOWNLOAD_BASICDATA:
        //                if (msg.result)
        //                {
        //                    //监管对象
        //                    if (msg.str1.Equals("regulatory"))
        //                    {
        //                        wnd.ShowDatas();
        //                    }
        //                    //经营户
        //                    else
        //                    {
        //                        wnd.basicBusiness = Global.KjServer.business;
        //                    }
        //                }
        //                wnd.RefreshInfo(false);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Search_Click(null, null);
            Search();
        }
        string errMsg = string.Empty;
        private void Search()
        {
            DataGridRecord.DataContext = null;
            DataTable dt = _company.GetAsDataTable("", string.Empty, 13);
             
            DataGridRecord.DataContext = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DataGridRecord.ItemsSource = null;
            sb.Length = 0;
            sb.AppendFormat(" r.reg_name like '%{0}%'", txtVal.Text.Trim());
            DataTable dt = _company.GetAsDataTable(sb.ToString(), string.Empty, 14);
            this.DataGridRecord.ItemsSource = (dt != null && dt.Rows.Count > 0) ? dt.DefaultView : null;
            //string val = txtVal.Text.Trim();
            //if (val.Length == 0)
            //{
            //    try
            //    {
            //        Message msg = new Message()
            //        {
            //            what = MsgCode.MSG_KJ_DOWNLOAD_BASICDATA,
            //            str1 = "regulatory"
            //        };
            //        Global.workThread.SendMessage(msg, _msgThread);
            //        RefreshInfo();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("出现异常!\n" + ex.Message);
            //    }
            //}
            //else
            //{
            //    if (Global.KjServer.companys != null && Global.KjServer.companys.Count > 0)
            //    {
            //        models = new List<DyInterfaceHelper.KjService.Regulatory.RegulatoryItem>();
            //        for (int i = 0; i < Global.KjServer.companys.Count; i++)
            //        {
            //            int index = Global.KjServer.companys[i].reg_name.IndexOf(val);
            //            if (index >= 0)
            //            {
            //                models.Add(Global.KjServer.companys[i]);
            //            }
            //        }
            //        ShowDatas(models);
            //    }
            //}
        }

        private void LoadBusiness() 
        {
            //Message msg = new Message()
            //{
            //    what = MsgCode.MSG_KJ_DOWNLOAD_BASICDATA,
            //    str1 = "business"
            //};
            //Global.workThread.SendMessage(msg, _msgThread);
        }

        private void ShowDatas(List<DyInterfaceHelper.KjService.Regulatory.RegulatoryItem> models = null)
        {
            //本地查询
            if (models != null)
            {
                DataGridRecord.DataContext = models;
                return;
            }

            //在线加载数据
            if (Global.KjServer.companys != null && Global.KjServer.companys.Count > 0)
            {
                DataGridRecord.DataContext = Global.KjServer.companys;
            }
            else
            {
                DataGridRecord.DataContext = null;
            }

            if (basicBusiness == null)
            {
                LoadBusiness();
            }
        }

        List<DyInterfaceHelper.KjService.BusinessEntity.Business> basicBusiness = null;
        private void Btn_SearchBusiness_Click(object sender, RoutedEventArgs e)
        {
            KCompany window = new KCompany();
            window.ShowDialog();
            //if (Btn_SearchBusiness.Content.ToString().Equals("返    回"))
            //{
            //    miBack_Click(null, null);
            //    return;
            //}

            //if (_selectedModel == null)
            //{
            //    MessageBox.Show("没有可操作的数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}

            //if (basicBusiness == null) return;

            //List<DyInterfaceHelper.KjService.BusinessEntity.Business> businessList = new List<DyInterfaceHelper.KjService.BusinessEntity.Business>();
            //for (int i = 0; i < basicBusiness.Count; i++)
            //{
            //    if (basicBusiness[i].reg_id.Equals(_selectedModel.id))
            //    {
            //        businessList.Add(basicBusiness[i]);
            //    }
            //}

            //if (businessList.Count > 0)
            //{
            //    DataGridRecord.Visibility = Visibility.Collapsed;
            //    Btn_SearchBusiness.Content = "返    回";
            //    DataGridRecord_B.Visibility = Visibility.Visible;
            //    DataGridRecord_B.DataContext = businessList;
            //}
            //else
            //{
            //    MessageBox.Show("没有可操作的数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["reg_name"].ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("未选择任何被检单位!", "操作提示");
            }
            //if (_selectedModel == null)
            //{
            //    MessageBox.Show("没有可操作的数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}

            //Global.KjServer._selectCompany = _selectedModel;
            //Global.KjServer._selectBusiness = _selectedModel_B;
            //this.Close();
        }

        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Selected_Click(null, null);
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["reg_name"].ToString();
                this.Close();
            }
        }

        //private void RefreshInfo(bool request = true)
        //{
        //    if (request)
        //    {
        //        this.Title = "正在获取数据···";
        //        Btn_Search.Content = "正在加载";
        //        miSearch.IsEnabled = Btn_Search.IsEnabled = false;
        //        miSelected.IsEnabled = Btn_Selected.IsEnabled = false;
        //    }
        //    else
        //    {
        //        this.Title = "被检单位查询选择";
        //        Btn_Search.Content = "查询";
        //        miSearch.IsEnabled = Btn_Search.IsEnabled = true;
        //        miSelected.IsEnabled = Btn_Selected.IsEnabled = true;
        //    }
        //}

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DataGridRecord.Visibility == Visibility.Visible)
            //{
            //    if (DataGridRecord.SelectedItems.Count > 0)
            //    {
            //        _selectedModel = (DyInterfaceHelper.KjService.Regulatory.RegulatoryItem)DataGridRecord.SelectedItem;
            //    }
            //    else
            //    {
            //        _selectedModel = null;
            //    }
            //}
            //else
            //{
            //    if (DataGridRecord_B.SelectedItems.Count > 0)
            //    {
            //        _selectedModel_B = (DyInterfaceHelper.KjService.BusinessEntity.Business)DataGridRecord_B.SelectedItem;
            //    }
            //    else
            //    {
            //        _selectedModel_B = null;
            //    }
            //}
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void miBack_Click(object sender, RoutedEventArgs e)
        {
            DataGridRecord_B.Visibility = Visibility.Collapsed;
            DataGridRecord.Visibility = Visibility.Visible;
            Btn_SearchBusiness.Content = "查询经营户";
        }

    }
}