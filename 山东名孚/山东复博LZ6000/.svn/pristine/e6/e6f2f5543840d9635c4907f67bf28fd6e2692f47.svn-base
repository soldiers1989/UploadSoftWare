using System;
using System.IO;
using System.Data;
using JH.CommBase;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace WindowsFormClient.BaseClass
{
	/// <summary>
	/// clsDY5000 的摘要说明。
	/// </summary>
	public class clsDY5000:CommBase
	{
		private static DataTable dt7 = new DataTable("dt7");
		private static bool IsCreatDT = false;

		public clsDY5000()
		{
			if(!IsCreatDT)
			{
				DataColumn myDataColumn;

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "孔位";
				dt7.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测项目";
				dt7.Columns.Add(myDataColumn);
				
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "吸光度";
				dt7.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测值";
				dt7.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测值单位";
				dt7.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测时间";
				dt7.Columns.Add(myDataColumn);
				IsCreatDT = true;
			}
		}

		private static string settingsFileName = "Data\\DY5000.Xml";
		private static int RxBufferP = 0;
		private static string strShow=string.Empty;
//		private static string strErr=string.Empty;
		private static bool IsHis=false;
        
		protected override CommBaseSettings CommSettings()
		{
			CommBaseSettings cs = new CommBaseSettings();
			string text1=Application.StartupPath;
			if (text1.Substring(text1.Length-1,1)!="\\")
			{
				text1=text1+"\\";
			}
			text1=text1 + settingsFileName;
			FileInfo f = new FileInfo(text1);
			if (f.Exists)
			{
				cs = CommBase.CommBaseSettings.LoadFromXML(f.OpenRead());
			}
			else
			{
				cs.SetStandard(ShareOption.ComPort, 2400, Handshake.none,Parity.none);
			}
			return cs;
		}
 
		protected override void OnRxChar(byte c)
		{
			strShow += c.ToString("X2");
			RxBufferP++;
			if(IsHis)
			{
				if(RxBufferP==710) 
				{
					RxBufferP=0;
					IsHis=false;
					this.DoWith(strShow.Substring(4,1416));
					strShow=string.Empty;
				}
			}
			else
			{
				if(RxBufferP==709)
				{
					RxBufferP=0;
					this.DoWith(strShow.Substring(2,1416));
					strShow=string.Empty;
				}
			}
		}

		public void ClearData()
		{
			dt7.Clear();
			WindowsFormClient.frmMain.formAutoTakeDY5000.ShowHisResult(dt7);
		}


		public void ReadNow()
		{
			byte tx=0x61;
			try
			{
				Send(tx);
			}
			catch
			{
			}
		}

		public void ReadHistory(string dtStart,string dtEnd)
		{
			string s="s"+dtStart+dtEnd;
			System.Text.ASCIIEncoding asciicode=new ASCIIEncoding();
			byte[] tx=asciicode.GetBytes(s);
			try
			{
				IsHis=true;
				Send(tx);
			}
			catch
			{
			}
		}

		private void DoWith(string strDo)
		{
			string kw=string.Empty;
			string xm=string.Empty;
			string xgd=string.Empty;
			string ndz=string.Empty;
			string dw=string.Empty;
			string sj=string.Empty;

			string temp=string.Empty;
			//得到项目名称
			temp=strDo.Substring(1376,40);
			temp=HexStr2HexStrTrim(temp);
			byte[] a=HexStr2ByteAry(temp);
			System.Text.Encoding gb2312=System.Text.Encoding.GetEncoding("gb2312");
			xm=gb2312.GetString(a);
			//得到单位
			temp=strDo.Substring(1344,20);
			temp=HexStr2HexStrTrim(temp);
			dw=HexStr2AsciiStr(temp);
			//得到时间
			temp=strDo.Substring(1364,12);
			sj=Hexstr2Datestr(temp);


			byte kwFirst=0x41;
			byte[] kwCur={0x00};
			int k=0;
			int t=0;
			for(int i=1; i<=96;i++)
			{
				//孔位
				k=i%8;
				t=i/8;
				if(k==0) 
				{
					k=8;
					t=t-1;
				}
				kwCur[0]=Convert.ToByte(kwFirst+k-1);
				System.Text.ASCIIEncoding ascencode=new ASCIIEncoding();
				kw=ascencode.GetString(kwCur)+(t+1).ToString();

				//吸光度
				temp=strDo.Substring((i-1)*6,6);
				xgd=HexStr2Float3Str(temp);

				//浓度值
				temp=strDo.Substring(576+(i-1)*8,8);
				ndz=HexStr2Float4Str(temp);


				AddNewHisData(kw,xm,xgd,ndz,dw,sj);
			}
			
			
		}

		private string HexStr2HexStrTrim(string s)
		{
			int k=s.Length/2;
			string t1=string.Empty;
			string temp=string.Empty;
			for(int i=0;i<k;i++)
			{
				t1=s.Substring(i*2,2);
				if(t1.Equals("00"))
				{
					return  temp;
				}
				else
				{
					temp=temp+t1;
				}
			}
			return temp;
		}

		private string HexStr2Float3Str(string s)
		{
			int i1=0;
			i1 = byte.Parse(s.Substring(0,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			string temp=i1.ToString();
			int i2=0;
			i2 = byte.Parse(s.Substring(2,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 128;
			i2 = i2 + byte.Parse(s.Substring(4,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			temp=temp+"."+i2.ToString("000");
			return temp;			
		}

		private string HexStr2Float4Str(string s)
		{
			int i1 = 0;
			i1 = byte.Parse(s.Substring(0,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
			i1 = i1 + byte.Parse(s.Substring(2,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			string temp=i1.ToString();
			int i2=0;
			i2 = byte.Parse(s.Substring(4,2),System.Globalization.NumberStyles.AllowHexSpecifier) * 128;
			i2 = i2 + byte.Parse(s.Substring(6,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			temp=temp+"."+i2.ToString("000");
			return temp;
		}

		private string HexStr2AsciiStr(string s)
		{
			Match m = Regex.Match(s,"[^0-9a-fA-F]");
			if(m.Success||s.Length%2 != 0)
			{
//				strErr="错误:不是一个有效的16进制字符串！";
				return string.Empty;
			}
			else
			{
				string t1=string.Empty;
				string t2=string.Empty;
				int k=s.Length/2;
				byte[] a1= new byte[k];
				for(int i=0 ; i<k;i++)
				{
					t1 = s.Substring(i*2,2);
					a1[i]=byte.Parse(t1,System.Globalization.NumberStyles.AllowHexSpecifier);
					t2 = t2 + Convert.ToChar(a1[i]).ToString();
				}
				return t2;
			}
		}

		private string Hexstr2Datestr(string s)
		{
			int k=0;
			string[] a1=new string[6];
			for(int i=0 ; i<6;i++)
			{
				a1[i] = s.Substring(i*2,2);
			}
			k = int.Parse(a1[0]) + 2000;
			string t1=string.Empty;
			t1 = k.ToString() + "-";
			t1 = t1 + a1[1].ToString() + "-";
			t1 = t1 + a1[2].ToString() + " ";
			t1 = t1 + a1[3].ToString() + ":";
			t1 = t1 + a1[4].ToString() + ":";
			t1 = t1 + a1[5].ToString() ;
			return t1;
		}

		private byte[] HexStr2ByteAry(string s)
		{
			Match m = Regex.Match(s,"[^0-9a-fA-F]");
			if(m.Success||s.Length%2 != 0)
			{
//				strErr="错误:不是一个有效的16进制串！";
				byte[] b1 = new byte[32];
				return b1;
			}
			else
			{
				string t1=string.Empty;
				int k=s.Length/2;
				byte[] a1= new byte[k];
				for(int i=0 ; i<k;i++)
				{
					t1 = s.Substring(i*2,2);
					a1[i]=byte.Parse(t1,System.Globalization.NumberStyles.AllowHexSpecifier);
				}
				return a1;
			}
		}

		private void AddNewHisData(string m,string  n,string f,string g,string h,string t)
		{
			DataRow myDataRow;
			myDataRow = dt7.NewRow();
			myDataRow["孔位"] = m;
			myDataRow["检测项目"] = n;
			myDataRow["吸光度"] = f;
			myDataRow["检测值"] = g;
			myDataRow["检测值单位"] = h;
			myDataRow["检测时间"] = t;
			dt7.Rows.Add(myDataRow);
			WindowsFormClient.frmMain.formAutoTakeDY5000.ShowHisResult(dt7);
		}

	}
}
