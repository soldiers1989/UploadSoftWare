using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeDYSeries : FrmAutoTakeDY
    {
        private clsDY3000DY curDY3000DY = null;
        private StringBuilder strWhere = new StringBuilder();
        private string tagName = string.Empty;
        private delegate void InvokeDelegate(DataTable dtbl);

        public FrmAutoTakeDYSeries(string tag)
            : base(tag)
        {
           // InitializeComponent();
            tagName = tag + "DY";//注意标识码后缀多了“DY”
            this.Load += new System.EventHandler(this.FrmAutoTakeDYSeries_Load);
            //MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
        }

        private void FrmAutoTakeDYSeries_Load(object sender, System.EventArgs e)
        {
            dtEnd.Visible = false;
            lblTo.Visible = false;
            btnReadHis.Location = new Point(157, 543);
            btnClear.Location = new Point(230, 543);
            if(tagName.Equals("DY1000DY"))
                lblSuppresser.Text = "抑制率：";
            this.BindCheckItem();
            base.BindInit();
        }

        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(tagName);
            try
            {
                curDY3000DY = new clsDY3000DY();
                if (!curDY3000DY.Online)
                    curDY3000DY.Open();
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

        protected override void btnReadHistory_Click(object sender, System.EventArgs e)
        {
            bool blOnline = false;
            try
            {
                blOnline = curDY3000DY.Online;
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
                btnReadHis.Enabled = false;
                btnClear.Enabled = false;
                dtEnd.Value = dtStart.Value.Date.AddDays(1).AddSeconds(-1);
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

        protected override void CheckRowState()
        {
            object obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["已保存"];
            if (obj != null && Convert.ToBoolean(obj))
            {
                MessageBox.Show("此数据已经保存过");
                return;
            }

            if (c1FlexGrid1.RowSel >= 1)
            {
                obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["数据可疑性"];
                if (obj.ToString().Equals("是"))
                {
                    DialogResult dr = MessageBox.Show(this, "这是可疑检测记录，是否真要采集数据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                        return;
                }
                string strItem = string.Empty;
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["抑制率"].ToString().Trim ();
                txtResultInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["单位"].ToString().Trim();
                strItem = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测项目"].ToString();
                lblHolesNum.Text = "";
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["编号"].ToString();
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
            }
            catch { }
            base.winClose();
        }

        /// <summary>
        /// 清空
        /// 2016年9月21日 wenj 修改清空数据的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            curDY3000DY.DataReadTable.Clear();
            c1FlexGrid1.DataSource = curDY3000DY.DataReadTable;
            c1FlexGrid1.AutoSizeCols();
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

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            DataView vie = curDY3000DY.DataReadTable.DefaultView;
            vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
            DataTable dt = vie.ToTable();
            ShowResult(dt, true);
        }

        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(DataTable dtbl,bool cleared)
        {
        	if (InvokeRequired)
                BeginInvoke(new InvokeDelegate(showOnControl), dtbl);
			else
                showOnControl(dtbl);

			if (!cleared && dtbl.Rows.Count <= 0)
			{
                string msg = "没有采集到相应数据,可能是仪器没有相应检测数据!";
                MessageBox.Show( msg, "无数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
        }

        /// <summary>
        /// 把数据绑定数据控件显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void showOnControl(DataTable dtbl)
        {
            if (dtbl == null)
            {
                Cursor = Cursors.Default;
                btnReadHis.Enabled = true;
                btnClear.Enabled = true;
                return;
            }
            DataView dv = null;
            if (dtbl.Rows.Count > 0)
            {
                dv = dtbl.DefaultView;
                dv.Sort = "检测时间 ASC";
                c1FlexGrid1.DataSource = dv;
                c1FlexGrid1.Cols["数据可疑性"].Visible = false;

                if (c1FlexGrid1.Rows.Count > 1)
                {
                    for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
                    {
                        //可疑数据标红处理
                        if (c1FlexGrid1.Rows[i]["数据可疑性"].ToString().Equals("是"))
                            c1FlexGrid1.SetCellStyle(i, 3, style1);
                        else
                            c1FlexGrid1.SetCellStyle(i, 3, styleNormal);
                        strWhere.Length = 0;
                        strWhere.AppendFormat(" MachineSampleNum='{0}'", c1FlexGrid1.Rows[i]["编号"].ToString());
                        strWhere.AppendFormat(" AND MachineItemName='{0}'", c1FlexGrid1.Rows[i]["检测项目"].ToString());
                        strWhere.AppendFormat(" AND CheckStartDate=#{0}#", c1FlexGrid1.Rows[i]["检测时间"].ToString());
                        c1FlexGrid1.Rows[i]["已保存"] = _resultBll.IsExist(strWhere.ToString());
                        strWhere.Length = 0;
                    }
                    if (c1FlexGrid1.Cols["已保存"] != null)
                    {
                        c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                        c1FlexGrid1.Cols["已保存"].AllowDragging = false;
                    }
                }
                c1FlexGrid1.AutoSizeCols();
            }
            c1FlexGrid1.AutoSizeCols();
            Cursor = Cursors.Default;
            btnReadHis.Enabled = true;
            btnClear.Enabled = true;
        }

    }
}
