namespace WorkstationUI.function
{
    partial class FrmSearchSample
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchSample));
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.btnlSearch = new CCWin.SkinControl.SkinButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSampleName = new System.Windows.Forms.TextBox();
            this.btnSelect = new CCWin.SkinControl.SkinButton();
            this.labelClose = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.CheckDatas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.CheckDatas.Location = new System.Drawing.Point(2, 82);
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
            this.CheckDatas.Size = new System.Drawing.Size(638, 355);
            this.CheckDatas.TabIndex = 2;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.CheckDatas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CheckDatas_CellMouseDoubleClick);
            // 
            // btnlSearch
            // 
            this.btnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnlSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnlSearch.DownBack = null;
            this.btnlSearch.Location = new System.Drawing.Point(257, 51);
            this.btnlSearch.MouseBack = null;
            this.btnlSearch.Name = "btnlSearch";
            this.btnlSearch.NormlBack = null;
            this.btnlSearch.Size = new System.Drawing.Size(66, 23);
            this.btnlSearch.TabIndex = 26;
            this.btnlSearch.Text = "查  询";
            this.btnlSearch.UseVisualStyleBackColor = false;
            this.btnlSearch.Click += new System.EventHandler(this.btnlSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(16, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "样品名称：";
            // 
            // txtSampleName
            // 
            this.txtSampleName.Location = new System.Drawing.Point(85, 53);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.Size = new System.Drawing.Size(133, 21);
            this.txtSampleName.TabIndex = 28;
            this.txtSampleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSampleName_KeyDown);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSelect.DownBack = null;
            this.btnSelect.Location = new System.Drawing.Point(392, 51);
            this.btnSelect.MouseBack = null;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.NormlBack = null;
            this.btnSelect.Size = new System.Drawing.Size(66, 23);
            this.btnSelect.TabIndex = 29;
            this.btnSelect.Text = "选  择";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.BackColor = System.Drawing.Color.Transparent;
            this.labelClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelClose.ForeColor = System.Drawing.Color.Transparent;
            this.labelClose.Location = new System.Drawing.Point(607, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(25, 16);
            this.labelClose.TabIndex = 54;
            this.labelClose.Text = "×";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            this.labelClose.MouseEnter += new System.EventHandler(this.labelClose_MouseEnter);
            this.labelClose.MouseLeave += new System.EventHandler(this.labelClose_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "样品数据查询";
            // 
            // FrmSearchSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(643, 438);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelClose);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtSampleName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnlSearch);
            this.Controls.Add(this.CheckDatas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSearchSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSearchSample";
            this.Load += new System.EventHandler(this.FrmSearchSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CCWin.SkinControl.SkinDataGridView CheckDatas;
        public CCWin.SkinControl.SkinButton btnlSearch;
        protected System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSampleName;
        public CCWin.SkinControl.SkinButton btnSelect;
        private System.Windows.Forms.Label labelClose;
        private System.Windows.Forms.Label label1;
    }
}