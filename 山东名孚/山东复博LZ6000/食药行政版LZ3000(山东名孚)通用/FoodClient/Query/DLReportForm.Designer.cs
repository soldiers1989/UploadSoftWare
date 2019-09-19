namespace FoodClient.Query
{
    partial class DLReportForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMeuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMeuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMeuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMeuPrint,
            this.TSMeuPrintPreview,
            this.TSMeuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(885, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TSMeuPrint
            // 
            this.TSMeuPrint.Name = "TSMeuPrint";
            this.TSMeuPrint.Size = new System.Drawing.Size(44, 21);
            this.TSMeuPrint.Text = "打印";
            this.TSMeuPrint.Click += new System.EventHandler(this.TSMeuPrint_Click);
            // 
            // TSMeuPrintPreview
            // 
            this.TSMeuPrintPreview.Name = "TSMeuPrintPreview";
            this.TSMeuPrintPreview.Size = new System.Drawing.Size(68, 21);
            this.TSMeuPrintPreview.Text = "打印预览";
            this.TSMeuPrintPreview.Click += new System.EventHandler(this.TSMeuPrintPreview_Click);
            // 
            // TSMeuExit
            // 
            this.TSMeuExit.Name = "TSMeuExit";
            this.TSMeuExit.Size = new System.Drawing.Size(44, 21);
            this.TSMeuExit.Text = "退出";
            this.TSMeuExit.Click += new System.EventHandler(this.TSMeuExit_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(885, 638);
            this.webBrowser1.TabIndex = 1;
            // 
            // DLReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 663);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DLReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DLReportForm";
            this.Load += new System.EventHandler(this.DLReportForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMeuPrint;
        private System.Windows.Forms.ToolStripMenuItem TSMeuPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem TSMeuExit;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}