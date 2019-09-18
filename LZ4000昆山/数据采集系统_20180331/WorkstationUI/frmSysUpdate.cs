using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationModel.Model;

namespace WorkstationUI
{
    public partial class frmSysUpdate : Form
    {
        private clsdiary dy = new clsdiary();
        public frmSysUpdate()
        {
            InitializeComponent();
        }

        private void labclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSysUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 10000;
                for (int i = 0; i < 10000; i++)
                {
                    progressBar1.Value++;
                }
                dy.savediary(DateTime.Now.ToString(), "进入系统升级", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入系统升级错误："+ex.Message, "错误");
                MessageBox.Show(ex.Message,"进入系统升级");
            }
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
        const int WM_NCHITTEST = 0x0084;
        const int HT_CAPTION = 2;
        protected override void WndProc(ref Message Msg)
        {
            //禁止双击最大化
            if (Msg.Msg == 0x0112 && Msg.WParam.ToInt32() == 61490) return;
            if (Msg.Msg == WM_NCHITTEST)
            {
                //允许拖动窗体移动
                Msg.Result = new IntPtr(HT_CAPTION);
                return;
            }
            base.WndProc(ref Msg);
        }
    }
}
