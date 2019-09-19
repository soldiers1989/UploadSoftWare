using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WorkstationUI.Basic;
using WorkstationUI.Test;
using WorkstationUI.machine;
using WorkstationUI.function;
using WorkstationBLL.Mode;
using WorkstationModel.Model;
using System.Threading;
using WorkstationDAL.Basic;
using WorkstationDAL.Model;
using WorkstationModel.Instrument;
using System.Configuration;
using System.Runtime.InteropServices;

namespace WorkstationUI
{
    public partial class MainForm : Form
    {
        #region 全局变量定义
        private clsdiary dy = new clsdiary();
        private System.Windows.Forms.Timer D_timer = null;
        private clsSetSqlData SQLstr = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private  int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;//窗体的宽度
        private ucDY5000 dy5000serial = new ucDY5000();
        private ucDY5800 dy5800 = new ucDY5800();
        private ucDY2620 dy2620 = new ucDY2620();
        private ucDY1000 dyserial = new ucDY1000();//DY1000、DY2000、DY3000、LZ3000
        private UCAll_LZ4000 AllLZ4000 = new UCAll_LZ4000();
        private UControl_LZ4000 ln4000 = new UControl_LZ4000();//辽宁版的LZ4000      
        private ucDY6100 dy6100 = new ucDY6100();
        private ucDY6200 dy6200 = new ucDY6200();//YD6200协议
        private ucDY64000 dy6400 = new ucDY64000();
        private ucDYRS2 dyrs2 = new ucDYRS2();
        private ucDY8120 dy8120 = new ucDY8120();
        private ucDY8200 dy8200 = new ucDY8200();
        private ucTTNJ16 ty16 = new ucTTNJ16();//湖北泰扬
        private ucTL310 tl310 = new ucTL310();//自主开发的TL310
        private ucDY7200 dy7200 = new ucDY7200();//外购DY7200
        private ucJunLuo jljsq = new ucJunLuo();//菌落计数器
        private ucSearchData UCSD = new ucSearchData();
        private ucEquipmenManage UCEM = new ucEquipmenManage();
        private MainPage mp = new MainPage();
        private string[] sysSetname = { "仪器管理", "网络设置", "操作日记", "用户管理", "系统帮助", "关于系统", "系统升级", "注销用户" };
        private DataTable dt = null;
        private Label lbFunction;
        private Label lbTask;
        private Label labelSysSet;
        private Label labelDataCenter;
        private string selectfuntion = string.Empty;
        private ucLZ2000 lz2000 = new ucLZ2000();
        private clsUpdateMessage cum = new clsUpdateMessage();
        private string err = string.Empty;
      
        #endregion
        /// <summary>
        /// 返回该窗体的唯一实例。如果之前该窗体没有被创建，进行创建并返回该窗体的唯一实例。
        /// 此处采用单键模式对窗体的唯一实例进行控制，以便外界窗体或控件可以随时访问本窗体。
        /// </summary>
        //public static MainForm mainform
        //{
        //    get
        //    {
        //        if (mainForm == null)
        //        {
        //            mainForm = new MainForm();
        //        }
        //        return mainForm;
        //    }
        //}
        private int ic = 0;
        public void timerRead(object sender, EventArgs e)
        {
            labelTime.Text =System.DateTime.Now.ToString();
            ic = ic + 1;
            if(ic==5)
            {
                ic = 0;
                ClearMemory();
            }
        }
 
        public MainForm()
        {
            InitializeComponent();             
        }
        #region WinForm窗体重绘
        const int WM_NCHITTEST = 0x0084;
        const int HT_LEFT = 10;
        const int HT_RIGHT = 11;
        const int HT_TOP = 12;
        const int HT_TOPLEFT = 13;
        const int HT_TOPRIGHT = 14;
        const int HT_BOTTOM = 15;
        const int HT_BOTTOMLEFT = 16;
        const int HT_BOTTOMRIGHT = 17;
        const int HT_CAPTION = 2;
        //protected override void WndProc(ref Message Msg)
        //{
        //    //禁止双击最大化
        //    if (Msg.Msg == 0x0112 && Msg.WParam.ToInt32() == 61490) return;
        //    if (Msg.Msg == WM_NCHITTEST)
        //    {
        //        //获取鼠标位置
        //        //int nPosX = (Msg.LParam.ToInt32() & 65535);
        //        //int nPosY = (Msg.LParam.ToInt32() >> 16);

