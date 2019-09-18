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
using WorkstationModel.beihai;
using WorkstationDAL.AnHui;
using AIO.src;

namespace WorkstationUI.function
{
    public partial class ucSearchData : UserControl
    {
        private clsSetSqlData sql = new clsSetSqlData();
        public DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private bool iscreate=false ;
        private int rowNum=0;
        private Word.Application G_wa;//定义Word应用程序字段
        private object G_missing = //定义G_missing字段并添加引用
            System.Reflection.Missing.Value;
        private Excel.Application G_ea=null;//定义Excel应用程序字段
        private DataTable distable=null ;
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
        string outErr = string.Empty;
        int icount = 0;
        int fcount = 0;
        private DataTable dtInfo = new DataTable();

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
                dataCol.ColumnName = "被检单位性质";
                distable.Columns.Add(dataCol);
                
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                distable.Columns.Add(dataCol);


                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "数量单位";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "条形码";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产单位";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地地址";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产企业";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产日期";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "送检日期";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "ID";
                distable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "处理结果";
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
                dataCol.ColumnName = "被检单位性质";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
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

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "数量单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "条形码";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地地址";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产企业";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产日期";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "送检日期";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "ID";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "处理结果";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
 
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
                CheckDatasdd.Columns["CompanyNeture"].HeaderText = "被检单位性质";
                CheckDatasdd.Columns["DoResult"].HeaderText = "处理结果";
                CheckDatasdd.Columns["HoleNum"].HeaderText = "通道号";
                CheckDatasdd.Columns["Xiguangdu"].HeaderText = "吸光度";
                CheckDatasdd.Columns["ChkCompanyCode"].HeaderText = "检测单位编号";
                CheckDatasdd.Columns["itemCode"].HeaderText = "检测项目编号";
                CheckDatasdd.Columns["SampleCategoryCode"].HeaderText = "样品种类编号";
                CheckDatasdd.Columns["ChkMethod"].HeaderText = "检测方法";
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
                CheckDatasdd.Columns["BID"].Visible = false;
                CheckDatasdd.Columns["TID"].Visible = false;
                CheckDatasdd.Columns["CompanyNeture"].Visible = false;
                CheckDatasdd.Columns["SampleTime"].Visible = false;
                CheckDatasdd.Columns["ProductPlace"].Visible = false;
                CheckDatasdd.Columns["CompanyNeture"].Visible = false;
                CheckDatasdd.Columns["SendTestDate"].Visible = false;
                CheckDatasdd.Columns["DetectUnit"].Visible = false;
                CheckDatasdd.Columns["SampleAddress"].Visible = false;
                CheckDatasdd.Columns["SampleNum"].Visible = false;

                
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
                //DataReadTable.Clear();
                CheckDatasdd.DataSource = null;
                //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
                sb.Clear();
                sb.AppendFormat("CheckTime>=#{0}#", dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));
                sb.AppendFormat(" AND CheckTime<=#{0}/", dTPEnd.Value.Year.ToString());     
                sb.AppendFormat("{0}/{1} 23:59:59#", dTPEnd.Value.Month.ToString(), dTPEnd.Value.Day.ToString());
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
                    CheckDatasdd.DataSource = dtb;
                   
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
                        //CheckDatasdd.Columns[8].Width = 160;
                        //CheckDatasdd.Columns[12].Width = 160;
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
        private void TableNewRow(string SampleName, string item, string chkdata, string Unit, string Testbase, string limitdata, string machine, string Result, string getSamptime, string getSampaddr ,string CheckUnit,
           string tester, string CheckTime,string detectunit ,string ChkNum,string SampleType,string id,string NumUnit,string barcode,string productorUnit,string ProductorAddr,string ProdoctCompany,string ProductPlace
            ,string ProductDate,string SendDate,string nature,string DoResult)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["ID"] = id;
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
            dr["检测员"] = tester;
            dr["检测时间"] = CheckTime;
            dr["检测数量"] = ChkNum;
            dr["样品种类"] = SampleType;
            dr["数量单位"] = NumUnit;
            dr["条形码"] = barcode;
            dr["生产单位"] = productorUnit;
            dr["产地地址"] = ProductorAddr;
            dr["生产企业"] = ProdoctCompany;
            dr["产地"] = ProductPlace;
            dr["生产日期"] = ProductDate;
            dr["送检日期"] = SendDate;
            dr["被检单位性质"] = nature;
            dr["处理结果"] = DoResult;
            //dr["进货数量"] =jinhuo;
            //dr["采样数量"] = samplenum;
            
