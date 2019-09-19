using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using WorkstationModel.Model;
using WorkstationModel.UpData;
using WorkstationDAL.Basic;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Configuration;
using WorkstationModel.beihai;

namespace WorkstationUI.function
{
    public partial class ucSearchData : UserControl
    {
        private clsSetSqlData sql = new clsSetSqlData();
        public DataTable DataReadTable = null;
        private int rowNum=0;
        private object G_missing = //定义G_missing字段并添加引用
            System.Reflection.Missing.Value;
        private string path = AppDomain.CurrentDomain.BaseDirectory;
        private clsUpLoadData udata = new clsUpLoadData();
        private string[] isup = null;//记录是否上传
        private DataTable dt = null;
        private string ExcelPath = string.Empty;
        private clsSaveResult InData = new clsSaveResult();
        private clsdiary dy = new clsdiary();
        private StringBuilder sb = new StringBuilder();
        int pageSize = 0;     //每页显示行数
        int nMax = 0;         //总记录数
        int pageCount = 0;    //页数＝总记录数/每页显示行数
        int pageCurrent = 0;   //当前页号
        int nCurrent = 0;      //当前记录行
        private DataTable dtInfo = new DataTable();
        public ucSearchData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表头字段名修改
        /// </summary>
        private void DataGridViewModefile()
        {
            if (CheckDatasdd.Rows.Count > 0)
            {
                CheckDatasdd.Columns["ChkNum"].HeaderText = "检测编号";
                CheckDatasdd.Columns["Checkitem"].HeaderText = "检测项目";
                CheckDatasdd.Columns["SampleName"].HeaderText = "样品名称";
                CheckDatasdd.Columns["CheckData"].HeaderText = "检测结果";
                CheckDatasdd.Columns["Unit"].HeaderText = "单位";
                CheckDatasdd.Columns["CheckTime"].HeaderText = "检测时间";
                CheckDatasdd.Columns["CheckUnit"].HeaderText = "被检单位";
                CheckDatasdd.Columns["Result"].HeaderText = "结论";
                CheckDatasdd.Columns["Save"].HeaderText = "已保存";
                CheckDatasdd.Columns["Machine"].HeaderText = "检测仪器";
                CheckDatasdd.Columns["SampleTime"].HeaderText = "采样时间";
                CheckDatasdd.Columns["SampleAddress"].HeaderText = "采样地点";
                CheckDatasdd.Columns["DetectUnit"].HeaderText = "检测单位";
                CheckDatasdd.Columns["TestBase"].HeaderText = "检测依据";
                CheckDatasdd.Columns["LimitData"].HeaderText = "标准值";
                CheckDatasdd.Columns["Tester"].HeaderText = "检测员";
                CheckDatasdd.Columns["StockIn"].HeaderText = "进货数量";
                CheckDatasdd.Columns["SampleNum"].HeaderText = "样品数量";
                CheckDatasdd.Columns["IsUpload"].HeaderText = "已上传";
                CheckDatasdd.Columns["SampleCode"].HeaderText = "样品编号";
                CheckDatasdd.Columns["SampleCategory"].HeaderText = "样品种类";
                CheckDatasdd.Columns["MachineNum"].HeaderText = "仪器编号";
                CheckDatasdd.Columns["ProductPlace"].HeaderText = "样品产地";
                CheckDatasdd.Columns["ProductDatetime"].HeaderText = "生产日期";
                CheckDatasdd.Columns["Barcode"].HeaderText = "条形码";
                CheckDatasdd.Columns["ProcodeCompany"].HeaderText = "生产企业";
                CheckDatasdd.Columns["ProduceAddr"].HeaderText = "产地地址";
                CheckDatasdd.Columns["ProduceUnit"].HeaderText = "生产单位";
                CheckDatasdd.Columns["SendTestDate"].HeaderText = "送检日期";
                CheckDatasdd.Columns["NumberUnit"].HeaderText = "数量单位";
                CheckDatasdd.Columns["CompanyNeture"].HeaderText = "被检单位编号";
                CheckDatasdd.Columns["DoResult"].HeaderText = "处理结果";
                CheckDatasdd.Columns["HoleNum"].HeaderText = "通道号";
                CheckDatasdd.Columns["Xiguangdu"].HeaderText = "吸光度";
                CheckDatasdd.Columns["StallNum"].HeaderText = "摊位号";
                
                //隐藏字段
                CheckDatasdd.Columns["ID"].Visible = false;
                CheckDatasdd.Columns["NumberUnit"].Visible = false;
                CheckDatasdd.Columns["Barcode"].Visible = false;
                CheckDatasdd.Columns["ProcodeCompany"].Visible = false;
                CheckDatasdd.Columns["ProduceAddr"].Visible = false;
                CheckDatasdd.Columns["ChkNum"].Visible = false;
                CheckDatasdd.Columns["StockIn"].Visible = false;
                CheckDatasdd.Columns["Save"].Visible = false;
                CheckDatasdd.Columns["DoResult"].Visible = false;
                CheckDatasdd.Columns["sampleid"].Visible = false;
                //CheckDatas.Columns["HoleNum"].Visible = false;
                CheckDatasdd.Columns["BID"].Visible = false;
                CheckDatasdd.Columns["Xiguangdu"].Visible  = false ;
                CheckDatasdd.Columns["DoResult"].Visible = false;
                CheckDatasdd.Columns["NumberUnit"].Visible = false;
                CheckDatasdd.Columns["ProductPlace"].Visible = false;
                CheckDatasdd.Columns["ProductDatetime"].Visible = false;
                CheckDatasdd.Columns["Barcode"].Visible = false;
                CheckDatasdd.Columns["ProcodeCompany"].Visible = false;
                CheckDatasdd.Columns["ProduceAddr"].Visible = false;
                CheckDatasdd.Columns["ProduceUnit"].Visible = false;
                CheckDatasdd.Columns["SendTestDate"].Visible = false;
                CheckDatasdd.Columns["SampleCategory"].Visible = false;
                CheckDatasdd.Columns["SampleAddress"].Visible = false;
                CheckDatasdd.Columns["SampleTime"].Visible = false;
                CheckDatasdd.Columns["TID"].Visible = false;
                CheckDatasdd.Columns["SampleNum"].Visible = false;
                CheckDatasdd.Columns["MachineNum"].Visible = false;
                CheckDatasdd.Columns["DetectUnit"].Visible = false;


            }
        }
        //查询
        private void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (dTStart.Value > dTPEnd.Value)
                {
                    MessageBox.Show("数据查询起始时间不能大于结束时间!", "系统提示");
                    return;
                }
                btnfind.Enabled = false;
                CheckDatasdd.DataSource = null;
                //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
                sb.Clear();
                sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
              
