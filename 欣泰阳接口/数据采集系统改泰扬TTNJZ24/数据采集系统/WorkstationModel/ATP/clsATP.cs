using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationModel.ATP
{
    public class clsATP
    {
        public clsATP() { }

        private int _ID;
        private String _user;
        private String _RLU;
        private String _result;
        private String _data;
        private String _time;
        private String _upper;
        private String _lower;

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
        public String Atp_CheckName
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
        public String Atp_RLU
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
        public String Atp_Result
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
        public String Atp_CheckData
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
        public String Atp_CheckTime
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
        public String Atp_Upper
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
        public String Atp_Lower
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
            private String _usr = "";

            public String usr
            {
                get { return _usr; }
                set { _usr = value; }
            }
            private String _pwd = "";

            public String pwd
            {
                get { return _pwd; }
                set { _pwd = value; }
            }
            private String _result = "";

            public String result
            {
                get { return _result; }
                set { _result = value; }
            }
        }
    }
}
