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

namespace WorkstationUI.function
{
    public partial class ucAddSample : UserControl
    {
        private bool m_IsCreatedDataTable = false;
        private DataTable DataDisTable = null;
        private clsSetSqlData csql = new clsSetSqlData();
        private DataTable dt = null;
        private com.szscgl.ncp.sDataInfrace webserver = new com.szscgl.ncp.sDataInfrace();
        /// <summary>
        /// 下载的检测样品标准集合
        /// </summary>
        private List<ItemAndStandard> downLoadItemAndStandardList = null;
        /// <summary>
        /// 下载的检测样品集合
        /// </summary>
        private List<Simple> downLoadSimpleList = null;
        private clsdiary dy = new clsdiary();
        private StringBuilder sb = new StringBuilder();

        public ucAddSample()
        {
            InitializeComponent();
        }

        private void ucAddSample_Load(object sender, EventArgs e)
        {
            //iniTable();
            KStable();
            try
            {
                KSsearchsample("");
               
                //dt = csql.GetDownChkItem("", "", out err);

                //dt = csql.GetChkItem("", "", out err);

              
                dy.savediary(DateTime.Now.ToString(), "进入样品信息成功", "成功" );
            }
            catch(Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入样品信息错误"+ex.Message , "错误");
                MessageBox.Show(ex.Message, "进入样品信息");
            }
        }
        private void KStable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataDisTable = new DataTable("sample");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "唯一标识";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "大类品种编码";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "大类品种名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "小类品种编码";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "小类品种名称";
                DataDisTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "更新时间";
                DataDisTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
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
                    DataTable dt = csql.GetDownChkItem("", "", out err);
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

                //string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
                //   " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
                string sql = "sampleName='" + CheckDatas.SelectedRows[0].Cells["样品名称"].Value.ToString() + "' and itemName='" + CheckDatas.SelectedRows[0].Cells["检测项目"].Value.ToString() + "'" +
                   " and standardName='" + CheckDatas.SelectedRows[0].Cells["检测依据"].Value.ToString() + "'";

                DataTable dt = csql.GetDownItemID(sql, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    fas.id = dt.Rows[0][0].ToString();
                }

