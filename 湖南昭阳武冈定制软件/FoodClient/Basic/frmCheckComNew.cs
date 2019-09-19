using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;
using System.Web.Security;

namespace FoodClient
{
    /// <summary>
    /// frmCheckComNew ��ժҪ˵����
    /// </summary>
    public class frmCheckComNew : System.Windows.Forms.Form
    {
        private C1.Win.C1List.C1Combo cmbDistrict;
        private System.Windows.Forms.TextBox txtLinkInfo;
        private System.Windows.Forms.TextBox txtLinkMan;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkReadOnly;
        private System.Windows.Forms.CheckBox chkLock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private C1.Win.C1List.C1Combo cmbCheckPointType;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtShortName;

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;
        private clsUserUnit model;
        private clsUserUnitOpr userUnitBll;

        private string checkedComSelectedValue = string.Empty;
        private Label lblCompanyId;
        private Button btnGetData;
        private Label lblNotes;
        private static DataSet dsRt = null;

        public frmCheckComNew()
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckComNew));
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
            this.cmbDistrict = new C1.Win.C1List.C1Combo();
            this.txtLinkInfo = new System.Windows.Forms.TextBox();
            this.txtLinkMan = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbCheckPointType = new C1.Win.C1List.C1Combo();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCompanyId = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.lblNotes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDistrict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckPointType)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.AddItemSeparator = ';';
            this.cmbDistrict.Caption = "";
            this.cmbDistrict.CaptionHeight = 17;
            this.cmbDistrict.CaptionStyle = style1;
            this.cmbDistrict.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbDistrict.ColumnCaptionHeight = 18;
            this.cmbDistrict.ColumnFooterHeight = 18;
            this.cmbDistrict.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbDistrict.ContentHeight = 16;
            this.cmbDistrict.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbDistrict.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbDistrict.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDistrict.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbDistrict.EditorHeight = 16;
            this.cmbDistrict.EvenRowStyle = style2;
            this.cmbDistrict.FooterStyle = style3;
            this.cmbDistrict.GapHeight = 2;
            this.cmbDistrict.HeadingStyle = style4;
            this.cmbDistrict.HighLightRowStyle = style5;
            this.cmbDistrict.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbDistrict.Images"))));
            this.cmbDistrict.ItemHeight = 15;
            this.cmbDistrict.Location = new System.Drawing.Point(103, 76);
            this.cmbDistrict.MatchEntryTimeout = ((long)(2000));
            this.cmbDistrict.MaxDropDownItems = ((short)(5));
            this.cmbDistrict.MaxLength = 32767;
            this.cmbDistrict.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.OddRowStyle = style6;
            this.cmbDistrict.PartialRightColumn = false;
            this.cmbDistrict.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbDistrict.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbDistrict.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbDistrict.SelectedStyle = style7;
            this.cmbDistrict.Size = new System.Drawing.Size(160, 22);
            this.cmbDistrict.Style = style8;
            this.cmbDistrict.TabIndex = 35;
            this.cmbDistrict.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.cmbDistrict_BeforeOpen);
            this.cmbDistrict.PropBag = resources.GetString("cmbDistrict.PropBag");
            // 
            // txtLinkInfo
            // 
            this.txtLinkInfo.Location = new System.Drawing.Point(592, 76);
            this.txtLinkInfo.Name = "txtLinkInfo";
            this.txtLinkInfo.Size = new System.Drawing.Size(122, 21);
            this.txtLinkInfo.TabIndex = 39;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Location = new System.Drawing.Point(448, 76);
            this.txtLinkMan.MaxLength = 50;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(97, 21);
            this.txtLinkMan.TabIndex = 38;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(103, 123);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(611, 65);
            this.txtRemark.TabIndex = 41;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(448, 9);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(266, 21);
            this.txtFullName.TabIndex = 31;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(103, 8);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(160, 21);
            this.txtCode.TabIndex = 30;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(136, 248);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(80, 21);
            this.txtSysID.TabIndex = 46;
            this.txtSysID.Visible = false;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(544, 77);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 21);
            this.label25.TabIndex = 57;
            this.label25.Text = "�绰��";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(391, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 21);
            this.label12.TabIndex = 56;
            this.label12.Text = "�����ˣ�";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(12, 76);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(91, 21);
            this.lblParent.TabIndex = 53;
            this.lblParent.Text = "����������";
            this.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(48, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 24);
            this.label9.TabIndex = 45;
            this.label9.Text = "ϵͳ���룺";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(248, 240);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(48, 24);
            this.chkReadOnly.TabIndex = 47;
            this.chkReadOnly.Text = "ֻ��";
            this.chkReadOnly.Visible = false;
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(479, 198);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(64, 24);
            this.chkLock.TabIndex = 42;
            this.chkLock.Text = "ͣ��";
            this.chkLock.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 59;
            this.label1.Text = "��ע��";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 52;
            this.label3.Text = "�������ͣ�";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(328, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(120, 21);
            this.lblName.TabIndex = 49;
            this.lblName.Text = "�������ƣ�";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(23, 8);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(80, 21);
            this.lblCode.TabIndex = 48;
            this.lblCode.Text = "�����ţ�";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(639, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(551, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbCheckPointType
            // 
            this.cmbCheckPointType.AddItemSeparator = ';';
            this.cmbCheckPointType.Caption = "";
            this.cmbCheckPointType.CaptionHeight = 17;
            this.cmbCheckPointType.CaptionStyle = style9;
            this.cmbCheckPointType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCheckPointType.ColumnCaptionHeight = 18;
            this.cmbCheckPointType.ColumnFooterHeight = 18;
            this.cmbCheckPointType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCheckPointType.ContentHeight = 16;
            this.cmbCheckPointType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCheckPointType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCheckPointType.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheckPointType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCheckPointType.EditorHeight = 16;
            this.cmbCheckPointType.EvenRowStyle = style10;
            this.cmbCheckPointType.FooterStyle = style11;
            this.cmbCheckPointType.GapHeight = 2;
            this.cmbCheckPointType.HeadingStyle = style12;
            this.cmbCheckPointType.HighLightRowStyle = style13;
            this.cmbCheckPointType.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCheckPointType.Images"))));
            this.cmbCheckPointType.ItemHeight = 15;
            this.cmbCheckPointType.Location = new System.Drawing.Point(103, 41);
            this.cmbCheckPointType.MatchEntryTimeout = ((long)(2000));
            this.cmbCheckPointType.MaxDropDownItems = ((short)(5));
            this.cmbCheckPointType.MaxLength = 32767;
            this.cmbCheckPointType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCheckPointType.Name = "cmbCheckPointType";
            this.cmbCheckPointType.OddRowStyle = style14;
            this.cmbCheckPointType.PartialRightColumn = false;
            this.cmbCheckPointType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCheckPointType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCheckPointType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCheckPointType.SelectedStyle = style15;
            this.cmbCheckPointType.Size = new System.Drawing.Size(160, 22);
            this.cmbCheckPointType.Style = style16;
            this.cmbCheckPointType.TabIndex = 60;
            this.cmbCheckPointType.SelectedValueChanged += new System.EventHandler(this.cmbCheckPointType_TextChanged);
            this.cmbCheckPointType.PropBag = resources.GetString("cmbCheckPointType.PropBag");
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(448, 42);
            this.txtShortName.MaxLength = 50;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(266, 21);
            this.txtShortName.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(368, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 21);
            this.label2.TabIndex = 62;
            this.label2.Text = "���������⣺";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCompanyId
            // 
            this.lblCompanyId.AutoSize = true;
            this.lblCompanyId.Location = new System.Drawing.Point(44, 204);
            this.lblCompanyId.Name = "lblCompanyId";
            this.lblCompanyId.Size = new System.Drawing.Size(53, 12);
            this.lblCompanyId.TabIndex = 63;
            this.lblCompanyId.Text = "��λ���";
            this.lblCompanyId.Visible = false;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(269, 41);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 64;
            this.btnGetData.Text = "����ͬ��";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(269, 81);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(101, 12);
            this.lblNotes.TabIndex = 65;
            this.lblNotes.Text = "������������ͬ��";
            // 
            // frmCheckComNew
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(736, 246);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.lblCompanyId);
            this.Controls.Add(this.txtShortName);
            this.Controls.Add(this.txtLinkInfo);
            this.Controls.Add(this.txtLinkMan);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCheckPointType);
            this.Controls.Add(this.cmbDistrict);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCheckComNew";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "��������";
            this.Load += new System.EventHandler(this.frmCheckComNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbDistrict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckPointType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

      
        /// <summary>
        /// �������ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheckComNew_Load(object sender, System.EventArgs e)
        {
            lblCompanyId.Text = "";
            lblNotes.Visible = !ShareOption.IsDataLink;
            btnGetData.Visible = !ShareOption.IsDataLink;

            if (!ShareOption.IsDataLink)
            {
                if (ShareOption.SysServerIP.Equals("") || ShareOption.SysServerID.Equals(""))
                {
                    MessageBox.Show(this, "���ȵ�ѡ��˵������÷�������ַ���¼ID��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnOK.Enabled = false;
                    return;
                }
            }
            else //if (ShareOption.IsDataLink) //������
            {
                DataTable dtCheckComType = null;
                clsCheckComTypeOpr checkComTypeBll = new clsCheckComTypeOpr();
                dtCheckComType = checkComTypeBll.GetAsDataTable("IsReadOnly=True And (VerType='" + ShareOption.SystemVersion + "' OR VerType='ȫ��')", "ID", 1);

                cmbCheckPointType.DataSource = dtCheckComType.DataSet;
                cmbCheckPointType.DataMember = "CheckComType";
                cmbCheckPointType.DisplayMember = "TypeName";
                cmbCheckPointType.ValueMember = "TypeName";
                cmbCheckPointType.Columns["TypeName"].Caption = "��������";
                cmbCheckPointType.ColumnWidth = cmbCheckPointType.Width;
                cmbCheckPointType.AllowColMove = false;
                cmbCheckPointType.HScrollBar.Style = C1.Win.C1List.ScrollBarStyleEnum.None;
                cmbCheckPointType.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended;
            }

            //��������
            string codeValue = string.Empty;
            string checkComType = string.Empty;
            string tempCode = ShareOption.DefaultUserUnitCode;
            userUnitBll = new clsUserUnitOpr();
            txtSysID.Text = tempCode;

            try
            {
                DataTable dtUserUnit = userUnitBll.GetAsDataTable(string.Format("A.SysCode='{0}'", tempCode), "", 0);//0001

                if (dtUserUnit != null && dtUserUnit.Rows.Count > 0)
                {
                    txtCode.Text = dtUserUnit.Rows[0]["STDCODE"].ToString();//1
                    txtFullName.Text = dtUserUnit.Rows[0]["FULLNAME"].ToString();//2
                    txtShortName.Text = dtUserUnit.Rows[0]["SHORTNAME"].ToString();//3

                    checkComType = dtUserUnit.Rows[0]["SHORTCUT"].ToString();//5
                    codeValue = dtUserUnit.Rows[0]["DISTRICTCODE"].ToString();//6

                    txtLinkMan.Text = dtUserUnit.Rows[0]["LINKMAN"].ToString();
                    txtLinkInfo.Text = dtUserUnit.Rows[0]["LINKINFO"].ToString();
                    txtRemark.Text = dtUserUnit.Rows[0]["REMARK"].ToString();
                    lblCompanyId.Text = dtUserUnit.Rows[0]["CompanyId"].ToString();

                    chkLock.Checked = Convert.ToBoolean(dtUserUnit.Rows[0]["ISLOCK"]);
                    chkReadOnly.Checked = Convert.ToBoolean(dtUserUnit.Rows[0]["ISREADONLY"].ToString());
                }

                if (!codeValue.Equals(""))
                {
                    cmbDistrict.Text = clsDistrictOpr.NameFromCode(codeValue);
                    cmbDistrict.SelectedValue = codeValue;
                    checkedComSelectedValue = codeValue;
                }

                if (!checkComType.Equals(""))
                {
                    cmbCheckPointType.Text = checkComType;
                    //cmbCheckPointType.SelectedValue = checkComType;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ϵͳ����:" + ex.Message);
                Close();
            }
        }

        /// <summary>
        /// ȷ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //���������ֵ�Ƿ�Ϸ�
            //����������Ƿ�������
            if (txtFullName.Text.Equals(""))
            {
                MessageBox.Show(this, "�������Ʊ�������!");
                txtFullName.Focus();
                Cursor = Cursors.Default;
                return;
            }
            if (cmbCheckPointType.Text.Equals(""))
            {
                MessageBox.Show(this, "�������ͱ�������!");
                cmbCheckPointType.Focus();
                Cursor = Cursors.Default;
                return;
            }
            if (checkedComSelectedValue.Equals(""))
            {
                MessageBox.Show(this, "����������λ����ѡ��!");
                cmbDistrict.Text = "";
                cmbDistrict.Focus();
                Cursor = Cursors.Default;
                return;
            }

            //�Ƿ��ַ����
            //Control posControl;
            //if (RegularCheck.HaveSpecChar(this, out posControl))
            //{
            //    MessageBox.Show(this, "�������зǷ��ַ�,����!");
            //    posControl.Focus();
            //    Cursor = Cursors.Default;
            //    return;
            //}

            //���ַ��ͼ��
            string comKind = string.Empty;//��λ����
            string swhere = string.Empty;
            string pointType = cmbCheckPointType.Text;//��λ����
            string msg = string.Empty;
            try
            {
                if (!ShareOption.IsDataLink)
                { 
                    DataRow[] checkTypeDr =null;
                    DataRow[] userUnitDr = null;
                    if (dsRt != null)
                    {
                        //�����������ڱ��쵥λ��ʱ�򣬼��Fss_Company�Ƿ������ͬ���룬���ƣ���λ���ʣ��������һ�²���
                        checkTypeDr = dsRt.Tables["Fss_CheckComType"].Select(string.Format("TypeName='{0}'", pointType));
                    }

                    if (checkTypeDr!=null&&checkTypeDr.Length == 1)
                    {
                        comKind = checkTypeDr[0]["ComKind"].ToString();
                    }
                    swhere = string.Format(" StdCode='{0}' And FullName='{1}' And DistrictCode='{2}' And ShortCut='{3}'", txtCode.Text.Trim(), txtFullName.Text.Trim(), checkedComSelectedValue, pointType);

                    if (dsRt != null)
                    {
                        userUnitDr = dsRt.Tables["Com_UserUnit"].Select(swhere);
                    }

                    if (!comKind.Equals(""))
                    {
                        msg = "����" + pointType + "�ļ�����Ϣ�������ڱ��쵥λ�н���������ͬ��š���λ���ơ���λ���ʺ�������֯����,����!";
                    }
                    else
                    {
                        msg = "����" + pointType + "�ļ�����Ϣ����������֯����ά���н���������ͬ��š�����������֯����Ҳ���뱣��һ��,����!";
                    }

                    if (userUnitDr!=null&&userUnitDr.Length <= 0)
                    {
                        MessageBox.Show(this, msg);
                        Cursor = Cursors.Default;
                        return;
                    }
                }

                //ȡֵ
                model = new clsUserUnit();
                model.SysCode = txtSysID.Text.Trim();
                model.StdCode = txtCode.Text.Trim();
                model.FullName = txtFullName.Text.Trim();
                model.ShortName = txtShortName.Text.Trim();
                model.ShortCut = cmbCheckPointType.Text.Trim();
                model.DistrictCode = checkedComSelectedValue;
                model.LinkMan = txtLinkMan.Text.Trim();
                model.LinkInfo = txtLinkInfo.Text.Trim();
                model.CompanyID = lblCompanyId.Text;
                //model.WWWInfo=clsFss_CheckComTypeOpr.ValueFromName(ConnectionStr,"ComKind",cmbCheckPointType.Text.Trim());
                //model.DisplayName=txtDisplayName.Text.Trim();
                //model.Address=txtAddress.Text.Trim();
                //model.PostCode=txtPostCode.Text.Trim();
                model.WWWInfo = comKind;
                model.Remark = txtRemark.Text.Trim();
                model.IsLock = chkLock.Checked;
                model.IsReadOnly = chkReadOnly.Checked;

                //�����ݿ���в���
                string err = string.Empty;
                userUnitBll = new clsUserUnitOpr();
                bool bExist = clsUserUnitOpr.ExistCode(ShareOption.DefaultUserUnitCode);
                if (!bExist)
                {
                    userUnitBll.Insert(model, out err);
                }
                else
                {
                    userUnitBll.UpdatePart(model, out err);
                }

                if (!err.Equals(""))
                {
                    MessageBox.Show(this, "���ݿ��������" + err);
                }
                Cursor = Cursors.Default;
                //�˳�
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void cmbCheckPointType_TextChanged(object sender, System.EventArgs e)
        {
            DataTable dtbl = null;
            DataRow[] comTypedr = null;
            string checkPoint = string.Empty;
            string code = txtCode.Text;//������
            if (!ShareOption.IsDataLink)//�����
            {
                if (dsRt != null)
                {
                    comTypedr = dsRt.Tables["Fss_CheckComType"].Select("TypeName='" + checkPoint + "'");

                    //�������ҵ��
                    if (ShareOption.SystemVersion.Equals(ShareOption.EnterpriseVersion))
                    {
                        DataRow[] userUnitDr = null;

                        userUnitDr = dsRt.Tables["Com_UserUnit"].Select(string.Format("StdCode='{0}'", code));
                        if (userUnitDr != null && userUnitDr.Length > 0)
                        {
                            lblCompanyId.Text = userUnitDr[0]["CompanyId"].ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("��������ͬ��");
                    return;
                }
            }
            else
            {
                clsCheckComTypeOpr checkComTypeBll = new clsCheckComTypeOpr();
                dtbl = checkComTypeBll.GetAsDataTable(string.Format("IsReadOnly=True AND (VerType='{0}' OR VerType='ȫ��')", ShareOption.SystemVersion), "ID", 2);

                comTypedr = dtbl.Select(string.Format("TypeName='{0}'", checkPoint));
            }

            if (comTypedr != null && comTypedr.Length > 0)
            {
                lblName.Text = comTypedr[0]["NameCall"].ToString() + "��";
                lblParent.Text = comTypedr[0]["AreaCall"].ToString() + "��";
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDistrict_BeforeOpen(object sender, CancelEventArgs e)
        {
            DataTable dt = null;
            if (!ShareOption.IsDataLink)//����������
            {
                if (dsRt != null)
                {
                    dt = dsRt.Tables["Com_District"];
                }
            }
            else
            {
                clsDistrictOpr unitOpr = new clsDistrictOpr();
                dt = unitOpr.GetAsDataTable("IsReadOnly=True And IsLock=False", "SysCode", 1);
            }
            frmDistrictSelect frm = new frmDistrictSelect(checkedComSelectedValue, dt);
            frm.ShowDialog(this);

            if (frm.DialogResult == DialogResult.OK)
            {
                checkedComSelectedValue = frm.sNodeTag;
                cmbDistrict.Text = frm.sNodeName;

                //������޸�Ϊ����������⣬����ȥ�� 2011-10-24
                //if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))
                //{
                //    txtCode.Text = frm.sNodeStd;
                //}
            }
            //else
            //{
            //    checkedComSelectedValue = "";
            //    cmbDistrict.Text = "";
            //    txtCode.Text = "";
            //}
            e.Cancel = true;
        }

        /// <summary>
        /// ����ͬ��,ֻ�������Ż����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            dsRt = new DataSet("UpdateRecord");
           
            string err;
            try
            {
                if (!ShareOption.IsDataLink)//����������
                {
                    if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
                    {
                        localhost.DataSyncService ws = new localhost.DataSyncService();
                        //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                        ws.Url = ShareOption.SysServerIP;
                        string rt = ws.GetPartDataDriver(ShareOption.SystemVersion, ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString());
                        if (rt.Substring(0, 10).Equals("errorInfo:"))
                        {
                            MessageBox.Show(this, "�޷���������Զ�̷�����������ԭ��Ϊ��" + rt.Substring(10, rt.Length - 10));
                            Cursor = Cursors.Default;
                            Close();
                        }
                        else
                        {
                            dsRt.ReadXml(new System.IO.StringReader(rt));
                        }
                    }
                    else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
                    {
                      ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                       // DY.WebService.ForNet.DataDriver ws = new DY.WebService.ForNet.DataDriver();
                        ws.Url = ShareOption.SysServerIP;
                        dsRt = ws.GetPartDataDriver(ShareOption.SystemVersion, ShareOption.SysServerID, ShareOption.SysServerPass, out err);
                        if (!err.Equals(""))
                        {
                            MessageBox.Show(this, "�޷���������Զ�̷�����������ԭ��Ϊ��" + err);
                            Cursor = Cursors.Default;
                            Close();
                        }
                    }
                    DataTable dtCheckComType = null;
                    dtCheckComType = dsRt.Tables["Fss_CheckComType"];
                    if (dtCheckComType != null)
                    {
                        cmbCheckPointType.DataSource = dtCheckComType.DataSet;
                        cmbCheckPointType.DataMember = "Fss_CheckComType";
                        cmbCheckPointType.DisplayMember = "TypeName";
                        cmbCheckPointType.ValueMember = "TypeName";
                        cmbCheckPointType.Columns["TypeName"].Caption = "��������";
                        cmbCheckPointType.ColumnWidth = cmbCheckPointType.Width;
                        cmbCheckPointType.AllowColMove = false;
                        cmbCheckPointType.HScrollBar.Style = C1.Win.C1List.ScrollBarStyleEnum.None;
                        cmbCheckPointType.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended;
                    }
                    else
                    {
                        cmbCheckPointType.Text = string.Empty;
                        MessageBox.Show(this, "û����ؼ������ͣ����ڼ��ƽ̨���ü������Ͳ�ͬ��!");
                        Cursor = Cursors.Default;
                        return;
                    }

                    //��������
                    //DataTable dt = dsRt.Tables["Com_District"];
                    //cmbDistrict.DataSource = dt.DataSet;
                    //cmbDistrict.DataMember = "Com_District";
                    //cmbDistrict.DisplayMember = "Name";
                    //cmbDistrict.ValueMember = "SysCode";
                    //cmbDistrict.Columns["StdCode"].Caption = "���";
                    //cmbDistrict.Columns["Name"].Caption = "����";
                    //cmbDistrict.Columns["SysCode"].Caption = "ϵͳ���";
                    //cmbDistrict.ColumnWidth = cmbDistrict.Width;
                    //cmbDistrict.AllowColMove = false;
                    //cmbDistrict.HScrollBar.Style = C1.Win.C1List.ScrollBarStyleEnum.None;
                    //cmbDistrict.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended;
                }
                MessageBox.Show(this,"����ͬ���ɹ��������úø�������!");
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(this, ex.Message);

                //MessageBox.Show(this, "�޷���������Զ�̷����������Բ��ܶ�ȡ��֯�������ݣ��뵽ϵͳ����--ѡ�������������������������������Ƿ����Ӻã�");
            }
            Cursor = Cursors.Default;
        }
    }
}
