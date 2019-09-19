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
            iniTable();
            try
            {
                string err = string.Empty;

                //DataTable dt = sql.GetExamedUnit("","",out err );

                DataTable dt = sql.GetInformation("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //addtable(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][7].ToString()); 
                            addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()); 
                        }
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[0].Width = 160;
                        CheckDatas.Columns[1].Width = 160;
                    }
                   
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        //加载数据到表
        private void addtable(string sel, string unit, string unitaddr, string tester, string ChkUnit, string ChkAddr)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            dr["默认选择"] = sel == "否" ? false : true;
            dr["生产单位"] = unit;
            dr["产地地址"] = unitaddr;
            dr["检测人"] = tester;
            dr["生产企业"] = ChkUnit;
            dr["产地"] = ChkAddr;

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
                dataCol.ColumnName = "生产单位";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地地址";
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
                dataCol.ColumnName = "生产企业";
                displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "机构编号";
                //displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地";
                displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "联系人";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测时间";
                //displayTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            frmunit frmu = new frmunit();
            DialogResult frmok= frmu.ShowDialog();
            if (frmok == DialogResult.OK)
            {
                try
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
                                addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            }
                        }
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[2].Width = 160;
                        CheckDatas.Columns[5].Width = 160;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
       
            if (e.ColumnIndex == 0)
            {
                string err = string.Empty;
                if (CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString() == "False")
                {
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = true;
                    if (e.RowIndex > -1)
                    {
                        Global.shengchandanwei = CheckDatas.Rows[e.RowIndex].Cells["生产单位"].Value.ToString();
                        Global.chandidizhi = CheckDatas.Rows[e.RowIndex].Cells["产地地址"].Value.ToString();
                        Global.shengchanqiye = CheckDatas.Rows[e.RowIndex].Cells["生产企业"].Value.ToString();
                        Global.chandi = CheckDatas.Rows[e.RowIndex].Cells["产地"].Value.ToString();
                        Global.jianceyuan = CheckDatas.Rows[e.RowIndex].Cells["检测人"].Value.ToString();
                    }
                    try
                    {                       
                        for (int j = 0; j < CheckDatas.Rows.Count; j++)
                        {
                            if (j != e.RowIndex)
                            {
                                CheckDatas.Rows[j].Cells[0].Value = false;
                                try
                                {
                                    sql.updateBasicIn(CheckDatas.Rows[j].Cells[0].Value.ToString(), CheckDatas.Rows[j].Cells[1].Value.ToString(),
                                         CheckDatas.Rows[j].Cells[2].Value.ToString(), CheckDatas.Rows[j].Cells[3].Value.ToString(),
                                          CheckDatas.Rows[j].Cells[4].Value.ToString(), CheckDatas.Rows[j].Cells[5].Value.ToString(),out err);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error");
                                }
                            }
                            else 
                            {
                                sql.updateBasicIn(CheckDatas.Rows[j].Cells[0].Value.ToString(), CheckDatas.Rows[j].Cells[1].Value.ToString(),
                                     CheckDatas.Rows[j].Cells[2].Value.ToString(), CheckDatas.Rows[j].Cells[3].Value.ToString(),
                                          CheckDatas.Rows[j].Cells[4].Value.ToString(), CheckDatas.Rows[j].Cells[5].Value.ToString(), out err);                           
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }                   
                }
                else
                {
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = false;
                    try
                    {
                        sql.updateBasicIn(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                    CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                    CheckDatas.Rows[e.RowIndex].Cells[4].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[5].Value.ToString(), out err);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
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
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("TestUnitName='");
                sb.Append(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[1].Value.ToString());
                sb.Append("' and ");
                sb.Append("TestUnitAddr='");
                sb.Append(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString());
                sb.Append("' and ");
                sb.Append("DetectUnitName='");
                sb.Append(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString());
                sb.Append("' and ");
                sb.Append("SampleAddress='");
                sb.Append(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[5].Value.ToString());
                sb.Append("' and ");
                sb.Append("Tester='");
                sb.Append(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString());
                sb.Append("'");

                int rtn= sql.deletetInfo(sb.ToString());
                if(rtn==1)
                {
                    MessageBox.Show("删除成功");
                }

                string err = string.Empty;
                DataTable dt = sql.GetInformation("", "", out err);
                displayTable.Clear();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                        }
                       
                    }
                    CheckDatas.DataSource = displayTable;
                    CheckDatas.Columns[2].Width = 160;
                    CheckDatas.Columns[5].Width = 160;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void btnrepair_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                string[,] a = new string[CheckDatas.SelectedRows.Count, 5];
                a[0, 0] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[1].Value.ToString();//检测单位
                a[0, 1] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString();//检测单位地址
                a[0, 2] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString();//检测人
                a[0, 3] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString();//被检单位
                a[0, 4] = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[5].Value.ToString();//被检单位地址
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
                try
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
                                addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            }
                        }
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[2].Width = 160;
                        CheckDatas.Columns[5].Width = 160;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
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
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                            addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()); 
                        }
                    }
                    CheckDatas.DataSource = displayTable;
                    CheckDatas.Columns[2].Width = 160;
                    CheckDatas.Columns[5].Width = 160;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //addtable(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][7].ToString());
                            addtable(dt.Rows[i][9].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                        }
                        CheckDatas.DataSource = displayTable;
                        CheckDatas.Columns[0].Width = 160;
                        CheckDatas.Columns[1].Width = 160;
                    }

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
      
    }
}
