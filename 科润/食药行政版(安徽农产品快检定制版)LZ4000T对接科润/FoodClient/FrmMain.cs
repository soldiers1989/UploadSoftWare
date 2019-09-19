using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DesktopAppSkin;
//using ET199ComLib;//加密狗引用命名空间
using DY.FoodClientLib;
using FoodClient.Query;
using FoodClient.Test;
using FoodClient.ATP;

//命名空间已经修改过，
//原命名空间：WindowsFormClient。
//修改: 2011-06-21
namespace FoodClient
{
    /// <summary>
    ///主窗体类
    /// </summary>
    public class FrmMain : System.Windows.Forms.Form
    {
        /// <summary>
        /// 自身实例对象
        /// </summary>
        public static FrmMain formMain;

        /// <summary>
        /// 是否已经加载了被检测单位
        /// </summary>
        public static bool AllownChange = false;

        public static bool IsLoadCheckedComSel = false;
        public static bool IsLoadCheckedUpperComSel = false;

        /// <summary>
        /// 选择被检单位
        /// </summary>
        public static frmCheckedCompany formCheckedComSelect;

        /// <summary>
        /// 所属组织机构,//这个对象也需要修改
        /// </summary>
        public static frmCheckedComSelect formCheckedUpperComSelect;

        public static DataTable CompanyTable = null;

        public static string IFGO = string.Empty;
        #region 注掉
        //private FrmAutoTakeLD frmAutoLD ; //原来是 public static
        //private FrmAutoTakeDY5000 frmAutoTakeDY5000;//原来是 public static
        //public static frmMachineEdit formMachineEdit;//对此对象不明白作者的用意？？？？
        //DY1000，DY2000，DY3000已经合并为一个AutoTakeDYSeries,相同的协议，没有必要写三个窗口
        //public static frmAutoTakeDY2000DY formAutoTakeDY2000DY;
        //public static frmAutoTakeDY1000DY formAutoTakeDY1000DY;
        //public static FrmAutoTakeLD formAutoTakeDY3000;
        //此字段设置了又没有用到，实在浪费。现在去掉2011-06-22
        //private readonly string sResultType = ShareOption.ResultTypeCode1;
        #endregion

        /// <summary>
        /// 当前用户单位代码
        /// </summary>
        internal string checkUnitCode;
        /// <summary>
        /// 用户代码
        /// </summary>
        internal string userCode;

        private int max = 0;
        private Hashtable htbl = null;//用户储存菜单

        /// <summary>
        /// 表示快捷方式的仪器操作.存储仪器类型字段如:DY1000
        /// </summary>
        private string defaultMethodTag;

        //private string[,] checkItems;

        #region 窗体私有变量

        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private ToolTip ToolTipMain;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem collectTSMI;
        private ToolStripMenuItem checkUploadTSMI;
        private ToolStripMenuItem baseDataTSMI;
        private ToolStripMenuItem sysTSMI;
        private ToolStripMenuItem windowTSMI;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem taizhanMngTSMI;
        private ToolStripMenuItem otherDataHandTSMI;
        private ToolStripMenuItem goodsIncomeTSMI;
        private ToolStripMenuItem salesIncomeTSMI;
        private ToolStripMenuItem goodsIncomeQueryTSMI;
        private ToolStripMenuItem salesQueryTSMI;
        private ToolStripMenuItem tZDataUploadTSMI;
        private ToolStripMenuItem checkItemMngTSMI;
        private ToolStripMenuItem foodClassMngTSMI;
        private ToolStripMenuItem checkTypeMngTSMI;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem orgMngTSMI;
        private ToolStripMenuItem checkPointTypeMngTSMI;
        private ToolStripMenuItem checkPointMngTSMI;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem unitTypeMngTSMI;
        private ToolStripMenuItem beCheckUnitMngTSMI;
        private ToolStripMenuItem producerUnitMngTSMI;
        private ToolStripMenuItem producePlaceMngTSMI;
        private ToolStripMenuItem userRoleMngTSMI;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem dataBaseMngTSMI;
        private ToolStripMenuItem dbBackupTSMI;
        private ToolStripMenuItem dbRestoreTSMI;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem optionTSMI;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem logoutTSMI;
        private ToolStripMenuItem exitTSMI;
        private ToolStripMenuItem layoutHorizontalTSMI;
        private ToolStripMenuItem layoutVerticalTSMI;
        private ToolStripMenuItem layoutCascadeTSMI;
        private ToolStripMenuItem systemHelpTSMI;
        private ToolStripMenuItem timerCountTSMI;
        private ToolStripMenuItem autoUpdateTSMI;
        private ToolStripMenuItem AboutTSMI;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbAutoCheck;
        private ToolStripButton tsbHandCheck;
        private ToolStripButton tsbDataGeneralQuery;
        private ToolStripButton tsbDataUpload;
        private ToolStripButton tsbDataMng;
        private ToolStripButton tsbDataBackup;
        private ToolStripButton tsbDataRestore;
        private ToolStripButton tsbTimer;
        private ToolStripButton tsbRelogin;
        private ToolStripButton tsbExit;
        private ToolStripButton tsbOption;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator TsSeparatorDownload;
        private ToolStripMenuItem baseDataDownloadTSMI;
        private Panel panelMenuToolContainer;
        private Panel panelBottomBorder;
        private Panel panelRightBorder2;
        private Panel panelLeftBorder2;
        private Panel panelMenuToobar;
        private Panel panelLeftBorder1;
        private Panel panelRightBorder1;
        private Label lblSystemName;
        private Button btnMinimizeBox;
        private Button btnClose;
        private Button btnMaximizeBox;
        private ToolStripMenuItem standarQueryTSMI;
        private ToolStripMenuItem otherQueryTSMI;
        private ToolStripMenuItem checkDataGeneralQueryTSMI;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem checkResultStatisticsTSMI;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem checkDataUploadTSMI;
        private ToolStripMenuItem checkDataUploadAgainTSMI;
        private Label lblCompany;
        private PictureBox pictureBox1;
        private Label lblUserName;
        private ToolStripButton toolStripbtnShowReport;
        private ToolStripMenuItem 信息管理ToolStripMenuItem;
        private ToolStripMenuItem taskTSMI;
        private ToolStripMenuItem noticeTSMI;

