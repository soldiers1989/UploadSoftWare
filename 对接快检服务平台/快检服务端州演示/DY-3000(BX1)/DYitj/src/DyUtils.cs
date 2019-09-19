using System;
using System.Collections.Generic;

namespace AIO.src
{
    public class DyUtils
    {
        public static double[] doubles;
        public static int[][] indexs;


        /// <summary>
        /// 获取一段数据的波谷信息
        /// </summary>
        /// <param name="origin">处理的原始数组</param>
        /// <param name="dValue">差值</param>
        /// <param name="point">点数</param>
        /// <returns>该段波的信息二维数组</returns>
        public static int[][] getWaveInfo(double[] origin, double dValue, int point)
        {
            //定义比较器, (i+1) -i>0 为正
            //起始点
            int[] startPoint = new int[point + 1];
            //结束点
            int[] endPoint = new int[point + 1];
            //波谷
            int[] wave = new int[point * 2];
            //数组长度
            int len = origin.Length;
            //所有波谷坐标集合
            List<int> boguPoint = new List<int>();

            //比较器赋值
            for (int i = 0; i < point + 1; i++)
            {
                startPoint[i] = i == 0 ? 1 : -1;
                endPoint[i] = i == point ? -1 : 1;
            }
            //波谷比较器赋值
            for (int i = 0; i < wave.Length; i++)
            {
                wave[i] = i < wave.Length / 2 ? -1 : 1;
            }
            //定义一个原数据走势
            int[] origin1 = new int[len];
            for (int i = 1; i < len; i++)
            {
                if (origin[i] - origin[i - 1] > dValue)
                {
                    origin1[i] = 1;
                }
                else if (origin[i - 1] - origin[i] > dValue)
                {
                    origin1[i] = -1;
                }
                else
                {
                    origin1[i] = 0;
                }
            }

            //找波谷
            for (int i = point; i < len - point; i++)
            {
                if (i > 115) break;
                int[] temp = new int[point * 2];
                for (int j = 0; j < point * 2; j++)
                {
                    temp[j] = origin1[j + i - point];
                }
                int arrSum = getArrSum(temp, wave);
                if (arrSum >= point * 2 - 1)
                {
                    boguPoint.Add(i);
                    i += point * 2;
                }
            }

            int[][] Sindex = new int[boguPoint.Count][];
            for (int i = 0; i < boguPoint.Count; i++)
            {
                Sindex[i] = new int[3];
            }

            //找出波谷起始点
            for (int i = 0; i < boguPoint.Count; i++)
            {
                int s = 0;
                s = boguPoint[i] - 20 < 0 ? 0 : boguPoint[i] - 20;
                Sindex[i][1] = boguPoint[i];
                for (int j = boguPoint[i] - point; j > s; j--)
                {
                    int[] chage = new int[point + 1];
                    for (int m = 0; m < point + 1; m++)
                    {
                        chage[m] = origin1[m + j];
                    }
                    int arrSum = getArrSum(chage, startPoint);
                    if (arrSum >= point)
                    {
                        Sindex[i][0] = j;
                        break;
                    }
                }

                if (Sindex[i][0] == 0)
                {
                    Sindex[i][0] = boguPoint[i] - 20 < 0 ? 0 : boguPoint[i] - 20;
                }
            }
            //找出波谷结束点
            for (int i = 0; i < boguPoint.Count; i++)
            {
                //如果长度超过就指定为最后一个
                int s = 0;
                s = boguPoint[i] + 20 > len ? len : boguPoint[i] + 20;

                for (int j = boguPoint[i]; j < s; j++)
                {
                    int[] chage = new int[point + 1];
                    for (int m = 0; m < point + 1; m++)
                    {
                        chage[m] = origin1[j + m - point];
                    }
                    int arrSum = getArrSum(chage, endPoint);
                    if (arrSum >= point)
                    {
                        Sindex[i][2] = j;
                        break;
                    }
                }

                if (Sindex[i][2] == 0)
                {
                    Sindex[i][2] = boguPoint[i] + 20 > len ? len - 1 : boguPoint[i] + 20;
                }
            }
            indexs = Sindex;
            return Sindex;
        }

