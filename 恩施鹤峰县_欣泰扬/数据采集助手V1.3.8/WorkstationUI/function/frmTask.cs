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
    public partial class frmTask : Form
    {
        private StringBuilder sb = new StringBuilder();
        private clsSetSqlData sql = new clsSetSqlData();
        private DataTable dt = null;
        private string err = "";

        public string sample = "";
        public string item = "";
        public string taskname = "";
        public string _ID="";
        public string company = "";
        public frmTask()
        {
            InitializeComponent();
        }

      
        private void frmTask_Load(object sender, EventArgs e)
        {
            dt = sql.GetQtask("", "s_sampling_date", 4);
            if (dt != null && dt.Rows.Count > 0)
            {
                CheckDatas.DataSource = dt;
                IniDataGrid();
            }
        }
        private void IniDataGrid()
        {
            CheckDatas.Columns["Tests"].Visible = false;
            CheckDatas.Columns["Tests"].HeaderText = "已选择";
            CheckDatas.Columns["sample_code"].HeaderText = "抽样单编号";
            CheckDatas.Columns["item_name"].HeaderText = "检测项目";
            CheckDatas.Columns["food_name"].HeaderText = "样品名称";
            CheckDatas.Columns["s_reg_name"].HeaderText = "被检单位";
            CheckDatas.Columns["s_ope_shop_code"].HeaderText = "档口号";
            CheckDatas.Columns["s_ope_shop_name"].HeaderText = "档口名称";
            CheckDatas.Columns["s_sampling_date"].HeaderText = "抽样时间";
            CheckDatas.Columns["t_task_title"].HeaderText = "任务名称";
            CheckDatas.Columns["Checktype"].HeaderText = "状态";
            CheckDatas.Columns["ID"].Visible = false;


        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSearch.Enabled = false;
                sb.Length = 0;
                sb.AppendFormat("food_name like '{0}'", textBoxSample.Text.Trim());
                CheckDatas.DataSource = dt;

                dt = sql.GetQtask("", "s_sampling_date", 4);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    IniDataGrid();
                }
                BtnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnSearch.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                sample = CheckDatas.SelectedRows[0].Cells["food_name"].Value.ToString();
                item = CheckDatas.SelectedRows[0].Cells["item_name"].Value.ToString();
                taskname = CheckDatas.SelectedRows[0].Cells["t_task_title"].Value.ToString();
                _ID = CheckDatas.SelectedRows[0].Cells["ID"].Value.ToString();
                company = CheckDatas.SelectedRows[0].Cells["s_reg_name"].Value.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("请先选择需要的检测任务，再单击选择");
            }
           
        }
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CheckDatas.SelectedRows.Count > 0)
            {
                sample = CheckDatas.SelectedRows[0].Cells["food_name"].Value.ToString();
                item = CheckDatas.SelectedRows[0].Cells["item_name"].Value.ToString();
                taskname = CheckDatas.SelectedRows[0].Cells["t_task_title"].Value.ToString();
                _ID = CheckDatas.SelectedRows[0].Cells["ID"].Value.ToString();
                company = CheckDatas.SelectedRows[0].Cells["s_reg_name"].Value.ToString();
            }

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

    }
}
