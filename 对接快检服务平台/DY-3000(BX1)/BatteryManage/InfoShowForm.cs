using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryManage
{
    public partial class InfoShowForm : Form
    {
        public string info = string.Empty;

        public InfoShowForm()
        {
            InitializeComponent();
        }

        private void btn_ClosedWindow_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe", "-s");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //若需继续使用，则取消定时关机
            Process.Start("shutdown.exe", "-a");//取消定时关机
            MessageBox.Show("电量即将告罄，请接通电源继续使用或关闭系统！", "提示", MessageBoxButtons.OKCancel);
            this.Close();
        }

        private void InfoShowForm_Load(object sender, EventArgs e)
        {
            //进入此界面说明电量已经不足，直接定时三分钟自动关机
            Process.Start("shutdown.exe", "-s -t " + 180);//定时关机
            if (info.Length > 0)
            {
                label_info.Text = info;
            }
        }

    }
}