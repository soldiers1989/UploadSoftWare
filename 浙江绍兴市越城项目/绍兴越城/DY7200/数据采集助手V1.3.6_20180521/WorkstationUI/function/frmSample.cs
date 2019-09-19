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
    public partial class frmSample : Form
    {
        private  YCsql _sql = new YCsql();
        private string err = "";
        private DataTable dt;
        public string sampleName = "";
        public frmSample()
        {
            InitializeComponent();
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            try
            {
                dt =_sql.GetSample("", "", out err);
                if(dt!=null && dt.Rows.Count >0)
                {
                    CheckDatas.DataSource = dt;
                   
                    CheckDatas.Columns["goodsName"].HeaderText = "样品名称";
                    CheckDatas.Columns["goodsCode"].HeaderText = "样品编号";
                    CheckDatas.Columns["goodsType"].HeaderText = "样品种类";
                    CheckDatas.Columns["goodsName"].DisplayIndex = 1;
                    CheckDatas.Columns["goodsCode"].DisplayIndex =2;
                    CheckDatas.Columns["goodsType"].DisplayIndex = 3;
                    CheckDatas.Columns["ID"].Visible  = false ;
                }
                sampleName = "";
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

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(CheckDatas.SelectedRows.Count >0)
            {
                sampleName = CheckDatas.SelectedRows[0].Cells["goodsName"].Value.ToString();

                this.Close();
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                sampleName = CheckDatas.SelectedRows[0].Cells["goodsName"].Value.ToString();

                this.Close();
            }
        }

        private void btnlSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDatas.DataSource = "";
                string where = "";
                if (txtSampleName.Text.Trim ()!="")
                {
                    where = "goodsName like '%" + txtSampleName.Text.Trim()+"%'";
                }
                dt = _sql.GetSample(where, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
               
                    CheckDatas.Columns["goodsName"].HeaderText = "样品名称";
                    CheckDatas.Columns["goodsCode"].HeaderText = "样品编号";
                    CheckDatas.Columns["goodsType"].HeaderText = "样品种类";
                    CheckDatas.Columns["goodsName"].DisplayIndex = 1;
                    CheckDatas.Columns["goodsCode"].DisplayIndex = 2;
                    CheckDatas.Columns["goodsType"].DisplayIndex = 3;
                    CheckDatas.Columns["ID"].Visible = false;
                }
                sampleName = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
