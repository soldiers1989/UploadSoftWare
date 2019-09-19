using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL;
using System.IO.Ports;
using System.Threading;
using WorkstationUI.Basic;
using WorkstationDAL.Basic;
using WorkstationDAL.Model;
using WorkstationBLL.Mode;
using WorkstationModel.Instrument;
using WorkstationUI.function;

namespace WorkstationUI.machine
{
    public partial class UControl_LZ4000 :BasicContent
    {
        public  SerialPort sp = new SerialPort();
        public IList<clsCheckData> _checkDatas = null;
        private clsLZ4000T curLZ4000T = null;
        private string _strData = string.Empty, _settingType = string.Empty;
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
        clsSaveResult resultdata = new clsSaveResult();
        clsSetSqlData sqlSet = new clsSetSqlData();
        clsSetSqlData SQLIN = new clsSetSqlData();
        DataTable ResultTable = null;
        private bool m_IsCreatedDataTable = false;
        private ComboBox cmbAdd = new ComboBox();//其他信息输入

        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        
        public UControl_LZ4000()
        {
            InitializeComponent();          
        }
       
        private void UControlLZ4000_Load(object sender, EventArgs e)
        {
            LbTitle.Text = Global.ChkManchine;           
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
            cmbAdd.Items.Add("请选择...");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("清除");
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

            iniTable();          
            Control.CheckForIllegalCrossThreadCalls = false;
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);             
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
        /// 打开、关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            if (btnlinkcom.Text == "连接设备")
            {
                WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
                try
                {
                    Int32 iBaud = Convert.ToInt32(Global.SerialBaud);
                    Int32 iDateBit = Convert.ToInt32(Global.SerialData);
                    sp.PortName = WorkstationDAL.Model.clsShareOption.ComPort;
                    sp.BaudRate = iBaud;
                    sp.DataBits = iDateBit;

                    switch (Global.SerialStop)            //停止位
                    {
                        case "1":
                            sp.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            sp.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            sp.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "系统提示");
                            break;
                    }

                    switch (Global.SerialParity)             //校验位
                    {
                        case "无":
                            sp.Parity = Parity.None;
                            break;
                        case "奇校验":
                            sp.Parity = Parity.Odd;
                            break;
                        case "偶校验":
                            sp.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "系统提示");
                            break;
                    }
                    if (sp.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        sp.Close();
                    }

