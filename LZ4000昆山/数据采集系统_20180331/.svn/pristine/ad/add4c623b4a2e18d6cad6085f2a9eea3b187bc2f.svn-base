using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WorkstationModel.function
{
    public class FilesRW
    {
        public static string SaveLogDirectory = AppDomain.CurrentDomain.BaseDirectory + "log\\UpData";
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
                AddToFile(SaveLogDirectory, filename, content);
            }
            
        }
    }
}
