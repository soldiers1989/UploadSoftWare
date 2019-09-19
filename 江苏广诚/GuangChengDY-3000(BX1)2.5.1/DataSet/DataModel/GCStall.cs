using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public  class GCStall
    {
        private int _ID;
        public int ID
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

        private string _sid = "";
        public string sid
        {
            get
            {
                return _sid;
            }
            set
            {
                _sid = value;
            }
        }
        private string _twhmc = "";
        public string twhmc
        {
            get
            {
                return _twhmc;
            }
            set
            {
                _twhmc = value;
            }
        }

        private string _companyID = "";
        public string companyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }
        private string _comany = "";
        public string comany
        {
            get
            {
                return _comany;
            }
            set
            {
                _comany = value;
            }
        }
    }
    public class StallInfo
    {
        public Stalldata booth { get; set; }
    }
    public class Stalldata
    {
        public string id { set; get; }
        public string twhmc { set; get; }
    }
}
