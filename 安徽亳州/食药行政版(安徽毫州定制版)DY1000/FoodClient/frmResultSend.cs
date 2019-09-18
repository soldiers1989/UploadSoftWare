using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;
using DY.FoodClientLib;
using DY.Process;
using FoodClient.AnHui;
using FoodClient.HeFei;

namespace FoodClient
{
    /// <summary>
    /// frmResultSend ��ժҪ˵����
    /// </summary>
    public class frmResultSend : TitleBarBase
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
        private StringBuilder sbWhere = new StringBuilder();

        public frmResultSend()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
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
            this.cmbResultType = new C1.Win.C1List.C1Combo();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbIsSend = new C1.Win.C1List.C1Combo();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResultType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.Location = new System.Drawing.Point(71, 95);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(72, 26);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "�ϴ�";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(166, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbResultType);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cmbIsSend);
            this.groupBox1.Location = new System.Drawing.Point(9, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(53, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�����ϴ�ѡ��";
            this.groupBox1.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "������";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
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
            this.cmbResultType.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label13.Text = "�Ƿ��ͣ�";
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
            this.cmbIsSend.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy��MM��dd��";
            this.dtpEndDate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(99, 63);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(180, 26);
            this.dtpEndDate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "��ֹ���ڣ�";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy��MM��dd��";
            this.dtpStartDate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(99, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(180, 26);
            this.dtpStartDate.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "��ʼ���ڣ�";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmResultSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(309, 169);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResultSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��������ϴ�";
            this.Load += new System.EventHandler(this.frmResultSend_Load);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.dtpStartDate, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnUpload, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dtpEndDate, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbResultType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsSend)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmResultSend_Load(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;
            cmbResultType.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbResultType.AddItemCols = 1;
            cmbResultType.AddItemTitles("�������");
            cmbResultType.AddItem("");
            cmbResultType.AddItem(ShareOption.ResultType1);
            cmbResultType.AddItem(ShareOption.ResultType2);
            cmbResultType.AddItem(ShareOption.ResultType3);
            cmbIsSend.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            cmbIsSend.AddItemCols = 1;
            cmbIsSend.AddItemTitles("����״̬");
            cmbIsSend.AddItem("");
            cmbIsSend.AddItem(ShareOption.SendState1);
            cmbIsSend.AddItem(ShareOption.SendState0);
            cmbIsSend.SelectedIndex = 2;
            TitleBarText = "��������ϴ�";

            //string str = ConfigurationManager.AppSettings["AnHui_interfaceVersion"];
            //Global.AnHuiInterface.interfaceVersion = str == null || str.Length == 0 ? "" : str;

            //str = ConfigurationManager.AppSettings["AnHui_userName"];
            //Global.AnHuiInterface.userName = str == null || str.Length == 0 ? "" : str;

            //str = ConfigurationManager.AppSettings["AnHui_passWord"];
            //Global.AnHuiInterface.passWord = str == null || str.Length == 0 ? "" : str;

            //str = ConfigurationManager.AppSettings["AnHui_instrument"];
            //Global.AnHuiInterface.instrument = str == null || str.Length == 0 ? "" : str;

            //str = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            //Global.AnHuiInterface.instrumentNo = str == null || str.Length == 0 ? "" : str;

            //str = ConfigurationManager.AppSettings["AnHui_mac"];
            //Global.AnHuiInterface.mac = str == null || str.Length == 0 ? "" : str;
        }

        /// <summary>
        /// �����ϴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, System.EventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "�豸�޷����ӵ����������������磡", "ϵͳ��ʾ");
                return;
            }

            sbWhere.Length = 0;
            sbWhere.Append("CheckStartDate>=#");
            sbWhere.Append(dtpStartDate.Value.ToString("yyyy-MM-dd"));
            sbWhere.Append(" 00:00:00#");
            sbWhere.Append(" AND CheckStartDate<=#");
            sbWhere.Append(dtpEndDate.Value.ToString("yyyy-MM-dd"));
            sbWhere.Append(" 23:59:59#");
            //2016��2��24�� wenj
            //�����ϴ����������ֵ��Ϊ�յĲ��ϴ�
            sbWhere.Append(" And CheckValueInfo Is Not Null And CheckValueInfo not like ''");
            string sendTag = cmbIsSend.Text.Trim();
            if (!sendTag.Equals(string.Empty))
            {
                if (sendTag.Equals(ShareOption.SendState1))//�Ѿ�����
                    sbWhere.Append(" AND IsSended=true");
                else if (sendTag.Equals(ShareOption.SendState0))
                    sbWhere.Append(" AND IsSended=false");
            }
            if (!cmbResultType.Text.Trim().Equals(""))
            {
                sbWhere.Append(" AND ResultType='");
                sbWhere.Append(cmbResultType.Text.Trim());
                sbWhere.Append("'");
            }

            //����Ƿ�������
            string errMsg = string.Empty;
            clsResultOpr model = new clsResultOpr();
            int intRecCount = model.GetRecCount(sbWhere.ToString(), out errMsg);

            if (errMsg.Equals(string.Empty) && intRecCount == 0)
            {
                MessageBox.Show(this, "û����Ҫ�ϴ��ļ�¼!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #region @zh 2016/11/21 �Ϸ�
            //if (Global.AnHuiInterface.interfaceVersion.Length == 0 || Global.AnHuiInterface.userName.Length == 0 ||
            //    Global.AnHuiInterface.passWord.Length == 0 || Global.AnHuiInterface.instrument.Length == 0 ||
            //    Global.AnHuiInterface.instrumentNo.Length == 0 || Global.AnHuiInterface.mac.Length == 0)
            //{
            //    MessageBox.Show(this, "���ȵ�����������ͬ�����˵������÷����������Ϣ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //PercentProcess process = new PercentProcess();
            //process.BackgroundWork = this.UploadAnHuiProcess;
            //process.MessageInfo = "�����ϴ�";
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            //process.Start();

            if (Global.AnHuiInterface.ServerAddr.Length == 0)
            {
                MessageBox.Show(this, "���ȵ�����������ͬ�����˵��������ϴ���ַ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.UploadHeFeiProcess;
            process.MessageInfo = "�����ϴ�";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();

            #endregion
        }

        /// <summary>
        /// �ϴ����Ϸ�ƽ̨
        /// </summary>
        /// <param name="percent"></param>
        private void UploadHeFeiProcess(Action<int> percent)
        {
            percent(0);
            Global.UploadCount = 0;
            string outErr = string.Empty;
            DataTable dt = new DataTable("Results");
            try
            {
                clsResultOpr resultBll = new clsResultOpr();
                sbWhere.Append(" AND A.SysCode<>'' AND A.CheckNo<>'' AND A.FoodCode<>''");
                dt = resultBll.GetUploadDataTable(sbWhere.ToString(), "A.SysCode", ShareOption.ApplicationTag);
                percent(5);
                float percentage1 = (float)95 / (float)dt.Rows.Count, percentage2 = 0;
                int count = (int)percentage1 + 5;
                FoodClient.AnHui.Global.AnHuiInterface.isok = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    object obj = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HeFeiUploadDataInfo model = new HeFeiUploadDataInfo();
                        //��Ʒ���
                        obj = dt.Rows[i]["FoodTypeName"];
                        if (obj != null && obj != DBNull.Value) model.SampleType = obj.ToString();
                        //��Ʒ����
                        obj = dt.Rows[i]["FoodName"];
                        if (obj != null && obj != DBNull.Value) model.SampleName = obj.ToString();
                        //�ͼ쵥λ
                        obj = dt.Rows[i]["SentCompany"];
                        if (obj != null && obj != DBNull.Value) model.SendUnit = obj.ToString();
                        //������λ�����أ�����
                        obj = dt.Rows[i]["CheckedCompany"];
                        if (obj != null && obj != DBNull.Value) model.ProductionUnit = obj.ToString();
                        //��Ʒ������λ �����쵥λ��
                        obj = dt.Rows[i]["CheckedCompanyInfo"];
                        if (obj != null && obj != DBNull.Value) model.GoodsUnit = obj.ToString();
                        //��ⵥλ
                        obj = dt.Rows[i]["CheckUnitName"];
                        if (obj != null && obj != DBNull.Value) model.CheckUnit = obj.ToString();

                        //��ⵥλ���
                        obj = dt.Rows[i]["CheckUnitCode"];
                        if (obj != null && obj != DBNull.Value) model.CheckUnitCode = obj.ToString();
                        //��Ʒ��������
                        obj = dt.Rows[i]["ProduceDate"];
                        if (obj != null && obj != DBNull.Value) model.GoodsDate = DateTime.Parse(obj.ToString());
                        //������
                        obj = dt.Rows[i]["CheckValueInfo"];
                        if (obj != null && obj != DBNull.Value) model.InhibitionRatio = float.Parse(obj.ToString());
                        //���Ա
                        obj = dt.Rows[i]["Checker"];
                        if (obj != null && obj != DBNull.Value) model.Operator = obj.ToString();
                        //����ʱ��
                        obj = dt.Rows[i]["ProduceDate"];
                        if (obj != null && obj != DBNull.Value) model.ProductionDate = DateTime.Parse(obj.ToString());
                        //����                             
                        obj = dt.Rows[i]["Result"];
                        if (obj != null && obj != DBNull.Value) model.Conclusion=obj.ToString();
                        //if (obj != null && obj != DBNull.Value) model.IsOk = obj.ToString().Equals(ShareOption.ResultEligi) ? true : false;
                        //���ʱ��
                        obj = dt.Rows[i]["CheckStartDate"];
                        if (obj != null && obj != DBNull.Value) model.Chktime = obj.ToString();
                        //�����Ŀ
                        obj = dt.Rows[i]["MachineItemName"];
                        if (obj != null && obj != DBNull.Value) model.ChkItem = obj.ToString();
                        //��Ʒ���
                        obj = dt.Rows[i]["SampleCode"];
                        if (obj != null && obj != DBNull.Value) model.SampleCode = obj.ToString();
                       
                        //�ϴ�
                        if (HeFeiUploadDataInfo.Upload(model))
                        {
                            string err = string.Empty;
                            Global.UploadCount++;
                            clsResult result = new clsResult();
                            result.SysCode = dt.Rows[i]["SysCode"].ToString();
                            result.IsSended = true;
                            result.IsReSended = true;
                            result.SendedDate = DateTime.Now;
                            result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
                            new clsResultOpr().SetUploadFlag(result, out err);
                        }
                        else
                            outErr += "��Ʒ���ƣ�[" + model.SampleName + "] �ϴ�ʧ�ܣ�" ;

                        if (count < 100)
                        {
                            percent(count);
                            percentage2 += percentage1;
                            if (percentage2 > 1)
                            {
                                count += (int)percentage2;
                                percentage2 = 0;
                            }
                        }
                        else
                        {
                            count = 100;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                outErr = e.Message;
            }
            finally
            {
                percent(100);
                if (outErr.Length == 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("�����ϴ��ɹ��� \r\n\r\n���ι��ϴ� " + FoodClient.AnHui.Global.AnHuiInterface.isok + " �����ݡ�", "ϵͳ��ʾ");
                }
                else if (outErr.Length > 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("�����ϴ�ʧ�ܣ� \r\n\r\nʧ��ԭ�� " + outErr, "ϵͳ��ʾ");
                }
                else if (outErr.Length > 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("���������ϴ�ʧ�ܣ� \r\n\r\nʧ��ԭ�� " + outErr, "ϵͳ��ʾ");
                }
            }
        }

        /// <summary>
        /// �ϴ�������ƽ̨
        /// </summary>
        /// <param name="percent"></param>
        private void UploadAnHuiProcess(Action<int> percent)
        {
            percent(0);
            Global.UploadCount = 0;
            string outErr = string.Empty;
            DataTable dt = new DataTable("Results");
            try
            {
                clsResultOpr resultBll = new clsResultOpr();
                sbWhere.Append(" AND A.SysCode<>'' AND A.CheckNo<>'' AND A.FoodCode<>''");
                dt = resultBll.GetUploadDataTable(sbWhere.ToString(), "A.SysCode", ShareOption.ApplicationTag);
                percent(5);
                float percentage1 = (float)95 / (float)dt.Rows.Count, percentage2 = 0;
                int count = (int)percentage1 + 5;
                if (dt != null && dt.Rows.Count > 0)
                {
                    object obj = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                        model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                        model.userName = Global.AnHuiInterface.userName;
                        model.instrument = Global.AnHuiInterface.instrument;
                        model.passWord = Global.AnHuiInterface.passWord;
                        model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                        model.mac = Global.AnHuiInterface.mac;

                        model.gps = string.Empty;
                        //��Ʒ���
                        obj = dt.Rows[i]["SampleCode"];
                        if (obj != null && obj != DBNull.Value) model.sampleNo = obj.ToString();
                        //��Ʒ����
                        obj = dt.Rows[i]["FoodName"];
                        if (obj != null && obj != DBNull.Value) model.fName = obj.ToString();
                        //��Ʒ����
                        obj = dt.Rows[i]["fTpye"];
                        if (obj != null && obj != DBNull.Value) model.fTpye = obj.ToString();
                        //��������
                        obj = dt.Rows[i]["ProduceDate"];
                        if (obj != null && obj != DBNull.Value) model.proDate = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        else model.proDate = "";
                        //���쵥λ���
                        obj = dt.Rows[i]["checkedUnit"];//CheckedCompanyCode
                        if (obj != null && obj != DBNull.Value) model.checkedUnit = obj.ToString();
                        else model.checkedUnit = "0";
                        //�����
                        obj = dt.Rows[i]["CheckNo"];
                        if (obj != null && obj != DBNull.Value) model.dataNum = obj.ToString();
                        //�����Ŀ���
                        obj = dt.Rows[i]["testPro"];
                        if (obj != null && obj != DBNull.Value) model.testPro = obj.ToString();
                        //���������
                        obj = dt.Rows[i]["quanOrQual"];
                        if (obj != null && obj != DBNull.Value) model.quanOrQual = obj.ToString();
                        else model.quanOrQual = "2";
                        //���ֵ
                        obj = dt.Rows[i]["CheckValueInfo"];
                        if (obj != null && obj != DBNull.Value) model.contents = obj.ToString();
                        //���ֵ��λ
                        obj = dt.Rows[i]["Unit"];
                        if (obj != null && obj != DBNull.Value) model.unit = obj.ToString();
                        //�����
                        obj = dt.Rows[i]["Result"];
                        if (obj != null && obj != DBNull.Value) model.testResult = obj.ToString();
                        //����׼ֵ
                        obj = dt.Rows[i]["StandValue"];
                        if (obj != null && obj != DBNull.Value) model.standardLimit = obj.ToString();
                        //�������
                        obj = dt.Rows[i]["Standard"];
                        if (obj != null && obj != DBNull.Value) model.basedStandard = obj.ToString();
                        //���������
                        model.testPerson = CurrentUser.GetInstance().UserInfo.Name;
                        //���ʱ��
                        obj = dt.Rows[i]["CheckStartDate"];
                        if (obj != null && obj != DBNull.Value) model.testTime = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        //���ʱ��
                        obj = dt.Rows[i]["CheckStartDate"];
                        if (obj != null && obj != DBNull.Value) model.sampleTime = DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        //������
                        obj = dt.Rows[i]["Remark"];
                        if (obj != null && obj != DBNull.Value) model.remark = obj.ToString();

                        model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.testTime +
                            model.instrumentNo + model.contents + model.testResult);

                        string str = Global.AnHuiInterface.instrumentInfoHandle(model);
                        List<string> rtnList = Global.AnHuiInterface.ParsingUploadXML(str);
                        if (rtnList[0].Equals("1"))
                        {
                            string err = string.Empty;
                            Global.UploadCount++;
                            clsResult result = new clsResult();
                            result.SysCode = dt.Rows[i]["SysCode"].ToString();
                            result.IsSended = true;
                            result.IsReSended = true;
                            result.SendedDate = DateTime.Now;
                            result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
                            new clsResultOpr().SetUploadFlag(result, out err);
                        }
                        else
                            outErr += "��Ʒ���ƣ�[" + model.fName + "] �ϴ�ʧ�ܣ�\r\n�쳣��Ϣ��" + rtnList[1] + "��\r\n\r\n";

                        if (count < 100)
                        {
                            percent(count);
                            percentage2 += percentage1;
                            if (percentage2 > 1)
                            {
                                count += (int)percentage2;
                                percentage2 = 0;
                            }
                        }
                        else
                        {
                            count = 100;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                outErr = e.Message;
            }
            finally
            {
                percent(100);
                if (outErr.Length == 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("�����ϴ��ɹ��� \r\n\r\n���ι��ϴ� " + Global.UploadCount + " �����ݡ�", "ϵͳ��ʾ");
                }
                else if (outErr.Length > 0 && Global.UploadCount == 0)
                {
                    MessageBox.Show("�����ϴ�ʧ�ܣ� \r\n\r\nʧ��ԭ�� " + outErr, "ϵͳ��ʾ");
                }
                else if (outErr.Length > 0 && Global.UploadCount > 0)
                {
                    MessageBox.Show("���������ϴ�ʧ�ܣ� \r\n\r\nʧ��ԭ�� " + outErr, "ϵͳ��ʾ");
                }
            }
        }

        /// <summary>
        /// �ϴ����ݼӽ�����
        /// </summary>
        /// <param name="percent"></param>
        private void UploadProcess(Action<int> percent)
        {
            percent(0);
            clsResultOpr resultBll = new clsResultOpr();
            DataTable dt = new DataTable("Results");
            try
            {
                sbWhere.Append(" AND A.SysCode<>'' AND A.CheckNo<>'' AND A.FoodCode<>''");
                dt = resultBll.GetUploadDataTable(sbWhere.ToString(), "A.SysCode", ShareOption.ApplicationTag);
                sbWhere.Length = 0;
                percent(5);
                if (dt == null)
                {
                    percent(100);
                    return;
                }
                string currentUser = CurrentUser.GetInstance().UserInfo.Name;
                string checkUnitName = string.Empty;
                string tag = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region ����Ϊ��ʱ
                    object obj = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj = dt.Rows[i]["SampleCode"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SampleCode"] = tag;
                        }

                        obj = dt.Rows[i]["CheckedCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckedCompany"] = tag;
                        }
                        obj = dt.Rows[i]["CheckedCompanyInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckedCompanyInfo"] = tag;
                        }

                        obj = dt.Rows[i]["CheckedComDis"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckedComDis"] = tag;
                        }
                        obj = dt.Rows[i]["CheckPlace"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckPlace"] = tag;
                        }
                        obj = dt.Rows[i]["FoodName"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["FoodName"] = tag;
                        }
                        obj = dt.Rows[i]["SentCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SentCompany"] = tag;
                        }
                        obj = dt.Rows[i]["Provider"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Provider"] = tag;
                        }
                        obj = dt.Rows[i]["ProduceDate"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["ProduceDate"] = DBNull.Value;
                        }
                        obj = dt.Rows[i]["ProduceCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["ProduceCompany"] = tag;
                        }
                        obj = dt.Rows[i]["ProducePlace"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["ProducePlace"] = tag;
                        }

                        dt.Rows[i]["UpLoader"] = currentUser;
                        obj = dt.Rows[i]["ProducePlaceInfo"];
                        if (obj != null && (obj.ToString().IndexOf("null") >= 0))
                        {
                            dt.Rows[i]["ProducePlaceInfo"] = DBNull.Value;
                        }
                        else
                        {
                            dt.Rows[i]["ProducePlaceInfo"] = obj.ToString().TrimEnd('/');
                        }

                        obj = dt.Rows[i]["Unit"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Unit"] = tag;
                        }
                        obj = dt.Rows[i]["SampleUnit"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SampleUnit"] = tag;
                        }
                        obj = dt.Rows[i]["SampleModel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SampleModel"] = tag;
                        }
                        obj = dt.Rows[i]["SampleLevel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SampleLevel"] = tag;
                        }
                        obj = dt.Rows[i]["SampleState"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["SampleState"] = tag;
                        }
                        obj = dt.Rows[i]["Standard"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Standard"] = tag;
                        }
                        obj = dt.Rows[i]["CheckMachine"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckMachine"] = tag;
                        }
                        obj = dt.Rows[i]["CheckMachineModel"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckMachineModel"] = tag;
                        }
                        obj = dt.Rows[i]["MachineCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["MachineCompany"] = tag;
                        }

                        obj = dt.Rows[i]["CheckValueInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckValueInfo"] = tag;
                        }

                        obj = dt.Rows[i]["StandValue"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["StandValue"] = tag;
                        }
                        obj = dt.Rows[i]["Result"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Result"] = tag;
                        }
                        obj = dt.Rows[i]["ResultInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["ResultInfo"] = tag;
                        }
                        obj = dt.Rows[i]["UpperCompany"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["UpperCompany"] = tag;
                        }

                        obj = dt.Rows[i]["CheckType"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckType"] = tag;
                        }
                        obj = dt.Rows[i]["CheckUnitName"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckUnitName"] = tag;
                        }
                        obj = dt.Rows[i]["CheckUnitInfo"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckUnitInfo"] = tag;
                        }
                        obj = dt.Rows[i]["Checker"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Checker"] = tag;
                        }
                        obj = dt.Rows[i]["Assessor"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Assessor"] = tag;
                        }
                        obj = dt.Rows[i]["Organizer"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Organizer"] = tag;
                        }

                        obj = dt.Rows[i]["Remark"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Remark"] = tag;
                        }
                        obj = dt.Rows[i]["CheckPlanCode"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckPlanCode"] = tag;
                        }
                        obj = dt.Rows[i]["CheckederVal"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckederVal"] = tag;
                        }
                        obj = dt.Rows[i]["IsSentCheck"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["IsSentCheck"] = tag;
                        }
                        obj = dt.Rows[i]["CheckederRemark"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["CheckederRemark"] = tag;
                        }

                        obj = dt.Rows[i]["Notes"];
                        if (obj == null || obj == DBNull.Value)
                        {
                            dt.Rows[i]["Notes"] = tag;
                        }
                    }
                    #endregion
                    percent(7);
                    checkUnitName = dt.Rows[0]["CheckUnitName"].ToString();
                    DataSet dst = new DataSet("UpdateResult");
                    dst.Tables.Add(dt.Copy());
                    percent(10);
                    if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
                    {
                        localhost.DataSyncService ws = new localhost.DataSyncService();
                        //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                        ws.Url = ShareOption.SysServerIP;
                        string ret = ws.SetDataDriver(dst.GetXml(), ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), checkUnitName, 3);//�Ӽ�������
                        percent(70);
                        if (ret.IndexOf("errofInfo") >= 0)//���ڴ���
                        {
                            MessageBox.Show("�ϴ�����ʧ�� " + ret);
                            percent(100);
                            return;
                        }
                        clsResult model = null;
                        string user = CurrentUser.GetInstance().UserInfo.UserCode;
                        string err = string.Empty;
                        string reSendTag = "��";
                        int len = 0;
                        int ind = ret.IndexOf("|");
                        if (ind >= 0)//����Ǵ��ݲ������� ���ݸ�ʽ�磺 2|29393939,112030283  2��ʾ�Ѿ��ɹ��������������ʾδ�ɹ��ı����"��"����
                        {
                            string first = ret.Substring(0, ind);
                            if (first.Trim() == "0")
                            {
                                MessageBox.Show("�ϴ�����ʧ�� ");
                                percent(100);
                                return;
                            }
                            string retTemp = ret.Substring(ind + 1, ret.Length - ind - 1);
                            string[] tArray = retTemp.Split(',');
                            len = tArray.Length;
                            System.Collections.Hashtable htbl = new System.Collections.Hashtable();
                            if (len > 0)
                            {
                                for (int i = 0; i < len; i++)//���ɹ��ı��
                                {
                                    htbl.Add(tArray[i], "0");
                                }
                                string temp = string.Empty;
                                int j = 0;
                                int k = (dt.Rows.Count / 30 + 1);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    temp = dt.Rows[i]["SysCode"].ToString();
                                    if (htbl[temp] != null && htbl[temp].ToString() == "0")
                                    {
                                        continue;
                                    }
                                    model = new clsResult();
                                    model.SysCode = temp;
                                    model.IsSended = true;
                                    reSendTag = dt.Rows[i]["IsReSended"].ToString();
                                    if (reSendTag == "��")
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
                                MessageBox.Show("���ݲ����ϴ��ɹ������ϴ�" + j + "����¼");
                            }
                        }
                        else //ȫ���ɹ�
                        {
                            len = Convert.ToInt32(ret);
                            if (len <= 0)
                            {
                                percent(100);
                                MessageBox.Show("û�з��ϵ����ݼ�¼");
                                return;
                            }
                            int k = len / 30 + 1;
                            for (int i = 0; i < len; i++)
                            {
                                model = new clsResult();
                                model.SysCode = dt.Rows[i]["SysCode"].ToString();
                                model.IsSended = true;
                                reSendTag = dt.Rows[i]["IsReSended"].ToString();
                                if (reSendTag == "��")
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
                            MessageBox.Show("�ϴ����ݳɹ������ϴ�" + ret + "����¼");//,��ʱ:"+ts.TotalMilliseconds);
                        }
                        //�����ϴ��ı�־λ���ϴ�ʱ��
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("�ϴ�����ʧ�ܣ�ʧ��ԭ��" + e.Message);
            }
            finally 
            {
                percent(100);
            }
        }

        private void backgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            //if (e.BackGroundException == null)
            //{
            //    groupBox1.Enabled = true;
            //    Cursor = Cursors.Default;
            //}
            //else
            //{
            //    MessageBox.Show("�쳣:" + e.BackGroundException.Message);
            //}
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
        {

        }

    }
}
