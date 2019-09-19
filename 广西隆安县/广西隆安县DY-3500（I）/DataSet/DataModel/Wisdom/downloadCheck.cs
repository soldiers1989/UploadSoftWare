using System;
using System.Collections.Generic;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 快检结果下载
    /// </summary>
    public class downloadCheck
    {

        public downloadCheck() { }

        private string _result = string.Empty;
        /// <summary>
        /// 1代表有值，0代表无检测结果
        /// </summary>
        public string result
        {
            get { return _result; }
            set { _result = value; }
        }
        private string _deviceid = string.Empty;
        /// <summary>
        /// 仪器唯一机器码
        /// </summary>
        public string deviceid
        {
            get { return _deviceid; }
            set { _deviceid = value; }
        }
        private string _totalnum = string.Empty;
        /// <summary>
        /// 下载数据条数
        /// </summary>
        public string totalnum
        {
            get { return _totalnum; }
            set { _totalnum = value; }
        }

    }
}
