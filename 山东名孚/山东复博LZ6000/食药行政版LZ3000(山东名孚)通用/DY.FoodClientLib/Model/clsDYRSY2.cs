namespace WindowsFormClient.BaseClass
{
	/// <summary>
	/// clsDYRSY2 的摘要说明。
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
				myDataColumn.ColumnName = "编号";
				dt1.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Decimal");
				myDataColumn.ColumnName = "检测值";
				dt1.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "种类";
				dt1.Columns.Add(myDataColumn);


				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Int32");
				myDataColumn.ColumnName = "编号";
				dt2.Columns.Add(myDataColumn);
            
				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.Decimal");
				myDataColumn.ColumnName = "检测值";
				dt2.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "种类";
				dt2.Columns.Add(myDataColumn);

				myDataColumn = new DataColumn();
				myDataColumn.DataType = System.Type.GetType("System.String");
				myDataColumn.ColumnName = "检测时间";
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
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送老读取历史命令：" + "01 55 61 62 04" + "\r\n";
			try
			{
				Send(TxBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"错误");
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
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送新读取版本命令：" + "01 56 41 41 04" + "\r\n";
			try
			{
				Send(TxBuffer);
			}
			catch(CommPortException e)
			{
				MessageBox.Show(e.Message.ToString(),"错误");
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
				MessageBox.Show("起始时间不能小于2000年,大于2099年！","错误");
			}
			else
			{
				intStartYear = (dtStart.Year - 2000);
			}
			if (dtEnd.Year < 2000 || dtEnd.Year>2099)
			{
				MessageBox.Show("结束时间不能小于2000年,大于2099年！","错误");
			}
			else
			{
				intEndYear = (dtEnd.Year - 2000);
			}
			if (dtEnd<dtStart)
			{
				MessageBox.Show("结束时间不能小于起始时间！","错误");
			}
			SendStr="R"+intStartYear.ToString("00")+dtStart.ToString("MMdd")+intEndYear.ToString("00")+dtEnd.ToString("MMdd");
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送新读取历史命令：" + SendStr + "\r\n";
            byte[] SendBuffer = new byte[17];
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送新读取历史命令：" ;
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
				MessageBox.Show(e.Message.ToString(),"错误");
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
				MessageBox.Show("设置系统时间不能小于2000年,大于2099年！","错误");
			}
			else
			{
				intYear = (dtSet.Year - 2000);
			}
			SendStr="S"+intYear.ToString("00")+dtSet.ToString("MMddHHmmss");
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送新设置日期命令：" + SendStr + "\r\n";
			byte[] SendBuffer = new byte[17];
//			WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="发送新设置日期命令：";
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
				MessageBox.Show(e.Message.ToString(),"错误");
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
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="最后一条数据：" + Showstr+"\r\n";
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="要处理数据：" + resultstr+"\r\n";
						this.DoWithResult(resultstr);
						resultstr="";
					}
					else if(Showstr.Substring(0,4)=="0144")
					{
						resultstr +=Showstr;
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="单个返回数据：" + Showstr+"\r\n";
					}
					else if(Showstr.Substring(0,2)=="44")
					{
						resultstr =resultstr+"01"+Showstr;
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="单个返回数据：" + Showstr+"\r\n";
					}
					else
					{
//						WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="其它返回数据：" + Showstr+"\r\n";
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
					strErr="错误:不是一个有效完整的数据！请重新读取";
					MessageBox.Show(strErr,"错误");
				}
				else
				{
					//得到第一个命令头
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
							strErr="错误:不是一个有效完整的数据！请重新读取";
							MessageBox.Show(strErr,"错误");
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
								myDataRow["编号"] = id;
								myDataRow["检测值"] = f;
								switch (itype)
								{
									case 1:
										myDataRow["种类"] = "猪肉";
										break;
									case 2:
										myDataRow["种类"] = "牛肉";
										break;
									case 3:
										myDataRow["种类"] = "羊肉";
										break;
									case 4:
										myDataRow["种类"] = "鸡肉";
										break;
									case 5:
										myDataRow["种类"] = "其它";
										break;
								}
								myDataRow["检测时间"]=dtTest;
								//							WindowsFormClient.frmMain.formAutoTakeSFY.textBox1.Text +="数据解析：" + id.ToString() +" " +f.ToString() + " " + itype.ToString() + " " + dtTest.ToString() +"\r\n";
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
							strErr="错误:不是一个有效完整的数据！请重新读取";
							MessageBox.Show(strErr,"错误");
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
									myDataRow["编号"] = id;
									myDataRow["检测值"] = f;
									switch (itype)
									{
										case 1:
											myDataRow["种类"] = "猪肉";
											break;
										case 2:
											myDataRow["种类"] = "牛肉";
											break;
										case 3:
											myDataRow["种类"] = "羊肉";
											break;
										case 4:
											myDataRow["种类"] = "鸡肉";
											break;
										case 5:
											myDataRow["种类"] = "其它";
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
						strErr="错误:不是一个有效的数据！请重新读取"+s.ToString();
						MessageBox.Show(strErr,"错误");
					}
				}
			}
			else
			{
				frmMain.formMain.Cursor=Cursors.Default;
				MessageBox.Show("该时间段内没有检测数据！","错误");
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
