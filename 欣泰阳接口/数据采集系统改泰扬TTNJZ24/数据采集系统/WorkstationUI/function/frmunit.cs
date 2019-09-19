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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            MessageBox.Show("数据保存成功", "提示");

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
    }
}
