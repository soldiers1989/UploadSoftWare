using System;
using System.Collections.Generic;
using System.Windows.Documents;
using com.lvrenyang;

namespace AIO
{
    // 计算前，都需要判断无效。
    public class FgdCaculate
    {
        public const double _VALUE_INVALID = Double.MinValue;
        public const double _VALUE_OVERFLOW = _VALUE_INVALID + 1;
        public static bool IsValid(double v)
        {
            if ((v != _VALUE_INVALID) && (v != _VALUE_OVERFLOW))
                return true;
            else
                return false;
        }

        public struct AT
        {
            public double t; // t 值
            public double a; // a 值
        }

        public struct FAT 
        {
            public double t; // t 值
            public double a; // a 值
        }

        public struct DeltaA
        {
            public double deltaA; // deltaA 值
        }

        // 检测孔AD值
        public class HolesAD
        {
            public int LED_ROW = 0;
            public int LED_COL = 0;
            public int LED_NUMS = 0;
            public double[][][] adValues; // 减去暗电流的AD值
            public double[] darkAdValues; // 暗电流
            public HolesAD(int row, int col, int nums)
            {
                LED_ROW = row;
                LED_COL = col;
                LED_NUMS = nums;

                adValues = new double[LED_ROW][][];
                for (int i = 0; i < LED_ROW; ++i)
                {
                    adValues[i] = new double[LED_COL][];
                    for (int j = 0; j < LED_COL; ++j)
                    {
                        adValues[i][j] = new double[LED_NUMS];
                    }
                }
                darkAdValues = new double[LED_ROW];
            }
        }

        // 检测孔T值
        public class HolesT
        {
            public int LED_ROW = 0;
            public int LED_COL = 0;
            public int LED_NUMS = 0;
            public double[][][] tValues; // T值
            public HolesT(int row, int col, int nums)
            {
                LED_ROW = row;
                LED_COL = col;
                LED_NUMS = nums;

                tValues = new double[LED_ROW][][];
                for (int i = 0; i < LED_ROW; ++i)
                {
                    tValues[i] = new double[LED_COL][];
                    for (int j = 0; j < LED_COL; ++j)
                    {
                        tValues[i][j] = new double[LED_NUMS];
                    }
                }
            }
        }

        public class HolesA
        {
            public int LED_ROW = 0;
            public int LED_COL = 0;
            public int LED_NUMS = 0;
            public double[][][] aValues; // T值
            public HolesA(int row, int col, int nums)
            {
                LED_ROW = row;
                LED_COL = col;
                LED_NUMS = nums;
                aValues = new double[LED_ROW][][];
                for (int i = 0; i < LED_ROW; ++i)
                {
                    aValues[i] = new double[LED_COL][];
                    for (int j = 0; j < LED_COL; ++j)
                    {
                        aValues[i][j] = new double[LED_NUMS];
                    }
                }
            }
        }

        public class TLimit
        {
            public double T_MAX_VALUE = 1.2;
            public double _100Min = 0.998; // 抑制率法
            public double _100Max = 1.002;
            public TLimit(double tMax, double _100Min, double _100Max)
            {
                this.T_MAX_VALUE = tMax;
                this._100Min = _100Min;
                this._100Max = _100Max;
            }
        }

        public class ALimit
        {
            public double A_MAX_VALUE = 5;
            public ALimit(double aMax)
            {
                this.A_MAX_VALUE = aMax;
            }
        }

        // 原始数据转为AD值
        public static HolesAD RawDataToAD(byte[] data)
        {
            int idx = 1;
            int LED_ROW = data[idx];
            int LED_COL = 8;
            int LED_NUMS = 4;
            HolesAD ad = new HolesAD(LED_ROW, LED_COL, LED_NUMS);
            idx = 2;
            for (int i = 0; i < LED_ROW; ++i)
            {
                ++idx;// 1个byte的长度
                // 灯全灭，暗电流
                ad.darkAdValues[i] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
                idx += 4;

                for (int j = 0; j < LED_COL; ++j)
                {
                    for (int k = 0; k < LED_NUMS; ++k)
                    {
                        ad.adValues[i][j][k] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
                        idx += 4;

                        if (ad.adValues[i][j][k] < ad.darkAdValues[i])
                            ad.adValues[i][j][k] = 0;
                        else
                            ad.adValues[i][j][k] -= ad.darkAdValues[i];

                        // adValues[offset] * 1000 / 0x10000 ,将AD转换成3.3V的千分之几。直观显示。
                    }
                }
                ++idx;// 1个byte的校验
            }
            return ad;
        }

