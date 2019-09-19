using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClientTools
{
    public partial class FrmMachineMng : Form
    {
        /// <summary>
        /// 检测项目组合字符串
        /// </summary>
        private string linkStdCode = string.Empty;
        private DataTable initDtbl = null;// new DataTable("initDtbl");
        private bool IsCreatDT = false;
        private readonly clsMachineOpr machBll = new clsMachineOpr();

        //private bool IsRead = false;
        // public static clsDY3000DY curDY3000DY;
        //private static bool IsDY3000DYOpen = false;

        /// <summary>
        /// 系统编码
        /// </summary>
        private string sysCode = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMachineMng(string code)
        {
            InitializeComponent();

            txtSysCode.Text = sysCode = code;

            //设置串口号数
            object[] obj =new object[256];
            for (int i = 0; i < 256; i++)
            {
                obj[i] = "COM" + (i + 1).ToString() + ":";
            }
            cmbCom.Items.AddRange(obj);

            if (initDtbl == null)
            {
                initDtbl = new DataTable("initDtbl");
            }
            if (!IsCreatDT)
            {
                createDataTable();
                IsCreatDT = true;
            }
        }

        /// <summary>
        /// 构造DataTable对象
        /// </summary>
        private void createDataTable()
        {
            DataColumn dCol = new DataColumn();
            dCol.DataType = System.Type.GetType("System.String");
            dCol.ColumnName = "ShortName";//ShortName检测功能简称
            initDtbl.Columns.Add(dCol);

            dCol = new DataColumn();
            dCol.DataType = System.Type.GetType("System.String");
            dCol.ColumnName = "ItemName";//ItemName对应检测项目
            initDtbl.Columns.Add(dCol);

            dCol = new DataColumn();
            dCol.DataType = System.Type.GetType("System.String");
            dCol.ColumnName = "ItemCode";//ItemCode检测项目代码
            initDtbl.Columns.Add(dCol);

            dCol = new DataColumn();
            dCol.DataType = System.Type.GetType("System.String");
            dCol.ColumnName = "CheckMethod";//CheckMethod检测项目方法
            initDtbl.Columns.Add(dCol);

            dCol = new DataColumn();
            dCol.DataType = System.Type.GetType("System.String");
            dCol.ColumnName = "Unit";//Unit检测值单位
            initDtbl.Columns.Add(dCol);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            winClose();
        }

        /// <summary>
        ///  初始化检测项目对应值下拉框
        /// </summary>
        private void initCheckItem()
        {
            clsCheckItemOpr opr1 = new clsCheckItemOpr();
            DataTable dtblItem = opr1.GetAsDataTable("IsLock=false", "SysCode", 1);
            cmbCheckItem.DataSource = dtblItem;
            //cmbCheckItem.DataMember = "CheckItem";
            cmbCheckItem.DisplayMember = "ItemDes";
            cmbCheckItem.ValueMember = "SysCode";
            //cmbCheckItem.Columns["StdCode"].Caption = "编号";
            //cmbCheckItem.Columns["ItemDes"].Caption = "检测项目";
            //cmbCheckItem.Columns["SysCode"].Caption = "系统编号";

        }

        /// <summary>
        /// 初始各项值
        /// </summary>
        /// <param name="machineModel"></param>
        private void setFormValue()
        {
            if (!string.IsNullOrEmpty(sysCode))
            {
                DataTable dtblForm = null;

                dtblForm = machBll.GetColumnDataTable(1, string.Format("SysCode='{0}'", sysCode), "SysCode,MachineName,MachineModel,Company,Protocol,IsSupport,LinkComNo,TestSign,TestValue,LinkStdCode,IsShow,OrderId");
                if (dtblForm != null)
                {
                    //txtSysCode.Text = sysCode;// dtblForm.Rows[0]["SysCode"].ToString();
                    txtName.Text = dtblForm.Rows[0]["MachineName"].ToString();
                    txtModel.Text = dtblForm.Rows[0]["MachineModel"].ToString();
                    txtCompany.Text = dtblForm.Rows[0]["Company"].ToString();
                    txtProtocol.Text = dtblForm.Rows[0]["Protocol"].ToString();
                    chkIsDef.Checked = Convert.ToBoolean(dtblForm.Rows[0]["IsSupport"]);
                    chbShow.Checked = Convert.ToBoolean(dtblForm.Rows[0]["IsShow"]);
                    cmbCom.SelectedIndex = Convert.ToInt32(dtblForm.Rows[0]["LinkComNo"]) - 1;
                    txtOrderId.Text = dtblForm.Rows[0]["OrderId"].ToString();
                    cmbSign.Text = dtblForm.Rows[0]["TestSign"].ToString();
                    txtTestValue.Text = dtblForm.Rows[0]["TestValue"].ToString();
                    linkStdCode = dtblForm.Rows[0]["LinkStdCode"].ToString();

                }
            }
            else
            {
                txtTestValue.Text = "-1";
            }

            //if (txtProtocol.Text.Equals("RS232DY3000") || txtProtocol.Text.Equals("RS232DY5000") || txtProtocol.Text.Equals("RS232DY5000LD"))
            //{
            //    btnAddItems.Text = "增加检测功能";
            //    btnAddItems.Top = 88;
            //    btnAddItems.Visible = true;
            //    btnDelItems.Visible = true;
            //}
            //else if (txtProtocol.Text.Equals("RS232DY3000DY"))
            //{
            //    btnAddItems.Text = "读取检测功能";
            //    btnAddItems.Top = 112;
            //    btnAddItems.Visible = true;
            //    btnDelItems.Visible = false;
            //}
            //else
            //{
            //    btnAddItems.Visible = false;
            //    btnDelItems.Visible = false;
            //}
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMachineMng_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            setFormValue();
            initCheckItem();
            bool bEnable = string.IsNullOrEmpty(sysCode);
            // btnAddItems.Enabled = bEnable; 
            //txtGNName.Enabled = bEnable;

            btnDelItems.Enabled = !bEnable;
            btnEdit.Enabled = !bEnable;
          
            txtSysCode.Enabled = bEnable;
            label9.Visible = bEnable;
            txtSysCode.Visible = bEnable;

            if (!linkStdCode.Equals(""))
            {
                readCheckItemCode();
            }
        }

        /// <summary>
        /// 绑定检测项目列表到DataGrid
        /// </summary>
        private void readCheckItemCode()
        {
            initDtbl.Clear();
            DataRow dr = null;
            string[,] strResult = null;
            if (txtProtocol.Text.Equals("RS232DY3000DY"))
            {
                strResult = StringUtil.GetDY3000DYAry(linkStdCode);
                for (int i = 0; i <= strResult.GetLength(0) - 1; i++)
                {
                    dr = initDtbl.NewRow();
                    dr["ShortName"] = strResult[i, 0];

                    if (strResult[i, 1].Equals("-1"))
                    {
                        dr["ItemName"] = "尚未对应";
                        dr["ItemCode"] = string.Empty;
                    }
                    else
                    {
                        dr["ItemName"] = clsCheckItemOpr.GetNameFromCode(strResult[i, 1]);
                        dr["ItemCode"] = strResult[i, 1];
                    }
                    dr["CheckMethod"] = strResult[i, 2];
                    dr["Unit"] = strResult[i, 3];
                    initDtbl.Rows.Add(dr);
                }
            }
            else
            {
                strResult = StringUtil.GetAry(linkStdCode);
                for (int i = 0; i <= strResult.GetLength(0) - 1; i++)
                {
                    dr = initDtbl.NewRow();
                    dr["ShortName"] = strResult[i, 0];

                    if (strResult[i, 1].Equals("-1"))
                    {
                        dr["ItemName"] = "尚未对应";
                        dr["ItemCode"] = string.Empty;
                    }
                    else
                    {
                        dr["ItemName"] = clsCheckItemOpr.GetNameFromCode(strResult[i, 1]);
                        dr["ItemCode"] = strResult[i, 1];
                    }
                    dr["CheckMethod"] = string.Empty;
                    dr["Unit"] = string.Empty;
                    initDtbl.Rows.Add(dr);
                }
            }
 
            dgItemList.DataSource = initDtbl;

            dgItemList.Columns["ItemName"].Width = 150;
            dgItemList.Columns["ShortName"].Width = 110;

            dgItemList.Columns["ShortName"].HeaderText = "仪器检测项目";
            dgItemList.Columns["ItemName"].HeaderText = "对应检测项目";


            dgItemList.Columns["ItemCode"].Visible = false;
            dgItemList.Columns["CheckMethod"].Visible = false;
            dgItemList.Columns["Unit"].Visible = false;

            bindItemFormGridRow();
        }

        private void bindItemFormGridRow()
        {
            if (dgItemList != null)
            {
                int row = dgItemList.CurrentRow.Index;
                if (row < 0)
                {
                    return;
                }
                int index = row ;
                txtGNName.Text = dgItemList.CurrentRow.Cells["ShortName"].Value.ToString();
                if (!dgItemList.CurrentRow.Cells["ItemCode"].Value.ToString().Equals(string.Empty))
                {
                    cmbCheckItem.SelectedValue = dgItemList.CurrentRow.Cells["ItemCode"].Value.ToString();
                    cmbCheckItem.Text = dgItemList.CurrentRow.Cells["ItemName"].Value.ToString();
                }
                else
                {
                    cmbCheckItem.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// 增加检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItems_Click(object sender, System.EventArgs e)
        {
            string strGNName = txtGNName.Text.Trim();
            if (string.IsNullOrEmpty(strGNName))
            {
                MessageBox.Show(this, "所要增加的检测项目名称不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool blnIsExist = false;
            string tmp = string.Empty;
            object obj=null;
            //检查项目是否有重复
            if (dgItemList != null)
            {
                int iLen = dgItemList.Rows.Count ;
                for (int i = 0; i < iLen; i++)
                {
                    obj = dgItemList.Rows[i].Cells["ShortName"].Value;
                    //tmp = dgItemList.Rows[i].Cells["ShortName"].Value.ToString();
                    if (obj != null)
                    {
                        tmp = obj.ToString();
                    }
                    if (tmp.Equals(strGNName))
                    {
                        blnIsExist = true;
                        break;
                    }
                }
            }
            if (!blnIsExist)
            {
                int len = strGNName.Length;
                string strEditNew = string.Empty;
                string strTemp = string.Empty;
                string strZY = @".$^{[(|)*+?\";
                for (int i = 0; i < len; i++)
                {
                    strTemp = strGNName.Substring(i, 1);
                    if (strZY.IndexOf(strTemp, 0) > 0)
                    {
                        strGNName = strGNName + @"\" + strTemp;
                    }
                    //else
                    //{
                    //    strGNName = strGNName + strTemp;
                    //}
                    //if (strZY.IndexOf(strTemp, 0) > 0)
                    //{
                    //    strGNName += @"\" + strTemp;
                    //}
                    //else
                    //{
                    //    strGNName += strTemp;
                    //}
                }
                if (cmbCheckItem.SelectedValue != null)
                {
                    strEditNew = "{" + strGNName + ":" + cmbCheckItem.SelectedValue.ToString() + "}";
                }
                else
                {
                    strEditNew = "{" + strGNName + ":-1}";
                }
                linkStdCode += strEditNew;
                readCheckItemCode();
            }
            else
            {
                MessageBox.Show(this, "所要增加的检测项目名称已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //btnAddItems.Text = "增加检测功能";
            //txtGNName.Enabled = false;
            //btnDelItems.Enabled = true;
            //btnEdit.Enabled = true;
            //}
            //else if (btnAddItems.Text.Equals("增加检测功能"))
            //{
            //    btnAddItems.Text = "确定";
            //    txtGNName.Enabled = true;
            //    txtGNName.Text = "";
            //    btnDelItems.Enabled = false;
            //    btnEdit.Enabled = false;
            //}
            //else if (btnAddItems.Text.Equals("读取检测功能"))
            //{
            //    if (c1FlexGrid1.Rows.Count >= 2)
            //    {
            //        DialogResult dr = MessageBox.Show(this, "已读取过检测项目，再次读取将覆盖原有检测项目及相关配置，真要再次读取吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (dr == DialogResult.No) return;
            //    }
            //    Cursor = Cursors.WaitCursor;
            //    if (!IsDY3000DYOpen)
            //    {
            //        ShareOption.ComPort = "COM" + Convert.ToString(cmbCom.SelectedIndex + 1);
            //        curDY3000DY = new clsDY3000DY();
            //        curDY3000DY.Open();
            //        IsDY3000DYOpen = true;
            //    }
            //    if (!curDY3000DY.Online)
            //    {
            //        MessageBox.Show(this, "无法与仪器正常通讯，请检查！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        Cursor = Cursors.Default;
            //        return;
            //    }
            //    btnAddItems.Enabled = false;
            //    btnEdit.Enabled = false;
            //    curDY3000DY.ReadItem();
            //}
        }

        /// <summary>
        /// 修改检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            int row = dgItemList.CurrentRow.Index; //c1FlexGrid1.RowSel;
            if (row <0)
            {
                return;
            }
            string strEditNew = string.Empty;
            int len = dgItemList.CurrentRow.Cells[0].Value.ToString().Length;
            string strGNName = string.Empty;
            string strTemp = string.Empty;
            string strZY = @".$^{[(|)*+?\";
            for (int i = 0; i < len; i++)
            {
                strTemp = dgItemList.CurrentRow.Cells[0].Value.ToString().Substring(i, 1);
                if (strZY.IndexOf(strTemp, 0) > 0)
                {
                    strGNName += @"\" + strTemp;
                }
                else
                {
                    strGNName += strTemp;
                }
            }
            if (txtProtocol.Text.Equals("RS232DY3000DY"))
            {
                if (cmbCheckItem.SelectedValue != null)
                {
                    strEditNew = "{" + dgItemList.CurrentRow.Cells[0].Value.ToString() + ":" + cmbCheckItem.SelectedValue.ToString() + ":" + dgItemList.CurrentRow.Cells[3].Value.ToString() + ":" + dgItemList.CurrentRow.Cells[4].Value.ToString() + "}";
                }
                else
                {
                    strEditNew = "{" + dgItemList.CurrentRow.Cells[0].Value.ToString() + ":-1" + ":" + dgItemList.CurrentRow.Cells[3].Value.ToString() + ":" + dgItemList.CurrentRow.Cells[4].Value.ToString() + "}";
                }
                string pattern = @"({" + strGNName + @":[\S\t]*?:[\S\t]*?:[\S\t]*?})";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                linkStdCode = r.Replace(linkStdCode, strEditNew);
            }
            else
            {
                if (cmbCheckItem.SelectedValue != null)
                {
                    strEditNew = "{" + dgItemList.CurrentRow.Cells[0].Value.ToString() + ":" + cmbCheckItem.SelectedValue.ToString() + "}";
                }
                else
                {
                    strEditNew = "{" + dgItemList.CurrentRow.Cells[0].Value.ToString() + ":-1}";
                }
                string pattern = @"({" + strGNName + @":[\S\t]*?})";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                linkStdCode = r.Replace(linkStdCode, strEditNew);
            }
            readCheckItemCode();
            //IsRead = true;
            //c1FlexGrid1.DataSource = initDtbl;
            //IsRead = false;
            //c1FlexGrid1.RowSel = row;
        }
        /// <summary>
        /// 删除检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelItems_Click(object sender, EventArgs e)
        {
            int row = dgItemList.CurrentRow.Index;// c1FlexGrid1.RowSel;
            if (row <= 0)
            {
                return;
            }
            DialogResult drt = MessageBox.Show(this, "是否要删除名为：" + dgItemList.CurrentRow.Cells[0].Value.ToString() + "的检测项目？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drt == DialogResult.Yes)
            {
                string pattern = @"({" + dgItemList.CurrentRow.Cells[0].Value.ToString() + @":[\S\t]*?})";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                linkStdCode = r.Replace(linkStdCode, "");
                readCheckItemCode();
            }
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string err = string.Empty;
            string tmpCode = txtSysCode.Text.Trim();
            bool isAdd = string.IsNullOrEmpty(sysCode);
            if (string.IsNullOrEmpty(tmpCode))
            {
                MessageBox.Show("系统编码不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSysCode.Focus();
                return;
            }
            if (isAdd)
            {
                if (machBll.IsExists(tmpCode))
                {
                    MessageBox.Show("系统编码已经存在请输另一个编码", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSysCode.Focus();
                    return;
                }
            }

            if (chkIsDef.Checked)
            {
                int i = clsMachineOpr.GetRecCount(out err);
                if (i >= 1)
                {
                    DialogResult drl = MessageBox.Show(this, "存在别的仪器为默认仪器!而您又设置新的默认仪器，系统将自动将以前默认仪器更改为非默认的", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (drl == DialogResult.Yes)
                    {
                        clsMachineOpr.UpdateIsSupport(out err);
                    }
                }
            }
            try
            {
                clsMachine model = new clsMachine();
                model.MachineModel = txtModel.Text;
                model.MachineName = txtName.Text;
                model.Company = txtCompany.Text;
                model.Protocol = txtProtocol.Text;
                model.SysCode = tmpCode;
                model.LinkComNo = cmbCom.SelectedIndex + 1;
                model.IsSupport = chkIsDef.Checked;
                model.TestValue = Convert.ToSingle(txtTestValue.Text.Trim());
                model.OrderId = Convert.ToInt32(txtOrderId.Text.Trim());

                model.TestSign = cmbSign.Text.Trim();
                model.LinkStdCode = linkStdCode;
                model.IsShow = chbShow.Checked;

                machBll.InsertOrUpdate(model, isAdd, out err);

                if (!isAdd)//是否更新的情况
                {
                    clsSysOptOpr bll = new clsSysOptOpr();
                    string temp = string.Empty;
                    if (sysCode.Equals("009"))//DY3000
                    {
                        if (model.Protocol.IndexOf("DY3000DY") >= 0)//新版3000
                        {
                            temp = "DY3000DY";
                        }
                        else
                        {
                            temp = "DY3000";
                        }

                        bll.UpdateCommand(string.Format("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='040101'", temp));
                    }
                    else if (sysCode.Equals("010"))//DY5000
                    {
                        if (model.Protocol.IndexOf("DY5000LD") >= 0)//新版3000
                        {
                            temp = "DY5000LD";
                        }
                        else
                        {
                            temp = "DY5000";
                        }

                        bll.UpdateCommand(string.Format("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='040102'", temp));
                    }
                }

                MessageBox.Show("操作成功！");
            }
            catch //(Exception ex)
            {
                MessageBox.Show(err);
            }
            finally
            {
                winClose();
            }
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.InfoAdd, "001");
            }
            if (initDtbl != null)
            {
                initDtbl.Dispose();
                initDtbl = null;
            }
            Dispose();
        }

        private void c1FlexGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bindItemFormGridRow();
        }
    }
}
