using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;
using System.Text.RegularExpressions;

namespace FoodClient
{
	/// <summary>
	/// frmFoodTypeEdit 的摘要说明。
	/// </summary>
	public class frmFoodTypeEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.Label label9;
		private C1.Win.C1List.C1Combo cmbCheckLevel;
		private System.Windows.Forms.GroupBox grpLine;
		private System.Windows.Forms.GroupBox grpSelected;
		private System.Windows.Forms.Button btnAllDel;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbSign;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox grpNoSelect;
		private System.Windows.Forms.TextBox txtUnit;
		private System.Windows.Forms.CheckBox chkDefault;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid2;
		private System.Windows.Forms.CheckBox chkAuto;
		private System.Windows.Forms.TextBox txtCheckItemCodes;
		private System.Windows.Forms.TextBox txtCheckItemValue;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsFoodClass curObject;
		private clsFoodClassOpr curObjectOpr;

		private  static DataTable dt1=new DataTable("dt1");
		private  static DataTable dt2=new DataTable("dt2");
		private System.Windows.Forms.Button btnSel;
		private System.Windows.Forms.Button btnAllSel;
		private System.Windows.Forms.Button btnEditCheckItem;
		private static bool IsCreatDT=false;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label7;
		private static bool IsRead=false;

		public frmFoodTypeEdit()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			if(IsCreatDT==false)
			{
				DataColumn myDataColumn;

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "SysCode";
				dt1.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "项目";
				dt1.Columns.Add(myDataColumn);
				
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Boolean");
				myDataColumn.ColumnName = "默认";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测标准值";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "Sign";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "Value";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "Unit";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "DefSign";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "DefValue";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "DefUnit";
				dt1.Columns.Add(myDataColumn);


				IsCreatDT=true;
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFoodTypeEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCheckLevel = new C1.Win.C1List.C1Combo();
            this.grpLine = new System.Windows.Forms.GroupBox();
            this.grpSelected = new System.Windows.Forms.GroupBox();
            this.btnEditCheckItem = new System.Windows.Forms.Button();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmbSign = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.btnAllDel = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSel = new System.Windows.Forms.Button();
            this.btnAllSel = new System.Windows.Forms.Button();
            this.grpNoSelect = new System.Windows.Forms.GroupBox();
            this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.txtCheckItemCodes = new System.Windows.Forms.TextBox();
            this.txtCheckItemValue = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).BeginInit();
            this.grpSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.grpNoSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(8, 104);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(40, 21);
            this.txtSysID.TabIndex = 9;
            this.txtSysID.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(72, 8);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(248, 21);
            this.txtCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(408, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(248, 21);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(336, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "种类名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(72, 33);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(248, 21);
            this.txtKey.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "快捷编码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(72, 62);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(520, 74);
            this.txtRemark.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(336, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "监控级别：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "备注：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(512, 143);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(600, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(600, 96);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 6;
            this.chkLock.Text = "停用";
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(600, 64);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 5;
            this.chkReadOnly.Text = "已审核";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
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
            this.cmbCheckLevel.Location = new System.Drawing.Point(408, 32);
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
            this.cmbCheckLevel.Size = new System.Drawing.Size(248, 22);
            this.cmbCheckLevel.Style = style8;
            this.cmbCheckLevel.TabIndex = 3;
            this.cmbCheckLevel.PropBag = resources.GetString("cmbCheckLevel.PropBag");
            // 
            // grpLine
            // 
            this.grpLine.Location = new System.Drawing.Point(8, 176);
            this.grpLine.Name = "grpLine";
            this.grpLine.Size = new System.Drawing.Size(664, 8);
            this.grpLine.TabIndex = 17;
            this.grpLine.TabStop = false;
            // 
            // grpSelected
            // 
            this.grpSelected.Controls.Add(this.btnEditCheckItem);
            this.grpSelected.Controls.Add(this.c1FlexGrid1);
            this.grpSelected.Controls.Add(this.txtUnit);
            this.grpSelected.Controls.Add(this.txtValue);
            this.grpSelected.Controls.Add(this.cmbSign);
            this.grpSelected.Controls.Add(this.label1);
            this.grpSelected.Controls.Add(this.label8);
            this.grpSelected.Controls.Add(this.chkDefault);
            this.grpSelected.Location = new System.Drawing.Point(8, 184);
            this.grpSelected.Name = "grpSelected";
            this.grpSelected.Size = new System.Drawing.Size(408, 286);
            this.grpSelected.TabIndex = 7;
            this.grpSelected.TabStop = false;
            this.grpSelected.Text = "已对应检测项目";
            // 
            // btnEditCheckItem
            // 
            this.btnEditCheckItem.Location = new System.Drawing.Point(320, 256);
            this.btnEditCheckItem.Name = "btnEditCheckItem";
            this.btnEditCheckItem.Size = new System.Drawing.Size(64, 24);
            this.btnEditCheckItem.TabIndex = 7;
            this.btnEditCheckItem.Text = "确定";
            this.btnEditCheckItem.Click += new System.EventHandler(this.btnEditCheckItem_Click);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid1.Location = new System.Drawing.Point(8, 14);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(392, 218);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 0;
            this.c1FlexGrid1.SelChange += new System.EventHandler(this.c1FlexGrid1_SelChange);
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(208, 258);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(48, 21);
            this.txtUnit.TabIndex = 5;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(152, 258);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(56, 21);
            this.txtValue.TabIndex = 4;
            // 
            // cmbSign
            // 
            this.cmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSign.Items.AddRange(new object[] {
            ">",
            "<",
            "≥",
            "≤"});
            this.cmbSign.Location = new System.Drawing.Point(96, 258);
            this.cmbSign.Name = "cmbSign";
            this.cmbSign.Size = new System.Drawing.Size(56, 20);
            this.cmbSign.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(56, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "检测值";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(248, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "为合格";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDefault
            // 
            this.chkDefault.Location = new System.Drawing.Point(8, 256);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(48, 24);
            this.chkDefault.TabIndex = 1;
            this.chkDefault.Text = "默认";
            this.chkDefault.CheckedChanged += new System.EventHandler(this.chkDefault_CheckedChanged);
            // 
            // btnAllDel
            // 
            this.btnAllDel.Location = new System.Drawing.Point(424, 272);
            this.btnAllDel.Name = "btnAllDel";
            this.btnAllDel.Size = new System.Drawing.Size(32, 24);
            this.btnAllDel.TabIndex = 9;
            this.btnAllDel.Text = ">>";
            this.btnAllDel.Click += new System.EventHandler(this.btnAllDel_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(424, 240);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(32, 24);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = ">";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSel
            // 
            this.btnSel.Location = new System.Drawing.Point(424, 344);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(32, 24);
            this.btnSel.TabIndex = 11;
            this.btnSel.Text = "<";
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // btnAllSel
            // 
            this.btnAllSel.Location = new System.Drawing.Point(424, 312);
            this.btnAllSel.Name = "btnAllSel";
            this.btnAllSel.Size = new System.Drawing.Size(32, 24);
            this.btnAllSel.TabIndex = 10;
            this.btnAllSel.Text = "<<";
            this.btnAllSel.Click += new System.EventHandler(this.btnAllSel_Click);
            // 
            // grpNoSelect
            // 
            this.grpNoSelect.Controls.Add(this.c1FlexGrid2);
            this.grpNoSelect.Location = new System.Drawing.Point(464, 184);
            this.grpNoSelect.Name = "grpNoSelect";
            this.grpNoSelect.Size = new System.Drawing.Size(208, 286);
            this.grpNoSelect.TabIndex = 12;
            this.grpNoSelect.TabStop = false;
            this.grpNoSelect.Text = "未对应检测项目";
            // 
            // c1FlexGrid2
            // 
            this.c1FlexGrid2.AllowEditing = false;
            this.c1FlexGrid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid2.ColumnInfo = "1,0,0,40,200,0,Columns:0{Width:127;Name:\"SysID\";Caption:\"检测项目\";TextAlignFixed:Cen" +
                "terCenter;}\t";
            this.c1FlexGrid2.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid2.Location = new System.Drawing.Point(8, 14);
            this.c1FlexGrid2.Name = "c1FlexGrid2";
            this.c1FlexGrid2.Rows.Count = 1;
            this.c1FlexGrid2.Rows.DefaultSize = 18;
            this.c1FlexGrid2.Rows.MaxSize = 200;
            this.c1FlexGrid2.Rows.MinSize = 20;
            this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid2.Size = new System.Drawing.Size(192, 266);
            this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
            this.c1FlexGrid2.TabIndex = 0;
            // 
            // chkAuto
            // 
            this.chkAuto.Checked = true;
            this.chkAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuto.Enabled = false;
            this.chkAuto.Location = new System.Drawing.Point(8, 143);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(248, 24);
            this.chkAuto.TabIndex = 13;
            this.chkAuto.Text = "系统自动将其下属" + ShareOption.SampleTitle + "种类进行同样设置";//食品
            // 
            // txtCheckItemCodes
            // 
            this.txtCheckItemCodes.Location = new System.Drawing.Point(312, 144);
            this.txtCheckItemCodes.Name = "txtCheckItemCodes";
            this.txtCheckItemCodes.Size = new System.Drawing.Size(168, 21);
            this.txtCheckItemCodes.TabIndex = 18;
            this.txtCheckItemCodes.Text = "textBox1";
            this.txtCheckItemCodes.Visible = false;
            // 
            // txtCheckItemValue
            // 
            this.txtCheckItemValue.Location = new System.Drawing.Point(280, 144);
            this.txtCheckItemValue.Name = "txtCheckItemValue";
            this.txtCheckItemValue.Size = new System.Drawing.Size(160, 21);
            this.txtCheckItemValue.TabIndex = 19;
            this.txtCheckItemValue.Text = "textBox2";
            this.txtCheckItemValue.Visible = false;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(21, 7);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 47;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(336, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(8, 21);
            this.label7.TabIndex = 48;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmFoodTypeEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(682, 479);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.txtCheckItemValue);
            this.Controls.Add(this.txtCheckItemCodes);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.grpNoSelect);
            this.Controls.Add(this.grpSelected);
            this.Controls.Add(this.btnSel);
            this.Controls.Add(this.btnAllSel);
            this.Controls.Add(this.btnAllDel);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.grpLine);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.cmbCheckLevel);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFoodTypeEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text =ShareOption.SampleTitle+ "种类修改";//食品
            this.Load += new System.EventHandler(this.frmFoodTypeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).EndInit();
            this.grpSelected.ResumeLayout(false);
            this.grpSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.grpNoSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{	
			//检验输入的值是否合法
			//必须输入的是否已输入
			if(this.txtCode.Text.Trim().Equals(""))
			{
                MessageBox.Show(this, ShareOption.SampleTitle + "种类编号必须输入!");//ShareOption.SampleTitle+
				this.txtCode.Focus();
				return;
			}

			if(this.txtName.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"种类名称必须输入!");//食品
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
			curObjectOpr=new clsFoodClassOpr();
			bool FoodIsExist=false;
			if(this.Tag.Equals("ADD"))
			{
				FoodIsExist=curObjectOpr.ExistSameValue(this.txtName.Text.Trim(),this.txtCode.Text.Trim(),"");
			}
			else
			{
				FoodIsExist=curObjectOpr.ExistSameValue(this.txtName.Text.Trim(),this.txtCode.Text.Trim(),this.txtSysID.Text.Trim());
			}
			if(FoodIsExist)
			{
				MessageBox.Show(this,"此种类名称或代码已存在，请重新输入!");//食品
				this.txtName.Focus();
				return;
			}


			//取值
			curObject=new clsFoodClass();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtCode.Text.Trim();
			curObject.CheckItemCodes=this.txtCheckItemCodes.Text;
			curObject.Name=this.txtName.Text.Trim();
			curObject.ShortCut=this.txtKey.Text.Trim();
			curObject.CheckLevel=this.cmbCheckLevel.Text.Trim();
			curObject.CheckItemValue =this.txtCheckItemValue.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//对数据库进行操作
			string sErr="";
//			curObjectOpr=new clsFoodClassOpr();
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
			if(this.Tag.Equals("ADD"))
			{
                this.Text = ShareOption.SampleTitle + "种类修改";//食品
				this.btnOK.Text="保存";
				this.btnCancel.Enabled=false;
				this.Tag="EDIT";
				this.Height=504;
				curObjectOpr=new clsFoodClassOpr();
				int max=curObjectOpr.GetMaxNO(this.txtCode.Text.ToString(),ShareOption.FoodCodeLevel,out sErr);
				if(max!=0)
				{
					this.chkAuto.Visible=true;
				}
				else
				{
					this.chkAuto.Visible=false;
				}
			}
			else
			{
				//退出
				this.DialogResult=DialogResult.OK;
				this.Close();
			}

		}

		private void frmFoodTypeEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
                this.Text = ShareOption.SampleTitle + "种类新增";//食品
				this.btnOK.Text="新增";
				this.chkAuto.Visible=false;
				this.Height=200;
			}
			else
			{
                this.Text = ShareOption.SampleTitle + "种类修改";//食品
				this.btnOK.Text="保存";
				this.Height=504;
				string sErr="";
				curObjectOpr=new clsFoodClassOpr();
				int max=curObjectOpr.GetMaxNO(this.txtCode.Text.ToString(),ShareOption.FoodCodeLevel,out sErr);
				if(max!=0)
				{
					this.chkAuto.Visible=true;
				}
				else
				{
					this.chkAuto.Visible=false;
				}
			}

			clsCheckLevelOpr unitOpr4=new clsCheckLevelOpr();
			DataTable dt4=unitOpr4.GetAsDataTable("IsLock=0","CheckLevel");
			this.cmbCheckLevel.DataSource=dt4.DataSet;
			this.cmbCheckLevel.DataMember="CheckLevel";	
			this.cmbCheckLevel.Columns["CheckLevel"].Caption="监控级别";	
		}

		internal void setValue(clsFoodClass curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtCode.Text=curObject.StdCode;
			this.txtName.Text=curObject.Name;
			this.txtKey.Text=curObject.ShortCut;
			this.cmbCheckLevel.Text=curObject.CheckLevel;
			this.txtRemark.Text=curObject.Remark;
			this.chkLock.Checked=curObject.IsLock;
			this.txtCheckItemCodes.Text=curObject.CheckItemCodes;
			this.txtCheckItemValue.Text=curObject.CheckItemValue;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
			this.ReadCheckItemCodes();
		}

        private void ReadCheckItemCodes()
        {
            dt1.Clear();
            dt2.Clear();
            string[,] strResult = GetAry(this.txtCheckItemCodes.Text);
            clsCheckItemOpr curCheckItemOpr = new clsCheckItemOpr();
            string strSysCodes = "";
            for (int i = 0; i <= strResult.GetLength(0) - 1; i++)
            {
                strSysCodes = strSysCodes + "'" + strResult[i, 0] + "',";
            }
            if (strSysCodes.Length > 0)
            {
                strSysCodes = strSysCodes.Substring(0, strSysCodes.Length - 1);
                DataTable dt = curCheckItemOpr.GetAsDataTable("SysCode In (" + strSysCodes + ")", "SysCode", 0);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= strResult.GetLength(0) - 1; j++)
                    {
                        if (dt.Rows[i]["SysCode"].ToString().Equals(strResult[j, 0]))
                        {
                            DataRow dr;
                            dr = dt1.NewRow();
                            dr["SysCode"] = strResult[j, 0];
                            dr["项目"] = dt.Rows[i]["ItemDes"];
                            if (strResult[j, 1].Equals("-1"))
                            {
                                dr["默认"] = true;
                                dr["检测标准值"] = dt.Rows[i]["DemarcateInfo"].ToString() + dt.Rows[i]["StandardValue"].ToString() + dt.Rows[i]["Unit"].ToString();
                                dr["Sign"] = dt.Rows[i]["DemarcateInfo"].ToString();
                                dr["Value"] = dt.Rows[i]["StandardValue"].ToString();
                                dr["Unit"] = dt.Rows[i]["Unit"].ToString();
                            }
                            else
                            {
                                dr["默认"] = false;
                                dr["检测标准值"] = strResult[j, 1] + strResult[j, 2] + strResult[j, 3];
                                dr["Sign"] = strResult[j, 1];
                                dr["Value"] = strResult[j, 2];
                                dr["Unit"] = strResult[j, 3];
                            }
                            dr["DefSign"] = dt.Rows[i]["DemarcateInfo"].ToString();
                            dr["DefValue"] = dt.Rows[i]["StandardValue"].ToString();
                            dr["DefUnit"] = dt.Rows[i]["Unit"].ToString(); ;
                            dt1.Rows.Add(dr);

                        }
                    }
                }
            }
            IsRead = true;
            this.c1FlexGrid1.DataSource = dt1;
            IsRead = false;
            this.c1FlexGrid1.Cols["SysCode"].Visible = false;
            this.c1FlexGrid1.Cols["Sign"].Visible = false;
            this.c1FlexGrid1.Cols["Value"].Visible = false;
            this.c1FlexGrid1.Cols["Unit"].Visible = false;
            this.c1FlexGrid1.Cols["DefSign"].Visible = false;
            this.c1FlexGrid1.Cols["DefValue"].Visible = false;
            this.c1FlexGrid1.Cols["DefUnit"].Visible = false;

            if (strSysCodes.Length > 0)
            {
                dt2 = curCheckItemOpr.GetAsDataTable("SysCode Not In (" + strSysCodes + ") And IsReadOnly=true", "SysCode", 0);
            }
            else
            {
                dt2 = curCheckItemOpr.GetAsDataTable("IsReadOnly=true", "SysCode", 0);
            }
            this.c1FlexGrid2.SetDataBinding(dt2.DataSet, "CheckItem");
            this.c1FlexGrid2.Cols["SysCode"].Visible = false;
            this.c1FlexGrid2.Cols["StdCode"].Visible = false;
            this.c1FlexGrid2.Cols["ItemDes"].Caption = "检测项目";
            this.c1FlexGrid2.Cols["CheckType"].Visible = false;
            this.c1FlexGrid2.Cols["StandardCode"].Visible = false;
            this.c1FlexGrid2.Cols["StandardValue"].Visible = false;
            this.c1FlexGrid2.Cols["Unit"].Visible = false;
            this.c1FlexGrid2.Cols["DemarcateInfo"].Visible = false;
            this.c1FlexGrid2.Cols["TestValue"].Visible = false;
            this.c1FlexGrid2.Cols["OperateHelp"].Visible = false;
            this.c1FlexGrid2.Cols["CheckLevel"].Visible = false;
            this.c1FlexGrid2.Cols["IsReadOnly"].Visible = false;
            this.c1FlexGrid2.Cols["IsLock"].Visible = false;
            this.c1FlexGrid2.Cols["Remark"].Visible = false;

            if (this.c1FlexGrid1.Rows.Count >= 2)
            {
                this.c1FlexGrid1.ColSel = 1;
                this.c1FlexGrid1.ColSel = 0;
            }
        }

