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
        private StringBuilder sb = new StringBuilder();
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
            try
            {
                if(txtdetectunitname.Text.Trim().Length ==0)
                {
                    MessageBox.Show("被检单位名称不能为空！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Warning );
                    return;
                }
                if (txtdetectNature.Text.Trim().Length == 0)
                {
                    MessageBox.Show("被检单位编号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string err = string.Empty;
                sb.Length = 0;
                sb.AppendFormat("'{0}',", txttestunit.Text.Trim());
                sb.AppendFormat("'{0}',", txtTestunitAddr.Text.Trim());
                sb.AppendFormat("'{0}',",txtuser.Text.Trim());
                sb.AppendFormat("'{0}',", txtdetectunitname.Text.Trim());
                sb.AppendFormat("'{0}',", txtdetectNature.Text.Trim());
                sb.AppendFormat("'{0}',", txtProductAddr.Text.Trim());
                sb.AppendFormat("'{0}'", ProductUnit.Text.Trim());

                int rt= sql.SaveInformation(sb.ToString(),out err);
                if(rt==1)
                {
                    MessageBox.Show("数据保存成功", "提示");
                }    
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
