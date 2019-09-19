namespace WorkstationUI.function
{
    partial class ucEquipmenManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEquipmenManage));
            this.label1 = new System.Windows.Forms.Label();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.btnAdd = new CCWin.SkinControl.SkinButton();
            this.btnDelete = new CCWin.SkinControl.SkinButton();
            this.btnUpdate = new CCWin.SkinControl.SkinButton();
            this.btnsave = new CCWin.SkinControl.SkinButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelEquipment = new System.Windows.Forms.Label();
            this.chkBoxMachine = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 38;
            this.label1.Text = "仪器管理";
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
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
            this.CheckDatas.Location = new System.Drawing.Point(0, 75);
            this.CheckDatas.Name = "CheckDatas";
            this.CheckDatas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CheckDatas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.CheckDatas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.CheckDatas.RowTemplate.Height = 23;
            this.CheckDatas.Size = new System.Drawing.Size(806, 529);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.CheckDatas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CheckDatas_CellClick);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnAdd.DownBack = null;
            this.btnAdd.Location = new System.Drawing.Point(197, 46);
            this.btnAdd.MouseBack = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormlBack = null;
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 43;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDelete.DownBack = null;
            this.btnDelete.Location = new System.Drawing.Point(529, 46);
            this.btnDelete.MouseBack = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormlBack = null;
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 44;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnUpdate.DownBack = null;
            this.btnUpdate.Location = new System.Drawing.Point(678, 46);
            this.btnUpdate.MouseBack = null;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.NormlBack = null;
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 45;
            this.btnUpdate.Text = "刷新";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnsave.DownBack = null;
            this.btnsave.Location = new System.Drawing.Point(358, 46);
            this.btnsave.MouseBack = null;
            this.btnsave.Name = "btnsave";
            this.btnsave.NormlBack = null;
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 46;
            this.btnsave.Text = "保存";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelEquipment);
            this.panel1.Location = new System.Drawing.Point(155, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 25);
            this.panel1.TabIndex = 47;
            this.panel1.Visible = false;
            // 
            // labelEquipment
            // 
            this.labelEquipment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEquipment.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEquipment.ForeColor = System.Drawing.Color.Blue;
            this.labelEquipment.Location = new System.Drawing.Point(11, 0);
            this.labelEquipment.Name = "labelEquipment";
            this.labelEquipment.Size = new System.Drawing.Size(584, 19);
            this.labelEquipment.TabIndex = 0;
            this.labelEquipment.Text = "已选择仪器：";
            this.labelEquipment.Visible = false;
            // 
            // chkBoxMachine
            // 
            this.chkBoxMachine.AutoSize = true;
            this.chkBoxMachine.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxMachine.Location = new System.Drawing.Point(19, 50);
            this.chkBoxMachine.Name = "chkBoxMachine";
            this.chkBoxMachine.Size = new System.Drawing.Size(96, 16);
            this.chkBoxMachine.TabIndex = 48;
            this.chkBoxMachine.Text = "仅选一款仪器";
            this.chkBoxMachine.UseVisualStyleBackColor = false;
            // 
            // ucEquipmenManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.chkBoxMachine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CheckDatas);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "ucEquipmenManage";
            this.Size = new System.Drawing.Size(824, 607);
            this.Load += new System.EventHandler(this.ucEquipmenManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        protected CCWin.SkinControl.SkinButton btnAdd;
        protected CCWin.SkinControl.SkinButton btnDelete;
        protected CCWin.SkinControl.SkinButton btnUpdate;
        protected CCWin.SkinControl.SkinButton btnsave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelEquipment;
        public CCWin.SkinControl.SkinDataGridView CheckDatas;
        private System.Windows.Forms.CheckBox chkBoxMachine;
    }
}
