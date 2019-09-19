using System;

namespace DataSetModel
{
	/// <summary>
	/// clsUserInfo ��ժҪ˵����
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
        /// �û�ϵͳ����
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
        /// �û���¼���
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
        /// �û�����
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
        /// �û���¼����
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
        /// �û���λ����
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
        /// �Ƿ����Ա
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
