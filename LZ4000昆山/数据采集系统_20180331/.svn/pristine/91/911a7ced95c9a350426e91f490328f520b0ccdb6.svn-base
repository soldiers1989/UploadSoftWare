using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkstationDAL.Model;
using WorkstationModel.Model;
using System.Collections;
using WorkstationBLL.Mode;

namespace WorkstationModel.Instrument
{
    public class clsTL310: JH.CommBase.CommBase
    {
        public DataTable ComDataTable = null;
        private bool IsCreate = false;
        private ArrayList Alist = new ArrayList();
        private string RtnTemp = string.Empty;
        private StringBuilder strWhere = new StringBuilder();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        public string[,] unitInfo = new string[1, 6];
        private string item = "";
        private string testbase = "";
        private string standvalue = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsTL310()
        {
            IntTable();
        }
        private void IntTable()
        {
            if (IsCreate == false)
            {
                ComDataTable = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "通道号";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检企业性质";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "产地";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产单位";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "生产日期";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "送检日期";
                ComDataTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "处理结果";
                ComDataTable.Columns.Add(dataCol);

                IsCreate = true;
            }
        }
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 115200, Handshake.none, Parity.none);//9600
            return cs;
        }

       
        /// <summary>
        /// 接收串口返回的数据
        /// </summary>
        /// <param name="c"></param>
        protected override void OnRxChar(byte c)
        {
            RtnTemp = RtnTemp + c.ToString("X2");

            //Alist.Add(c.ToString("X2"));
            if (Global.communicatType == "通信测试" && RtnTemp=="7E020400FF021010F8AA")
            {
                RtnTemp = "";
                if (MessageNotification.GetInstance() != null)
                {
                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "TLtongxin");
                }
            }
            else if (Global.communicatType == "数据读取" && RtnTemp.Length >20)
            {
                string record = RtnTemp.Substring(12,2);//低位在前
                record = record + RtnTemp.Substring(10, 2);//高位在后
                int TotalRecord = Convert.ToInt16(record, 16);
                if (RtnTemp.Length == TotalRecord * 27*2)
                {
                    for (int i = 0; i < TotalRecord; i++)
                    {
                        string RecordNo = RtnTemp.Substring(16+54*i, 2);
                        RecordNo = RecordNo + RtnTemp.Substring(14 + 54 * i, 2);//当前记录是第几条记录
                        string holenum = Convert.ToInt32( RtnTemp.Substring(18 + 54* i, 2),16).ToString();//通道号
                        string standard = RtnTemp.Substring(20 + 54 * i, 2);//检测标准
                        string y ="20"+ Convert.ToInt32(RtnTemp.Substring(22 + 54 * i, 2),16).ToString();
                        int year = Int32.Parse(y);//检测时间年
                        int Month = Convert.ToInt32(RtnTemp.Substring(24 + 54 * i, 2), 16);//检测时间月
                        int Day = Convert.ToInt32(RtnTemp.Substring(26 + 54* i, 2), 16);//检测时间日
                        int hour = Convert.ToInt32(RtnTemp.Substring(28 + 54 * i, 2),16);//检测时间时
                        int Muni = Convert.ToInt32(RtnTemp.Substring(30 + 54 * i, 2), 16);//检测时间分
                        int seconds = Convert.ToInt32(RtnTemp.Substring(32 + 54* i, 2), 16);//检测时间秒
                        DateTime ChkTime = new DateTime(year, Month, Day, hour, Muni, seconds);

                        //string xiguangdu = RtnTemp.Substring(38 + 54 * i, 4);//吸光度
                        int CheckData = Convert.ToInt32(RtnTemp.Substring(42 + 54 * i, 2), 16) + Convert.ToInt32(RtnTemp.Substring(44 + 54 * i, 2), 16) * 256;//检测结果 抑制率
                        double dd = CheckData / 10;
                        double ww = CheckData % 10;
                        string checkResult = dd.ToString() + "." + ww.ToString();
                        //double ss = double.Parse(rr);
                        //string checkResult = string.Format("{0:P1}", ss);
                        string conclusion = "";//结论
                        if (RtnTemp.Substring(46 + 54 * i, 2) == "02")
                        {
                            conclusion = "合格";
                        }
                        else if (RtnTemp.Substring(46 + 54 * i, 2) == "01")
                        {
                            conclusion = "超标";
                        }
                        if (CheckData == 32766)//NA 返回 7FFE
                        {
                            continue;
                        }
                        AddTableData(holenum, standard, checkResult, conclusion, ChkTime);
                    }

                    RtnTemp = "";
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTL310Data, "DataRecord");
                    }
                }
            }   
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        public void CommunicateTest()
        {
            Global.communicatType = "通信测试";
            //Records = 0;
            //strShow = "";
            //GetRD = "";
            Global.SendFirst = true;//判断第一个字节的帧头
            //iscom = false;
            byte checkSum = crc8(clsStringUtil.HexString2ByteArray("010100FF"));
            string crc = checkSum.ToString("X2");
            byte[] sendData = clsStringUtil.HexString2ByteArray("7E010100FF" + crc + "AA");
            Send(sendData);
        }
        /// <summary>
        /// 读取上一次数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public void ReadComData(DateTime startTime, DateTime endTime)
        {
            Global.communicatType = "数据读取";
            string Headstring = "7E";
            ComDataTable.Clear();
            string LastData = "150400FF000000";
            byte jiaoyanhe = crc8(clsStringUtil.HexString2ByteArray(LastData));
            string src8 = jiaoyanhe.ToString("X2");

            byte[] sendData = clsStringUtil.HexString2ByteArray(Headstring + LastData + src8 + "AA");
            Send(sendData);
        }
        /// <summary>
        /// 读取全部数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public void ReadAllData(DateTime startTime, DateTime endTime)
        {
            Global.communicatType = "数据读取";
            string Headstring = "7E";
            string AllData = "150400FFFFFFFF";
            ComDataTable.Clear();
            byte jiaoyanhe = crc8(clsStringUtil.HexString2ByteArray(AllData));
            string src8 = jiaoyanhe.ToString("X2");

            byte[] sendData = clsStringUtil.HexString2ByteArray(Headstring + AllData + src8 + "AA");
            Send(sendData);
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

        private void AddTableData(string Holenum, string Standard, string checkValue, string Conclusion, DateTime  time)
        {
            DataRow dr = ComDataTable.NewRow();
           
            if (Standard == "00")
            {
                item = "农药残留";
                testbase = "GB/T 5009.199-2003";
                standvalue = "50";
            }
            else if (Standard == "01")
            {
                item = "农药残留(农标)";
                testbase = "NY/T 448-2001";
                standvalue = "70";
            }

            strWhere.Length = 0;
            strWhere.AppendFormat(" CheckData='{0}' AND CheckTime=#{1}# ", checkValue, time);
            dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
            dr["通道号"] = Holenum;
            dr["样品名称"] = "";
            dr["检测项目"] = item;
            dr["检测结果"] = checkValue;
            dr["单位"] = "%";
            dr["检测依据"] = testbase;
            dr["标准值"] = standvalue;
            dr["检测仪器"] = Global.ChkManchine;
            dr["结论"] = Conclusion;
            dr["检测单位"] = unitInfo[0, 0];
            dr["采样时间"] = time.AddHours(-2).ToString();
            dr["采样地点"] = unitInfo[0, 4];
            dr["被检单位"] = unitInfo[0, 1];
            dr["检测员"] = unitInfo[0, 2];
            dr["检测时间"] = time;
            dr["检测数量"] = "1";
            dr["样品种类"] = "果菜类";
            dr["产地"] = unitInfo[0, 4];
            dr["生产单位"] = unitInfo[0, 5];
            dr["生产日期"] = time.AddDays(-3).ToString();
            dr["被检企业性质"] = unitInfo[0, 3];
            dr["送检日期"] = time.AddHours(-2).ToString();
            dr["处理结果"] = "已处理";

            ComDataTable.Rows.Add(dr);
        }
    }
}
