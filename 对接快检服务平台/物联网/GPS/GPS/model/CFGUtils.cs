using System.Configuration;
using System.Linq;

namespace GPS.model
{
    public class CFGUtils
    {
        /// <summary>
        /// 创建配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void createConfig(string key, string value)
        {
            Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!cf.AppSettings.Settings.AllKeys.Contains(key))
            {
                cf.AppSettings.Settings.Add(key, value);
                cf.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        public static void SaveConfig(string key, string value)
        {
            // 修改配置文件
            Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!cf.AppSettings.Settings.AllKeys.Contains(key))
            {
                cf.AppSettings.Settings.Add(key, value);
            }
            else
            {
                cf.AppSettings.Settings[key].Value = value;
            }
            cf.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetConfig(string key, string defaultValue)
        {
            string value;
            Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!cf.AppSettings.Settings.AllKeys.Contains(key))
            {
                value = defaultValue;
            }
            else
            {
                value = cf.AppSettings.Settings[key].Value.Length == 0 ? defaultValue : cf.AppSettings.Settings[key].Value;
            }
            return value;
        }
    }
}
