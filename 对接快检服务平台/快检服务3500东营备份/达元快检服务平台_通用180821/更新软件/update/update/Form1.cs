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
using System.Runtime.InteropServices;
using AIO.src;

namespace update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string ServerPath = "";
        private System.Timers.Timer Times = new System.Timers.Timer();//定时器
        private delegate void SetTBMethodInvok();
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private string path = Environment.CurrentDirectory + "\\Updateload.ini";
        private void Form1_Load(object sender, EventArgs e)
        {
            ServerPath = IniReadValue("Server","ServerPath");

            Times.Enabled = true;
            Times.Elapsed += new System.Timers.ElapsedEventHandler(Times_Elapsed);
            Times.Interval = 3000;//3S
            Times.Start();
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, path);
            return temp.ToString();
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        private bool Downfile(string server)
        {
            bool isok = false;

            if (!Directory.Exists("D:\\update"))
            {
                Directory.CreateDirectory("D:\\update");
            }
            string down = VersionUpdate(server);
            if (down == "OK")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //string dp = path;
                string[] FileProperties = new string[2];
               
                string [] filenames=Directory.GetFiles("D:\\update");
                for (int i = 0; i < filenames.GetLength(0);i++ )
                {
                    if(filenames[i].Contains(".rar"))
                    {
                        FileProperties[0] = filenames[i];
                    }
                }
                //"D:\\update\\Debug.rar";//"D:\\zip\\Debug.rar";//待解压的文件
                FileProperties[1] = "D:\\update\\updatefile";//解压后放置的目标目录
                //判断是否存在文件夹
                if (!Directory.Exists("D:\\update\\updatefile"))
                {
                    Directory.CreateDirectory("D:\\update\\updatefile");
                }
                zipdecompression zd = new zipdecompression();
                bool rt = zd.UnRarOrZip(FileProperties[1], FileProperties[0], true, "123");
                if (rt)
                {
                    //DialogResult dr = MessageBox.Show("系统更新完成是否启动成功","系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    isok = true;
                    //try
                    //{
                    //    System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\update.exe");
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("未找到系统自动升级服务！\r\n\r\n请联系软件供应商提供系统自动升级服务程序！", "提示",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                    //}
                }
                //UnZipFloClass UnZc = new UnZipFloClass();
                //UnZc.unZipFile(FileProperties[0], FileProperties[1]);
            }
            return isok;
        }
        /// <summary>
        /// 下载更新文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string VersionUpdate(string url)
        {
            try
            {
                WebClient client = new WebClient();
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                client.DownloadFile(new Uri(url), "D:\\update\\Debug.rar");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }


        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Times_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Times.Stop();
            timeupdate();
        }
        private Thread threadDown = null;
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

                threadDown = new Thread(new ThreadStart(UpdateSys));
                threadDown.IsBackground = true;
                threadDown.Start();

            }
        }

        private void UpdateSys()
        {
            bool dd = Downfile(ServerPath);
            if (dd)//下载更新包
            {
                Thread.Sleep(1000);//延时1秒
                UpdateFile();//复制文件
            }
            this.Close();
        }
        /// <summary>
        /// 更新文件
        /// </summary>
        private void UpdateFile()
        {
            try
            {
                string pathp = Environment.CurrentDirectory;
                string[] filenames = Directory.GetFiles("D:\\update");

                DirectoryInfo theFolder = new DirectoryInfo(@"D:\\update\\updatefile");
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();

                //string[] dp= Directory.GetFiles("D:\\update\\updatefile");//获取解压文件夹内的文件名
                //覆盖旧文件
                CopyFile(dirInfo[0].FullName, pathp);//+ "\\update\\"
                //System.IO.Directory.Delete(Directory.GetCurrentDirectory() + "", true);

                Thread.Sleep(2000);//延时1秒
                Directory.Delete("D:\\update", true);//删除文件

                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\DY-Detector.exe");
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
                    if(!files[i].Contains("update.exe"))
                    {
                        string[] childfile = files[i].Split('\\');
                        File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
                    }
                   
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
                    if (!files[i].Contains("update.exe"))
                    {
                        string[] childdir = dirs[i].Split('\\');
                        CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
                    }
                }
                catch (Exception ex)
                {
                    errorLog += string.Format("[{0}]", ex.Message);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            threadDown.Abort();
        }
     
    }
}
