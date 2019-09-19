using System;
using System.Collections.Generic;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 快检单上传
    /// </summary>
    public class uploadSample
    {

        public uploadSample() { }

        /// <summary>
        /// 快检单上传 - 请求
        /// </summary>
        public class Request
        {
            private String _deviceid = string.Empty;
            private String _totalnum = string.Empty;

            /// <summary>
            /// 仪器唯一机器码
            /// </summary>
            public String deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            /// <summary>
            /// TotalNum
            /// </summary>
            public String totalnum
            {
                get { return _totalnum; }
                set { _totalnum = value; }
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
                private String _sampleid = string.Empty;
                /// <summary>
                /// 抽检单号
                /// </summary>
                public String sampleid
                {
                    get { return _sampleid; }
                    set { _sampleid = value; }
                }
                private String _sampDate = string.Empty;
                /// <summary>
                /// 抽样日期
                /// </summary>
                public String sampDate
                {
                    get { return _sampDate; }
                    set { _sampDate = value; }
                }
                private String _sampCompany = string.Empty;
                /// <summary>
                /// 抽样单位
                /// </summary>
                public String sampCompany
                {
                    get { return _sampCompany; }
                    set { _sampCompany = value; }
                }
                private String _sampPerson = string.Empty;
                /// <summary>
                /// 抽样人
                /// </summary>
                public String sampPerson
                {
                    get { return _sampPerson; }
                    set { _sampPerson = value; }
                }
                private String _foodName = string.Empty;
                /// <summary>
                /// 样品名称
                /// </summary>
                public String foodName
                {
                    get { return _foodName; }
                    set { _foodName = value; }
                }
                private String _barcode = string.Empty;
                /// <summary>
                /// 条形码
                /// </summary>
                public String barcode
                {
                    get { return _barcode; }
                    set { _barcode = value; }
                }
                private String _bsampCompany = string.Empty;
                /// <summary>
                /// 被抽样单位
                /// </summary>
                public String bsampCompany
                {
                    get { return _bsampCompany; }
                    set { _bsampCompany = value; }
                }
                private String _bscompAddr = string.Empty;
                /// <summary>
                /// 被抽样单位地址
                /// </summary>
                public String bscompAddr
                {
                    get { return _bscompAddr; }
                    set { _bscompAddr = value; }
                }
                private String _bscompCont = string.Empty;
                /// <summary>
                /// 被抽样单位负责人
                /// </summary>
                public String bscompCont
                {
                    get { return _bscompCont; }
                    set { _bscompCont = value; }
                }
                private String _bscompPhon = string.Empty;
                /// <summary>
                /// 被抽样单位电话
                /// </summary>
                public String bscompPhon
                {
                    get { return _bscompPhon; }
                    set { _bscompPhon = value; }
                }
                private String _editor = string.Empty;
                /// <summary>
                /// 上传人
                /// </summary>
                public String editor
                {
                    get { return _editor; }
                    set { _editor = value; }
                }
                private String _deitorOrg = string.Empty;
                /// <summary>
                /// 上传人所属机构
                /// </summary>
                public String deitorOrg
                {
                    get { return _deitorOrg; }
                    set { _deitorOrg = value; }
                }
            }
        }

        public class Response
        {
            private String _code = string.Empty;
            /// <summary>
            /// 0	上报成功	表述正常
            /// 1	参数有误(返回具体的错误内容)	usr/pwd/result等参数缺失或者为空
            /// 2	返回具体的错误内容	json字符串解析失败	不符合JSON格式
            /// 3	返回具体的错误内容	系统处理失败
            /// </summary>
            public String code
            {
                get { return _code; }
                set { _code = value; }
            }
            private String _message = string.Empty;
            /// <summary>
            /// 具体内容
            /// </summary>
            public String message
            {
                get { return _message; }
                set { _message = value; }
            }
        }

    }
}
