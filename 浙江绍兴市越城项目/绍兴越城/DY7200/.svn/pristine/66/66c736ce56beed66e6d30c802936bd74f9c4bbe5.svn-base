using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using System.Collections;
using WorkstationBLL.Mode;
using WorkstationDAL.Basic;
using WorkstationModel.Instrument;
using WorkstationModel.Model;

namespace WorkstationUI.function
{
    public partial class frmSetResult : Form
    {
        private bool m_IsCreatedDataTable = false;
        public DataTable SaveReadTable = null;
        clsSetSqlData sql = new clsSetSqlData();
        public ComboBox cmbAdd = new ComboBox();//下拉列表
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private string[,] Sarr = null;
        private string chkitecode = string.Empty;
        protected decimal _dTestValue = 0;
        private string _sign = string.Empty;
        const int WM_NCHITTEST = 0x0084;
        const int HT_CAPTION = 2;
        public frmSetResult()
        {
            InitializeComponent();
        }       
        private void frmSetResult_Load(object sender, EventArgs e)
        {
            initable();//初始化表对象
            string err = string.Empty;
            //判断是否检测有数据
            if (Global.TableRowNum == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    SaveReadTable.Rows.Add("");
                }
            }
            else
            {
                for (int i = 0; i < Global.TableRowNum; i++)
                {
                    SaveReadTable.Rows.Add("");
                }
            }
            CheckDatas.DataSource = SaveReadTable;
            //将检测数据赋给daagridview
            if (Global.EditorSave.Length > 7)
            {
                for (int i = 0; i < (Global.EditorSave.GetLength(0)); i++)
                {                   
                    CheckDatas.Rows[i].Cells[0].Value = Global.EditorSave[i, 0];
                    CheckDatas.Rows[i].Cells[1].Value = Global.EditorSave[i, 1];
                    CheckDatas.Rows[i].Cells[2].Value = Global.EditorSave[i, 2];
                    CheckDatas.Rows[i].Cells[3].Value = Global.EditorSave[i, 3];
                    CheckDatas.Rows[i].Cells[4].Value = Global.EditorSave[i, 4];
                    CheckDatas.Rows[i].Cells[5].Value = Global.EditorSave[i, 5];
                    CheckDatas.Rows[i].Cells[6].Value = Global.EditorSave[i, 6];
                    //CheckDatas.Rows[i].Cells[14].Value = Global.userlog;
                }
            }
            //else 
            //{
            //    for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //    {
            //        CheckDatas.Rows[i].Cells[14].Value = Global.userlog;
            //    }
            //}
            //查询基本信息表获取 检测单位、地址
            DataTable dtb = sql.GetInformation("iChecked='是'", "", out err);
            if (dtb != null)
            {
                if (dtb.Rows.Count > 0)
                {
                    Global.TestUnitName = dtb.Rows[0][0].ToString();

                    Global.TestUnitAddr = dtb.Rows[0][1].ToString();

                    Global.DetectUnitName = dtb.Rows[0][2].ToString();

                    Global.SampleAddress = dtb.Rows[0][3].ToString();

                    //Global.StockInNum = dtb.Rows[0][4].ToString();

                    //Global.SampleNum = dtb.Rows[0][5].ToString();

                    //Global.GetTime = dtb.Rows[0][7].ToString();

                    //Global.CheckBase = dtb.Rows[0][6].ToString();

                    Global.userlog = dtb.Rows[0][8].ToString();
 
                }
            }

