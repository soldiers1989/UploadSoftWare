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
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="content">格式：window-方法名+异常信息</param>
        public static void Log(string content)
        {
            DateTime date = DateTime.Now;
            string fileName = date.ToString("yyyy-MM-dd") + ".log";
            AddToFile(LogDirectory, fileName,  date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
        }

        public static void ErrorLog(string fileName, string log) 
        {
            DateTime date = DateTime.Now;
            fileName = string.Format("{0} {1}.log", date.ToString("yyyy-MM-dd"), fileName);
            AddToFile(string.Format("{0}\\log\\{1}", Environment.CurrentDirectory, "Other"),
                fileName, string.Format("{0}\r\n",  log));
        }

        /// <summary>
        /// 所有操作日志记录 0服务器通讯相关 1分光 2胶体金 3干化学 4重金属 5ATP 6其他
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        /// <param name="log"></param>
        public static void OprLog(int type, string fileName, string log)
        {
            string typeName = string.Empty;
            switch (type)
            {
                //服务器通讯相关
                case 0:
                    typeName = "SERVER";
                    break;
                //分光模块
                case 1:
                    typeName = "FG";
                    break;
                //胶体金模块
                case 2:
                    typeName = "JTJ";
                    break;
                //干化学模块
                case 3:
                    typeName = "GHX";
                    break;
                //重金属模块
                case 4:
                    typeName = "ZJS";
                    break;
                //ATP模块
                case 5:
                    typeName = "ATP";
                    break;
                //其他模块
                default:
                    typeName = "Other";
                    break;
            }
            DateTime date = DateTime.Now;
            fileName = string.Format("{0} {1}.log", date.ToString("yyyy-MM-dd"), fileName);
            AddToFile(string.Format("{0}\\log\\{1}", Environment.CurrentDirectory, typeName),
                fileName, string.Format("{0}\r\n{1}\r\n\r\n", date.ToString("HH:mm:ss"), log));
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
