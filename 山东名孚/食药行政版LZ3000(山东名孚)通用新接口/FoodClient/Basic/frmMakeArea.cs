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
	/// frmMakeArea 的摘要说明。
	/// </summary>
	public class frmMakeArea : System.Windows.Forms.Form
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
		private C1.Win.C1Sizer.C1Sizer c1Sizer1;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		private System.Windows.Forms.TreeView treeView1;
		public C1.Win.C1Command.C1Command cmdAdd;
		private C1.Win.C1Command.C1Command cmdEdit;
		private C1.Win.C1Command.C1Command cmdDelete;
		private C1.Win.C1Command.C1Command cmdPrint;
		private C1.Win.C1Command.C1Command cmdExit;
	    private System.ComponentModel.IContainer components;

		private clsDistrict curObject;
		private clsDistrictOpr curObjectOpr;
		private System.Windows.Forms.ToolTip toolTip1;

		private TreeNode[] prevNodes;

		public frmMakeArea()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			curObjectOpr=new clsDistrictOpr();
			prevNodes=new TreeNode[ShareOption.MaxLevel];
			for(int i=0;i<ShareOption.MaxLevel;i++)
			{
				prevNodes[i]=new TreeNode();
			}
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMakeArea));
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
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
            this.cmdAdd.Text = "新增";
            this.cmdAdd.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAdd_Click);
            this.cmdAdd.Select += new System.EventHandler(this.cmdAdd_Select);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.cmdEdit.Text = "修改";
            this.cmdEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdEdit_Click);
            this.cmdEdit.Select += new System.EventHandler(this.cmdEdit_Select);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.cmdDelete.Text = "删除";
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
            this.cmdPrint.Text = "打印";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.cmdExit.Text = "退出";
            this.cmdExit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdExit_Click);
            this.cmdExit.Select += new System.EventHandler(this.cmdExit_Select);
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(815, 24);
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
            this.c1ToolBar1.Size = new System.Drawing.Size(815, 24);
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
            this.statusBar1.Location = new System.Drawing.Point(0, 488);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(815, 25);
            this.statusBar1.TabIndex = 4;
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.AllowDrop = true;
            this.c1Sizer1.Controls.Add(this.treeView1);
            this.c1Sizer1.Controls.Add(this.c1FlexGrid1);
            this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Sizer1.GridDefinition = "98.2758620689655:False:False;\t33.0061349693252:True:False;65.521472392638:False:F" +
                "alse;";
            this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Sizer1.Location = new System.Drawing.Point(0, 24);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(815, 464);
            this.c1Sizer1.TabIndex = 9;
            this.c1Sizer1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(269, 456);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Location = new System.Drawing.Point(277, 4);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(534, 456);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 2;
            // 
            // frmMakeArea
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(815, 513);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.c1CommandDock1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMakeArea";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组织机构维护";
            this.Load += new System.EventHandler(this.frmMakeArea_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
		}

		private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			int row=this.c1FlexGrid1.RowSel;
			if(this.treeView1.Nodes.Count==0 || row<=0)
			{
				return;
			}

			curObject=new clsDistrict();
			curObject.SysCode=this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
			curObject.StdCode=this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
			curObject.Name=this.c1FlexGrid1.Rows[row]["Name"].ToString();
			curObject.ShortCut=this.c1FlexGrid1.Rows[row]["ShortCut"].ToString();
			curObject.DistrictIndex=Convert.ToInt64(this.c1FlexGrid1.Rows[row]["DistrictIndex"]);
			curObject.CheckLevel=this.c1FlexGrid1.Rows[row]["CheckLevel"].ToString();
			curObject.Remark=this.c1FlexGrid1.Rows[row]["Remark"].ToString();
			curObject.IsLocal=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLocal"]);
			curObject.IsLock=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
			curObject.IsReadOnly=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsReadOnly"]);

			frmMakeAreaEdit frm=new frmMakeAreaEdit();
			frm.Tag="EDIT";		
			frm.setValue(curObject);
			DialogResult dr=frm.ShowDialog(this);

			//刷新窗体中的Grid
			if(dr==DialogResult.OK)
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}		
		}

		private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			curObject=new clsDistrict();
			//获取一个新的系统编号
			if(this.treeView1.Nodes.Count==0)
			{
				curObject.SysCode=1.ToString().PadLeft(ShareOption.DistrictCodeLevel,'0');
			}
			else
			{
				string curCode=this.treeView1.SelectedNode.Tag.ToString();
			
				string sErr="";
				int max=curObjectOpr.GetMaxNO(curCode,ShareOption.DistrictCodeLevel,out sErr);
				string syscode=curCode + (max+1).ToString().PadLeft(ShareOption.DistrictCodeLevel,'0');
				curObject.SysCode=syscode;
			}
			
			if(ShareOption.SysStdCodeSame)
			{
				curObject.StdCode=curObject.SysCode;
			}
			else
			{
				curObject.StdCode="";
			}
			curObject.Name="";
			curObject.ShortCut="";
			curObject.CheckLevel="";
			curObject.DistrictIndex=0;
			curObject.Remark="";
			curObject.IsLock=false;
			curObject.IsReadOnly=ShareOption.DefaultIsReadOnly;

			frmMakeAreaEdit frm=new frmMakeAreaEdit();
			frm.Tag="ADD";
			frm.setValue(curObject);
			DialogResult dr=frm.ShowDialog(this);

			//刷新窗体中的Grid
			if(dr==DialogResult.OK)
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}
		}

		private void frmMakeArea_Load(object sender, System.EventArgs e)
		{
			this.refreshGrid(null);

			if(this.treeView1.Nodes.Count>0)
			{
				this.treeView1.SelectedNode=this.treeView1.Nodes[0];
			}
		}

		private void refreshGrid(TreeNode oprNode)
		{
			this.treeView1.Nodes.Clear();

			DataTable dt=curObjectOpr.GetAsDataTable("","SysCode",0);

			if(dt==null || dt.Rows.Count==0)
			{
				this.c1FlexGrid1.SetDataBinding(dt.DataSet,"District");
				this.setGridStyle();
				this.c1FlexGrid1.AutoSizeCols();
				return;
			}
			
			TreeNode posNode=null;
			for(int i=0;i<dt.Rows.Count;i++)
			{
				string sCode=dt.Rows[i]["SysCode"].ToString();

				TreeNode theNode=new TreeNode();				
				theNode.Text=dt.Rows[i]["Name"].ToString();
				theNode.Tag=sCode;
				if(oprNode!=null && oprNode.Tag.ToString().Equals(sCode))
				{
					posNode=theNode;
				}

				int iMod=sCode.Length/ShareOption.DistrictCodeLevel;
				if(iMod==1)
				{
					this.treeView1.Nodes.Add(theNode);
				}
				else
				{
					prevNodes[iMod-2].Nodes.Add(theNode);
				}

				prevNodes[iMod-1]=theNode;
			}
			
			string sCoding="";
			if(oprNode==null)
			{
				sCoding=dt.Rows[0]["SysCode"].ToString();
			}
			else
			{
				sCoding=oprNode.Tag.ToString();
			}

			DataTable dt2=curObjectOpr.GetAsDataTable("SysCode ='" + sCoding 
				+ "' or " + "SysCode like '" + sCoding 
				+ StringUtil.RepeatChar('_',ShareOption.DistrictCodeLevel) + "'","SysCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"District");

			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
			
			//定位treeview所在节点
			if(posNode!=null)
			{
				this.treeView1.SelectedNode=posNode;
				posNode.EnsureVisible();
			}
			else
			{
				if(this.treeView1.Nodes.Count>=1) this.treeView1.Nodes[0].Expand();
			}
		}
		
		private void setGridStyle()
		{
			this.c1FlexGrid1.Cols["SysCode"].Caption="系统编号";
			this.c1FlexGrid1.Cols["StdCode"].Caption="编号";
			this.c1FlexGrid1.Cols["Name"].Caption="机构名称";
			this.c1FlexGrid1.Cols["ShortCut"].Caption="快捷编码";
			this.c1FlexGrid1.Cols["DistrictIndex"].Caption="顺序号";
			this.c1FlexGrid1.Cols["CheckLevel"].Caption="监控级别";
			this.c1FlexGrid1.Cols["IsLocal"].Caption="本地管辖";
			this.c1FlexGrid1.Cols["IsLock"].Caption="停用";
			this.c1FlexGrid1.Cols["IsReadOnly"].Caption="已审核";
			this.c1FlexGrid1.Cols["Remark"].Caption="备注";

			this.c1FlexGrid1.Cols["SysCode"].Visible=false;
//			this.c1FlexGrid1.Cols["IsReadOnly"].Visible=false;
			this.c1FlexGrid1.Cols["CheckLevel"].Visible=false;
			this.c1FlexGrid1.Cols["IsLocal"].Visible=false;

			this.c1FlexGrid1.Cols["StdCode"].Width=60;
			this.c1FlexGrid1.Cols["Name"].Width=300;
			this.c1FlexGrid1.Cols["ShortCut"].Width=60;
			this.c1FlexGrid1.Cols["DistrictIndex"].Width=30;
			this.c1FlexGrid1.Cols["IsLocal"].Width=30;
			this.c1FlexGrid1.Cols["IsLock"].Width=30;
			this.c1FlexGrid1.Cols["Remark"].Width=300;
		}

		private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			//判断是否有删除的记录
			int row=this.c1FlexGrid1.RowSel;
			if(this.treeView1.Nodes.Count==0 || row<=0)
			{
				MessageBox.Show(this,"请选择将要删除的记录！");
				return;
			}
			
			string delStr=this.c1FlexGrid1[row,"SysCode"].ToString().Trim();
			//判断是否可以删除
			if(!curObjectOpr.CanDelete(delStr,ShareOption.DistrictCodeLevel))
			{
				MessageBox.Show(this,"被删除记录下面有子记录，请先删除子记录！");
				return;
			}
			if(!curObjectOpr.CanDelete(delStr))
			{
				MessageBox.Show(this,"此记录不能被删除，请先删除相关的记录！");
				return;
			}

			//让用户确定删除操作
			if(MessageBox.Show(this,"确定要删除选择的记录？","询问",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
			{
				return;
			}
			
			//删除记录
			string sErr="";
			curObjectOpr.DeleteByPrimaryKey(delStr,out sErr);
			if(!sErr.Equals(""))
			{
				MessageBox.Show(this,"数据库操作出错！");
			}
		
			if(this.treeView1.SelectedNode.Nodes.Count==0)
			{
				if(this.treeView1.SelectedNode.NextNode==null)
				{
					if(this.treeView1.SelectedNode.PrevNode ==null)
					{
						this.refreshGrid(this.treeView1.SelectedNode.Parent);
					}
					else
					{
						this.refreshGrid(this.treeView1.SelectedNode.PrevNode );
					}
				}
				else
				{
					this.refreshGrid(this.treeView1.SelectedNode.NextNode);
				}
			}
			else
			{
				this.refreshGrid(this.treeView1.SelectedNode);
				this.treeView1.SelectedNode.Expand();
			}
		}

        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            PrintOperation.PreviewGrid(this.c1FlexGrid1, "组织机构列表", null);
        }

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Node==null || e.Node.Tag==null)
			{
				return;
			}
			string first=e.Node.Tag.ToString();

			DataTable dt2=curObjectOpr.GetAsDataTable("SysCode ='" + first 
				+ "' or " + "SysCode like '" + first 
				+ StringUtil.RepeatChar('_',ShareOption.DistrictCodeLevel) + "'","SysCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"District");
			
			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
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
