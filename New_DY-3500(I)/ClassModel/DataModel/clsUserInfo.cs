using System;

namespace DataSetModel
{
	/// <summary>
	/// clsUserInfo 的摘要说明。
	/// </summary>
    public class clsUserInfo
    {
        public clsUserInfo()
        {
        }

        private string _UserCode;
        private string _LoginID;
        private string _Name;
        private string _PassWord;
        private string _UnitCode;
        private string _WebLoginID;
        private string _WebPassWord;
        private bool _IsAdmin;
        private bool _IsLock;
        private string _Remark;

        /// <summary>
        /// 用户系统代码
        /// </summary>
        public string UserCode
        {
            set
            {
                _UserCode = value;
            }
            get
            {
                return _UserCode;
            }
        }
        /// <summary>
        /// 用户登录编号
        /// </summary>
        public string LoginID
        {
            set
            {
                _LoginID = value;
            }
            get
            {
                return _LoginID;
            }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
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
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string PassWord
        {
            set
            {
                _PassWord = value;
            }
            get
            {
                return _PassWord;
            }
        }
        /// <summary>
        /// 用户单位代码
        /// </summary>
        public string UnitCode
        {
            set
            {
                _UnitCode = value;
            }
            get
            {
                return _UnitCode;
            }
        }
        public string WebLoginID
        {
            set
            {
                _WebLoginID = value;
            }
            get
            {
                return _WebLoginID;
            }
        }
        public string WebPassWord
        {
            set
            {
                _WebPassWord = value;
            }
            get
            {
                return _WebPassWord;
            }
        }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin
        {
            set
            {
                _IsAdmin = value;
            }
            get
            {
                return _IsAdmin;
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
