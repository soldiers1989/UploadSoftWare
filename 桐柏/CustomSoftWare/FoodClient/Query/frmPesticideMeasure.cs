using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;
using DY.FoodClientLib.Model;
using FoodClient.Query;
using System.Configuration;
using FoodClient.AnHui;
using DY.Process;
using FoodClient.HeFei;

namespace FoodClient
{
    /// <summary>
    /// frmPesticideMeasure 的摘要说明。
    /// </summary>
    public class frmPesticideMeasure : TitleBarBase
    {

        #region 控件私有变量
        private C1.Win.C1Command.C1Command cmdDelete;
        private C1.Win.C1Command.C1Command c1Command5;
        private C1.Win.C1Command.C1Command cmdEdit;
        private C1.Win.C1Command.C1Command cmdPrint;
        private C1.Win.C1Command.C1Command cmdExit;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblIsAllreadySended;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordSum;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private C1.Win.C1Command.C1Command c1Command1;
        private C1.Win.C1Command.C1Command cmdQuery;
        private C1.Win.C1Command.C1Command cmdSend;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink12;
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
        private C1.Win.C1Command.C1Command c1Command2;
        private C1.Win.C1Command.C1Command c1CommandReport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        #endregion

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
        private clsResult _resultModel;
        private readonly clsResultOpr _clsResultOpr;
        private int _queryType;
        private clsResultOpr _curObjectOpr = new clsResultOpr();

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        private bool IsSuperAdmin = false;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;
        private C1.Win.C1Command.C1ToolBar c1ToolBar1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink19;
        public C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandLink c1CommandLink7;
        private C1.Win.C1Command.C1CommandLink c1CommandLink8;
        private C1.Win.C1Command.C1CommandLink c1CommandLink10;
        private C1.Win.C1Command.C1CommandLink c1CommandLink9;
        private C1.Win.C1Command.C1CommandLink c1CommandLink5;
        private C1.Win.C1Command.C1CommandLink c1CommandLink17;
        private C1.Win.C1Command.C1CommandLink c1CommandLink18;
        private C1.Win.C1Command.C1CommandLink c1CommandLink20;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1CommandLink c1CommandLink6;

