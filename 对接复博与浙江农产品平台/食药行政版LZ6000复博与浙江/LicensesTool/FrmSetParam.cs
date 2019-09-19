using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LicensesTool
{
    public partial class FrmSetParam : Form
    {
        DY.FileLib.INIFile ini =null; 
        public FrmSetParam()
        {
            InitializeComponent();
            ini =new DY.FileLib.INIFile(AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        }
        private void FrmSetParam_Load(object sender, EventArgs e)
        {
			//txtStart.Text = DY.Security.RandomEncryption.Operate(ini.IniReadValue("DateSetting", "Day"), false);
			//txtInterval.Text = DY.Security.RandomEncryption.Operate(ini.IniReadValue("DateSetting", "Interval"), false);
			//txtPwd.Text = DY.Security.RandomEncryption.Operate(ini.IniReadValue("Password", "pwd"), false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string start = txtStart.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            string interval = txtInterval.Text.Trim();
            if (string.IsNullOrEmpty(start))
            {
                MessageBox.Show("起始时间不能为空");
                return;
            }
            if (string.IsNullOrEmpty(interval))
            {
                MessageBox.Show("时间间隔不能为空");
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            try
            {
				//ini.IniWriteValue("DateSetting", "Day", DY.Security.RandomEncryption.Operate(start, true));
				//ini.IniWriteValue("DateSetting", "Interval", DY.Security.RandomEncryption.Operate(interval, true));
				//ini.IniWriteValue("Password", "pwd", DY.Security.RandomEncryption.Operate(pwd, true));
                MessageBox.Show("设置成功");
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
