using System;
using System.IO;
using JH.CommBase;
using System.Windows.Forms;

namespace DY.FoodClientLib
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

		//private static string settingFileName = "Data\\DY5000LD.Xml";

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            //string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    cs = CommBase.CommBaseSettings.LoadFromXML(file.OpenRead());
            //}
            //else
            //{
                cs.SetStandard(ShareOption.ComPort, 19200, Handshake.none, Parity.odd);
                clsDY5000LD.CommBaseSettings set = cs;
                set.autoReopen = false;
                set.sendTimeoutMultiplier = 10;
                set.sendTimeoutConstant = 1000;
                set.rxLowWater = 100;
                set.rxHighWater = 100;
                set.rxQueue = 0;
                set.txQueue = 0;
                set.useDTR = JH.CommBase.CommBase.HSOutput.none;
                set.useRTS = JH.CommBase.CommBase.HSOutput.none;
                set.checkAllSends = true;
            //}
            return cs;
        }

        public void ReadNow()
        {
            byte tx = 0x61;
            Send(tx);
        }

        /// <summary>
        /// 需要根据协议修改
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        public void ReadHistory(string dtStart, string dtEnd)
        {
            string s = "s" + dtStart + dtEnd;
            System.Text.ASCIIEncoding asciicode = new System.Text.ASCIIEncoding();
            byte[] tx = asciicode.GetBytes(s);
            Send(tx);
        }

        /// <summary>
        /// 读取到数据
        /// </summary>
        /// <param name="bty"></param>
        protected override void OnRxChar(byte bty)
        {
            if (MessageNotify.Instance() != null)
            {
                MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.Read3000LDData, bty.ToString("X2"));
            }
            //if (FoodClient.frmMain.formMain.frmAutoLD != null)
            //{
            //    FoodClient.frmMain.formMain.frmAutoLD.ShowResult(bty.ToString("X2"));
            //}
        }
	}
}
