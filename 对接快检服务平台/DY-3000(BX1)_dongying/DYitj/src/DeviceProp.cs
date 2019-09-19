using System;
using System.Collections.Generic;

namespace AIO
{
    [Serializable]
    public class DeviceProp
    {
        [Serializable]
        // LEDWave[i][j]表示第i个检测孔的第j个LED灯的波长 
        public class DeviceHole
        {
            public static int[] TotalWaves = { 410, 536, 595, 620 };
            // LEDWave[i][j]表示第i个检测孔的第j个LED灯的波长
            public int[][] LEDWave;
            public int LedCount = 4;
            public const int MAX_HOLECOUNT = 48;
            public int HoleCount = 8;
            public const int MAX_SXTCOUNT = 8;
            public int SxtCount = 4;
            public int HmCount = 1;
            public DeviceHole()
            {
                LEDWave = new int[MAX_HOLECOUNT][];
                for (int i = 0; i < LEDWave.Length; ++i)
                {
                    LEDWave[i] = new int[LedCount];
                    for (int j = 0; j < LedCount; ++j)
                    {
                        LEDWave[i][j] = TotalWaves[j];
                    }
                }
            }

            // 
            public List<int> GetWaveList()
            {
                List<int> waves = new List<int>();
                for (int i = 0; i < HoleCount; ++i)
                {
                    for (int j = 0; j < LedCount; ++j)
                    {
                        if (!waves.Contains(LEDWave[i][j]))
                            waves.Add(LEDWave[i][j]);
                    }
                }
                waves.Sort();
                return waves;
            }

            // 根据给定的波长，获取可用的孔位信息。
            // 返回的数据是索引，如果对应孔位存在该波长，则值为索引，否则，值为-1
            public List<int> GetHoleWaveIdx(int wave)
            {
                List<int> ledIdx = new List<int>();
                int i,j;
                for(i = 0; i < HoleCount; ++i)
                {
                    for(j = 0; j < LedCount; ++j)
                    {
                        if(wave == LEDWave[i][j])
                        {
                            ledIdx.Add(j);
                            break;
                        }
                    }
                    if (j == LedCount)
                        ledIdx.Add(-1);
                }
                return ledIdx;
            }

            public string GetWaveHoleString(int wave)
            {
                List<int> ledIdx = GetHoleWaveIdx(wave);
                string str = "";
                for(int i = 0; i < ledIdx.Count; ++i)
                {
                    if (ledIdx[i] >= 0)
                    {
                        if ("".Equals(str))
                            str += (i + 1);
                        else
                            str += ", " + (i + 1);
                    }
                }
                if("".Equals(str))
                {
                    str = "无";
                }
                else
                {
                    str = "(" + str + ")";
                }
                return str;
            }
        }
        
    }
}
