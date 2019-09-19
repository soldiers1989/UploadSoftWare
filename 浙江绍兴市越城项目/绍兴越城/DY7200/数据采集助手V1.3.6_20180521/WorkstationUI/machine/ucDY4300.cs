using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WorkstationUI.Basic;
using WorkstationModel.Instrument;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationModel.Model;
using WorkstationDAL.Model;

namespace WorkstationUI.machine
{
    public partial class ucDY4300 : BasicContent 
    {
        public ucDY4300()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DY5000LD系列
        /// </summary>
        private DataTable checkDtbl = null;
        private bool IsCreatDT = false;
        private readonly HolesSetingOpr holesBll = new HolesSetingOpr();
        private StringBuilder strWhere = new StringBuilder();
        protected string _machineCode = string.Empty;
        /// <summary>
        /// 接收仪器传输的数据
        /// </summary>
        private StringBuilder sbData = new StringBuilder();
        private Hashtable htbl = new Hashtable();
        private DataTable holeDtbl = null;
        private DataTable dtTemp = null;
        private bool isSaved = false;//标识已经保存
        private int secondCharIndex = 0;
        private int holeLen = 0;
        private string th1 = string.Empty;//第一个孔字符如:A1
        private string th2 = string.Empty;//第二个孔字符如:A2
        private string tempField = string.Empty;
        private string tempHole = string.Empty;
        private string dataheadString = string.Empty;
        //"clsDY5000LD"注意要加"LD"表示雷度版本     
        private clsDY5000LD dy5000ld = new clsDY5000LD();
        protected string[,] _checkItems;
        private clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        protected readonly clsResultOpr _resultBll = new clsResultOpr();
        private bool m_IsCreatedDataTable = false;
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private ComboBox cmbAdd = new ComboBox();//其他信息录入
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            bool blOnline = false;
            try
            {
                blOnline = dy5000ld.Online;
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message + "，无法与仪器正常通讯，请重启界面！");
                return;
            }

            if (!blOnline)
            {
                MessageBox.Show(this, "串口连接有误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                return;
            }
            if (blOnline)
            {
                Cursor = Cursors.WaitCursor;
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;
                DTPEnd.Value = DTPStart.Value.Date.AddDays(1).AddSeconds(-1);
                dy5000ld.ReadHistory(DTPStart.Value.Date, DTPEnd.Value);
            }
        }

