using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
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

namespace WorkstationUI
{
    public partial class MainForm : Form
    {   
        //public SerialPort port_com = new SerialPort();
        private clsdiary dy = new clsdiary();
        private System.Windows.Forms.Timer D_timer = null;
        private bool labellz200 = false;
        private bool labelprint = false;
        private bool stati = false;
        private bool backup = false;
        private bool dataup = false;
        private bool DataReturn = false;
        private Label lbfindptint = new Label();//查询打印
        private Label labStatistical = new Label();//统计分析
        private Label labDataManage = new Label();//数据管理
        private Label labDataReturn = new Label();//数据恢复
        //private Label labDataUp = new Label();//数据上传
        private Label labbasedata = new Label();//基础数据
        private Label labelUnit = new Label();//单位信息
        private Label labelSample = new Label();//样品信息
        private Label lab_Set = new Label();//系统设计
        private bool labSetbool = false;
        private bool user = false;
        private bool help = false;
        private bool about = false;
        private bool basedata=false ;
        private bool server = false;
        private bool dairy=false ;
        private bool sysupdate = false;
        private bool iunit = false;
        private bool isample = false;        
        private clsSetSqlData SQLstr = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private  int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;//窗体的宽度
        private ucDY5000 dy5000serial = new ucDY5000();
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
            //Basic.BasicNavigation newControl = Basic.BasicNavigation.Instance;
            //newControl.BasicNgtitle = "项目栏";         
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
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {     
           // this.WindowState = FormWindowState.Maximized;
            Global.maxwindow = true;
            panelProject.Visible = false;//显示项目的panel
            LeftUserControl1 user = new LeftUserControl1();
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
            //查询数据库获取串口设置信息
            string err = string.Empty;
            SQLstr.Delete("TempResult",out err);
            string strWhere = "SetCom ";

            D_timer = new System.Windows.Forms.Timer(); ;
            D_timer.Tick += new EventHandler(timerRead);
            D_timer.Enabled = true;

            DataTable dt = SQLstr.SearchData(strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<clsSetCom> serial = (List<clsSetCom>)clsStringUtil.DataTableToIList<clsSetCom>(dt, 1);
                WorkstationDAL.Model.clsShareOption.ComPort = "COM" + serial[0].ComPort;
                Global.SerialData = serial[0].ComDataBit;
                Global.SerialParity = serial[0].ComParity;
                Global.SerialStop = serial[0].ComStopBit;
                Global.SerialBaud = serial[0].ComBaud;
            }
            //WorkstationDAL.Model.clsShareOption.ComPort = Global.SerialCOM;
            labeluser.Text = "当前用户："+Global.userlog;
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
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //LZ2000按钮单击事件
        private void LZ200Label_Click(object Sender, EventArgs e)
        {         
            //labellz200 = true;
            //lz2000Label.Image  = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            //if (Global.TestInstrument[0,0] == "LZ-2000农药残留快检")
            //{
            //    Global.currentform = "LZ2000农残";
            //    ucLZ2000 LZ2000 = new ucLZ2000();
            //    PnlContent.Controls.Clear();
            //    PnlContent.Controls.Add(LZ2000);
            //    Global.ChkManchine = "LZ-2000农药残留快检仪";
            //}
            //else if (Global.TestInstrument[0,0] == "DY-1000农药残留速测仪")
            //{
            //    PnlContent.Controls.Clear();
            //    PnlContent.Controls.Add(DY1000);
            //    Global.ChkManchine = "DY-1000农药残留速测仪";
            //}
            //else if (Global.TestInstrument[0,0] == "LZ-4000农药残留快速测试仪")
            //{
            //    //ClearPanel();
            //    PnlContent.Controls.Clear();
            //    PnlContent.Controls.Add(AllLZ4000);
            //    Global.TestItem = "LZ-4000农药残留快速测试仪";
            //}
            //else if (Global.TestInstrument[0,0] == "TL-300快速测试仪")
            //{
            //    PnlContent.Controls.Clear();
            //    PnlContent.Controls.Add(tl300);
            //    Global.TestItem = "TL-300快速测试仪";

            //}
            //else if (Global.TestInstrument[0,0] == "DY-4300三聚氰胺快速分析仪")
            //{
            //    PnlContent.Controls.Clear();
            //    PnlContent.Controls.Add(dy4300);
            //    Global.TestItem = "DY-4300三聚氰胺快速分析仪";
            //}
            //dy.savediary(DateTime.Now.ToString(), "进入读取数据", "OK");
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
        #region 定义按钮
       
        private Label lz2000Label = new Label();
        private Label labelDY100 = new Label();
     
        #endregion
        private DataTable dt = null;
        private Label lbFunction;
        private string selectfuntion = string.Empty;
        private ucLZ2000 lz2000 = new ucLZ2000();
        private clsUpdateMessage cum = new clsUpdateMessage();
        ///
        ///数据采集单击事件
        ///
        private void skinPanel2_MouseClick_1(object sender, MouseEventArgs e)
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
           
            Mainpanel.Visible = false ;
            panelProject.Controls.Clear();
            Global.SearchData = true;
            PnlContent.Controls.Clear();
            string Protocol = string.Empty;//协议
            string name = string.Empty;
            //查询数据库获取检测仪器名称、串口号-----------------------------------------
            try
            {
                string err = string.Empty;
                sb.Clear();
                sb.Append("WHERE Isselect='是'");
                sb.Append(" order by ID");
                dt= SQLstr.GetIntrument(sb.ToString(),out err);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Global.TestInstrument = new string[dt.Rows.Count,2];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Global.TestInstrument[i,0] = dt.Rows[i][0].ToString();
                            WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[i][2].ToString();
                            Global.TestInstrument[i, 1] = dt.Rows[i][5].ToString();
                            Protocol = dt.Rows[0][6].ToString();
                            name = dt.Rows[0][0].ToString();
                        }
                    }
                    else
                    {
                        //没选择仪器时默认选择LZ2000
                        Global.TestInstrument = new string[1,2];
                        Global.TestInstrument[0,0] = "LZ-2000农药残留快检";
                        Global.TestInstrument[0, 1] = "LZ2000";
                        Protocol = "HIDLZ2000";
                        name = "LZ-2000农药残留快检";

                        lbFunction = new Label();
                        lbFunction.Size = new Size(220, 60);
                        lbFunction.Location = new Point(0,0);
                        lbFunction.Font = new Font("楷体_GB2312", 14);
                        lbFunction.Name = "LZ2000"; 
                        lbFunction.Text = "LZ-2000农药残留快检";
                        lbFunction.BackColor = Color.Transparent;
                        lbFunction.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                        lbFunction.TextAlign = ContentAlignment.MiddleCenter;
                        lbFunction.MouseClick += lbFunction_MouseClick;
                        lbFunction.MouseEnter += lbFunction_MouseEnter;
                        lbFunction.MouseLeave += lbFunction_MouseLeave;
                        Global.ChkManchine = "LZ-2000农药残留快检";
                        panelProject.Controls.Add(lbFunction);

                        PnlContent.Controls.Clear();
                        PnlContent.Controls.Add(lz2000);
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            
            panelProject.Controls.Clear();
            for (int t = 0; t < Global.TestInstrument.GetLength(0);t++ )
            {
                lbFunction = new Label();
                lbFunction.Size = new Size(220, 60);
                lbFunction.Location = new Point(0, t * 60);
                lbFunction.Font = new Font("楷体_GB2312", 14);
                lbFunction.Name =Global.TestInstrument[t,1];
                lbFunction.Text = Global.TestInstrument[t,0];
                lbFunction.BackColor = Color.Transparent;
                if (t == 0)
                {
                    lbFunction.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
                }
                lbFunction.TextAlign = ContentAlignment.MiddleCenter;
                lbFunction.MouseClick += lbFunction_MouseClick;
                lbFunction.MouseEnter += lbFunction_MouseEnter;
                lbFunction.MouseLeave += lbFunction_MouseLeave;
                
                panelProject.Controls.Add(lbFunction);
            }
            selectfuntion = Global.TestInstrument[0, 1];
            Global.ChkManchine = name;  //获取查询第一个仪器的名称

            //根据协议选择测试窗体
            if (Protocol == "RS232DY3000" )//DY1000、DY2000、DY3000、DY3100、TL300
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(dyserial);
                
            }
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
            else if (Protocol == "RS232TYZ24")
            {
                PnlContent.Controls.Clear();
                ty16.protocol = "RS232TYZ24";
                PnlContent.Controls.Add(ty16);
            }
               
        }
        private ucDY1000 dyserial = new ucDY1000();//DY1000、DY2000、DY3000、LZ3000
        private UCAll_LZ4000 AllLZ4000 = new UCAll_LZ4000();
        private UControl_LZ4000 ln4000 = new UControl_LZ4000();//辽宁版的LZ4000      
        private ucDY6100 dy6100 = new ucDY6100();        
        private ucDY6200 dy6200 = new ucDY6200();//YD6200协议
        private ucDY6400 dy6400 = new ucDY6400();
        private ucDYRS2 dyrs2 = new ucDYRS2();
        private ucDY8120 dy8120 = new ucDY8120();
        private ucDY8200 dy8200 = new ucDY8200();
        private ucTTNJ16 ty16 = new ucTTNJ16();//湖北泰扬

