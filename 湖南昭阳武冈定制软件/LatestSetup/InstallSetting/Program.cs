using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace InstallSetting
{
   public class Program
    {
       static void Main(string[] args)
       {
           string baseFold = AppDomain.CurrentDomain.BaseDirectory;
           string file = "ET199Com.dll";
           if (!File.Exists(baseFold + file))
           {
               //MessageBox.Show("找没有授权文件，请加载授权文件");
               Console.WriteLine("缺少组件：ET199Com.dll");
               return;
           }
           string systemFold = GetSystemPath();

           //把加密组件拷贝到系统system32目录
           File.Copy(baseFold + file, systemFold + file, true);

           file = "regester.bat";
           //File.Copy(baseFold + file, systemFold + file, true);
           //Console.WriteLine("正启动注册");

           System.Diagnostics.Process.Start(baseFold + file);

           //File.Delete(baseFold + file);

           Console.WriteLine("配置成功");
           //MessageBox.Show("授权成功");
       }

        /// <summary>
        /// 获取系统的System32目录
        /// </summary>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetSystemDirectoryA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long GetSystemDirectory(StringBuilder lpBuffer, long nSize);
        /// <summary>
        /// 获取系统的Windows目录
        /// </summary>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetWindowsDirectoryA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long GetWindowsDirectory(StringBuilder lpBuffer, long nSize);

        //调用 
        public static string GetSystemPath()
        {
            StringBuilder p = new StringBuilder(100);
            long length;
            length = GetSystemDirectory(p, 100);
            return p.ToString();
        }
        public static string GetWindowsPath()
        {
            StringBuilder p = new StringBuilder(100);
            long length;
            length = GetWindowsDirectory(p, 100);
            return p.ToString();
        }
    }
}
