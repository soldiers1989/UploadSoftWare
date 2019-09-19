﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class clsDistrict
    {
        public clsDistrict()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private string _SysCode;
        private string _StdCode;
        private string _Name;
        private string _ShortCut;
        private long _DistrictIndex;
        private string _CheckLevel;
        private bool _IsLocal;
        private bool _IsReadOnly;
        private bool _IsLock;
        private string _Remark;
        private string _UDate;

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
        public string StdCode
        {
            set
            {
                _StdCode = value;
            }
            get
            {
                return _StdCode;
            }
        }
        public string Name
        {
            set
            {
                _Name = value;
            }
            get
            {
                return _Name;
            }
        }
        public string ShortCut
        {
            set
            {
                _ShortCut = value;
            }
            get
            {
                return _ShortCut;
            }
        }
        public long DistrictIndex
        {
            set
            {
                _DistrictIndex = value;
            }
            get
            {
                return _DistrictIndex;
            }
        }
        public string CheckLevel
        {
            set
            {
                _CheckLevel = value;
            }
            get
            {
                return _CheckLevel;
            }
        }
        public bool IsLocal
        {
            set
            {
                _IsLocal = value;
            }
            get
            {
                return _IsLocal;
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
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UDate
        {
            set
            {
                _UDate = value;
            }
            get
            {
                return _UDate;
            }
        }
    }
}
