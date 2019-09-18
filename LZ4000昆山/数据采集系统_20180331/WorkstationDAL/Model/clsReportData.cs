using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class clsReportData
    {
        private string _ID = "";
        /// <summary>
        /// 序号
        /// </summary>
        public string ID
        {
            set
            {
                _ID = value;
            }
            get
            {
                return _ID;
            }
        }

        private string _CheckNumber = "";
        //检测编号
        public string CheckNumber
        {
            set
            {
                _CheckNumber = value;
            }
            get
            {
                return _CheckNumber;
            }
        }


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
        private string _ProductPlace = "";
        /// <summary>
        /// 产地
        /// </summary>
        public string ProductPlace
        {
            set
            {
                _ProductPlace = value;
            }
            get
            {
                return _ProductPlace;
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
        private string _CompanyNeture = "";
        /// <summary>
        /// 被检单位性质
        /// </summary>
        public string CompanyNeture
        {
            set
            {
                _CompanyNeture = value;
            }
            get
            {
                return _CompanyNeture;
            }
        }
        //被检单位
        private string _CheckUnit = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
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

        private string _CheckUnitNature = "";
        /// <summary>
        /// 被检单位性质
        /// </summary>
        public string CheckUnitNature
        {
            set
            {
                _CheckUnitNature = value;
            }
            get
            {
                return _CheckUnitNature;
            }
        }
        private string _CheckCompanyCode = "";
        /// <summary>
        /// 被检单位编号
        /// </summary>
        public string CheckCompanyCode
        {
            set
            {
                _CheckCompanyCode = value;
            }
            get
            {
                return _CheckCompanyCode;
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
        private string _MachineNumber = "";
        /// <summary>
        /// 仪器编号
        /// </summary>
        public string MachineNumber
        {
            set
            {
                _MachineNumber = value;
            }
            get
            {
                return _MachineNumber;
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
        private string _Retester = string.Empty;
        /// <summary>
        /// 复核人
        /// </summary>
        public string Retester
        {
            set
            {
                _Retester = value;
            }
            get
            {
                return _Retester;
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
        private string _SampleCode = "";
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SampleCode
        {
            set
            {
                _SampleCode = value;
            }
            get
            {
                return _SampleCode;
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
        private string _Doresult = "";
        /// <summary>
        /// 处理结果
        /// </summary>
        public string Doresult
        {
            set
            {
                _Doresult = value;
            }
            get
            {
                return _Doresult;
            }
        }
        private string _IsUpload = "";
        /// <summary>
        /// 已上传
        /// </summary>
        public string IsUpload
        {
            set
            {
                _IsUpload = value;
            }
            get
            {
                return _IsUpload;
            }
        }
        private string _IsSave = "";
        /// <summary>
        /// 已保存
        /// </summary>
        public string IsSave
        {
            set
            {
                _IsSave = value;
            }
            get
            {
                return _IsSave;
            }
        }

        private string _MarketType = "";
        /// <summary>
        /// 市场类别
        /// </summary>
        public string MarketType
        {
            set
            {
                _MarketType = value;
            }
            get
            {
                return _MarketType;
            }
        }
        private string _jyhsfzh = "";
        /// <summary>
        /// 身份证号
        /// </summary>
        public string jyhsfzh
        {
            set
            {
                _jyhsfzh = value;
            }
            get
            {
                return _jyhsfzh;
            }
        }
        private string _stallnum = "";
        /// <summary>
        /// 摊位号
        /// </summary>
        public string stallnum
        {
            set
            {
                _stallnum = value;
            }
            get
            {
                return _stallnum;
            }
        }
        private string _jyhu = "";
        /// <summary>
        /// 经营户
        /// </summary>
        public string jyhu
        {
            set
            {
                _jyhu = value;
            }
            get
            {
                return _jyhu;
            }
        }
        private string _CheckItemCode = "";
        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string CheckItemCode 
        {
            set
            {
                _CheckItemCode = value;
            }
            get
            {
                return _CheckItemCode;
            }
        }
        private string _CheckItemsmallCode = "";
        /// <summary>
        /// 检测项目小类编号
        /// </summary>
        public string CheckItemsmallCode
        {
            set
            {
                _CheckItemsmallCode = value;
            }
            get
            {
                return _CheckItemsmallCode;
            }
        }
        private string _Checkjigou = "";
        /// <summary>
        /// 检测机构
        /// </summary>
        public string Checkjigou
        {
            set
            {
                _Checkjigou = value;
            }
            get
            {
                return _Checkjigou;
            }
        }
        private string _CheckjigouCode = "";
        /// <summary>
        /// 检测机构编号
        /// </summary>
        public string CheckjigouCode
        {
            set
            {
                _CheckjigouCode = value;
            }
            get
            {
                return _CheckjigouCode;
            }
        }
        private string _CheckRestest = "";
        /// <summary>
        /// 初检/复检
        /// </summary>
        public string CheckRestest
        {
            set
            {
                _CheckRestest = value;
            }
            get
            {
                return _CheckRestest;
            }
        }
        private string _beizhu = "";
        /// <summary>
        /// 初检/复检
        /// </summary>
        public string beizhu 
        {
            set
            {
                _beizhu = value;
            }
            get
            {
                return _beizhu;
            }
        }
    }
}