        public static string[,] GetAry(string input)
        {
            System.Text.RegularExpressions.Regex r;
            System.Text.RegularExpressions.Match m;
            System.Collections.ArrayList ar1 = new System.Collections.ArrayList();
            System.Collections.ArrayList ar2 = new System.Collections.ArrayList();
            System.Collections.ArrayList ar3 = new System.Collections.ArrayList();
            System.Collections.ArrayList ar4 = new System.Collections.ArrayList();

            r = new System.Text.RegularExpressions.Regex(@"\{([ \S\t]*?):([ \S\t]*?):([ \S\t]*?):([ \S\t]*?)}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            for (m = r.Match(input); m.Success; m = m.NextMatch())
            {
                ar1.Add(m.Groups[1].ToString());
                ar2.Add(m.Groups[2].ToString());
                ar3.Add(m.Groups[3].ToString());
                ar4.Add(m.Groups[4].ToString());
            }
            string[,] strr = new string[ar1.Count, 4];
            for (int i = 0; i <= ar1.Count - 1; i++)
            {
                strr[i, 0] = ar1[i].ToString();
                strr[i, 1] = ar2[i].ToString();
                strr[i, 2] = ar3[i].ToString();
                strr[i, 3] = ar4[i].ToString();
            }
            return strr;
        }


        private void DelCheckItem(string strCode)
        {
            //检查本级食品是否存在该检测项目，有，不删除上级，没有删除上级
            int len = this.txtSysID.Text.Length / ShareOption.FoodCodeLevel - 1;
            string strPreCode = string.Empty;
            string sErr = string.Empty;
            for (int i = len; i >= 1; i--)
            {
                strPreCode = this.txtSysID.Text.Substring(0, i * ShareOption.FoodCodeLevel);
                if (!clsFoodClassOpr.ExistCheckItem(strPreCode, this.txtSysID.Text, strCode, out sErr))
                {
                    clsFoodClassOpr.DelCheckItem(strPreCode, strCode, out sErr);
                }
                else
                {
                    break;
                }
            }

            //自动将其本身和下级食品的该检测项目全部删除
            clsFoodClassOpr.DelCheckItem(this.txtSysID.Text, strCode, out sErr);
            curObjectOpr = new clsFoodClassOpr();
            DataTable dt = curObjectOpr.GetAsDataTable(" SysCode Like'" + this.txtSysID.Text + "_%' And CheckItemCodes Like '%{" + strCode + ":%}%'", "SysCode", 1);
            for (int i = 1; i <= dt.Rows.Count - 1; i++)
            {
                clsFoodClassOpr.DelCheckItem(dt.Rows[i]["SysCode"].ToString(), strCode, out sErr);
            }
        }

        private void AddCheckItem(string strCode)
        {
            //检查上级食品是否存在该检测项目，有，不增加，没有，增加
            int len = this.txtSysID.Text.Length / ShareOption.FoodCodeLevel - 1;
            string strPreCode = "";
            string sErr = "";
            for (int i = len; i >= 1; i--)
            {
                strPreCode = this.txtSysID.Text.Substring(0, i * ShareOption.FoodCodeLevel);
                curObjectOpr = new clsFoodClassOpr();
                string strCheckItemCodes = curObjectOpr.GetPreCheckItemCodes(strPreCode, out sErr);
                string pattern = @"({" + strCode + @":[\S\t]*?})";
                Regex r = new Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Match m = r.Match(strCheckItemCodes);
                if (m.Success)
                {
                    break;
                }
                else
                {
                    clsFoodClassOpr.AddCheckItem(strPreCode, strCode, out sErr);
                }

            }

            //自动将其本身和下级食品的该检测项目全部增加
            clsFoodClassOpr.AddAllCheckItem(this.txtSysID.Text, strCode, out sErr);
        }

		private string EditCheckItem(string strCode,string strSign,string strValue,string strUnit)
		{
			//只修改本身和下级食品中存在的该检测项目
			string strEditNew="{"+strCode+":"+strSign+":"+strValue+":"+strUnit+"}";
			string sErr="";
			string strResult=clsFoodClassOpr.EditCheckItem(this.txtSysID.Text,strCode,strEditNew,out sErr);
			curObjectOpr=new clsFoodClassOpr();
			DataTable dt=curObjectOpr.GetAsDataTable(" SysCode Like '"+this.txtSysID.Text+"_%'","SysCode",1);
			for(int i=0;i<=dt.Rows.Count-1;i++)
			{
				clsFoodClassOpr.EditCheckItem(dt.Rows[i]["SysCode"].ToString(),strCode,strEditNew,out sErr);
			}
			return strResult;

		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void c1FlexGrid1_SelChange(object sender, EventArgs e)
		{
			if(IsRead) return;
			int row=this.c1FlexGrid1.RowSel;
			if(row<=0) return;
			this.chkDefault.Checked=Convert.ToBoolean(this.c1FlexGrid1.Rows[row][2]);
			switch(this.c1FlexGrid1.Rows[row][4].ToString())
			{
				case ">":
					this.cmbSign.SelectedIndex = 0;
					break;
				case "<":
					this.cmbSign.SelectedIndex = 1;
					break;
				case "≥":
					this.cmbSign.SelectedIndex = 2;
					break;
				case "≤":
					this.cmbSign.SelectedIndex = 3;
					break;
			}
			this.txtValue.Text=this.c1FlexGrid1.Rows[row][5].ToString();
			this.txtUnit.Text=this.c1FlexGrid1.Rows[row][6].ToString();
		}

		private void chkDefault_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chkDefault.Checked)
			{
				this.cmbSign.Enabled=false;
				this.txtValue.Enabled=false;
				this.txtUnit.Enabled=false;
			}
			else
			{
				this.cmbSign.Enabled=true;
				this.txtValue.Enabled=true;
				this.txtUnit.Enabled=true;
			}
		}

