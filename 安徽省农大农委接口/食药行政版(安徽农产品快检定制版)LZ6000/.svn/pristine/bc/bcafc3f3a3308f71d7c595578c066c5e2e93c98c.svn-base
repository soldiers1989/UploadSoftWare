using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodClient.Query
{
    public partial class Choose : Form
    {
        public Choose()
        {
            InitializeComponent();
        }

        frmPesticideMeasure _form = new frmPesticideMeasure(1);

        /// <summary>
        /// 多项目生成报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kBtnMultiple_Click(object sender, EventArgs e)
        {
            _form.Multiple();
            this.Close();
        }

        private void kBtnOne_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能开发中。");
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Choose_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = this.MinimizeBox = false;
        }
    }
}
