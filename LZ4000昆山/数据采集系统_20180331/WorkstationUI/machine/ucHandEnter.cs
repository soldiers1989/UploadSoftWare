using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using System.Configuration;
using System.Web.Script.Serialization;
using WorkstationModel.UpData;

namespace WorkstationUI.machine
{
    public partial class ucHandEnter : UserControl
    {
        public bool HandInput = false;
        private bool m_IsCreatedDataTable = false;
        private DataTable DataReadTable = null;
        private clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private clsUpLoadData udata = new clsUpLoadData();
        private ComboBox cmbAdd = new ComboBox();
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];
        public ucHandEnter()
        {
            InitializeComponent();
        }
        private void ucHandEnter_Load(object sender, EventArgs e)
        {
            Initable();

            string err = "";
            cdt = sqlSet.GetInformation("", "", out err);
            if (cdt != null)
            {
                if (cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n][9].ToString() == "是")
                        {
                            unitInfo[0, 0] = cdt.Rows[n][2].ToString();
                            unitInfo[0, 1] = cdt.Rows[n][3].ToString();
                            unitInfo[0, 2] = cdt.Rows[n][8].ToString();
                            unitInfo[0, 3] = cdt.Rows[n][0].ToString();//检测单位
                        }
                    }
                }
            }
          
            btnUpdata.Visible = false;
            if (HandInput == false)
            {
                labelTile.Text = "其他手工录入";
                AddNewHistoricData("", "", "", "", "", "","");
            }
            else
            {
                labelTile.Text =Global.ChkManchine+"手工录入";
                AddNewHistoricData("", "", "", "", "", "", Global.ChkManchine);
            }
            CheckDatas.DataSource = DataReadTable;

            cmbAdd.Visible = false;
            //cmbAdd.Items.Add("请选择...");
            //cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.KeyUp += cmbAdd_KeyUp;
            cmbAdd.SelectedIndexChanged += cmbAdd_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbAdd);
           
        }
        private void Initable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text;
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbChkItem.Visible = false;
            //cmbSample.Visible = false;
            if (((ComboBox)sender).Text == "删除")
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
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbAdd.Text = "";
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {

            if (HandInput == false)
            {             
                AddNewHistoricData("", "", "", "", "", "", "");
            }
            else
            {               
                AddNewHistoricData("", "", "", "", "", "", Global.ChkManchine);
            }           
            CheckDatas.DataSource = DataReadTable;
           
        }
        private void AddNewHistoricData(string num, string item, string checkValue, string unit, string time, string p ,string machine)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["已保存"] = "否";
            dr["样品名称"] = "";
            dr["检测项目"] = item;
            dr["检测结果"] = checkValue;
            dr["单位"] = unit;//检测值
            dr["检测依据"] = "";
            dr["标准值"] = "";
            dr["检测仪器"] = machine;
            dr["结论"] = "";
            dr["检测单位"] = unitInfo[0, 3];
            dr["采样时间"] = System.DateTime.Now.ToString();
            dr["采样地点"] = unitInfo[0, 1];
            dr["被检单位"] = unitInfo[0, 0];
            dr["检测员"] = unitInfo[0, 2];
            dr["检测时间"] = System.DateTime.Now.ToString();
            dr["检测数量"] = "";
            dr["样品种类"] = "";

            DataReadTable.Rows.Add(dr);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckDatas.SelectedRows.Count< 1)
            {
                MessageBox.Show("请选择需要删除的记录","提示");
                return;
            }
            DialogResult dr= MessageBox.Show("是否删除选中的记录","提示",MessageBoxButtons.YesNoCancel ,MessageBoxIcon.Information );
            if (dr == DialogResult.Yes)
            {
                for (int i = 0; i < CheckDatas.SelectedRows.Count; i++)
                {
                    DataRowView drv = CheckDatas.SelectedRows[i].DataBoundItem as DataRowView;
                    drv.Delete();
                }
            }
            else 
            {
                return;
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDatsave_Click(object sender, EventArgs e)
        {
            int isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells[0].Value.ToString() != "是")
                        {
                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.SampleName = CheckDatas.Rows[i].Cells[1].Value.ToString().Trim();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
                            resultdata.detectunit = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells[10].Value.ToString().Trim();//采样时间
                            resultdata.Getplace = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();
                            chk = CheckDatas.Rows[i].Cells[14].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = DateTime.Parse(chk);
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells[15].Value.ToString();//检测样品数量
                            resultdata.SampleType = CheckDatas.Rows[i].Cells[16].Value.ToString();//样品种类

                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if (iok == 1)
                            {
                                isave = isave + 1;
                                CheckDatas.Rows[i].Cells[0].Value = "是";
                            }
                        }
                    }
                    if (isave == 0)
                    {
                        MessageBox.Show("数据已成功保存过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("数据保存成功，共保存" + isave + "条数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdata_Click(object sender, EventArgs e)
        {
            if (CheckDatas.Rows.Count < 1)
            {
                MessageBox.Show("没有检测数据上传", "提示");
                return;
            }

            if (Global.ServerAdd.Length == 0)
            {
                MessageBox.Show("服务器地址不能为空", "提示");
                return;
            }
            if (Global.DetectUnit.Length == 0)
            {
                MessageBox.Show("检测单位不能为空", "提示");
                return;
            }

            DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (tishi == DialogResult.No)
            {
                return;
            }
            btnUpdata.Enabled = false;
            Global.ServerAdd = ConfigurationManager.AppSettings["ServerAddr"];//服务器地址
            Global.DetectUnit = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位
            Global.DetectUnitNo = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            Global.UploadType = ConfigurationManager.AppSettings["UploadType"];//上传类型
            Global.IntrumentNum = ConfigurationManager.AppSettings["IntrumentNum"];//设备ID

            string err = "";
            int iok = 0;
            int ino = 0;
            string ErrInfo = "";
            try
            {
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    //边查询边上传
                    StringBuilder sbd = new StringBuilder();

                    sbd.Append("Checkitem='");
                    sbd.Append(CheckDatas.Rows[i].Cells[2].Value.ToString());
                    sbd.Append("' and SampleName='");
                    sbd.Append(CheckDatas.Rows[i].Cells[1].Value.ToString());
                    sbd.Append("' and CheckData='");
                    sbd.Append(CheckDatas.Rows[i].Cells[3].Value.ToString());
                    sbd.Append("' and CheckTime=#");
                    sbd.Append(DateTime.Parse(CheckDatas.Rows[i].Cells[14].Value.ToString()));
                    sbd.Append("#");
                    //查询界面上没有的数据信息
                    DataTable rt = sqlSet.GetDataTable(sbd.ToString(), "");
                    if (rt != null && rt.Rows.Count > 0)
                    {
                        udata.detectunit = CheckDatas.SelectedRows[i].Cells[9].Value.ToString();//被检单位
                        udata.checkunit = CheckDatas.SelectedRows[i].Cells[11].Value.ToString();//被检单位
                        udata.checkitem = CheckDatas.SelectedRows[i].Cells[1].Value.ToString();//检测项目
                        udata.ttime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();//检测时间
                        udata.chkdata = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();//检测数值
                        udata.unit = System.Web.HttpUtility.UrlEncode(rt.Rows[0][4].ToString()); //System.Web.HttpUtility.UrlEncode("%");//数值单位 对非法字符进行url转换

                        if (CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "阴性" || CheckDatas.SelectedRows[i].Cells[7].Value.ToString() == "合格")
                        {
                            udata.Conclusion = "合格";// 检测结论
                        }
                        else
                        {
                            udata.Conclusion = "不合格";
                        }
                        udata.samplenumber = rt.Rows[0][18].ToString();//样品编号
                        udata.samplename = CheckDatas.SelectedRows[i].Cells[0].Value.ToString();//样品名称
                        udata.sampleOrigin = CheckDatas.SelectedRows[i].Cells[10].Value.ToString();//样品产地
                        udata.testbase = rt.Rows[0][12].ToString();//检测依据
                        udata.ChkMachineNum = rt.Rows[0][20].ToString();//设备编号
                        udata.standvalue = rt.Rows[0][13].ToString();//标准值
                        udata.chker = CheckDatas.SelectedRows[i].Cells[12].Value.ToString();
                        udata.uptype = int.Parse(Global.UploadType);
                        udata.shebeiID = Global.IntrumentNum;

                        string rd = KeRunUpData.UpData(udata);

                        JavaScriptSerializer jsup = new JavaScriptSerializer();
                        KeRunUpData.ReturnInfo retu = jsup.Deserialize<KeRunUpData.ReturnInfo>(rd); //将json数据转化为对象类型并赋值给list

                        if (retu.status == "1" && retu.info == "success")
                        {
                            CheckDatas.SelectedRows[i].DefaultCellStyle.BackColor = Color.Green;//上传成功变绿色
                            iok = iok + 1;
                            clsUpdateData ud = new clsUpdateData();
                            ud.result = CheckDatas.SelectedRows[i].Cells[2].Value.ToString();
                            ud.ChkTime = CheckDatas.SelectedRows[i].Cells[13].Value.ToString();
                            sqlSet.SetUpLoadData(ud, out err);
                        }
                        else
                        {
                            ino = ino + 1;
                            ErrInfo = ErrInfo + "(" + ino + ")" + retu.info;
                        }
                    }
                }
                MessageBox.Show("共成功上传" + iok + "条数据；失败" + ino + "条数据,失败原因：" + ErrInfo, "数据上传");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            btnUpdata.Enabled = true;
        }
        /// <summary>
        /// DataGridView的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CheckDatas.CurrentCell.ColumnIndex < 1)
            {
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                //cmbSample.Visible = false;
                //cmbChkItem.Visible = false;
                cmbAdd.Visible = false;
                return;
            }
            cmbAdd.Visible = true;
            cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
            Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
            cmbAdd.Left = rect.Left;
            cmbAdd.Top = rect.Top;
            cmbAdd.Width = rect.Width;
            cmbAdd.Height = rect.Height;
        }

        private void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
        }

        private void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
        }
    }
}
