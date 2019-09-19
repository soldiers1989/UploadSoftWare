using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace DY.FoodClientLib
{
	public class MessageNotification
	{
		private static MessageNotification _mNotification = null;

		public delegate void DataReadEventHandler(object sender, NotificationEventArgs e);

		public event DataReadEventHandler DataRead;







        protected MessageNotification()
		{

		}


		public static MessageNotification GetInstance()
		{
			if (_mNotification == null)
			{
				_mNotification = new MessageNotification();
			}
			return _mNotification;
		}


		public void OnDataRead(NotificationInfo code, string message)
		{
			NotificationEventArgs e = new NotificationEventArgs(code, message);
			if (DataRead != null)
			{
				DataRead(this, e);
			}
		}

		public class NotificationEventArgs : EventArgs
		{
			private NotificationInfo _mNCode;

			private string _mStrMessage = string.Empty;

			public NotificationInfo Code
			{
				get
				{
					return _mNCode;
				}
				set
				{
					_mNCode = value;
				}
			}

			public string Message
			{
				get
				{
					return _mStrMessage;
				}
				set
				{
					_mStrMessage = value;
					if (_mStrMessage == null)
					{
						_mStrMessage = string.Empty;
					}
				}
			}

			public NotificationEventArgs()
			{

			}

			/// <summary>
			/// 构造函数
			/// </summary>
			/// <param name="code">举例更新</param>
			/// <param name="message">消息内容</param>
			public NotificationEventArgs(NotificationInfo code, string message)
			{
				_mNCode = code;
				_mStrMessage = message;
			}
		}


		public enum NotificationInfo
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
            /// 读取LZ4000T系列检测项目
            /// </summary>
            ReadLZ4000TItem,

			/// <summary>
			///  读取DY3000DY系列检测数据，自产仪器
			/// </summary>
			ReadDY3000DYData,
            /// <summary>
            /// 读取TL310数据
            /// </summary>
            ReadTL310Data,

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
