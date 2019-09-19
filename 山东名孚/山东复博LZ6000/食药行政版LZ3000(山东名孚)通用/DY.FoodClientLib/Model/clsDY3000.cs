using System;
using System.IO;
using JH.CommBase;
//using System.Windows.Forms;


namespace WindowsFormClient.BaseClass
{
	/// <summary>
	/// clsDY3000 的摘要说明。
	/// </summary>
    public class clsDY3000 : CommBase
    {
        public clsDY3000()
        {

        }

        private static string settingFileName = "Data\\DY3000.Xml";

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            FileInfo f = new FileInfo(path);
            if (f.Exists)
            {
                cs = CommBase.CommBaseSettings.LoadFromXML(f.OpenRead());
            }
            else
            {
                cs.SetStandard(ShareOption.ComPort, 19200, Handshake.none, Parity.even);
                clsDY3000.CommBaseSettings s = cs;
                s.autoReopen = false;
                s.sendTimeoutMultiplier = 0;
                s.sendTimeoutConstant = 0;
                s.rxLowWater = 0;
                s.rxHighWater = 0;
                s.rxQueue = 0;
                s.txQueue = 0;
                s.checkAllSends = true;
            }
            return cs;
        }

        protected override void OnRxChar(byte c)
        {
            //string s = c.ToString("X2");
            frmMain.formMain.frmAutoLD.SetChar(c.ToString("X2"));
            //frmMain.formAutoTakeDY3000.SetChar(c.ToString("X2"));
        }
    }
}
