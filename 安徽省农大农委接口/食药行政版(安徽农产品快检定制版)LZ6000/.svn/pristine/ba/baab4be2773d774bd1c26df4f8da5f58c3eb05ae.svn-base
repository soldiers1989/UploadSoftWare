using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 引用:Newtonsoft.Json.dll
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//System.web.Extension.dll
using System.Web.Script.Serialization;

namespace FoodClient.HeFei
{
    public class clsJson
    {

        private string _rwbh = string.Empty;
        /// <summary>
        /// 任务编号
        /// </summary>
        public string rwbh
        {
            set
            {
                _rwbh = value;
            }
            get
            {
                return _rwbh;
            }
        }
        private string _bjdw = string.Empty;
        /// <summary>
        /// 被检单位
        /// </summary>
        public string bjdw
        {
            set
            {
                _bjdw = value;
            }
            get
            {
                return _bjdw;
            }
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        private string _ChkItem;
        public string ChkItem
        {
            set
            {
                _ChkItem = value;
            }
            get
            {
                return _ChkItem;
            }
        }
        //private DateTime _birthday;
        //public DateTime birthday
        //{
        //    set
        //    {
        //        _birthday = value;
        //    }
        //    get
        //    {
        //        return _birthday;
        //    }
        //}
        /// <summary>
        /// 检测时间
        /// </summary>
        private string _ChkTime = string.Empty;
        public string ChkTime
        {
            set
            {
                _ChkTime = value;
            }
            get
            {
                return _ChkTime;
            }
        }

        private string _ChkData = string.Empty;
        /// <summary>
        /// 检测值
        /// </summary>
        public string ChkData
        {
            set
            {
                _ChkData = value;
            }
            get
            {
                return _ChkData;
            }
        }

        private string _unit = string.Empty;
        /// <summary>
        /// 检测值单位
        /// </summary>
        public string unit
        {
            set
            {
                _unit = value;
            }
            get
            {
                return _unit;
            }
        }
        private string _result = string.Empty;
        /// <summary>
        /// 结论
        /// </summary>
        public string result
        {
            set
            {
                _result = value;
            }
            get
            {
                return _result;
            }
        }

        private string _samleNum= string.Empty;
        /// <summary>
        /// 样品编号
        /// </summary>
        public string samleNum
        {
            set
            {
                _samleNum = value;
            }
            get
            {
                return _samleNum;
            }
        }

        private string _samleName = string.Empty;
        /// <summary>
        /// 样品名称
        /// </summary>
        public string samleName
        {
            set
            {
                _samleName = value;
            }
            get
            {
                return _samleName;
            }
        }

        private string _samleAddress = string.Empty;
        /// <summary>
        /// 样品产地
        /// </summary>
        public string samleAddress
        {
            set
            {
                _samleAddress = value;
            }
            get
            {
                return _samleAddress;
            }
        }

        private string _ChkStandard = string.Empty;
        /// <summary>
        /// 检测标准
        /// </summary>
        public string ChkStandard
        {
            set
            {
                _ChkStandard = value;
            }
            get
            {
                return _ChkStandard;
            }
        }

        private string _ChkMachine = string.Empty;
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ChkMachine
        {
            set
            {
                _ChkMachine = value;
            }
            get
            {
                return _ChkMachine;
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

        public static string ToJson(object obj)
        {
            JavaScriptSerializer jl = new JavaScriptSerializer();
            return jl.Serialize(obj);
        }
    }
}
