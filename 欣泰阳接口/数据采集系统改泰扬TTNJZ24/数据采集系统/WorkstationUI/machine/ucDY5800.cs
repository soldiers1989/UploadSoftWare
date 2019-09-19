﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationModel.Instrument;
using WorkstationModel.Model;
using WorkstationDAL.Model;

namespace WorkstationUI.machine
{
    public partial class ucDY5800 : UserControl
    {
        private clsSetSqlData sql = new clsSetSqlData();
        private clsDY5000 curDY5000;//旧DY5000版本       
        private StringBuilder strWhere = new StringBuilder();
        public string[,] _checkItems;

        public ucDY5800()
        {
            InitializeComponent();
        }

        private void BtnReadHis_Click(object sender, EventArgs e)
        {
            if (!curDY5000.Online)
            {
                MessageBox.Show("串口连接有误!");
                return;
            }
            int dtSpan = DTPStart.Value.Year - 2000;
            string dt = dtSpan.ToString("00") + DTPStart.Value.Month.ToString("00") + DTPStart.Value.Day.ToString("00");
            int endYear = DTPEnd.Value.Year - 2000;
            string endDate = endYear.ToString("00") + DTPEnd.Value.Month.ToString("00") + DTPEnd.Value.Day.ToString("00");
            curDY5000.ReadHistory(dt, endDate);
        }

        private void ucDY5800_Load(object sender, EventArgs e)
        {
            BindCheckItem();
            clsMessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }
        /// <summary>
        /// 查询检测项目
        /// </summary>
        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting("DY-5000食品安全综合分析仪");//代表老版本DY5000
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
            _checkItems = clsStringUtil.GetAry(clsShareOption.DefaultCheckItemCode);
        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, clsMessageNotify.NotifyEventArgs e)
        {
            if (e.Code == clsMessageNotify.NotifyInfo.Read5000Data && e.Message.Equals("OK"))
            {
                ShowResult(curDY5000._dataReadTable);
            }
        }
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

            CheckDatas.DataSource = dtbl;
            CheckDatas.Refresh();
            //base.c1FlexGrid1.AutoSizeCols();
            if (CheckDatas.Columns["已保存"] != null)
            {
                CheckDatas.Columns["已保存"].DefaultCellStyle.BackColor = Color.LightGray;
                //CheckDatas.Columns["已保存"].AllowDragging = false;
            }
        }

        /// <summary>
        /// 委托回调
        /// </summary>
        /// <param name="s"></param>
        private delegate void InvokeDelegate(DataTable dtbl);
        protected void winClose()
        {
            if (clsMessageNotify.Instance() != null)
            {
                clsMessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent;
            }

            if (curDY5000 != null)
            {
                curDY5000.Close();
            }
            this.Dispose();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            curDY5000._dataReadTable.Clear();
            CheckDatas.DataSource = curDY5000._dataReadTable;
        }

        private void BtnReadHis_Click_1(object sender, EventArgs e)
        {

        }
    }
}
