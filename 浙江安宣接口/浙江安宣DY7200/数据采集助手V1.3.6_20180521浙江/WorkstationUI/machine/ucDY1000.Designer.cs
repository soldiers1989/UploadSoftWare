namespace WorkstationUI.machine
{
    partial class ucDY1000
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
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2017, 5, 6, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2017, 5, 6, 0, 0, 0, 0);
            // 
            // LbTitle
            // 
            this.LbTitle.Size = new System.Drawing.Size(0, 16);
            this.LbTitle.Text = "";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.Transparent;
            this.labelTitle.Location = new System.Drawing.Point(65, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(224, 16);
            this.labelTitle.TabIndex = 15;
            this.labelTitle.Text = "LZ-3000农药残留快速测试仪";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(571, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "方式";
            this.label5.Visible = false;
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "非试纸法",
            "金标卡法",
            "干化学法"});
            this.cmbMethod.Location = new System.Drawing.Point(606, 49);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(70, 20);
            this.cmbMethod.TabIndex = 31;
            this.cmbMethod.Visible = false;
            // 
            // ucDY1000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelTitle);
            this.Name = "ucDY1000";
            this.Size = new System.Drawing.Size(1003, 436);
            this.Load += new System.EventHandler(this.ucDY1000_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtlink, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.LbTitle, 0);
            this.Controls.SetChildIndex(this.cmbCOMbox, 0);
            this.Controls.SetChildIndex(this.btnlinkcom, 0);
            this.Controls.SetChildIndex(this.BtnReadHis, 0);
            this.Controls.SetChildIndex(this.BtnClear, 0);
            this.Controls.SetChildIndex(this.DTPStart, 0);
            this.Controls.SetChildIndex(this.DTPEnd, 0);
            this.Controls.SetChildIndex(this.btnadd, 0);
            this.Controls.SetChildIndex(this.btnDatsave, 0);
            this.Controls.SetChildIndex(this.labelTitle, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbMethod, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMethod;

    }
}
