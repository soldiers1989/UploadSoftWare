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
    public partial class frmunit : Form
    {
        private clsSetSqlData sql = new clsSetSqlData();
        public frmunit()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //if (txtdetectunitname.Text.Trim() == "")
            //{
            //    MessageBox.Show("被检单位不能为空","Error");
            //    return;
            //}
            try
            {
                string err = string.Empty;
               
                StringBuilder sb = new StringBuilder();
                sb.Append(txttestunit.Text );
                sb.Append("','");
                sb.Append(txtTestunitAddr.Text);
                sb.Append("','");
                sb.Append(txtdetectunitname.Text);
                sb.Append("','");
                sb.Append(txtdetectaddr.Text);
                sb.Append("','");
                sb.Append(txtuser.Text);
                sb.Append("','");
                sb.Append("否");
                //sb.Append("','");
                //sb.Append(DTPsampletime.Value);
                //sb.Append("','");
                //sb.Append(txttestbase.Text);
                sql.SaveInformation(sb.ToString());
                MessageBox.Show("数据保存成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void frmunit_Load(object sender, EventArgs e)
        {
             //Global.s
            txttestunit.Text = Global.DetectUnit;
            txtuser.Text = Global.NickName;
            //查询被检单位
            string err="";
            try
            {
                DataTable dtcompany = sql.GetExamedUnit("", "", out err);
                if (dtcompany != null && dtcompany.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcompany.Rows.Count; i++)
                    {
                        cmbExamedUnit.Items.Add(dtcompany.Rows[i][2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                     
        }
    }
}
