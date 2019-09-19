using System;
using System.IO;
using System.Windows;
using com.lvrenyang;

namespace AIO
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Global.workThread = new WorkThread();
            Global.workThread.Start();
            Global.printThread = new WorkThread();
            Global.printThread.Start();
            Global.updateThread = new WorkThread();
            Global.updateThread.Start();
            // 这两个文件夹开始的时候就创建好，因为后面要存放数据时，都没有再判断文件夹
            if (!Directory.Exists(Global.ItemsDirectory))
                Directory.CreateDirectory(Global.ItemsDirectory);
            if (!Directory.Exists(Global.AccountsDirectory))
                Directory.CreateDirectory(Global.AccountsDirectory);
            if (!Directory.Exists(Global.OthersDirectory))
                Directory.CreateDirectory(Global.OthersDirectory);
            if (!Directory.Exists(Global.TxtItemsDirectory))
                Directory.CreateDirectory(Global.TxtItemsDirectory);
            FileUtils.LogDirectory = Global.LogDirectory;
            // 读取config文件，并设置全局变量
            Global.strADPORT = CFGUtils.GetConfig("ADPORT", "COM7");
            Global.strSXT1PORT = CFGUtils.GetConfig("SXT1PORT", "COM8");
            Global.strSXT2PORT = CFGUtils.GetConfig("SXT2PORT", "COM9");
            Global.strSXT3PORT = CFGUtils.GetConfig("SXT3PORT", "COM12");
            Global.strSXT4PORT = CFGUtils.GetConfig("SXT4PORT", "COM13");
            Global.strPRINTPORT = CFGUtils.GetConfig("PRINTPORT", "COM10");
            Global.strHMPORT = CFGUtils.GetConfig("HMPORT", "COM11");
            Global.strWSWPATH = CFGUtils.GetConfig("WSWPATH", "C:\\");
            Global.strGZZPATH = CFGUtils.GetConfig("GZZPATH", "C:\\");
            Global.updateServer = new UpdateServer()
            {
                ServerAddr = CFGUtils.GetConfig("SERVERADDR", "http://127.0.0.1:8080/web/services/DataSyncService"),
                RegisterID = CFGUtils.GetConfig("REGISTERID", "user"),
                RegisterPassword = CFGUtils.GetConfig("REGISTERPASSWORD", "123456"),
                CheckPointID = CFGUtils.GetConfig("CHECKPOINTID", "001002001002"),
                CheckPointName = CFGUtils.GetConfig("CHECKPOINTNAME", "食品药品监督管理局检测中心"),
                CheckPointType = CFGUtils.GetConfig("CHECKPOINTTYPE", "检测中心"),
                Organization = CFGUtils.GetConfig("ORGANIZATION", "食品药品监督管理局")
            };
            Global.Version = CFGUtils.GetConfig("VERSION", "XZ");
            Global.IsTest = CFGUtils.GetConfig("TEST", "0").Equals("0") ? false : true;
            Global.VideoAddress = Environment.CurrentDirectory + "\\" + CFGUtils.GetConfig("VIDEOADDRESS", string.Empty);
            Global.set_FaultDetection = CFGUtils.GetConfig("FAULTDETECTION", "0").Equals("0") ? false : true;
            Global.set_ShowFgd = CFGUtils.GetConfig("SHOWFGD", "0").Equals("0") ? false : true;
            Global.IsEnableVideo = CFGUtils.GetConfig("IsEnableVideo", "0").Equals("0") ? false : true;
            Global.IsSetIndex = CFGUtils.GetConfig("ISSETINDEX", "0").Equals("0") ? false : true;
            Global.IsEnableWswOrAtp = CFGUtils.GetConfig("EnableWswOrAtp", "0").Equals("0") ? false : true;
            Global.IsWswOrAtp = CFGUtils.GetConfig("IsWswOrAtp", "WSW");
            Global.MicrobialAddress = CFGUtils.GetConfig("MICROBIALADDRESS", "MICROBIAL"); //;Environment.CurrentDirectory + "\\" + CFGUtils.GetConfig("MICROBIALADDRESS", "MICROBIAL");
            Global.IsDELETED = CFGUtils.GetConfig("ISDELETED", "0").Equals("0") ? false : true;
            Global.PdfAddress = Environment.CurrentDirectory + "\\" + CFGUtils.GetConfig("PDFADDRESS", string.Empty);
            Global.EachDistrict = CFGUtils.GetConfig("EACHDISTRICT", string.Empty);
            Global.ExportType = CFGUtils.GetConfig("PRINTTYPE", "Excel");
            Global.IsUpdateChekcedValue = CFGUtils.GetConfig("ISUPDATECHECKEDVALUE", "0").Equals("0") ? false : true;
            Global.IsOpenFile = CFGUtils.GetConfig("ISOPENFILE", "0").Equals("0") ? false : true;
            Global.InstrumentName = CFGUtils.GetConfig("InstrumentName", "食品综合分析仪");
            Global.InstrumentNameModel = CFGUtils.GetConfig("InstrumentNameModel", "DY-3500");
            Global.InterfaceType = CFGUtils.GetConfig("InterfaceType", "DY");
            Global.LoggonNmae = CFGUtils.GetConfig("LogonUser", "13814601756");

            //广东省智慧云平台相关
            Wisdom.GETSAMPLE_URL = CFGUtils.GetConfig("getsampleURL", string.Empty);
            //Wisdom.UPLOADSAMPLE_URL = CFGUtils.GetConfig("uploadSampleURL", string.Empty);
            Wisdom.UPLOADRESULT_URL = CFGUtils.GetConfig("uploadResultURL", string.Empty);
            //Wisdom.DOWNLOADRESULT_URL = CFGUtils.GetConfig("downloadResultURL", string.Empty);
            Wisdom.DEVICESTATUS_URL = CFGUtils.GetConfig("deviceStatusURL", string.Empty);
            Wisdom.DeviceID = CFGUtils.GetConfig("DeviceId", string.Empty);
            Wisdom.USER = CFGUtils.GetConfig("USER", string.Empty);
            Wisdom.PASSWORD = CFGUtils.GetConfig("PASSWORD", string.Empty);
            Wisdom.gpsAddress = CFGUtils.GetConfig("GPSADDRES", string.Empty);
            Wisdom.gpsJD = CFGUtils.GetConfig("GPSJD", string.Empty);
            Wisdom.gpsWD = CFGUtils.GetConfig("GPSWD", string.Empty);
            Global.IsSelectSampleName = CFGUtils.GetConfig("IsSelectSampleName", "0").Equals("0") ? false : true;

            // 读取配置文件，有多少个检测孔，这个是第一个需要确定的。
            Global.DeSerializeFromFile(out Global.deviceHole, Global.deviceHoleFile);
            try
            {
                Int32.TryParse(CFGUtils.GetConfig("HOLECOUNT", "16"), out Global.deviceHole.HoleCount);
                Int32.TryParse(CFGUtils.GetConfig("SXTCOUNT", "4"), out Global.deviceHole.SxtCount);
                Int32.TryParse(CFGUtils.GetConfig("HMCOUNT", "1"), out Global.deviceHole.HmCount);
                //滤光片%30
                Double.TryParse(CFGUtils.GetConfig("STANDARD1", "0.2994"), out Global.Standard1);
                Double.TryParse(CFGUtils.GetConfig("STANDARD2", "0.3008"), out Global.Standard2);
                Double.TryParse(CFGUtils.GetConfig("STANDARD3", "0.2609"), out Global.Standard3);
                Double.TryParse(CFGUtils.GetConfig("STANDARD4", "0.2534"), out Global.Standard4);
                Double.TryParse(CFGUtils.GetConfig("DECISIONCRITERIA1", "0.2"), out Global.DecisionCriteria1);
                Double.TryParse(CFGUtils.GetConfig("DECISIONCRITERIA2", "0.1"), out Global.DecisionCriteria2);
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
            Global.DeSerializeFromFile(out Global.userAccounts, Global.userAccountsFile);
            Global.DeSerializeFromFile(out Global.fgdItems, Global.fgdItemsFile);
            Global.DeSerializeFromFile(out Global.gszItems, Global.gszItemsFile);
            Global.DeSerializeFromFile(out Global.jtjItems, Global.jtjItemsFile);
            Global.DeSerializeFromFile(out Global.hmItems, Global.hmItemsFile);
            //Sample Adapter for StandardValue and display items2015-06-18
            Global.DeSerializeFromFile(out Global.samplenameadapter, Global.samplenameadapterFile);
            Global.DeSerializeFromFile(out Global.adapteritem, Global.adapteritemFile);
            Global.DeSerializeFromFile(out Global.displayItems, Global.displayItemsFile);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            CFGUtils.SaveConfig("ADPORT", Global.strADPORT);
            CFGUtils.SaveConfig("SXT1PORT", Global.strSXT1PORT);
            CFGUtils.SaveConfig("SXT2PORT", Global.strSXT2PORT);
            CFGUtils.SaveConfig("SXT3PORT", Global.strSXT3PORT);
            CFGUtils.SaveConfig("SXT4PORT", Global.strSXT4PORT);
            CFGUtils.SaveConfig("PRINTPORT", Global.strPRINTPORT);
            CFGUtils.SaveConfig("HMPORT", Global.strHMPORT);
            CFGUtils.SaveConfig("WSWPATH", Global.strWSWPATH);
            CFGUtils.SaveConfig("GZZPATH", Global.strGZZPATH);
            CFGUtils.SaveConfig("HOLECOUNT", string.Empty + Global.deviceHole.HoleCount);
            CFGUtils.SaveConfig("SXTCOUNT", string.Empty + Global.deviceHole.SxtCount);
            CFGUtils.SaveConfig("HMCOUNT", string.Empty + Global.deviceHole.HmCount);
            CFGUtils.SaveConfig("SERVERADDR", Global.strSERVERADDR);
            //CFGUtils.SaveConfig("SERVERADDR", textBoxServerAddr.Text);
            CFGUtils.SaveConfig("REGISTERID", Global.REGISTERID);//"cgqcheck");
            CFGUtils.SaveConfig("REGISTERPASSWORD", Global.REGISTERPASSWORD);//"888888");
            CFGUtils.SaveConfig("CHECKPOINTID", Global.CHECKPOINTID);//"001002001002");
            CFGUtils.SaveConfig("CHECKPOINTNAME", Global.CHECKPOINTNAME);// "城关区食品药品监督管理局检测中心");
            CFGUtils.SaveConfig("CHECKPOINTTYPE", Global.CHECKPOINTTYPE);// "检测中心");
            CFGUtils.SaveConfig("ORGANIZATION", Global.ORGANIZATION);
            #region
            // 如果读取项目出错，然后退出程序的时候，又保存，那么这些项目就会没掉。
            //Global.SerializeToFile( Global.deviceHole, Global.deviceHoleFile);
            //Global.SerializeToFile( Global.userAccounts, Global.userAccountsFile);
            //Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
            //Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
            //Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            //Global.SerializeToFile(Global.hmItems, Global.hmItemsFile);
            #endregion
            Global.updateThread.Stop();
            Global.workThread.Stop();
            Global.printThread.Stop();
        }

    }
}
