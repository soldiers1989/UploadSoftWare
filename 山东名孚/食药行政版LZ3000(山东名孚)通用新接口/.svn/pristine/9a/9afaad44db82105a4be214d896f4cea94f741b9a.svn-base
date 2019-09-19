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
	/// frmItemEdit 的摘要说明。
	/// </summary>
	public class frmItemEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUnit;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtStdValue;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtOperateHelp;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtTestValue;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label13;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsCheckItem curObject;
		private C1.Win.C1List.C1Combo cmbCheckLevel;
		private C1.Win.C1List.C1Combo cmbStandard;
		private clsCheckItemOpr curObjectOpr;

		private string codeValue;
		private C1.Win.C1List.C1Combo cmbCheckType;
		private System.Windows.Forms.ComboBox cmbDemarcateInfo;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;

		private bool IsLoading=true;

		public frmItemEdit()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style17 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style18 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style19 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style20 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style21 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style22 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style23 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style24 = new C1.Win.C1List.Style();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStdValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOperateHelp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbCheckLevel = new C1.Win.C1List.C1Combo();
            this.cmbStandard = new C1.Win.C1List.C1Combo();
            this.cmbCheckType = new C1.Win.C1List.C1Combo();
            this.cmbDemarcateInfo = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStandard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckType)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(152, 392);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 27;
            this.label6.Text = "备注：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(120, 298);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(186, 55);
            this.txtRemark.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 20;
            this.label5.Text = "检测标准：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 19;
            this.label4.Text = "检测类型：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 47);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 18;
            this.label3.Text = "检测项目名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(120, 16);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(186, 21);
            this.txtCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(78, 422);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(40, 21);
            this.txtSysID.TabIndex = 15;
            this.txtSysID.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 26;
            this.label1.Text = "监控级别：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(120, 208);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(186, 21);
            this.txtUnit.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 21);
            this.label8.TabIndex = 22;
            this.label8.Text = "检测标准值单位：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStdValue
            // 
            this.txtStdValue.Location = new System.Drawing.Point(120, 176);
            this.txtStdValue.MaxLength = 50;
            this.txtStdValue.Name = "txtStdValue";
            this.txtStdValue.Size = new System.Drawing.Size(186, 21);
            this.txtStdValue.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 21);
            this.label7.TabIndex = 21;
            this.label7.Text = "检测标准值：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(25, 392);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 16;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(88, 392);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 11;
            this.chkLock.Text = "停用";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 426);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // txtOperateHelp
            // 
            this.txtOperateHelp.Location = new System.Drawing.Point(126, 449);
            this.txtOperateHelp.Name = "txtOperateHelp";
            this.txtOperateHelp.Size = new System.Drawing.Size(186, 21);
            this.txtOperateHelp.TabIndex = 8;
            this.txtOperateHelp.Visible = false;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(22, 450);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 21);
            this.label10.TabIndex = 25;
            this.label10.Text = "操作帮助：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // txtTestValue
            // 
            this.txtTestValue.Location = new System.Drawing.Point(120, 235);
            this.txtTestValue.Name = "txtTestValue";
            this.txtTestValue.Size = new System.Drawing.Size(186, 21);
            this.txtTestValue.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(16, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 21);
            this.label11.TabIndex = 24;
            this.label11.Text = "测试合格值：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 144);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 21);
            this.label13.TabIndex = 23;
            this.label13.Text = "检测标准值符号：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbCheckLevel.Location = new System.Drawing.Point(120, 266);
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
            this.cmbCheckLevel.TabIndex = 9;
            this.cmbCheckLevel.PropBag = resources.GetString("cmbCheckLevel.PropBag");
            // 
            // cmbStandard
            // 
            this.cmbStandard.AddItemSeparator = ';';
            this.cmbStandard.Caption = "";
            this.cmbStandard.CaptionHeight = 17;
            this.cmbStandard.CaptionStyle = style9;
            this.cmbStandard.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbStandard.ColumnCaptionHeight = 18;
            this.cmbStandard.ColumnFooterHeight = 18;
            this.cmbStandard.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbStandard.ContentHeight = 16;
            this.cmbStandard.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbStandard.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbStandard.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbStandard.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbStandard.EditorHeight = 16;
            this.cmbStandard.EvenRowStyle = style10;
            this.cmbStandard.FooterStyle = style11;
            this.cmbStandard.GapHeight = 2;
            this.cmbStandard.HeadingStyle = style12;
            this.cmbStandard.HighLightRowStyle = style13;
            this.cmbStandard.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbStandard.Images"))));
            this.cmbStandard.ItemHeight = 15;
            this.cmbStandard.Location = new System.Drawing.Point(120, 110);
            this.cmbStandard.MatchEntryTimeout = ((long)(2000));
            this.cmbStandard.MaxDropDownItems = ((short)(5));
            this.cmbStandard.MaxLength = 32767;
            this.cmbStandard.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbStandard.Name = "cmbStandard";
            this.cmbStandard.OddRowStyle = style14;
            this.cmbStandard.PartialRightColumn = false;
            this.cmbStandard.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbStandard.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbStandard.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbStandard.SelectedStyle = style15;
            this.cmbStandard.Size = new System.Drawing.Size(186, 22);
            this.cmbStandard.Style = style16;
            this.cmbStandard.TabIndex = 3;
            this.cmbStandard.TextChanged += new System.EventHandler(this.cmbStandard_TextChanged);
            this.cmbStandard.PropBag = resources.GetString("cmbStandard.PropBag");
            // 
            // cmbCheckType
            // 
            this.cmbCheckType.AddItemSeparator = ';';
            this.cmbCheckType.Caption = "";
            this.cmbCheckType.CaptionHeight = 17;
            this.cmbCheckType.CaptionStyle = style17;
            this.cmbCheckType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckType.ColumnCaptionHeight = 18;
            this.cmbCheckType.ColumnFooterHeight = 18;
            this.cmbCheckType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckType.ContentHeight = 16;
            this.cmbCheckType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckType.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckType.EditorHeight = 16;
            this.cmbCheckType.EvenRowStyle = style18;
            this.cmbCheckType.FooterStyle = style19;
            this.cmbCheckType.GapHeight = 2;
            this.cmbCheckType.HeadingStyle = style20;
            this.cmbCheckType.HighLightRowStyle = style21;
            this.cmbCheckType.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckType.Images"))));
            this.cmbCheckType.ItemHeight = 15;
            this.cmbCheckType.Location = new System.Drawing.Point(120, 79);
            this.cmbCheckType.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckType.MaxDropDownItems = ((short)(5));
            this.cmbCheckType.MaxLength = 32767;
            this.cmbCheckType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckType.Name = "cmbCheckType";
            this.cmbCheckType.OddRowStyle = style22;
            this.cmbCheckType.PartialRightColumn = false;
            this.cmbCheckType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckType.SelectedStyle = style23;
            this.cmbCheckType.Size = new System.Drawing.Size(186, 22);
            this.cmbCheckType.Style = style24;
            this.cmbCheckType.TabIndex = 2;
            this.cmbCheckType.PropBag = resources.GetString("cmbCheckType.PropBag");
            // 
            // cmbDemarcateInfo
            // 
            this.cmbDemarcateInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDemarcateInfo.Items.AddRange(new object[] {
            ">",
            "<",
            "≥",
            "≤"});
            this.cmbDemarcateInfo.Location = new System.Drawing.Point(120, 144);
            this.cmbDemarcateInfo.Name = "cmbDemarcateInfo";
            this.cmbDemarcateInfo.Size = new System.Drawing.Size(186, 20);
            this.cmbDemarcateInfo.TabIndex = 28;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(16, 48);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 49;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(30, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(8, 21);
            this.label12.TabIndex = 50;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(35, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(8, 21);
            this.label14.TabIndex = 51;
            this.label14.Text = "*";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(4, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(8, 21);
            this.label15.TabIndex = 52;
            this.label15.Text = "*";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmItemEdit
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(328, 421);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.cmbCheckType);
            this.Controls.Add(this.cmbCheckLevel);
            this.Controls.Add(this.txtOperateHelp);
            this.Controls.Add(this.txtTestValue);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtStdValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbStandard);
            this.Controls.Add(this.cmbDemarcateInfo);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmItemEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检测项目修改";
            this.Load += new System.EventHandler(this.frmItemEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStandard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckType)).EndInit();
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
			if(this.txtName.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"检测项目名称必须输入!");
				this.txtName.Focus();
				return;
			}
			if(this.cmbStandard.SelectedValue==null)
			{
				MessageBox.Show(this,"检测项目对应的标准必须输入!");
				this.cmbStandard.Text="";
				this.cmbStandard.Focus();
				return;
			}
			if(this.cmbDemarcateInfo.SelectedIndex<0)
			{
				MessageBox.Show(this,"不能不选择符号");
				this.cmbDemarcateInfo.Focus();
				return;
			}
			if(!(StringUtil.IsNumeric(this.txtStdValue.Text.Trim(),false)))
			{
				MessageBox.Show(this,"检测标准值必须输入数字!");
				this.txtStdValue.Focus();
				return;
			}
			if(Convert.ToDecimal(this.txtStdValue.Text.Trim())<0)
			{
				MessageBox.Show(this,"检测标准值必须输入正数!");
				this.txtStdValue.Focus();
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
			curObject=new clsCheckItem();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtCode.Text.Trim();
			curObject.ItemDes=this.txtName.Text.Trim();
			curObject.CheckType=this.cmbCheckType.Text.Trim();
			curObject.StandardCode=this.cmbStandard.SelectedValue.ToString();
			curObject.StandardValue=this.txtStdValue.Text.Trim();
			curObject.Unit=this.txtUnit.Text.Trim();
			curObject.DemarcateInfo=this.cmbDemarcateInfo.Text.Trim();
			curObject.TestValue=this.txtTestValue.Text.Trim();
			curObject.OperateHelp=this.txtOperateHelp.Text.Trim();
			curObject.CheckLevel=this.cmbCheckLevel.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//对数据库进行操作
			string sErr="";
			curObjectOpr=new clsCheckItemOpr();
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

		private void frmItemEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="检测项目新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="检测项目修改";
				this.btnOK.Text="保存";
			}

			clsCheckLevelOpr unitOpr4=new clsCheckLevelOpr();
			DataTable dt4=unitOpr4.GetAsDataTable("IsLock=0","CheckLevel");
			this.cmbCheckLevel.DataSource=dt4.DataSet;
			this.cmbCheckLevel.DataMember="CheckLevel";	
			this.cmbCheckLevel.Columns["CheckLevel"].Caption="监控级别";	

			clsCheckTypeOpr unitOpr3=new clsCheckTypeOpr();
			DataTable dt3=unitOpr3.GetAsDataTable("IsLock=0 And IsReadOnly=true","Name",1);
			if(dt3==null || dt3.Rows.Count==0)
			{
				MessageBox.Show("请先增加检测项目的类型！");
				this.btnOK.Enabled=false;
				return;
			}
			this.cmbCheckType.DataSource=dt3.DataSet;
			this.cmbCheckType.DataMember="CheckType";	
			this.cmbCheckType.Columns["Name"].Caption="检测类型";	


			clsStandardOpr unitOpr=new clsStandardOpr();
			DataTable dt=unitOpr.GetAsDataTable("IsLock=0 ","SysCode",1);
			this.cmbStandard.DataSource=dt.DataSet;
			this.cmbStandard.DataMember="Standard";
			this.cmbStandard.DisplayMember="StdDes";
			this.cmbStandard.ValueMember="SysCode";
			this.cmbStandard.Columns["StdCode"].Caption="编号";
			this.cmbStandard.Columns["StdDes"].Caption="标准";
			this.cmbStandard.Columns["SysCode"].Caption="系统编号";			
			
			this.cmbStandard.ColumnWidth=this.cmbStandard.Width;
			this.cmbStandard.AllowColMove=false;
			this.cmbStandard.HScrollBar.Style=C1.Win.C1List.ScrollBarStyleEnum.None;
			this.cmbStandard.MatchEntry=C1.Win.C1List.MatchEntryEnum.Extended;

			if(!codeValue.Equals(""))
			{
				this.cmbStandard.Text=clsStandardOpr.GetNameFromCode(codeValue);
				this.cmbStandard.SelectedValue=codeValue;
			}
		
			IsLoading=false;
		}

		internal void setValue(clsCheckItem curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtCode.Text=curObject.StdCode;
			this.txtName.Text=curObject.ItemDes;
			this.cmbCheckType.Text=curObject.CheckType;
			codeValue=curObject.StandardCode;
			this.txtStdValue.Text=curObject.StandardValue;
			this.txtUnit.Text=curObject.Unit;
			switch(curObject.DemarcateInfo)
			{
				case ">":
					this.cmbDemarcateInfo.SelectedIndex = 0;
					break;
				case "<":
					this.cmbDemarcateInfo.SelectedIndex = 1;
					break;
				case "≥":
					this.cmbDemarcateInfo.SelectedIndex = 2;
					break;
				case "≤":
					this.cmbDemarcateInfo.SelectedIndex = 3;
					break;
			}
			this.txtTestValue.Text=curObject.TestValue;
			this.txtOperateHelp.Text=curObject.OperateHelp;
			this.cmbCheckLevel.Text=curObject.CheckLevel;
			this.txtRemark.Text=curObject.Remark;
			this.chkLock.Checked=curObject.IsLock;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
		}

        private void cmbStandard_TextChanged(object sender, System.EventArgs e)
        {
            ComboTextChanged((C1.Win.C1List.C1Combo)sender, "StdDes", IsLoading);
        }
        private void ComboTextChanged(C1.Win.C1List.C1Combo combo, string fieldName, bool isLoading)
        {
            if (!isLoading && combo.FindString(combo.Text.Trim(), 0, fieldName) != -1)
            {
                combo.OpenCombo();
            }
            if (combo.Text.Trim().Equals(string.Empty))
            {
                combo.CloseCombo();
            }
        }
	}
}
