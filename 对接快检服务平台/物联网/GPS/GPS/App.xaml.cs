using GPS.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace GPS
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Global.FactoryNumber = CFGUtils.GetConfig("factoryNo", "");
            Global.ServerAddr  = CFGUtils.GetConfig("ServerAddr", "");
            Global.SoftwareVer  = CFGUtils.GetConfig("SoftwareVer", "");
            Global.HardwareVer  = CFGUtils.GetConfig("HardwareVer", "");
            Global.times =int.Parse ( CFGUtils.GetConfig("times", "0"));
            CFGUtils.createConfig("GPSAddr", @"C:\Users\Administrator\AppData\Local\Packages\5544df9e-0776-456d-800f-dd17165fa41e_75cr2b68sm664\LocalState\sample.txt");
            Global.GPSAddr = CFGUtils.GetConfig("GPSAddr", "");
        }
    }
}
