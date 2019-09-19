﻿using System;
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
using System.Xml;

namespace WorkstationUI.machine
{
    public partial class UCAll_LZ4000 : BasicContent   
    {   
        public IList<clsCheckData> _checkDatas = null;
        private string _strData = string.Empty, _settingType = string.Empty;       
        private clsdiary dy = new clsdiary();
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
        private ComboBox cmbDetectUnitNature = new ComboBox();//被检单位性质
        private ComboBox cmbProductCompany = new ComboBox();//生产单位
        private bool startread = false;
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
        /// <summary>
        /// 检测单位、被检单位信息
        /// </summary>
        private string[] BaseUnitInfo = new string[5];
        private string checkUnit = "";//被检单位
        private string decUnit = "";//检测单位
       
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
                //产地
                cmbGenertAddr.Items.Add("以下相同");
                cmbGenertAddr.Items.Add("删除");
                cmbGenertAddr.Visible = false;
                cmbGenertAddr.MouseClick += cmbGenertAddr_MouseClick;
                cmbGenertAddr.SelectedIndexChanged += cmbGenertAddr_SelectedIndexChanged;
                cmbGenertAddr.KeyUp += cmbGenertAddr_KeyUp;
                CheckDatas.Controls.Add(cmbGenertAddr);
                //生产单位
                cmbProductCompany.Items.Add("以下相同");
                cmbProductCompany.Items.Add("删除");
                cmbProductCompany.Visible = false;
                cmbProductCompany.MouseClick += cmbProductCompany_MouseClick;
                cmbProductCompany.SelectedIndexChanged += cmbProductCompany_SelectedIndexChanged;
                cmbProductCompany.KeyUp += cmbProductCompany_KeyUp;
                CheckDatas.Controls.Add(cmbProductCompany);
                //被检单位性质
                cmbDetectUnitNature.Items.Add("以下相同");
                cmbDetectUnitNature.Items.Add("删除");
                cmbDetectUnitNature.Visible = false;
                cmbDetectUnitNature.MouseClick += cmbDetectUnitNature_MouseClick;
                cmbDetectUnitNature.KeyUp += cmbDetectUnitNature_KeyUp;
                CheckDatas.Controls.Add(cmbDetectUnitNature);

