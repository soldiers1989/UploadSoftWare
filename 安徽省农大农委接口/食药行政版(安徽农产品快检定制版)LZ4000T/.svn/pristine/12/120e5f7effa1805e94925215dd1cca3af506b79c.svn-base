using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LicensesTool
{
    public partial class FrmPwd : Form
    {
        public FrmPwd()
        {
            InitializeComponent();
            txtPwd.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string pwd = txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("请输入密码");
                txtPwd.Focus();
                return;
            }
            DY.FileLib.INIFile ini = new DY.FileLib.INIFile(AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
            string pass = ini.IniReadValue("Password", "pwd");
			//pass = DY.Security.RandomEncryption.Operate(pass, false);//解密
            if (pwd != pass)
            {
                MessageBox.Show("密码不对");
                txtPwd.Focus();
                return;
            }
            FrmSetParam para = new FrmSetParam();
            para.ShowDialog();
            para.BringToFront();
            this.Dispose();
            
        }
    }
}
