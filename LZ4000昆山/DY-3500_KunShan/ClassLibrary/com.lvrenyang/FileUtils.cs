using System;
using System.Collections.Generic;
using System.IO;

namespace com.lvrenyang
{
    public class FileUtils
    {
        public static void AddToFile(string strPath, string strContent)
        {
            using (FileStream fs = new FileStream(strPath, FileMode.Append))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(strContent);
                sw.Flush();
            }
        }

        public static void AddToFile(string strDirName, string strFileName, string strContent)
        {
            if (!Directory.Exists(strDirName))
                Directory.CreateDirectory(strDirName);
            FileUtils.AddToFile(strDirName + "\\" + strFileName, strContent);
        }

        public static void SaveToFile(string strPath, string strContent)
        {
            using (FileStream fs = new FileStream(strPath, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(strContent);
                sw.Flush();
            }
        }

        public static void SaveToFile(string strPath, byte[] buffer, int offset, int count)
        {
            using (FileStream fs = new FileStream(strPath, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buffer, offset, count);
                bw.Flush();
            }
        }

        public static string ReadStringFromFile(string strPath)
        {
            using (FileStream fs = new FileStream(strPath, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                return sr.ReadToEnd();
            }
        }

        public static List<string> ReadStringsFromFile(string strPath)
        {
            List<string> content = new List<string>();
            using (FileStream fs = new FileStream(strPath, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    content.Add(sr.ReadLine());
                }
            }
            return content;
        }

        public static string LogDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\log";
        public static void Log(string content)
        {
            DateTime date = DateTime.Now;
            string fileName = date.ToString("yyyy-MM-dd") + ".log";
            AddToFile(LogDirectory, fileName, date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
        }

        public static void UploadLog(string content)
        {
            DateTime date = DateTime.Now;
            string fileName = "UploadLog " + date.ToString("yyyy-MM-dd") + ".log";
            AddToFile(Environment.CurrentDirectory + "\\log", fileName, date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
        }

        /// <summary>
        /// ErrorLog
        /// </summary>
        /// <param name="fileName">模块名称例如JTJ</param>
        /// <param name="errorName">异常名称</param>
        /// <param name="content">异常类容</param>
        public static void ErrorLog(string fileName, string errorName, string content)
        {
            DateTime date = DateTime.Now;
            fileName = date.ToString("yyyy-MM-dd") + "_" + fileName + ".log";
            AddToFile(Environment.CurrentDirectory + "\\log", fileName, date.ToString("HH:mm:ss") + "_" + errorName + "\r\n" + content + "\r\n\r\n");
        }

        // 枚举根目录下的所有文件，包括子目录
        public static void EnmurateFile(string root, List<string> files)
        {
            // 将目录下的文件添加进去
            files.AddRange(Directory.EnumerateFiles(root));

            // 枚举目录
            IEnumerable<string> dirs = Directory.EnumerateDirectories(root);
            foreach (string dir in dirs)
            {
                EnmurateFile(dir, files);
            }
        }

    }
}