                string[,] a = new string[CheckDatas.SelectedRows.Count , 6];
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    a[i, 0] = CheckDatas.SelectedRows[i].Cells["样品名称"].Value.ToString();
                    a[i, 1] = CheckDatas.SelectedRows[i].Cells["检测项目"].Value.ToString();
                    a[i, 2] = CheckDatas.SelectedRows[i].Cells["检测依据"].Value.ToString();
                    a[i, 3] = CheckDatas.SelectedRows[i].Cells["标准值"].Value.ToString();
                    a[i, 4] = CheckDatas.SelectedRows[i].Cells["判定符号"].Value.ToString();
                    a[i, 5] = CheckDatas.SelectedRows[i].Cells["单位"].Value.ToString();
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
        private void KSsearchsample(string where )
        {
            string err = string.Empty;
            sb.Length = 0;
            try
            {
                dt = csql.GetKSsample(where, "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataDisTable.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                    //}
                    CheckDatas.DataSource = dt;//DataDisTable;
                    CheckDatas.Columns["sId"].HeaderCell.Value = "唯一标识";
                    CheckDatas.Columns["ItemCode"].HeaderCell.Value = "大类品种编码";
                    CheckDatas.Columns["ItemName"].HeaderCell.Value = "大类品种名称";
                    CheckDatas.Columns["SubItemCode"].HeaderCell.Value = "小类品种编码";
                    CheckDatas.Columns["SubItemName"].HeaderCell.Value = "小类品种名称";
                    CheckDatas.Columns["UpdateDate"].HeaderCell.Value = "更新时间";
                    //for (int j = 0; j < CheckDatas.RowCount; j++)
                    //{
                    CheckDatas.Columns[2].Width = 150;
                    CheckDatas.Columns[3].Width = 150;
                    CheckDatas.Columns[4].Width = 150;
                    CheckDatas.Columns[5].Width = 150;
                    //}
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                sb.Length = 0;
                if (txtSample.Text.Trim().Length >0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }
                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if(sb.ToString ().Length >0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat("ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }
               
                KSsearchsample(sb.ToString());
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
                    sb.Append("ChkItemStandard where sampleName='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
                    sb.Append("' and itemName='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
                    sb.Append("' and standardName='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
                    sb.Append("' and standardValue='");
                    sb.Append(CheckDatas.SelectedRows[i].Cells[3].Value.ToString());
                    sb.Append("'");
                    //sb.Append("tStandSample where FtypeNmae='");
                    //sb.Append(CheckDatas.SelectedRows[i].Cells[0].Value.ToString());
                    //sb.Append("' and Name='");
                    //sb.Append(CheckDatas.SelectedRows[i].Cells[1].Value.ToString());
                    //sb.Append("' and ItemDes='");
                    //sb.Append(CheckDatas.SelectedRows[i].Cells[2].Value.ToString());
                    //sb.Append("' and StandardValue='");
                    //sb.Append(CheckDatas.SelectedRows[i].Cells[3].Value.ToString());
                    //sb.Append("'");

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
            int icount = 0;
            try
            {
                KunShanEntity.CheckItemRequest.webService checkItem = new KunShanEntity.CheckItemRequest.webService();
                checkItem.head = new KunShanEntity.CheckItemRequest.Head();
                checkItem.head.name = Global.ServerName;
                checkItem.head.password = Global.MD5(Global.ServerPassword);
                checkItem.request = new KunShanEntity.CheckItemRequest.Request();
                checkItem.request.Id = "0";
                checkItem.request.UpdateDate = "";//DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                string response = XmlHelper.EntityToXml<KunShanEntity.CheckItemRequest.webService>(checkItem);
                string CheckItem = webserver.QueryCheckItem(response);
                List<KunShanEntity.CheckItem> models = JsonHelper.JsonToEntity<List<KunShanEntity.CheckItem>>(CheckItem);
                if (models != null && models.Count >0)
                {
                    csql.DeleteItem("", out err);//删除原有数据
                    KunShanEntity.CheckItem items = new KunShanEntity.CheckItem();
                    for (int i = 0; i < models.Count; i++)
                    {
                        items.Id = models[i].Id;
                        items.ItemCode = models[i].ItemCode;
                        items.ItemName = models[i].ItemName;
                        items.SubItemCode = models[i].SubItemCode;
                        items.SubItemName = models[i].SubItemName;
                        items.UpdateDate = models[i].UpdateDate;

                        csql.InKSItem(items, out err);
                        icount = icount + 1;
                    }                   
                }

                MessageBox.Show("检测项目标准下载成功，共下载" + icount.ToString() + "条记录", "提示");
                //string rtndata = InterfaceHelper.DownloadBasicData(Global.DownLoadItemAndStandard, out err);
                //ResultMsg jsonResult = GetData(rtndata);
                //if (jsonResult == null && jsonResult.result != null)
                //{
                //    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    btnDownItemStand.Enabled = true;
                //    return;
                //}
                //ItemAndStandard itemAndStandard = null;
                //downLoadItemAndStandardList = new List<ItemAndStandard>();
                //downLoadItemAndStandardList = JsonHelper.JsonToEntity<List<ItemAndStandard>>(jsonResult.result.ToString());//(List<ItemAndStandard>)JsonConvert.DeserializeObject(jsonResult.result.ToString(), typeof(List<ItemAndStandard>));
                //if (downLoadItemAndStandardList == null || downLoadItemAndStandardList.Count == 0)
                //{
                //    MessageBox.Show( "暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    btnDownItemStand.Enabled = true;
                //    return;
                //}
                ////删除原来表的数据再插入
                //csql.DeleteItemStandard("", "", out err);

                //for (int i = 0; i < downLoadItemAndStandardList.Count; i++)
                //{                 
                //    itemAndStandard = downLoadItemAndStandardList[i];
                //    csql.InItemStandard(itemAndStandard ,out err );
                //}
               
            }
            catch (Exception ex)
            {
                btnDownItemStand.Enabled = true;
                MessageBox.Show(ex.Message ,"Error");
            }

            //try
            //{
            //    StringBuilder sb = new StringBuilder();
              

            //    dt = csql.GetDownChkItem("", "", out err);

            //    //dt=  csql.GetChkItem("", "", out err);
            //    if (dt != null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            DataDisTable.Clear();
            //            //for (int i = 0; i < dt.Rows.Count; i++)
            //            //{
            //            //    addtable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
            //            //}
            //            CheckDatas.DataSource = dt;//DataDisTable;
            //            CheckDatas.Columns[0].HeaderCell.Value = "样品名称";
            //            CheckDatas.Columns[1].HeaderCell.Value = "检测项目";
            //            CheckDatas.Columns[2].HeaderCell.Value = "检测依据";
            //            CheckDatas.Columns[3].HeaderCell.Value = "标准值";
            //            CheckDatas.Columns[4].HeaderCell.Value = "判定符号";
            //            CheckDatas.Columns[5].HeaderCell.Value = "单位";
            //            for (int j = 0; j < CheckDatas.RowCount; j++)
            //            {
            //                CheckDatas.Columns[0].Width = 200;
            //                CheckDatas.Columns[1].Width = 200;
            //                CheckDatas.Columns[2].Width = 200;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    btnDownItemStand.Enabled = true;
            //    MessageBox.Show(ex.Message, "Error");
            //}
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
            int scount = 0;
            try
            {
                KunShanEntity.SalesItemRequest.webService salesItem = new KunShanEntity.SalesItemRequest.webService();
                salesItem.head = new KunShanEntity.SalesItemRequest.Head();
                salesItem.head.name = Global.ServerName;
                salesItem.head.password = Global.MD5(Global.ServerPassword);
                salesItem.request = new KunShanEntity.SalesItemRequest.Request();
                salesItem.request.Id = "0";
                salesItem.request.UpdateDate = "";//DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                string response = XmlHelper.EntityToXml<KunShanEntity.SalesItemRequest.webService>(salesItem);
                
                response=  webserver.QuerySalesItem(response);
                List<KunShanEntity.SalesItem> models = JsonHelper.JsonToEntity<List<KunShanEntity.SalesItem>>(response);
                if (models != null && models.Count > 0)
                {
                    csql.DeleteKSsample("",out err );//删除原来的数据
                    KunShanEntity.SalesItem samplelist = new KunShanEntity.SalesItem();
                    for(int i=0;i<models.Count;i++)
                    {
                        samplelist.Id = models[i].Id;
                        samplelist.ItemCode = models[i].ItemCode;
                        samplelist.ItemName = models[i].ItemName;
                        //samplelist.SubItemAlias = models[i].SubItemAlias;
                        samplelist.SubItemCode = models[i].SubItemCode;
                        samplelist.SubItemName = models[i].SubItemName;
                        samplelist.UpdateDate = models[i].UpdateDate;

                        csql.InKSSamples(samplelist,out err);
                        scount = scount + 1;
                    }
                }

                MessageBox.Show("检测样品下载成功,共成功下载"+scount +"条数据！", "提示");

                //string sample= InterfaceHelper.DownloadBasicData(Global.DownLoadSample, out err);
                //ResultMsg jsonResult = GetData(sample);
                //if (jsonResult == null && jsonResult.result != null)
                //{
                //    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //downLoadSimpleList = new List<Simple>();
                //downLoadSimpleList = JsonHelper.JsonToEntity<List<Simple>>(jsonResult.result.ToString());
                //if (downLoadSimpleList == null || downLoadSimpleList.Count == 0)
                //{
                //    MessageBox.Show("暂时没有数据可更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                ////删除原有的记录
                // csql.DeleteSample("","",out err);
                // Simple simple = null;
                 
                // for (int i = 0; i < downLoadSimpleList.Count; i++)
                // {
                //     simple = downLoadSimpleList[i];
                //     csql.InSample(simple ,out err );

                // }
                KSsearchsample("");
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"Error");
            }
            btnSampleDown.Enabled = true;
        }

        private void txtSampleType_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            CheckDatas.DataSource = null;
            try
            {
                sb.Length = 0;
                if (txtSample.Text.Trim().Length > 0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }
                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if (sb.ToString().Length > 0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat("ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }

                KSsearchsample(sb.ToString());
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品信息查询错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品信息查询");
            }
        }

        private void txtSample_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            CheckDatas.DataSource = null;
            try
            {
                sb.Length = 0;
                if (txtSample.Text.Trim().Length > 0)
                {
                    sb.AppendFormat("SubItemName like '%{0}%'", txtSample.Text.Trim());
                }
                if (txtSampleType.Text.Trim().Length > 0)
                {
                    if (sb.ToString().Length > 0)
                    {
                        sb.AppendFormat(" and ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                    else
                    {
                        sb.AppendFormat("ItemName like '%{0}%'", txtSampleType.Text.Trim());
                    }
                }

                KSsearchsample(sb.ToString());
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "样品信息查询错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "样品信息查询");
            }
        }
 
    }
}