		private void btnEditCheckItem_Click(object sender, System.EventArgs e)
		{
			int row=this.c1FlexGrid1.RowSel;
			if(row<=0) return;
			if(this.chkDefault.Checked)
			{
				this.txtCheckItemCodes.Text=this.EditCheckItem(this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString(),"-1","-1","-1");
				dt1.Rows[row-1][2]=true;
				dt1.Rows[row-1][3]=this.c1FlexGrid1.Rows[row][7].ToString()+this.c1FlexGrid1.Rows[row][8].ToString()+this.c1FlexGrid1.Rows[row][9].ToString();
				dt1.Rows[row-1][4]=this.c1FlexGrid1.Rows[row][7].ToString();
				dt1.Rows[row-1][5]=this.c1FlexGrid1.Rows[row][8].ToString();
				dt1.Rows[row-1][6]=this.c1FlexGrid1.Rows[row][9].ToString();
			}
		    else
			{
				if(this.cmbSign.SelectedIndex<0)
				{
					MessageBox.Show(this,"不能不选择符号");
					this.cmbSign.Focus();
					return;
				}
				if(!(StringUtil.IsNumeric(this.txtValue.Text.Trim().ToString(),false)))
				{
					MessageBox.Show(this,"必须输入数字!");
					this.txtValue.Focus();
					return;
				}
				if(Convert.ToDecimal(this.txtValue.Text.Trim())<0)
				{
					MessageBox.Show(this,"不能输入负数!");
					this.txtValue.Focus();
					return;
				}
				string strSign=this.cmbSign.Text;
				string strValue=this.txtValue.Text.Trim();
				string strUnit=this.txtUnit.Text.Trim();
				string defSign=this.c1FlexGrid1.Rows[row]["DefSign"].ToString();
				string defValue=this.c1FlexGrid1.Rows[row]["DefValue"].ToString();
				string defUnit=this.c1FlexGrid1.Rows[row]["DefUnit"].ToString();
				if(strSign.Equals(defSign)&&strValue.Equals(defValue)&&strUnit.Equals(defUnit))
				{
					this.txtCheckItemCodes.Text=this.EditCheckItem(this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString(),"-1","-1","-1");
					dt1.Rows[row-1][2]=true;
					dt1.Rows[row-1][3]=this.c1FlexGrid1.Rows[row]["DefSign"].ToString()+this.c1FlexGrid1.Rows[row]["DefValue"].ToString()+this.c1FlexGrid1.Rows[row]["DefUnit"].ToString();
					dt1.Rows[row-1][4]=this.c1FlexGrid1.Rows[row]["DefSign"].ToString();
					dt1.Rows[row-1][5]=this.c1FlexGrid1.Rows[row]["DefValue"].ToString();
					dt1.Rows[row-1][6]=this.c1FlexGrid1.Rows[row]["DefUnit"].ToString();
					this.chkDefault.Checked=true;
				}
				else
				{
					this.txtCheckItemCodes.Text=this.EditCheckItem(this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString(),strSign,strValue,strUnit);
					dt1.Rows[row-1][2]=false;
					dt1.Rows[row-1][3]=strSign+strValue+strUnit;
					dt1.Rows[row-1][4]=strSign;
					dt1.Rows[row-1][5]=strValue;
					dt1.Rows[row-1][6]=strUnit;
				}
			}
			IsRead=true;
			this.c1FlexGrid1.DataSource=dt1;
			IsRead=false;
			this.c1FlexGrid1.RowSel=row;
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
			this.DelItem();
			this.RefreshCheckItems(this.txtSysID.Text);
		}

