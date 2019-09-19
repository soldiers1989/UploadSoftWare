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
            private String _sampleid = string.Empty;
            private String _deviceid = string.Empty;

            /// <summary>
            /// 快检单号
            /// </summary>
            public String sampleid
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
            public String deviceid
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
            private String _result = string.Empty;
            /// <summary>
            ///  1代表有值，0代表无此抽样单
            /// </summary>
            public String result
            {
                get { return _result; }
                set { _result = value; }
            }
            private String _sampleid = string.Empty;
            /// <summary>
            /// 抽样单号
            /// </summary>
            public String sampleid
            {
                get { return _sampleid; }
                set { _sampleid = value; }
            }
            private String _productName = string.Empty;
            /// <summary>
            /// 产品名称
            /// </summary>
            public String productName
            {
                get { return _productName; }
                set { _productName = value; }
            }
            private String _sampleDate = string.Empty;
            /// <summary>
            /// 抽样时间
            /// </summary>
            public String sampleDate
            {
                get { return _sampleDate; }
                set { _sampleDate = value; }
            }
            private String _sampleDept = string.Empty;
            /// <summary>
            /// 抽样单位
            /// </summary>
            public String sampleDept
            {
                get { return _sampleDept; }
                set { _sampleDept = value; }
            }
            private String _scompAddr = string.Empty;
            /// <summary>
            /// 抽样单位地址
            /// </summary>
            public String scompAddr
            {
                get { return _scompAddr; }
                set { _scompAddr = value; }
            }
            private String _scompCont = string.Empty;
            /// <summary>
            /// 抽样单位联系人
            /// </summary>
            public String scompCont
            {
                get { return _scompCont; }
                set { _scompCont = value; }
            }
            private String _scompPhon = string.Empty;
            /// <summary>
            /// 抽样单位联系电话
            /// </summary>
            public String scompPhon
            {
                get { return _scompPhon; }
                set { _scompPhon = value; }
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
            private String _barCode = string.Empty;
            /// <summary>
            /// 条形码
            /// </summary>
            public String barCode
            {
                get { return _barCode; }
                set { _barCode = value; }
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
            private String _bscompPhon = string.Empty;
            /// <summary>
            /// 被抽样单位电话
            /// </summary>
            public String bscompPhon
            {
                get { return _bscompPhon; }
                set { _bscompPhon = value; }
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
            private String _brand = string.Empty;
            /// <summary>
            /// 商标
            /// </summary>
            public String brand
            {
                get { return _brand; }
                set { _brand = value; }
            }
            private String _prodate = string.Empty;
            /// <summary>
            /// 生产日期
            /// </summary>
            public String prodate
            {
                get { return _prodate; }
                set { _prodate = value; }
            }
            private String _model = string.Empty;
            /// <summary>
            /// 规格型号
            /// </summary>
            public String model
            {
                get { return _model; }
                set { _model = value; }
            }
            private String _batchNum = string.Empty;
            /// <summary>
            /// 样品批号
            /// </summary>
            public String batchNum
            {
                get { return _batchNum; }
                set { _batchNum = value; }
            }
            private String _shelfLife = string.Empty;
            /// <summary>
            /// 保质期
            /// </summary>
            public String shelfLife
            {
                get { return _shelfLife; }
                set { _shelfLife = value; }
            }
            private String _proCompany = string.Empty;
            /// <summary>
            /// 生产者名称
            /// </summary>
            public String proCompany
            {
                get { return _proCompany; }
                set { _proCompany = value; }
            }
            private String _procompAddr = string.Empty;
            /// <summary>
            /// 生产者地址
            /// </summary>
            public String procompAddr
            {
                get { return _procompAddr; }
                set { _procompAddr = value; }
            }
            private String _procompPhon = string.Empty;
            /// <summary>
            /// 生产者联系电话
            /// </summary>
            public String procompPhon
            {
                get { return _procompPhon; }
                set { _procompPhon = value; }
            }

        }

    }
}
