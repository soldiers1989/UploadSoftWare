namespace FoodClient.Basic
{
    partial class frmNewInterface
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.chkboxSelect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWebServerice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtChkUnitID = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.checkBoxDouble = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(204, 188);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 33);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "检测单位测试";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // chkboxSelect
            // 
            this.chkboxSelect.AutoSize = true;
            this.chkboxSelect.Location = new System.Drawing.Point(289, 34);
            this.chkboxSelect.Name = "chkboxSelect";
            this.chkboxSelect.Size = new System.Drawing.Size(156, 16);
            this.chkboxSelect.TabIndex = 1;
            this.chkboxSelect.Text = "使用新接口上传检测数据";
            this.chkboxSelect.UseVisualStyleBackColor = true;
            this.chkboxSelect.CheckedChanged += new System.EventHandler(this.chkboxSelect_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器地址：";
            // 
            // txtWebServerice
            // 
            this.txtWebServerice.Location = new System.Drawing.Point(115, 56);
            this.txtWebServerice.Name = "txtWebServerice";
            this.txtWebServerice.Size = new System.Drawing.Size(353, 21);
            this.txtWebServerice.TabIndex = 3;
            this.txtWebServerice.Text = "http://ncadmin.ifesports.cn/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "检测单位ID：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "检测单位名称：";
            // 
            // txtChkUnitID
            // 
            this.txtChkUnitID.Location = new System.Drawing.Point(115, 98);
            this.txtChkUnitID.Name = "txtChkUnitID";
            this.txtChkUnitID.Size = new System.Drawing.Size(151, 21);
            this.txtChkUnitID.TabIndex = 7;
            this.txtChkUnitID.Text = "123456";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(348, 98);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(120, 21);
            this.txtPwd.TabIndex = 8;
            this.txtPwd.Text = "service";
            // 
            // txtUnitName
            // 
            this.txtUnitName.Location = new System.Drawing.Point(115, 135);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(151, 21);
            this.txtUnitName.TabIndex = 9;
            this.txtUnitName.Text = "达元";
            // 
            // checkBoxDouble
            // 
            this.checkBoxDouble.AutoSize = true;
            this.checkBoxDouble.Location = new System.Drawing.Point(36, 34);
            this.checkBoxDouble.Name = "checkBoxDouble";
            this.checkBoxDouble.Size = new System.Drawing.Size(84, 16);
            this.checkBoxDouble.TabIndex = 18;
            this.checkBoxDouble.Text = "双平台上传";
            this.checkBoxDouble.UseVisualStyleBackColor = true;
            this.checkBoxDouble.CheckedChanged += new System.EventHandler(this.checkBoxDouble_CheckedChanged);
            // 
            // frmNewInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 248);
            this.Controls.Add(this.checkBoxDouble);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtChkUnitID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWebServerice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkboxSelect);
            this.Controls.Add(this.btnConnect);
            this.Name = "frmNewInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNewInterface";
            this.Load += new System.EventHandler(this.frmNewInterface_Load);
            this.Controls.SetChildIndex(this.btnConnect, 0);
            this.Controls.SetChildIndex(this.chkboxSelect, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtWebServerice, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtChkUnitID, 0);
            this.Controls.SetChildIndex(this.txtPwd, 0);
            this.Controls.SetChildIndex(this.txtUnitName, 0);
            this.Controls.SetChildIndex(this.checkBoxDouble, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkboxSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWebServerice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtChkUnitID;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.CheckBox checkBoxDouble;
    }
}