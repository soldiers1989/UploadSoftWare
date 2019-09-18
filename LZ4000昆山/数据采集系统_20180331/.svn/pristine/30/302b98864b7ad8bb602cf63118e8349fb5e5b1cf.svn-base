using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkstationUI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void labelclose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void labelclose_MouseEnter(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.Red;
        }

        private void labelclose_MouseLeave(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.White;
        }

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
    }
}
