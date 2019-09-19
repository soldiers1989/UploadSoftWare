using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmItemTypeEdit 的摘要说明。
	/// </summary>
	public class frmItemTypeEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsCheckType curObject;
		private System.Windows.Forms.TextBox txtOldName;
		private System.Windows.Forms.Label label7;
		private clsCheckTypeOpr curObjectOpr;

		public frmItemTypeEdit()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemTypeEdit));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOldName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(152, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(40, 112);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 6;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(96, 112);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 2;
            this.chkLock.Text = "停用";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "备注：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(120, 50);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(186, 54);
            this.txtRemark.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 18);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "检测类别名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOldName
            // 
            this.txtOldName.Enabled = false;
            this.txtOldName.Location = new System.Drawing.Point(16, 136);
            this.txtOldName.MaxLength = 50;
            this.txtOldName.Name = "txtOldName";
            this.txtOldName.Size = new System.Drawing.Size(48, 21);
            this.txtOldName.TabIndex = 5;
            this.txtOldName.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(16, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(8, 21);
            this.label7.TabIndex = 49;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmItemTypeEdit
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(328, 143);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtOldName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmItemTypeEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检测类别修改";
            this.Load += new System.EventHandler(this.frmItemTypeEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//检验输入的值是否合法
			//必须输入的是否已输入
			if(this.txtName.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"检测类别名称必须输入!");
				this.txtName.Focus();
				return;
			}

			if(this.txtName.Text.Trim()!=this.txtOldName.Text.Trim()&& clsCheckTypeOpr.ExistSameValue(this.txtName.Text.Trim()))
			{
				MessageBox.Show(this,"此检测类别名称已存在，请重新输入!");
				this.txtName.Focus();
				return;
			}
			//非法字符检查
			Control posControl;
			if(RegularCheck.HaveSpecChar(this,out posControl))
			{
				MessageBox.Show(this,"输入中有非法字符,请检查!");
				posControl.Focus();
				return;
			}

			//非字符型检查

			//取值
			curObject=new clsCheckType();
			curObject.Name=this.txtName.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//对数据库进行操作
			string sErr="";
			curObjectOpr=new clsCheckTypeOpr();
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out sErr);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,this.txtOldName.Text.Trim(),out sErr);
			}
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}

			//退出
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmItemTypeEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="检测项目类别新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="检测项目类别修改";
				this.btnOK.Text="保存";
			}
		}

		internal void setValue(clsCheckType curObject)
		{
			this.txtName.Text=curObject.Name;
			this.txtOldName.Text=curObject.Name;
			this.txtRemark.Text=curObject.Remark;
			this.chkLock.Checked=curObject.IsLock;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
		}
	}
}
