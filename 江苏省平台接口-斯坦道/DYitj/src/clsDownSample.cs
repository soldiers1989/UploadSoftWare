using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public class clsDownSample
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
    public class Sampledata
    {
        //数组  CKItem可以随便定义
        public CKSample[] data { get; set; }
    }
    public class CKSample
    {
        public string id { get; set; }
        public string name { get; set; }
        public string typeLevel { get; set; }
        public string typeLevelName { get; set; }
        public string hierarchy { get; set; }
    }
}
