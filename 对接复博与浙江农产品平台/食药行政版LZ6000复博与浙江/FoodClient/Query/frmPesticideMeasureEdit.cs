using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// frmPesticideMeasureEdit 的摘要说明。
    /// </summary>
    public class frmPesticideMeasureEdit : TitleBarBase
    {
        #region 窗体变量
        private System.Windows.Forms.TextBox txtSampleNum;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.TextBox txtSampleBaseNum;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOK;
        private C1.Win.C1List.C1Combo cmbFood;
        private System.Windows.Forms.Label lblResult;
        private C1.Win.C1List.C1Combo cmbCheckUnit;
        private System.Windows.Forms.Label lblReferStandard;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTakeDate;
        private System.Windows.Forms.DateTimePicker dtpCheckStartDate;
        private C1.Win.C1List.C1Combo cmbChecker;
        private System.Windows.Forms.Label lblSuppresser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSampleModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSampleLevel;
        private System.Windows.Forms.TextBox txtSampleState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private C1.Win.C1List.C1Combo cmbAssessor;
        private C1.Win.C1List.C1Combo cmbOrganizer;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label7;
        public C1.Win.C1List.C1Combo cmbCheckMachine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkNoHaveMachine;
        private C1.Win.C1List.C1Combo cmbCheckItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCompanyInfo;
        private C1.Win.C1List.C1Combo cmbResult;
        private System.Windows.Forms.TextBox txtStandValue;
        private System.Windows.Forms.Label label30;
        private C1.Win.C1List.C1Combo cmbProducePlace;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtImportNum;
        private System.Windows.Forms.TextBox txtCheckValueInfo;
        private System.Windows.Forms.TextBox txtSaveNum;
        private System.Windows.Forms.TextBox txtSentCompany;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.TextBox txtSampleCode;
        private System.Windows.Forms.TextBox txtSampleUnit;
        private C1.Win.C1List.C1Combo cmbProduceCompany;
        private System.Windows.Forms.TextBox txtOrCheckNo;
        private System.Windows.Forms.TextBox txtStdCode;
        private System.Windows.Forms.ComboBox cmbCheckType;

        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtResultInfo;
        private C1.Win.C1List.C1Combo cmbUpperCompany;
        private System.Windows.Forms.ComboBox cmbCheckerVal;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtCheckerRemark;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSaleNum;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox cmbIsSentCheck;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtCheckPlanCode;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDomain;
        private System.ComponentModel.Container components = null;
        #endregion

        private bool BigTip = false;
        private static string strMachineCode = string.Empty;
        private static string strStandardCode = string.Empty;
        private static string strCheckItemCode = string.Empty;
        private static string strSign = string.Empty;
        private static decimal dTestValue = 0;
        private static string strUnit = string.Empty;
        private string[,] strCheckItems;
        private string checkunitcode;
        private string assessor;
        private string organizer;
        private string checker;
        private string sFoodSelectedValue = string.Empty;
        private string sProduceComSelectedValue = string.Empty;
        private string sCheckedComSelectedValue = string.Empty;
        private string sProducePlaceSelectValue = string.Empty;
        private TextBox txtProduceDate;
        private string sCheckedUpperComSelectedValue = string.Empty;
        private ComboBox cmbCheckedCompany;
        private Button btnSelect;
        private int queryType;
        private ComboBox cmbCheckItemAnHui;
        private Label label21;
        private ComboBox cmbFoodType;
        private Label label26;
        //时间控件
        private DateTimePicker dtpProduceDate;
        private TextBox textFoodType;
        private Label label16;
        private Label label40;
        private TextBox txtbarcode;
        private clsResultOpr clsResultOpr = new clsResultOpr();

        /// <summary>
        /// 检测数据修改
        /// </summary>
        /// <param name="selectType">1代表标准速测法查询,2表示其他查询, 3表示综合查询</param>
        public frmPesticideMeasureEdit(int selectType)
        {
            InitializeComponent();
            queryType = selectType;
            BindCompanies();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesticideMeasureEdit));
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
            this.txtSampleNum = new System.Windows.Forms.TextBox();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.txtSampleBaseNum = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
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
            this.label28 = new System.Windows.Forms.Label();
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
            this.txtSampleModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSampleLevel = new System.Windows.Forms.TextBox();
            this.txtSampleState = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSaveNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSentCompany = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbAssessor = new C1.Win.C1List.C1Combo();
            this.cmbOrganizer = new C1.Win.C1List.C1Combo();
            this.label35 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCheckMachine = new C1.Win.C1List.C1Combo();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkNoHaveMachine = new System.Windows.Forms.CheckBox();
            this.cmbCheckItem = new C1.Win.C1List.C1Combo();
            this.txtSampleCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSampleUnit = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.cmbProduceCompany = new C1.Win.C1List.C1Combo();
            this.label17 = new System.Windows.Forms.Label();
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
            this.cmbUpperCompany = new C1.Win.C1List.C1Combo();
            this.cmbCheckerVal = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtCheckerRemark = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSaleNum = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.cmbIsSentCheck = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtCheckPlanCode = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txtProduceDate = new System.Windows.Forms.TextBox();
            this.cmbCheckedCompany = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dtpProduceDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCheckItemAnHui = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbFoodType = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textFoodType = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtbarcode = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // txtSampleNum
            // 
            this.txtSampleNum.Location = new System.Drawing.Point(1039, 268);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleNum.TabIndex = 19;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Enabled = false;
            this.txtCheckNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckNo.Location = new System.Drawing.Point(122, 76);
            this.txtCheckNo.MaxLength = 50;
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(150, 26);
            this.txtCheckNo.TabIndex = 2;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(909, 616);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 72;
            this.txtSysID.Visible = false;
            // 
            // txtSampleBaseNum
            // 
            this.txtSampleBaseNum.Location = new System.Drawing.Point(1032, 198);
            this.txtSampleBaseNum.Name = "txtSampleBaseNum";
            this.txtSampleBaseNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleBaseNum.TabIndex = 20;
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(122, 458);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(398, 41);
            this.txtRemark.TabIndex = 43;
            // 
            // txtStandard
            // 
            this.txtStandard.Location = new System.Drawing.Point(1032, 58);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.Size = new System.Drawing.Size(150, 21);
            this.txtStandard.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(829, 616);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 71;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(360, 602);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 26);
            this.btnOK.TabIndex = 46;
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
            this.cmbFood.Location = new System.Drawing.Point(122, 196);
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
            this.cmbFood.TabIndex = 7;
            this.cmbFood.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbFood_BeforeOpen);
            this.cmbFood.PropBag = resources.GetString("cmbFood.PropBag");
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(309, 279);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(66, 21);
            this.lblResult.TabIndex = 64;
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
            this.cmbCheckUnit.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
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
            this.cmbCheckUnit.Location = new System.Drawing.Point(1036, 576);
            this.cmbCheckUnit.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckUnit.MaxDropDownItems = ((short)(5));
            this.cmbCheckUnit.MaxLength = 10;
            this.cmbCheckUnit.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckUnit.Name = "cmbCheckUnit";
            this.cmbCheckUnit.OddRowStyle = style14;
            this.cmbCheckUnit.PartialRightColumn = false;
            this.cmbCheckUnit.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckUnit.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.SelectedStyle = style15;
            this.cmbCheckUnit.Size = new System.Drawing.Size(150, 22);
            this.cmbCheckUnit.Style = style16;
            this.cmbCheckUnit.TabIndex = 40;
            this.cmbCheckUnit.PropBag = resources.GetString("cmbCheckUnit.PropBag");
            // 
            // lblReferStandard
            // 
            this.lblReferStandard.Location = new System.Drawing.Point(965, 58);
            this.lblReferStandard.Name = "lblReferStandard";
            this.lblReferStandard.Size = new System.Drawing.Size(66, 21);
            this.lblReferStandard.TabIndex = 97;
            this.lblReferStandard.Text = "检测依据：";
            this.lblReferStandard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSample
            // 
            this.lblSample.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSample.Location = new System.Drawing.Point(35, 199);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(92, 21);
            this.lblSample.TabIndex = 54;
            this.lblSample.Text = "商品名称：";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(5, 159);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(123, 21);
            this.lblName.TabIndex = 74;
            this.lblName.Text = "受检人/单位：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(961, 198);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 21);
            this.label22.TabIndex = 81;
            this.label22.Text = "抽样基数：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(39, 469);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 21);
            this.label23.TabIndex = 68;
            this.label23.Text = "处理情况：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(32, 79);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(92, 21);
            this.label25.TabIndex = 50;
            this.label25.Text = "检测编号：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(976, 268);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 21);
            this.label27.TabIndex = 58;
            this.label27.Text = "抽样数量：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(973, 577);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(66, 21);
            this.label28.TabIndex = 67;
            this.label28.Text = "检测单位：";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImportNum
            // 
            this.txtImportNum.Location = new System.Drawing.Point(1036, 453);
            this.txtImportNum.MaxLength = 50;
            this.txtImportNum.Name = "txtImportNum";
            this.txtImportNum.Size = new System.Drawing.Size(150, 21);
            this.txtImportNum.TabIndex = 22;
            // 
            // txtCheckValueInfo
            // 
            this.txtCheckValueInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckValueInfo.Location = new System.Drawing.Point(122, 236);
            this.txtCheckValueInfo.Name = "txtCheckValueInfo";
            this.txtCheckValueInfo.Size = new System.Drawing.Size(150, 26);
            this.txtCheckValueInfo.TabIndex = 33;
            this.txtCheckValueInfo.TextChanged += new System.EventHandler(this.txtCheckValueInfo_TextChanged);
            this.txtCheckValueInfo.LostFocus += new System.EventHandler(this.txtCheckValueInfo_LostFocus);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(973, 453);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 21);
            this.label13.TabIndex = 59;
            this.label13.Text = "进货数量：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(285, 321);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 21);
            this.label18.TabIndex = 84;
            this.label18.Text = "抽样日期：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(50, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 86;
            this.label1.Text = "检测人：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTakeDate
            // 
            this.dtpTakeDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTakeDate.Location = new System.Drawing.Point(370, 316);
            this.dtpTakeDate.Name = "dtpTakeDate";
            this.dtpTakeDate.Size = new System.Drawing.Size(150, 26);
            this.dtpTakeDate.TabIndex = 29;
            // 
            // dtpCheckStartDate
            // 
            this.dtpCheckStartDate.CustomFormat = "";
            this.dtpCheckStartDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCheckStartDate.Location = new System.Drawing.Point(370, 356);
            this.dtpCheckStartDate.Name = "dtpCheckStartDate";
            this.dtpCheckStartDate.Size = new System.Drawing.Size(150, 26);
            this.dtpCheckStartDate.TabIndex = 30;
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
            this.cmbChecker.Location = new System.Drawing.Point(122, 356);
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
            this.cmbChecker.TabIndex = 38;
            this.cmbChecker.PropBag = resources.GetString("cmbChecker.PropBag");
            // 
            // lblSuppresser
            // 
            this.lblSuppresser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSuppresser.Location = new System.Drawing.Point(56, 239);
            this.lblSuppresser.Name = "lblSuppresser";
            this.lblSuppresser.Size = new System.Drawing.Size(72, 21);
            this.lblSuppresser.TabIndex = 0;
            this.lblSuppresser.Text = "抑制率：";
            this.lblSuppresser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(285, 359);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 21);
            this.label10.TabIndex = 96;
            this.label10.Text = "检测日期：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleModel
            // 
            this.txtSampleModel.Location = new System.Drawing.Point(1038, 337);
            this.txtSampleModel.Name = "txtSampleModel";
            this.txtSampleModel.Size = new System.Drawing.Size(150, 21);
            this.txtSampleModel.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(967, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 75;
            this.label2.Text = "规格型号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleLevel
            // 
            this.txtSampleLevel.Location = new System.Drawing.Point(1032, 129);
            this.txtSampleLevel.MaxLength = 50;
            this.txtSampleLevel.Name = "txtSampleLevel";
            this.txtSampleLevel.Size = new System.Drawing.Size(150, 21);
            this.txtSampleLevel.TabIndex = 9;
            // 
            // txtSampleState
            // 
            this.txtSampleState.Location = new System.Drawing.Point(1036, 526);
            this.txtSampleState.MaxLength = 50;
            this.txtSampleState.Name = "txtSampleState";
            this.txtSampleState.Size = new System.Drawing.Size(150, 21);
            this.txtSampleState.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(959, 526);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 55;
            this.label3.Text = "批号或编号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(965, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 21);
            this.label12.TabIndex = 89;
            this.label12.Text = "质量等级：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaveNum
            // 
            this.txtSaveNum.Location = new System.Drawing.Point(1039, 312);
            this.txtSaveNum.MaxLength = 20;
            this.txtSaveNum.Name = "txtSaveNum";
            this.txtSaveNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaveNum.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(968, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 21);
            this.label8.TabIndex = 83;
            this.label8.Text = "库存数量：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSentCompany
            // 
            this.txtSentCompany.Location = new System.Drawing.Point(1038, 406);
            this.txtSentCompany.Name = "txtSentCompany";
            this.txtSentCompany.Size = new System.Drawing.Size(150, 21);
            this.txtSentCompany.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(965, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 95;
            this.label4.Text = "送检单位：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(1038, 360);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(150, 21);
            this.txtProvider.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 21);
            this.label5.TabIndex = 62;
            this.label5.Text = "检测项目：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(945, 360);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 21);
            this.label6.TabIndex = 78;
            this.label6.Text = "供货商/商标：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(973, 500);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(66, 21);
            this.label31.TabIndex = 57;
            this.label31.Text = "检测类型：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.Location = new System.Drawing.Point(46, 399);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(83, 21);
            this.label33.TabIndex = 98;
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
            this.cmbAssessor.Location = new System.Drawing.Point(122, 396);
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
            this.cmbAssessor.TabIndex = 39;
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
            this.cmbOrganizer.Location = new System.Drawing.Point(122, 316);
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
            this.cmbOrganizer.TabIndex = 28;
            this.cmbOrganizer.PropBag = resources.GetString("cmbOrganizer.PropBag");
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.Location = new System.Drawing.Point(45, 319);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(84, 21);
            this.label35.TabIndex = 61;
            this.label35.Text = "抽样人：";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(32, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 21);
            this.label7.TabIndex = 48;
            this.label7.Text = "检测仪器：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbCheckMachine.EvenRowStyle = style42;
            this.cmbCheckMachine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckMachine.FooterStyle = style43;
            this.cmbCheckMachine.GapHeight = 2;
            this.cmbCheckMachine.HeadingStyle = style44;
            this.cmbCheckMachine.HighLightRowStyle = style45;
            this.cmbCheckMachine.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckMachine.Images"))));
            this.cmbCheckMachine.ItemHeight = 15;
            this.cmbCheckMachine.Location = new System.Drawing.Point(122, 36);
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
            this.cmbCheckMachine.Size = new System.Drawing.Size(398, 22);
            this.cmbCheckMachine.Style = style48;
            this.cmbCheckMachine.TabIndex = 0;
            this.cmbCheckMachine.SelectedValueChanged += new System.EventHandler(this.cmbCheckMachine_SelectedValueChanged);
            this.cmbCheckMachine.PropBag = resources.GetString("cmbCheckMachine.PropBag");
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(448, 602);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 26);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkNoHaveMachine
            // 
            this.chkNoHaveMachine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNoHaveMachine.Location = new System.Drawing.Point(441, 36);
            this.chkNoHaveMachine.Name = "chkNoHaveMachine";
            this.chkNoHaveMachine.Size = new System.Drawing.Size(79, 22);
            this.chkNoHaveMachine.TabIndex = 1;
            this.chkNoHaveMachine.Text = "非自动检测";
            this.chkNoHaveMachine.Visible = false;
            this.chkNoHaveMachine.CheckedChanged += new System.EventHandler(this.chkNoHaveMachine_CheckedChanged);
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
            this.cmbCheckItem.Enabled = false;
            this.cmbCheckItem.EvenRowStyle = style50;
            this.cmbCheckItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItem.FooterStyle = style51;
            this.cmbCheckItem.GapHeight = 2;
            this.cmbCheckItem.HeadingStyle = style52;
            this.cmbCheckItem.HighLightRowStyle = style53;
            this.cmbCheckItem.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckItem.Images"))));
            this.cmbCheckItem.ItemHeight = 15;
            this.cmbCheckItem.Location = new System.Drawing.Point(122, 116);
            this.cmbCheckItem.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckItem.MaxDropDownItems = ((short)(5));
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
            this.cmbCheckItem.TabIndex = 31;
            this.cmbCheckItem.SelectedValueChanged += new System.EventHandler(this.cmbCheckItem_SelectedValueChanged);
            this.cmbCheckItem.TextChanged += new System.EventHandler(this.cmbCheckItem_TextChanged);
            this.cmbCheckItem.PropBag = resources.GetString("cmbCheckItem.PropBag");
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSampleCode.Location = new System.Drawing.Point(370, 76);
            this.txtSampleCode.MaxLength = 50;
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.Size = new System.Drawing.Size(150, 26);
            this.txtSampleCode.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(284, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 21);
            this.label11.TabIndex = 92;
            this.label11.Text = "样品编号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleUnit
            // 
            this.txtSampleUnit.Location = new System.Drawing.Point(1032, 221);
            this.txtSampleUnit.Name = "txtSampleUnit";
            this.txtSampleUnit.Size = new System.Drawing.Size(150, 21);
            this.txtSampleUnit.TabIndex = 21;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(1032, 244);
            this.txtUnit.MaxLength = 50;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(150, 21);
            this.txtUnit.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(934, 222);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 21);
            this.label14.TabIndex = 93;
            this.label14.Text = "数据单位：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(965, 244);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 21);
            this.label15.TabIndex = 94;
            this.label15.Text = "数据单位：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(970, 551);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(66, 17);
            this.lblParent.TabIndex = 52;
            this.lblParent.Text = "所属市场：";
            this.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbProduceCompany.Location = new System.Drawing.Point(1038, 382);
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
            this.cmbProduceCompany.TabIndex = 14;
            this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
            this.cmbProduceCompany.PropBag = resources.GetString("cmbProduceCompany.PropBag");
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(967, 383);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 21);
            this.label17.TabIndex = 79;
            this.label17.Text = "供应商：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(538, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 21);
            this.label19.TabIndex = 56;
            this.label19.Text = "生产日期：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrCheckNo
            // 
            this.txtOrCheckNo.Location = new System.Drawing.Point(1036, 430);
            this.txtOrCheckNo.MaxLength = 50;
            this.txtOrCheckNo.Name = "txtOrCheckNo";
            this.txtOrCheckNo.Size = new System.Drawing.Size(150, 21);
            this.txtOrCheckNo.TabIndex = 17;
            // 
            // txtStdCode
            // 
            this.txtStdCode.Location = new System.Drawing.Point(1032, 152);
            this.txtStdCode.Name = "txtStdCode";
            this.txtStdCode.Size = new System.Drawing.Size(150, 21);
            this.txtStdCode.TabIndex = 12;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(956, 430);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 21);
            this.label20.TabIndex = 80;
            this.label20.Text = "原检测编号：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(965, 152);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 21);
            this.label24.TabIndex = 90;
            this.label24.Text = "条形码：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(915, 82);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(116, 17);
            this.lblDomain.TabIndex = 88;
            this.lblDomain.Text = "档口/店面/车牌号：";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompanyInfo
            // 
            this.txtCompanyInfo.Enabled = false;
            this.txtCompanyInfo.Location = new System.Drawing.Point(1032, 82);
            this.txtCompanyInfo.Name = "txtCompanyInfo";
            this.txtCompanyInfo.Size = new System.Drawing.Size(150, 21);
            this.txtCompanyInfo.TabIndex = 6;
            // 
            // cmbCheckType
            // 
            this.cmbCheckType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckType.Items.AddRange(new object[] {
            "抽检",
            "送检",
            "抽检复检",
            "送检复检"});
            this.cmbCheckType.Location = new System.Drawing.Point(1036, 500);
            this.cmbCheckType.Name = "cmbCheckType";
            this.cmbCheckType.Size = new System.Drawing.Size(150, 20);
            this.cmbCheckType.TabIndex = 16;
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
            this.cmbResult.Location = new System.Drawing.Point(370, 276);
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
            this.cmbResult.TabIndex = 37;
            this.cmbResult.PropBag = resources.GetString("cmbResult.PropBag");
            // 
            // txtStandValue
            // 
            this.txtStandValue.Enabled = false;
            this.txtStandValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStandValue.Location = new System.Drawing.Point(370, 236);
            this.txtStandValue.MaxLength = 50;
            this.txtStandValue.Name = "txtStandValue";
            this.txtStandValue.Size = new System.Drawing.Size(150, 26);
            this.txtStandValue.TabIndex = 34;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(303, 239);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(73, 21);
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
            this.cmbProducePlace.Location = new System.Drawing.Point(1032, 174);
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
            this.cmbProducePlace.TabIndex = 15;
            this.cmbProducePlace.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProducePlace_BeforeOpen);
            this.cmbProducePlace.PropBag = resources.GetString("cmbProducePlace.PropBag");
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(965, 175);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 21);
            this.label29.TabIndex = 91;
            this.label29.Text = "产地：";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtResultInfo
            // 
            this.txtResultInfo.Enabled = false;
            this.txtResultInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResultInfo.Location = new System.Drawing.Point(122, 276);
            this.txtResultInfo.MaxLength = 50;
            this.txtResultInfo.Name = "txtResultInfo";
            this.txtResultInfo.Size = new System.Drawing.Size(150, 26);
            this.txtResultInfo.TabIndex = 36;
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.Location = new System.Drawing.Point(22, 279);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(107, 21);
            this.label32.TabIndex = 35;
            this.label32.Text = "检测值单位：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbUpperCompany.Location = new System.Drawing.Point(1033, 550);
            this.cmbUpperCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbUpperCompany.MaxDropDownItems = ((short)(5));
            this.cmbUpperCompany.MaxLength = 10;
            this.cmbUpperCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbUpperCompany.Name = "cmbUpperCompany";
            this.cmbUpperCompany.OddRowStyle = style86;
            this.cmbUpperCompany.PartialRightColumn = false;
            this.cmbUpperCompany.ReadOnly = true;
            this.cmbUpperCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbUpperCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.SelectedStyle = style87;
            this.cmbUpperCompany.Size = new System.Drawing.Size(150, 22);
            this.cmbUpperCompany.Style = style88;
            this.cmbUpperCompany.TabIndex = 4;
            this.cmbUpperCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbUpperCompany_BeforeOpen);
            this.cmbUpperCompany.PropBag = resources.GetString("cmbUpperCompany.PropBag");
            // 
            // cmbCheckerVal
            // 
            this.cmbCheckerVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckerVal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckerVal.Items.AddRange(new object[] {
            "无异议",
            "有异议",
            ""});
            this.cmbCheckerVal.Location = new System.Drawing.Point(370, 396);
            this.cmbCheckerVal.Name = "cmbCheckerVal";
            this.cmbCheckerVal.Size = new System.Drawing.Size(150, 24);
            this.cmbCheckerVal.TabIndex = 41;
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.Location = new System.Drawing.Point(264, 399);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(114, 21);
            this.label48.TabIndex = 87;
            this.label48.Text = "被检人确定：";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNotes.Location = new System.Drawing.Point(122, 555);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNotes.Size = new System.Drawing.Size(398, 35);
            this.txtNotes.TabIndex = 45;
            // 
            // txtCheckerRemark
            // 
            this.txtCheckerRemark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckerRemark.Location = new System.Drawing.Point(122, 505);
            this.txtCheckerRemark.Multiline = true;
            this.txtCheckerRemark.Name = "txtCheckerRemark";
            this.txtCheckerRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCheckerRemark.Size = new System.Drawing.Size(398, 44);
            this.txtCheckerRemark.TabIndex = 44;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(1036, 476);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(150, 21);
            this.txtPrice.TabIndex = 25;
            // 
            // txtSaleNum
            // 
            this.txtSaleNum.Location = new System.Drawing.Point(1039, 289);
            this.txtSaleNum.MaxLength = 20;
            this.txtSaleNum.Name = "txtSaleNum";
            this.txtSaleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaleNum.TabIndex = 23;
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.Location = new System.Drawing.Point(58, 555);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(66, 21);
            this.label47.TabIndex = 70;
            this.label47.Text = "备注：";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.Location = new System.Drawing.Point(17, 518);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(107, 21);
            this.label46.TabIndex = 69;
            this.label46.Text = "被检人说明：";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIsSentCheck
            // 
            this.cmbIsSentCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsSentCheck.Items.AddRange(new object[] {
            "否",
            "是",
            ""});
            this.cmbIsSentCheck.Location = new System.Drawing.Point(1038, 601);
            this.cmbIsSentCheck.Name = "cmbIsSentCheck";
            this.cmbIsSentCheck.Size = new System.Drawing.Size(150, 20);
            this.cmbIsSentCheck.TabIndex = 42;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(971, 601);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(66, 21);
            this.label38.TabIndex = 99;
            this.label38.Text = "是否送检：";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(967, 476);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(72, 21);
            this.label37.TabIndex = 60;
            this.label37.Text = "单价(元)：";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(968, 289);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(66, 21);
            this.label36.TabIndex = 82;
            this.label36.Text = "销售数量：";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckPlanCode
            // 
            this.txtCheckPlanCode.Location = new System.Drawing.Point(1032, 33);
            this.txtCheckPlanCode.MaxLength = 50;
            this.txtCheckPlanCode.Name = "txtCheckPlanCode";
            this.txtCheckPlanCode.Size = new System.Drawing.Size(150, 21);
            this.txtCheckPlanCode.TabIndex = 3;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(931, 33);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(96, 21);
            this.label34.TabIndex = 73;
            this.label34.Text = "检测计划编号：";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label45
            // 
            this.label45.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(965, 576);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(8, 21);
            this.label45.TabIndex = 66;
            this.label45.Text = "*";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label44.ForeColor = System.Drawing.Color.Red;
            this.label44.Location = new System.Drawing.Point(35, 334);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(8, 21);
            this.label44.TabIndex = 63;
            this.label44.Text = "*";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label44.Visible = false;
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.ForeColor = System.Drawing.Color.Red;
            this.label43.Location = new System.Drawing.Point(55, 358);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(8, 21);
            this.label43.TabIndex = 65;
            this.label43.Text = "*";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label43.Visible = false;
            // 
            // label42
            // 
            this.label42.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.ForeColor = System.Drawing.Color.Red;
            this.label42.Location = new System.Drawing.Point(8, 81);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(8, 21);
            this.label42.TabIndex = 49;
            this.label42.Text = "*";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label42.Visible = false;
            // 
            // label41
            // 
            this.label41.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label41.ForeColor = System.Drawing.Color.Red;
            this.label41.Location = new System.Drawing.Point(31, 193);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(8, 21);
            this.label41.TabIndex = 53;
            this.label41.Text = "*";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label41.Visible = false;
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Location = new System.Drawing.Point(8, 147);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(10, 21);
            this.label49.TabIndex = 76;
            this.label49.Text = "*";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label49.Visible = false;
            // 
            // label50
            // 
            this.label50.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label50.ForeColor = System.Drawing.Color.Red;
            this.label50.Location = new System.Drawing.Point(962, 550);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(8, 21);
            this.label50.TabIndex = 51;
            this.label50.Text = "*";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(7, 117);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 100;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label39.Visible = false;
            // 
            // txtProduceDate
            // 
            this.txtProduceDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtProduceDate.Location = new System.Drawing.Point(601, 114);
            this.txtProduceDate.Name = "txtProduceDate";
            this.txtProduceDate.Size = new System.Drawing.Size(150, 21);
            this.txtProduceDate.TabIndex = 112;
            // 
            // cmbCheckedCompany
            // 
            this.cmbCheckedCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCheckedCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCheckedCompany.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckedCompany.FormattingEnabled = true;
            this.cmbCheckedCompany.ItemHeight = 16;
            this.cmbCheckedCompany.Location = new System.Drawing.Point(122, 156);
            this.cmbCheckedCompany.Name = "cmbCheckedCompany";
            this.cmbCheckedCompany.Size = new System.Drawing.Size(398, 24);
            this.cmbCheckedCompany.TabIndex = 113;
            this.cmbCheckedCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCheckedCompany_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(446, 156);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(38, 23);
            this.btnSelect.TabIndex = 114;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dtpProduceDate
            // 
            this.dtpProduceDate.Location = new System.Drawing.Point(601, 115);
            this.dtpProduceDate.Name = "dtpProduceDate";
            this.dtpProduceDate.Size = new System.Drawing.Size(150, 21);
            this.dtpProduceDate.TabIndex = 222;
            this.dtpProduceDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            this.dtpProduceDate.DropDown += new System.EventHandler(this.dtpProduceDate_DropDown);
            // 
            // cmbCheckItemAnHui
            // 
            this.cmbCheckItemAnHui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckItemAnHui.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItemAnHui.FormattingEnabled = true;
            this.cmbCheckItemAnHui.Location = new System.Drawing.Point(370, 116);
            this.cmbCheckItemAnHui.Name = "cmbCheckItemAnHui";
            this.cmbCheckItemAnHui.Size = new System.Drawing.Size(150, 24);
            this.cmbCheckItemAnHui.TabIndex = 247;
            this.cmbCheckItemAnHui.SelectedIndexChanged += new System.EventHandler(this.cmbCheckItemAnHui_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(282, 119);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 21);
            this.label21.TabIndex = 246;
            this.label21.Text = "检测项目：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFoodType
            // 
            this.cmbFoodType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFoodType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFoodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFoodType.Enabled = false;
            this.cmbFoodType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFoodType.FormattingEnabled = true;
            this.cmbFoodType.Location = new System.Drawing.Point(620, 152);
            this.cmbFoodType.Name = "cmbFoodType";
            this.cmbFoodType.Size = new System.Drawing.Size(150, 24);
            this.cmbFoodType.TabIndex = 249;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(537, 155);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(90, 21);
            this.label26.TabIndex = 248;
            this.label26.Text = "样品种类：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textFoodType
            // 
            this.textFoodType.Enabled = false;
            this.textFoodType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textFoodType.Location = new System.Drawing.Point(370, 196);
            this.textFoodType.MaxLength = 50;
            this.textFoodType.Name = "textFoodType";
            this.textFoodType.Size = new System.Drawing.Size(150, 26);
            this.textFoodType.TabIndex = 251;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(293, 199);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 21);
            this.label16.TabIndex = 250;
            this.label16.Text = "样品种类：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(70, 434);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 12);
            this.label40.TabIndex = 252;
            this.label40.Text = "条形码：";
            // 
            // txtbarcode
            // 
            this.txtbarcode.Location = new System.Drawing.Point(122, 431);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Size = new System.Drawing.Size(398, 21);
            this.txtbarcode.TabIndex = 253;
            // 
            // frmPesticideMeasureEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(536, 636);
            this.Controls.Add(this.txtbarcode);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.textFoodType);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbFoodType);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.cmbCheckItemAnHui);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dtpProduceDate);
            this.Controls.Add(this.txtProduceDate);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.cmbCheckerVal);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtCheckerRemark);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtSaleNum);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.cmbIsSentCheck);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txtCheckPlanCode);
            this.Controls.Add(this.cmbUpperCompany);
            this.Controls.Add(this.txtResultInfo);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txtStandValue);
            this.Controls.Add(this.txtCompanyInfo);
            this.Controls.Add(this.txtOrCheckNo);
            this.Controls.Add(this.txtStdCode);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.txtSentCompany);
            this.Controls.Add(this.txtSampleUnit);
            this.Controls.Add(this.txtSampleCode);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.txtSampleLevel);
            this.Controls.Add(this.txtSampleState);
            this.Controls.Add(this.txtSampleModel);
            this.Controls.Add(this.txtCheckNo);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtStandard);
            this.Controls.Add(this.txtCheckValueInfo);
            this.Controls.Add(this.txtSampleNum);
            this.Controls.Add(this.txtSampleBaseNum);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtImportNum);
            this.Controls.Add(this.txtSaveNum);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cmbProducePlace);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cmbProduceCompany);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.dtpTakeDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbCheckMachine);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbCheckItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbOrganizer);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.cmbAssessor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbFood);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.cmbCheckUnit);
            this.Controls.Add(this.lblReferStandard);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.dtpCheckStartDate);
            this.Controls.Add(this.cmbChecker);
            this.Controls.Add(this.lblSuppresser);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCheckType);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.cmbCheckedCompany);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.chkNoHaveMachine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPesticideMeasureEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmPesticideMeasureEdit_Load);
            this.Controls.SetChildIndex(this.chkNoHaveMachine, 0);
            this.Controls.SetChildIndex(this.label48, 0);
            this.Controls.SetChildIndex(this.lblSample, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.btnSelect, 0);
            this.Controls.SetChildIndex(this.cmbCheckedCompany, 0);
            this.Controls.SetChildIndex(this.lblDomain, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.cmbCheckType, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.lblSuppresser, 0);
            this.Controls.SetChildIndex(this.cmbChecker, 0);
            this.Controls.SetChildIndex(this.dtpCheckStartDate, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.lblReferStandard, 0);
            this.Controls.SetChildIndex(this.cmbCheckUnit, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.Controls.SetChildIndex(this.cmbFood, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbAssessor, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.cmbOrganizer, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.cmbCheckItem, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cmbCheckMachine, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.dtpTakeDate, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.cmbProduceCompany, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.cmbResult, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.cmbProducePlace, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.txtSaveNum, 0);
            this.Controls.SetChildIndex(this.txtImportNum, 0);
            this.Controls.SetChildIndex(this.txtUnit, 0);
            this.Controls.SetChildIndex(this.txtSampleBaseNum, 0);
            this.Controls.SetChildIndex(this.txtSampleNum, 0);
            this.Controls.SetChildIndex(this.txtCheckValueInfo, 0);
            this.Controls.SetChildIndex(this.txtStandard, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.txtSysID, 0);
            this.Controls.SetChildIndex(this.txtCheckNo, 0);
            this.Controls.SetChildIndex(this.txtSampleModel, 0);
            this.Controls.SetChildIndex(this.txtSampleState, 0);
            this.Controls.SetChildIndex(this.txtSampleLevel, 0);
            this.Controls.SetChildIndex(this.txtProvider, 0);
            this.Controls.SetChildIndex(this.txtSampleCode, 0);
            this.Controls.SetChildIndex(this.txtSampleUnit, 0);
            this.Controls.SetChildIndex(this.txtSentCompany, 0);
            this.Controls.SetChildIndex(this.lblParent, 0);
            this.Controls.SetChildIndex(this.txtStdCode, 0);
            this.Controls.SetChildIndex(this.txtOrCheckNo, 0);
            this.Controls.SetChildIndex(this.txtCompanyInfo, 0);
            this.Controls.SetChildIndex(this.txtStandValue, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.txtResultInfo, 0);
            this.Controls.SetChildIndex(this.cmbUpperCompany, 0);
            this.Controls.SetChildIndex(this.txtCheckPlanCode, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label37, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.cmbIsSentCheck, 0);
            this.Controls.SetChildIndex(this.label46, 0);
            this.Controls.SetChildIndex(this.label47, 0);
            this.Controls.SetChildIndex(this.txtSaleNum, 0);
            this.Controls.SetChildIndex(this.txtPrice, 0);
            this.Controls.SetChildIndex(this.txtCheckerRemark, 0);
            this.Controls.SetChildIndex(this.txtNotes, 0);
            this.Controls.SetChildIndex(this.cmbCheckerVal, 0);
            this.Controls.SetChildIndex(this.label50, 0);
            this.Controls.SetChildIndex(this.label49, 0);
            this.Controls.SetChildIndex(this.label41, 0);
            this.Controls.SetChildIndex(this.label42, 0);
            this.Controls.SetChildIndex(this.label43, 0);
            this.Controls.SetChildIndex(this.label44, 0);
            this.Controls.SetChildIndex(this.label45, 0);
            this.Controls.SetChildIndex(this.label39, 0);
            this.Controls.SetChildIndex(this.txtProduceDate, 0);
            this.Controls.SetChildIndex(this.dtpProduceDate, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.cmbCheckItemAnHui, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.cmbFoodType, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.textFoodType, 0);
            this.Controls.SetChildIndex(this.label40, 0);
            this.Controls.SetChildIndex(this.txtbarcode, 0);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void frmPesticideMeasureEdit_Load(object sender, System.EventArgs e)
        {
            TitleBarText = this.Text;
            //if (ShareOption.IsDataLink)
            {
                this.lblParent.Text = ShareOption.AreaTitle + "：";
                this.lblName.Text = ShareOption.NameTitle + "：";
                this.lblDomain.Text = ShareOption.DomainTitle + "：";
                this.lblSample.Text = ShareOption.SampleTitle + "：";
            }
            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }
            clsMachineOpr opr9 = new clsMachineOpr();
            DataTable dt9 = opr9.GetAsDataTable("", "SysCode", 1);
            if (dt9 != null)
            {
                this.cmbCheckMachine.DataSource = dt9.DataSet;
                this.cmbCheckMachine.DataMember = "Machine";
                this.cmbCheckMachine.DisplayMember = "MachineName";
                this.cmbCheckMachine.ValueMember = "SysCode";
                this.cmbCheckMachine.Columns["MachineName"].Caption = "检测仪器";
                this.cmbCheckMachine.Columns["SysCode"].Caption = "系统编号";
            }

            if (ShareOption.DtblCheckCompany != null)
            {
                this.cmbCheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
                this.cmbCheckUnit.DataMember = "UserUnit";
                this.cmbCheckUnit.DisplayMember = "FullName";
                this.cmbCheckUnit.ValueMember = "SysCode";
                this.cmbCheckUnit.Columns["StdCode"].Caption = "编号";
                this.cmbCheckUnit.Columns["FullName"].Caption = "检测单位";
                this.cmbCheckUnit.Columns["SysCode"].Caption = "系统编号";
            }
            if (ShareOption.DtblChecker != null)
            {
                DataSet dstChecker = ShareOption.DtblChecker.DataSet;
                this.cmbChecker.DataSource = dstChecker;
                this.cmbChecker.DataMember = "UserInfo";
                this.cmbChecker.DisplayMember = "Name";
                this.cmbChecker.ValueMember = "UserCode";
                this.cmbChecker.Columns["Name"].Caption = "检测人";
                this.cmbChecker.Columns["UserCode"].Caption = "系统编号";
                this.cmbAssessor.DataSource = dstChecker.Copy();
                this.cmbAssessor.DataMember = "UserInfo";
                this.cmbAssessor.DisplayMember = "Name";
                this.cmbAssessor.ValueMember = "UserCode";
                this.cmbAssessor.Columns["Name"].Caption = "审核人";
                this.cmbAssessor.Columns["UserCode"].Caption = "系统编号";
                this.cmbOrganizer.DataSource = dstChecker.Copy();
                this.cmbOrganizer.DataMember = "UserInfo";
                this.cmbOrganizer.DisplayMember = "Name";
                this.cmbOrganizer.ValueMember = "UserCode";
                this.cmbOrganizer.Columns["Name"].Caption = "编制人";
                this.cmbOrganizer.Columns["UserCode"].Caption = "系统编号";
            }
            this.cmbResult.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.cmbResult.AddItemCols = 1;
            this.cmbResult.AddItemTitles("检测结果");
            this.cmbResult.AddItem(ShareOption.ResultEligi);
            this.cmbResult.AddItem(ShareOption.ResultFailure);
            this.txtStandard.Enabled = false;
            this.txtStandValue.Enabled = false;
            this.cmbCheckUnit.Enabled = false;
            this.cmbChecker.Enabled = false;
            if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
            {
                this.cmbUpperCompany.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
                this.sCheckedUpperComSelectedValue = clsCompanyOpr.CodeFromStdCode(clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode));
            }
            if (ShareOption.AllowHandInputCheckUint)
            {
                //this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownCombo;
                if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                {
                    this.cmbUpperCompany.Enabled = true;
                }
                else
                {
                    this.cmbUpperCompany.Enabled = false;
                }
                this.txtCompanyInfo.Enabled = true;
            }
            else
            {
                //this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
                this.cmbUpperCompany.Enabled = false;
                this.txtCompanyInfo.Enabled = false;
            }
            if (!strMachineCode.Equals(""))
            {
                this.cmbCheckMachine.SelectedValue = strMachineCode;
                this.cmbCheckMachine.Text = clsMachineOpr.GetMachineNameFromCode(strMachineCode);
            }
            else
            {
                if (this.Tag.ToString() != "MX")
                {
                    bindCheckItem("IsLock=false");
                }
            }
            if (!string.IsNullOrEmpty(strCheckItemCode) && this.Tag.ToString() != "MX")// != null && !strCheckItemCode.Equals(""))
            {
                this.cmbCheckItem.SelectedValue = strCheckItemCode;
                this.cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(strCheckItemCode);
            }
            if (!sFoodSelectedValue.Equals(""))
            {
                this.cmbFood.SelectedValue = sFoodSelectedValue;
                this.cmbFood.Text = clsFoodClassOpr.NameFromCode(sFoodSelectedValue);
            }
            if (!sProduceComSelectedValue.Equals(""))
            {
                this.cmbProduceCompany.SelectedValue = sProduceComSelectedValue;
                this.cmbProduceCompany.Text = clsCompanyOpr.NameFromCode(sProduceComSelectedValue);
            }
            if (!sProducePlaceSelectValue.Equals(""))
            {
                this.cmbProducePlace.SelectedValue = sProducePlaceSelectValue;
                this.cmbProducePlace.Text = clsProduceAreaOpr.NameFromCode(sProducePlaceSelectValue);
            }
            if (!string.IsNullOrEmpty(checkunitcode))// != null && !checkunitcode.Equals(""))
            {
                this.cmbCheckUnit.SelectedValue = checkunitcode;
                this.cmbCheckUnit.Text = clsUserUnitOpr.GetNameFromCode(checkunitcode);
            }
            if (!string.IsNullOrEmpty(checker))//!= null && !checker.Equals(""))
            {
                this.cmbChecker.SelectedValue = checker;
                this.cmbChecker.Text = clsUserInfoOpr.NameFromCode(checker);
            }
            if (!string.IsNullOrEmpty(assessor))//!= null && !assessor.Equals(""))
            {
                this.cmbAssessor.SelectedValue = assessor;
                this.cmbAssessor.Text = clsUserInfoOpr.NameFromCode(assessor);
            }
            if (!string.IsNullOrEmpty(organizer))// != null && !organizer.Equals(""))
            {
                this.cmbOrganizer.SelectedValue = organizer;
                this.cmbOrganizer.Text = clsUserInfoOpr.NameFromCode(organizer);
            }

            //样品种类
            cmbFoodType.DataSource = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '食品分类'", 1, "FoodType");
            cmbFoodType.DisplayMember = "name";
            cmbFoodType.ValueMember = "codeId";
            txtStandValue.Text = ShareOption.standardvalue;
            txtbarcode.Text = ShareOption.barcode;
            if (this.Text == "综合检测数据明细")
            {
                txtbarcode.ReadOnly = true;
            }
            else
            {
                txtbarcode.ReadOnly = false;
            }
            
            //if (txtStandValue.Text.Trim().Length == 0)
            //{
            //    txtStandValue.Text = ShareOption.standardvalue;
            //}
        }

        private void bindCheckItem(string strWhere)
        {
            clsCheckItemOpr opr1 = new clsCheckItemOpr();
            DataTable dt1 = opr1.GetAsDataTable(strWhere, "SysCode", 1);//"IsLock=false"
            if (dt1 != null)
            {
                this.cmbCheckItem.DataSource = dt1.DataSet;
                this.cmbCheckItem.DataMember = "CheckItem";
                this.cmbCheckItem.DisplayMember = "ItemDes";
                this.cmbCheckItem.ValueMember = "SysCode";
                this.cmbCheckItem.Columns["StdCode"].Caption = "编号";
                this.cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
                this.cmbCheckItem.Columns["SysCode"].Caption = "系统编号";
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //必须输入的是否已输入
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
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "生产日期无效，请重新填写!");
                    return;
                }
            }

            if (this.txtCheckNo.Text.Equals(""))
            {
                MessageBox.Show(this, "检测编号必须输入!");
                this.txtCheckNo.Focus();
                return;
            }
            if (this.cmbCheckItem.Text.Equals(""))
            {
                MessageBox.Show(this, "检测项目不能为空，请查验选项中的检测项目对应项!");
                this.cmbCheckItem.Focus();
                return;
            }
            //if (this.txtStandValue.Text.Equals("")   )
            //{
            //    MessageBox.Show(this, "标准值不能为空，请查验检测项目对应样品!");
            //    this.txtStandValue.Focus();
            //    return;
            //}
            if (this.txtCheckValueInfo.Text.Equals("") && label44.Visible == true)
            {
                MessageBox.Show(this, lblSuppresser.Text.Substring(0, 3) + "不能为空，请查验!");
                this.txtCheckValueInfo.Focus();
                return;
            }
            if (this.sFoodSelectedValue.Equals(""))
            {
                MessageBox.Show(this, ShareOption.SampleTitle + "必须输入!");
                this.cmbFood.Text = "";
                this.cmbFood.Focus();
                return;
            }
            if (ShareOption.AllowHandInputCheckUint)
            {
                if (this.sCheckedUpperComSelectedValue.Equals(""))
                {
                    MessageBox.Show(this, ShareOption.AreaTitle + "必须输入!");
                    this.cmbUpperCompany.Text = "";
                    this.cmbUpperCompany.Focus();
                    return;
                }
                if (this.cmbCheckedCompany.Text.Equals(""))
                {
                    MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                    this.cmbCheckedCompany.Text = "";
                    this.cmbCheckedCompany.Focus();
                    return;
                }
            }
            else
            {
                //if (this.sCheckedComSelectedValue.Equals(""))
                //{
                //    MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                //    this.cmbCheckedCompany.Text = "";
                //    this.cmbCheckedCompany.Focus();
                //    return;
                //}
            }
            if (this.cmbCheckUnit.SelectedValue == null)
            {
                MessageBox.Show(this, "检测单位必须输入!");
                this.cmbCheckUnit.Text = "";
                this.cmbCheckUnit.Focus();
                return;
            }
            if (this.cmbChecker.SelectedValue == null)
            {
                MessageBox.Show(this, "检测人必须输入!");
                this.cmbChecker.Text = "";
                this.cmbChecker.Focus();
                return;
            }
            if (this.cmbResult.Text.Equals(""))
            {
                MessageBox.Show(this, "结论必须输入!");
                this.cmbResult.Focus();
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
            if (!StringUtil.IsValidNumber(this.txtSampleBaseNum.Text.Trim()))
            {
                MessageBox.Show(this, "抽样基数必须为整数!");
                this.txtSampleBaseNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(this.txtSampleNum.Text.Trim()))
            {
                MessageBox.Show(this, "抽样数量必须为数字!");
                this.txtSampleNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(this.txtImportNum.Text.Trim()))
            {
                MessageBox.Show(this, "进货数量必须为数字!");
                this.txtImportNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(this.txtSaleNum.Text.Trim()))
            {
                MessageBox.Show(this, "销售数量必须为数字!");
                this.txtSaleNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(this.txtPrice.Text.Trim()))
            {
                MessageBox.Show(this, "单价必须为数字!");
                this.txtPrice.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(this.txtSaveNum.Text.Trim()))
            {
                MessageBox.Show(this, "库存数量必须为数字!");
                this.txtSaveNum.Focus();
                return;
            }
            if (this.dtpTakeDate.Value > this.dtpCheckStartDate.Value)
            {
                MessageBox.Show(this, "抽样日期不能超过检测开始时间!");
                this.dtpTakeDate.Focus();
                return;
            }


            //取值
            clsResult model = new clsResult();
            string sErr = string.Empty;

            model.SysCode = this.txtSysID.Text.Trim();
            model.CheckNo = this.txtCheckNo.Text.Trim();
            if (!this.cmbCheckMachine.Enabled)
            {
                model.ResultType = ShareOption.ResultType5;
            }
            else
            {
                model.ResultType = ShareOption.ResultType1;
            }
            model.StdCode =txtbarcode.Text; //this.txtStdCode.Text.Trim();
            model.SampleCode = this.txtSampleCode.Text.Trim();
            model.CheckedCompany = this.sCheckedComSelectedValue;

            model.CheckedCompanyName = this.cmbCheckedCompany.Text.Trim();

            model.CheckedComDis = this.txtCompanyInfo.Text.Trim();
            model.CheckPlaceCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode);
            model.FoodCode = this.sFoodSelectedValue;

            string produceDate = txtProduceDate.Text;
            if (!string.IsNullOrEmpty(produceDate))
            {
                model.ProduceDate = Convert.ToDateTime(produceDate); //dtpProduceDate.Value;
            }
            //if (string.IsNullOrEmpty(produceDate))
            //{
            //    model.ProduceDate = null ;
            //}
            model.ProduceCompany = this.sProduceComSelectedValue;
            model.ProducePlace = this.sProducePlaceSelectValue;
            model.SentCompany = this.txtSentCompany.Text.Trim();
            model.Provider = this.txtProvider.Text.Trim();
            model.TakeDate = this.dtpTakeDate.Value;
            model.CheckStartDate = this.dtpCheckStartDate.Value;
            if (this.txtImportNum.Text.Trim().Equals(""))
            {
                model.ImportNum = "";
            }
            else
            {
                model.ImportNum = Convert.ToDouble(this.txtImportNum.Text.Trim()).ToString();
            }
            if (this.txtSaveNum.Text.Trim().Equals(""))
            {
                model.SaveNum = "";
            }
            else
            {
                model.SaveNum = Convert.ToDouble(this.txtSaveNum.Text.Trim()).ToString();
            }
            model.Unit = this.txtUnit.Text.Trim();
            if (this.txtSampleBaseNum.Text.Trim().Equals(""))
            {
                model.SampleBaseNum = "null";
            }
            else
            {
                model.SampleBaseNum = this.txtSampleBaseNum.Text.Trim();
            }
            if (this.txtSampleNum.Text.Trim().Equals(""))
            {
                model.SampleNum = "null";
            }
            else
            {
                model.SampleNum = this.txtSampleNum.Text.Trim();
            }
            model.SampleUnit = this.txtSampleUnit.Text.Trim();
            model.SampleLevel = this.txtSampleLevel.Text.Trim();
            model.SampleModel = this.txtSampleModel.Text.Trim();
            model.SampleState = this.txtSampleState.Text.Trim();
            if (!this.chkNoHaveMachine.Checked)
            {
                if (!strMachineCode.Equals(""))
                {
                    model.CheckMachine = this.cmbCheckMachine.SelectedValue.ToString();
                }
                else
                {
                    model.CheckMachine = "";
                }
                if (this.cmbCheckItem.SelectedValue == null)
                {

                    model.CheckTotalItem = "";
                }
                else
                {
                    model.CheckTotalItem = this.cmbCheckItem.SelectedValue.ToString();
                }
                model.Standard = strStandardCode;
            }
            else
            {
                model.Standard = strStandardCode;
                if (this.cmbCheckItem.SelectedValue == null)
                {
                    model.CheckTotalItem = "";
                }
                else
                {
                    model.CheckTotalItem = this.cmbCheckItem.SelectedValue.ToString();
                }
            }
            model.CheckValueInfo = this.txtCheckValueInfo.Text.Trim();
            model.StandValue = this.txtStandValue.Text.Trim();
            model.Result = this.cmbResult.Text.Trim();
            model.ResultInfo = this.txtResultInfo.Text;
            model.UpperCompany = this.sCheckedUpperComSelectedValue.ToString();
            model.UpperCompanyName = this.cmbUpperCompany.Text.Trim();
            model.OrCheckNo = this.txtOrCheckNo.Text.Trim();
            model.CheckType = this.cmbCheckType.Text;
            if (this.cmbCheckUnit.SelectedValue == null)
            {
                model.CheckUnitCode = "";
            }
            else
            {
                model.CheckUnitCode = this.cmbCheckUnit.SelectedValue.ToString();
            }
            if (this.cmbChecker.SelectedValue == null)
            {
                model.Checker = "";
            }
            else
            {
                model.Checker = this.cmbChecker.SelectedValue.ToString();
            }
            if (this.cmbAssessor.SelectedValue == null)
            {
                model.Assessor = "";
            }
            else
            {
                model.Assessor = this.cmbAssessor.SelectedValue.ToString();
            }
            if (this.cmbOrganizer.SelectedValue == null)
            {
                model.Organizer = "";
            }
            else
            {
                model.Organizer = this.cmbOrganizer.SelectedValue.ToString();
            }
            model.Remark = this.txtRemark.Text.Trim();
            model.CheckPlanCode = this.txtCheckPlanCode.Text.Trim();
            if (this.txtSaleNum.Text.Trim().Equals(""))
            {
                model.SaleNum = "null";
            }
            else
            {
                model.SaleNum = this.txtSaleNum.Text.Trim();
            }
            if (this.txtPrice.Text.Trim().Equals(""))
            {
                model.Price = "null";
            }
            else
            {
                model.Price = this.txtPrice.Text.Trim();
            }
            model.CheckederVal = this.cmbCheckerVal.Text.ToString();
            model.IsSentCheck = this.cmbIsSentCheck.Text.ToString();
            model.CheckederRemark = this.txtCheckerRemark.Text.ToString();
            model.Notes = this.txtNotes.Text.ToString();


            //对数据库进行操作
            clsResultOpr.UpdatePart(model, out sErr);

            if (!sErr.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！" + sErr);
            }

            //退出
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        internal void setValue(clsResult model)
        {
            this.txtSysID.Text = model.SysCode;
            this.txtCheckNo.Text = model.CheckNo;
            this.txtStdCode.Text = model.StdCode;
            this.txtSampleCode.Text = model.SampleCode;
            this.sCheckedComSelectedValue = model.CheckedCompany;
            this.cmbCheckedCompany.Text = model.CheckedCompanyName;
            this.txtCompanyInfo.Text = model.CheckedComDis;
            this.sFoodSelectedValue = model.FoodCode;

            DateTime? tempdt = model.ProduceDate;
            if (tempdt != null)
            {
                //dtpProduceDate.Value = ;
                txtProduceDate.Text = Convert.ToDateTime(tempdt).ToString("yyyy-MM-dd");
            }

            this.sProduceComSelectedValue = model.ProduceCompany;
            this.sProducePlaceSelectValue = model.ProducePlace;
            this.txtSentCompany.Text = model.SentCompany;
            this.txtProvider.Text = model.Provider;
            this.dtpTakeDate.Value = model.TakeDate;
            this.dtpCheckStartDate.Value = model.CheckStartDate;
            this.txtImportNum.Text = model.ImportNum;
            this.txtSaveNum.Text = model.SaveNum;
            this.txtSampleBaseNum.Text = model.SampleBaseNum;
            this.txtSampleNum.Text = model.SampleNum;
            this.txtUnit.Text = model.Unit;
            this.txtSampleUnit.Text = model.SampleUnit;
            this.txtSampleLevel.Text = model.SampleLevel;
            this.txtSampleModel.Text = model.SampleModel;
            this.txtSampleState.Text = model.SampleState;

            strMachineCode = model.CheckMachine;
            strCheckItemCode = model.CheckTotalItem;
            strStandardCode = model.Standard;

            this.txtCheckValueInfo.Text = model.CheckValueInfo;
            this.txtStandValue.Text = model.StandValue;
            ShareOption.standardvalue = model.StandValue;
            this.cmbResult.Text = model.Result;
            this.txtResultInfo.Text = model.ResultInfo;
            this.sCheckedUpperComSelectedValue = model.UpperCompany;
            this.cmbUpperCompany.Text = model.UpperCompanyName;
            this.txtOrCheckNo.Text = model.OrCheckNo;
            this.txtbarcode.Text = model.StdCode;
            txtbarcode.ReadOnly = true;
            ShareOption.barcode  = model.StdCode;
            //获取样品品种
            string fTpye = string.Empty;
            DataTable dtbl = clsResultOpr.GetAsDataTable_AnHui("A.SysCode like '" + model.SysCode + "'", "A.CheckStartDate DESC, A.syscode desc");
            if (dtbl != null && dtbl.Rows.Count == 1)
            {
                fTpye = dtbl.Rows[0]["fTpye"].ToString().Trim();
                //样品种类
                cmbFoodType.DataSource = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '食品分类' AND codeId like '" + fTpye + "'", 1, "FoodType");
                cmbFoodType.DisplayMember = "name";
                cmbFoodType.ValueMember = "codeId";
            }

            //获取样品名称
            clsFoodClassOpr foodBll = new clsFoodClassOpr();
            DataTable dt = foodBll.GetAsDataTable("SysCode= '" + model.FoodCode + "'", "SysCode", 2);
            if (dt.Rows.Count > 0)
            {
                cmbFood.Text = dt.Rows[0]["Name"].ToString();
                //cmbFood.DataSource = dt;
                //cmbFood.DisplayMember = "Name";
                //cmbFood.ValueMember = "SysCode";
            }

            switch (model.CheckType)
            {
                case "抽检":
                    this.cmbCheckType.SelectedIndex = 0;
                    break;
                case "送检":
                    this.cmbCheckType.SelectedIndex = 1;
                    break;
                case "抽检复检":
                    this.cmbCheckType.SelectedIndex = 2;
                    break;
                case "送检复检":
                    this.cmbCheckType.SelectedIndex = 3;
                    break;
            }
            checkunitcode = model.CheckUnitCode;
            checker = model.Checker;
            assessor = model.Assessor;
            organizer = model.Organizer;
            this.txtRemark.Text = model.Remark;
            cmbCheckItem.Text = model.MachineItemName;
            if (model.ResultType.Equals(ShareOption.ResultType1))//手工输入法
            {
                this.txtCheckValueInfo.Enabled = true;
                this.cmbCheckMachine.Enabled = true;
                this.cmbCheckItem.Enabled = true;
                this.cmbResult.Enabled = true;
                this.dtpCheckStartDate.Enabled = true;
            }
            else
            {
                //this.txtCheckValueInfo.Enabled = false;
                //this.cmbCheckMachine.Enabled = false;
                this.cmbCheckItem.Enabled = false;
                this.cmbResult.Enabled = false;
                this.dtpCheckStartDate.Enabled = false;

            }
            if (model.ResultType.Equals(ShareOption.ResultType5))//仪器自动检测
            {
                this.dtpCheckStartDate.Enabled = false;
            }
            else
            {
                this.dtpCheckStartDate.Enabled = true;
            }
            if (model.ResultType.Equals(ShareOption.ResultType3))//其他手工检测
            {
                cmbCheckItem.Enabled = true;
                //label7.Visible = false;
                label44.Visible = false;
                //cmbCheckMachine.Visible = false;
                cmbCheckMachine.Text = "其他手工检测";
                cmbCheckMachine.Enabled = false;
                txtCheckValueInfo.Enabled = true;
                cmbResult.Enabled = true;
                lblSuppresser.Text = "检测值：";
            }

            this.txtCheckPlanCode.Text = model.CheckPlanCode;

            if (model.SaleNum == "null")
            {
                this.txtSaleNum.Text = "";
            }
            else
            {
                this.txtSaleNum.Text = model.SaleNum.ToString();
            }

            if (model.Price == "null")
            {
                this.txtPrice.Text = "";
            }
            else
            {
                this.txtPrice.Text = model.Price.ToString();
            }

            switch (model.CheckederVal)
            {
                case "":
                    this.cmbCheckerVal.SelectedIndex = 2;
                    break;
                case "无异议":
                    this.cmbCheckerVal.SelectedIndex = 0;
                    break;
                case "有异议":
                    this.cmbCheckerVal.SelectedIndex = 1;
                    break;
            }
            switch (model.IsSentCheck)
            {
                case "":
                    this.cmbIsSentCheck.SelectedIndex = 2;
                    break;
                case "否":
                    this.cmbIsSentCheck.SelectedIndex = 0;
                    break;
                case "是":
                    this.cmbIsSentCheck.SelectedIndex = 1;
                    break;
            }
            this.txtCheckerRemark.Text = model.CheckederRemark;
            this.txtNotes.Text = model.Notes;
            BigTip = model.IsSended;
            //0620
            if (model.IsSended == true)
            {
                //this.Text = "标准速测法检测数据查看明细";
                this.txtCheckPlanCode.ReadOnly = true;
                this.txtSaleNum.ReadOnly = true;
                this.txtPrice.ReadOnly = true;
                this.cmbCheckerVal.Enabled = false;
                this.cmbIsSentCheck.Enabled = false;
                this.txtCheckerRemark.ReadOnly = true;
                this.txtNotes.ReadOnly = true;
                this.cmbAssessor.ReadOnly = true;
                this.cmbCheckedCompany.Enabled = false;
                this.cmbChecker.ReadOnly = true;
                this.cmbCheckItem.ReadOnly = true;
                this.cmbCheckMachine.ReadOnly = true;
                this.cmbCheckType.Enabled = false;
                this.cmbCheckUnit.ReadOnly = true;
                this.cmbFood.ReadOnly = true;
                this.cmbOrganizer.ReadOnly = true;
                this.cmbProduceCompany.ReadOnly = true;
                this.cmbProducePlace.ReadOnly = true;
                this.cmbResult.ReadOnly = true;
                this.cmbUpperCompany.ReadOnly = true;
                this.txtCheckNo.ReadOnly = true;
                this.txtCheckValueInfo.ReadOnly = true;
                this.txtCompanyInfo.ReadOnly = true;
                this.txtImportNum.ReadOnly = true;
                this.txtOrCheckNo.ReadOnly = true;
                this.txtProvider.ReadOnly = true;
                this.txtRemark.ReadOnly = true;
                this.txtResultInfo.ReadOnly = true;
                this.txtSampleBaseNum.ReadOnly = true;
                this.txtSampleCode.ReadOnly = true;
                this.txtSampleLevel.ReadOnly = true;
                this.txtSampleModel.ReadOnly = true;
                this.txtSampleNum.ReadOnly = true;
                this.txtSampleUnit.ReadOnly = true;
                this.txtSampleState.ReadOnly = true;
                this.txtSaveNum.ReadOnly = true;
                this.txtSentCompany.ReadOnly = true;
                this.txtStandard.ReadOnly = true;
                this.txtStandValue.ReadOnly = true;
                this.txtStdCode.ReadOnly = true;
                this.txtSysID.ReadOnly = true;
                this.txtUnit.ReadOnly = true;
                this.dtpCheckStartDate.Enabled = false;
                this.dtpProduceDate.Enabled = false;
                this.dtpTakeDate.Enabled = false;
                //this.btnOK.Visible = false;
                btnSelect.Enabled = false;
            }
            if (this.Tag.ToString().Equals("MX"))
            {
                this.Text = "标准速测法检测数据查看明细";
                //this.txtCheckPlanCode.ReadOnly = true;
                //this.txtSaleNum.ReadOnly = true;
                //this.txtPrice.ReadOnly = true;
                //this.cmbCheckerVal.Enabled = false;
                //this.cmbIsSentCheck.Enabled = false;
                //this.txtCheckerRemark.ReadOnly = true;
                //this.txtNotes.ReadOnly = true;
                //this.cmbAssessor.ReadOnly = true;
                //this.cmbCheckedCompany.Enabled = false ;
                //this.cmbChecker.ReadOnly = true;
                //this.cmbCheckItem.ReadOnly = true;
                //this.cmbCheckMachine.ReadOnly = true;
                //this.cmbCheckType.Enabled = false;
                //this.cmbCheckUnit.ReadOnly = true;
                //this.cmbFood.ReadOnly = true;
                //this.cmbOrganizer.ReadOnly = true;
                //this.cmbProduceCompany.ReadOnly = true;
                //this.cmbProducePlace.ReadOnly = true;
                //this.cmbResult.ReadOnly = true;
                //this.cmbUpperCompany.ReadOnly = true;
                //this.txtCheckNo.ReadOnly = true;
                //this.txtCheckValueInfo.ReadOnly = true;
                //this.txtCompanyInfo.ReadOnly = true;
                //this.txtImportNum.ReadOnly = true;
                //this.txtOrCheckNo.ReadOnly = true;
                //this.txtProvider.ReadOnly = true;
                //this.txtRemark.ReadOnly = true;
                //this.txtResultInfo.ReadOnly = true;
                //this.txtSampleBaseNum.ReadOnly = true;
                //this.txtSampleCode.ReadOnly = true;
                //this.txtSampleLevel.ReadOnly = true;
                //this.txtSampleModel.ReadOnly = true;
                //this.txtSampleNum.ReadOnly = true;
                //this.txtSampleUnit.ReadOnly = true;
                //this.txtSampleState.ReadOnly = true;
                //this.txtSaveNum.ReadOnly = true;
                //this.txtSentCompany.ReadOnly = true;
                //this.txtStandard.ReadOnly = true;
                //this.txtStandValue.ReadOnly = true;
                //this.txtStdCode.ReadOnly = true;
                //this.txtSysID.ReadOnly = true;
                //this.txtUnit.ReadOnly = true;
                this.txtCheckPlanCode.Enabled = false;
                this.txtSaleNum.Enabled = false;
                this.txtPrice.Enabled = false;
                this.cmbCheckerVal.Enabled = false;
                this.cmbIsSentCheck.Enabled = false;
                this.txtCheckerRemark.Enabled = false;
                this.txtNotes.Enabled = false;
                this.cmbAssessor.Enabled = false;
                this.cmbCheckedCompany.Enabled = false;
                this.cmbChecker.Enabled = false;
                this.cmbCheckItem.Enabled = false;
                this.cmbCheckMachine.Enabled = false;
                this.cmbCheckType.Enabled = false;
                this.cmbCheckUnit.Enabled = false;
                this.cmbFood.Enabled = false;
                this.cmbOrganizer.Enabled = false;
                this.cmbProduceCompany.Enabled = false;
                this.cmbProducePlace.Enabled = false;
                this.cmbResult.Enabled = false;
                this.cmbUpperCompany.Enabled = false;
                this.txtCheckNo.Enabled = false;
                this.txtCheckValueInfo.Enabled = false;
                this.txtCompanyInfo.Enabled = false;
                this.txtImportNum.Enabled = false;
                this.txtOrCheckNo.Enabled = false;
                this.txtProvider.Enabled = false;
                this.txtRemark.Enabled = false;
                this.txtResultInfo.Enabled = false;
                this.txtSampleBaseNum.Enabled = false;
                this.txtSampleCode.Enabled = false;
                this.txtSampleLevel.Enabled = false;
                this.txtSampleModel.Enabled = false;
                this.txtSampleNum.Enabled = false;
                this.txtSampleUnit.Enabled = false;
                this.txtSampleState.Enabled = false;
                this.txtSaveNum.Enabled = false;
                this.txtSentCompany.Enabled = false;
                this.txtStandard.Enabled = false;
                this.txtStandValue.Enabled = false;
                this.txtStdCode.Enabled = false;
                this.txtSysID.Enabled = false;
                this.txtUnit.Enabled = false;

                this.dtpCheckStartDate.Enabled = false;
                this.dtpTakeDate.Enabled = false;

                this.txtProduceDate.Enabled = false;
                this.dtpProduceDate.Enabled = false;

                this.btnOK.Visible = false;
                btnSelect.Enabled = false;
            }
        }

        private void cmbCheckMachine_SelectedValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                string sProtocol = clsMachineOpr.GetNameFromCode("Protocol", this.cmbCheckMachine.SelectedValue.ToString());
                switch (sProtocol)
                {
                    case "RS232水分仪插件":
                        this.lblSuppresser.Text = "水分含量：";
                        break;

                    case "RS232DY3000DY":
                        if (this.cmbCheckMachine.SelectedValue.ToString().Equals("018"))
                        {
                            this.lblSuppresser.Text = "抑制率：";
                        }
                        else
                        {
                            this.lblSuppresser.Text = "检测值：";
                        }
                        break;

                    case "RS232LZ4000TDY":
                        this.lblSuppresser.Text = "抑制率：";
                        break;

                    default:
                        this.lblSuppresser.Text = "检测值：";
                        break;
                }
                string strLinkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", this.cmbCheckMachine.SelectedValue.ToString());
                if (sProtocol.Equals("RS232DY3000DY") || sProtocol.Trim().Equals("RS232LZ4000TDY"))
                {
                    this.strCheckItems = StringUtil.GetDY3000DYAry(strLinkStdCode);
                }
                else
                {
                    this.strCheckItems = StringUtil.GetAry(strLinkStdCode);
                }

                if (strCheckItems.GetLength(0) <= 0)
                {
                    return;
                }
                if (strCheckItems.GetLength(0) == 1 && this.strCheckItems[0, 1].ToString() != "-1")
                {
                    strCheckItemCode = this.strCheckItems[0, 1].ToString();
                }
                if (strCheckItems.GetLength(0) == 1 && this.strCheckItems[0, 1].ToString() == "-1")
                {
                    strCheckItemCode = string.Empty;
                }
                if (this.Tag.ToString() != "MX")
                {
                    this.cmbCheckItem.Enabled = true;

                    string strWhere = string.Empty;
                    if (strCheckItems.GetLength(0) > 1)
                    {
                        string strSql = string.Empty;
                        bool blExist = false;
                        for (int i = 0; i < strCheckItems.GetLength(0); i++)
                        {
                            if (strCheckItems[i, 1].ToString() != "-1")
                            {
                                strSql += "'" + strCheckItems[i, 1].ToString() + "',";
                            }
                            if (strCheckItems[i, 1].ToString() == strCheckItemCode)
                            {
                                blExist = true;
                                break;
                            }
                        }
                        if (!blExist)
                        {
                            strCheckItemCode = string.Empty;
                        }
                        if (strSql.Length > 0)
                        {
                            strSql = strSql.Substring(0, strSql.Length - 1);
                            strWhere = string.Format("IsLock=false And SysCode In({0})", strSql);
                        }
                        if (strSql.Length <= 0)
                        {
                            MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
                            //return;
                        }
                    }
                    else
                    {
                        strWhere = string.Format("IsLock=false And SysCode ='{0}'", strCheckItems[0, 1].ToString());
                    }
                    if (this.Tag.ToString() != "MX")
                    {
                        if (!string.IsNullOrEmpty(strWhere))
                        {
                            bindCheckItem(strWhere);
                        }
                        if (strCheckItemCode.Equals(""))
                        {
                            this.cmbCheckItem.SelectedIndex = -1;
                            this.cmbCheckItem.Text = string.Empty;
                        }
                        else
                        {
                            this.cmbCheckItem.SelectedValue = strCheckItemCode;
                            this.cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(strCheckItemCode);
                        }
                        if (strCheckItems.GetLength(0) > 1)
                        {
                            this.cmbCheckItem.Enabled = this.cmbCheckMachine.Enabled;
                        }
                        else
                        {
                            this.cmbCheckItem.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置！");
            }
        }

        private void chkNoHaveMachine_CheckedChanged(object sender, System.EventArgs e)
        {
            this.cmbCheckMachine.Enabled = !this.chkNoHaveMachine.Checked;
            this.cmbCheckItem.Enabled = this.chkNoHaveMachine.Checked;
            if (!this.chkNoHaveMachine.Checked)
            {
                //sResultType = ShareOption.ResultTypeCode1;
                cmbCheckMachine_SelectedValueChanged(null, null);
            }
            else
            {
                // sResultType = ShareOption.ResultTypeCode3;
                cmbCheckItem.SelectedIndex = -1;
                lblSuppresser.Text = "检测值：";
            }
        }

        private void cmbCheckItem_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (this.cmbCheckItem.SelectedValue != null)
            {
                this.txtResultInfo.Text = clsCheckItemOpr.GetUnitFromCode(this.cmbCheckItem.SelectedValue.ToString());
                strStandardCode = clsCheckItemOpr.GetStandardCode(this.cmbCheckItem.SelectedValue.ToString());
                this.txtStandard.Text = clsStandardOpr.GetNameFromCode(strStandardCode);
                if (!this.sFoodSelectedValue.Equals(""))
                {
                    string[] strResult = clsFoodClassOpr.ValueFromCode(this.sFoodSelectedValue.ToString(), this.cmbCheckItem.SelectedValue.ToString());
                    strSign = strResult[0];
                    try
                    {

                        dTestValue = Convert.ToDecimal(strResult[1]);
                    }
                    catch (Exception)
                    {
                        dTestValue = 0;
                    }
                    strUnit = strResult[2];
                    if (strSign.Equals("-1") && dTestValue == 0 && strUnit.Equals("-1"))
                    {
                        this.sFoodSelectedValue = string.Empty;
                        this.cmbFood.Text = string.Empty;
                        this.txtStandValue.Text = string.Empty;
                    }
                    else
                    {
                        this.txtStandValue.Text = dTestValue.ToString();
                    }
                }
            }
            else
            {
                strStandardCode = string.Empty;
                this.txtStandard.Text = string.Empty;
            }
        }

        private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (cmbCheckItem.SelectedValue != null)
            //{
            frmFoodSelect frm = new frmFoodSelect("", "");
            clsFoodClassOpr foodBll = new clsFoodClassOpr();
            //DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + cmbCheckItem.SelectedValue.ToString() + ":%'", "SysCode", 0);
            DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true", "SysCode", 0);
            if (dt.Rows.Count > 0)
            {
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                {
                    sFoodSelectedValue = frm.sNodeTag;
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
                    string sysCode = sFoodSelectedValue.Substring(0, 10);
                    dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and SysCode= '" + sysCode + "'", "SysCode", 0);
                    if (dt.Rows.Count > 0)
                    {
                        textFoodType.Text = dt.Rows[0]["Name"].ToString();
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
            e.Cancel = true;
            btnOK.Focus();
            //if (this.cmbCheckItem.SelectedValue != null)
            //{
            //    frmFoodSelect frm = new frmFoodSelect(this.cmbCheckItem.SelectedValue.ToString(), sFoodSelectedValue);
            //    frm.ShowDialog(this);
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        this.sFoodSelectedValue = frm.sNodeTag;
            //        this.cmbFood.Text = frm.sNodeName;
            //        strSign = frm.sSign;
            //        dTestValue = Convert.ToDecimal(frm.sValue);
            //        strUnit = frm.sUnit;
            //        this.txtStandValue.Text = frm.sValue;
            //    }
            //}
            //else
            //{
            //    if (this.cmbCheckMachine.SelectedValue != null)
            //    {
            //        MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置！");
            //    }
            //    else
            //    {
            //        MessageBox.Show(this, "没有选择检测项目！");
            //    }
            //}
            //e.Cancel = true;
        }

        private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            frmCheckedCompany formCheckedComSelect = new frmCheckedCompany("", sCheckedUpperComSelectedValue);
            if (!FrmMain.AllownChange)
            {
                formCheckedComSelect.Tag = "Checked";
            }
            else
            {
                formCheckedComSelect.Tag = "Checked";
            }
            formCheckedComSelect.ShowDialog(this);
            if (formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                cmbCheckedCompany.Text = formCheckedComSelect.sNodeName;
                cmbUpperCompany.Text = formCheckedComSelect.sParentCompanyName;
                sCheckedUpperComSelectedValue = formCheckedComSelect.sNodeTag;
                txtCompanyInfo.Text = formCheckedComSelect.sDisplayName;
            }
            formCheckedComSelect.Hide();
            e.Cancel = true;
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
            //switch (this.cmbCheckType.SelectedIndex)
            //{
            //    case 0:
            //    case 1:
            //        this.txtOrCheckNo.Enabled=false;
            //        break;
            //    case 2:
            //    case 3:
            //        this.txtOrCheckNo.Enabled=true;
            //        break;
            //}
        }

        private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmCheckedComSelect frm = new frmCheckedComSelect("", sProduceComSelectedValue);
            frm.Tag = "Produce";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.sProduceComSelectedValue = frm.sNodeTag;
                this.cmbProduceCompany.Text = frm.sNodeName;
            }
            else
            {
                this.sProduceComSelectedValue = string.Empty;
                this.cmbProduceCompany.Text = string.Empty;
            }
            e.Cancel = true;

        }

        private void cmbProducePlace_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmProduceAreaSelect frm = new frmProduceAreaSelect(this.sProducePlaceSelectValue);
            frm.Tag = "ProducePlace";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.sProducePlaceSelectValue = frm.sNodeTag;
                this.cmbProducePlace.Text = frm.sNodeName;
            }
            else
            {
                this.sProducePlaceSelectValue = string.Empty;
                this.cmbProducePlace.Text = string.Empty;
            }
            e.Cancel = true;
        }

        //private void cmbCheckedCompany_LostFocus(object sender, EventArgs e)
        //{
        //    //if((!this.sCheckedComSelectedValue.Equals(""))&&this.cmbCheckedCompany.ComboStyle==C1.Win.C1List.ComboStyleEnum.DropdownCombo)
        //    //{
        //    //    string strComName=clsCompanyOpr.NameFromCode(this.sCheckedComSelectedValue);
        //    //    if(!this.cmbCheckedCompany.Text.Trim().Equals(strComName))
        //    //    {
        //    //        this.sCheckedComSelectedValue = string.Empty;
        //    //    }
        //    //}
        //}

        private void cmbUpperCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            if (!FrmMain.IsLoadCheckedUpperComSel)
            {
                FrmMain.formCheckedUpperComSelect = new frmCheckedComSelect("", sCheckedUpperComSelectedValue);
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";

                //FrmMain.formCheckedComSelect = new frmCheckedCompany("", sCheckedUpperComSelectedValue);
                //FrmMain.formCheckedComSelect.Tag = "Checked";
            }
            else
            {
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
            }
            FrmMain.formCheckedUpperComSelect.ShowDialog(this);
            if (FrmMain.formCheckedUpperComSelect.DialogResult == DialogResult.OK)
            {
                if (this.sCheckedUpperComSelectedValue.Equals("") || (!this.sCheckedUpperComSelectedValue.Equals(FrmMain.formCheckedUpperComSelect.sNodeTag)))
                {
                    this.sCheckedUpperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    this.cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                    this.sCheckedComSelectedValue = string.Empty;
                    this.cmbCheckedCompany.Text = string.Empty;
                    this.txtCompanyInfo.Text = string.Empty;
                }
                else
                {
                    this.sCheckedUpperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    this.cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                }
            }
            FrmMain.formCheckedUpperComSelect.Hide();
            e.Cancel = true;
        }

        private void txtCheckValueInfo_LostFocus(object sender, EventArgs e)
        {
            this.txtCheckValueInfo.Text = this.txtCheckValueInfo.Text.Trim();
            if (!this.txtCheckValueInfo.Text.Equals(""))
            {
                if (StringUtil.IsNumeric(this.txtCheckValueInfo.Text))
                {
                    switch (strSign)
                    {
                        case "<":
                            if (Decimal.Parse(this.txtCheckValueInfo.Text) >= dTestValue)
                            {
                                this.cmbResult.Text = "不合格";
                            }
                            else
                            {
                                this.cmbResult.Text = "合格";
                            }
                            break;
                        case "≤":
                            if (Decimal.Parse(this.txtCheckValueInfo.Text) > dTestValue)
                            {
                                this.cmbResult.Text = "不合格";
                            }
                            else
                            {
                                this.cmbResult.Text = "合格";
                            }
                            break;
                        case ">":
                            if (Decimal.Parse(this.txtCheckValueInfo.Text) <= dTestValue)
                            {
                                this.cmbResult.Text = "不合格";
                            }
                            else
                            {
                                this.cmbResult.Text = "合格";
                            }
                            break;
                        case "≥":
                            if (Decimal.Parse(this.txtCheckValueInfo.Text) < dTestValue)
                            {
                                this.cmbResult.Text = "不合格";
                            }
                            else
                            {
                                this.cmbResult.Text = "合格";
                            }
                            break;
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            frmCheckedCompany formCheckedComSelect = new frmCheckedCompany("", sCheckedUpperComSelectedValue);
            if (!FrmMain.AllownChange)
            {
                formCheckedComSelect.Tag = "Checked";
            }
            else
            {
                formCheckedComSelect.Tag = "Checked";
            }
            formCheckedComSelect.ShowDialog(this);
            if (formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                cmbCheckedCompany.Text = formCheckedComSelect.sNodeName;
                cmbUpperCompany.Text = formCheckedComSelect.sParentCompanyName;
                sCheckedUpperComSelectedValue = formCheckedComSelect.sNodeTag;
                txtCompanyInfo.Text = formCheckedComSelect.sDisplayName;
            }
            formCheckedComSelect.Hide();
        }

        private void BindCompanies()
        {
            cmbCheckedCompany.DataSource = FrmMain.CompanyTable;

            cmbCheckedCompany.DisplayMember = "FullName";
            cmbCheckedCompany.ValueMember = "SysCode";
        }

        private void cmbCheckedCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            sCheckedComSelectedValue = ((DataRowView)cmbCheckedCompany.SelectedItem)["SysCode"].ToString();
            cmbCheckedCompany.Text = ((DataRowView)cmbCheckedCompany.SelectedItem)["FullName"].ToString();

            txtCompanyInfo.Text = clsCompanyOpr.CompanyInfo(cmbCheckedCompany.Text);

            cmbUpperCompany.Text = clsProprietorsOpr.CiidNameFromCode(cmbCheckedCompany.Text);

            if (cmbUpperCompany.Text.Equals(""))
            {
                cmbUpperCompany.Text = cmbCheckedCompany.Text;
            }
        }

        private void dtpProduceDate_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProduceDate.Text))
            {
                txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
            }
        }

        private string _checkItemsCode = string.Empty;
        private void cmbCheckItemAnHui_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbCheckItemAnHui.SelectedItem) != null)
            {
                _checkItemsCode = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["codeId"].ToString();
                cmbCheckItemAnHui.Text = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["name"].ToString();
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
