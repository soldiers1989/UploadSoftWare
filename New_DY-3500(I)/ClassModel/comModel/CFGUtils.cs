using System.Configuration;
using System.Linq;

namespace comModel
{
    public class CFGUtils
    {
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
                value = cf.AppSettings.Settings[key].Value;
            }
            return value;
        }
    }
}
