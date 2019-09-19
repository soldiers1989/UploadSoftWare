namespace BatteryManage
{
    partial class BatteryManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatteryManage));
            this.lb_BatteryContent = new System.Windows.Forms.Label();
            this.lb_BatteryType = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel_ElectricQuantity = new System.Windows.Forms.Panel();
            this.panel_Charging = new System.Windows.Forms.Panel();
            this.lb_version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_BatteryContent
            // 
            this.lb_BatteryContent.BackColor = System.Drawing.Color.Transparent;
            this.lb_BatteryContent.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_BatteryContent.ForeColor = System.Drawing.Color.White;
            this.lb_BatteryContent.Location = new System.Drawing.Point(155, 12);
            this.lb_BatteryContent.Name = "lb_BatteryContent";
            this.lb_BatteryContent.Size = new System.Drawing.Size(120, 55);
            this.lb_BatteryContent.TabIndex = 2;
            this.lb_BatteryContent.Text = "N/A";
            this.lb_BatteryContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_BatteryType
            // 
            this.lb_BatteryType.BackColor = System.Drawing.Color.Transparent;
            this.lb_BatteryType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_BatteryType.ForeColor = System.Drawing.Color.White;
            this.lb_BatteryType.Location = new System.Drawing.Point(276, 12);
            this.lb_BatteryType.Name = "lb_BatteryType";
            this.lb_BatteryType.Size = new System.Drawing.Size(140, 55);
            this.lb_BatteryType.TabIndex = 3;
            this.lb_BatteryType.Text = "正在获取电池信息";
            this.lb_BatteryType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "正在获取电池信息···";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
            // 
            // panel_ElectricQuantity
            // 
            this.panel_ElectricQuantity.BackColor = System.Drawing.Color.Transparent;
            this.panel_ElectricQuantity.BackgroundImage = global::BatteryManage.Properties.Resources.battery0;
            this.panel_ElectricQuantity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_ElectricQuantity.Location = new System.Drawing.Point(42, 12);
            this.panel_ElectricQuantity.Name = "panel_ElectricQuantity";
            this.panel_ElectricQuantity.Size = new System.Drawing.Size(115, 55);
            this.panel_ElectricQuantity.TabIndex = 5;
            // 
            // panel_Charging
            // 
            this.panel_Charging.BackColor = System.Drawing.Color.Transparent;
            this.panel_Charging.BackgroundImage = global::BatteryManage.Properties.Resources.plug;
            this.panel_Charging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Charging.Location = new System.Drawing.Point(2, 12);
            this.panel_Charging.Name = "panel_Charging";
            this.panel_Charging.Size = new System.Drawing.Size(48, 55);
            this.panel_Charging.TabIndex = 4;
            // 
            // lb_version
            // 
            this.lb_version.AutoSize = true;
            this.lb_version.ForeColor = System.Drawing.SystemColors.Control;
            this.lb_version.Location = new System.Drawing.Point(358, 66);
            this.lb_version.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_version.Name = "lb_version";
            this.lb_version.Size = new System.Drawing.Size(59, 12);
            this.lb_version.TabIndex = 6;
            this.lb_version.Text = "Ver 1.0.3";
            // 
            // BatteryManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(420, 80);
            this.Controls.Add(this.lb_version);
            this.Controls.Add(this.panel_ElectricQuantity);
            this.Controls.Add(this.lb_BatteryContent);
            this.Controls.Add(this.lb_BatteryType);
            this.Controls.Add(this.panel_Charging);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BatteryManage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BatteryManage";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatteryManage_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BatteryManage_FormClosed);
            this.Load += new System.EventHandler(this.BatteryManage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_BatteryContent;
        private System.Windows.Forms.Label lb_BatteryType;
        private System.Windows.Forms.Panel panel_Charging;
        private System.Windows.Forms.Panel panel_ElectricQuantity;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lb_version;
    }
}

