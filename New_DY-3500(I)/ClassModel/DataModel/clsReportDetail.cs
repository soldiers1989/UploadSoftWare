using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSetModel.DataModel
{
    public class clsReportDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 报表ID
        /// </summary>
        public int ReportID { get; set; }

        /// <summary>
        /// 食品名称
        /// </summary>
        public string FoodName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 检测数据
        /// </summary>
        public string CheckData { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDeleted { get; set; }
    }
}
