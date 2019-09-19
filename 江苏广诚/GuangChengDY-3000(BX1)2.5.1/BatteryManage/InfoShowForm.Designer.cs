namespace BatteryManage
{
    partial class InfoShowForm
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
            this.lb_BatteryContent = new System.Windows.Forms.Label();
            this.label_info = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_ClosedWindow = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_BatteryContent
            // 
            this.lb_BatteryContent.BackColor = System.Drawing.Color.Transparent;
            this.lb_BatteryContent.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_BatteryContent.ForeColor = System.Drawing.Color.White;
            this.lb_BatteryContent.Location = new System.Drawing.Point(12, 9);
            this.lb_BatteryContent.Name = "lb_BatteryContent";
            this.lb_BatteryContent.Size = new System.Drawing.Size(576, 55);
            this.lb_BatteryContent.TabIndex = 3;
            this.lb_BatteryContent.Text = "达元检测仪电池管理系统";
            this.lb_BatteryContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info
            // 
            this.label_info.BackColor = System.Drawing.Color.Transparent;
            this.label_info.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_info.ForeColor = System.Drawing.Color.White;
            this.label_info.Location = new System.Drawing.Point(12, 64);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(576, 55);
            this.label_info.TabIndex = 4;
            this.label_info.Text = "电池剩余电量已不足10%，为保证系统正常运行，现给出如下建议：";
            this.label_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(576, 55);
            this.label2.TabIndex = 5;
            this.label2.Text = "1、若您需要继续使用，请接上电源。";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(576, 55);
            this.label3.TabIndex = 6;
            this.label3.Text = "2、保存正在使用或操作的文档，关闭检测仪。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_ClosedWindow
            // 
            this.btn_ClosedWindow.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClosedWindow.ForeColor = System.Drawing.Color.Red;
            this.btn_ClosedWindow.Location = new System.Drawing.Point(125, 255);
            this.btn_ClosedWindow.Name = "btn_ClosedWindow";
            this.btn_ClosedWindow.Size = new System.Drawing.Size(85, 35);
            this.btn_ClosedWindow.TabIndex = 7;
            this.btn_ClosedWindow.Text = "立即关机";
            this.btn_ClosedWindow.UseVisualStyleBackColor = true;
            this.btn_ClosedWindow.Click += new System.EventHandler(this.btn_ClosedWindow_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.Location = new System.Drawing.Point(390, 255);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(85, 35);
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "继续使用";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // InfoShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_ClosedWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.lb_BatteryContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InfoShowForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoShowForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InfoShowForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_BatteryContent;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_ClosedWindow;
        private System.Windows.Forms.Button btn_Close;
    }
}