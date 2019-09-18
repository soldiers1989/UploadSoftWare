using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using DY.FoodClientLib;

namespace FoodClientTools
{
	/// <summary>
	/// frmCheckComTypeEdit 的摘要说明。
	/// </summary>
    public class FrmCheckComTypeEdit : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameCall;
        private System.Windows.Forms.TextBox txtAreaCall;
        //private C1.Win.C1List.C1Combo cmbComKind;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDomainTitle;
        private System.Windows.Forms.TextBox txtNameTitle;
        private System.Windows.Forms.TextBox txtAreaTitle;
        private System.Windows.Forms.TextBox txtSampleTitle;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        private clsCheckComType model;
        private TextBox txtProducerTag;
        private Label label1;
        private readonly clsCheckComTypeOpr comTypeBll = new clsCheckComTypeOpr();

        //private string svertype = "";
        //private string sname = "";
        //private string scomkind="";
        //private string tag = string.Empty;

        public FrmCheckComTypeEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckComTypeEdit));
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNameCall = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAreaCall = new System.Windows.Forms.TextBox();
            this.txtDomainTitle = new System.Windows.Forms.TextBox();
            this.txtNameTitle = new System.Windows.Forms.TextBox();
            this.txtAreaTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSampleTitle = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtProducerTag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(168, 265);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNameCall
            // 
            this.txtNameCall.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtNameCall.Location = new System.Drawing.Point(144, 24);
            this.txtNameCall.MaxLength = 50;
            this.txtNameCall.Name = "txtNameCall";
            this.txtNameCall.Size = new System.Drawing.Size(186, 21);
            this.txtNameCall.TabIndex = 1;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(106, 268);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 8;
            this.txtSysID.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "所属组织机构域标题：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "单位名称域标题：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAreaCall
            // 
            this.txtAreaCall.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtAreaCall.Location = new System.Drawing.Point(144, 56);
            this.txtAreaCall.MaxLength = 50;
            this.txtAreaCall.Name = "txtAreaCall";
            this.txtAreaCall.Size = new System.Drawing.Size(186, 21);
            this.txtAreaCall.TabIndex = 2;
            // 
            // txtDomainTitle
            // 
            this.txtDomainTitle.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtDomainTitle.Location = new System.Drawing.Point(144, 152);
            this.txtDomainTitle.MaxLength = 50;
            this.txtDomainTitle.Name = "txtDomainTitle";
            this.txtDomainTitle.Size = new System.Drawing.Size(186, 21);
            this.txtDomainTitle.TabIndex = 6;
            // 
            // txtNameTitle
            // 
            this.txtNameTitle.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtNameTitle.Location = new System.Drawing.Point(144, 120);
            this.txtNameTitle.MaxLength = 50;
            this.txtNameTitle.Name = "txtNameTitle";
            this.txtNameTitle.Size = new System.Drawing.Size(186, 21);
            this.txtNameTitle.TabIndex = 5;
            // 
            // txtAreaTitle
            // 
            this.txtAreaTitle.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtAreaTitle.Location = new System.Drawing.Point(144, 88);
            this.txtAreaTitle.MaxLength = 50;
            this.txtAreaTitle.Name = "txtAreaTitle";
            this.txtAreaTitle.Size = new System.Drawing.Size(186, 21);
            this.txtAreaTitle.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(34, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 21);
            this.label8.TabIndex = 71;
            this.label8.Text = "位置编号域标题：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 21);
            this.label10.TabIndex = 70;
            this.label10.Text = "受检人/单位域标题：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(34, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 21);
            this.label11.TabIndex = 69;
            this.label11.Text = "所属组织域标题：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleTitle
            // 
            this.txtSampleTitle.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtSampleTitle.Location = new System.Drawing.Point(144, 184);
            this.txtSampleTitle.MaxLength = 50;
            this.txtSampleTitle.Name = "txtSampleTitle";
            this.txtSampleTitle.Size = new System.Drawing.Size(186, 21);
            this.txtSampleTitle.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(34, 184);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 21);
            this.label14.TabIndex = 75;
            this.label14.Text = "样品名称域标题：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProducerTag
            // 
            this.txtProducerTag.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtProducerTag.Location = new System.Drawing.Point(144, 217);
            this.txtProducerTag.MaxLength = 50;
            this.txtProducerTag.Name = "txtProducerTag";
            this.txtProducerTag.Size = new System.Drawing.Size(186, 21);
            this.txtProducerTag.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(34, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 77;
            this.label1.Text = "生产单位域标题：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCheckComTypeEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(370, 317);
            this.Controls.Add(this.txtProducerTag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSampleTitle);
            this.Controls.Add(this.txtDomainTitle);
            this.Controls.Add(this.txtNameTitle);
            this.Controls.Add(this.txtAreaTitle);
            this.Controls.Add(this.txtAreaCall);
            this.Controls.Add(this.txtNameCall);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmCheckComTypeEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据域标题修改";
            this.Load += new System.EventHandler(this.FrmCheckComTypeEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private readonly clsSysOptOpr bll = new clsSysOptOpr();
        private void FrmCheckComTypeEdit_Load(object sender, System.EventArgs e)
        {
            DataTable dtbl = comTypeBll.GetAsDataTable(string.Empty, "ID", 3);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                txtSysID.Text = dtbl.Rows[0]["ID"].ToString();
                txtNameCall.Text = dtbl.Rows[0]["NameCall"].ToString();//单位名称域标题
                txtAreaCall.Text = dtbl.Rows[0]["AreaCall"].ToString();//所属组织机构域标题
                txtAreaTitle.Text = dtbl.Rows[0]["AreaTitle"].ToString();//所属组织域标题
                txtDomainTitle.Text = dtbl.Rows[0]["DomainTitle"].ToString();//位置编号域标题
                txtNameTitle.Text = dtbl.Rows[0]["NameTitle"].ToString();//受检人/单位域标题
                txtSampleTitle.Text = dtbl.Rows[0]["SampleTitle"].ToString();//样品名称域标题
            }
            bindData();
        }
        /// <summary>
        /// 绑定初始化数据
        /// 对应该数据表SysOpt 03打头的行
        /// </summary>
        private void bindData()
        {
            DataTable dtbl = bll.GetColumnDataTable(0, "Len(SysCode)=6 AND OptType='0501'", "OptValue");//SysCode LIKE '______'
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                txtProducerTag.Text = dtbl.Rows[0]["OptValue"].ToString();

            }
        }


        private void btnOK_Click(object sender, System.EventArgs e)
        {
            model = new clsCheckComType();
            model.ID = Convert.ToInt32(this.txtSysID.Text);
            model.NameCall = this.txtNameCall.Text.Trim();
            model.AreaCall = this.txtAreaCall.Text.Trim();
            model.AreaTitle = this.txtAreaTitle.Text.Trim();
            model.NameTitle = this.txtNameTitle.Text.Trim();
            model.DomainTitle = this.txtDomainTitle.Text.Trim();
            model.SampleTitle = this.txtSampleTitle.Text.Trim();

            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='050101'",txtProducerTag.Text);
            bll.UpdateCommand(sb.ToString());
            sb.Length = 0;

            int ret = comTypeBll.UpdatePartTag(model, out err);
            if (!err.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！");
                return;
            }
            if (ret > 0)
            {
                MessageBox.Show("操作成功！");
            }

            //退出
            this.DialogResult = DialogResult.OK;

            this.Close();
        }
    }
}
