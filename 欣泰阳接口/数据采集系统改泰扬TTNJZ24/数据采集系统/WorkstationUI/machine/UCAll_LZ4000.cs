using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using System.IO.Ports;
using System.Threading;
using WorkstationDAL.Model;
using WorkstationModel.Instrument;
using WorkstationModel.Model;
using WorkstationBLL.Mode;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;
using WorkstationDAL.Basic;
using WorkstationUI.function;

namespace WorkstationUI.machine
{
    public partial class UCAll_LZ4000 : BasicContent   
    {
        private SerialPort SP = new SerialPort();
        private IntPtr hPort;
        public IList<clsCheckData> _checkDatas = null;
        private IList<clsCheckedUnit> _checkedUnits = null;
        private IList<clsProduct> _products = null;
        private int _selIndex = -1;
        private byte getDeviceModel = 0x00;
        private string _strData = string.Empty, _settingType = string.Empty;
        private bool _IsReadOver = false;
        private clsProduct _newProduct = null;
        private clsCheckedUnit _newCheckedUnit = null;
       
        //串口数据接收另一种方法
        private Thread rxThread = null;
        private bool[] empty = new bool[1];
        protected string[,] _checkItems;
        private delegate void InvokeDelegate(DataTable dtbl);
        private ComboBox cmbAdd=new ComboBox() ;
        private clsLZ4000T curLZ4000T  = new clsLZ4000T();

        clsSaveResult resultdata = new clsSaveResult();
        clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder strWhere = new StringBuilder();
        private DataTable dti = null;
        private DataTable cdt = null;

        public UCAll_LZ4000()
        {
            InitializeComponent();

            this.Disposed += closecom; 
        }
       
        private void closecom(object sender, EventArgs e)
        {
            curLZ4000T.Close();
 
        }

        //清除控件数据
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
         
            CheckDatas.DataSource = null;
           
        }
        /// <summary>
        /// 读取数据按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            bool blOnline = false;
            labelTile.Text = Global.ChkManchine;
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
                //Cursor = Cursors.WaitCursor;
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;

