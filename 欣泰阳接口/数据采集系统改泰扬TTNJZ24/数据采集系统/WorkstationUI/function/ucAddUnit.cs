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

                DataTable dt= sql.GetInformation("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        displayTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
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
                MessageBox.Show(ex.Message,"Error");
            }
        }
        //加载数据到表
        private void addtable(string sel,string unit, string unitaddr, string tester, string ChkUnit, string ChkAddr)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            dr["已选择"] = sel=="否"?false :true ;
            dr["检测单位"] = unit;
            dr["检测单位地址"] = unitaddr;
            dr["检测人"] = tester;
            dr["被检单位"] = ChkUnit;
            dr["被检单位地址"] = ChkAddr;
       
            displayTable.Rows.Add(dr);
        }
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                displayTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已选择";
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
                dataCol.ColumnName = "被检单位地址";
                displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样地址";
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

                sql.deletetInfo(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
            }

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
}
