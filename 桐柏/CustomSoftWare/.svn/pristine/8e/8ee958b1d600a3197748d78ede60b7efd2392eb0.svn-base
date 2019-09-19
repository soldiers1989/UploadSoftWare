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
	/// frmMakeAreaEdit 的摘要说明。
	/// </summary>
	public class frmMakeAreaEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtNO;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.CheckBox chkLocal;
		private C1.Win.C1List.C1Combo cmbCheckLevel;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsDistrict curObject;
		private clsDistrictOpr curObjectOpr;

		private string strCode="";
		private System.Windows.Forms.Label label8;
		private string strName="";

		public frmMakeAreaEdit()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMakeAreaEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.cmbCheckLevel = new C1.Win.C1List.C1Combo();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(224, 225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(136, 225);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNO
            // 
            this.txtNO.Location = new System.Drawing.Point(104, 88);
            this.txtNO.Name = "txtNO";
            this.txtNO.Size = new System.Drawing.Size(186, 21);
            this.txtNO.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 21);
            this.label7.TabIndex = 16;
            this.label7.Text = "顺序号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(-2, 227);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 24);
            this.label9.TabIndex = 11;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(240, 194);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 12;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(184, 194);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 7;
            this.chkLock.Text = "停用";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 21);
            this.label6.TabIndex = 18;
            this.label6.Text = "备注：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(104, 148);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(186, 44);
            this.txtRemark.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 21);
            this.label5.TabIndex = 17;
            this.label5.Text = "监控级别：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(104, 61);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(186, 21);
            this.txtKey.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 21);
            this.label4.TabIndex = 15;
            this.label4.Text = "快捷编码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 34);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "机构名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(104, 7);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(186, 21);
            this.txtCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(78, 227);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(40, 21);
            this.txtSysID.TabIndex = 10;
            this.txtSysID.Visible = false;
            // 
            // chkLocal
            // 
            this.chkLocal.Location = new System.Drawing.Point(104, 194);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(80, 24);
            this.chkLocal.TabIndex = 6;
            this.chkLocal.Text = "本地管辖";
            this.chkLocal.Visible = false;
            // 
            // cmbCheckLevel
            // 
            this.cmbCheckLevel.AddItemSeparator = ';';
            this.cmbCheckLevel.Caption = "";
            this.cmbCheckLevel.CaptionHeight = 17;
            this.cmbCheckLevel.CaptionStyle = style1;
            this.cmbCheckLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckLevel.ColumnCaptionHeight = 18;
            this.cmbCheckLevel.ColumnFooterHeight = 18;
            this.cmbCheckLevel.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckLevel.ContentHeight = 16;
            this.cmbCheckLevel.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckLevel.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckLevel.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckLevel.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckLevel.EditorHeight = 16;
            this.cmbCheckLevel.EvenRowStyle = style2;
            this.cmbCheckLevel.FooterStyle = style3;
            this.cmbCheckLevel.GapHeight = 2;
            this.cmbCheckLevel.HeadingStyle = style4;
            this.cmbCheckLevel.HighLightRowStyle = style5;
            this.cmbCheckLevel.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckLevel.Images"))));
            this.cmbCheckLevel.ItemHeight = 15;
            this.cmbCheckLevel.Location = new System.Drawing.Point(104, 115);
            this.cmbCheckLevel.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckLevel.MaxDropDownItems = ((short)(5));
            this.cmbCheckLevel.MaxLength = 32767;
            this.cmbCheckLevel.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckLevel.Name = "cmbCheckLevel";
            this.cmbCheckLevel.OddRowStyle = style6;
            this.cmbCheckLevel.PartialRightColumn = false;
            this.cmbCheckLevel.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckLevel.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.SelectedStyle = style7;
            this.cmbCheckLevel.Size = new System.Drawing.Size(186, 22);
            this.cmbCheckLevel.Style = style8;
            this.cmbCheckLevel.TabIndex = 4;
            this.cmbCheckLevel.PropBag = resources.GetString("cmbCheckLevel.PropBag");
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(32, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(8, 21);
            this.label8.TabIndex = 52;
            this.label8.Text = "*";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMakeAreaEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(312, 255);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCheckLevel);
            this.Controls.Add(this.chkLocal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.txtNO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMakeAreaEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组织机构修改";
            this.Load += new System.EventHandler(this.frmMakeAreaEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmMakeAreaEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="组织机构新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="组织机构修改";
				this.btnOK.Text="保存";
			}

			clsCheckLevelOpr unitOpr4=new clsCheckLevelOpr();
			DataTable dt4=unitOpr4.GetAsDataTable("IsLock=0","CheckLevel");
			this.cmbCheckLevel.DataSource=dt4.DataSet;
			this.cmbCheckLevel.DataMember="CheckLevel";	
			this.cmbCheckLevel.Columns["CheckLevel"].Caption="监控级别";	
		}

		internal void setValue(clsDistrict curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtCode.Text=curObject.StdCode;
			strCode=curObject.StdCode;
			this.txtName.Text=curObject.Name;
			strName=curObject.Name;
			this.txtKey.Text=curObject.ShortCut;
			this.txtNO.Text=curObject.DistrictIndex.ToString();
			this.cmbCheckLevel.Text=curObject.CheckLevel;
			this.txtRemark.Text=curObject.Remark;
			this.chkLocal.Checked=curObject.IsLocal;
			this.chkLock.Checked=curObject.IsLock;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//检验输入的值是否合法
			//必须输入的是否已输入
			if(this.txtName.Text.Equals(""))
			{
				MessageBox.Show(this,"机构名称必须输入!");
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
			if(!StringUtil.IsNumeric(this.txtNO.Text.Trim()))
			{
				MessageBox.Show(this,"顺序号必须为数字!");
				this.txtNO.Focus();
				return;
			}

			if(this.Tag.Equals("ADD"))
			{
				if(clsDistrictOpr.DistrictIsExist(" StdCode='" + this.txtCode.Text.Trim() +"'"))
				{
					MessageBox.Show(this,"编号重复，请重新输入新编号");
					this.txtCode.Focus();
					return;
				}
				if(clsDistrictOpr.DistrictIsExist(" Name='" + this.txtName.Text.Trim() +"'"))
				{
					MessageBox.Show(this,"机构名称重复，请重新输入新机构名称");
					this.txtName.Focus();
					return;
				}
			}
			else
			{
				if(strCode!=this.txtCode.Text.Trim() &&clsDistrictOpr.DistrictIsExist(" StdCode='" + this.txtCode.Text.Trim() +"'"))
				{
					MessageBox.Show(this,"编号重复，请重新输入新编号");
					this.txtCode.Focus();
					return;
				}
				if(strName!=this.txtName.Text.Trim() &&clsDistrictOpr.DistrictIsExist(" Name='" + this.txtName.Text.Trim() +"'"))
				{
					MessageBox.Show(this,"机构名称重复，请重新输入新机构名称");
					this.txtName.Focus();
					return;
				}
			}
			//取值
			curObject=new clsDistrict();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtCode.Text.Trim();
			curObject.Name=this.txtName.Text.Trim();
			curObject.ShortCut=this.txtKey.Text.Trim();
			curObject.DistrictIndex=Convert.ToInt64(this.txtNO.Text.Trim());
			curObject.CheckLevel=this.cmbCheckLevel.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLocal=this.chkLocal.Checked;
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//对数据库进行操作
			string err="";
			curObjectOpr=new clsDistrictOpr();
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out err);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,ShareOption.DistrictCodeLevel,out err);
			}
			if(!err.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}

			//退出
			this.DialogResult=DialogResult.OK;
			this.Close();
		}
	}
}
