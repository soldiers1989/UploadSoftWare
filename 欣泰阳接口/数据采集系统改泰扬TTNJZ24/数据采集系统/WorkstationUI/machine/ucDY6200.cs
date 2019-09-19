using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using WorkstationUI.Basic;
using WorkstationModel.Model;
using WorkstationModel.Instrument;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;

namespace WorkstationUI.machine
{
    public partial class ucDY6200 : BasicContent
    {
        private  clsDY6200 dy6200 = new clsDY6200();
        private string tagName = string.Empty;
        protected string[,] _checkItems;
        private clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private ComboBox cmbAdd = new ComboBox();
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 3];

        public ucDY6200()
        {
            InitializeComponent();
        }

        private void ucDY6200_Load(object sender, EventArgs e)
        {
            this.LbTitle.Text = Global.ChkManchine;
            string err = string.Empty; 

            string[] Port = SerialPort.GetPortNames();
            if (Port.Length == 0)
            {
                cmbCOMbox.Items.Add("没有COM口");
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCOMbox.Items.Add(c);
            }
            cmbCOMbox.SelectedIndex = 0;

            cmbAdd.Visible = false;
            //cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.KeyUp += cmbAdd_KeyUp;
            cmbAdd.SelectedIndexChanged += cmbAdd_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbAdd);

