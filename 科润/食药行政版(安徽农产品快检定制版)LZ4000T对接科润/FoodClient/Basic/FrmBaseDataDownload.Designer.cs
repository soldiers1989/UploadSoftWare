namespace FoodClient
{
    partial class FrmBaseDataDownload
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
            this.btnAllDownload = new System.Windows.Forms.Button();
            this.btnFoodClass = new System.Windows.Forms.Button();
            this.btnCheckComTypeOpr = new System.Windows.Forms.Button();
            this.btnStandardType = new System.Windows.Forms.Button();
            this.btnCompanyKind = new System.Windows.Forms.Button();
            this.btnDistrict = new System.Windows.Forms.Button();
            this.btnProduceArea = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.tb_passWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_instrumentNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_instrument = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_interfaceVersion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_downLoadCompany = new System.Windows.Forms.Button();
            this.btn_downLoadDataDictionary = new System.Windows.Forms.Button();
            this.btn_downLoadAllDatas = new System.Windows.Forms.Button();
            this.tb_mac = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMachineID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_ChkUnit = new System.Windows.Forms.TextBox();
            this.txt_UnitID = new System.Windows.Forms.TextBox();
            this.cmbUpType = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAllDownload
            // 
            this.btnAllDownload.ForeColor = System.Drawing.Color.Black;
            this.btnAllDownload.Location = new System.Drawing.Point(103, 126);
            this.btnAllDownload.Name = "btnAllDownload";
            this.btnAllDownload.Size = new System.Drawing.Size(158, 23);
            this.btnAllDownload.TabIndex = 45;
            this.btnAllDownload.Text = "全部下载";
            this.btnAllDownload.UseVisualStyleBackColor = true;
            this.btnAllDownload.Click += new System.EventHandler(this.btnAllDownload_Click);
            // 
            // btnFoodClass
            // 
            this.btnFoodClass.Location = new System.Drawing.Point(10, 14);
            this.btnFoodClass.Name = "btnFoodClass";
            this.btnFoodClass.Size = new System.Drawing.Size(158, 23);
            this.btnFoodClass.TabIndex = 1;
            this.btnFoodClass.Text = "下载样品种类";
            this.btnFoodClass.UseVisualStyleBackColor = true;
            this.btnFoodClass.Click += new System.EventHandler(this.btnFoodClass_Click);
            // 
            // btnCheckComTypeOpr
            // 
            this.btnCheckComTypeOpr.Location = new System.Drawing.Point(10, 51);
            this.btnCheckComTypeOpr.Name = "btnCheckComTypeOpr";
            this.btnCheckComTypeOpr.Size = new System.Drawing.Size(158, 23);
            this.btnCheckComTypeOpr.TabIndex = 2;
            this.btnCheckComTypeOpr.Text = "下载检测点类型";
            this.btnCheckComTypeOpr.UseVisualStyleBackColor = true;
            this.btnCheckComTypeOpr.Click += new System.EventHandler(this.btnCheckComTypeOpr_Click);
            // 
            // btnStandardType
            // 
            this.btnStandardType.Location = new System.Drawing.Point(10, 88);
            this.btnStandardType.Name = "btnStandardType";
            this.btnStandardType.Size = new System.Drawing.Size(158, 23);
            this.btnStandardType.TabIndex = 3;
            this.btnStandardType.Text = "下载检测标准及检测项目";
            this.btnStandardType.UseVisualStyleBackColor = true;
            this.btnStandardType.Click += new System.EventHandler(this.btnStandardType_Click);
            // 
            // btnCompanyKind
            // 
            this.btnCompanyKind.Location = new System.Drawing.Point(198, 88);
            this.btnCompanyKind.Name = "btnCompanyKind";
            this.btnCompanyKind.Size = new System.Drawing.Size(158, 23);
            this.btnCompanyKind.TabIndex = 8;
            this.btnCompanyKind.Text = "下载单位类别及单位信息";
            this.btnCompanyKind.UseVisualStyleBackColor = true;
            this.btnCompanyKind.Click += new System.EventHandler(this.btnCompanyKind_Click);
            // 
            // btnDistrict
            // 
            this.btnDistrict.Location = new System.Drawing.Point(198, 14);
            this.btnDistrict.Name = "btnDistrict";
            this.btnDistrict.Size = new System.Drawing.Size(158, 23);
            this.btnDistrict.TabIndex = 6;
            this.btnDistrict.Text = "下载行政机构";
            this.btnDistrict.UseVisualStyleBackColor = true;
            this.btnDistrict.Click += new System.EventHandler(this.btnDistrict_Click);
            // 
            // btnProduceArea
            // 
            this.btnProduceArea.Location = new System.Drawing.Point(198, 51);
            this.btnProduceArea.Name = "btnProduceArea";
            this.btnProduceArea.Size = new System.Drawing.Size(158, 23);
            this.btnProduceArea.TabIndex = 7;
            this.btnProduceArea.Text = "下载产品产地";
            this.btnProduceArea.UseVisualStyleBackColor = true;
            this.btnProduceArea.Click += new System.EventHandler(this.btnProduceArea_Click);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(34, 182);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 12);
            this.lblTip.TabIndex = 46;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCompanyKind);
            this.panel2.Controls.Add(this.btnDistrict);
            this.panel2.Controls.Add(this.btnStandardType);
            this.panel2.Controls.Add(this.btnCheckComTypeOpr);
            this.panel2.Controls.Add(this.btnAllDownload);
            this.panel2.Controls.Add(this.btnFoodClass);
            this.panel2.Controls.Add(this.btnProduceArea);
            this.panel2.Location = new System.Drawing.Point(588, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 52);
            this.panel2.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "服务器地址：";
            // 
            // tb_Url
            // 
            this.tb_Url.Location = new System.Drawing.Point(95, 38);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(271, 21);
            this.tb_Url.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "用户名：";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(89, 155);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(120, 21);
            this.tb_userName.TabIndex = 51;
            // 
            // tb_passWord
            // 
            this.tb_passWord.Location = new System.Drawing.Point(304, 158);
            this.tb_passWord.Name = "tb_passWord";
            this.tb_passWord.Size = new System.Drawing.Size(120, 21);
            this.tb_passWord.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "密码：";
            // 
            // tb_instrumentNo
            // 
            this.tb_instrumentNo.Location = new System.Drawing.Point(304, 231);
            this.tb_instrumentNo.Name = "tb_instrumentNo";
            this.tb_instrumentNo.Size = new System.Drawing.Size(120, 21);
            this.tb_instrumentNo.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 56;
            this.label4.Text = "检测站编号：";
            // 
            // tb_instrument
            // 
            this.tb_instrument.Location = new System.Drawing.Point(89, 231);
            this.tb_instrument.Name = "tb_instrument";
            this.tb_instrument.Size = new System.Drawing.Size(120, 21);
            this.tb_instrument.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 54;
            this.label5.Text = "检测单位：";
            // 
            // tb_interfaceVersion
            // 
            this.tb_interfaceVersion.Location = new System.Drawing.Point(304, 179);
            this.tb_interfaceVersion.Name = "tb_interfaceVersion";
            this.tb_interfaceVersion.Size = new System.Drawing.Size(120, 21);
            this.tb_interfaceVersion.TabIndex = 61;
            this.tb_interfaceVersion.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(221, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 60;
            this.label6.Text = "接口版本号：";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 58;
            this.label7.Text = "MAC地址：";
            this.label7.Visible = false;
            // 
            // btn_downLoadCompany
            // 
            this.btn_downLoadCompany.Enabled = false;
            this.btn_downLoadCompany.Location = new System.Drawing.Point(36, 202);
            this.btn_downLoadCompany.Name = "btn_downLoadCompany";
            this.btn_downLoadCompany.Size = new System.Drawing.Size(95, 23);
            this.btn_downLoadCompany.TabIndex = 62;
            this.btn_downLoadCompany.Text = "被检企业下载";
            this.btn_downLoadCompany.UseVisualStyleBackColor = true;
            this.btn_downLoadCompany.Visible = false;
            this.btn_downLoadCompany.Click += new System.EventHandler(this.btn_downLoadCompany_Click);
            // 
            // btn_downLoadDataDictionary
            // 
            this.btn_downLoadDataDictionary.Enabled = false;
            this.btn_downLoadDataDictionary.Location = new System.Drawing.Point(173, 202);
            this.btn_downLoadDataDictionary.Name = "btn_downLoadDataDictionary";
            this.btn_downLoadDataDictionary.Size = new System.Drawing.Size(95, 23);
            this.btn_downLoadDataDictionary.TabIndex = 63;
            this.btn_downLoadDataDictionary.Text = "数据字典下载";
            this.btn_downLoadDataDictionary.UseVisualStyleBackColor = true;
            this.btn_downLoadDataDictionary.Visible = false;
            this.btn_downLoadDataDictionary.Click += new System.EventHandler(this.btn_downLoadDataDictionary_Click);
            // 
            // btn_downLoadAllDatas
            // 
            this.btn_downLoadAllDatas.Enabled = false;
            this.btn_downLoadAllDatas.Location = new System.Drawing.Point(309, 202);
            this.btn_downLoadAllDatas.Name = "btn_downLoadAllDatas";
            this.btn_downLoadAllDatas.Size = new System.Drawing.Size(95, 23);
            this.btn_downLoadAllDatas.TabIndex = 64;
            this.btn_downLoadAllDatas.Text = "全部数据下载";
            this.btn_downLoadAllDatas.UseVisualStyleBackColor = true;
            this.btn_downLoadAllDatas.Visible = false;
            this.btn_downLoadAllDatas.Click += new System.EventHandler(this.btn_downLoadAllDatas_Click);
            // 
            // tb_mac
            // 
            this.tb_mac.FormattingEnabled = true;
            this.tb_mac.Location = new System.Drawing.Point(88, 179);
            this.tb_mac.Name = "tb_mac";
            this.tb_mac.Size = new System.Drawing.Size(121, 20);
            this.tb_mac.TabIndex = 65;
            this.tb_mac.Visible = false;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(382, 36);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(47, 23);
            this.btn_ok.TabIndex = 66;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 67;
            this.label8.Text = "上传类别：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(257, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 69;
            this.label9.Text = "设备ID：";
            // 
            // txtMachineID
            // 
            this.txtMachineID.Location = new System.Drawing.Point(309, 112);
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.Size = new System.Drawing.Size(120, 21);
            this.txtMachineID.TabIndex = 70;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 71;
            this.label10.Text = "仪器名称：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(235, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 72;
            this.label11.Text = "仪器编号：";
            // 
            // txt_ChkUnit
            // 
            this.txt_ChkUnit.Location = new System.Drawing.Point(95, 77);
            this.txt_ChkUnit.Name = "txt_ChkUnit";
            this.txt_ChkUnit.Size = new System.Drawing.Size(120, 21);
            this.txt_ChkUnit.TabIndex = 73;
            // 
            // txt_UnitID
            // 
            this.txt_UnitID.Location = new System.Drawing.Point(309, 77);
            this.txt_UnitID.Name = "txt_UnitID";
            this.txt_UnitID.Size = new System.Drawing.Size(120, 21);
            this.txt_UnitID.TabIndex = 74;
            // 
            // cmbUpType
            // 
            this.cmbUpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpType.FormattingEnabled = true;
            this.cmbUpType.Items.AddRange(new object[] {
            "基地上传",
            "检测中心"});
            this.cmbUpType.Location = new System.Drawing.Point(95, 112);
            this.cmbUpType.Name = "cmbUpType";
            this.cmbUpType.Size = new System.Drawing.Size(120, 20);
            this.cmbUpType.TabIndex = 75;
            // 
            // FrmBaseDataDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(453, 154);
            this.Controls.Add(this.cmbUpType);
            this.Controls.Add(this.txt_UnitID);
            this.Controls.Add(this.txt_ChkUnit);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMachineID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.tb_mac);
            this.Controls.Add(this.btn_downLoadAllDatas);
            this.Controls.Add(this.btn_downLoadDataDictionary);
            this.Controls.Add(this.btn_downLoadCompany);
            this.Controls.Add(this.tb_interfaceVersion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_instrumentNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_instrument);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_passWord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_userName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaseDataDownload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础数据同步";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBaseDataDownload_FormClosing);
            this.Load += new System.EventHandler(this.FrmBaseDataDownload_Load);
            this.Controls.SetChildIndex(this.lblTip, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tb_Url, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tb_userName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tb_passWord, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.tb_instrument, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tb_instrumentNo, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.tb_interfaceVersion, 0);
            this.Controls.SetChildIndex(this.btn_downLoadCompany, 0);
            this.Controls.SetChildIndex(this.btn_downLoadDataDictionary, 0);
            this.Controls.SetChildIndex(this.btn_downLoadAllDatas, 0);
            this.Controls.SetChildIndex(this.tb_mac, 0);
            this.Controls.SetChildIndex(this.btn_ok, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtMachineID, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.txt_ChkUnit, 0);
            this.Controls.SetChildIndex(this.txt_UnitID, 0);
            this.Controls.SetChildIndex(this.cmbUpType, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAllDownload;
        private System.Windows.Forms.Button btnFoodClass;
        private System.Windows.Forms.Button btnCheckComTypeOpr;
        private System.Windows.Forms.Button btnStandardType;
        private System.Windows.Forms.Button btnCompanyKind;
        private System.Windows.Forms.Button btnDistrict;
		private System.Windows.Forms.Button btnProduceArea;
		private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.TextBox tb_passWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_instrumentNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_instrument;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_interfaceVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_downLoadCompany;
        private System.Windows.Forms.Button btn_downLoadDataDictionary;
        private System.Windows.Forms.Button btn_downLoadAllDatas;
        private System.Windows.Forms.ComboBox tb_mac;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMachineID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_ChkUnit;
        private System.Windows.Forms.TextBox txt_UnitID;
        private System.Windows.Forms.ComboBox cmbUpType;
    }
}