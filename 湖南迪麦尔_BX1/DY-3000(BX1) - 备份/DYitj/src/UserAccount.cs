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
    [Serializable]
    public class Prints
    {
        public  bool PrintUnit ;// 单位
        public  bool PrintDate ;//检测日期
        public  bool PrintItemCategory ;//检测方法
        public  bool PrintCompany;//被检单位
        public  bool PrintInstrumentName ;//检测设备
        public  bool PrintUser ;//检测人员
        public  bool PrintReviewers ;//审核人员
        public  bool PrintQR;//是否打印二维码
        public bool XPrinter;//是否启用热干胶打印机
        public string XprinterName = "";//打印机名称
    }
}