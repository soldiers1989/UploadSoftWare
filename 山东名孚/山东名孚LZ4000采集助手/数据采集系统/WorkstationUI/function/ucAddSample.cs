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

namespace WorkstationUI.function
{
    public partial class ucAddSample : UserControl
    {
        private bool m_IsCreatedDataTable = false;
        private DataTable DataDisTable = null;
        private clsSetSqlData csql = new clsSetSqlData();
        private DataTable dt = null;
        /// <summary>
        /// 下载的检测样品标准集合
        /// </summary>
        private List<ItemAndStandard> downLoadItemAndStandardList = null;
        /// <summary>
        /// 下载的检测样品集合
        /// </summary>
        private List<Simple> downLoadSimpleList = null;

        public ucAddSample()
        {
            InitializeComponent();
        }

        private void ucAddSample_Load(object sender, EventArgs e)
        {
            iniTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                string err = string.Empty;

                //dt = csql.GetDownChkItem("", "", out err);

                dt = csql.GetChkItem("", "", out err);
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
                        //for (int j = 0; j < CheckDatas.RowCount; j++)
                        //{
                        CheckDatas.Columns[0].Width = 200;
                        CheckDatas.Columns[1].Width = 200;
                        CheckDatas.Columns[2].Width = 200;
                        //}
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
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
                    MessageBox.Show(ex.Message, "Error");
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
            string err = string.Empty;
            frmAddSample fas = new frmAddSample();

            //string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
            //   " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
            string sql = "sampleName='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and itemName='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
               " and standardName='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";

            DataTable dt = csql.GetDownItemID(sql, "", out err);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    fas.id = dt.Rows[0][0].ToString();
                }
            }

            string[,] a = new string[CheckDatas.SelectedRows.Count , 6];
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
                try
                {
                    StringBuilder sb = new StringBuilder();

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
                    MessageBox.Show(ex.Message, "Error");
                }
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
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                string err = string.Empty;
                if (!txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    //sb.Append("FtypeNmae like '%");
                    //sb.Append(txtSample.Text);
                    //sb.Append("%' and ");
                    //sb.Append("Name like '%");
                    //sb.Append(txtItem.Text);
                    //sb.Append("%'");
                    sb.Append("sampleName like '%");
                    sb.Append(txtSample.Text);
                    sb.Append("%' and ");
                    sb.Append("itemName like '%");
                    sb.Append(txtItem.Text);
                    sb.Append("%'");
                }
                if (!txtSample.Text.Equals("") && txtItem.Text.Equals(""))
                {
                    sb.Append("sampleName like '%");
                    sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    //sb.Append("Name='");
                    //sb.Append(txtItem.Text);
                    sb.Append("%'");
                }
                if (txtSample.Text.Equals("") && !txtItem.Text.Equals(""))
                {
                    //sb.Append("FtypeNmae='");
                    //sb.Append(txtSample.Text);
                    //sb.Append("' and ");
                    sb.Append("itemName like '%");
                    sb.Append(txtItem.Text);
                    sb.Append("%'");
                }

                //dt = csql.GetChkItem(sb.ToString(), "", out err);
                dt = csql.GetDownChkItem(sb.ToString(), "", out err);
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
                MessageBox.Show(ex.Message,"Error");
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
     
            string err = string.Empty;
            frmAddSample fas = new frmAddSample();
            //string sql = "FtypeNmae='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and Name='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'"+
            //    " and ItemDes='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
            string sql = "sampleName='" + CheckDatas.SelectedRows[0].Cells[0].Value.ToString() + "' and itemName='" + CheckDatas.SelectedRows[0].Cells[1].Value.ToString() + "'" +
                " and standardName='" + CheckDatas.SelectedRows[0].Cells[2].Value.ToString() + "'";
            DataTable dt = csql.GetDownItemID(sql, "", out err);
            if (dt != null)
            {
                if(dt.Rows.Count>0)
                {
                    fas.id = dt.Rows[0][0].ToString();
                }
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
                try
                {
                    StringBuilder sb = new StringBuilder();
                    
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
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
 
    }
}
