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
using WorkstationBLL.Mode;

namespace WorkstationUI.Basic
{
    public partial class ShowData : UserControl
    {
        clsSetSqlData sql = new clsSetSqlData();
        private DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        public ShowData()
        {
            iniTablt();
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append("2017-4-19");//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
          
            sb.Append("2017-4-29");
            sb.Append(" 23:59:59#");
            //if (!string.IsNullOrEmpty(cmbResult.Text.Trim()))
            //{
            //    sb.AppendFormat("AND Result='%{0}%'", cmbResult.Text.Trim());
            //}
            //if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()))
            //{
            //    sb.AppendFormat("AND Checkitem='%{0}%'", cmbTestItem.Text.Trim());
            //}
            //if (!string.IsNullOrEmpty(cmbTestUnit.Text.Trim()))
            //{
            //    sb.AppendFormat("AND CheckUnit='%{0}%'", cmbTestUnit.Text.Trim());
            //}
            //if (!string.IsNullOrEmpty(cmbSample.Text.Trim()))
            //{
            //    sb.AppendFormat("AND SampleName='%{0}%'", cmbSample.Text.Trim());
            //}
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    // GridNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Machine");
                    TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                       dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
            }


        }
        clsLZ4000T curLZ4000T = new clsLZ4000T();
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

        private void iniTablt()
        {

            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkResult");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "序列号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);



                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "抑制率";
                DataReadTable.Columns.Add(dataCol);

              
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;

