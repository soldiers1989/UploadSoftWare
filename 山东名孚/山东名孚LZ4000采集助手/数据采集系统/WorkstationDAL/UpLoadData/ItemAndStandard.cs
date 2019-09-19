using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    public class ItemAndStandard
    {
        /// <summary>
        /// 平台表主键
        /// </summary>
        public string checkId { get; set; }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string sampleName { get; set; }

        /// <summary>
        /// 样品编号
        /// </summary>
        public string sampleNum { get; set; }

        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        public string standardName { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string standardValue { get; set; }

        /// <summary>
        /// 判定方向
        /// </summary>
        public string checkSign { get; set; }

        /// <summary>
        /// 检测值单位
        /// </summary>
        public string checkValueUnit { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string uDate { get; set; }
    }
}
