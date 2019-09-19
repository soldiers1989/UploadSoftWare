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
            this.btnlink = new System.Windows.Forms.Button();
            this.btnUpdata = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnlink
            // 
            this.btnlink.Location = new System.Drawing.Point(52, 62);
            this.btnlink.Name = "btnlink";
            this.btnlink.Size = new System.Drawing.Size(75, 23);
            this.btnlink.TabIndex = 0;
            this.btnlink.Text = "通信测试";
            this.btnlink.UseVisualStyleBackColor = true;
            this.btnlink.Click += new System.EventHandler(this.btnlink_Click);
            // 
            // btnUpdata
            // 
            this.btnUpdata.Location = new System.Drawing.Point(197, 62);
            this.btnUpdata.Name = "btnUpdata";
            this.btnUpdata.Size = new System.Drawing.Size(75, 23);
            this.btnUpdata.TabIndex = 1;
            this.btnUpdata.Text = "上传";
            this.btnUpdata.UseVisualStyleBackColor = true;
            this.btnUpdata.Click += new System.EventHandler(this.btnUpdata_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 281);
            this.Controls.Add(this.btnUpdata);
            this.Controls.Add(this.btnlink);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnlink;
        private System.Windows.Forms.Button btnUpdata;
    }
}

