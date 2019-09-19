﻿using System;
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
    public  class clsDY2620
    {
        public static SerialPort comn = new SerialPort();
        public DataTable dt = null;
        public DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private DataTable cdt = null;
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private byte[] b2 = new byte[4];//定义byte型数据，用于byte-float转换
      
        private StringBuilder strWhere = new StringBuilder();
        public string[,] unitInfo = new string[1, 6];
        private string checkvalue = "";
        private string unit = "";
        private string conclusion = "";
        public string ReadAll = "";//读取全部数据
        private int N = 0;

        public clsDY2620()
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
                dataCol.ColumnName = "温度";
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
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void comn_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                 string temp = "";
                 int n = comn.BytesToRead;//
                 byte[] buf = new byte[n];//
                 comn.Read(buf, 0, n);//读取缓冲数据 
                 if (ReadAll == "全部")
                 {
                     for (int j = 0; j < buf.Length; j++)//读取缓存数据
                     {
                         temp = temp + buf[j].ToString("X2");
                         if (temp.Length > 4)
                         {
                             if (temp.Substring(0, 4) == "0165")
                             {
                                 if (temp.Length > 5)
                                 {
                                     int datalength = Convert.ToInt32(temp.Substring(4, 2), 16);//1条历史记录的数字长度
                                     if ((datalength * 2 + 6 + 4) == temp.Length)//接收完一条记录
                                     {
                                         string data = temp.Substring(6, datalength * 2);
                                         for (int i = 0; i < datalength / 10; i++)
                                         {
                                             string year = Convert.ToInt32(data.Substring(0 + 20 * i, 2), 16).ToString();//年
                                             string month = Convert.ToInt32(data.Substring(2 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//月
                                             string day = Convert.ToInt32(data.Substring(4 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//日
                                             string hour = Convert.ToInt32(data.Substring(6 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//时
                                             string minus = Convert.ToInt32(data.Substring(8 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//分

                                             string temperature = Convert.ToInt32(data.Substring(10 + 20 * i, 2), 16).ToString();//单位
                                             double tempdd = (double)Convert.ToInt32(data.Substring(12 + 20 * i, 4), 16) / 10;
                                             string tempera = tempdd.ToString() + "℃";//温度
                                             double resultdd = (double)Convert.ToInt32(data.Substring(16 + 20 * i, 4), 16) / 10;
                                             string result = resultdd.ToString();//检测结果
                                             string conclusion = "";
                                             if (resultdd < 27 || resultdd == 27)
                                             {
                                                 conclusion = "合格";
                                             }
                                             else
                                             {
                                                 conclusion = "不合格";
                                             }

                                             string time = "20" + year + "-" + month + "-" + day + " " + hour + ":" + minus + ":00";

                                             AddTable("", "", result, "%", conclusion, tempera, "", "", time);
                                         }
                                         if (MessageNotification.GetInstance() != null)
                                         {
                                             temp = "";
                                             MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY2620, "Data");
                                         }

                                         ReadNext();//读下一页
                                     }
                                 }
                             }
                             else
                             {
                                 if (temp.Length>5)
                                 {
                                     if (temp.Substring(4, 2) == "02" && temp.Length==10)
                                     {
                                         if (MessageNotification.GetInstance() != null)
                                         {
                                             temp = "";
                                             MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY2620, "End");
                                         }
                                     }
                                 }
                                 
                                 Console.WriteLine(temp);
                             }
                         }
                     }
                 }
                 else
                 {
                     for (int j = 0; j < buf.Length; j++)//读取缓存数据
                     {
                         temp = temp + buf[j].ToString("X2");
                         if (temp.Length > 4)
                         {
                             if (temp.Substring(0, 4) == "0165")
                             {
                                 if (temp.Length > 5)
                                 {
                                     int datalength = Convert.ToInt32(temp.Substring(4, 2), 16);//1条历史记录的数字长度
                                     if ((datalength * 2 + 6 + 4) == temp.Length)//接收完一条记录
                                     {
                                         string data = temp.Substring(6, datalength * 2);
                                         for (int i = 0; i < datalength / 10; i++)
                                         {
                                             string year = Convert.ToInt32(data.Substring(0 + 20 * i, 2), 16).ToString();//年
                                             string month = Convert.ToInt32(data.Substring(2 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//月
                                             string day = Convert.ToInt32(data.Substring(4 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//日
                                             string hour = Convert.ToInt32(data.Substring(6 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//时
                                             string minus = Convert.ToInt32(data.Substring(8 + 20 * i, 2), 16).ToString().PadLeft(2, '0');//分

                                             string temperature = Convert.ToInt32(data.Substring(10 + 20 * i, 2), 16).ToString();//单位
                                             double tempdd = (double)Convert.ToInt32(data.Substring(12 + 20 * i, 4), 16) / 10;
                                             string tempera = tempdd.ToString() + "℃";//温度
                                             double resultdd = (double)Convert.ToInt32(data.Substring(16 + 20 * i, 4), 16) / 10;
                                             string conclusion = "";
                                             if (resultdd < 27 || resultdd == 27)
                                             {
                                                 conclusion = "合格";
                                             }
                                             else
                                             {
                                                 conclusion = "不合格";
                                             }
                                             string result = resultdd.ToString();//检测结果

                                             string time = "20" + year + "-" + month + "-" + day + " " + hour + ":" + minus + ":00";

                                             AddTable("", "", result, "%", conclusion, tempera, "", "", time);
                                         }
                                         if (MessageNotification.GetInstance() != null)
                                         {
                                             temp = "";
                                             MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY2620, "Data");
                                         }
                                     }

                                 }
                             }
                             else
                             {
                                 temp = "";
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
       /// 读取全部数据
       /// </summary>
        public void ReadAllData()
        {
            ReadAll = "全部";
            string headdata = "016500000001";
            N = 0;
           
            string crcdata = CRC.ToModbusCRC16(headdata, true);

            byte[] dataSent = clsStringUtil.HexString2ByteArray(headdata + crcdata);
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
        /// 读下一页数据
        /// </summary>
        private void ReadNext()
        {
            N = N + 1;
            N = 900;
            if(N<1000)
            {
                string headdata = "0165";
                string second = "0001";
                string RecordNo = string.Format("{0:x4}", N);

                string crcdata = CRC.ToModbusCRC16(headdata + RecordNo + second, true);
                byte[] dataSent = clsStringUtil.HexString2ByteArray(headdata + RecordNo + second + crcdata);
                try
                {
                    SendData(dataSent);
                }
                catch (JH.CommBase.CommPortException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ReadHistory(DateTime startDate, DateTime endDate)
        {
            ReadAll = "上一次";
            //string headdata = "0165000000018C02";
            string headdata = "016500000001";

            string crcdata = CRC.ToModbusCRC16(headdata,true  );



            byte[] dataSent = clsStringUtil.HexString2ByteArray(headdata+crcdata);   //start + end是协议中提到的[数据]
            try
            {
                SendData(dataSent);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
           
            }
        }

        private void AddTable(string samplename, string item, string checkValue, string unit, string conclusion, string temper, string sentunit, string standard, string time)
        {
           
            DataRow dr = DataReadTable.NewRow();
            strWhere.Length = 0;
            DateTime dt = new DateTime();
            if (time != "")
            {
                dt = DateTime.Parse(time);
                strWhere.AppendFormat(" CheckData='{0}' AND CheckTime='{1}'", checkValue, time);
            }

            dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
            dr["样品名称"] = "食用油";
            dr["检测项目"] = "极性组分";
            dr["检测结果"] = checkValue;
            dr["温度"] = temper;
            dr["单位"] = unit;//检测值
            dr["检测依据"] = "GB 5009.202";
            dr["标准值"] = "27";
            dr["检测仪器"] = Global.ChkManchine;
            dr["结论"] = conclusion;
            dr["检测单位"] = Global.pointName;
            if (dt != null)
            {
                dr["采样时间"] = dt.AddHours(-2).ToString();
                dr["生产日期"] = dt.AddDays(-3).ToString();
                dr["送检日期"] = dt.AddHours(-2).ToString();
            }
            dr["采样地点"] = unitInfo[0, 4];
            dr["被检单位"] = unitInfo[0, 1];
            dr["检测员"] = Global.user_name;
            dr["检测时间"] = time;
            dr["检测数量"] = "1";
            dr["样品种类"] = "食用油";
            dr["产地"] = unitInfo[0, 4];
            dr["生产单位"] = unitInfo[0, 5];
            dr["被检企业性质"] = unitInfo[0, 3];
            dr["处理结果"] = "已处理";
            DataReadTable.Rows.Add(dr);
        }

    }
}
