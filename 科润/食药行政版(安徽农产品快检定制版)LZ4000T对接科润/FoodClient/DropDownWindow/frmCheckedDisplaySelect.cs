using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DY.FoodClientLib;
using System.Text.RegularExpressions;

namespace FoodClient
{
	/// <summary>
	/// frmCheckedDisplaySelect 的摘要说明。
	/// </summary>
	public class frmCheckedDisplaySelect : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TreeView treeView1;
		
		private System.ComponentModel.Container components = null;

		private string _sCode;
		private string _sType;
		private clsCompanyOpr curObjectOpr;
		public string sNodeTag="";
		public string sNodeName="";
		private System.Windows.Forms.Button btnNext;
		private TreeNode[] prevNodes;

        public frmCheckedDisplaySelect(string pType, string pCode)
        {
            InitializeComponent();

            curObjectOpr = new clsCompanyOpr();
            _sType = pType;
            _sCode = pCode;
            prevNodes = new TreeNode[ShareOption.MaxLevel];
            for (int i = 0; i < ShareOption.MaxLevel; i++)
            {
                prevNodes[i] = new TreeNode();
            }
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
            this.btnLocation = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(224, 5);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(72, 24);
            this.btnLocation.TabIndex = 21;
            this.btnLocation.Text = "定位";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 7);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(128, 21);
            this.txtName.TabIndex = 19;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 32);
            this.label3.TabIndex = 20;
            this.label3.Text = "档口/店面／车牌号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 343);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 343);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(8, 31);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(384, 304);
            this.treeView1.TabIndex = 16;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(304, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(72, 24);
            this.btnNext.TabIndex = 23;
            this.btnNext.Text = "下一个";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // frmCheckedDisplaySelect
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(400, 373);
            this.ControlBox = false;
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckedDisplaySelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择档口/店面／车牌号";
            this.Load += new System.EventHandler(this.frmCheckedDisplaySelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0)
			{
				selectNode(this.txtName.Text.Trim(),false);
			}
		}

		private void selectNode(string sNodeName,bool IsNext)
		{
			bool IsReRun=false;
			TreeNode CurrentNode=null;
			if(!IsNext)
			{
				CurrentNode=this.treeView1.Nodes[0];
			}
			else
			{
				CurrentNode=this.GetNextNode(this.treeView1.SelectedNode,true);
			}
			System.Text.RegularExpressions.Regex r=new Regex(sNodeName,System.Text.RegularExpressions.RegexOptions.IgnoreCase); 
			while(CurrentNode!=null)
			{
				Match m=r.Match(CurrentNode.Text);
				if(m.Success)
				{
					this.treeView1.SelectedNode=CurrentNode;
					CurrentNode.EnsureVisible();
					return;
				}
				else
				{
					if(IsReRun&&IsNext&&CurrentNode.Equals(this.treeView1.SelectedNode)) return;
					CurrentNode=this.GetNextNode(CurrentNode,false);
					if(CurrentNode==null&&IsNext)
					{
						IsReRun=true;
						CurrentNode=this.treeView1.Nodes[0];
					}
				}
			}
		}

		private TreeNode GetNextNode(TreeNode CurrentNode,bool IsReRun)
		{
			TreeNode NextNode=null;
			//得到下一个节点
			if(CurrentNode.FirstNode != null) 	
			{
				NextNode=CurrentNode.FirstNode;
			}
			else if(CurrentNode.NextNode != null)
			{
				NextNode=CurrentNode.NextNode;
			}
			else if(CurrentNode.Parent != null)
			{
				while(CurrentNode.Parent != null)
				{
					CurrentNode=CurrentNode.Parent;
					if(CurrentNode.NextNode != null)
					{
						NextNode=CurrentNode.NextNode;
						return NextNode;
					}
				}
			}
			if(IsReRun&&NextNode==null) NextNode=CurrentNode.TreeView.Nodes[0];
			return NextNode;
		}


		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(this.treeView1.SelectedNode.Tag.ToString().Substring(0,4).Equals("SJDW")|| this.treeView1.SelectedNode.Tag.ToString().Substring(0,4).Equals("DWLX"))
			{
				return;
			}
			
			sNodeTag=this.treeView1.SelectedNode.Tag.ToString();
			sNodeName=this.treeView1.SelectedNode.Text;

			this.DialogResult=DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			sNodeTag="";
			sNodeName="";
			this.Close();
		}

		private void frmCheckedDisplaySelect_Load(object sender, System.EventArgs e)
		{
//			this.Text="选择" +ShareOption.DomainTitle ;
//			this.label3.Text=ShareOption.DomainTitle +"：";

            //2011-06-20注掉
			//if(frmMain.IsLoadCheckedDisplaySel) return;
			TreeNode posNode=null;
			string sCode=string.Empty;
            
			TreeNode theNode=new TreeNode();
			TreeNode preNode=new TreeNode();
			DataTable dt=null;
			FrmMain.formMain.Cursor=Cursors.WaitCursor;
			if(ShareOption.SystemVersion==ShareOption.LocalBaseVersion)
			{
				//得到检测单位的下级单位
				string sDistrictCode=clsUserUnitOpr.GetNameFromCode("DistrictCode","0001");
			
				clsDistrictOpr curObjOpr=new clsDistrictOpr();
				dt=curObjOpr.GetAsDataTable("SysCode Like '" + sDistrictCode + "_%'","SysCode",0);

				DataTable dt1=curObjectOpr.GetAsDataTable("","A.SysCode",0);

				if (dt.Rows.Count==0)
				{
					TreeNode comNode=null;
					TreeNode comsNode=null;

					clsCompanyKindOpr unitOpr2=new clsCompanyKindOpr();
					DataTable dt2=unitOpr2.GetAsDataTable("IsLock=0 And CompanyProperty='被检单位'","SysCode",1);
					if(dt2==null||dt2.Rows.Count==0)
					{
						MessageBox.Show(this,"单位类别没有设置，请设置!");
						FrmMain.formMain.Cursor=Cursors.Default;
						return;
					}
					else
					{
						for(int j=0;j<dt2.Rows.Count;j++)
						{
							theNode=new TreeNode();		
							theNode.Text=dt2.Rows[j]["Name"].ToString();
							theNode.Tag="DWLX"+dt2.Rows[j]["SysCode"].ToString();

							string sql="Property='被检单位'"
								+ " and KindCode = '" + dt2.Rows[j]["SysCode"].ToString()+ "' And DistrictCode='" + sDistrictCode +"' And Len(StdCode)=" + ShareOption.CompanyStdCodeLen.ToString();
							DataTable dt3=curObjectOpr.GetAsDataTable(sql,"SysCode",1);
							if(dt3==null||dt3.Rows.Count==0)
							{
							}
							else
							{
								for(int k=0;k<dt3.Rows.Count;k++)
								{
									comNode=new TreeNode();
									comNode.Text=dt3.Rows[k]["FullName"].ToString();
									comNode.Tag= "SJDW"+ dt3.Rows[k]["SysCode"].ToString();
									theNode.Nodes.Add(comNode);

									sql="Property='被检单位'"
										+ " And StdCode Like '" + dt3.Rows[k]["StdCode"].ToString() + "_%' And DisplayName<>''";
									DataTable dt4=curObjectOpr.GetAsDataTable(sql,"SysCode",1);
									if(dt4==null||dt4.Rows.Count==0)
									{
									}
									else
									{
										for(int l=0;l<dt4.Rows.Count;l++)
										{
											comsNode=new TreeNode();
											comsNode.Text=dt4.Rows[l]["DisplayName"].ToString();
											comsNode.Tag= dt4.Rows[l]["SysCode"].ToString();
											comNode.Nodes.Add(comsNode);
										}
									}
								}
							}
							this.treeView1.Nodes.Add(theNode);
						}
						if(this.treeView1.Nodes.Count>=1) this.treeView1.Nodes[0].Expand();
					}
				}
				else
				{
					for(int i=0;i<dt.Rows.Count;i++)
					{
						sCode=dt.Rows[i]["SysCode"].ToString();

						theNode=new TreeNode();				
						theNode.Text=dt.Rows[i]["Name"].ToString();
						theNode.Tag="XZJG"+sCode;
						//检查该节点是不是明细子节点，是就增加单位类型节点
						if(clsDistrictOpr.DistrictIsMX(sCode))
						{
							TreeNode comNode=null;
							TreeNode comsNode=null;

							clsCompanyKindOpr unitOpr2=new clsCompanyKindOpr();
							DataTable dt2=unitOpr2.GetAsDataTable("IsLock=0 And CompanyProperty='被检单位'","SysCode",1);
							if(dt2==null||dt2.Rows.Count==0)
							{
								MessageBox.Show(this,"单位类别没有设置，请设置!");
								FrmMain.formMain.Cursor=Cursors.Default;
								return;
							}
							else
							{
								for(int j=0;j<dt2.Rows.Count;j++)
								{
									TreeNode childNode=new TreeNode();				
									childNode.Text=dt2.Rows[j]["Name"].ToString();
									childNode.Tag="DWLX"+dt2.Rows[j]["SysCode"].ToString();
									theNode.Nodes.Add(childNode);

									string sql="Property='被检单位'"
										+ " and KindCode = '" + dt2.Rows[j]["SysCode"].ToString()+ "' And DistrictCode='" + sCode +"' And Len(StdCode)=" + ShareOption.CompanyStdCodeLen.ToString();
									DataTable dt3=curObjectOpr.GetAsDataTable(sql,"SysCode",1);
									if(dt3==null||dt3.Rows.Count==0)
									{
									}
									else
									{
										for(int k=0;k<dt3.Rows.Count;k++)
										{
											comNode=new TreeNode();
											comNode.Text=dt3.Rows[k]["FullName"].ToString();
											comNode.Tag= "SJDW"+dt3.Rows[k]["SysCode"].ToString();
											childNode.Nodes.Add(comNode);

											sql="Property='被检单位'"
												+ " And StdCode Like '" + dt3.Rows[k]["StdCode"].ToString() + "_%' And DisplayName<>''";
											DataTable dt4=curObjectOpr.GetAsDataTable(sql,"SysCode",1);
											if(dt4==null||dt4.Rows.Count==0)
											{
											}
											else
											{
												for(int l=0;l<dt4.Rows.Count;l++)
												{
													comsNode=new TreeNode();
													comsNode.Text=dt4.Rows[l]["DisplayName"].ToString();
													comsNode.Tag= dt4.Rows[l]["SysCode"].ToString();
													comNode.Nodes.Add(comsNode);


												}
											}
										}
									}

								}
							}
				
						}
						int iMod=(sCode.Length-sDistrictCode.Length)/ShareOption.DistrictCodeLevel;
						if(iMod==1)
						{
							this.treeView1.Nodes.Add(theNode);
						}
						else
						{
							prevNodes[iMod-2].Nodes.Add(theNode);
						}

						prevNodes[iMod-1]=theNode;

						if(this.treeView1.Nodes.Count>=1) this.treeView1.Nodes[0].Expand();
					}

					
				}
			}
			else
			{
				theNode=new TreeNode();
				theNode.Text=clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
				theNode.Tag=clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode);
				dt=curObjectOpr.GetAsDataTable("IsLock=false And Property='被检单位' And StdCode Like '" + theNode.Tag + "_%'","SysCode",1);
				if(dt!=null)
				{
					foreach(DataRow dr in dt.Rows)
					{
						sCode=dr["SysCode"].ToString();

						TreeNode childNode=new TreeNode();
						childNode.Text=dr["DisplayName"].ToString();
						childNode.Tag=sCode;
						if(_sCode.Equals(sCode))
						{
							posNode=childNode;
						}
						theNode.Nodes.Add(childNode);
					}
				}
				this.treeView1.Nodes.Add(theNode);
			
			}
			//frmMain.IsLoadCheckedDisplaySel=true;
			//定位treeview所在节点
			if(posNode!=null)
			{
				this.treeView1.SelectedNode=posNode;
				posNode.EnsureVisible();
			}
			else
			{
				this.treeView1.ExpandAll();
			}

			TreeNode CurrentNode=null;
			if(this.treeView1.Nodes.Count>0)
			{
				CurrentNode=this.treeView1.Nodes[0];
				while(CurrentNode!=null)
				{
					if(CurrentNode.Tag.ToString().Equals(_sCode))
					{
						this.treeView1.SelectedNode=CurrentNode;
						CurrentNode.EnsureVisible();
						CurrentNode.Expand();
						FrmMain.formMain.Cursor=Cursors.Default;
						return;
					}
					else
					{
						CurrentNode=this.GetNextNode(CurrentNode,false);
						if(CurrentNode==null)
						{
							this.treeView1.SelectedNode=this.treeView1.Nodes[0];
							this.treeView1.ExpandAll();
							FrmMain.formMain.Cursor=Cursors.Default;
							return;
						}
					}
				}
			}

			if(this.treeView1.Nodes.Count<=0)
			{
				this.btnOK.Enabled=false;
				this.txtName.Enabled=false;
			}
			FrmMain.formMain.Cursor=Cursors.Default;

		}

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.treeView1.SelectedNode.Tag.ToString().Substring(0,4).Equals("SJDW")|| this.treeView1.SelectedNode.Tag.ToString().Substring(0,4).Equals("DWLX"))
			{
				return;
			}
			
			sNodeTag=this.treeView1.SelectedNode.Tag.ToString();
			sNodeName=this.treeView1.SelectedNode.Text;

			this.DialogResult=DialogResult.OK;
			this.Close();		
		}

		private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0 && e.KeyCode==Keys.Enter)
			{
				selectNode(this.txtName.Text.Trim(),false);
			}
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0 )
			{
				selectNode(this.txtName.Text.Trim(),true);
			}
		}

	}
}
