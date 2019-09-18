using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    public class clsSaveResult
    {
      
        private string _Save = string.Empty;
        /// <summary>
        /// 是否已保存
        /// </summary>
        public string Save
        {
            get
            {
                return _Save;
            }
            set
            {
                _Save = value;
            }
        }
        private string _CheckNumber = string.Empty;
        /// <summary>
        /// 检测编号
        /// </summary>
        public string CheckNumber
        {
            get
            {
                return _CheckNumber;
            }
            set
            {
                _CheckNumber = value;
            }
        }

       

        /// <summary>
        /// DataGrid记录编号
        /// </summary>
        private string _Gridnum = string.Empty;
        public string Gridnum
        {
            get
            {
                return _Gridnum;
            }
            set
            {
                _Gridnum = value;
            }
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        private string _Checkitem = string.Empty;
        public string Checkitem
        {
            get
            {
                return _Checkitem;
            }
            set
            {
                _Checkitem = value;
            }

        }
        private string _SampeID = string.Empty;
        public string SampeID
        {
            get
            {
                return _SampeID;
            }
            set
            {
                _SampeID = value;
            }
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        private string _SampleName = string.Empty;
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SampleName
        {
            get
            {
                return _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }
        private string _SampleCode = string.Empty;
        public string SampleCode
        {
            get
            {
                return _SampleCode;
            }
            set
            {
                _SampleCode = value;
            }
        }
        /// <summary>
        /// 检测数据，抑制率
        /// </summary>
        private string _CheckData = string.Empty;
        public string CheckData
        {
            get
            {
                return _CheckData;
            }
            set
            {
                _CheckData = value;
            }

        }
        /// <summary>
        /// 检测数据结果的单位
        /// </summary>
        private string _Unit = string.Empty;
        public string Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }
        /// <summary>
        /// 检测时间
        /// </summary>
        private DateTime _CheckTime;
        public DateTime CheckTime
        {
            get
            {
                return _CheckTime;
            }
            set
            {
                _CheckTime = value;
            }
        }
        /// <summary>
        /// 被检单位
        /// </summary>
        private string _CheckUnit = string.Empty;
        public string CheckUnit
        {
            get
            {
                return _CheckUnit;
            }
            set
            {
                _CheckUnit = value;
            }
        }

        private string _CheckUnitNature = "";
        /// <summary>
        /// 被检单位性质
        /// </summary>
        public string CheckUnitNature
        {
            get
            {
                return _CheckUnitNature;
            }
            set
            {
                _CheckUnitNature = value;
            }
        }

        private string _Result = string.Empty;
        /// <summary>
        /// 结论
        /// </summary>
        public string Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }
        private string _instrument = string.Empty;
        /// <summary>
        /// 仪器名称
        /// </summary>
        public string Instrument
        {
            get
            {
                return _instrument;
            }
            set
            {
                _instrument = value;
            }
        }
        private string _gettime = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public string Gettime
        {
            get
            {
                return _gettime;
            }
            set
            {
                _gettime = value;
            }
        }
        private string _getplace = string.Empty;
        /// <summary>
        /// 采样地点
        /// </summary>
        public string Getplace
        {
            get
            {
                return _getplace;
            }
            set
            {
                _getplace = value;
            }
        }
        private string _quantityin = string.Empty;
        /// <summary>
        /// 进货数量
        /// </summary>
        public string quantityin
        {
            get
            {
                return _quantityin;
            }
            set
            {
                _quantityin = value;
            }
        }
        private string _sampleNum;
        /// <summary>
        /// 采样数量
        /// </summary>
        public string sampleNum
        {
            get
            {
                return _sampleNum;
            }
            set
            {
                _sampleNum = value;
            }
        }
        /// <summary>
        /// 通道号
        /// </summary>
        private string _holeSize = "";
        public string holeSize
        {
            get
            {
                return _holeSize;
            }
            set
            {
                _holeSize = value;
            }
        }

        private string _planNum = string.Empty;
        /// <summary>
        /// 计划编号 
        /// </summary>
        public string PlanNum
        {
            get
            {
                return _planNum;
            }
            set
            {
                _planNum = value;
            }
        }
        private string _testbase;
        /// <summary>
        /// 检测依据
        /// </summary>
        public string Testbase
        {
            get
            {
                return _testbase;
            }
            set
            {
                _testbase = value;
            }
        }
        private string _limitData = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public string LimitData
        {
            get
            {
                return _limitData;
            }
            set
            {
                _limitData = value;
            }
        }
        private string _tester = string.Empty;
        /// <summary>
        /// 检定员
        /// </summary>
        public string Tester
        {
            get
            {
                return _tester;
            }
            set
            {
                _tester = value;
            }
        }
        private string _stockin = string.Empty;
        /// <summary>
        /// 进货数量
        /// </summary>
        public string stockin
        {
            get
            {
                return _stockin;
            }
            set
            {
                _stockin = value;
            }
        }
        private string _detectunit = string.Empty;
        /// <summary>
        /// 检测单位
        /// </summary>
        public string detectunit
        {
            get
            {
                return _detectunit;
            }
            set
            {
                _detectunit = value;
            }
        }
        private string _SampleType = "";
        /// <summary>
        /// 样品种类
        /// </summary>
        public string SampleType
        {
            get
            {
                return _SampleType;
            }
            set
            {
                _SampleType = value;
            }
        }
        private string _IntrumentNum = "";
        /// <summary>
        /// 仪器编号
        /// </summary>
        public string IntrumentNum
        {
            get
            {
                return _IntrumentNum;
            }
            set
            {
                _IntrumentNum = value;
            }
        }

        private string _ProductPlace = "";
        /// <summary>
        /// 样品产地
        /// </summary>
        public string ProductPlace
        {
            get
            {
                return _ProductPlace;
            }
            set
            {
                _ProductPlace = value;
            }
        }
        private string _ProductDatetime = "";
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductDatetime
        {
            get
            {
                return _ProductDatetime;
            }
            set
            {
                _ProductDatetime = value;
            }
        }

        private string _Barcode = "";
        /// <summary>
        /// 条形码
        /// </summary>
        public string Barcode
        {
            get
            {
                return _Barcode;
            }
            set
            {
                _Barcode = value;
            }
        }
        private string _ProcodeCompany = "";
        /// <summary>
        /// 生产企业
        /// </summary>
        public string ProcodeCompany
        {
            get
            {
                return _ProcodeCompany;
            }
            set
            {
                _ProcodeCompany = value;
            }
        }
        private string _ProduceAddr = "";
        /// <summary>
        /// 产地地址
        /// </summary>
        public string ProduceAddr
        {
            get
            {
                return _ProduceAddr;
            }
            set
            {
                _ProduceAddr = value;
            }
        }
        private string _ProduceUnit = "";
        /// <summary>
        /// 生产单位
        /// </summary>
        public string ProduceUnit
        {
            get
            {
                return _ProduceUnit;
            }
            set
            {
                _ProduceUnit = value;
            }
        }
        private string _SendTestDate = "";
        /// <summary>
        /// 送检日期
        /// </summary>
        public string SendTestDate
        {
            get
            {
                return _SendTestDate;
            }
            set
            {
                _SendTestDate = value;
            }
        }
        private string _NumberUnit = "";
        /// <summary>
        /// 数量单位
        /// </summary>
        public string NumberUnit
        {
            get
            {
                return _NumberUnit;
            }
            set
            {
                _NumberUnit = value;
            }
        }
        private string _numUnit = "";
        /// <summary>
        /// 数值单位
        /// </summary>
        public string numUnit
        {
            set
            {
                _numUnit = value;
            }
            get
            {
                return _numUnit;
            }
        }
        //private string _barcode = "";
        ///// <summary>
        ///// 条形码
        ///// </summary>
        //public string Barcode
        //{
        //    set
        //    {
        //        _barcode = value;
        //    }
        //    get
        //    {
        //        return _barcode;
        //    }
        //}
        private string _productUnit = "";
        /// <summary>
        /// 生产单位
        /// </summary>
        public string productUnit
        {
            set
            {
                _productUnit = value;
            }
            get
            {
                return _productUnit;
            }
        }
        private string _productAddr = "";
        /// <summary>
        /// 产地地址
        /// </summary>
        public string productAddr
        {
            set
            {
                _productAddr = value;
            }
            get
            {
                return _productAddr;
            }
        }

        private string _ProductCompany = "";
        /// <summary>
        /// 生产企业
        /// </summary>
        public string ProductCompany
        {
            set
            {
                _ProductCompany = value;
            }
            get
            {
                return _ProductCompany;
            }
        }
        private string _Addr = "";
        /// <summary>
        /// 产地
        /// </summary>
        public string Addr
        {
            set
            {
                _Addr = value;
            }
            get
            {
                return _Addr;
            }
        }
        private string _ProductDate = "";
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductDate
        {
            set
            {
                _ProductDate = value;
            }
            get
            {
                return _ProductDate;
            }
        }
        private string _SendDate = "";
        /// <summary>
        /// 送检日期
        /// </summary>
        public string SendDate
        {
            set
            {
                _SendDate = value;
            }
            get
            {
                return _SendDate;
            }
        }
        private string _TreatResult = "";
        /// <summary>
        /// 处理结果
        /// </summary>
        public string TreatResult
        {
            set
            {
                _TreatResult = value;
            }
            get
            {
                return _TreatResult;
            }
        }
        private string _IsUpLoad = "";
        /// <summary>
        ///是否已上传
        /// </summary>
        public string IsUpLoad 
        {
            set
            {
                _IsUpLoad = value;
            }
            get
            {
                return _IsUpLoad;
            }
        }
        private string _HoleNumber = string.Empty;
        public string HoleNumber
        {
            set
            {
                _HoleNumber = value;
            }
            get
            {
                return _HoleNumber;
            }
        }
        private string _BID = "";
        public string BID
        {
            set
            {
                _BID = value;
            }
            get
            {
                return _BID;
            }
        }
       
    }
}
