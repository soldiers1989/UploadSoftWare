using System;
using System.Collections.Generic;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 快检结果上报
    /// </summary>
    public class uploadResult
    {

        public uploadResult() { }

        /// <summary>
        /// 快检结果上报 - 请求
        /// </summary>
        public class Request
        {
            private string _username = string.Empty;
            private string _itemid = string.Empty;
            private string _deviceid = string.Empty;
            private Int32 _totalnum = 0;
            private string _Longitude = string.Empty;
            private string _Latitude = string.Empty;

            /// <summary>
            /// 登录人账号
            /// </summary>
            public string username
            {
                get { return _username; }
                set { _username = value; }
            }
            /// <summary>
            /// 检测项目
            /// </summary>
            public string itemid
            {
                get { return _itemid; }
                set { _itemid = value; }
            }
            /// <summary>
            /// 设备编号
            /// </summary>
            public string deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            /// <summary>
            /// details属性中的条数
            /// </summary>
            public Int32 totalnum
            {
                get { return _totalnum; }
                set { _totalnum = value; }
            }
            /// <summary>
            /// 经度
            /// </summary>
            public string longitude
            {
                get { return _Longitude; }
                set { _Longitude = value; }
            }
            /// <summary>
            /// 维度
            /// </summary>
            public string latitude
            {
                get { return _Latitude; }
                set { _Latitude = value; }
            }

            public List<details> Details
            {
                get
                {
                    return detailsList;
                }
                set
                {
                    value = detailsList;
                }
            }
            public List<details> detailsList = new List<details>();
            public class details
            {
                public string SysCode = string.Empty;

                private string _sampleid = string.Empty;
                /// <summary>
                /// 样品编号
                /// </summary>
                public string sampleid
                {
                    get { return _sampleid; }
                    set { _sampleid = value; }
                }
                private string _doublevalue = string.Empty;
                /// <summary>
                /// 检测结果中的数值结果
                /// </summary>
                public string doublevalue
                {
                    get { return _doublevalue; }
                    set { _doublevalue = value; }
                }
                private string _unit = string.Empty;
                /// <summary>
                /// 检测结果中的单位
                /// </summary>
                public string unit
                {
                    get { return _unit; }
                    set { _unit = value; }
                }

                private string _stringvalue = string.Empty;
                /// <summary>
                /// 检测结果中的字符串结果
                /// </summary>
                public string stringvalue
                {
                    get { return _stringvalue; }
                    set { _stringvalue = value; }
                }
                private string _time = string.Empty;
                /// <summary>
                /// 时间
                /// </summary>
                public string time
                {
                    get { return _time; }
                    set { _time = value; }
                }
                //private string _barcode = string.Empty;
                ///// <summary>
                ///// 条形码
                ///// </summary>
                //public string barcode
                //{
                //    get { return _barcode; }
                //    set { _barcode = value; }
                //}
                //private string _bsampCompany = string.Empty;
                ///// <summary>
                ///// 被抽样单位
                ///// </summary>
                //public string bsampCompany
                //{
                //    get { return _bsampCompany; }
                //    set { _bsampCompany = value; }
                //}
                //private string _bscompAddr = string.Empty;
                ///// <summary>
                ///// 被抽样单位地址
                ///// </summary>
                //public string bscompAddr
                //{
                //    get { return _bscompAddr; }
                //    set { _bscompAddr = value; }
                //}
                //private string _bscompCont = string.Empty;
                ///// <summary>
                ///// 被抽样单位负责人
                ///// </summary>
                //public string bscompCont
                //{
                //    get { return _bscompCont; }
                //    set { _bscompCont = value; }
                //}
                //private string _bscompPhon = string.Empty;
                ///// <summary>
                ///// 被抽样单位电话
                ///// </summary>
                //public string bscompPhon
                //{
                //    get { return _bscompPhon; }
                //    set { _bscompPhon = value; }
                //}
                //private string _editor = string.Empty;
                ///// <summary>
                ///// 上传人
                ///// </summary>
                //public string editor
                //{
                //    get { return _editor; }
                //    set { _editor = value; }
                //}
                //private string _deitorOrg = string.Empty;
                ///// <summary>
                ///// 上传人所属机构
                ///// </summary>
                //public string deitorOrg
                //{
                //    get { return _deitorOrg; }
                //    set { _deitorOrg = value; }
                //}
            }
        }

        public class Response
        {
            private string _code = string.Empty;
            /// <summary>
            /// 0	上报成功	表述正常
            /// 1	参数有误(返回具体的错误内容)	usr/pwd/result等参数缺失或者为空
            /// 2	返回具体的错误内容	json字符串解析失败	不符合JSON格式
            /// 3	返回具体的错误内容	系统处理失败
            /// </summary>
            public string code
            {
                get { return _code; }
                set { _code = value; }
            }
            private string _message = string.Empty;
            /// <summary>
            /// 具体内容
            /// </summary>
            public string message
            {
                get { return _message; }
                set { _message = value; }
            }
        }

    }
}
