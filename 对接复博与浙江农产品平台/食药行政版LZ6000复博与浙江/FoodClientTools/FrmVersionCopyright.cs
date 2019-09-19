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
    public partial class FrmVersionCopyright : Form
    {
        public FrmVersionCopyright()
        {
            InitializeComponent();
        }

        private readonly clsSysOptOpr bll = new clsSysOptOpr();
        /// <summary>
        /// 窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCopyRight_Load(object sender, EventArgs e)
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
            DataTable dtbl = bll.GetColumnDataTable(0, "Len(SysCode)=6 AND OptType='03'", "OptValue");//SysCode LIKE '______'
            if (dtbl != null&&dtbl.Rows.Count>0)
            {
                cbSysVersion.SelectedItem = dtbl.Rows[0]["OptValue"].ToString();
                //txtVersionName.Text = dtbl.Rows[0]["OptValue"].ToString();
                txtVersionNum.Text = dtbl.Rows[1]["OptValue"].ToString();
                txtSystemName.Text = dtbl.Rows[2]["OptValue"].ToString();
                txtCompanyName.Text = dtbl.Rows[3]["OptValue"].ToString();
                txtServicePhone.Text = dtbl.Rows[4]["OptValue"].ToString();
                txtWebSite.Text = dtbl.Rows[5]["OptValue"].ToString();
                txtAddress.Text = dtbl.Rows[6]["OptValue"].ToString();
                txtZipcode.Text = dtbl.Rows[7]["OptValue"].ToString();
                bool isEnabled = Convert.ToBoolean(dtbl.Rows[8]["OptValue"].ToString());
                chbIsDataLink.Checked = isEnabled;
                cmbInterface.SelectedItem = dtbl.Rows[9]["OptValue"].ToString();
                cbAppType.SelectedItem = dtbl.Rows[10]["OptValue"].ToString();
             
            }
        }
        
        /// <summary>
        /// 保存操作,由于Access不能支持像";或者go"批量更新,只能一行行更新，
        /// 比较浪费，但这些配置好了，就不在做什么操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030101'", cbSysVersion.SelectedItem.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030102'", txtVersionNum.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030103'", txtSystemName.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030104'", txtCompanyName.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030105'", txtServicePhone.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030106'", txtWebSite.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030107'", txtAddress.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030108'",txtZipcode.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030109'", chbIsDataLink.Checked.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030110'", cmbInterface.SelectedItem.ToString());
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030111'", cbAppType.SelectedItem.ToString());
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbIsDataLink_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = !chbIsDataLink.Checked;
            lblInterface.Enabled = isEnabled;
            cmbInterface.Enabled = isEnabled;
        }

      
    }
}
