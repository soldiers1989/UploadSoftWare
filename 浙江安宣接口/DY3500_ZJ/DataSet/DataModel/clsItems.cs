using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsItems
    {
        public string resultCode { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
        public string message { get; set; }
        public string type { get; set; }

        private List<Datalist> _datalist;

        public List<Datalist> datalist
        {
            get { return _datalist; }
            set { _datalist = value; }
        }

        public class Datalist
        {
            public string itemname { get; set; }
            public string itemcode { get; set; }
        }
    }
}
