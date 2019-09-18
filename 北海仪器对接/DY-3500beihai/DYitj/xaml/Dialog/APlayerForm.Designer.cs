namespace AIO.xaml.Dialog
{
    partial class APlayerForm
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
            System.Windows.Forms.ColumnHeader FilePath;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APlayerForm));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_NextPage = new System.Windows.Forms.Button();
            this.Btn_Player = new System.Windows.Forms.Button();
            this.Btn_UpPage = new System.Windows.Forms.Button();
            this.Lb_AllTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lb_NowTime = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axPlayer = new AxAPlayer3Lib.AxPlayer();
            FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // FilePath
            // 
            FilePath.Text = "路径";
            FilePath.Width = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(531, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 311);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listView);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(418, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(116, 311);
            this.panel4.TabIndex = 6;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileName,
            FilePath});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(116, 311);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // FileName
            // 
            this.FileName.Text = "播放列表";
            this.FileName.Width = 110;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(415, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 311);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 256);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(415, 55);
            this.panel3.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_NextPage);
            this.panel2.Controls.Add(this.Btn_Player);
            this.panel2.Controls.Add(this.Btn_UpPage);
            this.panel2.Controls.Add(this.Lb_AllTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Lb_NowTime);
            this.panel2.Controls.Add(this.trackBar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 55);
            this.panel2.TabIndex = 9;
            // 
            // Btn_NextPage
            // 
            this.Btn_NextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_NextPage.Enabled = false;
            this.Btn_NextPage.Location = new System.Drawing.Point(178, 23);
            this.Btn_NextPage.Name = "Btn_NextPage";
            this.Btn_NextPage.Size = new System.Drawing.Size(53, 23);
            this.Btn_NextPage.TabIndex = 6;
            this.Btn_NextPage.Text = "下一页";
            this.Btn_NextPage.UseVisualStyleBackColor = true;
            this.Btn_NextPage.Click += new System.EventHandler(this.Btn_NextPage_Click);
            // 
            // Btn_Player
            // 
            this.Btn_Player.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Player.Location = new System.Drawing.Point(108, 23);
            this.Btn_Player.Name = "Btn_Player";
            this.Btn_Player.Size = new System.Drawing.Size(53, 23);
            this.Btn_Player.TabIndex = 5;
            this.Btn_Player.Text = "播放";
            this.Btn_Player.UseVisualStyleBackColor = true;
            this.Btn_Player.Click += new System.EventHandler(this.Btn_Player_Click);
            // 
            // Btn_UpPage
            // 
            this.Btn_UpPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_UpPage.Enabled = false;
            this.Btn_UpPage.Location = new System.Drawing.Point(38, 23);
            this.Btn_UpPage.Name = "Btn_UpPage";
            this.Btn_UpPage.Size = new System.Drawing.Size(53, 23);
            this.Btn_UpPage.TabIndex = 4;
            this.Btn_UpPage.Text = "上一页";
            this.Btn_UpPage.UseVisualStyleBackColor = true;
            this.Btn_UpPage.Click += new System.EventHandler(this.Btn_UpPage_Click);
            // 
            // Lb_AllTime
            // 
            this.Lb_AllTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lb_AllTime.AutoSize = true;
            this.Lb_AllTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_AllTime.Location = new System.Drawing.Point(355, 26);
            this.Lb_AllTime.Name = "Lb_AllTime";
            this.Lb_AllTime.Size = new System.Drawing.Size(56, 17);
            this.Lb_AllTime.TabIndex = 3;
            this.Lb_AllTime.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(339, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "/";
            // 
            // Lb_NowTime
            // 
            this.Lb_NowTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lb_NowTime.AutoSize = true;
            this.Lb_NowTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_NowTime.ForeColor = System.Drawing.Color.Green;
            this.Lb_NowTime.Location = new System.Drawing.Point(281, 26);
            this.Lb_NowTime.Name = "Lb_NowTime";
            this.Lb_NowTime.Size = new System.Drawing.Size(56, 17);
            this.Lb_NowTime.TabIndex = 1;
            this.Lb_NowTime.Text = "00:00:00";
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.AutoSize = false;
            this.trackBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.trackBar.Location = new System.Drawing.Point(4, 4);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(411, 19);
            this.trackBar.TabIndex = 0;
            this.trackBar.TabStop = false;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            this.trackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseDown);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axPlayer);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 311);
            this.panel1.TabIndex = 6;
            // 
            // axPlayer
            // 
            this.axPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPlayer.Enabled = true;
            this.axPlayer.Location = new System.Drawing.Point(0, 0);
            this.axPlayer.Name = "axPlayer";
            this.axPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPlayer.OcxState")));
            this.axPlayer.Size = new System.Drawing.Size(415, 256);
            this.axPlayer.TabIndex = 10;
            // 
            // APlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "APlayerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APlayerForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.APlayerForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Btn_NextPage;
        private System.Windows.Forms.Button Btn_Player;
        private System.Windows.Forms.Button Btn_UpPage;
        private System.Windows.Forms.Label Lb_AllTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lb_NowTime;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Panel panel1;
        private AxAPlayer3Lib.AxPlayer axPlayer;

    }
}