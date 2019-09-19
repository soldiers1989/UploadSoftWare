using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public static class wave
    {
        //小波分解
        static double[] h = { 0.230378, 0.714847, 0.630881, -0.027984, -0.187035, 0.030841, 0.032883, -0.010597 };
        //g[k] = (-1)^k*h[(1-k)]
        static double[] g = { -0.010597, -0.032883, 0.030841, 0.187035, -0.027984, -0.630881, 0.714847, -0.230378 };
        //小波重构
        static double[] h1 = { -0.010597, 0.032883, 0.030841, -0.187035, -0.027984, 0.630881, 0.714847, 0.230378 };
        //g1[k] = (-1)^k*h1[(1-k)]
        static double[] g1 = { -0.230378, 0.714847, -0.630881, -0.027984, 0.187035, 0.030841, -0.032883, -0.010597 };

        public static double[] DB4DWT(double[] pBuf)
        {
            double[] tmp;
            int i, j, n, pDLen, half, nLen = pBuf.Length;

            if (pBuf == null) return null;	//指针空返回NULL
            if (nLen < 8) return null;    	//数据长度小于8返回NULL

            if (nLen % 2 == 1)
            {
                tmp = new double[nLen + 7]; //malloc(sizeof(double) * (nLen + 7));
                pDLen = (nLen + 7);
            }
            else
            {
                tmp = new double[nLen + 6]; //malloc(sizeof(double) * (nLen + 6));
                pDLen = (nLen + 6);
            }

            half = pDLen / 2;
            for (i = 0; i < half; i++)
            {
                /*tmp数组初始化*/
                tmp[i] = 0;
                tmp[i + half] = 0;

                for (j = 0; j < 8; j++)
                {
                    n = 2 * i + j - 6;

                    if (n >= nLen) n = 2 * nLen - 1 - n;
                    else if (n < 0) n = -1 * n - 1;
                    tmp[i] += h[j] * pBuf[n];
                    tmp[i + half] += g[j] * pBuf[n];
                }
            }
            /*	printf("\n分解后数据：\n");
                for(i = 0;i < *pDLen;i++)
                {
                    printf("%f\t",tmp[i]);
                    if(i%5 == 4) printf("\n");	
                }*/
            return tmp;
        }

        public static double[] DB4IDWT(double[] pBuf)
        {
            double[] tmp;
            int i, j, n, half, nLen = pBuf.Length;
            if (pBuf == null) return null;
            if (nLen < 8) return null;
            half = nLen / 2;
            tmp = new double[nLen - 6];//malloc(sizeof(double)*(nLen-6));
            for (i = 0; i < nLen - 6; i++)
            {
                tmp[i] = 0;
                for (j = 0; j < 8; j++)
                {
                    n = i + j - 1;
                    if (n >= nLen) n = 2 * nLen - 1 - n;
                    if (n % 2 == 0)
                    {
                        tmp[i] += h1[j] * pBuf[n / 2];
                    }
                    else
                    {
                        tmp[i] += g1[j] * pBuf[half + n / 2];
                    }
                }
            }
            //printf("\n重构后数据：\n");
            //for(i = 0;i < nLen-6;i++)
            //{
            //    printf("%f\t",tmp[i]);
            //    if(i%5 == 4) printf("\n");	
            //}
            return tmp;
        }

        public static double[] DBFLT(double[] db)
        {
            double[] rtn = new double[db.Length / 2];
            Array.Copy(db, 0, rtn, 0, db.Length / 2);
            //for (int i = 0; i < db.Length; i++)
            //{
                
            //}

            return rtn;
        }

    }
}
