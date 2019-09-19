using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.IO;

namespace FoodClient
{
	/// <summary>
	/// frmPesticideMeasureEdit 的摘要说明。
	/// </summary>
	public class frmPesticideMeasureEdit : System.Windows.Forms.Form
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
		private C1.Win.C1List.C1Combo cmbCheckedCompany;
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
		private System.Windows.Forms.DateTimePicker dtpProduceDate;
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

        private int queryType;

        /// <summary>
        /// 检测数据修改
        /// </summary>
       /// <param name="selectType">1代表标准速测法查询,2表示其他查询, 3表示综合查询</param>
        public frmPesticideMeasureEdit(int selectType)
        {
            InitializeComponent();
            queryType = selectType;
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
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.txtSampleBaseNum = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbFood = new C1.Win.C1List.C1Combo();
            this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
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
            this.dtpProduceDate = new System.Windows.Forms.DateTimePicker();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
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
            this.txtSampleNum.Location = new System.Drawing.Point(328, 190);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleNum.TabIndex = 19;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Enabled = false;
            this.txtCheckNo.Location = new System.Drawing.Point(80, 25);
            this.txtCheckNo.MaxLength = 50;
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(150, 21);
            this.txtCheckNo.TabIndex = 2;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(88, 528);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 72;
            this.txtSysID.Visible = false;
            // 
            // txtSampleBaseNum
            // 
            this.txtSampleBaseNum.Location = new System.Drawing.Point(592, 187);
            this.txtSampleBaseNum.Name = "txtSampleBaseNum";
            this.txtSampleBaseNum.Size = new System.Drawing.Size(150, 21);
            this.txtSampleBaseNum.TabIndex = 20;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(80, 352);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(664, 56);
            this.txtRemark.TabIndex = 43;
            // 
            // txtStandard
            // 
            this.txtStandard.Location = new System.Drawing.Point(592, 49);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.Size = new System.Drawing.Size(150, 21);
            this.txtStandard.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 528);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 71;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(560, 520);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
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
            this.cmbFood.FooterStyle = style3;
            this.cmbFood.GapHeight = 2;
            this.cmbFood.HeadingStyle = style4;
            this.cmbFood.HighLightRowStyle = style5;
            this.cmbFood.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbFood.Images"))));
            this.cmbFood.ItemHeight = 15;
            this.cmbFood.Location = new System.Drawing.Point(80, 96);
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
            // cmbCheckedCompany
            // 
            this.cmbCheckedCompany.AddItemSeparator = ';';
            this.cmbCheckedCompany.Caption = "";
            this.cmbCheckedCompany.CaptionHeight = 17;
            this.cmbCheckedCompany.CaptionStyle = style9;
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
            this.cmbCheckedCompany.EvenRowStyle = style10;
            this.cmbCheckedCompany.FooterStyle = style11;
            this.cmbCheckedCompany.GapHeight = 2;
            this.cmbCheckedCompany.HeadingStyle = style12;
            this.cmbCheckedCompany.HighLightRowStyle = style13;
            this.cmbCheckedCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckedCompany.Images"))));
            this.cmbCheckedCompany.ItemHeight = 15;
            this.cmbCheckedCompany.Location = new System.Drawing.Point(328, 72);
            this.cmbCheckedCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckedCompany.MaxDropDownItems = ((short)(5));
            this.cmbCheckedCompany.MaxLength = 10;
            this.cmbCheckedCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckedCompany.Name = "cmbCheckedCompany";
            this.cmbCheckedCompany.OddRowStyle = style14;
            this.cmbCheckedCompany.PartialRightColumn = false;
            this.cmbCheckedCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckedCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.SelectedStyle = style15;
            this.cmbCheckedCompany.Size = new System.Drawing.Size(150, 22);
            this.cmbCheckedCompany.Style = style16;
            this.cmbCheckedCompany.TabIndex = 5;
            this.cmbCheckedCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbCheckedCompany_BeforeOpen);
            this.cmbCheckedCompany.LostFocus += new System.EventHandler(this.cmbCheckedCompany_LostFocus);
            this.cmbCheckedCompany.PropBag = resources.GetString("cmbCheckedCompany.PropBag");
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(17, 305);
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
            this.cmbCheckUnit.CaptionStyle = style17;
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
            this.cmbCheckUnit.EvenRowStyle = style18;
            this.cmbCheckUnit.FooterStyle = style19;
            this.cmbCheckUnit.GapHeight = 2;
            this.cmbCheckUnit.HeadingStyle = style20;
            this.cmbCheckUnit.HighLightRowStyle = style21;
            this.cmbCheckUnit.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckUnit.Images"))));
            this.cmbCheckUnit.ItemHeight = 15;
            this.cmbCheckUnit.Location = new System.Drawing.Point(80, 328);
            this.cmbCheckUnit.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckUnit.MaxDropDownItems = ((short)(5));
            this.cmbCheckUnit.MaxLength = 10;
            this.cmbCheckUnit.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckUnit.Name = "cmbCheckUnit";
            this.cmbCheckUnit.OddRowStyle = style22;
            this.cmbCheckUnit.PartialRightColumn = false;
            this.cmbCheckUnit.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckUnit.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.SelectedStyle = style23;
            this.cmbCheckUnit.Size = new System.Drawing.Size(150, 22);
            this.cmbCheckUnit.Style = style24;
            this.cmbCheckUnit.TabIndex = 40;
            this.cmbCheckUnit.PropBag = resources.GetString("cmbCheckUnit.PropBag");
            // 
            // lblReferStandard
            // 
            this.lblReferStandard.Location = new System.Drawing.Point(525, 49);
            this.lblReferStandard.Name = "lblReferStandard";
            this.lblReferStandard.Size = new System.Drawing.Size(66, 21);
            this.lblReferStandard.TabIndex = 97;
            this.lblReferStandard.Text = "检测依据：";
            this.lblReferStandard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSample
            // 
            this.lblSample.Location = new System.Drawing.Point(17, 97);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(66, 17);
            this.lblSample.TabIndex = 54;
            this.lblSample.Text = "商品名称：";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(237, 73);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 17);
            this.lblName.TabIndex = 74;
            this.lblName.Text = "受检人/单位：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(521, 187);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 21);
            this.label22.TabIndex = 81;
            this.label22.Text = "抽样基数：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(17, 352);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 21);
            this.label23.TabIndex = 68;
            this.label23.Text = "处理情况：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(17, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(66, 21);
            this.label25.TabIndex = 50;
            this.label25.Text = "检测编号：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(265, 190);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 21);
            this.label27.TabIndex = 58;
            this.label27.Text = "抽样数量：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(17, 329);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(66, 21);
            this.label28.TabIndex = 67;
            this.label28.Text = "检测单位：";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImportNum
            // 
            this.txtImportNum.Location = new System.Drawing.Point(80, 211);
            this.txtImportNum.MaxLength = 50;
            this.txtImportNum.Name = "txtImportNum";
            this.txtImportNum.Size = new System.Drawing.Size(150, 21);
            this.txtImportNum.TabIndex = 22;
            // 
            // txtCheckValueInfo
            // 
            this.txtCheckValueInfo.Location = new System.Drawing.Point(80, 281);
            this.txtCheckValueInfo.Name = "txtCheckValueInfo";
            this.txtCheckValueInfo.Size = new System.Drawing.Size(150, 21);
            this.txtCheckValueInfo.TabIndex = 33;
            this.txtCheckValueInfo.LostFocus += new System.EventHandler(this.txtCheckValueInfo_LostFocus);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(17, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 21);
            this.label13.TabIndex = 59;
            this.label13.Text = "进货数量：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(257, 258);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 21);
            this.label18.TabIndex = 84;
            this.label18.Text = "抽样日期：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(257, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 86;
            this.label1.Text = "检测人：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTakeDate
            // 
            this.dtpTakeDate.Location = new System.Drawing.Point(328, 258);
            this.dtpTakeDate.Name = "dtpTakeDate";
            this.dtpTakeDate.Size = new System.Drawing.Size(150, 21);
            this.dtpTakeDate.TabIndex = 29;
            // 
            // dtpCheckStartDate
            // 
            this.dtpCheckStartDate.CustomFormat = "";
            this.dtpCheckStartDate.Location = new System.Drawing.Point(592, 258);
            this.dtpCheckStartDate.Name = "dtpCheckStartDate";
            this.dtpCheckStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtpCheckStartDate.TabIndex = 30;
            // 
            // cmbChecker
            // 
            this.cmbChecker.AddItemSeparator = ';';
            this.cmbChecker.Caption = "";
            this.cmbChecker.CaptionHeight = 17;
            this.cmbChecker.CaptionStyle = style25;
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
            this.cmbChecker.EvenRowStyle = style26;
            this.cmbChecker.FooterStyle = style27;
            this.cmbChecker.GapHeight = 2;
            this.cmbChecker.HeadingStyle = style28;
            this.cmbChecker.HighLightRowStyle = style29;
            this.cmbChecker.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbChecker.Images"))));
            this.cmbChecker.ItemHeight = 15;
            this.cmbChecker.Location = new System.Drawing.Point(328, 304);
            this.cmbChecker.MatchEntryTimeout = ((long)(2000));
            this.cmbChecker.MaxDropDownItems = ((short)(5));
            this.cmbChecker.MaxLength = 10;
            this.cmbChecker.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbChecker.Name = "cmbChecker";
            this.cmbChecker.OddRowStyle = style30;
            this.cmbChecker.PartialRightColumn = false;
            this.cmbChecker.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbChecker.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbChecker.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbChecker.SelectedStyle = style31;
            this.cmbChecker.Size = new System.Drawing.Size(150, 22);
            this.cmbChecker.Style = style32;
            this.cmbChecker.TabIndex = 38;
            this.cmbChecker.PropBag = resources.GetString("cmbChecker.PropBag");
            // 
            // lblSuppresser
            // 
            this.lblSuppresser.Location = new System.Drawing.Point(11, 281);
            this.lblSuppresser.Name = "lblSuppresser";
            this.lblSuppresser.Size = new System.Drawing.Size(72, 21);
            this.lblSuppresser.TabIndex = 0;
            this.lblSuppresser.Text = "抑制率：";
            this.lblSuppresser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(525, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 21);
            this.label10.TabIndex = 96;
            this.label10.Text = "检测日期：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleModel
            // 
            this.txtSampleModel.Location = new System.Drawing.Point(328, 97);
            this.txtSampleModel.Name = "txtSampleModel";
            this.txtSampleModel.Size = new System.Drawing.Size(150, 21);
            this.txtSampleModel.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(257, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 75;
            this.label2.Text = "规格型号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleLevel
            // 
            this.txtSampleLevel.Location = new System.Drawing.Point(592, 97);
            this.txtSampleLevel.MaxLength = 50;
            this.txtSampleLevel.Name = "txtSampleLevel";
            this.txtSampleLevel.Size = new System.Drawing.Size(150, 21);
            this.txtSampleLevel.TabIndex = 9;
            // 
            // txtSampleState
            // 
            this.txtSampleState.Location = new System.Drawing.Point(80, 120);
            this.txtSampleState.MaxLength = 50;
            this.txtSampleState.Name = "txtSampleState";
            this.txtSampleState.Size = new System.Drawing.Size(150, 21);
            this.txtSampleState.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 55;
            this.label3.Text = "批号或编号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(525, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 21);
            this.label12.TabIndex = 89;
            this.label12.Text = "质量等级：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaveNum
            // 
            this.txtSaveNum.Location = new System.Drawing.Point(328, 234);
            this.txtSaveNum.MaxLength = 20;
            this.txtSaveNum.Name = "txtSaveNum";
            this.txtSaveNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaveNum.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(257, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 21);
            this.label8.TabIndex = 83;
            this.label8.Text = "库存数量：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSentCompany
            // 
            this.txtSentCompany.Location = new System.Drawing.Point(328, 166);
            this.txtSentCompany.Name = "txtSentCompany";
            this.txtSentCompany.Size = new System.Drawing.Size(414, 21);
            this.txtSentCompany.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(255, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 95;
            this.label4.Text = "送检单位：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(328, 120);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(150, 21);
            this.txtProvider.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 21);
            this.label5.TabIndex = 62;
            this.label5.Text = "检测项目：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(235, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 21);
            this.label6.TabIndex = 78;
            this.label6.Text = "供货商/商标：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(17, 166);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(66, 21);
            this.label31.TabIndex = 57;
            this.label31.Text = "检测类型：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(525, 305);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(66, 21);
            this.label33.TabIndex = 98;
            this.label33.Text = "核准人：";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAssessor
            // 
            this.cmbAssessor.AddItemSeparator = ';';
            this.cmbAssessor.Caption = "";
            this.cmbAssessor.CaptionHeight = 17;
            this.cmbAssessor.CaptionStyle = style33;
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
            this.cmbAssessor.EvenRowStyle = style34;
            this.cmbAssessor.FooterStyle = style35;
            this.cmbAssessor.GapHeight = 2;
            this.cmbAssessor.HeadingStyle = style36;
            this.cmbAssessor.HighLightRowStyle = style37;
            this.cmbAssessor.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbAssessor.Images"))));
            this.cmbAssessor.ItemHeight = 15;
            this.cmbAssessor.Location = new System.Drawing.Point(592, 304);
            this.cmbAssessor.MatchEntryTimeout = ((long)(2000));
            this.cmbAssessor.MaxDropDownItems = ((short)(5));
            this.cmbAssessor.MaxLength = 10;
            this.cmbAssessor.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbAssessor.Name = "cmbAssessor";
            this.cmbAssessor.OddRowStyle = style38;
            this.cmbAssessor.PartialRightColumn = false;
            this.cmbAssessor.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbAssessor.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbAssessor.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbAssessor.SelectedStyle = style39;
            this.cmbAssessor.Size = new System.Drawing.Size(150, 22);
            this.cmbAssessor.Style = style40;
            this.cmbAssessor.TabIndex = 39;
            this.cmbAssessor.PropBag = resources.GetString("cmbAssessor.PropBag");
            // 
            // cmbOrganizer
            // 
            this.cmbOrganizer.AddItemSeparator = ';';
            this.cmbOrganizer.Caption = "";
            this.cmbOrganizer.CaptionHeight = 17;
            this.cmbOrganizer.CaptionStyle = style41;
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
            this.cmbOrganizer.EvenRowStyle = style42;
            this.cmbOrganizer.FooterStyle = style43;
            this.cmbOrganizer.GapHeight = 2;
            this.cmbOrganizer.HeadingStyle = style44;
            this.cmbOrganizer.HighLightRowStyle = style45;
            this.cmbOrganizer.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbOrganizer.Images"))));
            this.cmbOrganizer.ItemHeight = 15;
            this.cmbOrganizer.Location = new System.Drawing.Point(80, 257);
            this.cmbOrganizer.MatchEntryTimeout = ((long)(2000));
            this.cmbOrganizer.MaxDropDownItems = ((short)(5));
            this.cmbOrganizer.MaxLength = 10;
            this.cmbOrganizer.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbOrganizer.Name = "cmbOrganizer";
            this.cmbOrganizer.OddRowStyle = style46;
            this.cmbOrganizer.PartialRightColumn = false;
            this.cmbOrganizer.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbOrganizer.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.SelectedStyle = style47;
            this.cmbOrganizer.Size = new System.Drawing.Size(150, 22);
            this.cmbOrganizer.Style = style48;
            this.cmbOrganizer.TabIndex = 28;
            this.cmbOrganizer.PropBag = resources.GetString("cmbOrganizer.PropBag");
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(17, 258);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 21);
            this.label35.TabIndex = 61;
            this.label35.Text = "抽样人：";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 48;
            this.label7.Text = "检测仪器：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckMachine
            // 
            this.cmbCheckMachine.AddItemSeparator = ';';
            this.cmbCheckMachine.Caption = "";
            this.cmbCheckMachine.CaptionHeight = 17;
            this.cmbCheckMachine.CaptionStyle = style49;
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
            this.cmbCheckMachine.EvenRowStyle = style50;
            this.cmbCheckMachine.FooterStyle = style51;
            this.cmbCheckMachine.GapHeight = 2;
            this.cmbCheckMachine.HeadingStyle = style52;
            this.cmbCheckMachine.HighLightRowStyle = style53;
            this.cmbCheckMachine.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckMachine.Images"))));
            this.cmbCheckMachine.ItemHeight = 15;
            this.cmbCheckMachine.Location = new System.Drawing.Point(80, 1);
            this.cmbCheckMachine.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckMachine.MaxDropDownItems = ((short)(5));
            this.cmbCheckMachine.MaxLength = 10;
            this.cmbCheckMachine.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckMachine.Name = "cmbCheckMachine";
            this.cmbCheckMachine.OddRowStyle = style54;
            this.cmbCheckMachine.PartialRightColumn = false;
            this.cmbCheckMachine.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckMachine.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.SelectedStyle = style55;
            this.cmbCheckMachine.Size = new System.Drawing.Size(232, 22);
            this.cmbCheckMachine.Style = style56;
            this.cmbCheckMachine.TabIndex = 0;
            this.cmbCheckMachine.SelectedValueChanged += new System.EventHandler(this.cmbCheckMachine_SelectedValueChanged);
            this.cmbCheckMachine.PropBag = resources.GetString("cmbCheckMachine.PropBag");
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(648, 520);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkNoHaveMachine
            // 
            this.chkNoHaveMachine.Location = new System.Drawing.Point(328, 1);
            this.chkNoHaveMachine.Name = "chkNoHaveMachine";
            this.chkNoHaveMachine.Size = new System.Drawing.Size(88, 22);
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
            this.cmbCheckItem.CaptionStyle = style57;
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
            this.cmbCheckItem.EvenRowStyle = style58;
            this.cmbCheckItem.FooterStyle = style59;
            this.cmbCheckItem.GapHeight = 2;
            this.cmbCheckItem.HeadingStyle = style60;
            this.cmbCheckItem.HighLightRowStyle = style61;
            this.cmbCheckItem.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckItem.Images"))));
            this.cmbCheckItem.ItemHeight = 15;
            this.cmbCheckItem.Location = new System.Drawing.Point(78, 47);
            this.cmbCheckItem.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckItem.MaxDropDownItems = ((short)(5));
            this.cmbCheckItem.MaxLength = 10;
            this.cmbCheckItem.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckItem.Name = "cmbCheckItem";
            this.cmbCheckItem.OddRowStyle = style62;
            this.cmbCheckItem.PartialRightColumn = false;
            this.cmbCheckItem.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckItem.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.SelectedStyle = style63;
            this.cmbCheckItem.Size = new System.Drawing.Size(400, 22);
            this.cmbCheckItem.Style = style64;
            this.cmbCheckItem.TabIndex = 31;
            this.cmbCheckItem.SelectedValueChanged += new System.EventHandler(this.cmbCheckItem_SelectedValueChanged);
            this.cmbCheckItem.PropBag = resources.GetString("cmbCheckItem.PropBag");
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.Location = new System.Drawing.Point(328, 23);
            this.txtSampleCode.MaxLength = 50;
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.Size = new System.Drawing.Size(150, 21);
            this.txtSampleCode.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(261, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 21);
            this.label11.TabIndex = 92;
            this.label11.Text = "样品编号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleUnit
            // 
            this.txtSampleUnit.Location = new System.Drawing.Point(592, 210);
            this.txtSampleUnit.Name = "txtSampleUnit";
            this.txtSampleUnit.Size = new System.Drawing.Size(150, 21);
            this.txtSampleUnit.TabIndex = 21;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(592, 233);
            this.txtUnit.MaxLength = 50;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(150, 21);
            this.txtUnit.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(494, 211);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 21);
            this.label14.TabIndex = 93;
            this.label14.Text = "抽样数据单位：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(525, 233);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 21);
            this.label15.TabIndex = 94;
            this.label15.Text = "数据单位：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(17, 73);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(66, 17);
            this.lblParent.TabIndex = 52;
            this.lblParent.Text = "所属市场：";
            this.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpProduceDate
            // 
            this.dtpProduceDate.Location = new System.Drawing.Point(210, 143);
            this.dtpProduceDate.Name = "dtpProduceDate";
            this.dtpProduceDate.Size = new System.Drawing.Size(20, 21);
            this.dtpProduceDate.TabIndex = 13;
            this.dtpProduceDate.Value = new System.DateTime(2000, 1, 1, 15, 3, 0, 0);
            this.dtpProduceDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            // 
            // cmbProduceCompany
            // 
            this.cmbProduceCompany.AddItemSeparator = ';';
            this.cmbProduceCompany.Caption = "";
            this.cmbProduceCompany.CaptionHeight = 17;
            this.cmbProduceCompany.CaptionStyle = style65;
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
            this.cmbProduceCompany.EvenRowStyle = style66;
            this.cmbProduceCompany.FooterStyle = style67;
            this.cmbProduceCompany.GapHeight = 2;
            this.cmbProduceCompany.HeadingStyle = style68;
            this.cmbProduceCompany.HighLightRowStyle = style69;
            this.cmbProduceCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProduceCompany.Images"))));
            this.cmbProduceCompany.ItemHeight = 15;
            this.cmbProduceCompany.Location = new System.Drawing.Point(328, 142);
            this.cmbProduceCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbProduceCompany.MaxDropDownItems = ((short)(5));
            this.cmbProduceCompany.MaxLength = 10;
            this.cmbProduceCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProduceCompany.Name = "cmbProduceCompany";
            this.cmbProduceCompany.OddRowStyle = style70;
            this.cmbProduceCompany.PartialRightColumn = false;
            this.cmbProduceCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProduceCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.SelectedStyle = style71;
            this.cmbProduceCompany.Size = new System.Drawing.Size(150, 22);
            this.cmbProduceCompany.Style = style72;
            this.cmbProduceCompany.TabIndex = 14;
            this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
            this.cmbProduceCompany.PropBag = resources.GetString("cmbProduceCompany.PropBag");
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(257, 143);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 21);
            this.label17.TabIndex = 79;
            this.label17.Text = ShareOption.ProductionUnitNameTag + "：";// "生产单位：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(17, 143);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 21);
            this.label19.TabIndex = 56;
            this.label19.Text = "生产日期：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrCheckNo
            // 
            this.txtOrCheckNo.Location = new System.Drawing.Point(80, 188);
            this.txtOrCheckNo.MaxLength = 50;
            this.txtOrCheckNo.Name = "txtOrCheckNo";
            this.txtOrCheckNo.Size = new System.Drawing.Size(150, 21);
            this.txtOrCheckNo.TabIndex = 17;
            // 
            // txtStdCode
            // 
            this.txtStdCode.Location = new System.Drawing.Point(592, 120);
            this.txtStdCode.Name = "txtStdCode";
            this.txtStdCode.Size = new System.Drawing.Size(150, 21);
            this.txtStdCode.TabIndex = 12;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(0, 188);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 21);
            this.label20.TabIndex = 80;
            this.label20.Text = "原检测编号：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(525, 120);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 21);
            this.label24.TabIndex = 90;
            this.label24.Text = "条形码：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(475, 73);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(116, 17);
            this.lblDomain.TabIndex = 88;
            this.lblDomain.Text = "档口/店面/车牌号：";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompanyInfo
            // 
            this.txtCompanyInfo.Enabled = false;
            this.txtCompanyInfo.Location = new System.Drawing.Point(592, 73);
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
            this.cmbCheckType.Location = new System.Drawing.Point(80, 166);
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
            this.cmbResult.CaptionStyle = style73;
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
            this.cmbResult.EvenRowStyle = style74;
            this.cmbResult.FooterStyle = style75;
            this.cmbResult.GapHeight = 2;
            this.cmbResult.HeadingStyle = style76;
            this.cmbResult.HighLightRowStyle = style77;
            this.cmbResult.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbResult.Images"))));
            this.cmbResult.ItemHeight = 15;
            this.cmbResult.Location = new System.Drawing.Point(80, 304);
            this.cmbResult.MatchEntryTimeout = ((long)(2000));
            this.cmbResult.MaxDropDownItems = ((short)(5));
            this.cmbResult.MaxLength = 32767;
            this.cmbResult.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.OddRowStyle = style78;
            this.cmbResult.PartialRightColumn = false;
            this.cmbResult.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbResult.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbResult.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbResult.SelectedStyle = style79;
            this.cmbResult.Size = new System.Drawing.Size(150, 22);
            this.cmbResult.Style = style80;
            this.cmbResult.TabIndex = 37;
            this.cmbResult.PropBag = resources.GetString("cmbResult.PropBag");
            // 
            // txtStandValue
            // 
            this.txtStandValue.Enabled = false;
            this.txtStandValue.Location = new System.Drawing.Point(328, 281);
            this.txtStandValue.MaxLength = 50;
            this.txtStandValue.Name = "txtStandValue";
            this.txtStandValue.Size = new System.Drawing.Size(150, 21);
            this.txtStandValue.TabIndex = 34;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(257, 281);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(66, 21);
            this.label30.TabIndex = 85;
            this.label30.Text = "标准值：";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProducePlace
            // 
            this.cmbProducePlace.AddItemSeparator = ';';
            this.cmbProducePlace.Caption = "";
            this.cmbProducePlace.CaptionHeight = 17;
            this.cmbProducePlace.CaptionStyle = style81;
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
            this.cmbProducePlace.EvenRowStyle = style82;
            this.cmbProducePlace.FooterStyle = style83;
            this.cmbProducePlace.GapHeight = 2;
            this.cmbProducePlace.HeadingStyle = style84;
            this.cmbProducePlace.HighLightRowStyle = style85;
            this.cmbProducePlace.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProducePlace.Images"))));
            this.cmbProducePlace.ItemHeight = 15;
            this.cmbProducePlace.Location = new System.Drawing.Point(592, 142);
            this.cmbProducePlace.MatchEntryTimeout = ((long)(2000));
            this.cmbProducePlace.MaxDropDownItems = ((short)(5));
            this.cmbProducePlace.MaxLength = 10;
            this.cmbProducePlace.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProducePlace.Name = "cmbProducePlace";
            this.cmbProducePlace.OddRowStyle = style86;
            this.cmbProducePlace.PartialRightColumn = false;
            this.cmbProducePlace.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProducePlace.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.SelectedStyle = style87;
            this.cmbProducePlace.Size = new System.Drawing.Size(150, 22);
            this.cmbProducePlace.Style = style88;
            this.cmbProducePlace.TabIndex = 15;
            this.cmbProducePlace.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProducePlace_BeforeOpen);
            this.cmbProducePlace.PropBag = resources.GetString("cmbProducePlace.PropBag");
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(525, 143);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 21);
            this.label29.TabIndex = 91;
            this.label29.Text = "产地：";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtResultInfo
            // 
            this.txtResultInfo.Enabled = false;
            this.txtResultInfo.Location = new System.Drawing.Point(592, 281);
            this.txtResultInfo.MaxLength = 50;
            this.txtResultInfo.Name = "txtResultInfo";
            this.txtResultInfo.Size = new System.Drawing.Size(150, 21);
            this.txtResultInfo.TabIndex = 36;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(511, 281);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(80, 21);
            this.label32.TabIndex = 35;
            this.label32.Text = "检测值单位：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUpperCompany
            // 
            this.cmbUpperCompany.AddItemSeparator = ';';
            this.cmbUpperCompany.Caption = "";
            this.cmbUpperCompany.CaptionHeight = 17;
            this.cmbUpperCompany.CaptionStyle = style89;
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
            this.cmbUpperCompany.EvenRowStyle = style90;
            this.cmbUpperCompany.FooterStyle = style91;
            this.cmbUpperCompany.GapHeight = 2;
            this.cmbUpperCompany.HeadingStyle = style92;
            this.cmbUpperCompany.HighLightRowStyle = style93;
            this.cmbUpperCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbUpperCompany.Images"))));
            this.cmbUpperCompany.ItemHeight = 15;
            this.cmbUpperCompany.Location = new System.Drawing.Point(80, 72);
            this.cmbUpperCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbUpperCompany.MaxDropDownItems = ((short)(5));
            this.cmbUpperCompany.MaxLength = 10;
            this.cmbUpperCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbUpperCompany.Name = "cmbUpperCompany";
            this.cmbUpperCompany.OddRowStyle = style94;
            this.cmbUpperCompany.PartialRightColumn = false;
            this.cmbUpperCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbUpperCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.SelectedStyle = style95;
            this.cmbUpperCompany.Size = new System.Drawing.Size(150, 22);
            this.cmbUpperCompany.Style = style96;
            this.cmbUpperCompany.TabIndex = 4;
            this.cmbUpperCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbUpperCompany_BeforeOpen);
            this.cmbUpperCompany.PropBag = resources.GetString("cmbUpperCompany.PropBag");
            // 
            // cmbCheckerVal
            // 
            this.cmbCheckerVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckerVal.Items.AddRange(new object[] {
            "无异议",
            "有异议",
            ""});
            this.cmbCheckerVal.Location = new System.Drawing.Point(328, 329);
            this.cmbCheckerVal.Name = "cmbCheckerVal";
            this.cmbCheckerVal.Size = new System.Drawing.Size(150, 20);
            this.cmbCheckerVal.TabIndex = 41;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(235, 329);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(88, 21);
            this.label48.TabIndex = 87;
            this.label48.Text = "被检人确定：";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(80, 460);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNotes.Size = new System.Drawing.Size(664, 48);
            this.txtNotes.TabIndex = 45;
            // 
            // txtCheckerRemark
            // 
            this.txtCheckerRemark.Location = new System.Drawing.Point(80, 410);
            this.txtCheckerRemark.Multiline = true;
            this.txtCheckerRemark.Name = "txtCheckerRemark";
            this.txtCheckerRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCheckerRemark.Size = new System.Drawing.Size(664, 48);
            this.txtCheckerRemark.TabIndex = 44;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(80, 234);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(150, 21);
            this.txtPrice.TabIndex = 25;
            // 
            // txtSaleNum
            // 
            this.txtSaleNum.Location = new System.Drawing.Point(328, 211);
            this.txtSaleNum.MaxLength = 20;
            this.txtSaleNum.Name = "txtSaleNum";
            this.txtSaleNum.Size = new System.Drawing.Size(150, 21);
            this.txtSaleNum.TabIndex = 23;
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(17, 460);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(66, 21);
            this.label47.TabIndex = 70;
            this.label47.Text = "备注：";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(3, 410);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(80, 21);
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
            this.cmbIsSentCheck.Location = new System.Drawing.Point(592, 329);
            this.cmbIsSentCheck.Name = "cmbIsSentCheck";
            this.cmbIsSentCheck.Size = new System.Drawing.Size(150, 20);
            this.cmbIsSentCheck.TabIndex = 42;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(525, 329);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(66, 21);
            this.label38.TabIndex = 99;
            this.label38.Text = "是否送检：";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(11, 234);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(72, 21);
            this.label37.TabIndex = 60;
            this.label37.Text = "单价(元)：";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(257, 211);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(66, 21);
            this.label36.TabIndex = 82;
            this.label36.Text = "销售数量：";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckPlanCode
            // 
            this.txtCheckPlanCode.Location = new System.Drawing.Point(592, 24);
            this.txtCheckPlanCode.MaxLength = 50;
            this.txtCheckPlanCode.Name = "txtCheckPlanCode";
            this.txtCheckPlanCode.Size = new System.Drawing.Size(150, 21);
            this.txtCheckPlanCode.TabIndex = 3;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(491, 24);
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
            this.label45.Location = new System.Drawing.Point(9, 328);
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
            this.label44.Location = new System.Drawing.Point(13, 280);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(8, 21);
            this.label44.TabIndex = 63;
            this.label44.Text = "*";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.ForeColor = System.Drawing.Color.Red;
            this.label43.Location = new System.Drawing.Point(33, 304);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(8, 21);
            this.label43.TabIndex = 65;
            this.label43.Text = "*";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label42
            // 
            this.label42.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.ForeColor = System.Drawing.Color.Red;
            this.label42.Location = new System.Drawing.Point(9, 24);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(8, 21);
            this.label42.TabIndex = 49;
            this.label42.Text = "*";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label41.ForeColor = System.Drawing.Color.Red;
            this.label41.Location = new System.Drawing.Point(9, 96);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(8, 21);
            this.label41.TabIndex = 53;
            this.label41.Text = "*";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Location = new System.Drawing.Point(233, 72);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(8, 21);
            this.label49.TabIndex = 76;
            this.label49.Text = "*";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label50
            // 
            this.label50.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label50.ForeColor = System.Drawing.Color.Red;
            this.label50.Location = new System.Drawing.Point(9, 72);
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
            this.label39.Location = new System.Drawing.Point(7, 48);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 100;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduceDate
            // 
            this.txtProduceDate.Location = new System.Drawing.Point(80, 143);
            this.txtProduceDate.Name = "txtProduceDate";
            this.txtProduceDate.Size = new System.Drawing.Size(131, 21);
            this.txtProduceDate.TabIndex = 112;
            // 
            // frmPesticideMeasureEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(754, 586);
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
            this.Controls.Add(this.label48);
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
            this.Controls.Add(this.dtpProduceDate);
            this.Controls.Add(this.cmbProduceCompany);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.dtpTakeDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbCheckMachine);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbCheckItem);
            this.Controls.Add(this.chkNoHaveMachine);
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
            this.Controls.Add(this.cmbCheckedCompany);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.cmbCheckUnit);
            this.Controls.Add(this.lblReferStandard);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.lblName);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPesticideMeasureEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmPesticideMeasureEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
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
            bindCheckItem("IsLock=false");

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
                this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownCombo;
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
                this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
                this.cmbUpperCompany.Enabled = false;
                this.txtCompanyInfo.Enabled = false;
            }

            if (!strMachineCode.Equals(""))
            {
                this.cmbCheckMachine.SelectedValue = strMachineCode;
                this.cmbCheckMachine.Text = clsMachineOpr.GetMachineNameFromCode(strMachineCode);
            }
            //else
            //{
            //    this.cmbCheckMachine.SelectedIndex = 0;
            //}

            if (!string.IsNullOrEmpty(strCheckItemCode))// != null && !strCheckItemCode.Equals(""))
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
            if (this.txtCheckNo.Text.Equals(""))
            {
                MessageBox.Show(this, "检测编号必须输入!");
                this.txtCheckNo.Focus();
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
                if (this.sCheckedComSelectedValue.Equals(""))
                {
                    MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                    this.cmbCheckedCompany.Text = "";
                    this.cmbCheckedCompany.Focus();
                    return;
                }
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
            model.StdCode = this.txtStdCode.Text.Trim();
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
                if (model.CheckMachine != null)
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
            clsResultOpr curObjectOpr = new clsResultOpr();
            curObjectOpr.UpdatePart(model, out sErr);

            if (!sErr.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！"+sErr);
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
                dtpProduceDate.Value = Convert.ToDateTime(tempdt);
                txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
            }

            this.sProduceComSelectedValue = model.ProduceCompany;
            this.sProducePlaceSelectValue = model.ProducePlace;
            this.txtSentCompany.Text = model.SentCompany;
            this.txtProvider.Text = model.Provider;
            this.dtpTakeDate.Value = model.TakeDate;
            this.dtpCheckStartDate.Value = model.CheckStartDate;
            this.txtImportNum.Text = model.ImportNum;
            this.txtSaveNum.Text = model.SaleNum;
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
            this.cmbResult.Text = model.Result;
            this.txtResultInfo.Text = model.ResultInfo;
            this.sCheckedUpperComSelectedValue = model.UpperCompany;
            this.cmbUpperCompany.Text = model.UpperCompanyName;
            this.txtOrCheckNo.Text = model.OrCheckNo;

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
                this.txtCheckValueInfo.Enabled = false;
                this.cmbCheckMachine.Enabled = false;
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
                label7.Visible = false;
                cmbCheckMachine.Visible = false;
                txtCheckValueInfo.Enabled = true;
                cmbResult.Enabled = true;
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

            if (this.Tag.ToString().Equals("MX"))
            {
                this.Text = "标准速测法检测数据查看明细";
                this.txtCheckPlanCode.ReadOnly = true;
                this.txtSaleNum.ReadOnly = true;
                this.txtPrice.ReadOnly = true;
                this.cmbCheckerVal.Enabled = false;
                this.cmbIsSentCheck.Enabled = false;
                this.txtCheckerRemark.ReadOnly = true;
                this.txtNotes.ReadOnly = true;
                this.cmbAssessor.ReadOnly = true;
                this.cmbCheckedCompany.ReadOnly = true;
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
                this.btnOK.Visible = false;
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
                    default:
                        this.lblSuppresser.Text = "检测值：";
                        break;
                }
                string strLinkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", this.cmbCheckMachine.SelectedValue.ToString());
                if (sProtocol.Equals("RS232DY3000DY") || sProtocol.Equals("RS232LZ4000TDY"))
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
                        }
                    }
                    if (!blExist)
                    {
                        strCheckItemCode = string.Empty;
                    }
                    strSql = strSql.Substring(0, strSql.Length - 1);
                    strWhere = string.Format("IsLock=false And SysCode In({0})", strSql);
                }
                else
                {
                    strWhere = string.Format("IsLock=false And SysCode ='{0}'", strCheckItems[0, 1].ToString());
                }
                bindCheckItem(strWhere);

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
            catch
            {
                MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置仪器！");
            }
        }

        private void chkNoHaveMachine_CheckedChanged(object sender, System.EventArgs e)
        {
            clsResultOpr curObjectOpr = new clsResultOpr();

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
                    dTestValue = Convert.ToDecimal(strResult[1]);
                    strUnit = strResult[2];
                    if (strSign.Equals("-1") && dTestValue == 0 && strUnit.Equals("-1"))
                    {
                        //this.sFoodSelectedValue = string.Empty;
                        //this.cmbFood.Text = string.Empty;
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
			if(this.cmbCheckItem.SelectedValue!=null)			
			{
				frmFoodSelect frm=new frmFoodSelect(this.cmbCheckItem.SelectedValue.ToString(),sFoodSelectedValue);
				frm.ShowDialog(this);
				if(frm.DialogResult==DialogResult.OK)
				{
					this.sFoodSelectedValue=frm.sNodeTag;
					this.cmbFood.Text=frm.sNodeName;
					strSign = frm.sSign;
					dTestValue = Convert.ToDecimal(frm.sValue);
					strUnit=frm.sUnit;
					this.txtStandValue.Text=frm.sValue;
				}
			}
			else
			{
				if(this.cmbCheckMachine.SelectedValue !=null)
				{
					MessageBox.Show(this,"没有设置仪器所对应的检测项目，请到选项中设置仪器！");
				}
				else
				{
					MessageBox.Show(this,"没有选择检测项目！");
				}
			}
			e.Cancel=true;
		}

		private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
            frmCheckedComSelect comSelect = new frmCheckedComSelect(string.Empty, sCheckedUpperComSelectedValue);
			if(ShareOption.AllowHandInputCheckUint)
			{
				if(!sCheckedUpperComSelectedValue.Equals(""))
				{
                    //comSelect = new frmCheckedComSelect("", sCheckedUpperComSelectedValue);
					comSelect.Tag="Checked";
				}
				else
				{
					MessageBox.Show(this,"请先选择所属市场！");
					e.Cancel=true;
					return;
				}
			}
			else
			{
				if(!FrmMain.IsLoadCheckedComSel)
				{
					comSelect=new frmCheckedComSelect("",sCheckedComSelectedValue);
					comSelect.Tag="Checked";
				}
				else
				{
					comSelect.Tag="Checked";
					comSelect.SetFormValues("",sCheckedComSelectedValue);
				}
			}
			comSelect.ShowDialog(this);
			if(comSelect.DialogResult==DialogResult.OK)
			{
				this.sCheckedComSelectedValue=comSelect.sNodeTag;
				this.cmbCheckedCompany.Text=comSelect.sNodeName;
				if(!ShareOption.AllowHandInputCheckUint)
				{
					if(comSelect.sParentCompanyName.Equals(""))
					{
                        this.cmbUpperCompany.Text = string.Empty;
                        this.sCheckedUpperComSelectedValue = string.Empty;
					}
					else
					{
						this.cmbUpperCompany.Text=comSelect.sParentCompanyName.ToString();
						this.sCheckedUpperComSelectedValue=comSelect.sParentCompanyTag.ToString();

					}
				}
				if(comSelect.sNodeCompanyInfo.Equals(""))
				{
                    this.txtCompanyInfo.Text = string.Empty;
				}
				else
				{
					this.txtCompanyInfo.Text=comSelect.sNodeCompanyInfo.ToString();
				}
			}
			comSelect.Hide();
			e.Cancel=true;
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
			frmCheckedComSelect frm=new frmCheckedComSelect("",sProduceComSelectedValue);
			frm.Tag="Produce";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProduceComSelectedValue=frm.sNodeTag;
				this.cmbProduceCompany.Text=frm.sNodeName;
			}
			else
			{
                this.sProduceComSelectedValue = string.Empty;
                this.cmbProduceCompany.Text = string.Empty;
			}
			e.Cancel=true;
		}

		private void cmbProducePlace_BeforeOpen(object sender, CancelEventArgs e)
		{
			frmProduceAreaSelect frm=new frmProduceAreaSelect(this.sProducePlaceSelectValue);
			frm.Tag="ProducePlace";
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				this.sProducePlaceSelectValue=frm.sNodeTag;
				this.cmbProducePlace.Text=frm.sNodeName;
			}
			else
			{
                this.sProducePlaceSelectValue = string.Empty;
                this.cmbProducePlace.Text = string.Empty;
			}
			e.Cancel=true;
		}

		private void cmbCheckedCompany_LostFocus(object sender, EventArgs e)
		{
			if((!this.sCheckedComSelectedValue.Equals(""))&&this.cmbCheckedCompany.ComboStyle==C1.Win.C1List.ComboStyleEnum.DropdownCombo)
			{
				string strComName=clsCompanyOpr.NameFromCode(this.sCheckedComSelectedValue);
				if(!this.cmbCheckedCompany.Text.Trim().Equals(strComName))
				{
                    this.sCheckedComSelectedValue = string.Empty;
				}
			}
		}

		private void cmbUpperCompany_BeforeOpen(object sender, CancelEventArgs e)
		{
			if(!FrmMain.IsLoadCheckedUpperComSel)
			{
				FrmMain.formCheckedUpperComSelect=new frmCheckedComSelect("",sCheckedUpperComSelectedValue);
				FrmMain.formCheckedUpperComSelect.Tag="UpperChecked";
			}
			else
			{
				FrmMain.formCheckedUpperComSelect.Tag="UpperChecked";
				FrmMain.formCheckedUpperComSelect.SetFormValues("",sCheckedUpperComSelectedValue);
			}
			FrmMain.formCheckedUpperComSelect.ShowDialog(this);
			if(FrmMain.formCheckedUpperComSelect.DialogResult==DialogResult.OK)
			{
				if(this.sCheckedUpperComSelectedValue.Equals("")||(!this.sCheckedUpperComSelectedValue.Equals(FrmMain.formCheckedUpperComSelect.sNodeTag)))
				{
					this.sCheckedUpperComSelectedValue=FrmMain.formCheckedUpperComSelect.sNodeTag;
					this.cmbUpperCompany.Text=FrmMain.formCheckedUpperComSelect.sNodeName;
					this.sCheckedComSelectedValue=string.Empty;
                    this.cmbCheckedCompany.Text = string.Empty;
                    this.txtCompanyInfo.Text = string.Empty;
				}
				else
				{
					this.sCheckedUpperComSelectedValue=FrmMain.formCheckedUpperComSelect.sNodeTag;
					this.cmbUpperCompany.Text=FrmMain.formCheckedUpperComSelect.sNodeName;
				}
			}
			FrmMain.formCheckedUpperComSelect.Hide();
			e.Cancel=true;
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
                //else
                //{
                //}
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
	}
}
