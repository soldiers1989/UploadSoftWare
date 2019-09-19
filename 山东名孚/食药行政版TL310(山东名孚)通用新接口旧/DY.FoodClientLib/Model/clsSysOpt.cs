using System;

namespace DY.FoodClientLib
{
    /// <summary>
    /// clsSysOpt 的摘要说明。
    /// </summary>
    public class clsSysOpt
    {
        public clsSysOpt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private string _SysCode;
        private string _OptDes;
        private string _OptType;
        private string _OptValue;
        private bool _IsDisplay;
        private bool _IsReadOnly;
        private bool _IsLock;
        private string _Remark;

        public string SysCode
        {
            set
            {
                _SysCode = value;
            }
            get
            {
                return _SysCode;
            }
        }
        public string OptDes
        {
            set
            {
                _OptDes = value;
            }
            get
            {
                return _OptDes;
            }
        }
        public string OptType
        {
            set
            {
                _OptType = value;
            }
            get
            {
                return _OptType;
            }
        }
        public string OptValue
        {
            set
            {
                _OptValue = value;
            }
            get
            {
                return _OptValue;
            }
        }
        public bool IsDisplay
        {
            set
            {
                _IsDisplay = value;
            }
            get
            {
                return _IsDisplay;
            }
        }
        public bool IsReadOnly
        {
            set
            {
                _IsReadOnly = value;
            }
            get
            {
                return _IsReadOnly;
            }
        }
        public bool IsLock
        {
            set
            {
                _IsLock = value;
            }
            get
            {
                return _IsLock;
            }
        }
        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
            }
        }
    }
}