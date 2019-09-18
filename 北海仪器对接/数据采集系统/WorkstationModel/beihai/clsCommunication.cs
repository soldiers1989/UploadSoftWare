using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationModel.beihai
{
    /// <summary>
    /// 通信返回解释
    /// </summary>
    public  class clsCommunication
    {
        public string status { get; set; }
        public string username { get; set; }
        public string unit { get; set; }
        public string unitname { get; set; }
    }
    public class BeiHaiUploadData
    {
        public string productId { get; set; }
        public string goodsName { get; set; }
        public string testItem { get; set; }
        public string testOrganization { get; set; }
        public string checkPerson { get; set; }
        public string checkValue { get; set; }
        public string standardValue { get; set; }
        public string result { get; set; }
        public string checkTime { get; set; }
    }
    public class Beihaisamples
    {
        public List<tsampling> samplings { get; set; }
    }
    public class tsampling
    {
        public string productId { get; set; }
        public string goodsName { get; set; }
        public string operateId { get; set; }
        public string operateName { get; set; }
        public string marketId { get; set; }
        public string marketName { get; set; }
        public string samplingPerson { get; set; }
        public string samplingTime { get; set; }
        public string positionAddress { get; set; }
        public string goodsId { get; set; }
        public string IsTest { get; set; }
    }
  

}