        private void lbFunction_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Image = null;
            for (int i = 0; i < Global.TestInstrument.GetLength(0); i++)
            {
                if(selectfuntion !="")
                {
                    //查找单击的label添加背景
                    (panelProject.Controls.Find(selectfuntion, false)[0] as Label).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png"); 
                }               
            }
        }
        
        private void lbFunction_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");             
        }
        /// <summary>
        /// 测试仪器单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbFunction_MouseClick(object sender, MouseEventArgs e)
        {
            string mprotocol = string.Empty;
            for (int i = 0; i < Global.TestInstrument.GetLength(0); i++)
            {
                (panelProject.Controls.Find(Global.TestInstrument[i,1], false)[0] as Label).Image  = null ; //查找所有的label清空背景                            
            }
            ((Label)sender).Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            selectfuntion = ((Label)sender).Name;

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (selectfuntion == dt.Rows[j][5].ToString())
                {
                    mprotocol = dt.Rows[j][6].ToString();
                    WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[j][2].ToString();
                    Global.ChkManchine = dt.Rows[j][0].ToString();
                }
            }

            //根据协议选择测试窗体
            if (mprotocol == "RS232DY3000")//DY1000、DY2000、DY3000、DY3100、TL300
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(dyserial);
                //发送消息通知事件改界面label
                cum.SendOutMessage("RS232DY3000", Global.ChkManchine);
            }
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
            else if (mprotocol == "RS232TY8")//泰扬按键8通道
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(ty16);
                //发送消息改label名称
                cum.SendOutMessage("RS232TY8", Global.ChkManchine);
            }
            else if (mprotocol == "RS232TY16")//泰扬按键16通道
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(ty16);
                //发送消息改label名称
                cum.SendOutMessage("RS232TY16", Global.ChkManchine);
            }
            else if (mprotocol == "RS232TYZ16")//泰扬智能型16通道
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(ty16);
                //发送消息改label名称
                cum.SendOutMessage("RS232TYZ16", Global.ChkManchine);
            }
            else if (mprotocol == "RS232TYZ24")//泰扬智能型24通道
            {
                PnlContent.Controls.Clear();
                PnlContent.Controls.Add(ty16);
                //发送消息改label名称
                cum.SendOutMessage("RS232TYZ24", Global.ChkManchine);
            }
        }
        private void labelDY100_Click(object Sender, EventArgs e)
        {
            labelDY100.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            ucDY1000 DY1000 = new ucDY1000();
            PnlContent.Controls.Clear();
            PnlContent.Controls.Add(DY1000);
            Global.ChkManchine = "DY-1000农药残留速测仪";
 
        }
        //LZ2000鼠标进入事件
        private void LZ200Label_Enter(object Sender, EventArgs e)
        {
            lz2000Label.Image  = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
            lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
        ucSearchData UCSD = new ucSearchData();
        //private bool opendia=false ;
        /// <summary>
        /// 数据中心单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void skinPanel4_MouseClick(object sender, MouseEventArgs e)
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

            //基础数据
            //labbasedata.Size = new Size(220, 60);
            //labbasedata.Location = new Point(0, 120);
            //labbasedata.Font = new Font("楷体_GB2312", 14);
            //labbasedata.Text = "基础数据";
            //labbasedata.FlatStyle = FlatStyle.Flat;
            //labbasedata.BackColor = Color.Transparent;
            //labbasedata.Image = null;
            //labbasedata.TextAlign = ContentAlignment.MiddleCenter;
            //labbasedata.Click += new System.EventHandler(labbasedata_Click);
            //labbasedata.MouseEnter += new System.EventHandler(labbasedata_Enter);
            //labbasedata.MouseLeave += new System.EventHandler(labbasedata_Leave);
            //panelProject.Controls.Add(labbasedata);
            //basedata = false;
            
            //单位信息
            labelUnit.Size = new Size(220, 60);
            labelUnit.Location = new Point(0, 120);
            labelUnit.Font = new Font("楷体_GB2312", 14);
            labelUnit.Text = "机构信息";
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
            dy.savediary(DateTime.Now.ToString(), "进入数据中心菜单", "OK");

            //按钮带图片显示
            //Button bt = new Button();
            //bt.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\picture.bmp");
            //bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //bt.ForeColor = Color.Red;//设置按钮前景色为红色
            //bt.FlatStyle = FlatStyle.Flat;//设置按钮以平面显示
            //bt.ForeColor = Color.Blue;//设置按钮前景色为蓝色
            //bt.ForeColor = Color.MediumBlue;//设置按钮前景色为绿色WorkstationUI
            //bt.FlatStyle = FlatStyle.Popup;//得到焦点后按钮为三维样式
            //bt.Size = new Size(218, 60);
            ////bt.FlatStyle = FlatStyle.System;//按钮外观由操作系统决定
            //bt.Font = new Font("隶书", 20);//设置按钮文字字体
            //bt.Location = new Point(0, 360);
            //bt.TextAlign = ContentAlignment.MiddleCenter;//字体对齐方式
            //bt.Text = "数据中心";          
            //panelProject.Controls.Add(bt);

            //数据上传
            //labDataUp.Size = new Size(220, 60);
            //labDataUp.Location = new Point(0, 240);
            //labDataUp.Font = new Font("楷体_GB2312", 14);
            //labDataUp.Text = "数据上传";
            //labDataUp.FlatStyle = FlatStyle.Flat;
            //labDataUp.BackColor = Color.Transparent;
            //labDataUp.Image = null;
            //labDataUp.TextAlign = ContentAlignment.MiddleCenter;
            //labDataUp.Click += new System.EventHandler(labDataUp_Click);
            //labDataUp.MouseEnter += new System.EventHandler(labDataUp_Enter);
            //labDataUp.MouseLeave += new System.EventHandler(labDataUp_Leave);
            //panelProject.Controls.Add(labDataUp);
            //dataup = false;
        }

        void labelSample_MouseEnter(object sender, EventArgs e)
        {
            labelSample.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }

        void labelSample_MouseLeave(object sender, EventArgs e)
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

     

        void labelUnit_MouseLeave(object sender, EventArgs e)
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

        void labelUnit_MouseEnter(object sender, EventArgs e)
        {
            labelUnit.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        /// <summary>
        /// 样品信息单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void labelSample_MouseClick(object sender, MouseEventArgs e)
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
        }
        /// <summary>
        /// 检测单位与被检单位信息单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void labelUnit_MouseClick(object sender, MouseEventArgs e)
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
        }
        private ucAddSample sam = new ucAddSample();
        private  ucAddUnit un = new ucAddUnit();
        private ucStatiscal ustatis = new ucStatiscal();
        private ucBaseData bd = new ucBaseData();

        private void labbasedata_Leave(object Sender, EventArgs e)
        {
            if (basedata == true)
            {
                labbasedata.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            }
            else 
            {
                labbasedata.Image = null;
            }
 
        }
        private void labbasedata_Enter(object Sender, EventArgs e)
        {
            labbasedata.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        /// <summary>
        /// 基础数据
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labbasedata_Click(object Sender, EventArgs e)
        {
            basedata = true;
            dataup = false ;
            backup = false;
            stati = false;
            labelprint = false;
            DataReturn = false;
            lbfindptint.Image = null;
            labStatistical.Image = null;
            labDataManage.Image = null;
            //labDataUp.Image = null;
            labbasedata.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            PnlContent.Controls.Clear();
            PnlContent.Controls.Add(bd);
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
            labDataReturn.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        /// <summary>
        /// 数据恢复
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labDataReturn_Click(object Sender, MouseEventArgs e)
        {
            DataReturn = true;
            dataup = false ;
            backup = false;
            stati = false;
            iunit = false ;
            isample = false;
            labelprint = false;
            basedata = false;
            lbfindptint.Image = null;
            labStatistical.Image = null;
            labDataManage.Image = null;
            labelSample.Image = null;
            labelUnit.Image = null;
            //labDataUp.Image = null;
            labbasedata = null;
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
            dy.savediary(DateTime.Now.ToString(),"进入数据恢复","OK");
 
        }

        private void labDataUp_Enter(object Sender, EventArgs e)
        {
            //labDataUp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        private void labDataUp_Leave(object Sender, EventArgs e)
        {
            //if (dataup == true)
            //{
            //    labDataUp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            //}
            //else
            //{
            //    labDataUp.Image = null;
            //}    
        }
        ucDataUpload DU = new ucDataUpload();
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="Sender"></param> 
        /// <param name="e"></param>
        private void labDataUp_Click(object Sender, EventArgs e)
        {
            dataup = true;
            backup = false;
            stati = false;
            labelprint = false;
            DataReturn = false;
            basedata = false;
            lbfindptint.Image = null;
            labStatistical.Image = null;
            labDataManage.Image = null;
            labbasedata.Image = null;
            //labDataUp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            PnlContent.Controls.Clear();
            PnlContent.Controls.Add(DU);
        }
        ucDataBackup DB = new ucDataBackup();
        /// <summary>
        /// 数据备份单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labDataManage_Click(object Sender, MouseEventArgs e)
        {
            backup = true ;
            dataup = false;          
            stati = false;
            labelprint = false;
            DataReturn = false;
            basedata = false;
            isample = false;
            iunit = false;
            lbfindptint.Image = null;
            labStatistical.Image = null;
            //labDataUp.Image= null;
            labDataReturn.Image = null;
            //labbasedata.Image = null;
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
                catch
                {
                    MessageBox.Show("数据库备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("数据库备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dy.savediary(DateTime.Now.ToString(), "进入数据备份", "OK");
        }
        private void labDataManage_Enter(object Sender, EventArgs e)
        {
            labDataManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
            Global.currentform = "统计分析";
            stati = true;
            labelprint = false;
            backup = false;
            dataup = false;
            DataReturn = false;
            basedata = false;
            isample = false;
            iunit = false;
            lbfindptint.Image = null;
            //labDataUp.Image = null;
            labDataManage.Image = null;
            labDataReturn.Image = null;
            //labbasedata.Image = null;     
            labelSample.Image = null;
            labelUnit.Image = null;
            labStatistical.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            if (Global.maxwindow == true)
            {
                ustatis.Width = 1140;
            }
            PnlContent.Controls.Clear();
            PnlContent.Controls.Add(ustatis);
            dy.savediary(DateTime.Now.ToString(), "进入统计分析", "OK");
        }
        //鼠标进入统计分析
        private void labStatistical_Enter(object Sender, EventArgs e)
        {
            labStatistical.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
            Global.currentform = "数据查询";
            PnLeft.Visible = true;
            labelprint = true;
            stati = false;
            backup = false;
            dataup = false;
            DataReturn = false;
            basedata = false;
            isample = false;
            iunit = false;
            labelSample.Image = null;
            labelUnit.Image = null;
            labStatistical.Image = null;
            labDataManage.Image = null;
            //labDataUp.Image = null;
            labDataReturn.Image = null;
            //labbasedata.Image = null;
            lbfindptint.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");                      
            PnlContent.Controls.Clear();
            if (Global.maxwindow == true)
            {
                UCSD.Width = 1135;
            }
            PnlContent.Controls.Add(UCSD);
            UCSD.searchdatabase();
            dy.savediary(DateTime.Now.ToString(), "进入数据查询", "OK");
        }
        /// <summary>
        /// 主页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMain_MouseClick(object sender, MouseEventArgs e)
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
                Mainpanel.Width  = 1400;
                mp.Width = iActulaWidth - 20;
            }
            
            Mainpanel.Controls.Clear();
            Mainpanel.Controls.Add(mp);
            dy.savediary(DateTime.Now.ToString(), "进入主页菜单", "OK");
        }
        ucSystemset ucsys = new ucSystemset();
        ucEquipmenManage UCEM = new ucEquipmenManage();
        private MainPage mp = new MainPage();
        /// <summary>
        /// 系统设置单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelSysSet_MouseClick(object sender, MouseEventArgs e)
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
       
            //panelProject.Controls.Clear();
            panelProject.Controls.Add(labelDairy);
            dairy = false;//恢复鼠标进入Label初始化值
            dy.savediary(DateTime.Now.ToString(), "进入系统设计菜单", "OK");

            labelUserManage.Size = new Size(220, 60);
            labelUserManage.Location = new Point(0, 180);
            labelUserManage.Font = new Font("楷体_GB2312", 14);
            labelUserManage.Text = "用户管理";
            labelUserManage.FlatStyle = FlatStyle.Flat;
            labelUserManage.BackColor = Color.Transparent;
            labelUserManage.Image = null; //Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            labelUserManage.TextAlign = ContentAlignment.MiddleCenter;

            panelProject.Controls.Add(labelUserManage);
            user = false;//恢复鼠标进入Label初始化值

            //labelHelp.Size = new Size(220, 60);
            //labelHelp.Location = new Point(0, 240);
            //labelHelp.Font = new Font("楷体_GB2312", 14);
            //labelHelp.Text = "系统帮助";
            //labelHelp.FlatStyle = FlatStyle.Flat;
            //labelHelp.BackColor = Color.Transparent;
            ////labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            //labelHelp.TextAlign = ContentAlignment.MiddleCenter;
           
            //labelHelp.Controls.Clear();
            //panelProject.Controls.Add(labelHelp);
            help = false;//恢复鼠标进入Label初始化值
            //关于
            labeAbout.Size = new Size(220, 60);
            labeAbout.Location = new Point(0, 240);
            labeAbout.Font = new Font("楷体_GB2312", 14);
            labeAbout.Text = "关于系统";
            labeAbout.FlatStyle = FlatStyle.Flat;
            labeAbout.BackColor = Color.Transparent;
            labeAbout.Image = null; //Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            labeAbout.TextAlign = ContentAlignment.MiddleCenter;
            
            //labeAbout.Controls.Clear();
            panelProject.Controls.Add(labeAbout);
            about = false;//恢复鼠标进入Label初始化值
            //系统更新
            labelSysUpdate.Size = new Size(220, 60);
            labelSysUpdate.Location = new Point(0, 300);
            labelSysUpdate.Font = new Font("楷体_GB2312", 14);
            labelSysUpdate.Text = "系统升级";
            labelSysUpdate.FlatStyle = FlatStyle.Flat;
            labelSysUpdate.BackColor = Color.Transparent;
            labelSysUpdate.Image = null; //Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            labelSysUpdate.TextAlign = ContentAlignment.MiddleCenter;
            
            panelProject.Controls.Add(labelSysUpdate);
            sysupdate = false;//恢复鼠标进入Label初始化值
        }
        private Label labSysSet = new Label();
        private Label labelUserManage = new Label();
        private Label labelHelp = new Label();
        private Label labeAbout = new Label();
        private Label labelServer = new Label();
        private Label labelDairy = new Label();
        private Label labelSysUpdate = new Label();
        //ucSystemset uss = new ucSystemset();
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
            labelSysUpdate.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        /// <summary>
        /// 系统升级
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelSysUpdate_click(object Sender, MouseEventArgs e)
        {
            frmSysUpdate UpD = new frmSysUpdate();

            sysupdate = true;
            dairy = false;
            about = false;
            help = false;
            user = false;
            labSetbool = false;
            server = false;
            labSysSet.Image = null;
            labelUserManage.Image = null;
            labelHelp.Image = null;
            labeAbout.Image = null;
            labelServer.Image = null;
            labelDairy.Image = null;
            labelSysUpdate.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            UpD.Show();
            dy.savediary(DateTime.Now.ToString(), "进入系统升级", "OK");
 
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
            labelDairy.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
 
        }
        /// <summary>
        /// 操作日记
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelDairy_click(object Sender, MouseEventArgs e)
        {          
            ucDiary dy = new ucDiary();
            dairy = true;
            about = false;
            help = false;
            user = false;
            labSetbool = false;
            server = false;
            sysupdate = false;
            labelSysUpdate.Image = null;
            labSysSet.Image = null;
            labelUserManage.Image = null;
            labelHelp.Image = null;
            labeAbout.Image = null;
            labelServer.Image = null;
            labelDairy.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            PnlContent.Controls.Clear();
            if (Global.maxwindow == true)
            {
                dy.Width = 1140;
            }
            PnlContent.Controls.Add(dy);
            Global.currentform = "操作日记";
            clsdiary d = new clsdiary();
            d.savediary(DateTime.Now.ToString(), "进入系统升级", "OK");
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
            labelServer.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
        //服务器配置单击事件
        private void labelServer_click(object Sender, EventArgs e)
        {
            Global.currentform = "服务器配置";
            ucServer sv = new ucServer();
            server = true;
            about = false ;
            help = false;
            user = false;
            labSetbool = false;
            dairy = false;
            sysupdate = false;
            labelSysUpdate.Image = null;
            labSysSet.Image = null;
            labelUserManage.Image = null;
            labelHelp.Image = null;
            labeAbout.Image = null;
            labelDairy.Image = null;
            labelServer.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            //PnlContent.Controls.Clear();
            //if (Global.maxwindow == true)
            //{
            //    sv.Width = 1150;
            //}
            //PnlContent.Controls.Add(sv);

            frmServer fs = new frmServer();
            fs.ShowDialog();
            //clsdiary d = new clsdiary();
            dy.savediary(DateTime.Now.ToString(), "进入服务器配置", "OK");
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
            labeAbout.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
 
        }
        
        /// <summary>
        /// 关于的单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labeAbout_click(object Sender, MouseEventArgs e)
        {
            about = true;
            help = false;
            user = false;
            labSetbool = false;
            server = false;
            dairy = false;
            sysupdate = false;
            labelSysUpdate.Image = null;
            labSysSet.Image = null;
            labelUserManage.Image = null;
            labelHelp.Image = null;
            labelServer.Image = null;
            labelDairy.Image = null;
            labeAbout.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            frmAbout fa = new frmAbout();
            fa.Show();
            //clsdiary d = new clsdiary();
            dy.savediary(DateTime.Now.ToString(), "进入关于", "OK");
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
            labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }      
        private ucUser us = new ucUser();
        /// <summary>
        /// 帮助单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labelHelp_click(object Sender, MouseEventArgs e)
        {          
            ucHelp uchp = new ucHelp();
           
            help = true;
            user = false;
            labSetbool = false;
            about = false;
            server = false;
            dairy = false;
            sysupdate = false;
            labelSysUpdate.Image = null;
            labeAbout.Image = null;
            labSysSet.Image = null;
            labelUserManage.Image = null;
            labelDairy.Image = null;
            labelServer.Image = null;
            labelHelp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            PnlContent.Controls.Clear();
            if (Global.maxwindow == true)
            {
                uchp.Width = 1144;
            }
            PnlContent.Controls.Add(uchp);
            Global.currentform = "帮助";
            dy.savediary(DateTime.Now.ToString(), "进入帮助", "OK");
        }
        private void labelHelp_Enter(object Sender, EventArgs e)
        {
            labelHelp.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
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
            Global.currentform = "用户管理";
            user = true;
            about = false ;
            labSetbool = false;
            server = false;
            dairy = false;
            help = false;
            sysupdate = false;
            labelServer.Image = null;
            labelDairy.Image = null;
            labSysSet.Image = null;
            labelHelp.Image = null;
            labeAbout.Image=null ;
            labelSysUpdate.Image = null;
            if (Global.maxwindow == true)
            {
                us.Width = 1150;
            }
            labelUserManage.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            PnlContent.Controls.Clear();
            PnlContent.Controls.Add(us);
        }
        /// <summary>
        /// 仪器管理单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void labSysSet_click(object Sender, MouseEventArgs e)
        {
            Global.currentform = "仪器管理";
            labSetbool = true;
            user = false;
            about = false ;
            server = false;
            dairy = false;
            sysupdate = false;
            labelSysUpdate.Image = null;
            labelDairy.Image = null;
            labelServer.Image = null;
            labeAbout.Image = null;
            labelHelp.Image = null;
            labSysSet.Image=Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
            labelUserManage.Image = null;
            PnlContent.Controls.Clear();
            if (Global.maxwindow == true)
            {
                UCEM.Width = 1130;
            }
            PnlContent.Controls.Add(UCEM);
            //clsdiary d = new clsdiary();
            dy.savediary(DateTime.Now.ToString(), "进入仪器管理", "OK");
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
            labSysSet.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "pic\\xuan.png");
        }
       
        //清理资源
        /// <summary>
        /// 清空Panel
        /// </summary>
        public void ClearPanel()
        {
            foreach (Control control in this.Controls)
            {
                //找到控件名称为PnlContent的Panel
                if ((control is Panel) && (control as Panel).Name.Equals("PnlContent"))
                {
                    //然后在panel控件中查找UserControl子控件
                    foreach (Control subcontrol in control.Controls)
                    {
                        //然后清理控件资源
                        subcontrol.Dispose();
                    }
                }
                if ((control is Panel) && (control as Panel).Name.Equals("Mainpanel"))
                {
                    //然后在panel控件中查找子控件
                    foreach (Control subcontrol in control.Controls)
                    {
                        //然后清理控件资源
                        subcontrol.Dispose();
                    }
                }
            }
        }
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
            
            DialogResult dr=MessageBox.Show("是否退出系统","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                //if (MessageNotification.GetInstance() != null)
                //{
                //    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.closeCOM, "closeCOM");
                //}
                ty16.closeTYCOM();
                ty16.closecomlink();
                clsdiary dy = new clsdiary();
                dy.savediary(DateTime.Now.ToString(), "系统退出", "成功");
                this.Close();
                Application.Exit();
            }
            else 
            {
                return;
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
            dy.savediary(DateTime.Now.ToString(), "进入商城菜单", "OK");
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
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                //labelclose.Location = new Point(1000, 7);
                //labelBig.Location = new Point(975, 9);
                //labelmin.Location = new Point(946, 7);
                //PnlUser.Location = new Point(850,55);
                //PnlTime.Location = new Point(850, 80);
                Global.maxwindow = false;
                if (Global.currentform == "商城")
                {
                    ucShopmall shop = new ucShopmall();
                    //shop.labelback.Location = new Point(880,2);
                    //shop.labelclose.Location = new Point(960,12);
                    Mainpanel.Width = 1010;
                    shop.Width = 1010;
                    Mainpanel.Controls.Clear();
                    Mainpanel.Controls.Add(shop);
                }
                if (Global.currentform == "仪器管理")
                {
                    UCEM.Width = 800;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(UCEM);
                }
                if (Global.currentform == "数据查询")
                {
                    UCSD.Width = 800;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(UCSD);
                }
                if (Global.currentform == "帮助")
                {
                    ucHelp uchp = new ucHelp();
                    uchp.Width = 800;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(uchp);
                }
                if (Global.currentform == "操作日记")
                {
                    ucDiary dy = new ucDiary();
                    dy.Width = 800;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy);
                }
                if (Global.currentform == "服务器配置")
                {
                    ucServer sv = new ucServer();
                    sv.Width = 800;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(sv);
                }
                if (Global.currentform == "统计分析")
                {
                    PnlContent.Controls.Clear();
                    ustatis.Width = 800;
                    PnlContent.Controls.Add(ustatis);
                }
                if (Global.currentform == "用户管理")
                {
                    PnlContent.Controls.Clear();
                    us.Width = 800;
                    PnlContent.Controls.Add(us);
                }
                if (Global.currentform == "样品信息")
                {
                    PnlContent.Controls.Clear();
                    sam.Width = 800;
                    PnlContent.Controls.Add(sam);
                }
                if (Global.currentform == "单位信息")
                {
                    PnlContent.Controls.Clear();
                    un.Width = 800;
                    PnlContent.Controls.Add(un);
                }
                if (Global.currentform == "LZ-4000农药残留快速测试仪")
                {
                    AllLZ4000.Width = 800;
                    PnlContent.Controls.Add(AllLZ4000);
                }
                if (Global.currentform == "主页")
                {
                    MainPage mpp = new MainPage();
                    mpp.Width = iActulaWidth - 350;
                    panelProject.Visible = false;

                    Mainpanel.Visible = true;
                    Mainpanel.Controls.Clear();
                    Mainpanel.Controls.Add(mpp);
                }
            }
            else 
            {
                this.WindowState = FormWindowState.Maximized;
                //labelclose.Location = new Point(1340, 5);
                //labelBig.Location = new Point(1315, 7);
                //labelmin.Location = new Point(1286, 5);
                //PnlUser.Location = new Point(1180, 55);
                //PnlTime.Location = new Point(1180, 80);
                Global.maxwindow = true;
                if (Global.currentform == "仪器管理")
                {
                    UCEM.Width = 1110;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(UCEM);
                }
                if (Global.currentform == "数据查询")
                {
                    UCSD.Width = 1130;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(UCSD);
                }
                if (Global.currentform == "商城")
                {
                    ucShopmall shop = new ucShopmall();           
                    shop.Width = 1345;
                    Mainpanel.Width = 1345;
                    Mainpanel.Controls.Clear();
                    Mainpanel.Controls.Add(shop);
                }
                if (Global.currentform == "帮助")
                {
                    ucHelp uchp = new ucHelp();
                    uchp.Width = 1144;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(uchp);
                }
                if(Global.currentform == "服务器配置")
                {
                    ucServer sv = new ucServer();
                    PnlContent.Controls.Clear();
                    sv.Width = 1150;
                    PnlContent.Controls.Add(sv);
                }
                if (Global.currentform == "操作日记")
                {
                    ucDiary dy = new ucDiary();
                    dy.Width = 1150;
                    PnlContent.Controls.Clear();
                    PnlContent.Controls.Add(dy);
                }
                if (Global.currentform == "统计分析")
                {
                    PnlContent.Controls.Clear();
                    ustatis.Width = 1150;
                    PnlContent.Controls.Add(ustatis);
                }
                if (Global.currentform == "用户管理")
                {
                    PnlContent.Controls.Clear();
                    us.Width = 1150;
                    PnlContent.Controls.Add(us);
                }
                if (Global.currentform == "样品信息")
                {
                    PnlContent.Controls.Clear();
                    sam.Width = 1150;
                    PnlContent.Controls.Add(sam);
                }
                if (Global.currentform == "单位信息")
                {
                    PnlContent.Controls.Clear();
                    un.Width = 1150;
                    PnlContent.Controls.Add(un);
                }
                if (Global.currentform == "主页")
                {                 
                    Mainpanel.Visible = true;
                    mp.Width = iActulaWidth - 20; ;
                    //Mainpanel.Width = 1400;
                    Mainpanel.Controls.Clear();
                    Mainpanel.Controls.Add(mp);

                }
                if (Global.currentform == "LZ-4000农药残留快速测试仪")
                {
                    AllLZ4000.Width = 1400;
                    PnlContent.Controls.Add(AllLZ4000);
                }
            }
        }
    }
}
