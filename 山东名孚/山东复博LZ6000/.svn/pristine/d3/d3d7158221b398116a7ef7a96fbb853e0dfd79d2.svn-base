using System;
using System.IO;
using JH.CommBase;
using System.Windows.Forms;

namespace WindowsFormClient.BaseClass
{
	/// <summary>
	/// clsDY5000LD 的摘要说明。
	/// </summary>
	public class clsDY5000LD:CommBase
	{
        /// <summary>
        /// 构造函数
        /// </summary>
		public clsDY5000LD()
		{
			
		}

		private static string settingFileName = "Data\\DY5000LD.Xml";

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                cs = CommBase.CommBaseSettings.LoadFromXML(file.OpenRead());
            }
            else
            {
                cs.SetStandard(ShareOption.ComPort, 19200, Handshake.none, Parity.odd);
                clsDY5000LD.CommBaseSettings s = cs;
                s.autoReopen = false;
                s.sendTimeoutMultiplier = 10;
                s.sendTimeoutConstant = 1000;
                s.rxLowWater = 100;
                s.rxHighWater = 100;
                s.rxQueue = 0;
                s.txQueue = 0;
                s.useDTR = JH.CommBase.CommBase.HSOutput.none;
                s.useRTS = JH.CommBase.CommBase.HSOutput.none;
                s.checkAllSends = true;
            }
            return cs;
        }

        /// <summary>
        /// 读取到数据
        /// </summary>
        /// <param name="bty"></param>
        protected override void OnRxChar(byte bty)
        {
            if (frmMain.formMain.frmAutoLD != null)
            {
                frmMain.formMain.frmAutoLD.SetChar(bty.ToString("X2"));
            }
        }
	}
}
