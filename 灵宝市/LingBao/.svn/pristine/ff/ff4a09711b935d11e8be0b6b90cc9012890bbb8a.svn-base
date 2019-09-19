using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WorkstationModel.function
{
    public class FilesRW
    {
        public static string SaveLogDirectory = AppDomain.CurrentDomain.BaseDirectory + "log";
        public static void SaveToFile(string strPath, string strContent)
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
            FilesRW.SaveToFile(strDirName + "\\" + strFileName, strContent);
        }
        public static void SLog(string filename,string content,int type)
        {
            if(type==1)
            {
                AddToFile(SaveLogDirectory + "\\UpData", filename, content);
            }
            
        }

         /// <summary>
        /// 快检服务平台收发日记
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        public static void KLog(string content,string SendReceive, int type)
        {
            if (type == 1)//登录
            {
                DateTime date = DateTime.Now;
                string fileName ="登录"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\login", fileName, SendReceive + date.ToString(" HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 2)//注册
            {
                DateTime date = DateTime.Now;
                string fileName ="注册"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\register", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 3)//任务数量
            { 
                DateTime date = DateTime.Now;
                string fileName ="任务数量"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Numbertasks", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 4)//检测任务接收
            {
                DateTime date = DateTime.Now;
                string fileName ="任务接收"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Taskreception", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 5)//法律法规
            {
                DateTime date = DateTime.Now;
                string fileName ="法律"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Law", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 6)//国家标准
            {
                DateTime date = DateTime.Now;
                string fileName ="国家标准"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Nationalstandard", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 7)//检测项目
            {
                DateTime date = DateTime.Now;
                string fileName ="检测项目"+ date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\TestItem", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 8)//样品
            {
                DateTime date = DateTime.Now;
                string fileName ="样品" +date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\Sample", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type == 9)//检测项目样品标准
            {
                DateTime date = DateTime.Now;
                string fileName = "检测项目样品标准" + date.ToString("yyyy-MM-dd") + ".log";
                AddToFile(SaveLogDirectory + "\\TestItemSampleStandard", fileName, SendReceive + date.ToString("HH:mm:ss\r\n") + content + "\r\n\r\n");
            }
            else if (type ==10)//仪器检测项目
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
                string fileName ="系统更新"+ date.ToString("yyyy-MM-dd") + ".log";
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
        }
    }
}
