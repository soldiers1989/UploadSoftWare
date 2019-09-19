using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public static class CurveSmoothing
    {

        /// <summary>
        /// 分解数据
        /// </summary>
        /// <param name="nBuf"></param>
        /// <param name="nLen"></param>
        public static double[] WaveletDecomposition(double[] nBuf)
        {
            double[] temp = new double[64];
            int i, j;
            for (i = 0; i < 64; i++)
            {
                temp[i] = nBuf[i];
            }
            for (j = 64; j > 1; j /= 2)
            {
                for (i = 0; i < j; i += 2)
                {
                    temp[i + 1] -= temp[i];
                    temp[i] += temp[i + 1] / 2;
                }
                for (i = 0; i < j / 2; i++)
                {
                    nBuf[i + j / 2] = temp[2 * i + 1];
                    temp[i] = temp[2 * i];
                }
                string str = string.Empty;
            }
            nBuf[0] = temp[0];
            return nBuf;
        }

        /// <summary>
        /// 重构
        /// </summary>
        /// <param name="nBuf"></param>
        /// <param name="nLen"></param>
        public static double[] WaveletStructure(double[] nBuf)
        {
            double[] temp = new double[64];
            int i, j;
            for (j = 2; j <= 64; j *= 2)
            {
                for (i = 0; i < j / 2; i++)
                {
                    temp[2 * i] = nBuf[i];
                    temp[2 * i + 1] = nBuf[j / 2 + i];
                }
                for (i = 0; i < j / 2; i++)
                {
                    temp[2 * i] -= temp[2 * i + 1] / 2;
                    temp[2 * i + 1] += temp[2 * i];
                }
                for (i = 0; i < j; i++)
                {
                    nBuf[i] = temp[i];
                }
            }
            return nBuf;
        }

        public static double[][] FFT(double[][] A, int ifft)
        {
            int j,k,jk;
            int N = A[0].Length;
            double[][] X=new double[2][];
            X[0] = new double[N];
            X[1] = new double[N];
            double[] Wi = new double[N];
            double[] Wr = new double[N];
            double unit = 2 * 3.141592 / (double)N;
            for (j = 0; j < N; j++)
            {
                Wr[j] = System.Math.Cos(unit * (double)j);
                Wi[j] = System.Math.Sin(unit * (double)j);
            }
            if (ifft == -1)
            {
                for (k = 0; k < N; k++)
                {
                    A[1][k] = -A[1][k];
                }
            }
            for (j = 0; j < N; j++)
            {
                for (k = 0; k < N; k++)
                {
                    jk = (j * k) % N;
                    X[0][j] = X[0][j] + A[0][k] * Wr[jk] + A[1][k] * Wi[jk];
                    X[1][j] = X[1][j] + A[1][k] * Wr[jk] - A[0][k] * Wi[jk];
                }
            }
            if (ifft == -1)
            {
                for (j = 0; j < N; j++)
                {
                    A[0][j] = X[0][j] / (double)N;
                    A[1][j] = -X[1][j] / (double)N;
                }
            }
            else
            {
                for (j = 0; j < N; j++)
                {
                    A[0][j] = X[0][j];
                    A[1][j] = X[1][j];
                }
            }

            return A;
        }

        public static double[] SUM(double[][] A) 
        {
            int len = A[0].Length;
            double[] db = new double[len];
            for (int i = 0; i < len; i++)
            {
                db[i] = System.Math.Sqrt(A[0][i] * A[0][i] + A[1][i] * A[1][i]);
            }
            return db;
        }

        public static double[][] FLT(double[][] A)
        {
            int i, N = A[0].Length;
            for (i = 5; i <= N - 5; i++)
            {
                A[0][i] = 0;
                A[1][i] = 0;
                //A[0][N - 8] = 0;
                //A[1][N - 8] = 0;
            }
            return A;
        }

        /// <summary>
        /// 滤波 旧方法
        /// </summary>
        /// <param name="nBuf"></param>
        //public static double[] FLT(double[] nBuf)
        //{
        //    int i, j;

        //    for (i = 1; i < 64; i++)
        //    {
        //        if (i >= 32) nBuf[i] = nBuf[i];//H 0.5平滑处理
        //        else if (i >= 16) nBuf[i] = nBuf[i];//LH 
        //        else if (i >= 8) nBuf[i] = nBuf[i];//LLH 
        //        else if (i >= 4) nBuf[i] = nBuf[i];//LLLH
        //        else if (i >= 2) nBuf[i] = nBuf[i];
        //        //{
        //        //    for (j = 63; j >= 4; j--)
        //        //    {
        //        //        if (i == 3) break;
        //        //        if (nBuf[j] == 0) continue;
        //        //        if (j >= 32)
        //        //        {
        //        //            if ((j % 32) < 16) nBuf[j] -= nBuf[2] / 16;
        //        //            else nBuf[j] -= nBuf[3] / 16;
        //        //        }
        //        //        else if (j >= 16)
        //        //        {
        //        //            if (j % 16 < 8) nBuf[j] -= nBuf[2] / 8;
        //        //            else nBuf[j] -= nBuf[3] / 8;
        //        //        }
        //        //        else if (j >= 8)
        //        //        {
        //        //            if (j % 8 < 4) nBuf[j] -= nBuf[2] / 4;
        //        //            else nBuf[j] -= nBuf[3] / 4;
        //        //        }
        //        //        else if (j >= 4)
        //        //        {
        //        //            if (j % 4 < 2) nBuf[j] -= nBuf[2] / 2;
        //        //            else nBuf[j] -= nBuf[3] / 2;
        //        //        }
        //        //        //else nBuf[j]=0;//LLLLLH	
        //        //    }
        //        //    nBuf[i] = 0;
        //        //}
        //        else
        //        {
        //            for (j = 63; j > 0; j--)
        //            {
        //                if (nBuf[j] == 0) continue;
        //                if (j >= 32) nBuf[j] -= nBuf[i] / 32;
        //                else if (j >= 16) nBuf[j] -= nBuf[i] / 16;
        //                else if (j >= 8) nBuf[j] -= nBuf[i] / 8;
        //                else if (j >= 4) nBuf[j] -= nBuf[i] / 4;
        //                else if (j >= 2) nBuf[j] -= nBuf[i] / 2;
        //                else nBuf[j] = 0;//LLLLLL	
        //            }
        //        }
        //    }

        //    for (i = 1; i < 64; i++)
        //    {
        //        if (i >= 32) nBuf[i] = 0;
        //        else if (i >= 16) nBuf[i] = 0;//LH 
        //        else if (i >= 7) nBuf[i] = 0;//LLH 
        //        else if (i >= 5) nBuf[i] = nBuf[i] / 2;//LLLH
        //        else if (i >= 2) nBuf[i] = 0;
        //        else if (i >= 1) nBuf[i] = 0;
        //    }

        //    return nBuf;
        //}

        private static double BufSum(double[] Buf)
        {
            int len = Buf.Length;
            double sum = 0;
            for (int i = 0; i < len; i++) sum += Buf[i];
            return sum;
        }

        private static double XBufSum(double[] Buf)
        {
            int len = Buf.Length;
            double sum = 0;
            for (int i = 0; i < len; i++) sum += (i+1) * Buf[i];
            return sum;
        }

        public static double[] ZB(double[] Buf) 
        {
            int LEN = Buf.Length;
            double minNum = 65535;
            for (int i = 0; i < LEN; i++)
            {
                if (Buf[i] >= 10000) Buf[i] = 10000;
            }
            for (int i = 0; i < LEN; i++)
            {
                if (minNum > Buf[i]) minNum = Buf[i];
            }
            if (Buf[1] - minNum <= 100)
            {
                for (int i = 0; i < LEN; i++)
                {
                    Buf[i] = Buf[0];
                }
            }
            return Buf;
        }

        /// <summary>
        /// 基线校正
        /// </summary>
        /// <param name="Buf"></param>
        /// <returns></returns>
        public static double[] BaselineCorrect(double[] Buf)
        {
            int LEN = Buf.Length;
            double XY, X, Y, XX;
            double a, b;
            if (Buf == null || LEN < 2) return null;
            X = LEN * (LEN + 1) / 2;
            XX = LEN * (LEN + 1) * (2 * LEN + 1) / 6;
            Y = BufSum(Buf);
            XY = XBufSum(Buf);
            a = ((LEN) * XY - X * Y) / ((LEN) * XX - X * X);
            b = (XX * Y - X * XY) / (LEN * XX - X * X);
            Buf[0] = a * LEN / 2 + b;
            //for (int i = 1; i < LEN; i++)
            //{
            //    Buf[i] = Buf[i] + (LEN - 2 * i) * a / 2;
            //}
            //Buf[LEN - 1] = Buf[0];
            for (int i = LEN - 1; i >= 0; i--)
            {
                Buf[i] = Buf[i] * 10000 / Buf[0];
            }
            return Buf;
        }

        /// <summary>
        /// 查找有效数据
        /// </summary>
        /// <param name="nBuf"></param>
        /// <returns></returns>
        public static int[] FindUsefulData(double[] nBuf)
        {
            int ucLen = nBuf.Length;
            byte ucHead = 0, ucEnd = 0;//有数据的头和尾
            int ucCnt = 0;//连续计数
            byte i = 0;
            int[] index = new int[2];
            int[] nTempBuf = new int[ucLen];//存放差值
            if (ucLen <= 2)
                return null;

            for (i = 0; i < ucLen - 1; i++)
                nTempBuf[i] = (byte)(nBuf[i + 1] - nBuf[i]);

            //头
            for (i = 0; i < ucLen - 1; i++)
            {
                int abs = System.Math.Abs(nTempBuf[i]);
                if ((nBuf[i] < 4000) || (nBuf[i] > 12000) || (abs > 500))
                {
                    ucCnt = 0;
                    continue;
                }
                ucCnt++;
                if (ucCnt >= 15)
                    break;
            }
            if ((i >= 15) && (i != (ucLen - 1)))
            {
                ucHead = (byte)i;//(i - 5);
                if (index != null && index.Length >= 2) index[0] = (int)ucHead;
            }
            else
                index = null;

            //尾
            ucCnt = 0;
            for (i = (byte)(ucLen - 1); i > 0; i--)
            {
                if ((nBuf[i] < 4000) || (nBuf[i] > 12000) || (System.Math.Abs(nTempBuf[i]) > 500))
                {
                    ucCnt = 0;
                    continue;
                }
                ucCnt++;
                if (ucCnt >= 15)
                    break;
            }

            if ((i < ucLen - 16) && (i != 0))
            {
                ucEnd = (byte)i;//(i + 3);
                if (index != null && index.Length >= 2) index[1] = (int)ucEnd;
            }
            else
                index = null;

            if (ucEnd < (ucHead + 20))
                index = null;

            //如果没有取到有效数据范围，则直接指定20-65
            if (index == null)
            {
                index = new int[2];
                index[0] = 15;
                index[1] = 65;
            }
            if (index[0] < 10 || index[0] > 25) index[0] = 15;
            if (index[1] < 60 || index[1] > 75) index[1] = 65;

            return index;
        }

        /// <summary>
        /// 对byte[]进行延伸或缩减
        /// 长度不足64个byte时补足64，大于64个byte时缩减至64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static double[] CombinationBytes(double[] bytes)
        {
            int len = bytes.Length;
            double[] rtnBytes = new double[64];
            if (len == 64)
            {
                return bytes;
            }
            else if (len > 64)
            {
                //为true时不取首坐标的值，为false时不取尾坐标的值
                bool IsSingular = true;
                while (len != 64)
                {
                    double[] dbs = new double[len - 1];
                    int index = 0;
                    if (IsSingular)
                    {
                        index = 1;
                        IsSingular = false;
                    }
                    else
                    {
                        index = 0;
                        IsSingular = true;
                    }
                    Array.ConstrainedCopy(bytes, index, dbs, 0, dbs.Length);
                    bytes = dbs;
                    len = bytes.Length;
                }
            }
            else if (len < 64)
            {
                //为true时首坐标前+1个byte，为false时尾坐标后+1个byte
                bool IsSingular = true;
                while (len != 64)
                {
                    double[] dbs = new double[len + 1];
                    if (IsSingular)
                    {
                        dbs[0] = bytes[0];
                        Array.ConstrainedCopy(bytes, 0, dbs, 1, bytes.Length);
                        IsSingular = false;
                    }
                    else
                    {
                        dbs[dbs.Length - 1] = bytes[bytes.Length - 1];
                        Array.ConstrainedCopy(bytes, 0, dbs, 0, bytes.Length);
                        IsSingular = true;
                    }
                    bytes = dbs;
                    len = bytes.Length;
                }
            }
            return bytes;
        }

    }
}