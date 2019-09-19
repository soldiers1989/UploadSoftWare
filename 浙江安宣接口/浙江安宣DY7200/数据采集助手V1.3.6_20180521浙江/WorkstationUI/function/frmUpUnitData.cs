using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkstationUI.function
{
    public partial class frmUpUnitData : Form
    {
        public frmUpUnitData()
        {
            InitializeComponent();
        }

        private void frmUpUnitData_Load(object sender, EventArgs e)
        {

        }
        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void BtnCommunicate_Click(object sender, EventArgs e)
        {
            
            
                BtnCommunicate.Enabled = false;
                string data = "<T_farmsDTS>" +
                    "<Farms>"
                    + "<FARMID>" + txtUnitID.Text.Trim() + "</FARMID>"
                    + "<PWD>" + txtPassword.Text.Trim() + "</PWD>"
                    + "<NAMES>" + txtUnitName.Text.Trim() + "</NAMES>"
                    + "<REGIONID>" + txtUnitID.Text.Trim() + "</REGIONID>"
                    + "<ADDRE>" + txtPlaceAddr.Text.Trim() + "</ADDRE>"
                    + "<FARMURL>" + txtCompanyweb.Text.Trim() + "</FARMURL>"
                    + "<LINKMAN>" + txtLinker.Text.Trim() + "</LINKMAN> "
                    + "<TELEPHONE>" + txtPhone.Text.Trim() + "</TELEPHONE>"
                    + "<BUILDTIME>" + txtCreateDate.Text.Trim() + "</BUILDTIME>"
                    + "<USERID>" + txtUserID.Text.Trim() + "</USERID>"
                    + "<GROUPID>" + txtGroundID.Text.Trim() + "</GROUPID>"
                    + "</Farms>"
                    + "</T_farmsDTS>";


                bool rtn = WorkstationModel.shandong.SDUpdata.UpUnit(data);
                if (rtn == true)
                {
                    MessageBox.Show("检测单位信息同步成功", "操作提示");
                }
                BtnCommunicate.Enabled = true;
            
        
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
