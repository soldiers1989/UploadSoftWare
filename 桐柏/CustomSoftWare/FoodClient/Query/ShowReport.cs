using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DY.FoodClientLib;
using DY.FoodClientLib.Model;

namespace FoodClient.Query
{
    public partial class ShowReport : Form
    {
        public ShowReport()
        {
            InitializeComponent();
        }
        private string _strType = System.Configuration.ConfigurationManager.AppSettings["ReportsType"].ToString();
        public DataTable _dataTable = new DataTable();
        private clsResultOpr _clsResultOpr = new clsResultOpr();
        private IList<clsReportDetail> _reportDetailList = null;
        private IList<clsReport> _reportList = null;
        private IList<clsReportSZ> _reportListSZ = null;
        private IList<clsReportDetailSZ> _reportDetailListSZ = null;
        private string _sysCode = "", _reportID_mzyj = "";
        private int _reportID_sz = 0;

        private void ShowReport_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            if (_strType.Equals("MZYJ"))
            {
                gb_MZYJ.Visible = gb_dgv_MZYJ.Visible = true;
                SetValues_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                gb_SZ.Visible = true;
                SetValues_SZ();
            }
        }

        /// <summary>
        /// 初始化数据 - 深圳
        /// </summary>
        private void SetValues_SZ()
        {
            try
            {
                if (_dataTable.Rows.Count > 0)
                {
                    _reportListSZ = (List<clsReportSZ>)StringUtil.DataTableToIList<clsReportSZ>(_dataTable, 1);
                    if (_reportListSZ.Count > 0)
                    {
                        _reportID_sz = _reportListSZ[0].ID;
                        this.tb_ReportName.Text = _reportListSZ[0].ReportName;
                        this.tbSZ_CheckedCompany.Text = _reportListSZ[0].CheckedCompany;
                        this.tbSZ_CheckedCompanyArea.Text = _reportListSZ[0].CheckedCompanyArea;
                        this.tbSZ_CheckUser.Text = _reportListSZ[0].CheckUser;
                        this.tbSZ_Contact.Text = _reportListSZ[0].Contact;
                        this.tbSZ_ContactPhone.Text = _reportListSZ[0].ContactPhone;
                        this.tbSZ_SamplingData.Text = _reportListSZ[0].SamplingData;
                        this.tbSZ_SamplingPerson.Text = _reportListSZ[0].SamplingPerson;
                    }

                    DataTable dt = _clsResultOpr.SearchReportDetail(_reportListSZ[0].ID.ToString(), "SZ");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.dataGridView_SZ.DataSource = null;
                        _reportDetailListSZ = (List<clsReportDetailSZ>)StringUtil.DataTableToIList<clsReportDetailSZ>(dt, 1);
                        if (_reportDetailListSZ != null && _reportDetailListSZ.Count > 0)
                        {
                            this.dataGridView_SZ.DataSource = new SortableBindingList<clsReportDetailSZ>(_reportDetailListSZ);
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
        /// 初始化数据 - 每周一检
        /// </summary>
        private void SetValues_MZYJ()
        {
            try
            {
                if (_dataTable.Rows.Count > 0)
                {
                    _reportList = (List<clsReport>)StringUtil.DataTableToIList<clsReport>(_dataTable, 1);
                    if (_reportList.Count > 0)
                    {
                        _reportID_mzyj = _reportList[0].ID;
                        this.labelSysCode.Text = "样品编号:" + _reportList[0].SamplingCode;
                        this.tb_ReportName.Text = _reportList[0].ReportName;
                        this.textBoxCheckedCompany.Text = _reportList[0].CheckedCompany;
                        this.textBoxAddress.Text = _reportList[0].Address;
                        this.textBoxZipCode.Text = _reportList[0].ZipCode;
                        this.textBoxBusinessLicense.Text = _reportList[0].BusinessLicense;
                        this.textBoxBusinessNature.Text = _reportList[0].BusinessNature;
                        this.textBoxContact.Text = _reportList[0].Contact;
                        this.textBoxContactPhone.Text = _reportList[0].ContactPhone;
                        this.textBoxFax.Text = _reportList[0].Fax;
                        this.textBoxProductName.Text = _reportList[0].ProductName;
                        this.textBoxProductPrice.Text = _reportList[0].ProductPrice;
                        this.textBoxSpecifications.Text = _reportList[0].Specifications;
                        this.textBoxQualityGrade.Text = _reportList[0].QualityGrade;
                        this.textBoxBatchNumber.Text = _reportList[0].BatchNumber;
                        this.textBoxRegisteredTrademark.Text = _reportList[0].RegisteredTrademark;
                        this.textBoxSamplingNumber.Text = _reportList[0].SamplingNumber;
                        this.textBoxSamplingBase.Text = _reportList[0].SamplingBase;
                        this.textBoxIntoNumber.Text = _reportList[0].IntoNumber;
                        this.textBoxInventoryNubmer.Text = _reportList[0].InventoryNubmer;
                        this.textBoxNotes.Text = _reportList[0].Notes;
                        this.textBoxSamplingData.Text = _reportList[0].SamplingData;
                        this.textBoxSamplingPerson.Text = _reportList[0].SamplingPerson;
                        this.textBoxApprovedUser.Text = _reportList[0].ApprovedUser;
                        this.textBoxConclusion.Text = "合格";
                    }

                    DataTable dt = _clsResultOpr.SearchReportDetail("'" + _reportList[0].ID + "'", "MZYJ");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.dataGridView.DataSource = null;
                        _reportDetailList = (List<clsReportDetail>)StringUtil.DataTableToIList<clsReportDetail>(dt, 1);
                        if (_reportDetailList != null && _reportDetailList.Count > 0)
                        {
                            for (int i = 0; i < _reportDetailList.Count; i++)
                            {
                                if (_reportDetailList[i].Conclusion.Equals("不合格"))
                                {
                                    this.textBoxConclusion.Text = "不合格";
                                    break;
                                }
                            }
                            this.dataGridView.DataSource = new SortableBindingList<clsReportDetail>(_reportDetailList);
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_strType.Equals("MZYJ"))
            {
                Save_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                Save_SZ();
            }
        }

        /// <summary>
        /// 保存报表信息 - 深圳
        /// </summary>
        private void Save_SZ() 
        {
            string sErr = "";
            int rtn = 0;
            try
            {
                clsReportSZ model = new clsReportSZ();
                model.ReportName = this.tb_ReportName.Text.Trim();
                model.CheckedCompany = this.tbSZ_CheckedCompany.Text.Trim();
                model.Contact = this.tbSZ_Contact.Text.Trim();
                model.ContactPhone = this.tbSZ_ContactPhone.Text.Trim();
                model.CheckedCompanyArea = this.tbSZ_CheckedCompanyArea.Text.Trim();
                model.SamplingData = this.tbSZ_SamplingData.Text.Trim();
                model.SamplingPerson = this.tbSZ_SamplingPerson.Text.Trim();
                model.CheckUser = this.tbSZ_CheckUser.Text.Trim();
                model.ID = _reportID_sz;
                rtn = _clsResultOpr.UpdateReport(model, out sErr);
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }
            finally
            {
                if (sErr.Equals("") && rtn == 1)
                {
                    SearchReport_MZYJ();
                    MessageBox.Show("保存成功！", "操作提示");
                }
                else
                {
                    MessageBox.Show("保存出现异常！\n错误信息：" + sErr);
                }
            }
        }

        /// <summary>
        /// 保存报表信息 - 每周一检
        /// </summary>
        private void Save_MZYJ() 
        {
            string sErr = "";
            int rtn = 0;
            try
            {
                clsReport model = new clsReport();
                model.ReportName = this.tb_ReportName.Text.Trim();
                model.Address = this.textBoxAddress.Text.Trim();
                model.ZipCode = this.textBoxZipCode.Text.Trim();
                model.BusinessLicense = this.textBoxBusinessLicense.Text.Trim();
                model.BusinessNature = this.textBoxBusinessNature.Text.Trim();
                model.Contact = this.textBoxContact.Text.Trim();
                model.ContactPhone = this.textBoxContactPhone.Text.Trim();
                model.Fax = this.textBoxFax.Text.Trim();
                model.ProductName = this.textBoxProductName.Text.Trim();
                model.ProductPrice = this.textBoxProductPrice.Text.Trim();
                model.Specifications = this.textBoxSpecifications.Text.Trim();
                model.QualityGrade = this.textBoxQualityGrade.Text.Trim();
                model.BatchNumber = this.textBoxBatchNumber.Text.Trim();
                model.RegisteredTrademark = this.textBoxRegisteredTrademark.Text.Trim();
                model.SamplingNumber = this.textBoxSamplingNumber.Text.Trim();
                model.SamplingBase = this.textBoxSamplingBase.Text.Trim();
                model.IntoNumber = this.textBoxIntoNumber.Text.Trim();
                model.InventoryNubmer = this.textBoxInventoryNubmer.Text.Trim();
                model.Notes = this.textBoxNotes.Text.Trim();
                model.SamplingPerson = this.textBoxSamplingPerson.Text.Trim();
                model.ApprovedUser = this.textBoxApprovedUser.Text.Trim();
                model.ID = _reportID_mzyj;
                rtn = _clsResultOpr.UpdateReport(model, out sErr);
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }
            finally
            {
                if (sErr.Equals("") && rtn == 1)
                {
                    SearchReport_MZYJ();
                    MessageBox.Show("保存成功！", "操作提示");
                }
                else
                {
                    MessageBox.Show("保存出现异常！\n错误信息：" + sErr);
                }
            }
        }

        private void SearchReport_MZYJ()
        {
            _dataTable = _clsResultOpr.SearchReport_MZYJ(" SysCode = '" + _sysCode + "'");
            if (_dataTable != null && _dataTable.Rows.Count > 0)
            {
                SetValues_MZYJ();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
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

        private void dataGridView_SZ_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

    }
}