            //检测项目
            cmbChkItem.Visible = false;
            //cmbChkItem.Items.Add("请选择...");
            cmbChkItem.Items.Add("以下相同");
            cmbChkItem.Items.Add("删除");
            cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbChkItem.SelectedIndex = 0;
            cmbChkItem.KeyUp += cmbChkItem_KeyUp;
            cmbChkItem.SelectedIndexChanged += cmbChkItem_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbChkItem);

            //样品名称
            cmbSample.Visible = false;
            //cmbSample.Items.Add("请选择...");
            cmbSample.Items.Add("以下相同");
            cmbSample.Items.Add("删除");
            //cmbSample.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbSample.SelectedIndex = 0;
            cmbSample.MouseClick += cmbSample_MouseClick;
            cmbSample.KeyUp += cmbSample_KeyUp;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbSample);

            BindCheckItem();
            clsMessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;

            try
            {
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null)
                {
                    if (cdt.Rows.Count > 0)
                    {
                        for (int n = 0; n < cdt.Rows.Count; n++)
                        {
                            if (cdt.Rows[n][9].ToString() == "是")
                            {
                                unitInfo[0, 0] = cdt.Rows[n][2].ToString();
                                unitInfo[0, 1] = cdt.Rows[n][3].ToString();
                                unitInfo[0, 2] = cdt.Rows[n][8].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text;
            //cmbAdd.Visible = false;
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbChkItem.Visible = false;
            //cmbSample.Visible = false;
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbAdd.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbAdd.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbChkItem.Text;
        }

        //首先加载进获取系统PAI函数的引用：
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();//重写系统API函数获取鼠标双击的有效间隔
        private DateTime dtCmbLastClick = DateTime.MinValue;//存储两次单击的时间间隔
        /// <summary>
        /// combox双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbSample_MouseClick(object sender, MouseEventArgs e)
        {
            string err = string.Empty;
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                try
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;
                    //string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                            string symbol = dt.Rows[0][4].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                dtCmbLastClick = DateTime.Now;
            }
        }
        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            string err = string.Empty;

            if (cmbSample.Text == "以下相同")
            {

                try
                {
                    for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                    {

                        CheckDatas.Rows[j].Cells[1].Value = CheckDatas.CurrentCell.Value.ToString();

                        string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[j].Cells[2].Value.ToString() + "'";

                        DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                CheckDatas.Rows[j].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                                CheckDatas.Rows[j].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                                string symbol = dt.Rows[0][4].ToString();
                                switch (symbol)
                                {
                                    case "≤":
                                        {
                                            if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                                Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "合格";
                                            }
                                            else
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                            }
                                        }
                                        break;
                                    case "<":
                                        {
                                            if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "合格";
                                            }
                                            else
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                            }
                                        }
                                        break;
                                    case "≥":
                                        {
                                            if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                                Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "合格";
                                            }
                                            else
                                            {
                                                CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                            }
                                        }
                                        break;

                                    default:
                                        break;

                                }


                            }
                            else
                            {
                                CheckDatas.Rows[j].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                                CheckDatas.Rows[j].Cells["标准值"].Value = "50";
                                if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < 50)
                                {
                                    CheckDatas.Rows[j].Cells[8].Value = "合格";
                                }
                                else
                                {
                                    CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                }
                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            }
            else if (cmbSample.Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbSample.Visible = false;
            }
            else
            {
                try
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;

                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                            string symbol = dt.Rows[0][4].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;

                                default:
                                    break;

                            }


                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSample.Visible = false;
            cmbAdd.Visible = false;
            if (cmbChkItem.Text == "以下相同")
            {
                cmbChkItem.Visible = false;
                for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                {
                    CheckDatas.Rows[k].Cells[2].Value = CheckDatas.CurrentCell.Value.ToString();
                }
            }
            else if (cmbChkItem.Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChkItem.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = cmbChkItem.Text;
                cmbChkItem.Visible = false;
            }
        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                            string symbol = dt.Rows[0][4].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;

                                default:
                                    break;

                            }

                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                CheckDatas.CurrentCell.Value = cmbSample.Text;
            }
        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnNotifyEvent(object sender, clsMessageNotify.NotifyEventArgs e)
        {
            if (e.Code == clsMessageNotify.NotifyInfo.ReadDY6200Data)
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
            CheckDatas.DataSource = dtbl;
           
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                CheckDatas.Rows[i].Cells[7].Value = Global.ChkManchine;
                CheckDatas.Rows[i].Cells[9].Value = cdt.Rows[0][0].ToString();//检测单位
                CheckDatas.Rows[i].Cells[11].Value = cdt.Rows[0][3].ToString();
                CheckDatas.Rows[i].Cells[12].Value = cdt.Rows[0][2].ToString();
                CheckDatas.Rows[i].Cells[13].Value = cdt.Rows[0][8].ToString();
                if (CheckDatas.Rows[i].Cells[8].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells[8].Value.ToString() == "阳性")
                {
                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            BtnReadHis.Enabled = true;
            BtnClear.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            dy6200.checkDtbl.Clear();
            CheckDatas.DataSource = null;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            if (!dy6200.Online)
            {
                MessageBox.Show("串口连接有误!");
                return;
            }

            // this.Cursor = Cursors.WaitCursor;
            BtnReadHis.Enabled = false;
            BtnClear.Enabled = false;
            //this.btnMachineDel.Enabled = false;
            DateTime dtstr = DTPStart.Value;
            DateTime dten = DTPEnd.Value;
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
        /// <summary>
        /// 绑定检测项目
        /// </summary>
        private void BindCheckItem()
        {
            _checkItems = clsStringUtil.GetAry(ShareOption.DefaultCheckItemCode);
        }
         /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonOperation.GetMachineSetting(tagName);
                //dy6200 = new clsDY6200();
                if (!dy6200.Online)
                {
                    dy6200.Open();
                    txtlink.Text = "已创建连接";
                    //MessageBox.Show("连接设备成功","提示");
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string chk = string.Empty;
            int iok = 0;
            int s = 0;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells[0].Value.ToString() == "False" || CheckDatas.Rows[i].Cells[0].Value.ToString() == "否")
                {
                    resultdata.Save = "是";
                    //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    resultdata.SampleName = CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim();
                    resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString();
                    resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString();
                    resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString();
                    resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString();
                    resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString();//标准值
                    resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString();//检测仪器
                    resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString();
                    resultdata.detectunit = CheckDatas.Rows[i].Cells[9].Value.ToString();//检测单位
                    resultdata.Gettime = CheckDatas.Rows[i].Cells[10].Value.ToString();//采样时间
                    resultdata.Getplace = CheckDatas.Rows[i].Cells[11].Value.ToString();
                    resultdata.CheckUnit = CheckDatas.Rows[i].Cells[12].Value.ToString();
                    resultdata.Tester = CheckDatas.Rows[i].Cells[13].Value.ToString();
                    chk = CheckDatas.Rows[i].Cells[14].Value.ToString().Replace("-", "/");
                    resultdata.CheckTime = DateTime.Parse(chk);

                    s = sqlSet.ResuInsert(resultdata, out err);
                    if (s == 1)
                    {
                        iok = iok + 1;
                        CheckDatas.Rows[i].Cells[0].Value = "是";
                    }
                }
            }
            MessageBox.Show("共保存" + iok + "条数据", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
        }
    }
}
