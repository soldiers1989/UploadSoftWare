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

        public class Report
        {
            public string ItemName = string.Empty;
            public string User = string.Empty;
            public string ItemCategory = string.Empty;
            public string Unit = string.Empty;
            public string Date = string.Empty;
            public string Judgment = string.Empty;
            public string ContrastValue = string.Empty;

            public List<string> SampleNum = new List<string>();
            public List<string> Result = new List<string>();
            public List<string> SampleName = new List<string>();
            public List<string> JudgmentTemp = new List<string>();

            public byte[] GeneratePrintBytes()
            {
                List<byte> data = new List<byte>();
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_CENTER);
                data.AddRange(CMD_SETDOUBLESIZE);
                data.AddRange(GBKEncoding.GetBytes("\r\n" + ItemName + "\r\n"));
                data.AddRange(CMD_RESET);
                data.AddRange(CMD_SETGBK);
                data.AddRange(CMD_SETshortSIZE);
                data.AddRange(GBKEncoding.GetBytes("\r\n检测设备：" + Global.InstrumentName + "\r\n检测人员：" + User + "\r\n检测类别：" + ItemCategory
                        + "\r\n检测日期：" + Date + "\r\n单位：" + Unit + "\r\n\r\n"));
                //if (ContrastValue.Equals("NULL") || ContrastValue.Equals(string.Empty))
                //    data.AddRange(GBKEncoding.GetBytes("\r\n检测人员：" + User + "\r\n检测类别：" + ItemCategory 
                //        + "\r\n检测日期：" + Date + "\r\n单位：" + Unit + "\r\n\r\n"));
                //else
                //    data.AddRange(GBKEncoding.GetBytes("\r\n检测人员：" + User + "\r\n检测类别：" + ItemCategory 
                //        + "\r\n检测日期：" + Date + "\r\n单位：" + Unit 
                //        + "\r\n对照值：" + double.Parse(ContrastValue).ToString("F3") + "\r\n\r\n"));
                data.AddRange(CMD_SETHT);
                data.AddRange(CMD_SETshortSIZE);
                data.AddRange(GBKEncoding.GetBytes("编号"));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("样品名称"));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("检测结果"));
                data.AddRange(CMD_Five);
                data.AddRange(GBKEncoding.GetBytes("判定结果"));
                data.AddRange(CMD_FEEDLINE);

                data.AddRange(GBKEncoding.GetBytes("--------------------------------"));
                for (int i = 0; i < SampleNum.Count; ++i)
                {
                    //2015年12月15日wenj 对照时同时测试样品，打印时不打印对照，故编号+1
                    int num = int.Parse(SampleNum[i]);
                    data.AddRange(GBKEncoding.GetBytes(String.Format("{0:D5}", num)));
                    data.AddRange(CMD_HTHT);
                    if (!string.IsNullOrEmpty(SampleName[i].ToString()))
                        data.AddRange(GBKEncoding.GetBytes(SampleName[i]));
                    else
                        data.AddRange(GBKEncoding.GetBytes(string.Empty));
                    data.AddRange(CMD_HTHT);
                    data.AddRange(GBKEncoding.GetBytes(Result[i]));
                    data.AddRange(CMD_HTHT);
                    data.AddRange(GBKEncoding.GetBytes(JudgmentTemp[i]));
                    data.AddRange(CMD_FEEDLINE);
                }
                data.AddRange(GBKEncoding.GetBytes("--------------------------------\r\n"));

                if (!ContrastValue.Equals("NULL") && !ContrastValue.Equals(string.Empty))
                    data.AddRange(GBKEncoding.GetBytes("对照值：△A=" + double.Parse(ContrastValue).ToString("F3") + "   Abs\r\n"));
                data.AddRange(GBKEncoding.GetBytes("\r\n检测人员：" + User));
                data.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________\r\n\r\n\r\n"));

                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                return buffer;
            }
        }
    }
}
