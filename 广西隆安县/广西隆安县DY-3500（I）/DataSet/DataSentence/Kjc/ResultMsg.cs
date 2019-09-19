
namespace DYSeriesDataSet.DataSentence.Kjc
{
    /// <summary>
    /// 接口返回信息
    /// Create wenj
    /// Time 2017年4月27日
    /// </summary>
    public class ResultMsg
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string resultDescripe { get; set; }

        /// <summary>
        /// json对象
        /// </summary>
        public object result { get; set; }

    }
}
