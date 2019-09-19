namespace WorkstationUI.function
{
    partial class frmunitrepair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmunitrepair));
            this.label1 = new System.Windows.Forms.Label();
            this.labelClose = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTestUnit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnitAddr = new System.Windows.Forms.TextBox();
            this.txtChkUnit = new System.Windows.Forms.TextBox();
            this.txtTestUser = new System.Windows.Forms.TextBox();
            this.btnrepair = new CCWin.SkinControl.SkinButton();
            this.cmbExamedUnit = new System.Windows.Forms.ComboBox();
            this.txtdetectNature = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductUnit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProductAddr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "修改";
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(378, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 52;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseEnter += new System.EventHandler(this.labelClose_MouseEnter);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(53, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "检测单位：";
            // 
            // txtTestUnit
            // 
            this.txtTestUnit.Location = new System.Drawing.Point(113, 173);
            this.txtTestUnit.Name = "txtTestUnit";
            this.txtTestUnit.Size = new System.Drawing.Size(237, 21);
            this.txtTestUnit.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(29, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 55;
            this.label3.Text = "检测单位地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(54, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 56;
            this.label4.Text = "被检单位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(64, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 58;
            this.label6.Text = "检测人：";
            // 
            // txtUnitAddr
            // 
            this.txtUnitAddr.Location = new System.Drawing.Point(113, 200);
            this.txtUnitAddr.Name = "txtUnitAddr";
            this.txtUnitAddr.Size = new System.Drawing.Size(237, 21);
            this.txtUnitAddr.TabIndex = 59;
            // 
            // txtChkUnit
            // 
            this.txtChkUnit.Location = new System.Drawing.Point(116, 48);
            this.txtChkUnit.Name = "txtChkUnit";
            this.txtChkUnit.Size = new System.Drawing.Size(237, 21);
            this.txtChkUnit.TabIndex = 60;
            // 
            // txtTestUser
            // 
            this.txtTestUser.Location = new System.Drawing.Point(114, 221);
            this.txtTestUser.Name = "txtTestUser";
            this.txtTestUser.Size = new System.Drawing.Size(237, 21);
            this.txtTestUser.TabIndex = 62;
            // 
            // btnrepair
            // 
            this.btnrepair.BackColor = System.Drawing.Color.Transparent;
            this.btnrepair.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnrepair.DownBack = null;
            this.btnrepair.Location = new System.Drawing.Point(182, 124);
            this.btnrepair.MouseBack = null;
            this.btnrepair.Name = "btnrepair";
            this.btnrepair.NormlBack = null;
            this.btnrepair.Size = new System.Drawing.Size(56, 29);
            this.btnrepair.TabIndex = 66;
            this.btnrepair.Text = "修改";
            this.btnrepair.UseVisualStyleBackColor = false;
            this.btnrepair.Click += new System.EventHandler(this.btnrepair_Click);
            // 
            // cmbExamedUnit
            // 
            this.cmbExamedUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExamedUnit.FormattingEnabled = true;
            this.cmbExamedUnit.Location = new System.Drawing.Point(92, 331);
            this.cmbExamedUnit.Name = "cmbExamedUnit";
            this.cmbExamedUnit.Size = new System.Drawing.Size(182, 20);
            this.cmbExamedUnit.TabIndex = 69;
            this.cmbExamedUnit.Visible = false;
            // 
            // txtdetectNature
            // 
            this.txtdetectNature.Location = new System.Drawing.Point(115, 86);
            this.txtdetectNature.Name = "txtdetectNature";
            this.txtdetectNature.Size = new System.Drawing.Size(238, 21);
            this.txtdetectNature.TabIndex = 107;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(13, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 106;
            this.label5.Text = "被检单位性质：";
            // 
            // txtProductUnit
            // 
            this.txtProductUnit.Location = new System.Drawing.Point(114, 282);
            this.txtProductUnit.Name = "txtProductUnit";
            this.txtProductUnit.Size = new System.Drawing.Size(239, 21);
            this.txtProductUnit.TabIndex = 113;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(38, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 14);
            this.label8.TabIndex = 112;
            this.label8.Text = "生产单位：";
            // 
            // txtProductAddr
            // 
            this.txtProductAddr.Location = new System.Drawing.Point(115, 244);
            this.txtProductAddr.Name = "txtProductAddr";
            this.txtProductAddr.Size = new System.Drawing.Size(238, 21);
            this.txtProductAddr.TabIndex = 111;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(66, 245);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 110;
            this.label9.Text = "产地：";
            // 
            // frmunitrepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(406, 165);
            this.Controls.Add(this.txtProductUnit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtProductAddr);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtdetectNature);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbExamedUnit);
            this.Controls.Add(this.btnrepair);
            this.Controls.Add(this.txtTestUser);
            this.Controls.Add(this.txtChkUnit);
            this.Controls.Add(this.txtUnitAddr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTestUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelClose);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmunitrepair";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmunitrepair";
            this.Load += new System.EventHandler(this.frmunitrepair_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTestUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnitAddr;
        private System.Windows.Forms.TextBox txtChkUnit;
        private System.Windows.Forms.TextBox txtTestUser;
        protected CCWin.SkinControl.SkinButton btnrepair;
        private System.Windows.Forms.ComboBox cmbExamedUnit;
        private System.Windows.Forms.TextBox txtdetectNature;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductUnit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProductAddr;
        private System.Windows.Forms.Label label9;
    }
}