using System;

namespace AIO.src
{
    public class CurveSmoothing
    {

        public static int[] FindUsefulData(string context, double[] buffer)
        {
            int len = buffer.Length;
            double[] nTempBuf = new double[len];
            int i = 0;
            int ucCnt = 0;
            int ucHead = 0, ucEnd = 0;
            int[] index = new int[2];
            if (len <= 2)
                index = null;
            for (i = 0; i < (len - 1); i++)
            {
                nTempBuf[i] = buffer[i + 1] - buffer[i];
            }
            for (i = 0; i < (len - 1); i++)
            {
                if ((buffer[i] < 4000) || (buffer[i] > 12000) || (Math.Abs(nTempBuf[i]) > 500))
                {
                    ucCnt = 0;
                    continue;
                }
                ucCnt++;
                if (ucCnt >= 15)
                    break;
            }
            if ((i >= 15) && (i != (len - 1)))
            {
                ucHead = (byte)i;
                if (index != null && index.Length >= 2)
                    index[0] = ucHead;
            }
            else
                index = null;
            ucCnt = 0;
            for (i = (byte)(len - 1); i > 0; i--)
            {
                // 差值250
                if ((buffer[i] < 4000) || (buffer[i] > 12000) || (Math.Abs(nTempBuf[i]) > 500))
                {
                    ucCnt = 0;
                    continue;
                }
                ucCnt++;
                if (ucCnt >= 15)
                    break;
            }
            if ((i < len - 16) && (i != 0))
            {
                ucEnd = (byte)i;
                if (index != null && index.Length >= 2) index[1] = ucEnd;
            }
            else
            {
                index = null;
            }
            if (ucEnd < (ucHead + 20))
                index = null;
            // 如果没有取到有效数据范围，则直接指定20-65
            if (index == null)
            {
                index = new int[2];
                index[0] = 15;
                index[1] = 65;
            }
            else
            {
                if (index[0] < 10 || index[0] > 25)
                    index[0] = 15;
                if (index[1] < 60 || index[1] > 75)
                    index[1] = 65;
            }
            return index;
        }

