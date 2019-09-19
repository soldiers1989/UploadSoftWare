using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using System.Threading;
using WorkstationModel.Model;

namespace WorkstationModel.Instrument
{
    public class clsTYTTNJZ24
    {
        public static SerialPort comn = new SerialPort();
        public DataTable dt = null;
        private byte[] b2 = new byte[4];//定义byte型数据，用于byte-float转换
        public static DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private DataTable cdt = null;
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private string[,] unitInfo = new string[1, 4];
        private string rtn = string.Empty;
        private string rtd = "";
        private string bianhao = "";
        private string checkdate = "";
        private string checkuser = "";
        private string biaozhi = "";
        private string result = "";

        public clsTYTTNJZ24()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("ReadTable");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通道号";
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
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
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
                dataCol.ColumnName = "检测时间";
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

                m_IsCreatedDataTable = true;
            }
            try
            {
                string err = "";
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null)
                {
                    if (cdt.Rows.Count > 0)
                    {
                        for (int n = 0; n < cdt.Rows.Count; n++)
                        {
                            if (cdt.Rows[n][9].ToString() == "是")
                            {
                                unitInfo[0, 0] = cdt.Rows[n][2].ToString();
                                unitInfo[0, 1] = cdt.Rows[n][3].ToString();
                                unitInfo[0, 2] = cdt.Rows[n][8].ToString();
                                unitInfo[0, 3] = cdt.Rows[n][0].ToString();//检测单位
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    //添加事件注册  
                    comn.DataReceived +=comn_DataReceived;
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
                }
                
                comn.Open();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comn_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(60);
                int n = comn.BytesToRead;//
                byte[] buf = new byte[n];//

                comn.Read(buf, 0, n);//读取缓冲数据 
                //comn.DiscardInBuffer();//清掉数据缓冲区数据就读不回来了
                if (Global.SendData == "1BBF")
                {
                    for (int j = 0; j < buf.Length; j++)
                    {
                        result += buf[j].ToString("X2");
                    }
                    if (result == "1B05")
                    {
                        result = "";
                        System.Threading.Thread.Sleep(200);
                        SendData("1B B1");
                        Global.SendData = "1BB1";
                    }
                }
                else if (Global.SendData == "1BB1")
                {
                    for (int j = 0; j < buf.Length; j++)
                    {
                        result += buf[j].ToString("X2");
                    }
                    if (result == "1BC1")
                    {
                        result = "";
                        if (MessageNotification.GetInstance() != null)
                        {
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTYZ24, "1BC1");
                        }
                    }
                }
                else if (Global.SendData == "24")
                {
                    foreach (byte b in buf)
                    {
                        sb.Append(b.ToString("X2") + "");
                    }
                    rtd = sb.ToString();
                    if (rtd.Length == 1002)
                    {
                        AnalyData(rtd);//解析一组数据
                        rtd = "";
                        sb.Clear();//清除字符串构造器的内容  
                        if (MessageNotification.GetInstance() != null)
                        {
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTYZ24, "Z24");
                        }
                    }
                    if (rtd.Length > 3)
                    {
                        if (rtd.Substring(rtd.Length - 4, 4) == "1BFE")
                        {
                            rtd = "";
                            sb.Clear();//清除字符串构造器的内容  
                            if (MessageNotification.GetInstance() != null)
                            {
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTYZ24, "1BFE");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="send"></param>
        /// <returns></returns>
        public bool SendData(string send)
        {
            bool rtn = false;
            try
            {
                byte[] buffer = StrToBytes(send);
                comn.Write(buffer, 0, buffer.Length);
                rtn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtn;
        }
        /// <summary>
        /// 字符串转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] StrToBytes(string data)
        {
            string[] strArray = data.Split(' ');
            byte[] byffer = new byte[strArray.Length];
            int ii = 0;
            for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
            {
                Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);
                int decNum = 0;
                if (strArray[i] == "")
                {
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                }

                try    //防止输错，使其只能输入一个字节的字符
                {
                    byffer[ii] = Convert.ToByte(decNum);
                }
                catch (Exception)
                {

                }
                ii++;
            }
            return byffer;
        }
        /// <summary>
        /// 解析一组数据
        /// </summary>
        /// <param name="rtndata"></param>
        private void AnalyData(string rtndata)
        {
            //bianhao = HexToInt(rtndata.Substring(0, 8));//编号
            checkdate = HexToChar(rtndata.Substring(10, 22));//检测日期
            //checkuser = HexToChar(rtndata.Substring(32, 22));//检测人员
            biaozhi = rtndata.Substring(56, 2);//共2个字节，第一个字节的8位为空测标志位，对应的位上若为1则表示该通道放入样品测量，若为0则表示未放入样品。
            int tem = Convert.ToInt32(biaozhi, 16);
            string er = Convert.ToString(tem, 2);
            int leng = er.Length;
            if (er.Length < 8)//不足8位补零
            {
                for (int j = 0; j < 8 - leng; j++)
                {
                    er = "0" + er;
                }
            }

            for (int i = 0; i < 8; i++)//8个通道的数据
            {
                string sel = er.Substring(7 - i, 1);
                if (sel == "1")//为说明该通道有样品数据
                {
                    string hole = SearchHole(rtndata.Substring(58 + i * 118, 2));//通道
                    //string chkno= rtndata.Substring(60 + i * 118, 4);//仪器上的检测编号
                    string sample = HexToChar(rtndata.Substring(64 + i * 118, 30));//样品名称
                    //正则表达式去除字符串中的数字
                    sample = System.Text.RegularExpressions.Regex.Replace(sample, "[0-9]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    //string source= HexToChar(rtndata.Substring(94 + i * 118, 14));//来源
                    //string beizhu = HexToChar(rtndata.Substring( 108 + i * 118, 42));//来源备注
                    //string xiguang= HexToFloat(rtndata.Substring(150 + i * 118, 8));//吸光度
                    //string dataA=HexToFloat(rtndata.Substring(158 + i * 118, 8));//样品DeTaA	
                    string value = HexToFloat(rtndata.Substring(166 + i * 118, 8));//检测结果值

                    string conclusion = "";// rtndata.Substring(174 + i * 118, 2) == "01" ? "合格" : "超标";//结论
                    if (rtndata.Substring(174 + i * 118, 2) == "01")
                    {
                        conclusion = "合格";
                    }
                    else if (rtndata.Substring(174 + i * 118, 2) == "02")
                    {
                        conclusion = "超标";
                    }
                    else
                    {
                        conclusion = "无效";
                    }

                    AddToTable(sample, "农药残留", conclusion, value, checkdate, hole);
                }
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
            string gg = chs.GetString(zu);
            gg = gg.Replace("\b", "");
            gg = gg.Replace("\0", "");
            return gg;
        }
        /// <summary>
        /// 16进制转10进制
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private string HexToInt(string hex)
        {
            uint a = Convert.ToUInt32(hex, 16);
            return a.ToString();
        }
        /// <summary>
        /// 16进制转浮点型
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private string HexToFloat(string hex)
        {
            //将4字节的字符串格式转换成16进制的byte格式
            b2[3] = Convert.ToByte(hex.Substring(0, 2), 16);
            b2[2] = Convert.ToByte(hex.Substring(2, 2), 16);
            b2[1] = Convert.ToByte(hex.Substring(4, 2), 16);
            b2[0] = Convert.ToByte(hex.Substring(6, 2), 16);

            //将16进制byte转换成浮点数格式
            float f2 = BitConverter.ToSingle(b2, 0) * 100;
            int f3 = (int)f2;//去掉小数
            return f3.ToString();

        }
        //检测通道号
        private string SearchHole(string hole)
        {
            string rtn = "";
            if (hole.Substring(0, 1) == "2")//A组通道
            {
                if (hole == "21")
                {
                    rtn = "A1";
                }
                else if (hole == "22")
                {
                    rtn = "A2";
                }
                else if (hole == "23")
                {
                    rtn = "A3";
                }
                else if (hole == "24")
                {
                    rtn = "A4";
                }
                else if (hole == "25")
                {
                    rtn = "A5";
                }
                else if (hole == "26")
                {
                    rtn = "A6";
                }
                else if (hole == "27")
                {
                    rtn = "A7";
                }
                else if (hole == "28")
                {
                    rtn = "A8";
                }
            }
            else if (hole.Substring(0, 1) == "3")//B组通道
            {
                if (hole == "31")
                {
                    rtn = "B1";
                }
                else if (hole == "32")
                {
                    rtn = "B2";
                }
                else if (hole == "33")
                {
                    rtn = "B3";
                }
                else if (hole == "34")
                {
                    rtn = "B4";
                }
                else if (hole == "35")
                {
                    rtn = "B5";
                }
                else if (hole == "26")
                {
                    rtn = "A6";
                }
                else if (hole == "37")
                {
                    rtn = "B7";
                }
                else if (hole == "38")
                {
                    rtn = "B8";
                }
            }
            else if (hole.Substring(0, 1) == "4")//C组通道
            {
                if (hole == "41")
                {
                    rtn = "C1";
                }
                else if (hole == "42")
                {
                    rtn = "C2";
                }
                else if (hole == "43")
                {
                    rtn = "C3";
                }
                else if (hole == "44")
                {
                    rtn = "C4";
                }
                else if (hole == "45")
                {
                    rtn = "C5";
                }
                else if (hole == "46")
                {
                    rtn = "C6";
                }
                else if (hole == "47")
                {
                    rtn = "C7";
                }
                else if (hole == "48")
                {
                    rtn = "C8";
                }
            }
            return rtn;
        }
        /// <summary>
        /// 添加数据到表
        /// </summary>
        /// <param name="checkValue"></param>
        /// <param name="time"></param>
        /// <param name="holes"></param>
        private void AddToTable(string sample, string item, string conclusion, string checkValue, string checktime, string holes)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["样品名称"] = sample;
            dr["检测项目"] = "农药残留";
            dr["检测结果"] = checkValue;
            dr["单位"] = "%";//检测值
            dr["检测依据"] = "GB/T 5009.199-2003";
            dr["标准值"] = "50";
            dr["检测仪器"] = Global.ChkManchine;
            dr["结论"] = conclusion;
            //if (Convert.ToDouble(checkValue) < 50)
            //{
            //    dr["结论"] = "合格";
            //}
            //else
            //{
            //    dr["结论"] = "不合格";
            //}
            dr["检测单位"] = unitInfo[0, 3];
            dr["检测数量"] = "";
            dr["通道号"] = holes;
            dr["采样时间"] = System.DateTime.Now.ToString();
            dr["采样地点"] = unitInfo[0, 1];
            dr["被检单位"] = unitInfo[0, 0];
            dr["检测员"] = unitInfo[0, 2];
            dr["检测时间"] = checktime;

            sb.Length = 0;
            sb.AppendFormat(" CheckData='{0}'", checkValue);
            sb.AppendFormat(" AND Checkitem='{0}'", "农药残留");
            sb.AppendFormat(" AND CheckTime='{0}'", checktime);
            dr["已保存"] = (sqlSet.IsExist(sb.ToString()) == true ? "是" : "否");

            DataReadTable.Rows.Add(dr);
        }

    }
}