        // 透光率
        public static HolesT CaculateT(HolesAD ad, HolesAD fullAd, TLimit limit)
        {
            HolesT t = new HolesT(ad.LED_ROW, ad.LED_COL, ad.LED_NUMS);
            for (int i = 0; i < ad.LED_ROW; ++i)
            {
                for (int j = 0; j < ad.LED_COL; ++j)
                {
                    for (int k = 0; k < ad.LED_NUMS; ++k)
                    {
                        if (0 == fullAd.adValues[i][j][k])
                        {
                            t.tValues[i][j][k] = _VALUE_INVALID;
                        }
                        else
                        {
                            t.tValues[i][j][k] = ad.adValues[i][j][k] * 1.0 / fullAd.adValues[i][j][k];
                            // 以下情况做特别处理
                            if (t.tValues[i][j][k] >= limit.T_MAX_VALUE)
                            {
                                t.tValues[i][j][k] = _VALUE_OVERFLOW;
                            }
                            else if ((t.tValues[i][j][k] >= limit._100Min) && (t.tValues[i][j][k] <= limit._100Max))
                            {
                                t.tValues[i][j][k] = 1;
                            }
                        }

                    }
                }
            }
            return t;
        }



        /// <summary>
        /// 透光率|分光度TEST
        /// </summary>
        /// <param name="ad"></param>
        /// <param name="fullAd"></param>
        /// <param name="limit"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<double> CaculateT(HolesAD ad, HolesAD fullAd, TLimit limit, string str)
        {
            List<double> dbList = new List<double>();
            HolesT t = new HolesT(ad.LED_ROW, ad.LED_COL, ad.LED_NUMS);
            for (int i = 0; i < ad.LED_ROW; ++i)
            {
                for (int j = 0; j < ad.LED_COL; ++j)
                {
                    for (int k = 0; k < ad.LED_NUMS; ++k)
                    {
                        if (0 != fullAd.adValues[i][j][k])
                        {
                            t.tValues[i][j][k] = ad.adValues[i][j][k] * 1.0 / fullAd.adValues[i][j][k];
                            // 以下情况做特别处理
                            if (t.tValues[i][j][k] >= limit.T_MAX_VALUE)
                            {
                                t.tValues[i][j][k] = _VALUE_OVERFLOW;
                            }
                            else if ((t.tValues[i][j][k] >= limit._100Min) && (t.tValues[i][j][k] <= limit._100Max))
                            {
                                t.tValues[i][j][k] = 1;
                            }
                            dbList.Add(t.tValues[i][j][k]);
                        }
                        else
                        {
                            dbList.Add(0);
                        }
                    }
                }
            }
            return dbList;
        }

        // 将透光率转化为字符串
        public static string T_To_String(double v)
        {
            string str = string.Empty;
            if (v == _VALUE_INVALID)
            {
                str = " ****";
            }
            else if (v == _VALUE_OVERFLOW)
            {
                str = "---.-";
            }
            else
            {
                str = String.Format("{0:P2}", v);
            }
            return str;
        }

