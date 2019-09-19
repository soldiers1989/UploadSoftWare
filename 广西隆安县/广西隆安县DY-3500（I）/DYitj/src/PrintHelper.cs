﻿using System.Collections.Generic;
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
        public static bool IsPrintCode = true;//打印二维码
        public static bool isCompany = true;//被检单位

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
                //data.AddRange(GBKEncoding.GetBytes("\r\n检测设备：" + Global.InstrumentName + "\r\n检测类别：" + ItemCategory
                //        + "\r\n检测日期：" + Date + "\r\n单位：" + Unit + "\r\n\r\n"));

                if (IsInstrumentName)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测设备：" + Global.InstrumentName));
                }
                if (IsItemCategory)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测类别：" + ItemCategory));
                }
                if (IsDate)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测日期：" + Date));
                }
                if (IsUnit)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n单位：" + Unit));
                }
                data.AddRange(GBKEncoding.GetBytes("\r\n\r\n"));

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
                if (IsUser)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n检测人员：" + User));
                }
                if (IsReviewers)
                {
                    data.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________\r\n"));
                }

                if (IsPrintCode) data.AddRange(CreateQRCode());

                data.AddRange(GBKEncoding.GetBytes("\r\n\r\n"));

                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                return buffer;
            }

            /// <summary>
            /// 生成二维码
            /// </summary>
            /// <returns>返回的数据</returns>
            private List<byte> CreateQRCode()
            {
                //二维码数据
                List<byte> QRbyte = new List<byte>();
                QRbyte.AddRange(GBKEncoding.GetBytes("检测项目:" + ItemName));
                if (IsInstrumentName) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n检测设备:" + Global.InstrumentName));
                if (IsItemCategory) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n检测类别:" + ItemCategory));
                if (IsDate) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n检测日期:" + Date));
                if (IsUnit) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n单位:" + Unit));
                QRbyte.AddRange(GBKEncoding.GetBytes("\r\n\r\n--------------------------------"));
                //样品列表
                for (int i = 0; i < SampleNum.Count; ++i)
                {
                    //2015年12月15日wenj 对照时同时测试样品，打印时不打印对照，故编号+1
                    int num = int.Parse(SampleNum[i]);
                    QRbyte.AddRange(GBKEncoding.GetBytes("\r\n编号:" + String.Format("{0:D5}", num) + "\r\n"));
                    //QRbyte.AddRange(CMD_HTHT);
                    if (!string.IsNullOrEmpty(SampleName[i].ToString()))
                        QRbyte.AddRange(GBKEncoding.GetBytes("名称:" + SampleName[i] + "\r\n"));
                    else
                        QRbyte.AddRange(GBKEncoding.GetBytes("名称:" + "\r\n"));
                    //QRbyte.AddRange(CMD_HTHT);
                    QRbyte.AddRange(GBKEncoding.GetBytes("检测结果:" + Result[i] + "\r\n"));
                    //QRbyte.AddRange(CMD_HTHT);
                    QRbyte.AddRange(GBKEncoding.GetBytes("判定结果:" + JudgmentTemp[i] + "\r\n"));
                    //QRbyte.AddRange(CMD_FEEDLINE);
                }
                QRbyte.AddRange(GBKEncoding.GetBytes("--------------------------------\r\n"));
                if (!ContrastValue.Equals("NULL") && !ContrastValue.Equals(string.Empty))
                {
                    QRbyte.AddRange(GBKEncoding.GetBytes("对照值：△A=" + double.Parse(ContrastValue).ToString("F3") + "   Abs\r\n"));
                }
                else
                {
                    QRbyte.AddRange(GBKEncoding.GetBytes("对照值：△A=N/A"));
                }
                if (IsUser) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n检测人员:" + User));
                //if (IsReviewers) QRbyte.AddRange(GBKEncoding.GetBytes("\r\n审核人员：_____________________\r\n\r\n\r\n"));

                List<byte> QRbytes = new List<byte>();
                //1b 40开头 1d 28 6b为指令 //0x1D 0x28 0x6B 0x03 0x00 0x31 0x43 0x05 设置大小
                byte[] bts = Global.ATP.StringToBytes("0x1B 0x40 0x1D 0x28 0x6B 0x03 0x00 0x31 0x43 0x06 0x1D 0x28 0x6B", new string[] { " ", " " }, 16);
                QRbytes.AddRange(bts);
                //pL pH cn fn m
                bts = new byte[5];
                if ((QRbyte.Count + 3) > 256)
                {
                    bts[0] = Convert.ToByte(((QRbyte.Count + 3) % 256).ToString("x"), 16);
                    bts[1] = Convert.ToByte(((QRbyte.Count + 3) / 256).ToString("x"), 16);
                }
                else
                {
                    bts[0] = Convert.ToByte((QRbyte.Count + 3).ToString("x"), 16);
                    bts[1] = 0x00;
                }

                bts[2] = 0x31;
                bts[3] = 0x50;
                bts[4] = 0x30;
                QRbytes.AddRange(bts);
                QRbytes.AddRange(QRbyte);
                bts = Global.ATP.StringToBytes("0x1B 0x61 0x01 0x1D 0x28 0x6B 0x03 0x00 0x31 0x52 0x30 0x1D 0x28 0x6B 0x03 0x00 0x31 0x51 0x30", new string[] { " ", " " }, 16);
                QRbytes.AddRange(bts);

                bts = new byte[QRbytes.Count];
                QRbytes.CopyTo(bts);

                return QRbytes;
            }
        }

    }
}