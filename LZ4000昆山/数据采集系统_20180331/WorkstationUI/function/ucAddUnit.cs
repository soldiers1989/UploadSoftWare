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

namespace WorkstationUI.function
{
    public partial class ucAddUnit : UserControl
    {
        public ucAddUnit()
        {
            InitializeComponent();
        }
        private clsSetSqlData sql = new clsSetSqlData();
     
        private bool m_IsCreatedDataTable = false;
        private DataTable displayTable = null;
        private DataTable Searchtable = null;
        private  StringBuilder sb = new StringBuilder();
        private clsdiary dy = new clsdiary();
        private string err = "";
        private DataTable dt = null;
        /// <summary>
        /// 下载的被检单位集合
        /// </summary>
        private List<Company> downLoadCompanyList = null;
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
            //iniTable();
            KStable();
            //BHtable();
            try
            {
                //string err = string.Empty;
                //BSearchSample("", "IsTest='否'");

                //DataTable dt = sql.GetExamedUnit("", "", out err);

                //DataTable dt = sql.GetInformation("", "", out err);
                searchKSmanage("");
               
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业成功" , "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入单位/企业错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"进入单位/企业");
            }
        }
        private void searchKSmanage(string where)
        {
            try
            {
                string err = string.Empty;
                dt = sql.GetAreaMarket(where, "", out err);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //displayTable.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addKSTable(dt.Rows[i]["LicenseNo"].ToString(), dt.Rows[i]["MarketName"].ToString(), dt.Rows[i]["MarketRef"].ToString(), dt.Rows[i]["Abbreviation"].ToString(), dt.Rows[i]["PositionDistrictName"].ToString(), dt.Rows[i]["PositionNo"].ToString(), dt.Rows[i]["DABH"].ToString(), dt.Rows[i]["Contactor"].ToString(), dt.Rows[i]["ContactTel"].ToString());
                    //}
                    //CheckDatas.DataSource = displayTable;
                    CheckDatas.DataSource = dt;
                    CheckDatas.Columns["LicenseNo"].HeaderText = "单位主体编码";
                    CheckDatas.Columns["MarketName"].HeaderText = "单位主体名称";
                    CheckDatas.Columns["MarketRef"].HeaderText = "单位主体类别";
                    CheckDatas.Columns["Abbreviation"].HeaderText = "单位主体简称";
                    CheckDatas.Columns["PositionDistrictName"].HeaderText = "摊位区域名称";
                    CheckDatas.Columns["PositionNo"].HeaderText = "摊位号";
                    CheckDatas.Columns["DABH"].HeaderText = "身份证号";
                    CheckDatas.Columns["Contactor"].HeaderText = "姓名";
                    CheckDatas.Columns["ContactTel"].HeaderText = "手机号码";
                    CheckDatas.Columns["ID"].Visible  = false ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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

        private void addKSTable(string unitcode,string unitname,string unittype,string jinachneng,string twqymc,string stallnum,string sfzh,string person,string phone)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            dr["单位主体编码"] = unitcode;
            dr["单位主体名称"] = unitname;
            dr["单位主体类别"] = unittype;
            dr["单位主体简称"] = jinachneng;
            dr["摊位区域名称"] = twqymc;
            dr["摊位号"] = stallnum;
            dr["身份证号"] = sfzh;
            dr["姓名"] = person;
            dr["手机号码"] = phone;
            displayTable.Rows.Add(dr);
 
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
        ////加载数据到表
        //private void addtable(string id,string unit, string unitNo, string ChkAddr,string Chker)
        //{
        //    DataRow dr;
        //    dr = displayTable.NewRow();
        //    //dr["默认选择"] = sel == "否" ? false : true;
        //    //dr["检测单位"] = unit;
        //    //dr["检测单位地址"] = unitaddr;
        //    //dr["检测人"] = tester;
        //    //dr["被检单位"] = ChkUnit;
        //    //dr["被检单位地址"] = ChkAddr;

        //    //dr["关联ID"] = id;
        //    //dr["被检单位"] = unit;
        //    //dr["机构编号"] = unitNo;
        //    //dr["被检单位地址"] = ChkAddr;
        //    //dr["联系人"] = Chker;
       
        //    displayTable.Rows.Add(dr);
        //}

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
        private void KStable()
        {
            if (!m_IsCreatedDataTable)
            {
                displayTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(bool);
                //dataCol.ColumnName = "默认选择";
                //displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位主体编码";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位主体名称";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位主体类别";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位主体简称";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "摊位区域名称";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "摊位号";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "身份证号";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "姓名";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "手机号码";
                displayTable.Columns.Add(dataCol);


                m_IsCreatedDataTable = true;
            }
        }

        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                displayTable = new DataTable("checkDtbl");
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

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "关联ID";
                //displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "机构编号";
                //displayTable.Columns.Add(dataCol);

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
                if (e.ColumnIndex == 0)
                {
                    string err = string.Empty;
                    if (CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value.ToString() == "False")
                    {
                        CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value = true;
                        if (e.RowIndex > -1)
                        {
                            Global.shengchandanwei = CheckDatas.Rows[e.RowIndex].Cells["检测单位"].Value.ToString();
                            Global.chandidizhi = CheckDatas.Rows[e.RowIndex].Cells["检测单位地址"].Value.ToString();
                            Global.shengchanqiye = CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString();
                            Global.chandi = CheckDatas.Rows[e.RowIndex].Cells["被检单位"].Value.ToString();
                            Global.jianceyuan = CheckDatas.Rows[e.RowIndex].Cells["被检单位性质"].Value.ToString();
                        }               
                        for (int j = 0; j < CheckDatas.Rows.Count; j++)
                        {
                            if (j != e.RowIndex)
                            {
                                CheckDatas.Rows[j].Cells["默认选择"].Value = false;
                                
                                sql.updateBasicIn(CheckDatas.Rows[j].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[j].Cells["检测单位"].Value.ToString(),
                                        CheckDatas.Rows[j].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[j].Cells["检测人"].Value.ToString(),
                                        CheckDatas.Rows[j].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[j].Cells["被检单位性质"].Value.ToString(),
                                        CheckDatas.Rows[j].Cells["产地"].Value.ToString(), CheckDatas.Rows[j].Cells["生产单位"].Value.ToString(), out err);
                               
                            }
                            else 
                            {
                                sql.updateBasicIn(CheckDatas.Rows[j].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[j].Cells["检测单位"].Value.ToString(),
                                            CheckDatas.Rows[j].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[j].Cells["检测人"].Value.ToString(),
                                            CheckDatas.Rows[j].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[j].Cells["被检单位性质"].Value.ToString(),
                                            CheckDatas.Rows[j].Cells["产地"].Value.ToString(), CheckDatas.Rows[j].Cells["生产单位"].Value.ToString(), out err);           
                            }
                        }
                                  
                    }
                    else
                    {
                        CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value = false;
                   
                            sql.updateBasicIn(CheckDatas.Rows[e.RowIndex].Cells["默认选择"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["检测单位"].Value.ToString(),
                                             CheckDatas.Rows[e.RowIndex].Cells["检测单位地址"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString(),
                                              CheckDatas.Rows[e.RowIndex].Cells["被检单位"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["被检单位性质"].Value.ToString(),
                                              CheckDatas.Rows[e.RowIndex].Cells["产地"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["生产单位"].Value.ToString(), out err);           
                            //sql.updateBasicIn(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                        //CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                        //CheckDatas.Rows[e.RowIndex].Cells[4].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[5].Value.ToString(), out err);
                   
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
               

                //DataTable dt = sql.GetExamedUnit("", "", out err);

                DataTable dt = sql.GetInformation("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    displayTable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //addtable(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][7].ToString());
                        //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
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
        /// 经营户下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperators_Click(object sender, EventArgs e)
        {
            btnOperators.Enabled = false;
            try
            {
                com.szscgl.ncp.sDataInfrace webserver = new com.szscgl.ncp.sDataInfrace();
                string AreaMarket = string.Empty;
                KunShanEntity.GetTokenRequest.webService querySignContact = new KunShanEntity.GetTokenRequest.webService();
                querySignContact.request = new KunShanEntity.GetTokenRequest.Request();
                querySignContact.request.name = Global.ServerName ;
                querySignContact.request.password = Global.MD5(Global.ServerPassword );
                string  response = XmlHelper.EntityToXml<KunShanEntity.GetTokenRequest.webService>(querySignContact);
                //System.Console.WriteLine(string.Format("GetAreaMarket-Request:{0}", response));
                AreaMarket = webserver.GetAreaMarket(response);
                //System.Console.WriteLine(string.Format("GetAreaMarket-Response:{0}", AreaMarket));
                List<KunShanEntity.AreaMarket> models = JsonHelper.JsonToEntity<List<KunShanEntity.AreaMarket>>(AreaMarket);
                if (models != null && models.Count > 0)
                {
                    //List<KunShanEntity.AreaMarket> models = as List<KunShanEntity.AreaMarket>;
                        sql.DeleteAreaMarket("", out err);//删除原有的数据
                        sql.DeleteKSAreaSignContact("", out err);//删除原有的经用户数据
                        KunShanEntity.SignContact indata = new KunShanEntity.SignContact();
                        KunShanEntity.AreaMarket AM = new KunShanEntity.AreaMarket();
                        int markcount = 0;
                        for (int i = 0; i < models.Count; i++)
                        {
                            //目前MarketRef只有为1或者2的时候才有经营户数据
                            //if (!models[i].MarketRef.Equals("1") && !models[i].MarketRef.Equals("2"))
                            //{
                            //    continue;
                            //}
                            AM.Abbreviation = models[i].Abbreviation;
                            AM.LicenseNo = models[i].LicenseNo;
                            AM.MarketName = models[i].MarketName;
                            AM.MarketRef = getmarket(models[i].MarketRef);

                            sql.InAreaMarket(AM, out err);
                            //根据主体单位获取经营户信息
                            string  AreaSignContact = string.Empty;
                            KunShanEntity.GetAreaSignContactReqyest.webService getAreaSignContactReqyest = new KunShanEntity.GetAreaSignContactReqyest.webService();
                            getAreaSignContactReqyest.request = new KunShanEntity.GetAreaSignContactReqyest.Request();
                            getAreaSignContactReqyest.request.LicenseNo = models[i].LicenseNo;
                            getAreaSignContactReqyest.head = new KunShanEntity.GetAreaSignContactReqyest.Head();
                            getAreaSignContactReqyest.head.name = Global.ServerName;
                            getAreaSignContactReqyest.head.password = Global.MD5(Global.ServerPassword);
                            response = XmlHelper.EntityToXml<KunShanEntity.GetAreaSignContactReqyest.webService>(getAreaSignContactReqyest);
                            AreaSignContact = webserver.GetAreaSignContact(response);
                            if ("-1001|您市场在签约有效期内没有经营户！" == AreaSignContact)
                            {
                                continue;
                            }
                            List<KunShanEntity.SignContact> _models = JsonHelper.JsonToEntity<List<KunShanEntity.SignContact>>(AreaSignContact);
                            if (_models != null && _models.Count > 0)
                            {
                                for(int j=0;j<_models.Count;j++)
                                {
                                    indata.LicenseNo = AM.LicenseNo;
                                    indata.Abbreviation = AM.Abbreviation;
                                    indata.MarketName = AM.MarketName;
                                    indata.MarketRef = AM.MarketRef;

                                    indata.PositionDistrictName = _models[j].PositionDistrictName;
                                    indata.PositionNo = _models[j].PositionNo;
                                    indata.DABH = _models[j].DABH;
                                    indata.Contactor= _models[j].Contactor;
                                    indata.ContactTel = _models[j].ContactTel;


                                    sql.InKSAreaSignContact(indata, out err);
                                    markcount = markcount + 1;
                                }
                               
                            }
                            //System.Console.WriteLine(string.Format("GetAreaSignContactReqyest-Response:{0}", AreaSignContact));
                         }

                        MessageBox.Show("经营户信息下载成功","操作提示",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                        searchKSmanage("");
                }
                else
                {
                    MessageBox.Show(AreaMarket + "\r\n请确保输入的用户名与密码无误！", "下载提示",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnOperators.Enabled = true;
            }
            btnOperators.Enabled = true ;
        }
        private string getmarket(string num)
        {
            string rtndata = "";
            try
            {
                if (num == "1")
                {
                    rtndata = "批发市场";
                }
                else if (num == "2")
                {
                    rtndata = "农贸市场";
                }
                else if (num == "3")
                {
                    rtndata = "检测机构";
                }
                else if (num == "4")
                {
                    rtndata = "餐饮单位";
                }
                else if (num == "5")
                {
                    rtndata = "食品生产企业";
                }
                else if (num == "6")
                {
                    rtndata = "商场超市";
                }

                else if (num == "7")
                {
                    rtndata = "个体工商户";
                }
                else if (num == "8")
                {
                    rtndata = "食材配送企业";
                }
                else if (num == "9")
                {
                    rtndata = "单位食堂";
                }
                else if (num == "10")
                {
                    rtndata = "集体用餐配送和中央厨房";
                }
                else if (num == "11")
                {
                    rtndata = "农产品基地";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return rtndata;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try 
            {
                CheckDatas.DataSource =null;
                dt.Clear();
                sb.Length = 0;
                if (txtmanagename.Text.Trim() != "" && txtstall.Text.Trim() != "")
                {
                    sb.AppendFormat("MarketName like '%{0}%' and ", txtmanagename.Text.Trim());
                    sb.AppendFormat("Contactor like '%{0}%'", txtstall.Text.Trim());
                }
                else if (txtmanagename.Text.Trim() != "")
                {
                    sb.AppendFormat("MarketName like '%{0}%' ", txtmanagename.Text.Trim());
                }
                else if (txtstall.Text.Trim()!="")
                {
                    sb.AppendFormat("Contactor like '%{0}%'", txtstall.Text.Trim());
                }
                searchKSmanage(sb.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewCompany_Click(object sender, EventArgs e)
        {
            frmKScompany window = new frmKScompany();
            window.ShowDialog();
        }
    }
}
