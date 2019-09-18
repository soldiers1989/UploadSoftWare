﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private  string preimei, imei; 
        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process[] runFst = Process.GetProcessesByName("adb");
            if (runFst.Length > 0)
            {
                for (int i = 0; i < runFst.Length; i++)
                {
                    if (runFst[i].ProcessName.StartsWith("adb"))
                    {
                        runFst[i].Kill();
                    }
                }
            }
        }

        private void KillAdb() 
        {
            //return;
            //检测adb是否运行，如果已经运行就杀掉进程
            Process[] runFst = Process.GetProcessesByName("adb");
            if (runFst.Length > 0)
            {
                for (int i = 0; i < runFst.Length; i++)
                {
                    if (runFst[i].ProcessName.StartsWith("adb"))
                    {
                        runFst[i].Kill();
                    }
                }
            }
        }

        string adbPath = Application.StartupPath + "\\adb\\adb.exe";

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                KillAdb();

                Process p = new Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo();
                p.StartInfo.FileName = adbPath;//设定程序名  
                p.StartInfo.Arguments = "forward tcp:12580 tcp:10086";
                p.StartInfo.UseShellExecute = false; //关闭shell的使用  
                p.StartInfo.RedirectStandardInput = true; //重定向标准输入  
                p.StartInfo.RedirectStandardOutput = true; //重定向标准输出  
                p.StartInfo.RedirectStandardError = true; //重定向错误输出  
                p.StartInfo.CreateNoWindow = true;//设置不显示窗口  
                p.Start();
                p.Close();

                p.StartInfo = new System.Diagnostics.ProcessStartInfo();
                p.StartInfo.FileName = adbPath;//设定程序名  
                p.StartInfo.Arguments = "shell am broadcast -a NotifyServiceStart";
                p.StartInfo.UseShellExecute = false; //关闭shell的使用  
                p.StartInfo.RedirectStandardInput = true; //重定向标准输入  
                p.StartInfo.RedirectStandardOutput = true; //重定向标准输出  
                p.StartInfo.RedirectStandardError = true; //重定向错误输出  
                p.StartInfo.CreateNoWindow = true;//设置不显示窗口  
                p.Start();
                //Thread.Sleep(3000);
                ////string ReadData = p.StandardOutput.ReadToEnd();
                //string data = "";
                //string ip = "127.0.0.1", cmd = "SAKJ";
                //TcpClient client = new TcpClient(ip, 12580);
                //IPEndPoint ipendpoint = client.Client.RemoteEndPoint as IPEndPoint;
                //NetworkStream stream = client.GetStream();
                //byte[] messages = Encoding.Default.GetBytes(cmd);
                //stream.Write(messages, 0, messages.Length);
                ////MessageBox.Show(string.Format("{0:HH:mm:ss}->发送数据(to {1})：{2}", DateTime.Now, ip, cmd));

                //byte[] bytes = new Byte[1024];
                //int length = stream.Read(bytes, 0, bytes.Length);
                //if (length > 0)
                //{
                //    data = Encoding.UTF8.GetString(bytes, 0, length);
                //    MessageBox.Show(string.Format("{0:HH:mm:ss}->接收数据(from {1}:{2})：{3}", DateTime.Now, ipendpoint.Address, ipendpoint.Port, data));
                //}

                p.Close();

                Process.Start(adbPath);

                MessageBox.Show("连接成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string listen()
        {
            string data = string.Empty;
            string ip = "127.0.0.1", cmd = "READ";
            TcpClient client = new TcpClient(ip, 12580);
            IPEndPoint ipendpoint = client.Client.RemoteEndPoint as IPEndPoint;
            NetworkStream stream = client.GetStream();
            byte[] messages = Encoding.Default.GetBytes(cmd);
            stream.Write(messages, 0, messages.Length);
            //MessageBox.Show(string.Format("{0:HH:mm:ss}->发送数据(to {1})：{2}", DateTime.Now, ip, cmd));

            byte[] bytes = new Byte[1024];
            int length = stream.Read(bytes, 0, bytes.Length);
            if (length > 0)
            {
                data = Encoding.UTF8.GetString(bytes, 0, length);
                MessageBox.Show(string.Format("{0:HH:mm:ss}->接收数据(from {1}:{2})：{3}", DateTime.Now, ipendpoint.Address, ipendpoint.Port, data));
            }
            stream.Close();
            client.Close();
            return data;
        }  
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                listen();
                //// 新建客户端套接字
                //TcpClient tcpclnt = new TcpClient();

                //// 连接服务器
                //tcpclnt.ReceiveTimeout = 3000;//设置Socket的接收超时时间为3S。
                //tcpclnt.Connect("127.0.0.1", 12580);
                ////Console.WriteLine("已连接");
                ////Console.Write("请输入要传输的字符串 : ");
                //// 读入字符串
                //String str = "READ";//Console.ReadLine();

                //// 得到客户端的流
                //Stream stm = tcpclnt.GetStream();
                //// 发送字符串
                //ASCIIEncoding asen= new ASCIIEncoding();

                ////string sedata = "READ";
                //byte[] ba=asen.GetBytes(str);
                ////Console.WriteLine("传输中.....");
                //stm.Write(ba,0,ba.Length);
                //// 接收从服务器返回的信息

                //byte[] bb=new byte[100];

                
                //int k=stm.Read(bb,0,100);   //3秒后会出现超时异常
                //// 输出服务器返回信息
                //for (int i=0;i<bb.Length ;i++)
                //{
                //    Console.Write(Convert.ToChar(bb[i]));
                //}
                //// 关闭客户端连接
                //tcpclnt.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            //Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
