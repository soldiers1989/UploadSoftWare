using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class frmQueryModifi : Form
    {
        private clsSetSqlData sql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        public int ssid = 0;


        public frmQueryModifi()
        {
            InitializeComponent();
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQueryModifi_Load(object sender, EventArgs e)
        {
            if (Global.AddItem.GetLength(0) > 0)
            {
                txtSampleName.Text = Global.AddItem[0, 0];
                txtChkItem.Text = Global.AddItem[0, 1];
                txtChkResult.Text = Global.AddItem[0, 2];
                txtUnit.Text = Global.AddItem[0, 3];
                txtTestBase.Text = Global.AddItem[0, 4];
                txtStandValue.Text = Global.AddItem[0, 5];
                txtIntrument.Text = Global.AddItem[0, 6];
                txtConclusion.Text = Global.AddItem[0, 7];
                txtdetectunit.Text = Global.AddItem[0, 8];
                txtGettime.Text = Global.AddItem[0, 9];
                txtAddress.Text = Global.AddItem[0, 10];
                txtInspectionunit.Text = Global.AddItem[0, 11];
                txtUnitNature.Text = Global.AddItem[0, 12];
                txtTester.Text = Global.AddItem[0, 13];
                txtChkTime.Text = Global.AddItem[0, 14];
                txtSampleType.Text = Global.AddItem[0, 15];//样品种类
                txtSampleNum.Text = Global.AddItem[0, 16]; //样品数量
                txtProductPlace.Text = Global.AddItem[0, 17]; //产地
                txtProductUnit.Text = Global.AddItem[0, 18]; //生产单位
                txtProductDate.Text = Global.AddItem[0, 19]; //生产日期
                txtSendDate.Text = Global.AddItem[0, 20]; //送检日期
                txtDoResult.Text = Global.AddItem[0, 21]; //处理结果

                //string err = string.Empty;
                //try
                //{
                //    sb.Append("Checkitem='");
                //    sb.Append(Global.AddItem[0, 1]);
                //    sb.Append("' and SampleName='");
                //    sb.Append(Global.AddItem[0, 0]);
                //    sb.Append("' and CheckData='");
                //    sb.Append(Global.AddItem[0, 2]);
                //    sb.Append("' and CheckTime=#");
                //    sb.Append(DateTime.Parse(Global.AddItem[0, 14]));
                //    sb.Append("#");
                //    DataTable dt = sql.isSaveID(sb.ToString(), "", out err);

                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        ssid = Convert.ToInt32(dt.Rows[0][0].ToString());
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Error");
                //}
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUpdateData updata = new clsUpdateData();
           
            string err = string.Empty;
            try 
            {
                updata.ChkSample = txtSampleName.Text.Trim();
                updata.Chkxiangmu = txtChkItem.Text.Trim();
                updata.result = txtChkResult.Text.Trim();
                updata.unit = txtUnit.Text.Trim();
                updata.Chktestbase = txtTestBase.Text.Trim();
                updata.ChklimitData = txtStandValue.Text.Trim();
                updata.intrument = txtIntrument.Text.Trim();
                updata.conclusion = txtConclusion.Text.Trim();
                updata.detectunit = txtdetectunit.Text.Trim();
                updata.GetSampTime = txtGettime.Text.Trim();
                updata.GetSampPlace = txtAddress.Text.Trim();
                updata.ChkUnit = txtInspectionunit.Text.Trim();
                updata.ChkPeople = txtTester.Text.Trim();
                updata.ChkTime = txtChkTime.Text.Trim();
                updata.sampleNum = txtSampleNum.Text.Trim();
                updata.CheckCompanyNature = txtUnitNature.Text.Trim();
                updata.sampletype = txtSampleType.Text.Trim();//样品种类
                updata.ProductPlace = txtProductPlace.Text.Trim();//产地
                updata.ProductUnit = txtProductUnit.Text.Trim();//生产单位
                updata.ProductDate = txtProductDate.Text.Trim();//生产日期
                updata.SendDate = txtSendDate.Text.Trim();//送检日期
                updata.DoResult = txtDoResult.Text.Trim();//处理结果

                sql.RepairResult(updata, ssid,out err);

                this.DialogResult = DialogResult.OK;
                MessageBox.Show("数据修改成功","提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }

            this.Close();
        }

    }
}
