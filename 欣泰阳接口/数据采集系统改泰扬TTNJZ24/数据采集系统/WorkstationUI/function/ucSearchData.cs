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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.IO;
using WorkstationModel.Model;
using WorkstationModel.UpData;
using WorkstationDAL.Basic;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Configuration;


namespace WorkstationUI.function
{
    public partial class ucSearchData : UserControl
    {
        #region WinForm窗体重绘
        const int WM_NCHITTEST = 0x0084;
        const int HT_LEFT = 10;
        const int HT_RIGHT = 11;
        const int HT_TOP = 12;
        const int HT_TOPLEFT = 13;
        const int HT_TOPRIGHT = 14;
        const int HT_BOTTOM = 15;
        const int HT_BOTTOMLEFT = 16;
        const int HT_BOTTOMRIGHT = 17;
        const int HT_CAPTION = 2;
        #endregion
        clsSetSqlData sql = new clsSetSqlData();
        public DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private bool iscreate=false ;
        private int rowNum=0;
        private int  irow=0;
        private Word.Application G_wa;//定义Word应用程序字段
        private object G_missing = //定义G_missing字段并添加引用
            System.Reflection.Missing.Value;
        private Excel.Application G_ea;//定义Excel应用程序字段
        private DataTable distable=null ;
        private string path = Environment.CurrentDirectory;

