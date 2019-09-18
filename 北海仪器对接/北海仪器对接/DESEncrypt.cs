/*************************************************************
 * 文 件 名：DESEncrypt
 * 命名空间：HPK.Utilities
 * 作    者：鸣飞
 * 生成日期：2016-05-27 22:31:23
 * 描    述：
 * ===========================================================
 * 修 改 者：
 * 修改时间：
 * 描    述：
 * ===========================================================
 * 版 本 号：V1.0.0.0
 * 版权说明：Copyright © 2015-- 鸣飞. All Rights Reserved 
*************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HPK.Utilities
{
    /// <summary>
    /// 加密、解密帮助类
    /// </summary>
    public class DESEncrypt
    {
        /// <summary>
        /// 向量
        /// </summary>
        private const string IV_64 = "EacicDpc";
        /// <summary>
        /// 密钥
        /// </summary>
        private const string KEY_64 = "EacicDpc";

        #region 加密
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data">加密内容</param>
        /// <returns>加密后的数据</returns>
        public static string Encrypt(string data)
        {
            if (data != "")
            {
                byte[] bytes = Encoding.ASCII.GetBytes(KEY_64);
                byte[] rgbIV = Encoding.ASCII.GetBytes(IV_64);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                int keySize = provider.KeySize;
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(stream2);
                writer.Write(data);
                writer.Flush();
                stream2.FlushFinalBlock();
                writer.Flush();
                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }
            return data;
        }
        #endregion

        #region 解密
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <returns>解密后的数据</returns>
        public static string Decrypt(string data)
        {
            if (data != "")
            {
                byte[] buffer3;
                byte[] bytes = Encoding.ASCII.GetBytes(KEY_64);
                byte[] rgbIV = Encoding.ASCII.GetBytes(IV_64);
                try
                {
                    buffer3 = Convert.FromBase64String(data);
                }
                catch (Exception ex)
                {
                    return null;
                }
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream(buffer3);
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(stream2);
                return reader.ReadToEnd();
            }
            return data;
        }
        #endregion
    }
}
