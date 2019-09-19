
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
        /// 获取国家局数据库信息
        /// </summary>
        public const int MSG_GETCOUNTRYDATA = 10038;
        /// <summary>
        /// 样品信息下载
        /// </summary>
        public const int MSG_SAMPLE = 10039;
        /// <summary>
        /// 获取软件版本
        /// </summary>
        public const int MSG_SoftwareVersion = 10040;

    }
}
