namespace WorkstationUI.function
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
            this.Btn_Save = new CCWin.SkinControl.SkinButton();
            this.label14 = new System.Windows.Forms.Label();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.Txt_User = new System.Windows.Forms.TextBox();
            this.Txt_PassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.labelClose.Location = new System.Drawing.Point(373, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 53;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Save.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.Btn_Save.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.Btn_Save.DownBack = null;
            this.Btn_Save.Location = new System.Drawing.Point(154, 207);
            this.Btn_Save.MouseBack = null;
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.NormlBack = null;
            this.Btn_Save.Size = new System.Drawing.Size(81, 28);
            this.Btn_Save.TabIndex = 109;
            this.Btn_Save.Text = "保存";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(19, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 21);
            this.label14.TabIndex = 110;
            this.label14.Text = "服务器地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Txt_Url
            // 
            this.Txt_Url.Location = new System.Drawing.Point(111, 64);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(261, 21);
            this.Txt_Url.TabIndex = 111;
            // 
            // Txt_User
            // 
            this.Txt_User.Location = new System.Drawing.Point(111, 108);
            this.Txt_User.Name = "Txt_User";
            this.Txt_User.Size = new System.Drawing.Size(261, 21);
            this.Txt_User.TabIndex = 112;
            // 
            // Txt_PassWord
            // 
            this.Txt_PassWord.Location = new System.Drawing.Point(111, 157);
            this.Txt_PassWord.Name = "Txt_PassWord";
            this.Txt_PassWord.Size = new System.Drawing.Size(261, 21);
            this.Txt_PassWord.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 108);
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
            this.label2.Location = new System.Drawing.Point(54, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 115;
            this.label2.Text = "密码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(410, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_PassWord);
            this.Controls.Add(this.Txt_User);
            this.Controls.Add(this.Txt_Url);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Btn_Save);
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
        private CCWin.SkinControl.SkinButton Btn_Save;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.TextBox Txt_User;
        private System.Windows.Forms.TextBox Txt_PassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}