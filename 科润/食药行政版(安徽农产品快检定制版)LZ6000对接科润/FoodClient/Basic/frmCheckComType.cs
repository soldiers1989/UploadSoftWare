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
	/// frmCheckComType ��ժҪ˵����
	/// </summary>
	public class frmCheckComType : System.Windows.Forms.Form
	{
		private C1.Win.C1Command.C1Command cmdDelete;
		private C1.Win.C1Command.C1Command cmdEdit;
		private C1.Win.C1Command.C1Command c1Command5;
		private C1.Win.C1Command.C1Command cmdExit;
        private C1.Win.C1Command.C1Command cmdPrint;
        private C1.Win.C1Command.C1Command cmdAdd;
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1CommandLink c1CommandLink5;
		private System.Windows.Forms.StatusBar statusBar1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink6;
		private C1.Win.C1Command.C1CommandDock c1CommandDock1;
		private C1.Win.C1Command.C1ToolBar c1ToolBar1;
		public C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private System.Windows.Forms.Panel panel1;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;

		private clsCheckComType curObject;
		private readonly clsCheckComTypeOpr curObjectOpr;

        public frmCheckComType()
        {
            InitializeComponent();

            curObjectOpr = new clsCheckComTypeOpr();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckComType));
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.cmdAdd = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "ɾ��";
            this.cmdDelete.Visible = false;
            this.cmdDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDelete_Click);
            this.cmdDelete.Select += new System.EventHandler(this.cmdDelete_Select);
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
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
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
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "��ӡ";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Enabled = false;
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.cmdAdd.Text = "����";
            this.cmdAdd.Visible = false;
            this.cmdAdd.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd_Click);
            this.cmdAdd.Select += new System.EventHandler(this.cmdAdd_Select);
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
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.cmdDelete;
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.Command = this.cmdPrint;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 428);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(835, 25);
            this.statusBar1.TabIndex = 8;
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.Command = this.cmdExit;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(835, 24);
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
            this.c1ToolBar1.Size = new System.Drawing.Size(835, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdAdd;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.ButtonLook = ((C1.Win.C1Command.ButtonLookFlags)((C1.Win.C1Command.ButtonLookFlags.Text | C1.Win.C1Command.ButtonLookFlags.Image)));
            this.c1CommandLink2.Command = this.cmdEdit;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1FlexGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 404);
            this.panel1.TabIndex = 11;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(835, 404);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 4;
            // 
            // frmCheckComType
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(835, 453);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCheckComType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��������ά��";
            this.Load += new System.EventHandler(this.frmCheckComType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            curObject = new clsCheckComType();
            //��ȡһ���µ�ϵͳ���		
            curObject.ID = 0;
            curObject.TypeName = string.Empty;
            curObject.NameCall = string.Empty;
            curObject.AreaCall = string.Empty;
            curObject.VerType = string.Empty;
            curObject.AreaTitle = string.Empty;
            curObject.NameTitle = string.Empty;
            curObject.DomainTitle = string.Empty;
            curObject.SampleTitle = string.Empty;
            curObject.IsLock = false;
            curObject.IsReadOnly = ShareOption.DefaultIsReadOnly;
            curObject.ComKind = string.Empty;

            frmCheckComTypeEdit frm = new frmCheckComTypeEdit();
            frm.Tag = "ADD";
            frm.setValue(curObject);
            DialogResult dr = frm.ShowDialog(this);

            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid();
            }
        }

        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }

            curObject = new clsCheckComType();
            curObject.ID = Convert.ToInt32(this.c1FlexGrid1.Rows[row]["ID"].ToString());
            curObject.TypeName = this.c1FlexGrid1.Rows[row]["TypeName"].ToString();
            curObject.NameCall = this.c1FlexGrid1.Rows[row]["NameCall"].ToString();
            curObject.AreaCall = this.c1FlexGrid1.Rows[row]["AreaCall"].ToString();
            curObject.VerType = this.c1FlexGrid1.Rows[row]["VerType"].ToString();
            curObject.IsLock = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
            curObject.IsReadOnly = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsReadOnly"]);
            curObject.ComKind = this.c1FlexGrid1.Rows[row]["ComKind"].ToString();
            curObject.AreaTitle = this.c1FlexGrid1.Rows[row]["AreaTitle"].ToString();
            curObject.NameTitle = this.c1FlexGrid1.Rows[row]["NameTitle"].ToString();
            curObject.DomainTitle = this.c1FlexGrid1.Rows[row]["DomainTitle"].ToString();
            curObject.SampleTitle = this.c1FlexGrid1.Rows[row]["SampleTitle"].ToString();

            frmCheckComTypeEdit frm = new frmCheckComTypeEdit();
            frm.Tag = "EDIT";
            frm.setValue(curObject);
            DialogResult dr = frm.ShowDialog(this);

            //ˢ�´����е�Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid();
            }
        }

		private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			//�ж��Ƿ���ɾ���ļ�¼
			int row=this.c1FlexGrid1.RowSel;
			if(row<=0)
			{
				MessageBox.Show(this,"��ѡ��Ҫɾ���ļ�¼��");
				return;
			}
			
			string delStr=this.c1FlexGrid1[row,"ID"].ToString().Trim();

			if(clsCheckComTypeOpr.TypeIsNoDel(this.c1FlexGrid1[row,"TypeName"].ToString()))
			{
				MessageBox.Show(this,"ϵͳ���趨�Ĵ���"+this.c1FlexGrid1[row,"TypeName"].ToString()+"���͵ļ��㣡���Բ���ɾ��");
				return;
			}
			//���û�ȷ��ɾ������
			if(MessageBox.Show(this,"ȷ��Ҫɾ��ѡ��ļ�¼��","ѯ��",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
			{
				return;
			}
			
			//ɾ����¼
			string sErr=string.Empty;
			curObjectOpr.DeleteByPrimaryKey(delStr,out sErr);
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"���ݿ��������");
			}
		
			this.refreshGrid();
		}
		
		private void setGridStyle()
		{
			this.c1FlexGrid1.Cols.Count=13;
			this.c1FlexGrid1.Cols["ID"].Caption="ϵͳ���";
			this.c1FlexGrid1.Cols["TypeName"].Caption="������������";
			this.c1FlexGrid1.Cols["NameCall"].Caption="��λ���������";
			this.c1FlexGrid1.Cols["AreaCall"].Caption="������֯���������";
			this.c1FlexGrid1.Cols["VerType"].Caption="��Ӧ�汾";
			this.c1FlexGrid1.Cols["IsLock"].Caption="ͣ��";
			this.c1FlexGrid1.Cols["IsReadOnly"].Caption="�����";
			this.c1FlexGrid1.Cols["ComKind"].Caption="��λ�����";
			this.c1FlexGrid1.Cols["ComKindName"].Caption="������λ���";
			this.c1FlexGrid1.Cols["AreaTitle"].Caption ="������֯�����";
			this.c1FlexGrid1.Cols["NameTitle"].Caption ="�ܼ���/��λ�����";
			this.c1FlexGrid1.Cols["DomainTitle"].Caption ="λ�ñ�������";
			this.c1FlexGrid1.Cols["SampleTitle"].Caption ="��Ʒ���������";


			this.c1FlexGrid1.Cols["ID"].Visible=false;
//			this.c1FlexGrid1.Cols["IsReadOnly"].Visible=false;
			this.c1FlexGrid1.Cols["ComKind"].Visible=false;
			this.c1FlexGrid1.Cols["ComKindName"].Visible=false;
			if(ShareOption.IsDataLink)
			{
				this.c1FlexGrid1.Cols["VerType"].Visible=false;
			}

		}

		private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
//			PrintOperation.PrintGrid(this.c1FlexGrid1,"��λ����б�",null);
			PrintOperation.PreviewGrid(this.c1FlexGrid1,"�������ͱ�",null);
		}

		private void refreshGrid()
		{
			DataTable dt=curObjectOpr.GetAsDataTable("","A.ID",0);
			this.c1FlexGrid1.SetDataBinding(dt.DataSet,"CheckComType");

			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
		}

		private void frmCheckComType_Load(object sender, System.EventArgs e)
		{
			this.refreshGrid();
		}

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
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
