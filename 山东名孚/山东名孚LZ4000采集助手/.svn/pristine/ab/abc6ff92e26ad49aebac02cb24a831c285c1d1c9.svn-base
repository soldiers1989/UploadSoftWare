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
using WorkstationModel.Thread;
using System.Web.Script.Serialization;
using WorkstationModel.UpData;
using System.Configuration;

namespace WorkstationUI.machine
{
    public partial class UCAll_LZ4000 : BasicContent   
    {   
        public IList<clsCheckData> _checkDatas = null;
        private string _strData = string.Empty, _settingType = string.Empty;       
        private clsdiary dy = new clsdiary();
       
       // private Thread rxThread = null;
        private bool[] empty = new bool[1];
        protected string[,] _checkItems;
        private delegate void InvokeDelegate(DataTable dtbl);
        private delegate void InvokeButton(Button btn);
        private ComboBox cmbAdd=new ComboBox() ;//其他信息录入
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private ComboBox cmbGenertAddr = new ComboBox();//产地地址
        private clsLZ4000T curLZ4000T  = new clsLZ4000T();
        private clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder strWhere = new StringBuilder();
        private DataTable cdt = null;
        SubThread _subthread;//定义一个线程
        private clsUpLoadData udata = new clsUpLoadData();
        private DateTime dts;//开始
        private DateTime dte;//结束
        /// <summary>
        /// 读取定时器
        /// 超过6 S还没读取到数据就恢复读取按钮
        /// </summary>
        private System.Timers.Timer stt = new System.Timers.Timer();
        private bool startread = false;
        /// <summary>
        /// 检测单位、被检单位信息
        /// </summary>
        private string[] BaseUnitInfo = new string[5];
       
