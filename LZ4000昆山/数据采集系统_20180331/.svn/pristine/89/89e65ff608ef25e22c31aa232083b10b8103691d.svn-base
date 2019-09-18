using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO.Ports;
using System.Threading;

namespace WorkstationDAL.Model
{
    public class Global
    {
        public static string TestItem = string.Empty;  //测试项目
        /// <summary>
        /// 测试仪器
        /// </summary>
        public static string ChkManchine = string.Empty;//测试仪器
        public static bool ComON = false;    //判断COM口是否打开
        #region 记录按钮单击事件,切换主页背景突出显示
        public static bool MainPage=false ;    //主页
        public static bool SearchData = false; //数据采集
        public static bool Dairypb = false;    //操作日记
        public static bool SysSet = false;     //系统设计
        public static bool Shop = false;//商城

        #endregion
        /// <summary>
        /// 设备类型
        /// </summary>
        public static string deviceType = string.Empty;
        /// <summary>
        /// 查询数据起始时间
        /// </summary>
        public static string firsttime = string.Empty;
        /// <summary>
        /// 查询数据结束时间
        /// </summary>
        public static string lasttime = string.Empty;

        /// <summary>
        /// 检测编号
        /// </summary>
        public static string bianhao = string.Empty;
        /// <summary>
        /// 检测项目
        /// </summary>
        public static string Chkxiangmu = string.Empty;
        /// <summary>
        /// 检测时间
        /// </summary>
        public static DateTime ChkTime;
        /// <summary>
        /// 样品名称
        /// </summary>
        public static string ChkSample = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public static string GetSampTime = string.Empty;
        /// <summary>
        ///  采样地点
        /// </summary>
        public static string GetSampPlace = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public static string plannumber = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        public static string Chktestbase = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public static string ChklimitData = string.Empty;
        /// <summary>
        /// 检测人员
        /// </summary>
        public static string ChkPeople = string.Empty;

