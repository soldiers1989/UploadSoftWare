using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    public class ZJResultDatas
    {
        public string resultCode { get; set; }
        public string message { get; set; }
        public ItemDetail[] datalist { get; set; }
        public class ItemDetail
        {
            public string itemname { get; set; }
            public string itemcode { get; set; }

            public string str()
            {
                return itemname + "," + itemcode;
            }
        }

    }
}
