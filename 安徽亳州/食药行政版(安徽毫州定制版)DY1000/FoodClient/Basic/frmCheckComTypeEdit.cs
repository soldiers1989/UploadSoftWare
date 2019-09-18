using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DY.FoodClientLib;


namespace FoodClient
{
	/// <summary>
	/// frmCheckComTypeEdit 的摘要说明。
	/// </summary>
	public class frmCheckComTypeEdit : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTypeName;
		private System.Windows.Forms.TextBox txtNameCall;
		private System.Windows.Forms.TextBox txtAreaCall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbVerType;
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
		private clsCheckComTypeOpr comTypeBll;

        private string svertype = "";
        private string sname = "";
		private string scomkind="";
        private CheckBox chkReadOnly;
        private CheckBox chkLock;
        private GroupBox groupBox1;
        private Label label7;
        private Label label6;
        private Label label29;
        private Label label5;
        private C1.Win.C1List.C1Combo cmbComKind;
        private string tag = string.Empty;

        public frmCheckComTypeEdit()
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckComTypeEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNameCall = new System.Windows.Forms.TextBox();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAreaCall = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVerType = new System.Windows.Forms.ComboBox();
            this.txtDomainTitle = new System.Windows.Forms.TextBox();
            this.txtNameTitle = new System.Windows.Forms.TextBox();
            this.txtAreaTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSampleTitle = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbComKind = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.cmbComKind)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(60, 348);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(168, 248);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNameCall
            // 
            this.txtNameCall.Location = new System.Drawing.Point(144, 48);
            this.txtNameCall.MaxLength = 50;
            this.txtNameCall.Name = "txtNameCall";
            this.txtNameCall.Size = new System.Drawing.Size(186, 21);
            this.txtNameCall.TabIndex = 1;
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(144, 16);
            this.txtTypeName.MaxLength = 50;
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(186, 21);
            this.txtTypeName.TabIndex = 0;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(131, 339);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 8;
            this.txtSysID.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "所属组织机构域标题：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "单位名称域标题：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(40, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "类型名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAreaCall
            // 
            this.txtAreaCall.Location = new System.Drawing.Point(144, 80);
            this.txtAreaCall.MaxLength = 50;
            this.txtAreaCall.Name = "txtAreaCall";
            this.txtAreaCall.Size = new System.Drawing.Size(186, 21);
            this.txtAreaCall.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-5, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 21);
            this.label1.TabIndex = 15;
            this.label1.Text = "对应版本：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbVerType
            // 
            this.cmbVerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVerType.Location = new System.Drawing.Point(131, 312);
            this.cmbVerType.Name = "cmbVerType";
            this.cmbVerType.Size = new System.Drawing.Size(186, 20);
            this.cmbVerType.TabIndex = 16;
            // 
            // txtDomainTitle
            // 
            this.txtDomainTitle.Location = new System.Drawing.Point(144, 184);
            this.txtDomainTitle.MaxLength = 50;
            this.txtDomainTitle.Name = "txtDomainTitle";
            this.txtDomainTitle.Size = new System.Drawing.Size(186, 21);
            this.txtDomainTitle.TabIndex = 6;
            // 
            // txtNameTitle
            // 
            this.txtNameTitle.Location = new System.Drawing.Point(144, 152);
            this.txtNameTitle.MaxLength = 50;
            this.txtNameTitle.Name = "txtNameTitle";
            this.txtNameTitle.Size = new System.Drawing.Size(186, 21);
            this.txtNameTitle.TabIndex = 5;
            // 
            // txtAreaTitle
            // 
            this.txtAreaTitle.Location = new System.Drawing.Point(144, 120);
            this.txtAreaTitle.MaxLength = 50;
            this.txtAreaTitle.Name = "txtAreaTitle";
            this.txtAreaTitle.Size = new System.Drawing.Size(186, 21);
            this.txtAreaTitle.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 21);
            this.label8.TabIndex = 71;
            this.label8.Text = "位置编号域标题：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 21);
            this.label10.TabIndex = 70;
            this.label10.Text = "受检人/单位域标题：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(16, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 21);
            this.label11.TabIndex = 69;
            this.label11.Text = "所属组织域标题：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleTitle
            // 
            this.txtSampleTitle.Location = new System.Drawing.Point(144, 216);
            this.txtSampleTitle.MaxLength = 50;
            this.txtSampleTitle.Name = "txtSampleTitle";
            this.txtSampleTitle.Size = new System.Drawing.Size(186, 21);
            this.txtSampleTitle.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(24, 216);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 21);
            this.label14.TabIndex = 75;
            this.label14.Text = "样品名称域标题：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(56, 248);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 8;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(112, 248);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 9;
            this.chkLock.Text = "停用";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(17, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 8);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(3, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(8, 21);
            this.label7.TabIndex = 66;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(28, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(8, 21);
            this.label6.TabIndex = 65;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(60, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(8, 21);
            this.label29.TabIndex = 64;
            this.label29.Text = "*";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 62;
            this.label5.Text = "对应单位类型：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // cmbComKind
            // 
            this.cmbComKind.AddItemSeparator = ';';
            this.cmbComKind.Caption = "";
            this.cmbComKind.CaptionHeight = 17;
            this.cmbComKind.CaptionStyle = style1;
            this.cmbComKind.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbComKind.ColumnCaptionHeight = 18;
            this.cmbComKind.ColumnFooterHeight = 18;
            this.cmbComKind.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbComKind.ContentHeight = 16;
            this.cmbComKind.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbComKind.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbComKind.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbComKind.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbComKind.EditorHeight = 16;
            this.cmbComKind.EvenRowStyle = style2;
            this.cmbComKind.FooterStyle = style3;
            this.cmbComKind.GapHeight = 2;
            this.cmbComKind.HeadingStyle = style4;
            this.cmbComKind.HighLightRowStyle = style5;
            this.cmbComKind.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbComKind.Images"))));
            this.cmbComKind.ItemHeight = 15;
            this.cmbComKind.Location = new System.Drawing.Point(131, 284);
            this.cmbComKind.MatchEntryTimeout = ((long)(2000));
            this.cmbComKind.MaxDropDownItems = ((short)(5));
            this.cmbComKind.MaxLength = 32767;
            this.cmbComKind.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbComKind.Name = "cmbComKind";
            this.cmbComKind.OddRowStyle = style6;
            this.cmbComKind.PartialRightColumn = false;
            this.cmbComKind.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbComKind.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbComKind.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbComKind.SelectedStyle = style7;
            this.cmbComKind.Size = new System.Drawing.Size(186, 22);
            this.cmbComKind.Style = style8;
            this.cmbComKind.TabIndex = 3;
            this.cmbComKind.Visible = false;
            this.cmbComKind.PropBag = resources.GetString("cmbComKind.PropBag");
            // 
            // frmCheckComTypeEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(338, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSampleTitle);
            this.Controls.Add(this.txtDomainTitle);
            this.Controls.Add(this.txtNameTitle);
            this.Controls.Add(this.txtAreaTitle);
            this.Controls.Add(this.txtAreaCall);
            this.Controls.Add(this.txtNameCall);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.cmbComKind);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbVerType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCheckComTypeEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检测点类型及数据域标题修改";
            this.Load += new System.EventHandler(this.frmCheckComTypeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbComKind)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmCheckComTypeEdit_Load(object sender, System.EventArgs e)
		{
            tag = this.Tag.ToString();
			if(tag.Equals("ADD"))
			{
				this.Text="检测点类型新增";
				this.btnOK.Text="新增";
			}
			else
			{
                this.Text = "检测点类型及数据域标题修改";// "检测点类型修改";
				this.btnOK.Text="保存";
			}

			this.cmbVerType.Items.Add(ShareOption.LocalBaseVersion);
			this.cmbVerType.Items.Add(ShareOption.EnterpriseVersion);
			this.cmbVerType.Items.Add("全部");

			if(ShareOption.IsDataLink)
			{
				this.cmbVerType.SelectedIndex=2;
				this.cmbVerType.Visible=false;
				this.label1.Visible=false;
			}
            //对应版本
            this.cmbVerType.Visible = false;
            this.label1.Visible = false;

			if(!svertype.Equals(""))
			{
				if(svertype.Equals(ShareOption.LocalBaseVersion))
				{
					this.cmbVerType.SelectedIndex=0;
				}
				else if(svertype.Equals(ShareOption.EnterpriseVersion))
				{
					this.cmbVerType.SelectedIndex=1;
				}
				else if(svertype.Equals("全部"))
				{
					this.cmbVerType.SelectedIndex=2;
				}
			}

            //clsCompanyKindOpr unitOpr2=new clsCompanyKindOpr();
            //DataTable dt2=unitOpr2.GetAsDataTable("IsLock=0 And CompanyProperty='被检单位'","SysCode",1);
            //this.cmbComKind.DataSource=dt2.DataSet;
            //this.cmbComKind.DataMember="CompanyKind";
            //this.cmbComKind.DisplayMember="Name";
            //this.cmbComKind.ValueMember="SysCode";
            //this.cmbComKind.Columns["StdCode"].Caption="编号";
            //this.cmbComKind.Columns["Name"].Caption="单位类别";
            //this.cmbComKind.Columns["SysCode"].Caption="系统编号";			
			
            //this.cmbComKind.LeftCol=1;
            //this.cmbComKind.AllowColMove=false;
            //this.cmbComKind.HScrollBar.Style=C1.Win.C1List.ScrollBarStyleEnum.None;
            //this.cmbComKind.MatchEntry=C1.Win.C1List.MatchEntryEnum.Extended;

            //if(!scomkind.Equals(""))
            //{
            //    this.cmbComKind.SelectedValue=scomkind;
            //    this.cmbComKind.Text=clsCompanyKindOpr.NameFromCode(scomkind);
            //}
		}

        internal void setValue(clsCheckComType comModel)
        {
            this.txtSysID.Text = comModel.ID.ToString();
            this.txtTypeName.Text = comModel.TypeName;
            sname = this.txtTypeName.Text;
            svertype = comModel.VerType;
            scomkind = comModel.ComKind;
            this.txtNameCall.Text = comModel.NameCall;
            this.txtAreaCall.Text = comModel.AreaCall;
            this.chkLock.Checked = comModel.IsLock;
            this.chkReadOnly.Checked = comModel.IsReadOnly;
            this.txtAreaTitle.Text = comModel.AreaTitle;
            this.txtNameTitle.Text = comModel.NameTitle;
            this.txtDomainTitle.Text = comModel.DomainTitle;
            this.txtSampleTitle.Text = comModel.SampleTitle;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //检验输入的值是否合法
            //必须输入的是否已输入
            if (this.txtTypeName.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "单位类型名称必须输入!");
                this.txtTypeName.Focus();
                return;
            }
            if (this.txtNameCall.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "单位名称域标题必须输入!");
                this.txtNameCall.Focus();
                return;
            }
            if (this.txtAreaCall.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "所属组织机构域标题必须输入!");
                this.txtAreaCall.Focus();
                return;
            }
            if (clsCheckComTypeOpr.TypeNameIsExist(this.txtTypeName.Text.Trim()) && (!this.txtTypeName.Text.Trim().Equals(sname)))
            {
                MessageBox.Show(this, "单位类型名称重复请重新录入!");
                this.txtTypeName.Focus();
                this.txtTypeName.Select(0, this.txtTypeName.Text.Length);
                return;
            }
            if (this.cmbVerType.Text.Equals(""))
            {
                MessageBox.Show(this, "请选择对应版本!");
                this.cmbVerType.Focus();
                return;
            }

            //非法字符检查
            Control posControl;
            if (RegularCheck.HaveSpecChar(this, out posControl))
            {
                MessageBox.Show(this, "输入中有非法字符,请检查!");
                posControl.Focus();
                return;
            }

            //非字符型检查


            //取值
            model = new clsCheckComType();
            model.ID = Convert.ToInt32(this.txtSysID.Text);
            model.TypeName = this.txtTypeName.Text.Trim();
            model.NameCall = this.txtNameCall.Text.Trim();
            model.AreaCall = this.txtAreaCall.Text.Trim();
            model.VerType = this.cmbVerType.Text.Trim();
            model.IsLock = this.chkLock.Checked;
            model.IsReadOnly = this.chkReadOnly.Checked;
            if (this.cmbComKind.SelectedValue != null)
            {
                model.ComKind = this.cmbComKind.SelectedValue.ToString();
            }
            else
            {
                model.ComKind = "";
            }
            model.AreaTitle = this.txtAreaTitle.Text.Trim();
            model.NameTitle = this.txtNameTitle.Text.Trim();
            model.DomainTitle = this.txtDomainTitle.Text.Trim();
            model.SampleTitle = this.txtSampleTitle.Text.Trim();
            //对数据库进行操作
            string err = string.Empty;
            comTypeBll = new clsCheckComTypeOpr();
            if (tag.Equals("ADD"))
            {
                comTypeBll.Insert(model, out err);
            }
            else
            {
                comTypeBll.UpdatePart(model, out err);
            }
            if (!err.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！");
            }

            //退出
            this.DialogResult = DialogResult.OK;
            CommonOperation.GetTitleSet();
            this.Close();
        }

//        private void cmbComKind_SelectedValueChanged(object sender, EventArgs e)
//        {
////			if(this.cmbComKind.SelectedValue!=null)
////			{
////				this.txtTypeName.Enabled=false;
////				this.txtTypeName.Text=this.cmbComKind.SelectedText;
////			}
////			else
////			{
////				this.txtTypeName.Enabled=true;
////			}
//        }
	}
}
