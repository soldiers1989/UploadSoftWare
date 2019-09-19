using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationModel.Thread
{
    public class ThreadHandle
    {
    }
    //定义一个代理参数类,用以传递信息,可以根据自己的需要添加属性
    public class MessageEventArgs : EventArgs
    {
        public String Message; //传递辽符串信息
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
    public class SubThread
    {
        /*定义代理
         * 名称MessageEventHandler;
         * 参数
         *    object 是发送者
         *    MessageEventArgs 发送的信息
         * */
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        //定义事件
        public event MessageEventHandler MessageSend;
        /*
         * 说明:定义事件处理函数,当然这里也可以不用直接在引发事件时调用this.MessageSend(sender, e);
         * 这里的参数要和事件代理的参数一样
         * */
        public void OnMessageSend(object sender, MessageEventArgs e)
        {
            if (MessageSend != null)
                this.MessageSend(sender, e);
        }
        //定义一个线程
        public System.Threading.Thread sendthread;
        /*线程处理函数
         * 说明:每毫秒向主界面发送一次信息
         * */
        public void Sendding()
        {
            try
            {
                //while (true)
                //{
                //    System.Threading.Thread.Sleep(200);
                    this.OnMessageSend(this, new MessageEventArgs("恢复按钮"));
                //}
            }
            catch
            {
            }
        }
        //开始线程函数
        public void StartSend()
        {
            sendthread = new System.Threading.Thread(new System.Threading.ThreadStart(Sendding));
            sendthread.IsBackground = true;//设置为后台线程程序关闭时关闭线程
            sendthread.Start();
        }
        //结束线程函数
        public void EndSend()
        {
            sendthread.Abort();
        }
    }
}
