using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JH.CommBase;

namespace DY.FoodClientLib
{
    public class clsDY3000DY : CommBase
    {

        #region �Զ������
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
        private string m_DataRead = string.Empty;
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
        #endregion

        public clsDY3000DY()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "�ѱ���";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(int);
                dataCol.ColumnName = "���";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "�����Ŀ";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "������";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "��λ";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "���ʱ��";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "���ݿ�����";
                DataReadTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
            }
        }

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
            return cs;
        }

        protected override CommBaseSettings NewCommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(ShareOption.ComPort, 115200, Handshake.none, Parity.none);//9600
            return cs;
        }

        public void ReadHistoryThree()
        {
            string start = "AABB068301F9010000";
            byte[] comd;
            comd = Encoding.Default.GetBytes(start);
            byte[] dataSent = StringUtil.HexString2ByteArray(start);
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
                            item = "ũҩ����(��)";
                            double value = (double)(dataList[i][8] + dataList[i][9] * 256);
                            checkValue = (value / 100).ToString("F2");
                            unit = "%";
                            datetime = "20" + dataList[i][3] + "-" + dataList[i][4] + "-" + dataList[i][5] + " "
                                + dataList[i][6] + ":" + dataList[i][7] + ":00";
                            error = "��";
                            AddNewHistoricData(num, item, checkValue, unit, datetime, error);
                        }
                        m_RecordCount = 0;
                        m_RecordStartPosition = 0;
                        m_RecordEndPosition = 0;
                        m_DataReadByteCount = 0;
                        m_DataRead = string.Empty;
                        m_LatterDataByteCount = 0;
                        if (FoodClientLib.MessageNotification.GetInstance() != null)
                            FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
                    }
                }
            }
        }

        protected override void OnRxChar(byte c)
        {
            if (m_DataRead.Equals("8104"))
                dataLen = int.Parse(c.ToString(), 0);
            string currentByte = c.ToString("X2");    //ת����2λ��16����
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
                if (FoodClientLib.MessageNotification.GetInstance() != null)
                    FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
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
                //�ٴ��жϻ�ò����Ƿ���ȷ,����ȷ���,��ȷȷ���������ݵĳ���
                if (m_DataReadByteCount == 11)
                {
                    m_DataRead = m_DataRead.Substring(len - 22, 22);
                    checkSum = StringUtil.CheckDataSum(m_DataRead.Substring(0, 20));
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
                        checkSum = StringUtil.CheckDataSum(m_DataRead.Substring(24, m_LatterDataByteCount * 2));
                        if (m_DataRead.Substring(22, 2).Equals(checkSum))
                            ParseDataFromDevice(m_DataRead);
                    }
                    if (m_LatterDataByteCount == 0)//��������Ŀ
                    {
                        int tlen = m_DataRead.Length;
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("42")) //���������Ŀ����
                        {
                            if (FoodClientLib.MessageNotification.GetInstance() != null)
                                FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("62")) //���������Ŀ����
                        {
                            if (FoodClientLib.MessageNotification.GetInstance() != null)
                                FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                        if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("72")) //���������Ŀ����
                        {
                            if (FoodClientLib.MessageNotification.GetInstance() != null)
                                FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                        }
                    }
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;
                }
            }
        }

        public void ReadHistory2(DateTime startDate, DateTime endDate, string quer)
        {
            headering = string.Empty;
            if (DataReadTable.Rows != null)
                DataReadTable.Rows.Clear();

            if (quer == "����ֽ��")
            {
                N_DataTemp42 = "����ֽ��";
                N_DataTemp62 = "";
                N_DataTemp72 = "";
                headering = "03FF4300000000000800B7";//��ʶͷ
            }
            if (quer == "��꿨��")
            {
                N_DataTemp72 = "";
                N_DataTemp42 = "";
                N_DataTemp62 = "��꿨��";
                headering = "03FF630000000000080097";//��ʶͷ
            }
            if (quer == "�ɻ�ѧ��")
            {
                N_DataTemp62 = "";
                N_DataTemp42 = "";
                N_DataTemp72 = "�ɻ�ѧ��";
                headering = "03FF730000000000080087";//��ʶͷ
            }
            uint tempStartDate, tempEndDate, resultStartDate, resultEndDate;
            tempStartDate = (uint)(startDate.Year % 100) << 25;
            tempStartDate = tempStartDate | ((uint)(startDate.Month << 21));
            tempStartDate = tempStartDate | ((uint)(startDate.Day << 16));
            //����ʼʱ���ʱ�����Ϊ0,���Բ�������λ
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
            resultStartDate = StringUtil.ToLittleEndian(resultStartDate);//ת��ΪС��ģʽ
            resultEndDate = StringUtil.ToLittleEndian(resultEndDate);
            string start = resultStartDate.ToString("X8");// 8λ16����
            string end = resultEndDate.ToString("X8");
            string checkSum = StringUtil.CheckDataSum(start + end);//Э�����ᵽ��[���ݲ���У���]
            string o = headering + checkSum + start + end;
            byte[] dataSent = StringUtil.HexString2ByteArray(headering + checkSum + start + end);   //start + end��Э�����ᵽ��[����]
            try { }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Send(dataSent);
        }

        public void ReadHistory(DateTime startDate, DateTime endDate)
        {
            if (DataReadTable.Rows != null)
            {
                DataReadTable.Rows.Clear();
            }
            string header = "03FF4300000000000800B7";//��ʶͷ
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
            resultStartDate = StringUtil.ToLittleEndian(resultStartDate);//ת��ΪС��ģʽ
            resultEndDate = StringUtil.ToLittleEndian(resultEndDate);
            string start = resultStartDate.ToString("X8");// 8λ16����
            string end = resultEndDate.ToString("X8");
            string checkSum = StringUtil.CheckDataSum(start + end);//Э�����ᵽ��[���ݲ���У���]
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + start + end);   //start + end��Э�����ᵽ��[����]
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
            //ʪ��ѧ
            #region
            if (command.Equals("42") || command.Equals("62") || command.Equals("72")) //����Ƕ�ȡ�����Ŀ
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                temp = dataRead.Substring(24, 32);
                byte[] itemHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
                string chineseItemName = gb2312.GetString(itemHexArray);//��Ŀ����
                temp = dataRead.Substring(56, 16);
                byte[] unitHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
                string chineseUnit = gb2312.GetString(unitHexArray);//��λ
                temp = dataRead.Substring(72, 2);
                int checkValue = Convert.ToByte(temp, 16);
                if (command.Equals("42") && DY660 == "022")
                {
                    m_ItemParts += "{" + "����ֽ��-" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                if (command.Equals("62") && DY660 == "022")
                {
                    m_ItemParts += "{" + "��귨-" + chineseItemName.Trim() + ":-6:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                if (command.Equals("72") && DY660 == "022")
                {
                    m_ItemParts += "{" + "�ɻ�ѧ��-" + chineseItemName.Trim() + ":-7:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                }
                else if (command.Equals("42") && DY660 != "022")
                { m_ItemParts += "{" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}"; }
                if (dataRead.Length > 512)
                {
                    temp = dataRead.Substring(280, 32);
                    itemHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
                    chineseItemName = gb2312.GetString(itemHexArray);
                    temp = dataRead.Substring(312, 16);
                    unitHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
                    chineseUnit = gb2312.GetString(unitHexArray);
                    temp = dataRead.Substring(328, 2);
                    checkValue = Convert.ToByte(temp, 16);
                    if (command.Equals("42") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "����ֽ��-" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                    }
                    if (command.Equals("62") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "��귨-" + chineseItemName.Trim() + ":-6:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
                    }
                    if (command.Equals("72") && DY660 == "022")
                    {
                        m_ItemParts += "{" + "�ɻ�ѧ��-" + chineseItemName.Trim() + ":-7:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
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
                else //���ݲ�����
                {
                    m_DataReadByteCount = 0;
                    m_DataRead = string.Empty;
                    m_LatterDataByteCount = 0;

                    if (FoodClientLib.MessageNotification.GetInstance() != null)
                    {
                        FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYItem, m_ItemParts);
                    }
                }
            }
            else if (command.Equals("43") || command.Equals("63") || command.Equals("73")) //�������¼����,��ȡ�����Ŀ����Ҫ�õ��˺���
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
                //���ݼ�¼���������췢������
                if (m_RecordStartPosition + 1 <= m_RecordEndPosition)
                {
                    if (N_DataTemp42 == "����ֽ��")
                    {
                        ReadRecords(m_RecordStartPosition);
                    }
                    if (N_DataTemp62 == "��꿨��")
                    {
                        ReadRecords1(m_RecordStartPosition);
                    }
                    if (N_DataTemp72 == "�ɻ�ѧ��")
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
                    if (FoodClientLib.MessageNotification.GetInstance() != null)
                    {
                        FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
                    }
                }
            }
            else if (command.Equals("45") || command.Equals("65") || command.Equals("75")) //�������ļ�¼����
            {
                string ON1 = dataRead.Substring(24, 2);
                if (!dataRead.Substring(24, 2).Equals("00"))
                {
                    int itemID = Convert.ToByte(dataRead.Substring(26, 2), 16);   //�����ϵ���Ŀ���
                    string item = string.Empty;
                    string ON2 = dataRead.Substring(26, 2);
                    string unit = string.Empty;
                    string methodNO = string.Empty;//�����ڲ���ⷽ�����
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
                        datetime = StringUtil.HexStr2DateTimeString(dataRead.Substring(32, 8));
                    }
                    if (command.Equals("65") || command.Equals("75"))
                    {
                        datetime = StringUtil.HexStr2DateTimeString(dataRead.Substring(28, 8));
                    }
                    string num = string.Empty;
                    string checkValue = string.Empty;   //���ֵ
                    string error = "��";    // �����Ƿ����
                    string partOutcome = string.Empty;
                    string tempStr = dataRead.Substring(48);
                    int byteCount = 16;    //���ݴ�����(8���ֽ�)
                    int recordCount = tempStr.Length / byteCount;
                    int p = 0;
                    int channelNO = 0;
                    if (command.Equals("45"))
                    {
                        for (int i = 0; i < recordCount; i++)
                        {
                            channelNO = Convert.ToByte(dataRead.Substring(40 + i * 16, 2), 16);
                            p = Convert.ToByte(dataRead.Substring(42 + i * 16, 2), 16);
                            //if (p >= 128 && channelNO > 0)   // if ( ctrlChannel > 0) //p���ڵ���128������Ч,k=0�����Ƕ���ͨ��
                            if (channelNO > 0)
                            {
                                error = "��";
                                if (p >= 192)
                                {
                                    error = "��";       //˵�����ݿ���
                                }
                                num = channelNO.ToString();

                                if (methodNO.Equals("0")) //Ϊ�����ʷ�
                                {
                                    partOutcome = dataRead.Substring(48 + i * 16, 4).ToUpper();
                                    if (partOutcome.Equals("FF7F"))//��Ч����
                                    {
                                        checkValue = "---.-";
                                    }
                                    else if (partOutcome.Equals("FE7F"))
                                    {
                                        checkValue = "NA";
                                    }
                                    else
                                    {
                                        checkValue = StringUtil.HexString2Float2String(partOutcome);
                                    }
                                }
                                else //��������
                                {
                                    partOutcome = dataRead.Substring(48 + i * 16, 8).ToUpper();
                                    if (partOutcome.Equals("FFFFFF7F") || partOutcome.Equals("FEFFFF7F") || partOutcome.Equals("FF7F0000") || partOutcome.Equals("2C420F00"))    //��Ч����
                                    {
                                        checkValue = "-.---";
                                    }
                                    else if (partOutcome.Equals("FDFFFF7F") || partOutcome.Equals("FE7F0000"))    //�ɻ�������
                                    {
                                        checkValue = "NA";
                                    }
                                    else
                                    {
                                        checkValue = StringUtil.HexString2Float4String(partOutcome);
                                    }
                                }
                                if (!(checkValue.Equals("NA") || checkValue.Equals("-.---") || checkValue.Equals("---.-")))
                                {
                                    AddNewHistoricData(num, item, checkValue, unit, datetime, error);
                                }
                            }
                        }
                    }
                    if (command.Equals("65") || command.Equals("75"))
                    {
                        string OUTnum = dataRead.Substring(36, 4).ToUpper();
                        double NOE = Convert.ToDouble(StringUtil.HexString2Float2String(OUTnum));
                        num = (NOE * 10).ToString();
                        byte[] TempArry = StringUtil.HexString2ByteArray(dataRead.Substring(24));
                        string o = dataRead.Substring(24);
                        if (command.Equals("65"))
                        {
                            switch (TempArry[8] >> 6 & 0x03)
                            {  //��2λ��ʾ��ⷽ��
                                case 0:
                                case 1: switch (TempArry[8] & 0x03)
                                    {
                                        case 1: checkValue = "����";
                                            break;
                                        case 2: checkValue = "����";
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
                                    checkValue = StringUtil.HexString2Float4String(partOutcome);
                                    break;
                            }
                        }
                        if (command.Equals("75"))
                        {
                            switch (TempArry[8] >> 6 & 0x03)
                            {  //��2λ��ʾ��ⷽ��
                                case 0: switch (TempArry[8] & 0x03)
                                    {
                                        case 1: checkValue = "����";
                                            break;
                                        case 2: checkValue = "����";
                                            break;
                                        case 3: checkValue = "NA";
                                            break;
                                        default: checkValue = "NA";
                                            break;
                                    }; break;

                                case 2: partOutcome = dataRead.Substring(42, 12).ToUpper();
                                    checkValue = StringUtil.HexString2Float4String(partOutcome); break;
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
                    if (FoodClientLib.MessageNotification.GetInstance() != null)
                    {
                        FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadDY3000DYData, "");
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
            //�����
        }

        private void ReadRecords(int input)
        {
            uint little = StringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF4500" + parameter + "0000";
            string checkSum = StringUtil.CheckDataSum(header);
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadRecords1(int input)
        {
            uint little = StringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF6500" + parameter + "0000";
            string checkSum = StringUtil.CheckDataSum(header);
            string o = header + checkSum + "00";
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadRecords2(int input)
        {
            uint little = StringUtil.ToLittleEndian((uint)input);
            string parameter = little.ToString("X8");
            string header = "03FF7500" + parameter + "0000";
            string checkSum = StringUtil.CheckDataSum(header);
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadItems(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF420000" + parameter + "00000000";
            string checkSum = StringUtil.CheckDataSum(header);//�����
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        /// <summary>
        /// DY-6600��Ŀ��ȡ
        /// </summary>
        /// <param name="input"></param>
        private void ReadItems1(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF620000" + parameter + "00000000";
            string checkSum = StringUtil.CheckDataSum(header);//�����
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void ReadItems2(byte input)
        {
            string parameter = input.ToString("X2");
            string header = "03FF720000" + parameter + "00000000";
            string checkSum = StringUtil.CheckDataSum(header);//�����
            byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
            Send(dataSent);
        }

        private void AddNewHistoricData(string num, string item, string checkValue, string unit, string time, string p)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["�ѱ���"] = false;
            dr["���"] = num;
            dr["�����Ŀ"] = item;
            dr["������"] = checkValue;
            dr["��λ"] = unit;//���ֵ
            dr["���ʱ��"] = time;
            dr["���ݿ�����"] = p;
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

    }
}