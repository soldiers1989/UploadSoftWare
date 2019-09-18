
namespace DY.FoodClientLib.Model
{
    /// <summary>
    /// 深圳报表子表model
    /// 2016年1月14日
    /// wenj
    /// </summary>
    public class clsReportDetailSZ
    {
        private int _ID = 0;
        private int _ReportID = 0;
        private string _Code = "";
        private string _SampleName = "";
        private string _SampleBase = "";
        private string _SampleSource = "";
        private string _Result = "";
        private string _SysCode = "";

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 主表ID
        /// </summary>
        public int ReportID 
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string SampleName
        {
            get { return _SampleName; }
            set { _SampleName = value; }
        }

        /// <summary>
        /// 抽样基数
        /// </summary>
        public string SampleBase
        {
            get { return _SampleBase; }
            set { _SampleBase = value; }
        }

        /// <summary>
        /// 样品来源
        /// </summary>
        public string SampleSource
        {
            get { return _SampleSource; }
            set { _SampleSource = value; }
        }

        /// <summary>
        /// 结论
        /// </summary>
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }

    }
}
