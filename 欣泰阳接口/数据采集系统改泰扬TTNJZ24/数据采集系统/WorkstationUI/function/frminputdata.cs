using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationUI.machine;

namespace WorkstationUI.function
{
    public partial class frminputdata : Form
    {
        public frminputdata()
        {
            InitializeComponent();
        }
        public frmRepairData frd;
        public frmSetResult Redata;
        public frmnewdata fnd;
        public ucTTNJ16 TTN;

        private void frminputdata_Load(object sender, EventArgs e)
        {
            
        }

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            //if (Global.newdata == "查询")
            //{
            //    frd.CheckDatas.CurrentCell.Value = txtinput.Text;
            //    frd.cmbAdd.Items.Add(txtinput.Text);
            //    frd.cmbAdd.Visible = false;
            //}
            //else if (Global.newdata == "保存编辑数据")
            //{
            //    Redata.CheckDatas.CurrentCell.Value = txtinput.Text;
            //    Redata.cmbAdd.Items.Add(txtinput.Text);
            //    Redata.cmbAdd.Visible = false;
            //}
            //else if (Global.newdata == "少查询")
            //{
            //    fnd.CheckDatas.CurrentCell.Value = txtinput.Text;
            //    fnd.cmbAdd.Items.Add(txtinput.Text);
            //    fnd.cmbAdd.Visible = false;
            //}
            TTN.CheckDatas.CurrentCell.Value = txtinput.Text;
            this.Close();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 判断按下回车键时数据输入完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtinput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==13)
            {
                //if (Global.newdata == "查询")
                //{
                //    frd.CheckDatas.CurrentCell.Value = txtinput.Text;
                //    frd.cmbAdd.Items.Add(txtinput.Text);
                //    frd.cmbAdd.Visible = false;
                //}
                //else if (Global.newdata == "保存编辑数据")
                //{
                //    Redata.CheckDatas.CurrentCell.Value = txtinput.Text;
                //    Redata.cmbAdd.Items.Add(txtinput.Text);
                //    Redata.cmbAdd.Visible = false;
                //}
                //else if (Global.newdata == "少查询")
                //{
                //    fnd.CheckDatas.CurrentCell.Value = txtinput.Text;
                //    fnd.cmbAdd.Items.Add(txtinput.Text);
                //    fnd.cmbAdd.Visible = false;
                //}
                TTN.CheckDatas.CurrentCell.Value = txtinput.Text;
                this.Close();
            }         
        }

        private void labelClose_MouseDown(object sender, MouseEventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }
   
    }
}
