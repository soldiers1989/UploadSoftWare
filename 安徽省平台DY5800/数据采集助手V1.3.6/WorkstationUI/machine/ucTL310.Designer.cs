namespace WorkstationUI.machine
{
    partial class ucTL310
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
            this.btnReadAllData = new CCWin.SkinControl.SkinButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BtnRead = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2018, 3, 16, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2018, 3, 16, 0, 0, 0, 0);
            // 
            // btnReadAllData
            // 
            this.btnReadAllData.BackColor = System.Drawing.Color.Transparent;
            this.btnReadAllData.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnReadAllData.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.btnReadAllData.DownBack = null;
            this.btnReadAllData.Location = new System.Drawing.Point(605, 48);
            this.btnReadAllData.MouseBack = null;
            this.btnReadAllData.Name = "btnReadAllData";
            this.btnReadAllData.NormlBack = null;
            this.btnReadAllData.Size = new System.Drawing.Size(86, 23);
            this.btnReadAllData.TabIndex = 30;
            this.btnReadAllData.Text = "采集全部数据";
            this.btnReadAllData.UseVisualStyleBackColor = false;
            this.btnReadAllData.Click += new System.EventHandler(this.btnReadAllData_Click);
            // 
            // progressBar
            // 
            this.progressBar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.progressBar.Location = new System.Drawing.Point(584, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(317, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 31;
            this.progressBar.Visible = false;
            // 
            // BtnRead
            // 
            this.BtnRead.BackColor = System.Drawing.Color.Transparent;
            this.BtnRead.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnRead.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.BtnRead.DownBack = null;
            this.BtnRead.Enabled = false;
            this.BtnRead.Location = new System.Drawing.Point(550, 48);
            this.BtnRead.MouseBack = null;
            this.BtnRead.Name = "BtnRead";
            this.BtnRead.NormlBack = null;
            this.BtnRead.Size = new System.Drawing.Size(49, 23);
            this.BtnRead.TabIndex = 32;
            this.BtnRead.Text = "读取";
            this.BtnRead.UseVisualStyleBackColor = false;
            this.BtnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // ucTL310
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.BtnRead);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnReadAllData);
            this.Name = "ucTL310";
            this.Size = new System.Drawing.Size(1014, 568);
            this.Load += new System.EventHandler(this.ucTL310_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.BtnReadHis, 0);
            this.Controls.SetChildIndex(this.BtnClear, 0);
            this.Controls.SetChildIndex(this.DTPStart, 0);
            this.Controls.SetChildIndex(this.DTPEnd, 0);
            this.Controls.SetChildIndex(this.btnadd, 0);
            this.Controls.SetChildIndex(this.btnDatsave, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbCOMbox, 0);
            this.Controls.SetChildIndex(this.btnlinkcom, 0);
            this.Controls.SetChildIndex(this.LbTitle, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtlink, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.btnReadAllData, 0);
            this.Controls.SetChildIndex(this.progressBar, 0);
            this.Controls.SetChildIndex(this.BtnRead, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected CCWin.SkinControl.SkinButton btnReadAllData;
        private System.Windows.Forms.ProgressBar progressBar;
        protected CCWin.SkinControl.SkinButton BtnRead;
    }
}
