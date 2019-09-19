using System;
using System.IO.Ports;
using System.Linq;

namespace com.lvrenyang
{
    public class ComPort : IDisposable
    {
        private SerialPort mSerial = null;
        private RxBuffer rxBuffer = new RxBuffer(90000);
        public int bufferlength = 0;
        public static bool isver = false;
        public static int dlength = 0;


        public ComPort()
        {
            mSerial = new SerialPort();
            mSerial.DataReceived += SerialPort_DataReceived;
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
                //mSerial.ReadTimeout = 3000;
                //mSerial.WriteTimeout = 3000;
                mSerial.Open();
            }
            catch (System.IO.IOException ex)
            {
                FileUtils.Log(ex.ToString());
                // 如果设备未就绪，则重试
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
            return mSerial.IsOpen;
        }

        public bool Open(string port)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Open(port, 256000))
                    return true;
                else
                    DateUtils.WaitMs(300);
            }
            return mSerial.IsOpen;
        }

        public void Close()
        {
            try
            {
                mSerial.Close();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        public void Clear()
        {
            try
            {
                mSerial.DiscardOutBuffer();
                mSerial.DiscardInBuffer();
                rxBuffer.Clear();
                bufferlength = 0;
                dlength = 0;
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
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
                System.Console.WriteLine("发送成功：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Join(",", buffer));
                writeCount = count;
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
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
                do
                {
                    if (!mSerial.IsOpen)
                        throw new Exception("SerialPort is not open");
                    int len = mSerial.BytesToRead;
                    if (len <= 0)
                        return;
                    byte[] buf = new byte[len];

                    mSerial.Read(buf, 0, len);//读取缓冲数据

                    if (isver)//摄像头读取数据 
                    {
                        rxBuffer.CopyArray(buf, bufferlength);//复制到缓存数组
                        bufferlength = bufferlength + len;//记录缓存的数据长度
                        Console.WriteLine("长度：" + bufferlength);
                        dlength = bufferlength;
                    }
                    else
                    {
                        for (int i = 0; i < len; ++i)
                            rxBuffer.PutByte(buf[i]);
                    }
                }
                while (mSerial.BytesToRead > 0);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                mSerial.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        /// <summary>
        /// 读取缓存数组数据
        /// </summary>
        /// <returns></returns>
        public byte[] ReadData()
        {
            byte[] Rdata;
            Rdata = rxBuffer.ReadData(bufferlength);
            Console.WriteLine("缓存长度：" + bufferlength);
            return Rdata;
        }
    }
}
