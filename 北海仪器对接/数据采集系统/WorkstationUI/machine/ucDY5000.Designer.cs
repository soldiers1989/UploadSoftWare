﻿namespace WorkstationUI.machine
{
    partial class ucDY5000
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
            this.SuspendLayout();
            // 
            // DTPEnd
            // 
            this.DTPEnd.Value = new System.DateTime(2017, 6, 26, 0, 0, 0, 0);
            // 
            // DTPStart
            // 
            this.DTPStart.Location = new System.Drawing.Point(436, 50);
            this.DTPStart.Value = new System.DateTime(2017, 6, 26, 0, 0, 0, 0);
            // 
            // LbTitle
            // 
            this.LbTitle.Location = new System.Drawing.Point(65, 10);
            // 
            // ucDY5000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDY5000";
            this.Size = new System.Drawing.Size(1017, 509);
            this.Load += new System.EventHandler(this.ucDY5000_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
