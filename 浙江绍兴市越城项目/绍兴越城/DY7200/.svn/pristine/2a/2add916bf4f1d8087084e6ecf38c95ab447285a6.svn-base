using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class frmItem : Form
    {
        public string item = "";
        private YCsql _sql = new YCsql();
        public frmItem()
        {
            InitializeComponent();
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            item = "";
            DataTable dtitem = _sql.GetYCItemTable("");
            if (dtitem != null && dtitem.Rows.Count > 0)
            {
                CheckDatas.DataSource = dtitem;
                CheckDatas.Columns["itemName"].HeaderText = "项目名称";
                CheckDatas.Columns["itemCode"].HeaderText = "项目编号";
                CheckDatas.Columns["itemType"].HeaderText = "项目类型";
                CheckDatas.Columns["ID"].Visible = false ;
            }
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlSearch_Click(object sender, EventArgs e)
        {
            string where = "";
            CheckDatas.DataSource = "";
            if(txtSampleName.Text.Trim ()!="")
            {
                where = "itemName like '%" + txtSampleName.Text.Trim()+"%'";
            }
            DataTable dtitem = _sql.GetYCItemTable(where);
            if (dtitem != null && dtitem.Rows.Count > 0)
            {
                CheckDatas.DataSource = dtitem;
                CheckDatas.Columns["itemName"].HeaderText = "项目名称";
                CheckDatas.Columns["itemCode"].HeaderText = "项目编号";
                CheckDatas.Columns["itemType"].HeaderText = "项目类型";
                CheckDatas.Columns["ID"].Visible = false;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                item = CheckDatas.SelectedRows[0].Cells["itemName"].Value.ToString();
                this.Close();
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
            if (CheckDatas.SelectedRows.Count > 0)
            {
                item = CheckDatas.SelectedRows[0].Cells["itemName"].Value.ToString();
                this.Close();
            }
        }
    }
}
