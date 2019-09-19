using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsLAUpData
    {
        public object result { set; get; }
    }
    public class resultdetail
    {
        public string sampleNO { set; get; }
        public object checkResultList { set; get; }
    }
    public class ChkRList
    {
        public string sysCode { set; get; }
        public string checkItem { set; get; }
        public string checkResult { set; get; }
        public string limitValue { set; get; }
        public string checkMethod { set; get; }
        public string checkConclusion { set; get; }
        public string checkDate { set; get; }
        public string checkUser { set; get; }
        
    }
}