            //cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAdd.Visible = false;
            cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            //cmbAdd.SelectedIndex = 0;
            CheckDatas.Controls.Add(cmbAdd);
            CheckDatas.Columns[6].Width = 160;
            CheckDatas.Columns[9].Width = 160;
            cmbAdd.KeyUp +=new KeyEventHandler(cmbAdd_KeyUp);
            cmbAdd.SelectedValueChanged += new EventHandler(cmbAdd_SelectedValueChanged);
            //不合格字体变红色
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells[5].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells[5].Value.ToString() == "阳性")
                {
                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                CheckDatas.Rows[i].Cells[7].Value = Global.DetectUnitName;//被检单位
                CheckDatas.Rows[i].Cells[10].Value = Global.SampleAddress ;//采样地址
                CheckDatas.Rows[i].Cells[9].Value = Global.GetTime;//采样时间
                CheckDatas.Rows[i].Cells[11].Value = Global.SampleNum;//抽样数量
                CheckDatas.Rows[i].Cells[12].Value = Global.StockInNum;//进货数量
                //CheckDatas.Rows[i].Cells[13].Value = Global.CheckBase;//进货数量
                //CheckDatas.Rows[i].Cells[14].Value = Global.userlog;
            }
            //检测项目
            cmbChkItem.Visible = false;
            cmbChkItem.Items.Add("请选择...");
            cmbChkItem.Items.Add("以下相同");
            cmbChkItem.Items.Add("删除");
            cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChkItem.SelectedIndex = 0;
            cmbChkItem.SelectedIndexChanged += cmbChkItem_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbChkItem);
            //样品名称
            cmbSample.Visible = false;
            cmbSample.Items.Add("请选择...");
            cmbSample.Items.Add("以下相同");
            cmbSample.Items.Add("删除");
            cmbSample.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSample.SelectedIndex = 0;
            CheckDatas.Controls.Add(cmbSample);
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
       
            //if (Global.TestInstrument == "DY-1000农药残留速测仪" || Global.TestInstrument == "DY-2000肉类安全综合分析仪" || Global.TestInstrument == "DY-3000多功能食品综合分析仪"
            //    || Global.TestInstrument == "DY-3100高通量食品安全分析仪" || Global.TestInstrument == "DY-4300三聚氰胺快速分析仪" || Global.TestInstrument == "TL-300多通道农药残留快速测试仪")
            //{
            //    for (int i = 0; i < clsDY3000DY.CheckItemsArray.GetLength(0); i++)
            //    {     
            //        cmbChkItem.Items.Add(clsDY3000DY.CheckItemsArray[i, 0]);
            //        if (clsDY3000DY.CheckItemsArray[i, 0] == CheckDatas.Rows[0].Cells[2].Value.ToString())
            //        {
            //            chkitecode = clsDY3000DY.CheckItemsArray[i, 1];
            //        }
            //    }
            //    for (int i = 0; i < clsDY3000DY.CheckItemsArray.GetLength(0); i++)
            //    {
            //        if (CheckDatas.Rows[0].Cells[2].Value.ToString() == clsDY3000DY.CheckItemsArray[i, 0])
            //        {
            //            DataTable dt = sql.GetSample("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + clsDY3000DY.CheckItemsArray[i, 1] + ":%'", "SysCode", out err);
            //            if (dt != null)
            //            {
            //                if (dt.Rows.Count > 0)
            //                {
            //                    string[,] a = new string[dt.Rows.Count, 2];

            //                    for (int j = 0; j < dt.Rows.Count; j++)
            //                    {
            //                        cmbSample.Items.Add(dt.Rows[j][2].ToString());
            //                        a[j, 0] = dt.Rows[j][2].ToString();//样品名称
            //                        a[j, 1] = dt.Rows[j][1].ToString();//样品编号
            //                    }
            //                    Sarr = a;
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (Global.TestInstrument == "DY-5000食品安全综合分析仪")
            //{
 
            //}
        }
        /// <summary>
        /// 检测项目选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChkItem.Text == "以下相同")
            {             
                cmbChkItem.Visible = false;
                for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                {
                    CheckDatas.Rows[k].Cells[2].Value = CheckDatas.CurrentCell.Value.ToString();
                }               
            }
            else if (cmbChkItem.Text == "删除")
            {
               
                CheckDatas.CurrentCell.Value = "";
                cmbChkItem.Visible = false;
            }
            else 
            {              
                CheckDatas.CurrentCell.Value = cmbChkItem.Text;
                cmbChkItem.Visible = false;
            }
        }
        /// <summary>
        /// 根据样品名称判定检测结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LZ2000读回数据有结果无需判断
            if (Global.ChkManchine == "LZ-2000农药残留快检仪")
            {
                if (cmbSample.Text == "以下相同")
                {
                    for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                    {
                        CheckDatas.Rows[k].Cells[8].Value = CheckDatas.CurrentCell.Value.ToString();
                    }
                    cmbSample.Visible = false;
                }
                else if (cmbSample.Text == "删除")
                {
                    CheckDatas.CurrentCell.Value = "";
                    cmbSample.Visible = false;
                }
                else
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;
                    cmbSample.Visible = false;
                }
            }
            else  //非LZ2000
            {
                if (cmbSample.Text == "以下相同")
                {
                    for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                    {
                        CheckDatas.Rows[k].Cells[8].Value = CheckDatas.CurrentCell.Value.ToString();
                        //查找检测项目编号
                        for (int d = 0; d < clsDY3000DY.CheckItemsArray.GetLength(1); d++)
                        {
                            if (clsDY3000DY.CheckItemsArray[d, 0] == CheckDatas.Rows[k].Cells[2].Value.ToString())
                            {
                                chkitecode = clsDY3000DY.CheckItemsArray[d, 1];
                            }
                        }
                        try
                        {
                            string[] rtn = new string[3];
                            rtn[0] = "";
                            rtn[1] = "0";
                            rtn[2] = "";
                            bool IsExist = false;
                            string foodCode = string.Empty;
                            for (int j = 0; j < Sarr.GetLength(0); j++)
                            {
                                if (CheckDatas.Rows[k].Cells[8].Value.ToString() == Sarr[j, 0])
                                {
                                    foodCode = Sarr[j, 1];
                                }
                            }
                            string errMsg = string.Empty;
                            string sql = "SELECT CheckItemCodes FROM CheckSample WHERE syscode='" + foodCode + "'";
                            Object o = DataBase.GetOneValue(sql, out errMsg);
                            if (o == null)
                            {
                                return;
                            }
                            else
                            {
                                string[,] result = clsStringUtil.GetFoodAry(o.ToString());
                                if (result.GetLength(0) >= 1)
                                {
                                    for (int j = 0; j < result.GetLength(0); j++)
                                    {
                                        int d = result.GetLength(0);
                                        if (result[j, 0].Equals(chkitecode))
                                        {
                                            IsExist = true;
                                            if (result[j, 1].Equals("-1"))
                                            {
                                                sql = "SELECT DemarcateInfo FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                                o = DataBase.GetOneValue(sql, out errMsg);
                                                if (o == null)
                                                {
                                                    rtn[0] = "";
                                                }
                                                else
                                                {
                                                    rtn[0] = o.ToString();
                                                }
                                                sql = "SELECT StandardValue FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                                o = DataBase.GetOneValue(sql, out errMsg);
                                                if (o == null)
                                                {
                                                    rtn[1] = "";
                                                }
                                                else
                                                {
                                                    rtn[1] = o.ToString();
                                                }
                                                sql = "SELECT Unit FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                                o = DataBase.GetOneValue(sql, out errMsg);
                                                if (o == null)
                                                {
                                                    rtn[2] = "";
                                                }
                                                else
                                                {
                                                    rtn[2] = o.ToString();
                                                }
                                            }
                                            else
                                            {
                                                rtn[0] = result[j, 1];
                                                rtn[1] = result[j, 2];
                                                rtn[2] = result[j, 3];
                                            }
                                            break;
                                        }
                                        if (!IsExist)
                                        {
                                            rtn[0] = "-1";
                                            rtn[1] = "0";
                                            rtn[2] = "-1";
                                        }
                                    }
                                }
                            }
                            _dTestValue = Convert.ToDecimal(rtn[1]);
                            _sign = rtn[0];
                            if (CheckDatas.Rows[k].Cells[2].Value.ToString() != string.Empty)
                            {
                                checkValueState(_dTestValue, k);//自动关联合格或不合格
                            }
                            cmbSample.Visible = false;
                            //查询检测依据
                            string Ssql = "SELECT ItemDes FROM tStandSample WHERE FtypeNmae='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value.ToString() + "'";
                            Object os = DataBase.GetOneValue(Ssql, out errMsg);
                            if (os != null)
                            {
                                CheckDatas.Rows[k].Cells[11].Value = os.ToString();
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
                else if (cmbSample.Text == "删除")
                {
                    CheckDatas.CurrentCell.Value = "";
                    cmbSample.Visible = false;
                }
                else
                {
                    try
                    {
                        CheckDatas.CurrentCell.Value = cmbSample.Text;
                        string[] rtn = new string[3];
                        rtn[0] = "";
                        rtn[1] = "0";
                        rtn[2] = "";
                        bool IsExist = false;
                        string foodCode = string.Empty;
                        for (int i = 0; i < Sarr.GetLength(0); i++)
                        {
                            if (CheckDatas.CurrentCell.Value.ToString() == Sarr[i, 0])
                            {
                                foodCode = Sarr[i, 1];
                            }
                        }
                        string errMsg = string.Empty;
                        string sql = "SELECT CheckItemCodes FROM CheckSample WHERE syscode='" + foodCode + "'";
                        Object o = DataBase.GetOneValue(sql, out errMsg);
                        if (o == null)
                        {
                            return;
                        }
                        else
                        {
                            string[,] result = clsStringUtil.GetFoodAry(o.ToString());
                            if (result.GetLength(0) >= 1)
                            {
                                for (int i = 0; i < result.GetLength(0); i++)
                                {
                                    int d = result.GetLength(0);
                                    if (result[i, 0].Equals(chkitecode))
                                    {
                                        IsExist = true;
                                        if (result[i, 1].Equals("-1"))
                                        {
                                            sql = "SELECT DemarcateInfo FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                            o = DataBase.GetOneValue(sql, out errMsg);
                                            if (o == null)
                                            {
                                                rtn[0] = "";
                                            }
                                            else
                                            {
                                                rtn[0] = o.ToString();
                                            }
                                            sql = "SELECT StandardValue FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                            o = DataBase.GetOneValue(sql, out errMsg);
                                            if (o == null)
                                            {
                                                rtn[1] = "";
                                            }
                                            else
                                            {
                                                rtn[1] = o.ToString();
                                            }
                                            sql = "SELECT Unit FROM tCheckItem WHERE SysCode='" + chkitecode + "'";
                                            o = DataBase.GetOneValue(sql, out errMsg);
                                            if (o == null)
                                            {
                                                rtn[2] = "";
                                            }
                                            else
                                            {
                                                rtn[2] = o.ToString();
                                            }
                                        }
                                        else
                                        {
                                            rtn[0] = result[i, 1];
                                            rtn[1] = result[i, 2];
                                            rtn[2] = result[i, 3];
                                        }
                                        break;
                                    }
                                    if (!IsExist)
                                    {
                                        rtn[0] = "-1";
                                        rtn[1] = "0";
                                        rtn[2] = "-1";
                                    }
                                }
                            }
                        }
                        _dTestValue = Convert.ToDecimal(rtn[1]);
                        _sign = rtn[0];
                        if (CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() != string.Empty)
                        {
                            checkValueState(_dTestValue, CheckDatas.CurrentCell.RowIndex);//自动关联合格或不合格
                        }
                        cmbSample.Visible = false;
                        //查询检测依据
                        string Ssql = "SELECT ItemDes FROM tStandSample WHERE FtypeNmae='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value.ToString () + "'";
                        Object os = DataBase.GetOneValue(Ssql, out errMsg);
                        if (os != null)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[11].Value = os.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }
        /// <summary>
        /// 判断检测值状态
        /// </summary>
        /// <param name="testValue"></param>
        private void checkValueState(decimal testValue,int r)
        {
            if (!CheckDatas.Rows[r].Cells[3].Value.ToString().Trim().EndsWith("性"))
            {
                decimal checkValue = Decimal.Parse(CheckDatas.Rows[r].Cells[3].Value.ToString().Trim());
                switch (_sign)
                {
                    case "<":
                        CheckDatas.Rows[r].Cells[5].Value = checkValue >= testValue ? "不合格" : "合格";
                        break;
                    case "≤":
                        CheckDatas.Rows[r].Cells[5].Value = checkValue > testValue ? "不合格" : "合格";
                        break;
                    case ">":
                        CheckDatas.Rows[r].Cells[5].Value = checkValue <= testValue ? "不合格" : "合格";
                        break;
                    case "≥":
                        CheckDatas.Rows[r].Cells[5].Value = checkValue < testValue ? "不合格" : "合格";
                        break;
                }              
            }
        }
        void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {          
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;
        }
       
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
        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }
        /// <summary>
        /// 选择单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "输入")
            {
                Global.newdata = "保存编辑数据";
                frminputdata input = new frminputdata();
                input.Redata = this;
                input.Show();
                cmbAdd.Visible = false;
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

        private void labelClose_Click(object sender, EventArgs e)
        {
            //Global.CheckedUnit = txtChkUnit.Text;
            //Global.getsampletime = DTPsampletime.Value.ToString();
            //Global.getsampleaddress = txtSampleAddre.Text;
            //Global.plannum = txtPlanNum.Text;
            //Global.testbase = cmbTestbase.Text;
            //Global.limitdata = txtLimitData.Text;
            //Global.tester = txtTester.Text;
            //Global.AddItem = addsavedata();

            this.Close();
        }
        /// <summary>
        /// 添加数据保存到二维数组
        /// </summary>
        /// <returns></returns>
        private string[,] addsavedata()
        {
            string[,] strr = new string[CheckDatas.Rows.Count, 7];
            for (int i=0; i<CheckDatas.Rows.Count; i++)
            {
                strr[i, 0] =CheckDatas.Rows[i].Cells[0].Value.ToString ();
                strr[i, 1] =CheckDatas.Rows[i].Cells[1].Value.ToString ();
                strr[i, 2] =CheckDatas.Rows[i].Cells[2].Value.ToString ();
                strr[i, 3] =CheckDatas.Rows[i].Cells[3].Value.ToString ();
                strr[i, 4] =CheckDatas.Rows[i].Cells[4].Value.ToString ();
                strr[i, 5] =CheckDatas.Rows[i].Cells[5].Value.ToString ();
                strr[i, 6] = CheckDatas.Rows[i].Cells[6].Value.ToString(); 
            }
            return strr;
        }
        private void initable()
        {
            if (!m_IsCreatedDataTable)
            {
                SaveReadTable = new DataTable("checkDtbl");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测编号";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";//检测值
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "判定结果";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "采样时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地址";
                SaveReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "抽样数量";
                //SaveReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "进货数量";
                //SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";//检测值
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测人员";
                SaveReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 确定保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelOK_Click(object sender, EventArgs e)
        {
            Global.AddItem = addsavedata();
            clsSaveResult resultdata = new clsSaveResult();
            int isave = 0;
            string chk = "";
            string err = string.Empty;
            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells[0].Value.ToString() != "是" && CheckDatas.Rows[i].Cells[6].Value.ToString()!="")
                        {
                            resultdata.Save = "是";
                            resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString();
                            resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString();
                            resultdata.Result = CheckDatas.Rows[i].Cells[5].Value.ToString();
                            chk = CheckDatas.Rows[i].Cells[6].Value.ToString().Replace("-","/");
                            resultdata.CheckTime = DateTime.Parse(chk);
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells[7].Value.ToString();
                            resultdata.SampleName = CheckDatas.Rows[i].Cells[8].Value.ToString();
                            resultdata.Gettime = CheckDatas.Rows[i].Cells[9].Value.ToString();
                            resultdata.Getplace = CheckDatas.Rows[i].Cells[10].Value.ToString();
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells[11].Value.ToString();
                            resultdata.quantityin = CheckDatas.Rows[i].Cells[12].Value.ToString();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells[13].Value.ToString();
                            resultdata.Tester = CheckDatas.Rows[i].Cells[14].Value.ToString();
                            sql.ResuInsert(resultdata, out err);
                            isave = isave + 1;
                        }                       
                    }
                    if (isave == 0)
                    {
                        MessageBox.Show("数据已成功保存过！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("数据保存成功，共保存" + isave + "条数据！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
            }
            this.Close();
        }

        private void labelOK_MouseEnter(object sender, EventArgs e)
        {
            labelOK.ForeColor = Color.Red;
        }

        private void labelOK_MouseLeave(object sender, EventArgs e)
        {
            labelOK.ForeColor = Color.White;
        }

        private void CheckDatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //{
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "被检单位")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString() ;
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "采样时间")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //    if(CheckDatas.Columns[e.ColumnIndex].HeaderText=="检测人员")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[i].Cells[10].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "采样地址")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[0].Cells[e.ColumnIndex].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "抽样数量")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[0].Cells[e.ColumnIndex].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "进货数量")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[0].Cells[e.ColumnIndex].Value.ToString();
            //        }
            //    }
            //    if (CheckDatas.Columns[e.ColumnIndex].HeaderText == "检测依据")
            //    {
            //        for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //        {
            //            CheckDatas.Rows[i].Cells[e.ColumnIndex].Value = CheckDatas.Rows[0].Cells[e.ColumnIndex].Value.ToString();
            //        }
            //    }
            //}
        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //frminputdata frmin = new frminputdata();
            //frmin.Redata = this;
            //frmin.Show();
           
            //combox控件的位置
            if (CheckDatas.CurrentCell.ColumnIndex < 1)
            {
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex>-1 && CheckDatas.CurrentCell.RowIndex >-1)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 2 )
                    {
                        Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkItem.Left = irect.Left;
                        cmbChkItem.Top = irect.Top;
                        cmbChkItem.Width = irect.Width;
                        cmbChkItem.Height = irect.Height;
                        cmbChkItem.Visible = true;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 8)
                    {
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbSample.Visible = true;
                    }
                    else
                    {
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbAdd.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 滚动隐藏combox控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
        }
        /// <summary>
        /// 改变宽度隐藏combox控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
        }
    }
}
