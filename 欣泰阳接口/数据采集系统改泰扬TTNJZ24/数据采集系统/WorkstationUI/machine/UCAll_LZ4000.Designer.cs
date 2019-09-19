namespace WorkstationUI.machine
{
    partial class UCAll_LZ4000
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
            this.labelTile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2017, 4, 19, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2017, 4, 19, 0, 0, 0, 0);
            // 
            // labelTile
            // 
            this.labelTile.AutoSize = true;
            this.labelTile.BackColor = System.Drawing.Color.Transparent;
            this.labelTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTile.ForeColor = System.Drawing.Color.White;
            this.labelTile.Location = new System.Drawing.Point(55, 11);
            this.labelTile.Name = "labelTile";
            this.labelTile.Size = new System.Drawing.Size(232, 16);
            this.labelTile.TabIndex = 13;
            this.labelTile.Text = "通用LZ4000农药残留快速检测";
            // 
            // UCAll_LZ4000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labelTile);
            this.Name = "UCAll_LZ4000";
            this.Size = new System.Drawing.Size(1309, 585);
            this.Load += new System.EventHandler(this.UCAll_LZ4000_Load);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtlink, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.LbTitle, 0);
            this.Controls.SetChildIndex(this.cmbCOMbox, 0);
            this.Controls.SetChildIndex(this.btnlinkcom, 0);
            this.Controls.SetChildIndex(this.btnadd, 0);
            this.Controls.SetChildIndex(this.btnDatsave, 0);
            this.Controls.SetChildIndex(this.BtnReadHis, 0);
            this.Controls.SetChildIndex(this.BtnClear, 0);
            this.Controls.SetChildIndex(this.DTPStart, 0);
            this.Controls.SetChildIndex(this.DTPEnd, 0);
            this.Controls.SetChildIndex(this.labelTile, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTile;
    }
}
