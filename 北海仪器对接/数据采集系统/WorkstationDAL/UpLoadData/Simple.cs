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
}
