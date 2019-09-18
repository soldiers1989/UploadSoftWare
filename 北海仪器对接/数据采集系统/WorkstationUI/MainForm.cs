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

namespace WorkstationUI
{
    public partial class MainForm : Form
    {
        #region 全局变量定义
        private clsdiary dy = new clsdiary();
        private System.Windows.Forms.Timer D_timer = null;
        private bool labellz200 = false;
        private bool labelprint = false;
        private bool stati = false;
        private bool backup = false;
        private bool DataReturn = false;
        private Label lbfindptint = new Label();//查询打印
        private Label labStatistical = new Label();//统计分析
        private Label labDataManage = new Label();//数据管理
        private Label labDataReturn = new Label();//数据恢复      
        private Label labelUnit = new Label();//单位信息
        private Label labelSample = new Label();//样品信息
        private Label lab_Set = new Label();//系统设计
        private Label lz2000Label = new Label();
        private Label labelDY100 = new Label();
        private bool labSetbool = false;
        private bool user = false;
        private bool help = false;
        private bool about = false;
        private bool server = false;
        private bool dairy=false ;
        private bool sysupdate = false;
        private bool logcancel = false;
        private bool iunit = false;
        private bool isample = false;        
        private clsSetSqlData SQLstr = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private  int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;//窗体的宽度
        private ucDY5000 dy5000serial = new ucDY5000();
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
        private ucSearchData UCSD = new ucSearchData();
        private ucAddSample sam = new ucAddSample();
        private ucAddUnit un = new ucAddUnit();
        private ucStatiscal ustatis = new ucStatiscal();
        private ucBaseData bd = new ucBaseData();
        private ucSystemset ucsys = new ucSystemset();
        private ucEquipmenManage UCEM = new ucEquipmenManage();
        private MainPage mp = new MainPage();
        private string[] sysSetname = { "仪器管理", "网络设置", "操作日记", "用户管理", "系统帮助", "关于系统", "系统升级", "注销用户" };
        private DataTable dt = null;
        private Label lbFunction;
        private string selectfuntion = string.Empty;
        private ucLZ2000 lz2000 = new ucLZ2000();
        private clsUpdateMessage cum = new clsUpdateMessage();
        private string err = string.Empty;
        private Label labSysSet = new Label();
        private Label labelUserManage = new Label();
        private Label labelHelp = new Label();
        private Label labeAbout = new Label();
        private Label labelServer = new Label();
        private Label labelDairy = new Label();
        private Label labelSysUpdate = new Label();
        private Label labellogoff = new Label();//注销用户
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
        public void timerRead(object sender, EventArgs e)
        {
            labelTime.Text =System.DateTime.Now.ToString();         
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
                Global.BCheckUnitName = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位
                Global.maxwindow = true;
                panelProject.Visible = false;//显示项目的panel
                //LeftUserControl1 user = new LeftUserControl1();
                ucSecondTitle user = new ucSecondTitle();
                PnLeft.Controls.Add(user);
                PnLeft.Visible = false;
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
                D_timer = new System.Windows.Forms.Timer(); ;
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
                //new各功能下label功能单击事件------------------------------------------------------------------------           
                //统计分析
                labStatistical.MouseClick += new System.Windows.Forms.MouseEventHandler(labStatistical_Click);
                labStatistical.MouseEnter += new System.EventHandler(labStatistical_Enter);
                labStatistical.MouseLeave += new System.EventHandler(labStatistical_Leave);
                //查询打印
                lbfindptint.MouseClick += new System.Windows.Forms.MouseEventHandler(lbfindptint_Click);
                lbfindptint.MouseEnter += new System.EventHandler(lbfindptint_Enter);
                lbfindptint.MouseLeave += new System.EventHandler(lbfindptint_Leave);
                //数据备份
                labDataManage.MouseClick += new System.Windows.Forms.MouseEventHandler(labDataManage_Click);
                labDataManage.MouseEnter += new System.EventHandler(labDataManage_Enter);
                labDataManage.MouseLeave += new System.EventHandler(labDataManage_Leave);
                //数据恢复
                labDataReturn.MouseClick += new System.Windows.Forms.MouseEventHandler(labDataReturn_Click);
                labDataReturn.MouseEnter += new System.EventHandler(labDataReturn_Enter);
                labDataReturn.MouseLeave += new System.EventHandler(labDataReturn_Leave);
                //数据采集
                lz2000Label.Click += new System.EventHandler(LZ200Label_Click);
                lz2000Label.MouseEnter += new System.EventHandler(LZ200Label_Enter);
                lz2000Label.MouseLeave += new System.EventHandler(LZ200Label_Leave);
                //仪器管理
                labSysSet.MouseClick += new System.Windows.Forms.MouseEventHandler(labSysSet_click);
                labSysSet.MouseEnter += new System.EventHandler(labSysSet_Enter);
                labSysSet.MouseLeave += new System.EventHandler(labSysSet_Leave);
                //服务器配置
                labelServer.MouseClick += new System.Windows.Forms.MouseEventHandler(labelServer_click);
                labelServer.MouseEnter += new System.EventHandler(labelServer_Enter);
                labelServer.MouseLeave += new System.EventHandler(labelServer_Leave);
                //操作日记
                labelDairy.MouseClick += new System.Windows.Forms.MouseEventHandler(labelDairy_click);
                labelDairy.MouseEnter += new System.EventHandler(labelDairy_Enter);
                labelDairy.MouseLeave += new System.EventHandler(labelDairy_Leave);
                //用户管理
                labelUserManage.MouseClick += new System.Windows.Forms.MouseEventHandler(labelUserManage_click);
                labelUserManage.MouseEnter += new System.EventHandler(labelUserManage_Enter);
                labelUserManage.MouseLeave += new System.EventHandler(labelUserManage_Leave);
                //帮助
                labelHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(labelHelp_click);
                labelHelp.MouseEnter += new System.EventHandler(labelHelp_Enter);
                labelHelp.MouseLeave += new System.EventHandler(labelHelp_Leave);
                //关于
                labeAbout.MouseClick += new System.Windows.Forms.MouseEventHandler(labeAbout_click);
                labeAbout.MouseEnter += new System.EventHandler(labeAbout_Enter);
                labeAbout.MouseLeave += new System.EventHandler(labeAbout_Leave);
                //系统更新
                labelSysUpdate.MouseClick += new System.Windows.Forms.MouseEventHandler(labelSysUpdate_click);
                labelSysUpdate.MouseEnter += new System.EventHandler(labelSysUpdate_Enter);
                labelSysUpdate.MouseLeave += new System.EventHandler(labelSysUpdate_Leave);
                //样品信息
                labelSample.MouseClick += labelSample_MouseClick;
                labelSample.MouseEnter += labelSample_MouseEnter;
                labelSample.MouseLeave += labelSample_MouseLeave;
                //检测单位与被检单位信息
                labelUnit.MouseClick += labelUnit_MouseClick;
                labelUnit.MouseEnter += labelUnit_MouseEnter;
                labelUnit.MouseLeave += labelUnit_MouseLeave;
                //注销用户
                labellogoff.MouseClick += labellogoff_MouseClick;
                labellogoff.MouseEnter += labellogoff_MouseEnter;
                labellogoff.MouseLeave += labellogoff_MouseLeave;
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

        //LZ2000按钮单击事件
        private void LZ200Label_Click(object Sender, EventArgs e)
        {         
           
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
                PnLeft.Visible = true;
                panelProject.Visible = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picboxShop.Visible = false;
                Global.SysSet = false;
                //Global.SearchData = false;
                Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;
                //pictureBox1.Visible = true;           
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
                else if (Protocol == "RS232TY16")
                {
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(ty16);
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
                    else if (mprotocol == "RS232TY16")
                    {
                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(ty16);
                        //发送消息改label名称
                        cum.SendOutMessage("RS232TY16", Global.ChkManchine);
                    }
                }
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "仪器选择事件：" + ex.Message, "错误");
                MessageBox.Show(ex.Message ,"选择仪器单击事件");
            }
        }      
        //LZ2000鼠标进入事件
        private void LZ200Label_Enter(object Sender, EventArgs e)
        {
            lz2000Label.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        //LZ2000鼠标离开事件
        private void LZ200Label_Leave(object Sender, EventArgs e)
        {
            if (labellz200 == true)
            {
                lz2000Label.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                lz2000Label.Image = null;
            }
        }      
        //鼠标进入查询打印
        private void lbfindptint_Enter(object sender, EventArgs e)
        {
            lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        //鼠标移出查询打印
        private void lbfindptint_Leave(object sender, EventArgs e)
        {
            if (labelprint == true)
            {
                lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                lbfindptint.Image = null;
            }
        }
       
        //private bool opendia=false ;
        /// <summary>
        /// 数据中心单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void skinPanel4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                PnLeft.Visible = true;
                Global.Dairypb = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = true;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Shop = false;
                //Global.Dairypb = false;
                panelProject.Visible = true;
                Mainpanel.Visible = false;
                ucSearchData UCSD = new ucSearchData();
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    UCSD.Width = 1130;
                }
                PnlContent.Controls.Add(UCSD);
                Global.currentform = "数据查询";
                panelProject.Controls.Clear();
                UCSD.searchdatabase();
                //数据查询
                lbfindptint.Size = new Size(220, 60);
                lbfindptint.Location = new Point(0, 0);
                lbfindptint.Font = new Font("楷体_GB2312", 14);
                lbfindptint.Text = "数据查询";
                lbfindptint.FlatStyle = FlatStyle.Flat;
                lbfindptint.BackColor = Color.Transparent;
                lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                lbfindptint.TextAlign = ContentAlignment.MiddleCenter;           
                panelProject.Controls.Add(lbfindptint);
                labelprint = true;
                //统计分析
                labStatistical.Size = new Size(220, 60);
                labStatistical.Location = new Point(0, 60);
                labStatistical.Font = new Font("楷体_GB2312", 14);
                labStatistical.Text = "统计分析";
                labStatistical.FlatStyle = FlatStyle.Flat;
                labStatistical.BackColor = Color.Transparent;
                labStatistical.Image = null;
                labStatistical.TextAlign = ContentAlignment.MiddleCenter;                         
                panelProject.Controls.Add(labStatistical);
                stati = false;
                //单位信息
                labelUnit.Size = new Size(220, 60);
                labelUnit.Location = new Point(0, 120);
                labelUnit.Font = new Font("楷体_GB2312", 14);
                labelUnit.Text = "检测任务";//单位/企业
                labelUnit.FlatStyle = FlatStyle.Flat;
                labelUnit.BackColor = Color.Transparent;
                labelUnit.Image = null;
                labelUnit.TextAlign = ContentAlignment.MiddleCenter;         
                panelProject.Controls.Add(labelUnit);
                iunit = false;           
                //样品信息
                labelSample.Size = new Size(220, 60);
                labelSample.Location = new Point(0,180);
                labelSample.Font = new Font("楷体_GB2312", 14);
                labelSample.Text = "样品信息";
                labelSample.FlatStyle = FlatStyle.Flat;
                labelSample.BackColor = Color.Transparent;
                labelSample.Image = null;
                labelSample.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labelSample);
                isample = false;
                //本地数据库备份
                labDataManage.Size = new Size(220, 60);
                labDataManage.Location = new Point(0, 240);
                labDataManage.Font = new Font("楷体_GB2312", 14);
                labDataManage.Text = "数据备份";
                labDataManage.FlatStyle = FlatStyle.Flat;
                labDataManage.BackColor = Color.Transparent;
                labDataManage.Image = null;
                labDataManage.TextAlign = ContentAlignment.MiddleCenter;           
            
