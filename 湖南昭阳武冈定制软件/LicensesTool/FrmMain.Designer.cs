namespace LicensesTool
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateLicense = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBroswerLicense = new System.Windows.Forms.Button();
            this.txtLicenseFile = new System.Windows.Forms.TextBox();
            this.btnOpenSet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateLicense
            // 
            this.btnCreateLicense.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateLicense.Location = new System.Drawing.Point(207, 128);
            this.btnCreateLicense.Name = "btnCreateLicense";
            this.btnCreateLicense.Size = new System.Drawing.Size(88, 44);
            this.btnCreateLicense.TabIndex = 2;
            this.btnCreateLicense.Text = "制 作";
            this.btnCreateLicense.UseVisualStyleBackColor = true;
            this.btnCreateLicense.Click += new System.EventHandler(this.btnCreateLicense_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBroswerLicense);
            this.groupBox1.Controls.Add(this.txtLicenseFile);
            this.groupBox1.Controls.Add(this.btnCreateLicense);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 215);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "授权文件\r\n保存路径";
            // 
            // btnBroswerLicense
            // 
            this.btnBroswerLicense.Location = new System.Drawing.Point(437, 34);
            this.btnBroswerLicense.Name = "btnBroswerLicense";
            this.btnBroswerLicense.Size = new System.Drawing.Size(75, 29);
            this.btnBroswerLicense.TabIndex = 5;
            this.btnBroswerLicense.Text = "浏览...";
            this.btnBroswerLicense.UseVisualStyleBackColor = true;
            this.btnBroswerLicense.Click += new System.EventHandler(this.btnBroswerLicense_Click);
            // 
            // txtLicenseFile
            // 
            this.txtLicenseFile.Location = new System.Drawing.Point(66, 34);
            this.txtLicenseFile.Multiline = true;
            this.txtLicenseFile.Name = "txtLicenseFile";
            this.txtLicenseFile.Size = new System.Drawing.Size(372, 29);
            this.txtLicenseFile.TabIndex = 4;
            // 
            // btnOpenSet
            // 
            this.btnOpenSet.Location = new System.Drawing.Point(375, 248);
            this.btnOpenSet.Name = "btnOpenSet";
            this.btnOpenSet.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSet.TabIndex = 7;
            this.btnOpenSet.Text = "OpenSet";
            this.btnOpenSet.UseVisualStyleBackColor = true;
            this.btnOpenSet.Click += new System.EventHandler(this.btnOpenSet_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 240);
            this.Controls.Add(this.btnOpenSet);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单机版软件加密狗制作工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateLicense;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBroswerLicense;
        private System.Windows.Forms.TextBox txtLicenseFile;
        private System.Windows.Forms.Button btnOpenSet;
    }
}

