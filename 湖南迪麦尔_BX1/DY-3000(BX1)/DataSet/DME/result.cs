using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DME
{
    public  class result
    {
        public string userToken { set; get; }
        public string body { set; get; }
    }
    public class Bodys
    {
        public string samplename { set; get; }
        public string controlvalue { set; get; }
        public string testproject { set; get; }
        public string testingtime { set; get; }
        public string testresult { set; get; }
        public string inspector { set; get; }
        public string samplenum { set; get; }
        public string stand_num { set; get; }
        public string deviceNumber { set; get; }
        public string decisionoutcome { set; get; }
        public string cov_unit { set; get; }

        public string site { set; get; }
      
    }

    public class returnList
    {
        public string code { set; get; }
        public string ok { set; get; }
        public string msg { set; get; }
        public object body { set; get; }
        public object extra { set; get; }
        
    }

    public class returnBody
    {
        public string token { set; get; }
        public object user { set; get; }
    }
    public class users
    {
        public string id { set; get; }
        public string username { set; get; }
        public string enabled { set; get; }
        public string nickname { set; get; }
        public string mobile { set; get; }
        public string organId { set; get; }
        public string organCode { set; get; }
        public string organName { set; get; }
    }
}
