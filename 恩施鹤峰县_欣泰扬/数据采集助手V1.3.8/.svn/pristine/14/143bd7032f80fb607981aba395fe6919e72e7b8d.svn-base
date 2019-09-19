using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationModel.Model;


namespace WorkstationModel.Instrument
{
    public class DY7200
    {
        public static SerialPort comn = new SerialPort();
        public DataTable dt = null;
        public DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private DataTable cdt = null;
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private byte[] b2 = new byte[4];//定义byte型数据，用于byte-float转换
        //private string[,] unitInfo = new string[1, 7];
        private string result = "";
        private string RtnTemp = string.Empty;
        private int datalength = 0;
        public int count = 0;
        private ArrayList alist = new ArrayList();
        public int rtndata = 0;
        private StringBuilder strWhere = new StringBuilder();
        public string[,] unitInfo = new string[1, 6];
        private string checkvalue = "";
        private string unit = "";
        private string conclusion = "";
       

         public DY7200()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "任务主题";
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
                dataCol.ColumnName = "检测单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检企业性质";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产日期";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "送检日期";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "处理结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "ID";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }

         /// <summary>
         /// 串口初始化
         /// </summary>
         /// <param name="portname"></param>
         /// <param name="baud"></param>
         /// <returns></returns>
         public string IniSearialport(string portname, string baud)
         {
             try
             {
                 string srtn = string.Empty;
                 //根据当前串口对象，来判断操作  
                 if (comn.IsOpen)
                 {
                     comn.Close();
                 }
                 else
                 {
                     //初始化serialPort对象  
                     comn.NewLine = "/r/n";
                     comn.RtsEnable = true;//根据实际情况决定 
                     comn.ReadBufferSize = 24096;//设置缓存大小
                     //添加事件注册  
                     comn.DataReceived += comn_DataReceived;
                     //关闭点击时，则关闭串口  
                     comn.PortName = portname;
                     comn.BaudRate = int.Parse(baud); //波特率

                     //Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                     Int32 iDateBits = Convert.ToInt32("8");

                     //comn.BaudRate = iBaudRate;       //波特率
                     comn.DataBits = iDateBits;       //数据位
                     switch ("1")            //停止位
                     {
                         case "1":
                             comn.StopBits = StopBits.One;
                             break;
                         case "1.5":
                             comn.StopBits = StopBits.OnePointFive;
                             break;
                         case "2":
                             comn.StopBits = StopBits.Two;
                             break;
                         default:
                             srtn = "Error：参数不正确!";
                             break;
                     }
                     switch ("无")             //校验位
                     {
                         case "无":
                             comn.Parity = Parity.None;
                             break;
                         case "奇校验":
                             comn.Parity = Parity.Odd;
                             break;
                         case "偶校验":
                             comn.Parity = Parity.Even;
                             break;
                         default:
                             srtn = "Error：参数不正确!";
                             break;
                     }
                     if (comn.IsOpen)
                     {
                         comn.Close();
                     }
                     comn.Open();
                 }

             }
             catch (Exception ex)
             {
                 return ex.Message;
             }
             return "OK";
         }
         private string firstdata = "";

