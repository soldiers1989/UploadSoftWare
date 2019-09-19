using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private System.Timers.Timer Times = new System.Timers.Timer();//定时器
        private delegate void SetTBMethodInvok();

        private void Form1_Load(object sender, EventArgs e)
        {
          
            Times.Enabled = true;
            Times.Elapsed += new System.Timers.ElapsedEventHandler(Times_Elapsed);
            Times.Interval = 5000;//5S
            Times.Start();
        }
        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Times_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            timeupdate();

        }
       
        private void timeupdate()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTBMethodInvok(timeupdate));
            }
            else 
            {

                //关闭打开的软件
                Process[] allProcess = Process.GetProcesses();
                foreach (Process p in allProcess)
                {
                    if (p.ProcessName == "DY-Detector")
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                    }
                }
                UpdateFile();//复制文件
                Thread.Sleep(1000);//延时2秒
                Directory.Delete("D:\\update", true);//删除文件
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\DY-Detector.exe");//启动程序
                this.Close();
            }
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private void UpdateFile()
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                //覆盖旧文件
                CopyFile(Directory.GetCurrentDirectory() + "\\jieya\\快检服务DY3500(I)\\", Directory.GetCurrentDirectory());//+ "\\update\\"
                //System.IO.Directory.Delete(Directory.GetCurrentDirectory() + "", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "升级出错！\r\n\r\n异常信息：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="objPath"></param>
        public void CopyFile(string sourcePath, string objPath)
        {
            string errorLog = "";
            if (!Directory.Exists(objPath))
            {
                Directory.CreateDirectory(objPath);
            }
            string[] files = Directory.GetFiles(sourcePath);
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string[] childfile = files[i].Split('\\');
                    File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
                }
                catch (Exception ex)
                {
                    errorLog += string.Format("[{0}]", ex.Message);
                }
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    string[] childdir = dirs[i].Split('\\');
                    CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
                }
                catch (Exception ex)
                {
                    errorLog += string.Format("[{0}]", ex.Message);
                }
            }
        }
     
    }
}
