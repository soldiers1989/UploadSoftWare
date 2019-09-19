using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkstationDAL.yc;

namespace WorkstationModel.UpData
{
    public class JsonsItem
    {
        public static List<Items> anaylise(string items)
        {
             List<Items> item= JsonConvert.DeserializeObject<List<Items>>(items);
             return item;
        }
        public static List<samples> analyseSample(string items)
        {
            List<samples> item=JsonConvert.DeserializeObject<List<samples>>(items);
            return item;
        }
        public static List<marketInfo> analyseMarket(string markets)
        {
            List<marketInfo> mk = JsonConvert.DeserializeObject<List<marketInfo>>(markets);
            return mk;
        }
        public static List<Operates> analyseOperator(string ops)
        {
            List<Operates> resultm = JsonConvert.DeserializeObject<List<Operates>>(ops);
            return resultm;
        }
    }
}
