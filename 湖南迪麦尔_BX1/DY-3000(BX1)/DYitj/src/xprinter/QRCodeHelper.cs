using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace AIO.src.xprinter
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
                if(Global.IsGoverTest )
                {
                    encoder.QRCodeScale = 1;//二维码大小控制
                }
                else
                {
                    encoder.QRCodeScale = 2;//二维码大小控制
                    encoder.QRCodeVersion = 0;//数据多版本要设置为0
                }
               
                pic = encoder.Encode(url, Encoding.UTF8); 
                
            }
            catch (Exception ex)
            {  

            }

            return pic;
        }
    }
}
