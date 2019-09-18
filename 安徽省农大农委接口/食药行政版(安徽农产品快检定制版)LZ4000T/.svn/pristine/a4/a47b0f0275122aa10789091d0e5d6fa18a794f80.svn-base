using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DY.FoodClientLib;

namespace FoodClientTools
{
    public partial class FrmEnvirnomentSet : Form
    {
        public FrmEnvirnomentSet()
        {
            InitializeComponent();
        }

        private readonly clsSysOptOpr bll = new clsSysOptOpr();

        /// <summary>
        ///  窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEnvirnomentSet_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            bindData();
        }

        /// <summary>
        /// 绑定初始化数据
        /// </summary>
        private void bindData()
        {
            DataTable dtbl = bll.GetColumnDataTable(3, "Len(SysCode)=6", "OptValue");//SysCode LIKE '______'
            if (dtbl != null)
            {
                txtTemperature.Text = dtbl.Rows[0]["OptValue"].ToString();
                txtHumidity.Text = dtbl.Rows[1]["OptValue"].ToString();
                txtSampleUnit.Text = dtbl.Rows[2]["OptValue"].ToString();
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='010101'", txtTemperature.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat(" UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='010102' ", txtHumidity.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='010103'", txtSampleUnit.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                MessageBox.Show("操作成功");
                bindData();//重新修改数据
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
