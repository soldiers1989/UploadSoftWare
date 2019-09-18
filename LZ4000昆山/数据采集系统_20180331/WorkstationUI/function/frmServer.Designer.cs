﻿namespace WorkstationUI.function
{
    partial class frmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.label10 = new System.Windows.Forms.Label();
            this.labelClose = new System.Windows.Forms.Label();
            this.BtnCommunicate = new CCWin.SkinControl.SkinButton();
            this.label14 = new System.Windows.Forms.Label();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.Txt_User = new System.Windows.Forms.TextBox();
            this.Txt_PassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDetectUnit = new System.Windows.Forms.TextBox();
            this.txtDetectUnitNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrganize = new System.Windows.Forms.TextBox();
            this.txtOrganizeNo = new System.Windows.Forms.TextBox();
            this.txtDetectType = new System.Windows.Forms.TextBox();
            this.btnChkUnitUp = new CCWin.SkinControl.SkinButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtManufacture = new System.Windows.Forms.TextBox();
            this.txtMachineModel = new System.Windows.Forms.TextBox();
            this.txtMachineSerial = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "数据上传网络设置";
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(460, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 53;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseEnter += new System.EventHandler(this.labelClose_MouseEnter);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // BtnCommunicate
            // 
            this.BtnCommunicate.BackColor = System.Drawing.Color.Transparent;
            this.BtnCommunicate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.BtnCommunicate.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnCommunicate.DownBack = null;
            this.BtnCommunicate.Location = new System.Drawing.Point(192, 313);
            this.BtnCommunicate.MouseBack = null;
            this.BtnCommunicate.Name = "BtnCommunicate";
            this.BtnCommunicate.NormlBack = null;
            this.BtnCommunicate.Size = new System.Drawing.Size(85, 23);
            this.BtnCommunicate.TabIndex = 109;
            this.BtnCommunicate.Text = "通信测试";
            this.BtnCommunicate.UseVisualStyleBackColor = false;
            this.BtnCommunicate.Click += new System.EventHandler(this.BtnCommunicate_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(25, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 21);
            this.label14.TabIndex = 110;
            this.label14.Text = "服务器地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Txt_Url
            // 
            this.Txt_Url.Location = new System.Drawing.Point(119, 41);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(317, 21);
            this.Txt_Url.TabIndex = 111;
            // 
            // Txt_User
            // 
            this.Txt_User.Location = new System.Drawing.Point(119, 74);
            this.Txt_User.Name = "Txt_User";
            this.Txt_User.Size = new System.Drawing.Size(317, 21);
            this.Txt_User.TabIndex = 112;
            // 
            // Txt_PassWord
            // 
            this.Txt_PassWord.Location = new System.Drawing.Point(119, 111);
            this.Txt_PassWord.Name = "Txt_PassWord";
            this.Txt_PassWord.PasswordChar = '*';
            this.Txt_PassWord.Size = new System.Drawing.Size(317, 21);
            this.Txt_PassWord.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 21);
            this.label1.TabIndex = 114;
            this.label1.Text = "用户名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(62, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 115;
            this.label2.Text = "密码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 21);
            this.label3.TabIndex = 116;
            this.label3.Text = "接入单位名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(10, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 21);
            this.label4.TabIndex = 117;
            this.label4.Text = "接入单位编号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetectUnit
            // 
            this.txtDetectUnit.Location = new System.Drawing.Point(119, 147);
            this.txtDetectUnit.Name = "txtDetectUnit";
            this.txtDetectUnit.ReadOnly = true;
            this.txtDetectUnit.Size = new System.Drawing.Size(317, 21);
            this.txtDetectUnit.TabIndex = 118;
            // 
            // txtDetectUnitNo
            // 
            this.txtDetectUnitNo.Location = new System.Drawing.Point(119, 181);
            this.txtDetectUnitNo.Name = "txtDetectUnitNo";
            this.txtDetectUnitNo.ReadOnly = true;
            this.txtDetectUnitNo.Size = new System.Drawing.Size(317, 21);
            this.txtDetectUnitNo.TabIndex = 119;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(5, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 21);
            this.label5.TabIndex = 120;
            this.label5.Text = "检测站类型：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(242, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 21);
            this.label6.TabIndex = 122;
            this.label6.Text = "用户昵称：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(319, 340);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(144, 21);
            this.txtUserName.TabIndex = 123;
            this.txtUserName.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(211, 369);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 21);
            this.label7.TabIndex = 124;
            this.label7.Text = "机构名称：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(12, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 21);
            this.label8.TabIndex = 125;
            this.label8.Text = "用户单位标识：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Visible = false;
            // 
            // txtOrganize
            // 
            this.txtOrganize.Location = new System.Drawing.Point(309, 371);
            this.txtOrganize.Name = "txtOrganize";
            this.txtOrganize.Size = new System.Drawing.Size(127, 21);
            this.txtOrganize.TabIndex = 126;
            this.txtOrganize.Visible = false;
            // 
            // txtOrganizeNo
            // 
            this.txtOrganizeNo.Location = new System.Drawing.Point(97, 340);
            this.txtOrganizeNo.Name = "txtOrganizeNo";
            this.txtOrganizeNo.ReadOnly = true;
            this.txtOrganizeNo.Size = new System.Drawing.Size(317, 21);
            this.txtOrganizeNo.TabIndex = 127;
            this.txtOrganizeNo.Visible = false;
            // 
            // txtDetectType
            // 
            this.txtDetectType.Location = new System.Drawing.Point(90, 342);
            this.txtDetectType.Name = "txtDetectType";
            this.txtDetectType.Size = new System.Drawing.Size(129, 21);
            this.txtDetectType.TabIndex = 128;
            this.txtDetectType.Visible = false;
            // 
            // btnChkUnitUp
            // 
            this.btnChkUnitUp.BackColor = System.Drawing.Color.Transparent;
            this.btnChkUnitUp.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.btnChkUnitUp.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnChkUnitUp.DownBack = null;
            this.btnChkUnitUp.Location = new System.Drawing.Point(42, 364);
            this.btnChkUnitUp.MouseBack = null;
            this.btnChkUnitUp.Name = "btnChkUnitUp";
            this.btnChkUnitUp.NormlBack = null;
            this.btnChkUnitUp.Size = new System.Drawing.Size(116, 28);
            this.btnChkUnitUp.TabIndex = 129;
            this.btnChkUnitUp.Text = "同步检测单位信息";
            this.btnChkUnitUp.UseVisualStyleBackColor = false;
            this.btnChkUnitUp.Visible = false;
            this.btnChkUnitUp.Click += new System.EventHandler(this.btnChkUnitUp_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(9, 247);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 21);
            this.label11.TabIndex = 131;
            this.label11.Text = "检测设备型号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(6, 276);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 21);
            this.label12.TabIndex = 132;
            this.label12.Text = "设备唯一序号：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(12, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 21);
            this.label9.TabIndex = 133;
            this.label9.Text = "检测设备厂家：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtManufacture
            // 
            this.txtManufacture.Location = new System.Drawing.Point(119, 215);
            this.txtManufacture.Name = "txtManufacture";
            this.txtManufacture.Size = new System.Drawing.Size(317, 21);
            this.txtManufacture.TabIndex = 134;
            // 
            // txtMachineModel
            // 
            this.txtMachineModel.Location = new System.Drawing.Point(119, 246);
            this.txtMachineModel.Name = "txtMachineModel";
            this.txtMachineModel.Size = new System.Drawing.Size(317, 21);
            this.txtMachineModel.TabIndex = 135;
            // 
            // txtMachineSerial
            // 
            this.txtMachineSerial.Location = new System.Drawing.Point(120, 277);
            this.txtMachineSerial.Name = "txtMachineSerial";
            this.txtMachineSerial.Size = new System.Drawing.Size(317, 21);
            this.txtMachineSerial.TabIndex = 136;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Controls.Add(this.txtMachineSerial);
            this.Controls.Add(this.txtMachineModel);
            this.Controls.Add(this.txtManufacture);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnChkUnitUp);
            this.Controls.Add(this.txtDetectType);
            this.Controls.Add(this.txtOrganizeNo);
            this.Controls.Add(this.txtOrganize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDetectUnitNo);
            this.Controls.Add(this.txtDetectUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_PassWord);
            this.Controls.Add(this.Txt_User);
            this.Controls.Add(this.Txt_Url);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.BtnCommunicate);
            this.Controls.Add(this.labelClose);
            this.Controls.Add(this.label10);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmServer";
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelClose;
        private CCWin.SkinControl.SkinButton BtnCommunicate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.TextBox Txt_User;
        private System.Windows.Forms.TextBox Txt_PassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDetectUnit;
        private System.Windows.Forms.TextBox txtDetectUnitNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOrganize;
        private System.Windows.Forms.TextBox txtOrganizeNo;
        private System.Windows.Forms.TextBox txtDetectType;
        private CCWin.SkinControl.SkinButton btnChkUnitUp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtManufacture;
        private System.Windows.Forms.TextBox txtMachineModel;
        private System.Windows.Forms.TextBox txtMachineSerial;
    }
}