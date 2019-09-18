using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class frmnewdata : Form
    {
        public frmnewdata()
        {
            InitializeComponent();
        }
        #region WinForm窗体重绘
        const int WM_NCHITTEST = 0x0084;
        const int HT_LEFT = 10;
        const int HT_RIGHT = 11;
        const int HT_TOP = 12;
        const int HT_TOPLEFT = 13;
        const int HT_TOPRIGHT = 14;
        const int HT_BOTTOM = 15;
        const int HT_BOTTOMLEFT = 16;
        const int HT_BOTTOMRIGHT = 17;
        const int HT_CAPTION = 2;
        #endregion

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
        private bool m_IsCreatedDataTable = false;
        public DataTable SaveReadTable = null;
        public ComboBox cmbAdd = new ComboBox();//下拉列表


        private void labelclose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void labelclose_MouseEnter(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.Red;
        }

        private void labelclose_MouseLeave(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.White;
        }

        private void frmnewdata_Load(object sender, EventArgs e)
        {
            initable();

            for (int i = 0; i < Global.AddItem.Length / 15; i++)
            {
                addtable(Global.AddItem[i, 0], Global.AddItem[i, 1], Global.AddItem[i, 2], Global.AddItem[i, 3], Global.AddItem[i, 4], Global.AddItem[i, 5]
                    , Global.AddItem[i, 6], Global.AddItem[i, 7], Global.AddItem[i, 8], Global.AddItem[i, 9], Global.AddItem[i, 10], Global.AddItem[i, 11]
                    , Global.AddItem[i, 12], Global.AddItem[i, 13], Global.AddItem[i, 14]);
            }
            CheckDatas.DataSource = SaveReadTable;
            cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            //cmbAdd.SelectedIndex = 0;
            //cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAdd.Visible = false;
            CheckDatas.Controls.Add(cmbAdd);
            CheckDatas.Columns[2].Width = 160;
            CheckDatas.Columns[6].Width = 160;
            cmbAdd.SelectedValueChanged += new EventHandler(cmbAdd_SelectedValueChanged);
            cmbAdd.KeyUp += new KeyEventHandler(cmbAdd_KeyUp);
        }
        private void cmbAdd_KeyUp(object Sender,KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;
        }
        private void addtable(string num, string item, string chktime, string sample, string result, string unit, string gettime, string getplace, string intrum
            , string testbase, string tester, string testunit, string quantityin, string SampleNum,string con)
        {
            DataRow dr;
            dr = SaveReadTable.NewRow();
            dr["检测编号"] = num;
            dr["检测项目"] = item;
            dr["检测时间"] = chktime;
            dr["样品名称"] = sample;
            dr["检测结果"] = result;
            dr["单位"] = unit;
            dr["采样时间"] = gettime;
            dr["采样地点"] = getplace;
            dr["检测仪器"] = intrum;
            dr["检测依据"] = testbase;
            dr["检测人员"] = tester;
            dr["被检单位"] = testunit;
            //dr["进货数量"] = quantityin;
            //dr["采样数量"] = SampleNum;
            dr["结论"] = con;
            SaveReadTable.Rows.Add(dr);
        }

        private void initable()
        {
            if (!m_IsCreatedDataTable)
            {
                SaveReadTable = new DataTable("checkDtbl");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                //dataCol.DataType = typeof(bool);
                //dataCol.ColumnName = "已保存";
                //SaveReadTable.Columns.Add(dataCol);
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测编号";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
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
                dataCol.ColumnName = "采样时间";//检测值
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测人员";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                SaveReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "进货数量";
                //SaveReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样数量";
                //SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                SaveReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        private void cmbAdd_SelectedValueChanged(object sender, EventArgs e)
        {
            Global.newdata = "少查询";
            if (((ComboBox)sender).Text == "输入")
            {
                //CheckDatas.CurrentCell.Value = "1234";
                frminputdata input = new frminputdata();
                input.fnd = this;
                //input.frd = this;
                input.Show();
            }
            else if (((ComboBox)sender).Text == "删除")
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

        private void labelSave_Click(object sender, EventArgs e)
        {
            clsUpdateData updata = new clsUpdateData();
            clsSetSqlData sql = new clsSetSqlData();
            string err = string.Empty;
            try
            {
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    //updata.bianhao = CheckDatas.Rows[i].Cells[0].Value.ToString();
                    Global.bianhao = CheckDatas.Rows[i].Cells[0].Value.ToString();
                    //updata.Chkxiangmu = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    Global.Chkxiangmu = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    //updata.ChkTime = CheckDatas.Rows[i].Cells[2].Value.ToString();
                    Global.ChkTime =DateTime.Parse( CheckDatas.Rows[i].Cells[2].Value.ToString());
                    //updata.ChkSample = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    Global.ChkSample = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    updata.result = CheckDatas.Rows[i].Cells[14].Value.ToString();
                    updata.unit = CheckDatas.Rows[i].Cells[5].Value.ToString();
                    updata.GetSampTime = CheckDatas.Rows[i].Cells[6].Value.ToString();
                    updata.GetSampPlace = CheckDatas.Rows[i].Cells[7].Value.ToString();
                    updata.intrument = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    updata.Chktestbase = CheckDatas.Rows[i].Cells[9].Value.ToString();
                    //updata.ChklimitData = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    updata.ChkPeople = CheckDatas.Rows[i].Cells[10].Value.ToString();
                    updata.ChkUnit = CheckDatas.Rows[i].Cells[11].Value.ToString();

                    updata.quantityin = CheckDatas.Rows[i].Cells[12].Value.ToString();
                    updata.sampleNum = CheckDatas.Rows[i].Cells[13].Value.ToString();

                    sql.UpdateResult(updata, out err);
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

        private void labelSave_MouseEnter(object sender, EventArgs e)
        {
            labelSave.ForeColor = Color.Red;
        }

        private void labelSave_MouseLeave(object sender, EventArgs e)
        {
            labelSave.ForeColor = Color.White;
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
                if (CheckDatas.CurrentCell.RowIndex > -1 && CheckDatas.CurrentCell.ColumnIndex > 3)
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
        private void CheckDatas_Scroll_1(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
        }
        /// <summary>
        /// 改变列宽下拉框不可见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_ColumnWidthChanged_1(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
        }
    }
}
