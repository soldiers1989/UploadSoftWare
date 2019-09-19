namespace WorkstationUI.Basic
{
    partial class BasicContent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnDatsave = new CCWin.SkinControl.SkinButton();
            this.btnadd = new CCWin.SkinControl.SkinButton();
            this.DTPEnd = new System.Windows.Forms.DateTimePicker();
            this.DTPStart = new System.Windows.Forms.DateTimePicker();
            this.BtnClear = new CCWin.SkinControl.SkinButton();
            this.BtnReadHis = new CCWin.SkinControl.SkinButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCOMbox = new System.Windows.Forms.ComboBox();
            this.btnlinkcom = new CCWin.SkinControl.SkinButton();
            this.LbTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtlink = new System.Windows.Forms.TextBox();
            this.btnRefresh = new CCWin.SkinControl.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckDatas
            // 
            this.CheckDatas.AllowUserToAddRows = false;
            this.CheckDatas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.CheckDatas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CheckDatas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckDatas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.CheckDatas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CheckDatas.ColumnFont = null;
            this.CheckDatas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CheckDatas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CheckDatas.ColumnHeadersHeight = 25;
            this.CheckDatas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CheckDatas.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckDatas.DefaultCellStyle = dataGridViewCellStyle3;
            this.CheckDatas.EnableHeadersVisualStyles = false;
            this.CheckDatas.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDatas.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CheckDatas.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDatas.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.Location = new System.Drawing.Point(0, 80);
            this.CheckDatas.Name = "CheckDatas";
            this.CheckDatas.ReadOnly = true;
            this.CheckDatas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CheckDatas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.CheckDatas.RowTemplate.Height = 23;
            this.CheckDatas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CheckDatas.Size = new System.Drawing.Size(1024, 521);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.CheckDatas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CheckDatas_CellClick);
            this.CheckDatas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CheckDatas_CellMouseDoubleClick);
            this.CheckDatas.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.CheckDatas_ColumnWidthChanged);
            this.CheckDatas.CurrentCellChanged += new System.EventHandler(this.CheckDatas_CurrentCellChanged);
            this.CheckDatas.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CheckDatas_Scroll);
            this.CheckDatas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CheckDatas_KeyUp);
            // 
            // btnDatsave
            // 
            this.btnDatsave.BackColor = System.Drawing.Color.Transparent;
            this.btnDatsave.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDatsave.DownBack = null;
            this.btnDatsave.Location = new System.Drawing.Point(864, 49);
            this.btnDatsave.MouseBack = null;
            this.btnDatsave.Name = "btnDatsave";
            this.btnDatsave.NormlBack = null;
            this.btnDatsave.Size = new System.Drawing.Size(75, 23);
            this.btnDatsave.TabIndex = 22;
            this.btnDatsave.Text = "保存";
            this.btnDatsave.UseVisualStyleBackColor = false;
            this.btnDatsave.Click += new System.EventHandler(this.btnDatsave_Click);
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.Transparent;
            this.btnadd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnadd.DownBack = null;
            this.btnadd.Location = new System.Drawing.Point(959, 49);
            this.btnadd.MouseBack = null;
            this.btnadd.Name = "btnadd";
            this.btnadd.NormlBack = null;
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 21;
            this.btnadd.Text = "上传";
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // DTPEnd
            // 
            this.DTPEnd.Location = new System.Drawing.Point(567, 48);
            this.DTPEnd.Name = "DTPEnd";
            this.DTPEnd.Size = new System.Drawing.Size(109, 21);
            this.DTPEnd.TabIndex = 20;
            // 
            // DTPStart
            // 
            this.DTPStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DTPStart.Location = new System.Drawing.Point(432, 49);
            this.DTPStart.Name = "DTPStart";
            this.DTPStart.Size = new System.Drawing.Size(114, 21);
            this.DTPStart.TabIndex = 19;
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnClear.DownBack = null;
            this.BtnClear.Location = new System.Drawing.Point(773, 48);
            this.BtnClear.MouseBack = null;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.NormlBack = null;
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 18;
            this.BtnClear.Text = "清除数据";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnReadHis
            // 
            this.BtnReadHis.BackColor = System.Drawing.Color.Transparent;
            this.BtnReadHis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnReadHis.DownBack = null;
            this.BtnReadHis.Location = new System.Drawing.Point(682, 47);
            this.BtnReadHis.MouseBack = null;
            this.BtnReadHis.Name = "BtnReadHis";
            this.BtnReadHis.NormlBack = null;
            this.BtnReadHis.Size = new System.Drawing.Size(75, 23);
            this.BtnReadHis.TabIndex = 17;
            this.BtnReadHis.Text = "采集数据";
            this.BtnReadHis.UseVisualStyleBackColor = false;
            this.BtnReadHis.Click += new System.EventHandler(this.BtnReadHis_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(548, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "至";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(375, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "检测时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(11, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "端口号：";
            // 
            // cmbCOMbox
            // 
            this.cmbCOMbox.FormattingEnabled = true;
            this.cmbCOMbox.Location = new System.Drawing.Point(62, 50);
            this.cmbCOMbox.Name = "cmbCOMbox";
            this.cmbCOMbox.Size = new System.Drawing.Size(68, 20);
            this.cmbCOMbox.TabIndex = 24;
            // 
            // btnlinkcom
            // 
            this.btnlinkcom.BackColor = System.Drawing.Color.Transparent;
            this.btnlinkcom.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnlinkcom.DownBack = null;
            this.btnlinkcom.Location = new System.Drawing.Point(137, 49);
            this.btnlinkcom.MouseBack = null;
            this.btnlinkcom.Name = "btnlinkcom";
            this.btnlinkcom.NormlBack = null;
            this.btnlinkcom.Size = new System.Drawing.Size(75, 23);
            this.btnlinkcom.TabIndex = 25;
            this.btnlinkcom.Text = "连接设备";
            this.btnlinkcom.UseVisualStyleBackColor = false;
            this.btnlinkcom.Click += new System.EventHandler(this.btnlinkcom_Click);
            // 
            // LbTitle
            // 
            this.LbTitle.AutoSize = true;
            this.LbTitle.BackColor = System.Drawing.Color.Transparent;
            this.LbTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LbTitle.ForeColor = System.Drawing.Color.White;
            this.LbTitle.Location = new System.Drawing.Point(65, 11);
            this.LbTitle.Name = "LbTitle";
            this.LbTitle.Size = new System.Drawing.Size(53, 16);
            this.LbTitle.TabIndex = 26;
            this.LbTitle.Text = "title";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(268, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "状态：";
            // 
            // txtlink
            // 
            this.txtlink.Location = new System.Drawing.Point(304, 50);
            this.txtlink.Name = "txtlink";
            this.txtlink.ReadOnly = true;
            this.txtlink.Size = new System.Drawing.Size(68, 21);
            this.txtlink.TabIndex = 28;
            this.txtlink.Text = "未连接";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRefresh.DownBack = null;
            this.btnRefresh.Location = new System.Drawing.Point(221, 49);
            this.btnRefresh.MouseBack = null;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormlBack = null;
            this.btnRefresh.Size = new System.Drawing.Size(43, 23);
            this.btnRefresh.TabIndex = 29;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // BasicContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WorkstationUI.Properties.Resources.right;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtlink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LbTitle);
            this.Controls.Add(this.btnlinkcom);
            this.Controls.Add(this.cmbCOMbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CheckDatas);
            this.Controls.Add(this.btnDatsave);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.DTPEnd);
            this.Controls.Add(this.DTPStart);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnReadHis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "BasicContent";
            this.Size = new System.Drawing.Size(1027, 604);
            this.Load += new System.EventHandler(this.BasicContent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        public CCWin.SkinControl.SkinButton btnDatsave;
        protected CCWin.SkinControl.SkinButton btnadd;
        protected System.Windows.Forms.DateTimePicker DTPEnd;
        protected System.Windows.Forms.DateTimePicker DTPStart;
        protected CCWin.SkinControl.SkinButton BtnClear;
        protected CCWin.SkinControl.SkinButton BtnReadHis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public CCWin.SkinControl.SkinButton btnlinkcom;
        protected System.Windows.Forms.ComboBox cmbCOMbox;
        public CCWin.SkinControl.SkinDataGridView CheckDatas;
        public System.Windows.Forms.Label LbTitle;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox txtlink;
        public CCWin.SkinControl.SkinButton btnRefresh;
    }
}
