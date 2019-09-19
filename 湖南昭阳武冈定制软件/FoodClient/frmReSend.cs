using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmReSend ��ժҪ˵����
	/// </summary>
    public class frmReSend : System.Windows.Forms.Form
	{
		private C1.Win.C1Command.C1Command cmdDelete;
		private C1.Win.C1Command.C1Command c1Command5;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1Command cmdEdit;
		private C1.Win.C1Command.C1Command cmdPrint;
		private C1.Win.C1Command.C1CommandLink c1CommandLink6;
		private C1.Win.C1Command.C1Command cmdExit;
		private C1.Win.C1Command.C1CommandLink c1CommandLink5;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1CommandDock c1CommandDock1;
		private C1.Win.C1Command.C1ToolBar c1ToolBar1;
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		public C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		private C1.Win.C1Command.C1Command c1Command1;
		private C1.Win.C1Command.C1Command cmdQuery;
		private C1.Win.C1Command.C1Command cmdSend;
		private C1.Win.C1Command.C1CommandLink c1CommandLink7;
		private C1.Win.C1Command.C1CommandLink c1CommandLink8;
		private C1.Win.C1Command.C1CommandLink c1CommandLink9;
		private C1.Win.C1Command.C1CommandLink c1CommandLink10;
		private C1.Win.C1Command.C1CommandMenu c1CommandMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink12;
        private C1.Win.C1Command.C1Command cmdAdd01;
		private C1.Win.C1Command.C1CommandLink c1CommandLink13;
		public C1.Win.C1Command.C1Command cmdAdd02;
		private System.Windows.Forms.Panel pnlSended;
		private System.Windows.Forms.Panel pnlNoEligible;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.ToolTip toolTip1;
		private C1.Win.C1Command.C1Command c1Command3;
		private C1.Win.C1Command.C1Command c1Command4;
		private C1.Win.C1Command.C1Command c1Command6;
		private C1.Win.C1Command.C1Command c1Command7;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu2;
		private C1.Win.C1Command.C1CommandLink c1CommandLink14;
        private C1.Win.C1Command.C1CommandLink c1CommandLink15;
		private C1.Win.C1Report.C1Report c1Report1;
		private System.Windows.Forms.Label lblPassed;
		private System.Windows.Forms.Label lblUnPass;
		private System.Windows.Forms.Label lblUnsend;
		private System.Windows.Forms.Label lblSended;
		private C1.Win.C1Command.C1Command cmdShowAll;
		private C1.Win.C1Command.C1CommandLink c1CommandLink17;
		private C1.Win.C1Command.C1CommandLink c1CommandLink18;
		private C1.Win.C1Command.C1Command c1Command2;
		private C1.Win.C1Command.C1CommandLink c1CommandLink19;
		private System.Windows.Forms.Label lblRecordSum;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        /// <summary>
        /// δ�ϴ��ϸ�
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle styleNormal = null;
        /// <summary>
        /// δ�ϴ����ϸ�
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style1;

        /// <summary>
        /// �Ѿ��ϴ����ϸ�
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style2 = null;

        /// <summary>
        /// �Ѿ��ϴ��ϸ�
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style3;
        private clsResult model;
        private readonly clsResultOpr resultBll;
        private string queryString;

        /// <summary>
        /// ���캯��
        /// </summary>
        public frmReSend()
        {
            InitializeComponent();

            resultBll = new clsResultOpr();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReSend));
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.cmdAdd01 = new C1.Win.C1Command.C1Command();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.c1Command1 = new C1.Win.C1Command.C1Command();
            this.cmdQuery = new C1.Win.C1Command.C1Command();
            this.cmdSend = new C1.Win.C1Command.C1Command();
            this.c1CommandMenu1 = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink12 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink13 = new C1.Win.C1Command.C1CommandLink();
            this.cmdAdd02 = new C1.Win.C1Command.C1Command();
            this.c1Command3 = new C1.Win.C1Command.C1Command();
            this.c1Command4 = new C1.Win.C1Command.C1Command();
            this.c1Command6 = new C1.Win.C1Command.C1Command();
            this.c1Command7 = new C1.Win.C1Command.C1Command();
            this.c1CommandMenu2 = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink14 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink15 = new C1.Win.C1Command.C1CommandLink();
            this.cmdShowAll = new C1.Win.C1Command.C1Command();
            this.c1Command2 = new C1.Win.C1Command.C1Command();
            this.c1CommandLink19 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink8 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink10 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink17 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink18 = new C1.Win.C1Command.C1CommandLink();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUnsend = new System.Windows.Forms.Label();
            this.lblSended = new System.Windows.Forms.Label();
            this.lblUnPass = new System.Windows.Forms.Label();
            this.lblRecordSum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSended = new System.Windows.Forms.Panel();
            this.pnlNoEligible = new System.Windows.Forms.Panel();
            this.lblPassed = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.c1Report1 = new C1.Win.C1Report.C1Report();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "ɾ��";
            this.cmdDelete.Visible = false;
            this.cmdDelete.Select += new System.EventHandler(this.cmdDelete_Select);
            // 
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.cmdDelete;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.cmdEdit.Text = "�޸�";
            this.cmdEdit.Visible = false;
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "��ӡ���м�¼";
            this.cmdPrint.Visible = false;
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.Command = this.cmdExit;
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
            // c1CommandLink5
            // 
            this.c1CommandLink5.Command = this.cmdPrint;
            // 
            // cmdAdd01
            // 
            this.cmdAdd01.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd01.Image")));
            this.cmdAdd01.Name = "cmdAdd01";
            this.cmdAdd01.Text = "����ϴ���־";
            this.cmdAdd01.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd01_Click);
            this.cmdAdd01.Select += new System.EventHandler(this.cmdAdd01_Select);
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(632, 24);
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink19,
            this.c1CommandLink1,
            this.c1CommandLink2,
            this.c1CommandLink3,
            this.c1CommandLink7,
            this.c1CommandLink8,
            this.c1CommandLink10,
            this.c1CommandLink9,
            this.c1CommandLink5,
            this.c1CommandLink17,
            this.c1CommandLink18,
            this.c1CommandLink4,
            this.c1CommandLink6});
            this.c1ToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.ShowToolTips = false;
            this.c1ToolBar1.Size = new System.Drawing.Size(632, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.cmdAdd01);
            this.c1CommandHolder1.Commands.Add(this.cmdEdit);
            this.c1CommandHolder1.Commands.Add(this.cmdDelete);
            this.c1CommandHolder1.Commands.Add(this.c1Command1);
            this.c1CommandHolder1.Commands.Add(this.cmdQuery);
            this.c1CommandHolder1.Commands.Add(this.cmdSend);
            this.c1CommandHolder1.Commands.Add(this.c1Command5);
            this.c1CommandHolder1.Commands.Add(this.cmdPrint);
            this.c1CommandHolder1.Commands.Add(this.cmdExit);
            this.c1CommandHolder1.Commands.Add(this.c1CommandMenu1);
            this.c1CommandHolder1.Commands.Add(this.cmdAdd02);
            this.c1CommandHolder1.Commands.Add(this.c1Command3);
            this.c1CommandHolder1.Commands.Add(this.c1Command4);
            this.c1CommandHolder1.Commands.Add(this.c1Command6);
            this.c1CommandHolder1.Commands.Add(this.c1Command7);
            this.c1CommandHolder1.Commands.Add(this.c1CommandMenu2);
            this.c1CommandHolder1.Commands.Add(this.cmdShowAll);
            this.c1CommandHolder1.Commands.Add(this.c1Command2);
            this.c1CommandHolder1.Owner = this;
            this.c1CommandHolder1.SmoothImages = false;
            // 
            // c1Command1
            // 
            this.c1Command1.Name = "c1Command1";
            this.c1Command1.Text = "-";
            // 
            // cmdQuery
            // 
            this.cmdQuery.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuery.Image")));
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Text = "��ѯ";
            this.cmdQuery.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdQuery_Click);
            this.cmdQuery.Select += new System.EventHandler(this.cmdQuery_Select);
            // 
            // cmdSend
            // 
            this.cmdSend.Image = ((System.Drawing.Image)(resources.GetObject("cmdSend.Image")));
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Text = "�ϴ�";
            this.cmdSend.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdSend_Click);
            this.cmdSend.Select += new System.EventHandler(this.cmdSend_Select);
            // 
            // c1CommandMenu1
            // 
            this.c1CommandMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink12,
            this.c1CommandLink13});
            this.c1CommandMenu1.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu1.Image")));
            this.c1CommandMenu1.Name = "c1CommandMenu1";
            this.c1CommandMenu1.Text = "����";
            this.c1CommandMenu1.Visible = false;
            this.c1CommandMenu1.Select += new System.EventHandler(this.c1CommandMenu1_Select);
            // 
            // c1CommandLink12
            // 
            this.c1CommandLink12.Command = this.cmdAdd01;
            this.c1CommandLink12.Text = "������ֹ�¼��";
            // 
            // c1CommandLink13
            // 
            this.c1CommandLink13.Command = this.cmdAdd02;
            this.c1CommandLink13.Text = "���ٿ��ֹ�¼��";
            // 
            // cmdAdd02
            // 
            this.cmdAdd02.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd02.Image")));
            this.cmdAdd02.Name = "cmdAdd02";
            this.cmdAdd02.Text = "��ϸ";
            this.cmdAdd02.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd02_Click);
            this.cmdAdd02.Select += new System.EventHandler(this.cmdAdd02_Select);
            // 
            // c1Command3
            // 
            this.c1Command3.Image = ((System.Drawing.Image)(resources.GetObject("c1Command3.Image")));
            this.c1Command3.Name = "c1Command3";
            this.c1Command3.Text = "ת��ΪWord";
            // 
            // c1Command4
            // 
            this.c1Command4.Image = ((System.Drawing.Image)(resources.GetObject("c1Command4.Image")));
            this.c1Command4.Name = "c1Command4";
            this.c1Command4.Text = "ת��ΪExcel";
            // 
            // c1Command6
            // 
            this.c1Command6.Image = ((System.Drawing.Image)(resources.GetObject("c1Command6.Image")));
            this.c1Command6.Name = "c1Command6";
            this.c1Command6.Text = "ת��ΪXML";
            // 
            // c1Command7
            // 
            this.c1Command7.Image = ((System.Drawing.Image)(resources.GetObject("c1Command7.Image")));
            this.c1Command7.Name = "c1Command7";
            this.c1Command7.Text = "ת��Ϊ��Ԫ���ݸ�ʽ";
            // 
            // c1CommandMenu2
            // 
            this.c1CommandMenu2.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink14,
            this.c1CommandLink15});
            this.c1CommandMenu2.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu2.Image")));
            this.c1CommandMenu2.Name = "c1CommandMenu2";
            this.c1CommandMenu2.Text = "ת��";
            this.c1CommandMenu2.Visible = false;
            this.c1CommandMenu2.Select += new System.EventHandler(this.c1CommandMenu2_Select);
            // 
            // c1CommandLink14
            // 
            this.c1CommandLink14.Command = this.c1Command4;
            // 
            // c1CommandLink15
            // 
            this.c1CommandLink15.Command = this.c1Command6;
            // 
            // cmdShowAll
            // 
            this.cmdShowAll.Icon = ((System.Drawing.Icon)(resources.GetObject("cmdShowAll.Icon")));
            this.cmdShowAll.Name = "cmdShowAll";
            this.cmdShowAll.Text = "��ʾ��������";
            this.cmdShowAll.ToolTipText = "��ʾ��������";
            this.cmdShowAll.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdShowAll_Click);
            this.cmdShowAll.Select += new System.EventHandler(this.cmdShowAll_Select);
            // 
            // c1Command2
            // 
            this.c1Command2.Image = ((System.Drawing.Image)(resources.GetObject("c1Command2.Image")));
            this.c1Command2.Name = "c1Command2";
            this.c1Command2.Text = "��ӡ��ⵥ";
            this.c1Command2.Visible = false;
            this.c1Command2.Select += new System.EventHandler(this.c1Command2_Select);
            // 
            // c1CommandLink19
            // 
            this.c1CommandLink19.Command = this.cmdAdd02;
            this.c1CommandLink19.Text = "��ϸ";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdAdd01;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.cmdEdit;
            this.c1CommandLink2.Text = "�����ش�";
            // 
            // c1CommandLink7
            // 
            this.c1CommandLink7.Command = this.c1Command1;
            // 
            // c1CommandLink8
            // 
            this.c1CommandLink8.Command = this.cmdQuery;
            // 
            // c1CommandLink10
            // 
            this.c1CommandLink10.Command = this.cmdSend;
            // 
            // c1CommandLink9
            // 
            this.c1CommandLink9.Command = this.c1CommandMenu2;
            // 
            // c1CommandLink17
            // 
            this.c1CommandLink17.Command = this.cmdShowAll;
            // 
            // c1CommandLink18
            // 
            this.c1CommandLink18.Command = this.c1Command2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1FlexGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 301);
            this.panel1.TabIndex = 10;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AutoResize = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(632, 301);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 1;
            this.c1FlexGrid1.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid1_AfterSort);
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUnsend);
            this.panel2.Controls.Add(this.lblSended);
            this.panel2.Controls.Add(this.lblUnPass);
            this.panel2.Controls.Add(this.lblRecordSum);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pnlSended);
            this.panel2.Controls.Add(this.pnlNoEligible);
            this.panel2.Controls.Add(this.lblPassed);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 32);
            this.panel2.TabIndex = 11;
            // 
            // lblUnsend
            // 
            this.lblUnsend.Location = new System.Drawing.Point(405, 9);
            this.lblUnsend.Name = "lblUnsend";
            this.lblUnsend.Size = new System.Drawing.Size(82, 17);
            this.lblUnsend.TabIndex = 13;
            this.lblUnsend.Text = "���ϴ���";
            this.lblUnsend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSended
            // 
            this.lblSended.Location = new System.Drawing.Point(306, 9);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(82, 17);
            this.lblSended.TabIndex = 12;
            this.lblSended.Text = "���ϴ���";
            this.lblSended.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnPass
            // 
            this.lblUnPass.Location = new System.Drawing.Point(207, 9);
            this.lblUnPass.Name = "lblUnPass";
            this.lblUnPass.Size = new System.Drawing.Size(82, 17);
            this.lblUnPass.TabIndex = 11;
            this.lblUnPass.Text = "���ϸ���";
            this.lblUnPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordSum
            // 
            this.lblRecordSum.Location = new System.Drawing.Point(9, 9);
            this.lblRecordSum.Name = "lblRecordSum";
            this.lblRecordSum.Size = new System.Drawing.Size(82, 17);
            this.lblRecordSum.TabIndex = 10;
            this.lblRecordSum.Text = "��¼����";
            this.lblRecordSum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(582, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "���ϴ�";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(511, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "���ϸ�";
            // 
            // pnlSended
            // 
            this.pnlSended.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlSended.Location = new System.Drawing.Point(569, 11);
            this.pnlSended.Name = "pnlSended";
            this.pnlSended.Size = new System.Drawing.Size(13, 12);
            this.pnlSended.TabIndex = 7;
            // 
            // pnlNoEligible
            // 
            this.pnlNoEligible.BackColor = System.Drawing.Color.Red;
            this.pnlNoEligible.Location = new System.Drawing.Point(497, 12);
            this.pnlNoEligible.Name = "pnlNoEligible";
            this.pnlNoEligible.Size = new System.Drawing.Size(13, 12);
            this.pnlNoEligible.TabIndex = 6;
            // 
            // lblPassed
            // 
            this.lblPassed.Location = new System.Drawing.Point(108, 9);
            this.lblPassed.Name = "lblPassed";
            this.lblPassed.Size = new System.Drawing.Size(82, 17);
            this.lblPassed.TabIndex = 5;
            this.lblPassed.Text = "�ϸ���";
            this.lblPassed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "WorkSheetReport";
            // 
            // frmReSend
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(632, 357);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��������ش�����";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReSend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private void cmdQuery_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            frmResultQuery query = new frmResultQuery(3);//query�������
            query.cmbIsSend.Enabled = false;
            DialogResult dr = query.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                this.queryString = query.QueryString;
                this.refreshGrid(this.queryString);
            }
        }

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
		}

        private void refreshGrid(string queryStr)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(queryStr))
            {
                sb.Append(queryStr);
                sb.Append(" AND( ");
            }
            sb.Append("ResultType<>'' AND IsSended=true");

            if (!string.IsNullOrEmpty(queryStr))
            {
                sb.Append(" ) ");
            }
            string q = sb.ToString();


            DataTable dt = resultBll.GetAsReSendDataTable(q, "A.CheckStartDate Desc");
            this.c1FlexGrid1.SetDataBinding(dt.DataSet, "Result");

            this.lblRecordSum.Text = "��¼����:" + (c1FlexGrid1.Rows.Count - 1).ToString();
            string sError = string.Empty;

            string queryStr2 = string.Format("({0}) AND Result='{1}'", q, ShareOption.ResultEligi);
            int iPassCount = resultBll.GetRecCount(queryStr2, out sError);

            queryStr2 = string.Format("({0}) AND IsSended=True", q);
            int iSendedCount = resultBll.GetRecCount(queryStr2, out sError);


            this.lblPassed.Text = "�ϸ���:" + iPassCount.ToString();
            this.lblUnPass.Text = "���ϸ���:" + (c1FlexGrid1.Rows.Count - 1 - iPassCount).ToString();
            this.lblSended.Text = "���ϴ���:" + iSendedCount.ToString();

            setRowStyle();
            //for (int i = 1; i < this.c1FlexGrid1.Rows.Count; i++)
            //{

            //    if (c1FlexGrid1.Rows[i]["Result"].ToString().Equals(ShareOption.Result0))
            //    {
            //        c1FlexGrid1.Rows[i].Style = style1;
            //    }
            //    else
            //    {
            //        if (Convert.ToBoolean(this.c1FlexGrid1.Rows[i]["IsSended"]))
            //        {
            //            this.c1FlexGrid1.Rows[i].Style = style2;
            //        }
            //        else
            //        {
            //            this.c1FlexGrid1.Rows[i].Style = styleNormal;
            //        }
            //    }
            //}
     
            setGridStyle();

            c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.AutoSizeCols();
        }
        private void setRowStyle()
        {
            if (c1FlexGrid1.Rows.Count > 1)
            {
                bool send = false;
                for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                {
                    send = Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsSended"]);
                    if (c1FlexGrid1.Rows[i]["Result"].ToString().Equals(ShareOption.ResultFailure))
                    {
                        if (send)
                        {
                            c1FlexGrid1.Rows[i].Style = style2;
                        }
                        else
                        {
                            c1FlexGrid1.Rows[i].Style = style1;
                        }
                        c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
                    }
                    else //�ϸ�
                    {
                        c1FlexGrid1.Styles.Highlight.ForeColor = Color.Black;
                        if (send)//�Ѿ��ϴ�
                        {
                            this.c1FlexGrid1.Rows[i].Style = style3;
                        }
                        else
                        {
                            this.c1FlexGrid1.Rows[i].Style = styleNormal;
                        }
                    }
                }
            }
        }
        private void setGridStyle()
        {
             //c1FlexGrid1.Cols.Count = 68;
            //�����ֶ�
            //c1FlexGrid1.Cols["HolesNum"].Caption = "��λ/ͨ����";
            //c1FlexGrid1.Cols["MachineSampleNum"].Caption = "���������";

            c1FlexGrid1.Cols["SysCode"].Caption = "ϵͳ���";
            c1FlexGrid1.Cols["CheckNo"].Caption = "�����";
            c1FlexGrid1.Cols["CheckedCompany"].Caption = "���쵥λ����";
            //c1FlexGrid1.Cols["CheckedCompanyName"].Caption = "�ܼ���/��λ";
            //c1FlexGrid1.Cols["CheckedComDis"].Caption = "����/����/���ƺ�";
            c1FlexGrid1.Cols["CheckedCompanyName"].Caption = ShareOption.NameTitle;
            c1FlexGrid1.Cols["CheckedComDis"].Caption = ShareOption.DomainTitle;
            c1FlexGrid1.Cols["UpperCompany"].Caption = "�����г�����";
            //c1FlexGrid1.Cols["UpperCompanyName"].Caption = "�����г�";
            c1FlexGrid1.Cols["UpperCompanyName"].Caption = ShareOption.AreaTitle;
            c1FlexGrid1.Cols["FoodCode"].Caption = "����"+ShareOption.SampleTitle+"����";//ʳƷ
            //c1FlexGrid1.Cols["FoodName"].Caption = "��Ʒ����";
            c1FlexGrid1.Cols["FoodName"].Caption = ShareOption.SampleTitle;
            c1FlexGrid1.Cols["CheckType"].Caption = "�������";
            c1FlexGrid1.Cols["SampleModel"].Caption = "����ͺ�";
            c1FlexGrid1.Cols["SampleLevel"].Caption = "�����ȼ�";
            c1FlexGrid1.Cols["SampleState"].Caption = "���Ż���";
            c1FlexGrid1.Cols["Provider"].Caption = "������/�̱�";
            c1FlexGrid1.Cols["StdCode"].Caption = "������";
            c1FlexGrid1.Cols["OrCheckNo"].Caption = "ԭ�����";
            c1FlexGrid1.Cols["ProduceCompany"].Caption = ShareOption.ProductionUnitNameTag + "����";//������λ
            c1FlexGrid1.Cols["ProduceCompanyName"].Caption = ShareOption.ProductionUnitNameTag;// "������λ";
            c1FlexGrid1.Cols["ProducePlace"].Caption = "���ر���";
            c1FlexGrid1.Cols["ProducePlaceName"].Caption = "����";
            c1FlexGrid1.Cols["ProduceDate"].Caption = "��������";
            c1FlexGrid1.Cols["ImportNum"].Caption = "��������";
            c1FlexGrid1.Cols["SaveNum"].Caption = "�������";
            c1FlexGrid1.Cols["Unit"].Caption = "���ݵ�λ";
            c1FlexGrid1.Cols["SampleNum"].Caption = "�������";
            c1FlexGrid1.Cols["SampleBaseNum"].Caption = "������";
            c1FlexGrid1.Cols["SampleUnit"].Caption = "�������ݵ�λ";
            c1FlexGrid1.Cols["SentCompany"].Caption = "�ͼ쵥λ";
            c1FlexGrid1.Cols["Remark"].Caption = "�������";
            c1FlexGrid1.Cols["TakeDate"].Caption = "��������";
            c1FlexGrid1.Cols["OrganizerName"].Caption = "������";
            c1FlexGrid1.Cols["CheckTotalItem"].Caption = "�����Ŀ����";
            c1FlexGrid1.Cols["CheckTotalItemName"].Caption = "�����Ŀ";
            c1FlexGrid1.Cols["Standard"].Caption = "����׼����";
            c1FlexGrid1.Cols["StandardName"].Caption = "�������";
            c1FlexGrid1.Cols["CheckValueInfo"].Caption = "���ֵ";
            c1FlexGrid1.Cols["ResultInfo"].Caption = "���ֵ��λ";
            c1FlexGrid1.Cols["StandValue"].Caption = "��׼ֵ";
            c1FlexGrid1.Cols["SampleCode"].Caption = "��Ʒ���";
            c1FlexGrid1.Cols["Result"].Caption = "����";
            c1FlexGrid1.Cols["CheckStartDate"].Caption = "�������";
            c1FlexGrid1.Cols["Checker"].Caption = "����˱���";
            c1FlexGrid1.Cols["CheckerName"].Caption = "�����";
            c1FlexGrid1.Cols["Assessor"].Caption = "��׼�˱���";
            c1FlexGrid1.Cols["AssessorName"].Caption = "��׼��";
            c1FlexGrid1.Cols["CheckUnitCode"].Caption = "��ⵥλ����";
            c1FlexGrid1.Cols["CheckUnitName"].Caption = "��ⵥλ";
            c1FlexGrid1.Cols["IsSended"].Caption = "�ѷ���";
            c1FlexGrid1.Cols["SendedDate"].Caption = "��������";
            c1FlexGrid1.Cols["ResultType"].Caption = "����ֶ�";
            c1FlexGrid1.Cols["CheckPlaceCode"].Caption = "���ص����";
            c1FlexGrid1.Cols["CheckPlace"].Caption = "���ص�";
            c1FlexGrid1.Cols["CheckEndDate"].Caption = "������ʱ��";
            c1FlexGrid1.Cols["CheckMachine"].Caption = "�����������";
            c1FlexGrid1.Cols["MachineName"].Caption = "�������";
            c1FlexGrid1.Cols["Organizer"].Caption = "�����˱���";
            c1FlexGrid1.Cols["Sender"].Caption = "�����˱���";
            c1FlexGrid1.Cols["SenderName"].Caption = "������";
            c1FlexGrid1.Cols["CheckPlanCode"].Caption = "���ƻ����";
            c1FlexGrid1.Cols["SaleNum"].Caption = "��������";
            c1FlexGrid1.Cols["Price"].Caption = "����";
            c1FlexGrid1.Cols["CheckederVal"].Caption = "������ȷ��";
            c1FlexGrid1.Cols["IsSentCheck"].Caption = "�Ƿ��ͼ�";
            c1FlexGrid1.Cols["CheckederRemark"].Caption = "������˵��";
            c1FlexGrid1.Cols["IsReSended"].Caption = "�ش���־";
            c1FlexGrid1.Cols["Notes"].Caption = "��ע";


            c1FlexGrid1.Cols["SysCode"].Visible = false;
            c1FlexGrid1.Cols["CheckedCompany"].Visible = false;
            c1FlexGrid1.Cols["UpperCompany"].Visible = false;
            c1FlexGrid1.Cols["FoodCode"].Visible = false;
            c1FlexGrid1.Cols["ProduceCompany"].Visible = false;
            c1FlexGrid1.Cols["ProducePlace"].Visible = false;
            c1FlexGrid1.Cols["CheckTotalItem"].Visible = false;
            c1FlexGrid1.Cols["Standard"].Visible = false;
            c1FlexGrid1.Cols["Checker"].Visible = false;
            c1FlexGrid1.Cols["Assessor"].Visible = false;
            c1FlexGrid1.Cols["CheckUnitCode"].Visible = false;
            c1FlexGrid1.Cols["CheckPlaceCode"].Visible = false;
            c1FlexGrid1.Cols["CheckPlace"].Visible = false;
            c1FlexGrid1.Cols["CheckEndDate"].Visible = false;
            c1FlexGrid1.Cols["CheckMachine"].Visible = false;
            c1FlexGrid1.Cols["Organizer"].Visible = false;
            c1FlexGrid1.Cols["Sender"].Visible = false;
        }

        private void cmdAdd01_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            string sErr = string.Empty;

            if (MessageBox.Show("ȷ���Ƿ�ȫ�������ǰ��ѯ��ʾ��¼�е��ϴ���־��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (clsResultOpr.ExeAllReSend(queryString, out sErr))
                {
                    MessageBox.Show("����ϴ���¼�ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("����ϴ���¼ʧ�ܣ�ԭ��" + sErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                refreshGrid(queryString);
            }
        }


		private void frmReSend_Load(object sender, System.EventArgs e)
		{
            //style1=c1FlexGrid1.Styles.Add("style1");
            //style2 =c1FlexGrid1.Styles.Add("style2");
            //styleNormal = c1FlexGrid1.Styles.Add("styleNormal");
            //style1.BackColor = pnlNoEligible.BackColor;
            //style2.BackColor = pnlSended.BackColor;
            //styleNormal.BackColor = panel2.BackColor;

            //δ�ϴ��ϸ�
            styleNormal = c1FlexGrid1.Styles.Add("styleNormal");
            styleNormal.BackColor = Color.White;
            styleNormal.ForeColor = Color.Black;


            //δ�ϴ����ϸ�
            style1 = c1FlexGrid1.Styles.Add("style1");
            style1.BackColor = Color.White;
            style1.ForeColor = Color.Red;

            //��ʾ�Ѿ��ϴ����ϸ�
            style2 = c1FlexGrid1.Styles.Add("style2");
            style2.BackColor = pnlSended.BackColor;
            style2.ForeColor = Color.Red;

            //��ʾ�Ѿ��ϴ��ϸ�
            style3 = c1FlexGrid1.Styles.Add("style3");
            style3.BackColor = pnlSended.BackColor;
            style3.ForeColor = Color.Black;

			queryString=string.Empty;
			refreshGrid(queryString);

            int index = c1FlexGrid1.RowSel;
            if (index >= 0 && c1FlexGrid1.Rows[index]["Result"].ToString().Equals(ShareOption.ResultFailure))
            {
                c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
            }
		}

		private void cmdSend_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			frmResultSend send=new frmResultSend();
			DialogResult dr=send.ShowDialog(this);
			refreshGrid(queryString);
		}

		
		private void c1CommandMenu2_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,c1CommandMenu2.Text);
		}

		
		private void cmdEdit_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdEdit.Text);
		}

		private void cmdDelete_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdDelete.Text);
		}

        private void cmdQuery_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdQuery.Text);
		}


        private void cmdSend_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdSend.Text);
		}

        private void c1CommandMenu1_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,c1CommandMenu1.Text);
		}

		private void cmdAdd01_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdAdd01.Text);
		}

		private void cmdAdd02_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdAdd02.Text);
		}

		private void cmdPrint_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdPrint.Text);
		}

		private void cmdExit_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdExit.Text);
		}

		private void c1FlexGrid1_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
		{
            setRowStyle();
		}

        private void c1FlexGrid1_DoubleClick(object sender, System.EventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            string sErr = string.Empty;

            if (clsResultOpr.ExeReSend(c1FlexGrid1.Rows[row]["SysCode"].ToString(), out sErr))
            {
                MessageBox.Show("����ϴ���¼�ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("����ϴ���¼ʧ�ܣ�ԭ��" + sErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            refreshGrid(queryString);

        }

		private void cmdShowAll_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			refreshGrid(string.Empty);
		}

		private void cmdShowAll_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,cmdShowAll.Text);
		}

    	private void c1Command2_Select(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(c1ToolBar1,c1Command2.Text);
		}

        private void cmdAdd02_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }

            model = new clsResult();
            model.SysCode = c1FlexGrid1.Rows[row]["SysCode"].ToString();
            model.ResultType = c1FlexGrid1.Rows[row]["ResultType"].ToString();
            model.CheckNo = c1FlexGrid1.Rows[row]["CheckNo"].ToString();
            model.StdCode = c1FlexGrid1.Rows[row]["StdCode"].ToString();
            model.SampleCode = c1FlexGrid1.Rows[row]["SampleCode"].ToString();
            model.CheckedCompany = c1FlexGrid1.Rows[row]["CheckedCompany"].ToString();
            model.CheckedCompanyName = c1FlexGrid1.Rows[row]["CheckedCompanyName"].ToString();
            model.CheckedComDis = c1FlexGrid1.Rows[row]["CheckedComDis"].ToString();
            model.CheckPlaceCode = c1FlexGrid1.Rows[row]["CheckPlaceCode"].ToString();
            model.FoodCode = c1FlexGrid1.Rows[row]["FoodCode"].ToString();
            model.ProduceDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["ProduceDate"]);
            model.ProduceCompany = c1FlexGrid1.Rows[row]["ProduceCompany"].ToString();
            model.ProducePlace = c1FlexGrid1.Rows[row]["ProducePlace"].ToString();
            model.SentCompany = c1FlexGrid1.Rows[row]["SentCompany"].ToString();
            model.Provider = c1FlexGrid1.Rows[row]["Provider"].ToString();
            model.TakeDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["TakeDate"]);
            model.CheckStartDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["CheckStartDate"]);
            model.ImportNum = c1FlexGrid1.Rows[row]["ImportNum"].ToString();
            model.SaveNum = c1FlexGrid1.Rows[row]["SaveNum"].ToString();
            model.Unit = c1FlexGrid1.Rows[row]["Unit"].ToString();
            if (!c1FlexGrid1.Rows[row]["SampleNum"].ToString().Equals(""))
            {
                model.SampleNum = c1FlexGrid1.Rows[row]["SampleNum"].ToString();
            }
            else
            {
                model.SampleNum = "";
            }
            if (!c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString().Equals(""))
            {
                model.SampleBaseNum = c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString();
            }
            else
            {
                model.SampleBaseNum = "";
            }
            model.SampleUnit = c1FlexGrid1.Rows[row]["SampleUnit"].ToString();
            model.SampleLevel = c1FlexGrid1.Rows[row]["SampleLevel"].ToString();
            model.SampleModel = c1FlexGrid1.Rows[row]["SampleModel"].ToString();
            model.SampleState = c1FlexGrid1.Rows[row]["SampleState"].ToString();

            model.CheckMachine = c1FlexGrid1.Rows[row]["CheckMachine"].ToString();
            model.CheckTotalItem = c1FlexGrid1.Rows[row]["CheckTotalItem"].ToString();
            model.Standard = c1FlexGrid1.Rows[row]["Standard"].ToString();
            model.CheckValueInfo = c1FlexGrid1.Rows[row]["CheckValueInfo"].ToString();
            model.StandValue = c1FlexGrid1.Rows[row]["StandValue"].ToString();
            model.Result = c1FlexGrid1.Rows[row]["Result"].ToString();
            model.ResultInfo = c1FlexGrid1.Rows[row]["ResultInfo"].ToString();
            model.UpperCompany = c1FlexGrid1.Rows[row]["UpperCompany"].ToString();
            model.UpperCompanyName = c1FlexGrid1.Rows[row]["UpperCompanyName"].ToString();
            model.OrCheckNo = c1FlexGrid1.Rows[row]["OrCheckNo"].ToString();
            model.CheckType = c1FlexGrid1.Rows[row]["CheckType"].ToString();
            model.CheckUnitCode = c1FlexGrid1.Rows[row]["CheckUnitCode"].ToString();
            model.Checker = c1FlexGrid1.Rows[row]["Checker"].ToString();
            model.Assessor = c1FlexGrid1.Rows[row]["Assessor"].ToString();
            model.Organizer = c1FlexGrid1.Rows[row]["Organizer"].ToString();
            model.Remark = c1FlexGrid1.Rows[row]["Remark"].ToString();
            model.CheckPlanCode = c1FlexGrid1.Rows[row]["CheckPlanCode"].ToString();
            model.SaleNum = c1FlexGrid1.Rows[row]["SaleNum"].ToString();
            model.Price = c1FlexGrid1.Rows[row]["Price"].ToString();
            model.CheckederVal = c1FlexGrid1.Rows[row]["CheckederVal"].ToString();
            model.IsSentCheck = c1FlexGrid1.Rows[row]["IsSentCheck"].ToString();
            model.CheckederRemark = c1FlexGrid1.Rows[row]["CheckederRemark"].ToString();
            model.Notes = c1FlexGrid1.Rows[row]["Notes"].ToString();

            frmPesticideMeasureEdit frm = new frmPesticideMeasureEdit(3);
            frm.Tag = "MX";
            frm.setValue(model);
            DialogResult dr = frm.ShowDialog(this);
        }
	}
}
