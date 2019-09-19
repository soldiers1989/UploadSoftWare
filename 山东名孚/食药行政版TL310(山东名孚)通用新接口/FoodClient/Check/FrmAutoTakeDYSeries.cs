using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;
using FoodClient.App_Code;
using DY.Process;

namespace FoodClient
{
    public partial class FrmAutoTakeDYSeries : FrmAutoTakeDY
    {
        private TL310New tl310new = new TL310New();
        private clsDY3000DY curDY3000DY = null;
        //private clsTL310 tl310 = new clsTL310();
        private StringBuilder strWhere = new StringBuilder();
        private string tagName = string.Empty;
        private delegate void InvokeDelegate(DataTable dtbl);
        private Timer TimeButton = new Timer();
        private delegate void InvokeButton(Button btn);
        private PercentProcess process = new PercentProcess();
        public FrmAutoTakeDYSeries(string tag)
            : base(tag)
        {
           //InitializeComponent();
            if (tag == "TL310")
            {
                tagName = tag;
            }
            else
            {
                tagName = tag + "DY";//注意标识码后缀多了“DY”
            }
            
            this.Load += new System.EventHandler(this.FrmAutoTakeDYSeries_Load);
            //MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
        }

        private void FrmAutoTakeDYSeries_Load(object sender, System.EventArgs e)
        {
            if (tagName == "TL310")
            {
                DTPchktime.Value = DateTime.Now;
                dtEnd.Visible = false;
                lblTo.Visible = false;
                dtStart.Visible = false;
                lblFrom.Visible = false;
                btnReadAllData.Visible = true;
                labelChkTime.Visible = true ;
                DTPchktime.Visible = true ;
                btnRead.Visible = true ;
                btnReadHis.Text = "读取最近数据";
                btnReadHis.Width = 117;
                btnReadHis.Height = 27;
                btnReadAllData.Location = new Point(187, 553);
                btnReadHis.Location = new Point(57, 553);
                btnClear.Location = new Point(330, 553);

                //c1FlexGrid1.DataSource = tl310.DataReadTable;
            }
            else
            {
                labelChkTime.Visible = false;
                DTPchktime.Visible = false;
                btnRead.Visible = false;
                dtEnd.Visible = true ;
                lblTo.Visible = true ;
                btnReadAllData.Visible = false;
                btnReadHis.Location = new Point(300, 543);
                btnClear.Location = new Point(385, 543);
            }
           
            if(tagName.Equals("DY1000DY"))
                lblSuppresser.Text = "抑制率：";
            this.BindCheckItem();
            base.BindInit();
            TimeButton.Interval=15000;
            TimeButton.Tick += new EventHandler(TimeButton_Tick);
            TimeButton.Start() ;
        }

        private void TimeButton_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(new InvokeButton(showbutton), btnReadAllData);
            else
                btnReadAllData.Enabled =true ;
        }

