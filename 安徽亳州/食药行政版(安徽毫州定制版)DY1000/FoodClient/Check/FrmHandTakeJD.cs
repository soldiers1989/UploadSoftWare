using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// frmHandTakeJD 的摘要说明。
    /// </summary>
    public class FrmHandTakeJD : TitleBarBase
    {
        #region 窗体控件
        private TextBox txtProduceDate;
        private System.Windows.Forms.TextBox txtSampleNum;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.TextBox txtSampleBaseNum;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnOK;
        private C1.Win.C1List.C1Combo cmbFood;
        private System.Windows.Forms.Label lblResult;
        private C1.Win.C1List.C1Combo cmbCheckUnit;
        private System.Windows.Forms.Label lblReferStandard;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label labelCheckUnit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTakeDate;
        private System.Windows.Forms.DateTimePicker dtpCheckStartDate;
        private C1.Win.C1List.C1Combo cmbChecker;
        private System.Windows.Forms.Label lblSuppresser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSampleLevel;
        private System.Windows.Forms.TextBox txtSampleState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelSendCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private C1.Win.C1List.C1Combo cmbAssessor;
        private C1.Win.C1List.C1Combo cmbOrganizer;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblMachineTag;
        public C1.Win.C1List.C1Combo cmbCheckMachine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkNoHaveMachine;
        private C1.Win.C1List.C1Combo cmbCheckItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpProduceDate;
        private System.Windows.Forms.Label lblProduceTag;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCompanyInfo;
        private C1.Win.C1List.C1Combo cmbResult;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtImportNum;
        private System.Windows.Forms.TextBox txtCheckValueInfo;
        private System.Windows.Forms.TextBox txtSaveNum;
        private System.Windows.Forms.TextBox txtSentCompany;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.TextBox txtSampleCode;
        private C1.Win.C1List.C1Combo cmbProduceCompany;
        private System.Windows.Forms.TextBox txtOrCheckNo;
        private System.Windows.Forms.TextBox txtStdCode;
        private System.Windows.Forms.ComboBox cmbCheckType;
        private System.Windows.Forms.TextBox txtStandValue;
        private System.Windows.Forms.Label label30;
        private C1.Win.C1List.C1Combo cmbProducePlace;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtResultInfo;
        private System.Windows.Forms.Label label32;

        private System.Windows.Forms.TextBox txtCheckPlanCode;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtSaleNum;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmbIsSentCheck;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtCheckerRemark;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox cmbCheckerVal;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDomain;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion
        /// <summary>
        /// true 深圳
        /// </summary>
        private bool _saveType = System.Configuration.ConfigurationManager.AppSettings["ReportsType"].ToString().Equals("SZ") ? true : false;
        private bool _afterAdded = false;
        private string _machineCode = string.Empty;
        private string _standardCode = string.Empty;
        private string _checkItemCode = string.Empty;
        private string _sign = string.Empty;
        private decimal _dTestValue = 0;
        private string _checkUnit = string.Empty;
        private string[,] _checkItems;
        private string _foodSelectedValue = string.Empty;
        private string _produceComSelectedValue = string.Empty;
        /// <summary>
        /// 受检单位
        /// </summary>
        private string _checkedComSelectedValue = string.Empty;
        private string _upperComSelectedValue = string.Empty;
        private string _producePlaceSelectValue = string.Empty;
        private string _sysCode = string.Empty;
        private Label lblPerProduceDateTag;
        private Label lblPerProduceComTag;
        private Label lblPerProduceTag;
        private Label lblPerImportNumTag;
        private Label lblPerSaveNumTag;
        private ComboBox cmbCompany;
        private Button btnSelect;
        private TextBox txtCiname;
        private Label label16;
        private readonly clsResultOpr _resultBll = new clsResultOpr();
        private ComboBox txtSampleUnit;
        private ComboBox txtUnit;
        private TextBox txtSampleAmount;
        private Label label7;
        private TextBox txtSysID;
        private C1.Win.C1List.C1Combo cmbUpperCompany;
        private Label lblParent;
        private Label label9;
        private C1.Win.C1List.C1Combo cmbCheckedCompany;
        private TextBox tb_Phone;
        private Label label17;
        private ComboBox cb_CompanyArea;
        private TextBox tb_Contact;
        private TextBox tb_IsDestruction;
        private TextBox tb_Quality;
        private ComboBox cmbCheckItemAnHui;
        private Label label21;
        private ComboBox cmbFoodType;
        private Label label26;
        private string _wheresql = string.Empty;
        private string _checkItemsCode = string.Empty;
        private TextBox txtSampleModel;
        private TextBox tb_SampleSource;
        private TextBox textFoodType;
        private string _foodType = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmHandTakeJD()
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHandTakeJD));
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
            C1.Win.C1List.Style style41 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style42 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style43 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style44 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style45 = new C1.Win.C1List.Style();
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
            C1.Win.C1List.Style style81 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style82 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style83 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style84 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style85 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style86 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style87 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style88 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style89 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style90 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style91 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style92 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style93 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style94 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style95 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style96 = new C1.Win.C1List.Style();
            this.txtSampleNum = new System.Windows.Forms.TextBox();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.txtSampleBaseNum = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbFood = new C1.Win.C1List.C1Combo();
            this.lblResult = new System.Windows.Forms.Label();
            this.cmbCheckUnit = new C1.Win.C1List.C1Combo();
            this.lblReferStandard = new System.Windows.Forms.Label();
            this.lblSample = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.labelCheckUnit = new System.Windows.Forms.Label();
            this.txtImportNum = new System.Windows.Forms.TextBox();
            this.txtCheckValueInfo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTakeDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbChecker = new C1.Win.C1List.C1Combo();
            this.lblSuppresser = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSampleLevel = new System.Windows.Forms.TextBox();
            this.txtSampleState = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSaveNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSentCompany = new System.Windows.Forms.TextBox();
            this.labelSendCompany = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbAssessor = new C1.Win.C1List.C1Combo();
            this.cmbOrganizer = new C1.Win.C1List.C1Combo();
            this.label35 = new System.Windows.Forms.Label();
            this.lblMachineTag = new System.Windows.Forms.Label();
            this.cmbCheckMachine = new C1.Win.C1List.C1Combo();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkNoHaveMachine = new System.Windows.Forms.CheckBox();
            this.cmbCheckItem = new C1.Win.C1List.C1Combo();
            this.txtSampleCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpProduceDate = new System.Windows.Forms.DateTimePicker();
            this.cmbProduceCompany = new C1.Win.C1List.C1Combo();
            this.lblProduceTag = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtOrCheckNo = new System.Windows.Forms.TextBox();
            this.txtStdCode = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtCompanyInfo = new System.Windows.Forms.TextBox();
            this.cmbCheckType = new System.Windows.Forms.ComboBox();
            this.cmbResult = new C1.Win.C1List.C1Combo();
            this.txtStandValue = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmbProducePlace = new C1.Win.C1List.C1Combo();
            this.label29 = new System.Windows.Forms.Label();
            this.txtResultInfo = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtCheckPlanCode = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtSaleNum = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cmbIsSentCheck = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txtCheckerRemark = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.cmbCheckerVal = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtProduceDate = new System.Windows.Forms.TextBox();
            this.lblPerProduceDateTag = new System.Windows.Forms.Label();
            this.lblPerProduceComTag = new System.Windows.Forms.Label();
            this.lblPerProduceTag = new System.Windows.Forms.Label();
            this.lblPerImportNumTag = new System.Windows.Forms.Label();
            this.lblPerSaveNumTag = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtCiname = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSampleUnit = new System.Windows.Forms.ComboBox();
            this.txtUnit = new System.Windows.Forms.ComboBox();
            this.txtSampleAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.cmbUpperCompany = new C1.Win.C1List.C1Combo();
            this.lblParent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
            this.tb_Phone = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cb_CompanyArea = new System.Windows.Forms.ComboBox();
            this.tb_Contact = new System.Windows.Forms.TextBox();
            this.tb_IsDestruction = new System.Windows.Forms.TextBox();
            this.tb_Quality = new System.Windows.Forms.TextBox();
            this.cmbCheckItemAnHui = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbFoodType = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtSampleModel = new System.Windows.Forms.TextBox();
            this.tb_SampleSource = new System.Windows.Forms.TextBox();
            this.textFoodType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganizer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducePlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUpperCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSampleNum
            // 
            this.txtSampleNum.Location = new System.Drawing.Point(917, 351);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleNum.TabIndex = 40;
            this.txtSampleNum.Validated += new System.EventHandler(this.txtSampleNum_Validated);
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckNo.Location = new System.Drawing.Point(138, 36);
            this.txtCheckNo.MaxLength = 50;
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(150, 26);
            this.txtCheckNo.TabIndex = 20;
            this.txtCheckNo.TextChanged += new System.EventHandler(this.txtCheckNo_TextChanged);
            // 
            // txtSampleBaseNum
            // 
            this.txtSampleBaseNum.Location = new System.Drawing.Point(915, 280);
            this.txtSampleBaseNum.Name = "txtSampleBaseNum";
            this.txtSampleBaseNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleBaseNum.TabIndex = 41;
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(138, 452);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(403, 48);
            this.txtRemark.TabIndex = 60;
            // 
            // txtStandard
            // 
            this.txtStandard.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStandard.Location = new System.Drawing.Point(390, 74);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.Size = new System.Drawing.Size(150, 26);
            this.txtStandard.TabIndex = 24;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(380, 608);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 27);
            this.btnOK.TabIndex = 63;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbFood
            // 
            this.cmbFood.AddItemSeparator = ';';
            this.cmbFood.Caption = "";
            this.cmbFood.CaptionHeight = 17;
            this.cmbFood.CaptionStyle = style1;
            this.cmbFood.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbFood.ColumnCaptionHeight = 18;
            this.cmbFood.ColumnFooterHeight = 18;
            this.cmbFood.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbFood.ContentHeight = 16;
            this.cmbFood.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbFood.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbFood.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFood.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbFood.EditorHeight = 16;
            this.cmbFood.EvenRowStyle = style2;
            this.cmbFood.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFood.FooterStyle = style3;
            this.cmbFood.GapHeight = 2;
            this.cmbFood.HeadingStyle = style4;
            this.cmbFood.HighLightRowStyle = style5;
            this.cmbFood.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbFood.Images"))));
            this.cmbFood.ItemHeight = 15;
            this.cmbFood.Location = new System.Drawing.Point(138, 113);
            this.cmbFood.MatchEntryTimeout = ((long)(2000));
            this.cmbFood.MaxDropDownItems = ((short)(5));
            this.cmbFood.MaxLength = 50;
            this.cmbFood.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbFood.Name = "cmbFood";
            this.cmbFood.OddRowStyle = style6;
            this.cmbFood.PartialRightColumn = false;
            this.cmbFood.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbFood.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbFood.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbFood.SelectedStyle = style7;
            this.cmbFood.Size = new System.Drawing.Size(150, 22);
            this.cmbFood.Style = style8;
            this.cmbFood.TabIndex = 28;
            this.cmbFood.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbFood_BeforeOpen);
            this.cmbFood.PropBag = resources.GetString("cmbFood.PropBag");
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(323, 306);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(76, 21);
            this.lblResult.TabIndex = 86;
            this.lblResult.Text = "结论：";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckUnit
            // 
            this.cmbCheckUnit.AddItemSeparator = ';';
            this.cmbCheckUnit.Caption = "";
            this.cmbCheckUnit.CaptionHeight = 17;
            this.cmbCheckUnit.CaptionStyle = style9;
            this.cmbCheckUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckUnit.ColumnCaptionHeight = 18;
            this.cmbCheckUnit.ColumnFooterHeight = 18;
            this.cmbCheckUnit.ComboStyle = C1.Win.C1List.ComboStyleEnum.SimpleCombo;
            this.cmbCheckUnit.ContentHeight = 16;
            this.cmbCheckUnit.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckUnit.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckUnit.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckUnit.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckUnit.EditorHeight = 16;
            this.cmbCheckUnit.EvenRowStyle = style10;
            this.cmbCheckUnit.FooterStyle = style11;
            this.cmbCheckUnit.GapHeight = 2;
            this.cmbCheckUnit.HeadingStyle = style12;
            this.cmbCheckUnit.HighLightRowStyle = style13;
            this.cmbCheckUnit.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckUnit.Images"))));
            this.cmbCheckUnit.ItemHeight = 15;
            this.cmbCheckUnit.Location = new System.Drawing.Point(138, 230);
            this.cmbCheckUnit.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckUnit.MaxDropDownItems = ((short)(5));
            this.cmbCheckUnit.MaxLength = 10;
            this.cmbCheckUnit.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckUnit.Name = "cmbCheckUnit";
            this.cmbCheckUnit.OddRowStyle = style14;
            this.cmbCheckUnit.PartialRightColumn = false;
            this.cmbCheckUnit.ReadOnly = true;
            this.cmbCheckUnit.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckUnit.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.SelectedStyle = style15;
            this.cmbCheckUnit.Size = new System.Drawing.Size(148, 21);
            this.cmbCheckUnit.Style = style16;
            this.cmbCheckUnit.TabIndex = 57;
            this.cmbCheckUnit.PropBag = resources.GetString("cmbCheckUnit.PropBag");
            // 
            // lblReferStandard
            // 
            this.lblReferStandard.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReferStandard.Location = new System.Drawing.Point(302, 76);
            this.lblReferStandard.Name = "lblReferStandard";
            this.lblReferStandard.Size = new System.Drawing.Size(101, 21);
            this.lblReferStandard.TabIndex = 81;
            this.lblReferStandard.Text = "检测依据：";
            this.lblReferStandard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSample
            // 
            this.lblSample.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSample.Location = new System.Drawing.Point(37, 114);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(104, 21);
            this.lblSample.TabIndex = 56;
            this.lblSample.Text = "样品名称：";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(7, 156);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(134, 21);
            this.lblName.TabIndex = 54;
            this.lblName.Text = "受检人/单位：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(849, 280);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 21);
            this.label22.TabIndex = 69;
            this.label22.Text = "抽样基数：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(26, 452);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(115, 21);
            this.label23.TabIndex = 94;
            this.label23.Text = "处理情况：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(34, 39);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(107, 21);
            this.label25.TabIndex = 49;
            this.label25.Text = "检测编号：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(853, 351);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 21);
            this.label27.TabIndex = 68;
            this.label27.Text = "抽样数量：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCheckUnit
            // 
            this.labelCheckUnit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCheckUnit.Location = new System.Drawing.Point(49, 230);
            this.labelCheckUnit.Name = "labelCheckUnit";
            this.labelCheckUnit.Size = new System.Drawing.Size(93, 21);
            this.labelCheckUnit.TabIndex = 91;
            this.labelCheckUnit.Text = "检测单位：";
            this.labelCheckUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImportNum
            // 
            this.txtImportNum.Location = new System.Drawing.Point(924, 478);
            this.txtImportNum.MaxLength = 50;
            this.txtImportNum.Name = "txtImportNum";
            this.txtImportNum.Size = new System.Drawing.Size(154, 21);
            this.txtImportNum.TabIndex = 42;
            this.txtImportNum.TextChanged += new System.EventHandler(this.txtImportNum_TextChanged);
            this.txtImportNum.Validated += new System.EventHandler(this.txtImportNum_Validated);
            // 
            // txtCheckValueInfo
            // 
            this.txtCheckValueInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckValueInfo.Location = new System.Drawing.Point(138, 263);
            this.txtCheckValueInfo.Name = "txtCheckValueInfo";
            this.txtCheckValueInfo.Size = new System.Drawing.Size(150, 26);
            this.txtCheckValueInfo.TabIndex = 51;
            this.txtCheckValueInfo.TextChanged += new System.EventHandler(this.txtCheckValueInfo_TextChanged);
            this.txtCheckValueInfo.LostFocus += new System.EventHandler(this.txtCheckValueInfo_LostFocus);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(860, 478);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 21);
            this.label13.TabIndex = 71;
            this.label13.Text = "进货数量：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(310, 346);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 21);
            this.label18.TabIndex = 78;
            this.label18.Text = "抽样日期：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(49, 386);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 88;
            this.label1.Text = "检测人：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTakeDate
            // 
            this.dtpTakeDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTakeDate.Location = new System.Drawing.Point(391, 343);
            this.dtpTakeDate.Name = "dtpTakeDate";
            this.dtpTakeDate.Size = new System.Drawing.Size(150, 26);
            this.dtpTakeDate.TabIndex = 49;
            // 
            // dtpCheckStartDate
            // 
            this.dtpCheckStartDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpCheckStartDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCheckStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckStartDate.Location = new System.Drawing.Point(391, 383);
            this.dtpCheckStartDate.Name = "dtpCheckStartDate";
            this.dtpCheckStartDate.Size = new System.Drawing.Size(150, 26);
            this.dtpCheckStartDate.TabIndex = 50;
            this.dtpCheckStartDate.ValueChanged += new System.EventHandler(this.dtpCheckStartDate_DropDown);
            this.dtpCheckStartDate.DropDown += new System.EventHandler(this.dtpCheckStartDate_DropDown);
            // 
            // cmbChecker
            // 
            this.cmbChecker.AddItemSeparator = ';';
            this.cmbChecker.Caption = "";
            this.cmbChecker.CaptionHeight = 17;
            this.cmbChecker.CaptionStyle = style17;
            this.cmbChecker.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbChecker.ColumnCaptionHeight = 18;
            this.cmbChecker.ColumnFooterHeight = 18;
            this.cmbChecker.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbChecker.ContentHeight = 16;
            this.cmbChecker.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbChecker.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbChecker.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbChecker.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbChecker.EditorHeight = 16;
            this.cmbChecker.EvenRowStyle = style18;
            this.cmbChecker.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbChecker.FooterStyle = style19;
            this.cmbChecker.GapHeight = 2;
            this.cmbChecker.HeadingStyle = style20;
            this.cmbChecker.HighLightRowStyle = style21;
            this.cmbChecker.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbChecker.Images"))));
            this.cmbChecker.ItemHeight = 15;
            this.cmbChecker.Location = new System.Drawing.Point(138, 383);
            this.cmbChecker.MatchEntryTimeout = ((long)(2000));
            this.cmbChecker.MaxDropDownItems = ((short)(5));
            this.cmbChecker.MaxLength = 10;
            this.cmbChecker.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbChecker.Name = "cmbChecker";
            this.cmbChecker.OddRowStyle = style22;
            this.cmbChecker.PartialRightColumn = false;
            this.cmbChecker.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbChecker.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbChecker.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbChecker.SelectedStyle = style23;
            this.cmbChecker.Size = new System.Drawing.Size(150, 22);
            this.cmbChecker.Style = style24;
            this.cmbChecker.TabIndex = 55;
            this.cmbChecker.PropBag = resources.GetString("cmbChecker.PropBag");
            // 
            // lblSuppresser
            // 
            this.lblSuppresser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSuppresser.Location = new System.Drawing.Point(37, 266);
            this.lblSuppresser.Name = "lblSuppresser";
            this.lblSuppresser.Size = new System.Drawing.Size(104, 21);
            this.lblSuppresser.TabIndex = 83;
            this.lblSuppresser.Text = "检测值：";
            this.lblSuppresser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(313, 386);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 21);
            this.label10.TabIndex = 79;
            this.label10.Text = "检测日期：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(594, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 57;
            this.label2.Text = "规格型号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleLevel
            // 
            this.txtSampleLevel.Location = new System.Drawing.Point(674, 264);
            this.txtSampleLevel.MaxLength = 50;
            this.txtSampleLevel.Name = "txtSampleLevel";
            this.txtSampleLevel.Size = new System.Drawing.Size(150, 21);
            this.txtSampleLevel.TabIndex = 30;
            // 
            // txtSampleState
            // 
            this.txtSampleState.Location = new System.Drawing.Point(977, 576);
            this.txtSampleState.MaxLength = 50;
            this.txtSampleState.Name = "txtSampleState";
            this.txtSampleState.Size = new System.Drawing.Size(154, 21);
            this.txtSampleState.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(899, 576);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 59;
            this.label3.Text = "批号或编号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(805, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 21);
            this.label12.TabIndex = 58;
            this.label12.Text = "质量等级：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaveNum
            // 
            this.txtSaveNum.Location = new System.Drawing.Point(922, 430);
            this.txtSaveNum.MaxLength = 20;
            this.txtSaveNum.Name = "txtSaveNum";
            this.txtSaveNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaveNum.TabIndex = 46;
            this.txtSaveNum.TextChanged += new System.EventHandler(this.txtSaveNum_TextChanged);
            this.txtSaveNum.Validated += new System.EventHandler(this.txtSaveNum_Validated);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(856, 430);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 21);
            this.label8.TabIndex = 75;
            this.label8.Text = "库存数量：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSentCompany
            // 
            this.txtSentCompany.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSentCompany.Location = new System.Drawing.Point(391, 231);
            this.txtSentCompany.Name = "txtSentCompany";
            this.txtSentCompany.Size = new System.Drawing.Size(150, 26);
            this.txtSentCompany.TabIndex = 38;
            this.txtSentCompany.Text = "食品药品质量安全监管站";
            // 
            // labelSendCompany
            // 
            this.labelSendCompany.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSendCompany.Location = new System.Drawing.Point(313, 232);
            this.labelSendCompany.Name = "labelSendCompany";
            this.labelSendCompany.Size = new System.Drawing.Size(89, 21);
            this.labelSendCompany.TabIndex = 76;
            this.labelSendCompany.Text = "送检单位：";
            this.labelSendCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(949, 525);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(150, 21);
            this.txtProvider.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(37, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 21);
            this.label5.TabIndex = 80;
            this.label5.Text = "检测项目：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(860, 525);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 21);
            this.label6.TabIndex = 60;
            this.label6.Text = "供货商/商标：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(849, 62);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(66, 21);
            this.label31.TabIndex = 65;
            this.label31.Text = "检测类型：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.Location = new System.Drawing.Point(53, 426);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(88, 21);
            this.label33.TabIndex = 89;
            this.label33.Text = "核准人：";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAssessor
            // 
            this.cmbAssessor.AddItemSeparator = ';';
            this.cmbAssessor.Caption = "";
            this.cmbAssessor.CaptionHeight = 17;
            this.cmbAssessor.CaptionStyle = style25;
            this.cmbAssessor.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbAssessor.ColumnCaptionHeight = 18;
            this.cmbAssessor.ColumnFooterHeight = 18;
            this.cmbAssessor.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbAssessor.ContentHeight = 16;
            this.cmbAssessor.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbAssessor.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbAssessor.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAssessor.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbAssessor.EditorHeight = 16;
            this.cmbAssessor.EvenRowStyle = style26;
            this.cmbAssessor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAssessor.FooterStyle = style27;
            this.cmbAssessor.GapHeight = 2;
            this.cmbAssessor.HeadingStyle = style28;
            this.cmbAssessor.HighLightRowStyle = style29;
            this.cmbAssessor.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbAssessor.Images"))));
            this.cmbAssessor.ItemHeight = 15;
            this.cmbAssessor.Location = new System.Drawing.Point(138, 423);
            this.cmbAssessor.MatchEntryTimeout = ((long)(2000));
            this.cmbAssessor.MaxDropDownItems = ((short)(5));
            this.cmbAssessor.MaxLength = 10;
            this.cmbAssessor.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbAssessor.Name = "cmbAssessor";
            this.cmbAssessor.OddRowStyle = style30;
            this.cmbAssessor.PartialRightColumn = false;
            this.cmbAssessor.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbAssessor.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbAssessor.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbAssessor.SelectedStyle = style31;
            this.cmbAssessor.Size = new System.Drawing.Size(150, 22);
            this.cmbAssessor.Style = style32;
            this.cmbAssessor.TabIndex = 56;
            this.cmbAssessor.PropBag = resources.GetString("cmbAssessor.PropBag");
            // 
            // cmbOrganizer
            // 
            this.cmbOrganizer.AddItemSeparator = ';';
            this.cmbOrganizer.Caption = "";
            this.cmbOrganizer.CaptionHeight = 17;
            this.cmbOrganizer.CaptionStyle = style33;
            this.cmbOrganizer.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbOrganizer.ColumnCaptionHeight = 18;
            this.cmbOrganizer.ColumnFooterHeight = 18;
            this.cmbOrganizer.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbOrganizer.ContentHeight = 16;
            this.cmbOrganizer.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbOrganizer.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbOrganizer.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOrganizer.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOrganizer.EditorHeight = 16;
            this.cmbOrganizer.EvenRowStyle = style34;
            this.cmbOrganizer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOrganizer.FooterStyle = style35;
            this.cmbOrganizer.GapHeight = 2;
            this.cmbOrganizer.HeadingStyle = style36;
            this.cmbOrganizer.HighLightRowStyle = style37;
            this.cmbOrganizer.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbOrganizer.Images"))));
            this.cmbOrganizer.ItemHeight = 15;
            this.cmbOrganizer.Location = new System.Drawing.Point(138, 343);
            this.cmbOrganizer.MatchEntryTimeout = ((long)(2000));
            this.cmbOrganizer.MaxDropDownItems = ((short)(5));
            this.cmbOrganizer.MaxLength = 10;
            this.cmbOrganizer.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbOrganizer.Name = "cmbOrganizer";
            this.cmbOrganizer.OddRowStyle = style38;
            this.cmbOrganizer.PartialRightColumn = false;
            this.cmbOrganizer.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbOrganizer.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.SelectedStyle = style39;
            this.cmbOrganizer.Size = new System.Drawing.Size(150, 22);
            this.cmbOrganizer.Style = style40;
            this.cmbOrganizer.TabIndex = 48;
            this.cmbOrganizer.PropBag = resources.GetString("cmbOrganizer.PropBag");
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.Location = new System.Drawing.Point(37, 346);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(104, 21);
            this.label35.TabIndex = 77;
            this.label35.Text = "抽样人：";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMachineTag
            // 
            this.lblMachineTag.Location = new System.Drawing.Point(596, 31);
            this.lblMachineTag.Name = "lblMachineTag";
            this.lblMachineTag.Size = new System.Drawing.Size(70, 21);
            this.lblMachineTag.TabIndex = 47;
            this.lblMachineTag.Text = "检测仪器：";
            this.lblMachineTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckMachine
            // 
            this.cmbCheckMachine.AddItemSeparator = ';';
            this.cmbCheckMachine.Caption = "";
            this.cmbCheckMachine.CaptionHeight = 17;
            this.cmbCheckMachine.CaptionStyle = style41;
            this.cmbCheckMachine.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckMachine.ColumnCaptionHeight = 18;
            this.cmbCheckMachine.ColumnFooterHeight = 18;
            this.cmbCheckMachine.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckMachine.ContentHeight = 16;
            this.cmbCheckMachine.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckMachine.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckMachine.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckMachine.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckMachine.EditorHeight = 16;
            this.cmbCheckMachine.Enabled = false;
            this.cmbCheckMachine.EvenRowStyle = style42;
            this.cmbCheckMachine.FooterStyle = style43;
            this.cmbCheckMachine.GapHeight = 2;
            this.cmbCheckMachine.HeadingStyle = style44;
            this.cmbCheckMachine.HighLightRowStyle = style45;
            this.cmbCheckMachine.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckMachine.Images"))));
            this.cmbCheckMachine.ItemHeight = 15;
            this.cmbCheckMachine.Location = new System.Drawing.Point(666, 31);
            this.cmbCheckMachine.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckMachine.MaxDropDownItems = ((short)(5));
            this.cmbCheckMachine.MaxLength = 10;
            this.cmbCheckMachine.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckMachine.Name = "cmbCheckMachine";
            this.cmbCheckMachine.OddRowStyle = style46;
            this.cmbCheckMachine.PartialRightColumn = false;
            this.cmbCheckMachine.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckMachine.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.SelectedStyle = style47;
            this.cmbCheckMachine.Size = new System.Drawing.Size(43, 22);
            this.cmbCheckMachine.Style = style48;
            this.cmbCheckMachine.TabIndex = 18;
            this.cmbCheckMachine.PropBag = resources.GetString("cmbCheckMachine.PropBag");
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(468, 608);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 27);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkNoHaveMachine
            // 
            this.chkNoHaveMachine.Checked = true;
            this.chkNoHaveMachine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNoHaveMachine.Location = new System.Drawing.Point(690, 78);
            this.chkNoHaveMachine.Name = "chkNoHaveMachine";
            this.chkNoHaveMachine.Size = new System.Drawing.Size(110, 22);
            this.chkNoHaveMachine.TabIndex = 19;
            this.chkNoHaveMachine.Text = "其他检测手段";
            // 
            // cmbCheckItem
            // 
            this.cmbCheckItem.AddItemSeparator = ';';
            this.cmbCheckItem.Caption = "";
            this.cmbCheckItem.CaptionHeight = 17;
            this.cmbCheckItem.CaptionStyle = style49;
            this.cmbCheckItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckItem.ColumnCaptionHeight = 18;
            this.cmbCheckItem.ColumnFooterHeight = 18;
            this.cmbCheckItem.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckItem.ContentHeight = 16;
            this.cmbCheckItem.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckItem.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckItem.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItem.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckItem.EditorHeight = 16;
            this.cmbCheckItem.EvenRowStyle = style50;
            this.cmbCheckItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItem.FooterStyle = style51;
            this.cmbCheckItem.GapHeight = 2;
            this.cmbCheckItem.HeadingStyle = style52;
            this.cmbCheckItem.HighLightRowStyle = style53;
            this.cmbCheckItem.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckItem.Images"))));
            this.cmbCheckItem.ItemHeight = 15;
            this.cmbCheckItem.Location = new System.Drawing.Point(138, 76);
            this.cmbCheckItem.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckItem.MaxDropDownItems = ((short)(20));
            this.cmbCheckItem.MaxLength = 10;
            this.cmbCheckItem.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckItem.Name = "cmbCheckItem";
            this.cmbCheckItem.OddRowStyle = style54;
            this.cmbCheckItem.PartialRightColumn = false;
            this.cmbCheckItem.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckItem.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.SelectedStyle = style55;
            this.cmbCheckItem.Size = new System.Drawing.Size(150, 22);
            this.cmbCheckItem.Style = style56;
            this.cmbCheckItem.TabIndex = 23;
            this.cmbCheckItem.SelectedValueChanged += new System.EventHandler(this.cmbCheckItem_SelectedValueChanged);
            this.cmbCheckItem.TextChanged += new System.EventHandler(this.cmbCheckItem_TextChanged);
            this.cmbCheckItem.PropBag = resources.GetString("cmbCheckItem.PropBag");
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSampleCode.Location = new System.Drawing.Point(390, 36);
            this.txtSampleCode.MaxLength = 50;
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.Size = new System.Drawing.Size(150, 26);
            this.txtSampleCode.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(307, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 21);
            this.label11.TabIndex = 67;
            this.label11.Text = "样品编号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(818, 329);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 21);
            this.label14.TabIndex = 70;
            this.label14.Text = "数据单位：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Visible = false;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(853, 400);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 21);
            this.label15.TabIndex = 73;
            this.label15.Text = "数据单位：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpProduceDate
            // 
            this.dtpProduceDate.CalendarFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpProduceDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpProduceDate.Location = new System.Drawing.Point(391, 197);
            this.dtpProduceDate.Name = "dtpProduceDate";
            this.dtpProduceDate.Size = new System.Drawing.Size(149, 26);
            this.dtpProduceDate.TabIndex = 18;
            this.dtpProduceDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            this.dtpProduceDate.DropDown += new System.EventHandler(this.dtpProduceDate_DropDown);
            // 
            // cmbProduceCompany
            // 
            this.cmbProduceCompany.AddItemSeparator = ';';
            this.cmbProduceCompany.Caption = "";
            this.cmbProduceCompany.CaptionHeight = 17;
            this.cmbProduceCompany.CaptionStyle = style57;
            this.cmbProduceCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbProduceCompany.ColumnCaptionHeight = 18;
            this.cmbProduceCompany.ColumnFooterHeight = 18;
            this.cmbProduceCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbProduceCompany.ContentHeight = 16;
            this.cmbProduceCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbProduceCompany.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbProduceCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProduceCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbProduceCompany.EditorHeight = 16;
            this.cmbProduceCompany.EvenRowStyle = style58;
            this.cmbProduceCompany.FooterStyle = style59;
            this.cmbProduceCompany.GapHeight = 2;
            this.cmbProduceCompany.HeadingStyle = style60;
            this.cmbProduceCompany.HighLightRowStyle = style61;
            this.cmbProduceCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProduceCompany.Images"))));
            this.cmbProduceCompany.ItemHeight = 15;
            this.cmbProduceCompany.Location = new System.Drawing.Point(139, 194);
            this.cmbProduceCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbProduceCompany.MaxDropDownItems = ((short)(5));
            this.cmbProduceCompany.MaxLength = 10;
            this.cmbProduceCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProduceCompany.Name = "cmbProduceCompany";
            this.cmbProduceCompany.OddRowStyle = style62;
            this.cmbProduceCompany.PartialRightColumn = false;
            this.cmbProduceCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProduceCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.SelectedStyle = style63;
            this.cmbProduceCompany.Size = new System.Drawing.Size(150, 22);
            this.cmbProduceCompany.Style = style64;
            this.cmbProduceCompany.TabIndex = 35;
            this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
            this.cmbProduceCompany.PropBag = resources.GetString("cmbProduceCompany.PropBag");
            // 
            // lblProduceTag
            // 
            this.lblProduceTag.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProduceTag.Location = new System.Drawing.Point(47, 195);
            this.lblProduceTag.Name = "lblProduceTag";
            this.lblProduceTag.Size = new System.Drawing.Size(95, 21);
            this.lblProduceTag.TabIndex = 63;
            this.lblProduceTag.Text = "供应商：";
            this.lblProduceTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(305, 198);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 21);
            this.label19.TabIndex = 62;
            this.label19.Text = "生产日期：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrCheckNo
            // 
            this.txtOrCheckNo.Location = new System.Drawing.Point(924, 454);
            this.txtOrCheckNo.MaxLength = 50;
            this.txtOrCheckNo.Name = "txtOrCheckNo";
            this.txtOrCheckNo.Size = new System.Drawing.Size(154, 21);
            this.txtOrCheckNo.TabIndex = 39;
            // 
            // txtStdCode
            // 
            this.txtStdCode.Location = new System.Drawing.Point(674, 295);
            this.txtStdCode.Name = "txtStdCode";
            this.txtStdCode.Size = new System.Drawing.Size(150, 21);
            this.txtStdCode.TabIndex = 33;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(842, 454);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 21);
            this.label20.TabIndex = 66;
            this.label20.Text = "原检测编号：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(807, 205);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(106, 21);
            this.label24.TabIndex = 61;
            this.label24.Text = "条形码：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(797, 137);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(116, 17);
            this.lblDomain.TabIndex = 54;
            this.lblDomain.Text = "档口/店面/车牌号：";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompanyInfo
            // 
            this.txtCompanyInfo.Enabled = false;
            this.txtCompanyInfo.Location = new System.Drawing.Point(674, 233);
            this.txtCompanyInfo.Name = "txtCompanyInfo";
            this.txtCompanyInfo.Size = new System.Drawing.Size(150, 21);
            this.txtCompanyInfo.TabIndex = 26;
            // 
            // cmbCheckType
            // 
            this.cmbCheckType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckType.Items.AddRange(new object[] {
            "抽检",
            "送检",
            "抽检复检",
            "送检复检"});
            this.cmbCheckType.Location = new System.Drawing.Point(913, 62);
            this.cmbCheckType.Name = "cmbCheckType";
            this.cmbCheckType.Size = new System.Drawing.Size(154, 20);
            this.cmbCheckType.TabIndex = 37;
            this.cmbCheckType.SelectedIndexChanged += new System.EventHandler(this.cmbCheckType_SelectedIndexChanged);
            // 
            // cmbResult
            // 
            this.cmbResult.AddItemSeparator = ';';
            this.cmbResult.Caption = "";
            this.cmbResult.CaptionHeight = 17;
            this.cmbResult.CaptionStyle = style65;
            this.cmbResult.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbResult.ColumnCaptionHeight = 18;
            this.cmbResult.ColumnFooterHeight = 18;
            this.cmbResult.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbResult.ContentHeight = 16;
            this.cmbResult.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbResult.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbResult.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbResult.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbResult.EditorHeight = 16;
            this.cmbResult.EvenRowStyle = style66;
            this.cmbResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbResult.FooterStyle = style67;
            this.cmbResult.GapHeight = 2;
            this.cmbResult.HeadingStyle = style68;
            this.cmbResult.HighLightRowStyle = style69;
            this.cmbResult.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbResult.Images"))));
            this.cmbResult.ItemHeight = 15;
            this.cmbResult.Location = new System.Drawing.Point(391, 303);
            this.cmbResult.MatchEntryTimeout = ((long)(2000));
            this.cmbResult.MaxDropDownItems = ((short)(5));
            this.cmbResult.MaxLength = 32767;
            this.cmbResult.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.OddRowStyle = style70;
            this.cmbResult.PartialRightColumn = false;
            this.cmbResult.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbResult.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbResult.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbResult.SelectedStyle = style71;
            this.cmbResult.Size = new System.Drawing.Size(150, 22);
            this.cmbResult.Style = style72;
            this.cmbResult.TabIndex = 54;
            this.cmbResult.PropBag = resources.GetString("cmbResult.PropBag");
            // 
            // txtStandValue
            // 
            this.txtStandValue.Enabled = false;
            this.txtStandValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStandValue.Location = new System.Drawing.Point(391, 263);
            this.txtStandValue.MaxLength = 50;
            this.txtStandValue.Name = "txtStandValue";
            this.txtStandValue.Size = new System.Drawing.Size(150, 26);
            this.txtStandValue.TabIndex = 52;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(316, 266);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(87, 21);
            this.label30.TabIndex = 85;
            this.label30.Text = "标准值：";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProducePlace
            // 
            this.cmbProducePlace.AddItemSeparator = ';';
            this.cmbProducePlace.Caption = "";
            this.cmbProducePlace.CaptionHeight = 17;
            this.cmbProducePlace.CaptionStyle = style73;
            this.cmbProducePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbProducePlace.ColumnCaptionHeight = 18;
            this.cmbProducePlace.ColumnFooterHeight = 18;
            this.cmbProducePlace.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbProducePlace.ContentHeight = 16;
            this.cmbProducePlace.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbProducePlace.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbProducePlace.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProducePlace.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbProducePlace.EditorHeight = 16;
            this.cmbProducePlace.EvenRowStyle = style74;
            this.cmbProducePlace.FooterStyle = style75;
            this.cmbProducePlace.GapHeight = 2;
            this.cmbProducePlace.HeadingStyle = style76;
            this.cmbProducePlace.HighLightRowStyle = style77;
            this.cmbProducePlace.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProducePlace.Images"))));
            this.cmbProducePlace.ItemHeight = 15;
            this.cmbProducePlace.Location = new System.Drawing.Point(913, 228);
            this.cmbProducePlace.MatchEntryTimeout = ((long)(2000));
            this.cmbProducePlace.MaxDropDownItems = ((short)(5));
            this.cmbProducePlace.MaxLength = 10;
            this.cmbProducePlace.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProducePlace.Name = "cmbProducePlace";
            this.cmbProducePlace.OddRowStyle = style78;
            this.cmbProducePlace.PartialRightColumn = false;
            this.cmbProducePlace.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProducePlace.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.SelectedStyle = style79;
            this.cmbProducePlace.Size = new System.Drawing.Size(150, 22);
            this.cmbProducePlace.Style = style80;
            this.cmbProducePlace.TabIndex = 36;
            this.cmbProducePlace.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProducePlace_BeforeOpen);
            this.cmbProducePlace.PropBag = resources.GetString("cmbProducePlace.PropBag");
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(847, 229);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 21);
            this.label29.TabIndex = 64;
            this.label29.Text = "产品产地：";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtResultInfo
            // 
            this.txtResultInfo.Enabled = false;
            this.txtResultInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResultInfo.Location = new System.Drawing.Point(138, 303);
            this.txtResultInfo.MaxLength = 50;
            this.txtResultInfo.Name = "txtResultInfo";
            this.txtResultInfo.Size = new System.Drawing.Size(150, 26);
            this.txtResultInfo.TabIndex = 53;
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.Location = new System.Drawing.Point(21, 306);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(120, 21);
            this.label32.TabIndex = 84;
            this.label32.Text = "检测值单位：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckPlanCode
            // 
            this.txtCheckPlanCode.Location = new System.Drawing.Point(913, 256);
            this.txtCheckPlanCode.MaxLength = 50;
            this.txtCheckPlanCode.Name = "txtCheckPlanCode";
            this.txtCheckPlanCode.Size = new System.Drawing.Size(150, 21);
            this.txtCheckPlanCode.TabIndex = 22;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(817, 256);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(96, 21);
            this.label34.TabIndex = 50;
            this.label34.Text = "检测计划编号：";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleNum
            // 
            this.txtSaleNum.Location = new System.Drawing.Point(918, 374);
            this.txtSaleNum.MaxLength = 20;
            this.txtSaleNum.Name = "txtSaleNum";
            this.txtSaleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaleNum.TabIndex = 43;
            this.txtSaleNum.Validated += new System.EventHandler(this.txtSaleNum_Validated);
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(852, 374);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(66, 21);
            this.label36.TabIndex = 72;
            this.label36.Text = "销售数量：";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(924, 501);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(154, 21);
            this.txtPrice.TabIndex = 45;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.Validated += new System.EventHandler(this.txtPrice_Validated);
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(854, 501);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(72, 21);
            this.label37.TabIndex = 74;
            this.label37.Text = "单价(元)：";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIsSentCheck
            // 
            this.cmbIsSentCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsSentCheck.Items.AddRange(new object[] {
            "否",
            "是",
            ""});
            this.cmbIsSentCheck.Location = new System.Drawing.Point(913, 88);
            this.cmbIsSentCheck.Name = "cmbIsSentCheck";
            this.cmbIsSentCheck.Size = new System.Drawing.Size(150, 20);
            this.cmbIsSentCheck.TabIndex = 59;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(847, 88);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(66, 21);
            this.label38.TabIndex = 93;
            this.label38.Text = "是否送检：";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(25, 39);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(15, 21);
            this.label39.TabIndex = 48;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label39.Visible = false;
            // 
            // label40
            // 
            this.label40.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(12, 156);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(15, 21);
            this.label40.TabIndex = 53;
            this.label40.Text = "*";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label40.Visible = false;
            // 
            // label42
            // 
            this.label42.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.ForeColor = System.Drawing.Color.Red;
            this.label42.Location = new System.Drawing.Point(25, 116);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(15, 21);
            this.label42.TabIndex = 55;
            this.label42.Text = "*";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label42.Visible = false;
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.ForeColor = System.Drawing.Color.Red;
            this.label43.Location = new System.Drawing.Point(314, 306);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(15, 21);
            this.label43.TabIndex = 87;
            this.label43.Text = "*";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label43.Visible = false;
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label44.ForeColor = System.Drawing.Color.Red;
            this.label44.Location = new System.Drawing.Point(25, 264);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(15, 21);
            this.label44.TabIndex = 82;
            this.label44.Text = "*";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label44.Visible = false;
            // 
            // label45
            // 
            this.label45.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(26, 225);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(15, 21);
            this.label45.TabIndex = 90;
            this.label45.Text = "*";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label45.Visible = false;
            // 
            // txtCheckerRemark
            // 
            this.txtCheckerRemark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckerRemark.Location = new System.Drawing.Point(138, 503);
            this.txtCheckerRemark.Multiline = true;
            this.txtCheckerRemark.Name = "txtCheckerRemark";
            this.txtCheckerRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCheckerRemark.Size = new System.Drawing.Size(402, 48);
            this.txtCheckerRemark.TabIndex = 61;
            // 
            // label46
            // 
            this.label46.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.Location = new System.Drawing.Point(20, 504);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(121, 21);
            this.label46.TabIndex = 95;
            this.label46.Text = "被检人说明：";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNotes.Location = new System.Drawing.Point(138, 554);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNotes.Size = new System.Drawing.Size(402, 48);
            this.txtNotes.TabIndex = 62;
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.Location = new System.Drawing.Point(34, 560);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(107, 21);
            this.label47.TabIndex = 96;
            this.label47.Text = "备注：";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckerVal
            // 
            this.cmbCheckerVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckerVal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckerVal.Items.AddRange(new object[] {
            "无异议",
            "有异议",
            ""});
            this.cmbCheckerVal.Location = new System.Drawing.Point(391, 423);
            this.cmbCheckerVal.Name = "cmbCheckerVal";
            this.cmbCheckerVal.Size = new System.Drawing.Size(150, 24);
            this.cmbCheckerVal.TabIndex = 58;
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.Location = new System.Drawing.Point(289, 426);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(115, 21);
            this.label48.TabIndex = 92;
            this.label48.Text = "被检人确定：";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Location = new System.Drawing.Point(25, 79);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(15, 21);
            this.label49.TabIndex = 99;
            this.label49.Text = "*";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label49.Visible = false;
            // 
            // txtProduceDate
            // 
            this.txtProduceDate.BackColor = System.Drawing.Color.White;
            this.txtProduceDate.Enabled = false;
            this.txtProduceDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProduceDate.Location = new System.Drawing.Point(391, 197);
            this.txtProduceDate.Name = "txtProduceDate";
            this.txtProduceDate.Size = new System.Drawing.Size(132, 26);
            this.txtProduceDate.TabIndex = 34;
            // 
            // lblPerProduceDateTag
            // 
            this.lblPerProduceDateTag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerProduceDateTag.ForeColor = System.Drawing.Color.Red;
            this.lblPerProduceDateTag.Location = new System.Drawing.Point(299, 197);
            this.lblPerProduceDateTag.Name = "lblPerProduceDateTag";
            this.lblPerProduceDateTag.Size = new System.Drawing.Size(15, 21);
            this.lblPerProduceDateTag.TabIndex = 217;
            this.lblPerProduceDateTag.Text = "*";
            this.lblPerProduceDateTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPerProduceDateTag.Visible = false;
            // 
            // lblPerProduceComTag
            // 
            this.lblPerProduceComTag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerProduceComTag.ForeColor = System.Drawing.Color.Red;
            this.lblPerProduceComTag.Location = new System.Drawing.Point(26, 193);
            this.lblPerProduceComTag.Name = "lblPerProduceComTag";
            this.lblPerProduceComTag.Size = new System.Drawing.Size(15, 21);
            this.lblPerProduceComTag.TabIndex = 218;
            this.lblPerProduceComTag.Text = "*";
            this.lblPerProduceComTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPerProduceComTag.Visible = false;
            // 
            // lblPerProduceTag
            // 
            this.lblPerProduceTag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerProduceTag.ForeColor = System.Drawing.Color.Red;
            this.lblPerProduceTag.Location = new System.Drawing.Point(830, 228);
            this.lblPerProduceTag.Name = "lblPerProduceTag";
            this.lblPerProduceTag.Size = new System.Drawing.Size(15, 21);
            this.lblPerProduceTag.TabIndex = 219;
            this.lblPerProduceTag.Text = "*";
            this.lblPerProduceTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPerProduceTag.Visible = false;
            // 
            // lblPerImportNumTag
            // 
            this.lblPerImportNumTag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerImportNumTag.ForeColor = System.Drawing.Color.Red;
            this.lblPerImportNumTag.Location = new System.Drawing.Point(841, 477);
            this.lblPerImportNumTag.Name = "lblPerImportNumTag";
            this.lblPerImportNumTag.Size = new System.Drawing.Size(15, 21);
            this.lblPerImportNumTag.TabIndex = 220;
            this.lblPerImportNumTag.Text = "*";
            this.lblPerImportNumTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPerImportNumTag.Visible = false;
            // 
            // lblPerSaveNumTag
            // 
            this.lblPerSaveNumTag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerSaveNumTag.ForeColor = System.Drawing.Color.Red;
            this.lblPerSaveNumTag.Location = new System.Drawing.Point(841, 427);
            this.lblPerSaveNumTag.Name = "lblPerSaveNumTag";
            this.lblPerSaveNumTag.Size = new System.Drawing.Size(15, 24);
            this.lblPerSaveNumTag.TabIndex = 221;
            this.lblPerSaveNumTag.Text = "*";
            this.lblPerSaveNumTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPerSaveNumTag.Visible = false;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(138, 153);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(403, 24);
            this.cmbCompany.TabIndex = 25;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(674, 205);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 23);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "选  择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtCiname
            // 
            this.txtCiname.Enabled = false;
            this.txtCiname.Location = new System.Drawing.Point(690, 355);
            this.txtCiname.Name = "txtCiname";
            this.txtCiname.Size = new System.Drawing.Size(154, 21);
            this.txtCiname.TabIndex = 27;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(605, 356);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 21);
            this.label16.TabIndex = 227;
            this.label16.Text = "所属组织：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleUnit
            // 
            this.txtSampleUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSampleUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtSampleUnit.FormattingEnabled = true;
            this.txtSampleUnit.Items.AddRange(new object[] {
            "个",
            "斤",
            "公斤",
            "瓶",
            "盒",
            "包",
            "袋",
            "箱"});
            this.txtSampleUnit.Location = new System.Drawing.Point(915, 330);
            this.txtSampleUnit.Name = "txtSampleUnit";
            this.txtSampleUnit.Size = new System.Drawing.Size(150, 20);
            this.txtSampleUnit.TabIndex = 228;
            this.txtSampleUnit.Visible = false;
            this.txtSampleUnit.SelectedIndexChanged += new System.EventHandler(this.txtSampleUnit_SelectedIndexChanged);
            // 
            // txtUnit
            // 
            this.txtUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtUnit.FormattingEnabled = true;
            this.txtUnit.Items.AddRange(new object[] {
            "个",
            "份",
            "斤",
            "公斤",
            "瓶",
            "盒",
            "包",
            "袋",
            "箱"});
            this.txtUnit.Location = new System.Drawing.Point(919, 401);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(150, 20);
            this.txtUnit.TabIndex = 229;
            this.txtUnit.SelectedIndexChanged += new System.EventHandler(this.txtUnit_SelectedIndexChanged);
            // 
            // txtSampleAmount
            // 
            this.txtSampleAmount.Location = new System.Drawing.Point(915, 303);
            this.txtSampleAmount.Name = "txtSampleAmount";
            this.txtSampleAmount.Size = new System.Drawing.Size(150, 21);
            this.txtSampleAmount.TabIndex = 230;
            this.txtSampleAmount.TextChanged += new System.EventHandler(this.txtSampleAmount_TextChanged);
            this.txtSampleAmount.Validated += new System.EventHandler(this.txtSampleAmount_Validated);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(849, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 21);
            this.label7.TabIndex = 231;
            this.label7.Text = "抽样金额：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(760, 179);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 236;
            this.txtSysID.Visible = false;
            // 
            // cmbUpperCompany
            // 
            this.cmbUpperCompany.AddItemSeparator = ';';
            this.cmbUpperCompany.Caption = "";
            this.cmbUpperCompany.CaptionHeight = 17;
            this.cmbUpperCompany.CaptionStyle = style81;
            this.cmbUpperCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbUpperCompany.ColumnCaptionHeight = 18;
            this.cmbUpperCompany.ColumnFooterHeight = 18;
            this.cmbUpperCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbUpperCompany.ContentHeight = 16;
            this.cmbUpperCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbUpperCompany.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbUpperCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUpperCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbUpperCompany.EditorHeight = 16;
            this.cmbUpperCompany.EvenRowStyle = style82;
            this.cmbUpperCompany.FooterStyle = style83;
            this.cmbUpperCompany.GapHeight = 2;
            this.cmbUpperCompany.HeadingStyle = style84;
            this.cmbUpperCompany.HighLightRowStyle = style85;
            this.cmbUpperCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbUpperCompany.Images"))));
            this.cmbUpperCompany.ItemHeight = 15;
            this.cmbUpperCompany.Location = new System.Drawing.Point(913, 34);
            this.cmbUpperCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbUpperCompany.MaxDropDownItems = ((short)(5));
            this.cmbUpperCompany.MaxLength = 10;
            this.cmbUpperCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbUpperCompany.Name = "cmbUpperCompany";
            this.cmbUpperCompany.OddRowStyle = style86;
            this.cmbUpperCompany.PartialRightColumn = false;
            this.cmbUpperCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbUpperCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.SelectedStyle = style87;
            this.cmbUpperCompany.Size = new System.Drawing.Size(81, 22);
            this.cmbUpperCompany.Style = style88;
            this.cmbUpperCompany.TabIndex = 232;
            this.cmbUpperCompany.Visible = false;
            this.cmbUpperCompany.PropBag = resources.GetString("cmbUpperCompany.PropBag");
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(842, 39);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(66, 17);
            this.lblParent.TabIndex = 234;
            this.lblParent.Text = "所属市场：";
            this.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblParent.Visible = false;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(688, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 235;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // cmbCheckedCompany
            // 
            this.cmbCheckedCompany.AddItemSeparator = ';';
            this.cmbCheckedCompany.Caption = "";
            this.cmbCheckedCompany.CaptionHeight = 17;
            this.cmbCheckedCompany.CaptionStyle = style89;
            this.cmbCheckedCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckedCompany.ColumnCaptionHeight = 18;
            this.cmbCheckedCompany.ColumnFooterHeight = 18;
            this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckedCompany.ContentHeight = 16;
            this.cmbCheckedCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckedCompany.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckedCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckedCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckedCompany.EditorHeight = 16;
            this.cmbCheckedCompany.EvenRowStyle = style90;
            this.cmbCheckedCompany.FooterStyle = style91;
            this.cmbCheckedCompany.GapHeight = 2;
            this.cmbCheckedCompany.HeadingStyle = style92;
            this.cmbCheckedCompany.HighLightRowStyle = style93;
            this.cmbCheckedCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckedCompany.Images"))));
            this.cmbCheckedCompany.ItemHeight = 15;
            this.cmbCheckedCompany.Location = new System.Drawing.Point(913, 117);
            this.cmbCheckedCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckedCompany.MaxDropDownItems = ((short)(5));
            this.cmbCheckedCompany.MaxLength = 10;
            this.cmbCheckedCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckedCompany.Name = "cmbCheckedCompany";
            this.cmbCheckedCompany.OddRowStyle = style94;
            this.cmbCheckedCompany.PartialRightColumn = false;
            this.cmbCheckedCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckedCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.SelectedStyle = style95;
            this.cmbCheckedCompany.Size = new System.Drawing.Size(79, 22);
            this.cmbCheckedCompany.Style = style96;
            this.cmbCheckedCompany.TabIndex = 233;
            this.cmbCheckedCompany.Visible = false;
            this.cmbCheckedCompany.PropBag = resources.GetString("cmbCheckedCompany.PropBag");
            // 
            // tb_Phone
            // 
            this.tb_Phone.Location = new System.Drawing.Point(913, 159);
            this.tb_Phone.Name = "tb_Phone";
            this.tb_Phone.Size = new System.Drawing.Size(150, 21);
            this.tb_Phone.TabIndex = 237;
            this.tb_Phone.Visible = false;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(797, 161);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 17);
            this.label17.TabIndex = 238;
            this.label17.Text = "电话：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Visible = false;
            // 
            // cb_CompanyArea
            // 
            this.cb_CompanyArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_CompanyArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_CompanyArea.FormattingEnabled = true;
            this.cb_CompanyArea.Items.AddRange(new object[] {
            "福田",
            "罗湖",
            "南山",
            "盐田",
            "宝安",
            "龙岗",
            "光明",
            "坪山",
            "龙华",
            "大鹏"});
            this.cb_CompanyArea.Location = new System.Drawing.Point(701, 329);
            this.cb_CompanyArea.Name = "cb_CompanyArea";
            this.cb_CompanyArea.Size = new System.Drawing.Size(123, 20);
            this.cb_CompanyArea.TabIndex = 239;
            this.cb_CompanyArea.Visible = false;
            // 
            // tb_Contact
            // 
            this.tb_Contact.Enabled = false;
            this.tb_Contact.Location = new System.Drawing.Point(913, 137);
            this.tb_Contact.Name = "tb_Contact";
            this.tb_Contact.Size = new System.Drawing.Size(150, 21);
            this.tb_Contact.TabIndex = 240;
            // 
            // tb_IsDestruction
            // 
            this.tb_IsDestruction.Location = new System.Drawing.Point(913, 182);
            this.tb_IsDestruction.MaxLength = 50;
            this.tb_IsDestruction.Name = "tb_IsDestruction";
            this.tb_IsDestruction.Size = new System.Drawing.Size(150, 21);
            this.tb_IsDestruction.TabIndex = 242;
            // 
            // tb_Quality
            // 
            this.tb_Quality.Location = new System.Drawing.Point(913, 205);
            this.tb_Quality.Name = "tb_Quality";
            this.tb_Quality.Size = new System.Drawing.Size(150, 21);
            this.tb_Quality.TabIndex = 243;
            // 
            // cmbCheckItemAnHui
            // 
            this.cmbCheckItemAnHui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckItemAnHui.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItemAnHui.FormattingEnabled = true;
            this.cmbCheckItemAnHui.Location = new System.Drawing.Point(665, 451);
            this.cmbCheckItemAnHui.Name = "cmbCheckItemAnHui";
            this.cmbCheckItemAnHui.Size = new System.Drawing.Size(151, 24);
            this.cmbCheckItemAnHui.TabIndex = 245;
            this.cmbCheckItemAnHui.SelectedIndexChanged += new System.EventHandler(this.cmbCheckItemAnHui_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(585, 454);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 21);
            this.label21.TabIndex = 244;
            this.label21.Text = "检测项目：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFoodType
            // 
            this.cmbFoodType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFoodType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFoodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFoodType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFoodType.FormattingEnabled = true;
            this.cmbFoodType.Location = new System.Drawing.Point(695, 381);
            this.cmbFoodType.Name = "cmbFoodType";
            this.cmbFoodType.Size = new System.Drawing.Size(149, 24);
            this.cmbFoodType.TabIndex = 247;
            this.cmbFoodType.SelectedIndexChanged += new System.EventHandler(this.cmbFoodType_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(313, 114);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(90, 21);
            this.label26.TabIndex = 246;
            this.label26.Text = "样品种类：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleModel
            // 
            this.txtSampleModel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSampleModel.Location = new System.Drawing.Point(666, 106);
            this.txtSampleModel.Name = "txtSampleModel";
            this.txtSampleModel.Size = new System.Drawing.Size(150, 26);
            this.txtSampleModel.TabIndex = 29;
            this.txtSampleModel.Visible = false;
            this.txtSampleModel.WordWrap = false;
            // 
            // tb_SampleSource
            // 
            this.tb_SampleSource.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_SampleSource.Location = new System.Drawing.Point(666, 138);
            this.tb_SampleSource.Name = "tb_SampleSource";
            this.tb_SampleSource.Size = new System.Drawing.Size(150, 26);
            this.tb_SampleSource.TabIndex = 241;
            this.tb_SampleSource.Visible = false;
            this.tb_SampleSource.WordWrap = false;
            // 
            // textFoodType
            // 
            this.textFoodType.Enabled = false;
            this.textFoodType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textFoodType.Location = new System.Drawing.Point(390, 111);
            this.textFoodType.MaxLength = 50;
            this.textFoodType.Name = "textFoodType";
            this.textFoodType.Size = new System.Drawing.Size(150, 26);
            this.textFoodType.TabIndex = 248;
            // 
            // FrmHandTakeJD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(566, 642);
            this.Controls.Add(this.dtpCheckStartDate);
            this.Controls.Add(this.dtpTakeDate);
            this.Controls.Add(this.textFoodType);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.cmbFoodType);
            this.Controls.Add(this.cb_CompanyArea);
            this.Controls.Add(this.txtCiname);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbCheckItemAnHui);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.tb_Quality);
            this.Controls.Add(this.tb_IsDestruction);
            this.Controls.Add(this.tb_Contact);
            this.Controls.Add(this.cmbCheckItem);
            this.Controls.Add(this.txtCheckNo);
            this.Controls.Add(this.tb_Phone);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.cmbUpperCompany);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbCheckedCompany);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.lblPerSaveNumTag);
            this.Controls.Add(this.lblPerImportNumTag);
            this.Controls.Add(this.lblPerProduceTag);
            this.Controls.Add(this.lblPerProduceComTag);
            this.Controls.Add(this.lblPerProduceDateTag);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.cmbCheckerVal);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtCheckerRemark);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtSaleNum);
            this.Controls.Add(this.txtCheckPlanCode);
            this.Controls.Add(this.txtResultInfo);
            this.Controls.Add(this.txtStandValue);
            this.Controls.Add(this.txtCompanyInfo);
            this.Controls.Add(this.txtOrCheckNo);
            this.Controls.Add(this.txtStdCode);
            this.Controls.Add(this.txtSentCompany);
            this.Controls.Add(this.txtSampleCode);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.txtSampleLevel);
            this.Controls.Add(this.txtSampleState);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtStandard);
            this.Controls.Add(this.txtCheckValueInfo);
            this.Controls.Add(this.txtSampleNum);
            this.Controls.Add(this.txtSampleBaseNum);
            this.Controls.Add(this.txtImportNum);
            this.Controls.Add(this.txtSaveNum);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.cmbIsSentCheck);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cmbProducePlace);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cmbProduceCompany);
            this.Controls.Add(this.lblProduceTag);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbCheckMachine);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.chkNoHaveMachine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbOrganizer);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.cmbAssessor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelSendCompany);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbFood);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblReferStandard);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cmbChecker);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCheckType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblMachineTag);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.cmbCheckUnit);
            this.Controls.Add(this.lblSuppresser);
            this.Controls.Add(this.txtSampleAmount);
            this.Controls.Add(this.txtSampleUnit);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.txtSampleModel);
            this.Controls.Add(this.tb_SampleSource);
            this.Controls.Add(this.labelCheckUnit);
            this.Controls.Add(this.dtpProduceDate);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtProduceDate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHandTakeJD";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "其他检测数据手工录入";
            this.Load += new System.EventHandler(this.FrmHandTakeJD_Load);
            this.Controls.SetChildIndex(this.txtProduceDate, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.dtpProduceDate, 0);
            this.Controls.SetChildIndex(this.labelCheckUnit, 0);
            this.Controls.SetChildIndex(this.tb_SampleSource, 0);
            this.Controls.SetChildIndex(this.txtSampleModel, 0);
            this.Controls.SetChildIndex(this.label48, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.btnSelect, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.txtSampleUnit, 0);
            this.Controls.SetChildIndex(this.txtSampleAmount, 0);
            this.Controls.SetChildIndex(this.lblSuppresser, 0);
            this.Controls.SetChildIndex(this.cmbCheckUnit, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.lblMachineTag, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbCheckType, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.cmbChecker, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.lblSample, 0);
            this.Controls.SetChildIndex(this.lblReferStandard, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.Controls.SetChildIndex(this.cmbFood, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.labelSendCompany, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbAssessor, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.cmbOrganizer, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkNoHaveMachine, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cmbCheckMachine, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.lblProduceTag, 0);
            this.Controls.SetChildIndex(this.cmbProduceCompany, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.cmbResult, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.cmbProducePlace, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.lblDomain, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label37, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.cmbIsSentCheck, 0);
            this.Controls.SetChildIndex(this.label39, 0);
            this.Controls.SetChildIndex(this.label42, 0);
            this.Controls.SetChildIndex(this.label43, 0);
            this.Controls.SetChildIndex(this.label44, 0);
            this.Controls.SetChildIndex(this.label45, 0);
            this.Controls.SetChildIndex(this.label46, 0);
            this.Controls.SetChildIndex(this.label47, 0);
            this.Controls.SetChildIndex(this.txtSaveNum, 0);
            this.Controls.SetChildIndex(this.txtImportNum, 0);
            this.Controls.SetChildIndex(this.txtSampleBaseNum, 0);
            this.Controls.SetChildIndex(this.txtSampleNum, 0);
            this.Controls.SetChildIndex(this.txtCheckValueInfo, 0);
            this.Controls.SetChildIndex(this.txtStandard, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.txtSampleState, 0);
            this.Controls.SetChildIndex(this.txtSampleLevel, 0);
            this.Controls.SetChildIndex(this.txtProvider, 0);
            this.Controls.SetChildIndex(this.txtSampleCode, 0);
            this.Controls.SetChildIndex(this.txtSentCompany, 0);
            this.Controls.SetChildIndex(this.txtStdCode, 0);
            this.Controls.SetChildIndex(this.txtOrCheckNo, 0);
            this.Controls.SetChildIndex(this.txtCompanyInfo, 0);
            this.Controls.SetChildIndex(this.txtStandValue, 0);
            this.Controls.SetChildIndex(this.txtResultInfo, 0);
            this.Controls.SetChildIndex(this.txtCheckPlanCode, 0);
            this.Controls.SetChildIndex(this.txtSaleNum, 0);
            this.Controls.SetChildIndex(this.txtPrice, 0);
            this.Controls.SetChildIndex(this.txtCheckerRemark, 0);
            this.Controls.SetChildIndex(this.txtNotes, 0);
            this.Controls.SetChildIndex(this.cmbCheckerVal, 0);
            this.Controls.SetChildIndex(this.label49, 0);
            this.Controls.SetChildIndex(this.lblPerProduceDateTag, 0);
            this.Controls.SetChildIndex(this.lblPerProduceComTag, 0);
            this.Controls.SetChildIndex(this.lblPerProduceTag, 0);
            this.Controls.SetChildIndex(this.lblPerImportNumTag, 0);
            this.Controls.SetChildIndex(this.lblPerSaveNumTag, 0);
            this.Controls.SetChildIndex(this.txtUnit, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbCheckedCompany, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.lblParent, 0);
            this.Controls.SetChildIndex(this.cmbUpperCompany, 0);
            this.Controls.SetChildIndex(this.txtSysID, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.tb_Phone, 0);
            this.Controls.SetChildIndex(this.txtCheckNo, 0);
            this.Controls.SetChildIndex(this.cmbCheckItem, 0);
            this.Controls.SetChildIndex(this.tb_Contact, 0);
            this.Controls.SetChildIndex(this.tb_IsDestruction, 0);
            this.Controls.SetChildIndex(this.tb_Quality, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.cmbCheckItemAnHui, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.txtCiname, 0);
            this.Controls.SetChildIndex(this.cb_CompanyArea, 0);
            this.Controls.SetChildIndex(this.cmbFoodType, 0);
            this.Controls.SetChildIndex(this.label40, 0);
            this.Controls.SetChildIndex(this.textFoodType, 0);
            this.Controls.SetChildIndex(this.dtpTakeDate, 0);
            this.Controls.SetChildIndex(this.dtpCheckStartDate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganizer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducePlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUpperCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// 绑定检测项目
        /// </summary>
        /// <param name="strWhere"></param>
        private void bindCheckItem(string strWhere)
        {
            clsCheckItemOpr checkItemBll = new clsCheckItemOpr();
            DataTable dtCheckItem = checkItemBll.GetAsDataTable(strWhere, "ItemDes", 1);
            if (dtCheckItem.Rows.Count <= 0 || dtCheckItem == null)
            {
                MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
                return;
            }
            DataRow[] rows = dtCheckItem.Select("", "ItemDes asc");
            DataTable t = dtCheckItem.Clone();
            t.Clear();

            foreach (DataRow row in rows)
                t.ImportRow(row);

            dtCheckItem = t;
            if (dtCheckItem != null)
            {
                cmbCheckItem.DataSource = dtCheckItem;
                cmbCheckItem.DataMember = "CheckItem";
                cmbCheckItem.DisplayMember = "ItemDes";
                cmbCheckItem.ValueMember = "SysCode";
                cmbCheckItem.Columns["StdCode"].Caption = "编号";
                cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
                cmbCheckItem.Columns["SysCode"].Caption = "系统编号";
            }
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmHandTakeJD_Load(object sender, System.EventArgs e)
        {
            lblParent.Text = ShareOption.AreaTitle + "：";
            lblName.Text = ShareOption.NameTitle + "：";
            lblDomain.Text = ShareOption.DomainTitle + "：";
            lblSample.Text = ShareOption.SampleTitle + "名称：";
            lblProduceTag.Text = ShareOption.ProductionUnitNameTag;
            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }
            #region
            //绑定仪器列表
            //clsMachineOpr machineBll = new clsMachineOpr();
            //DataTable dtMachineList = machineBll.GetAsDataTable("IsShow=True", "OrderId", 1);
            //if (dtMachineList != null)
            //{
            //    cmbCheckMachine.DataSource = dtMachineList.DataSet;//.DataSet;
            //    cmbCheckMachine.DataMember = "Machine";
            //    cmbCheckMachine.DisplayMember = "MachineName";
            //    cmbCheckMachine.ValueMember = "SysCode";
            //    cmbCheckMachine.Columns["MachineName"].Caption = "检测仪器";
            //    cmbCheckMachine.Columns["SysCode"].Caption = "系统编号";
            //    cmbCheckMachine.SelectedValue = machineCode;
            //}
            //bindCheckItem("IsLock=false");
            #endregion
            if (ShareOption.DtblCheckCompany != null)
            {
                cmbCheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
                cmbCheckUnit.DataMember = "UserUnit";
                cmbCheckUnit.DisplayMember = "FullName";
                cmbCheckUnit.ValueMember = "SysCode";
                cmbCheckUnit.Columns["StdCode"].Caption = "编号";
                cmbCheckUnit.Columns["FullName"].Caption = "检测单位";
                cmbCheckUnit.Columns["SysCode"].Caption = "系统编号";
            }

            if (ShareOption.DtblChecker != null)
            {
                DataSet dstChecker = ShareOption.DtblChecker.DataSet;
                cmbChecker.DataSource = dstChecker;
                cmbChecker.DataMember = "UserInfo";
                cmbChecker.DisplayMember = "Name";
                cmbChecker.ValueMember = "UserCode";
                cmbChecker.Columns["Name"].Caption = "检测人";
                cmbChecker.Columns["UserCode"].Caption = "系统编号";

                cmbAssessor.DataSource = dstChecker.Copy();
                cmbAssessor.DataMember = "UserInfo";
                cmbAssessor.DisplayMember = "Name";
                cmbAssessor.ValueMember = "UserCode";
                cmbAssessor.Columns["Name"].Caption = "审核人";
                cmbAssessor.Columns["UserCode"].Caption = "系统编号";

                cmbOrganizer.DataSource = dstChecker.Copy();
                cmbOrganizer.DataMember = "UserInfo";
                cmbOrganizer.DisplayMember = "Name";
                cmbOrganizer.ValueMember = "UserCode";
                cmbOrganizer.Columns["Name"].Caption = "编制人";
                cmbOrganizer.Columns["UserCode"].Caption = "系统编号";
            }
            cmbResult.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbResult.AddItemCols = 1;
            cmbResult.AddItemTitles("检测结果");
            cmbResult.AddItem(ShareOption.ResultEligi);
            cmbResult.AddItem(ShareOption.ResultFailure);
            txtStandard.Enabled = false;
            txtStandValue.Enabled = false;
            //cmbChecker.Enabled = false; //每周一检需要更换检测人
            clsResult model = new clsResult();
            string syscode = FrmMain.formMain.getNewSystemCode(false);
            txtSysID.Text = syscode;
            //txtCheckNo.Text = ShareOption.SysStdCodeSame ? syscode : string.Empty;
            cmbCheckType.SelectedIndex = 0;
            txtUnit.Text = ShareOption.SysUnit;
            txtSampleUnit.Text = ShareOption.SysUnit;
            dtpTakeDate.Value = DateTime.Today;
            cmbCheckerVal.SelectedIndex = 0;
            cmbAssessor.SelectedIndex = 0;
            cmbOrganizer.SelectedIndex = 0;
            cmbIsSentCheck.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(_machineCode))//如果是带仪器手工录入
            {
                #region
                ////绑定仪器列表
                //clsMachineOpr machineBll = new clsMachineOpr();
                //DataTable dtMachineList = machineBll.GetAsDataTable("IsShow=True", "OrderId", 1);
                //if (dtMachineList != null)
                //{
                //    cmbCheckMachine.DataSource = dtMachineList.DataSet;//.DataSet;
                //    cmbCheckMachine.DataMember = "Machine";
                //    cmbCheckMachine.DisplayMember = "MachineName";
                //    cmbCheckMachine.ValueMember = "SysCode";
                //    cmbCheckMachine.Columns["MachineName"].Caption = "检测仪器";
                //    cmbCheckMachine.Columns["SysCode"].Caption = "系统编号";
                //    cmbCheckMachine.SelectedValue = machineCode;
                //}
                #endregion
                cmbCheckMachine.Visible = true;
                lblMachineTag.Visible = true;
                string machineName = clsMachineOpr.GetMachineNameFromCode(_machineCode);//仪器名称
                cmbCheckMachine.SelectedValue = _machineCode;
                cmbCheckMachine.Text = machineName;
                this.Text = machineName + "手工录入";
                chkNoHaveMachine.Visible = false;
                chkNoHaveMachine.Checked = false;
                bindInit();
            }
            else //如果是其他手工录入
            {
                TitleBarText = "其它检测数据手工录入";
                lblMachineTag.Visible = false;
                cmbCheckMachine.Visible = false;
                chkNoHaveMachine.Visible = false;
                //绑定检测项目
                bindCheckItem("IsLock=false");
            }
            dtpCheckStartDate.Value = DateTime.Now;
            cmbCheckUnit.SelectedValue = FrmMain.formMain.checkUnitCode;
            cmbCheckUnit.Text = clsUserUnitOpr.GetNameFromCode(FrmMain.formMain.checkUnitCode);
            cmbChecker.SelectedValue = FrmMain.formMain.userCode;
            cmbChecker.Text = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
            if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
            {
                cmbUpperCompany.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
                _upperComSelectedValue = clsCompanyOpr.CodeFromStdCode(clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode));
            }
            if (ShareOption.AllowHandInputCheckUint)
            {
                cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownCombo;
                cmbUpperCompany.Enabled = ShareOption.SystemVersion == ShareOption.LocalBaseVersion ? true : false;
                txtCompanyInfo.Enabled = true;
            }
            else
            {
                cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
                cmbUpperCompany.Enabled = false;
                txtCompanyInfo.Enabled = false;
            }
            BindCompanies();
            cmbCompany.Text = string.Empty;
            txtCompanyInfo.Text = string.Empty;
            txtCiname.Text = string.Empty;
            if (_saveType)
            {
                lblDomain.Text = "联系人：";
                txtCompanyInfo.Enabled = true;
                tb_Phone.Visible = true;
                tb_Contact.Enabled = true;
                label16.Text = "行政区域：";
                label16.Width = 114;
                cb_CompanyArea.Visible = true;
                txtCiname.Visible = false;
                label17.Visible = true;
                label2.Text = "样品来源：";
                label12.Text = "是否主动销毁：";
                label24.Text = "销毁重量(KG)：";
            }

            ////样品种类
            //cmbFoodType.DataSource = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '食品分类'", 1, "FoodType");
            //cmbFoodType.DisplayMember = "name";
            //cmbFoodType.ValueMember = "codeId";
            //txtSampleCode.Text = txtCheckNo.Text.Trim();
            //
            #region @zh 2016/11/20 合肥
            //调整界面文字
            lblName.Text = "受检单位：";
            lblProduceTag.Text = "生产单位：";
            txtSentCompany.Enabled = true;
            if (txtStandValue.Text.Trim().Length==0)
            {
                txtStandValue.Text = "40";
            }
            #endregion
        }

        private void bindInit()
        {
            TitleBarText = this.Text;
            bool ischeck = false;
            string protocol = clsMachineOpr.GetNameFromCode("Protocol", _machineCode);
            if (protocol.Equals("RS232DY3000DY") || protocol.Equals("RS232LZ4000TDY"))
            {
                ischeck = true;
            }
            switch (_machineCode)
            {
                case "002"://RS232水分仪插件
                case "003":
                    lblSuppresser.Text = "水分含量：";
                    break;
                case "018"://RS232DY3000DY
                    lblSuppresser.Text = "抑制率：";
                    break;
                default:
                    lblSuppresser.Text = "检测值：";
                    break;
            }
            string linkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", _machineCode);
            //DY系列
            _checkItems = ischeck ? StringUtil.GetDY3000DYAry(linkStdCode) : StringUtil.GetAry(linkStdCode);
            int len = _checkItems.GetLength(0);

            if (len >= 1 && _checkItems[0, 1].ToString() != "-1")
            {
                _checkItemCode = _checkItems[0, 1].ToString();
            }

            if (len == 1 && _checkItems[0, 1].ToString() == "-1")
            {
                _checkItemCode = string.Empty;
            }
            if (len <= 0 && _machineCode != "")
            {
                MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
                return;
            }
            string strWhere = string.Empty;
            string sql = string.Empty;
            bool blExist = false;
            if (len > 1)
            {
                for (int i = 0; i < len; i++)
                {
                    if (_checkItems[i, 1].ToString() != "-1")
                        sql = sql + "'" + _checkItems[i, 1].ToString() + "',";
                    if (_checkItems[i, 1].ToString() == _checkItemCode)
                        blExist = true;
                }
                if (!blExist)
                    _checkItemCode = string.Empty;
                if (sql.Length > 0)
                {
                    sql = sql.Substring(0, sql.Length - 1);
                    strWhere = "IsLock=false AND SysCode IN(" + sql + ")";
                }
                if (sql.Length <= 0)
                {
                    MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
                    return;
                }

            }
            else
            {
                strWhere = "IsLock=false AND SysCode ='" + _checkItems[0, 1].ToString() + "'";
            }
            if (len >= 0 && _machineCode != "")
            {
                bindCheckItem(strWhere);

                if (_checkItemCode.Equals(""))
                {
                    cmbCheckItem.SelectedIndex = -1;
                    cmbCheckItem.Text = string.Empty;
                }
                else
                {
                    cmbCheckItem.SelectedValue = _checkItemCode;
                    cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(_checkItemCode);
                }
            }

        }

        /// <summary>
        /// 检验输入的值是否合法
        /// </summary>
        /// <returns></returns>
        private bool checkValue()
        {
            try
            {
                if (_foodType.Length == 0)
                {
                    MessageBox.Show(this, "请选择样品种类！");
                    return false;
                }
                //if (_checkItemsCode.Length == 0)
                //{
                //    MessageBox.Show(this, "请选锁定上传的检测项目标识！");
                //    return false;
                //}
                if (txtSampleCode.Text.Trim().Equals(""))
                {
                    MessageBox.Show(this, "样品编号必须输入!");
                    txtSampleCode.Focus();
                    return false;
                }
                if (txtCheckNo.Text.Trim().Equals(""))
                {
                    MessageBox.Show(this, "检测编号必须输入!");
                    txtCheckNo.Focus();
                    return false;
                }
                if (txtSentCompany.Text.Trim().Length==0)
                {
                    MessageBox.Show(this, "送检单位不能为空!");
                    txtSentCompany.Focus();
                    return false;
                }
                if (ShareOption.AllowHandInputCheckUint)
                {
                    if (_upperComSelectedValue.Equals(""))
                    {
                        MessageBox.Show(this, ShareOption.AreaTitle + "必须输入!");
                        cmbUpperCompany.Text = string.Empty;
                        cmbUpperCompany.Focus();
                        return false;
                    }
                    if (cmbCheckedCompany.Text.Trim().Equals(""))
                    {
                        MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                        cmbCheckedCompany.Text = string.Empty;
                        cmbCheckedCompany.Focus();
                        return false;
                    }
                }
                else
                {
                    string companyName = cmbCompany.Text.Trim();
                    string companyCiname = txtCiname.Text.Trim();
                    if (companyName.Equals(string.Empty))
                    {
                        MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                        cmbCompany.Focus();
                        return false;
                    }
                    string name = clsCompanyOpr.GetCompanyName(cmbCompany.Text);
                    string sename = clsProprietorsOpr.GetCompanyName(cmbCompany.Text);
                    if (!_saveType)
                    {
                        if (name == string.Empty && sename == string.Empty)
                        {
                            MessageBox.Show(this, "录入的" + ShareOption.NameTitle + "不存在,请重新录入!");
                            cmbCompany.SelectAll();
                            cmbCompany.Focus();
                            return false;
                        }
                    }
                    _checkedComSelectedValue = clsCompanyOpr.GetCompanyCode(companyName);
                }
                if (_foodSelectedValue.Equals(""))
                {
                    MessageBox.Show(this, ShareOption.SampleTitle + "必须输入!");
                    cmbFood.Text = string.Empty;
                    cmbFood.Focus();
                    return false;
                }
                if (!txtProduceDate.Text.Trim().Equals(""))
                {
                    DateTime dti = new DateTime();
                    if (DateTime.TryParse(txtProduceDate.Text.Trim(), out  dti))
                    {
                        if (txtProduceDate.Text.Trim().Length >= 8)
                        {
                            dti = Convert.ToDateTime(txtProduceDate.Text);
                        }
                        if (txtProduceDate.Text.Trim().Length <= 7)
                        {
                            MessageBox.Show(this, "生产日期无效，请重新填写!");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "生产日期无效，请重新填写!");
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(_machineCode))
                {
                    if (txtCheckValueInfo.Text.Trim().Equals(""))
                    {
                        MessageBox.Show(this, lblSuppresser.Text.Replace("：", "") + "必须输入!");
                        return false;
                    }
                }
                if (StringUtil.IsNumeric(txtCheckValueInfo.Text) && txtCheckValueInfo.Text != string.Empty)
                    setTestValue();
                if (cmbCheckUnit.SelectedValue == null)
                {
                    MessageBox.Show(this, "检测单位必须输入!");
                    cmbCheckUnit.Text = string.Empty;
                    cmbCheckUnit.Focus();
                    return false;
                }
                if (cmbChecker.SelectedValue == null)
                {
                    MessageBox.Show(this, "检测人必须输入!");
                    cmbChecker.Text = string.Empty;
                    cmbChecker.Focus();
                    return false;
                }
                if (cmbResult.Text.Trim().Equals(""))
                {
                    MessageBox.Show(this, "结论必须输入!");
                    cmbResult.Focus();
                    return false;
                }
                //如果是工商版本同时为不合格的时候
                if (ShareOption.ApplicationTag == ShareOption.ICAppTag && cmbResult.Text.Trim().Equals("不合格"))
                {
                    if (txtProduceDate.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格，生产日期必须输入!");
                        txtProduceDate.Focus();
                        return false;
                    }
                    if (cmbProduceCompany.Text.Trim().Equals(""))
                    {
                        MessageBox.Show(this, string.Format("检测结果不合格，{0}必须输入!", lblProduceTag.Text));
                        cmbProduceCompany.Focus();
                        return false;
                    }
                    if (cmbProducePlace.Text.Trim().Equals(""))
                    {
                        MessageBox.Show(this, "检测结果不合格,产品产地必须输入");
                        cmbProducePlace.Focus();
                        return false;
                    }
                    if (txtImportNum.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格,进货数量必须输入");
                        txtImportNum.Focus();
                        return false;
                    }
                    if (txtSaveNum.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格,库存数量必须输入");
                        txtSaveNum.Focus();
                        return false;
                    }
                }
                //非法字符检查
                Control posControl;
                if (RegularCheck.HaveSpecChar(this, out posControl))
                {
                    MessageBox.Show(this, "输入中有非法字符,请检查!");
                    posControl.Focus();
                    return false;
                }
                //非字符型检查
                if (!StringUtil.IsValidNumber(txtSampleBaseNum.Text.Trim()))
                {
                    MessageBox.Show(this, "抽样基数必须为整数!");
                    txtSampleBaseNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(txtSampleNum.Text.Trim()))
                {
                    MessageBox.Show(this, "抽样数量必须为数字!");
                    txtSampleNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(txtImportNum.Text.Trim()))
                {
                    MessageBox.Show(this, "进货数量必须为数字!");
                    txtImportNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(txtSaleNum.Text.Trim()))
                {
                    MessageBox.Show(this, "销售数量必须为数字!");
                    txtSaleNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(txtPrice.Text.Trim()))
                {
                    MessageBox.Show(this, "单价必须为数字!");
                    txtPrice.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(txtSaveNum.Text.Trim()))
                {
                    MessageBox.Show(this, "库存数量必须为数字!");
                    txtSaveNum.Focus();
                    return false;
                }
                if (dtpTakeDate.Value > dtpCheckStartDate.Value)
                {
                    MessageBox.Show(this, "抽样日期不能超过检测开始时间!");
                    dtpTakeDate.Focus();
                    return false;
                }
                string sErr = string.Empty;
                if (_resultBll.GetRecCount(" CheckNo='" + txtCheckNo.Text.Trim() + "'", out sErr) > 0)
                {
                    MessageBox.Show(this, "检测编号已经存在请换一个编号!");
                    txtCheckNo.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
            return true;
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (checkValue())
            {
                clsResult model = new clsResult();
                try
                {
                    model.SysCode = txtSysID.Text.Trim();
                    model.CheckNo = txtCheckNo.Text.Trim();
                    model.ResultType = !string.IsNullOrEmpty(_machineCode) ? ShareOption.ResultType1 : ShareOption.ResultType3;
                    model.StdCode = txtStdCode.Text.Trim();
                    model.SampleCode = txtSampleCode.Text.Trim();
                    model.CheckedCompany = _checkedComSelectedValue;
                    model.CheckedCompanyName = cmbCompany.Text.Trim();
                    model.CheckedComDis = txtCompanyInfo.Text.Trim();
                    model.CheckPlaceCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode);
                    model.FoodCode = _foodSelectedValue;//样品编号
                    string produceDate = txtProduceDate.Text;
                    if (!string.IsNullOrEmpty(produceDate))
                        model.ProduceDate = Convert.ToDateTime(produceDate);
                    model.ProduceCompany = _produceComSelectedValue;
                    model.ProducePlace = _producePlaceSelectValue;
                    model.SentCompany = txtSentCompany.Text.Trim();
                    model.Provider = txtProvider.Text.Trim();
                    model.TakeDate = dtpTakeDate.Value;
                    model.CheckStartDate = dtpCheckStartDate.Value;
                    model.ImportNum = txtImportNum.Text.Trim();
                    model.SaveNum = txtSaveNum.Text.Trim();
                    model.Unit = txtUnit.Text.Trim();
                    model.SampleBaseNum = txtSampleBaseNum.Text.Trim().Equals("") ? "null" : txtSampleBaseNum.Text.Trim();
                    model.SampleNum = txtSampleNum.Text.Trim().Equals("") ? "null" : txtSampleNum.Text.Trim();
                    model.SampleUnit = txtSampleUnit.Text.Trim();
                    model.SampleLevel = txtSampleLevel.Text.Trim();
                    model.SampleModel = txtSampleModel.Text.Trim();
                    model.SampleState = txtSampleState.Text.Trim();
                    model.CheckMachine = _machineCode;
                    #region
                    //if (!chkNoHaveMachine.Checked)//不勾选的情况,即有仪器名称
                    //{
                    //    model.CheckMachine = cmbCheckMachine.SelectedValue.ToString();
                    //}
                    //else
                    //{
                    //    model.CheckMachine = string.Empty;
                    //}
                    #endregion
                    model.Standard = _standardCode;
                    model.CheckTotalItem = cmbCheckItem.SelectedValue == null ? string.Empty : cmbCheckItem.SelectedValue.ToString();
                    model.CheckValueInfo = txtCheckValueInfo.Text.Trim();
                    model.StandValue = txtStandValue.Text.Trim();
                    model.Result = cmbResult.Text.Trim();
                    model.ResultInfo = txtResultInfo.Text.Trim();
                    //行政机构名称和编号入库有问题
                    model.UpperCompany = _upperComSelectedValue.ToString();
                    model.UpperCompanyName = cmbProduceCompany.Text.Trim();
                    model.OrCheckNo = txtOrCheckNo.Text.Trim();
                    model.CheckType = cmbCheckType.Text;
                    model.CheckUnitCode = cmbCheckUnit.SelectedValue == null ? string.Empty : cmbCheckUnit.SelectedValue.ToString();
                    model.Checker = cmbChecker.SelectedValue == null ? string.Empty : cmbChecker.SelectedValue.ToString();
                    model.Assessor = cmbAssessor.SelectedValue == null ? string.Empty : cmbAssessor.SelectedValue.ToString();
                    model.Organizer = cmbOrganizer.SelectedValue == null ? string.Empty : cmbOrganizer.SelectedValue.ToString();
                    model.Remark = txtRemark.Text.Trim();
                    model.CheckPlanCode = txtCheckPlanCode.Text.Trim();
                    model.SaleNum = txtSaleNum.Text.Trim().Equals("") ? "null" : txtSaleNum.Text.Trim();
                    model.Price = txtPrice.Text.Trim().Equals("") ? "null" : txtPrice.Text.Trim();
                    model.CheckederVal = cmbCheckerVal.Text.Trim();
                    model.IsSentCheck = cmbIsSentCheck.Text.Trim();
                    model.CheckederRemark = txtCheckerRemark.Text.Trim();
                    model.Notes = txtNotes.Text.Trim();
                    model.SampleAmount = txtSampleAmount.Text.Trim();
                    if (_saveType)
                    {
                        model.SampleSource = tb_SampleSource.Text.Trim();
                        model.Contact = tb_Contact.Text.Trim();
                        model.ContactPhone = tb_Phone.Text.Trim();
                        model.IsDestruction = tb_IsDestruction.Text.Trim();
                        model.DestructionKG = tb_Quality.Text.Trim();
                        model.Area = cb_CompanyArea.Text.Trim();
                    }
                    model.fTpye = _foodType;
                    model.testPro = _checkItemsCode;
                    model.dataNum = txtCheckNo.Text;
                    model.checkedUnit = "0";
                    //对数据库进行操作
                    string sErr = String.Empty;
                    if (_saveType)
                        _resultBll.InsertSZ(model, out sErr);
                    else
                        _resultBll.Insert(model, out sErr);
                    AddEmpty();
                    FrmMsg frm = new FrmMsg("检测记录已经保存");
                    DialogResult dlg = frm.ShowDialog();
                    if (dlg == DialogResult.OK)
                        Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "数据库操作出错:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 添加成功之后清空数据项
        /// </summary>
        private void AddEmpty()
        {
            clsResult model = new clsResult();
            string syscode = FrmMain.formMain.getNewSystemCode(false);
            model.SysCode = syscode;
            model.CheckNo = ShareOption.SysStdCodeSame ? syscode : string.Empty;
            model.StdCode = string.Empty;
            model.SampleCode = model.CheckNo;
            model.CheckedCompany = _checkedComSelectedValue;
            model.CheckedCompanyName = cmbCompany.Text;
            model.CheckedComDis = txtCompanyInfo.Text;
            model.CheckPlaceCode = string.Empty;
            model.FoodCode = _foodSelectedValue;
            model.ProduceCompany = _produceComSelectedValue;
            model.ProducePlace = _producePlaceSelectValue;
            model.SentCompany = string.Empty;
            model.Provider = string.Empty;
            model.TakeDate = DateTime.Today;
            model.CheckStartDate = DateTime.Today;
            model.ImportNum = string.Empty;
            model.SaveNum = string.Empty;
            model.Unit = ShareOption.SysUnit;
            model.SampleBaseNum = string.Empty;
            model.SampleNum = string.Empty;
            model.SampleUnit = ShareOption.SysUnit;
            model.SampleLevel = string.Empty;
            model.SampleModel = string.Empty;
            model.SampleState = string.Empty;
            model.Standard = string.Empty;
            model.CheckValueInfo = string.Empty;
            model.StandValue = txtStandValue.Text;
            model.Result = string.Empty;
            model.ResultInfo = txtResultInfo.Text;
            model.UpperCompany = _upperComSelectedValue.ToString();
            model.UpperCompanyName = cmbCompany.Text.Trim();
            model.OrCheckNo = string.Empty;
            model.CheckType = "抽检";
            model.CheckUnitCode = FrmMain.formMain.checkUnitCode;
            model.Checker = FrmMain.formMain.userCode;
            model.Assessor = string.Empty;
            model.Organizer = string.Empty;
            model.Remark = string.Empty;
            model.CheckPlanCode = string.Empty;
            model.SaleNum = string.Empty;
            model.Price = string.Empty;
            model.CheckederVal = string.Empty;
            model.IsSentCheck = string.Empty;
            model.CheckederRemark = string.Empty;
            model.Notes = string.Empty;
            setNEWValue(model);
        }

        /// <summary>
        /// 设置赋值
        /// 2015年12月31日 wenj
        /// 将某些值在保存后继续保留在界面上
        /// </summary>
        /// <param name="model"></param>
        internal void setNEWValue(clsResult model)
        {
            txtSysID.Text = model.SysCode;
            txtCheckNo.Text = model.CheckNo;
            txtStdCode.Text = model.StdCode;
            txtSampleCode.Text = model.SampleCode;
            _checkedComSelectedValue = model.CheckedCompany;
            cmbCompany.Text = model.CheckedCompanyName;
            txtCompanyInfo.Text = model.CheckedComDis;
            _foodSelectedValue = model.FoodCode;
            _produceComSelectedValue = model.ProduceCompany;
            _producePlaceSelectValue = model.ProducePlace;
            if (model.SentCompany.Length>0) txtSentCompany.Text = model.SentCompany;
            txtProvider.Text = model.Provider;
            dtpTakeDate.Value = model.TakeDate;
            dtpCheckStartDate.Value = model.CheckStartDate;
            //txtImportNum.Text = model.ImportNum;
            //txtSaveNum.Text = model.SaveNum;
            txtUnit.Text = model.Unit;
            txtSampleBaseNum.Text = model.SampleBaseNum.Equals("null") ? string.Empty : model.SampleBaseNum.ToString();
            //txtSampleNum.Text = model.SampleNum.Equals("null") ? string.Empty : model.SampleNum.ToString();
            txtSampleUnit.Text = model.SampleUnit;
            txtSampleLevel.Text = model.SampleLevel;
            txtSampleModel.Text = model.SampleModel;
            txtSampleState.Text = model.SampleState;
            //txtCheckValueInfo.Text = model.CheckValueInfo;
            txtStandValue.Text = model.StandValue;
            //cmbResult.Text = model.Result;
            txtResultInfo.Text = model.ResultInfo;
            _upperComSelectedValue = model.UpperCompany;
            cmbUpperCompany.Text = model.UpperCompanyName;
            txtOrCheckNo.Text = model.OrCheckNo;
            switch (model.CheckType)
            {
                case "抽检":
                    cmbCheckType.SelectedIndex = 0;
                    break;
                case "送检":
                    cmbCheckType.SelectedIndex = 1;
                    break;
                case "抽检复检":
                    cmbCheckType.SelectedIndex = 2;
                    break;
                case "送检复检":
                    cmbCheckType.SelectedIndex = 3;
                    break;
            }
            txtRemark.Text = model.Remark;
            txtCheckPlanCode.Text = model.CheckPlanCode;
            txtSaleNum.Text = model.SaleNum.Equals("null") ? string.Empty : model.SaleNum.ToString();
            //txtPrice.Text = model.Price.Equals("null") ? string.Empty : model.Price.ToString();
            txtCheckerRemark.Text = model.CheckederRemark;
            txtNotes.Text = model.Notes;
        }

        /// <summary>
        /// 设置赋值
        /// </summary>
        /// <param name="model"></param>
        internal void setValue(clsResult model)
        {
            _machineCode = model.CheckMachine;
            txtSysID.Text = model.SysCode;
            txtCheckNo.Text = model.CheckNo;
            txtStdCode.Text = model.StdCode;
            txtSampleCode.Text = model.CheckNo;// model.SampleCode;
            _checkedComSelectedValue = model.CheckedCompany;
            cmbCompany.Text = model.CheckedCompanyName;
            txtCompanyInfo.Text = model.CheckedComDis;
            _foodSelectedValue = model.FoodCode;
            txtProduceDate.Text = string.Empty;
            DateTime? tempdt = model.ProduceDate;
            if (tempdt != null)
                txtProduceDate.Text = Convert.ToDateTime(tempdt).ToString("yyyy-MM-dd");
            _produceComSelectedValue = model.ProduceCompany;
            _producePlaceSelectValue = model.ProducePlace;
            if (model.SentCompany.Length>0) txtSentCompany.Text = model.SentCompany;
            txtProvider.Text = model.Provider;
            dtpTakeDate.Value = model.TakeDate;
            dtpCheckStartDate.Value = model.CheckStartDate;
            txtImportNum.Text = model.ImportNum;
            txtSaveNum.Text = model.SaveNum;
            txtUnit.Text = model.Unit;
            txtSampleBaseNum.Text = model.SampleBaseNum.Equals("null") ? string.Empty : model.SampleBaseNum.ToString();
            txtSampleNum.Text = model.SampleNum.Equals("null") ? string.Empty : model.SampleNum.ToString();
            txtSampleUnit.Text = model.SampleUnit;
            txtSampleLevel.Text = model.SampleLevel;
            txtSampleModel.Text = model.SampleModel;
            txtSampleState.Text = model.SampleState;
            txtCheckValueInfo.Text = model.CheckValueInfo;
            txtStandValue.Text = model.StandValue;
            cmbResult.Text = model.Result;
            txtResultInfo.Text = model.ResultInfo;
            _upperComSelectedValue = model.UpperCompany;
            cmbUpperCompany.Text = model.UpperCompanyName;
            txtOrCheckNo.Text = model.OrCheckNo;
            switch (model.CheckType)
            {
                case "抽检":
                    cmbCheckType.SelectedIndex = 0;
                    break;
                case "送检":
                    cmbCheckType.SelectedIndex = 1;
                    break;
                case "抽检复检":
                    cmbCheckType.SelectedIndex = 2;
                    break;
                case "送检复检":
                    cmbCheckType.SelectedIndex = 3;
                    break;
            }
            txtRemark.Text = model.Remark;
            txtCheckPlanCode.Text = model.CheckPlanCode;
            txtSaleNum.Text = model.SaleNum.Equals("null") ? string.Empty : model.SaleNum.ToString();
            txtPrice.Text = model.Price.Equals("null") ? string.Empty : model.Price.ToString();
            switch (model.CheckederVal)
            {
                case "":
                    cmbCheckerVal.SelectedIndex = 2;
                    break;
                case "无异议":
                    cmbCheckerVal.SelectedIndex = 0;
                    break;
                case "有异议":
                    cmbCheckerVal.SelectedIndex = 1;
                    break;
            }
            switch (model.IsSentCheck)
            {
                case "":
                    cmbIsSentCheck.SelectedIndex = 2;
                    break;
                case "否":
                    cmbIsSentCheck.SelectedIndex = 0;
                    break;
                case "是":
                    cmbIsSentCheck.SelectedIndex = 1;
                    break;
            }
            txtCheckerRemark.Text = model.CheckederRemark;
            txtNotes.Text = model.Notes;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            windClose();
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (_afterAdded)
                DialogResult = DialogResult.OK;
            windClose();
        }

        private void windClose()
        {
            this.Dispose();
        }

        #region 无效代码
        ///// <summary>
        ///// 仪器数据改变时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cmbCheckMachine_SelectedValueChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        string protocol = clsMachineOpr.GetNameFromCode("Protocol", cmbCheckMachine.SelectedValue.ToString());
        //        switch (protocol)
        //        {
        //            case "RS232水分仪插件":
        //                lblSuppresser.Text = "水分含量：";
        //                break;

        //            case "RS232DY3000DY":
        //                if (cmbCheckMachine.SelectedValue.ToString().Equals("018"))//DY3000
        //                {
        //                    lblSuppresser.Text = "抑制率：";
        //                }
        //                else
        //                {
        //                    lblSuppresser.Text = "检测值：";
        //                }
        //                break;
        //            default:
        //                lblSuppresser.Text = "检测值：";
        //                break;
        //        }
        //        string linkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", cmbCheckMachine.SelectedValue.ToString());

        //        if (protocol.Equals("RS232DY3000DY"))
        //        {
        //            checkItems = StringUtil.GetDY3000DYAry(linkStdCode);
        //        }
        //        else
        //        {
        //            checkItems = StringUtil.GetAry(linkStdCode);
        //        }
        //        if (checkItems.GetLength(0) <= 0) return;

        //        if (checkItems.GetLength(0) >= 1 && checkItems[0, 1].ToString() != "-1")
        //        {
        //            checkItemCode = checkItems[0, 1].ToString();
        //        }
        //        if (checkItems.GetLength(0) == 1 && checkItems[0, 1].ToString() == "-1")
        //        {
        //            checkItemCode = string.Empty;
        //        }
        //        cmbCheckItem.Enabled = true;

        //        string strWhere = string.Empty;
        //        if (checkItems.GetLength(0) > 1)
        //        {
        //            string strSql = string.Empty;
        //            bool blExist = false;

        //            for (int i = 0; i < checkItems.GetLength(0); i++)
        //            {
        //                if (checkItems[i, 1].ToString() != "-1")
        //                {
        //                    strSql = strSql + "'" + checkItems[i, 1].ToString() + "',";
        //                }
        //                if (checkItems[i, 1].ToString() == checkItemCode) blExist = true;
        //            }
        //            if (!blExist)
        //            {
        //                checkItemCode = string.Empty;
        //            }
        //            strSql = strSql.Substring(0, strSql.Length - 1);
        //            strWhere = "IsLock=false AND SysCode IN(" + strSql + ")";
        //        }
        //        else
        //        {
        //            strWhere = "IsLock=false AND SysCode ='" + checkItems[0, 1].ToString() + "'";
        //        }
        //        bindCheckItem(strWhere);

        //        if (checkItemCode.Equals(""))
        //        {
        //            cmbCheckItem.SelectedIndex = -1;
        //            cmbCheckItem.Text = string.Empty;
        //        }
        //        else
        //        {
        //            cmbCheckItem.SelectedValue = checkItemCode;
        //            cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(checkItemCode);
        //        }
        //        if (checkItems.GetLength(0) > 1)
        //        {
        //            cmbCheckItem.Enabled = true;
        //        }
        //        else
        //        {
        //            cmbCheckItem.Enabled = false;
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置仪器！");
        //    }
        //}

        //private void chkNoHaveMachine_CheckedChanged(object sender, System.EventArgs e)
        //{
        //   // cmbCheckMachine.Enabled = !chkNoHaveMachine.Checked;
        //    cmbCheckItem.Enabled = chkNoHaveMachine.Checked;
        //    //if (!chkNoHaveMachine.Checked)
        //    //{
        //    //    //sResultType = ShareOption.ResultTypeCode1;
        //    //    cmbCheckMachine_SelectedValueChanged(null, null);
        //    //}
        //    //else
        //    if (chkNoHaveMachine.Checked)
        //    {
        //       // sResultType = ShareOption.ResultTypeCode3;
        //        bindCheckItem("IsLock=false");

        //        //clsCheckItemOpr opr1 = new clsCheckItemOpr();
        //        //DataTable dt1 = opr1.GetAsDataTable("IsLock=false", "SysCode", 1);
        //        //cmbCheckItem.DataSource = dt1.DataSet;
        //        //cmbCheckItem.DataMember = "CheckItem";
        //        //cmbCheckItem.DisplayMember = "ItemDes";
        //        //cmbCheckItem.ValueMember = "SysCode";
        //        //cmbCheckItem.Columns["StdCode"].Caption = "编号";
        //        //cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
        //        //cmbCheckItem.Columns["SysCode"].Caption = "系统编号";
        //        cmbCheckItem.SelectedIndex = -1;
        //        lblSuppresser.Text = "检测值：";
        //    }
        //}
        #endregion

        private void cmbCheckItem_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (cmbCheckItem.SelectedValue != null)
            {
                txtResultInfo.Text = clsCheckItemOpr.GetUnitFromCode(cmbCheckItem.SelectedValue.ToString());
                _standardCode = clsCheckItemOpr.GetStandardCode(cmbCheckItem.SelectedValue.ToString());
                txtStandard.Text = clsStandardOpr.GetNameFromCode(_standardCode);
                txtStandValue.Text = clsStandardOpr.GetStandValueFromCode(_standardCode);
                _dTestValue = Convert.ToDecimal(txtStandValue.Text);
                _sign = clsStandardOpr.GetSignValueFromCode(_standardCode);
                //if (!_foodSelectedValue.Equals(""))
                //{
                //    string[] strResult = clsFoodClassOpr.ValueFromCode(_foodSelectedValue.ToString(), cmbCheckItem.SelectedValue.ToString());
                //    _sign = strResult[0];
                //    _dTestValue = Convert.ToDecimal(strResult[1]);
                //    _checkUnit = strResult[2];
                //    if (_sign.Equals("-1") && _dTestValue == 0 && _checkUnit.Equals("-1"))
                //    {
                //        _foodSelectedValue = string.Empty;
                //        cmbFood.Text = string.Empty;
                //        txtStandValue.Text = string.Empty;
                //    }
                //    else
                //        txtStandValue.Text = _dTestValue.ToString();
                //}
            }
            else
            {
                _standardCode = string.Empty;
                txtStandard.Text = string.Empty;
            }
        }

        private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (cmbCheckItem.SelectedValue != null)
            //{
            frmFoodSelect frm = new frmFoodSelect("", _foodSelectedValue);
            clsFoodClassOpr foodBll = new clsFoodClassOpr();
            //DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + cmbCheckItem.SelectedValue.ToString() + ":%'", "SysCode", 0);
            DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true", "SysCode", 0);
            if (dt.Rows.Count > 0)
            {
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                {
                    _foodSelectedValue = frm.sNodeTag;
                    cmbFood.Text = frm.sNodeName;
                    //_sign = frm.sSign;
                    //try
                    //{
                    //    _dTestValue = Convert.ToDecimal(frm.sValue);
                    //}
                    //catch (Exception)
                    //{
                    //    _dTestValue = 0;
                    //}
                    //_checkUnit = frm.sUnit;
                    //txtStandValue.Text = frm.sValue;
                    #region @zh 2016/11/12 合肥
                    //@zh 获取所对应的样品的样品种类
                    //string sysCode = _foodSelectedValue.Substring(0, _foodSelectedValue.Length - 5);
                    string sysCode = _foodSelectedValue.Substring(0, 10);
                    dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and SysCode= '" + sysCode + "'", "SysCode", 0);
                    if (dt.Rows.Count > 0)
                    {
                        textFoodType.Text = dt.Rows[0]["Name"].ToString();
                        _foodType = sysCode;
                    }
                    #endregion
                }
            }
            else
                MessageBox.Show(this, "该检测项目无对应检测样品！");
            //}
            //else
            //{
            //    if (cmbCheckMachine.SelectedValue != null)
            //        MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置！");
            //    else
            //        MessageBox.Show(this, "没有选择检测项目！");
            //}
            //e.Cancel = true;
            setTestValue();
            e.Cancel = true;
            btnOK.Focus();
        }

        private void cmbCheckType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cmbCheckType.SelectedIndex)
            {
                case 0:
                    txtSentCompany.Enabled = false;
                    txtOrCheckNo.Enabled = false;
                    break;

                case 1:
                    txtOrCheckNo.Enabled = false;
                    txtSentCompany.Enabled = true;
                    break;

                case 2:
                    txtSentCompany.Enabled = false;
                    txtOrCheckNo.Enabled = true;
                    break;

                case 3:
                    txtOrCheckNo.Enabled = true;
                    txtSentCompany.Enabled = true;
                    break;
            }
        }

        private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmCheckedComSelect frm = new frmCheckedComSelect(string.Empty, _produceComSelectedValue);
            frm.Tag = "Produce";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                _produceComSelectedValue = frm.sNodeTag;
                cmbProduceCompany.Text = frm.sNodeName;
            }
            else
            {
                _produceComSelectedValue = string.Empty;
                cmbProduceCompany.Text = string.Empty;
            }
            e.Cancel = true;
        }

        private void cmbProducePlace_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmProduceAreaSelect frm = new frmProduceAreaSelect(_producePlaceSelectValue);
            frm.Tag = "ProducePlace";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                _producePlaceSelectValue = frm.sNodeTag;
                cmbProducePlace.Text = frm.sNodeName;
            }
            else
            {
                _producePlaceSelectValue = string.Empty;
                cmbProducePlace.Text = string.Empty;
            }
            e.Cancel = true;
        }

        private void txtCheckValueInfo_LostFocus(object sender, EventArgs e)
        {
            if (StringUtil.IsNumeric(txtCheckValueInfo.Text) && txtCheckValueInfo.Text != string.Empty)
            {
                setTestValue();
                return;
            }
            if (string.IsNullOrEmpty(_machineCode))
                return;

            txtCheckValueInfo.Text = txtCheckValueInfo.Text.Trim();
            if (txtCheckValueInfo.Text.Equals(""))
            {
                MessageBox.Show(this, lblSuppresser.Text.Replace("：", "") + "必须输入！");
                //txtCheckValueInfo.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtCheckValueInfo.Text))
            {
                MessageBox.Show(this, lblSuppresser.Text.Replace("：", "") + "必须为数字类型");
                // txtCheckValueInfo.Focus();
                return;
            }
            setTestValue();
        }

        private void setTestValue()
        {
            if (txtCheckValueInfo.Text.Trim().Length == 0)
                txtCheckValueInfo.Text = "0";

            if (!_sign.Equals("≤")) _sign = "≤";
            if (!_checkUnit.Equals("%")) _checkUnit = "%";
            if (int.Parse(txtStandValue.Text.Trim()) <= 0) txtStandValue.Text = "40";
            
            decimal checkValue = Decimal.Parse(txtCheckValueInfo.Text);
            switch (_sign)
            {
                case "<":
                    if (checkValue >= _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＜":
                    if (checkValue >= _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≤":
                    if (checkValue > _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case ">":
                    if (checkValue <= _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＞":
                    if (checkValue <= _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≥":
                    if (checkValue < _dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
            }
            //如果是工商版本同时是不合格的时候
            //产品产地、生产单位、生产日期、进货数量、库存数量 是必录项
            if (ShareOption.ApplicationTag == ShareOption.ICAppTag && cmbResult.Text == "不合格")
            {
                lblPerImportNumTag.Visible = true;
                lblPerProduceComTag.Visible = true;
                lblPerProduceDateTag.Visible = true;
                lblPerProduceTag.Visible = true;
                lblPerSaveNumTag.Visible = true;
            }
        }

        private void cmbUpperCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            if (!FrmMain.IsLoadCheckedUpperComSel)
            {
                FrmMain.formCheckedUpperComSelect = new frmCheckedComSelect("", _upperComSelectedValue);
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
            }
            else
            {
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
                FrmMain.formCheckedUpperComSelect.SetFormValues("", _upperComSelectedValue);
            }
            FrmMain.formCheckedUpperComSelect.ShowDialog(this);
            if (FrmMain.formCheckedUpperComSelect.DialogResult == DialogResult.OK)
            {
                if (_upperComSelectedValue.Equals("") || (!_upperComSelectedValue.Equals(FrmMain.formCheckedUpperComSelect.sNodeTag)))
                {
                    _upperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                    _checkedComSelectedValue = string.Empty;
                    cmbCheckedCompany.Text = string.Empty;
                    txtCompanyInfo.Text = string.Empty;
                }
                else
                {
                    _upperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                }
            }
            FrmMain.formCheckedUpperComSelect.Hide();
            e.Cancel = true;
        }

        private void cmbCheckedCompany_LostFocus(object sender, EventArgs e)
        {
            if ((!_checkedComSelectedValue.Equals("")) && cmbCheckedCompany.ComboStyle == C1.Win.C1List.ComboStyleEnum.DropdownCombo)
            {
                string strComName = clsCompanyOpr.Namefullname(_checkedComSelectedValue);
                if (!cmbCheckedCompany.Text.Trim().Equals(strComName))
                    _checkedComSelectedValue = string.Empty;
            }
        }

        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        private void dtpProduceDate_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProduceDate.Text))
                txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        private void dtpCheckStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpTakeDate.Value = dtpCheckStartDate.Value;
        }

        /// <summary>
        /// 初始化受检人/被检单位
        /// </summary>
        private void BindCompanies()
        {
            cmbCompany.DataSource = new clsCompanyOpr().GetAllCompanies();
            cmbCompany.DisplayMember = "FullName";
            cmbCompany.ValueMember = "SysCode";
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbCompany.SelectedItem) != null)
            {
                _checkedComSelectedValue = ((DataRowView)cmbCompany.SelectedItem)["fullname"].ToString();
                txtCompanyInfo.Text = clsCompanyOpr.CompanyInfo(_checkedComSelectedValue);
                txtCiname.Text = clsProprietorsOpr.CiidNameFromCode(_checkedComSelectedValue);
                if (txtCiname.Text.Equals(""))
                    txtCiname.Text = cmbCompany.Text;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!FrmMain.IsLoadCheckedComSel)
            {
                FrmMain.formCheckedComSelect = new frmCheckedCompany("", _checkedComSelectedValue);
                FrmMain.formCheckedComSelect.Tag = "Checked";
            }
            else
            {
                FrmMain.formCheckedComSelect.Tag = "Checked";
                //FrmMain.formCheckedComSelect.SetFormValues("", checkedComSelectedValue);
            }
            FrmMain.formCheckedComSelect.ShowDialog(this);
            if (FrmMain.formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                cmbCompany.Text = FrmMain.formCheckedComSelect.sNodeName;
                txtCiname.Text = FrmMain.formCheckedComSelect.sParentCompanyName;
                _checkedComSelectedValue = FrmMain.formCheckedComSelect.sNodeTag;
                txtCompanyInfo.Text = FrmMain.formCheckedComSelect.sDisplayName;
            }
            FrmMain.formCheckedComSelect.Hide();
        }

        private void dtpCheckStartDate_DropDown(object sender, EventArgs e)
        {
            //dtpCheckStartDate.Value = DateTime.Now;
        }

        #region 验证输入并自动赋值

        private void txtSampleUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.txtSampleUnit.ToString().Equals(""))
                this.txtUnit.Text = this.txtSampleUnit.Text.Trim();
        }

        private void txtUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.txtUnit.ToString().Equals(""))
                this.txtSampleUnit.Text = this.txtUnit.Text.Trim();
        }

        private void txtSaveNum_TextChanged(object sender, EventArgs e)
        {
            double SaveNum = 0, ImportNum = 0;
            if (double.TryParse(txtSaveNum.Text.Trim(), out SaveNum) && double.TryParse(txtImportNum.Text.Trim(), out ImportNum))
            {
                txtSaleNum.Text = (ImportNum - SaveNum).ToString("f2");
            }
        }

        private void txtImportNum_TextChanged(object sender, EventArgs e)
        {
            double ImportNum = 0, SaveNum = 0;
            if (double.TryParse(txtImportNum.Text.Trim(), out ImportNum) && double.TryParse(txtSaveNum.Text.Trim(), out SaveNum))
            {
                txtSaleNum.Text = (ImportNum - SaveNum).ToString("f2");
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            double price = 0, sampleAmount = 0;
            if (double.TryParse(txtPrice.Text.Trim(), out price) && double.TryParse(txtSampleAmount.Text.Trim(), out sampleAmount))
            {
                txtSampleNum.Text = (sampleAmount / price).ToString("f2");
            }
        }

        private void txtSampleAmount_TextChanged(object sender, EventArgs e)
        {
            double price = 0, sampleAmount = 0;
            if (double.TryParse(txtPrice.Text.Trim(), out price) && double.TryParse(txtSampleAmount.Text.Trim(), out sampleAmount))
            {
                txtSampleNum.Text = (sampleAmount / price).ToString("f2");
            }
        }

        private void txtImportNum_Validated(object sender, EventArgs e)
        {
            if (txtImportNum.Text.Trim().Length > 0)
            {
                double ImportNum = 0;
                if (double.TryParse(txtImportNum.Text.Trim(), out ImportNum))
                {
                    txtImportNum.Text = ImportNum.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtImportNum.Focus();
                }
            }
        }

        private void txtPrice_Validated(object sender, EventArgs e)
        {
            if (txtPrice.Text.Trim().Length > 0)
            {
                double Price = 0;
                if (double.TryParse(txtPrice.Text.Trim(), out Price))
                {
                    txtPrice.Text = Price.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtPrice.Focus();
                }
            }
        }

        private void txtSampleNum_Validated(object sender, EventArgs e)
        {
            if (txtSampleNum.Text.Trim().Length > 0)
            {
                double SampleNum = 0;
                if (double.TryParse(txtSampleNum.Text.Trim(), out SampleNum))
                {
                    txtSampleNum.Text = SampleNum.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtSampleNum.Focus();
                }
            }
        }

        private void txtSaleNum_Validated(object sender, EventArgs e)
        {
            if (txtSaleNum.Text.Trim().Length > 0)
            {
                double SaleNum = 0;
                if (double.TryParse(txtSaleNum.Text.Trim(), out SaleNum))
                {
                    txtSaleNum.Text = SaleNum.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtSaleNum.Focus();
                }
            }
        }

        private void txtSaveNum_Validated(object sender, EventArgs e)
        {
            if (txtSaveNum.Text.Trim().Length > 0)
            {
                double SaveNum = 0;
                if (double.TryParse(txtSaveNum.Text.Trim(), out SaveNum))
                {
                    txtSaveNum.Text = SaveNum.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtSaveNum.Focus();
                }
            }
        }

        private void txtSampleAmount_Validated(object sender, EventArgs e)
        {
            if (txtSampleAmount.Text.Trim().Length > 0)
            {
                double SampleAmount = 0;
                if (double.TryParse(txtSampleAmount.Text.Trim(), out SampleAmount))
                {
                    txtSampleAmount.Text = SampleAmount.ToString("F2");
                }
                else
                {
                    MessageBox.Show("请输入正确的值!", "操作提示");
                    txtImportNum.Focus();
                }
            }
        }

        #endregion

        private void cmbCheckItemAnHui_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbCheckItemAnHui.SelectedItem) != null)
            {
                _checkItemsCode = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["codeId"].ToString();
                cmbCheckItemAnHui.Text = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["name"].ToString();
            }
        }

        private void cmbFoodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbFoodType.SelectedItem) != null)
            {
                _foodType = ((DataRowView)cmbFoodType.SelectedItem)["codeId"].ToString();
                cmbFoodType.Text = ((DataRowView)cmbFoodType.SelectedItem)["name"].ToString();
            }
        }
        private clsCompanyOpr clsCompanyOpr = new clsCompanyOpr();
        private void cmbCheckItem_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '检测项目' AND name like '%"
                + cmbCheckItem.Text.Substring(0, 1) + "%'", 1, "CheckItem");
            if (dt == null || dt.Rows.Count == 0)
            {
                dt = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '检测项目'", 1, "CheckItem");
            }
            //检测项目
            cmbCheckItemAnHui.DataSource = dt;
            cmbCheckItemAnHui.DisplayMember = "name";
            cmbCheckItemAnHui.ValueMember = "codeId";
        }

        private void txtCheckNo_TextChanged(object sender, EventArgs e)
        {
            txtSampleCode.Text = txtCheckNo.Text.Trim();
        }

        private void txtCheckValueInfo_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckValueInfo.Text.Trim().Length == 0)
                txtCheckValueInfo.Text = "0";
            if (txtStandValue.Text.Trim().Length == 0) txtStandValue.Text = "40";

            decimal checkValue = Decimal.Parse(txtCheckValueInfo.Text);
            decimal dTestValue = Decimal.Parse(txtStandValue.Text);
            switch ("≤")
            {
                case "<":
                    if (checkValue >= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＜":
                    if (checkValue >= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≤":
                    if (checkValue > dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case ">":
                    if (checkValue <= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＞":
                    if (checkValue <= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≥":
                    if (checkValue < dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
            }
        }
    }
}