        /// <summary>
        /// 接收外部条件
        /// </summary>
        private string queryString;//组合查询条件


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="selectType">1代表标准速测法查询,2表示其他查询, 3表示综合查询</param>
        public frmPesticideMeasure(int selectType)
        {
            InitializeComponent();
            _queryType = selectType;
            _clsResultOpr = new clsResultOpr();
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesticideMeasure));
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.cmdExit = new C1.Win.C1Command.C1Command();
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
            this.c1CommandReport = new C1.Win.C1Command.C1Command();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUnsend = new System.Windows.Forms.Label();
            this.lblSended = new System.Windows.Forms.Label();
            this.lblUnPass = new System.Windows.Forms.Label();
            this.lblRecordSum = new System.Windows.Forms.Label();
            this.lblIsAllreadySended = new System.Windows.Forms.Label();
            this.pnlSended = new System.Windows.Forms.Panel();
            this.pnlNoEligible = new System.Windows.Forms.Panel();
            this.lblPassed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.c1Report1 = new C1.Win.C1Report.C1Report();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandLink19 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink8 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink10 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink17 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink18 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink20 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "删除";
            this.cmdDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDelete_Click);
            this.cmdDelete.Select += new System.EventHandler(this.cmdDelete_Select);
            // 
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.cmdEdit.Text = "修改";
            this.cmdEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdEdit_Click);
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "打印所有记录";
            this.cmdPrint.Visible = false;
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
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
            // c1CommandHolder1
            // 
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
            this.c1CommandHolder1.Commands.Add(this.c1CommandReport);
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
            // c1CommandLink13
            // 
            this.c1CommandLink13.Command = this.cmdAdd02;
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
            this.c1Command3.Name = "c1Command3";
            // 
            // c1Command4
            // 
            this.c1Command4.Image = ((System.Drawing.Image)(resources.GetObject("c1Command4.Image")));
            this.c1Command4.Name = "c1Command4";
            this.c1Command4.Text = "转存为Excel";
            this.c1Command4.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command4_Click);
            // 
            // c1Command6
            // 
            this.c1Command6.Image = ((System.Drawing.Image)(resources.GetObject("c1Command6.Image")));
            this.c1Command6.Name = "c1Command6";
            this.c1Command6.Text = "转存为XML";
            this.c1Command6.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command6_Click);
            // 
            // c1Command7
            // 
            this.c1Command7.Name = "c1Command7";
            // 
            // c1CommandMenu2
            // 
            this.c1CommandMenu2.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink14,
            this.c1CommandLink15});
            this.c1CommandMenu2.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu2.Image")));
            this.c1CommandMenu2.Name = "c1CommandMenu2";
            this.c1CommandMenu2.Text = "转存";
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
            this.c1Command2.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command2_Click);
            this.c1Command2.Select += new System.EventHandler(this.c1Command2_Select);
            // 
            // c1CommandReport
            // 
            this.c1CommandReport.Image = global::FoodClient.Properties.Resources.report;
            this.c1CommandReport.Name = "c1CommandReport";
            this.c1CommandReport.Text = "生成报表";
            this.c1CommandReport.Visible = false;
            this.c1CommandReport.Click += new C1.Win.C1Command.ClickEventHandler(this.c1CommandReport_Click);
            this.c1CommandReport.Select += new System.EventHandler(this.c1CommandReport_Select);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.AutoResize = false;
            this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
            this.c1FlexGrid1.Location = new System.Drawing.Point(3, 59);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 23;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.c1FlexGrid1.Size = new System.Drawing.Size(1041, 516);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 1;
            this.c1FlexGrid1.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid1_AfterSort);
            this.c1FlexGrid1.Click += new System.EventHandler(this.c1FlexGrid1_Click);
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lblUnsend);
            this.panel2.Controls.Add(this.lblSended);
            this.panel2.Controls.Add(this.lblUnPass);
            this.panel2.Controls.Add(this.lblRecordSum);
            this.panel2.Controls.Add(this.lblIsAllreadySended);
            this.panel2.Controls.Add(this.pnlSended);
            this.panel2.Controls.Add(this.pnlNoEligible);
            this.panel2.Controls.Add(this.lblPassed);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(3, 579);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1041, 32);
            this.panel2.TabIndex = 11;
            // 
            // lblUnsend
            // 
            this.lblUnsend.Location = new System.Drawing.Point(542, 4);
            this.lblUnsend.Name = "lblUnsend";
            this.lblUnsend.Size = new System.Drawing.Size(120, 21);
            this.lblUnsend.TabIndex = 13;
            this.lblUnsend.Text = "待上传数";
            this.lblUnsend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSended
            // 
            this.lblSended.Location = new System.Drawing.Point(418, 5);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(118, 21);
            this.lblSended.TabIndex = 12;
            this.lblSended.Text = "已上传数";
            this.lblSended.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnPass
            // 
            this.lblUnPass.Location = new System.Drawing.Point(287, 5);
            this.lblUnPass.Name = "lblUnPass";
            this.lblUnPass.Size = new System.Drawing.Size(125, 21);
            this.lblUnPass.TabIndex = 11;
            this.lblUnPass.Text = "不合格数";
            this.lblUnPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordSum
            // 
            this.lblRecordSum.Location = new System.Drawing.Point(9, 7);
            this.lblRecordSum.Name = "lblRecordSum";
            this.lblRecordSum.Size = new System.Drawing.Size(139, 21);
            this.lblRecordSum.TabIndex = 10;
            this.lblRecordSum.Text = "记录总数";
            this.lblRecordSum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIsAllreadySended
            // 
            this.lblIsAllreadySended.Location = new System.Drawing.Point(772, 8);
            this.lblIsAllreadySended.Name = "lblIsAllreadySended";
            this.lblIsAllreadySended.Size = new System.Drawing.Size(56, 21);
            this.lblIsAllreadySended.TabIndex = 9;
            this.lblIsAllreadySended.Text = "已上传";
            // 
            // pnlSended
            // 
            this.pnlSended.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlSended.Location = new System.Drawing.Point(754, 10);
            this.pnlSended.Name = "pnlSended";
            this.pnlSended.Size = new System.Drawing.Size(13, 12);
            this.pnlSended.TabIndex = 7;
            // 
            // pnlNoEligible
            // 
            this.pnlNoEligible.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlNoEligible.Location = new System.Drawing.Point(668, 10);
            this.pnlNoEligible.Name = "pnlNoEligible";
            this.pnlNoEligible.Size = new System.Drawing.Size(13, 12);
            this.pnlNoEligible.TabIndex = 6;
            // 
            // lblPassed
            // 
            this.lblPassed.Location = new System.Drawing.Point(154, 7);
            this.lblPassed.Name = "lblPassed";
            this.lblPassed.Size = new System.Drawing.Size(127, 21);
            this.lblPassed.TabIndex = 5;
            this.lblPassed.Text = "合格数";
            this.lblPassed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(686, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "不合格";
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "WorkSheetReport";
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
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
            this.c1CommandLink20,
            this.c1CommandLink4,
            this.c1CommandLink6});
            this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.ShowToolTips = false;
            this.c1ToolBar1.Size = new System.Drawing.Size(1061, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandLink19
            // 
            this.c1CommandLink19.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink19.Command = this.cmdAdd02;
            this.c1CommandLink19.Text = "明细";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink1.Command = this.c1CommandMenu1;
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
            // c1CommandLink7
            // 
            this.c1CommandLink7.Command = this.c1Command1;
            // 
            // c1CommandLink8
            // 
            this.c1CommandLink8.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink8.Command = this.cmdQuery;
            // 
            // c1CommandLink10
            // 
            this.c1CommandLink10.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink10.Command = this.cmdSend;
            // 
            // c1CommandLink9
            // 
            this.c1CommandLink9.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink9.Command = this.c1CommandMenu2;
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink5.Command = this.cmdPrint;
            // 
            // c1CommandLink17
            // 
            this.c1CommandLink17.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink17.Command = this.cmdShowAll;
            // 
            // c1CommandLink18
            // 
            this.c1CommandLink18.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink18.Command = this.c1Command2;
            // 
            // c1CommandLink20
            // 
            this.c1CommandLink20.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink20.Command = this.c1CommandReport;
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink6.Command = this.cmdExit;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1CommandDock1.AutoDockBottom = false;
            this.c1CommandDock1.AutoDockLeft = false;
            this.c1CommandDock1.AutoDockRight = false;
            this.c1CommandDock1.AutoDockTop = false;
            this.c1CommandDock1.AutoSize = false;
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Dock = System.Windows.Forms.DockStyle.None;
            this.c1CommandDock1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(3, 29);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(1041, 32);
            // 
            // frmPesticideMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(1047, 616);
            this.ControlBox = false;
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPesticideMeasure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测数据综合查询";
            this.Load += new System.EventHandler(this.frmPesticideMeasure_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPesticideMeasure_KeyDown);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.c1FlexGrid1, 0);
            this.Controls.SetChildIndex(this.c1CommandDock1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPesticideMeasure_Load(object sender, System.EventArgs e)
        {
            TitleBarText = this.Text;
            if (ShareOption.IsDataLink)//如果是单机版
            {
                lblSended.Visible = false;
                lblUnsend.Visible = false;
                pnlSended.Visible = false;
                lblIsAllreadySended.Visible = false;
                cmdSend.Visible = false;
            }
            #region @zh 2016/11/20 合肥
            //打开上传相关功能
            lblSended.Visible = true;
            lblUnsend.Visible = true;
            pnlSended.Visible = true;
            lblIsAllreadySended.Visible = true;
            cmdSend.Visible = true;

            #endregion

            c1FlexGrid1.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;

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

            StringBuilder sb = new StringBuilder();
            sb.Append("CheckStartDate>=#");
            sb.Append(DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd"));//前一个月内
            sb.Append("#");
            sb.Append(" AND CheckStartDate<=#");
            sb.Append(DateTime.Today.ToString("yyyy-MM-dd"));
            sb.Append(" 23:59:59#");

            queryString = sb.ToString();
            refreshGrid(queryString);
            sb.Length = 0;
        }

        /// <summary>
        /// 消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.SystemAdmin && e.Message.Equals("1"))
            {
                IsSuperAdmin = true;
            }
        }

        /// <summary>
        /// 筛选条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdQuery_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            frmResultQuery query = new frmResultQuery(_queryType);
            DialogResult dr = query.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                queryString = query.QueryString;
                refreshGrid(queryString);
            }
        }

        /// <summary>
        /// 退出关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            this.queryString = null;
            this.Close();
        }

        /// <summary>
        /// 加载数据绑定控件
        /// </summary>
        /// <param name="query"></param>
        private void refreshGrid(string query)
        {
            StringBuilder sb = new StringBuilder();
            
            if (!string.IsNullOrEmpty(query))
            {
                sb.Append(query);
                sb.Append(" AND ");
            }
            switch (_queryType)
            {
                //标准速测法
                case 1: sb.AppendFormat("(ResultType='{0}' or ResultType='{1}' or ResultType='{2}' )", ShareOption.ResultType1, ShareOption.ResultType2, ShareOption.ResultType5);
                    break;

                //其他速测法
                case 2: sb.AppendFormat("ResultType='{0}' ", ShareOption.ResultType3);
                    break;

                //综合查询
                case 3: sb.Append("ResultType<>''");
                    break;
            }
            string qWhere = sb.ToString();
            sb.Length = 0;
            DataTable dtbl = _clsResultOpr.GetAsDataTable(qWhere, "A.CheckStartDate DESC, A.syscode desc");
            if (dtbl != null)
            {
                int d = dtbl.Rows.Count;
                c1FlexGrid1.DataSource = dtbl;
            }
            else
            {
                return;
            }
            int len = 0;
            if (dtbl != null && c1FlexGrid1.Rows.Count > 0)
            {
                len = c1FlexGrid1.Rows.Count - 1;
            }
            int passCount = 0;//合格数
            int unpassCount = 0;//不合格数
            int sendedCount = 0;//已上传
            int waiteSend = 0;//待上传的
            string error = string.Empty;
            string queryFilter = string.Empty;

            queryFilter = string.Format("({0}) AND Result='{1}'", qWhere, ShareOption.ResultEligi);
            passCount = _clsResultOpr.GetRecCount(queryFilter, out error);
            queryFilter = string.Format("({0}) AND IsSended=True", qWhere);
            sendedCount = _clsResultOpr.GetRecCount(queryFilter, out error);

            if (len >= passCount)
            {
                unpassCount = len - passCount;
            }
            else
            {
                unpassCount = 0;
                passCount = len;
            }
            if (len >= sendedCount)
            {
                waiteSend = len - sendedCount;
            }
            else
            {
                waiteSend = 0;
                sendedCount = len;
            }
            lblRecordSum.Text = "记录总数:" + len.ToString();
            lblPassed.Text = "合格数:" + passCount.ToString();
            lblUnPass.Text = "不合格数:" + unpassCount.ToString();
            lblSended.Text = "已上传数:" + sendedCount.ToString();
            lblUnsend.Text = "待上传数:" + waiteSend.ToString();

            setRowStyle();
            setGridStyle();
            c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.AutoSizeCols();
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        private void setGridStyle()
        {
            c1FlexGrid1.Cols.Count = 71;
            c1FlexGrid1.Cols["SysCode"].Caption = "系统编号";
            c1FlexGrid1.Cols["CheckNo"].Caption = "检测编号";
            c1FlexGrid1.Cols["CheckedCompany"].Caption = "被检单位编码";
            c1FlexGrid1.Cols["CheckedCompanyName"].Caption = ShareOption.NameTitle;
            c1FlexGrid1.Cols["CheckedComDis"].Caption = ShareOption.DomainTitle;
            c1FlexGrid1.Cols["CheckedComDis"].Visible = false;
            c1FlexGrid1.Cols["UpperCompany"].Caption = "所属市场代码";
            c1FlexGrid1.Cols["UpperCompanyName"].Caption = ShareOption.AreaTitle;
            c1FlexGrid1.Cols["UpperCompanyName"].Visible = false;
            c1FlexGrid1.Cols["FoodCode"].Caption = "被检" + ShareOption.SampleTitle + "编码";
            c1FlexGrid1.Cols["FoodName"].Caption = ShareOption.SampleTitle;
            c1FlexGrid1.Cols["CheckType"].Caption = "检测类型";
            c1FlexGrid1.Cols["CheckType"].Visible = false;
            c1FlexGrid1.Cols["SampleModel"].Caption = "规格型号";
            c1FlexGrid1.Cols["SampleModel"].Visible = false;
            c1FlexGrid1.Cols["SampleLevel"].Caption = "质量等级";
            c1FlexGrid1.Cols["SampleLevel"].Visible = false;
            c1FlexGrid1.Cols["SampleState"].Caption = "批号或编号";
            c1FlexGrid1.Cols["SampleState"].Visible = false;
            c1FlexGrid1.Cols["Provider"].Caption = "供货商/商标";
            c1FlexGrid1.Cols["Provider"].Visible = false;
            c1FlexGrid1.Cols["StdCode"].Caption = "条形码";
            c1FlexGrid1.Cols["StdCode"].Visible = false;
            c1FlexGrid1.Cols["OrCheckNo"].Caption = "原检测编号";
            c1FlexGrid1.Cols["OrCheckNo"].Visible = false;
            c1FlexGrid1.Cols["ProduceCompany"].Caption = ShareOption.ProductionUnitNameTag + "编码";
            c1FlexGrid1.Cols["ProduceCompany"].Visible = false;
            c1FlexGrid1.Cols["ProduceCompanyName"].Caption = ShareOption.ProductionUnitNameTag;//"生产单位";
            c1FlexGrid1.Cols["ProduceCompanyName"].Visible = false;
            c1FlexGrid1.Cols["ProducePlace"].Caption = "产地编码";
            c1FlexGrid1.Cols["ProducePlace"].Visible = false;
            c1FlexGrid1.Cols["ProducePlaceName"].Caption = "产地";
            c1FlexGrid1.Cols["ProducePlaceName"].Visible = false;
            c1FlexGrid1.Cols["ProduceDate"].Caption = "生产日期";
            c1FlexGrid1.Cols["ProduceDate"].Visible = false;
            c1FlexGrid1.Cols["ImportNum"].Caption = "进货数量";
            c1FlexGrid1.Cols["ImportNum"].Visible = false;
            c1FlexGrid1.Cols["SaveNum"].Caption = "库存数量";
            c1FlexGrid1.Cols["SaveNum"].Visible = false;
            c1FlexGrid1.Cols["Unit"].Caption = "单位";//"数据单位";
            c1FlexGrid1.Cols["SampleNum"].Caption = "抽检数量";
            c1FlexGrid1.Cols["SampleNum"].Visible = false;
            c1FlexGrid1.Cols["SampleBaseNum"].Caption = "抽检基数";
            c1FlexGrid1.Cols["SampleBaseNum"].Visible = false;
            c1FlexGrid1.Cols["SampleUnit"].Caption = "抽样数据单位";
            c1FlexGrid1.Cols["SampleUnit"].Visible = false;
            c1FlexGrid1.Cols["SentCompany"].Caption = "送检单位";
            c1FlexGrid1.Cols["SentCompany"].Visible = false;
            c1FlexGrid1.Cols["Remark"].Caption = "处理情况";
            c1FlexGrid1.Cols["Remark"].Visible = false;
            c1FlexGrid1.Cols["TakeDate"].Caption = "抽样日期";
            c1FlexGrid1.Cols["OrganizerName"].Caption = "抽样人";
            c1FlexGrid1.Cols["CheckTotalItem"].Caption = "检测项目编码";
            c1FlexGrid1.Cols["CheckTotalItem"].Visible = false;
            c1FlexGrid1.Cols["CheckTotalItemName"].Caption = "检测项目";
            c1FlexGrid1.Cols["Standard"].Caption = "检测标准编码";
            c1FlexGrid1.Cols["StandardName"].Caption = "检测依据";
            c1FlexGrid1.Cols["CheckValueInfo"].Caption = "检测值";
            c1FlexGrid1.Cols["ResultInfo"].Caption = "检测值单位";
            c1FlexGrid1.Cols["StandValue"].Caption = "标准值";
            c1FlexGrid1.Cols["SampleCode"].Caption = "样品编号";
            c1FlexGrid1.Cols["Result"].Caption = "结论";
            c1FlexGrid1.Cols["CheckStartDate"].Caption = "检测时间";
            c1FlexGrid1.Cols["Checker"].Caption = "检测人编码";
            c1FlexGrid1.Cols["CheckerName"].Caption = "检测人";
            c1FlexGrid1.Cols["Assessor"].Caption = "核准人编码";
            c1FlexGrid1.Cols["AssessorName"].Caption = "核准人";
            c1FlexGrid1.Cols["CheckUnitCode"].Caption = "检测单位代码";
            c1FlexGrid1.Cols["CheckUnitName"].Caption = "检测单位";
            c1FlexGrid1.Cols["IsSended"].Caption = "已发送";
            c1FlexGrid1.Cols["SendedDate"].Caption = "发送日期";
            c1FlexGrid1.Cols["SendedDate"].Visible = false;
            c1FlexGrid1.Cols["ResultType"].Caption = "检测手段";
            c1FlexGrid1.Cols["CheckPlaceCode"].Caption = "抽检地点编码";
            c1FlexGrid1.Cols["CheckPlace"].Caption = "抽检地点";
            c1FlexGrid1.Cols["CheckEndDate"].Caption = "检测结束时间";
            c1FlexGrid1.Cols["CheckEndDate"].Visible = false;
            c1FlexGrid1.Cols["CheckMachine"].Caption = "检测仪器编码";
            c1FlexGrid1.Cols["CheckMachine"].Visible = false;
            c1FlexGrid1.Cols["HolesNum"].Caption = "孔位/通道号";
            c1FlexGrid1.Cols["HolesNum"].Visible = false;
            c1FlexGrid1.Cols["MachineSampleNum"].Caption = "仪器检测编号";
            c1FlexGrid1.Cols["MachineSampleNum"].Visible = false;
            c1FlexGrid1.Cols["MachineName"].Caption = "检测仪器";
            c1FlexGrid1.Cols["Organizer"].Caption = "编制人编码";
            c1FlexGrid1.Cols["Organizer"].Visible = false;
            c1FlexGrid1.Cols["Sender"].Caption = "发送人编码";
            c1FlexGrid1.Cols["Sender"].Visible = false;
            c1FlexGrid1.Cols["SenderName"].Caption = "发送人";

            c1FlexGrid1.Cols["CheckPlanCode"].Caption = "检测计划编号";
            c1FlexGrid1.Cols["CheckPlanCode"].Visible = false;
            c1FlexGrid1.Cols["SaleNum"].Caption = "销售数量";
            c1FlexGrid1.Cols["SaleNum"].Visible = false;
            c1FlexGrid1.Cols["Price"].Caption = "单价";
            c1FlexGrid1.Cols["Price"].Visible = false;
            c1FlexGrid1.Cols["CheckederVal"].Caption = "被检人确认";
            c1FlexGrid1.Cols["CheckederVal"].Visible = false;
            c1FlexGrid1.Cols["IsSentCheck"].Caption = "是否送检";
            c1FlexGrid1.Cols["IsSentCheck"].Visible = false;
            c1FlexGrid1.Cols["CheckederRemark"].Caption = "被检人说明";
            c1FlexGrid1.Cols["CheckederRemark"].Visible = false;
            c1FlexGrid1.Cols["IsReSended"].Caption = "重传标志";
            c1FlexGrid1.Cols["IsReSended"].Visible = false;
            c1FlexGrid1.Cols["Notes"].Caption = "备注";
            c1FlexGrid1.Cols["Notes"].Visible = false;
            if (ShareOption.IsDataLink)
            {
                c1FlexGrid1.Cols["IsSended"].Visible = false;
                c1FlexGrid1.Cols["SendedDate"].Visible = false;
                c1FlexGrid1.Cols["SenderName"].Visible = false;
                c1FlexGrid1.Cols["IsReSended"].Visible = false;
            }
            #region @zh 2016/11/12 合肥
            //打开上传相关功能
            {
                c1FlexGrid1.Cols["IsSended"].Visible = true;
                c1FlexGrid1.Cols["SendedDate"].Visible = true;
                c1FlexGrid1.Cols["SenderName"].Visible = true;
                c1FlexGrid1.Cols["IsReSended"].Visible = true;
            }
            #endregion

            if (_queryType == 2)
            {
                c1FlexGrid1.Cols["MachineSampleNum"].Visible = false;
                c1FlexGrid1.Cols["CheckMachine"].Visible = false;
                c1FlexGrid1.Cols["MachineName"].Visible = false;
            }
            c1FlexGrid1.Cols["SysCode"].Visible = false;
            c1FlexGrid1.Cols["CheckedCompany"].Visible = false;
            c1FlexGrid1.Cols["UpperCompany"].Visible = false;
            c1FlexGrid1.Cols["FoodCode"].Visible = false;
            c1FlexGrid1.Cols["ProduceCompany"].Visible = false;
            c1FlexGrid1.Cols["ProducePlace"].Visible = false;
            c1FlexGrid1.Cols["CheckTotalItem"].Visible = false;
            c1FlexGrid1.Cols["CheckUnitName"].Visible = false;
            c1FlexGrid1.Cols["CheckUnitInfo"].Visible = false;
            c1FlexGrid1.Cols["MachineModel"].Visible = false;
            c1FlexGrid1.Cols["MachineCompany"].Visible = false;
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
            c1FlexGrid1.Cols["IsReSended"].Visible = false;
            //检测编号
            c1FlexGrid1.Cols["CheckNo"].Visible = false;
            //发送人
            c1FlexGrid1.Cols["SenderName"].Visible = false;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            if (Convert.ToBoolean(c1FlexGrid1.Rows[row]["IsSended"]))
            {
                MessageBox.Show("该记录已经上传！无法进行修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmPesticideMeasureEdit frm = new frmPesticideMeasureEdit(_queryType);
            frm.Tag = "EDIT";
            GetModel(row);
            frm.setValue(_resultModel);
            frm.Text = getEditTitle("修改");
            DialogResult dr = frm.ShowDialog(this);
            //刷新窗体中的Grid
            if (dr == DialogResult.OK)
            {
                refreshGrid(queryString);
            }
        }

        /// <summary>
        /// 设置窗口标题名称
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string getEditTitle(string tag)
        {
            string title = string.Empty;
            switch (_queryType)
            {
                case 1: title = "标准速测法检测数据"; break;
                case 2: title = "其他速测法检测数据"; break;
                case 3: title = "综合检测数据"; break;
                default: title = ""; break;
            }
            return title + tag;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            int row = c1FlexGrid1.RowSel, delCount = 0;
            string sysCode = string.Empty, err = string.Empty, msg = string.Empty;

            if (row <= 0)
            {
                MessageBox.Show(this, "请选择将要删除的记录！");
                return;
            }

            try
            {
                for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                {
                    if (c1FlexGrid1.Rows[i].Selected == true)
                    {
                        if (!IsSuperAdmin)//对于超级管理员无效
                        {
                            if (Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsSended"]))
                            {
                                msg += "[" + c1FlexGrid1[i, "FoodName"].ToString().Trim() + "]";
                                continue;
                            }
                            //if (Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsReSended"])) del = false;
                        }
                       
                        sysCode = c1FlexGrid1[i, "SysCode"].ToString().Trim();
                        _clsResultOpr.DeleteByPrimaryKey(sysCode, out err);
                        if (err.Length == 0) delCount++;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                refreshGrid(queryString);
                if (msg.Length > 0)
                {
                    MessageBox.Show(this, "部分记录因已上传平台，无法删除！\r\n样品列表：\r\n" + msg, "系统提示");
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            PrintOperation.PreviewGrid(c1FlexGrid1, "检测结果列表", "制表人");
            refreshGrid(string.Empty);
        }

        private void UploadAnHuiProcess(Action<int> percent)
        {
            percent(0);
            string sysCode = string.Empty, err = string.Empty, msg = string.Empty, outErr = string.Empty;
            int count = 0, uploadCount = 0;
            try
            {
                //从数据库中拿到全部数据，然后和选择的记录对比。
                DataTable dt = _curObjectOpr.GetUploadDataTable("", "A.SysCode", ShareOption.ApplicationTag);
                percent(3);
                uploadCount = c1FlexGrid1.Rows.Selected.Count;

                float percentage1 = (float)97 / (float)uploadCount, percentage2 = 0;
                count = (int)percentage1 + 3;
                object obj = null;
                for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                {
                    sysCode = c1FlexGrid1[i, "SysCode"].ToString().Trim();
                    if (c1FlexGrid1.Rows[i].Selected == true && !Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsSended"]))
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["SysCode"].ToString().Trim().Equals(sysCode))
                            {
                                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                                model.userName = Global.AnHuiInterface.userName;
                                model.instrument = Global.AnHuiInterface.instrument;
                                model.passWord = Global.AnHuiInterface.passWord;
                                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                                model.mac = Global.AnHuiInterface.mac;

                                model.gps = string.Empty;
                                //样品编号
                                obj = dt.Rows[j]["SampleCode"];
                                if (obj != null && obj != DBNull.Value) model.sampleNo = obj.ToString();
                                //样品名称
                                obj = dt.Rows[j]["FoodName"];
                                if (obj != null && obj != DBNull.Value) model.fName = obj.ToString();
                                //样品名称
                                obj = dt.Rows[j]["fTpye"];
                                if (obj != null && obj != DBNull.Value) model.fTpye = obj.ToString();
                                //生产日期
                                obj = dt.Rows[j]["ProduceDate"];
                                if (obj != null && obj != DBNull.Value) model.proDate = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                else model.proDate = "";
                                //被检单位编号
                                obj = dt.Rows[j]["checkedUnit"];//CheckedCompanyCode
                                if (obj != null && obj != DBNull.Value) model.checkedUnit = obj.ToString();
                                else model.checkedUnit = "0";
                                //检测编号
                                obj = dt.Rows[j]["CheckNo"];
                                if (obj != null && obj != DBNull.Value) model.dataNum = obj.ToString();
                                //检测项目编号
                                obj = dt.Rows[j]["testPro"];
                                if (obj != null && obj != DBNull.Value) model.testPro = obj.ToString();
                                //检测结果类型
                                obj = dt.Rows[j]["quanOrQual"];
                                if (obj != null && obj != DBNull.Value) model.quanOrQual = obj.ToString();
                                else model.quanOrQual = "2";
                                //检测值
                                obj = dt.Rows[j]["CheckValueInfo"];
                                if (obj != null && obj != DBNull.Value) model.contents = obj.ToString();
                                //检测值单位
                                obj = dt.Rows[j]["Unit"];
                                if (obj != null && obj != DBNull.Value) model.unit = obj.ToString();
                                //检测结果
                                obj = dt.Rows[j]["Result"];
                                if (obj != null && obj != DBNull.Value) model.testResult = obj.ToString();
                                //检测标准值
                                obj = dt.Rows[j]["StandValue"];
                                if (obj != null && obj != DBNull.Value) model.standardLimit = obj.ToString();
                                //检测依据
                                obj = dt.Rows[j]["Standard"];
                                if (obj != null && obj != DBNull.Value) model.basedStandard = obj.ToString();
                                //检测人姓名
                                model.testPerson = CurrentUser.GetInstance().UserInfo.Name;
                                //检测时间
                                obj = dt.Rows[j]["CheckStartDate"];
                                if (obj != null && obj != DBNull.Value) model.testTime = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                //抽检时间
                                obj = dt.Rows[j]["CheckStartDate"];
                                if (obj != null && obj != DBNull.Value) model.sampleTime = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                //处理结果
                                obj = dt.Rows[j]["Remark"];
                                if (obj != null && obj != DBNull.Value) model.remark = obj.ToString();

                                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.testTime +
                                    model.instrumentNo + model.contents + model.testResult);

                                string str = Global.AnHuiInterface.instrumentInfoHandle(model);
                                List<string> rtnList = Global.AnHuiInterface.ParsingUploadXML(str);
                                if (rtnList[0].Equals("1"))
                                {
                                    err = string.Empty;
                                    Global.UploadCount++;
                                    clsResult result = new clsResult();
                                    result.SysCode = sysCode;
                                    result.IsSended = true;
                                    result.IsReSended = true;
                                    result.SendedDate = DateTime.Now;
                                    result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
                                    _curObjectOpr.SetUploadFlag(result, out err);
                                }
                                else
                                    outErr += "样品名称：[" + model.fName + "] 上传失败！\r\n异常信息：" + rtnList[1] + "；\r\n\r\n";

                                if (count < 100)
                                {
                                    percent(count);
                                    percentage2 += percentage1;
                                    if (percentage2 > 1)
                                    {
                                        count += (int)percentage2;
                                        percentage2 = 0;
                                    }
                                }
                                else
                                {
                                    count = 100;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                outErr = e.Message;
            }
            finally
            {
                percent(100);
                if (outErr.Length == 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("数据上传成功！ \r\n\r\n本次共上传 " + Global.UploadCount + " 条数据。", "系统提示");
                }
                else if (outErr.Length > 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("数据上传失败！ \r\n\r\n失败原因： " + outErr, "系统提示");
                }
                else if (outErr.Length > 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("部分数据上传失败！ \r\n\r\n失败原因： " + outErr, "系统提示");
                }
                else if (outErr.Length == 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("选择的记录已上传！", "系统提示");
                }
            }
        }

        /// <summary>
        /// 安徽上传数据功能
        /// </summary>
        private void AnHuiUpload() 
        {
            //2016年11月1日 wenj 修改上传功能为按照选择的记录上传
            if (c1FlexGrid1.RowSel <= 0)
            {
                MessageBox.Show(this, "请选择将要上传的记录！");
                return;
            }

            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示");
                return;
            }

            Global.UploadCount = 0;
            string str = ConfigurationManager.AppSettings["AnHui_interfaceVersion"];
            Global.AnHuiInterface.interfaceVersion = str == null || str.Length == 0 ? "" : str;

            str = ConfigurationManager.AppSettings["AnHui_userName"];
            Global.AnHuiInterface.userName = str == null || str.Length == 0 ? "" : str;

            str = ConfigurationManager.AppSettings["AnHui_passWord"];
            Global.AnHuiInterface.passWord = str == null || str.Length == 0 ? "" : str;

            str = ConfigurationManager.AppSettings["AnHui_instrument"];
            Global.AnHuiInterface.instrument = str == null || str.Length == 0 ? "" : str;

            str = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            Global.AnHuiInterface.instrumentNo = str == null || str.Length == 0 ? "" : str;

            str = ConfigurationManager.AppSettings["AnHui_mac"];
            Global.AnHuiInterface.mac = str == null || str.Length == 0 ? "" : str;

            if (Global.AnHuiInterface.interfaceVersion.Length == 0 || Global.AnHuiInterface.userName.Length == 0 ||
                Global.AnHuiInterface.passWord.Length == 0 || Global.AnHuiInterface.instrument.Length == 0 ||
                Global.AnHuiInterface.instrumentNo.Length == 0 || Global.AnHuiInterface.mac.Length == 0)
            {
                MessageBox.Show(this, "请先到【基础数据同步】菜单中设置服务器相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.UploadAnHuiProcess;
            process.MessageInfo = "正在上传";
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();
        }

        /// <summary>
        /// 合肥上传数据功能
        /// </summary>
        private void HeFeiUpload()
        {        
            if (c1FlexGrid1.RowSel <= 0)
            {
                MessageBox.Show(this, "请选择将要上传的记录！");
                return;
            }

            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示");
                return;
            }

            Global.UploadCount = 0;
            if (Global.AnHuiInterface.ServerAddr.Length == 0)
            {
                MessageBox.Show(this, "请先到【基础数据同步】菜单中设置上传地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dlg = MessageBox.Show("是否确定上传选中数据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.No)
            {
                return;
            }

            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.UploadHeFeiProcess;
            process.MessageInfo = "正在上传";
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();
        }

        private void UploadHeFeiProcess(Action<int> percent)
        {
            percent(0);
            string sysCode = string.Empty, err = string.Empty, msg = string.Empty, outErr = string.Empty;
            int count = 0, uploadCount = 0;
            try
            {
                //从数据库中拿到全部数据，然后和选择的记录对比。
                DataTable dt = _curObjectOpr.GetUploadDataTable(" IsSended=false ", "A.SysCode", ShareOption.ApplicationTag);
                percent(3);
                uploadCount = c1FlexGrid1.Rows.Selected.Count;

                float percentage1 = (float)97 / (float)uploadCount, percentage2 = 0;
                count = (int)percentage1 + 3;
                object obj = null;
                FoodClient.AnHui.Global.AnHuiInterface.isok = 0;
                for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                {
                    sysCode = c1FlexGrid1[i, "SysCode"].ToString().Trim();
                    if (c1FlexGrid1.Rows[i].Selected == true && !Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsSended"]))
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["SysCode"].ToString().Trim().Equals(sysCode))
                            {
                                HeFeiUploadDataInfo model = new HeFeiUploadDataInfo();
                                //样品类别
                                obj = dt.Rows[j]["FoodTypeName"];
                                if (obj != null && obj != DBNull.Value) model.SampleType = obj.ToString();
                                //样品名称
                                obj = dt.Rows[j]["FoodName"];
                                if (obj != null && obj != DBNull.Value) model.SampleName = obj.ToString();
                                //送检单位
                                obj = dt.Rows[j]["SentCompany"];
                                if (obj != null && obj != DBNull.Value) model.SendUnit = obj.ToString();
                                //生产单位（产地？？）
                                obj = dt.Rows[j]["CheckedCompany"];
                                if (obj != null && obj != DBNull.Value) model.ProductionUnit = obj.ToString();
                                //产品生产单位 （被检单位）
                                obj = dt.Rows[j]["CheckedCompanyInfo"];
                                if (obj != null && obj != DBNull.Value) model.GoodsUnit = obj.ToString();
                                //检测单位
                                obj = dt.Rows[j]["CheckUnitName"];
                                if (obj != null && obj != DBNull.Value) model.CheckUnit = obj.ToString();
                                //检测单位编号
                                obj = dt.Rows[j]["CheckUnitCode"];
                                if (obj != null && obj != DBNull.Value) model.CheckUnitCode = obj.ToString();
                                //产品生产日期
                                obj = dt.Rows[j]["ProduceDate"];
                                if (obj != null && obj != DBNull.Value) model.GoodsDate = DateTime.Parse(obj.ToString());
                                //抑制率
                                obj = dt.Rows[j]["CheckValueInfo"];
                                if (obj != null && obj != DBNull.Value) model.InhibitionRatio = float.Parse(obj.ToString());// 100f;
                                //检测员
                                obj = dt.Rows[j]["Checker"];
                                if (obj != null && obj != DBNull.Value) model.Operator = obj.ToString();
                                //生产时间改
                                obj = dt.Rows[j]["ProduceDate"];
                                if (obj != null && obj != DBNull.Value) model.ProductionDate = DateTime.Parse(obj.ToString());
                                //产品是否合格                             
                                //obj = dt.Rows[j]["Result"];
                                //if (obj != null && obj != DBNull.Value) model.IsOk = obj.ToString().Equals(ShareOption.ResultEligi) ? true : false;
                                //结论                             

                                obj = dt.Rows[j]["Result"];
                                if (obj != null && obj != DBNull.Value) model.Conclusion = obj.ToString();
                                //检测时间
                                obj = dt.Rows[j]["CheckStartDate"];
                                if (obj != null && obj != DBNull.Value) model.Chktime = obj.ToString();
                                //检测项目

                                obj = dt.Rows[j]["MachineItemName"];
                                if (obj != null && obj != DBNull.Value) model.ChkItem = obj.ToString();
                                //样品编号
                                obj = dt.Rows[j]["SampleCode"];
                                if (obj != null && obj != DBNull.Value) model.SampleCode = obj.ToString();
                                //检测类别
                                obj = dt.Rows[j]["CheckType"];
                                if (obj != null && obj != DBNull.Value) model.leibie = obj.ToString();
                                HeFeiUploadDataInfo.uperr = "";

                                //上传
                                if (HeFeiUploadDataInfo.Upload(model))
                                {
                                    err = string.Empty;
                                    Global.UploadCount++;
                                    clsResult result = new clsResult();
                                    result.SysCode = sysCode;
                                    result.IsSended = true;
                                    result.IsReSended = true;
                                    result.SendedDate = DateTime.Now;
                                    result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
                                    _curObjectOpr.SetUploadFlag(result, out err);//TODO 这里只更新选择的记录
                                    //}
                                    //else
                                    //    outErr += "样品名称：[" + model.SampleName + "] 上传失败！";
                                }
                                else
                                {
                                    outErr += "样品名称：[" + model.SampleName + "] 上传失败！";
                                }

                                if (count < 100)
                                {
                                    percent(count);
                                    percentage2 += percentage1;
                                    if (percentage2 > 1)
                                    {
                                        count += (int)percentage2;
                                        percentage2 = 0;
                                    }
                                }
                                else
                                {
                                    count = 100;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                outErr = e.Message;
            }
            finally
            {
                percent(100);
                outErr+=HeFeiUploadDataInfo.uperr;
                if (outErr.Length == 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("数据上传成功！ \r\n\r\n本次共上传 " + FoodClient.AnHui.Global.AnHuiInterface.isok + " 条数据。", "系统提示");
                }
                else if (outErr.Length > 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("数据上传失败！ \r\n\r\n失败原因： " + outErr   , "系统提示");
                }
                else if (outErr.Length > 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("部分数据上传失败！ \r\n\r\n失败原因： " + outErr , "系统提示");
                }
                else if (outErr.Length == 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("选择的记录已上传！", "系统提示");
                }
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSend_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //发送至监管平台
            //frmResultSend send = new frmResultSend();
            //DialogResult dr = send.ShowDialog(this);

            //上传至青岛用户指定网络路径
            //UploadDatasToQD();

            ////上传至安徽平台
            //AnHuiUpload();
            //上传至合肥平台
            HeFeiUpload();
            refreshGrid(queryString);
        }

        /// <summary>
        /// 每周一检单一检测项目生成
        /// 2016年1月28日
        /// wenj
        /// </summary>
        private void MultipleReport_MZYJ() 
        {
            int reportNumber = 0, num = 0;
            for (int i = 0; i < 1; i++)
            {
                num = GenerateReport_MZYJ();
                if (num > 0)
                {
                    reportNumber += num;
                    i--;
                }
                else
                    break;
            }
            string strMessageBox = reportNumber > 0 ? "已成功生成 " + reportNumber + " 张报表！是否立即查看？！" : "所有报表已全部生成！是否立即查看？！";
            if (MessageBox.Show(strMessageBox, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                new ReportForm().Show();
        }

        /// <summary>
        /// 每周一检 多项目生成报表
        /// 2016年1月15日
        /// wenj
        /// </summary>
        private void MultipleReport_MZYJ_N() 
        {
            int reportNumber = 0, num = 0;
            for (int i = 0; i < 1; i++)
            {
                num = GenerateReport_MZYJ_N();
                if (num > 0)
                {
                    reportNumber += num;
                    i--;
                }
                else
                    break;
            }
            string strMessageBox = reportNumber > 0 ? "已成功生成 " + reportNumber + " 张报表！是否立即查看？！" : "所有报表已全部生成！是否立即查看？！";
            if (MessageBox.Show(strMessageBox, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                new ReportForm().Show();
        }

        /// <summary>
        /// 深圳报表生成 
        /// 2016年1月15日
        /// wenj
        /// </summary>
        public void MultipleReport_SZ()
        {
            int reportNumber = 0, num = 0;
            for (int i = 0; i < 1; i++)
            {
                num = GenerateReport_SZ();
                if (num > 0)
                {
                    reportNumber += num;
                    i--;
                }
                else
                    break;
            }
            string strMessageBox = reportNumber > 0 ? "已成功生成 " + reportNumber + " 张报表！是否立即查看？！" : "所有报表已全部生成！是否立即查看？！";
            if (MessageBox.Show(strMessageBox, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                new ReportForm().Show();
        }

        /// <summary>
        /// 生成报表 - 选择单一项目OR多项目
        /// </summary>
        private void c1CommandReport_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strType = System.Configuration.ConfigurationManager.AppSettings["ReportsType"].ToString();
            if (strType.Equals("MZYJ"))
            {
                MultipleReport_MZYJ();
            }
            else if (strType.Equals("SZ"))
            {
                MultipleReport_SZ();
            }
            else if (strType.Equals("MZYJ_N"))
            {
                MultipleReport_MZYJ_N();
            }
        }

        /// <summary>
        /// 深圳报表生成
        /// 2016年1月15日
        /// wenj
        /// </summary>
        /// <returns></returns>
        private int GenerateReport_SZ()
        {
            int reportNumber = 0;
            Dictionary<string, clsReportSZ> dicReprot = new Dictionary<string, clsReportSZ>();
            List<string> strKey = new List<string>();
            Dictionary<string, string> dicString = new Dictionary<string, string>();
            List<string> strSysCode = new List<string>();
            string sysCode = "", sErr = "";
            for (int i = 0; i <= c1FlexGrid1.BottomRow; i++)
            {
                sysCode = c1FlexGrid1.Rows[i]["SysCode"].ToString();
                string strWhere = "SysCode = '" + sysCode + "' And IsReport = 'N'";
                //验证是否已经生成报表，若已生成则跳过
                if (_clsResultOpr.IsExist(strWhere))
                {
                    if (!dicString.ContainsKey(sysCode))
                    {
                        dicString.Add(sysCode, sysCode);
                        strSysCode.Add(sysCode);
                    }
                    strWhere = "SysCode = '" + sysCode + "'";
                    DataTable dt = _curObjectOpr.SearchResult(strWhere);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsResultSZ> report = (List<clsResultSZ>)StringUtil.DataTableToIList<clsResultSZ>(dt, 1);
                        //key=受检单位_抽样日期
                        string key = c1FlexGrid1.Rows[i]["CheckedCompanyName"].ToString() + "_" + c1FlexGrid1.Rows[i]["TakeDate"].ToString();
                        if (!dicReprot.ContainsKey(key))
                        {
                            //主表信息
                            clsReportSZ model = new clsReportSZ();
                            DataTable dtUserUnit = new clsUserUnitOpr().GetAsDataTable(string.Format("A.SysCode='{0}'", ShareOption.DefaultUserUnitCode), "", 0);
                            string reportName = dtUserUnit.Rows[0]["FULLNAME"].ToString();
                            model.ReportName = reportName.Length > 0 ? reportName : "食品药品监督管理局食品快速检测工作单";//报表名称
                            model.CheckedCompany = c1FlexGrid1.Rows[i]["CheckedCompanyName"].ToString();//受检人/被检单位
                            model.Contact = report[0].Contact;//联系人
                            model.ContactPhone = report[0].ContactPhone;//联系人电话
                            model.CheckedCompanyArea = report[0].Area;//被检单位行政区域
                            model.SamplingData = c1FlexGrid1.Rows[i]["TakeDate"].ToString();//抽样日期
                            model.SamplingPerson = c1FlexGrid1.Rows[i]["OrganizerName"].ToString();//抽样人
                            model.CheckUser = c1FlexGrid1.Rows[i]["CheckerName"].ToString();//检测人

                            //子表信息
                            clsReportSZ.clsReportDetailSZ modelDetail = new clsReportSZ.clsReportDetailSZ();
                            string SampleCode = c1FlexGrid1.Rows[i]["FoodCode"].ToString();
                            modelDetail.Code = SampleCode;//编码
                            modelDetail.SampleName = clsFoodClassOpr.NameFromCode(SampleCode);//样品名称
                            modelDetail.SampleBase = c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString();//抽样基数
                            modelDetail.SampleSource = report[0].SampleSource;//样品来源
                            modelDetail.Result = c1FlexGrid1.Rows[i]["Result"].ToString();//结论
                            modelDetail.SysCode = sysCode;
                            modelDetail.SampleAmount = report[0].SampleAmount;//抽样金额
                            modelDetail.SampleNumber = report[0].SampleNum;//抽样数量
                            modelDetail.Price = report[0].Price.ToString();//价格
                            modelDetail.ChekcedValue = report[0].CheckValueInfo;//实测值
                            modelDetail.StandardValue = report[0].StandValue;//标准值
                            modelDetail.Note = report[0].Notes;//备注
                            modelDetail.IsDestruction = report[0].IsDestruction;//是否主动销毁
                            modelDetail.DestructionKG = report[0].DestructionKG;//销毁重量
                            model.reportList.Add(modelDetail);
                            dicReprot.Add(key, model);
                            strKey.Add(key);
                        }
                        else
                        {
                            //子表信息
                            clsReportSZ.clsReportDetailSZ modelDetail = new clsReportSZ.clsReportDetailSZ();
                            string SampleCode = c1FlexGrid1.Rows[i]["FoodCode"].ToString();
                            modelDetail.Code = SampleCode;//编码
                            modelDetail.SampleName = clsFoodClassOpr.NameFromCode(SampleCode);//样品名称
                            modelDetail.SampleBase = c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString();//抽样基数
                            modelDetail.SampleSource = report[0].SampleSource;//样品来源
                            modelDetail.Result = c1FlexGrid1.Rows[i]["Result"].ToString();//结论
                            modelDetail.SysCode = sysCode;
                            modelDetail.SampleAmount = report[0].SampleAmount;//抽样金额
                            modelDetail.SampleNumber = report[0].SampleNum;//抽样数量
                            modelDetail.Price = report[0].Price.ToString();//价格
                            modelDetail.ChekcedValue = report[0].CheckValueInfo;//实测值
                            modelDetail.StandardValue = report[0].StandValue;//标准值
                            modelDetail.Note = report[0].Notes;//备注
                            modelDetail.IsDestruction = report[0].IsDestruction;//是否主动销毁
                            modelDetail.DestructionKG = report[0].DestructionKG;//销毁重量
                            dicReprot[key].reportList.Add(modelDetail);
                        }
                    }
                }
            }
            if (dicReprot.Count > 0)
            {
                clsReportSZ report = new clsReportSZ();
                for (int i = 0; i < dicReprot.Count; i++)
                {
                    report = dicReprot[strKey[i]];
                    DataTable dt = _curObjectOpr.Insert(report, out sErr);
                    if (dt != null && dt.Rows.Count > 0 && "".Equals(sErr))
                    {
                        List<clsReportSZ> reportsz = (List<clsReportSZ>)StringUtil.DataTableToIList<clsReportSZ>(dt, 1);
                        int reportID = reportsz[0].ID;
                        for (int j = 0; j < report.reportList.Count; j++)
                        {
                            clsReportSZ.clsReportDetailSZ reportDetail = new clsReportSZ.clsReportDetailSZ();
                            reportDetail = report.reportList[j];
                            reportDetail.ReportID = reportID;
                            _curObjectOpr.Insert(reportDetail, out sErr);
                        }
                    }
                    if ("".Equals(sErr))
                        reportNumber += 1;
                }
                for (int i = 0; i < strSysCode.Count; i++)
                {
                    clsResult result = new clsResult();
                    result.SysCode = strSysCode[i];
                    result.IsReport = "Y";
                    _curObjectOpr.UpdatePartReport(result, out sErr);
                }
            }
            return reportNumber;
        }

        /// <summary>
        /// 每周一检报表生成
        /// 单一检测项目
        /// </summary>
        /// <returns></returns>
        private int GenerateReport_MZYJ()
        {
            int reportNumber = 0;
            string sysCode = "", sErr = "";
            clsReport model = new clsReport();
            for (int i = 0; i <= c1FlexGrid1.BottomRow; i++)
            {
                double doub = 0;
                string str = "";
                sysCode = c1FlexGrid1.Rows[i]["SysCode"].ToString();
                string strWhere = "SysCode = '" + sysCode + "' And IsReport = 'N'";
                //验证是否已经生成报表，若已生成则跳过
                if (_clsResultOpr.IsExist(strWhere))
                {
                    model = new clsReport();
                    model.CheckedCompany = c1FlexGrid1.Rows[i]["CheckedCompanyName"].ToString();//受检人/被检单位
                    List<string> strCompanyList = new List<string>();
                    strCompanyList = clsCompanyOpr.GetCompanyByName(model.CheckedCompany.Trim());
                    model.Address = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[0] : "";//经营地址
                    model.BusinessNature = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[2] : "";//经营性质
                    model.BusinessLicense = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[1] : "";//营业执照
                    model.Contact = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[3] : "";//联系人
                    model.ContactPhone = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[4] : "";//联系人电话
                    model.ZipCode = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[5] : "";//邮编
                    model.Fax = "";//传真
                    string SampleCode = c1FlexGrid1.Rows[i]["FoodCode"].ToString();
                    model.ProductName = clsFoodClassOpr.NameFromCode(SampleCode);//商品名称
                    str = c1FlexGrid1.Rows[i]["Price"].ToString();
                    if (double.TryParse(str, out doub))
                        str = doub.ToString("f2");
                    model.ProductPrice = str + (str.Trim().Equals("") ? "" : "元"); //商品售价
                    model.Specifications = c1FlexGrid1.Rows[i]["SampleModel"].ToString();//规格型号
                    model.QualityGrade = c1FlexGrid1.Rows[i]["SampleLevel"].ToString();//质量等级
                    model.BatchNumber = c1FlexGrid1.Rows[i]["SampleState"].ToString();//批号或编号
                    model.RegisteredTrademark = c1FlexGrid1.Rows[i]["Provider"].ToString();//注册商标
                    if (double.TryParse(c1FlexGrid1.Rows[i]["SampleNum"].ToString(), out doub))
                        model.SamplingNumber = doub.ToString("f2");//抽样数量
                    else
                        model.SamplingNumber = c1FlexGrid1.Rows[i]["SampleNum"].ToString();
                    if (double.TryParse(c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString(), out doub))
                        model.SamplingBase = doub.ToString("f2");//抽样基数
                    else
                        model.SamplingNumber = c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString();
                    if (double.TryParse(c1FlexGrid1.Rows[i]["ImportNum"].ToString(), out doub))
                        model.IntoNumber = doub.ToString("f2");//进货数量
                    else
                        model.IntoNumber = c1FlexGrid1.Rows[i]["ImportNum"].ToString();//进货数量
                    model.Implementation = "";//商品进货验收制度执行情况
                    if (double.TryParse(c1FlexGrid1.Rows[i]["SaveNum"].ToString(), out doub))
                        model.InventoryNubmer = doub.ToString("f2");//库存数量
                    else
                        model.InventoryNubmer = c1FlexGrid1.Rows[i]["SaveNum"].ToString();//库存数量
                    model.Notes = c1FlexGrid1.Rows[i]["Notes"].ToString();//备注
                    model.SamplingData = c1FlexGrid1.Rows[i]["TakeDate"].ToString();//抽样日期
                    model.SamplingPerson = c1FlexGrid1.Rows[i]["OrganizerName"].ToString();//抽样人
                    model.SamplingCode = c1FlexGrid1.Rows[i]["SampleCode"].ToString();//样品编号
                    model.NoteUnder = "";//备注
                    model.ApprovedUser = c1FlexGrid1.Rows[i]["AssessorName"].ToString();//核准人
                    DataTable dtUserUnit = new clsUserUnitOpr().GetAsDataTable(string.Format("A.SysCode='{0}'", ShareOption.DefaultUserUnitCode), "", 0);
                    string reportName = dtUserUnit.Rows[0]["FULLNAME"].ToString();
                    model.ReportName = reportName.Length > 0 ? reportName : "食品药品监督管理局食品快速检测工作单";
                    model.Unit = c1FlexGrid1.Rows[i]["Unit"].ToString();
                    model.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    model.ID = DateTime.Now.Ticks.ToString() + "_" + Environment.TickCount.ToString();
                    _curObjectOpr.Insert(model, out sErr);

                    //报表子表
                    clsReport.ReportDetail reportDetail = new clsReport.ReportDetail();
                    reportDetail.CheckItem = c1FlexGrid1.Rows[i]["CheckTotalItemName"].ToString();//检测项目
                    reportDetail.CheckBasis = c1FlexGrid1.Rows[i]["StandardName"].ToString();//检测依据
                    if (double.TryParse(c1FlexGrid1.Rows[i]["StandValue"].ToString(), out doub))
                        reportDetail.StandardValues = doub.ToString("f2");//标准值
                    else
                        reportDetail.StandardValues = c1FlexGrid1.Rows[i]["StandValue"].ToString();//标准值
                    if (double.TryParse(c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString(), out doub))
                        reportDetail.CheckValues = doub.ToString("f2");//实测值
                    else
                        reportDetail.CheckValues = c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString();//实测值
                    reportDetail.Conclusion = c1FlexGrid1.Rows[i]["Result"].ToString();//结论
                    reportDetail.CheckData = c1FlexGrid1.Rows[i]["CheckStartDate"].ToString();//检测日期
                    reportDetail.CheckUser = c1FlexGrid1.Rows[i]["CheckerName"].ToString();//检测人
                    reportDetail.Unit = c1FlexGrid1.Rows[i]["ResultInfo"].ToString();//单位
                    reportDetail.SysCode = sysCode;
                    model.reportList.Add(reportDetail);

                    if (sErr.Equals(""))
                    {
                        reportDetail.ReportID = model.ID;
                        _curObjectOpr.Insert(reportDetail, out sErr);
                        if (sErr.Equals(""))
                        {
                            reportNumber += 1;
                            clsResult result = new clsResult();
                            result.SysCode = sysCode;
                            result.IsReport = "Y";
                            _curObjectOpr.UpdatePartReport(result, out sErr);

                        }
                    }
                }
            }
            return reportNumber;
        }

        /// <summary>
        /// 每周一检报表生成
        /// 每张报表最多四条数据，溢出时生成2张或更多张报表
        /// </summary>
        /// <returns></returns>
        private int GenerateReport_MZYJ_N() 
        {
            int reportNumber = 0;
            Dictionary<string, clsReport> dicReprot = new Dictionary<string, clsReport>();
            List<string> strKey = new List<string>();
            Dictionary<string, string> dicString = new Dictionary<string, string>();
            List<string> strSysCode = new List<string>();
            string sysCode = "", sErr = "";
            clsReport model = new clsReport();
            for (int i = 0; i <= c1FlexGrid1.BottomRow; i++)
            {
                double doub = 0;
                string str = "";
                sysCode = c1FlexGrid1.Rows[i]["SysCode"].ToString();
                string strWhere = "SysCode = '" + sysCode + "' And IsReport = 'N'";
                //验证是否已经生成报表，若已生成则跳过
                if (_clsResultOpr.IsExist(strWhere))
                {
                    if (!dicString.ContainsKey(sysCode))
                    {
                        dicString.Add(sysCode, sysCode);
                        strSysCode.Add(sysCode);
                    }
                    model = new clsReport();
                    //key=受检人+样品名称+抽样日期
                    string key = c1FlexGrid1.Rows[i]["CheckedCompany"].ToString() + "_" + c1FlexGrid1.Rows[i]["FoodCode"].ToString() + "_" + c1FlexGrid1.Rows[i]["TakeDate"].ToString();
                    if (!dicReprot.ContainsKey(key))
                    {
                        model.CheckedCompany = c1FlexGrid1.Rows[i]["CheckedCompanyName"].ToString();//受检人/被检单位
                        List<string> strCompanyList = new List<string>();
                        strCompanyList = clsCompanyOpr.GetCompanyByName(model.CheckedCompany.Trim());
                        model.Address = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[0] : "";//经营地址
                        model.BusinessNature = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[2] : "";//经营性质
                        model.BusinessLicense = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[1] : "";//营业执照
                        model.Contact = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[3] : "";//联系人
                        model.ContactPhone = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[4] : "";//联系人电话
                        model.ZipCode = (strCompanyList != null && strCompanyList.Count > 0) ? strCompanyList[5] : "";//邮编
                        model.Fax = "";//传真
                        string SampleCode = c1FlexGrid1.Rows[i]["FoodCode"].ToString();
                        model.ProductName = clsFoodClassOpr.NameFromCode(SampleCode);//商品名称
                        str = c1FlexGrid1.Rows[i]["Price"].ToString();
                        if (double.TryParse(str, out doub))
                            str = doub.ToString("f2");
                        model.ProductPrice = str + (str.Trim().Equals("") ? "" : "元"); //商品售价
                        model.Specifications = c1FlexGrid1.Rows[i]["SampleModel"].ToString();//规格型号
                        model.QualityGrade = c1FlexGrid1.Rows[i]["SampleLevel"].ToString();//质量等级
                        model.BatchNumber = c1FlexGrid1.Rows[i]["SampleState"].ToString();//批号或编
                        model.RegisteredTrademark = c1FlexGrid1.Rows[i]["Provider"].ToString();//注册商标
                        if (double.TryParse(c1FlexGrid1.Rows[i]["SampleNum"].ToString(), out doub))
                            model.SamplingNumber = doub.ToString("f2");//抽样数量
                        else
                            model.SamplingNumber = c1FlexGrid1.Rows[i]["SampleNum"].ToString();
                        if (double.TryParse(c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString(), out doub))
                            model.SamplingBase = doub.ToString("f2");//抽样基数
                        else
                            model.SamplingNumber = c1FlexGrid1.Rows[i]["SampleBaseNum"].ToString();
                        if (double.TryParse(c1FlexGrid1.Rows[i]["ImportNum"].ToString(), out doub))
                            model.IntoNumber = doub.ToString("f2");//进货数量
                        else
                            model.IntoNumber = c1FlexGrid1.Rows[i]["ImportNum"].ToString();//进货数量
                        model.Implementation = "";//商品进货验收制度执行情况
                        if (double.TryParse(c1FlexGrid1.Rows[i]["SaveNum"].ToString(), out doub))
                            model.InventoryNubmer = doub.ToString("f2");//库存数量
                        else
                            model.InventoryNubmer = c1FlexGrid1.Rows[i]["SaveNum"].ToString();//库存数量
                        model.Notes = c1FlexGrid1.Rows[i]["Notes"].ToString();//备注
                        model.SamplingData = c1FlexGrid1.Rows[i]["TakeDate"].ToString();//抽样日期
                        model.SamplingPerson = c1FlexGrid1.Rows[i]["OrganizerName"].ToString();//抽样人
                        model.SamplingCode = c1FlexGrid1.Rows[i]["SampleCode"].ToString();//样品编号
                        model.NoteUnder = "";//备注
                        model.ApprovedUser = c1FlexGrid1.Rows[i]["AssessorName"].ToString();//核准人
                        DataTable dtUserUnit = new clsUserUnitOpr().GetAsDataTable(string.Format("A.SysCode='{0}'", ShareOption.DefaultUserUnitCode), "", 0);
                        string reportName = dtUserUnit.Rows[0]["FULLNAME"].ToString();
                        model.ReportName = reportName.Length > 0 ? reportName : "食品药品监督管理局食品快速检测工作单";//报表名称
                        model.Unit = c1FlexGrid1.Rows[i]["Unit"].ToString();
                        model.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        //报表子表
                        clsReport.ReportDetail reportDetail = new clsReport.ReportDetail();
                        reportDetail.CheckItem = c1FlexGrid1.Rows[i]["CheckTotalItemName"].ToString();//检测项目
                        reportDetail.CheckBasis = c1FlexGrid1.Rows[i]["StandardName"].ToString();//检测依据
                        if (double.TryParse(c1FlexGrid1.Rows[i]["StandValue"].ToString(), out doub))
                            reportDetail.StandardValues = doub.ToString("f2");//标准值
                        else
                            reportDetail.StandardValues = c1FlexGrid1.Rows[i]["StandValue"].ToString();//标准值
                        if (double.TryParse(c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString(), out doub))
                            reportDetail.CheckValues = doub.ToString("f2");//实测值
                        else
                            reportDetail.CheckValues = c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString();//实测值
                        reportDetail.Conclusion = c1FlexGrid1.Rows[i]["Result"].ToString();//结论
                        reportDetail.CheckData = c1FlexGrid1.Rows[i]["CheckStartDate"].ToString();//检测日期
                        reportDetail.CheckUser = c1FlexGrid1.Rows[i]["CheckerName"].ToString();//检测人
                        reportDetail.Unit = c1FlexGrid1.Rows[i]["ResultInfo"].ToString();//单位
                        reportDetail.SysCode = sysCode;
                        model.reportList.Add(reportDetail);
                        dicReprot.Add(key, model);
                        strKey.Add(key);
                    }
                    else
                    {
                        //2016年1月4日 wenj key相同时，自动生成的报表中不允许超过四条数据。溢出部分生成下一张报表。
                        if (dicReprot[key].reportDetailList.Count < 4)
                        {
                            clsReport.ReportDetail reportDetail = new clsReport.ReportDetail();
                            reportDetail.CheckItem = c1FlexGrid1.Rows[i]["CheckTotalItemName"].ToString();//检测项目
                            reportDetail.CheckBasis = c1FlexGrid1.Rows[i]["StandardName"].ToString();//检测依据
                            if (double.TryParse(c1FlexGrid1.Rows[i]["StandValue"].ToString(), out doub))
                                reportDetail.StandardValues = doub.ToString("f2");//标准值
                            else
                                reportDetail.StandardValues = c1FlexGrid1.Rows[i]["StandValue"].ToString();//标准值
                            if (double.TryParse(c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString(), out doub))
                                reportDetail.CheckValues = doub.ToString("f2");//实测值
                            else
                                reportDetail.CheckValues = c1FlexGrid1.Rows[i]["CheckValueInfo"].ToString();//实测值
                            reportDetail.Conclusion = c1FlexGrid1.Rows[i]["Result"].ToString();//结论
                            reportDetail.CheckData = c1FlexGrid1.Rows[i]["CheckStartDate"].ToString();//检测日期
                            reportDetail.CheckUser = c1FlexGrid1.Rows[i]["CheckerName"].ToString();//检测人
                            reportDetail.Unit = c1FlexGrid1.Rows[i]["ResultInfo"].ToString();
                            reportDetail.SysCode = sysCode;
                            dicReprot[key].reportList.Add(reportDetail);
                        }
                        else
                        {
                            strSysCode.Remove(sysCode);
                        }
                    }
                }
            }
            if (dicReprot.Count > 0)
            {
                clsReport report = new clsReport();
                for (int i = 0; i < dicReprot.Count; i++)
                {
                    report = dicReprot[strKey[i]];
                    report.ID = DateTime.Now.Ticks.ToString() + "_" + Environment.TickCount.ToString();
                    _curObjectOpr.Insert(report, out sErr);
                    if ("".Equals(sErr))
                    {
                        for (int j = 0; j < report.reportList.Count; j++)
                        {
                            clsReport.ReportDetail reportDetail = new clsReport.ReportDetail();
                            reportDetail = report.reportList[j];
                            reportDetail.ReportID = report.ID;
                            _curObjectOpr.Insert(reportDetail, out sErr);
                        }
                    }
                    if ("".Equals(sErr))
                        reportNumber += 1;
                }
                for (int i = 0; i < strSysCode.Count; i++)
                {
                    clsResult result = new clsResult();
                    result.SysCode = strSysCode[i];
                    result.IsReport = "Y";
                    _curObjectOpr.UpdatePartReport(result, out sErr);
                }
            }
            return reportNumber;
        }

        /// <summary>
        /// 上传至用户指定网络路径
        /// 上传青岛客户提的需求
        /// </summary>
        private void UploadDatasToQD()
        {
            int num = 0;
            //全部上传
            for (int i = 1; i <= c1FlexGrid1.BottomRow; i++)
            {
                StringBuilder strB = new StringBuilder();
                strB.Append("<?xml version=\"1.0\" encoding=\"gb2312\"?>\r\n<results>\r\n  ");
                strB.Append("<record ");
                //样品编号
                string sample_no = c1FlexGrid1.Rows[i]["FoodCode"].ToString();
                strB.Append("sample_no=\"" + sample_no + "\" ");

                //检测方法
                string check_method = c1FlexGrid1.Rows[i]["ResultType"].ToString();
                strB.Append("check_method=\"" + check_method + "\" ");

                //样品类别
                string sub_category = clsFoodClassOpr.SearchTypeNameByCode(sample_no);
                strB.Append("sub_category=\"" + sub_category + "\" ");

                //样品名称
                string sample = clsFoodClassOpr.NameFromCode(sample_no);
                strB.Append("sample=\"" + sample + "\" ");

                //检测项目
                string check_item = c1FlexGrid1.Rows[i]["CheckTotalItemName"].ToString();
                strB.Append("check_item=\"" + check_item + "\" ");

                //检测判定结果
                string result = c1FlexGrid1.Rows[i]["Result"].ToString();
                strB.Append("result=\"" + result + "\" ");

                //检测单位
                string check_org = c1FlexGrid1.Rows[i]["CheckUnitCode"].ToString();
                strB.Append("check_org=\"" + clsUserUnitOpr.GetNameFromCode(check_org) + "\" ");

                //所属地区
                string region = c1FlexGrid1.Rows[i]["UpperCompanyName"].ToString();
                strB.Append("region=\"" + region + "\" ");

                //检测任务
                string check_district = "0";
                strB.Append("check_district=\"" + check_district + "\" ");

                //生产单位 
                string produce_org = c1FlexGrid1.Rows[i]["ProduceCompany"].ToString();
                strB.Append("produce_org=\"" + clsCompanyOpr.NameFromCode(produce_org) + "\" ");

                //被检单位 
                string checked_org = c1FlexGrid1.Rows[i]["CheckedCompanyName"].ToString();
                strB.Append("checked_org=\"" + checked_org + "\" ");

                //被检单位类别
                string checked_org_type = "0";
                strB.Append("checked_org_type=\"" + checked_org_type + "\" ");

                //检测结果
                string rate = c1FlexGrid1.Rows[i]["CheckederVal"].ToString().Equals("合格") ? "1" : "0";
                strB.Append("rate=\"" + rate + "\" ");

                //检测时间
                string check_time = c1FlexGrid1.Rows[i]["CheckStartDate"].ToString();
                strB.Append("check_time=\"" + check_time + "\" ");

                //检测人
                string Operator = c1FlexGrid1.Rows[i]["Organizer"].ToString();
                strB.Append("operator=\"" + clsUserInfoOpr.NameFromCode(Operator) + "\" ");

                //仪器编号 
                string hardware_no = c1FlexGrid1.Rows[i]["CheckMachine"].ToString();
                string name = clsMachineOpr.GetMachineNameFromCode(hardware_no);
                if (name.Length > 5)
                {
                    name = name.Substring(0, 10);
                }
                strB.Append("hardware_no=\"" + name + "\" ");

                //单位
                string unit = c1FlexGrid1.Rows[i]["ResultInfo"].ToString();
                strB.Append("unit=\"" + unit + "\" ");

                strB.Append("/>\r\n");
                strB.Append("</results>");
                string xml = strB.ToString();
                if (upDown(xml)) num += 1;
            }
            if (num > 0)
            {
                MessageBox.Show("成功上传: " + num + " 条数据");
            }
        }

        private bool upDown(string xml)
        {
            try
            {
                System.GC.Collect();
                string strUrl = "", strAcc = "", strPass = "";
                object objUrl = System.Configuration.ConfigurationManager.AppSettings["ConnectionAddress"];
                if (objUrl != null)
                    strUrl = objUrl.ToString();
                else
                    return false;

                object objAccount = System.Configuration.ConfigurationManager.AppSettings["Account"];
                string[] strAccL = null;
                if (objAccount != null)
                {
                    strAcc = objAccount.ToString();
                    strAccL = strAcc.Split(new char[] { ',' });
                }
                else
                    return false;

                object objPassWord = System.Configuration.ConfigurationManager.AppSettings["PassWord"];
                string[] strAccP = null;
                if (objPassWord != null)
                {
                    strPass = objPassWord.ToString();
                    strAccP = strPass.Split(new char[] { ',' });
                }
                else
                    return false;

                byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(xml);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(strUrl);
                webReq.Method = "POST";
                webReq.Headers.Add(strAccL[0], strAccL[1]);
                webReq.Headers.Add(strAccP[0], strAccP[1]);
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = buffer.Length;
                webReq.Proxy = null;
                Stream postData = webReq.GetRequestStream();
                //上传流
                postData.Write(buffer, 0, buffer.Length);
                if (webReq != null)
                    webReq.Abort();
                if (postData != null)
                    postData.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="row"></param>
        private void GetModel(int row)
        {
            _resultModel = new clsResult();
            _resultModel.SysCode = c1FlexGrid1.Rows[row]["SysCode"].ToString();
            _resultModel.IsSended = Convert.ToBoolean(c1FlexGrid1.Rows[row]["IsSended"]);
            _resultModel.ResultType = c1FlexGrid1.Rows[row]["ResultType"].ToString();
            _resultModel.CheckNo = c1FlexGrid1.Rows[row]["CheckNo"].ToString();
            _resultModel.StdCode = c1FlexGrid1.Rows[row]["StdCode"].ToString();
            _resultModel.SampleCode = c1FlexGrid1.Rows[row]["SampleCode"].ToString();
            _resultModel.CheckedCompany = c1FlexGrid1.Rows[row]["CheckedCompany"].ToString();
            _resultModel.CheckedCompanyName = c1FlexGrid1.Rows[row]["CheckedCompanyName"].ToString();
            _resultModel.CheckedComDis = c1FlexGrid1.Rows[row]["CheckedComDis"].ToString();
            _resultModel.CheckPlaceCode = c1FlexGrid1.Rows[row]["CheckPlaceCode"].ToString();
            _resultModel.FoodCode = c1FlexGrid1.Rows[row]["FoodCode"].ToString();
            if (c1FlexGrid1.Rows[row]["ProduceDate"] != null && c1FlexGrid1.Rows[row]["ProduceDate"] != DBNull.Value)
            {
                _resultModel.ProduceDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["ProduceDate"]);
            }
            _resultModel.ProduceCompany = c1FlexGrid1.Rows[row]["ProduceCompany"].ToString();
            _resultModel.ProducePlace = c1FlexGrid1.Rows[row]["ProducePlace"].ToString();
            _resultModel.SentCompany = c1FlexGrid1.Rows[row]["SentCompany"].ToString();
            _resultModel.Provider = c1FlexGrid1.Rows[row]["Provider"].ToString();
            _resultModel.TakeDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["TakeDate"]);
            _resultModel.CheckStartDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["CheckStartDate"]);
            _resultModel.ImportNum = c1FlexGrid1.Rows[row]["ImportNum"].ToString();
            _resultModel.SaveNum = c1FlexGrid1.Rows[row]["SaveNum"].ToString();
            _resultModel.Unit = c1FlexGrid1.Rows[row]["Unit"].ToString();
            if (!c1FlexGrid1.Rows[row]["SampleNum"].ToString().Equals(""))
            {
                _resultModel.SampleNum = c1FlexGrid1.Rows[row]["SampleNum"].ToString();
            }
            else
            {
                _resultModel.SampleNum = string.Empty;
            }
            if (!c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString().Equals(""))
            {
                _resultModel.SampleBaseNum = c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString();
            }
            else
            {
                _resultModel.SampleBaseNum = string.Empty;
            }
            _resultModel.SampleUnit = c1FlexGrid1.Rows[row]["SampleUnit"].ToString();
            _resultModel.SampleLevel = c1FlexGrid1.Rows[row]["SampleLevel"].ToString();
            _resultModel.SampleModel = c1FlexGrid1.Rows[row]["SampleModel"].ToString();
            _resultModel.SampleState = c1FlexGrid1.Rows[row]["SampleState"].ToString();
            _resultModel.CheckMachine = c1FlexGrid1.Rows[row]["CheckMachine"].ToString();
            _resultModel.CheckTotalItem = c1FlexGrid1.Rows[row]["CheckTotalItem"].ToString();
            _resultModel.Standard = c1FlexGrid1.Rows[row]["Standard"].ToString();
            _resultModel.CheckValueInfo = c1FlexGrid1.Rows[row]["CheckValueInfo"].ToString();
            _resultModel.StandValue = c1FlexGrid1.Rows[row]["StandValue"].ToString();
            _resultModel.Result = c1FlexGrid1.Rows[row]["Result"].ToString();
            _resultModel.ResultInfo = c1FlexGrid1.Rows[row]["ResultInfo"].ToString();
            _resultModel.UpperCompany = c1FlexGrid1.Rows[row]["UpperCompany"].ToString();
            _resultModel.UpperCompanyName = c1FlexGrid1.Rows[row]["UpperCompanyName"].ToString();
            _resultModel.OrCheckNo = c1FlexGrid1.Rows[row]["OrCheckNo"].ToString();
            _resultModel.CheckType = c1FlexGrid1.Rows[row]["CheckType"].ToString();
            _resultModel.CheckUnitCode = c1FlexGrid1.Rows[row]["CheckUnitCode"].ToString();
            _resultModel.Checker = c1FlexGrid1.Rows[row]["Checker"].ToString();
            _resultModel.Assessor = c1FlexGrid1.Rows[row]["Assessor"].ToString();
            _resultModel.Organizer = c1FlexGrid1.Rows[row]["Organizer"].ToString();
            _resultModel.Remark = c1FlexGrid1.Rows[row]["Remark"].ToString();
            _resultModel.CheckPlanCode = c1FlexGrid1.Rows[row]["CheckPlanCode"].ToString();
            _resultModel.SaleNum = c1FlexGrid1.Rows[row]["SaleNum"].ToString();
            _resultModel.Price = c1FlexGrid1.Rows[row]["Price"].ToString();
            _resultModel.CheckederVal = c1FlexGrid1.Rows[row]["CheckederVal"].ToString();
            _resultModel.IsSentCheck = c1FlexGrid1.Rows[row]["IsSentCheck"].ToString();
            _resultModel.CheckederRemark = c1FlexGrid1.Rows[row]["CheckederRemark"].ToString();
            _resultModel.Notes = c1FlexGrid1.Rows[row]["Notes"].ToString();
            _resultModel.MachineItemName = c1FlexGrid1.Rows[row]["CheckTotalItemName"].ToString();
        }

        /// <summary>
        /// 查看明细时
        /// </summary>
        private void seeDetailInfo()
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            GetModel(row);
            frmPesticideMeasureEdit frm = new frmPesticideMeasureEdit(_queryType);
            frm.Tag = "MX";
            frm.setValue(_resultModel);
            frm.Text = getEditTitle("明细");
            frm.ShowDialog(this);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 查看明细点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd02_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            seeDetailInfo();
        }

        private void c1CommandMenu2_Select(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(c1ToolBar1, c1CommandMenu2.Text);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdEdit.Text);
        }

        private void cmdDelete_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdDelete.Text);
        }

        private void cmdQuery_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdQuery.Text);
        }

        private void cmdSend_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdSend.Text);
        }

        private void c1CommandMenu1_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1CommandMenu1.Text);
        }

        private void cmdAdd02_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdAdd02.Text);
        }

        private void cmdPrint_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdPrint.Text);
        }

        private void cmdExit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdExit.Text);
        }
        private void c1Command2_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1Command2.Text);
        }
        private void c1CommandReport_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1CommandReport.Text);
        }

        ///// <summary>
        ///// 导出word格式
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void c1Command3_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        //{
        //    this.saveFileDialog1.InitialDirectory=Application.StartupPath;
        //    this.saveFileDialog1.CheckPathExists=true;
        //    this.saveFileDialog1.DefaultExt="doc";
        //    this.saveFileDialog1.Filter="Word文件(*.doc)|*.doc|All files (*.*)|*.*";
        //    DialogResult dr=this.saveFileDialog1.ShowDialog(this); 
        //    if(dr==DialogResult.OK)
        //    {
        //        try
        //        {
        //            c1FlexGrid1.SaveGrid(this.saveFileDialog1.FileName,C1.Win.C1FlexGrid.FileFormatEnum.TextTab,C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells,System.Text.Encoding.UTF8);
        //        }
        //        catch
        //        {
        //            MessageBox.Show("转存Word文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }

        //        MessageBox.Show("转存Word文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c1Command4_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel文件(*.xls)|*.xls|All files (*.*)|*.*";
            DialogResult dr = this.saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    c1FlexGrid1.SaveExcel(this.saveFileDialog1.FileName, this.Text, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                }
                catch
                {
                    MessageBox.Show("转存Excel文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("转存Excel文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void c1Command6_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "Xml文件(*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult dr = this.saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    c1FlexGrid1.WriteXml(this.saveFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("转存XML文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("转存XML文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void c1Command7_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        //{
        //    this.saveFileDialog1.InitialDirectory = Application.StartupPath;
        //    this.saveFileDialog1.CheckPathExists = true;
        //    this.saveFileDialog1.DefaultExt = "dyr";
        //    this.saveFileDialog1.Filter = "达元数据文件(*.dyr)|*.dyr|All files (*.*)|*.*";
        //    DialogResult dr = this.saveFileDialog1.ShowDialog(this);
        //    if (dr == DialogResult.OK)
        //    {
        //        try
        //        {
        //            string text1 = Application.StartupPath;
        //            if (text1.Substring(text1.Length - 1, 1) != "\\")
        //            {
        //                text1 = text1 + "\\";
        //            }
        //            text1 = text1 + "Data\\Send.Mdb";
        //            System.IO.File.Copy(text1, this.saveFileDialog1.FileName, true);
        //            PublicOperation.SaveSendDB(this.queryString, this.saveFileDialog1.FileName);
        //        }
        //        catch
        //        {
        //            MessageBox.Show("转存达元数据文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }

        //        MessageBox.Show("转存达元数据文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}


        private void c1FlexGrid1_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            setRowStyle();
        }

        /// <summary>
        /// 设置行样式
        /// </summary>
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
                        c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
                        if (send)
                        {
                            c1FlexGrid1.Rows[i].Style = style2;
                        }
                        else
                        {
                            c1FlexGrid1.Rows[i].Style = style1;
                        }

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

                c1FlexGrid1_Click(null, null);
            }
        }

        private void c1FlexGrid1_DoubleClick(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            seeDetailInfo();
        }

        /// <summary>
        /// 显示全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdShowAll_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.refreshGrid(string.Empty);
        }

        /// <summary>
        /// 显示全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdShowAll_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdShowAll.Text);
        }

        /// <summary>
        /// 打印检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c1Command2_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (this.c1FlexGrid1.RowSel <= 0) return;

            DataTable dt = _clsResultOpr.GetDataTable_ReportGZ("A.SysCode='" + this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString() + "'", "");
            if (dt == null)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            PrintOperation.PreviewC1Report(c1Report1, dt, "WorkSheetGZ");
            Cursor = Cursors.Default;
        }

        private void c1FlexGrid1_Click(object sender, EventArgs e)
        {
            int selIdx = c1FlexGrid1.RowSel;
            if (selIdx < 1) return;

            bool send = Convert.ToBoolean(c1FlexGrid1.Rows[selIdx]["IsSended"]);
            if (c1FlexGrid1.Rows[selIdx]["Result"].ToString().Equals(ShareOption.ResultFailure))
            {
                c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
                if (send)
                {
                    c1FlexGrid1.Rows[selIdx].Style = style2;
                }
                else
                {
                    c1FlexGrid1.Rows[selIdx].Style = style1;
                }
            }
            else //合格
            {
                c1FlexGrid1.Styles.Highlight.ForeColor = Color.Black;
                if (send)//已经上传
                {
                    this.c1FlexGrid1.Rows[selIdx].Style = style3;
                }
                else
                {
                    this.c1FlexGrid1.Rows[selIdx].Style = styleNormal;
                }
            }
        }

        /// <summary>
        /// 组合键输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPesticideMeasure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J && e.Control)
            {
                FrmSuperAdminLogin superAdmin = new FrmSuperAdminLogin();
                superAdmin.ShowDialog(FrmMain.formMain);
                //MessageBox.Show("OK");
                //btnSuperadminLogin.PerformClick();
            }
        }

    }
}
