using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace AIO.src.xprinter
{
    public class TicketPrinterHelper
    {
        //定义一个字符串流，用来接收所要打印的数据
        private static StringReader sr;

        //打印列表
        static PrintModel[] PrintModels;

        //二维码
        static string QrCode;

        //str要打印的数据
        public static bool Print(string str)
        {
            bool result = true;
            try
            {
                sr = new StringReader(str);
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                PaperSize pageSize = new PaperSize("Custom", getYc(193), 600);
                pd.DefaultPageSettings.Margins.Top = 2;
                pd.DefaultPageSettings.Margins.Left = 10;
                pd.DefaultPageSettings.PaperSize = pageSize;
                pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }


        public static bool Print(PrintModel[] pms, string code)
        {
            bool result = true;
            try
            {
                PrintModels = pms;
                QrCode = code;
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                //PaperSize pageSize = new PaperSize("Custom", getYc(193), 600);
                PaperSize pageSize = new PaperSize("Custom", getYc(193), 310);//纸张设置
                pd.DefaultPageSettings.Margins.Top = 2;
                pd.DefaultPageSettings.Margins.Left = 10;
                pd.DefaultPageSettings.PaperSize = pageSize;
                pd.PrinterSettings.PrinterName = Global.Printing[0].XprinterName;// "Xprinter XP-237B";// pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage1);
                pd.Print();
                
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }

        private static int getYc(double cm)
        {

            return (int)(cm / 25.4) * 100;

        }

        private static void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            Font printFont = new Font("Arial", 9);//打印字体
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage && ((line = sr.ReadLine()) != null))
            {
                //leftMargin = (350 - ev.Graphics.MeasureString(line, printFont).ToSize().Width)/2;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos);
                count++;
            }
            ev.Graphics.DrawImage(QRCodeHelper.CreateQRCode("http://www.baidu.com"), new Point(70, Convert.ToInt32(yPos) + 20));
            //If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }


        private static void pd_PrintPage1(object sender, PrintPageEventArgs ev)
        {
            float yPos = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            int count = 0;
            Font printFont;
            foreach (PrintModel pm in PrintModels)
            {
                if (pm.IsBold)
                {
                    printFont = new Font(pm.FontFamily, pm.FontSize, FontStyle.Bold);//打印字体
                }
                else
                {
                    printFont = new Font(pm.FontFamily, pm.FontSize);//打印字体
                }
                float linesPerPage = 0;
                String line = "";
                linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
                while (count < linesPerPage && ((line = pm.Text.ReadLine()) != null))
                {
                    //leftMargin = (350 - ev.Graphics.MeasureString(line, printFont).ToSize().Width)/2;
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black,
                       leftMargin, yPos);
                    count++;
                }
                //If more lines exist, print another page.
                if (line != null)
                    ev.HasMorePages = true;
                else
                    ev.HasMorePages = false;
                topMargin = topMargin + 20;
                yPos = yPos + 20;
            }
            if (!string.IsNullOrEmpty(QrCode))
            {
                ev.Graphics.DrawImage(QRCodeHelper.CreateQRCode(QrCode), new Point(40, Convert.ToInt32(yPos)));
            }
        }
    }

    public enum Printer
    {
        REGO = 0,
        DASCOM = 1
    }
}
