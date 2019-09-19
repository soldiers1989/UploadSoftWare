using System.Collections.Generic;
namespace DYSeriesDataSet.DataModel
{
    public class clsReportGS
    {

        public int ID { get; set; }

        /// <summary>
        /// 报表名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 食品名称
        /// </summary>
        public string FoodName { get; set; }

        /// <summary>
        /// 食品类型
        /// </summary>
        public string FoodType { get; set; }

        /// <summary>
        /// 生产/加工/购进日期
        /// </summary>
        public string ProductionDate { get; set; }

        /// <summary>
        /// 被抽样单位名称
        /// </summary>
        public string CheckedCompanyName { get; set; }

        /// <summary>
        /// 被抽样单位地址
        /// </summary>
        public string CheckedCompanyAddress { get; set; }

        /// <summary>
        /// 被抽样单位联系电话
        /// </summary>
        public string CheckedCompanyPhone { get; set; }

        /// <summary>
        /// 产地、标称生产企业或供货单位名称
        /// </summary>
        public string LabelProducerName { get; set; }

        /// <summary>
        /// 产地、标称生产企业或供货单位地址
        /// </summary>
        public string LabelProducerAddress { get; set; }

        /// <summary>
        /// 产地、标称生产企业或供货单位电话
        /// </summary>
        public string LabelProducerPhone { get; set; }

        /// <summary>
        /// 抽样日期
        /// </summary>
        public string SamplingData { get; set; }

        /// <summary>
        /// 抽样人
        /// </summary>
        public string SamplingPerson { get; set; }

        /// <summary>
        /// 样品数量
        /// </summary>
        public string SampleNum { get; set; }

        /// <summary>
        /// 抽样基数
        /// </summary>
        public string SamplingBase { get; set; }

        /// <summary>
        /// 抽样地点
        /// </summary>
        public string SamplingAddress { get; set; }

        /// <summary>
        /// 抽样单编号
        /// </summary>
        public string SamplingOrderCode { get; set; }

        /// <summary>
        /// 检验依据
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// 检验结论
        /// </summary>
        public string InspectionConclusion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 审核
        /// </summary>
        public string Audit { get; set; }

        /// <summary>
        /// 检验人
        /// </summary>
        public string Surveyor { get; set; }

        /// <summary>
        /// 检测项目集合
        /// </summary>
        public string ItemsList { get; set; }

        /// <summary>
        /// 检测项目不合格集合
        /// </summary>
        public string UnqualifiedItemsList { get; set; }

        /// <summary>
        /// 样品集合
        /// </summary>
        public string SampleList { get; set; }

        public List<ReportDetail> reportList
        {
            get
            {
                return reportDetailList;
            }
        }
        public List<ReportDetail> reportDetailList = new List<ReportDetail>();
        public class ReportDetail
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
}