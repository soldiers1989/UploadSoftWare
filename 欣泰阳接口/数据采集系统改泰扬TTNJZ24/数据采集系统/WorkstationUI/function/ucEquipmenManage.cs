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

namespace WorkstationUI.function
{
    public partial class ucEquipmenManage : UserControl
    {
        public ucEquipmenManage()
        {
            InitializeComponent();
        }
        clsSetSqlData sql = new clsSetSqlData();
        private bool isCreateTable=false ;
        private DataTable displaytable = null;
        private bool addMachine = false;//增加仪器标志
        private string intrument = string.Empty;
        private int mach = 0;
        private void ucEquipmenManage_Load(object sender, EventArgs e)
        {
            panel1.VerticalScroll.Enabled=true;
            //CheckDatas.Visible = false;
            //btnAdd.Visible = false;
            //btnsave.Visible = false;
            //btnUpdate.Visible = false;
            //btnDelete.Visible = false;

            initable();
            //用户不能编辑单元格  
            CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;  
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append("order by ID");

                DataTable dt= sql.GetIntrument(sb.ToString(),out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][6].ToString());
                        if (dt.Rows[i][3].ToString() == "是")
                        {
                            labelEquipment.Text = "已选择仪器：" + dt.Rows[i][0].ToString();

                        }
                    }
                }                
                CheckDatas.DataSource = displaytable;
                this.CheckDatas.Columns[0].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
                CheckDatas.Columns[2].Width = 280;
                CheckDatas.Columns[3].Width = 180;             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
            }
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
                        AddItemToTable(dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()
                            , dt.Rows[i][6].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CheckDatas.Columns[2].Width = 300;
                CheckDatas.Columns[3].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
                    Ins = sql.insertInstrument(CheckDatas.Rows[0].Cells[2].Value.ToString(), CheckDatas.Rows[0].Cells[3].Value.ToString(), CheckDatas.Rows[0].Cells[4].Value.ToString(),
                        CheckDatas.Rows[0].Cells[0].Value.ToString(), CheckDatas.Rows[0].Cells[1].Value.ToString(), CheckDatas.Rows[0].Cells[5].Value.ToString());
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
                    sb.Append("order by ID");

                    DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddItemToTable(dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()
                                , dt.Rows[i][6].ToString());
                        }
                    }
                    CheckDatas.DataSource = displaytable;
                    CheckDatas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CheckDatas.Columns[2].Width = 300;
                    CheckDatas.Columns[3].Width = 200;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message ,"Error");
                }
            }
            else
            {
                MessageBox.Show("请新增再保存数据","操作提示");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (addMachine == true)
            {
                MessageBox.Show("请在仪器列表选择需要删除的仪器","操作提示");
                return;
            }
            if(CheckDatas.SelectedRows.Count<1)
            {
                MessageBox.Show("请选择需要删除的仪器","操作提示");
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                int d = 0;
                sb.Append("Name='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[2].Value.ToString());
                sb.Append("' and ");
                sb.Append("Manufacturer='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[3].Value.ToString());
                sb.Append("' and ");
                sb.Append("communication='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[4].Value.ToString());
                sb.Append("'");
                d=sql.deleteInstrument(sb.ToString());
                if (d == 1)
                {
                    MessageBox.Show("删除成功","提示");
                }

                //用户不能编辑单元格  
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                addMachine = false;
               
                string err = string.Empty;
                    //StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetIntrument(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()
                            , dt.Rows[i][6].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[2].Width = 300;
                CheckDatas.Columns[3].Width = 200;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
            }
        }

        /// <summary>
        /// 单击选种测试仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
               
                string err = string.Empty;
                if (CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString() == "False")
                {
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = true;
                    labelEquipment.Text = "已选择仪器：" + CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //Global.TestInstrument[0,0] = CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString();
                    try
                    {
                        sql.SetIntrument(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString(), out err);
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
                                if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "True")
                                {
                                    //string d = CheckDatas.Rows[j].Cells[0].Value.ToString(); 
                                    CheckDatas.Rows[j].Cells[0].Value = false;
                                    try
                                    {
                                        sql.SetIntrument(CheckDatas.Rows[j].Cells[0].Value.ToString(), CheckDatas.Rows[j].Cells[1].Value.ToString(), out err);
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
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = false ;
                    try
                    {
                         sql.SetIntrument(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[1].Value.ToString(), out err);
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

                    frmMachine mh = new frmMachine();
                    mh.ucem = this;
                    string err = string.Empty;
                    DialogResult dr = mh.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        if (CheckDatas.CurrentCell.ColumnIndex == 4)
                        {
                            sql.RepairIntrument(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString(), CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString(),
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString(),CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[6].Value.ToString(), out err);
                        }
                        if (CheckDatas.CurrentCell.ColumnIndex == 3)
                        {
                            sql.RepairIntrument(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString(), CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[4].Value.ToString(),
                               CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString(),CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[6].Value.ToString(), out err);
                        }
                    }                  
                }
            }
        }

     
    }
}
