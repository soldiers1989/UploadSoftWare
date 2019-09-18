using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DY.FoodClientLib;
using DY.FoodClientLib.Model;
using System.Globalization;

namespace FoodClient.Query
{
    public partial class PrintReportForm : Form
    {
        public PrintReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 报表类型 SZ 深圳；MZYJ 每周一检；
        /// </summary>
        private string _strType = System.Configuration.ConfigurationManager.AppSettings["ReportsType"].ToString();
        public DataTable _dataTable = new DataTable();
        private IList<clsReport> _reportListMZYJ = null;
        private IList<clsReportDetail> _reportDetailListMZYJ = null;
        private IList<clsReportSZ> _reportListSZ = null;
        private IList<clsReportDetailSZ> _reportDetailListSZ = null;
        private string _Conclusion = "合格";
        private clsResultOpr _clsROBLL = new clsResultOpr();

        private void PrintReportForm_Load(object sender, EventArgs e)
        {
            if (_strType.Equals("SZ"))
                dataGridView_SZ.Visible = true;
            else if (_strType.Equals("MZYJ") || _strType.Equals("MZYJ_N"))
                dataGridView_MZYJ.Visible = true;
            ChangeRows();
            PrintPreview();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void PrintPreview() 
        {
            myp.PageUnits = VBprinter.VB2008Print.PageExportUnit.CentiMeter;
            myp.IsImmediatePrintNotPreview = false;
            myp.IsUseDGVPadding = false;
            myp.CellMargin = new System.Drawing.Printing.Margins(10, 20, 10, 10);
            myp.InvalidatePreview();
            tabControl1.SelectedIndex = 1;
        }

        private void myp_PrintDocument()
        {
            if (_strType.Equals("MZYJ_N"))
            {
                PrintDocument_MZYJ_N();
            }
            else if (_strType.Equals("SZ"))
            {
                PrintDocument_SZ();
            }
            else if (_strType.Equals("MZYJ"))
            {
                PrintDocument_MZYJ();
            }
        }

        /// <summary>
        /// 每周一检报表打印预览 
        /// 单一检测项目
        /// </summary>
        private void PrintDocument_MZYJ()
        {
            DataGridView newdgv = new DataGridView();
            try
            {
                myp.CopyDataGridView(dataGridView_MZYJ, newdgv, false);
                newdgv.GridColor = Color.Black;
                //打印处理过程
                myp.IsNeedCheckNewPage = false;
                System.Drawing.Printing.Margins mymargin;
                mymargin = new System.Drawing.Printing.Margins();
                mymargin.Left = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Right = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Top = Convert.ToInt32(myp.ConvertInchToCm(80));
                mymargin.Bottom = Convert.ToInt32(myp.ConvertInchToCm(45));
                myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                mymargin.Top = 20;
                //先打印表头内容
                Font Font1 = new Font("黑体", 18, FontStyle.Bold);
                Font Font2 = new Font("黑体", 14, FontStyle.Bold);
                Font Font3 = new Font("宋体", 12);
                Font Font4 = new Font("华文行楷", 12);
                string str = _reportListMZYJ[0].ReportName;
                myp.DrawTitle("", Font1, Color.Black, 0);
                myp.NewRow(60);
                float rowheight = 93;
                float centerx = (myp.PaperPrintWidth - 1800) / 2;
                myp.Currentx = centerx;

                //编号
                myp.NewRow(rowheight);
                str = _reportListMZYJ[0].SamplingCode;
                str = (str.Equals("") ? "/" : str);
                myp.DrawText("", 1410, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "" : str, 390, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);


                //受检人
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].CheckedCompany;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 1550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //经营地址
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].Address;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 1550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //经营性质
                float width = 0;
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].BusinessNature;
                if (str.Equals("国有"))
                    width = 360;
                else if (str.Equals("集体"))
                    width = 560;
                else if (str.Equals("私营"))
                    width = 785;
                else if (str.Equals("外商投资"))
                    width = 1135;
                else if (str.Equals("个体户"))
                    width = 1385;
                else if (str.Equals("其他"))
                    width = 1585;
                else
                    width = 1585;
                myp.DrawText("", width, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText("√", 1800 - width, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //营业执照 | 邮政编号
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].BusinessLicense;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 1150, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].ZipCode;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //联系人 | 电  话 | 传  真
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].Contact;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].ContactPhone;
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].Fax;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);


                //商品名称 | 商品售价 | 规格型号
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].ProductName;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].ProductPrice;
                if (!_reportListMZYJ[0].Unit.Equals(""))
                    str = str.Equals("") ? "" : (str + "/" + _reportListMZYJ[0].Unit);
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].Specifications;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //质量等级 | 批号 | 注册商标
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].QualityGrade;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].BatchNumber;
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].RegisteredTrademark;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //抽样数量 | 抽样基数 | 进货数量
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].SamplingNumber;
                if (!_reportListMZYJ[0].Unit.Equals(""))
                    str = str.Equals("") ? "" : str + _reportListMZYJ[0].Unit;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].SamplingBase;
                if (!_reportListMZYJ[0].Unit.Equals(""))
                    str = str.Equals("") ? "" : str + _reportListMZYJ[0].Unit;
                myp.DrawText(str.Equals("") ? "/" : str, 575, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].IntoNumber;
                if (!_reportListMZYJ[0].Unit.Equals(""))
                    str = str.Equals("") ? "" : str + _reportListMZYJ[0].Unit;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //商品进货验收制度执行情况 | 库存数量
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].Implementation;
                myp.DrawText("", 550, rowheight * 2, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 850, rowheight * 2, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].InventoryNubmer;
                if (!_reportListMZYJ[0].Unit.Equals(""))
                    str = str.Equals("") ? "" : str + _reportListMZYJ[0].Unit;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight * 2, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                //备注 | 抽样日期
                myp.NewRow(rowheight * 2);
                myp.Currentx = centerx;
                str = _reportListMZYJ[0].Notes;
                myp.DrawText("", 250, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                myp.DrawText(str.Equals("") ? "/" : str, 1150, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                str = _reportListMZYJ[0].SamplingData;
                myp.DrawText(str.Equals("") ? "/" : str, 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                int rowCount = _reportDetailListMZYJ.Count;
                if (rowCount > 0)
                {
                    //检测项目 | 检测依据
                    myp.NewRow(600);
                    myp.Currentx = centerx;
                    str = _reportDetailListMZYJ[0].CheckItem;
                    myp.DrawText("", 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    myp.DrawText(str.Equals("") ? "/" : str, 850, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    str = _reportDetailListMZYJ[0].CheckBasis;
                    myp.DrawText(str.Equals("") ? "/" : str, 550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                    //标准值 | 实测值
                    myp.NewRow(rowheight);
                    myp.Currentx = centerx;
                    str = _reportDetailListMZYJ[0].StandardValues;
                    if (!_reportDetailListMZYJ[0].Unit.Equals(""))
                        str = str.Equals("") ? "" : (str + _reportDetailListMZYJ[0].Unit);
                    myp.DrawText("", 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    myp.DrawText(str.Equals("") ? "/" : str, 850, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    str = _reportDetailListMZYJ[0].CheckValues;
                    if (!_reportDetailListMZYJ[0].Unit.Equals(""))
                        str = str.Equals("") ? "" : (str + _reportDetailListMZYJ[0].Unit);
                    myp.DrawText(str.Equals("") ? "/" : str, 550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                    //样品编号 | 结论
                    myp.NewRow(rowheight);
                    myp.Currentx = centerx;
                    str = _reportListMZYJ[0].SamplingCode;
                    myp.DrawText("", 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    myp.DrawText(str.Equals("") ? "/" : str, 850, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    str = _reportDetailListMZYJ[0].Conclusion;
                    myp.DrawText(str.Equals("") ? "/" : str, 550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);

                    //备注 | 检测日期
                    myp.NewRow(rowheight);
                    myp.Currentx = centerx;
                    str = _reportListMZYJ[0].Notes;
                    myp.DrawText("", 400, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    myp.DrawText(str.Equals("") ? "/" : str, 850, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    DateTime dt = new DateTime();//时间只格式 yyyy/MM/dd
                    dt = Convert.ToDateTime(_reportDetailListMZYJ[0].CheckData);
                    str = dt.ToShortDateString().ToString();
                    myp.DrawText(str.Equals("") ? "/" : str, 550, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);


                    //检测人 | 核准人
                    myp.NewRow(rowheight);
                    myp.Currentx = centerx;
                    str = _reportDetailListMZYJ[0].CheckUser;
                    myp.DrawText("", 200, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    myp.DrawText(str.Equals("") ? "/" : str, 950, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                    str = _reportListMZYJ[0].ApprovedUser;
                    myp.DrawText(str.Equals("") ? "/" : str, 650, rowheight, Font3, StringAlignment.Near, StringAlignment.Center, false, true, false, false, 6, 0);
                }
                newdgv.Dispose();
                newdgv = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 深圳报表打印预览
        /// </summary>
        private void PrintDocument_SZ()
        {
            DataGridView newdgv = new DataGridView();
            try
            {
                myp.CopyDataGridView(dataGridView_SZ, newdgv, false);
                newdgv.GridColor = Color.Black;
                //打印处理过程
                myp.IsNeedCheckNewPage = false;
                System.Drawing.Printing.Margins mymargin;
                mymargin = new System.Drawing.Printing.Margins();
                mymargin.Left = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Right = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Top = Convert.ToInt32(myp.ConvertInchToCm(80));
                mymargin.Bottom = Convert.ToInt32(myp.ConvertInchToCm(45));
                myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                mymargin.Top = 20;
                //先打印表头内容
                Font Font1 = new Font("黑体", 18, FontStyle.Bold);
                Font Font2 = new Font("黑体", 14, FontStyle.Bold);
                Font Font3 = new Font("宋体", 12);
                Font Font4 = new Font("华文行楷", 12);
                Font Font5 = new Font("宋体", 9);
                Font Font6 = new Font("黑体", 12, FontStyle.Underline);
                string str = _reportListSZ[0].ReportName, sTr = "";
                if (str.Equals("深圳市食品药品监督管理局食用农产品质量安全快速筛查抽样检测单"))
                {
                    str = "深圳市食品药品监督管理局食用农产\n品质量安全\n快速筛查抽样检测单";
                }
                myp.DrawTitle(str, Font1, Color.Black, 0);
                myp.NewRow(10);
                float rowheight = 90;
                float centerx = (myp.PaperPrintWidth - 1880) / 2;
                myp.Currentx = centerx;
                float rowWidth = 400;

                //时间
                myp.NewRow(rowheight);
                sTr = _reportListSZ[0].SamplingData;
                str = "时间:" + (sTr.Equals("") ? "/" : sTr);
                myp.DrawText(str, myp.PaperPrintWidth, rowheight, Font3, StringAlignment.Far, StringAlignment.Center, false, true, false, false, 6, 0);

                //受检人
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListSZ[0].CheckedCompany;
                myp.DrawCell("受检单位", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, 1600, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //联系人 | 联系电话
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListSZ[0].Contact;
                myp.DrawCell("联系人", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, 800, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListSZ[0].ContactPhone;
                myp.DrawCell("联系电话", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //受检单位行政区域
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListSZ[0].CheckedCompanyArea;
                if (sTr.Equals("福田"))
                {
                    sTr = "福田☑ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("罗湖"))
                {
                    sTr = "福田□ 罗湖☑ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("南山"))
                {
                    sTr = "福田□ 罗湖□ 南山☑ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("盐田"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田☑ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("宝安"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安☑ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("龙岗"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗☑ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("光明"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明☑ 坪山□ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("坪山"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山☑ 龙华□ 大鹏□";
                }
                else if (sTr.Equals("龙华"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华☑ 大鹏□";
                }
                else if (sTr.Equals("大鹏□"))
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏☑";
                }
                else
                {
                    sTr = "福田□ 罗湖□ 南山□ 盐田□ 宝安□ 龙岗□ 光明□ 坪山□ 龙华□ 大鹏□";
                }
                myp.DrawCell("受检单位\n行政区域", 280, rowheight, Font5, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr, 1600, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //打印表格标题 检测项目
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                myp.DrawCell("编号", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("样品名称", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("抽样基数(kg)", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("样品来源", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("快筛结果", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);

                int rowCount = _reportDetailListSZ.Count, rowNum = 0;
                string SampleNumber = rowCount.ToString(), SampleYX = "";
                rowNum = rowCount < 15 ? 15 : rowCount;
                if (rowCount > 0)
                {
                    for (int i = 0; i < rowNum; i++)
                    {
                        string strCode = (i + 1).ToString("d3");
                        if (rowCount > 0)
                        {
                            myp.NewRow(rowheight);
                            myp.Currentx = centerx;
                            //编号
                            myp.DrawCell(strCode, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListSZ[i].SampleName.Equals("") ? "/" : _reportDetailListSZ[i].SampleName, rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListSZ[i].SampleBase.Equals("") ? "/" :_reportDetailListSZ[i].SampleBase, rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListSZ[i].SampleSource.Equals("") ? "/" :_reportDetailListSZ[i].SampleSource, rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListSZ[i].Result.Equals("") ? "/" : _reportDetailListSZ[i].Result, rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            if (_reportDetailListSZ[i].Result.Equals("不合格"))
                            {
                                if (!SampleYX.Equals(""))
                                    SampleYX += "，";
                                SampleYX += strCode;
                            }
                            rowCount--;
                        }
                        else
                        {
                            myp.NewRow(rowheight);
                            myp.Currentx = centerx;
                            myp.DrawCell(strCode, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rowNum; i++)
                    {
                        string strCode = (i + 1).ToString("d3");
                        myp.NewRow(rowheight);
                        myp.Currentx = centerx;
                        myp.DrawCell(strCode, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", rowWidth, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                    }
                }

                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                myp.DrawCell("抽样人员\n签字确认", 280, rowheight * 2, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("本次共抽取 " + SampleNumber + " 份样品进行快速检测，其中：" + (SampleYX.Equals("") ? "/" : SampleYX) + " 号为阳性，其余为阴性。抽样检测人员签名：", 1600, rowheight * 2, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                myp.NewRow(rowheight * 2);
                myp.Currentx = centerx;
                myp.DrawCell("受检单位\n签字确认", 280, rowheight * 3, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("1、本单位对________________号样品阳性检测结果无异议，自愿销毁抽样批次所代表的农产品。\n2、本单位对________________号样品阳性检测结果有异议，暂停销售，等待定量检测结果。\n                                     受检单位签字(盖章):", 1600, rowheight * 3, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                #region 循环打印dataGridView中的内容
                //myp.NewRow(rowheight);
                ////正式开始打印表格
                //rowheight = 100;//表格要打印的行高
                //bool needprintheader = true;
                //for (int i = 0; i < dataGridView.RowCount; i++)
                //{
                //    if (needprintheader == true)
                //    {
                //        //打印表格头
                //        //if (i != 0)
                //        //{
                //        //    //不是第一页，需要打印标题行                   
                //        //    str = "打印测试";
                //        //    myp.DrawTitle(str, Font1, Color.Black, 0);
                //        //    str = "Charge Bill" + "\n" + "\n";
                //        //    myp.DrawTitle(str, Font2, Color.Black, 0);
                //        //    myp.Currenty = myp.Currenty + 30;//留一点空白
                //        //}
                //        //再打印表格列标题行
                //        myp.DrawDGVHeader(newdgv, rowheight, true, true, "1111", 6);
                //        needprintheader = false;
                //    }
                //    if (myp.IsNewPage(rowheight) == true)
                //    {
                //        //需要换页
                //        myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                //        needprintheader = true;
                //        i = i - 1;
                //    }
                //    else
                //    {
                //        //打印当前行
                //        myp.DrawDGVRow(newdgv, i, rowheight, true, false, "1111", 6);
                //    }
                //}

                ////以下开始打印表尾
                //float footerheight = 500;//表尾要打印的高度，下面打印的内容不能超过这个高度
                //if (myp.IsNewPage(footerheight) == true)
                //{
                //    //说明表尾要不能全部打印下去，先换页
                //    myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                //}

                ////然后定位表尾要打印的开始位置
                //myp.Currenty = myp.PaperPrintHeight - footerheight;
                ////抽样人 | 受检人
                //myp.NewRow(rowheight);
                //myp.Currentx = centerx;
                //str = "结论:" + _Conclusion + "                               核准人:\n\n" + "检测单位盖章:\n\n" + "受检人对检验结果确认签名:";
                //myp.DrawCell(str, 1880, 350, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                #endregion

                newdgv.Dispose();
                newdgv = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 每周一检报表打印预览 
        /// 多检测项目
        /// </summary>
        private void PrintDocument_MZYJ_N() 
        {
            DataGridView newdgv = new DataGridView();
            try
            {
                myp.CopyDataGridView(dataGridView_MZYJ, newdgv, false);
                newdgv.GridColor = Color.Black;
                //打印处理过程
                myp.IsNeedCheckNewPage = false;
                System.Drawing.Printing.Margins mymargin;
                mymargin = new System.Drawing.Printing.Margins();
                mymargin.Left = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Right = Convert.ToInt32(myp.ConvertInchToCm(45));
                mymargin.Top = Convert.ToInt32(myp.ConvertInchToCm(80));
                mymargin.Bottom = Convert.ToInt32(myp.ConvertInchToCm(45));
                myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                mymargin.Top = 20;
                //先打印表头内容
                Font Font1 = new Font("黑体", 18, FontStyle.Bold);
                Font Font2 = new Font("黑体", 14, FontStyle.Bold);
                Font Font3 = new Font("宋体", 12);
                Font Font4 = new Font("华文行楷", 12);
                string str = _reportListMZYJ[0].ReportName, sTr = "";
                myp.DrawTitle(str, Font1, Color.Black, 0);
                myp.NewRow(60);
                float rowheight = 100;
                float centerx = (myp.PaperPrintWidth - 1880) / 2;
                myp.Currentx = centerx;
                float rowWidth = 420;

                //样品编号
                myp.NewRow(rowheight);
                sTr = _reportListMZYJ[0].SamplingCode;
                str = "样品编号:" + (sTr.Equals("") ? "/" : sTr);
                myp.DrawText(str, myp.PaperPrintWidth, rowheight, Font3, StringAlignment.Far, StringAlignment.Center, false, true, false, false, 6, 0);

                //受检人
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].CheckedCompany;
                myp.DrawCell("受检人", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, 1670, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //经营地址 | 邮政编号
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].Address;
                myp.DrawCell("经营地址", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, 1050, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].ZipCode;
                myp.DrawCell("邮政编号", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //营业执照 | 经营性质
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].BusinessLicense;
                myp.DrawCell("营业执照", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, 1050, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].BusinessNature;
                myp.DrawCell("经营性质", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //联系人 | 电  话 | 传  真
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].Contact;
                myp.DrawCell("联系人", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].ContactPhone;
                myp.DrawCell("电  话", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].Fax;
                myp.DrawCell("传  真", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //商品名称 | 商品售价 | 规格型号
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].ProductName;
                myp.DrawCell("商品名称", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].ProductPrice;
                myp.DrawCell("商品售价", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].Specifications;
                myp.DrawCell("规格型号", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //质量等级 | 批号 | 注册商标
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].QualityGrade;
                myp.DrawCell("质量等级", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].BatchNumber;
                myp.DrawCell("批  号", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].RegisteredTrademark;
                myp.DrawCell("注册商标", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //抽样数量 | 抽样基数 | 进货数量
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].SamplingNumber;
                myp.DrawCell("抽样数量", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : (sTr + _reportListMZYJ[0].Unit), rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].SamplingBase;
                myp.DrawCell("抽样基数", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].IntoNumber;
                myp.DrawCell("进货数量", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : (sTr + _reportListMZYJ[0].Unit), rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //库存数量 | 备  注 | 进货数量
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                sTr = _reportListMZYJ[0].InventoryNubmer;
                myp.DrawCell("库存数量", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : (sTr + _reportListMZYJ[0].Unit), rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].Notes;
                myp.DrawCell("备  注", 210, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                sTr = _reportListMZYJ[0].SamplingData;
                myp.DrawCell("抽样日期", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Far, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell(sTr.Equals("") ? "/" : sTr, rowWidth, rowheight, Font3, "6666", Color.Black, sTr.Equals("") ? StringAlignment.Center : StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                //抽样人 | 受检人
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                myp.DrawCell("抽样人:                                  受检人(单位):", 1880, 350, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Near, true, false, false, false, 0, 0);

                myp.NewRow(310);
                str = "";
                myp.DrawText(str, myp.PaperPrintWidth, 210, Font3, StringAlignment.Far, StringAlignment.Center, false, true, false, false, 6, 0);

                //打印表格标题 检测项目
                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                myp.DrawCell("检测项目", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("检测依据", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("标准值", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("实测值", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("结论", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("检测人", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                myp.DrawCell("检测日期", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);

                //循环打印四行表格出来
                int rowCount = _reportDetailListMZYJ.Count;
                bool first = true;
                if (rowCount > 0)
                {
                    rowheight = 120;
                    for (int i = 0; i < 4; i++)
                    {
                        if (rowCount > 0)
                        {
                            myp.NewRow(first ? 100 : rowheight);
                            myp.Currentx = centerx;
                            myp.DrawCell(_reportDetailListMZYJ[i].CheckItem, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListMZYJ[i].CheckBasis, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            sTr = _reportDetailListMZYJ[i].StandardValues;
                            myp.DrawCell(sTr.Equals("") ? "" : (sTr + _reportDetailListMZYJ[i].Unit), 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            sTr = _reportDetailListMZYJ[i].CheckValues;
                            myp.DrawCell(sTr.Equals("") ? "" : (sTr + _reportDetailListMZYJ[i].Unit), 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListMZYJ[i].Conclusion, 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell(_reportDetailListMZYJ[i].CheckUser, 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            DateTime dt = new DateTime();//时间只格式 yyyy/MM/dd
                            dt = Convert.ToDateTime(_reportDetailListMZYJ[i].CheckData);
                            myp.DrawCell(dt.ToShortDateString().ToString(), 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                            rowCount -= 1;
                        }
                        else
                        {
                            myp.NewRow(first ? 100 : rowheight);
                            myp.Currentx = centerx;
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                            myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        }
                        first = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        myp.NewRow(first ? 100 : rowheight);
                        myp.Currentx = centerx;
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 200, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        myp.DrawCell("", 280, rowheight, Font3, "6666", Color.Black, StringAlignment.Center, StringAlignment.Center, true, false, false, false, 0, 0);
                        first = false;
                    }
                }

                myp.NewRow(rowheight);
                myp.Currentx = centerx;
                str = "结论:" + _Conclusion + "                               核准人:\n\n\n" + "检测单位盖章:\n\n\n" + "受检人对检验结果确认签名:";
                myp.DrawCell(str, 1880, 400, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);

                #region 循环打印dataGridView中的内容
                //myp.NewRow(rowheight);
                ////正式开始打印表格
                //rowheight = 100;//表格要打印的行高
                //bool needprintheader = true;
                //for (int i = 0; i < dataGridView.RowCount; i++)
                //{
                //    if (needprintheader == true)
                //    {
                //        //打印表格头
                //        //if (i != 0)
                //        //{
                //        //    //不是第一页，需要打印标题行                   
                //        //    str = "打印测试";
                //        //    myp.DrawTitle(str, Font1, Color.Black, 0);
                //        //    str = "Charge Bill" + "\n" + "\n";
                //        //    myp.DrawTitle(str, Font2, Color.Black, 0);
                //        //    myp.Currenty = myp.Currenty + 30;//留一点空白
                //        //}
                //        //再打印表格列标题行
                //        myp.DrawDGVHeader(newdgv, rowheight, true, true, "1111", 6);
                //        needprintheader = false;
                //    }
                //    if (myp.IsNewPage(rowheight) == true)
                //    {
                //        //需要换页
                //        myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                //        needprintheader = true;
                //        i = i - 1;
                //    }
                //    else
                //    {
                //        //打印当前行
                //        myp.DrawDGVRow(newdgv, i, rowheight, true, false, "1111", 6);
                //    }
                //}

                ////以下开始打印表尾
                //float footerheight = 500;//表尾要打印的高度，下面打印的内容不能超过这个高度
                //if (myp.IsNewPage(footerheight) == true)
                //{
                //    //说明表尾要不能全部打印下去，先换页
                //    myp.NewPage(System.Drawing.Printing.PaperKind.A4, mymargin, false);
                //}

                ////然后定位表尾要打印的开始位置
                //myp.Currenty = myp.PaperPrintHeight - footerheight;
                ////抽样人 | 受检人
                //myp.NewRow(rowheight);
                //myp.Currentx = centerx;
                //str = "结论:" + _Conclusion + "                               核准人:\n\n" + "检测单位盖章:\n\n" + "受检人对检验结果确认签名:";
                //myp.DrawCell(str, 1880, 350, Font3, "6666", Color.Black, StringAlignment.Near, StringAlignment.Center, true, false, false, false, 0, 0);
                #endregion

                newdgv.Dispose();
                newdgv = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 页码
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="curpage"></param>
        private void myp_HeaderFooterOut(int pages, int curpage)
        {
            //myp.PrintFooter("", "第" + Convert.ToString(curpage) + "页 共" + Convert.ToString(pages) + "页", "", 0);
        }

        /// <summary>
        /// 初始化表格数据
        /// </summary>
        private void ChangeRows()
        {
            if (_strType.Equals("MZYJ_N") || _strType.Equals("MZYJ"))
            {
                ChangeRows_MZYJ();
            }
            else if (_strType.Equals("SZ"))
            {
                ChangeRows_SZ();
            }
        }

        /// <summary>
        /// 深圳
        /// </summary>
        private void ChangeRows_SZ() 
        {
            //构建打印数据,每页仅打印四条检测数据
            Random myrnd = new Random();
            dataGridView_SZ.AllowUserToAddRows = false;
            try
            {
                if (_dataTable.Rows.Count > 0)
                {
                    _reportListSZ = (List<clsReportSZ>)StringUtil.DataTableToIList<clsReportSZ>(_dataTable, 1);
                    DataTable dt = _clsROBLL.SearchReportDetail(_reportListSZ[0].ID.ToString(), "SZ");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        _reportDetailListSZ = (List<clsReportDetailSZ>)StringUtil.DataTableToIList<clsReportDetailSZ>(dt, 1);
                        dataGridView_SZ.RowCount = _reportDetailListSZ.Count;
                        for (int i = 0; i < _reportDetailListSZ.Count; i++)
                        {
                            dataGridView_SZ.Rows[i].Cells[0].Value = _reportDetailListSZ[i].SampleName;
                            dataGridView_SZ.Rows[i].Cells[1].Value = _reportDetailListSZ[i].SampleBase;
                            dataGridView_SZ.Rows[i].Cells[2].Value = _reportDetailListSZ[i].SampleSource;
                            dataGridView_SZ.Rows[i].Cells[3].Value = _reportDetailListSZ[i].Result;
                            dataGridView_SZ.Rows[i].Cells[4].Value = _reportDetailListSZ[i].SysCode;
                            dataGridView_SZ.Rows[i].Cells[5].Value = _reportDetailListSZ[i].ID;
                            dataGridView_SZ.Rows[i].Cells[6].Value = _reportDetailListSZ[i].ReportID;
                            dataGridView_SZ.Rows[i].Cells[7].Value = _reportDetailListSZ[i].Code;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 每周一检
        /// </summary>
        private void ChangeRows_MZYJ() 
        {
            //构建打印数据,每页仅打印四条检测数据
            Random myrnd = new Random();
            dataGridView_MZYJ.AllowUserToAddRows = false;
            try
            {
                if (_dataTable.Rows.Count > 0)
                {
                    _reportListMZYJ = (List<clsReport>)StringUtil.DataTableToIList<clsReport>(_dataTable, 1);
                    DataTable dataTable = _clsROBLL.SearchReportDetail("'" + _reportListMZYJ[0].ID + "'", "MZYJ");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        _reportDetailListMZYJ = (List<clsReportDetail>)StringUtil.DataTableToIList<clsReportDetail>(dataTable, 1);
                        dataGridView_MZYJ.RowCount = _reportDetailListMZYJ.Count;
                        for (int i = 0; i < _reportDetailListMZYJ.Count; i++)
                        {
                            dataGridView_MZYJ.Rows[i].Cells[0].Value = _reportDetailListMZYJ[i].CheckItem;
                            dataGridView_MZYJ.Rows[i].Cells[1].Value = _reportDetailListMZYJ[i].CheckBasis;
                            dataGridView_MZYJ.Rows[i].Cells[2].Value = _reportDetailListMZYJ[i].StandardValues;
                            dataGridView_MZYJ.Rows[i].Cells[3].Value = _reportDetailListMZYJ[i].CheckValues;
                            dataGridView_MZYJ.Rows[i].Cells[4].Value = _reportDetailListMZYJ[i].Unit;
                            dataGridView_MZYJ.Rows[i].Cells[5].Value = _reportDetailListMZYJ[i].Conclusion;
                            dataGridView_MZYJ.Rows[i].Cells[6].Value = _reportDetailListMZYJ[i].CheckUser;
                            dataGridView_MZYJ.Rows[i].Cells[7].Value = _reportDetailListMZYJ[i].CheckData;
                            if (_reportDetailListMZYJ[i].Conclusion.Equals("不合格"))
                                _Conclusion = "不合格";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

    }
}
