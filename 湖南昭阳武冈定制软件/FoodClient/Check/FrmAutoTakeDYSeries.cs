using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeDYSeries : FrmAutoTakeDY // partial
    {
        private clsDY3000DY _curDY3000DY = null;
        private StringBuilder _strWhere = new StringBuilder();
        private string _tagName = string.Empty;

        public FrmAutoTakeDYSeries(string tag)
            : base(tag)
        {
            _tagName = tag + "DY";//注意标识码后缀多了“DY”
            this.Load += new System.EventHandler(this.FrmAutoTakeDYSeries_Load);
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeDYSeries_Load(object sender, System.EventArgs e)
        {
            dtEnd.Visible = false;
            lblTo.Visible = false;
            btnReadHis.Location = new Point(157, 514);
            btnClear.Location = new Point(220, 514);
            if (_tagName.Equals("DY1000DY"))
                lblSuppresser.Text = "抑制率：";
            this.BindCheckItem();
            base.BindInit();
        }

        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(_tagName);
            try
            {
                _curDY3000DY = new clsDY3000DY();
                if (!_curDY3000DY.Online)
                    _curDY3000DY.Open();
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _checkItems = StringUtil.GetDY3000DYAry(ShareOption.DefaultCheckItemCode);
            clsDY3000DY.CheckItemsArray = _checkItems;
        }

        protected override void btnReadHistory_Click(object sender, System.EventArgs e)
        {
            bool blOnline = _curDY3000DY.Online;
            if (!blOnline)
            {
                MessageBox.Show(this, "无法与仪器正常通讯，请检查！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                return;
            }
            Cursor = Cursors.WaitCursor;
            btnReadHis.Enabled = false;
            btnClear.Enabled = false;
            dtEnd.Value = dtStart.Value.Date.AddDays(1).AddSeconds(-1);
            _curDY3000DY.ReadHistory(dtStart.Value.Date, dtEnd.Value);
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
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测值"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["单位"].ToString();
                strItem = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["项目"].ToString();

                lblHolesNum.Text = "";//无孔位的概念
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["编号"].ToString();
            }
        }

        protected override void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }
            try
            {
                if (_curDY3000DY != null)
                {
                    if (_curDY3000DY.Online)
                    {
                        _curDY3000DY.Close();
                    }
                    _curDY3000DY = null;
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
                //return;
            }
            //this.Dispose();
            base.winClose();
        }

        protected override void btnClear_Click(object sender, EventArgs e)
        {
            _strWhere.Length = 0;
            ClearData();
            //curDY3000DY.ClearData();
            // base.btnClear_Click(sender, e);
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearData()
        {
            if (_curDY3000DY.DataReadTable != null)
            {
                _curDY3000DY.DataReadTable.Clear();
                ShowResult(_curDY3000DY.DataReadTable);
            }
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.ReadDY3000DYData)
            {
                ShowResult(_curDY3000DY.DataReadTable);
            }
        }

        /// <summary>
        /// 委托回调
        /// </summary>
        /// <param name="s"></param>
        private delegate void InvokeDelegate(DataTable dtbl);

        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(DataTable dtbl)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new InvokeDelegate(showOnControl), dtbl);
            }
            else
            {
                showOnControl(dtbl);
            }
        }

        /// <summary>
        /// 把数据绑定数据控件显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void showOnControl(DataTable dtbl)
        {
            if (dtbl == null)
                return;

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
                        {
                            c1FlexGrid1.SetCellStyle(i, 3, style1);
                        }
                        else
                        {
                            c1FlexGrid1.SetCellStyle(i, 3, styleNormal);
                        }
                        _strWhere.Length = 0;
                        _strWhere.AppendFormat(" MachineSampleNum='{0}'", c1FlexGrid1.Rows[i]["编号"].ToString());
                        _strWhere.AppendFormat(" AND MachineItemName='{0}'", c1FlexGrid1.Rows[i]["项目"].ToString());
                        _strWhere.AppendFormat(" AND CheckStartDate=#{0}#", c1FlexGrid1.Rows[i]["检测时间"].ToString());
                        c1FlexGrid1.Rows[i]["已保存"] = _resultBll.IsExist(_strWhere.ToString());
                        _strWhere.Length = 0;
                    }
                    /////////////////////新增
                    if (c1FlexGrid1.Cols["已保存"] != null)
                    {
                        c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                        c1FlexGrid1.Cols["已保存"].AllowDragging = false;
                    }
                    //c1FlexGrid1.AutoSizeCols();
                }
                c1FlexGrid1.AutoSizeCols();
            }
            Cursor = Cursors.Default;
            btnReadHis.Enabled = true;
            btnClear.Enabled = true;
        }

    }
}