        // 吸光度 标准曲线法，只需要计算吸光度即可。
        public static HolesA CaculateA(HolesAD ad, HolesAD fullAd, HolesT t, ALimit limit)
        {
            HolesA a = new HolesA(t.LED_ROW, t.LED_COL, t.LED_NUMS);
            for (int i = 0; i < t.LED_ROW; ++i)
            {
                for (int j = 0; j < t.LED_COL; ++j)
                {
                    for (int k = 0; k < t.LED_NUMS; ++k)
                    {
                        if (_VALUE_INVALID == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = _VALUE_INVALID;
                        }
                        else if (_VALUE_OVERFLOW == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = _VALUE_OVERFLOW;
                        }
                        else if (1 == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = 0;
                        }
                        else
                        {
                            if (ad.adValues[i][j][k] < 1)
                            {
                                a.aValues[i][j][k] = 5;
                            }
                            else
                            {
                                a.aValues[i][j][k] = Math.Log10(fullAd.adValues[i][j][k] / ad.adValues[i][j][k]);
                            }
                        }
                    }
                }
            }
            return a;
        }

        public static List<double> CaculateA(HolesAD ad, HolesAD fullAd, HolesT t, ALimit limit, string str)
        {
            List<double> tgList = new List<double>();
            HolesA a = new HolesA(t.LED_ROW, t.LED_COL, t.LED_NUMS);
            for (int i = 0; i < t.LED_ROW; ++i)
            {
                for (int j = 0; j < t.LED_COL; ++j)
                {
                    for (int k = 0; k < t.LED_NUMS; ++k)
                    {
                        if (_VALUE_INVALID == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = _VALUE_INVALID;
                        }
                        else if (_VALUE_OVERFLOW == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = _VALUE_OVERFLOW;
                        }
                        else if (1 == t.tValues[i][j][k])
                        {
                            a.aValues[i][j][k] = 0;
                        }
                        else
                        {
                            if (ad.adValues[i][j][k] < 1)
                            {
                                a.aValues[i][j][k] = 5;
                            }
                            else
                            {
                                a.aValues[i][j][k] = Math.Log10(fullAd.adValues[i][j][k] / ad.adValues[i][j][k]);
                            }
                        }
                        tgList.Add(a.aValues[i][j][k]);
                    }
                }
            }
            return tgList;
        }

        // 函数    将吸光度转换为字符串
        public static string A_To_String(double v)
        {
            string str = string.Empty;
            if (v == _VALUE_INVALID)
            {
                str = " *****";
            }
            else if (v == _VALUE_OVERFLOW)
            {
                str = " -.---";
            }
            else
            {
                str = String.Format("{0:F3}", v);
            }
            return str;
        }

