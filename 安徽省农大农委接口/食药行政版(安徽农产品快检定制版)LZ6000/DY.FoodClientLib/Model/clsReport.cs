using System.Collections.Generic;

namespace DY.FoodClientLib.Model
{
    public class clsReport
    {
        public clsReport() { }

        private string _ID = "";
        private string _CheckedCompany = "";
        //private string _CheckedCompanyName = "";
        private string _Address = "";
        private string _BusinessNature = "";
        private string _BusinessLicense = "";
        private string _Contact = "";
        private string _ContactPhone = "";
        private string _Fax = "";
        private string _ZipCode = "";
        private string _ProductName = "";
        private string _ProductPrice = "";
        private string _Specifications = "";
        private string _QualityGrade = "";
        private string _BatchNumber = "";
        private string _RegisteredTrademark = "";
        private string _SamplingNumber = "";
        private string _SamplingBase = "";
        private string _IntoNumber = "";
        private string _Implementation = "";
        private string _InventoryNubmer = "";
        private string _Notes = "";
        private string _SamplingData = "";
        private string _SamplingPerson = "";
        private string _SamplingCode = "";
        private string _NoteUnder = "";
        private string _ApprovedUser = "";
        private string _ReportName = "";
        private string _CreateDate = "";
        private string _Unit = "";

        public string ID 
        {
            get { return _ID; }
            set { _ID = value; }
        }
        /// <summary>
        /// 受检人/被检单位编号
        /// </summary>
        public string CheckedCompany
        {
            get { return _CheckedCompany; }
            set { _CheckedCompany = value; }
        }
        /// <summary>
        /// 受检人/被检单位名称
        /// </summary>
        //public string CheckedCompanyName
        //{
        //    get { return _CheckedCompanyName; }
        //    set { _CheckedCompanyName = value; }
        //}
        /// <summary>
        /// 经营地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        /// <summary>
        /// 经营性质
        /// </summary>
        public string BusinessNature
        {
            get { return _BusinessNature; }
            set { _BusinessNature = value; }
        }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicense
        {
            get { return _BusinessLicense; }
            set { _BusinessLicense = value; }
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
        /// 联系人电话
        /// </summary>
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        /// <summary>
        /// 商品售价
        /// </summary>
        public string ProductPrice
        {
            get { return _ProductPrice; }
            set { _ProductPrice = value; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specifications
        {
            get { return _Specifications; }
            set { _Specifications = value; }
        }
        /// <summary>
        /// 质量等级
        /// </summary>
        public string QualityGrade
        {
            get { return _QualityGrade; }
            set { _QualityGrade = value; }
        }
        /// <summary>
        /// 批号或编号
        /// </summary>
        public string BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }
        /// <summary>
        /// 注册商标
        /// </summary>
        public string RegisteredTrademark
        {
            get { return _RegisteredTrademark; }
            set { _RegisteredTrademark = value; }
        }
        /// <summary>
        /// 抽样数量
        /// </summary>
        public string SamplingNumber
        {
            get { return _SamplingNumber; }
            set { _SamplingNumber = value; }
        }
        /// <summary>
        /// 抽样基数
        /// </summary>
        public string SamplingBase
        {
            get { return _SamplingBase; }
            set { _SamplingBase = value; }
        }
        /// <summary>
        /// 进货数量
        /// </summary>
        public string IntoNumber
        {
            get { return _IntoNumber; }
            set { _IntoNumber = value; }
        }
        /// <summary>
        /// 商品进货验收制度执行情况
        /// </summary>
        public string Implementation
        {
            get { return _Implementation; }
            set { _Implementation = value; }
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public string InventoryNubmer
        {
            get { return _InventoryNubmer; }
            set { _InventoryNubmer = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
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
        /// 样品编号
        /// </summary>
        public string SamplingCode
        {
            get { return _SamplingCode; }
            set { _SamplingCode = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NoteUnder
        {
            get { return _NoteUnder; }
            set { _NoteUnder = value; }
        }
        /// <summary>
        /// 核准人
        /// </summary>
        public string ApprovedUser
        {
            get { return _ApprovedUser; }
            set { _ApprovedUser = value; }
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
        /// 创建时间
        /// </summary>
        public string CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit 
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

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
            /// 报表ID
            /// </summary>
            public string ReportID { get; set; }

            /// <summary>
            /// 检测项目
            /// </summary>
            public string CheckItem { get; set; }

            /// <summary>
            /// 检测依据
            /// </summary>
            public string CheckBasis { get; set; }

            /// <summary>
            /// 项目名称
            /// </summary>
            public string ProjectName { get; set; }

            /// <summary>
            /// 标准值
            /// </summary>
            public string StandardValues { get; set; }

            /// <summary>
            /// 实测值
            /// </summary>
            public string CheckValues { get; set; }

            /// <summary>
            /// 结论
            /// </summary>
            public string Conclusion { get; set; }

            /// <summary>
            /// 检测日期
            /// </summary>
            public string CheckData { get; set; }

            /// <summary>
            /// 检测日期
            /// </summary>
            public string CheckUser { get; set; }

            /// <summary>
            /// 系统编码
            /// </summary>
            public string SysCode { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public string Unit { get; set; }
        }

    }
}
