using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public class clsUpReturnData
    {
        private string _code = "";
        /// <summary>
        /// 返回数据编码
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        private string _description = "";
        /// <summary>
        /// 返回描述
        /// </summary>
        public string description
        {
            set { _description =value; }
            get { return _description; }
        }
        private string _result = "";
        /// <summary>
        /// 返回结果
        /// </summary>
        public string result
        {
            set { _result = value; }
            get { return _result; }
        }
        private string _status = "";
        /// <summary>
        /// 返回状态
        /// </summary>
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
    }
}
