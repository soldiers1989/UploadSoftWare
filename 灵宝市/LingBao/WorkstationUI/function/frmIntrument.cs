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
    public partial class frmIntrument : Form
    {

        private clsSetSqlData sql = new clsSetSqlData();
        public frmIntrument()
        {
            InitializeComponent();
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

        private void frmIntrument_Load(object sender, EventArgs e)
        {
            txtIntrumentNo.Text =Global.ediIntrument[0,0];
            txtIntrumentName.Text = Global.ediIntrument[0, 1];
            txtmadeplace.Text=  Global.ediIntrument[0, 2];
            txtCommunication.Text = Global.ediIntrument[0, 3];
            txtProtocol.Text = Global.ediIntrument[0, 4];
        }

        private void btnrepair_Click(object sender, EventArgs e)
        {
            StringBuilder sb=new StringBuilder ();
            string err = "";
            sb.Append("Numbering='");
            sb.Append(txtIntrumentNo.Text.Trim());
            sb.Append("',Name='");
            sb.Append(txtIntrumentName.Text.Trim());
            sb.Append("',Manufacturer='");
            sb.Append(txtmadeplace.Text.Trim());        
            sb.Append("',communication='");
            sb.Append(txtCommunication.Text.Trim());          
            sb.Append("',Protocol='");
            sb.Append(txtProtocol.Text.Trim());           

            sql.ModifeIntrument(sb.ToString(), Global.ediIntrument[0, 5],out err);

            this.Close();
        }
    }
}
