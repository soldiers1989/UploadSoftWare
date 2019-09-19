using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeDY723PC : FrmAutoTakeDY
    {
        private clsDY723PC dy723pc;
        private string tagName = string.Empty;
        private bool flag = false;
        /// <summary>
        /// 用于标识不同的传输方式
        /// wince通讯模式. 1表示串口通讯,2表示FTP方式，3表示共享模式
        /// </summary>
        private int mode = 1;

        public FrmAutoTakeDY723PC(string tag)
            : base(tag)
        {
            this.tagName = tag;

            this.Load += new System.EventHandler(this.FrmAutoTakeDY723PC_Load);

            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeDY723PC_Load(object sender, EventArgs e)
        {
            base.dtStart.Visible = true;
            base.dtEnd.Visible = true;
            base.lblFrom.Text = "  从";
            base.btnReadHis.Text = "读取数据";
            base.btnReadHis.Location = new System.Drawing.Point(29, 540);
            base.btnReadHis.Size = new System.Drawing.Size(90, 24);
            base.btnReadHis.Focus();

            base.btnClear.Text = "清除列表";
            base.btnClear.Location = new System.Drawing.Point(160, 540);
            base.btnClear.Size = new System.Drawing.Size(70, 24);

            object obj = System.Configuration.ConfigurationManager.AppSettings["TransferMode"];
            if (obj != null)
            {
                mode = Convert.ToInt32(obj);
            }

            this.BindCheckItem();
            base.BindInit();
        }


        private void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(tagName);
            dy723pc = new clsDY723PC();

            if (mode == 1)//串口方式
            {
                try
                {
                    if (!dy723pc.Online)
                    {
                        dy723pc.Open();
                    }
                }
                catch (JH.CommBase.CommPortException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }
        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            dy723pc.ClearData();
            base.c1FlexGrid1.DataSource = null;
            //if (dy723pc.checkDtbl != null)
            //{
            //    dy723pc.checkDtbl.Clear();
            //    base.c1FlexGrid1.DataSource = null;
            //    base.c1FlexGrid1.Focus();

            //    //ShowResult(dy723pc.checkDtbl);

            //    //base.c1FlexGrid1.AutoResize = true;
            //    //base.c1FlexGrid1.AutoSizeCols();
            //}
        }

        /// <summary>
        /// 读取检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == 1)//串口方式
                {
                    if (dy723pc != null && !dy723pc.Online)
                    {
                        MessageBox.Show("串口连接有误!");
                        return;
                    }
                }

                DateTime dtstr = dtStart.Value;
                DateTime dten = dtEnd.Value;
                DateTime dtnow = DateTime.Now;

                if (dtstr > dten)
                {
                    MessageBox.Show("起始时间不能大于截止时间");
                    return;
                    //dten = dtstr.AddDays(1).AddSeconds(-1);
                }
                base.btnReadHis.Enabled = false;
                base.btnClear.Enabled = false;
                //if (dtstr.Date > dtnow)
                //{
                //    dtstr = dtnow;
                //}
                //if (dten.Date > dtnow)
                //{
                //    dten = dtnow;
                //}
                Cursor.Current = Cursors.WaitCursor;

                dy723pc.ReadHistory(dtstr, dten, mode);
            }
            catch (NullReferenceException nfex)
            {
                MessageBox.Show("存在串口问题：" + nfex.Message);
            }
        }

        protected override void CheckRowState()
        {
            int row = c1FlexGrid1.RowSel;
            object obj = c1FlexGrid1.Rows[row]["已保存"];
            if (obj != null && Convert.ToBoolean(obj))
            {
                MessageBox.Show("此数据已经保存过");
                return;
            }

            if (row >= 1)
            {
                string strItem = string.Empty;
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[row]["检测值"].ToString();//定量结果
                txtResultInfo.Text = c1FlexGrid1.Rows[row]["单位"].ToString();
                strItem = c1FlexGrid1.Rows[row]["项目"].ToString();

                ///////////////////////////新增
                lblHolesNum.Text = c1FlexGrid1.Rows[row]["孔位"].ToString();
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[row]["样本号"].ToString();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        protected override void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }
            //dy723pc.checkDtbl.Clear();
            dy723pc.ClearData();
            if (dy723pc != null && mode == 1)
            {
                dy723pc.Close();
            }
            base.winClose();
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.ReadDY723PCData)
            {
                if (e.Message.Equals("OK"))
                {
                    flag = false;
                    ShowResult(dy723pc.checkDtbl);
                }
                else
                {
                    //ShowResult(dy723pc.checkDtbl);
                    if (!flag)
                    {
                        flag = true;
                        MessageBox.Show(e.Message);
                    }
                }
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
            try
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
            catch (FormatException fex)
            {
                MessageBox.Show(fex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统错误:" + ex.Message);
            }
        }

        /// <summary>
        /// 显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void showOnControl(DataTable dtbl)
        {
            Cursor.Current = Cursors.Default;
            base.c1FlexGrid1.DataSource = dtbl;
            if (base.c1FlexGrid1 != null && base.c1FlexGrid1.Cols["已保存"] != null)
            {
                base.c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                base.c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }

            base.btnReadHis.Enabled = true;
            base.btnClear.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
