using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSetModel.DataModel
{
    public class clsReport
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 检测单位
        /// </summary>
        public string CheckUnitName { get; set; }

        /// <summary>
        /// 商标
        /// </summary>
        public string Trademark { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specifications { get; set; }

        /// <summary>
        /// 生产日期/批号
        /// </summary>
        public string ProductionDate { get; set; }

        /// <summary>
        /// 样品质量等级
        /// </summary>
        public string QualityGrade { get; set; }

        /// <summary>
        /// 被抽样单位名称
        /// </summary>
        public string CheckedCompanyName { get; set; }

        /// <summary>
        /// 被抽样单位联系电话
        /// </summary>
        public string CheckedCompanyPhone { get; set; }

        /// <summary>
        /// 生产单位名称
        /// </summary>
        public string ProductionUnitsName { get; set; }

        /// <summary>
        /// 生产单位联系电话
        /// </summary>
        public string ProductionUnitsPhone { get; set; }

        /// <summary>
        /// 任务来源
        /// </summary>
        public string TaskSource { get; set; }

        /// <summary>
        /// 检验依据
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// 抽样日期
        /// </summary>
        public string SamplingData { get; set; }

        /// <summary>
        /// 样品数量
        /// </summary>
        public string SampleNum { get; set; }

        /// <summary>
        /// 抽样单编号
        /// </summary>
        public string SamplingCode { get; set; }

        /// <summary>
        /// 样品到达日期
        /// </summary>
        public string SampleArrivalData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateData { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

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
}