        private System.ComponentModel.IContainer components;
        #endregion

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ToolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.collectTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkUploadTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.standarQueryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.otherQueryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDataGeneralQueryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkResultStatisticsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.checkDataUploadTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDataUploadAgainTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.baseDataTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkItemMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.foodClassMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTypeMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.orgMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkPointTypeMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.checkPointMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.unitTypeMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.beCheckUnitMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.producerUnitMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.producePlaceMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.TsSeparatorDownload = new System.Windows.Forms.ToolStripSeparator();
            this.baseDataDownloadTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.taizhanMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsIncomeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.salesIncomeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsIncomeQueryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.salesQueryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tZDataUploadTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.sysTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.userRoleMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.dataBaseMngTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dbBackupTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dbRestoreTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.optionTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.exitTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.noticeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.windowTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutHorizontalTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutVerticalTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutCascadeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemHelpTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCountTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.autoUpdateTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.otherDataHandTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbHandCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbDataGeneralQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnShowReport = new System.Windows.Forms.ToolStripButton();
            this.tsbDataUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDataMng = new System.Windows.Forms.ToolStripButton();
            this.tsbDataBackup = new System.Windows.Forms.ToolStripButton();
            this.tsbDataRestore = new System.Windows.Forms.ToolStripButton();
            this.tsbOption = new System.Windows.Forms.ToolStripButton();
            this.tsbTimer = new System.Windows.Forms.ToolStripButton();
            this.tsbRelogin = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.panelMenuToolContainer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMaximizeBox = new System.Windows.Forms.Button();
            this.btnMinimizeBox = new System.Windows.Forms.Button();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.panelMenuToobar = new System.Windows.Forms.Panel();
            this.panelLeftBorder1 = new System.Windows.Forms.Panel();
            this.panelRightBorder1 = new System.Windows.Forms.Panel();
            this.panelBottomBorder = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.panelRightBorder2 = new System.Windows.Forms.Panel();
            this.panelLeftBorder2 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelMenuToolContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMenuToobar.SuspendLayout();
            this.panelBottomBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "bak";
            this.saveFileDialog1.Filter = "备份文件(*.bak)|所有文件(*.*)";
            this.saveFileDialog1.Title = "数据库备份";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "bak";
            this.openFileDialog1.Filter = "备份文件(*.bak)|所有文件(*.*)";
            this.openFileDialog1.Title = "数据库恢复";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(202)))), ((int)(((byte)(187)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collectTSMI,
            this.checkUploadTSMI,
            this.baseDataTSMI,
            this.taizhanMngTSMI,
            this.sysTSMI,
            this.信息管理ToolStripMenuItem,
            this.windowTSMI,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 0);
            this.menuStrip1.MdiWindowListItem = this.windowTSMI;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 30);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // collectTSMI
            // 
            this.collectTSMI.Name = "collectTSMI";
            this.collectTSMI.Size = new System.Drawing.Size(103, 26);
            this.collectTSMI.Text = "数据采集(&I)";
            // 
            // checkUploadTSMI
            // 
            this.checkUploadTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.standarQueryTSMI,
            this.otherQueryTSMI,
            this.checkDataGeneralQueryTSMI,
            this.toolStripSeparator3,
            this.checkResultStatisticsTSMI,
            this.toolStripSeparator4,
            this.checkDataUploadTSMI,
            this.checkDataUploadAgainTSMI});
            this.checkUploadTSMI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkUploadTSMI.Name = "checkUploadTSMI";
            this.checkUploadTSMI.Size = new System.Drawing.Size(109, 26);
            this.checkUploadTSMI.Text = "数据查询(&C)";
            this.checkUploadTSMI.Visible = false;
            // 
            // standarQueryTSMI
            // 
            this.standarQueryTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.standarQueryTSMI.Name = "standarQueryTSMI";
            this.standarQueryTSMI.Size = new System.Drawing.Size(256, 26);
            this.standarQueryTSMI.Text = "标准速测法检测数据查询";
            this.standarQueryTSMI.Visible = false;
            this.standarQueryTSMI.Click += new System.EventHandler(this.standarQueryTSMI_Click);
            // 
            // otherQueryTSMI
            // 
            this.otherQueryTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.otherQueryTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.otherQueryTSMI.Name = "otherQueryTSMI";
            this.otherQueryTSMI.Size = new System.Drawing.Size(256, 26);
            this.otherQueryTSMI.Text = "其他速测法检测数据查询";
            this.otherQueryTSMI.Visible = false;
            this.otherQueryTSMI.Click += new System.EventHandler(this.otherQueryTSMI_Click);
            // 
            // checkDataGeneralQueryTSMI
            // 
            this.checkDataGeneralQueryTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.checkDataGeneralQueryTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkDataGeneralQueryTSMI.Name = "checkDataGeneralQueryTSMI";
            this.checkDataGeneralQueryTSMI.Size = new System.Drawing.Size(256, 26);
            this.checkDataGeneralQueryTSMI.Text = "检测数据综合查询";
            this.checkDataGeneralQueryTSMI.Click += new System.EventHandler(this.CheckDataGeneralQueryTSMI_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(253, 6);
            // 
            // checkResultStatisticsTSMI
            // 
            this.checkResultStatisticsTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.checkResultStatisticsTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkResultStatisticsTSMI.Name = "checkResultStatisticsTSMI";
            this.checkResultStatisticsTSMI.Size = new System.Drawing.Size(256, 26);
            this.checkResultStatisticsTSMI.Text = "理化检验数据统计";
            this.checkResultStatisticsTSMI.Visible = false;
            this.checkResultStatisticsTSMI.Click += new System.EventHandler(this.CheckResultStatisticsTSMI_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(253, 6);
            // 
            // checkDataUploadTSMI
            // 
            this.checkDataUploadTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.checkDataUploadTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkDataUploadTSMI.Name = "checkDataUploadTSMI";
            this.checkDataUploadTSMI.Size = new System.Drawing.Size(256, 26);
            this.checkDataUploadTSMI.Text = "检测数据上传";
            this.checkDataUploadTSMI.Click += new System.EventHandler(this.CheckDataUploadTSMI_Click);
            // 
            // checkDataUploadAgainTSMI
            // 
            this.checkDataUploadAgainTSMI.BackColor = System.Drawing.SystemColors.Control;
            this.checkDataUploadAgainTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkDataUploadAgainTSMI.Name = "checkDataUploadAgainTSMI";
            this.checkDataUploadAgainTSMI.Size = new System.Drawing.Size(256, 26);
            this.checkDataUploadAgainTSMI.Text = "检测数据重传维护";
            this.checkDataUploadAgainTSMI.Visible = false;
            this.checkDataUploadAgainTSMI.Click += new System.EventHandler(this.CheckDataUploadAgainTSMI_Click);
            // 
            // baseDataTSMI
            // 
            this.baseDataTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkItemMngTSMI,
            this.foodClassMngTSMI,
            this.checkTypeMngTSMI,
            this.toolStripSeparator5,
            this.orgMngTSMI,
            this.checkPointTypeMngTSMI,
            this.checkPointMngTSMI,
            this.toolStripSeparator6,
            this.unitTypeMngTSMI,
            this.beCheckUnitMngTSMI,
            this.producerUnitMngTSMI,
            this.producePlaceMngTSMI,
            this.TsSeparatorDownload,
            this.baseDataDownloadTSMI});
            this.baseDataTSMI.Name = "baseDataTSMI";
            this.baseDataTSMI.Size = new System.Drawing.Size(109, 26);
            this.baseDataTSMI.Text = "基础数据(&B)";
            //this.baseDataTSMI.Click += new System.EventHandler(this.baseDataTSMI_Click);
            // 
            // checkItemMngTSMI
            // 
            this.checkItemMngTSMI.Name = "checkItemMngTSMI";
            this.checkItemMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.checkItemMngTSMI.Text = "检测项目维护";
            this.checkItemMngTSMI.Visible = false;
            this.checkItemMngTSMI.Click += new System.EventHandler(this.CheckItemMngTSMI_Click);
            // 
            // foodClassMngTSMI
            // 
            this.foodClassMngTSMI.Name = "foodClassMngTSMI";
            this.foodClassMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.foodClassMngTSMI.Text = "食品种类维护";
            this.foodClassMngTSMI.Click += new System.EventHandler(this.FoodClassMngTSMI_Click);
            // 
            // checkTypeMngTSMI
            // 
            this.checkTypeMngTSMI.Name = "checkTypeMngTSMI";
            this.checkTypeMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.checkTypeMngTSMI.Text = "检测类别维护";
            this.checkTypeMngTSMI.Visible = false;
            this.checkTypeMngTSMI.Click += new System.EventHandler(this.CheckTypeMngTSMI_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(189, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // orgMngTSMI
            // 
            this.orgMngTSMI.Name = "orgMngTSMI";
            this.orgMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.orgMngTSMI.Text = "组织机构维护";
            this.orgMngTSMI.Visible = false;
            this.orgMngTSMI.Click += new System.EventHandler(this.OrgMngTSMI_Click);
            // 
            // checkPointTypeMngTSMI
            // 
            this.checkPointTypeMngTSMI.Name = "checkPointTypeMngTSMI";
            this.checkPointTypeMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.checkPointTypeMngTSMI.Text = "检测点类型维护";
            this.checkPointTypeMngTSMI.Visible = false;
            this.checkPointTypeMngTSMI.Click += new System.EventHandler(this.CheckPointTypeMngTSMI_Click);
            // 
            // checkPointMngTSMI
            // 
            this.checkPointMngTSMI.Name = "checkPointMngTSMI";
            this.checkPointMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.checkPointMngTSMI.Text = "检测点设置";
            this.checkPointMngTSMI.Visible = false;
            this.checkPointMngTSMI.Click += new System.EventHandler(this.CheckPointMngTSMI_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(189, 6);
            this.toolStripSeparator6.Visible = false;
            // 
            // unitTypeMngTSMI
            // 
            this.unitTypeMngTSMI.Name = "unitTypeMngTSMI";
            this.unitTypeMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.unitTypeMngTSMI.Text = "单位类别维护";
            this.unitTypeMngTSMI.Visible = false;
            this.unitTypeMngTSMI.Click += new System.EventHandler(this.UnitTypeMngTSMI_Click);
            // 
            // beCheckUnitMngTSMI
            // 
            this.beCheckUnitMngTSMI.Name = "beCheckUnitMngTSMI";
            this.beCheckUnitMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.beCheckUnitMngTSMI.Text = "被检单位维护";
            this.beCheckUnitMngTSMI.Click += new System.EventHandler(this.BeCheckUnitMngTSMI_Click);
            // 
            // producerUnitMngTSMI
            // 
            this.producerUnitMngTSMI.Name = "producerUnitMngTSMI";
            this.producerUnitMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.producerUnitMngTSMI.Click +=new EventHandler( ProducerUnitMngTSMI_Click);
            // 
            // producePlaceMngTSMI
            // 
            this.producePlaceMngTSMI.Name = "producePlaceMngTSMI";
            this.producePlaceMngTSMI.Size = new System.Drawing.Size(192, 26);
            this.producePlaceMngTSMI.Text = "产品产地维护";
            this.producePlaceMngTSMI.Visible = false;
            this.producePlaceMngTSMI.Click += new System.EventHandler(this.ProducePlaceMngTSMI_Click);
            // 
            // TsSeparatorDownload
            // 
            this.TsSeparatorDownload.Name = "TsSeparatorDownload";
            this.TsSeparatorDownload.Size = new System.Drawing.Size(189, 6);
            // 
            // baseDataDownloadTSMI
            // 
            this.baseDataDownloadTSMI.Name = "baseDataDownloadTSMI";
            this.baseDataDownloadTSMI.Size = new System.Drawing.Size(192, 26);
            this.baseDataDownloadTSMI.Text = "基础数据同步";
            this.baseDataDownloadTSMI.Click += new System.EventHandler(this.baseDataDownloadTSMI_Click);
            // 
            // taizhanMngTSMI
            // 
            this.taizhanMngTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goodsIncomeTSMI,
            this.salesIncomeTSMI,
            this.goodsIncomeQueryTSMI,
            this.salesQueryTSMI,
            this.tZDataUploadTSMI});
            this.taizhanMngTSMI.Name = "taizhanMngTSMI";
            this.taizhanMngTSMI.Size = new System.Drawing.Size(108, 26);
            this.taizhanMngTSMI.Text = "台帐管理(&T)";
            // 
            // goodsIncomeTSMI
            // 
            this.goodsIncomeTSMI.Name = "goodsIncomeTSMI";
            this.goodsIncomeTSMI.Size = new System.Drawing.Size(176, 26);
            this.goodsIncomeTSMI.Text = "进货台帐录入";
            this.goodsIncomeTSMI.Click += new System.EventHandler(this.goodsIncomeTSMI_Click);
            // 
            // salesIncomeTSMI
            // 
            this.salesIncomeTSMI.Name = "salesIncomeTSMI";
            this.salesIncomeTSMI.Size = new System.Drawing.Size(176, 26);
            this.salesIncomeTSMI.Text = "销售台帐录入";
            this.salesIncomeTSMI.Click += new System.EventHandler(this.salesIncomeTSMI_Click);
            // 
            // goodsIncomeQueryTSMI
            // 
            this.goodsIncomeQueryTSMI.Name = "goodsIncomeQueryTSMI";
            this.goodsIncomeQueryTSMI.Size = new System.Drawing.Size(176, 26);
            this.goodsIncomeQueryTSMI.Text = "进货台帐查询";
            // 
            // salesQueryTSMI
            // 
            this.salesQueryTSMI.Name = "salesQueryTSMI";
            this.salesQueryTSMI.Size = new System.Drawing.Size(176, 26);
            this.salesQueryTSMI.Text = "销售台帐查询";
            // 
            // tZDataUploadTSMI
            // 
            this.tZDataUploadTSMI.Name = "tZDataUploadTSMI";
            this.tZDataUploadTSMI.Size = new System.Drawing.Size(176, 26);
            this.tZDataUploadTSMI.Text = "数据台帐上传";
            // 
            // sysTSMI
            // 
            this.sysTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userRoleMngTSMI,
            this.toolStripSeparator7,
            this.dataBaseMngTSMI,
            this.dbBackupTSMI,
            this.dbRestoreTSMI,
            this.toolStripSeparator8,
            this.optionTSMI,
            this.toolStripSeparator9,
            this.logoutTSMI,
            this.exitTSMI});
            this.sysTSMI.Name = "sysTSMI";
            this.sysTSMI.Size = new System.Drawing.Size(108, 26);
            this.sysTSMI.Text = "系统管理(&S)";
            // 
            // userRoleMngTSMI
            // 
            this.userRoleMngTSMI.Name = "userRoleMngTSMI";
            this.userRoleMngTSMI.Size = new System.Drawing.Size(176, 26);
            this.userRoleMngTSMI.Text = "用户权限管理";
            this.userRoleMngTSMI.Click += new System.EventHandler(this.UserRoleMngTSMI_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(173, 6);
            this.toolStripSeparator7.Visible = false;
            // 
            // dataBaseMngTSMI
            // 
            this.dataBaseMngTSMI.Name = "dataBaseMngTSMI";
            this.dataBaseMngTSMI.Size = new System.Drawing.Size(176, 26);
            this.dataBaseMngTSMI.Text = "数据库维护";
            this.dataBaseMngTSMI.Visible = false;
            this.dataBaseMngTSMI.Click += new System.EventHandler(this.DataBaseMngTSMI_Click);
            // 
            // dbBackupTSMI
            // 
            this.dbBackupTSMI.Name = "dbBackupTSMI";
            this.dbBackupTSMI.Size = new System.Drawing.Size(176, 26);
            this.dbBackupTSMI.Text = "数据库备份";
            this.dbBackupTSMI.Visible = false;
            this.dbBackupTSMI.Click += new System.EventHandler(this.DbBackupTSMI_Click);
            // 
            // dbRestoreTSMI
            // 
            this.dbRestoreTSMI.Name = "dbRestoreTSMI";
            this.dbRestoreTSMI.Size = new System.Drawing.Size(176, 26);
            this.dbRestoreTSMI.Text = "数据库恢复";
            this.dbRestoreTSMI.Visible = false;
            this.dbRestoreTSMI.Click += new System.EventHandler(this.DbRestoreTSMI_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(173, 6);
            // 
            // optionTSMI
            // 
            this.optionTSMI.Name = "optionTSMI";
            this.optionTSMI.Size = new System.Drawing.Size(176, 26);
            this.optionTSMI.Text = "选项";
            this.optionTSMI.Click += new System.EventHandler(this.OptionTSMI_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(173, 6);
            // 
            // logoutTSMI
            // 
            this.logoutTSMI.Name = "logoutTSMI";
            this.logoutTSMI.Size = new System.Drawing.Size(176, 26);
            this.logoutTSMI.Text = "注销";
            this.logoutTSMI.Click += new System.EventHandler(this.LogoutTSMI_Click);
            // 
            // exitTSMI
            // 
            this.exitTSMI.Name = "exitTSMI";
            this.exitTSMI.Size = new System.Drawing.Size(176, 26);
            this.exitTSMI.Text = "退出";
            this.exitTSMI.Click += new System.EventHandler(this.ExitTSMI_Click);
            // 
            // 信息管理ToolStripMenuItem
            // 
            this.信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskTSMI,
            this.noticeTSMI});
            this.信息管理ToolStripMenuItem.Name = "信息管理ToolStripMenuItem";
            this.信息管理ToolStripMenuItem.Size = new System.Drawing.Size(86, 26);
            this.信息管理ToolStripMenuItem.Text = "信息管理";
            this.信息管理ToolStripMenuItem.Visible = false;
            // 
            // taskTSMI
            // 
            this.taskTSMI.Name = "taskTSMI";
            this.taskTSMI.Size = new System.Drawing.Size(144, 26);
            this.taskTSMI.Text = "任务接收";
            this.taskTSMI.Click += new System.EventHandler(this.taskTSMI_Click);
            // 
            // noticeTSMI
            // 
            this.noticeTSMI.Name = "noticeTSMI";
            this.noticeTSMI.Size = new System.Drawing.Size(144, 26);
            this.noticeTSMI.Text = "通知接收";
            this.noticeTSMI.Click += new System.EventHandler(this.noticeTSMI_Click);
            // 
            // windowTSMI
            // 
            this.windowTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutHorizontalTSMI,
            this.layoutVerticalTSMI,
            this.layoutCascadeTSMI});
            this.windowTSMI.Name = "windowTSMI";
            this.windowTSMI.Size = new System.Drawing.Size(83, 26);
            this.windowTSMI.Text = "窗口(&W)";
            this.windowTSMI.Visible = false;
            this.windowTSMI.DropDownOpening += new System.EventHandler(this.windowTSMI_DropDownOpening);
            // 
            // layoutHorizontalTSMI
            // 
            this.layoutHorizontalTSMI.Name = "layoutHorizontalTSMI";
            this.layoutHorizontalTSMI.Size = new System.Drawing.Size(144, 26);
            this.layoutHorizontalTSMI.Text = "水平平铺";
            this.layoutHorizontalTSMI.Click += new System.EventHandler(this.LayoutHorizontalTSMI_Click);
            // 
            // layoutVerticalTSMI
            // 
            this.layoutVerticalTSMI.Name = "layoutVerticalTSMI";
            this.layoutVerticalTSMI.Size = new System.Drawing.Size(144, 26);
            this.layoutVerticalTSMI.Text = "垂直平铺";
            this.layoutVerticalTSMI.Click += new System.EventHandler(this.LayoutVerticalTSMI_Click);
            // 
            // layoutCascadeTSMI
            // 
            this.layoutCascadeTSMI.Name = "layoutCascadeTSMI";
            this.layoutCascadeTSMI.Size = new System.Drawing.Size(144, 26);
            this.layoutCascadeTSMI.Text = "重叠";
            this.layoutCascadeTSMI.Click += new System.EventHandler(this.LayoutCascadeTSMI_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemHelpTSMI,
            this.timerCountTSMI,
            this.toolStripSeparator1,
            this.autoUpdateTSMI,
            this.AboutTSMI});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(79, 26);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // systemHelpTSMI
            // 
            this.systemHelpTSMI.Name = "systemHelpTSMI";
            this.systemHelpTSMI.Size = new System.Drawing.Size(160, 26);
            this.systemHelpTSMI.Text = "系统帮助";
            this.systemHelpTSMI.Click += new System.EventHandler(this.SystemHelpTSMI_Click);
            // 
            // timerCountTSMI
            // 
            this.timerCountTSMI.Name = "timerCountTSMI";
            this.timerCountTSMI.Size = new System.Drawing.Size(160, 26);
            this.timerCountTSMI.Text = "计时工具";
            this.timerCountTSMI.Click += new System.EventHandler(this.TimerCountTSMI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // autoUpdateTSMI
            // 
            this.autoUpdateTSMI.Name = "autoUpdateTSMI";
            this.autoUpdateTSMI.Size = new System.Drawing.Size(160, 26);
            this.autoUpdateTSMI.Text = "在线更新";
            this.autoUpdateTSMI.Click += new System.EventHandler(this.AutoUpdateTSMI_Click);
            // 
            // AboutTSMI
            // 
            this.AboutTSMI.Name = "AboutTSMI";
            this.AboutTSMI.Size = new System.Drawing.Size(160, 26);
            this.AboutTSMI.Text = "关于本软件";
            this.AboutTSMI.Click += new System.EventHandler(this.AboutTSMI_Click);
            // 
            // otherDataHandTSMI
            // 
            this.otherDataHandTSMI.Name = "otherDataHandTSMI";
            this.otherDataHandTSMI.Size = new System.Drawing.Size(190, 22);
            this.otherDataHandTSMI.Text = "其他检测数据手工录入";
            this.otherDataHandTSMI.Click += new System.EventHandler(this.OtherDataHandTSMI_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(223)))), ((int)(((byte)(205)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAutoCheck,
            this.tsbHandCheck,
            this.tsbDataGeneralQuery,
            this.toolStripbtnShowReport,
            this.tsbDataUpload,
            this.tsbDataMng,
            this.tsbDataBackup,
            this.tsbDataRestore,
            this.tsbOption,
            this.tsbTimer,
            this.tsbRelogin,
            this.tsbExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(5, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(795, 42);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "快捷菜单";
            // 
            // tsbAutoCheck
            // 
            this.tsbAutoCheck.AutoSize = false;
            this.tsbAutoCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoCheck.Image")));
            this.tsbAutoCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoCheck.Name = "tsbAutoCheck";
            this.tsbAutoCheck.Size = new System.Drawing.Size(94, 39);
            this.tsbAutoCheck.Text = "自动检测";
            this.tsbAutoCheck.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbAutoCheck.Click += new System.EventHandler(this.tsbAutoCheck_Click);
            // 
            // tsbHandCheck
            // 
            this.tsbHandCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbHandCheck.Image")));
            this.tsbHandCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHandCheck.Name = "tsbHandCheck";
            this.tsbHandCheck.Size = new System.Drawing.Size(94, 39);
            this.tsbHandCheck.Text = "手工录入";
            this.tsbHandCheck.Click += new System.EventHandler(this.tsbHandCheck_Click);
            // 
            // tsbDataGeneralQuery
            // 
            this.tsbDataGeneralQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataGeneralQuery.Image")));
            this.tsbDataGeneralQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataGeneralQuery.Name = "tsbDataGeneralQuery";
            this.tsbDataGeneralQuery.Size = new System.Drawing.Size(94, 39);
            this.tsbDataGeneralQuery.Text = "综合查询";
            this.tsbDataGeneralQuery.Click += new System.EventHandler(this.CheckDataGeneralQueryTSMI_Click);
            // 
            // toolStripbtnShowReport
            // 
            this.toolStripbtnShowReport.Image = global::FoodClient.Properties.Resources.report;
            this.toolStripbtnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnShowReport.Name = "toolStripbtnShowReport";
            this.toolStripbtnShowReport.Size = new System.Drawing.Size(94, 39);
            this.toolStripbtnShowReport.Text = "查看报表";
            this.toolStripbtnShowReport.Visible = false;
            this.toolStripbtnShowReport.Click += new System.EventHandler(this.toolStripbtnShowReport_Click);
            // 
            // tsbDataUpload
            // 
            this.tsbDataUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataUpload.Image")));
            this.tsbDataUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataUpload.Name = "tsbDataUpload";
            this.tsbDataUpload.Size = new System.Drawing.Size(110, 39);
            this.tsbDataUpload.Text = "按日期上传";
            this.tsbDataUpload.Click += new System.EventHandler(this.CheckDataUploadTSMI_Click);
            // 
            // tsbDataMng
            // 
            this.tsbDataMng.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataMng.Image")));
            this.tsbDataMng.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataMng.Name = "tsbDataMng";
            this.tsbDataMng.Size = new System.Drawing.Size(62, 39);
            this.tsbDataMng.Text = "维护";
            this.tsbDataMng.Visible = false;
            this.tsbDataMng.Click += new System.EventHandler(this.DataBaseMngTSMI_Click);
            // 
            // tsbDataBackup
            // 
            this.tsbDataBackup.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataBackup.Image")));
            this.tsbDataBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataBackup.Name = "tsbDataBackup";
            this.tsbDataBackup.Size = new System.Drawing.Size(62, 39);
            this.tsbDataBackup.Text = "备份";
            this.tsbDataBackup.Visible = false;
            this.tsbDataBackup.Click += new System.EventHandler(this.DbBackupTSMI_Click);
            // 
            // tsbDataRestore
            // 
            this.tsbDataRestore.Image = ((System.Drawing.Image)(resources.GetObject("tsbDataRestore.Image")));
            this.tsbDataRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDataRestore.Name = "tsbDataRestore";
            this.tsbDataRestore.Size = new System.Drawing.Size(62, 39);
            this.tsbDataRestore.Text = "还原";
            this.tsbDataRestore.Visible = false;
            this.tsbDataRestore.Click += new System.EventHandler(this.DbRestoreTSMI_Click);
            // 
            // tsbOption
            // 
            this.tsbOption.Image = ((System.Drawing.Image)(resources.GetObject("tsbOption.Image")));
            this.tsbOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOption.Name = "tsbOption";
            this.tsbOption.Size = new System.Drawing.Size(62, 39);
            this.tsbOption.Text = "选项";
            this.tsbOption.Click += new System.EventHandler(this.OptionTSMI_Click);
            // 
            // tsbTimer
            // 
            this.tsbTimer.Image = ((System.Drawing.Image)(resources.GetObject("tsbTimer.Image")));
            this.tsbTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTimer.Name = "tsbTimer";
            this.tsbTimer.Size = new System.Drawing.Size(62, 39);
            this.tsbTimer.Text = "计时";
            this.tsbTimer.Click += new System.EventHandler(this.TimerCountTSMI_Click);
            // 
            // tsbRelogin
            // 
            this.tsbRelogin.Image = ((System.Drawing.Image)(resources.GetObject("tsbRelogin.Image")));
            this.tsbRelogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRelogin.Name = "tsbRelogin";
            this.tsbRelogin.Size = new System.Drawing.Size(62, 39);
            this.tsbRelogin.Text = "注销";
            this.tsbRelogin.Click += new System.EventHandler(this.LogoutTSMI_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(62, 39);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.ExitTSMI_Click);
            // 
            // panelMenuToolContainer
            // 
            this.panelMenuToolContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(120)))));
            this.panelMenuToolContainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMenuToolContainer.BackgroundImage")));
            this.panelMenuToolContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelMenuToolContainer.Controls.Add(this.pictureBox1);
            this.panelMenuToolContainer.Controls.Add(this.btnClose);
            this.panelMenuToolContainer.Controls.Add(this.btnMaximizeBox);
            this.panelMenuToolContainer.Controls.Add(this.btnMinimizeBox);
            this.panelMenuToolContainer.Controls.Add(this.lblSystemName);
            this.panelMenuToolContainer.Controls.Add(this.panelMenuToobar);
            this.panelMenuToolContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuToolContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMenuToolContainer.Name = "panelMenuToolContainer";
            this.panelMenuToolContainer.Size = new System.Drawing.Size(804, 101);
            this.panelMenuToolContainer.TabIndex = 9;
            this.panelMenuToolContainer.DoubleClick += new System.EventHandler(this.OnTitleBarDoubleClick);
            this.panelMenuToolContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseDown);
            this.panelMenuToolContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseMove);
            this.panelMenuToolContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.BackgroundImage = global::FoodClient.Properties.Resources.Close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Location = new System.Drawing.Point(780, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 4;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximizeBox
            // 
            this.btnMaximizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizeBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMaximizeBox.BackgroundImage = global::FoodClient.Properties.Resources.MaximizeBox;
            this.btnMaximizeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMaximizeBox.Location = new System.Drawing.Point(760, 8);
            this.btnMaximizeBox.Name = "btnMaximizeBox";
            this.btnMaximizeBox.Size = new System.Drawing.Size(20, 20);
            this.btnMaximizeBox.TabIndex = 3;
            this.btnMaximizeBox.UseVisualStyleBackColor = false;
            this.btnMaximizeBox.Click += new System.EventHandler(this.btnMaximizeBox_Click);
            // 
            // btnMinimizeBox
            // 
            this.btnMinimizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizeBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMinimizeBox.BackgroundImage = global::FoodClient.Properties.Resources.MinimizeBox;
            this.btnMinimizeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMinimizeBox.Location = new System.Drawing.Point(740, 8);
            this.btnMinimizeBox.Name = "btnMinimizeBox";
            this.btnMinimizeBox.Size = new System.Drawing.Size(20, 20);
            this.btnMinimizeBox.TabIndex = 2;
            this.btnMinimizeBox.UseVisualStyleBackColor = false;
            this.btnMinimizeBox.Click += new System.EventHandler(this.btnMinimizeBox_Click);
            // 
            // lblSystemName
            // 
            this.lblSystemName.BackColor = System.Drawing.Color.Transparent;
            this.lblSystemName.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.lblSystemName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSystemName.Location = new System.Drawing.Point(31, 8);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(391, 18);
            this.lblSystemName.TabIndex = 1;
            this.lblSystemName.Text = "SystemName";
            this.lblSystemName.DoubleClick += new System.EventHandler(this.OnTitleBarDoubleClick);
            this.lblSystemName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseDown);
            this.lblSystemName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseMove);
            this.lblSystemName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseUp);
            // 
            // panelMenuToobar
            // 
            this.panelMenuToobar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(223)))), ((int)(((byte)(205)))));
            this.panelMenuToobar.Controls.Add(this.menuStrip1);
            this.panelMenuToobar.Controls.Add(this.panelLeftBorder1);
            this.panelMenuToobar.Controls.Add(this.panelRightBorder1);
            this.panelMenuToobar.Controls.Add(this.toolStrip1);
            this.panelMenuToobar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenuToobar.Location = new System.Drawing.Point(0, 35);
            this.panelMenuToobar.Name = "panelMenuToobar";
            this.panelMenuToobar.Size = new System.Drawing.Size(804, 66);
            this.panelMenuToobar.TabIndex = 0;
            // 
            // panelLeftBorder1
            // 
            this.panelLeftBorder1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelLeftBorder1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftBorder1.Location = new System.Drawing.Point(0, 0);
            this.panelLeftBorder1.Name = "panelLeftBorder1";
            this.panelLeftBorder1.Size = new System.Drawing.Size(5, 66);
            this.panelLeftBorder1.TabIndex = 9;
            // 
            // panelRightBorder1
            // 
            this.panelRightBorder1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelRightBorder1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightBorder1.Location = new System.Drawing.Point(799, 0);
            this.panelRightBorder1.Name = "panelRightBorder1";
            this.panelRightBorder1.Size = new System.Drawing.Size(5, 66);
            this.panelRightBorder1.TabIndex = 8;
            // 
            // panelBottomBorder
            // 
            this.panelBottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelBottomBorder.Controls.Add(this.lblUserName);
            this.panelBottomBorder.Controls.Add(this.lblCompany);
            this.panelBottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomBorder.Location = new System.Drawing.Point(0, 533);
            this.panelBottomBorder.Name = "panelBottomBorder";
            this.panelBottomBorder.Size = new System.Drawing.Size(804, 18);
            this.panelBottomBorder.TabIndex = 11;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserName.Location = new System.Drawing.Point(23, 5);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 12);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "UserName";
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompany.Location = new System.Drawing.Point(557, 3);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(239, 12);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "广东达元绿洲食品安全科技股份有限公司";
            // 
            // panelRightBorder2
            // 
            this.panelRightBorder2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelRightBorder2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightBorder2.Location = new System.Drawing.Point(799, 101);
            this.panelRightBorder2.Name = "panelRightBorder2";
            this.panelRightBorder2.Size = new System.Drawing.Size(5, 432);
            this.panelRightBorder2.TabIndex = 12;
            // 
            // panelLeftBorder2
            // 
            this.panelLeftBorder2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelLeftBorder2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftBorder2.Location = new System.Drawing.Point(0, 101);
            this.panelLeftBorder2.Name = "panelLeftBorder2";
            this.panelLeftBorder2.Size = new System.Drawing.Size(5, 432);
            this.panelLeftBorder2.TabIndex = 13;
            // 
            // FrmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(804, 551);
            this.Controls.Add(this.panelLeftBorder2);
            this.Controls.Add(this.panelRightBorder2);
            this.Controls.Add(this.panelBottomBorder);
            this.Controls.Add(this.panelMenuToolContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "达元食品药品安全检测工作站系统单机版";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMenuToolContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMenuToobar.ResumeLayout(false);
            this.panelMenuToobar.PerformLayout();
            this.panelBottomBorder.ResumeLayout(false);
            this.panelBottomBorder.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            formMain = new FrmMain();

            //2011-06-17获取系统配置信息,其实这里就应该全局配置好所有基本信息,
            CommonOperation.GetSystemInfo();

            if (ShareOption.IsDataLink)//如果是单机版，单机版加密狗
            {
                object obj = System.Configuration.ConfigurationManager.AppSettings["RockeyScanInterval"];
                if (obj != null && obj.ToString() != "")
                {
                    int interval = 1;
                    try
                    {
                        interval = Convert.ToInt32(obj);
                    }
                    catch (InvalidCastException)
                    {
                        interval = 10;
                    }
                    if (interval > 10)//如果外部遇到修改则这里
                        interval = 10;
                    ShareOption.RockeyScanInterval = interval;
                }

            }
            if (!ShareOption.SysAutoLogin)
            {
                //Form frmSplash = new frmSplash();
                Form frmLogin = new frmLogin();
                //frmSplash.ShowDialog();
                DialogResult drst = frmLogin.ShowDialog();
                if (drst == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }
            else
            {
                clsUserInfoOpr opr = new clsUserInfoOpr();
                clsUserInfo user = opr.GetInfo("UserCode='00'");
                CurrentUser.GetInstance().UserInfo = user;
            }
            //重新安装版本，不需要检测数据库版本
            Application.Run(formMain);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            
            CreateMenu(collectTSMI);
            toolStrip1.Renderer = new CustomToolStripRenderer();
            menuStrip1.Renderer = new CustomMenuStripRender();
            int WS_SYSMENU = 0x00080000;
            int windowLong = (GetWindowLong(new HandleRef(this, this.Handle), -16));
            SetWindowLong(new HandleRef(this, this.Handle), -16, windowLong | WS_SYSMENU);
        }
        /// <summary>
        /// 计时器定时扫描加密狗
        /// </summary>
        private System.Timers.Timer aTimer = new System.Timers.Timer();

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void TimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {

        }

        /// <summary>
        /// 验证加密锁
        ///1.判断加密狗有没有存在
        ///2.从加密狗读出系列号
        ///3.对比两个系列号，如果一致正常使用，如果不一致退出
        ///4.定期扫描检测加密狗
        /// </summary>
        /// <returns></returns>
        private static bool VerifyRocky()
        {
            //ET199 et = null;
            //try
            //{
            //    et = new ET199();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return false;
            //}
            //int devCount = et.Enum();//获取加密锁设备

            //if (devCount <= 0)
            //{
            //    MessageBox.Show("没有插入加密狗!");
            //    return false;
            //}
            //et.Open(0);//打开加密锁

            //byte[] byID = (byte[])et.ID;//加密锁系列号
            //string gid = string.Empty;
            //for (int i = 0; i < byID.Length; i++)
            //{
            //    gid += byID[i].ToString("x4") + "-";
            //}
            //gid = gid.TrimEnd('-');

            //et.Close();

            //string licenseFile = AppDomain.CurrentDomain.BaseDirectory + "licenses.licx";
            //if (!File.Exists(licenseFile))
            //{
            //    MessageBox.Show("软件没有授权!");
            //    return false;
            //}
            //else
            //{
            //    StringBuilder sb = new StringBuilder();
            //    using (StreamReader streamRead = new StreamReader(licenseFile, Encoding.Default))
            //    {
            //        sb.Append(streamRead.ReadToEnd());
            //        streamRead.Close();
            //    }
            //    string ret = "test";//DY.Security.SystemEncryption.DESDecrypt(sb.ToString());
            //    ret = ret.Remove(7, 5);
            //    if (!gid.Equals(ret))
            //    {
            //        MessageBox.Show("软件授权不正确!");
            //        return false;
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 根据数据库仪器表创建动态菜单
        /// </summary>
        /// <param name="parent">ToolStripMenuItem根，如第一个菜单根名称为:CollectTSMI</param>
        private void CreateMenu(ToolStripMenuItem parent)
        {
            clsMachineOpr curMachineOpr = new clsMachineOpr();
            DataTable dtbl = curMachineOpr.GetColumnDataTable(0, "IsShow=True ORDER BY OrderId ASC", "MachineName,MachineModel,IsSupport");
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                htbl = new Hashtable();
                string code = string.Empty;
                string text = string.Empty;
                ToolStripMenuItem tsmi = null;
                //自动检测菜单部分
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    code = dtbl.Rows[i]["MachineModel"].ToString().Trim();
                    text = dtbl.Rows[i]["MachineName"].ToString().Trim();
                    htbl.Add(code, text);// 加到hashtale中以备下面使用
                    if (Convert.ToBoolean(dtbl.Rows[i]["IsSupport"]))
                        defaultMethodTag = code;

                    code = "Auto_" + code;//表示自动检测
                    text += "自动检测";
                    tsmi = new ToolStripMenuItem();
                    tsmi.Name = code;
                    tsmi.Text = text;
                    tsmi.Click += new EventHandler(DynamicTSMI_Click);
                    parent.DropDownItems.Add(tsmi);
                }
                //加分隔符
                ToolStripSeparator tStripSeparator1 = new ToolStripSeparator();
                tStripSeparator1.Name = "tStripSeparator1";
                tStripSeparator1.Size = new System.Drawing.Size(217, 6);
                parent.DropDownItems.Add(tStripSeparator1);

                //手工录入菜单部分
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    code = "Hand_" + dtbl.Rows[i]["MachineModel"].ToString().Trim();//表示自检测
                    text = dtbl.Rows[i]["MachineName"].ToString().Trim() + "手工录入";
                    tsmi = new ToolStripMenuItem();
                    tsmi.Name = code;
                    tsmi.Text = text;
                    tsmi.Click += new EventHandler(DynamicTSMI_Click);
                    parent.DropDownItems.Add(tsmi);
                }
                //分隔符
                tStripSeparator1 = new ToolStripSeparator();
                tStripSeparator1.Name = "tStripSeparator2";
                tStripSeparator1.Size = new System.Drawing.Size(217, 6);
                parent.DropDownItems.Add(tStripSeparator1);
            }
            //最后还有一个其他检测的子菜单项
            parent.DropDownItems.Add(otherDataHandTSMI);
        }

        /// <summary>
        /// 动态菜单事件,
        /// 采用反射技术。
        /// 注意:菜单click事件函数只能用public访问权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicTSMI_Click(object sender, EventArgs e)
        {
            // MenuClickMethod menuClickMethod = new MenuClickMethod();
            //创建菜单调用方法类的实例，MenuClickMethod 的方法以子菜单名字命名，预先定义好每个
            //子菜单的操作，采用外部类的方法可以实现更加动态，接近插件形式进行封装.
            //但本次时间过紧，不考虑用外部类和插件形式实现。使用内部公有方法即可实现
            Type type = this.GetType(); //menuClickMethod.GetType();如果用外部类采用注释的代码
            clsCompanyOpr companyBll = new clsCompanyOpr();
            CompanyTable = companyBll.GetAllCompanies();

            try
            {
                //动态获取方法对象
                MethodInfo mi = type.GetMethod(((ToolStripMenuItem)sender).Name);
                mi.Invoke(this, null);  //menuClickMethod  //如果用外部类调用，则用注解代码替换this。指定方法
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            lblSystemName.Text = ShareOption.SystemTitle;
            this.Text = ShareOption.SystemTitle;
            autoUpdateTSMI.Visible = false;//在线更新屏掉
            checkUnitCode = CurrentUser.GetInstance().UserInfo.UnitCode;
            userCode = CurrentUser.GetInstance().UserInfo.UserCode;
            setFormState(); //加载权限设置菜单

            //如果是行政版
            if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))
                taizhanMngTSMI.Visible = false;//台账
            else //企业版
                taizhanMngTSMI.Visible = false;//台账先不开放 先屏蔽
            if (ShareOption.IsDataLink)//单机版
            {
                toolStripSeparator4.Visible = false;//数据重传上面的分隔符
                taizhanMngTSMI.Visible = false;//台账
                tsbDataUpload.Visible = false;
                checkDataUploadTSMI.Visible = false;//检测数据重传
                checkDataUploadAgainTSMI.Visible = false;//检测数据重传维护
                TsSeparatorDownload.Visible = false;//基础数据下载上面的分隔符
                baseDataDownloadTSMI.Visible = false;//基础数据同步
                //启动验证计时器
                aTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
                aTimer.Enabled = true;
                aTimer.Start();
                aTimer.Interval = ShareOption.RockeyScanInterval * 60 * 1000; //每隔N分钟数,最后总的时间单位是毫秒
            }
            else //网络版工作站
            {
                checkUploadTSMI.Text = "查询上传(&C)";
                toolStripSeparator3.Visible = false;
                foodClassMngTSMI.Visible = false;
                checkTypeMngTSMI.Visible = false;
                checkItemMngTSMI.Visible = false;
                beCheckUnitMngTSMI.Text = "被检单位查询";//被检单位维护
                unitTypeMngTSMI.Visible = false;
                checkPointTypeMngTSMI.Visible = false;//检测点类型维护
                orgMngTSMI.Visible = false;
                producePlaceMngTSMI.Visible = false;
                toolStripSeparator5.Visible = false;
                toolStripSeparator6.Visible = false;

                //检测点设置
                checkPointMngTSMI.Visible = false;
                beCheckUnitMngTSMI.Visible = false;
                producerUnitMngTSMI.Visible = false;
                TsSeparatorDownload.Visible = false;
                clsCompanyOpr companyBll = new clsCompanyOpr();
                CompanyTable = companyBll.GetAllCompanies();
            }
            #region @zh 2016/11/20 合肥 
            //将数据库中的系统配置中设置为单机版
            //打开数据上传功能            
            TsSeparatorDownload.Visible = true;
            baseDataDownloadTSMI.Visible = true;
            baseDataDownloadTSMI.Text = "数据交互地址";
            tsbDataUpload.Visible = true;
            producerUnitMngTSMI.Visible = false ;//供应商
            producePlaceMngTSMI.Visible = false;//产地维护
            #endregion

            //配置几个可变标签名称
            CommonOperation.GetTitleSet();
            this.foodClassMngTSMI.Text = ShareOption.SampleTitle + "种类维护";//区分"食品"或者"样品"
            this.producerUnitMngTSMI.Text = ShareOption.ProductionUnitNameTag + "维护";//单位标签"生产单位/供应商"
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.WindowState = FormWindowState.Maximized;
            menuStrip1.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - 14, menuStrip1.Size.Height);
            toolStrip1.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - 14, toolStrip1.Size.Height);
            this.Show();
        }

        /// <summary>
        /// 通过窗口标题判断窗口是否存在,目的是为了只打开一次
        /// </summary>
        /// <param name="mdiFrm">包含多个子窗口的主窗口</param>
        /// <param name="caption">子窗口标题</param>
        /// <returns></returns>
        private static bool IsWinExist(Form mdiFrm, string caption)
        {
            bool isExist = false;
            foreach (Form frm in mdiFrm.MdiChildren)
            {
                if (frm.Text.Equals(caption))
                {
                    isExist = true;
                    frm.Activate();
                    frm.Show();
                    break;
                }
            }
            return isExist;
        }

        /// <summary>
        /// 设置窗体可见与否
        /// 原来是public void SetFormState(),
        /// 只在主窗体作用域,现修改为private. 2011-6-16
        /// </summary>
        private void setFormState()
        {
            lblUserName.Text = "用户：" + CurrentUser.GetInstance().UserInfo.Name.ToString();
            bool isAdmin = CurrentUser.GetInstance().UserInfo.IsAdmin;
            checkUnitCode = CurrentUser.GetInstance().UserInfo.UnitCode;
            userCode = CurrentUser.GetInstance().UserInfo.UserCode;
            checkDataUploadAgainTSMI.Enabled = isAdmin;
            checkPointMngTSMI.Enabled = isAdmin;
            userRoleMngTSMI.Enabled = isAdmin;
            dataBaseMngTSMI.Enabled = isAdmin;
            dbBackupTSMI.Enabled = isAdmin;
            dbRestoreTSMI.Enabled = isAdmin;
            optionTSMI.Enabled = isAdmin;
            tsbDataBackup.Enabled = isAdmin;
            tsbDataMng.Enabled = isAdmin;
            tsbDataRestore.Enabled = isAdmin;
            tsbOption.Enabled = isAdmin;
        }

        /// <summary>
        /// 获取新的系统编码,
        /// 要读取三次数据库存，效率有待改进
        /// </summary>
        /// <param name="isMachine">指示是否为仪器手工法,true为仪器，其它为false</param>
        /// <returns></returns>
        internal string getNewSystemCode(bool isMachine)
        {
            clsResultOpr resultBll = new clsResultOpr();
            string checkUnitStdCode = clsUserUnitOpr.GetNameFromCode("StdCode", checkUnitCode);
            int nLen = 0;
            string temp = ShareOption.FormatStrMachineCode;
            if (!isMachine)
            {
                temp = ShareOption.FormatStandardCode;
            }
            string curCode = CommonOperation.GetPreCode(temp, checkUnitStdCode, userCode, out nLen);
            string err = string.Empty;
            max = resultBll.GetMaxNO(curCode, nLen, out err);
            string syscode = curCode + (max + 1).ToString().PadLeft(nLen, '0');
            return syscode;
        }

        #region 自动检测
        /// <summary>
        /// LD版仪器自动检测
        /// </summary>
        /// <param name="machineTag">代表不同的仪器型号代码</param>
        private void machineAutoLD(string machineTag)
        {
            string caption = htbl[machineTag].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeLD frmAutoLD = new FrmAutoTakeLD(machineTag);//雷杜版使用DY3000的接口
                if (this.MdiChildren.Length <= 0)
                    frmAutoLD.MdiParent = this;
                frmAutoLD.Text = caption;
                frmAutoLD.Show();
                frmAutoLD.Activate();
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// DY系列自动检测仪器
        /// </summary>
        /// <param name="machineTag">代表不同的仪器型号代码，注意MachineModel字段不包含"DY"后缀</param>
        private void machineAutoDYSeries(string machineTag)
        {
            string caption = htbl[machineTag].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDYSeries frmAutoDYSeries = new FrmAutoTakeDYSeries(machineTag);
                if (this.MdiChildren.Length <= 0)
                    frmAutoDYSeries.MdiParent = this;
                frmAutoDYSeries.Text = caption;
                frmAutoDYSeries.Show();
                frmAutoDYSeries.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// LZ4000T|4000
        /// </summary>
        /// <param name="machineTag">代表不同的仪器型号代码，注意MachineModel字段不包含"DY"后缀</param>
        private void machineAutoLZ4000T(string machineTag)
        {
            string caption = htbl[machineTag].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeLZ4000T frmAutoLZ4000T = new FrmAutoTakeLZ4000T(machineTag);
                if (this.MdiChildren.Length <= 0)
                    frmAutoLZ4000T.MdiParent = this;
                frmAutoLZ4000T.Text = caption;
                frmAutoLZ4000T.Show();
                frmAutoLZ4000T.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// DY-5000自动检测
        /// </summary>
        public void Auto_DY5000()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (ShareOption.DY5000Name.Equals("DY5000LD1"))//表示雷度版
            {
                machineAutoLD("DY5000");//machineModel中没有LD后缀
            }
            else //旧版DY-5000自动检测
            {
                string caption = htbl["DY5000"].ToString() + "自动检测";
                if (!IsWinExist(this, caption))
                {
                    FrmAutoTakeDY5000 frmAutoTakeDY5000 = new FrmAutoTakeDY5000("DY5000");
                    if (this.MdiChildren.Length <= 0)
                    {
                        frmAutoTakeDY5000.MdiParent = this;
                    }
                    frmAutoTakeDY5000.Text = caption;
                    frmAutoTakeDY5000.Show();
                    frmAutoTakeDY5000.Activate();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// DY-5500自动检测
        /// </summary>
        public void Auto_DY5500()
        {
            machineAutoLD("DY5500");
        }

        /// <summary>
        /// DY-5600自动检测
        /// </summary>
        public void Auto_DY5600()
        {
            machineAutoLD("DY5600");
        }

        /// <summary>
        /// DY-5800自动检测
        /// </summary>
        public void Auto_DY5800()
        {
            machineAutoLD("DY5800");
        }

        /// <summary>
        /// DY-4300自动检测
        /// </summary>
        public void Auto_DY4300()
        {
            machineAutoLD("DY4300");
        }

        /// <summary>
        /// DY-3000自动检测
        /// </summary>
        public void Auto_DY3000()
        {
            //旧版3000
            if (ShareOption.CurDY3000Tag.Equals("DY3000"))
            {
                machineAutoLD("DY3000");
            }
            else //新版DY3000
            {
                machineAutoDYSeries("DY3000");
            }
        }

        /// <summary>
        /// DY-2000自动检测
        /// </summary>
        public void Auto_DY2000()
        {
            machineAutoDYSeries("DY2000");
        }

        /// <summary>
        /// DY1000自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY1000()
        {
            machineAutoDYSeries("DY1000");
        }

        /// <summary>
        /// DY3900自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY3900()
        {
            machineAutoDYSeries("DY3900");
        }

        /// <summary>
        /// DY1600自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY1600()
        {
            machineAutoDYSeries("DY1600");
        }

        /// <summary>
        /// DY3300自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY3300()
        {
            machineAutoDYSeries("DY3300");
        }

        /// <summary>
        /// LZ4000自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_LZ4000()
        {
            machineAutoLZ4000T("LZ4000");
        }

        /// <summary>
        /// LZ4000T自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_LZ4000T()
        {
            machineAutoLZ4000T("LZ4000T");
        }
       
        /// <summary>
        /// 肉类水分分析仪自动检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DYRSY2()
        {
            string caption = htbl["DYRSY2"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeSFY frmAutoTakeSFY = new FrmAutoTakeSFY("DYRSY2");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAutoTakeSFY.MdiParent = this;
                }

                frmAutoTakeSFY.Text = caption;
                frmAutoTakeSFY.Show();
                frmAutoTakeSFY.Activate();
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// DY6100自动检测，即ATP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DY6100()
        {
            //string caption = htbl["DY6100"].ToString() + "自动检测";
            //if (!IsWinExist(this, caption)) 
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    FrmAutoATP atp = new FrmAutoATP("DY6100");
            //    if (this.MdiChildren.Length <= 0)
            //    {
            //        atp.MdiParent = this;
            //    }

            //    atp.Text = caption;
            //    atp.Show();
            //    atp.Activate();
            //    Cursor.Current = Cursors.Default;
            //}

            string caption = htbl["DY6100"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                if (Global.FindTheHid())
                {
                    Global.getCommunication();
                }
                FrmAutoTakeDY6100 dy6100 = new FrmAutoTakeDY6100("DY6100");
                if (this.MdiChildren.Length <= 0)
                {
                    dy6100.MdiParent = this;
                }

                dy6100.Text = caption;
                dy6100.Show();
                dy6100.Activate();
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// 6200金标卡分析仪自动检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DY6200()
        {

            string caption = htbl["DY6200"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDY6200 frmAutoDY6200 = new FrmAutoTakeDY6200("DY6200");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAutoDY6200.MdiParent = this;
                }

                frmAutoDY6200.Text = caption;
                frmAutoDY6200.Show();
                frmAutoDY6200.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        public void Auto_DY6201()
        {
            string caption = htbl["DY6201"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDY6201 frmAutoDY6201 = new FrmAutoTakeDY6201("DY6201");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAutoDY6201.MdiParent = this;
                }

                frmAutoDY6201.Text = caption;
                frmAutoDY6201.Show();
                frmAutoDY6201.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 新肉类水分分析仪自动检测DY6400
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DY6400()
        {
            string caption = htbl["DY6400"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDY6400 frmAutoDY6400 = new FrmAutoTakeDY6400("DY6400");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAutoDY6400.MdiParent = this;
                }

                frmAutoDY6400.Text = caption;
                frmAutoDY6400.Show();
                frmAutoDY6400.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 全波长723PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DY723PC()
        {
            string caption = htbl["DY723PC"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDY723PC frmAuto = new FrmAutoTakeDY723PC("DY723PC");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAuto.MdiParent = this;
                }

                frmAuto.Text = caption;
                frmAuto.Show();
                frmAuto.Activate();
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// 8120分析仪自动检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Auto_DY8120()
        {
            string caption = htbl["DY8120"].ToString() + "自动检测";
            if (!IsWinExist(this, caption))
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmAutoTakeDY8120 frmAutoDY8120 = new FrmAutoTakeDY8120("DY8120");
                if (this.MdiChildren.Length <= 0)
                {
                    frmAutoDY8120.MdiParent = this;
                }

                frmAutoDY8120.Text = caption;
                frmAutoDY8120.Show();
                frmAutoDY8120.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// DY-6600自动检测
        /// </summary>
        public void Auto_DY6600()
        {
            //旧版3000
            if (ShareOption.CurDY3000Tag.Equals("DY6600"))
            {
                machineAutoLD("DY6600");
            }
            else //新版DY3000
            {
                machineAutoDYSeries("DY6600");
            }
        }


        /// <summary>
        /// 自动检测快捷方式
        /// 利用反射实现动态调用公有方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAutoCheck_Click(object sender, EventArgs e)
        {
            Type type = this.GetType();
            clsCompanyOpr companyBll = new clsCompanyOpr();
            CompanyTable = companyBll.GetAllCompanies();
            try
            {
                //动态获取方法对象
                MethodInfo mi = type.GetMethod("Auto_" + defaultMethodTag);
                mi.Invoke(this, null);  //如果用外部类调用，则用注解代码替换this。指定方法
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("未设置默认仪器，请设置后重新操作");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 手工录入
        /// <summary>
        /// 基他手工录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherDataHandTSMI_Click(object sender, EventArgs e)
        {
            clsCompanyOpr companyBll = new clsCompanyOpr();
            CompanyTable = companyBll.GetAllCompanies();
            machineHand(string.Empty);
        }

        /// <summary>
        /// 手工录入快捷方式
        /// 利用反射实现动态调用公有方法
        /// </summary>
        /// <param name="sender">不同仪器代码编号，跟数据库对应</param>
        /// <param name="e"></param>
        private void tsbHandCheck_Click(object sender, EventArgs e)
        {
            machineHand(string.Empty);
        }
        private string checkItemCode = string.Empty;
        /// <summary>
        /// 手动录入公共函数
        /// </summary>
        /// <param name="typeCode"></param>
        private void machineHand(string typeCode)
        {
            #region
            //string protocol = clsMachineOpr.GetNameFromCode("Protocol", typeCode);

            //string linkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", typeCode);


            //if (protocol.Equals("RS232DY3000DY"))//DY系列
            //{
            //    checkItems = StringUtil.GetDY3000DYAry(linkStdCode);
            //}
            //else
            //{
            //    checkItems = StringUtil.GetAry(linkStdCode);
            //}

            //int len = checkItems.GetLength(0);
            //string strWhere = string.Empty;
            //string sql = string.Empty;
            //bool blExist = false;
            //if (len >= 1)
            //{
            //    for (int i = 0; i < len; i++)
            //    {
            //        if (checkItems[i, 1].ToString() != "-1")
            //        {
            //            sql = sql + "'" + checkItems[i, 1].ToString() + "',";
            //        }
            //        if (checkItems[i, 1].ToString() == checkItemCode)
            //        {
            //            blExist = true;
            //        }
            //    }
            //    if (!blExist)
            //    {
            //        checkItemCode = string.Empty;
            //    }
            //    if (sql.Length > 0)
            //    {
            //        sql = sql.Substring(0, sql.Length - 1);
            //        strWhere = "IsLock=false AND SysCode IN(" + sql + ")";

            //        clsCheckItemOpr checkItemBll = new clsCheckItemOpr();
            //        DataTable dtCheckItem = checkItemBll.GetAsDataTable(strWhere, "ItemDes", 1);

            //        if (dtCheckItem.Rows.Count <= 0)
            //        {
            //            MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
            //        }
            //        if (dtCheckItem.Rows.Count >= 1)
            //        {
            //            clsResult model = GetResultModel(typeCode, true);
            //            FrmHandTakeJD frm = new FrmHandTakeJD();
            //            frm.cmbCheckMachine.Enabled = false;
            //            frm.setValue(model);
            //            frm.ShowDialog(this);
            //        }
            //    }
            //    if (sql.Length <= 0)
            //    {
            //        MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
            //    }
            //}
            //if (len <= 0&&typeCode != "")
            //{
            //    MessageBox.Show("没有设置仪器所对应的检测项目，请到选项中设置！");
            //}
            //if (len <= 0 && typeCode == "")
            //{
            #endregion
            clsResult model = GetResultModel(typeCode, true);
            FrmHandTakeJD frm = new FrmHandTakeJD();
            frm.cmbCheckMachine.Enabled = false;
            frm.setValue(model);
            frm.ShowDialog(this);
            //}
        }

        /// <summary>
        /// DY5000手工录入
        /// </summary>
        public void Hand_DY5000()
        {
            machineHand("DY5000");
        }
        /// <summary>
        /// DY5500手工录入
        /// </summary>
        public void Hand_DY5500()
        {
            machineHand("DY5500");
        }
        /// <summary>
        /// DY5600手工录入
        /// </summary>
        public void Hand_DY5600()
        {
            machineHand("DY5600");
        }
        /// <summary>
        /// DY5800手工录入
        /// </summary>
        public void Hand_DY5800()
        {
            machineHand("DY5800");
        }
        /// <summary>
        /// DY4300手工录入
        /// </summary>
        public void Hand_DY4300()
        {
            machineHand("DY4300");
        }
        /// <summary>
        /// DY1000手工录入
        /// </summary>
        public void Hand_DY1000()
        {
            machineHand("DY1000");
        }

        /// <summary>
        /// DY-2330自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY2330()
        {
            machineAutoDYSeries("DY2330");
        }

        /// <summary>
        /// DY-2330手工录入
        /// </summary>
        public void Hand_DY2330()
        {
            machineHand("DY2330");
        }

        /// <summary>
        /// DY-2340自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY2340()
        {
            machineAutoDYSeries("DY2340");
        }

        /// <summary>
        /// DY-2340手工录入
        /// </summary>
        public void Hand_DY2340()
        {
            machineHand("DY2340");
        }

        /// <summary>
        /// DY-2360自动检测
        ///用反射只能开放用public权限 
        /// </summary>
        public void Auto_DY2360()
        {
            machineAutoDYSeries("DY2360");
        }

        /// <summary>
        /// DY-2360手工录入
        /// </summary>
        public void Hand_DY2360()
        {
            machineHand("DY2360");
        }

        /// <summary>
        /// DY2000手工录入
        /// </summary>
        public void Hand_DY2000()
        {
            machineHand("DY2000");
        }

        /// <summary>
        /// DY3900手工录入
        /// </summary>
        public void Hand_DY3900()
        {
            machineHand("DY3900");
        }

        /// <summary>
        /// DY3300手工录入
        /// </summary>
        public void Hand_DY3300()
        {
            machineHand("DY3300");
        }

        /// <summary>
        /// DY3000手工录入
        /// </summary>
        public void Hand_DY3000()
        {
            machineHand("DY3000");
        }

        /// <summary>
        /// LZ4000手工录入
        /// </summary>
        public void Hand_LZ4000()
        {
            machineHand("LZ4000");
        }

        /// <summary>
        /// LZ4000T手工录入
        /// </summary>
        public void Hand_LZ4000T()
        {
            machineHand("LZ4000T");
        }

        /// <summary>
        /// 肉类水份仪检测仪器手动检测
        /// </summary>
        public void Hand_DYRSY2()
        {
            machineHand("DYRSY2");
        }

        /// <summary>
        /// 新肉类水份仪检测仪器手动检测DY-6400
        /// </summary>
        public void Hand_DY6400()
        {
            machineHand("DY6400");
        }
        /// <summary>
        /// 6200干式试纸分析仪
        /// </summary>
        public void Hand_DY6200()
        {
            machineHand("DY6200");
        }

        public void Hand_DY6201()
        {
            machineHand("022");
        }

        /// <summary>
        /// ATP荧光检测仪
        /// </summary>
        public void Hand_DY6100()
        {
            machineHand("DY6100");
        }

        public void Hand_DY6600()
        {
            machineHand("DY6600");
        }

        public void Hand_DY1600()
        {
            machineHand("DY1600");
        }

        /// <summary>
        /// 8120测仪
        /// </summary>
        public void Hand_DY8120()
        {
            machineHand("DY8120");
        }

        /// <summary>
        /// 全波长723pC测仪
        /// </summary>
        public void Hand_DY723PC()
        {
            machineHand("DY723PC");
        }
        #endregion

        /// <summary>
        /// 获取Result model实例，公共方法。
        /// 改进原来重复写多次代码的问题 
        /// 2011-06-11 修改
        /// </summary>
        /// <param name="machinCode">标识不同仪器代码</param>
        /// <param name="isStandad">是否为标准代码</param>
        /// <returns></returns>
        private clsResult GetResultModel(string machinCode, bool isStandad)
        {
            string syscode = getNewSystemCode(false);
            clsResult model = new clsResult();
            if (isStandad)//两种方式
            {
                if (ShareOption.SysStdCodeSame)
                    model.CheckNo = CommonOperation.GetCodeString(ShareOption.FormatStandardCode, max, clsUserUnitOpr.GetNameFromCode("StdCode", checkUnitCode), userCode);
                else
                    model.CheckNo = string.Empty;
            }
            else
            {
                if (ShareOption.SysStdCodeSame)
                    model.CheckNo = syscode;
                else
                    model.CheckNo = string.Empty;
            }
            model.StdCode = string.Empty;
            model.SampleCode = string.Empty;
            model.CheckedCompany = string.Empty;
            model.CheckPlaceCode = string.Empty;
            model.FoodCode = string.Empty;
            model.ProduceCompany = string.Empty;
            model.ProducePlace = string.Empty;
            model.SentCompany = string.Empty;
            model.Provider = string.Empty;
            model.TakeDate = DateTime.Today;
            model.CheckStartDate = DateTime.Today;
            model.ImportNum = string.Empty;
            model.SaveNum = string.Empty;
            model.Unit = ShareOption.SysUnit;
            model.SampleBaseNum = string.Empty;
            model.SampleNum = string.Empty;
            model.SampleUnit = ShareOption.SysUnit;
            model.SampleLevel = string.Empty;
            model.SampleModel = string.Empty;
            model.SampleState = string.Empty;
            model.Standard = string.Empty;
            model.CheckMachine = machinCode;
            model.CheckTotalItem = string.Empty;
            model.CheckValueInfo = string.Empty;
            model.StandValue = string.Empty;
            model.Result = string.Empty;
            model.ResultInfo = string.Empty;
            model.UpperCompany = string.Empty;
            model.OrCheckNo = string.Empty;
            model.CheckType = "抽检";
            model.CheckUnitCode = checkUnitCode;
            model.Checker = userCode;
            model.Assessor = string.Empty;
            model.Organizer = string.Empty;
            model.Remark = string.Empty;
            model.CheckPlanCode = string.Empty;
            model.SaleNum = string.Empty;
            model.Price = string.Empty;
            model.CheckederVal = string.Empty;
            model.IsSentCheck = string.Empty;
            model.CheckederRemark = string.Empty;
            model.Notes = string.Empty;
            return model;
        }

        #region 检测数据查询
        /// <summary>
        /// 标准速测法检测数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void standarQueryTSMI_Click(object sender, EventArgs e)
        {
            string caption = standarQueryTSMI.Text;// "标准速测法检测数据查询";
            if (!IsWinExist(this, caption))
            {
                frmPesticideMeasure frmQuery = new frmPesticideMeasure(1);
                frmQuery.MdiParent = this;
                frmQuery.Text = caption;
                frmQuery.Show();
                frmQuery.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                frmQuery.WindowState = FormWindowState.Maximized;
            }
            //frmPesticideMeasure frmQuery = null;
            //foreach (Form ftemp in MdiChildren) //查找当前父表单所有子表单
            //{
            //    if (ftemp is frmPesticideMeasure)
            //    {
            //        frmQuery = (frmPesticideMeasure)ftemp;
            //        break;
            //    }
            //}

            //if (frmQuery == null || frmQuery.IsDisposed)
            //{
            //    frmQuery = new frmPesticideMeasure(1);
            //    frmQuery.MdiParent = this;
            //}
            //frmQuery.Show();
            //frmQuery.Activate();
            //frmQuery.Text = "标准速测法检测数据查询";
        }

        /// <summary>
        /// 其他速测法检测数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void otherQueryTSMI_Click(object sender, EventArgs e)
        {
            string caption = otherQueryTSMI.Text;// "其他速测法检测数据查询";
            if (!IsWinExist(this, caption))
            {
                frmPesticideMeasure frmQuery = new frmPesticideMeasure(2);
                frmQuery.MdiParent = this;
                frmQuery.Text = caption;
                frmQuery.Show();
                frmQuery.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                frmQuery.WindowState = FormWindowState.Maximized;
            }
        }
        /// <summary>
        ///检测数据综合查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDataGeneralQueryTSMI_Click(object sender, EventArgs e)
        {
            string caption = checkDataGeneralQueryTSMI.Text;// "检测数据综合查询";
            if (!IsWinExist(this, caption))
            {
                frmPesticideMeasure frmQuery = new frmPesticideMeasure(3);
                frmQuery.MdiParent = this;
                frmQuery.Text = caption;
                frmQuery.Show();
                frmQuery.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                frmQuery.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 检测数据汇总统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckResultStatisticsTSMI_Click(object sender, EventArgs e)
        {
            string caption = checkResultStatisticsTSMI.Text;// "检测结果汇总统计";
            if (!IsWinExist(this, caption))
            {
                FrmCollectionMng collect = new FrmCollectionMng();
                collect.Text = caption;
                collect.MdiParent = this;
                collect.Show();
            }
        }

        /// <summary>
        /// 检测数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDataUploadTSMI_Click(object sender, EventArgs e)
        {
            string d = FoodClient.AnHui.Global.AnHuiInterface.userName;
            childFormClose();
            frmResultSend send = new frmResultSend();
            send.MdiParent = this;
            send.Text = "检测数据上传";
            send.Show();
        }

        /// <summary>
        /// 检测数据重传维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDataUploadAgainTSMI_Click(object sender, EventArgs e)
        {
            frmReSend reSend = new frmReSend();
            reSend.MdiParent = this;
            reSend.Text = "检测数据重传维护";
            reSend.Show();

            reSend.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            reSend.WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region 窗口菜单相关

        /// <summary>
        /// 水平平铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutHorizontalTSMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// 垂直平铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutVerticalTSMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// 重叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutCascadeTSMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        #endregion

        #region 检测项目维护菜单
        /// <summary>
        /// 检测项目维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckItemMngTSMI_Click(object sender, EventArgs e)
        {
            frmItem formItem = new frmItem();
            formItem.ShowDialog(this);
        }

        /// <summary>
        /// 食品种类维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoodClassMngTSMI_Click(object sender, EventArgs e)
        {
            frmFoodType formFoodType = new frmFoodType();
            formFoodType.ShowDialog(this);
        }

        /// <summary>
        /// 检测类别维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTypeMngTSMI_Click(object sender, EventArgs e)
        {
            frmItemType formItemType = new frmItemType();
            formItemType.ShowDialog(this);
        }

        /// <summary>
        /// 组织机构维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgMngTSMI_Click(object sender, EventArgs e)
        {
            frmMakeArea formMakeArea = new frmMakeArea();
            formMakeArea.ShowDialog(this);
        }

        /// <summary>
        /// 检测点类型维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPointTypeMngTSMI_Click(object sender, EventArgs e)
        {
            frmCheckComType formCheckComType = new frmCheckComType();
            formCheckComType.ShowDialog(this);
        }

        /// <summary>
        /// 检测点设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPointMngTSMI_Click(object sender, EventArgs e)
        {
            frmCheckComNew formCheckCom = new frmCheckComNew();
            formCheckCom.ShowDialog(this);
        }

        /// <summary>
        /// 单位类别维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnitTypeMngTSMI_Click(object sender, EventArgs e)
        {
            frmCompanyKind formStandard = new frmCompanyKind();
            formStandard.ShowDialog(this);
        }

        /// <summary>
        /// 被检单位类别维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeCheckUnitMngTSMI_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            frmCheckedCom formCheckedCom = new frmCheckedCom();
            formCheckedCom.Tag = "CHECKED";
            formCheckedCom.MdiParent = this;
            formCheckedCom.Show();
            formCheckedCom.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            formCheckedCom.WindowState = FormWindowState.Maximized;
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 生产单位维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProducerUnitMngTSMI_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            frmCheckedCom formCheckedCom = new frmCheckedCom();
            formCheckedCom.Tag = "MAKE";
            formCheckedCom.MdiParent = this;
            formCheckedCom.Show();
            formCheckedCom.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            formCheckedCom.WindowState = FormWindowState.Maximized;
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 产品产地维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProducePlaceMngTSMI_Click(object sender, EventArgs e)
        {
            frmProduceArea formProduceArea = new frmProduceArea();
            formProduceArea.ShowDialog(this);
        }

        #endregion

        #region 系统管理菜单
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserRoleMngTSMI_Click(object sender, EventArgs e)
        {
            frmUsersInfo formUsersInfo = new frmUsersInfo();
            formUsersInfo.ShowDialog(this);
            CommonOperation.RunExeCache(3);
        }

        /// <summary>
        /// 数据库维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBaseMngTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                DataBase.CompactAccessDB();
                MessageBox.Show("已经完成数据库压缩与修复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("数据库修复失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 数据库备份
        /// 数据备份时加默认备份文件夹dataBackup
        /// 默认文件名当前日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbBackupTSMI_Click(object sender, EventArgs e)
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
                catch
                {
                    MessageBox.Show("数据库备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("数据库备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 数据库恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbRestoreTSMI_Click(object sender, EventArgs e)
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
        }

        /// <summary>
        /// 选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionTSMI_Click(object sender, EventArgs e)
        {
            frmSysOption formOption = new frmSysOption();
            formOption.ShowDialog(this);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutTSMI_Click(object sender, EventArgs e)
        {
            frmLogin formReLogin = new frmLogin();
            formReLogin.Tag = "RELOGIN";
            DialogResult dlg = formReLogin.ShowDialog(this);

            if (dlg == DialogResult.OK)
            {
                //关闭所有的子窗体
                foreach (Form frm in MdiChildren)
                {
                    frm.Close();
                }
                foreach (Form frm in OwnedForms)
                {
                    frm.Close();
                }

                //重新给予相应的权限
                setFormState();
            }
        }

        /// <summary>
        ///退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitTSMI_Click(object sender, EventArgs e)
        {
            if (formMain.MdiChildren.GetLength(0) > 0)
            {
                DialogResult dlg = MessageBox.Show("有未关闭的窗口是否确认退出？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.No)
                {
                    return;
                }
                else
                {
                    childFormClose();
                }
            }
            else
            {
                DialogResult dlg = MessageBox.Show("确定要退出系统吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.No)
                {
                    return;
                }
            }
            Application.Exit();
        }

        /// <summary>
        /// 关闭子窗口
        /// </summary>
        private void childFormClose()
        {
            foreach (Form frm in MdiChildren)
            {
                frm.Close();
            }
        }

        /// <summary>
        /// 打开系统帮助文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemHelpTSMI_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "help.doc";
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show(this, "无法找到帮助文件！文件全路径为：\r\n" + path, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 计时工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCountTSMI_Click(object sender, EventArgs e)
        {
            frmTimers timers = new frmTimers();
            timers.Show(this);
        }

        /// <summary>
        /// 在线自动更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoUpdateTSMI_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("更新程序需要关闭本系统！是否确认", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.OK)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe";
                string updateArguments = string.Empty;
                string serverIp = ShareOption.SysServerIP;
                string serverId = ShareOption.SysServerID;
                string password = ShareOption.SysServerPass;

                if (string.IsNullOrEmpty(password))
                {
                    updateArguments = " " + serverIp + " " + serverId + " ''";
                }
                else
                {
                    updateArguments = " " + serverIp + " " + serverId + " " + password;
                }
                System.Diagnostics.Process.Start(path, updateArguments);
                Application.Exit();
            }
        }

        /// <summary>
        /// 关于本程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutTSMI_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog(this);
        }

        #endregion

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            if (formMain.MdiChildren.GetLength(0) > 0)
            {
                DialogResult dr = MessageBox.Show("有未关闭的窗口是否确认退出！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (aTimer != null)
                    {
                        aTimer.Stop();
                        aTimer.Enabled = false;
                    }
                    childFormClose();
                    e.Cancel = false;
                }
            }
        }



        /// <summary>
        /// 基础数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseDataDownloadTSMI_Click(object sender, EventArgs e)
        {
            FrmBaseDataDownload frm = new FrmBaseDataDownload();
            frm.ShowDialog(FrmMain.formMain);
        }

        /// <summary>
        /// 进货台帐录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goodsIncomeTSMI_Click(object sender, EventArgs e)
        {
            frmStockRecord formStockRecord = new frmStockRecord();
            formStockRecord.ShowDialog(this);
        }
        /// <summary>
        /// 销售台账录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salesIncomeTSMI_Click(object sender, EventArgs e)
        {
            frmSaleRecord formSaleRecord = new frmSaleRecord();
            formSaleRecord.ShowDialog(this);
        }

        /// <summary>
        /// 子窗口最大化时去掉主窗体的icon图标
        /// 在mdi子窗体最大化的时候。会在父窗体的MenuStrip上添加4个Item.
        /// 分别为Icon,最大化，恢复跟最小化。
        /// 其中除了Icon之外，其他三个的Text属性都赋予了文本值。
        /// 另外，Icon作为MenuStrip的第一项Item，它的索引为0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text == "")
            {
                menuStrip1.Items.RemoveAt(0);
            }
        }

        private bool m_IsMouseDown = false;
        private Point m_FormLocation;     //form的location
        private Point m_MouseOffset;      //鼠标的按下位置 


        private void OnTitleBarDoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void OnTitleBarMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    m_IsMouseDown = true;
                    m_FormLocation = this.Location;
                    m_MouseOffset = Control.MousePosition;
                }
            }
            catch (Exception) { }
        }

        private void OnTitleBarMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                int x = 0;
                int y = 0;
                if (m_IsMouseDown)
                {
                    Point pt = Control.MousePosition;
                    x = m_MouseOffset.X - pt.X;
                    y = m_MouseOffset.Y - pt.Y;
                    this.Location = new Point(m_FormLocation.X - x, m_FormLocation.Y - y);
                }
            }
            catch (Exception) { }
        }

        private void OnTitleBarMouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                m_IsMouseDown = false;
            }
            catch (Exception) { }
        }

        private void btnMinimizeBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizeBox_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ExitTSMI_Click(null, null);
        }

        private void windowTSMI_DropDownOpening(object sender, EventArgs e)
        {
            int itemCount = windowTSMI.DropDownItems.Count;
            if (itemCount == 4)
            {
                windowTSMI.DropDownItems.RemoveAt(3);
            }
        }

        private void toolStripbtnShowReport_Click(object sender, EventArgs e)
        {
            new ReportForm().Show();
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        #region 改变窗体大小
        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;    //左边界
        const int HTRIGHT = 11;   //右边界
        const int HTTOP = 12; //上边界
        const int HTTOPLEFT = 13; //左上角
        const int HTTOPRIGHT = 14;    //右上角
        const int HTBOTTOM = 15;  //下边界
        const int HTBOTTOMLEFT = 0x10;    //左下角
        const int HTBOTTOMRIGHT = 17;
        #endregion

        /// <summary>
        /// 任务接收
        /// 深圳投标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskTSMI_Click(object sender, EventArgs e)
        {
            TaskSZForm window = new TaskSZForm();
            window.ShowDialog();
        }

        /// <summary>
        /// 通知接收
        /// 深圳投标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noticeTSMI_Click(object sender, EventArgs e)
        {
            BaseInfosForm window = new BaseInfosForm();
            window.ShowDialog();
        }

       
       
    }
}
