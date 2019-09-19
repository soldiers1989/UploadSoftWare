using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel.Wisdom
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
            private String _deviceid = string.Empty;
            /// <summary>
            /// 仪器唯一机器码
            /// </summary>
            public String deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            private String _dateStart = string.Empty;
            /// <summary>
            /// 开始时间
            /// </summary>
            public String dateStart
            {
                get { return _dateStart; }
                set { _dateStart = value; }
            }
            private String _dateEnd = string.Empty;
            /// <summary>
            /// 结束时间
            /// </summary>
            public String dateEnd
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
            private String _result = string.Empty;
            /// <summary>
            /// 1代表有值，0代表无检测结果
            /// </summary>
            public String result
            {
                get { return _result; }
                set { _result = value; }
            }
            private String _deviceid = string.Empty;
            /// <summary>
            /// 仪器唯一机器码
            /// </summary>
            public String deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            private String _totalnum = string.Empty;
            /// <summary>
            /// 下载数据条数
            /// </summary>
            public String totalnum
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
                private String _productName = string.Empty;
                /// <summary>
                /// 产品名称
                /// </summary>
                public String productName
                {
                    get { return _productName; }
                    set { _productName = value; }
                }
                private String _sampleid = string.Empty;
                /// <summary>
                /// 样品编号
                /// </summary>
                public String sampleid
                {
                    get { return _sampleid; }
                    set { _sampleid = value; }
                }
                private String _itemid = string.Empty;
                /// <summary>
                /// 检测项目
                /// </summary>
                public String itemid
                {
                    get { return _itemid; }
                    set { _itemid = value; }
                }
                private String _doublevalue = string.Empty;
                /// <summary>
                /// 检测值
                /// </summary>
                public String doublevalue
                {
                    get { return _doublevalue; }
                    set { _doublevalue = value; }
                }
                private String _unit = string.Empty;
                /// <summary>
                /// 检测值单位
                /// </summary>
                public String unit
                {
                    get { return _unit; }
                    set { _unit = value; }
                }
                private String _stringvalue = string.Empty;
                /// <summary>
                /// 检测结论
                /// </summary>
                public String stringvalue
                {
                    get { return _stringvalue; }
                    set { _stringvalue = value; }
                }
                private String _time = string.Empty;
                /// <summary>
                /// 检测时间
                /// </summary>
                public String time
                {
                    get { return _time; }
                    set { _time = value; }
                }
                private String _ckcheckType = string.Empty;
                /// <summary>
                /// 项目类别（检测手段）
                /// </summary>
                public String ckcheckType
                {
                    get { return _ckcheckType; }
                    set { _ckcheckType = value; }
                }
                private String _ckvalue = string.Empty;
                /// <summary>
                /// 标准值
                /// </summary>
                public String ckvalue
                {
                    get { return _ckvalue; }
                    set { _ckvalue = value; }
                }
                private String _ckoName = string.Empty;
                /// <summary>
                /// 受检人/单位
                /// </summary>
                public String ckoName
                {
                    get { return _ckoName; }
                    set { _ckoName = value; }
                }
                private String _cksInfo = string.Empty;
                /// <summary>
                /// 检测依据
                /// </summary>
                public String cksInfo
                {
                    get { return _cksInfo; }
                    set { _cksInfo = value; }
                }
                private String _ckunitName = string.Empty;
                /// <summary>
                /// 检测单位
                /// </summary>
                public String ckunitName
                {
                    get { return _ckunitName; }
                    set { _ckunitName = value; }
                }
                private String _ckckName = string.Empty;
                /// <summary>
                /// 检测人
                /// </summary>
                public String ckckName
                {
                    get { return _ckckName; }
                    set { _ckckName = value; }
                }
                private String _cktakeName = string.Empty;
                /// <summary>
                /// 抽样人
                /// </summary>
                public String cktakeName
                {
                    get { return _cktakeName; }
                    set { _cktakeName = value; }
                }
                private String _ckoLdnum = string.Empty;
                /// <summary>
                /// 抽样单号
                /// </summary>
                public String ckoLdnum
                {
                    get { return _ckoLdnum; }
                    set { _ckoLdnum = value; }
                }

            }
        }

    }
}