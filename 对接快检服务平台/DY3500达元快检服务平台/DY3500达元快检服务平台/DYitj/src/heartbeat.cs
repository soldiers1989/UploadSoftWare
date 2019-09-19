using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO
{
    public class heartbeat
    {
        public string status { set; get; }
        public string onlineDate { set; get; }
        public string softwareVersion { set; get; }
        public string handwareVersion { set; get; }
        public string offlineDate { set; get; }
        public string longitude { set; get; }
        public string latitude { set; get; }
    }
    public class resultdata
    {
        public string msg { set; get; }
        public string resultCode { set; get; }
        public string obj { set; get; }
       
    }
    public class HeatObj
    {
        public string id { set; get; }
        public string app_type { set; get; }
        public string app_name { set; get; }
        public string modular_version { set; get; }
        public string version { set; get; }
        public string force_update { set; get; }
        public string url_path { set; get; }
        public string description { set; get; }
        public string imp_date { set; get; }
        public string delete_flag { set; get; }
        public string file_size { set; get; }
        public string introduce { set; get; }
        public string update_content { set; get; }
        public string param1 { set; get; }
        public string param2 { set; get; }
        public string param3 { set; get; }
        
    }
}
