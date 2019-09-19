using System;

namespace AIO
{
    [Serializable]
    public class CheckPointInfo
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddr = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        public string RegisterID = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string RegisterPassword = string.Empty;
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string CheckPointID = string.Empty;
        /// <summary>
        /// 检测点名称
        /// </summary>
        public string CheckPointName = string.Empty;
        /// <summary>
        /// 检测点类型
        /// </summary>
        public string CheckPointType = string.Empty;
        /// <summary>
        /// 所属行政机构
        /// </summary>
        public string Organization = string.Empty;
        /// <summary>
        /// 所属行政机构编号
        /// </summary>
        public string CheckPlaceCode = string.Empty;
        public string UploadUser;
        public string UploadUserUUID;

    }
}