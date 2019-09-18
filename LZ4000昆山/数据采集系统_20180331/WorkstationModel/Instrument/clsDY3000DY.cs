using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using WorkstationModel.Model;
using WorkstationDAL.Model;
using WorkstationBLL.Mode;

namespace WorkstationModel.Instrument
{
    public class clsDY3000DY : JH.CommBase.CommBase
    {
         #region 自定义变量
        private bool m_IsCreatedDataTable = false;
        private byte m_ItemID = 0;
        private byte m_ItemID1 = 0;
        private byte m_ItemID2 = 0;
        private int m_DataReadByteCount = 0;
        private int m_LatterDataByteCount = 0;
        private int m_RecordStartPosition = 0;
        private int m_RecordEndPosition = 0;
        private int m_RecordCount = 0;
        private string N_DataTemp42 = string.Empty;
        private string N_DataTemp62 = string.Empty;
        private string N_DataTemp72 = string.Empty;
        public  string m_DataRead = string.Empty;
        private string m_ItemParts = string.Empty;
        private string headering;
        private int dataLen = 0, index = 0;
        private List<byte[]> btlist = new List<byte[]>();
        private byte[] bt = new byte[40960];
        public static string[,] CheckItemsArray;
        public static string[,] CheckItemsArray62;
        public static string[,] CheckItemsArray72;
        public DataTable DataReadTable = null;
        public string DY660;
        public string[,] unitInfo = new string[1, 6];
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder strWhere = new StringBuilder();
        #endregion

