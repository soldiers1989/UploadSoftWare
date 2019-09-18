namespace WorkstationUI.function
{
    partial class frmIntrument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntrument));
            this.labelClose = new System.Windows.Forms.Label();
            this.btnrepair = new CCWin.SkinControl.SkinButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIntrumentNo = new System.Windows.Forms.TextBox();
            this.txtIntrumentName = new System.Windows.Forms.TextBox();
            this.txtmadeplace = new System.Windows.Forms.TextBox();
            this.txtCommunication = new System.Windows.Forms.TextBox();
            this.txtProtocol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(369, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 53;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseEnter += new System.EventHandler(this.labelClose_MouseEnter);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // btnrepair
            // 
            this.btnrepair.BackColor = System.Drawing.Color.Transparent;
            this.btnrepair.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnrepair.DownBack = null;
            this.btnrepair.Location = new System.Drawing.Point(163, 232);
            this.btnrepair.MouseBack = null;
            this.btnrepair.Name = "btnrepair";
            this.btnrepair.NormlBack = null;
            this.btnrepair.Size = new System.Drawing.Size(75, 23);
            this.btnrepair.TabIndex = 67;
            this.btnrepair.Text = "修改";
            this.btnrepair.UseVisualStyleBackColor = false;
            this.btnrepair.Click += new System.EventHandler(this.btnrepair_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(48, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 68;
            this.label2.Text = "仪器编号：";
            // 
            // txtIntrumentNo
            // 
            this.txtIntrumentNo.Location = new System.Drawing.Point(129, 45);
            this.txtIntrumentNo.Name = "txtIntrumentNo";
            this.txtIntrumentNo.Size = new System.Drawing.Size(207, 21);
            this.txtIntrumentNo.TabIndex = 69;
            // 
            // txtIntrumentName
            // 
            this.txtIntrumentName.Location = new System.Drawing.Point(129, 78);
            this.txtIntrumentName.Name = "txtIntrumentName";
            this.txtIntrumentName.Size = new System.Drawing.Size(207, 21);
            this.txtIntrumentName.TabIndex = 70;
            // 
            // txtmadeplace
            // 
            this.txtmadeplace.Location = new System.Drawing.Point(129, 117);
            this.txtmadeplace.Name = "txtmadeplace";
            this.txtmadeplace.Size = new System.Drawing.Size(207, 21);
            this.txtmadeplace.TabIndex = 71;
            // 
            // txtCommunication
            // 
            this.txtCommunication.Location = new System.Drawing.Point(129, 155);
            this.txtCommunication.Name = "txtCommunication";
            this.txtCommunication.Size = new System.Drawing.Size(207, 21);
            this.txtCommunication.TabIndex = 72;
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(129, 196);
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Size = new System.Drawing.Size(207, 21);
            this.txtProtocol.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(48, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 74;
            this.label1.Text = "仪器名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(60, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 75;
            this.label3.Text = "制造商：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(48, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 76;
            this.label4.Text = "通信方式：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(47, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 77;
            this.label5.Text = "通信协议：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 78;
            this.label6.Text = "修改仪器";
            // 
            // frmIntrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(406, 276);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(this.txtCommunication);
            this.Controls.Add(this.txtmadeplace);
            this.Controls.Add(this.txtIntrumentName);
            this.Controls.Add(this.txtIntrumentNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnrepair);
            this.Controls.Add(this.labelClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmIntrument";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmIntrument";
            this.Load += new System.EventHandler(this.frmIntrument_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClose;
        protected CCWin.SkinControl.SkinButton btnrepair;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIntrumentNo;
        private System.Windows.Forms.TextBox txtIntrumentName;
        private System.Windows.Forms.TextBox txtmadeplace;
        private System.Windows.Forms.TextBox txtCommunication;
        private System.Windows.Forms.TextBox txtProtocol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}