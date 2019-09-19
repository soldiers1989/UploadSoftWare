using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;

namespace FoodClient
{
	/// <summary>
	/// frmRecordQuery 的摘要说明。
	/// </summary>
    public class frmRecordQuery : System.Windows.Forms.Form
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
        public C1.Win.C1Command.C1Command cmdAdd01;
        private C1.Win.C1Command.C1CommandLink c1CommandLink13;
        public C1.Win.C1Command.C1Command cmdAdd02;
        private System.Windows.Forms.Panel pnlSended;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolTip toolTip1;
        private C1.Win.C1Command.C1Command c1Command3;
        private C1.Win.C1Command.C1Command c1Command4;
        private C1.Win.C1Command.C1Command c1Command6;
        private C1.Win.C1Command.C1Command c1Command7;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink11;
        private C1.Win.C1Command.C1CommandLink c1CommandLink14;
        private C1.Win.C1Command.C1CommandLink c1CommandLink15;
        private C1.Win.C1Command.C1CommandLink c1CommandLink16;
        private C1.Win.C1Report.C1Report c1Report1;
        private System.Windows.Forms.Label lblUnsend;
        private System.Windows.Forms.Label lblSended;
        private C1.Win.C1Command.C1Command cmdShowAll;
        private C1.Win.C1Command.C1CommandLink c1CommandLink17;
        private C1.Win.C1Command.C1CommandLink c1CommandLink18;
        private C1.Win.C1Command.C1Command c1Command2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        private clsSaleRecordOpr curSaleObjectOpr;
        private clsSaleRecord curSaleObject;
        private clsStockRecordOpr curStockObjectOpr;
        private clsStockRecord curStockObject;

        private C1.Win.C1FlexGrid.CellStyle style2;
        private C1.Win.C1FlexGrid.CellStyle styleNormal;

