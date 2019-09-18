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
    public partial class frmKSOperators : Form
    {
        private DataTable dt = null;
        private clsSetSqlData sql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        public  bool isselect = false;
        private string err = string.Empty;
        public frmKSOperators()
        {
            InitializeComponent();
        }

        private void frmKSOperators_Load(object sender, EventArgs e)
        {
            isselect = false;
            if (Global.isselectunit==false)
            {
                txtSample.ReadOnly = false;
                searchKSmanage("");
            }
            else if (Global.isselectunit == true)
            {
                Global.isselectunit = false;
                txtSample.Text = Global.KSCompany;
                txtSample.ReadOnly = true;
                sb.Length = 0;
                sb.AppendFormat("MarketName='{0}'", Global.KSCompany);
                searchKSmanage(sb.ToString());
            }
       
        }
        private void searchKSmanage(string where)
        {
            try
            {
                dt = sql.GetAreaMarket(where, "", out err);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //displayTable.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addKSTable(dt.Rows[i]["LicenseNo"].ToString(), dt.Rows[i]["MarketName"].ToString(), dt.Rows[i]["MarketRef"].ToString(), dt.Rows[i]["Abbreviation"].ToString(), dt.Rows[i]["PositionDistrictName"].ToString(), dt.Rows[i]["PositionNo"].ToString(), dt.Rows[i]["DABH"].ToString(), dt.Rows[i]["Contactor"].ToString(), dt.Rows[i]["ContactTel"].ToString());
                    //}
                    CheckDatas.DataSource = dt;
                    CheckDatas.Columns["LicenseNo"].HeaderText = "单位主体编码";
                    CheckDatas.Columns["MarketName"].HeaderText = "单位主体名称";
                    CheckDatas.Columns["MarketRef"].HeaderText = "单位主体类别";
                    CheckDatas.Columns["Abbreviation"].HeaderText = "单位主体简称";
                    CheckDatas.Columns["PositionDistrictName"].HeaderText = "摊位区域名称";
                    CheckDatas.Columns["PositionNo"].HeaderText = "摊位号";
                    CheckDatas.Columns["DABH"].HeaderText = "身份证号";
                    CheckDatas.Columns["Contactor"].HeaderText = "姓名";
                    CheckDatas.Columns["ContactTel"].HeaderText = "手机号码";
                    CheckDatas.Columns["ID"].Visible = false;

                    CheckDatas.Columns["Contactor"].DisplayIndex = 0;
                    CheckDatas.Columns["DABH"].DisplayIndex = 1;
                    CheckDatas.Columns["PositionDistrictName"].DisplayIndex = 2;
                    CheckDatas.Columns["PositionNo"].DisplayIndex = 3;
                    CheckDatas.Columns["ContactTel"].DisplayIndex = 4;
                    CheckDatas.Columns["MarketName"].DisplayIndex = 5;
                    CheckDatas.Columns["LicenseNo"].DisplayIndex = 6;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                sb.Length = 0;
                CheckDatas.DataSource = null ;
                if (dt != null)
                dt.Clear();
                if (txtSample.Text.Trim() != "" && txtmanagename.Text.Trim() != "")
                {
                    sb.AppendFormat("MarketName like '%{0}%' and ", txtSample.Text.Trim());
                    sb.AppendFormat("PositionNo like '%{0}%'", txtmanagename.Text.Trim());
                }
                else if (txtSample.Text.Trim() != "")
                {
                    sb.AppendFormat("MarketName like '%{0}%' ", txtSample.Text.Trim());
                }
                else if (txtmanagename.Text.Trim()!="")
                {
                    sb.AppendFormat("PositionNo like '%{0}%'", txtmanagename.Text.Trim());
                }
                searchKSmanage(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要的被检单位");
                return;
            }
            Global.jingyinghu = CheckDatas.SelectedRows[0].Cells["Contactor"].Value.ToString();
            Global.sfzh = CheckDatas.SelectedRows[0].Cells["DABH"].Value.ToString();
            Global.stall = CheckDatas.SelectedRows[0].Cells["PositionNo"].Value.ToString();
            Global.KSCompany = CheckDatas.SelectedRows[0].Cells["MarketName"].Value.ToString();
            Global.KScompanyCode = CheckDatas.SelectedRows[0].Cells["LicenseNo"].Value.ToString();
            Global.Markettype = CheckDatas.SelectedRows[0].Cells["MarketRef"].Value.ToString();
            isselect = true;
            this.Close();
        }

        private void CheckDatas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Global.jingyinghu = CheckDatas.SelectedRows[0].Cells["Contactor"].Value.ToString();
            Global.sfzh = CheckDatas.SelectedRows[0].Cells["DABH"].Value.ToString();
            Global.stall = CheckDatas.SelectedRows[0].Cells["PositionNo"].Value.ToString();
            Global.KSCompany = CheckDatas.SelectedRows[0].Cells["MarketName"].Value.ToString();
            Global.KScompanyCode = CheckDatas.SelectedRows[0].Cells["LicenseNo"].Value.ToString();
            Global.Markettype = CheckDatas.SelectedRows[0].Cells["MarketRef"].Value.ToString();
            isselect = true;
            this.Close();
        }

       
    }
}