                //sb.AppendFormat("CheckTime>=#{0}#", dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));
                //sb.AppendFormat(" AND CheckTime<=#{0}/", dTPEnd.Value.Year.ToString());     
                //sb.AppendFormat("{0}/{1} 23:59:59#", dTPEnd.Value.Month.ToString(), dTPEnd.Value.Day.ToString());
                if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
                }
                if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
                }
                if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
                }
                sb.Append(" ORDER BY ID");
                DataTable dtb = sql.GetDataTable(sb.ToString(), "");
                if (dtb != null && dtb.Rows.Count >0)
                {
                    CheckDatasdd.DataSource = dtb;

                    if (disAll == true)
                    {
                        CheckDatasdd.DataSource = dtb;
                        DataGridViewModefile();
                        if (CheckDatasdd.Rows.Count > 0)
                        {
                            CheckDatasdd.Columns["ID"].Visible = false;
                        }
                    }
                    else
                    {
                        dtInfo = dtb;
                        InitDataSet();
                        if (CheckDatasdd.Rows.Count > 0)
                        {
                            CheckDatasdd.Columns["ID"].Visible = false;
                        }
                    }

                    CmbgetItemName();
                    CmbgetSampleName();
                }
                btnfind.Enabled = true;
             
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "数据查询");
                btnfind.Enabled = true;
            }
        }
        //分页显示
        private void InitDataSet()
        {
            pageSize = 20;      //设置页面行数
            nMax = dtInfo.Rows.Count;
            pageCount = (nMax / pageSize);    //计算出总页数
            if ((nMax % pageSize) > 0) pageCount++;
            pageCurrent = 1;    //当前页数从1开始
            nCurrent =0;       //当前记录数从0开始
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                int nStartPos = 0;   //当前页面开始记录行
                int nEndPos = 0;     //当前页面结束记录行
                DataTable dtTemp = dtInfo.Clone();   //克隆DataTable结构框架
                if (pageCurrent == pageCount)
                    nEndPos = nMax;
                else
                    nEndPos = pageSize * pageCurrent;
                nStartPos = nCurrent;
                tspallpage.Text = pageCount.ToString();
                tsplabpage.Text = Convert.ToString(pageCurrent);
                toolStripItemCount.Text = "页 / " + nMax + "条";
                //lblPageCount.Text = pageCount.ToString();
                //txtCurrentPage.Text = Convert.ToString(pageCurrent);
                if (dtInfo.Rows.Count < 1)
                {
                    //CheckDatasdd.DataSource = DataReadTable;
                    CheckDatasdd.DataSource = null;
                    return;
                }
                //从元数据源复制记录行
                for (int i = nStartPos; i < nEndPos; i++)
                {
                    dtTemp.ImportRow(dtInfo.Rows[i]);
                    nCurrent++;
                }
                //CheckDatas.DataSource = distable;
                CheckDatasdd.DataSource = dtTemp;
                DataGridViewModefile();
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据查询");
            }
        }

        //查询数据显示
        public  void searchdatabase()
        {
            try
            {
                //DataReadTable.Clear();//清除缓存的数据
                CheckDatasdd.DataSource = null;
                //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
                sb.Clear();
                sb.AppendFormat("CheckTime>=#{0}#", dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));
                sb.AppendFormat(" AND CheckTime<=#{0} 23:59:59#", DateTime.Now.ToString("yyyy/MM/dd"));

                if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat("AND Result='%{0}%'", cmbResult.Text.Trim());
                }
                if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat("AND Checkitem='%{0}%'", cmbTestItem.Text.Trim());
                }
                //if (!string.IsNullOrEmpty(cmbTestUnit.Text.Trim()))
                //{
                //    sb.AppendFormat("AND CheckUnit='%{0}%'", cmbTestUnit.Text.Trim());
                //}
                //if (!string.IsNullOrEmpty(cmbSample.Text.Trim()))
                //{
                //    sb.AppendFormat("AND SampleName='%{0}%'", cmbSample.Text.Trim());
                //}
                sb.Append(" ORDER BY ID");
                DataTable dtb = sql.GetDataTable(sb.ToString(), "");
                if (dtb != null)
                {
                  
                    if (disAll == true)
                    {
                        //CheckDatas.DataSource = DataReadTable;
                        CheckDatasdd.DataSource = dtb;
                        DataGridViewModefile();
                    }
                    else
                    {
                        dtInfo = dtb;
                        InitDataSet();
                    }
                  
                    CmbgetItemName();
                    CmbgetSampleName();
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据查询");
            }
        }
     
        public  void ucSearchData_Load(object sender, EventArgs e)
        {
            try
            {
               
                dTStart.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString();
              

                if (CheckDatasdd!=null )
                {
                    for (int i = 0; i < CheckDatasdd.Columns.Count; i++)
                    {
                        CheckDatasdd.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
               
                dy.savediary(DateTime.Now.ToString(), "数据查询成功" , "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据查询");
            }
        }

        //把数据导出到Excel
        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDatasdd.Rows.Count < 1)
                {
                    MessageBox.Show("没有数据，请重新查询", "提示");
                    return;
                }
                string err="";
                DialogResult D = MessageBox.Show("导出选中的记录单击 [是(Y)]，导出全部记录选择 [否(N)]", "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (D == DialogResult.No)//导出全部记录
                {
                    sb.Clear();
                    //sb.AppendFormat("CheckTime>=#{0}#", dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));
                    //sb.AppendFormat(" AND CheckTime<=#{0}/", dTPEnd.Value.Year.ToString());
                    //sb.AppendFormat("{0}/{1} 23:59:59#", dTPEnd.Value.Month.ToString(), dTPEnd.Value.Day.ToString());
                    sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
              
                    if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
                    {
                        sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
                    {
                        sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
                    {
                        sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
                    }

                    SaveFileDialog P_SaveFileDialog = new SaveFileDialog();//创建保存文件对话框对象
                    P_SaveFileDialog.Filter = "*.xls|*.xls";
                    P_SaveFileDialog.FileName = string.Format("{0} 检测数据 - {1}", Global.MachineNum, DateTime.Now.ToString("yyyy-MM-dd"));
                    
                    if (DialogResult.OK == P_SaveFileDialog.ShowDialog())//确认是否保存文件                
                    {
                        DataTable dtex = sql.ExportExcel(sb.ToString(), "", out err);
                        if (dtex != null && dtex.Rows.Count != 0)
                        {
                            if (ExcelHelper.CreateExcel(dtex, P_SaveFileDialog.FileName))
                            {
                                MessageBox.Show("导出成功！本次共导出 " + dtex.Rows.Count + " 条数据！", "系统提示");
                            }
                        }
                        else
                        {
                            MessageBox.Show("暂时没有数据可导出！", "操作提示");
                        }
                    }

                }
                else if (D == DialogResult.Yes) //导出选中的记录
                {
                    if (CheckDatasdd.SelectedRows.Count < 1)
                    {
                        MessageBox.Show("请选择需要导出的记录", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    sb.Clear();
                    string ids="";
                    for(int i=0;i<CheckDatasdd.SelectedRows.Count;i++)
                    {
                        ids += ids == "" ? CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString().Trim() : "," + CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString().Trim();
                    }

                    sb.AppendFormat("ID in ({0})", ids);
                    SaveFileDialog P_SaveFileDialog = new SaveFileDialog();//创建保存文件对话框对象
                    P_SaveFileDialog.Filter = "*.xls|*.xls";
                    if (DialogResult.OK == P_SaveFileDialog.ShowDialog())//确认是否保存文件                
                    {
                        DataTable dtex = sql.ExportExcel(sb.ToString(), "", out err);
                        if (dtex != null && dtex.Rows.Count != 0)
                        {
                            if (ExcelHelper.CreateExcel(dtex, P_SaveFileDialog.FileName))
                            {
                                MessageBox.Show("导出成功！本次共导出 " + dtex.Rows.Count + " 条数据！", "系统提示");
                            }
                        }
                        else
                        {
                            MessageBox.Show("暂时没有数据可导出！", "操作提示");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "导出数据失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"导出数据");
            }
        }
        //报表打印
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDatasdd.SelectedRows.Count < 1)
                {
                    MessageBox.Show("请选择需要打印的记录", "提示");
                    return;
                }
                PrintCheckedReport();
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "打印报表失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message);
            }
        }
        private void PrintCheckedReport()
        {
            try
            {
                string html=GetHtmlDoc();
                SaveFile(html);
                //打印检测报告
                reportform window = new reportform()
                {
                    HtmlUrl = path + "report\\CheckedReportModel.html"
                };
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "打印报表失败：" + ex.Message, "错误");
                MessageBox.Show(string.Format("打印检测报告时出现异常！\r\n\r\n异常信息：\r\n{0}", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetHtmlDoc()
        {
            string htmlDoc = string.Empty;
            try
            {
                for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                {
                    //string Organization = Global.samplenameadapter[0].Organization;
                    string Result = string.Empty;
                    if (CheckDatasdd.SelectedRows.Count > 0)//CheckDatas.SelectedRows != null &&
                    {
                       
                        Result = CheckDatasdd.Rows[rowNum].Cells["Result"].Value.ToString();
                        htmlDoc = System.IO.File.ReadAllText(path + "\\report\\CheckedReportTitle.txt", System.Text.Encoding.Default);
                        htmlDoc += string.Format("<div class=\"number\">编号：{0}</div>", "/");// CheckDatas.Rows[rowNum].Cells[0].Value.ToString().Length > 0 ? (CheckDatas.Rows[rowNum].Cells[0].Value.ToString()) : "/"
                        htmlDoc += "<div style=\"float:left; margin-left:40px; margin-top:20px;\">";
                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

                        htmlDoc += "<tbody><tr><th width=\"190\" height=\"76\" class=\"as\">样品名称</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["SampleName"].Value.ToString().Length > 0 ? (CheckDatasdd.Rows[rowNum].Cells["SampleName"].Value.ToString()) : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"as\">样品编号</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td></tr>", "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）时间</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["SampleTime"].Value.ToString().Length > 0 ? DateTime.Parse(CheckDatasdd.Rows[rowNum].Cells["SampleTime"].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）地点</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckDatasdd.Rows[rowNum].Cells["SampleAddress"].Value.ToString().Length > 0 ? (CheckDatasdd.Rows[rowNum].Cells["SampleAddress"].Value.ToString()) : "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">被检单位</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["CheckUnit"].Value.ToString().Length > 0 ? (CheckDatasdd.Rows[rowNum].Cells["CheckUnit"].Value.ToString()) : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">被检单位地址</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">检测时间</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["CheckTime"].Value.ToString().Length > 0 ? DateTime.Parse((CheckDatasdd.Rows[rowNum].Cells["CheckTime"].Value.ToString())).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">检测依据</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckDatasdd.Rows[rowNum].Cells["TestBase"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["TestBase"].Value.ToString() : "/");
                        htmlDoc += "</tbody></table>";

                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测项目</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测仪器</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测结果</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">标准值</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\" id=\"unique\">检测结论</th></tr>";

                        htmlDoc += string.Format("<tr><td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["Checkitem"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["Checkitem"].Value.ToString() : "/");
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["Machine"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["Machine"].Value.ToString() : "/");
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["CheckData"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["CheckData"].Value.ToString() : "/");//+ (Result.Equals("阴性") || Result.Equals("阴性") ? string.Empty : (" " + CheckDatas.Rows[rowNum].Cells[3].Value.ToString()))
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatasdd.Rows[rowNum].Cells["LimitData"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["LimitData"].Value.ToString() : "/");//CheckDatas.Rows[rowNum].Cells[13].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[13].Value.ToString() :
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\" id=\"unique\">{0}</td></tr>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");
                        htmlDoc += "</tbody></table>";

                        htmlDoc += string.Format("<div class=\"forming\"><div class=\"conclusion\">结论：{0}</div>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");
                        htmlDoc += "<div class=\"img\"><img src=\"CheckedReportOfficialSeal.png\"></div>";
                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-54px;font-size:16px;\">";
                        htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                        htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">检测单位：{0}</td>", CheckDatasdd.Rows[rowNum].Cells["DetectUnit"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["DetectUnit"].Value.ToString() : "/");
                        htmlDoc += "</tr></tbody></table>";

                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-20px;font-size:16px;\">";
                        htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                        htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">报告时间：{0}</td>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        htmlDoc += "</tr></tbody></table></div>";

                        htmlDoc += "<div class=\"forming\" style=\"height:54px; margin-top:0; float:left;\"><div class=\"note\"><span class=\"NT\">备注：检测结论为不合格时，应该及时上报。</span></div></div></div>";
                        htmlDoc += string.Format("<div class=\"personnel\">检测人员：{0}</div>", CheckDatasdd.Rows[rowNum].Cells["Tester"].Value.ToString().Length > 0 ? CheckDatasdd.Rows[rowNum].Cells["Tester"].Value.ToString() : "/");
                        htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">审核：</div>";
                        htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">批准：</div>";
                        htmlDoc += "<div style=\" float:left; width:800px; height:60px;\"></div></div></body></html>";
                    }
                    else
                    {
                        MessageBox.Show("请选择需要打印的记录","操作提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "生成报表失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return htmlDoc;
        }
        private void SaveFile(string str)
        {
            if (!(Directory.Exists(path + "\\report")))
            {
                Directory.CreateDirectory(path + "\\report");
            }
            string filePath = path + "\\report\\CheckedReportModel.html";
            //如果存在则删除
            //if (File.Exists(filePath)) System.IO.File.Delete(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "保存失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "保存", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        //导入数据
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string err = string.Empty;
                int n = 0;
                int re = 0;
                int importNum = 0;

                openFileDialog1.Filter="Excel文件|*.xls";//设置打开文件筛选器
                openFileDialog1.Title = "打开Excel文件";//设置打开对话框标题
                ExcelPath = openFileDialog1.FileName;
                DataTable dtExcel = null;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件
                {
                    dtExcel = ExcelHelper.ImportExcel(openFileDialog1.FileName.Trim(), 0);
                    if (dtExcel != null && dtExcel.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            sb.Length = 0;
                            sb.AppendFormat("ChkNum='{0}' and SampleName='{1}' and CheckTime='{2}'", dtExcel.Rows[i]["检测编号"].ToString(), dtExcel.Rows[i]["样品名称"].ToString(), dtExcel.Rows[i]["检测时间"].ToString());
                            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
                            if (dtb != null && dtb.Rows.Count > 0)
                            {
                                continue;
                            }

                            InData.CheckNumber = dtExcel.Rows[i]["检测编号"].ToString();// Global.GUID(null, 1);
                            InData.Checkitem = dtExcel.Rows[i]["检测项目"].ToString();
                            InData.SampleName = dtExcel.Rows[i]["样品名称"].ToString();
                            InData.CheckData = dtExcel.Rows[i]["检测结果"].ToString();
                            InData.Unit = dtExcel.Rows[i]["单位"].ToString();
                            InData.CheckTime =dtExcel.Rows[i]["检测时间"].ToString();
                            InData.CheckUnit = dtExcel.Rows[i]["被检单位"].ToString();//被检单位
                            InData.Result = dtExcel.Rows[i]["结论"].ToString();
                            InData.Save = "是";//是否已保存
                            InData.Instrument = dtExcel.Rows[i]["检测仪器"].ToString();
                            InData.Gettime = dtExcel.Rows[i]["采样时间"].ToString();
                            InData.Getplace = dtExcel.Rows[i]["采样地点"].ToString();
                            InData.detectunit = dtExcel.Rows[i]["检测单位"].ToString();
                            InData.Testbase = dtExcel.Rows[i]["检测依据"].ToString();
                            InData.LimitData = dtExcel.Rows[i]["标准值"].ToString();
                            InData.Tester = dtExcel.Rows[i]["检测员"].ToString();
                            InData.stockin = dtExcel.Rows[i]["进货数量"].ToString();
                            InData.sampleNum = dtExcel.Rows[i]["检测数量"].ToString();
                            InData.IsUpLoad = dtExcel.Rows[i]["已上传"].ToString();
                            InData.SampleCode = dtExcel.Rows[i]["样品编号"].ToString();
                            InData.SampleType = dtExcel.Rows[i]["样品种类"].ToString();
                            InData.SampleID = dtExcel.Rows[i]["样品ID"].ToString();
                            InData.IntrumentNum = dtExcel.Rows[i]["仪器编号"].ToString();
                            InData.Addr = dtExcel.Rows[i]["产地"].ToString();
                            InData.ProductDate = dtExcel.Rows[i]["生产日期"].ToString();
                            InData.Barcode = dtExcel.Rows[i]["条形码"].ToString();
                            InData.ProductCompany = dtExcel.Rows[i]["生产企业"].ToString();
                            InData.productAddr = dtExcel.Rows[i]["产地地址"].ToString();
                            InData.productUnit = dtExcel.Rows[i]["生产单位"].ToString();
                            InData.SendDate = dtExcel.Rows[i]["送检日期"].ToString();
                            InData.numUnit = dtExcel.Rows[i]["数量单位"].ToString();
                            InData.CheckUnitNature = dtExcel.Rows[i]["被检单位性质"].ToString();//被检单位性质
                            InData.TreatResult = dtExcel.Rows[i]["处理结果"].ToString();
                            InData.Xiguagndu = dtExcel.Rows[i]["吸光度"].ToString();
                            InData.HoleNumber = dtExcel.Rows[i]["通道号"].ToString();
                            InData.stockin = dtExcel.Rows[i]["进货数量"].ToString();//进货数量
                            InData.StallNum = dtExcel.Rows[i]["摊位号"].ToString();//摊位号

                            re = sql.LoadInData(InData, out err);
                            if (re == 1)
                            {
                                importNum = importNum + 1;
                            }
                        }
                    }

                    //MessageBox.Show("共成功导入" + n + "条数据", "操作提示");
                    MessageBox.Show("共成功导入" + importNum + "条数据", "操作提示");
                    //刷新数据
                    btnfind_Click(null, null);
                
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "导入数据失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "导入数据");
            }
        }
         
        //修改数据
        private void btnxiu_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDatasdd.SelectedRows.Count < 1)
                {
                    MessageBox.Show("请选择需要修改的数据", "提示");
                    return;
                }
                Global.AddItem = null;

                string[,] ar1 = new string[CheckDatasdd.SelectedRows.Count, 16];
                if (CheckDatasdd.SelectedRows.Count > 0)
                {
                    
                    string[,] modified = new string[CheckDatasdd.SelectedRows.Count, 22];
                    modified[0, 0] = CheckDatasdd.SelectedRows[0].Cells["SampleName"].Value.ToString();
                    modified[0, 1] = CheckDatasdd.SelectedRows[0].Cells["Checkitem"].Value.ToString();
                    modified[0, 2] = CheckDatasdd.SelectedRows[0].Cells["CheckData"].Value.ToString();
                    modified[0, 3] = CheckDatasdd.SelectedRows[0].Cells["Unit"].Value.ToString();
                    modified[0, 4] = CheckDatasdd.SelectedRows[0].Cells["TestBase"].Value.ToString();
                    modified[0, 5] = CheckDatasdd.SelectedRows[0].Cells["LimitData"].Value.ToString();
                    modified[0, 6] = CheckDatasdd.SelectedRows[0].Cells["Machine"].Value.ToString();
                    modified[0, 7] = CheckDatasdd.SelectedRows[0].Cells["Result"].Value.ToString();
                    modified[0, 8] = CheckDatasdd.SelectedRows[0].Cells["DetectUnit"].Value.ToString();
                    modified[0, 9] = CheckDatasdd.SelectedRows[0].Cells["SampleTime"].Value.ToString();
                    modified[0, 10] = CheckDatasdd.SelectedRows[0].Cells["SampleAddress"].Value.ToString();
                    modified[0, 11] = CheckDatasdd.SelectedRows[0].Cells["CheckUnit"].Value.ToString();
                    modified[0, 12] = CheckDatasdd.SelectedRows[0].Cells["CompanyNeture"].Value.ToString();
                    modified[0, 13] = CheckDatasdd.SelectedRows[0].Cells["Tester"].Value.ToString();
                    modified[0, 14] = CheckDatasdd.SelectedRows[0].Cells["CheckTime"].Value.ToString();
                    modified[0, 15] = CheckDatasdd.SelectedRows[0].Cells["SampleCategory"].Value.ToString();
                    modified[0, 16] = CheckDatasdd.SelectedRows[0].Cells["SampleNum"].Value.ToString();
                    modified[0, 17] = CheckDatasdd.SelectedRows[0].Cells["ProductPlace"].Value.ToString();
                    modified[0, 18] = CheckDatasdd.SelectedRows[0].Cells["ProduceUnit"].Value.ToString();
                    modified[0, 19] = CheckDatasdd.SelectedRows[0].Cells["ProductDatetime"].Value.ToString();
                    modified[0, 20] = CheckDatasdd.SelectedRows[0].Cells["SendTestDate"].Value.ToString();
                    modified[0, 21] = CheckDatasdd.SelectedRows[0].Cells["DoResult"].Value.ToString();

                    Global.AddItem = modified;
                    frmQueryModifi fqm = new frmQueryModifi();
                    fqm.ssid =int.Parse( CheckDatasdd.SelectedRows[0].Cells["ID"].Value.ToString().Trim());
                    DialogResult dr = fqm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        btnfind_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据修改失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据修改");
            }
        }
        //双击修改数据
        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Global.AddItem = null;
                string[,] modified = new string[CheckDatasdd.SelectedRows.Count, 22];
                modified[0, 0] = CheckDatasdd.SelectedRows[0].Cells["SampleName"].Value.ToString();
                modified[0, 1] = CheckDatasdd.SelectedRows[0].Cells["Checkitem"].Value.ToString();
                modified[0, 2] = CheckDatasdd.SelectedRows[0].Cells["CheckData"].Value.ToString();
                modified[0, 3] = CheckDatasdd.SelectedRows[0].Cells["Unit"].Value.ToString();
                modified[0, 4] = CheckDatasdd.SelectedRows[0].Cells["TestBase"].Value.ToString();
                modified[0, 5] = CheckDatasdd.SelectedRows[0].Cells["LimitData"].Value.ToString();
                modified[0, 6] = CheckDatasdd.SelectedRows[0].Cells["Machine"].Value.ToString();
                modified[0, 7] = CheckDatasdd.SelectedRows[0].Cells["Result"].Value.ToString();
                modified[0, 8] = CheckDatasdd.SelectedRows[0].Cells["DetectUnit"].Value.ToString();
                modified[0, 9] = CheckDatasdd.SelectedRows[0].Cells["SampleTime"].Value.ToString();
                modified[0, 10] = CheckDatasdd.SelectedRows[0].Cells["SampleAddress"].Value.ToString();
                modified[0, 11] = CheckDatasdd.SelectedRows[0].Cells["CheckUnit"].Value.ToString();
                modified[0, 12] = CheckDatasdd.SelectedRows[0].Cells["CompanyNeture"].Value.ToString();
                modified[0, 13] = CheckDatasdd.SelectedRows[0].Cells["Tester"].Value.ToString();
                modified[0, 14] = CheckDatasdd.SelectedRows[0].Cells["CheckTime"].Value.ToString();
                modified[0, 15] = CheckDatasdd.SelectedRows[0].Cells["SampleCategory"].Value.ToString();
                modified[0, 16] = CheckDatasdd.SelectedRows[0].Cells["SampleNum"].Value.ToString();
                modified[0, 17] = CheckDatasdd.SelectedRows[0].Cells["ProductPlace"].Value.ToString();
                modified[0, 18] = CheckDatasdd.SelectedRows[0].Cells["ProduceUnit"].Value.ToString();
                modified[0, 19] = CheckDatasdd.SelectedRows[0].Cells["ProductDatetime"].Value.ToString();
                modified[0, 20] = CheckDatasdd.SelectedRows[0].Cells["SendTestDate"].Value.ToString();
                modified[0, 21] = CheckDatasdd.SelectedRows[0].Cells["DoResult"].Value.ToString();

                Global.AddItem = modified;
                frmQueryModifi fqm = new frmQueryModifi();
                DialogResult dr = fqm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    btnfind_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据修改失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据修改");
            }
        }
        //单击修改数据
        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowNum=e.RowIndex;
        }

        //自动查询
        private void dTPEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dTStart.Value > dTPEnd.Value)
                {
                    MessageBox.Show("数据查询结束时间不能大于起始时间!", "系统提示");
                    return;
                }
                //DataReadTable.Clear();
                CheckDatasdd.DataSource = null;
                //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                //sb.Append("CheckTime>=#");
                //sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
                //sb.Append("#");
                //sb.Append(" AND CheckTime<=#");
                //sb.Append(dTPEnd.Value.Year.ToString());
                //sb.Append("/");
                //sb.Append(dTPEnd.Value.Month.ToString());
                //sb.Append("/");
                //sb.Append(dTPEnd.Value.Day.ToString());
                ////sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
                //sb.Append(" 23:59:59#");

                sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));

                if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
                }
                if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
                }
                if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
                {
                    sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
                }
                sb.Append(" ORDER BY ID");
                DataTable dtb = sql.GetDataTable(sb.ToString(), "");
                if (dtb != null)
                {
                    
                    if (disAll == true)
                    {
                        //CheckDatas.DataSource = DataReadTable;
                        CheckDatasdd.DataSource = dtb;
                        DataGridViewModefile();

                    }
                    else
                    {
                        dtInfo = dtb;
                        InitDataSet();
                    }

                    CmbgetItemName();
                    CmbgetSampleName();
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "日期查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"日期查询");
            }
        }
        
        //获取同一列不同的检测项目
        //以下方法是实现将界面上的dataGridView1控件中第一列的值填充到界面上的comboBox控件中的，，重复的值是不会添加的。
        //在窗体的启动事件中调用此方法就可以了，如果是取其它列的值，修改“Cells[0]”的值就可以了；      
      private void CmbgetItemName()
      {
          List<string> lsName = new List<string>();        
          lsName.Add("请选择...");
          for(int i=0;i<this.CheckDatasdd.Rows.Count;i++)
          {
              string name = this.CheckDatasdd.Rows[i].Cells["Checkitem"].Value.ToString();
              if(lsName.Contains(name))
              {
                  continue;
              }
              else
              {
                  lsName.Add(name);
              }
           }

          this.cmbTestItem.DataSource=lsName;
       }
       //获取样品名称添加到combox中
      private void CmbgetSampleName()
      {
          List<string> lsName = new List<string>();
          lsName.Add("请选择...");
          for (int i = 0; i < this.CheckDatasdd.Rows.Count; i++)
          {
              string name = this.CheckDatasdd.Rows[i].Cells["SampleName"].Value.ToString();
              if (lsName.Contains(name))
              {
                  continue;
              }
              else
              {
                  lsName.Add(name);
              }
          }
      
          this.cmbSample.DataSource = lsName;
      }
      /// <summary>
      /// 选择结论单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbResult_SelectedValueChanged(object sender, EventArgs e)
      {
          try
          {
              //DataReadTable.Clear();
              CheckDatasdd.DataSource = null;
              //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
              StringBuilder sb = new StringBuilder();
              sb.Clear();
              //sb.Append("CheckTime>=#");
              //sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
              //sb.Append("#");
              //sb.Append(" AND CheckTime<=#");
              //sb.Append(dTPEnd.Value.Year.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Month.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Day.ToString());
              ////sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
              //sb.Append(" 23:59:59#");
              sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
              if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
              }
              sb.Append(" ORDER BY ID");
              DataTable dtb = sql.GetDataTable(sb.ToString(), "");
              if (dtb != null)
              {
                 
                  if (disAll == true)
                  {
                      //CheckDatas.DataSource = DataReadTable;
                      CheckDatasdd.DataSource = dtb;
                      DataGridViewModefile();
                  }
                  else
                  {
                      dtInfo = dtb;
                      InitDataSet();
                  }

                 
                  CmbgetItemName();
                  CmbgetSampleName();
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "结论查询失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message ,"结论查询");
          }
      }
      /// <summary>
      /// 选择样品名称单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbSample_SelectedValueChanged(object sender, EventArgs e)
      {
          try
          {
              string selsamp = cmbResult.Text.Trim();
              //DataReadTable.Clear();
              CheckDatasdd.DataSource = null;
              //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
              StringBuilder sb = new StringBuilder();
              sb.Clear();
              //sb.Append("CheckTime>=#");
              //sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
              //sb.Append("#");
              //sb.Append(" AND CheckTime<=#");
              //sb.Append(dTPEnd.Value.Year.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Month.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Day.ToString());
              ////sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
              //sb.Append(" 23:59:59#");

              sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));

              if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
              }
              sb.Append(" ORDER BY ID");
              DataTable dtb = sql.GetDataTable(sb.ToString(), "");
              if (dtb != null && dtb.Rows.Count > 0)
              {
                  isup = new string[dtb.Rows.Count];
                 
                  if (disAll == true)
                  {
                      //CheckDatas.DataSource = DataReadTable;
                      CheckDatasdd.DataSource = dtb;
                      DataGridViewModefile();
                  }
                  else
                  {
                      dtInfo = dtb;
                      InitDataSet();
                  }
                 
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "样品查询失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"样品查询");
          }
      }
      
      /// <summary>
      /// 选择检测项目单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbTestItem_SelectedValueChanged(object sender, EventArgs e)
      {
          try
          {
              string sel = cmbTestItem.Text.Trim();
              //DataReadTable.Clear();
              CheckDatasdd.DataSource = null;
              //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
              StringBuilder sb = new StringBuilder();
              sb.Clear();
              //sb.Append("CheckTime>=#");
              //sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
              //sb.Append("#");
              //sb.Append(" AND CheckTime<=#");
              //sb.Append(dTPEnd.Value.Year.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Month.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Day.ToString());
              ////sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
              //sb.Append(" 23:59:59#");
              sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));

              if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
              }
              sb.Append(" ORDER BY ID");
              DataTable dtb = sql.GetDataTable(sb.ToString(), "");
              if (dtb != null)
              {
                  if (disAll == true)
                  {
                      //CheckDatas.DataSource = DataReadTable;
                      CheckDatasdd.DataSource = dtb;
                      DataGridViewModefile();
                  }
                  else
                  {
                      dtInfo = dtb;
                      InitDataSet();
                  }
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "检测项目失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"检测项目查询");
          }
      }
      /// <summary>
      /// 查询后数据在datagridview上是分页显示  false
      /// </summary>
      private bool disAll = false;
           /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      private void tspbtndisplay_Click(object sender, EventArgs e)
      {
          try
          {
              if (tspbtndisplay.Text == "全部显示")
              {
                  tspbtndisplay.Text = "分页显示";
                  disAll = true;
                  toolStripLabel3.Visible = false;
                  toolStripItemCount.Visible = false;
                  toolStripLabel5.Visible = false;
                  toolStripItems6.Visible = false;
                  tspallpage.Visible = false;
                  tsplabpage.Visible = false;
                  tspbtnnext.Visible = false;
                  tspbtnuppage.Visible = false;
                  searchdatabase();

              }
              else
              {
                  tspbtndisplay.Text = "全部显示";
                  disAll = false;
                  toolStripLabel3.Visible = true;
                  toolStripItemCount.Visible = true;
                  toolStripLabel5.Visible = true;
                  toolStripItems6.Visible = true;
                  tspallpage.Visible = true;
                  tsplabpage.Visible = true;
                  tspbtnnext.Visible = true;
                  tspbtnuppage.Visible = true;
                  searchdatabase();
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "分页显示失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message ,"分页显示");
          }
      }
      //下一页
      private void tspbtnnext_Click(object sender, EventArgs e)
      {
          try
          {
              pageCurrent++;
              if (pageCurrent > pageCount)
              {
                  MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                  return;
              }
              else
              {
                  nCurrent = pageSize * (pageCurrent - 1);
              }
              LoadData();
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "下一页失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message ,"下一页");
          }
      }
      //上一页
      private void tspbtnuppage_Click(object sender, EventArgs e)
      {
          try
          {
              pageCurrent--;
              if (pageCurrent <= 0)
              {
                  MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                  return;
              }
              else
              {
                  nCurrent = pageSize * (pageCurrent - 1);
              }

              LoadData();
          }
          catch(Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "下一页失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"上一页");
          }
      }
      /// <summary>
      /// 开始时间查询
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      public  void dTStart_ValueChanged(object sender, EventArgs e)
      {
          try
          {
              if (dTStart.Value > dTPEnd.Value)
              {
                  MessageBox.Show("数据查询起始时间不能大于结束时间!","系统提示");
                  return;
              }

              //DataReadTable.Clear();
              CheckDatasdd.DataSource = null;
              //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
              StringBuilder sb = new StringBuilder();
              sb.Clear();
              //sb.Append("CheckTime>=#");
              //sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
              //sb.Append("#");
              //sb.Append(" AND CheckTime<=#");
              //sb.Append(dTPEnd.Value.Year.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Month.ToString());
              //sb.Append("/");
              //sb.Append(dTPEnd.Value.Day.ToString());
              //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
              //sb.Append(" 23:59:59#");
              sb.AppendFormat("CheckTime between '{0}' and '{1}' ", dTStart.Value.ToString("yyyy-MM-dd 00:00:00"), dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
              if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND Checkitem='{0}'", cmbTestItem.Text.Trim());
              }
              if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
              {
                  sb.AppendFormat(" AND SampleName='{0}'", cmbSample.Text.Trim());
              }
              sb.Append(" ORDER BY ID");
              DataTable dtb = sql.GetDataTable(sb.ToString(), "");
              if (dtb != null)
              {
                  
                  if (disAll == true)
                  {
                      //CheckDatas.DataSource = DataReadTable;
                      CheckDatasdd.DataSource = dtb;
                      DataGridViewModefile();
                      if (CheckDatasdd.Rows.Count > 0)
                      {
                          CheckDatasdd.Columns["ID"].Visible = false;
                      }
                      
                  }
                  else
                  {
                      dtInfo = dtb;
                      InitDataSet();
                      if (CheckDatasdd.Rows.Count > 0)
                      {
                          CheckDatasdd.Columns["ID"].Visible = false;
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "开始时间查询失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"开始时间查询");
          }
      }
      /// <summary>
      /// 删除选中数据
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnDelete_Click(object sender, EventArgs e)
      {
          try
          {
              if (CheckDatasdd.SelectedRows.Count < 1)
              {
                  MessageBox.Show("请选择需要删除的记录", "提示");
                  return;
              }
              DialogResult dr = MessageBox.Show("是否删除选中的数据", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
              if (dr == DialogResult.Yes)
              {
                  string err = string.Empty;
                  StringBuilder sb = new StringBuilder();
                  for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                  {
                      sb.Clear();
                      sb.AppendFormat("CheckResult where ID={0} ", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());

                      sql.Delete(sb.ToString(), out err);

                  }
                  MessageBox.Show("删除数据成功", "提示");
                  //刷新数据
                  btnfind_Click(null, null);
              }
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "删除数据失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"删除数据");
          }
      }
     

    /// <summary>
    /// 数据上传
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
      private void btnUpload_Click(object sender, EventArgs e)
      {
          try
          {
              btnUpload.Enabled = false;
              if (Global.linkNet() == false)
              {
                  MessageBox.Show("无法连接到互联网，请检查网络连接！", "系统提示");
                  btnUpload.Enabled = true;
                  return;
              }

              int IsSuccess = 0;
              string err = "";

              if (CheckDatasdd.SelectedRows.Count < 1)
              {
                  MessageBox.Show("请选择需要上传的记录", "操作提示");
                  btnUpload.Enabled = true ;
                  return;
              }

              if (Global.ServerAdd.Length == 0)
              {
                  MessageBox.Show("服务器地址不能为空", "提示");
                  btnUpload.Enabled = true;
                  return;
              }
              if (Global.ServerName.Length == 0)
              {
                  MessageBox.Show("用户名不能为空", "提示");
                  btnUpload.Enabled = true;
                  return;
              }
              if (Global.ServerPassword.Length == 0)
              {
                  MessageBox.Show("密码不能为空", "提示");
                  btnUpload.Enabled = true;
                  return;
              }
              string errstr="";
              int isupdata = 0;
              if (Global.Platform == "DYBus")//达元快检车平台
              {
                  string cregid = "";
                  string ogranco = "";

                  //WebReference.PutService putServer = new WebReference.PutService();
                  //BeiHaiUploadData bupdata = new BeiHaiUploadData();

                  for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                  {
                      sb.Length = 0;
                      sb.AppendFormat("ID={0}", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());
                      dt = sql.GetResult(sb.ToString(), "", 1, out err);
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          if (dt.Rows[0]["IsUpload"].ToString() == "是")//不允许重传
                          {
                              //MessageBox.Show("本条记录已上传","提示");
                              isupdata = isupdata + 1;
                              continue;
                          }

                          //查询被检单位
                          DataTable dtcompany = sql.GetExamedUnit("regName='" + CheckDatasdd.SelectedRows[i].Cells["CheckUnit"].Value.ToString() + "'", "", out err);
                          if (dtcompany != null && dtcompany.Rows.Count > 0)
                          {
                              cregid = dtcompany.Rows[0]["regId"].ToString();
                              ogranco = dtcompany.Rows[0]["organizationCode"].ToString();
                          }

                          string samplecode = "";
                          //查询样品编号
                          DataTable ds = sql.GetSampleDetail("foodName='" + CheckDatasdd.SelectedRows[i].Cells["SampleName"].Value.ToString().Trim() + "'", "", out err);
                          //DataTable ds = sql.GetItemStandard("sampleName='" + CheckDatas.SelectedRows[i].Cells[0].Value.ToString() + "' and itemName='"+
                          //    CheckDatas.SelectedRows[i].Cells[1].Value.ToString()+"'", "", out err);
                          if (ds != null && ds.Rows.Count > 0)
                          {
                              samplecode = ds.Rows[0]["foodCode"].ToString();
                          }
                          clsUpLoadCheckData upDatas = new clsUpLoadCheckData();
                          upDatas.result = new List<clsUpLoadCheckData.results>();
                          clsUpLoadCheckData.results model = new clsUpLoadCheckData.results();
                          model.sysCode = Global.GUID();
                          model.foodName = CheckDatasdd.SelectedRows[i].Cells["SampleName"].Value.ToString();
                          model.foodCode = samplecode == "" ? "0000100310002" : samplecode;
                          model.foodType = CheckDatasdd.SelectedRows[i].Cells["SampleCategory"].Value.ToString().Trim() == "" ? "蔬菜" : CheckDatasdd.SelectedRows[i].Cells["SampleCategory"].Value.ToString().Trim();
                          model.sampleNo = "";
                          model.planCode = "";
                          model.checkPId = Global.pointID;
                          DateTime dtt = DateTime.Parse(CheckDatasdd.SelectedRows[i].Cells["CheckTime"].Value.ToString().Replace("/", "-"));
                          model.checkDate = dtt.ToString("yyyy-MM-dd HH:mm:ss"); //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                          model.checkAccord = CheckDatasdd.SelectedRows[i].Cells["TestBase"].Value.ToString();
                          model.checkItemName = CheckDatasdd.SelectedRows[i].Cells["Checkitem"].Value.ToString();
                          model.checkDevice = CheckDatasdd.SelectedRows[i].Cells["Machine"].Value.ToString();
                          model.regId = cregid;
                          model.ckcName = CheckDatasdd.SelectedRows[i].Cells["CheckUnit"].Value.ToString();
                          model.cdId = "";
                          model.ckcCode = ogranco;
                          model.checkResult = CheckDatasdd.SelectedRows[i].Cells["CheckData"].Value.ToString();
                          model.checkUnit = CheckDatasdd.SelectedRows[i].Cells["DetectUnit"].Value.ToString();
                          model.limitValue = CheckDatasdd.SelectedRows[i].Cells["LimitData"].Value.ToString();
                          model.checkConclusion = (CheckDatasdd.SelectedRows[i].Cells["Result"].Value.ToString() == "阴性" || CheckDatasdd.SelectedRows[i].Cells["Result"].Value.ToString() == "合格") ? "合格" : "不合格";
                          model.dataStatus = 1;
                          model.dataSource = 0;
                          model.checkUser = CheckDatasdd.SelectedRows[i].Cells["Tester"].Value.ToString();
                          model.dataUploadUser = CheckDatasdd.SelectedRows[i].Cells["Tester"].Value.ToString();
                          model.deviceCompany = "广东达元";
                          model.deviceModel = CheckDatasdd.SelectedRows[i].Cells["MachineNum"].Value.ToString();
                          upDatas.result.Add(model);
                          string json = JsonHelper.EntityToJson(upDatas);
                          string rtn = InterfaceHelper.UploadChkData(json, out err);
                          ResultMsg msgResult = null;
                          msgResult = JsonHelper.JsonToEntity<ResultMsg>(rtn);
                          if (msgResult.resultCode.Equals("success1"))
                          {
                              IsSuccess = IsSuccess + 1;
                              sql.SetUploadResult(CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString(), out err);
                              CheckDatasdd.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Green;
                          }

                          //写日记
                          string fileName = "Send" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                      }
                  }
                  MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据！ 共" + isupdata + "条数据已传！");
              }
              else if (Global.Platform == "DYKJFW")//达元快检服务平台
              {
                
                  string ruld=  QuickInspectServing.GetServiceURL(Global.ServerAdd, 6);
                  for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                  {
                      sb.Length = 0;
                      sb.AppendFormat("ID={0}", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());
                      dt = sql.GetResult(sb.ToString(), "", 1, out err);
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          string rtn = QuickInspectServing.UpLoadData(dt, ruld, Global.ServerName, Global.ServerPassword);
                          if (rtn.Contains("msg") || rtn.Contains("success"))
                          {
                              ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                              if (Jresult.msg == "操作成功" && Jresult.success == true)
                              {
                                  IsSuccess = IsSuccess + 1;
                                  sql.SetUploadResult(CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString(), out err);
                              }
                              else
                              {
                                  errstr = errstr + Jresult.msg;
                              }
                          }
                          else
                          {
                              errstr = errstr + rtn;
                          }
                      }
                  }
                  if (errstr == "")
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据！");
                  }
                  else
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据；共" + isupdata + "条数据已传！ 提示信息：" + errstr);
                  }
              }
              else
              {
                  for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                  {
                      sb.Length = 0;
                      sb.AppendFormat("ID={0}", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());
                      dt = sql.GetResult(sb.ToString(), "", 1, out err);
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          if (dt.Rows[0]["IsUpload"].ToString() == "是")//不允许重传
                          {
                              isupdata = isupdata + 1;
                              continue;
                          }
                          string rtn = ZYUpData.UpData(dt, Global.ServerAdd, Global.ServerName, Global.ServerPassword);
                          if (rtn.Contains("resultCode"))
                          {
                              rtnResult results = JsonHelper.JsonToEntity<rtnResult>(rtn);
                              if (results != null)
                              {
                                  if (results.resultCode == "1")
                                  {
                                      IsSuccess = IsSuccess + 1;
                                      sql.SetUploadResult(CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString(), out err);
                                  }
                                  else
                                  {
                                      errstr = errstr + results.resultMsg;
                                  }
                              }
                              else
                              {
                                  errstr = errstr + rtn;
                              }
                          }
                          else
                          {
                              errstr = errstr + rtn;
                          }
                      }
                  }
                  if (errstr == "")
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据！");
                  }
                  else
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据；共" + isupdata + "条数据已传！ 提示信息：" + errstr);
                  }
              }
              
              btnfind_Click(null ,null);
              btnUpload.Enabled = true;
          }
          catch (Exception ex)
          {
              dy.savediary(DateTime.Now.ToString(), "数据上传失败：" + ex.Message, "错误");
              MessageBox.Show(ex.Message,"数据上传");
              btnUpload.Enabled = true;
          }
         // if (ConfigurationManager.AppSettings["InterfaceManufacture"] == "DY")
         // {
         //     int upok = 0;
         //     string cregid = "";
         //     string ogranco = "";
         //     try
         //     {
         //         for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
         //         {
         //             //查询被检单位
         //             DataTable dtcompany = sql.GetExamedUnit("regName='" + CheckDatas.SelectedRows[i].Cells[11].Value.ToString()+"'", "", out err);
         //             if (dtcompany!=null && dtcompany.Rows.Count > 0)
         //             {
         //                 cregid = dtcompany.Rows[0][1].ToString();
         //                 ogranco = dtcompany.Rows[0][5].ToString();
         //             }
         //             //查询数据是否已上传
         //             StringBuilder sb = new StringBuilder();
         //             sb.Append("CheckTime=#");
         //             sb.Append(CheckDatas.SelectedRows[i].Cells[13].Value.ToString());
         //             sb.Append("# and CheckData='");
         //             sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
         //             sb.Append("' and Machine='");
         //             sb.Append(CheckDatas.SelectedRows[i].Cells[6].Value.ToString());
         //             sb.Append("'");
         //             sb.Append(" and SampleName='");
         //             sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
         //             sb.Append("' and Checkitem='");
         //             sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
         //             sb.Append("'");

         //             DataTable dup = sql.GetSave(sb.ToString(), out err);
         //             if (dup !=null && dup.Rows.Count>0)
         //             {
         //                 if (dup.Rows[0][0].ToString() == "是")
         //                 {
         //                     //MessageBox.Show("本条记录已上传");
         //                     continue ;
         //                 }
         //             }
         //             string samplecode = "";
         //             //查询样品编号
         //             DataTable ds = sql.GetSampleDetail("foodName='" + CheckDatas.Rows[i].Cells[0].Value.ToString().Trim() + "'", "", out err);
         //             //DataTable ds = sql.GetItemStandard("sampleName='" + CheckDatas.SelectedRows[i].Cells[0].Value.ToString() + "' and itemName='"+
         //             //    CheckDatas.SelectedRows[i].Cells[1].Value.ToString()+"'", "", out err);
         //             if (ds != null && ds.Rows.Count > 0)
         //             {
         //                 samplecode = ds.Rows[0][2].ToString();
         //             }
         //             clsUpLoadCheckData upDatas = new clsUpLoadCheckData();
         //             upDatas.result = new List<clsUpLoadCheckData.results>();
         //             clsUpLoadCheckData.results model = new clsUpLoadCheckData.results();
         //             model.sysCode = Global.GUID();
         //             model.foodName = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
         //             model.foodCode = samplecode == "" ? "0000100310002" : samplecode;
         //             model.foodType = CheckDatas.SelectedRows[i].Cells[15].Value.ToString().Trim() == "" ? "蔬菜" : CheckDatas.SelectedRows[i].Cells[15].Value.ToString().Trim();
         //             model.sampleNo = "";
         //             model.planCode = "";
         //             model.checkPId = Global.pointID;
         //             DateTime dt = DateTime.Parse(CheckDatas.SelectedRows[i].Cells[13].Value.ToString().Replace("/", "-"));
         //             model.checkDate = dt.ToString("yyyy-MM-dd HH:mm:ss"); //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

         //             model.checkAccord = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
         //             model.checkItemName = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
         //             model.checkDevice = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
         //             model.regId = cregid;
         //             model.ckcName = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();
         //             model.cdId = "";
         //             model.ckcCode = ogranco;
         //             model.checkResult = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
         //             model.checkUnit = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
         //             model.limitValue = "<" + CheckDatas.SelectedRows[i].Cells[5].Value.ToString() + CheckDatas.SelectedRows[i].Cells[3].Value.ToString(); ;
         //             model.checkConclusion = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
         //             model.dataStatus = 1;
         //             model.dataSource = 0;
         //             model.checkUser = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
         //             model.dataUploadUser = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
         //             model.deviceCompany = "广东达元";
         //             model.deviceModel = CheckDatas.SelectedRows[i].Cells[6].Value.ToString().Substring(0,7);
         //             upDatas.result.Add(model);
         //             string json = JsonHelper.EntityToJson(upDatas);
         //             string rtn = InterfaceHelper.UploadChkData(json, out err);
         //             ResultMsg msgResult = null;
         //             msgResult = JsonHelper.JsonToEntity<ResultMsg>(rtn);
         //             if (msgResult.resultCode.Equals("success1"))
         //             {
         //                 upok = upok + 1;
         //                 CheckDatas.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Green;//上传成功变绿色
         //                 clsUpdateData ud = new clsUpdateData();
         //                 ud.result = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
         //                 ud.ChkTime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
         //                 ud.intrument = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
         //                 ud.ChkSample = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
         //                 ud.Chkxiangmu = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
         //                 sql.SetUpLoadData(ud, out err);
         //             }
         //         }
         //         MessageBox.Show("共成功能够上传" + upok + "条数据", "数据上传");
         //     }
         //     catch (Exception ex)
         //     {
         //         MessageBox.Show(ex.Message,"Error");
         //     }
         // }
         // else if (ConfigurationManager.AppSettings["InterfaceManufacture"] == "kerun")
         // { 
         ////山东科润平台接口
         //     for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
         //     {
         //         //边查询边上传
         //         StringBuilder sbd=new StringBuilder();

         //         sbd.Append("Checkitem='");
         //         sbd.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
         //         sbd.Append("' and SampleName='");
         //         sbd.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
         //         sbd.Append("' and CheckData='");
         //         sbd.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
         //         sbd.Append("' and CheckTime=#");
         //         sbd.Append(DateTime.Parse(CheckDatas.SelectedRows[i].Cells[13].Value.ToString()));
         //         sbd.Append("#");
         //         //查询界面上没有的数据
         //         DataTable rt = sql.GetDataTable(sbd.ToString(), "");
         //         udata.checkunit = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();//被检单位
         //         udata.checkitem = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();//检测项目
         //         udata.ttime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();//检测时间
         //         udata.chkdata = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();//检测数值
         //         udata.unit = System.Web.HttpUtility.UrlEncode(rt.Rows[0][4].ToString()); //System.Web.HttpUtility.UrlEncode("%");//数值单位 对非法字符进行url转换
                 
         //         if (CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "阴性" || CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "合格")
         //         {
         //             udata.Conclusion = "合格";// 检测结论
         //         }
         //         else
         //         {
         //             udata.Conclusion = "不合格";
         //         }
         //         udata.samplenumber = rt.Rows[0][18].ToString();//样品编号
         //         udata.samplename = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();//样品名称
         //         udata.sampleOrigin = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();//样品产地
         //         udata.testbase = rt.Rows[0][12].ToString();//检测依据
         //         udata.ChkMachineNum = rt.Rows[0][20].ToString();//设备编号
         //         udata.standvalue = rt.Rows[0][13].ToString();//标准值
         //         udata.chker = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
         //         udata.uptype = int.Parse(Global.UploadType);
         //         udata.shebeiID = Global.IntrumentNum;

         //         string rd = KeRunUpData.UpData(udata);

         //         JavaScriptSerializer jsup = new JavaScriptSerializer();
         //         KeRunUpData.ReturnInfo retu = jsup.Deserialize<KeRunUpData.ReturnInfo>(rd); //将json数据转化为对象类型并赋值给list
                  
         //         if (retu.status =="1" && retu.info == "success")
         //         {
         //             CheckDatas.SelectedRows[i].DefaultCellStyle.BackColor = Color.Green;//上传成功变绿色
         //             iok = iok + 1;
         //             clsUpdateData ud = new clsUpdateData();
         //             ud.result = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
         //             ud.ChkTime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
         //             sql.SetUpLoadData(ud, out err);
         //         }
         //         else
         //         {
         //             ino = ino + 1;
         //             ErrInfo = ErrInfo +"("+ ino+")" + retu.info;
         //         }
         //     }
         //     MessageBox.Show("共成功上传" + iok + "条数据；失败" + ino + "条数据,失败原因：" + ErrInfo, "数据上传");

         // }
         // else if (ConfigurationManager.AppSettings["InterfaceManufacture"] == "taiyang")
         // {
         // //湖北泰扬接口
         //     //查询数据上传的地址
         //     try
         //     {
         //         Global.ServerAdd = ConfigurationManager.AppSettings["ServerAddr"];
         //         Global.ServerName = ConfigurationManager.AppSettings["ServerName"];
         //         Global.ServerPassword = ConfigurationManager.AppSettings["ServerPassword"];
         //         if (Global.ServerAdd.Trim().Length == 0)
         //         {
         //             MessageBox.Show("服务器地址不能为空", "提示");
         //             return;
         //         }
         //         if (Global.ServerName.Trim().Length == 0)
         //         {
         //             MessageBox.Show("登录用户名不能为空", "提示");
         //             return;
         //         }
         //         if (Global.ServerPassword.Trim().Length == 0)
         //         {
         //             MessageBox.Show("登录密码不能为空", "提示");
         //             return;
         //         }
         //         if (CheckDatas.SelectedRows.Count < 1)
         //         {
         //             MessageBox.Show("请选择需要上传的记录", "提示");
         //             return;
         //         }
         //         //string err = string.Empty;
         //         //StringBuilder sb = new StringBuilder();

         //         //sb.Append(" where isselect='是'");

         //         //DataTable dt = sql.GetServer(sb.ToString(), out err);
         //         //if (dt != null)
         //         //{
         //         //    Global.ServerAdd = dt.Rows[0][1].ToString();
         //         //    Global.ServerName = dt.Rows[0][2].ToString();
         //         //    Global.ServerPassword = dt.Rows[0][3].ToString();
         //         //}
         //         //登录
         //         string rt = TYUpData.Logon();

         //         JavaScriptSerializer js = new JavaScriptSerializer();
         //         TYUpData.login list = js.Deserialize<TYUpData.login>(rt);    //将json数据转化为对象类型并赋值给list
         //         Global.CompanyCode = list.qycode;
         //         Global.UserCode = list.userid;
         //         string fandata = list.isSucess;
         //         if (fandata != "")
         //         {
         //             MessageBox.Show(fandata, "Error");
         //             return;
         //         }

         //         for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
         //         {
         //             udata.shuliang = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
         //             udata.ttime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
         //             udata.chker = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
         //             udata.hegelv = "100";
         //             udata.duixiang = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
         //             udata.chkdata = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
         //             udata.companyCode = Global.CompanyCode;
         //             if (CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "阴性" || CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "合格")
         //             {
         //                 udata.Conclusion = "合格";
         //             }
         //             else
         //             {
         //                 udata.Conclusion = "不合格";
         //             }

         //             string rd = TYUpData.UpData(udata);

         //             JavaScriptSerializer jsup = new JavaScriptSerializer();
         //             TYUpData.Upload retu = jsup.Deserialize<TYUpData.Upload>(rd);    //将json数据转化为对象类型并赋值给list
         //             if (retu.isSucess == "上传成功")
         //             {
         //                 CheckDatas.SelectedRows[i].DefaultCellStyle.BackColor = Color.Green;//上传成功变绿色
         //                 iok = iok + 1;
         //                 clsUpdateData ud = new clsUpdateData();
         //                 ud.result = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
         //                 ud.ChkTime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
         //                 sql.SetUpLoadData(ud, out err);
         //             }
         //             else
         //             {
         //                 ino = ino + 1;
         //             }
         //         }
         //         MessageBox.Show("共成功上传" + iok + "条数据，失败" + ino + "条数据", "数据上传");
         //     }
         //     catch (Exception ex)
         //     {
         //         MessageBox.Show(ex.Message, "Error");
         //     }
         // }
      }
       
      private void CheckDatasdd_Scroll(object sender, ScrollEventArgs e)
      {
          CheckDatasdd.Refresh();
      }

      private void CheckDatasdd_Paint(object sender, PaintEventArgs e)
      {
          if (CheckDatasdd != null && CheckDatasdd.Rows.Count > 0)
          {
              for (int i = 0; i < CheckDatasdd.Rows.Count; i++)
              {
                  if (CheckDatasdd.Rows[i].Cells["Result"].Value.ToString() == "阳性" || CheckDatasdd.Rows[i].Cells["Result"].Value.ToString() == "不合格")
                  {
                      if (CheckDatasdd.Rows[i].Cells["IsUpload"].Value.ToString() == "是")
                      {
                          CheckDatasdd.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                      }
                      else
                      {
                          CheckDatasdd.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                      }
                  }
                  else
                  {
                      if (CheckDatasdd.Rows[i].Cells["IsUpload"].Value.ToString() == "是")
                      {
                          CheckDatasdd.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                      }
                  }

              }
          }
      }
    }
}

