using System;

namespace comModel
{
    public class DateUtils
    {
        // 获取当前日期表示的字符串
        public static string GetDateString()
        {
            // 看区域设置，里面有日期和时间
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static void WaitMs(long ms)
        {
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > ms)
                    break;
            }
        }

    }
}
