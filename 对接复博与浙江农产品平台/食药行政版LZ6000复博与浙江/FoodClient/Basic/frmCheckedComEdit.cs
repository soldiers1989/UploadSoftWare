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
    /// 被检测单位/生产单位 增加或者修改
    /// </summary>
    public class frmCheckedComEdit : TitleBarBase
    {
        #region 控件变量
        private System.Windows.Forms.TextBox txtLinkInfo;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DateTimePicker dtpRegDate;
        private System.Windows.Forms.TextBox txtFoodSafeRecord;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtOtherInfo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtProductInfo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCreditRecord;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtLinkMan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPostCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtIncorporator;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtRegCapital;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkReadOnly;
        private System.Windows.Forms.CheckBox chkLock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtShortName;
        private System.Windows.Forms.Label lblDomainTitle;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1List.C1Combo cmbDistrict;
        private C1.Win.C1List.C1Combo cmbCompanyKind;
        private C1.Win.C1List.C1Combo cmbCompanyProperty;
        private C1.Win.C1List.C1Combo cmbCreditLevel;
        private C1.Win.C1List.C1Combo cmbCheckLevel;
        private System.Windows.Forms.TextBox txtCompanyID;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtOtherCodeInfo;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtComProperty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblArea;
        private System.ComponentModel.Container components = null;
        private TextBox txtRegDate;
        #endregion

        private clsCompany companyModel;
        private readonly clsCompanyOpr companyBll;
        private string IdCode = string.Empty;
        private string codeValue;
        private string kindValue=string.Empty;
        private bool IsLoading = true;
        private string tag;
        private TextBox TXTTsgn;
        private DataTable dtblCompanyKind = null;

        //时间控件
        private string CBOYear = string.Empty;
        private string CBOMouth = string.Empty;
        private Label label6;
        private TextBox textBox1;
        private Label label15;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label30;
        private TextBox textBox4;
        private Label label31;
        private TextBox textBox5;
        private Label label32;
        private TextBox textBox6;
        private Label label33;
        private TextBox textBox7;
        private Label label34;
        private TextBox textBox8;
        private Label label35;
        private TextBox textBox9;
        private Label label36;
        private TextBox textBox10;
        private Label label37;
        private Label label29;
        private string CBODay = string.Empty;


        /// <summary>
        /// 构造函数
        /// </summary>
        public frmCheckedComEdit(clsCompany model,string distCode)
        {
            InitializeComponent();
            codeValue = distCode;
            companyBll = new clsCompanyOpr();
            if (model != null)
            {
                setValue(model);
            }
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckedComEdit));
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
            C1.Win.C1List.Style style25 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style26 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style27 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style28 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style29 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style30 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style31 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style32 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style33 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style34 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style35 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style36 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style37 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style38 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style39 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style40 = new C1.Win.C1List.Style();
            this.txtLinkInfo = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.dtpRegDate = new System.Windows.Forms.DateTimePicker();
            this.txtFoodSafeRecord = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtOtherInfo = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtProductInfo = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCreditRecord = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtLinkMan = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtIncorporator = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRegCapital = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.lblDomainTitle = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCompanyID = new System.Windows.Forms.TextBox();
            this.cmbDistrict = new C1.Win.C1List.C1Combo();
            this.cmbCompanyKind = new C1.Win.C1List.C1Combo();
            this.cmbCompanyProperty = new C1.Win.C1List.C1Combo();
            this.cmbCreditLevel = new C1.Win.C1List.C1Combo();
            this.cmbCheckLevel = new C1.Win.C1List.C1Combo();
            this.label26 = new System.Windows.Forms.Label();
            this.txtOtherCodeInfo = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtComProperty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtRegDate = new System.Windows.Forms.TextBox();
            this.TXTTsgn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDistrict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreditLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLinkInfo
            // 
            this.txtLinkInfo.Location = new System.Drawing.Point(536, 348);
            this.txtLinkInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtLinkInfo.Name = "txtLinkInfo";
            this.txtLinkInfo.Size = new System.Drawing.Size(199, 26);
            this.txtLinkInfo.TabIndex = 17;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(397, 348);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(128, 28);
            this.label25.TabIndex = 52;
            this.label25.Text = "联系方式：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.Location = new System.Drawing.Point(536, 263);
            this.dtpRegDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.Size = new System.Drawing.Size(198, 26);
            this.dtpRegDate.TabIndex = 12;
            this.dtpRegDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            this.dtpRegDate.DropDown += new System.EventHandler(this.dtpProduceDate_DropDown);
            // 
            // txtFoodSafeRecord
            // 
            this.txtFoodSafeRecord.Location = new System.Drawing.Point(903, 516);
            this.txtFoodSafeRecord.Margin = new System.Windows.Forms.Padding(4);
            this.txtFoodSafeRecord.Name = "txtFoodSafeRecord";
            this.txtFoodSafeRecord.Size = new System.Drawing.Size(201, 26);
            this.txtFoodSafeRecord.TabIndex = 22;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(765, 516);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 28);
            this.label20.TabIndex = 43;
            this.label20.Text = "安全记录：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOtherInfo
            // 
            this.txtOtherInfo.Location = new System.Drawing.Point(903, 556);
            this.txtOtherInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtOtherInfo.Name = "txtOtherInfo";
            this.txtOtherInfo.Size = new System.Drawing.Size(201, 26);
            this.txtOtherInfo.TabIndex = 21;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(36, 140);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 28);
            this.label21.TabIndex = 54;
            this.label21.Text = "卫生许可证号：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductInfo
            // 
            this.txtProductInfo.Location = new System.Drawing.Point(903, 472);
            this.txtProductInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductInfo.Name = "txtProductInfo";
            this.txtProductInfo.Size = new System.Drawing.Size(201, 26);
            this.txtProductInfo.TabIndex = 20;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(765, 472);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(128, 28);
            this.label22.TabIndex = 42;
            this.label22.Text = "产品信息：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreditRecord
            // 
            this.txtCreditRecord.Location = new System.Drawing.Point(903, 428);
            this.txtCreditRecord.Margin = new System.Windows.Forms.Padding(4);
            this.txtCreditRecord.Name = "txtCreditRecord";
            this.txtCreditRecord.Size = new System.Drawing.Size(201, 26);
            this.txtCreditRecord.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(765, 428);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(128, 28);
            this.label23.TabIndex = 53;
            this.label23.Text = "信用记录：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(35, 391);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(128, 28);
            this.label24.TabIndex = 41;
            this.label24.Text = "信用等级：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Location = new System.Drawing.Point(173, 348);
            this.txtLinkMan.Margin = new System.Windows.Forms.Padding(4);
            this.txtLinkMan.MaxLength = 50;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(199, 26);
            this.txtLinkMan.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(35, 348);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 28);
            this.label12.TabIndex = 40;
            this.label12.Text = "联系人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(536, 305);
            this.txtPostCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtPostCode.MaxLength = 50;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(199, 26);
            this.txtPostCode.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(397, 305);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 28);
            this.label13.TabIndex = 51;
            this.label13.Text = "邮编：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(35, 305);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 28);
            this.label14.TabIndex = 39;
            this.label14.Text = "详细地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(173, 305);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(199, 26);
            this.txtAddress.TabIndex = 14;
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(433, 181);
            this.lblArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(92, 23);
            this.lblArea.TabIndex = 38;
            this.lblArea.Text = "所属组织：";
            this.lblArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(397, 263);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(128, 28);
            this.label16.TabIndex = 50;
            this.label16.Text = "注册时间：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIncorporator
            // 
            this.txtIncorporator.Location = new System.Drawing.Point(173, 263);
            this.txtIncorporator.Margin = new System.Windows.Forms.Padding(4);
            this.txtIncorporator.MaxLength = 50;
            this.txtIncorporator.Name = "txtIncorporator";
            this.txtIncorporator.Size = new System.Drawing.Size(199, 26);
            this.txtIncorporator.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(35, 263);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(128, 28);
            this.label17.TabIndex = 37;
            this.label17.Text = "法人：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(536, 220);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnit.MaxLength = 10;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(199, 26);
            this.txtUnit.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(397, 220);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 28);
            this.label18.TabIndex = 49;
            this.label18.Text = "资金单位：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRegCapital
            // 
            this.txtRegCapital.Location = new System.Drawing.Point(173, 220);
            this.txtRegCapital.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegCapital.MaxLength = 20;
            this.txtRegCapital.Name = "txtRegCapital";
            this.txtRegCapital.Size = new System.Drawing.Size(201, 26);
            this.txtRegCapital.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(35, 220);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(128, 28);
            this.label19.TabIndex = 36;
            this.label19.Text = "注册资金：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 816);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(352, 503);
            this.chkReadOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(85, 32);
            this.chkReadOnly.TabIndex = 29;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(448, 503);
            this.chkLock.Margin = new System.Windows.Forms.Padding(4);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(64, 32);
            this.chkLock.TabIndex = 25;
            this.chkLock.Text = "停用";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(68, 419);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 28);
            this.label1.TabIndex = 44;
            this.label1.Text = "备注：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(170, 421);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(564, 73);
            this.txtRemark.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(436, 391);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 28);
            this.label2.TabIndex = 55;
            this.label2.Text = "监控级别：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(333, 804);
            this.txtKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(41, 26);
            this.txtKey.TabIndex = 3;
            this.txtKey.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(229, 811);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 28);
            this.label3.TabIndex = 46;
            this.label3.Text = "快捷编码：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(173, 72);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(564, 26);
            this.txtFullName.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(59, 76);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "单位名称：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(173, 41);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(244, 26);
            this.txtCode.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(436, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 28);
            this.label11.TabIndex = 31;
            this.label11.Text = "营业执照：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(93, 809);
            this.txtSysID.Margin = new System.Windows.Forms.Padding(4);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(63, 26);
            this.txtSysID.TabIndex = 28;
            this.txtSysID.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(637, 502);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 32);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(520, 502);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 32);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(35, 177);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 28);
            this.label8.TabIndex = 48;
            this.label8.Text = "单位类别：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(13, 745);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 28);
            this.label7.TabIndex = 35;
            this.label7.Text = "单位性质：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(335, 768);
            this.txtShortName.Margin = new System.Windows.Forms.Padding(4);
            this.txtShortName.MaxLength = 50;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(52, 26);
            this.txtShortName.TabIndex = 5;
            this.txtShortName.Visible = false;
            // 
            // lblDomainTitle
            // 
            this.lblDomainTitle.Location = new System.Drawing.Point(11, 111);
            this.lblDomainTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDomainTitle.Name = "lblDomainTitle";
            this.lblDomainTitle.Size = new System.Drawing.Size(155, 23);
            this.lblDomainTitle.TabIndex = 34;
            this.lblDomainTitle.Text = "档口/店面/车牌号：";
            this.lblDomainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(173, 111);
            this.txtDisplayName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDisplayName.MaxLength = 50;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(564, 26);
            this.txtDisplayName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(229, 767);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 28);
            this.label5.TabIndex = 47;
            this.label5.Text = "单位简称：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // txtCompanyID
            // 
            this.txtCompanyID.Location = new System.Drawing.Point(536, 42);
            this.txtCompanyID.Margin = new System.Windows.Forms.Padding(4);
            this.txtCompanyID.MaxLength = 50;
            this.txtCompanyID.Name = "txtCompanyID";
            this.txtCompanyID.Size = new System.Drawing.Size(199, 26);
            this.txtCompanyID.TabIndex = 1;
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.AddItemSeparator = ';';
            this.cmbDistrict.Caption = "";
            this.cmbDistrict.CaptionHeight = 17;
            this.cmbDistrict.CaptionStyle = style1;
            this.cmbDistrict.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbDistrict.ColumnCaptionHeight = 18;
            this.cmbDistrict.ColumnFooterHeight = 18;
            this.cmbDistrict.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbDistrict.ContentHeight = 16;
            this.cmbDistrict.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbDistrict.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbDistrict.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDistrict.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbDistrict.EditorHeight = 16;
            this.cmbDistrict.Enabled = false;
            this.cmbDistrict.EvenRowStyle = style2;
            this.cmbDistrict.FooterStyle = style3;
            this.cmbDistrict.GapHeight = 2;
            this.cmbDistrict.HeadingStyle = style4;
            this.cmbDistrict.HighLightRowStyle = style5;
            this.cmbDistrict.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbDistrict.Images"))));
            this.cmbDistrict.ItemHeight = 15;
            this.cmbDistrict.Location = new System.Drawing.Point(536, 177);
            this.cmbDistrict.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDistrict.MatchEntryTimeout = ((long)(2000));
            this.cmbDistrict.MaxDropDownItems = ((short)(5));
            this.cmbDistrict.MaxLength = 50;
            this.cmbDistrict.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.OddRowStyle = style6;
            this.cmbDistrict.PartialRightColumn = false;
            this.cmbDistrict.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbDistrict.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbDistrict.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbDistrict.SelectedStyle = style7;
            this.cmbDistrict.Size = new System.Drawing.Size(200, 22);
            this.cmbDistrict.Style = style8;
            this.cmbDistrict.TabIndex = 13;
            this.cmbDistrict.TextChanged += new System.EventHandler(this.cmbDistrict_TextChanged);
            this.cmbDistrict.PropBag = resources.GetString("cmbDistrict.PropBag");
            // 
            // cmbCompanyKind
            // 
            this.cmbCompanyKind.AddItemSeparator = ';';
            this.cmbCompanyKind.Caption = "";
            this.cmbCompanyKind.CaptionHeight = 17;
            this.cmbCompanyKind.CaptionStyle = style9;
            this.cmbCompanyKind.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCompanyKind.ColumnCaptionHeight = 18;
            this.cmbCompanyKind.ColumnFooterHeight = 18;
            this.cmbCompanyKind.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCompanyKind.ContentHeight = 16;
            this.cmbCompanyKind.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCompanyKind.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyKind.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCompanyKind.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCompanyKind.EditorHeight = 16;
            this.cmbCompanyKind.Enabled = false;
            this.cmbCompanyKind.EvenRowStyle = style10;
            this.cmbCompanyKind.FooterStyle = style11;
            this.cmbCompanyKind.GapHeight = 2;
            this.cmbCompanyKind.HeadingStyle = style12;
            this.cmbCompanyKind.HighLightRowStyle = style13;
            this.cmbCompanyKind.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCompanyKind.Images"))));
            this.cmbCompanyKind.ItemHeight = 15;
            this.cmbCompanyKind.Location = new System.Drawing.Point(173, 177);
            this.cmbCompanyKind.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompanyKind.MatchEntryTimeout = ((long)(2000));
            this.cmbCompanyKind.MaxDropDownItems = ((short)(5));
            this.cmbCompanyKind.MaxLength = 50;
            this.cmbCompanyKind.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCompanyKind.Name = "cmbCompanyKind";
            this.cmbCompanyKind.OddRowStyle = style14;
            this.cmbCompanyKind.PartialRightColumn = false;
            this.cmbCompanyKind.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCompanyKind.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCompanyKind.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCompanyKind.SelectedStyle = style15;
            this.cmbCompanyKind.Size = new System.Drawing.Size(200, 22);
            this.cmbCompanyKind.Style = style16;
            this.cmbCompanyKind.TabIndex = 8;
            this.cmbCompanyKind.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbCompanyKind_BeforeOpen);
            this.cmbCompanyKind.PropBag = resources.GetString("cmbCompanyKind.PropBag");
            // 
            // cmbCompanyProperty
            // 
            this.cmbCompanyProperty.AddItemSeparator = ';';
            this.cmbCompanyProperty.Caption = "";
            this.cmbCompanyProperty.CaptionHeight = 17;
            this.cmbCompanyProperty.CaptionStyle = style17;
            this.cmbCompanyProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCompanyProperty.ColumnCaptionHeight = 18;
            this.cmbCompanyProperty.ColumnFooterHeight = 18;
            this.cmbCompanyProperty.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCompanyProperty.ContentHeight = 16;
            this.cmbCompanyProperty.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCompanyProperty.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyProperty.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCompanyProperty.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCompanyProperty.EditorHeight = 16;
            this.cmbCompanyProperty.EvenRowStyle = style18;
            this.cmbCompanyProperty.FooterStyle = style19;
            this.cmbCompanyProperty.GapHeight = 2;
            this.cmbCompanyProperty.HeadingStyle = style20;
            this.cmbCompanyProperty.HighLightRowStyle = style21;
            this.cmbCompanyProperty.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCompanyProperty.Images"))));
            this.cmbCompanyProperty.ItemHeight = 15;
            this.cmbCompanyProperty.Location = new System.Drawing.Point(105, 744);
            this.cmbCompanyProperty.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompanyProperty.MatchEntryTimeout = ((long)(2000));
            this.cmbCompanyProperty.MaxDropDownItems = ((short)(5));
            this.cmbCompanyProperty.MaxLength = 10;
            this.cmbCompanyProperty.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCompanyProperty.Name = "cmbCompanyProperty";
            this.cmbCompanyProperty.OddRowStyle = style22;
            this.cmbCompanyProperty.PartialRightColumn = false;
            this.cmbCompanyProperty.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCompanyProperty.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.SelectedStyle = style23;
            this.cmbCompanyProperty.Size = new System.Drawing.Size(64, 22);
            this.cmbCompanyProperty.Style = style24;
            this.cmbCompanyProperty.TabIndex = 7;
            this.cmbCompanyProperty.Visible = false;
            this.cmbCompanyProperty.PropBag = resources.GetString("cmbCompanyProperty.PropBag");
            // 
            // cmbCreditLevel
            // 
            this.cmbCreditLevel.AddItemSeparator = ';';
            this.cmbCreditLevel.Caption = "";
            this.cmbCreditLevel.CaptionHeight = 17;
            this.cmbCreditLevel.CaptionStyle = style25;
            this.cmbCreditLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCreditLevel.ColumnCaptionHeight = 18;
            this.cmbCreditLevel.ColumnFooterHeight = 18;
            this.cmbCreditLevel.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCreditLevel.ContentHeight = 16;
            this.cmbCreditLevel.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCreditLevel.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCreditLevel.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCreditLevel.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCreditLevel.EditorHeight = 16;
            this.cmbCreditLevel.EvenRowStyle = style26;
            this.cmbCreditLevel.FooterStyle = style27;
            this.cmbCreditLevel.GapHeight = 2;
            this.cmbCreditLevel.HeadingStyle = style28;
            this.cmbCreditLevel.HighLightRowStyle = style29;
            this.cmbCreditLevel.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCreditLevel.Images"))));
            this.cmbCreditLevel.ItemHeight = 15;
            this.cmbCreditLevel.Location = new System.Drawing.Point(173, 391);
            this.cmbCreditLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCreditLevel.MatchEntryTimeout = ((long)(2000));
            this.cmbCreditLevel.MaxDropDownItems = ((short)(5));
            this.cmbCreditLevel.MaxLength = 10;
            this.cmbCreditLevel.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCreditLevel.Name = "cmbCreditLevel";
            this.cmbCreditLevel.OddRowStyle = style30;
            this.cmbCreditLevel.PartialRightColumn = false;
            this.cmbCreditLevel.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCreditLevel.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCreditLevel.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCreditLevel.SelectedStyle = style31;
            this.cmbCreditLevel.Size = new System.Drawing.Size(200, 22);
            this.cmbCreditLevel.Style = style32;
            this.cmbCreditLevel.TabIndex = 18;
            this.cmbCreditLevel.PropBag = resources.GetString("cmbCreditLevel.PropBag");
            // 
            // cmbCheckLevel
            // 
            this.cmbCheckLevel.AddItemSeparator = ';';
            this.cmbCheckLevel.Caption = "";
            this.cmbCheckLevel.CaptionHeight = 17;
            this.cmbCheckLevel.CaptionStyle = style33;
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
            this.cmbCheckLevel.EvenRowStyle = style34;
            this.cmbCheckLevel.FooterStyle = style35;
            this.cmbCheckLevel.GapHeight = 2;
            this.cmbCheckLevel.HeadingStyle = style36;
            this.cmbCheckLevel.HighLightRowStyle = style37;
            this.cmbCheckLevel.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckLevel.Images"))));
            this.cmbCheckLevel.ItemHeight = 15;
            this.cmbCheckLevel.Location = new System.Drawing.Point(536, 391);
            this.cmbCheckLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCheckLevel.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckLevel.MaxDropDownItems = ((short)(5));
            this.cmbCheckLevel.MaxLength = 10;
            this.cmbCheckLevel.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckLevel.Name = "cmbCheckLevel";
            this.cmbCheckLevel.OddRowStyle = style38;
            this.cmbCheckLevel.PartialRightColumn = false;
            this.cmbCheckLevel.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckLevel.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.SelectedStyle = style39;
            this.cmbCheckLevel.Size = new System.Drawing.Size(200, 22);
            this.cmbCheckLevel.Style = style40;
            this.cmbCheckLevel.TabIndex = 23;
            this.cmbCheckLevel.PropBag = resources.GetString("cmbCheckLevel.PropBag");
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(17, 781);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 28);
            this.label26.TabIndex = 32;
            this.label26.Text = "标准编码：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label26.Visible = false;
            // 
            // txtOtherCodeInfo
            // 
            this.txtOtherCodeInfo.Location = new System.Drawing.Point(72, 777);
            this.txtOtherCodeInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtOtherCodeInfo.Name = "txtOtherCodeInfo";
            this.txtOtherCodeInfo.Size = new System.Drawing.Size(40, 26);
            this.txtOtherCodeInfo.TabIndex = 2;
            this.txtOtherCodeInfo.Visible = false;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(11, 41);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(152, 28);
            this.label27.TabIndex = 45;
            this.label27.Text = "系统编号：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComProperty
            // 
            this.txtComProperty.Location = new System.Drawing.Point(903, 592);
            this.txtComProperty.Margin = new System.Windows.Forms.Padding(4);
            this.txtComProperty.Name = "txtComProperty";
            this.txtComProperty.Size = new System.Drawing.Size(201, 26);
            this.txtComProperty.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(798, 595);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 28);
            this.label4.TabIndex = 57;
            this.label4.Text = "区域性质：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(45, 71);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(11, 28);
            this.label39.TabIndex = 58;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label39.Visible = false;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(428, 176);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(11, 28);
            this.label28.TabIndex = 59;
            this.label28.Text = "*";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label28.Visible = false;
            // 
            // txtRegDate
            // 
            this.txtRegDate.BackColor = System.Drawing.Color.White;
            this.txtRegDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegDate.ForeColor = System.Drawing.Color.Black;
            this.txtRegDate.Location = new System.Drawing.Point(536, 263);
            this.txtRegDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegDate.MaxLength = 10;
            this.txtRegDate.Name = "txtRegDate";
            this.txtRegDate.Size = new System.Drawing.Size(173, 26);
            this.txtRegDate.TabIndex = 61;
            // 
            // TXTTsgn
            // 
            this.TXTTsgn.Location = new System.Drawing.Point(577, 783);
            this.TXTTsgn.Margin = new System.Windows.Forms.Padding(4);
            this.TXTTsgn.Name = "TXTTsgn";
            this.TXTTsgn.Size = new System.Drawing.Size(132, 26);
            this.TXTTsgn.TabIndex = 62;
            this.TXTTsgn.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(767, 556);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 28);
            this.label6.TabIndex = 63;
            this.label6.Text = "其他信息：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(173, 144);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(564, 26);
            this.textBox1.TabIndex = 64;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(764, 71);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 28);
            this.label15.TabIndex = 65;
            this.label15.Text = "发证机构：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(900, 72);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.MaxLength = 100;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 26);
            this.textBox2.TabIndex = 66;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(903, 115);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.MaxLength = 100;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 26);
            this.textBox3.TabIndex = 68;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(767, 113);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(128, 28);
            this.label30.TabIndex = 67;
            this.label30.Text = "发证日期：";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(903, 155);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.MaxLength = 100;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 26);
            this.textBox4.TabIndex = 70;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(767, 153);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(128, 28);
            this.label31.TabIndex = 69;
            this.label31.Text = "有效起始日期：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(903, 191);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.MaxLength = 100;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(200, 26);
            this.textBox5.TabIndex = 72;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(767, 189);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(128, 28);
            this.label32.TabIndex = 71;
            this.label32.Text = "有效截取日期：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(903, 228);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4);
            this.textBox6.MaxLength = 100;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(200, 26);
            this.textBox6.TabIndex = 74;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(767, 227);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(128, 28);
            this.label33.TabIndex = 73;
            this.label33.Text = "违规次数：";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(903, 265);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.MaxLength = 100;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(200, 26);
            this.textBox7.TabIndex = 76;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(767, 264);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(128, 28);
            this.label34.TabIndex = 75;
            this.label34.Text = "经度：";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(903, 307);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.MaxLength = 100;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(200, 26);
            this.textBox8.TabIndex = 78;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(767, 305);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(128, 28);
            this.label35.TabIndex = 77;
            this.label35.Text = "纬度：";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(903, 349);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4);
            this.textBox9.MaxLength = 100;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(200, 26);
            this.textBox9.TabIndex = 80;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(767, 348);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(128, 28);
            this.label36.TabIndex = 79;
            this.label36.Text = "规模：";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(900, 392);
            this.textBox10.Margin = new System.Windows.Forms.Padding(4);
            this.textBox10.MaxLength = 100;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(200, 26);
            this.textBox10.TabIndex = 82;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(764, 391);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(128, 28);
            this.label37.TabIndex = 81;
            this.label37.Text = "违规处理：";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(11, 40);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(11, 28);
            this.label29.TabIndex = 83;
            this.label29.Text = "*";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label29.Visible = false;
            // 
            // frmCheckedComEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(753, 546);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TXTTsgn);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.txtComProperty);
            this.Controls.Add(this.txtLinkInfo);
            this.Controls.Add(this.txtFoodSafeRecord);
            this.Controls.Add(this.txtCreditRecord);
            this.Controls.Add(this.txtLinkMan);
            this.Controls.Add(this.txtPostCode);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtIncorporator);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtRegCapital);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.txtShortName);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.txtCompanyID);
            this.Controls.Add(this.txtOtherCodeInfo);
            this.Controls.Add(this.txtOtherInfo);
            this.Controls.Add(this.txtProductInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCompanyProperty);
            this.Controls.Add(this.cmbDistrict);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.dtpRegDate);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblDomainTitle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCreditLevel);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCheckLevel);
            this.Controls.Add(this.cmbCompanyKind);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtRegDate);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmCheckedComEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "被检商户修改";
            this.Load += new System.EventHandler(this.frmCheckedComEdit_Load);
            this.Controls.SetChildIndex(this.txtRegDate, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbCompanyKind, 0);
            this.Controls.SetChildIndex(this.cmbCheckLevel, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.cmbCreditLevel, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblDomainTitle, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkLock, 0);
            this.Controls.SetChildIndex(this.chkReadOnly, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.lblArea, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.dtpRegDate, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.cmbDistrict, 0);
            this.Controls.SetChildIndex(this.cmbCompanyProperty, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtProductInfo, 0);
            this.Controls.SetChildIndex(this.txtOtherInfo, 0);
            this.Controls.SetChildIndex(this.txtOtherCodeInfo, 0);
            this.Controls.SetChildIndex(this.txtCompanyID, 0);
            this.Controls.SetChildIndex(this.txtDisplayName, 0);
            this.Controls.SetChildIndex(this.txtShortName, 0);
            this.Controls.SetChildIndex(this.txtSysID, 0);
            this.Controls.SetChildIndex(this.txtCode, 0);
            this.Controls.SetChildIndex(this.txtFullName, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.txtRegCapital, 0);
            this.Controls.SetChildIndex(this.txtUnit, 0);
            this.Controls.SetChildIndex(this.txtIncorporator, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.txtPostCode, 0);
            this.Controls.SetChildIndex(this.txtLinkMan, 0);
            this.Controls.SetChildIndex(this.txtCreditRecord, 0);
            this.Controls.SetChildIndex(this.txtFoodSafeRecord, 0);
            this.Controls.SetChildIndex(this.txtLinkInfo, 0);
            this.Controls.SetChildIndex(this.txtComProperty, 0);
            this.Controls.SetChildIndex(this.label39, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.TXTTsgn, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.textBox3, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.textBox4, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.textBox5, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.textBox6, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.textBox7, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.textBox8, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.textBox9, 0);
            this.Controls.SetChildIndex(this.label37, 0);
            this.Controls.SetChildIndex(this.textBox10, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            ((System.ComponentModel.ISupportInitialize)(this.cmbDistrict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreditLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private void frmCheckedComEdit_Load(object sender, System.EventArgs e)
        {
			TitleBarText = "新增";
            tag = Tag.ToString();
            string title = "新增";
            string strWhere = string.Empty;
            int top = 0;
            lblArea.Text = ShareOption.AreaTitle + "：";
            lblDomainTitle.Text = ShareOption.DomainTitle + "：";

           
            if (ShareOption.IsDataLink)//如果是单机版本
            {
                if (tag.IndexOf("CHECKED") >= 0)//如果被检单位
                {
                    if (tag.IndexOf("EDIT") >= 0)//如果编辑
                    {
                        title = "修改";
						TitleBarText = "修改";
                    }

                    //子编码与父编码长度不一
                    if (txtCode.Text.Length == ShareOption.CompanyStdCodeLen * 2)//被检单位
                    {
                        Text = ShareOption.NameTitle + title;
                        label8.Visible = false;
                        cmbCompanyKind.Visible = false;
                        cmbDistrict.Enabled = false;
                        Height = 568;
                        top = txtComProperty.Top;
                        txtComProperty.Top = top - 30;
                        label4.Top = top - 30;
                        txtRemark.Top = top;
                        label1.Top = top;

                        btnCancel.Top = Height - 60;
                        btnOK.Top = Height - 60;
                        chkLock.Top = Height - 60;
                        chkReadOnly.Top = Height - 60;

                        label21.Left = label2.Left;
                        txtOtherInfo.Top = cmbCheckLevel.Top;
                        txtOtherInfo.Left = cmbCheckLevel.Left;
                        txtOtherInfo.Width = cmbCheckLevel.Width;
                        label2.Top = label8.Top;
                        label2.Left = label8.Left;
                        cmbCheckLevel.Top = cmbCompanyKind.Top;
                        cmbCheckLevel.Left = cmbCompanyKind.Left;
                        lblDomainTitle.Text = ShareOption.DomainTitle + "：";
                        //label29.Location = new System.Drawing.Point(31, 7);//设置必填"*"位置
                        label27.Text = "营业执照号：";

                        label39.Location = new System.Drawing.Point(30, 40);
                        label10.Text = ShareOption.NameTitle + "：";
                        label21.Text = "卫生许可证号：";
                    }
                    else
                    {
                        Text = "被检单位" + title;
                        Height = 568;
                        lblDomainTitle.Text = "单位信息：";
                        label26.Text = "标准编码：";
                        label21.Text = "其它信息：";
                    }
                    label26.Text = "标准编码：";
                }
                else  //生产单位 
                {
                    if (tag.IndexOf("EDIT") >= 0)//如果编辑
                    {
                        title = "修改";
						TitleBarText = "修改";
                    }

                    Text = ShareOption.ProductionUnitNameTag + title;// "生产单位"
                    Height = 548;

                    lblArea.Visible = false;
                    lblDomainTitle.Enabled = true;
                    lblDomainTitle.Text = "单位信息：";
                    label8.Text = "单位类别：";
                    label10.Text = "单位名称：";

                    txtDisplayName.Enabled = true;
                    cmbCompanyKind.Enabled = true;
                    cmbDistrict.Visible = false;
                    label28.Visible = false;

                    top = txtComProperty.Top;

                    txtComProperty.Top = top - 30;
                    label4.Top = top - 30;

                    txtRemark.Top = top;
                    label1.Top = top;

                    btnCancel.Top = Height - 50;
                    btnOK.Top = Height - 50;
                    chkLock.Top = Height - 50;
                    chkReadOnly.Top = Height - 50;

                    label21.Visible = false;
                    txtOtherInfo.Visible = false;

                    label2.Left = lblArea.Left - 30;
                    label2.Top = lblArea.Top;
                    cmbCheckLevel.Left = cmbDistrict.Left;
                    cmbCheckLevel.Top = cmbDistrict.Top;
                }
            }
            else //网络版
            {
                if (tag.IndexOf("CHECKED") >= 0)//如果被检单位
                {
                    if (tag.IndexOf("EDIT") >= 0)
                    {
                        title = "信息查看";
                        TitleBarText = "信息详细";
                        btnOK.Visible = false;
                        btnCancel.Text = "返回";

                        txtAddress.ReadOnly = true;
                        txtCompanyID.ReadOnly = true;
                        txtCreditRecord.ReadOnly = true;
                        txtDisplayName.ReadOnly = true;
                        txtFoodSafeRecord.ReadOnly = true;
                        txtFullName.ReadOnly = true;
                        txtIncorporator.ReadOnly = true;
                        txtLinkInfo.ReadOnly = true;
                        txtLinkMan.ReadOnly = true;
                        txtOtherCodeInfo.ReadOnly = true;
                        txtOtherInfo.ReadOnly = true;
                        txtPostCode.ReadOnly = true;
                        txtProductInfo.ReadOnly = true;
                        txtRegCapital.ReadOnly = true;
                        txtRemark.ReadOnly = true;
                        txtShortName.ReadOnly = true;
                        txtUnit.ReadOnly = true;
                        txtComProperty.ReadOnly = true;
                        cmbCheckLevel.Enabled = false;
                        cmbCompanyKind.Enabled = false;
                        cmbCreditLevel.Enabled = false;
                        chkLock.Enabled = false;
                        //button1.Enabled = false;

                        chkReadOnly.Visible = false;
                        chkLock.Visible = false;
                    }


                    txtCode.Location = new System.Drawing.Point(130,54);
                    txtCode.Enabled = false;
                    label11.Location = new System.Drawing.Point(325, 54);
                    Text = "被检单位" + title;//商户
                    //label29.Location = new System.Drawing.Point(31, 7);
                    label27.Text = "营业执照号：";//label29.Text +
                    label8.Text = "单位类别：";
                    label10.Text = "单位全称：";

                    //this.label27.Location = new System.Drawing.Point(44, 31);
                }
                else //如果是生产单位/供应商
                {
                    if (tag.IndexOf("EDIT") >= 0)
                    {
                        title = "修改";
						TitleBarText = "修改";
                    }
                    Text = ShareOption.ProductionUnitNameTag + title;// "生产单位"


                    this.ClientSize = new System.Drawing.Size(566, 639);
                    this.txtCreditRecord.Size = new System.Drawing.Size(424, 21);
                    this.txtProductInfo.Size = new System.Drawing.Size(424, 21);
                    this.txtFoodSafeRecord.Size = new System.Drawing.Size(424, 21);
                    this.txtOtherInfo.Size = new System.Drawing.Size(424, 21);
                    this.txtComProperty.Size = new System.Drawing.Size(424, 21);
                    this.txtRemark.Size = new System.Drawing.Size(424, 21);

                    label27.Text = "单位编号:";
                    label27.Visible = false;
                    txtCode.Visible = false;
                    txtCode.Location = new System.Drawing.Point(130, 54);
                    label27.Location = new System.Drawing.Point(325, 54);

                    label11.Text = "营业执照:";
                    //label11.Location = new System.Drawing.Point(28, 31);
                    label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    txtCompanyID.Location = new System.Drawing.Point(130, 54);
                    int le = label8.Right ;
                    label11.Left = 60;

                    label28.Visible = false;
                    lblArea.Visible = false;
                    cmbDistrict.Visible = false;
                    cmbCompanyKind.Enabled = true;
                    label8.Text = "单位类别：";
                    label10.Text = "单位名称：";
                    lblDomainTitle.Text = "单位信息：";
                    label2.Left = lblArea.Left;
                    cmbCheckLevel.Left = cmbDistrict.Left;

                }

                //////////////////////隐藏经营户/门牌号
                int tt0 = label10.Top; 
                label11.Top = tt0;             
                label27.Top = tt0;

                this.label29.Location = new System.Drawing.Point(34, 30);
                label29.Top = tt0;

                txtCompanyID.Top = tt0;
                label26.Top = tt0;
                txtOtherCodeInfo.Top = tt0;
                int tt1 = lblDomainTitle.Top;
                label10.Top = tt1;
                txtFullName.Top = tt1;

                label39.Top = tt1;
                //没有经营户
                lblDomainTitle.Visible = false;
                txtDisplayName.Visible = false;
            }

            clsCheckLevelOpr unitOpr4 = new clsCheckLevelOpr();
            DataTable dt4 = unitOpr4.GetAsDataTable("IsLock=false", "CheckLevel");
            cmbCheckLevel.DataSource = dt4;
            cmbCheckLevel.DataMember = "CheckLevel";
            cmbCheckLevel.Columns["CheckLevel"].Caption = "监控级别";

            clsCreditLevelOpr unitOpr3 = new clsCreditLevelOpr();
            DataTable dt3 = unitOpr3.GetAsDataTable("IsLock=false", "CreditLevel");
            cmbCreditLevel.DataSource = dt3;
            cmbCreditLevel.DataMember = "CreditLevel";
            cmbCreditLevel.Columns["CreditLevel"].Caption = "信用等级";

            clsCompanyPropertyOpr unitOpr5 = new clsCompanyPropertyOpr();
            DataTable dtCompanyProperty = null;
            if (tag.IndexOf("CHECKED") >= 0)
            {
                dtCompanyProperty = unitOpr5.GetAsDataTable(string.Format("IsLock=false AND (CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0, ShareOption.CompanyProperty1), "CompanyProperty");
            }
            else 
            {
                dtCompanyProperty = unitOpr5.GetAsDataTable(string.Format("IsLock=false AND (CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0, ShareOption.CompanyProperty2), "CompanyProperty");
            }

            if (dtCompanyProperty != null)
            {
                cmbCompanyProperty.DataSource = dtCompanyProperty.DataSet;
                cmbCompanyProperty.DataMember = "CompanyProperty";
                cmbCompanyProperty.Columns["CompanyProperty"].Caption = "单位性质";
            }

            clsDistrictOpr unitOpr = new clsDistrictOpr();
            DataTable dtDistrict = unitOpr.GetAsDataTable("IsLock=false And  SysCode Not In (SELECT SysCode FROM tDistrict AS A Where Exists (SELECT SysCode From tDistrict  WHERE LEFT(SysCode, LEN(A.SysCode)) = A.SysCode AND SysCode <> A.SysCode))", "SysCode", 1);
            cmbDistrict.DataSource = dtDistrict.DataSet;
            cmbDistrict.DataMember = "District";
            cmbDistrict.DisplayMember = "Name";
            cmbDistrict.ValueMember = "SysCode";
            cmbDistrict.Columns["StdCode"].Caption = "编号";
            cmbDistrict.Columns["Name"].Caption = "地区";
            cmbDistrict.Columns["SysCode"].Caption = "系统编号";
            cmbDistrict.LeftCol = 1;

            if (!codeValue.Equals(""))
            {
                cmbDistrict.Text = clsDistrictOpr.NameFromCode(codeValue);
                cmbDistrict.SelectedValue = codeValue;
            }
            bindCompanyKind(cmbCompanyProperty.Text.Trim());

            if (!kindValue.Equals("") && dtblCompanyKind != null)
            {
                cmbCompanyKind.SelectedValue = kindValue;
                for (int i = 0; i < dtblCompanyKind.Rows.Count; i++)
                {
                    if (kindValue.Equals(dtblCompanyKind.Rows[i]["SysCode"].ToString()))
                    {
                        cmbCompanyKind.Text = dtblCompanyKind.Rows[i]["Name"].ToString();
                        break;
                    }
                }
            }
         if (tag.IndexOf("EDIT") > 0)//如果是修改的情况
            {
                txtCompanyID.Text = IdCode;
            }
            IsLoading = false;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="model"></param>
        internal void setValue(clsCompany model)
        {
            txtSysID.Text = model.SysCode;

            txtCode.Text = model.StdCode;

            IdCode = model.CompanyID;

            textBox2.Text = model.ISSUEAGENCY;
            textBox3.Text = model.ISSUEDATE;
            textBox4.Text = model.PERIODSTART;
            textBox5.Text = model.PERIODEND;
            textBox6.Text = model.VIOLATENUM;
            textBox7.Text = model.LONGITUDE;
            textBox8.Text = model.LATITUDE;
            textBox9.Text = model.SCOPE;
            textBox10.Text = model.PUNISH; 

            txtOtherCodeInfo.Text = model.OtherCodeInfo;
            txtFullName.Text = model.FullName;
            txtShortName.Text = model.ShortName;
            txtDisplayName.Text = model.DisplayName;
            txtKey.Text = model.ShortCut;
            cmbCompanyProperty.Text = model.Property;
            kindValue = model.KindCode;
            if (model.RegCapital == 0)
            {
                txtRegCapital.Text = string.Empty;
            }
            else
            {
                txtRegCapital.Text = model.RegCapital.ToString();
            }
            txtUnit.Text = model.Unit;
            txtIncorporator.Text = model.Incorporator;

            DateTime? tempdt = model.RegDate;
            if (tempdt != null)
            {
                //dtpRegDate.Value = ;
                txtRegDate.Text = Convert.ToDateTime(tempdt).ToString("yyyy-MM-dd");
            }
            codeValue = model.DistrictCode;
            txtAddress.Text = model.Address;
            txtPostCode.Text = model.PostCode;
            txtLinkMan.Text = model.LinkMan;
            txtLinkInfo.Text = model.LinkInfo;
            cmbCreditLevel.Text = model.CreditLevel;
            txtCreditRecord.Text = model.CreditRecord;
            txtProductInfo.Text = model.ProductInfo;
            txtOtherInfo.Text = model.OtherInfo;
            cmbCheckLevel.Text = model.CheckLevel;
            txtFoodSafeRecord.Text = model.FoodSafeRecord;
            txtRemark.Text = model.Remark;
            chkLock.Checked = model.IsLock;
            chkReadOnly.Checked = model.IsReadOnly;
            txtComProperty.Text = model.ComProperty;
        }
      
        /// <summary>
        /// 绑定单位类别
        /// </summary>
        /// <param name="companyProperty"></param>
        private void bindCompanyKind(string companyProperty)
        {
            string temp = "ISLOCK=FALSE AND ISREADONLY=TRUE";
            if (!string.IsNullOrEmpty(companyProperty))
            {
                temp += string.Format(" AND(CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0,companyProperty);
            }
            clsCompanyKindOpr unitBll = new clsCompanyKindOpr();
            dtblCompanyKind = unitBll.GetAsDataTable(temp, "SysCode", 1);//需要加上一个单位性质..........
            if (dtblCompanyKind != null)
            {
                cmbCompanyKind.DataSource = dtblCompanyKind.DataSet;
                cmbCompanyKind.DataMember = "CompanyKind";
                cmbCompanyKind.DisplayMember = "Name";
                cmbCompanyKind.ValueMember = "SysCode";
                cmbCompanyKind.Columns["StdCode"].Caption = "编号";
                cmbCompanyKind.Columns["Name"].Caption = "单位类别";
                cmbCompanyKind.Columns["SysCode"].Caption = "系统编号";

                cmbCompanyKind.LeftCol = 1;
                cmbCompanyKind.AllowColMove = false;
                cmbCompanyKind.HScrollBar.Style = C1.Win.C1List.ScrollBarStyleEnum.None;
                cmbCompanyKind.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended;
            }
        }

       
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //2016年2月24日 wenj
            //供应商是没有营业执照的，故不验证营业执照
            string companyId = txtCompanyID.Text.Trim();
            //if (companyId == "")
            //{
            //    MessageBox.Show(this, "营业执照"+ "必须输入!");
            //    txtCompanyID.Focus();
            //    return;
            //}
            if (txtFullName.Text.Equals(""))
            {
                MessageBox.Show(this, label10.Text + "必须输入!");
                txtFullName.Focus();
                return;
            }
            if (tag.IndexOf("CHECKED") >= 0) //被检测单位
            {
                if (cmbDistrict.SelectedValue == null)
                {
                    MessageBox.Show(this, lblArea.Text + "必须选择!");
                    cmbDistrict.Focus();
                    return;
                }
            }

            //检查单位编码是否唯一
            //if ((Tag.ToString().Equals("CHECKEDADD") || Tag.ToString().Equals("CHECKEDEDIT")) && txtCode.Text.Length == ShareOption.CompanyStdCodeLen * 2)

            if (tag.IndexOf("CHECKED") >= 0)//被检测单位
            {
                // if (txtCode.Text.Length == ShareOption.CompanyStdCodeLen * 2)
                if (clsCompanyOpr.CompanyIsExist("CompanyID ='" + companyId + "'") && IdCode != companyId)
                {
                    MessageBox.Show(this, "营业执照号已存在!");
                    txtCompanyID.Focus();
                    return;
                }
            }
            else
            {
                if (clsCompanyOpr.CompanyIsExist("CompanyID ='" + companyId + "'") && IdCode != companyId)
                {
                    MessageBox.Show(this, "单位编号已存在!");
                    txtCompanyID.Focus();
                    return;
                }
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
            if (!txtRegCapital.Text.Trim().Equals(""))
            {
                if (!StringUtil.IsNumeric(txtRegCapital.Text.Trim()))
                {
                    MessageBox.Show(this, "注册资金必须为数字!");
                    txtRegCapital.Focus();
                    return;
                }
            }
            if (!txtRegDate.Text.Trim().Equals(""))
            {
                DateTime dti = new DateTime();
                if (DateTime.TryParse(txtRegDate.Text.Trim(), out  dti))
                {
                    if (txtRegDate.Text.Trim().Length >=8)
                    {
                    dti = Convert.ToDateTime(txtRegDate.Text);
                    }
                    if (txtRegDate.Text.Trim().Length <=7)
                    {
                        MessageBox.Show(this, "注册时间无效，请重新填写!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "注册时间无效，请重新填写!");
                    return;
                }
            }

            //取值
            companyModel = new clsCompany();
            string scode = txtSysID.Text.Trim();
            string tcode = scode ;
            string skind = kindValue;
            //if (cmbCompanyKind.SelectedValue == null)
            //{
            //    companyModel.SysCode = scode;
            //}
            //else
            if(cmbCompanyKind.SelectedValue!=null)
            {
                skind = cmbCompanyKind.SelectedValue.ToString();
            }
            if (tag.IndexOf("ADD") >= 0)
            {
                string sErr2 = string.Empty;
                int max = companyBll.GetMaxNO(scode.Substring(0, 9) + skind, ShareOption.CompanyCodeLen, out sErr2);
                tcode = scode.Substring(0, 9) + skind + (max + 1).ToString().PadLeft(ShareOption.CompanyCodeLen, '0');
            }
            else
            {
                tcode = scode.Substring(0, 9) + skind + scode.Substring(scode.Length - ShareOption.CompanyCodeLen, ShareOption.CompanyCodeLen);
            }

            companyModel.SysCode = tcode;
            companyModel.StdCode = tcode;// txtCode.Text.Trim();
            companyModel.CAllow = textBox1.Text.Trim () ;
            companyModel.CompanyID = txtCompanyID.Text.Trim();
            companyModel.OtherCodeInfo = txtOtherCodeInfo.Text.Trim();
            companyModel.FullName = txtFullName.Text.Trim();
            companyModel.ShortName = txtShortName.Text.Trim();
            companyModel.DisplayName = txtDisplayName.Text.Trim();
            companyModel.ShortCut = txtKey.Text.Trim();
            companyModel.Property = cmbCompanyProperty.Text.Trim();
            companyModel.KindCode = skind;
            if (txtRegCapital.Text.Trim().Equals(""))
            {
                companyModel.RegCapital = 0;
            }
            else
            {
                companyModel.RegCapital = Convert.ToInt64(txtRegCapital.Text.Trim());
            }
            companyModel.Unit = txtUnit.Text.Trim();
            companyModel.Incorporator = txtIncorporator.Text.Trim();
            string regDate = txtRegDate.Text;
            if (!string.IsNullOrEmpty(regDate))
            {
                companyModel.RegDate = Convert.ToDateTime(regDate);
            }
            if (cmbDistrict.SelectedValue == null)
            {
                companyModel.DistrictCode = "001";
            }
            else
            {
                companyModel.DistrictCode = cmbDistrict.SelectedValue.ToString();
            }
            companyModel.Address = txtAddress.Text.Trim();
            companyModel.PostCode = txtPostCode.Text.Trim();
            companyModel.LinkMan = txtLinkMan.Text.Trim();
            companyModel.LinkInfo = txtLinkInfo.Text.Trim();
            companyModel.CreditLevel = cmbCreditLevel.Text.Trim();
            companyModel.CreditRecord = txtCreditRecord.Text.Trim();
            companyModel.ProductInfo = txtProductInfo.Text.Trim();
            companyModel.OtherInfo = txtOtherInfo.Text.Trim();
            companyModel.CheckLevel = cmbCheckLevel.Text.Trim();
            companyModel.FoodSafeRecord = txtFoodSafeRecord.Text.Trim();
            companyModel.Remark = txtRemark.Text.Trim();
            companyModel.IsLock = chkLock.Checked;
            companyModel.IsReadOnly = chkReadOnly.Checked;
            companyModel.ComProperty = txtComProperty.Text.Trim();
            companyModel.TSign = "本地增";

            //对数据库进行操作
            string errMsg = string.Empty;
            //if (Tag.ToString().Substring(Tag.ToString().Length - 3, 3).Equals("ADD"))
            if (tag.IndexOf("ADD") >= 0)
            {
                companyBll.Insert(companyModel, out errMsg);
            }
            else //if (Tag.ToString().Substring(Tag.ToString().Length - 4, 4).Equals("EDIT"))
            {
                companyBll.UpdatePart(companyModel, scode, out errMsg);
            }
            if (tag.IndexOf("CHECKED") >= 0)
            {
                FrmMain.IsLoadCheckedComSel = false;
            }
            if (!errMsg.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！");
            }

            //退出
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void cmbCompanyKind_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbCompanyProperty.Text.Trim().Equals(""))
            {
                cmbCompanyKind.CloseCombo();
                cmbCompanyKind.DataSource = null;
            }
            else
            {
                bindCompanyKind(cmbCompanyProperty.Text.Trim());
                //clsCompanyKindOpr unitOpr2 = new clsCompanyKindOpr();
                //DataTable dt2 = unitOpr2.GetAsDataTable("IsLock=false and CompanyProperty='"
                //    + cmbCompanyProperty.Text.Trim() + "' And IsReadOnly=true", "SysCode", 1);
                //cmbCompanyKind.DataSource = dt2.DataSet;
                //cmbCompanyKind.DataMember = "CompanyKind";
                //cmbCompanyKind.DisplayMember = "Name";
                //cmbCompanyKind.ValueMember = "SysCode";
                //cmbCompanyKind.Columns["StdCode"].Caption = "编号";
                //cmbCompanyKind.Columns["Name"].Caption = "单位类别";
                //cmbCompanyKind.Columns["SysCode"].Caption = "系统编号";

            }
        }

        private void cmbDistrict_TextChanged(object sender, System.EventArgs e)
        {
            ComboTextChanged((C1.Win.C1List.C1Combo)sender, "Name", IsLoading);
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
        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtRegDate.Text = dtpRegDate.Value.ToString("yyyy-MM-dd");
        }
        private void dtpProduceDate_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegDate.Text))
            {
                txtRegDate.Text = dtpRegDate.Value.ToString("yyyy-MM-dd");
            }
        }

        private void monthCalendar1_VisibleChanged(object sender, EventArgs e)
        {
            if (CBOYear != "" && CBOMouth!= "" && CBODay != "")
            {
                txtRegDate.Text = CBOYear + "-" + CBOMouth + "-" + CBODay;
            }
        }

    }
}
