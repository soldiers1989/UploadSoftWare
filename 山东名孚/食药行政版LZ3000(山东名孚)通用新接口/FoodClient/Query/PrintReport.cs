using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using DY.FoodClientLib.Model;
using DY.FoodClientLib;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FoodClient.Query
{
    public partial class PrintReport : Form
    {
        public PrintReport()
        {
            InitializeComponent();
        }

        public DataTable _dataTable = new DataTable();
        private IList<clsReport> _reportList = null;
        private IList<clsReportDetail> _reportDetailList = null;

        private void PrintReport_Load(object sender, System.EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SetValues();
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(print);
            timer1.Start();
        }

        private void print(object sender, EventArgs e)
        {
            timer1.Stop();
            CaptureScreen();
            printPreviewDialog1.TopLevel = true;
            printPreviewDialog1.Show();
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void SetValues()
        {
            if (_dataTable.Rows.Count > 0)
            {
                _reportList = (List<clsReport>)StringUtil.DataTableToIList<clsReport>(_dataTable, 1);
                if (_reportList != null & _reportList.Count > 0)
                {
                    this.labelReportName.Text = _reportList[0].ReportName;
                    this.labelSampleNum.Text = "样品编号：" + _reportList[0].SamplingCode;
                    this.labelCheckedCompany.Text = _reportList[0].CheckedCompany;
                    this.labelAddress.Text = _reportList[0].Address;
                    this.labelZipCode.Text = _reportList[0].ZipCode;
                    this.labelZipCode.Text = _reportList[0].ZipCode;
                    this.labelBusinessLicense.Text = _reportList[0].BusinessLicense;
                    this.labelContactPhone.Text = _reportList[0].ContactPhone;
                    this.labelContact.Text = _reportList[0].Contact;
                    this.labelFax.Text = _reportList[0].Fax;
                    this.labelProductName.Text = _reportList[0].ProductName;
                    this.labelProductPrice.Text = _reportList[0].ProductPrice;
                    this.labelSpecifications.Text = _reportList[0].Specifications;
                    this.labelQualityGrade.Text = _reportList[0].QualityGrade;
                    this.labelBatchNumber.Text = _reportList[0].BatchNumber;
                    this.labelRegisteredTrademark.Text = _reportList[0].RegisteredTrademark;
                    this.labelSamplingNumber.Text = _reportList[0].SamplingNumber;
                    this.labelSamplingBase.Text = _reportList[0].SamplingBase;
                    this.labelIntoNumber.Text = _reportList[0].IntoNumber;
                    this.labelInventoryNubmer.Text = _reportList[0].InventoryNubmer;
                    this.labelNotes.Text = _reportList[0].Notes;
                    this.labelSamplingData.Text = _reportList[0].SamplingData;
                    this.labelSamplingPerson.Text = "抽样人：" + _reportList[0].SamplingPerson;
                    this.labelApprovedUser.Text = "核准人：" + _reportList[0].ApprovedUser;

                    DataTable dataTable = new clsResultOpr().SearchReportDetail("'" + _reportList[0].SysCode + "'");
                    if (dataTable.Rows.Count > 0)
                    {
                        this.dataGridView.DataSource = null;
                        _reportDetailList = (List<clsReportDetail>)StringUtil.DataTableToIList<clsReportDetail>(dataTable, 1);
                        if (_reportDetailList != null && _reportDetailList.Count > 0)
                        {
                            for (int i = 0; i < _reportDetailList.Count; i++)
                            {
                                if (_reportDetailList[i].Conclusion.Equals("不合格"))
                                {
                                    this.labelConclusion.Text = "结论：不合格：";
                                    break;
                                }
                            }
                            this.dataGridView.DataSource = new SortableBindingList<clsReportDetail>(_reportDetailList);
                            dataGridView.ClearSelection();
                        }
                    }
                }
            }
        }

        [DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void CaptureScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
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
                case WM_NCHITTEST:

                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() == HTCLIENT)
                    {
                        m.Result = new IntPtr(HTCAPTION);
                        return;
                    }
                    break;
                case WM_NCLBUTTONDBLCLK:
                    Console.WriteLine(this.WindowState);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

    }
}