using System;
using System.Collections.Generic;
using System.Text;

namespace FoodClient.ATP
{
    public static class Global
    {
        private static Synoxo.USBHidDevice.DeviceManagement MyDeviceManagement = new Synoxo.USBHidDevice.DeviceManagement();

        ///  <summary>
        ///  用VID和PID查找HID设备
        ///  </summary>
        ///  <returns>True： 找到设备</returns>
        public static Boolean FindTheHid()
        {
            try
            {
                String strvid = "0483", strpid = "5750";
                int myVendorID = 0x03EB;
                int myProductID = 0x2013;
                int vid = 0;
                int pid = 0;
                try
                {
                    vid = Convert.ToInt32(strvid, 16);
                    pid = Convert.ToInt32(strpid, 16);
                    myVendorID = vid;
                    myProductID = pid;
                }
                catch (SystemException e)
                {
                    throw e;
                }
                if (MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                {
                    getCommunication();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 获取数据指令
        /// </summary>
        /// <returns></returns>
        public static String getCmd(int index)
        {
            String str = "0x08 0x31 0x00 0x00 0x" + index.ToString("X2");
            byte crc = 0;
            byte[] btList = StringToBytes(str, new string[] { ",", " " }, 16);
            for (int i = 0; i < btList.Length; i++)
                crc += btList[i];

            str = "0xFF 0x08 0x31 0x00 0x00 0x" + index.ToString("X2") + " 0x" + crc.ToString("X2") + " 0xFE";

            return str;
        }

        /// <summary>
        /// 将给定的字符串按照给定的分隔符和进制转换成字节数组
        /// </summary>
        /// <param name="str">给定的字符串</param>
        /// <param name="splitString">分隔符</param>
        /// <param name="fromBase">给定的进制</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] StringToBytes(string str, string[] splitString, int fromBase)
        {
            string[] strBytes = str.Split(splitString, StringSplitOptions.RemoveEmptyEntries);
            if (strBytes == null || strBytes.Length == 0)
                return null;
            byte[] _return = new byte[strBytes.Length];
            for (int i = 0; i < strBytes.Length; i++)
            {
                try
                {
                    _return[i] = Convert.ToByte(strBytes[i], fromBase);
                }
                catch (SystemException ex)
                {
                    throw ex;
                    //MessageBox.Show("发现不可转换的字符串->" + strBytes[i]);
                }
            }
            return _return;
        }

        public static List<byte[]> getByteList(byte[] data)
        {
            List<byte[]> dataList = new List<byte[]>();
            int index = 3;
            for (int i = 0; i < 3; i++)
            {
                byte[] bt = new byte[18];
                for (int j = 0; j < bt.Length; j++)
                {
                    index++;
                    bt[j] = data[index];
                    if (index == 21 || index == 39 || index == 57)
                        dataList.Add(bt);
                }
            }
            return dataList;
        }

        /// <summary>
        /// 获取通讯
        /// </summary>
        /// <returns></returns>
        public static void getCommunication()
        {
            //建立通讯
            String cmd = "0xFF 0x08 0x30 0x00 0x00 0x00 0x38 0xFE";
            byte[] bt = ReadAndWriteToDevice(cmd);
        }

        /// <summary>
        /// 读取下位机返回的数据
        /// </summary>
        /// <returns></returns>
        public static byte[] ReadAndWriteToDevice(String cmd)
        {
            int len = 64;
            byte[] inputdatas = new byte[len];
            try
            {
                byte[] outdatas = new byte[len];
                outdatas[0] = 0x55;
                outdatas[1] = 0x2;
                outdatas[2] = 0x1;
                outdatas[3] = 0x00;
                byte[] inputs = StringToBytes(cmd, new string[] { ",", " " }, 16);
                if (inputs != null && inputs.Length > 0)
                    outdatas = inputs;
                System.Windows.Forms.Application.DoEvents();
                //MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, 100);
                if (MyDeviceManagement.WriteReport(0, false, outdatas, ref inputdatas, 100))
                {
                    int length = 0;
                    while (!MyDeviceManagement.ReadReport(0, false, outdatas, ref inputdatas, 100))
                    {
                        length++;
                        if (length == 20)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return inputdatas;
        }

    }
}
