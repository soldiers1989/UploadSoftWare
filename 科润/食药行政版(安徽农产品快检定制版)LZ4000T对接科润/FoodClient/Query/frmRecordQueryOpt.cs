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
	/// frmRecordQueryOpt 的摘要说明。
	/// </summary>
	public class frmRecordQueryOpt : System.Windows.Forms.Form
	{
		
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DateTimePicker dtpCheckEndDate;
		private System.Windows.Forms.DateTimePicker dtpCheckStartDate;
		private C1.Win.C1List.C1Combo cmbFood;
		private C1.Win.C1List.C1Combo cmbCheckedCompany;
		private C1.Win.C1List.C1Combo cmbChecker;
		private C1.Win.C1List.C1Combo cmbIsSend;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStdCode;
		private System.Windows.Forms.Label label2;
		public C1.Win.C1List.C1Combo cmbProvider;
		public System.Windows.Forms.Label label7;
		public C1.Win.C1List.C1Combo cmbProduceCompany;
		public System.Windows.Forms.Label label17;
		public System.Windows.Forms.TextBox txtPurchaser;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label8;
		private C1.Win.C1List.C1Combo cmbDisplayCompany;
		private System.Windows.Forms.TextBox txtModel;

		public string QueryStr;
		private string sFoodSelectedValue="";
		private string sCheckedComSelectedValue="";
		private string sDisplayComSelectedValue="";
		private string sProviderComSelectedValue="";
		private string sProduceComSelectedValue="";

		public frmRecordQueryOpt()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			QueryStr="";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRecordQueryOpt));
			this.btnQuery = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dtpCheckEndDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.dtpCheckStartDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cmbFood = new C1.Win.C1List.C1Combo();
			this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
			this.cmbChecker = new C1.Win.C1List.C1Combo();
			this.cmbIsSend = new C1.Win.C1List.C1Combo();
			this.txtStdCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.cmbProvider = new C1.Win.C1List.C1Combo();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbProduceCompany = new C1.Win.C1List.C1Combo();
			this.label17 = new System.Windows.Forms.Label();
			this.txtPurchaser = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbDisplayCompany = new C1.Win.C1List.C1Combo();
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbDisplayCompany)).BeginInit();
			this.SuspendLayout();
			// 
			// btnQuery
			// 
			this.btnQuery.Location = new System.Drawing.Point(360, 200);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(72, 24);
			this.btnQuery.TabIndex = 11;
			this.btnQuery.Text = "查询(&Q)";
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(304, 165);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 21);
			this.label13.TabIndex = 23;
			this.label13.Text = "是否发送：";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(56, 165);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 21);
			this.label12.TabIndex = 21;
			this.label12.Text = "经办人：";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(272, 10);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104, 16);
			this.label9.TabIndex = 20;
			this.label9.Text = "档口/店面编号：";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(304, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 21);
			this.label6.TabIndex = 14;
			this.label6.Text = "商品名称：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpCheckEndDate
			// 
			this.dtpCheckEndDate.CustomFormat = "yyyy年MM月dd日";
			this.dtpCheckEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCheckEndDate.Location = new System.Drawing.Point(376, 71);
			this.dtpCheckEndDate.Name = "dtpCheckEndDate";
			this.dtpCheckEndDate.Size = new System.Drawing.Size(150, 21);
			this.dtpCheckEndDate.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(304, 71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 21);
			this.label5.TabIndex = 19;
			this.label5.Text = "结束日期：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpCheckStartDate
			// 
			this.dtpCheckStartDate.CustomFormat = "yyyy年MM月dd日";
			this.dtpCheckStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCheckStartDate.Location = new System.Drawing.Point(128, 71);
			this.dtpCheckStartDate.Name = "dtpCheckStartDate";
			this.dtpCheckStartDate.Size = new System.Drawing.Size(150, 21);
			this.dtpCheckStartDate.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(17, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 21);
			this.label4.TabIndex = 13;
			this.label4.Text = "开始日期：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(448, 200);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
			this.cmbFood.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.cmbFood.ItemHeight = 15;
			this.cmbFood.Location = new System.Drawing.Point(376, 39);
			this.cmbFood.MatchEntryTimeout = ((long)(2000));
			this.cmbFood.MaxDropDownItems = ((short)(5));
			this.cmbFood.MaxLength = 32767;
			this.cmbFood.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbFood.Name = "cmbFood";
			this.cmbFood.PartialRightColumn = false;
			this.cmbFood.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbFood.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbFood.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbFood.Size = new System.Drawing.Size(150, 22);
			this.cmbFood.TabIndex = 2;
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
			this.cmbCheckedCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.cmbCheckedCompany.ItemHeight = 15;
			this.cmbCheckedCompany.Location = new System.Drawing.Point(128, 39);
			this.cmbCheckedCompany.MatchEntryTimeout = ((long)(2000));
			this.cmbCheckedCompany.MaxDropDownItems = ((short)(5));
			this.cmbCheckedCompany.MaxLength = 32767;
			this.cmbCheckedCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbCheckedCompany.Name = "cmbCheckedCompany";
			this.cmbCheckedCompany.PartialRightColumn = false;
			this.cmbCheckedCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbCheckedCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbCheckedCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbCheckedCompany.Size = new System.Drawing.Size(150, 22);
			this.cmbCheckedCompany.TabIndex = 3;
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
			// cmbChecker
			// 
			this.cmbChecker.AddItemSeparator = ';';
			this.cmbChecker.Caption = "";
			this.cmbChecker.CaptionHeight = 17;
			this.cmbChecker.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbChecker.ColumnCaptionHeight = 18;
			this.cmbChecker.ColumnFooterHeight = 18;
			this.cmbChecker.ContentHeight = 16;
			this.cmbChecker.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbChecker.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbChecker.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbChecker.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbChecker.EditorHeight = 16;
			this.cmbChecker.GapHeight = 2;
			this.cmbChecker.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.cmbChecker.ItemHeight = 15;
			this.cmbChecker.Location = new System.Drawing.Point(128, 164);
			this.cmbChecker.MatchEntryTimeout = ((long)(2000));
			this.cmbChecker.MaxDropDownItems = ((short)(5));
			this.cmbChecker.MaxLength = 32767;
			this.cmbChecker.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbChecker.Name = "cmbChecker";
			this.cmbChecker.PartialRightColumn = false;
			this.cmbChecker.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbChecker.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbChecker.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbChecker.Size = new System.Drawing.Size(150, 22);
			this.cmbChecker.TabIndex = 5;
			this.cmbChecker.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// cmbIsSend
			// 
			this.cmbIsSend.AddItemSeparator = ';';
			this.cmbIsSend.Caption = "";
			this.cmbIsSend.CaptionHeight = 17;
			this.cmbIsSend.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbIsSend.ColumnCaptionHeight = 18;
			this.cmbIsSend.ColumnFooterHeight = 18;
			this.cmbIsSend.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbIsSend.ContentHeight = 16;
			this.cmbIsSend.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbIsSend.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbIsSend.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbIsSend.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbIsSend.EditorHeight = 16;
			this.cmbIsSend.GapHeight = 2;
			this.cmbIsSend.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.cmbIsSend.ItemHeight = 15;
			this.cmbIsSend.Location = new System.Drawing.Point(376, 164);
			this.cmbIsSend.MatchEntryTimeout = ((long)(2000));
			this.cmbIsSend.MaxDropDownItems = ((short)(5));
			this.cmbIsSend.MaxLength = 32767;
			this.cmbIsSend.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbIsSend.Name = "cmbIsSend";
			this.cmbIsSend.PartialRightColumn = false;
			this.cmbIsSend.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbIsSend.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbIsSend.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbIsSend.Size = new System.Drawing.Size(150, 22);
			this.cmbIsSend.TabIndex = 9;
			this.cmbIsSend.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// txtStdCode
			// 
			this.txtStdCode.Location = new System.Drawing.Point(128, 8);
			this.txtStdCode.Name = "txtStdCode";
			this.txtStdCode.Size = new System.Drawing.Size(150, 21);
			this.txtStdCode.TabIndex = 24;
			this.txtStdCode.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 21);
			this.label1.TabIndex = 25;
			this.label1.Text = "记录编码：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 21);
			this.label2.TabIndex = 27;
			this.label2.Text = "规格/型号/品种：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(128, 102);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(150, 21);
			this.txtModel.TabIndex = 26;
			this.txtModel.Text = "";
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
			this.cmbProvider.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.cmbProvider.ItemHeight = 15;
			this.cmbProvider.Location = new System.Drawing.Point(376, 132);
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
			this.cmbProvider.TabIndex = 29;
			this.cmbProvider.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProvider_BeforeOpen);
			this.cmbProvider.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(272, 135);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 17);
			this.label7.TabIndex = 31;
			this.label7.Text = "供货单位/个人：";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.cmbProduceCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
			this.cmbProduceCompany.ItemHeight = 15;
			this.cmbProduceCompany.Location = new System.Drawing.Point(128, 132);
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
			this.cmbProduceCompany.TabIndex = 28;
			this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
			this.cmbProduceCompany.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(-8, 135);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(136, 17);
			this.label17.TabIndex = 30;
			this.label17.Text = "生产/加工单位/个人：";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPurchaser
			// 
			this.txtPurchaser.Location = new System.Drawing.Point(128, 133);
			this.txtPurchaser.Name = "txtPurchaser";
			this.txtPurchaser.Size = new System.Drawing.Size(400, 21);
			this.txtPurchaser.TabIndex = 40;
			this.txtPurchaser.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 135);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 17);
			this.label3.TabIndex = 39;
			this.label3.Text = "购货单位/个人：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(56, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 21);
			this.label8.TabIndex = 42;
			this.label8.Text = "商户名称：";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbDisplayCompany
			// 
			this.cmbDisplayCompany.AddItemSeparator = ';';
			this.cmbDisplayCompany.Caption = "";
			this.cmbDisplayCompany.CaptionHeight = 17;
			this.cmbDisplayCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbDisplayCompany.ColumnCaptionHeight = 18;
			this.cmbDisplayCompany.ColumnFooterHeight = 18;
			this.cmbDisplayCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbDisplayCompany.ContentHeight = 16;
			this.cmbDisplayCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbDisplayCompany.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbDisplayCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbDisplayCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbDisplayCompany.EditorHeight = 16;
			this.cmbDisplayCompany.GapHeight = 2;
			this.cmbDisplayCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
			this.cmbDisplayCompany.ItemHeight = 15;
			this.cmbDisplayCompany.Location = new System.Drawing.Point(376, 8);
			this.cmbDisplayCompany.MatchEntryTimeout = ((long)(2000));
			this.cmbDisplayCompany.MaxDropDownItems = ((short)(5));
			this.cmbDisplayCompany.MaxLength = 32767;
			this.cmbDisplayCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbDisplayCompany.Name = "cmbDisplayCompany";
			this.cmbDisplayCompany.PartialRightColumn = false;
			this.cmbDisplayCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbDisplayCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbDisplayCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbDisplayCompany.Size = new System.Drawing.Size(150, 22);
			this.cmbDisplayCompany.TabIndex = 41;
			this.cmbDisplayCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbDisplayCompany_BeforeOpen);
			this.cmbDisplayCompany.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// frmRecordQueryOpt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(538, 231);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmbProduceCompany);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtStdCode);
			this.Controls.Add(this.txtPurchaser);
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.dtpCheckStartDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cmbCheckedCompany);
			this.Controls.Add(this.cmbChecker);
			this.Controls.Add(this.cmbProvider);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cmbFood);
			this.Controls.Add(this.dtpCheckEndDate);
			this.Controls.Add(this.cmbIsSend);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.cmbDisplayCompany);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmRecordQueryOpt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "台帐记录查询";
			this.Load += new System.EventHandler(this.frmRecordQueryOpt_Load);
			((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbDisplayCompany)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("Stock"))
			{
				QueryStr+="InputDate>=#" + this.dtpCheckStartDate.Value.Date.ToString() + " #";
				QueryStr+=" and InputDate<=#" +  this.dtpCheckEndDate.Value.Year.ToString() + "-" + this.dtpCheckEndDate.Value.Month.ToString() + "-" + this.dtpCheckEndDate.Value.Day.ToString() + " 23:59:59#";
				if(!this.sProviderComSelectedValue.Equals(""))
				{
					QueryStr+=" and PrivoderID='" + this.cmbProvider.SelectedValue.ToString() + "'";
				}
				if(!this.sProduceComSelectedValue.Equals(""))
				{
					QueryStr+=" and ProduceCompanyID='" + this.cmbProduceCompany.SelectedValue.ToString() + "'";
				}
			}
			else
			{
				QueryStr+="SaleDate>=#" + this.dtpCheckStartDate.Value.Date.ToString() + "#";
				QueryStr+=" and SaleDate<=#" + this.dtpCheckEndDate.Value.Year.ToString() + "-" + this.dtpCheckEndDate.Value.Month.ToString() + "-" + this.dtpCheckEndDate.Value.Day.ToString() + " 23:59:59#";
				if(!this.txtPurchaser.Text.Equals(""))
				{
					QueryStr+=" and PurchaserName Like '%" + this.txtPurchaser.Text.ToString() + "%'";
				}
			}
			if(!this.sFoodSelectedValue.Equals(""))
			{
				QueryStr+=" and FoodID='" + sFoodSelectedValue + "'";		
			}
			if(!this.sCheckedComSelectedValue.Equals(""))
			{
				QueryStr+=" and CompanyID='" + sCheckedComSelectedValue + "'";		
			}
			if(!this.sDisplayComSelectedValue.Equals(""))
			{
				QueryStr+=" and CompanyID='" + sDisplayComSelectedValue + "'";		
			}
			if(!this.cmbChecker.Text.Equals(""))
			{
				QueryStr+=" and MakeMan='" + this.cmbChecker.Text.ToString() + "'";
			}
			if(!this.cmbIsSend.Text.Trim().Equals(""))
			{
				if(this.cmbIsSend.Text.Trim().Equals("已上传"))
				{
					QueryStr+=" and IsSended=true";
				}
				else if(this.cmbIsSend.Text.Trim().Equals("未上传"))
				{
					QueryStr+=" and IsSended=false";
				}
			}
			if(!this.txtStdCode.Text.Trim().Equals(""))
			{
				QueryStr+=" and StdCode Like '%" + this.txtStdCode.Text.Trim() + "%'";
			}
			if(!this.txtModel.Text.Trim().Equals(""))
			{
				QueryStr+=" and Model Like '%" + this.txtModel.Text.Trim() + "%'";
			}
			

			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void frmRecordQueryOpt_Load(object sender, System.EventArgs e)
		{
			this.dtpCheckStartDate.Value=DateTime.Now.AddMonths(-1);
			this.dtpCheckEndDate.Value=DateTime.Now;

			if(!ShareOption.IsRunCache) CommonOperation.RunExeCache(10);
			
			this.cmbChecker.DataSource=ShareOption.DtblChecker.DataSet;
			this.cmbChecker.DataMember="UserInfo";
			this.cmbChecker.DisplayMember="Name";
			this.cmbChecker.ValueMember="UserCode";
			this.cmbChecker.Columns["Name"].Caption="经办人";
			this.cmbChecker.Columns["UserCode"].Caption="系统编号";	
	
			this.cmbChecker.ColumnWidth=this.cmbChecker.Width;
			this.cmbChecker.AllowColMove=false;
			this.cmbChecker.HScrollBar.Style=C1.Win.C1List.ScrollBarStyleEnum.None;
			this.cmbChecker.MatchEntry=C1.Win.C1List.MatchEntryEnum.Extended;

			this.cmbIsSend.DataMode=C1.Win.C1List.DataModeEnum.AddItem;
			this.cmbIsSend.AddItemCols=1;
			this.cmbIsSend.AddItemTitles("发送状态");
			this.cmbIsSend.AddItem("");
			this.cmbIsSend.AddItem(ShareOption.SendState1);
			this.cmbIsSend.AddItem(ShareOption.SendState0);

		}

		private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string sType="";
			sType="查询";
			
			frmFoodSelect frm=new frmFoodSelect(sType,sFoodSelectedValue);
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sFoodSelectedValue=frm.sNodeTag;
				this.cmbFood.Text=frm.sNodeName;
			}
			else
			{
				this.sFoodSelectedValue="";
				this.cmbFood.Text=frm.sNodeName;
			}
			e.Cancel=true;
		}

		private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
            frmCheckedComSelect comSelect = new frmCheckedComSelect("", sCheckedComSelectedValue);
            if (!frmMain.IsLoadCheckedComSel)
            {
                comSelect.Tag = "Checked";
            }
			else
			{
				comSelect.Tag="Checked";
				comSelect.SetFormValues("",sCheckedComSelectedValue);
			}
			comSelect.ShowDialog(this);
			if(comSelect.DialogResult==DialogResult.OK)
			{
				this.sCheckedComSelectedValue=comSelect.sNodeTag;
				this.cmbCheckedCompany.Text=comSelect.sNodeName;
				
			}
			else
			{
				this.sDisplayComSelectedValue="";
				this.cmbCheckedCompany.Text="";
			}
			comSelect.Hide();
			e.Cancel=true;				
		}

		private void cmbProvider_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedComSelect frm=new frmCheckedComSelect("",sCheckedComSelectedValue);
			frm.Tag="Provider";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProviderComSelectedValue=frm.sNodeTag;
				this.cmbProvider.Text=frm.sNodeName;
			}
			else
			{
				this.sProviderComSelectedValue="";
				this.cmbProvider.Text="";
			}
			e.Cancel=true;		
		}

		private void cmbDisplayCompany_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedDisplaySelect frm=new frmCheckedDisplaySelect("",sDisplayComSelectedValue);
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sDisplayComSelectedValue=frm.sNodeTag;
				this.cmbDisplayCompany.Text=frm.sNodeName;
			}
			else
			{
				this.sDisplayComSelectedValue="";
				this.cmbDisplayCompany.Text="";
			}
			e.Cancel=true;
		}


		private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmCheckedComSelect frm=new frmCheckedComSelect("",sCheckedComSelectedValue);
			frm.Tag="Produce";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProduceComSelectedValue=frm.sNodeTag;
				this.cmbProduceCompany.Text=frm.sNodeName;
			}
			else
			{
				this.sProduceComSelectedValue="";
				this.cmbProduceCompany.Text="";
			}
			e.Cancel=true;
		}
	}
}
