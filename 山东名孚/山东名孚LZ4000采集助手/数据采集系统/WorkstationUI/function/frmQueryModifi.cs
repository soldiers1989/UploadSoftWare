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
        private int ssid = 0;


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
                txtChkItem.Text  = Global.AddItem[0, 1];
                txtChkResult.Text = Global.AddItem[0, 2];
                txtUnit.Text  = Global.AddItem[0, 3];
                txtTestBase.Text  = Global.AddItem[0, 4];
                txtStandValue.Text = Global.AddItem[0, 5];               
                txtIntrument.Text  = Global.AddItem[0, 6];
                txtConclusion.Text = Global.AddItem[0, 7];
                txtdetectunit.Text = Global.AddItem[0, 8];
                txtGettime.Text = Global.AddItem[0, 9];
                txtAddress.Text = Global.AddItem[0, 10];
                txtInspectionunit.Text = Global.AddItem[0, 11];
                txtTester.Text = Global.AddItem[0, 12];
                txtChkTime.Text = Global.AddItem[0, 13];
                //样品数量
                txtSampleNum.Text = Global.AddItem[0, 14];
                txtSampleType.Text = Global.AddItem[0, 15];//样品种类
            }
            string err = string.Empty;
            try
            {
                sb.Append("Checkitem='");
                sb.Append(Global.AddItem[0, 1]);
                sb.Append("' and SampleName='");
                sb.Append(Global.AddItem[0, 0]);
                sb.Append("' and CheckData='");
                sb.Append(Global.AddItem[0, 2]);
                sb.Append("' and CheckTime=#");
                sb.Append(DateTime.Parse(Global.AddItem[0, 13]));
                sb.Append("#");
                DataTable dt= sql.isSaveID(sb.ToString(), "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ssid =Convert.ToInt32( dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUpdateData updata = new clsUpdateData();
            clsSetSqlData sql = new clsSetSqlData();
            string err = string.Empty;
            try 
            {
                updata.ChkSample = txtSampleName.Text;
                updata.Chkxiangmu = txtChkItem.Text;
                updata.result = txtChkResult.Text;
                updata.unit = txtUnit.Text;
                updata.Chktestbase = txtTestBase.Text;
                updata.ChklimitData = txtStandValue.Text;
                updata.intrument = txtIntrument.Text;
                updata.conclusion = txtConclusion.Text;
                updata.detectunit = txtdetectunit.Text;
                updata.GetSampTime = txtGettime.Text;
                updata.GetSampPlace = txtAddress.Text;
                updata.ChkUnit = txtInspectionunit.Text;
                updata.ChkPeople = txtTester.Text;
                updata.ChkTime = txtChkTime.Text;
                updata.sampleNum = txtSampleNum.Text;
                updata.sampletype = txtSampleType.Text;//样品种类

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
