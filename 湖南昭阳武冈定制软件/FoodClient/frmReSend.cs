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
	/// frmReSend 的摘要说明。
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
        /// 未上传合格
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle styleNormal = null;
        /// <summary>
        /// 未上传不合格
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style1;

        /// <summary>
        /// 已经上传不合格
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style2 = null;

        /// <summary>
        /// 已经上传合格
        /// </summary>
        private C1.Win.C1FlexGrid.CellStyle style3;
        private clsResult model;
        private readonly clsResultOpr resultBll;
        private string queryString;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmReSend()
        {
            InitializeComponent();

            resultBll = new clsResultOpr();
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
            this.cmdDelete.Text = "删除";
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
            this.cmdEdit.Text = "修改";
            this.cmdEdit.Visible = false;
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "打印所有记录";
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
            this.cmdExit.Text = "退出";
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
            this.cmdAdd01.Text = "清除上传标志";
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
            this.cmdQuery.Text = "查询";
            this.cmdQuery.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdQuery_Click);
            this.cmdQuery.Select += new System.EventHandler(this.cmdQuery_Select);
            // 
            // cmdSend
            // 
            this.cmdSend.Image = ((System.Drawing.Image)(resources.GetObject("cmdSend.Image")));
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Text = "上传";
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
            this.c1CommandMenu1.Text = "新增";
            this.c1CommandMenu1.Visible = false;
            this.c1CommandMenu1.Select += new System.EventHandler(this.c1CommandMenu1_Select);
            // 
            // c1CommandLink12
            // 
            this.c1CommandLink12.Command = this.cmdAdd01;
            this.c1CommandLink12.Text = "检测仪手工录入";
            // 
            // c1CommandLink13
            // 
            this.c1CommandLink13.Command = this.cmdAdd02;
            this.c1CommandLink13.Text = "测速卡手工录入";
            // 
            // cmdAdd02
            // 
            this.cmdAdd02.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd02.Image")));
            this.cmdAdd02.Name = "cmdAdd02";
            this.cmdAdd02.Text = "明细";
            this.cmdAdd02.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd02_Click);
            this.cmdAdd02.Select += new System.EventHandler(this.cmdAdd02_Select);
            // 
            // c1Command3
            // 
            this.c1Command3.Image = ((System.Drawing.Image)(resources.GetObject("c1Command3.Image")));
            this.c1Command3.Name = "c1Command3";
            this.c1Command3.Text = "转存为Word";
            // 
            // c1Command4
            // 
            this.c1Command4.Image = ((System.Drawing.Image)(resources.GetObject("c1Command4.Image")));
            this.c1Command4.Name = "c1Command4";
            this.c1Command4.Text = "转存为Excel";
            // 
            // c1Command6
            // 
            this.c1Command6.Image = ((System.Drawing.Image)(resources.GetObject("c1Command6.Image")));
            this.c1Command6.Name = "c1Command6";
            this.c1Command6.Text = "转存为XML";
            // 
            // c1Command7
            // 
            this.c1Command7.Image = ((System.Drawing.Image)(resources.GetObject("c1Command7.Image")));
            this.c1Command7.Name = "c1Command7";
            this.c1Command7.Text = "转存为达元数据格式";
            // 
            // c1CommandMenu2
            // 
            this.c1CommandMenu2.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink14,
            this.c1CommandLink15});
            this.c1CommandMenu2.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu2.Image")));
            this.c1CommandMenu2.Name = "c1CommandMenu2";
            this.c1CommandMenu2.Text = "转存";
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
            this.cmdShowAll.Text = "显示所有数据";
            this.cmdShowAll.ToolTipText = "显示所有数据";
            this.cmdShowAll.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdShowAll_Click);
            this.cmdShowAll.Select += new System.EventHandler(this.cmdShowAll_Select);
            // 
            // c1Command2
            // 
            this.c1Command2.Image = ((System.Drawing.Image)(resources.GetObject("c1Command2.Image")));
            this.c1Command2.Name = "c1Command2";
            this.c1Command2.Text = "打印检测单";
            this.c1Command2.Visible = false;
            this.c1Command2.Select += new System.EventHandler(this.c1Command2_Select);
            // 
            // c1CommandLink19
            // 
            this.c1CommandLink19.Command = this.cmdAdd02;
            this.c1CommandLink19.Text = "明细";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdAdd01;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.cmdEdit;
            this.c1CommandLink2.Text = "设置重传";
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
            this.lblUnsend.Text = "待上传数";
            this.lblUnsend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSended
            // 
            this.lblSended.Location = new System.Drawing.Point(306, 9);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(82, 17);
            this.lblSended.TabIndex = 12;
            this.lblSended.Text = "已上传数";
            this.lblSended.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnPass
            // 
            this.lblUnPass.Location = new System.Drawing.Point(207, 9);
            this.lblUnPass.Name = "lblUnPass";
            this.lblUnPass.Size = new System.Drawing.Size(82, 17);
            this.lblUnPass.TabIndex = 11;
            this.lblUnPass.Text = "不合格数";
            this.lblUnPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordSum
            // 
            this.lblRecordSum.Location = new System.Drawing.Point(9, 9);
            this.lblRecordSum.Name = "lblRecordSum";
            this.lblRecordSum.Size = new System.Drawing.Size(82, 17);
            this.lblRecordSum.TabIndex = 10;
            this.lblRecordSum.Text = "记录总数";
            this.lblRecordSum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(582, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "已上传";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(511, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "不合格";
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
            this.lblPassed.Text = "合格数";
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
            this.Text = "检测数据重传管理";
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
            frmResultQuery query = new frmResultQuery(3);//query分类代码
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

            this.lblRecordSum.Text = "记录总数:" + (c1FlexGrid1.Rows.Count - 1).ToString();
            string sError = string.Empty;

            string queryStr2 = string.Format("({0}) AND Result='{1}'", q, ShareOption.ResultEligi);
            int iPassCount = resultBll.GetRecCount(queryStr2, out sError);

            queryStr2 = string.Format("({0}) AND IsSended=True", q);
            int iSendedCount = resultBll.GetRecCount(queryStr2, out sError);


            this.lblPassed.Text = "合格数:" + iPassCount.ToString();
            this.lblUnPass.Text = "不合格数:" + (c1FlexGrid1.Rows.Count - 1 - iPassCount).ToString();
            this.lblSended.Text = "已上传数:" + iSendedCount.ToString();

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
                    else //合格
                    {
                        c1FlexGrid1.Styles.Highlight.ForeColor = Color.Black;
                        if (send)//已经上传
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
            //新增字段
            //c1FlexGrid1.Cols["HolesNum"].Caption = "孔位/通道号";
            //c1FlexGrid1.Cols["MachineSampleNum"].Caption = "仪器检测编号";

            c1FlexGrid1.Cols["SysCode"].Caption = "系统编号";
            c1FlexGrid1.Cols["CheckNo"].Caption = "检测编号";
            c1FlexGrid1.Cols["CheckedCompany"].Caption = "被检单位编码";
            //c1FlexGrid1.Cols["CheckedCompanyName"].Caption = "受检人/单位";
            //c1FlexGrid1.Cols["CheckedComDis"].Caption = "档口/店面/车牌号";
            c1FlexGrid1.Cols["CheckedCompanyName"].Caption = ShareOption.NameTitle;
            c1FlexGrid1.Cols["CheckedComDis"].Caption = ShareOption.DomainTitle;
            c1FlexGrid1.Cols["UpperCompany"].Caption = "所属市场代码";
            //c1FlexGrid1.Cols["UpperCompanyName"].Caption = "所属市场";
            c1FlexGrid1.Cols["UpperCompanyName"].Caption = ShareOption.AreaTitle;
            c1FlexGrid1.Cols["FoodCode"].Caption = "被检"+ShareOption.SampleTitle+"编码";//食品
            //c1FlexGrid1.Cols["FoodName"].Caption = "商品名称";
            c1FlexGrid1.Cols["FoodName"].Caption = ShareOption.SampleTitle;
            c1FlexGrid1.Cols["CheckType"].Caption = "检测类型";
            c1FlexGrid1.Cols["SampleModel"].Caption = "规格型号";
            c1FlexGrid1.Cols["SampleLevel"].Caption = "质量等级";
            c1FlexGrid1.Cols["SampleState"].Caption = "批号或编号";
            c1FlexGrid1.Cols["Provider"].Caption = "供货商/商标";
            c1FlexGrid1.Cols["StdCode"].Caption = "条形码";
            c1FlexGrid1.Cols["OrCheckNo"].Caption = "原检测编号";
            c1FlexGrid1.Cols["ProduceCompany"].Caption = ShareOption.ProductionUnitNameTag + "编码";//生产单位
            c1FlexGrid1.Cols["ProduceCompanyName"].Caption = ShareOption.ProductionUnitNameTag;// "生产单位";
            c1FlexGrid1.Cols["ProducePlace"].Caption = "产地编码";
            c1FlexGrid1.Cols["ProducePlaceName"].Caption = "产地";
            c1FlexGrid1.Cols["ProduceDate"].Caption = "生产日期";
            c1FlexGrid1.Cols["ImportNum"].Caption = "进货数量";
            c1FlexGrid1.Cols["SaveNum"].Caption = "库存数量";
            c1FlexGrid1.Cols["Unit"].Caption = "数据单位";
            c1FlexGrid1.Cols["SampleNum"].Caption = "抽检数量";
            c1FlexGrid1.Cols["SampleBaseNum"].Caption = "抽检基数";
            c1FlexGrid1.Cols["SampleUnit"].Caption = "抽样数据单位";
            c1FlexGrid1.Cols["SentCompany"].Caption = "送检单位";
            c1FlexGrid1.Cols["Remark"].Caption = "处理情况";
            c1FlexGrid1.Cols["TakeDate"].Caption = "抽样日期";
            c1FlexGrid1.Cols["OrganizerName"].Caption = "抽样人";
            c1FlexGrid1.Cols["CheckTotalItem"].Caption = "检测项目编码";
            c1FlexGrid1.Cols["CheckTotalItemName"].Caption = "检测项目";
            c1FlexGrid1.Cols["Standard"].Caption = "检测标准编码";
            c1FlexGrid1.Cols["StandardName"].Caption = "检测依据";
            c1FlexGrid1.Cols["CheckValueInfo"].Caption = "检测值";
            c1FlexGrid1.Cols["ResultInfo"].Caption = "检测值单位";
            c1FlexGrid1.Cols["StandValue"].Caption = "标准值";
            c1FlexGrid1.Cols["SampleCode"].Caption = "样品编号";
            c1FlexGrid1.Cols["Result"].Caption = "结论";
            c1FlexGrid1.Cols["CheckStartDate"].Caption = "检测日期";
            c1FlexGrid1.Cols["Checker"].Caption = "检测人编码";
            c1FlexGrid1.Cols["CheckerName"].Caption = "检测人";
            c1FlexGrid1.Cols["Assessor"].Caption = "核准人编码";
            c1FlexGrid1.Cols["AssessorName"].Caption = "核准人";
            c1FlexGrid1.Cols["CheckUnitCode"].Caption = "检测单位代码";
            c1FlexGrid1.Cols["CheckUnitName"].Caption = "检测单位";
            c1FlexGrid1.Cols["IsSended"].Caption = "已发送";
            c1FlexGrid1.Cols["SendedDate"].Caption = "发送日期";
            c1FlexGrid1.Cols["ResultType"].Caption = "检测手段";
            c1FlexGrid1.Cols["CheckPlaceCode"].Caption = "抽检地点编码";
            c1FlexGrid1.Cols["CheckPlace"].Caption = "抽检地点";
            c1FlexGrid1.Cols["CheckEndDate"].Caption = "检测结束时间";
            c1FlexGrid1.Cols["CheckMachine"].Caption = "检测仪器编码";
            c1FlexGrid1.Cols["MachineName"].Caption = "检测仪器";
            c1FlexGrid1.Cols["Organizer"].Caption = "编制人编码";
            c1FlexGrid1.Cols["Sender"].Caption = "发送人编码";
            c1FlexGrid1.Cols["SenderName"].Caption = "发送人";
            c1FlexGrid1.Cols["CheckPlanCode"].Caption = "检测计划编号";
            c1FlexGrid1.Cols["SaleNum"].Caption = "销售数量";
            c1FlexGrid1.Cols["Price"].Caption = "单价";
            c1FlexGrid1.Cols["CheckederVal"].Caption = "被检人确认";
            c1FlexGrid1.Cols["IsSentCheck"].Caption = "是否送检";
            c1FlexGrid1.Cols["CheckederRemark"].Caption = "被检人说明";
            c1FlexGrid1.Cols["IsReSended"].Caption = "重传标志";
            c1FlexGrid1.Cols["Notes"].Caption = "备注";


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

            if (MessageBox.Show("确认是否全部清除当前查询显示记录中的上传标志？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (clsResultOpr.ExeAllReSend(queryString, out sErr))
                {
                    MessageBox.Show("清除上传记录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("清除上传记录失败！原因：" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            //未上传合格
            styleNormal = c1FlexGrid1.Styles.Add("styleNormal");
            styleNormal.BackColor = Color.White;
            styleNormal.ForeColor = Color.Black;


            //未上传不合格
            style1 = c1FlexGrid1.Styles.Add("style1");
            style1.BackColor = Color.White;
            style1.ForeColor = Color.Red;

            //表示已经上传不合格
            style2 = c1FlexGrid1.Styles.Add("style2");
            style2.BackColor = pnlSended.BackColor;
            style2.ForeColor = Color.Red;

            //表示已经上传合格
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
                MessageBox.Show("清除上传记录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("清除上传记录失败！原因：" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
