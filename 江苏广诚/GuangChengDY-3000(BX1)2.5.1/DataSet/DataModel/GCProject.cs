using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class GCProject
    {
        private int _ID;
        public int  ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _did = "";
        public string did
        {
            get 
            {
                return _did;
            }
            set
            {
                _did = value;
            }
        }
        private string _jgmc = "";
        public string jgmc
        {
            get
            {
                return _jgmc;
            }
            set
            {
                _jgmc = value;
            }
        }
    }

    public class projectInfo
    {
        public projectdata jigougl { get; set; }
    }
    public class projectdata
    {
        public string id { set; get; }
        public string jgmc { set; get; }
    }

}
