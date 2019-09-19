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
using System.Collections ;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;
using System.Configuration;
using System.Web.Script.Serialization;
using WorkstationModel.UpData;
using System.Threading;

namespace WorkstationUI.machine
{
    public partial class ucTTNJ16 : Basic.BasicContent
    {
        public clsTYTTNJZ24 tyz24 = new clsTYTTNJZ24();
        private clsTY16 ty16 = new clsTY16();
        public ComboBox cmbAdd = new ComboBox();//下拉列表
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private clsUpLoadData udata = new clsUpLoadData();
        private clsSetSqlData sql = new clsSetSqlData();
        public string protocol = string.Empty;//通信协议
        private int rc = 0;
        private int col = 0;
        public delegate void Rshowdata();
        private string messagedata = string.Empty;
        private string mesgaecode = string.Empty;
        //private  List<byte> intList = new List<byte>();
        
        public ucTTNJ16()
        {
            InitializeComponent();
        }

        private void ucTTNJ16_Load(object sender, EventArgs e)
        {
            LbTitle.Text = Global.ChkManchine;
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
            //cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
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

            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
            clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
            
            //CheckDatas.DataSource = clsTY16.DataReadTable;
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
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnadd_Click(object sender, EventArgs e)
        {
            btnadd.Enabled = false;
            Global.ServerAdd = ConfigurationManager.AppSettings["ServerAddr"];
            Global.ServerName = ConfigurationManager.AppSettings["ServerName"];
            Global.ServerPassword = ConfigurationManager.AppSettings["ServerPassword"];
            string err = "";
            int iok = 0;
            int ino = 0;
            try
            {
                //登录
                string rt = TYUpData.Logon();

                JavaScriptSerializer js = new JavaScriptSerializer();
                TYUpData.login list = js.Deserialize<TYUpData.login>(rt);    //将json数据转化为对象类型并赋值给list
                Global.CompanyCode = list.qycode;
                Global.UserCode = list.userid;

                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    udata.shuliang = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();
                    udata.ttime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();
                    udata.chker = CheckDatas.Rows[i].Cells[16].Value.ToString().Trim();

                    udata.duixiang = CheckDatas.Rows[i].Cells[1].Value.ToString().Trim();
                    udata.chkdata = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                    udata.companyCode = Global.CompanyCode;
                    if (CheckDatas.Rows[i].Cells[8].Value.ToString().Trim() == "阴性" || CheckDatas.Rows[i].Cells[8].Value.ToString().Trim() == "合格")
                    {
                        udata.Conclusion = "合格";
                        udata.hegelv = "100";//合格率
                    }
                    else
                    {
                        udata.Conclusion = "不合格";
                        udata.hegelv = "0";//合格率
                    }

                    string rd = TYUpData.UpData(udata);

                    JavaScriptSerializer jsup = new JavaScriptSerializer();
                    TYUpData.Upload retu = jsup.Deserialize<TYUpData.Upload>(rd);    //将json数据转化为对象类型并赋值给list
                    if (retu.isSucess == "上传成功")
                    {
                        CheckDatas.Rows[i].DefaultCellStyle.BackColor = Color.Green;//上传成功变绿色
                        iok = iok + 1;
                        clsUpdateData ud = new clsUpdateData();
                        ud.result = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                        ud.ChkTime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();
                        sql.SetUpLoadData(ud, out err);
                    }
                    else
                    {
                        ino = ino + 1;
                    }
                }
                MessageBox.Show("共成功上传" + iok + "条数据，失败" + ino + "条数据", "数据上传");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            btnadd.Enabled = true;
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {

        }
      
        private void cmbSample_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text;
        }
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[rc].Cells[col].Value = cmbAdd.Text;
            //CheckDatas.CurrentCell.Value = cmbAdd.Text;
        }
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
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
        /// 消息更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clsUpdateMessage_LabelUpdated(object sender, clsUpdateMessage.LabelUpdateEventArgs e)
        {         
             LbTitle.Text =Global.ChkManchine;         
        }
         /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            //if (e.Code.ToString() == "ReadTY16")
            //{
            //    if (e.Message == "1B01" || e.Message == "1B02")
            //    {
            //        txtlink.Text = "已创建连接";
            //    }
            //    else if (e.Message == "JX")
            //    {
            //        ty16.SendToInstrument("24");
            //        Global.SendToTYData = "24";
            //    }
            //    else if (e.Message == "ReadOver")
            //    {
            //        ShowData();
            //        Global.SendToTYData = "";
            //    }
            //}
            if (e.Code.ToString() == "ReadTY16")
            {
                mesgaecode = e.Code.ToString();
                messagedata = e.Message;
                showdata();
            }
            else if (e.Code.ToString() == "ReadTYZ24")//读取24通道农残仪
            {
                mesgaecode = e.Code.ToString();
                messagedata = e.Message;
                showdata();
            }
        }
        /// <summary>
        /// 数据显示
        /// </summary>
        private void showdata()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new Rshowdata(showdata));
                }
                catch (ObjectDisposedException)
                {
                    return;
                }
            }
            else
            {
                if (mesgaecode == "ReadTY16")
                {
                    if (messagedata == "1B01" || messagedata == "1B02")
                    {
                        //Thread.Sleep(50);
                        txtlink.Text = "已创建连接";
                        //Thread.Sleep(600);
                        //ty16.SendToInstrument("1BB1");
                        //Global.SendToTYData = "1BB1";
                    }
                    else if (messagedata == "JX")
                    {
                        Thread.Sleep(100);
                        ty16.SendToInstrument("24");
                        Global.SendToTYData = "24";
                    }
                    else if (messagedata == "ReadOver")
                    {
                        ShowData();
                        Global.SendToTYData = "";
                        Thread.Sleep(100);
                        //清除数据
                        //ty16.SendToInstrument("1BB9");
                        Thread.Sleep(200);//延时等待单片机处理程序
                        //断开连接
                        ty16.SendToInstrument("1BFF");
                        txtlink.Text = "未连接";
                        btnlinkcom.Text = "连接设备";
                        BtnReadHis.Enabled = true;
                    }
                }
                else if (mesgaecode == "ReadTYZ24")
                {
                    if (messagedata == "1BC1")
                    {
                        txtlink.Text = "已创建连接";
                        btnlinkcom.Enabled = true;
                        btnlinkcom.Text = "断开连接";
                    }
                    else if (messagedata == "1BFE")
                    {
                        BtnReadHis.Enabled = true;

                        CheckDatas.DataSource = clsTYTTNJZ24.DataReadTable;
                        for (int i = 0; i < CheckDatas.Rows.Count; i++)
                        {
                            CheckDatas.Columns[7].Width = 170;
                            CheckDatas.Columns[9].Width = 170;
                            if (CheckDatas.Rows[i].Cells[8].Value.ToString() == "超标")
                            {
                                CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                        MessageBox.Show("数据采集完成", "提示");
                    }
                    else if (messagedata == "Z24")
                    {
                        CheckDatas.DataSource = clsTYTTNJZ24.DataReadTable;
                    }
                }
            }
        }
        private void ShowData()
        {
            CheckDatas.DataSource = clsTY16.DataReadTable;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                CheckDatas.Columns[7].Width = 170;
                CheckDatas.Columns[9].Width = 170;
                if (CheckDatas.Rows[i].Cells[8].Value.ToString().Trim() == "不合格")
                {
                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

        }
        public void closecom()
        {
            ty16.Close();
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
        /// <summary>
        /// 打开串口、连接设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            btnlinkcom.Enabled = false;
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            try
            {
                //if (!ty16.Online)
                //{
                //    ty16.Open();
                //    if(ty16.Online ==true)
                //    {
                //        //发送握手指令
                //        ty16.SendToInstrument("1BBF");
                //        Global.SendToTYData = "1BBF";
                //        //txtlink.Text = "已创建连接";
                //    }
                //}
                //else
                //{
                //    txtlink.Text = "未创建连接";
                //    MessageBox.Show("串口已打开", "提示");
                //}

                if (btnlinkcom.Text == "连接设备")
                {
                    if (protocol == "RS232TYZ24" || protocol == "RS232TYZ16")
                    {
                        string Pdata = tyz24.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");
                        if (Pdata == "OK")
                        {
                            tyz24.SendData("1B BF");
                            Global.SendData = "1BBF";
                        }
                    }
                    else if (protocol == "RS232TY8" || protocol == "RS232TY16")
                    {
                        if (ty16.Online != true)
                        {
                            ty16.Open();
                            btnlinkcom.Text = "断开连接";
                        }
                        if (ty16.Online == true)
                        {
                            //发送握手指令
                            ty16.SendToInstrument("1BBF");
                            Global.SendToTYData = "1BBF";
                            txtlink.Text = "已创建连接";
                            btnlinkcom.Enabled = true;
                        }
                    }
                }
                else if (btnlinkcom.Text == "断开连接")
                {
                    if (protocol == "RS232TYZ24" || protocol == "RS232TYZ16")
                    {
                        string Pdata = tyz24.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");//判断打开串口后关闭
                        if (Pdata == "OK")
                        {
                            btnlinkcom.Text = "连接设备";
                            txtlink.Text = "未连接";
                            btnlinkcom.Enabled = true;
                        }
                    }
                    else if (protocol == "RS232TY8" || protocol == "RS232TY16")
                    {
                        ty16.SendToInstrument("1BFF");
                        Thread.Sleep(50);
                        ty16.Close();
                        btnlinkcom.Text = "连接设备";
                        txtlink.Text = "未连接";
                        btnlinkcom.Enabled = true;
                    }
                    //MessageBox.Show("串口已打开", "提示");
                }


                //btnlinkcom.Enabled = true;
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }      
        }
        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            BtnReadHis.Enabled = false;
            try
            {
                if (protocol == "RS232TYZ24" || protocol == "RS232TYZ16")
                {
                    tyz24.SendData("24");
                    Global.SendData = "24";
                }
                else if (protocol == "RS232TY8" || protocol == "RS232TY16")
                {
                    ty16.SendToInstrument("1BB1");
                    Global.SendToTYData = "1BB1";
                    clsTY16.DataReadTable.Clear();
                }

            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            BtnReadHis.Enabled = true;
        }
        //数据保存
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            btnDatsave.Enabled = false;
            int isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells[0].Value.ToString().Trim() != "是")
                        {
                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.SampleName = CheckDatas.Rows[i].Cells[1].Value.ToString().Trim();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
                            //chk = CheckDatas.Rows[i].Cells[9].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();
                            resultdata.detectunit = CheckDatas.Rows[i].Cells[10].Value.ToString().Trim();//检测单位
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();//检测数量
                            resultdata.holeSize = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();//
                            resultdata.Gettime = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();//采样时间
                            resultdata.Getplace = CheckDatas.Rows[i].Cells[14].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells[15].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells[16].Value.ToString().Trim();

                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if (iok == 1)
                            {
                                isave = isave + 1;
                                CheckDatas.Rows[i].Cells[0].Value = "是";
                            }
                        }
                    }
                    if (isave == 0)
                    {
                        MessageBox.Show("数据已成功保存过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("数据保存成功，共保存" + isave + "条数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnDatsave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            BtnClear.Enabled = false;

            CheckDatas.DataSource = null;

            BtnClear.Enabled = true;
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
                    else
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
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
            //cmbSample.Visible = false;
            //cmbChkItem.Visible = false;
            //cmbAdd.Visible = false;
            //rc = e.RowIndex;
            //col = e.ColumnIndex;
            //if (CheckDatas.CurrentCell.ColumnIndex < 1)
            //{
            //    CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
            //    cmbSample.Visible = false;
            //    cmbChkItem.Visible = false;
            //    cmbAdd.Visible = false;
            //    return;
            //}
            //try
            //{
            //    if (CheckDatas.CurrentCell.ColumnIndex == 1|| CheckDatas.CurrentCell.ColumnIndex == 10 || CheckDatas.CurrentCell.ColumnIndex == 11 || CheckDatas.CurrentCell.ColumnIndex == 12
            //       || CheckDatas.CurrentCell.ColumnIndex == 13 || CheckDatas.CurrentCell.ColumnIndex == 14 || CheckDatas.CurrentCell.ColumnIndex == 15 || CheckDatas.CurrentCell.ColumnIndex == 16)
            //    {
            //        cmbSample.Visible = false;
            //        cmbChkItem.Visible = false;
            //        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();

            //        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
            //        cmbAdd.Left = rect.Left;
            //        cmbAdd.Top = rect.Top;
            //        cmbAdd.Width = rect.Width;
            //        cmbAdd.Height = rect.Height;
            //        cmbAdd.Visible = true;
 
            //    }             
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //}
        }
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {           
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;   
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            CheckDatas.Refresh();
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;   
        }
    }
}
