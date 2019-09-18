using System;

namespace DY.FoodClientLib
{
    /// <summary>
    ///当前用户
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
        /// 当前用户实例
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
        /// 当前用户信息
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
