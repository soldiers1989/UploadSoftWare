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
    public partial class frmEditUser : Form
    {
        private clsSetSqlData sql = new clsSetSqlData();
        public frmEditUser()
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //StringBuilder sb = new StringBuilder();
                string err = string.Empty;

                sql.UpdateUserInfo(txtUserName.Text, TxtPassword.Text, TxtUserType.Text, Global.edituser[0, 3], out err);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            MessageBox.Show("修改成功","提示");
            this.Close();
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            txtUserName.Text =Global.edituser[0,0];
            TxtPassword.Text = Global.edituser[0, 1];
            TxtUserType.Text = Global.edituser[0, 2];


        }
    }
}
