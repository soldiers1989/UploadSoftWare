using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeDY6200 : FrmAutoTakeDY
    {
        private clsDY6200 dy6200;
        private string tagName = string.Empty;
        private System.Windows.Forms.Button btnMachineDel;
        public FrmAutoTakeDY6200(string tag)
            : base(tag)
        {
            this.tagName = tag;

            this.btnMachineDel = new System.Windows.Forms.Button();
            this.btnMachineDel.Location = new System.Drawing.Point(200, 540);
            this.btnMachineDel.Name = "btnMachineDel";
            this.btnMachineDel.Size = new System.Drawing.Size(91, 24);
            this.btnMachineDel.TabIndex = 478;
            this.btnMachineDel.Text = "清除仪器数据";
            this.btnMachineDel.Click += new System.EventHandler(this.btnMachineDel_Click);
            this.Controls.Add(btnMachineDel);

            this.Load += new System.EventHandler(this.FrmAutoTakeDY6200_Load);

            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeDY6200_Load(object sender, EventArgs e)
        {
            base.dtStart.Visible = true;
            base.dtEnd.Visible = true;
            base.lblFrom.Text = "  从";
            base.btnReadHis.Text = "读取数据";
            base.btnReadHis.Location = new System.Drawing.Point(29, 540);
            base.btnReadHis.Size = new System.Drawing.Size(90, 24);
            base.btnReadHis.Focus();

            base.btnClear.Text = "清除列表";
            base.btnClear.Location = new System.Drawing.Point(125, 540);
            base.btnClear.Size = new System.Drawing.Size(70, 24);

            this.BindCheckItem();
            base.BindInit();
        }

        private void BindCheckItem()
        {
            try
            {
                CommonOperation.GetMachineSetting(tagName);
                dy6200 = new clsDY6200();
                if (!dy6200.Online)
                {
                    dy6200.Open();
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }

        /// <summary>
        /// 清除仪器内数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMachineDel_Click(object sender, EventArgs e)
        {
            if (!dy6200.Online)
            {
                MessageBox.Show("串口连接有误!");
                return;
            }
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            if (MessageBox.Show(this, "是否确定删除仪器数据？如果确定，仪器数据将被删除且永远不可恢复", "提示", buttons, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                dy6200.ClearCache();
                if (dy6200.checkDtbl != null)
                {
                    dy6200.checkDtbl.Clear();
                    ShowResult(dy6200.checkDtbl);
                }
            }
        }

        /// <summary>
        /// 读取检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            if (!dy6200.Online)
            {
                MessageBox.Show("串口连接有误!");
                return;
            }

           // this.Cursor = Cursors.WaitCursor;
            base.btnReadHis.Enabled = false;
            base.btnClear.Enabled = false;
            this.btnMachineDel.Enabled = false;
            DateTime dtstr = dtStart.Value;
            DateTime dten = dtEnd.Value;
            DateTime dtnow = DateTime.Now;
            if (dtstr > dten)
            {
                MessageBox.Show("起始时间不能大于截止时间");
                return;
                //dten = dtstr.AddDays(1).AddSeconds(-1);
            }
            //if (dtstr > dtnow)
            //{
            //    dtstr = dtnow;
            //}
            //if (dten > dtnow)
            //{
            //    dten = dtnow;
            //}
            //if (dtstr >= dten)
            //{
            //    dten = dtstr.AddDays(1).AddSeconds(-1);
            //}
         
            dy6200.ReadHistory(dtstr, dten);
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
                //obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["数据可疑性"];
                //if (obj.ToString().Equals("是"))
                //{
                //    DialogResult dr = MessageBox.Show(this, "这是可疑检测记录，是否真要采集数据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dr == DialogResult.No)
                //    {
                //        return;
                //    }
                //}

                string strItem = string.Empty;
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测值"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["单位"].ToString();
                strItem = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["项目"].ToString();

                ////////////////////// 
                lblHolesNum.Text = "";//无孔位的概念
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["编号"].ToString();
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
            if (dy6200 != null)
            {
                dy6200.Close();
               
            }
            base.winClose();
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            //dy6400.ClearCache();//删除仪器数据

            if (dy6200.checkDtbl != null)
            {
                dy6200.checkDtbl.Clear();
                ShowResult(dy6200.checkDtbl);
            }
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.ReadDY6200Data)
            {
                //base.btnReadHis.Enabled = true;
                //base.btnClear.Enabled = true;
                //this.btnMachineDel.Enabled = true;
                if (e.Message.Equals("OK"))
                {
                    ShowResult(dy6200.checkDtbl);
                }
                else
                {
                    ShowResult(dy6200.checkDtbl);
                    MessageBox.Show(e.Message);
                
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
            base.c1FlexGrid1.DataSource = dtbl;
            base.c1FlexGrid1.AutoSizeCols();
            if (base.c1FlexGrid1.Cols["已保存"] != null)
            {
                base.c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                base.c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }
            this.btnMachineDel.Enabled = true;
            base.btnReadHis.Enabled = true;
            base.btnClear.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}