using System;
using System.Collections.Generic;
using System.IO;

namespace com.lvrenyang
{
    public class FileUtils
    {
        public static string SaveLogDirectory = AppDomain.CurrentDomain.BaseDirectory + "log";
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

        /// <summary>
        /// 快检服务平台收发日记
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        public static void KLog(string content, string SendReceive, int type)
        {
            if (type == 1)//登录
            {
                DateTime date = DateTime.Now;
                string fileName = "登录" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\login", fileName, SendReceive + date.ToString(" HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 2)//注册
            {
                DateTime date = DateTime.Now;
                string fileName = "注册" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\register", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 3)//任务数量
            {
                DateTime date = DateTime.Now;
                string fileName = "任务数量" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Numbertasks", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 4)//检测任务接收
            {
                DateTime date = DateTime.Now;
                string fileName = "任务接收" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Taskreception", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 5)//法律法规
            {
                DateTime date = DateTime.Now;
                string fileName = "法律" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Law", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 6)//国家标准
            {
                DateTime date = DateTime.Now;
                string fileName = "国家标准" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Nationalstandard", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 7)//检测项目
            {
                DateTime date = DateTime.Now;
                string fileName = "检测项目" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\TestItem", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 8)//样品
            {
                DateTime date = DateTime.Now;
                string fileName = "样品" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Sample", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 9)//检测项目样品标准
            {
                DateTime date = DateTime.Now;
                string fileName = "检测项目样品标准" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\TestItemSampleStandard", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 10)//仪器检测项目
            {
                DateTime date = DateTime.Now;
                string fileName = "仪器检测项目" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\InstrumentalTestingProject", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }

            else if (type == 11)//下载法律文件
            {
                DateTime date = DateTime.Now;
                string fileName = "下载法律文件" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\DownloadLegalDocuments", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 12)//下载国家标准文件
            {
                DateTime date = DateTime.Now;
                string fileName = "下载国家标准文件" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\DownloadNationalStandardFiles", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 13)//软件自动更新
            {
                DateTime date = DateTime.Now;
                string fileName = "系统更新" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\SystemUpdate", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 14)//软件自动更新
            {
                DateTime date = DateTime.Now;
                string fileName = "数据上传" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\DataUpload", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 15)//任务拒收
            {
                DateTime date = DateTime.Now;
                string fileName = "任务拒收" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\objectTask", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 16)//任务拒收
            {
                DateTime date = DateTime.Now;
                string fileName = "任务接收" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\JieshouTask", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 17)//任务数量
            {
                DateTime date = DateTime.Now;
                string fileName = "任务数量" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\ManageTaskNum", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 18)//任务管理
            {
                DateTime date = DateTime.Now;
                string fileName = "任务管理" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\ManageTask", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 19)//更新任务管理状态
            {
                DateTime date = DateTime.Now;
                string fileName = "任务管理状态" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\ManageTaskStatue", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 20)//消息公报数量
            {
                DateTime date = DateTime.Now;
                string fileName = "公报数量" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\gongbaoNumber", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 21)//消息公报
            {
                DateTime date = DateTime.Now;
                string fileName = "公报消息" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\gongbao", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 22)//更新消息公报状态
            {
                DateTime date = DateTime.Now;
                string fileName = "公报消息状态" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\gongbaozhuangtai", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 23)//扫码下载任务
            {
                DateTime date = DateTime.Now;
                string fileName = "扫码下载任务" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\saoma", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
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
