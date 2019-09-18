using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationModel.Model;
using System.Windows.Forms;

namespace WorkstationModel.Instrument
{
    public class clsDY5800 
    {
        public static SerialPort comn = new SerialPort();
        public DataTable dt = null;
        public static DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private DataTable cdt = null;
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private byte[] b2 = new byte[4];//定义byte型数据，用于byte-float转换
        public  string[,] unitInfo = new string[1, 7];
        private string result = "";
        public clsDY5800()
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
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品编号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "浓度值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "吸光度";
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

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样地点";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "被检企业性质";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);


                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测数量";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "产地";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产日期";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "送检日期";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测方法";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "处理结果";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
            //try
            //{
            //    string err = "";
            //    cdt = sqlSet.GetAHCompany("", "", out err);//安徽
            //    //cdt = sqlSet.GetInformation("", "", out err);
            //    if (cdt != null)
            //    {
            //        if (cdt.Rows.Count > 0)
            //        {
            //            for (int n = 0; n < cdt.Rows.Count; n++)
            //            {
            //                if (cdt.Rows[n]["iChecked"].ToString() == "是")
            //                {
            //                    unitInfo[0, 0] = cdt.Rows[n]["DetectUnitName"].ToString();
            //                    unitInfo[0, 1] = cdt.Rows[n]["SampleAddress"].ToString();
            //                    unitInfo[0, 2] = cdt.Rows[n]["Tester"].ToString();
            //                    unitInfo[0, 3] = cdt.Rows[n]["TestUnitName"].ToString();//检测单位
            //                    unitInfo[0, 4] = cdt.Rows[n]["DetectUnitNature"].ToString();//被检单位性质
            //                    unitInfo[0, 5] = cdt.Rows[n]["ProductAddr"].ToString();//产地
            //                    unitInfo[0, 6] = cdt.Rows[n]["ProductCompany"].ToString();//生产单位
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comn_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //System.Threading.Thread.Sleep(1000);
                int n = comn.BytesToRead;//
                byte[] buf = new byte[n];//
                comn.Read(buf, 0, n);//读取缓冲数据 
                //comn.DiscardInBuffer();//清掉数据缓冲区数据就读不回来了
                //result = "";
                string anandata = "";
                for (int j = 0; j < buf.Length; j++)
                {
                    if (firstdata == "" && buf[j].ToString("X2") == "50")
                    {
                        firstdata = buf[j].ToString("X2");
                        result += buf[j].ToString("X2");
                    }
                    else if (firstdata == "50")
                    {
                        Console.Write(buf[j].ToString("X2"));
                        if (buf[j].ToString("X2") == "58" && result.Length > 161)
                        {
                            result += buf[j].ToString("X2");
                            firstdata = "";
                            Analysedata(result);//解析数据
                            result = "";
                        }
                        else
                        {
                            result += buf[j].ToString("X2");
                        }
                    }

                    //result += buf[j].ToString("X2");

                    //if (buf[j].ToString("X2") == "58" && result.Substring(0, 2) == "50" && result.Length ==164)
                    //{
                    //    Analysedata(result);//解析数据
                    //    result = "";
                    //}
                }
                //Console.Write(result);
                if (MessageNotification.GetInstance() != null)
                {
                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY5800, "DY5800");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void Analysedata(string data)
        {
            string hole = HexToChar(data.Substring(2,6));
            string samplecode = HexToChar(data.Substring(8, 14));
            string item= HexToChar(data.Substring(22, 40));
            string units= HexToChar(data.Substring(62, 20));
            string nongduzhi = HexToChar(data.Substring(84, 10));
            string xiguangdu = HexToChar(data.Substring(94, 10));
            string checkdata = HexToChar(data.Substring(104, 12));

            string datat = data.Substring(106, 12);
            string datati = data.Substring(116, 8);

            string checktime= HexToChar(data.Substring(116, 8));

            string times ="20" + checkdata.Substring(0, 2) + "-" + checkdata.Substring(2, 2) + "-" + checkdata.Substring(4, 2) + " " + checktime.Substring(0, 2) + ":" + checktime.Substring(2, 2)+":"+"00"; 
            string checkuser = HexToChar(data.Substring(122, 20));
            string samplename = HexToChar(data.Substring(142, 20));

            AddToTable(samplename, item, "", nongduzhi, times, hole, samplecode, units, xiguangdu);
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
        /// 添加数据到表
        /// </summary>
        /// <param name="checkValue"></param>
        /// <param name="time"></param>
        /// <param name="holes"></param>
        private void AddToTable(string sample, string item, string conclusion, string checkValue, string checktime, string holes,string samplecode,string unit,string xiguagn)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["通道号"] = holes.Trim();
            dr["样品名称"] = sample.Trim();
            dr["样品编号"] = samplecode.Trim();
            dr["检测项目"] = item.Trim();
            dr["浓度值"] = checkValue.Trim();
            dr["吸光度"] = xiguagn.Trim();
            dr["单位"] = unit.Trim();//检测值
            if (item.Trim() == "农药总残留")
            {
                dr["检测依据"] = "GB/T 5009.199-2003";
                dr["标准值"] = "50";
                if (checkValue == "NaN")
                {
                    dr["结论"] = "无效";
                }
                else if (Convert.ToDouble(checkValue) < 50 || Convert.ToDouble(checkValue) == 50)
                {
                    dr["结论"] = "合格";
                }
                else
                {
                    dr["结论"] = "不合格";
                }
            }
            else
            {
                dr["检测依据"] = "";
                dr["标准值"] = "";
                dr["结论"] = "";
            }
            if (checktime.Length >8)
            {
                dr["采样时间"] = DateTime.Parse(checktime).AddHours(-2);
                dr["生产日期"] = DateTime.Parse(checktime).AddDays(-1);
                //dr["送检日期"] = DateTime.Parse(checktime).AddDays(-1);
                sb.Length = 0;
                sb.AppendFormat(" CheckData='{0}'", checkValue);
                sb.AppendFormat(" AND Checkitem='{0}'", item);
                sb.AppendFormat(" AND HoleNum='{0}'", holes);
                sb.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(checktime));
                dr["已保存"] = (sqlSet.IsExist(sb.ToString()) == true ? "是" : "否");
            }
            dr["检测仪器"] = Global.ChkManchine;
            //dr["检测单位"] = ""; //unitInfo[0, 3];
            //dr["检测数量"] = "1";
            //dr["采样地点"] = unitInfo[0, 1];
            dr["被检单位"] = Global.tCompany;//unitInfo[0, 0];
            dr["检测员"] =Global.userlog;
            dr["检测时间"] = checktime;
            //dr["被检企业性质"] = unitInfo[0, 4];
            dr["样品种类"] = "";
            //dr["产地"] = unitInfo[0, 5];
            dr["生产单位"] ="";
            dr["处理结果"] = "已处理";
            dr["检测方法"] = "定量";
            DataReadTable.Rows.Add(dr);
        }
    }
}
