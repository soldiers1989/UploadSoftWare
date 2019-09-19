using JH.CommBase;

namespace DY.FoodClientLib
{
    /// <summary>
    /// 老版本DY3000系列仪器是LD系列
    /// </summary>
    public class clsDY3000 : CommBase
    {

        public clsDY3000()
        {

        }

        //private static string settingFileName = "Data\\DY3000.Xml";

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            //string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            //FileInfo f = new FileInfo(path);
            //if (f.Exists)
            //{
            //    cs = CommBase.CommBaseSettings.LoadFromXML(f.OpenRead());
            //}
            //else
            //{
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
            //}
            return cs;
        }

        protected override void OnRxChar(byte bty)
        {
            // FoodClient.frmMain.formMain.frmAutoLD.ShowResult(c.ToString("X2"));
            if (FoodClientLib.MessageNotify.Instance() != null)
            {
                FoodClientLib.MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.Read3000LDData, bty.ToString("X2"));
            }

            //string s = c.ToString("X2");
            //frmMain.formAutoTakeDY3000.SetChar(c.ToString("X2"));
        }
    }
}
