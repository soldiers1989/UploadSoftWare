
namespace DYSeriesDataSet.DataSentence.Kjc
{
    /// <summary>
    /// 验证用户名合法性
    /// Create wenj
    /// Time 2017年9月19日
    /// </summary>
    public class CheckUserConnect
    {
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string pointNum { get; set; }

        /// <summary>
        /// 检测点ID
        /// </summary>
        public string pointId { get; set; }

        /// <summary>
        /// 检测点名称
        /// </summary>
        public string pointName { get; set; }

        /// <summary>
        /// 检测点类型
        /// </summary>
        public string pointType { get; set; }

        /// <summary>
        /// 检测点所属机构编号
        /// </summary>
        public string orgNum { get; set; }

        /// <summary>
        /// 检测点所属机构名称
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 检测点名称
        /// </summary>
        public string p_point_name { get; set; }
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string p_point_code { get; set; }
        /// <summary>
        /// 检测点类型
        /// </summary>
        public string p_point_type { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string d_depart_name { get; set; }
        /// <summary>
        /// 行政机构ID
        /// </summary>
        public string d_id { get; set; }

    }
}