using System;
using System.Collections.Generic;
using System.Text;

namespace DY.Process
{
    public class BackgroundWorkerEventArgs : EventArgs
    {
        /// <summary>
        /// 后台程序运行时抛出的异常
        /// </summary>
        public Exception BackGroundException { get; set; }
    }
}
