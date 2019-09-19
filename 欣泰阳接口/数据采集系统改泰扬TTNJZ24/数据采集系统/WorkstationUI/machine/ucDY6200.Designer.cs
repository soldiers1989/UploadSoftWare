namespace WorkstationUI.machine
{
    partial class ucDY6200
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2017, 6, 7, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2017, 6, 7, 0, 0, 0, 0);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.Transparent;
            this.labelTitle.Location = new System.Drawing.Point(56, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(190, 16);
            this.labelTitle.TabIndex = 16;
            this.labelTitle.Text = "DY-6200干式试纸分析仪";
            // 
            // ucDY6200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labelTitle);
            this.Name = "ucDY6200";
            this.Size = new System.Drawing.Size(1035, 585);
            this.Load += new System.EventHandler(this.ucDY6200_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbCOMbox, 0);
            this.Controls.SetChildIndex(this.btnlinkcom, 0);
            this.Controls.SetChildIndex(this.LbTitle, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtlink, 0);
            this.Controls.SetChildIndex(this.BtnReadHis, 0);
            this.Controls.SetChildIndex(this.BtnClear, 0);
            this.Controls.SetChildIndex(this.DTPStart, 0);
            this.Controls.SetChildIndex(this.DTPEnd, 0);
            this.Controls.SetChildIndex(this.btnadd, 0);
            this.Controls.SetChildIndex(this.btnDatsave, 0);
            this.Controls.SetChildIndex(this.labelTitle, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelTitle;
    }
}
