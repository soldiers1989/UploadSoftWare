
using System.Collections.Generic;
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
    }
    /// <summary>
    /// 北海通信类
    /// </summary>
    public class BeiHaiCommuncate
    {
        public string status { get; set; }
        public string username { get; set; }
        public string unit { get; set; }
        public string unitname { get; set; }
    }
    public class Beihaisamples
    {
        public List<tsampling> samplings { get; set; }
    }
    public class tsampling
    {
        public string productId { get; set; }
        public string goodsName { get; set; }
        public string operateId { get; set; }
        public string operateName { get; set; }
        public string marketId { get; set; }
        public string marketName { get; set; }
        public string samplingPerson { get; set; }
        public string samplingTime { get; set; }
        public string positionAddress { get; set; }
        public string goodsId { get; set; }
        public string IsTest { get; set; }
    }

    public class TestSamples
    {
        public string productId { get; set; }
        public string goodsName { get; set; }
        public string operateId { get; set; }
        public string operateName { get; set; }
        public string marketId { get; set; }
        public string marketName { get; set; }
        public string samplingPerson { get; set; }
        public string samplingTime { get; set; }
        public string positionAddress { get; set; }
        public string goodsId { get; set; }
        public int ID{ get; set; }
        public string IsTest { get; set; }

    }
    public class BeiHaiUploadData
    {
        public string productId { get; set; }
        public string goodsName { get; set; }
        public string testItem { get; set; }
        public string testOrganization { get; set; }
        public string checkPerson { get; set; }
        public string checkValue { get; set; }
        public string standardValue { get; set; }
        public string result { get; set; }
        public string checkTime { get; set; }
    }
}