using System;
using System.Data;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// 被检测单位/生产单位 增加或者修改
    /// </summary>
    public class frmCheckedComEdit : System.Windows.Forms.Form
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
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblArea;
        private System.ComponentModel.Container components = null;
        private TextBox txtRegDate;
        #endregion

        private clsCompany _companyModel;
        private readonly clsCompanyOpr _companyBll;
        private string _IdCode = string.Empty;
        private string _codeValue;
        private string _kindValue = string.Empty;
        private bool _IsLoading = true;
        private string _tag;
        private DataTable _dtblCompanyKind = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmCheckedComEdit(clsCompany model, string distCode)
        {
            InitializeComponent();
            _codeValue = distCode;
            _companyBll = new clsCompanyOpr();
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
            C1.Win.C1List.Style style41 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style42 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style43 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style44 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style45 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckedComEdit));
            C1.Win.C1List.Style style46 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style47 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style48 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style49 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style50 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style51 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style52 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style53 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style54 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style55 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style56 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style57 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style58 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style59 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style60 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style61 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style62 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style63 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style64 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style65 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style66 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style67 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style68 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style69 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style70 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style71 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style72 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style73 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style74 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style75 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style76 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style77 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style78 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style79 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style80 = new C1.Win.C1List.Style();
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
            this.label29 = new System.Windows.Forms.Label();
            this.txtRegDate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDistrict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreditLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLinkInfo
            // 
            this.txtLinkInfo.Location = new System.Drawing.Point(392, 232);
            this.txtLinkInfo.Name = "txtLinkInfo";
            this.txtLinkInfo.Size = new System.Drawing.Size(150, 21);
            this.txtLinkInfo.TabIndex = 17;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(288, 232);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(96, 21);
            this.label25.TabIndex = 52;
            this.label25.Text = "联系方式：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.Location = new System.Drawing.Point(518, 168);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.Size = new System.Drawing.Size(24, 21);
            this.dtpRegDate.TabIndex = 12;
            this.dtpRegDate.Value = new System.DateTime(2000, 1, 1, 17, 42, 0, 0);
            this.dtpRegDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            this.dtpRegDate.DropDown += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            // 
            // txtFoodSafeRecord
            // 
            this.txtFoodSafeRecord.Location = new System.Drawing.Point(120, 362);
            this.txtFoodSafeRecord.Name = "txtFoodSafeRecord";
            this.txtFoodSafeRecord.Size = new System.Drawing.Size(424, 21);
            this.txtFoodSafeRecord.TabIndex = 22;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(16, 362);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 21);
            this.label20.TabIndex = 43;
            this.label20.Text = "安全记录：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOtherInfo
            // 
            this.txtOtherInfo.Location = new System.Drawing.Point(120, 392);
            this.txtOtherInfo.Name = "txtOtherInfo";
            this.txtOtherInfo.Size = new System.Drawing.Size(424, 21);
            this.txtOtherInfo.TabIndex = 21;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(16, 392);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 21);
            this.label21.TabIndex = 54;
            this.label21.Text = "卫生许可证号：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductInfo
            // 
            this.txtProductInfo.Location = new System.Drawing.Point(120, 329);
            this.txtProductInfo.Name = "txtProductInfo";
            this.txtProductInfo.Size = new System.Drawing.Size(424, 21);
            this.txtProductInfo.TabIndex = 20;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(16, 329);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 21);
            this.label22.TabIndex = 42;
            this.label22.Text = "产品信息：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreditRecord
            // 
            this.txtCreditRecord.Location = new System.Drawing.Point(120, 296);
            this.txtCreditRecord.Name = "txtCreditRecord";
            this.txtCreditRecord.Size = new System.Drawing.Size(424, 21);
            this.txtCreditRecord.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(16, 296);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 21);
            this.label23.TabIndex = 53;
            this.label23.Text = "信用记录：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(16, 264);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(96, 21);
            this.label24.TabIndex = 41;
            this.label24.Text = "信用等级：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Location = new System.Drawing.Point(120, 232);
            this.txtLinkMan.MaxLength = 50;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(150, 21);
            this.txtLinkMan.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(16, 232);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 21);
            this.label12.TabIndex = 40;
            this.label12.Text = "联系人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(392, 200);
            this.txtPostCode.MaxLength = 50;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(150, 21);
            this.txtPostCode.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(288, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 21);
            this.label13.TabIndex = 51;
            this.label13.Text = "邮编：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(16, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 21);
            this.label14.TabIndex = 39;
            this.label14.Text = "详细地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 200);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(150, 21);
            this.txtAddress.TabIndex = 14;
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(315, 107);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(69, 17);
            this.lblArea.TabIndex = 38;
            this.lblArea.Text = "所属组织：";
            this.lblArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(288, 168);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 21);
            this.label16.TabIndex = 50;
            this.label16.Text = "注册时间：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIncorporator
            // 
            this.txtIncorporator.Location = new System.Drawing.Point(120, 168);
            this.txtIncorporator.MaxLength = 50;
            this.txtIncorporator.Name = "txtIncorporator";
            this.txtIncorporator.Size = new System.Drawing.Size(150, 21);
            this.txtIncorporator.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(16, 168);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 21);
            this.label17.TabIndex = 37;
            this.label17.Text = "法人：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(392, 136);
            this.txtUnit.MaxLength = 10;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(150, 21);
            this.txtUnit.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(288, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 21);
            this.label18.TabIndex = 49;
            this.label18.Text = "资金单位：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRegCapital
            // 
            this.txtRegCapital.Location = new System.Drawing.Point(120, 136);
            this.txtRegCapital.MaxLength = 20;
            this.txtRegCapital.Name = "txtRegCapital";
            this.txtRegCapital.Size = new System.Drawing.Size(152, 21);
            this.txtRegCapital.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(16, 136);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 21);
            this.label19.TabIndex = 36;
            this.label19.Text = "注册资金：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 564);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 30;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(232, 514);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 29;
            this.chkReadOnly.Text = "已审核";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(304, 514);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 25;
            this.chkLock.Text = "停用";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 446);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 44;
            this.label1.Text = "备注：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(120, 447);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(424, 56);
            this.txtRemark.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(317, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 55;
            this.label2.Text = "监控级别：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(240, 569);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(32, 21);
            this.txtKey.TabIndex = 3;
            this.txtKey.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(162, 574);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 21);
            this.label3.TabIndex = 46;
            this.label3.Text = "快捷编码：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(120, 40);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(424, 21);
            this.txtFullName.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(34, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 17);
            this.label10.TabIndex = 33;
            this.label10.Text = "单位名称：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(67, 531);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(32, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.Visible = false;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(18, 535);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 21);
            this.label11.TabIndex = 31;
            this.label11.Text = "编号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Visible = false;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(67, 559);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(48, 21);
            this.txtSysID.TabIndex = 28;
            this.txtSysID.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(464, 514);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(376, 514);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 21);
            this.label8.TabIndex = 48;
            this.label8.Text = "单位类别：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(-1, 504);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 21);
            this.label7.TabIndex = 35;
            this.label7.Text = "单位性质：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(241, 542);
            this.txtShortName.MaxLength = 50;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(40, 21);
            this.txtShortName.TabIndex = 5;
            this.txtShortName.Visible = false;
            // 
            // lblDomainTitle
            // 
            this.lblDomainTitle.Location = new System.Drawing.Point(-2, 72);
            this.lblDomainTitle.Name = "lblDomainTitle";
            this.lblDomainTitle.Size = new System.Drawing.Size(116, 17);
            this.lblDomainTitle.TabIndex = 34;
            this.lblDomainTitle.Text = "档口/店面/车牌号：";
            this.lblDomainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(120, 72);
            this.txtDisplayName.MaxLength = 50;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(424, 21);
            this.txtDisplayName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(162, 541);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 47;
            this.label5.Text = "单位简称：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // txtCompanyID
            // 
            this.txtCompanyID.Location = new System.Drawing.Point(120, 8);
            this.txtCompanyID.MaxLength = 50;
            this.txtCompanyID.Name = "txtCompanyID";
            this.txtCompanyID.Size = new System.Drawing.Size(150, 21);
            this.txtCompanyID.TabIndex = 1;
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.AddItemSeparator = ';';
            this.cmbDistrict.Caption = "";
            this.cmbDistrict.CaptionHeight = 17;
            this.cmbDistrict.CaptionStyle = style41;
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
            this.cmbDistrict.EvenRowStyle = style42;
            this.cmbDistrict.FooterStyle = style43;
            this.cmbDistrict.GapHeight = 2;
            this.cmbDistrict.HeadingStyle = style44;
            this.cmbDistrict.HighLightRowStyle = style45;
            this.cmbDistrict.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbDistrict.Images"))));
            this.cmbDistrict.ItemHeight = 15;
            this.cmbDistrict.Location = new System.Drawing.Point(392, 104);
            this.cmbDistrict.MatchEntryTimeout = ((long)(2000));
            this.cmbDistrict.MaxDropDownItems = ((short)(5));
            this.cmbDistrict.MaxLength = 50;
            this.cmbDistrict.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.OddRowStyle = style46;
            this.cmbDistrict.PartialRightColumn = false;
            this.cmbDistrict.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbDistrict.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbDistrict.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbDistrict.SelectedStyle = style47;
            this.cmbDistrict.Size = new System.Drawing.Size(150, 22);
            this.cmbDistrict.Style = style48;
            this.cmbDistrict.TabIndex = 13;
            this.cmbDistrict.TextChanged += new System.EventHandler(this.cmbDistrict_TextChanged);
            this.cmbDistrict.PropBag = resources.GetString("cmbDistrict.PropBag");
            // 
            // cmbCompanyKind
            // 
            this.cmbCompanyKind.AddItemSeparator = ';';
            this.cmbCompanyKind.Caption = "";
            this.cmbCompanyKind.CaptionHeight = 17;
            this.cmbCompanyKind.CaptionStyle = style49;
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
            this.cmbCompanyKind.EvenRowStyle = style50;
            this.cmbCompanyKind.FooterStyle = style51;
            this.cmbCompanyKind.GapHeight = 2;
            this.cmbCompanyKind.HeadingStyle = style52;
            this.cmbCompanyKind.HighLightRowStyle = style53;
            this.cmbCompanyKind.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCompanyKind.Images"))));
            this.cmbCompanyKind.ItemHeight = 15;
            this.cmbCompanyKind.Location = new System.Drawing.Point(120, 104);
            this.cmbCompanyKind.MatchEntryTimeout = ((long)(2000));
            this.cmbCompanyKind.MaxDropDownItems = ((short)(5));
            this.cmbCompanyKind.MaxLength = 50;
            this.cmbCompanyKind.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCompanyKind.Name = "cmbCompanyKind";
            this.cmbCompanyKind.OddRowStyle = style54;
            this.cmbCompanyKind.PartialRightColumn = false;
            this.cmbCompanyKind.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCompanyKind.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCompanyKind.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCompanyKind.SelectedStyle = style55;
            this.cmbCompanyKind.Size = new System.Drawing.Size(150, 22);
            this.cmbCompanyKind.Style = style56;
            this.cmbCompanyKind.TabIndex = 8;
            this.cmbCompanyKind.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbCompanyKind_BeforeOpen);
            this.cmbCompanyKind.PropBag = resources.GetString("cmbCompanyKind.PropBag");
            // 
            // cmbCompanyProperty
            // 
            this.cmbCompanyProperty.AddItemSeparator = ';';
            this.cmbCompanyProperty.Caption = "";
            this.cmbCompanyProperty.CaptionHeight = 17;
            this.cmbCompanyProperty.CaptionStyle = style57;
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
            this.cmbCompanyProperty.EvenRowStyle = style58;
            this.cmbCompanyProperty.FooterStyle = style59;
            this.cmbCompanyProperty.GapHeight = 2;
            this.cmbCompanyProperty.HeadingStyle = style60;
            this.cmbCompanyProperty.HighLightRowStyle = style61;
            this.cmbCompanyProperty.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCompanyProperty.Images"))));
            this.cmbCompanyProperty.ItemHeight = 15;
            this.cmbCompanyProperty.Location = new System.Drawing.Point(68, 503);
            this.cmbCompanyProperty.MatchEntryTimeout = ((long)(2000));
            this.cmbCompanyProperty.MaxDropDownItems = ((short)(5));
            this.cmbCompanyProperty.MaxLength = 10;
            this.cmbCompanyProperty.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCompanyProperty.Name = "cmbCompanyProperty";
            this.cmbCompanyProperty.OddRowStyle = style62;
            this.cmbCompanyProperty.PartialRightColumn = false;
            this.cmbCompanyProperty.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCompanyProperty.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.SelectedStyle = style63;
            this.cmbCompanyProperty.Size = new System.Drawing.Size(48, 22);
            this.cmbCompanyProperty.Style = style64;
            this.cmbCompanyProperty.TabIndex = 7;
            this.cmbCompanyProperty.Visible = false;
            this.cmbCompanyProperty.PropBag = resources.GetString("cmbCompanyProperty.PropBag");
            // 
            // cmbCreditLevel
            // 
            this.cmbCreditLevel.AddItemSeparator = ';';
            this.cmbCreditLevel.Caption = "";
            this.cmbCreditLevel.CaptionHeight = 17;
            this.cmbCreditLevel.CaptionStyle = style65;
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
            this.cmbCreditLevel.EvenRowStyle = style66;
            this.cmbCreditLevel.FooterStyle = style67;
            this.cmbCreditLevel.GapHeight = 2;
            this.cmbCreditLevel.HeadingStyle = style68;
            this.cmbCreditLevel.HighLightRowStyle = style69;
            this.cmbCreditLevel.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCreditLevel.Images"))));
            this.cmbCreditLevel.ItemHeight = 15;
            this.cmbCreditLevel.Location = new System.Drawing.Point(120, 264);
            this.cmbCreditLevel.MatchEntryTimeout = ((long)(2000));
            this.cmbCreditLevel.MaxDropDownItems = ((short)(5));
            this.cmbCreditLevel.MaxLength = 10;
            this.cmbCreditLevel.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCreditLevel.Name = "cmbCreditLevel";
            this.cmbCreditLevel.OddRowStyle = style70;
            this.cmbCreditLevel.PartialRightColumn = false;
            this.cmbCreditLevel.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCreditLevel.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCreditLevel.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCreditLevel.SelectedStyle = style71;
            this.cmbCreditLevel.Size = new System.Drawing.Size(150, 22);
            this.cmbCreditLevel.Style = style72;
            this.cmbCreditLevel.TabIndex = 18;
            this.cmbCreditLevel.PropBag = resources.GetString("cmbCreditLevel.PropBag");
            // 
            // cmbCheckLevel
            // 
            this.cmbCheckLevel.AddItemSeparator = ';';
            this.cmbCheckLevel.Caption = "";
            this.cmbCheckLevel.CaptionHeight = 17;
            this.cmbCheckLevel.CaptionStyle = style73;
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
            this.cmbCheckLevel.EvenRowStyle = style74;
            this.cmbCheckLevel.FooterStyle = style75;
            this.cmbCheckLevel.GapHeight = 2;
            this.cmbCheckLevel.HeadingStyle = style76;
            this.cmbCheckLevel.HighLightRowStyle = style77;
            this.cmbCheckLevel.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckLevel.Images"))));
            this.cmbCheckLevel.ItemHeight = 15;
            this.cmbCheckLevel.Location = new System.Drawing.Point(392, 264);
            this.cmbCheckLevel.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckLevel.MaxDropDownItems = ((short)(5));
            this.cmbCheckLevel.MaxLength = 10;
            this.cmbCheckLevel.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckLevel.Name = "cmbCheckLevel";
            this.cmbCheckLevel.OddRowStyle = style78;
            this.cmbCheckLevel.PartialRightColumn = false;
            this.cmbCheckLevel.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckLevel.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckLevel.SelectedStyle = style79;
            this.cmbCheckLevel.Size = new System.Drawing.Size(150, 22);
            this.cmbCheckLevel.Style = style80;
            this.cmbCheckLevel.TabIndex = 23;
            this.cmbCheckLevel.PropBag = resources.GetString("cmbCheckLevel.PropBag");
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(288, 8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(96, 21);
            this.label26.TabIndex = 32;
            this.label26.Text = "标准编码：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOtherCodeInfo
            // 
            this.txtOtherCodeInfo.Location = new System.Drawing.Point(392, 8);
            this.txtOtherCodeInfo.Name = "txtOtherCodeInfo";
            this.txtOtherCodeInfo.Size = new System.Drawing.Size(150, 21);
            this.txtOtherCodeInfo.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(32, 8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(84, 21);
            this.label27.TabIndex = 45;
            this.label27.Text = "营业执照号：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComProperty
            // 
            this.txtComProperty.Location = new System.Drawing.Point(120, 419);
            this.txtComProperty.Name = "txtComProperty";
            this.txtComProperty.Size = new System.Drawing.Size(424, 21);
            this.txtComProperty.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(41, 421);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 21);
            this.label4.TabIndex = 57;
            this.label4.Text = "区域性质：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(24, 39);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 58;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(311, 103);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(8, 21);
            this.label28.TabIndex = 59;
            this.label28.Text = "*";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(23, 6);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(8, 21);
            this.label29.TabIndex = 60;
            this.label29.Text = "*";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRegDate
            // 
            this.txtRegDate.Location = new System.Drawing.Point(390, 168);
            this.txtRegDate.MaxLength = 10;
            this.txtRegDate.Name = "txtRegDate";
            this.txtRegDate.Size = new System.Drawing.Size(134, 21);
            this.txtRegDate.TabIndex = 61;
            // 
            // frmCheckedComEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(560, 598);
            this.Controls.Add(this.txtRegDate);
            this.Controls.Add(this.label29);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCheckedComEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "被检商户修改";
            this.Load += new System.EventHandler(this.frmCheckedComEdit_Load);
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
            try
            {
                _tag = Tag.ToString();
                string title = "新增";
                string strWhere = string.Empty;
                int top = 0;
                lblArea.Text = ShareOption.AreaTitle + "：";
                lblDomainTitle.Text = ShareOption.DomainTitle + "：";

                if (ShareOption.IsDataLink)//如果是单机版本
                {
                    if (_tag.IndexOf("CHECKED") >= 0)//如果被检单位
                    {
                        if (_tag.IndexOf("EDIT") >= 0)//如果编辑
                        {
                            title = "修改";
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
                            label29.Location = new System.Drawing.Point(31, 7);//设置必填"*"位置
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
                    }
                    else  //生产单位 
                    {
                        if (_tag.IndexOf("EDIT") >= 0)//如果编辑
                        {
                            title = "修改";
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
                    if (_tag.IndexOf("CHECKED") >= 0)//如果被检单位
                    {
                        if (_tag.IndexOf("EDIT") >= 0)
                        {
                            title = "信息查看";
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
                            dtpRegDate.Enabled = false;

                            chkReadOnly.Visible = false;
                            chkLock.Visible = false;
                        }
                        Text = "被检单位" + title;//商户
                        label29.Location = new System.Drawing.Point(31, 7);
                        label27.Text = "营业执照号：";//label29.Text +
                        label8.Text = "单位类别：";
                        label10.Text = "单位全称：";
                    }
                    else //如果是生产单位/供应商
                    {
                        if (_tag.IndexOf("EDIT") >= 0)
                        {
                            title = "修改";
                        }
                        Text = ShareOption.ProductionUnitNameTag + title;// "生产单位"

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
                    label27.Top = tt0;
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
                if (_tag.IndexOf("CHECKED") >= 0)
                    dtCompanyProperty = unitOpr5.GetAsDataTable(string.Format("IsLock=false AND (CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0, ShareOption.CompanyProperty1), "CompanyProperty");
                else
                    dtCompanyProperty = unitOpr5.GetAsDataTable(string.Format("IsLock=false AND (CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0, ShareOption.CompanyProperty2), "CompanyProperty");

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
                if (!_codeValue.Equals(""))
                {
                    cmbDistrict.Text = clsDistrictOpr.NameFromCode(_codeValue);
                    cmbDistrict.SelectedValue = _codeValue;
                }
                bindCompanyKind(cmbCompanyProperty.Text.Trim());
                if (!_kindValue.Equals("") && _dtblCompanyKind != null)
                {
                    cmbCompanyKind.SelectedValue = _kindValue;
                    for (int i = 0; i < _dtblCompanyKind.Rows.Count; i++)
                    {
                        if (_kindValue.Equals(_dtblCompanyKind.Rows[i]["SysCode"].ToString()))
                        {
                            cmbCompanyKind.Text = _dtblCompanyKind.Rows[i]["Name"].ToString();
                            break;
                        }
                    }
                }
                if (_tag.IndexOf("EDIT") > 0)//如果是修改的情况
                    txtCompanyID.Text = _IdCode;
                _IsLoading = false;
                if (!"MAKEADD".Equals(_tag))
                    label26.Text = "身份证号：";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "出现异常，请联系管理员！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="model"></param>
        internal void setValue(clsCompany model)
        {
            txtSysID.Text = model.SysCode;
            txtCode.Text = model.StdCode;
            _IdCode = model.CompanyID;

            txtOtherCodeInfo.Text = model.OtherCodeInfo;
            txtFullName.Text = model.FullName;
            txtShortName.Text = model.ShortName;
            txtDisplayName.Text = model.DisplayName;
            txtKey.Text = model.ShortCut;
            cmbCompanyProperty.Text = model.Property;
            _kindValue = model.KindCode;
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
                dtpRegDate.Value = Convert.ToDateTime(tempdt);
                txtRegDate.Text = dtpRegDate.Value.ToString("yyyy-MM-dd");
            }
            _codeValue = model.DistrictCode;
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
                temp += string.Format(" AND(CompanyProperty='{0}' OR CompanyProperty='{1}')", ShareOption.CompanyProperty0, companyProperty);
            }
            clsCompanyKindOpr unitBll = new clsCompanyKindOpr();
            _dtblCompanyKind = unitBll.GetAsDataTable(temp, "SysCode", 1);//需要加上一个单位性质..........
            if (_dtblCompanyKind != null)
            {
                cmbCompanyKind.DataSource = _dtblCompanyKind.DataSet;
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
            string companyId = txtCompanyID.Text.Trim();
            if (companyId == "")
            {
                MessageBox.Show(this, label27.Text + "必须输入!");
                txtCompanyID.Focus();
                return;
            }
            if (txtFullName.Text.Equals(""))
            {
                MessageBox.Show(this, label10.Text + "必须输入!");
                txtFullName.Focus();
                return;
            }
            if (_tag.IndexOf("CHECKED") >= 0) //被检测单位
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

            if (_tag.IndexOf("CHECKED") >= 0)//被检测单位
            {
                // if (txtCode.Text.Length == ShareOption.CompanyStdCodeLen * 2)
                if (clsCompanyOpr.CompanyIsExist("CompanyID ='" + companyId + "'") && _IdCode != companyId)
                {
                    MessageBox.Show(this, "营业执照号已存在!");
                    txtCompanyID.Focus();
                    return;
                }
            }
            else
            {
                if (clsCompanyOpr.CompanyIsExist("CompanyID ='" + companyId + "'") && _IdCode != companyId)
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

            //取值
            _companyModel = new clsCompany();
            string scode = txtSysID.Text.Trim();
            string tcode = scode;
            string skind = _kindValue;
            //if (cmbCompanyKind.SelectedValue == null)
            //{
            //    companyModel.SysCode = scode;
            //}
            //else
            if (cmbCompanyKind.SelectedValue != null)
            {
                skind = cmbCompanyKind.SelectedValue.ToString();
            }
            if (_tag.IndexOf("ADD") >= 0)
            {
                string sErr2 = string.Empty;
                int max = _companyBll.GetMaxNO(scode.Substring(0, 9) + skind, ShareOption.CompanyCodeLen, out sErr2);
                tcode = scode.Substring(0, 9) + skind + (max + 1).ToString().PadLeft(ShareOption.CompanyCodeLen, '0');
            }
            else
            {
                tcode = scode.Substring(0, 9) + skind + scode.Substring(scode.Length - ShareOption.CompanyCodeLen, ShareOption.CompanyCodeLen);
            }

            _companyModel.SysCode = tcode;
            _companyModel.StdCode = tcode;// txtCode.Text.Trim();
            _companyModel.CompanyID = txtCompanyID.Text.Trim();
            _companyModel.OtherCodeInfo = txtOtherCodeInfo.Text.Trim();
            _companyModel.FullName = txtFullName.Text.Trim();
            _companyModel.ShortName = txtShortName.Text.Trim();
            _companyModel.DisplayName = txtDisplayName.Text.Trim();
            _companyModel.ShortCut = txtKey.Text.Trim();
            _companyModel.Property = cmbCompanyProperty.Text.Trim();
            _companyModel.KindCode = skind;
            if (txtRegCapital.Text.Trim().Equals(""))
            {
                _companyModel.RegCapital = 0;
            }
            else
            {
                _companyModel.RegCapital = Convert.ToInt64(txtRegCapital.Text.Trim());
            }
            _companyModel.Unit = txtUnit.Text.Trim();
            _companyModel.Incorporator = txtIncorporator.Text.Trim();
            string regDate = txtRegDate.Text.Trim();
            DateTime dtDate = new DateTime();
            if (!DateTime.TryParse(regDate, out dtDate))
            {
                MessageBox.Show("注册时间格式输入有误，请重新输入！\n日期格式：2000-01-01");
                return;
            }
            if (!string.IsNullOrEmpty(regDate))
            {
                _companyModel.RegDate = Convert.ToDateTime(regDate);
            }
            if (cmbDistrict.SelectedValue == null)
            {
                _companyModel.DistrictCode = "001";
            }
            else
            {
                _companyModel.DistrictCode = cmbDistrict.SelectedValue.ToString();
            }
            _companyModel.Address = txtAddress.Text.Trim();
            _companyModel.PostCode = txtPostCode.Text.Trim();
            _companyModel.LinkMan = txtLinkMan.Text.Trim();
            _companyModel.LinkInfo = txtLinkInfo.Text.Trim();
            _companyModel.CreditLevel = cmbCreditLevel.Text.Trim();
            _companyModel.CreditRecord = txtCreditRecord.Text.Trim();
            _companyModel.ProductInfo = txtProductInfo.Text.Trim();
            _companyModel.OtherInfo = txtOtherInfo.Text.Trim();
            _companyModel.CheckLevel = cmbCheckLevel.Text.Trim();
            _companyModel.FoodSafeRecord = txtFoodSafeRecord.Text.Trim();
            _companyModel.Remark = txtRemark.Text.Trim();
            _companyModel.IsLock = chkLock.Checked;
            _companyModel.IsReadOnly = chkReadOnly.Checked;
            _companyModel.ComProperty = txtComProperty.Text.Trim();

            //对数据库进行操作
            string errMsg = string.Empty;
            //if (Tag.ToString().Substring(Tag.ToString().Length - 3, 3).Equals("ADD"))
            if (_tag.IndexOf("ADD") >= 0)
            {
                _companyBll.Insert(_companyModel, out errMsg);
            }
            else //if (Tag.ToString().Substring(Tag.ToString().Length - 4, 4).Equals("EDIT"))
            {
                _companyBll.UpdatePart(_companyModel, scode, out errMsg);
            }
            if (_tag.IndexOf("CHECKED") >= 0)
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
            ComboTextChanged((C1.Win.C1List.C1Combo)sender, "Name", _IsLoading);
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
    }
}