        public ucSearchData()
        {
            InitializeComponent();
        }
        private void disTable()
        {
            if (!iscreate)
            {
                distable = new DataTable("diatable");//去掉Static
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测项目";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检定员";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                distable.Columns.Add(dataCol);

                iscreate = true;
            }
        }
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkResult");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测编号";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检定员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                DataReadTable.Columns.Add(dataCol);
                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "进货数量";
                //DataReadTable.Columns.Add(dataCol);
                //m_IsCreatedDataTable = true;

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样数量";
                //DataReadTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;

            }
 
        }
        //查询
        private void btnfind_Click(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;          
            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
            sb.Append(dTPEnd.Value.Year.ToString());
            sb.Append("/");
            sb.Append(dTPEnd.Value.Month.ToString());
            sb.Append("/");
            sb.Append(dTPEnd.Value.Day.ToString());
            //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat(" AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim()!="请选择...")
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
                for (int i = 0; i < dtb.Rows.Count;i++ )
                {
                    TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                      dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                      dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
                }
                if (disAll == true)
                {
                    CheckDatas.DataSource = DataReadTable;
                }
                else
                {
                    dtInfo = dtb;
                    InitDataSet();
                }
                
                CheckDatas.Columns[8].Width = 160;
                CheckDatas.Columns[12].Width = 160;
                for (int i = 0; i < CheckDatas.Rows.Count;i++ )
                {
                    if (CheckDatas.Rows[i].Cells[7].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells[7].Value.ToString() == "阳性")
                    {
                        CheckDatas.Rows[i].DefaultCellStyle.ForeColor  = Color.Red;
                    }                  
                }
                    //heckDatas.Rows[2].DefaultCellStyle.BackColor = Color.Red;
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
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行

            DataTable dtTemp = dtInfo.Clone();   //克隆DataTable结构框架

            if (pageCurrent == pageCount)
                nEndPos = nMax;
            else
                nEndPos = pageSize * pageCurrent;

            nStartPos = nCurrent;

            tspallpage.Text = pageCount.ToString ();
            tsplabpage.Text = Convert.ToString(pageCurrent);
            //lblPageCount.Text = pageCount.ToString();
            //txtCurrentPage.Text = Convert.ToString(pageCurrent);
            if (dtInfo.Rows.Count < 1)
            {
                CheckDatas.DataSource = DataReadTable;
                return;
            }
            //从元数据源复制记录行
            for (int i = nStartPos; i < nEndPos; i++)
            {
                dtTemp.ImportRow(dtInfo.Rows[i]);
                nCurrent++;
            }
            //bdsInfo.DataSource = dtTemp;
            //bdnInfo.BindingSource = bdsInfo;
            distable.Clear();
            for (int j = 0; j < dtTemp.Rows.Count; j++)
            {
                disTableNewRow(dtTemp.Rows[j][2].ToString(), dtTemp.Rows[j][1].ToString(), dtTemp.Rows[j][3].ToString(), dtTemp.Rows[j][4].ToString(),
                       dtTemp.Rows[j][12].ToString(), dtTemp.Rows[j][13].ToString(), dtTemp.Rows[j][8].ToString(), dtTemp.Rows[j][7].ToString(), dtTemp.Rows[j][9].ToString(), dtTemp.Rows[j][10].ToString(),
                       dtTemp.Rows[j][6].ToString(), dtTemp.Rows[j][14].ToString(), dtTemp.Rows[j][5].ToString(), dtTemp.Rows[j][11].ToString(),dtTemp.Rows[j][16].ToString());               
            }
            CheckDatas.DataSource = distable;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells[7].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells[7].Value.ToString() == "阳性")
                {
                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            //CheckDatas.Columns.dtTemp distable
        }

        int pageSize = 0;     //每页显示行数
        int nMax = 0;         //总记录数
        int pageCount = 0;    //页数＝总记录数/每页显示行数
        int pageCurrent = 0;   //当前页号
        int nCurrent = 0;      //当前记录行
        //DataSet ds = new DataSet();
        private DataTable dtInfo = new DataTable();
        //查询数据显示
        public  void searchdatabase()
        {
            DataReadTable.Clear();//清除缓存的数据
            CheckDatas.DataSource = null;          
            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            //DateTime from =dTStart.Value;
            //DateTime to = dTPEnd.Value;
            //string _start = dTStart.Value.ToString("yyyy-MM-dd 00:00:00");
            //string _end = dTPEnd.Value.ToString("yyyy-MM-dd 23:59:59");           
            //sb.AppendFormat(" CheckTime between '{0}' and  '{1}' ", _start, _end);
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");

            sb.Append(DateTime.Now.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim()!="请选择...")
            {
                sb.AppendFormat("AND Result='%{0}%'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim()!= "请选择...")
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
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                   
                    TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                       dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
                }
                if (disAll == true)
                {
                    CheckDatas.DataSource = DataReadTable;
                }
                else
                {
                    dtInfo = dtb;
                    InitDataSet();
                }
                CheckDatas.Columns[8].Width = 160;
                CheckDatas.Columns[12].Width = 160;
                if (CheckDatas.Rows.Count > 0)
                {                 
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells[7].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[7].Value.ToString() == "不合格")
                        {                      
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor  = Color.Red;
                        }
                    }
                }
            }
        }
        private void TableNewRow(string SampleName, string item, string chkdata, string Unit, string Testbase, string limitdata, string machine, string Result, string getSamptime, string getSampaddr ,string CheckUnit,
           string tester, string CheckTime,string detectunit ,string ChkNum)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["样品名称"] = SampleName;
            dr["检测项目"] = item;
            dr["检测结果"] = chkdata;
            dr["单位"] = Unit;
            dr["检测依据"] = Testbase;
            dr["标准值"] = limitdata;
            dr["检测仪器"] = machine;
            dr["结论"] = Result;
            dr["检测单位"] = detectunit;
            dr["采样时间"] = getSamptime;
            dr["采样地点"] = getSampaddr;
            dr["被检单位"] = CheckUnit;
            dr["检定员"] = tester;
            dr["检测时间"] = CheckTime;
            dr["检测数量"] = ChkNum;
            //dr["进货数量"] = jinhuo;
            //dr["采样数量"] = samplenum;
            
            DataReadTable.Rows.Add(dr);
        }
        //显示重新加载表否则有英文
        private void disTableNewRow(string SampleName, string item, string chkdata, string Unit, string Testbase, string limitdata, string machine, string Result, string getSamptime, string getSampaddr, string CheckUnit,
           string tester, string CheckTime,string detectunit,string ChkNum)
        {
            DataRow dr;
            dr = distable.NewRow();

            //dr["检测编号"] = num;
            dr["样品名称"] = SampleName;
            dr["检测项目"] = item;          
            dr["检测结果"] = chkdata;
            dr["单位"] = Unit;
            dr["检测依据"] = Testbase;
            dr["标准值"] = limitdata;
            dr["检测仪器"] = machine;
            dr["结论"] = Result;
            dr["检测单位"] = detectunit;
            dr["采样时间"] = getSamptime;
            dr["采样地点"] = getSampaddr;
            dr["被检单位"] = CheckUnit;
            dr["检定员"] = tester;
            dr["检测时间"] = CheckTime;
            dr["检测数量"] = ChkNum;
            //dr["进货数量"] = stdvalue;
            //dr["采样数量"] = samplenum;

            distable.Rows.Add(dr);
        }
        private void ucSearchData_Load(object sender, EventArgs e)
        {
            iniTable();
            disTable();
            dTStart.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString ();
            searchdatabase();//查询数据显示
            CmbgetItemName();
            CmbgetSampleName();
        }
        //把数据导出到Excel
        private void btnExcel_Click(object sender, EventArgs e)
        {
            DialogResult  D= MessageBox.Show("导出选中的记录单击是，导出全部记录选择否", "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (D == DialogResult.No)
            {
                List<clsReportData> RD = new List<clsReportData>();//创建数据集合
                RD.Clear();
                foreach (DataGridViewRow dgvr in CheckDatas.Rows)
                {
                    RD.Add(new clsReportData()//向数据集合添加数据
                    {
                        //GridNum = dgvr.Cells[0].Value.ToString(),
                        SampleName = dgvr.Cells[0].Value.ToString(),
                        Checkitem = dgvr.Cells[1].Value.ToString(),                      
                        CheckData = dgvr.Cells[2].Value.ToString(),
                        Unit = dgvr.Cells[3].Value.ToString(),
                        testbase = dgvr.Cells[4].Value.ToString(),
                        limitdata = dgvr.Cells[5].Value.ToString(),
                        machine = dgvr.Cells[6].Value.ToString(),
                        Result = dgvr.Cells[7].Value.ToString(),
                        dectectunit = dgvr.Cells[8].Value.ToString(),
                        gettime = dgvr.Cells[9].Value.ToString(),
                        getplace = dgvr.Cells[10].Value.ToString(),
                        CheckUnit = dgvr.Cells[11].Value.ToString(),
                        tester = dgvr.Cells[12].Value.ToString(),
                        CheckTime = dgvr.Cells[13].Value.ToString(),
                        //plannum = dgvr.Cells[11].Value.ToString(),
                        //stockin = dgvr.Cells[13].Value.ToString(),
                        sampleNum = dgvr.Cells[14].Value.ToString(),
                    });
                }
                SaveFileDialog P_SaveFileDialog = new SaveFileDialog();//创建保存文件对话框对象
                P_SaveFileDialog.Filter = "*.xls|*.xls";
                if (DialogResult.OK == P_SaveFileDialog.ShowDialog())//确认是否保存文件                
                {
                    ThreadPool.QueueUserWorkItem(//开始线程池
                    (pp) =>//使用lambda表达式
                    {
                        G_ea = new Microsoft.Office.Interop.Excel.Application();//创建应用程序对象
                        Excel.Workbook P_wk = G_ea.Workbooks.Add(G_missing);//创建Excel文档
                        Excel.Worksheet P_ws = (Excel.Worksheet)P_wk.Worksheets.Add(G_missing, G_missing, G_missing, G_missing);//创建工作区域
                        //生成字段名
                        for (int j = 0; j < CheckDatas.ColumnCount;j++ )
                        {
                            P_ws.Cells[1, j + 1] = CheckDatas.Columns[j].HeaderText;
                        }
                        int FormatNum;//保存excel文件的格式
                        string Version;//excel版本号
                        Excel.Application Application = new Excel.Application();
                        //Excel.Workbook workbook = (Excel.Workbook)Application.Workbooks.Add(Missing.Value);//激活工作簿
                        //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.Add(true);//给工作簿添加一个sheet
                        Version = Application.Version;//获取你使用的excel 的版本号
                        if (Convert.ToDouble(Version) < 12)//You use Excel 97-2003
                        {
                            FormatNum = -4143;
                        }
                        else//you use excel 2007 or later
                        {
                            FormatNum = 56;
                        }
                        //填充数据
                        for (int i = 0; i < RD.Count; i++)
                        {
                            //P_ws.Cells[i + 2, 1] = RD[i].GridNum;//检测编号
                            P_ws.Cells[i + 2, 1] = RD[i].SampleName.ToString();//样品名称
                            P_ws.Cells[i + 2, 2] = RD[i].Checkitem.ToString();//检测项目 
                            P_ws.Cells[i + 2, 3] = RD[i].CheckData.ToString();//检测结果
                            P_ws.Cells[i + 2, 4] = RD[i].Unit.ToString();//数值单位
                            P_ws.Cells[i + 2, 5] = RD[i].testbase.ToString();//检测依据
                            P_ws.Cells[i + 2, 6] = RD[i].limitdata.ToString();//标准值
                            P_ws.Cells[i + 2, 7] = RD[i].machine.ToString();//检测仪器 
                            P_ws.Cells[i + 2, 8] = RD[i].Result.ToString();//结论
                            P_ws.Cells[i + 2, 9] = RD[i].dectectunit.ToString();//检测单位
                            P_ws.Cells[i + 2, 10] = RD[i].gettime.ToString();//采样时间
                            P_ws.Cells[i + 2, 11] = RD[i].getplace.ToString();//采样地点
                            P_ws.Cells[i + 2, 12] = RD[i].CheckUnit.ToString();//被检单位 
                            P_ws.Cells[i + 2, 13] = RD[i].tester.ToString();//检定员
                            P_ws.Cells[i + 2, 14] = RD[i].CheckTime.ToString();//检测时间
                            
                            //P_ws.Cells[i + 2, 12] = RD[i].plannum.ToString();//
                            //P_ws.Cells[i + 2, 14] = RD[i].stockin.ToString();// 
                            P_ws.Cells[i + 2, 15] = RD[i].sampleNum.ToString();//样品数量
                        }
                        P_wk.SaveAs(//保存Word文件
                            P_SaveFileDialog.FileName, FormatNum, G_missing, G_missing,
                            G_missing, G_missing, Excel.XlSaveAsAccessMode.xlShared, G_missing,
                            G_missing, G_missing, G_missing, G_missing);
                        ((Excel._Application)G_ea.Application).Quit();//退出应用程序

                        this.Invoke(//调用窗体线程
                            (MethodInvoker)(() =>//使用lambda表达式
                            {
                                MessageBox.Show("成功导出全部记录并创建Excel文档！", "操作提示！");
                            }));
                    });
                }
            }
            else if (D == DialogResult.Yes) //导出选中的记录
            {
                if (CheckDatas.SelectedRows.Count < 1)
                {
                    MessageBox.Show("请选择需要导出的记录","操作提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                    return;
                }
                List<clsReportData> RD = new List<clsReportData>();//创建数据集合
                RD.Clear();
                for (int k = 0; k < CheckDatas.SelectedRows.Count; k++)
                {                   
                    RD.Add(new clsReportData()//向数据集合添加数据
                    {
                        //GridNum = CheckDatas.SelectedRows[k].Cells[0].Value.ToString(),
                        SampleName = CheckDatas.SelectedRows[k].Cells[0].Value.ToString(),
                        Checkitem = CheckDatas.SelectedRows[k].Cells[1].Value.ToString(),                       
                        CheckData = CheckDatas.SelectedRows[k].Cells[2].Value.ToString(),
                        Unit = CheckDatas.SelectedRows[k].Cells[3].Value.ToString(),
                        testbase = CheckDatas.SelectedRows[k].Cells[4].Value.ToString(),
                        limitdata = CheckDatas.SelectedRows[k].Cells[5].Value.ToString(),
                        machine = CheckDatas.SelectedRows[k].Cells[6].Value.ToString(),
                        Result = CheckDatas.SelectedRows[k].Cells[7].Value.ToString(),
                        dectectunit = CheckDatas.SelectedRows[k].Cells[8].Value.ToString(),
                        gettime = CheckDatas.SelectedRows[k].Cells[9].Value.ToString(),
                        getplace = CheckDatas.SelectedRows[k].Cells[10].Value.ToString(),
                        CheckUnit = CheckDatas.SelectedRows[k].Cells[11].Value.ToString(),
                        tester = CheckDatas.SelectedRows[k].Cells[12].Value.ToString(),
                        CheckTime = CheckDatas.SelectedRows[k].Cells[13].Value.ToString(),

                        //plannum = CheckDatas.SelectedRows[k].Cells[11].Value.ToString(),
                        //stockin = CheckDatas.SelectedRows[k].Cells[13].Value.ToString(),
                        sampleNum = CheckDatas.SelectedRows[k].Cells[14].Value.ToString(),
                    });                   
                }
                    SaveFileDialog P_SaveFileDialog = new SaveFileDialog();//创建保存文件对话框对象
                    P_SaveFileDialog.Filter = "*.xls|*.xls";
                    if (DialogResult.OK == P_SaveFileDialog.ShowDialog())//确认是否保存文件                
                    {
                        ThreadPool.QueueUserWorkItem(//开始线程池
                        (pp) =>//使用lambda表达式
                        {
                            G_ea = new Microsoft.Office.Interop.Excel.Application();//创建应用程序对象
                            Excel.Workbook P_wk = G_ea.Workbooks.Add(G_missing);//创建Excel文档
                            Excel.Worksheet P_ws = (Excel.Worksheet)P_wk.Worksheets.Add(G_missing, G_missing, G_missing, G_missing);//创建工作区域
                            //生成字段名
                            for (int j = 0; j < CheckDatas.ColumnCount; j++)
                            {
                                P_ws.Cells[1, j + 1] = CheckDatas.Columns[j].HeaderText;
                            }
                            //填充数据
                            for (int i = 0; i < RD.Count; i++)
                            {
                                P_ws.Cells[i + 2, 1] = RD[i].SampleName.ToString();//样品名称
                                P_ws.Cells[i + 2, 2] = RD[i].Checkitem.ToString();//检测项目 
                                P_ws.Cells[i + 2, 3] = RD[i].CheckData.ToString();//检测结果
                                P_ws.Cells[i + 2, 4] = RD[i].Unit.ToString();//数值单位
                                P_ws.Cells[i + 2, 5] = RD[i].testbase.ToString();//检测依据
                                P_ws.Cells[i + 2, 6] = RD[i].limitdata.ToString();//标准值
                                P_ws.Cells[i + 2, 7] = RD[i].machine.ToString();//检测仪器 
                                P_ws.Cells[i + 2, 8] = RD[i].Result.ToString();//结论
                                P_ws.Cells[i + 2, 9] = RD[i].dectectunit.ToString();//检测单位
                                P_ws.Cells[i + 2, 10] = RD[i].gettime.ToString();//采样时间
                                P_ws.Cells[i + 2, 11] = RD[i].getplace.ToString();//采样地点
                                P_ws.Cells[i + 2, 12] = RD[i].CheckUnit.ToString();//被检单位 
                                P_ws.Cells[i + 2, 13] = RD[i].tester.ToString();//检定员
                                P_ws.Cells[i + 2, 14] = RD[i].CheckTime.ToString();//检测时间

                                //P_ws.Cells[i + 2, 1] = RD[i].GridNum;//向Excel文档中写入内容
                                //P_ws.Cells[i + 2, 2] = RD[i].Checkitem.ToString();//向Excel文档中写入内容
                                //P_ws.Cells[i + 2, 3] = RD[i].SampleName.ToString();//向Excel文档中写入内容
                                //P_ws.Cells[i + 2, 4] = RD[i].CheckData.ToString();//向Excel文档中写入内容
                                //P_ws.Cells[i + 2, 5] = RD[i].Unit.ToString();//向Excel文档中写入内容
                                //P_ws.Cells[i + 2, 6] = RD[i].CheckTime.ToString();//向Excel文档中写入内容  
                                //P_ws.Cells[i + 2, 7] = RD[i].CheckUnit.ToString();//向Excel文档中写入内容  
                                //P_ws.Cells[i + 2, 8] = RD[i].Result.ToString();//向Excel文档中写入内容  
                                //P_ws.Cells[i + 2, 9] = RD[i].machine.ToString();//向Excel文档中写入内容 
                                //P_ws.Cells[i + 2, 10] = RD[i].gettime.ToString();//向Excel文档中写入内容 
                                //P_ws.Cells[i + 2, 11] = RD[i].getplace.ToString();//向Excel文档中写入内容  
                                ////P_ws.Cells[i + 2, 12] = RD[i].plannum.ToString();//向Excel文档中写入内容   
                                //P_ws.Cells[i + 2, 12] = RD[i].testbase.ToString();//向Excel文档中写入内容 
                                //P_ws.Cells[i + 2, 13] = RD[i].limitdata.ToString();//向Excel文档中写入内容 
                                //P_ws.Cells[i + 2, 14] = RD[i].tester.ToString();//向Excel文档中写入内容 
                                //P_ws.Cells[i + 2, 14] = RD[i].stockin.ToString();//向Excel文档中写入内容 
                                P_ws.Cells[i + 2, 15] = RD[i].sampleNum.ToString();//向Excel文档中写入内容 
                            }
                            int FormatNum;//保存excel文件的格式
                            string Version;//excel版本号
                            Excel.Application Application = new Excel.Application();
                            //Excel.Workbook workbook = (Excel.Workbook)Application.Workbooks.Add(Missing.Value);//激活工作簿
                            //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.Add(true);//给工作簿添加一个sheet
                            Version = Application.Version;//获取你使用的excel 的版本号
                            if (Convert.ToDouble(Version) < 12)//You use Excel 97-2003
                            {
                                FormatNum = -4143;
                            }
                            else//you use excel 2007 or later
                            {
                                FormatNum = 56;
                            }
                            P_wk.SaveAs(//保存Word文件
                                P_SaveFileDialog.FileName, FormatNum, G_missing, G_missing,
                                G_missing, G_missing, Excel.XlSaveAsAccessMode.xlShared, G_missing,
                                G_missing, G_missing, G_missing, G_missing);
                            ((Excel._Application)G_ea.Application).Quit();//退出应用程序

                            this.Invoke(//调用窗体线程
                                (MethodInvoker)(() =>//使用lambda表达式
                                {
                                    MessageBox.Show("成功导出选中记录并创建Excel文档！", "操作提示！");
                                }));
                        });
                    }               
            }
        }
        //报表打印
        private void btnReport_Click(object sender, EventArgs e)
        {
            PrintCheckedReport();
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
                    HtmlUrl = path + "\\report\\CheckedReportModel.html"
                };
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("打印检测报告时出现异常！\r\n\r\n异常信息：\r\n{0}", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetHtmlDoc()
        {
            string htmlDoc = string.Empty;
            try
            {
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    //string Organization = Global.samplenameadapter[0].Organization;
                    string Result = string.Empty;
                    if (CheckDatas.SelectedRows.Count > 0)//CheckDatas.SelectedRows != null &&
                    {
                        //tlsTtResultSecond md = _selectedRecords[0];
                        //string CheckPlanCode = md.CheckPlanCode;
                        //if (CheckPlanCode.Length > 0)
                        //{
                        //    string[] str = CheckPlanCode.Split('-');
                        //    CheckPlanCode = str[0];
                        //}
                        //else
                        //{
                        //    CheckPlanCode = "/";
                        //}
                        Result = CheckDatas.Rows[rowNum].Cells[7].Value.ToString();
                        htmlDoc = System.IO.File.ReadAllText(path + "\\report\\CheckedReportTitle.txt", System.Text.Encoding.Default);
                        htmlDoc += string.Format("<div class=\"number\">编号：{0}</div>", "/");// CheckDatas.Rows[rowNum].Cells[0].Value.ToString().Length > 0 ? (CheckDatas.Rows[rowNum].Cells[0].Value.ToString()) : "/"
                        htmlDoc += "<div style=\"float:left; margin-left:40px; margin-top:20px;\">";
                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

                        htmlDoc += "<tbody><tr><th width=\"190\" height=\"76\" class=\"as\">样品名称</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as\">{0}</td>", CheckDatas.Rows[rowNum].Cells[0].Value.ToString().Length > 0 ? (CheckDatas.Rows[rowNum].Cells[0].Value.ToString()) : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"as\">唯一性单号或样品单号</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td></tr>", "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）时间</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatas.Rows[rowNum].Cells[9].Value.ToString().Length > 0 ? DateTime.Parse((CheckDatas.Rows[rowNum].Cells[9].Value.ToString())).AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）地点</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckDatas.Rows[rowNum].Cells[10].Value.ToString().Length > 0 ? (CheckDatas.Rows[rowNum].Cells[10].Value.ToString()) : "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">被检单位</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatas.Rows[rowNum].Cells[11].Value.ToString().Length > 0 ? (CheckDatas.Rows[rowNum].Cells[11].Value.ToString()) : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">被检单位地址</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", "/");

                        htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">检测时间</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", CheckDatas.Rows[rowNum].Cells[13].Value.ToString().Length > 0 ? DateTime.Parse((CheckDatas.Rows[rowNum].Cells[13].Value.ToString())).AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                        htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">检测依据</th>";
                        htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckDatas.Rows[rowNum].Cells[4].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[4].Value.ToString() : "/");
                        htmlDoc += "</tbody></table>";

                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测项目</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测仪器</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测结果</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">参考值</th>";
                        htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\" id=\"unique\">检测结论</th></tr>";

                        htmlDoc += string.Format("<tr><td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatas.Rows[rowNum].Cells[1].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[1].Value.ToString() : "/");
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatas.Rows[rowNum].Cells[6].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[6].Value.ToString() : "/");
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", CheckDatas.Rows[rowNum].Cells[2].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[2].Value.ToString() : "/");//+ (Result.Equals("阴性") || Result.Equals("阴性") ? string.Empty : (" " + CheckDatas.Rows[rowNum].Cells[3].Value.ToString()))
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>",CheckDatas.Rows[rowNum].Cells[5].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[5].Value.ToString() : "/");//CheckDatas.Rows[rowNum].Cells[13].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[13].Value.ToString() :
                        htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\" id=\"unique\">{0}</td></tr>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");

                        htmlDoc += "</tbody></table>";

                        htmlDoc += string.Format("<div class=\"forming\"><div class=\"conclusion\">结论：{0}</div>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");
                        //htmlDoc += "<div class=\"img\"><img src=\"CheckedReportOfficialSeal.gif\"></div>";
                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-54px;font-size:16px;\">";
                        htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                        htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">检测单位：{0}</td>", CheckDatas.Rows[rowNum].Cells[8].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[8].Value.ToString() : "/");
                        htmlDoc += "</tr></tbody></table>";

                        htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-20px;font-size:16px;\">";
                        htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                        htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">报告时间：{0}</td>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        htmlDoc += "</tr></tbody></table></div>";

                        htmlDoc += "<div class=\"forming\" style=\"height:54px; margin-top:0; float:left;\"><div class=\"note\"><span class=\"NT\">备注：检测结论为不合格时，应该及时上报。</span></div></div></div>";
                        htmlDoc += string.Format("<div class=\"personnel\">检测人员：{0}</div>", CheckDatas.Rows[rowNum].Cells[12].Value.ToString().Length > 0 ? CheckDatas.Rows[rowNum].Cells[12].Value.ToString() : "/");
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
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                throw;
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
       
        //EXCEL数据处理
        private void CBoxBind()//对下拉列表进行数据绑定
        {          
            //连接Excel数据库
            OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=Excel 8.0");
            olecon.Open();//打开数据库连接
            System.Data.DataTable DTable = olecon.GetSchema("Tables");//实例化表对象
            DataTableReader DTReader = new DataTableReader(DTable);//实例化表读取对象
            while (DTReader.Read())//循环读取
            {
                string P_str_Name = DTReader["Table_Name"].ToString().Replace('$', ' ').Trim();//记录工作表名称
                //if (!cbox_SheetName.Items.Contains(P_str_Name))//判断下拉列表中是否已经存在该工作表名称
                //    cbox_SheetName.Items.Add(P_str_Name);//将工作表名添加到下拉列表中
            }
            DTable = null;//清空表对象
            DTReader = null;//清空表读取对象
            olecon.Close();//关闭数据库连接
            
        }
        private void CloseProcess(string P_str_Process)//关闭进程
        {
            System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName(P_str_Process);//实例化进程对象
            foreach (System.Diagnostics.Process p in excelProcess)
                p.Kill();//关闭进程
            System.Threading.Thread.Sleep(10);//使线程休眠10毫秒
        }

        //导入数据
        private string ExcelPath = string.Empty;
        clsSaveResult InData = new clsSaveResult();
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            int n = 0;
            int re = 0;
            openFileDialog1.Filter="Excel文件|*.xls";//设置打开文件筛选器
            openFileDialog1.Title = "打开Excel文件";//设置打开对话框标题
            ExcelPath = openFileDialog1.FileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                try
                {
                    //连接Excel数据库
                    OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=Excel 8.0");
                    olecon.Open();//打开数据库连接
                    OleDbDataAdapter oledbda = new OleDbDataAdapter("select * from [" + "Sheet4" + "$]", olecon);//从工作表中查询数据
                    DataSet myds = new DataSet();//实例化数据集对象
                    oledbda.Fill(myds);//填充数据集              
                    //int P_int_Counts = myds.Tables[0].Rows.Count;
                    for (int r = 0; r < myds.Tables[0].Rows.Count; r++)
                    {
                        //InData.Gridnum = myds.Tables[0].Rows[r][0].ToString();
                        InData.SampleName = myds.Tables[0].Rows[r][0].ToString();
                        InData.Checkitem = myds.Tables[0].Rows[r][1].ToString();
                        InData.CheckData = myds.Tables[0].Rows[r][2].ToString();
                        InData.Unit = myds.Tables[0].Rows[r][3].ToString();
                        InData.Testbase = myds.Tables[0].Rows[r][4].ToString();
                        InData.LimitData = myds.Tables[0].Rows[r][5].ToString();
                        InData.Instrument = myds.Tables[0].Rows[r][6].ToString();
                        InData.Result = myds.Tables[0].Rows[r][7].ToString();
                        InData.detectunit = myds.Tables[0].Rows[r][8].ToString();
                        InData.Gettime = myds.Tables[0].Rows[r][9].ToString();
                        InData.Getplace = myds.Tables[0].Rows[r][10].ToString();
                        InData.CheckUnit = myds.Tables[0].Rows[r][11].ToString();//备检单位
                        InData.Tester = myds.Tables[0].Rows[r][12].ToString();
                        InData.CheckTime =DateTime.Parse( myds.Tables[0].Rows[r][13].ToString().Replace("-","/") );
                        //InData.PlanNum = myds.Tables[0].Rows[r][11].ToString();
                        //InData.stockin = myds.Tables[0].Rows[r][13].ToString();
                        InData.sampleNum = myds.Tables[0].Rows[r][14].ToString();

                        re=sql.LoadInData(InData, out err);
                        if (re == 1)
                        {
                            n = n + 1;
                        }
                    }
                    MessageBox.Show("共成功导入"+n+"条数据","操作提示");
                    //刷新数据
                    btnfind_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message ,"Error");
                }

                //object missing = System.Reflection.Missing.Value;//声明object缺省值
                //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//实例化Excel对象
                ////打开Excel文件
                //Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(openFileDialog1.FileName , missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //Microsoft.Office.Interop.Excel.Worksheet worksheet;//声明工作表
                //Microsoft.Office.Interop.Access.Application access = new Microsoft.Office.Interop.Access.Application();//实例化Access对象
                //worksheet = ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["Sheet1"]);//获取选择的工作表
                //worksheet.Move(workbook.Sheets[1], missing);//将选择的工作表作为第一个工作表
                //object P_obj_Name = (object)worksheet.Name;//获取工作表名称
                //excel.DisplayAlerts = false;//设置Excel保存时不显示对话框
                //workbook.Save();//保存工作簿
                //CloseProcess("EXCEL");//关闭所有Excel进程
                //object P_obj_Excel = (object)openFileDialog1.FileName;//记录Excel文件路径
                //try
                //{
                //    string password = "dy05xl378";
                //    string dataName = "local.Mdb";
                //    string getLocalDBPathString = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
                //    access.OpenCurrentDatabase(getLocalDBPathString, true, password);//打开Access数据库
                //    //将Excel指定工作表中的数据导入到Access中
                //    access.DoCmd.TransferSpreadsheet(Microsoft.Office.Interop.Access.AcDataTransferType.acImport, Microsoft.Office.Interop.Access.AcSpreadSheetType.acSpreadsheetTypeExcel97, P_obj_Name, P_obj_Excel, true, missing, missing);
                //    access.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveAll);//关闭并保存Access数据库文件
                //    CloseProcess("MSACCESS");//关闭所有Access数据库进程
                //    MessageBox.Show("已经将Excel的" + "Sheet1" + "工作表中的数据导入到Access数据库中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch
                //{
                //    MessageBox.Show("Access数据库中已经存在该表！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }
         
        //修改数据
        private void btnxiu_Click(object sender, EventArgs e)
        {
            Global.AddItem = null;

            string[,] ar1 = new string[CheckDatas.SelectedRows.Count, 15];
            if (CheckDatas.SelectedRows.Count > 1)
            {
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    ar1[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                    ar1[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                    ar1[i, 2] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                    ar1[i, 3] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                    ar1[i, 4] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                    ar1[i, 5] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
                    ar1[i, 6] = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
                    ar1[i, 7] = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
                    ar1[i, 8] = CheckDatas.SelectedRows[i].Cells[8].Value.ToString();
                    ar1[i, 9] = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();
                    ar1[i, 10] = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();
                    ar1[i, 11] = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();                
                    ar1[i, 12] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
                    ar1[i, 13] = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
                    ar1[i, 14] = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
                }
                Global.AddItem = ar1;
                frmRepairData frd = new frmRepairData();
                DialogResult dr=  frd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    btnfind_Click(null ,null);
                }                
            }
            else
            {
                string[,] modified = new string[CheckDatas.SelectedRows.Count, 15];
                modified[0, 0] = CheckDatas.SelectedRows[0].Cells[0].Value.ToString();
                modified[0, 1] = CheckDatas.SelectedRows[0].Cells[1].Value.ToString();
                modified[0, 2] = CheckDatas.SelectedRows[0].Cells[2].Value.ToString();
                modified[0, 3] = CheckDatas.SelectedRows[0].Cells[3].Value.ToString();
                modified[0, 4] = CheckDatas.SelectedRows[0].Cells[4].Value.ToString();
                modified[0, 5] = CheckDatas.SelectedRows[0].Cells[5].Value.ToString();
                modified[0, 6] = CheckDatas.SelectedRows[0].Cells[6].Value.ToString();
                modified[0, 7] = CheckDatas.SelectedRows[0].Cells[7].Value.ToString();
                modified[0, 8] = CheckDatas.SelectedRows[0].Cells[8].Value.ToString();
                modified[0, 9] = CheckDatas.SelectedRows[0].Cells[9].Value.ToString();
                modified[0, 10] = CheckDatas.SelectedRows[0].Cells[10].Value.ToString();
                modified[0, 11] = CheckDatas.SelectedRows[0].Cells[11].Value.ToString();
                modified[0, 12] = CheckDatas.SelectedRows[0].Cells[12].Value.ToString();
                modified[0, 13] = CheckDatas.SelectedRows[0].Cells[13].Value.ToString();
                modified[0, 14] = CheckDatas.SelectedRows[0].Cells[14].Value.ToString();

                Global.AddItem = modified;
                frmQueryModifi fqm = new frmQueryModifi();
                DialogResult dr = fqm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    btnfind_Click(null, null);
                }           

                //for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                //{
                //    ar1[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                //    ar1[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                //    ar1[i, 2] = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();
                //    ar1[i, 3] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                //    ar1[i, 4] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                //    ar1[i, 5] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                //    ar1[i, 6] = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
                //    ar1[i, 7] = CheckDatas.SelectedRows[i].Cells[8].Value.ToString();
                //    ar1[i, 8] = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
                //    ar1[i, 9] = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();
                //    ar1[i, 10] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
                //    ar1[i, 11] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
                //    //ar1[i, 12] = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
                //    //ar1[i, 13] = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
                //    ar1[i, 14] = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();
                //}
                //Global.AddItem = ar1;
                //frmnewdata frd = new frmnewdata();
                //DialogResult dr = frd.ShowDialog();
                //if (dr == DialogResult.OK)
                //{
                //    btnfind_Click(null, null);
                //}
            }
        }
        //双击修改数据
        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Global.AddItem = null;
            string[,] modified = new string[CheckDatas.SelectedRows.Count, 15];
            modified[0, 0] = CheckDatas.SelectedRows[0].Cells[0].Value.ToString();
            modified[0, 1] = CheckDatas.SelectedRows[0].Cells[1].Value.ToString();
            modified[0, 2] = CheckDatas.SelectedRows[0].Cells[2].Value.ToString();
            modified[0, 3] = CheckDatas.SelectedRows[0].Cells[3].Value.ToString();
            modified[0, 4] = CheckDatas.SelectedRows[0].Cells[4].Value.ToString();
            modified[0, 5] = CheckDatas.SelectedRows[0].Cells[5].Value.ToString();
            modified[0, 6] = CheckDatas.SelectedRows[0].Cells[6].Value.ToString();
            modified[0, 7] = CheckDatas.SelectedRows[0].Cells[7].Value.ToString();
            modified[0, 8] = CheckDatas.SelectedRows[0].Cells[8].Value.ToString();
            modified[0, 9] = CheckDatas.SelectedRows[0].Cells[9].Value.ToString();
            modified[0, 10] = CheckDatas.SelectedRows[0].Cells[10].Value.ToString();
            modified[0, 11] = CheckDatas.SelectedRows[0].Cells[11].Value.ToString();
            modified[0, 12] = CheckDatas.SelectedRows[0].Cells[12].Value.ToString();
            modified[0, 13] = CheckDatas.SelectedRows[0].Cells[13].Value.ToString();
            modified[0, 14] = CheckDatas.SelectedRows[0].Cells[14].Value.ToString();
            Global.AddItem = modified;
            frmQueryModifi fqm = new frmQueryModifi();
            DialogResult dr = fqm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                btnfind_Click(null, null);
            }           

            //if (CheckDatas.SelectedRows.Count > 2)
            //{
            //    string[,] ar1 = new string[CheckDatas.SelectedRows.Count, 15];
            //    for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
            //    {
            //        ar1[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
            //        ar1[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
            //        ar1[i, 2] = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();
            //        ar1[i, 3] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
            //        ar1[i, 4] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
            //        ar1[i, 5] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
            //        ar1[i, 6] = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
            //        ar1[i, 7] = CheckDatas.SelectedRows[i].Cells[8].Value.ToString();
            //        ar1[i, 8] = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
            //        ar1[i, 9] = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();
            //        ar1[i, 10] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
            //        ar1[i, 11] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
            //        ar1[i, 12] = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
            //        //ar1[i, 13] = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
            //        //ar1[i, 14] = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();
            //    }
            //    Global.AddItem = ar1;
            //    frmRepairData frd = new frmRepairData();
            //    DialogResult dr = frd.ShowDialog();
            //    if (dr == DialogResult.OK)
            //    {
            //        btnfind_Click(null, null);
            //    }
            //}
            //else
            //{
            //    string[,] ar1 = new string[CheckDatas.SelectedRows.Count, 15];
            //    for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
            //    {
            //        ar1[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
            //        ar1[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
            //        ar1[i, 2] = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();
            //        ar1[i, 3] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
            //        ar1[i, 4] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
            //        ar1[i, 5] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
            //        ar1[i, 6] = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
            //        ar1[i, 7] = CheckDatas.SelectedRows[i].Cells[8].Value.ToString();
            //        ar1[i, 8] = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
            //        ar1[i, 9] = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();
            //        ar1[i, 10] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
            //        ar1[i, 11] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
            //        ar1[i, 12] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
            //        //ar1[i, 13] = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
            //        //ar1[i, 14] = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();
            //    }
            //    Global.AddItem = ar1;
            //    frmnewdata frd = new frmnewdata();
            //    frmQueryModifi fqm=new frmQueryModifi ();
            //    DialogResult dr = fqm.ShowDialog();
            //    if (dr == DialogResult.OK)
            //    {
            //        btnfind_Click(null, null);
            //    }               
            //}                  
        }
        //单击修改数据
        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowNum=e.RowIndex;
        }

        //自动查询
        private void dTPEnd_ValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;           
            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
            sb.Append(dTPEnd.Value.Year.ToString());
            sb.Append("/");
            sb.Append(dTPEnd.Value.Month.ToString());
            sb.Append("/");
            sb.Append(dTPEnd.Value.Day.ToString());
            //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
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
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                       dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
                }
                if (disAll == true)
                {
                    CheckDatas.DataSource = DataReadTable;
                }
                else
                {
                    dtInfo = dtb;
                    InitDataSet();
                }

                CheckDatas.Columns[7].Width = 160;
                CheckDatas.Columns[10].Width = 160;
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells[9].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[9].Value.ToString() == "不合格")
                        {
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor  = Color.Red;
                        }
                    }
                }
            }
        }
        
        //获取同一列不同的检测项目
        //以下方法是实现将界面上的dataGridView1控件中第一列的值填充到界面上的comboBox控件中的，，重复的值是不会添加的。
        //在窗体的启动事件中调用此方法就可以了，如果是取其它列的值，修改“Cells[0]”的值就可以了；      
      private void CmbgetItemName()
      {
          List<string> lsName = new List<string>();
          lsName.Add("请选择...");
          for(int i=0;i<this.CheckDatas.Rows.Count;i++)
          {
              string name=this.CheckDatas.Rows[i].Cells[1].Value.ToString();
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
          for (int i = 0; i < this.CheckDatas.Rows.Count; i++)
          {
              string name = this.CheckDatas.Rows[i].Cells[0].Value.ToString();
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
      /// 右键单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void CheckDatas_MouseDown(object sender, MouseEventArgs e)
      {
          //if (e.Button == MouseButtons.Right)
          //{
          //    DialogResult r= MessageBox.Show("是否删除选中的记录","提示",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information );
          //    if (r == DialogResult.Yes)
          //    {
          //        StringBuilder sb = new StringBuilder();
          //        sb.Clear();
          //        int d = 0;
          //        try
          //        {
          //            for(int i=0;i<CheckDatas.SelectedRows.Count ;i++)
          //            {
          //                sb.Append("Checkitem='");
          //                sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
          //                sb.Append("' AND ");
          //                sb.Append("SampleName='");
          //                sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
          //                sb.Append("' AND ");
          //                sb.Append("CheckTime='");
          //                sb.Append(CheckDatas.SelectedRows[i].Cells[6].Value.ToString());//yyyy-MM-dd
          //                sb.Append("'");
          //                d=sql.deleteSD(sb.ToString());
          //                if (d == 1)
          //                {
          //                    MessageBox.Show("数据删除成功","提示");
          //                }
          //            }

          //        }
          //        catch (Exception ex)
          //        {
          //            MessageBox.Show(ex.Message, "Error");
          //        }
                 
          //    }             
          //}
      }
     
      /// <summary>
      /// 选择结论单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbResult_SelectedValueChanged(object sender, EventArgs e)
      {

          DataReadTable.Clear();
          CheckDatas.DataSource = null;
          //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
          StringBuilder sb = new StringBuilder();
          sb.Clear();
          sb.Append("CheckTime>=#");
          sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
          sb.Append("#");
          sb.Append(" AND CheckTime<=#");
          sb.Append(dTPEnd.Value.Year.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Month.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Day.ToString());
          //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
          sb.Append(" 23:59:59#");
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
              for (int i = 0; i < dtb.Rows.Count; i++)
              {
                  TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                       dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString()
                       );
              }
              if (disAll == true)
              {
                  CheckDatas.DataSource = DataReadTable;
              }
              else
              {
                  dtInfo = dtb;
                  InitDataSet();
              }

              CheckDatas.Columns[7].Width = 160;
              CheckDatas.Columns[10].Width = 160;
              if (CheckDatas.Rows.Count > 0)
              {
                  for (int i = 0; i < CheckDatas.Rows.Count; i++)
                  {
                      if (CheckDatas.Rows[i].Cells[9].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[9].Value.ToString() == "不合格")
                      {
                          CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                      }
                  }
              }
          }

      }
      /// <summary>
      /// 选择样品名称单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbSample_SelectedValueChanged(object sender, EventArgs e)
      {
          DataReadTable.Clear();
          CheckDatas.DataSource = null;
          //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
          StringBuilder sb = new StringBuilder();
          sb.Clear();
          sb.Append("CheckTime>=#");
          sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
          sb.Append("#");
          sb.Append(" AND CheckTime<=#");
          sb.Append(dTPEnd.Value.Year.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Month.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Day.ToString());
          //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
          sb.Append(" 23:59:59#");
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
              for (int i = 0; i < dtb.Rows.Count; i++)
              {
                  TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                       dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
              }
              if (disAll == true)
              {
                  CheckDatas.DataSource = DataReadTable;
              }
              else
              {
                  dtInfo = dtb;
                  InitDataSet();
              }
              CheckDatas.Columns[7].Width = 160;
              CheckDatas.Columns[10].Width = 160;
              if (CheckDatas.Rows.Count > 0)
              {
                  for (int i = 0; i < CheckDatas.Rows.Count; i++)
                  {
                      if (CheckDatas.Rows[i].Cells[9].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[9].Value.ToString() == "不合格")
                      {
                          CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                      }
                  }
              }
          }
      }
      
      /// <summary>
      /// 选择检测项目单击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbTestItem_SelectedValueChanged(object sender, EventArgs e)
      {
          DataReadTable.Clear();
          CheckDatas.DataSource = null;
          //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
          StringBuilder sb = new StringBuilder();
          sb.Clear();
          sb.Append("CheckTime>=#");
          sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
          sb.Append("#");
          sb.Append(" AND CheckTime<=#");
          sb.Append(dTPEnd.Value.Year.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Month.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Day.ToString());
          //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
          sb.Append(" 23:59:59#");
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
              for (int i = 0; i < dtb.Rows.Count; i++)
              {
                  TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                       dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
              }
              if (disAll == true)
              {
                  CheckDatas.DataSource = DataReadTable;
              }
              else
              {
                  dtInfo = dtb;
                  InitDataSet();
              }

              CheckDatas.Columns[7].Width = 160;
              CheckDatas.Columns[10].Width = 160;
              if (CheckDatas.Rows.Count > 0)
              {
                  for (int i = 0; i < CheckDatas.Rows.Count; i++)
                  {
                      if (CheckDatas.Rows[i].Cells[9].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[9].Value.ToString() == "不合格")
                      {
                          CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                      }
                  }
              }
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
          if (tspbtndisplay.Text == "全部显示")
          {
              tspbtndisplay.Text = "分页显示";
              disAll = true;
              toolStripLabel3.Visible = false;
              toolStripLabel4.Visible = false;
              toolStripLabel5.Visible = false;
              toolStripLabel6.Visible = false;
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
              toolStripLabel4.Visible = true;
              toolStripLabel5.Visible = true;
              toolStripLabel6.Visible = true;
              tspallpage.Visible = true;
              tsplabpage.Visible = true;
              tspbtnnext.Visible = true;
              tspbtnuppage.Visible = true;
              searchdatabase();
          }
          
      }
      //下一页
      private void tspbtnnext_Click(object sender, EventArgs e)
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
      //上一页
      private void tspbtnuppage_Click(object sender, EventArgs e)
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
      /// <summary>
      /// 开始时间查询
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void dTStart_ValueChanged(object sender, EventArgs e)
      {
          DataReadTable.Clear();
          CheckDatas.DataSource = null;
          //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
          StringBuilder sb = new StringBuilder();
          sb.Clear();
          sb.Append("CheckTime>=#");
          sb.Append(dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));//yyyy-MM-dd
          sb.Append("#");
          sb.Append(" AND CheckTime<=#");
          sb.Append(dTPEnd.Value.Year.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Month.ToString());
          sb.Append("/");
          sb.Append(dTPEnd.Value.Day.ToString());
          //sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
          sb.Append(" 23:59:59#");
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
              for (int i = 0; i < dtb.Rows.Count; i++)
              {
                  TableNewRow(dtb.Rows[i][2].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                     dtb.Rows[i][12].ToString(), dtb.Rows[i][13].ToString(), dtb.Rows[i][8].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][9].ToString(), dtb.Rows[i][10].ToString(),
                     dtb.Rows[i][6].ToString(), dtb.Rows[i][14].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][11].ToString(), dtb.Rows[i][16].ToString());
              }
              if (disAll == true)
              {
                  CheckDatas.DataSource = DataReadTable;
              }
              else
              {
                  dtInfo = dtb;
                  InitDataSet();
              }

              CheckDatas.Columns[7].Width = 160;
              CheckDatas.Columns[10].Width = 160;
              if (CheckDatas.Rows.Count > 0)
              {
                  for (int i = 0; i < CheckDatas.Rows.Count; i++)
                  {
                      if (CheckDatas.Rows[i].Cells[9].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[9].Value.ToString() == "不合格")
                      {
                          CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                      }
                  }
              }
          }
      }
      /// <summary>
      /// 删除选中数据
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnDelete_Click(object sender, EventArgs e)
      {
          string err = string.Empty;
          StringBuilder sb = new StringBuilder();
          for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
          {
              sb.Clear();
              sb.Append(" CheckResult where SampleName='");
              sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
              sb.Append("' and Checkitem='");
              sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
              sb.Append("'");
              sb.Append(" and CheckData='");
              sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
              sb.Append("'");
              sb.Append(" and Machine='");
              sb.Append(CheckDatas.SelectedRows[i].Cells[6].Value.ToString());
              sb.Append("' and CheckTime=#");
              sb.Append(DateTime.Parse( CheckDatas.SelectedRows[i].Cells[13].Value.ToString()));
              sb.Append("#");

              sql.Delete(sb.ToString(),out err);

          }
          MessageBox.Show("删除数据成功","提示");
          //刷新数据
          btnfind_Click(null, null);
      }

      private clsUpLoadData udata = new clsUpLoadData(); 
     //数据上传
      private void btnUpload_Click(object sender, EventArgs e)
      {
          string err = "";
          int iok = 0;
          int ino = 0;

          //查询数据上传的地址
          try
          {
              Global.ServerAdd =ConfigurationManager.AppSettings["ServerAddr"];
              Global.ServerName = ConfigurationManager.AppSettings["ServerName"];
              Global.ServerPassword = ConfigurationManager.AppSettings["ServerPassword"];
              //string err = string.Empty;
              //StringBuilder sb = new StringBuilder();

              //sb.Append(" where isselect='是'");

              //DataTable dt = sql.GetServer(sb.ToString(), out err);
              //if (dt != null)
              //{
              //    Global.ServerAdd = dt.Rows[0][1].ToString();
              //    Global.ServerName = dt.Rows[0][2].ToString();
              //    Global.ServerPassword = dt.Rows[0][3].ToString();
              //}
              //登录
              string rt = TYUpData.Logon();

              JavaScriptSerializer js = new JavaScriptSerializer();
              TYUpData.login list = js.Deserialize<TYUpData.login>(rt);    //将json数据转化为对象类型并赋值给list
              Global.CompanyCode = list.qycode;
              Global.UserCode = list.userid;

              for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
              {
                  udata.shuliang = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
                  udata.ttime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString().Replace("/", "-").Trim();
                  udata.chker = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
                  udata.hegelv ="100";
                  udata.duixiang = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                  udata.chkdata = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                  udata.companyCode = Global.CompanyCode;
                  if (CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "阴性" || CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "合格")
                  {
                      udata.Conclusion = "合格";
                  }
                  else
                  {
                      udata.Conclusion = "不合格";
                  }

                  string rd = TYUpData.UpData(udata);

                  JavaScriptSerializer jsup = new JavaScriptSerializer();
                  TYUpData.Upload retu = jsup.Deserialize<TYUpData.Upload>(rd);    //将json数据转化为对象类型并赋值给list
                  if (retu.isSucess == "上传成功")
                  {
                      CheckDatas.SelectedRows[i].DefaultCellStyle.BackColor = Color.Green;//上传成功变绿色
                      iok = iok + 1;
                      clsUpdateData ud = new clsUpdateData();
                      ud.result = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                      ud.ChkTime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
                      sql.SetUpLoadData(ud,out err);
                  }
                  else
                  {
                      ino= ino + 1;
                  }
              }
              MessageBox.Show("共成功上传"+iok+"条数据，失败"+ino+"条数据","数据上传");

          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message, "Error");
          }
      }

    }
}

