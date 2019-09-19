using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public class clsDownItem
    {
       
        /// <summary>
        /// 返回数据编码
        /// </summary>
        public string code { get; set; }     
        /// <summary>
        /// 返回描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public object result { get; set; }    
        /// <summary>
        /// 返回状态
        /// </summary>
        public string status { get; set; }
    }
    public class itemdata
    {
        //数组  CKItem可以随便定义
        public CKItem[] data { get; set; }
       
    }
    public class CKItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pid { get; set; }
        public string hierarchy { get; set; }
    }
}
