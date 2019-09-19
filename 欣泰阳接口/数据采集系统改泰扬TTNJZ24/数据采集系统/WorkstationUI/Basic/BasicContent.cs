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
       
        //void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    //MessageBox.Show("返回");
        //    //这里处理数据接收
        //    Thread.Sleep(500);//延迟一秒解决偶有漏读的bug
        //    //此处可能没有必要判断是否打开，但为了严谨性，我还是加上了
        //    if (sp.IsOpen)
        //    {
               
        //        byte[] byteRead = new byte[sp.BytesToRead];    //BytesToRead:sp1接收的字符个数
                
        //        try
        //        {
        //            Byte[] receivedData = new Byte[sp.BytesToRead];        //创建接收字节数组
        //            sp.Read(receivedData, 0, receivedData.Length);         //读取数据
        //            sp.DiscardInBuffer();                                  //清空SerialPort控件的Buffer
        //            string strRcv = string.Empty;
        //            for (int i = 0; i < receivedData.Length; i++) //窗体显示
        //            {
        //                strRcv += strRcv.Length == 0 ? receivedData[i].ToString("X2") :
        //                    " " + receivedData[i].ToString("X2");
        //            }
        //            //txtReceive.Text += strRcv.Length == 0 ? strRcv : " " + strRcv;
        //            if (_settingType.Length > 0)
        //            {
        //                _strData += _strData.Length == 0 ? strRcv : " " + strRcv;
        //                //try
        //                //{
        //                //    _strData = _strData.Substring(_strData.IndexOf("7E"));//删除7E前面的所有字节
        //                //    _strData = _strData.Substring(0, _strData.LastIndexOf("AA") + 2);//删除最后一个AA后面的所有字节
        //                //}
        //                //catch (Exception) { }
        //                //此处根据类型判定是否执行成功
        //                if (_strData.Length > 0 && clsTool.CheckData(_strData, _settingType))
        //                {
        //                    if (_settingType.Equals(clsTool.READ_HIS) && clsTool._readDatas.Length > 0)
        //                    {
        //                        #region 读取仪器检测数据
        //                        string input = clsTool._readDatas;
        //                        string scmd = input.Substring(8, 4);
        //                        string temp = string.Empty;
        //                        byte[] data = clsTool.HexString2ByteArray(input);
        //                        List<byte[]> dataList = new List<byte[]>();
        //                        int dtIndex = 0;
        //                        byte[] dt = new byte[42];
        //                        for (int j = 0; j < data.Length; j++)
        //                        {
        //                            dt[dtIndex] = data[j];
        //                            if (dtIndex > 40)
        //                            {
        //                                dataList.Add(dt);
        //                                dt = new byte[42];
        //                                dtIndex = -1;
        //                            }
        //                            dtIndex++;
        //                        }
        //                        string item = string.Empty;
        //                        //样品名称、被检单位
        //                        string productName = string.Empty, checkedUnit = string.Empty;
        //                        string unit = string.Empty;
        //                        string num = string.Empty;
        //                        //抑制率
        //                        string ckValue = string.Empty;
        //                        string datetime = string.Empty;
        //                        byte[] pname = new byte[6], uname = new byte[16];
        //                        _checkDatas = new List<clsCheckData>();
        //                        for (int i = 0; i < dataList.Count; i++)
        //                        {
        //                            datetime = "20" + dataList[i][16].ToString("D2") + "-" + dataList[i][17].ToString("D2") + "-" + dataList[i][18].ToString("D2") + " "
        //                                + dataList[i][19].ToString("D2") + ":" + dataList[i][20].ToString("D2") + ":" + dataList[i][21].ToString("D2");
        //                            num = (dataList[i][8] + dataList[i][9] * 256).ToString("D4");
        //                            item = "农药残留";
        //                            for (int k = 0; k < pname.Length; k++)
        //                            {
        //                                pname[k] = dataList[i][k + 10];
        //                            }
        //                            productName = Encoding.Default.GetString(pname);
        //                            unit = "%";
        //                            ckValue = ((double)(dataList[i][22] + dataList[i][23] * 256) / 100).ToString("F2");
        //                            for (int k = 0; k < uname.Length; k++)
        //                            {
        //                                //Encoding.Default.GetBytes(strValue);
        //                                if (dataList[i][k + 24] == 0)
        //                                {
        //                                    uname[k] = 0x20;
        //                                    continue;
        //                                }
        //                                uname[k] = dataList[i][k + 24];
        //                            }
        //                            checkedUnit = Encoding.Default.GetString(uname);
        //                            clsCheckData cData = new clsCheckData();
        //                            cData.num = num;
        //                            cData.item = item;
        //                            cData.productName = productName;
        //                            cData.checkValue = ckValue;
        //                            cData.unit = unit;
        //                            cData.time = datetime;
        //                            cData.checkedUnit = checkedUnit;
        //                            _checkDatas.Add(cData);
        //                        }
        //                        _IsReadOver = true;
        //                        #endregion
        //                    }
        //                    else if (_settingType.Equals(clsTool.READ_PRODUCT))
        //                    {
        //                        #region 样品信息读取响应
        //                        int len = 6;
        //                        if (clsTool.dataList != null && clsTool.dataList.Count > 0)
        //                        {
        //                            _products = new List<clsProduct>();
        //                            foreach (byte[] bts in clsTool.dataList)
        //                            {
        //                                byte[] name = new byte[len];
        //                                Array.ConstrainedCopy(bts, 10, name, 0, len);
        //                                clsProduct product = new clsProduct();
        //                                product.name = Encoding.Default.GetString(name);
        //                                foreach (byte bt in bts)
        //                                    product.value += product.value == null ? bt.ToString("X2") : " " + bt.ToString("X2");
        //                                product.index = bts[8] + bts[9] * 256;
        //                                _products.Add(product);
        //                            }
        //                            _IsReadOver = true;
        //                        }
        //                        _settingType = string.Empty;
        //                        #endregion
        //                    }
        //                    else if (_settingType.Equals(clsTool.READ_CHECKEDUNIT))
        //                    {
        //                        #region 被检单位信息读取响应
        //                        int len = 16;
        //                        if (clsTool.dataList != null && clsTool.dataList.Count > 0)
        //                        {
        //                            _checkedUnits = new List<clsCheckedUnit>();
        //                            foreach (byte[] bts in clsTool.dataList)
        //                            {
        //                                byte[] name = new byte[len];
        //                                Array.ConstrainedCopy(bts, 10, name, 0, len);
        //                                clsCheckedUnit checkedUnit = new clsCheckedUnit();
        //                                checkedUnit.name = Encoding.Default.GetString(name);
        //                                foreach (byte bt in bts)
        //                                    checkedUnit.value += checkedUnit.value == null ? bt.ToString("X2") : " " + bt.ToString("X2");
        //                                checkedUnit.index = bts[8] + bts[9] * 256;
        //                                _checkedUnits.Add(checkedUnit);
        //                            }
        //                            _IsReadOver = true;
        //                        }
        //                        _settingType = string.Empty;
        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region 修改仪器设置
        //                        if (!_settingType.Equals(clsTool.UPDATE_PRODUCT) && !_settingType.Equals(clsTool.ADD_PRODUCT) && !_settingType.Equals(clsTool.DELETED_PRODUCT))
        //                        {
        //                            //this.txtSettingReceive.AppendText((txtSettingReceive.Text.Trim().Length > 0 ? "\r\n" : "") +
        //                            //        DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
        //                        }
        //                        if (_settingType.Equals(clsTool.SET_SN))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 出厂编号设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "出厂编号设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_BLUETOOTH))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 蓝牙设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "蓝牙设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_CHECKTIME))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 检测时间设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "检测时间设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_PRINT))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 自动打印设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "自动打印设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_WIFI))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - WiFi设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "WiFi设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_SERVER))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 服务器设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "服务器设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_ETHERNET))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 以太网设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "以太网设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.SET_DEVICETIME))
        //                        {
        //                            //this.txtSettingReceive.AppendText(" - 仪器时间设置成功!");
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "仪器时间设置成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.UPDATE_PRODUCT))
        //                        {
        //                            //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
        //                            //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
        //                            //this.txtProductRead.AppendText(" - 样品名称:[" + txtOldName.Text.Trim() + "]改为[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
        //                            if (_newProduct != null && _selIndex >= 0) _products[_selIndex] = _newProduct;
        //                            MessageBox.Show(this, "样品名称修改成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.ADD_PRODUCT))
        //                        {
        //                            //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
        //                            //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
        //                            //this.txtProductRead.AppendText(" - 新增样品:[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_products.Count + 1) + "]\r\n");
        //                            if (_newProduct != null) _products.Add(_newProduct);
        //                            MessageBox.Show(this, "新增样品成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.DELETED_PRODUCT))
        //                        {
        //                            //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
        //                            //   DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
        //                            //this.txtProductRead.AppendText(" - 删除样品:[" + txtOldName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
        //                            if (_newProduct != null && _selIndex >= 0) _products[_selIndex].name = string.Empty;
        //                            MessageBox.Show(this, "删除样品成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.UPDATE_CHECKEDUNIT))
        //                        {
        //                            if (_newCheckedUnit != null && _selIndex >= 0) _checkedUnits[_selIndex] = _newCheckedUnit;
        //                            _IsReadOver = true;
        //                            MessageBox.Show(this, "被检单位名称修改成功！", "系统提示");
        //                        }
        //                        else if (_settingType.Equals(clsTool.GET_DEVICEMODEL))
        //                        {
        //                            // title = ((Tool.DEVICEMODEL == clsTool.LZ4000T) ? "LZ4000(T)" : "LZ4000") + " 维护工具" + Ver;
        //                            //btn_CheckedUnit.Visible = clsTool.DEVICEMODEL == Tool.LZ4000 ? true : false;
        //                            //this.Text = title;
        //                            //this.lb_home.Text = "欢迎使用" + title;
        //                        }
        //                        _settingType = string.Empty;
        //                        #endregion
        //                    }
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            _IsReadOver = true;

        //            MessageBox.Show(this, ex.Message, "出错提示");
        //        }
        //        finally
        //        {
        //            //CheckDatas.DataSource = null;
        //            CheckDatas.DataSource = _checkDatas;
        //            //settingCkDataGridView();
        //        }
                
        //    }
        //    else
        //    {
        //        _IsReadOver = true;
        //        MessageBox.Show(this, "请先打开端口", "错误提示");
        //    }
        //}
        protected virtual void settingCkDataGridView()
        { 

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
                //CheckDatas.Cols["数据可疑性"].Visible = false;

                if (CheckDatas.Rows.Count > 1)
                {
                    for (int i = 1; i < CheckDatas.Rows.Count; i++)
                    {
                        //可疑数据标红处理
                        //if (c1FlexGrid1.Rows[i]["数据可疑性"].ToString().Equals("是"))
                        //    c1FlexGrid1.SetCellStyle(i, 3, style1);
                        //else
                        //    c1FlexGrid1.SetCellStyle(i, 3, styleNormal);
                        //strWhere.Length = 0;
                        //strWhere.AppendFormat(" MachineSampleNum='{0}'", c1FlexGrid1.Rows[i]["编号"].ToString());
                        //strWhere.AppendFormat(" AND MachineItemName='{0}'", c1FlexGrid1.Rows[i]["检测项目"].ToString());
                        //strWhere.AppendFormat(" AND CheckStartDate=#{0}#", c1FlexGrid1.Rows[i]["检测时间"].ToString());
                        //c1FlexGrid1.Rows[i]["已保存"] = _resultBll.IsExist(strWhere.ToString());
                        //strWhere.Length = 0;
                    }
                    //if (c1FlexGrid1.Cols["已保存"] != null)
                    //{
                    //    c1FlexGrid1.Cols["已保存"].Style.BackColor = Color.LightGray;
                    //    c1FlexGrid1.Cols["已保存"].AllowDragging = false;
                    //}
                }
                //c1FlexGrid1.AutoSizeCols();
            }
           
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
        /// <summary>
        /// 读取串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
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

    }
}
