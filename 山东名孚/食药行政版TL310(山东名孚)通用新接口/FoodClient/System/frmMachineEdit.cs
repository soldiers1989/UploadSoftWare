using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using JH.CommBase;

namespace FoodClient
{
	/// <summary>
	/// frmMachineEdit 的摘要说明。
	/// </summary>
    public class frmMachineEdit : TitleBarBase
    {
        #region 窗体变量
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProtocol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTestValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSysCode;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.ComboBox cmbSign;
        private System.Windows.Forms.CheckBox chkIsDef;
        private System.Windows.Forms.Label label8;
        private Label label6;
        private GroupBox groupBox1;
        private Button btnDry;
        private Button btnGoldCard;
        private Button btnNOTry;
        private Button btnHolesSetting;
        private Button btnDelItems;
        private Button btnAddItems;
        private Button btnEdit;
        private TextBox txtGNName;
        private Label label10;
        private C1.Win.C1List.C1Combo cmbCheckItem;
        private Label label11;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;

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
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMachineEdit));
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProtocol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTestValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtSysCode = new System.Windows.Forms.TextBox();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.cmbSign = new System.Windows.Forms.ComboBox();
            this.chkIsDef = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDry = new System.Windows.Forms.Button();
            this.btnGoldCard = new System.Windows.Forms.Button();
            this.btnNOTry = new System.Windows.Forms.Button();
            this.btnHolesSetting = new System.Windows.Forms.Button();
            this.btnDelItems = new System.Windows.Forms.Button();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtGNName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCheckItem = new C1.Win.C1List.C1Combo();
            this.label11 = new System.Windows.Forms.Label();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCompany
            // 
            this.txtCompany.Enabled = false;
            this.txtCompany.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCompany.Location = new System.Drawing.Point(92, 62);
            this.txtCompany.MaxLength = 50;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(392, 26);
            this.txtCompany.TabIndex = 2;
            // 
            // txtModel
            // 
            this.txtModel.Enabled = false;
            this.txtModel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtModel.Location = new System.Drawing.Point(590, 29);
            this.txtModel.MaxLength = 50;
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(43, 26);
            this.txtModel.TabIndex = 1;
            this.txtModel.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(92, 32);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(392, 26);
            this.txtName.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(16, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "生产厂商：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(143, 384);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "检测标准：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(13, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "使用端口：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(514, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "仪器型号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "仪器名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProtocol
            // 
            this.txtProtocol.Enabled = false;
            this.txtProtocol.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProtocol.Location = new System.Drawing.Point(590, 59);
            this.txtProtocol.MaxLength = 50;
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Size = new System.Drawing.Size(43, 26);
            this.txtProtocol.TabIndex = 3;
            this.txtProtocol.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(514, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "使用插件：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // txtTestValue
            // 
            this.txtTestValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestValue.Location = new System.Drawing.Point(275, 379);
            this.txtTestValue.MaxLength = 50;
            this.txtTestValue.Name = "txtTestValue";
            this.txtTestValue.Size = new System.Drawing.Size(64, 26);
            this.txtTestValue.TabIndex = 7;
            this.txtTestValue.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(13, 385);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 18);
            this.label9.TabIndex = 11;
            this.label9.Text = "系统编码：";
            this.label9.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(555, 379);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 26);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(467, 379);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 26);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtSysCode
            // 
            this.txtSysCode.Enabled = false;
            this.txtSysCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSysCode.Location = new System.Drawing.Point(88, 379);
            this.txtSysCode.MaxLength = 50;
            this.txtSysCode.Name = "txtSysCode";
            this.txtSysCode.Size = new System.Drawing.Size(42, 26);
            this.txtSysCode.TabIndex = 12;
            this.txtSysCode.Visible = false;
            // 
            // cmbCom
            // 
            this.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCom.Location = new System.Drawing.Point(92, 95);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(72, 24);
            this.cmbCom.TabIndex = 4;
            // 
            // cmbSign
            // 
            this.cmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbSign.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSign.Items.AddRange(new object[] {
            ">",
            "<",
            "≥",
            "≤"});
            this.cmbSign.Location = new System.Drawing.Point(219, 379);
            this.cmbSign.Name = "cmbSign";
            this.cmbSign.Size = new System.Drawing.Size(50, 26);
            this.cmbSign.TabIndex = 6;
            this.cmbSign.Visible = false;
            // 
            // chkIsDef
            // 
            this.chkIsDef.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIsDef.Location = new System.Drawing.Point(186, 97);
            this.chkIsDef.Name = "chkIsDef";
            this.chkIsDef.Size = new System.Drawing.Size(259, 24);
            this.chkIsDef.TabIndex = 5;
            this.chkIsDef.Text = "是否默认关联自动检测快捷按钮";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(340, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "为合格";
            this.label8.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(18, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(216, 16);
            this.label6.TabIndex = 21;
            this.label6.Text = "检测项目已默认为农药残留！";
            this.label6.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDry);
            this.groupBox1.Controls.Add(this.btnGoldCard);
            this.groupBox1.Controls.Add(this.btnNOTry);
            this.groupBox1.Controls.Add(this.btnHolesSetting);
            this.groupBox1.Controls.Add(this.btnDelItems);
            this.groupBox1.Controls.Add(this.btnAddItems);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.txtGNName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbCheckItem);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.c1FlexGrid1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 245);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置对应检测项目";
            // 
            // btnDry
            // 
            this.btnDry.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDry.Location = new System.Drawing.Point(272, 138);
            this.btnDry.Name = "btnDry";
            this.btnDry.Size = new System.Drawing.Size(75, 26);
            this.btnDry.TabIndex = 34;
            this.btnDry.Text = "干化学法";
            this.btnDry.UseVisualStyleBackColor = true;
            this.btnDry.Click += new System.EventHandler(this.btnDry_Click);
            // 
            // btnGoldCard
            // 
            this.btnGoldCard.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGoldCard.Location = new System.Drawing.Point(272, 113);
            this.btnGoldCard.Name = "btnGoldCard";
            this.btnGoldCard.Size = new System.Drawing.Size(75, 26);
            this.btnGoldCard.TabIndex = 33;
            this.btnGoldCard.Text = "金标卡法";
            this.btnGoldCard.UseVisualStyleBackColor = true;
            this.btnGoldCard.Click += new System.EventHandler(this.btnGoldCard_Click);
            // 
            // btnNOTry
            // 
            this.btnNOTry.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNOTry.Location = new System.Drawing.Point(272, 88);
            this.btnNOTry.Name = "btnNOTry";
            this.btnNOTry.Size = new System.Drawing.Size(75, 26);
            this.btnNOTry.TabIndex = 32;
            this.btnNOTry.Text = "非试纸法";
            this.btnNOTry.UseVisualStyleBackColor = true;
            this.btnNOTry.Click += new System.EventHandler(this.btnNOTry_Click);
            // 
            // btnHolesSetting
            // 
            this.btnHolesSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHolesSetting.Location = new System.Drawing.Point(355, 150);
            this.btnHolesSetting.Name = "btnHolesSetting";
            this.btnHolesSetting.Size = new System.Drawing.Size(112, 26);
            this.btnHolesSetting.TabIndex = 31;
            this.btnHolesSetting.Text = "非样品孔位设置";
            this.btnHolesSetting.Visible = false;
            this.btnHolesSetting.Click += new System.EventHandler(this.btnHolesSetting_Click);
            // 
            // btnDelItems
            // 
            this.btnDelItems.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelItems.Location = new System.Drawing.Point(489, 150);
            this.btnDelItems.Name = "btnDelItems";
            this.btnDelItems.Size = new System.Drawing.Size(112, 26);
            this.btnDelItems.TabIndex = 30;
            this.btnDelItems.Text = "删除仪器检测项目";
            this.btnDelItems.Click += new System.EventHandler(this.btnDelItems_Click);
            // 
            // btnAddItems
            // 
            this.btnAddItems.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddItems.Location = new System.Drawing.Point(355, 112);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(112, 26);
            this.btnAddItems.TabIndex = 29;
            this.btnAddItems.Text = "增加仪器检测项目";
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.Location = new System.Drawing.Point(489, 112);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(112, 26);
            this.btnEdit.TabIndex = 27;
            this.btnEdit.Text = "修改对应检测项目";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtGNName
            // 
            this.txtGNName.Enabled = false;
            this.txtGNName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGNName.Location = new System.Drawing.Point(376, 24);
            this.txtGNName.MaxLength = 50;
            this.txtGNName.Name = "txtGNName";
            this.txtGNName.Size = new System.Drawing.Size(242, 26);
            this.txtGNName.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(270, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 16);
            this.label10.TabIndex = 26;
            this.label10.Text = "仪器检测项目：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheckItem
            // 
            this.cmbCheckItem.AddItemSeparator = ';';
            this.cmbCheckItem.Caption = "";
            this.cmbCheckItem.CaptionHeight = 17;
            this.cmbCheckItem.CaptionStyle = style9;
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
            this.cmbCheckItem.EvenRowStyle = style10;
            this.cmbCheckItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckItem.FooterStyle = style11;
            this.cmbCheckItem.GapHeight = 2;
            this.cmbCheckItem.HeadingStyle = style12;
            this.cmbCheckItem.HighLightRowStyle = style13;
            this.cmbCheckItem.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckItem.Images"))));
            this.cmbCheckItem.ItemHeight = 15;
            this.cmbCheckItem.Location = new System.Drawing.Point(376, 56);
            this.cmbCheckItem.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckItem.MaxDropDownItems = ((short)(20));
            this.cmbCheckItem.MaxLength = 32767;
            this.cmbCheckItem.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckItem.Name = "cmbCheckItem";
            this.cmbCheckItem.OddRowStyle = style14;
            this.cmbCheckItem.PartialRightColumn = false;
            this.cmbCheckItem.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckItem.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckItem.SelectedStyle = style15;
            this.cmbCheckItem.Size = new System.Drawing.Size(242, 22);
            this.cmbCheckItem.Style = style16;
            this.cmbCheckItem.TabIndex = 23;
            this.cmbCheckItem.PropBag = resources.GetString("cmbCheckItem.PropBag");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(270, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "对应检测项目：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 24);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 23;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(266, 215);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 22;
            this.c1FlexGrid1.SelChange += new System.EventHandler(this.c1FlexGrid1_SelChange);
            // 
            // frmMachineEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(643, 415);
            this.Controls.Add(this.cmbSign);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSysCode);
            this.Controls.Add(this.txtTestValue);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkIsDef);
            this.Controls.Add(this.cmbCom);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMachineEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测仪器设置";
            this.Load += new System.EventHandler(this.frmMachineEdit_Load);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.cmbCom, 0);
            this.Controls.SetChildIndex(this.chkIsDef, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtModel, 0);
            this.Controls.SetChildIndex(this.txtCompany, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtProtocol, 0);
            this.Controls.SetChildIndex(this.txtTestValue, 0);
            this.Controls.SetChildIndex(this.txtSysCode, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmbSign, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion]

        private bool IsCreatDT = false;
        private bool IsRead = false;
        private bool IsDY3000DYOpen = false;
        private string protocol = string.Empty;
        /// <summary>
        /// 检测项目组合字符串
        /// </summary>
        private string linkStdCode = string.Empty;
        private string linkStdCode1 = string.Empty;
        private string linkStdCode2 = string.Empty;
        private string linkStdCode3 = string.Empty;
        private DataTable initDtbl = new DataTable("dt1");
        private clsDY3000DY curDY3000DY=null;

      

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMachineEdit()
        {
            InitializeComponent();
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
            //Control.CheckForIllegalCrossThreadCalls = false;//解决多线程冲突属性.不是很好的办法
            //设置串口列表号数
            object[] obj = new object[256];
            for (int i = 0; i < 256; i++)
            {
                obj[i] = "COM" + (i + 1).ToString() + ":";
            }

            cmbCom.Items.AddRange(obj);
            if (!IsCreatDT)
            {
                DataColumn myCol;

                myCol = new DataColumn();
                myCol.DataType = typeof(string);// System.Type.GetType("System.String");
                myCol.ColumnName = "仪器检测项目";
                initDtbl.Columns.Add(myCol);

                myCol = new DataColumn();
                myCol.DataType = typeof(string);
                myCol.ColumnName = "对应检测项目";
                initDtbl.Columns.Add(myCol);

                myCol = new DataColumn();
                myCol.DataType = typeof(string);
                myCol.ColumnName = "检测项目代码";
                initDtbl.Columns.Add(myCol);

                myCol = new DataColumn();
                myCol.DataType = typeof(string);
                myCol.ColumnName = "检测方法代码";
                initDtbl.Columns.Add(myCol);

                myCol = new DataColumn();
                myCol.DataType = typeof(string);
                myCol.ColumnName = "单位";
                initDtbl.Columns.Add(myCol);
                IsCreatDT = true;
            }
            curDY3000DY = new clsDY3000DY();
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param

        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            if (e.Code == MessageNotification.NotificationInfo.ReadDY3000DYItem)
            {
                ShowResult(e.Message);
            }
        }
        /// <summary>
        /// 委托回调
        /// </summary>
        /// <param name="s"></param>
        private delegate void InvokeDelegate(string item);

        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(string item)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new InvokeDelegate(showOnControl), item);
            }
            else
            {
                showOnControl(item);
            }
        }
        /// <summary>
        /// 赋值操作设置
        /// </summary>
        /// <param name="model"></param>
        internal void setValue(clsMachine model)
        {
            txtSysCode.Text = model.SysCode;
            txtName.Text = model.MachineName;
           
            txtModel.Text = model.MachineModel;
            txtCompany.Text = model.Company;
            txtProtocol.Text = model.Protocol;
            chkIsDef.Checked = model.IsSupport;
            cmbCom.SelectedIndex = model.LinkComNo - 1;
            linkStdCode = model.LinkStdCode;
            cmbSign.Text = model.TestSign;
            txtTestValue.Text = model.TestValue.ToString();
            protocol = txtProtocol.Text;
            if (protocol.Equals("RS232DY3000DY"))//DY系列
            {
                btnAddItems.Text = "读取仪器检测项目";
                btnAddItems.Visible = true;
                btnDelItems.Visible = false;
            }
            //else if (protocol.Equals("RS232DY3000") || protocol.Equals("RS232DY5000") || protocol.Equals("RS232DY5000LD"))
          else
            {
                btnAddItems.Text = "增加仪器检测项目";
                btnAddItems.Visible = true;
                btnDelItems.Visible = true;
            }
            //else
            //{
            //    btnAddItems.Visible = false;
            //    btnDelItems.Visible = false;
            //}
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string err = string.Empty;
            if (chkIsDef.Checked)
            {
                int i = clsMachineOpr.GetRecCount(out err);
                if (i >= 1)
                {
                    MessageBox.Show(this, "系统将自动清除以前关联快捷按钮，重启本程序此操作才能生效！");
                    clsMachineOpr.UpdateIsSupport(out err);
                }
            }
            clsMachine model = new clsMachine();
            model.SysCode = txtSysCode.Text.Trim();
            model.LinkComNo = cmbCom.SelectedIndex + 1;
            model.IsSupport = chkIsDef.Checked;
            model.TestValue = Convert.ToSingle(txtTestValue.Text.Trim());
            model.TestSign = cmbSign.Text.Trim();
            model.LinkStdCode = linkStdCode;

            err = string.Empty;
            clsMachineOpr curMachineOpr = new clsMachineOpr();
            curMachineOpr.UpdatePart(model, out err);

            if (!err.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！" + err);
            }
            //退出
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMachineEdit_Load(object sender, System.EventArgs e)
        {
			c1FlexGrid1.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;

            clsCheckItemOpr opr1 = new clsCheckItemOpr();
            DataTable dt1 = opr1.GetAsDataTable("IsLock=false", "SysCode", 11);

            DataRow[] rows = dt1.Select("", "ItemDes asc");

            DataTable t = dt1.Clone();

            t.Clear();

            foreach (DataRow row in rows)

                t.ImportRow(row);

            dt1 = t;

            if (dt1 != null)
            {
                cmbCheckItem.DataSource = dt1;
                cmbCheckItem.DataMember = "CheckItem";

                cmbCheckItem.DisplayMember = "ItemDes";
                cmbCheckItem.ValueMember = "SysCode";
                cmbCheckItem.Columns["SysCode"].Caption = "编号";
                cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
                //cmbCheckItem.Columns["StdCode"].Caption = "编号";
                //cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
                //cmbCheckItem.Columns["SysCode"].Caption = "系统编号";
                cmbCheckItem.Columns["SysCode"].DataWidth = 1;
                cmbCheckItem.AutoSize = true;
            }
 
            protocol = txtProtocol.Text;
            if (!linkStdCode.Equals(""))
            {
                readCheckItemCode();
            }

            if (protocol.Equals("RS232DY5000LD"))//新版仪器可以设置非样品孔位
            {
                btnHolesSetting.Visible = true;
                // btnHolesSetting.Enabled = true;
            }
            if (txtSysCode.Text == "022")
            {
                curDY3000DY.DY660 = txtSysCode.Text;
                btnNOTry.Visible = true;
                btnDry.Visible = true;
                btnGoldCard.Visible = true;
            }
            else 
            {
                btnNOTry.Visible = false ;
                btnDry.Visible = false;
                btnGoldCard.Visible = false;
            }
            string str = txtName.Text;
            if (str.Equals("LZ-4000(T)农药残留快速测试仪") || str.Equals("LZ-4000农药残留快速测试仪"))
            {
                groupBox1.Visible = false;
                label6.Visible = true;
            }
            if (str.Equals("DY-6100(S)ATP荧光检测仪"))
            {
                label4.Visible = false;
                cmbCom.Visible = false;
                chkIsDef.Location = new System.Drawing.Point(16, 81);
            }

			TitleBarText = "检测仪器设置";
            
        }

        /// <summary>
        /// 读取检测项目代码 
        /// </summary>
        private void readCheckItemCode()
        {
            initDtbl.Clear();
            DataRow dr = null;

            linkStdCode1 = string.Empty;
            linkStdCode2 = string.Empty;
            linkStdCode3 = string.Empty;

            string[,] resultArry = null;
            if (protocol.Equals("RS232DY3000DY") || protocol.Equals("RS232TL310"))//DY系列
            {
                resultArry = StringUtil.GetDY3000DYAry(linkStdCode);

                for (int i = 0; i <= resultArry.GetLength(0) - 1; i++)
                {
                    dr = initDtbl.NewRow();
                    dr["仪器检测项目"] = resultArry[i, 0];

                    if (resultArry[i, 1].Equals("-1") || resultArry[i, 1].Equals("-6") || resultArry[i, 1].Equals("-7"))
                    {
                        dr["对应检测项目"] = "尚未对应";
                        dr["检测项目代码"] = string.Empty;
                    }
                    else
                    {
                        dr["对应检测项目"] = clsCheckItemOpr.GetNameFromCode(resultArry[i, 1]);
                        dr["检测项目代码"] = resultArry[i, 1];
                    }
                    dr["检测方法代码"] = resultArry[i, 2];
                    dr["单位"] = resultArry[i, 3];

                    if (resultArry[i, 0].Contains("金标法"))
                    {
                        linkStdCode1 += "{" + resultArry[i, 0] + ":" + resultArry[i, 1] + ":" + resultArry[i, 2] + ":" + resultArry[i, 3] + "}";
                    }
                    if (resultArry[i, 0].Contains("干化学法"))
                    {
                        linkStdCode2 += "{" + resultArry[i, 0] + ":" + resultArry[i, 1] + ":" + resultArry[i, 2] + ":" + resultArry[i, 3] + "}";
                    }
                    if (resultArry[i, 0].Contains("非试纸法"))
                    {
                        linkStdCode3 += "{" + resultArry[i, 0] + ":" + resultArry[i, 1] + ":" + resultArry[i, 2] + ":" + resultArry[i, 3] + "}";
                    }
                    if (btnAddItems.Text == "读取金标卡项目" && resultArry[i, 0].Contains("金标法"))
                    {                       
                        initDtbl.Rows.Add(dr);
                    }
                    else if (btnAddItems.Text == "读取干化学项目" && resultArry[i, 0].Contains("干化学法"))
                    {
                        initDtbl.Rows.Add(dr);
                    }
                    else if (btnAddItems.Text == "读取非试纸法项目" && resultArry[i, 0].Contains("非试纸法"))
                    {
                        initDtbl.Rows.Add(dr);
                    }
                    else if (!resultArry[i, 0].Contains("非试纸法") && !resultArry[i, 0].Contains("金标法") && !resultArry[i, 0].Contains("干化学法"))
                    {
                        initDtbl.Rows.Add(dr);
                    }
                }
                if (txtSysCode.Text == "022") 
                {
                    linkStdCode = linkStdCode1 + linkStdCode2 + linkStdCode3;
                }
                
            }
            else
            {
                resultArry = StringUtil.GetAry(linkStdCode);
                string temp = string.Empty;
                string temp2 = string.Empty;
                for (int i = 0; i <= resultArry.GetLength(0) - 1; i++)
                {

                    dr = initDtbl.NewRow();
                    temp = resultArry[i, 0];
                    temp2 = resultArry[i, 1];

                    dr["仪器检测项目"] = temp;
                    if (temp2.Equals("-1"))
                    {
                        dr["对应检测项目"] = "尚未对应";
                        dr["检测项目代码"] = string.Empty;
                    }
                    else
                    {
                        temp = clsCheckItemOpr.GetNameFromCode(temp2);
                        dr["对应检测项目"] = temp;
                        dr["检测项目代码"] = temp2;
                    }
                    dr["检测方法代码"] = string.Empty;
                    dr["单位"] = string.Empty;
                    initDtbl.Rows.Add(dr);
                }
            }
            IsRead = true;
            c1FlexGrid1.DataSource = initDtbl;
            c1FlexGrid1.AutoSizeCols();
            IsRead = false;

            c1FlexGrid1.Cols["对应检测项目"].AllowSorting = true ;
            c1FlexGrid1.Cols["仪器检测项目"].AllowSorting = true;
            c1FlexGrid1.Cols["检测项目代码"].Visible = true;
            c1FlexGrid1.Cols["检测方法代码"].Visible = true;
            c1FlexGrid1.Cols["单位"].Visible = true;

            if (c1FlexGrid1.Rows.Count >= 2)
            {
                c1FlexGrid1.ColSel = 1;
            }
        }
      

        /// <summary>
        /// 修改对应检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            string editNew = string.Empty;
            string machineItem = string.Empty;
            string gNName = string.Empty;
            string temp = string.Empty;
            string strZY = @".$^{[(|)*+?\";
            string pattern = string.Empty;
            string checkItem = string.Empty;

            Regex reg = null;
            if (c1FlexGrid1.Rows[row][0] != null)
            {
                machineItem = c1FlexGrid1.Rows[row][0].ToString();
            }
            int len = machineItem.Length;

            for (int i = 0; i < len; i++)
            {
                temp = machineItem.Substring(i, 1);
                if (strZY.IndexOf(temp, 0) > 0)
                {
                    gNName += @"\" + temp;
                }
                else
                {
                    gNName += temp;
                }
            }

            if (cmbCheckItem.SelectedValue != null)
            {
                checkItem = cmbCheckItem.SelectedValue.ToString();
            }

            if (protocol.Equals("RS232DY3000DY") || protocol.Equals("RS232TL310"))//DY系列
            {
                if (protocol.Equals("RS232TL310"))
                {
                    string cValue = c1FlexGrid1.Rows[row][3].ToString();
                    string unit = c1FlexGrid1.Rows[row][4].ToString();
                    if (cmbCheckItem.SelectedValue != null)
                    {
                        editNew = "{" + machineItem + ":" + checkItem + ":" + cValue + ":" + unit + "}";
                    }
                    else
                    {
                        editNew = "{" + machineItem + ":-1" + ":" + cValue + ":" + unit + "}";
                    }
                }
                else
                {
                    string cValue = c1FlexGrid1.Rows[row][3].ToString();
                    string unit = c1FlexGrid1.Rows[row][4].ToString();
                    if (cmbCheckItem.SelectedValue != null)
                    {
                        editNew = "{" + machineItem + ":" + checkItem + ":" + cValue + ":" + unit + "}";
                    }
                    else
                    {
                        editNew = "{" + machineItem + ":-1" + ":" + cValue + ":" + unit + "}";
                    }
                }

				pattern = @"({" + gNName + @":[\s\S\t]*?:[\s\S\t]*?:[\s\S\t]*?})";
				reg = new Regex(pattern, RegexOptions.IgnoreCase);
                linkStdCode = reg.Replace(linkStdCode, editNew);
            }
            else //非DY系列
            {
                if (cmbCheckItem.SelectedValue != null)
                {
                    editNew = "{" + machineItem + ":" + checkItem + "}";
                }
                else
                {
                    editNew = "{" + machineItem + ":-1}";
                }
                pattern = @"({" + gNName + @":[\S\t]*?})";
                reg = new Regex(pattern, RegexOptions.IgnoreCase);
                linkStdCode = reg.Replace(linkStdCode, editNew);
            }
            readCheckItemCode();
            //IsRead = true;
            //c1FlexGrid1.DataSource = initDtbl;
            c1FlexGrid1.RowSel = row;
        }

        private void btnAddItems_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (txtModel.Text == "DY6600")
                {
                    if (btnAddItems.Text.Equals("读取仪器检测项目"))
                    {
                        DialogResult dr = MessageBox.Show(this, "请选择检测方式，再读取仪器检测项目!", "询问", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
                else if (txtModel.Text == "LZ4000")
                {
                    
                }
                string machineName = txtGNName.Text;
                string btnAddText = btnAddItems.Text;
                if (btnAddText.Equals("确定"))
                {
                    machineName = txtGNName.Text.Trim();
                    bool blnIsExist = false;
                    if (!machineName.Equals(""))
                    {
                        //增加代码
                        //检查项目是否有重复
                        if (c1FlexGrid1.Rows.Count > 1)
                        {
                            object obj = null;
                            for (int i = 0; i <= c1FlexGrid1.Rows.Count - 1; i++)
                            {
                                obj = c1FlexGrid1.Rows[i]["仪器检测项目"];
                                if (obj != null && obj.ToString().Equals(machineName))
                                {
                                    blnIsExist = true;
                                    break;
                                }
                            }
                        }
                        if (!blnIsExist)
                        {
                            string editNew = string.Empty;
                            int len = machineName.Length;
                            string gNName = string.Empty;
                            string temp = string.Empty;
                            string strZY = @".$^{[(|)*+?\";

                            for (int i = 0; i < len; i++)
                            {
                                temp = machineName.Substring(i, 1);
                                if (strZY.IndexOf(temp, 0) > 0)
                                {
                                    gNName = gNName + @"\" + temp;
                                }
                                else
                                {
                                    gNName = gNName + temp;
                                }
                            }
                            if (cmbCheckItem.SelectedValue != null)
                            {
                                editNew = "{" + machineName + ":" + cmbCheckItem.SelectedValue.ToString() + "}";
                            }
                            else
                            {
                                editNew = "{" + machineName + ":-1}";
                            }
                            linkStdCode = linkStdCode + editNew;
                            readCheckItemCode();
                            IsRead = true;
                            c1FlexGrid1.DataSource = initDtbl;
                            IsRead = false;
                            c1FlexGrid1.RowSel = c1FlexGrid1.Rows.Count - 1;
                        }
                        else
                        {
                            MessageBox.Show(this, "所要增加的检测项目名称已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show(this, "所要增加的检测项目名称不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    btnAddItems.Text = "增加仪器检测项目";
                    txtGNName.Enabled = false;
                    btnDelItems.Enabled = true;
                    btnEdit.Enabled = true;
                }
                else if (btnAddText.Equals("增加仪器检测项目"))
                {
                    btnAddItems.Text = "确定";
                    txtGNName.Enabled = true;
                    txtGNName.Text = "";
                    btnDelItems.Enabled = false;
                    btnEdit.Enabled = false;
                }

                else if (btnAddText.Equals("读取仪器检测项目") || btnAddText.Equals("读取非试纸法项目") || btnAddText.Equals("读取金标卡项目") || btnAddText.Equals("读取干化学项目"))//DY读取检测项目
                {
                    if (c1FlexGrid1.Rows.Count >= 2)
                    {
                        DialogResult dr = MessageBox.Show(this, "已读取过检测项目，再次读取将覆盖原有检测项目及相关配置，并需重启软件，真要再次读取吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            return;
                        }
                    }

                    if (!IsDY3000DYOpen)
                    {
                        ShareOption.ComPort = "COM" + (cmbCom.SelectedIndex + 1).ToString() + ":";
                        curDY3000DY = new clsDY3000DY();
                        if (!curDY3000DY.Online)
                        {
                            curDY3000DY.Open();
                            IsDY3000DYOpen = true;
                        }
                    }
                    if (!IsDY3000DYOpen)
                    {
                        MessageBox.Show(this, "无法与仪器正常通讯，请检查！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor = Cursors.Default;
                        return;
                    }
                    btnAddItems.Enabled = false;

                    btnEdit.Enabled = false;

                    if (btnAddItems.Text == "读取非试纸法项目")
                    {
                        curDY3000DY.ReadItem(txtSysCode.Text);
                    }
                    if (btnAddItems.Text == "读取金标卡项目")
                    {
                        curDY3000DY.ReadItem1(txtSysCode.Text);
                    }
                    if (btnAddItems.Text == "读取干化学项目")
                    {
                        curDY3000DY.ReadItem2(txtSysCode.Text);
                    }
                    if (btnAddText.Equals("读取仪器检测项目"))
                    {
                        curDY3000DY.ReadItem(txtSysCode.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 从仪器读取检测项目显示结果
        /// </summary>
        /// <param name="items"></param>
        private void showOnControl(string items)//ShowItems
        {
            if (items.Contains("非试纸法"))
            {
                linkStdCode3 = items;
                linkStdCode = linkStdCode3 + linkStdCode1 + linkStdCode2;
            }
            if (items.Contains("金标法"))
            {
                linkStdCode1 = items;
                linkStdCode = linkStdCode3 + linkStdCode1 + linkStdCode2;
            }
            if (items.Contains("干化学法"))
            {
                linkStdCode2 = items;
                linkStdCode = linkStdCode3 + linkStdCode1 + linkStdCode2;
            }
            
            if (!items.Contains("非试纸法") && !items.Contains("金标法") && !items.Contains("干化学法"))
            {
                linkStdCode = items;
            }

            
            readCheckItemCode();
            btnAddItems.Enabled = true;
            btnEdit.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void c1FlexGrid1_SelChange(object sender, EventArgs e)
        {
            if (IsRead)
            {
                return;
            }
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            if (c1FlexGrid1.Rows[row][0] != null)
            {
                txtGNName.Text = c1FlexGrid1.Rows[row][0].ToString();
            }
            if (c1FlexGrid1.Rows[row][2] != null && !c1FlexGrid1.Rows[row][2].ToString().Equals(""))
            {
                cmbCheckItem.SelectedValue = c1FlexGrid1.Rows[row][2].ToString();
                cmbCheckItem.Text = c1FlexGrid1.Rows[row][1].ToString();
            }
            else
            {
                cmbCheckItem.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 删除检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelItems_Click(object sender, System.EventArgs e)
        {
            int row = c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            DialogResult drt = MessageBox.Show(this, "是否要删除名为：" + c1FlexGrid1.Rows[row][0].ToString() + "的检测项目？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drt == DialogResult.Yes)
            {
                string pattern = @"({" + c1FlexGrid1.Rows[row][0].ToString() + @":[\S\t]*?})";
                Regex r = new Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                linkStdCode = r.Replace(linkStdCode, "");
                readCheckItemCode();
                IsRead = true;
                c1FlexGrid1.DataSource = initDtbl;
                IsRead = false;
                if (row == c1FlexGrid1.Rows.Count)
                {
                    c1FlexGrid1.RowSel = c1FlexGrid1.Rows.Count - 1;
                }
                else
                {
                    c1FlexGrid1.RowSel = row;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            windClose();
        }

        private void windClose()
        {
            MessageNotification.GetInstance().DataRead -= NotificationEventHandler;

            if (IsDY3000DYOpen)
            {
                IsDY3000DYOpen = false;
                curDY3000DY.Close();
            }
            this.Dispose();   
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            windClose();
        }

        /// <summary>
        /// 非样品孔位设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHolesSetting_Click(object sender, EventArgs e)
        {
            FrmHolesSet frmHoles = new FrmHolesSet(txtSysCode.Text);
            frmHoles.ShowDialog();
        }

		protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
		{

		}

        private void btnNOTry_Click(object sender, EventArgs e)
        {
            btnAddItems.Text = "读取非试纸法项目";
            readCheckItemCode();
        }

        private void btnGoldCard_Click(object sender, EventArgs e)
        {
            btnAddItems.Text = "读取金标卡项目";
            readCheckItemCode();
        }

        private void btnDry_Click(object sender, EventArgs e)
        {
            btnAddItems.Text = "读取干化学项目";
            readCheckItemCode();
       } 


    }
}