        //串口设置
        public static string SerialCOM = string.Empty;//串口号
        public static string SerialBaud = string.Empty;//波特率
        public static string SerialData = string.Empty;//数据位
        public static string SerialStop = string.Empty; //停止位
        public static string SerialParity = string.Empty;//校验位
        public static string tagName = string.Empty;
        public static IList<clsCheckData> _checkDatas = null;
        public delegate void InvokeDelegate(DataTable dtbl);
        public static string _strData = string.Empty, _settingType = string.Empty;
        //public static SerialPort  sp = new SerialPort ();
        public static byte btDeviceTime = 0x20, btSN = 0x18, btCheckTime = 0x1A, btProductRead = 0x14, btProductSetting = 0x16,
           btPrint = 0x1C, btWiFi = 0x22, btBluetooth = 0x18, btServer = 0x26, btEthernet = 0x24, getDeviceModel = 0x00, btCheckedUnitRead = 0x40;
        public static bool _IsReadOver = false;
        public static IList<clsProduct> _products = null;
        public static IList<clsCheckedUnit> _checkedUnits = null;
        public static clsProduct _newProduct = null;
        public static clsCheckedUnit _newCheckedUnit = null;
        public static int _selIndex = -1;
        //串口通信
        public static SerialPort port_com = new SerialPort();
        /// <summary>
        /// HID通信的VID
        /// </summary>
        public static string hid_vid = string.Empty;
        /// <summary>
        /// HID通信的PID
        /// </summary>
        public static string hid_pid = string.Empty;
        public static void Link_COM()
        {
            try
            {
                Int32 iBaud = Convert.ToInt32(Global.SerialBaud);
                Int32 iDateBit = Convert.ToInt32(Global.SerialData);

                port_com.PortName = Global.SerialCOM;
                port_com.BaudRate = iBaud;
                port_com.DataBits = iDateBit;

                switch (Global.SerialStop)            //停止位
                {
                    case "1":
                        port_com.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        port_com.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        port_com.StopBits = StopBits.Two;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }

                switch (Global.SerialParity)             //校验位
                {
                    case "无":
                        port_com.Parity = Parity.None;
                        break;
                    case "奇校验":
                        port_com.Parity = Parity.Odd;
                        break;
                    case "偶校验":
                        port_com.Parity = Parity.Even;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }
                if (port_com.IsOpen == true)//如果打开状态，则先关闭一下
                {
                    port_com.Close();
                }
                port_com.Open(); //打开串口
                //if (port_com.IsOpen == true)
                //{
                //    Global.ComON = true;
                //}
                Global.ComON = true;
                //Control.CheckForIllegalCrossThreadCalls = false;
                //port_com.DataReceived += new SerialDataReceivedEventHandler(port_com_DataReceive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }
        public static void port_com_DataReceive(object Send, SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show("返回");
            //这里处理数据接收
            Thread.Sleep(500);//延迟一秒解决偶有漏读的bug
            //此处可能没有必要判断是否打开，但为了严谨性，我还是加上了
            if (port_com.IsOpen)
            {
                //txtReceive.Text += "\r\n" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n";
                //this.txtReceive.AppendText("\r\n" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                byte[] byteRead = new byte[port_com.BytesToRead];    //BytesToRead:sp1接收的字符个数
                //if (rdSendStr.Checked)                          //'发送字符串'单选按钮
                //{
                //    this.txtReceive.AppendText(sp.ReadLine() + "\r\n");
                //    //txtReceive.Text += _port.ReadLine() + "\r\n"; //注意：回车换行必须这样写，单独使用"\r"和"\n"都不会有效果
                //    sp.DiscardInBuffer();                      //清空SerialPort控件的Buffer 
                //}
                //else                                            //'发送16进制按钮'
                //{
                try
                {
                    Byte[] receivedData = new Byte[port_com.BytesToRead];        //创建接收字节数组
                    port_com.Read(receivedData, 0, receivedData.Length);         //读取数据
                    port_com.DiscardInBuffer();                                  //清空SerialPort控件的Buffer
                    string strRcv = string.Empty;
                    for (int i = 0; i < receivedData.Length; i++) //窗体显示
                    {
                        strRcv += strRcv.Length == 0 ? receivedData[i].ToString("X2") :
                            " " + receivedData[i].ToString("X2");
                    }
                    //txtReceive.Text += strRcv.Length == 0 ? strRcv : " " + strRcv;
                    if (_settingType.Length > 0)
                    {
                        _strData += _strData.Length == 0 ? strRcv : " " + strRcv;
                        //try
                        //{
                        //    _strData = _strData.Substring(_strData.IndexOf("7E"));//删除7E前面的所有字节
                        //    _strData = _strData.Substring(0, _strData.LastIndexOf("AA") + 2);//删除最后一个AA后面的所有字节
                        //}
                        //catch (Exception) { }
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
                                    MessageBox.Show("出厂编号设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_BLUETOOTH))
                                {
                                    //this.txtSettingReceive.AppendText(" - 蓝牙设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("蓝牙设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_CHECKTIME))
                                {
                                    //this.txtSettingReceive.AppendText(" - 检测时间设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("检测时间设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_PRINT))
                                {
                                    //this.txtSettingReceive.AppendText(" - 自动打印设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("自动打印设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_WIFI))
                                {
                                    //this.txtSettingReceive.AppendText(" - WiFi设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("WiFi设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_SERVER))
                                {
                                    //this.txtSettingReceive.AppendText(" - 服务器设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("服务器设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_ETHERNET))
                                {
                                    //this.txtSettingReceive.AppendText(" - 以太网设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("以太网设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_DEVICETIME))
                                {
                                    //this.txtSettingReceive.AppendText(" - 仪器时间设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show("仪器时间设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.UPDATE_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 样品名称:[" + txtOldName.Text.Trim() + "]改为[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
                                    if (_newProduct != null && _selIndex >= 0) _products[_selIndex] = _newProduct;
                                    MessageBox.Show("样品名称修改成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.ADD_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 新增样品:[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_products.Count + 1) + "]\r\n");
                                    if (_newProduct != null) _products.Add(_newProduct);
                                    MessageBox.Show("新增样品成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.DELETED_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //   DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 删除样品:[" + txtOldName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
                                    if (_newProduct != null && _selIndex >= 0) _products[_selIndex].name = string.Empty;
                                    MessageBox.Show("删除样品成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.UPDATE_CHECKEDUNIT))
                                {
                                    if (_newCheckedUnit != null && _selIndex >= 0) _checkedUnits[_selIndex] = _newCheckedUnit;
                                    _IsReadOver = true;
                                    MessageBox.Show("被检单位名称修改成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.GET_DEVICEMODEL))
                                {
                                    // title = ((Tool.DEVICEMODEL == clsTool.LZ4000T) ? "LZ4000(T)" : "LZ4000") + " 维护工具" + Ver;
                                    //btn_CheckedUnit.Visible = clsTool.DEVICEMODEL == Tool.LZ4000 ? true : false;
                                    //this.Text = title;
                                    //this.lb_home.Text = "欢迎使用" + title;
                                }
                                _settingType = string.Empty;
                                #endregion
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    _IsReadOver = true;

                    MessageBox.Show(ex.Message, "出错提示");
                }
            }

        }
        public static void Loadport()
        {
            port_com.BaudRate = Convert.ToInt32("112500");
            port_com.DataBits = Convert.ToInt32("8");
            port_com.Parity = Parity.None;
            port_com.StopBits = StopBits.One;
            port_com.PortName = "COM3";
            Control.CheckForIllegalCrossThreadCalls = false;
            //SerialPort sp = new SerialPort();
            //sp.DataReceived += new SerialDataReceivedEventArgs(port_com_DataReceive);
            port_com.DataReceived += new SerialDataReceivedEventHandler(port_com_DataReceive);
            //sp.DataReceived += new SerialDataReceivedEventHandler(port_com_DataReceive);
            //检测端口
            string[] str = SerialPort.GetPortNames();
            if (str == null || str.Length == 0)
            {
                MessageBox.Show("本机没有端口！\r\n\r\n提示：\r\n1、请检查UBS连接线是否插好。\r\n2、请确保驱动程序已正确安装。", "系统提示");
            }
            port_com.ReceivedBytesThreshold = 1;
            port_com.ReadTimeout = 1500;
            port_com.Close();

        }
        /// <summary>
        /// 采样时间
        /// </summary>
        public static string getsampletime = string.Empty;
        /// <summary>
        /// 采样地点
        /// </summary>
        public static string getsampleaddress = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public static string plannum = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        public static string testbase = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public static string limitdata = string.Empty;
        /// <summary>
        /// 检测员
        /// </summary>
        public static string tester = string.Empty;
        /// <summary>
        /// 审查员
        /// </summary>
        public static string retester = string.Empty;
        /// <summary>
        /// 主管
        /// </summary>
        public static string Manage = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
        public static string CheckedUnit = string.Empty;
        /// <summary>
        /// 读取数据后表的记录数
        /// </summary>
        public static int TableRowNum = 0;
        /// <summary>
        /// 新添加的数据
        /// </summary>
        public static string[,] AddItem = null;
        /// <summary>
        /// 保存时编辑数据
        /// </summary>
        public static string[,] EditorSave = null;
        /// <summary>
        /// 测试仪器
        /// </summary>
        public static string TestInstrument = "LZ-2000农药残留快检";
        /// <summary>
        /// 窗体最大化
        /// </summary>
        public static bool maxwindow = false;
        /// <summary>
        /// 当前窗体
        /// </summary>
        public static string currentform = string.Empty;
        /// <summary>
        ///判断是修改哪里的数据
        /// </summary>
        public static string newdata = string.Empty;
        /// <summary>
        /// 当前登录用户名称
        /// </summary>
        public static string userlog = string.Empty;

    }
}