        // / <summary>
        // / 分解数据
        // / </summary>
        // / <param name="nBuf"></param>
        // / <param name="nLen"></param>
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
            }
            nBuf[0] = temp[0];
            return nBuf;
        }

        // / <summary>
        // / 重构
        // / </summary>
        // / <param name="nBuf"></param>
        // / <param name="nLen"></param>
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
            int j, k, jk;
            int N = A[0].Length;
            double[][] X = new double[2][];
            X[0] = new double[N];
            X[1] = new double[N];
            double[] Wi = new double[N];
            double[] Wr = new double[N];
            double unit = 2 * 3.141592 / (double)N;
            for (j = 0; j < N; j++)
            {
                Wr[j] = Math.Cos(unit * (double)j);
                Wi[j] = Math.Sin(unit * (double)j);
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
                db[i] = Math.Sqrt(A[0][i] * A[0][i] + A[1][i] * A[1][i]);
            }
            return db;
        }

        //public static double[][] FLT(double[][] A, int x)
        //{
        //    int i, N = A[0].Length;
        //    //double xx = 0;
        //    for (i = N / x; i <= N - N / x; i++)
        //    {
        //        //xx = (i > (N / 2) ? (N * N) / (N - i) : N * N / i) * double.Parse(Global.testVal);
        //        //if (Math.Abs(A[0][i]) > xx) A[0][i] = 0;
        //        //if (Math.Abs(A[1][i]) > xx) A[1][i] = 0;
        //        A[0][i] = 0;
        //        A[1][i] = 0;
        //    }
        //    return A;
        //}

        public static double[][] FLT(double[][] A, int x)
        {
            int i, N = A[0].Length;
            for (i = N / x; i <= N - N / x; i++)
            {
                A[0][i] = 0;
                A[1][i] = 0;
                // A[0][N - 8] = 0;
                // A[1][N - 8] = 0;
            }
            return A;
        }

        private static double BufSum(double[] Buf)
        {
            int len = Buf.Length;
            double sum = 0;
            for (int i = 0; i < len; i++)
                sum += Buf[i];
            return sum;
        }

        private static double XBufSum(double[] Buf)
        {
            int len = Buf.Length;
            double sum = 0;
            for (int i = 0; i < len; i++)
                sum += (i + 1) * Buf[i];
            return sum;
        }

        public static double[] ZB(double[] Buf)
        {
            int LEN = Buf.Length;
            double minNum = 65535;
            for (int i = 0; i < LEN; i++)
            {
                if (Buf[i] >= 10000)
                    Buf[i] = 10000;
            }
            for (int i = 0; i < LEN; i++)
            {
                if (minNum > Buf[i])
                    minNum = Buf[i];
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

        // return null;
        // / <summary>
        // / 基线校正
        // / </summary>
        // / <param name="Buf"></param>
        // / <returns></returns>
        public static double[] BaselineCorrect(double[] Buf)
        {
            int LEN = Buf.Length;
            double XY, X, Y, XX;
            double a, b;
            if (Buf == null || LEN < 2)
                return null;
            X = LEN * (LEN + 1) / 2;
            XX = LEN * (LEN + 1) * (2 * LEN + 1) / 6;
            Y = BufSum(Buf);
            XY = XBufSum(Buf);
            a = ((LEN) * XY - X * Y) / ((LEN) * XX - X * X);
            b = (XX * Y - X * XY) / (LEN * XX - X * X);
            Buf[0] = a * LEN / 2 + b;
            for (int i = 1; i < LEN; i++)
            {
                Buf[i] = Buf[i] + (LEN - 2 * i) * a / 2;
            }
            for (int i = LEN - 1; i >= 0; i--)
            {
                Buf[i] = Buf[i] * 10000 / Buf[0];
            }
            return Buf;
        }

        // / <summary>
        // / 对byte[]进行延伸或缩减
        // / 长度不足64个byte时补足64，大于64个byte时缩减至64
        // / </summary>
        // / <param name="bytes"></param>
        // / <returns></returns>
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
                // 为true时不取首坐标的值，为false时不取尾坐标的值
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
                    Array.Copy(bytes, index, dbs, 0, dbs.Length);
                    bytes = dbs;
                    len = bytes.Length;
                }
            }
            else if (len < 64)
            {
                // 为true时首坐标前+1个byte，为false时尾坐标后+1个byte
                bool IsSingular = true;
                while (len != 64)
                {
                    double[] dbs = new double[len + 1];
                    if (IsSingular)
                    {
                        dbs[0] = bytes[0];
                        Array.Copy(bytes, 0, dbs, 1, bytes.Length);
                        IsSingular = false;
                    }
                    else
                    {
                        dbs[dbs.Length - 1] = bytes[bytes.Length - 1];
                        Array.Copy(bytes, 0, dbs, 0, bytes.Length);
                        IsSingular = true;
                    }
                    bytes = dbs;
                    len = bytes.Length;
                }
            }
            return bytes;
        }

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[] DB4WDT(double[] arr)
        {
            int length = arr.Length;
            double[] h = new double[] { 0.2304, 0.7148, 0.6309, -0.0280, -0.1870, 0.0308, 0.0329, -0.0106 };
            if (length > 8)
            {
                int j, k;
                int half = arr.Length / 2;
                double[] temp = new double[length / 2];
                for (j = 0; j < half; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        temp[j] += arr[(2 * j + i) % length] * h[i];
                    }
                }
                return temp;
            }
            else
            {
                return arr;
            }
        }

        /// <summary>
        /// 重构
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[] DB4IWDT(double[] arr)
        {
            int len = arr.Length;
            double[] h = new double[] { 0.2304, 0.7148, 0.6309, -0.0280, -0.1870, 0.0308, 0.0329, -0.0106 };
            double[] temp = new double[2 * len];
            for (int i = 0; i < 2 * len; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j - 7) % 2 == 0)
                    {
                        temp[i] += h[7 - j] * arr[((i + j - 5 + 2 * len) % (2 * len)) / 2];
                    }
                }
            }
            return temp;
        }

    }
}