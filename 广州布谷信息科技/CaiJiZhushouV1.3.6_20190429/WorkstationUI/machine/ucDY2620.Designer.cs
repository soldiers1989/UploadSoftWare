namespace WorkstationUI.machine
{
    partial class ucDY2620
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
            this.btnLastData = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2018, 7, 17, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Value = new System.DateTime(2018, 7, 17, 0, 0, 0, 0);
            // 
            // btnLastData
            // 
            this.btnLastData.BackColor = System.Drawing.Color.Transparent;
            this.btnLastData.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnLastData.DownBack = null;
            this.btnLastData.Location = new System.Drawing.Point(540, 47);
            this.btnLastData.MouseBack = null;
            this.btnLastData.Name = "btnLastData";
            this.btnLastData.NormlBack = null;
            this.btnLastData.Size = new System.Drawing.Size(100, 23);
            this.btnLastData.TabIndex = 70;
            this.btnLastData.Text = "采集上一次数据";
            this.btnLastData.UseVisualStyleBackColor = false;
            this.btnLastData.Click += new System.EventHandler(this.btnLastData_Click);
            // 
            // ucDY2620
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLastData);
            this.Name = "ucDY2620";
            this.Size = new System.Drawing.Size(1031, 585);
            this.Load += new System.EventHandler(this.ucDY2620_Load);
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
            this.Controls.SetChildIndex(this.btnLastData, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected CCWin.SkinControl.SkinButton btnLastData;
    }
}
