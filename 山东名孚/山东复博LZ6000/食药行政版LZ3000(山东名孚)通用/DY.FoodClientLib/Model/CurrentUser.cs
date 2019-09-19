using System;

namespace DY.FoodClientLib
{
    /// <summary>
    ///��ǰ�û�
    /// </summary>
    public class CurrentUser
    {
        private static CurrentUser _Instance = null;
        private clsUserInfo _UserInfo;
        private string _Unit;

        private CurrentUser()
        {
            _UserInfo = new clsUserInfo();
        }

        /// <summary>
        /// ��ǰ�û�ʵ��
        /// </summary>
        /// <returns></returns>
        public static CurrentUser GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new CurrentUser();
            }

            return _Instance;
        }

        /// <summary>
        /// ��ǰ�û���Ϣ
        /// </summary>
        public clsUserInfo UserInfo
        {
            get
            {
                return _UserInfo;
            }
            set
            {
                _UserInfo = value;
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
    }
}
