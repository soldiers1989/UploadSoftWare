using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Model
{
    public class clsCheckData
    {
        private string _num = string.Empty;
        /// <summary>
        /// 编号
        /// </summary>
        public string num
        {

            get { return _num; }
            set { _num = value; }
        }
        /// <summary>
        /// 检测项目
        /// </summary>

        private string _item = string.Empty;
        public string item
        {
            get { return _item; }
            set { _item = value; }
        }

        /// <summary>
        /// 样品名称
        /// </summary>
        /// 
        private string _productName = string.Empty;
        public string productName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        /// <summary>
        /// 抑制率
        /// </summary>
        private string _checkValue = string.Empty;
        public string checkValue
        {
            get { return _checkValue; }
            set { _checkValue = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        private string _unit = string.Empty;
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// 检测时间
        /// </summary>
        private string _time = string.Empty;
        public string time
        {
            get { return _time; }
            set { _time = value; }
        }

        /// <summary>
        /// 被检单位
        /// </summary>
        private string _checkedUnit = string.Empty;
        public string checkedUnit
        {
            get { return _checkedUnit; }
            set { _checkedUnit = value; }
        }
    }
}
