namespace FoodClientTools
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EnvrinomentParamTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.MachineParamTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemParamTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ConectParamTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyRightTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.EnvrinomentParamtsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.machineParamtsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.systemParamtsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyRightParamtsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.conectParamtsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetTagName = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tsbDataMng = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMDataMng = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.windowsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(765, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip_ItemAdded);
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnvrinomentParamTSMI,
            this.MachineParamTSMI,
            this.SystemParamTSMI,
            this.ConectParamTSMI,
            this.CopyRightTSMI,
            this.TSMDataMng,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(59, 20);
            this.fileMenu.Text = "选项(&F)";
            // 
            // EnvrinomentParamTSMI
            // 
            this.EnvrinomentParamTSMI.ImageTransparentColor = System.Drawing.Color.Black;
            this.EnvrinomentParamTSMI.Name = "EnvrinomentParamTSMI";
            this.EnvrinomentParamTSMI.Size = new System.Drawing.Size(152, 22);
            this.EnvrinomentParamTSMI.Text = "配置环境参数";
            this.EnvrinomentParamTSMI.Click += new System.EventHandler(this.EnvirnomentForm_Click);
            // 
            // MachineParamTSMI
            // 
            this.MachineParamTSMI.ImageTransparentColor = System.Drawing.Color.Black;
            this.MachineParamTSMI.Name = "MachineParamTSMI";
            this.MachineParamTSMI.Size = new System.Drawing.Size(152, 22);
            this.MachineParamTSMI.Text = "配置仪器参数";
            this.MachineParamTSMI.Click += new System.EventHandler(this.MachineParamSet_Click);
            // 
            // SystemParamTSMI
            // 
            this.SystemParamTSMI.ImageTransparentColor = System.Drawing.Color.Black;
            this.SystemParamTSMI.Name = "SystemParamTSMI";
            this.SystemParamTSMI.Size = new System.Drawing.Size(152, 22);
            this.SystemParamTSMI.Text = "配置系统参数";
            this.SystemParamTSMI.Click += new System.EventHandler(this.SystemParamTSMI_Click);
            // 
            // ConectParamTSMI
            // 
            this.ConectParamTSMI.Name = "ConectParamTSMI";
            this.ConectParamTSMI.Size = new System.Drawing.Size(152, 22);
            this.ConectParamTSMI.Text = "配置联网参数";
            this.ConectParamTSMI.Click += new System.EventHandler(this.ConectParamSet_Click);
            // 
            // CopyRightTSMI
            // 
            this.CopyRightTSMI.ImageTransparentColor = System.Drawing.Color.Black;
            this.CopyRightTSMI.Name = "CopyRightTSMI";
            this.CopyRightTSMI.Size = new System.Drawing.Size(152, 22);
            this.CopyRightTSMI.Text = "配置版权信息";
            this.CopyRightTSMI.Click += new System.EventHandler(this.copyRightTSMI_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "退出(&X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(59, 20);
            this.windowsMenu.Text = "窗口(&W)";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cascadeToolStripMenuItem.Text = "层叠(&C)";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.tileVerticalToolStripMenuItem.Text = "垂直平铺(&V)";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.tileHorizontalToolStripMenuItem.Text = "水平平铺(&H)";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.closeAllToolStripMenuItem.Text = "全部关闭(&L)";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnvrinomentParamtsb,
            this.toolStripSeparator3,
            this.machineParamtsb,
            this.toolStripSeparator4,
            this.systemParamtsb,
            this.toolStripSeparator1,
            this.copyRightParamtsb,
            this.toolStripSeparator6,
            this.conectParamtsb,
            this.toolStripSeparator2,
            this.tsbSetTagName,
            this.toolStripSeparator7,
            this.tsbDataMng});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(765, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // EnvrinomentParamtsb
            // 
            this.EnvrinomentParamtsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EnvrinomentParamtsb.ImageTransparentColor = System.Drawing.Color.Black;
            this.EnvrinomentParamtsb.Name = "EnvrinomentParamtsb";
            this.EnvrinomentParamtsb.Size = new System.Drawing.Size(81, 22);
            this.EnvrinomentParamtsb.Text = "环境参数配置";
            this.EnvrinomentParamtsb.Click += new System.EventHandler(this.EnvirnomentForm_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // machineParamtsb
            // 
            this.machineParamtsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.machineParamtsb.ImageTransparentColor = System.Drawing.Color.Black;
            this.machineParamtsb.Name = "machineParamtsb";
            this.machineParamtsb.Size = new System.Drawing.Size(81, 22);
            this.machineParamtsb.Text = "仪器参数配置";
            this.machineParamtsb.Click += new System.EventHandler(this.MachineParamSet_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // systemParamtsb
            // 
            this.systemParamtsb.ImageTransparentColor = System.Drawing.Color.Black;
            this.systemParamtsb.Name = "systemParamtsb";
            this.systemParamtsb.Size = new System.Drawing.Size(81, 22);
            this.systemParamtsb.Text = "系统参数配置";
            this.systemParamtsb.Click += new System.EventHandler(this.SystemParamTSMI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // copyRightParamtsb
            // 
            this.copyRightParamtsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyRightParamtsb.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyRightParamtsb.Name = "copyRightParamtsb";
            this.copyRightParamtsb.Size = new System.Drawing.Size(81, 22);
            this.copyRightParamtsb.Text = "版本信息配置";
            this.copyRightParamtsb.Click += new System.EventHandler(this.copyRightTSMI_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // conectParamtsb
            // 
            this.conectParamtsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.conectParamtsb.ImageTransparentColor = System.Drawing.Color.Black;
            this.conectParamtsb.Name = "conectParamtsb";
            this.conectParamtsb.Size = new System.Drawing.Size(81, 22);
            this.conectParamtsb.Text = "联网参数配置";
            this.conectParamtsb.Click += new System.EventHandler(this.ConectParamSet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSetTagName
            // 
            this.tsbSetTagName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSetTagName.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetTagName.Image")));
            this.tsbSetTagName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetTagName.Name = "tsbSetTagName";
            this.tsbSetTagName.Size = new System.Drawing.Size(93, 22);
            this.tsbSetTagName.Text = "配置数据域标签";
            this.tsbSetTagName.Click += new System.EventHandler(this.tsbSetTagName_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 496);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(765, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel.Text = "状态";
            // 
            // tsbDataMng
            // 
            this.tsbDataMng.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDataMng.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataMng.Image")));
            this.tsbDataMng.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataMng.Name = "tsbDataMng";
            this.tsbDataMng.Size = new System.Drawing.Size(57, 22);
            this.tsbDataMng.Text = "数据整理";
            this.tsbDataMng.Click += new System.EventHandler(this.tsbDataMng_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // TSMDataMng
            // 
            this.TSMDataMng.Name = "TSMDataMng";
            this.TSMDataMng.Size = new System.Drawing.Size(152, 22);
            this.TSMDataMng.Text = "数据整理";
            this.TSMDataMng.Click += new System.EventHandler(this.tsbDataMng_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 518);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem EnvrinomentParamTSMI;
        private System.Windows.Forms.ToolStripMenuItem MachineParamTSMI;
        private System.Windows.Forms.ToolStripMenuItem SystemParamTSMI;
        private System.Windows.Forms.ToolStripMenuItem ConectParamTSMI;
        private System.Windows.Forms.ToolStripMenuItem CopyRightTSMI;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton EnvrinomentParamtsb;
        private System.Windows.Forms.ToolStripButton machineParamtsb;
        private System.Windows.Forms.ToolStripButton systemParamtsb;
        private System.Windows.Forms.ToolStripButton conectParamtsb;
        private System.Windows.Forms.ToolStripButton copyRightParamtsb;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbSetTagName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbDataMng;
        private System.Windows.Forms.ToolStripMenuItem TSMDataMng;
    }
}



