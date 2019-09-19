using System.Collections.Generic;
using System.Text;
using System;

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

        public static bool IsUnit = true;// 单位
        public static bool IsDate = true;//检测日期
        public static bool IsItemCategory = true;//检测方法
        public static bool IsCompany = true;//被检单位
        public static bool IsInstrumentName = true;//检测设备
        public static bool IsUser = true;//检测人员
        public static bool IsReviewers = true;//审核人员

        public class Report
        {
            public string ItemName = string.Empty;
            public string User = string.Empty;
            public string ItemCategory = string.Empty;
            public string Unit = string.Empty;
            public string Date = string.Empty;
            public string Judgment = string.Empty;
            public string ContrastValue = string.Empty;
            public string Company = string.Empty;

            public List<string> SampleNum = new List<string>();
            public List<string> Result = new List<string>();
            public List<string> SampleName = new List<string>();
            public List<string> JudgmentTemp = new List<string>();

            /// <summary>
            /// 新的方法
            /// 2016年2月29日
            /// wenj
            /// </summary>
            /// <returns></returns>
            public byte[] GeneratePrintBytes()
            {
                List<byte> data = new List<byte>();
                //对照值、检验人员、审核人员
                //if (ContrastValue.Equals("NULL") || ContrastValue.Equals(""))
                //    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________" + "\r\n检测人员：" + User + "\r\n\r\n"));
                //else
                //    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________" + "\n检测人员：" + User
                //    + "\r\n\r\n对照值：△A=" + double.Parse(ContrastValue).ToString("F3") + "   Abs\r\n"));

                if (IsReviewers)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________"));
                }
                if (IsUser)
                {
                    data.AddRange(GBKEncoding.GetBytes("\n检测人员：" + User));
                }
                if (!ContrastValue.Equals("NULL") && !ContrastValue.Equals(""))
                    data.AddRange(GBKEncoding.GetBytes("\r\n\r\n对照值：△A=" + double.Parse(ContrastValue).ToString("F3") + "   Abs\r\n"));

                
                data.AddRange(GBKEncoding.GetBytes("\r\n--------------------------------"));
                for (int i = 0; i < SampleNum.Count; ++i)
                {
                    //2015年12月15日wenj 对照时同时测试样品，打印时不打印对照，故编号+1
                    int num = int.Parse(SampleNum[i].Remove(0, SampleNum[i].Length - 5));
                    //num = Global.IsContrast ? num += 1 : num;
                    data.AddRange(GBKEncoding.GetBytes(String.Format("\r\n{0:D5}", num)));
                    data.AddRange(GBKEncoding.GetBytes(" "));
                    if (!string.IsNullOrEmpty(SampleName[i].ToString()))
                        data.AddRange(GBKEncoding.GetBytes(SampleName[i]));
                    else
                        data.AddRange(GBKEncoding.GetBytes(" "));
                    data.AddRange(GBKEncoding.GetBytes(" "));
                    data.AddRange(GBKEncoding.GetBytes(Result[i]));
                    data.AddRange(GBKEncoding.GetBytes(" "));
                    data.AddRange(GBKEncoding.GetBytes(JudgmentTemp[i]));
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
                //data.AddRange(GBKEncoding.GetBytes("\r\n单位：" + Unit + "\r\n检测日期：" + Date
                //    + "\r\n检测方法：" + ItemCategory + "\r\n被检单位：" + Company + "" + Global.InstrumentName + ""));

                if (IsUnit)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n单位：" + Unit));
                }
                if (IsDate)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测日期：" + Date));
                }
                if (IsItemCategory)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测方法：" + ItemCategory));
                }
                if (IsCompany)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n被检单位：" + Company));
                }
                if (IsInstrumentName)
                {
                    data.AddRange(GBKEncoding.GetBytes(string.Format("\r\n检测设备：{0}\r\n\r\n", Global.InstrumentName)));
                }

                //检测项目
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_CENTER);
                data.AddRange(CMD_SETDOUBLESIZE);
                data.AddRange(GBKEncoding.GetBytes("\r\n" + ItemName + "\r\n"));
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_SETshortSIZE);
                data.AddRange(GBKEncoding.GetBytes("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n"));
                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                return buffer;
            }
        }

    }
}