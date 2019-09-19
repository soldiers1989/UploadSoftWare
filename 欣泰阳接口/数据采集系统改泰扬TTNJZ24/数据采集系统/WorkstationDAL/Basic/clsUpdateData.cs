using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    public class clsUpdateData
    {
        /// <summary>
        /// 检测编号
        /// </summary>
        private string _bianhao = string.Empty;
        public string bianhao
        {
            get
            {
                return _bianhao;
            }
            set
            {
                _bianhao = value;
            }

        }
        /// <summary>
        /// 检测项目
        /// </summary>
        private string _Chkxiangmu = string.Empty;
        public string Chkxiangmu
        {
            get
            {
                return _Chkxiangmu;
            }
            set
            {
                _Chkxiangmu = value;
            }

        }
        /// <summary>
        /// 检测时间
        /// </summary>
        private string _ChkTime=string.Empty ;
        public string ChkTime
        {
            get
            {
                return _ChkTime;
            }
            set
            {
                _ChkTime = value;
            }

        }
        /// <summary>
        /// 样品名称
        /// </summary>
        private  string _ChkSample = string.Empty;
        public string ChkSample
        {
            get
            {
                return _ChkSample;
            }
            set
            {
                _ChkSample = value;
            }

        }
        /// <summary>
        /// 采样时间
        /// </summary>
        private  string _GetSampTime = string.Empty;
        public string GetSampTime
        {
            get
            {
                return _GetSampTime;
            }
            set
            {
                _GetSampTime = value;
            }

        }
        /// <summary>
        ///  采样地点
        /// </summary>
        private   string _GetSampPlace = string.Empty;
        public string GetSampPlace
        {
            get
            {
                return _GetSampPlace;
            }
            set
            {
                _GetSampPlace = value;
            }

        }
        /// <su
        /// <summary>
        /// 计划编号
        /// </summary>
        private  string _plannumber = string.Empty;
        public string plannumber
        {
            get
            {
                return _plannumber;
            }
            set
            {
                _plannumber = value;
            }

        }
        /// <summary>
        /// 检测依据
        /// </summary>
        private  string _Chktestbase = string.Empty;
        public string Chktestbase
        {
            get
            {
                return _Chktestbase;
            }
            set
            {
                _Chktestbase = value;
            }

        }
        /// <summary>
        /// 限定值
        /// </summary>
        private string _ChklimitData = string.Empty;
        public string ChklimitData
        {
            get
            {
                return _ChklimitData;
            }
            set
            {
                _ChklimitData = value;
            }

        }
        /// <summary>
        /// 检测人员
        /// </summary>
        private  string _ChkPeople = string.Empty;
        public string ChkPeople
        {
            get
            {
                return _ChkPeople;
            }
            set
            {
                _ChkPeople = value;
            }
        }
        private string _ChkUnit = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
        public string ChkUnit
        {
            get
            {
                return _ChkUnit;
            }
            set
            {
                _ChkUnit = value;
            }
        }
        private string _quantityin;
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
        /// 样品数量
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
        private string _result = string.Empty;
        /// <summary>
        /// 检测结果 数据
        /// </summary>
        public string result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
        private string _unit = string.Empty;
        /// <summary>
        /// 数值单位
        /// </summary>
        public string unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }
        private string _intrument = string.Empty;
        /// <summary>
        /// 检测仪器
        /// </summary>
        public string intrument
        {
            get
            {
                return _intrument;
            }
            set
            {
                _intrument = value;
            }
        }
        private string _conclusion = string.Empty;
        /// <summary>
        /// 检测结论
        /// </summary>
        public string conclusion
        {
            get
            {
                return _conclusion;
            }
            set
            {
                _conclusion = value;
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
