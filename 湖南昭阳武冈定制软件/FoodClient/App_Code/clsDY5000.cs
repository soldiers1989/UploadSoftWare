using System;
using System.IO;
using System.Data;
using JH.CommBase;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.IO.Ports;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsDY5000 的摘要说明。
	/// </summary>
	public class clsDY5000 : CommBase
	{
		public DataTable DataReadTable = new DataTable("checkDtbl");
		private bool m_IsCreatedDataTable = false;

		//	private int m_DataReadByteCount = 0;
		private int m_DataReadByteCount = -1;
		private int m_SelfDectectionByteCount = 0;
		private bool m_IsSelfDectection = false;

		private string m_DataRead = string.Empty;
		private bool m_IsHistoric = false;
		private StringBuilder m_WhereBuilder = new StringBuilder();
		private readonly clsResultOpr m_ResultOperation = new clsResultOpr();
		private bool m_IsFirst = false;




		public clsDY5000()
		{
			if (!m_IsCreatedDataTable)
			{
				DataColumn dataCol;

				dataCol = new DataColumn();
				dataCol.DataType = typeof(bool);
				dataCol.ColumnName = "已保存";
				DataReadTable.Columns.Add(dataCol);


				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "孔位";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string); //System.Type.GetType("System.String");
				dataCol.ColumnName = "项目";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "吸光度";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "检测值";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string); //System.Type.GetType("System.String");
				dataCol.ColumnName = "单位";
				DataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string); //System.Type.GetType("System.String");
				dataCol.ColumnName = "检测时间";
				DataReadTable.Columns.Add(dataCol);
				m_IsCreatedDataTable = true;
			}
		}

		protected override CommBaseSettings CommSettings()
		{
			CommBaseSettings cs = new CommBaseSettings();

			//string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
			//FileInfo f = new FileInfo(path);
			//if (f.Exists)
			//{
			//    cs = CommBase.CommBaseSettings.LoadFromXML(f.OpenRead());
			//}
			//else
			//{
			cs.SetStandard(ShareOption.ComPort, 2400, Handshake.none, Parity.none);
			//}
			return cs;
		}





		public void ReadHistory(string startDate, string endDate)
		{
			//DataReadTable.Rows.Clear();

			string originalData = "s" + startDate + endDate;
			ASCIIEncoding asciiCode = new ASCIIEncoding();
			byte[] dataSent = asciiCode.GetBytes(originalData);

			m_IsHistoric = true;

			m_IsFirst = false;
			m_IsSelfDectection = false;
			m_SelfDectectionByteCount = 0;
			m_DataReadByteCount = -1;
			//SerialPort port = new SerialPort("COM1", 2400, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
			//port.Dispose();
			//port.Open();
			//port.DiscardInBuffer();
			//port.Close();

			//string[] ss = SerialPort.GetPortNames();
			//SerialPort port = new SerialPort("COM1", 2400, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
			//port.Open();
			//port.DiscardInBuffer();

			Send(dataSent);
		}


		protected override void OnRxChar(byte c)
		{
			//if (!m_IsHistoric)
			//{
			//    return;
			//}

			string currentChar = c.ToString("X2");
			string asciiChar = HexStr2AsciiStr(currentChar);
			m_SelfDectectionByteCount++;

			if (asciiChar == "s" && !m_IsFirst)
			{
				m_IsFirst = true;
				//	m_DataReadByteCount++;
				if (m_SelfDectectionByteCount - 1 > 1)
				{
					m_IsSelfDectection = true;
				}
				return;
			}
			if (m_IsSelfDectection)
			{
				return;
			}

			if (m_IsFirst)
			{
				if (asciiChar != "e")
				{
					m_DataRead += currentChar;
				}
				if (asciiChar == "e")
				{
					m_IsFirst = false;

					m_IsSelfDectection = false;
					m_SelfDectectionByteCount = 0;

					this.ParseDataFromDevice(m_DataRead);
					m_DataRead = string.Empty;
				}
				//m_DataReadByteCount++;
			}

			//if (m_DataReadByteCount == 708)
			//{
			//    m_DataReadByteCount = -1;
			//    m_IsFirst = false;

			//    m_IsSelfDectection = false;
			//    m_SelfDectectionByteCount = 0;

			//    this.ParseDataFromDevice(m_DataRead);
			//    m_DataRead = string.Empty;
			//}


			//int index;
			//string ss = string.Empty;
			//for (index = 0; index < 1420; index = index + 2)
			//{
			//    string temp = m_DataRead.Substring(index, 2);
			//    ss = HexStr2AsciiStr(temp);

			//    if (ss.ToLower() == "s")
			//    {
			//        break;
			//    }
			//}


			//string currentChar = c.ToString("X2");
			//m_DataRead += currentChar;
			//m_DataReadByteCount++;

			//if (m_IsHistoric)
			//{
			//    if (m_DataReadByteCount == 710)
			//    {
			//        m_DataReadByteCount = 0;
			//        m_IsHistoric = false;
			//        this.ParseDataFromDevice(m_DataRead.Substring(4, 1416));
			//        m_DataRead = string.Empty;
			//    }
			//}
			//else
			//{
			//    if (m_DataReadByteCount == 709)
			//    {
			//        m_DataReadByteCount = 0;
			//        this.ParseDataFromDevice(m_DataRead.Substring(2, 1416));
			//        m_DataRead = string.Empty;
			//    }
			//}
		}


		private void ParseDataFromDevice(string dataRead)
		{
			////单位
			//string hexCode = dataRead.Substring(1344, 20);
			//hexCode = HexStr2HexStrTrim(hexCode);
			//string unit = HexStr2AsciiStr(hexCode);


			////检测时间
			//hexCode = dataRead.Substring(1364, 12);
			//string checkDate = Hexstr2Datestr(hexCode);


			////项目名称
			//hexCode = dataRead.Substring(1376, 40);
			//hexCode = HexStr2HexStrTrim(hexCode);
			//byte[] itemCode = HexStr2ByteArray(hexCode);
			//Encoding gb2312 = Encoding.GetEncoding("gb2312");
			//string itemName = gb2312.GetString(itemCode);

			int dataLength = dataRead.Length;
			if (dataLength < 72)
			{
				return;
			}

			//单位
			string hexCode = dataRead.Substring(dataLength-72, 20);
			hexCode = HexStr2HexStrTrim(hexCode);
			string unit = HexStr2AsciiStr(hexCode);


			//检测时间
			hexCode = dataRead.Substring(dataLength - 72 + 20, 12);
			string checkDate = Hexstr2Datestr(hexCode);


			//项目名称
			hexCode = dataRead.Substring(dataLength - 72 + 20 + 12, 40);
			hexCode = HexStr2HexStrTrim(hexCode);
			byte[] itemCode = HexStr2ByteArray(hexCode);
			Encoding gb2312 = Encoding.GetEncoding("gb2312");
			string itemName = gb2312.GetString(itemCode);


			if ((dataLength - 72) % 22 != 0)
			{
				return;
			}

			int nunOfHole = (dataLength - 72) / 22;
			for(int index=0;index<nunOfHole;index++)
			{
				//hexCode = dataRead.Substring(index * 10 + 2, 2);
				//string holePosition = HexStr2AsciiStr(hexCode);
				//hexCode = dataRead.Substring(index * 10 + 4, 2);
				//string holeNo = HexStr2AsciiStr(hexCode);
				//holePosition = holePosition + (Convert.ToInt32(holeNo) + 1).ToString();

				string holePosition = string.Empty;
				byte holePositionHeader = 0x41;
				byte[] currenHole = { 0x00 };
				
				hexCode = dataRead.Substring(index * 10 + 2, 2);
				byte holeNum = Convert.ToByte(hexCode, 16);
				ASCIIEncoding asciiCode = new ASCIIEncoding();
				if (holeNum < 8)
				{
					currenHole[0] = Convert.ToByte(holePositionHeader + holeNum);
					holePosition = asciiCode.GetString(currenHole) + "1";
				}
				else
				{
					currenHole[0] = Convert.ToByte(holePositionHeader + holeNum % 8);
					holePosition = asciiCode.GetString(currenHole) + (holeNum/8 +1).ToString();
				}

				

				hexCode = dataRead.Substring(index * 10 + 4, 6);
				string absorbance = HexStr2Float3Str(hexCode);

				hexCode = dataRead.Substring(nunOfHole * 10 + index * 12 + 4, 8);
				string thickness = HexStr2Float4Str(hexCode);

				AddNewHistoricData(holePosition, itemName, absorbance, thickness, unit, checkDate);
			}


			//byte holePositionHeader = 0x41;
			//byte[] currenHole = { 0x00 };   
			//int k = 0;
			//int t = 0;
			//for (int i = 1; i <= 96; i++)
			//{
			//    //孔位
			//    k = i % 8;
			//    t = i / 8;
			//    if (k == 0)
			//    {
			//        k = 8;
			//        t = t - 1;
			//    }

			//    currenHole[0] = Convert.ToByte(holePositionHeader + k - 1);
			//    ASCIIEncoding asciiCode = new ASCIIEncoding();
			//    string holePosition = asciiCode.GetString(currenHole) + (t + 1).ToString();

			//    //吸光度
			//    hexCode = dataRead.Substring((i - 1) * 6, 6);
			//    string absorbance = HexStr2Float3Str(hexCode);

			//    //浓度值
			//    hexCode = dataRead.Substring(576 + (i - 1) * 8, 8);
			//    string thickness = HexStr2Float4Str(hexCode);


			//	AddNewHistoricData(holePosition, itemName, absorbance, thickness, unit, checkDate);
			//}
		}



		/// <summary>
		/// 读取历史数据
		/// </summary>
		/// <param name="holes"></param>
		/// <param name="item"></param>
		/// <param name="abs"></param>
		/// <param name="checkValue"></param>
		/// <param name="unit"></param>
		/// <param name="time"></param>
		private void AddNewHistoricData(string holes, string item, string abs, string checkValue, string unit, string time)
		{
			DataRow myDr;
			myDr = DataReadTable.NewRow();
			myDr["已保存"] = false;
			myDr["孔位"] = holes;
			myDr["项目"] = item;
			myDr["吸光度"] = abs;
			myDr["检测值"] = checkValue;
			myDr["单位"] = unit;
			myDr["检测时间"] = time;

			m_WhereBuilder.Length = 0;
			m_WhereBuilder.AppendFormat(" checkMachine='010' AND HolesNum='{0}'", myDr["孔位"].ToString());
			m_WhereBuilder.AppendFormat(" AND MachineItemName='{0}'", myDr["项目"].ToString());
			m_WhereBuilder.AppendFormat(" AND CheckStartDate=#{0}#", myDr["检测时间"].ToString());
			myDr["已保存"] = m_ResultOperation.IsExist(m_WhereBuilder.ToString());


			DataReadTable.Rows.Add(myDr);

			if (MessageNotify.Instance() != null)
			{
				MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.Read5000Data, "OK");
			}
		}





		/// <summary>
		/// 读取当前数据
		/// </summary>
		public void ReadNow()
		{
			byte tx = 0x61;
			Send(tx);
		}



		private string HexStr2HexStrTrim(string s)
		{
			int k = s.Length / 2;
			string t1 = string.Empty;
			string temp = string.Empty;
			for (int i = 0; i < k; i++)
			{
				t1 = s.Substring(i * 2, 2);
				if (t1.Equals("00"))
				{
					return temp;
				}
				else
				{
					temp = temp + t1;
				}
			}
			return temp;
		}

		private string HexStr2Float3Str(string s)
		{
			int i1 = 0;
			//i1 = byte.Parse(s.Substring(0,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			i1 = Convert.ToByte(s.Substring(0, 2), 16);
			string temp = i1.ToString();
			int i2 = 0;
			i2 = Convert.ToByte(s.Substring(2, 2), 16) << 7;
			i2 += Convert.ToByte(s.Substring(4, 2), 16);
			//i2 = byte.Parse(s.Substring(2,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 128;
			//i2 = i2 + byte.Parse(s.Substring(4,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			temp = temp + "." + i2.ToString("000");
			return temp;
		}

		private string HexStr2Float4Str(string s)
		{
			int i1 = 0;
			i1 = Convert.ToByte(s.Substring(0, 2), 16) << 8;
			i1 += Convert.ToByte(s.Substring(2, 2), 16);
			//i1 = byte.Parse(s.Substring(0,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
			//i1 = i1 + byte.Parse(s.Substring(2,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			string temp = i1.ToString();
			int i2 = 0;
			//i2 = byte.Parse(s.Substring(4,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 128;
			//i2 = i2 + byte.Parse(s.Substring(6,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			i2 = Convert.ToByte(s.Substring(4, 2), 16) << 7;
			i2 += Convert.ToByte(s.Substring(6, 2), 16);
			temp = temp + "." + i2.ToString("000");
			return temp;
		}

		private string HexStr2AsciiStr(string s)
		{
			Match m = Regex.Match(s, "[^0-9a-fA-F]");
			if (m.Success || s.Length % 2 != 0)
			{
				return string.Empty;
			}
			else
			{
				string t1 = string.Empty;
				string t2 = string.Empty;
				int k = s.Length / 2;
				byte[] a1 = new byte[k];
				for (int i = 0; i < k; i++)
				{
					t1 = s.Substring(i * 2, 2);
					a1[i] = Convert.ToByte(t1, 16);// byte.Parse(t1, System.Globalization.NumberStyles.AllowHexSpecifier);
					t2 = t2 + Convert.ToChar(a1[i]).ToString();
				}
				return t2;
			}
		}

		private string Hexstr2Datestr(string s)
		{
			int k = 0;
			string[] a1 = new string[6];
			for (int i = 0; i < 6; i++)
			{
				a1[i] = s.Substring(i * 2, 2);
			}
			k = int.Parse(a1[0]) + 2000;
			string t1 = string.Empty;
			t1 = k.ToString() + "-";
			t1 = t1 + a1[1].ToString() + "-";
			t1 = t1 + a1[2].ToString() + " ";
			t1 = t1 + a1[3].ToString() + ":";
			t1 = t1 + a1[4].ToString() + ":";
			t1 = t1 + a1[5].ToString();
			return t1;
		}

		private byte[] HexStr2ByteArray(string s)
		{
			Match m = Regex.Match(s, "[^0-9a-fA-F]");
			if (m.Success || s.Length % 2 != 0)
			{
				byte[] b1 = new byte[32];
				return b1;
			}
			else
			{
				string t1 = string.Empty;
				int k = s.Length / 2;
				byte[] a1 = new byte[k];
				for (int i = 0; i < k; i++)
				{
					t1 = s.Substring(i * 2, 2);
					a1[i] = Convert.ToByte(t1, 16);
					//byte.Parse(t1, System.Globalization.NumberStyles.AllowHexSpecifier);
				}
				return a1;
			}
		}



		//private void ClearData()
		//{
		//    strShow = string.Empty;
		//    checkDtbl.Clear();
		//    FoodClient.frmMain.formAutoTakeDY5000.ShowResult(checkDtbl);
		//}
	}
}