        private void ucDY4300_Load(object sender, EventArgs e)
        {
            string itemcode = string.Empty;
            string err = string.Empty;
            tableinit();
            //其他信息录入
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.Visible = false;
            cmbAdd.SelectedIndexChanged += new EventHandler(cmbAdd_SelectedIndexChanged);
            //cmbAdd.MouseDoubleClick += cmbAdd_MouseDoubleClick;//无法实现           
            cmbAdd.MouseClick += cmbAdd_MouseClick;
            cmbAdd.KeyUp += new KeyEventHandler(cmbAdd_KeyUp);
            CheckDatas.Controls.Add(cmbAdd);
            clsMessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
            //检测单位
            cmbChkUnit.Items.Add("以下相同");
            cmbChkUnit.Items.Add("删除");
            cmbChkUnit.Visible = false;
            cmbChkUnit.SelectedIndexChanged += cmbChkUnit_SelectedIndexChanged;
            //cmbChkUnit.MouseClick += cmbChkUnit_MouseClick;
            cmbChkUnit.KeyUp += cmbChkUnit_KeyUp;
            CheckDatas.Controls.Add(cmbChkUnit);
            //被检单位              
            cmbDetectUnit.Items.Add("以下相同");
            cmbDetectUnit.Items.Add("删除");
            cmbDetectUnit.Visible = false;
            cmbDetectUnit.SelectedIndexChanged += cmbDetectUnit_SelectedIndexChanged;
            //cmbDetectUnit.MouseClick += cmbDetectUnit_MouseClick;
            cmbDetectUnit.KeyUp += cmbDetectUnit_KeyUp;
            CheckDatas.Controls.Add(cmbDetectUnit);
            //采样地址
            cmbGetSampleAddr.Items.Add("以下相同");
            cmbGetSampleAddr.Items.Add("删除");
            cmbGetSampleAddr.Visible = false;
            cmbGetSampleAddr.SelectedIndexChanged += cmbGetSampleAddr_SelectedIndexChanged;
            //cmbGetSampleAddr.MouseClick += cmbGetSampleAddr_MouseClick;
            cmbGetSampleAddr.KeyUp += cmbGetSampleAddr_KeyUp;
            CheckDatas.Controls.Add(cmbGetSampleAddr);
            //检测员
            cmbChker.Items.Add("以下相同");
            cmbChker.Items.Add("删除");
            cmbChker.Visible = false;
            cmbChker.MouseClick += cmbChker_MouseClick;
            cmbChker.SelectedIndexChanged += cmbChker_SelectedIndexChanged;
            cmbChker.KeyUp += cmbChker_KeyUp;
            CheckDatas.Controls.Add(cmbChker);
            try
            {
                strWhere.Clear();
                strWhere.Append("WHERE Name='");
                strWhere.Append("DY-3000多功能食品综合分析仪");
                strWhere.Append("'");
                DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        itemcode = dt.Rows[0][4].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            _checkItems = clsStringUtil.GetDY3000DYAry(itemcode);
            clsDY3000DY.CheckItemsArray = _checkItems;

            try
            {
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null)
                {
                    if (cdt.Rows.Count > 0)
                    {
                        for (int n = 0; n < cdt.Rows.Count; n++)
                        {
                            if (cdt.Rows[n][9].ToString() == "是")
                            {
                                unitInfo[0,0] = cdt.Rows[n][0].ToString();//检测单位
                                unitInfo[0,1] = cdt.Rows[n][2].ToString();
                                unitInfo[0,2] = cdt.Rows[n][3].ToString();
                                unitInfo[0,3] = cdt.Rows[n][8].ToString();
                            }
                            cmbChkUnit.Items.Add(cdt.Rows[n][0].ToString());//检测单位
                            cmbDetectUnit.Items.Add(cdt.Rows[n][2].ToString());//被检单位
                            cmbGetSampleAddr.Items.Add(cdt.Rows[n][3].ToString());//采样地址
                            cmbChker.Items.Add(cdt.Rows[n][8].ToString());//检测员
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        //首先加载进获取系统PAI函数的引用：
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();//重写系统API函数获取鼠标双击的有效间隔
        private DateTime dtCmbLastClick = DateTime.MinValue;//存储两次单击的时间间隔
        private void cmbAdd_MouseClick(object sender, MouseEventArgs e)
        {
            //if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            //{
            //    // 双击事件处理方式
            //    frmChkUnitInfo fchk = new frmChkUnitInfo();
            //    //fchk.lz4000 = this;
            //    DialogResult dr = fchk.ShowDialog();

            //    if (dr == DialogResult.OK)
            //    {
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[11].Value = Global.ChkInfo[0, 0];
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[10].Value = Global.ChkInfo[0, 1];
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[12].Value = Global.ChkInfo[0, 2];
            //        cmbAdd.Visible = false;
            //    }
            //}
            //else
            //{
            //    dtCmbLastClick = DateTime.Now;
            //}      
        }

        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        //检测员按键弹起事件
        private void cmbChker_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChker.Text;
        }
        //检测员选择事件
        private void cmbChker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChker.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChker.Visible = false;
            }
        }
        //检测员单击事件
        private void cmbChker_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //采样地址选择事件
        private void cmbGetSampleAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbGetSampleAddr.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbGetSampleAddr.Visible = false;
            }
        }
        //采样地址弹起事件
        private void cmbGetSampleAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbGetSampleAddr.Text;
        }
        //采样地址单击事件
        private void cmbGetSampleAddr_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //被检单位键盘弹起事件
        private void cmbDetectUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbDetectUnit.Text.Trim(); ;
        }
        //被检单位单击事件
        private void cmbDetectUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //被检单位选择事件
        private void cmbDetectUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbDetectUnit.Text = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbDetectUnit.Visible = false;
            }
        }
        //检测单位按键弹起事件
        private void cmbChkUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChkUnit.Text.Trim();
        }
        //检测单位单击事件
        private void cmbChkUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //检测单位选择事件
        private void cmbChkUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChkUnit.Text = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChkUnit.Visible = false;
            }
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            if (btnlinkcom.Text == "连接设备")
            {
                try
                {
                    if (!dy5000ld.Online)
                    {
                        dy5000ld.Open();
                        txtlink.Text = "已连接设备";
                        btnlinkcom.Text = "断开设备";
                        //MessageBox.Show("连接设备成功","提示");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (btnlinkcom.Text == "断开设备")
            {
                dy5000ld.Close();
                txtlink.Text = "未连接";
                btnlinkcom.Text = "连接设备";
            }
        }

        private void tableinit()
        {
            if (!m_IsCreatedDataTable)
            {
                checkDtbl = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "孔位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType =typeof(string);
                dataCol.ColumnName = "abs";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
            }
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, clsMessageNotify.NotifyEventArgs e)
        {
            if (e.Code == clsMessageNotify.NotifyInfo.Read3000LDData)
            {
                ShowResult(e.Message);
            }
        }

        /// <summary>
        /// 委托回调
        /// </summary>
        /// <param name="s"></param>
        private delegate void InvokeDelegate(string input);

        /// <summary>
        /// 设置接收数据
        /// </summary>
        /// <param name="code"></param>
        private void ShowResult(string code)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new InvokeDelegate(ShowOnControl), code);
            }
            else
            {
                ShowOnControl(code);
            }
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="input"></param>
        private void ShowOnControl(string input)
        {
            Cursor = Cursors.WaitCursor;
            sbData.Append(input);
            if (input.Equals("48"))
            {
                string data = sbData.ToString();
                if (data.Length <= 30)
                {
                    sbData.Length = 0;
                    Cursor = Cursors.Default;
                    MessageBox.Show("没有相应的检测数据。");

                    return;
                }
                if (data.Length > 30)
                {
                    int ilen = data.Length;
                    if (data.Substring(ilen - 30, 30).Equals("5452414E534645522046494E495348"))
                    {
                        string t1 = data.Substring(0, ilen - 30);
                        if (t1.Length < 4)
                        {
                            sbData.Length = 0;
                            Cursor = Cursors.Default;
                            MessageBox.Show("没有相应的检测数据。");
                            return;
                        }
                        int ihead = -2;
                        int iend = -1;
                        int icount = 0;
                        bool Isflag = false;
                        while (true)
                        {
                            iend = t1.IndexOf(dataheadString, ihead + 2, t1.Length - ihead - 2);
                            if (iend < 0)
                            {
                                break;
                            }
                            icount++;
                            if (iend - ihead > 4 && icount > 1)
                            {
                                if (Isflag)
                                {
                                    doWith(t1.Substring(ihead - 4, iend - ihead + 4));
                                    Isflag = false;
                                }
                                else
                                {
                                    doWith(t1.Substring(ihead, iend - ihead));
                                }
                            }
                            else
                            {
                                if (iend - ihead == 4 && ihead >= 0)
                                {
                                    Isflag = true;
                                }
                            }
                            ihead = iend;
                        }
                        if (Isflag)
                        {
                            doWith(t1.Substring(ihead - 4, t1.Length - ihead + 4));
                            Isflag = false;
                        }
                        else
                        {
                            doWith(t1.Substring(ihead, t1.Length - ihead));
                        }
                        sbData.Length = 0;
                        //IsSavedRecord();
                        CheckDatas.DataSource = checkDtbl.DefaultView ;
                        CheckDatas.Columns[13].Width = 180;
                        CheckDatas.Columns[2].Width = 150;
                        CheckDatas.Columns[9].Width = 150;
                        CheckDatas.Columns[7].Width = 180;
                        CheckDatas.Columns[14].Width = 180;
                        //判断数据是否保存过
                        if (CheckDatas.Rows.Count > 1)
                        {
                            for (int i = 0; i < CheckDatas.Rows.Count; i++)
                            {
                                strWhere.Length = 0;
                                strWhere.AppendFormat(" CheckData='{0}'", CheckDatas.Rows[i].Cells[3].Value.ToString());
                                strWhere.AppendFormat(" AND Checkitem='{0}'", CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());
                                strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/")));
                                CheckDatas.Rows[i].Cells[0].Value = (sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否");
                                //自动添加数据到表
                                CheckDatas.Rows[i].Cells[7].Value = "LZ-3000农药残留快速测试仪";//Global.TestInstrument;
                                CheckDatas.Rows[i].Cells[12].Value = cdt.Rows[0][8].ToString();
                                CheckDatas.Rows[i].Cells[10].Value = cdt.Rows[0][3].ToString();
                                CheckDatas.Rows[i].Cells[11].Value = cdt.Rows[0][2].ToString();
                            }
                        }       

                    }
                }
            }
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// 判断某一个记录是否已经保存过了，是的话把“已保存”字段设置为true否为false
        /// </summary>
        private void IsSavedRecord()
        {
            dtTemp.Rows.Clear();
            for (int i = 0; i < checkDtbl.Rows.Count; i++)
            {
                secondCharIndex = 0;
                strWhere.AppendFormat(" MachineCode='{0}' AND IsShowOnData=False ", _machineCode);

                tempField = checkDtbl.Rows[i]["项目"].ToString();
                strWhere.AppendFormat(" AND CheckItemId='{0}'", tempField);

                tempHole = checkDtbl.Rows[i]["孔位"].ToString().ToUpper().Trim();

                holeLen = tempHole.Length;
                if (holeLen > 3)//如果存在双孔的情况
                {
                    secondCharIndex = getSecondCharIndex(tempHole.ToCharArray());
                    th1 = tempHole.Substring(0, secondCharIndex);
                    th2 = tempHole.Substring(secondCharIndex, holeLen - secondCharIndex);
                    strWhere.AppendFormat(" AND (HolesIndex='{0}' OR HolesIndex='{1}') ", th1, th2);
                }
                else
                {
                    strWhere.AppendFormat(" AND HolesIndex='{0}' ", tempHole);
                    //if (htbl[tempHole] != null && htbl[tempHole].ToString() == "1")
                    //{
                    //    continue;
                    //}
                }
                if (!holesBll.IsExist(strWhere.ToString()))
                {
                    dtTemp.ImportRow(checkDtbl.Rows[i]);
                }
                strWhere.Length = 0;
            }

            tempField = string.Empty;
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                strWhere.Length = 0;
                secondCharIndex = 0;
                //孔位 如:A1,A2...H12
                tempHole = dtTemp.Rows[i]["孔位"].ToString().ToUpper().Trim();
                holeLen = tempHole.Length;
                if (holeLen > 3)//如果存在双孔的情况
                {
                    secondCharIndex = getSecondCharIndex(tempHole.ToCharArray());
                    th1 = tempHole.Substring(0, secondCharIndex);
                    th2 = tempHole.Substring(secondCharIndex, holeLen - secondCharIndex);
                    strWhere.AppendFormat(" (HolesNum LIKE '%{0}%' OR HolesNum LIKE '%{1}%') ", th1, th2);
                }
                else
                {
                    strWhere.AppendFormat(" HolesNum LIKE '%{0}%' ", tempHole);
                }
                tempField = dtTemp.Rows[i]["样本号"].ToString();
                if (strWhere.Length != 0)
                {
                    strWhere.Append(" AND ");
                }
                strWhere.AppendFormat(" MachineSampleNum='{0}'", tempField);
                tempField = dtTemp.Rows[i]["项目"].ToString();
                strWhere.AppendFormat(" AND MachineItemName='{0}'", tempField);
                tempField = dtTemp.Rows[i]["检测时间"].ToString();
                strWhere.AppendFormat(" AND CheckStartDate=#{0}#", tempField);
                strWhere.AppendFormat(" AND CheckMachine='{0}'", _machineCode);//检测仪器
                isSaved = _resultBll.IsExist(strWhere.ToString());

                dtTemp.Rows[i]["已保存"] = isSaved;
                strWhere.Length = 0;
            }

            DataView dv = dtTemp.DefaultView;
            dv.Sort = "检测时间 ASC,孔位 ASC";
            CheckDatas.DataSource = dv;

        }
        /// <summary>
        /// 获取第二字母的索引位置
        /// </summary>
        /// <param name="chArry"></param>
        /// <returns></returns>
        private int getSecondCharIndex(char[] chArry)
        {
            int j = 0;
            foreach (char c in chArry)
            {
                if (j > 0 && c >= 'A' && c <= 'H')//找出第二个字母的位置A1A2中 A2的位置
                {
                    break;
                }
                j++;
            }
            return j;
        }

        /// <summary>
        /// 分析结果字符串
        /// </summary>
        /// <param name="input"></param>
        private void doWith(string input)
        {
            byte[] bty = clsStringUtil.HexString2ByteArray(input);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            string temp = gb2312.GetString(bty);

            string num = string.Empty;
            string item = string.Empty;
            string checkValue = string.Empty;
            string unit = string.Empty;
            string time = string.Empty;
            string holes = string.Empty;
            string checkResult = string.Empty;
            string abs = string.Empty;
            int head = 2;
            int end = 0;


            head = 2;
            end = temp.IndexOf(",", head);
            num = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            item = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            holes = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            abs = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            checkValue = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            checkResult = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            unit = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            time = temp.Substring(head, end - head);

            head = end + 1;
            end = temp.IndexOf(",", head);
            time += " " + temp.Substring(head, end - head);

            addNewHistoryData(num, item, holes, abs, checkValue, checkResult, unit, time);

        }
        private void addNewHistoryData(string num, string item, string holes, string abs,
           string checkValue, string checkResult, string unit, string time)
        {
            DataRow dr = checkDtbl.NewRow();
            //dr["样本号"] = num;
            dr["已保存"] = "否";
            dr["孔位"] = holes;
            dr["样品名称"] ="" ;
            dr["检测项目"] = item;
            dr["abs"] = abs;
            dr["检测结果"] = checkValue;
            dr["单位"] = unit;
            dr["检测依据"] = "";
            dr["标准值"] = "";
            dr["检测仪器"] = "";
            dr["结论"] = checkResult;
            dr["采样时间"] = "";
            dr["采样地点"] = "";
            dr["被检单位"] = "";
            dr["检测员"] = "";
            if (time.Length == 0)
            {
                dr["检测时间"] = System.DateTime.Today.ToString();
            }
            else
            {
                dr["检测时间"] = Convert.ToDateTime(time).ToString();
            }
            checkDtbl.Rows.Add(dr);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //string err = string.Empty;
            //Global.EditorSave = null;
            //string[,] arr = new string[CheckDatas.Rows.Count, 7];
            //for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //{
            //    arr[i, 0] = CheckDatas.Rows[i].Cells[0].Value.ToString();
            //    arr[i, 1] = CheckDatas.Rows[i].Cells[1].Value.ToString();
            //    arr[i, 2] = CheckDatas.Rows[i].Cells[2].Value.ToString();
            //    arr[i, 3] = CheckDatas.Rows[i].Cells[3].Value.ToString();
            //    arr[i, 4] = CheckDatas.Rows[i].Cells[4].Value.ToString();
            //    arr[i, 5] = CheckDatas.Rows[i].Cells[5].Value.ToString();
            //    arr[i, 6] = CheckDatas.Rows[i].Cells[6].Value.ToString();
            //}
            //Global.EditorSave = arr;
            //Global.TableRowNum = CheckDatas.Rows.Count;
            //function.frmSetResult Sform = new function.frmSetResult();
            //DialogResult dr = Sform.ShowDialog(this);
            //if (dr == DialogResult.OK)
            //{
            //    for (int j = 0; j < CheckDatas.Rows.Count; j++)
            //    {
            //        CheckDatas.Rows[j].Cells[0].Value = "是";
            //        sqlSet.UpdateTempResult("是", CheckDatas.Rows[j].Cells[1].Value.ToString(), out err);
            //    }
            //}
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string chk = string.Empty;
            int iok = 0;
            int s = 0;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells[0].Value.ToString() == "False" || CheckDatas.Rows[i].Cells[0].Value.ToString() == "否")
                {
                    resultdata.Save = "是";
                    //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    resultdata.SampleName = CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim();

                    resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
                    resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                    resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
                    resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
                    resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();//标准值
                    resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString().Trim();//检测仪器
                    resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
                    resultdata.Gettime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();//采样时间
                    resultdata.Getplace = CheckDatas.Rows[i].Cells[10].Value.ToString().Trim();
                    resultdata.CheckUnit = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();
                    resultdata.Tester = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();
                    chk = CheckDatas.Rows[i].Cells[13].Value.ToString().Replace("-", "/").Trim();
                    resultdata.CheckTime = DateTime.Parse(chk);

                    s = sqlSet.ResuInsert(resultdata, out err);
                    if (s == 1)
                    {
                        iok = iok + 1;
                        CheckDatas.Rows[i].Cells[0].Value = "是";
                    }
                }
            }
            MessageBox.Show("共保存" + iok + "条数据", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            CheckDatas.Refresh();
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbAdd.Visible = false;
        }
    }
}
