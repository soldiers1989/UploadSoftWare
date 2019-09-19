using System;
using System.Collections.Generic;
using System.Text;

namespace AIO
{
    // 打印帮助类，不同的结果，打印数据不同。
    public class PrintHelper
    {
        private static Encoding GBKEncoding = Encoding.GetEncoding("GBK");

        private static byte[] CMD_RESET = { 0x1b, 0x40 };
        private static byte[] CMD_SETDOUBLESIZE = { 0x1c, 0x21, 0x0c };
        private static byte[] CMD_CENTER = { 0x1b, 0x61, 0x01 };
        private static byte[] CMD_FEEDLINE = { 0x0d, 0x0a };
        private static byte[] CMD_SETGBK = { 0x1c, 0x26, 0x1b, 0x39, 0x00 };
        private static byte[] CMD_SETHT = { 0x1b, 0x44, 0x0a, 0x18, 0x00 };
        private static byte[] CMD_HT = { 0x09 };
        private static byte[] CMD_SIX = { 0x06 };
        private static byte[] CMD_Seven = { 0x07 };
        private static byte[] CMD_Five = { 0x05 };
        private static byte[] CMD_HTHT = { 0x02 };
        private static byte[] CMD_SETshortSIZE = { 0x1c, 0x21, 0x00 };

        public static bool PrintUnit = true;// 单位
        public static bool PrintDate = true;//检测日期
        public static bool PrintItemCategory = true;//检测方法
        public static bool PrintCompany = true;//被检单位
        public static bool PrintInstrumentName = true;//检测设备
        public static bool PrintUser = true;//检测人员
        public static bool PrintReviewers = true;//审核人员
        public static bool PrintQR = true;//是否打印二维码

        public class Report
        {
            public string ItemName = string.Empty;
            public string Standard = string.Empty;
            public string User = string.Empty;
            public string ItemCategory = string.Empty;
            public string Unit = string.Empty;
            public string Date = string.Empty;
            public string Judgment = string.Empty;
            public string samplecode = "";//样品编号
            /// <summary>
            /// 对照值
            /// </summary>
            public string ContrastValue = string.Empty;
            /// <summary>
            /// 被检单位
            /// </summary>
            public string Company = string.Empty;

            public List<string> SampleNum = new List<string>();
            public List<string> Result = new List<string>();
            public List<string> SampleName = new List<string>();
            public List<string> JudgmentTemp = new List<string>();

            private List<string> GetVals(string val, bool isItem = false)
            {
                int vallen = isItem ? 7 : 15;
                val = val.Contains("\r") ? val.Replace("\r", string.Empty) : val;
                val = val.Contains("\n") ? val.Replace("\n", string.Empty) : val;
                List<string> vals = new List<string>();
                if (val.Length <= vallen) vals.Add(val);
                else
                {
                    string v = string.Empty;
                    vallen *= 2;
                    for (int i = 0; i < val.Length; i++)
                    {
                        v += val.Substring(i, 1);
                        if (GBKEncoding.GetBytes(v).Length >= vallen)
                        {
                            vals.Add(v);
                            v = string.Empty;
                        }
                        if (i == val.Length - 1)
                        {
                            vals.Add(v);
                        }
                    }
                }
                return vals;
            }

            /// <summary>
            /// 新的方法
            /// 2016年2月29日
            /// wenj
            /// </summary>
            /// <returns></returns>
            public byte[] GeneratePrintBytes()
            {
                List<byte> data = new List<byte>();
                List<string> vals = null;
                string val = string.Empty;
                //对照值、检验人员、审核人员

                if (PrintReviewers)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员:_ _ _ _ _ _ _ _ _ _ _"));
                }

                if (PrintUser)
                {
                    vals = GetVals("检测人员:" + User);
                    for (int i = vals.Count - 1; i >= 0; i--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[i]));
                    }
                }

                if (!ContrastValue.Equals("NULL") && !ContrastValue.Equals(""))
                    data.AddRange(GBKEncoding.GetBytes("\r\n\r\n对照值:△A=" + double.Parse(ContrastValue).ToString("F3") + "   Abs\r\n"));

                data.AddRange(GBKEncoding.GetBytes("\r\n--------------------------------"));

                for (int i = 0; i < SampleNum.Count; ++i)
                {
                    val = string.Empty;
                    try
                    {
                        //2015年12月15日wenj 对照时同时测试样品，打印时不打印对照，故编号+1
                        int num = int.Parse(SampleNum[i].Remove(0, SampleNum[i].Length - 5));
                        val += string.Format("{0:D5}", num);
                    }
                    catch (Exception)
                    {
                        val += SampleNum[i];
                    }
                    val += " ";
                    if (!string.IsNullOrEmpty(SampleName[i].ToString()))
                        val += SampleName[i];
                    else
                        val += " ";
                    val += " ";
                    val += Result[i];
                    val += " ";
                    val += JudgmentTemp[i];
                    vals = GetVals(val);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }
                data.AddRange(GBKEncoding.GetBytes("\r\n--------------------------------"));

