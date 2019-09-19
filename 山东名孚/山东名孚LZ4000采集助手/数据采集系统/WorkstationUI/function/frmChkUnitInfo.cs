using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationUI.machine;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class frmChkUnitInfo : Form
    {
        private clsSetSqlData sql = new clsSetSqlData();
        private DataTable displayTable = null;
        private bool m_IsCreatedDataTable = false;
        //public UCAll_LZ4000 lz4000=new UCAll_LZ4000():

        public frmChkUnitInfo()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void frmChkUnitInfo_Load(object sender, EventArgs e)
        {
            string err = string.Empty;

            iniTable();
            try
            {
                DataTable dti = sql.GetInformation("", "", out err);
                if (dti != null)
                {
                    if (dti.Rows.Count > 0)
                    {
                        for (int n = 0; n < dti.Rows.Count; n++)
                        {
                            addtable(dti.Rows[n][2].ToString(), dti.Rows[n][3].ToString(), dti.Rows[n][8].ToString());
                        }
                    }
                    CheckDatas.DataSource = displayTable;
                    CheckDatas.Columns[1].Width =180;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                displayTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(bool);
                //dataCol.ColumnName = "已选择";
                //displayTable.Columns.Add(dataCol)

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位";
                //displayTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位地址";
                //displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位地址";
                displayTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测人";
                displayTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        //加载数据到表
        private void addtable(  string ChkUnit, string ChkAddr, string tester)
        {
            DataRow dr;
            dr = displayTable.NewRow();
            //dr["已选择"] = sel == "否" ? false : true;
            //dr["检测单位"] = unit;
            //dr["检测单位地址"] = unitaddr;          
            dr["被检单位"] = ChkUnit;
            dr["被检单位地址"] = ChkAddr;
            dr["检测人"] = tester;

            displayTable.Rows.Add(dr);
        }
        private void btnquary_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 双击选中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Global.ChkInfo[0, 0] = CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString();//被检单位
            Global.ChkInfo[0, 1] = CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString();//被检单位地址
            Global.ChkInfo[0, 2] = CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString();//检测人

            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        
    }
}