                panelProject.Controls.Add(labDataManage);
                backup = false;
                //数据恢复
                labDataReturn.Size = new Size(220, 60);
                labDataReturn.Location = new Point(0,300);
                labDataReturn.Font = new Font("楷体_GB2312", 14);
                labDataReturn.Text = "数据恢复";
                labDataReturn.FlatStyle = FlatStyle.Flat;
                labDataReturn.BackColor = Color.Transparent;
                labDataReturn.Image = null;
                labDataReturn.TextAlign = ContentAlignment.MiddleCenter;
            
                panelProject.Controls.Add(labDataReturn);
                DataReturn = false;
                dy.savediary(DateTime.Now.ToString(), "进入数据中心菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入数据中心菜单", "错误");
                MessageBox.Show(ex.Message, "数据中心单击事件");
            }
        }

        private  void labelSample_MouseEnter(object sender, EventArgs e)
        {
            labelSample.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }

        private  void labelSample_MouseLeave(object sender, EventArgs e)
        {
            if (isample == true)
            {
                labelSample.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labelSample.Image = null;
            }
        }

        private  void labelUnit_MouseLeave(object sender, EventArgs e)
        {
            if (iunit == true)
            {
                labelUnit.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labelUnit.Image = null;
            }
        }
        private  void labelUnit_MouseEnter(object sender, EventArgs e)
        {
            labelUnit.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        /// <summary>
        /// 样品信息单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void labelSample_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                isample = true;
                labelSample.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                iunit = false;
                labelprint = false;
                stati = false;
                backup = false;
                DataReturn = false;
                lbfindptint.Image = null;
                labStatistical.Image = null;
                labelUnit.Image = null;
                labDataManage.Image = null;
                labDataReturn.Image = null;
                if (Global.maxwindow == true)
                {
                    sam.Width = 1150;
                }

                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(sam);
                Global.currentform = "样品信息";
                //dy.savediary(DateTime.Now.ToString(), "进入样品信息", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入样品信息错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入样品信息");
            }
        }
        /// <summary>
        /// 检测单位与被检单位信息单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelUnit_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                iunit = true;
                labelUnit.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                isample = false;
                labelprint = false;
                stati = false;
                backup = false;
                DataReturn = false;
                lbfindptint.Image = null;
                labStatistical.Image = null;
                labelSample.Image = null;
                labDataManage.Image = null;
                labDataReturn.Image = null;
                if (Global.maxwindow == true)
                {
                    un.Width = 1150;
                }

                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(un);
                Global.currentform = "单位信息";
                //dy.savediary(DateTime.Now.ToString(), "进入机构信息", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入机构信息" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入机构信息");
            }
        }
      
        private void labDataReturn_Leave(object Sender, EventArgs e)
        {
            if (DataReturn == true)
            {
                labDataReturn.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labDataReturn.Image = null;
            }
        }
        private void labDataReturn_Enter(object Sender, EventArgs e)
        {
            labDataReturn.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        /// <summary>
        /// 数据恢复
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labDataReturn_Click(object Sender, MouseEventArgs e)
        {
            try
            {
                DataReturn = true;
                //dataup = false;
                backup = false;
                stati = false;
                iunit = false;
                isample = false;
                labelprint = false;
                //basedata = false;
                lbfindptint.Image = null;
                labStatistical.Image = null;
                labDataManage.Image = null;
                labelSample.Image = null;
                labelUnit.Image = null;
                labDataReturn.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
                    try
                    {
                        File.Copy(openFileDialog1.FileName, path + "Data\\Local.mdb", true);
                    }
                    catch
                    {
                        MessageBox.Show("数据库恢复失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MessageBox.Show("数据库恢复成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dy.savediary(DateTime.Now.ToString(), "进入数据恢复", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入数据恢复错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入数据恢复");
            }
        }     
        //private  ucDataUpload DU = new ucDataUpload();     
        //private  ucDataBackup DB = new ucDataBackup();
        /// <summary>
        /// 数据备份单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labDataManage_Click(object Sender, MouseEventArgs e)
        {
            try
            {
                backup = true;
                //dataup = false;
                stati = false;
                labelprint = false;
                DataReturn = false;
                //basedata = false;
                isample = false;
                iunit = false;
                lbfindptint.Image = null;
                labStatistical.Image = null;
                labDataReturn.Image = null;
                labelUnit.Image = null;
                labelSample.Image = null;
                labDataManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");

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
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入数据备份失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入数据备份");
            }
        }
        private void labDataManage_Enter(object Sender, EventArgs e)
        {
            labDataManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        private void labDataManage_Leave(object Sender, EventArgs e)
        {
            if (backup== true)
            {
                labDataManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labDataManage.Image = null;
            }   
        }
        /// <summary>
        /// 统计分析单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labStatistical_Click(object Sender, MouseEventArgs e)
        {
            try
            {
                Global.currentform = "统计分析";
                stati = true;
                labelprint = false;
                backup = false;
                //dataup = false;
                DataReturn = false;
                //basedata = false;
                isample = false;
                iunit = false;
                lbfindptint.Image = null;
                labDataManage.Image = null;
                labDataReturn.Image = null;   
                labelSample.Image = null;
                labelUnit.Image = null;

                labStatistical.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                ucStatiscal ustatis = new ucStatiscal();
                if (Global.maxwindow == true)
                {
                    ustatis.Width = 1140;
                }
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(ustatis);
                //dy.savediary(DateTime.Now.ToString(), "进入统计分析", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入统计分析失败"+ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入统计分析");
            }
        }
        //鼠标进入统计分析
        private void labStatistical_Enter(object Sender, EventArgs e)
        {
            labStatistical.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        //鼠标离开统计分析
        private void labStatistical_Leave(object Sender, EventArgs e)
        {
            if (stati == true)
            {
                labStatistical.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labStatistical.Image = null;
            }               
        }        
        //数据查询单击事件
        private void lbfindptint_Click(object Sender, MouseEventArgs e)
        {
            try
            {
                Global.currentform = "数据查询";
                PnLeft.Visible = true;
                labelprint = true;
                stati = false;
                backup = false;
                //dataup = false;
                DataReturn = false;
                //basedata = false;
                isample = false;
                iunit = false;
                labelSample.Image = null;
                labelUnit.Image = null;
                labStatistical.Image = null;
                labDataManage.Image = null;
                labDataReturn.Image = null;
                lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    UCSD.Width = 1135;
                }
                PnlContent.Controls.Add(UCSD);
                UCSD.searchdatabase();
                //dy.savediary(DateTime.Now.ToString(), "进入数据查询", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入数据查询失败：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入数据查询");
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
                PnLeft.Visible = false;
                Global.MainPage = true;
                picBoxSysSet.Visible = false;
                picBoxData.Visible = false;
                picboxMain.Visible = true;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;
                Global.SysSet = false;
                Global.SearchData = false;
                //Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;
                panelProject.Visible = false;
                Mainpanel.Visible = true;
                //Test.uctest Ts = new Test.uctest();
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
                PnLeft.Visible = true;
                Global.SysSet = true;
                picBoxSysSet.Visible = true;
                picBoxData.Visible = false;
                picboxMain.Visible = false;
                picBoxCollect.Visible = false;
                picboxShop.Visible = false;

                Global.SearchData = false;
                Global.MainPage = false;
                Global.Dairypb = false;
                Global.Shop = false;

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
                labSysSet.Size = new Size(220, 60);
                labSysSet.Location = new Point(0, 0);
                labSysSet.Font = new Font("楷体_GB2312", 14);
                labSysSet.Text = "仪器管理";
                labSysSet.FlatStyle = FlatStyle.Flat;
                labSysSet.BackColor = Color.Transparent;
                labSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labSysSet.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Clear();
                panelProject.Controls.Add(labSysSet);
                labSetbool = true;//恢复鼠标进入Label初始化值

                //ucServer usv = new ucServer();
                //PnlContent.Controls.Add(usv);
                labelServer.Size = new Size(220, 60);
                labelServer.Location = new Point(0, 60);
                labelServer.Font = new Font("楷体_GB2312", 14);
                labelServer.Text = "网络设置";
                labelServer.FlatStyle = FlatStyle.Flat;
                labelServer.BackColor = Color.Transparent;
                panelProject.Controls.Add(labelServer);
                labelServer.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelServer.TextAlign = ContentAlignment.MiddleCenter;

                //panelProject.Controls.Clear();
                panelProject.Controls.Add(labelServer);
                server = false;//恢复鼠标进入Label初始化值

                labelDairy.Size = new Size(220, 60);
                labelDairy.Location = new Point(0, 120);
                labelDairy.Font = new Font("楷体_GB2312", 14);
                labelDairy.Text = "操作日记";
                labelDairy.FlatStyle = FlatStyle.Flat;
                labelDairy.BackColor = Color.Transparent;
                labelDairy.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelDairy.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labelDairy);
                dairy = false;//恢复鼠标进入Label初始化值

                labelUserManage.Size = new Size(220, 60);
                labelUserManage.Location = new Point(0, 180);
                labelUserManage.Font = new Font("楷体_GB2312", 14);
                labelUserManage.Text = "用户管理";
                labelUserManage.FlatStyle = FlatStyle.Flat;
                labelUserManage.BackColor = Color.Transparent;
                labelUserManage.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelUserManage.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labelUserManage);
                user = false;//恢复鼠标进入Label初始化值

                labelHelp.Size = new Size(220, 60);
                labelHelp.Location = new Point(0, 240);
                labelHelp.Font = new Font("楷体_GB2312", 14);
                labelHelp.Text = "系统帮助";
                labelHelp.FlatStyle = FlatStyle.Flat;
                labelHelp.BackColor = Color.Transparent;
                labelHelp.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelHelp.TextAlign = ContentAlignment.MiddleCenter;
                
                panelProject.Controls.Add(labelHelp);
                help = false;//恢复鼠标进入Label初始化值
                //关于
                labeAbout.Size = new Size(220, 60);
                labeAbout.Location = new Point(0, 300);
                labeAbout.Font = new Font("楷体_GB2312", 14);
                labeAbout.Text = "关于系统";
                labeAbout.FlatStyle = FlatStyle.Flat;
                labeAbout.BackColor = Color.Transparent;
                labeAbout.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labeAbout.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labeAbout);
                about = false;//恢复鼠标进入Label初始化值
                //系统更新
                labelSysUpdate.Size = new Size(220, 60);
                labelSysUpdate.Location = new Point(0, 360);
                labelSysUpdate.Font = new Font("楷体_GB2312", 14);
                labelSysUpdate.Text = "系统升级";
                labelSysUpdate.FlatStyle = FlatStyle.Flat;
                labelSysUpdate.BackColor = Color.Transparent;
                labelSysUpdate.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelSysUpdate.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labelSysUpdate);
                sysupdate = false;//恢复鼠标进入Label初始化值
                //注销用户
                labellogoff.Size = new Size(220, 60);
                labellogoff.Location = new Point(0, 420);
                labellogoff.Font = new Font("楷体_GB2312", 14);
                labellogoff.Text = "注销用户";
                labellogoff.FlatStyle = FlatStyle.Flat;
                labellogoff.BackColor = Color.Transparent;
                labellogoff.Image = null;//Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labellogoff.TextAlign = ContentAlignment.MiddleCenter;
                panelProject.Controls.Add(labellogoff);
                logcancel = false;

                dy.savediary(DateTime.Now.ToString(), "进入系统设计菜单", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入系统设计菜单错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入系统设计菜单");
            }
        }


        private void labellogoff_MouseLeave(object sender, EventArgs e)
        {
            if (logcancel == true)
            {
                labellogoff.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labellogoff.Image = null;
            }
        }

        private void labellogoff_MouseEnter(object sender, EventArgs e)
        {
            labellogoff.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }

        private void labellogoff_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                dairy = false;
                about = false;
                help = false;
                user = false;
                labSetbool = false;
                server = false;
                sysupdate = false;
                logcancel = true;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelHelp.Image = null;
                labeAbout.Image = null;
                labelServer.Image = null;
                labelDairy.Image = null;
                labellogoff.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelSysUpdate.Image = null;
                DialogResult dr = MessageBox.Show("是否退出当前用户", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.No)
                {
                    return;
                }
                //切换账户
                Application.ExitThread();
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "切换用户错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message,"切换用户");
            }
        }
       
        private void labelSysUpdate_Leave(object Sender, EventArgs e)
        {
            if (sysupdate == true)
            {
                labelSysUpdate.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else 
            {
                labelSysUpdate.Image = null;
            }
 
        }
        private void labelSysUpdate_Enter(object Sender, EventArgs e)
        {
            labelSysUpdate.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        /// <summary>
        /// 系统升级
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelSysUpdate_click(object Sender, MouseEventArgs e)
        {
            try
            {
                frmSysUpdate UpD = new frmSysUpdate();
                sysupdate = true;
                dairy = false;
                about = false;
                help = false;
                user = false;
                labSetbool = false;
                server = false;
                logcancel = false;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelHelp.Image = null;
                labeAbout.Image = null;
                labelServer.Image = null;
                labelDairy.Image = null;
                labellogoff.Image = null;
                labelSysUpdate.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                UpD.Show();
                //dy.savediary(DateTime.Now.ToString(), "进入系统升级", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入系统升级错误："+ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入系统升级");
            }
        }
        private void labelDairy_Leave(object Sender, EventArgs e)
        {
            if (dairy == true)
            {
                labelDairy.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else 
            {
                labelDairy.Image = null;
            }
        }
        private void labelDairy_Enter(object Sender, EventArgs e)
        {
            labelDairy.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
 
        }
        /// <summary>
        /// 操作日记
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelDairy_click(object Sender, MouseEventArgs e)
        {
            try
            {
                ucDiary dy = new ucDiary();
                dairy = true;
                about = false;
                help = false;
                user = false;
                labSetbool = false;
                server = false;
                sysupdate = false;
                logcancel = false;
                labelSysUpdate.Image = null;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelHelp.Image = null;
                labeAbout.Image = null;
                labelServer.Image = null;
                labellogoff.Image = null;
                labelDairy.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    dy.Width = 1140;
                }
                PnlContent.Controls.Add(dy);
                Global.currentform = "操作日记";
                //clsdiary d = new clsdiary();
                //d.savediary(DateTime.Now.ToString(), "查看操作日记", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "查看操作日记错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "查看操作日记");
            }
        }
        private void labelServer_Leave(object Sender, EventArgs e)
        {
            if (server == true)
            {
                labelServer.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labelServer.Image = null;
            }
        }
        private void labelServer_Enter(object Sender, EventArgs e)
        {
            labelServer.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        //服务器配置单击事件
        private void labelServer_click(object Sender, EventArgs e)
        {
            try
            {
                Global.currentform = "服务器配置";
                ucServer sv = new ucServer();
                server = true;
                about = false;
                help = false;
                user = false;
                labSetbool = false;
                dairy = false;
                sysupdate = false;
                logcancel = false;
                labelSysUpdate.Image = null;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelHelp.Image = null;
                labeAbout.Image = null;
                labelDairy.Image = null;
                labellogoff.Image = null;
                labelServer.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                //PnlContent.Controls.Clear();
                //if (Global.maxwindow == true)
                //{
                //    sv.Width = 1150;
                //}
                //PnlContent.Controls.Add(sv);

                frmServer fs = new frmServer();
                fs.ShowDialog();
              
                //dy.savediary(DateTime.Now.ToString(), "进入服务器配置", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入服务器配置错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入服务器配置");
            }
        }
        private void labeAbout_Leave(object Sender, EventArgs e)
        {
            if (about == true)
            {
                labeAbout.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else 
            {
                labeAbout.Image = null;
            }
        }
        private void labeAbout_Enter(object Sender, EventArgs e)
        {
            labeAbout.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
 
        }       
        /// <summary>
        /// 关于的单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labeAbout_click(object Sender, MouseEventArgs e)
        {
            try
            {
                about = true;
                help = false;
                user = false;
                labSetbool = false;
                server = false;
                dairy = false;
                sysupdate = false;
                logcancel = false;
                labelSysUpdate.Image = null;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelHelp.Image = null;
                labelServer.Image = null;
                labelDairy.Image = null;
                labellogoff.Image = null;
                labeAbout.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                frmAbout fa = new frmAbout();
                fa.Show();
                //clsdiary d = new clsdiary();
                dy.savediary(DateTime.Now.ToString(), "进入关于", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入关于错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入关于");
            }
        }
        private void labelUserManage_Leave(object Sender, EventArgs e)
        {
            if (user == true)
            {
                labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labelUserManage.Image = null;
            }
        }
        private void labelUserManage_Enter(object Sender, EventArgs e)
        {
            labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }      
        private ucUser us = new ucUser();
        /// <summary>
        /// 帮助单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelHelp_click(object Sender, MouseEventArgs e)
        {
            try
            {
                ucHelp uchp = new ucHelp();

                help = true;
                user = false;
                labSetbool = false;
                about = false;
                server = false;
                dairy = false;
                sysupdate = false;
                logcancel = false;
                labelSysUpdate.Image = null;
                labeAbout.Image = null;
                labSysSet.Image = null;
                labelUserManage.Image = null;
                labelDairy.Image = null;
                labelServer.Image = null;
                labellogoff.Image = null;
                labelHelp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    uchp.Width = 1144;
                }
                PnlContent.Controls.Add(uchp);
                Global.currentform = "帮助";
                //dy.savediary(DateTime.Now.ToString(), "进入帮助", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入帮助错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "进入帮助");
            }
        }
        private void labelHelp_Enter(object Sender, EventArgs e)
        {
            labelHelp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
        }
        private void labelHelp_Leave(object Sender, EventArgs e)
        {
            if (help == true)
            {
                labelHelp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            {
                labelHelp.Image = null;
            }
        }
        /// <summary>
        /// 用户管理单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelUserManage_click(object Sender, MouseEventArgs e)
        {
            try
            {
                Global.currentform = "用户管理";
                user = true;
                about = false;
                labSetbool = false;
                server = false;
                dairy = false;
                help = false;
                sysupdate = false;
                logcancel = false;
                labelServer.Image = null;
                labelDairy.Image = null;
                labSysSet.Image = null;
                labelHelp.Image = null;
                labeAbout.Image = null;
                labelSysUpdate.Image = null;
                labellogoff.Image = null;
                if (Global.maxwindow == true)
                {
                    us.Width = 1150;
                }
                labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(us);
                //dy.savediary(DateTime.Now.ToString(), "用户管理", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "用户管理错误：" + ex.Message, "错误" );
                MessageBox.Show(ex.Message, "用户管理");
            }
        }
        /// <summary>
        /// 仪器管理单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labSysSet_click(object Sender, MouseEventArgs e)
        {
            try
            {
                Global.currentform = "仪器管理";
                labSetbool = true;
                user = false;
                about = false;
                server = false;
                dairy = false;
                sysupdate = false;
                logcancel = false;
                labelSysUpdate.Image = null;
                labelDairy.Image = null;
                labelServer.Image = null;
                labeAbout.Image = null;
                labelHelp.Image = null;
                labellogoff.Image = null;
                labSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                labelUserManage.Image = null;
                PnlContent.Controls.Clear();
                if (Global.maxwindow == true)
                {
                    UCEM.Width = 1130;
                }
                PnlContent.Controls.Add(UCEM);
                //clsdiary d = new clsdiary();
                //dy.savediary(DateTime.Now.ToString(), "进入仪器管理", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), "进入仪器管理错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message);
            }
        }
        //鼠标离开设计按钮
        private void labSysSet_Leave(object Sender, EventArgs e)
        {
            if (labSetbool == true)
            {
                labSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else
            { 
                labSysSet.Image =null ;
            }
        }
        //鼠标进入设计按钮
        private void labSysSet_Enter(object Sender, EventArgs e)
        {
            labSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\background.png");
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
                Global.SysSet = false;
                Global.SearchData = false;
                Global.MainPage = false;
                Global.Shop = true;

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
    }
}
