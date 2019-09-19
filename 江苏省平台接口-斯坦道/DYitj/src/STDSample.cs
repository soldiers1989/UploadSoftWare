using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public class STDSample
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
    public class STDfood
    {
        //数组  CKItem可以随便定义
        public CKsample[] data { get; set; }

    }
    public class CKsample
    {
        public string sampleNum { get; set; }
        public string stallNumber { get; set; }
        public string productName { get; set; }
        public string category { get; set; }
        public string enterpriseId { get; set; }
        public string foodTypeId { get; set; }
        public string isDetection { get; set; }
        public string enterpriseName { get; set; }
        public string createTime { get; set; }
    }
}
