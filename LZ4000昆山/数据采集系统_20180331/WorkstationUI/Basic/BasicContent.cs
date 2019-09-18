using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using WorkstationDAL;
using WorkstationDAL.Model;
using WorkstationModel.Model;
using WorkstationUI;
using System.Threading;
using WorkstationModel.Instrument;

namespace WorkstationUI.Basic
{
    public partial class BasicContent : UserControl
    {
        //clsLZ4000T curLZ4000T = new clsLZ4000T();
        public string BasicCttitle = string.Empty;
        private static BasicContent instance;
        private string tagName = string.Empty;
        private delegate void InvokeDelegate(DataTable dtbl);
        private string _strData = string.Empty, _settingType = string.Empty;
        private IList<clsCheckData> _checkDatas = null;
        private  SerialPort  sp = new SerialPort ();
        private byte btDeviceTime = 0x20, btSN = 0x18, btCheckTime = 0x1A, btProductRead = 0x14, btProductSetting = 0x16,
           btPrint = 0x1C, btWiFi = 0x22, btBluetooth = 0x18, btServer = 0x26, btEthernet = 0x24, getDeviceModel = 0x00, btCheckedUnitRead = 0x40;
        private bool _IsReadOver = false;
        private IList<clsProduct> _products = null;
        private IList<clsCheckedUnit> _checkedUnits = null;
        private clsProduct _newProduct = null;
        private clsCheckedUnit _newCheckedUnit = null;
        private int _selIndex = -1;
        private System.Windows.Forms.Timer _timer = null;
        private int _timerCount = 0;

        /// <summary>
        /// 返回一个该控件的实例。如果之前该控件已经被创建，直接返回已创建的控件。
        /// 此处采用单键模式对控件实例进行缓存，避免因界面切换重复创建和销毁对象。
        /// </summary>
        public static BasicContent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BasicContent();
                }
                return instance;
            }
        }
        public BasicContent()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            //clsMessageNotification.GetInstance().DataRead += NotificationEventHandler;
        }

        private void BasicContent_Load(object sender, EventArgs e)
        {
            //label4.Text = BasicCttitle;
            //this.LbTitle.Text = BasicCttitle;
            this.DTPStart.Text = this.DTPEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
           
        }
     

        //继承虚拟按钮事件
        protected virtual void BtnReadHis_Click(object sender, EventArgs e)
        {
            
        }
       
        /// <summary>
        /// 检测数据列表清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BtnClear_Click(object sender, EventArgs e)
        {
            
                
        }
     
      

        protected virtual void btnadd_Click(object sender, EventArgs e)
        {
            
        }

        protected virtual void btnDatsave_Click(object sender, EventArgs e)
        {

        }
        //关闭窗体
        protected virtual void winClose()
        {
            this.Dispose();
        }
        /// <summary>
        /// datagridview的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        protected virtual void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

        protected virtual void CheckDatas_CurrentCellChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 滑动滚动条隐藏控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {

        }

        protected virtual void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        /// <summary>
        /// 链接设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnlinkcom_Click(object sender, EventArgs e)
        {

        }

        protected virtual void CheckDatas_KeyUp(object sender, KeyEventArgs e)
        {

        }
        
        protected virtual void btnRefresh_Click(object sender, EventArgs e)
        {
           
        }

      

    }
}
