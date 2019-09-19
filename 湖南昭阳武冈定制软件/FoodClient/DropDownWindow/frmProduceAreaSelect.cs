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
	/// frmProduceAreaSelect 的摘要说明。
	/// </summary>
	public class frmProduceAreaSelect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Button btnNext;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string _sCode;

		private clsProduceAreaOpr curObjectOpr;
		private TreeNode[] prevNodes;

		public string sNodeTag="";
		public string sNodeName="";

		public frmProduceAreaSelect(string pCode)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			curObjectOpr=new clsProduceAreaOpr();
			prevNodes=new TreeNode[ShareOption.MaxLevel];
			for(int i=0;i<ShareOption.MaxLevel;i++)
			{
				prevNodes[i]=new TreeNode();
			}

			_sCode=pCode;
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLocation = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(8, 32);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(384, 304);
            this.treeView1.TabIndex = 1;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(128, 21);
            this.txtName.TabIndex = 13;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "产地：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(224, 6);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(72, 24);
            this.btnLocation.TabIndex = 15;
            this.btnLocation.Text = "定位";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(304, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(72, 24);
            this.btnNext.TabIndex = 24;
            this.btnNext.Text = "下一个";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // frmProduceAreaSelect
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(402, 375);
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
            this.Name = "frmProduceAreaSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择产地";
            this.Load += new System.EventHandler(this.frmProduceAreaSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(this.treeView1.SelectedNode.GetNodeCount(false)>0) return;
			sNodeTag=this.treeView1.SelectedNode.Tag.ToString();
			sNodeName=this.treeView1.SelectedNode.Text;
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

        private void frmProduceAreaSelect_Load(object sender, System.EventArgs e)
        {
            this.treeView1.Nodes.Clear();

            DataTable dt = curObjectOpr.GetAsDataTable("IsLock=false And IsReadOnly=true", "SysCode", 0);

            TreeNode posNode = null;
            int iFirstLevel = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sCode = dt.Rows[i]["SysCode"].ToString();

                TreeNode theNode = new TreeNode();
                theNode.Text = dt.Rows[i]["Name"].ToString();
                theNode.Tag = sCode;
                if (_sCode.Equals(sCode))
                {
                    posNode = theNode;
                }

                int iMod = sCode.Length / ShareOption.DistrictCodeLevel;

                if (i == 0)
                {
                    iFirstLevel = iMod;
                    this.treeView1.Nodes.Add(theNode);
                }
                else
                {
                    prevNodes[iMod - iFirstLevel - 1].Nodes.Add(theNode);
                }

                prevNodes[iMod - iFirstLevel] = theNode;
            }

            //定位treeview所在节点
            if (this.treeView1.Nodes.Count <= 0)
            {
                this.btnOK.Enabled = false;
                this.txtName.Enabled = false;
                return;
            }
            if (posNode != null)
            {
                this.treeView1.SelectedNode = posNode;
                posNode.EnsureVisible();
            }
            else
            {
                this.treeView1.Nodes[0].Expand();
            }
        }

		private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0 && e.KeyCode==Keys.Enter)
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
				if(CurrentNode.Parent.NextNode != null)
				{
					NextNode=CurrentNode.Parent.NextNode;
				}
			}
			if(IsReRun&&NextNode==null) NextNode=CurrentNode.TreeView.Nodes[0];
			return NextNode;
		}

		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0)
			{
				selectNode(this.txtName.Text.Trim(),false);
			}
		}

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.treeView1.SelectedNode.GetNodeCount(false)>0) return;
			sNodeTag=this.treeView1.SelectedNode.Tag.ToString();
			sNodeName=this.treeView1.SelectedNode.Text;
			this.DialogResult=DialogResult.OK;
			this.Close();		
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text.Trim()!="" && this.treeView1.Nodes.Count>0)
			{
				selectNode(this.txtName.Text.Trim(),true);
			}		
		}
	}
}
