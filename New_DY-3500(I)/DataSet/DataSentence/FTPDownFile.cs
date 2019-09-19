using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Net;


namespace DataSetModel.DataSentence
{
    public  class FTPDownFile
    {

        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名,20120817,ylh
        /// </summary>
        ///  <param name="ftpPath">要下载文件所在ftp上的完整路径，如ftp://192.168.0.111/2012-08-17/yinluhui.xml</param>
        /// <param name="ftpFile">要下载文件的文件名,如yinluhui.xml</param>
        /// <param name="LocalPath">本地路径,如D:\ftp临时文件\20120817</param>
        public bool DownFtpToLocation(String ftpPath, String ftpFile, String LocalPath, string username, string password)
        {
            byte[] bt = null;
            try
            {
                //if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(LocalFileName) || !IsValidPathChars(LocalPath))
                //{
                //    throw new Exception("非法文件名或目录名!");
                //}
                if (Directory.Exists(LocalPath) == false)
                {
                    Directory.CreateDirectory(LocalPath);
                }
                string LocalFullPath = Path.Combine(LocalPath, ftpFile);
                //if (File.Exists(LocalFullPath))
                //{
                //    throw new Exception("当前路径下已经存在同名文件！");
                //}
                bt = DownloadFile(ftpPath, LocalPath, username, password);
                if (bt != null)
                {
                    FileStream stream = new FileStream(LocalFullPath, FileMode.Create);
                    stream.Write(bt, 0, bt.Length);
                    stream.Flush();
                    stream.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ep)
            {
                //ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        /// <summary>
        /// 从FTP服务器下载文件，返回文件二进制数据
        /// </summary>
        public byte[] DownloadFile(String ftpPath, String LocalPath, string username, string password)
        {
            try
            {
                //if (!IsValidFileChars(RemoteFileName))
                //{
                //    throw new Exception("非法文件名或目录名!");
                //}
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
                ftpRequest.Credentials = new NetworkCredential(username, password);//登陆ftp的用户名，密码
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse Response = (FtpWebResponse)ftpRequest.GetResponse();
                Stream Reader = Response.GetResponseStream();

                MemoryStream mem = new MemoryStream(1024 * 500);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                int TotalByteRead = 0;
                while (true)
                {
                    bytesRead = Reader.Read(buffer, 0, buffer.Length);
                    TotalByteRead += bytesRead;
                    if (bytesRead == 0)
                        break;
                    mem.Write(buffer, 0, bytesRead);
                }
                if (mem.Length > 0)
                {
                    return mem.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ep)
            {
                throw ep;
            }
        }
    }
}
