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
    public partial class frmItemshow : Form
    {
        private clsSetSqlData sqlSet = new clsSetSqlData();
        string err = "";
        private DataTable dt = null;
        public string Itemcodes = "";
        public frmItemshow()
        {
            InitializeComponent();
        }

        private void frmItemshow_Load(object sender, EventArgs e)
        {
            Itemcodes = "";
            Search("");
        }
        private void Search(string name)
        {
            CheckDatas.DataSource = null;
            if (name == "")
            {
                dt = sqlSet.GetZJItem("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                }
            }
            else
            {
                dt = sqlSet.GetZJItem("itemName like '%"+name+"%'", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                }
            }
            if(CheckDatas!=null && CheckDatas.Rows.Count >0)
            {
                CheckDatas.Columns["ID"].HeaderText = "ID";
                CheckDatas.Columns["itemName"].HeaderText = "项目名称";
                CheckDatas.Columns["itemCode"].HeaderText = "项目编号";
            }

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text.Trim() != "")
            {
                Search(txtItemName.Text.Trim());
            }
            else
            {
                Search("");
            }
            
        }

        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Itemcodes = CheckDatas.SelectedRows[0].Cells["itemName"].Value.ToString();
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                Itemcodes = CheckDatas.SelectedRows[0].Cells["itemName"].Value.ToString();
                this.Close();
            }
            else 
            {
                MessageBox.Show("请选择需要的检测项目再单击选择","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

       
    }
}
