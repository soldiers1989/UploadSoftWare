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
    public partial class FrmCompany : Form
    {
        private DataTable dt = null;
        private YCsql _sql = new YCsql();
        public string _company = "";

        public FrmCompany()
        {
            InitializeComponent();
        }


        private void FrmCompany_Load(object sender, EventArgs e)
        {
            dt = _sql.GetYCMarket("");
            if (dt != null && dt.Rows.Count > 0)
            {
                CheckDatas.DataSource = dt;
                CheckDatas.Columns["marketCode"].HeaderText = "市场名称";
                CheckDatas.Columns["marketName"].HeaderText = "市场编号";

                CheckDatas.Columns["ID"].Visible = false;
              
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDatas.DataSource = null;
                string where = "";
                if(textBoxCompany.Text.Trim()!="")
                {
                    where = "marketName like '%" + textBoxCompany.Text.Trim()+"%'";
                }
                dt = _sql.GetYCMarket(where);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    CheckDatas.Columns["marketCode"].HeaderText = "市场名称";
                    CheckDatas.Columns["marketName"].HeaderText = "市场编号";

                    CheckDatas.Columns["ID"].Visible = false;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                _company = CheckDatas.SelectedRows[0].Cells["marketName"].Value.ToString();
                this.Close();
            }
            else
            {
                _company = "";
                MessageBox.Show("请选择需要的记录再单击确定","系统提示");
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

     

        private void CheckDatas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                _company = CheckDatas.SelectedRows[0].Cells["marketName"].Value.ToString();
                this.Close();
            }
            else
            {
                _company = "";
                MessageBox.Show("请选择需要的记录再单击确定", "系统提示");
            }
        }
    }
}
