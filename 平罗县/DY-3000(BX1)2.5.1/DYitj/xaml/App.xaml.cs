﻿using System;
using System.IO;
using System.Windows;
using AIO.src;
using com.lvrenyang;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private string logType = "app-error";
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
            CFGUtils.createConfig("PrintQrCode", "1");
            CFGUtils.createConfig("AddColum", "1");
            CFGUtils.createConfig("Old3500I", "1");
            CFGUtils.createConfig("EACHDISTRICT","");
            //CFGUtils.createConfig("ContrastTime", "");
            CFGUtils.createConfig("NoOutCard", "0");

            Global.strADPORT = CFGUtils.GetConfig("ADPORT", "COM7");
            Global.strSXT1PORT = CFGUtils.GetConfig("SXT1PORT", "COM8");
            Global.strSXT2PORT = CFGUtils.GetConfig("SXT2PORT", "COM9");
            Global.strSXT3PORT = CFGUtils.GetConfig("SXT3PORT", "COM12");
            Global.strSXT4PORT = CFGUtils.GetConfig("SXT4PORT", "COM13");
            Global.strPRINTPORT = CFGUtils.GetConfig("PRINTPORT", "COM10");
            Global.strHMPORT = CFGUtils.GetConfig("HMPORT", "COM11");
            Global.strWSWPATH = CFGUtils.GetConfig("WSWPATH", "C:\\");
            Global.strGZZPATH = CFGUtils.GetConfig("GZZPATH", "C:\\");
            Global.updateServer = new UpdateServer();
            Global.updateServer.ServerAddr = CFGUtils.GetConfig("SERVERADDR", "http://127.0.0.1:8080/web/services/DataSyncService");
            Global.updateServer.RegisterID = CFGUtils.GetConfig("REGISTERID", "");
            Global.updateServer.RegisterPassword = CFGUtils.GetConfig("REGISTERPASSWORD", "");
            Global.updateServer.CheckPointID = CFGUtils.GetConfig("CHECKPOINTID", "");
            Global.updateServer.CheckPointName = CFGUtils.GetConfig("CHECKPOINTNAME", "");
            Global.updateServer.CheckPointType = CFGUtils.GetConfig("CHECKPOINTTYPE", "");
            Global.updateServer.Organization = CFGUtils.GetConfig("ORGANIZATION", "");
            Global.Version = CFGUtils.GetConfig("Version", "XZ");
            Global.IsTest = CFGUtils.GetConfig("TEST", "0").Equals("1") ? true : false;
            Global.VideoAddress = Environment.CurrentDirectory + "\\" + CFGUtils.GetConfig("VideoAddress", "Video");
            Global.set_FaultDetection = CFGUtils.GetConfig("FAULTDETECTION", "0").Equals("1") ? true : false;
            Global.set_ShowFgd = CFGUtils.GetConfig("SHOWFGD", "0").Equals("1") ? true : false;
            Global.EnableVideo = CFGUtils.GetConfig("EnableVideo", "0").Equals("1") ? true : false;
            Global.EnableBattery = CFGUtils.GetConfig("EnableBattery", "0").Equals("1") ? true : false;
            Global.IsSetIndex = CFGUtils.GetConfig("IsSettingIndex", "0").Equals("1") ? true : false;
            Global.IsEnableWswOrAtp = CFGUtils.GetConfig("EnableWswOrAtp", "0").Equals("1") ? true : false;
            Global.IsEnableBcsp = CFGUtils.GetConfig("EnableBcsp", "0").Equals("1") ? true : false;
            Global.IsEnableYgmy = CFGUtils.GetConfig("EnableYgmy", "0").Equals("1") ? true : false;
            Global.IsEnableSyxwsw = CFGUtils.GetConfig("EnableSyxwsw", "0").Equals("1") ? true : false;
            Global.IsWswOrAtp = CFGUtils.GetConfig("IsWswOrAtp", "WSW");
            Global.MicrobialAddress = CFGUtils.GetConfig("MICROBIALADDRESS", "");
            Global.IsDELETED = CFGUtils.GetConfig("IsDeleted", "0").Equals("1") ? true : false;
            Global.PdfAddress = CFGUtils.GetConfig("PdfAddress", "");
            Global.PrintType = CFGUtils.GetConfig("PrintType", "");
            Global.IsUpdateChekcedValue = CFGUtils.GetConfig("UpdateCheckedValue", "0").Equals("1") ? true : false;
            Global.IsOpenFile = CFGUtils.GetConfig("IsOpenFile", "0").Equals("1") ? true : false;
            Global.InstrumentName = CFGUtils.GetConfig("InstrumentName", "便携式食品综合分析仪");
            Global.InstrumentNameModel = CFGUtils.GetConfig("InstrumentNameModel", "DY-3000(BX1)");
            Global.IsShowValue = CFGUtils.GetConfig("IsShowValue", "0").Equals("1") ? true : false;
            Global.InterfaceType = CFGUtils.GetConfig("InterfaceType", "");
            Global.EnableKnowledgeBase = CFGUtils.GetConfig("EnableKnowledgeBase", "0").Equals("1") ? true : false;
            Global.EnableChapter = CFGUtils.GetConfig("EnableChapter", "0").Equals("1") ? true : false;
            Global.EnableValidCode = CFGUtils.GetConfig("EnableValidCode", "0").Equals("1") ? true : false;
            Global.EnableGPS = CFGUtils.GetConfig("EnableGPS", "0").Equals("1") ? true : false;
            Global.PrintQrCode = CFGUtils.GetConfig("PrintQrCode", "0").Equals("1") ? true : false;
            Global.Mac = CFGUtils.GetConfig("Mac", "");
            Global.DeviceID = CFGUtils.GetConfig("DeviceId", "");
            Global.JtjVersion = int.Parse(CFGUtils.GetConfig("JtjVersion", "1"));
            //是否为DY3500(I)
            Global.Is3500I =CFGUtils.GetConfig("Old3500I", "0").Equals("1") ? true : false;
            Global.EachDistrict = CFGUtils.GetConfig("EACHDISTRICT", string.Empty);
            //摄像头新模块不用进出卡
            Global.NoOutCard = CFGUtils.GetConfig("NoOutCard", "0").Equals("1") ? true : false;
            //上一次的对照时间
            //Global.ContrastTime = CFGUtils.GetConfig("ContrastTime", string.Empty);
            //安徽省项目相关
            Global.AnHuiInterface.userName = CFGUtils.GetConfig("ah_userName", "5009101");
            Global.AnHuiInterface.passWord = CFGUtils.GetConfig("ah_passWord", "F9F67D794225FDD2379736C4807459DD");
            Global.AnHuiInterface.interfaceVersion = CFGUtils.GetConfig("ah_interfaceVersion", "1.0");
            Global.AnHuiInterface.instrument = CFGUtils.GetConfig("ah_instrument", "DY-3000(BX1)");
            Global.AnHuiInterface.instrumentNo = CFGUtils.GetConfig("ah_instrumentNo", "");
            Global.AnHuiInterface.ServerAddr = CFGUtils.GetConfig("ah_ServerAddr", "http://36.7.107.82:5680/kjsys_new/webservice/instrumentDockingServiceProviderService?wsdl");

            //快检服务相关
            Global.KjServer.KjServerAddr = CFGUtils.GetConfig("KjServerAddr", "");
            Global.KjServer.Kjuser_name = CFGUtils.GetConfig("Kjuser_name", "");
            Global.KjServer.Kjpassword = CFGUtils.GetConfig("Kjpassword", "");
            Global.KjServer.Kjrealname = CFGUtils.GetConfig("Kjrealname", "");
            Global.KjServer.Kjd_depart_name = CFGUtils.GetConfig("Kjd_depart_name", "");
            Global.KjServer.Kjp_point_name = CFGUtils.GetConfig("Kjp_point_name", "");
            Global.KjServer.Kjseries = Global.InstrumentNameModel;

            //广东省智慧平台相关
            Wisdom.GETSAMPLE_URL = CFGUtils.GetConfig("getsampleURL", string.Empty);
            //Wisdom.UPLOADSAMPLE_URL = CFGUtils.GetConfig("uploadSampleURL", string.Empty);
            Wisdom.UPLOADRESULT_URL = CFGUtils.GetConfig("uploadResultURL", string.Empty);
            //Wisdom.DOWNLOADRESULT_URL = CFGUtils.GetConfig("downloadResultURL", string.Empty);
            Wisdom.DEVICESTATUS_URL = CFGUtils.GetConfig("deviceStatusURL", string.Empty);
            Wisdom.USER = CFGUtils.GetConfig("USER", string.Empty);
            Wisdom.PASSWORD = CFGUtils.GetConfig("PASSWORD", string.Empty);
            Wisdom.gpsAddress = CFGUtils.GetConfig("GPSADDRES", string.Empty);
            Wisdom.gpsJD = CFGUtils.GetConfig("GPSJD", string.Empty);
            Wisdom.gpsWD = CFGUtils.GetConfig("GPSWD", string.Empty);
            Global.IsSelectSampleName = CFGUtils.GetConfig("IsSelectSampleName", "0").Equals("0") ? false : true;

            //Global.STANDARD1
            // 读取配置文件，有多少个检测孔，这个是第一个需要确定的。Global.deviceHoleFile
            //F:\\Projects\\SVN\\DY-3500\\DYitj\\bin\\Debug\\Others\\deviceHole.dat
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
                FileUtils.OprLog(6, logType, ex.ToString());
            }
            Global.DeSerializeFromFile(out Global.userAccounts, Global.userAccountsFile);
            Global.DeSerializeFromFile(out Global.fgdItems, Global.fgdItemsFile);
            Global.DeSerializeFromFile(out Global.gszItems, Global.gszItemsFile);
            Global.DeSerializeFromFile(out Global.jtjItems, Global.jtjItemsFile);
            Global.DeSerializeFromFile(out Global.bcspItems, Global.bcspItemsFile);
            Global.DeSerializeFromFile(out Global.hmItems, Global.hmItemsFile);
            Global.DeSerializeFromFile(out Global.samplenameadapter, Global.samplenameadapterFile);
            Global.DeSerializeFromFile(out Global.adapteritem, Global.adapteritemFile);
            Global.DeSerializeFromFile(out Global.displayItems, Global.displayItemsFile);
            //打印
            Global.DeSerializeFromFile(out Global.Printing, Global.PrintViewFile);
            ////杀掉电池应用
            Process[] ps = Process.GetProcesses();
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].ProcessName.StartsWith("BatteryManage"))
                {
                    ps[i].Kill();
                    break;
                }
            }

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
            CFGUtils.SaveConfig("HOLECOUNT", "" + Global.deviceHole.HoleCount);
            CFGUtils.SaveConfig("SXTCOUNT", "" + Global.deviceHole.SxtCount);
            CFGUtils.SaveConfig("HMCOUNT", "" + Global.deviceHole.HmCount);
            CFGUtils.SaveConfig("SERVERADDR", Global.strSERVERADDR);
            CFGUtils.SaveConfig("REGISTERID", Global.REGISTERID);//"cgqcheck");
            CFGUtils.SaveConfig("REGISTERPASSWORD", Global.REGISTERPASSWORD);//"888888");
            CFGUtils.SaveConfig("CHECKPOINTID", Global.CHECKPOINTID);//"001002001002");
            CFGUtils.SaveConfig("CHECKPOINTNAME", Global.CHECKPOINTNAME);// "城关区食品药品监督管理局检测中心");
            CFGUtils.SaveConfig("CHECKPOINTTYPE", Global.CHECKPOINTTYPE);// "检测中心");
            CFGUtils.SaveConfig("ORGANIZATION", Global.ORGANIZATION);
            Global.updateThread.Stop();
            Global.workThread.Stop();
            Global.printThread.Stop();
        }

    }
}