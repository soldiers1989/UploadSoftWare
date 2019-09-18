using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationModel.Model;
using WorkstationDAL.Model;
using JH.CommBase;

namespace WorkstationModel.Instrument
{
    public class clsLZ4000T :CommBase
    {
        clsSaveResult resultdata = new clsSaveResult();
        clsSetSqlData sqlSet = new clsSetSqlData();
        #region 自定义变量
        private bool m_IsCreatedDataTable = false;
        private string N_DataTemp42 = string.Empty;
        private string N_DataTemp62 = string.Empty;
        private string N_DataTemp72 = string.Empty;
        private string m_DataRead = string.Empty;
        private string m_ItemParts = string.Empty;
        private byte[] bt = new byte[40960];
        public string strShow = string.Empty;
        private int intRec = 0;
        public static string[,] CheckItemsArray;
        public static string[,] CheckItemsArray62;
        public static string[,] CheckItemsArray72;
        public DataTable DataReadTable = null;
        public string DY660;
        private StringBuilder strWhere = new StringBuilder();
        private string err="";
        /// <summary>
        /// 检测单位、被检单位信息
        /// </summary>
        public string[] BaseUnitInfo = new string[4];
        private int Rbegin = 0;//真开始位置
        public bool iscom = false;
        public int Records = 0;//记录数
        private DataTable dta =null ;
        #endregion

        public clsLZ4000T()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");//去掉Static
             
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品编号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目小类";
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
                dataCol.ColumnName = "检测依据";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测单位编号";
                //DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位编号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "经营户";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "经营户身份证号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "摊位编号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "复核人";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位类别";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "初检/复检";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "备注";
                DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样时间";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "采样地点";
                //DataReadTable.Columns.Add(dataCol);

               

              

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "样品种类";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "检测数量";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "数量单位";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "条形码";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "生产单位";//19
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "产地地址";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "生产企业";//21
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "产地";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "生产日期";
                //DataReadTable.Columns.Add(dataCol);

                //dataCol = new DataColumn();
                //dataCol.DataType = typeof(string);
                //dataCol.ColumnName = "送检日期";
                //DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 115200, Handshake.none, Parity.none);
            return cs;
        }
        //截取返回数据
        public string GetRD = "";
       
