namespace WorkstationUI.machine
{
    partial class ucTL300
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
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2017, 5, 22, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2017, 5, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(46, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "TL-300多通道农药残留快速测试仪";
            // 
            // ucTL300
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label3);
            this.Name = "ucTL300";
            this.Size = new System.Drawing.Size(1309, 585);
            this.Load += new System.EventHandler(this.ucTL300_Load);
            this.Controls.SetChildIndex(this.LbTitle, 0);
            this.Controls.SetChildIndex(this.cmbCOMbox, 0);
            this.Controls.SetChildIndex(this.btnlinkcom, 0);
            this.Controls.SetChildIndex(this.BtnReadHis, 0);
            this.Controls.SetChildIndex(this.BtnClear, 0);
            this.Controls.SetChildIndex(this.DTPStart, 0);
            this.Controls.SetChildIndex(this.DTPEnd, 0);
            this.Controls.SetChildIndex(this.btnadd, 0);
            this.Controls.SetChildIndex(this.btnDatsave, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
    }
}
