using System;
using System.Collections.Generic;
using System.Data;
using com.lvrenyang;
using DYSeriesDataSet;
using ThreadMessaging;

namespace AIO
{
    [Serializable]
    public class Message
    {
        public int what = 0;
        public int arg1 = 0;
        public int arg2 = 0;
        public int arg3 = 0;
        public string str1 = string.Empty;
        public string str2 = string.Empty;
        public string str3 = string.Empty;
        public string str4 = string.Empty;
        public string str5 = string.Empty;
        public string str6 = string.Empty;
        //2015-06-11Lee
        public Object obj1;
        public Object obj2;
        public Object obj3;
        public DataTable table;

        //2016年6月22日 wenj
        public List<tlsTtResultSecond> selectedRecords = null;

        public double double1 = 0;
        public double double2 = 0;
        public double double3 = 0;
        public bool bool1 = false;
        public bool bool2 = false;
        public bool bool3 = false;
        public string SampleStandardName = string.Empty;
        public string CheckItemsTempList = string.Empty;
        public string CheckItems = string.Empty;
        public string Standard = string.Empty;
        public string CheckItemsAdapter = string.Empty;
        public string DownLoadTask = string.Empty;
        public string DownLoadCompany = string.Empty;
        public Queue<string> args;
        public byte[] data;
        public bool result = false;
        public int cValue = 0;
        public int tValue = 0;
        public string errMsg = string.Empty;
        public string responseInfo = string.Empty;
        public ComPort port = null;
        public Message()
        {
            args = new Queue<string>();
        }

        public string Other { get; set; }
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
