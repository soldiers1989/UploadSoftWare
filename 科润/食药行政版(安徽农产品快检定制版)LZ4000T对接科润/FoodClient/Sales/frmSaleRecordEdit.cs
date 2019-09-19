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
	/// frmSaleRecordEdit 的摘要说明。
	/// </summary>
	public class frmSaleRecordEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label label9;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.TextBox txtStdCode;
		private C1.Win.C1List.C1Combo cmbCheckedCompany;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label18;
		private C1.Win.C1List.C1Combo cmbFood;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtLinkInfo;
		private System.Windows.Forms.TextBox txtCompanyID;
		private System.Windows.Forms.TextBox txtModel;
		private C1.Win.C1List.C1Combo cmbMakeMan;
		private System.Windows.Forms.Label label11;

		private clsSaleRecord curObject;
		private clsSaleRecordOpr curObjectOpr;

		private string foodid;
		private string companyid;
		private string strMakeMan;

		private string sFoodSelectedValue="";
		private string sCheckedComSelectedValue="";

		private System.Windows.Forms.DateTimePicker dtpSaleDate;
		private System.Windows.Forms.TextBox txtSaleNumber;
		private System.Windows.Forms.TextBox txtPurchaser;
		private System.Windows.Forms.TextBox txtUnit;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPrice;

		public frmSaleRecordEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSaleRecordEdit));
			this.txtSysID = new System.Windows.Forms.TextBox();
			this.txtStdCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLinkInfo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.txtCompanyID = new System.Windows.Forms.TextBox();
			this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
			this.label21 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.dtpSaleDate = new System.Windows.Forms.DateTimePicker();
			this.label18 = new System.Windows.Forms.Label();
			this.cmbFood = new C1.Win.C1List.C1Combo();
			this.label16 = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSaleNumber = new System.Windows.Forms.TextBox();
			this.txtPrice = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbMakeMan = new C1.Win.C1List.C1Combo();
			this.label11 = new System.Windows.Forms.Label();
			this.txtPurchaser = new System.Windows.Forms.TextBox();
			this.txtUnit = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbMakeMan)).BeginInit();
			this.SuspendLayout();
			// 
			// txtSysID
			// 
			this.txtSysID.Enabled = false;
			this.txtSysID.Location = new System.Drawing.Point(168, 264);
			this.txtSysID.MaxLength = 50;
			this.txtSysID.Name = "txtSysID";
			this.txtSysID.Size = new System.Drawing.Size(40, 21);
			this.txtSysID.TabIndex = 37;
			this.txtSysID.Text = "";
			this.txtSysID.Visible = false;
			// 
			// txtStdCode
			// 
			this.txtStdCode.Location = new System.Drawing.Point(88, 8);
			this.txtStdCode.MaxLength = 50;
			this.txtStdCode.Name = "txtStdCode";
			this.txtStdCode.Size = new System.Drawing.Size(152, 21);
			this.txtStdCode.TabIndex = 0;
			this.txtStdCode.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 21);
			this.label2.TabIndex = 19;
			this.label2.Text = "记录编码：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLinkInfo
			// 
			this.txtLinkInfo.Location = new System.Drawing.Point(344, 160);
			this.txtLinkInfo.MaxLength = 50;
			this.txtLinkInfo.Name = "txtLinkInfo";
			this.txtLinkInfo.Size = new System.Drawing.Size(150, 21);
			this.txtLinkInfo.TabIndex = 12;
			this.txtLinkInfo.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(248, 160);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 21);
			this.label3.TabIndex = 35;
			this.label3.Text = "联系电话：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtRemark
			// 
			this.txtRemark.Location = new System.Drawing.Point(88, 192);
			this.txtRemark.Multiline = true;
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtRemark.Size = new System.Drawing.Size(408, 64);
			this.txtRemark.TabIndex = 16;
			this.txtRemark.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 21);
			this.label6.TabIndex = 28;
			this.label6.Text = "备注：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(336, 267);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(424, 267);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(80, 264);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 29;
			this.label9.Text = "系统编码：";
			this.label9.Visible = false;
			// 
			// txtCompanyID
			// 
			this.txtCompanyID.Enabled = false;
			this.txtCompanyID.Location = new System.Drawing.Point(344, 8);
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
			this.cmbCheckedCompany.Location = new System.Drawing.Point(88, 37);
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
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 40);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 17);
			this.label21.TabIndex = 20;
			this.label21.Text = "商户名称：";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(248, 10);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(97, 17);
			this.label26.TabIndex = 30;
			this.label26.Text = "档口/店面编号：";
			// 
			// dtpSaleDate
			// 
			this.dtpSaleDate.Location = new System.Drawing.Point(88, 64);
			this.dtpSaleDate.Name = "dtpSaleDate";
			this.dtpSaleDate.Size = new System.Drawing.Size(150, 21);
			this.dtpSaleDate.TabIndex = 3;
			this.dtpSaleDate.Value = new System.DateTime(2007, 5, 14, 0, 0, 0, 0);
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(16, 66);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(66, 17);
			this.label18.TabIndex = 21;
			this.label18.Text = "销货日期：";
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
			this.cmbFood.Location = new System.Drawing.Point(344, 37);
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
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(280, 40);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(66, 17);
			this.label16.TabIndex = 31;
			this.label16.Text = "商品名称：";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(344, 64);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(150, 21);
			this.txtModel.TabIndex = 6;
			this.txtModel.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(240, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 17);
			this.label1.TabIndex = 32;
			this.label1.Text = "规格/型号/品种：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtSaleNumber
			// 
			this.txtSaleNumber.Location = new System.Drawing.Point(88, 94);
			this.txtSaleNumber.MaxLength = 50;
			this.txtSaleNumber.Name = "txtSaleNumber";
			this.txtSaleNumber.Size = new System.Drawing.Size(150, 21);
			this.txtSaleNumber.TabIndex = 5;
			this.txtSaleNumber.Text = "";
			// 
			// txtPrice
			// 
			this.txtPrice.Location = new System.Drawing.Point(88, 160);
			this.txtPrice.MaxLength = 20;
			this.txtPrice.Name = "txtPrice";
			this.txtPrice.Size = new System.Drawing.Size(150, 21);
			this.txtPrice.TabIndex = 7;
			this.txtPrice.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(-8, 160);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 17);
			this.label8.TabIndex = 23;
			this.label8.Text = "销售单价(元)：";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 96);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(66, 17);
			this.label13.TabIndex = 22;
			this.label13.Text = "销货数量：";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(240, 96);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 17);
			this.label7.TabIndex = 25;
			this.label7.Text = "购货单位/个人：";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.cmbMakeMan.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.cmbMakeMan.ItemHeight = 15;
			this.cmbMakeMan.Location = new System.Drawing.Point(344, 124);
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
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(280, 127);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66, 17);
			this.label11.TabIndex = 36;
			this.label11.Text = "经办人：";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPurchaser
			// 
			this.txtPurchaser.Location = new System.Drawing.Point(344, 94);
			this.txtPurchaser.Name = "txtPurchaser";
			this.txtPurchaser.Size = new System.Drawing.Size(150, 21);
			this.txtPurchaser.TabIndex = 38;
			this.txtPurchaser.Text = "";
			// 
			// txtUnit
			// 
			this.txtUnit.Location = new System.Drawing.Point(88, 125);
			this.txtUnit.MaxLength = 50;
			this.txtUnit.Name = "txtUnit";
			this.txtUnit.Size = new System.Drawing.Size(150, 21);
			this.txtUnit.TabIndex = 39;
			this.txtUnit.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-8, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 21);
			this.label4.TabIndex = 40;
			this.label4.Text = "数量单位：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmSaleRecordEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(514, 303);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPurchaser);
			this.Controls.Add(this.txtSaleNumber);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtCompanyID);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.txtRemark);
			this.Controls.Add(this.txtLinkInfo);
			this.Controls.Add(this.txtStdCode);
			this.Controls.Add(this.txtSysID);
			this.Controls.Add(this.cmbMakeMan);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbFood);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.dtpSaleDate);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.cmbCheckedCompany);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtUnit);
			this.Controls.Add(this.txtPrice);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmSaleRecordEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "销售台帐记录修改";
			this.Load += new System.EventHandler(this.frmSaleRecordEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
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
			if(this.txtPurchaser.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"购货单位/个人必须输入!");
				this.txtPurchaser.Text="";
				this.txtPurchaser.Focus();
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
			curObjectOpr=new clsSaleRecordOpr();
			if(this.Tag.Equals("ADD"))
			{
				if(curObjectOpr.ExistSameValue(this.txtStdCode.Text.Trim()))
				{
					MessageBox.Show(this,"此记录编号已存在，请重新输入!");
					this.txtStdCode.Focus();
					return;
				}
			}
			if(!StringUtil.IsNumeric(this.txtSaleNumber.Text.Trim()))
			{
				MessageBox.Show(this,"销货数量必须为数字!");
				this.txtSaleNumber.Focus();
				return;
			}
			if(!StringUtil.IsNumeric(this.txtPrice.Text.Trim()))
			{
				MessageBox.Show(this,"销售单价必须为数字!");
				this.txtPrice.Focus();
				return;
			}

			//取值
			curObject=new clsSaleRecord();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtStdCode.Text.Trim();
			curObject.CompanyID =this.sCheckedComSelectedValue;
			curObject.CompanyName =this.cmbCheckedCompany.Text.Trim();
			curObject.DisplayName = this.txtCompanyID.Text;
			curObject.SaleDate = this.dtpSaleDate.Value ;
			curObject.FoodID =this.sFoodSelectedValue;
			curObject.FoodName =this.cmbFood.Text.Trim();
			curObject.Model = this.txtModel.Text.Trim();
			if(this.txtSaleNumber.Text.Trim().Equals(""))
			{
				curObject.SaleNumber=0;
			}
			else
			{
				curObject.SaleNumber = Convert.ToDecimal(this.txtSaleNumber.Text.Trim());
			}
			if(this.txtPrice.Text.Trim().Equals(""))
			{
				curObject.Price=0;
			}
			else
			{
				curObject.Price = Convert.ToDecimal(this.txtPrice.Text.Trim());
			}
			curObject.Unit = this.txtUnit.Text.ToString();
			curObject.PurchaserID ="";
			curObject.PurchaserName =this.txtPurchaser.Text.Trim();
			curObject.LinkInfo =this.txtLinkInfo.Text.Trim();
			curObject.LinkMan = "";
			curObject.MakeMan =this.cmbMakeMan.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsSended = false;
			curObject.DistrictCode = clsUserUnitOpr.GetNameFromCode("DistrictCode",CurrentUser.GetInstance().UserInfo.UnitCode);
			
			//对数据库进行操作
			string sErr="";
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out sErr);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,curObject.SysCode,out sErr);
			}
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}

			//退出
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void frmSaleRecordEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="销售台帐记录新增";
				this.btnOK.Text="新增";
			}
			else
			{
				this.Text="销售台帐修改";
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

		internal void setValue(clsSaleRecord curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtStdCode.Text=curObject.StdCode;
			companyid=curObject.CompanyID;
			this.sCheckedComSelectedValue=companyid;
			this.txtCompanyID.Text=curObject.DisplayName;
			this.dtpSaleDate.Value = curObject.SaleDate;
			foodid = curObject.FoodID;
			this.sFoodSelectedValue=foodid;
			this.txtModel.Text = curObject.Model ;
			if(curObject.SaleNumber.ToString().Equals("0"))
			{
				this.txtSaleNumber.Text ="";
			}
			else
			{
				this.txtSaleNumber.Text = curObject.SaleNumber.ToString();
			}
			if(curObject.Price.ToString().Equals("0"))
			{
				this.txtPrice.Text = "";
			}
			else
			{
				this.txtPrice.Text = curObject.Price.ToString();
			}
			this.txtUnit.Text=curObject.Unit.ToString();
			this.txtPurchaser.Text  = curObject.PurchaserName;
			this.txtLinkInfo.Text = curObject.LinkInfo;
			strMakeMan = curObject.MakeMan;
			this.txtRemark.Text=curObject.Remark;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
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
	}
}
