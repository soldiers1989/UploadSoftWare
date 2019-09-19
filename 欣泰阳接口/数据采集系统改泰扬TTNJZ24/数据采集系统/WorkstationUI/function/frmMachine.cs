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
    public partial class frmMachine : Form
    {
        public frmMachine()
        {
            InitializeComponent();
        }
        public ucEquipmenManage ucem = new ucEquipmenManage();
        private void labelClose_MouseClick(object sender, MouseEventArgs e)
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

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            ucem.CheckDatas.CurrentCell.Value = txtdata.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

       
        private void txtdata_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == 13)
            {
                ucem.CheckDatas.CurrentCell.Value = txtdata.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
