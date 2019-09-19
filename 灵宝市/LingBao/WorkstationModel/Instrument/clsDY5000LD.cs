using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.CommBase;
using WorkstationModel.Model;

namespace WorkstationModel.Instrument
{
    public class clsDY5000LD:CommBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY5000LD()
        {

        }
        
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
        /// 读取数据
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        public void ReadHistory(DateTime dtStart, DateTime dtEnd)
        {
            string s = "s" + dtStart.ToString() + dtEnd.ToString();
            System.Text.ASCIIEncoding asciicode = new System.Text.ASCIIEncoding();
            byte[] tx = asciicode.GetBytes(s);
            Send(tx);
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="bty"></param>
        protected override void OnRxChar(byte bty)
        {
            if (clsMessageNotify.Instance() != null)
            {
                clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.Read3000LDData, bty.ToString("X2"));
            }
            //if (FoodClient.frmMain.formMain.frmAutoLD != null)
            //{
            //    FoodClient.frmMain.formMain.frmAutoLD.ShowResult(bty.ToString("X2"));
            //}
        }


    }
}
