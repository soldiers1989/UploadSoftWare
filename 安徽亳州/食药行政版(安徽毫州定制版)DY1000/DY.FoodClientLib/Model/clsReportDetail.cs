using System;
using System.Collections.Generic;
using System.Text;

namespace DY.FoodClientLib.Model
{
    public class clsReportDetail
    {
        public clsReportDetail() { }

        private string _SysCode = "";
        private string _CheckItem = "";
        private string _CheckBasis = "";
        private string _StandardValues = "";
        private string _CheckValues = "";
        private string _Conclusion = "";
        private string _CheckData = "";
        private string _CheckUser = "";
        private string _Unit = "";

        /// <summary>
        /// 编号
        /// </summary>
        public string SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string CheckItem
        {
            get { return _CheckItem; }
            set { _CheckItem = value; }
        }
        /// <summary>
        /// 检测依据
        /// </summary>
        public string CheckBasis
        {
            get { return _CheckBasis; }
            set { _CheckBasis = value; }
        }
        /// <summary>
        /// 标准值
        /// </summary>
        public string StandardValues
        {
            get { return _StandardValues; }
            set { _StandardValues = value; }
        }
        /// <summary>
        /// 实测值
        /// </summary>
        public string CheckValues
        {
            get { return _CheckValues; }
            set { _CheckValues = value; }
        }
        /// <summary>
        /// 结论
        /// </summary>
        public string Conclusion
        {
            get { return _Conclusion; }
            set { _Conclusion = value; }
        }
        /// <summary>
        /// 检测日期
        /// </summary>
        public string CheckData
        {
            get { return _CheckData; }
            set { _CheckData = value; }
        }

        /// <summary>
        /// 检测人
        /// </summary>
        public string CheckUser
        {
            get { return _CheckUser; }
            set { _CheckUser = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }
       
    }
}