        public clsDY3000DY()
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
        }

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
            return cs;
        }

        protected override CommBaseSettings NewCommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 115200, Handshake.none, Parity.none);//9600
            return cs;
        }

        public void ReadHistoryThree()
        {
            string start = "AABB068301F9010000";
            byte[] comd;
            comd = Encoding.Default.GetBytes(start);
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(start);
            Send(comd);
        }

        private void rtnData(string data)
        {
            if (data.Length > 0)
            {
                if ((bt[0] == 129) && (bt[dataLen + 5] == 129))
                {
                    List<byte[]> dataList = new List<byte[]>();
                    byte[] databt = new byte[10];
                    int ind = 0;
                    for (int i = 0; i < dataLen + 4; i++)
                    {
                        if (i > 3)
                        {
                            databt[ind] = bt[i];
                            if (ind == 9)
                            {
                                dataList.Add(databt);
                                ind = -1;
                                databt = new byte[10];
                            }
                            ind++;
                        }
                    }
                    if (dataList.Count > 0)
                    {
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            string num = "", item = "", checkValue = "", unit = "", datetime = "", error = "";
                            num = dataList[i][0].ToString();
                            item = "农药残留(国)";
                            double value = (double)(dataList[i][8] + dataList[i][9] * 256);
                            checkValue = (value / 100).ToString("F2");
                            unit = "%";
                            datetime = "20" + dataList[i][3] + "-" + dataList[i][4] + "-" + dataList[i][5] + " "
                                + dataList[i][6] + ":" + dataList[i][7] + ":00";
                            error = "否";
                            AddNewHistoricData(num, item, checkValue, unit, datetime, error);
                        }
                        m_RecordCount = 0;
                        m_RecordStartPosition = 0;
                        m_RecordEndPosition = 0;
                        m_DataReadByteCount = 0;
                        m_DataRead = string.Empty;
                        m_LatterDataByteCount = 0;
                        if (Model.MessageNotification.GetInstance() != null)
                            Model.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
                    }
                }
            }
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
                m_DataRead = "";
                Send(dataSent);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        public void ReadHistory(DateTime startDate, DateTime endDate)
        {
            m_DataRead = "";
            if (DataReadTable.Rows != null)
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
                Send(dataSent);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ParseDataFromDevice(string dataRead)
        {
            string command = dataRead.Substring(4, 2);
            string temp = string.Empty;
            //通信测试
            if (command.Equals("20"))
            {
                //if (dataRead.Substring(dataRead.Length -16, 16) == "50524E2032335858")
                //{
                    if (MessageNotification.GetInstance() != null)
                    {
                        m_DataRead = "";
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem,"tongxin");
                    }
                //}
            }
            //湿化学
            #region
            if (command.Equals("42") || command.Equals("62") || command.Equals("72")) //如果是读取检测项目
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                temp = dataRead.Substring(24, 32);
                byte[] itemHexArray = clsStringUtil.HexString2ByteArray(clsStringUtil.HexStringTrim(temp));
                string chineseItemName = gb2312.GetString(itemHexArray);//项目名称
                temp = dataRead.Substring(56, 16);
                byte[] unitHexArray = clsStringUtil.HexString2ByteArray(clsStringUtil.HexStringTrim(temp));
                string chineseUnit = gb2312.GetString(unitHexArray);//单位
                temp = dataRead.Substring(72, 2);
                int checkValue = Convert.ToByte(temp, 16);
                if (command.Equals("42") && DY660 == "022")
                {
                    m_ItemParts += "{" + "非试纸法-" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                if (command.Equals("62") && DY660 == "022")
                {
                    m_ItemParts += "{" + "金标法-" + chineseItemName.Trim() + ":-6:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                if (command.Equals("72") && DY660 == "022")
                {
                    m_ItemParts += "{" + "干化学法-" + chineseItemName.Trim() + ":-7:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                else if (command.Equals("42") && DY660 != "022")
                { m_ItemParts += "{" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}"; }
                if (dataRead.Length > 512)
                {
                    temp = dataRead.Substring(280, 32);
                    itemHexArray = clsStringUtil.HexString2ByteArray(clsStringUtil.HexStringTrim(temp));
                    chineseItemName = gb2312.GetString(itemHexArray);
                    temp = dataRead.Substring(312, 16);
                    unitHexArray = clsStringUtil.HexString2ByteArray(clsStringUtil.HexStringTrim(temp));
                    chineseUnit = gb2312.GetString(unitHexArray);
                    temp = dataRead.Substring(328, 2);
                    checkValue = Convert.ToByte(temp, 16);
                    if (command.Equals("42") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "非试纸法-" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                    }
                    if (command.Equals("62") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "金标法-" + chineseItemName.Trim() + ":-6:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                    }
                    if (command.Equals("72") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "干化学法-" + chineseItemName.Trim() + ":-7:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                    }
                    else if (command.Equals("42") && DY660 != "022")
                    { m_ItemParts += "{" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}"; }
                    if (command.Equals("42"))
                    {
                        m_ItemID++;
                        ReadItems(m_ItemID);
                    }
                    if (command.Equals("62"))
                    {
                        m_ItemID1++;
                        ReadItems1(m_ItemID1);
                    }
                    if (command.Equals("72"))
                    {
                        m_ItemID2++;
                        ReadItems2(m_ItemID2);
                    }
                }
                else //数据不完整
                {
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;

                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                    }
                }
            }
            else if (command.Equals("43") || command.Equals("63") || command.Equals("73")) //如果读记录条数,读取检测项目不需要用到此函数
            {
                string tempStr = dataRead.Substring(40, 4);
                int i1 = 0;
                i1 = Convert.ToByte(tempStr.Substring(2, 2), 16) << 8;
                i1 += Convert.ToByte(tempStr.Substring(0, 2), 16);
                m_RecordStartPosition = i1;
                tempStr = dataRead.Substring(44, 4);
                i1 = Convert.ToByte(tempStr.Substring(2, 2), 16) << 8;
                i1 += Convert.ToByte(tempStr.Substring(0, 2), 16);
                m_RecordEndPosition = i1;    //recordNum = 0;
                //根据记录条数，构造发送数据
                if (m_RecordStartPosition + 1 <= m_RecordEndPosition)
                {
                    if (N_DataTemp42 == "非试纸法")
                    {
                        ReadRecords(m_RecordStartPosition);
                    }
                    if (N_DataTemp62 == "金标卡法")
                    {
                        ReadRecords1(m_RecordStartPosition);
                    }
                    if (N_DataTemp72 == "干化学法")
                    {
                        ReadRecords2(m_RecordStartPosition);
                    }
                    if (string.IsNullOrEmpty(N_DataTemp72) && string.IsNullOrEmpty(N_DataTemp62) && string.IsNullOrEmpty(N_DataTemp42))
                    {
                        ReadRecords(m_RecordStartPosition);
                    }
                }
                else
                {
                    m_RecordCount = 0;
                    m_RecordStartPosition = 0;
                    m_RecordEndPosition = 0;
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
                    }
                }
            }
            else if (command.Equals("45") || command.Equals("65") || command.Equals("75")) //表明读的记录条数
            {
                string ON1 = dataRead.Substring(24, 2);
                if (!dataRead.Substring(24, 2).Equals("00"))
                {
                    int itemID = Convert.ToByte(dataRead.Substring(26, 2), 16);   //仪器上的项目序号
                    string item = string.Empty;
                    string ON2 = dataRead.Substring(26, 2);
                    string unit = string.Empty;
                    string methodNO = string.Empty;//仪器内部检测方法编号
                    string datetime = string.Empty;
                    if (itemID >= 0 && itemID <= CheckItemsArray.GetLength(0) - 1)
                    {
                        if (command.Equals("45"))
                        {
                            item = CheckItemsArray[itemID, 0];
                            unit = CheckItemsArray[itemID, 3];
                            methodNO = CheckItemsArray[itemID, 2];
                        }
                        if (command.Equals("65"))
                        {
                            item = CheckItemsArray62[itemID, 0];
                            unit = CheckItemsArray62[itemID, 3];
                            methodNO = CheckItemsArray62[itemID, 2];
                        }
                        if (command.Equals("75"))
                        {
                            item = CheckItemsArray72[itemID, 0];
                            unit = CheckItemsArray72[itemID, 3];
                            methodNO = CheckItemsArray72[itemID, 2];
                        }
                    }
                    if (command.Equals("45"))
                    {
                        datetime = clsStringUtil.HexStr2DateTimeString(dataRead.Substring(32, 8));
                    }
                    if (command.Equals("65") || command.Equals("75"))
                    {
                        datetime = clsStringUtil.HexStr2DateTimeString(dataRead.Substring(28, 8));
                    }
                    string num = string.Empty;
                    string checkValue = string.Empty;   //检测值
                    string error = "否";    // 数据是否可疑
                    string partOutcome = string.Empty;
                    string tempStr = dataRead.Substring(48);
                    int byteCount = 16;    //数据串长度(8个字节)
                    int recordCount = tempStr.Length / byteCount;
                    int p = 0;
                    int channelNO = 0;
                    if (command.Equals("45"))
                    {
                        for (int i = 0; i < recordCount;         i++)
                        {
                            channelNO = Convert.ToByte(dataRead.Substring(40 + i * 16, 2), 16);
                            p = Convert.ToByte(dataRead.Substring(42 + i * 16, 2), 16);
                            //if (p >= 128 && channelNO > 0)   // if ( ctrlChannel > 0) //p大于等于128表明有效,k=0表明是对照通道
                            if (channelNO > 0)
                            {
                                error = "否";
                                if (p >= 192)
                                {
                                    error = "是";       //说明数据可疑
                                }
                                num = channelNO.ToString();

                                if (methodNO.Equals("0")) //为抑制率法
                                {
                                    partOutcome = dataRead.Substring(48 + i * 16, 4).ToUpper();
                                    if (partOutcome.Equals("FF7F"))//无效数据
                                    {
                                        checkValue = "---.-";
                                    }
                                    else if (partOutcome.Equals("FE7F"))
                                    {
                                        checkValue = "NA";
                                    }
                                    else
                                    {
                                        checkValue = clsStringUtil.HexString2Float2String(partOutcome);
                                    }
                                }
                                else //其他方法
                                {
                                    partOutcome = dataRead.Substring(48 + i * 16, 8).ToUpper();
                                    if (partOutcome.Equals("FFFFFF7F") || partOutcome.Equals("FEFFFF7F") || partOutcome.Equals("FF7F0000") || partOutcome.Equals("2C420F00"))    //无效数据
                                    {
                                        checkValue = "-.---";
                                    }
                                    else if (partOutcome.Equals("FDFFFF7F") || partOutcome.Equals("FE7F0000"))    //可怀疑数据
                                    {
                                        checkValue = "NA";
                                    }
                                    else
                                    {
                                        checkValue = clsStringUtil.HexString2Float4String(partOutcome);
                                    }
                                }
                                if (!(checkValue.Equals("NA") || checkValue.Equals("-.---") || checkValue.Equals("---.-")))
                                {
                                    AddNewHistoricData(num, item, checkValue, unit, datetime, error);
                                    int d = DataReadTable.Rows.Count;
                                }
                               
                            }
                        }
                    }
                    if (command.Equals("65") || command.Equals("75"))
                    {
                        string OUTnum = dataRead.Substring(36, 4).ToUpper();
                        double NOE = Convert.ToDouble(clsStringUtil.HexString2Float2String(OUTnum));
                        num = (NOE * 10).ToString();
                        byte[] TempArry = clsStringUtil.HexString2ByteArray(dataRead.Substring(24));
                        string o = dataRead.Substring(24);
                        if (command.Equals("65"))
                        {
                            switch (TempArry[8] >> 6 & 0x03)
                            {  //高2位表示检测方法
                                case 0:
                                case 1: switch (TempArry[8] & 0x03)
                                    {
                                        case 1: checkValue = "阳性";
                                            break;
                                        case 2: checkValue = "阴性";
                                            break;
                                        case 3: checkValue = "NA";
                                            break;
                                        default: checkValue = "NA";
                                            break;
                                    }
                                    break;
                                case 2:
                                case 3:
                                    partOutcome = dataRead.Substring(42, 12).ToUpper();
                                    checkValue = clsStringUtil.HexString2Float4String(partOutcome);
                                    break;
                            }
                        }
                        if (command.Equals("75"))
                        {
                            switch (TempArry[8] >> 6 & 0x03)
                            {  //高2位表示检测方法
                                case 0: switch (TempArry[8] & 0x03)
                                    {
                                        case 1: checkValue = "阳性";
                                            break;
                                        case 2: checkValue = "阴性";
                                            break;
                                        case 3: checkValue = "NA";
                                            break;
                                        default: checkValue = "NA";
                                            break;
                                    }; break;

                                case 2: partOutcome = dataRead.Substring(42, 12).ToUpper();
                                    checkValue = clsStringUtil.HexString2Float4String(partOutcome); break;
                            }
                        }
                        if (!(checkValue.Equals("NA") || checkValue.Equals("-.---") || checkValue.Equals("---.-")))
                        {
                            AddNewHistoricData(num, item, checkValue, unit, datetime, error);
                            
                        }
                    }
                }
                m_RecordCount++;
                int endPosition = m_RecordStartPosition + m_RecordCount;
                if (endPosition >= m_RecordEndPosition)
                {
                    m_RecordCount = 0;
                    m_RecordStartPosition = 0;
                    m_RecordEndPosition = 0;
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
                       
                    }
                }
                else
                {
                    if (command.Equals("45"))
                    {
                        ReadRecords(endPosition);
                    }
                    if (command.Equals("65"))
                    {
                        ReadRecords1(endPosition);
                    }
                    if (command.Equals("75"))
                    {
                        ReadRecords2(endPosition);
                    }
                }
            }
            #endregion
            //胶体金
        }

        private void ReadRecords(int input)
        {
            uint little = Model.clsStringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF4500" + parameter + "0000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadRecords1(int input)
        {
            uint little = Model.clsStringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF6500" + parameter + "0000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);
            string o = header + checkSum + "00";
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadRecords2(int input)
        {
            uint little = Model.clsStringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF7500" + parameter + "0000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadItems(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF420000" + parameter + "00000000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);//检验和
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        /// <summary>
        /// DY-6600项目读取
        /// </summary>
        /// <param name="input"></param>
        private void ReadItems1(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF620000" + parameter + "00000000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);//检验和
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadItems2(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF720000" + parameter + "00000000";
            string checkSum = Model.clsStringUtil.CheckDataSum(header);//检验和
            byte[] dataSent = Model.clsStringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void AddNewHistoricData(string num, string item, string checkValue, string unit, string time, string p)
        {
         
            DataRow  dr = DataReadTable.NewRow();
            DateTime dt = DateTime.Parse(time);

            strWhere.Length = 0;
            if (Global.ChkManchine == "DY-6600手持执法快检通")
            {
                strWhere.AppendFormat(" CheckData='{0}' AND Checkitem='{1}' AND CheckTime=#{2}#", checkValue, item.Substring(4, item.Length - 4), DateTime.Parse(time));
                dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
                dr["样品名称"] = "";
                string[] itm = item.Split('-');               
                dr["检测项目"] = itm[1];
                dr["检测结果"] = checkValue;
                dr["单位"] = unit;//检测值
                dr["检测依据"] ="";
                dr["标准值"] = "";
                dr["检测仪器"] = Global.ChkManchine;
                dr["结论"] = "";//checkValue == "阴性" || checkValue == "合格" ? "合格" : "不合格";
                dr["检测单位"] = Global.DetectUnit;
                dr["采样时间"] = dt.AddHours(-2).ToString();
                dr["采样地点"] = unitInfo[0, 1];
                dr["被检单位"] = unitInfo[0, 2];
                dr["检测员"] = Global.NickName;
                dr["检测时间"] = time;
                dr["检测数量"] = "1";
                dr["样品种类"] = "食用农产品";            
            }
            else
            {
                strWhere.AppendFormat(" CheckData='{0}' AND Checkitem='{1}' AND CheckTime=#{2}#", checkValue, item, DateTime.Parse(time));
                dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
                dr["样品名称"] = "";               
                dr["检测项目"] = item;               
                dr["检测结果"] = checkValue;
                dr["单位"] = unit;//检测值
                dr["检测依据"] = "GB/T 5009.199-2003";
                dr["标准值"] ="50" ;
                dr["检测仪器"] = Global.ChkManchine;
                dr["结论"] = "";
                dr["检测单位"] = unitInfo[0, 0];
                dr["采样时间"] = dt.AddHours(-2).ToString();
                dr["采样地点"] = unitInfo[0, 4];
                dr["被检单位"] = unitInfo[0, 1];
                dr["检测员"] = unitInfo[0, 2];
                dr["检测时间"] = time;
                dr["检测数量"] = "1";
                dr["样品种类"] = "果菜类";
                dr["产地"] = unitInfo[0, 4];
                dr["生产单位"] = unitInfo[0, 5];
                dr["生产日期"] = dt.AddDays(-3).ToString();
                dr["被检企业性质"] = unitInfo[0, 3];
                dr["送检日期"] = dt.AddHours(-2).ToString();
                dr["处理结果"] = "销售";
            }    
            DataReadTable.Rows.Add(dr);
        }

        public void ReadItem(string Name)
        {
            DY660 = Name;
            m_ItemParts = string.Empty;
            m_ItemID = 0;
            ReadItems(m_ItemID);
        }

        public void ReadItem1(string Name)
        {
            DY660 = Name;
            m_ItemParts = string.Empty;
            m_ItemID1 = 0;
            ReadItems1(m_ItemID1);
        }

        public void ReadItem2(string Name)
        {
            DY660 = Name;
            m_ItemParts = string.Empty;
            m_ItemID2 = 0;
            ReadItems2(m_ItemID2);
        }
        protected override void OnRxChar(byte c)
        {
            if (m_DataRead.Equals("8104"))
                dataLen = int.Parse(c.ToString(), 0);
            string currentByte = c.ToString("X2");    //转换成2位的16进制
            m_DataRead += currentByte;
            bt[index] = c;
            index++;
            if (dataLen > 0)
            {
                if (m_DataRead.Length == (dataLen + 6) * 2)
                    rtnData(m_DataRead);
            }
            else if (m_DataRead.Equals("810400000181"))
            {
                m_RecordCount = 0;
                m_RecordStartPosition = 0;
                m_RecordEndPosition = 0;
                m_DataReadByteCount = 0;
                m_DataRead = string.Empty;
                m_LatterDataByteCount = 0;
                if (MessageNotification.GetInstance() != null)
                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
            }

            int len = m_DataRead.Length;
            if (m_DataReadByteCount == 0 && currentByte.Equals("FE") && len >= 4)
            {
                bool isEnd = m_DataRead.Substring(len - 4, 2).Equals("03");
                if (isEnd)
                    m_DataReadByteCount = 1;
            }
            if (m_DataReadByteCount > 0)
            {
                m_DataReadByteCount++;
                string checkSum = string.Empty;
                //再次判断获得参数是否正确,不正确清空,正确确定后续数据的长度
                if (m_DataReadByteCount == 11)
                {
                    m_DataRead = m_DataRead.Substring(len - 22, 22);
                    checkSum = clsStringUtil.CheckDataSum(m_DataRead.Substring(0, 20));
                    if (currentByte.Equals(checkSum))
                    {
                        string tempStr = m_DataRead.Substring(16, 4);
                        int i1 = Convert.ToByte(tempStr.Substring(2, 2), 16) << 8;
                        i1 += Convert.ToByte(tempStr.Substring(0, 2), 16);
                        m_LatterDataByteCount = i1;
                    }
                    else
                    {
                        m_DataReadByteCount = 0;
                        m_DataRead = string.Empty;
                        m_LatterDataByteCount = 0;
                    }
                }
               
                if (m_DataReadByteCount == 12 + m_LatterDataByteCount)
                {
                    if (m_LatterDataByteCount > 0)
                    {
                        checkSum = clsStringUtil.CheckDataSum(m_DataRead.Substring(24, m_LatterDataByteCount * 2));
                        if (m_DataRead.Substring(22, 2).Equals(checkSum))
                            ParseDataFromDevice(m_DataRead);
                    }
                    if (m_LatterDataByteCount == 0)//如果检测项目
                    {
                        int tlen = m_DataRead.Length;
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("42")) //表明检测项目读完
                        {
                            if (MessageNotification.GetInstance() != null)
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("62")) //表明检测项目读完
                        {
                            if (MessageNotification.GetInstance() != null)
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("72")) //表明检测项目读完
                        {
                            if (MessageNotification.GetInstance() != null)
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                    }
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;
                }
            }
        }
        /// <summary>
        /// DY6600读取数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="quer"></param>
        public void ReadHistory2(DateTime startDate, DateTime endDate, string quer)
        {
            headering = string.Empty;
            if (DataReadTable.Rows != null)
                DataReadTable.Rows.Clear();

            if (quer == "非试纸法")
            {
                N_DataTemp42 = "非试纸法";
                N_DataTemp62 = "";
                N_DataTemp72 = "";
                headering = "03FF4300000000000800B7";//标识头
            }
            if (quer == "金标卡法")
            {
                N_DataTemp72 = "";
                N_DataTemp42 = "";
                N_DataTemp62 = "金标卡法";
                
                headering = "03FF630000000000080097";//标识头
            }
            if (quer == "干化学法")
            {
                N_DataTemp62 = "";
                N_DataTemp42 = "";
                N_DataTemp72 = "干化学法";
                headering = "03FF730000000000080087";//标识头
            }
            uint tempStartDate, tempEndDate, resultStartDate, resultEndDate;
            tempStartDate = (uint)(startDate.Year % 100) << 25;
            tempStartDate = tempStartDate | ((uint)(startDate.Month << 21));
            tempStartDate = tempStartDate | ((uint)(startDate.Day << 16));
            //因起始时间的时分秒均为0,所以不用再移位
            //vt = vt | ((uint)(dtStart.Hour << 11));
            //vt = vt | ((uint)(dtStart.Minute << 5));
            //vt = vt | ((uint)(dtStart.Second >> 1));
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
            string o = headering + checkSum + start + end;
            byte[] dataSent = clsStringUtil.HexString2ByteArray(headering + checkSum + start + end);   //start + end是协议中提到的[数据]
            try { }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Send(dataSent);
        }
    }
}
