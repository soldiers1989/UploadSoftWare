using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.CommBase;
using DY.FoodClientLib;
using System.Data;
using com.lvrenyang;
using System.Windows.Forms;

namespace FoodClient.App_Code
{
    public class clsTL310 : CommBase
    {
        private bool m_IsCreatedDataTable = false;
        public string readtype = "";
        private string RtnTemp = string.Empty;
        public DataTable DataReadTable = null;
        private string item = "";
        private string testbase = "";
        private string standvalue = "";
        private StringBuilder strWhere = new StringBuilder();
        public  int icount = 0;
        public int Ncount = 0;
        protected readonly clsResultOpr _resultBll = new clsResultOpr();

        public clsTL310()
        {
            if (m_IsCreatedDataTable == false)
            {
                DataReadTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通道号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
        /// <summary>
        /// 设置波特
        /// </summary>
        /// <returns></returns>
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(ShareOption.ComPort, 115200, Handshake.none, Parity.none);//115200
            return cs;
        }
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="c"></param>
        protected override void OnRxChar(byte c)
        {
            try
            {
                RtnTemp = RtnTemp + c.ToString("X2");
                if (RtnTemp.Length > 20)
                {
                    string record = RtnTemp.Substring(12, 2);//低位在前
                    record = record + RtnTemp.Substring(10, 2);//高位在后
                    int TotalRecord = Convert.ToInt16(record, 16);
                    if (RtnTemp.Length == 54)
                    {
                        string RecordNo = RtnTemp.Substring(16, 2);
                        RecordNo = RecordNo + RtnTemp.Substring(14, 2);//当前记录是第几条记录
                        string holenum = Convert.ToInt32(RtnTemp.Substring(18, 2), 16).ToString();//通道号
                        string standard = RtnTemp.Substring(20, 2);//检测标准
                        string y = "20" + Convert.ToInt32(RtnTemp.Substring(22, 2), 16).ToString();
                        int year = Int32.Parse(y);//检测时间年
                        int Month = Convert.ToInt32(RtnTemp.Substring(24, 2), 16);//检测时间月
                        int Day = Convert.ToInt32(RtnTemp.Substring(26, 2), 16);//检测时间日
                        int hour = Convert.ToInt32(RtnTemp.Substring(28, 2), 16);//检测时间时
                        int Muni = Convert.ToInt32(RtnTemp.Substring(30, 2), 16);//检测时间分
                        int seconds = Convert.ToInt32(RtnTemp.Substring(32, 2), 16);//检测时间秒
                        Console.WriteLine(year + "," + Month + "," + Day + "," + hour + "," + Muni + "," + seconds);
                        Console.WriteLine(RtnTemp);
                        DateTime ChkTime = DateTime.Now;// new DateTime(year, Month, Day, hour, Muni, seconds);

                        //Console.WriteLine(ChkTime.ToString("yyyy-MM-dd HH:mm:ss"));

                        //string xiguangdu = RtnTemp.Substring(38 + 54 * i, 4);//吸光度
                        int CheckData = Convert.ToInt32(RtnTemp.Substring(42, 2), 16) + Convert.ToInt32(RtnTemp.Substring(44, 2), 16) * 256;//检测结果 抑制率
                        double dd = CheckData / 10;
                        double ww = CheckData % 10;
                        string checkResult = dd.ToString() + "." + ww.ToString();
                        //double ss = double.Parse(rr);
                        //string checkResult = string.Format("{0:P1}", ss);
                        string conclusion = "";//结论
                        if (RtnTemp.Substring(46, 2) == "02")
                        {
                            conclusion = "合格";
                        }
                        else if (RtnTemp.Substring(46, 2) == "01")
                        {
                            conclusion = "超标";
                        }
                        if (CheckData == 32766 || CheckData > 32000)//NA 返回 7FFE
                        {
                            //NA不显示
                            Ncount = Ncount + 1;
                        }
                        else
                        {
                            AddTableData(holenum, standard, checkResult, conclusion, ChkTime);
                            //icount = icount + 1;
                            if (MessageNotification.GetInstance() != null)
                            {
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "DataRecord");
                            }
                        }
                        icount = icount + 1;
                        Console.WriteLine(icount);
                        RtnTemp = "";
                        if (TotalRecord == (icount + Ncount))
                        {
                            if (MessageNotification.GetInstance() != null)
                            {

                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "end");
                            }
                        }
                    }

                    //if (RtnTemp.Length == TotalRecord * 27 * 2)
                    //{
                    //    //FileUtils.KLog(RtnTemp , "接收", 15);
                    //    for (int i = 0; i < TotalRecord; i++)
                    //    {
                    //        string RecordNo = RtnTemp.Substring(16 + 54 * i, 2);
                    //        RecordNo = RecordNo + RtnTemp.Substring(14 + 54 * i, 2);//当前记录是第几条记录
                    //        string holenum = Convert.ToInt32(RtnTemp.Substring(18 + 54 * i, 2), 16).ToString();//通道号
                    //        string standard = RtnTemp.Substring(20 + 54 * i, 2);//检测标准
                    //        string y = "20" + Convert.ToInt32(RtnTemp.Substring(22 + 54 * i, 2), 16).ToString();
                    //        int year = Int32.Parse(y);//检测时间年
                    //        int Month = Convert.ToInt32(RtnTemp.Substring(24 + 54 * i, 2), 16);//检测时间月
                    //        int Day = Convert.ToInt32(RtnTemp.Substring(26 + 54 * i, 2), 16);//检测时间日
                    //        int hour = Convert.ToInt32(RtnTemp.Substring(28 + 54 * i, 2), 16);//检测时间时
                    //        int Muni = Convert.ToInt32(RtnTemp.Substring(30 + 54 * i, 2), 16);//检测时间分
                    //        int seconds = Convert.ToInt32(RtnTemp.Substring(32 + 54 * i, 2), 16);//检测时间秒
                    //        DateTime ChkTime = new DateTime(year, Month, Day, hour, Muni, seconds);

                    //        //string xiguangdu = RtnTemp.Substring(38 + 54 * i, 4);//吸光度
                    //        int CheckData = Convert.ToInt32(RtnTemp.Substring(42 + 54 * i, 2), 16) + Convert.ToInt32(RtnTemp.Substring(44 + 54 * i, 2), 16) * 256;//检测结果 抑制率
                    //        double dd = CheckData / 10;
                    //        double ww = CheckData % 10;
                    //        string checkResult = dd.ToString() + "." + ww.ToString();
                    //        //double ss = double.Parse(rr);
                    //        //string checkResult = string.Format("{0:P1}", ss);
                    //        string conclusion = "";//结论
                    //        if (RtnTemp.Substring(46 + 54 * i, 2) == "02")
                    //        {
                    //            conclusion = "合格";
                    //        }
                    //        else if (RtnTemp.Substring(46 + 54 * i, 2) == "01")
                    //        {
                    //            conclusion = "超标";
                    //        }
                    //        if (CheckData == 32766 || CheckData>32000)//NA 返回 7FFE
                    //        {
                    //            continue;
                    //        }
                    //        AddTableData(holenum, standard, checkResult, conclusion, ChkTime);

                    //        if (MessageNotification.GetInstance() != null)
                    //        {
                    //            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "DataRecord");
                    //        }
                    //    }

                    //    RtnTemp = "";

                    //}
                    //else if(RtnTemp.Length>=54)
                    //{
                    //    FileUtils.KLog(RtnTemp, "接收", 15);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddTableData(string Holenum, string Standard, string checkValue, string Conclusion, DateTime time)
        {
            DataRow dr = DataReadTable.NewRow();

            if (Standard == "00")
            {
                item = "农药残留(国)";
                testbase = "GB/T 5009.199-2003";
                standvalue = "50";
            }
            else if (Standard == "01")
            {
                item = "农药残留(农)";
                testbase = "NY/T 448-2001";
                standvalue = "70";
            }

            strWhere.Length = 0;
            strWhere.AppendFormat(" HolesNum='{0}'", Holenum);
            strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
            strWhere.AppendFormat(" AND CheckValueInfo='{0}'", checkValue);
            strWhere.AppendFormat(" AND CheckStartDate=#{0}#", time);

            dr["已保存"] = _resultBll.IsExist(strWhere.ToString()); ;
            dr["通道号"] = Holenum;
            dr["检测项目"] = item;
            dr["检测结果"] = checkValue;
            dr["单位"] = "%";
            dr["结论"] = Conclusion;
            dr["检测时间"] = time.ToString();

            DataReadTable.Rows.Add(dr);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ReadHistory(DateTime startDate, DateTime endDate)
        {
            icount = 0;
            Ncount = 0;
            if (readtype == "读取上一次数据")
            {
                string Headstring = "7E";
                string LastData = "150400FF000000";
                byte jiaoyanhe = crc8(StringUtil.HexString2ByteArray(LastData));
                string src8 = jiaoyanhe.ToString("X2");
                //FileUtils.KLog(Headstring + LastData + src8 + "AA", "上一次", 15);

                byte[] sendData = StringUtil.HexString2ByteArray(Headstring + LastData + src8 + "AA");
                Send(sendData);

            }
            else if (readtype == "读取全部数据")
            {
                string Headstring = "7E";
                string AllData = "150400FFFFFFFF";
                byte jiaoyanhe = crc8(StringUtil.HexString2ByteArray(AllData));
                string src8 = jiaoyanhe.ToString("X2");
                //FileUtils.KLog(Headstring + AllData + src8 + "AA", "全部", 15);

                byte[] sendData = StringUtil.HexString2ByteArray(Headstring + AllData + src8 + "AA");
                Send(sendData);
            }
        }
        /// <summary>
        /// crc国际标准
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

    }
}
