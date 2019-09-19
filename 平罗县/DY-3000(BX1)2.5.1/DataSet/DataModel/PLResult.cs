using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class PLResult
    {
        private string _code = "";
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }
        private string _msg = "";
        public string msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }
        private string _time = "";
        public string time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }
        private object  _data = "";
        public object data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
    public class PLData
    {
        private string _id = "";
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }

        }

        private string _category_id = "";
        public string category_id
        {
            get
            {
                return _category_id;
            }
            set
            {
                _category_id = value;
            }
        }
        private string _name = "";
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }

    public class PLSample
    {
        private int _ID = -1;
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

        private string _idPL = "";
        public string idPL
        {
            get
            {
                return _idPL;
            }
            set
            {
                _idPL = value;
            }

        }

        private string _category_id = "";
        public string category_id
        {
            get
            {
                return _category_id;
            }
            set
            {
                _category_id = value;
            }
        }
        public string category
        {
            get
            {
                string rtn = "";
                if (_category_id=="2")
                {
                    rtn = "蔬菜";
                }
                else if (_category_id == "3")
                {
                    rtn = "水果";
                }
                else
                {
                    rtn = "食用农产品";
                }
                return rtn;
            }
            set
            {
                _category_id = value;
            }
        }
        private string _samplename = "";
        public string samplename
        {
            get
            {
                return _samplename;
            }
            set
            {
                _samplename = value;
            }
        }
    }
}
