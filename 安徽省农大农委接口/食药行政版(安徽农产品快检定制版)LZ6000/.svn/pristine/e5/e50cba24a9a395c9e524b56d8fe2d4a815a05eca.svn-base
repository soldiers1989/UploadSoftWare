using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// 设定超级授权密码
    /// 设定初始密码：73F504E1F2D86EA73483B932D5CF73DB
    /// 明码：dy010203
    /// </summary>
    public partial class FrmSuperAdminLogin : Form
    {
        public FrmSuperAdminLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
       
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string pass = txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("密码必录输入");
                return;
            }
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToString();
            //txtPwd.Text = pwd;

            string pass2 = System.Configuration.ConfigurationManager.AppSettings["SupAdmin"].ToString();
            if (pwd == pass2)
            {
                MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.SystemAdmin, "1");
                this.Close();
            }
            else
            {
                MessageBox.Show("授权密码不对！");
            }
        }
    }
}