                    sp.Open(); //打开串口
                    if (sp.IsOpen == true)
                    {
                        Global.ComON = true;
                        txtlink.Text = "已创建连接";
                        btnlinkcom.Text = "断开设备";
                    }
                    else
                    {
                        txtlink.Text = "未连接";
                    }
                    Global.ComON = true;
                    _settingType = clsTool.GET_DEVICEMODEL;
                    _strData = string.Empty;
                    string data = "00 00";
                    string strBuffer = clsTool.GetBuffer(data, getDeviceModel);
                    byte[] buffer = clsTool.StrToBytes(strBuffer);
                    sp.Write(buffer, 0, buffer.Length);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    txtlink.Text = "未连接";
                    return;
                }
            }
            else if (btnlinkcom.Text == "断开设备")
            {
                if (sp.IsOpen == true)//关闭串口
                {
                    sp.Close();
                    txtlink.Text = "未连接";
                    btnlinkcom.Text = "连接设备";
                }
            }

        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void closecom()
        {
            sp.Close();
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

        //首先加载进获取系统PAI函数的引用：
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();//重写系统API函数获取鼠标双击的有效间隔
        private DateTime dtCmbLastClick = DateTime.MinValue;//存储两次单击的时间间隔
        private  void cmbAdd_MouseClick(object sender, MouseEventArgs e)
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
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "清除")
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
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                ResultTable = new DataTable("checkDtbl");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检定员";
                ResultTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                ResultTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {         
            //这里处理数据接收
            Thread.Sleep(50);//延迟一秒解决偶有漏读的bug
            //此处可能没有必要判断是否打开，但为了严谨性，我还是加上了
            if (sp.IsOpen)
            {
                try
                {
                    Byte[] receivedData = new Byte[sp.BytesToRead];        //创建接收字节数组
                    sp.Read(receivedData, 0, receivedData.Length);         //读取数据
                    sp.DiscardInBuffer();                                  //清空SerialPort控件的Buffer
                    string strRcv = string.Empty;
                    for (int i = 0; i < receivedData.Length; i++) //窗体显示
                    {
                        strRcv += strRcv.Length == 0 ? receivedData[i].ToString("X2") :
                            " " + receivedData[i].ToString("X2");
                    }
                    if (_settingType.Length > 0)
                    {
                        _strData += _strData.Length == 0 ? strRcv : " " + strRcv;
                       
                        //此处根据类型判定是否执行成功
                        if (_strData.Length > 0 && clsTool.CheckData(_strData, _settingType))
                        {
                            if (_settingType.Equals(clsTool.READ_HIS) && clsTool._readDatas.Length > 0)
                            {
                                #region 读取仪器检测数据
                                string input = clsTool._readDatas;
                                string scmd = input.Substring(8, 4);
                                string temp = string.Empty;
                                byte[] data = clsTool.HexString2ByteArray(input);
                                List<byte[]> dataList = new List<byte[]>();
                                int dtIndex = 0;
                                byte[] dt = new byte[42];
                                for (int j = 0; j < data.Length; j++)
                                {
                                    dt[dtIndex] = data[j];
                                    if (dtIndex > 40)
                                    {
                                        dataList.Add(dt);
                                        dt = new byte[42];
                                        dtIndex = -1;
                                    }
                                    dtIndex++;
                                }
                                string item = string.Empty;
                                //样品名称、被检单位
                                string productName = string.Empty, checkedUnit = string.Empty;
                                string unit = string.Empty;
                                string num = string.Empty;
                                //抑制率
                                string ckValue = string.Empty;
                                string datetime = string.Empty;
                                byte[] pname = new byte[6], uname = new byte[16];
                                _checkDatas = new List<clsCheckData>();
                          
                                
                                for (int i = 0; i < dataList.Count; i++)
                                {
                                    datetime = "20" + dataList[i][16].ToString("D2") + "-" + dataList[i][17].ToString("D2") + "-" + dataList[i][18].ToString("D2") + " "
                                        + dataList[i][19].ToString("D2") + ":" + dataList[i][20].ToString("D2") + ":" + dataList[i][21].ToString("D2");
                                    num = (dataList[i][8] + dataList[i][9] * 256).ToString("D4");
                                    item = "农药残留";
                                    for (int k = 0; k < pname.Length; k++)
                                    {
                                        pname[k] = dataList[i][k + 10];
                                    }
                                    productName = Encoding.Default.GetString(pname);
                                    unit = "%";
                                    ckValue = ((double)(dataList[i][22] + dataList[i][23] * 256) / 100).ToString("F2");
                                    for (int k = 0; k < uname.Length; k++)
                                    {
                                        //Encoding.Default.GetBytes(strValue);
                                        if (dataList[i][k + 24] == 0)
                                        {
                                            uname[k] = 0x20;
                                            continue;
                                        }
                                        uname[k] = dataList[i][k + 24];
                                    }
                                    checkedUnit = Encoding.Default.GetString(uname);
                                    clsCheckData cData = new clsCheckData();
                                    cData.num = num;
                                    cData.item = item;
                                    cData.productName = productName;
                                    cData.checkValue = ckValue;
                                    cData.unit = unit;
                                    cData.time = datetime;
                                    cData.checkedUnit = checkedUnit;
                                    //把检定数据加入表格
                                    TableAddData(num, item, productName, ckValue, unit, datetime, checkedUnit);
                                    _checkDatas.Add(cData);
                                }
                                _IsReadOver = true;
                                #endregion
                            }
                            else if (_settingType.Equals(clsTool.READ_PRODUCT))
                            {
                                #region 样品信息读取响应
                                int len = 6;
                                if (clsTool.dataList != null && clsTool.dataList.Count > 0)
                                {
                                    _products = new List<clsProduct>();
                                    foreach (byte[] bts in clsTool.dataList)
                                    {
                                        byte[] name = new byte[len];
                                        Array.ConstrainedCopy(bts, 10, name, 0, len);
                                        clsProduct product = new clsProduct();
                                        product.name = Encoding.Default.GetString(name);
                                        foreach (byte bt in bts)
                                            product.value += product.value == null ? bt.ToString("X2") : " " + bt.ToString("X2");
                                        product.index = bts[8] + bts[9] * 256;
                                        _products.Add(product);
                                    }
                                    _IsReadOver = true;
                                }
                                _settingType = string.Empty;
                                #endregion
                            }
                            else if (_settingType.Equals(clsTool.READ_CHECKEDUNIT))
                            {
                                #region 被检单位信息读取响应
                                int len = 16;
                                if (clsTool.dataList != null && clsTool.dataList.Count > 0)
                                {
                                    _checkedUnits = new List<clsCheckedUnit>();
                                    foreach (byte[] bts in clsTool.dataList)
                                    {
                                        byte[] name = new byte[len];
                                        Array.ConstrainedCopy(bts, 10, name, 0, len);
                                        clsCheckedUnit checkedUnit = new clsCheckedUnit();
                                        checkedUnit.name = Encoding.Default.GetString(name);
                                        foreach (byte bt in bts)
                                            checkedUnit.value += checkedUnit.value == null ? bt.ToString("X2") : " " + bt.ToString("X2");
                                        checkedUnit.index = bts[8] + bts[9] * 256;
                                        _checkedUnits.Add(checkedUnit);
                                    }
                                    _IsReadOver = true;
                                }
                                _settingType = string.Empty;
                                #endregion
                            }
                            else
                            {
                                #region 修改仪器设置
                                if (!_settingType.Equals(clsTool.UPDATE_PRODUCT) && !_settingType.Equals(clsTool.ADD_PRODUCT) && !_settingType.Equals(clsTool.DELETED_PRODUCT))
                                {
                                    //this.txtSettingReceive.AppendText((txtSettingReceive.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //        DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
                                }
                                if (_settingType.Equals(clsTool.SET_SN))
                                {
                                    //this.txtSettingReceive.AppendText(" - 出厂编号设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "出厂编号设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_BLUETOOTH))
                                {
                                    //this.txtSettingReceive.AppendText(" - 蓝牙设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "蓝牙设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_CHECKTIME))
                                {
                                    //this.txtSettingReceive.AppendText(" - 检测时间设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "检测时间设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_PRINT))
                                {
                                   
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "自动打印设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_WIFI))
                                {
                                    //this.txtSettingReceive.AppendText(" - WiFi设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "WiFi设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_SERVER))
                                {
                                    
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "服务器设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_ETHERNET))
                                {
                                   
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "以太网设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_DEVICETIME))
                                {
                                   
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "仪器时间设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.UPDATE_PRODUCT))
                                {
                                    
                                    if (_newProduct != null && _selIndex >= 0) _products[_selIndex] = _newProduct;
                                    MessageBox.Show(this, "样品名称修改成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.ADD_PRODUCT))
                                {
                                    
                                    if (_newProduct != null) _products.Add(_newProduct);
                                    MessageBox.Show(this, "新增样品成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.DELETED_PRODUCT))
                                {
                                   
                                    if (_newProduct != null && _selIndex >= 0) _products[_selIndex].name = string.Empty;
                                    MessageBox.Show(this, "删除样品成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.UPDATE_CHECKEDUNIT))
                                {
                                    if (_newCheckedUnit != null && _selIndex >= 0) _checkedUnits[_selIndex] = _newCheckedUnit;
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "被检单位名称修改成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.GET_DEVICEMODEL))
                                {
                                   
                                }
                                _settingType = string.Empty;
                                #endregion
                            }
                        }
                    }
                    CheckDatas.DataSource = null;
                    //CheckDatas.DataSource = _checkDatas;
                    CheckDatas.DataSource = ResultTable;
                    for (int j = 0; j < CheckDatas.Rows.Count; j++)
                    {
                        CheckDatas.Columns[14].Width = 1800;
                        CheckDatas.Columns[7].Width = 1800;
                    }
                }
                catch (System.Exception ex)
                {
                    _IsReadOver = true;

                    MessageBox.Show(this, ex.Message, "出错提示");
                }
            }
            else
            {
                _IsReadOver = true;
                MessageBox.Show(this, "请先打开端口", "错误提示");
            }
        }
        /// <summary>
        /// 检定结果数据加入表，然后再显示到控件
        /// </summary>
        /// <param name="num"></param>
        /// <param name="item"></param>
        /// <param name="sample"></param>
        /// <param name="ckValue"></param>
        /// <param name="unit"></param>
        /// <param name="ckTime"></param>
        /// <param name="ckUnit"></param>
        private void TableAddData(string num,string item,string sample,string ckValue,string unit,string ckTime,string ckUnit)
        {
            DataRow dt;
            dt = ResultTable.NewRow();
         
            dt["已保存"] = "否";
            dt["样品名称"] = sample;
            dt["检测项目"] = item;
            dt["检测结果"] = ckValue;
            dt["单位"] = unit;//检测值
            dt["检测依据"] = "";
            dt["标准值"] = "";
            dt["检测仪器"] = "";
            dt["结论"] = "";
            dt["采样时间"] = System.DateTime.Now.ToString() ;
            dt["采样地点"] = "";
            dt["被检单位"] = ckUnit;
            dt["检定员"] = "";
            dt["检测时间"] = ckTime;

            ResultTable.Rows.Add(dt );
        }
        /// <summary>
        /// 读取数据按钮
        /// 发送指令给串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
                DateTime dataTime = DTPStart.Value.Date;
                string head = "7E100300", strTime = (dataTime.Year - 2000).ToString("X2") + dataTime.Month.ToString("X2") + dataTime.Day.ToString("X2");
                byte checkSum = clsTool.crc8(clsTool.HexString2ByteArray("100300" + strTime));
                string crc = checkSum.ToString("X2");
                byte[] sendData = clsTool.HexString2ByteArray(head + strTime + crc + "AA");
                _settingType = clsTool.READ_HIS;
                clsTool._readDatas = _strData = string.Empty;
                _checkDatas = new List<clsCheckData>();
                sp.Write(sendData, 0, sendData.Length);
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;           
        }
        //数据保存
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string chk = string.Empty;
            int iok = 0;
            int s = 0;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells[0].Value.ToString() == "False" || CheckDatas.Rows[i].Cells[0].Value.ToString() == "否")
                {
                    resultdata.Save = "是";
                    //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                    resultdata.SampleName = CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim();

                    resultdata.Checkitem = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
                    resultdata.CheckData = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
                    resultdata.Unit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
                    resultdata.Testbase = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
                    resultdata.LimitData = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();//标准值
                    resultdata.Instrument = CheckDatas.Rows[i].Cells[7].Value.ToString().Trim();//检测仪器
                    resultdata.Result = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
                    resultdata.Gettime = CheckDatas.Rows[i].Cells[9].Value.ToString().Trim();//采样时间
                    resultdata.Getplace = CheckDatas.Rows[i].Cells[10].Value.ToString().Trim();
                    resultdata.CheckUnit = CheckDatas.Rows[i].Cells[11].Value.ToString().Trim();
                    resultdata.Tester = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();
                    //chk = CheckDatas.Rows[i].Cells[13].Value.ToString().Replace("-", "/").Trim();
                    resultdata.CheckTime = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();

                    s = sqlSet.ResuInsert(resultdata, out err);
                    if (s == 1)
                    {
                        iok = iok + 1;
                        CheckDatas.Rows[i].Cells[0].Value = "是";
                    }
                }
            }
            MessageBox.Show("共保存" + iok + "条数据", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        protected override void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > 8 && CheckDatas.CurrentCell.RowIndex > -1 && CheckDatas.CurrentCell.ColumnIndex < 14)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 9)//检测单位
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
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
