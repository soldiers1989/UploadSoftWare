using System;
using System.Collections.Generic;

namespace BatteryManage
{
    public class WorkThread : ChildThread
    {
        private ComPort _port = new ComPort();
        private int _timeout = 0, _length = 0;

        private void console(byte[] data)
        {
            if (data == null || data.Length == 0) return;

            string strdt = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                strdt += strdt.Length == 0 ? data[i].ToString() : "|" + data[i];
            }

            System.Console.WriteLine(strdt);
        }

        protected override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            switch (msg.what)
            {
                //获取电池状态
                case MsgCode.MSG_GET_BATTERY:
                    msg.result = false;
                    try
                    {
                        if (_port.OpenBattery(msg.str1))
                        {
                            byte[] data;
                            msg.batteryData = new List<byte[]>();
                            //获取电池充电放电状态
                            data = getBatteryState(_port);
                            msg.batteryData.Add(data);
                            if (data != null)
                            {
                                System.Console.Write("电池充放电状态数据：");
                                console(data);
                                //0x00则表示没有电池，就不进行电量获取
                                if (data[4] != 0x00)
                                {
                                    //获取电池电量
                                    data = getBattery(_port);
                                    msg.batteryData.Add(data);
                                    if (data != null)
                                    {
                                        System.Console.Write("电池电量数据：");
                                        console(data);
                                        msg.result = true;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("获取电池电量失败");
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("没有电池");
                                    msg.batteryData.Add(null);
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("获取电池充电放电状态失败");
                            }
                            //获取电池原始数据
                            //msg.batteryData.Add(getBatteryType(_port));
                        }
                        else
                        {
                            System.Console.WriteLine(string.Format("{0} 串口打开失败", msg.str1));
                            msg.batteryData = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                    }

                    _port.Close();
                    if (this.target != null) target.SendMessage(msg, null);
                    break;
            }
        }

        /// <summary>
        /// 获取电池充电放电状态
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] getBatteryState(ComPort comPort)
        {
            _timeout = 1000;
            _length = 7;
            byte[] rtnData = new byte[_length];
            byte[] writeData = { 0x7E, 0x19, 0x00, 0x00, 0x19, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return rtnData;
                }
                else
                {
                    System.Console.WriteLine("充电放电状态校验失败！");
                }
            }
            else
            {
                System.Console.WriteLine("充电放电状态获取失败！");
            }
            return null;
        }

        /// <summary>
        /// 电池电量请求
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] getBattery(ComPort comPort)
        {
            _timeout = 1000;
            _length = 7;
            byte[] rtnData = new byte[_length];
            byte[] writeData = { 0x7E, 0x1B, 0x00, 0x00, 0x1B, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            //返回数据len个字节
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                if (checkCRC8(writeData, rtnData))
                {
                    return rtnData;
                }
                else
                {
                    System.Console.WriteLine("电量请求获取失败！");
                }
            }
            else
            {
                System.Console.WriteLine("电量请求获取失败！");
            }
            return null;
        }

        /// <summary>
        /// 电池原始数据状态请求
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] getBatteryType(ComPort comPort)
        {
            _timeout = 1000;
            _length = 9;
            byte[] rtnData = new byte[_length];
            byte[] writeData = { 0x7E, 0x1F, 0x00, 0x00, 0x1F, 0x7E };
            comPort.Clear();
            comPort.Write(writeData, 0, writeData.Length);
            //返回数据len个字节
            if (comPort.Read(rtnData, 0, _length, _timeout) == _length)
            {
                System.Console.Write("原始数据：");
                console(rtnData);
                return rtnData;
            }
            else
            {
                System.Console.WriteLine("电量原始数据状态获取失败！");
            }
            return null;
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="writeData">上位机发送的数据</param>
        /// <param name="rtnData">下位机返回的数据</param>
        /// <returns></returns>
        private bool checkCRC8(byte[] writeData, byte[] rtnData)
        {
            _length = rtnData.Length - 1;
            //校验标识头、尾
            if (rtnData[0] == rtnData[_length] && rtnData[_length] == 0x7E)
            {
                //0x01 0x02 AD通讯测试
                if (writeData[1] == 0x01 && rtnData[1] == 0x02)
                {
                    if (rtnData[4] == 0x06)
                    {
                        return true;
                    }
                }
                //0x11 0x12 AD校准
                else if (writeData[1] == 0x11 && rtnData[1] == 0x12)
                {
                    if (rtnData[4] == 0x12)
                    {
                        return true;
                    }
                }
                //0x15 0x16 读取AD数据
                else if (writeData[1] == 0x15 && rtnData[1] == 0x16)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    byte crc = 0x00;
                    //和校验
                    for (int i = 1; i <= _length + 3; i++)
                    {
                        crc += rtnData[i];
                    }
                    if (crc == rtnData[132])
                    {
                        return true;
                    }
                }
                //0x1B 0x1C 电池电量获取
                else if (writeData[1] == 0x1B && rtnData[1] == 0x1C)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    if (_length > 0)
                    {
                        return true;
                    }
                }
                //0x19 0x1A 电池充电放电状态获取
                else if (writeData[1] == 0x19 && rtnData[1] == 0x1A)
                {
                    _length = rtnData[3] + rtnData[2] * 256;
                    if (_length > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}