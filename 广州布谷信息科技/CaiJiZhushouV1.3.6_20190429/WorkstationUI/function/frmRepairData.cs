using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationModel.Model;
using WorkstationBLL.Mode;
using WorkstationDAL.Basic;
using WorkstationDAL.Model ;

namespace WorkstationUI.function
{
    public partial class frmRepairData : Form
    {
        public frmRepairData()
        {
            InitializeComponent();
        }
        private bool m_IsCreatedDataTable = false;
        public DataTable SaveReadTable = null;
        public ComboBox cmbAdd = new ComboBox();//下拉列表
        private clsSetSqlData sql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private int[] ssid = null;

        private void frmRepairData_Load(object sender, EventArgs e)
        {
            string err = string.Empty;
            initable();
            ssid = new int[Global.AddItem.GetLength(0)];

            for (int i = 0; i < Global.AddItem.GetLength(0); i++)
            {
                addtable(Global.AddItem[i, 0], Global.AddItem[i, 1], Global.AddItem[i, 2], Global.AddItem[i, 3], Global.AddItem[i, 4], Global.AddItem[i, 5]
                    , Global.AddItem[i, 6], Global.AddItem[i, 7], Global.AddItem[i, 8], Global.AddItem[i, 9], Global.AddItem[i, 10], Global.AddItem[i, 11]
                    , Global.AddItem[i, 12], Global.AddItem[i, 13]);
                try
                {
                    sb.Append("Checkitem='");
                    sb.Append(Global.AddItem[i, 1]);
                    sb.Append("' and SampleName='");
                    sb.Append(Global.AddItem[i, 0]);
                    sb.Append("' and CheckData='");
                    sb.Append(Global.AddItem[i, 2]);
                    sb.Append("' and CheckTime=#");
                    sb.Append(DateTime.Parse(Global.AddItem[0, 13]));
                    sb.Append("#");
                    DataTable dt = sql.isSaveID(sb.ToString(), "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ssid[i] = Convert.ToInt32(dt.Rows[0][0].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }

            }
            CheckDatas.DataSource = SaveReadTable;
            cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("清除");
            //cmbAdd.SelectedIndex = 0;
            //cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAdd.Visible = false;
            CheckDatas.Controls.Add(cmbAdd);
            CheckDatas.Columns[2].Width = 160;
            CheckDatas.Columns[6].Width = 160;
            cmbAdd.SelectedValueChanged += new EventHandler(cmbAdd_SelectedValueChanged);
            cmbAdd.KeyUp += new KeyEventHandler(cmbAdd_KeyUp);
        }
        private void addtable(string SampleName, string item, string chkdata, string Unit, string Testbase, string limitdata, string machine, string Result,string dentectunit, string getSamptime, string getSampaddr, string CheckUnit,
           string tester, string CheckTime)
        {
            DataRow dr;
            dr = SaveReadTable.NewRow();

            dr["样品名称"] = SampleName;
            dr["检测项目"] = item;
            dr["检测结果"] = chkdata;
            dr["单位"] = Unit;
            dr["检测依据"] = Testbase;
            dr["标准值"] = limitdata;
            dr["检测仪器"] = machine;
            dr["结论"] = Result;
            dr["检测单位"] = dentectunit;
            dr["采样时间"] = getSamptime;
            dr["采样地点"] = getSampaddr;
            dr["被检单位"] = CheckUnit;
            dr["检定员"] = tester;
            dr["检测时间"] = CheckTime;

            SaveReadTable.Rows.Add(dr);
        }
        private void cmbAdd_KeyUp(object sender,KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;

        }

        private void labelclose_MouseEnter(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.Red;
        }

        private void labelclose_MouseLeave(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.White;
        }
        //关闭
        private void labelclose_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
        const int WM_NCHITTEST = 0x0084;
        const int HT_CAPTION = 2;
        protected override void WndProc(ref Message Msg)
        {
            //禁止双击最大化
            if (Msg.Msg == 0x0112 && Msg.WParam.ToInt32() == 61490) return;
            if (Msg.Msg == WM_NCHITTEST)
            {
                //允许拖动窗体移动
                Msg.Result = new IntPtr(HT_CAPTION);
                return;
            }
            base.WndProc(ref Msg);
        }
        private void initable()
        {
            if (!m_IsCreatedDataTable)
            {
                SaveReadTable = new DataTable("checkDtbl");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测项目";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检定员";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                SaveReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 确定修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelRepair_Click(object sender, EventArgs e)
        {
            clsUpdateData updata = new clsUpdateData();
            clsSetSqlData sql = new clsSetSqlData();
            string err = string.Empty;
            try
            {
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    //updata.bianhao = CheckDatas.Rows[i].Cells[0].Value.ToString();
                    //Global.bianhao = CheckDatas.Rows[i].Cells[0].Value.ToString();
                    ////updata.Chkxiangmu = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    //Global.Chkxiangmu = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    ////updata.ChkTime = CheckDatas.Rows[i].Cells[2].Value.ToString();
                    //Global.ChkTime =DateTime.Parse( CheckDatas.Rows[i].Cells[2].Value.ToString());
                    ////updata.ChkSample = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    //Global.ChkSample = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    //updata.result = CheckDatas.Rows[i].Cells[14].Value.ToString();
                    //updata.unit = CheckDatas.Rows[i].Cells[5].Value.ToString();
                    //updata.GetSampTime = CheckDatas.Rows[i].Cells[6].Value.ToString();
                    //updata.GetSampPlace = CheckDatas.Rows[i].Cells[7].Value.ToString();
                    //updata.intrument = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    //updata.Chktestbase = CheckDatas.Rows[i].Cells[9].Value.ToString();
                    ////updata.ChklimitData = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    //updata.ChkPeople = CheckDatas.Rows[i].Cells[10].Value.ToString();
                    //updata.ChkUnit = CheckDatas.Rows[i].Cells[11].Value.ToString();
                    //updata.quantityin = CheckDatas.Rows[i].Cells[12].Value.ToString();
                    //updata.sampleNum = CheckDatas.Rows[i].Cells[13].Value.ToString();
                    //sql.UpdateResult(updata, out err);

                    updata.ChkSample = CheckDatas.Rows[i].Cells[0].Value.ToString(); ;
                    updata.Chkxiangmu = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    updata.result = CheckDatas.Rows[i].Cells[2].Value.ToString();
                    updata.unit = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    updata.Chktestbase = CheckDatas.Rows[i].Cells[4].Value.ToString();
                    updata.ChklimitData = CheckDatas.Rows[i].Cells[5].Value.ToString();
                    updata.intrument = CheckDatas.Rows[i].Cells[6].Value.ToString();
                    updata.conclusion = CheckDatas.Rows[i].Cells[7].Value.ToString();
                    updata.detectunit = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    updata.GetSampTime = CheckDatas.Rows[i].Cells[9].Value.ToString();
                    updata.GetSampPlace = CheckDatas.Rows[i].Cells[10].Value.ToString();
                    updata.ChkUnit = CheckDatas.Rows[i].Cells[11].Value.ToString();
                    updata.ChkPeople = CheckDatas.Rows[i].Cells[12].Value.ToString();
                    updata.ChkTime = CheckDatas.Rows[i].Cells[13].Value.ToString();

                    sql.RepairResult(updata, ssid[i], out err);
                }

                MessageBox.Show("修改成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            this.Close();
        }

        private void labelRepair_MouseEnter(object sender, EventArgs e)
        {
            labelRepair.ForeColor = Color.Red;
        }

        private void labelRepair_MouseLeave(object sender, EventArgs e)
        {
            labelRepair.ForeColor = Color.White;
        }

       

        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //{
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "被检单位")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "样品名称")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
 
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "采样时间")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "采样地点")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "检测人员")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //}
        }

        private void CheckDatas_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }
        private void cmbAdd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "输入")
            {
                Global.newdata = "查询";
                frminputdata input = new frminputdata();
                input.frd = this;
                input.Show();
            }
            else if (((ComboBox)sender).Text == "清除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbAdd.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();

                }
                cmbAdd.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckDatas.CurrentCell.ColumnIndex < 4)
            {
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.ColumnIndex>-1)
                {
                    CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;  
                    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    cmbAdd.Left = rect.Left;
                    cmbAdd.Top = rect.Top;
                    cmbAdd.Width = rect.Width;
                    cmbAdd.Height = rect.Height;
                    cmbAdd.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 滚动Datagridview下拉框不可见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
        }
        /// <summary>
        /// 改变列宽下拉框不可见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
        }
        
    }
}
