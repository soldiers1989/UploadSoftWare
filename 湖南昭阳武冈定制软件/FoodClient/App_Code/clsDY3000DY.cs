using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JH.CommBase;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsDY3000DY ���ڲ�����
	/// ReadItem��readItems�����Ƿ����ȥ�����Ż�?
	/// </summary>
	public class clsDY3000DY : CommBase
	{
		public DataTable DataReadTable = null;//ȥ��Static
		private bool m_IsCreatedDataTable = false;
		private byte m_ItemID = 0;  //��doWith��ReadItem�����г���
		private int m_DataReadByteCount = 0;         //ֻ��doWith��OnRxChar�����г���,Ӧ�ÿ���ͨ���ֲ�������ʵ��
		private int m_LatterDataByteCount = 0;  //ֻ��doWith��OnRxChar�����г���,Ӧ�ÿ���ͨ���ֲ�������ʵ��
		private int m_RecordStartPosition = 0;  //��ʼ��������,0,1,2,.....,ֻ��doWith�г���,Ӧ�ÿ���ͨ���ֲ�������ʵ��
		private int m_RecordEndPosition = 0;   //������������,����3������,recordEnd=3,ֻ��doWith�г���,Ӧ�ÿ���ͨ���ֲ�������ʵ��
		private int m_RecordCount = 0;  //��ȡ����������,ֻ��doWith�г���,Ӧ�ÿ���ͨ���ֲ�������ʵ��
		private string m_DataRead = string.Empty; //������������16��������
		private string m_ItemParts = string.Empty;
		/// <summary>
		/// �����Ŀ����
		/// </summary>
		public static string[,] CheckItemsArray;  //��Ӧ���ݿ��tMachine��LinkStdCode
		/// <summary>
		/// �����м����
		/// </summary>
		private StringBuilder sbTemp = new StringBuilder();    //�ƺ�û����(2012-08-23)
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsDY3000DY()
		{
			if (!m_IsCreatedDataTable)
			{
				DataReadTable = new DataTable("checkDtbl");//ȥ��Static
				DataColumn dataCol;
				///////////////////����
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
				dataCol.ColumnName = "��Ŀ";
				DataReadTable.Columns.Add(dataCol);


				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "���ֵ";
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

		/// <summary>
		/// ���ڲ�������
		/// </summary>
		/// <returns></returns>
		protected override CommBaseSettings CommSettings()
		{
			CommBaseSettings cs = new CommBaseSettings();
			cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
			return cs;
		}

		/// <summary>
		/// ��ȡ��ʷ����,�����ʱ������һ����֤dtStart<=dtEnd
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		public void ReadHistory(DateTime startDate, DateTime endDate)
		{
			//head = "03FF4300000000000800B7"�Ľ�������:
			//StringUtil.CheckDataSum("03FF4300000000000800")����B7
			//03:����ʼ��־   FF:������(PC���豸)   4300:���ݰ�����(С��ģʽ------���ֽ���ǰ�����ֽ��ں�)
			//00000000:�������    0800:���ݳ���(С��ģʽ ��������ʱ���4�ֽ�,����8���ֽ�)
			//B7��ͷУ���
			string header = "03FF4300000000000800B7";//��ʶͷ
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
				resultEndDate = tempEndDate;
			resultEndDate = resultEndDate | 0xFFFF;

			resultStartDate = StringUtil.ToLittleEndian(resultStartDate);//ת��ΪС��ģʽ
			resultEndDate = StringUtil.ToLittleEndian(resultEndDate);
			string start = resultStartDate.ToString("X8");// 8λ16����
			string end = resultEndDate.ToString("X8");
			string checkSum = StringUtil.CheckDataSum(start + end);//Э�����ᵽ��[���ݲ���У���]
			byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + start + end);   //start + end��Э�����ᵽ��[����]
			
			Send(dataSent);
		}


		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="c"></param>
		protected override void OnRxChar(byte c)
		{
			string currentByte = c.ToString("X2");    //ת����2λ��16����
			m_DataRead += currentByte;
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
							if (FoodClientLib.MessageNotify.Instance() != null)
								FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYItem, m_ItemParts);
						}
					}
					m_DataReadByteCount = 0;
					m_DataRead = string.Empty;
					m_LatterDataByteCount = 0;
				}
			}
		}


		private void ParseDataFromDevice(string dataRead)
		{
			string command = dataRead.Substring(4, 2);
			string temp = string.Empty;
			if (command.Equals("42")) //����Ƕ�ȡ�����Ŀ
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

				m_ItemParts += "{" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";

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

					m_ItemParts += "{" + chineseItemName.Trim() + ":-1:" + checkValue.ToString() + ":" + chineseUnit.Trim() + "}";
					
					m_ItemID++;
					ReadItems(m_ItemID);
				}
				else //���ݲ�����
				{
					m_DataReadByteCount = 0;
					m_DataRead = string.Empty;
					m_LatterDataByteCount = 0;

					if (FoodClientLib.MessageNotify.Instance() != null)
						FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYItem, m_ItemParts);
				}
			}
			else if (command.Equals("43")) //�������¼����,��ȡ�����Ŀ����Ҫ�õ��˺���
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
					ReadRecords(m_RecordStartPosition);
				else
				{
					m_RecordCount = 0;
					m_RecordStartPosition = 0;
					m_RecordEndPosition = 0;
					m_DataReadByteCount = 0;
					m_DataRead = string.Empty;
					m_LatterDataByteCount = 0;


					if (DataReadTable.Rows.Count <= 0)
						MessageBox.Show("û����Ӧ�����ļ�����ݡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

					if (FoodClientLib.MessageNotify.Instance() != null)
						FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYData, "");
				}
			}
			else if (command.Equals("45")) //�������ļ�¼����
			{
				if (!dataRead.Substring(24, 2).Equals("00"))
				{
					int itemID = Convert.ToByte(dataRead.Substring(26, 2), 16);   //�����ϵ���Ŀ���
					string item = string.Empty;
					string unit = string.Empty;
					string methodNO = string.Empty;//�����ڲ���ⷽ�����

					if (itemID >= 0 && itemID <= CheckItemsArray.GetLength(0) - 1)
					{
						item = CheckItemsArray[itemID, 0];
						unit = CheckItemsArray[itemID, 3];
						methodNO = CheckItemsArray[itemID, 2];
					}
					string datetime = StringUtil.HexStr2DateTimeString(dataRead.Substring(32, 8));
					string num = string.Empty;
					string checkValue = string.Empty;   //���ֵ
					string error = "��";    // �����Ƿ����
					string partOutcome = string.Empty;
					string tempStr = dataRead.Substring(48);
					int byteCount = 16;    //���ݴ�����(�ֽ�)
					int recordCount = tempStr.Length / byteCount;
					int p = 0;
					int channelNO = 0;
					for (int i = 0; i < recordCount; i++)
					{
						p = Convert.ToByte(dataRead.Substring(42 + i * 16, 2), 16);
						channelNO = Convert.ToByte(dataRead.Substring(40 + i * 16, 2), 16);
						
						if (channelNO > 0)   // if (p >= 128 && ctrlChannel > 0) //p���ڵ���128������Ч,k=0�����Ƕ���ͨ��
						{
							error = "��";
							if (p >= 192)
								error = "��";       //˵�����ݿ���
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
								if (partOutcome.Equals("FFFFFF7F") || partOutcome.Equals("FEFFFF7F"))    //��Ч����
								{
									checkValue = "-.---";
								}
								else if (partOutcome.Equals("FDFFFF7F"))    //�ɻ�������
								{
									checkValue = "NA";
								}
								else
								{
									checkValue = StringUtil.HexString2Float4String(partOutcome);
								}
							}
                            //2015��12��22�� wenjcheckValue="-.---"||"---.-"ʱ��Ҳ�ų�
                            if (checkValue.Equals("-.---")||checkValue.Equals("---.-"))
                                checkValue = "NA";
                            if (!checkValue.Equals("NA"))//jcz
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

					if (DataReadTable.Rows.Count <= 0)
					{
						MessageBox.Show("û����Ӧ�����ļ�����ݡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
					
					if (FoodClientLib.MessageNotify.Instance() != null)
					{
						FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYData, "");
					}
				}
				else
				{
					ReadRecords(endPosition);
				}
			}
		}


		/// <summary>
		/// ��ȡ��¼����.
		/// </summary>
		/// <param name="input"></param>
		private void ReadRecords(int input)
		{
			uint little = StringUtil.ToLittleEndian((uint)input);
			string parameter = little.ToString("X8");
			string header = "03FF4500" + parameter + "0000";
			string checkSum = StringUtil.CheckDataSum(header);

			byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
			Send(dataSent);
		}


		/// <summary>
		/// ��ȡ�����Ŀ
		/// �ܴ���ת��תȥ�����˷�ʱ�䣬��Ҫ�Ż�
		/// ֱ��ת��Ϊbyte[]��OK��
		/// </summary>
		/// <param name="i"></param>
		private void ReadItems(byte input)
		{
			string parameter = input.ToString("X2");
			string header = "03FF420000" + parameter + "00000000";
			string checkSum = StringUtil.CheckDataSum(header);//�����
			
			byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
			Send(dataSent);
		}




		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="num">���</param>
		/// <param name="item">��Ŀ</param>
		/// <param name="checkValue">���ֵ</param>
		/// <param name="unit">��λ</param>
		/// <param name="time">����</param>
		/// <param name="p">���ݿ�����</param>
		private void AddNewHistoricData(string num, string item, string checkValue, string unit, string time, string p)
		{
			DataRow dr;
			dr = DataReadTable.NewRow();
			dr["�ѱ���"] = false;
			dr["���"] = num;
			dr["��Ŀ"] = item;
			dr["���ֵ"] = checkValue;
			dr["��λ"] = unit;//���ֵ
			dr["���ʱ��"] = time;
			dr["���ݿ�����"] = p;
			DataReadTable.Rows.Add(dr);
		}








		/// <summary>
		/// ��ȡ�����Ŀ
		/// </summary>
		public void ReadItem()
		{
			m_ItemParts = string.Empty;
			m_ItemID = 0;
			ReadItems(m_ItemID);
		}








		/// <summary>
		/// �������     [�˷����ƺ�û���õ�(2012-08-23)]
		/// </summary>
		private void ClearData()
		{
			sbTemp.Length = 0;
			m_DataRead = string.Empty;
			//if (checkDtbl != null)
			//{
			//    checkDtbl.Clear();
			//    FoodClient.frmMain.frmAutoDYSeries.ShowResult(checkDtbl);
			//}
		}

		#region ע�͵�
		///// <summary>
		///// ת��С��ģʽ
		///// </summary>
		///// <param name="bigEndian"></param>
		///// <returns></returns>
		//private uint ToLittleEndian(uint bigEndian)
		//{
		//    byte[] bt = BitConverter.GetBytes(bigEndian);
		//    return (uint)(bt[0] << 24) + (uint)(bt[1] << 16) + (uint)(bt[2] << 8) + (uint)(bt[3]);
		//}
		///// <summary>
		///// ��ʮ�������ַ���ת���ֽ�����
		///// �޸��ߣ��¹���
		///// �޸�ʱ�䣺2011-10-21
		///// </summary>
		///// <param name="input"></param>
		///// <returns></returns>
		//private byte[] HexString2ByteArray(string input)
		//{
		//    Match m = Regex.Match(input, "[^0-9a-fA-F]");
		//    if (m.Success || input.Length % 2 != 0)
		//    {
		//        byte[] b1 = new byte[32];
		//        return b1;
		//    }
		//    else
		//    {
		//        string temp = string.Empty;
		//        int len = input.Length / 2;
		//        byte[] bt = new byte[len];
		//        for (int i = 0; i < len; i++)
		//        {
		//            temp = input.Substring(i * 2, 2);
		//            bt[i] = Convert.ToByte(temp, 16);
		//        }
		//        return bt;
		//    }
		//}


		///// <summary>
		///// ��16����ת��Ϊ����ʱ���ַ���
		///// ���ִ���ʽ̫�ֲ��ˡ�
		///// �˺����ڲ��Ѿ��޸�
		///// �޸��ߣ��¹��� 2011-10-21
		///// </summary>
		///// <param name="input"></param>
		///// <returns></returns>
		//private string HexStr2DateTimeString(string input)
		//{
		//    Match match = Regex.Match(input, "[^0-9a-fA-F]");
		//    if (match.Success || input.Length % 2 != 0)
		//    {
		//        return "00";
		//    }
		//    else
		//    {
		//        /* 
		//         * �ֲܿ���ת������,�Ѿ��޸Ĵ˷���
		//         * int ath = byte.Parse(s.Substring(0, 2), NumberStyles.AllowHexSpecifier);
		//         int atl = byte.Parse(s.Substring(2, 2), NumberStyles.AllowHexSpecifier);
		//         string stimel = Convert.ToInt64(Convert.ToString(atl, 2)).ToString("D8");
		//         string stimeh = Convert.ToInt64(Convert.ToString(ath, 2)).ToString("D8");
		//         string stime = stimel + stimeh;
		//         string ss = Convert.ToString(Convert.ToInt64(stime.Substring(11, 5), 2) * 2);
		//         string M = Convert.ToString(Convert.ToInt64(stime.Substring(5, 6), 2));
		//         string h = Convert.ToString(Convert.ToInt64(stime.Substring(0, 5), 2));

		//         int adh = byte.Parse(s.Substring(4, 2), NumberStyles.AllowHexSpecifier);
		//         int adl = byte.Parse(s.Substring(6, 2), NumberStyles.AllowHexSpecifier);
		//         string sdatel = Convert.ToInt64(Convert.ToString(adl, 2)).ToString("D8");
		//         string sdateh = Convert.ToInt64(Convert.ToString(adh, 2)).ToString("D8");
		//         string sdate = sdatel + sdateh;
		//         string d = Convert.ToString(Convert.ToInt64(sdate.Substring(11, 5), 2));
		//         string mm = Convert.ToString(Convert.ToInt64(sdate.Substring(7, 4), 2));
		//         string y = Convert.ToString(Convert.ToInt64(sdate.Substring(0, 7), 2));
		//         string sdatetime = "20" + y + "-" + mm + "-" + d + " " + h + ":" + M + ":" + ss;
		//         try
		//         {
		//             DateTime dt = Convert.ToDateTime(sdatetime);
		//         }
		//         catch
		//         {
		//         }
		//         return sdatetime;
		//        */

		//        sbTemp.Length = 0;
		//        uint utDate;
		//        string temp = string.Empty;
		//        byte[] by = new byte[4];
		//        int len = input.Length / 2;

		//        for (int i = 0; i < len; i++)
		//        {
		//            temp = input.Substring(i * 2, 2);
		//            by[i] = Convert.ToByte(temp, 16);
		//        }
		//        utDate = (uint)(by[0] | (by[1] << 8) | (by[2] << 16) | (by[3] << 24));
		//        sbTemp.Append(((utDate >> 25) & 0x7f));//year
		//        sbTemp.Append("-");
		//        sbTemp.Append(((utDate >> 21) & 0x0f));//month
		//        sbTemp.Append("-");
		//        sbTemp.Append(((utDate >> 16) & 0x1f));//date
		//        sbTemp.Append(" ");
		//        sbTemp.Append(((utDate >> 11) & 0x1f));//hour
		//        sbTemp.Append(":");
		//        sbTemp.Append(((utDate >> 5) & 0x3f));//minute
		//        sbTemp.Append(":");
		//        sbTemp.Append(((utDate << 1) & 0x3f));//second

		//        string ret = Convert.ToDateTime(sbTemp.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
		//        sbTemp.Length = 0;
		//        return ret;
		//    }
		//}

		// <summary>
		// �����
		// </summary>
		// <param name="input"></param>
		// <returns></returns>
		//private string CheckDataSum(string input)
		//{
		//    Match match = Regex.Match(input, "[^0-9a-fA-F]");
		//    if (match.Success || input.Length % 2 != 0)
		//    {
		//        return "00";
		//    }
		//    else
		//    {
		//        string temp = string.Empty;
		//        int len = input.Length / 2;
		//        byte bt1 = 0;
		//        byte bt2 = 0;
		//        for (int i = 0; i < len; i++)
		//        {
		//            temp = input.Substring(i * 2, 2);
		//            bt2 = Convert.ToByte(temp, 16);
		//            bt1 =  Convert.ToByte(bt1 ^ bt2);
		//        }
		//        return bt1.ToString("X2");
		//    }
		//}

		///// <summary>
		///// ���˿��ַ�
		///// </summary>
		///// <param name="input"></param>
		///// <returns></returns>
		//private string HexStringTrim(string input)
		//{
		//    int len = input.Length / 2;
		//    string temp = string.Empty;
		//    sbTemp.Length = 0;
		//    for (int i = 0; i < len; i++)
		//    {
		//        temp = input.Substring(i * 2, 2);
		//        if (temp.Equals("00"))
		//        {
		//            return sbTemp.ToString();
		//        }
		//        sbTemp.Append(temp);
		//    }
		//    temp = sbTemp.ToString();
		//    sbTemp.Length = 0;
		//    return temp;
		//}
		///// <summary>
		///// ��ʮ�������ַ���ת��Ϊ��λ��������
		///// </summary>
		///// <param name="input"></param>
		///// <returns></returns>
		//private string HexString2Float2String(string input)
		//{
		//    int i2 = 0;
		//    i2 = Convert.ToByte(input.Substring(2, 2), 16) << 8;
		//    i2 += Convert.ToByte(input.Substring(0, 2), 16);
		//    string temp = string.Empty;
		//    temp = ((float)i2 / 10).ToString();
		//    return temp;
		//}

		///// <summary>
		///// ��ʮ�������ַ���ת��Ϊ��λ��������
		///// </summary>
		///// <param name="input"></param>
		///// <returns></returns>
		//private string HexString2Float4String(string input)
		//{
		//    int iflag = 1;
		//    int ret = 0;
		//    ret = Convert.ToByte(input.Substring(6, 2), 16);

		//    //t1=byte.Parse(input.Substring(6, 2), NumberStyles.AllowHexSpecifier);
		//    if (ret >= 128)
		//    {
		//        iflag = -1;
		//        //t1 = (t1 - 128) * 256 * 256 * 256;
		//        ret = (ret - 128) << 24;
		//    }
		//    //ret += byte.Parse(input.Substring(4, 2), NumberStyles.AllowHexSpecifier) * 256 * 256;
		//    //ret += byte.Parse(input.Substring(2, 2), NumberStyles.AllowHexSpecifier) * 256;
		//    //ret += byte.Parse(input.Substring(0, 2), NumberStyles.AllowHexSpecifier);
		//    ret += Convert.ToByte(input.Substring(4, 2), 16) << 16;
		//    ret += Convert.ToByte(input.Substring(2, 2), 16) << 8;
		//    ret += Convert.ToByte(input.Substring(0, 2), 16);

		//    float fRet = (float)ret / 1000 * iflag;
		//    return fRet.ToString();
		//}


		///// <summary>
		///// ��ʱ��ת��16�������ַ���
		///// ���������Ҫ�޸�
		//// ����ʱ���ʽ�������Ѿ��޸��ˡ��˺�������ʹ��
		//// �޸��ߣ��¹��� 2011-10-21
		///// </summary>
		///// <param name="dt"></param>
		///// <returns></returns>
		//private string DateTime2HexString(DateTime dt)
		//{
		//    //9-15	��(ֻ����0-99����) 7λ
		//    int y = Convert.ToInt16(dt.ToString("yy"));
		//    long y2 = Convert.ToInt64(Convert.ToString(y, 2));

		//    //5-8	�� 4λ
		//    int m = Convert.ToInt16(dt.ToString("MM"));
		//    long m2 = Convert.ToInt64(Convert.ToString(m, 2));

		//    //0-4	�� 5λ
		//    int d = Convert.ToInt16(dt.ToString("dd"));
		//    long d2 = Convert.ToInt64(Convert.ToString(d, 2));
		//    string sdate = Convert.ToInt16(y2).ToString("D7") + Convert.ToInt16(m2).ToString("D4") + Convert.ToInt16(d2).ToString("D5");
		//    long sdl = Convert.ToInt64(sdate.Substring(0, 8), 2);
		//    long sdh = Convert.ToInt64(sdate.Substring(8, 8), 2);
		//    string sdatel = sdl.ToString("X2");
		//    string sdateh = sdh.ToString("X2");

		//    //11-15	ʱ(00-23) 5λ
		//    int h = Convert.ToInt16(dt.ToString("HH"));
		//    long h2 = Convert.ToInt64(Convert.ToString(h, 2));

		//    //5-10	��(00-59) 6λ
		//    int M = Convert.ToInt16(dt.ToString("mm"));
		//    long M2 = Convert.ToInt64(Convert.ToString(M, 2));
		//    //0-4	��/2��ʱ�侫��Ϊ2�롣

		//    string stime = Convert.ToInt16(h2).ToString("D5") + Convert.ToInt16(M2).ToString("D6") + "00000";
		//    long stl = Convert.ToInt64(stime.Substring(0, 8), 2);
		//    long sth = Convert.ToInt64(stime.Substring(8, 8), 2);
		//    string stimel = stl.ToString("X2");
		//    string stimeh = sth.ToString("X2");
		//    return stimeh + stimel + sdateh + sdatel;

		//}
		#endregion
	}
}