            DataReadTable.Rows.Add(dr);
        }
        //显示重新加载表否则有英文
        private void disTableNewRow(string SampleName, string item, string chkdata, string Unit, string Testbase, string limitdata, string machine, string Result, string getSamptime, string getSampaddr, string CheckUnit,
           string tester, string CheckTime, string detectunit, string ChkNum, string sampletype, string id, string NumUnit, string barcode, string productorUnit, string ProductorAddr, string ProdoctCompany, string ProductPlace, string ProductDate, string SendDate,string nature,string DoResult)
        {
            DataRow dr;
            dr = distable.NewRow();
            dr["ID"] = id;
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
            dr["检测员"] = tester;
            dr["检测时间"] = CheckTime;
            dr["检测数量"] = ChkNum;
            dr["样品种类"] = sampletype;
            dr["数量单位"] = NumUnit;
            dr["条形码"] = barcode;
            dr["生产单位"] = productorUnit;
            dr["产地地址"] = ProductorAddr;
            dr["生产企业"] = ProdoctCompany;
            dr["产地"] = ProductPlace;
            dr["生产日期"] = ProductDate;
            dr["送检日期"] = SendDate;
            dr["被检单位性质"] = nature;
            dr["处理结果"] = DoResult;
            //dr["进货数量"] = stdvalue;
            //dr["采样数量"] = samplenum;

            distable.Rows.Add(dr);
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
                    sb.AppendFormat("CheckTime>=#{0}#", dTStart.Value.ToString("yyyy/MM/dd 00:00:00"));
                    sb.AppendFormat(" AND CheckTime<=#{0}/", dTPEnd.Value.Year.ToString());
                    sb.AppendFormat("{0}/{1} 23:59:59#", dTPEnd.Value.Month.ToString(), dTPEnd.Value.Day.ToString());
                  
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
                    string ids = "";
                    for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
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
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string err = string.Empty;
                int n = 0;
                int re = 0;
                int importNum = 0;
                DataTable dtExcel = null;
                openFileDialog1.Filter="Excel文件|*.xls";//设置打开文件筛选器
                openFileDialog1.Title = "打开Excel文件";//设置打开对话框标题
                ExcelPath = openFileDialog1.FileName;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件
                {
                    dtExcel = ExcelHelper.ImportExcel(openFileDialog1.FileName.Trim(), 0);
                    if (dtExcel != null && dtExcel.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            InData.CheckNumber = Global.GUID(null, 1);
                            InData.Checkitem = dtExcel.Rows[i]["检测项目"].ToString();
                            InData.SampleName = dtExcel.Rows[i]["样品名称"].ToString();
                            InData.CheckData = dtExcel.Rows[i]["检测结果"].ToString();
                            InData.Unit = dtExcel.Rows[i]["单位"].ToString();
                            InData.CheckTime =DateTime.Parse (dtExcel.Rows[i]["检测时间"].ToString());
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
                            //InData.SampleID = dtExcel.Rows[i]["样品ID"].ToString();
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
                            InData.CheckCompanyCode = dtExcel.Rows[i]["检测单位编号"].ToString();
                            InData.CheckitemCode = dtExcel.Rows[i]["检测项目编号"].ToString();
                            InData.SampleTypeCode = dtExcel.Rows[i]["样品种类编号"].ToString();
                            InData.ChkMethod = dtExcel.Rows[i]["检测方法"].ToString();

                            re = sql.LoadInData(InData, out err);
                            if (re == 1)
                            {
                                importNum = importNum + 1;
                            }
                        }
                    }
                  
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
                    //for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                    //{
                    //    ar1[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                    //    ar1[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                    //    ar1[i, 2] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                    //    ar1[i, 3] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                    //    ar1[i, 4] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                    //    ar1[i, 5] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
                    //    ar1[i, 6] = CheckDatas.SelectedRows[i].Cells[6].Value.ToString();
                    //    ar1[i, 7] = CheckDatas.SelectedRows[i].Cells[7].Value.ToString();
                    //    ar1[i, 8] = CheckDatas.SelectedRows[i].Cells[8].Value.ToString();
                    //    ar1[i, 9] = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();
                    //    ar1[i, 10] = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();
                    //    ar1[i, 11] = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();                
                    //    ar1[i, 12] = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
                    //    ar1[i, 13] = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
                    //    ar1[i, 14] = CheckDatas.SelectedRows[i].Cells[14].Value.ToString();
                    //    ar1[i, 15] = CheckDatas.SelectedRows[i].Cells[15].Value.ToString();
                    //}
                    //Global.AddItem = ar1;
                    //frmRepairData frd = new frmRepairData();
                    //DialogResult dr=  frd.ShowDialog();
                    //if (dr == DialogResult.OK)
                    //{
                    //    btnfind_Click(null ,null);
                    //}                
                    //}
                    //else
                    //{
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
                //string[,] modified = new string[CheckDatas.SelectedRows.Count, 16];
                //modified[0, 0] = CheckDatas.SelectedRows[0].Cells[0].Value.ToString();
                //modified[0, 1] = CheckDatas.SelectedRows[0].Cells[1].Value.ToString();
                //modified[0, 2] = CheckDatas.SelectedRows[0].Cells[2].Value.ToString();
                //modified[0, 3] = CheckDatas.SelectedRows[0].Cells[3].Value.ToString();
                //modified[0, 4] = CheckDatas.SelectedRows[0].Cells[4].Value.ToString();
                //modified[0, 5] = CheckDatas.SelectedRows[0].Cells[5].Value.ToString();
                //modified[0, 6] = CheckDatas.SelectedRows[0].Cells[6].Value.ToString();
                //modified[0, 7] = CheckDatas.SelectedRows[0].Cells[7].Value.ToString();
                //modified[0, 8] = CheckDatas.SelectedRows[0].Cells[8].Value.ToString();
                //modified[0, 9] = CheckDatas.SelectedRows[0].Cells[9].Value.ToString();
                //modified[0, 10] = CheckDatas.SelectedRows[0].Cells[10].Value.ToString();
                //modified[0, 11] = CheckDatas.SelectedRows[0].Cells[11].Value.ToString();
                //modified[0, 12] = CheckDatas.SelectedRows[0].Cells[12].Value.ToString();
                //modified[0, 13] = CheckDatas.SelectedRows[0].Cells[13].Value.ToString();
                //modified[0, 14] = CheckDatas.SelectedRows[0].Cells[14].Value.ToString();
                //modified[0, 15] = CheckDatas.SelectedRows[0].Cells[15].Value.ToString();

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
      private string geterrmessage(string err)
      {
          string rtn = "";
          if (err == "-1")
          {
              rtn = "用户名或密码不能为空";
          }
          else if (err == "0")
          {
              rtn = "Code口令错误";
          }
          else if (err == "1")
          {
              rtn = "用户名或者密码输入错误";
          }
          else if (err == "2")
          {
              rtn = "检测数据不能为空";
          }
          else if (err == "3")
          {
              rtn = "表示没有可上传的数据库";
          }
          else if (err == "4")
          {
              rtn = "检测数据上传成功";
          }
          return rtn;
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
              if (Global.InstrumentType.Length == 0)
              {
                  MessageBox.Show("仪器类型不能为空", "提示");
                  btnUpload.Enabled = true;
                  return;
              }
              if (Global.IntrumentNums.Length == 0)
              {
                  MessageBox.Show("仪器编号不能为空", "提示");
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
                  //string urlUp = GetServiceURL(Global.ServerAdd, 6);
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
              else if (Global.Platform == "AH")//安徽
              {
                  string errmsg= UploadResultAH();
                  if(errmsg=="")
                  {
                      MessageBox.Show("数据上传成功！共成功上传"+icount +"条数据！","数据上传",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                  }
                  else if (errmsg.Length > 0 && icount>0)
                  {
                      MessageBox.Show("共成功上传" + icount + "条数据！\r\n上传失败"+fcount +"条数据！\r\n失败原因："+errmsg, "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  else if (errmsg.Length > 0 && icount==0)
                  {
                      MessageBox.Show("数据上传失败！！！\r\n失败原因：" + errmsg, "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
       
      }
    
      /// <summary>
      /// 上传至安徽平台
      /// </summary>
      /// <param name="server">服务器相关</param>
      /// <param name="results">上传的数据</param>
      /// <returns>结果</returns>
      private string UploadResultAH()
      {
          outErr = string.Empty;
          string errs = ""; 
          icount = 0;
          fcount = 0;
          try
          {
              Global.AnHuiInterface.ServerAddr = Global.ServerAdd;
              Global.AnHuiInterface.userName = Global.ServerName;
              Global.AnHuiInterface.passWord = Global.ServerPassword;
              Global.AnHuiInterface.instrument = Global.InstrumentType;//仪器类型
              Global.AnHuiInterface.instrumentNo = Global.IntrumentNums;//仪器编号
              Global.AnHuiInterface.interfaceVersion = Global.InterfaceVer;

              for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
              {
                  sb.Length = 0;
                  sb.AppendFormat("ID={0}", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());
                  dt = sql.GetResult(sb.ToString(), "", 1, out errs);
                  if (dt != null && dt.Rows.Count > 0)
                  {
                      clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                      {
                          interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                          userName = Global.AnHuiInterface.userName,
                          instrument = Global.AnHuiInterface.instrument,
                          passWord = Global.AnHuiInterface.passWord
                      };
                      model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                      model.mac = Global.MacAddr;

                      model.gps = string.Empty;
                      model.fTpye = dt.Rows[0]["SampleCategoryCode"].ToString();//中类编号
                      model.fName = dt.Rows[0]["SampleName"].ToString();
                      model.tradeMark = string.Empty;
                      model.foodcode = string.Empty;
                      model.proBatch = string.Empty;
                      model.proDate = dt.Rows[0]["ProductDatetime"].ToString().Replace('/','-');//生产日期
                      model.proSpecifications = string.Empty;
                      model.manuUnit = string.Empty;
                      model.checkedNo = string.Empty;
                      model.sampleNo = dt.Rows[0]["SampleCode"].ToString().Length == 0 ? DateTime.Now.ToString("yyyyMMddHHmmss") : dt.Rows[0]["SampleCode"].ToString();
                      model.checkedUnit =dt.Rows[0]["ChkCompanyCode"].ToString().Length > 0 ? dt.Rows[0]["ChkCompanyCode"].ToString() : "0";//CheckUnit  dt.Rows[0]["CheckUnit"].ToString();
                      model.dataNum = dt.Rows[0]["ChkNum"].ToString().Length == 0 ? model.sampleNo : dt.Rows[0]["ChkNum"].ToString();
                      model.testPro = dt.Rows[0]["itemCode"].ToString().Length == 0 ? "默认检测项目" : dt.Rows[0]["itemCode"].ToString();
                      model.quanOrQual = dt.Rows[0]["ChkMethod"].ToString().Length == 0 ? "1" : dt.Rows[0]["ChkMethod"].ToString();
                      model.contents = dt.Rows[0]["CheckData"].ToString().Length == 0 ? "0" : dt.Rows[0]["CheckData"].ToString();
                      model.unit = dt.Rows[0]["Unit"].ToString();
                      model.testResult = dt.Rows[0]["Result"].ToString().Length == 0 ? "不合格" : dt.Rows[0]["Result"].ToString();
                      model.dilutionRa = string.Empty;
                      model.testRange = string.Empty;
                      model.standardLimit = dt.Rows[0]["LimitData"].ToString();
                      model.basedStandard = dt.Rows[0]["TestBase"].ToString();
                      model.testPerson = dt.Rows[0]["Tester"].ToString().Length == 0 ? Global.user_name: dt.Rows[0]["Tester"].ToString();
                      model.testTime = dt.Rows[0]["CheckTime"].ToString().Replace('/', '-');
                      model.sampleTime = dt.Rows[0]["CheckTime"].ToString().Replace('/', '-').Length == 0 ? model.testTime : dt.Rows[0]["CheckTime"].ToString().Replace('/', '-');
                      model.remark = dt.Rows[0]["DoResult"].ToString();
                      model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.testTime +
                          model.instrumentNo + model.contents + model.testResult);

                      string str = Global.AnHuiInterface.instrumentInfoHandle(model);
                      List<string> rtnList = Global.AnHuiInterface.ParsingUploadXML(str);
                      if (rtnList[0].Equals("1"))
                      {
                          icount++;
                          sql.SetUploadResult(CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString(), out errs);
                      }
                      else
                      {
                          fcount++;
                          outErr += "样品名称：[" + model.fName + "] 上传失败！\r\n异常信息：" + rtnList[1] + "；\r\n\r\n";
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              outErr = ex.Message;
              icount = 0;
          }
          return outErr;
      }
        /// <summary>
        /// 导入仪器数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      private void btnLoadMachine_Click(object sender, EventArgs e)
      {
          try
          {

              OpenFileDialog openFileDialog=new OpenFileDialog();
              //openFileDialog.InitialDirectory="c:\\";//注意这里写路径时要用c:\\而不是c:\
              openFileDialog.Filter="仪器数据|*.csv|所有文件|*.*";
              openFileDialog.RestoreDirectory=true;
              openFileDialog.FilterIndex=1;
              if (openFileDialog.ShowDialog() == DialogResult.OK)
              {
                  string Path = openFileDialog.FileName;
                  FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.None);
                  StreamReader sr = new StreamReader(fs, System.Text.UTF8Encoding.Default);

                  string str = "";
                  string s = Console.ReadLine();
                  int i = 0;
                  while (str != null)
                  {
                      str = sr.ReadLine();
                      if (str == null)
                          break;
                      string[] xu = new String[5];
                      xu = str.Split(',');
                      string F_Date = xu[0];
                      string F_Fax = xu[1].Replace("\"", "");//过滤双引号  
                      string F_ScanId = xu[2].Replace("\"", "");
                      string F_StoreNumber = xu[3];
                      string F_Num2 = xu[4];
                      DateTime time = DateTime.Now;
                      if (F_Fax == s)
                      {
                          //Log.info(F_Fax); 
                          break;
                      }
                      if (i != 0)//过滤表头  
                      {
                          //sql.Put("yearmonth", F_Date);
                          //sql.Put("shop_no", F_Fax);
                          //sql.Put("JAN", F_ScanId);
                          //sql.Put("pro_count", F_StoreNumber);
                          //sql.Put("real_count", F_Num2);
                          //sql.Put("insert_time", time);
                          //db.Execute(sql.GetSQL(InventorySql.InsertInventory)).ToString();
                      }
                      i++;
                  }
                  sr.Close();
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message);
          }  
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

