using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeLD : FrmAutoTakeDY //partial
    {
        private clsDY3000 curDY3000;//原来有static 表示旧3000
        private clsDY5000LD curDY5000LD;//原来有static
        private string tagName;
        /// <summary>
        /// DY5000LD系列
        /// </summary>
        private DataTable checkDtbl = null;
        private bool IsCreatDT = false;
        private readonly HolesSetingOpr holesBll = new HolesSetingOpr();
        private StringBuilder strWhere = new StringBuilder();
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
        public FrmAutoTakeLD(string tag)
            : base(tag)
        {
            this.Load += new System.EventHandler(this.FrmAutoTakeLeiDu_Load);
            tagName = tag;
            CreateTable();
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeLeiDu_Load(object sender, EventArgs e)
        {
            btnReadHis.Visible = false;
            dtStart.Visible = false;
            dtEnd.Visible = false;
            lblFrom.Visible = false;
            lblTo.Visible = false;
            BindCheckItem();
            base.BindInit();
            this.btnClear.Location = new System.Drawing.Point(158, 532);
        }

        /// <summary>
        /// 创建表结构
        /// </summary>
        private void CreateTable()
        {
            if (!IsCreatDT)
            {
                DataColumn dataCol;
                checkDtbl = new DataTable("checkData");

                ////////////////////////////新增
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                //int 编号如果是int可以按正确数值的排序,但非数字就有问题，如果是string按第一字符排
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int string
                dataCol.ColumnName = "样本号";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "项目";
                checkDtbl.Columns.Add(dataCol);

                if (!tagName.Equals("DY3000"))//DY5000LD系列
                {
                    dataCol = new DataColumn();
                    dataCol.DataType = typeof(string);
                    dataCol.ColumnName = "孔位";
                    checkDtbl.Columns.Add(dataCol);

                    dataCol = new DataColumn();
                    dataCol.DataType = typeof(string);
                    dataCol.ColumnName = "abs";
                    checkDtbl.Columns.Add(dataCol);

                    dataCol = new DataColumn();
                    dataCol.DataType = typeof(string);
                    dataCol.ColumnName = "定量结果";
                    checkDtbl.Columns.Add(dataCol);

                    dataCol = new DataColumn();
                    dataCol.DataType = typeof(string);
                    dataCol.ColumnName = "定性结果";
                    checkDtbl.Columns.Add(dataCol);
                }
                else //旧版DY3000
                {
                    dataCol = new DataColumn();
                    dataCol.DataType = typeof(string);
                    dataCol.ColumnName = "检测值";
                    checkDtbl.Columns.Add(dataCol);
                }

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);
                IsCreatDT = true;

                holeDtbl = new DataTable();
                //加入临时表 DataTable对象
                dtTemp = new DataTable();
                for (int i = 0; i < checkDtbl.Columns.Count; i++)
                {
                    //有重载的方法，可以加入列数据的类型
                    dtTemp.Columns.Add(checkDtbl.Columns[i].ColumnName, checkDtbl.Columns[i].DataType);
                }
            }
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

            // htbl.Clear();
            DataView dv = dtTemp.DefaultView;
            dv.Sort = "检测时间 ASC,孔位 ASC";
            c1FlexGrid1.DataSource = dv;
            c1FlexGrid1.AutoSizeCols();
            /////////////////////////新增
            if (c1FlexGrid1.Cols["已保存"] != null)
            {
                c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }
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

        protected void BindCheckItem()
        {
            try
            {
                if (tagName.Equals("DY3000"))//老版本DY3000
                {
                    ///加载仪器信息
                    CommonOperation.GetMachineSetting(tagName);//"DY3000"
                    c1FlexGrid1.AutoSizeCols();
                    dataheadString = "522C";
                    curDY3000 = new clsDY3000();
                    if (!curDY3000.Online)
                    {
                        curDY3000.Open();
                    }
                }
                else
                {
                    //加载仪器信息
                    CommonOperation.GetMachineSetting(tagName + "LD");//"clsDY5000LD"注意要加"LD"表示雷度版本
                    c1FlexGrid1.AutoSizeCols();
                    dataheadString = "422C";
                    curDY5000LD = new clsDY5000LD();
                    if (!curDY5000LD.Online)
                    {
                        curDY5000LD.Open();
                    }
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
            // base.BindCheckItem();//不能加此函数
        }

        protected override void CheckRowState()
        {
            int rows = c1FlexGrid1.RowSel;
            if (rows < 1)
            {
                return;
            }

            string strItem = string.Empty;
            if (tagName.Equals("DY3000"))//旧版DY3000
            {
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[rows]["检测值"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[rows]["单位"].ToString();
                strItem = c1FlexGrid1.Rows[rows]["项目"].ToString();

                ///////////////////////////////新增。记录在label控件中
                lblHolesNum.Text = string.Empty;//无孔位的概念
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[rows]["样本号"].ToString();
            }
            else //DY5000LD系列
            {
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[rows]["定量结果"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[rows]["单位"].ToString();
                strItem = c1FlexGrid1.Rows[rows]["项目"].ToString();

                ///////////////////////////新增
                lblHolesNum.Text = c1FlexGrid1.Rows[rows]["孔位"].ToString();
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[rows]["样本号"].ToString();
            }
        }

        /// <summary>
        /// 重写关闭
        /// </summary>
        protected override void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }

            if (tagName.Equals("DY3000"))
            {
                if (curDY3000 != null)
                {
                    curDY3000.Close();
                    curDY3000 = null;
                }
            }
            else
            {
                if (curDY5000LD != null)
                {
                    curDY5000LD.Close();
                    curDY5000LD = null;
                }
            }
            if (checkDtbl != null)
            {
                checkDtbl.Clear();
            }
            this.Dispose();
            base.winClose();
        }

        /// <summary>
        /// 重写清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            strWhere.Length = 0;
            sbData.Length = 0;
            checkDtbl.Clear();
            c1FlexGrid1.DataSource = checkDtbl;
            c1FlexGrid1.AutoSizeCols();
        }

        /// <summary>
        /// 构造旧DY5000系列 行数据
        /// </summary>
        /// <param name="num">样品号</param>
        /// <param name="item"></param>
        /// <param name="holes"></param>
        /// <param name="abs"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkResult"></param>
        /// <param name="unit"></param>
        /// <param name="time"></param>
        private void addNewHistoryData(string num, string item, string holes, string abs,
            string checkValue, string checkResult, string unit, string time)
        {
            DataRow dr = checkDtbl.NewRow();
            dr["样本号"] = num;
            dr["项目"] = item;
            dr["孔位"] = holes;
            dr["abs"] = abs;
            dr["定量结果"] = checkValue;
            dr["定性结果"] = checkResult;
            dr["单位"] = unit;
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

        /// <summary>
        /// 构造旧DY3000行数据
        /// </summary>
        /// <param name="num"></param>
        /// <param name="item"></param>
        /// <param name="checkValue"></param>
        /// <param name="unit"></param>
        /// <param name="time"></param>
        private void addNewHistoryData(string num, string item, string checkValue, string unit, string time)
        {
            DataRow dr;
            dr = checkDtbl.NewRow();
            dr["样本号"] = num;
            dr["项目"] = item;
            dr["检测值"] = checkValue;
            dr["单位"] = unit;
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

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.Read3000LDData)
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
                        IsSavedRecord();
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 分析结果字符串
        /// </summary>
        /// <param name="input"></param>
        private void doWith(string input)
        {
            byte[] bty = StringUtil.HexString2ByteArray(input);
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
            if (tagName.Equals("DY3000"))
            {
                head = 2;
                end = temp.IndexOf(",", head);
                num = temp.Substring(head, end - head);

                head = end + 1;
                end = temp.IndexOf(",", head);
                item = temp.Substring(head, end - head);

                head = end + 1;
                end = temp.IndexOf(",", head);
                checkValue = temp.Substring(head, end - head);

                head = end + 1;
                end = temp.IndexOf(",", head);
                unit = temp.Substring(head, end - head);

                head = end + 1;
                end = temp.IndexOf(",", head);
                if (end > 0)
                {
                    time = temp.Substring(head, end - head);
                }
                addNewHistoryData(num, item, checkValue, unit, time);
            }

            else
            {
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
        }

    }
}