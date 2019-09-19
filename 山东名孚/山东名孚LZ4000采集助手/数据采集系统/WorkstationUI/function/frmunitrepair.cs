using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class frmunitrepair : Form
    {
        private StringBuilder sb = new StringBuilder();
        private clsSetSqlData sqld = new clsSetSqlData();
        private int sid = 0;
        public frmunitrepair()
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

        private void btnrepair_Click(object sender, EventArgs e)
        {
            //if (cmbExamedUnit.Text.Trim() == "")
            //{
            //    MessageBox.Show("被检单位不能为空", "Error");
            //    return;
            //}
            try
            {
                string err=string.Empty ;
                sb.Clear();
                sb.Append("TestUnitName='");
                sb.Append(txtTestUnit.Text);
                sb.Append("',TestUnitAddr='");
                sb.Append(txtUnitAddr.Text);
                sb.Append("',DetectUnitName='");
                sb.Append(txtChkUnit.Text );
                sb.Append("',SampleAddress='");
                sb.Append(txtChkAddr.Text);
                sb.Append("',Tester='");
                sb.Append(txtTestUser.Text);
                sb.Append("'");

                sqld.updateunit(sb.ToString(), sid, out err);

                this.DialogResult = DialogResult.OK;
                MessageBox.Show("修改成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void frmunitrepair_Load(object sender, EventArgs e)
        {
            if (Global.repairunit.GetLength(0) > 0)
            {
                for (int i = 0; i < Global.repairunit.GetLength(0); i++)
                {
                    txtTestUnit.Text = Global.repairunit[i, 0];
                    txtUnitAddr.Text = Global.repairunit[i, 1];
                    txtTestUser.Text = Global.repairunit[i, 2];
                    txtChkUnit.Text = Global.repairunit[i, 3];
                    txtChkAddr.Text = Global.repairunit[i, 4];
                }
            }
            string err = string.Empty;
            try
            {
                
                sb.Clear();
                sb.Append("TestUnitName='");
                sb.Append(txtTestUnit.Text);
                sb.Append("' and TestUnitAddr='");
                sb.Append(txtUnitAddr.Text);
                sb.Append("' and DetectUnitName='");
                sb.Append(txtChkUnit.Text);
                sb.Append("' and SampleAddress='");
                sb.Append(txtChkAddr.Text);
                sb.Append("' and Tester='");
                sb.Append(txtTestUser.Text);
                sb.Append("'");

                DataTable dt = sqld.GetInformation(sb.ToString(), "", out err);
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         sid = (int)dt.Rows[0][10];  //获取ID号修改数据
                     }
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }

            //查询被检单位
            
            try
            {
                DataTable dtcompany = sqld.GetExamedUnit("", "", out err);
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
