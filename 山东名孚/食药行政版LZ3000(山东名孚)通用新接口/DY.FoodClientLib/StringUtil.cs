using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DY.FoodClientLib
{
    /// <summary>
    /// 字符串操作常用类
    /// 原作者：徐磊
    /// </summary>
    public class StringUtil
    {
        private StringUtil()
        {
        }
        /// <summary>
        /// 重复N次某一个字符
        /// </summary>
        /// <param name="ch">目标字符 </param>
        /// <param name="lev">重得个数</param>
        /// <returns></returns>
        public static string RepeatChar(char ch, int lev)
        {
            string rtn = string.Empty;
            for (int i = 0; i < lev; i++)
            {
                rtn += ch.ToString();
            }

            return rtn;
        }

        /// <summary>
        /// 检测是否合法字符
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string strIn)
        {
            return IsValidNumber(strIn, true);
        }

        public static bool IsValidNumber(string strIn, bool allowEmpty)
        {
            if (allowEmpty && strIn.Trim().Length == 0)
            {
                return true;
            }
            return Regex.IsMatch(strIn, @"^[0-9]*$");
        }

        /// <summary>
        /// 检测是否数字
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsNumeric(string strIn)
        {
            return IsNumeric(strIn, true);
        }

        public static bool IsNumeric(string strIn, bool allowEmpty)
        {
            if (allowEmpty && strIn.Trim().Length == 0)
            {
                return true;
            }
            try
            {
                double dbl = Convert.ToDouble(strIn.Trim());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 把检测项目字符串分隔并加载到数组里面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[,] GetAry(string input)
        {
            Regex r;
            Match m;
            ArrayList ar1 = new ArrayList();
            ArrayList ar2 = new ArrayList();

            r = new Regex(@"\{([ \S\t]*?):([ \S\t]*?)}", RegexOptions.IgnoreCase);
            for (m = r.Match(input); m.Success; m = m.NextMatch())
            {
                ar1.Add(m.Groups[1].ToString());
                ar2.Add(m.Groups[2].ToString());
            }
            string[,] strr = new string[ar1.Count, 2];
            for (int i = 0; i <= ar1.Count - 1; i++)
            {
                strr[i, 0] = ar1[i].ToString();
                strr[i, 1] = ar2[i].ToString();
            }
            return strr;
        }

        /// <summary>
        /// 组合字符串,获取DY系列检测项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[,] GetDY3000DYAry(string input)
        {
            Regex r;
            Match m;
            ArrayList ar1 = new ArrayList();
            ArrayList ar2 = new ArrayList();
            ArrayList ar3 = new ArrayList();
            ArrayList ar4 = new ArrayList();

            r = new Regex(@"\{([ \S\t]*?):([ \S\t]*?):([ \S\t]*?):([ \S\t]*?)}", RegexOptions.IgnoreCase);
            for (m = r.Match(input); m.Success; m = m.NextMatch())
            {
                ar1.Add(m.Groups[1].ToString());
                ar2.Add(m.Groups[2].ToString());
                ar3.Add(m.Groups[3].ToString());
                ar4.Add(m.Groups[4].ToString());
            }
            string[,] strr = new string[ar1.Count, 4];
            for (int i = 0; i <= ar1.Count - 1; i++)
            {
                strr[i, 0] = ar1[i].ToString();
                strr[i, 1] = ar2[i].ToString();
                strr[i, 2] = ar3[i].ToString();
                strr[i, 3] = ar4[i].ToString();
            }
            return strr;
        }

        /// <summary>
        /// 食品分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[,] GetFoodAry(string input)
        {
            Regex r;
            Match m;
            ArrayList ar1 = new ArrayList();
            ArrayList ar2 = new ArrayList();
            ArrayList ar3 = new ArrayList();
            ArrayList ar4 = new ArrayList();

            r = new Regex(@"\{([ \S\t]*?):([ \S\t]*?):([ \S\t]*?):([ \S\t]*?)}", RegexOptions.IgnoreCase);
            for (m = r.Match(input); m.Success; m = m.NextMatch())
            {
                ar1.Add(m.Groups[1].ToString());
                ar2.Add(m.Groups[2].ToString());
                ar3.Add(m.Groups[3].ToString());
                ar4.Add(m.Groups[4].ToString());
            }
            string[,] err = new string[ar1.Count, 4];
            for (int i = 0; i < ar1.Count; i++)
            {
                err[i, 0] = ar1[i].ToString();
                err[i, 1] = ar2[i].ToString();
                err[i, 2] = ar3[i].ToString();
                err[i, 3] = ar4[i].ToString();
            }
            return err;
        }

        /// <summary>
        /// 把十六进制字符串转化字节数组
        /// 修改者：
        /// 修改时间：2011-10-21
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] HexString2ByteArray(string input)
        {
            string temp = string.Empty;
            int len = input.Length / 2;
            byte[] bt = new byte[len];
            for (int i = 0; i < len; i++)
            {
                temp = input.Substring(i * 2, 2);
                bt[i] = Convert.ToByte(temp, 16);
            }
            return bt;
        }

        /// <summary>
        /// 把16进制转化为日期时间字符串
        /// 此函数内部已经修改
        /// 修改者： 2011-10-21
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexStr2DateTimeString(string input)
        {
            Match match = Regex.Match(input, "[^0-9a-fA-F]");
            if (match.Success || input.Length % 2 != 0)
            {
                return "00";
            }
            else
            {
                StringBuilder sbTemp = new StringBuilder();
                uint utDate;
                string temp = string.Empty;
                byte[] by = new byte[4];
                int len = input.Length / 2;

                for (int i = 0; i < len; i++)
                {
                    temp = input.Substring(i * 2, 2);
                    by[i] = Convert.ToByte(temp, 16);
                }
                utDate = (uint)(by[0] | (by[1] << 8) | (by[2] << 16) | (by[3] << 24));
                sbTemp.Append(((utDate >> 25) & 0x7f));//year
                sbTemp.Append("-");
                sbTemp.Append(((utDate >> 21) & 0x0f));//month
                sbTemp.Append("-");
                sbTemp.Append(((utDate >> 16) & 0x1f));//date
                sbTemp.Append(" ");
                sbTemp.Append(((utDate >> 11) & 0x1f));//hour
                sbTemp.Append(":");
                sbTemp.Append(((utDate >> 5) & 0x3f));//minute
                sbTemp.Append(":");
                sbTemp.Append(((utDate << 1) & 0x3f));//second

                string ret = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                try
                {
                    ret = Convert.ToDateTime(sbTemp.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (FormatException)
                {
                    ret = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                sbTemp.Length = 0;
                return ret;
            }
        }

        /// <summary>
        /// 检验和
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CheckDataSum(string input)
        {
            Match match = Regex.Match(input, "[^0-9a-fA-F]");
            if (match.Success || input.Length % 2 != 0)
            {
                return "00";
            }
            else
            {
                string temp = string.Empty;
                int len = input.Length / 2;
                byte bt1 = 0;
                byte bt2 = 0;
                for (int i = 0; i < len; i++)
                {
                    temp = input.Substring(i * 2, 2);
                    bt2 = Convert.ToByte(temp, 16);
                    bt1 = Convert.ToByte(bt1 ^ bt2);
                }
                return bt1.ToString("X2");
            }
        }

        /// <summary>
        /// 过滤空字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexStringTrim(string input)
        {
            int len = input.Length / 2;
            string temp = string.Empty;

            StringBuilder sbTemp = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                temp = input.Substring(i * 2, 2);
                if (temp.Equals("00"))
                {
                    return sbTemp.ToString();
                }
                sbTemp.Append(temp);
            }
            temp = sbTemp.ToString();
            sbTemp.Length = 0;
            return temp;
        }
        /// <summary>
        /// 把十六进制字符串转化为两位浮点数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexString2Float2String(string input)
        {
            int i2 = 0;
            i2 = Convert.ToByte(input.Substring(2, 2), 16) << 8;
            i2 += Convert.ToByte(input.Substring(0, 2), 16);
            string temp = string.Empty;
            temp = ((float)i2 / 10).ToString();
            return temp;
        }

        /// <summary>
        /// 把十六制字符串转化四位浮点数据字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexString2Float4String(string input)
        {
            int iflag = 1;
            int ret = 0;
            ret = Convert.ToByte(input.Substring(6, 2), 16);
            if (ret >= 128)
            {
                iflag = -1;
                ret = (ret - 128) << 24;
            }

            ret += Convert.ToByte(input.Substring(4, 2), 16) << 16;
            ret += Convert.ToByte(input.Substring(2, 2), 16) << 8;
            ret += Convert.ToByte(input.Substring(0, 2), 16);

            float fRet = ((float)ret / 1000) * iflag;
            string temp = fRet.ToString();
            return temp;
        }
        /// <summary>
        /// 转化小端模式
        /// </summary>
        /// <param name="bigEndian"></param>
        /// <returns></returns>
        public static uint ToLittleEndian(uint bigEndian)
        {
            //byte[] tdata = new byte[4];
            byte[] bt = BitConverter.GetBytes(bigEndian);
            return (uint)(bt[0] << 24) + (uint)(bt[1] << 16) + (uint)(bt[2] << 8) + (uint)(bt[3]);
        }

        /// <summary>
        /// 将字符串从fromBase进制转换为toBase进制
        /// </summary>
        /// <param name="value">字符串原始值</param>
        /// <param name="fromBase">原字符串进制如16进制</param>
        /// <param name="toBase">转化目标进制值 如10进制</param>
        /// <returns></returns>
        public static string ConvertString(string value, int fromBase, int toBase)
        {
            int intValue = Convert.ToInt32(value, fromBase);
            return Convert.ToString(intValue, toBase);
        }

        /// <summary> 
        /// DataTable装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="p_DataSet">DataSet</param> 
        /// <param name="p_TableName">待转换数据表名称</param> 
        /// <returns></returns> 
        /// 2015年12月10日 wenj 
        public static IList<T> DataTableToIList<T>(DataTable p_DataSet, int p_TableName)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in p_DataSet.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (p_DataSet.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (tempName == "CheckedCompanyName")
                        {
                            string d = value.ToString();
                        }
                        if (!value.ToString().Equals(""))
                        {
                          
                            if (tempName == "CheckStartDate" || tempName == "TakeDate" || tempName == "CheckEndDate" || tempName == "SendedDate")
                            {
                                DateTime d = DateTime.Parse(value.ToString());
                                pro.SetValue(t,d, null);
                            }
                            else
                            {
                                pro.SetValue(t, value, null);
                            }
                        }
                    }
                }
                list.Add(t);
            }
            return list.Count == 0 ? null : list;
        }

    }
}
