using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Web.Security;

using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmSysOption 的摘要说明。
	/// </summary>
    public class frmSysOption : System.Windows.Forms.Form
    {
        #region 控件变量
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cmbSysUnit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServerId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chbAllowHandInputCheckUint;//this.chbAutoSave.Text = "允许手工录入被检单位中的经营户";
        private System.Windows.Forms.CheckBox chbAutoCloseWinAsSave;//AutoCloseWinAsSave
        private System.Windows.Forms.CheckBox chbExitPrompt;
        private System.Windows.Forms.CheckBox chbAutoReadLastResult;
        private System.Windows.Forms.CheckBox chbAutoLogin;
        private System.Windows.Forms.Panel panel0101;
        private System.Windows.Forms.Panel panel0201;
        private System.Windows.Forms.Panel panel0202;
        private System.Windows.Forms.TextBox txtFormatMachineCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFormatCardCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQualitativeCodeFormat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chbAutoCreateCheckCode;
        private System.Windows.Forms.Panel panel0203;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtTimerEndPlayWav;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEdit;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button btnTestConn;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysOption));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panel0101 = new System.Windows.Forms.Panel();
            this.cmbSysUnit = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel0201 = new System.Windows.Forms.Panel();
            this.txtQualitativeCodeFormat = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFormatCardCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFormatMachineCode = new System.Windows.Forms.TextBox();
            this.chbAutoCreateCheckCode = new System.Windows.Forms.CheckBox();
            this.chbAllowHandInputCheckUint = new System.Windows.Forms.CheckBox();
            this.chbExitPrompt = new System.Windows.Forms.CheckBox();
            this.chbAutoReadLastResult = new System.Windows.Forms.CheckBox();
            this.chbAutoLogin = new System.Windows.Forms.CheckBox();
            this.chbAutoCloseWinAsSave = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel0203 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTimerEndPlayWav = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.panel0202 = new System.Windows.Forms.Panel();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.txtServerPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServerId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panel0101.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.panel0201.SuspendLayout();
            this.panel0203.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.panel0202.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panelRight);
            this.c1Sizer1.Controls.Add(this.treeView1);
            this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Sizer1.GridDefinition = "97.9220779220779:False:False;\t25.1798561151079:False:False;73.0935251798561:False" +
                ":False;";
            this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(695, 385);
            this.c1Sizer1.TabIndex = 1;
            this.c1Sizer1.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panel0101);
            this.panelRight.Controls.Add(this.panel0201);
            this.panelRight.Controls.Add(this.panel0203);
            this.panelRight.Controls.Add(this.panel0202);
            this.panelRight.Location = new System.Drawing.Point(183, 4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(508, 377);
            this.panelRight.TabIndex = 1;
            // 
            // panel0101
            // 
            this.panel0101.Controls.Add(this.cmbSysUnit);
            this.panel0101.Controls.Add(this.label8);
            this.panel0101.Controls.Add(this.label3);
            this.panel0101.Controls.Add(this.numericUpDown2);
            this.panel0101.Controls.Add(this.label2);
            this.panel0101.Controls.Add(this.numericUpDown1);
            this.panel0101.Controls.Add(this.label1);
            this.panel0101.Controls.Add(this.label4);
            this.panel0101.Controls.Add(this.groupBox2);
            this.panel0101.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel0101.Location = new System.Drawing.Point(0, 0);
            this.panel0101.Name = "panel0101";
            this.panel0101.Size = new System.Drawing.Size(508, 377);
            this.panel0101.TabIndex = 0;
            // 
            // cmbSysUnit
            // 
            this.cmbSysUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysUnit.Items.AddRange(new object[] {
            "公斤",
            "亩"});
            this.cmbSysUnit.Location = new System.Drawing.Point(319, 14);
            this.cmbSysUnit.Name = "cmbSysUnit";
            this.cmbSysUnit.Size = new System.Drawing.Size(88, 20);
            this.cmbSysUnit.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(255, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 21);
            this.label8.TabIndex = 9;
            this.label8.Text = "样品单位：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(230, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(188, 14);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown2.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(94, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "摄氏度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(53, 14);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "温度：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(140, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "湿度：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.c1FlexGrid1);
            this.groupBox2.Location = new System.Drawing.Point(1, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 280);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "仪器设置";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(382, 244);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(72, 24);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "修改";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid1.Location = new System.Drawing.Point(8, 16);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(475, 216);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 13;
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            // 
            // panel0201
            // 
            this.panel0201.Controls.Add(this.txtQualitativeCodeFormat);
            this.panel0201.Controls.Add(this.label12);
            this.panel0201.Controls.Add(this.txtFormatCardCode);
            this.panel0201.Controls.Add(this.label11);
            this.panel0201.Controls.Add(this.label10);
            this.panel0201.Controls.Add(this.txtFormatMachineCode);
            this.panel0201.Controls.Add(this.chbAutoCreateCheckCode);
            this.panel0201.Controls.Add(this.chbAllowHandInputCheckUint);
            this.panel0201.Controls.Add(this.chbExitPrompt);
            this.panel0201.Controls.Add(this.chbAutoReadLastResult);
            this.panel0201.Controls.Add(this.chbAutoLogin);
            this.panel0201.Controls.Add(this.chbAutoCloseWinAsSave);
            this.panel0201.Controls.Add(this.label9);
            this.panel0201.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel0201.Location = new System.Drawing.Point(0, 0);
            this.panel0201.Name = "panel0201";
            this.panel0201.Size = new System.Drawing.Size(508, 377);
            this.panel0201.TabIndex = 2;
            // 
            // txtQualitativeCodeFormat
            // 
            this.txtQualitativeCodeFormat.Location = new System.Drawing.Point(128, 123);
            this.txtQualitativeCodeFormat.Name = "txtQualitativeCodeFormat";
            this.txtQualitativeCodeFormat.Size = new System.Drawing.Size(192, 21);
            this.txtQualitativeCodeFormat.TabIndex = 12;
            this.txtQualitativeCodeFormat.Visible = false;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(16, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "定性定量编码格式：";
            this.label12.Visible = false;
            // 
            // txtFormatCardCode
            // 
            this.txtFormatCardCode.Location = new System.Drawing.Point(128, 99);
            this.txtFormatCardCode.Name = "txtFormatCardCode";
            this.txtFormatCardCode.Size = new System.Drawing.Size(192, 21);
            this.txtFormatCardCode.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(16, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "手工录入编码格式：";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(328, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 66);
            this.label10.TabIndex = 9;
            this.label10.Text = "注意编码最多不能超过50个字符，一个汉字算两个字符";
            // 
            // txtFormatMachineCode
            // 
            this.txtFormatMachineCode.Location = new System.Drawing.Point(128, 75);
            this.txtFormatMachineCode.Name = "txtFormatMachineCode";
            this.txtFormatMachineCode.Size = new System.Drawing.Size(192, 21);
            this.txtFormatMachineCode.TabIndex = 7;
            // 
            // chbStdCodeSame
            // 
            this.chbAutoCreateCheckCode.Location = new System.Drawing.Point(16, 47);
            this.chbAutoCreateCheckCode.Name = "chbStdCodeSame";
            this.chbAutoCreateCheckCode.Size = new System.Drawing.Size(184, 20);
            this.chbAutoCreateCheckCode.TabIndex = 5;
            this.chbAutoCreateCheckCode.Text = "自动生成检测编号";
            // 
            // chbAutoSave
            // 
            this.chbAllowHandInputCheckUint.Location = new System.Drawing.Point(16, 16);
            this.chbAllowHandInputCheckUint.Name = "chbAutoSave";
            this.chbAllowHandInputCheckUint.Size = new System.Drawing.Size(216, 20);
            this.chbAllowHandInputCheckUint.TabIndex = 4;
            this.chbAllowHandInputCheckUint.Text = "允许手工录入被检单位中的经营户";
            // 
            // chbExitPrompt
            // 
            this.chbExitPrompt.Enabled = false;
            this.chbExitPrompt.Location = new System.Drawing.Point(16, 311);
            this.chbExitPrompt.Name = "chbExitPrompt";
            this.chbExitPrompt.Size = new System.Drawing.Size(184, 20);
            this.chbExitPrompt.TabIndex = 2;
            this.chbExitPrompt.Text = "退出时不再提示未上传的数据";
            this.chbExitPrompt.Visible = false;
            // 
            // chbAutoReadLastResult
            // 
            this.chbAutoReadLastResult.Enabled = false;
            this.chbAutoReadLastResult.Location = new System.Drawing.Point(16, 280);
            this.chbAutoReadLastResult.Name = "chbAutoReadLastResult";
            this.chbAutoReadLastResult.Size = new System.Drawing.Size(216, 20);
            this.chbAutoReadLastResult.TabIndex = 1;
            this.chbAutoReadLastResult.Text = "检测窗口自动读取上次读取的对照";
            this.chbAutoReadLastResult.Visible = false;
            // 
            // chbAutoLogin
            // 
            this.chbAutoLogin.Enabled = false;
            this.chbAutoLogin.Location = new System.Drawing.Point(16, 248);
            this.chbAutoLogin.Name = "chbAutoLogin";
            this.chbAutoLogin.Size = new System.Drawing.Size(128, 21);
            this.chbAutoLogin.TabIndex = 0;
            this.chbAutoLogin.Text = "跳过密码自动登录";
            this.chbAutoLogin.Visible = false;
            // 
            // chbAutoCloseWinAsSave
            // 
            this.chbAutoCloseWinAsSave.Enabled = false;
            this.chbAutoCloseWinAsSave.Location = new System.Drawing.Point(16, 342);
            this.chbAutoCloseWinAsSave.Name = "chbAutoCloseWinAsSave";
            this.chbAutoCloseWinAsSave.Size = new System.Drawing.Size(176, 20);
            this.chbAutoCloseWinAsSave.TabIndex = 3;
            this.chbAutoCloseWinAsSave.Text = "保存数据后自动关闭对话框";
            this.chbAutoCloseWinAsSave.Visible = false;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "自动检测编码格式：";
            // 
            // panel0203
            // 
            this.panel0203.Controls.Add(this.btnView);
            this.panel0203.Controls.Add(this.txtTimerEndPlayWav);
            this.panel0203.Controls.Add(this.label17);
            this.panel0203.Controls.Add(this.label15);
            this.panel0203.Controls.Add(this.numericUpDown5);
            this.panel0203.Controls.Add(this.label16);
            this.panel0203.Controls.Add(this.label19);
            this.panel0203.Controls.Add(this.numericUpDown6);
            this.panel0203.Controls.Add(this.label20);
            this.panel0203.Controls.Add(this.label22);
            this.panel0203.Controls.Add(this.numericUpDown7);
            this.panel0203.Controls.Add(this.label23);
            this.panel0203.Controls.Add(this.label24);
            this.panel0203.Controls.Add(this.numericUpDown8);
            this.panel0203.Controls.Add(this.label25);
            this.panel0203.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel0203.Location = new System.Drawing.Point(0, 0);
            this.panel0203.Name = "panel0203";
            this.panel0203.Size = new System.Drawing.Size(508, 377);
            this.panel0203.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(360, 166);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(56, 24);
            this.btnView.TabIndex = 14;
            this.btnView.Text = "浏览...";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTimerEndPlayWav
            // 
            this.txtTimerEndPlayWav.Enabled = false;
            this.txtTimerEndPlayWav.Location = new System.Drawing.Point(176, 168);
            this.txtTimerEndPlayWav.Name = "txtTimerEndPlayWav";
            this.txtTimerEndPlayWav.Size = new System.Drawing.Size(176, 21);
            this.txtTimerEndPlayWav.TabIndex = 13;
            this.txtTimerEndPlayWav.Text = "awoke.wav";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(8, 168);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(152, 21);
            this.label17.TabIndex = 12;
            this.label17.Text = "计时工具声音设置";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(217, 127);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 21);
            this.label15.TabIndex = 11;
            this.label15.Text = "分钟";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(176, 127);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown5.TabIndex = 10;
            this.numericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown5.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 21);
            this.label16.TabIndex = 9;
            this.label16.Text = "计时工具按钮4定时时长";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(217, 90);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 21);
            this.label19.TabIndex = 8;
            this.label19.Text = "分钟";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(176, 90);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown6.TabIndex = 7;
            this.numericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown6.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(8, 90);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(152, 21);
            this.label20.TabIndex = 6;
            this.label20.Text = "计时工具按钮3定时时长";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(217, 53);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 21);
            this.label22.TabIndex = 5;
            this.label22.Text = "分钟";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(176, 53);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown7.TabIndex = 4;
            this.numericUpDown7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown7.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(8, 53);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(152, 21);
            this.label23.TabIndex = 3;
            this.label23.Text = "计时工具按钮2定时时长";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(217, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(52, 21);
            this.label24.TabIndex = 2;
            this.label24.Text = "分钟";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Location = new System.Drawing.Point(176, 16);
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(39, 21);
            this.numericUpDown8.TabIndex = 1;
            this.numericUpDown8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown8.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(8, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(152, 21);
            this.label25.TabIndex = 0;
            this.label25.Text = "计时工具按钮1定时时长";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel0202
            // 
            this.panel0202.Controls.Add(this.btnTestConn);
            this.panel0202.Controls.Add(this.txtServerPwd);
            this.panel0202.Controls.Add(this.label5);
            this.panel0202.Controls.Add(this.txtServerId);
            this.panel0202.Controls.Add(this.label6);
            this.panel0202.Controls.Add(this.txtServerIp);
            this.panel0202.Controls.Add(this.label7);
            this.panel0202.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel0202.Location = new System.Drawing.Point(0, 0);
            this.panel0202.Name = "panel0202";
            this.panel0202.Size = new System.Drawing.Size(508, 377);
            this.panel0202.TabIndex = 1;
            // 
            // btnTestConn
            // 
            this.btnTestConn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTestConn.Location = new System.Drawing.Point(248, 136);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(112, 24);
            this.btnTestConn.TabIndex = 6;
            this.btnTestConn.Text = "测试连接";
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // txtServerPwd
            // 
            this.txtServerPwd.Location = new System.Drawing.Point(96, 88);
            this.txtServerPwd.MaxLength = 50;
            this.txtServerPwd.Name = "txtServerPwd";
            this.txtServerPwd.PasswordChar = '*';
            this.txtServerPwd.Size = new System.Drawing.Size(300, 21);
            this.txtServerPwd.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = " 注册密码：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerId
            // 
            this.txtServerId.Location = new System.Drawing.Point(96, 52);
            this.txtServerId.MaxLength = 50;
            this.txtServerId.Name = "txtServerId";
            this.txtServerId.Size = new System.Drawing.Size(300, 21);
            this.txtServerId.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = " 注册ID：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(96, 16);
            this.txtServerIp.MaxLength = 100;
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(300, 21);
            this.txtServerIp.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "服务器地址：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(175, 377);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.groupBox1);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnApply);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 345);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(695, 40);
            this.panelBottom.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 10);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(428, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(515, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(604, 12);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(72, 24);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmSysOption
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(695, 385);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.c1Sizer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSysOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSysOption_Closing);
            this.Load += new System.EventHandler(this.FrmSysOption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panel0101.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.panel0201.ResumeLayout(false);
            this.panel0201.PerformLayout();
            this.panel0203.ResumeLayout(false);
            this.panel0203.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.panel0202.ResumeLayout(false);
            this.panel0202.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private readonly clsSysOptOpr sysBll;
        private readonly clsMachineOpr machineBll;
        private clsMachine machineModel;
        private static int lev = 2;
        private TreeNode[] prevNodes;
        private bool bTestConn = false;



        /// <summary>
        ///构造函数
        /// </summary>
        public frmSysOption()
        {
            InitializeComponent();

            sysBll = new clsSysOptOpr();
            machineBll = new clsMachineOpr();
            prevNodes = new TreeNode[ShareOption.MaxLevel];
            for (int i = 0; i < ShareOption.MaxLevel; i++)
            {
                prevNodes[i] = new TreeNode();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void FrmSysOption_Load(object sender, System.EventArgs e)
        {
            treeView1.Nodes.Clear();

            DataTable dtbl = sysBll.GetAsDataTable("optType NOT LIKE '%05'", "SysCode");
            if (dtbl == null || dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("数据库数据配置有误!");
                return;
            }

            string tCode = string.Empty;
            TreeNode theNode = null;

            int iMod = 0;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                tCode = dtbl.Rows[i]["SysCode"].ToString();

                theNode = new TreeNode();
                theNode.Text = dtbl.Rows[i]["OptDes"].ToString();
                theNode.Tag = tCode;

                iMod = tCode.Length / lev;
                if (iMod == 1)
                {
                    treeView1.Nodes.Add(theNode);
                }
                else if (iMod >= 3)
                {
                    continue;
                }
                else
                {
                    if (iMod == 2 && tCode.Substring(2, 2).Equals("00"))
                    {
                        prevNodes[iMod - 1].Nodes.Add(theNode);
                    }
                    else
                    {
                        if (!(ShareOption.IsDataLink && tCode.Equals("0202")))
                        {
                            prevNodes[iMod - 2].Nodes.Add(theNode);
                        }
                    }
                }
                prevNodes[iMod - 1] = theNode;
            }

            treeView1.ExpandAll();
            drawPanel("0");
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
        /// <summary>
        /// 选择不同的节点触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }
            string first = e.Node.Tag.ToString();

            if (first.Length == 2)
            {
                drawPanel(first + "01");
            }
            else if (first.Length == 4)
            {
                drawPanel(first);
            }
            else if (first.Length >= 8)
            {
                MessageBox.Show("数据库数据有误!");
            }
        }
        /// <summary>
        /// 显示节点
        /// </summary>
        /// <param name="code"></param>
        private void drawPanel(string code)
        {
            if (code.Equals("0"))//第一个节点,表示根节点
            {
                DataTable dt2 = sysBll.GetAsDataTable("SysCode like '" + StringUtil.RepeatChar('_', 6) + "'", "SysCode");

                chbAutoLogin.Checked = Convert.ToBoolean(dt2.Rows[3]["OptValue"]);
                chbAutoReadLastResult.Checked = Convert.ToBoolean(dt2.Rows[4]["OptValue"]);
                chbExitPrompt.Checked = Convert.ToBoolean(dt2.Rows[5]["OptValue"]);
                chbAutoCloseWinAsSave.Checked = Convert.ToBoolean(dt2.Rows[6]["OptValue"]);
                chbAllowHandInputCheckUint.Checked = Convert.ToBoolean(dt2.Rows[7]["OptValue"]);
                chbAutoCreateCheckCode.Checked = Convert.ToBoolean(dt2.Rows[8]["OptValue"]);
                txtFormatMachineCode.Text = dt2.Rows[9]["OptValue"].ToString();
                txtFormatCardCode.Text = dt2.Rows[10]["OptValue"].ToString();
                txtQualitativeCodeFormat.Text = dt2.Rows[11]["OptValue"].ToString();
                txtServerIp.Text = dt2.Rows[12]["OptValue"].ToString();
                txtServerId.Text = dt2.Rows[13]["OptValue"].ToString();
                txtServerPwd.Text = dt2.Rows[14]["OptValue"].ToString();
                numericUpDown8.Value = Convert.ToDecimal(dt2.Rows[15]["OptValue"]);
                numericUpDown7.Value = Convert.ToDecimal(dt2.Rows[16]["OptValue"]);
                numericUpDown6.Value = Convert.ToDecimal(dt2.Rows[17]["OptValue"]);
                numericUpDown5.Value = Convert.ToDecimal(dt2.Rows[18]["OptValue"]);
                txtTimerEndPlayWav.Text = dt2.Rows[19]["OptValue"].ToString();

            }
            if (code.Length != 4)
            {
                return;
            }

            DataTable dt = sysBll.GetAsDataTable("SysCode like '" + code+ StringUtil.RepeatChar('_', 2) + "'", "SysCode");

            if (code.Equals("0101"))//第一个节点，仪器参数设置
            {

                DataTable dtblMachine = machineBll.GetAsDataTable("IsShow=True", "OrderId ASC", 0);
                numericUpDown1.Value = Convert.ToDecimal(dt.Rows[0]["OptValue"]);
                numericUpDown2.Value = Convert.ToDecimal(dt.Rows[1]["OptValue"]);
                cmbSysUnit.Text = dt.Rows[2]["OptValue"].ToString();
                c1FlexGrid1.SetDataBinding(dtblMachine.DataSet, "Machine");
                setGridStyle();
                c1FlexGrid1.AutoSizeCols();
                panel0101.BringToFront();
            }
            else if (code.Equals("0201"))//第二节点第一子节点 系统参数设置
            {
                chbAutoLogin.Checked = Convert.ToBoolean(dt.Rows[0]["OptValue"]);
                chbAutoReadLastResult.Checked = Convert.ToBoolean(dt.Rows[1]["OptValue"]);
                chbExitPrompt.Checked = Convert.ToBoolean(dt.Rows[2]["OptValue"]);
                chbAutoCloseWinAsSave.Checked = Convert.ToBoolean(dt.Rows[3]["OptValue"]);
                chbAllowHandInputCheckUint.Checked = Convert.ToBoolean(dt.Rows[4]["OptValue"]);
                chbAutoCreateCheckCode.Checked = Convert.ToBoolean(dt.Rows[5]["OptValue"]);
                txtFormatMachineCode.Text = dt.Rows[6]["OptValue"].ToString();
                txtFormatCardCode.Text = dt.Rows[7]["OptValue"].ToString();
                txtQualitativeCodeFormat.Text = dt.Rows[8]["OptValue"].ToString();
                panel0201.BringToFront();
            }
            else if (code.Equals("0202"))//第二子节点网络设置
            {
                txtServerIp.Text = dt.Rows[0]["OptValue"].ToString();
                txtServerId.Text = dt.Rows[1]["OptValue"].ToString();
                txtServerPwd.Text = dt.Rows[2]["OptValue"].ToString();
                panel0202.BringToFront();
            }
            else if (code.Equals("0203"))//计数器设置
            {
                numericUpDown8.Value = Convert.ToDecimal(dt.Rows[0]["OptValue"]);
                numericUpDown7.Value = Convert.ToDecimal(dt.Rows[1]["OptValue"]);
                numericUpDown6.Value = Convert.ToDecimal(dt.Rows[2]["OptValue"]);
                numericUpDown5.Value = Convert.ToDecimal(dt.Rows[3]["OptValue"]);
                txtTimerEndPlayWav.Text = dt.Rows[4]["OptValue"].ToString();
                panel0203.BringToFront();
            }
            else
            {
                MessageBox.Show("数据库数据有误!");
            }
        }

        /// <summary>
        /// 显示DataGridView
        /// </summary>
        private void setGridStyle()
        {
            c1FlexGrid1.Cols["SysCode"].Caption = "编号";
            c1FlexGrid1.Cols["MachineName"].Caption = "仪器名称";
            c1FlexGrid1.Cols["ShortCut"].Caption = "快捷编码";
            c1FlexGrid1.Cols["MachineModel"].Caption = "型号";
            c1FlexGrid1.Cols["Company"].Caption = "生产厂商";
            c1FlexGrid1.Cols["Protocol"].Caption = "所用插件";
            c1FlexGrid1.Cols["LinkComNo"].Caption = "使用端口";
            c1FlexGrid1.Cols["IsSupport"].Caption = "是否默认";
            c1FlexGrid1.Cols["TestValue"].Caption = "检测值";
            c1FlexGrid1.Cols["TestSign"].Caption = "检测符号";
            c1FlexGrid1.Cols["LinkStdCode"].Caption = "所用标准";

            c1FlexGrid1.Cols["IsShow"].Caption = "是否启用";//新增字段
            c1FlexGrid1.Cols["MachineModel"].Visible = false;
            c1FlexGrid1.Cols["IsShow"].Visible = false;
            c1FlexGrid1.Cols["Protocol"].Visible = false;
            c1FlexGrid1.Cols["TestValue"].Visible = false;
            c1FlexGrid1.Cols["IsSupport"].Visible = false;

            c1FlexGrid1.Cols["SysCode"].Visible = false;
            c1FlexGrid1.Cols["ShortCut"].Visible = false;
            c1FlexGrid1.Cols["TestSign"].Visible = false;
            c1FlexGrid1.Cols["LinkStdCode"].Visible = false;
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, System.EventArgs e)
        {
            string code = treeView1.SelectedNode.Tag.ToString();

            if (code.Length == 2)
            {
                code += "01";
            }

            string err = string.Empty;
            if (code.Equals("0101"))
            {
                sysBll.Update("OptValue='" + numericUpDown1.Value.ToString() + "'", "SysCode='010101'", out err);
                sysBll.Update("OptValue='" + numericUpDown2.Value.ToString() + "'", "SysCode='010102'", out err);
                sysBll.Update("OptValue='" + cmbSysUnit.Text.Trim() + "'", "SysCode='010103'", out err);
            }
            else if (code.Equals("0201"))
            {
                //sysBll.Update("OptValue='" + checkBox1.Checked.ToString() + "'", "SysCode='020101'", out sErr);
                //sysBll.Update("OptValue='" + checkBox2.Checked.ToString() + "'", "SysCode='020102'", out sErr);
                //sysBll.Update("OptValue='" + checkBox3.Checked.ToString() + "'", "SysCode='020103'", out sErr);
                //sysBll.Update("OptValue='" + checkBox4.Checked.ToString() + "'", "SysCode='020104'", out sErr);
                sysBll.Update("OptValue='" + chbAllowHandInputCheckUint.Checked.ToString() + "'", "SysCode='020105'", out err);
                sysBll.Update("OptValue='" + chbAutoCreateCheckCode.Checked.ToString() + "'", "SysCode='020106'", out err);
                sysBll.Update("OptValue='" + txtFormatMachineCode.Text.Trim() + "'", "SysCode='020107'", out err);
                sysBll.Update("OptValue='" + txtFormatCardCode.Text.Trim() + "'", "SysCode='020108'", out err);
                sysBll.Update("OptValue='" + txtQualitativeCodeFormat.Text.Trim() + "'", "SysCode='020109'", out err);

                ShareOption.SysAutoLogin = chbAutoLogin.Checked;
                ShareOption.SysExitPrompt = chbExitPrompt.Checked;
                if (chbAllowHandInputCheckUint.Checked != ShareOption.AllowHandInputCheckUint)
                {
                    ShareOption.AllowHandInputCheckUint = chbAllowHandInputCheckUint.Checked;
                    FrmMain.IsLoadCheckedComSel = false;
                }
                ShareOption.SysStdCodeSame = chbAutoCreateCheckCode.Checked;
                ShareOption.FormatStrMachineCode = txtFormatMachineCode.Text.Trim();
                ShareOption.FormatStandardCode = txtFormatCardCode.Text.Trim();
            }
            else if (code.Equals("0202"))//连接参数设置
            {
                //if (RegularCheck.HaveSpecChar(txtServerIp)
                //    || RegularCheck.HaveSpecChar(txtServerIp)
                //    || RegularCheck.HaveSpecChar(txtServerIp))
                //{
                //    MessageBox.Show(this, "输入中有非法字符,请检查!");
                //    return;
                //}

                sysBll.Update("OptValue='" + txtServerIp.Text.Trim() + "'", "SysCode='020201'", out err);//SysCode='020201'
                sysBll.Update("OptValue='" + txtServerId.Text.Trim() + "'", "SysCode='020202'", out err);//SysCode='020202'
                sysBll.Update("OptValue='" + txtServerPwd.Text.Trim() + "'", "SysCode='020203'", out err);//SysCode='020203'

                ShareOption.SysServerIP = txtServerIp.Text.Trim();
                ShareOption.SysServerID = txtServerId.Text.Trim();
                ShareOption.SysServerPass = txtServerPwd.Text.Trim();
                CommonOperation.writeWebServer(txtServerIp.Text.Trim());
                //保留
            }
            else if (code.Equals("0203"))
            {
                sysBll.Update("OptValue='" + numericUpDown8.Value.ToString() + "'", "SysCode='020301'", out err);
                sysBll.Update("OptValue='" + numericUpDown7.Value.ToString() + "'", "SysCode='020302'", out err);
                sysBll.Update("OptValue='" + numericUpDown6.Value.ToString() + "'", "SysCode='020303'", out err);
                sysBll.Update("OptValue='" + numericUpDown5.Value.ToString() + "'", "SysCode='020304'", out err);
                sysBll.Update("OptValue='" + txtTimerEndPlayWav.Text.Trim() + "'", "SysCode='020305'", out err);
                ShareOption.SysTimer1 = numericUpDown8.Value;
                ShareOption.SysTimer2 = numericUpDown7.Value;
                ShareOption.SysTimer3 = numericUpDown6.Value;
                ShareOption.SysTimer4 = numericUpDown5.Value;
                ShareOption.SysTimerEndPlayWav = txtTimerEndPlayWav.Text;
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string err = string.Empty;

            //if (RegularCheck.HaveSpecChar(txtServerIp))
            ////|| RegularCheck.HaveSpecChar(textBox2)
            ////|| RegularCheck.HaveSpecChar(textBox3))
            //{
            //    MessageBox.Show(this, "输入中有非法字符,请检查!");
            //    return;
            //}

            if (chbAutoCreateCheckCode.Checked)
            {
                if (txtFormatMachineCode.Text.Trim().Equals("") || txtFormatCardCode.Text.Trim().Equals(""))
                {
                    MessageBox.Show(this, "您已选择自动生成标准编码,但仪器检测编码格式或标准速测编码格式为空,请检查!");
                    return;
                }
            }
            sysBll.Update("OptValue='" + numericUpDown1.Value.ToString() + "'", "SysCode='010101'", out err);
            sysBll.Update("OptValue='" + numericUpDown2.Value.ToString() + "'", "SysCode='010102'", out err);
            sysBll.Update("OptValue='" + cmbSysUnit.Text.Trim() + "'", "SysCode='010103'", out err);

            // sysBll.Update("OptValue='" + checkBox1.Checked.ToString() + "'", "SysCode='020101'", out sErr);
            //sysBll.Update("OptValue='" + checkBox2.Checked.ToString() + "'", "SysCode='020102'", out sErr);
            // sysBll.Update("OptValue='" + checkBox3.Checked.ToString() + "'", "SysCode='020103'", out sErr);
            //sysBll.Update("OptValue='" + checkBox4.Checked.ToString() + "'", "SysCode='020104'", out sErr);
            sysBll.Update("OptValue='" + chbAllowHandInputCheckUint.Checked.ToString() + "'", "SysCode='020105'", out err);
            sysBll.Update("OptValue='" + chbAutoCreateCheckCode.Checked.ToString() + "'", "SysCode='020106'", out err);
            sysBll.Update("OptValue='" + txtFormatMachineCode.Text.Trim() + "'", "SysCode='020107'", out err);
            sysBll.Update("OptValue='" + txtFormatCardCode.Text.Trim() + "'", "SysCode='020108'", out err);
            sysBll.Update("OptValue='" + txtQualitativeCodeFormat.Text.Trim() + "'", "SysCode='020109'", out err);

            sysBll.Update("OptValue='" + txtServerIp.Text.Trim() + "'", "SysCode='020201'", out err);
            sysBll.Update("OptValue='" + txtServerId.Text.Trim() + "'", "SysCode='020202'", out err);
            sysBll.Update("OptValue='" + txtServerPwd.Text.Trim() + "'", "SysCode='020203'", out err);

            sysBll.Update("OptValue='" + numericUpDown8.Value.ToString() + "'", "SysCode='020301'", out err);
            sysBll.Update("OptValue='" + numericUpDown7.Value.ToString() + "'", "SysCode='020302'", out err);
            sysBll.Update("OptValue='" + numericUpDown6.Value.ToString() + "'", "SysCode='020303'", out err);
            sysBll.Update("OptValue='" + numericUpDown5.Value.ToString() + "'", "SysCode='020304'", out err);
            sysBll.Update("OptValue='" + txtTimerEndPlayWav.Text.Trim() + "'", "SysCode='020305'", out err);

            ShareOption.SysTemperature = numericUpDown1.Value;
            ShareOption.SysHumidity = numericUpDown2.Value;
            ShareOption.SysUnit = cmbSysUnit.Text.Trim();
            //ShareOption.SysAutoLogin = checkBox1.Checked;
            ShareOption.SysExitPrompt = chbExitPrompt.Checked;
            ShareOption.AllowHandInputCheckUint = chbAllowHandInputCheckUint.Checked;
            ShareOption.SysStdCodeSame = chbAutoCreateCheckCode.Checked;
            ShareOption.FormatStrMachineCode = txtFormatMachineCode.Text.Trim();
            ShareOption.FormatStandardCode = txtFormatCardCode.Text.Trim();
            ShareOption.SysServerIP = txtServerIp.Text.Trim();
            ShareOption.SysServerID = txtServerId.Text.Trim();
            ShareOption.SysServerPass = txtServerPwd.Text.Trim();
            ShareOption.SysTimer1 = numericUpDown8.Value;
            ShareOption.SysTimer2 = numericUpDown7.Value;
            ShareOption.SysTimer3 = numericUpDown6.Value;
            ShareOption.SysTimer4 = numericUpDown5.Value;
            ShareOption.SysTimerEndPlayWav = txtTimerEndPlayWav.Text;
            CommonOperation.writeWebServer(txtServerIp.Text.Trim());
            clsMachineOpr.GetDY5000();
            Close();
        }


        /// <summary>
        /// 调用声音文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.DefaultExt = "wav";
            openFileDialog1.Filter = "Wav音乐文件(*.wav)|*.wav|All files (*.*)|*.*";
            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                txtTimerEndPlayWav.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private bool editMachineInfo()
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return false;
            }
            machineModel = new clsMachine();
            machineModel.SysCode = c1FlexGrid1.Rows[row]["SysCode"].ToString();
            machineModel.MachineName = c1FlexGrid1.Rows[row]["MachineName"].ToString();
            machineModel.MachineModel = c1FlexGrid1.Rows[row]["MachineModel"].ToString();
            machineModel.ShortCut = c1FlexGrid1.Rows[row]["ShortCut"].ToString();
            machineModel.Company = c1FlexGrid1.Rows[row]["Company"].ToString();
            machineModel.Protocol = c1FlexGrid1.Rows[row]["Protocol"].ToString();
            machineModel.LinkComNo = Convert.ToInt16(c1FlexGrid1.Rows[row]["LinkComNo"]);
            machineModel.IsSupport = Convert.ToBoolean(c1FlexGrid1.Rows[row]["IsSupport"]);
            machineModel.TestValue = Convert.ToSingle(c1FlexGrid1.Rows[row]["TestValue"]);
            machineModel.TestSign = c1FlexGrid1.Rows[row]["TestSign"].ToString();
            machineModel.LinkStdCode = c1FlexGrid1.Rows[row]["LinkStdCode"].ToString();

            frmMachineEdit machineEdit = new frmMachineEdit();
            machineEdit.setValue(machineModel);
            DialogResult dr = machineEdit.ShowDialog(FrmMain.formMain);
            if (dr == DialogResult.OK)
            {
                DataTable dtbl = machineBll.GetAsDataTable("IsShow=True", "orderId ASC", 0);
                c1FlexGrid1.SetDataBinding(dtbl.DataSet, "Machine");
                setGridStyle();
            }
            return true;
        }


        /// <summary>
        /// 点击修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            editMachineInfo();
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConn_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
                {
                   localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                    //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                    ws.Url = txtServerIp.Text.Trim();

                    string blrtn = ws.CheckConnection(txtServerId.Text.Trim(), FormsAuthentication.HashPasswordForStoringInConfigFile(txtServerPwd.Text.Trim(), "MD5").ToString());
                    if (blrtn.Equals("true"))
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
                {
                    ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                   // DY.WebService.ForNet.DataDriver ws = new DY.WebService.ForNet.DataDriver();
                    ws.Url = txtServerIp.Text.Trim();
                    string sErr = string.Empty;
                    bool blrtn = ws.CheckConnection(txtServerId.Text.Trim(), txtServerPwd.Text.Trim(), out sErr);
                    if (blrtn)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bTestConn = true;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSysOption_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bTestConn)
            {
                bTestConn = false;
                e.Cancel = true;
            }
        }

        private void c1FlexGrid1_DoubleClick(object sender, System.EventArgs e)
        {
            editMachineInfo();
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnSetting_Click(object sender, EventArgs e)
        //{
        //    FrmMachinList frmmachine = new FrmMachinList();
        //    frmmachine.ShowDialog();
        //}

        ///// <summary>
        ///// 快捷键打开配置
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void frmSysOption_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.I && e.Control)
        //    {
        //        btnSetting.PerformClick();
        //    }
        //}
    }
}
