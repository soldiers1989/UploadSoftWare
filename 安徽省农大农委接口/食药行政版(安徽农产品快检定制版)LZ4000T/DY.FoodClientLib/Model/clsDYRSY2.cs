namespace WindowsFormClient.BaseClass
{
	/// <summary>
	/// clsDYRSY2 ��ժҪ˵����
	/// </summary>
	using System;
	using System.IO;
	using System.Data;
	using JH.CommBase;
	using System.Windows.Forms;
	using System.Text.RegularExpressions;

	public class clsDYRSY2:CommBase
	{

		public static DataTable dt1 = new DataTable("dt1");
		public static DataTable dt2 = new DataTable("dt2");
		private string strErr="";
		private static bool IsCreatDT = false;
		public static string    Showstr="";
       
		public clsDYRSY2()
		{
			if(!IsCreatDT)
			{
				DataColumn myDataColumn;

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Int32");
				myDataColumn.ColumnName = "���";
				dt1.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Decimal");
				myDataColumn.ColumnName = "���ֵ";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "����";
				dt1.Columns.Add(myDataColumn);


				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Int32");
				myDataColumn.ColumnName = "���";
				dt2.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Decimal");
				myDataColumn.ColumnName = "���ֵ";
				dt2.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "����";
				dt2.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "���ʱ��";
				dt2.Columns.Add(myDataColumn);

				IsCreatDT = true;
			}
		}
       
		private static string settingsFileName = "Data\\DYRSY2.Xml";
		private static int TRecordNum = 0;
		private static int RRecordNum = 0;
		private static int CRecordNum = 0;
		private static byte[] TxBuffer = new byte[5];
		private static string resultstr="";
        
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
				cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none,Parity.none);
			}
			return cs;
		}
        
		public void ReadHistory()
		{
			dt1.Clear();
			Showstr = "";
			resultstr="";
			TRecordNum = 0;
			RRecordNum = 0;
			CRecordNum = 0;
			TxBuffer[0] = 0x01;
			TxBuffer[1] = 0x55;
			TxBuffer[2] = 0x61;
			TxBuffer[3] = 0x62;
			TxBuffer[4] = 0x04;
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����϶�ȡ��ʷ���" + "01 55 61 62 04" + "\r\n";
			try
			{
				Send(TxBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"����");
			}
		}

		public void NReadVer()
		{
			Showstr = "";
			resultstr="";
			TRecordNum = 0;
			RRecordNum = 0;
			CRecordNum = 0;
			TxBuffer[0] = 0x01;
			TxBuffer[1] = 0x56;
			TxBuffer[2] = 0x41;
			TxBuffer[3] = 0x41;
			TxBuffer[4] = 0x04;
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����¶�ȡ�汾���" + "01 56 41 41 04" + "\r\n";
			try
			{
				Send(TxBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"����");
			}
		}

		public void NReadHistory(DateTime dtStart,DateTime dtEnd)
		{
			dt2.Clear();
			string SendStr="";
			Showstr = "";
			resultstr="";
			int    intStartYear = 0;
			int    intEndYear = 0;
			if (dtStart.Year < 2000 || dtStart.Year>2099)
			{
				MessageBox.Show("��ʼʱ�䲻��С��2000��,����2099�꣡","����");
			}
			else
			{
				intStartYear = (dtStart.Year - 2000);
			}
			if (dtEnd.Year < 2000 || dtEnd.Year>2099)
			{
				MessageBox.Show("����ʱ�䲻��С��2000��,����2099�꣡","����");
			}
			else
			{
				intEndYear = (dtEnd.Year - 2000);
			}
			if (dtEnd<dtStart)
			{
				MessageBox.Show("����ʱ�䲻��С����ʼʱ�䣡","����");
			}
			SendStr="R"+intStartYear.ToString("00")+dtStart.ToString("MMdd")+intEndYear.ToString("00")+dtEnd.ToString("MMdd");
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����¶�ȡ��ʷ���" + SendStr + "\r\n";
            byte[] SendBuffer = new byte[17];
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����¶�ȡ��ʷ���" ;
			SendBuffer=Str2ByteAry(SendStr);
			Showstr = "";
			TRecordNum = 0;
			RRecordNum = 0;
			CRecordNum = 0;
			try
			{
				Send(SendBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"����");
			}
		}

		public void NSetTime(DateTime dtSet)
		{
			string SendStr="";
			Showstr = "";
			resultstr="";
			int    intYear = 0;
			if (dtSet.Year < 2000 || dtSet.Year>2099)
			{
				MessageBox.Show("����ϵͳʱ�䲻��С��2000��,����2099�꣡","����");
			}
			else
			{
				intYear = (dtSet.Year - 2000);
			}
			SendStr="S"+intYear.ToString("00")+dtSet.ToString("MMddHHmmss");
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����������������" + SendStr + "\r\n";
			byte[] SendBuffer = new byte[17];
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����������������";
			SendBuffer=Str2ByteAry(SendStr);
			Showstr = "";
			TRecordNum = 0;
			RRecordNum = 0;
			CRecordNum = 0;
			try
			{
				Send(SendBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"����");
			}
		}

		private byte[] Str2ByteAry(string s)
		{
			int k=s.Length+4;
			byte[] a1= new byte[k];
			a1[0] = 0x01;
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +=a1[0].ToString("X2")+" ";
			int b=0;
			byte[] b1=new byte[1];
			for(int i=0; i<s.Length;i++)
			{		
				b1=System.Text.Encoding.ASCII.GetBytes(s.Substring(i,1));
				a1[i+1]=b1[0];
//				WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +=a1[i+1].ToString("X2")+" ";
				b += a1[i+1];
			}
			byte a=byte.Parse(b.ToString("X2").Substring(b.ToString().Length-2,2),System.Globalization.NumberStyles.AllowHexSpecifier);
			byte c=Convert.ToByte(0xFF-a + 0x01);
			b1=System.Text.Encoding.ASCII.GetBytes(c.ToString("X2").Substring(0,1));
			a1[k-3]=b1[0];
			b1=System.Text.Encoding.ASCII.GetBytes(c.ToString("X2").Substring(1,1));
			a1[k-2]=b1[0];
			a1[k-1]= 0x04;
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +=a1[k-3].ToString("X2")+" "+a1[k-2].ToString("X2")+" 04"+"\r\n";
			return a1;
		}

		protected override void OnRxChar(byte c)
		{
			Showstr = Showstr+c.ToString("X2");
			if (c.ToString("X2")=="04")
			{
				if(WindowsFormClient.frmMain.formAutoTakeSFY.IsNewVer)
				{
					if(Showstr.Substring(0,4)=="0145")
					{
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="���һ�����ݣ�" + Showstr+"\r\n";
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="Ҫ�������ݣ�" + resultstr+"\r\n";
						this.DoWithResult(resultstr);
						resultstr="";
					}
					else if(Showstr.Substring(0,4)=="0144")
					{
						resultstr +=Showstr;
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����������ݣ�" + Showstr+"\r\n";
					}
					else if(Showstr.Substring(0,2)=="44")
					{
						resultstr =resultstr+"01"+Showstr;
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����������ݣ�" + Showstr+"\r\n";
					}
					else
					{
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="�����������ݣ�" + Showstr+"\r\n";
					}
				}
				else
				{
					this.DoWithResult(Showstr);
				}
				Showstr="";
			}
		}

		private void DoWithResult(string s)
		{
			int id = 0;
			float f = 0;
			int itype = 0;
			string dtTest ;

			string tempstr="";
			Match m = Regex.Match(s,"[^0-9a-fA-F]");
			if(s.Length!=0)
			{
				if(m.Success||s.Length%2 != 0||s.Length/2 < 7||s.Substring(s.Length-2,2) != "04")
				{
					strErr="����:����һ����Ч���������ݣ������¶�ȡ";
					MessageBox.Show(strErr,"����");
				}
				else
				{
					//�õ���һ������ͷ
					if(s.Substring(0,2)=="56"||s.Substring(0,4)=="0156")
					{

						WindowsFormClient.frmMain.formAutoTakeSFY.IsNewVer=true;
						WindowsFormClient.frmMain.formAutoTakeSFY.SetStat();
						return;
					}
					if(s.Substring(0,4)=="0144"&&WindowsFormClient.frmMain.formAutoTakeSFY.IsNewVer)
					{
						if (s.Length%50!=0)
						{
							strErr="����:����һ����Ч���������ݣ������¶�ȡ";
							MessageBox.Show(strErr,"����");
						}
						else
						{
							TRecordNum = s.Length/50;
							for(int i=0; i<=TRecordNum-1;i++)
							{
								tempstr=HexStr2AsiiStr(s.Substring(i*50+4,40));
								id = int.Parse(tempstr.Substring(0,4));
								f =(float)(int.Parse(tempstr.Substring(4,3))*0.1);
								itype = int.Parse(tempstr.Substring(7,1));
//								dtTest = "20" + tempstr.Substring(8,2) + "-" + tempstr.Substring(10,2) + "-" + tempstr.Substring(12,2) + " "+ tempstr.Substring(14,2) +":"+ tempstr.Substring(16,2) +":"+ tempstr.Substring(18,2) ;
								dtTest = "20" + tempstr.Substring(8,2) + "-" + tempstr.Substring(10,2) + "-" + tempstr.Substring(12,2) + " ";
								DataRow myDataRow;
								myDataRow = dt2.NewRow();
								myDataRow["���"] = id;
								myDataRow["���ֵ"] = f;
								switch (itype)
								{
									case 1:
										myDataRow["����"] = "����";
										break;
									case 2:
										myDataRow["����"] = "ţ��";
										break;
									case 3:
										myDataRow["����"] = "����";
										break;
									case 4:
										myDataRow["����"] = "����";
										break;
									case 5:
										myDataRow["����"] = "����";
										break;
								}
								myDataRow["���ʱ��"]=dtTest;
								//							WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="���ݽ�����" + id.ToString() +" " +f.ToString() + " " + itype.ToString() + " " + dtTest.ToString() +"\r\n";
								dt2.Rows.Add(myDataRow);
							}
							WindowsFormClient.frmMain.formAutoTakeSFY.ShowResult(dt2);
						}
					}
					else if(!WindowsFormClient.frmMain.formAutoTakeSFY.IsNewVer)
					{
						TRecordNum = int.Parse(HexStr2AsiiStr(s.Substring(2,6)));
						if (s.Length/2 != TRecordNum*19 + 7)
						{
							strErr="����:����һ����Ч���������ݣ������¶�ȡ";
							MessageBox.Show(strErr,"����");
						}
						else
						{
							for(int i=0; i<=TRecordNum-1;i++)
							{
								tempstr=s.Substring(8+i*38,38);
								if(tempstr.Substring(0,2) != "1E" || tempstr.Substring(14,2) != "20" ||tempstr.Substring(18,2) != "20" || tempstr.Substring(36,2) != "20")
								{
									CRecordNum++;
								}
								else
								{
									id = int.Parse(HexStr2AsiiStr(tempstr.Substring(2,6)));
									f =(float)(int.Parse(HexStr2AsiiStr(tempstr.Substring(8,6)))*0.1);
									itype = int.Parse(HexStr2AsiiStr(tempstr.Substring(16,2)));
									dtTest = HexStr2AsiiStr(tempstr.Substring(20,16));
									dtTest = "20" + dtTest.Substring(0,2) + "-" + dtTest.Substring(3,2) + "-" + dtTest.Substring(6,2) + " 00:00:00";
									DataRow myDataRow;
									myDataRow = dt1.NewRow();
									myDataRow["���"] = id;
									myDataRow["���ֵ"] = f;
									switch (itype)
									{
										case 1:
											myDataRow["����"] = "����";
											break;
										case 2:
											myDataRow["����"] = "ţ��";
											break;
										case 3:
											myDataRow["����"] = "����";
											break;
										case 4:
											myDataRow["����"] = "����";
											break;
										case 5:
											myDataRow["����"] = "����";
											break;
									}
									dt1.Rows.Add(myDataRow);
									RRecordNum++;
								}
							}
							WindowsFormClient.frmMain.formAutoTakeSFY.ShowResult(dt1);
						}
					}
					else
					{
						strErr="����:����һ����Ч�����ݣ������¶�ȡ"+s.ToString();
						MessageBox.Show(strErr,"����");
					}
				}
			}
			else
			{
				frmMain.formMain.Cursor=Cursors.Default;
				MessageBox.Show("��ʱ�����û�м�����ݣ�","����");
				WindowsFormClient.frmMain.formAutoTakeSFY.ShowResult(dt2);
			}

		}

		private string HexStr2AsiiStr(string s)
		{
			string t1="";
			int k=s.Length/2;
			byte[] a1= new byte[k];
			for(int i=0 ; i<k;i++)
			{
				t1 = s.Substring(i*2,2);
				a1[i]=byte.Parse(t1,System.Globalization.NumberStyles.AllowHexSpecifier);
			}
            t1 = System.Text.Encoding.ASCII.GetString(a1,0,k);
			return t1;
		}

	}
}
