namespace FoodClient
{
    partial class FrmBaseDataDownload
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
            this.btnAllDownload = new System.Windows.Forms.Button();
            this.btnFoodClass = new System.Windows.Forms.Button();
            this.btnCheckComTypeOpr = new System.Windows.Forms.Button();
            this.btnStandardType = new System.Windows.Forms.Button();
            this.btnCompanyKind = new System.Windows.Forms.Button();
            this.btnDistrict = new System.Windows.Forms.Button();
            this.btnProduceArea = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAllDownload
            // 
            this.btnAllDownload.ForeColor = System.Drawing.Color.Black;
            this.btnAllDownload.Location = new System.Drawing.Point(141, 180);
            this.btnAllDownload.Name = "btnAllDownload";
            this.btnAllDownload.Size = new System.Drawing.Size(158, 23);
            this.btnAllDownload.TabIndex = 45;
            this.btnAllDownload.Text = "全部下载";
            this.btnAllDownload.UseVisualStyleBackColor = true;
            this.btnAllDownload.Click += new System.EventHandler(this.btnAllDownload_Click);
            // 
            // btnFoodClass
            // 
            this.btnFoodClass.Location = new System.Drawing.Point(48, 38);
            this.btnFoodClass.Name = "btnFoodClass";
            this.btnFoodClass.Size = new System.Drawing.Size(158, 23);
            this.btnFoodClass.TabIndex = 1;
            this.btnFoodClass.Text = "下载样品种类";
            this.btnFoodClass.UseVisualStyleBackColor = true;
            this.btnFoodClass.Click += new System.EventHandler(this.btnFoodClass_Click);
            // 
            // btnCheckComTypeOpr
            // 
            this.btnCheckComTypeOpr.Location = new System.Drawing.Point(48, 80);
            this.btnCheckComTypeOpr.Name = "btnCheckComTypeOpr";
            this.btnCheckComTypeOpr.Size = new System.Drawing.Size(158, 23);
            this.btnCheckComTypeOpr.TabIndex = 2;
            this.btnCheckComTypeOpr.Text = "下载检测点类型";
            this.btnCheckComTypeOpr.UseVisualStyleBackColor = true;
            this.btnCheckComTypeOpr.Click += new System.EventHandler(this.btnCheckComTypeOpr_Click);
            // 
            // btnStandardType
            // 
            this.btnStandardType.Location = new System.Drawing.Point(48, 122);
            this.btnStandardType.Name = "btnStandardType";
            this.btnStandardType.Size = new System.Drawing.Size(158, 23);
            this.btnStandardType.TabIndex = 3;
            this.btnStandardType.Text = "下载检测标准及检测项目";
            this.btnStandardType.UseVisualStyleBackColor = true;
            this.btnStandardType.Click += new System.EventHandler(this.btnStandardType_Click);
            // 
            // btnCompanyKind
            // 
            this.btnCompanyKind.Location = new System.Drawing.Point(236, 122);
            this.btnCompanyKind.Name = "btnCompanyKind";
            this.btnCompanyKind.Size = new System.Drawing.Size(158, 23);
            this.btnCompanyKind.TabIndex = 8;
            this.btnCompanyKind.Text = "下载单位类别及被检单位";
            this.btnCompanyKind.UseVisualStyleBackColor = true;
            this.btnCompanyKind.Click += new System.EventHandler(this.btnCompanyKind_Click);
            // 
            // btnDistrict
            // 
            this.btnDistrict.Location = new System.Drawing.Point(236, 38);
            this.btnDistrict.Name = "btnDistrict";
            this.btnDistrict.Size = new System.Drawing.Size(158, 23);
            this.btnDistrict.TabIndex = 6;
            this.btnDistrict.Text = "下载行政机构";
            this.btnDistrict.UseVisualStyleBackColor = true;
            this.btnDistrict.Click += new System.EventHandler(this.btnDistrict_Click);
            // 
            // btnProduceArea
            // 
            this.btnProduceArea.Location = new System.Drawing.Point(236, 80);
            this.btnProduceArea.Name = "btnProduceArea";
            this.btnProduceArea.Size = new System.Drawing.Size(158, 23);
            this.btnProduceArea.TabIndex = 7;
            this.btnProduceArea.Text = "下载产品产地";
            this.btnProduceArea.UseVisualStyleBackColor = true;
            this.btnProduceArea.Click += new System.EventHandler(this.btnProduceArea_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStandardType);
            this.groupBox1.Controls.Add(this.btnAllDownload);
            this.groupBox1.Controls.Add(this.btnProduceArea);
            this.groupBox1.Controls.Add(this.btnFoodClass);
            this.groupBox1.Controls.Add(this.btnDistrict);
            this.groupBox1.Controls.Add(this.btnCheckComTypeOpr);
            this.groupBox1.Controls.Add(this.btnCompanyKind);
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 223);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基础数据同步下载";
            // 
            // FrmBaseDataDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 261);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaseDataDownload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础数据同步";
            this.Load += new System.EventHandler(this.FrmBaseDataDownload_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAllDownload;
        private System.Windows.Forms.Button btnFoodClass;
        private System.Windows.Forms.Button btnCheckComTypeOpr;
        private System.Windows.Forms.Button btnStandardType;
        private System.Windows.Forms.Button btnCompanyKind;
        private System.Windows.Forms.Button btnDistrict;
        private System.Windows.Forms.Button btnProduceArea;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}