        public UCAll_LZ4000()
        {
            InitializeComponent();
        }
        private void UCAll_LZ4000_Load(object sender, EventArgs e)
        {
            DTPEnd.Visible = true;
            BtnReadHis.Enabled = false;
            //btnadd.Visible = false;
            this._subthread = new SubThread();
            //添加后台线程的消息处理实现函数,
            this._subthread.MessageSend += new SubThread.MessageEventHandler(_subthread_MessageSend);
            try
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
                //其他信息录入
                //cmbAdd.Items.Add("输入");
                cmbAdd.Items.Add("以下相同");
                cmbAdd.Items.Add("删除");
                cmbAdd.Visible = false;
                cmbAdd.SelectedIndexChanged += new EventHandler(cmbAdd_SelectedIndexChanged);                        
                cmbAdd.MouseClick += cmbAdd_MouseClick;
                cmbAdd.KeyUp += new KeyEventHandler(cmbAdd_KeyUp);
                CheckDatas.Controls.Add(cmbAdd);
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
                //产地地址
                cmbGenertAddr.Items.Add("以下相同");
                cmbGenertAddr.Items.Add("删除");
                cmbGenertAddr.Visible = false;
                cmbGenertAddr.MouseClick += cmbGenertAddr_MouseClick;
                cmbGenertAddr.SelectedIndexChanged += cmbGenertAddr_SelectedIndexChanged;
                cmbGenertAddr.KeyUp += cmbGenertAddr_KeyUp;
                CheckDatas.Controls.Add(cmbGenertAddr);

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
                _checkItems = clsStringUtil.GetDY3000DYAry(itemcode);
                clsLZ4000T.CheckItemsArray = _checkItems;
                MessageNotification.GetInstance().DataRead += NotificationEventHandler;
                clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
                //加载被检单位信息   
                //cdt = sqlSet.GetExamedUnit("", "", out err);
                //if (cdt != null && cdt.Rows.Count>0)
                //{
                //    for (int n = 0; n < cdt.Rows.Count; n++)
                //    {
                //        cmbDetectUnit.Items.Add(cdt.Rows[n][2].ToString());//被检单位
                //        cmbGetSampleAddr.Items.Add(cdt.Rows[n][3].ToString());//采样地址
                //        curLZ4000T.BaseUnitInfo[1] = cdt.Rows[n][3].ToString();
                //        curLZ4000T.BaseUnitInfo[2] = cdt.Rows[n][2].ToString();
                //    }
                //}

                try
                {
                    cdt = sqlSet.GetInformation("", "", out err);
                    if (cdt != null)
                    {
                        if (cdt.Rows.Count > 0)
                        {
                            for (int n = 0; n < cdt.Rows.Count; n++)
                            {
                                if (cdt.Rows[n][9].ToString() == "是")//记录选中的单位信息
                                {
                                    //BaseUnitInfo[0] = cdt.Rows[n]["TestUnitName"].ToString();//生产单位
                                    //BaseUnitInfo[1] = cdt.Rows[n]["SampleAddress"].ToString();//产地
                                    //BaseUnitInfo[2] = cdt.Rows[n]["DetectUnitName"].ToString();//生产企业
                                    //BaseUnitInfo[3] = cdt.Rows[n]["Tester"].ToString();//检测员
                                    //BaseUnitInfo[4] = cdt.Rows[n]["TestUnitAddr"].ToString();//产地地址
                                    Global.shengchandanwei = cdt.Rows[n]["TestUnitName"].ToString();
                                    Global.chandi = cdt.Rows[n]["SampleAddress"].ToString();
                                    Global.shengchanqiye = cdt.Rows[n]["DetectUnitName"].ToString();
                                    Global.jianceyuan = cdt.Rows[n]["Tester"].ToString();
                                    Global.chandidizhi = cdt.Rows[n]["TestUnitAddr"].ToString();
                                }
                                cmbChkUnit.Items.Add(cdt.Rows[n]["TestUnitName"].ToString());//生产单位
                                cmbDetectUnit.Items.Add(cdt.Rows[n]["DetectUnitName"].ToString());//生产企业
                                cmbGetSampleAddr.Items.Add(cdt.Rows[n]["SampleAddress"].ToString());//产地
                                cmbChker.Items.Add(cdt.Rows[n]["Tester"].ToString());//检测员
                                cmbGenertAddr.Items.Add(cdt.Rows[n]["TestUnitAddr"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                dy.savediary(DateTime.Now.ToString(), "打开LZ-4000", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "打开LZ-4000", "失败：" + ex.Message);
            }
        }

        private void cmbGenertAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbGenertAddr.Text;
        }

        private void cmbGenertAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbGenertAddr.Text = "";
                cmbGenertAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbGenertAddr.Text = "";
                cmbGenertAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbGenertAddr.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbGenertAddr.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbGenertAddr.Visible = false;
            }       
        }

        private void cmbGenertAddr_MouseClick(object sender, MouseEventArgs e)
        {
            
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
            BtnReadHis.Enabled = false;
            bool blOnline = false;
            labelTile.Text = Global.ChkManchine;
            try
            {
                blOnline = curLZ4000T.Online;
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message + "，无法与仪器正常通讯，请重启界面！");
                BtnReadHis.Enabled = true;
                return;
            }

            if (!blOnline)
            {
                MessageBox.Show(this, "串口连接有误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                BtnReadHis.Enabled = true;
                return;
            }
            if (blOnline)
            {
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;
                startread = true;

                dts = DTPStart.Value;//记录起始时间
                dte = DTPEnd.Value;//记录结束时间

                //DTPEnd.Value = DTPStart.Value.Date.AddDays(1).AddSeconds(-1);
                curLZ4000T.DataReadTable.Clear();
                curLZ4000T.ReadRecord(DTPStart.Value.Date);

                //启动定时器
                stt.Elapsed += stt_Elapsed;
                stt.Interval = 120 * 1000;
                stt.AutoReset = false;//只执行一次
                stt.Enabled = true;
            }
        }
        /// <summary>
        /// 定时响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stt_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {        
            this._subthread.StartSend();
        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            curLZ4000T.strShow = string.Empty;//清除缓存
            Thread.Sleep(50);//延时50毫秒让单片机休息再请求
            if (e.Message == "tongxin")
            {
                BeginInvoke(new InvokeButton(showbtn), btnlinkcom);            
            }
            else if (e.Message == "NoRecord")//没有记录
            {              
                //继续读取下个日期的数据
                dts = dts.AddDays(1);
                if (dts < dte || dte == dts)
                {
                    curLZ4000T.ReadRecord(dts);
                }
                else
                {
                    ShowResult(curLZ4000T.DataReadTable, true);
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
                    ShowResult(curLZ4000T.DataReadTable, true);
                }    
            }
        }
        /// <summary>
        /// 显示连接上设备
        /// </summary>
        /// <param name="btn"></param>
        private void showbtn(Button btn)
        {
            txtlink.Text = "已连接设备";
            btnlinkcom.Text = "断开设备";
            BtnReadHis.Enabled = true;
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
            string err = string.Empty;
            string symbol = string.Empty;
            if (dtbl == null)
            {
                Cursor = Cursors.Default;           
                return;
            }         
            if (dtbl.Rows.Count > 0)
            {
                CheckDatas.DataSource = dtbl;
                CheckDatas.Columns[10].Width = 180;
                CheckDatas.Columns[2].Width = 150;
                CheckDatas.Columns[9].Width = 150;
                CheckDatas.Columns[7].Width = 180;
                CheckDatas.Columns[14].Width = 180; //检测时间 
                CheckDatas.Columns["生产日期"].Width = 180;
                CheckDatas.Columns["送检日期"].Width = 180; 
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                   
            //        strWhere.Clear();
            //        strWhere.AppendFormat("sampleName='{0}' and itemName='{1}'", CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim(), CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());                  
            //        //strWhere.Append("' and itemName='" + CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());                         
            //        //strWhere.Append("'");

            //        DataTable sdt = sqlSet.GetDownChkItem(strWhere.ToString(), "", out err);

            //        if (sdt != null)
            //        {
            //            if (sdt.Rows.Count > 0)
            //            {
            //                CheckDatas.Rows[i].Cells[5].Value = sdt.Rows[0][2].ToString();//检测依据
            //                CheckDatas.Rows[i].Cells[6].Value = sdt.Rows[0][3].ToString();//标准值
            //                symbol = sdt.Rows[0][4].ToString();//判断符号
            //                if (symbol == "≤")
            //                {
            //                    if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50 || Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) == 50)
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "合格";
            //                    }
            //                    else
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "不合格";
            //                    }
            //                }
            //                else if (symbol == "<")
            //                {
            //                    if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50)
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "合格";
            //                    }
            //                    else
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "不合格";
            //                    }
            //                }
            //                else if (symbol == "≥")
            //                {
            //                    if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) > 50 || Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString())==50)
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "合格";
            //                    }
            //                    else
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "不合格";
            //                    }
 
            //                }
            //                else if (symbol == ">")
            //                {
            //                    if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) > 50 )
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "合格";
            //                    }
            //                    else
            //                    {
            //                        CheckDatas.Rows[i].Cells[8].Value = "不合格";
            //                    }
            //                }
            //            }
            //            else 
            //            {
            //                CheckDatas.Rows[i].Cells[5].Value = "GB/T 5009.199-2003";
            //                CheckDatas.Rows[i].Cells[6].Value = "50";
            //                if (Convert.ToDecimal(CheckDatas.Rows[i].Cells[3].Value.ToString()) < 50)
            //                {
            //                    CheckDatas.Rows[i].Cells[8].Value = "合格";
            //                }
            //                else
            //                {
            //                    CheckDatas.Rows[i].Cells[8].Value = "不合格";
            //                }
            //            }
            //        }
                    //strWhere.Clear();
                    //string samplecode = "";
                    //strWhere.AppendFormat("foodName='{0}'", CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", ""));
                    //DataTable dtsam = sqlSet.GetSampleDetail(strWhere.ToString(), "", out err);
                    //if (dtsam != null && dtsam.Rows.Count > 0)
                    //{
                    //    samplecode = dtsam.Rows[0][2].ToString();
                    //}
                    //if (samplecode.Length > 9)
                    //{
                    //    strWhere.Clear();
                    //    strWhere.AppendFormat("foodCode='{0}'", samplecode.Substring(0, 9));
                    //    DataTable dtType = sqlSet.GetSampleDetail(strWhere.ToString(), "", out err);
                    //    if (dtType != null && dtType.Rows.Count > 0)
                    //    {
                    //        CheckDatas.Rows[i].Cells[15].Value = dtType.Rows[0][3].ToString();
                    //    }
                    //}
            //        //自动添加数据到表                   
            //        CheckDatas.Rows[i].Cells[11].Value = BaseUnitInfo[1]; // BaseUnitInfo[1];
            //        CheckDatas.Rows[i].Cells[12].Value = BaseUnitInfo[2]; // BaseUnitInfo[2];

                    //CheckDatas.Rows[i].Cells[9].Value = BaseUnitInfo[0];//检测单位
                    //CheckDatas.Rows[i].Cells[11].Value = BaseUnitInfo[1];
                    //CheckDatas.Rows[i].Cells[12].Value = BaseUnitInfo[2];
                    //CheckDatas.Rows[i].Cells[13].Value = BaseUnitInfo[3];

                    CheckDatas.Columns["检测单位"].Visible = false;
                    CheckDatas.Columns["采样地点"].Visible = false;
                    CheckDatas.Columns["被检单位"].Visible = false;

                    //Global.shengchandanwei = cdt.Rows[n]["TestUnitName"].ToString();
                    //Global.chandi = cdt.Rows[n]["SampleAddress"].ToString();
                    //Global.shengchanqiye = cdt.Rows[n]["DetectUnitName"].ToString();
                    //Global.jianceyuan = cdt.Rows[n]["Tester"].ToString();
                    //Global.chandidizhi = cdt.Rows[n]["TestUnitAddr"].ToString();

                    CheckDatas.Rows[i].Cells["生产单位"].Value = Global.shengchandanwei;
                    CheckDatas.Rows[i].Cells["产地地址"].Value = Global.chandidizhi;
                    CheckDatas.Rows[i].Cells["生产企业"].Value = Global.shengchanqiye;
                    CheckDatas.Rows[i].Cells["产地"].Value = Global.chandi;
                    CheckDatas.Rows[i].Cells["检测员"].Value = Global.jianceyuan;

                    if (CheckDatas.Rows[i].Cells[8].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells[8].Value.ToString() == "阳性")
                    {
                        CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    } 
                }
                CheckDatas.Refresh();
            }
            BtnReadHis.Enabled = true ;
            BtnClear.Enabled = true ;        
        }

