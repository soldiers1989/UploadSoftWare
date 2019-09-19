using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmStockRecord ��ժҪ˵����
	/// </summary>
	public class frmStockRecord : System.Windows.Forms.Form
	{
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private C1.Win.C1Command.C1Command c1Command5;
		private C1.Win.C1Command.C1CommandDock c1CommandDock1;
		private C1.Win.C1Command.C1ToolBar c1ToolBar1;
		public C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1CommandLink c1CommandLink5;
		private C1.Win.C1Command.C1CommandLink c1CommandLink6;
		private System.Windows.Forms.StatusBar statusBar1;
		public C1.Win.C1Sizer.C1Sizer c1Sizer1;
		private System.Windows.Forms.TreeView treeView1;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		public C1.Win.C1Command.C1Command cmdAdd;
		private C1.Win.C1Command.C1Command cmdEdit;
		private C1.Win.C1Command.C1Command cmdDelete;
		private C1.Win.C1Command.C1Command cmdPrint;
		private C1.Win.C1Command.C1Command cmdExit;
	    private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolTip toolTip1;

		private clsStockRecord model;
		private readonly clsStockRecordOpr recordBll;
	

		public frmStockRecord()
		{
			InitializeComponent();	

			recordBll=new clsStockRecordOpr();
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            if (ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(1);
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockRecord));
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.cmdAdd = new C1.Win.C1Command.C1Command();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.cmdAdd);
            this.c1CommandHolder1.Commands.Add(this.cmdEdit);
            this.c1CommandHolder1.Commands.Add(this.cmdDelete);
            this.c1CommandHolder1.Commands.Add(this.c1Command5);
            this.c1CommandHolder1.Commands.Add(this.cmdPrint);
            this.c1CommandHolder1.Commands.Add(this.cmdExit);
            this.c1CommandHolder1.Owner = this;
            this.c1CommandHolder1.SmoothImages = false;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.cmdAdd.Text = "����";
            this.cmdAdd.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd_Click);
            this.cmdAdd.Select += new System.EventHandler(this.cmdAdd_Select);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.cmdEdit.Text = "�޸�";
            this.cmdEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdEdit_Click);
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "ɾ��";
            this.cmdDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDelete_Click);
            this.cmdDelete.Select += new System.EventHandler(this.cmdDelete_Select);
            // 
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "��ӡ";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.cmdExit.Text = "�˳�";
            this.cmdExit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdExit_Click);
            this.cmdExit.Select += new System.EventHandler(this.cmdExit_Select);
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(620, 24);
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1,
            this.c1CommandLink2,
            this.c1CommandLink3,
            this.c1CommandLink4,
            this.c1CommandLink5,
            this.c1CommandLink6});
            this.c1ToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.ShowToolTips = false;
            this.c1ToolBar1.Size = new System.Drawing.Size(620, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdAdd;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.cmdEdit;
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.cmdDelete;
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.Command = this.cmdPrint;
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.Command = this.cmdExit;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 313);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(620, 25);
            this.statusBar1.TabIndex = 4;
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.AllowDrop = true;
            this.c1Sizer1.Controls.Add(this.c1FlexGrid1);
            this.c1Sizer1.Controls.Add(this.treeView1);
            this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Sizer1.GridDefinition = "97.2318339100346:False:False;\t20.9677419354839:True:False;77.0967741935484:False:" +
                "False;";
            this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Sizer1.Location = new System.Drawing.Point(0, 24);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(620, 289);
            this.c1Sizer1.TabIndex = 8;
            this.c1Sizer1.TabStop = false;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Location = new System.Drawing.Point(138, 4);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(478, 281);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(130, 281);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // frmStockRecord
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(620, 338);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.c1CommandDock1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStockRecord";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����̨��";
            this.Load += new System.EventHandler(this.frmStockRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (this.treeView1.Nodes.Count == 0)
            {
                MessageBox.Show(this, "����¼�뱻�쵥λ��");
                return;
            }

            model = new clsStockRecord();

            //��ȡһ���µ�ϵͳ���
            string checkunitcode = FrmMain.formMain.checkUnitCode;
            // CurrentUser.GetInstance().UserInfo.UnitCode;
            string curCode = clsUserUnitOpr.GetNameFromCode("StdCode", checkunitcode) + DateTime.Now.ToString("yyyyMM");
            string err = string.Empty;
            int max = recordBll.GetMaxNO(curCode, ShareOption.RecordCodeLen, out err);
            string syscode = curCode + (max + 1).ToString().PadLeft(ShareOption.RecordCodeLen, '0'); ;
            model.SysCode = syscode;

            if (ShareOption.SysStdCodeSame)
            {
                model.StdCode = model.SysCode;
            }
            else
            {
                model.StdCode = string.Empty;
            }

            string temp = this.treeView1.SelectedNode.Text.ToString();
            model.CompanyID = this.treeView1.SelectedNode.Tag.ToString();
            model.CompanyName = temp;
            model.DisplayName = clsCompanyOpr.DisplayNameFromCode(temp);
            model.InputDate = System.DateTime.Now;
            model.FoodID = "";
            model.FoodName = "";
            model.Model = "";
            model.InputNumber = 0;
            model.OutputNumber = 0;
            model.Unit = "";
            // model.ProduceDate = Convert.ToDateTime("1900-1-1");
            model.ExpirationDate = "";
            model.ProduceCompanyID = "";
            model.ProduceCompanyName = "";
            model.PrivoderID = "";
            model.PrivoderName = "";
            model.LinkInfo = "";
            model.LinkMan = "";
            model.CertificateType1 = "";
            model.CertificateType2 = "";
            model.CertificateType3 = "";
            model.CertificateType4 = "";
            model.CertificateType5 = "";
            model.CertificateType6 = "";
            model.CertificateType7 = "";
            model.CertificateType8 = "";
            model.CertificateType9 = "";
            model.CertificateInfo = "";
            model.MakeMan = "";
            model.Remark = "";

            frmStockRecordEdit frm = new frmStockRecordEdit();
            frm.setValue(model);
            frm.Tag = "ADD";
            DialogResult dr = frm.ShowDialog(this);

            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (this.treeView1.Nodes.Count == 0 || row <= 0)
            {
                MessageBox.Show(this, "����¼�뱻�쵥λ��ѡ���Ҫ�޸ĵļ�¼��");
                return;
            }
            if (this.c1FlexGrid1.Rows[row]["IsSended"].ToString().Equals("True"))
            {
                MessageBox.Show(this, "�ü�¼���ϴ��������޸��ˣ�");
                return;
            }
            model = new clsStockRecord();
            model.SysCode = this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
            model.StdCode = this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
            model.CompanyID = this.c1FlexGrid1.Rows[row]["CompanyID"].ToString();
            model.CompanyName = this.c1FlexGrid1.Rows[row]["CompanyName"].ToString();
            model.DisplayName = this.c1FlexGrid1.Rows[row]["DisplayName"].ToString();
            model.InputDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["InputDate"].ToString());
            model.FoodID = this.c1FlexGrid1.Rows[row]["FoodID"].ToString();
            model.FoodName = this.c1FlexGrid1.Rows[row]["FoodName"].ToString();
            model.Model = this.c1FlexGrid1.Rows[row]["Model"].ToString();
            model.InputNumber = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["InputNumber"].ToString());
            model.OutputNumber = Convert.ToDecimal(this.c1FlexGrid1.Rows[row]["OutputNumber"].ToString());
            model.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
            model.ProduceDate = Convert.ToDateTime(this.c1FlexGrid1.Rows[row]["ProduceDate"].ToString());
            model.ExpirationDate = this.c1FlexGrid1.Rows[row]["ExpirationDate"].ToString();
            model.ProduceCompanyID = this.c1FlexGrid1.Rows[row]["ProduceCompanyID"].ToString();
            model.ProduceCompanyName = this.c1FlexGrid1.Rows[row]["ProduceCompanyName"].ToString();
            model.PrivoderID = this.c1FlexGrid1.Rows[row]["PrivoderID"].ToString();
            model.PrivoderName = this.c1FlexGrid1.Rows[row]["PrivoderName"].ToString();
            model.LinkInfo = this.c1FlexGrid1.Rows[row]["LinkInfo"].ToString();
            model.LinkMan = this.c1FlexGrid1.Rows[row]["LinkMan"].ToString();
            model.CertificateType1 = this.c1FlexGrid1.Rows[row]["CertificateType1"].ToString();
            model.CertificateType2 = this.c1FlexGrid1.Rows[row]["CertificateType2"].ToString();
            model.CertificateType3 = this.c1FlexGrid1.Rows[row]["CertificateType3"].ToString();
            model.CertificateType4 = this.c1FlexGrid1.Rows[row]["CertificateType4"].ToString();
            model.CertificateType5 = this.c1FlexGrid1.Rows[row]["CertificateType5"].ToString();
            model.CertificateType6 = this.c1FlexGrid1.Rows[row]["CertificateType6"].ToString();
            model.CertificateType7 = this.c1FlexGrid1.Rows[row]["CertificateType7"].ToString();
            model.CertificateType8 = this.c1FlexGrid1.Rows[row]["CertificateType8"].ToString();
            model.CertificateType9 = this.c1FlexGrid1.Rows[row]["CertificateType9"].ToString();
            model.CertificateInfo = this.c1FlexGrid1.Rows[row]["CertificateInfo"].ToString();
            model.MakeMan = this.c1FlexGrid1.Rows[row]["MakeMan"].ToString();
            model.Remark = this.c1FlexGrid1.Rows[row]["Remark"].ToString();

            frmStockRecordEdit frm = new frmStockRecordEdit();
            frm.Tag = "EDIT";
            frm.setValue(model);
            DialogResult dr = frm.ShowDialog(this);

            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
		}

		private void frmStockRecord_Load(object sender, System.EventArgs e)
		{
			TreeNode theNode=new TreeNode();
			theNode.Text= "���е�λ";
			theNode.Tag = "%";

			clsCompanyOpr curCompanyOpr = new clsCompanyOpr();
			DataTable dt = curCompanyOpr.GetAsDataTable("(Property='" + ShareOption.CompanyProperty0 
					+ "' or Property='"+ ShareOption.CompanyProperty1 + "') And Len(StdCode)>6","SysCode",1);

			if(dt != null)
			{
				foreach(DataRow dr in dt.Rows)
				{
					TreeNode childNode = new TreeNode();
					childNode.Text = dr["FullName"].ToString();
					childNode.Tag = dr["SysCode"].ToString();

					theNode.Nodes.Add(childNode);
				}
			}
			
			this.treeView1.Nodes.Add(theNode);
			this.refreshGrid(theNode);
		}

		private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			//�ж��Ƿ���ɾ���ļ�¼
			int row=this.c1FlexGrid1.RowSel;
			if( row<=0)
			{
				MessageBox.Show(this,"��ѡ��Ҫɾ���ļ�¼��");
				return;
			}
			if(this.c1FlexGrid1.Rows[row]["IsSended"].ToString().Equals("True"))
			{
				MessageBox.Show(this,"�ü�¼���ϴ�������ɾ���ˣ�");
				return;
			}
			//���û�ȷ��ɾ������
			if(MessageBox.Show(this,"ȷ��Ҫɾ��ѡ��ļ�¼��","ѯ��",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
			{
				return;
			}
			
			//ɾ����¼
			string sErr="";
			string delStr=this.c1FlexGrid1[row,"SysCode"].ToString().Trim();
			recordBll.DeleteByPrimaryKey(delStr,out sErr);
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"���ݿ��������");
			}
		
			this.refreshGrid(this.treeView1.Nodes[0]);
		}
		
		private void setGridStyle()
		{
			this.c1FlexGrid1.Cols.Count =35;

			this.c1FlexGrid1.Cols["SysCode"].Caption="ϵͳ���";
			this.c1FlexGrid1.Cols["StdCode"].Caption="��¼���";
			this.c1FlexGrid1.Cols["CompanyID"].Caption="�̻����";
			this.c1FlexGrid1.Cols["CompanyName"].Caption="�̻�����";
			this.c1FlexGrid1.Cols["DisplayName"].Caption="��/�̺�";
			this.c1FlexGrid1.Cols["InputDate"].Caption="��������";
			this.c1FlexGrid1.Cols["FoodID"].Caption="��Ʒ���";
			this.c1FlexGrid1.Cols["FoodName"].Caption="��Ʒ����";
			this.c1FlexGrid1.Cols["Model"].Caption="���/�ͺ�/Ʒ��";
			this.c1FlexGrid1.Cols["InputNumber"].Caption="��������";
			this.c1FlexGrid1.Cols["OutputNumber"].Caption="��������";
			this.c1FlexGrid1.Cols["Unit"].Caption="������λ";
			this.c1FlexGrid1.Cols["ProduceDate"].Caption="��������";
			this.c1FlexGrid1.Cols["ExpirationDate"].Caption="������";
			this.c1FlexGrid1.Cols["ProduceCompanyID"].Caption="����/�ӹ���λ/���˱��";
			this.c1FlexGrid1.Cols["ProduceCompanyName"].Caption="����/�ӹ���λ/����";
			this.c1FlexGrid1.Cols["PrivoderID"].Caption="������λ/���˱��";
			this.c1FlexGrid1.Cols["PrivoderName"].Caption="������λ/����";
			this.c1FlexGrid1.Cols["LinkInfo"].Caption="��ϵ�绰";
			this.c1FlexGrid1.Cols["LinkMan"].Caption="��ϵ��";
			this.c1FlexGrid1.Cols["CertificateType1"].Caption="Ӫҵִ��";
			this.c1FlexGrid1.Cols["CertificateType2"].Caption="�������֤";
			this.c1FlexGrid1.Cols["CertificateType3"].Caption="�������֤��";
			this.c1FlexGrid1.Cols["CertificateType4"].Caption="��Ʒ�ϸ�֤";
			this.c1FlexGrid1.Cols["CertificateType5"].Caption="˰Ʊ�������";
			this.c1FlexGrid1.Cols["CertificateType6"].Caption="������Դ֤��";
			this.c1FlexGrid1.Cols["CertificateType7"].Caption="ũ�м��ϸ�֤";
			this.c1FlexGrid1.Cols["CertificateType8"].Caption="QS��־��֤֤��";
			this.c1FlexGrid1.Cols["CertificateType9"].Caption="����֤Ʊ";
			this.c1FlexGrid1.Cols["CertificateInfo"].Caption="��ȡ֤Ʊ��Ϣ";
			this.c1FlexGrid1.Cols["MakeMan"].Caption="������";
			this.c1FlexGrid1.Cols["Remark"].Caption="��ע";
			this.c1FlexGrid1.Cols["DistrictCode"].Caption="������������";
			this.c1FlexGrid1.Cols["IsSended"].Caption="�Ƿ��ϴ�";
			this.c1FlexGrid1.Cols["SentDate"].Caption="�ϴ�����";

			this.c1FlexGrid1.Cols["SysCode"].Visible=false;
			this.c1FlexGrid1.Cols["CompanyID"].Visible=false;
			this.c1FlexGrid1.Cols["FoodID"].Visible=false;
			this.c1FlexGrid1.Cols["ProduceCompanyID"].Visible=false;
			this.c1FlexGrid1.Cols["PrivoderID"].Visible=false;
			this.c1FlexGrid1.Cols["LinkMan"].Visible=false;
			this.c1FlexGrid1.Cols["CertificateInfo"].Visible=false;
			this.c1FlexGrid1.Cols["DistrictCode"].Visible=false;
			this.c1FlexGrid1.Cols["SentDate"].Visible=false;
			if (ShareOption.IsDataLink)
			{
				this.c1FlexGrid1.Cols["IsSended"].Visible=false;
			}
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Node==null || e.Node.Tag==null)
			{
				return;
			}
			refreshGrid(e.Node);
		}

		private void refreshGrid(TreeNode oprNode)
		{
			string firstTag = oprNode.Tag.ToString();
			string firstText = oprNode.Text.ToString();

			DataTable dt2=recordBll.GetAsDataTable("CompanyID  Like '" + firstTag 
				+ "'","SysCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"StockRecord");
			
			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
		}

		private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			PrintOperation.PreviewGrid(this.c1FlexGrid1,"����̨�ʼ�¼��",null);
		}

		private void cmdAdd_Select(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(c1ToolBar1,this.cmdAdd.Text);
		}

		
		private void cmdEdit_Select(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(c1ToolBar1,this.cmdEdit.Text);
		}

		private void cmdDelete_Select(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(c1ToolBar1,this.cmdDelete.Text);
		}

		private void cmdPrint_Select(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(c1ToolBar1,this.cmdPrint.Text);
		}

		private void cmdExit_Select(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(c1ToolBar1,this.cmdExit.Text);
		}
	}
}
