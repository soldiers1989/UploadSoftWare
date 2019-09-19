namespace WorkstationUI.machine
{
    partial class ucDY5600
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDY5600));
            this.label1 = new System.Windows.Forms.Label();
            this.skinCaptionPanel1 = new CCWin.SkinControl.SkinCaptionPanel();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.btnadd = new CCWin.SkinControl.SkinButton();
            this.DTPEnd = new System.Windows.Forms.DateTimePicker();
            this.DTPStart = new System.Windows.Forms.DateTimePicker();
            this.BtnClear = new CCWin.SkinControl.SkinButton();
            this.BtnReadHis = new CCWin.SkinControl.SkinButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.skinCaptionPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "DY-5600食品安全综合分析仪";
            // 
            // skinCaptionPanel1
            // 
            this.skinCaptionPanel1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.skinCaptionPanel1.Controls.Add(this.CheckDatas);
            this.skinCaptionPanel1.Location = new System.Drawing.Point(3, 102);
            this.skinCaptionPanel1.Name = "skinCaptionPanel1";
            this.skinCaptionPanel1.Size = new System.Drawing.Size(767, 484);
            this.skinCaptionPanel1.TabIndex = 41;
            this.skinCaptionPanel1.Text = "检测数据采集";
            // 
            // CheckDatas
            // 
            this.CheckDatas.AllowUserToAddRows = false;
            this.CheckDatas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.CheckDatas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.CheckDatas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckDatas.EnableHeadersVisualStyles = false;
            this.CheckDatas.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDatas.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CheckDatas.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDatas.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.Location = new System.Drawing.Point(2, 24);
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
            this.CheckDatas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CheckDatas.Size = new System.Drawing.Size(763, 458);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.Transparent;
            this.btnadd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnadd.DownBack = null;
            this.btnadd.Location = new System.Drawing.Point(543, 58);
            this.btnadd.MouseBack = null;
            this.btnadd.Name = "btnadd";
            this.btnadd.NormlBack = null;
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 73;
            this.btnadd.Text = "添加数据";
            this.btnadd.UseVisualStyleBackColor = false;
            // 
            // DTPEnd
            // 
            this.DTPEnd.Location = new System.Drawing.Point(205, 60);
            this.DTPEnd.Name = "DTPEnd";
            this.DTPEnd.Size = new System.Drawing.Size(109, 21);
            this.DTPEnd.TabIndex = 72;
            // 
            // DTPStart
            // 
            this.DTPStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DTPStart.Location = new System.Drawing.Point(62, 60);
            this.DTPStart.Name = "DTPStart";
            this.DTPStart.Size = new System.Drawing.Size(114, 21);
            this.DTPStart.TabIndex = 71;
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnClear.DownBack = null;
            this.BtnClear.Location = new System.Drawing.Point(416, 58);
            this.BtnClear.MouseBack = null;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.NormlBack = null;
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 70;
            this.BtnClear.Text = "清除";
            this.BtnClear.UseVisualStyleBackColor = false;
            // 
            // BtnReadHis
            // 
            this.BtnReadHis.BackColor = System.Drawing.Color.Transparent;
            this.BtnReadHis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnReadHis.DownBack = null;
            this.BtnReadHis.Location = new System.Drawing.Point(325, 58);
            this.BtnReadHis.MouseBack = null;
            this.BtnReadHis.Name = "BtnReadHis";
            this.BtnReadHis.NormlBack = null;
            this.BtnReadHis.Size = new System.Drawing.Size(75, 23);
            this.BtnReadHis.TabIndex = 69;
            this.BtnReadHis.Text = "读取";
            this.BtnReadHis.UseVisualStyleBackColor = false;
            this.BtnReadHis.Click += new System.EventHandler(this.BtnReadHis_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(182, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 68;
            this.label2.Text = "至";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(21, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 67;
            this.label3.Text = "日期:";
            // 
            // ucDY5600
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.DTPEnd);
            this.Controls.Add(this.DTPStart);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnReadHis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.skinCaptionPanel1);
            this.Controls.Add(this.label1);
            this.Name = "ucDY5600";
            this.Size = new System.Drawing.Size(864, 637);
            this.Load += new System.EventHandler(this.ucDY5600_Load);
            this.skinCaptionPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        protected CCWin.SkinControl.SkinCaptionPanel skinCaptionPanel1;
        protected CCWin.SkinControl.SkinDataGridView CheckDatas;
        protected CCWin.SkinControl.SkinButton btnadd;
        protected System.Windows.Forms.DateTimePicker DTPEnd;
        protected System.Windows.Forms.DateTimePicker DTPStart;
        protected CCWin.SkinControl.SkinButton BtnClear;
        protected CCWin.SkinControl.SkinButton BtnReadHis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