        /// <summary>
        ///  农残计算deltaA
        /// </summary>
        /// <param name="first">初始值</param>
        /// <param name="last">结束值</param>
        /// /// <param name="contrast">是否是对照</param>
        /// <returns></returns>
        public static DeltaA[] CaculateDeltaA(AT[] first, AT[] last, bool contrast)
        {
            DeltaA[] result = new DeltaA[first.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                // 数据无效
                if ((!IsValid(first[i].a) && !IsValid(last[i].a)))
                {
                    result[i].deltaA = _VALUE_INVALID;
                }
                // 计算 DeltaA的值
                else
                {
                    //对照
                    if (contrast)
                    {
                        // 结束值 > 初始值
                        if ((last[i].a > first[i].a))
                        {
                            result[i].deltaA = last[i].a - first[i].a;
                        }
                        // 结束值必须大于初始值才有效，若相等(初始值和结束值都在很靠近0)的话则代表无任何变化，可判定为未放比色皿
                        else
                        {
                            result[i].deltaA = _VALUE_INVALID;
                        }
                        contrast = false;
                    }
                    //样品
                    else
                    {
                        if ((System.Math.Abs(last[i].a) >= 0.01))
                        {
                            result[i].deltaA = last[i].a - first[i].a;
                        }
                        else
                        {
                            result[i].deltaA = _VALUE_INVALID;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 农残计算抑制率
        /// </summary>
        /// <param name="deltaA">数据集</param>
        /// <param name="refDeltaA">对照值</param>
        /// <returns></returns>
        public static double[] CaculateIzhilv(DeltaA[] deltaA, double refDeltaA)
        {
            double[] izhilv = new double[deltaA.Length];
            for (int i = 0; i < deltaA.Length; ++i)
            {
                // 对照和样品都为有效数
                if (IsValid(refDeltaA) && IsValid(deltaA[i].deltaA))
                {
                    // 对照为0则数据无效(除数不能为0)
                    if (refDeltaA == 0)
                    {
                        izhilv[i] = _VALUE_INVALID;
                    }
                    // 对照 - 样品 ＜ 0 时
                    else if (((refDeltaA - deltaA[i].deltaA) < 0))
                    {
                        // 样品 < 0.15 归零，否则无效//2016年12月2日改为归零
                        //izhilv[i] = (deltaA[i].deltaA < 0.15) ? 0 : _VALUE_INVALID;
                        izhilv[i] = 0;
                    }
                    else
                    {
                        izhilv[i] = (refDeltaA - deltaA[i].deltaA) / refDeltaA;
                        if (izhilv[i] > 1)
                        {
                            izhilv[i] = _VALUE_INVALID;
                        }
                    }
                }
                // 对照和样品都为无效输时，结果无效
                else
                    izhilv[i] = _VALUE_INVALID;
            }
            return izhilv;
        }

        // 标准曲线法计算吸光度
        public static double[] HoleStatusToSCA(AT[] holeStatus, double refA)
        {
            double[] a = new double[holeStatus.Length];
            for (int i = 0; i < holeStatus.Length; ++i)
            {
                if (IsValid(holeStatus[i].a) && IsValid(refA))
                    a[i] = holeStatus[i].a - refA;
                else
                    a[i] = _VALUE_INVALID;
            }
            return a;
        }

        // 动力学法，计算一分钟的变化率。
        public static double[] CaculateChangeA(DeltaA[] deltaA, int time)
        {
            double[] perA = new double[deltaA.Length];
            for (int i = 0; i < deltaA.Length; ++i)
            {
                if (IsValid(deltaA[i].deltaA))
                    perA[i] = deltaA[i].deltaA;// time;2016年6月1日 应邓工要求去掉除以时间
                else
                    perA[i] = _VALUE_INVALID;
            }
            return perA;
        }

        /// <summary>
        /// 计算吸光度的值需要根据曲线的区间来计算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="dsOrbs"></param>
        /// <returns></returns>
        public static double WeiTiaoNew(double x, double a, double b, double c, double d, double dsOrbs)
        {
            double xFix = 0;
            if (IsValid(x))
                xFix = (a + b * x + c * x * x + d * x * x * x) * dsOrbs;
            else
                xFix = _VALUE_INVALID * dsOrbs;
            return xFix;
        }

        // 公式a+bx+cx2+dx3 微调
        public static double[] WeiTiaoX(double[] x, double a, double b, double c, double d, List<double> dsOrbs)
        {
            //Global.aValue改为dsOrbs 可以单独为每个通道设置不同的值
            double[] xFix = new double[x.Length];
            for (int i = 0; i < xFix.Length; ++i)
            {
                if (IsValid(x[i]))
                    xFix[i] = (a + b * x[i] + c * x[i] * x[i] + d * x[i] * x[i] * x[i]) * dsOrbs[i];
                else
                    //xFix[i] = a + b * x[i] + c * x[i] * x[i] + d * x[i] * x[i] * x[i];
                    xFix[i] = _VALUE_INVALID * dsOrbs[i];
            }
            return xFix;
        }

        public static double[] WeiTiaoXnew(double[] x, double a, List<double> dsOrbs)
        {
            double[] xFix = new double[x.Length];
            for (int i = 0; i < xFix.Length; ++i)
            {
                if (IsValid(x[i]))
                    xFix[i] = x[i] * a * dsOrbs[i];
                else
                    //xFix[i] = a + b * x[i] + c * x[i] * x[i] + d * x[i] * x[i] * x[i];
                    xFix[i] = _VALUE_INVALID * dsOrbs[i];
            }
            return xFix;
        }

        public static double[] CaculateKOH(int count, List<double> dsOrbs, double a0, double a1, double a2, double a3)
        {
            double[] koh = new double[count];
            for (int i = 0; i < count; ++i)
            {
                koh[i] = DataUtils.GetThreeOrderEqutionSolution(dsOrbs[i], a0, a1, a2, a3);
            }
            return koh;
        }

    }
}

#region
//namespace AIO
//{
//    // 计算前，都需要判断无效。
//    public class FgdCaculate
//    {
//        public const double VALUE_INVALID = Double.MinValue;
//        public const double VALUE_OVERFLOW = VALUE_INVALID + 1;

//        public static bool IsValid(double v)
//        {
//            if ((v != VALUE_INVALID) && (v != VALUE_OVERFLOW))
//                return true;
//            else
//                return false;
//        }

//        public struct AT
//        {
//            public double t; // t 值
//            public double a; // a 值
//        }

//        public struct DeltaA
//        {
//            public double deltaA; // deltaA 值
//        }
//        // 检测孔AD值
//        public class HolesAD
//        {
//            public int LED_ROW = 0;
//            public int LED_COL = 0;
//            public int LED_NUMS = 0;
//            public double[][][] adValues; // 减去暗电流的AD值
//            public double[] darkAdValues; // 暗电流

//            public HolesAD(int row, int col, int nums)
//            {
//                LED_ROW = row;
//                LED_COL = col;
//                LED_NUMS = nums;

//                adValues = new double[LED_ROW][][];
//                for (int i = 0; i < LED_ROW; ++i)
//                {
//                    adValues[i] = new double[LED_COL][];
//                    for (int j = 0; j < LED_COL; ++j)
//                    {
//                        adValues[i][j] = new double[LED_NUMS];
//                    }
//                }
//                darkAdValues = new double[LED_ROW];
//            }

//        }

//        // 检测孔T值
//        public class HolesT
//        {
//            public int LED_ROW = 0;
//            public int LED_COL = 0;
//            public int LED_NUMS = 0;
//            public double[][][] tValues; // T值

//            public HolesT(int row, int col, int nums)
//            {
//                LED_ROW = row;
//                LED_COL = col;
//                LED_NUMS = nums;

//                tValues = new double[LED_ROW][][];
//                for (int i = 0; i < LED_ROW; ++i)
//                {
//                    tValues[i] = new double[LED_COL][];
//                    for (int j = 0; j < LED_COL; ++j)
//                    {
//                        tValues[i][j] = new double[LED_NUMS];
//                    }
//                }
//            }

//        }

//        public class HolesA
//        {
//            public int LED_ROW = 0;
//            public int LED_COL = 0;
//            public int LED_NUMS = 0;
//            public double[][][] aValues; // T值

//            public HolesA(int row, int col, int nums)
//            {
//                LED_ROW = row;
//                LED_COL = col;
//                LED_NUMS = nums;

//                aValues = new double[LED_ROW][][];
//                for (int i = 0; i < LED_ROW; ++i)
//                {
//                    aValues[i] = new double[LED_COL][];
//                    for (int j = 0; j < LED_COL; ++j)
//                    {
//                        aValues[i][j] = new double[LED_NUMS];
//                    }
//                }
//            }
//        }

//        public class TLimit
//        {
//            public double T_MAX_VALUE = 1.2;
//            public double _100Min = 0.995; // 抑制率法
//            public double _100Max = 1.005;

//            public TLimit(double tMax, double _100Min, double _100Max)
//            {
//                this.T_MAX_VALUE = tMax;
//                this._100Min = _100Min;
//                this._100Max = _100Max;
//            }
//        }

//        public class ALimit
//        {
//            public double A_MAX_VALUE = 5;
//            public ALimit(double aMax)
//            {
//                this.A_MAX_VALUE = aMax;
//            }
//        }

//        // 原始数据转为AD值
//        public static HolesAD RawDataToAD(byte[] data)
//        {
//            int idx = 1;
//            int LED_ROW = data[idx];
//            int LED_COL = 8;
//            int LED_NUMS = 4;
//            HolesAD ad = new HolesAD(LED_ROW, LED_COL, LED_NUMS);
//            idx = 2;
//            for (int i = 0; i < LED_ROW; ++i)
//            {
//                ++idx;// 1个byte的长度

//                // 灯全灭，暗电流
//                ad.darkAdValues[i] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
//                idx += 4;

//                for (int j = 0; j < LED_COL; ++j)
//                {
//                    for (int k = 0; k < LED_NUMS; ++k)
//                    {
//                        ad.adValues[i][j][k] = ((UInt32)data[idx]) + ((UInt32)(data[idx + 1] << 8)) + ((UInt32)(data[idx + 2] << 16)) + ((UInt32)(data[idx + 3] << 24));
//                        idx += 4;

//                        if (ad.adValues[i][j][k] < ad.darkAdValues[i])
//                            ad.adValues[i][j][k] = 0;
//                        else
//                            ad.adValues[i][j][k] -= ad.darkAdValues[i];

//                        // adValues[offset] * 1000 / 0x10000 ,将AD转换成3.3V的千分之几。直观显示。
//                    }
//                }

//                ++idx;// 1个byte的校验
//            }
//            return ad;
//        }

//        // 透光率
//        public static HolesT CaculateT(HolesAD ad, HolesAD fullAd, TLimit limit)
//        {
//            HolesT t = new HolesT(ad.LED_ROW, ad.LED_COL, ad.LED_NUMS);

//            for (int i = 0; i < ad.LED_ROW; ++i)
//            {
//                for (int j = 0; j < ad.LED_COL; ++j)
//                {
//                    for (int k = 0; k < ad.LED_NUMS; ++k)
//                    {
//                        if (0 == fullAd.adValues[i][j][k])
//                        {
//                            t.tValues[i][j][k] = VALUE_INVALID;
//                        }
//                        else
//                        {
//                            t.tValues[i][j][k] = ad.adValues[i][j][k] * 1.0 / fullAd.adValues[i][j][k];
//                            // 以下情况做特别处理
//                            if (t.tValues[i][j][k] >= limit.T_MAX_VALUE)
//                            {
//                                t.tValues[i][j][k] = VALUE_OVERFLOW;
//                            }
//                            else if ((t.tValues[i][j][k] >= limit._100Min) && (t.tValues[i][j][k] <= limit._100Max))
//                            {
//                                t.tValues[i][j][k] = 1;
//                            }
//                        }

//                    }
//                }
//            }

//            return t;
//        }

//        // 将透光率转化为字符串
//        public static string T_To_String(double v)
//        {
//            string str = string.Empty;
//            if (v == VALUE_INVALID)
//            {
//                str = " ****";
//            }
//            else if (v == VALUE_OVERFLOW)
//            {
//                str = "---.-";
//            }
//            else
//            {
//                str = String.Format("{0:P1}", v);
//            }
//            return str;
//        }

//        // 吸光度 标准曲线法，只需要计算吸光度即可。
//        public static HolesA CaculateA(HolesAD ad, HolesAD fullAd, HolesT t, ALimit limit)
//        {
//            HolesA a = new HolesA(t.LED_ROW, t.LED_COL, t.LED_NUMS);

//            for (int i = 0; i < t.LED_ROW; ++i)
//            {
//                for (int j = 0; j < t.LED_COL; ++j)
//                {
//                    for (int k = 0; k < t.LED_NUMS; ++k)
//                    {
//                        if (VALUE_INVALID == t.tValues[i][j][k])
//                        {
//                            a.aValues[i][j][k] = VALUE_INVALID;
//                        }
//                        else if (VALUE_OVERFLOW == t.tValues[i][j][k])
//                        {
//                            a.aValues[i][j][k] = VALUE_OVERFLOW;
//                        }
//                        else if (1 == t.tValues[i][j][k])
//                        {
//                            a.aValues[i][j][k] = 0;
//                        }
//                        else
//                        {
//                            if (ad.adValues[i][j][k] < 1)
//                            {
//                                a.aValues[i][j][k] = 5;
//                            }
//                            else
//                            {
//                                a.aValues[i][j][k] = Math.Log10(fullAd.adValues[i][j][k] / ad.adValues[i][j][k]);
//                            }
//                        }

//                    }
//                }
//            }

//            return a;
//        }

//        // 函数    将吸光度转换为字符串
//        public static string A_To_String(double v)
//        {
//            string str = string.Empty;

//            if (v == VALUE_INVALID)
//            {
//                str = " *****";
//            }
//            else if (v == VALUE_OVERFLOW)
//            {
//                str = " -.---";
//            }
//            else
//            {
//                str = String.Format("{0:F3}", v);
//            }
//            return str;
//        }

//        // 农残计算deltaA
//        public static DeltaA[] CaculateDeltaA(AT[] first, AT[] last)
//        {
//            DeltaA[] result = new DeltaA[first.Length];
//            for (int i = 0; i < result.Length; ++i)
//            {
//                // 检测完毕
//                if ((first[i].a < 0.002) && (last[i].a < 0.002))
//                {   // 判断样品空白
//                    result[i].deltaA = VALUE_INVALID;
//                }
//                else if (!(IsValid(first[i].a) && IsValid(last[i].a)))
//                {   // 判断样品非法
//                    result[i].deltaA = VALUE_INVALID;
//                }
//                else
//                {   // 计算 DeltaA的值
//                    if (last[i].a >= first[i].a)
//                    {
//                        result[i].deltaA = last[i].a - first[i].a;
//                    }
//                    else
//                    {
//                        if ((first[i].a - last[i].a) > 0.1)
//                        {
//                            result[i].deltaA = VALUE_INVALID;
//                        }
//                        else
//                        {
//                            result[i].deltaA = 0;
//                        }
//                    }
//                }
//            }

//            return result;
//        }

//        // 农残计算抑制率
//        public static double[] CaculateIzhilv(DeltaA[] deltaA, double refDeltaA)
//        {
//            double[] izhilv = new double[deltaA.Length];
//            for (int i = 0; i < deltaA.Length; ++i)
//            {
//                if (IsValid(refDeltaA) && IsValid(deltaA[i].deltaA))
//                    izhilv[i] = (refDeltaA - deltaA[i].deltaA) / refDeltaA;
//                else
//                    izhilv[i] = VALUE_INVALID;
//            }
//            return izhilv;
//        }

//        // 标准曲线法计算吸光度
//        public static double[] HoleStatusToSCA(AT[] holeStatus, double refA)
//        {
//            double[] a = new double[holeStatus.Length];
//            for (int i = 0; i < holeStatus.Length; ++i)
//            {
//                if (IsValid(holeStatus[i].a) && IsValid(refA))
//                    a[i] = holeStatus[i].a - refA;
//                else
//                    a[i] = VALUE_INVALID;
//            }

//            return a;
//        }

//        // 动力学法，计算一分钟的变化率。
//        public static double[] CaculateChangeA(DeltaA[] deltaA, int time)
//        {
//            double[] perA = new double[deltaA.Length];
//            for (int i = 0; i < deltaA.Length; ++i)
//            {
//                if (IsValid(deltaA[i].deltaA))
//                    perA[i] = deltaA[i].deltaA / time;
//                else
//                    perA[i] = VALUE_INVALID;
//            }
//            return perA;
//        }

//        // 公式a+bx+cx2+dx3 微调
//        public static double[] WeiTiaoX(double[] x, double a, double b, double c, double d)
//        {
//            double[] xFix = new double[x.Length];
//            for (int i = 0; i < xFix.Length; ++i)
//            {
//                if (IsValid(x[i]))
//                    xFix[i] = a + b * x[i] + c * x[i] * x[i] + d * x[i] * x[i] * x[i];
//                else
//                    xFix[i] = VALUE_INVALID;
//            }

//            return xFix;
//        }

//        public static double[] CaculateKOH(int count, double dishu, double a0, double a1, double a2, double a3)
//        {
//            double[] koh = new double[count];
//            for (int i = 0; i < count; ++i)
//            {
//                koh[i] = DataUtils.GetThreeOrderEqutionSolution(dishu, a0, a1, a2, a3);
//            }
//            return koh;
//        }

//    }
//}
#endregion