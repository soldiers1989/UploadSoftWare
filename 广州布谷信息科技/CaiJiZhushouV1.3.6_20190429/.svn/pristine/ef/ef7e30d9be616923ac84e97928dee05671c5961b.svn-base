
using System;
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
    public  class TL310New
    {
        public bool Isshow = false;
        public  SerialPort mSerial = new SerialPort();
        private bool m_IsCreatedDataTable = false;
        public string readtype = "";
        private string RtnTemp = string.Empty;
        public DataTable DataReadTable = null;
        private string item = "";
        private string testbase = "";
        private string standvalue = "";
        private StringBuilder strWhere = new StringBuilder();
        public int icount = 0;
        public int Ncount = 0;
        private clsSetSqlData sqlSet = new clsSetSqlData();
        public string[,] unitInfo = new string[1, 6];
        public string Versions = "";//版本号
       
        public TL310New()
        {
            if (m_IsCreatedDataTable == false)
            {
                DataReadTable = new DataTable("checkDtbl");
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
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "样品编号";
                //DataReadTable.Columns.Add(dataCol);

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
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "被检单位编号";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "摊位号";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
      

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        /// <returns></returns>
        public  bool Open(string port, int baudrate)
        {
            bool isopen = false;
            try
            {
                string[] ports = SerialPort.GetPortNames();
                if (!ports.Contains<string>(port))
                    throw new Exception("端口：" + port + "不存在");
                mSerial.PortName = port;
                mSerial.BaudRate = baudrate;

                mSerial.Parity = Parity.None;

                mSerial.DataBits = 8;

                mSerial.StopBits = StopBits.One;

                mSerial.ReadBufferSize = 115200000;
                mSerial.WriteBufferSize = 0x20000;
                mSerial.ReadTimeout = 3000;
                mSerial.WriteTimeout = 3000;
                mSerial.Open();
                mSerial.DataReceived +=mSerial_DataReceived;
                isopen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isopen;
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        public void CommunicateTest()
        {
            readtype = "通信测试";
            //Records = 0;
            //strShow = "";
            //GetRD = "";
            Global.SendFirst = true;//判断第一个字节的帧头
            //iscom = false;
            byte checkSum = crc8(clsStringUtil.HexString2ByteArray("010100FF"));
            string crc = checkSum.ToString("X2");
            byte[] dataSent = clsStringUtil.HexString2ByteArray("7E010100FF" + crc + "AA");
            mSerial.Write(dataSent, 0, dataSent.Length);


        }
        /// <summary>
        /// 按日期读取数据
        /// </summary>
        public void ReadTimeData(DateTime DtTest)
        {
            icount = 0;
            Ncount = 0;
            readtype = "日期";
            DataReadTable.Clear();
            mSerial.DiscardInBuffer();//丢弃传输缓冲区数据
            mSerial.DiscardOutBuffer();//每次丢弃接收缓冲区的数据
            string year = DtTest.Year.ToString();
            if (year.Length == 4 || year.Length>3)
            {
                year = year.Substring(2, 2);
            }
            year = string.Format("{0:X2}",Int32.Parse(year) );
            string Month = DtTest.Month .ToString("X2");
            string day = DtTest.Day.ToString("X2");
            string header = "150400FF"+year +Month +day ;
            byte sum = crc8(HexString2ByteArray(header));

            byte[] dataSent = HexString2ByteArray("7E"+header +sum.ToString("X2") +"AA");

            string send = "";
            for (int i = 0; i < dataSent.GetLength(0); i++)
            {
                send = send + dataSent[i].ToString("X2");
            }
            Console.WriteLine("发送：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + send);
            mSerial.Write(dataSent, 0, dataSent.Length);
        }


        /// <summary>
        /// 读取上一次数据
        /// </summary>
        public void sendOnce()
        {
            readtype = "上一次";
            DataReadTable.Clear();
            its = 0;
            Isshow = false;
            mSerial.DiscardInBuffer();//丢弃传输缓冲区数据
            mSerial.DiscardOutBuffer();//每次丢弃接收缓冲区的数据
            icount = 0;
            Ncount = 0;
            string dd = "7E150400FF0000002BAA";
            byte[] dataSent = HexString2ByteArray(dd);

            string send = "";
            for (int i = 0; i < dataSent.GetLength(0); i++)
            {
                send = send + dataSent[i].ToString("X2");
            }
            Console.WriteLine ( "发送：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + send);
            mSerial.Write(dataSent, 0, dataSent.Length);
        }
        /// <summary>
        /// 读取全部数据
        /// </summary>
        public void sendSecond()
        {
            readtype = "全部";
            its = 0;
            Isshow = false;
           
            mSerial.DiscardInBuffer();//丢弃传输缓冲区数据
            mSerial.DiscardOutBuffer();//每次丢弃接收缓冲区的数据
            DataReadTable.Clear();
            icount = 0;
            Ncount = 0;
            string dd = "7E150400FFFFFFFF4DAA";
            byte[] dataSent = HexString2ByteArray(dd);

            string send = "";
            for (int i = 0; i < dataSent.GetLength(0); i++)
            {
                send = send + dataSent[i].ToString("X2");
            }
            Console.WriteLine("发送：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + send);
            mSerial.Write(dataSent, 0, dataSent.Length);
        }

        /// 把十六进制字符串转化字节数组
        /// 修改者：
        /// 修改时间：2011-10-21
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private byte[] HexString2ByteArray(string input)
        {
            string temp = string.Empty;
            int len = input.Length / 2;
            byte[] bt = new byte[len];
            for (int i = 0; i < len; i++)
            {
                temp = input.Substring(i * 2, 2);
                bt[i] = Convert.ToByte(temp, 16);
            }
            return bt;
        }
        private string ReceiveData = "";
        private  int its = 0;
        private  void mSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (mSerial.IsOpen == false)
                {
                    return;
                }
                System.Threading.Thread.Sleep(5);
                //string rtt = "";
                int n = mSerial.BytesToRead;//
                byte[] buf = new byte[n];//
                System.Threading.Thread.Sleep(5);
                mSerial.Read(buf, 0, n);//读取缓冲数据 
                if (readtype=="通信测试")
                {
                    for (int j = 0; j < buf.Length; j++)//读取缓存数据
                    {
                        RtnTemp = RtnTemp + buf[j].ToString("X2");
                        if (RtnTemp.Length > 4)
                        {
                            if (RtnTemp.Substring(0, 4) == "7E02")
                            {
                                if (buf[j].ToString("X2") == "AA")
                                {
                                    Console.WriteLine(RtnTemp);
                                    Versions = RtnTemp.Substring(14,2);
                                    RtnTemp = "";
                                    if (MessageNotification.GetInstance() != null)
                                    {
                                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "TLtongxin");
                                    }
                                }
                            }
                        }  
                    }
                }
                else
                {
                    for (int j = 0; j < buf.Length; j++)//读取缓存数据
                    {
                        RtnTemp = RtnTemp + buf[j].ToString("X2");
                        //rtt = rtt +buf[j].ToString("X2");
                        //Console.Write(buf[j].ToString("X2"));
                        if (RtnTemp.Length > 20)
                        {
                            if (RtnTemp.Substring(0, 4) != "7E16")
                            {
                                RtnTemp = "";//丢弃其他数据
                                continue;
                            }
                            string record = RtnTemp.Substring(12, 2);//低位在前
                            record = record + RtnTemp.Substring(10, 2);//高位在后
                            int TotalRecord = Convert.ToInt16(record, 16);
                            if (RtnTemp.Length == 54 && RtnTemp.Substring(0, 8) == "7E161500")
                            {

                                string RecordNo = RtnTemp.Substring(16, 2);
                                RecordNo = RecordNo + RtnTemp.Substring(14, 2);//当前记录是第几条记录
                                string holenum = Convert.ToInt32(RtnTemp.Substring(18, 2), 16).ToString();//通道号
                                string standard = RtnTemp.Substring(20, 2);//检测标准
                                string y = "20" + Convert.ToInt32(RtnTemp.Substring(22, 2), 16).ToString();
                                int year = Int32.Parse(y);//检测时间年
                                int Month = Convert.ToInt32(RtnTemp.Substring(24, 2), 16);//检测时间月
                                int Day = Convert.ToInt32(RtnTemp.Substring(26, 2), 16);//检测时间日
                                int hour = Convert.ToInt32(RtnTemp.Substring(28, 2), 16);//检测时间时
                                int Muni = Convert.ToInt32(RtnTemp.Substring(30, 2), 16);//检测时间分
                                int seconds = Convert.ToInt32(RtnTemp.Substring(32, 2), 16);//检测时间秒
                                //Console.WriteLine(year + "," + Month + "," + Day + "," + hour + "," + Muni + "," + seconds);
                                //Console.WriteLine(RtnTemp);
                                DateTime ChkTime = new DateTime(year, Month, Day, hour, Muni, seconds);

                                //Console.WriteLine(ChkTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                int checkNum = Convert.ToInt32(RtnTemp.Substring(34, 4), 16);//检测编号
                               
                                //string xiguangdu = RtnTemp.Substring(38 + 54 * i, 4);//吸光度
                                int CheckData = Convert.ToInt32(RtnTemp.Substring(42, 2), 16) + Convert.ToInt32(RtnTemp.Substring(44, 2), 16) * 256;//检测结果 抑制率
                                double dd = CheckData / 10;
                                double ww = CheckData % 10;
                                string checkResult = dd.ToString() + "." + ww.ToString();
                                //double ss = double.Parse(rr);
                                //string checkResult = string.Format("{0:P1}", ss);
                                string conclusion = "";//结论
                                if (RtnTemp.Substring(46, 2) == "02")
                                {
                                    conclusion = "合格";
                                }
                                else if (RtnTemp.Substring(46, 2) == "01")
                                {
                                    conclusion = "不合格";
                                }
                                if (CheckData == 32766 || CheckData > 32000 || checkNum==0)//NA 返回 7FFE
                                {
                                    //NA不显示
                                    Ncount = Ncount + 1;
                                }
                                else
                                {
                                    AddTableData(holenum, standard, checkResult, conclusion, ChkTime);
                                    icount = icount + 1;
                                    Console.WriteLine(icount);
                                    if (readtype == "全部")
                                    {
                                        its = its + 1;
                                        if (its == 160)
                                        {
                                            its = 0;
                                            if (MessageNotification.GetInstance() != null)
                                            {
                                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "TL310");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageNotification.GetInstance() != null)
                                        {
                                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "TL310");
                                        }
                                    }

                                }
                                //icount = icount + 1;
                                //Console.WriteLine(icount + Ncount);
                                RtnTemp = "";
                                if (TotalRecord == (icount + Ncount))
                                {
                                    if (MessageNotification.GetInstance() != null)
                                    {
                                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "end");
                                    }
                                }
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

        private void AddTableData(string Holenum, string Standard, string checkValue, string Conclusion, DateTime time)
        {
            DataRow dr = DataReadTable.NewRow();

            if (Standard == "00")
            {
                item = "农药残留";
                testbase = "GB/T 5009.199-2003";
                standvalue = "50";
            }
            else if (Standard == "01")
            {
                item = "农药残留(农标)";
                testbase = "NY/T 448-2001";
                standvalue = "70";
            }

            strWhere.Length = 0;
            strWhere.AppendFormat(" CheckData='{0}' AND CheckTime='{1}' and Checkitem='{2}' and HoleNum='{3}' ", checkValue, time.ToString("yyyy-MM-dd HH:mm:ss"), item, Holenum);
            dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
            dr["通道号"] = Holenum;
            dr["样品名称"] = "";
    
            dr["检测项目"] = item;
            dr["检测结果"] = checkValue;
            dr["单位"] = "%";
            dr["检测依据"] = testbase;
            dr["标准值"] = standvalue;
            dr["检测仪器"] = Global.ChkManchine;
            dr["结论"] = Conclusion;
            //dr["检测单位"] = unitInfo[0, 0];
            dr["被检单位"] = unitInfo[0, 1];
            //dr["被检单位编号"] = unitInfo[0, 3]; 
            dr["检测员"] = Global.userlog;//unitInfo[0, 2];
            dr["检测时间"] = time.ToString ("yyyy-MM-dd HH:mm:ss");
            dr["摊位号"] = "";

            DataReadTable.Rows.Add(dr);
        }
        /// <summary>
        /// crc国际标准
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        private byte crc8(byte[] bt)
        {
            byte crc = 0;
            for (int j = 0; j < bt.Length; j++)
            {
                crc = (byte)(crc ^ bt[j]);
                for (int i = 8; i > 0; i--)
                {
                    if ((crc & 0x80) == 0x80)
                    {
                        crc = (byte)((crc << 1) ^ 0x31);
                    }
                    else
                    {
                        crc = (byte)(crc << 1);
                    }
                }
            }

            return crc;
        }

       
        /// <summary>
        /// ctc8校验
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string CheckDataSum(string str)
        {
            int len = str.Length / 2;
            string temp = string.Empty;
            byte bt = 0;
            for (int i = 0; i < len; i++)
            {
                temp = str.Substring(i * 2, 2);
                bt += Convert.ToByte(temp, 16);
            }
            return bt.ToString("X2");
        }

        public void closecom()
        {
            if (mSerial.IsOpen)
            {
                mSerial.Close();

            }
        }
    }
}
