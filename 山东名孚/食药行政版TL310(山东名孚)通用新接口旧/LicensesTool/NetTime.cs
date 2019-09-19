using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //需要使用多线程
using System.Net; //需要使用网络
using System.Net.Sockets; //需要使用网络。
using System.Runtime.InteropServices; 

namespace LicensesTool
{
    public class NetTime
    {
        private Thread thrTimeSync=null; 

        /// <summary>
        ///NetTime nt = new NetTime();
        ///nt.Start();
        ///DateTime dtNow = DateTime.Now;
        /// </summary>
        public NetTime()
        {
          
        }
        public void Start()
        {
            thrTimeSync = new Thread(new ThreadStart(TimeSyncProc));
            thrTimeSync.Start(); 
        }
        public void Stop()
        {
            if (thrTimeSync != null)
            {
                thrTimeSync.Abort();
            }
        }
        [DllImport("kernel32.dll")]
        public static extern int SetLocalTime(ref SystemTime lpSystemTime);
        public struct SystemTime
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        private void TimeSyncProc()
        {
            while (true)
            {
                TcpClient c = new TcpClient();
                c.Connect("www.time.ac.cn ", 37); //连接到国内授时服务器

                NetworkStream s;
                s = c.GetStream(); //读取数据流
                c.Close();

                byte[] buf = new byte[4];
                s.Read(buf, 0, 4); //把数据存到数组中

                uint utcTime;

                //把服务器返回数据转换成1900/1/1 UTC 到现在所经过的秒数
                utcTime = ((uint)buf[0] << 24) + ((uint)buf[1] << 16) + ((uint)buf[2] << 8) + (uint)buf[3];

                //得到真实的本地时间
                System.DateTime datetime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
                datetime = datetime.AddSeconds(utcTime).ToLocalTime();
                SystemTime sysDateTime = new SystemTime();

                sysDateTime.wYear = (short)datetime.Year;
                sysDateTime.wDay = (short)datetime.Day;
                sysDateTime.wMonth = (short)datetime.Month;

                sysDateTime.wHour = (short)datetime.Hour;
                sysDateTime.wMinute = (short)datetime.Minute;
                sysDateTime.wSecond = (short)datetime.Second;

                SetLocalTime(ref sysDateTime);

                System.Threading.Thread.Sleep(1 * 1000); 
            }
        }
    }
}
