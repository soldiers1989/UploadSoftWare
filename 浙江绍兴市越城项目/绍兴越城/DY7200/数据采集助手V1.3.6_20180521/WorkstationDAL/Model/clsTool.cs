using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class clsTool
    {
        /// <summary>
        /// 存储样品信息和被检单位信息集合
        /// </summary>
        public static List<byte[]> dataList;
        public static string _readDatas = string.Empty;

        #region 全局标识
        /// <summary>
        /// LZ-4000(T)农残残留快速测试仪
        /// </summary>
        public static byte LZ4000T = 0x01;
        /// <summary>
        /// LZ-4000农残残留快速测试仪
        /// </summary>
        public static byte LZ4000 = 0x02;
        /// <summary>
        /// 仪器信息 目前仅有 LZ4000T(0x01) 和 LZ4000(0x02)
        /// </summary>
        public static byte DEVICEMODEL = 0x00;
        /// <summary>
        /// 获取仪器信息
        /// </summary>
        public static string GET_DEVICEMODEL = "GetDeviceModel";
        /// <summary>
        /// 读取样品信息
        /// </summary>
        public static string READ_PRODUCT = "productRead";
        /// <summary>
        /// 读取被检单位
        /// </summary>
        public static string READ_CHECKEDUNIT = "checkedUnitRead";
        /// <summary>
        /// 修改样品
        /// </summary>
        public static string UPDATE_PRODUCT = "UpdateProduct";
        /// <summary>
        /// 修改被检单位
        /// </summary>
        public static string UPDATE_CHECKEDUNIT = "UpdateCheckedUnit";
        /// <summary>
        /// 删除样品
        /// </summary>
        public static string DELETED_PRODUCT = "DelProduct";
        /// <summary>
        /// 删除被检单位
        /// </summary>
        public static string DELETED_CHECKEDUNIT = "DelCheckedUnit";
        /// <summary>
        /// 读取检测数据
        /// </summary>
        public static string READ_HIS = "readHis";
        /// <summary>
        /// 新增样品
        /// </summary>
        public static string ADD_PRODUCT = "AddProduct";
        /// <summary>
        /// 设置仪器时间
        /// </summary>
        public static string SET_DEVICETIME = "settingDeviceTime";
        /// <summary>
        /// 设置出厂编号
        /// </summary>
        public static string SET_SN = "settingSN";
        /// <summary>
        /// 设置检查时间
        /// </summary>
        public static string SET_CHECKTIME = "settingCheckTime";
        /// <summary>
        /// 设置是否自动打印
        /// </summary>
        public static string SET_PRINT = "settingPrint";
        /// <summary>
        /// WIFI设置
        /// </summary>
        public static string SET_WIFI = "settingWiFi";
        /// <summary>
        /// 蓝牙设置
        /// </summary>
        public static string SET_BLUETOOTH = "settingBluetooth";
        /// <summary>
        /// 服务器设置
        /// </summary>
        public static string SET_SERVER = "settingServer";
        /// <summary>
        /// 以太网设置
        /// </summary>
        public static string SET_ETHERNET = "settingEthernet";

        #endregion

        /// <summary>
        /// 字符串转byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] StrToBytes(string data)
        {
            string[] strArray = data.Split(' ');
            byte[] byffer = new byte[strArray.Length];
            int ii = 0;
            for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
            {
                Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);
                int decNum = 0;
                if (strArray[i] == "")
                {
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                }

                try    //防止输错，使其只能输入一个字节的字符
                {
                    byffer[ii] = Convert.ToByte(decNum);
                }
                catch (Exception)
                {

                }
                ii++;
            }
            return byffer;
        }


        public static string UTF8ToASCII(string data, int len)
        {
            string str = string.Empty;
            try
            {
                byte[] temp = Encoding.Default.GetBytes(data);
                if (temp.Length > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        try
                        {
                            str += temp[i].ToString("X2");
                            if (i < temp.Length - 1)
                                str += " ";
                        }
                        catch (Exception)
                        {
                            str += " 00";
                        }
                    }
                }
            }
            catch (Exception)
            {
                str = string.Empty;
            }
            return str;
        }


        public static string GetBuffer(string data, byte cmd)
        {
            string buffer = "7E ", strCRC = string.Empty, Crc = string.Empty;
            switch (cmd)
            {
                case 0x00://仪器信息
                    strCRC = "00 02 00 " + data;
                    break;
                case 0x22://WiFi
                    strCRC = "22 3C 00 " + data;
                    break;
                case 0x18://出厂编号
                    strCRC = "18 1E 00 " + data;
                    break;
                case 0x1A://检测时间
                    strCRC = "1A 02 00 " + data;
                    break;
                case 0x1C://自动打印
                    strCRC = "1C 01 00 " + data;
                    break;
                case 0x26://服务器设置
                    strCRC = "26 06 00 " + data;
                    break;
                case 0x24://以太网设置
                    strCRC = "24 0D 00 " + data;
                    break;
                case 0x20://仪器时间设置
                    strCRC = "20 06 00 " + data;
                    break;
                case 0x14://读取样品信息
                    strCRC = "14 00 00";
                    break;
                case 0x40://读取被检单位
                    strCRC = "40 00 00";
                    break;
                default:
                    break;
            }
            byte[] crcData = StrToBytes(strCRC);
            Crc = " " + CRC(crcData).ToString("X2");
            buffer += strCRC + Crc + " AA";
            return buffer;
        }

        /// <summary>
        /// CRC校验 国际标准
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static byte CRC(byte[] bt)
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
        /// 验证返回数据是否成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckData(string data, string type)
        {
            byte[] bt = StrToBytes(data);
            try
            {
                if (type.Equals(READ_HIS) && checkReadData(bt).Length > 0)
                {
                    //读取数据
                    return true;
                }
                else if (type.Equals(READ_PRODUCT))
                {
                    //样品信息读取响应
                    return checkData(bt, READ_PRODUCT);
                }
                else if (type.Equals(READ_CHECKEDUNIT))
                {
                    //被检单位信息读取响应
                    return checkData(bt, READ_CHECKEDUNIT);
                }
                else if (!type.Equals(READ_PRODUCT))
                {
                    if (bt[0] == 0x7e && bt[bt.Length - 1] == 0xaa)
                    {
                        //出厂编号设置响应 cmd 0x19； data 0x01 成功； 0x00 失败
                        if ((type.Equals(SET_SN) || type.Equals(SET_BLUETOOTH)) && (bt[1] == 0x19 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //检测时间响应 cmd 0x1B； data 0x01：成功， 0x00：失败
                        else if (type.Equals(SET_CHECKTIME) && (bt[1] == 0x1B && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //自动打印响应 cmd 0x1D； data 0x01：成功， 0x00：失败
                        else if (type.Equals(SET_PRINT) && (bt[1] == 0x1D && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //WiFi设置响应 cmd 0x23； data 0x01：成功， 0x00：失败
                        else if (type.Equals(SET_WIFI) && (bt[1] == 0x23 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //服务器设置响应 cmd 0x27； data 0x01：成功， 0x00：失败
                        else if (type.Equals(SET_SERVER) && (bt[1] == 0x27 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //以太网设置响应 
                        else if (type.Equals(SET_ETHERNET) && (bt[1] == 0x25 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //仪器时间设置响应 
                        else if (type.Equals(SET_DEVICETIME) && (bt[1] == 0x21 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //操作样品
                        else if ((type.Equals(UPDATE_PRODUCT) || type.Equals(ADD_PRODUCT) || type.Equals(DELETED_PRODUCT))
                            && (bt[1] == 0x17 && bt[4] == 0x01))
                        {
                            return true;
                        }
                        //获取仪器信息
                        else if (type.Equals(GET_DEVICEMODEL) && (bt[1] == 0x01))
                        {
                            DEVICEMODEL = bt[4];
                            return true;
                        }
                        //操作被检单位
                        else if ((type.Equals(UPDATE_CHECKEDUNIT) || type.Equals(DELETED_CHECKEDUNIT)) &&
                            (bt[1] == 0x43 && bt[4] == 0x01))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 验证检测数据是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string checkReadData(byte[] data)
        {
            int intRec = 0;
            _readDatas = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                _readDatas += data[i].ToString("X2");
                if (_readDatas.Length > 8 && _readDatas.Length <= 12)
                {
                    if (_readDatas.Length == 10)
                        intRec = data[i];
                    else
                        intRec += data[i] * 256;
                }
                //26*2
                if (_readDatas.Length == intRec * 84 && intRec > 0)
                {
                    return _readDatas;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 验证样品信息响应数据是否正确
        /// 同时将数据分解为样品List
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool checkData(byte[] data, string type)
        {
            int count = 0;
            dataList = new List<byte[]>();

            if (data[0] == 0x7E || data[data.Length - 1] == 0xAA)
            {
                if (type.Equals(READ_PRODUCT))//读取样品信息
                {
                    int len = 18;
                    //1、确定本次读取的样品总记录数
                    count = data[5] > 0x00 ? data[5] * 256 + data[4] : data[4];
                    //2、确定本次数据总字节数，每条样品记录18个字节
                    if (count > 0 && count * len == data.Length)
                    {
                        int index = 0;
                        for (int i = 0; i < count; i++)
                        {
                            byte[] bt = new byte[len];
                            Array.ConstrainedCopy(data, index, bt, 0, len);
                            if (bt[0] == 0x7E || bt[bt.Length - 1] == 0xAA)
                            {
                                dataList.Add(bt);
                                index += len;
                            }
                            else
                            {
                                dataList = new List<byte[]>();
                                return false;
                            }
                        }
                    }
                }
                else if (type.Equals(READ_CHECKEDUNIT))//读取被检单位信息
                {
                    int len = 28;
                    //1、确定本次读取的被检单位总记录数
                    count = data[5] > 0x00 ? data[5] * 256 + data[4] : data[4];
                    //2、确定本次数据总字节数，每条被检单位记录28个字节
                    if (count > 0 && count * len == data.Length)
                    {
                        int index = 0;
                        for (int i = 0; i < count; i++)
                        {
                            byte[] bt = new byte[len];
                            Array.ConstrainedCopy(data, index, bt, 0, len);
                            if (bt[0] == 0x7E || bt[bt.Length - 1] == 0xAA)
                            {
                                dataList.Add(bt);
                                index += len;
                            }
                            else
                            {
                                dataList = new List<byte[]>();
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    dataList = new List<byte[]>();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 将IP地址转换成十六进制的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string strIpSplit(string str, char symbol)
        {
            string rtn = string.Empty;
            string[] strList = str.Split(symbol);
            foreach (string item in strList)
            {
                string stritem = int.Parse(item).ToString("X2");
                rtn += rtn.Length == 0 ? stritem : " " + stritem;
            }
            return rtn;
        }

        /// <summary>
        /// 十进制的字符串转十六进制的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string strTo16(string str, int len)
        {
            string rtn = int.Parse(str).ToString("X2");
            if (rtn.Length > 2)
            {
                string value = string.Empty;
                if (rtn.Length == 3)
                {
                    value = rtn.Substring(rtn.Length - 2, 2) + " " + rtn.Substring(0, 1);
                }
                else if (rtn.Length == 4)
                {
                    value = rtn.Substring(rtn.Length - 2, 2) + " " + rtn.Substring(0, 2);
                }
                rtn = value;
            }
            else
            {
                if (len == 2)
                {
                    rtn += " 00";
                }
            }
            return rtn;
        }

        public static byte[] HexString2ByteArray(string input)
        {
            string temp = string.Empty;
            int len = input.Length / 2;
            byte[] bt = new byte[len];
            for (int i = 0; i < len; i++)
            {
                temp = input.Substring(i * 2, 2);
                bt[i] = Convert.ToByte(temp, 16);
            }
            return bt;
        }

        /// <summary>
        /// crc国际标准
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static byte crc8(byte[] bt)
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
        /// 获取DataGradView表列的名称
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string getColumnName(string column)
        {
            if (column.Equals("num"))
            {
                return "编号";
            }
            if (column.Equals("checkedUnit"))
            {
                return "被检单位";
            }
            else if (column.Equals("item"))
            {
                return "检测项目";
            }
            else if (column.Equals("productName"))
            {
                return "样品名称";
            }
            else if (column.Equals("checkValue"))
            {
                return "抑制率";
            }
            else if (column.Equals("unit"))
            {
                return "单位";
            }
            else if (column.Equals("time"))
            {
                return "检测时间";
            }
            return column;
        }
    }
}
