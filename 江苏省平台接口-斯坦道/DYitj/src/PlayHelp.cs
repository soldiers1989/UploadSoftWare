
namespace AIO.src
{
    public class PlayHelp
    {
        /// <summary>
        /// 播放状态
        /// </summary>
        public enum PLAY_STATE
        {
            /// <summary>
            /// 正在加载
            /// </summary>
            PS_READY = 0,
            /// <summary>
            /// 正在打开
            /// </summary>
            PS_OPENING = 1,
            /// <summary>
            /// 正在暂停
            /// </summary>
            PS_PAUSING = 2,
            /// <summary>
            /// 暂停
            /// </summary>
            PS_PAUSED = 3,
            /// <summary>
            /// 正在播放
            /// </summary>
            PS_PLAYING = 4,
            /// <summary>
            /// 播放
            /// </summary>
            PS_PLAY = 5,
            /// <summary>
            /// 正在关闭
            /// </summary>
            PS_CLOSING = 6,
        }

        public enum Scrol_State
        {
            /// <summary>
            /// 静止
            /// </summary>
            None = 0,
            /// <summary>
            /// 正常移动
            /// </summary>
            NormalMove = 1,
            /// <summary>
            /// 手动移动
            /// </summary>
            ManualMove = 2,
            /// <summary>
            /// 手动移动开始
            /// </summary>
            MoveBegin = 3,
            /// <summary>
            /// 手动移动结束
            /// </summary>
            MoveEnd = 4
        }
        /// <summary>
        /// 根据毫秒获取时分秒
        /// </summary>
        /// <param name="whs">表示毫秒的数字</param>
        /// <returns></returns>
        public string GetTime(int whs)
        {
            string time = "";
            int h = whs / 3600000;
            int m = (whs / 60000) % 60;
            int s = (whs / 1000) % 60;
            time += h.ToString();
            if (m.ToString().Length < 2)
                time += ":0" + m.ToString();
            else
                time += ":" + m.ToString();

            if (s.ToString().Length < 2)
                time += ":0" + s.ToString();
            else
                time += ":" + s.ToString();
            return time;
        }

    }
}
