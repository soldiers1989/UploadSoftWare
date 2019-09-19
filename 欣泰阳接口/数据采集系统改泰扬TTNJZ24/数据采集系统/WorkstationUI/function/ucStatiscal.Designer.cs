namespace WorkstationUI.function
{
    partial class ucStatiscal
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucStatiscal));
            this.label1 = new System.Windows.Forms.Label();
            this.CheckDatas = new CCWin.SkinControl.SkinDataGridView();
            this.btnfind = new CCWin.SkinControl.SkinButton();
            this.cmbTestItem = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSample = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbResult = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dTPEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dTStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStatical = new CCWin.SkinControl.SkinButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "统计分析";
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
            this.CheckDatas.Location = new System.Drawing.Point(3, 78);
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
            this.CheckDatas.Size = new System.Drawing.Size(1088, 69);
            this.CheckDatas.TabIndex = 1;
            this.CheckDatas.TitleBack = null;
            this.CheckDatas.TitleBackColorBegin = System.Drawing.Color.White;
            this.CheckDatas.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.CheckDatas.Visible = false;
            // 
            // btnfind
            // 
            this.btnfind.BackColor = System.Drawing.Color.Transparent;
            this.btnfind.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnfind.DownBack = null;
            this.btnfind.Location = new System.Drawing.Point(1018, 48);
            this.btnfind.MouseBack = null;
            this.btnfind.Name = "btnfind";
            this.btnfind.NormlBack = null;
            this.btnfind.Size = new System.Drawing.Size(75, 23);
            this.btnfind.TabIndex = 62;
            this.btnfind.Text = "统计";
            this.btnfind.UseVisualStyleBackColor = false;
            this.btnfind.Click += new System.EventHandler(this.btnfind_Click);
            // 
            // cmbTestItem
            // 
            this.cmbTestItem.BackColor = System.Drawing.Color.White;
            this.cmbTestItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTestItem.FormattingEnabled = true;
            this.cmbTestItem.Location = new System.Drawing.Point(668, 50);
            this.cmbTestItem.Name = "cmbTestItem";
            this.cmbTestItem.Size = new System.Drawing.Size(126, 20);
            this.cmbTestItem.TabIndex = 61;
            this.cmbTestItem.Text = "请选择...";
            this.cmbTestItem.SelectedValueChanged += new System.EventHandler(this.cmbTestItem_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(597, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "检测项目：";
            // 
            // cmbSample
            // 
            this.cmbSample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSample.FormattingEnabled = true;
            this.cmbSample.Location = new System.Drawing.Point(880, 51);
            this.cmbSample.Name = "cmbSample";
            this.cmbSample.Size = new System.Drawing.Size(121, 20);
            this.cmbSample.TabIndex = 59;
            this.cmbSample.Text = "请选择...";
            this.cmbSample.SelectedValueChanged += new System.EventHandler(this.cmbSample_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(813, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 58;
            this.label7.Text = "样品名称：";
            // 
            // cmbResult
            // 
            this.cmbResult.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbResult.FormattingEnabled = true;
            this.cmbResult.Items.AddRange(new object[] {
            "请选择...",
            "合格",
            "不合格",
            "阴性",
            "阳性"});
            this.cmbResult.Location = new System.Drawing.Point(467, 50);
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.Size = new System.Drawing.Size(103, 20);
            this.cmbResult.TabIndex = 57;
            this.cmbResult.Text = "请选择...";
            this.cmbResult.SelectedValueChanged += new System.EventHandler(this.cmbResult_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(396, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 56;
            this.label2.Text = "检测结论：";
            // 
            // dTPEnd
            // 
            this.dTPEnd.Location = new System.Drawing.Point(265, 51);
            this.dTPEnd.Name = "dTPEnd";
            this.dTPEnd.Size = new System.Drawing.Size(121, 21);
            this.dTPEnd.TabIndex = 55;
            this.dTPEnd.ValueChanged += new System.EventHandler(this.dTPEnd_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(203, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 54;
            this.label4.Text = "结束日期：";
            // 
            // dTStart
            // 
            this.dTStart.Location = new System.Drawing.Point(65, 49);
            this.dTStart.Name = "dTStart";
            this.dTStart.Size = new System.Drawing.Size(127, 21);
            this.dTStart.TabIndex = 53;
            this.dTStart.ValueChanged += new System.EventHandler(this.dTStart_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "开始日期：";
            // 
            // btnStatical
            // 
            this.btnStatical.BackColor = System.Drawing.Color.Transparent;
            this.btnStatical.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnStatical.DownBack = null;
            this.btnStatical.Location = new System.Drawing.Point(3, 76);
            this.btnStatical.MouseBack = null;
            this.btnStatical.Name = "btnStatical";
            this.btnStatical.NormlBack = null;
            this.btnStatical.Size = new System.Drawing.Size(75, 23);
            this.btnStatical.TabIndex = 63;
            this.btnStatical.Text = "统计分析";
            this.btnStatical.UseVisualStyleBackColor = false;
            this.btnStatical.Visible = false;
            this.btnStatical.Click += new System.EventHandler(this.btnStatical_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.AppStarting;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(15, 89);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(732, 243);
            this.chart1.TabIndex = 67;
            this.chart1.Text = "chart1";
            title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title1.Name = "Title1";
            title1.Text = "检测项目数量统计";
            this.chart1.Titles.Add(title1);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(772, 89);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.IsValueShownAsLabel = true;
            series2.IsXValueIndexed = true;
            series2.Label = "#AXISLABEL  #VAL";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(319, 243);
            this.chart2.TabIndex = 68;
            this.chart2.Text = "chart2";
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title2.Name = "Title1";
            title2.Text = "检测结果统计";
            this.chart2.Titles.Add(title2);
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chart3.Legends.Add(legend3);
            this.chart3.Location = new System.Drawing.Point(15, 349);
            this.chart3.Name = "chart3";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.LabelForeColor = System.Drawing.Color.Transparent;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart3.Series.Add(series3);
            this.chart3.Size = new System.Drawing.Size(1078, 236);
            this.chart3.TabIndex = 69;
            this.chart3.Text = "chart3";
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title3.Name = "Title1";
            title3.Text = "样品数量统计";
            this.chart3.Titles.Add(title3);
            // 
            // ucStatiscal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.CheckDatas);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnStatical);
            this.Controls.Add(this.btnfind);
            this.Controls.Add(this.cmbTestItem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbSample);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTPEnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dTStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "ucStatiscal";
            this.Size = new System.Drawing.Size(1117, 602);
            this.Load += new System.EventHandler(this.ucStatiscal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckDatas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        protected CCWin.SkinControl.SkinDataGridView CheckDatas;
        protected CCWin.SkinControl.SkinButton btnfind;
        private System.Windows.Forms.ComboBox cmbTestItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSample;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTPEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dTStart;
        private System.Windows.Forms.Label label3;
        protected CCWin.SkinControl.SkinButton btnStatical;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
    }
}
