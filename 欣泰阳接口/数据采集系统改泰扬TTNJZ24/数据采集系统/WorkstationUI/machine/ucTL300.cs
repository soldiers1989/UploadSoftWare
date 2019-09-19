using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using WorkstationModel.Instrument;
using WorkstationModel.Model;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;

namespace WorkstationUI.machine
{
    public partial class ucTL300 :Basic.BasicContent
    {
        public ucTL300()
        {
            InitializeComponent();
        }
        private clsTL300 tl300 =new clsTL300() ;
        protected string[,] _checkItems;
        private delegate void InvokeDelegate(DataTable dtbl);
        private StringBuilder strWhere = new StringBuilder();
        private clsSetSqlData sqlset = new clsSetSqlData();
        private clsSaveResult resultdata = new clsSaveResult();
        public ComboBox cmbAdd = new ComboBox();//下拉列表
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private DataTable dti = null;
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1,3];

        private void ucTL300_Load(object sender, EventArgs e)
        {
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

            string checkItem = "{农药残留:000029000001:0:%}{甲醛:-1:1:mg/kg}{吊白块:-1:1:mg/kg}{双氧水:-1:1:mg/kg}{酱油中氨基酸态氮:-1:1:g/100mL}{味精中谷氨酸钠:-1:1:%}{亚硝酸盐:-1:1:mg/kg}{二氧化硫:-1:1:mg/kg}{硼砂:-1:1:mg/kg}{重金属铬:-1:1:mg/kg}{余氯:-1:1:mg/L}{硝酸盐:-1:1:mg/kg}{蛋白质:-1:1:g/100g}{碘盐碘含量:-1:1:mg/kg}{饮料中糖精钠:-1:1:mg/kg}{色素（亮蓝）:-1:1:mg/kg}";

            _checkItems =WorkstationModel.Model.clsStringUtil.GetDY3000DYAry(checkItem);
            clsTL300 .CheckItemsArray= _checkItems;
            clsDY3000DY.CheckItemsArray = _checkItems;
           
            //btnadd.Text = "数据编辑";
           
            //try
            //{
            //    if (!tl300.Online)
            //    {
            //        tl300.Open();
            //    }
            //}
            //catch (JH.CommBase.CommPortException ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //    return;
            //}

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

            for (int j = 0; j < _checkItems.GetLength(0); j++)
            {
                cmbChkItem.Items.Add(_checkItems[j, 0]);
                DataTable dt = sqlset.GetSample("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + _checkItems[j, 1] + ":%'", "SysCode", out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmbSample.Items.Add(dt.Rows[i][2].ToString());               
                        }
                    }
                }
            }
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
            try
            {
                cdt = sqlset.GetInformation("", "", out err);
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
    
        private  void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
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

        private  void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
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

                            DataTable dt = sqlset.GetChkItem(sql, "", out err);
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

                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='"+CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString()+"'";

                    DataTable dt = sqlset.GetChkItem(sql, "", out err);
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
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString())||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString())== Decimal.Parse(dt.Rows[0][3].ToString()))
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

                                default :
                                    break;

                            }
                            
                           
                        }
                        else 
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
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
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                try
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;
                    string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlset.GetChkItem(sql, "", out err);
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
        void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlset.GetChkItem(sql, "", out err);
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

        void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
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

        void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbChkItem.Text;
        }

        void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;
            if (e.KeyValue == 13)
            {
 
            }
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
                if (!tl300.Online)
                {
                    tl300.Open();
                    txtlink.Text = "已创建连接";
                    //MessageBox.Show("串口打开成功", "提示");
                }
                else
                {
                    txtlink.Text = "未创建连接";
                    MessageBox.Show("串口已打开", "提示");
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            bool blOnline = false;
            try
            {
                blOnline = tl300.Online;
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
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;
                DTPEnd.Value = DTPStart.Value.Date.AddDays(1).AddSeconds(-1);
                tl300.ReadHistory(DTPStart.Value.Date, DTPEnd.Value);
            }

        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            DataView vie =tl300.DataReadTable.DefaultView;
            vie.RowFilter = "检测时间 > #" +DTPStart.Value.Date + "# and 检测时间 < #" +DTPEnd.Value + "#";
            DataTable dt = vie.ToTable();
            ShowResult(dt, true);
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
                BtnReadHis.Enabled = true;
                BtnClear.Enabled = true;
                return;
            }
            DataView dv = null;
            if (dtbl.Rows.Count > 0)
            {
                dv = dtbl.DefaultView;
                dv.Sort = "检测时间 ASC";
                CheckDatas.DataSource = dv;
                CheckDatas.Columns[7].Width = 200;
                CheckDatas.Columns[9].Width = 180;
                CheckDatas.Columns[13].Width = 180;

                //判断数据是否保存过
                if (CheckDatas.Rows.Count > 1)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        strWhere.Length = 0;
                        strWhere.Clear();
                        //strWhere.AppendFormat(" SampleName='{0}'", CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim());//返回样品数据末尾包含“\0\0”
                        //strWhere.AppendFormat("and CheckData='{0}'", CheckDatas.Rows[i].Cells[3].Value.ToString());
                        strWhere.AppendFormat(" CheckData='{0}'", CheckDatas.Rows[i].Cells[3].Value.ToString());
                        strWhere.AppendFormat(" and Checkitem='{0}'", CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());
                        strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/")));
                        CheckDatas.Rows[i].Cells[0].Value = (sqlset.IsExist(strWhere.ToString()) == true ? "是" : "否");
                        //自动添加数据到表
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
            } 
            Cursor = Cursors.Default;
            BtnReadHis.Enabled = true;
            BtnClear.Enabled = true;
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;        
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnadd_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            Global.EditorSave = null;
            Global.TableRowNum = CheckDatas.Rows.Count;
            string[,] arr = new string[CheckDatas.Rows.Count, 7];
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                arr[i, 0] = CheckDatas.Rows[i].Cells[0].Value.ToString();
                arr[i, 1] = CheckDatas.Rows[i].Cells[1].Value.ToString();
                arr[i, 2] = CheckDatas.Rows[i].Cells[2].Value.ToString();
                arr[i, 3] = CheckDatas.Rows[i].Cells[3].Value.ToString();
                arr[i, 4] = CheckDatas.Rows[i].Cells[4].Value.ToString();
                arr[i, 5] = CheckDatas.Rows[i].Cells[5].Value.ToString();
                arr[i, 6] = CheckDatas.Rows[i].Cells[6].Value.ToString();
            }
            Global.EditorSave = arr;
            function.frmSetResult Sform = new function.frmSetResult();
            DialogResult dr = Sform.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                for (int j = 0; j < CheckDatas.Rows.Count; j++)
                {
                    CheckDatas.Rows[j].Cells[0].Value = "是";
                    sqlset.UpdateTempResult("是", CheckDatas.Rows[j].Cells[1].Value.ToString(), out err);
                }
            }
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            btnDatsave.Enabled = false;
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

                    s = sqlset.ResuInsert(resultdata, out err);
                    if (s == 1)
                    {
                        iok = iok + 1;
                        CheckDatas.Rows[i].Cells[0].Value = "是";
                    }
                }
            }
            MessageBox.Show("共保存" + iok + "条数据", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDatsave.Enabled = true;
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
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex > 8 && CheckDatas.CurrentCell.ColumnIndex < 13)
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbAdd.Visible = true;
                    }
                    else 
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
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