        //        ////右下角
        //        //if (nPosX >= this.Right - 6 && nPosY >= this.Bottom - 6)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_BOTTOMRIGHT);
        //        //    return;
        //        //}
        //        ////左上角
        //        //else if (nPosX <= this.Left + 6 && nPosY <= this.Top + 6)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_TOPLEFT);
        //        //    return;
        //        //}
        //        ////左下角
        //        //else if (nPosX <= this.Left + 6 && nPosY >= this.Bottom - 6)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_BOTTOMLEFT);
        //        //    return;
        //        //}
        //        ////右上角
        //        //else if (nPosX >= this.Right - 6 && nPosY <= this.Top + 6)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_TOPRIGHT);
        //        //    return;
        //        //}
        //        //else if (nPosX >= this.Right - 2)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_RIGHT);
        //        //    MessageBox.Show("5");
        //        //    return;
        //        //}
        //        //else if (nPosY >= this.Bottom - 2)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_BOTTOM);
        //        //    MessageBox.Show("6");
        //        //    return;
        //        //}
        //        //else if (nPosX <= this.Left + 2)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_LEFT);
        //        //    return;
        //        //}
        //        //else if (nPosY <= this.Top + 2)
        //        //{
        //        //    Msg.Result = new IntPtr(HT_TOP);
        //        //    MessageBox.Show("8");
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //允许拖动窗体移动
        //        Msg.Result = new IntPtr(HT_CAPTION);
        //        return;
        //        //}
        //    }
        //    base.WndProc(ref Msg);
        //}
        #endregion
        /// <summary>
        /// 设置窗体圆角
        /// </summary>
        private void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 10);
            this.Region = new Region(FormPath);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            // 左上角
            path.AddArc(arcRect, 180, 90);
            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //使窗体最大化时不覆盖任务栏
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //重绘时使用双缓存解决闪烁问题
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Refresh();
            SetWindowRegion();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Global.maxwindow = true;
                //加载配置信息
                Global.ServerAdd = ConfigurationManager.AppSettings["ServerAddr"];//服务器地址
                Global.ServerName = ConfigurationManager.AppSettings["ServerName"];//用户名
                Global.ServerPassword = ConfigurationManager.AppSettings["ServerPassword"];//密码
                Global.DetectUnit = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位
                Global.CheckUnitcode = ConfigurationManager.AppSettings["UpDetectUnitNo"];
                Global.OrganizeName = ConfigurationManager.AppSettings["Organize"];
                Global.OrganizeNo = ConfigurationManager.AppSettings["OrganizeNo"];
                Global.DetectUnitType = ConfigurationManager.AppSettings["ChkUnitType"];
                Global.NickName = ConfigurationManager.AppSettings["NickName"];//检测员
                Global.pointID = ConfigurationManager.AppSettings["pointID"];
                Global.IntrumManifacture = ConfigurationManager.AppSettings["InstrumManufact"];
                Global.MachineSerialCode = ConfigurationManager.AppSettings["IntrumentSeriersNum"];//仪器系列号
                Global.Platform = ConfigurationManager.AppSettings["InterfaceManufacture"];//平台厂家
                Global.MachineModel = ConfigurationManager.AppSettings["InstrumentNameModel"];//设备型号
                Global.MachineProductor = ConfigurationManager.AppSettings["InstrumManufact"];//设备厂家
                Global.maxwindow = true;
                panelProject.Visible = false;//显示项目的panel
                ucSecondTitle user = new ucSecondTitle();
                PnLeft.Controls.Add(user);
                //显示当前日期和时间
                labelTime.Text = System.DateTime.Now.ToString();
                picboxMain.Visible = false;
                panelMain.Visible = true;
                PnlCollect.Visible = true;
                picBoxCollect.Visible = false;
                Mainpanel.Controls.Clear();
                Mainpanel.Width = iActulaWidth - 20;
                mp.Width = iActulaWidth - 20;
                Mainpanel.Controls.Add(mp);
                Global.currentform = "主页";
                D_timer = new System.Windows.Forms.Timer();
                D_timer.Interval = 1000;
                D_timer.Tick += new EventHandler(timerRead);
                D_timer.Enabled = true;
                //查询数据库获取串口设置信息
                dt = SQLstr.SearchData("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    WorkstationDAL.Model.clsShareOption.ComPort = "COM" + dt.Rows[0]["ComPort"].ToString();
                    Global.SerialData = dt.Rows[0]["ComDataBit"].ToString();
                    Global.SerialParity = dt.Rows[0]["ComParity"].ToString();
                    Global.SerialStop = dt.Rows[0]["ComStopBit"].ToString();
                    Global.SerialBaud = dt.Rows[0]["ComBaud"].ToString();
                }
                labeluser.Text = "当前用户：" + Global.userlog;

                if (Global.Platform != "DYKJFW" && Global.Platform != "DYBus")
                {
                    panelTask.Visible = false;
                    picBoxTask.Visible = false;
                    pnlShop.Location = new Point(746, 11);
                    picboxShop.Location = new Point(743, 8);
                }

                dy.savediary(DateTime.Now.ToString(), "系统登录", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "系统登录："+ex.Message, "错误");
                MessageBox.Show(ex.Message,"系统登录");
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       
        //鼠标离开数据采集Panel
        private void skinPanel2_MouseLeave(object sender, EventArgs e)
        {
            PnlCollect.Visible = true;
            if (Global.SearchData == true)
            {
                picBoxCollect.Visible = true;
            }
            else 
            {
                picBoxCollect.Visible = false;
            }
        }
        //鼠标进入数据采集Panel
        private void skinPanel2_MouseEnter(object sender, EventArgs e)
        {
            PnlCollect.Visible = true;
            picBoxCollect.Visible = true;      
        }
        //鼠标进入主页Panel
        private void panelMain_MouseEnter(object sender, EventArgs e)
        {       
            panelMain.Visible =true;
            picboxMain.Visible =true ;
        }
        // //鼠标离开主页Panel
        private void panelMain_MouseLeave(object sender, EventArgs e)
        {
            panelMain.Visible = true ;
            if (Global.MainPage == true)
            {
                picboxMain.Visible = true;
            }
            else
            {
                picboxMain.Visible = false ;
            }
        }
        //鼠标进入操作记录Panel
        private void skinPanel4_MouseEnter(object sender, EventArgs e)
        {
            PnlCollect.Visible = true;
            picBoxData.Visible = true;
        }
        //鼠标离开操作记录Panel
        private void skinPanel4_MouseLeave(object sender, EventArgs e)
        {
            PnlCollect.Visible = true;
            if (Global.Dairypb == true)
            {
                picBoxData.Visible = true;
            }
            else
            {
                picBoxData.Visible = false;
            }
        }
        //鼠标进入系统设计Panel
        private void PanelSysSet_MouseEnter(object sender, EventArgs e)
        {
            PanelSysSet.Visible = true;
            picBoxSysSet.Visible = true;
        }
        //鼠标离开系统设计Panel
        private void PanelSysSet_MouseLeave(object sender, EventArgs e)
        {
            PanelSysSet.Visible = true;
            if (Global.SysSet == true)
            {
                picBoxSysSet.Visible = true;
            }
            else
            {
                picBoxSysSet.Visible = false;
            }
        }

