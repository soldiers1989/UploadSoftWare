using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using JH.CommBase;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsDY5000 ��ժҪ˵����
	/// </summary>
	public class clsDY5000 : CommBase
	{
		public DataTable _dataReadTable = new DataTable("checkDtbl");
		private bool _IsCreatedDataTable = false;
		private int _SelfDectectionByteCount = 0;
		private bool _IsSelfDectection = false;
		private string _DataRead = string.Empty;
		private StringBuilder _WhereBuilder = new StringBuilder();
		private readonly clsResultOpr m_ResultOperation = new clsResultOpr();
		private bool _IsFirst = false;

		public clsDY5000()
		{
			if (!_IsCreatedDataTable)
			{
				DataColumn dataCol;

				dataCol = new DataColumn();
				dataCol.DataType = typeof(bool);
				dataCol.ColumnName = "�ѱ���";
				_dataReadTable.Columns.Add(dataCol);


				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "��λ";
				_dataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string); //System.Type.GetType("System.String");
				dataCol.ColumnName = "��Ŀ";
				_dataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "�����";
				_dataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
				dataCol.ColumnName = "���ֵ";
				_dataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string); //System.Type.GetType("System.String");
				dataCol.ColumnName = "��λ";
				_dataReadTable.Columns.Add(dataCol);

				dataCol = new DataColumn();
				dataCol.DataType = typeof(string);
				dataCol.ColumnName = "���ʱ��";
				_dataReadTable.Columns.Add(dataCol);
				_IsCreatedDataTable = true;
			}
		}

		protected override CommBaseSettings CommSettings()
		{
			CommBaseSettings cs = new CommBaseSettings();
			cs.SetStandard(ShareOption.ComPort, 2400, Handshake.none, Parity.none);
			return cs;
		}

		public void ReadHistory(string startDate, string endDate)
		{
			string originalData = "s" + startDate + endDate;
			ASCIIEncoding asciiCode = new ASCIIEncoding();
			byte[] dataSent = asciiCode.GetBytes(originalData);
			_IsFirst = false;
			_IsSelfDectection = false;
			_SelfDectectionByteCount = 0;
			Send(dataSent);
		}

		protected override void OnRxChar(byte c)
		{
			
			string currentChar = c.ToString("X2");
			string asciiChar = HexStr2AsciiStr(currentChar);
			_SelfDectectionByteCount++;
			if (asciiChar == "s" && !_IsFirst)
			{
				_IsFirst = true;
				if (_SelfDectectionByteCount - 1 > 1)
					_IsSelfDectection = true;
				return;
			}
			if (_IsSelfDectection)
				return;

			if (_IsFirst)
			{
				if (asciiChar != "e")
					_DataRead += currentChar;
				if (asciiChar == "e")
				{
					_IsFirst = false;
					_IsSelfDectection = false;
					_SelfDectectionByteCount = 0;
					this.ParseDataFromDevice(_DataRead);
					_DataRead = string.Empty;
				}
			}
		}


		private void ParseDataFromDevice(string dataRead)
		{
			////��λ
			//string hexCode = dataRead.Substring(1344, 20);
			//hexCode = HexStr2HexStrTrim(hexCode);
			//string unit = HexStr2AsciiStr(hexCode);


			////���ʱ��
			//hexCode = dataRead.Substring(1364, 12);
			//string checkDate = Hexstr2Datestr(hexCode);


			////��Ŀ����
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

			//��λ
			string hexCode = dataRead.Substring(dataLength-72, 20);
			hexCode = HexStr2HexStrTrim(hexCode);
			string unit = HexStr2AsciiStr(hexCode);


			//���ʱ��
			hexCode = dataRead.Substring(dataLength - 72 + 20, 12);
			string checkDate = Hexstr2Datestr(hexCode);


			//��Ŀ����
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
			//    //��λ
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

			//    //�����
			//    hexCode = dataRead.Substring((i - 1) * 6, 6);
			//    string absorbance = HexStr2Float3Str(hexCode);

			//    //Ũ��ֵ
			//    hexCode = dataRead.Substring(576 + (i - 1) * 8, 8);
			//    string thickness = HexStr2Float4Str(hexCode);


			//	AddNewHistoricData(holePosition, itemName, absorbance, thickness, unit, checkDate);
			//}
		}



		/// <summary>
		/// ��ȡ��ʷ����
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
			myDr = _dataReadTable.NewRow();
			myDr["�ѱ���"] = false;
			myDr["��λ"] = holes;
			myDr["��Ŀ"] = item;
			myDr["�����"] = abs;
			myDr["���ֵ"] = checkValue;
			myDr["��λ"] = unit;
			myDr["���ʱ��"] = time;

			_WhereBuilder.Length = 0;
			_WhereBuilder.AppendFormat(" checkMachine='010' AND HolesNum='{0}'", myDr["��λ"].ToString());
			_WhereBuilder.AppendFormat(" AND MachineItemName='{0}'", myDr["��Ŀ"].ToString());
			_WhereBuilder.AppendFormat(" AND CheckStartDate=#{0}#", myDr["���ʱ��"].ToString());
			myDr["�ѱ���"] = m_ResultOperation.IsExist(_WhereBuilder.ToString());


			_dataReadTable.Rows.Add(myDr);

			if (MessageNotify.Instance() != null)
			{
				MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.Read5000Data, "OK");
			}
		}





		/// <summary>
		/// ��ȡ��ǰ����
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
