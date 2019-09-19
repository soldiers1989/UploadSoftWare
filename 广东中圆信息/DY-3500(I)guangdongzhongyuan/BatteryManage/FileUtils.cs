using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BatteryManage
{
    public static class FileUtils
    {
        public static string LogDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\batteryLog";
        
        private static object olog = new object();
        private static object logfile = new object();
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="content">格式：window-方法名+异常信息</param>
        public static void Log(string content)
        {
            lock (logfile)
            {
                DateTime date = DateTime.Now;
                string fileName = date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(LogDirectory, fileName, date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
         }
       
        public static void LogType1(string content)
        {
            DateTime date = DateTime.Now;
            string fileName = date.ToString("yyyy-MM-dd") + "充放电状态.log";
            AddToFile(LogDirectory, fileName, content + "\r\n"); 
        }
       
        public static void LogType2(string content)
        {
            DateTime date = DateTime.Now;
            string fileName = date.ToString("yyyy-MM-dd") + "电池原始数据.log";
            AddToFile(LogDirectory, fileName, content + "\r\n");
        }

        public static void AddToFile(string strDirName, string strFileName, string strContent)
        {
            lock (olog)
            {
                if (!Directory.Exists(strDirName))
                    Directory.CreateDirectory(strDirName);
                FileUtils.AddToFile(strDirName + "\\" + strFileName, strContent);
            }
           
        }

        public static void AddToFile(string strPath, string strContent)
        {
            using (FileStream fs = new FileStream(strPath, FileMode.Append,FileAccess.Write ,FileShare.ReadWrite ))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(strContent);
                sw.Flush();
            }
        }

    }
}