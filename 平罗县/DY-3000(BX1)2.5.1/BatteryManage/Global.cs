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
        /// <summary>
        /// 定义一个串口
        /// </summary>
        public static ComPort _port=new ComPort();

        public static WorkThread workThread = null;
        private static object configs = new object();
        public static string GetConfig(string key, string defaultValue)
        {
            lock(configs)
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
                catch (Exception ex)
                {
                    value = string.Empty;
                    FileUtils.Log(string.Format("读写配置文件：{0}", ex.Message ));
                }
                return value;
            }
            
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