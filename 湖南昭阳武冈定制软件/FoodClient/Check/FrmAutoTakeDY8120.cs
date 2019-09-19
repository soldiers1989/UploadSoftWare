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
    /// <summary>
    /// DY-8120仪器数据显示窗口
    /// </summary>
    public class FrmAutoTakeDY8120 : FrmAutoTakeDY
    {
        private string tagName = string.Empty;
       // private clsDY8120 dy8120;

        public FrmAutoTakeDY8120(string tag)
            : base(tag)
        {
            this.tagName = tag;

            this.Load += new System.EventHandler(this.FrmAutoTakeDY8120_Load);

            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
        }

        private void FrmAutoTakeDY8120_Load(object sender, EventArgs e)
        {
            base.dtStart.Visible = true;
            base.dtEnd.Visible = true;
            base.lblFrom.Text = "  从";
            base.btnReadHis.Text = "读取数据";
            base.btnReadHis.Location = new System.Drawing.Point(29, 540);
            base.btnReadHis.Size = new System.Drawing.Size(90, 24);

            base.btnClear.Text = "清除列表";
            base.btnClear.Location = new System.Drawing.Point(175, 540);
            base.btnClear.Size = new System.Drawing.Size(70, 24);

            this.BindCheckItem();
            base.BindInit();
        }

        private void BindCheckItem()
        {
            try
            {
                CommonOperation.GetMachineSetting(tagName);
                //不是真实读取仪器数据，先屏蔽
                //dy8120 = new clsDY8120();
                //if (!dy8120.Online)
                //{
                //    dy8120.Open();
                //}
                base._checkItems = StringUtil.GetAry(ShareOption.DefaultCheckItemCode);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
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
            DateTime dtstr = dtStart.Value;
            DateTime dten = dtEnd.Value;
            DateTime dtnow = DateTime.Now;
            if (dtstr > dtnow)
            {
                dtstr = dtnow;
            }
            if (dtstr >= dten)
            {
                dten = dtstr.AddDays(1).AddSeconds(-1);
            }
            if (dten > dtnow)
            {
                dten = dtnow;
            }
            //测试数据
            TestData();
            //dy8120.ReadHistory(dtstr, dten);//从仪器采集数据
        }

        //以下变量为临数据使用
        private StringBuilder strWhere = new StringBuilder();
        DataTable checkDtbl = null;
        string testFile = AppDomain.CurrentDomain.BaseDirectory + "TestData.xml";

        private void TestData()
        {
            if (checkDtbl == null)
            {
                checkDtbl = new DataTable("checkDtbl");
                DataColumn dataCol;
                ////////////新增
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "编号";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "项目";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);
            }
            DataRow dr;
            //XmlDocument doc = new XmlDocument();
            //doc.Load(testFile);

            DataSet dst = new DataSet();
            dst.ReadXml(testFile);
            DataTable dtbl = null;
            if (dst != null)
            {
                dtbl = dst.Tables[0];
            }
            if (dtbl != null)
            {
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    strWhere.Append(" checkMachine='020'");
                    strWhere.AppendFormat(" AND MachineSampleNum='{0}'", dtbl.Rows[i]["id"].ToString());
                    strWhere.AppendFormat(" AND MachineItemName='{0}'", dtbl.Rows[i]["item"].ToString());
                    strWhere.AppendFormat(" AND CheckStartDate=#{0}#", dtbl.Rows[i]["checkTime"].ToString());

                    dr = checkDtbl.NewRow();
                    dr["已保存"] = _resultBll.IsExist(strWhere.ToString());
                    dr["编号"] = dtbl.Rows[i]["id"].ToString();
                    dr["项目"] = dtbl.Rows[i]["item"].ToString();
                    dr["检测值"] = dtbl.Rows[i]["checkValue"].ToString();
                    dr["单位"] = dtbl.Rows[i]["unit"].ToString();
                    dr["检测时间"] = dtbl.Rows[i]["checkTime"].ToString();
                    checkDtbl.Rows.Add(dr);
                    strWhere.Length = 0;
                }
            }
            ShowResult(checkDtbl);
        }

        protected override void CheckRowState()
        {
            int rows = c1FlexGrid1.RowSel;
            object obj = c1FlexGrid1.Rows[rows]["已保存"];
            if (obj != null && Convert.ToBoolean(obj))
            {
                MessageBox.Show("此数据已经保存过");
                return;
            }
            if (rows >= 1)
            {
                string strItem = string.Empty;
                txtCheckValueInfo.Text = c1FlexGrid1.Rows[rows]["检测值"].ToString();
                txtResultInfo.Text = c1FlexGrid1.Rows[rows]["单位"].ToString();//没有检测单位
                strItem = c1FlexGrid1.Rows[rows]["项目"].ToString();

                ////////////////////// 
                lblHolesNum.Text = "";//无孔位的概念
                lblMachineItemName.Text = strItem;
                lblMachineSampleNum.Text = c1FlexGrid1.Rows[rows]["编号"].ToString();
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
            //if (dy8120 != null)
            //{
            //    dy8120.Close();
            //    dy8120 = null;
            //}
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
            //if (dy6400.checkDtbl != null)
            //{
            //    dy6400.checkDtbl.Clear();
            //    ShowResult(dy6400.checkDtbl);
            //}
            if (checkDtbl != null)
            {
                checkDtbl.Clear();
                ShowResult(checkDtbl);
                //checkDtbl = null;
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
                if (e.Message.Equals("OK"))
                {
                    //ShowResult(dy8120.checkDtbl);
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
            base.btnReadHis.Enabled = true;
            base.btnClear.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
