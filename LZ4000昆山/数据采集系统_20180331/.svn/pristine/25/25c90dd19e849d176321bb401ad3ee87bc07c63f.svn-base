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
    public partial class frmKScompany : Form
    {
        private DataTable dt = null;
        private clsSetSqlData sql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        public bool isselect = false;
        public frmKScompany()
        {
            InitializeComponent();
        }

        private void frmKScompany_Load(object sender, EventArgs e)
        {
            isselect = false;
            Global.isselectunit = false;
            Searchdata("");
        }
        private void Searchdata(string where)
        {
            try
            { 
                string err = string.Empty;
                dt = sql.GetKSAreaMarket(where,out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    CheckDatas.Columns["LicenseNo"].HeaderText = "主体编码";
                    CheckDatas.Columns["MarketName"].HeaderText = "主体名称";
                    CheckDatas.Columns["MarketRef"].HeaderText = "主体类别";
                    CheckDatas.Columns["Abbreviation"].HeaderText = "主体简称";
                    CheckDatas.Columns["ID"].Visible  = false ;
                    CheckDatas.Columns["MarketName"].DisplayIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sb.Length = 0;
            CheckDatas.DataSource =null;
            if (dt != null)
            dt.Clear();
            sb.AppendFormat("MarketName like '%{0}%'", txtcompany.Text.Trim());
            Searchdata(sb.ToString());
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White ;
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isselect = true;
            Global.isselectunit = true;
            Global.KSCompany = CheckDatas.SelectedRows[0].Cells["MarketName"].Value.ToString();
            Global.KScompanyCode = CheckDatas.SelectedRows[0].Cells["LicenseNo"].Value.ToString();
            Global.Markettype = CheckDatas.SelectedRows[0].Cells["MarketRef"].Value.ToString();
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要的被检单位");
                return;
            }
            isselect = true;
            Global.isselectunit = true;
            Global.KSCompany = CheckDatas.SelectedRows[0].Cells["MarketName"].Value.ToString();
            Global.KScompanyCode = CheckDatas.SelectedRows[0].Cells["LicenseNo"].Value.ToString();
            Global.Markettype = CheckDatas.SelectedRows[0].Cells["MarketRef"].Value.ToString();
            this.Close();
        }
    }
}
