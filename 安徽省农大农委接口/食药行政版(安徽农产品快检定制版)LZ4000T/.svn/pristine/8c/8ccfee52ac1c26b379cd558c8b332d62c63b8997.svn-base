using System;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using JH.CommBase;
using System.Windows.Forms;


namespace WindowsFormClient.BaseClass
{
    /// <summary>
    /// clsDY3000DY 的摘要说明。
    /// </summary>
    public class clsDY3000DY : CommBase
    {
        private static DataTable dt7 = new DataTable("dt7");
        private static bool IsCreatDT = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY3000DY()
        {
            if (!IsCreatDT)
            {
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "编号";
                dt7.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "项目";
                dt7.Columns.Add(dataCol);


                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "检测值";
                dt7.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "检测值单位";
                dt7.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "检测日期";
                dt7.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = System.Type.GetType("System.String");
                dataCol.ColumnName = "数据可疑性";
                dt7.Columns.Add(dataCol);
                IsCreatDT = true;
            }
        }

        private static string settingFileName = "Data\\DY3000DY.Xml";
        private static int intRec = 0;
        private static int intRecData = 0;
        private static string strShow = string.Empty;
        private static string strItemCode = string.Empty;
        private static byte byteItemID = 0;
        private static int intRecordStr = 0;
        private static int intRecordEnd = 0;
        private static int intRecordNum = 0;
        public static string[,] strCheckItems;

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                cs = CommBase.CommBaseSettings.LoadFromXML(file.OpenRead());
            }
            else
            {
                cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);
            }
            return cs;
        }

        protected override void OnRxChar(byte c)
        {
            string s = c.ToString("X2");
            strShow += s;
            if (intRec == 0 && s.Equals("FE") && strShow.Length >= 4 && strShow.Substring(strShow.Length - 4, 2).Equals("03"))
            {
                intRec = 1;
            }
            if (intRec > 0)
            {
                intRec++;
                //再次判断获得参数是否正确,不正确清空,正确确定后续数据的长度
                if (intRec == 11)
                {
                    strShow = strShow.Substring(strShow.Length - 22, 22);
                    if (s.Equals(GetCheckDataStr(strShow.Substring(0, 20))))
                    {
                        string strtemp = strShow.Substring(16, 4);
                        int i1 = byte.Parse(strtemp.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
                        i1 = i1 + byte.Parse(strtemp.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                        intRecData = i1;
                    }
                    else
                    {
                        intRec = 0;
                        strShow = "";
                        intRecData = 0;
                    }
                }
                if (intRec == 12 + intRecData)
                {
                    if (intRecData > 0)
                    {
                        if (strShow.Substring(22, 2).Equals(GetCheckDataStr(strShow.Substring(24, intRecData * 2))))
                        {
                            DoWith(strShow);
                        }
                    }
                    else if (intRecData == 0)
                    {
                        if (strShow.Substring(4, 2).Equals("42")) //表明检测项目读完
                        {
                            //							IsRec=false;
                            WindowsFormClient.frmMain.formMachineEdit.ShowItems(strItemCode);
                        }
                    }
                    intRec = 0;
                    strShow = "";
                    intRecData = 0;
                }
            }
        }

        public void ClearData()
        {
            dt7.Clear();
            WindowsFormClient.frmMain.frmAutoDYSeries.ShowResult(dt7);
        }

        public void ReadItem()
        {
            strItemCode = "";
            byteItemID = 0;
            ReadItems(byteItemID);
        }

        private void ReadItems(byte i)
        {
            string sparam = i.ToString("X2");
            string s = "03FF420000" + sparam + "00000000";
            string ck = GetCheckDataStr(s);
            byte[] tx = this.HexStr2ByteAry(s + ck + "00");
            //			IsRec=true;
            try
            {
                Send(tx);
            }
            catch
            {
            }
        }

        public void ReadHistory(DateTime dtStart, DateTime dtEnd)
        {
            string s = "03FF4300000000000800B7";
            string dts = DateTime2HexStr(dtStart);
            string dte = DateTime2HexStr(dtEnd);
            string ck = GetCheckDataStr(dts + dte);
            byte[] tx = this.HexStr2ByteAry(s + ck + dts + dte);
            //			IsRec=true;
            try
            {
                Send(tx);
            }
            catch
            {
            }
        }

        private void ReadRec(long i)
        {
            string sparam = Convert.ToInt64(Convert.ToString(i, 2)).ToString("D32");
            string sparam1 = Convert.ToInt64(sparam.Substring(0, 8), 2).ToString("X2");
            string sparam2 = Convert.ToInt64(sparam.Substring(8, 8), 2).ToString("X2");
            string sparam3 = Convert.ToInt64(sparam.Substring(16, 8), 2).ToString("X2");
            string sparam4 = Convert.ToInt64(sparam.Substring(24, 8), 2).ToString("X2");
            sparam = sparam4 + sparam3 + sparam2 + sparam1;
            string s = "03FF4500" + sparam + "0000";
            string ck = GetCheckDataStr(s);
            byte[] tx = this.HexStr2ByteAry(s + ck + "00");
            //			IsRec=true;
            try
            {
                Send(tx);
            }
            catch
            {
            }
        }

        private void DoWith(string strDo)
        {
            string scmd = strDo.Substring(4, 2);
            string temp = "";
            if (scmd.Equals("42")) //表明检测项目
            {
                temp = strDo.Substring(24, 32);
                byte[] a = HexStr2ByteAry(HexStr2HexStrTrim(temp));
                System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                string xmmc = gb2312.GetString(a);
                temp = strDo.Substring(56, 16);
                byte[] b = HexStr2ByteAry(HexStr2HexStrTrim(temp));
                string xmdw = gb2312.GetString(b);
                temp = strDo.Substring(72, 2);
                int i = byte.Parse(temp, System.Globalization.NumberStyles.AllowHexSpecifier);
                strItemCode += "{" + xmmc.Trim() + ":-1:" + i.ToString() + ":" + xmdw.Trim() + "}";
                if (strDo.Length > 512)
                {
                    temp = strDo.Substring(280, 32);
                    a = HexStr2ByteAry(HexStr2HexStrTrim(temp));
                    xmmc = gb2312.GetString(a);
                    temp = strDo.Substring(312, 16);
                    b = HexStr2ByteAry(HexStr2HexStrTrim(temp));
                    xmdw = gb2312.GetString(b);
                    temp = strDo.Substring(328, 2);
                    i = byte.Parse(temp, System.Globalization.NumberStyles.AllowHexSpecifier);
                    strItemCode += "{" + xmmc.Trim() + ":-1:" + i.ToString() + ":" + xmdw.Trim() + "}";
                    byteItemID++;
                    ReadItems(byteItemID);
                }
                else
                {
                    //					IsRec=false;
                    intRec = 0;
                    strShow = "";
                    intRecData = 0;
                    WindowsFormClient.frmMain.formMachineEdit.ShowItems(strItemCode);
                }
            }
            else if (scmd.Equals("43")) //表明读记录数
            {
                string strtemp = strDo.Substring(40, 4);
                int i1 = byte.Parse(strtemp.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
                i1 = i1 + byte.Parse(strtemp.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                intRecordStr = i1;
                strtemp = strDo.Substring(44, 4);
                i1 = byte.Parse(strtemp.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
                i1 = i1 + byte.Parse(strtemp.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                intRecordEnd = i1;
                intRecordNum = 0;
                if (intRecordStr + 1 <= intRecordEnd)
                {
                    ReadRec(intRecordStr);
                }
                else
                {
                    intRecordNum = 0;
                    intRecordStr = 0;
                    intRecordEnd = 0;
                    //					IsRec=false;
                    intRec = 0;
                    strShow = "";
                    intRecData = 0;
                    if (dt7.Rows.Count <= 0)
                    {
                        MessageBox.Show("没有相应条件的检测数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    WindowsFormClient.frmMain.frmAutoDYSeries.ShowResult(dt7);
                }
            }
            else if (scmd.Equals("45")) //表明读的记录
            {
                //处理记录
                if (strDo.Substring(24, 2).Equals("00"))
                {

                }
                else
                {
                    string strtemp = strDo.Substring(16, 4);
                    int i1 = byte.Parse(strtemp.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
                    i1 = i1 + byte.Parse(strtemp.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    //i1为数据大小
                    int xmid = byte.Parse(strDo.Substring(26, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    string xmmc = "";
                    string xmdw = "";
                    string xmff = "";
                    if (xmid >= 0 && xmid <= strCheckItems.GetLength(0) - 1)
                    {
                        xmmc = strCheckItems[xmid, 0];
                        xmdw = strCheckItems[xmid, 3];
                        xmff = strCheckItems[xmid, 2];
                    }
                    string jcdatetime = HexStr2DateTimeStr(strDo.Substring(32, 8));
                    string tdnum = "";
                    string jcz = "";
                    string sjky = "否";
                    string sj = "";
                    int len = i1 / 8 - 1;
                    for (int i = 0; i < len; i++)
                    {
                        int p = byte.Parse(strDo.Substring(40 + i * 16 + 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                        int k = byte.Parse(strDo.Substring(40 + i * 16, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                        if (p >= 128 && k > 0) //p大于等于128表明有效,k=0表明是对照通道
                        {
                            sjky = "否";
                            if (p >= 192) sjky = "是";       //说明数据可疑
                            tdnum = k.ToString();
                            if (xmff.Equals("0"))   //为抑制率法
                            {
                                sj = strDo.Substring(40 + i * 16 + 8, 4);
                                if (sj.ToLower().Equals("ff7f"))
                                {
                                    jcz = "---.-";
                                }
                                else if (sj.ToLower().Equals("fe7f"))
                                {
                                    jcz = "NA";
                                }
                                else
                                {
                                    jcz = HexStr2Float2Str(sj);
                                }
                            }
                            else
                            {
                                sj = strDo.Substring(40 + i * 16 + 8, 8);
                                if (sj.ToLower().Equals("ffffff7f") || sj.ToLower().Equals("feffff7f"))
                                {
                                    jcz = "-.---";
                                }
                                else if (sj.ToLower().Equals("fdffff7f"))
                                {
                                    jcz = "NA";
                                }
                                else
                                {
                                    jcz = HexStr2Float4Str(sj);
                                }
                            }
                            if (!jcz.Equals("NA")) AddNewHisData(tdnum, xmmc, jcz, xmdw, jcdatetime, sjky);
                        }
                    }

                }
                intRecordNum++;
                if (intRecordStr + intRecordNum >= intRecordEnd)
                {
                    intRecordNum = 0;
                    intRecordStr = 0;
                    intRecordEnd = 0;
                    //					IsRec=false;
                    intRec = 0;
                    strShow = "";
                    intRecData = 0;
                    if (dt7.Rows.Count <= 0)
                    {
                        MessageBox.Show("没有相应条件的检测数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    WindowsFormClient.frmMain.frmAutoDYSeries.ShowResult(dt7);
                }
                else
                {
                    ReadRec(intRecordStr + intRecordNum);
                }
            }
        }

        private void AddNewHisData(string n, string f, string g, string h, string t, string p)
        {
            DataRow myDataRow;
            myDataRow = dt7.NewRow();
            myDataRow["编号"] = n;
            myDataRow["项目"] = f;
            myDataRow["检测值"] = g;
            myDataRow["检测值单位"] = h;
            myDataRow["检测日期"] = t;
            myDataRow["数据可疑性"] = p;
            dt7.Rows.Add(myDataRow);
        }


        private byte[] HexStr2ByteAry(string s)
        {
            Match m = Regex.Match(s, "[^0-9a-fA-F]");
            if (m.Success || s.Length % 2 != 0)
            {
                //				strErr="错误:不是一个有效的16进制串！";
                byte[] b1 = new byte[32];
                return b1;
            }
            else
            {
                string t1 = "";
                int k = s.Length / 2;
                byte[] a1 = new byte[k];
                for (int i = 0; i < k; i++)
                {
                    t1 = s.Substring(i * 2, 2);
                    a1[i] = byte.Parse(t1, System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                return a1;
            }
        }

        private string DateTime2HexStr(DateTime dt)
        {
            //9-15	年(只包含0-99部分) 7位
            int y = Convert.ToInt16(dt.ToString("yy"));
            long y2 = Convert.ToInt64(Convert.ToString(y, 2));
            //5-8	月 4位
            int m = Convert.ToInt16(dt.ToString("MM"));
            long m2 = Convert.ToInt64(Convert.ToString(m, 2));
            //0-4	日 5位
            int d = Convert.ToInt16(dt.ToString("dd"));
            long d2 = Convert.ToInt64(Convert.ToString(d, 2));
            string sdate = Convert.ToInt16(y2).ToString("D7") + Convert.ToInt16(m2).ToString("D4") + Convert.ToInt16(d2).ToString("D5");
            long sdl = Convert.ToInt64(sdate.Substring(0, 8), 2);
            long sdh = Convert.ToInt64(sdate.Substring(8, 8), 2);
            string sdatel = sdl.ToString("X2");
            string sdateh = sdh.ToString("X2");

            //11-15	时(00-23) 5位
            int h = Convert.ToInt16(dt.ToString("HH"));
            long h2 = Convert.ToInt64(Convert.ToString(h, 2));
            //5-10	分(00-59) 6位
            int M = Convert.ToInt16(dt.ToString("mm"));
            long M2 = Convert.ToInt64(Convert.ToString(M, 2));
            //0-4	秒/2，时间精度为2秒。

            string stime = Convert.ToInt16(h2).ToString("D5") + Convert.ToInt16(M2).ToString("D6") + "00000";
            long stl = Convert.ToInt64(stime.Substring(0, 8), 2);
            long sth = Convert.ToInt64(stime.Substring(8, 8), 2);
            string stimel = stl.ToString("X2");
            string stimeh = sth.ToString("X2");
            return stimeh + stimel + sdateh + sdatel;
        }

        private string HexStr2DateTimeStr(string s)
        {
            Match m = Regex.Match(s, "[^0-9a-fA-F]");
            if (m.Success || s.Length % 2 != 0)
            {
                //				strErr="错误:不是一个有效的16进制串！";
                return "00";
            }
            else
            {
                int ath = byte.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                int atl = byte.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                string stimel = Convert.ToInt64(Convert.ToString(atl, 2)).ToString("D8");
                string stimeh = Convert.ToInt64(Convert.ToString(ath, 2)).ToString("D8");
                string stime = stimel + stimeh;
                string ss = Convert.ToString(Convert.ToInt64(stime.Substring(11, 5), 2) * 2);
                string M = Convert.ToString(Convert.ToInt64(stime.Substring(5, 6), 2));
                string h = Convert.ToString(Convert.ToInt64(stime.Substring(0, 5), 2));

                int adh = byte.Parse(s.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                int adl = byte.Parse(s.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                string sdatel = Convert.ToInt64(Convert.ToString(adl, 2)).ToString("D8");
                string sdateh = Convert.ToInt64(Convert.ToString(adh, 2)).ToString("D8");
                string sdate = sdatel + sdateh;
                string d = Convert.ToString(Convert.ToInt64(sdate.Substring(11, 5), 2));
                string mm = Convert.ToString(Convert.ToInt64(sdate.Substring(7, 4), 2));
                string y = Convert.ToString(Convert.ToInt64(sdate.Substring(0, 7), 2));
                string sdatetime = "20" + y + "-" + mm + "-" + d + " " + h + ":" + M + ":" + ss;
                try
                {
                    DateTime dt = Convert.ToDateTime(sdatetime);
                }
                catch
                {
                }
                return sdatetime;
            }

        }

        private string GetCheckDataStr(String s)
        {
            Match m = Regex.Match(s, "[^0-9a-fA-F]");
            if (m.Success || s.Length % 2 != 0)
            {
                //				strErr="错误:不是一个有效的16进制串！";
                return "00";
            }
            else
            {
                string t1 = "";
                int k = s.Length / 2;
                byte a1 = 0;
                byte a2 = 0;
                for (int i = 0; i < k; i++)
                {
                    t1 = s.Substring(i * 2, 2);
                    a2 = byte.Parse(t1, System.Globalization.NumberStyles.AllowHexSpecifier);
                    a1 = Convert.ToByte(a1 ^ a2);
                }
                return a1.ToString("X2");
            }
        }

        private string HexStr2HexStrTrim(string s)
        {
            int k = s.Length / 2;
            string t1 = "";
            string temp = "";
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

        private string HexStr2Float2Str(string s)
        {
            int i2 = 0;
            i2 = byte.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
            i2 = i2 + byte.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            string temp = i2.ToString();
            try
            {
                temp = Convert.ToString(Convert.ToSingle(temp) / 10);
            }
            catch
            {
            }
            return temp;
        }

        private string HexStr2Float4Str(string s)
        {
            int iflag = 1;
            int i1 = 0;
            i1 = byte.Parse(s.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            if (i1 >= 128)
            {
                iflag = -1;
                i1 = (i1 - 128) * 256 * 256 * 256;
            }
            i1 += byte.Parse(s.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256 * 256;
            i1 += byte.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier) * 256;
            i1 += byte.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            float f = (float)i1 / 1000 * iflag;
            string temp = f.ToString();
            return temp;
        }

        private string HexStr2AsciiStr(string s)
        {
            Match m = Regex.Match(s, "[^0-9a-fA-F]");
            if (m.Success || s.Length % 2 != 0)
            {
                //				strErr="错误:不是一个有效的16进制字符串！";
                return "";
            }
            else
            {
                string t1 = "";
                string t2 = "";
                int k = s.Length / 2;
                byte[] a1 = new byte[k];
                for (int i = 0; i < k; i++)
                {
                    t1 = s.Substring(i * 2, 2);
                    a1[i] = byte.Parse(t1, System.Globalization.NumberStyles.AllowHexSpecifier);
                    t2 = t2 + Convert.ToChar(a1[i]).ToString();
                }
                return t2;
            }
        }
    }
}
