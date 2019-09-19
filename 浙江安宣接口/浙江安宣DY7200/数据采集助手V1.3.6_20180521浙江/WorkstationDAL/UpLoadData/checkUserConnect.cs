using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    public class checkUserConnect
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
}