                DTPEnd.Value = DTPStart.Value.Date.AddDays(1).AddSeconds(-1);
                curLZ4000T.DataReadTable.Clear();
                curLZ4000T.ReadHistory(DTPStart.Value.Date);
                BtnReadHis.Enabled = true ;
                BtnClear.Enabled = true;
            }
        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            DataView vie = curLZ4000T.DataReadTable.DefaultView;
            vie.RowFilter = "检测时间 > #" + DTPStart.Value.Date + "# and 检测时间 < #" + DTPEnd.Value + "#";
            DataTable dt = vie.ToTable();
            ShowResult(dt, true);
        }

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
        private void  showOnControl(DataTable dtbl)
        {
            //CheckDatas.Rows.Clear();
            string err = string.Empty;
            string symbol = string.Empty;
            if (dtbl == null)
            {
                Cursor = Cursors.Default;           
                return;
            }
            DataView dv = null;
            
            if (dtbl.Rows.Count > 0)
            {
                dv = dtbl.DefaultView;
                //dv.Sort = "检测时间 ASC";
                CheckDatas.DataSource = dv;
                CheckDatas.Columns[13].Width = 180;
                CheckDatas.Columns[2].Width = 150;
                CheckDatas.Columns[9].Width = 150;
                CheckDatas.Columns[7].Width = 180;
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    //if (CheckDatas.Rows[i].Cells[1].Value.ToString().Contains("\0\0"))
                    //{
                    //    CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0","").Trim();//返回样品数据末尾包含“\0\0”
                    //}
                    strWhere.Length = 0;
                    strWhere.Clear();
                    strWhere.AppendFormat(" SampleName='{0}'", CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim());//返回样品数据末尾包含“\0\0”
                    strWhere.AppendFormat("and CheckData='{0}'", CheckDatas.Rows[i].Cells[3].Value.ToString());
                    strWhere.AppendFormat(" and Checkitem='{0}'", CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());
                    strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/")));
                    CheckDatas.Rows[i].Cells[0].Value = (sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否");

                    strWhere.Clear();
                    strWhere.Append("FtypeNmae='");
                    strWhere.Append(CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim());
                    strWhere.Append("' and Name='");
                    strWhere.Append(CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());
                    //strWhere.Append("农药残留");
                    strWhere.Append("'");

                    DataTable sdt=  sqlSet.GetStandardValue(strWhere.ToString(), "", out err);

                    if (sdt != null)
                    {
                        if (sdt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[i].Cells[5].Value = sdt.Rows[0][3].ToString();//检测依据
                            CheckDatas.Rows[i].Cells[6].Value = sdt.Rows[0][4].ToString();//标准值
                            symbol = sdt.Rows[0][3].ToString();//判断符号
                            if (symbol == "≤")
                            {
                                if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50 || Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) == 50)
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "合格";
                                }
                                else
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "不合格";
                                }
                            }
                            else if (symbol == "<")
                            {
                                if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50)
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "合格";
                                }
                                else
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "不合格";
                                }
                            }
                            else if (symbol == "≥")
                            {
                                if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) > 50 || Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString())==50)
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "合格";
                                }
                                else
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "不合格";
                                }
 
                            }
                            else if (symbol == ">")
                            {
                                if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) > 50 )
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "合格";
                                }
                                else
                                {
                                    CheckDatas.Rows[i].Cells[8].Value = "不合格";
                                }
                            }
                        }
                        else 
                        {
                            CheckDatas.Rows[i].Cells[5].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[i].Cells[6].Value = "50";
                            if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[i].Cells[8].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[i].Cells[8].Value = "不合格";
                            }
                        }
                    }
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
            BtnReadHis.Enabled = true ;
            BtnClear.Enabled = true ;        
        }

        protected  override  void winClose()
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            this.Dispose();
        }
        //数据保存
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string chk = string.Empty;
            int iok = 0;
            int s = 0;
            for (int i = 0; i < CheckDatas.Rows.Count ; i++)  
            {
                if (CheckDatas.Rows[i].Cells[0].Value.ToString() == "False" || CheckDatas.Rows[i].Cells[0].Value.ToString()=="否")
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
            MessageBox.Show("共保存" + iok + "条数据", "操作提示");
        }  

        private void UCAll_LZ4000_Load(object sender, EventArgs e)
        {
            labelTile.Text = Global.ChkManchine;
            string[] Port = SerialPort.GetPortNames();
            string itemcode = string.Empty;
            string err = string.Empty;

            if (Port.Length == 0)
            {
                cmbCOMbox.Items.Add("没有COM口");
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCOMbox.Items.Add(c);
            }
            cmbCOMbox.SelectedIndex = 0;

            //cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");       
            cmbAdd.Visible = false;
            cmbAdd.SelectedIndexChanged += new EventHandler(cmbAdd_SelectedIndexChanged);
            //cmbAdd.MouseDoubleClick += cmbAdd_MouseDoubleClick;//无法实现           
            cmbAdd.MouseClick += cmbAdd_MouseClick;
            cmbAdd.KeyUp += new KeyEventHandler(cmbAdd_KeyUp);
            CheckDatas.Controls.Add(cmbAdd);

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
            //try
            //{
            //    if (!curLZ4000T.Online)
            //        curLZ4000T.Open();
            //}
            //catch (JH.CommBase.CommPortException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
            _checkItems = clsStringUtil.GetDY3000DYAry(itemcode);
            clsLZ4000T.CheckItemsArray = _checkItems;
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
            clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
            //加载被检单位信息           
            try
            {
                dti = sqlSet.GetInformation("iChecked='是'", "", out err);
                if (dti != null)
                {
                    if (dti.Rows.Count > 0)
                    {
                        for (int n = 0; n < dti.Rows.Count; n++)
                        {
                            //cmbAdd.Items.Add(dti.Rows[n][2].ToString());
                            //WorkstationDAL.Model.clsShareOption.ComPort = dti.Rows[n][2].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
                            cmbAdd.Items.Add(cdt.Rows[n][2].ToString());
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
        /// 消息事件更新label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clsUpdateMessage_LabelUpdated(object sender, clsUpdateMessage.LabelUpdateEventArgs e)
        {
            if (e.Code == "RS232LZ4000")
            {
                labelTile.Text = e.Slabel;
            }
        }
          //首先加载进获取系统PAI函数的引用：
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();//重写系统API函数获取鼠标双击的有效间隔
        private DateTime dtCmbLastClick = DateTime.MinValue;//存储两次单击的时间间隔
        void cmbAdd_MouseClick(object sender, MouseEventArgs e)
        {
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                frmChkUnitInfo fchk = new frmChkUnitInfo();
                //fchk.lz4000 = this;
                DialogResult dr = fchk.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[11].Value = Global.ChkInfo[0, 0];
                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[10].Value = Global.ChkInfo[0, 1];
                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[12].Value = Global.ChkInfo[0, 2];
                    cmbAdd.Visible = false;
                }
            }
            else
            {
                dtCmbLastClick = DateTime.Now;
            }      
        }

        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {          
             CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbAdd.Text;     
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {  
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
        /// datagridview 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > 8 && CheckDatas.CurrentCell.RowIndex > -1 && CheckDatas.CurrentCell.ColumnIndex < 13)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 11)
                    {
                        cmbAdd.Items.Clear();
                        cmbAdd.Items.Add("以下相同");
                        cmbAdd.Items.Add("删除");
                        for (int k = 0; k < cdt.Rows.Count;k++ )
                        {
                            cmbAdd.Items.Add(cdt.Rows[k][2].ToString());
                        }

                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbAdd.Visible = true;
                    }
                    else 
                    {
                        cmbAdd.Items.Clear();
                        cmbAdd.Items.Add("以下相同");
                        cmbAdd.Items.Add("删除");

                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbAdd.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override  void btnlinkcom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!curLZ4000T.Online)
                {
                    curLZ4000T.Open();
                    txtlink.Text = "已连接设备";
                    //MessageBox.Show("仪器连接成功", "提示");
                }
                else
                {
                    MessageBox.Show("串口已打开","提示");
                }
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
        }

    }
}
