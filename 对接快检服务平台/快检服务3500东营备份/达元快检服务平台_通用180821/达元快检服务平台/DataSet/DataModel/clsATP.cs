using System;

namespace DYSeriesDataSet
{
    /// <summary>
    /// clsCheckItem 的摘要说明。
    /// </summary>
    public class clsATP
    {

        public clsATP() { }

        private int _ID;
        private string _user;
        private string _RLU;
        private string _result;
        private string _data;
        private string _time;
        private string _upper;
        private string _lower;

        public int ID
        {
            set
            {
                _ID = value;
            }
            get
            {
                return _ID;
            }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public string Atp_CheckName
        {
            set
            {
                _user = value;
            }
            get
            {
                return _user;
            }
        }
        /// <summary>
        /// RLU
        /// </summary>
        public string Atp_RLU
        {
            set
            {
                _RLU = value;
            }
            get
            {
                return _RLU;
            }
        }
        /// <summary>
        /// 结果
        /// </summary>
        public string Atp_Result
        {
            set
            {
                _result = value;
            }
            get
            {
                return _result;
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string Atp_CheckData
        {
            set
            {
                _data = value;
            }
            get
            {
                return _data;
            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public string Atp_CheckTime
        {
            set
            {
                _time = value;
            }
            get
            {
                return _time;
            }
        }
        /// <summary>
        /// 上限
        /// </summary>
        public string Atp_Upper
        {
            set
            {
                _upper = value;
            }
            get
            {
                return _upper;
            }
        }
        /// <summary>
        /// 下限
        /// </summary>
        public string Atp_Lower
        {
            set
            {
                _lower = value;
            }
            get
            {
                return _lower;
            }
        }

        public class StringList
        {
            private string _usr = string.Empty;

            public string usr
            {
                get { return _usr; }
                set { _usr = value; }
            }
            private string _pwd = string.Empty;

            public string pwd
            {
                get { return _pwd; }
                set { _pwd = value; }
            }
            private string _result = string.Empty;

            public string result
            {
                get { return _result; }
                set { _result = value; }
            }
        }

    }
}
