namespace WorkstationUI.function
{
    partial class frminputdata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frminputdata));
            this.label1 = new System.Windows.Forms.Label();
            this.txtinput = new System.Windows.Forms.TextBox();
            this.labelClose = new System.Windows.Forms.Label();
            this.btnconfirm = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入：";
            // 
            // txtinput
            // 
            this.txtinput.Location = new System.Drawing.Point(73, 59);
            this.txtinput.Name = "txtinput";
            this.txtinput.Size = new System.Drawing.Size(154, 21);
            this.txtinput.TabIndex = 1;
            this.txtinput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtinput_KeyPress);
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(321, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 52;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelClose_MouseDown);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // btnconfirm
            // 
            this.btnconfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnconfirm.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnconfirm.DownBack = null;
            this.btnconfirm.Location = new System.Drawing.Point(254, 59);
            this.btnconfirm.MouseBack = null;
            this.btnconfirm.Name = "btnconfirm";
            this.btnconfirm.NormlBack = null;
            this.btnconfirm.Size = new System.Drawing.Size(75, 23);
            this.btnconfirm.TabIndex = 65;
            this.btnconfirm.Text = "确定";
            this.btnconfirm.UseVisualStyleBackColor = false;
            this.btnconfirm.Click += new System.EventHandler(this.btnconfirm_Click);
            // 
            // frminputdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(358, 105);
            this.Controls.Add(this.btnconfirm);
            this.Controls.Add(this.labelClose);
            this.Controls.Add(this.txtinput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frminputdata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frminputdata";
            this.Load += new System.EventHandler(this.frminputdata_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtinput;
        private System.Windows.Forms.Label labelClose;
        protected CCWin.SkinControl.SkinButton btnconfirm;
    }
}