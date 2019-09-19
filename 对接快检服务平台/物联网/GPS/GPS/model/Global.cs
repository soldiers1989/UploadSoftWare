using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GPS.model
{
    public  class Global
    {
        public static string FactoryNumber = "DY3500I_1234";//出厂编号
        public static string ServerAddr = "";//服务器地址
        public static string SoftwareVer = "";//软件版本
        public static string HardwareVer = "";//硬件版本
        public static int times = 0;//定时时间
        public static string lat = "";//精度
        public static string lon = "";//纬度
        public static string GPSAddr = "";
     /// <summary>
     /// 获取经纬度坐标
     /// </summary>
     /// <returns></returns>
        public static string getFilePath()
        {
            //文件路径  C:\Users\Administrator\AppData\Local\Packages\5544df9e-0776-456d-800f-dd17165fa41e_75cr2b68sm664\LocalState
            //string filePath = @"C:\Users\Administrator\AppData\Local\Packages\e13910e5-bd9b-48f2-9af9-c05d9aa6d867_75cr2b68sm664\LocalState\sample.txt";
            //string filePath = @"C:\Users\Administrator\AppData\Local\Packages\5544df9e-0776-456d-800f-dd17165fa41e_75cr2b68sm664\LocalState\sample.txt";
            string filePath = Global.GPSAddr;
            try
            {
                if (File.Exists(filePath))
                {
                    string data = File.ReadAllText(filePath);
                    string[] lt = data.Split(',');
                    if (lt != null)
                    {
                        Global.lon = lt[0];
                        Global.lat = lt[1];
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return filePath;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
        /// <summary>
        /// 检查网络是否可以连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }

    }
}
