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
	/// frmStockRecordEdit 的摘要说明。
	/// </summary>
	public class frmStockRecordEdit : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtStdCode;
		private C1.Win.C1List.C1Combo cmbCheckedCompany;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.DateTimePicker dtpProduceDate;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private C1.Win.C1List.C1Combo cmbFood;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtLinkInfo;
		private System.Windows.Forms.TextBox txtExpirationDate;
		private System.Windows.Forms.TextBox txtCompanyID;
		private System.Windows.Forms.DateTimePicker dtpInputDate;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.TextBox txtInputNumber;
		private System.Windows.Forms.TextBox txtOutputNumber;
		private C1.Win.C1List.C1Combo cmbProduceCompany;
		private C1.Win.C1List.C1Combo cmbProvider;
		private C1.Win.C1List.C1Combo cmbMakeMan;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtCertificateType2;
		private System.Windows.Forms.TextBox txtCertificateType1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCertificateType3;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtCertificateType6;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtCertificateType4;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtCertificateType5;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtCertificateType9;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtCertificateType7;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox txtCertificateType8;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox txtUnit;
		private System.Windows.Forms.Label label25;

		private clsStockRecord curObject;
		private clsStockRecordOpr curObjectOpr;

		private string foodid;
		private string companyid;
		private string producecompanyid;
		private string providerid;
		private string strMakeMan;

		private string sFoodSelectedValue="";
		private string sCheckedComSelectedValue="";
		private string sProduceComSelectedValue="";
		private string sProviderSelectedValue="";


		public frmStockRecordEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmStockRecordEdit));
			this.txtSysID = new System.Windows.Forms.TextBox();
			this.txtStdCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLinkInfo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtExpirationDate = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.txtCompanyID = new System.Windows.Forms.TextBox();
			this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
			this.label21 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.dtpProduceDate = new System.Windows.Forms.DateTimePicker();
			this.label19 = new System.Windows.Forms.Label();
			this.dtpInputDate = new System.Windows.Forms.DateTimePicker();
			this.label18 = new System.Windows.Forms.Label();
			this.cmbFood = new C1.Win.C1List.C1Combo();
			this.label16 = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInputNumber = new System.Windows.Forms.TextBox();
			this.txtOutputNumber = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.cmbProduceCompany = new C1.Win.C1List.C1Combo();
			this.label17 = new System.Windows.Forms.Label();
			this.cmbProvider = new C1.Win.C1List.C1Combo();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCertificateType2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cmbMakeMan = new C1.Win.C1List.C1Combo();
			this.label11 = new System.Windows.Forms.Label();
			this.txtCertificateType1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCertificateType3 = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtCertificateType6 = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtCertificateType4 = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtCertificateType5 = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.txtCertificateType9 = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.txtCertificateType7 = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.txtCertificateType8 = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.txtUnit = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbMakeMan)).BeginInit();
			this.SuspendLayout();
			// 
			// txtSysID
			// 
			this.txtSysID.Enabled = false;
			this.txtSysID.Location = new System.Drawing.Point(88, 488);
			this.txtSysID.MaxLength = 50;
			this.txtSysID.Name = "txtSysID";
			this.txtSysID.Size = new System.Drawing.Size(40, 21);
			this.txtSysID.TabIndex = 53;
			this.txtSysID.Text = "";
			this.txtSysID.Visible = false;
			// 
			// txtStdCode
			// 
			this.txtStdCode.Location = new System.Drawing.Point(144, 6);
			this.txtStdCode.MaxLength = 50;
			this.txtStdCode.Name = "txtStdCode";
			this.txtStdCode.Size = new System.Drawing.Size(150, 21);
			this.txtStdCode.TabIndex = 0;
			this.txtStdCode.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(64, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 21);
			this.label2.TabIndex = 27;
			this.label2.Text = "记录编码：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLinkInfo
			// 
			this.txtLinkInfo.Location = new System.Drawing.Point(400, 153);
			this.txtLinkInfo.MaxLength = 50;
			this.txtLinkInfo.Name = "txtLinkInfo";
			this.txtLinkInfo.Size = new System.Drawing.Size(150, 21);
			this.txtLinkInfo.TabIndex = 12;
			this.txtLinkInfo.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(305, 153);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 21);
			this.label3.TabIndex = 50;
			this.label3.Text = "联系电话：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtExpirationDate
			// 
			this.txtExpirationDate.Location = new System.Drawing.Point(400, 128);
			this.txtExpirationDate.MaxLength = 10;
			this.txtExpirationDate.Name = "txtExpirationDate";
			this.txtExpirationDate.Size = new System.Drawing.Size(150, 21);
			this.txtExpirationDate.TabIndex = 10;
			this.txtExpirationDate.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(337, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 21);
			this.label4.TabIndex = 49;
			this.label4.Text = "保质期：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtRemark
			// 
			this.txtRemark.Location = new System.Drawing.Point(144, 416);
			this.txtRemark.Multiline = true;
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtRemark.Size = new System.Drawing.Size(408, 64);
			this.txtRemark.TabIndex = 24;
			this.txtRemark.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(80, 416);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 21);
			this.label6.TabIndex = 44;
			this.label6.Text = "备注：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(392, 488);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 25;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(480, 488);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 26;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(0, 488);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 52;
			this.label9.Text = "系统编码：";
			this.label9.Visible = false;
			// 
			// txtCompanyID
			// 
			this.txtCompanyID.Enabled = false;
			this.txtCompanyID.Location = new System.Drawing.Point(400, 30);
			this.txtCompanyID.Name = "txtCompanyID";
			this.txtCompanyID.Size = new System.Drawing.Size(150, 21);
			this.txtCompanyID.TabIndex = 2;
			this.txtCompanyID.Text = "";
			// 
			// cmbCheckedCompany
			// 
			this.cmbCheckedCompany.AddItemSeparator = ';';
			this.cmbCheckedCompany.Caption = "";
			this.cmbCheckedCompany.CaptionHeight = 17;
			this.cmbCheckedCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbCheckedCompany.ColumnCaptionHeight = 18;
			this.cmbCheckedCompany.ColumnFooterHeight = 18;
			this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbCheckedCompany.ContentHeight = 16;
			this.cmbCheckedCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbCheckedCompany.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbCheckedCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbCheckedCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbCheckedCompany.EditorHeight = 16;
			this.cmbCheckedCompany.GapHeight = 2;
			this.cmbCheckedCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.cmbCheckedCompany.ItemHeight = 15;
			this.cmbCheckedCompany.Location = new System.Drawing.Point(144, 29);
			this.cmbCheckedCompany.MatchEntryTimeout = ((long)(2000));
			this.cmbCheckedCompany.MaxDropDownItems = ((short)(5));
			this.cmbCheckedCompany.MaxLength = 10;
			this.cmbCheckedCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbCheckedCompany.Name = "cmbCheckedCompany";
			this.cmbCheckedCompany.PartialRightColumn = false;
			this.cmbCheckedCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbCheckedCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbCheckedCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbCheckedCompany.Size = new System.Drawing.Size(150, 22);
			this.cmbCheckedCompany.TabIndex = 1;
			this.cmbCheckedCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbCheckedCompany_BeforeOpen);
			this.cmbCheckedCompany.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{Alig" +
				"nImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(64, 32);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 17);
			this.label21.TabIndex = 28;
			this.label21.Text = "商户名称：";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(304, 32);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(97, 17);
			this.label26.TabIndex = 45;
			this.label26.Text = "档口/店面编号：";
			// 
			// dtpProduceDate
			// 
			this.dtpProduceDate.Location = new System.Drawing.Point(400, 104);
			this.dtpProduceDate.Name = "dtpProduceDate";
			this.dtpProduceDate.Size = new System.Drawing.Size(150, 21);
			this.dtpProduceDate.TabIndex = 8;
			this.dtpProduceDate.Value = new System.DateTime(2000, 1, 1, 18, 11, 0, 0);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(335, 106);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(66, 17);
			this.label19.TabIndex = 48;
			this.label19.Text = "生产日期：";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpInputDate
			// 
			this.dtpInputDate.Location = new System.Drawing.Point(144, 55);
			this.dtpInputDate.Name = "dtpInputDate";
			this.dtpInputDate.Size = new System.Drawing.Size(150, 21);
			this.dtpInputDate.TabIndex = 3;
			this.dtpInputDate.Value = new System.DateTime(2007, 2, 13, 0, 0, 0, 0);
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(70, 57);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(66, 17);
			this.label18.TabIndex = 29;
			this.label18.Text = "进货日期：";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbFood
			// 
			this.cmbFood.AddItemSeparator = ';';
			this.cmbFood.Caption = "";
			this.cmbFood.CaptionHeight = 17;
			this.cmbFood.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbFood.ColumnCaptionHeight = 18;
			this.cmbFood.ColumnFooterHeight = 18;
			this.cmbFood.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbFood.ContentHeight = 16;
			this.cmbFood.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbFood.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbFood.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbFood.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbFood.EditorHeight = 16;
			this.cmbFood.GapHeight = 2;
			this.cmbFood.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.cmbFood.ItemHeight = 15;
			this.cmbFood.Location = new System.Drawing.Point(400, 54);
			this.cmbFood.MatchEntryTimeout = ((long)(2000));
			this.cmbFood.MaxDropDownItems = ((short)(5));
			this.cmbFood.MaxLength = 50;
			this.cmbFood.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbFood.Name = "cmbFood";
			this.cmbFood.PartialRightColumn = false;
			this.cmbFood.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbFood.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbFood.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbFood.Size = new System.Drawing.Size(150, 22);
			this.cmbFood.TabIndex = 4;
			this.cmbFood.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbFood_BeforeOpen);
			this.cmbFood.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
				"rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
				"1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
				"yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(335, 57);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(66, 17);
			this.label16.TabIndex = 46;
			this.label16.Text = "商品名称：";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(400, 79);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(150, 21);
			this.txtModel.TabIndex = 6;
			this.txtModel.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(297, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 17);
			this.label1.TabIndex = 47;
			this.label1.Text = "规格/型号/品种：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtInputNumber
			// 
			this.txtInputNumber.Location = new System.Drawing.Point(144, 79);
			this.txtInputNumber.MaxLength = 50;
			this.txtInputNumber.Name = "txtInputNumber";
			this.txtInputNumber.Size = new System.Drawing.Size(150, 21);
			this.txtInputNumber.TabIndex = 5;
			this.txtInputNumber.Text = "";
			// 
			// txtOutputNumber
			// 
			this.txtOutputNumber.Location = new System.Drawing.Point(144, 104);
			this.txtOutputNumber.MaxLength = 20;
			this.txtOutputNumber.Name = "txtOutputNumber";
			this.txtOutputNumber.Size = new System.Drawing.Size(150, 21);
			this.txtOutputNumber.TabIndex = 7;
			this.txtOutputNumber.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(70, 106);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66, 17);
			this.label8.TabIndex = 31;
			this.label8.Text = "销售数量：";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(70, 81);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(66, 17);
			this.label13.TabIndex = 30;
			this.label13.Text = "进货数量：";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbProduceCompany
			// 
			this.cmbProduceCompany.AddItemSeparator = ';';
			this.cmbProduceCompany.Caption = "";
			this.cmbProduceCompany.CaptionHeight = 17;
			this.cmbProduceCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbProduceCompany.ColumnCaptionHeight = 18;
			this.cmbProduceCompany.ColumnFooterHeight = 18;
			this.cmbProduceCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbProduceCompany.ContentHeight = 16;
			this.cmbProduceCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbProduceCompany.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbProduceCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbProduceCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbProduceCompany.EditorHeight = 16;
			this.cmbProduceCompany.GapHeight = 2;
			this.cmbProduceCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.cmbProduceCompany.ItemHeight = 15;
			this.cmbProduceCompany.Location = new System.Drawing.Point(144, 152);
			this.cmbProduceCompany.MatchEntryTimeout = ((long)(2000));
			this.cmbProduceCompany.MaxDropDownItems = ((short)(5));
			this.cmbProduceCompany.MaxLength = 10;
			this.cmbProduceCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbProduceCompany.Name = "cmbProduceCompany";
			this.cmbProduceCompany.PartialRightColumn = false;
			this.cmbProduceCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbProduceCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbProduceCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbProduceCompany.Size = new System.Drawing.Size(150, 22);
			this.cmbProduceCompany.TabIndex = 11;
			this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
			this.cmbProduceCompany.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{Alig" +
				"nImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(0, 152);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(136, 17);
			this.label17.TabIndex = 33;
			this.label17.Text = "生产/加工单位/个人：";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbProvider
			// 
			this.cmbProvider.AddItemSeparator = ';';
			this.cmbProvider.Caption = "";
			this.cmbProvider.CaptionHeight = 17;
			this.cmbProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbProvider.ColumnCaptionHeight = 18;
			this.cmbProvider.ColumnFooterHeight = 18;
			this.cmbProvider.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbProvider.ContentHeight = 16;
			this.cmbProvider.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbProvider.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbProvider.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbProvider.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbProvider.EditorHeight = 16;
			this.cmbProvider.GapHeight = 2;
			this.cmbProvider.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.cmbProvider.ItemHeight = 15;
			this.cmbProvider.Location = new System.Drawing.Point(144, 176);
			this.cmbProvider.MatchEntryTimeout = ((long)(2000));
			this.cmbProvider.MaxDropDownItems = ((short)(5));
			this.cmbProvider.MaxLength = 10;
			this.cmbProvider.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbProvider.Name = "cmbProvider";
			this.cmbProvider.PartialRightColumn = false;
			this.cmbProvider.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbProvider.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbProvider.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbProvider.Size = new System.Drawing.Size(150, 22);
			this.cmbProvider.TabIndex = 13;
			this.cmbProvider.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProvider_BeforeOpen);
			this.cmbProvider.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
				"rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
				"1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
				"yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(32, 176);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 17);
			this.label7.TabIndex = 34;
			this.label7.Text = "供货单位/个人：";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType2
			// 
			this.txtCertificateType2.Location = new System.Drawing.Point(144, 224);
			this.txtCertificateType2.MaxLength = 50;
			this.txtCertificateType2.Name = "txtCertificateType2";
			this.txtCertificateType2.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType2.TabIndex = 16;
			this.txtCertificateType2.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(40, 224);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 21);
			this.label10.TabIndex = 36;
			this.label10.Text = "卫生许可证：";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbMakeMan
			// 
			this.cmbMakeMan.AddItemSeparator = ';';
			this.cmbMakeMan.Caption = "";
			this.cmbMakeMan.CaptionHeight = 17;
			this.cmbMakeMan.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbMakeMan.ColumnCaptionHeight = 18;
			this.cmbMakeMan.ColumnFooterHeight = 18;
			this.cmbMakeMan.ContentHeight = 16;
			this.cmbMakeMan.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbMakeMan.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbMakeMan.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbMakeMan.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbMakeMan.EditorHeight = 16;
			this.cmbMakeMan.GapHeight = 2;
			this.cmbMakeMan.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.cmbMakeMan.ItemHeight = 15;
			this.cmbMakeMan.Location = new System.Drawing.Point(400, 176);
			this.cmbMakeMan.MatchEntryTimeout = ((long)(2000));
			this.cmbMakeMan.MaxDropDownItems = ((short)(5));
			this.cmbMakeMan.MaxLength = 10;
			this.cmbMakeMan.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbMakeMan.Name = "cmbMakeMan";
			this.cmbMakeMan.PartialRightColumn = false;
			this.cmbMakeMan.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbMakeMan.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbMakeMan.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbMakeMan.Size = new System.Drawing.Size(150, 22);
			this.cmbMakeMan.TabIndex = 14;
			this.cmbMakeMan.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{Alig" +
				"nImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(335, 179);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66, 17);
			this.label11.TabIndex = 51;
			this.label11.Text = "经办人：";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType1
			// 
			this.txtCertificateType1.Location = new System.Drawing.Point(144, 200);
			this.txtCertificateType1.MaxLength = 50;
			this.txtCertificateType1.Name = "txtCertificateType1";
			this.txtCertificateType1.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType1.TabIndex = 15;
			this.txtCertificateType1.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(40, 200);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 21);
			this.label5.TabIndex = 35;
			this.label5.Text = "营业执照：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType3
			// 
			this.txtCertificateType3.Location = new System.Drawing.Point(144, 248);
			this.txtCertificateType3.MaxLength = 50;
			this.txtCertificateType3.Name = "txtCertificateType3";
			this.txtCertificateType3.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType3.TabIndex = 17;
			this.txtCertificateType3.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(40, 248);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(96, 21);
			this.label12.TabIndex = 37;
			this.label12.Text = "检验检疫证明：";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType6
			// 
			this.txtCertificateType6.Location = new System.Drawing.Point(144, 320);
			this.txtCertificateType6.MaxLength = 50;
			this.txtCertificateType6.Name = "txtCertificateType6";
			this.txtCertificateType6.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType6.TabIndex = 20;
			this.txtCertificateType6.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(40, 320);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(96, 21);
			this.label14.TabIndex = 40;
			this.label14.Text = "产地来源证明：";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType4
			// 
			this.txtCertificateType4.Location = new System.Drawing.Point(144, 272);
			this.txtCertificateType4.MaxLength = 50;
			this.txtCertificateType4.Name = "txtCertificateType4";
			this.txtCertificateType4.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType4.TabIndex = 18;
			this.txtCertificateType4.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(40, 272);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(96, 21);
			this.label15.TabIndex = 38;
			this.label15.Text = "产品合格证：";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType5
			// 
			this.txtCertificateType5.Location = new System.Drawing.Point(144, 296);
			this.txtCertificateType5.MaxLength = 50;
			this.txtCertificateType5.Name = "txtCertificateType5";
			this.txtCertificateType5.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType5.TabIndex = 19;
			this.txtCertificateType5.Text = "";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(40, 296);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 21);
			this.label20.TabIndex = 39;
			this.label20.Text = "税票或进货单：";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType9
			// 
			this.txtCertificateType9.Location = new System.Drawing.Point(144, 392);
			this.txtCertificateType9.MaxLength = 50;
			this.txtCertificateType9.Name = "txtCertificateType9";
			this.txtCertificateType9.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType9.TabIndex = 23;
			this.txtCertificateType9.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(40, 392);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(96, 21);
			this.label22.TabIndex = 43;
			this.label22.Text = "其他证票：";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType7
			// 
			this.txtCertificateType7.Location = new System.Drawing.Point(144, 344);
			this.txtCertificateType7.MaxLength = 50;
			this.txtCertificateType7.Name = "txtCertificateType7";
			this.txtCertificateType7.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType7.TabIndex = 21;
			this.txtCertificateType7.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(24, 344);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(112, 21);
			this.label23.TabIndex = 41;
			this.label23.Text = "农残检测合格证：";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCertificateType8
			// 
			this.txtCertificateType8.Location = new System.Drawing.Point(144, 368);
			this.txtCertificateType8.MaxLength = 50;
			this.txtCertificateType8.Name = "txtCertificateType8";
			this.txtCertificateType8.Size = new System.Drawing.Size(408, 21);
			this.txtCertificateType8.TabIndex = 22;
			this.txtCertificateType8.Text = "";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(32, 368);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(104, 21);
			this.label24.TabIndex = 42;
			this.label24.Text = "QS标志认证证书：";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtUnit
			// 
			this.txtUnit.Location = new System.Drawing.Point(144, 128);
			this.txtUnit.MaxLength = 20;
			this.txtUnit.Name = "txtUnit";
			this.txtUnit.Size = new System.Drawing.Size(150, 21);
			this.txtUnit.TabIndex = 9;
			this.txtUnit.Text = "";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(70, 128);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(66, 17);
			this.label25.TabIndex = 32;
			this.label25.Text = "数量单位：";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmStockRecordEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(562, 519);
			this.Controls.Add(this.txtUnit);
			this.Controls.Add(this.txtCertificateType9);
			this.Controls.Add(this.txtCertificateType7);
			this.Controls.Add(this.txtCertificateType8);
			this.Controls.Add(this.txtCertificateType6);
			this.Controls.Add(this.txtCertificateType4);
			this.Controls.Add(this.txtCertificateType5);
			this.Controls.Add(this.txtCertificateType3);
			this.Controls.Add(this.txtCertificateType1);
			this.Controls.Add(this.txtCertificateType2);
			this.Controls.Add(this.txtInputNumber);
			this.Controls.Add(this.txtOutputNumber);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtCompanyID);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.txtRemark);
			this.Controls.Add(this.txtExpirationDate);
			this.Controls.Add(this.txtLinkInfo);
			this.Controls.Add(this.txtStdCode);
			this.Controls.Add(this.txtSysID);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmbMakeMan);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.cmbProvider);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cmbProduceCompany);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbFood);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.dtpProduceDate);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.dtpInputDate);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.cmbCheckedCompany);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmStockRecordEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "进货台帐记录修改";
			this.Load += new System.EventHandler(this.frmStockRecordEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbMakeMan)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{	
			//检验输入的值是否合法
			//必须输入的是否已输入
			if(this.txtStdCode.Equals(""))
			{
				MessageBox.Show(this,"记录编码必须输入!");
				this.txtStdCode.Focus();
				return;
			}

			if(this.sCheckedComSelectedValue.Equals(""))
			{
				MessageBox.Show(this,"商户必须输入!");
				this.cmbCheckedCompany.Text="";
				this.cmbCheckedCompany.Focus();
				return;
			}

			if(this.sFoodSelectedValue.Equals(""))
			{
				MessageBox.Show(this,"商品必须输入!");
				this.cmbFood.Text="";
				this.cmbFood.Focus();
				return;
			}
			if(this.sProduceComSelectedValue.Equals(""))
			{
				MessageBox.Show(this,"生产/加工单位/个人必须输入!");
				this.cmbProduceCompany.Text="";
				this.cmbProduceCompany.Focus();
				return;
			}

			if(this.sProviderSelectedValue.Equals(""))
			{
				MessageBox.Show(this,"供货单位/个人必须输入!");
				this.cmbProvider.Text="";
				this.cmbProvider.Focus();
				return;
			}
			if(this.cmbMakeMan.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"经办人必须输入!");
				this.cmbMakeMan.Text="";
				this.cmbMakeMan.Focus();
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
			curObjectOpr=new clsStockRecordOpr();
			if(this.Tag.Equals("ADD"))
			{
				if(curObjectOpr.ExistSameValue(this.txtStdCode.Text.Trim()))
				{
					MessageBox.Show(this,"此记录编号已存在，请重新输入!");
					this.txtStdCode.Focus();
					return;
				}
			}
			if(!StringUtil.IsNumeric(this.txtInputNumber.Text.Trim()))
			{
				MessageBox.Show(this,"进货数量必须为数字!");
				this.txtInputNumber.Focus();
				return;
			}
			if(!StringUtil.IsNumeric(this.txtOutputNumber.Text.Trim()))
			{
				MessageBox.Show(this,"销售数量必须为数字!");
				this.txtOutputNumber.Focus();
				return;
			}

			//取值
			curObject=new clsStockRecord();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtStdCode.Text.Trim();
			curObject.CompanyID =this.sCheckedComSelectedValue;
			curObject.CompanyName =this.cmbCheckedCompany.Text.Trim();
			curObject.DisplayName = this.txtCompanyID.Text;
			curObject.InputDate = this.dtpInputDate.Value ;
			curObject.FoodID =this.sFoodSelectedValue;
			curObject.FoodName =this.cmbFood.Text.Trim();
			curObject.Model = this.txtModel.Text.Trim();
			if(this.txtInputNumber.Text.Trim().Equals(""))
			{
				curObject.InputNumber=0;
			}
			else
			{
				curObject.InputNumber = Convert.ToDecimal(this.txtInputNumber.Text.Trim());
			}
			if(this.txtOutputNumber.Text.Trim().Equals(""))
			{
				curObject.OutputNumber=0;
			}
			else
			{
				curObject.OutputNumber = Convert.ToDecimal(this.txtOutputNumber.Text.Trim());
			}
			curObject.Unit  =this.txtUnit.Text.Trim();
			curObject.ProduceDate = this.dtpProduceDate.Value;
			curObject.ExpirationDate = this.txtExpirationDate.Text.Trim();
			curObject.ProduceCompanyID =this.sProduceComSelectedValue;
			curObject.ProduceCompanyName =this.cmbProduceCompany.Text.ToString();
			curObject.PrivoderID =this.sProviderSelectedValue ;
			curObject.PrivoderName =this.cmbProvider.Text.ToString();
			curObject.LinkInfo =this.txtLinkInfo.Text.Trim();
			curObject.LinkMan = "";
			curObject.CertificateType1 =this.txtCertificateType1.Text.Trim();
			curObject.CertificateType2 =this.txtCertificateType2.Text.Trim();
			curObject.CertificateType3 =this.txtCertificateType3.Text.Trim();
			curObject.CertificateType4 =this.txtCertificateType4.Text.Trim();
			curObject.CertificateType5 =this.txtCertificateType5.Text.Trim();
			curObject.CertificateType6 =this.txtCertificateType6.Text.Trim();
			curObject.CertificateType7 =this.txtCertificateType7.Text.Trim();
			curObject.CertificateType8 =this.txtCertificateType8.Text.Trim();
			curObject.CertificateType9 =this.txtCertificateType9.Text.Trim();
			curObject.CertificateInfo ="";
			curObject.MakeMan =this.cmbMakeMan.Text.ToString();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.DistrictCode = clsUserUnitOpr.GetNameFromCode("DistrictCode",CurrentUser.GetInstance().UserInfo.UnitCode);
			curObject.IsSended= false;
			//对数据库进行操作
			string err="";
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out err);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,curObject.SysCode,out err);
			}
			if(!err.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}

			//退出
			this.DialogResult=DialogResult.OK;
			this.Close();
		}


		internal void setValue(clsStockRecord curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtStdCode.Text=curObject.StdCode;
			companyid=curObject.CompanyID;
			this.sCheckedComSelectedValue=companyid;
			this.txtCompanyID.Text=curObject.DisplayName;
			this.dtpInputDate.Value = curObject.InputDate;
			foodid = curObject.FoodID;
			this.sFoodSelectedValue=foodid;
			this.txtModel.Text = curObject.Model ;
			if(curObject.InputNumber.ToString().Equals("0"))
			{
				this.txtInputNumber.Text="";
			}
			else
			{
				this.txtInputNumber.Text = curObject.InputNumber.ToString();
			}
			if(curObject.OutputNumber.ToString().Equals("0"))
			{
				this.txtOutputNumber.Text = "";
			}
			else
			{
				this.txtOutputNumber.Text = curObject.OutputNumber.ToString();
			}
			this.txtUnit.Text=curObject.Unit;
			//this.dtpProduceDate.Value = curObject.ProduceDate;
			this.txtExpirationDate.Text = curObject.ExpirationDate;
			producecompanyid = curObject.ProduceCompanyID;
			providerid = curObject.PrivoderID;
			sProduceComSelectedValue=producecompanyid;
			sProviderSelectedValue=providerid;
			this.txtLinkInfo.Text = curObject.LinkInfo;
			this.txtCertificateType1.Text  = curObject.CertificateType1;
			this.txtCertificateType2.Text  = curObject.CertificateType2;
			this.txtCertificateType3.Text  = curObject.CertificateType3;
			this.txtCertificateType4.Text  = curObject.CertificateType4;
			this.txtCertificateType5.Text  = curObject.CertificateType5;
			this.txtCertificateType6.Text  = curObject.CertificateType6;
			this.txtCertificateType7.Text  = curObject.CertificateType7;
			this.txtCertificateType8.Text  = curObject.CertificateType8;
			this.txtCertificateType9.Text  = curObject.CertificateType9;
			strMakeMan = curObject.MakeMan;
			this.txtRemark.Text=curObject.Remark;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmStockRecordEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="进货台帐记录新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="进货台帐修改";
				this.btnOK.Text="保存";
			}

			if(!ShareOption.IsRunCache) CommonOperation.RunExeCache(10);

        	if(!foodid.Equals(""))
			{
				this.cmbFood.Text=clsFoodClassOpr.NameFromCode(foodid);
			}

			if(!companyid.Equals(""))
			{
				this.cmbCheckedCompany.Text=clsCompanyOpr.NameFromCode(companyid);
				this.txtCompanyID.Text =clsCompanyOpr.DisplayNameFromCode(companyid);
			}

			if(!producecompanyid.Equals(""))
			{
				this.cmbProduceCompany.Text=clsCompanyOpr.NameFromCode(producecompanyid);
			}

			if(!providerid.Equals(""))
			{
				this.cmbProvider.Text=clsCompanyOpr.NameFromCode(providerid);
			}		
			
			this.cmbMakeMan.DataSource=ShareOption.DtblChecker.DataSet;
			this.cmbMakeMan.DataMember="UserInfo";
			this.cmbMakeMan.DisplayMember="Name";
			this.cmbMakeMan.ValueMember="UserCode";
			this.cmbMakeMan.Columns["Name"].Caption="经办人";
			this.cmbMakeMan.Columns["UserCode"].Caption="系统编号";			
			
			this.cmbMakeMan.ColumnWidth=this.cmbMakeMan.Width;
			this.cmbMakeMan.AllowColMove=false;
			this.cmbMakeMan.HScrollBar.Style=C1.Win.C1List.ScrollBarStyleEnum.None;
			this.cmbMakeMan.MatchEntry=C1.Win.C1List.MatchEntryEnum.Extended;
			if(!strMakeMan.Equals(""))
			{
				bool IsOk=false;
				for(int i=0 ; i<cmbMakeMan.ListCount; i++)
				{
					this.cmbMakeMan.SelectedIndex=i;
					if(this.cmbMakeMan.SelectedText.Equals(strMakeMan))
					{
						IsOk=true;
						break;
					}
				}
				if(!IsOk)
				{
					this.cmbMakeMan.Text=strMakeMan;
				}	
			}	

               
		}

		private void cmbCheckedCompany_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedComSelect frm=new frmCheckedComSelect("",sCheckedComSelectedValue);
			frm.Tag="Checked";
			frm.Text = "选择商户";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sCheckedComSelectedValue=frm.sNodeTag;
				this.cmbCheckedCompany.Text=frm.sNodeName;
				this.txtCompanyID.Text =clsCompanyOpr.DisplayNameFromCode(this.sCheckedComSelectedValue);
			}
			e.Cancel=true;
		}

		private void cmbFood_BeforeOpen(object sender, CancelEventArgs e)
		{
			string sType="查询";
			frmFoodSelect frm=new frmFoodSelect(sType,sFoodSelectedValue);
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sFoodSelectedValue=frm.sNodeTag;
				this.cmbFood.Text=frm.sNodeName;
			}
			e.Cancel=true;
		}

		private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedComSelect frm=new frmCheckedComSelect("",sProduceComSelectedValue);
			frm.Tag="Produce";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProduceComSelectedValue=frm.sNodeTag;
				this.cmbProduceCompany.Text=frm.sNodeName;
			}
			e.Cancel=true;
		}


		private void cmbProvider_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedComSelect frm=new frmCheckedComSelect("",sProviderSelectedValue);
			frm.Tag="Provider";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProviderSelectedValue=frm.sNodeTag;
				this.cmbProvider.Text=frm.sNodeName;
			}
			e.Cancel=true;
		}
	}
}
