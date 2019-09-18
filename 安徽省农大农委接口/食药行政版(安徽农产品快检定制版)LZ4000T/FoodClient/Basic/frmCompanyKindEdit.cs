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
	/// frmCompanyKindEdit ��ժҪ˵����
	/// </summary>
	public class frmCompanyKindEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkLock;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtSysID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private C1.Win.C1List.C1Combo cmbCompanyProperty;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsCompanyKind curObject;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOldName;
		private clsCompanyKindOpr curObjectOpr;

		public frmCompanyKindEdit()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyKindEdit));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.label9 = new System.Windows.Forms.Label();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCompanyProperty = new C1.Win.C1List.C1Combo();
            this.label39 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOldName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(24, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 24);
            this.label9.TabIndex = 7;
            this.label9.Text = "ϵͳ���룺";
            this.label9.Visible = false;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.Location = new System.Drawing.Point(32, 176);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(64, 24);
            this.chkReadOnly.TabIndex = 9;
            this.chkReadOnly.Text = "�����";
            // 
            // chkLock
            // 
            this.chkLock.Location = new System.Drawing.Point(88, 176);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 24);
            this.chkLock.TabIndex = 4;
            this.chkLock.Text = "ͣ��";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(144, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(120, 112);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(186, 56);
            this.txtRemark.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 48);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(120, 16);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(186, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtSysID
            // 
            this.txtSysID.Enabled = false;
            this.txtSysID.Location = new System.Drawing.Point(112, 216);
            this.txtSysID.MaxLength = 50;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(56, 21);
            this.txtSysID.TabIndex = 8;
            this.txtSysID.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "��ע��";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "��λ���ʣ�";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "��λ������ƣ�";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "��ţ�";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCompanyProperty
            // 
            this.cmbCompanyProperty.AddItemSeparator = ';';
            this.cmbCompanyProperty.Caption = "";
            this.cmbCompanyProperty.CaptionHeight = 17;
            this.cmbCompanyProperty.CaptionStyle = style1;
            this.cmbCompanyProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbCompanyProperty.ColumnCaptionHeight = 18;
            this.cmbCompanyProperty.ColumnFooterHeight = 18;
            this.cmbCompanyProperty.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cmbCompanyProperty.ContentHeight = 16;
            this.cmbCompanyProperty.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cmbCompanyProperty.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyProperty.EditorFont = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCompanyProperty.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCompanyProperty.EditorHeight = 16;
            this.cmbCompanyProperty.EvenRowStyle = style2;
            this.cmbCompanyProperty.FooterStyle = style3;
            this.cmbCompanyProperty.GapHeight = 2;
            this.cmbCompanyProperty.HeadingStyle = style4;
            this.cmbCompanyProperty.HighLightRowStyle = style5;
            this.cmbCompanyProperty.Images.Add(((System.Drawing.Image)(resources.GetObject("cmbCompanyProperty.Images"))));
            this.cmbCompanyProperty.ItemHeight = 15;
            this.cmbCompanyProperty.Location = new System.Drawing.Point(120, 80);
            this.cmbCompanyProperty.MatchEntryTimeout = ((long)(2000));
            this.cmbCompanyProperty.MaxDropDownItems = ((short)(5));
            this.cmbCompanyProperty.MaxLength = 10;
            this.cmbCompanyProperty.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cmbCompanyProperty.Name = "cmbCompanyProperty";
            this.cmbCompanyProperty.OddRowStyle = style6;
            this.cmbCompanyProperty.PartialRightColumn = false;
            this.cmbCompanyProperty.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cmbCompanyProperty.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cmbCompanyProperty.SelectedStyle = style7;
            this.cmbCompanyProperty.Size = new System.Drawing.Size(186, 22);
            this.cmbCompanyProperty.Style = style8;
            this.cmbCompanyProperty.TabIndex = 2;
            this.cmbCompanyProperty.PropBag = resources.GetString("cmbCompanyProperty.PropBag");
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(16, 47);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(8, 21);
            this.label39.TabIndex = 49;
            this.label39.Text = "*";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(38, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 21);
            this.label1.TabIndex = 50;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOldName
            // 
            this.txtOldName.Location = new System.Drawing.Point(32, 240);
            this.txtOldName.MaxLength = 50;
            this.txtOldName.Name = "txtOldName";
            this.txtOldName.Size = new System.Drawing.Size(96, 21);
            this.txtOldName.TabIndex = 51;
            this.txtOldName.Visible = false;
            // 
            // frmCompanyKindEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(328, 207);
            this.Controls.Add(this.txtOldName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.cmbCompanyProperty);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCompanyKindEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "��λ����޸�";
            this.Load += new System.EventHandler(this.frmCompanyKindEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyProperty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmCompanyKindEdit_Load(object sender, System.EventArgs e)
		{
			if(this.Tag.Equals("ADD"))
			{
				this.Text="��λ�������";
				this.btnOK.Text="����";
			}
			else
			{
				this.Text="��λ����޸�";
				this.btnOK.Text="����";
			}

			clsCompanyPropertyOpr unitOpr=new clsCompanyPropertyOpr();
			DataTable dt=unitOpr.GetAsDataTable("IsLock=0","CompanyProperty");
			this.cmbCompanyProperty.DataSource=dt.DataSet;
			this.cmbCompanyProperty.DataMember="CompanyProperty";
			this.cmbCompanyProperty.Columns["CompanyProperty"].Caption="��λ����";
			//this.cmbCompanyProperty.Text= "���쵥λ";
			//this.cmbCompanyProperty.Enabled=false;
		}

		internal void setValue(clsCompanyKind curObject)
		{
			this.txtSysID.Text=curObject.SysCode;
			this.txtCode.Text=curObject.StdCode;
			this.txtName.Text=curObject.Name;
			this.txtOldName.Text=curObject.Name;
			this.cmbCompanyProperty.Text=curObject.CompanyProperty;
			this.txtRemark.Text=curObject.Remark;
			this.chkLock.Checked=curObject.IsLock;
			this.chkReadOnly.Checked=curObject.IsReadOnly;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//���������ֵ�Ƿ�Ϸ�
			//����������Ƿ�������
			if(this.txtName.Text.Trim().Equals(""))
			{
				MessageBox.Show(this,"��λ������Ʊ�������!");
				this.txtName.Focus();
				return;
			}
			if(this.cmbCompanyProperty.Text.Equals(""))
			{
				MessageBox.Show(this,"��λ���ʱ�������!");
				this.cmbCompanyProperty.Focus();
				return;
			}

			if(this.txtName.Text.Trim()!=this.txtOldName.Text.Trim()&& clsCompanyKindOpr.ExistSameValue(this.txtName.Text.Trim()))
			{
				MessageBox.Show(this,"��λ��������Ѵ���!");
				this.txtName.Focus();
				return;
			}
			
			//�Ƿ��ַ����
			Control posControl;
			if(RegularCheck.HaveSpecChar(this,out posControl))
			{
				MessageBox.Show(this,"�������зǷ��ַ�,����!");
				posControl.Focus();
				return;
			}

			//���ַ��ͼ��


			//ȡֵ
			curObject=new clsCompanyKind();
			curObject.SysCode=this.txtSysID.Text.Trim();
			curObject.StdCode=this.txtCode.Text.Trim();
			curObject.Name=this.txtName.Text.Trim();
			curObject.CompanyProperty=this.cmbCompanyProperty.Text.Trim();
			curObject.Remark=this.txtRemark.Text.Trim();
			curObject.IsLock=this.chkLock.Checked;
			curObject.IsReadOnly=this.chkReadOnly.Checked;
			
			//�����ݿ���в���
			string sErr="";
			curObjectOpr=new clsCompanyKindOpr();
			if(this.Tag.Equals("ADD"))
			{
				curObjectOpr.Insert(curObject,out sErr);
			}
			else
			{
				curObjectOpr.UpdatePart(curObject,out sErr);
			}
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"���ݿ��������");
			}

			//�˳�
			this.DialogResult=DialogResult.OK;
			this.Close();
		}
	}
}
