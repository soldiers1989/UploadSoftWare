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
    /// frmItem 的摘要说明。
    /// </summary>
    public class frmItem : System.Windows.Forms.Form
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
        public C1.Win.C1Command.C1Command cmdAdd;
        private C1.Win.C1Command.C1Command cmdEdit;
        private C1.Win.C1Command.C1Command cmdDelete;
        private C1.Win.C1Command.C1Command cmdPrint;
        private C1.Win.C1Command.C1Command cmdExit;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.TreeView treeView1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid2;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem itmAdd;
        private System.Windows.Forms.MenuItem itmEdit;
        private System.Windows.Forms.MenuItem itmDelete;
        private System.ComponentModel.IContainer components;

        private clsCheckItem curObject;
        private readonly clsCheckItemOpr curObjectOpr;

        private static char prevChar = 'S';

        private readonly clsStandardTypeOpr curStdTypeOpr;

        private clsStandard curStd;
        private System.Windows.Forms.ToolTip toolTip1;
        private C1.Win.C1Command.C1Command cmdStandAdd;
        private C1.Win.C1Command.C1Command cmdStandEdit;
        private C1.Win.C1Command.C1Command cmdStandDelete;
        private C1.Win.C1Command.C1CommandLink c1CommandLink7;
        private C1.Win.C1Command.C1CommandLink c1CommandLink8;
        private C1.Win.C1Command.C1CommandLink c1CommandLink9;
        private clsStandardOpr curStdOpr;

        public frmItem()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            curObjectOpr = new clsCheckItemOpr();
            curStdTypeOpr = new clsStandardTypeOpr();
            curStdOpr = new clsStandardOpr();
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItem));
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.cmdAdd = new C1.Win.C1Command.C1Command();
            this.cmdEdit = new C1.Win.C1Command.C1Command();
            this.cmdDelete = new C1.Win.C1Command.C1Command();
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.cmdStandAdd = new C1.Win.C1Command.C1Command();
            this.cmdStandEdit = new C1.Win.C1Command.C1Command();
            this.cmdStandDelete = new C1.Win.C1Command.C1Command();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink8 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
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
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.itmAdd = new System.Windows.Forms.MenuItem();
            this.itmEdit = new System.Windows.Forms.MenuItem();
            this.itmDelete = new System.Windows.Forms.MenuItem();
            this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).BeginInit();
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
            this.c1CommandHolder1.Commands.Add(this.cmdStandAdd);
            this.c1CommandHolder1.Commands.Add(this.cmdStandEdit);
            this.c1CommandHolder1.Commands.Add(this.cmdStandDelete);
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
            // cmdStandAdd
            // 
            this.cmdStandAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdStandAdd.Image")));
            this.cmdStandAdd.Name = "cmdStandAdd";
            this.cmdStandAdd.Text = "新增标准";
            this.cmdStandAdd.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdStandAdd_Click);
            this.cmdStandAdd.Select += new System.EventHandler(this.cmdStandAdd_Select);
            // 
            // cmdStandEdit
            // 
            this.cmdStandEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdStandEdit.Image")));
            this.cmdStandEdit.Name = "cmdStandEdit";
            this.cmdStandEdit.Text = "修改标准";
            this.cmdStandEdit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdStandEdit_Click);
            this.cmdStandEdit.Select += new System.EventHandler(this.cmdStandEdit_Select);
            // 
            // cmdStandDelete
            // 
            this.cmdStandDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdStandDelete.Image")));
            this.cmdStandDelete.Name = "cmdStandDelete";
            this.cmdStandDelete.Text = "删除标准";
            this.cmdStandDelete.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdStandDelete_Click);
            this.cmdStandDelete.Select += new System.EventHandler(this.cmdStandDelete_Select);
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(702, 24);
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink7,
            this.c1CommandLink8,
            this.c1CommandLink9,
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
            this.c1ToolBar1.Size = new System.Drawing.Size(702, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandLink7
            // 
            this.c1CommandLink7.Command = this.cmdStandAdd;
            // 
            // c1CommandLink8
            // 
            this.c1CommandLink8.Command = this.cmdStandEdit;
            // 
            // c1CommandLink9
            // 
            this.c1CommandLink9.Command = this.cmdStandDelete;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdAdd;
            this.c1CommandLink1.Delimiter = true;
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
            this.statusBar1.Location = new System.Drawing.Point(0, 413);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(702, 25);
            this.statusBar1.TabIndex = 4;
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.c1FlexGrid1);
            this.c1Sizer1.Controls.Add(this.treeView1);
            this.c1Sizer1.Controls.Add(this.c1FlexGrid2);
            this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Sizer1.GridDefinition = "49.1002570694087:True:False;47.8149100257069:False:False;\t19.5156695156695:True:F" +
                "alse;78.7749287749288:False:False;";
            this.c1Sizer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Sizer1.Location = new System.Drawing.Point(0, 24);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(702, 389);
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.TabStop = false;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Location = new System.Drawing.Point(145, 199);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MaxSize = 200;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(553, 186);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 13;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenu = this.contextMenu1;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(137, 381);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmAdd,
            this.itmEdit,
            this.itmDelete});
            // 
            // itmAdd
            // 
            this.itmAdd.Index = 0;
            this.itmAdd.Text = "新增标准";
            this.itmAdd.Click += new System.EventHandler(this.itmAdd_Click);
            // 
            // itmEdit
            // 
            this.itmEdit.Index = 1;
            this.itmEdit.Text = "修改标准";
            this.itmEdit.Click += new System.EventHandler(this.itmEdit_Click);
            // 
            // itmDelete
            // 
            this.itmDelete.Index = 2;
            this.itmDelete.Text = "删除标准";
            this.itmDelete.Click += new System.EventHandler(this.itmDelete_Click);
            // 
            // c1FlexGrid2
            // 
            this.c1FlexGrid2.AllowEditing = false;
            this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
            this.c1FlexGrid2.Location = new System.Drawing.Point(145, 4);
            this.c1FlexGrid2.Name = "c1FlexGrid2";
            this.c1FlexGrid2.Rows.Count = 1;
            this.c1FlexGrid2.Rows.DefaultSize = 18;
            this.c1FlexGrid2.Rows.MaxSize = 200;
            this.c1FlexGrid2.Rows.MinSize = 20;
            this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid2.Size = new System.Drawing.Size(553, 191);
            this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
            this.c1FlexGrid2.TabIndex = 13;
            // 
            // frmItem
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(702, 438);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.c1CommandDock1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测项目维护";
            this.Load += new System.EventHandler(this.frmItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 编辑检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int row = this.c1FlexGrid1.RowSel;
            if (this.treeView1.Nodes.Count == 0 || row <= 0)
            {
                return;
            }

            curObject = new clsCheckItem();
            curObject.SysCode = this.c1FlexGrid1.Rows[row]["SysCode"].ToString();
            curObject.StdCode = this.c1FlexGrid1.Rows[row]["StdCode"].ToString();
            curObject.ItemDes = this.c1FlexGrid1.Rows[row]["ItemDes"].ToString();
            curObject.CheckType = this.c1FlexGrid1.Rows[row]["CheckType"].ToString();
            curObject.StandardCode = this.c1FlexGrid1.Rows[row]["StandardCode"].ToString();
            curObject.StandardValue = this.c1FlexGrid1.Rows[row]["StandardValue"].ToString();
            curObject.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
            curObject.DemarcateInfo = this.c1FlexGrid1.Rows[row]["DemarcateInfo"].ToString();
            curObject.TestValue = this.c1FlexGrid1.Rows[row]["TestValue"].ToString();
            curObject.OperateHelp = this.c1FlexGrid1.Rows[row]["OperateHelp"].ToString();
            curObject.CheckLevel = this.c1FlexGrid1.Rows[row]["CheckLevel"].ToString();
            curObject.Remark = this.c1FlexGrid1.Rows[row]["Remark"].ToString();
            curObject.IsLock = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsLock"]);
            curObject.IsReadOnly = Convert.ToBoolean(this.c1FlexGrid1.Rows[row]["IsReadOnly"]);

            frmItemEdit frm = new frmItemEdit();
            frm.Tag = "EDIT";
            frm.setValue(curObject);
            DialogResult dr = frm.ShowDialog(this);

            //刷新窗体中的Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 增加检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (this.treeView1.Nodes.Count == 0)
            {
                return;
            }
            curObject = new clsCheckItem();
            if (this.treeView1.SelectedNode != null
                && this.nodeIsStdType(this.treeView1.SelectedNode)
                && this.c1FlexGrid1.RowSel == -1)
            {
                curObject.SysCode = 1.ToString().PadLeft(ShareOption.CheckItemCodeLen, '0');
            }
            else
            {
                string curCode = string.Empty;

                //				if(!this.nodeIsStdType(this.treeView1.SelectedNode))
                //				{
                //					curCode=this.treeView1.SelectedNode.Tag.ToString();
                //				}

                string sErr = string.Empty;
                int max = curObjectOpr.GetMaxNO(curCode, out sErr);
                string syscode = (max + 1).ToString().PadLeft(ShareOption.CheckItemCodeLen, '0');
                curObject.SysCode = syscode;
            }

            if (ShareOption.SysStdCodeSame)
            {
                curObject.StdCode = curObject.SysCode;
            }
            else
            {
                curObject.StdCode = string.Empty;
            }
            curObject.ItemDes = string.Empty;
            curObject.CheckType = string.Empty;
            curObject.StandardCode = string.Empty;
            curObject.StandardValue = string.Empty;
            curObject.DemarcateInfo = string.Empty;
            curObject.TestValue = string.Empty;
      
            curObject.Unit = string.Empty;
      
            curObject.OperateHelp = string.Empty;
            curObject.CheckLevel = string.Empty;
            curObject.Remark = string.Empty;
            curObject.IsLock = false;
            curObject.IsReadOnly = ShareOption.DefaultIsReadOnly;
            int row = this.c1FlexGrid1.RowSel;
            if (row >= 1)
            {
                curObject.StandardCode = this.c1FlexGrid1.Rows[row]["StandardCode"].ToString();
                curObject.StandardValue = this.c1FlexGrid1.Rows[row]["StandardValue"].ToString();
                curObject.DemarcateInfo = this.c1FlexGrid1.Rows[row]["DemarcateInfo"].ToString();
                curObject.TestValue = this.c1FlexGrid1.Rows[row]["TestValue"].ToString();
                curObject.Unit = this.c1FlexGrid1.Rows[row]["Unit"].ToString();
            }
            int row1 = this.c1FlexGrid2.RowSel;
            if (row1 >= 1 && curObject.StandardCode.Equals(""))
            {
                curObject.StandardCode = this.c1FlexGrid2.Rows[row1]["stdCode"].ToString();
            }
            frmItemEdit frm = new frmItemEdit();
            frm.Tag = "ADD";
            frm.setValue(curObject);
            DialogResult dr = frm.ShowDialog(this);

            //刷新窗体中的Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

        private void cmdDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //判断是否有删除的记录
            if (!this.c1FlexGrid1.Focused || this.c1FlexGrid1.RowSel == -1 || this.c1FlexGrid1.RowSel == 0)
            {
                MessageBox.Show(this, "请选择将要删除的记录！");
                return;
            }

            string delStr = this.c1FlexGrid1[this.c1FlexGrid1.RowSel, 0].ToString().Trim();
            ////判断是否可以删除
            //if (!curObjectOpr.CanDelete(delStr))
            //{
            //    MessageBox.Show(this, "此记录不能被删除，请先删除相关的记录！");
            //    return;
            //}

            //让用户确定删除操作
            if (MessageBox.Show(this, "确定要删除选择的记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            //删除记录
            string sErr =string.Empty;
            curObjectOpr.DeleteByPrimaryKey(delStr, out sErr);
            if (!sErr.Equals(""))
            {
                MessageBox.Show(this, "数据库操作出错！");
            }

            this.refreshGrid(this.treeView1.SelectedNode);
        }

        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //PrintOperation.PrintGrid(this.c1FlexGrid1,"检测项目列表",null);
            PrintOperation.PreviewGrid(this.c1FlexGrid1, "检测项目列表", null);
        }

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }

            string sCoding = string.Empty;
            string sStdCode = string.Empty;
            string whereSql = string.Empty;
            if (this.nodeIsStdType(e.Node))
            {
                sCoding = e.Node.Text;
                whereSql = "StdType='" + sCoding + "'";
            }
            else
            {
                sStdCode = e.Node.Tag.ToString();
                whereSql = "SysCode='" + sStdCode + "'";
            }

            DataTable dt2 = curStdOpr.GetAsDataTable(whereSql, "SysCode", 0);
            this.c1FlexGrid2.SetDataBinding(dt2.DataSet, "Standard");
            this.setGrid2Style();
            this.c1FlexGrid2.AutoSizeCols();
            DataTable dt3 = curObjectOpr.GetAsDataTable1("A.StandardCode ='" + sStdCode + "'", "A.SysCode", 0);
            this.c1FlexGrid1.SetDataBinding(dt3.DataSet, "CheckItem");
            this.setGridStyle();
            this.c1FlexGrid1.AutoSizeCols();
        }

        private void refreshGrid(TreeNode oprNode)
        {
            this.treeView1.Nodes.Clear();

            DataTable dtStdType = curStdTypeOpr.GetAsDataTable("IsLock=0", "StdName", 0);
            if (dtStdType == null || dtStdType.Rows.Count == 0)
            {
                MessageBox.Show(this, "数据库中数据有误！");
                return;
            }

            string sTag = null;
            bool bIsStdType = false;
            if (oprNode != null)
            {
                sTag = oprNode.Tag.ToString();
                bIsStdType = this.nodeIsStdType(oprNode);
            }
            TreeNode posNode = null;

            for (int i = 0; i < dtStdType.Rows.Count; i++)
            {
                TreeNode childNode = new TreeNode();
                childNode.Text = dtStdType.Rows[i]["StdName"].ToString();

                //i.ToString()的长度不能超过ShareOption.CheckItemCodeLen-1位

                childNode.Tag = i.ToString().PadLeft(ShareOption.CheckItemCodeLen, prevChar);
                this.treeView1.Nodes.Add(childNode);

                if (bIsStdType && sTag != null && childNode.Tag.ToString().Equals(sTag))
                {
                    posNode = childNode;
                }
            }

            DataTable dtStd = curStdOpr.GetAsDataTable("", "SysCode", 0);
            for (int i = 0; i < dtStd.Rows.Count; i++)
            {
                string sCode = dtStd.Rows[i]["StdType"].ToString();

                foreach (TreeNode node in this.treeView1.Nodes)
                {
                    if (node.Text.Equals(sCode))
                    {
                        TreeNode chNode = new TreeNode();
                        chNode.Text = dtStd.Rows[i]["StdDes"].ToString();
                        chNode.Tag = dtStd.Rows[i]["SysCode"].ToString();
                        node.Nodes.Add(chNode);

                        if (!bIsStdType && sTag != null && chNode.Tag.ToString().Equals(sTag))
                        {
                            posNode = chNode;
                        }

                        continue;
                    }
                }
            }

            string sCoding = string.Empty;
            string sStdCode = string.Empty;
            string whereSql = string.Empty;
            if (oprNode == null)
            {
                sCoding = dtStdType.Rows[0]["StdName"].ToString();
                whereSql = "StdType='" + sCoding + "'";
            }
            else
            {
                if (this.nodeIsStdType(oprNode))
                {
                    sCoding = oprNode.Text;
                    whereSql = "StdType='" + sCoding + "'";
                }
                else
                {
                    sStdCode = oprNode.Tag.ToString();
                    whereSql = "SysCode='" + sCoding + "'";
                }
            }

            DataTable dt2 = curStdOpr.GetAsDataTable(whereSql, "SysCode", 0);
            this.c1FlexGrid2.SetDataBinding(dt2.DataSet, "Standard");
            this.setGrid2Style();
            this.c1FlexGrid2.AutoSizeCols();

            DataTable dt3 = curObjectOpr.GetAsDataTable1("A.StandardCode ='" + sStdCode + "'", "A.SysCode", 0);
            this.c1FlexGrid1.SetDataBinding(dt3.DataSet, "CheckItem");
            this.setGridStyle();
            this.c1FlexGrid1.AutoSizeCols();

            //定位treeview所在节点
            if (posNode != null)
            {
                this.treeView1.SelectedNode = posNode;
                posNode.EnsureVisible();
            }
        }

        private void setGridStyle()
        {
            this.c1FlexGrid1.Cols["SysCode"].Caption = "系统编号";
            this.c1FlexGrid1.Cols["StdCode"].Caption = "编号";
            this.c1FlexGrid1.Cols["ItemDes"].Caption = "检测项目名称";
            this.c1FlexGrid1.Cols["CheckType"].Caption = "检测类型";
            this.c1FlexGrid1.Cols["StdDes"].Caption = "检测标准";
            this.c1FlexGrid1.Cols["StandardValue"].Caption = "标准值";
            this.c1FlexGrid1.Cols["Unit"].Caption = "单位";
            this.c1FlexGrid1.Cols["DemarcateInfo"].Caption = "标准值符号";
            this.c1FlexGrid1.Cols["TestValue"].Caption = "测试合格值";
            this.c1FlexGrid1.Cols["OperateHelp"].Caption = "操作帮助";
            this.c1FlexGrid1.Cols["CheckLevel"].Caption = "监控级别";
            this.c1FlexGrid1.Cols["IsReadOnly"].Caption = "已审核";
            this.c1FlexGrid1.Cols["IsLock"].Caption = "停用";
            this.c1FlexGrid1.Cols["Remark"].Caption = "备注";

            this.c1FlexGrid1.Cols["SysCode"].Visible = false;
            this.c1FlexGrid1.Cols["StandardCode"].Visible = false;
            this.c1FlexGrid1.Cols["OperateHelp"].Visible = false;
            //			this.c1FlexGrid1.Cols["IsReadOnly"].Visible=false;
            //			this.c1FlexGrid1.Cols["CheckLevel"].Visible=false;

            this.c1FlexGrid1.Cols["StdCode"].Width = 60;
            this.c1FlexGrid1.Cols["ItemDes"].Width = 300;
            this.c1FlexGrid1.Cols["CheckType"].Width = 60;
            this.c1FlexGrid1.Cols["StandardValue"].Width = 60;
            this.c1FlexGrid1.Cols["Unit"].Width = 100;
            this.c1FlexGrid1.Cols["DemarcateInfo"].Width = 60;
            this.c1FlexGrid1.Cols["TestValue"].Width = 60;
            this.c1FlexGrid1.Cols["IsLock"].Width = 30;
            this.c1FlexGrid1.Cols["Remark"].Width = 300;
        }

        private void setGrid2Style()
        {
            this.c1FlexGrid2.Cols["SysCode"].Caption = "系统编号";
            this.c1FlexGrid2.Cols["StdCode"].Caption = "编号";
            this.c1FlexGrid2.Cols["StdDes"].Caption = "检测标准名称";
            this.c1FlexGrid2.Cols["ShortCut"].Caption = "快捷编码";
            this.c1FlexGrid2.Cols["StdInfo"].Caption = "检测标准信息";
            this.c1FlexGrid2.Cols["StdType"].Caption = "检测标准类型";
            this.c1FlexGrid2.Cols["IsReadOnly"].Caption = "已审核";
            this.c1FlexGrid2.Cols["IsLock"].Caption = "停用";
            this.c1FlexGrid2.Cols["Remark"].Caption = "备注";

            this.c1FlexGrid2.Cols["SysCode"].Visible = false;
            //this.c1FlexGrid2.Cols["IsReadOnly"].Visible=false;

            this.c1FlexGrid2.Cols["StdCode"].Width = 60;
            this.c1FlexGrid2.Cols["StdDes"].Width = 300;
            this.c1FlexGrid2.Cols["ShortCut"].Width = 60;
            this.c1FlexGrid2.Cols["StdInfo"].Width = 300;
            this.c1FlexGrid2.Cols["StdType"].Width = 100;
            this.c1FlexGrid2.Cols["IsLock"].Width = 30;
            this.c1FlexGrid2.Cols["Remark"].Width = 300;
        }

        private void frmItem_Load(object sender, System.EventArgs e)
        {
            this.refreshGrid(null);

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }
        }

        //判断点中的treeview节点是标准的类型还是标准
        //调用前请一定先判断TreeNode是否为null
        private bool nodeIsStdType(TreeNode node)
        {
            if (node.Tag.ToString().Substring(0, 1).Equals(prevChar.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void itmAdd_Click(object sender, System.EventArgs e)
        {
            if (this.treeView1.Nodes.Count == 0 || this.treeView1.SelectedNode == null)
            {
                return;
            }

            curStd = new clsStandard();
            //获取一个新的系统编号
            string sStdTypeCode = "";
            if (this.nodeIsStdType(this.treeView1.SelectedNode))
            {
                sStdTypeCode = this.treeView1.SelectedNode.Text;
            }
            else
            {
                sStdTypeCode = this.treeView1.SelectedNode.Parent.Text;
            }

            string sErr = "";
            int max = curStdOpr.GetMaxNO(out sErr);
            string syscode = (max + 1).ToString().PadLeft(ShareOption.StandardCodeLen, '0');
            curStd.SysCode = syscode;

            if (ShareOption.SysStdCodeSame)
            {
                curStd.StdCode = curStd.SysCode;
            }
            else
            {
                curStd.StdCode = "";
            }
            curStd.StdDes = "";
            curStd.ShortCut = "";
            curStd.StdInfo = "";
            curStd.StdType = sStdTypeCode;
            curStd.Remark = "";
            curStd.IsLock = false;
            curStd.IsReadOnly = ShareOption.DefaultIsReadOnly;

            frmStandardEdit frm = new frmStandardEdit();
            frm.Tag = "ADD";
            frm.setValue(curStd);
            DialogResult dr = frm.ShowDialog(this);

            //刷新窗体中的Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

        private void itmEdit_Click(object sender, System.EventArgs e)
        {
            if (this.treeView1.Nodes.Count == 0 || this.treeView1.SelectedNode == null || this.nodeIsStdType(this.treeView1.SelectedNode))
            {
                MessageBox.Show(this, "请选择将要修改的记录！");
                return;
            }

            int row = this.c1FlexGrid2.RowSel;
            if (row <= 0)
            {
                return;
            }

            curStd = new clsStandard();
            curStd.SysCode = this.c1FlexGrid2.Rows[row]["SysCode"].ToString();
            curStd.StdCode = this.c1FlexGrid2.Rows[row]["StdCode"].ToString();
            curStd.StdDes = this.c1FlexGrid2.Rows[row]["StdDes"].ToString();
            curStd.ShortCut = this.c1FlexGrid2.Rows[row]["ShortCut"].ToString();
            curStd.StdInfo = this.c1FlexGrid2.Rows[row]["StdInfo"].ToString();
            curStd.StdType = this.c1FlexGrid2.Rows[row]["StdType"].ToString();
            curStd.Remark = this.c1FlexGrid2.Rows[row]["Remark"].ToString();
            curStd.IsLock = Convert.ToBoolean(this.c1FlexGrid2.Rows[row]["IsLock"]);
            curStd.IsReadOnly = Convert.ToBoolean(this.c1FlexGrid2.Rows[row]["IsReadOnly"]);

            frmStandardEdit frm = new frmStandardEdit();
            frm.Tag = "EDIT";
            frm.setValue(curStd);
            DialogResult dr = frm.ShowDialog(this);

            //刷新窗体中的Grid
            if (dr == DialogResult.OK)
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
        }

        private void itmDelete_Click(object sender, System.EventArgs e)
        {
            //判断是否有删除的记录
            if (this.treeView1.Nodes.Count == 0 || this.treeView1.SelectedNode == null || this.nodeIsStdType(this.treeView1.SelectedNode))
            {
                MessageBox.Show(this, "请选择将要删除的记录！");
                return;
            }

            string delStr = this.c1FlexGrid2[this.c1FlexGrid2.RowSel, 0].ToString().Trim();

            //让用户确定删除操作
            if (MessageBox.Show(this, "确定要删除选择的记录及相关记录？", "询问", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            //删除记录
            string sErr = string.Empty;
            curStdOpr.DeleteByPrimaryKey(delStr, out sErr);
            //if (!sErr.Equals(""))
            //{
            //    MessageBox.Show(this, "数据库操作出错！");
            //}
            if (this.nodeIsStdType(this.treeView1.SelectedNode))
            {
                this.refreshGrid(this.treeView1.SelectedNode);
            }
            else
            {
                TreeNode node = this.treeView1.SelectedNode;
                TreeNode posNode = null;
                if (this.treeView1.SelectedNode.PrevNode == null)
                {
                    if (this.treeView1.SelectedNode.NextNode == null)
                    {
                        posNode = this.treeView1.SelectedNode.Parent;
                    }
                    else
                    {
                        posNode = this.treeView1.SelectedNode.NextNode;
                    }
                }
                else
                {
                    posNode = this.treeView1.SelectedNode.PrevNode;
                }
                this.treeView1.SelectedNode = posNode;
                this.treeView1.Nodes.Remove(node);
            }

        }

        private void cmdAdd_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdAdd.Text);
        }


        private void cmdEdit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdEdit.Text);
        }

        private void cmdDelete_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdDelete.Text);
        }

        private void cmdPrint_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdPrint.Text);
        }

        private void cmdExit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdExit.Text);
        }

        private void cmdStandAdd_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.itmAdd_Click(null, null);
        }

        private void cmdStandEdit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.itmEdit_Click(null, null);
        }

        private void cmdStandDelete_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.itmDelete_Click(null, null);
        }

        private void cmdStandAdd_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdStandAdd.Text);
        }

        private void cmdStandEdit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdStandEdit.Text);
        }

        private void cmdStandDelete_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdStandDelete.Text);
        }
    }
}
