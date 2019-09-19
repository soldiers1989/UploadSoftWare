using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;

namespace FoodClient
{
	/// <summary>
	/// frmStandardEdit 的摘要说明。
	/// </summary>
	public class frmStandardEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtStdInfo;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsStandard curObject;
		private C1.Win.C1List.C1Combo cmbStdType;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label7;
		private clsStandardOpr curObjectOpr;

		public frmStandardEdit()
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
            C1.Win.C1List.Style style1 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStandardEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.label9 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtStdInfo = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStdType = new C1.Win.C1List.C1Combo();
            this.label39 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStdType)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 24);
            this.label9.TabIndex = 16;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(16, 224);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 15;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(72, 224);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 6;
            this.chkLock.Text = "停用";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(144, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtStdInfo
            // 
            this.txtStdInfo.Location = new System.Drawing.Point(120, 92);
            this.txtStdInfo.Name = "txtStdInfo";
            this.txtStdInfo.Size = new System.Drawing.Size(186, 21);
            this.txtStdInfo.TabIndex = 3;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(120, 151);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(186, 54);
            this.txtRemark.TabIndex = 5;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(120, 64);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(186, 21);
            this.txtKey.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 36);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(120, 8);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(186, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(96, 240);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(32, 21);
            this.txtSysID.TabIndex = 17;
            this.txtSysID.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "备注：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "检测标准信息：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "快捷编码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "检测标准名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "检测标准类型：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStdType
            // 
            this.cmbStdType.AddItemSeparator = ';';
            this.cmbStdType.Caption = "";
            this.cmbStdType.CaptionHeight = 17;
            this.cmbStdType.CaptionStyle = style1;
            this.cmbStdType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbStdType.ColumnCaptionHeight = 18;
            this.cmbStdType.ColumnFooterHeight = 18;
            this.cmbStdType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbStdType.ContentHeight = 16;
            this.cmbStdType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbStdType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbStdType.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbStdType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbStdType.EditorHeight = 16;
            this.cmbStdType.EvenRowStyle = style2;
            this.cmbStdType.FooterStyle = style3;
            this.cmbStdType.GapHeight = 2;
            this.cmbStdType.HeadingStyle = style4;
            this.cmbStdType.HighLightRowStyle = style5;
            this.cmbStdType.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbStdType.Images"))));
            this.cmbStdType.ItemHeight = 15;
            this.cmbStdType.Location = new System.Drawing.Point(120, 120);
            this.cmbStdType.MatchEntryTimeout = ((long)(2000));
            this.cmbStdType.MaxDropDownItems = ((short)(5));
            this.cmbStdType.MaxLength = 32767;
            this.cmbStdType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbStdType.Name = "cmbStdType";
            this.cmbStdType.OddRowStyle = style6;
            this.cmbStdType.PartialRightColumn = false;
            this.cmbStdType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbStdType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbStdType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbStdType.SelectedStyle = style7;
            this.cmbStdType.Size = new System.Drawing.Size(186, 22);
            this.cmbStdType.Style = style8;
            this.cmbStdType.TabIndex = 4;
            this.cmbStdType.PropBag = resources.GetString("cmbStdType.PropBag");
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(16, 119);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 48;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(16, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(8, 21);
            this.label7.TabIndex = 49;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmStandardEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(320, 255);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.cmbStdType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtStdInfo);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmStandardEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检测标准修改";
            this.Load += new System.EventHandler(this.frmStandardEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbStdType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//检验输入的值是否合法
			//必须输入的是否已输入
			if(this.txtName.Text.Equals(""))
			{
				MessageBox.Show(this,"检测标准名称必须输入!");
				this.txtName.Focus();
				return;
			}
			if(this.cmbStdType.Text.Equals(""))
			{
				MessageBox.Show(this,"检测标准的类型必须输入!");
				this.cmbStdType.Focus();
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
			curObject=new clsStandard();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtCode.Text.Trim();
			curObject.StdDes=this.txtName.Text.Trim();
			curObject.ShortCut=this.txtKey.Text.Trim();
			curObject.StdInfo=this.txtStdInfo.Text.Trim();
			curObject.StdType=this.cmbStdType.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//对数据库进行操作
			string sErr="";
			curObjectOpr=new clsStandardOpr();
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out sErr);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,out sErr);
			}
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}

			//退出
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void frmStandardEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="检测标准新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="检测标准修改";
				this.btnOK.Text="保存";
			}

			clsStandardTypeOpr unitOpr3=new clsStandardTypeOpr();
			DataTable dt3=unitOpr3.GetAsDataTable("IsLock=0","StdName",1);
			this.cmbStdType.DataSource=dt3.DataSet;
			this.cmbStdType.DataMember="StandardType";
			this.cmbStdType.Columns["StdName"].Caption="标准类型";
		}

		internal void setValue(clsStandard curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtCode.Text=curObject.StdCode;
			this.txtName.Text=curObject.StdDes;
			this.txtKey.Text=curObject.ShortCut;
			this.txtStdInfo.Text=curObject.StdInfo;
			this.cmbStdType.Text=curObject.StdType;
			this.txtRemark.Text=curObject.Remark;
			this.chkLock.Checked=curObject.IsLock;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
		}
	}
}
