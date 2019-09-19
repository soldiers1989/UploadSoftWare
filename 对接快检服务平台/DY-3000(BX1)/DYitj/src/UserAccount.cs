using System;

namespace AIO
{
    [Serializable]
    public class UserAccount
    {
        public string UserName;
        public string UserPassword;
        public bool UIFaceOne;
        public bool UIFaceTwo;
        public bool UIFaceThree;
        public bool UIFaceFour;
        public bool UIFaceFive;
        public bool UIFaceBcsp;
        public bool UIFaceYgmy;
        public bool UIFaceSyxwsw;
        public bool UpDateNowing;
        public string test;
        public bool Create;
        /// <summary>
        /// 广东省智慧云平台 是否验证快检单号
        /// </summary>
        public bool CheckSampleID = true;

    }
}