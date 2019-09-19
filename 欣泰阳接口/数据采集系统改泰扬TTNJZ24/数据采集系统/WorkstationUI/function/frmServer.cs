using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            Txt_Url.Text = ConfigurationManager.AppSettings["ServerAddr"];
            Txt_User.Text = ConfigurationManager.AppSettings["ServerName"];
            Txt_PassWord.Text = ConfigurationManager.AppSettings["ServerPassword"];
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Global.ServerAdd = Txt_Url.Text;
            Global.ServerName = Txt_User.Text;
            Global.ServerPassword = Txt_PassWord.Text;
            //保存服务器配置信息到配置文件
            Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //服务器IP设置
            string str = ConfigurationManager.AppSettings["ServerAddr"];
            if (str != null)
            {
                cf.AppSettings.Settings["ServerAddr"].Value = Global.ServerAdd;
            }
            else
            {
                cf.AppSettings.Settings.Add("ServerAddr", Global.ServerAdd);
            }
            //服务器名
            string strname = ConfigurationManager.AppSettings["ServerName"];
            if (strname != null)
            {
                cf.AppSettings.Settings["ServerName"].Value = Global.ServerName;
            }
            else
            {
                cf.AppSettings.Settings.Add("ServerName", Global.ServerName);
            }
            //服务器密码
            string pwd = ConfigurationManager.AppSettings["ServerPassword"];
            if (pwd != null)
            {
                cf.AppSettings.Settings["ServerPassword"].Value = Global.ServerPassword;
            }
            else
            {
                cf.AppSettings.Settings.Add("ServerPassword", Global.ServerPassword);
            }
            cf.Save();
            ConfigurationManager.RefreshSection("appSettings");

            this.Close();

        }
    }
}
