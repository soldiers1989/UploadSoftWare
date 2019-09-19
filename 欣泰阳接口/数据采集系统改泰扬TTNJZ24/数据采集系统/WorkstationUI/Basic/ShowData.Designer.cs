namespace WorkstationUI.Basic
{
    partial class ShowData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowData));
            this.skinCaptionPanel1 = new CCWin.SkinControl.SkinCaptionPanel();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.DTPStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPEnd = new System.Windows.Forms.DateTimePicker();
            this.BtnReadHis = new CCWin.SkinControl.SkinButton();
            this.BtnClear = new CCWin.SkinControl.SkinButton();
            this.btnadd = new CCWin.SkinControl.SkinButton();
            this.btnSave = new CCWin.SkinControl.SkinButton();
            this.skinCaptionPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            this.SuspendLayout();
            // 
            // skinCaptionPanel1
            // 
            this.skinCaptionPanel1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.skinCaptionPanel1.Controls.Add(this.CheckDatas);
            this.skinCaptionPanel1.Location = new System.Drawing.Point(3, 39);
            this.skinCaptionPanel1.Name = "skinCaptionPanel1";
            this.skinCaptionPanel1.Size = new System.Drawing.Size(763, 482);
            this.skinCaptionPanel1.TabIndex = 3;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CheckDatas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CheckDatas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CheckDatas.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckDatas.DefaultCellStyle = dataGridViewCellStyle3;
            this.CheckDatas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckDatas.EnableHeadersVisualStyles = false;
            this.CheckDatas.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CheckDatas.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDatas.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.Location = new System.Drawing.Point(2, 24);
            this.CheckDatas.Name = "CheckDatas";
            this.CheckDatas.ReadOnly = true;
            this.CheckDatas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CheckDatas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.CheckDatas.RowTemplate.Height = 23;
            this.CheckDatas.Size = new System.Drawing.Size(759, 456);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(18, 552);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "日期:";
            // 
            // DTPStart
            // 
            this.DTPStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DTPStart.Location = new System.Drawing.Point(59, 546);
            this.DTPStart.Name = "DTPStart";
            this.DTPStart.Size = new System.Drawing.Size(114, 21);
            this.DTPStart.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(189, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "至";
            // 
            // DTPEnd
            // 
            this.DTPEnd.Location = new System.Drawing.Point(212, 546);
            this.DTPEnd.Name = "DTPEnd";
            this.DTPEnd.Size = new System.Drawing.Size(109, 21);
            this.DTPEnd.TabIndex = 12;
            // 
            // BtnReadHis
            // 
            this.BtnReadHis.BackColor = System.Drawing.Color.Transparent;
            this.BtnReadHis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnReadHis.DownBack = null;
            this.BtnReadHis.Location = new System.Drawing.Point(371, 547);
            this.BtnReadHis.MouseBack = null;
            this.BtnReadHis.Name = "BtnReadHis";
            this.BtnReadHis.NormlBack = null;
            this.BtnReadHis.Size = new System.Drawing.Size(75, 23);
            this.BtnReadHis.TabIndex = 13;
            this.BtnReadHis.Text = "读取";
            this.BtnReadHis.UseVisualStyleBackColor = false;
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnClear.DownBack = null;
            this.BtnClear.Location = new System.Drawing.Point(472, 547);
            this.BtnClear.MouseBack = null;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.NormlBack = null;
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 14;
            this.BtnClear.Text = "清除";
            this.BtnClear.UseVisualStyleBackColor = false;
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.Transparent;
            this.btnadd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnadd.DownBack = null;
            this.btnadd.Location = new System.Drawing.Point(577, 547);
            this.btnadd.MouseBack = null;
            this.btnadd.Name = "btnadd";
            this.btnadd.NormlBack = null;
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 15;
            this.btnadd.Text = "添加数据";
            this.btnadd.UseVisualStyleBackColor = false;
            //this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSave.DownBack = null;
            this.btnSave.Location = new System.Drawing.Point(679, 547);
            this.btnSave.MouseBack = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormlBack = null;
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存数据";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // ShowData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnReadHis);
            this.Controls.Add(this.DTPEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DTPStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skinCaptionPanel1);
            this.DoubleBuffered = true;
            this.Name = "ShowData";
            this.Size = new System.Drawing.Size(769, 589);
            this.Load += new System.EventHandler(this.ShowData_Load);
            this.skinCaptionPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected CCWin.SkinControl.SkinCaptionPanel skinCaptionPanel1;
        protected CCWin.SkinControl.SkinDataGridView CheckDatas;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.DateTimePicker DTPStart;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.DateTimePicker DTPEnd;
        protected CCWin.SkinControl.SkinButton BtnReadHis;
        protected CCWin.SkinControl.SkinButton BtnClear;
        protected CCWin.SkinControl.SkinButton btnadd;
        protected CCWin.SkinControl.SkinButton btnSave;
    }
}