        protected override void OnRxChar(byte c)
        {
            string rec = c.ToString("X2");
            strShow += rec;

            if (strShow.Length > 16 && strShow.Substring(0, 2) == "7E")
            {
                //读取数据
                if (strShow.Substring(0, 4) == "7E11")
                {
                    //找到帧头、帧尾
                    if (strShow.Length == 52 && strShow.Substring(50, 2) == "AA")
                    {
                        //crc校验
                        byte crcsum = crc8(clsStringUtil.HexString2ByteArray(strShow.Substring(2, 46)));
                        //确定本次读取的数据总条数
                        int value = Convert.ToInt32(strShow.Substring(6, 4), 16);
                        Records = Records + 1;
                        if (crcsum.ToString("X2") == strShow.Substring(48, 2))
                        {                          
                            ParseDataFromDevice(strShow);
                            strShow = string.Empty;
                            if (Records == value)//读完当天所有记录才发送消息读下一天
                            {
                                Records = 0;
                                if (MessageNotification.GetInstance() != null)
                                {
                                 
                                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
                                }
                            }
                        }
                        else if (Records == value)
                        {
                            //校验数据不对就清空，返回
                            strShow = "";
                            if (MessageNotification.GetInstance() != null)
                            {
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
                            }
                        }
                        else
                        {
                            //校验数据不对就清空
                            strShow = "";
                        }
                    }
                }
                else if (strShow.Substring(0, 4) == "7E01") //通信测试
                {
                    if (strShow.Length == 18 && strShow.Substring(16, 2) == "AA")
                    {
                        intRec = 0;
                        strShow = string.Empty;
                        if (MessageNotification.GetInstance() != null)
                        {
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "tongxin");
                        }
                    }
                }
                else if (strShow.Substring(0, 4) == "7E13")//读取记录条数
                {
                    if (strShow.Length == 22 && strShow.Substring(14, 4) == "0000" && strShow.Substring(20, 2) == "AA")//没有记录
                    {
                        if (MessageNotification.GetInstance() != null)
                        {
                            strShow = "";
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "NoRecord");
                        }
                    }
                    else if (strShow.Length == 22 && strShow.Substring(20, 2) == "AA")
                    {
                        if (MessageNotification.GetInstance() != null)//有记录
                        {
                            strShow = "";
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "Record");
                        }
                    }
                }
            }
            else if (strShow.Substring(0, 2) != "7E")//第一个字节不是7E，截取7E开始的字符串
            {
                if (Global.communicatType == "读数据")
                {
                    if (rec == "7E")
                    {
                        iscom = true;
                    }
                    if (iscom == true)
                    {
                        GetRD += rec;
                        if (GetRD.Length > 6)
                        {
                            if (GetRD.Substring(0, 4) == "7E7E")
                            {
                                GetRD = strShow.Substring(2, GetRD.Length - 2);//取最后两个7E
                            }
                        }
                    }
                    if (GetRD.Length > 50)
                    {
                        if (GetRD.Substring(0, 4) == "7E11" && GetRD.Length == 52 && rec == "AA")
                        {
                            //crc校验
                            byte crcsum = crc8(clsStringUtil.HexString2ByteArray(GetRD.Substring(2, 46)));
                            //确定本次读取的数据总条数
                            int value = Convert.ToInt32(GetRD.Substring(6, 4), 16);
                            Records = Records + 1;
                            if (crcsum.ToString("X2") == GetRD.Substring(48, 2))
                            {
                                ParseDataFromDevice(GetRD);
                                GetRD = string.Empty;
                                if (Records == value)//读完当天所有记录才发送消息读下一天
                                {
                                    Records = 0;
                                    iscom = false;
                                    if (MessageNotification.GetInstance() != null)
                                    {
                                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
                                    }
                                }
                            }
                            else if (Records == value)
                            {
                                //校验数据不对就清空，丢掉本条记录
                                GetRD = "";
                                if (MessageNotification.GetInstance() != null)
                                {
                                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
                                }
                            }
                            else 
                            {
                                //校验数据不对就清空，丢掉本条记录
                                GetRD = "";
                            }
                        }
                    }
                }
                else if (Global.communicatType == "通信测试")
                {
                    if (rec == "7E")
                    {
                        iscom = true;
                    }
                    if (iscom == true)
                    {
                        GetRD += rec;
                        if (GetRD.Length > 6)
                        {
                            if (GetRD.Substring(0, 4) == "7E7E")
                            {
                                GetRD = GetRD.Substring(2, GetRD.Length - 2);//取最后两个7E
                            }
                        }
                    }
                    if (GetRD.Length > 16)
                    {
                        if (GetRD.Substring(0, 4) == "7E01" && GetRD.Length == 18 && GetRD.Substring(16, 2) == "AA")
                        {
                            intRec = 0;
                            GetRD = string.Empty;
                            iscom = false;
                            if (MessageNotification.GetInstance() != null)
                            {
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "tongxin");
                            }
                        }
                    }
                }
                else if (Global.communicatType == "读记录")
                {
                    if (rec == "7E")
                    {
                        iscom = true;
                    }
                    if (iscom == true)
                    {
                        GetRD += rec;
                        if (GetRD.Length > 6)
                        {
                            if (GetRD.Substring(0, 4) == "7E7E")
                            {
                                GetRD = GetRD.Substring(2, GetRD.Length - 2);//取最后两个7E
                            }
                        }
                    }
                    if (GetRD.Length > 16)
                    {
                        if (GetRD.Length == 22 && GetRD.Substring(0, 4) == "7E13" && GetRD.Substring(14, 4) == "0000" && GetRD.Substring(20, 2) == "AA")
                        {
                            intRec = 0;
                            GetRD = string.Empty;
                            iscom = false;
                            if (MessageNotification.GetInstance() != null)
                            {
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "NoRecord");
                            }
                        }
                        else if (GetRD.Length == 22 && GetRD.Substring(0, 4) == "7E13" && GetRD.Substring(20, 2) == "AA")
                        {
                            if (MessageNotification.GetInstance() != null)//有记录
                            {
                                strShow = "";
                                GetRD = string.Empty;
                                iscom = false;
                                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "Record");
                            }
                        }
                    }
                }              
            }

                //strShow += rec;
                ////确定本次读取的数据总条数
                //if (strShow.Length > 8 && strShow.Length <= 12)
                //{
                //    if (strShow.Length == 10)
                //        intRec = c;
                //    else
                //        intRec += c * 256;
                //}
                ////通信测试
                //if (strShow.Length > 5)
                //{
                //    if (strShow.Substring(0, 4) == "7E01")
                //    {
                //        if (strShow.Length == 18 && strShow.Substring(16, 2) == "AA")
                //        {
                //            intRec = 0;
                //            strShow = string.Empty;
                //            if (MessageNotification.GetInstance() != null)
                //            {
                //                MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "tongxin");
                //            }
                //        }
                //    }
                //}
                ////查询读取日期是否有检测记录
                //if (strShow.Length > 6)
                //{
                //    if (strShow.Substring(0, 4) == "7E13")
                //    {
                //        if (strShow.Length == 22)
                //        {
                //            if (strShow.Substring(14, 4) == "0000" && strShow.Substring(20, 2) == "AA")//没有记录
                //            {
                //                if (MessageNotification.GetInstance() != null)
                //                {
                //                    strShow = "";
                //                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "NoRecord");
                //                }
                //            }
                //            else
                //            {
                //                if (MessageNotification.GetInstance() != null)//有记录
                //                {
                //                    strShow = "";
                //                    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "Record");
                //                }
                //            }
                //        }
                //    }
                //}

                //if (strShow.Length == intRec * 52 && intRec > 0)
                //{
                //    ParseDataFromDevice(strShow);
                //    intRec = 0;
                //    strShow = string.Empty;
                //}
               // m_DataRead = string.Empty;
            
        }

        /// <summary>
        /// 读取历史数据,传入的时间数据一定保证
        /// LZ-4000T读取设备检测数据
        /// request:(FLAG)0x7E (CMD)0x10 (LEN)0x0003 (DATA)year month day CRC8 (FLAG)0xAA
        /// </summary>
        /// <param name="dataTime"></param>
        public void ReadHistory(DateTime dataTime)
        {
            try
            {
                Global.communicatType = "读数据";
                Global.SendFirst = true;//判断第一个字节的帧头
                iscom = false;
                Records = 0;
                strShow = "";
                GetRD = "";
                //发送第一个日期是否有记录请求
                string head = "7E100300", strTime = (dataTime.Year - 2000).ToString("X2") + dataTime.Month.ToString("X2") + dataTime.Day.ToString("X2");
                byte checkSum = crc8(clsStringUtil.HexString2ByteArray("100300" + strTime));              
                string crc = checkSum.ToString("X2");
                byte[] sendData = clsStringUtil.HexString2ByteArray(head + strTime + crc + "AA");
                Send(sendData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 读取记录数
        /// </summary>
        /// <param name="dataTime"></param>
        public void ReadRecord(DateTime dataTime)
        {
            try
            {
                Global.communicatType = "读记录";
                Global.SendFirst = true;//判断第一个字节的帧头
                iscom = false;
                strShow = "";
                Records = 0;
                GetRD = "";
                //发送第一个日期是否有记录请求
                string head = "7E120300", strTime = (dataTime.Year - 2000).ToString("X2") + dataTime.Month.ToString("X2") + dataTime.Day.ToString("X2");
                byte checkSum = crc8(clsStringUtil.HexString2ByteArray("120300" + strTime));
                string crc = checkSum.ToString("X2");
                byte[] sendData = clsStringUtil.HexString2ByteArray(head + strTime + crc + "AA");
                Send(sendData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 发送通信测试指令
        /// </summary>
        public void Communicate()
        {
            Global.communicatType = "通信测试";
            Records = 0;
            strShow = "";
            GetRD = "";
            Global.SendFirst = true;//判断第一个字节的帧头
            iscom = false;
            byte checkSum = crc8(clsStringUtil.HexString2ByteArray("0002000000"));
            string crc = checkSum.ToString("X2");
            byte[] sendData = clsStringUtil.HexString2ByteArray("7E0002000000"+crc+"AA");
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

        /// <summary>
        /// CRC8和校验
        /// time：2016年6月1日
        /// 作者：wenj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string CheckDataSum(string str)
        {
            int len = str.Length / 2;
            string temp = string.Empty;
            byte bt = 0;
            for (int i = 0; i < len; i++)
            {
                temp = str.Substring(i * 2, 2);
                bt += Convert.ToByte(temp, 16);
            }
            return bt.ToString("X2");
        }

        /// <summary>
        /// 验证数据是否有效
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] checkData(byte[] data)
        {
            byte[] rtnData = null;
            int dataCount = 0;
            int len = 26;
            for (int i = 0; i < data.Length; i++)
            {
                //验证标识头和CMD
                if (data[i] == 0x7E && data[i + 1] == 0x11)
                {
                    //获取总记录数
                    dataCount = data[i + 5] > 0x00 ? data[i + 5] * 256 + data[i + 4] : data[i + 4];
                    if (dataCount > 0 && data[i + (dataCount * len) - 1] == 0xAA)
                    {
                        if (i == 0 && data[data.Length - 1] == 0xAA)
                        {
                            rtnData = new byte[dataCount * len];
                            Array.Copy(data, rtnData, dataCount * len);
                            return rtnData;
                        }
                    }
                    else
                    {
                        data = Remove(data);
                        i = -1;
                    }
                }
                else
                {
                    data = Remove(data);
                    i = -1;
                }
            }
            return rtnData;
        }

        /// <summary>
        /// 移除数组中下标0的值
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte[] Remove(byte[] array)
        {
            int length = array.Length;
            byte[] result = new byte[length - 1];
            Array.Copy(array, result, 0);
            Array.Copy(array, 1, result, 0, length - 1);
            return result;
        }

        private void ParseDataFromDevice(string input)
        {
            string scmd = input.Substring(8, 4);
            string temp = string.Empty;
            byte[] data = clsStringUtil.HexString2ByteArray(input);
           // data = checkData(data);
            List<byte[]> dataList = new List<byte[]>();
            int dtIndex = 0;
            byte[] dt = new byte[26];
            for (int j = 0; j < data.Length; j++)
            {
                dt[dtIndex] = data[j];
                if (dtIndex > 24)
                {
                    dataList.Add(dt);
                    dt = new byte[26];
                    dtIndex = -1;
                }
                dtIndex++;
            }
            string item = string.Empty;
            string product = string.Empty;
            string unit = string.Empty;
            string xmff = string.Empty;
            string num = string.Empty;
            string chValue = string.Empty;
            string sjky = "否";
            string datetime = string.Empty;
            byte[] btname = new byte[6];
            for (int i = 0; i < dataList.Count; i++)
            {
                datetime = "20" + dataList[i][16].ToString("D2") + "-" + dataList[i][17].ToString("D2") + "-" + dataList[i][18].ToString("D2") + " "
                    + dataList[i][19].ToString("D2") + ":" + dataList[i][20].ToString("D2") + ":" + dataList[i][21].ToString("D2");
                num = (dataList[i][8] + dataList[i][9] * 256).ToString("D4");
                item = CheckItemsArray[0, 0];
                btname[0] = dataList[i][10];
                btname[1] = dataList[i][11];
                btname[2] = dataList[i][12];
                btname[3] = dataList[i][13];
                btname[4] = dataList[i][14];
                btname[5] = dataList[i][15];
                product = Encoding.Default.GetString(btname);
                unit = CheckItemsArray[0, 3];
                xmff = CheckItemsArray[0, 2];
                chValue = ((double)(dataList[i][22] + dataList[i][23] * 256) / 100).ToString("F2");
                AddNewHistoricData(num, item, product, chValue, unit, datetime, sjky);
                //SaveResult(num, item, product, chValue, unit, datetime, sjky);
            }
            //if (MessageNotification.GetInstance() != null)
            //{
            //    strShow = "";
            //    MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadLZ4000TData, "");
            //}
        }
        private void AddNewHistoricData(string num, string item, string product, string checkValue, string unit, string time, string p)
        {
            DataRow dr = DataReadTable.NewRow();
            strWhere.Clear();
            strWhere.AppendFormat(" SampleName='{0}' and CheckData='{1}'", product.Replace("\0\0", "").Trim(), checkValue);//返回样品数据末尾包含“\0\0”            
            strWhere.AppendFormat(" and Checkitem='{0}' AND CheckTime=#{1}#", item, DateTime.Parse(time));
            
            DateTime dt = DateTime.Parse(time);
            dr["已保存"] = sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否";
            dr["样品名称"] = product.Replace("\0\0", "").Trim();
            dr["检测项目"] = item;
            dr["检测项目小类"] = "有机磷和氨基甲酸酯类";
            dr["检测结果"] = checkValue;
            dr["单位"] = unit;//检测值
            dr["检测依据"] = "GB/T 5009.199-2003";
            dr["标准值"] = "50";
            dr["检测仪器"] = Global.ChkManchine;
            dr["结论"] = Convert.ToDecimal(checkValue) < 50 || Convert.ToDecimal(checkValue)==50 ? "合格":"不合格";
            dr["检测时间"] = time;
            //dr["检测单位"] = Global.DetectUnit;
            //dr["检测单位编号"] = Global.DetectUnitNo;
            dr["经营户"] = "";
            dr["经营户身份证号"] = "";
            dr["摊位编号"] = "";
            dr["检测员"] = Global.userlog;
            dr["复核人"] = Global.userlog;
            dr["单位类别"] = "";
            dr["初检/复检"] = "初检";
            dr["备注"] = "无";

            strWhere.Length = 0;
            strWhere.AppendFormat("SubItemName='{0}'", product.Replace("\0\0", "").Trim());
            dta = sqlSet.GetKSsample(strWhere.ToString(), "", out err);
            if (dta != null && dta.Rows.Count > 0)
            {
                dr["样品编号"] = dta.Rows[0]["SubItemCode"].ToString();
            }

            //dr["采样时间"] = dt.AddHours(-2).ToString();
            //dr["采样地点"] = BaseUnitInfo[1];
            //dr["被检单位"] = BaseUnitInfo[2];
            //dr["检测数量"] = "1";
            //dr["数量单位"] = "斤";
            //dr["生产日期"] = dt.AddDays(-5).ToString();
            //dr["送检日期"] = dt.AddDays(-1).ToString(); 
            //strWhere.Length = 0;
            //strWhere.AppendFormat("samplename='{0}'", product.Replace("\0\0", "").Trim());
            //dta = sqlSet.Getsampletype( strWhere.ToString(),"",out err);
            //if (dta != null && dta.Rows.Count > 0)
            //{
            //    dr["样品种类"] = dta.Rows[0][0].ToString();
            //}
            //else
            //{
            //    dr["样品种类"] = "蔬菜";
            //}
            DataReadTable.Rows.Add(dr);
        }
        private void SaveResult(string num, string item, string product, string checkValue, string unit, string time, string p)
        {
            string err = string.Empty;

            resultdata.Save = "是";
            resultdata.Gridnum = num;
            resultdata.Checkitem = item;
            resultdata.SampleName = product;
            resultdata.CheckData = checkValue;
            resultdata.Unit = unit;
            resultdata.CheckUnit = "";
            resultdata.CheckTime = DateTime.ParseExact(time, "yyyy/MM/dd hh:mm:ss", null);
            resultdata.Result = p;
            sqlSet.ResuInsert(resultdata, out err);
        }
        //private void ReadRecords(int input)
        //{
        //    uint little = clsStringUtil.ToLittleEndian((uint)input);
        //    string parameter = little.ToString("X8");
        //    string header = "03FF4500" + parameter + "0000";
        //    string checkSum = clsStringUtil.CheckDataSum(header);
        //    byte[] dataSent = clsStringUtil.HexString2ByteArray(header + checkSum + "00");
        //    Send(dataSent);
        //}  
        //public void Close()
        //{
        //    if (online)
        //    {
        //        auto = false;
        //        BeforeClose(false);
        //        InternalClose();
        //        rxException = null;
        //    }
        //}
          
    }
}
