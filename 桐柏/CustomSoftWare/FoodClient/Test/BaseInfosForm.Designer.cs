namespace FoodClient.Test
{
    partial class BaseInfosForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseInfosForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_updateBaseInfos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_ToView = new System.Windows.Forms.ToolStripButton();
            this.btn_Upload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUSES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AUTHOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUBLISHER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INFORTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(223)))), ((int)(((byte)(205)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_updateBaseInfos,
            this.toolStripSeparator10,
            this.btn_ToView,
            this.btn_Upload,
            this.toolStripSeparator2,
            this.tsb_Exit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(635, 39);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "快捷菜单";
            // 
            // btn_updateBaseInfos
            // 
            this.btn_updateBaseInfos.Image = ((System.Drawing.Image)(resources.GetObject("btn_updateBaseInfos.Image")));
            this.btn_updateBaseInfos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_updateBaseInfos.Name = "btn_updateBaseInfos";
            this.btn_updateBaseInfos.Size = new System.Drawing.Size(76, 36);
            this.btn_updateBaseInfos.Text = "更新通知";
            this.btn_updateBaseInfos.Click += new System.EventHandler(this.btn_updateBaseInfos_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_ToView
            // 
            this.btn_ToView.Image = ((System.Drawing.Image)(resources.GetObject("btn_ToView.Image")));
            this.btn_ToView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ToView.Name = "btn_ToView";
            this.btn_ToView.Size = new System.Drawing.Size(76, 36);
            this.btn_ToView.Text = "查看通知";
            this.btn_ToView.Click += new System.EventHandler(this.btn_ToView_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Image = global::FoodClient.Properties.Resources.print;
            this.btn_Upload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(76, 36);
            this.btn_Upload.Text = "标记已读";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(52, 36);
            this.tsb_Exit.Text = "退出";
            this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.STATUSES,
            this.TITLE,
            this.PDATE,
            this.AUTHOR,
            this.PUBLISHER,
            this.CONTENT,
            this.CARNAME,
            this.INFORTYPE,
            this.EDATE,
            this.SDATE,
            this.VNUM});
            this.dataGridView.Location = new System.Drawing.Point(1, 38);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(633, 355);
            this.dataGridView.TabIndex = 24;
            this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // STATUSES
            // 
            this.STATUSES.DataPropertyName = "STATUSES";
            this.STATUSES.HeaderText = "状态";
            this.STATUSES.Name = "STATUSES";
            this.STATUSES.ReadOnly = true;
            // 
            // TITLE
            // 
            this.TITLE.DataPropertyName = "TITLE";
            this.TITLE.HeaderText = "标题";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            // 
            // PDATE
            // 
            this.PDATE.DataPropertyName = "PDATE";
            this.PDATE.HeaderText = "发布时间";
            this.PDATE.Name = "PDATE";
            this.PDATE.ReadOnly = true;
            // 
            // AUTHOR
            // 
            this.AUTHOR.DataPropertyName = "AUTHOR";
            this.AUTHOR.HeaderText = "作者";
            this.AUTHOR.Name = "AUTHOR";
            this.AUTHOR.ReadOnly = true;
            // 
            // PUBLISHER
            // 
            this.PUBLISHER.DataPropertyName = "PUBLISHER";
            this.PUBLISHER.HeaderText = "发布人";
            this.PUBLISHER.Name = "PUBLISHER";
            this.PUBLISHER.ReadOnly = true;
            // 
            // CONTENT
            // 
            this.CONTENT.DataPropertyName = "CONTENT";
            this.CONTENT.HeaderText = "信息内容";
            this.CONTENT.Name = "CONTENT";
            this.CONTENT.ReadOnly = true;
            // 
            // CARNAME
            // 
            this.CARNAME.DataPropertyName = "CARNAME";
            this.CARNAME.HeaderText = "接受检测车或仪器";
            this.CARNAME.Name = "CARNAME";
            this.CARNAME.ReadOnly = true;
            this.CARNAME.Width = 150;
            // 
            // INFORTYPE
            // 
            this.INFORTYPE.DataPropertyName = "INFORTYPE";
            this.INFORTYPE.HeaderText = "信息类别";
            this.INFORTYPE.Name = "INFORTYPE";
            this.INFORTYPE.ReadOnly = true;
            // 
            // EDATE
            // 
            this.EDATE.DataPropertyName = "EDATE";
            this.EDATE.HeaderText = "编辑时间";
            this.EDATE.Name = "EDATE";
            this.EDATE.ReadOnly = true;
            // 
            // SDATE
            // 
            this.SDATE.DataPropertyName = "SDATE";
            this.SDATE.HeaderText = "置顶时间";
            this.SDATE.Name = "SDATE";
            this.SDATE.ReadOnly = true;
            // 
            // VNUM
            // 
            this.VNUM.DataPropertyName = "VNUM";
            this.VNUM.HeaderText = "浏览次数";
            this.VNUM.Name = "VNUM";
            this.VNUM.ReadOnly = true;
            // 
            // BaseInfosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(634, 394);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BaseInfosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息通知";
            this.Load += new System.EventHandler(this.BaseInfosForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_updateBaseInfos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btn_ToView;
        private System.Windows.Forms.ToolStripButton btn_Upload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUSES;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn AUTHOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUBLISHER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn INFORTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNUM;
    }
}