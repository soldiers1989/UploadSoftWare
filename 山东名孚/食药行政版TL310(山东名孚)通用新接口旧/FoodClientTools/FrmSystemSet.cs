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
    public partial class FrmSystemSet : Form
    {
        private readonly clsSysOptOpr bll = new clsSysOptOpr();
        public FrmSystemSet()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020105'", chbAllowHandInputCheckUnit.Checked.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020106'", chbAutoCreateCheckCode.Checked.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020107'", txtFormatMachineCode.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020108'", txtFormatHandInputCode.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020301'",numericUpDown1.Value.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020302'", numericUpDown2.Value.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020303'", numericUpDown3.Value.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020304'", numericUpDown4.Value.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                MessageBox.Show("操作成功");
                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmSystemSet_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            bindData();
        }

        /// <summary>
        /// 绑定初始化数据
        /// 对应该数据表SysOpt 03打头的行
        /// </summary>
        private void bindData()
        {
            DataTable dtbl = bll.GetColumnDataTable(0, "Len(SysCode)=6 AND OptType IN('0201','0203')", "OptValue");//SysCode LIKE '______'
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                chbAllowHandInputCheckUnit.Checked = Convert.ToBoolean(dtbl.Rows[0]["OptValue"].ToString());
                chbAutoCreateCheckCode.Checked = Convert.ToBoolean(dtbl.Rows[1]["OptValue"].ToString());
                txtFormatMachineCode.Text = dtbl.Rows[2]["OptValue"].ToString();
                txtFormatHandInputCode.Text = dtbl.Rows[3]["OptValue"].ToString();
                numericUpDown1.Value = Convert.ToDecimal(dtbl.Rows[4]["OptValue"].ToString());
                numericUpDown2.Value = Convert.ToDecimal(dtbl.Rows[5]["OptValue"].ToString());
                numericUpDown3.Value = Convert.ToDecimal(dtbl.Rows[6]["OptValue"].ToString());
                numericUpDown4.Value = Convert.ToDecimal(dtbl.Rows[7]["OptValue"].ToString());
            }
        }
    }
}
