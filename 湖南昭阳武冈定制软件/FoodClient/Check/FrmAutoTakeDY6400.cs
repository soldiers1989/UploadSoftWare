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
    /// <summary>
    /// 新版肉类水分仪
    /// </summary>
    public class FrmAutoTakeDY6400 : FrmAutoTakeDY
    {
        private clsDY6400 dy6400;
        private string tagName = string.Empty;
        private System.Windows.Forms.Button btnMachineDel;

        public FrmAutoTakeDY6400(string tag)
            : base(tag)
        {
            this.tagName = tag;

            this.Load += new System.EventHandler(this.FrmAutoTakeDY6400_Load);

            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;

            this.btnMachineDel = new System.Windows.Forms.Button();
            this.btnMachineDel.Location = new System.Drawing.Point(200, 514);
            this.btnMachineDel.Name = "btnMachineDel";
            this.btnMachineDel.Size = new System.Drawing.Size(91, 24);
            this.btnMachineDel.TabIndex = 478;
            this.btnMachineDel.Text = "清除仪器数据";
            this.btnMachineDel.Click += new System.EventHandler(this.btnMachineDel_Click);
            this.Controls.Add(btnMachineDel);

        }

        private void FrmAutoTakeDY6400_Load(object sender, EventArgs e)
        {
            base.lblFrom.Visible = false;
            base.dtStart.Visible = false;
            base.dtEnd.Visible = false;
            base.lblTo.Visible = false;
            base.btnReadHis.Location = new Point(67, 514);//107
            base.btnClear.Location = new Point(130, 514);//170

            this.BindCheckItem();
            base.BindInit();
        }

        protected void BindCheckItem()
        {
            try
            {
                CommonOperation.GetMachineSetting(tagName);
                dy6400 = new clsDY6400();
                if (!dy6400.Online)
                {
                    dy6400.Open();
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
        /// 读取检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            base.btnReadHis.Enabled = false;
            base.btnClear.Enabled = false;
            this.btnMachineDel.Enabled = false;
            dy6400.ReadHistory();
        }

        protected override void CheckRowState()
        {
            base.cmbFood.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["种类"].ToString();
            base._foodSelectedValue = clsFoodClassOpr.CodeFromName(cmbFood.Text, _checkItemCode);
            string[] resultArry = clsFoodClassOpr.ValueFromCode(_foodSelectedValue, _checkItemCode);
            if (resultArry != null)
            {
                base._sign = resultArry[0];
                base._dTestValue = Convert.ToDecimal(resultArry[1]);
                base._checkUnit = resultArry[2];
                base.txtStandValue.Text = resultArry[1];
            }
            int currentRow = c1FlexGrid1.RowSel;
            base.txtCheckValueInfo.Text = c1FlexGrid1.Rows[currentRow]["检测值"].ToString();
            base.lblHolesNum.Text = "";//无孔位的概念
            base.lblMachineItemName.Text = c1FlexGrid1.Rows[currentRow]["种类"].ToString();
            base.lblMachineSampleNum.Text = c1FlexGrid1.Rows[currentRow]["编号"].ToString();
            base.dtpCheckStartDate.Value = DateTime.Parse(c1FlexGrid1.Rows[currentRow]["检测时间"].ToString());
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
            if (dy6400 != null)
            {
                dy6400.Close();
                dy6400 = null;
            }
            base.winClose();
        }
        /// <summary>
        /// 清除仪器数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMachineDel_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            if (MessageBox.Show(this, "是否确定删除仪器数据？如果确定，仪器数据将被删除且永远不可恢复", "提示", buttons, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                dy6400.ClearCache();//删除仪器数据
            }
        }
        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnClear_Click(object sender, EventArgs e)
        {
            //dy6400.ClearCache();//删除仪器数据

            if (dy6400.checkDtbl != null)
            {
                dy6400.checkDtbl.Clear();
                ShowResult(dy6400.checkDtbl);
            }
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.ReadDY6400Data)
            {
                this.Cursor = Cursors.Default;
                base.btnReadHis.Enabled = true;
                base.btnClear.Enabled = true;
                this.btnMachineDel.Enabled = true;
                if (e.Message.Equals("OK"))
                {
                    ShowResult(dy6400.checkDtbl);
                }
                if (e.Message.Equals(""))
                {
                    MessageBox.Show("暂无数据!");
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
                MessageBox.Show("系统错误:"+ex.Message);
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
            base.btnReadHis.Enabled = true;
            base.btnClear.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
