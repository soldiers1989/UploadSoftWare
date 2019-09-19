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
using WorkstationModel.UpData;
using WorkstationDAL.UpLoadData;
using WorkstationModel.Model;
using WorkstationModel.beihai;
using DY.Process;
using WorkstationDAL.yc;
namespace WorkstationUI.function
{
    public partial class ucAddUnit : UserControl
    {
        private string rtndata = string.Empty;
        private ResultData resultd = null;
        private clsSetSqlData sql = new clsSetSqlData();
        private bool m_IsCreatedDataTable = false;
        private bool BusCreateTable = false;
        private DataTable displayTable = null;
        private DataTable Searchtable = null;
        private StringBuilder sb = new StringBuilder();
        private clsdiary dy = new clsdiary();
        private string err = "";
        private DataTable dt = null;
        private YCsql _sql = new YCsql();

        /// <summary>
        /// 下载的被检单位集合
        /// </summary>
        private List<Company> downLoadCompanyList = null;

        public ucAddUnit()
        {
            InitializeComponent();
        }
      
        ///// <summary>
        ///// 保存
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnsave_Click(object sender, EventArgs e)
        //{
        //    //Global.TestUnitName = txttestunit.Text;
        //    //Global.TestUnitAddr = txtunitaddr.Text;
        //    //Global.DetectUnitName = txtdetectunit.Text;
        //    //Global.SampleAddress = txtdetectaddr.Text;
        //    //Global.StockInNum = txtinnum.Text;
        //    //Global.SampleNum = txtsamplenum.Text;
        //    //Global.GetTime = DTPsampletime.Value.ToString();
        //    //Global.CheckBase = txttestbase.Text;
        //    try
        //    {
        //        string err=string.Empty ;
        //        sql.Delete("BasicInformation",out err);
        //        StringBuilder sb=new StringBuilder();
        //        //sb.Append(txttestunit.Text);
        //        //sb.Append("','");
        //        //sb.Append(txtunitaddr.Text);
        //        //sb.Append("','");
        //        //sb.Append(txtdetectunit.Text);
        //        //sb.Append("','");
        //        //sb.Append(txtdetectaddr.Text);
        //        //sb.Append("','");
        //        //sb.Append(txtinnum.Text);
        //        //sb.Append("','");
        //        //sb.Append(txtsamplenum.Text);
        //        //sb.Append("','");
        //        //sb.Append(DTPsampletime.Value);
        //        //sb.Append("','");
        //        //sb.Append(txttestbase.Text);
        //        sql.SaveInformation(sb.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message,"Error");
        //    }
        //    MessageBox.Show("数据保存成功","提示");
        //}

