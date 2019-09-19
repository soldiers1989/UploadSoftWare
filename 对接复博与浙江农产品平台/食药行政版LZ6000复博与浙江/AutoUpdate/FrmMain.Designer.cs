
namespace AutoUpdate
{
    partial class FrmMain
    {
        internal System.Windows.Forms.Button ButtonExit;
        internal System.Windows.Forms.Button ButtonStart;
        internal System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label LabelTips;
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
       public void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmMain));
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelTips = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(302, 66);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(67, 24);
            this.ButtonExit.TabIndex = 5;
            this.ButtonExit.Text = "取消退出";
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.CausesValidation = false;
            this.ButtonStart.Location = new System.Drawing.Point(238, 66);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(64, 24);
            this.ButtonStart.TabIndex = 4;
            this.ButtonStart.Text = "开始升级";
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(8, 32);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(360, 16);
            this.ProgressBar.Step = 3;
            this.ProgressBar.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 8);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // LabelTips
            // 
            this.LabelTips.Location = new System.Drawing.Point(8, 72);
            this.LabelTips.Name = "LabelTips";
            this.LabelTips.Size = new System.Drawing.Size(220, 16);
            this.LabelTips.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "进行升级前请您首先确认已经连上互联网！";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(376, 93);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelTips);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "达元BS自动升级程序";
            this.TopMost = true;
            this.ResumeLayout(false);

        }
        #endregion

    }
}
