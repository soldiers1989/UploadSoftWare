using System;
using System.Collections.Generic;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 快检结果下载
    /// </summary>
    public class downloadResult
    {

        public downloadResult() { }

        /// <summary>
        /// 下载请求
        /// </summary>
        public class Request
        {
            private string _deviceid = string.Empty;
            /// <summary>
            /// 仪器唯一机器码
            /// </summary>
            public string deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            private string _dateStart = string.Empty;
            /// <summary>
            /// 开始时间
            /// </summary>
            public string dateStart
            {
                get { return _dateStart; }
                set { _dateStart = value; }
            }
            private string _dateEnd = string.Empty;
            /// <summary>
            /// 结束时间
            /// </summary>
            public string dateEnd
            {
                get { return _dateEnd; }
                set { _dateEnd = value; }
            }
        }

        /// <summary>
        /// 下载返回
        /// </summary>
        public class Response
        {
            private string _result = string.Empty;
            /// <summary>
            /// 1代表有值，0代表无检测结果
            /// </summary>
            public string result
            {
                get { return _result; }
                set { _result = value; }
            }
            private string _deviceid = string.Empty;
            /// <summary>
            /// 仪器唯一机器码
            /// </summary>
            public string deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            private string _totalnum = string.Empty;
            /// <summary>
            /// 下载数据条数
            /// </summary>
            public string totalnum
            {
                get { return _totalnum; }
                set { _totalnum = value; }
            }

            private List<Details> detailsList
            {
                get
                {
                    return details;
                }
                set
                {
                    value = details;
                }
            }
            public List<Details> details = new List<Details>();
            public class Details
            {
                private string _productName = string.Empty;
                /// <summary>
                /// 产品名称
                /// </summary>
                public string productName
                {
                    get { return _productName; }
                    set { _productName = value; }
                }
                private string _sampleid = string.Empty;
                /// <summary>
                /// 样品编号
                /// </summary>
                public string sampleid
                {
                    get { return _sampleid; }
                    set { _sampleid = value; }
                }
                private string _itemid = string.Empty;
                /// <summary>
                /// 检测项目
                /// </summary>
                public string itemid
                {
                    get { return _itemid; }
                    set { _itemid = value; }
                }
                private string _doublevalue = string.Empty;
                /// <summary>
                /// 检测值
                /// </summary>
                public string doublevalue
                {
                    get { return _doublevalue; }
                    set { _doublevalue = value; }
                }
                private string _unit = string.Empty;
                /// <summary>
                /// 检测值单位
                /// </summary>
                public string unit
                {
                    get { return _unit; }
                    set { _unit = value; }
                }
                private string _stringvalue = string.Empty;
                /// <summary>
                /// 检测结论
                /// </summary>
                public string stringvalue
                {
                    get { return _stringvalue; }
                    set { _stringvalue = value; }
                }
                private string _time = string.Empty;
                /// <summary>
                /// 检测时间
                /// </summary>
                public string time
                {
                    get { return _time; }
                    set { _time = value; }
                }
                private string _ckcheckType = string.Empty;
                /// <summary>
                /// 项目类别（检测手段）
                /// </summary>
                public string ckcheckType
                {
                    get { return _ckcheckType; }
                    set { _ckcheckType = value; }
                }
                private string _ckvalue = string.Empty;
                /// <summary>
                /// 标准值
                /// </summary>
                public string ckvalue
                {
                    get { return _ckvalue; }
                    set { _ckvalue = value; }
                }
                private string _ckoName = string.Empty;
                /// <summary>
                /// 受检人/单位
                /// </summary>
                public string ckoName
                {
                    get { return _ckoName; }
                    set { _ckoName = value; }
                }
                private string _cksInfo = string.Empty;
                /// <summary>
                /// 检测依据
                /// </summary>
                public string cksInfo
                {
                    get { return _cksInfo; }
                    set { _cksInfo = value; }
                }
                private string _ckunitName = string.Empty;
                /// <summary>
                /// 检测单位
                /// </summary>
                public string ckunitName
                {
                    get { return _ckunitName; }
                    set { _ckunitName = value; }
                }
                private string _ckckName = string.Empty;
                /// <summary>
                /// 检测人
                /// </summary>
                public string ckckName
                {
                    get { return _ckckName; }
                    set { _ckckName = value; }
                }
                private string _cktakeName = string.Empty;
                /// <summary>
                /// 抽样人
                /// </summary>
                public string cktakeName
                {
                    get { return _cktakeName; }
                    set { _cktakeName = value; }
                }
                private string _ckoLdnum = string.Empty;
                /// <summary>
                /// 抽样单号
                /// </summary>
                public string ckoLdnum
                {
                    get { return _ckoLdnum; }
                    set { _ckoLdnum = value; }
                }


            }
        }



    }
}
