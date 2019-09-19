using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    public class clsSaveResult
    {
        /// <summary>
        /// 是否保存
        /// </summary>
        private string _Save = string.Empty;
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
        /// <summary>
        /// 样品名称
        /// </summary>
        private string _SampleName = string.Empty;
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
       
    }
}
