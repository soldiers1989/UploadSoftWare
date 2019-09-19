namespace TicketPrinter
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
            this.button1 = new System.Windows.Forms.Button();
            this.rbxContent = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxContent = new System.Windows.Forms.TextBox();
            this.cbxTitleFontFamily = new System.Windows.Forms.ComboBox();
            this.cbxContent1FontFamily = new System.Windows.Forms.ComboBox();
            this.cbxContent2FontFamily = new System.Windows.Forms.ComboBox();
            this.nudTitleFontSize = new System.Windows.Forms.NumericUpDown();
            this.nudContent1FontSize = new System.Windows.Forms.NumericUpDown();
            this.nudContent2FontSize = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxQrCode = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numUpDownTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudTitleFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContent1FontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContent2FontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTime)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbxContent
            // 
            this.rbxContent.Location = new System.Drawing.Point(89, 128);
            this.rbxContent.Name = "rbxContent";
            this.rbxContent.Size = new System.Drawing.Size(354, 110);
            this.rbxContent.TabIndex = 1;
            this.rbxContent.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "内容2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "标题";
            // 
            // tbxTitle
            // 
            this.tbxTitle.Location = new System.Drawing.Point(89, 10);
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.Size = new System.Drawing.Size(354, 21);
            this.tbxTitle.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "内容1";
            // 
            // tbxContent
            // 
            this.tbxContent.Location = new System.Drawing.Point(89, 59);
            this.tbxContent.Name = "tbxContent";
            this.tbxContent.Size = new System.Drawing.Size(354, 21);
            this.tbxContent.TabIndex = 6;
            // 
            // cbxTitleFontFamily
            // 
            this.cbxTitleFontFamily.FormattingEnabled = true;
            this.cbxTitleFontFamily.Location = new System.Drawing.Point(474, 11);
            this.cbxTitleFontFamily.Name = "cbxTitleFontFamily";
            this.cbxTitleFontFamily.Size = new System.Drawing.Size(121, 20);
            this.cbxTitleFontFamily.TabIndex = 7;
            // 
            // cbxContent1FontFamily
            // 
            this.cbxContent1FontFamily.FormattingEnabled = true;
            this.cbxContent1FontFamily.Location = new System.Drawing.Point(474, 59);
            this.cbxContent1FontFamily.Name = "cbxContent1FontFamily";
            this.cbxContent1FontFamily.Size = new System.Drawing.Size(121, 20);
            this.cbxContent1FontFamily.TabIndex = 8;
            // 
            // cbxContent2FontFamily
            // 
            this.cbxContent2FontFamily.FormattingEnabled = true;
            this.cbxContent2FontFamily.Location = new System.Drawing.Point(474, 130);
            this.cbxContent2FontFamily.Name = "cbxContent2FontFamily";
            this.cbxContent2FontFamily.Size = new System.Drawing.Size(121, 20);
            this.cbxContent2FontFamily.TabIndex = 9;
            // 
            // nudTitleFontSize
            // 
            this.nudTitleFontSize.Location = new System.Drawing.Point(610, 10);
            this.nudTitleFontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudTitleFontSize.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudTitleFontSize.Name = "nudTitleFontSize";
            this.nudTitleFontSize.Size = new System.Drawing.Size(47, 21);
            this.nudTitleFontSize.TabIndex = 10;
            this.nudTitleFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // nudContent1FontSize
            // 
            this.nudContent1FontSize.Location = new System.Drawing.Point(610, 58);
            this.nudContent1FontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudContent1FontSize.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudContent1FontSize.Name = "nudContent1FontSize";
            this.nudContent1FontSize.Size = new System.Drawing.Size(47, 21);
            this.nudContent1FontSize.TabIndex = 11;
            this.nudContent1FontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudContent2FontSize
            // 
            this.nudContent2FontSize.Location = new System.Drawing.Point(610, 129);
            this.nudContent2FontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudContent2FontSize.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudContent2FontSize.Name = "nudContent2FontSize";
            this.nudContent2FontSize.Size = new System.Drawing.Size(47, 21);
            this.nudContent2FontSize.TabIndex = 12;
            this.nudContent2FontSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(663, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Bold";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "二维码内容";
            // 
            // tbxQrCode
            // 
            this.tbxQrCode.Location = new System.Drawing.Point(89, 303);
            this.tbxQrCode.Name = "tbxQrCode";
            this.tbxQrCode.Size = new System.Drawing.Size(354, 21);
            this.tbxQrCode.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(449, 195);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 187);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(530, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "打印张数：";
            // 
            // numUpDownTime
            // 
            this.numUpDownTime.Location = new System.Drawing.Point(610, 168);
            this.numUpDownTime.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUpDownTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownTime.Name = "numUpDownTime";
            this.numUpDownTime.Size = new System.Drawing.Size(47, 21);
            this.numUpDownTime.TabIndex = 18;
            this.numUpDownTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 436);
            this.Controls.Add(this.numUpDownTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbxQrCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.nudContent2FontSize);
            this.Controls.Add(this.nudContent1FontSize);
            this.Controls.Add(this.nudTitleFontSize);
            this.Controls.Add(this.cbxContent2FontFamily);
            this.Controls.Add(this.cbxContent1FontFamily);
            this.Controls.Add(this.cbxTitleFontFamily);
            this.Controls.Add(this.tbxContent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbxContent);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTitleFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContent1FontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContent2FontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rbxContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxContent;
        private System.Windows.Forms.ComboBox cbxTitleFontFamily;
        private System.Windows.Forms.ComboBox cbxContent1FontFamily;
        private System.Windows.Forms.ComboBox cbxContent2FontFamily;
        private System.Windows.Forms.NumericUpDown nudTitleFontSize;
        private System.Windows.Forms.NumericUpDown nudContent1FontSize;
        private System.Windows.Forms.NumericUpDown nudContent2FontSize;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxQrCode;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numUpDownTime;
    }
}

