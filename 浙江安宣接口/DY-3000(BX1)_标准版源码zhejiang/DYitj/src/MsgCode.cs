﻿
namespace AIO
{
    public class MsgCode
    {
        public const int MSG_COMM_TEST = 1000;
        public const int MSG_READ_AD = 1001;
        public const int MSG_READ_AD_CYCLE = 1007;
        public const int MSG_TIMER_WORK = 1002; // 通知定时工作线程进入循环
        public const int MSG_DETECTE_START = 1003; // 样品检测开始
        public const int MSG_READ_CAM = 1004;
        public const int MSG_SET_TCLINE = 1005; // 设置TC线
        public const int MSG_READ_GRAYVALUES = 1006;
        public const int MSG_READ_RGBVALUE = 1008; // 读取RGB的值
        public const int MSG_RECORD_INIT = 1009; // 给定一个目录，读取所有记录。
        public const int MSG_RECORD_QUERY = 1010; // 根据条件查询记录。
        public const int MSG_PRINT = 1011; // 通讯测试并打印。

        public const int MSG_CHECK_CONNECTION = 1012; // 检测连接状态
        public const int MSG_CHECK_SYNC = 1013; // 数据同步
        public const int MSG_DETECTE_START_HEAVYMETAL = 1014; // 检测重金属
        public const int MSG_COMM_TEST_HM = 1015; // 重金属通讯测试
        public const int MSG_READ_HEAVYMETAL = 1016; // 重金属数据

        //2015-06-11Lee
        public const int MSG_WSD_TEST = 1017;
        public const int MSG_GPS_TEST = 1018;
        public const int MSG_READ_WSD_CYCLE = 1019;
        public const int MSG_READ_GPS_CYCLE = 1020;

        public const int MSG_UPLOAD = 1021;
        public const int MSG_DownTask = 2000;
        public const int MSG_DownCompany = 2001;
        public const int MSG_DownCheckItems = 2002;

        public const int MSG_LED_ENABLE = 1022;
        public const int MSG_LED_DISABLE = 1023;
        /// <summary>
        /// 校准AD 1024
        /// </summary>
        public const int MSG_COMM_CABT = 1024;
        /// <summary>
        /// 金标卡 出卡
        /// </summary>
        public const int MSG_JBK_OUT = 10025;
        /// <summary>
        /// 金标卡 进卡
        /// </summary>
        public const int MSG_JBK_IN = 10026;
        /// <summary>
        /// 金标卡 测试
        /// </summary>
        public const int MSG_JBK_TEST = 10027;
        /// <summary>
        /// 金标卡 进卡and测试
        /// </summary>
        public const int MSG_JBK_InAndTest = 10028;
        /// <summary>
        /// 打印机通讯测试
        /// </summary>
        public const int MSG_COMP_TEST = 10029;
        /// <summary>
        /// 版本信息
        /// </summary>
        public const int MSG_GET_VERSION = 10030;
        /// <summary>
        /// 获取电池状态
        /// </summary>
        public const int MSG_GET_BATTERY = 10031;
        /// <summary>
        /// 金标卡校准
        /// </summary>
        public const int MSG_JBK_CBT = 10032;
        /// <summary>
        /// 金标卡校准
        /// </summary>
        public const int MSG_JBK_CKC = 10033;

        /// <summary>
        /// 获取国家局数据库信息
        /// </summary>
        public const int MSG_GETCOUNTRYDATA = 10038;
    }
}
