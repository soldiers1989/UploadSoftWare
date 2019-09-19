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
        private clsSetSqlData sql = new clsSetSqlData();
        public string _company = "";

        public FrmCompany()
        {
            InitializeComponent();
        }


        private void FrmCompany_Load(object sender, EventArgs e)
        {
            dt = sql.GetRegulator("", "", 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                CheckDatas.DataSource = dt;
                CheckDatas.Columns["IsSelects"].HeaderText = "已选择";
                CheckDatas.Columns["reg_name"].HeaderText = "被检单位名称";
                CheckDatas.Columns["link_user"].HeaderText = "单位负责人";
                CheckDatas.Columns["reg_address"].HeaderText = "地址";
                CheckDatas.Columns["link_phone"].HeaderText = "联系信息";
                CheckDatas.Columns["update_date"].HeaderText = "更新时间";
                CheckDatas.Columns["rid"].HeaderText = "任务ID";
                CheckDatas.Columns["reg_type"].HeaderText = "单位性质";
                CheckDatas.Columns["ID"].Visible = false;
                CheckDatas.Columns["IsSelects"].Visible = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                _company = CheckDatas.SelectedRows[0].Cells["reg_name"].Value.ToString();
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

        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                _company = CheckDatas.SelectedRows[0].Cells["reg_name"].Value.ToString();
                this.Close();
            }
            else
            {
                _company = "";
                MessageBox.Show("请选择需要的记录再单击确定", "系统提示");
            }
            this.Close();
        }
    }
}
