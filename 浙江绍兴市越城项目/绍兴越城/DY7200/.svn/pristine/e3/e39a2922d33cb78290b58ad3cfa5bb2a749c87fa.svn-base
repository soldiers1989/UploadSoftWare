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
    public partial class frmOpertor : Form
    {
        public string _Opertor = "";
        public string _market = "";
        private DataTable cdt=null;
        private YCsql _sql = new YCsql();
        public frmOpertor()
        {
            InitializeComponent();
        }

        private void frmOpertor_Load(object sender, EventArgs e)
        {

              cdt = _sql.GetYCOpertorTable("marketName='" + _market+"'");
              if (cdt != null && cdt.Rows.Count > 0)
              {
                  CheckDatas.DataSource = cdt;
                  CheckDatas.Columns["stallNo"].HeaderText = "摊位号";
                  CheckDatas.Columns["operatorCode"].HeaderText = "经营户编号";
                  CheckDatas.Columns["operatorName"].HeaderText = "经营户";
                  CheckDatas.Columns["socialCreditCode"].HeaderText = "社会编号";
                  CheckDatas.Columns["marketName"].HeaderText = "市场名称";
                  CheckDatas.Columns["marketCode"].HeaderText = "市场编号";

                  CheckDatas.Columns["isselect"].Visible = false;
                  CheckDatas.Columns["ID"].Visible = false;

                  CheckDatas.Columns["operatorName"].DisplayIndex = 1;
                  CheckDatas.Columns["operatorCode"].DisplayIndex = 2;
                  CheckDatas.Columns["socialCreditCode"].DisplayIndex = 3;
                  CheckDatas.Columns["marketName"].DisplayIndex = 4;
                  CheckDatas.Columns["marketCode"].DisplayIndex = 5;

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

        private void btnlSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string where = "marketName='" + _market + "' ";
                if (txtSampleName.Text.Trim()!="")
                {
                    where += " and operatorName like '%"+txtSampleName.Text.Trim ()+"%'";
                }
                cdt = _sql.GetYCOpertorTable(where);
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = cdt;
                    CheckDatas.Columns["stallNo"].HeaderText = "摊位号";
                    CheckDatas.Columns["operatorCode"].HeaderText = "经营户编号";
                    CheckDatas.Columns["operatorName"].HeaderText = "经营户";
                    CheckDatas.Columns["socialCreditCode"].HeaderText = "社会编号";
                    CheckDatas.Columns["marketName"].HeaderText = "市场名称";
                    CheckDatas.Columns["marketCode"].HeaderText = "市场编号";

                    CheckDatas.Columns["isselect"].Visible = false;
                    CheckDatas.Columns["ID"].Visible = false;

                    CheckDatas.Columns["operatorName"].DisplayIndex = 1;
                    CheckDatas.Columns["operatorCode"].DisplayIndex = 2;
                    CheckDatas.Columns["socialCreditCode"].DisplayIndex = 3;
                    CheckDatas.Columns["marketName"].DisplayIndex = 4;
                    CheckDatas.Columns["marketCode"].DisplayIndex = 5;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if(CheckDatas.SelectedRows.Count >0)
                {
                    _Opertor = CheckDatas.SelectedRows[0].Cells["operatorName"].Value.ToString();
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void CheckDatas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (CheckDatas.SelectedRows.Count > 0)
                {
                    _Opertor = CheckDatas.SelectedRows[0].Cells["operatorName"].Value.ToString();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
