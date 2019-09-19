using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;


namespace FoodClient
{
	/// <summary>
	/// frmPesticideMeasure 的摘要说明。
	/// </summary>
    public class frmPesticideMeasure : System.Windows.Forms.Form
    {
        #region 控件私有变量
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
        private System.Windows.Forms.Label lblIsAllreadySended;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordSum;
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
        // private C1.Win.C1Command.C1Command cmdAdd01;
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
        #endregion

        private clsResult resultModel;
        private readonly clsResultOpr resultBll;
        private int queryType;

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        private bool IsSuperAdmin = false;

        /// <summary>
        /// 接收外部条件
        /// </summary>
        private string queryString;//组合查询条件
        /// <summary>
        /// 程序路径
        /// </summary>
        private string path = Environment.CurrentDirectory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="selectType">1代表标准速测法查询,2表示其他查询, 3表示综合查询</param>
        public frmPesticideMeasure(int selectType)
        {
            InitializeComponent();
            queryType = selectType;
            resultBll = new clsResultOpr();
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
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
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
            this.lblIsAllreadySended = new System.Windows.Forms.Label();
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
            this.cmdDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDelete_Click);
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
            this.cmdEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdEdit_Click);
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "打印所有记录";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
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
            this.c1CommandDock1.Size = new System.Drawing.Size(793, 24);
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
            this.c1ToolBar1.Size = new System.Drawing.Size(793, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
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
            this.cmdQuery.Text = "查询,当前窗口默认显示最近一个月数据";
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
            this.c1Command2.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command2_Click);
            this.c1Command2.Select += new System.EventHandler(this.c1Command2_Select);
            // 
            // c1CommandLink19
            // 
            this.c1CommandLink19.Command = this.cmdAdd02;
            this.c1CommandLink19.Text = "明细";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.c1CommandMenu1;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.cmdEdit;
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
            this.panel1.Size = new System.Drawing.Size(793, 347);
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
            this.c1FlexGrid1.Size = new System.Drawing.Size(793, 347);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 1;
            this.c1FlexGrid1.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid1_AfterSort);
            this.c1FlexGrid1.Click += new System.EventHandler(this.c1FlexGrid1_Click);
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUnsend);
            this.panel2.Controls.Add(this.lblSended);
            this.panel2.Controls.Add(this.lblUnPass);
            this.panel2.Controls.Add(this.lblRecordSum);
            this.panel2.Controls.Add(this.lblIsAllreadySended);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pnlSended);
            this.panel2.Controls.Add(this.pnlNoEligible);
            this.panel2.Controls.Add(this.lblPassed);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 371);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 32);
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
            // lblIsAllreadySended
            // 
            this.lblIsAllreadySended.Location = new System.Drawing.Point(582, 10);
            this.lblIsAllreadySended.Name = "lblIsAllreadySended";
            this.lblIsAllreadySended.Size = new System.Drawing.Size(45, 15);
            this.lblIsAllreadySended.TabIndex = 9;
            this.lblIsAllreadySended.Text = "已上传";
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
            this.pnlNoEligible.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            // frmPesticideMeasure
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(793, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPesticideMeasure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测数据综合查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPesticideMeasure_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPesticideMeasure_KeyDown);
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

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPesticideMeasure_Load(object sender, System.EventArgs e)
        {
            if (ShareOption.IsDataLink)//如果是单机版
            {
                lblSended.Visible = false;
                lblUnsend.Visible = false;
                pnlSended.Visible = false;
                lblIsAllreadySended.Visible = false;
                cmdSend.Visible = false;
            }

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

            int index = c1FlexGrid1.RowSel;
            if (index >= 0 && c1FlexGrid1.Rows[index]["Result"].ToString().Equals(ShareOption.ResultFailure))
            {
                c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
            }
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
            frmResultQuery query = new frmResultQuery(queryType);
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
            switch (queryType)
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
            DataTable dtbl = resultBll.GetAsDataTable(qWhere, "A.CheckStartDate DESC");
            c1FlexGrid1.DataSource = dtbl;
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
            passCount = resultBll.GetRecCount(queryFilter, out error);
            queryFilter = string.Format("({0}) AND IsSended=True", qWhere);
            sendedCount = resultBll.GetRecCount(queryFilter, out error);

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
            c1FlexGrid1.Cols["UpperCompany"].Caption = "所属市场代码";
            c1FlexGrid1.Cols["UpperCompanyName"].Caption = ShareOption.AreaTitle;
            c1FlexGrid1.Cols["FoodCode"].Caption = "被检" + ShareOption.SampleTitle + "编码";
            c1FlexGrid1.Cols["FoodName"].Caption = ShareOption.SampleTitle;
            c1FlexGrid1.Cols["CheckType"].Caption = "检测类型";
            c1FlexGrid1.Cols["SampleModel"].Caption = "规格型号";
            c1FlexGrid1.Cols["SampleLevel"].Caption = "质量等级";
            c1FlexGrid1.Cols["SampleState"].Caption = "批号或编号";
            c1FlexGrid1.Cols["Provider"].Caption = "供货商/商标";
            c1FlexGrid1.Cols["StdCode"].Caption = "条形码";
            c1FlexGrid1.Cols["OrCheckNo"].Caption = "原检测编号";
            c1FlexGrid1.Cols["ProduceCompany"].Caption = ShareOption.ProductionUnitNameTag + "编码";
            c1FlexGrid1.Cols["ProduceCompanyName"].Caption = ShareOption.ProductionUnitNameTag;//"生产单位";
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
            c1FlexGrid1.Cols["CheckStartDate"].Caption = "检测时间";
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
            //新增字段
            c1FlexGrid1.Cols["HolesNum"].Caption = "孔位/通道号";
            c1FlexGrid1.Cols["MachineSampleNum"].Caption = "仪器检测编号";

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



            if (ShareOption.IsDataLink)
            {
                c1FlexGrid1.Cols["IsSended"].Visible = false;
                c1FlexGrid1.Cols["SendedDate"].Visible = false;
                c1FlexGrid1.Cols["SenderName"].Visible = false;
                c1FlexGrid1.Cols["IsReSended"].Visible = false;
            }
            if (queryType == 2)
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

            GetModel(row);
            frmPesticideMeasureEdit frm = new frmPesticideMeasureEdit(queryType);
            frm.Tag = "EDIT";
            frm.setValue(resultModel);
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
            switch (queryType)
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
            //判断是否有删除的记录
            int row = c1FlexGrid1.RowSel;
            try
            {
                if (row <= 0)
                {
                    MessageBox.Show(this, "请选择将要删除的记录！");
                    return;
                }

                if (!IsSuperAdmin)//对于超级管理员无效
                {
                    if (Convert.ToBoolean(c1FlexGrid1.Rows[row]["IsSended"]))
                    {
                        MessageBox.Show("该记录已经上传！无法进行删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Convert.ToBoolean(c1FlexGrid1.Rows[row]["IsReSended"]))
                    {
                        MessageBox.Show("该记录设置为重新上传！无法进行删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (c1FlexGrid1.Rows[row]["ResultType"].Equals("检测仪自动"))
                    {
                        MessageBox.Show("该记录为检测仪自动检测记录！无法进行删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                string delStr = c1FlexGrid1[row, "SysCode"].ToString().Trim();

                //让用户确定删除操作
                if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                //删除记录
                string err = string.Empty;
                resultBll.DeleteByPrimaryKey(delStr, out err);
                if (!err.Equals(""))
                {
                    MessageBox.Show(this, "数据库操作出错！");
                }

                refreshGrid(queryString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "出现异常，请联系管理员！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            PrintOperation.PreviewGrid(c1FlexGrid1, "检测结果列表", null);
            refreshGrid(string.Empty);
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSend_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            frmResultSend send = new frmResultSend();
            DialogResult dr = send.ShowDialog(this);
            refreshGrid(queryString);
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="row"></param>
        private void GetModel(int row)
        {
            try
            {
                resultModel = new clsResult();
                resultModel.SysCode = c1FlexGrid1.Rows[row]["SysCode"].ToString();
                resultModel.ResultType = c1FlexGrid1.Rows[row]["ResultType"].ToString();
                resultModel.CheckNo = c1FlexGrid1.Rows[row]["CheckNo"].ToString();
                resultModel.StdCode = c1FlexGrid1.Rows[row]["StdCode"].ToString();
                resultModel.SampleCode = c1FlexGrid1.Rows[row]["SampleCode"].ToString();
                resultModel.CheckedCompany = c1FlexGrid1.Rows[row]["CheckedCompany"].ToString();
                resultModel.CheckedCompanyName = c1FlexGrid1.Rows[row]["CheckedCompanyName"].ToString();
                resultModel.CheckedComDis = c1FlexGrid1.Rows[row]["CheckedComDis"].ToString();
                resultModel.CheckPlaceCode = c1FlexGrid1.Rows[row]["CheckPlaceCode"].ToString();
                resultModel.FoodCode = c1FlexGrid1.Rows[row]["FoodCode"].ToString();
                if (c1FlexGrid1.Rows[row]["ProduceDate"] != null && c1FlexGrid1.Rows[row]["ProduceDate"] != DBNull.Value)
                {
                    resultModel.ProduceDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["ProduceDate"]);
                }
                resultModel.ProduceCompany = c1FlexGrid1.Rows[row]["ProduceCompany"].ToString();
                resultModel.ProducePlace = c1FlexGrid1.Rows[row]["ProducePlace"].ToString();
                resultModel.SentCompany = c1FlexGrid1.Rows[row]["SentCompany"].ToString();
                resultModel.Provider = c1FlexGrid1.Rows[row]["Provider"].ToString();
                resultModel.TakeDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["TakeDate"]);
                resultModel.CheckStartDate = Convert.ToDateTime(c1FlexGrid1.Rows[row]["CheckStartDate"]);
                resultModel.ImportNum = c1FlexGrid1.Rows[row]["ImportNum"].ToString();
                resultModel.SaveNum = c1FlexGrid1.Rows[row]["SaveNum"].ToString();
                resultModel.Unit = c1FlexGrid1.Rows[row]["Unit"].ToString();
                if (!c1FlexGrid1.Rows[row]["SampleNum"].ToString().Equals(""))
                {
                    resultModel.SampleNum = c1FlexGrid1.Rows[row]["SampleNum"].ToString();
                }
                else
                {
                    resultModel.SampleNum = string.Empty;
                }
                if (!c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString().Equals(""))
                {
                    resultModel.SampleBaseNum = c1FlexGrid1.Rows[row]["SampleBaseNum"].ToString();
                }
                else
                {
                    resultModel.SampleBaseNum = string.Empty;
                }
                resultModel.SampleUnit = c1FlexGrid1.Rows[row]["SampleUnit"].ToString();
                resultModel.SampleLevel = c1FlexGrid1.Rows[row]["SampleLevel"].ToString();
                resultModel.SampleModel = c1FlexGrid1.Rows[row]["SampleModel"].ToString();
                resultModel.SampleState = c1FlexGrid1.Rows[row]["SampleState"].ToString();
                resultModel.CheckMachine = c1FlexGrid1.Rows[row]["CheckMachine"].ToString();
                resultModel.CheckTotalItem = c1FlexGrid1.Rows[row]["CheckTotalItem"].ToString();
                resultModel.Standard = c1FlexGrid1.Rows[row]["Standard"].ToString();
                resultModel.CheckValueInfo = c1FlexGrid1.Rows[row]["CheckValueInfo"].ToString();
                resultModel.StandValue = c1FlexGrid1.Rows[row]["StandValue"].ToString();
                resultModel.Result = c1FlexGrid1.Rows[row]["Result"].ToString();
                resultModel.ResultInfo = c1FlexGrid1.Rows[row]["ResultInfo"].ToString();
                resultModel.UpperCompany = c1FlexGrid1.Rows[row]["UpperCompany"].ToString();
                resultModel.UpperCompanyName = c1FlexGrid1.Rows[row]["UpperCompanyName"].ToString();
                resultModel.OrCheckNo = c1FlexGrid1.Rows[row]["OrCheckNo"].ToString();
                resultModel.CheckType = c1FlexGrid1.Rows[row]["CheckType"].ToString();
                resultModel.CheckUnitCode = c1FlexGrid1.Rows[row]["CheckUnitCode"].ToString();
                resultModel.Checker = c1FlexGrid1.Rows[row]["Checker"].ToString();
                resultModel.Assessor = c1FlexGrid1.Rows[row]["Assessor"].ToString();
                resultModel.Organizer = c1FlexGrid1.Rows[row]["Organizer"].ToString();
                resultModel.Remark = c1FlexGrid1.Rows[row]["Remark"].ToString();
                resultModel.CheckPlanCode = c1FlexGrid1.Rows[row]["CheckPlanCode"].ToString();
                resultModel.SaleNum = c1FlexGrid1.Rows[row]["SaleNum"].ToString();
                resultModel.Price = c1FlexGrid1.Rows[row]["Price"].ToString();
                resultModel.CheckederVal = c1FlexGrid1.Rows[row]["CheckederVal"].ToString();
                resultModel.IsSentCheck = c1FlexGrid1.Rows[row]["IsSentCheck"].ToString();
                resultModel.CheckederRemark = c1FlexGrid1.Rows[row]["CheckederRemark"].ToString();
                resultModel.Notes = c1FlexGrid1.Rows[row]["Notes"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "出现异常，请联系管理员！\n" + ex.Message);
            }
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
            frmPesticideMeasureEdit frm = new frmPesticideMeasureEdit(queryType);
            frm.Tag = "MX";
            frm.setValue(resultModel);
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
                try
                {
                    bool send = false;
                    for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                    {
                        send = Convert.ToBoolean(c1FlexGrid1.Rows[i]["IsSended"]);
                        string str = c1FlexGrid1.Rows[i]["Result"].ToString();
                        if (str.Equals(ShareOption.ResultFailure))
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
                catch (Exception ex)
                {
                    MessageBox.Show(this, "出现异常，请联系管理员！\n" + ex.Message);
                }
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
            try
            {
                DataTable dt = resultBll.GetDataTable_ReportGZ("A.SysCode='" + this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString() + "'", "");
                if (dt == null)
                {
                    return;
                }
                //加载报表数据GetAsDataTable
                SaveFile(GetHtmlDoc(dt));
                frmReport report = new frmReport();
                report.HtmlUrl = path + "\\Others\\CheckedReportModel.html";
                report.ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            //Cursor = Cursors.WaitCursor;
            //PrintOperation.PreviewC1Report(c1Report1, dt, "WorkSheetGZ");
            //Cursor = Cursors.Default;
        }
        private void SaveFile(string str)
        {
            if (!(Directory.Exists(path + "\\Others")))
            {
                Directory.CreateDirectory(path + "\\Others");
            }
            string filePath = path + "\\Others\\CheckedReportModel.html";
            //如果存在则删除
            //if (File.Exists(filePath)) System.IO.File.Delete(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
        private string GetHtmlDoc(DataTable Reportdata)
        {
            string htmlDoc = string.Empty;
            try
            {
                //string d1=Reportdata.Rows[0][0].ToString();//检测机关
                //string d2 = Reportdata.Rows[0][1].ToString();//编号
                //string d3 = Reportdata.Rows[0][2].ToString();//样品名称
                //string d4 = Reportdata.Rows[0][3].ToString();//商标
                //string d5 = Reportdata.Rows[0][4].ToString();//型号
                //string d6 = Reportdata.Rows[0][5].ToString();//检测依据
                //string d7 = Reportdata.Rows[0][6].ToString();//生产时间2016/6/7 0:00:00
                //string d8 = Reportdata.Rows[0][7].ToString();//单价42
                //string d9 = Reportdata.Rows[0][8].ToString();//进货数量54
                //string d10 = Reportdata.Rows[0][9].ToString();//库存数量34
                //string d11 = Reportdata.Rows[0][10].ToString();//供应商  生产单位
                //string d12 = Reportdata.Rows[0][11].ToString();//联系人
                //string d13 = Reportdata.Rows[0][12].ToString();//联系方式
                //string d14 = Reportdata.Rows[0][13].ToString();//编号
                //string d15 = Reportdata.Rows[0][14].ToString();//联系人
                //string d16 = Reportdata.Rows[0][15].ToString();//联系电话
                //string d17 = Reportdata.Rows[0][16].ToString();//邮编
                //string d18 = Reportdata.Rows[0][17].ToString();//被检单位
                //string d19 = Reportdata.Rows[0][18].ToString();//地址
                //string d20 = Reportdata.Rows[0][19].ToString();//批号
                //string d21 = Reportdata.Rows[0][20].ToString();//商标
                //string d22 = Reportdata.Rows[0][21].ToString();//单价 
                //string d23 = Reportdata.Rows[0][22].ToString();//抽样基数 
                //string d24 = Reportdata.Rows[0][23].ToString();//进货数量
                //string d25 = Reportdata.Rows[0][24].ToString();//
                //string d26 = Reportdata.Rows[0][25].ToString();//库存数量 
                //string d27 = Reportdata.Rows[0][26].ToString();//处理情况 
                //string d28 = Reportdata.Rows[0][27].ToString();//抽样时间2017/10/18 0:00:00  
                //string d29 = Reportdata.Rows[0][28].ToString();//组织机构 
                //string d30 = Reportdata.Rows[0][29].ToString();//市场  
                //string d31 = Reportdata.Rows[0][30].ToString();//所在地 餐饮食品安全检测单位
                //string d32 = Reportdata.Rows[0][31].ToString();//检测单位  
                //string d33 = Reportdata.Rows[0][32].ToString();//负责人 
                //string d34 = Reportdata.Rows[0][33].ToString();//联系方式
                //string d35 = Reportdata.Rows[0][34].ToString();//检测项目 
                //string d36 = Reportdata.Rows[0][35].ToString();// 
                //string d37 = Reportdata.Rows[0][36].ToString();// 检测时间2016/6/2 16:38:24 
                //string d38 = Reportdata.Rows[0][37].ToString();//抽样数量  
                //string d39 = Reportdata.Rows[0][38].ToString();//抽样基数  
                //string d40 = Reportdata.Rows[0][39].ToString();//40%标准值 
                //string d41 = Reportdata.Rows[0][40].ToString();//检测值 
                //string d42 = Reportdata.Rows[0][41].ToString();//检测结论 
                //string d43 = Reportdata.Rows[0][42].ToString();//检测结果确认
                //string d44 = Reportdata.Rows[0][43].ToString();//"否" 是否送检
                //string d45 = Reportdata.Rows[0][44].ToString();//"已检"
                //string d46 = Reportdata.Rows[0][45].ToString();//已处理
                //int dd = Reportdata.Columns.Count;

                if (Reportdata != null && Reportdata.Rows.Count > 0)
                {
                    htmlDoc = System.IO.File.ReadAllText(path + "\\Others\\CheckedReportTitle.txt", System.Text.Encoding.Default);
                    htmlDoc += "<div class=\"content\"><div class=\"monitoring\"><div class=\"left_form\">监测机关信息</div><div class=\"organ\"><span class=\"wenan\">监测机关</span> </div>";
                    htmlDoc += string.Format("<div class=\"neirong\"><span class=\"wenan_box\" style=\" margin-left:20px;\">{0}</span></div>",  Reportdata.Rows[0][0].ToString());
                    htmlDoc += "<div class=\"organ\"><span class=\"wenan\">负责人</span></div>";
                    htmlDoc += string.Format("<div class=\"neirong\" style=\" width:225px; border-right:1px #1a1a1a solid;\"><span class=\"wenan_box\" style=\" margin-left:20px;\">{0}</span>", Reportdata.Rows[0][32].ToString());
                    htmlDoc += "</div><div class=\"organ\"><span class=\"wenan\">电话/传真</span></div>";
                    htmlDoc += string.Format("<div class=\"neirong\" style=\" width:229px;\"><span class=\"wenan_box\" style=\" margin-left:20px;\">{0}</span>", Reportdata.Rows[0][33].ToString());
                    htmlDoc += "</div></div><div class=\"Monitor\"><div class=\"Mon_form\">被监测人信息</div>";
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\"><span class=\"Mon_wenan\">注册号</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:15px;\">{0}</span></div>", Reportdata.Rows[0][13].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">法人</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:15px;\">{0}</span></div>", Reportdata.Rows[0][14].ToString());
                    htmlDoc +="<div class=\"Mon_organ\"><span class=\"Mon_wenan\">联系电话</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:130px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:15px;\">{0}</span></div>", Reportdata.Rows[0][15].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">邮编</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:15px;\">{0}</span></div></div>", Reportdata.Rows[0][16].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">名称</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:225px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div>", Reportdata.Rows[0][17].ToString());
                    htmlDoc += "<div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">住所</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:229px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div></div>", Reportdata.Rows[0][18].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">所在地</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:606px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div></div>", Reportdata.Rows[0][31].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">场所</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:606px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div></div>", Reportdata.Rows[0][30].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px; border-bottom:0; height:50px;\"><span class=\"Mon_wenan\">性质</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:606px;border:0; height:50px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\"></span>{0}</div></div></div>","");
                    htmlDoc += "<div class=\"Monitor\" style=\" height:200px;\"><div class=\"Mon_form\" style=\" height:151px;\">样品信息</div><div style=\" float:left;\"><div class=\"Mon_organ\"><span class=\"Mon_wenan\">名称</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:15px;\">{0}</span></div>", Reportdata.Rows[0][2].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">商标</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][3].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">规格</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][4].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">执行标准</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:140px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div></div>", Reportdata.Rows[0][5].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\"><span class=\"Mon_wenan\">生产日期</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][6].ToString().Split(' '));
                    htmlDoc +="<div class=\"Mon_organ\"><span class=\"Mon_wenan\">单价(元)</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][21].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">进货量</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][23].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">库存量</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:140px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div></div>", Reportdata.Rows[0][25].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:181px;\"><span class=\"Mon_wenan\">生产/进货单位名称</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:171px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div>", Reportdata.Rows[0][10].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">联系人</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px;\">{0}</span></div>", Reportdata.Rows[0][11].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">电话</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:140px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div></div>", Reportdata.Rows[0][12].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px; border-bottom:0; height:50px;\"><span class=\"Mon_wenan\">商品属性</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:606px;border:0; height:50px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div></div></div>", "");
                    htmlDoc += "<div class=\"Monitor\" style=\" height:150px;\"><div class=\"Mon_form\" style=\" height:149px; padding-top:2px;\">样品检测信息</div><div style=\" float:left;\"><div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">检测项目</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:202px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:20px;\">{0}</span></div>", Reportdata.Rows[0][34].ToString());
                    htmlDoc += "<div class=\"Mon_organ\" style=\" width:150px;\"><span class=\"Mon_wenan\">检测仪器</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:252px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:14px;\">{0}</span></div></div>","DY-5000食品安全综合分析仪");
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\"><span class=\"Mon_wenan\">抽样日期</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][27].ToString().Split(' '));
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">抽样数量</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][37].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">标准值</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:80px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div>", Reportdata.Rows[0][39].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"><span class=\"Mon_wenan\">实测值</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:140px;border-right:0;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px; font-size:14px;\">{0}</span></div></div>", Reportdata.Rows[0][40].ToString());
                    htmlDoc += "<div style=\" float:left;\"><div class=\"Mon_organ\"  style=\" border-bottom:0; height:50px;\"><span class=\"Mon_wenan\">检测结果</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px; border-bottom:0; height:50px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px;\">{0}</span></div>", Reportdata.Rows[0][41].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"  style=\" border-bottom:0; height:50px; width:262px;\"><span class=\"Mon_wenan\">被监测人结果确认</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:128px; border-bottom:0; height:50px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px;\">{0}</span></div>", Reportdata.Rows[0][42].ToString());
                    htmlDoc += "<div class=\"Mon_organ\"  style=\" border-bottom:0; height:50px;\"><span class=\"Mon_wenan\">是否送检</span></div>";
                    htmlDoc += string.Format("<div class=\"Mon_neirong\" style=\" width:90px; border:0; height:50px;\"><span class=\"Mon_wenan_box\" style=\" margin-left:10px;\">{0}</span></div></div></div>", Reportdata.Rows[0][43].ToString());
                    htmlDoc += "<div class=\"seal\"><div class=\"seal_wenzi\">检测单位（章）:</div><div style=\" float:right; margin-right:30px; font-size:16px; margin-top:174px;\">年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</div></div>";
                    htmlDoc += "<div class=\"seal\" style=\" width:399px; border-right:0;\"><div class=\"seal_wenzi\">检测人员:</div><div style=\" float:right; margin-right:30px; font-size:16px; margin-top:174px;\">年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</div></div></div><div style=\" width:100%; height:60px; float:left;\"></div></div></body></html>";
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return htmlDoc;
        }
        private void c1FlexGrid1_Click(object sender, EventArgs e)
        {
            try
            {
                int index = c1FlexGrid1.RowSel;
                int cellIndex = c1FlexGrid1.ColSel;
                if (index < 1)
                {
                    return;
                }
                if (c1FlexGrid1.Rows[index]["Result"].ToString().Equals(ShareOption.ResultFailure))
                {
                    c1FlexGrid1.Styles.Highlight.ForeColor = Color.Red;
                }
                else
                {
                    c1FlexGrid1.Styles.Highlight.ForeColor = Color.Black;
                }
                string ssss = c1FlexGrid1.Rows[index][cellIndex].ToString();
                object cell = c1FlexGrid1.Rows[index][cellIndex];
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "出现异常，请联系管理员！\n" + ex.Message);
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
