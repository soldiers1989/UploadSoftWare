using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSetModel.DataModel
{
    public class clsReportGSDetail
    {
        public int ID { get; set; }
        /// <summary>
        /// 报表ID
        /// </summary>
        public int ReportGSID { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 标准值
        /// </summary>
        public string InspectionStandard { get; set; }
        /// <summary>
        /// 实测值
        /// </summary>
        public string IndividualResults { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string IndividualDecision { get; set; }
    }
}
