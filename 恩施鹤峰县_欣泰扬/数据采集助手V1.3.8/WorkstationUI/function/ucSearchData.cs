﻿using System;
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
using System.Xml;

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
                CheckDatasdd.Columns["HoleNum"].Visible = false;
                CheckDatasdd.Columns["BID"].Visible = false;
                CheckDatasdd.Columns["Xiguangdu"].Visible = false;
                CheckDatasdd.Columns["TID"].Visible = false;
                CheckDatasdd.Columns["HoleNum"].Visible =false;
                CheckDatasdd.Columns["CompanyNeture"].Visible =false;
                CheckDatasdd.Columns["ProduceUnit"].Visible = false;
                CheckDatasdd.Columns["SampleTime"].Visible = false;
                CheckDatasdd.Columns["SampleAddress"].Visible = false;
                CheckDatasdd.Columns["ProductDatetime"].Visible = false;
                CheckDatasdd.Columns["SendTestDate"].Visible = false;
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
                DialogResult D = MessageBox.Show("导出选中的记录单击 [是(Y)]，导出全部记录选择 [否(N)]", "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (D == DialogResult.No)//导出全部记录
                {
                    List<clsReportData> RD = new List<clsReportData>();//创建数据集合
                    RD.Clear();
                    foreach (DataGridViewRow dgvr in CheckDatasdd.Rows)
                    {
                        RD.Add(new clsReportData()//向数据集合添加数据
                        {
                            ID = dgvr.Cells["ID"].Value.ToString(),
                            CheckNumber = dgvr.Cells["ChkNum"].Value.ToString(),
                            SampleName = dgvr.Cells["SampleName"].Value.ToString(),
                            SampleCode = (dgvr.Cells["SampleCode"].Value.ToString() == null) ? "" : ("'" + dgvr.Cells["SampleCode"].Value.ToString()),
                            Checkitem = dgvr.Cells["Checkitem"].Value.ToString(),
                            XiGuangDu = dgvr.Cells["Xiguangdu"].Value.ToString(),//吸光度
                            CheckData = dgvr.Cells["CheckData"].Value.ToString(),
                            Unit = dgvr.Cells["Unit"].Value.ToString(),
                            testbase = dgvr.Cells["TestBase"].Value.ToString(),
                            limitdata = dgvr.Cells["LimitData"].Value.ToString(),
                            machine = dgvr.Cells["Machine"].Value.ToString(),
                            MachineNumber = dgvr.Cells["MachineNum"].Value.ToString(),
                            Result = dgvr.Cells["Result"].Value.ToString(),
                            dectectunit = dgvr.Cells["DetectUnit"].Value.ToString(),
                            gettime = dgvr.Cells["SampleTime"].Value.ToString(),
                            getplace = dgvr.Cells["SampleAddress"].Value.ToString(),
                            CheckUnit = dgvr.Cells["CheckUnit"].Value.ToString(),
                            CheckUnitNature = dgvr.Cells["CompanyNeture"].Value.ToString(),
                            tester = dgvr.Cells["Tester"].Value.ToString(),
                            CheckTime = dgvr.Cells["CheckTime"].Value.ToString(),
                            CompanyNeture = dgvr.Cells["CompanyNeture"].Value.ToString(),
                            //plannum = CheckDatas.SelectedRows[k].Cells[11].Value.ToString(),
                            stockin = dgvr.Cells["StockIn"].Value.ToString(),
                            sampleNum = dgvr.Cells["SampleNum"].Value.ToString(),
                            sampletype = dgvr.Cells["SampleCategory"].Value.ToString(),
                            numUnit = dgvr.Cells["NumberUnit"].Value.ToString(),
                            Barcode = dgvr.Cells["Barcode"].Value.ToString(),
                            productUnit = dgvr.Cells["ProduceUnit"].Value.ToString(),
                            productAddr = dgvr.Cells["ProduceAddr"].Value.ToString(),
                            ProductCompany = dgvr.Cells["ProcodeCompany"].Value.ToString(),
                            Addr = dgvr.Cells["ProductPlace"].Value.ToString(),
                            ProductDate = dgvr.Cells["ProductDatetime"].Value.ToString(),
                            SendDate = dgvr.Cells["SendTestDate"].Value.ToString(),
                            Doresult = dgvr.Cells["DoResult"].Value.ToString(),
                            IsUpload = dgvr.Cells["IsUpload"].Value.ToString(),
                            HoleNum = dgvr.Cells["HoleNum"].Value.ToString(),

                            ////GridNum = dgvr.Cells[0].Value.ToString(),
                            //SampleName = dgvr.Cells["样品名称"].Value.ToString(),
                            //Checkitem = dgvr.Cells["检测项目"].Value.ToString(),
                            //CheckData = dgvr.Cells["检测结果"].Value.ToString(),
                            //Unit = dgvr.Cells["单位"].Value.ToString(),
                            //testbase = dgvr.Cells["检测依据"].Value.ToString(),
                            //limitdata = dgvr.Cells["标准值"].Value.ToString(),
                            //machine = dgvr.Cells["检测仪器"].Value.ToString(),
                            //Result = dgvr.Cells["结论"].Value.ToString(),
                            //dectectunit = dgvr.Cells["检测单位"].Value.ToString(),
                            //gettime = dgvr.Cells["采样时间"].Value.ToString(),
                            //getplace = dgvr.Cells["采样地点"].Value.ToString(),
                            //CheckUnit = dgvr.Cells["被检单位"].Value.ToString(),
                            //CheckUnitNature = dgvr.Cells["被检单位性质"].Value.ToString(),
                            //tester = dgvr.Cells["检测员"].Value.ToString(),
                            //CheckTime = dgvr.Cells["检测时间"].Value.ToString(),
                            ////plannum = dgvr.Cells[11].Value.ToString(),
                            ////stockin = dgvr.Cells[13].Value.ToString(),
                            //sampleNum = dgvr.Cells["检测数量"].Value.ToString(),
                            //sampletype = dgvr.Cells["样品种类"].Value.ToString(),
                            //numUnit = dgvr.Cells["数量单位"].Value.ToString(),
                            //Barcode = dgvr.Cells["条形码"].Value.ToString(),
                            //productUnit = dgvr.Cells["生产单位"].Value.ToString(),
                            //productAddr = dgvr.Cells["产地地址"].Value.ToString(),
                            //ProductCompany = dgvr.Cells["生产企业"].Value.ToString(),
                            //Addr = dgvr.Cells["产地"].Value.ToString(),
                            //ProductDate = dgvr.Cells["生产日期"].Value.ToString(),
                            //SendDate = dgvr.Cells["送检日期"].Value.ToString(),
                            //Doresult = dgvr.Cells["处理结果"].Value.ToString(),
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
                            P_ws.Name = "resultdata";
                            //生成字段名
                            for (int j = 0; j < CheckDatasdd.ColumnCount; j++)
                            {
                                //if (CheckDatasdd.Columns[j].HeaderText == "sampleid" || CheckDatasdd.Columns[j].HeaderText == "BID")
                                //{
                                //    continue;
                                //}
                                P_ws.Cells[1, j + 1] = CheckDatasdd.Columns[j].HeaderText;
                            }
                            int FormatNum;//保存excel文件的格式
                            string Version;//excel版本号
                            Excel.Application Application = new Excel.Application();
                            //Excel.Workbook workbook = (Excel.Workbook)Application.Workbooks.Add(G_missing);//激活工作簿
                            //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.Add(true);//给工作簿添加一个sheet
                            //worksheet.Name = "resultdata";
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
                                P_ws.Cells[i + 2, 1] = RD[i].ID.ToString();//ID
                                P_ws.Cells[i + 2, 2] = RD[i].CheckNumber.ToString();//检测编号
                                P_ws.Cells[i + 2, 3] = RD[i].Checkitem.ToString();//检测项目
                                P_ws.Cells[i + 2, 4] = RD[i].SampleName.ToString();//样品编号
                                P_ws.Cells[i + 2, 5] = RD[i].XiGuangDu.ToString();//样品编号
                                P_ws.Cells[i + 2, 6] = RD[i].CheckData.ToString();//检测结果
                                P_ws.Cells[i + 2, 7] = RD[i].Unit.ToString();//单位
                                P_ws.Cells[i + 2, 8] = RD[i].CheckTime.ToString();//检测时间
                                P_ws.Cells[i + 2, 9] = RD[i].CheckUnit.ToString();//被检单位
                                P_ws.Cells[i + 2, 10] = RD[i].Result.ToString();//结论
                                P_ws.Cells[i + 2, 11] = RD[i].IsSave.ToString();//已保存
                                P_ws.Cells[i + 2, 12] = RD[i].machine.ToString();//检测仪器
                                P_ws.Cells[i + 2, 13] = RD[i].gettime.ToString();//采样时间
                                P_ws.Cells[i + 2, 14] = RD[i].getplace.ToString();//采样地点
                                P_ws.Cells[i + 2, 15] = RD[i].dectectunit.ToString();//检测单位
                                P_ws.Cells[i + 2, 16] = RD[i].testbase.ToString();//检测依据
                                P_ws.Cells[i + 2, 17] = RD[i].limitdata.ToString();//标准值
                                P_ws.Cells[i + 2, 18] = RD[i].tester.ToString();//检测员
                                P_ws.Cells[i + 2, 19] = RD[i].stockin.ToString();//进货数量
                                P_ws.Cells[i + 2, 20] = RD[i].sampleNum.ToString();//样品数量
                                P_ws.Cells[i + 2, 21] = RD[i].IsUpload.ToString();//已上传
                                P_ws.Cells[i + 2, 22] = RD[i].SampleCode.ToString();//样品编号
                                P_ws.Cells[i + 2, 23] = RD[i].sampletype.ToString();//样品种类
                                P_ws.Cells[i + 2, 24] = RD[i].MachineNumber.ToString();//仪器编号
                                P_ws.Cells[i + 2, 25] = RD[i].Addr.ToString();//样品产地
                                P_ws.Cells[i + 2, 26] = RD[i].ProductDate.ToString();//生产日期
                                P_ws.Cells[i + 2, 27] = RD[i].Barcode.ToString();//条形码
                                P_ws.Cells[i + 2, 28] = RD[i].ProductCompany.ToString();//生产企业
                                P_ws.Cells[i + 2, 29] = RD[i].productAddr.ToString();//产地地址
                                P_ws.Cells[i + 2, 30] = RD[i].productUnit.ToString();//生产单位
                                P_ws.Cells[i + 2, 31] = RD[i].SendDate.ToString();//送检日期
                                P_ws.Cells[i + 2, 32] = RD[i].numUnit.ToString();//数量单位
                                P_ws.Cells[i + 2, 33] = RD[i].CheckUnitNature.ToString();//被检单位性质
                                P_ws.Cells[i + 2, 34] = RD[i].Doresult.ToString();//处理结果
                                P_ws.Cells[i + 2, 36] = RD[i].HoleNum.ToString();//通道号
                                ////P_ws.Cells[i + 2, 1] = RD[i].GridNum;//检测编号
                                //P_ws.Cells[i + 2, 1] = RD[i].SampleName.ToString();//样品名称
                                //P_ws.Cells[i + 2, 2] = RD[i].Checkitem.ToString();//检测项目 
                                //P_ws.Cells[i + 2, 3] = RD[i].CheckData.ToString();//检测结果
                                //P_ws.Cells[i + 2, 4] = RD[i].Unit.ToString();//数值单位
                                //P_ws.Cells[i + 2, 5] = RD[i].testbase.ToString();//检测依据
                                //P_ws.Cells[i + 2, 6] = RD[i].limitdata.ToString();//标准值
                                //P_ws.Cells[i + 2, 7] = RD[i].machine.ToString();//检测仪器 
                                //P_ws.Cells[i + 2, 8] = RD[i].Result.ToString();//结论
                                //P_ws.Cells[i + 2, 9] = RD[i].dectectunit.ToString();//检测单位
                                //P_ws.Cells[i + 2, 10] = RD[i].gettime.ToString();//采样时间
                                //P_ws.Cells[i + 2, 11] = RD[i].getplace.ToString();//采样地点
                                //P_ws.Cells[i + 2, 12] = RD[i].CheckUnit.ToString();//被检单位 
                                //P_ws.Cells[i + 2, 13] = RD[i].CheckUnitNature.ToString();//被检单位性质
                                //P_ws.Cells[i + 2, 14] = RD[i].tester.ToString();//检定员
                                //P_ws.Cells[i + 2, 15] = RD[i].CheckTime.ToString();//检测时间

                                ////P_ws.Cells[i + 2, 12] = RD[i].plannum.ToString();//
                                ////P_ws.Cells[i + 2, 14] = RD[i].stockin.ToString();// 
                                //P_ws.Cells[i + 2, 16] = RD[i].sampleNum.ToString();//样品数量
                                //P_ws.Cells[i + 2, 17] = RD[i].sampletype.ToString();//样品种类
                                //P_ws.Cells[i + 2, 18] = RD[i].numUnit.ToString();
                                //P_ws.Cells[i + 2, 19] = RD[i].Barcode.ToString();
                                //P_ws.Cells[i + 2, 20] = RD[i].productUnit.ToString();
                                //P_ws.Cells[i + 2, 21] = RD[i].productAddr.ToString();
                                //P_ws.Cells[i + 2, 22] = RD[i].ProductCompany.ToString();
                                //P_ws.Cells[i + 2, 23] = RD[i].Addr.ToString();
                                //P_ws.Cells[i + 2, 24] = RD[i].ProductDate.ToString();
                                //P_ws.Cells[i + 2, 25] = RD[i].SendDate.ToString();
                                //P_ws.Cells[i + 2, 27] = RD[i].Doresult.ToString();//处理结果

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
                    if (CheckDatasdd.SelectedRows.Count < 1)
                    {
                        MessageBox.Show("请选择需要导出的记录", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    List<clsReportData> RD = new List<clsReportData>();//创建数据集合
                    RD.Clear();
                    for (int k = 0; k < CheckDatasdd.SelectedRows.Count; k++)
                    {
                        RD.Add(new clsReportData()//向数据集合添加数据
                        {
                            ID = CheckDatasdd.SelectedRows[k].Cells["ID"].Value.ToString(),
                            CheckNumber = CheckDatasdd.SelectedRows[k].Cells["ChkNum"].Value.ToString(),
                            SampleName = CheckDatasdd.SelectedRows[k].Cells["SampleName"].Value.ToString(),
                            SampleCode =(CheckDatasdd.SelectedRows[k].Cells["SampleCode"].Value.ToString()==null) ?"":("'"+ CheckDatasdd.SelectedRows[k].Cells["SampleCode"].Value.ToString()),
                            Checkitem = CheckDatasdd.SelectedRows[k].Cells["Checkitem"].Value.ToString(),
                            XiGuangDu = CheckDatasdd.SelectedRows[k].Cells["Xiguangdu"].Value.ToString(),
                            CheckData = CheckDatasdd.SelectedRows[k].Cells["CheckData"].Value.ToString(),
                            Unit = CheckDatasdd.SelectedRows[k].Cells["Unit"].Value.ToString(),
                            testbase = CheckDatasdd.SelectedRows[k].Cells["TestBase"].Value.ToString(),
                            limitdata = CheckDatasdd.SelectedRows[k].Cells["LimitData"].Value.ToString(),
                            machine = CheckDatasdd.SelectedRows[k].Cells["Machine"].Value.ToString(),
                            MachineNumber = CheckDatasdd.SelectedRows[k].Cells["MachineNum"].Value.ToString(),
                            Result = CheckDatasdd.SelectedRows[k].Cells["Result"].Value.ToString(),
                            dectectunit = CheckDatasdd.SelectedRows[k].Cells["DetectUnit"].Value.ToString(),
                            gettime = CheckDatasdd.SelectedRows[k].Cells["SampleTime"].Value.ToString(),
                            getplace = CheckDatasdd.SelectedRows[k].Cells["SampleAddress"].Value.ToString(),
                            CheckUnit = CheckDatasdd.SelectedRows[k].Cells["CheckUnit"].Value.ToString(),
                            CheckUnitNature = CheckDatasdd.SelectedRows[k].Cells["CompanyNeture"].Value.ToString(),
                            tester = CheckDatasdd.SelectedRows[k].Cells["Tester"].Value.ToString(),
                            CheckTime = CheckDatasdd.SelectedRows[k].Cells["CheckTime"].Value.ToString(),
                            CompanyNeture = CheckDatasdd.SelectedRows[k].Cells["CompanyNeture"].Value.ToString(),
                            //plannum = CheckDatas.SelectedRows[k].Cells[11].Value.ToString(),
                            stockin = CheckDatasdd.SelectedRows[k].Cells["StockIn"].Value.ToString(),
                            sampleNum = CheckDatasdd.SelectedRows[k].Cells["SampleNum"].Value.ToString(),
                            sampletype = CheckDatasdd.SelectedRows[k].Cells["SampleCategory"].Value.ToString(),
                            numUnit = CheckDatasdd.SelectedRows[k].Cells["NumberUnit"].Value.ToString(),
                            Barcode = CheckDatasdd.SelectedRows[k].Cells["Barcode"].Value.ToString(),
                            productUnit = CheckDatasdd.SelectedRows[k].Cells["ProduceUnit"].Value.ToString(),
                            productAddr = CheckDatasdd.SelectedRows[k].Cells["ProduceAddr"].Value.ToString(),
                            ProductCompany = CheckDatasdd.SelectedRows[k].Cells["ProcodeCompany"].Value.ToString(),
                            Addr = CheckDatasdd.SelectedRows[k].Cells["ProductPlace"].Value.ToString(),
                            ProductDate = CheckDatasdd.SelectedRows[k].Cells["ProductDatetime"].Value.ToString(),
                            SendDate = CheckDatasdd.SelectedRows[k].Cells["SendTestDate"].Value.ToString(),
                            Doresult = CheckDatasdd.SelectedRows[k].Cells["DoResult"].Value.ToString(),
                            IsUpload = CheckDatasdd.SelectedRows[k].Cells["IsUpload"].Value.ToString(),
                            HoleNum = CheckDatasdd.SelectedRows[k].Cells["HoleNum"].Value.ToString(),

                            ////GridNum = CheckDatas.SelectedRows[k].Cells[0].Value.ToString(),
                            //SampleName = CheckDatas.SelectedRows[k].Cells["样品名称"].Value.ToString(),
                            //Checkitem = CheckDatas.SelectedRows[k].Cells["检测项目"].Value.ToString(),
                            //CheckData = CheckDatas.SelectedRows[k].Cells["检测结果"].Value.ToString(),
                            //Unit = CheckDatas.SelectedRows[k].Cells["单位"].Value.ToString(),
                            //testbase = CheckDatas.SelectedRows[k].Cells["检测依据"].Value.ToString(),
                            //limitdata = CheckDatas.SelectedRows[k].Cells["标准值"].Value.ToString(),
                            //machine = CheckDatas.SelectedRows[k].Cells["检测仪器"].Value.ToString(),
                            //Result = CheckDatas.SelectedRows[k].Cells["结论"].Value.ToString(),
                            //dectectunit = CheckDatas.SelectedRows[k].Cells["检测单位"].Value.ToString(),
                            //gettime = CheckDatas.SelectedRows[k].Cells["采样时间"].Value.ToString(),
                            //getplace = CheckDatas.SelectedRows[k].Cells["采样地点"].Value.ToString(),
                            //CheckUnit = CheckDatas.SelectedRows[k].Cells["被检单位"].Value.ToString(),
                            //CheckUnitNature = CheckDatas.SelectedRows[k].Cells["被检单位性质"].Value.ToString(),
                            //tester = CheckDatas.SelectedRows[k].Cells["检测员"].Value.ToString(),
                            //CheckTime = CheckDatas.SelectedRows[k].Cells["检测时间"].Value.ToString(),

                            ////plannum = CheckDatas.SelectedRows[k].Cells[11].Value.ToString(),
                            ////stockin = CheckDatas.SelectedRows[k].Cells[13].Value.ToString(),
                            //sampleNum = CheckDatas.SelectedRows[k].Cells["检测数量"].Value.ToString(),
                            //sampletype = CheckDatas.SelectedRows[k].Cells["样品种类"].Value.ToString(),
                            //numUnit = CheckDatas.SelectedRows[k].Cells["数量单位"].Value.ToString(),
                            //Barcode = CheckDatas.SelectedRows[k].Cells["条形码"].Value.ToString(),
                            //productUnit = CheckDatas.SelectedRows[k].Cells["生产单位"].Value.ToString(),
                            //productAddr = CheckDatas.SelectedRows[k].Cells["产地地址"].Value.ToString(),
                            //ProductCompany = CheckDatas.SelectedRows[k].Cells["生产企业"].Value.ToString(),
                            //Addr = CheckDatas.SelectedRows[k].Cells["产地"].Value.ToString(),
                            //ProductDate = CheckDatas.SelectedRows[k].Cells["生产日期"].Value.ToString(),
                            //SendDate = CheckDatas.SelectedRows[k].Cells["送检日期"].Value.ToString(),
                            //Doresult = CheckDatas.SelectedRows[k].Cells["处理结果"].Value.ToString(),
                        });
                    }
                    SaveFileDialog P_SaveFileDialog = new SaveFileDialog();//创建保存文件对话框对象
                    P_SaveFileDialog.Filter = "*.xls|*.xls";
                    if (DialogResult.OK == P_SaveFileDialog.ShowDialog())//确认是否保存文件                
                    {
                        ThreadPool.QueueUserWorkItem(//开始线程池
                        (pp) =>//使用lambda表达式
                        {
                            //建立excel对象
                            Excel.Application excel = new Excel.Application();
                            excel.Application.Workbooks.Add(true);

                            G_ea = new Microsoft.Office.Interop.Excel.Application();//创建应用程序对象
                            Excel.Workbook P_wk = G_ea.Workbooks.Add(G_missing);//创建Excel文档
                            Excel.Worksheet P_ws = (Excel.Worksheet)P_wk.Worksheets.Add(G_missing, G_missing, G_missing, G_missing);//创建工作区域
                            P_ws.Name = "resultdata";
                            //生成字段名
                            for (int j = 0; j < CheckDatasdd.ColumnCount; j++)
                            {
                                //if (CheckDatasdd.Columns[j].HeaderText == "sampleid" || CheckDatasdd.Columns[j].HeaderText == "BID")
                                //{
                                //    continue;
                                //}
                                P_ws.Cells[1, j + 1] = CheckDatasdd.Columns[j].HeaderText;
                            }
                            //填充数据
                            for (int i = 0; i < RD.Count; i++)
                            {
                                P_ws.Cells[i + 2, 1] = RD[i].ID.ToString();//ID
                                P_ws.Cells[i + 2, 2] = RD[i].CheckNumber.ToString();//检测编号
                                P_ws.Cells[i + 2, 3] = RD[i].Checkitem.ToString();//检测项目
                                P_ws.Cells[i + 2, 4] = RD[i].SampleName.ToString();//样品名称
                                P_ws.Cells[i + 2, 5] = RD[i].XiGuangDu.ToString();//吸光度
                                P_ws.Cells[i + 2, 6] = RD[i].CheckData.ToString();//检测结果
                                P_ws.Cells[i + 2, 7] = RD[i].Unit.ToString();//单位
                                P_ws.Cells[i + 2, 8] = RD[i].CheckTime.ToString();//检测时间
                                P_ws.Cells[i + 2, 9] = RD[i].CheckUnit.ToString();//被检单位
                                P_ws.Cells[i + 2, 10] = RD[i].Result.ToString();//结论
                                P_ws.Cells[i + 2, 11] = RD[i].IsSave.ToString();//已保存
                                P_ws.Cells[i + 2, 12] = RD[i].machine.ToString();//检测仪器
                                P_ws.Cells[i + 2, 13] = RD[i].gettime.ToString();//采样时间
                                P_ws.Cells[i + 2, 14] = RD[i].getplace.ToString();//采样地点
                                P_ws.Cells[i + 2, 15] = RD[i].dectectunit.ToString();//检测单位
                                P_ws.Cells[i + 2, 16] = RD[i].testbase.ToString();//检测依据
                                P_ws.Cells[i + 2, 17] = RD[i].limitdata.ToString();//标准值
                                P_ws.Cells[i + 2, 18] = RD[i].tester.ToString();//检测员
                                P_ws.Cells[i + 2, 19] = RD[i].stockin.ToString();//进货数量
                                P_ws.Cells[i + 2, 20] = RD[i].sampleNum.ToString();//样品数量
                                P_ws.Cells[i + 2, 21] = RD[i].IsUpload .ToString();//已上传
                                P_ws.Cells[i + 2, 22] = RD[i].SampleCode.ToString();//样品编号
                                P_ws.Cells[i + 2, 23] = RD[i].sampletype.ToString();//样品种类
                                P_ws.Cells[i + 2, 24] = RD[i].MachineNumber.ToString();//仪器编号
                                P_ws.Cells[i + 2, 25] = RD[i].Addr.ToString();//样品产地
                                P_ws.Cells[i + 2, 26] = RD[i].ProductDate.ToString();//生产日期
                                P_ws.Cells[i + 2, 27] = RD[i].Barcode.ToString();//条形码
                                P_ws.Cells[i + 2, 28] = RD[i].ProductCompany.ToString();//生产企业
                                P_ws.Cells[i + 2, 29] = RD[i].productAddr.ToString();//产地地址
                                P_ws.Cells[i + 2, 30] = RD[i].productUnit.ToString();//生产单位
                                P_ws.Cells[i + 2, 31] = RD[i].SendDate.ToString();//送检日期
                                P_ws.Cells[i + 2, 32] = RD[i].numUnit.ToString();//数量单位
                                P_ws.Cells[i + 2, 33] = RD[i].CheckUnitNature.ToString();//被检单位性质
                                P_ws.Cells[i + 2, 34] = RD[i].Doresult.ToString();//处理结果
                                P_ws.Cells[i + 2, 36] = RD[i].HoleNum.ToString();//通道号
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
                openFileDialog1.Filter="Excel文件|*.xls";//设置打开文件筛选器
                openFileDialog1.Title = "打开Excel文件";//设置打开对话框标题
                ExcelPath = openFileDialog1.FileName;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件
                {
                    //连接Excel数据库
                    OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'");//没有单引号就会报错
                    olecon.Open();//打开数据库连接
                    OleDbDataAdapter oledbda = new OleDbDataAdapter("select * from [" + "resultdata" + "$]", olecon);//从工作表中查询数据
                    DataSet myds = new DataSet();//实例化数据集对象
                    oledbda.Fill(myds);//填充数据集              
                    //int P_int_Counts = myds.Tables[0].Rows.Count;
                    for (int r = 0; r < myds.Tables[0].Rows.Count; r++)
                    {
                        //InData.Gridnum = myds.Tables[0].Rows[r][0].ToString();
                        InData.CheckNumber = Global.GUID(null, 1);
                        InData.Checkitem = myds.Tables[0].Rows[r]["检测项目"].ToString();
                        InData.SampleName = myds.Tables[0].Rows[r]["样品名称"].ToString();
                        InData.CheckData = myds.Tables[0].Rows[r]["检测结果"].ToString();
                        InData.Unit = myds.Tables[0].Rows[r]["单位"].ToString();
                        InData.CheckTime = DateTime.Parse(myds.Tables[0].Rows[r]["检测时间"].ToString().Replace("-", "/"));
                        InData.CheckUnit = myds.Tables[0].Rows[r]["被检单位"].ToString();//被检单位
                        InData.Result = myds.Tables[0].Rows[r]["结论"].ToString();
                        InData.Save = myds.Tables[0].Rows[r]["已保存"].ToString();//是否已保存
                        InData.Instrument = myds.Tables[0].Rows[r]["检测仪器"].ToString();
                        InData.Gettime = myds.Tables[0].Rows[r]["采样时间"].ToString();
                        InData.Getplace = myds.Tables[0].Rows[r]["采样地点"].ToString();
                        InData.detectunit = myds.Tables[0].Rows[r]["检测单位"].ToString();
                        InData.Testbase = myds.Tables[0].Rows[r]["检测依据"].ToString();
                        InData.LimitData = myds.Tables[0].Rows[r]["标准值"].ToString();
                        InData.Tester = myds.Tables[0].Rows[r]["检测员"].ToString();
                        InData.stockin = myds.Tables[0].Rows[r]["进货数量"].ToString();
                        InData.sampleNum = myds.Tables[0].Rows[r]["样品数量"].ToString();
                        InData.IsUpLoad = myds.Tables[0].Rows[r]["已上传"].ToString();
                        InData.SampleCode = myds.Tables[0].Rows[r]["样品编号"].ToString();
                        InData.SampleType = myds.Tables[0].Rows[r]["样品种类"].ToString();
                        InData.IntrumentNum = myds.Tables[0].Rows[r]["仪器编号"].ToString();
                        InData.Addr = myds.Tables[0].Rows[r]["样品产地"].ToString();
                        InData.ProductDate = myds.Tables[0].Rows[r]["生产日期"].ToString();
                        InData.Barcode = myds.Tables[0].Rows[r]["条形码"].ToString();
                        InData.ProductCompany = myds.Tables[0].Rows[r]["生产企业"].ToString();
                        InData.productAddr = myds.Tables[0].Rows[r]["产地地址"].ToString();
                        InData.productUnit = myds.Tables[0].Rows[r]["生产单位"].ToString();
                        InData.SendDate = myds.Tables[0].Rows[r]["送检日期"].ToString();
                        InData.numUnit = myds.Tables[0].Rows[r]["数量单位"].ToString();
                        InData.CheckUnitNature = myds.Tables[0].Rows[r]["被检单位性质"].ToString();//被检单位性质
                        InData.TreatResult  = myds.Tables[0].Rows[r]["处理结果"].ToString();
                        InData.Xiguagndu = myds.Tables[0].Rows[r]["吸光度"].ToString();
                        InData.HoleNumber = myds.Tables[0].Rows[r]["通道号"].ToString();

                        re=sql.LoadInData(InData, out err);
                        if (re == 1)
                        {
                            n = n + 1;
                        }
                    }
                    MessageBox.Show("共成功导入"+n+"条数据","操作提示");
                    //刷新数据
                    btnfind_Click(null, null);
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
              string errstr="";
              int isupdata = 0;
              int failItem = 0;
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
              else if (Global.Platform == "hexian")//分公司鹤县数据上传
              {
                  if(Global.Ticket =="")
                  {
                      MessageBox.Show("上传失败！！！\r\n上传数据前请先进行通信测试","提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                      btnUpload.Enabled = true;
                      return;
                  }
                  
                  string mesg = "";
                  for (int i = 0; i < CheckDatasdd.SelectedRows.Count; i++)
                  {
                      sb.Length = 0;
                      sb.AppendFormat("ID={0}", CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString());
                      dt = sql.GetResult(sb.ToString(), "", 1, out err);
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          string xml = HXUpLoad.UpLoadXML(dt);//上传数据到平台
                          com.tainot.foodsafe.wireDataService wds = new com.tainot.foodsafe.wireDataService();
                          string rt = wds.Upload(Global.Ticket, xml);
                          if (rt != "")
                          {
                              XmlDocument xmlDoc = new XmlDocument();
                              xmlDoc.LoadXml(rt);//加载XML字符串为XML文档
                              //查找<response>    
                              XmlNode root = xmlDoc.SelectSingleNode("response");
                              //获取到所有<response>的子节点    
                              XmlNodeList nodeList = xmlDoc.SelectSingleNode("response").ChildNodes;
                              string success = "";
                              string errMsg = "";
                              //遍历所有子节点   
                              foreach (XmlNode xn in nodeList)
                              {
                                  string name = xn.Name;
                                  Console.WriteLine(name);

                                  XmlElement xe = (XmlElement)xn;
                                  XmlNodeList subList = xe.ChildNodes;

                                  if (xn.Name == "head")
                                  {
                                      foreach (XmlNode xmlNode in subList)
                                      {
                                          if ("success_error".Equals(xmlNode.Name))
                                          {
                                              success = xmlNode.InnerText;
                                              Console.WriteLine("成功：" + xmlNode.InnerText);
                                          }
                                          else if ("success_count".Equals(xmlNode.Name))
                                          {
                                              Console.WriteLine("成功：" + xmlNode.InnerText);
                                          }
                                          else if ("error_count".Equals(xmlNode.Name))
                                          {
                                              Console.WriteLine("成功：" + xmlNode.InnerText);
                                          }
                                      }
                                  }
                                  else if (xn.Name == "success_dataset")
                                  {
                                      foreach (XmlNode xmlNode in subList)
                                      {
                                          if ("data".Equals(xmlNode.Name))
                                          {
                                              string id = xmlNode.Attributes["id"].Value;//通过Attributes获得属性名为id的属性
                                          }
                                      }

                                  }
                                  else if (xn.Name == "error_dataset")
                                  {
                                      foreach (XmlNode xmlNode in subList)
                                      {
                                          if ("data".Equals(xmlNode.Name))
                                          {
                                              string id = xmlNode.Attributes["id"].Value;
                                              string errs = xmlNode.Attributes["error_code"].Value;//通过Attributes获得属性名为id的属性
                                              errMsg = errs;
                                          }
                                      }
                                  }
                              }
                              if (success != "1")
                              {
                                  if (errMsg == "0001")
                                  {
                                      mesg = "字符串长度过长";
                                  }
                                  else if (errMsg == "0002")
                                  {
                                      mesg = "时间格式不正确";
                                  }
                                  else if (errMsg == "0003")
                                  {
                                      mesg = "数值格式不正确";
                                  }
                                  else if (errMsg == "0011")
                                  {
                                      mesg = "检测设备号不匹配";
                                  }
                                  else if (errMsg == "0012")
                                  {
                                      mesg = "检测样品号不匹配";
                                  }
                                  else if (errMsg == "0013")
                                  {
                                      mesg = "检测项目号不匹配";
                                  }
                                  else if (errMsg == "0014")
                                  {
                                      mesg = "产地号不匹配";
                                  }
                                  else if (errMsg == "0015")
                                  {
                                      mesg = "检测单位号不匹配";
                                  }
                                  else if (errMsg == "0016")
                                  {
                                      mesg = "被检单位号不匹配";
                                  }
                                  else if (errMsg == "0091")
                                  {
                                      mesg = "为知错误";
                                  }
                                  errstr = errstr + mesg;
                                  failItem++;
                              }
                              else if (success == "1")
                              {
                                  IsSuccess = IsSuccess + 1;
                                  sql.SetUploadResult(CheckDatasdd.SelectedRows[i].Cells["ID"].Value.ToString(), out err);
                              }

                          }
                      }
                  }
                  if (errstr == "")
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据！", "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  else
                  {
                      MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据；共" + isupdata + "条数据已传！\r\n共" + failItem + "条数据上传失败 失败原因：" + errstr, "数据上传",MessageBoxButtons.OK ,MessageBoxIcon.Information );
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

