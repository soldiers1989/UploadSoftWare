using System;
using System.IO.Ports;
using System.Linq;

namespace BatteryManage
{
    public class ComPort : IDisposable
    {
        private SerialPort mSerial = null;
        private RxBuffer rxBuffer = new RxBuffer(4096);
        private bool disposed = false;
        private bool Isopen = false;//串口打开标志

        public ComPort()
        {
            mSerial = new SerialPort();
            mSerial.DataReceived += SerialPort_DataReceived;
        }

        /// <summary>
        /// 新一体机打印机端口打开测试
        /// </summary>
        /// <param name="port">port</param>
        /// <param name="baudrate">9600</param>
        /// <returns>true OR false</returns>
        private bool NewOpen(string port, int baudrate)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                if (!ports.Contains<string>(port))
                    throw new Exception("端口：" + port + "不存在");
                mSerial.PortName = port;
                mSerial.BaudRate = baudrate;
                mSerial.Parity = Parity.None;
                mSerial.DataBits = 8;
                mSerial.StopBits = StopBits.One;
                mSerial.ReadBufferSize = 0x10000;
                mSerial.WriteBufferSize = 0x10000;
                mSerial.Open();
            }
            catch (System.IO.IOException ex)
            {
                //FileUtils.Log(ex.ToString());
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
            }
            return mSerial.IsOpen;
        }

        private bool Open(string port, int baudrate)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                if (!ports.Contains<string>(port))
                    throw new Exception("端口：" + port + "不存在");
                mSerial.PortName = port;
                mSerial.BaudRate = baudrate;
                mSerial.Parity = Parity.None;
                mSerial.DataBits = 8;
                mSerial.StopBits = StopBits.One;
                mSerial.ReadBufferSize = 0x10000;
                mSerial.WriteBufferSize = 0x10000;
                mSerial.Open();
                Isopen = true;
            }
            catch (System.IO.IOException ex)
            {
                //FileUtils.Log(ex.ToString());
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
            }
            return mSerial.IsOpen;
        }

        /// <summary>
        /// 新打印机波特率9600
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool NewOpen(string port)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (NewOpen(port, 9600))
                    return true;
                else
                    Global.WaitMs(100);
            }
            return mSerial.IsOpen;
        }

        public bool OpenBattery(string port)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Open(port, 115200))
                    return true;
                else
                    Global.WaitMs(100);
            }
            return mSerial.IsOpen;
        }

        public bool Open(string port)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Open(port, 115200))
                    return true;
                else
                    Global.WaitMs(100);
            }
            return mSerial.IsOpen;
        }

        public void Close()
        {
            try
            {
                mSerial.Close();
                Isopen = false;
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
            }
        }
        /// <summary>
        /// 串口打开标志
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            if (mSerial.IsOpen)
            {
                Isopen = true;
            }
            else
            {
                Isopen = false;
            }
            return Isopen;
        }

        public void Clear()
        {
            try
            {
                mSerial.DiscardOutBuffer();
                mSerial.DiscardInBuffer();
                rxBuffer.Clear();
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
            }
        }

        public int Write(byte[] buffer, int offset, int count)
        {
            int writeCount = 0;
            try
            {
                if (!mSerial.IsOpen)
                    throw new Exception("SerialPort is not open");
                mSerial.Write(buffer, offset, count);
                writeCount = count;
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
                writeCount = 0;
            }
            return writeCount;
        }

        public int Read(byte[] buffer, int offset, int count, int timeout)
        {
            int index = 0;
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (index == count)
                    break;

                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                    break;

                if (!rxBuffer.IsEmpty())
                {
                    buffer[offset + index] = rxBuffer.GetByte();
                    index++;
                }
            }
            return index;
        }

        /// <summary>
        /// 串口接受数据并显示
        /// </summary>
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)//Getting data from Comm Port
        {
            try
            {
                if (!mSerial.IsOpen)
                    throw new Exception("SerialPort is not open");
                int len = mSerial.BytesToRead;
                if (len <= 0)
                    return;
                byte[] buf = new byte[len];

                mSerial.Read(buf, 0, len);//读取缓冲数据
                for (int i = 0; i < len; ++i)
                    rxBuffer.PutByte(buf[i]);
            }
            catch (Exception ex)
            {
                //FileUtils.Log(ex.ToString());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                mSerial.Dispose();
            }
            disposed = true;
        }

    }
}