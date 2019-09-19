using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public  class Result
    {
        public Framsds FarmsDS { get; set; }
    }
    public class Framsds
    {
        public string UserName { get; set; }
        public string UserPassWord { get; set; }

        public object Farms { get; set; }
       
    }
    public class Farmsdata
    {
        public string shebei_code { get; set; }

        public string SampleCode { get; set; }

        public string SampleName { get; set; }
        public string BoothCode { get; set; }
        public string DetectionValue { get; set; }
        public string DetectionName { get; set; }
        public string BoothNum { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public string Area { get; set; }

        public string LabCode { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Range { get; set; }
        public string DetectionDate { get; set; }
        public string BoothName { get; set; }
        public string Manger { get; set; }
        public string Phone { get; set; }
        public string Stations { get; set; }
        public string holeNumber { get; set; }
        
    }

}
