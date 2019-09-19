using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using WpfPrintDemo;
using System.Collections;

namespace AIO.xaml.Print
{
    /// <summary>
    /// ReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private string _SqlWhere = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnDeletedAll.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            this.btnPrintHZ.Visibility = Visibility.Collapsed;
            if (Global.EachDistrict.Equals(string.Empty))
            {
                this.DataGridRecord.Visibility = Visibility.Visible;
                this.SearchReport();
            }
            else if (Global.EachDistrict.Equals("GS"))
            {
                this.miEditGS.Visibility = miDeletedGS.Visibility = btnUpdate.Visibility = btnDeleted.Visibility = Visibility.Collapsed;
                this.btnPrint.Margin = new Thickness(126, 20, 0, 20);
                this.btnPrint.Content = "打印报表";
                this.btnPrintHZ.Margin = new Thickness(232, 20, 0, 20);
                this.btnPrintHZ.Visibility = Visibility.Visible;
                this.dataGridGS.Visibility = Visibility.Visible;
                this.DataGridRecord.Visibility = Visibility.Collapsed;
                this.CheckBoxUser.Content = "食品名称";
                this.SearchReportGS();
            }
            else
            {
                this.dataGridGS.Visibility = Visibility.Collapsed;
                this.DataGridRecord.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 查询甘肃报表
        /// </summary>
        private void SearchReportGS()
        {
            this.dataGridGS.DataContext = null;
            string error = string.Empty;
            try
            {
                SqlWhere();
                DataTable dt = _resultTable.GetReport(_SqlWhere, 2, out error);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsReportGS> Items = (List<clsReportGS>)IListDataSet.DataTableToIList<clsReportGS>(dt, 1);
                    this.dataGridGS.DataContext = Items;
                }
                else if (!error.Equals(string.Empty))
                    MessageBox.Show(error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(SearchReport):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 查询原始报表
        /// </summary>
        private void SearchReport()
        {
            this.DataGridRecord.DataContext = null;
            try
            {
                SqlWhere();
                string error = string.Empty;
                DataTable dt = _resultTable.GetReport(_SqlWhere, 1, out error);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsReport> Items = (List<clsReport>)IListDataSet.DataTableToIList<clsReport>(dt, 1);
                    this.DataGridRecord.DataContext = Items;
                }
                else if (!error.Equals(string.Empty))
                    MessageBox.Show(error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(SearchReport):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 组装查询条件
        /// </summary>
        private void SqlWhere()
        {
            try
            {
                _SqlWhere = string.Empty;
                if (!this.ComboBoxCheckUnitName.Text.Trim().Equals(string.Empty) && !this.ComboBoxCheckedCompanyName.Text.Trim().Equals(string.Empty))
                {
                    if (Global.EachDistrict.Equals(string.Empty))
                    {
                        _SqlWhere = " CheckUnitName Like '%" + this.ComboBoxCheckUnitName.Text.Trim() + "%'";
                        _SqlWhere += " And CheckedCompanyName Like '%" + this.ComboBoxCheckedCompanyName.Text.Trim() + "%'";
                        _SqlWhere += " And IsDeleted='N'";
                    }
                    else if (Global.EachDistrict.Equals("GS"))
                    {
                        _SqlWhere = " FoodName Like '%" + this.ComboBoxCheckUnitName.Text.Trim() + "%'";
                        _SqlWhere += " And CheckedCompanyName Like '%" + this.ComboBoxCheckedCompanyName.Text.Trim() + "%'";
                    }
                }
                else if (this.ComboBoxCheckUnitName.Text.Trim().Equals(string.Empty) && !this.ComboBoxCheckedCompanyName.Text.Trim().Equals(string.Empty))
                {
                    _SqlWhere = " CheckedCompanyName Like '%" + this.ComboBoxCheckedCompanyName.Text.Trim() + "%'";
                    if (Global.EachDistrict.Equals(string.Empty))
                        _SqlWhere += " And IsDeleted='N'";
                }
                else if (!this.ComboBoxCheckUnitName.Text.Trim().Equals(string.Empty) && this.ComboBoxCheckedCompanyName.Text.Trim().Equals(string.Empty))
                {
                    if (Global.EachDistrict.Equals(string.Empty))
                    {
                        _SqlWhere = " CheckUnitName Like '%" + this.ComboBoxCheckUnitName.Text.Trim() + "%'";
                        _SqlWhere += " And IsDeleted='N'";
                    }
                    else if (Global.EachDistrict.Equals("GS"))
                    {
                        _SqlWhere = " FoodName Like '%" + this.ComboBoxCheckUnitName.Text.Trim() + "%'";
                    }
                }
                else
                {
                    if (Global.EachDistrict.Equals(string.Empty))
                        _SqlWhere = " IsDeleted='N'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(SqlWhere):\n" + ex.Message);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miReportPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        /// <summary>
        /// 打印甘肃回执
        /// </summary>
        private void PrintGSHZ()
        {
            Global.GSType = "HZ";
            clsReportGS record;
            try
            {
                if (dataGridGS.SelectedItems.Count > 0)
                {
                    IDictionary<string, string> dicItems = new Dictionary<string, string>();
                    IDictionary<string, string> dicSample = new Dictionary<string, string>();
                    record = (clsReportGS)dataGridGS.SelectedItems[0];
                    clsReportGS report = null;
                    if (record != null)
                    {
                        report = new clsReportGS();
                        report.Title = record.Title;
                        report.FoodName = record.FoodName;
                        report.FoodType = record.FoodType;
                        report.ProductionDate = record.ProductionDate;
                        report.CheckedCompanyName = record.CheckedCompanyName == null ? "______" : record.CheckedCompanyName;
                        report.CheckedCompanyAddress = record.CheckedCompanyAddress == null ? "______" : record.CheckedCompanyAddress;
                        report.CheckedCompanyPhone = record.CheckedCompanyPhone;
                        report.LabelProducerName = record.LabelProducerName;
                        report.LabelProducerAddress = record.LabelProducerAddress == null ? "______" : record.LabelProducerAddress;
                        report.LabelProducerPhone = record.LabelProducerPhone;
                        if (record.SamplingData != null)
                        {
                            report.SamplingData = record.SamplingData == null ? "____年__月__日" : record.SamplingData;
                            string str = record.SamplingData;
                            string[] s = str.Split(new char[] { '-' });
                            report.SamplingData = s[0] + "年" + s[1] + "月" + s[2] + "日";
                        }
                        else
                        {
                            report.SamplingData = "____年__月__日";
                        }
                        report.SamplingPerson = record.SamplingPerson;
                        report.SampleNum = record.SampleNum;
                        report.SamplingBase = record.SamplingBase;
                        report.SamplingAddress = record.SamplingAddress;
                        report.SamplingOrderCode = record.SamplingOrderCode;
                        report.Standard = record.Standard;
                        report.InspectionConclusion = record.InspectionConclusion;
                        report.Notes = record.Notes;
                        report.Surveyor = record.Surveyor;
                        report.ItemsList = report.UnqualifiedItemsList = report.SampleList = string.Empty;
                        //根据主表ID查询子表样品信息集合
                        DataTable dt = _resultTable.GetReportDetailGS(record.ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<clsReportGSDetail> ItemNames = (List<clsReportGSDetail>)IListDataSet.DataTableToIList<clsReportGSDetail>(dt, 1);
                            for (int i = 0; i < ItemNames.Count; i++)
                            {
                                clsReportGS.ReportDetail reportDetail = new clsReportGS.ReportDetail();
                                reportDetail.ProjectName = ItemNames[i].ProjectName;
                                reportDetail.Unit = ItemNames[i].Unit;
                                reportDetail.InspectionStandard = ItemNames[i].InspectionStandard;
                                reportDetail.IndividualResults = ItemNames[i].IndividualResults;
                                reportDetail.IndividualDecision = ItemNames[i].IndividualDecision;
                                report.reportDetailList.Add(reportDetail);
                                if (!dicItems.ContainsKey(ItemNames[i].ProjectName))
                                {
                                    if (i + 1 < ItemNames.Count)
                                        report.ItemsList += ItemNames[i].ProjectName + ",";
                                    else
                                        report.ItemsList += ItemNames[i].ProjectName;
                                    dicItems.Add(ItemNames[i].ProjectName, ItemNames[i].ProjectName);
                                }
                                if (!dicSample.ContainsKey(ItemNames[i].ProjectName))
                                {
                                    if (ItemNames[i].IndividualDecision.Equals("不合格"))
                                    {
                                        if (i + 1 < ItemNames.Count)
                                            report.UnqualifiedItemsList += ItemNames[i].ProjectName + ",";
                                        else
                                            report.UnqualifiedItemsList += ItemNames[i].ProjectName;
                                        dicSample.Add(ItemNames[i].ProjectName, ItemNames[i].ProjectName);
                                    }
                                }
                            }
                            if (report.UnqualifiedItemsList.Equals(string.Empty))
                                report.UnqualifiedItemsList = "____________________";
                            if (report.ItemsList.Equals(string.Empty))
                                report.ItemsList = "____________________";
                        }
                    }
                    //甘肃打印回执
                    PrintPreviewWindow print = new PrintPreviewWindow("xaml\\Print\\PrintModel\\GSHZDocument.xaml", report, new OrderDocumentRenderer());
                    print._resultGS = record;
                    print.Owner = this;
                    print.ShowInTaskbar = false;
                    print.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择打印条目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Print):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 打印甘肃报表
        /// </summary>
        private void PrintGS()
        {
            Global.GSType = string.Empty;
            clsReportGS record;
            try
            {
                if (dataGridGS.SelectedItems.Count > 0)
                {
                    record = (clsReportGS)dataGridGS.SelectedItems[0];
                    clsReportGS report = null;
                    if (record != null)
                    {
                        report = new clsReportGS();
                        report.Title = record.Title;
                        report.FoodName = record.FoodName;
                        report.FoodType = record.FoodType;
                        report.ProductionDate = record.ProductionDate;
                        report.CheckedCompanyName = record.CheckedCompanyName;
                        report.CheckedCompanyAddress = record.CheckedCompanyAddress;
                        report.CheckedCompanyPhone = record.CheckedCompanyPhone;
                        report.LabelProducerName = record.LabelProducerName;
                        report.LabelProducerAddress = record.LabelProducerAddress;
                        report.LabelProducerPhone = record.LabelProducerPhone;
                        report.SamplingData = record.SamplingData;
                        report.SamplingPerson = record.SamplingPerson;
                        report.SampleNum = record.SampleNum;
                        report.SamplingBase = record.SamplingBase;
                        report.SamplingAddress = record.SamplingAddress;
                        report.SamplingOrderCode = record.SamplingOrderCode;
                        report.Standard = record.Standard;
                        report.InspectionConclusion = record.InspectionConclusion;
                        report.Notes = record.Notes;
                        report.Surveyor = record.Surveyor;

                        //根据主表ID查询子表样品信息集合
                        DataTable dt = _resultTable.GetReportDetailGS(record.ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<clsReportGSDetail> ItemNames = (List<clsReportGSDetail>)IListDataSet.DataTableToIList<clsReportGSDetail>(dt, 1);
                            for (int i = 0; i < ItemNames.Count; i++)
                            {
                                clsReportGS.ReportDetail reportDetail = new clsReportGS.ReportDetail();
                                reportDetail.ProjectName = ItemNames[i].ProjectName;
                                reportDetail.Unit = ItemNames[i].Unit;
                                reportDetail.InspectionStandard = ItemNames[i].InspectionStandard;
                                reportDetail.IndividualResults = ItemNames[i].IndividualResults;
                                reportDetail.IndividualDecision = ItemNames[i].IndividualDecision;
                                report.reportDetailList.Add(reportDetail);
                            }
                        }
                    }
                    //甘肃打印模板
                    PrintPreviewWindow print = new PrintPreviewWindow("xaml\\Print\\PrintModel\\GSDocument.xaml", report, new OrderDocumentRenderer());
                    print._resultGS = record;
                    print.Owner = this;
                    print.ShowInTaskbar = false;
                    print.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择打印条目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Print):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 打印原始报表
        /// </summary>
        private void Print()
        {
            clsReport record;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    record = (clsReport)DataGridRecord.SelectedItems[0];
                    ReportClass report = null;
                    if (record != null)
                    {
                        report = new ReportClass();
                        report.r_Title = record.Title;
                        report.r_CheckUnitName = record.CheckUnitName;
                        report.r_Trademark = record.Trademark;
                        report.r_Specifications = record.Specifications;
                        report.r_ProductionDate = record.ProductionDate;
                        report.r_QualityGrade = record.QualityGrade;
                        report.r_CheckedCompanyName = record.CheckedCompanyName;
                        report.r_CheckedCompanyPhone = record.CheckedCompanyPhone;
                        report.r_ProductionUnitsName = record.ProductionUnitsName;
                        report.r_ProductionUnitsPhone = record.ProductionUnitsPhone;
                        report.r_TaskSource = record.TaskSource;
                        report.r_Standard = record.Standard;
                        report.r_SamplingData = record.SamplingData;
                        report.r_SampleNum = record.SampleNum;
                        report.r_SamplingCode = record.SamplingCode;
                        report.r_SampleArrivalData = record.SampleArrivalData;
                        report.r_Note = record.Notes;

                        //根据主表ID查询子表样品信息集合
                        DataTable dt = _resultTable.GetReportDetail(record.ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<clsReportDetail> ItemNames = (List<clsReportDetail>)IListDataSet.DataTableToIList<clsReportDetail>(dt, 1);
                            for (int i = 0; i < ItemNames.Count; i++)
                            {
                                ReportClass.ReportDetail reportDetail = new ReportClass.ReportDetail();
                                reportDetail.FoodName = ItemNames[i].FoodName;
                                reportDetail.ProjectName = ItemNames[i].ProjectName;
                                reportDetail.Unit = ItemNames[i].Unit;
                                reportDetail.CheckData = ItemNames[i].CheckData;
                                report.r_reportList.Add(reportDetail);
                            }
                        }
                    }
                    //原始打印模板
                    PrintPreviewWindow print = new PrintPreviewWindow("xaml\\Print\\PrintModel\\OrderDocument.xaml", report, new OrderDocumentRenderer());
                    print._result = record;
                    print.Owner = this;
                    print.ShowInTaskbar = false;
                    print.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择打印条目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Print):\n" + ex.Message);
            }
        }

        private void miEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        /// <summary>
        /// 编辑甘肃报表
        /// </summary>
        private void EditGS()
        {
            clsReportGS report;
            try
            {
                if (dataGridGS.SelectedItems.Count > 0)
                {
                    report = (clsReportGS)dataGridGS.SelectedItems[0];
                    //根据主表ID查询子表样品信息集合
                    DataTable dt = _resultTable.GetReportDetailGS(report.ID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsReportGSDetail> ItemNames = (List<clsReportGSDetail>)IListDataSet.DataTableToIList<clsReportGSDetail>(dt, 1);
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            DYSeriesDataSet.DataModel.clsReportGS.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReportGS.ReportDetail();
                            reportDetail.ID = ItemNames[i].ID;
                            reportDetail.ReportGSID = ItemNames[i].ReportGSID;
                            reportDetail.ProjectName = ItemNames[i].ProjectName;
                            reportDetail.InspectionStandard = ItemNames[i].InspectionStandard;
                            reportDetail.IndividualResults = ItemNames[i].IndividualResults;
                            reportDetail.Unit = ItemNames[i].Unit;
                            reportDetail.IndividualDecision = ItemNames[i].IndividualDecision;
                            report.reportDetailList.Add(reportDetail);
                        }
                    }
                    UpdateReportGSWindow window = new UpdateReportGSWindow();
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.GetValues(report);
                    window.ShowDialog();
                    SearchReportGS();
                }
                else
                {
                    MessageBox.Show("请选择编辑条目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Edit):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 编辑原始报表
        /// </summary>
        private void Edit()
        {
            clsReport report;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    report = (clsReport)DataGridRecord.SelectedItems[0];
                    //根据主表ID查询子表样品信息集合
                    DataTable dt = _resultTable.GetReportDetail(report.ID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsReportDetail> ItemNames = (List<clsReportDetail>)IListDataSet.DataTableToIList<clsReportDetail>(dt, 1);
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            DYSeriesDataSet.DataModel.clsReport.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReport.ReportDetail();
                            reportDetail.FoodName = ItemNames[i].FoodName;
                            reportDetail.ProjectName = ItemNames[i].ProjectName;
                            reportDetail.Unit = ItemNames[i].Unit;
                            reportDetail.CheckData = ItemNames[i].CheckData;
                            report.reportDetailList.Add(reportDetail);
                        }
                    }
                    UpdateReportWindow window = new UpdateReportWindow();
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.GetValues(report);
                    window.ShowDialog();
                    SearchReport();
                }
                else
                {
                    MessageBox.Show("请选择编辑条目!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Edit):\n" + ex.Message);
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clsReport report;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    report = (clsReport)DataGridRecord.SelectedItems[0];
                    //根据主表ID查询子表样品信息集合
                    DataTable dt = _resultTable.GetReportDetail(report.ID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsReportDetail> ItemNames = (List<clsReportDetail>)IListDataSet.DataTableToIList<clsReportDetail>(dt, 1);
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            DYSeriesDataSet.DataModel.clsReport.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReport.ReportDetail();
                            reportDetail.FoodName = ItemNames[i].FoodName;
                            reportDetail.ProjectName = ItemNames[i].ProjectName;
                            reportDetail.Unit = ItemNames[i].Unit;
                            reportDetail.CheckData = ItemNames[i].CheckData;
                            report.reportDetailList.Add(reportDetail);
                        }
                    }
                    UpdateReportWindow window = new UpdateReportWindow();
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.GetValues(report);
                    window.ShowDialog();
                    SearchReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Edit):\n" + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                Edit();
            else if (Global.EachDistrict.Equals("GS"))
                EditGS();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                Print();
            else if (Global.EachDistrict.Equals("GS"))
                PrintGS();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                SearchReport();
            else if (Global.EachDistrict.Equals("GS"))
                SearchReportGS();
        }
        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                Deletd();
            else if (Global.EachDistrict.Equals("GS"))
                DeletdGS();
        }

        /// <summary>
        /// 删除报表 甘肃
        /// </summary>
        private void DeletdGS()
        {
            string error = string.Empty;
            try
            {
                if (dataGridGS.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("请选择删除条目!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗!？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        clsReportGS report = (clsReportGS)dataGridGS.SelectedItems[0];
                        if (report != null && report.ID > 0)
                        {
                            _resultTable.DeletedGS(report.ID, out error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (!error.Equals(string.Empty))
                        {
                            MessageBox.Show("删除时出现异常!\n" + error);
                        }
                        else
                        {
                            MessageBox.Show("删除报表数据成功!");
                        }
                        SearchReportGS();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Deletd):\n" + ex.Message);
            }
        }

        /// <summary>
        /// 删除原始报表
        /// </summary>
        private void Deletd()
        {
            string error = string.Empty;
            try
            {
                if (DataGridRecord.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("请选择删除条目!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗!？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        clsReport report = (clsReport)DataGridRecord.SelectedItems[0];
                        if (report != null && report.ID > 0)
                            _resultTable.Deleted(report.ID, out error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (!error.Equals(string.Empty))
                        {
                            MessageBox.Show("删除时出现异常!\n" + error);
                        }
                        else
                        {
                            MessageBox.Show("删除报表数据成功!");
                        }
                        SearchReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常(Deletd):\n" + ex.Message);
            }
        }

        private void miDeleted_Click(object sender, RoutedEventArgs e)
        {
            Deletd();
        }

        /// <summary>
        /// 清空所有报表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletedAll_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, strErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有报表吗!？\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _resultTable.DeletedReportAll(out sErr);
                sErr += strErr;
                str = sErr.Equals(string.Empty) ? "已成功清理所有报表!" : ("清理报表时出现错误!\n异常：" + sErr);
                SearchReport();
                MessageBox.Show(str, "操作提示");
            }
        }

        /// <summary>
        /// 双击编辑报表 甘肃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridGS_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Global.EachDistrict.Equals("GS"))
            {
                clsReportGS report;
                try
                {
                    if (dataGridGS.SelectedItems.Count > 0)
                    {
                        report = (clsReportGS)dataGridGS.SelectedItems[0];
                        //根据主表ID查询子表样品信息集合
                        DataTable dt = _resultTable.GetReportDetailGS(report.ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<clsReportGSDetail> ItemNames = (List<clsReportGSDetail>)IListDataSet.DataTableToIList<clsReportGSDetail>(dt, 1);
                            for (int i = 0; i < ItemNames.Count; i++)
                            {
                                DYSeriesDataSet.DataModel.clsReportGS.ReportDetail reportDetail = new DYSeriesDataSet.DataModel.clsReportGS.ReportDetail();
                                reportDetail.ID = ItemNames[i].ID;
                                reportDetail.ReportGSID = ItemNames[i].ReportGSID;
                                reportDetail.ProjectName = ItemNames[i].ProjectName;
                                reportDetail.InspectionStandard = ItemNames[i].InspectionStandard;
                                reportDetail.IndividualResults = ItemNames[i].IndividualResults;
                                reportDetail.Unit = ItemNames[i].Unit;
                                reportDetail.IndividualDecision = ItemNames[i].IndividualDecision;
                                report.reportDetailList.Add(reportDetail);
                            }
                        }
                        UpdateReportGSWindow window = new UpdateReportGSWindow();
                        window.ShowInTaskbar = false;
                        window.Owner = this;
                        window.GetValues(report);
                        window.ShowDialog();
                        SearchReportGS();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("异常(Edit):\n" + ex.Message);
                }
            }
        }

        private void dataGridGS_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 编辑报表 甘肃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditGS_Click(object sender, RoutedEventArgs e)
        {
            EditGS();
        }

        /// <summary>
        /// 打印报表 甘肃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miReportPrintGS_Click(object sender, RoutedEventArgs e)
        {
            PrintGS();
        }

        /// <summary>
        /// 删除报表 甘肃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDeletedGS_Click(object sender, RoutedEventArgs e)
        {
            DeletdGS();
        }

        private void miReportPrintGSHZ_Click(object sender, RoutedEventArgs e)
        {
            PrintGSHZ();
        }

        private void btnPrintHZ_Click(object sender, RoutedEventArgs e)
        {
            PrintGSHZ();
        }

        private void ComboBoxCheckUnitName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                SearchReport();
            else if (Global.EachDistrict.Equals("GS"))
                SearchReportGS();
        }

        private void ComboBoxCheckedCompanyName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Global.EachDistrict.Equals(string.Empty))
                SearchReport();
            else if (Global.EachDistrict.Equals("GS"))
                SearchReportGS();
        }

    }
}