                //编号、名称、检测结果、判定结果
                data.AddRange(CMD_FEEDLINE);
                data.AddRange(GBKEncoding.GetBytes("编号     "));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("名称     "));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("结果     "));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("判定     "));
                data.AddRange(CMD_SETHT);
                data.AddRange(CMD_SETshortSIZE);

                //检测设备、类别、日期、单位

                if (PrintCompany)
                {
                    vals = GetVals("被检单位:" + Company);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }

                if (PrintUnit)
                {
                    vals = GetVals("检测值单位:" + Unit);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }

                if (PrintDate)
                {
                    vals = GetVals("检测日期:" + Date);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }

                if (PrintItemCategory)
                {
                    vals = GetVals("检测依据:" + Standard);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }

                if (PrintItemCategory)
                {
                    vals = GetVals("检测方法:" + ItemCategory);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                }

                if (PrintInstrumentName)
                {
                    vals = GetVals("检测设备:" + Global.InstrumentName);
                    for (int j = vals.Count - 1; j >= 0; j--)
                    {
                        data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                    }
                    data.AddRange(GBKEncoding.GetBytes("\r\n\r\n"));
                }

                //检测项目
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_CENTER);
                data.AddRange(CMD_SETDOUBLESIZE);
                vals = GetVals(ItemName, true);
                for (int j = vals.Count - 1; j >= 0; j--)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n" + vals[j]));
                }
                data.AddRange(GBKEncoding.GetBytes("\r\n"));
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_SETshortSIZE);
                data.AddRange(GBKEncoding.GetBytes("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n"));

                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                return buffer;
            }

            /// <summary>
            /// 一维码打印
            /// </summary>
            /// <returns></returns>
            public List<byte> GetBarcode()
            {
                List<byte> data = new List<byte>();
                data.Add(0x1B);//要加初始化1B 40 ，不加打印不出条形码
                data.Add(0x40);
                data.Add(0x1D);
                data.Add(0x6B);
                data.Add(0x49);
                data.Add(0x0A);
                data.Add(0x7B);
                data.Add(0x42);
                data.Add(0x4E);
                data.Add(0x6F);
                data.Add(0x2E);
                data.Add(0x7B);
                data.Add(0x43);
                //data.Add(0x0C);
                //data.Add(0x22);
                //data.Add(0x38);

                data.AddRange(GBKEncoding.GetBytes(samplecode));

                return data;
            }

            /// <summary>
            /// BX1二维码打印 - 需要顺序打印
            /// </summary>
            /// <returns></returns>
            public List<byte> GetQrCode()
            {
                List<byte> data = new List<byte>();
                //检测项目
                data.AddRange(GBKEncoding.GetBytes(ItemName));

                if (PrintInstrumentName)
                {
                    data.AddRange(GBKEncoding.GetBytes(string.Format("\r\n\r\n检测设备:{0}", Global.InstrumentName)));
                }

                if (PrintItemCategory)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测方法:" + ItemCategory));
                }

                data.AddRange(GBKEncoding.GetBytes("\r\n检测依据:" + Standard));

                if (PrintDate)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测日期:" + Date));
                }

                if (PrintUnit)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测值单位:" + Unit));
                }

                if (PrintCompany)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n被检单位:" + Company));
                }

                data.AddRange(GBKEncoding.GetBytes("\r\n"));

                //编号、名称、检测结果、判定结果
                string val = string.Empty;
                for (int i = 0; i < SampleNum.Count; ++i)
                {
                    val = string.Empty;
                    try
                    {
                        int num = int.Parse(SampleNum[i].Remove(0, SampleNum[i].Length - 5));
                        val = string.Format("\r\n样品编号:{0:D5}", num);
                    }
                    catch (Exception)
                    {
                        val += "\r\n样品编号:" + SampleNum[i];
                    }
                    val += string.Format("\r\n样品名称:{0}", SampleName[i]);
                    val += string.Format("\r\n检测结果:{0}", Result[i]);
                    val += string.Format("\r\n判定结果:{0}", JudgmentTemp[i]);
                    data.AddRange(GBKEncoding.GetBytes(val));
                }

                data.AddRange(GBKEncoding.GetBytes("\r\n"));

                if (!ContrastValue.Equals("NULL") && !ContrastValue.Equals(""))
                    data.AddRange(GBKEncoding.GetBytes("\r\n对照值:△A=" + double.Parse(ContrastValue).ToString("F3") + " Abs"));

                if (PrintUser)
                {
                    data.AddRange(GBKEncoding.GetBytes("\n检测人员:" + User));
                }

                if (PrintReviewers)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员:_ _ _ _ _ _ _ _ _ _ _"));
                } 

                List<byte> qcDts = new List<byte>();
                qcDts.Add(0x1d);
                qcDts.Add(0x5a);
                qcDts.Add(0x02);
                qcDts.Add(0x1b);
                qcDts.Add(0x5a);
                qcDts.Add(0x03);
                qcDts.Add(0x4C);//纠错等级
                qcDts.Add(0x05);//放大倍数
                qcDts.Add((byte)data.Count);//低位长度
                qcDts.Add((byte)(data.Count >> 8));//高位长度
                qcDts.AddRange(data);
                return qcDts;
            }

        }

    }
}