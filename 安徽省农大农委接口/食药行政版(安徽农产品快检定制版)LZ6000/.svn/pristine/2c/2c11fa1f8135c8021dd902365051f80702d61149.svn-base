using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public class FrmAutoTakeSFY : FrmAutoTakeDY //partial 
    {
        private clsDYRSY2 curDYRSY2;
        /// <summary>
        /// 标识是否第三代新版本
        /// </summary>
        public bool IsNewVersion = false;
        private string tagName = string.Empty;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.DateTimePicker dtpSet;
        public FrmAutoTakeSFY(string tag)
            : base(tag)
        {
            //InitializeComponent();
            this.dtpSet = new System.Windows.Forms.DateTimePicker();
            this.dtpSet.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dtpSet.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSet.Location = new System.Drawing.Point(40, 545);//56
            this.dtpSet.Name = "dtpSet";
            this.dtpSet.ShowUpDown = true;
            this.dtpSet.Size = new System.Drawing.Size(140, 21);
            this.dtpSet.TabIndex = 103;
            this.Controls.Add(this.dtpSet);

            this.btnSet = new System.Windows.Forms.Button();
            this.btnSet.Location = new System.Drawing.Point(190, 542);//222
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(56, 24);
            this.btnSet.TabIndex = 478;
            this.btnSet.Text = "设置";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            this.Controls.Add(this.btnSet);
            tagName = tag;
            this.Load += new System.EventHandler(this.FrmAutoTakeSFY_Load);
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeSFY_Load(object sender, EventArgs e)
        {
            dtEnd.Visible = false;
            dtStart.Visible = false;
            dtpSet.Visible = false;
            btnSet.Visible = false;
            lblFrom.Text = "注意：一定要等屏幕上显示End再进行数据处理";
            lblFrom.AutoSize = true;
            lblTo.Visible = false;
            lblSuppresser.Text = "水分含量：";
            base.btnReadHis.Location = new System.Drawing.Point(29, 571);
            base.btnClear.Location = new System.Drawing.Point(173, 571);
            BindCheckItem();
            base.BindInit();
        }

        protected void BindCheckItem()
        {
            try
            {
                CommonOperation.GetMachineSetting(tagName);//"DYRSY2"
                curDYRSY2 = new clsDYRSY2();
                if (!curDYRSY2.Online)
                {
                    curDYRSY2.Open();
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            curDYRSY2.NReadVersion();
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }

        protected override void CheckRowState()
        {
            cmbFood.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["种类"].ToString();
            _foodSelectedValue = clsFoodClassOpr.CodeFromName(cmbFood.Text, _checkItemCode);
            string[] result = clsFoodClassOpr.ValueFromCode(_foodSelectedValue, _checkItemCode);
            _sign = result[0];
            _dTestValue = Convert.ToDecimal(result[1]);
            _checkUnit = result[2];
            txtStandValue.Text = result[1];
            txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测值"].ToString();

            lblHolesNum.Text = "";//无孔位的概念
            lblMachineItemName.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["种类"].ToString();
            lblMachineSampleNum.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["编号"].ToString();
            if (IsNewVersion)
            {
                dtpCheckStartDate.Value = DateTime.Parse(c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["检测时间"].ToString());
            }
            //base.CheckRowState();
        }

        protected override void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }
            if (curDYRSY2 != null)
            {
                curDYRSY2.Close();
                curDYRSY2 = null;
            }
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
            curDYRSY2.checkDtbl.Clear();
            c1FlexGrid1.DataSource = curDYRSY2.checkDtbl;
            c1FlexGrid1.AutoSizeCols();

            //if (curDYRSY2.checkDtbl != null)
            //{
            //    curDYRSY2.checkDtbl.Clear();
            //    ShowResult(curDYRSY2.checkDtbl);
            //}
            // c1FlexGrid1.Clear();      
            //base.btnClear_Click(sender, e);
        }

        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnReadHis.Enabled = false;
            if (!IsNewVersion)//二代
            {
                curDYRSY2.ReadHistory();
            }
            else //三代
            {
                curDYRSY2.NReadHistory(dtStart.Value, dtEnd.Value);
            }
            // base.btnReadHistory_Click(sender, e);
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.ReadSFYData)
            {
                if (e.Message.Equals("V"))
                {
                    IsNewVersion = true;
                    SetState();
                }
                if (e.Message.Equals(string.Empty))
                {
                    ShowResult(curDYRSY2.checkDtbl);
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
        /// 显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        public void showOnControl(DataTable dtbl)
        {
            c1FlexGrid1.DataSource = dtbl;
            c1FlexGrid1.AutoSizeCols();
            if (c1FlexGrid1.Cols["已保存"] != null)
            {
                c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }
            if (c1FlexGrid1.Cols["检测时间"] != null && !IsNewVersion)
            {
                c1FlexGrid1.Cols["检测时间"].Visible = false;
            }
            btnReadHis.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        public void SetState()
        {
            c1FlexGrid1.Height = 496;
            if (IsNewVersion)
            {
                lblFrom.Top = 544;
                lblFrom.Text = "第三代水分仪";
                lblFrom.AutoSize = true;

                lblFrom.Visible = true;
                lblTo.Visible = true;
                dtpSet.Visible = false;
                btnSet.Visible = false;
                dtpSet.Value = System.DateTime.Now;

                dtStart.Visible = true;
                dtEnd.Visible = true;
                dtStart.Value = System.DateTime.Now;
                dtEnd.Value = System.DateTime.Now;
                c1FlexGrid1.Cols.Count = 4;
            }
            else
            {
                lblFrom.Top = 520;
                //lblFrom.Text = "";//注意：一定要等屏幕上显示End再进行数据处理
                lblFrom.AutoSize = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                dtpSet.Visible = false;
                btnSet.Visible = false;
                dtStart.Visible = false;
                dtEnd.Visible = false;
                //c1FlexGrid1.Cols.Count = 3;        
            }
        }
        private void btnSet_Click(object sender, System.EventArgs e)
        {
            if (IsNewVersion)
            {
                curDYRSY2.NSetTime(dtpSet.Value);
            }
        }

    }
}