using System;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 快检单获取
    /// </summary>
    public class getsample
    {

        public getsample() { }

        /// <summary>
        /// 获取快检单 - 请求
        /// </summary>
        public class Request
        {
            private string _sampleid = string.Empty;
            private string _deviceid = string.Empty;

            /// <summary>
            /// 快检单号
            /// </summary>
            public string sampleid
            {
                set
                {
                    _sampleid = value;
                }
                get
                {
                    return _sampleid;
                }
            }
            /// <summary>
            /// 唯一机器码
            /// </summary>
            public string deviceid
            {
                set
                {
                    _deviceid = value;
                }
                get
                {
                    return _deviceid;
                }
            }
        }

        /// <summary>
        /// 获取快检单 - 响应
        /// </summary>
        public class Response
        {
            private string _result = string.Empty;
            /// <summary>
            ///  1代表有值，0代表无此抽样单
            /// </summary>
            public string result
            {
                get { return _result; }
                set { _result = value; }
            }
            private string _sampleid = string.Empty;
            /// <summary>
            /// 抽样单号
            /// </summary>
            public string sampleid
            {
                get { return _sampleid; }
                set { _sampleid = value; }
            }
            private string _productName = string.Empty;
            /// <summary>
            /// 产品名称
            /// </summary>
            public string productName
            {
                get { return _productName; }
                set { _productName = value; }
            }
            private string _sampleDate = string.Empty;
            /// <summary>
            /// 抽样时间
            /// </summary>
            public string sampleDate
            {
                get { return _sampleDate; }
                set { _sampleDate = value; }
            }
            private string _sampleDept = string.Empty;
            /// <summary>
            /// 抽样单位
            /// </summary>
            public string sampleDept
            {
                get { return _sampleDept; }
                set { _sampleDept = value; }
            }
            private string _scompAddr = string.Empty;
            /// <summary>
            /// 抽样单位地址
            /// </summary>
            public string scompAddr
            {
                get { return _scompAddr; }
                set { _scompAddr = value; }
            }
            private string _scompCont = string.Empty;
            /// <summary>
            /// 抽样单位联系人
            /// </summary>
            public string scompCont
            {
                get { return _scompCont; }
                set { _scompCont = value; }
            }
            private string _scompPhon = string.Empty;
            /// <summary>
            /// 抽样单位联系电话
            /// </summary>
            public string scompPhon
            {
                get { return _scompPhon; }
                set { _scompPhon = value; }
            }
            private string _sampPerson = string.Empty;
            /// <summary>
            /// 抽样人
            /// </summary>
            public string sampPerson
            {
                get { return _sampPerson; }
                set { _sampPerson = value; }
            }
            private string _barCode = string.Empty;
            /// <summary>
            /// 条形码
            /// </summary>
            public string barCode
            {
                get { return _barCode; }
                set { _barCode = value; }
            }
            private string _bsampCompany = string.Empty;
            /// <summary>
            /// 被抽样单位
            /// </summary>
            public string bsampCompany
            {
                get { return _bsampCompany; }
                set { _bsampCompany = value; }
            }
            private string _bscompAddr = string.Empty;
            /// <summary>
            /// 被抽样单位地址
            /// </summary>
            public string bscompAddr
            {
                get { return _bscompAddr; }
                set { _bscompAddr = value; }
            }
            private string _bscompPhon = string.Empty;
            /// <summary>
            /// 被抽样单位电话
            /// </summary>
            public string bscompPhon
            {
                get { return _bscompPhon; }
                set { _bscompPhon = value; }
            }
            private string _bscompCont = string.Empty;
            /// <summary>
            /// 被抽样单位负责人
            /// </summary>
            public string bscompCont
            {
                get { return _bscompCont; }
                set { _bscompCont = value; }
            }
            private string _brand = string.Empty;
            /// <summary>
            /// 商标
            /// </summary>
            public string brand
            {
                get { return _brand; }
                set { _brand = value; }
            }
            private string _prodate = string.Empty;
            /// <summary>
            /// 生产日期
            /// </summary>
            public string prodate
            {
                get { return _prodate; }
                set { _prodate = value; }
            }
            private string _model = string.Empty;
            /// <summary>
            /// 规格型号
            /// </summary>
            public string model
            {
                get { return _model; }
                set { _model = value; }
            }
            private string _batchNum = string.Empty;
            /// <summary>
            /// 样品批号
            /// </summary>
            public string batchNum
            {
                get { return _batchNum; }
                set { _batchNum = value; }
            }
            private string _shelfLife = string.Empty;
            /// <summary>
            /// 保质期
            /// </summary>
            public string shelfLife
            {
                get { return _shelfLife; }
                set { _shelfLife = value; }
            }
            private string _proCompany = string.Empty;
            /// <summary>
            /// 生产者名称
            /// </summary>
            public string proCompany
            {
                get { return _proCompany; }
                set { _proCompany = value; }
            }
            private string _procompAddr = string.Empty;
            /// <summary>
            /// 生产者地址
            /// </summary>
            public string procompAddr
            {
                get { return _procompAddr; }
                set { _procompAddr = value; }
            }
            private string _procompPhon = string.Empty;
            /// <summary>
            /// 生产者联系电话
            /// </summary>
            public string procompPhon
            {
                get { return _procompPhon; }
                set { _procompPhon = value; }
            }

        }

    }
}