                if (Global.ChkManchine != "")
                {
                    strWhere.Length = 0;
                    strWhere.AppendFormat("WHERE Name='{0}'", Global.ChkManchine);
                    DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        itemcode = dt.Rows[0][4].ToString();
                        WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[0][2].ToString();
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
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n]["iChecked"].ToString() == "是")//记录选中的单位信息
                        {
                            //BaseUnitInfo[0] = cdt.Rows[n]["TestUnitName"].ToString();//生产单位
                            //BaseUnitInfo[1] = cdt.Rows[n]["SampleAddress"].ToString();//产地
                            //BaseUnitInfo[2] = cdt.Rows[n]["DetectUnitName"].ToString();//生产企业
                            //BaseUnitInfo[3] = cdt.Rows[n]["Tester"].ToString();//检测员
                            //BaseUnitInfo[4] = cdt.Rows[n]["TestUnitAddr"].ToString();//产地地址
                            Global.shengchandanwei = cdt.Rows[n]["TestUnitName"].ToString();
                            Global.chandi = cdt.Rows[n]["ProductAddr"].ToString();
                            Global.shengchanqiye = cdt.Rows[n]["DetectUnitName"].ToString();
                            Global.jianceyuan = cdt.Rows[n]["Tester"].ToString();
                            Global.chandidizhi = cdt.Rows[n]["TestUnitAddr"].ToString();
                            checkUnit = cdt.Rows[n]["DetectUnitName"].ToString();
                            decUnit = cdt.Rows[n]["TestUnitName"].ToString();
                        }
                        cmbChkUnit.Items.Add(cdt.Rows[n]["TestUnitName"].ToString());//生产单位
                        cmbDetectUnit.Items.Add(cdt.Rows[n]["DetectUnitName"].ToString());//生产企业
                        cmbGetSampleAddr.Items.Add(cdt.Rows[n]["ProductAddr"].ToString());//产地
                        cmbChker.Items.Add(cdt.Rows[n]["Tester"].ToString());//检测员
                        cmbGenertAddr.Items.Add(cdt.Rows[n]["TestUnitAddr"].ToString());
                    } 
                }

                //dy.savediary(DateTime.Now.ToString(), "打开LZ-4000", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "打开LZ-4000", "失败：" + ex.Message);
                MessageBox.Show(ex.Message, "打开LZ-4000");
            }
        }

        private void cmbDetectUnitNature_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbDetectUnitNature_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void cmbProductCompany_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private  void cmbProductCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductCompany_MouseClick(object sender, MouseEventArgs e)
        {
            
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
        /// <summary>
        /// 刷新串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnRefresh_Click(object sender, EventArgs e)
        {

            string[] Port = SerialPort.GetPortNames();
            cmbCOMbox.Items.Clear();
            if (Port.Length == 0)
            {
                cmbCOMbox.Items.Add("没有COM口");
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCOMbox.Items.Add(c);
            }
            cmbCOMbox.SelectedIndex = 0;
            btnlinkcom.Enabled = true;

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
                //CheckDatas.Columns["生产日期"].Width = 180;
                //CheckDatas.Columns["送检日期"].Width = 180;
                CheckDatas.Columns["生产单位"].Visible = false;
                CheckDatas.Columns["生产企业"].Visible = false;
                CheckDatas.Columns["产地地址"].Visible = false;
                CheckDatas.Columns["条形码"].Visible = false;

                CheckDatas.Columns["采样时间"].Visible = false;
                CheckDatas.Columns["采样地点"].Visible = false;
         
                CheckDatas.Columns["生产日期"].Visible = false;
                CheckDatas.Columns["送检日期"].Visible = false;

                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells["被检单位"].Value = checkUnit;
                    CheckDatas.Rows[i].Cells["检测单位"].Value = decUnit;
                    //CheckDatas.Rows[i].Cells["生产企业"].Value = Global.shengchanqiye;
                    CheckDatas.Rows[i].Cells["产地"].Value = Global.chandi;
                    CheckDatas.Rows[i].Cells["检测员"].Value = Global.jianceyuan;
                    if (CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "阳性")
                    {
                        CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    } 
                }
                CheckDatas.Refresh();
            }
            BtnReadHis.Enabled = true ;
            BtnClear.Enabled = true ;        
        }

      
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
                        DataTable dt = sqlSet.GetItemStandard("FtypeNmae='" + resultdata.SampleName + "'", "", out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resultdata.SampleCode = dt.Rows[0]["SampleNum"].ToString();
                        }
                        resultdata.CheckNumber = Global.GUID("N", 1);
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
                        resultdata.MachineCode = "LZ4000";

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
                    //if (CheckDatas.CurrentCell.ColumnIndex == 9)//生产单位
                    //{
                    //    cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbChkUnit.Left = rect.Left;
                    //    cmbChkUnit.Top = rect.Top;
                    //    cmbChkUnit.Width = rect.Width;
                    //    cmbChkUnit.Height = rect.Height;
                    //    cmbChkUnit.Visible = true;
                    //    cmbAdd.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbGenertAddr.Visible = false;
                    //}
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 10 || CheckDatas.CurrentCell.ColumnIndex >= 15 )//采样时间
                    //{
                    //    if (e.ColumnIndex == 15)//数量单位
                    //    {
                    //        //cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
                    //        cmbAdd.Items.Clear();
                    //        cmbAdd.Items.Add("以下相同");
                    //        cmbAdd.Items.Add("删除");
                    //        cmbAdd.Items.Add("吨");
                    //        cmbAdd.Items.Add("公斤");
                    //        cmbAdd.Items.Add("斤");
                    //        cmbAdd.Items.Add("两");
                    //        cmbAdd.Items.Add("克");
                    //        cmbAdd.Items.Add("毫克");
                    //        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbAdd.Left = rect.Left;
                    //        cmbAdd.Top = rect.Top;
                    //        cmbAdd.Width = rect.Width;
                    //        cmbAdd.Height = rect.Height;
                    //        cmbChkUnit.Visible = false;
                    //        cmbAdd.Visible = true;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = false;
                    //        cmbGetSampleAddr.Visible = false;
                    //        cmbGenertAddr.Visible = false;
                    //    }
                    //    else if (e.ColumnIndex == 21)//生产企业
                    //    {
                    //        cmbDetectUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbDetectUnit.Left = rect.Left;
                    //        cmbDetectUnit.Top = rect.Top;
                    //        cmbDetectUnit.Width = rect.Width;
                    //        cmbDetectUnit.Height = rect.Height;
                    //        cmbChkUnit.Visible = false;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = true;
                    //        cmbGetSampleAddr.Visible = false;
                    //        cmbAdd.Visible = false;
                    //        cmbGenertAddr.Visible = false;
                    //    }
                    //    else if (e.ColumnIndex == 19)//生产单位
                    //    {
                    //        cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbChkUnit.Left = rect.Left;
                    //        cmbChkUnit.Top = rect.Top;
                    //        cmbChkUnit.Width = rect.Width;
                    //        cmbChkUnit.Height = rect.Height;
                    //        cmbChkUnit.Visible = true;
                    //        cmbAdd.Visible = false;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = false;
                    //        cmbGetSampleAddr.Visible = false;
                    //        cmbGenertAddr.Visible = false;
                    //    }
                    //    else if (e.ColumnIndex == 14)//产地
                    //    {
                    //        cmbGetSampleAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbGetSampleAddr.Left = rect.Left;
                    //        cmbGetSampleAddr.Top = rect.Top;
                    //        cmbGetSampleAddr.Width = rect.Width;
                    //        cmbGetSampleAddr.Height = rect.Height;
                    //        cmbChkUnit.Visible = false;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = false;
                    //        cmbGetSampleAddr.Visible = true;
                    //        cmbAdd.Visible = false;
                    //        cmbGenertAddr.Visible = false;
                    //    }
                    //    else if (e.ColumnIndex == 20)//产地地址
                    //    {
                    //        cmbGenertAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbGenertAddr.Left = rect.Left;
                    //        cmbGenertAddr.Top = rect.Top;
                    //        cmbGenertAddr.Width = rect.Width;
                    //        cmbGenertAddr.Height = rect.Height;
                    //        cmbGenertAddr.Visible = true;
                    //        cmbChkUnit.Visible = false;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = false;
                    //        cmbGetSampleAddr.Visible = false;
                    //        cmbAdd.Visible = false;
 
                    //    }
                    //    else
                    //    {
                    //        cmbAdd.DropDownStyle = ComboBoxStyle.DropDown;
                    //        cmbAdd.Items.Clear();
                    //        cmbAdd.Items.Add("以下相同");
                    //        cmbAdd.Items.Add("删除");
                    //        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                    //        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //        cmbAdd.Left = rect.Left;
                    //        cmbAdd.Top = rect.Top;
                    //        cmbAdd.Width = rect.Width;
                    //        cmbAdd.Height = rect.Height;
                    //        cmbChkUnit.Visible = false;
                    //        cmbAdd.Visible = true;
                    //        cmbChker.Visible = false;
                    //        cmbDetectUnit.Visible = false;
                    //        cmbGetSampleAddr.Visible = false;
                    //        cmbGenertAddr.Visible = false ;
                    //    }   
                    //}
                    if (CheckDatas.CurrentCell.ColumnIndex == 13)//被检单位
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 14)//检测员
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
                    else if (e.ColumnIndex == 18)//数量单位
                    {
                        //cmbAdd.DropDownStyle = ComboBoxStyle.DropDownList;
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
                    else if (e.ColumnIndex == 16)//产地
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
                    else if (e.ColumnIndex == 10)//检测单位
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
                        cmbGenertAddr.Visible = false;
                    }   
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), Global.ChkManchine + "错误：" + ex.Message, "错误");
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
                if(Global.Ticket =="")
                {
                    MessageBox.Show("上传失败！！！\r\n上传数据前先进行通信测试！","系统提示",MessageBoxButtons.OK ,MessageBoxIcon.Information );
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
                int failItem = 0;

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
                        StringBuilder xmlResult = new StringBuilder();
                        xmlResult.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        xmlResult.Append("<request>");
                        xmlResult.Append("<head>");
                        xmlResult.AppendFormat("<data_count>{0}</data_count>", 1);
                        xmlResult.Append("</head>");
                        xmlResult.Append("<dataset>");
                        xmlResult.Append("<data no=\"1\">");
                        xmlResult.AppendFormat("<ID>{0}</ID>", dtb.Rows[i]["ChkNum"].ToString().Replace("-", "").Trim());
                        xmlResult.AppendFormat("<SAMPLE>{0}</SAMPLE>", dtb.Rows[i]["SampleName"].ToString());
                        xmlResult.AppendFormat("<CHECKITEM>{0}</CHECKITEM>", dtb.Rows[i]["Checkitem"].ToString());
                        xmlResult.AppendFormat("<CHECKRESULT>{0}</CHECKRESULT>", dtb.Rows[i]["Result"].ToString() == "合格" ? "1" : "2");
                        xmlResult.AppendFormat("<CHECKTIME>{0}</CHECKTIME>", dtb.Rows[i]["CheckTime"].ToString().Replace("/", "-"));
                        xmlResult.AppendFormat("<SENDCOMPANY>{0}</SENDCOMPANY>", dtb.Rows[i]["CheckUnit"].ToString());
                        xmlResult.AppendFormat("<CHECKVALUE>{0}</CHECKVALUE>", dtb.Rows[i]["CheckData"].ToString());
                        xmlResult.AppendFormat("<UNIT>{0}</UNIT>", dtb.Rows[i]["Unit"].ToString());
                        xmlResult.AppendFormat("<CITY>{0}</CITY>", dtb.Rows[i]["ProductPlace"].ToString());
                        xmlResult.AppendFormat("<SUPPLIER>{0}</SUPPLIER>", dtb.Rows[i]["ProcodeCompany"].ToString());
                        xmlResult.AppendFormat("<SAMPLENO>{0}</SAMPLENO>", dtb.Rows[i]["SampleCode"].ToString());
                        xmlResult.AppendFormat("<CHANNEL>{0}</CHANNEL>", dtb.Rows[i]["HoleNum"].ToString());
                        xmlResult.AppendFormat("<REFINFO>{0}</REFINFO>", dtb.Rows[i]["TestBase"].ToString());
                        xmlResult.AppendFormat("<REFVALUE>{0}</REFVALUE>", dtb.Rows[i]["LimitData"].ToString());
                        xmlResult.AppendFormat("<GREATKIND>{0}</GREATKIND>", "食用农产品");
                        xmlResult.AppendFormat("<LITTLEKIND>{0}</LITTLEKIND>", "果菜类");
                        xmlResult.AppendFormat("<A1>{0}</A1>", "0");
                        xmlResult.AppendFormat("<A2>{0}</A2>", "0");
                        xmlResult.AppendFormat("<CHECKER>{0}</CHECKER>", dtb.Rows[i]["Tester"].ToString());
                        xmlResult.AppendFormat("<DEVICE>{0}</DEVICE>", dtb.Rows[i]["Machine"].ToString());
                        xmlResult.AppendFormat("<LONGITUDE>{0}</LONGITUDE>", "0");
                        xmlResult.AppendFormat("<LATITUDE>{0}</LATITUDE>", "0");
                        xmlResult.Append("</data>");
                        xmlResult.Append("</dataset>");
                        xmlResult.Append("</request>");

                        com.tainot.foodsafe.wireDataService wds = new com.tainot.foodsafe.wireDataService();
                        string rt = wds.Upload(Global.Ticket, xmlResult.ToString());
                        if (rt != "")
                        {
                            analysedata(rt);
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(rt);//加载XML字符串为XML文档
                            //查找<response>    
                            XmlNode root = xmlDoc.SelectSingleNode("response");
                            //获取到所有<response>的子节点    
                            XmlNodeList nodeList = xmlDoc.SelectSingleNode("response").ChildNodes;
                            string success = "";
                            string errMsg = "";
                            //遍历所有子节点   
                            foreach (XmlNode xn in nodeList)
                            {
                                string name = xn.Name;
                                Console.WriteLine(name);

                                XmlElement xe = (XmlElement)xn;
                                XmlNodeList subList = xe.ChildNodes;

                                if (xn.Name == "head")
                                {
                                    foreach (XmlNode xmlNode in subList)
                                    {
                                        if ("success_error".Equals(xmlNode.Name))
                                        {
                                            success = xmlNode.InnerText;
                                            Console.WriteLine("成功：" + xmlNode.InnerText);
                                        }
                                        else if ("success_count".Equals(xmlNode.Name))
                                        {
                                            Console.WriteLine("成功：" + xmlNode.InnerText);
                                        }
                                        else if ("error_count".Equals(xmlNode.Name))
                                        {
                                            Console.WriteLine("成功：" + xmlNode.InnerText);
                                        }
                                    }
                                }
                                else if (xn.Name == "success_dataset")
                                {
                                    foreach (XmlNode xmlNode in subList)
                                    {
                                        if ("data".Equals(xmlNode.Name))
                                        {
                                            string id = xmlNode.Attributes["id"].Value;//通过Attributes获得属性名为id的属性
                                        }
                                    }

                                }
                                else if (xn.Name == "error_dataset")
                                {
                                    foreach (XmlNode xmlNode in subList)
                                    {
                                        if ("data".Equals(xmlNode.Name))
                                        {
                                            string id = xmlNode.Attributes["id"].Value;
                                            string errs = xmlNode.Attributes["error_code"].Value;//通过Attributes获得属性名为id的属性
                                            errMsg = errs;
                                        }
                                    }
                                }
                            }
                            if (success != "1")
                            {
                                string mesg = "";
                                if (errMsg == "0001")
                                {
                                    mesg = "字符串长度过长";
                                }
                                else if (errMsg == "0002")
                                {
                                    mesg = "时间格式不正确";
                                }
                                else if (errMsg == "0003")
                                {
                                    mesg = "数值格式不正确";
                                }
                                else if (errMsg == "0011")
                                {
                                    mesg = "检测设备号不匹配";
                                }
                                else if (errMsg == "0012")
                                {
                                    mesg = "检测样品号不匹配";
                                }
                                else if (errMsg == "0013")
                                {
                                    mesg = "检测项目号不匹配";
                                }
                                else if (errMsg == "0014")
                                {
                                    mesg = "产地号不匹配";
                                }
                                else if (errMsg == "0015")
                                {
                                    mesg = "检测单位号不匹配";
                                }
                                else if (errMsg == "0016")
                                {
                                    mesg = "被检单位号不匹配";
                                }
                                else if (errMsg == "0091")
                                {
                                    mesg = "为知错误";
                                }
                                errstr = errstr + mesg;
                                failItem++;
                            }
                            else if (success == "1")
                            {
                                upok = upok + 1;
                                sqlSet.SetUploadResult(dtb.Rows[i]["ID"].ToString(), out err);
                                CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;//修改上传数据背景颜色
                            }
                        }

                       
                    }
                }
                if (errstr == "")
                {
                    MessageBox.Show("共成功能够上传" + upok.ToString () + "条数据！", "数据上传");
                }
                else
                {
                    
                    MessageBox.Show("共成功上传" + upok.ToString() + "条数据；共" + isupdata + "条数据已传！\r\n共" + failItem + "条数据上传失败 失败原因：" + errstr, "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnadd.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnadd.Enabled = true;
            }
        }
        private void analysedata(string data)
        { 


        
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
            CheckDatas.Refresh();
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbAdd.Visible = false;
        }

    }
}
