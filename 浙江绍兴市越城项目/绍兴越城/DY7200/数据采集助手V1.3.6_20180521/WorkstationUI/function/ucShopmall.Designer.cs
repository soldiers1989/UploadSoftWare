namespace WorkstationUI.function
{
    partial class ucShopmall
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucShopmall));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.labelclose = new System.Windows.Forms.Label();
            this.labelback = new System.Windows.Forms.Label();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.labelBrowser = new System.Windows.Forms.Label();
            this.panelBack = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 41);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1011, 568);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // labelclose
            // 
            this.labelclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelclose.AutoSize = true;
            this.labelclose.BackColor = System.Drawing.Color.Transparent;
            this.labelclose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelclose.ForeColor = System.Drawing.Color.White;
            this.labelclose.Location = new System.Drawing.Point(972, 12);
            this.labelclose.Name = "labelclose";
            this.labelclose.Size = new System.Drawing.Size(25, 16);
            this.labelclose.TabIndex = 33;
            this.labelclose.Text = "×";
            this.toolTip1.SetToolTip(this.labelclose, "关闭");
            this.labelclose.Click += new System.EventHandler(this.labelclose_Click);
            this.labelclose.MouseEnter += new System.EventHandler(this.labelclose_MouseEnter);
            this.labelclose.MouseLeave += new System.EventHandler(this.labelclose_MouseLeave);
            // 
            // labelback
            // 
            this.labelback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelback.AutoSize = true;
            this.labelback.BackColor = System.Drawing.Color.Transparent;
            this.labelback.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelback.ForeColor = System.Drawing.Color.White;
            this.labelback.Location = new System.Drawing.Point(762, 8);
            this.labelback.Name = "labelback";
            this.labelback.Size = new System.Drawing.Size(37, 20);
            this.labelback.TabIndex = 34;
            this.labelback.Text = "后退";
            this.labelback.Visible = false;
            this.labelback.Click += new System.EventHandler(this.labelback_Click);
            this.labelback.MouseEnter += new System.EventHandler(this.labelback_MouseEnter);
            this.labelback.MouseLeave += new System.EventHandler(this.labelback_MouseLeave);
            // 
            // panelBrowser
            // 
            this.panelBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBrowser.BackColor = System.Drawing.Color.Transparent;
            this.panelBrowser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBrowser.BackgroundImage")));
            this.panelBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBrowser.Location = new System.Drawing.Point(832, 8);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(28, 27);
            this.panelBrowser.TabIndex = 35;
            this.toolTip1.SetToolTip(this.panelBrowser, "在浏览器打开商城");
            this.panelBrowser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelBrowser_MouseClick);
            // 
            // labelBrowser
            // 
            this.labelBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBrowser.AutoSize = true;
            this.labelBrowser.BackColor = System.Drawing.Color.Transparent;
            this.labelBrowser.ForeColor = System.Drawing.Color.White;
            this.labelBrowser.Location = new System.Drawing.Point(691, 12);
            this.labelBrowser.Name = "labelBrowser";
            this.labelBrowser.Size = new System.Drawing.Size(65, 12);
            this.labelBrowser.TabIndex = 36;
            this.labelBrowser.Text = "浏览器预览";
            this.labelBrowser.Visible = false;
            this.labelBrowser.Click += new System.EventHandler(this.labelBrowser_Click);
            this.labelBrowser.MouseEnter += new System.EventHandler(this.labelBrowser_MouseEnter);
            this.labelBrowser.MouseLeave += new System.EventHandler(this.labelBrowser_MouseLeave);
            // 
            // panelBack
            // 
            this.panelBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBack.BackColor = System.Drawing.Color.Transparent;
            this.panelBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBack.BackgroundImage")));
            this.panelBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBack.Location = new System.Drawing.Point(905, 8);
            this.panelBack.Name = "panelBack";
            this.panelBack.Size = new System.Drawing.Size(39, 26);
            this.panelBack.TabIndex = 37;
            this.toolTip1.SetToolTip(this.panelBack, "后退");
            this.panelBack.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelBack_MouseClick);
            // 
            // ucShopmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panelBack);
            this.Controls.Add(this.labelBrowser);
            this.Controls.Add(this.panelBrowser);
            this.Controls.Add(this.labelback);
            this.Controls.Add(this.labelclose);
            this.Controls.Add(this.webBrowser1);
            this.DoubleBuffered = true;
            this.Name = "ucShopmall";
            this.Size = new System.Drawing.Size(1011, 609);
            this.Load += new System.EventHandler(this.ucShopmall_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.Label labelclose;
        public System.Windows.Forms.Label labelback;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.Label labelBrowser;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