        private void ucAddUnit_Load(object sender, EventArgs e)
        {
            if(Global.Platform =="DYBus")//快检车
            {
                iniTable();
                btnDownUnit.Visible = true;
                btnDownUnit.Location = new Point(28,48);
            }
            else if (Global.Platform == "DYKJFW")//快检服务
            {
                BtnDeletesupervis.Visible = true;
                BtnDownSupervis.Visible = true;
                BtnReSupervis.Visible = true;
                BtnDownSupervis.Location = new Point(28, 48);
                BtnReSupervis.Location = new Point(138, 48);
                BtnDeletesupervis.Location = new Point(238, 48);
               
            }
            else
            {
                //IniBusTable();//通用
                //btnadd.Visible = true;
                //btnrepair.Visible = true;
                //btnclear.Visible = true;
                //btnrefresh.Visible = true;
            }
            //BHtable();
            try
            {
                string err = string.Empty;
                //BSearchSample("", "IsTest='否'");
                if (Global.Platform == "DYBus")//快检车
                {
                    dt = sql.GetExamedUnit("", "", out err);
                    displayTable.Clear();
                }
                else if (Global.Platform == "DYKJFW")//快检服务
                {
                    dt = sql.GetRegulator("", "", 1);
                }
                else
                {
                    dt = _sql.GetYCOpertorTable("");
                    //displayTable.Clear();
                    //dt = sql.GetInformation("", "", out err);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Global.Platform == "DYBus")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            
                            addtable1(dt.Rows[i]["regId"].ToString(), dt.Rows[i]["regName"].ToString(), dt.Rows[i]["organizationCode"].ToString(), dt.Rows[i]["regAddress"].ToString(), dt.Rows[i]["contactMan"].ToString());
                        }
                        CheckDatas.DataSource = displayTable;
                    }
                    else if (Global.Platform == "DYKJFW")
                    {
                        CheckDatas.DataSource = dt;
                        CheckDatas.Columns["IsSelects"].HeaderText = "已选择";
                        CheckDatas.Columns["reg_name"].HeaderText = "被检单位名称";
                        CheckDatas.Columns["link_user"].HeaderText = "单位负责人";
                        CheckDatas.Columns["reg_address"].HeaderText = "地址";
                        CheckDatas.Columns["link_phone"].HeaderText = "联系信息";
                        CheckDatas.Columns["update_date"].HeaderText = "更新时间";
                        CheckDatas.Columns["rid"].HeaderText = "任务ID";
                        CheckDatas.Columns["reg_type"].HeaderText = "单位性质";
                        CheckDatas.Columns["ID"].Visible = false;
                    }
                    else
                    {
                        CheckDatas.DataSource = dt;
                        CheckDatas.Columns["ID"].Visible = false;
                        CheckDatas.Columns["stallNo"].HeaderText = "摊位号";
                        CheckDatas.Columns["operatorCode"].HeaderText = "经营户编号";
                        CheckDatas.Columns["operatorName"].HeaderText = "经营户";
                        CheckDatas.Columns["socialCreditCode"].HeaderText = "社会编号";
                        CheckDatas.Columns["marketName"].HeaderText = "市场名称";
                        CheckDatas.Columns["marketCode"].HeaderText = "市场编号";
                        CheckDatas.Columns["isselect"].HeaderText = "默认选择";

                        CheckDatas.Columns["isselect"].DisplayIndex = 1;
                        CheckDatas.Columns["marketName"].DisplayIndex = 2;
                        CheckDatas.Columns["marketCode"].DisplayIndex = 3;
                        CheckDatas.Columns["operatorName"].DisplayIndex = 4;
                        CheckDatas.Columns["operatorCode"].DisplayIndex = 5;
                        CheckDatas.Columns["stallNo"].DisplayIndex = 6;
                        CheckDatas.Columns["socialCreditCode"].DisplayIndex =7;
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                            
                        //    addtable(dt.Rows[i]["iChecked"].ToString(), dt.Rows[i]["TestUnitName"].ToString(), dt.Rows[i]["TestUnitAddr"].ToString(), dt.Rows[i]["Tester"].ToString(),
                        //    dt.Rows[i]["DetectUnitName"].ToString(), dt.Rows[i]["DetectUnitNature"].ToString(), dt.Rows[i]["ProductAddr"].ToString(), dt.Rows[i]["ProductCompany"].ToString());
                           
                        //}

                        //CheckDatas.DataSource = displayTable;
                       
                    }

                }
                //dy.savediary(DateTime.Now.ToString(), "进入单位/企业成功" , "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"进入单位/企业");
            }
        }
        private void SearchOpertor(string where)
        {
            try
            {
                dt = _sql.GetYCOpertorTable(where);
                CheckDatas.DataSource = dt;
                CheckDatas.Columns["ID"].Visible = false;
                CheckDatas.Columns["stallNo"].HeaderText = "摊位号";
                CheckDatas.Columns["operatorCode"].HeaderText = "经营户编号";
                CheckDatas.Columns["operatorName"].HeaderText = "经营户";
                CheckDatas.Columns["socialCreditCode"].HeaderText = "社会编号";
                CheckDatas.Columns["marketName"].HeaderText = "市场名称";
                CheckDatas.Columns["marketCode"].HeaderText = "市场编号";
                CheckDatas.Columns["isselect"].HeaderText = "默认选择";

                CheckDatas.Columns["isselect"].DisplayIndex = 1;
                CheckDatas.Columns["marketName"].DisplayIndex =2;
                CheckDatas.Columns["marketCode"].DisplayIndex =3;
                CheckDatas.Columns["operatorName"].DisplayIndex =4;
                CheckDatas.Columns["operatorCode"].DisplayIndex = 5;
                CheckDatas.Columns["stallNo"].DisplayIndex = 6;
                CheckDatas.Columns["socialCreditCode"].DisplayIndex = 7;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SearchKJFWRegulatory()
        {
            CheckDatas.DataSource = null;
            dt = sql.GetRegulator("", "", 1);
            if (dt != null && dt.Rows.Count>0)
            {
                CheckDatas.DataSource = dt;
                CheckDatas.Columns["reg_name"].HeaderText = "被检单位名称";
                CheckDatas.Columns["link_user"].HeaderText = "单位负责人";
                CheckDatas.Columns["reg_address"].HeaderText = "地址";
                CheckDatas.Columns["link_phone"].HeaderText = "联系信息";
                CheckDatas.Columns["update_date"].HeaderText = "更新时间";
                CheckDatas.Columns["rid"].HeaderText = "任务ID";
            }
            
        }

        //加载数据到表
        private void addtable1(string id, string unit, string unitNo, string ChkAddr, string Chker)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            //dr["默认选择"] = sel=="否"?false :true ;
            dr["关联ID"] = id;
            dr["被检单位"] = unit;
            dr["机构编号"] = unitNo;
            dr["被检单位地址"] = ChkAddr;
            dr["联系人"] = Chker;

            displayTable.Rows.Add(dr);
        }
        private void BSearchSample(string where, string orderby)
        {
            try
            {
                dt = sql.GetTestData(where, orderby, 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Searchtable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BHaddtable(dt.Rows[i]["goodsName"].ToString(), dt.Rows[i]["productId"].ToString(), dt.Rows[i]["marketName"].ToString()
                            , dt.Rows[i]["operateName"].ToString(), dt.Rows[i]["samplingTime"].ToString(), dt.Rows[i]["positionAddress"].ToString()
                            , dt.Rows[i]["samplingPerson"].ToString(), dt.Rows[i]["IsTest"].ToString(), dt.Rows[i]["ID"].ToString());
                    }
                    CheckDatas.DataSource = Searchtable;
                    //for(int j=0;j<CheckDatas.Rows.Count ;j++)
                    //{
                    //    if (CheckDatas.Rows[j].Cells["已测试"].Value.ToString() == "是")
                    //    {
                    //        //CheckDatas.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    //    }
                    //}
                }
                else
                {
                    CheckDatas.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 加载数据到表
        /// </summary>
        private void BHaddtable(string sample,string sampleID,string checkunit,string person,string gettime,string getplace,string getperson,string istest,string id)
        {
            DataRow dr;
            dr = Searchtable.NewRow();
            dr["样品名称"] = sample;
            dr["样品ID"] = sampleID;
            dr["被检单位"] = checkunit;
            dr["经营户"] = person;
            dr["采样时间"] = gettime;
            dr["采样地点"] = getplace;
            dr["采样人"] = getperson;
            dr["已测试"] = istest;
            dr["ID"] = id;
            Searchtable.Rows.Add(dr);
        }

        //加载数据到表
        private void addtable(string sel, string unit, string unitaddr, string tester, string ChkUnit, string ChkNature,string ProductAddr,string ProductCompany)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            dr["默认选择"] = sel == "是" ? true : false ;
            dr["检测单位"] = unit;
            dr["检测单位地址"] = unitaddr;
            dr["检测人"] = tester;
            dr["被检单位"] = ChkUnit;
            dr["被检单位性质"] = ChkNature;
            dr["产地"] = ProductAddr;
            dr["生产单位"] = ProductCompany;
            displayTable.Rows.Add(dr);
        }
      

        private void BHtable()
        {
            if (!m_IsCreatedDataTable)
            {
                Searchtable = new DataTable("sample");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品ID";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "经营户";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样人";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已测试";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "ID";
                Searchtable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 通用表
        /// </summary>
        private void IniBusTable()
        {
             if(BusCreateTable==false )
             {
                 displayTable = new DataTable("checkUnit");
                 DataColumn dataCol;
                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(bool);
                 dataCol.ColumnName = "默认选择";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "检测单位";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "检测单位地址";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "检测人";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "被检单位";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "被检单位性质";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "产地";
                 displayTable.Columns.Add(dataCol);

                 dataCol = new DataColumn();
                 dataCol.DataType = typeof(string);
                 dataCol.ColumnName = "生产单位";
                 displayTable.Columns.Add(dataCol);
                
                 BusCreateTable = true;
             }
        }
        /// <summary>
        /// 快检车表
        /// </summary>
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                displayTable = new DataTable("checkUnit");
                DataColumn dataCol;
                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(bool);
                //dataCol.ColumnName = "默认选择";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位地址";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测人";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "关联ID";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "被检单位";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "机构编号";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "被检单位性质";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "产地";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "生产单位";
                //displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "关联ID";
                displayTable.Columns.Add(dataCol);


                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "机构编号";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位地址";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "联系人";
                displayTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                frmunit frmu = new frmunit();
                DialogResult frmok = frmu.ShowDialog();
                if (frmok == DialogResult.OK)
                {
                    string err = string.Empty;

                    DataTable dt = sql.GetInformation("", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            addtable(dt.Rows[i]["iChecked"].ToString(), dt.Rows[i]["TestUnitName"].ToString(), dt.Rows[i]["TestUnitAddr"].ToString(), dt.Rows[i]["Tester"].ToString(),
                        dt.Rows[i]["DetectUnitName"].ToString(), dt.Rows[i]["DetectUnitNature"].ToString(), dt.Rows[i]["ProductAddr"].ToString(), dt.Rows[i]["ProductCompany"].ToString());
                        }
                           
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[2].Width = 160;
                        CheckDatas.Columns[5].Width = 160;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业修改错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message );
            }
        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7 && e.RowIndex > -1)
                {
                    string err = string.Empty;
                    if (Global.Platform == "DYKJFW")
                    {
                        if (CheckDatas.Rows[e.RowIndex].Cells["IsSelects"].Value.ToString() == "False")
                        {
                            CheckDatas.Rows[e.RowIndex].Cells["IsSelects"].Value = true;
                            sql.UpdateRegulatSel("ID=" + CheckDatas.Rows[e.RowIndex].Cells["ID"].Value.ToString(),true, "", 1, out err);
                            //Global.shengchandanwei = CheckDatas.Rows[e.RowIndex].Cells["检测单位"].Value.ToString();
                            //Global.chandidizhi = CheckDatas.Rows[e.RowIndex].Cells["检测单位地址"].Value.ToString();
                            //Global.shengchanqiye = CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString();
                            //Global.chandi = CheckDatas.Rows[e.RowIndex].Cells["reg_name"].Value.ToString();
                            //Global.jianceyuan = CheckDatas.Rows[e.RowIndex].Cells["被检单位性质"].Value.ToString();
                            
                            //for (int j = 0; j < CheckDatas.Rows.Count; j++)
                            //{
                            //    if (j != e.RowIndex)
                            //    {
                            //        CheckDatas.Rows[j].Cells["IsSelects"].Value = false;
                            //        sql.UpdateRegulatSel("ID=" + CheckDatas.Rows[j].Cells["ID"].Value.ToString(),false , "", 1, out err);
                                    
                            //    }
                            //}

                        }
                        else
                        {
                            CheckDatas.Rows[e.RowIndex].Cells["IsSelects"].Value = false;
                            sql.UpdateRegulatSel("ID=" + CheckDatas.Rows[e.RowIndex].Cells["ID"].Value.ToString(), false, "", 1, out err);
                          
                            
                        }
                    }
                    else if (Global.Platform == "DYBus")
                    {

                    }
                    else
                    {
                        if (CheckDatas.Rows[e.RowIndex].Cells["isselect"].Value.ToString() == "False")
                        {
                            CheckDatas.Rows[e.RowIndex].Cells["isselect"].Value = true;
                            _ycsql.updateOperator(int.Parse(CheckDatas.Rows[e.RowIndex].Cells["ID"].Value.ToString()), true, out err);
                            //for (int j = 0; j < CheckDatas.Rows.Count; j++)
                            //{
                            //    if (j != e.RowIndex)
                            //    {
                            //        CheckDatas.Rows[j].Cells["isselect"].Value = false;
                            //        _ycsql.updateOperator(int.Parse(CheckDatas.Rows[j].Cells["ID"].Value.ToString()),false, out err);
                            //    }
                            //}
                        }
                        else
                        {
                            CheckDatas.Rows[e.RowIndex].Cells["isselect"].Value = false ;
                            _ycsql.updateOperator(int.Parse(CheckDatas.Rows[e.RowIndex].Cells["ID"].Value.ToString()), false, out err);
                           
                        }

                        //if (CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value.ToString() == "False")
                        //{
                        //    CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value = true;
                        //    if (e.RowIndex > -1)
                        //    {
                        //        Global.shengchandanwei = CheckDatas.Rows[e.RowIndex].Cells["检测单位"].Value.ToString();
                        //        Global.chandidizhi = CheckDatas.Rows[e.RowIndex].Cells["检测单位地址"].Value.ToString();
                        //        Global.shengchanqiye = CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString();
                        //        Global.chandi = CheckDatas.Rows[e.RowIndex].Cells["被检单位"].Value.ToString();
                        //        Global.jianceyuan = CheckDatas.Rows[e.RowIndex].Cells["被检单位性质"].Value.ToString();
                        //    }
                        //    for (int j = 0; j < CheckDatas.Rows.Count; j++)
                        //    {
                        //        if (j != e.RowIndex)
                        //        {
                        //            CheckDatas.Rows[j].Cells["默认选择"].Value = false;

                                    //sql.updateBasicIn(CheckDatas.Rows[j].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[j].Cells["检测单位"].Value.ToString(),
                        //                    CheckDatas.Rows[j].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[j].Cells["检测人"].Value.ToString(),
                        //                    CheckDatas.Rows[j].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[j].Cells["被检单位性质"].Value.ToString(),
                        //                    CheckDatas.Rows[j].Cells["产地"].Value.ToString(), CheckDatas.Rows[j].Cells["生产单位"].Value.ToString(), out err);

                        //        }
                        //        else
                        //        {
                        //            sql.updateBasicIn(CheckDatas.Rows[j].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[j].Cells["检测单位"].Value.ToString(),
                        //                        CheckDatas.Rows[j].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[j].Cells["检测人"].Value.ToString(),
                        //                        CheckDatas.Rows[j].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[j].Cells["被检单位性质"].Value.ToString(),
                        //                        CheckDatas.Rows[j].Cells["产地"].Value.ToString(), CheckDatas.Rows[j].Cells["生产单位"].Value.ToString(), out err);
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value = false;

                        //    sql.updateBasicIn(CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["检测单位"].Value.ToString(),
                        //                     CheckDatas.Rows[e.RowIndex].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString(),
                        //                      CheckDatas.Rows[e.RowIndex].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["被检单位性质"].Value.ToString(),
                        //                      CheckDatas.Rows[e.RowIndex].Cells["产地"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["生产单位"].Value.ToString(), out err);
                        //    //sql.updateBasicIn(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString(),
                        //    //CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[3].Value.ToString(),
                        //    //CheckDatas.Rows[e.RowIndex].Cells[4].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[5].Value.ToString(), out err);

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业选择错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入单位/企业选择");
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("是否删除选中的记录","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information );
            if (dr == DialogResult.No)
            {
                return;
            }
            if (CheckDatas.SelectedRows.Count <1)
            {
                MessageBox.Show("请选择需要删除的数据");
                return;
            }
            string err = string.Empty;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("TestUnitName='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测单位"].Value.ToString());
                sb.AppendFormat("TestUnitAddr='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测单位地址"].Value.ToString());
                sb.AppendFormat("Tester='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测人"].Value.ToString());
                sb.AppendFormat("DetectUnitName='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["被检单位"].Value.ToString());
                sb.AppendFormat("DetectUnitNature='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["被检单位性质"].Value.ToString());
                sb.AppendFormat("ProductAddr='{0}' and ", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["产地"].Value.ToString());
                sb.AppendFormat("ProductCompany='{0}'", CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["生产单位"].Value.ToString());

                int rtn= sql.deletetInfo(sb.ToString(),out err);
                if(rtn==1)
                {
                    MessageBox.Show("删除成功");
                }

                DataTable dt = sql.GetInformation("", "", out err);
                displayTable.Clear();
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    displayTable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                        addtable(dt.Rows[i]["iChecked"].ToString(), dt.Rows[i]["TestUnitName"].ToString(), dt.Rows[i]["TestUnitAddr"].ToString(), dt.Rows[i]["Tester"].ToString(),
                        dt.Rows[i]["DetectUnitName"].ToString(), dt.Rows[i]["DetectUnitNature"].ToString(), dt.Rows[i]["ProductAddr"].ToString(), dt.Rows[i]["ProductCompany"].ToString()); 
                    }
                       
                }
                CheckDatas.DataSource = displayTable;
                CheckDatas.Columns[2].Width = 160;
                CheckDatas.Columns[5].Width = 160;
                
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业删除错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "单位/企业删除");
            }
            
        }

        private void btnrepair_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDatas.SelectedRows.Count > 0)
                {
                    string[,] a = new string[CheckDatas.SelectedRows.Count, 7];
                    a[0, 0] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测单位"].Value.ToString();//检测单位
                    a[0, 1] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测单位地址"].Value.ToString();//检测单位地址
                    a[0, 2] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测人"].Value.ToString();//检测人
                    a[0, 3] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["被检单位"].Value.ToString();//被检单位
                    a[0, 4] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["被检单位性质"].Value.ToString();//被检单位地址
                    a[0, 5] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["产地"].Value.ToString();//产地
                    a[0, 6] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["生产单位"].Value.ToString();//生产单位
                
                    Global.repairunit = a;
                }
                else 
                {
                    MessageBox.Show("请选择需要修改的记录","提示");
                    return;
                }
            
                frmunitrepair fur = new frmunitrepair();
                DialogResult dr=  fur.ShowDialog();
                if (dr == DialogResult.OK)
                {
                
                    string err = string.Empty;
                    DataTable dt = sql.GetInformation("", "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            displayTable.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                                addtable(dt.Rows[i]["iChecked"].ToString(), dt.Rows[i]["TestUnitName"].ToString(), dt.Rows[i]["TestUnitAddr"].ToString(), dt.Rows[i]["Tester"].ToString(),
                            dt.Rows[i]["DetectUnitName"].ToString(), dt.Rows[i]["DetectUnitNature"].ToString(), dt.Rows[i]["ProductAddr"].ToString(), dt.Rows[i]["ProductCompany"].ToString()); 
                            }
                        }
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[2].Width = 160;
                        CheckDatas.Columns[5].Width = 160;
                    }
              
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业修改错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "单位/企业修改");
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string err = string.Empty;

                DataTable dt = sql.GetInformation("", "", out err);
               
                if (dt != null && dt.Rows.Count > 0)
                {
                    displayTable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                        addtable(dt.Rows[i]["iChecked"].ToString(), dt.Rows[i]["TestUnitName"].ToString(), dt.Rows[i]["TestUnitAddr"].ToString(), dt.Rows[i]["Tester"].ToString(),
                        dt.Rows[i]["DetectUnitName"].ToString(), dt.Rows[i]["DetectUnitNature"].ToString(), dt.Rows[i]["ProductAddr"].ToString(), dt.Rows[i]["ProductCompany"].ToString()); 
                    }
                }
                CheckDatas.DataSource = displayTable;
                CheckDatas.Columns[2].Width = 160;
                CheckDatas.Columns[5].Width = 160;
                
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业刷新错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "单位/企业刷新");
            }
        }
        /// <summary>
        /// 下载被检单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownUnit_Click(object sender, EventArgs e)
        {
            btnDownUnit.Enabled = false;
            if (Global.ServerAdd.Length == 0)
            {
                MessageBox.Show("服务器地址不能为空", "提示");
                btnDownUnit.Enabled = true;
                return;
            }
            if (Global.ServerName.Length == 0)
            {
                MessageBox.Show("用户名不能为空", "提示");
                btnDownUnit.Enabled = true;
                return;
            }
            if (Global.ServerPassword.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示");
                btnDownUnit.Enabled = true;
                return;
            }
            string err = "";
            try
            {
                string ExamUnit = InterfaceHelper.DownloadBasicData(Global.DownLoadCompany, out err);
                ResultMsg jsonResult = GetData(ExamUnit);
                if (jsonResult == null && jsonResult.result != null)
                {
                    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDownUnit.Enabled = true;
                    return;
                }
                downLoadCompanyList = new List<Company>();
                downLoadCompanyList = JsonHelper.JsonToEntity<List<Company>>(jsonResult.result.ToString());//(List<Company>)JsonConvert.DeserializeObject(jsonResult.result.ToString(), typeof(List<Company>));
                if (downLoadCompanyList == null || downLoadCompanyList.Count == 0)
                {
                    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDownUnit.Enabled = true;
                    return;
                }
                Company company = null;
                Company.business bs = null;
                //删除原来所有数据
                sql.DeleteExamedUnit("","",out err);

                for (int i = 0; i < downLoadCompanyList.Count; i++)
                {
                    company = downLoadCompanyList[i];
                    sql.InExamedUnit(company,out err);
                }
                MessageBox.Show("被检单位下载完成");
            }
            catch (Exception ex)
            {
                btnDownUnit.Enabled = true;
                MessageBox.Show(ex.Message,"Error");
            }

            try
            {
                DataTable dt = sql.GetExamedUnit("", "", out err);

                //DataTable dt = sql.GetInformation("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    displayTable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //addtable(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][7].ToString());
                        //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                        addtable1(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][7].ToString());
                    }
                    CheckDatas.DataSource = displayTable;
                    CheckDatas.Columns[0].Width = 160;
                    CheckDatas.Columns[1].Width = 160;
                  
                }
            }
            catch (Exception ex)
            {
                btnDownUnit.Enabled = true;
                MessageBox.Show(ex.Message, "Error");
            }
            btnDownUnit.Enabled = true;
        }
        /// <summary>
        /// 根据json获取数据，没有则返回null
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private ResultMsg GetData(string json)
        {
            if (json.Length == 0)
            {
                MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
            return clsDownExamedUnit.GetJsonResult(json);
        }
        /// <summary>
        /// 下载待检信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownTestData_Click(object sender, EventArgs e)
        {
            btnDownTestData.Enabled = false;
            try
            {
                string statu = clsHpptPost.BeihaiCommunicateTest(Global.ServerAdd, Global.ServerName, Global.ServerPassword, 2, out err);
                int d = 0;
                int count = 0;
                if (statu.Length > 5)
                {
                    for (int i = 0; i < statu.Length; i++)//分割字符串
                    {
                        if (statu.Substring(i, 1) == ",")
                        {
                            d = i;
                            break;
                        }
                    }
                    string st = statu.Substring(1, d - 1);
                    string sample = statu.Substring(d + 1, statu.Length - d - 2);
                    if (st.Contains("status"))
                    {
                        clsCommunication stu = JsonHelper.JsonToEntity<clsCommunication>(st);
                        if (stu.status == "2")
                        {
                            Beihaisamples bs = JsonHelper.JsonToEntity<Beihaisamples>(sample);
                            if (bs.samplings.Count > 0)
                            {
                                TestSamples model = new TestSamples();
                                for (int i = 0; i < bs.samplings.Count; i++)
                                {
                                    sb.Length = 0;
                                    sb.AppendFormat("productId='{0}' and ", bs.samplings[i].productId);
                                    sb.AppendFormat("goodsName='{0}' and ", bs.samplings[i].goodsName);
                                    sb.AppendFormat("operateId='{0}' and ", bs.samplings[i].operateId);
                                    sb.AppendFormat("operateName='{0}' and ", bs.samplings[i].operateName);
                                    sb.AppendFormat("marketId='{0}' and ", bs.samplings[i].marketId);
                                    sb.AppendFormat("marketName='{0}' and ", bs.samplings[i].marketName);
                                    sb.AppendFormat("samplingPerson='{0}' and ", bs.samplings[i].samplingPerson);
                                    sb.AppendFormat("samplingTime='{0}' and ", bs.samplings[i].samplingTime);
                                    sb.AppendFormat("positionAddress='{0}' and ", bs.samplings[i].positionAddress);
                                    sb.AppendFormat("goodsId='{0}'", bs.samplings[i].goodsId);

                                    dt = sql.GetTestData(sb.ToString(), "", 1, out err);
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        continue;
                                    }
                                    model.productId = bs.samplings[i].productId;
                                    model.goodsName = bs.samplings[i].goodsName;
                                    model.operateId = bs.samplings[i].operateId;
                                    model.operateName = bs.samplings[i].operateName;
                                    model.marketId = bs.samplings[i].marketId;
                                    model.marketName = bs.samplings[i].marketName;
                                    model.samplingPerson = bs.samplings[i].samplingPerson;
                                    model.samplingTime = bs.samplings[i].samplingTime;
                                    model.positionAddress = bs.samplings[i].positionAddress;
                                    model.goodsId = bs.samplings[i].goodsId;

                                    sql.InsertTestSample(model, out err);
                                    count = count + 1;
                                }
                            }
                           
                        }
                    }
                }
                MessageBox.Show("共成功下载" + count.ToString() + "条数据！");
                BSearchSample("", "IsTest='否'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDownTestData.Enabled = true;
            }
            btnDownTestData.Enabled = true;
        }
        /// <summary>
        /// 删除待检信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDatas.SelectedRows.Count < 1)
                {
                    MessageBox.Show("请选择需要删除的记录");
                    return;
                }
                DialogResult dr= MessageBox.Show("是否删除选中的数据？","提示",MessageBoxButtons.YesNo ,MessageBoxIcon.Question);
                if(dr==DialogResult.Yes )
                {
                    for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                    {
                        sb.Length = 0;
                        sb.AppendFormat("goodsName='{0}' and ", CheckDatas.SelectedRows[i].Cells["样品名称"].Value.ToString());
                        sb.AppendFormat("productId='{0}' and ", CheckDatas.SelectedRows[i].Cells["样品ID"].Value.ToString());
                        sb.AppendFormat("marketName='{0}' and ", CheckDatas.SelectedRows[i].Cells["被检单位"].Value.ToString());
                        sb.AppendFormat("operateName='{0}' and ", CheckDatas.SelectedRows[i].Cells["经营户"].Value.ToString());
                        sb.AppendFormat("samplingTime='{0}' and ", CheckDatas.SelectedRows[i].Cells["采样时间"].Value.ToString());
                        sb.AppendFormat("positionAddress='{0}' and ", CheckDatas.SelectedRows[i].Cells["采样地点"].Value.ToString());
                        sb.AppendFormat("IsTest='{0}' and ", CheckDatas.SelectedRows[i].Cells["已测试"].Value.ToString());
                        sb.AppendFormat("ID={0} ", CheckDatas.SelectedRows[i].Cells["ID"].Value.ToString());
                        sql.DeleteBeiHaiData(sb.ToString(), "", out err);
                    }
                        
                    MessageBox.Show("数据删除成功！");
                    BSearchSample("", "IsTest='否'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckDatas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //for (int j = 0; j < CheckDatas.Rows.Count; j++)
            //{
            //    if (CheckDatas.Rows[j].Cells["已测试"].Value.ToString() == "是")
            //    {
            //        CheckDatas.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;
            //    }
            //}
        }
        /// <summary>
        /// 下载监管对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownSupervis_Click(object sender, EventArgs e)
        {
            try
            {
                BtnDownSupervis.Enabled = false;
                PercentProcess process = new PercentProcess() 
                {
                    MessageInfo="监管对象信息下载",
                    BackgroundWork = DownRegular,
                };
                process.Start();

                SearchKJFWRegulatory();
                BtnDownSupervis.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnDownSupervis.Enabled = true;
            }
        }
        /// <summary>
        /// 重置监管对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReSupervis_Click(object sender, EventArgs e)
        {
            try
            {
                BtnReSupervis.Enabled = false;
                PercentProcess process = new PercentProcess()
                {
                    MessageInfo = "重置监管对象信息",
                    BackgroundWork = DownAllRegulatory,
                };
                process.Start();
                SearchKJFWRegulatory();
                BtnReSupervis.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnDownSupervis.Enabled = true;
            }
        }
        private void DownAllRegulatory(Action<int> percent)
        {
            try
            {
                int item = 0;
                string reqtime = "";
                percent(5);
                sb.Length = 0;
                string url = QuickInspectServing.GetServiceURL(Global.ServerAdd, 8);
                //下载监管对象
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "regulatory");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    regulatorylist regu = JsonHelper.JsonToEntity<regulatorylist>(resultd.obj.ToString());
                    percent(8);
                    if (regu.regulatory.Count > 0)
                    {
                        sql.DeleteRegulation("", "", out err);

                        for (int i = 0; i < regu.regulatory.Count; i++)
                        {
                            regulator rg = regu.regulatory[i];
                            //string dd = regu.regulatory[i].remark;//2018-05-07 改0无经营户、1有经营户
                            //sb.Length = 0;
                            //sb.AppendFormat("rid='{0}' ", rg.id);
                            //dt = sql.getregulation(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (rg.delete_flag == "1")
                                {
                                    sb.Length = 0;
                                    sb.AppendFormat("rid='{0}' ", rg.id);
                                    sql.DeleteRegulation(sb.ToString(), "", out err);
                                }
                                else
                                {
                                    sql.UpdateRegulation(rg, out err);
                                    item = item + 1;
                                }
                            }
                            else
                            {
                                if (rg.delete_flag == "0")
                                {
                                    sql.InsertRegulation(rg, out err);
                                    item = item + 1;
                                }
                            }
                        }
                    }
                }
                percent(30);
                //经营户下载
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "business");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    businesslist regu = JsonHelper.JsonToEntity<businesslist>(resultd.obj.ToString());
                    percent(40);
                    if (regu.business.Count > 0)
                    {

                        sql.DeleteBuiness("", "", out err);
                        for (int i = 0; i < regu.business.Count; i++)
                        {
                            Manbusiness rg = regu.business[i];

                            //sb.Length = 0;
                            //sb.AppendFormat("Bid='{0}' ", rg.id);

                            //dt = sql.getbusiness(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                sql.Updatebusiness(rg, out err);
                            }
                            else
                            {
                                sql.Insertbusiness(rg, out err);
                            }
                        }
                    }
                }
                percent(60);
                //监管对象人员
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "personnel");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    personlist regu = JsonHelper.JsonToEntity<personlist>(resultd.obj.ToString());
                    percent(70);
                    if (regu.personnel.Count > 0)
                    {
                        sql.DeletePerson("","",out err);
                        for (int i = 0; i < regu.personnel.Count; i++)
                        {
                            persons rg = regu.personnel[i];
                            //sb.Length = 0;
                            //sb.AppendFormat("pid='{0}' ", rg.id);
                            //dt = sql.getPerson(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                sql.UpdatePerson(rg, out err);
                            }
                            else
                            {
                                sql.InsertPerson(rg, out err);
                            }
                        }
                    }
                }
                percent(100);
                MessageBox.Show("经营户信息下载成功,共成功下载 " + item.ToString() + " 条数据", "提示");
            }
            catch (Exception ex)
            {
                percent(100);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除监管对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeletesupervis_Click(object sender, EventArgs e)
        {

            if (CheckDatas.SelectedRows.Count > 0)
            {
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    sb.Length = 0;
                    sb.AppendFormat("rid='{0}' ", CheckDatas.SelectedRows[i].Cells["rid"].Value.ToString());
                    sql.DeleteRegulation(sb.ToString(), "", out err);
                }
                SearchKJFWRegulatory();
            }
            else
            {
                MessageBox.Show("请选择需要删除的记录！", "系统提示");
            }
        }

        private void DownRegular(Action<int> percent)
        {
            try
            {
                int item = 0;
                string reqtime = "";
                dt = sql.GetRequestTime("RequestName='BuinessTime'", "", out err);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    //reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    sql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='BuinessTime'", "", 1, out err);
                }
                else
                {
                    sql.InsertResquestTime("'BuinessTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                }
                percent(5);
                sb.Length = 0;
                string url = QuickInspectServing.GetServiceURL(Global.ServerAdd , 8);
                //下载监管对象
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "regulatory");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());
               
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    regulatorylist regu = JsonHelper.JsonToEntity<regulatorylist>(resultd.obj.ToString());
                    percent(8);
                    if (regu.regulatory.Count > 0)
                    {
                        for (int i = 0; i < regu.regulatory.Count; i++)
                        {
                            regulator rg = regu.regulatory[i];
                            //string dd = regu.regulatory[i].remark;//2018-05-07 改0无经营户、1有经营户
                            sb.Length = 0;
                            sb.AppendFormat("rid='{0}' ", rg.id);

                            dt = sql.getregulation(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (rg.delete_flag == "1")
                                {
                                    sb.Length = 0;
                                    sb.AppendFormat("rid='{0}' ", rg.id);
                                    sql.DeleteRegulation(sb.ToString(), "", out err);
                                }
                                else
                                {
                                    sql.UpdateRegulation(rg, out err);
                                    item = item + 1;
                                }
                            }
                            else
                            {
                                if (rg.delete_flag == "0")
                                {
                                    sql.InsertRegulation(rg, out err);
                                    item = item + 1;
                                }
                            }
                        }
                    }
                }
                percent(30);
                //经营户下载
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "business");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    businesslist regu = JsonHelper.JsonToEntity<businesslist>(resultd.obj.ToString());
                    percent(40);
                    if (regu.business.Count > 0)
                    {
                        for (int i = 0; i < regu.business.Count; i++)
                        {
                            Manbusiness rg = regu.business[i];

                            sb.Length = 0;
                            sb.AppendFormat("Bid='{0}' ", rg.id);

                            dt = sql.getbusiness(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                sql.Updatebusiness(rg, out err);
                            }
                            else
                            {
                                sql.Insertbusiness(rg, out err);
                            }
                        }
                    }
                }
                percent(60);
                //监管对象人员
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "personnel");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = QuickInspectServing.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    personlist regu = JsonHelper.JsonToEntity<personlist>(resultd.obj.ToString());
                    percent(70);
                    if (regu.personnel.Count > 0)
                    {
                        for (int i = 0; i < regu.personnel.Count; i++)
                        {
                            persons rg = regu.personnel[i];
                            sb.Length = 0;
                            sb.AppendFormat("pid='{0}' ", rg.id);
                            dt = sql.getPerson(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                sql.UpdatePerson(rg, out err);
                            }
                            else
                            {
                                sql.InsertPerson(rg, out err);
                            }
                        }
                    }
                }
                percent(100);
                MessageBox.Show("经营户信息下载成功,共成功下载 " + item.ToString() + " 条数据", "提示");


            }
            catch(Exception ex)
            {
                percent(100);
                MessageBox.Show(ex.Message );
            }
            
        }

        private void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void btnMarketInfo_Click(object sender, EventArgs e)
        {
            try 
            {
       
                if(Global.ServerAdd=="")
                {
                    MessageBox.Show("服务器地址不能为空！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Warning );
                }
                if (Global.ServerName == "")
                {
                    MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Global.ServerPassword == "")
                {
                    MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Global.MachineSerialCode == "")
                {
                    MessageBox.Show("设备唯一码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Global.MachineModel == "")
                {
                    MessageBox.Show("设备型号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Global.MachineModel == "")
                {
                    MessageBox.Show("设备厂家不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                btnMarketInfo.Enabled = false;
                string urlm = getUrl.GetUrl(Global.ServerAdd, 1);
                Users um = new Users();
                um.thirdCompanyName = Global.ServerName;
                um.thirdCompanyCode = Global.ServerPassword;
                string datam = JsonHelper.EntityToJson(um);
                string resultm = Global.ycHttpMath("POST", urlm, datam);
                if (resultm.Contains("msg") || resultm.Contains("status"))
                {
                    resultMarket results = JsonHelper.JsonToEntity<resultMarket>(resultm);

                    if (results != null)
                    {
                        if (results.status == "00")//上传成功
                        {
                            saveMarket(results.market.ToString());
                        }
                        else if (results.status == "01")//无此检测公司
                        {
                            MessageBox.Show("下载失败！\r\n失败原因：" + resultm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (results.status == "02")//字段错误
                        {
                            MessageBox.Show("下载失败！\r\n失败原因：" + resultm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("下载失败！\r\n失败原因：" + resultm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("下载失败！\r\n失败原因：" + resultm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("下载失败！\r\n失败原因：" + resultm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MessageBox.Show("下载成功！\r\n共成功下载" + marketCount + "条市场信息数据！\r\n共成功下载" + OprCount + "条经营户数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                SearchOpertor("");

                btnMarketInfo.Enabled = true ;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
                btnMarketInfo.Enabled = true;
            }
        }
        private string markInfo = "";
        public void saveMarket(string markets)
        {
            try
            {
                markInfo = markets;
                PercentProcess process = new PercentProcess()
                {
                    BackgroundWork = this.saveMarks,
                    MessageInfo = "正在下载市场信息"
                };
                //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
                process.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int marketCount = 0;
        private int OprCount = 0;
        private  YCsql _ycsql = new YCsql();
        private void saveMarks(Action<int> percent)
        {
            string err = "";
          
            _ycsql.deletemarket("", "", out  err);//删除所有数据

            List<marketInfo> resultm =JsonsItem.analyseMarket(markInfo);

            if (resultm != null && resultm.Count > 0)
            {
                _ycsql.deleteOpertor("", "", out  err);

                float p = 0;
                float sp = 0;
                int showp = 0;
                percent(5);
                p = (float)95 / (float)resultm.Count;
                marketCount = 0;
                OprCount = 0;
                for (int i = 0; i < resultm.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);
                    //下载经营户
                    DownOperator(resultm[i].marketCode, resultm[i].marketName);
                    //插入数据库
                    _ycsql.InsertMarket(resultm[i], out err);
                    marketCount = marketCount + 1;
                }
                percent(100);
            }
            else
            {
                percent(100);
            }
        }
        private void DownOperator(string companyCode, string conmpanyname)
        {
            string rtn = "";
            string url = "";
            string errMsg = "";
            operate u = new operate();
            u.marketCode = companyCode;//获取市场返回的编号

            u.thirdCompanyName = Global.ServerName;
            u.thirdCompanyCode = Global.ServerPassword  ;
            url = Global.ServerAdd ;
            string urlm = getUrl.GetUrl(url, 2);
            rtn = JsonHelper.EntityToJson(u);

            string rtnm = Global.ycHttpMath("POST", urlm, rtn);
            if (rtnm.Contains("msg") || rtnm.Contains("status"))
            {
                resultOperate results = JsonHelper.JsonToEntity<resultOperate>(rtnm);
                if (results != null)
                {
                    List<Operates> resultm =JsonsItem.analyseOperator(results.@operator.ToString());

                    if (resultm != null && resultm.Count > 0)
                    {
                        for (int i = 0; i < resultm.Count; i++)
                        {
                            _ycsql.InsertOpertor(resultm[i], companyCode, conmpanyname, out errMsg);
                            OprCount = OprCount + 1;
                        }
                    }
                }
            }
        }
        
    }
}
