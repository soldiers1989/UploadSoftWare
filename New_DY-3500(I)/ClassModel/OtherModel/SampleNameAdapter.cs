using System;

namespace OtherModel
{
    [Serializable]
    public class CheckPointInfo
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string url = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        public string user = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd = string.Empty;
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string pointNum = string.Empty;
        /// <summary>
        /// 检测点ID
        /// </summary>
        public string pointId = string.Empty;
        /// <summary>
        /// 检测点名称
        /// </summary>
        public string pointName = string.Empty;
        /// <summary>
        /// 检测点类型
        /// </summary>
        public string pointType = string.Empty;
        /// <summary>
        /// 所属行政机构
        /// </summary>
        public string orgName = string.Empty;
        /// <summary>
        /// 所属行政机构编号
        /// </summary>
        public string orgNum = string.Empty;
        public string nickName = string.Empty;
        public string userId = string.Empty;

    }
}