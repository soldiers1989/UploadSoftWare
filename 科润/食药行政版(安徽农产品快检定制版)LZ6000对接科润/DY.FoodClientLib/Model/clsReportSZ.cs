
using System.Collections.Generic;
namespace DY.FoodClientLib.Model
{
    /// <summary>
    /// 深圳报表model
    /// 2016年1月14日
    /// wenj
    /// </summary>
    public class clsReportSZ
    {
        public clsReportSZ() { }

        private int _ID = 0;
        private string _ReportName = "";
        private string _CheckedCompany = "";
        private string _Contact = "";
        private string _ContactPhone = "";
        private string _CheckedCompanyArea = "";
        private string _SamplingData = "";
        private string _SamplingPerson = "";
        private string _CheckUser = "";

        public List<ExportReport> exportList
        {
            get
            {
                return ExportReportList;
            }
        }
        /// <summary>
        /// 深圳 导出实体类
        /// </summary>
        public List<ExportReport> ExportReportList = new List<ExportReport>();

        public class ExportReport 
        {
            /// <summary>
            /// 序号
            /// </summary>
            public string Number { get; set; }
            /// <summary>
            /// 编号
            /// </summary>
            public string SysCode { get; set; }
            /// <summary>
            ///  被检单位
            /// </summary>
            public string CheckedCompany { get; set; }
            /// <summary>
            ///  样品名称
            /// </summary>
            public string SampleName { get; set; }
            /// <summary>
            ///  抽样基数
            /// </summary>
            public string SampleBase { get; set; }
            /// <summary>
            ///  样品来源
            /// </summary>
            public string SampleSource { get; set; }
            /// <summary>
            ///  是否主动销毁
            /// </summary>
            public string IsDestruction { get; set; }
            /// <summary>
            ///  销毁重量(KG)
            /// </summary>
            public string DestructionKG { get; set; }
            /// <summary>
            ///  抽样金额
            /// </summary>
            public string SampleAmount { get; set; }
            /// <summary>
            ///  数量
            /// </summary>
            public string SampleNumber { get; set; }
            /// <summary>
            ///  价格
            /// </summary>
            public string Price { get; set; }
            /// <summary>
            ///  实测值
            /// </summary>
            public string ChekcedValue { get; set; }
            /// <summary>
            ///  标准值
            /// </summary>
            public string StandardValue { get; set; }
            /// <summary>
            ///  检测结果
            /// </summary>
            public string Result { get; set; }
            /// <summary>
            ///  备注
            /// </summary>
            public string Notes { get; set; }
        }

        public List<clsReportDetailSZ> reportList
        {
            get
            {
                return reportDetailList;
            }
        }

        public List<clsReportDetailSZ> reportDetailList = new List<clsReportDetailSZ>();

        public class clsReportDetailSZ
        {
            public int ID { get; set; }

            public int ReportID { get; set; }
            /// <summary>
            /// 编号
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 样品名称
            /// </summary>
            public string SampleName { get; set; }
            /// <summary>
            /// 抽样基数
            /// </summary>
            public string SampleBase { get; set; }
            /// <summary>
            /// 样品来源
            /// </summary>
            public string SampleSource { get; set; }
            /// <summary>
            /// 结论
            /// </summary>
            public string Result { get; set; }
            /// <summary>
            /// 系统编码
            /// </summary>
            public string SysCode { get; set; }
            /// <summary>
            /// 抽样金额
            /// </summary>
            public string SampleAmount { get; set; }
            /// <summary>
            /// 抽样数量
            /// </summary>
            public string SampleNumber { get; set; }
            /// <summary>
            /// 价格
            /// </summary>
            public string Price { get; set; }
            /// <summary>
            /// 实测值
            /// </summary>
            public string ChekcedValue { get; set; }
            /// <summary>
            /// 标准值
            /// </summary>
            public string StandardValue { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Note { get; set; }
            /// <summary>
            /// 是否主动销毁
            /// </summary>
            public string IsDestruction { get; set; }
            /// <summary>
            /// 销毁重量
            /// </summary>
            public string DestructionKG { get; set; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName 
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }

        /// <summary>
        /// 受检人
        /// </summary>
        public string CheckedCompany
        {
            get { return _CheckedCompany; }
            set { _CheckedCompany = value; }
        }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }

        /// <summary>
        /// 行政区域
        /// </summary>
        public string CheckedCompanyArea
        {
            get { return _CheckedCompanyArea; }
            set { _CheckedCompanyArea = value; }
        }

        /// <summary>
        /// 抽样日期
        /// </summary>
        public string SamplingData
        {
            get { return _SamplingData; }
            set { _SamplingData = value; }
        }

        /// <summary>
        /// 抽样人
        /// </summary>
        public string SamplingPerson
        {
            get { return _SamplingPerson; }
            set { _SamplingPerson = value; }
        }

        /// <summary>
        /// 检测人
        /// </summary>
        public string CheckUser
        {
            get { return _CheckUser; }
            set { _CheckUser = value; }
        }

    }
}
