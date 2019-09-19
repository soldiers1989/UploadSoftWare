using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace TicketPrinter
{
    /// <summary>
    /// 二维码生成辅助类
    /// </summary>
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="info"></param> 
        /// <returns>二维码临时文件路径</returns>
        public static Bitmap CreateQRCode(string code)
        {
            Bitmap pic=null;
            try
            {
                string url = code;
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeScale = 3;//二维码大小控制 2比1大
                encoder.QRCodeVersion = 4;//数据量大要设置版本为0
                pic = encoder.Encode(url, Encoding.UTF8); 
                
            }
            catch (Exception ex)
            {  
            }

            return pic;
        }
    }
}
