using System;
using System.Collections.Generic;
using ThreadMessaging;

namespace BatteryManage
{
    [Serializable]
    public class Message
    {
        public int what;
        public int arg1;
        public int arg2;
        public int arg3;
        public string str1;
        public string str2;
        public string str3;
        public string str4;
        public string str5;
        public string str6;
        public List<string> ports;
        //2015-06-11Lee
        public Object obj1;
        public Object obj2;
        public Object obj3;
        public double double1;
        public double double2;
        public double double3;
        public bool bool1;
        public bool bool2;
        public bool bool3;
        //Download SampleStandard\CheckItems\
        public string SampleStandardName;
        public string CheckItemsTempList;
        public string CheckItems;
        public string Standard;
        public string CheckItemsAdapter;
        public string DownLoadTask;
        public string DownLoadCompany;
        public Queue<string> args;
        public byte[] data;
        public List<byte[]> datas;
        /// <summary>
        /// 1为电池充电放电状态数组、2为电池电量、3为电池原始电量状态（测试用）
        /// </summary>
        public List<byte[]> batteryData;
        public bool result;
        public string error = string.Empty;
        public string calibrationValue = "50";

        //2016年6月22日 wenj
        public String outError = string.Empty;

        public string Other { get; set; }

        public Message()
        {
            args = new Queue<string>();
        }
    }

    /// <summary>
    /// 这里的工作者线程，就相当于消费者，不断循环接收消息。
    /// 至于生产者，就是UI线程了，UI线程要做什么事情，都发送消息。
    /// 工作者线程，接收消息并处理。
    /// </summary>
    public class ChildThread : SingleRunnable
    {
        private IChannel channel;
        protected ChildThread target = null;

        public ChildThread()
            : base(true, false, true)
        {
            channel = new ThreadChannel(32);
        }

        protected override void Run()
        {
            try
            {
                Console.WriteLine(this.GetType().FullName + " Start");
                while (running)
                {
                    Message msg = (Message)channel.Receive();
                    HandleMessage(msg);
                }
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                Console.WriteLine(this.GetType().FullName + " Stopped (controlled interrupt)");
            }
        }

        protected virtual void HandleMessage(Message msg)
        {
            Console.WriteLine(this.GetType().FullName + " HandleMessage: " + msg.what);
        }

        public void SendMessage(Message msg, ChildThread recThread)
        {
            target = recThread;
            channel.Send(msg);
        }

    }
}