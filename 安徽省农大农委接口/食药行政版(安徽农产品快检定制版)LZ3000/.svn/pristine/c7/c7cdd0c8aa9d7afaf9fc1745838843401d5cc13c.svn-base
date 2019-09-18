using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

//using ET199ComLib;

namespace LicensesTool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            string config = AppDomain.CurrentDomain.BaseDirectory + "Config.ini";

            if (!System.IO.File.Exists(config))
            {
                return;
            }
            DY.FileLib.INIFile ini = new DY.FileLib.INIFile(config);
			////string date = DY.Security.RandomEncryption.Operate(ini.IniReadValue("DateSetting", "Day"), false);
			////string interval = DY.Security.RandomEncryption.Operate(ini.IniReadValue("DateSetting", "Interval"), false);
			//DateTime dt = Convert.ToDateTime(date).AddDays(int.Parse(interval));

            DateTime dtNow = Daytime.GetTime().ToLocalTime();
			//if (dtNow > dt)
			//{
			//    MessageBox.Show("加密狗制作工具已经过期");
			//    return;
			//}
            Application.Run(new FrmMain());
        }

        private string baseFold = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string file = "licenses.licx";

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtLicenseFile.Text = baseFold;
        }
        /// <summary>
        /// 生成授权文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateLicense_Click(object sender, EventArgs e)
        {
		   // string gid = string.Empty;
		   // //string gid = Guid.NewGuid().ToString();//生成GUID;
		   // //ET199 et = null;
		   // //et = new ET199();

		   // baseFold = txtLicenseFile.Text+"\\";

		   // //int devCount = et.Enum();//获取加密锁设备

		   // if (devCount <= 0)
		   // {
		   //     MessageBox.Show("没有插入加密狗,请插入加密狗后重试!");
		   //     return;
		   // }
		   // et.Open(0);//打开加密锁

		   // byte[] byID = (byte[])et.ID;//加密锁系列号
          
		   // for (int i = 0; i < byID.Length; i++)
		   // {
		   //     gid += byID[i].ToString("x4") + "-";
		   // }
		   // gid = gid.TrimEnd('-');
            
		   //// txtGUID.Text = gid;
		   // string rand = "-" + CreateRandomNum(4);
		   // string last = gid.Insert(7, rand);//第7位插入随机干扰码
		   // StreamWriter sw = null;
		   // try
		   // {
		   //     last = DY.Security.SystemEncryption.DESEncrypt(last);
		   //     sw = File.CreateText(baseFold + file);//在当前工具目录下生成一个授权文件
		   //     sw.Write(last);
		   //     sw.Close();
		   //    // Encrypt(baseFold + file);
		   //     MessageBox.Show("制作成功");
		   // }
		   // catch (Exception ex)
		   // {
		   //     MessageBox.Show(ex.Message);
		   // }
        }

        ///// <summary>
        ///// 简单内容加密
        ///// </summary>
        ///// <param name="file"></param>
        //private void Encrypt(string file)
        //{
        //    FileStream fsr = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    long len = fsr.Length;
        //    byte[] read = new byte[len];
        //    int rlen = read.Length;
        //    fsr.Read(read, 0, rlen);
        //    fsr.Close();

        //    FileInfo fInfo = new FileInfo(file);
        //    StreamWriter w = fInfo.CreateText();
        //    int ka = 3, kb = 5, kc = 2, kd = 7, js = 0;
        //    StringBuilder builder = new StringBuilder(rlen * 2);
        //    for (int i = 0; i < rlen - 1; i += 2)
        //    {
        //        char c1 = (char)read[i];
        //        char c2 = (char)read[i + 1];
        //        int tmp = ka * c1 + kc * c2;
        //        while (tmp < 0)
        //        {
        //            tmp += 1024;
        //        }
        //        char s1 = (char)(tmp % 1024);
        //        char high = (char)((s1 >> 4) & 0x0f);
        //        char low = (char)(s1 & 0x0f);
        //        high = (char)(high < 10 ? (high + '0') : (high - (char)10 + 'A'));
        //        low = (char)(low < 10 ? (low + '0') : (low - (char)10 + 'A'));
        //        builder.Append(high);
        //        builder.Append(low);
        //        tmp = kb * c1 + kd * c2;
        //        while (tmp < 0)
        //        {
        //            tmp += 1024;
        //        }
        //        char s2 = (char)(tmp % 1024);
        //        high = (char)((s2 >> 4) & 0x0f);
        //        low = (char)(s2 & 0x0f);
        //        high = (char)(high < 10 ? (high + '0') : (high - (char)10 + 'A'));
        //        low = (char)(low < 10 ? (low + '0') : (low - (char)10 + 'A'));
        //        builder.Append(high);
        //        builder.Append(low);
        //    }
        //    if (js == 1)
        //    {
        //        char s3 = (char)((read[read.Length - 1] - 4) % 1024);
        //        char high = (char)((s3 >> 4) & 0x0f);
        //        char low = (char)(s3 & 0x0f);
        //        high = (char)(high < 10 ? (high + '0') : (high - (char)10 + 'A'));
        //        low = (char)(low < 10 ? (low + '0') : (low - (char)10 + 'A'));
        //        builder.Append(high);
        //        builder.Append(low);
        //    }
        //    w.Write(builder.ToString());
        //    w.Flush();
        //    w.Close();
        //}

        ///// <summary>
        ///// 简单内容解密
        ///// </summary>
        ///// <param name="file"></param>
        //private void Decrypt(string file)
        //{
        //    FileStream fsr = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    byte[] btArr = new byte[fsr.Length];
        //    fsr.Read(btArr, 0, btArr.Length);
        //    fsr.Close();

        //    for (int i = 0; i < btArr.Length; i++)
        //    {
        //        int ibt = btArr[i];
        //        ibt -= 100;
        //        ibt += 256;
        //        ibt %= 256;
        //        btArr[i] = Convert.ToByte(ibt);
        //    }
        //    //string strFileName = Path.GetExtension(file);
        //    FileStream fsw = new FileStream(file, FileMode.Create, FileAccess.Write);
        //    fsw.Write(btArr, 0, btArr.Length);
        //    fsw.Close();
        //}

        /// <summary>
        /// 生成随机字母
        /// </summary>
        /// <param name="chArray">随机基数组</param>
        /// <param name="num"></param>
        /// <returns></returns>
        string CreateRandom(char[] chArray, int num)//, bool IsSleep
        {
            int len = chArray.Length;
            StringBuilder sbRd = new StringBuilder();
            Random random = new Random(~(int)DateTime.Now.Ticks);
            int index = 0;
            for (int i = 0; i < num; i++)
            {
                index = random.Next(0, len);
                sbRd.Append(chArray[index]);
            }
            return sbRd.ToString();
        }
        /// <summary>
        /// 生成随机数字及字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        string CreateRandomNum(int num)
        {
            char[] chArray = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f' };
            return CreateRandom(chArray, num);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            Application.Exit();
            //base.OnClosing(e);
        }

        /// <summary>
        /// 浏览到授权文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBroswerLicense_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDlg = new FolderBrowserDialog();

            //OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.RootFolder = Environment.SpecialFolder.DesktopDirectory;// baseFold;

            //fileDlg.Filter = "授权文件(*.licx)|*.licx";
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                txtLicenseFile.Text = fileDlg.SelectedPath;
            }
        }

        private void btnOpenSet_Click(object sender, EventArgs e)
        {
            FrmPwd frm = new FrmPwd();
            frm.Show();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.I)
            {
                btnOpenSet.PerformClick();
            }
        }

        ///// <summary>
        ///// 授权文件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnLicense_Click(object sender, EventArgs e)
        //{
        //    if (!File.Exists(txtLicenseFile.Text))
        //    {
        //        MessageBox.Show("找没有授权文件，请加载授权文件");
        //        return;
        //    }
        //    File.Copy(txtLicenseFile.Text, baseFold + file, true);
        //    MessageBox.Show("授权成功");
        //}
    }
}
