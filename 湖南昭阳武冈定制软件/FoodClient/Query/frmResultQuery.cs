using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmResultQuery 的摘要说明。
	/// </summary>
    public class frmResultQuery : Form
    {
        #region 窗体控件
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private TextBox txtProduceDate;
        private TextBox txtTakeDate;
        private Label label17;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label5;
        private Label label4;
        private System.ComponentModel.Container components = null;
        private Button btnQuery;
        private Button btnCancel;
        private DateTimePicker dtpCheckEndDate;
        private DateTimePicker dtpCheckStartDate;
        private Label label10;
        private C1.Win.C1List.C1Combo cmbFood;
        private C1.Win.C1List.C1Combo cmbProduceCompany;
        private C1.Win.C1List.C1Combo cmbCheckedCompany;
        private C1.Win.C1List.C1Combo cmbCheckUnit;
        private C1.Win.C1List.C1Combo cmbChecker;
        public C1.Win.C1List.C1Combo cmbIsSend;
        private C1.Win.C1List.C1Combo cmbResult;
        private C1.Win.C1List.C1Combo cmbCheckMachine;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label7;
        private C1.Win.C1List.C1Combo cmbCheckMethod;
        private C1.Win.C1List.C1Combo cmbCheckItem;
        private C1.Win.C1List.C1Combo cmbAssessor;
        private Label label15;
        private Label label16;
        private Label label18;
        private Label label19;
        private Label label20;
        private C1.Win.C1List.C1Combo cmbDisplayName;
        private TextBox txtSampleModel;
        private C1.Win.C1List.C1Combo cmbOrganizer;
        private C1.Win.C1List.C1Combo cmbCheckType;
        private TextBox txtProvider;
        private TextBox txtSentCompany;
        private TextBox txtCheckNo;
        private C1.Win.C1List.C1Combo cmbUpperCompany;
        private Label lblParent;
        private C1.Win.C1List.C1Combo cmbProducePlace;
        private Label label29;
        private TextBox txtCheckPlanCode;
        private Label label34;
        private ComboBox cmbCheckerVal;
        private Label label48;
        private ComboBox cmbIsSentCheck;
        private Label label38;
        private Label label21;
        private Label label33;
        private ComboBox cmbIsReSend;
        private Label label22;
        private TextBox txtSender;
        private DateTimePicker dtpSendedDate;
        private Label lblName;
        private Label lblSample;
        private Label lblDomain;
        private TextBox txtSampleCode;
        private Label label6;
        private TextBox txtSampleState;
        private Label label8;
        private TextBox txtStdCode;
        private Label label24;
        private TextBox txtOrCheckNo;
        private Label label9;
        private DateTimePicker dtpProduceDate;
        private Label label23;
        private DateTimePicker dtpTakeDate;
        private Label label25;
        private TextBox txtCheckValueInfo;
        private Label lblSuppresser;
        #endregion

        private string foodSelectedValue = string.Empty;
        private string sCheckedComSelectedValue = string.Empty;
        private string produceComSelectedValue = string.Empty;
        private string producePlaceSelectValue = string.Empty;
        private string sUpperComSelectedValue = string.Empty;
        private string displayComSelectedValue = string.Empty;
        private frmCheckedDisplaySelect formCheckedDisplaySelect = null;


        private int queryType = 0;

        /// <summary>
        /// 组合查询语句
        /// </summary>
        public string QueryString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="selectType">1代表标准速测法查询,2表示其他查询, 3表示综合查询</param>
        public frmResultQuery(int selectType)
        {
            queryType = selectType;
            InitializeComponent();
            QueryString = string.Empty;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultQuery));
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
            C1.Win.C1List.Style style97 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style98 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style99 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style100 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style101 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style102 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style103 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style104 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style105 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style106 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style107 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style108 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style109 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style110 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style111 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style112 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style113 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style114 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style115 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style116 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style117 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style118 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style119 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style120 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style121 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style122 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style123 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style124 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style125 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style126 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style127 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style128 = new C1.Win.C1List.Style();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSample = new System.Windows.Forms.Label();
            this.dtpCheckEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpCheckStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbFood = new C1.Win.C1List.C1Combo();
            this.cmbCheckedCompany = new C1.Win.C1List.C1Combo();
            this.cmbCheckUnit = new C1.Win.C1List.C1Combo();
            this.cmbChecker = new C1.Win.C1List.C1Combo();
            this.cmbIsSend = new C1.Win.C1List.C1Combo();
            this.cmbProduceCompany = new C1.Win.C1List.C1Combo();
            this.cmbCheckMachine = new C1.Win.C1List.C1Combo();
            this.cmbResult = new C1.Win.C1List.C1Combo();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCheckMethod = new C1.Win.C1List.C1Combo();
            this.cmbCheckItem = new C1.Win.C1List.C1Combo();
            this.cmbAssessor = new C1.Win.C1List.C1Combo();
            this.lblDomain = new System.Windows.Forms.Label();
            this.cmbDisplayName = new C1.Win.C1List.C1Combo();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbCheckType = new C1.Win.C1List.C1Combo();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSampleModel = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbOrganizer = new C1.Win.C1List.C1Combo();
            this.txtSentCompany = new System.Windows.Forms.TextBox();
            this.cmbUpperCompany = new C1.Win.C1List.C1Combo();
            this.lblParent = new System.Windows.Forms.Label();
            this.cmbProducePlace = new C1.Win.C1List.C1Combo();
            this.label29 = new System.Windows.Forms.Label();
            this.txtCheckPlanCode = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbCheckerVal = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.cmbIsSentCheck = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.dtpSendedDate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbIsReSend = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.txtSampleCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSampleState = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStdCode = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtOrCheckNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpProduceDate = new System.Windows.Forms.DateTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.dtpTakeDate = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.txtCheckValueInfo = new System.Windows.Forms.TextBox();
            this.lblSuppresser = new System.Windows.Forms.Label();
            this.txtProduceDate = new System.Windows.Forms.TextBox();
            this.txtTakeDate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDisplayName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganizer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUpperCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducePlace)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(335, 425);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(72, 24);
            this.btnQuery.TabIndex = 28;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(255, 382);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 21);
            this.label17.TabIndex = 55;
            this.label17.Text = "检测仪器：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(261, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 21);
            this.label14.TabIndex = 53;
            this.label14.Text = "生产单位：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(24, 464);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 21);
            this.label13.TabIndex = 42;
            this.label13.Text = "是否发送：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Visible = false;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(261, 252);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 21);
            this.label12.TabIndex = 50;
            this.label12.Text = "检测人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(24, 383);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 21);
            this.label11.TabIndex = 41;
            this.label11.Text = "检测单位：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(236, 201);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(98, 21);
            this.lblName.TabIndex = 31;
            this.lblName.Text = "受检人/单位：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSample
            // 
            this.lblSample.Location = new System.Drawing.Point(21, 53);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(73, 21);
            this.lblSample.TabIndex = 45;
            this.lblSample.Text = "商品名称：";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpCheckEndDate
            // 
            this.dtpCheckEndDate.CustomFormat = "yyyy年MM月dd日";
            this.dtpCheckEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckEndDate.Location = new System.Drawing.Point(337, 2);
            this.dtpCheckEndDate.Name = "dtpCheckEndDate";
            this.dtpCheckEndDate.Size = new System.Drawing.Size(142, 21);
            this.dtpCheckEndDate.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(264, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 21);
            this.label5.TabIndex = 40;
            this.label5.Text = "结束日期：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpCheckStartDate
            // 
            this.dtpCheckStartDate.CustomFormat = "yyyy年MM月dd日";
            this.dtpCheckStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckStartDate.Location = new System.Drawing.Point(97, 2);
            this.dtpCheckStartDate.Name = "dtpCheckStartDate";
            this.dtpCheckStartDate.Size = new System.Drawing.Size(142, 21);
            this.dtpCheckStartDate.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.TabIndex = 39;
            this.label4.Text = "开始日期：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(417, 425);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.cmbFood.Location = new System.Drawing.Point(97, 50);
            this.cmbFood.MatchEntryTimeout = ((long)(2000));
            this.cmbFood.MaxDropDownItems = ((short)(5));
            this.cmbFood.MaxLength = 32767;
            this.cmbFood.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbFood.Name = "cmbFood";
            this.cmbFood.OddRowStyle = style6;
            this.cmbFood.PartialRightColumn = false;
            this.cmbFood.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbFood.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbFood.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbFood.SelectedStyle = style7;
            this.cmbFood.Size = new System.Drawing.Size(142, 22);
            this.cmbFood.Style = style8;
            this.cmbFood.TabIndex = 3;
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
            this.cmbCheckedCompany.Location = new System.Drawing.Point(337, 200);
            this.cmbCheckedCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckedCompany.MaxDropDownItems = ((short)(5));
            this.cmbCheckedCompany.MaxLength = 32767;
            this.cmbCheckedCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckedCompany.Name = "cmbCheckedCompany";
            this.cmbCheckedCompany.OddRowStyle = style14;
            this.cmbCheckedCompany.PartialRightColumn = false;
            this.cmbCheckedCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckedCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckedCompany.SelectedStyle = style15;
            this.cmbCheckedCompany.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckedCompany.Style = style16;
            this.cmbCheckedCompany.TabIndex = 2;
            this.cmbCheckedCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbCheckedCompany_BeforeOpen);
            this.cmbCheckedCompany.LostFocus += new System.EventHandler(this.cmbCheckedCompany_LostFocus);
            this.cmbCheckedCompany.PropBag = resources.GetString("cmbCheckedCompany.PropBag");
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
            this.cmbCheckUnit.Location = new System.Drawing.Point(98, 382);
            this.cmbCheckUnit.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckUnit.MaxDropDownItems = ((short)(5));
            this.cmbCheckUnit.MaxLength = 32767;
            this.cmbCheckUnit.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckUnit.Name = "cmbCheckUnit";
            this.cmbCheckUnit.OddRowStyle = style22;
            this.cmbCheckUnit.PartialRightColumn = false;
            this.cmbCheckUnit.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckUnit.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckUnit.SelectedStyle = style23;
            this.cmbCheckUnit.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckUnit.Style = style24;
            this.cmbCheckUnit.TabIndex = 22;
            this.cmbCheckUnit.PropBag = resources.GetString("cmbCheckUnit.PropBag");
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
            this.cmbChecker.Location = new System.Drawing.Point(337, 251);
            this.cmbChecker.MatchEntryTimeout = ((long)(2000));
            this.cmbChecker.MaxDropDownItems = ((short)(5));
            this.cmbChecker.MaxLength = 32767;
            this.cmbChecker.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbChecker.Name = "cmbChecker";
            this.cmbChecker.OddRowStyle = style30;
            this.cmbChecker.PartialRightColumn = false;
            this.cmbChecker.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbChecker.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbChecker.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbChecker.SelectedStyle = style31;
            this.cmbChecker.Size = new System.Drawing.Size(142, 22);
            this.cmbChecker.Style = style32;
            this.cmbChecker.TabIndex = 13;
            this.cmbChecker.PropBag = resources.GetString("cmbChecker.PropBag");
            // 
            // cmbIsSend
            // 
            this.cmbIsSend.AddItemSeparator = ';';
            this.cmbIsSend.Caption = "";
            this.cmbIsSend.CaptionHeight = 17;
            this.cmbIsSend.CaptionStyle = style33;
            this.cmbIsSend.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbIsSend.ColumnCaptionHeight = 18;
            this.cmbIsSend.ColumnFooterHeight = 18;
            this.cmbIsSend.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbIsSend.ContentHeight = 16;
            this.cmbIsSend.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbIsSend.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbIsSend.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbIsSend.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbIsSend.EditorHeight = 16;
            this.cmbIsSend.EvenRowStyle = style34;
            this.cmbIsSend.FooterStyle = style35;
            this.cmbIsSend.GapHeight = 2;
            this.cmbIsSend.HeadingStyle = style36;
            this.cmbIsSend.HighLightRowStyle = style37;
            this.cmbIsSend.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbIsSend.Images"))));
            this.cmbIsSend.ItemHeight = 15;
            this.cmbIsSend.Location = new System.Drawing.Point(97, 463);
            this.cmbIsSend.MatchEntryTimeout = ((long)(2000));
            this.cmbIsSend.MaxDropDownItems = ((short)(5));
            this.cmbIsSend.MaxLength = 32767;
            this.cmbIsSend.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbIsSend.Name = "cmbIsSend";
            this.cmbIsSend.OddRowStyle = style38;
            this.cmbIsSend.PartialRightColumn = false;
            this.cmbIsSend.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbIsSend.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbIsSend.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbIsSend.SelectedStyle = style39;
            this.cmbIsSend.Size = new System.Drawing.Size(142, 22);
            this.cmbIsSend.Style = style40;
            this.cmbIsSend.TabIndex = 24;
            this.cmbIsSend.Visible = false;
            this.cmbIsSend.PropBag = resources.GetString("cmbIsSend.PropBag");
            // 
            // cmbProduceCompany
            // 
            this.cmbProduceCompany.AddItemSeparator = ';';
            this.cmbProduceCompany.Caption = "";
            this.cmbProduceCompany.CaptionHeight = 17;
            this.cmbProduceCompany.CaptionStyle = style41;
            this.cmbProduceCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbProduceCompany.ColumnCaptionHeight = 18;
            this.cmbProduceCompany.ColumnFooterHeight = 18;
            this.cmbProduceCompany.ContentHeight = 16;
            this.cmbProduceCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbProduceCompany.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbProduceCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProduceCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbProduceCompany.EditorHeight = 16;
            this.cmbProduceCompany.EvenRowStyle = style42;
            this.cmbProduceCompany.FooterStyle = style43;
            this.cmbProduceCompany.GapHeight = 2;
            this.cmbProduceCompany.HeadingStyle = style44;
            this.cmbProduceCompany.HighLightRowStyle = style45;
            this.cmbProduceCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProduceCompany.Images"))));
            this.cmbProduceCompany.ItemHeight = 15;
            this.cmbProduceCompany.Location = new System.Drawing.Point(337, 124);
            this.cmbProduceCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbProduceCompany.MaxDropDownItems = ((short)(5));
            this.cmbProduceCompany.MaxLength = 32767;
            this.cmbProduceCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProduceCompany.Name = "cmbProduceCompany";
            this.cmbProduceCompany.OddRowStyle = style46;
            this.cmbProduceCompany.PartialRightColumn = false;
            this.cmbProduceCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProduceCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProduceCompany.SelectedStyle = style47;
            this.cmbProduceCompany.Size = new System.Drawing.Size(142, 22);
            this.cmbProduceCompany.Style = style48;
            this.cmbProduceCompany.TabIndex = 19;
            this.cmbProduceCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProduceCompany_BeforeOpen);
            this.cmbProduceCompany.PropBag = resources.GetString("cmbProduceCompany.PropBag");
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
            this.cmbCheckMachine.Location = new System.Drawing.Point(337, 382);
            this.cmbCheckMachine.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckMachine.MaxDropDownItems = ((short)(5));
            this.cmbCheckMachine.MaxLength = 32767;
            this.cmbCheckMachine.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckMachine.Name = "cmbCheckMachine";
            this.cmbCheckMachine.OddRowStyle = style54;
            this.cmbCheckMachine.PartialRightColumn = false;
            this.cmbCheckMachine.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckMachine.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckMachine.SelectedStyle = style55;
            this.cmbCheckMachine.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckMachine.Style = style56;
            this.cmbCheckMachine.TabIndex = 23;
            this.cmbCheckMachine.PropBag = resources.GetString("cmbCheckMachine.PropBag");
            // 
            // cmbResult
            // 
            this.cmbResult.AddItemSeparator = ';';
            this.cmbResult.Caption = "";
            this.cmbResult.CaptionHeight = 17;
            this.cmbResult.CaptionStyle = style57;
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
            this.cmbResult.EvenRowStyle = style58;
            this.cmbResult.FooterStyle = style59;
            this.cmbResult.GapHeight = 2;
            this.cmbResult.HeadingStyle = style60;
            this.cmbResult.HighLightRowStyle = style61;
            this.cmbResult.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbResult.Images"))));
            this.cmbResult.ItemHeight = 15;
            this.cmbResult.Location = new System.Drawing.Point(97, 354);
            this.cmbResult.MatchEntryTimeout = ((long)(2000));
            this.cmbResult.MaxDropDownItems = ((short)(5));
            this.cmbResult.MaxLength = 32767;
            this.cmbResult.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.OddRowStyle = style62;
            this.cmbResult.PartialRightColumn = false;
            this.cmbResult.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbResult.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbResult.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbResult.SelectedStyle = style63;
            this.cmbResult.Size = new System.Drawing.Size(142, 22);
            this.cmbResult.Style = style64;
            this.cmbResult.TabIndex = 14;
            this.cmbResult.PropBag = resources.GetString("cmbResult.PropBag");
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(22, 355);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 21);
            this.label10.TabIndex = 37;
            this.label10.Text = "结论：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Location = new System.Drawing.Point(97, 27);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(142, 21);
            this.txtCheckNo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.TabIndex = 30;
            this.label1.Text = "检测编号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(261, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 52;
            this.label2.Text = "核准人：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 49;
            this.label3.Text = "检测项目：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(259, 355);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 21);
            this.label7.TabIndex = 54;
            this.label7.Text = "检测手段：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckMethod
            // 
            this.cmbCheckMethod.AddItemSeparator = ';';
            this.cmbCheckMethod.Caption = "";
            this.cmbCheckMethod.CaptionHeight = 17;
            this.cmbCheckMethod.CaptionStyle = style65;
            this.cmbCheckMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckMethod.ColumnCaptionHeight = 18;
            this.cmbCheckMethod.ColumnFooterHeight = 18;
            this.cmbCheckMethod.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckMethod.ContentHeight = 16;
            this.cmbCheckMethod.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckMethod.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckMethod.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckMethod.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckMethod.EditorHeight = 16;
            this.cmbCheckMethod.EvenRowStyle = style66;
            this.cmbCheckMethod.FooterStyle = style67;
            this.cmbCheckMethod.GapHeight = 2;
            this.cmbCheckMethod.HeadingStyle = style68;
            this.cmbCheckMethod.HighLightRowStyle = style69;
            this.cmbCheckMethod.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckMethod.Images"))));
            this.cmbCheckMethod.ItemHeight = 15;
            this.cmbCheckMethod.Location = new System.Drawing.Point(337, 355);
            this.cmbCheckMethod.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckMethod.MaxDropDownItems = ((short)(5));
            this.cmbCheckMethod.MaxLength = 32767;
            this.cmbCheckMethod.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckMethod.Name = "cmbCheckMethod";
            this.cmbCheckMethod.OddRowStyle = style70;
            this.cmbCheckMethod.PartialRightColumn = false;
            this.cmbCheckMethod.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckMethod.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckMethod.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckMethod.SelectedStyle = style71;
            this.cmbCheckMethod.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckMethod.Style = style72;
            this.cmbCheckMethod.TabIndex = 21;
            this.cmbCheckMethod.PropBag = resources.GetString("cmbCheckMethod.PropBag");
            // 
            // cmbCheckItem
            // 
            this.cmbCheckItem.AddItemSeparator = ';';
            this.cmbCheckItem.Caption = "";
            this.cmbCheckItem.CaptionHeight = 17;
            this.cmbCheckItem.CaptionStyle = style73;
            this.cmbCheckItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckItem.ColumnCaptionHeight = 18;
            this.cmbCheckItem.ColumnFooterHeight = 18;
            this.cmbCheckItem.ContentHeight = 16;
            this.cmbCheckItem.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckItem.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckItem.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItem.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckItem.EditorHeight = 16;
            this.cmbCheckItem.EvenRowStyle = style74;
            this.cmbCheckItem.FooterStyle = style75;
            this.cmbCheckItem.GapHeight = 2;
            this.cmbCheckItem.HeadingStyle = style76;
            this.cmbCheckItem.HighLightRowStyle = style77;
            this.cmbCheckItem.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckItem.Images"))));
            this.cmbCheckItem.ItemHeight = 15;
            this.cmbCheckItem.Location = new System.Drawing.Point(97, 250);
            this.cmbCheckItem.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckItem.MaxDropDownItems = ((short)(5));
            this.cmbCheckItem.MaxLength = 32767;
            this.cmbCheckItem.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckItem.Name = "cmbCheckItem";
            this.cmbCheckItem.OddRowStyle = style78;
            this.cmbCheckItem.PartialRightColumn = false;
            this.cmbCheckItem.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckItem.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.SelectedStyle = style79;
            this.cmbCheckItem.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckItem.Style = style80;
            this.cmbCheckItem.TabIndex = 11;
            this.cmbCheckItem.PropBag = resources.GetString("cmbCheckItem.PropBag");
            // 
            // cmbAssessor
            // 
            this.cmbAssessor.AddItemSeparator = ';';
            this.cmbAssessor.Caption = "";
            this.cmbAssessor.CaptionHeight = 17;
            this.cmbAssessor.CaptionStyle = style81;
            this.cmbAssessor.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbAssessor.ColumnCaptionHeight = 18;
            this.cmbAssessor.ColumnFooterHeight = 18;
            this.cmbAssessor.ContentHeight = 16;
            this.cmbAssessor.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbAssessor.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbAssessor.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAssessor.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbAssessor.EditorHeight = 16;
            this.cmbAssessor.EvenRowStyle = style82;
            this.cmbAssessor.FooterStyle = style83;
            this.cmbAssessor.GapHeight = 2;
            this.cmbAssessor.HeadingStyle = style84;
            this.cmbAssessor.HighLightRowStyle = style85;
            this.cmbAssessor.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbAssessor.Images"))));
            this.cmbAssessor.ItemHeight = 15;
            this.cmbAssessor.Location = new System.Drawing.Point(337, 278);
            this.cmbAssessor.MatchEntryTimeout = ((long)(2000));
            this.cmbAssessor.MaxDropDownItems = ((short)(5));
            this.cmbAssessor.MaxLength = 32767;
            this.cmbAssessor.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbAssessor.Name = "cmbAssessor";
            this.cmbAssessor.OddRowStyle = style86;
            this.cmbAssessor.PartialRightColumn = false;
            this.cmbAssessor.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbAssessor.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbAssessor.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbAssessor.SelectedStyle = style87;
            this.cmbAssessor.Size = new System.Drawing.Size(142, 22);
            this.cmbAssessor.Style = style88;
            this.cmbAssessor.TabIndex = 17;
            this.cmbAssessor.PropBag = resources.GetString("cmbAssessor.PropBag");
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(264, 221);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(70, 32);
            this.lblDomain.TabIndex = 32;
            this.lblDomain.Text = "档口/店面／车牌号：";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDisplayName
            // 
            this.cmbDisplayName.AddItemSeparator = ';';
            this.cmbDisplayName.Caption = "";
            this.cmbDisplayName.CaptionHeight = 17;
            this.cmbDisplayName.CaptionStyle = style89;
            this.cmbDisplayName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbDisplayName.ColumnCaptionHeight = 18;
            this.cmbDisplayName.ColumnFooterHeight = 18;
            this.cmbDisplayName.ContentHeight = 16;
            this.cmbDisplayName.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbDisplayName.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbDisplayName.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDisplayName.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbDisplayName.EditorHeight = 16;
            this.cmbDisplayName.EvenRowStyle = style90;
            this.cmbDisplayName.FooterStyle = style91;
            this.cmbDisplayName.GapHeight = 2;
            this.cmbDisplayName.HeadingStyle = style92;
            this.cmbDisplayName.HighLightRowStyle = style93;
            this.cmbDisplayName.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbDisplayName.Images"))));
            this.cmbDisplayName.ItemHeight = 15;
            this.cmbDisplayName.Location = new System.Drawing.Point(337, 226);
            this.cmbDisplayName.MatchEntryTimeout = ((long)(2000));
            this.cmbDisplayName.MaxDropDownItems = ((short)(5));
            this.cmbDisplayName.MaxLength = 32767;
            this.cmbDisplayName.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbDisplayName.Name = "cmbDisplayName";
            this.cmbDisplayName.OddRowStyle = style94;
            this.cmbDisplayName.PartialRightColumn = false;
            this.cmbDisplayName.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbDisplayName.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbDisplayName.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbDisplayName.SelectedStyle = style95;
            this.cmbDisplayName.Size = new System.Drawing.Size(142, 22);
            this.cmbDisplayName.Style = style96;
            this.cmbDisplayName.TabIndex = 4;
            this.cmbDisplayName.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbDisplayName_BeforeOpen);
            this.cmbDisplayName.PropBag = resources.GetString("cmbDisplayName.PropBag");
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(-4, 174);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 21);
            this.label15.TabIndex = 34;
            this.label15.Text = "检测类型：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckType
            // 
            this.cmbCheckType.AddItemSeparator = ';';
            this.cmbCheckType.Caption = "";
            this.cmbCheckType.CaptionHeight = 17;
            this.cmbCheckType.CaptionStyle = style97;
            this.cmbCheckType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckType.ColumnCaptionHeight = 18;
            this.cmbCheckType.ColumnFooterHeight = 18;
            this.cmbCheckType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckType.ContentHeight = 16;
            this.cmbCheckType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckType.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckType.EditorHeight = 16;
            this.cmbCheckType.EvenRowStyle = style98;
            this.cmbCheckType.FooterStyle = style99;
            this.cmbCheckType.GapHeight = 2;
            this.cmbCheckType.HeadingStyle = style100;
            this.cmbCheckType.HighLightRowStyle = style101;
            this.cmbCheckType.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckType.Images"))));
            this.cmbCheckType.ItemHeight = 15;
            this.cmbCheckType.Location = new System.Drawing.Point(97, 173);
            this.cmbCheckType.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckType.MaxDropDownItems = ((short)(5));
            this.cmbCheckType.MaxLength = 32767;
            this.cmbCheckType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckType.Name = "cmbCheckType";
            this.cmbCheckType.OddRowStyle = style102;
            this.cmbCheckType.PartialRightColumn = false;
            this.cmbCheckType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckType.SelectedStyle = style103;
            this.cmbCheckType.Size = new System.Drawing.Size(142, 22);
            this.cmbCheckType.Style = style104;
            this.cmbCheckType.TabIndex = 8;
            this.cmbCheckType.PropBag = resources.GetString("cmbCheckType.PropBag");
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(261, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 21);
            this.label16.TabIndex = 47;
            this.label16.Text = "规格型号：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleModel
            // 
            this.txtSampleModel.Location = new System.Drawing.Point(337, 101);
            this.txtSampleModel.Name = "txtSampleModel";
            this.txtSampleModel.Size = new System.Drawing.Size(142, 21);
            this.txtSampleModel.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(8, 102);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 21);
            this.label18.TabIndex = 35;
            this.label18.Text = "供货商/商标：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(97, 100);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(142, 21);
            this.txtProvider.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(261, 175);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 21);
            this.label19.TabIndex = 48;
            this.label19.Text = "送检单位：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(22, 303);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 21);
            this.label20.TabIndex = 36;
            this.label20.Text = "抽样人：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbOrganizer
            // 
            this.cmbOrganizer.AddItemSeparator = ';';
            this.cmbOrganizer.Caption = "";
            this.cmbOrganizer.CaptionHeight = 17;
            this.cmbOrganizer.CaptionStyle = style105;
            this.cmbOrganizer.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbOrganizer.ColumnCaptionHeight = 18;
            this.cmbOrganizer.ColumnFooterHeight = 18;
            this.cmbOrganizer.ContentHeight = 16;
            this.cmbOrganizer.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbOrganizer.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbOrganizer.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOrganizer.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOrganizer.EditorHeight = 16;
            this.cmbOrganizer.EvenRowStyle = style106;
            this.cmbOrganizer.FooterStyle = style107;
            this.cmbOrganizer.GapHeight = 2;
            this.cmbOrganizer.HeadingStyle = style108;
            this.cmbOrganizer.HighLightRowStyle = style109;
            this.cmbOrganizer.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbOrganizer.Images"))));
            this.cmbOrganizer.ItemHeight = 15;
            this.cmbOrganizer.Location = new System.Drawing.Point(97, 302);
            this.cmbOrganizer.MatchEntryTimeout = ((long)(2000));
            this.cmbOrganizer.MaxDropDownItems = ((short)(5));
            this.cmbOrganizer.MaxLength = 32767;
            this.cmbOrganizer.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbOrganizer.Name = "cmbOrganizer";
            this.cmbOrganizer.OddRowStyle = style110;
            this.cmbOrganizer.PartialRightColumn = false;
            this.cmbOrganizer.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbOrganizer.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbOrganizer.SelectedStyle = style111;
            this.cmbOrganizer.Size = new System.Drawing.Size(144, 22);
            this.cmbOrganizer.Style = style112;
            this.cmbOrganizer.TabIndex = 12;
            this.cmbOrganizer.PropBag = resources.GetString("cmbOrganizer.PropBag");
            // 
            // txtSentCompany
            // 
            this.txtSentCompany.Location = new System.Drawing.Point(337, 175);
            this.txtSentCompany.Name = "txtSentCompany";
            this.txtSentCompany.Size = new System.Drawing.Size(142, 21);
            this.txtSentCompany.TabIndex = 9;
            // 
            // cmbUpperCompany
            // 
            this.cmbUpperCompany.AddItemSeparator = ';';
            this.cmbUpperCompany.Caption = "";
            this.cmbUpperCompany.CaptionHeight = 17;
            this.cmbUpperCompany.CaptionStyle = style113;
            this.cmbUpperCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbUpperCompany.ColumnCaptionHeight = 18;
            this.cmbUpperCompany.ColumnFooterHeight = 18;
            this.cmbUpperCompany.ContentHeight = 16;
            this.cmbUpperCompany.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbUpperCompany.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbUpperCompany.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUpperCompany.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbUpperCompany.EditorHeight = 16;
            this.cmbUpperCompany.EvenRowStyle = style114;
            this.cmbUpperCompany.FooterStyle = style115;
            this.cmbUpperCompany.GapHeight = 2;
            this.cmbUpperCompany.HeadingStyle = style116;
            this.cmbUpperCompany.HighLightRowStyle = style117;
            this.cmbUpperCompany.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbUpperCompany.Images"))));
            this.cmbUpperCompany.ItemHeight = 15;
            this.cmbUpperCompany.Location = new System.Drawing.Point(97, 223);
            this.cmbUpperCompany.MatchEntryTimeout = ((long)(2000));
            this.cmbUpperCompany.MaxDropDownItems = ((short)(5));
            this.cmbUpperCompany.MaxLength = 10;
            this.cmbUpperCompany.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbUpperCompany.Name = "cmbUpperCompany";
            this.cmbUpperCompany.OddRowStyle = style118;
            this.cmbUpperCompany.PartialRightColumn = false;
            this.cmbUpperCompany.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbUpperCompany.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbUpperCompany.SelectedStyle = style119;
            this.cmbUpperCompany.Size = new System.Drawing.Size(142, 22);
            this.cmbUpperCompany.Style = style120;
            this.cmbUpperCompany.TabIndex = 5;
            this.cmbUpperCompany.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbUpperCompany_BeforeOpen);
            this.cmbUpperCompany.PropBag = resources.GetString("cmbUpperCompany.PropBag");
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(27, 226);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(67, 17);
            this.lblParent.TabIndex = 46;
            this.lblParent.Text = "所属市场：";
            this.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProducePlace
            // 
            this.cmbProducePlace.AddItemSeparator = ';';
            this.cmbProducePlace.Caption = "";
            this.cmbProducePlace.CaptionHeight = 17;
            this.cmbProducePlace.CaptionStyle = style121;
            this.cmbProducePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbProducePlace.ColumnCaptionHeight = 18;
            this.cmbProducePlace.ColumnFooterHeight = 18;
            this.cmbProducePlace.ContentHeight = 16;
            this.cmbProducePlace.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbProducePlace.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbProducePlace.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProducePlace.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbProducePlace.EditorHeight = 16;
            this.cmbProducePlace.EvenRowStyle = style122;
            this.cmbProducePlace.FooterStyle = style123;
            this.cmbProducePlace.GapHeight = 2;
            this.cmbProducePlace.HeadingStyle = style124;
            this.cmbProducePlace.HighLightRowStyle = style125;
            this.cmbProducePlace.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbProducePlace.Images"))));
            this.cmbProducePlace.ItemHeight = 15;
            this.cmbProducePlace.Location = new System.Drawing.Point(97, 146);
            this.cmbProducePlace.MatchEntryTimeout = ((long)(2000));
            this.cmbProducePlace.MaxDropDownItems = ((short)(5));
            this.cmbProducePlace.MaxLength = 10;
            this.cmbProducePlace.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbProducePlace.Name = "cmbProducePlace";
            this.cmbProducePlace.OddRowStyle = style126;
            this.cmbProducePlace.PartialRightColumn = false;
            this.cmbProducePlace.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbProducePlace.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbProducePlace.SelectedStyle = style127;
            this.cmbProducePlace.Size = new System.Drawing.Size(142, 22);
            this.cmbProducePlace.Style = style128;
            this.cmbProducePlace.TabIndex = 6;
            this.cmbProducePlace.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbProducePlace_BeforeOpen);
            this.cmbProducePlace.PropBag = resources.GetString("cmbProducePlace.PropBag");
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(30, 149);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(64, 17);
            this.label29.TabIndex = 33;
            this.label29.Text = "产地：";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckPlanCode
            // 
            this.txtCheckPlanCode.Location = new System.Drawing.Point(337, 53);
            this.txtCheckPlanCode.MaxLength = 50;
            this.txtCheckPlanCode.Name = "txtCheckPlanCode";
            this.txtCheckPlanCode.Size = new System.Drawing.Size(142, 21);
            this.txtCheckPlanCode.TabIndex = 1;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(237, 54);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(97, 21);
            this.label34.TabIndex = 44;
            this.label34.Text = "检测计划编号：";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckerVal
            // 
            this.cmbCheckerVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckerVal.Items.AddRange(new object[] {
            "",
            "无异议",
            "有异议"});
            this.cmbCheckerVal.Location = new System.Drawing.Point(337, 329);
            this.cmbCheckerVal.Name = "cmbCheckerVal";
            this.cmbCheckerVal.Size = new System.Drawing.Size(142, 20);
            this.cmbCheckerVal.TabIndex = 16;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(246, 329);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(86, 21);
            this.label48.TabIndex = 38;
            this.label48.Text = "被检人确认：";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIsSentCheck
            // 
            this.cmbIsSentCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsSentCheck.Items.AddRange(new object[] {
            "",
            "否",
            "是"});
            this.cmbIsSentCheck.Location = new System.Drawing.Point(97, 329);
            this.cmbIsSentCheck.Name = "cmbIsSentCheck";
            this.cmbIsSentCheck.Size = new System.Drawing.Size(142, 20);
            this.cmbIsSentCheck.TabIndex = 15;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(25, 328);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 21);
            this.label38.TabIndex = 51;
            this.label38.Text = "是否送检：";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSendedDate
            // 
            this.dtpSendedDate.Checked = false;
            this.dtpSendedDate.Location = new System.Drawing.Point(337, 483);
            this.dtpSendedDate.Name = "dtpSendedDate";
            this.dtpSendedDate.ShowCheckBox = true;
            this.dtpSendedDate.Size = new System.Drawing.Size(142, 21);
            this.dtpSendedDate.TabIndex = 27;
            this.dtpSendedDate.Visible = false;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(265, 483);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 21);
            this.label21.TabIndex = 57;
            this.label21.Text = "上传时间：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Visible = false;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(263, 458);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(69, 21);
            this.label33.TabIndex = 56;
            this.label33.Text = "上传人：";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label33.Visible = false;
            // 
            // cmbIsReSend
            // 
            this.cmbIsReSend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsReSend.Items.AddRange(new object[] {
            "",
            "否",
            "是"});
            this.cmbIsReSend.Location = new System.Drawing.Point(97, 491);
            this.cmbIsReSend.Name = "cmbIsReSend";
            this.cmbIsReSend.Size = new System.Drawing.Size(142, 20);
            this.cmbIsReSend.TabIndex = 26;
            this.cmbIsReSend.Visible = false;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(22, 491);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(72, 21);
            this.label22.TabIndex = 43;
            this.label22.Text = "重传标志：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label22.Visible = false;
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(335, 458);
            this.txtSender.MaxLength = 50;
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(142, 21);
            this.txtSender.TabIndex = 25;
            this.txtSender.Visible = false;
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.Location = new System.Drawing.Point(337, 28);
            this.txtSampleCode.MaxLength = 50;
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.Size = new System.Drawing.Size(142, 21);
            this.txtSampleCode.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(265, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 21);
            this.label6.TabIndex = 65;
            this.label6.Text = "样品编号：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleState
            // 
            this.txtSampleState.Location = new System.Drawing.Point(97, 75);
            this.txtSampleState.MaxLength = 50;
            this.txtSampleState.Name = "txtSampleState";
            this.txtSampleState.Size = new System.Drawing.Size(142, 21);
            this.txtSampleState.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 21);
            this.label8.TabIndex = 67;
            this.label8.Text = "批号或编号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStdCode
            // 
            this.txtStdCode.Location = new System.Drawing.Point(337, 77);
            this.txtStdCode.Name = "txtStdCode";
            this.txtStdCode.Size = new System.Drawing.Size(142, 21);
            this.txtStdCode.TabIndex = 68;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(268, 76);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 21);
            this.label24.TabIndex = 69;
            this.label24.Text = "条形码：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrCheckNo
            // 
            this.txtOrCheckNo.Location = new System.Drawing.Point(97, 199);
            this.txtOrCheckNo.MaxLength = 50;
            this.txtOrCheckNo.Name = "txtOrCheckNo";
            this.txtOrCheckNo.Size = new System.Drawing.Size(142, 21);
            this.txtOrCheckNo.TabIndex = 70;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(14, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 21);
            this.label9.TabIndex = 71;
            this.label9.Text = "原检测编号：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpProduceDate
            // 
            this.dtpProduceDate.Location = new System.Drawing.Point(214, 122);
            this.dtpProduceDate.Name = "dtpProduceDate";
            this.dtpProduceDate.Size = new System.Drawing.Size(25, 21);
            this.dtpProduceDate.TabIndex = 72;
            this.dtpProduceDate.Value = new System.DateTime(2011, 7, 13, 0, 0, 0, 0);
            this.dtpProduceDate.ValueChanged += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            this.dtpProduceDate.DropDown += new System.EventHandler(this.dtpProduceDate_ValueChanged);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(28, 122);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 21);
            this.label23.TabIndex = 73;
            this.label23.Text = "生产日期：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTakeDate
            // 
            this.dtpTakeDate.Location = new System.Drawing.Point(456, 304);
            this.dtpTakeDate.Name = "dtpTakeDate";
            this.dtpTakeDate.Size = new System.Drawing.Size(23, 21);
            this.dtpTakeDate.TabIndex = 75;
            this.dtpTakeDate.Value = new System.DateTime(2011, 7, 13, 0, 0, 0, 0);
            this.dtpTakeDate.ValueChanged += new System.EventHandler(this.dtpTakeDate_ValueChanged);
            this.dtpTakeDate.DropDown += new System.EventHandler(this.dtpTakeDate_ValueChanged);
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(265, 304);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 21);
            this.label25.TabIndex = 76;
            this.label25.Text = "抽样日期：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckValueInfo
            // 
            this.txtCheckValueInfo.Location = new System.Drawing.Point(98, 277);
            this.txtCheckValueInfo.Name = "txtCheckValueInfo";
            this.txtCheckValueInfo.Size = new System.Drawing.Size(142, 21);
            this.txtCheckValueInfo.TabIndex = 80;
            // 
            // lblSuppresser
            // 
            this.lblSuppresser.Location = new System.Drawing.Point(27, 278);
            this.lblSuppresser.Name = "lblSuppresser";
            this.lblSuppresser.Size = new System.Drawing.Size(66, 21);
            this.lblSuppresser.TabIndex = 81;
            this.lblSuppresser.Text = "检测值：";
            this.lblSuppresser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduceDate
            // 
            this.txtProduceDate.Location = new System.Drawing.Point(97, 122);
            this.txtProduceDate.Name = "txtProduceDate";
            this.txtProduceDate.Size = new System.Drawing.Size(124, 21);
            this.txtProduceDate.TabIndex = 110;
            // 
            // txtTakeDate
            // 
            this.txtTakeDate.Location = new System.Drawing.Point(337, 304);
            this.txtTakeDate.Name = "txtTakeDate";
            this.txtTakeDate.Size = new System.Drawing.Size(124, 21);
            this.txtTakeDate.TabIndex = 111;
            // 
            // frmResultQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(507, 505);
            this.Controls.Add(this.txtTakeDate);
            this.Controls.Add(this.txtProduceDate);
            this.Controls.Add(this.txtCheckValueInfo);
            this.Controls.Add(this.lblSuppresser);
            this.Controls.Add(this.dtpTakeDate);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.dtpProduceDate);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtOrCheckNo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtStdCode);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtSampleState);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSampleCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSender);
            this.Controls.Add(this.dtpSendedDate);
            this.Controls.Add(this.txtCheckPlanCode);
            this.Controls.Add(this.txtSentCompany);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.txtSampleModel);
            this.Controls.Add(this.txtCheckNo);
            this.Controls.Add(this.cmbIsReSend);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cmbCheckerVal);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.cmbIsSentCheck);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.cmbUpperCompany);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.cmbProducePlace);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cmbOrganizer);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbCheckType);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.cmbDisplayName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbCheckMethod);
            this.Controls.Add(this.cmbCheckItem);
            this.Controls.Add(this.cmbAssessor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFood);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.dtpCheckEndDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpCheckStartDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbCheckedCompany);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.cmbCheckUnit);
            this.Controls.Add(this.cmbChecker);
            this.Controls.Add(this.cmbIsSend);
            this.Controls.Add(this.cmbProduceCompany);
            this.Controls.Add(this.cmbCheckMachine);
            this.Controls.Add(this.label34);
            this.MaximizeBox = false;
            this.Name = "frmResultQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测数据查询";
            this.Load += new System.EventHandler(this.frmResultQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckedCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChecker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduceCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDisplayName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganizer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUpperCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducePlace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能,陈国利修改(2011-06-14)
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckStartDate>=#");
            sb.Append(dtpCheckStartDate.Value.ToString("yyyy-MM-dd"));
            sb.Append("#");
            sb.Append(" AND CheckStartDate<=#");
            sb.Append(dtpCheckEndDate.Value.Year.ToString());
            sb.Append("-");
            sb.Append(dtpCheckEndDate.Value.Month.ToString());
            sb.Append("-");
            sb.Append(dtpCheckEndDate.Value.Day.ToString());
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(txtCheckNo.Text.Trim()))
                sb.AppendFormat(" AND CheckNo LIKE '%{0}%'", txtCheckNo.Text);
            if (!string.IsNullOrEmpty(txtSampleCode.Text.Trim()))
                sb.AppendFormat(" AND SampleCode LIKE '%{0}%'", txtSampleCode.Text);
            if (!foodSelectedValue.Equals(""))
                sb.AppendFormat(" AND FoodCode='{0}'", foodSelectedValue);
            else if (!string.IsNullOrEmpty(cmbFood.Text.Trim()))
                sb.AppendFormat(" AND FoodName LIKE '%{0}%'", cmbFood.Text);
            if (!string.IsNullOrEmpty(txtCheckPlanCode.Text.Trim()))
                sb.AppendFormat(" AND CheckPlanCode LIKE '%{0}%'", txtCheckPlanCode.Text);
            if (!string.IsNullOrEmpty(txtSampleState.Text.Trim()))
                sb.AppendFormat(" AND SampleState LIKE '%{0}%'", txtSampleState.Text);
            if (!string.IsNullOrEmpty(txtStdCode.Text.Trim()))
                sb.AppendFormat(" AND StdCode LIKE '%{0}%'", txtStdCode.Text);
            if (!string.IsNullOrEmpty(txtProvider.Text.Trim()))
                sb.AppendFormat(" AND Provider LIKE '%{0}%'", txtProvider.Text);
            if (!string.IsNullOrEmpty(txtSampleModel.Text.Trim()))//规格型号
                sb.AppendFormat(" AND SampleModel LIKE '%{0}%'", txtSampleModel.Text.Trim());
            string produceDate=txtProduceDate.Text;
            if (!string.IsNullOrEmpty(produceDate))//生产日期//dtpProduceDate.Checked
            {
                sb.Append(" AND ProduceDate>=#");
                sb.Append(produceDate);
                sb.Append("#");
                sb.Append(" AND ProduceDate<=#");
                sb.Append(produceDate);
                //sb.Append("-");
                //sb.Append(dtpProduceDate.Value.Month.ToString());
                //sb.Append("-");
                //sb.Append(dtpProduceDate.Value.Day.ToString());
                sb.Append(" 23:59:59#");
            }
            if (!produceComSelectedValue.Equals(""))//生产单位
                sb.AppendFormat(" AND ProduceCompany='{0}'", produceComSelectedValue);
            else if (!string.IsNullOrEmpty(cmbProduceCompany.Text.Trim()))
                sb.AppendFormat(" AND ProduceCompanyName LIKE '%{0}%'", cmbProduceCompany.Text);
            if (!producePlaceSelectValue.Equals(""))//产地
                sb.AppendFormat(" AND ProducePlace='{0}'", producePlaceSelectValue);
            else if (!cmbProducePlace.Text.Trim().Equals(""))
                sb.AppendFormat(" AND ProducePlaceName LIKE '%{0}%'", cmbProducePlace.Text);
            if (!displayComSelectedValue.Equals(""))//档口/店面
                sb.AppendFormat(" AND CheckedComDis='{0}'", displayComSelectedValue);
            else if (!cmbDisplayName.Text.Trim().Equals(""))
                sb.AppendFormat(" AND CheckedComDis LIKE '%{0}%'", cmbDisplayName.Text);
            if (cmbCheckType.SelectedIndex>=0)//检测类型 
                sb.AppendFormat(" AND CheckType='{0}'", cmbCheckType.Text);
            if (!string.IsNullOrEmpty(txtSentCompany.Text.Trim()))//送检单位
                sb.AppendFormat(" AND SentCompany LIKE '%{0}%'", txtSentCompany.Text);
            if (!string.IsNullOrEmpty(txtOrCheckNo.Text.Trim()))//原检测编号
                sb.AppendFormat(" AND OrCheckNo LIKE '%{0}%'", txtOrCheckNo.Text);
            if (!sUpperComSelectedValue.Equals(""))//所属市场
                sb.AppendFormat(" AND UpperCompany='{0}'", sUpperComSelectedValue);//cmbUpperCompany.Text
            else if (!cmbUpperCompany.Text.Trim().Equals(""))
                sb.AppendFormat(" AND UpperCompanyName LIKE '%{0}%'", cmbUpperCompany.Text);
            if (!sCheckedComSelectedValue.Equals(""))//受检人单位
                sb.AppendFormat(" AND CheckedCompany='{0}'", sCheckedComSelectedValue);
            else if (!cmbCheckedCompany.Text.Trim().Equals(""))
                sb.AppendFormat(" AND CheckedCompanyName LIKE '%{0}%'", cmbCheckedCompany.Text);
            if (cmbCheckItem.SelectedValue != null && cmbCheckItem.SelectedValue.ToString() != "")//检测项目
                sb.AppendFormat(" AND CheckTotalItem='{0}'", cmbCheckItem.SelectedValue.ToString());
            else if (cmbCheckItem.Text.Trim() != "")
                sb.AppendFormat(" AND CheckTotalItemName LIKE '%{0}%'", cmbCheckItem.Text);
            if (cmbChecker.SelectedValue != null)//检测人
                sb.AppendFormat(" AND Checker='{0}'", cmbChecker.SelectedValue.ToString());
            if (!txtCheckValueInfo.Text.Trim().Equals(""))//检测值
                sb.AppendFormat(" AND CheckValueInfo LIKE '%{0}%'", txtCheckValueInfo.Text);
            if (cmbAssessor.SelectedValue != null)//核准人
                sb.AppendFormat("AND Assessor='{0}'", cmbAssessor.SelectedValue.ToString());
            else if (!cmbAssessor.Text.Trim().Equals(""))
                sb.AppendFormat(" AND AssessorName LIKE '%{0}%'", cmbAssessor.Text);
            if (cmbOrganizer.SelectedValue != null)//抽检人
                sb.AppendFormat("AND Organizer='{0}'", cmbOrganizer.SelectedValue.ToString());
            else if (!cmbOrganizer.Text.Trim().Equals(""))
                sb.AppendFormat(" AND OrganizerName LIKE '%{0}%'", cmbOrganizer.Text);
            string takeDate = txtTakeDate.Text;
            if(!string.IsNullOrEmpty(takeDate))// (dtpTakeDate.Checked)//抽检日期
            {
                sb.Append(" AND TakeDate>=#");
                sb.Append(takeDate);
                sb.Append("#");
                sb.Append(" AND TakeDate<=#");
                sb.Append(takeDate);
                //sb.Append("-");
                //sb.Append(dtpTakeDate.Value.Month.ToString());
                //sb.Append("-");
                //sb.Append(dtpTakeDate.Value.Day.ToString());
                sb.Append(" 23:59:59#");
            }
            if (cmbIsSentCheck.SelectedIndex >= 0)//是否送检
                sb.AppendFormat(" AND IsSentCheck='{0}'", cmbIsSentCheck.Text);
            //if (cmbCheckerVal.SelectedValue != null)//被检人确认
            if(cmbCheckerVal.SelectedIndex>0)
                sb.AppendFormat(" AND CheckederVal='{0}'", cmbCheckerVal.Text);//cmbCheckerVal.SelectedValue.ToString()
            //if (cmbResult.SelectedValue != null)//检测结论
            if(cmbResult.SelectedIndex>0)
                //sb.AppendFormat(" AND Result LIKE '%{0}%'", cmbResult.Text);
                sb.AppendFormat(" AND Result='{0}'", cmbResult.Text);
            else if (cmbResult.Text.Trim() != "")
                sb.AppendFormat(" AND Result LIKE '%{0}%'", cmbResult.Text);

            if (cmbCheckMethod.Text != "")
                sb.AppendFormat(" AND ResultType='{0}'", cmbCheckMethod.Text);
            //if (cmbCheckMethod.SelectedValue != null)//检测手段
            //{
            //    //ResultType
            //    sb.AppendFormat(" AND ResultType='{0}'", cmbCheckMethod.SelectedValue.ToString());
            //    //sb.AppendFormat(" AND CheckMethod='{0}'", cmbCheckMethod.SelectedValue.ToString());
            //}
            if (cmbCheckUnit.SelectedValue != null)//检测单位
                sb.AppendFormat(" AND CheckUnitCode = '{0}'", cmbCheckUnit.SelectedValue.ToString());
            if (cmbCheckMachine.SelectedValue != null)
                sb.AppendFormat(" AND CheckMachine = '{0}'", cmbCheckMachine.SelectedValue.ToString());

            //以下为网络版本的
            if (!cmbIsSend.Text.Trim().Equals(""))
            {
                if (cmbIsSend.Text.Trim().Equals("已发送"))
                    sb.Append(" AND IsSended=true");
                else if (cmbIsSend.Text.Trim().Equals("未发送"))
                    sb.Append(" AND IsSended=false");
            }
            if (!txtSender.Text.Trim().Equals(""))
                sb.AppendFormat(" AND Sender LIKE '%{0}%'", txtSender.Text);

            if (!cmbIsReSend.Text.Trim().Equals(""))
            {
                if (cmbIsReSend.Text.Trim().Equals("是"))
                    sb.Append(" AND IsReSended=true");
                else if (cmbIsReSend.Text.Trim().Equals("否"))
                    sb.Append(" AND IsReSended=false");
            }

            if (dtpSendedDate.Checked)//上传时间
            {
                sb.Append(" AND SendedDate>=#");
                sb.Append(dtpSendedDate.Value.Date.ToString());
                sb.Append("#");
                sb.Append(" AND SendedDate<=#");
                sb.Append(dtpSendedDate.Value.Year.ToString());
                sb.Append("-");
                sb.Append(dtpSendedDate.Value.Month.ToString());
                sb.Append("-");
                sb.Append(dtpSendedDate.Value.Day.ToString());
                sb.Append(" 23:59:59#");

            }

            QueryString = sb.ToString();
            sb.Length = 0;

            #region 原先的写法
            //QueryStr+="CheckStartDate>=#" + dtpCheckStartDate.Value.Date.ToString() + "#";
            //QueryStr+=" and CheckStartDate<=#" + dtpCheckEndDate.Value.Year.ToString() + "-" + dtpCheckEndDate.Value.Month.ToString() + "-" + dtpCheckEndDate.Value.Day.ToString() + " 23:59:59#";
            //if(!sDisplayComSelectedValue.Equals(""))
            //{
            //    QueryStr+=" and CheckedComDis='" + cmbDisplayName.Text + "'";
            //}
            //else if(!cmbDisplayName.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and CheckedComDis Like '%" + cmbDisplayName.Text + "%'";
            //}

            //if(!sFoodSelectedValue.Equals(""))
            //{
            //    QueryStr+=" and FoodCode='" + sFoodSelectedValue + "'";		
            //}
            //if(!sProduceComSelectedValue.Equals(""))
            //{
            //    QueryStr+=" and ProduceCompany='" + sProduceComSelectedValue + "'";
            //}
            //if(!sCheckedComSelectedValue.Equals(""))
            //{
            //    QueryStr+=" and CheckedCompany='" + sCheckedComSelectedValue + "'";		
            //}
            //else if(!cmbCheckedCompany.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and CheckedCompanyName Like '%" + cmbCheckedCompany.Text.Trim() + "%'";		
            //}
            //if(!sCheckedUpperComSelectedValue.Equals(""))
            //{
            //    QueryStr+=" and UpperCompany='" + sCheckedUpperComSelectedValue + "'";	
            //}
            //if(!sProducePlaceSelectValue.Equals(""))
            //{
            //    QueryStr+=" and ProducePlace='" + sProducePlaceSelectValue + "'";	
            //}
            //if(!cmbCheckType.Text.Equals(""))
            //{
            //    QueryStr+=" and CheckType='" + cmbCheckType.Text.ToString() + "'";	
            //}
            //if(!txtSampleModel.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and SampleModel Like '%" + txtSampleModel.Text.Trim().ToString() + "%'";	
            //}
            //if(!txtProvider.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and Provider Like '%" + txtProvider.Text.Trim().ToString() + "%'";	
            //}
            //if(!txtSentCompany.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and SentCompany Like '%" + txtSentCompany.Text.Trim().ToString() + "%'";	
            //}
            //if(!cmbResult.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and Result='" + cmbResult.Text.Trim() + "'";
            //}
            //if(cmbCheckUnit.SelectedValue!=null)
            //{
            //    QueryStr+=" and CheckUnitCode='" + cmbCheckUnit.SelectedValue.ToString() + "'";
            //}
            //if(cmbChecker.SelectedValue!=null)
            //{
            //    QueryStr+=" and Checker='" + cmbChecker.SelectedValue.ToString() + "'";
            //}
            //if(cmbOrganizer.SelectedValue !=null)
            //{
            //    QueryStr+=" and Organizer='" + cmbOrganizer.SelectedValue.ToString() + "'";
            //}
            //if(!cmbIsSend.Text.Trim().Equals(""))
            //{
            //    if(cmbIsSend.Text.Trim().Equals("已发送"))
            //    {
            //        QueryStr+=" and IsSended=true";
            //    }
            //    else if(cmbIsSend.Text.Trim().Equals("未发送"))
            //    {
            //        QueryStr+=" and IsSended=false";
            //    }
            //}
            //if(cmbProduceCompany.SelectedValue!=null)
            //{
            //    QueryStr+=" and ProduceCompany='" + cmbProduceCompany.SelectedValue.ToString() + "'";
            //}
            //if(cmbCheckMachine.SelectedValue!=null)
            //{
            //    QueryStr+=" and CheckMachine='" + cmbCheckMachine.SelectedValue.ToString() + "'";
            //}
            //if(!txtCheckNo.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and CheckNo like '%" + txtCheckNo.Text.Trim() + "%'";
            //}
            //if(!cmbCheckMethod.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and ResultType='" + cmbCheckMethod.Text.Trim() + "'";
            //}
            //if(cmbCheckItem.SelectedValue!=null)
            //{
            //    QueryStr+=" and CheckTotalItem='" + cmbCheckItem.SelectedValue.ToString() + "'";
            //}
            //if(cmbAssessor.SelectedValue!=null)
            //{
            //    QueryStr+=" and Assessor='" + cmbAssessor.SelectedValue.ToString() + "'";
            //}
            //if(!txtCheckPlanCode.Text.Trim().Equals(""))
            //{
            //    QueryStr+=" and CheckPlanCode Like '%" + txtCheckPlanCode.Text.Trim() + "%'";
            //}
            //if(cmbIsSentCheck.SelectedIndex>=0 )
            //{
            //    QueryStr+=" and IsSentCheck= '" + cmbIsSentCheck.Text.Trim() + "'";
            //}
            //if(cmbCheckerVal.SelectedIndex>=0 )
            //{
            //    QueryStr+=" and CheckederVal= '" + cmbCheckerVal.Text.Trim() + "'";
            //}
            //if(!txtSender.Text.Trim().Equals("") )
            //{
            //    QueryStr+=" and SenderName Like '%" + txtSender.Text.Trim() + "%'";
            //}
            //if(!cmbIsReSend.Text.Trim().Equals(""))
            //{
            //    if(cmbIsReSend.Text.Trim().Equals("是"))
            //    {
            //        QueryStr+=" and IsReSended=true";
            //    }
            //    else if(cmbIsReSend.Text.Trim().Equals("否"))
            //    {
            //        QueryStr+=" and IsReSended=false";
            //    }
            //}
            //if(dtpSendedDate.Checked)
            //{
            //    QueryStr+=" And SendedDate>=#" + dtpSendedDate.Value.Date.ToString() + "#";
            //    QueryStr+=" and SendedDate<=#" + dtpSendedDate.Value.Year.ToString()+"-"+ dtpSendedDate.Value.Month.ToString()+"-"+dtpSendedDate.Value.Day.ToString()+ " 23:59:59#";

            //}
            #endregion

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmResultQuery_Load(object sender, System.EventArgs e)
        {
            //if (ShareOption.IsDataLink)
            {
                lblParent.Text = ShareOption.AreaTitle + "：";
                lblName.Text = ShareOption.NameTitle + "：";
                lblDomain.Text = ShareOption.DomainTitle + "：";
                lblSample.Text = ShareOption.SampleTitle + "：";
            }
            dtpCheckStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpCheckEndDate.Value = DateTime.Now;

            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }

            cmbCheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
            cmbCheckUnit.DataMember = "UserUnit";
            cmbCheckUnit.DisplayMember = "FullName";
            cmbCheckUnit.ValueMember = "SysCode";
            cmbCheckUnit.Columns["StdCode"].Caption = "编号";
            cmbCheckUnit.Columns["FullName"].Caption = "检测单位";
            cmbCheckUnit.Columns["SysCode"].Caption = "系统编号";

            DataSet dst = ShareOption.DtblChecker.DataSet;
            cmbChecker.DataSource = dst;
            cmbChecker.DataMember = "UserInfo";
            cmbChecker.DisplayMember = "Name";
            cmbChecker.ValueMember = "UserCode";
            cmbChecker.Columns["Name"].Caption = "检测人";
            cmbChecker.Columns["UserCode"].Caption = "系统编号";

         
            clsMachineOpr opr9 = new clsMachineOpr();
            DataTable dt9 = opr9.GetAsDataTable("IsShow=true", "OrderId", 1);
            cmbCheckMachine.DataSource = dt9.DataSet;
            cmbCheckMachine.DataMember = "Machine";
            cmbCheckMachine.DisplayMember = "MachineName";
            cmbCheckMachine.ValueMember = "SysCode";
            cmbCheckMachine.Columns["MachineName"].Caption = "检测仪器";
            cmbCheckMachine.Columns["SysCode"].Caption = "系统编号";

          
            cmbResult.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbResult.AddItemCols = 1;
            cmbResult.AddItemTitles("检测结果");
            cmbResult.AddItem("");
            cmbResult.AddItem(ShareOption.ResultEligi);
            cmbResult.AddItem(ShareOption.ResultFailure);

            cmbIsSend.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbIsSend.AddItemCols = 1;
            cmbIsSend.AddItemTitles("发送状态");
            cmbIsSend.AddItem("");
            cmbIsSend.AddItem(ShareOption.SendState1);
            cmbIsSend.AddItem(ShareOption.SendState0);

            cmbCheckMethod.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbCheckMethod.AddItemCols = 1;
            cmbCheckMethod.AddItemTitles("检测手段");
            cmbCheckMethod.AddItem(ShareOption.ResultType5);
            cmbCheckMethod.AddItem(ShareOption.ResultType1);
            if (queryType != 1)
            {
                cmbCheckMethod.AddItem(ShareOption.ResultType3);
            }


            clsCheckItemOpr opr1 = new clsCheckItemOpr();
            DataTable dt1 = opr1.GetAsDataTable("IsLock=false", "SysCode", 1);
            cmbCheckItem.DataSource = dt1.DataSet;
            cmbCheckItem.DataMember = "CheckItem";
            cmbCheckItem.DisplayMember = "ItemDes";
            cmbCheckItem.ValueMember = "SysCode";
            cmbCheckItem.Columns["StdCode"].Caption = "编号";
            cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
            cmbCheckItem.Columns["SysCode"].Caption = "系统编号";

            cmbAssessor.DataSource = dst.Copy();
            cmbAssessor.DataMember = "UserInfo";
            cmbAssessor.DisplayMember = "Name";
            cmbAssessor.ValueMember = "UserCode";
            cmbAssessor.Columns["Name"].Caption = "核准人";
            cmbAssessor.Columns["UserCode"].Caption = "系统编号";



            cmbOrganizer.DataSource = dst.Copy();
            cmbOrganizer.DataMember = "UserInfo";
            cmbOrganizer.DisplayMember = "Name";
            cmbOrganizer.ValueMember = "UserCode";
            cmbOrganizer.Columns["Name"].Caption = "抽样人";
            cmbOrganizer.Columns["UserCode"].Caption = "系统编号";
         

            cmbCheckType.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbCheckType.AddItemCols = 1;
            cmbCheckType.AddItemTitles("检测类型");
            cmbCheckType.AddItem("抽检");
            cmbCheckType.AddItem("送检");
            cmbCheckType.AddItem("抽检复检");
            cmbCheckType.AddItem("送检复检");
            if (queryType == 2)
            {
                cmbCheckMethod.Enabled = false;
                cmbCheckMachine.Enabled = false;
            }
        }

        private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string sType = "查询";
            frmFoodSelect frm = new frmFoodSelect(sType, foodSelectedValue);
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                foodSelectedValue = frm.sNodeTag;
                cmbFood.Text = frm.sNodeName;
            }
            else
            {
                foodSelectedValue = string.Empty;
                cmbFood.Text = string.Empty;
            }
            e.Cancel = true;
        }

        //private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    frmCheckedComSelect comSelect = new frmCheckedComSelect("", checkedComSelectedValue);
        //    if (!frmMain.IsLoadCheckedComSel)
        //    {
        //        comSelect.Tag = "Checked";
        //    }
        //    else
        //    {
        //        comSelect.Tag = "Checked";
        //        comSelect.SetFormValues("", checkedComSelectedValue);
        //    }
        //    comSelect.ShowDialog(this);
        //    if (comSelect.DialogResult == DialogResult.OK)
        //    {
        //        checkedComSelectedValue = comSelect.sNodeTag;
        //        cmbCheckedCompany.Text = comSelect.sNodeName;

        //    }
        //    else
        //    {
        //        displayComSelectedValue = string.Empty;
        //        cmbCheckedCompany.Text = string.Empty;
        //    }
        //    comSelect.Hide();
        //    e.Cancel = true;
        //}
        private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FrmMain.formCheckedComSelect == null)
                FrmMain.formCheckedComSelect = new frmCheckedComSelect("", "");

            if (ShareOption.AllowHandInputCheckUint)
            {
                if (!sUpperComSelectedValue.Equals(""))
                {
                    FrmMain.formCheckedComSelect = new frmCheckedComSelect("", sUpperComSelectedValue);
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                }
                else
                {
                    MessageBox.Show(this, "请先选择所属市场！");
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (!FrmMain.IsLoadCheckedComSel)
                {
                    FrmMain.formCheckedComSelect = new frmCheckedComSelect("", sCheckedComSelectedValue);
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                }
                else
                {
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                    FrmMain.formCheckedComSelect.SetFormValues("", sCheckedComSelectedValue);
                }
            }
            FrmMain.formCheckedComSelect.ShowDialog(this);
            if (FrmMain.formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                this.sCheckedComSelectedValue = FrmMain.formCheckedComSelect.sNodeTag;
                this.cmbCheckedCompany.Text = FrmMain.formCheckedComSelect.sNodeName;
                if (!ShareOption.AllowHandInputCheckUint)
                {
                    if (FrmMain.formCheckedComSelect.sParentCompanyName.Equals(""))
                    {
                        this.cmbUpperCompany.Text = "";
                        this.sUpperComSelectedValue = "";
                    }
                    else
                    {
                        this.cmbUpperCompany.Text = FrmMain.formCheckedComSelect.sParentCompanyName.ToString();
                        this.sUpperComSelectedValue = FrmMain.formCheckedComSelect.sParentCompanyTag.ToString();

                    }
                }
                //if (frmMain.formCheckedComSelect.sNodeCompanyInfo.Equals(""))
                //{
                //    this.txtCompanyInfo.Text = "";
                //}
                //else
                //{
                //    this.txtCompanyInfo.Text = frmMain.formCheckedComSelect.sNodeCompanyInfo.ToString();
                //}
            }
            FrmMain.formCheckedComSelect.Hide();
            e.Cancel = true;


        }
        private void cmbDisplayName_BeforeOpen(object sender, CancelEventArgs e)
        {
            //if (!frmMain.IsLoadCheckedDisplaySel)
            //{
            formCheckedDisplaySelect = new frmCheckedDisplaySelect(string.Empty, displayComSelectedValue);
            //}
            formCheckedDisplaySelect.ShowDialog(this);
            if (formCheckedDisplaySelect.DialogResult == DialogResult.OK)
            {
                displayComSelectedValue = formCheckedDisplaySelect.sNodeTag;
                cmbDisplayName.Text = formCheckedDisplaySelect.sNodeName;
            }
            else
            {
                displayComSelectedValue = string.Empty;
                cmbDisplayName.Text = string.Empty;
            }
            formCheckedDisplaySelect.Hide();
            e.Cancel = true;
        }

        private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmCheckedComSelect frm = new frmCheckedComSelect(string.Empty, produceComSelectedValue);
            frm.Tag = "Produce";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                produceComSelectedValue = frm.sNodeTag;
                cmbProduceCompany.Text = frm.sNodeName;
            }
            else
            {
                produceComSelectedValue = string.Empty;
                cmbProduceCompany.Text = string.Empty;
            }
            e.Cancel = true;
        }

        private void cmbUpperCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            if (!FrmMain.IsLoadCheckedUpperComSel)
            {
                FrmMain.formCheckedUpperComSelect = new frmCheckedComSelect("", sUpperComSelectedValue);
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
            }
            else
            {
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
                FrmMain.formCheckedUpperComSelect.SetFormValues("", sUpperComSelectedValue);
            }
            FrmMain.formCheckedUpperComSelect.ShowDialog(this);
            if (FrmMain.formCheckedUpperComSelect.DialogResult == DialogResult.OK)
            {
                sUpperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
            }
            FrmMain.formCheckedUpperComSelect.Hide();
            e.Cancel = true;
        }

        private void cmbProducePlace_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmProduceAreaSelect frm = new frmProduceAreaSelect(producePlaceSelectValue);
            frm.Tag = "ProducePlace";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                producePlaceSelectValue = frm.sNodeTag;
                cmbProducePlace.Text = frm.sNodeName;
            }
            else
            {
                producePlaceSelectValue = string.Empty;
                cmbProducePlace.Text = string.Empty;
            }
            e.Cancel = true;
        }

        private void cmbCheckedCompany_LostFocus(object sender, EventArgs e)
        {
            if ((!sCheckedComSelectedValue.Equals("")))
            {
                string strComName = clsCompanyOpr.NameFromCode(sCheckedComSelectedValue);
                if (!cmbCheckedCompany.Text.Trim().Equals(strComName))
                {
                    sCheckedComSelectedValue = string.Empty;
                }
            }
        }


        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        private void dtpTakeDate_ValueChanged(object sender, EventArgs e)
        {
            txtTakeDate.Text = dtpTakeDate.Value.ToString("yyyy-MM-dd");
        }
    }
}