        private void showbutton(Button btn)
        {
            btnReadAllData.Enabled = true;
        }
        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(tagName);
            try
            {  
                if (tagName == "TL310")
                {

                    if (!tl310new.mSerial.IsOpen)
                    {
                        tl310new.Open(ShareOption.ComPort.Replace("：", "").Replace(":", "").Trim(), 115200);
                    }
                    //if (!tl310.Online)
                    //{
                    //    tl310.Open();
                    //}
                }
                else
                {
                    curDY3000DY = new clsDY3000DY();
                    if (!curDY3000DY.Online)
                        curDY3000DY.Open();
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (tagName == "DY6600DY")
            {
                string[] arrTEM = ShareOption.DefaultCheckItemCode.Split('}');
                string Temp62 = string.Empty;
                string Temp42 = string.Empty;
                string Temp72 = string.Empty;
                for (int i = 0; i < arrTEM.Length; i++)
                {
                    if (arrTEM[i].Contains("金标法"))
                        Temp62 = Temp62 + arrTEM[i].ToString() + "}";
                    if (arrTEM[i].Contains("非试纸法"))
                        Temp42 = Temp42 + arrTEM[i].ToString() + "}";
                    if (arrTEM[i].Contains("干化学法"))
                        Temp72 = Temp72 + arrTEM[i].ToString() + "}";
                }
                _checkItems = StringUtil.GetDY3000DYAry(Temp42);
                _checkItems62 = StringUtil.GetDY3000DYAry(Temp62);
                _checkItems72 = StringUtil.GetDY3000DYAry(Temp72);

                clsDY3000DY.CheckItemsArray = _checkItems;
                clsDY3000DY.CheckItemsArray62 = _checkItems62;
                clsDY3000DY.CheckItemsArray72 = _checkItems72;
            }
            if (tagName != "DY6600DY")
            {
                _checkItems = StringUtil.GetDY3000DYAry(ShareOption.DefaultCheckItemCode);
                clsDY3000DY.CheckItemsArray = _checkItems;
            }
        }
        /// <summary>
        /// 山东按时间读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                bool blOnline = false;
                if (tl310new.mSerial.IsOpen)
                {
                    blOnline = true;
                }
                else
                {
                    blOnline = false;
                }
                if (!blOnline)
                {
                    MessageBox.Show(this, "串口连接有误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Default;
                    return;
                }
                tl310new.ReadTimeData(DTPchktime.Value);
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnReadHistory_Click(object sender, System.EventArgs e)
        {
            bool blOnline = false;
            try
            {
                if (tagName == "TL310")
                {
                    if (tl310new.mSerial.IsOpen)
                    {
                        blOnline = true;
                    }
                    else
                    {
                        blOnline = false;
                    }
                    //if (!tl310.Online)
                    //{
                    //    tl310.Open();
                    //}
                    //blOnline = tl310.Online;
                }
                else
                {
                    blOnline = curDY3000DY.Online;
                }
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
                //btnReadHis.Enabled = false;
                btnClear.Enabled = false;
                btnReadAllData.Enabled = false;
              
                c1FlexGrid1.DataSource = null;
                c1FlexGrid1.AutoSizeCols();
         
                if (tagName == "TL310")
                {
                  
                    tl310new.DataReadTable.Clear();
                    //tl310new.readtype = "读取上一次数据";
                    tl310new.sendOnce();
                    //process.BackgroundWork = downdata;
                    //process.Start();
                    //tl310.readtype = "读取上一次数据";
                    //tl310.ReadHistory(dtStart.Value, dtEnd.Value);
                }
                else
                {
                    //dtEnd.Value = dtStart.Value.Date.AddDays(1).AddSeconds(-1);
                    if (tagName == "DY6600DY")
                    {
                        if (!string.IsNullOrEmpty(base.cmbCqeck.Text))
                        {
                            curDY3000DY.ReadHistory2(dtStart.Value.Date, dtEnd.Value, base.cmbCqeck.Text);
                        }
                        else
                        {
                            MessageBox.Show("请选择检测方式！");
                            base.cmbCqeck.Focus();
                            Cursor = Cursors.Default;
                            btnReadHis.Enabled = true;
                            btnClear.Enabled = true;
                            return;
                        }
                    }
                    curDY3000DY.ReadHistory(dtStart.Value.Date, dtEnd.Value);
                }
            }
        }
        //private void downdata(Action<int> percent)
        //{
        //}
        /// <summary>
        /// 读取全部数据
        /// </summary>
        protected override void btnReadAllData_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            tl310new.DataReadTable.Clear();
            c1FlexGrid1.DataSource = null ;
            c1FlexGrid1.AutoSizeCols();
            tl310new.sendSecond();

            //if (!tl310.Online)
            //{
            //    tl310.Open();
            //}

            //if (!tl310.Online)
            //{
            //    MessageBox.Show(this, "串口连接有误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Cursor = Cursors.Default;
            //    return;
            //}

            btnReadAllData.Enabled = false;
            btnReadHis.Enabled = false;
            btnClear.Enabled = false;
            Cursor = Cursors.WaitCursor;
            //tl310.readtype = "读取全部数据";
            //tl310.ReadHistory(dtStart.Value, dtEnd.Value);
        }
        protected override void CheckRowState()
        {
            object obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["已保存"];
            if (obj != null && Convert.ToBoolean(obj))
            {
                MessageBox.Show("此数据已经保存过");
                return;
            }
            if (obj == null)
            {
                return;
            }

            if (c1FlexGrid1.RowSel >= 1 && obj!=null )
            {
                //obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["数据可疑性"];
                if (obj.ToString().Equals("是"))
                {
                    DialogResult dr = MessageBox.Show(this, "这是可疑检测记录，是否真要采集数据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                        return;
                }
                string strItem = string.Empty;
                if (tagName == "TL310")
                {
                    txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测结果"].ToString().Trim();
                    txtResultInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["单位"].ToString().Trim();
                    strItem = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测项目"].ToString();
                    lblHolesNum.Text = "";
                    lblMachineItemName.Text = strItem;
                    txtHole.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["通道号"].ToString();
                }
                else
                {
                    txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["抑制率"].ToString().Trim();
                    txtResultInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["单位"].ToString().Trim();
                    strItem = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测项目"].ToString();
                    lblHolesNum.Text = "";
                    lblMachineItemName.Text = strItem;
                    lblMachineSampleNum.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["编号"].ToString();
                }
            }
        }

        protected override void winClose()
        {
            if (MessageNotification.GetInstance() != null)
            {
                MessageNotification.GetInstance().DataRead -= NotificationEventHandler;
            }
            try
            {
                if (curDY3000DY != null)
                {
                    if (curDY3000DY.Online)
                        curDY3000DY.Close();
                    curDY3000DY = null;
                }
                else if (tl310new != null)
                {
                    if (tl310new.mSerial.IsOpen)
                        tl310new.closecom();
                    //if (tl310.Online)
                    //    tl310.Close();
                    //tl310 = null;
                }
            }
            catch { }
            base.winClose();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            if (tagName == "TL310")
            {
                tl310new.DataReadTable.Clear();
                c1FlexGrid1.DataSource = tl310new.DataReadTable;
                //tl310.DataReadTable.Clear();
                //c1FlexGrid1.DataSource = tl310.DataReadTable;
                c1FlexGrid1.AutoSizeCols();
            }
            else
            {
                curDY3000DY.DataReadTable.Clear();
                c1FlexGrid1.DataSource = curDY3000DY.DataReadTable;
                c1FlexGrid1.AutoSizeCols();
            }
            
            //strWhere.Length = 0;
            //ClearData();
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearData()
        {
            if (curDY3000DY.DataReadTable != null)
            {
                curDY3000DY.DataReadTable.Clear();
                DataView vie = curDY3000DY.DataReadTable.DefaultView;
                vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
                DataTable dt = vie .ToTable ();
                ShowResult(dt , true);
            }
        }
        
            //{
            //    BackgroundWork = downdata,
            //    MessageInfo = "正在更新数据"
            //};
            //process.Start();
        private string endstr = "";
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
           
            if (e.Message == "end")
            {
                //Control.CheckForIllegalCrossThreadCalls = false;
                //c1FlexGrid1.DataSource = tl310new.DataReadTable;
                MessageBox.Show("共读取" + (tl310new.icount ) + "条数据！");
                DataView vie = tl310new.DataReadTable.DefaultView;
                DataTable dt = vie.ToTable();
                endstr = e.Message;
                ShowResult(dt, true);
            }
            else if (tagName == "TL310")
            {

                DataView vie = tl310new.DataReadTable.DefaultView;
                //    //vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
                DataTable dt = vie.ToTable();
                ShowResult(dt, true);
            }
            else
            {
                DataView vie = curDY3000DY.DataReadTable.DefaultView;
                //vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
                DataTable dt = vie.ToTable();
                ShowOther(dt, true);
            }
        }
        public void ShowOther(DataTable dtbl,bool cleared)
        {
            if (InvokeRequired)
                BeginInvoke(new InvokeDelegate(showOnControlo), dtbl);
            else
                showOnControlo(dtbl);
        }
        private void showOnControlo(DataTable dtbl)
        {
            if (dtbl == null || dtbl.Rows.Count == 0)
            {
                Cursor = Cursors.Default;
                btnReadHis.Enabled = true;
                btnClear.Enabled = true;
                return;
            }
            c1FlexGrid1.DataSource = dtbl;
            if (c1FlexGrid1.Cols["已保存"] != null)
            {
                c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }

            c1FlexGrid1.ScrollBars = ScrollBars.Both;
            c1FlexGrid1.Refresh();
            Cursor = Cursors.Default;
            btnReadHis.Enabled = true;
            btnClear.Enabled = true;
            btnReadAllData.Enabled = true;
        }

        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(DataTable dtbl,bool cleared)
        {
            //c1FlexGrid1.Invoke(new InvokeDelegate(showOnControl), dtbl);

            if (InvokeRequired)
                BeginInvoke(new InvokeDelegate(showOnControl), dtbl);
            else
                showOnControl(dtbl);
           
            //if (!cleared && dtbl.Rows.Count <= 0)
            //{
            //    string msg = "没有采集到相应数据,可能是仪器没有相应检测数据!";
            //    MessageBox.Show( msg, "无数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
       
        /// <summary>
        /// 把数据绑定数据控件显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void showOnControl(DataTable dtbl)
        {
            if (dtbl == null || dtbl.Rows.Count==0)
            {
                Cursor = Cursors.Default;
                btnReadHis.Enabled = true;
                btnClear.Enabled = true;
                return;
            }
                c1FlexGrid1.DataSource = dtbl;
 
                //if (c1FlexGrid1.Rows.Count > 1)
                //{
                //    if (tagName != "TL310")
                //    {
                //        for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                //        {
                //            //if (tagName == "TL310")
                //            //{
                //            //    strWhere.Length = 0;
                //            //    strWhere.AppendFormat(" HolesNum='{0}'", c1FlexGrid1.Rows[i]["通道号"].ToString());
                //            //    strWhere.AppendFormat(" AND MachineItemName='{0}'", c1FlexGrid1.Rows[i]["检测项目"].ToString());
                //            //    strWhere.AppendFormat(" AND CheckValueInfo='{0}'", c1FlexGrid1.Rows[i]["检测结果"].ToString());
                //            //    strWhere.AppendFormat(" AND CheckStartDate=#{0}#", c1FlexGrid1.Rows[i]["检测时间"].ToString());
                //            //    c1FlexGrid1.Rows[i]["已保存"] = _resultBll.IsExist(strWhere.ToString());

                //            //}
                //            //else
                //            //{
                //                c1FlexGrid1.Cols["数据可疑性"].Visible = false;
                //                //可疑数据标红处理
                //                if (c1FlexGrid1.Rows[i]["数据可疑性"].ToString().Equals("是"))
                //                    c1FlexGrid1.SetCellStyle(i, 3, style1);
                //                else
                //                    c1FlexGrid1.SetCellStyle(i, 3, styleNormal);
                //                strWhere.Length = 0;
                //                strWhere.AppendFormat(" MachineSampleNum='{0}'", c1FlexGrid1.Rows[i]["编号"].ToString());
                //                strWhere.AppendFormat(" AND MachineItemName='{0}'", c1FlexGrid1.Rows[i]["检测项目"].ToString());
                //                strWhere.AppendFormat(" AND CheckStartDate=#{0}#", c1FlexGrid1.Rows[i]["检测时间"].ToString());
                //                c1FlexGrid1.Rows[i]["已保存"] = _resultBll.IsExist(strWhere.ToString());
                //                strWhere.Length = 0;
                //            //}
                //        }
                //    }
                //    if (c1FlexGrid1.Cols["已保存"] != null)
                //    {
                //        c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                //        c1FlexGrid1.Cols["已保存"].AllowDragging = false;
                //    }
                //}
            if (c1FlexGrid1.Cols["已保存"] != null)
            {
                c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }
           
            c1FlexGrid1.ScrollBars = ScrollBars.Both;
           
           
            c1FlexGrid1.Refresh();
            if (endstr == "end")
            {
                endstr = "";
                Cursor = Cursors.Default;
            }
            btnReadHis.Enabled = true;
            btnClear.Enabled = true;
            btnReadAllData.Enabled = true;
        }

    }
}