                m_IsCreatedDataTable = true;

            }
        }
        private void TableNewRow(string num, string item, string SampleName, string chkdata, string Unit, string CheckTime, string CheckUnit, string Result, string machine)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();

            dr["序列号"] = num;
            dr["检测项目"] = item;
            dr["样品名称"] = SampleName;
            dr["抑制率"] = chkdata;
            dr["单位"] = Unit;
            dr["结果"] = Result;
            dr["检测时间"] = CheckTime;
            dr["检测仪器"] = machine;
            dr["被检单位"] = CheckUnit;
            DataReadTable.Rows.Add(dr);

        }
        private void BasicContent_Load(object sender, EventArgs e)
        {
            //this.LbTitle.Text = BasicCttitle;
            this.DTPStart.Text = this.DTPEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.DtStart.Text = this.DtEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ////Loadport();
            //Control.CheckForIllegalCrossThreadCalls = false;
            //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //try
            //{
            //    Int32 iBaud = Convert.ToInt32(Global.SerialBaud);
            //    Int32 iDateBit = Convert.ToInt32(Global.SerialData);

            //    sp.PortName = Global.SerialCOM;
            //    sp.BaudRate = iBaud;
            //    sp.DataBits = iDateBit;

            //    switch (Global.SerialStop)            //停止位
            //    {
            //        case "1":
            //            sp.StopBits = StopBits.One;
            //            break;
            //        case "1.5":
            //            sp.StopBits = StopBits.OnePointFive;
            //            break;
            //        case "2":
            //            sp.StopBits = StopBits.Two;
            //            break;
            //        default:
            //            MessageBox.Show("Error：参数不正确!", "系统提示");
            //            break;
            //    }

            //    switch (Global.SerialParity)             //校验位
            //    {
            //        case "无":
            //            sp.Parity = Parity.None;
            //            break;
            //        case "奇校验":
            //            sp.Parity = Parity.Odd;
            //            break;
            //        case "偶校验":
            //            sp.Parity = Parity.Even;
            //            break;
            //        default:
            //            MessageBox.Show("Error：参数不正确!", "系统提示");
            //            break;
            //    }
            //    if (sp.IsOpen == true)//如果打开状态，则先关闭一下
            //    {
            //        sp.Close();
            //    }

            //    sp.Open(); //打开串口
            //    if (sp.IsOpen == true)
            //    {
            //        Global.ComON = true;
            //    }
            //    Global.ComON = true;
            //    _settingType = clsTool.GET_DEVICEMODEL;
            //    _strData = string.Empty;
            //    string data = "00 00";
            //    string strBuffer = clsTool.GetBuffer(data, getDeviceModel);
            //    byte[] buffer = clsTool.StrToBytes(strBuffer);
            //    sp.Write(buffer, 0, buffer.Length);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //    return;
            //}
            

        }
       
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show("返回");
            //这里处理数据接收
            Thread.Sleep(500);//延迟一秒解决偶有漏读的bug
            //此处可能没有必要判断是否打开，但为了严谨性，我还是加上了
            if (sp.IsOpen)
            {
               
                byte[] byteRead = new byte[sp.BytesToRead];    //BytesToRead:sp1接收的字符个数
                
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
                                    //this.txtSettingReceive.AppendText(" - 自动打印设置成功!");
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
                                    //this.txtSettingReceive.AppendText(" - 服务器设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "服务器设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_ETHERNET))
                                {
                                    //this.txtSettingReceive.AppendText(" - 以太网设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "以太网设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.SET_DEVICETIME))
                                {
                                    //this.txtSettingReceive.AppendText(" - 仪器时间设置成功!");
                                    _IsReadOver = true;
                                    MessageBox.Show(this, "仪器时间设置成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.UPDATE_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 样品名称:[" + txtOldName.Text.Trim() + "]改为[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
                                    if (_newProduct != null && _selIndex >= 0) _products[_selIndex] = _newProduct;
                                    MessageBox.Show(this, "样品名称修改成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.ADD_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //    DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 新增样品:[" + txtNewName.Text.Trim() + "] 操作成功! 样品序号[" + (_products.Count + 1) + "]\r\n");
                                    if (_newProduct != null) _products.Add(_newProduct);
                                    MessageBox.Show(this, "新增样品成功！", "系统提示");
                                }
                                else if (_settingType.Equals(clsTool.DELETED_PRODUCT))
                                {
                                    //this.txtProductRead.AppendText((txtProductRead.Text.Trim().Length > 0 ? "\r\n" : "") +
                                    //   DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\r\n");
                                    //this.txtProductRead.AppendText(" - 删除样品:[" + txtOldName.Text.Trim() + "] 操作成功! 样品序号[" + (_selIndex + 1) + "]\r\n");
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

                    MessageBox.Show(this, ex.Message, "出错提示");
                }
                finally
                {
                    //CheckDatas.DataSource = null;
                    CheckDatas.DataSource = _checkDatas;
                    //settingCkDataGridView();
                }
                
            }
            else
            {
                _IsReadOver = true;
                MessageBox.Show(this, "请先打开端口", "错误提示");
            }
        }
        protected virtual void settingCkDataGridView()
        { 

        }

        
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            DataView vie = curLZ4000T.DataReadTable.DefaultView;
            //vie.RowFilter = "检测时间 > #" + dtStart.Value.Date + "# and 检测时间 < #" + dtEnd.Value + "#";
            DataTable dt = vie.ToTable();
            ShowResult(dt, true);
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
            //Cursor = Cursors.Default;
            //btnReadHis.Enabled = true;
            //btnClear.Enabled = true;
        }

        //继承虚拟按钮事件
        protected virtual void BtnReadHis_Click(object sender, EventArgs e)
        {
            //if (Global.ComON == true)
            //{
            //    //switch (Otherclass.Global.TestItem)
            //    //{

            //    //    case "LZ4000农残检测":
            //            DateTime dataTime = DTPStart.Value.Date;

            //            string head = "7E100300", strTime = (dataTime.Year - 2000).ToString("X2") + dataTime.Month.ToString("X2") + dataTime.Day.ToString("X2");
            //            byte checkSum = clsTool.crc8(clsTool.HexString2ByteArray("100300" + strTime));
            //            string crc = checkSum.ToString("X2");
            //            byte[] sendData = clsTool.HexString2ByteArray(head + strTime + crc + "AA");
            //            _settingType = clsTool.READ_HIS;
            //            clsTool._readDatas = _strData = string.Empty;
            //            _checkDatas = new List<App_Code.clsCheckData>();
            //            Formating(_settingType, _settingType);

            //            sp.Write(sendData, 0, sendData.Length);
            //            //_port.Write(sendData, 0, sendData.Length);

            //    //        break;
            //    //    case "LZ400T":
            //    //        break;


            //    //    default:
            //    //        break;
            //    //}


            //}
            //else
            //{
            //    MessageBox.Show("串口未打开");
            //    return;
            //}
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
        /// <summary>
        /// 保存读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            function.frmSetResult Sform = new function.frmSetResult();
            Sform.Show();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void ShowData_Load(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {

        }
    }
}
