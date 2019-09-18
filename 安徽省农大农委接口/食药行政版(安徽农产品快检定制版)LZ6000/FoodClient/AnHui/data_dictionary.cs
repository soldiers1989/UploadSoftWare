
namespace FoodClient.AnHui
{ /// <summary>
    /// 安徽省项目 - 数据字典表，含食品种类和检测项目
    /// </summary>
    public class data_dictionary
    {
        public string id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 名称代码
        /// </summary>
        public string codeId { get; set; }
        /// <summary>
        /// 类别代码
        /// </summary>
        public string typeNum { get; set; }
        /// <summary>
        /// 父类id
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 数据状态(C:新增；U：修改；D：删除)
        /// </summary>
        public string status { get; set; }

        public string inputdate { get; set; }

        public string modifydate { get; set; }

    }
}