using System;

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
        public bool UIFaceBcsp;
        public bool UIFaceYgmy;
        public bool UIFaceSyxwsw;
        public bool UpDateNowing = false;
        public string test = string.Empty;
        public bool Create = false;
        /// <summary>
        /// 广东省智慧云平台 是否验证快检单号
        /// </summary>
        public bool CheckSampleID = true;
    }
    [Serializable]
    public class Prints
    {
        public bool PrintUnit;// 单位
        public bool PrintDate;//检测日期
        public bool PrintItemCategory;//检测方法
        public bool PrintCompany;//被检单位
        public bool PrintInstrumentName;//检测设备
        public bool PrintUser;//检测人员
        public bool PrintReviewers;//审核人员
        public bool PrintQR;//是否打印二维码

    }
}
