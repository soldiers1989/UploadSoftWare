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
        public string tileName = "";
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
                if (tileName == "编辑登录用户")
                {
                    //StringBuilder sb = new StringBuilder();
                    string err = string.Empty;

                    sql.UpdateUserInfo(txtUserName.Text, TxtPassword.Text, TxtUserType.Text, Global.edituser[0, 3], out err);
                    MessageBox.Show("修改成功", "提示");
                }
                else if (tileName == "新建用户")
                {
                    int save = 0;
                    save = sql.AddUser(txtUserName.Text.Trim(), TxtPassword.Text.Trim(), TxtUserType.Text.Trim());
                    if (save == 1)
                    {
                        MessageBox.Show("新增用户名成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            
            this.Close();
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            txtUserName.Text =Global.edituser[0,0];
            TxtPassword.Text = Global.edituser[0, 1];
            TxtUserType.Text = Global.edituser[0, 2];
            labelTile.Text = tileName;
        }
    }
}