         /// <summary>
         /// 发送数据
         /// </summary>
         /// <param name="send"></param>
         /// <returns></returns>
         public bool SendData(byte[] send)
         {
             bool rtn = false;
             try
             {
                 comn.Write(send, 0, send.Length);
                 rtn = true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return rtn;
         }

         /// <summary>
         /// 通信测试
         /// </summary>
         public void communicate()
         {
             string dd = clsStringUtil.CheckDataSum("03FF2000000000000800");
             string de = clsStringUtil.CheckDataSum("4445564943453F3F");
             byte[] dataSent = clsStringUtil.HexString2ByteArray("03FF2000000000000800" + dd + de + "4445564943453F3F");   //start + end是协议中提到的[数据]
             try
             {
                 SendData(dataSent);
             }
             catch (JH.CommBase.CommPortException ex)
             {
                 MessageBox.Show(ex.Message);

             }

         }
        
         /// <summary>
         /// 数据读取
         /// </summary>
         /// <param name="startDate"></param>
         /// <param name="endDate"></param>
         public void ReadHistory(DateTime startDate, DateTime endDate)
         {
             RtnTemp = "";
             if (DataReadTable != null)
             {
                 DataReadTable.Rows.Clear();
             }
             string header = "03FF4300000000000800B7";//标识头
             uint tempStartDate, tempEndDate, resultStartDate, resultEndDate;
             tempStartDate = (uint)(startDate.Year % 100) << 25;
             tempStartDate = tempStartDate | ((uint)(startDate.Month << 21));
             tempStartDate = tempStartDate | ((uint)(startDate.Day << 16));

             resultStartDate = tempStartDate;
             tempEndDate = (uint)(endDate.Year % 100) << 25;
             tempEndDate = tempEndDate | ((uint)(endDate.Month << 21));
             tempEndDate = tempEndDate | ((uint)(endDate.Day << 16));
             tempEndDate = tempEndDate | ((uint)(endDate.Hour << 11));
             tempEndDate = tempEndDate | ((uint)(endDate.Minute << 5));
             tempEndDate = tempEndDate | ((uint)(endDate.Second >> 1));
             if (tempEndDate < resultStartDate)
             {
                 resultEndDate = resultStartDate;
                 resultStartDate = tempEndDate;
             }
             else
             {
                 resultEndDate = tempEndDate;
             }
             resultEndDate = resultEndDate | 0xFFFF;
             resultStartDate = clsStringUtil.ToLittleEndian(resultStartDate);//转化为小端模式
             resultEndDate = clsStringUtil.ToLittleEndian(resultEndDate);
             string start = resultStartDate.ToString("X8");// 8位16进制
             string end = resultEndDate.ToString("X8");
             string checkSum = clsStringUtil.CheckDataSum(start + end);//协议中提到的[数据部分校验和]
             byte[] dataSent = clsStringUtil.HexString2ByteArray(header + checkSum + start + end);   //start + end是协议中提到的[数据]
             try
             {
                 SendData(dataSent);
             }
             catch (JH.CommBase.CommPortException ex)
             {
                 MessageBox.Show(ex.Message);
                 return;
             }
         }
         /// <summary>
         /// 读取记录
         /// </summary>
         public void ReadData()
         { 
            try
            {
                count = 0;

                string header = "03FF4500010000000000B800";
                byte[] dataSent = clsStringUtil.HexString2ByteArray(header);
                     
                SendData(dataSent);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);

            }
             
         }
         /// <summary>
         /// 串口返回数据
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         public void comn_DataReceived(object sender, SerialDataReceivedEventArgs e)
         {
             try
             {
                 //System.Threading.Thread.Sleep(100);
                 
                 int n = comn.BytesToRead;//
                 byte[] buf = new byte[n];//
                 comn.Read(buf, 0, n);//读取缓冲数据 
              
                 for (int j = 0; j < buf.Length; j++)//读取缓存数据
                 {
                     RtnTemp += buf[j].ToString("X2");
                     if (RtnTemp.Length > 40)
                     {
                         string command = RtnTemp.Substring(4, 2);
                         //string length = RtnTemp.Substring(18, 2) + RtnTemp.Substring(16, 2);
                         datalength = Convert.ToInt32(RtnTemp.Substring(18, 2) + RtnTemp.Substring(16, 2), 16);//返回数据长度  字节数
                         //通信测试
                         if (command.Equals("20"))
                         {
                             if (RtnTemp.Length == (12 * 2 + datalength * 2)) //包头部分乘以2、字节数乘以2
                             {
                                 string leng = RtnTemp.Substring(24, 2);
                                 int s = Convert.ToInt32(leng, 16);

                                 string machine = HexToChar(RtnTemp.Substring(26, s * 2));//检测设备

                                 int a = 26 + s * 2;
                                 string ml = RtnTemp.Substring(a, 2);

                                 int s1 = Convert.ToInt32(ml, 16);

                                 string model = HexToChar(RtnTemp.Substring(a + 2, s1 * 2));//型号

                                 int b = a + 2 + s1 * 2;

                                 string serial = RtnTemp.Substring(b, 2);

                                 int bb = Convert.ToInt32(serial, 16);

                                 string Serial = HexToChar(RtnTemp.Substring(b + 2, bb * 2));//系列号

                                 int st = b + 2 + bb * 2;

                                 string time = RtnTemp.Substring(st, 2);
                                 int tl = Convert.ToInt32(time, 16);
                                 string times = RtnTemp.Substring(st + 2, tl * 2);

                                 string nowtime = times.Substring(0, 2);
                                 int ttt = Convert.ToInt32(nowtime, 16);
                                 string timeer = Convert.ToString(ttt, 2);

                                 string nowdate = times.Substring(2, 2);
                                 int tttt = Convert.ToInt32(nowdate, 16);
                                 string dateer = Convert.ToString(tttt, 2);

                                 if (MessageNotification.GetInstance() != null)
                                 {
                                     RtnTemp = "";
                                     MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "tongxin");
                                 }
                             }
                         }
                         else if (command.Equals("43"))//读取设备信息
                         {
                             if (RtnTemp.Length == (12 * 2 + datalength * 2))
                             {
                                 string r = RtnTemp.Substring(RtnTemp.Length - 4, 4);
                                 string counts = r.Substring(2, 2) + r.Substring(0, 2);
                                 rtndata = Convert.ToInt32(counts, 16);//返回有多少条记录

                                 if (MessageNotification.GetInstance() != null)
                                 {
                                     RtnTemp = "";
                                     MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "xinxi");
                                 }
                             }
                         }
                         else if (command.Equals("45"))//读取数据
                         {
                             //int record = 12 * 2 + datalength * 2;//一条记录长度
                             if (RtnTemp.Length == (12 * 2 + datalength * 2))//一条条记录解析，数据长度会变
                             {
                                 //for (int i = 0; i < 1; i++)
                                 //{
                                     //string data = RtnTemp.Substring(30 + i * ((12 * 2 + datalength * 2)), datalength * 2 - 6);
                                     string data = RtnTemp.Substring(30 , datalength * 2 - 6);
                                     alist.Clear();
                                     //int RecordNo = Convert.ToInt32(RtnTemp.Substring(24 + i * record, 2), 16);//记录编号
                                     int start = 0;

                                     //string[] sArray = Regex.Split(data, "0D", RegexOptions.IgnoreCase);
                                     // 00 DD靠近会划分
                                     for (int k = 0; k< data.Length / 2; k++)
                                     {
                                         //string code = data.Substring( 2 * k, 2);
                                         if (data.Substring(2 * k, 2) == "0D")
                                         {
                                             alist.Add(data.Substring(start, k * 2 - start));
                                             start =k * 2 + 2;
                                         }
                                     }
                                     //string title = HexToChar(alist[0].ToString());//标题
                                     string sendUnit = HexToChar(alist[1].ToString());//送检单位
                                     string[] a = sendUnit.Split('：');
                                     if (a.GetLength(0) > 1)
                                         sendUnit = a[1];
                                     
                                     string TestUnit = HexToChar(alist[2].ToString());//检测单位
                                     a = TestUnit.Split('：');
                                     if (a.GetLength(0) > 1)
                                         TestUnit = a[1];
                                    

                                     string sampletype = HexToChar(alist[3].ToString());//样品类型
                                     a = sampletype.Split('：');
                                     if (a.GetLength(0) > 1)
                                         sampletype = a[1];

                                     //string pihao = HexToChar(alist[4].ToString());//批号
                                     //a = pihao.Split('：');
                                     //if (a.GetLength(0) > 1)
                                     //{
                                     //    pihao = a[1];
                                     //}

                                     //string samplecode = HexToChar(alist[5].ToString());//样品编号
                                     //a = samplecode.Split('：');
                                     //if (a.GetLength(0) > 1)
                                     //{
                                     //    samplecode = a[1];
                                     //}

                                     string TestItem = HexToChar(alist[6].ToString());//检测项目
                                     a = TestItem.Split('：');
                                     if (a.GetLength(0) > 1)
                                         TestItem = a[1];
                         

                                     string TestResult = HexToChar(alist[7].ToString());//检测结果
                                     a = TestResult.Split('：');
                                     if (a.GetLength(0) > 1)
                                         TestResult = a[1];
                                  
                                     string[] b = TestResult.Split(' ');
                                    
                                     if (b.GetLength(0) >= 3)//无效时数组长度变小就会报错
                                     {
                                         checkvalue = b[0];
                                         unit = b[1];
                                         conclusion = b[2];
                                     }
                                     else
                                     {
                                         if (b[0].Contains("无效"))
                                         {
                                             conclusion= checkvalue = b[0];
                                             //conclusion = b[0];
                                         }
                                         else
                                         {
                                             if (b.GetLength(0) == 1)//只有一个结论的
                                             {
                                                 conclusion=checkvalue = b[0];
                                                 //conclusion = b[0];
                                             }
                                             else if (b.GetLength(0) > 1)
                                             {
                                                 conclusion=checkvalue = b[0];
                                                 //conclusion = b[0];
                                                 unit = b[1];
                                             }
                                         }
                                     }
                                     string stand = HexToChar(alist[8].ToString());//检测标准
                                     a = stand.Split('：');
                                     if (a.GetLength(0) > 1)
                                         stand = a[1];

                                     string testTime = HexToChar(alist[9].ToString());//检测时间
                                     a = testTime.Split('：');
                                     if (a.GetLength(0) > 1)
                                         testTime = a[1];

                                     AddTable(sampletype, TestItem, checkvalue, unit, conclusion, TestUnit, sendUnit, stand, testTime);
                                     count = count + 1;
                                     Console.WriteLine(count);

                                     if (count % 10==0)
                                     {
                                         if (MessageNotification.GetInstance() != null)
                                         {
                                             RtnTemp = "";
                                             MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "data");
                                         }
                                     } 
                                     else
                                     {
                                         RtnTemp = "";
                                     }
                                 //}
                                 if (count == rtndata)//读取完成发送显示
                                 {
                                 
                                     if (MessageNotification.GetInstance() != null)
                                     {
                                         RtnTemp = "";
                                         MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "end");
                                     }
                                 }
                             }
                         }
                         else if (command.Equals("46"))//清除记录
                         {
                             if (MessageNotification.GetInstance() != null)
                             {
                                 MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "");
                             }
                         }
                         else if (command.Equals("??"))//设置时间
                         {
                             if (MessageNotification.GetInstance() != null)
                             {
                                 MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY7200, "");
                             }
                         }
                     }
                 }               
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }

         }
         /// <summary>
         /// 16进制转字符型
         /// </summary>
         /// <param name="rtndata"></param>
         /// <returns></returns>
         private string HexToChar(string hex)
         {
             byte[] zu = new byte[hex.Length / 2];
             for (int k = 0; k < zu.Length; k++)
             {
                 zu[k] = byte.Parse(hex.Substring(k * 2, 2), System.Globalization.NumberStyles.HexNumber);
             }
             System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");//GB2312编码
             string gg = chs.GetString(zu).Replace("\b", "").Replace("\0", "");
             return gg;
         }
         private void AddTable(string samplename, string item, string checkValue, string unit, string conclusion, string checkunit, string sentunit, string standard, string time)
         {
             if (conclusion == "携带" || conclusion == "阳性" || conclusion == "不合格")
             {
                 conclusion = "不合格";
             }
             else if (conclusion == "不携带" || conclusion == "阴性" || conclusion == "合格")
             {
                 conclusion = "合格";
             }
             DataRow dr = DataReadTable.NewRow();
             strWhere.Length = 0;
             DateTime dt = new DateTime();
             if (time != "")
             {
                 dt = DateTime.Parse(time);
                 strWhere.AppendFormat(" CheckData='{0}' AND Checkitem='{1}' AND CheckTime=#{2}#", checkValue, item, DateTime.Parse(time));
             }

             dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
             dr["样品名称"] = samplename;
             dr["检测项目"] = item;
             dr["检测结果"] = checkValue;
             dr["单位"] = unit;//检测值
             dr["检测依据"] = "";
             dr["标准值"] = standard;
             dr["检测仪器"] = Global.ChkManchine;
             dr["结论"] = conclusion;
             dr["检测单位"] = Global.DetectUnit;
             if (dt != null)
             {
                 dr["采样时间"] = dt.AddHours(-2).ToString();
                 dr["生产日期"] = dt.AddDays(-3).ToString();
                 dr["送检日期"] = dt.AddHours(-2).ToString();
             }

             dr["采样地点"] = unitInfo[0, 3];
             dr["被检单位"] = unitInfo[0, 1];
             dr["检测员"] = Global.NickName;
             dr["检测时间"] = time;
             dr["检测数量"] = "1";
             //dr["样品种类"] = "";
             dr["产地"] = unitInfo[0, 4];
             dr["生产单位"] = unitInfo[0, 5];
             dr["被检企业性质"] = unitInfo[0, 3];
             dr["处理结果"] = "已处理";
             DataReadTable.Rows.Add(dr);
         }
    }
}
