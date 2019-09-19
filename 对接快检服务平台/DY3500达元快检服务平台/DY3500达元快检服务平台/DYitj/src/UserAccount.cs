﻿using System;

namespace AIO
{
    [Serializable]
    public class UserAccount
    {
        public string UserName = string.Empty;
        public string UserPassword = string.Empty;
        public bool UIFaceOne = false;
        public bool UIFaceTwo = false;
        public bool UIFaceThree = false;
        public bool UIFaceFour = false;
        public bool UIFaceFive = false;
        public bool UpDateNowing = false;
        public string test = string.Empty;
        public bool Create = false;
        /// <summary>
        /// 广东省智慧云平台 是否验证快检单号
        /// </summary>
        public bool CheckSampleID = false;
    }
}
