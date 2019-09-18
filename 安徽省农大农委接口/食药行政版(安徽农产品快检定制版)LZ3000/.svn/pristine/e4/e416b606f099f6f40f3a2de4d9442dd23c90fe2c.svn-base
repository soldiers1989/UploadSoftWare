using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmRecordSend 的摘要说明。
	/// </summary>
	public class frmRecordSend : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnUpload;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label label4;
		private C1.Win.C1List.C1Combo cmbIsSend;
		private C1.Win.C1List.C1Combo cmbRecordType;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmRecordSend()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRecordSend));
			this.btnUpload = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbIsSend = new C1.Win.C1List.C1Combo();
			this.cmbRecordType = new C1.Win.C1List.C1Combo();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbRecordType)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUpload
			// 
			this.btnUpload.Location = new System.Drawing.Point(176, 128);
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Size = new System.Drawing.Size(72, 24);
			this.btnUpload.TabIndex = 1;
			this.btnUpload.Text = "上传";
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 128);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.dtpEndDate);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.dtpStartDate);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cmbRecordType);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.cmbIsSend);
			this.groupBox1.Location = new System.Drawing.Point(16, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 112);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "数据上传选项";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 82);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 21);
			this.label6.TabIndex = 7;
			this.label6.Text = "台帐类型：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpEndDate
			// 
			this.dtpEndDate.CustomFormat = "yyyy年MM月dd日";
			this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEndDate.Location = new System.Drawing.Point(120, 52);
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Size = new System.Drawing.Size(180, 21);
			this.dtpEndDate.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 21);
			this.label5.TabIndex = 5;
			this.label5.Text = "数据截止日期：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpStartDate
			// 
			this.dtpStartDate.CustomFormat = "yyyy年MM月dd日";
			this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStartDate.Location = new System.Drawing.Point(120, 24);
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Size = new System.Drawing.Size(180, 21);
			this.dtpStartDate.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 21);
			this.label4.TabIndex = 4;
			this.label4.Text = "数据起始日期：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.cmbIsSend.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.cmbIsSend.ItemHeight = 15;
			this.cmbIsSend.Location = new System.Drawing.Point(120, 80);
			this.cmbIsSend.MatchEntryTimeout = ((long)(2000));
			this.cmbIsSend.MaxDropDownItems = ((short)(5));
			this.cmbIsSend.MaxLength = 32767;
			this.cmbIsSend.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbIsSend.Name = "cmbIsSend";
			this.cmbIsSend.PartialRightColumn = false;
			this.cmbIsSend.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbIsSend.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbIsSend.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbIsSend.Size = new System.Drawing.Size(180, 22);
			this.cmbIsSend.TabIndex = 2;
			this.cmbIsSend.Visible = false;
			this.cmbIsSend.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// cmbRecordType
			// 
			this.cmbRecordType.AddItemSeparator = ';';
			this.cmbRecordType.Caption = "";
			this.cmbRecordType.CaptionHeight = 17;
			this.cmbRecordType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cmbRecordType.ColumnCaptionHeight = 18;
			this.cmbRecordType.ColumnFooterHeight = 18;
			this.cmbRecordType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cmbRecordType.ContentHeight = 16;
			this.cmbRecordType.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cmbRecordType.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cmbRecordType.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbRecordType.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbRecordType.EditorHeight = 16;
			this.cmbRecordType.GapHeight = 2;
			this.cmbRecordType.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.cmbRecordType.ItemHeight = 15;
			this.cmbRecordType.Location = new System.Drawing.Point(120, 80);
			this.cmbRecordType.MatchEntryTimeout = ((long)(2000));
			this.cmbRecordType.MaxDropDownItems = ((short)(5));
			this.cmbRecordType.MaxLength = 32767;
			this.cmbRecordType.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cmbRecordType.Name = "cmbRecordType";
			this.cmbRecordType.PartialRightColumn = false;
			this.cmbRecordType.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cmbRecordType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cmbRecordType.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cmbRecordType.Size = new System.Drawing.Size(180, 22);
			this.cmbRecordType.TabIndex = 3;
			this.cmbRecordType.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 82);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(96, 21);
			this.label13.TabIndex = 6;
			this.label13.Text = "是否发送：";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label13.Visible = false;
			// 
			// frmRecordSend
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(352, 159);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnUpload);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmRecordSend";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "台帐记录上传";
			this.Load += new System.EventHandler(this.frmRecordSend_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbRecordType)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmRecordSend_Load(object sender, System.EventArgs e)
		{
			this.dtpStartDate.Value=DateTime.Now.AddMonths(-1);
			this.dtpEndDate.Value=DateTime.Now;

			this.cmbRecordType.DataMode=C1.Win.C1List.DataModeEnum.AddItem;
			this.cmbRecordType.AddItemCols=1;
			this.cmbRecordType.AddItemTitles("台帐类型");
			this.cmbRecordType.AddItem("全部");
			this.cmbRecordType.AddItem("进货台帐");
			this.cmbRecordType.AddItem("销售台帐");

			this.cmbIsSend.DataMode=C1.Win.C1List.DataModeEnum.AddItem;
			this.cmbIsSend.AddItemCols=1;
			this.cmbIsSend.AddItemTitles("发送状态");
			this.cmbIsSend.AddItem("");
			this.cmbIsSend.AddItem(ShareOption.SendState1);
			this.cmbIsSend.AddItem(ShareOption.SendState0);
			this.cmbIsSend.SelectedIndex=2;
		}

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			string SalewhereStr="";
			string StockwhereStr="";
            int intType= 0;
  
			SalewhereStr+="SaleDate>=#" + this.dtpStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
			SalewhereStr+=" and SaleDate<#" + this.dtpEndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
			StockwhereStr+="InputDate>=#" + this.dtpStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
			StockwhereStr+=" and InputDate<#" + this.dtpEndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
			if(!this.cmbIsSend.Text.Trim().Equals(""))
			{
				if(this.cmbIsSend.Text.Trim().Equals(ShareOption.SendState1))
				{
					SalewhereStr+=" and IsSended=true";
					StockwhereStr+=" and IsSended=true";
				}
				else if(this.cmbIsSend.Text.Trim().Equals(ShareOption.SendState0))
				{
					SalewhereStr+=" and IsSended=false";
					StockwhereStr+=" and IsSended=false";
				}
			}
			if(this.cmbRecordType.SelectedText!=null)
			{
				if(this.cmbRecordType.SelectedText.Equals("进货台帐")  )
				{
					intType = 1;
				}
				if(this.cmbRecordType.SelectedText.Equals("销售台帐")  )
				{
					intType = 2;
				}
			}

			//检查是否有数据
			string sSaleErrMsg="";
			string sStockErrMsg="";
			int intSaleRecCount = 0;
			int intStockRecCount = 0;
			if(intType==1||intType==0)
			{
				clsStockRecordOpr curStockRecordOpr=new clsStockRecordOpr();
				intStockRecCount=curStockRecordOpr.GetRecCount(StockwhereStr,out sStockErrMsg);
			}
			if(intType==2||intType==0)
			{
				clsSaleRecordOpr curSaleRecordOpr=new clsSaleRecordOpr();
				intSaleRecCount=curSaleRecordOpr.GetRecCount(SalewhereStr,out sSaleErrMsg);
			}
			if(sStockErrMsg.Equals("")&&sSaleErrMsg.Equals("")&&intSaleRecCount==0&&intStockRecCount==0)
			{
				MessageBox.Show(this,"没有所需要上传的纪录!",
					"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if(ShareOption.SysServerIP.Equals("") 
				|| ShareOption.SysServerID.Equals(""))
			{
				MessageBox.Show(this,"请先到选项菜单中设置服务器地址与登录ID！",
					"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			Cursor=Cursors.WaitCursor;
//			string ConnectionStr="Provider=SQLOLEDB.1;Persist Security Info=False;Initial Catalog=FoodSafeSystem;Data Source="+ ShareOption.SysServerIP + ";User ID=" + ShareOption.SysServerID + ";PassWord="+ShareOption.SysServerPass;
//			if(!DataBase.TestConnection(ConnectionStr))
//			{
//				MessageBox.Show(this,"服务器无法连接，请检查网络连接或者服务器的设置！",
//					"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//				return;
//			}
//			if(intSaleRecCount!=0&&intStockRecCount!=0)
//			{
//				PublicOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,ConnectionStr,0);
//			}
//			else if(intSaleRecCount==0)
//			{
//				PublicOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,ConnectionStr,1);
//			}
//			else
//			{
//				PublicOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,ConnectionStr,2);
//			}
			if(intSaleRecCount!=0&&intStockRecCount!=0)
			{
				CommonOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,0);
			}
			else if(intSaleRecCount==0)
			{
				CommonOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,1);
			}
			else
			{
				CommonOperation.UpdateRecordDB(StockwhereStr,SalewhereStr,2);
			}
			Cursor=Cursors.Default;
//			MessageBox.Show(this,"数据上传完成！",
//				"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
