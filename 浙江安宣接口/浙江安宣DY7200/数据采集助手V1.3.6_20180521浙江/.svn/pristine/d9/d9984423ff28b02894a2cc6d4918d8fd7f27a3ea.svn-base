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
using WorkstationModel.Model;

namespace WorkstationUI.function
{
    public partial class ucEquipmenManage : UserControl
    {
        public ucEquipmenManage()
        {
            InitializeComponent();
        }
        private clsSetSqlData sql = new clsSetSqlData();
        private bool isCreateTable=false ;
        private bool isSpecial = false;
        private DataTable displaytable = null;
        private DataTable spectable = null;
        private bool addMachine = false;//增加仪器标志
        private string intrument = string.Empty;
        private int mach = 0;
        private StringBuilder sb = new StringBuilder();
        private clsdiary dy = new clsdiary();

        private void ucEquipmenManage_Load(object sender, EventArgs e)
        {
            try
            {
                if (Global.userlog != "sakj")
                {
                    //普通用户
                    publictable();
                    
                    btnAdd.Visible = false;
                    btnsave.Visible = false;
                    btnDelete.Visible = false;
                    btnUpdate.Visible = false;
                    chkBoxMachine.Visible = false;

                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                    string err = string.Empty;
                    sb.Length = 0;
                    spectable.Clear();
                    sb.Append("order by ID");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null && dt.Rows.Count>0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Isselect"].ToString() == "是")
                            {
                                addpublictable(dt.Rows[i]["Numbering"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Manufacturer"].ToString(), dt.Rows[i]["communication"].ToString());
                            }
                        }
                        CheckDatas.DataSource = spectable;
                        this.CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        CheckDatas.Columns[1].Width = 280;
                        CheckDatas.Columns[2].Width = 180;
                    }
                   
                }
                else if (Global.userlog == "sakj")
                {
                    initable();
                    //用户不能编辑单元格  
                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
               
                    string err = string.Empty;
                    //StringBuilder sb = new StringBuilder();
                    sb.Length=0;
                    displaytable.Clear();
                    sb.Append("order by ID");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddItemToTable(dt.Rows[i]["Isselect"].ToString(), dt.Rows[i]["Numbering"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Manufacturer"].ToString(), dt.Rows[i]["communication"].ToString(), dt.Rows[i]["Protocol"].ToString());
                            if (dt.Rows[i]["Isselect"].ToString() == "是")
                            {
                                //labelEquipment.Text = "已选择仪器：" + dt.Rows[i][0].ToString();

                            }
                        }
                    }
                    CheckDatas.DataSource = displaytable;
                    this.CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CheckDatas.Columns[2].Width = 280;
                    CheckDatas.Columns[3].Width = 180;
                
                }
                dy.savediary(DateTime.Now.ToString(), "进入仪器管理" , "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入仪器管理失败："+ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入仪器管理");
            }
        }
        private void publictable()
        {
            if (isSpecial == false)
            {
                spectable = new DataTable();
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "仪器编号";
                spectable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "仪器名称";
                spectable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "制造商";
                spectable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通信方式";
                spectable.Columns.Add(dataCol);                
            }
            isSpecial = true ;
 
        }
        /// <summary>
        /// 建表
        /// </summary>
        private void initable()
        {
            if(isCreateTable ==false)
            {
                displaytable = new DataTable("Instrument");
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已选择";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "仪器编号";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType=typeof(string);
                dataCol.ColumnName = "仪器名称";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "制造商";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通信方式";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通信协议";
                displaytable.Columns.Add(dataCol);
                
            }
            isCreateTable = true;
        }
        private void addpublictable(string numing,string name,string macf,string communic)
        {
            DataRow dr;
            dr = spectable.NewRow();

            dr["仪器编号"] = numing;
            dr["仪器名称"] = name;
            dr["制造商"] = macf;
            dr["通信方式"] = communic;

            spectable.Rows.Add(dr);
        }

        /// <summary>
        /// 特殊用户
        /// </summary>
        /// <param name="select"></param>
        /// <param name="num"></param>
        /// <param name="name"></param>
        /// <param name="macf"></param>
        /// <param name="communic"></param>
        /// <param name="protocol"></param>
        private void AddItemToTable(string select,string num,string name,string macf,string communic,string protocol)
        {
            DataRow dr;
            dr =displaytable.NewRow();
            dr["已选择"] =(select=="是"?true:false );
            dr["仪器编号"] = num;
            dr["仪器名称"] = name;
            dr["制造商"] = macf;
            dr["通信方式"] = communic;
            dr["通信协议"] = protocol;
            displaytable.Rows.Add(dr);
        }

        //刷新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //用户不能编辑单元格  
            CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
            addMachine = false;
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i]["Isselect"].ToString(), dt.Rows[i]["Numbering"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Manufacturer"].ToString(), dt.Rows[i]["communication"].ToString()
                            , dt.Rows[i]["Protocol"].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CheckDatas.Columns[2].Width = 300;
                CheckDatas.Columns[3].Width = 200;
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "刷新失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "刷新");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addMachine = true;
            CheckDatas.DataSource =null;
            displaytable.Clear();
           
            DataRow dr;
            dr = displaytable.NewRow();
            dr["已选择"] = false;
            dr["仪器编号"] = "";
            dr["仪器名称"] = "";
            dr["制造商"] = "";
            dr["通信方式"] = "";
            dr["通信协议"] = "";
            displaytable.Rows.Add(dr);
            CheckDatas.DataSource = displaytable;
            CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CheckDatas.Columns[2].Width = 280;
            CheckDatas.Columns[3].Width = 180;
            CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
         
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (addMachine == true)
            {
                if (CheckDatas.Rows[0].Cells[0].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("仪器名称不能为空","提示");
                    return;
                }
                if (CheckDatas.Rows[0].Cells[1].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("生产厂家不能为空", "提示");
                    return;
                }
                if (CheckDatas.Rows[0].Cells[2].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("通信方式不能为空", "提示");
                    return;
                }
                int Ins = 0;
                try
                {
                    Ins = sql.insertInstrument(CheckDatas.Rows[0].Cells["仪器名称"].Value.ToString(), CheckDatas.Rows[0].Cells["制造商"].Value.ToString(), CheckDatas.Rows[0].Cells["通信方式"].Value.ToString(),
                        CheckDatas.Rows[0].Cells["已选择"].Value.ToString(), CheckDatas.Rows[0].Cells["仪器编号"].Value.ToString(), CheckDatas.Rows[0].Cells["通信协议"].Value.ToString());
                    if (Ins == 1)
                    {
                        MessageBox.Show("数据保存成功");
                    }

                    //用户不能编辑单元格  
                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                    addMachine = false;
                   
                    string err = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    displaytable.Clear();
                    sb.Append(" order by ID");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddItemToTable(dt.Rows[i]["Isselect"].ToString(), dt.Rows[i]["Numbering"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Manufacturer"].ToString(), dt.Rows[i]["communication"].ToString()
                                , dt.Rows[i]["Protocol"].ToString());
                        }
                    }
                    CheckDatas.DataSource = displaytable;
                    CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CheckDatas.Columns[2].Width = 300;
                    CheckDatas.Columns[3].Width = 200;
                }
                catch(Exception ex)
                {
                    dy.savediary(DateTime.Now.ToString(), "保存数据失败：" + ex.Message, "错误");
                    MessageBox.Show(ex.Message ,"保存数据");
                }
            }
            else
            {
                MessageBox.Show("请新增再保存数据","操作提示");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (addMachine == true)
            {
                MessageBox.Show("请在仪器列表选择需要删除的仪器","操作提示");
                return;
            }
            if(CheckDatas.SelectedCells.Count<1)
            {               
                MessageBox.Show("请选择需要删除的仪器","操作提示");
                return;
            }
            try
            {
                int sl = 0;
                for (int j = 0; j < CheckDatas.Rows.Count; j++)
                {
                    if (CheckDatas.Rows[j].Cells["已选择"].Value.ToString() == "True")
                    {
                        sl = sl + 1;
                    }
                }
                if (sl == 1)//只有一款仪器不给删除
                {
                    MessageBox.Show("不能删除唯一选中的一款仪器", "提示");
                    return;
                }

                DialogResult dr= MessageBox.Show("是否删除选择的仪器","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    StringBuilder sb = new StringBuilder();
                   
                    for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                    {                                                    
                        sb.Clear();
                        int d = 0;
                        sb.Append("Name='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells["仪器名称"].Value.ToString());
                        sb.Append("' and ");
                        sb.Append("Manufacturer='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells["制造商"].Value.ToString());
                        sb.Append("' and ");
                        sb.Append("communication='");
                        sb.Append(CheckDatas.SelectedRows[i].Cells["通信方式"].Value.ToString());
                        sb.Append("'");
                        d = sql.deleteInstrument(sb.ToString());      
                    }
                    string err = string.Empty;
                       
                    displaytable.Clear();
                    sb.Clear();
                    sb.Append(" order by ID");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            //AddItemToTable(dt.Rows[j][3].ToString(), dt.Rows[j][5].ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString()
                            //    , dt.Rows[j][6].ToString());
                            AddItemToTable(dt.Rows[j]["Isselect"].ToString(), dt.Rows[j]["Numbering"].ToString(), dt.Rows[j]["Name"].ToString(), dt.Rows[j]["Manufacturer"].ToString(), dt.Rows[j]["communication"].ToString()
                            , dt.Rows[j]["Protocol"].ToString());
                        }
                    }
                    CheckDatas.DataSource = displaytable;
                    CheckDatas.Columns[2].Width = 300;
                    CheckDatas.Columns[3].Width = 200;
                    //用户不能编辑单元格  
                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                    addMachine = false;
                    MessageBox.Show("删除成功", "提示");
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "删除仪器失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "删除仪器");
            }
        }
        /// <summary>
        /// 单击选种测试仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Global.userlog == "sakj")
                {
                    if (e.ColumnIndex == 0)
                    {
                        string err = string.Empty;
                        if (CheckDatas.Rows[e.RowIndex].Cells["已选择"].Value.ToString() == "False")
                        {
                            CheckDatas.Rows[e.RowIndex].Cells["已选择"].Value = true;
                            //labelEquipment.Text = "已选择仪器：" + CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString();
                            //Global.TestInstrument[0,0] = CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString();
                            try
                            {
                                sql.SetIntrument(CheckDatas.Rows[e.RowIndex].Cells["已选择"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["仪器编号"].Value.ToString(), out err);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error");
                                return;
                            }
                            //只选择一款仪器
                            if (chkBoxMachine.Checked == true)
                            {
                                for (int j = 0; j < CheckDatas.Rows.Count; j++)
                                {
                                    if (j != e.RowIndex)
                                    {
                                        if (CheckDatas.Rows[j].Cells["已选择"].Value.ToString() == "True")
                                        {
                                            //string d = CheckDatas.Rows[j].Cells[0].Value.ToString(); 
                                            CheckDatas.Rows[j].Cells["已选择"].Value = false;
                                            try
                                            {
                                                sql.SetIntrument(CheckDatas.Rows[j].Cells["已选择"].Value.ToString(), CheckDatas.Rows[j].Cells["仪器编号"].Value.ToString(), out err);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message, "Error");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            int sl = 0;
                            for (int j = 0; j < CheckDatas.Rows.Count; j++)
                            {
                                if (CheckDatas.Rows[j].Cells["已选择"].Value.ToString() == "True")
                                {
                                    sl = sl + 1;
                                }
                            }
                            if (sl == 1)//只有一款仪器不给去掉勾选
                            {
                                MessageBox.Show("至少选择一款仪器", "提示");
                                return;
                            }
                            CheckDatas.Rows[e.RowIndex].Cells["已选择"].Value = false;
                            try
                            {
                                sql.SetIntrument(CheckDatas.Rows[e.RowIndex].Cells["已选择"].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells["仪器编号"].Value.ToString(), out err);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error");
                            }
                        }
                    }
                    else if (e.ColumnIndex > 3)
                    {
                        if (addMachine == false)
                        {

                            //frmMachine mh = new frmMachine();
                            //mh.ucem = this;
                            //string err = string.Empty;
                            //DialogResult dr = mh.ShowDialog();
                            //if (dr == DialogResult.OK)
                            //{
                            //    if (CheckDatas.CurrentCell.ColumnIndex == 4)
                            //    {
                            //        sql.RepairIntrument(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString(), CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString(),
                            //            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString(),CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[6].Value.ToString(), out err);
                            //    }
                            //    if (CheckDatas.CurrentCell.ColumnIndex == 3)
                            //    {
                            //sql.RepairIntrument(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString(), CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString(),
                            //           CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString(),CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[6].Value.ToString(), out err);
                            //    }
                            //}                  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "选择测试仪器失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"选择测试仪器");
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>Q21`
        private void btnrepair_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.userlog != "sakj")//普通用户
                {
                    if (CheckDatas.SelectedRows.Count > 0)
                    {
                        Global.ediIntrument[0, 0] = CheckDatas.SelectedRows[0].Cells["仪器编号"].Value.ToString();
                        Global.ediIntrument[0, 1] = CheckDatas.SelectedRows[0].Cells["仪器名称"].Value.ToString();
                        Global.ediIntrument[0, 2] = CheckDatas.SelectedRows[0].Cells["制造商"].Value.ToString();
                        Global.ediIntrument[0, 3] = CheckDatas.SelectedRows[0].Cells["通信方式"].Value.ToString();
                        string err = string.Empty;
                        StringBuilder sb = new StringBuilder();
                        sb.Length = 0;
                        sb.Append(" where Name='");
                        sb.Append(Global.ediIntrument[0, 1]);
                        sb.Append("' and Numbering='");
                        sb.Append(Global.ediIntrument[0, 0]);
                        sb.Append("' and Manufacturer='");
                        sb.Append(Global.ediIntrument[0, 2]);
                        sb.Append("' and ");
                        sb.Append("communication='");
                        sb.Append(Global.ediIntrument[0, 3]);
                        sb.Append("'");

                        DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            
                             Global.ediIntrument[0, 4] = dt.Rows[0]["ID"].ToString();
                            
                        }
                        frmPubIntru frmp = new frmPubIntru();
                        frmp.ShowDialog();

                        spectable.Clear();
                        sb.Clear();
                        sb.Append("order by ID");

                        DataTable dtc = sql.GetIntrument(sb.ToString(), out err);
                        if (dt != null)
                        {
                            for (int i = 0; i < dtc.Rows.Count; i++)
                            {
                                string d = dtc.Rows[i]["Isselect"].ToString();
                                if (dtc.Rows[i]["Isselect"].ToString() == "是")
                                {
                                    addpublictable(dtc.Rows[i]["Numbering"].ToString(), dtc.Rows[i]["Name"].ToString(), dtc.Rows[i]["Manufacturer"].ToString(), dtc.Rows[i]["communication"].ToString());
                                }
                            }
                            CheckDatas.DataSource = spectable;
                            this.CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            CheckDatas.Columns[1].Width = 280;
                            CheckDatas.Columns[2].Width = 180;
                        }
                    }
                }
                else
                {
                   
                    Global.ediIntrument[0, 0] = CheckDatas.SelectedRows[0].Cells["仪器编号"].Value.ToString();
                    Global.ediIntrument[0, 1] = CheckDatas.SelectedRows[0].Cells["仪器名称"].Value.ToString();
                    Global.ediIntrument[0, 2] = CheckDatas.SelectedRows[0].Cells["制造商"].Value.ToString();
                    Global.ediIntrument[0, 3] = CheckDatas.SelectedRows[0].Cells["通信方式"].Value.ToString();
                    Global.ediIntrument[0, 4] = CheckDatas.SelectedRows[0].Cells["通信协议"].Value.ToString();

                    string err = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    sb.Length = 0;
                    sb.Append(" where Name='");
                    sb.Append(Global.ediIntrument[0, 1]);
                    sb.Append("' and Numbering='");
                    sb.Append(Global.ediIntrument[0, 0]);
                    sb.Append("' and Manufacturer='");
                    sb.Append(Global.ediIntrument[0, 2]);
                    sb.Append("' and ");
                    sb.Append("communication='");
                    sb.Append(Global.ediIntrument[0, 3]);
                    sb.Append("'");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                        Global.ediIntrument[0, 5] = dt.Rows[0]["ID"].ToString();
                        
                    }

                    frmIntrument frmIntru = new frmIntrument();
                    frmIntru.ShowDialog();

                    btnUpdate_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "修改失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"修改");
            }
        }
        /// <summary>
        ///仅选一款仪器 单击取消选中部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxMachine_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (chkBoxMachine.Checked == true)
                {
                    bool sel = false;
                    string err = string.Empty;
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["已选择"].Value.ToString() == "True" && sel == false)//单击仅选一款仪器
                        {
                            CheckDatas.Rows[i].Cells["已选择"].Value = true;
                            sel = true;
                            sql.SetIntrument(CheckDatas.Rows[i].Cells["已选择"].Value.ToString(), CheckDatas.Rows[i].Cells["仪器编号"].Value.ToString(), out err);
                        }
                        else if (CheckDatas.Rows[i].Cells["已选择"].Value.ToString() == "True" && sel == true)
                        {
                            CheckDatas.Rows[i].Cells["已选择"].Value = false;

                            sql.SetIntrument(CheckDatas.Rows[i].Cells["已选择"].Value.ToString(), CheckDatas.Rows[i].Cells["仪器编号"].Value.ToString(), out err);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "选择仪器失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message ,"选择仪器");
            }
        }
    }
}
