﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test.model
{
    public class Users
    {
        public string thirdCompanyName { get; set; }
        public string thirdCompanyCode { get; set; }
    }
    public class operate
    {
        public string marketCode { get; set; }
        public string thirdCompanyName { get; set; }
        public string thirdCompanyCode { get; set; }
    }
    public class updateDeviceInfo
    {    
        public string thirdCompanyName { get; set; }
        public string thirdCompanyCode { get; set; }
        public string Count { get; set; }
        public List<devices> device { get; set; }
    }
    public class devices
    {
        public string deviceId { get; set; }
        public string type { get; set; }
        public string factory { get; set; }
    }
    public class uploadData
    {
        public string thirdCompanyName { get; set; }
        public string thirdCompanyCode { get; set; }
        public string checkCount { get; set; }
    
        public List<checkInfo> checkInfo { get; set; }
      
    }
    public class checkInfo
    {
        public string uuid { get; set; }
        public string deviceId { get; set; }
        public string marketName { get; set; }
        public string operatorName { get; set; }
        public string operatorCode { get; set; }
        public string stallNo { get; set; }
        public string checkDate { get; set; }
        public string goodsName { get; set; }
        public string amount { get; set; }
        public string unite { get; set; }
        public string itemType { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string value { get; set; }
        public string checkResult { get; set; }
    }
}
