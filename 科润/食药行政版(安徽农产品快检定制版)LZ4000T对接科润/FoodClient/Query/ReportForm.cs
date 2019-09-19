using System;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;
using System.Collections.Generic;
using DY.FoodClientLib.Model;
using System.ComponentModel;
using System.IO;

namespace FoodClient.Query
{
    // frmTimers
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 报表类型 SZ 深圳；MZYJ 每周一检；
        /// </summary>
        private string _strType = System.Configuration.ConfigurationManager.AppSettings["ReportsType"].ToString();
        clsResultOpr _clsResultOpr = new clsResultOpr();
        DataTable _dtb = new DataTable();

        private void ReportForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            Search();
        }

        /// <summary>
        /// 查询报表
        /// </summary>
        private void Search()
        {
            if (_strType.Equals("MZYJ") || _strType.Equals("MZYJ_N"))
            {
                this.dataGridView_MZYJ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_MZYJ.Visible = true;
                Search_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                this.dataGridView_SZ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_SZ.Visible = true;
                Search_SZ();
            }
        }

        /// <summary>
        /// 查询报表 - 深圳
        /// </summary>
        private void Search_SZ()
        {
            try
            {
                DataTable dataTable = new DataTable();
                _dtb = dataTable = _clsResultOpr.SearchReport_SZ("");
                if (dataTable.Rows.Count > 0)
                {
                    IList<clsReportSZ> reportList = (List<clsReportSZ>)StringUtil.DataTableToIList<clsReportSZ>(dataTable, 1);
                    BindingList<clsReportSZ> _BindingList = new BindingList<clsReportSZ>(reportList);
                    this.dataGridView_SZ.DataSource = _BindingList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 查询报表 - 每周一检
        /// </summary>
        private void Search_MZYJ()
        {
            try
            {
                DataTable dataTable = new DataTable();
                _dtb = dataTable = _clsResultOpr.SearchReport_MZYJ("");
                if (dataTable.Rows.Count > 0)
                {
                    IList<clsReport> reportList = (List<clsReport>)StringUtil.DataTableToIList<clsReport>(dataTable, 1);
                    BindingList<clsReport> _BindingList = new BindingList<clsReport>(reportList);
                    this.dataGridView_MZYJ.DataSource = _BindingList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btnToView_Click(object sender, EventArgs e)
        {
            if (dataGridView_MZYJ.SelectedRows.Count <= 0 && dataGridView_SZ.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            if (_strType.Equals("MZYJ"))
            {
                EditReport_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                EditReport_SZ();
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_strType.Equals("MZYJ"))
            {
                EditReport_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                EditReport_SZ();
            }
        }

        /// <summary>
        /// 查看|修改报表 - 深圳
        /// </summary>
        private void EditReport_SZ() 
        {
            try
            {
                if (dataGridView_SZ.DataSource != null)
                {
                    string ID = this.dataGridView_SZ.CurrentRow.Cells["ID_SZ"].Value.ToString();
                    if (!ID.Equals("") && ID != null)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = _clsResultOpr.SearchReport_SZ(" ID = " + ID);
                        if (dataTable.Rows.Count > 0)
                        {
                            ShowReport report = new ShowReport();
                            report._dataTable = dataTable;
                            report.ShowDialog(this);
                            Search_SZ();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 查看|修改报表 - 每周一检
        /// </summary>
        private void EditReport_MZYJ()
        {
            try
            {
                if (dataGridView_MZYJ.DataSource != null)
                {
                    string ID = this.dataGridView_MZYJ.CurrentRow.Cells["ID"].Value.ToString();
                    if (!ID.Equals("") && ID != null)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = _clsResultOpr.SearchReport_MZYJ(" ID = '" + ID + "'");
                        if (dataTable.Rows.Count > 0)
                        {
                            ShowReport report = new ShowReport();
                            report._dataTable = dataTable;
                            report.ShowDialog(this);
                            Search_MZYJ();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x01;
            const int HTCAPTION = 0x02;
            const int WM_NCLBUTTONDBLCLK = 0xA3;
            switch (m.Msg)
            {
                case 0x4e:
                case 0xd:
                case 0xe:
                case 0x14:
                    base.WndProc(ref m);
                    break;
                case WM_NCHITTEST://鼠标点任意位置后可以拖动窗体

                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() == HTCLIENT)
                    {
                        m.Result = new IntPtr(HTCAPTION);
                        return;
                    }
                    break;
                case WM_NCLBUTTONDBLCLK://禁止双击最大化
                    Console.WriteLine(this.WindowState);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        /// <summary>
        /// 报表打印
        /// </summary>
        private void Print() 
        {
            if (dataGridView_MZYJ.SelectedRows.Count <= 0 && dataGridView_SZ.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            if (_strType.Equals("MZYJ_N"))
            {
                Print_MZYJ_N();
            }
            else if (_strType.Equals("SZ"))
            {
                Print_SZ();
            }
            else if (_strType.Equals("MZYJ"))
            {
                Print_MZYJ();
            }
        }

        /// <summary>
        /// 打印 - 每周一检 
        /// 单一检测项目
        /// </summary>
        private void Print_MZYJ()
        {
            try
            {
                if (dataGridView_MZYJ.DataSource != null)
                {
                    string ID = this.dataGridView_MZYJ.CurrentRow.Cells["ID"].Value.ToString();
                    if (!ID.Equals("") && ID != null)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = _clsResultOpr.SearchReport_MZYJ(" ID = '" + ID + "'");
                        if (dataTable.Rows.Count > 0)
                        {
                            PrintReportForm print = new PrintReportForm();
                            print._dataTable = dataTable;
                            print.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 打印 - 每周一检 多检测项目
        /// </summary>
        private void Print_MZYJ_N() 
        {
            try
            {
                if (dataGridView_MZYJ.DataSource != null)
                {
                    string ID = this.dataGridView_MZYJ.CurrentRow.Cells["ID"].Value.ToString();
                    if (!ID.Equals("") && ID != null)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = _clsResultOpr.SearchReport_MZYJ(" ID = '" + ID + "'");
                        if (dataTable.Rows.Count > 0)
                        {
                            PrintReportForm print = new PrintReportForm();
                            print._dataTable = dataTable;
                            print.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 打印 - 深圳
        /// </summary>
        private void Print_SZ()
        {
            try
            {
                if (dataGridView_SZ.DataSource != null)
                {
                    string ID = this.dataGridView_SZ.CurrentRow.Cells["ID_SZ"].Value.ToString();
                    if (!ID.Equals("") && ID != null)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = _clsResultOpr.SearchReport_SZ(" ID = " + ID);
                        if (dataTable.Rows.Count > 0)
                        {
                            PrintReportForm print = new PrintReportForm();
                            print._dataTable = dataTable;
                            print.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted_Click(object sender, EventArgs e)
        {
            if (dataGridView_MZYJ.SelectedRows.Count <= 0 && dataGridView_SZ.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            if (MessageBox.Show("确定要删除所选择的报表吗", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_strType.Equals("MZYJ"))
                {
                    Deleted_MZYJ(_strType);
                }
                else if (_strType.Equals("SZ"))
                {
                    Deleted_SZ(_strType);
                }
            }
        }

        /// <summary>
        /// 删除报表 - 深圳
        /// </summary>
        private void Deleted_SZ(string type) 
        {
            if (dataGridView_SZ.DataSource != null)
            {
                int selectNum = this.dataGridView_SZ.SelectedRows.Count;
                if (selectNum > 0)
                {
                    int delNum = 0;
                    string sErr = string.Empty;
                    try
                    {
                        for (int i = 0; i < selectNum; i++)
                        {
                            sErr = string.Empty;
                            string ID = this.dataGridView_SZ.SelectedRows[i].Cells["ID_SZ"].Value.ToString();
                            //删除报表信息
                            _clsResultOpr.DeletedReportByID(ID, out sErr, type);
                            if (sErr.Equals(string.Empty))
                            {
                                DataTable dataTable = null;
                                dataTable = _clsResultOpr.SearchReportDetail(ID, type);
                                //根据ID删除报表关联的子表样品信息
                                _clsResultOpr.DeletedReportDetailByID(ID, type, out sErr);
                                //修改子表样品关联的检测记录信息为未生成报表状态
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    IList<clsReportDetailSZ> reportDetailList = null;
                                    reportDetailList = (List<clsReportDetailSZ>)StringUtil.DataTableToIList<clsReportDetailSZ>(dataTable, 1);
                                    if (reportDetailList != null && reportDetailList.Count > 0)
                                    {
                                        for (int j = 0; j < reportDetailList.Count; j++)
                                        {
                                            clsResult result = new clsResult();
                                            result.SysCode = reportDetailList[j].SysCode;
                                            result.IsReport = "N";
                                            _clsResultOpr.UpdatePartReport(result, out sErr);
                                        }
                                    }
                                }
                            }
                            if (sErr.Equals(string.Empty))
                                delNum += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("异常!\n" + ex.Message, "异常信息");
                    }
                    finally
                    {
                        if (delNum > 0)
                        {
                            sErr = selectNum == delNum ? "成功删除 " + delNum + " 条报表数据!" : "删除报表时出现了小问题,但依然删除了 " + delNum + " 条数据!";
                            MessageBox.Show(sErr, "操作提示");
                        }
                        else
                        {
                            MessageBox.Show("删除报表失败!", "操作提示");
                        }
                        Search_SZ();
                    }
                }
            }
        }

        /// <summary>
        /// 删除报表 - 每周一检
        /// </summary>
        private void Deleted_MZYJ(string type)
        {
            if (dataGridView_MZYJ.DataSource != null)
            {
                int selectNum = this.dataGridView_MZYJ.SelectedRows.Count;
                if (selectNum > 0)
                {
                    int delNum = 0;
                    string sErr = string.Empty;
                    try
                    {
                        for (int i = 0; i < selectNum; i++)
                        {
                            sErr = string.Empty;
                            string ID = this.dataGridView_MZYJ.SelectedRows[i].Cells["ID"].Value.ToString();
                            //删除报表信息
                            _clsResultOpr.DeletedReportByID(ID, out sErr, type);
                            if (sErr.Equals(string.Empty))
                            {
                                DataTable dataTable = null;
                                dataTable = _clsResultOpr.SearchReportDetail("'" + ID + "'", type);
                                //根据ID删除报表关联的子表样品信息
                                _clsResultOpr.DeletedReportDetailByID(ID, type, out sErr);
                                //修改子表样品关联的检测记录信息为未生成报表状态
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    IList<clsReportDetail> reportDetailList = null;
                                    reportDetailList = (List<clsReportDetail>)StringUtil.DataTableToIList<clsReportDetail>(dataTable, 1);
                                    if (reportDetailList != null && reportDetailList.Count > 0)
                                    {
                                        for (int j = 0; j < reportDetailList.Count; j++)
                                        {
                                            clsResult result = new clsResult();
                                            result.SysCode = reportDetailList[j].SysCode;
                                            result.IsReport = "N";
                                            _clsResultOpr.UpdatePartReport(result, out sErr);
                                        }
                                    }
                                }
                            }
                            if (sErr.Equals(string.Empty))
                                delNum += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("异常!\n" + ex.Message, "异常信息");
                    }
                    finally
                    {
                        if (delNum > 0)
                        {
                            sErr = selectNum == delNum ? "成功删除 " + delNum + " 条报表数据!" : "删除报表时出现了小问题,但依然删除了 " + delNum + " 条数据!";
                            MessageBox.Show(sErr, "操作提示");
                        }
                        else
                        {
                            MessageBox.Show("删除报表失败!", "操作提示");
                        }
                        Search_MZYJ();
                    }
                }
            }
        }

        private void dataGridView_SZ_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView_SZ_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_strType.Equals("MZYJ"))
            {
                EditReport_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                EditReport_SZ();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportReport();
        }

        private void ExportReport()
        {
            if (dataGridView_MZYJ.SelectedRows.Count <= 0 && dataGridView_SZ.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            if (_strType.Equals("MZYJ"))
            {
                //Deleted_MZYJ(_strType);
            }
            else if (_strType.Equals("SZ"))
            {
                ExportReport_SZ(_strType);
            }
        }

        /// <summary>
        /// 导出报表 - 深圳
        /// </summary>
        /// <param name="type"></param>
        private void ExportReport_SZ(string type) 
        {
            if (dataGridView_SZ.DataSource != null)
            {
                int selectNum = this.dataGridView_SZ.SelectedRows.Count;
                if (selectNum > 0)
                {
                    IList<clsReportSZ.ExportReport> exprotList = new List<clsReportSZ.ExportReport>();
                    int Number = 0;
                    try
                    {
                        for (int i = 0; i < selectNum; i++)
                        {
                            string ID = this.dataGridView_SZ.SelectedRows[i].Cells["ID_SZ"].Value.ToString();
                            string CheckedCompany = this.dataGridView_SZ.SelectedRows[i].Cells["CheckedCompany_SZ"].Value.ToString();
                            DataTable dt = null;
                            dt = _clsResultOpr.SearchReportDetail(ID, type);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                IList<clsReportSZ.clsReportDetailSZ> reportDetailSZList =
                                    (List<clsReportSZ.clsReportDetailSZ>)StringUtil.DataTableToIList<clsReportSZ.clsReportDetailSZ>(dt, 1);
                                for (int k = 0; k < reportDetailSZList.Count; k++)
                                {
                                    Number++;
                                    clsReportSZ.ExportReport exportReport = new clsReportSZ.ExportReport();
                                    exportReport.Number = Number.ToString();
                                    exportReport.SysCode = reportDetailSZList[k].SysCode == null ? "" : reportDetailSZList[k].SysCode;
                                    exportReport.CheckedCompany = CheckedCompany == null ? "" : CheckedCompany;
                                    exportReport.SampleName = reportDetailSZList[k].SampleName == null ? "" : reportDetailSZList[k].SampleName;
                                    exportReport.SampleBase = reportDetailSZList[k].SampleBase == null ? "" : reportDetailSZList[k].SampleBase;
                                    exportReport.SampleSource = reportDetailSZList[k].SampleSource == null ? "" : reportDetailSZList[k].SampleSource;
                                    exportReport.IsDestruction = reportDetailSZList[k].IsDestruction == null ? "" : reportDetailSZList[k].IsDestruction;
                                    exportReport.DestructionKG = reportDetailSZList[k].DestructionKG == null ? "" : reportDetailSZList[k].DestructionKG;
                                    exportReport.SampleAmount = reportDetailSZList[k].SampleAmount == null ? "" : reportDetailSZList[k].SampleAmount;
                                    exportReport.SampleNumber = reportDetailSZList[k].SampleNumber == null ? "" : reportDetailSZList[k].SampleNumber;
                                    exportReport.Price = reportDetailSZList[k].Price == null ? "" : reportDetailSZList[k].Price;
                                    exportReport.ChekcedValue = reportDetailSZList[k].ChekcedValue == null ? "" : reportDetailSZList[k].ChekcedValue;
                                    exportReport.StandardValue = reportDetailSZList[k].StandardValue == null ? "" : reportDetailSZList[k].StandardValue;
                                    exportReport.Result = reportDetailSZList[k].Result == null ? "" : reportDetailSZList[k].Result;
                                    exportReport.Notes = reportDetailSZList[k].Note == null ? "" : reportDetailSZList[k].Note;
                                    exprotList.Add(exportReport);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("异常!\n" + ex.Message, "异常信息");
                    }
                    finally
                    {
                        ExprotForm window = new ExprotForm();
                        window._exprotList = exprotList;
                        window.Show();
                    }
                }
            }
        }

    }
}
