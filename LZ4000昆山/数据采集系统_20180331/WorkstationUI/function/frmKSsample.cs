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
    public partial class frmKSsample : Form
    {
        private bool m_IsCreatedDataTable = false;
        private DataTable DataDisTable = null;
        private DataTable dt = null;
        private clsSetSqlData csql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        public frmKSsample()
        {
            InitializeComponent();
        }

        private void frmKSsample_Load(object sender, EventArgs e)
        {
            KStable();
            try
            {
                Global.KSsamplecode = "";
                Global.KSsampleName = "";
                //sb.Length = 0;
                //sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                KSsearchsample("");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }
        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void KStable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataDisTable = new DataTable("sample");
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "大类品种编码";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "大类品种名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "小类品种编码";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "小类品种名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "更新时间";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "唯一标识";
                DataDisTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        private void KSsearchsample(string where)
        {
            string err = string.Empty;
            CheckDatas.DataSource = null;
            if (dt!=null)
            dt.Clear();
            try
            {
                dt = csql.GetKSsample(where, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataDisTable.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                    //}
                    CheckDatas.DataSource = dt;//DataDisTable;
                    CheckDatas.Columns["sId"].HeaderCell.Value = "唯一标识";
                    CheckDatas.Columns["ItemCode"].HeaderCell.Value = "大类品种编码";
                    CheckDatas.Columns["ItemName"].HeaderCell.Value = "大类品种名称";
                    CheckDatas.Columns["SubItemCode"].HeaderCell.Value = "小类品种编码";
                    CheckDatas.Columns["SubItemName"].HeaderCell.Value = "小类品种名称";
                    CheckDatas.Columns["UpdateDate"].HeaderCell.Value = "更新时间";
                    CheckDatas.Columns["sId"].DisplayIndex= 5;
                    CheckDatas.Columns["ItemCode"].DisplayIndex = 0;
                    CheckDatas.Columns["ItemName"].DisplayIndex = 1;
                    CheckDatas.Columns["SubItemCode"].DisplayIndex = 2;
                    CheckDatas.Columns["SubItemName"].DisplayIndex = 3;
                    CheckDatas.Columns["UpdateDate"].DisplayIndex = 4;
                    //for (int j = 0; j < CheckDatas.RowCount; j++)
                    //{
                    //CheckDatas.Columns[2].Width = 150;
                    //CheckDatas.Columns[3].Width = 150;
                    //CheckDatas.Columns[4].Width = 150;
                    //CheckDatas.Columns[5].Width = 150;
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                sb.Length = 0;
                if (txtSample.Text.Trim().Length > 0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }

                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if (sb.ToString().Length > 0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat(" ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }

                KSsearchsample(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
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

        private void CheckDatas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Global.KSsamplecode = CheckDatas.SelectedRows[0].Cells["SubItemCode"].Value.ToString();
            Global.KSsampleName = CheckDatas.SelectedRows[0].Cells["SubItemName"].Value.ToString();
            this.Close();
        }

        private void frmKSsample_FormClosed(object sender, FormClosedEventArgs e)
        {
            if( Global.KSsamplecode =="" ||  Global.KSsampleName =="")
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSampleType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sb.Length = 0;
                if (txtSample.Text.Trim().Length > 0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }

                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if (sb.ToString().Length > 0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat(" ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }
                KSsearchsample(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSample_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sb.Length = 0;
                if (txtSample.Text.Trim().Length > 0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }
                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if (sb.ToString().Length > 0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat(" ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }
                KSsearchsample(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Global.KSsamplecode = CheckDatas.SelectedRows[0].Cells["SubItemCode"].Value.ToString();
            Global.KSsampleName = CheckDatas.SelectedRows[0].Cells["SubItemName"].Value.ToString();
            this.Close();
        }

    }
}