        ///
        ///数据采集单击事件
        ///
        private void skinPanel2_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                //PnLeft.Visible = true;
                panelProject.Visible = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picboxShop.Visible = false;
                picBoxTask.Visible = false;
                Global.SysSet = false;
                Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;
                Global.Task = false;
                       
                Mainpanel.Visible = false;
                panelProject.Controls.Clear();
                Global.SearchData = true;
                PnlContent.Controls.Clear();
                string Protocol = string.Empty;//协议
                string name = string.Empty;//第一个仪器名称
                string lbname = "";//第一个仪器名称的label名
                //查询数据库获取检测仪器名称、串口号-----------------------------------------
                sb.Clear();
                sb.Append("WHERE Isselect='是' order by ID");
                dt = SQLstr.GetIntrument(sb.ToString(), out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Global.TestInstrument = new string[dt.Rows.Count, 2];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Global.TestInstrument[i, 0] = dt.Rows[i]["Name"].ToString();
                        Global.TestInstrument[i, 1] = dt.Rows[i]["Numbering"].ToString();
                    }
                    WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[0]["communication"].ToString();
                    Protocol = dt.Rows[0]["Protocol"].ToString();
                    name = dt.Rows[0]["Name"].ToString();
                    lbname = dt.Rows[0]["Numbering"].ToString();
                }

                panelProject.Controls.Clear();
                for (int t = 0; t < Global.TestInstrument.GetLength(0); t++)
                {
                    lbFunction = new Label();
                    lbFunction.Size = new Size(320, 60);
                    lbFunction.Location = new Point(0, t * 60);
                    lbFunction.Font = new Font("楷体_GB2312", 14);
                    lbFunction.Name = Global.TestInstrument[t, 1];
                    lbFunction.Text = Global.TestInstrument[t, 0];
                    lbFunction.BackColor = Color.Transparent;
                    //lbFunction.AutoSize = false;
                    if (t == 0)
                    {
                        lbFunction.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                    }
                    lbFunction.TextAlign = ContentAlignment.MiddleLeft;
                    lbFunction.MouseClick += lbFunction_MouseClick;
                    lbFunction.MouseEnter += lbFunction_MouseEnter;
                    lbFunction.MouseLeave += lbFunction_MouseLeave;
                  
                    panelProject.Controls.Add(lbFunction);
                }
                Global.ChkManchine = name;  //获取查询第一个仪器的名称
                selectfuntion = lbname;
                Global.MachineNum = lbname;
                Global.CloseCOM = Protocol;
                //根据协议选择测试窗体
                if (Protocol == "RS232DY3000")//DY1000、DY2000、DY3000、DY3100、TL300
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dyserial);
                }
                //else if (Protocol == "RS232DY6600")
                //{
                //    ucDY6600 dy6600 = new ucDY6600();
                //    PnlContent.Controls.Clear();
                //    PnlContent.Controls.Add(dy6600);

                //}
                else if (Protocol == "RS232LZ4000")//LZ4000、LZ4000T
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(AllLZ4000);
                }
                else if (Protocol == "HIDLZ2000")//LZ2000
                {
                    //ucLZ2000 lz2000 = new ucLZ2000();
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(lz2000);
                }
                else if (Protocol == "RS232LNLZ4000")//辽宁版的LZ4000
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(ln4000);
                }
                else if (Protocol == "RS232DY5000")//DY5000、DY5500、DY5600、DY5800
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy5000serial);
                }
                else if (Protocol == "HIDDY6100")//HID协议DY6100
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy6100);
                }
                else if (Protocol == "RS232DY6200")//DY6200协议
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy6200);
                }
                else if (Protocol == "RS232DY6400")//DY6400协议
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy6400);
                }
                else if (Protocol == "RS232DYRS2")//DYRS2协议
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dyrs2);
                }
                else if (Protocol == "RS232DY8120")//DY8120协议
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy8120);
                }
                else if (Protocol == "RS232DY8200")//DY8200协议
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy8200);
                }
                else if (Protocol == "RS232TL310")
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(tl310);
                }
                else if (Protocol == "RS232DY7200")
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy7200);
                }
              
                else if (Protocol == "RS232TY8")//泰扬按键8通道
                {
                    PnlContent.Controls.Clear();
                    ty16.protocol = Protocol;
                    PnlContent.Controls.Add(ty16);
                }
                else if (Protocol == "RS232TY16")//泰扬按键16通道
                {
                    PnlContent.Controls.Clear();
                    ty16.protocol = Protocol;
                    PnlContent.Controls.Add(ty16);
                }
                else if (Protocol == "RS232TYZ16")//泰扬智能型16通道
                {
                    PnlContent.Controls.Clear();
                    ty16.protocol = Protocol;
                    PnlContent.Controls.Add(ty16);
                }
                else if (Protocol == "RS232TYZ24")//泰扬智能型24通道
                {
                    PnlContent.Controls.Clear();
                    ty16.protocol = Protocol;
                    PnlContent.Controls.Add(ty16);
                }
                else if (Protocol == "WSREAD")//菌落计数器
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(jljsq);
                }
                else if (Protocol == "RS232DY5800")
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy5800);
                }
                else if (Protocol == "RS232DY2620")//DY2620食用油品质分析仪
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy2620);
                }

            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "数据采集单击事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"数据采集单击事件");
            }
        }

        private void lbFunction_MouseLeave(object sender, EventArgs e)
        {           
            ((Label)sender).Image = null;
            if (selectfuntion != "")
            {                  
                //查找单击的label添加背景
                (panelProject.Controls.Find(selectfuntion, false)[0] as Label).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }                
        }     
        private void lbFunction_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(((Label)sender), ((Label)sender).Text);
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");             
        }
       
        /// <summary>
        /// 测试仪器单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbFunction_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DialogResult dr = MessageBox.Show("手动输入请选择是，其他输入请选择否", "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        ucHandEnter hand = new ucHandEnter();
                        hand.HandInput = true;
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(hand);
                    }
                    else if (dr == DialogResult.No)
                    {
                        ucHandEnter hand = new ucHandEnter();
                        hand.HandInput = false;
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(hand);
                    }
                }
                else
                {
                    string mprotocol = string.Empty;
                    for (int i = 0; i < Global.TestInstrument.GetLength(0); i++)
                    {
                        (panelProject.Controls.Find(Global.TestInstrument[i, 1], false)[0] as Label).Image = null; //查找所有的label清空背景                            
                    }
                    ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                    selectfuntion = ((Label)sender).Name;
                    Global.MachineNum = ((Label)sender).Name;//仪器编号

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (selectfuntion == dt.Rows[j][5].ToString())
                        {
                            mprotocol = dt.Rows[j][6].ToString();
                            WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[j][2].ToString();
                            Global.ChkManchine = dt.Rows[j][0].ToString();
                        }
                    }
                    Global.CloseCOM = mprotocol;
                    //根据协议选择测试窗体

                    if (mprotocol == "RS232DY3000")//DY1000、DY2000、DY3000、DY3100、TL300
                    {
                        PnlContent.Controls.Clear();
                        dyserial.ucDY1000_Load(null, null);
                        PnlContent.Controls.Add(dyserial);
                        //发送消息通知事件改界面label
                        cum.SendOutMessage("RS232DY3000", Global.ChkManchine);
                    }

                    //else if (mprotocol == "RS232DY6600")
                    //{
                    //    ucDY6600 dy6600 = new ucDY6600();
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(dy6600);

                    //}
                    else if (mprotocol == "RS232LZ4000")//LZ4000、LZ4000T
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(AllLZ4000);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232LZ4000", Global.ChkManchine);
                    }
                    else if (mprotocol == "HIDLZ2000")//LZ2000
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(lz2000);
                    }
                    else if (mprotocol == "RS232LNLZ4000")//辽宁版的LZ4000
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(ln4000);
                    }
                    else if (mprotocol == "RS232DY5000")//DY5000、DY5500、DY5600、DY5800
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy5000serial);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232DY5000", Global.ChkManchine);
                    }
                    else if (mprotocol == "HIDDY6100")//HID协议DY6100
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy6100);
                    }
                    else if (mprotocol == "RS232DY6200")//DY6200协议
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy6200);
                    }
                    else if (mprotocol == "RS232DY6400")//DY6400协议
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy6400);
                    }
                    else if (mprotocol == "RS232DYRS2")//DYRS2协议
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dyrs2);
                    }
                    else if (mprotocol == "RS232DY8120")//DY8120协议
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy8120);
                    }
                    else if (mprotocol == "RS232DY8200")//DY8200协议
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy8200);
                    }
                  
                    else if (mprotocol == "RS232TL310")
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(tl310);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TY8", Global.ChkManchine);
                    }
                    else if (mprotocol == "RS232DY7200")
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy7200);
                    }
                    else if (mprotocol == "RS232TY8")//泰扬按键8通道
                    {
                        PnlContent.Controls.Clear();
                        ty16.protocol = mprotocol;
                        PnlContent.Controls.Add(ty16);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TY8", Global.ChkManchine);
                    }
                    else if (mprotocol == "RS232TY16")//泰扬按键16通道
                    {
                        PnlContent.Controls.Clear();
                        ty16.protocol = mprotocol;
                        PnlContent.Controls.Add(ty16);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TY16", Global.ChkManchine);
                    }
                    else if (mprotocol == "RS232TYZ16")//泰扬智能型16通道
                    {
                        PnlContent.Controls.Clear();
                        ty16.protocol = mprotocol;
                        PnlContent.Controls.Add(ty16);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TYZ16", Global.ChkManchine);
                    }
                    else if (mprotocol == "RS232TYZ24")//泰扬智能型24通道
                    {
                        PnlContent.Controls.Clear();
                        ty16.protocol = mprotocol;
                        PnlContent.Controls.Add(ty16);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TYZ24", Global.ChkManchine);
                    }
                    else if (mprotocol == "WSREAD")//菌落计数器
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(jljsq);
                    }
                    else if (mprotocol == "RS232DY5800")
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy5800);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232DY5800", Global.ChkManchine);
                    }
                    else if (mprotocol == "RS232DY2620")//DY2620食用油品质分析仪
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(dy2620);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232DY2620", Global.ChkManchine);
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "仪器选择事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message ,"选择仪器单击事件");
            }
        }      

        /// <summary>
        /// 数据中心单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void skinPanel4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Global.Dairypb = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = true;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                picBoxTask.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Shop = false;
                Global.Task = false;
                //Global.Dairypb = false;
                panelProject.Visible = true;
                Mainpanel.Visible = false;
               
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    UCSD.Width = 1130;
                }
                
                PnlContent.Controls.Add(UCSD);
               
                Global.currentform = "数据查询";
                panelProject.Controls.Clear();

                for (int i = 0; i < Global.dataCenter.GetLength(0);i++ )
                {
                    labelDataCenter = new Label();
                    labelDataCenter.Size = new Size(220, 60);
                    labelDataCenter.Location = new Point(0, i * 60);
                    labelDataCenter.Font = new Font("楷体_GB2312", 14);
                    labelDataCenter.Name = Global.dataCenter[i, 0];
                    labelDataCenter.Text = Global.dataCenter[i, 1];
                    labelDataCenter.BackColor = Color.Transparent;
                    //lbTask.AutoSize = false;
                    if (i == 0)
                    {
                        labelDataCenter.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                        selectfuntion = Global.dataCenter[i, 0];
                    }
                    labelDataCenter.TextAlign = ContentAlignment.MiddleCenter;
                    labelDataCenter.MouseClick += labelDataCenter_MouseClick;
                    labelDataCenter.MouseEnter += labelDataCenter_MouseEnter;
                    labelDataCenter.MouseLeave += labelDataCenter_MouseLeave;

                    panelProject.Controls.Add(labelDataCenter);
                }
                dy.savediary(DateTime.Now.ToString(), "进入数据中心菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入数据中心菜单", "错误");
                MessageBox.Show(ex.Message, "数据中心单击事件");
            }
        }

        private  void labelDataCenter_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Image = null;
            if (selectfuntion != "")
            {
                //查找单击的label添加背景
                (panelProject.Controls.Find(selectfuntion, false)[0] as Label).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }     
        }

        private  void labelDataCenter_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(((Label)sender), ((Label)sender).Text);
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");  
        }
        /// <summary>
        /// 数据管理菜单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void labelDataCenter_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < Global.dataCenter.GetLength(0); i++)
                {
                    (panelProject.Controls.Find(Global.dataCenter[i, 0], false)[0] as Label).Image = null; //查找所有的label清空背景                            
                }
                ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                selectfuntion = ((Label)sender).Name;
               
                if (selectfuntion == "DataSearch")//数据查询
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(UCSD);
                    UCSD.Width = 1130;
                    UCSD.searchdatabase();
                }
                else if (selectfuntion == "DataStatic")//统计分析
                {
                    ucStatiscal ustatis = new ucStatiscal();
                    if (Global.maxwindow == true)
                    {
                        ustatis.Width = 1140;
                    }
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(ustatis);
                }
                else if (selectfuntion == "UnitCompany")//单位企业、被检单位
                {
                    ucAddUnit un = new ucAddUnit();
                    if (Global.maxwindow == true)
                    {
                        un.Width = 1150;
                    }

                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(un);
                    Global.currentform = "单位信息";
                }
                else if (selectfuntion == "SampleInfo")//样品信息
                {
                    ucAddSample sam = new ucAddSample();
                    if (Global.maxwindow == true)
                    {
                        sam.Width = 1150;
                    }
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(sam);
                }
                else if (selectfuntion == "DataBackup")//数据备份
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
                    string fold = "dataBackup";
                    string fullPath = path + fold;
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    saveFileDialog1.InitialDirectory = fullPath;
                    saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd") + ".bak";
                    saveFileDialog1.CheckPathExists = true;
                    saveFileDialog1.DefaultExt = "bak";
                    saveFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
                    DialogResult dlg = saveFileDialog1.ShowDialog(this);

                    if (dlg == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(path + "Data\\Local.mdb", saveFileDialog1.FileName, true);
                        }
                        catch (Exception ex)
                        {
                            dy.savediary(DateTime.Now.ToString(), "数据库备份失败：" + ex.Message, "错误");
                            MessageBox.Show("数据库备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MessageBox.Show("数据库备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dy.savediary(DateTime.Now.ToString(), "进入数据备份", "成功");
                }
                else if (selectfuntion == "DataReturn")//数据恢复
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
                    string fold = "dataBackup";
                    string fullPath = path + fold;
                    openFileDialog1.InitialDirectory = fullPath;
                    openFileDialog1.CheckPathExists = true;
                    openFileDialog1.CheckFileExists = true;
                    openFileDialog1.DefaultExt = "bak";
                    openFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
                    DialogResult dlg = openFileDialog1.ShowDialog(this);
                    if (dlg == DialogResult.OK)
                    {
                        File.Copy(openFileDialog1.FileName, path + "Data\\Local.mdb", true);
                        MessageBox.Show("数据库恢复成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dy.savediary(DateTime.Now.ToString(), "数据库恢复", "成功");
                    }
                }     
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "任务管理事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "任务管理单击事件");
            }
        }

        /// <summary>
        /// 主页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMain_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Global.currentform = "主页";
                //PnLeft.Visible = false;
                Global.MainPage = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = true;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                picBoxTask.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                //Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;
                Global.Task = false;

                panelProject.Visible = false;
                Mainpanel.Visible = true;
               
                mp.Width = iActulaWidth - 300;
                if (Global.maxwindow == true)
                {
                    Mainpanel.Width = 1400;
                    mp.Width = iActulaWidth - 20;
                }
                Mainpanel.Controls.Clear();
                Mainpanel.Controls.Add(mp);
                dy.savediary(DateTime.Now.ToString(), "进入主页菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入主页菜单：" + ex.Message, "错误");
                MessageBox.Show(ex.Message ,"进入主页");
            }
        }
        /// <summary>
        /// 系统设置单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelSysSet_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //PnLeft.Visible = true;
                Global.SysSet = true;
                picBoxSysSet.Visible = true;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                picBoxTask.Visible = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;
                Global.Task = false;
                panelProject.Visible = true;
                Mainpanel.Visible = false;
                panelProject.Controls.Clear();
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    UCEM.Width = 1130;
                }

                PnlContent.Controls.Add(UCEM);
                Global.currentform = "仪器管理";

                for (int i = 0;i< Global.SystemSet.GetLength(0); i++)
                {
                    labelSysSet = new Label();
                    labelSysSet.Size = new Size(220, 60);
                    labelSysSet.Location = new Point(0, i * 60);
                    labelSysSet.Font = new Font("楷体_GB2312", 14);
                    labelSysSet.Name = Global.SystemSet[i, 0];
                    labelSysSet.Text = Global.SystemSet[i, 1];
                    labelSysSet.BackColor = Color.Transparent;
                    //lbTask.AutoSize = false;
                    if (i == 0)
                    {
                        labelSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                        selectfuntion = Global.SystemSet[i, 0];
                    }
                    labelSysSet.TextAlign = ContentAlignment.MiddleCenter;
                    labelSysSet.MouseClick += labelSysSet_MouseClick;
                    labelSysSet.MouseEnter += labelSysSet_MouseEnter;
                    labelSysSet.MouseLeave += labelSysSet_MouseLeave;

                    panelProject.Controls.Add(labelSysSet);
                }
                dy.savediary(DateTime.Now.ToString(), "进入系统设计菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入系统设计菜单错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入系统设计菜单");
            }
        }

        private  void labelSysSet_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Image = null;
            if (selectfuntion != "")
            {
                //查找单击的label添加背景
                (panelProject.Controls.Find(selectfuntion, false)[0] as Label).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }     
        }

        private  void labelSysSet_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(((Label)sender), ((Label)sender).Text);
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");  
        }
        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void labelSysSet_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < Global.SystemSet.GetLength(0); i++)
                {
                    (panelProject.Controls.Find(Global.SystemSet[i, 0], false)[0] as Label).Image = null; //查找所有的label清空背景                            
                }
                ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                selectfuntion = ((Label)sender).Name;
               
                if (selectfuntion == "MachineManage")//仪器管理
                {
                    PnlContent.Controls.Clear();
                    ucEquipmenManage UCEM = new ucEquipmenManage();
                    UCEM.Width = 1130;
                    PnlContent.Controls.Add(UCEM);
                }
                else if (selectfuntion == "InternetSet")//网络设置、服务器配置
                {
                    frmServer fs = new frmServer();
                    fs.ShowDialog();
                }
                else if (selectfuntion == "SysDairy")//操作日记
                {
                    PnlContent.Controls.Clear();
                    ucDiary dy = new ucDiary();
                    dy.Width = 1130;
                    PnlContent.Controls.Add(dy);
                }
                else if (selectfuntion == "UserManage")//用户管理
                {
                    PnlContent.Controls.Clear();
                    ucUser us = new ucUser();
                    us.Width = 1130;
                    PnlContent.Controls.Add(us);
                }
                else if (selectfuntion == "SysHelp")//系统帮助
                {
                    PnlContent.Controls.Clear();
                    ucHelp uchp = new ucHelp();

                    uchp.Width = 1140;PnlContent.Controls.Add(uchp);
                }
                else if (selectfuntion == "SysAbout")//关于系统
                {
                    frmAbout fa = new frmAbout();
                    fa.Show();
                   
                    dy.savediary(DateTime.Now.ToString(), "进入关于", "成功");
                }
                else if (selectfuntion == "SysUpdate")//系统升级
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe");
                }
                else if (selectfuntion == "SysCancel")//注销系统
                {
                    DialogResult dr = MessageBox.Show("是否退出当前用户", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    //切换账户
                    Application.ExitThread();
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "任务管理事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "任务管理单击事件");
            }
        }

        //清理资源
        /// <summary>
        /// 清空Panel
        /// </summary>
        //public void ClearPanel()
        //{
        //    foreach (Control control in this.Controls)
        //    {
        //        //找到控件名称为PnlContent的Panel
        //        if ((control is Panel) && (control as Panel).Name.Equals("PnlContent"))
        //        {
        //            //然后在panel控件中查找UserControl子控件
        //            foreach (Control subcontrol in control.Controls)
        //            {
        //                //然后清理控件资源
        //                subcontrol.Dispose();
        //            }
        //        }
        //        if ((control is Panel) && (control as Panel).Name.Equals("Mainpanel"))
        //        {
        //            //然后在panel控件中查找子控件
        //            foreach (Control subcontrol in control.Controls)
        //            {
        //                //然后清理控件资源
        //                subcontrol.Dispose();
        //            }
        //        }
        //    }
        //}
        //鼠标移动窗体
        //protected override void WndProc(ref Message Msg)
        //{
        //    //禁止双击最大化
        //    if (Msg.Msg == 0x0112 && Msg.WParam.ToInt32() == 61490) return;

        //    if (Msg.Msg == WM_NCHITTEST)
        //    {
        //        //允许拖动窗体移动
        //        Msg.Result = new IntPtr(HT_CAPTION);
        //        return;
        //    }

        //    base.WndProc(ref Msg);
        //}

        //关闭窗体
        private void labelclose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否退出系统", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    if (Global.CloseCOM == "RS232DY3000")
                    {
                        dyserial.closecom();
                    }
                    else if (Global.CloseCOM == "RS232LZ4000")
                    {
                        AllLZ4000.closecom();
                    }
                    else if (Global.CloseCOM == "RS232LNLZ4000")
                    {
                        ln4000.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY5000")
                    {
                        dy5000serial.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY6200")//DY6200协议
                    {
                        dy6200.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY6400")//DY6400协议
                    {
                        dy6400.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DYRS2")//DYRS2协议
                    {
                        dyrs2.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY8120")//DY8120协议
                    {
                        dy8120.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY8200")//DY8200协议
                    {
                        dy8200.closecom();
                    }
                    else if (Global.CloseCOM == "RS232TY16")
                    {
                        ty16.closecom();
                    }
                    else if (Global.CloseCOM == "RS232TL310")
                    {
                        tl310.closecom();
                    }
                    else if (Global.CloseCOM == "RS232DY7200")
                    {
                        dy7200.closecom();
                       
                    }
                    else if (Global.CloseCOM == "RS232DY5800")
                    {
                        dy5800.closecom();

                    }
                    else if (Global.CloseCOM == "RS232DY2620")
                    {
                        dy2620.closecom();
                    }

                    dy.savediary(DateTime.Now.ToString(), "系统退出", "成功");
                    this.Close();
                    Application.Exit();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "系统退出" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "系统退出");
            }
        }

        //最小化窗体
        private void labelmin_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //鼠标进入关闭
        private void labelclose_MouseEnter(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.Red;
        }
        //鼠标进入最小化
        private void labelmin_MouseEnter(object sender, EventArgs e)
        {
            labelmin.ForeColor = Color.Red;
        }
        //鼠标离开最小化
        private void labelmin_MouseLeave(object sender, EventArgs e)
        {
            labelmin.ForeColor = Color.White;
        }
        //鼠标离开关闭
        private void labelclose_MouseLeave(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.White;
        }       
        //商城
        private void pnlShop_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = true;
                picBoxTask.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Shop = true;
                Global.Task = false;
                ucShopmall shop = new ucShopmall();
                panelProject.Visible = false;
                Mainpanel.Visible = true;
                Mainpanel.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    shop.Width = 1345;
                    //Mainpanel.Width = 1345;
                }
                Mainpanel.Controls.Add(shop);
                Global.currentform = "商城";
                //dy.savediary(DateTime.Now.ToString(), "进入商城菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入商城菜单" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"进入商城菜单");
            }
        }
        private void pnlShop_MouseEnter(object sender, EventArgs e)
        {
            pnlShop.Visible = true;
            picboxShop.Visible = true;
        }
        private void pnlShop_MouseLeave(object sender, EventArgs e)
        {
            if (Global.Shop == true)
            {
                pnlShop.Visible = true;
                picboxShop.Visible = true;
            }
            else 
            {
                pnlShop.Visible = true;
                picboxShop.Visible = false;
            }
        }
        private void labelBig_MouseEnter(object sender, EventArgs e)
        {
            labelBig.ForeColor = Color.Red;
        }
        private void labelBig_MouseLeave(object sender, EventArgs e)
        {
            labelBig.ForeColor = Color.White ;
        }
        //窗体最大化还原
        private void labelBig_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.maxwindow == true)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.StartPosition = FormStartPosition.CenterScreen;
                    //labelclose.Location = new Point(1000, 7);
                    //labelBig.Location = new Point(975, 9);
                    //labelmin.Location = new Point(946, 7);
                    //PnlUser.Location = new Point(850,55);
                    //PnlTime.Location = new Point(850, 80);
                    Global.maxwindow = false;
                    //if (Global.currentform == "商城")
                    //{
                    //    ucShopmall shop = new ucShopmall();
                    //    //shop.labelback.Location = new Point(880,2);
                    //    //shop.labelclose.Location = new Point(960,12);
                    //    Mainpanel.Width = 1010;
                    //    shop.Width = 1010;
                    //    Mainpanel.Controls.Clear();
                    //    Mainpanel.Controls.Add(shop);
                    //}
                    //if (Global.currentform == "仪器管理")
                    //{
                    //    UCEM.Width = 800;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(UCEM);
                    //}
                    //if (Global.currentform == "数据查询")
                    //{
                    //    UCSD.Width = 800;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(UCSD);
                    //}
                    //if (Global.currentform == "帮助")
                    //{
                    //    ucHelp uchp = new ucHelp();
                    //    uchp.Width = 800;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(uchp);
                    //}
                    //if (Global.currentform == "操作日记")
                    //{
                    //    ucDiary dy = new ucDiary();
                    //    dy.Width = 800;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(dy);
                    //}
                    //if (Global.currentform == "服务器配置")
                    //{
                    //    ucServer sv = new ucServer();
                    //    sv.Width = 800;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(sv);
                    //}
                    //if (Global.currentform == "统计分析")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    ustatis.Width = 800;
                    //    PnlContent.Controls.Add(ustatis);
                    //}
                    //if (Global.currentform == "用户管理")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    us.Width = 800;
                    //    PnlContent.Controls.Add(us);
                    //}
                    //if (Global.currentform == "样品信息")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    sam.Width = 800;
                    //    PnlContent.Controls.Add(sam);
                    //}
                    //if (Global.currentform == "单位信息")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    un.Width = 800;
                    //    PnlContent.Controls.Add(un);
                    //}
                    //if (Global.currentform == "LZ-4000农药残留快速测试仪")
                    //{
                    //    AllLZ4000.Width = 800;
                    //    PnlContent.Controls.Add(AllLZ4000);
                    //}
                    //if (Global.currentform == "主页")
                    //{
                    //    MainPage mpp = new MainPage();
                    //    mpp.Width = iActulaWidth - 350;
                    //    panelProject.Visible = false;

                    //    Mainpanel.Visible = true;
                    //    Mainpanel.Controls.Clear();
                    //    Mainpanel.Controls.Add(mpp);
                    //}
                }
                else
                {
                    //this.WindowState = FormWindowState.Maximized;
                    //this.Width = 1366;
                    //this.Height = 728;
                    this.WindowState = FormWindowState.Maximized;
                    //labelclose.Location = new Point(1340, 5);
                    //labelBig.Location = new Point(1315, 7);
                    //labelmin.Location = new Point(1286, 5);
                    //PnlUser.Location = new Point(1180, 55);
                    //PnlTime.Location = new Point(1180, 80);
                    Global.maxwindow = true;
                    //if (Global.currentform == "仪器管理")
                    //{
                    //    UCEM.Width = 1110;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(UCEM);
                    //}
                    //if (Global.currentform == "数据查询")
                    //{
                    //    UCSD.Width = 1130;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(UCSD);
                    //}
                    //if (Global.currentform == "商城")
                    //{
                    //    ucShopmall shop = new ucShopmall();
                    //    shop.Width = 1345;
                    //    Mainpanel.Width = 1345;
                    //    Mainpanel.Controls.Clear();
                    //    Mainpanel.Controls.Add(shop);
                    //}
                    //if (Global.currentform == "帮助")
                    //{
                    //    ucHelp uchp = new ucHelp();
                    //    uchp.Width = 1144;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(uchp);
                    //}
                    //if (Global.currentform == "服务器配置")
                    //{
                    //    ucServer sv = new ucServer();
                    //    PnlContent.Controls.Clear();
                    //    sv.Width = 1150;
                    //    PnlContent.Controls.Add(sv);
                    //}
                    //if (Global.currentform == "操作日记")
                    //{
                    //    ucDiary dy = new ucDiary();
                    //    dy.Width = 1150;
                    //    PnlContent.Controls.Clear();
                    //    PnlContent.Controls.Add(dy);
                    //}
                    //if (Global.currentform == "统计分析")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    ustatis.Width = 1150;
                    //    PnlContent.Controls.Add(ustatis);
                    //}
                    //if (Global.currentform == "用户管理")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    us.Width = 1150;
                    //    PnlContent.Controls.Add(us);
                    //}
                    //if (Global.currentform == "样品信息")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    sam.Width = 1150;
                    //    PnlContent.Controls.Add(sam);
                    //}
                    //if (Global.currentform == "单位信息")
                    //{
                    //    PnlContent.Controls.Clear();
                    //    un.Width = 1150;
                    //    PnlContent.Controls.Add(un);
                    //}
                    //if (Global.currentform == "主页")
                    //{
                    //    Mainpanel.Visible = true;
                    //    mp.Width = iActulaWidth - 20; ;
                    //    //Mainpanel.Width = 1400;
                    //    Mainpanel.Controls.Clear();
                    //    Mainpanel.Controls.Add(mp);
                    //}
                    //if (Global.currentform == "LZ-4000农药残留快速测试仪")
                    //{
                    //    AllLZ4000.Width = 1400;
                    //    PnlContent.Controls.Add(AllLZ4000);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "窗体最大还原");
            }
        }
        #region 鼠标拖动改变窗体大小
        /// <summary>
        /// 定义用于设置的常量值
        /// </summary>
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        /// <summary>
        /// 重写系统WndProc函数
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息 
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero; //默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
        /// <summary>
        /// 任务管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelTask_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Mainpanel.Visible = false;
                panelProject.Visible = true;
                panelProject.Controls.Clear();
                PnlContent.Controls.Clear();
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Shop = false;
                Global.Task = true;

                for (int i = 0; i < Global.ReceiveTask.GetLength(0); i++)
                {
                    lbTask  = new Label();
                    lbTask.Size = new Size(220, 60);
                    lbTask.Location = new Point(0, i * 60);
                    lbTask.Font = new Font("楷体_GB2312", 14);
                    lbTask.Name = Global.ReceiveTask[i, 0];
                    lbTask.Text = Global.ReceiveTask[i, 1];
                    lbTask.BackColor = Color.Transparent;
                    //lbTask.AutoSize = false;
                    if (i == 0)
                    {
                        lbTask.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                        selectfuntion = Global.ReceiveTask[i, 0];
                    }
                    lbTask.TextAlign = ContentAlignment.MiddleCenter;
                    lbTask.MouseClick += lbTask_MouseClick;
                    lbTask.MouseEnter += lbTask_MouseEnter;
                    lbTask.MouseLeave += lbTask_MouseLeave;

                    panelProject.Controls.Add(lbTask);
                }

                ucTaskSample task = new ucTaskSample();
                PnlContent.Controls.Add(task);
                Global.currentform = "任务管理";
                //dy.savediary(DateTime.Now.ToString(), "进入商城菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入任务管理菜单" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入任务管理菜单");
            }
        }

        private  void lbTask_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Image = null;
            if (selectfuntion != "")
            {
                //查找单击的label添加背景
                (panelProject.Controls.Find(selectfuntion, false)[0] as Label).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }       
        }

        private  void lbTask_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(((Label)sender), ((Label)sender).Text);
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");  
        }
        /// <summary>
        /// 任务单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void lbTask_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < Global.ReceiveTask.GetLength(0); i++)
                {
                    (panelProject.Controls.Find(Global.ReceiveTask[i, 0], false)[0] as Label).Image = null; //查找所有的label清空背景                            
                }
                ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                selectfuntion = ((Label)sender).Name;
                PnlContent.Controls.Clear();
                if (selectfuntion == "SampleTask")
                {
                    ucTaskSample task = new ucTaskSample();
                    PnlContent.Controls.Add(task);
                }
                else if (selectfuntion == "DetectTask")
                {
                    ucDetectTask task = new ucDetectTask();
                    PnlContent.Controls.Add(task);
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "任务管理事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "任务管理单击事件");
            }
        }

        private void panelTask_MouseEnter(object sender, EventArgs e)
        {
            panelTask.Visible = true;
            picBoxTask.Visible = true;
        }

        private void panelTask_MouseLeave(object sender, EventArgs e)
        {
            if (Global.Task == true)
            {
                panelTask.Visible = true;
                picBoxTask.Visible = true;
            }
            else
            {
                panelTask.Visible = true;
                picBoxTask.Visible = false;
            }
        }

        //在程序中用一个计时器，每隔几秒钟调用一次该函数，打开任务管理器，你会有惊奇的发现

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
    }
}