        //数组乘积和
        private static int getArrSum(int[] origin, int[] compare)
        {
            double sum = 0;
            for (int i = 0; i < origin.Length; i++)
            {
                if (origin[i] == compare[i])
                {
                    sum = sum + 1;
                }
                else if (origin[i] == 0)
                {
                    //sum = 0.91 + sum;
                    sum = (float)(origin.Length - 2) / origin.Length + sum;
                }
                else
                {
                    sum = sum - 1;
                }
            }
            return (int)sum;
        }


        //传入参数 : usefulTemp 起始结束区间赋0的数组 ,m 指定多项式
        public static double[] duoXiangShi(double[] usefulTemp, int m)
        {
            double[][] a = new double[m][];
            for (int i = 0; i < m; i++)
            {
                a[i] = new double[m];
            }
            double[] b = new double[m];
            double[] c = new double[m];
            for (int i = 0; i < usefulTemp.Length; i++)
            {
                if (usefulTemp[i] != 0)
                {
                    for (int j = 0; j < m; j++)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            //if (j + k == 0)
                            //{
                            //    a[j][k] = a[j][k] + 1;
                            //}
                            //else
                            //{
                            //    if (i == 0)
                            //    {
                            //        a[j][k] = a[j][k];
                            //    }
                            //    else
                            //    {
                            //        a[j][k] = a[j][k] + Math.Pow(i * 0.1, j + k);
                            //    }
                            //}
                            a[j][k] = j + k == 0 ? a[j][k] + 1 : (i == 0 ? a[j][k] : a[j][k] + Math.Pow(i * 0.1, j + k));
                        }
                        //if (j == 0)
                        //{
                        //    b[j] = b[j] + usefulTemp[i];
                        //}
                        //else
                        //{
                        //    if (i == 0)
                        //    {
                        //        b[j] = b[j];
                        //    }
                        //    else
                        //    {
                        //        b[j] = b[j] + usefulTemp[i] * Math.Pow(i * 0.1, j);
                        //    }
                        //}
                        b[j] = j == 0 ? b[j] + usefulTemp[i] : (i == 0 ? b[j] : b[j] + usefulTemp[i] * Math.Pow(i * 0.1, j));
                    }
                }
            }
            //a的逆矩阵
            a = getMatrix(a);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    c[i] = c[i] + a[j][i] * b[j];
                }
            }

            double[] temp3 = new double[usefulTemp.Length];
            for (int i = 0; i < usefulTemp.Length; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    temp3[i] = temp3[i] + c[j] * Math.Pow(0.1 * i, j);
                }
            }
            return temp3;
        }

        public static double[][] getMatrix(double[][] arr)
        {
            double abs = getJuZhengMo(arr);
            if (abs == 0)
            {
                System.Console.Write("数据错误");
                return null;
            }

            double[][] changeFunction = getChangeFunction(arr);
            changeFunction = getYuZiXizang(changeFunction);
            int cols = changeFunction.Length;
            int rows = changeFunction[0].Length;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //if ((i + j) % 2 == 0)
                    //{
                    //    changeFunction[i][j] = changeFunction[i][j] / abs;
                    //}
                    //else
                    //{
                    //    changeFunction[i][j] = -changeFunction[i][j] / abs;
                    //}
                    changeFunction[i][j] = (i + j) % 2 == 0 ? changeFunction[i][j] / abs : -changeFunction[i][j] / abs;
                }
            }
            return changeFunction;
        }

        //获取矩阵转置
        private static double[][] getChangeFunction(double[][] arr)
        {
            int length = arr[0].Length;
            double[][] temp = new double[length][];
            for (int i = 0; i < length; i++)
            {
                temp[i] = new double[length];
            }
            //r为行数
            for (int r = 0; r < length; r++)
            {
                //c为列数
                for (int c = 0; c < length; c++)
                {
                    temp[c][r] = arr[r][c];
                }
            }
            return temp;
        }
        //求矩阵的模
        private static double getJuZhengMo(double[][] arr)
        {
            double v = 0, val = 0;
            int length = arr[0].Length;
            if (length == 1)
            {
                return arr[0][0];
            }
            int ans = 0;
            double[][] B = new double[length - 1][];
            for (int i = 0; i < length - 1; i++)
            {
                B[i] = new double[length - 1];
            }
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    for (int k = 0; k < length - 1; k++)
                    {
                        ans = (k < i ? k : k + 1);
                        B[j][k] = arr[j + 1][ans];
                    }
                }

                v = getJuZhengMo(B);
                //if (i % 2 == 0)
                //{
                //    val = val + arr[0][i] * v;
                //}
                //else
                //{
                //    val = val - arr[0][i] * v;
                //}
                val = i % 2 == 0 ? val + arr[0][i] * v : val - arr[0][i] * v;
            }
            return val;
        }
        public static double[][] getYuZiXizang(double[][] arr)
        {

            int cols = arr.Length;
            int rows = arr[0].Length;
            double[][] temp = new double[cols][];
            for (int i = 0; i < cols; i++)
            {
                temp[i] = new double[rows];
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    double[][] temp1 = new double[cols - 1][];
                    for (int k = 0; k < cols - 1; k++)
                    {
                        temp1[k] = new double[rows - 1];
                    }
                    for (int c = 0; c < cols - 1; c++)
                    {
                        int c1 = c < i ? c : c + 1;
                        for (int r = 0; r < rows - 1; r++)
                        {
                            int r1 = r < j ? r : r + 1;
                            temp1[c][r] = arr[c1][r1];
                        }
                    }
                    temp[i][j] = getJuZhengMo(temp1);
                }
            }
            return temp;
        }

        //获取有用数据
        public static double[] getUserFulData(double[] arr)
        {
            double[] useful = new double[arr.Length - 110];
            Array.Copy(arr, 50, useful, 0, arr.Length - 110);
            return useful;
        }

        /*返回两个波 波峰到起始点/结束点 斜率对应值的差值
         * 参数: first 有效波
         *  second 趋势线
         * */
        public static int[] index = new int[2];

        public static double[] getPointValue(double[] first, double[] duoxiangshi)
        {
            double[] result = new double[first.Length];
            doubles = new double[first.Length + 2];
            for (int i = 0; i < first.Length; i++)
            {
                result[i] = first[i] / duoxiangshi[i];
                doubles[i] = result[i];
            }

            int[][] waveInfo = getWaveInfo(result, 0.001, 6);//0.0015
            double[] value = new double[2];
            int cidx = 0;
            for (int i = 0; i < waveInfo.Length; i++)
            {
                int start = waveInfo[i][0];
                int center = waveInfo[i][1];
                int end = waveInfo[i][2];
                double pinjun = (result[end] * (center - start) + result[start] * (end - center)) / (end - start) - result[center];
                pinjun = Math.Log(1 + pinjun) / Math.Log(2);

                //前半段找出最大值为C值
                if (waveInfo[i][1] < result.Length / 2)
                {
                    if (value[0] < pinjun)
                    {
                        value[0] = pinjun;
                        index[0] = center;
                        cidx = waveInfo[i][1];
                    }
                }
                //后半段的结果取值要根据C值坐标来判断，必须要大于35的差值
                else
                {
                    if (waveInfo[i][1] - cidx <= 35) continue;
                    if (value[1] < pinjun)
                    {
                        value[1] = pinjun;
                        index[1] = center;
                    }
                }
            }

            return value;
        }
        public static double[] dyMath(double[] arr, bool isTest = false)
        {
            double[] newArray = isTest ? arr : getUserFulData(arr);
            int[][] bogu = getWaveInfo(newArray, isTest ? 0.001 : 30, 6);
            List<int[]> bogus = new List<int[]>();
            for (int i = 0; i < bogu.Length; i++)
            {
                if ((newArray[bogu[i][0]] - newArray[bogu[i][1]]) / Math.Abs(bogu[i][0] - bogu[i][1]) > 30 && (newArray[bogu[i][2]] - newArray[bogu[i][1]]) / Math.Abs(bogu[i][2] - bogu[i][1]) > 30)
                {
                    bogus.Add(bogu[i]);
                }
            }

            bogu = new int[bogus.Count][];
            for (int i = 0; i < bogus.Count; i++)
            {
                bogu[i] = bogus[i];
            }
            double[] temp2 = new double[newArray.Length];
            for (int i = 0; i < newArray.Length; i++)
            {
                temp2[i] = newArray[i];
            }

            //波谷值取0
            for (int i = 0; i < bogu.Length; i++)
            {
                for (int j = bogu[i][0]; j <= bogu[i][2]; j++)
                {
                    temp2[j] = 0;
                }
            }
            double[] dxs = duoXiangShi(temp2, 5);
            double[] pointValue = getPointValue(newArray, dxs);

            return pointValue;
        }

    }
}