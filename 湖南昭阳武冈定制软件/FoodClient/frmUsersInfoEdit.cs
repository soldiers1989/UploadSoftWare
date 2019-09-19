using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;

namespace FoodClient
{
	/// <summary>
	/// frmUsersInfoEdit ��ժҪ˵����
	/// </summary>
	public class frmUsersInfoEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label9;
		public System.Windows.Forms.CheckBox chkAdmin;
		public System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
        public C1.Win.C1List.C1Combo cmbUnit;
        private System.Windows.Forms.TextBox txtComfire;
        private System.Windows.Forms.Label label1;

        private clsUserInfo userModel;
		private readonly clsUserInfoOpr userInfoBll;
		private string codeValue;
        private static string oldcode = string.Empty;

        public frmUsersInfoEdit()
        {
            InitializeComponent();
            userInfoBll = new clsUserInfoOpr();
        }

		/// <summary>
		/// ������������ʹ�õ���Դ��
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsersInfoEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUnit = new C1.Win.C1List.C1Combo();
            this.txtComfire = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(144, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(28, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "ϵͳ���룺";
            this.label9.Visible = false;
            // 
            // chkAdmin
            // 
            this.chkAdmin.Location = new System.Drawing.Point(112, 176);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(88, 24);
            this.chkAdmin.TabIndex = 7;
            this.chkAdmin.Text = "ϵͳ����Ա";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(208, 176);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 8;
            this.chkLock.Text = "ͣ��";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 144);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(186, 21);
            this.txtRemark.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(112, 66);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(186, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 41);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(112, 16);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(186, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(94, 211);
            this.txtSysID.MaxLength = 2;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(40, 21);
            this.txtSysID.TabIndex = 12;
            this.txtSysID.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 19;
            this.label6.Text = "��ע��";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 16;
            this.label5.Text = "��λ��";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 15;
            this.label4.Text = "��¼���룺";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "��ʾ���ƣ�";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "��¼����";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnit
            // 
            this.cmbUnit.AddItemSeparator = ';';
            this.cmbUnit.Caption = "";
            this.cmbUnit.CaptionHeight = 17;
            this.cmbUnit.CaptionStyle = style1;
            this.cmbUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbUnit.ColumnCaptionHeight = 18;
            this.cmbUnit.ColumnFooterHeight = 18;
            this.cmbUnit.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbUnit.ContentHeight = 16;
            this.cmbUnit.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbUnit.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbUnit.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUnit.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbUnit.EditorHeight = 16;
            this.cmbUnit.EvenRowStyle = style2;
            this.cmbUnit.FooterStyle = style3;
            this.cmbUnit.GapHeight = 2;
            this.cmbUnit.HeadingStyle = style4;
            this.cmbUnit.HighLightRowStyle = style5;
            this.cmbUnit.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbUnit.Images"))));
            this.cmbUnit.ItemHeight = 15;
            this.cmbUnit.Location = new System.Drawing.Point(112, 120);
            this.cmbUnit.MatchEntryTimeout = ((long)(2000));
            this.cmbUnit.MaxDropDownItems = ((short)(5));
            this.cmbUnit.MaxLength = 50;
            this.cmbUnit.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.OddRowStyle = style6;
            this.cmbUnit.PartialRightColumn = false;
            this.cmbUnit.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbUnit.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbUnit.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbUnit.SelectedStyle = style7;
            this.cmbUnit.Size = new System.Drawing.Size(186, 22);
            this.cmbUnit.Style = style8;
            this.cmbUnit.TabIndex = 3;
            this.cmbUnit.PropBag = resources.GetString("cmbUnit.PropBag");
            // 
            // txtComfire
            // 
            this.txtComfire.Location = new System.Drawing.Point(112, 92);
            this.txtComfire.MaxLength = 50;
            this.txtComfire.Name = "txtComfire";
            this.txtComfire.PasswordChar = '*';
            this.txtComfire.Size = new System.Drawing.Size(186, 21);
            this.txtComfire.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 21;
            this.label1.Text = "ȷ�����룺";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmUsersInfoEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(320, 259);
            this.Controls.Add(this.txtComfire);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkAdmin);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmUsersInfoEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "�û���Ϣ�޸�";
            this.Load += new System.EventHandler(this.frmUsersInfoEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUsersInfoEdit_Load(object sender, System.EventArgs e)
        {
            if (this.Tag.Equals("ADD"))
            {
                this.Text = "�û�����";
                this.btnOK.Text = "����";
            }
            else
            {
                this.Text = "�û��޸�";
                this.btnOK.Text = "����";
            }

            clsUserUnitOpr unitBll = new clsUserUnitOpr();
            DataTable dt = unitBll.GetAsDataTable("", "SysCode", 1);
            this.cmbUnit.DataSource = dt.DataSet;
            this.cmbUnit.DataMember = "UserUnit";
            this.cmbUnit.DisplayMember = "FullName";
            this.cmbUnit.ValueMember = "SysCode";
            this.cmbUnit.Columns["StdCode"].Caption = "���";
            this.cmbUnit.Columns["FullName"].Caption = "��λ";
            this.cmbUnit.Columns["SysCode"].Caption = "ϵͳ���";

            //***yzh2006-02-22begin***
            //this.cmbUnit.LeftCol=1;
            this.cmbUnit.ColumnWidth = this.cmbUnit.Width;
            //***yzh2006-02-22end***

            this.cmbUnit.AllowColMove = false;
            this.cmbUnit.HScrollBar.Style = C1.Win.C1List.ScrollBarStyleEnum.None;
            this.cmbUnit.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended;

            if (!codeValue.Equals(""))//0001
            {
                this.cmbUnit.Text = clsUserUnitOpr.GetNameFromCode(codeValue);//��������
                this.cmbUnit.SelectedValue = codeValue;
            }
        }

        internal void setValue(clsUserInfo userModel)
        {
            oldcode = userModel.LoginID;
            codeValue = userModel.UnitCode;
            this.txtSysID.Text = userModel.UserCode;
            this.txtCode.Text = userModel.LoginID;
            this.txtName.Text = userModel.Name;
            this.txtPassword.Text = userModel.PassWord;
            this.txtComfire.Text = userModel.PassWord;
            //this.txtWebLoginID.Text = userModel.WebLoginID;
            //this.txtWebPassWord.Text = userModel.WebPassWord;
            this.txtComfire.Text = userModel.PassWord;
            this.txtRemark.Text = userModel.Remark;
            this.chkLock.Checked = userModel.IsLock;
            this.chkAdmin.Checked = userModel.IsAdmin;
        }

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //���������ֵ�Ƿ�Ϸ�
            //����������Ƿ�������
            if (this.txtCode.Text.Equals(""))
            {
                MessageBox.Show(this, "��¼����������!");
                this.txtCode.Focus();
                return;
            }
            if (this.txtName.Text.Equals(""))
            {
                MessageBox.Show(this, "��ʾ���Ʊ�������!");
                this.txtName.Focus();
                return;
            }
            if (this.txtPassword.Text.Equals(""))
            {
                MessageBox.Show(this, "��¼�����������!");
                this.txtPassword.Focus();
                return;
            }
            if (!this.txtPassword.Text.Equals(this.txtComfire.Text))
            {
                MessageBox.Show(this, "ȷ������������¼����һ��!��������������");
                this.txtComfire.Text = "";
                this.txtComfire.Focus();
                return;
            }
            if (this.cmbUnit.Text.Equals(""))
            {
                MessageBox.Show(this, "��λ��������!");
                this.cmbUnit.Focus();
                return;
            }

            //�Ƿ��ַ����
            Control posControl;
            if (RegularCheck.HaveSpecChar(this, out posControl))
            {
                MessageBox.Show(this, "�������зǷ��ַ�,����!");
                posControl.Focus();
                return;
            }

            //���ַ��ͼ��

            //�����Ψһ��

            if (!this.txtCode.Text.Trim().Equals(oldcode))
            {
                if (userInfoBll.ExistSameValue(this.txtCode.Text.Trim()))
                {
                    MessageBox.Show(this, "����ͬ���ĵ�¼��������������!");
                    this.txtCode.Focus();
                    return;
                }
            }

            //ȡֵ
            userModel = new clsUserInfo();
            userModel.UserCode = this.txtSysID.Text.Trim();
            userModel.LoginID = this.txtCode.Text.Trim();
            userModel.Name = this.txtName.Text.Trim();
            userModel.PassWord = this.txtPassword.Text.Trim();
            userModel.UnitCode = this.cmbUnit.SelectedValue.ToString();
         
            userModel.Remark = this.txtRemark.Text.Trim();
            userModel.IsLock = this.chkLock.Checked;
            userModel.IsAdmin = this.chkAdmin.Checked;

            //�������ֶ�û���õ���ע��2011-10-25
            //curObject.WebLoginID = this.txtWebLoginID.Text.Trim();
            //curObject.WebPassWord = this.txtWebPassWord.Text.Trim();

            //�����ݿ���в���
            string err = string.Empty;

            if (this.Tag.Equals("ADD"))
            {
                userInfoBll.Insert(userModel, out err);
            }
            else
            {
                userInfoBll.UpdatePart(userModel, out err);
            }
            if (!err.Equals(""))
            {
                MessageBox.Show(this, "���ݿ��������");
            }

            //�˳�
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
	}
}
