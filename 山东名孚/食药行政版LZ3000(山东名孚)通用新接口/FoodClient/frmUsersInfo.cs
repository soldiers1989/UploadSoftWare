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
	/// frmUsersInfo 的摘要说明。
	/// </summary>
	public class frmUsersInfo : TitleBarBase
	{
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private C1.Win.C1Command.C1Command c1Command5;
		private C1.Win.C1Command.C1ToolBar c1ToolBar1;
		public C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1CommandLink c1CommandLink5;
		private C1.Win.C1Command.C1CommandLink c1CommandLink6;
		private C1.Win.C1Sizer.C1Sizer c1Sizer1;
		private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
		private System.Windows.Forms.TreeView treeView1;
		public C1.Win.C1Command.C1Command cmdAdd;
		private C1.Win.C1Command.C1Command cmdEdit;
		private C1.Win.C1Command.C1Command cmdDelete;
		private C1.Win.C1Command.C1Command cmdPrint;
		private C1.Win.C1Command.C1Command cmdExit;
	    private System.ComponentModel.IContainer components;

		private clsUserInfo curObject;
		private clsUserInfoOpr curObjectOpr;
		private System.Windows.Forms.ToolTip toolTip1;

		private TreeNode[] prevNodes;

		public frmUsersInfo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			curObjectOpr=new clsUserInfoOpr();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsersInfo));
			this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
			this.cmdAdd = new C1.Win.C1Command.C1Command();
			this.cmdEdit = new C1.Win.C1Command.C1Command();
			this.cmdDelete = new C1.Win.C1Command.C1Command();
			this.c1Command5 = new C1.Win.C1Command.C1Command();
			this.cmdPrint = new C1.Win.C1Command.C1Command();
			this.cmdExit = new C1.Win.C1Command.C1Command();
			this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
			this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
			this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
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
			// c1ToolBar1
			// 
			this.c1ToolBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
			this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
			this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1,
            this.c1CommandLink2,
            this.c1CommandLink3,
            this.c1CommandLink4,
            this.c1CommandLink5,
            this.c1CommandLink6});
			this.c1ToolBar1.Location = new System.Drawing.Point(3, 29);
			this.c1ToolBar1.Movable = false;
			this.c1ToolBar1.Name = "c1ToolBar1";
			this.c1ToolBar1.ShowToolTips = false;
			this.c1ToolBar1.Size = new System.Drawing.Size(123, 24);
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
			// c1Sizer1
			// 
			this.c1Sizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.c1Sizer1.Controls.Add(this.treeView1);
			this.c1Sizer1.Controls.Add(this.c1FlexGrid1);
			this.c1Sizer1.GridDefinition = "97.6261127596439:True:False;\t21.6931216931217:True:False;75.1322751322751:False:F" +
				"alse;";
			this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.c1Sizer1.Location = new System.Drawing.Point(3, 53);
			this.c1Sizer1.Name = "c1Sizer1";
			this.c1Sizer1.Size = new System.Drawing.Size(378, 337);
			this.c1Sizer1.TabIndex = 6;
			this.c1Sizer1.TabStop = false;
			// 
			// treeView1
			// 
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(4, 4);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(82, 329);
			this.treeView1.TabIndex = 3;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// c1FlexGrid1
			// 
			this.c1FlexGrid1.AllowEditing = false;
			this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
			this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
			this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
			this.c1FlexGrid1.Location = new System.Drawing.Point(90, 4);
			this.c1FlexGrid1.Name = "c1FlexGrid1";
			this.c1FlexGrid1.Rows.Count = 1;
			this.c1FlexGrid1.Rows.DefaultSize = 18;
			this.c1FlexGrid1.Rows.MaxSize = 200;
			this.c1FlexGrid1.Rows.MinSize = 20;
			this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.c1FlexGrid1.Size = new System.Drawing.Size(284, 329);
			this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
			this.c1FlexGrid1.TabIndex = 2;
			// 
			// frmUsersInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
			this.ClientSize = new System.Drawing.Size(386, 392);
			this.Controls.Add(this.c1ToolBar1);
			this.Controls.Add(this.c1Sizer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmUsersInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "用户权限管理";
			this.Load += new System.EventHandler(this.frmUsersInfo_Load);
			this.Controls.SetChildIndex(this.c1Sizer1, 0);
			this.Controls.SetChildIndex(this.c1ToolBar1, 0);
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
			this.c1Sizer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			int row=this.c1FlexGrid1.RowSel;
			if(this.treeView1.Nodes.Count==0 || row<=0)
			{
				return;
			}
			
			string delStr=this.c1FlexGrid1[row,"UserCode"].ToString().Trim();


			curObject=new clsUserInfo();
			curObject.UserCode=this.c1FlexGrid1.Rows[row]["UserCode"].ToString();
			curObject.LoginID=this.c1FlexGrid1.Rows[row]["LoginID"].ToString();
			curObject.Name=this.c1FlexGrid1.Rows[row]["Name"].ToString();
			curObject.PassWord=this.c1FlexGrid1.Rows[row]["PassWord"].ToString();
			curObject.UnitCode=this.c1FlexGrid1.Rows[row]["UnitCode"].ToString();
			curObject.WebLoginID=this.c1FlexGrid1.Rows[row]["WebLoginID"].ToString();
			curObject.WebPassWord=this.c1FlexGrid1.Rows[row]["WebPassWord"].ToString();
			curObject.Remark=this.c1FlexGrid1.Rows[row]["Remark"].ToString();
			curObject.IsLock=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
			curObject.IsAdmin=Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsAdmin"]);

			frmUsersInfoEdit frm=new frmUsersInfoEdit();
			frm.Tag="EDIT";		
			frm.setValue(curObject);
			//admin系统管理员不能修改
			if(delStr.Equals("00"))
			{
				frm.chkAdmin.Enabled = false;
				frm.chkLock.Enabled = false;
				frm.cmbUnit.Enabled = false;
			}
			DialogResult dr=frm.ShowDialog(this);

			//刷新窗体中的Grid
			if(dr==DialogResult.OK)
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}		
		}

		private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Close();
		}

		private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if(this.treeView1.Nodes.Count==0)
			{
				MessageBox.Show(this,"新增用户前必须选择一个检测单位！");
				return;
			}

			curObject=new clsUserInfo();
			//获取一个新的系统编号	
			string sErr="";
			int max=curObjectOpr.GetMaxNO(out sErr);
			string syscode=(max+1).ToString().PadLeft(ShareOption.UserCodeLen,'0');
			curObject.UserCode=syscode;

			curObject.LoginID="";
			curObject.Name="";
			curObject.PassWord="";
			if(this.treeView1.SelectedNode!=null)
			{
				curObject.UnitCode=this.treeView1.SelectedNode.Tag.ToString();
			}
			else
			{
				curObject.UnitCode="";
			}
			curObject.WebLoginID="";
			curObject.WebPassWord="";
			curObject.Remark="";
			curObject.IsLock=false;
			curObject.IsAdmin=false;

			frmUsersInfoEdit frm=new frmUsersInfoEdit();
			frm.Tag="ADD";
			frm.setValue(curObject);
			DialogResult dr=frm.ShowDialog(this);

			//刷新窗体中的Grid
			if(dr==DialogResult.OK)
			{
				this.refreshGrid(this.treeView1.SelectedNode);
			}		
		}

		private void frmUsersInfo_Load(object sender, System.EventArgs e)
		{
			this.treeView1.Nodes.Clear();
		    this.c1Sizer1.Grid.Columns[0].Size=0;
			clsUserUnitOpr curUnitOpr=new clsUserUnitOpr();
			DataTable dt=curUnitOpr.GetAsDataTable("","A.SysCode",0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sCode = dt.Rows[i]["SysCode"].ToString();

                TreeNode theNode = new TreeNode();
                theNode.Text = dt.Rows[i]["FullName"].ToString();
                theNode.Tag = sCode;

                int iMod = sCode.Length / ShareOption.UserUnitCodeLevel;
                if (iMod == 1)
                {
                    this.treeView1.Nodes.Add(theNode);
                }
                else
                {
                    prevNodes[iMod - 2].Nodes.Add(theNode);
                }

                prevNodes[iMod - 1] = theNode;
            }
			
			if(this.treeView1.Nodes.Count>0)
			{
				this.refreshGrid(this.treeView1.Nodes[0]);

				this.treeView1.SelectedNode=this.treeView1.Nodes[0];
			}

			TitleBarText = "用户权限管理";

			c1FlexGrid1.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;
		}

        private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //判断是否有删除的记录
            int row = this.c1FlexGrid1.RowSel;
            if (this.treeView1.Nodes.Count == 0 || row <= 0)
            {
                MessageBox.Show(this, "请选择将要删除的记录！");
                return;
            }

            string delStr = this.c1FlexGrid1[row, "UserCode"].ToString().Trim();
            //admin系统管理员不能删除
            if (delStr.Equals("00"))
            {
                MessageBox.Show(this, "系统管理员不能被删除！");
                return;
            }

            //让用户确定删除操作
            if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            //删除记录
            string sErr = string.Empty;
            curObjectOpr.DeleteByPrimaryKey(delStr, out sErr);
            if (!sErr.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！");
            }

            this.refreshGrid(this.treeView1.Nodes[0]);
        }
		
		private void setGridStyle()
		{
			this.c1FlexGrid1.Cols["UserCode"].Caption="系统编号";
			this.c1FlexGrid1.Cols["LoginID"].Caption="登录名";
			this.c1FlexGrid1.Cols["Name"].Caption="用户名称";
			this.c1FlexGrid1.Cols["PassWord"].Caption="登录密码";
			this.c1FlexGrid1.Cols["UnitCode"].Caption="检测单位";
			this.c1FlexGrid1.Cols["WebLoginID"].Caption="网站登陆名";
			this.c1FlexGrid1.Cols["WebPassWord"].Caption="网站登陆密码";
			this.c1FlexGrid1.Cols["IsLock"].Caption="停用";
			this.c1FlexGrid1.Cols["IsAdmin"].Caption="系统管理员";
			this.c1FlexGrid1.Cols["Remark"].Caption="备注";

			this.c1FlexGrid1.Cols["WebLoginID"].Visible=false;
			this.c1FlexGrid1.Cols["UserCode"].Visible=false;
			this.c1FlexGrid1.Cols["PassWord"].Visible=false;
			this.c1FlexGrid1.Cols["UnitCode"].Visible=false;
			this.c1FlexGrid1.Cols["WebPassWord"].Visible=false;

			this.c1FlexGrid1.Cols["LoginID"].Width=60;
			this.c1FlexGrid1.Cols["Name"].Width=300;			
			this.c1FlexGrid1.Cols["WebLoginID"].Width=60;
			this.c1FlexGrid1.Cols["IsLock"].Width=30;
			this.c1FlexGrid1.Cols["IsAdmin"].Width=30;
			this.c1FlexGrid1.Cols["Remark"].Width=300;
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Node==null || e.Node.Tag==null)
			{
				return;
			}
			
			this.refreshGrid(e.Node);
		}

		private void refreshGrid(TreeNode oprNode)
		{
			string sCoding=oprNode.Tag.ToString();

			DataTable dt2=curObjectOpr.GetAsDataTable("UnitCode ='" + sCoding + "'","UserCode",0);
			this.c1FlexGrid1.SetDataBinding(dt2.DataSet,"UserInfo");

			this.setGridStyle();
			this.c1FlexGrid1.AutoSizeCols();
			
			//定位treeview所在节点
			if(oprNode!=null)
			{
				this.treeView1.SelectedNode=oprNode;
				oprNode.EnsureVisible();
			}
		}

		private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
//			PrintOperation.PrintGrid(this.c1FlexGrid1,"用户列表",null);

			PrintOperation.PreviewGrid(this.c1FlexGrid1,"用户列表",null);
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



		protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
		{

		}
	}
}
