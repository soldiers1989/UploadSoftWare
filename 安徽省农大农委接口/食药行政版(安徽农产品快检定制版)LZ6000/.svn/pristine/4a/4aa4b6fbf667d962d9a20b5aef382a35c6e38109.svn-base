using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DY.FileLib
{
    /// <summary>
    /// 操作ini文件类型类
    /// </summary>
    public class INIFile
    {
        public string path;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="INIPath">文件路径</param>
        public INIFile(string INIPath)
        {
            this.path = INIPath;
        }

        /// <summary>
        /// 把二进制字转化为String类型
        /// </summary>
        /// <param name="sectionByte">二进制字节数组</param>
        /// <returns></returns>
        private string[] ByteToString(byte[] sectionByte)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(sectionByte).Split(new char[1]);
        }

        /// <summary>
        /// 清除所有节
        /// </summary>
        public void ClearAllSection()
        {
            this.IniWriteValue(null, null, null);
        }

        /// <summary>
        /// 清除指定节
        /// </summary>
        /// <param name="section">指定节</param>
        public void ClearSection(string section)
        {
            this.IniWriteValue(section, null, null);
        }

      

        /// <summary>
        /// 读取ini文件类型的值
        /// </summary>
        /// <param name="section">指定节</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public string IniReadValue(string section, string key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            GetPrivateProfileString(section, key, "", retVal, 0xff, this.path);
            return retVal.ToString();
        }

        /// <summary>
        /// 读取所有节
        /// </summary>
        /// <returns></returns>
        public string[] IniReadValues()
        {
            byte[] sectionByte = this.IniReadValues(null, null);
            return this.ByteToString(sectionByte);
        }

        /// <summary>
        /// 读取指节内容
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public string[] IniReadValues(string section)
        {
            byte[] sectionByte = this.IniReadValues(section, null);
            return this.ByteToString(sectionByte);
        }

        /// <summary>
        /// 读取指定节和键名内容
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] retVal = new byte[0xff];
            GetPrivateProfileString(section, key, "", retVal, 0xff, this.path);
            return retVal;
        }

        /// <summary>
        /// 对ini文件进行写操作
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void IniWriteValue(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, this.path);
        }

        #region 调用系统DLL
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #endregion
    }
}
