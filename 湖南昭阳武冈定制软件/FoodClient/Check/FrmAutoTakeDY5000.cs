using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO.Ports;

using DY.FoodClientLib;

namespace FoodClient
{
    public partial class FrmAutoTakeDY5000 : FrmAutoTakeDY
    {
        private clsDY5000 curDY5000;//旧DY5000版本
        private string tagName = string.Empty;
        private StringBuilder strWhere = new StringBuilder();
        private Button btnReadNow;





        public FrmAutoTakeDY5000(string tag)
            : base(tag)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FrmAutoTake5000_Load);

			//btnReadNow = new Button();
			//this.btnReadNow.Location = new System.Drawing.Point(59, 487);
			//this.btnReadNow.Name = "btnReadNow";
			//this.btnReadNow.Size = new System.Drawing.Size(120, 24);
			//this.btnReadNow.TabIndex = 0;
			//this.btnReadNow.Text = "读取当前检测数据";
			//this.btnReadNow.Click += new System.EventHandler(this.btnReadNow_Click);
			//this.Controls.Add(btnReadNow);

            tagName = tag;
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
            
        }



        private void FrmAutoTake5000_Load(object sender, EventArgs e)
        {
            this.BindCheckItem();
            base.BindInit();
        }



        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(tagName);//代表老版本DY5000
            try
            {
                curDY5000 = new clsDY5000();
				
                curDY5000.Open();
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }


        protected override void winClose()
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }

            if (curDY5000 != null)
            {
                curDY5000.Close();
            }
            base.winClose();
        }
        
		
		protected override void CheckRowState()
        {
            int rows = c1FlexGrid1.RowSel;
            if (rows >= 1)
            {
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[rows]["检测值"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[rows]["单位"].ToString();
                string strItem = c1FlexGrid1.Rows[rows]["项目"].ToString();

                lblHolesNum.Text = c1FlexGrid1.Rows[rows]["孔位"].ToString();
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = string.Empty;// c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["样
            }
        }
       
		
		protected override void btnClear_Click(object sender, EventArgs e)
        {
            strWhere.Length = 0;
            if (curDY5000.DataReadTable != null)
            {
                curDY5000.DataReadTable.Clear();
                ShowResult(curDY5000.DataReadTable);
            }
            //curDY5000.ClearData();
            //base.btnClear_Click(sender, e);

        }

        protected override void btnReadHistory_Click(object sender, EventArgs e)
        {
            int dtSpan = dtStart.Value.Year - 2000;
            string dt = dtSpan.ToString("00") + dtStart.Value.Month.ToString("00") + dtStart.Value.Day.ToString("00");
			int endYear = dtEnd.Value.Year - 2000;
			string endDate = endYear.ToString("00") + dtEnd.Value.Month.ToString("00") + dtEnd.Value.Day.ToString("00");
			curDY5000.ReadHistory(dt, endDate);
            //base.btnReadHistory_Click(sender, e);
        }
       
		
		/// <summary>
        /// 读取当前数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadNow_Click(object sender, System.EventArgs e)
        {
            curDY5000.ReadNow();
        }
      
		
		/// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.Read5000Data && e.Message.Equals("OK"))
            {
                ShowResult(curDY5000.DataReadTable);
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
                BeginInvoke(new InvokeDelegate(ShowOnControl), dtbl);
            }
            else
            {
                ShowOnControl(dtbl);
            }
        }


        /// <summary>
        /// 显示历史结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowOnControl(DataTable dtbl)
        {
			//if (dtbl != null)
			//{
			//    strWhere.Length = 0;
			//    for (int i = 0; i < dtbl.Rows.Count; i++)
			//    {
			//        strWhere.AppendFormat(" checkMachine='010' AND HolesNum='{0}'", dtbl.Rows[i]["孔位"].ToString());
			//        strWhere.AppendFormat(" AND MachineItemName='{0}'", dtbl.Rows[i]["项目"].ToString());
			//        strWhere.AppendFormat(" AND CheckStartDate=#{0}#", dtbl.Rows[i]["检测时间"].ToString());
			//        dtbl.Rows[i]["已保存"] = resultBll.IsExist(strWhere.ToString());
			//        strWhere.Length = 0;
			//    }
			//}

			//if (dtbl.Rows.Count > 10)
			//{
			//    dtbl.Rows.RemoveAt(dtbl.Rows.Count - 1);
			//}
            base.c1FlexGrid1.DataSource = dtbl;
		
			base.c1FlexGrid1.Refresh();
			//base.c1FlexGrid1.AutoSizeCols();
			
   
            if (c1FlexGrid1.Cols["已保存"] != null)
            {
                c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                c1FlexGrid1.Cols["已保存"].AllowDragging = false;
            }
        }
    }
}
