using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DY.FoodClientLib
{
    ///<summary>   
    ///   当信息更改是通知其他的窗口重新加载数据   
    ///   使用方法为：
    ///  1）通知信息更改(在更改的窗口调用)   
    ///   MessageNotify.Instance().SendMessage(NotifyInfo.InfoAdd,"提示消息");   
    ///   其中第一个参数为信息号，第二个参数为信息描述   
    /// 
    ///   2）收取信息(在另一个窗口中)
    ///   使用方法，在每个在构造函数中加入如下语句   
    ///    MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent; 
    ///  
    /// 同时编写如下的方法用于重新加载数据   
    ///protected void OnNotifyEvent(object sender,MessageNotify.NotifyEventArgs e)   
    ///{   
    /// if(e.Code == MessageNotity.NotifyInfo.InfoAdd)   
    /// {
    ///     //控件数据重新绑定
    /// }
    ///}   
    ///</summary>   
    public class MessageNotify
    {
        /// <summary>
        /// 消息自身实例
        /// </summary>
        private static MessageNotify _mNotify = null;

        /// <summary>
        /// 消息委托事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MsgNotifyEvent(object sender, NotifyEventArgs e);

        /// <summary>
        /// 消息事件对象
        /// </summary>
        public event MsgNotifyEvent OnMsgNotifyEvent;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected MessageNotify()
        {
        }

        /// <summary>
        /// 获得自身实例,单例实现
        /// </summary>
        /// <returns></returns>
        public static MessageNotify Instance()
        {
            if (_mNotify == null)
            {
                _mNotify = new MessageNotify();
            }
            return _mNotify;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void SendMessage(NotifyInfo code, string message)
        {
            NotifyEventArgs e = new NotifyEventArgs(code, message);
            if (OnMsgNotifyEvent != null)
            {
                OnMsgNotifyEvent(this, e);
            }
        }

        /// <summary>
        /// 更新消息事件
        /// </summary>
        public class NotifyEventArgs : System.EventArgs
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public NotifyEventArgs()
            {

            }
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="code">举例更新</param>
            /// <param name="message">消息内容</param>
            public NotifyEventArgs(NotifyInfo code, string message)
            {
                _mNCode = code;
                _mStrMessage = message;
            }

            private NotifyInfo _mNCode;
            private string _mStrMessage = string.Empty;
        

            /// <summary>
            /// 消息更新区域类型
            /// </summary>
            public NotifyInfo Code
            {
                get { return _mNCode; }
                set { _mNCode = value; }
            }

            /// <summary>
            /// 消息
            /// </summary>
            public string Message
            {
                get { return _mStrMessage; }
                set
                {
                    _mStrMessage = value;
                    if (_mStrMessage == null)
                    {
                        _mStrMessage = string.Empty;
                    }
                }
            }
       
        }

        /// <summary>
        /// 各种更新信号枚举
        /// </summary>
        public enum NotifyInfo
        {
            /// <summary>
            /// 删除时发生的消息
            /// </summary>
            InfoAdd,

            /// <summary>
            /// 修改时发生的消息
            /// </summary>
            InfoEidt,

            /// <summary>
            /// 删除发生的消息
            /// </summary>
            InfoDelete,

            /// <summary>
            /// 读取DY3000DY系列检测项目
            /// </summary>
            ReadDY3000DYItem,

            /// <summary>
            ///  读取DY3000DY系列检测数据，自产仪器
            /// </summary>
            ReadDY3000DYData,

            /// <summary>
            ///  读取LZ4000T检测数据，自产仪器
            /// </summary>
            ReadLZ4000TData,

            /// <summary>
            /// 5000LD,3000LD系列数据
            /// </summary>
            Read3000LDData,

            /// <summary>
            /// 读取旧版DY5000数据
            /// </summary>
            Read5000Data,

            /// <summary>
            /// 读取肉类水份仪数据
            /// </summary>
            ReadSFYData,

            /// <summary>
            /// 新版肉类水分仪
            /// </summary>
            ReadDY6400Data,

            /// <summary>
            /// 干式化学分析仪
            /// </summary>
            ReadDY6200Data,

            /// <summary>
            /// DY8120检测数据
            /// </summary>
            ReadDY8120Data,

            /// <summary>
            /// DY723pc检测数据
            /// </summary>
            ReadDY723PCData,

            /// <summary>
            /// 表明是系统超级管理员
            /// </summary>
            SystemAdmin,
        }
    }
}