		private void DelItem()
		{
			int row=this.c1FlexGrid1.RowSel;
			if(row<=0) return;
			this.DelCheckItem(this.c1FlexGrid1.Rows[row]["SysCode"].ToString());
		}

		private void btnAllDel_Click(object sender, System.EventArgs e)
		{
			if(this.c1FlexGrid1.Rows.Count<=1) return;

            Cursor.Current = Cursors.WaitCursor;
			for(int i=1;i<=this.c1FlexGrid1.Rows.Count-1;i++)
			{
				this.c1FlexGrid1.RowSel=i;
				this.DelItem();
			}
			this.RefreshCheckItems(this.txtSysID.Text);
            Cursor.Current = Cursors.Default;
		}

		private void btnAllSel_Click(object sender, System.EventArgs e)
		{
			if(this.c1FlexGrid2.Rows.Count<=1) return;
            Cursor.Current = Cursors.WaitCursor;
			for(int i=1;i<=this.c1FlexGrid2.Rows.Count-1;i++)
			{
				this.c1FlexGrid2.RowSel=i;
				this.AddItem();
			}
			this.RefreshCheckItems(this.txtSysID.Text);
            Cursor.Current = Cursors.Default;
		}

		private void btnSel_Click(object sender, System.EventArgs e)
		{
			this.AddItem();
			this.RefreshCheckItems(this.txtSysID.Text);
		}

		private void AddItem()
		{
			int row=this.c1FlexGrid2.RowSel;
			if(row<=0) return;
			this.AddCheckItem(this.c1FlexGrid2.Rows[row]["SysCode"].ToString());
		}

		private void RefreshCheckItems(string strFoodCode)
		{
			this.txtCheckItemCodes.Text=clsFoodClassOpr.CheckItemsFromCode(strFoodCode);
			this.ReadCheckItemCodes();
		}
	}
}
