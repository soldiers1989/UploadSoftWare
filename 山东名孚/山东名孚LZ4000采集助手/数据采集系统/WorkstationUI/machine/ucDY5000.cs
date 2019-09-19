using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.machine;
using WorkstationDAL.Model;
using WorkstationBLL.Mode;
using WorkstationModel.Instrument;
using WorkstationModel.Model;
using WorkstationDAL.Basic;
using WorkstationUI.Basic;
using System.IO.Ports;

namespace WorkstationUI.machine
{
    public partial class ucDY5000 : BasicContent 
    {
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private clsDY5000 curDY5000=new clsDY5000();//旧DY5000版本
        private string tagName = string.Empty;
        private StringBuilder strWhere = new StringBuilder();
        public string[,] _checkItems;
        private clsSaveResult resultdata = new clsSaveResult();
        private ComboBox cmbAdd = new ComboBox();
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];

        public ucDY5000()
        {
            InitializeComponent();
        }

        private void ucDY5000_Load(object sender, EventArgs e)
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
            //其他信息录入
            cmbCOMbox.SelectedIndex = 0;
            cmbAdd.Visible = false;           
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.KeyUp += cmbAdd_KeyUp;
            cmbAdd.SelectedIndexChanged += cmbAdd_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbAdd);
            //检测项目
            cmbChkItem.Visible = false;          
            cmbChkItem.Items.Add("以下相同");
            cmbChkItem.Items.Add("删除");
            cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChkItem.KeyUp += cmbChkItem_KeyUp;
            cmbChkItem.SelectedIndexChanged += cmbChkItem_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbChkItem);
            //样品名称
            cmbSample.Visible = false;           
            cmbSample.Items.Add("以下相同");
            cmbSample.Items.Add("删除");
            cmbSample.MouseClick += cmbSample_MouseClick;
            cmbSample.KeyUp += cmbSample_KeyUp;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbSample);
            //检测单位
            cmbChkUnit.Items.Add("以下相同");
            cmbChkUnit.Items.Add("删除");
            cmbChkUnit.Visible = false;
            cmbChkUnit.SelectedIndexChanged += cmbChkUnit_SelectedIndexChanged;
            //cmbChkUnit.MouseClick += cmbChkUnit_MouseClick;
            cmbChkUnit.KeyUp += cmbChkUnit_KeyUp;
            CheckDatas.Controls.Add(cmbChkUnit);
            //被检单位              
            cmbDetectUnit.Items.Add("以下相同");
            cmbDetectUnit.Items.Add("删除");
            cmbDetectUnit.Visible = false;
            cmbDetectUnit.SelectedIndexChanged += cmbDetectUnit_SelectedIndexChanged;
            //cmbDetectUnit.MouseClick += cmbDetectUnit_MouseClick;
            cmbDetectUnit.KeyUp += cmbDetectUnit_KeyUp;
            CheckDatas.Controls.Add(cmbDetectUnit);
            //采样地址
            cmbGetSampleAddr.Items.Add("以下相同");
            cmbGetSampleAddr.Items.Add("删除");
            cmbGetSampleAddr.Visible = false;
            cmbGetSampleAddr.SelectedIndexChanged += cmbGetSampleAddr_SelectedIndexChanged;
            //cmbGetSampleAddr.MouseClick += cmbGetSampleAddr_MouseClick;
            cmbGetSampleAddr.KeyUp += cmbGetSampleAddr_KeyUp;
            CheckDatas.Controls.Add(cmbGetSampleAddr);
            //检测员
            cmbChker.Items.Add("以下相同");
            cmbChker.Items.Add("删除");
            cmbChker.Visible = false;
            cmbChker.MouseClick += cmbChker_MouseClick;
            cmbChker.SelectedIndexChanged += cmbChker_SelectedIndexChanged;
            cmbChker.KeyUp += cmbChker_KeyUp;
            CheckDatas.Controls.Add(cmbChker);
            
            BindCheckItem();

            for (int j = 0; j < _checkItems.GetLength(0); j++)
            {
                cmbChkItem.Items.Add(_checkItems[j, 0]);
                DataTable dt = sqlSet.GetSample("Name='" + _checkItems[j, 0] + "'", "idx", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmbSample.Items.Add(dt.Rows[i][0].ToString());
                        }
                    }
                }
            }
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
                                unitInfo[0, 0] = cdt.Rows[n][0].ToString();
                                unitInfo[0, 1] = cdt.Rows[n][2].ToString();
                                unitInfo[0, 2] = cdt.Rows[n][3].ToString();
                                unitInfo[0, 3] = cdt.Rows[n][8].ToString();
                            }
                            cmbChkUnit.Items.Add(cdt.Rows[n][0].ToString());//检测单位
                            cmbDetectUnit.Items.Add(cdt.Rows[n][2].ToString());//被检单位
                            cmbGetSampleAddr.Items.Add(cdt.Rows[n][3].ToString());//采样地址
                            cmbChker.Items.Add(cdt.Rows[n][8].ToString());//检测员
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            clsMessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent;
            clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
        }
        //检测员按键弹起事件
        private void cmbChker_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChker.Text;
        }
        //检测员选择事件
        private void cmbChker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChker.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChker.Visible = false;
            }
        }
        //检测员单击事件
        private void cmbChker_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //采样地址选择事件
        private void cmbGetSampleAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbGetSampleAddr.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbGetSampleAddr.Visible = false;
            }
        }
        //采样地址弹起事件
        private void cmbGetSampleAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbGetSampleAddr.Text;
        }
        //采样地址单击事件
        private void cmbGetSampleAddr_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //被检单位键盘弹起事件
        private void cmbDetectUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbDetectUnit.Text.Trim(); ;
        }
        //被检单位单击事件
        private void cmbDetectUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //被检单位选择事件
        private void cmbDetectUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbDetectUnit.Text = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbDetectUnit.Visible = false;
            }
        }
        //检测单位按键弹起事件
        private void cmbChkUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChkUnit.Text.Trim();
        }
        //检测单位单击事件
        private void cmbChkUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //检测单位选择事件
        private void cmbChkUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChkUnit.Text = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChkUnit.Visible = false;
            }
        }
        /// <summary>
        /// 更新标题的label事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clsUpdateMessage_LabelUpdated(object sender, clsUpdateMessage.LabelUpdateEventArgs e)
        {
            if (e.Code == "RS232DY5000")
            {
                LbTitle.Text = e.Slabel;
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
        private void cmbSample_MouseClick(object sender, MouseEventArgs e)
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
        public void closecom()
        {
            curDY5000.Close();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            if (btnlinkcom.Text == "连接设备")
            {
                try
                {
                    if (!curDY5000.Online)
                    {
                        curDY5000.Open();
                        txtlink.Text = "已连接设备";
                        btnlinkcom.Text = "断开设备";
                        //MessageBox.Show("仪器连接成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("串口已打开", "提示");
                    }
                }
                catch (JH.CommBase.CommPortException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (btnlinkcom.Text == "断开设备")
            {
                curDY5000.Close();
                txtlink.Text = "未连接";
                btnlinkcom.Text = "连接设备";
 
            }
        }
        /// <summary>
        /// 查询检测项目
        /// </summary>
        protected void BindCheckItem()
        {
            //CommonOperation.GetMachineSetting("DY-5000食品安全综合分析仪");//代表老版本DY5000
            string itemcode = string.Empty;
            string err = "";
            if (Global.ChkManchine != "")
            {
                try
                {
                    strWhere.Clear();
                    strWhere.Append("WHERE Name='");
                    strWhere.Append(Global.ChkManchine);
                    strWhere.Append("'");
                    DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            itemcode = dt.Rows[0][4].ToString();
                            WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[0][2].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            _checkItems = clsStringUtil.GetAry(itemcode);
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
            CheckDatas.DataSource = dtbl;
            CheckDatas.Refresh();
            CheckDatas.Columns[7].Width = 180;
            CheckDatas.Columns[14].Width = 180;
            //自动添加数据到表
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
           
        }

        protected override void winClose()
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

                    resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
                    resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                    resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
                    resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
                    resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();//标准值
                    resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString().Trim();//检测仪器
                    resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
                    resultdata.Gettime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();//采样时间
                    resultdata.Getplace = CheckDatas.Rows[i].Cells[10].Value.ToString().Trim();
                    resultdata.CheckUnit = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();
                    resultdata.Tester = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();
                    chk = CheckDatas.Rows[i].Cells[13].Value.ToString().Replace("-", "/");
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


        protected  override  void BtnClear_Click(object sender, EventArgs e)
        {
            curDY5000._dataReadTable.Clear();
            CheckDatas.DataSource = curDY5000._dataReadTable;
        }

        protected override void BtnReadHis_Click(object sender, EventArgs e)
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
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }
        /// <summary>
        /// datagridview 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CheckDatas.CurrentCell.ColumnIndex < 1)
            {
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                cmbSample.Visible = false;
                cmbChkItem.Visible = false;
                cmbAdd.Visible = false;
                cmbChkUnit.Visible = false;
                cmbAdd.Visible = false;
                cmbChker.Visible = false;
                cmbDetectUnit.Visible = false;
                cmbGetSampleAddr.Visible = false;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.RowIndex > -1)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 2)
                    {
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkItem.Left = irect.Left;
                        cmbChkItem.Top = irect.Top;
                        cmbChkItem.Width = irect.Width;
                        cmbChkItem.Height = irect.Height;
                        cmbChkItem.Visible = true;
                        cmbChkUnit.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 1)
                    {
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
                        cmbSample.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbSample.Visible = true;
                        cmbChkUnit.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 9)//检测单位
                    {
                        cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkUnit.Left = rect.Left;
                        cmbChkUnit.Top = rect.Top;
                        cmbChkUnit.Width = rect.Width;
                        cmbChkUnit.Height = rect.Height;
                        cmbChkUnit.Visible = true;
                        cmbAdd.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 10)//采样时间
                    {
                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbAdd.Visible = true;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 11)//采样地点
                    {
                        cmbGetSampleAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbGetSampleAddr.Left = rect.Left;
                        cmbGetSampleAddr.Top = rect.Top;
                        cmbGetSampleAddr.Width = rect.Width;
                        cmbGetSampleAddr.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = true;
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 12)//被检单位
                    {
                        cmbDetectUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbDetectUnit.Left = rect.Left;
                        cmbDetectUnit.Top = rect.Top;
                        cmbDetectUnit.Width = rect.Width;
                        cmbDetectUnit.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = true;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;

                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 13)//检测员
                    {
                        cmbChker.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChker.Left = rect.Left;
                        cmbChker.Top = rect.Top;
                        cmbChker.Width = rect.Width;
                        cmbChker.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = true;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                    }
                    else
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;                    
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbSample.Visible = false;
            cmbChkItem.Visible = false;          
            cmbAdd.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbSample.Visible = false;
            cmbChkItem.Visible = false;          
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbAdd.Visible = false;
        }

    }
}
