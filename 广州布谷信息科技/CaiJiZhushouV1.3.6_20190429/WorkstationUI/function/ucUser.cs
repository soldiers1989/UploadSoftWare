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
    public partial class ucUser : UserControl
    {
        public ucUser()
        {
            InitializeComponent();
        }
        clsSetSqlData sql = new clsSetSqlData();
        private bool isCreateTable = false;
        private DataTable displaytable = null;
        private bool adduser = false;//新增用户标志
        private clsdiary dy = new clsdiary();
        
        private void ucUser_Load(object sender, EventArgs e)
        {
            initable();
            //用户不能编辑单元格  
            CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 180;
                CheckDatas.Columns[1].Width = 180;
                CheckDatas.Columns[2].Width = 180;
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "用户管理错误："+ex.Message , "错误");
                MessageBox.Show(ex.Message, "用户管理");
            }
            dy.savediary(DateTime.Now.ToString(), "用户管理", "成功");
        }
        /// <summary>
        /// 建表
        /// </summary>
        private void initable()
        {
            if (isCreateTable == false)
            {
                displaytable = new DataTable("Instrument");
                DataColumn dataCol;

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
                dataCol.ColumnName = "用户类型";
                displaytable.Columns.Add(dataCol);
            }
            isCreateTable = true;
        }
        private void AddItemToTable(string name, string macf, string communic)
        {
            DataRow dr;
            dr = displaytable.NewRow();
            dr["用户名"] = name;
            dr["密码"] = macf;
            dr["用户类型"] = communic;

            displaytable.Rows.Add(dr);
        }
        //新增
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                adduser = true;

                //CheckDatas.DataSource = null;
                //displaytable.Clear();
                ////panelmain.Text = "新增用户";
                //DataRow dr;
                //dr = displaytable.NewRow();
                //dr["用户名"] = "";
                //dr["密码"] = "";
                //dr["用户类型"] = "";
                //displaytable.Rows.Add(dr);
                //CheckDatas.DataSource = displaytable;
                //CheckDatas.Columns[0].Width = 180;
                //CheckDatas.Columns[1].Width = 180;
                //CheckDatas.Columns[2].Width = 200;
                //CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;

                frmEditUser feu = new frmEditUser();
                feu.tileName = "新建用户";
                feu.ShowDialog();

                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 180;
                CheckDatas.Columns[1].Width = 180;
                CheckDatas.Columns[2].Width = 180;
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "新增用户错误："+ex.Message, "错误");
                MessageBox.Show(ex.Message,"新增用户");
            }
            
        }
        //保存
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (adduser == true)
                {
                    if (CheckDatas.Rows[0].Cells[0].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("用户名不能为空", "提示");
                        return;
                    }
                    if (CheckDatas.Rows[0].Cells[1].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("密码不能为空", "提示");
                        return;
                    }
                    if (CheckDatas.Rows[0].Cells[2].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("用户类型不能为空", "提示");
                        return;
                    }
                   
                    int save = 0;
                    save = sql.AddUser(CheckDatas.Rows[0].Cells[0].Value.ToString(), CheckDatas.Rows[0].Cells[1].Value.ToString(), CheckDatas.Rows[0].Cells[2].Value.ToString());
                    if (save == 1)
                    {
                        MessageBox.Show("新增用户名成功");
                    }
                    adduser = false;
                    //用户不能编辑单元格  
                    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;

                    string err = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    displaytable.Clear();
                    sb.Append("order by ID");

                    DataTable dt = sql.GetUser(sb.ToString(), out err);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                        }
                    }
                    CheckDatas.DataSource = displaytable;
                    CheckDatas.Columns[0].Width = 180;
                    CheckDatas.Columns[1].Width = 180;
                    CheckDatas.Columns[2].Width = 180;
                }
                else
                {
                    MessageBox.Show("请新增再保存数据", "提示");
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "保存数据错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"保存数据");
            }

        }
        //刷新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            adduser = false;
            //用户不能编辑单元格  
            CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
            try
            {
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Append(" order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 180;
                CheckDatas.Columns[1].Width = 180;
                CheckDatas.Columns[2].Width = 180;
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "刷新错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "刷新");
            }
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (adduser == true)
            {
                MessageBox.Show("请在仪器列表选择需要删除的仪器", "操作提示");
                return;
            }
            if (CheckDatas.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择需要删除的仪器", "操作提示");
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                int d = 0;
                sb.Append("userlog='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[0].Value.ToString());
                sb.Append("' and ");
                sb.Append("passData='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[1].Value.ToString());
                sb.Append("' and ");
                sb.Append("usertype='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[2].Value.ToString());
                sb.Append("'");
                d = sql.deletetUser(sb.ToString());
                if (d == 1)
                {
                    MessageBox.Show("删除成功", "提示");
                }
                adduser = false;
                //用户不能编辑单元格  
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
               
                string err = string.Empty;
                //StringBuilder sb = new StringBuilder();
                displaytable.Clear();
                sb.Clear();
                sb.Append("order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 180;
                CheckDatas.Columns[1].Width = 180;
                CheckDatas.Columns[2].Width = 180;

            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "删除错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "删除");
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepair_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string err = string.Empty;
                sb.Append("where userlog='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[0].Value.ToString());
                sb.Append("' and passData='");
                sb.Append(CheckDatas.SelectedRows[0].Cells[1].Value.ToString());
                sb.Append("'");
                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Global.edituser[0, 3] = dt.Rows[0][3].ToString();
                    }
                }

                Global.edituser[0, 0] = CheckDatas.SelectedRows[0].Cells[0].Value.ToString();
                Global.edituser[0, 1] = CheckDatas.SelectedRows[0].Cells[1].Value.ToString();
                Global.edituser[0, 2] = CheckDatas.SelectedRows[0].Cells[2].Value.ToString();
                frmEditUser feu = new frmEditUser();
                feu.tileName = "编辑登录用户";
                feu.ShowDialog();
                btnUpdate_Click(null, null);
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "修改错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"修改");
            }
        }
    }
}
