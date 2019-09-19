namespace WorkstationUI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.PnlDataCentre = new CCWin.SkinControl.SkinPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.PanelSysSet = new CCWin.SkinControl.SkinPanel();
            this.PnLeft = new CCWin.SkinControl.SkinPanel();
            this.PnlContent = new System.Windows.Forms.Panel();
            this.Mainpanel = new System.Windows.Forms.Panel();
            this.PnlUser = new CCWin.SkinControl.SkinPanel();
            this.labeluser = new System.Windows.Forms.Label();
            this.PnlTime = new CCWin.SkinControl.SkinPanel();
            this.labelTime = new System.Windows.Forms.Label();
            this.panelProject = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.PnlCollect = new CCWin.SkinControl.SkinPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBoxCollect = new System.Windows.Forms.PictureBox();
            this.picBoxData = new System.Windows.Forms.PictureBox();
            this.picBoxSysSet = new System.Windows.Forms.PictureBox();
            this.picboxMain = new System.Windows.Forms.PictureBox();
            this.labelmin = new System.Windows.Forms.Label();
            this.labelclose = new System.Windows.Forms.Label();
            this.pnlShop = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.picboxShop = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelBig = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PnlDataCentre.SuspendLayout();
            this.PanelSysSet.SuspendLayout();
            this.PnlUser.SuspendLayout();
            this.PnlTime.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.PnlCollect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCollect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSysSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMain)).BeginInit();
            this.pnlShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxShop)).BeginInit();
            this.SuspendLayout();
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPanel1.BackgroundImage")));
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(21, 27);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(179, 62);
            this.skinPanel1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据中心";
            // 
            // PnlDataCentre
            // 
            this.PnlDataCentre.BackColor = System.Drawing.Color.Transparent;
            this.PnlDataCentre.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PnlDataCentre.BackgroundImage")));
            this.PnlDataCentre.Controls.Add(this.label3);
            this.PnlDataCentre.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PnlDataCentre.DownBack = null;
            this.PnlDataCentre.Location = new System.Drawing.Point(499, 12);
            this.PnlDataCentre.MouseBack = null;
            this.PnlDataCentre.Name = "PnlDataCentre";
            this.PnlDataCentre.NormlBack = null;
            this.PnlDataCentre.Size = new System.Drawing.Size(94, 92);
            this.PnlDataCentre.TabIndex = 18;
            this.PnlDataCentre.MouseClick += new System.Windows.Forms.MouseEventHandler(this.skinPanel4_MouseClick);
            this.PnlDataCentre.MouseEnter += new System.EventHandler(this.skinPanel4_MouseEnter);
            this.PnlDataCentre.MouseLeave += new System.EventHandler(this.skinPanel4_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "系统设置";
            // 
            // PanelSysSet
            // 
            this.PanelSysSet.BackColor = System.Drawing.Color.Transparent;
            this.PanelSysSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelSysSet.BackgroundImage")));
            this.PanelSysSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PanelSysSet.Controls.Add(this.label4);
            this.PanelSysSet.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PanelSysSet.DownBack = null;
            this.PanelSysSet.Location = new System.Drawing.Point(623, 11);
            this.PanelSysSet.MouseBack = null;
            this.PanelSysSet.Name = "PanelSysSet";
            this.PanelSysSet.NormlBack = null;
            this.PanelSysSet.Size = new System.Drawing.Size(93, 92);
            this.PanelSysSet.TabIndex = 19;
            this.PanelSysSet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelSysSet_MouseClick);
            this.PanelSysSet.MouseEnter += new System.EventHandler(this.PanelSysSet_MouseEnter);
            this.PanelSysSet.MouseLeave += new System.EventHandler(this.PanelSysSet_MouseLeave);
            // 
            // PnLeft
            // 
            this.PnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PnLeft.BackColor = System.Drawing.Color.Transparent;
            this.PnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnLeft.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PnLeft.DownBack = null;
            this.PnLeft.Location = new System.Drawing.Point(11, 117);
            this.PnLeft.MouseBack = null;
            this.PnLeft.Name = "PnLeft";
            this.PnLeft.NormlBack = null;
            this.PnLeft.Size = new System.Drawing.Size(219, 602);
            this.PnLeft.TabIndex = 21;
            // 
            // PnlContent
            // 
            this.PnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlContent.BackColor = System.Drawing.Color.Transparent;
            this.PnlContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PnlContent.Location = new System.Drawing.Point(236, 117);
            this.PnlContent.Name = "PnlContent";
            this.PnlContent.Size = new System.Drawing.Size(1119, 602);
            this.PnlContent.TabIndex = 22;
            // 
            // Mainpanel
            // 
            this.Mainpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mainpanel.BackColor = System.Drawing.Color.Transparent;
            this.Mainpanel.Location = new System.Drawing.Point(11, 117);
            this.Mainpanel.Name = "Mainpanel";
            this.Mainpanel.Size = new System.Drawing.Size(1345, 606);
            this.Mainpanel.TabIndex = 30;
            // 
            // PnlUser
            // 
            this.PnlUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlUser.BackColor = System.Drawing.Color.Transparent;
            this.PnlUser.Controls.Add(this.labeluser);
            this.PnlUser.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PnlUser.DownBack = null;
            this.PnlUser.Location = new System.Drawing.Point(1185, 47);
            this.PnlUser.MouseBack = null;
            this.PnlUser.Name = "PnlUser";
            this.PnlUser.NormlBack = null;
            this.PnlUser.Size = new System.Drawing.Size(179, 23);
            this.PnlUser.TabIndex = 23;
            // 
            // labeluser
            // 
            this.labeluser.AutoSize = true;
            this.labeluser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeluser.ForeColor = System.Drawing.Color.White;
            this.labeluser.Location = new System.Drawing.Point(3, 4);
            this.labeluser.Name = "labeluser";
            this.labeluser.Size = new System.Drawing.Size(138, 16);
            this.labeluser.TabIndex = 5;
            this.labeluser.Text = "当前用户：admin";
            // 
            // PnlTime
            // 
            this.PnlTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlTime.BackColor = System.Drawing.Color.Transparent;
            this.PnlTime.Controls.Add(this.labelTime);
            this.PnlTime.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PnlTime.DownBack = null;
            this.PnlTime.Location = new System.Drawing.Point(1186, 80);
            this.PnlTime.MouseBack = null;
            this.PnlTime.Name = "PnlTime";
            this.PnlTime.NormlBack = null;
            this.PnlTime.Size = new System.Drawing.Size(179, 23);
            this.PnlTime.TabIndex = 24;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(3, 3);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(93, 16);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "当前时间：";
            // 
            // panelProject
            // 
            this.panelProject.BackColor = System.Drawing.Color.White;
            this.panelProject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelProject.BackgroundImage")));
            this.panelProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelProject.Location = new System.Drawing.Point(11, 156);
            this.panelProject.Name = "panelProject";
            this.panelProject.Size = new System.Drawing.Size(219, 552);
            this.panelProject.TabIndex = 0;
            this.panelProject.Visible = false;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMain.BackgroundImage")));
            this.panelMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Location = new System.Drawing.Point(239, 10);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(107, 92);
            this.panelMain.TabIndex = 26;
            this.panelMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseClick);
            this.panelMain.MouseEnter += new System.EventHandler(this.panelMain_MouseEnter);
            this.panelMain.MouseLeave += new System.EventHandler(this.panelMain_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "首页";
            // 
            // PnlCollect
            // 
            this.PnlCollect.BackColor = System.Drawing.Color.Transparent;
            this.PnlCollect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PnlCollect.BackgroundImage")));
            this.PnlCollect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PnlCollect.Controls.Add(this.label1);
            this.PnlCollect.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.PnlCollect.DownBack = null;
            this.PnlCollect.Location = new System.Drawing.Point(369, 11);
            this.PnlCollect.MouseBack = null;
            this.PnlCollect.Name = "PnlCollect";
            this.PnlCollect.NormlBack = null;
            this.PnlCollect.Size = new System.Drawing.Size(99, 92);
            this.PnlCollect.TabIndex = 16;
            this.PnlCollect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.skinPanel2_MouseClick_1);
            this.PnlCollect.MouseEnter += new System.EventHandler(this.skinPanel2_MouseEnter);
            this.PnlCollect.MouseLeave += new System.EventHandler(this.skinPanel2_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据采集";
            // 
            // picBoxCollect
            // 
            this.picBoxCollect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxCollect.BackgroundImage")));
            this.picBoxCollect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxCollect.Location = new System.Drawing.Point(367, 7);
            this.picBoxCollect.Name = "picBoxCollect";
            this.picBoxCollect.Size = new System.Drawing.Size(104, 99);
            this.picBoxCollect.TabIndex = 28;
            this.picBoxCollect.TabStop = false;
            // 
            // picBoxData
            // 
            this.picBoxData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxData.BackgroundImage")));
            this.picBoxData.Location = new System.Drawing.Point(495, 8);
            this.picBoxData.Name = "picBoxData";
            this.picBoxData.Size = new System.Drawing.Size(102, 99);
            this.picBoxData.TabIndex = 0;
            this.picBoxData.TabStop = false;
            this.picBoxData.Visible = false;
            // 
            // picBoxSysSet
            // 
            this.picBoxSysSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxSysSet.BackgroundImage")));
            this.picBoxSysSet.Location = new System.Drawing.Point(621, 7);
            this.picBoxSysSet.Name = "picBoxSysSet";
            this.picBoxSysSet.Size = new System.Drawing.Size(98, 99);
            this.picBoxSysSet.TabIndex = 0;
            this.picBoxSysSet.TabStop = false;
            this.picBoxSysSet.Visible = false;
            // 
            // picboxMain
            // 
            this.picboxMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picboxMain.BackgroundImage")));
            this.picboxMain.Location = new System.Drawing.Point(236, 6);
            this.picboxMain.Name = "picboxMain";
            this.picboxMain.Size = new System.Drawing.Size(114, 99);
            this.picboxMain.TabIndex = 29;
            this.picboxMain.TabStop = false;
            // 
            // labelmin
            // 
            this.labelmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelmin.AutoSize = true;
            this.labelmin.BackColor = System.Drawing.Color.Transparent;
            this.labelmin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmin.ForeColor = System.Drawing.Color.White;
            this.labelmin.Location = new System.Drawing.Point(1304, 8);
            this.labelmin.Name = "labelmin";
            this.labelmin.Size = new System.Drawing.Size(17, 16);
            this.labelmin.TabIndex = 31;
            this.labelmin.Text = "-";
            this.toolTip1.SetToolTip(this.labelmin, "最小化");
            this.labelmin.Click += new System.EventHandler(this.labelmin_Click_1);
            this.labelmin.MouseEnter += new System.EventHandler(this.labelmin_MouseEnter);
            this.labelmin.MouseLeave += new System.EventHandler(this.labelmin_MouseLeave);
            // 
            // labelclose
            // 
            this.labelclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelclose.AutoSize = true;
            this.labelclose.BackColor = System.Drawing.Color.Transparent;
            this.labelclose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelclose.ForeColor = System.Drawing.Color.White;
            this.labelclose.Location = new System.Drawing.Point(1339, 8);
            this.labelclose.Name = "labelclose";
            this.labelclose.Size = new System.Drawing.Size(25, 16);
            this.labelclose.TabIndex = 32;
            this.labelclose.Text = "×";
            this.toolTip1.SetToolTip(this.labelclose, "关闭");
            this.labelclose.Click += new System.EventHandler(this.labelclose_Click);
            this.labelclose.MouseEnter += new System.EventHandler(this.labelclose_MouseEnter);
            this.labelclose.MouseLeave += new System.EventHandler(this.labelclose_MouseLeave);
            // 
            // pnlShop
            // 
            this.pnlShop.BackColor = System.Drawing.Color.Transparent;
            this.pnlShop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlShop.BackgroundImage")));
            this.pnlShop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlShop.Controls.Add(this.label6);
            this.pnlShop.Location = new System.Drawing.Point(744, 12);
            this.pnlShop.Name = "pnlShop";
            this.pnlShop.Size = new System.Drawing.Size(91, 90);
            this.pnlShop.TabIndex = 33;
            this.pnlShop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlShop_MouseClick);
            this.pnlShop.MouseEnter += new System.EventHandler(this.pnlShop_MouseEnter);
            this.pnlShop.MouseLeave += new System.EventHandler(this.pnlShop_MouseLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(25, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "商城";
            // 
            // picboxShop
            // 
            this.picboxShop.BackColor = System.Drawing.Color.Transparent;
            this.picboxShop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picboxShop.BackgroundImage")));
            this.picboxShop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picboxShop.Location = new System.Drawing.Point(742, 7);
            this.picboxShop.Name = "picboxShop";
            this.picboxShop.Size = new System.Drawing.Size(95, 99);
            this.picboxShop.TabIndex = 36;
            this.picboxShop.TabStop = false;
            this.picboxShop.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelBig
            // 
            this.labelBig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBig.AutoSize = true;
            this.labelBig.BackColor = System.Drawing.Color.Transparent;
            this.labelBig.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBig.ForeColor = System.Drawing.Color.Transparent;
            this.labelBig.Location = new System.Drawing.Point(1265, 9);
            this.labelBig.Name = "labelBig";
            this.labelBig.Size = new System.Drawing.Size(17, 12);
            this.labelBig.TabIndex = 37;
            this.labelBig.Text = "□";
            this.labelBig.Visible = false;
            this.labelBig.Click += new System.EventHandler(this.labelBig_Click);
            this.labelBig.MouseEnter += new System.EventHandler(this.labelBig_MouseEnter);
            this.labelBig.MouseLeave += new System.EventHandler(this.labelBig_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 728);
            this.Controls.Add(this.Mainpanel);
            this.Controls.Add(this.labelBig);
            this.Controls.Add(this.pnlShop);
            this.Controls.Add(this.picboxShop);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.picboxMain);
            this.Controls.Add(this.labelclose);
            this.Controls.Add(this.labelmin);
            this.Controls.Add(this.PanelSysSet);
            this.Controls.Add(this.picBoxSysSet);
            this.Controls.Add(this.PnlDataCentre);
            this.Controls.Add(this.picBoxData);
            this.Controls.Add(this.PnlCollect);
            this.Controls.Add(this.picBoxCollect);
            this.Controls.Add(this.panelProject);
            this.Controls.Add(this.PnlTime);
            this.Controls.Add(this.PnlUser);
            this.Controls.Add(this.PnlContent);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.PnLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "食安科技数据采集系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.PnlDataCentre.ResumeLayout(false);
            this.PnlDataCentre.PerformLayout();
            this.PanelSysSet.ResumeLayout(false);
            this.PanelSysSet.PerformLayout();
            this.PnlUser.ResumeLayout(false);
            this.PnlUser.PerformLayout();
            this.PnlTime.ResumeLayout(false);
            this.PnlTime.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.PnlCollect.ResumeLayout(false);
            this.PnlCollect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCollect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSysSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMain)).EndInit();
            this.pnlShop.ResumeLayout(false);
            this.pnlShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxShop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinPanel skinPanel1;
        private System.Windows.Forms.Label label3;
        private CCWin.SkinControl.SkinPanel PnlDataCentre;
        private System.Windows.Forms.Label label4;
        private CCWin.SkinControl.SkinPanel PanelSysSet;
        private CCWin.SkinControl.SkinPanel PnlUser;
        private System.Windows.Forms.Label labeluser;
        private CCWin.SkinControl.SkinPanel PnlTime;
        private System.Windows.Forms.Label labelTime;
        public CCWin.SkinControl.SkinPanel PnLeft;
        public System.Windows.Forms.Panel PnlContent;
        private System.Windows.Forms.Panel panelProject;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private CCWin.SkinControl.SkinPanel PnlCollect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBoxCollect;
        private System.Windows.Forms.PictureBox picBoxData;
        private System.Windows.Forms.PictureBox picBoxSysSet;
        private System.Windows.Forms.PictureBox picboxMain;
        public System.Windows.Forms.Panel Mainpanel;
        private System.Windows.Forms.Label labelmin;
        private System.Windows.Forms.Label labelclose;
        private System.Windows.Forms.Panel pnlShop;
        private System.Windows.Forms.PictureBox picboxShop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelBig;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}

