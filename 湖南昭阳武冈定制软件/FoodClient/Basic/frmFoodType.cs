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
	/// frmFoodType 的摘要说明。
	/// </summary>
	public class frmFoodType : System.Windows.Forms.Form
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
		
		private clsFoodClass curObject;
		private clsFoodClassOpr curObjectOpr;
		private System.Windows.Forms.ToolTip toolTip1;

		private TreeNode[] prevNodes;

		public frmFoodType()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();	

			curObjectOpr=new clsFoodClassOpr();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFoodType));
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
            // frmFoodType
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(620, 338);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.c1CommandDock1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFoodType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text =ShareOption.SampleTitle+ "种类维护";//食品
            this.Load += new System.EventHandler(this.frmFoodType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			curObject=new clsFoodClass();
			//获取一个新的系统编号
			if(this.treeView1.Nodes.Count==0)
			{
				curObject.SysCode=1.ToString().PadLeft(ShareOption.FoodCodeLevel,'0');
				curObject.CheckItemCodes="";
			}
			else
			{
				string curCode=this.treeView1.SelectedNode.Tag.ToString();
			
				string sErr="";
				int max=curObjectOpr.GetMaxNO(curCode,ShareOption.FoodCodeLevel,out sErr);
				string syscode=curCode + (max+1).ToString().PadLeft(ShareOption.FoodCodeLevel,'0');
				curObject.SysCode=syscode;
				curObject.CheckItemCodes=curObjectOpr.GetPreCheckItemCodes(curCode,out sErr);
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
			curObject.CheckItemValue="";
			curObject.Remark="";
			curObject.IsLock=false;
			curObject.IsReadOnly=ShareOption.DefaultIsReadOnly;

			frmFoodTypeEdit frm=new frmFoodTypeEdit();
			frm.Tag="ADD";
			frm.setValue(curObject);
			DialogResult dr=frm.ShowDialog(this);

			//刷新窗体中的Grid
			if(dr==DialogResult.OK)
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}
		}

		private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			int row=this.c1FlexGrid1.RowSel;
			if(this.treeView1.Nodes.Count==0 || row<=0)
			{
				return;
			}

			curObject=new clsFoodClass();
			curObject.SysCode=this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
			curObject.StdCode=this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
			curObject.Name=this.c1FlexGrid1.Rows[row]["Name"].ToString();
			curObject.ShortCut=this.c1FlexGrid1.Rows[row]["ShortCut"].ToString();
			curObject.CheckLevel=this.c1FlexGrid1.Rows[row]["CheckLevel"].ToString();
			curObject.CheckItemCodes=this.c1FlexGrid1.Rows[row]["CheckItemCodes"].ToString();
			curObject.CheckItemValue=this.c1FlexGrid1.Rows[row]["CheckItemValue"].ToString();
			curObject.Remark=this.c1FlexGrid1.Rows[row]["Remark"].ToString();
			curObject.IsLock=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
			curObject.IsReadOnly=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsReadOnly"]);

			frmFoodTypeEdit frm=new frmFoodTypeEdit();
			frm.Tag="EDIT";		
			frm.setValue(curObject);
			DialogResult dr=frm.ShowDialog(this);

			this.refreshGrid(this.treeView1.SelectedNode);
		}

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
		}

		private void frmFoodType_Load(object sender, System.EventArgs e)
		{
			this.refreshGrid(null);

			if(this.treeView1.Nodes.Count>0)
			{
				this.treeView1.SelectedNode=this.treeView1.Nodes[0];
			}
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
			if(!curObjectOpr.CanDelete(delStr,ShareOption.FoodCodeLevel))
			{
				MessageBox.Show(this,"被删除记录下面有子记录，请先删除子记录！");
				return;
			}

			//让用户确定删除操作
			if(MessageBox.Show(this,"确定要删除选择的记录及相关的记录？","询问",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
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
		
			//检查删除的是不是当前选择的记录
			TreeNode posNode=null;
			TreeNode delNode=null;
			if(this.treeView1.SelectedNode.Tag.ToString().Equals(this.c1FlexGrid1[row,"SysCode"].ToString()))
			{
				delNode=this.treeView1.SelectedNode;
				if(this.treeView1.SelectedNode.PrevNode==null)
				{
					if(this.treeView1.SelectedNode.NextNode==null)
					{
						if(this.treeView1.SelectedNode.Parent==null)
						{
							this.refreshGrid(this.treeView1.SelectedNode);
						}
						else
						{
							posNode=this.treeView1.SelectedNode.Parent;
						}
					}
					else
					{
						posNode=this.treeView1.SelectedNode.NextNode;
					}
				}
				else
				{
					posNode=this.treeView1.SelectedNode.PrevNode;
				}
				this.treeView1.SelectedNode=posNode;
				this.treeView1.Nodes.Remove ( delNode);
			}
			else
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}
		}
		
		private void setGridStyle()
		{
			this.c1FlexGrid1.Cols["SysCode"].Caption="系统编号";
			this.c1FlexGrid1.Cols["StdCode"].Caption="编号";
			this.c1FlexGrid1.Cols["Name"].Caption="种类名称";
			this.c1FlexGrid1.Cols["ShortCut"].Caption="快捷编码";
			this.c1FlexGrid1.Cols["CheckLevel"].Caption="监控级别";
			this.c1FlexGrid1.Cols["CheckItemCodes"].Caption="检测项目编码";
			this.c1FlexGrid1.Cols["CheckItemValue"].Caption="检测项目值";
			this.c1FlexGrid1.Cols["IsLock"].Caption="停用";
			this.c1FlexGrid1.Cols["IsReadOnly"].Caption="已审核";
			this.c1FlexGrid1.Cols["Remark"].Caption="备注";
			this.c1FlexGrid1.Cols["FoodProperty"].Caption="备注";

			this.c1FlexGrid1.Cols["SysCode"].Visible=false;
			this.c1FlexGrid1.Cols["IsReadOnly"].Visible=true;
			this.c1FlexGrid1.Cols["CheckItemCodes"].Visible=false;
			this.c1FlexGrid1.Cols["CheckItemValue"].Visible=false;
//			this.c1FlexGrid1.Cols["CheckLevel"].Visible=false;
			this.c1FlexGrid1.Cols["FoodProperty"].Visible=false;

			this.c1FlexGrid1.Cols["StdCode"].Width=60;
			this.c1FlexGrid1.Cols["Name"].Width=300;
			this.c1FlexGrid1.Cols["ShortCut"].Width=60;
			this.c1FlexGrid1.Cols["IsLock"].Width=30;
			this.c1FlexGrid1.Cols["Remark"].Width=300;
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
				+ StringUtil.RepeatChar('_',ShareOption.FoodCodeLevel) + "'","SysCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"foodclass");
			
			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
		}

		private void refreshGrid(TreeNode oprNode)
		{
			this.treeView1.Nodes.Clear();

			DataTable dt=curObjectOpr.GetAsDataTable("","SysCode",0);

			if(dt==null || dt.Rows.Count==0)
			{
				this.c1FlexGrid1.SetDataBinding(dt.DataSet,"foodclass");
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

				int iMod=sCode.Length/ShareOption.FoodCodeLevel;
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
				+ StringUtil.RepeatChar('_',ShareOption.FoodCodeLevel) + "'","SysCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"foodclass");

			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();

			//定位treeview所在节点
			if(posNode!=null)
			{
				this.treeView1.SelectedNode=posNode;
				posNode.EnsureVisible();
			}
		}

		private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
//			PrintOperation.PrintGrid(this.c1FlexGrid1,"食品类别列表",null);
			PrintOperation.PreviewGrid(this.c1FlexGrid1,ShareOption.SampleTitle+"类别列表",null);//食品
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
