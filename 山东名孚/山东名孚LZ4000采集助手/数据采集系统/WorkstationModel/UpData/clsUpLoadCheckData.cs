using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationModel.UpData
{
    public class clsUpLoadCheckData
    {
        public clsUpLoadCheckData() { }

        /// <summary>
        /// 上传数据json数组（必填）
        /// </summary>
        public List<results> result;
        public class results
        {
            /// <summary>
            /// 检测唯一编号（必填）
            /// </summary>
            public string sysCode { get; set; }

            //private string _ckNo;
            ///// <summary>
            ///// 检测编号（必填）
            ///// </summary>
            //public string ckNo
            //{
            //    get { return _ckNo; }
            //    set { _ckNo = value; }
            //}

            private string _foodType = string.Empty;
            /// <summary>
            /// 样品种类（必填）
            /// </summary>
            public string foodType
            {
                get { return _foodType; }
                set { _foodType = value; }
            }

            private string _foodName = string.Empty;
            /// <summary>
            /// 样品名称（必填）
            /// </summary>
            public string foodName
            {
                get { return _foodName; }
                set { _foodName = value; }
            }

            private string _sampleNo = string.Empty;
            /// <summary>
            /// 抽样单号
            /// </summary>
            public string sampleNo
            {
                get { return _sampleNo; }
                set { _sampleNo = value; }
            }

            private string _foodCode = string.Empty;
            /// <summary>
            /// 样品编号(对应抽样单明细中的样品编号，当sampleNO有值时必填)
            /// </summary>
            public string foodCode
            {
                get { return _foodCode; }
                set { _foodCode = value; }
            }

            private string _planCode = string.Empty;
            /// <summary>
            /// 检测任务编号
            /// </summary>
            public string planCode
            {
                get { return _planCode; }
                set { _planCode = value; }
            }

            private string _checkPId = string.Empty;
            /// <summary>
            /// 检测单位（必填）(接口1的 pointId)
            /// </summary>
            public string checkPId
            {
                get { return _checkPId; }
                set { _checkPId = value; }
            }

            private string _checkDate = string.Empty;
            /// <summary>
            /// 检测时间（必填）时间戳格式
            /// </summary>
            public string checkDate
            {
                get { return _checkDate; }
                set { _checkDate = value; }
            }

            private string _checkAccord = string.Empty;
            /// <summary>
            /// 检测依据（必填）
            /// </summary>
            public string checkAccord
            {
                get { return _checkAccord; }
                set { _checkAccord = value; }
            }

            private string _checkItemName = string.Empty;
            /// <summary>
            /// 检测项目（必填）
            /// </summary>
            public string checkItemName
            {
                get { return _checkItemName; }
                set { _checkItemName = value; }
            }

            private string _checkDevice = string.Empty;
            /// <summary>
            /// 检测仪器（必填）
            /// </summary>
            public string checkDevice
            {
                get { return _checkDevice; }
                set { _checkDevice = value; }
            }

            private string _regId = string.Empty;
            /// <summary>
            /// 监管对象ID(关联被检单位的regId )
            /// </summary>
            public string regId
            {
                get { return _regId; }
                set { _regId = value; }
            }

            private string _ckcName = string.Empty;
            /// <summary>
            /// 被检单位名称
            /// </summary>
            public string ckcName
            {
                get { return _ckcName; }
                set { _ckcName = value; }
            }

            private string _ckcCode = string.Empty;
            /// <summary>
            /// 所属行政机构编号（必填）（被检单位的organizationCode）
            /// </summary>
            public string ckcCode
            {
                get { return _ckcCode; }
                set { _ckcCode = value; }
            }

            private string _cdId = string.Empty;
            /// <summary>
            /// 经营户ID (管理被检单位子集的cdId)
            /// </summary>
            public string cdId
            {
                get { return _cdId; }
                set { _cdId = value; }
            }
            /// <summary>
            /// 经营户名称
            /// </summary>
            public string cdName { get; set; }

            private string _checkResult = string.Empty;
            /// <summary>
            /// 检测结果值（必填）
            /// </summary>
            public string checkResult
            {
                get { return _checkResult; }
                set { _checkResult = value; }
            }

            private string _limitValue = string.Empty;
            /// <summary>
            /// 限定值（必填）
            /// </summary>
            public string limitValue
            {
                get { return _limitValue; }
                set { _limitValue = value; }
            }

            private string _checkConclusion = string.Empty;
            /// <summary>
            /// 检测结论（必填）
            /// </summary>
            public string checkConclusion
            {
                get { return _checkConclusion; }
                set { _checkConclusion = value; }
            }

            private string _checkUnit = string.Empty;
            /// <summary>
            /// 检测单位值（必填）
            /// </summary>
            public string checkUnit
            {
                get { return _checkUnit; }
                set { _checkUnit = value; }
            }

            private short _dataStatus;
            /// <summary>
            /// 检测数据状态（必填）
            /// </summary>
            public short dataStatus
            {
                get { return _dataStatus; }
                set { _dataStatus = value; }
            }
            /// <summary>
            /// 检测人
            /// </summary>
            public string checkUser { get; set; }
            /// <summary>
            /// 数据上传人
            /// </summary>
            public string dataUploadUser { get; set; }

            private short _dataSource;
            /// <summary>
            /// 数据来源 0检测工作站，1监管通app，3平台上传（必填）
            /// </summary>
            public short dataSource
            {
                get { return _dataSource; }
                set { _dataSource = value; }
            }
            private  string _DeviceCompany = "";
            /// <summary>
            /// 仪器生产企业
            /// </summary>
            public string deviceCompany
            {
                get { return _DeviceCompany; }
                set { _DeviceCompany = value; }
            }
            private string _deviceModel = "";
            /// <summary>
            /// 仪器型号
            /// </summary>
            public string deviceModel
            {
                get { return _deviceModel; }
                set { _deviceModel = value; }
            }
        }
    }
}
