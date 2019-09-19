﻿using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Threading;

namespace FoodClient
{
    public class FrmAutoTakeLZ4000T : FrmAutoTakeDY
    {
        private clsLZ4000T curLZ4000T = null;
        private StringBuilder strWhere = new StringBuilder();
        private string tagName = string.Empty;
        private delegate void InvokeDelegate(DataTable dtbl);
        private DateTime dts;//开始
        private DateTime dte;//结束

        public FrmAutoTakeLZ4000T(string tag)
            : base(tag)
        {
            tagName = tag + "LZ";
            this.Load += new System.EventHandler(this.FrmAutoTakeLZ4000T_Load);
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
        }

        private void FrmAutoTakeLZ4000T_Load(object sender, System.EventArgs e)
        {
            //dtEnd.Visible = false;
            //lblTo.Visible = false;
            //btnReadHis.Location = new Point(157, 543);
            //btnClear.Location = new Point(230, 543);
            txtChkUnit.Text = FoodClient.AnHui.Global.AnHuiInterface.instrument;
            txtChkUnitNum.Text = FoodClient.AnHui.Global.AnHuiInterface.instrumentNo;
            this.BindCheckItem();
            base.BindInit();
        }

        protected void BindCheckItem()
        {
            CommonOperation.GetMachineSetting(tagName);
            try
            {
                curLZ4000T = new clsLZ4000T();
                if (!curLZ4000T.Online)
                    curLZ4000T.Open();
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _checkItems = StringUtil.GetDY3000DYAry(ShareOption.DefaultCheckItemCode);
            clsLZ4000T.CheckItemsArray = _checkItems;
        }

        protected override void btnReadHistory_Click(object sender, System.EventArgs e)
        {
            TimeSpan ts = dtEnd.Value - dtStart.Value;
            if (ts.Days > 30)
            {
                MessageBox.Show("本次采集数据的日期间隔不能大于30天","读取数据",MessageBoxButtons.OK,MessageBoxIcon.Information );
                return;
            }
            bool blOnline = false;
            try
            {
                blOnline = curLZ4000T.Online;
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
                dts = dtStart.Value;//记录起始时间
                dte = dtEnd.Value;//记录结束时间
                //dtEnd.Value = dtStart.Value.Date.AddDays(1).AddSeconds(-1);
                //curLZ4000T.ReadHistory(dtStart.Value.Date);
                curLZ4000T.DataReadTable.Clear();
                curLZ4000T.ReadRecord(dtStart.Value.Date);
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
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["抑制率"].ToString().Trim();
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
                if (curLZ4000T != null)
                {
                    if (curLZ4000T.Online)
                        curLZ4000T.Close();
                    curLZ4000T = null;
                }
            }
            catch {  }
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
            curLZ4000T.DataReadTable.Clear();
            c1FlexGrid1.DataSource = curLZ4000T.DataReadTable;
            c1FlexGrid1.AutoSizeCols();

            //strWhere.Length = 0;
            //ClearData();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearData()
        {
            if (curLZ4000T.DataReadTable != null)
            {
                curLZ4000T.DataReadTable.Clear();
                DataView vie = curLZ4000T.DataReadTable.DefaultView;
                //vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
                DataTable dt = vie.ToTable();
                ShowResult(dt, true);
            }
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            //if(curLZ4000T.DataReadTable!=null && curLZ4000T.DataReadTable.Rows.Count>0)
            //{
            //    int s = curLZ4000T.DataReadTable.Rows.Count;
            //    Console.WriteLine(s);
            //}

            curLZ4000T.strShow = string.Empty;//清除缓存
            Thread.Sleep(50);//延时50毫秒让单片机休息再请求
            if (e.Message == "NoRecord")//没有记录
            {
                //继续读取下个日期的数据
                dts = dts.AddDays(1);
                if (dts < dte || dte == dts)
                {
                    curLZ4000T.ReadRecord(dts);
                }
                else
                {
                    DataView vie = curLZ4000T.DataReadTable.DefaultView;
                    DataTable dt = vie.ToTable();
                    ShowResult(dt, true);
                }
            }
            else if (e.Message == "Record")//有记录
            {
                curLZ4000T.ReadHistory(dts);
            }
            else
            {
                dts = dts.AddDays(1);
                if (dts < dte || dte == dts)
                {
                    curLZ4000T.ReadRecord(dts);
                }
                else
                {
                    
                    //ShowResult(curLZ4000T.DataReadTable, true);
                    DataView vie = curLZ4000T.DataReadTable.DefaultView;
                    DataTable dt = vie.ToTable();
                    ShowResult(dt, true);
                }
            }
            
            
            //DataView vie = curLZ4000T.DataReadTable.DefaultView;
            //vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
            //DataTable dt = vie.ToTable();
            //ShowResult(dt, true);
        }

        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(DataTable dtbl, bool cleared)
        {
            if (InvokeRequired)
                BeginInvoke(new InvokeDelegate(showOnControl), dtbl);
            else
                showOnControl(dtbl);

            if (!cleared && dtbl.Rows.Count <= 0)
            {
                string msg = "没有采集到相应数据,可能是仪器没有相应检测数据!";
                MessageBox.Show(msg, "无数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //DataView dv = null;
            if (dtbl.Rows.Count > 0)
            {
                //dv = dtbl.DefaultView;
                //dv.Sort = "检测时间 ASC";
                c1FlexGrid1.DataSource = dtbl;
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
            if (dte == dts || dte <dts)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("数据采集完成！","系统提示");
                btnReadHis.Enabled = true;
                btnClear.Enabled = true;

            }
        }

    }
}