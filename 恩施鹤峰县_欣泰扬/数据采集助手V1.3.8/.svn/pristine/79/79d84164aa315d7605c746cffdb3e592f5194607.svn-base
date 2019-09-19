using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationModel.UpData;
using WorkstationDAL.UpLoadData;
using WorkstationModel.Model;
using WorkstationModel.function;

namespace WorkstationUI.function
{
    public partial class ucAddSample : UserControl
    {
        private bool m_IsCreatedDataTable = false;
        private DataTable DataDisTable = null;
        private clsSetSqlData csql = new clsSetSqlData();
        private DataTable dt = null;
        private StringBuilder sb = new StringBuilder();
        private string err = "";
        /// <summary>
        /// 下载的检测样品标准集合
        /// </summary>
        private List<ItemAndStandard> downLoadItemAndStandardList = null;
        /// <summary>
        /// 下载的检测样品集合
        /// </summary>
        private List<Simple> downLoadSimpleList = null;
        private clsdiary dy = new clsdiary();

        public ucAddSample()
        {
            InitializeComponent();
        }

        private void ucAddSample_Load(object sender, EventArgs e)
        {
            iniTable();
            try
            {
                string err = string.Empty;
                sb.Length = 0;
                if (Global.Platform == "DYBus")
                {
                    dt = csql.GetDownChkItem("", "", out err);
                    btnDownItemStand.Visible = true;
                    btnSampleDown.Visible = true;
                    btnDownItemStand.Location = new Point(473,46);
                    btnSampleDown.Location = new Point(637,46);
                }
                else if (Global.Platform == "DYKJFW")
                {
                    btnSample.Location = new Point(473, 46);
                    btnSample.Visible = true;
                    btnStandard.Location = new Point(673, 46);
                    btnStandard.Visible = true;
                    BtnDownItem.Location = new Point(573, 46);
                    BtnDownItem.Visible = true;
                    dt = csql.GetSamplestd("", "", 1, out err);
                    //dt = csql.Getfoodtype("", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;//DataDisTable;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "单位";
                        CheckDatas.Columns[5].HeaderCell.Value = "更新时间";
                        //for (int j = 0; j < CheckDatas.RowCount; j++)
                        //{
                        CheckDatas.Columns[0].Width = 200;
                        CheckDatas.Columns[1].Width = 200;
                        CheckDatas.Columns[2].Width = 200;
                        //}
                    }
                }
                else
                {
                    dt = csql.GetChkItem("", "", out err);
                    btnAdd.Visible = true;
                    btnRepair.Visible = true;
                    BtnClear.Visible = true;
                    btnrefresh.Visible = true;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;//DataDisTable;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[5].HeaderCell.Value = "单位";
                        //for (int j = 0; j < CheckDatas.RowCount; j++)
                        //{
                        CheckDatas.Columns[0].Width = 200;
                        CheckDatas.Columns[1].Width = 200;
                        CheckDatas.Columns[2].Width = 200;
                        //}
                    }
                }

                
                dy.savediary(DateTime.Now.ToString(), "进入样品信息成功", "成功" );
            }
            catch(Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入样品信息错误"+ex.Message , "错误");
                MessageBox.Show(ex.Message, "进入样品信息");
            }
        }
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataDisTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "判定符号";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataDisTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测时间";
                //DataTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
            }
        }
        private void addtable(string SamName, string ItemName, string testbase, string Svalue, string symbol, string iunit)
        {
            DataRow dr;
            dr = DataDisTable.NewRow();
            dr["样品名称"] = SamName;
            dr["检测项目"] = ItemName;
            dr["检测依据"] = testbase;
            dr["标准值"] = Svalue;
            dr["判定符号"] = symbol;
            dr["单位"] = iunit;

            DataDisTable.Rows.Add(dr);
        }
        /// <summary>
        /// 新增标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddSample fas = new frmAddSample();
            fas.SaveRepair = "新增";
            DialogResult dr = fas.ShowDialog();
            if(dr==DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    string err = string.Empty;
                    dt = csql.GetDownChkItem("", "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataDisTable.Clear();
                            //for (int i = 0; i < dt.Rows.Count; i++)
                            //{
                            //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                            //}
                            CheckDatas.DataSource = dt;//DataDisTable;
                            CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                            CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                            CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                            CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                            CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                            CheckDatas.Columns[5].HeaderCell.Value = "单位";
                        }
                    }
                }
                catch (Exception ex)
                {
                    dy.savediary(DateTime.Now.ToString(), "进入样品信息查询错误：" + ex.Message, "错误");
                    MessageBox.Show(ex.Message, "样品信息查询");
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要修改的记录","提示");
                return;
            }
            try
            {
                string err = string.Empty;
                frmAddSample fas = new frmAddSample();
                string sql = "";
                if (Global.Platform == "DYBus")
                {
                    sql = "sampleName='" + CheckDatas.SelectedRows[0].Cells["样品名称"].Value.ToString() + "' and itemName='" + CheckDatas.SelectedRows[0].Cells["检测项目"].Value.ToString() + "'" +
                                " and standardName='" + CheckDatas.SelectedRows[0].Cells["检测依据"].Value.ToString() + "'";
                }
                else
                {
                    sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells["FtypeNmae"].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells["Name"].Value.ToString() + "'" +
                       " and ItemDes='" + CheckDatas.SelectedRows[0].Cells["ItemDes"].Value.ToString() + "'";
                }

                DataTable dt = csql.GetDownItemID(sql, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    fas.id = dt.Rows[0]["idx"].ToString();
                }

                string[,] a = new string[CheckDatas.SelectedRows.Count , 6];
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    a[i, 0] = CheckDatas.SelectedRows[i].Cells["FtypeNmae"].Value.ToString();
                    a[i, 1] = CheckDatas.SelectedRows[i].Cells["Name"].Value.ToString();
                    a[i, 2] = CheckDatas.SelectedRows[i].Cells["ItemDes"].Value.ToString();
                    a[i, 3] = CheckDatas.SelectedRows[i].Cells["StandardValue"].Value.ToString();
                    a[i, 4] = CheckDatas.SelectedRows[i].Cells["Demarcate"].Value.ToString();
                    a[i, 5] = CheckDatas.SelectedRows[i].Cells["Unit"].Value.ToString();
                }
                Global.repairSample = a;
                fas.SaveRepair = "修改";
                DialogResult dr = fas.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();

                    dt = csql.GetDownChkItem("", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;//DataDisTable;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[5].HeaderCell.Value = "单位";
                        CheckDatas.Columns[0].Width = 200;
                        CheckDatas.Columns[1].Width = 200;
                        CheckDatas.Columns[2].Width = 200;  
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品信息修改错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品信息修改");
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dt.Clear();
            CheckDatas.DataSource = null;
            try
            {
               
                sb.Clear();
                string err = string.Empty;
                if (!txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    if (Global.Platform == "DYBus")
                    {
                        sb.Append("sampleName like '%");
                        sb.Append(txtSample.Text);
                        sb.Append("%' and ");
                        sb.Append("itemName like '%");
                        sb.Append(txtItem.Text);
                        sb.Append("%'");
                    }
                    else
                    {

                        sb.AppendFormat("FtypeNmae like '%{0}%'", txtSample.Text.Trim());
                        sb.AppendFormat(" and Name like '%{0}%'", txtItem.Text.Trim());
                       
                    }
                    
                }
                else if (!txtSample.Text.Equals("") && txtItem.Text.Equals(""))
                {
                    if (Global.Platform == "DYBus")
                    {
                        sb.AppendFormat("sampleName like '%{0}%'", txtSample.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat("FtypeNmae like '%{0}%'", txtSample.Text.Trim());
                    }
                    //sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    //sb.Append("Name='");
                    //sb.Append(txtItem.Text);
                    //sb.Append("%'");
                }
                else if (txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    //sb.Append("FtypeNmae='");
                    //sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    sb.AppendFormat(" Name like '%{0}%'", txtItem.Text.Trim());
                    //sb.Append("itemName like '%");
                    //sb.Append(txtItem.Text);
                    //sb.Append("%'");
                }
                if (Global.Platform == "DYBus")
                {
                    dt = csql.GetDownChkItem(sb.ToString(), "", out err);
                }
                else
                {
                    dt = csql.GetChkItem(sb.ToString(), "", out err);
                }
                
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataDisTable.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                    //}
                    CheckDatas.DataSource = dt;
                    CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                    CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                    CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                    CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                    CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                    CheckDatas.Columns[5].HeaderCell.Value = "单位";
                    CheckDatas.Columns[0].Width = 200;
                    CheckDatas.Columns[1].Width = 200;
                    CheckDatas.Columns[2].Width = 200;
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品信息查询错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品信息查询");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("是否删除选中记录","提示",MessageBoxButtons.YesNo ,MessageBoxIcon.Information );
            if (dr == DialogResult.No)
            {
                return;
            }
            try
            {
                string err = string.Empty;
                int iok = 0;
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {  
                    StringBuilder sb = new StringBuilder();
                    if (Global.Platform == "DYBus")
                    {
                        sb.Append("ChkItemStandard where sampleName='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
                        sb.Append("' and itemName='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
                        sb.Append("' and standardName='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
                        sb.Append("' and standardValue='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[3].Value.ToString());
                        sb.Append("'");
                    }
                    else
                    {
                        sb.Append("tStandSample where FtypeNmae='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
                        sb.Append("' and Name='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
                        sb.Append("' and ItemDes='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
                        sb.Append("' and StandardValue='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells[3].Value.ToString());
                        sb.Append("'");
                    }


                    iok = csql.Delete(sb.ToString(), out err);
                }                
                MessageBox.Show("数据删除成功");
                btnrefresh_Click(null ,null);
              
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品删除错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品删除");
            }
            
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string err = string.Empty;
                dt = csql.GetDownChkItem("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[5].HeaderCell.Value = "单位";
                        CheckDatas.Columns[0].Width = 200;
                        CheckDatas.Columns[1].Width = 200;
                        CheckDatas.Columns[2].Width = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品刷新错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品刷新");
            }
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string err = string.Empty;
                frmAddSample fas = new frmAddSample();
                //string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'"+
                //    " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
                string sql = "sampleName='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and itemName='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
                    " and standardName='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
                DataTable dt = csql.GetDownItemID(sql, "", out err);
                if (dt != null && dt.Rows.Count>0)
                {
                    
                    fas.id = dt.Rows[0][0].ToString();
                    
                }

                string[,] a = new string[CheckDatas.SelectedRows.Count, 6];
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    a[i, 0] = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();
                    a[i, 1] = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();
                    a[i, 2] = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                    a[i, 3] = CheckDatas.SelectedRows[i].Cells[3].Value.ToString();
                    a[i, 4] = CheckDatas.SelectedRows[i].Cells[4].Value.ToString();
                    a[i, 5] = CheckDatas.SelectedRows[i].Cells[5].Value.ToString();
                }
                Global.repairSample = a;
                fas.SaveRepair = "修改";
                DialogResult dr = fas.ShowDialog();
                if (dr == DialogResult.OK)
                {
               
                    StringBuilder sb = new StringBuilder();
                    dt = csql.GetDownChkItem("", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                            
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[5].HeaderCell.Value = "单位";
                            
                    }
                }
             }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品修改错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品修改");
            }
        }
        /// <summary>
        /// 检测项目标准下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownItemStand_Click(object sender, EventArgs e)
        {
            btnDownItemStand.Enabled = false;
            if (Global.ServerAdd.Length == 0)
            {
                MessageBox.Show("服务器地址不能为空", "提示");
                btnDownItemStand.Enabled = true;
                return;
            }
            if (Global.ServerName.Length == 0)
            {
                MessageBox.Show("用户名不能为空", "提示");
                btnDownItemStand.Enabled = true;
                return;
            }
            if (Global.ServerPassword.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示");
                btnDownItemStand.Enabled = true;
                return;
            }
            string err = "";
            try
            {
                string rtndata = InterfaceHelper.DownloadBasicData(Global.DownLoadItemAndStandard, out err);
                ResultMsg jsonResult = GetData(rtndata);
                if (jsonResult == null && jsonResult.result != null)
                {
                    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDownItemStand.Enabled = true;
                    return;
                }
                ItemAndStandard itemAndStandard = null;
                downLoadItemAndStandardList = new List<ItemAndStandard>();
                downLoadItemAndStandardList = JsonHelper.JsonToEntity<List<ItemAndStandard>>(jsonResult.result.ToString());//(List<ItemAndStandard>)JsonConvert.DeserializeObject(jsonResult.result.ToString(), typeof(List<ItemAndStandard>));
                if (downLoadItemAndStandardList == null || downLoadItemAndStandardList.Count == 0)
                {
                    MessageBox.Show( "暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDownItemStand.Enabled = true;
                    return;
                }
                //删除原来表的数据再插入
                csql.DeleteItemStandard("", "", out err);

                for (int i = 0; i < downLoadItemAndStandardList.Count; i++)
                {                 
                    itemAndStandard = downLoadItemAndStandardList[i];
                    csql.InItemStandard(itemAndStandard ,out err );
                }
                MessageBox.Show("检测项目标准下载成功，共下载"+downLoadItemAndStandardList.Count+"条记录","提示");
            }
            catch (Exception ex)
            {
                btnDownItemStand.Enabled = true;
                MessageBox.Show(ex.Message ,"Error");
            }

            try
            {
                StringBuilder sb = new StringBuilder();
              

                dt = csql.GetDownChkItem("", "", out err);

                //dt=  csql.GetChkItem("", "", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataDisTable.Clear();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                        //}
                        CheckDatas.DataSource = dt;//DataDisTable;
                        CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
                        CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
                        CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
                        CheckDatas.Columns[3].HeaderCell.Value = "标准值";
                        CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
                        CheckDatas.Columns[5].HeaderCell.Value = "单位";
                        for (int j = 0; j < CheckDatas.RowCount; j++)
                        {
                            CheckDatas.Columns[0].Width = 200;
                            CheckDatas.Columns[1].Width = 200;
                            CheckDatas.Columns[2].Width = 200;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnDownItemStand.Enabled = true;
                MessageBox.Show(ex.Message, "Error");
            }
            btnDownItemStand.Enabled = true;
        }
        /// <summary>
        /// 根据json获取数据，没有则返回null
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private ResultMsg GetData(string json)
        {
            if (json.Length == 0)
            {
                MessageBox.Show( "暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }

            return clsDownExamedUnit.GetJsonResult(json); ;
        }
        /// <summary>
        /// 下载检测样品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSampleDown_Click(object sender, EventArgs e)
        {
            btnSampleDown.Enabled = false;
            if (Global.ServerAdd.Length == 0)
            {
                MessageBox.Show("服务器地址不能为空", "提示");
                return;
            }
            if (Global.ServerName.Length == 0)
            {
                MessageBox.Show("用户名不能为空", "提示");
                return;
            }
            if (Global.ServerPassword.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示");
                return;
            }
            string err = "";
            try
            {
                string sample= InterfaceHelper.DownloadBasicData(Global.DownLoadSample, out err);
                ResultMsg jsonResult = GetData(sample);
                if (jsonResult == null && jsonResult.result != null)
                {
                    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                downLoadSimpleList = new List<Simple>();
                downLoadSimpleList = JsonHelper.JsonToEntity<List<Simple>>(jsonResult.result.ToString());
                if (downLoadSimpleList == null || downLoadSimpleList.Count == 0)
                {
                    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //删除原有的记录
                 csql.DeleteSample("","",out err);
                 Simple simple = null;
                 
                 for (int i = 0; i < downLoadSimpleList.Count; i++)
                 {
                     simple = downLoadSimpleList[i];
                     csql.InSample(simple ,out err );

                 }
                 MessageBox.Show("检测样品下载成功","提示");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"Error");
            }
            btnSampleDown.Enabled = true;
        }

        private void BtnDownItem_Click(object sender, EventArgs e)
        {
            BtnDownItem.Enabled = false;
            string lasttime = "";
            string err = "";
            dt = csql.GetRequestTime("RequestName='CheckItem'", "", out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                //lasttime = dt.Rows[0]["UpdateTime"].ToString();
                csql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='CheckItem'", "", 1, out err);
            }
            else
            {
                csql.InsertResquestTime("'CheckItem','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
            }

            string url2 = Global.ServerAdd ;

            string ItemAddr = QuickInspectServing.GetServiceURL(url2, 5);//地址
            sb.Length = 0;
            sb.Append(ItemAddr);
            sb.AppendFormat("?userToken={0}", Global.Token);
            sb.AppendFormat("&type={0}", "item");
            sb.AppendFormat("&serialName={0}", "");
            sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
            //FileUtils.KLog(sb.ToString(), "发送", 7);
            string itemlist = QuickInspectServing.HttpsPost(sb.ToString());

            //RE.KLog(itemlist, "接收", 7);
          
                ResultData Jitem = JsonHelper.JsonToEntity<ResultData>(itemlist);
                detectitem obj = JsonHelper.JsonToEntity<detectitem>(Jitem.obj.ToString());
                if (obj.detectItem.Count > 0)
                {
                    int Gitem = 0;
                    CheckItem CI = new CheckItem();
                    for (int i = 0; i < obj.detectItem.Count; i++)
                    {
                        int rt = 0;
                        sb.Length =0;
                        sb.AppendFormat("cid='{0}' ", obj.detectItem[i].id);

                        dt = csql.GetDetectItem(sb.ToString(), "", out err);

                        CI = obj.detectItem[i];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            rt = csql.UpdateDetectItem(CI);
                            if (rt == 1)
                            {
                                Gitem =Gitem + 1;
                            }
                        }
                        else
                        {
                            rt = csql.InsertDetectItem(CI);
                            if (rt == 1)
                            {
                                Gitem =Gitem + 1;
                            }
                        }
                    }
                }
            
            MessageBox.Show("检测项目下载成功！");
            BtnDownItem.Enabled = true ;
        }

        private void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            
        }
        /// <summary>
        /// 快检服务样品下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSample_Click(object sender, EventArgs e)
        {
            string  lasttime = "";
            int count = 0;
            string saddr = QuickInspectServing.GetServiceURL(Global.ServerAdd, 8);//地址
            dt = csql.GetRequestTime("RequestName='foodTypes'", "", out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                lasttime = dt.Rows[0]["UpdateTime"].ToString();
                csql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='foodTypes'", "", 1, out err);
            }
            else
            {
                csql.InsertResquestTime("'foodTypes','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
            }
            sb.Length = 0;
            sb.Append(saddr);
            sb.AppendFormat("?userToken={0}", Global.Token);
            sb.AppendFormat("&type={0}", "food");
            sb.AppendFormat("&serialNumber={0}", Global.MachineSerialCode);
            sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);

            FilesRW.KLog(sb.ToString(), "发送", 8);

            string stype = QuickInspectServing.HttpsPost(sb.ToString());
            FilesRW.KLog(stype, "接收", 8);
            if (stype.Contains("obj") && stype.Contains("success"))
            {
                ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
               
                foodtype ft = new foodtype();
                for (int i = 0; i < obj.food.Count; i++)
                {
                    int rt = 0;
                    sb.Length = 0;
                    sb.AppendFormat("fid='{0}' ", obj.food[i].id);
                   
                    dt = csql.Getfoodtype(sb.ToString(), "", out err);
                    ft = obj.food[i];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rt = csql.Updatefoodtype(ft);
                        if (rt == 1)
                        {
                            count = count + 1;
                        }
                    }
                    else
                    {
                        rt = csql.Insertfoodtype(ft);
                        if (rt == 1)
                        {
                            count = count + 1;
                        }
                    }
                }
                
            }
            MessageBox.Show("共成功下载"+count+"条数据");
        }
        /// <summary>
        /// 快检服务判定标准下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStandard_Click(object sender, EventArgs e)
        {
            string SD = QuickInspectServing.GetServiceURL(Global.ServerAdd, 8);//地址 msg.str1 + "iSampling/updateStatus.do";
            int couunt = 0;
            string lasttime = "";
            dt = csql.GetRequestTime("RequestName='SampleItemstd'", "", out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                //lasttime = dt.Rows[0]["UpdateTime"].ToString();//获取上一次获取时间
                csql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='SampleItemstd'", "", 3, out err);
            }
            else
            {
                csql.InsertResquestTime("'SampleItemstd','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 3, out err);
            }
            sb.Length = 0;
            sb.Append(SD);
            sb.AppendFormat("?userToken={0}", Global.Token);
            sb.AppendFormat("&type={0}", "foodItem");
            sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
            sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
            sb.AppendFormat("&pageNumber={0}", "");

            FilesRW.KLog(sb.ToString(), "发送", 9);
            string std = QuickInspectServing.HttpsPost(sb.ToString());
            FilesRW.KLog(std, "接收", 9);

            ResultData sslist = JsonHelper.JsonToEntity<ResultData>(std);
            if (sslist.msg == "操作成功" && sslist.success == true)
            {

                fooditemlist flist = JsonHelper.JsonToEntity<fooditemlist>(sslist.obj.ToString());
                if (flist.foodItem.Count > 0)
                {
                    fooditem fi = new fooditem();
                    for (int i = 0; i < flist.foodItem.Count; i++)
                    {
                        int w = 0;
                        sb.Length = 0;
                        sb.AppendFormat("sid='{0}' ", flist.foodItem[i].id);
                      
                        dt = csql.GetSampleStand(sb.ToString(), "", out err);

                        fi = flist.foodItem[i];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            w = csql.UpdateSampleStandard(fi);
                            if (w == 1)
                            {
                                couunt =couunt +1;
                            }
                        }
                        else
                        {
                            w = csql.InsertSampleStandard(fi);
                            if (w == 1)
                            {
                                couunt = couunt + 1;
                            }
                        }

                    }
                }
            }
            MessageBox.Show("共成功下载 "+couunt+" 条数据！");
        }
 
    }
}
