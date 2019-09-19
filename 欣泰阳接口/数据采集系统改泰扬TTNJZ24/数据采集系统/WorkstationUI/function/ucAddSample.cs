using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class ucAddSample : UserControl
    {
        private bool m_IsCreatedDataTable = false;
        private DataTable DataDisTable = null;
        private clsSetSqlData csql = new clsSetSqlData();
        private DataTable dt = null;

        public ucAddSample()
        {
            InitializeComponent();
        }

        private void ucAddSample_Load(object sender, EventArgs e)
        {
            iniTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                string err = string.Empty;
                dt=  csql.GetChkItem("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        }
                        CheckDatas.DataSource = DataDisTable;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataDisTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "判定符号";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataDisTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测时间";
                //DataTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
            }
        }
        private void addtable(string SamName, string ItemName, string testbase, string Svalue, string symbol, string iunit)
        {
            DataRow dr;
            dr = DataDisTable.NewRow();
            dr["样品名称"] = SamName;
            dr["检测项目"] = ItemName;
            dr["检测依据"] = testbase;
            dr["标准值"] = Svalue;
            dr["判定符号"] = symbol;
            dr["单位"] = iunit;

            DataDisTable.Rows.Add(dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddSample fas = new frmAddSample();
            fas.SaveRepair = "新增";
            DialogResult dr = fas.ShowDialog();
            if(dr==DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    string err = string.Empty;
                    DataTable dt = csql.GetChkItem("", "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataDisTable.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                            }
                            CheckDatas.DataSource = DataDisTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要修改的记录","提示");
                return;
            }
            string err = string.Empty;
            frmAddSample fas = new frmAddSample();

            string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
               " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
            DataTable dt = csql.GetChkItem(sql, "", out err);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    fas.id = dt.Rows[0][6].ToString();
                }
            }

            string[,] a = new string[CheckDatas.SelectedRows.Count , 6];
            for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
            {
                a[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                a[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                a[i, 2] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                a[i, 3] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                a[i, 4] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                a[i, 5] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
            }
            Global.repairSample = a;
            fas.SaveRepair = "修改";
            DialogResult dr = fas.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    
                    dt = csql.GetChkItem("", "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataDisTable.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                            }
                            CheckDatas.DataSource = DataDisTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                string err = string.Empty;
                if (!txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    sb.Append("FtypeNmae='");
                    sb.Append(txtSample.Text);
                    sb.Append("' and ");
                    sb.Append("Name='");
                    sb.Append(txtItem.Text);
                    sb.Append("'");
                }
                if (!txtSample.Text.Equals("") && txtItem.Text.Equals(""))
                {
                    sb.Append("FtypeNmae='");
                    sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    //sb.Append("Name='");
                    //sb.Append(txtItem.Text);
                    sb.Append("'");
                }
                if (txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    //sb.Append("FtypeNmae='");
                    //sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    sb.Append("Name='");
                    sb.Append(txtItem.Text);
                    sb.Append("'");
                }

                dt = csql.GetChkItem(sb.ToString(), "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        }
                        CheckDatas.DataSource = DataDisTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                string err = string.Empty;
                int iok = 0;
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {  
                    StringBuilder sb = new StringBuilder();

                    sb.Append("tStandSample where FtypeNmae='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
                    sb.Append("' and Name='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
                    sb.Append("' and ItemDes='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
                    sb.Append("' and StandardValue='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[3].Value.ToString());
                    sb.Append("'");

                    iok = csql.Delete(sb.ToString(), out err);
                }                
                MessageBox.Show("数据删除成功");
                btnrefresh_Click(null ,null);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
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
                StringBuilder sb = new StringBuilder();
                string err = string.Empty;
                dt = csql.GetChkItem("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        }
                        CheckDatas.DataSource = DataDisTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (CheckDatas.SelectedRows.Count < 1)
            //{
            //    MessageBox.Show("请选择需要修改的记录", "提示");
            //    return;
            //}
            string err = string.Empty;
            frmAddSample fas = new frmAddSample();
            string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'"+
                " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
            DataTable dt= csql.GetChkItem(sql, "", out err);
            if (dt != null)
            {
                if(dt.Rows.Count>0)
                {
                    fas.id = dt.Rows[0][6].ToString();
                }
            }

            string[,] a = new string[CheckDatas.SelectedRows.Count, 6];
            for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
            {
                a[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                a[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                a[i, 2] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                a[i, 3] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                a[i, 4] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                a[i, 5] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
            }
            Global.repairSample = a;
            fas.SaveRepair = "修改";
            DialogResult dr = fas.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    
                    dt = csql.GetChkItem("", "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataDisTable.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                            }
                            CheckDatas.DataSource = DataDisTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
 
    }
}
