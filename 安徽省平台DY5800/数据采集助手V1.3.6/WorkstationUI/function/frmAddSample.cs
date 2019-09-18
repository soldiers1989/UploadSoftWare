using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class frmAddSample : Form
    {
        public string SaveRepair = string.Empty;
        public string id = string.Empty;
        private StringBuilder sb = new StringBuilder();
        private clsSetSqlData sql = new clsSetSqlData();

        public frmAddSample()
        {
            InitializeComponent();
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void frmAddSample_Load(object sender, EventArgs e)
        {
            if (SaveRepair == "修改")
            {
                label5.Text = "修改样品";
                txtSampleName.Text = Global.repairSample[0, 0];
                txtItemName.Text = Global.repairSample[0, 1];
                txtTestBase.Text = Global.repairSample[0, 2];
                txtStandValue.Text = Global.repairSample[0, 3];
                txtSymbol.Text = Global.repairSample[0, 4];
                txtUnit.Text = Global.repairSample[0, 5];
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSampleName.Text == "")
            {
                MessageBox.Show("样品名称不能为空","提示");
                return;
            }
            if (txtItemName.Text == "")
            {
                MessageBox.Show("检测项目不能为空", "提示");
                return;
            }           
            if (txtTestBase.Text == "")
            {
                MessageBox.Show("检测依据不能为空", "提示");
                return;
            }
            if (txtStandValue.Text == "")
            {
                MessageBox.Show("标准值不能为空", "提示");
                return;
            }
            if (txtSymbol.Text == "")
            {
                MessageBox.Show("判定符号不能为空", "提示");
                return;
            }
            if (txtUnit.Text == "")
            {
                MessageBox.Show("数值单位不能为空", "提示");
                return;
            }

            try
            {
                string err = string.Empty;
                if (SaveRepair == "修改")
                {
                    sb.Clear();
                    if (Global.Platform == "DYBus")
                    {
                        sb.Append("sampleName='");
                        sb.Append(txtSampleName.Text);
                        sb.Append("',");
                        sb.Append("itemName='");
                        sb.Append(txtItemName.Text);
                        sb.Append("',");
                        sb.Append("standardValue='");
                        sb.Append(txtStandValue.Text);
                        sb.Append("',");
                        sb.Append("checkSign='");
                        sb.Append(txtSymbol.Text);
                        sb.Append("',");
                        sb.Append("checkValueUnit='");
                        sb.Append(txtUnit.Text);
                        sb.Append("',");
                        sb.Append("standardName='");
                        sb.Append(txtTestBase.Text);
                        sb.Append("'");
                    }
                    else
                    {
                        sb.Append("FtypeNmae='");
                        sb.Append(txtSampleName.Text);
                        sb.Append("',");
                        sb.Append("Name='");
                        sb.Append(txtItemName.Text);
                        sb.Append("',");
                        sb.Append("StandardValue='");
                        sb.Append(txtStandValue.Text);
                        sb.Append("',");
                        sb.Append("Demarcate='");
                        sb.Append(txtSymbol.Text);
                        sb.Append("',");
                        sb.Append("Unit='");
                        sb.Append(txtUnit.Text);
                        sb.Append("',");
                        sb.Append("ItemDes='");
                        sb.Append(txtTestBase.Text);
                        sb.Append("'");
                    }

                    sql.SaveDYSample(sb.ToString(), id, 1, out err);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("数据修改成功", "提示");
                }
                else 
                {
                    sb.Clear();
                    sb.Append("'");
                    sb.Append(txtSampleName.Text);
                    sb.Append("','");
                    sb.Append(DateTime.Now.Millisecond.ToString());
                    sb.Append("','");
                    sb.Append(txtItemName.Text);
                    sb.Append("','");
                    sb.Append(txtTestBase.Text);
                    sb.Append("','");
                    sb.Append(txtStandValue.Text);
                    sb.Append("','");               
                    sb.Append(txtSymbol.Text);
                    sb.Append("','");                 
                    sb.Append(txtUnit.Text);
                    sb.Append("'");

                    sql.SaveDYSample(sb.ToString(), "", 0, out err);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("数据保存成功", "提示");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            this.Close();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
