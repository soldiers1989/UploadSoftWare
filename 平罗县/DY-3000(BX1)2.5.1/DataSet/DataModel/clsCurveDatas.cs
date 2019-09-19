
namespace DYSeriesDataSet.DataModel
{
    /// <summary>
    /// 曲线数据
    /// Create wenj
    /// Time 2017年6月8日
    /// </summary>
    public class clsCurveDatas
    {
        private int _ID = 0;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _SysCode = string.Empty;
        /// <summary>
        /// 对应检测结果的UUID
        /// </summary>
        public string SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }


        private string _CData = string.Empty;
        /// <summary>
        /// 曲线数据
        /// </summary>
        public string CData
        {
            get { return _CData; }
            set { _CData = value; }
        }

    }
}