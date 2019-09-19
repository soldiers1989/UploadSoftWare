namespace test
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.btnDownSample = new System.Windows.Forms.Button();
            this.txtShowSample = new System.Windows.Forms.TextBox();
            this.btnUploadData = new System.Windows.Forms.Button();
            this.txtShowUpData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址：";
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(65, 12);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(342, 21);
            this.txtAddr.TabIndex = 1;
            this.txtAddr.Text = "http://plx.verytong.com/";
            // 
            // btnDownSample
            // 
            this.btnDownSample.Location = new System.Drawing.Point(332, 74);
            this.btnDownSample.Name = "btnDownSample";
            this.btnDownSample.Size = new System.Drawing.Size(75, 23);
            this.btnDownSample.TabIndex = 4;
            this.btnDownSample.Text = "下载样品";
            this.btnDownSample.UseVisualStyleBackColor = true;
            this.btnDownSample.Click += new System.EventHandler(this.btnDownSample_Click);
            // 
            // txtShowSample
            // 
            this.txtShowSample.Location = new System.Drawing.Point(12, 73);
            this.txtShowSample.Multiline = true;
            this.txtShowSample.Name = "txtShowSample";
            this.txtShowSample.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowSample.Size = new System.Drawing.Size(314, 91);
            this.txtShowSample.TabIndex = 5;
            // 
            // btnUploadData
            // 
            this.btnUploadData.Location = new System.Drawing.Point(332, 189);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(75, 23);
            this.btnUploadData.TabIndex = 6;
            this.btnUploadData.Text = "上传数据";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // txtShowUpData
            // 
            this.txtShowUpData.Location = new System.Drawing.Point(12, 186);
            this.txtShowUpData.Multiline = true;
            this.txtShowUpData.Name = "txtShowUpData";
            this.txtShowUpData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowUpData.Size = new System.Drawing.Size(314, 85);
            this.txtShowUpData.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 307);
            this.Controls.Add(this.txtShowUpData);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.txtShowSample);
            this.Controls.Add(this.btnDownSample);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Button btnDownSample;
        private System.Windows.Forms.TextBox txtShowSample;
        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.TextBox txtShowUpData;
    }
}

