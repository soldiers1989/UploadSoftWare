using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryManage
{
    public static class Global
    {

        public static WorkThread workThread = null;

        public static string GetConfig(string key, string defaultValue)
        {
            string value = string.Empty;
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = Application.StartupPath + @"\DY-Detector.exe.config";
                FileUtils.Log(string.Format("config路径：{0}", map.ExeConfigFilename));
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                value = config.AppSettings.Settings[key].Value;
            }
            catch (Exception)
            {
                value = string.Empty;
            }
            return value;
        }

        public static void WaitMs(long ms)
        {
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > ms)
                    break;
            }
        }

    }
}