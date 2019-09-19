using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public partial class FrmAutoTakeDY6300 : FrmAutoTakeDY
    {
        private string tagName = string.Empty;
        private List<int> array = new List<int>();
        //private int[] array = new int[0x4e20];//已经修改为泛型数组
        private bool bBusyImport = false;
        private int buff;
        private int count = 0;
        private int countOutput = 0;
        private int endadd = 0;
        private int number;
        private int procent = 0;
        private int procentOutput = 0;
        private int serial;
        private int startadd = 0;
        private int waitingTime;
        private string content = string.Empty;

        /// <summary>
        /// 处理中间过程数据
        /// </summary>
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 组合查询条件
        /// </summary>
        private StringBuilder strWhere = new StringBuilder();
        private bool IsCreatDT = false;//指示是否创建数据表对象
        private bool IsReadByDate = false;//指是否按时间段读取数据
        private DataTable checkDtbl = new DataTable("checkDtbl");

        //private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnReadByDate;
        private System.Windows.Forms.Button btnMachineDel;
        private System.IO.Ports.SerialPort port;

        public FrmAutoTakeDY6300(string tag)
            : base(tag)
        {
            //InitializeComponent();
            // this.components = new System.ComponentModel.Container();

            this.progressBar1 = new ProgressBar();
            this.progressBar1.Location = new System.Drawing.Point(322, 540);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(277, 23);
            this.progressBar1.TabIndex = 485;
            //	progressBar1.
            this.Controls.Add(this.progressBar1);

            this.btnReadByDate = new System.Windows.Forms.Button();
            this.btnReadByDate.Location = new System.Drawing.Point(288, 571);
            this.btnReadByDate.Name = "btnReadByDate";
            this.btnReadByDate.Size = new System.Drawing.Size(49, 24);
            this.btnReadByDate.TabIndex = 486;
            this.btnReadByDate.Text = "读取";
            this.btnReadByDate.Click += new System.EventHandler(this.btnReadByDate_Click);
            this.Controls.Add(this.btnReadByDate);

            this.btnMachineDel = new System.Windows.Forms.Button();
            this.btnMachineDel.Location = new System.Drawing.Point(190, 571);
            this.btnMachineDel.Name = "btnMachineDel";
            this.btnMachineDel.Size = new System.Drawing.Size(90, 24);
            this.btnMachineDel.TabIndex = 478;
            this.btnMachineDel.Text = "清除仪器数据";
            this.btnMachineDel.Click += new System.EventHandler(this.btnMachineDel_Click);
            this.Controls.Add(btnMachineDel);

            this.Load += new System.EventHandler(this.FrmAutoTakeATP_Load);

            this.port = new System.IO.Ports.SerialPort();//this.components
            this.port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.port_DataReceived);
            //this.SuspendLayout();
            //this.ResumeLayout(false);

            tagName = tag;
        }

        private void FrmAutoTakeATP_Load(object sender, EventArgs e)
        {
            dtStart.Visible = true;
            dtEnd.Visible = true;
            lblFrom.Text = "  从";
            btnReadHis.Text = "读取所有数据";
            btnReadHis.Location = new System.Drawing.Point(16, 571);
            btnReadHis.Size = new System.Drawing.Size(90, 24);

            btnClear.Text = "清除列表";
            btnClear.Location = new System.Drawing.Point(112, 571);
            btnClear.Size = new System.Drawing.Size(70, 24);
            this.BindCheckItem();
            base.BindInit();
        }

        private void BindCheckItem()
        {
            //配置DY6100
            CommonOperation.GetMachineSetting(tagName);//"DY6100"
            port.PortName = ShareOption.ComPort.Replace(":", "");
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }

        protected override void winClose()
        {
            if (port.IsOpen)
            {
                //port.Write("E");
                port.Close();
            }
            base.winClose();
        }

        protected override void CheckRowState()
        {
            int rows = c1FlexGrid1.RowSel;
            if (rows >= 1)
            {
                if (cmbCheckItem.Text.Equals(""))
                {
                    cmbCheckItem.Text = c1FlexGrid1.Rows[rows]["项目"].ToString();
                }
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[rows]["检测值"].ToString();
                cmbResult.Text = c1FlexGrid1.Rows[rows]["结论"].ToString();

                txtResultInfo.Text = c1FlexGrid1.Rows[rows]["单位"].ToString();
                lblHolesNum.Text = "";//无孔位的概念
                lblMachineItemName.Text = c1FlexGrid1.Rows[rows]["项目"].ToString();
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[rows]["编号"].ToString();
                //base.CheckRowState();
            }
        }

        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            btnReadHis.Enabled = false;
            btnReadByDate.Enabled = false;

            if (!port.IsOpen)
            {
                try
                {
                    port.Open();
                    port.Write("F");
                }
                catch (Exception)
                {
                    EndConnation();
                    MessageBox.Show("串口配置有误!");
                    return;
                }
            }

            if (!bBusyImport)
            {
                bBusyImport = false;
                cleanArray();
                if (port.IsOpen)
                {
                    waitingTime = 300;
                    doOperation(waitingTime, progressBar1, btnReadHis);
                    getResultRecordNum();
                    readArray();

                    getSerialNumber();
                    Thread.Sleep(500);
                    readArray();
                    if (number != 0)
                    {
                        GetData();
                        Thread.Sleep(0x3e8);//1000
                        bBusyImport = true;
                        RecieveData();
                    }
                    else
                    {
                        EndConnation();
                        MessageBox.Show("暂无数据");
                    }
                }
                else
                {
                    EndConnation();
                    MessageBox.Show("串口配置有误");
                }
            }
            else
            {
                EndConnation();
                MessageBox.Show("仪器正在工作，请稍等", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void btnClear_Click(object sender, EventArgs e)
        {
            array.Clear();
            startadd = 0;
            count = 0;
            procentOutput = 0;
            procent = 0;
            sb.Length = 0;
            checkDtbl.Clear();
            c1FlexGrid1.DataSource = checkDtbl;
            c1FlexGrid1.AutoSizeCols();
            //base.btnClear_Click(sender, e);
        }

        private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            RecieveData();
        }

        /// <summary>
        /// 构造表格对象
        /// </summary>
        private void CreateDataTable()
        {
            if (!IsCreatDT)
            {
                DataColumn dataCol;

                ///////////////////新增
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(int);//int,string
                dataCol.ColumnName = "编号";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "项目";
                checkDtbl.Columns.Add(dataCol);


                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                checkDtbl.Columns.Add(dataCol);
                IsCreatDT = true;
            }
        }

        //private delegate void SetTextCallback(string text);
        private delegate void DoOperationDelegate(int seconds, ProgressBar pBar, Button button);
        private delegate void UpdateUIDelegate(Button button, ProgressBar pBar, int value);

        /// <summary>
        /// 更新界面状态
        /// </summary>
        /// <param name="button"></param>
        /// <param name="pBar"></param>
        /// <param name="value"></param>
        private void UpdateUI(Button button, ProgressBar pBar, int value)
        {
            if (InvokeRequired)
            {
                UpdateUIDelegate d = new UpdateUIDelegate(UpdateUI);
                Invoke(d, new object[] { button, pBar, value });
            }
            else if (value >= 100)
            {
                button.Enabled = true;
                pBar.Value = 100;
            }
            else
            {
                pBar.Value = value;
            }
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="pBar"></param>
        /// <param name="button"></param>
        private void doOperation(int seconds, ProgressBar pBar, Button button)
        {
            btnReadHis.Enabled = false;
            btnReadByDate.Enabled = false;

            for (int i = 0; i <= 100; i++)
            {
                seconds = waitingTime;
                Thread.Sleep((int)(seconds / 100));
                UpdateUI(button, pBar, i);
            }
            //btnReadHis.Enabled = true;
            //btnReadByDate.Enabled = true;
        }
        /// <summary>
        /// 读取完毕
        /// </summary>
        private void EndConnation()
        {
            //由于发送“E”会使仪器死掉，所以暂不发送
            //if (port.IsOpen)
            //{
            //    port.Write("E");
            //    port.Close();
            //}
            UpdateUI(btnReadHis, progressBar1, 0);
            sb.Length = 0;
            btnReadHis.Enabled = true;
            btnReadByDate.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 清理数组
        /// </summary>
        private void cleanArray()
        {
            startadd = 0;
            array.Clear();
            //array.Initialize();
            //int b = 0;
            //while (count > b)
            //{
            //    array[b] = 0;
            //    b++;
            //}

            count = 0;
            procentOutput = 0;
            procent = 0;
            UpdateUI(btnReadHis, progressBar1, 0);
        }

        /// <summary>
        /// 删除仪器结果命令
        /// </summary>
        private void deleteResult()
        {
            port.Write("D");
        }

        /// <summary>
        /// 获取检测记录条数命令
        /// </summary>
        private void getResultRecordNum()
        {
            port.Write("N");
        }
        /// <summary>
        /// 获取串口号
        /// </summary>
        private void getSerialNumber()
        {
            port.Write("G");
        }
        /// <summary>
        /// 发送R获取检测记录
        /// </summary>
        private void GetData()
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Write("R");
                }
                else
                {
                    string caption = "串口接连异常";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    if (MessageBox.Show(this, "串口没有接连", caption, buttons, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        port.Write("F");
                        GetData();
                        EndConnation();
                    }
                }
            }
            catch
            {
                EndConnation();
                MessageBox.Show("数据读取有误");
                //sMessageErrReadingData
            }
        }
        /// <summary>
        /// 读取数组
        /// </summary>
        private void readArray()
        {
            if (count <= 0)
            {
                return;
            }
            //for (int i = 0; i <= count; i++)
            for (int i = 0; i < count; i++)
            {
                if (i >= array.Count)
                {
                    continue;
                }
                //sb.Append(array[i]);
                sb.Append(array[i]);
                sb.Append(";");
                // if ((array[i] == 110) & (i <= 6))
                if ((i <= 6) && (array[i] == 110))
                {
                    i++;
                    i++;
                    number = array[i];
                    number = number << 8;
                    i--;
                    number += array[i];
                    i--;
                }
                //if ((array[i] == 0x67) & (i <= 10))
                if ((i <= 10) && (array[i] == 0x67))
                {
                    i++;
                    i++;
                    serial = array[i];
                    serial = serial << 8;
                    i--;
                    serial += array[i];
                    startadd = i + 2;
                    i--;
                }
                endadd = i;
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void RecieveData()
        {
            int[] iNumArray = new int[2];
            endadd = number * 0x13;
            endadd = (endadd + startadd) - 1;
            try
            {
                if (port.BytesToRead > 0)
                {
                    while (port.BytesToRead > 0)
                    {
                        buff = port.ReadByte();
                        array.Add(buff);
                        //array[count] = buff;
                        if (bBusyImport)
                        {
                            countOutput = count;
                            procent = endadd / 100;
                            countOutput = count % procent;
                            if (countOutput == 0)
                            {
                                procentOutput++;
                                UpdateUI(btnReadHis, progressBar1, procentOutput);
                            }
                        }
                        count++;
                    }
                }
                if (bBusyImport)
                {
                    if (count >= endadd)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        btnReadHis.Enabled = false;
                        btnReadByDate.Enabled = false;

                        readArray();
                        GetFormatData();
                        bBusyImport = false;
                        cleanArray();
                        EndConnation();
                    }
                }
            }
            catch
            {
                EndConnation();
                // MessageBox.Show("数据传输异常");
            }
        }


        /// <summary>
        /// 最后格式化字符串,输出
        /// 数据格式：
        /// 仪器、测试号、结论、测试值、下限、上限、分组号、检测编点、时间、星期、日期
        /// sw.WriteLine("SERIAL-NR, TEST-NR, RESULT, RLU, DOWN, UP, GROUP, PROG, TIME, DAY, DATE,");
        /// </summary>
        /// <returns></returns>
        private void GetFormatData()
        {
            if (checkDtbl != null)
            {
                checkDtbl.Clear();
            }
            int b = 0;
            int x = startadd + 1;
            content = sb.ToString();
            if (content != "")
            {
                int iResultNr = 0;
                int iRLU = 0;
                int iDown = 0;
                int iUp = 0;
                int iGroup = 0;
                int iProg = 0;
                int iSec = 0;
                int iMin = 0;
                int iHours = 0;
                int iDate = 0;
                int iMonth = 0;
                int iYear = 0;

                bool isSaved = false;
                DateTime date;
                DateTime dstart = dtStart.Value.Date;
                DateTime dEnd = dtEnd.Value.Date.AddDays(1).AddSeconds(-1);
                string item = "洁净度";
                string day;//原来是指星期,现为长日期字符串
                string result;//结论

                DataRow dr;
                x = startadd + 2;
                try
                {
                    CreateDataTable();
                    while (b < number)
                    {
                        day = string.Empty;
                        result = string.Empty;
                        iResultNr = array[x];
                        iResultNr = iResultNr << 8;
                        x--;
                        iResultNr += array[x];

                        x += 3;
                        iRLU = array[x];
                        iRLU = iRLU << 8;
                        x--;
                        iRLU += array[x];

                        x += 3;
                        iDown = array[x];
                        iDown = iDown << 8;
                        x--;
                        iDown += array[x];

                        x += 3;
                        iUp = array[x];
                        iUp = iUp << 8;
                        x--;
                        iUp += array[x];

                        x += 2;
                        iGroup = array[x];//分组号没用到

                        x++;
                        iProg = array[x];//检测点号没用到

                        x++;
                        iSec = array[x];

                        x++;
                        iMin = array[x];

                        x++;
                        iHours = array[x];
                        x++;

                        //原来有一个星期的段
                        x++;
                        iDate = array[x];

                        x++;
                        iMonth = array[x];

                        x++;
                        iYear = array[x];

                        iYear = 0x7d0 + iYear;
                        x++;

                        //////////////////由于仪器时间存在问题，下面做一些转化
                        if (iSec >= 60)
                        {
                            iSec = iSec % 60;
                            iMin += 1;
                        }
                        if (iMin >= 60)
                        {
                            iMin = iMin % 60;
                            iHours += 1;
                        }
                        if (iHours >= 24)
                        {
                            iHours = iHours % 24;
                            iDate += 1;
                        }

                        if (iMonth > 12)
                        {
                            iMonth = iMonth % 12;
                            iYear += 1;
                        }

                        if (iRLU <= iDown)
                        {
                            result = "合格";//合格pass
                        }
                        else if ((iRLU > iDown) & (iRLU <= iUp))
                        {
                            result = "警戒";//警戒 warning
                        }
                        else //不合格
                        {
                            result = "不合格";//error
                        }
                        day = string.Concat(new object[] { iYear, "-", iMonth, "-", iDate, " ", iHours, ":", iMin, ":", iSec });

                        try
                        {
                            date = Convert.ToDateTime(day);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        //还要判断是否已经保存的数据
                        strWhere.Length = 0;
                        strWhere.Append(" checkMachine='019'");
                        strWhere.AppendFormat(" AND MachineSampleNum='{0}'", iResultNr);
                        strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                        strWhere.AppendFormat(" AND CheckStartDate=#{0}#", date);
                        isSaved = _resultBll.IsExist(strWhere.ToString());
                        strWhere.Length = 0;

                        if (IsReadByDate)//如果是按时间段
                        {
                            //if (date < dstart || date > dEnd)
                            //{
                            //    continue;
                            //}

                            //if (date >= dstart && date <= dEnd)
                            //{
                            //dr = checkDtbl.NewRow();
                            //dr["已保存"] = isSaved;
                            //dr["编号"] = iResultNr;
                            //dr["项目"] = item;
                            //dr["检测值"] = iRLU;
                            //dr["单位"] = "RLU";//检测值
                            //dr["检测时间"] = date;
                            //dr["结论"] = result;
                            //checkDtbl.Rows.Add(dr);
                            //}
                        }
                        //else
                        //{
                        //    dr = checkDtbl.NewRow();
                        //    dr["已保存"] = isSaved;
                        //    dr["编号"] = iResultNr;
                        //    dr["项目"] = item;
                        //    dr["检测值"] = iRLU;
                        //    dr["单位"] = "RLU";//检测值
                        //    dr["检测时间"] = date;
                        //    dr["结论"] = result;
                        //    checkDtbl.Rows.Add(dr);
                        //}

                        dr = checkDtbl.NewRow();
                        dr["已保存"] = isSaved;
                        dr["编号"] = iResultNr;
                        dr["项目"] = item;
                        dr["检测值"] = iRLU;
                        dr["单位"] = "RLU";//检测值
                        dr["检测时间"] = date;
                        dr["结论"] = result;
                        checkDtbl.Rows.Add(dr);

                        b++;
                        x += 3;
                    }
                    if (checkDtbl.Rows.Count <= 0)
                    {
                        IsReadByDate = false;
                        EndConnation();
                        MessageBox.Show("暂无符合条件数据");
                        return;
                    }
                    if (IsReadByDate)//如果是按时间段
                    {
                        for (int rowIndex = 0; rowIndex < checkDtbl.Rows.Count; rowIndex++)
                        {
                            DateTime dateTime = Convert.ToDateTime(checkDtbl.Rows[rowIndex]["检测时间"]);
                            if (dateTime < dstart || dateTime > dEnd)
                            {
                                checkDtbl.Rows.RemoveAt(rowIndex);
                                rowIndex--;
                            }
                        }
                    }
                    c1FlexGrid1.DataSource = checkDtbl;
                    c1FlexGrid1.AutoSizeCols();
                    if (c1FlexGrid1.Cols["已保存"] != null)
                    {
                        c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                        c1FlexGrid1.Cols["已保存"].AllowDragging = false;
                        c1FlexGrid1.Cols["已保存"].AllowSorting = false;
                    }
                    IsReadByDate = false;
                    EndConnation();
                }
                catch (Exception e)
                {
                    EndConnation();
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// 接日期时间读取数据范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadByDate_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show(this, "开始时间不能超过截止时间");
                return;
            }
            IsReadByDate = true;
            btnReadHistory_Click(sender, e);
        }
        /// <summary>
        /// 清除仪器数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMachineDel_Click(object sender, EventArgs e)
        {
            if (!port.IsOpen)
            {
                MessageBox.Show("串口连接有误!");
                return;
            }

            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            if (MessageBox.Show(this, "是否确定删除仪器数据？如果确定，仪器数据将被删除且永远不可恢复", "提示", buttons, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                deleteResult();
            }
        }

    }
}