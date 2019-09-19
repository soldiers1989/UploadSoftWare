using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JH.CommBase;

namespace DY.FoodClientLib
{
    public class clsLZ4000T : CommBase
    {

        #region �Զ������
        private bool m_IsCreatedDataTable = false;
        private string N_DataTemp42 = string.Empty;
        private string N_DataTemp62 = string.Empty;
        private string N_DataTemp72 = string.Empty;
        private string m_DataRead = string.Empty;
        private string m_ItemParts = string.Empty;
        private byte[] bt = new byte[40960];
        private string strShow = string.Empty;
        private int intRec = 0;
        public static string[,] CheckItemsArray;
        public static string[,] CheckItemsArray62;
        public static string[,] CheckItemsArray72;
        public DataTable DataReadTable = null;
        public string DY660;
        #endregion

        public clsLZ4000T()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");//ȥ��Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "�ѱ���";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(int);//int,string
                dataCol.ColumnName = "���";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "�����Ŀ";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "��Ʒ����";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "������";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "��λ";//���ֵ
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
            cs.SetStandard(ShareOption.ComPort, 115200, Handshake.none, Parity.none);
            return cs;
        }


        protected override void OnRxChar(byte c)
        {
            string rec = c.ToString("X2");
            //if ((!rec.Equals("7e") || !rec.Equals("7E")) && strShow.Length == 0)
            //    return;
            strShow += rec;
            //ȷ�����ζ�ȡ������������
            if (strShow.Length > 8 && strShow.Length <= 12)
            {
                if (strShow.Length == 10)
                    intRec = c;
                else
                    intRec += c * 256;
            }
            if (strShow.Length == intRec * 52 && intRec > 0)
            {
                ParseDataFromDevice(strShow);
                intRec = 0;
                strShow = string.Empty;
            }
            m_DataRead = string.Empty;
        }

        /// <summary>
        /// ��ȡ��ʷ����,�����ʱ������һ����֤
        /// LZ-4000T��ȡ�豸�������
        /// request:(FLAG)0x7E (CMD)0x10 (LEN)0x0003 (DATA)year month day CRC8 (FLAG)0xAA
        /// </summary>
        /// <param name="dataTime"></param>
        public void ReadHistory(DateTime dataTime)
        {
            try
            {
                string head = "7E100300", strTime = (dataTime.Year - 2000).ToString("X2") + dataTime.Month.ToString("X2") + dataTime.Day.ToString("X2");
                byte checkSum = crc8(StringUtil.HexString2ByteArray("100300" + strTime));
                string crc = checkSum.ToString("X2");
                byte[] sendData = StringUtil.HexString2ByteArray(head + strTime + crc + "AA");
                Send(sendData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// crc���ʱ�׼
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
        /// CRC8��У��
        /// time��2016��6��1��
        /// ���ߣ�wenj
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

        /// <summary>
        /// ��֤�����Ƿ���Ч
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] checkData(byte[] data)
        {
            byte[] rtnData = null;
            int dataCount = 0;
            for (int i = 0; i < data.Length; i++)
            {
                //��֤��ʶͷ��CMD
                if (data[i] == 0x7E && data[i + 1] == 0x11)
                {
                    //��ȡ�ܼ�¼��
                    dataCount = data[i + 5] > 0x00 ? data[i + 5] * 256 + data[i + 4] : data[i + 4];
                    if (dataCount > 0 && data[i + (dataCount * 26) - 1] == 0xAA)
                    {
                        if (i == 0 && data[data.Length - 1] == 0xAA)
                        {
                            rtnData = new byte[dataCount * 26];
                            Array.Copy(data, rtnData, dataCount * 26);
                            return rtnData;
                        }
                    }
                    else
                    {
                        data = Remove(data);
                        i = -1;
                    }
                }
                else
                {
                    data = Remove(data);
                    i = -1;
                }
            }
            return rtnData;
        }

        /// <summary>
        /// �Ƴ��������±�0��ֵ
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte[] Remove(byte[] array)
        {
            int length = array.Length;
            byte[] result = new byte[length - 1];
            Array.Copy(array, result, 0);
            Array.Copy(array, 1, result, 0, length - 1);
            return result;
        }

        private void ParseDataFromDevice(string input)
        {
            string scmd = input.Substring(8, 4);
            string temp = string.Empty;
            byte[] data = StringUtil.HexString2ByteArray(input);
            data = checkData(data);
            List<byte[]> dataList = new List<byte[]>();
            int dtIndex = 0;
            byte[] dt = new byte[26];
            for (int j = 0; j < data.Length; j++)
            {
                dt[dtIndex] = data[j];
                if (dtIndex > 24)
                {
                    dataList.Add(dt);
                    dt = new byte[26];
                    dtIndex = -1;
                }
                dtIndex++;
            }
            string item = string.Empty;
            string product = string.Empty;
            string unit = string.Empty;
            string xmff = string.Empty;
            string num = string.Empty;
            string chValue = string.Empty;
            string sjky = "��";
            string datetime = string.Empty;
            byte[] btname = new byte[6];
            for (int i = 0; i < dataList.Count; i++)
            {
                datetime = "20" + dataList[i][16].ToString("D2") + "-" + dataList[i][17].ToString("D2") + "-" + dataList[i][18].ToString("D2") + " "
                    + dataList[i][19].ToString("D2") + ":" + dataList[i][20].ToString("D2") + ":" + dataList[i][21].ToString("D2");
                num = (dataList[i][8] + dataList[i][9] * 256).ToString("D4");
                item = CheckItemsArray[0, 0];
                btname[0] = dataList[i][10];
                btname[1] = dataList[i][11];
                btname[2] = dataList[i][12];
                btname[3] = dataList[i][13];
                btname[4] = dataList[i][14];
                btname[5] = dataList[i][15];
                product = Encoding.Default.GetString(btname);
                unit = CheckItemsArray[0, 3];
                xmff = CheckItemsArray[0, 2];
                chValue = ((double)(dataList[i][22] + dataList[i][23] * 256) / 100).ToString("F2");
                AddNewHistoricData(num, item, product, chValue, unit, datetime, sjky);
            }
            if (FoodClientLib.MessageNotification.GetInstance() != null)
            {
                FoodClientLib.MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
            }
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

        private void AddNewHistoricData(string num, string item, string product, string checkValue, string unit, string time, string p)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["�ѱ���"] = false;
            dr["���"] = num;
            dr["�����Ŀ"] = item;
            dr["��Ʒ����"] = product;
            dr["������"] = checkValue;
            dr["��λ"] = unit;//���ֵ
            dr["���ʱ��"] = time;
            dr["���ݿ�����"] = p;
            DataReadTable.Rows.Add(dr);
        }

    }
}