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

        private String _result = string.Empty;
        /// <summary>
        /// 1代表有值，0代表无检测结果
        /// </summary>
        public String result
        {
            get { return _result; }
            set { _result = value; }
        }
        private String _deviceid = string.Empty;
        /// <summary>
        /// 仪器唯一机器码
        /// </summary>
        public String deviceid
        {
            get { return _deviceid; }
            set { _deviceid = value; }
        }
        private String _totalnum = string.Empty;
        /// <summary>
        /// 下载数据条数
        /// </summary>
        public String totalnum
        {
            get { return _totalnum; }
            set { _totalnum = value; }
        }

    }
}
