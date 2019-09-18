using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    public class KunShanEntity
    {
        /// <summary>
        /// 获取令牌 - 请求
        /// </summary>
        public class GetTokenRequest
        {
            public class Request
            {
                public string name { get; set; }
                public string password { get; set; }
            }

            public class webService
            {
                public Request request { get; set; }
            }
        }

        /// <summary>
        /// 获取令牌 - 响应
        /// </summary>
        public class GetTokenResponse
        {
            public class Response
            {
                public string tokenNo { get; set; }
                public string error { get; set; }
            }

            public class webService
            {
                public Response response { get; set; }
            }
        }

        /// <summary>
        /// 注销令牌 - 请求
        /// </summary>
        public class LogoutTokenRequest
        {
            public class Request
            {
                public string tokenNo { get; set; }
            }

            public class webService
            {
                public Request request { get; set; }
            }
        }

        /// <summary>
        /// 注销令牌 - 响应
        /// </summary>
        public class LogoutTokenResponse
        {
            public class Response
            {
                public string ResultCode { get; set; }
                public string error { get; set; }
            }

            public class webService
            {
                public Response response { get; set; }
            }
        }

        public class CheckItemRequest
        {
            public class Request
            {
                public string Id { get; set; }
                public string UpdateDate { get; set; }
            }

            public class Head
            {
                public string name { get; set; }
                public string password { get; set; }
            }

            public class webService
            {
                public Head head { get; set; }
                public Request request { get; set; }
            }
        }

        public class SalesItemRequest
        {
            public class Request
            {
                public string Id { get; set; }
                public string UpdateDate { get; set; }
            }

            public class Head
            {
                public string name { get; set; }
                public string password { get; set; }
            }

            public class webService
            {
                public Head head { get; set; }
                public Request request { get; set; }
            }
        }

        public class GetAreaSignContactReqyest
        {
            public class Request
            {
                public string LicenseNo { get; set; }
            }

            public class Head
            {
                public string name { get; set; }
                public string password { get; set; }
            }

            public class webService
            {
                public Head head { get; set; }
                public Request request { get; set; }
            }

            //public WebService webService { get; set; }
        }

        /// <summary>
        /// 获取市场的注册号和名称 - 请求
        /// </summary>
        public class QueryMarketRequest
        {
            public class Request
            {
                public string name { get; set; }

                public string password { get; set; }
            }

            public class webService
            {
                public Request request { get; set; }
            }
        }

        /// <summary>
        /// 获取市场的注册号和名称 - 响应
        /// </summary>
        public class QueryMarketResponse
        {
            public class Response
            {
                public string LicenseNo { get; set; }
                public string MarketName { get; set; }

               
            }
        }

        /// <summary>
        /// 数据上传 - 请求
        /// </summary>
        public class UploadRequest
        {
            public class Head
            {
                /// <summary>
                /// *接入单位编号
                /// </summary>
                public string marketCode { get; set; }
                /// <summary>
                /// *接入单位名称
                /// </summary>
                public string marketName { get; set; }
                /// <summary>
                /// *令牌号
                /// </summary>
                public string tokenNo { get; set; }
            }

            public class QuickCheckItemJC
            {
                /// <summary>
                /// *检测编号
                /// </summary>
                public string JCCode { get; set; }
                /// <summary>
                /// 软件版本：0市场,  1.超市  2.检测机构
                /// </summary>
                public string MarketType { get; set; }
                /// <summary>
                /// 经营户身份证号码 | 摊位编号 必须有一个有值
                /// </summary>
                public string DABH { get; set; }
                /// <summary>
                /// 经营户摊位编号 身份证号码 必须有一个有值
                /// </summary>
                public string PositionNo { get; set; }
                /// <summary>
                /// *经营户姓名
                /// </summary>
                public string Name { get; set; }
                /// <summary>
                /// *抽检的品种编码 见字典 6.8
                /// </summary>
                public string SubItemCode { get; set; }
                /// <summary>
                /// *抽检的品种名称
                /// </summary>
                public string SubItemName { get; set; }
                /// <summary>
                /// *抽检项目大类编号 见字典6.10
                /// </summary>
                public string QuickCheckItemCode { get; set; }
                /// <summary>
                /// *抽检项目小类编号 见字典6.11
                /// </summary>
                public string QuickCheckSubItemCode { get; set; }
                /// <summary>
                /// *检测时间
                /// </summary>
                public string QuickCheckDate { get; set; }
                /// <summary>
                /// *定性结果 参见6.4
                /// </summary>
                public string QuickCheckResult { get; set; }
                /// <summary>
                /// 定量结果值
                /// </summary>
                public string QuickCheckResultValue { get; set; }
                /// <summary>
                /// *单位
                /// </summary>
                public string QuickCheckResultValueUnit { get; set; }
                /// <summary>
                /// 参考范围
                /// </summary>
                public string QuickCheckResultValueCKarea { get; set; }
                /// <summary>
                /// *检测依据
                /// </summary>
                public string QuickCheckResultDependOn { get; set; }
                /// <summary>
                /// 检测备注
                /// </summary>
                public string QuickCheckRemarks { get; set; }
                /// <summary>
                /// *检测人姓名
                /// </summary>
                public string QuickChecker { get; set; }
                /// <summary>
                /// *复核人员姓名
                /// </summary>
                public string QuickReChecker { get; set; }
                /// <summary>
                /// 检测机构
                /// </summary>
                public string QuickCheckUnitName { get; set; }
                /// <summary>
                /// 检测机构编码
                /// </summary>
                public string QuickCheckUnitId { get; set; }
                /// <summary>
                /// *检测设备厂家中文名
                /// </summary>
                public string JCManufactor { get; set; }
                /// <summary>
                /// *检测设备型号
                /// </summary>
                public string JCModel { get; set; }
                /// <summary>
                /// *检测设备编号
                /// </summary>
                public string JCSN { get; set; }
                /// <summary>
                /// 0 初检，1复检
                /// </summary>
                public string ReviewIs { get; set; }
                
            }

            public class DataList
            {
                public QuickCheckItemJC QuickCheckItemJC { get; set; }
            }

            public class Request
            {
                public DataList dataList { get; set; }
            }

            public class webService
            {
                public Head head { get; set; }
                public Request request { get; set; }
            }
        }

        /// <summary>
        /// 数据上传 - 响应
        /// </summary>
        public class UploadResponse
        {
            public class Head
            {
                /// <summary>
                /// 发送时间
                /// </summary>
                public string sendTime { get; set; }
                /// <summary>
                /// 上传活动标识号
                /// </summary>
                public string ResultCode { get; set; }
                /// <summary>
                /// 总体描述
                /// </summary>
                public string describe { get; set; }
            }

            public class ErrorList
            {
                public string error { get; set; }
            }

            public class Response
            {
                public ErrorList errorList { get; set; }
            }

            public class webService
            {
                public Head head { get; set; }
                public Response response { get; set; }
            }
        }

        /// <summary>
        /// 分局辖区内的单位主体信息
        /// </summary>
        public class AreaMarket
        {
            private string _LicenseNo = string.Empty;
            /// <summary>
            /// 单位主体编码 用于获取对应的经营户信息
            /// </summary>
            public string LicenseNo
            {
                get { return _LicenseNo; }
                set { _LicenseNo = value; }
            }

            private string _MarketName = string.Empty;
            /// <summary>
            /// 单位主体名称
            /// </summary>
            public string MarketName
            {
                get { return _MarketName; }
                set { _MarketName = value; }
            }

            private string _Abbreviation = string.Empty;
            /// <summary>
            /// 单位主体简称
            /// </summary>
            public string Abbreviation
            {
                get { return _Abbreviation; }
                set { _Abbreviation = value; }
            }

            private string _MarketRef = string.Empty;
            /// <summary>
            /// 单位主体类别 1 批发市场 2.农贸市场 3. 检测机构 4.餐饮单位 5.食品生产企业 6.商场超市 7.个体工商户 8.食材配送企业 9. 单位食堂 10.集体用餐配送和中央厨房 11.农产品基地
            /// </summary>
            public string MarketRef
            {
                get
                {
                    return _MarketRef;
                }
                set { _MarketRef = value; }
            }
        }

        /// <summary>
        /// 经营户信息
        /// </summary>
        public class SignContact
        {
            private string _MarketName = string.Empty;
            /// <summary>
            /// 单位主体名称
            /// </summary>
            public string MarketName
            {
                get { return _MarketName; }
                set { _MarketName = value; }
            }

            private string _Abbreviation = string.Empty;
            /// <summary>
            /// 单位主体简称
            /// </summary>
            public string Abbreviation
            {
                get { return _Abbreviation; }
                set { _Abbreviation = value; }
            }

            private string _MarketRef = string.Empty;
            /// <summary>
            /// 单位主体类别
            /// </summary>
            public string MarketRef
            {
                get { return _MarketRef; }
                set { _MarketRef = value; }
            }

            /// <summary>
            /// 单位主体编号
            /// </summary>
            public string LicenseNo { get; set; }
            /// <summary>
            /// 摊位区域名称
            /// </summary>
            public string PositionDistrictName { get; set; }
            /// <summary>
            /// 摊位号
            /// </summary>
            public string PositionNo { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string DABH { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Contactor { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string ContactTel { get; set; }
        }

        /// <summary>
        /// 检测项目
        /// </summary>
        public class CheckItem
        {
            /// <summary>
            /// 唯一标识
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 大类项目编码
            /// </summary>
            public string ItemCode { get; set; }
            /// <summary>
            /// 大类项目名称
            /// </summary>
            public string ItemName { get; set; }
            /// <summary>
            /// 小类项目编码
            /// </summary>
            public string SubItemCode { get; set; }
            /// <summary>
            /// 小类项目名称
            /// </summary>
            public string SubItemName { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public string UpdateDate { get; set; }
        }

        /// <summary>
        /// 检测品种
        /// </summary>
        public class SalesItem
        {
            /// <summary>
            /// 唯一标识
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 大类品种编码
            /// </summary>
            public string ItemCode { get; set; }
            /// <summary>
            /// 大类品种名称
            /// </summary>
            public string ItemName { get; set; }
            /// <summary>
            /// 小类品种编码
            /// </summary>
            public string SubItemCode { get; set; }
            /// <summary>
            /// 小类品种名称
            /// </summary>
            public string SubItemName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SubItemAlias { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public string UpdateDate { get; set; }
        }

    }
}
