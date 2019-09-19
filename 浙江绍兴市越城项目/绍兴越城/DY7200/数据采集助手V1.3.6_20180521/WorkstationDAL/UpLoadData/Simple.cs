using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    public class Simple
    {
        /// <summary>
        /// 系统生成唯一编号或表主键
        /// </summary>
        public string foodId { get; set; }

        /// <summary>
        /// 样品id或编号
        /// </summary>
        public string foodCode { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string foodName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string uDate { get; set; }
    }
    public class sampletype
    {
        public List<foodtype> food { get; set; }
    }
    public class foodtype
    {
        public string id { get; set; }
        public string food_name { get; set; }
        public string food_name_en { get; set; }
        public string food_name_other { get; set; }
        public string parent_id { get; set; }
        public string cimonitor_level { get; set; }
        public string sorting { get; set; }
        public string @checked { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string isFood { get; set; }

    }
    public class fooditemlist
    {
        public List<fooditem> foodItem { get; set; }
    }
    public class fooditem
    {
        public string id { get; set; }
        public string food_id { get; set; }
        public string food_id1 { get; set; }
        public string item_id { get; set; }
        public string detect_sign { get; set; }
        public string detect_value { get; set; }
        public string detect_value_unit { get; set; }
        public string remark { get; set; }
        public string use_default { get; set; }
        public string @checked { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }

    }
}
