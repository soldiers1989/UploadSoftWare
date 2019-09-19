using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JH.CommBase;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsDY3000DY 串口操作类
	/// ReadItem与readItems方法是否可以去掉或优化?
	/// </summary>
	public class clsDY3000DY : CommBase
	{
		public DataTable DataReadTable = null;//去掉Static
		private bool m_IsCreatedDataTable = false;
		private byte m_ItemID = 0;  //在doWith与ReadItem方法中出现
		private int m_DataReadByteCount = 0;         //只在doWith与OnRxChar方法中出现,应该可以通过局部变量来实现
		private int m_LatterDataByteCount = 0;  //只在doWith与OnRxChar方法中出现,应该可以通过局部变量来实现
		private int m_RecordStartPosition = 0;  //起始数据索引,0,1,2,.....,只在doWith中出现,应该可以通过局部变量来实现
		private int m_RecordEndPosition = 0;   //结束数据索引,若有3条数据,recordEnd=3,只在doWith中出现,应该可以通过局部变量来实现
		private int m_RecordCount = 0;  //读取的数据条数,只在doWith中出现,应该可以通过局部变量来实现
		private string m_DataRead = string.Empty; //从仪器读到的16进制数据
		private string m_ItemParts = string.Empty;
		/// <summary>
		/// 检测项目数组
		/// </summary>
		public static string[,] CheckItemsArray;  //对应数据库表tMachine中LinkStdCode
		/// <summary>
		/// 过程中间变量
		/// </summary>
		private StringBuilder sbTemp = new StringBuilder();    //似乎没有用(2012-08-23)
		/// <summary>
		/// 构造函数
		/// </summary>
		public clsDY3000DY()
		{
			if (!m_IsCreatedDataTable)
			{
				DataReadTable = new DataTable("checkDtbl");//去掉Static
				DataColumn dataCol;
				///////////////////新增
				dataCol = new DataColumn();
				dataCol.DataType = typeof(bool);
				dataCol.ColumnName = "已保存";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(int);//int,string
				dataCol.ColumnName = "编号";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "项目";
				DataReadTable.Columns.Add(dataCol);


				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "检测值";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "单位";//检测值
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "检测时间";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "数据可疑性";
				DataReadTable.Columns.Add(dataCol);
				m_IsCreatedDataTable = true;
			}
		}

		/// <summary>
		/// 串口参数设置
		/// </summary>
		/// <returns></returns>
		protected override CommBaseSettings CommSettings()
		{
			CommBaseSettings cs = new CommBaseSettings();
			cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
			return cs;
		}

		/// <summary>
		/// 读取历史数据,传入的时间数据一定保证dtStart<=dtEnd
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		public void ReadHistory(DateTime startDate, DateTime endDate)
		{
			//head = "03FF4300000000000800B7"的解释如下:
			//StringUtil.CheckDataSum("03FF4300000000000800")返回B7
			//03:包起始标志   FF:包方向(PC到设备)   4300:数据包命令(小端模式------低字节在前，高字节在后)
			//00000000:命令参数    0800:数据长度(小端模式 起讫日期时间各4字节,所以8个字节)
			//B7包头校验和
			string header = "03FF4300000000000800B7";//标识头
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
				resultEndDate = tempEndDate;
			resultEndDate = resultEndDate | 0xFFFF;

			resultStartDate = StringUtil.ToLittleEndian(resultStartDate);//转化为小端模式
			resultEndDate = StringUtil.ToLittleEndian(resultEndDate);
			string start = resultStartDate.ToString("X8");// 8位16进制
			string end = resultEndDate.ToString("X8");
			string checkSum = StringUtil.CheckDataSum(start + end);//协议中提到的[数据部分校验和]
			byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + start + end);   //start + end是协议中提到的[数据]
			
			Send(dataSent);
		}


		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="c"></param>
		protected override void OnRxChar(byte c)
		{
			string currentByte = c.ToString("X2");    //转换成2位的16进制
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
				//再次判断获得参数是否正确,不正确清空,正确确定后续数据的长度
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

					if (m_LatterDataByteCount == 0)//如果检测项目
					{
						int tlen = m_DataRead.Length;
						if (tlen >= 6 && m_DataRead.Substring(4, 2).Equals("42")) //表明检测项目读完
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
			if (command.Equals("42")) //如果是读取检测项目
			{
				Encoding gb2312 = Encoding.GetEncoding("gb2312");

				temp = dataRead.Substring(24, 32);
				byte[] itemHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
				string chineseItemName = gb2312.GetString(itemHexArray);//项目名称

				temp = dataRead.Substring(56, 16);
				byte[] unitHexArray = StringUtil.HexString2ByteArray(StringUtil.HexStringTrim(temp));
				string chineseUnit = gb2312.GetString(unitHexArray);//单位

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
				else //数据不完整
				{
					m_DataReadByteCount = 0;
					m_DataRead = string.Empty;
					m_LatterDataByteCount = 0;

					if (FoodClientLib.MessageNotify.Instance() != null)
						FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYItem, m_ItemParts);
				}
			}
			else if (command.Equals("43")) //如果读记录条数,读取检测项目不需要用到此函数
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
						MessageBox.Show("没有相应条件的检测数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					if (FoodClientLib.MessageNotify.Instance() != null)
						FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY3000DYData, "");
				}
			}
			else if (command.Equals("45")) //表明读的记录条数
			{
				if (!dataRead.Substring(24, 2).Equals("00"))
				{
					int itemID = Convert.ToByte(dataRead.Substring(26, 2), 16);   //仪器上的项目序号
					string item = string.Empty;
					string unit = string.Empty;
					string methodNO = string.Empty;//仪器内部检测方法编号

					if (itemID >= 0 && itemID <= CheckItemsArray.GetLength(0) - 1)
					{
						item = CheckItemsArray[itemID, 0];
						unit = CheckItemsArray[itemID, 3];
						methodNO = CheckItemsArray[itemID, 2];
					}
					string datetime = StringUtil.HexStr2DateTimeString(dataRead.Substring(32, 8));
					string num = string.Empty;
					string checkValue = string.Empty;   //检测值
					string error = "否";    // 数据是否可疑
					string partOutcome = string.Empty;
					string tempStr = dataRead.Substring(48);
					int byteCount = 16;    //数据串长度(字节)
					int recordCount = tempStr.Length / byteCount;
					int p = 0;
					int channelNO = 0;
					for (int i = 0; i < recordCount; i++)
					{
						p = Convert.ToByte(dataRead.Substring(42 + i * 16, 2), 16);
						channelNO = Convert.ToByte(dataRead.Substring(40 + i * 16, 2), 16);
						
						if (channelNO > 0)   // if (p >= 128 && ctrlChannel > 0) //p大于等于128表明有效,k=0表明是对照通道
						{
							error = "否";
							if (p >= 192)
								error = "是";       //说明数据可疑
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
									checkValue = StringUtil.HexString2Float2String(partOutcome);
								}
							}
							else //其他方法
							{
								partOutcome = dataRead.Substring(48 + i * 16, 8).ToUpper();    
								if (partOutcome.Equals("FFFFFF7F") || partOutcome.Equals("FEFFFF7F"))    //无效数据
								{
									checkValue = "-.---";
								}
								else if (partOutcome.Equals("FDFFFF7F"))    //可怀疑数据
								{
									checkValue = "NA";
								}
								else
								{
									checkValue = StringUtil.HexString2Float4String(partOutcome);
								}
							}
                            //2015年12月22日 wenjcheckValue="-.---"||"---.-"时，也排除
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
						MessageBox.Show("没有相应条件的检测数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
		/// 读取记录条数.
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
		/// 读取检测项目
		/// 很代码转来转去，很浪费时间，需要优化
		/// 直接转化为byte[]就OK了
		/// </summary>
		/// <param name="i"></param>
		private void ReadItems(byte input)
		{
			string parameter = input.ToString("X2");
			string header = "03FF420000" + parameter + "00000000";
			string checkSum = StringUtil.CheckDataSum(header);//检验和
			
			byte[] dataSent = StringUtil.HexString2ByteArray(header + checkSum + "00");
			Send(dataSent);
		}




		/// <summary>
		/// 构造一条数据
		/// </summary>
		/// <param name="num">编号</param>
		/// <param name="item">项目</param>
		/// <param name="checkValue">检测值</param>
		/// <param name="unit">单位</param>
		/// <param name="time">日期</param>
		/// <param name="p">数据可疑性</param>
		private void AddNewHistoricData(string num, string item, string checkValue, string unit, string time, string p)
		{
			DataRow dr;
			dr = DataReadTable.NewRow();
			dr["已保存"] = false;
			dr["编号"] = num;
			dr["项目"] = item;
			dr["检测值"] = checkValue;
			dr["单位"] = unit;//检测值
			dr["检测时间"] = time;
			dr["数据可疑性"] = p;
			DataReadTable.Rows.Add(dr);
		}








		/// <summary>
		/// 读取检测项目
		/// </summary>
		public void ReadItem()
		{
			m_ItemParts = string.Empty;
			m_ItemID = 0;
			ReadItems(m_ItemID);
		}








		/// <summary>
		/// 清空数据     [此方法似乎没有用到(2012-08-23)]
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

		#region 注释掉
		///// <summary>
		///// 转化小端模式
		///// </summary>
		///// <param name="bigEndian"></param>
		///// <returns></returns>
		//private uint ToLittleEndian(uint bigEndian)
		//{
		//    byte[] bt = BitConverter.GetBytes(bigEndian);
		//    return (uint)(bt[0] << 24) + (uint)(bt[1] << 16) + (uint)(bt[2] << 8) + (uint)(bt[3]);
		//}
		///// <summary>
		///// 把十六进制字符串转化字节数组
		///// 修改者：陈国利
		///// 修改时间：2011-10-21
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
		///// 把16进制转化为日期时间字符串
		///// 这种处理方式太恐怖了。
		///// 此函数内部已经修改
		///// 修改者：陈国利 2011-10-21
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
		//         * 很恐怖的转化过程,已经修改此方法
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
		// 检验和
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
		///// 过滤空字符
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
		///// 把十六进制字符串转化为两位浮点数据
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
		///// 把十六进制字符串转化为四位浮点数据
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
		///// 把时间转化16进度制字符串
		///// 这个函数需要修改
		//// 传入时间格式化问题已经修改了。此函数不再使用
		//// 修改者：陈国利 2011-10-21
		///// </summary>
		///// <param name="dt"></param>
		///// <returns></returns>
		//private string DateTime2HexString(DateTime dt)
		//{
		//    //9-15	年(只包含0-99部分) 7位
		//    int y = Convert.ToInt16(dt.ToString("yy"));
		//    long y2 = Convert.ToInt64(Convert.ToString(y, 2));

		//    //5-8	月 4位
		//    int m = Convert.ToInt16(dt.ToString("MM"));
		//    long m2 = Convert.ToInt64(Convert.ToString(m, 2));

		//    //0-4	日 5位
		//    int d = Convert.ToInt16(dt.ToString("dd"));
		//    long d2 = Convert.ToInt64(Convert.ToString(d, 2));
		//    string sdate = Convert.ToInt16(y2).ToString("D7") + Convert.ToInt16(m2).ToString("D4") + Convert.ToInt16(d2).ToString("D5");
		//    long sdl = Convert.ToInt64(sdate.Substring(0, 8), 2);
		//    long sdh = Convert.ToInt64(sdate.Substring(8, 8), 2);
		//    string sdatel = sdl.ToString("X2");
		//    string sdateh = sdh.ToString("X2");

		//    //11-15	时(00-23) 5位
		//    int h = Convert.ToInt16(dt.ToString("HH"));
		//    long h2 = Convert.ToInt64(Convert.ToString(h, 2));

		//    //5-10	分(00-59) 6位
		//    int M = Convert.ToInt16(dt.ToString("mm"));
		//    long M2 = Convert.ToInt64(Convert.ToString(M, 2));
		//    //0-4	秒/2，时间精度为2秒。

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
