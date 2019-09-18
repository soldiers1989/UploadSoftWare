namespace WorkstationUI.function
{
    partial class frmServer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.label10 = new System.Windows.Forms.Label();
            this.labelClose = new System.Windows.Forms.Label();
            this.BtnCommunicate = new CCWin.SkinControl.SkinButton();
            this.label14 = new System.Windows.Forms.Label();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.Txt_User = new System.Windows.Forms.TextBox();
            this.Txt_PassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelchkstation = new System.Windows.Forms.Label();
            this.labelchkstationNum = new System.Windows.Forms.Label();
            this.txtDetectUnit = new System.Windows.Forms.TextBox();
            this.txtDetectUnitNo = new System.Windows.Forms.TextBox();
            this.labelChkunittype = new System.Windows.Forms.Label();
            this.labelchkusername = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.labeljigoumingcheng = new System.Windows.Forms.Label();
            this.txtOrganize = new System.Windows.Forms.TextBox();
            this.txtOrganizeNo = new System.Windows.Forms.TextBox();
            this.txtDetectType = new System.Windows.Forms.TextBox();
            this.btnChkUnitUp = new CCWin.SkinControl.SkinButton();
            this.labeljigoubianhao = new System.Windows.Forms.Label();
            this.labelMachineSerialModel = new System.Windows.Forms.Label();
            this.txtmodel = new System.Windows.Forms.TextBox();
            this.labelMachineCode = new System.Windows.Forms.Label();
            this.txtMachineSerial = new System.Windows.Forms.TextBox();
            this.btnRegiter = new CCWin.SkinControl.SkinButton();
            this.txtserials = new System.Windows.Forms.TextBox();
            this.labelIntrumentType = new System.Windows.Forms.Label();
            this.txtInstrumentType = new System.Windows.Forms.TextBox();
            this.labelIntrumentNum = new System.Windows.Forms.Label();
            this.txtIntrumentNum = new System.Windows.Forms.TextBox();
            this.labelmacAddr = new System.Windows.Forms.Label();
            this.txtMacAddr = new System.Windows.Forms.TextBox();
            this.labelInterfaceVer = new System.Windows.Forms.Label();
            this.txtInterfaceVer = new System.Windows.Forms.TextBox();
            this.btnOK = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(12, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "数据上传网络设置";
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(460, 10);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 53;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseEnter += new System.EventHandler(this.labelClose_MouseEnter);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // BtnCommunicate
            // 
            this.BtnCommunicate.BackColor = System.Drawing.Color.Transparent;
            this.BtnCommunicate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.BtnCommunicate.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnCommunicate.DownBack = null;
            this.BtnCommunicate.Location = new System.Drawing.Point(161, 262);
            this.BtnCommunicate.MouseBack = null;
            this.BtnCommunicate.Name = "BtnCommunicate";
            this.BtnCommunicate.NormlBack = null;
            this.BtnCommunicate.Size = new System.Drawing.Size(85, 28);
            this.BtnCommunicate.TabIndex = 109;
            this.BtnCommunicate.Text = "通信测试";
            this.BtnCommunicate.UseVisualStyleBackColor = false;
            this.BtnCommunicate.Click += new System.EventHandler(this.BtnCommunicate_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(1, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 21);
            this.label14.TabIndex = 110;
            this.label14.Text = "服务器地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Txt_Url
            // 
            this.Txt_Url.Location = new System.Drawing.Point(90, 43);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(365, 21);
            this.Txt_Url.TabIndex = 111;
            // 
            // Txt_User
            // 
            this.Txt_User.Location = new System.Drawing.Point(90, 76);
            this.Txt_User.Name = "Txt_User";
            this.Txt_User.Size = new System.Drawing.Size(129, 21);
            this.Txt_User.TabIndex = 112;
            // 
            // Txt_PassWord
            // 
            this.Txt_PassWord.Location = new System.Drawing.Point(311, 76);
            this.Txt_PassWord.Name = "Txt_PassWord";
            this.Txt_PassWord.PasswordChar = '*';
            this.Txt_PassWord.Size = new System.Drawing.Size(144, 21);
            this.Txt_PassWord.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 114;
            this.label1.Text = "用户名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(255, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 115;
            this.label2.Text = "密码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelchkstation
            // 
            this.labelchkstation.BackColor = System.Drawing.Color.Transparent;
            this.labelchkstation.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelchkstation.Location = new System.Drawing.Point(-1, 153);
            this.labelchkstation.Name = "labelchkstation";
            this.labelchkstation.Size = new System.Drawing.Size(86, 21);
            this.labelchkstation.TabIndex = 116;
            this.labelchkstation.Text = "检测站名称：";
            this.labelchkstation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelchkstationNum
            // 
            this.labelchkstationNum.BackColor = System.Drawing.Color.Transparent;
            this.labelchkstationNum.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelchkstationNum.Location = new System.Drawing.Point(221, 155);
            this.labelchkstationNum.Name = "labelchkstationNum";
            this.labelchkstationNum.Size = new System.Drawing.Size(89, 21);
            this.labelchkstationNum.TabIndex = 117;
            this.labelchkstationNum.Text = "检测站编号：";
            this.labelchkstationNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetectUnit
            // 
            this.txtDetectUnit.Location = new System.Drawing.Point(90, 153);
            this.txtDetectUnit.Name = "txtDetectUnit";
            this.txtDetectUnit.Size = new System.Drawing.Size(129, 21);
            this.txtDetectUnit.TabIndex = 118;
            // 
            // txtDetectUnitNo
            // 
            this.txtDetectUnitNo.Location = new System.Drawing.Point(311, 155);
            this.txtDetectUnitNo.Name = "txtDetectUnitNo";
            this.txtDetectUnitNo.Size = new System.Drawing.Size(144, 21);
            this.txtDetectUnitNo.TabIndex = 119;
            // 
            // labelChkunittype
            // 
            this.labelChkunittype.BackColor = System.Drawing.Color.Transparent;
            this.labelChkunittype.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelChkunittype.Location = new System.Drawing.Point(0, 190);
            this.labelChkunittype.Name = "labelChkunittype";
            this.labelChkunittype.Size = new System.Drawing.Size(86, 21);
            this.labelChkunittype.TabIndex = 120;
            this.labelChkunittype.Text = "检测站类型：";
            this.labelChkunittype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelchkusername
            // 
            this.labelchkusername.BackColor = System.Drawing.Color.Transparent;
            this.labelchkusername.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelchkusername.Location = new System.Drawing.Point(237, 191);
            this.labelchkusername.Name = "labelchkusername";
            this.labelchkusername.Size = new System.Drawing.Size(73, 21);
            this.labelchkusername.TabIndex = 122;
            this.labelchkusername.Text = "用户昵称：";
            this.labelchkusername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(313, 192);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(144, 21);
            this.txtUserName.TabIndex = 123;
            // 
            // labeljigoumingcheng
            // 
            this.labeljigoumingcheng.BackColor = System.Drawing.Color.Transparent;
            this.labeljigoumingcheng.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeljigoumingcheng.Location = new System.Drawing.Point(-4, 111);
            this.labeljigoumingcheng.Name = "labeljigoumingcheng";
            this.labeljigoumingcheng.Size = new System.Drawing.Size(90, 21);
            this.labeljigoumingcheng.TabIndex = 124;
            this.labeljigoumingcheng.Text = "机构名称：";
            this.labeljigoumingcheng.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrganize
            // 
            this.txtOrganize.Location = new System.Drawing.Point(90, 111);
            this.txtOrganize.Name = "txtOrganize";
            this.txtOrganize.Size = new System.Drawing.Size(127, 21);
            this.txtOrganize.TabIndex = 126;
            // 
            // txtOrganizeNo
            // 
            this.txtOrganizeNo.Location = new System.Drawing.Point(311, 115);
            this.txtOrganizeNo.Name = "txtOrganizeNo";
            this.txtOrganizeNo.Size = new System.Drawing.Size(144, 21);
            this.txtOrganizeNo.TabIndex = 127;
            // 
            // txtDetectType
            // 
            this.txtDetectType.Location = new System.Drawing.Point(90, 190);
            this.txtDetectType.Name = "txtDetectType";
            this.txtDetectType.Size = new System.Drawing.Size(129, 21);
            this.txtDetectType.TabIndex = 128;
            // 
            // btnChkUnitUp
            // 
            this.btnChkUnitUp.BackColor = System.Drawing.Color.Transparent;
            this.btnChkUnitUp.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.btnChkUnitUp.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnChkUnitUp.DownBack = null;
            this.btnChkUnitUp.Location = new System.Drawing.Point(42, 281);
            this.btnChkUnitUp.MouseBack = null;
            this.btnChkUnitUp.Name = "btnChkUnitUp";
            this.btnChkUnitUp.NormlBack = null;
            this.btnChkUnitUp.Size = new System.Drawing.Size(116, 28);
            this.btnChkUnitUp.TabIndex = 129;
            this.btnChkUnitUp.Text = "同步检测单位信息";
            this.btnChkUnitUp.UseVisualStyleBackColor = false;
            this.btnChkUnitUp.Visible = false;
            this.btnChkUnitUp.Click += new System.EventHandler(this.btnChkUnitUp_Click);
            // 
            // labeljigoubianhao
            // 
            this.labeljigoubianhao.BackColor = System.Drawing.Color.Transparent;
            this.labeljigoubianhao.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeljigoubianhao.Location = new System.Drawing.Point(234, 113);
            this.labeljigoubianhao.Name = "labeljigoubianhao";
            this.labeljigoubianhao.Size = new System.Drawing.Size(74, 21);
            this.labeljigoubianhao.TabIndex = 130;
            this.labeljigoubianhao.Text = "机构编号：";
            this.labeljigoubianhao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMachineSerialModel
            // 
            this.labelMachineSerialModel.BackColor = System.Drawing.Color.Transparent;
            this.labelMachineSerialModel.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMachineSerialModel.Location = new System.Drawing.Point(0, 222);
            this.labelMachineSerialModel.Name = "labelMachineSerialModel";
            this.labelMachineSerialModel.Size = new System.Drawing.Size(86, 21);
            this.labelMachineSerialModel.TabIndex = 131;
            this.labelMachineSerialModel.Text = "仪系列型号：";
            this.labelMachineSerialModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtmodel
            // 
            this.txtmodel.Location = new System.Drawing.Point(90, 221);
            this.txtmodel.Name = "txtmodel";
            this.txtmodel.Size = new System.Drawing.Size(129, 21);
            this.txtmodel.TabIndex = 132;
            // 
            // labelMachineCode
            // 
            this.labelMachineCode.BackColor = System.Drawing.Color.Transparent;
            this.labelMachineCode.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMachineCode.Location = new System.Drawing.Point(224, 224);
            this.labelMachineCode.Name = "labelMachineCode";
            this.labelMachineCode.Size = new System.Drawing.Size(86, 21);
            this.labelMachineCode.TabIndex = 133;
            this.labelMachineCode.Text = "仪器唯一码：";
            this.labelMachineCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMachineSerial
            // 
            this.txtMachineSerial.Location = new System.Drawing.Point(311, 224);
            this.txtMachineSerial.Name = "txtMachineSerial";
            this.txtMachineSerial.Size = new System.Drawing.Size(146, 21);
            this.txtMachineSerial.TabIndex = 134;
            // 
            // btnRegiter
            // 
            this.btnRegiter.BackColor = System.Drawing.Color.Transparent;
            this.btnRegiter.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.btnRegiter.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegiter.DownBack = null;
            this.btnRegiter.Location = new System.Drawing.Point(262, 262);
            this.btnRegiter.MouseBack = null;
            this.btnRegiter.Name = "btnRegiter";
            this.btnRegiter.NormlBack = null;
            this.btnRegiter.Size = new System.Drawing.Size(85, 28);
            this.btnRegiter.TabIndex = 135;
            this.btnRegiter.Text = "仪器注册";
            this.btnRegiter.UseVisualStyleBackColor = false;
            this.btnRegiter.Click += new System.EventHandler(this.btnRegiter_Click);
            // 
            // txtserials
            // 
            this.txtserials.Location = new System.Drawing.Point(311, 224);
            this.txtserials.Name = "txtserials";
            this.txtserials.Size = new System.Drawing.Size(146, 21);
            this.txtserials.TabIndex = 134;
            // 
            // labelIntrumentType
            // 
            this.labelIntrumentType.BackColor = System.Drawing.Color.Transparent;
            this.labelIntrumentType.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIntrumentType.Location = new System.Drawing.Point(11, 114);
            this.labelIntrumentType.Name = "labelIntrumentType";
            this.labelIntrumentType.Size = new System.Drawing.Size(75, 19);
            this.labelIntrumentType.TabIndex = 136;
            this.labelIntrumentType.Text = "仪器类型：";
            this.labelIntrumentType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInstrumentType
            // 
            this.txtInstrumentType.Location = new System.Drawing.Point(90, 115);
            this.txtInstrumentType.Name = "txtInstrumentType";
            this.txtInstrumentType.Size = new System.Drawing.Size(127, 21);
            this.txtInstrumentType.TabIndex = 137;
            // 
            // labelIntrumentNum
            // 
            this.labelIntrumentNum.BackColor = System.Drawing.Color.Transparent;
            this.labelIntrumentNum.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIntrumentNum.Location = new System.Drawing.Point(236, 117);
            this.labelIntrumentNum.Name = "labelIntrumentNum";
            this.labelIntrumentNum.Size = new System.Drawing.Size(74, 21);
            this.labelIntrumentNum.TabIndex = 138;
            this.labelIntrumentNum.Text = "仪器编号：";
            this.labelIntrumentNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIntrumentNum
            // 
            this.txtIntrumentNum.Location = new System.Drawing.Point(311, 117);
            this.txtIntrumentNum.Name = "txtIntrumentNum";
            this.txtIntrumentNum.Size = new System.Drawing.Size(144, 21);
            this.txtIntrumentNum.TabIndex = 139;
            // 
            // labelmacAddr
            // 
            this.labelmacAddr.BackColor = System.Drawing.Color.Transparent;
            this.labelmacAddr.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmacAddr.Location = new System.Drawing.Point(12, 157);
            this.labelmacAddr.Name = "labelmacAddr";
            this.labelmacAddr.Size = new System.Drawing.Size(72, 21);
            this.labelmacAddr.TabIndex = 140;
            this.labelmacAddr.Text = "MAC地址：";
            this.labelmacAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMacAddr
            // 
            this.txtMacAddr.Location = new System.Drawing.Point(90, 160);
            this.txtMacAddr.Name = "txtMacAddr";
            this.txtMacAddr.ReadOnly = true;
            this.txtMacAddr.Size = new System.Drawing.Size(129, 21);
            this.txtMacAddr.TabIndex = 141;
            // 
            // labelInterfaceVer
            // 
            this.labelInterfaceVer.BackColor = System.Drawing.Color.Transparent;
            this.labelInterfaceVer.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInterfaceVer.Location = new System.Drawing.Point(234, 160);
            this.labelInterfaceVer.Name = "labelInterfaceVer";
            this.labelInterfaceVer.Size = new System.Drawing.Size(74, 21);
            this.labelInterfaceVer.TabIndex = 142;
            this.labelInterfaceVer.Text = "接口版本：";
            this.labelInterfaceVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInterfaceVer
            // 
            this.txtInterfaceVer.Location = new System.Drawing.Point(311, 161);
            this.txtInterfaceVer.Name = "txtInterfaceVer";
            this.txtInterfaceVer.ReadOnly = true;
            this.txtInterfaceVer.Size = new System.Drawing.Size(144, 21);
            this.txtInterfaceVer.TabIndex = 143;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(255)))));
            this.btnOK.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnOK.DownBack = null;
            this.btnOK.Location = new System.Drawing.Point(206, 214);
            this.btnOK.MouseBack = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormlBack = null;
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 144;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(494, 296);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInterfaceVer);
            this.Controls.Add(this.labelInterfaceVer);
            this.Controls.Add(this.txtMacAddr);
            this.Controls.Add(this.labelmacAddr);
            this.Controls.Add(this.txtIntrumentNum);
            this.Controls.Add(this.labelIntrumentNum);
            this.Controls.Add(this.txtInstrumentType);
            this.Controls.Add(this.labelIntrumentType);
            this.Controls.Add(this.btnRegiter);
            this.Controls.Add(this.txtMachineSerial);
            this.Controls.Add(this.labelMachineCode);
            this.Controls.Add(this.txtmodel);
            this.Controls.Add(this.labelMachineSerialModel);
            this.Controls.Add(this.labeljigoubianhao);
            this.Controls.Add(this.btnChkUnitUp);
            this.Controls.Add(this.txtDetectType);
            this.Controls.Add(this.txtOrganizeNo);
            this.Controls.Add(this.txtOrganize);
            this.Controls.Add(this.labeljigoumingcheng);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.labelchkusername);
            this.Controls.Add(this.labelChkunittype);
            this.Controls.Add(this.txtDetectUnitNo);
            this.Controls.Add(this.txtDetectUnit);
            this.Controls.Add(this.labelchkstationNum);
            this.Controls.Add(this.labelchkstation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_PassWord);
            this.Controls.Add(this.Txt_User);
            this.Controls.Add(this.Txt_Url);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.BtnCommunicate);
            this.Controls.Add(this.labelClose);
            this.Controls.Add(this.label10);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmServer";
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelClose;
        private CCWin.SkinControl.SkinButton BtnCommunicate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.TextBox Txt_User;
        private System.Windows.Forms.TextBox Txt_PassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelchkstation;
        private System.Windows.Forms.Label labelchkstationNum;
        private System.Windows.Forms.TextBox txtDetectUnit;
        private System.Windows.Forms.TextBox txtDetectUnitNo;
        private System.Windows.Forms.Label labelChkunittype;
        private System.Windows.Forms.Label labelchkusername;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label labeljigoumingcheng;
        private System.Windows.Forms.TextBox txtOrganize;
        private System.Windows.Forms.TextBox txtOrganizeNo;
        private System.Windows.Forms.TextBox txtDetectType;
        private CCWin.SkinControl.SkinButton btnChkUnitUp;
        private System.Windows.Forms.Label labeljigoubianhao;
        private System.Windows.Forms.Label labelMachineSerialModel;
        private System.Windows.Forms.TextBox txtmodel;
        private System.Windows.Forms.Label labelMachineCode;
        private System.Windows.Forms.TextBox txtMachineSerial;
        private CCWin.SkinControl.SkinButton btnRegiter;
        private System.Windows.Forms.TextBox txtserials;
        private System.Windows.Forms.Label labelIntrumentType;
        private System.Windows.Forms.TextBox txtInstrumentType;
        private System.Windows.Forms.Label labelIntrumentNum;
        private System.Windows.Forms.TextBox txtIntrumentNum;
        private System.Windows.Forms.Label labelmacAddr;
        private System.Windows.Forms.TextBox txtMacAddr;
        private System.Windows.Forms.Label labelInterfaceVer;
        private System.Windows.Forms.TextBox txtInterfaceVer;
        private CCWin.SkinControl.SkinButton btnOK;
    }
}