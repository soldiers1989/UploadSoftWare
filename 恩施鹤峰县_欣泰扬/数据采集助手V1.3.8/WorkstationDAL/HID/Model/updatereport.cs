using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class updatereport
    {
        #region Field Members
        private string _ID;
        private string _item;
        private string _SampleName;
        private string _chkdata;
        private string _Unit;
        private string _Result;
        private string _ChkTime;
        private string _machine;
        private string _chkUnit;
        private string _save;
        private string _getsmplttime;
        private string _getsampleAddr;
        private string _plannum;
        private string _testbase;
        private string _limitdata;
        private string _Tester;
        private string _retester;
        private string _manage;




        /// <summary>
        ///序列号
        /// </summary> 
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public string item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }
        public string SampleName
        {
            get
            {
                return _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }
        public string chkdata
        {
            get
            {
                return _chkdata;
            }
            set
            {
                _chkdata = value;
            }
        }
        public string Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }
        public string Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }

        public string ChkTime
        {
            get
            {
                return _ChkTime;
            }
            set
            {
                _ChkTime = value;
            }
        }

        public string machine
        {
            get
            {
                return _machine;
            }
            set
            {
                _machine = value;
            }
        }
        public string chkUnit
        {
            get
            {
                return _chkUnit;
            }
            set
            {
                _chkUnit = value;
            }
        }
        public string save
        {
            get
            {
                return _save;
            }
            set
            {
                _save = value;
            }
        }
        public string getsmplttime
        {
            get
            {
                return _getsmplttime;
            }
            set
            {
                _getsmplttime = value;
            }
        }
        public string getsampleAddr
        {
            get
            {
                return _getsampleAddr;
            }
            set
            {
                _getsampleAddr = value;
            }
        }
        public string plannum
        {
            get
            {
                return _plannum;
            }
            set
            {
                _plannum = value;
            }
        }
        public string testbase
        {
            get
            {
                return _testbase;
            }
            set
            {
                _testbase = value;
            }
        }
        public string limitdata
        {
            get
            {
                return _limitdata;
            }
            set
            {
                _limitdata = value;
            }
        }
        public string Tester
        {
            get
            {
                return _Tester;
            }
            set
            {
                _Tester = value;
            }
        }
        public string retester
        {
            get
            {
                return _retester;
            }
            set
            {
                _retester = value;
            }
        }
        public string manage
        {
            get
            {
                return _manage;
            }
            set
            {
                _manage = value;
            }
        }
        #endregion
    }
}
