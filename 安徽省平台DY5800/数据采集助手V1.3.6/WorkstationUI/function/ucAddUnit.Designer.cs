namespace WorkstationUI.function
{
    partial class ucAddUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAddUnit));
            this.LbTitle = new System.Windows.Forms.Label();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.btnrepair = new CCWin.SkinControl.SkinButton();
            this.btnclear = new CCWin.SkinControl.SkinButton();
            this.btnadd = new CCWin.SkinControl.SkinButton();
            this.btnrefresh = new CCWin.SkinControl.SkinButton();
            this.btnDownUnit = new CCWin.SkinControl.SkinButton();
            this.btnDownTestData = new CCWin.SkinControl.SkinButton();
            this.btnDelete = new CCWin.SkinControl.SkinButton();
            this.BtnDownSupervis = new CCWin.SkinControl.SkinButton();
            this.BtnReSupervis = new CCWin.SkinControl.SkinButton();
            this.BtnDeletesupervis = new CCWin.SkinControl.SkinButton();
            this.btnDownCompany = new CCWin.SkinControl.SkinButton();
            this.btnItem = new CCWin.SkinControl.SkinButton();
            this.btnAllData = new CCWin.SkinControl.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            this.SuspendLayout();
            // 
            // LbTitle
            // 
            this.LbTitle.AutoSize = true;
            this.LbTitle.BackColor = System.Drawing.Color.Transparent;
            this.LbTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LbTitle.ForeColor = System.Drawing.Color.White;
            this.LbTitle.Location = new System.Drawing.Point(25, 11);
            this.LbTitle.Name = "LbTitle";
            this.LbTitle.Size = new System.Drawing.Size(85, 16);
            this.LbTitle.TabIndex = 3;
            this.LbTitle.Text = "单位/企业";
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
            this.CheckDatas.Location = new System.Drawing.Point(1, 77);
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
            this.CheckDatas.Size = new System.Drawing.Size(773, 520);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.CheckDatas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CheckDatas_CellClick);
            this.CheckDatas.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.CheckDatas_CellPainting);
            this.CheckDatas.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CheckDatas_Scroll);
            // 
            // btnrepair
            // 
            this.btnrepair.BackColor = System.Drawing.Color.Transparent;
            this.btnrepair.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnrepair.DownBack = null;
            this.btnrepair.Location = new System.Drawing.Point(153, 48);
            this.btnrepair.MouseBack = null;
            this.btnrepair.Name = "btnrepair";
            this.btnrepair.NormlBack = null;
            this.btnrepair.Size = new System.Drawing.Size(75, 23);
            this.btnrepair.TabIndex = 68;
            this.btnrepair.Text = "修改";
            this.btnrepair.UseVisualStyleBackColor = false;
            this.btnrepair.Visible = false;
            this.btnrepair.Click += new System.EventHandler(this.btnrepair_Click);
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.Transparent;
            this.btnclear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnclear.DownBack = null;
            this.btnclear.Location = new System.Drawing.Point(282, 48);
            this.btnclear.MouseBack = null;
            this.btnclear.Name = "btnclear";
            this.btnclear.NormlBack = null;
            this.btnclear.Size = new System.Drawing.Size(75, 23);
            this.btnclear.TabIndex = 67;
            this.btnclear.Text = "删除";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Visible = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.Transparent;
            this.btnadd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnadd.DownBack = null;
            this.btnadd.Location = new System.Drawing.Point(28, 48);
            this.btnadd.MouseBack = null;
            this.btnadd.Name = "btnadd";
            this.btnadd.NormlBack = null;
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 66;
            this.btnadd.Text = "新增";
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Visible = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnrefresh.DownBack = null;
            this.btnrefresh.Location = new System.Drawing.Point(425, 48);
            this.btnrefresh.MouseBack = null;
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.NormlBack = null;
            this.btnrefresh.Size = new System.Drawing.Size(75, 23);
            this.btnrefresh.TabIndex = 69;
            this.btnrefresh.Text = "刷新";
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Visible = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnDownUnit
            // 
            this.btnDownUnit.BackColor = System.Drawing.Color.Transparent;
            this.btnDownUnit.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDownUnit.DownBack = null;
            this.btnDownUnit.Location = new System.Drawing.Point(123, 11);
            this.btnDownUnit.MouseBack = null;
            this.btnDownUnit.Name = "btnDownUnit";
            this.btnDownUnit.NormlBack = null;
            this.btnDownUnit.Size = new System.Drawing.Size(92, 23);
            this.btnDownUnit.TabIndex = 70;
            this.btnDownUnit.Text = "被检单位下载";
            this.btnDownUnit.UseVisualStyleBackColor = false;
            this.btnDownUnit.Visible = false;
            this.btnDownUnit.Click += new System.EventHandler(this.btnDownUnit_Click);
            // 
            // btnDownTestData
            // 
            this.btnDownTestData.BackColor = System.Drawing.Color.Transparent;
            this.btnDownTestData.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDownTestData.DownBack = null;
            this.btnDownTestData.Location = new System.Drawing.Point(230, 11);
            this.btnDownTestData.MouseBack = null;
            this.btnDownTestData.Name = "btnDownTestData";
            this.btnDownTestData.NormlBack = null;
            this.btnDownTestData.Size = new System.Drawing.Size(88, 23);
            this.btnDownTestData.TabIndex = 71;
            this.btnDownTestData.Text = "下载待检信息";
            this.btnDownTestData.UseVisualStyleBackColor = false;
            this.btnDownTestData.Visible = false;
            this.btnDownTestData.Click += new System.EventHandler(this.btnDownTestData_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDelete.DownBack = null;
            this.btnDelete.Location = new System.Drawing.Point(345, 11);
            this.btnDelete.MouseBack = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormlBack = null;
            this.btnDelete.Size = new System.Drawing.Size(69, 23);
            this.btnDelete.TabIndex = 72;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BtnDownSupervis
            // 
            this.BtnDownSupervis.BackColor = System.Drawing.Color.Transparent;
            this.BtnDownSupervis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnDownSupervis.DownBack = null;
            this.BtnDownSupervis.Location = new System.Drawing.Point(506, 10);
            this.BtnDownSupervis.MouseBack = null;
            this.BtnDownSupervis.Name = "BtnDownSupervis";
            this.BtnDownSupervis.NormlBack = null;
            this.BtnDownSupervis.Size = new System.Drawing.Size(88, 23);
            this.BtnDownSupervis.TabIndex = 73;
            this.BtnDownSupervis.Text = "监管对象下载";
            this.BtnDownSupervis.UseVisualStyleBackColor = false;
            this.BtnDownSupervis.Visible = false;
            this.BtnDownSupervis.Click += new System.EventHandler(this.BtnDownSupervis_Click);
            // 
            // BtnReSupervis
            // 
            this.BtnReSupervis.BackColor = System.Drawing.Color.Transparent;
            this.BtnReSupervis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnReSupervis.DownBack = null;
            this.BtnReSupervis.Location = new System.Drawing.Point(610, 11);
            this.BtnReSupervis.MouseBack = null;
            this.BtnReSupervis.Name = "BtnReSupervis";
            this.BtnReSupervis.NormlBack = null;
            this.BtnReSupervis.Size = new System.Drawing.Size(88, 23);
            this.BtnReSupervis.TabIndex = 74;
            this.BtnReSupervis.Text = "重置监管对象";
            this.BtnReSupervis.UseVisualStyleBackColor = false;
            this.BtnReSupervis.Visible = false;
            this.BtnReSupervis.Click += new System.EventHandler(this.BtnReSupervis_Click);
            // 
            // BtnDeletesupervis
            // 
            this.BtnDeletesupervis.BackColor = System.Drawing.Color.Transparent;
            this.BtnDeletesupervis.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnDeletesupervis.DownBack = null;
            this.BtnDeletesupervis.Location = new System.Drawing.Point(716, 11);
            this.BtnDeletesupervis.MouseBack = null;
            this.BtnDeletesupervis.Name = "BtnDeletesupervis";
            this.BtnDeletesupervis.NormlBack = null;
            this.BtnDeletesupervis.Size = new System.Drawing.Size(88, 23);
            this.BtnDeletesupervis.TabIndex = 75;
            this.BtnDeletesupervis.Text = "删除监管对象";
            this.BtnDeletesupervis.UseVisualStyleBackColor = false;
            this.BtnDeletesupervis.Visible = false;
            this.BtnDeletesupervis.Click += new System.EventHandler(this.BtnDeletesupervis_Click);
            // 
            // btnDownCompany
            // 
            this.btnDownCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnDownCompany.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDownCompany.DownBack = null;
            this.btnDownCompany.Location = new System.Drawing.Point(28, 48);
            this.btnDownCompany.MouseBack = null;
            this.btnDownCompany.Name = "btnDownCompany";
            this.btnDownCompany.NormlBack = null;
            this.btnDownCompany.Size = new System.Drawing.Size(97, 23);
            this.btnDownCompany.TabIndex = 76;
            this.btnDownCompany.Text = "被检企业下载";
            this.btnDownCompany.UseVisualStyleBackColor = false;
            this.btnDownCompany.Click += new System.EventHandler(this.btnDownCompany_Click);
            // 
            // btnItem
            // 
            this.btnItem.BackColor = System.Drawing.Color.Transparent;
            this.btnItem.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnItem.DownBack = null;
            this.btnItem.Location = new System.Drawing.Point(153, 48);
            this.btnItem.MouseBack = null;
            this.btnItem.Name = "btnItem";
            this.btnItem.NormlBack = null;
            this.btnItem.Size = new System.Drawing.Size(97, 23);
            this.btnItem.TabIndex = 77;
            this.btnItem.Text = "数据字典下载";
            this.btnItem.UseVisualStyleBackColor = false;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnAllData
            // 
            this.btnAllData.BackColor = System.Drawing.Color.Transparent;
            this.btnAllData.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnAllData.DownBack = null;
            this.btnAllData.Location = new System.Drawing.Point(282, 48);
            this.btnAllData.MouseBack = null;
            this.btnAllData.Name = "btnAllData";
            this.btnAllData.NormlBack = null;
            this.btnAllData.Size = new System.Drawing.Size(97, 23);
            this.btnAllData.TabIndex = 78;
            this.btnAllData.Text = "全部数据下载";
            this.btnAllData.UseVisualStyleBackColor = false;
            this.btnAllData.Click += new System.EventHandler(this.btnAllData_Click);
            // 
            // ucAddUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnAllData);
            this.Controls.Add(this.btnItem);
            this.Controls.Add(this.btnDownCompany);
            this.Controls.Add(this.BtnDeletesupervis);
            this.Controls.Add(this.BtnReSupervis);
            this.Controls.Add(this.BtnDownSupervis);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDownTestData);
            this.Controls.Add(this.btnDownUnit);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.btnrepair);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.CheckDatas);
            this.Controls.Add(this.LbTitle);
            this.DoubleBuffered = true;
            this.Name = "ucAddUnit";
            this.Size = new System.Drawing.Size(814, 617);
            this.Load += new System.EventHandler(this.ucAddUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbTitle;
        protected CCWin.SkinControl.SkinDataGridView CheckDatas;
        protected CCWin.SkinControl.SkinButton btnrepair;
        protected CCWin.SkinControl.SkinButton btnclear;
        protected CCWin.SkinControl.SkinButton btnadd;
        protected CCWin.SkinControl.SkinButton btnrefresh;
        protected CCWin.SkinControl.SkinButton btnDownUnit;
        protected CCWin.SkinControl.SkinButton btnDownTestData;
        protected CCWin.SkinControl.SkinButton btnDelete;
        protected CCWin.SkinControl.SkinButton BtnDownSupervis;
        protected CCWin.SkinControl.SkinButton BtnReSupervis;
        protected CCWin.SkinControl.SkinButton BtnDeletesupervis;
        protected CCWin.SkinControl.SkinButton btnDownCompany;
        protected CCWin.SkinControl.SkinButton btnItem;
        protected CCWin.SkinControl.SkinButton btnAllData;
    }
}
