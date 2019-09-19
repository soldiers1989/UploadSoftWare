using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports ;
using System.Threading;
using WorkstationDAL.Model;
using WorkstationUI;
using WorkstationUI.function;
using System.IO;

namespace WorkstationUI.Basic
{
    public partial class MainOpen : UserControl
    {
        public EventHandler UpdatePanel = null;
        private SerialPort Seria = new SerialPort();
        private byte getDeviceModel = 0x00;
        private string _settingType = string.Empty;
        private string _strData = string.Empty;
        
        public MainOpen()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 串口连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConNet_Click(object sender, EventArgs e)
        {
            Seria.Close();
            Seria.Dispose();
            Control.CheckForIllegalCrossThreadCalls = false;
            Seria.DataReceived += new SerialDataReceivedEventHandler(Seria_DataReceived);
            try
            {
                Int32 iBaud = Convert.ToInt32(Global.SerialBaud);
                Int32 iDateBit = Convert.ToInt32(Global.SerialData);

                Seria.PortName = Global.SerialCOM;
                Seria.BaudRate = iBaud;
                Seria.DataBits = iDateBit;

                switch (Global.SerialStop)            //停止位
                {
                    case "1":
                        Seria.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        Seria.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        Seria.StopBits = StopBits.Two;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }

                switch (Global.SerialParity)             //校验位
                {
                    case "无":
                        Seria.Parity = Parity.None;
                        break;
                    case "奇校验":
                        Seria.Parity = Parity.Odd;
                        break;
                    case "偶校验":
                        Seria.Parity = Parity.Even;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }

                if (Seria.IsOpen == true)//如果打开状态，则先关闭一下
                {
                    Seria.Close();
                }

                Seria.Open(); //打开串口
                if (Seria.IsOpen == true)
                {
                    Global.ComON = true;
                }
                Global.ComON = true;
                //_settingType = clsTool.GET_DEVICEMODEL;
                //_strData = string.Empty;
                string data = "00 00";
                string strBuffer = clsTool.GetBuffer(data, getDeviceModel);
                byte[] buffer = clsTool.StrToBytes(strBuffer);
                Seria.Write(buffer, 0, buffer.Length);
                //MessageBox.Show("串口打开成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口连接失败  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        private void Seria_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {   
            MessageBox.Show("串口连接成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Seria.Dispose();
   
            //这里处理数据接收
            //Thread.Sleep(50);//延迟一秒解决偶有漏读的bug
            ////此处可能没有必要判断是否打开，但为了严谨性，我还是加上了
            //if (Seria.IsOpen)
            //{
 
            //}
         }

        //private void btnSaveAs_Click(object sender, EventArgs e)
        //{
        //    DialogResult R=MessageBox.Show("数据备份请单击“是”，数据恢复请单击“否”，不操作请单击“取消”",
        //                "操作提示",MessageBoxButtons.YesNoCancel ,MessageBoxIcon.Information );
        //    //数据备份
        //    if (R == DialogResult.Yes)
        //    {
        //        string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
        //        string fold = "dataBackup";
        //        string fullPath = path + fold;
        //        if (!Directory.Exists(fullPath))
        //        {
        //            Directory.CreateDirectory(fullPath);
        //        }
        //        saveFileDialog1.InitialDirectory = fullPath;
        //        saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd") + ".bak";
        //        saveFileDialog1.CheckPathExists = true;
        //        saveFileDialog1.DefaultExt = "bak";
        //        saveFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
        //        DialogResult dlg = saveFileDialog1.ShowDialog(this);

        //        if (dlg == DialogResult.OK)
        //        {
        //            try
        //            {
        //                File.Copy(path + "Data\\Local.mdb", saveFileDialog1.FileName, true);
        //            }
        //            catch
        //            {
        //                MessageBox.Show("数据库备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //            MessageBox.Show("数据库备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    //数据恢复
        //    if (R == DialogResult.No)
        //    {
        //        string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
        //        string fold = "dataBackup";
        //        string fullPath = path + fold;
        //        openFileDialog1.InitialDirectory = fullPath;
        //        openFileDialog1.CheckPathExists = true;
        //        openFileDialog1.CheckFileExists = true;
        //        openFileDialog1.DefaultExt = "bak";
        //        openFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
        //        DialogResult dlg = openFileDialog1.ShowDialog(this);
        //        if (dlg == DialogResult.OK)
        //        {
        //            try
        //            {
        //                File.Copy(openFileDialog1.FileName, path + "Data\\Local.mdb", true);
        //            }
        //            catch
        //            {
        //                MessageBox.Show("数据库恢复失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //            MessageBox.Show("数据库恢复成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

       
        //统计分析
        private void pnlAnalysis_MouseClick(object sender, MouseEventArgs e)
        {
            //frmSearchData frms = new frmSearchData();
            ////frms.Close();
            //frms.Location = new Point(180, 120);
            //frms.Show();
        }

        //商城链接事件
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://tianhelvzhou.tmall.com/");
        }
        //关于的单击事件
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            frmAbout frma = new frmAbout();
            frma.Show();
        }
        //连接测试
        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            Seria.Close();
            Seria.Dispose();
            Control.CheckForIllegalCrossThreadCalls = false;
            Seria.DataReceived += new SerialDataReceivedEventHandler(Seria_DataReceived);
            try
            {
                Int32 iBaud = Convert.ToInt32(Global.SerialBaud);
                Int32 iDateBit = Convert.ToInt32(Global.SerialData);

                Seria.PortName = Global.SerialCOM;
                Seria.BaudRate = iBaud;
                Seria.DataBits = iDateBit;

                switch (Global.SerialStop)            //停止位
                {
                    case "1":
                        Seria.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        Seria.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        Seria.StopBits = StopBits.Two;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }

                switch (Global.SerialParity)             //校验位
                {
                    case "无":
                        Seria.Parity = Parity.None;
                        break;
                    case "奇校验":
                        Seria.Parity = Parity.Odd;
                        break;
                    case "偶校验":
                        Seria.Parity = Parity.Even;
                        break;
                    default:
                        MessageBox.Show("Error：参数不正确!", "系统提示");
                        break;
                }

                if (Seria.IsOpen == true)//如果打开状态，则先关闭一下
                {
                    Seria.Close();
                }

                Seria.Open(); //打开串口
                if (Seria.IsOpen == true)
                {
                    Global.ComON = true;
                }
                Global.ComON = true;
                //_settingType = clsTool.GET_DEVICEMODEL;
                //_strData = string.Empty;
                string data = "00 00";
                string strBuffer = clsTool.GetBuffer(data, getDeviceModel);
                byte[] buffer = clsTool.StrToBytes(strBuffer);
                Seria.Write(buffer, 0, buffer.Length);
                //MessageBox.Show("串口打开成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口连接失败  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void pnlSave_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult R = MessageBox.Show("数据备份请单击“是”，数据恢复请单击“否”，不操作请单击“取消”",
                        "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            //数据备份
            if (R == DialogResult.Yes)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
                string fold = "dataBackup";
                string fullPath = path + fold;
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                saveFileDialog1.InitialDirectory = fullPath;
                saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd") + ".bak";
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "bak";
                saveFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
                DialogResult dlg = saveFileDialog1.ShowDialog(this);

                if (dlg == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(path + "Data\\Local.mdb", saveFileDialog1.FileName, true);
                    }
                    catch
                    {
                        MessageBox.Show("数据库备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    MessageBox.Show("数据库备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //数据恢复
            if (R == DialogResult.No)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
                string fold = "dataBackup";
                string fullPath = path + fold;
                openFileDialog1.InitialDirectory = fullPath;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.DefaultExt = "bak";
                openFileDialog1.Filter = "备份文件(*.bak)|*.bak|All files (*.*)|*.*";
                DialogResult dlg = openFileDialog1.ShowDialog(this);
                if (dlg == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(openFileDialog1.FileName, path + "Data\\Local.mdb", true);
                    }
                    catch
                    {
                        MessageBox.Show("数据库恢复失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MessageBox.Show("数据库恢复成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void pnlAnalysis_Paint(object sender, PaintEventArgs e)
        {

        }

      


    }
    
}
