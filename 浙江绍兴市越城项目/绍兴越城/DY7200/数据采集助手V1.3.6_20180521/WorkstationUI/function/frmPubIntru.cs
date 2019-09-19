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
    public partial class frmPubIntru : Form
    {
        private clsSetSqlData sql = new clsSetSqlData();
        public frmPubIntru()
        {
            InitializeComponent();
        }

        private void frmPubIntru_Load(object sender, EventArgs e)
        {
            txtIntrumentNo.Text=Global.ediIntrument[0,0];
            txtIntruName.Text = Global.ediIntrument[0, 1];
            txtManifact.Text = Global.ediIntrument[0, 2];
            txtCommunicat.Text = Global.ediIntrument[0, 3];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string err = "";
            sb.Append("Numbering='");
            sb.Append(txtIntrumentNo.Text.Trim());
            sb.Append("',Name='");
            sb.Append(txtIntruName.Text.Trim());
            sb.Append("',Manufacturer='");
            sb.Append(txtManifact.Text.Trim());
            sb.Append("',communication='");
            sb.Append(txtCommunicat.Text.Trim());
           
            
            sql.ModifeIntrument(sb.ToString(), Global.ediIntrument[0, 4], out err);

            this.Close();
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
    }
}
