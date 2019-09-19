using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class FrmSearchSample : Form
    {
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private DataTable dt = null;
        private string err = "";
        public string _item = "";
        private bool iscreate = false;
        public DataTable SampleTable = null;
        public FrmSearchSample()
        {
            InitializeComponent();
            Initable();
        }

        private void FrmSearchSample_Load(object sender, EventArgs e)
        {
            Global.iSampleName = "";
            if (Global.Platform == "DYKJFW" || Global.Platform == "DYBus")
            {


                string sql = " where f.fid=std.food_id and d.cid=std.item_id and d.detect_item_name='" + _item + "'";
                dt = sqlSet.GetSamplestd(sql, "", 2, out err);
                //dt = csql.Getfoodtype("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                    //}
                    CheckDatas.DataSource = dt;//DataDisTable;
                    CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                    CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                    CheckDatas.Columns[2].HeaderCell.Value = "判定符号";
                    CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                    CheckDatas.Columns[4].HeaderCell.Value = "单位";
                    CheckDatas.Columns[5].HeaderCell.Value = "更新时间";
                    //for (int j = 0; j < CheckDatas.RowCount; j++)
                    //{
                    CheckDatas.Columns[0].Width = 200;
                    CheckDatas.Columns[1].Width = 200;
                    CheckDatas.Columns[2].Width = 200;
                    //}
                }

            }
            else
            {
                try
                {
                    string sql = " Name='" + _item + "'";
                    dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddTable(dt.Rows[i]["FtypeNmae"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ItemDes"].ToString(), dt.Rows[i]["StandardValue"].ToString(), dt.Rows[i]["Demarcate"].ToString(), dt.Rows[i]["Unit"].ToString());
                        }
                    }
                    CheckDatas.DataSource = SampleTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
            
        }
        private void Initable()
        {
            if (iscreate == false)
            {
                SampleTable = new DataTable("diatable");//去掉Static
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                SampleTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                SampleTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                SampleTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                SampleTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "判定符号";
                SampleTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                SampleTable.Columns.Add(dataCol);

                iscreate = true;
            }
        }
        private void AddTable(string sample,string item,string testbase,string stdvalue,string Symbol,string unit)
        {
            DataRow dr;
            dr = SampleTable.NewRow();
            dr["样品名称"] = sample;
            dr["检测项目"] = item;
            dr["检测依据"] = testbase;
            dr["标准值"] = stdvalue;
            dr["判定符号"] = Symbol;
            dr["单位"] = unit;
            SampleTable.Rows.Add(dr);
        }
        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要记录再单击确定");
            }
            else
            {
                Global.iSampleName = CheckDatas.SelectedRows[0].Cells[0].Value.ToString();
            }
            this.Close();
        }

        private void btnlSearch_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;
            SampleTable.Clear();
            try
            {
                string sql ="FtypeNmae like '%"+txtSampleName.Text.Trim()+ "%' and Name='" + _item + "'";
                dt = sqlSet.GetChkItem(sql, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddTable(dt.Rows[i]["FtypeNmae"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ItemDes"].ToString(), dt.Rows[i]["StandardValue"].ToString(), dt.Rows[i]["Demarcate"].ToString(), dt.Rows[i]["Unit"].ToString());
                    }
                }
                CheckDatas.DataSource = SampleTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White ;
        }

        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要记录");
            }
            else
            {
                Global.iSampleName = CheckDatas.SelectedRows[0].Cells[0].Value.ToString(); ;
            }
            this.Close();
        }

        private void txtSampleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckDatas.DataSource = null;
                SampleTable.Clear();
                try
                {
                    string sql = "FtypeNmae like '%" + txtSampleName.Text.Trim() + "%' and Name='" + _item + "'";
                    dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddTable(dt.Rows[i]["FtypeNmae"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ItemDes"].ToString(), dt.Rows[i]["StandardValue"].ToString(), dt.Rows[i]["Demarcate"].ToString(), dt.Rows[i]["Unit"].ToString());
                        }
                    }
                    CheckDatas.DataSource = SampleTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
