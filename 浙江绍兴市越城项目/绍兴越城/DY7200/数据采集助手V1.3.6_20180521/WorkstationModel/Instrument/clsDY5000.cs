using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.CommBase;
using System.Data;
using WorkstationModel.Model;
using System.Text.RegularExpressions;
using WorkstationDAL.Model;

namespace WorkstationModel.Instrument
{
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
        public  string[,] unitInfo = new string[1, 7];
        public clsDY5000()
        {
            if (!_IsCreatedDataTable)
            {
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);// System.Type.GetType("System.String");
                dataCol.ColumnName = "孔位";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string); 
                dataCol.ColumnName = "检测项目";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string); 
                dataCol.ColumnName = "单位";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "吸光度";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地址";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                _dataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                _dataReadTable.Columns.Add(dataCol);

                _IsCreatedDataTable = true;
            }

           
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
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 2400, Handshake.none, Parity.none);
            return cs;
        }
        protected override void OnRxChar(byte c)
        {
            Console.Write(c.ToString("X2"));
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
                    if (clsMessageNotify.Instance() != null)
                    {
                        clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.Read5000Data, "OK");
                    }
                    _DataRead = string.Empty;
                }
            }
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
            string hexCode = dataRead.Substring(dataLength - 72, 20);
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
            for (int index = 0; index < nunOfHole; index++)
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
                    holePosition = asciiCode.GetString(currenHole) + (holeNum / 8 + 1).ToString();
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
     
            DataRow myDr = _dataReadTable.NewRow();
            
            myDr["孔位"] = holes;
            //myDr["样品名称"] = "";
            myDr["检测项目"] = item;
            myDr["检测结果"] = checkValue;
            myDr["单位"] = unit;
            myDr["吸光度"] = abs;
            //myDr["检测依据"] = "";
            //myDr["标准值"] = "";
            myDr["检测仪器"] = Global.ChkManchine;
            //myDr["结论"] = "";
            myDr["检测单位"] = unitInfo[0, 0];
            myDr["采样时间"] = System.DateTime.Now.ToString();
            myDr["采样地址"] = unitInfo[0, 2];
            myDr["检测员"] = unitInfo[0, 3];
            myDr["检测时间"] = time;
            myDr["被检单位"] = unitInfo[0, 1];
            //查询数据库是否保存过数据
            _WhereBuilder.Length = 0;
            _WhereBuilder.AppendFormat(" CheckData='{0}' AND Checkitem='{1}' AND CheckTime=#{2}#", checkValue, item, DateTime.Parse(time.Replace("-", "/")));
            //_WhereBuilder.AppendFormat(" AND Checkitem='{0}'", item);
            //_WhereBuilder.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(time.Replace("-", "/")));
            myDr["已保存"] = (m_ResultOperation.IsExist(_WhereBuilder.ToString()) == true ? "是" : "否");

            _dataReadTable.Rows.Add(myDr);
           
        }

        /// <summary>
        /// 读取当前数据
        /// </summary>
        //public void ReadNow()
        //{
        //    byte tx = 0x61;
        //    Send(tx);
        //}

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
    }
}
