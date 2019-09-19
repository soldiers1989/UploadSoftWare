using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Web.Security;

using DY.FoodClientLib;
using DY.Process;

namespace FoodClient
{
	/// <summary>
	/// frmResultSend 的摘要说明。
	/// </summary>
	public class frmResultSend : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnUpload;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private C1.Win.C1List.C1Combo cmbResultType;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label label4;
		private C1.Win.C1List.C1Combo cmbIsSend;
 
		private System.ComponentModel.Container components = null;

		public frmResultSend()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            C1.Win.C1List.Style style1 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultSend));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbResultType = new C1.Win.C1List.C1Combo();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbIsSend = new C1.Win.C1List.C1Combo();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResultType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(168, 127);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(72, 24);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "上传";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbResultType);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cmbIsSend);
            this.groupBox1.Location = new System.Drawing.Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据上传选项";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "检测类别：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy年MM月dd日";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(120, 52);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(180, 21);
            this.dtpEndDate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "数据截止日期：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy年MM月dd日";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(120, 24);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(180, 21);
            this.dtpStartDate.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据起始日期：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbResultType
            // 
            this.cmbResultType.AddItemSeparator = ';';
            this.cmbResultType.Caption = "";
            this.cmbResultType.CaptionHeight = 17;
            this.cmbResultType.CaptionStyle = style1;
            this.cmbResultType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbResultType.ColumnCaptionHeight = 18;
            this.cmbResultType.ColumnFooterHeight = 18;
            this.cmbResultType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbResultType.ContentHeight = 16;
            this.cmbResultType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbResultType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbResultType.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbResultType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbResultType.EditorHeight = 16;
            this.cmbResultType.EvenRowStyle = style2;
            this.cmbResultType.FooterStyle = style3;
            this.cmbResultType.GapHeight = 2;
            this.cmbResultType.HeadingStyle = style4;
            this.cmbResultType.HighLightRowStyle = style5;
            this.cmbResultType.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbResultType.Images"))));
            this.cmbResultType.ItemHeight = 15;
            this.cmbResultType.Location = new System.Drawing.Point(120, 80);
            this.cmbResultType.MatchEntryTimeout = ((long)(2000));
            this.cmbResultType.MaxDropDownItems = ((short)(5));
            this.cmbResultType.MaxLength = 32767;
            this.cmbResultType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbResultType.Name = "cmbResultType";
            this.cmbResultType.OddRowStyle = style6;
            this.cmbResultType.PartialRightColumn = false;
            this.cmbResultType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbResultType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbResultType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbResultType.SelectedStyle = style7;
            this.cmbResultType.Size = new System.Drawing.Size(180, 22);
            this.cmbResultType.Style = style8;
            this.cmbResultType.TabIndex = 3;
            this.cmbResultType.Visible = false;
            this.cmbResultType.PropBag = resources.GetString("cmbResultType.PropBag");
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(16, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 21);
            this.label13.TabIndex = 6;
            this.label13.Text = "是否发送：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Visible = false;
            // 
            // cmbIsSend
            // 
            this.cmbIsSend.AddItemSeparator = ';';
            this.cmbIsSend.Caption = "";
            this.cmbIsSend.CaptionHeight = 17;
            this.cmbIsSend.CaptionStyle = style9;
            this.cmbIsSend.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbIsSend.ColumnCaptionHeight = 18;
            this.cmbIsSend.ColumnFooterHeight = 18;
            this.cmbIsSend.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbIsSend.ContentHeight = 16;
            this.cmbIsSend.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbIsSend.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbIsSend.EditorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbIsSend.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbIsSend.EditorHeight = 16;
            this.cmbIsSend.EvenRowStyle = style10;
            this.cmbIsSend.FooterStyle = style11;
            this.cmbIsSend.GapHeight = 2;
            this.cmbIsSend.HeadingStyle = style12;
            this.cmbIsSend.HighLightRowStyle = style13;
            this.cmbIsSend.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbIsSend.Images"))));
            this.cmbIsSend.ItemHeight = 15;
            this.cmbIsSend.Location = new System.Drawing.Point(120, 80);
            this.cmbIsSend.MatchEntryTimeout = ((long)(2000));
            this.cmbIsSend.MaxDropDownItems = ((short)(5));
            this.cmbIsSend.MaxLength = 32767;
            this.cmbIsSend.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbIsSend.Name = "cmbIsSend";
            this.cmbIsSend.OddRowStyle = style14;
            this.cmbIsSend.PartialRightColumn = false;
            this.cmbIsSend.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbIsSend.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbIsSend.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbIsSend.SelectedStyle = style15;
            this.cmbIsSend.Size = new System.Drawing.Size(180, 22);
            this.cmbIsSend.Style = style16;
            this.cmbIsSend.TabIndex = 2;
            this.cmbIsSend.Visible = false;
            this.cmbIsSend.PropBag = resources.GetString("cmbIsSend.PropBag");
            // 
            // frmResultSend
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(352, 164);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResultSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测数据上传";
            this.Load += new System.EventHandler(this.frmResultSend_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbResultType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	
        private void frmResultSend_Load(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.WindowState= FormWindowState.Normal;
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;
            cmbResultType.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbResultType.AddItemCols = 1;
            cmbResultType.AddItemTitles("检测数据");
            cmbResultType.AddItem("");
            cmbResultType.AddItem(ShareOption.ResultType1);
            cmbResultType.AddItem(ShareOption.ResultType2);
            cmbResultType.AddItem(ShareOption.ResultType3);

            cmbIsSend.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbIsSend.AddItemCols = 1;
            cmbIsSend.AddItemTitles("发送状态");
            cmbIsSend.AddItem("");
            cmbIsSend.AddItem(ShareOption.SendState1);
            cmbIsSend.AddItem(ShareOption.SendState0);
            cmbIsSend.SelectedIndex = 2;
        }

        StringBuilder sbWhere = new StringBuilder();
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, System.EventArgs e)
        {
            sbWhere.Length = 0;
            sbWhere.Append("CheckStartDate>=#");
            sbWhere.Append(dtpStartDate.Value.ToString("yyyy-MM-dd"));
            sbWhere.Append(" 00:00:00#");
            sbWhere.Append(" AND CheckStartDate<=#");
            sbWhere.Append(dtpEndDate.Value.ToString("yyyy-MM-dd"));
            sbWhere.Append(" 23:59:59#");

            string sendTag = cmbIsSend.Text.Trim();
            if (!sendTag.Equals(string.Empty))
            {
                if (sendTag.Equals(ShareOption.SendState1))//已经发送
                {
                    sbWhere.Append(" AND IsSended=true");
                }
                else if (sendTag.Equals(ShareOption.SendState0))
                {
                    sbWhere.Append(" AND IsSended=false");
                    
                }
            }
            if (!cmbResultType.Text.Trim().Equals(""))
            {
                sbWhere.Append(" AND ResultType='");
                sbWhere.Append(cmbResultType.Text.Trim());
                sbWhere.Append("'");
            }

            //检查是否有数据
            string errMsg = string.Empty;
            clsResultOpr model = new clsResultOpr();
            int intRecCount = model.GetRecCount(sbWhere.ToString(), out errMsg);
           
            if (errMsg.Equals(string.Empty) && intRecCount == 0)
            {
                MessageBox.Show(this, "没有所需要上传的纪录!","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ShareOption.SysServerIP.Equals(string.Empty) || ShareOption.SysServerID.Equals(string.Empty))
            {
                MessageBox.Show(this, "请先到选项菜单中设置服务器地址与登录ID！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;
            groupBox1.Enabled = false;

            //CommonOperation.DataUpload(sb.ToString());
            //Cursor = Cursors.Default;

            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.UploadProcess;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();
        }

        /// <summary>
        /// 上传数据加进度条
        /// </summary>
        /// <param name="percent"></param>
        private void UploadProcess(Action<int> percent)
        {
            percent(0);
            DataTable dtResult = new DataTable("Results");
            try
            {
                clsResultOpr resultBll = new clsResultOpr();
                sbWhere.Append(" AND A.SysCode<>'' AND A.CheckNo<>'' AND A.FoodCode<>''");
                dtResult = resultBll.GetUploadDataTable(sbWhere.ToString(), "A.SysCode",ShareOption.ApplicationTag);//GetAsDataTable(strWhere, "SysCode");
                sbWhere.Length = 0;
                percent(5);
                if (dtResult == null)
                {
                    percent(100);
                    return;
                }
                string currentUser = CurrentUser.GetInstance().UserInfo.Name;
                string checkUnitName = string.Empty;
                string tag = "";
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    #region 检验为空时
                    object obj = null;
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        obj = dtResult.Rows[i]["SampleCode"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SampleCode"] = tag;
                        }

                        obj = dtResult.Rows[i]["CheckedCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckedCompany"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckedCompanyInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckedCompanyInfo"] = tag;
                        }

                        obj = dtResult.Rows[i]["CheckedComDis"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckedComDis"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckPlace"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckPlace"] = tag;
                        }
                        obj = dtResult.Rows[i]["FoodName"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["FoodName"] = tag;
                        }
                        obj = dtResult.Rows[i]["SentCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SentCompany"] = tag;
                        }
                        obj = dtResult.Rows[i]["Provider"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Provider"] = tag;
                        }
                        obj = dtResult.Rows[i]["ProduceDate"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["ProduceDate"] = DBNull.Value;
                        }
                        obj = dtResult.Rows[i]["ProduceCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["ProduceCompany"] = tag;
                        }
                        obj = dtResult.Rows[i]["ProducePlace"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["ProducePlace"] = tag;
                        }

                        dtResult.Rows[i]["UpLoader"] = currentUser;
                        obj = dtResult.Rows[i]["ProducePlaceInfo"];
                        if (obj != null && (obj.ToString().IndexOf("null") >= 0))
                        {
                            dtResult.Rows[i]["ProducePlaceInfo"] = DBNull.Value;
                        }
                        else
                        {
                            dtResult.Rows[i]["ProducePlaceInfo"] = obj.ToString().TrimEnd('/');
                        }

                        obj = dtResult.Rows[i]["Unit"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Unit"] = tag;
                        }
                        obj = dtResult.Rows[i]["SampleUnit"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SampleUnit"] = tag;
                        }
                        obj = dtResult.Rows[i]["SampleModel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SampleModel"] = tag;
                        }
                        obj = dtResult.Rows[i]["SampleLevel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SampleLevel"] = tag;
                        }
                        obj = dtResult.Rows[i]["SampleState"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["SampleState"] = tag;
                        }
                        obj = dtResult.Rows[i]["Standard"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Standard"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckMachine"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckMachine"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckMachineModel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckMachineModel"] = tag;
                        }
                        obj = dtResult.Rows[i]["MachineCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["MachineCompany"] = tag;
                        }

                        obj = dtResult.Rows[i]["CheckValueInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckValueInfo"] = tag;
                        }

                        obj = dtResult.Rows[i]["StandValue"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["StandValue"] = tag;
                        }
                        obj = dtResult.Rows[i]["Result"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Result"] = tag;
                        }
                        obj = dtResult.Rows[i]["ResultInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["ResultInfo"] = tag;
                        }
                        obj = dtResult.Rows[i]["UpperCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["UpperCompany"] = tag;
                        }

                        obj = dtResult.Rows[i]["CheckType"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckType"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckUnitName"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckUnitName"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckUnitInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckUnitInfo"] = tag;
                        }
                        obj = dtResult.Rows[i]["Checker"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Checker"] = tag;
                        }
                        obj = dtResult.Rows[i]["Assessor"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Assessor"] = tag;
                        }
                        obj = dtResult.Rows[i]["Organizer"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Organizer"] = tag;
                        }

                        obj = dtResult.Rows[i]["Remark"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Remark"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckPlanCode"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckPlanCode"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckederVal"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckederVal"] = tag;
                        }
                        obj = dtResult.Rows[i]["IsSentCheck"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["IsSentCheck"] = tag;
                        }
                        obj = dtResult.Rows[i]["CheckederRemark"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["CheckederRemark"] = tag;
                        }

                        obj = dtResult.Rows[i]["Notes"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dtResult.Rows[i]["Notes"] = tag;
                        }
                    }
                    #endregion
                    percent(7);

                    checkUnitName = dtResult.Rows[0]["CheckUnitName"].ToString();
                    DataSet dst = new DataSet("UpdateResult");
                    dst.Tables.Add(dtResult.Copy());

                    percent(10);

                    if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
                    {
                         localhost.DataSyncService ws = new localhost.DataSyncService();
                        //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                        ws.Url = ShareOption.SysServerIP;
                        string ret = ws.SetDataDriver(dst.GetXml(), ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), checkUnitName, 3);//加检测点名称

                        percent(70);

                        if (ret.IndexOf("errofInfo") >= 0)//存在错误
                        {
                            MessageBox.Show("上传数据失败 " + ret);
                            percent(100);
                            return;
                        }
                        clsResult model = null;
                        string user = CurrentUser.GetInstance().UserInfo.UserCode;
                        string err = string.Empty;
                        string reSendTag = "否";
                        int len = 0;
                        int ind = ret.IndexOf("|");
                        if (ind >= 0)//如果是传递部分数据 数据格式如： 2|29393939,112030283  2表示已经成功的条数，后面表示未成功的编号用"，"隔开
                        {
                            string first = ret.Substring(0, ind);
                            if (first.Trim() == "0")
                            {
                                MessageBox.Show("上传数据失败 ");
                                percent(100);
                                return;
                            }

                            string retTemp = ret.Substring(ind + 1, ret.Length - ind - 1);
                            string[] tArray = retTemp.Split(',');
                            len = tArray.Length;
                            System.Collections.Hashtable htbl = new System.Collections.Hashtable();
                            if (len > 0)
                            {
                                for (int i = 0; i < len; i++)//不成功的编号
                                {
                                    htbl.Add(tArray[i], "0");
                                }
                                string temp = string.Empty;
                                int j = 0;

                                int k = (dtResult.Rows.Count / 30 + 1);

                                for (int i = 0; i < dtResult.Rows.Count; i++)
                                {
                                    temp = dtResult.Rows[i]["SysCode"].ToString();
                                    if (htbl[temp] != null && htbl[temp].ToString() == "0")
                                    {
                                        continue;
                                    }
                                    model = new clsResult();
                                    model.SysCode = temp;
                                    model.IsSended = true;
                                    reSendTag = dtResult.Rows[i]["IsReSended"].ToString();
                                    if (reSendTag == "是")
                                    {
                                        model.IsReSended = true;
                                    }
                                    else
                                    {
                                        model.IsReSended = false;
                                    }
                                    model.SendedDate = DateTime.Now;
                                    model.Sender = user;
                                    resultBll.SetUploadFlag(model, out err);
                                    j++;
                                    percent(i / k + 70);
                                }
                                percent(100);
                                MessageBox.Show("数据部分上传成功，共上传" + j + "条记录");
                                
                            }
                        }
                        else //全部成功
                        {
                            len = Convert.ToInt32(ret);
                            if (len <= 0)
                            {
                                percent(100);

                                MessageBox.Show("没有符合的数据记录");
                                return;
                            }
                            int k = len / 30 + 1;

                            for (int i = 0; i < len; i++)
                            {
                                model = new clsResult();
                                model.SysCode = dtResult.Rows[i]["SysCode"].ToString();
                                model.IsSended = true;
                                reSendTag = dtResult.Rows[i]["IsReSended"].ToString();
                                if (reSendTag == "是")
                                {
                                    model.IsReSended = true;
                                }
                                else
                                {
                                    model.IsReSended = false;
                                }
                                model.SendedDate = DateTime.Now;
                                model.Sender = user;
                                resultBll.SetUploadFlag(model, out err);
                                percent(i / k + 70);
                            }
                            percent(100);
                            MessageBox.Show("上传数据成功，共上传" + ret + "条记录");//,用时:"+ts.TotalMilliseconds);
                        }
                        //设置上传的标志位与上传时间
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("上传数据失败，失败原因：" + e.Message);
            }
            //sbWhere.Length = 0;

            percent(100);
        }

       private void backgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                groupBox1.Enabled = true;
                Cursor = Cursors.Default;
                //MessageBox.Show("执行完毕");
            }
            else
            {
                MessageBox.Show("异常:" + e.BackGroundException.Message);
            }
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

	}
}