        //protected  override  void winClose()
        //{
        //    if (MessageNotification.GetInstance() != null)
        //    {
        //        MessageNotification.GetInstance().DataRead -= NotificationEventHandler;
        //    }
        //    try
        //    {
        //        if (curLZ4000T != null)
        //        {
        //            if (curLZ4000T.Online)
        //                curLZ4000T.Close();
        //                curLZ4000T = null;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error");
        //    }
        //    this.Dispose();
        //}
        //数据保存
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            try
            {
                string err = string.Empty;
                string chk = string.Empty;
                int iok = 0;
                int s = 0;
                btnDatsave.Enabled = false;
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    if (CheckDatas.Rows[i].Cells[0].Value.ToString().Trim() == "False" || CheckDatas.Rows[i].Cells[0].Value.ToString().Trim() == "否")
                    {
                        resultdata.Save = "是";
                        resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Replace("\0\0", "").Trim();
                        DataTable dt = sqlSet.GetItemStandard("sampleName='" + resultdata.SampleName + "'", "", out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resultdata.SampleCode = dt.Rows[0]["sampleNum"].ToString();
                        }
                        resultdata.CheckNumber = Global.GUID(null, 1);
                        resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                        resultdata.CheckData = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim();
                        resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                        resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                        resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();//标准值
                        resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();//检测仪器
                        resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim();
                        resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString().Trim();//检测单位
                        resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString().Trim();//采样时间
                        resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString().Trim();
                        resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();
                        resultdata.Tester = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                        chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim();
                        resultdata.CheckTime = DateTime.Parse(chk);
                        resultdata.SampleType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim();//样品种类 
                        resultdata.sampleNum = CheckDatas.Rows[i].Cells["检测数量"].Value.ToString().Trim();//检测样品数量
                        resultdata.NumberUnit = CheckDatas.Rows[i].Cells["数量单位"].Value.ToString().Trim();//数量单位
                        resultdata.Barcode = CheckDatas.Rows[i].Cells["条形码"].Value.ToString().Trim();//数量单位
                        resultdata.ProduceUnit = CheckDatas.Rows[i].Cells["生产单位"].Value.ToString().Trim();//生产单位
                        resultdata.ProduceAddr = CheckDatas.Rows[i].Cells["产地地址"].Value.ToString().Trim();//产地地址
                        resultdata.ProcodeCompany = CheckDatas.Rows[i].Cells["生产企业"].Value.ToString().Trim();//生产企业
                        resultdata.ProductPlace = CheckDatas.Rows[i].Cells["产地"].Value.ToString().Trim();//产地
                        resultdata.ProductDatetime = CheckDatas.Rows[i].Cells["生产日期"].Value.ToString().Trim();//生产日期
                        resultdata.SendTestDate = CheckDatas.Rows[i].Cells["送检日期"].Value.ToString().Trim();//送检日期

                        s = sqlSet.ResuInsert(resultdata, out err);
                        if (s == 1)
                        {
                            iok = iok + 1;
                            CheckDatas.Rows[i].Cells[0].Value = "是";
                        }
                    }
                }
                MessageBox.Show("共保存" + iok + "条数据", "操作提示");
                btnDatsave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDatsave.Enabled = true;
            }
        }
        #region 线程处理
        /*定义一代理
        * 说明:其实例作为Invoke参数,以实现后台线程调用主线的函数
        * MessageEventArgs传递显示的信息.
        * */
        public delegate void MessageHandler(MessageEventArgs e);
        public void Message(MessageEventArgs e)
        {
            BtnClear.Enabled = true;
            BtnReadHis.Enabled = true;
            //this._subthread.EndSend();
        }
        /*说明:通过代理,消息事件,实际就是实现在后台线程调用本函数,以前就说了在后台线程中不能直接把消息发送到主线程,
         * 那么就要用到Invoke,关于怎么用不再多说
         * 参数要和MessageEventArgs代理的参数一至
         * **/
        private void _subthread_MessageSend(object sender, MessageEventArgs e)
        {
            if (e.Message == "恢复按钮")
            {
                //实例化代理
                MessageHandler handler = new MessageHandler(Message);
                //调用Invoke
                this.Invoke(handler, new object[] { e });
            }
        }
        #endregion
       
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
        private  void cmbGetSampleAddr_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        //被检单位键盘弹起事件
        private void cmbDetectUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbDetectUnit.Text.Trim();;
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
        private void cmbAdd_MouseClick(object sender, MouseEventArgs e)
        {
            //if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            //{
            //    // 双击事件处理方式
            //    frmChkUnitInfo fchk = new frmChkUnitInfo();
            //    //fchk.lz4000 = this;
            //    DialogResult dr = fchk.ShowDialog();

            //    if (dr == DialogResult.OK)
            //    {
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[11].Value = Global.ChkInfo[0, 0];
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[10].Value = Global.ChkInfo[0, 1];
            //        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[12].Value = Global.ChkInfo[0, 2];
            //        cmbAdd.Visible = false;
            //    }
            //}
            //else
            //{
            //    dtCmbLastClick = DateTime.Now;
            //}      
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
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbAdd.Text = "";
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
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > 8 && CheckDatas.CurrentCell.RowIndex > -1 && CheckDatas.CurrentCell.ColumnIndex < 25)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 9)//生产单位
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
                        cmbGenertAddr.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 10 || CheckDatas.CurrentCell.ColumnIndex >= 15 )//采样时间
                    {
                        if (e.ColumnIndex == 17)//数量单位
                        {
                            cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbAdd.Items.Clear();
                            cmbAdd.Items.Add("以下相同");
                            cmbAdd.Items.Add("删除");
                            cmbAdd.Items.Add("吨");
                            cmbAdd.Items.Add("公斤");
                            cmbAdd.Items.Add("斤");
                            cmbAdd.Items.Add("两");
                            cmbAdd.Items.Add("克");
                            cmbAdd.Items.Add("毫克");
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
                            cmbGenertAddr.Visible = false;
                        }
                        else if (e.ColumnIndex == 21)//生产企业
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
                            cmbGenertAddr.Visible = false;
                        }
                        else if (e.ColumnIndex == 19)//生产单位
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
                            cmbGenertAddr.Visible = false;
                        }
                        else if (e.ColumnIndex == 22)//产地
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
                            cmbGenertAddr.Visible = false;
                        }
                        else if (e.ColumnIndex == 20)//产地地址
                        {
                            cmbGenertAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                            CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                            Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                            cmbGenertAddr.Left = rect.Left;
                            cmbGenertAddr.Top = rect.Top;
                            cmbGenertAddr.Width = rect.Width;
                            cmbGenertAddr.Height = rect.Height;
                            cmbGenertAddr.Visible = true;
                            cmbChkUnit.Visible = false;
                            cmbChker.Visible = false;
                            cmbDetectUnit.Visible = false;
                            cmbGetSampleAddr.Visible = false;
                            cmbAdd.Visible = false;
 
                        }
                        else
                        {
                            cmbAdd.DropDownStyle = ComboBoxStyle.DropDown;
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
                            cmbChkUnit.Visible = false;
                            cmbAdd.Visible = true;
                            cmbChker.Visible = false;
                            cmbDetectUnit.Visible = false;
                            cmbGetSampleAddr.Visible = false;
                            cmbGenertAddr.Visible = false ;
                        }   
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
                        cmbChkUnit.Visible = false ;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = true;
                        cmbAdd.Visible = false;
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
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void closecom()
        {
            curLZ4000T.Close();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override  void btnlinkcom_Click(object sender, EventArgs e)
        {
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            if (btnlinkcom.Text == "连接设备")
            {
                try
                {
                    if (!curLZ4000T.Online)
                    {
                        curLZ4000T.Open();

                        curLZ4000T.Communicate();
                        cmbCOMbox.Enabled = false;
                        btnlinkcom.Text = "断开设备";
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
                curLZ4000T.Close();
                txtlink.Text = "未连接";
                btnlinkcom.Text = "连接设备";
                cmbCOMbox.Enabled = true;
                BtnReadHis.Enabled = false;
            }
        }
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnadd_Click(object sender, EventArgs e)
        {
            btnadd.Enabled = false;
            if (Global.linkNet()==false )
            {
                MessageBox.Show("无法连接到互联网，请检查网络连接！","系统提示");
                btnadd.Enabled = true;
                return;
            }
            try
            {
                if (CheckDatas.Rows.Count < 1)
                {
                    MessageBox.Show("没有检测数据上传", "提示");
                    btnadd.Enabled = true;
                    return;
                }

                if (Global.ServerAdd.Length == 0)
                {
                    MessageBox.Show("服务器地址不能为空", "提示");
                    btnadd.Enabled = true;
                    return;
                }
                if (Global.ServerName.Length == 0)
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    btnadd.Enabled = true;
                    return;
                }
                if (Global.ServerPassword.Length == 0)
                {
                    MessageBox.Show("密码不能为空", "提示");
                    btnadd.Enabled = true;
                    return;
                }

                string err = "";
                int upok = 0;
             
                for (int j = 0; j < CheckDatas.Rows.Count; j++)
                {
                    if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "否")
                    {
                        MessageBox.Show("请保存数据再上传", "提示");
                        btnadd.Enabled = true;
                        return;
                    }
                }

                DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (tishi == DialogResult.No)
                {
                    btnadd.Enabled = true;
                    return;
                }
                WorkstationModel.shandong.SDUpdata updata = new WorkstationModel.shandong.SDUpdata();

                strWhere.Length = 0;
                string errstr = "";
                int isupdata = 0;

                DataTable dtb = sqlSet.GetResultTable(strWhere.ToString(), "", 1, CheckDatas.Rows.Count, out err);
                if (dtb != null && dtb.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb.Rows.Count; i++)
                    {
                        if (dtb.Rows[i]["IsUpload"].ToString() == "是")//不允许重传
                        {
                            isupdata = isupdata + 1;
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                            continue;
                        }
                        updata.CheckNumber = dtb.Rows[i]["ChkNum"].ToString();
                        updata.CheckUnitCode = Global.ServerName;
                        updata.SampleName = dtb.Rows[i]["SampleName"].ToString();
                        updata.Operator = dtb.Rows[i]["Tester"].ToString();
                        updata.InhibitionRatio = float.Parse(dtb.Rows[i]["CheckData"].ToString());
                        updata.Chktime =DateTime.Parse( dtb.Rows[i]["CheckTime"].ToString());
                        updata.GoodsUnit = dtb.Rows[i]["ProduceUnit"].ToString();
                        updata.ProductAddr = dtb.Rows[i]["ProduceAddr"].ToString();
                        updata.ProductionDate = DateTime.Parse(dtb.Rows[i]["ProductDatetime"].ToString());
                        updata.barcode = dtb.Rows[i]["Barcode"].ToString();
                        updata.ProductionUnit = dtb.Rows[i]["ProduceUnit"].ToString();//生产企业
                        updata.Conclusion = dtb.Rows[i]["Result"].ToString();
                        updata.SampleType = dtb.Rows[i]["SampleCategory"].ToString();//样品种类
                        updata.ProductPlace = dtb.Rows[i]["ProductPlace"].ToString();//产地
                        updata.SendTime =DateTime.Parse( dtb.Rows[i]["SendTestDate"].ToString());//送检日期
                        updata.samplenum = dtb.Rows[i]["SampleNum"].ToString();//样品数量
                       
                        updata.SampleCode = dtb.Rows[i]["SampleCode"].ToString();//样品编号
                        updata.numunit = dtb.Rows[i]["NumberUnit"].ToString();//数值单位

                        if (WorkstationModel.shandong.SDUpdata.InvokeAndCallWebService(updata))
                        {
                            upok = upok + 1;
                            sqlSet.SetUploadResult(dtb.Rows[i]["ID"].ToString(), out err);
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;//修改上传数据背景颜色
                        }
                        if (Global.ReturnMessage != "")
                        {
                            if (errstr != "")
                            {
                                errstr = errstr + "," + geterrmessage(Global.ReturnMessage);
                            }
                            else
                            {
                                errstr = errstr + geterrmessage(Global.ReturnMessage);
                            }
                        }
                    }
                }
                if (errstr == "")
                {
                    MessageBox.Show("共成功能够上传" + upok.ToString () + "条数据； 共"+isupdata +"条数据已传！", "数据上传");
                }
                else
                {
                    MessageBox.Show("共成功上传" + upok.ToString() + "条数据；共"+isupdata +"条数据已传！ 提示信息：" + errstr);
                }
                btnadd.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnadd.Enabled = true;
            }
        }
        private string geterrmessage(string err)
        {
            string rtn = "";
            if (err == "-1")
            {
                rtn = "用户名或密码不能为空";
            }
            else if (err == "0")
            {
                rtn = "Code口令错误";
            }
            else if (err == "1")
            {
                rtn = "用户名或者密码输入错误";
            }
            else if (err == "2")
            {
                rtn = "检测数据不能为空";
            }
            else if (err == "3")
            {
                rtn = "表示没有可上传的数据库";
            }
            else if (err == "4")
            {
                rtn = "检测数据上传成功";
            }
            return rtn;
        }
        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false ;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;   
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {         
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbAdd.Visible = false;
        }

    }
}
