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
	/// frmDistrictSelect 的摘要说明。
	/// </summary>
    public class frmDistrictSelect : TitleBarBase
    {
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnNext;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        private string _sCode;
        private clsCompanyOpr curObjectOpr;
        public string sNodeTag = "";
        public string sNodeName = "";
        public string sNodeStd = "";
        private TreeNode[] prevNodes;
        private static DataTable dtRT = null;


        public frmDistrictSelect(string pCode, DataTable dt)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            curObjectOpr = new clsCompanyOpr();

            dtRT = dt;
            _sCode = pCode;
            prevNodes = new TreeNode[ShareOption.MaxLevel];
            for (int i = 0; i < ShareOption.MaxLevel; i++)
            {
                prevNodes[i] = new TreeNode();
            }
        }

        public void SetFormValues(string pCode)
        {
            _sCode = pCode;
            TreeNode CurrentNode = null;
            if (this.treeView1.Nodes.Count > 0)
            {
                CurrentNode = this.treeView1.Nodes[0];
                while (CurrentNode != null)
                {
                    if (CurrentNode.Tag.ToString().Equals(_sCode))
                    {
                        this.treeView1.SelectedNode = CurrentNode;
                        CurrentNode.EnsureVisible();
                        CurrentNode.Expand();
                        return;
                    }
                    else
                    {
                        CurrentNode = this.GetNextNode(CurrentNode, false);
                        if (CurrentNode == null)
                        {
                            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                            this.treeView1.ExpandAll();
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 清理所有正在使用的资源。
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
			this.btnLocation.Location = new System.Drawing.Point(234, 31);
			this.btnLocation.Name = "btnLocation";
			this.btnLocation.Size = new System.Drawing.Size(72, 24);
			this.btnLocation.TabIndex = 21;
			this.btnLocation.Text = "定位";
			this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(98, 33);
			this.txtName.MaxLength = 50;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(128, 21);
			this.txtName.TabIndex = 19;
			this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 21);
			this.label3.TabIndex = 20;
			this.label3.Text = "组织机构：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(314, 369);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(226, 369);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// treeView1
			// 
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(18, 57);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(384, 304);
			this.treeView1.TabIndex = 16;
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(314, 31);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(72, 24);
			this.btnNext.TabIndex = 22;
			this.btnNext.Text = "下一个";
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// frmDistrictSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
			this.ClientSize = new System.Drawing.Size(414, 412);
			this.ControlBox = false;
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnLocation);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.treeView1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDistrictSelect";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "选择所属组织机构";
			this.Load += new System.EventHandler(this.frmDistrictSelect_Load);
			this.Controls.SetChildIndex(this.treeView1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtName, 0);
			this.Controls.SetChildIndex(this.btnLocation, 0);
			this.Controls.SetChildIndex(this.btnNext, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion

        private void btnLocation_Click(object sender, System.EventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0)
            {
                selectNode(this.txtName.Text.Trim(), false);
            }
        }

        private void selectNode(string sNodeName, bool IsNext)
        {
            bool IsReRun = false;
            TreeNode CurrentNode = null;
            if (!IsNext)
            {
                CurrentNode = this.treeView1.Nodes[0];
            }
            else
            {
                CurrentNode = this.GetNextNode(this.treeView1.SelectedNode, true);
            }
           Regex r = new Regex(sNodeName,RegexOptions.IgnoreCase);
            while (CurrentNode != null)
            {
                Match m = r.Match(CurrentNode.Text);
                if (m.Success)
                {
                    this.treeView1.SelectedNode = CurrentNode;
                    CurrentNode.EnsureVisible();
                    return;
                }
                else
                {
                    if (IsReRun && IsNext && CurrentNode.Equals(this.treeView1.SelectedNode))
                    {
                        return;
                    }
                    CurrentNode = this.GetNextNode(CurrentNode, false);
                    if (CurrentNode == null && IsNext)
                    {
                        IsReRun = true;
                        CurrentNode = this.treeView1.Nodes[0];
                    }
                }
            }
        }

        private TreeNode GetNextNode(TreeNode currentNode, bool IsReRun)
        {
            TreeNode NextNode = null;
            //得到下一个节点
            if (currentNode.FirstNode != null)
            {
                NextNode = currentNode.FirstNode;
            }
            else if (currentNode.NextNode != null)
            {
                NextNode = currentNode.NextNode;
            }
            else if (currentNode.Parent != null)
            {
                while (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                    if (currentNode.NextNode != null)
                    {
                        NextNode = currentNode.NextNode;
                        return NextNode;
                    }
                }
            }
            if (IsReRun && NextNode == null)
            {
                NextNode = currentNode.TreeView.Nodes[0];
            }
            return NextNode;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string temp = string.Empty;
            sNodeTag = this.treeView1.SelectedNode.Tag.ToString();
            temp = this.treeView1.SelectedNode.Text;
            sNodeName = temp.Substring(temp.IndexOf("-") + 1);
            sNodeStd = temp.Substring(0, temp.IndexOf("-"));
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            sNodeTag = string.Empty;
            sNodeName = string.Empty;
        }

        private void frmDistrictSelect_Load(object sender, System.EventArgs e)
        {
			TitleBarText = "选择所属机构";

            FrmMain.formMain.Cursor = Cursors.WaitCursor;
            if (dtRT == null || dtRT.Rows.Count == 0)
            {
                MessageBox.Show(this, "所属机构没有设置，请设置!");
                FrmMain.formMain.Cursor = Cursors.Default;
                return;
            }

            TreeNode posNode = null;
            int intMin = 0;
            string code = string.Empty;
            int iMod = 0;
            for (int i = 0; i < dtRT.Rows.Count; i++)
            {
                 code = dtRT.Rows[i]["SysCode"].ToString();

                 iMod = code.Length / ShareOption.DistrictCodeLevel;
                if (i == 0)
                {
                    intMin = iMod;
                }
                else if (iMod < intMin)
                {
                    intMin = iMod;
                }
            }
            for (int i = 0; i < dtRT.Rows.Count; i++)
            {
                code = dtRT.Rows[i]["SysCode"].ToString();

                TreeNode theNode = new TreeNode();
                theNode.Text = dtRT.Rows[i]["StdCode"].ToString() + "-" + dtRT.Rows[i]["Name"].ToString();
                theNode.Tag = code;
                if (code.Equals(_sCode))
                {
                    posNode = theNode;
                }

                iMod = code.Length / ShareOption.DistrictCodeLevel;
                if (iMod == intMin)
                {
                    this.treeView1.Nodes.Add(theNode);
                }
                else
                {
                    prevNodes[iMod - 2].Nodes.Add(theNode);
                }

                prevNodes[iMod - 1] = theNode;
            }

            //定位treeview所在节点
            if (posNode != null)
            {
                this.treeView1.SelectedNode = posNode;
                posNode.EnsureVisible();
            }
            else
            {
                this.treeView1.ExpandAll();
            }

            TreeNode CurrentNode = null;
            if (this.treeView1.Nodes.Count > 0)
            {
                CurrentNode = this.treeView1.Nodes[0];
                while (CurrentNode != null)
                {
                    if (CurrentNode.Tag.ToString().Equals(_sCode))
                    {
                        this.treeView1.SelectedNode = CurrentNode;
                        CurrentNode.EnsureVisible();
                        CurrentNode.Expand();
                        FrmMain.formMain.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        CurrentNode = this.GetNextNode(CurrentNode, false);
                        if (CurrentNode == null)
                        {
                            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                            this.treeView1.ExpandAll();
                            FrmMain.formMain.Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
            }

            if (this.treeView1.Nodes.Count <= 0)
            {
                this.btnOK.Enabled = false;
                this.txtName.Enabled = false;
            }
            FrmMain.formMain.Cursor = Cursors.Default;

			
        }

        private void treeView1_DoubleClick(object sender, System.EventArgs e)
        {

            string temp = string.Empty;
            sNodeTag = this.treeView1.SelectedNode.Tag.ToString();
            temp = this.treeView1.SelectedNode.Text;
            sNodeName = temp.Substring(temp.IndexOf("-") + 1);
            sNodeStd = temp.Substring(0, temp.IndexOf("-"));

            this.DialogResult = DialogResult.OK;
        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0 && e.KeyCode == Keys.Enter)
            {
                selectNode(this.txtName.Text.Trim(), false);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0)
            {
                selectNode(this.txtName.Text.Trim(), true);
            }
        }

		protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
		{

		}


    }
}
