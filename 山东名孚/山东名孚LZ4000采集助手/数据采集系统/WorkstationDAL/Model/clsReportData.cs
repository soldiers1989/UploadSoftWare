using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class clsReportData
    {
        //编号
        private string _GridNum = string.Empty;
        public string GridNum
        {
            set
            {
                _GridNum = value;
            }
            get
            {
                return _GridNum;
            }
        }
        //检测项目SampleName
        private string _Checkitem = string.Empty;
        public string Checkitem
        {
            set
            {
                _Checkitem = value;
            }
            get
            {
                return _Checkitem;
            }
        }
        //样品名称
        private string _SampleName = string.Empty;
        public string SampleName
        {
            set
            {
                _SampleName = value;
            }
            get
            {
                return _SampleName;
            }
        }
        //检测数据
        private string _CheckData = string.Empty;
        public string CheckData
        {
            set
            {
                _CheckData = value;
            }
            get
            {
                return _CheckData;
            }
        }
        //检测数据的单位
        private string _Unit = string.Empty;
        public string Unit
        {
            set
            {
                _Unit = value;
            }
            get
            {
                return _Unit;
            }
        }
        //测试时间
        private string _CheckTime = string.Empty;
        public string CheckTime
        {
            set
            {
                _CheckTime = value;
            }
            get
            {
                return _CheckTime;
            }
        }
        //送检单位
        private string _CheckUnit = string.Empty;
        public string CheckUnit
        {
            set
            {
                _CheckUnit = value;
            }
            get
            {
                return _CheckUnit;
            }
        }
        //结论
        private string _Result = string.Empty;
        public string Result
        {
            set
            {
                _Result = value;
            }
            get
            {
                return _Result;
            }
        }
        private string _machine = string.Empty;
        /// <summary>
        /// 检测仪器
        /// </summary>
        public string machine
        {
            set
            {
                _machine = value;
            }
            get
            {
                return _machine;
            }        
        }
        private string _gettime = string.Empty;
        /// <summary>
        /// 采样时间
        /// </summary>
        public string gettime
        {
            set
            {
                _gettime = value;
            }
            get
            {
                return _gettime;
            }    
        }
        private string _getplace = string.Empty;
        /// <summary>
        /// 采样地点
        /// </summary>
        public string getplace
        {
            set
            {
                _getplace = value;
            }
            get
            {
                return _getplace;
            }    
        }
        private string _plannum = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public string plannum
        {          
            set
            {
                _plannum = value;
            }
            get
            {
                return _plannum;
            }    
        }
        private string _testbase = string.Empty;
        /// <summary>
        /// 检定依据
        /// </summary>
        public string testbase
        {
            set
            {
                _testbase = value;
            }
            get
            {
                return _testbase;
            }    
        }
        private string _limitdata = string.Empty;
        /// <summary>
        /// 限定值
        /// </summary>
        public string limitdata
        {
            set
            {
                _limitdata = value;
            }
            get
            {
                return _limitdata;
            }    
        }
        private string _tester = string.Empty;
        /// <summary>
        /// 检定员
        /// </summary>
        public string tester
        {
            set
            {
                _tester = value;
            }
            get
            {
                return _tester;
            }    
        }
        private string _stockin = string.Empty;
        /// <summary>
        /// 进货数量
        /// </summary>
        public string stockin
        {
            set
            {
                _stockin = value;
            }
            get
            {
                return _stockin;
            }
        }
        private string _sampleNum = string.Empty;
        /// <summary>
        /// 样品数量
        /// </summary>
        public string sampleNum
        {
            set
            {
                _sampleNum = value;
            }
            get
            {
                return _sampleNum;
            }
        }
        private string _detectunit = string.Empty;
        /// <summary>
        /// 检测单位
        /// </summary>
        public string dectectunit
        {
            set
            {
                _detectunit = value;
            }
            get
            {
                return _detectunit;
            }
 
        }
        private string _sampletype = "";
        /// <summary>
        /// 样品种类
        /// </summary>
        public string sampletype
        {
            set
            {
                _sampletype = value;
            }
            get
            {
                return _sampletype;
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
        private string _barcode = "";
        /// <summary>
        /// 条形码
        /// </summary>
        public string Barcode
        {
            set
            {
                _barcode = value;
            }
            get
            {
                return _barcode;
            }
        }
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
    }
}
