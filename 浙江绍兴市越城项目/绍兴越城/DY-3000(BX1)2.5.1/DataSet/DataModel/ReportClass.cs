using System.Collections.Generic;

namespace DYSeriesDataSet.DataModel
{
    public class ReportClass
    {
        /// <summary>
        /// 检测单位
        /// </summary>
        public string r_CheckUnitName { get; set; }
        ///// <summary>
        ///// 食品名称
        ///// </summary>
        //public string r_FoodName { get; set; }
        /// <summary>
        /// 商标
        /// </summary>
        public string r_Trademark { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string r_Specifications { get; set; }
        /// <summary>
        /// 生产日期/批号
        /// </summary>
        public string r_ProductionDate { get; set; }
        /// <summary>
        /// 样品质量等级
        /// </summary>
        public string r_QualityGrade { get; set; }
        /// <summary>
        /// 被抽样单位名称
        /// </summary>
        public string r_CheckedCompanyName { get; set; }
        /// <summary>
        /// 被抽样单位联系电话
        /// </summary>
        public string r_CheckedCompanyPhone { get; set; }
        /// <summary>
        /// 生产单位名称
        /// </summary>
        public string r_ProductionUnitsName { get; set; }
        /// <summary>
        /// 生产单位联系电话
        /// </summary>
        public string r_ProductionUnitsPhone { get; set; }
        /// <summary>
        /// 任务来源
        /// </summary>
        public string r_TaskSource { get; set; }
        /// <summary>
        /// 检验依据
        /// </summary>
        public string r_Standard { get; set; }
        /// <summary>
        /// 抽样日期
        /// </summary>
        public string r_SamplingData { get; set; }
        /// <summary>
        /// 样品数量
        /// </summary>
        public string r_SampleNum { get; set; }
        /// <summary>
        /// 抽样单编号
        /// </summary>
        public string r_SamplingCode { get; set; }
        /// <summary>
        /// 样品到达日期
        /// </summary>
        public string r_SampleArrivalData { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string r_Note { get; set; }
        /// <summary>
        /// 报表标题
        /// </summary>
        public string r_Title { get; set; }
        /// <summary>
        /// 检测详细列表
        /// </summary>
        public List<ReportDetail> r_reportList 
        {
            get 
            {
                return reportDetail;
            }
        }

        public List<ReportDetail> reportDetail = new List<ReportDetail>();
        public class ReportDetail 
        {
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
        }
    }
}
