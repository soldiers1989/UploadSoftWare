using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class ucServer : UserControl
    {
        public ucServer()
        {
            InitializeComponent();
        }
        clsSetSqlData sql = new clsSetSqlData();
        private bool isCreateTable = false;
        private DataTable displaytable = null;
        private bool addMachine = false;//增加仪器标志
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

                DataTable dt = sql.GetServer(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 300;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void AddItemToTable(string sel,string name, string macf, string communic,string productor)
        {
            DataRow dr;
            dr = displaytable.NewRow();
            dr["已选择"] = sel=="否"?false:true ;
            dr["服务器地址"] = name;
            dr["用户名"] = macf;
            dr["密码"] = communic;
            dr["平台厂家"] = productor;

            displaytable.Rows.Add(dr);
        }
        private void ucServer_Load(object sender, EventArgs e)
        {
            initable();

            CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
            addMachine = false;
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetServer(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[1].Width = 310;
                CheckDatas.Columns[4].Width = 220;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 建表
        /// </summary>
        private void initable()
        {
            if (isCreateTable == false)
            {
                displaytable = new DataTable("webServer");
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool );
                dataCol.ColumnName = "已选择";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "服务器地址";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "用户名";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "密码";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "平台厂家";
                displaytable.Columns.Add(dataCol);
            }
            isCreateTable = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
            addMachine = false;
            try
            {
                addMachine = true;
                CheckDatas.DataSource = null;
                displaytable.Clear();
                
                DataRow dr;
                dr = displaytable.NewRow();
                dr["已选择"] =false;
                dr["服务器地址"] = "";
                dr["用户名"] = "";
                dr["密码"] = "";
                dr["平台厂家"] = "";
                displaytable.Rows.Add(dr);
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[1].Width = 310;
                CheckDatas.Columns[4].Width = 220;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (addMachine == true)
            {
                if (CheckDatas.Rows[0].Cells[0].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("服务器地址不能为空", "提示");
                    return;
                }
                if (CheckDatas.Rows[0].Cells[1].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    return;
                }
                if (CheckDatas.Rows[0].Cells[2].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("密码不能为空", "提示");
                    return;
                }
                int Ins = 0;
                try
                {
                    Ins = sql.insertServer(CheckDatas.Rows[0].Cells[0].Value.ToString(), CheckDatas.Rows[0].Cells[1].Value.ToString(), CheckDatas.Rows[0].Cells[2].Value.ToString(),
                        CheckDatas.Rows[0].Cells[3].Value.ToString(), CheckDatas.Rows[0].Cells[4].Value.ToString());
                    if (Ins == 1)
                    {
                        MessageBox.Show("数据保存成功");
                        //用户不能编辑单元格  
                        CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                        addMachine = false;
                        try
                        {
                            string err = string.Empty;
                            StringBuilder sb = new StringBuilder();
                            sb.Clear();
                            displaytable.Clear();
                            sb.Append("order by ID");

                            DataTable dt = sql.GetServer(sb.ToString(), out err);
                            if (dt != null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                                }
                            }
                            CheckDatas.DataSource = displaytable;
                            CheckDatas.Columns[1].Width = 300;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("请新增再保存数据", "操作提示");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (addMachine == true)
            {
                MessageBox.Show("请在服务器列表选择需要删除的服务器名", "操作提示");
                return;
            }
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要删除的服务器名", "操作提示");
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                int d = 0;
                sb.Append("webAddress='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[1].Value.ToString());
                sb.Append("' and ");
                sb.Append("name='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[2].Value.ToString());
                sb.Append("' and ");
                sb.Append("pd='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[3].Value.ToString());
                sb.Append("' and ");
                sb.Append("productor='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[4].Value.ToString());
                sb.Append("'");
                d = sql.deleteServer(sb.ToString());
                if (d == 1)
                {
                    MessageBox.Show("删除成功", "提示");
                    //用户不能编辑单元格  
                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                    addMachine = false;
                    try
                    {
                        string err = string.Empty;
                        //StringBuilder sb = new StringBuilder();
                        sb.Clear();
                        displaytable.Clear();
                        sb.Append("order by ID");

                        DataTable dt = sql.GetServer(sb.ToString(), out err);
                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                            }
                        }
                        CheckDatas.DataSource = displaytable;
                        CheckDatas.Columns[0].Width = 300;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                string err = string.Empty;
                if (CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString() == "False")
                {
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = true;
                    try
                    {
                        sql.SetWebServerce(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString(), out err);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
                else
                {
                    CheckDatas.Rows[e.RowIndex].Cells[0].Value = false;
                    try
                    {
                        sql.SetWebServerce(CheckDatas.Rows[e.RowIndex].Cells[0].Value.ToString(), CheckDatas.Rows[e.RowIndex].Cells[2].Value.ToString(), out err);
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