        public frmRecordQuery()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            curSaleObjectOpr = new clsSaleRecordOpr();
            curStockObjectOpr = new clsStockRecordOpr();
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

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

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRecordQuery));
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
            this.c1CommandLink11 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink14 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink15 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink16 = new C1.Win.C1Command.C1CommandLink();
            this.cmdShowAll = new C1.Win.C1Command.C1Command();
            this.c1Command2 = new C1.Win.C1Command.C1Command();
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
            this.lblRecordSum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSended = new System.Windows.Forms.Panel();
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
            // cmdAdd01
            // 
            this.cmdAdd01.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd01.Image")));
            this.cmdAdd01.Name = "cmdAdd01";
            this.cmdAdd01.Text = "新增";
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
            this.c1ToolBar1.AutoSize = false;
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
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
            this.cmdAdd02.Text = "新增";
            this.cmdAdd02.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd02_Click);
            this.cmdAdd02.Select += new System.EventHandler(this.cmdAdd02_Select);
            // 
            // c1Command3
            // 
            this.c1Command3.Image = ((System.Drawing.Image)(resources.GetObject("c1Command3.Image")));
            this.c1Command3.Name = "c1Command3";
            this.c1Command3.Text = "转存为Word";
            this.c1Command3.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command3_Click);
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
            this.c1Command7.Image = ((System.Drawing.Image)(resources.GetObject("c1Command7.Image")));
            this.c1Command7.Name = "c1Command7";
            this.c1Command7.Text = "转存为达元数据格式";
            this.c1Command7.Visible = false;
            this.c1Command7.Click += new C1.Win.C1Command.ClickEventHandler(this.c1Command7_Click);
            // 
            // c1CommandMenu2
            // 
            this.c1CommandMenu2.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
																							   this.c1CommandLink11,
																							   this.c1CommandLink14,
																							   this.c1CommandLink15,
																							   this.c1CommandLink16});
            this.c1CommandMenu2.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu2.Image")));
            this.c1CommandMenu2.Name = "c1CommandMenu2";
            this.c1CommandMenu2.Text = "转存";
            this.c1CommandMenu2.Select += new System.EventHandler(this.c1CommandMenu2_Select);
            // 
            // c1CommandLink11
            // 
            this.c1CommandLink11.Command = this.c1Command3;
            // 
            // c1CommandLink14
            // 
            this.c1CommandLink14.Command = this.c1Command4;
            // 
            // c1CommandLink15
            // 
            this.c1CommandLink15.Command = this.c1Command6;
            // 
            // c1CommandLink16
            // 
            this.c1CommandLink16.Command = this.c1Command7;
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
            this.panel1.Size = new System.Drawing.Size(632, 301);
            this.panel1.TabIndex = 10;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.BackColor = System.Drawing.Color.Gainsboro;
            this.c1FlexGrid1.ColumnInfo = @"13,0,0,40,200,0,Columns:0{Width:57;Name:""ID"";Caption:""样品编号"";AllowEditing:False;TextAlignFixed:CenterCenter;}	1{Width:70;Name:""FoodType2"";Caption:""食品种类"";AllowEditing:False;TextAlignFixed:CenterCenter;}	2{Width:55;Name:""Type"";Caption:""抑制率"";AllowEditing:False;TextAlign:RightCenter;TextAlignFixed:CenterCenter;}	3{Width:61;Name:""Result"";Caption:""检测结果"";AllowEditing:False;TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}	4{Width:59;Name:""Date"";Caption:""检测日期"";AllowEditing:False;TextAlignFixed:CenterCenter;}	5{Width:75;Name:""BJDWLX"";Caption:""被检单位类别"";AllowEditing:False;TextAlignFixed:CenterCenter;}	6{Width:72;Name:""BJDW"";Caption:""被检单位"";AllowEditing:False;TextAlignFixed:CenterCenter;}	7{Width:45;Name:""CD"";Caption:""产地"";AllowEditing:False;TextAlignFixed:CenterCenter;}	8{Width:55;Name:""SCDW"";Caption:""生产单位"";AllowEditing:False;TextAlignFixed:CenterCenter;}	9{Width:64;Caption:""检测单位"";AllowEditing:False;TextAlignFixed:CenterCenter;}	10{Caption:""检测人"";TextAlignFixed:CenterCenter;}	11{Width:57;Caption:""仪器类型"";AllowEditing:False;TextAlignFixed:CenterCenter;}	12{Width:56;Caption:""发送日期"";TextAlignFixed:CenterCenter;}	";
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(632, 301);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{BackColor:Gainsboro;Border:Flat,1,Black,Both;}	Alternate{BackColor:182, 221, 182;}	Fixed{BackColor:146, 206, 195;ForeColor:ControlText;TextAlign:CenterCenter;}	Highlight{BackColor:ActiveCaptionText;ForeColor:Black;}	Focus{BackColor:ActiveCaptionText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:238, 247, 238;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
            this.c1FlexGrid1.TabIndex = 1;
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            this.c1FlexGrid1.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid1_AfterSort);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUnsend);
            this.panel2.Controls.Add(this.lblSended);
            this.panel2.Controls.Add(this.lblRecordSum);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pnlSended);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 32);
            this.panel2.TabIndex = 11;
            // 
            // lblUnsend
            // 
            this.lblUnsend.Location = new System.Drawing.Point(209, 9);
            this.lblUnsend.Name = "lblUnsend";
            this.lblUnsend.Size = new System.Drawing.Size(82, 17);
            this.lblUnsend.TabIndex = 13;
            this.lblUnsend.Text = "待上传数";
            this.lblUnsend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSended
            // 
            this.lblSended.Location = new System.Drawing.Point(109, 9);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(82, 17);
            this.lblSended.TabIndex = 12;
            this.lblSended.Text = "已上传数";
            this.lblSended.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label3.Location = new System.Drawing.Point(303, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "已上传";
            // 
            // pnlSended
            // 
            this.pnlSended.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(128)));
            this.pnlSended.Location = new System.Drawing.Point(291, 10);
            this.pnlSended.Name = "pnlSended";
            this.pnlSended.Size = new System.Drawing.Size(13, 12);
            this.pnlSended.TabIndex = 7;
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = @"<!--Report *** c1Report1 ***--><Report version=""2.5.20052.176""><Name>c1Report1</Name><DataSource /><Layout /><Font><Name>Arial</Name><Size>9</Size></Font><Groups /><Sections><Section><Name>Detail</Name><Type>0</Type><Visible>0</Visible></Section><Section><Name>Header</Name><Type>1</Type><Visible>0</Visible></Section><Section><Name>Footer</Name><Type>2</Type><Visible>0</Visible></Section><Section><Name>PageHeader</Name><Type>3</Type><Visible>0</Visible></Section><Section><Name>PageFooter</Name><Type>4</Type><Visible>0</Visible></Section></Sections><Fields /></Report>";
            this.c1Report1.ReportName = "c1Report1";
            // 
            // frmRecordQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(632, 357);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecordQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "台帐数据查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRecordQuery_Load);
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
            frmRecordQueryOpt query = new frmRecordQueryOpt();
            if (this.Tag.ToString().Equals("Sale"))
            {
                query.Text = "销售台帐查询条件";
                query.label3.Visible = true;
                query.txtPurchaser.Visible = true;
                query.label17.Visible = false;
                query.label7.Visible = false;
                query.cmbProduceCompany.Visible = false;
                query.cmbProvider.Visible = false;
            }
            else
            {
                query.Text = "进货台帐查询条件";
                query.label3.Visible = false;
                query.txtPurchaser.Visible = false;
                query.label17.Visible = true;
                query.label7.Visible = true;
                query.cmbProduceCompany.Visible = true;
                query.cmbProvider.Visible = true;
            }
            query.Tag = this.Tag.ToString();
            DialogResult dr = query.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                this.Querystr = query.QueryStr;
                this.refreshGrid(this.Querystr);
            }
        }

        private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Close();
        }

        private void refreshGrid(string queryStr)
        {
            DataTable dt = null;
            string sError = string.Empty;
            string queryStr3 = "(" + queryStr + ") and IsSended=True";
            int iSendedCount = 0;
            if (this.Tag.Equals("Stock"))
            {
                dt = curStockObjectOpr.GetAsDataTable(queryStr, "SysCode", 0);
                this.c1FlexGrid1.SetDataBinding(dt.DataSet, "StockRecord");
                iSendedCount = curStockObjectOpr.GetRecCount(queryStr3, out sError);
            }
            else
            {
                dt = curSaleObjectOpr.GetAsDataTable(queryStr, "SysCode", 0);
                this.c1FlexGrid1.SetDataBinding(dt.DataSet, "SaleRecord");
                iSendedCount = curSaleObjectOpr.GetRecCount(queryStr3, out sError);
            }
            this.lblRecordSum.Text = "记录总数:" + Convert.ToString(this.c1FlexGrid1.Rows.Count - 1);
            this.lblSended.Text = "已上传数:" + Convert.ToString(iSendedCount);
            this.lblUnsend.Text = "待上传数:" + Convert.ToString(this.c1FlexGrid1.Rows.Count - 1 - iSendedCount);
            if (this.c1FlexGrid1.Rows.Count > 1)
            {
                for (int i = 1; i < this.c1FlexGrid1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.c1FlexGrid1.Rows[i]["IsSended"]))
                    {
                        this.c1FlexGrid1.Rows[i].Style = style2;
                    }
                    else
                    {
                        this.c1FlexGrid1.Rows[i].Style = styleNormal;
                    }
                }
            }

            this.setGridStyle();
            this.c1FlexGrid1.AutoSizeCols();
        }

        private void setGridStyle()
        {
            if (this.Tag.Equals("Stock"))
            {
                this.c1FlexGrid1.Cols.Count = 35;

                this.c1FlexGrid1.Cols["SysCode"].Caption = "系统编号";
                this.c1FlexGrid1.Cols["StdCode"].Caption = "记录编号";
                this.c1FlexGrid1.Cols["CompanyID"].Caption = "商户编号";
                this.c1FlexGrid1.Cols["CompanyName"].Caption = "商户名称";
                this.c1FlexGrid1.Cols["DisplayName"].Caption = "挡/铺号";
                this.c1FlexGrid1.Cols["InputDate"].Caption = "进货日期";
                this.c1FlexGrid1.Cols["FoodID"].Caption = "商品编号";
                this.c1FlexGrid1.Cols["FoodName"].Caption = "商品名称";
                this.c1FlexGrid1.Cols["Model"].Caption = "规格/型号/品种";
                this.c1FlexGrid1.Cols["InputNumber"].Caption = "进货数量";
                this.c1FlexGrid1.Cols["OutputNumber"].Caption = "销售数量";
                this.c1FlexGrid1.Cols["Unit"].Caption = "数量单位";
                this.c1FlexGrid1.Cols["ProduceDate"].Caption = "生产日期";
                this.c1FlexGrid1.Cols["ExpirationDate"].Caption = "保质期";
                this.c1FlexGrid1.Cols["ProduceCompanyID"].Caption = "生产/加工单位/个人编号";
                this.c1FlexGrid1.Cols["ProduceCompanyName"].Caption = "生产/加工单位/个人";
                this.c1FlexGrid1.Cols["PrivoderID"].Caption = "供货单位/个人编号";
                this.c1FlexGrid1.Cols["PrivoderName"].Caption = "供货单位/个人";
                this.c1FlexGrid1.Cols["LinkInfo"].Caption = "联系电话";
                this.c1FlexGrid1.Cols["LinkMan"].Caption = "联系人";
                this.c1FlexGrid1.Cols["CertificateType1"].Caption = "营业执照";
                this.c1FlexGrid1.Cols["CertificateType2"].Caption = "卫生许可证";
                this.c1FlexGrid1.Cols["CertificateType3"].Caption = "检验检疫证明";
                this.c1FlexGrid1.Cols["CertificateType4"].Caption = "产品合格证";
                this.c1FlexGrid1.Cols["CertificateType5"].Caption = "税票或进货单";
                this.c1FlexGrid1.Cols["CertificateType6"].Caption = "产地来源证明";
                this.c1FlexGrid1.Cols["CertificateType7"].Caption = "农残检测合格证";
                this.c1FlexGrid1.Cols["CertificateType8"].Caption = "QS标志认证证书";
                this.c1FlexGrid1.Cols["CertificateType9"].Caption = "其他证票";
                this.c1FlexGrid1.Cols["CertificateInfo"].Caption = "索取证票信息";
                this.c1FlexGrid1.Cols["MakeMan"].Caption = "经办人";
                this.c1FlexGrid1.Cols["Remark"].Caption = "备注";
                this.c1FlexGrid1.Cols["DistrictCode"].Caption = "所属地区代码";
                this.c1FlexGrid1.Cols["IsSended"].Caption = "是否上传";
                this.c1FlexGrid1.Cols["SentDate"].Caption = "上传日期";

                this.c1FlexGrid1.Cols["SysCode"].Visible = false;
                this.c1FlexGrid1.Cols["CompanyID"].Visible = false;
                this.c1FlexGrid1.Cols["FoodID"].Visible = false;
                this.c1FlexGrid1.Cols["ProduceCompanyID"].Visible = false;
                this.c1FlexGrid1.Cols["PrivoderID"].Visible = false;
                this.c1FlexGrid1.Cols["LinkMan"].Visible = false;
                this.c1FlexGrid1.Cols["CertificateInfo"].Visible = false;
                this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;

            }
            else
            {
                this.c1FlexGrid1.Cols.Count = 21;

                this.c1FlexGrid1.Cols["SysCode"].Caption = "系统编号";
                this.c1FlexGrid1.Cols["StdCode"].Caption = "记录编号";
                this.c1FlexGrid1.Cols["CompanyID"].Caption = "商户编号";
                this.c1FlexGrid1.Cols["CompanyName"].Caption = "商户名称";
                this.c1FlexGrid1.Cols["DisplayName"].Caption = "挡/铺号";
                this.c1FlexGrid1.Cols["SaleDate"].Caption = "销货日期";
                this.c1FlexGrid1.Cols["FoodID"].Caption = "商品编号";
                this.c1FlexGrid1.Cols["FoodName"].Caption = "商品名称";
                this.c1FlexGrid1.Cols["Model"].Caption = "规格/型号/品种";
                this.c1FlexGrid1.Cols["SaleNumber"].Caption = "销货数量";
                this.c1FlexGrid1.Cols["Price"].Caption = "销售单价";
                this.c1FlexGrid1.Cols["Unit"].Caption = "数量单位";
                this.c1FlexGrid1.Cols["PurchaserID"].Caption = "购货单位/个人编号";
                this.c1FlexGrid1.Cols["PurchaserName"].Caption = "购货单位/个人";
                this.c1FlexGrid1.Cols["LinkInfo"].Caption = "联系电话";
                this.c1FlexGrid1.Cols["LinkMan"].Caption = "联系人";
                this.c1FlexGrid1.Cols["MakeMan"].Caption = "经办人";
                this.c1FlexGrid1.Cols["Remark"].Caption = "备注";
                this.c1FlexGrid1.Cols["DistrictCode"].Caption = "所属地区代码";
                this.c1FlexGrid1.Cols["IsSended"].Caption = "是否上传";
                this.c1FlexGrid1.Cols["SentDate"].Caption = "上传日期";

                this.c1FlexGrid1.Cols["SysCode"].Visible = false;
                this.c1FlexGrid1.Cols["CompanyID"].Visible = false;
                this.c1FlexGrid1.Cols["FoodID"].Visible = false;
                this.c1FlexGrid1.Cols["PurchaserID"].Visible = false;
                this.c1FlexGrid1.Cols["LinkMan"].Visible = false;
                this.c1FlexGrid1.Cols["DistrictCode"].Visible = false;
            }
            if (ShareOption.IsDataLink)
            {
                this.c1FlexGrid1.Cols["IsSended"].Visible = false;
                this.c1FlexGrid1.Cols["SentDate"].Visible = false;
            }
        }

        private void cmdAdd01_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {

        }

        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                MessageBox.Show(this, "请先选择好要修改的记录！");
                return;
            }
            if (this.c1FlexGrid1.Rows[row]["IsSended"].ToString().Equals("True"))
            {
                MessageBox.Show(this, "该记录以上传，不能修改了！");
                return;
            }
            if (this.Tag.Equals("Stock"))
            {
                curStockObject = new clsStockRecord();
                curStockObject.SysCode = this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
                curStockObject.StdCode = this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
                curStockObject.CompanyID = this.c1FlexGrid1.Rows[row]["CompanyID"].ToString();
                curStockObject.CompanyName = this.c1FlexGrid1.Rows[row]["CompanyName"].ToString();
                curStockObject.DisplayName = this.c1FlexGrid1.Rows[row]["DisplayName"].ToString();
                curStockObject.InputDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["InputDate"].ToString());
                curStockObject.FoodID = this.c1FlexGrid1.Rows[row]["FoodID"].ToString();
                curStockObject.FoodName = this.c1FlexGrid1.Rows[row]["FoodName"].ToString();
                curStockObject.Model = this.c1FlexGrid1.Rows[row]["Model"].ToString();
                curStockObject.InputNumber = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["InputNumber"].ToString());
                curStockObject.OutputNumber = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["OutputNumber"].ToString());
                curStockObject.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
                curStockObject.ProduceDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["ProduceDate"].ToString());
                curStockObject.ExpirationDate = this.c1FlexGrid1.Rows[row]["ExpirationDate"].ToString();
                curStockObject.ProduceCompanyID = this.c1FlexGrid1.Rows[row]["ProduceCompanyID"].ToString();
                curStockObject.ProduceCompanyName = this.c1FlexGrid1.Rows[row]["ProduceCompanyName"].ToString();
                curStockObject.PrivoderID = this.c1FlexGrid1.Rows[row]["PrivoderID"].ToString();
                curStockObject.PrivoderName = this.c1FlexGrid1.Rows[row]["PrivoderName"].ToString();
                curStockObject.LinkInfo = this.c1FlexGrid1.Rows[row]["LinkInfo"].ToString();
                curStockObject.LinkMan = this.c1FlexGrid1.Rows[row]["LinkMan"].ToString();
                curStockObject.CertificateType1 = this.c1FlexGrid1.Rows[row]["CertificateType1"].ToString();
                curStockObject.CertificateType2 = this.c1FlexGrid1.Rows[row]["CertificateType2"].ToString();
                curStockObject.CertificateType3 = this.c1FlexGrid1.Rows[row]["CertificateType3"].ToString();
                curStockObject.CertificateType4 = this.c1FlexGrid1.Rows[row]["CertificateType4"].ToString();
                curStockObject.CertificateType5 = this.c1FlexGrid1.Rows[row]["CertificateType5"].ToString();
                curStockObject.CertificateType6 = this.c1FlexGrid1.Rows[row]["CertificateType6"].ToString();
                curStockObject.CertificateType7 = this.c1FlexGrid1.Rows[row]["CertificateType7"].ToString();
                curStockObject.CertificateType8 = this.c1FlexGrid1.Rows[row]["CertificateType8"].ToString();
                curStockObject.CertificateType9 = this.c1FlexGrid1.Rows[row]["CertificateType9"].ToString();
                curStockObject.CertificateInfo = this.c1FlexGrid1.Rows[row]["CertificateInfo"].ToString();
                curStockObject.MakeMan = this.c1FlexGrid1.Rows[row]["MakeMan"].ToString();
                curStockObject.Remark = this.c1FlexGrid1.Rows[row]["Remark"].ToString();

                frmStockRecordEdit frm = new frmStockRecordEdit();
                frm.Tag = "EDIT";
                frm.setValue(curStockObject);
                DialogResult dr = frm.ShowDialog(this);

                //刷新窗体中的Grid
                if (dr == DialogResult.OK)
                {
                    this.refreshGrid("");
                }
            }
            else
            {
                curSaleObject = new clsSaleRecord();
                curSaleObject.SysCode = this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
                curSaleObject.StdCode = this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
                curSaleObject.CompanyID = this.c1FlexGrid1.Rows[row]["CompanyID"].ToString();
                curSaleObject.CompanyName = this.c1FlexGrid1.Rows[row]["CompanyName"].ToString();
                curSaleObject.DisplayName = this.c1FlexGrid1.Rows[row]["DisplayName"].ToString();
                curSaleObject.SaleDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["SaleDate"].ToString());
                curSaleObject.FoodID = this.c1FlexGrid1.Rows[row]["FoodID"].ToString();
                curSaleObject.FoodName = this.c1FlexGrid1.Rows[row]["FoodName"].ToString();
                curSaleObject.Model = this.c1FlexGrid1.Rows[row]["Model"].ToString();
                curSaleObject.SaleNumber = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["SaleNumber"].ToString());
                curSaleObject.Price = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["Price"].ToString());
                curSaleObject.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
                curSaleObject.PurchaserID = this.c1FlexGrid1.Rows[row]["PurchaserID"].ToString();
                curSaleObject.PurchaserName = this.c1FlexGrid1.Rows[row]["PurchaserName"].ToString();
                curSaleObject.LinkInfo = this.c1FlexGrid1.Rows[row]["LinkInfo"].ToString();
                curSaleObject.LinkMan = this.c1FlexGrid1.Rows[row]["LinkMan"].ToString();
                curSaleObject.MakeMan = this.c1FlexGrid1.Rows[row]["MakeMan"].ToString();
                curSaleObject.Remark = this.c1FlexGrid1.Rows[row]["Remark"].ToString();

                frmSaleRecordEdit frm = new frmSaleRecordEdit();
                frm.Tag = "EDIT";
                frm.setValue(curSaleObject);
                DialogResult dr = frm.ShowDialog(this);

                //刷新窗体中的Grid
                if (dr == DialogResult.OK)
                {
                    this.refreshGrid("");
                }
            }
        }

        private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                MessageBox.Show(this, "请先选择好要删除的记录！");
                return;
            }
            if (this.c1FlexGrid1.Rows[row]["IsSended"].ToString().Equals("True"))
            {
                MessageBox.Show(this, "该记录以上传，不能删除了！");
                return;
            }
            if (this.Tag.Equals("Stock"))
            {
                curStockObjectOpr = new clsStockRecordOpr();
                string delStr = this.c1FlexGrid1[row, "SysCode"].ToString().Trim();
                if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                //删除记录
                string sErr = "";
                curStockObjectOpr.DeleteByPrimaryKey(delStr, out sErr);
                if (!sErr.Equals(""))
                {
                    MessageBox.Show(this, "数据库操作出错！");
                }
            }
            else
            {
                curSaleObjectOpr = new clsSaleRecordOpr();
                string delStr = this.c1FlexGrid1[row, "SysCode"].ToString().Trim();
                if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                //删除记录
                string sErr = "";
                curSaleObjectOpr.DeleteByPrimaryKey(delStr, out sErr);
                if (!sErr.Equals(""))
                {
                    MessageBox.Show(this, "数据库操作出错！");
                }
            }
            this.refreshGrid("");
        }

        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (this.Tag.Equals("Sale"))
            {
                PrintOperation.PreviewGrid(this.c1FlexGrid1, "销售台帐记录查询结果表", null);
            }
            else
            {
                PrintOperation.PreviewGrid(this.c1FlexGrid1, "进货台帐记录查询结果表", null);
            }
            refreshGrid("");
        }

        private void frmRecordQuery_Load(object sender, System.EventArgs e)
        {
            if (ShareOption.IsDataLink)
            {
                this.lblSended.Visible = false;
                this.lblUnsend.Visible = false;
                this.pnlSended.Visible = false;
                this.label3.Visible = false;
                style2 = this.c1FlexGrid1.Styles.Add("style2");
                style2.BackColor = this.panel2.BackColor;
                styleNormal = this.c1FlexGrid1.Styles.Add("styleNormal");
                styleNormal.BackColor = this.panel2.BackColor;
                this.cmdSend.Visible = false;
            }
            else
            {
                style2 = this.c1FlexGrid1.Styles.Add("style2");
                style2.BackColor = this.pnlSended.BackColor;
                styleNormal = this.c1FlexGrid1.Styles.Add("styleNormal");
                styleNormal.BackColor = this.panel2.BackColor;
            }

            if (this.Tag.Equals("Sale"))
            {
                this.Text = "销售台帐记录查询";
            }
            else
            {
                this.Text = "进货台帐记录查询";
            }
            this.Querystr = "";
            this.refreshGrid(this.Querystr);
        }

        private void cmdSend_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            frmRecordSend send = new frmRecordSend();
            DialogResult dr = send.ShowDialog(this);
        }

        private void cmdAdd02_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {

        }

        private void c1CommandMenu2_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1CommandMenu2.Text);
        }


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

        private void cmdAdd01_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdAdd01.Text);
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

        private void c1Command3_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.DefaultExt = "doc";
            this.saveFileDialog1.Filter = "Word文件(*.doc)|*.doc|All files (*.*)|*.*";
            DialogResult dr = this.saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    c1FlexGrid1.SaveGrid(this.saveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells, System.Text.Encoding.UTF8);
                }
                catch
                {
                    MessageBox.Show("转存Word文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("转存Word文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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

        private void c1Command7_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //			this.saveFileDialog1.InitialDirectory=Application.StartupPath;
            //			this.saveFileDialog1.CheckPathExists=true;
            //			this.saveFileDialog1.DefaultExt="dyr";
            //			this.saveFileDialog1.Filter="达元数据文件(*.dyr)|*.dyr|All files (*.*)|*.*";
            //			DialogResult dr=this.saveFileDialog1.ShowDialog(this); 
            //			if(dr==DialogResult.OK)
            //			{
            //				try
            //				{
            //					string text1=Application.StartupPath;
            //					if (text1.Substring(text1.Length-1,1)!="\\")
            //					{
            //						text1=text1+"\\";
            //					}
            //					text1=text1+"Data\\Send.Mdb";
            //					System.IO.File.Copy(text1,this.saveFileDialog1.FileName,true);
            //					PublicOperation.SaveSendDB(this.Querystr,this.saveFileDialog1.FileName);
            //				}
            //				catch
            //				{
            //					MessageBox.Show("转存达元数据文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //					return;
            //				}
            //
            //				MessageBox.Show("转存达元数据文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //			}		
        }

        private string Querystr;

        private void c1FlexGrid1_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            if (this.c1FlexGrid1.Rows.Count > 1)
            {
                for (int i = 1; i < this.c1FlexGrid1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.c1FlexGrid1.Rows[i]["IsSended"]))
                    {
                        this.c1FlexGrid1.Rows[i].Style = style2;
                    }
                    else
                    {
                        this.c1FlexGrid1.Rows[i].Style = styleNormal;
                    }
                }
            }
        }

        private void c1FlexGrid1_DoubleClick(object sender, System.EventArgs e)
        {
            //			clsResultOpr resultOpr=new clsResultOpr();
            //			DataTable dt=resultOpr.GetDataTable_ReportEx("A.SysCode='" + this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString() + "'","");
            //			if(dt==null)
            //			{
            //				return;
            //			}
            //			
            //			Cursor = Cursors.WaitCursor;
            //			PrintOperation.PreviewC1Report(c1Report1,dt,"WorkSheetReport");
            //			Cursor = Cursors.Default;
        }

        private void cmdShowAll_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.refreshGrid("");
        }

        private void cmdShowAll_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdShowAll.Text);
        }

        private void c1Command2_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //			if(this.c1FlexGrid1.RowSel<=0) return;
            //			clsResultOpr resultOpr=new clsResultOpr();
            //			DataTable dt=resultOpr.GetDataTable_ReportEx("A.SysCode='" + this.c1FlexGrid1.Rows[this.c1FlexGrid1.RowSel]["SysCode"].ToString() + "'","");
            //			if(dt==null)
            //			{
            //				return;
            //			}
            //			
            //			Cursor = Cursors.WaitCursor;
            //			PrintOperation.PreviewC1Report(c1Report1,dt,"WorkSheetReport");
            //			Cursor = Cursors.Default;
        }

        private void c1Command2_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1Command2.Text);
        }
    }
}
