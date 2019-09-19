using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// ���쵥λά��
    /// </summary>
    public class frmCheckedCom : TitleBarBase
    {
        #region �ؼ�����
        private System.Windows.Forms.Button btnAllShow;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Button btnQuery;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1Command c1Command5;
        private C1.Win.C1Command.C1ToolBar c1ToolBar1;
        public C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1CommandLink c1CommandLink5;
        private C1.Win.C1Command.C1CommandLink c1CommandLink6;
        public C1.Win.C1Command.C1Command cmdAdd;
        private C1.Win.C1Command.C1Command cmdEdit;
        private C1.Win.C1Command.C1Command cmdDelete;
        private C1.Win.C1Command.C1Command cmdPrint;
        private C1.Win.C1Command.C1Command cmdExit;
        private System.ComponentModel.IContainer components;
        private C1.Win.C1Command.C1CommandLink c1CommandLink7;
        private C1.Win.C1Command.C1CommandLink c1CommandLink8;
        private C1.Win.C1Command.C1CommandControl c1CommandControl1;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink9;
        private C1.Win.C1Command.C1CommandLink c1CommandLink10;
        private C1.Win.C1Command.C1CommandControl c1CommandControl2;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink11;
        private C1.Win.C1Command.C1CommandLink c1CommandLink12;
        private C1.Win.C1Command.C1CommandControl c1CommandControl3;
        private System.Windows.Forms.TextBox txtDisplayName;
        private C1.Win.C1Command.C1CommandLink c1CommandLink13;
        private C1.Win.C1Command.C1CommandLink c1CommandLink14;
        private C1.Win.C1Command.C1CommandControl c1CommandControl4;
        private C1.Win.C1Command.C1CommandLink c1CommandLink15;
        private C1.Win.C1Command.C1CommandLink c1CommandLink16;
        private C1.Win.C1Command.C1CommandControl c1CommandControl5;
        private C1.Win.C1Command.C1CommandLink c1CommandLink17;
        private C1.Win.C1Command.C1CommandLink c1CommandLink18;
        private C1.Win.C1Command.C1CommandControl c1CommandControl6;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TreeView treeView1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink19;
        private C1.Win.C1Command.C1Command cmdShow;
        #endregion

        private clsCompany companyModel;
        private readonly clsCompanyOpr curObjectOpr;
        private static string parentAreaCode = string.Empty;
        private static string kindCode = string.Empty;
        private static string parentStdCode = string.Empty;
        private string tag = string.Empty;
        private SplitContainer splitContainer1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        protected C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid02;
        private Button btnDatailQuer;
        private TreeNode[] prevNodes;
        private C1.Win.C1Command.C1CommandControl c1CommandControl7;
        private string NODE = string.Empty;
        private C1.Win.C1Command.C1CommandControl c1CommandControl8;
        private C1.Win.C1Command.C1CommandLink c1CommandLink20;
        private string CIIDname = string.Empty;

        /// <summary>
        ///����ⵥλά��
        /// </summary>
        public frmCheckedCom()
        {
            InitializeComponent();
            curObjectOpr = new clsCompanyOpr();
            prevNodes = new TreeNode[ShareOption.MaxLevel];
            for (int i = 0; i < ShareOption.MaxLevel; i++)
            {
                prevNodes[i] = new TreeNode();
            }
            c1FlexGrid1.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// ������������ʹ�õ���Դ��
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
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckedCom));
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.cmdAdd = new C1.Win.C1Command.C1Command();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.c1CommandControl1 = new C1.Win.C1Command.C1CommandControl();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.c1CommandControl2 = new C1.Win.C1Command.C1CommandControl();
            this.label1 = new System.Windows.Forms.Label();
            this.c1CommandControl3 = new C1.Win.C1Command.C1CommandControl();
            this.label2 = new System.Windows.Forms.Label();
            this.c1CommandControl4 = new C1.Win.C1Command.C1CommandControl();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.c1CommandControl5 = new C1.Win.C1Command.C1CommandControl();
            this.btnQuery = new System.Windows.Forms.Button();
            this.c1CommandControl6 = new C1.Win.C1Command.C1CommandControl();
            this.btnAllShow = new System.Windows.Forms.Button();
            this.c1CommandControl7 = new C1.Win.C1Command.C1CommandControl();
            this.btnDatailQuer = new System.Windows.Forms.Button();
            this.cmdShow = new C1.Win.C1Command.C1Command();
            this.c1CommandControl8 = new C1.Win.C1Command.C1CommandControl();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink19 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink10 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink8 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink12 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink14 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink16 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink18 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink20 = new C1.Win.C1Command.C1CommandLink();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1FlexGrid02 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink11 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink13 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink15 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink17 = new C1.Win.C1Command.C1CommandLink();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.c1ToolBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid02)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.cmdAdd);
            this.c1CommandHolder1.Commands.Add(this.cmdEdit);
            this.c1CommandHolder1.Commands.Add(this.cmdDelete);
            this.c1CommandHolder1.Commands.Add(this.c1Command5);
            this.c1CommandHolder1.Commands.Add(this.cmdPrint);
            this.c1CommandHolder1.Commands.Add(this.cmdExit);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl1);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl2);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl3);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl4);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl5);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl6);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl7);
            this.c1CommandHolder1.Commands.Add(this.cmdShow);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl8);
            this.c1CommandHolder1.Owner = this;
            this.c1CommandHolder1.SmoothImages = false;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.cmdAdd.Text = "����";
            this.cmdAdd.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd_Click);
            this.cmdAdd.Select += new System.EventHandler(this.cmdAdd_Select);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.cmdEdit.Text = "�޸�";
            this.cmdEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdEdit_Click);
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "ɾ��";
            this.cmdDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDelete_Click);
            this.cmdDelete.Select += new System.EventHandler(this.cmdDelete_Select);
            // 
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "��ӡ";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.cmdExit.Text = "�˳�";
            this.cmdExit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdExit_Click);
            this.cmdExit.Select += new System.EventHandler(this.cmdExit_Select);
            // 
            // c1CommandControl1
            // 
            this.c1CommandControl1.Control = this.txtFullName;
            this.c1CommandControl1.Name = "c1CommandControl1";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFullName.Location = new System.Drawing.Point(534, 4);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(168, 26);
            this.txtFullName.TabIndex = 0;
            this.txtFullName.TabStop = false;
            // 
            // c1CommandControl2
            // 
            this.c1CommandControl2.Control = this.label1;
            this.c1CommandControl2.Name = "c1CommandControl2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.label1.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(412, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "��λ����λ����";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // c1CommandControl3
            // 
            this.c1CommandControl3.Control = this.label2;
            this.c1CommandControl3.Name = "c1CommandControl3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.label2.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(704, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "����/����/���ƺ�";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // c1CommandControl4
            // 
            this.c1CommandControl4.Control = this.txtDisplayName;
            this.c1CommandControl4.Name = "c1CommandControl4";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDisplayName.Location = new System.Drawing.Point(842, 4);
            this.txtDisplayName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(132, 26);
            this.txtDisplayName.TabIndex = 3;
            this.txtDisplayName.TabStop = false;
            // 
            // c1CommandControl5
            // 
            this.c1CommandControl5.Control = this.btnQuery;
            this.c1CommandControl5.Name = "c1CommandControl5";
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.btnQuery.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(976, 2);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(92, 31);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.TabStop = false;
            this.btnQuery.Text = "��ѯ";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // c1CommandControl6
            // 
            this.c1CommandControl6.Control = this.btnAllShow;
            this.c1CommandControl6.Name = "c1CommandControl6";
            // 
            // btnAllShow
            // 
            this.btnAllShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.btnAllShow.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAllShow.Location = new System.Drawing.Point(1070, 2);
            this.btnAllShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAllShow.Name = "btnAllShow";
            this.btnAllShow.Size = new System.Drawing.Size(100, 31);
            this.btnAllShow.TabIndex = 5;
            this.btnAllShow.TabStop = false;
            this.btnAllShow.Text = "ȫ����ʾ";
            this.btnAllShow.UseVisualStyleBackColor = false;
            this.btnAllShow.Click += new System.EventHandler(this.btnAllShow_Click);
            // 
            // c1CommandControl7
            // 
            this.c1CommandControl7.Control = this.btnDatailQuer;
            this.c1CommandControl7.Name = "c1CommandControl7";
            // 
            // btnDatailQuer
            // 
            this.btnDatailQuer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.btnDatailQuer.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDatailQuer.Location = new System.Drawing.Point(1172, 2);
            this.btnDatailQuer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDatailQuer.Name = "btnDatailQuer";
            this.btnDatailQuer.Size = new System.Drawing.Size(100, 31);
            this.btnDatailQuer.TabIndex = 19;
            this.btnDatailQuer.TabStop = false;
            this.btnDatailQuer.Text = "��ʾ��Ӫ��";
            this.btnDatailQuer.UseVisualStyleBackColor = false;
            this.btnDatailQuer.Visible = false;
            this.btnDatailQuer.Click += new System.EventHandler(this.btnDatailQuer_Click);
            // 
            // cmdShow
            // 
            this.cmdShow.Image = ((System.Drawing.Image)(resources.GetObject("cmdShow.Image")));
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.Text = "��ʾ��ϸ��Ϣ";
            this.cmdShow.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdShow_Click);
            this.cmdShow.Select += new System.EventHandler(this.cmdShow_Select);
            // 
            // c1CommandControl8
            // 
            this.c1CommandControl8.Control = this.btnDatailQuer;
            this.c1CommandControl8.Name = "c1CommandControl8";
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1,
            this.c1CommandLink19,
            this.c1CommandLink2,
            this.c1CommandLink3,
            this.c1CommandLink4,
            this.c1CommandLink5,
            this.c1CommandLink6,
            this.c1CommandLink10,
            this.c1CommandLink8,
            this.c1CommandLink12,
            this.c1CommandLink14,
            this.c1CommandLink16,
            this.c1CommandLink18,
            this.c1CommandLink20});
            this.c1ToolBar1.Controls.Add(this.btnAllShow);
            this.c1ToolBar1.Controls.Add(this.btnQuery);
            this.c1ToolBar1.Controls.Add(this.txtDisplayName);
            this.c1ToolBar1.Controls.Add(this.label2);
            this.c1ToolBar1.Controls.Add(this.label1);
            this.c1ToolBar1.Controls.Add(this.txtFullName);
            this.c1ToolBar1.Controls.Add(this.btnDatailQuer);
            this.c1ToolBar1.Location = new System.Drawing.Point(4, 39);
            this.c1ToolBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.ShowToolTips = false;
            this.c1ToolBar1.Size = new System.Drawing.Size(1273, 35);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink1.Command = this.cmdAdd;
            // 
            // c1CommandLink19
            // 
            this.c1CommandLink19.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink19.Command = this.cmdShow;
            this.c1CommandLink19.Text = "�鿴";
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink2.Command = this.cmdEdit;
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink3.Command = this.cmdDelete;
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink5.Command = this.cmdPrint;
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink6.Command = this.cmdExit;
            // 
            // c1CommandLink10
            // 
            this.c1CommandLink10.Command = this.c1CommandControl2;
            // 
            // c1CommandLink8
            // 
            this.c1CommandLink8.Command = this.c1CommandControl1;
            // 
            // c1CommandLink12
            // 
            this.c1CommandLink12.Command = this.c1CommandControl3;
            // 
            // c1CommandLink14
            // 
            this.c1CommandLink14.Command = this.c1CommandControl4;
            // 
            // c1CommandLink16
            // 
            this.c1CommandLink16.Command = this.c1CommandControl5;
            // 
            // c1CommandLink18
            // 
            this.c1CommandLink18.Command = this.c1CommandControl6;
            // 
            // c1CommandLink20
            // 
            this.c1CommandLink20.Command = this.c1CommandControl8;
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1Sizer1.Controls.Add(this.splitContainer1);
            this.c1Sizer1.Controls.Add(this.treeView1);
            this.c1Sizer1.GridDefinition = "98.8217967599411:False:False;\t25.7690075449797:False:False;73.5345327916425:False" +
    ":False;";
            this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Sizer1.Location = new System.Drawing.Point(4, 75);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(1723, 679);
            this.c1Sizer1.TabIndex = 8;
            this.c1Sizer1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(452, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.c1FlexGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.c1FlexGrid02);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1267, 671);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 9;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 23;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(1267, 671);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 8;
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick_1);
            // 
            // c1FlexGrid02
            // 
            this.c1FlexGrid02.AllowEditing = false;
            this.c1FlexGrid02.BackColor = System.Drawing.Color.White;
            this.c1FlexGrid02.ColumnInfo = resources.GetString("c1FlexGrid02.ColumnInfo");
            this.c1FlexGrid02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid02.ForeColor = System.Drawing.Color.Black;
            this.c1FlexGrid02.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid02.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1FlexGrid02.Name = "c1FlexGrid02";
            this.c1FlexGrid02.Rows.Count = 1;
            this.c1FlexGrid02.Rows.DefaultSize = 23;
            this.c1FlexGrid02.Rows.MaxSize = 200;
            this.c1FlexGrid02.Rows.MinSize = 20;
            this.c1FlexGrid02.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid02.Size = new System.Drawing.Size(128, 133);
            this.c1FlexGrid02.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid02.Styles"));
            this.c1FlexGrid02.TabIndex = 25;
            this.c1FlexGrid02.Click += new System.EventHandler(this.c1FlexGrid02_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(444, 671);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.Click += new System.EventHandler(this.treeView1_Click);
            // 
            // frmCheckedCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(1378, 757);
            this.Controls.Add(this.c1ToolBar1);
            this.Controls.Add(this.c1Sizer1);
            this.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmCheckedCom";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "���쵥λά��";
            this.Load += new System.EventHandler(this.frmCheckedCom_Load);
            this.Controls.SetChildIndex(this.c1Sizer1, 0);
            this.Controls.SetChildIndex(this.c1ToolBar1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.c1ToolBar1.ResumeLayout(false);
            this.c1ToolBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid02)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheckedCom_Load(object sender, System.EventArgs e)
        {
            tag = this.Tag.ToString();
            if (tag.Equals("CHECKED"))//���쵥λ
            {
                if (ShareOption.IsDataLink)
                {
                    this.cmdAdd.Visible = true;
                    this.cmdEdit.Visible = true;
                    this.cmdShow.Visible = false;
                    this.cmdDelete.Visible = true;
                    this.c1Command5.Visible = true;
                    this.Text = "���쵥λά��";
                }
                else
                {
                    this.cmdAdd.Visible = false;
                    this.cmdEdit.Visible = false;
                    this.cmdShow.Visible = true;
                    this.cmdDelete.Visible = false;
                    this.c1Command5.Visible = false;
                    this.Text = "���쵥λ��ѯ";
                }
                this.label1.Text = "��λ����λ����";
                this.label2.Text = "����/����/���ƺ�";
                this.c1CommandControl3.Visible = true;
                this.c1CommandControl4.Visible = true;
            }
            else if (tag.Equals("MAKE"))//������λ
            {
                this.cmdShow.Visible = false;
                this.treeView1.Visible = false;
                this.c1Sizer1.Grid.Columns[0].Size = 0;
                this.Text = ShareOption.ProductionUnitNameTag + "ά��";
                this.c1CommandControl3.Visible = false;
                this.c1CommandControl4.Visible = false;
                this.btnDatailQuer.Visible = false;
                if (!ShareOption.IsDataLink)
                {
                    label2.Visible = false;
                    txtDisplayName.Visible = false;
                }
            }
            this.refreshGrid(null);

            if (tag.ToUpper() == "MAKE")
            {
                TitleBarText = "��Ӧ��ά��";
            }
            else
            {
                TitleBarText = "���쵥λ��ѯ";
            }
        }

        /// <summary>
        /// �˳�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            string distCode = string.Empty;
            if (this.treeView1.SelectedNode != null)
            {
                distCode = this.treeView1.SelectedNode.Tag.ToString();
            }
            clsCompany model = getModel(row);
            frmCheckedComEdit frm = new frmCheckedComEdit(model, distCode);
            if (tag.Equals("CHECKED"))//���쵥λά��
            {
                frm.Tag = "CHECKEDEDIT";
            }
            else if (tag.Equals("MAKE"))//������λά��
            {
                frm.Tag = "MAKEEDIT";
            }
            DialogResult dr = frm.ShowDialog(this);
            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                if (tag.Equals("CHECKED"))
                {
                    refreshGridPart();
                }
                else
                {
                    refreshGrid(this.treeView1.SelectedNode);
                }
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            companyModel = new clsCompany();
            //��ȡһ���µ�ϵͳ���
            string curCode = DateTime.Now.ToString("yyyyMMdd") + "1" + StringUtil.RepeatChar('0', ShareOption.CompanyKindCodeLen);
            string err = string.Empty;
            int max = curObjectOpr.GetMaxNO(curCode, ShareOption.CompanyCodeLen, out err);
            string syscode = curCode + (max + 1).ToString().PadLeft(ShareOption.CompanyCodeLen, '0');
            companyModel.SysCode = syscode;
            max = curObjectOpr.GetStdCodeMaxNO(parentStdCode, ShareOption.CompanyStdCodeLen, out err);
            companyModel.StdCode = parentStdCode + (max + 1).ToString().PadLeft(ShareOption.CompanyStdCodeLen, '0');
            companyModel.CompanyID = string.Empty;
            companyModel.OtherCodeInfo = string.Empty;
            companyModel.FullName = string.Empty;
            companyModel.ShortName = string.Empty;
            companyModel.DisplayName = string.Empty;
            companyModel.ShortCut = string.Empty;
            companyModel.RegCapital = 0;
            companyModel.Unit = string.Empty;
            companyModel.Incorporator = string.Empty;
            companyModel.Address = string.Empty;
            companyModel.PostCode = string.Empty;
            companyModel.LinkMan = string.Empty;
            companyModel.LinkInfo = string.Empty;
            companyModel.CreditLevel = string.Empty;
            companyModel.CreditRecord = string.Empty;
            companyModel.ProductInfo = string.Empty;
            companyModel.OtherInfo = string.Empty;
            companyModel.CheckLevel = string.Empty;
            companyModel.FoodSafeRecord = string.Empty;
            companyModel.FoodSafeRecord = string.Empty;
            companyModel.Remark = string.Empty;
            companyModel.IsLock = false;
            companyModel.IsReadOnly = ShareOption.DefaultIsReadOnly;
            string distCode = parentAreaCode;
            companyModel.Property = ShareOption.CompanyProperty0;//���߶���
            if (tag.Equals("CHECKED"))
            {
                companyModel.Property = ShareOption.CompanyProperty1;// "���쵥λ";
                if (ShareOption.IsDataLink)
                {
                    if (string.IsNullOrEmpty(parentAreaCode))
                    {
                        distCode = parentStdCode;
                    }
                    companyModel.DistrictCode = distCode;
                    companyModel.KindCode = kindCode;
                }
                else
                {
                    companyModel.DistrictCode = clsCompanyOpr.AreaCodeFromStdCode(parentStdCode);
                    companyModel.KindCode = clsCompanyOpr.KindCodeFromStdCode(parentStdCode);
                }
            }
            else if (tag.Equals("MAKE"))
            {
                //companyModel.Property = "������λ";
                companyModel.Property = ShareOption.CompanyProperty2;
                companyModel.KindCode = "00009";
                companyModel.DistrictCode = "001";
                companyModel.TSign = "������";
            }
            frmCheckedComEdit frm = new frmCheckedComEdit(companyModel, parentAreaCode);
            if (tag.Equals("CHECKED"))
            {
                frm.Tag = "CHECKEDADD";
            }
            else if (tag.Equals("MAKE"))
            {
                frm.Tag = "MAKEADD";

            }
            DialogResult dr = frm.ShowDialog(this);
            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                if (tag.Equals("CHECKED"))
                {
                    refreshGridPart();
                }
                else
                {
                    refreshGrid(this.treeView1.SelectedNode);
                    //refreshGrid(null );
                }
            }
        }

        private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //�ж��Ƿ���ɾ���ļ�¼
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                MessageBox.Show(this, "��ѡ��Ҫɾ���ļ�¼��");
                return;
            }
            string delStr = c1FlexGrid1[row, "SysCode"].ToString().Trim();
            //���û�ȷ��ɾ������
            if (MessageBox.Show(this, "ȷ��Ҫɾ��ѡ��ļ�¼����ؼ�¼��", "ѯ��", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            //ɾ����¼
            string err = string.Empty;
            int ret = curObjectOpr.DeleteByPrimaryKey(delStr, out err);
            if (!err.Equals(""))
            {
                MessageBox.Show(this, "���ݿ��������" + err);
            }
            if (tag.Equals("CHECKED"))
            {
                refreshGridPart();
            }
            else
            {
                refreshGrid(treeView1.SelectedNode);
            }
        }

        /// <summary>
        /// ����б�
        /// </summary>
        private void refreshGridPart()
        {
            TreeNode parentNode = this.treeView1.SelectedNode.Parent;
            TreeNode selNode = this.treeView1.SelectedNode;
            string selNodeTag = selNode.Tag.ToString();
            if (parentNode == null)
            {
                return;
            }
            string code = string.Empty;
            if (parentNode.Tag.ToString().Length > 4)
            {
                code = parentNode.Tag.ToString().Substring(4, parentNode.Tag.ToString().Length - 4);
            }
            if (selNodeTag.Substring(0, 4).Equals("DWLX"))
            {
                //����ǵ�λ���ͽڵ㣬���ýڵ��µ��ֽڵ���գ����¶�ȡ�µģ���չ��
                string sql = string.Empty;
                selNode.Nodes.Clear();
                sql = string.Format("Property='���쵥λ' and KindCode = '{0}' And DistrictCode='{1}' And Len(StdCode)={2}", selNodeTag.Substring(4, selNodeTag.Length - 4), code, ShareOption.CompanyStdCodeLen.ToString());
                TreeNode comNode = null;
                DataTable dt2 = curObjectOpr.GetAsDataTable(sql, "SysCode", 1);
                if (dt2 != null)
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        comNode = new TreeNode();
                        comNode.Text = dt2.Rows[j]["FullName"].ToString();
                        comNode.Tag = "DWST" + dt2.Rows[j]["StdCode"].ToString();
                        selNode.Nodes.Add(comNode);
                    }
                }
            }
            this.treeView1.SelectedNode = null;
            this.treeView1.SelectedNode = selNode;
        }

        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (tag.Equals("CHECKED"))
            {
                PrintOperation.PreviewGrid(this.c1FlexGrid1, "�����̻��б�", null);
            }
            else if (tag.Equals("MAKE"))
            {
                PrintOperation.PreviewGrid(this.c1FlexGrid1, ShareOption.ProductionUnitNameTag + "�б�", null);
            }
        }

        private void refreshGrid(TreeNode oprNode)
        {
            if (tag.Equals("CHECKED"))
            {
                this.treeView1.Nodes.Clear();
                clsUserUnitOpr curUserUnitOpr = new clsUserUnitOpr();
                int iType = 0;
                DataTable dt4 = curUserUnitOpr.GetParents(out iType);
                if (iType == -1)
                {
                    MessageBox.Show(this, "�����趨���㣡");
                    return;
                }
                if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                {
                    clsDistrictOpr curObjOpr = new clsDistrictOpr();
                    DataTable dtblDistrict = curObjOpr.GetAsDataTable(" SysCode Like '%" + clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode) + "%'", "SysCode", 0);
                    DataTable dt1 = curObjectOpr.GetAsDataTable("", "  A.SysCode ", 0);
                    if (dtblDistrict == null || dtblDistrict.Rows.Count == 0)
                    {
                        this.c1FlexGrid1.SetDataBinding(dt1.DataSet, "UserUnit");
                        this.setGridStyle1();
                        this.c1FlexGrid1.AutoSizeCols();
                        return;
                    }
                    clsCompanyKindOpr companyKindBLL = new clsCompanyKindOpr();
                    DataTable dtblCompanyKind = null;
                    //TreeNode comNode = null;
                    TreeNode childNode = null;
                    TreeNode theNode = null;
                    string sCode = string.Empty;
                    for (int i = 0; i < dtblDistrict.Rows.Count; i++)
                    {
                        sCode = dtblDistrict.Rows[i]["SysCode"].ToString();
                        theNode = new TreeNode();
                        theNode.Text = dtblDistrict.Rows[i]["Name"].ToString();
                        theNode.Tag = "XZJG" + sCode;
                        //���ýڵ��ǲ�����ϸ�ӽڵ㣬�Ǿ����ӵ�λ���ͽڵ�
                        if (clsDistrictOpr.DistrictIsMX(sCode))
                        {
                            dtblCompanyKind = companyKindBLL.GetAsDataTable(string.Format("IsLock=0 And CompanyProperty='{0}' or CompanyProperty='���߶���' And IsReadOnly=true", ShareOption.CompanyProperty1), "SysCode", 1);
                            if (dtblCompanyKind != null && dtblCompanyKind.Rows.Count >= 1)
                            {
                                for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
                                {
                                    childNode = new TreeNode();
                                    childNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                                    childNode.Tag = "DWLX" + dtblCompanyKind.Rows[j]["SysCode"].ToString();
                                    theNode.Nodes.Add(childNode);
                                    //or Property='���߶���' 
                                    string sql = string.Format("Property<>'{0}' and KindCode = '{1}' And DistrictCode='{2}' ", ShareOption.CompanyProperty2, dtblCompanyKind.Rows[j]["SysCode"].ToString(), sCode, ShareOption.CompanyStdCodeLen.ToString());
                                    DataTable dtblCompany = curObjectOpr.GetAsDataTable(sql, "SysCode", 3);
                                    #region
                                    //if (dtblCompany != null && dtblCompany.Rows.Count >= 1)
                                    //{
                                    //    for (int k = 0; k < dtblCompany.Rows.Count; k++)
                                    //    {
                                    //        comNode = new TreeNode();
                                    //        comNode.Text = dtblCompany.Rows[k]["FullName"].ToString();
                                    //        comNode.Tag = "DWST" + dtblCompany.Rows[k]["StdCode"].ToString();
                                    //        childNode.Nodes.Add(comNode);
                                    //    }
                                    //}
                                    #endregion
                                }
                            }
                        }
                        int icc = sCode.Length / ShareOption.DistrictCodeLevel;
                        int idd = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode).Length / ShareOption.DistrictCodeLevel;
                        int iMod = icc - idd + 1;
                        if (iMod == 1)
                        {
                            this.treeView1.Nodes.Add(theNode);
                        }
                        else
                        {
                            if (iMod >= 2)
                            {
                                prevNodes[iMod - 2].Nodes.Add(theNode);
                            }
                        }
                        if (iMod >= 1)
                            prevNodes[iMod - 1] = theNode;
                    }
                    if (this.treeView1.Nodes.Count >= 1)
                    {
                        this.treeView1.Nodes[0].Expand();
                    }
                }
                else
                {
                    TreeNode theNode = new TreeNode();
                    theNode.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
                    theNode.Tag = clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode);
                    this.treeView1.Nodes.Add(theNode);
                }
            }
            else if (tag.Equals("MAKE"))
            {
                DataTable dt = curObjectOpr.GetAsDataTable(string.Format("A.TSign='������'"), "A.SysCode", 0);
                this.c1FlexGrid1.DataSource = null;
                this.c1FlexGrid1.DataSource = dt; //.SetDataBinding(dt.DataSet, "Company");
                this.setGridStyle();
                this.c1FlexGrid1.AutoSizeCols();
            }
        }

        //����������������ʵ���Ժϲ�Ϊһ����������������Ҫ�ظ�����
        private void setGridStyle1()
        {
            try
            {
                this.c1FlexGrid1.Cols["SysCode"].Caption = "ϵͳ���";
                this.c1FlexGrid1.Cols["SysCode"].Visible = false;
                this.c1FlexGrid1.Cols["CAllow"].Caption = "���֤��";
                this.c1FlexGrid1.Cols["CompanyID"].Caption = "��λ����";
                this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "���֤��";
                this.c1FlexGrid1.Cols["OtherCodeInfo"].Visible = false;
                if (tag.Equals("MAKE"))
                {
                    //this.c1FlexGrid1.Cols["CompanyID"].Caption = "��λ����";
                    this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "��׼����";
                }
                this.c1FlexGrid1.Cols["FullName"].Caption = "��λ����";
                this.c1FlexGrid1.Cols["ShortName"].Caption = "��λ���";
                this.c1FlexGrid1.Cols["ShortName"].Visible = false;
                this.c1FlexGrid1.Cols["StdCode"].Caption = "Ӫҵִ��";
                this.c1FlexGrid1.Cols["DisplayName"].Caption = "��λ��Ϣ";
                c1FlexGrid1.Cols["DisplayName"].Visible = false;
                this.c1FlexGrid1.Cols["ShortCut"].Caption = "��ݱ���";
                this.c1FlexGrid1.Cols["ShortCut"].Visible = false;
                this.c1FlexGrid1.Cols["Property"].Caption = "��λ����";
                this.c1FlexGrid1.Cols["Property"].Visible = false;
                this.c1FlexGrid1.Cols["ComProperty"].Caption = "��������";
                this.c1FlexGrid1.Cols["ComProperty"].Visible = false;
                this.c1FlexGrid1.Cols["KindCode"].Caption = "��λ������";
                this.c1FlexGrid1.Cols["KindCode"].Visible = false;
                this.c1FlexGrid1.Cols["KindName"].Caption = "��λ���";
                this.c1FlexGrid1.Cols["RegCapital"].Caption = "ע���ʽ�";
                this.c1FlexGrid1.Cols["Unit"].Caption = "�ʽ�λ";
                this.c1FlexGrid1.Cols["RegCapital"].Visible = false;
                this.c1FlexGrid1.Cols["Unit"].Visible = false;
                this.c1FlexGrid1.Cols["Incorporator"].Caption = "����";
                this.c1FlexGrid1.Cols["RegDate"].Caption = "ע������";
                this.c1FlexGrid1.Cols["RegDate"].Visible = false;
                this.c1FlexGrid1.Cols["DistrictCode"].Caption = "����";
                this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;
                this.c1FlexGrid1.Cols["DistrictName"].Caption = ShareOption.AreaTitle;
                this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;
                this.c1FlexGrid1.Cols["PostCode"].Caption = "�ʱ�";
                this.c1FlexGrid1.Cols["PostCode"].Visible = false;
                this.c1FlexGrid1.Cols["Address"].Caption = "��ַ";
                this.c1FlexGrid1.Cols["Address"].Visible = false;
                this.c1FlexGrid1.Cols["LinkMan"].Caption = "��ϵ��";
                this.c1FlexGrid1.Cols["LinkInfo"].Caption = "��ϵ��ʽ";
                this.c1FlexGrid1.Cols["CreditLevel"].Caption = "���õȼ�";
                this.c1FlexGrid1.Cols["CreditLevel"].Visible = false;
                this.c1FlexGrid1.Cols["CreditRecord"].Caption = "���ü�¼";
                this.c1FlexGrid1.Cols["CreditRecord"].Visible = false;
                this.c1FlexGrid1.Cols["ProductInfo"].Caption = "��Ʒ��Ϣ";
                this.c1FlexGrid1.Cols["ProductInfo"].Visible = false;
                this.c1FlexGrid1.Cols["OtherInfo"].Caption = "������Ϣ";
                //this.c1FlexGrid1.Cols["OtherInfo"].Visible = false;
                this.c1FlexGrid1.Cols["CheckLevel"].Caption = "��صȼ�";
                this.c1FlexGrid1.Cols["CheckLevel"].Visible = false;
                this.c1FlexGrid1.Cols["FoodSafeRecord"].Caption = "��ȫ��¼";//ʳƷ
                this.c1FlexGrid1.Cols["FoodSafeRecord"].Visible = false;
                this.c1FlexGrid1.Cols["IsLock"].Caption = "ͣ��";
                this.c1FlexGrid1.Cols["IsReadOnly"].Caption = "�����";
                this.c1FlexGrid1.Cols["Remark"].Caption = "��ע";
                this.c1FlexGrid1.Cols["Remark"].Visible = false;
                this.c1FlexGrid1.Cols["TSign"].Caption = "��ʾ";
                this.c1FlexGrid1.Cols["TSign"].Visible = false;
                this.c1FlexGrid1.Cols["ISSUEAGENCY"].Caption = "��֤����";
                this.c1FlexGrid1.Cols["ISSUEAGENCY"].Visible = false;
                this.c1FlexGrid1.Cols["ISSUEDATE"].Caption = "��֤����";
                this.c1FlexGrid1.Cols["ISSUEDATE"].Visible = false;
                this.c1FlexGrid1.Cols["PERIODSTART"].Caption = "��Ч��ʼ����";
                this.c1FlexGrid1.Cols["PERIODSTART"].Visible = false;
                this.c1FlexGrid1.Cols["PERIODEND"].Caption = "��Ч��ȡ����";
                this.c1FlexGrid1.Cols["PERIODEND"].Visible = false;
                this.c1FlexGrid1.Cols["VIOLATENUM"].Caption = "Υ�����";
                this.c1FlexGrid1.Cols["VIOLATENUM"].Visible = false;
                this.c1FlexGrid1.Cols["LONGITUDE"].Caption = "����";
                this.c1FlexGrid1.Cols["LONGITUDE"].Visible = false;
                this.c1FlexGrid1.Cols["LATITUDE"].Caption = "γ��";
                this.c1FlexGrid1.Cols["LATITUDE"].Visible = false;
                this.c1FlexGrid1.Cols["SCOPE"].Caption = "��ģ";
                this.c1FlexGrid1.Cols["SCOPE"].Visible = false;
                this.c1FlexGrid1.Cols["PUNISH"].Caption = "Υ�洦�����";
                this.c1FlexGrid1.Cols["PUNISH"].Visible = false;
                this.c1FlexGrid1.Cols["RegCapital"].Format = "#";
            }
            catch
            {
                MessageBox.Show(this, "��ػ�����Ϣû��������ȷ,�����ǻ�û�����ü�����Ϣ����û�����ú���֯������");
            }
        }

        private void setGridStyle2()
        {
            this.c1FlexGrid1.Cols["SysCode"].Caption = "ϵͳ���";
            this.c1FlexGrid1.Cols["StdCode"].Caption = "���";
            this.c1FlexGrid1.Cols["CompanyID"].Caption = "Ӫҵִ�պ�";
            this.c1FlexGrid1.Cols["FullName"].Caption = ShareOption.NameTitle;
            this.c1FlexGrid1.Cols["DisplayName"].Visible = true;
            this.c1FlexGrid1.Cols["KindName"].Visible = false;
            this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "���֤��";// "���֤��";
            if (tag.Equals("MAKE"))
            {
                //this.c1FlexGrid1.Cols["CompanyID"].Caption = "��λ����";
                this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "��׼����";// "���֤��";
            }
            this.c1FlexGrid1.Cols["ShortName"].Caption = "��λ���";
            this.c1FlexGrid1.Cols["DisplayName"].Caption = ShareOption.DomainTitle;
            this.c1FlexGrid1.Cols["ShortCut"].Caption = "��ݱ���";
            this.c1FlexGrid1.Cols["Property"].Caption = "��λ����";
            this.c1FlexGrid1.Cols["ComProperty"].Caption = "��������";
            this.c1FlexGrid1.Cols["KindCode"].Caption = "��λ������";
            this.c1FlexGrid1.Cols["KindName"].Caption = "��λ���";
            this.c1FlexGrid1.Cols["RegCapital"].Caption = "ע���ʽ�";
            this.c1FlexGrid1.Cols["Unit"].Caption = "�ʽ�λ";
            this.c1FlexGrid1.Cols["Incorporator"].Caption = "����";
            this.c1FlexGrid1.Cols["RegCapital"].Visible = false;
            this.c1FlexGrid1.Cols["Unit"].Visible = false;
            this.c1FlexGrid1.Cols["DistrictCode"].Caption = "����";
            this.c1FlexGrid1.Cols["DistrictName"].Caption = "ע�����";
            this.c1FlexGrid1.Cols["PostCode"].Caption = "�ʱ�";
            this.c1FlexGrid1.Cols["Address"].Caption = "��ַ";
            this.c1FlexGrid1.Cols["LinkMan"].Caption = "��ϵ��";
            this.c1FlexGrid1.Cols["LinkInfo"].Caption = "��ϵ��ʽ";
            this.c1FlexGrid1.Cols["CreditLevel"].Caption = "���õȼ�";
            this.c1FlexGrid1.Cols["CreditRecord"].Caption = "���ü�¼";
            this.c1FlexGrid1.Cols["ProductInfo"].Caption = "��Ʒ��Ϣ";
            this.c1FlexGrid1.Cols["OtherInfo"].Caption = "�������֤��";
            this.c1FlexGrid1.Cols["CheckLevel"].Caption = "��صȼ�";
            this.c1FlexGrid1.Cols["FoodSafeRecord"].Caption = "��ȫ��¼";//ʳƷ
            this.c1FlexGrid1.Cols["IsLock"].Caption = "ͣ��";
            this.c1FlexGrid1.Cols["IsReadOnly"].Caption = "�����";
            this.c1FlexGrid1.Cols["Remark"].Caption = "��ע";
            this.c1FlexGrid1.Cols["TSign"].Caption = "��ʾ";
            this.c1FlexGrid1.Cols["TSign"].Visible = false;
            this.c1FlexGrid1.Cols["StdCode"].Visible = false;
            this.c1FlexGrid1.Cols["SysCode"].Visible = false;
            this.c1FlexGrid1.Cols["KindCode"].Visible = false;
            this.c1FlexGrid1.Cols["ShortName"].Visible = false;
            this.c1FlexGrid1.Cols["ShortCut"].Visible = false;
            this.c1FlexGrid1.Cols["Property"].Visible = false;
            this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;
            this.c1FlexGrid1.Cols["DistrictName"].Visible = true;
            this.c1FlexGrid1.Cols["RegCapital"].Format = "#";
        }

        private void setGridStyle()
        {
            this.c1FlexGrid1.Cols["SysCode"].Caption = "ϵͳ���";
            this.c1FlexGrid1.Cols["CompanyID"].Caption = "��λ����";
            if (tag.Equals("CHECKED"))//�������ⵥλ
            {
                this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "���֤��";
                this.c1FlexGrid1.Cols["FullName"].Caption = "��λ����";
                this.c1FlexGrid1.Cols["DisplayName"].Caption = "����/����/���ƺ�";
                this.c1FlexGrid1.Cols["DisplayName"].Visible = true;
                this.c1FlexGrid1.Cols["KindName"].Visible = false;
            }
            else //������λ
            {
                this.c1FlexGrid1.Cols["CompanyID"].Caption = "Ӫҵִ�պ�";
                this.c1FlexGrid1.Cols["OtherCodeInfo"].Caption = "��׼����";
                //this.c1FlexGrid1.Cols["DisplayName"].Visible=false;
                this.c1FlexGrid1.Cols["KindName"].Visible = true;
                this.c1FlexGrid1.Cols["FullName"].Caption = "��λ����"; //"��λȫ��";
                this.c1FlexGrid1.Cols["DisplayName"].Caption = "��λ��Ϣ";
                this.c1FlexGrid1.Cols["CAllow"].Caption = "���֤��";
                //this.c1FlexGrid1.Cols["CAllow"].Visible = false;
                this.c1FlexGrid1.Cols["PUNISH"].Caption = "Υ�洦�����";
                this.c1FlexGrid1.Cols["PUNISH"].Visible = false;
                this.c1FlexGrid1.Cols["ISSUEAGENCY"].Caption = "��֤����";
                this.c1FlexGrid1.Cols["ISSUEAGENCY"].Visible = false;
                this.c1FlexGrid1.Cols["ISSUEDATE"].Caption = "��֤����";
                this.c1FlexGrid1.Cols["ISSUEDATE"].Visible = false;
                this.c1FlexGrid1.Cols["PERIODSTART"].Caption = "��Ч��ʼ����";
                this.c1FlexGrid1.Cols["PERIODSTART"].Visible = false;
                this.c1FlexGrid1.Cols["PERIODEND"].Caption = "��Ч��ȡ����";
                this.c1FlexGrid1.Cols["PERIODEND"].Visible = false;
                this.c1FlexGrid1.Cols["VIOLATENUM"].Caption = "Υ�����";
                this.c1FlexGrid1.Cols["VIOLATENUM"].Visible = false;
                this.c1FlexGrid1.Cols["LONGITUDE"].Caption = "����";
                this.c1FlexGrid1.Cols["LONGITUDE"].Visible = false;
                this.c1FlexGrid1.Cols["LATITUDE"].Caption = "γ��";
                this.c1FlexGrid1.Cols["LATITUDE"].Visible = false;
                this.c1FlexGrid1.Cols["SCOPE"].Caption = "��ģ";
                this.c1FlexGrid1.Cols["SCOPE"].Visible = false;
            }
            this.c1FlexGrid1.Cols["StdCode"].Caption = "���";
            this.c1FlexGrid1.Cols["ShortName"].Caption = "��λ���";
            this.c1FlexGrid1.Cols["ShortCut"].Caption = "��ݱ���";
            this.c1FlexGrid1.Cols["Property"].Caption = "��λ����";
            this.c1FlexGrid1.Cols["ComProperty"].Caption = "��������";
            this.c1FlexGrid1.Cols["KindCode"].Caption = "��λ������";
            this.c1FlexGrid1.Cols["KindName"].Caption = "��λ���";
            this.c1FlexGrid1.Cols["RegCapital"].Caption = "ע���ʽ�";
            this.c1FlexGrid1.Cols["Unit"].Caption = "�ʽ�λ";
            this.c1FlexGrid1.Cols["Incorporator"].Caption = "����";
            this.c1FlexGrid1.Cols["RegDate"].Caption = "ע������";
            this.c1FlexGrid1.Cols["DistrictCode"].Caption = "����";
            this.c1FlexGrid1.Cols["DistrictName"].Caption = "ע�����";
            this.c1FlexGrid1.Cols["PostCode"].Caption = "�ʱ�";
            this.c1FlexGrid1.Cols["Address"].Caption = "��ַ";
            this.c1FlexGrid1.Cols["LinkMan"].Caption = "��ϵ��";
            this.c1FlexGrid1.Cols["LinkInfo"].Caption = "��ϵ��ʽ";
            this.c1FlexGrid1.Cols["CreditLevel"].Caption = "���õȼ�";
            this.c1FlexGrid1.Cols["CreditRecord"].Caption = "���ü�¼";
            this.c1FlexGrid1.Cols["ProductInfo"].Caption = "��Ʒ��Ϣ";
            this.c1FlexGrid1.Cols["OtherInfo"].Caption = "������Ϣ";
            this.c1FlexGrid1.Cols["CheckLevel"].Caption = "��صȼ�";
            this.c1FlexGrid1.Cols["FoodSafeRecord"].Caption = "��ȫ��¼";//ʳƷ
            this.c1FlexGrid1.Cols["IsLock"].Caption = "ͣ��";
            this.c1FlexGrid1.Cols["IsReadOnly"].Caption = "�����";
            this.c1FlexGrid1.Cols["Remark"].Caption = "��ע";
            this.c1FlexGrid1.Cols["TSign"].Caption = "��ʾ";
            this.c1FlexGrid1.Cols["TSign"].Visible = false;
            this.c1FlexGrid1.Cols["SysCode"].Visible = false;
            this.c1FlexGrid1.Cols["StdCode"].Visible = false;
            c1FlexGrid1.Cols["DisplayName"].Visible = false;
            this.c1FlexGrid1.Cols["KindCode"].Visible = false;
            this.c1FlexGrid1.Cols["ShortName"].Visible = false;
            this.c1FlexGrid1.Cols["ShortCut"].Visible = false;
            this.c1FlexGrid1.Cols["Property"].Visible = false;
            this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;

            if (tag.Equals("CHECKED"))
            {
                this.c1FlexGrid1.Cols["DistrictName"].Visible = true;
            }
            else
            {
                this.c1FlexGrid1.Cols["DistrictName"].Visible = false;
            }
        }

        #region ��ʾ��ݷ�ʽ��ʾ����
        private void cmdAdd_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdAdd.Text);
        }

        private void cmdEdit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdEdit.Text);
        }

        private void cmdDelete_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdDelete.Text);
        }

        private void cmdPrint_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdPrint.Text);
        }

        private void cmdExit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdExit.Text);
        }

        private void cmdShow_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdShow.Text);
        }
        #endregion

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1Collapsed = false;
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }

            this.treeView1.SelectedNode = e.Node;
            string first = e.Node.Tag.ToString();
            NODE = e.Node.Text.Trim();
            parentAreaCode = string.Empty;
            kindCode = string.Empty;
            parentStdCode = string.Empty;
            DataTable dt = null;
            string conn = string.Empty;
            string where = string.Empty;
            if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
            {
                string first1st = first.Substring(0, 4);
                string first2st = first.Substring(4, first.Length - 4);
                string parenttag = string.Empty;
                string kindtag = string.Empty;
                if (first1st.Equals("XZJG"))
                {
                    where = string.Format("A.TSign<>'������' And A.DistrictCode Like '%{0}%' ", first2st, ShareOption.CompanyStdCodeLen.ToString());

                    dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
                    this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
                    this.setGridStyle1();
                    this.c1FlexGrid1.AutoSizeCols();
                    return;
                }
                else
                {
                    if (first1st.Equals("DWLX"))
                    {
                        parenttag = e.Node.Parent.Tag.ToString();
                        kindtag = e.Node.Tag.ToString();
                    }
                    if (first1st.Equals("DWST"))
                    {
                        parenttag = e.Node.Parent.Parent.Tag.ToString();
                        kindtag = e.Node.Parent.Tag.ToString();
                    }
                    kindCode = kindtag.Substring(4, kindtag.Length - 4);
                    parentAreaCode = parenttag.Substring(4, parenttag.Length - 4);
                    conn += "(A.DistrictCode like '" + parentAreaCode + "%')";
                    if (first1st.Equals("DWLX"))
                    {
                        //conn += " And Len(A.StdCode)=" + ShareOption.CompanyStdCodeLen.ToString();
                    }
                    if (first1st.Equals("DWST"))
                    {
                        parentStdCode = e.Node.Tag.ToString().Substring(4, e.Node.Tag.ToString().Length - 4);
                        conn += "  And A.StdCode Like '" + parentStdCode + StringUtil.RepeatChar('_', ShareOption.CompanyStdCodeLen) + "' ";
                    }
                    where = "A.TSign<>'������'" + "'";
                    where = " A.KindCode = '" + kindCode + "'";
                    where += " and " + conn;
                    dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
                    if (dt != null)
                    {
                        this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
                    }
                    if (first1st.Equals("DWLX"))
                    {
                        this.setGridStyle1();
                    }
                    if (first1st.Equals("DWST"))
                    {
                        this.setGridStyle2();
                    }
                    this.c1FlexGrid1.AutoSizeCols();
                }
            }
            else
            {
                parentStdCode = first.ToString();
                //where = string.Format("And A.Property='{0}' And A.StdCode Like '{1}_%' ", ShareOption.CompanyProperty1, parentStdCode);
                where = string.Format(" And A.StdCode Like '{}_%' ", parentStdCode);
                dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
                this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
                this.setGridStyle2();
                this.c1FlexGrid1.AutoSizeCols();
            }
        }

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            DataTable dt = null;
            string fullName = this.txtFullName.Text.Trim();
            string displayName = this.txtDisplayName.Text.Trim();
            if (fullName != "" || displayName != "")
            {
                string where = null;
                if (splitContainer1.Panel2Collapsed == false)
                {
                    clsProprietorsOpr ProBll = new clsProprietorsOpr();
                    DataTable ProTable = new DataTable();
                    if (CIIDname != "")
                    {
                        ProTable = ProBll.GetAsDataTable("Cdname  Like '%" + fullName + "%' and DisplayName  Like '%" + displayName + "%' and Ciname  Like '%" + CIIDname + "%'", "", 0);
                    }
                    if (CIIDname == "")
                    {
                        ProTable = ProBll.GetAsDataTable("Cdname  Like '%" + fullName + "%' and DisplayName  Like '%" + displayName + "%'", "", 0);
                    }
                    if (ProTable != null)
                    {
                        if (ProTable.Rows.Count <= 0)
                        {
                            BindCompaniesTosecond(ProTable);
                            splitContainer1.Panel1Collapsed = true;
                            splitContainer1.Panel2Collapsed = false;
                            return;
                        }
                        if (ProTable.Rows.Count > 0)
                        {
                            splitContainer1.Panel1Collapsed = true;
                            splitContainer1.Panel2Collapsed = false;
                            BindCompaniesTosecond(ProTable);
                        }
                    }
                }
                if (displayName != "" && fullName != "" && splitContainer1.Panel2Collapsed == true)
                {
                    if (tag.Equals("CHECKED"))
                    {
                        where += string.Format("A.FullName  Like '%{0}%' ", fullName);
                        where += string.Format("And A.TSign<>'������'  ");
                    }
                    dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
                    this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
                    this.setGridStyle();
                    this.c1FlexGrid1.AutoSizeCols();
                }
                if (fullName != "" && displayName == "" && splitContainer1.Panel2Collapsed == true)
                {
                    if (tag.Equals("CHECKED"))
                    {
                        where += string.Format("A.FullName  Like '%{0}%'  And Property Like '{1}'", fullName, "���쵥λ");
                        //where += string.Format("And A.TSign<>'������'  ");
                    }
                    else //if (this.Tag.ToString().Equals("MAKE"))
                    {
                        where += string.Format("A.FullName Like '%{0}%' ", fullName);
                        //where += string.Format("And A.TSign='������'");
                    }
                    dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
                    this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
                    this.setGridStyle1();
                    this.c1FlexGrid1.AutoSizeCols();
                }
            }
            else
            {
                if (tag.Equals("CHECKED"))
                {
                    MessageBox.Show("������Ҫ��ѯ�ĵ�λ���ƣ�");
                }
                else
                {
                    MessageBox.Show("������Ҫ��ѯ�ĵ�λ���ƣ�");
                }
            }
        }

        private void btnAllShow_Click(object sender, System.EventArgs e)
        {
            //this.treeView1.SelectedNode = null;
            DataTable dt = null;
            string fullName = string.Empty;
            string displayName = string.Empty;
            string where = string.Format("A.FullName Like '%{0}%' And A.DisplayName Like '%{1}%' ", fullName, displayName);
            if (tag.Equals("CHECKED"))
            {
                //���쵥λ��ѯ
                where += string.Format("And  A.Property like '���쵥λ'");
                //where += string.Format("And  A.TSign<>'������'  And A.StdCode Like '%{0}%' ", parentStdCode);
            }
            else //if (this.Tag.ToString().Equals("MAKE"))
            {
                //������λ��ѯ
                //where += string.Format("And A.TSign='������'  ");
            }
            dt = curObjectOpr.GetAsDataTable(where, "A.SysCode", 0);
            this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Company");
            this.setGridStyle1();
            this.c1FlexGrid1.AutoSizeCols();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1Collapsed = false;
            NODE = string.Empty;
        }

        private clsCompany getModel(int row)
        {
            if (row <= 0)
            {
                return null;
            }
            companyModel = new clsCompany();
            companyModel.SysCode = this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
            companyModel.StdCode = this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
            companyModel.CompanyID = this.c1FlexGrid1.Rows[row]["CompanyID"].ToString();
            companyModel.CAllow = this.c1FlexGrid1.Rows[row]["CAllow"].ToString();
            companyModel.ISSUEAGENCY = this.c1FlexGrid1.Rows[row]["ISSUEAGENCY"].ToString();
            companyModel.ISSUEDATE = this.c1FlexGrid1.Rows[row]["ISSUEDATE"].ToString();
            companyModel.PERIODSTART = this.c1FlexGrid1.Rows[row]["PERIODSTART"].ToString();
            companyModel.PERIODEND = this.c1FlexGrid1.Rows[row]["PERIODEND"].ToString();
            companyModel.VIOLATENUM = this.c1FlexGrid1.Rows[row]["VIOLATENUM"].ToString();
            companyModel.LONGITUDE = this.c1FlexGrid1.Rows[row]["LONGITUDE"].ToString();
            companyModel.LATITUDE = this.c1FlexGrid1.Rows[row]["LATITUDE"].ToString();
            companyModel.SCOPE = this.c1FlexGrid1.Rows[row]["SCOPE"].ToString();
            companyModel.PUNISH = this.c1FlexGrid1.Rows[row]["PUNISH"].ToString();
            companyModel.OtherCodeInfo = this.c1FlexGrid1.Rows[row]["OtherCodeInfo"].ToString();
            companyModel.FullName = this.c1FlexGrid1.Rows[row]["FullName"].ToString();
            companyModel.ShortName = this.c1FlexGrid1.Rows[row]["ShortName"].ToString();
            companyModel.DisplayName = this.c1FlexGrid1.Rows[row]["DisplayName"].ToString();
            companyModel.ShortCut = this.c1FlexGrid1.Rows[row]["ShortCut"].ToString();
            companyModel.Property = this.c1FlexGrid1.Rows[row]["Property"].ToString();
            companyModel.KindCode = this.c1FlexGrid1.Rows[row]["KindCode"].ToString();
            companyModel.RegCapital = Convert.ToInt64(this.c1FlexGrid1.Rows[row]["RegCapital"]);
            companyModel.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
            companyModel.Incorporator = this.c1FlexGrid1.Rows[row]["Incorporator"].ToString();
            if (c1FlexGrid1.Rows[row]["RegDate"] != null && c1FlexGrid1.Rows[row]["RegDate"] != DBNull.Value)
            {
                companyModel.RegDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["RegDate"]);
            }
            companyModel.DistrictCode = this.c1FlexGrid1.Rows[row]["DistrictCode"].ToString();
            companyModel.Address = this.c1FlexGrid1.Rows[row]["Address"].ToString();
            companyModel.PostCode = this.c1FlexGrid1.Rows[row]["PostCode"].ToString();
            companyModel.LinkMan = this.c1FlexGrid1.Rows[row]["LinkMan"].ToString();
            companyModel.LinkInfo = this.c1FlexGrid1.Rows[row]["LinkInfo"].ToString();
            companyModel.CreditLevel = this.c1FlexGrid1.Rows[row]["CreditLevel"].ToString();
            companyModel.CreditRecord = this.c1FlexGrid1.Rows[row]["CreditRecord"].ToString();
            companyModel.ProductInfo = this.c1FlexGrid1.Rows[row]["ProductInfo"].ToString();
            companyModel.OtherInfo = this.c1FlexGrid1.Rows[row]["OtherInfo"].ToString();
            companyModel.CheckLevel = this.c1FlexGrid1.Rows[row]["CheckLevel"].ToString();
            companyModel.FoodSafeRecord = this.c1FlexGrid1.Rows[row]["FoodSafeRecord"].ToString();
            companyModel.Remark = this.c1FlexGrid1.Rows[row]["Remark"].ToString();
            companyModel.ComProperty = this.c1FlexGrid1.Rows[row]["ComProperty"].ToString();
            companyModel.IsLock = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
            companyModel.IsReadOnly = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsReadOnly"]);
            return companyModel;
        }
        private clsProprietors NAmodel(int row)
        {
            if (row <= 0)
            {
                return null;
            }
            clsProprietors proMode = new clsProprietors();
            proMode.Cdbuslicence = this.c1FlexGrid02.Rows[row]["colCdbuslicence"].ToString();
            //proMode.Ciid = this.c1FlexGrid02.Rows[row]["Ciid"].ToString();
            proMode.Cdcode = this.c1FlexGrid02.Rows[row]["colCdcode"].ToString();
            proMode.CAllow = this.c1FlexGrid02.Rows[row]["colCAllow"].ToString();
            proMode.Ciname = this.c1FlexGrid02.Rows[row]["colCiname"].ToString();
            proMode.Cdname = this.c1FlexGrid02.Rows[row]["colCdname"].ToString();
            proMode.Cdcardid = this.c1FlexGrid02.Rows[row]["colCdcardid"].ToString();
            proMode.DisplayName = this.c1FlexGrid02.Rows[row]["colDisplayName"].ToString();
            //proMode.Property = this.c1FlexGrid02.Rows[row]["colProperty"].ToString();
            //proMode.KindCode = this.c1FlexGrid02.Rows[row]["colKindCode"].ToString();
            proMode.RegCapital = this.c1FlexGrid02.Rows[row]["colRegCapital"].ToString();
            proMode.Unit = this.c1FlexGrid02.Rows[row]["colUnit"].ToString();
            proMode.Incorporator = this.c1FlexGrid02.Rows[row]["colIncorporator"].ToString();
            if (c1FlexGrid02.Rows[row]["colRegDate"] != null && c1FlexGrid02.Rows[row]["colRegDate"] != DBNull.Value)
            {
                proMode.RegDate = Convert.ToDateTime(this.c1FlexGrid02.Rows[row]["colRegDate"]);
            }
            //proMode.DistrictCode = this.c1FlexGrid02.Rows[row]["colDistrictCode"].ToString();
            proMode.PostCode = this.c1FlexGrid02.Rows[row]["colPostCode"].ToString();
            proMode.Address = this.c1FlexGrid02.Rows[row]["colAddress"].ToString();
            proMode.LinkMan = this.c1FlexGrid02.Rows[row]["colLinkMan"].ToString();
            proMode.LinkInfo = this.c1FlexGrid02.Rows[row]["colLinkInfo"].ToString();
            proMode.CreditLevel = this.c1FlexGrid02.Rows[row]["colCreditLevel"].ToString();
            proMode.CreditRecord = this.c1FlexGrid02.Rows[row]["colCreditRecord"].ToString();
            proMode.ProductInfo = this.c1FlexGrid02.Rows[row]["colProductInfo"].ToString();
            proMode.OtherInfo = this.c1FlexGrid02.Rows[row]["colOtherInfo"].ToString();
            proMode.CheckLevel = this.c1FlexGrid02.Rows[row]["colCheckLevel"].ToString();
            proMode.FoodSafeRecord = this.c1FlexGrid02.Rows[row]["colFoodSafeRecord"].ToString();
            //proMode.IsLock = Convert.ToBoolean(this.c1FlexGrid02.Rows[row]["colIsLock"]);
            proMode.IsReadOnly = Convert.ToBoolean(this.c1FlexGrid02.Rows[row]["colIsReadOnly"]);
            proMode.Remark = this.c1FlexGrid02.Rows[row]["colRemark"].ToString();
            return proMode;
        }
        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }

            string distCode = string.Empty;
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null)
            {
                distCode = treeView1.SelectedNode.Tag.ToString();
            }
            clsCompany model = getModel(row);
            frmCheckedComEdit frm = new frmCheckedComEdit(model, distCode);
            if (tag.Equals("CHECKED"))
            {
                frm.Tag = "CHECKEDEDIT";
            }
            else if (tag.Equals("MAKE"))
            {
                frm.Tag = "MAKEEDIT";
            }
            DialogResult dr = frm.ShowDialog(this);

            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                if (tag.Equals("CHECKED"))
                {
                    refreshGridPart();
                }
                else
                {
                    refreshGrid(treeView1.SelectedNode);
                }
            }
        }

        private void cmdShow_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (splitContainer1.Panel2Collapsed == false && splitContainer1.Panel1Collapsed == true)
            {
                int row = this.c1FlexGrid02.RowSel;
                if (row <= 0)
                {
                    return;
                }
                clsProprietors model = NAmodel(row);
                FrmProprietorsDetail frm = new FrmProprietorsDetail();
                frm.GetValue(model);
                frm.ShowDialog(this);

            }
            else
            {
                int row = this.c1FlexGrid1.RowSel;
                if (row <= 0)
                {
                    return;
                }
                string distCode = string.Empty;
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null)
                {
                    distCode = treeView1.SelectedNode.Tag.ToString();
                }
                clsCompany model = getModel(row);
                frmCheckedComEdit frm = new frmCheckedComEdit(model, distCode);
                if (tag.Equals("CHECKED"))
                {
                    frm.Tag = "CHECKEDEDIT";
                }
                else if (tag.Equals("MAKE"))
                {
                    frm.Tag = "MAKEEDIT";
                }
                DialogResult dr = frm.ShowDialog(this);

                //ˢ�´����е�Grid
                if (dr == DialogResult.OK)
                {
                    if (tag.Equals("CHECKED"))
                    {
                        refreshGridPart();
                    }
                    else
                    {
                        refreshGrid(this.treeView1.SelectedNode);
                    }
                }
            }
        }

        private void BindCompaniesTosecond(DataTable companies)
        {
            if (c1FlexGrid02.Rows.Count > 0)
            {
                c1FlexGrid02.Rows.RemoveRange(1, c1FlexGrid02.Rows.Count - 1);
            }
            if (companies == null)
            {
                return;
            }
            for (int rowIndex = 0; rowIndex < companies.Rows.Count; rowIndex++)
            {
                c1FlexGrid02.AddItem(new object[] { companies.Rows[rowIndex]["Cdcode"].ToString(),
                companies.Rows[rowIndex]["Cdname"].ToString(),
				companies.Rows[rowIndex]["Incorporator"].ToString(),
				companies.Rows[rowIndex]["DisplayName"].ToString(),
				companies.Rows[rowIndex]["Ciname"].ToString(),
                companies.Rows[rowIndex]["Cdbuslicence"].ToString(),
                companies.Rows[rowIndex]["CAllow"].ToString(),
                companies.Rows[rowIndex]["Cdcardid"].ToString(),
                companies.Rows[rowIndex]["RegCapital"].ToString(),
                companies.Rows[rowIndex]["Unit"].ToString(),
                companies.Rows[rowIndex]["RegDate"].ToString (),
                companies.Rows[rowIndex]["PostCode"].ToString(),
                companies.Rows[rowIndex]["Address"].ToString(),
                companies.Rows[rowIndex]["LinkMan"].ToString(),
                companies.Rows[rowIndex]["LinkInfo"].ToString(),
                companies.Rows[rowIndex]["CreditLevel"].ToString(),
                companies.Rows[rowIndex]["CreditRecord"].ToString(),
                companies.Rows[rowIndex]["ProductInfo"].ToString(),
                companies.Rows[rowIndex]["OtherInfo"].ToString(),
                companies.Rows[rowIndex]["CheckLevel"].ToString(),
                companies.Rows[rowIndex]["FoodSafeRecord"].ToString(),
                companies.Rows[rowIndex]["IsReadOnly"].ToString(),
                companies.Rows[rowIndex]["Remark"].ToString()});
                C1.Win.C1FlexGrid.CellStyle unselectedStyle = c1FlexGrid02.Styles.Add("unselectedStyle");
                unselectedStyle.BackColor = Color.White;
                unselectedStyle.ForeColor = Color.Black;
                c1FlexGrid02.Rows[rowIndex + 1].Style = unselectedStyle;
                c1FlexGrid02.Refresh();
            }
            c1FlexGrid02.AutoSizeCols();
        }

        private void c1FlexGrid02_Click(object sender, EventArgs e)
        {
            int rowIndex = c1FlexGrid02.RowSel;
            if (rowIndex <= 0)
            {
                return;
            }

            if (c1FlexGrid02.Rows[rowIndex].Style.BackColor == Color.Black)
            {
                c1FlexGrid02.Styles.Highlight.ForeColor = Color.Black;
            }
            else
            {
                c1FlexGrid02.Styles.Highlight.ForeColor = Color.Red;
            }
        }

        private void btnDatailQuer_Click(object sender, EventArgs e)
        {
            string SNIDENAME = string.Empty;
            string SNIDETAG = string.Empty;
            int rowIndex = c1FlexGrid1.RowSel;
            if (rowIndex <= 0)
                return;

            SNIDENAME = c1FlexGrid1.Rows[rowIndex]["FullName"].ToString();
            string sqlWhere = string.Format("Ciname='{0}'", SNIDENAME);
            clsProprietorsOpr ProBll = new clsProprietorsOpr();
            DataTable ProTable = ProBll.GetAsDataTable(sqlWhere, "", 0);
            if (ProTable.Rows.Count <= 0)
            {
                BindCompaniesTosecond(ProTable);
                splitContainer1.Panel2Collapsed = true;

                return;
            }
            if (ProTable.Rows.Count > 0)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel1Collapsed = true;
                BindCompaniesTosecond(ProTable);
            }
            CIIDname = c1FlexGrid1.Rows[rowIndex]["FullName"].ToString();
        }

        private void c1FlexGrid1_DoubleClick_1(object sender, EventArgs e)
        {
            string SNIDENAME = string.Empty;
            string SNIDETAG = string.Empty;

            int rowIndex = c1FlexGrid1.RowSel;
            if (rowIndex <= 0)
            {
                return;
            }
            SNIDENAME = c1FlexGrid1.Rows[rowIndex]["FullName"].ToString();
            string sqlWhere = string.Format("Ciname='{0}'", SNIDENAME);
            clsProprietorsOpr ProBll = new clsProprietorsOpr();
            DataTable ProTable = ProBll.GetAsDataTable(sqlWhere, "", 0);
            if (ProTable.Rows.Count <= 0)
            {
                BindCompaniesTosecond(ProTable);
                splitContainer1.Panel2Collapsed = true;
                //c1FlexGrid02.Refresh();
                return;
            }
            if (ProTable.Rows.Count > 0)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel1Collapsed = true;
                BindCompaniesTosecond(ProTable);
            }
            CIIDname = c1FlexGrid1.Rows[rowIndex]["FullName"].ToString();
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1Collapsed = false;
        }
    }
}
