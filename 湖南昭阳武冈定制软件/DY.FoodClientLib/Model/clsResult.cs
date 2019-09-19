using System;

namespace DY.FoodClientLib
{
	/// <summary>
	///检测记录实体类
    ///原作者没有任何注解
    ///修改：陈国利 2011-06-23
	/// </summary>
    public class clsResult
    {
        public clsResult()
        {
        }
        private string _SysCode;
        private string _ResultType;
        private string _CheckNo;
        private string _StdCode;
        private string _FoodCode;
        private string _CheckPlaceCode;
        private string _CheckedCompany;
        private string _CheckedCompanyName;
        private string _CheckedComDis;
        private string _SampleUnit;
        private string _SampleCode;
        private string _ImportNum;
        private string _SaveNum;
        private DateTime? _ProduceDate;
        private DateTime _TakeDate;
        private DateTime _CheckStartDate;
        private DateTime _CheckEndDate;
        private string _SampleBaseNum;
        private string _SampleNum;
        private string _Unit;
        private string _SampleLevel;
        private string _SampleModel;
        private string _SampleState;
        private string _SentCompany;
        private string _Provider;
        private string _Standard;
        private string _CheckMachine;
        private string _CheckTotalItem;
        private string _CheckValueInfo;
        private string _Result;
        private string _UpperCompany;
        private string _UpperCompanyName;
        private string _OrCheckNo;
        private string _CheckType;
        private string _CheckUnitCode;
        private string _ProduceCompany;
        private string _Checker;
        private string _Assessor;
        private string _Organizer;
        private bool _IsSended;
        private DateTime _SendedDate;
        private string _Sender;
        private string _Remark;
        private string _ProducePlace;
        private string _StandValue;
        private string _ResultInfo;
        private string _CheckPlanCode;
        private string _SaleNum;
        private string _Price;
        private string _CheckederVal;
        private string _IsSentCheck;
        private string _CheckederRemark;
        private bool _IsReSended;
        private string _Notes;

        private string _holesNum;
        private string _machineSampleNum;
        private string _machineItemName;

        /// <summary>
        /// 孔位，新增字段
        /// </summary>
        public string HolesNum
        {
            get { return _holesNum; }
            set { _holesNum = value; }
        }

        /// <summary>
        /// 检测仪器编号/样本号
        /// </summary>
        public string MachineSampleNum
        {
            get { return _machineSampleNum; }
            set { _machineSampleNum = value; }
        }
        /// <summary>
        /// 仪器检测项目名称
        /// </summary>
        public string MachineItemName
        {
            get { return _machineItemName; }
            set { _machineItemName = value; }
        }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string SysCode
        {
            set
            {
                _SysCode = value;
            }
            get
            {
                return _SysCode;
            }
        }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string ResultType
        {
            set
            {
                _ResultType = value;
            }
            get
            {
                return _ResultType;
            }
        }

        /// <summary>
        /// 检测编码
        /// </summary>
        public string CheckNo
        {
            set
            {
                _CheckNo = value;
            }
            get
            {
                return _CheckNo;
            }
        }
        /// <summary>
        /// 条型码
        /// </summary>
        public string StdCode
        {
            set
            {
                _StdCode = value;
            }
            get
            {
                return _StdCode;
            }
        }
        /// <summary>
        /// 食品编码
        /// </summary>
        public string FoodCode
        {
            set
            {
                _FoodCode = value;
            }
            get
            {
                return _FoodCode;
            }
        }
        /// <summary>
        /// 检测点代码
        /// </summary>
        public string CheckPlaceCode
        {
            set
            {
                _CheckPlaceCode = value;
            }
            get
            {
                return _CheckPlaceCode;
            }
        }
        /// <summary>
        /// 检测单位
        /// </summary>
        public string CheckedCompany
        {
            set
            {
                _CheckedCompany = value;
            }
            get
            {
                return _CheckedCompany;
            }
        }
        /// <summary>
        /// 检测单位名称
        /// </summary>
        public string CheckedCompanyName
        {
            set
            {
                _CheckedCompanyName = value;
            }
            get
            {
                return _CheckedCompanyName;
            }
        }
        /// <summary>
        /// 检测单位描述
        /// </summary>
        public string CheckedComDis
        {
            set
            {
                _CheckedComDis = value;
            }
            get
            {
                return _CheckedComDis;
            }
        }
        /// <summary>
        ///  样品单位
        /// </summary>
        public string SampleUnit
        {
            set
            {
                _SampleUnit = value;
            }
            get
            {
                return _SampleUnit;
            }
        }
        /// <summary>
        /// 样品编码 
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
        /// <summary>
        /// 
        /// </summary>
        public string ImportNum
        {
            set
            {
                _ImportNum = value;
            }
            get
            {
                return _ImportNum;
            }
        }
        public string SaveNum
        {
            set
            {
                _SaveNum = value;
            }
            get
            {
                return _SaveNum;
            }
        }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? ProduceDate
        {
            set
            {
                _ProduceDate = value;
            }
            get
            {
                return _ProduceDate;
            }
        }
        /// <summary>
        /// 检测日期 
        /// </summary>
        public DateTime TakeDate
        {
            set
            {
                _TakeDate = value;
            }
            get
            {
                return _TakeDate;
            }
        }
        /// <summary>
        /// 检测开始日期
        /// </summary>
        public DateTime CheckStartDate
        {
            set
            {
                _CheckStartDate = value;
            }
            get
            {
                return _CheckStartDate;
            }
        }
        /// <summary>
        /// 检测结束时期
        /// </summary>
        public DateTime CheckEndDate
        {
            set
            {
                _CheckEndDate = value;
            }
            get
            {
                return _CheckEndDate;
            }
        }
        /// <summary>
        /// 样品抽检基数
        /// </summary>
        public string SampleBaseNum
        {
            set
            {
                _SampleBaseNum = value;
            }
            get
            {
                return _SampleBaseNum;
            }
        }
        /// <summary>
        /// 样品数量
        /// </summary>
        public string SampleNum
        {
            set
            {
                _SampleNum = value;
            }
            get
            {
                return _SampleNum;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
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
        /// <summary>
        /// 样品水平
        /// </summary>
        public string SampleLevel
        {
            set
            {
                _SampleLevel = value;
            }
            get
            {
                return _SampleLevel;
            }
        }
        /// <summary>
        /// 样品规格
        /// </summary>
        public string SampleModel
        {
            set
            {
                _SampleModel = value;
            }
            get
            {
                return _SampleModel;
            }
        }
        /// <summary>
        /// 样品状态
        /// </summary>
        public string SampleState
        {
            set
            {
                _SampleState = value;
            }
            get
            {
                return _SampleState;
            }
        }
        public string SentCompany
        {
            set
            {
                _SentCompany = value;
            }
            get
            {
                return _SentCompany;
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Provider
        {
            set
            {
                _Provider = value;
            }
            get
            {
                return _Provider;
            }
        }

        /// <summary>
        /// 标准
        /// </summary>
        public string Standard
        {
            set
            {
                _Standard = value;
            }
            get
            {
                return _Standard;
            }
        }
        /// <summary>
        /// 检测仪器
        /// </summary>
        public string CheckMachine
        {
            set
            {
                _CheckMachine = value;
            }
            get
            {
                return _CheckMachine;
            }
        }
        /// <summary>
        /// 检测项目编码
        /// </summary>
        public string CheckTotalItem
        {
            set
            {
                _CheckTotalItem = value;
            }
            get
            {
                return _CheckTotalItem;
            }
        }
        /// <summary>
        /// 检测值信息
        /// </summary>
        public string CheckValueInfo
        {
            set
            {
                _CheckValueInfo = value;
            }
            get
            {
                return _CheckValueInfo;
            }
        }

        /// <summary>
        /// 检测结果
        /// </summary>
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
        public string UpperCompany
        {
            set
            {
                _UpperCompany = value;
            }
            get
            {
                return _UpperCompany;
            }
        }
        public string UpperCompanyName
        {
            set
            {
                _UpperCompanyName = value;
            }
            get
            {
                return _UpperCompanyName;
            }
        }
        public string OrCheckNo
        {
            set
            {
                _OrCheckNo = value;
            }
            get
            {
                return _OrCheckNo;
            }
        }
        public string CheckType
        {
            set
            {
                _CheckType = value;
            }
            get
            {
                return _CheckType;
            }
        }
        public string CheckUnitCode
        {
            set
            {
                _CheckUnitCode = value;
            }
            get
            {
                return _CheckUnitCode;
            }
        }
        /// <summary>
        /// 检测人
        /// </summary>
        public string Checker
        {
            set
            {
                _Checker = value;
            }
            get
            {
                return _Checker;
            }
        }
        /// <summary>
        /// 生产单位
        /// </summary>
        public string ProduceCompany
        {
            set
            {
                _ProduceCompany = value;
            }
            get
            {
                return _ProduceCompany;
            }
        }
        /// <summary>
        /// 核准人
        /// </summary>
        public string Assessor
        {
            set
            {
                _Assessor = value;
            }
            get
            {
                return _Assessor;
            }
        }
        /// <summary>
        /// 抽验人
        /// </summary>
        public string Organizer
        {
            set
            {
                _Organizer = value;
            }
            get
            {
                return _Organizer;
            }
        }
        /// <summary>
        /// 是否上传
        /// </summary>
        public bool IsSended
        {
            set
            {
                _IsSended = value;
            }
            get
            {
                return _IsSended;
            }
        }
        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime SendedDate
        {
            set
            {
                _SendedDate = value;
            }
            get
            {
                return _SendedDate;
            }
        }
        /// <summary>
        /// 上传人
        /// </summary>
        public string Sender
        {
            set
            {
                _Sender = value;
            }
            get
            {
                return _Sender;
            }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
            }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string ProducePlace
        {
            set
            {
                _ProducePlace = value;
            }
            get
            {
                return _ProducePlace;
            }
        }
        /// <summary>
        /// 标准值
        /// </summary>
        public string StandValue
        {
            set
            {
                _StandValue = value;
            }
            get
            {
                return _StandValue;
            }
        }

        /// <summary>
        /// 结果其他信息
        /// </summary>
        public string ResultInfo
        {
            set
            {
                _ResultInfo = value;
            }
            get
            {
                return _ResultInfo;
            }
        }
        /// <summary>
        /// 检测计划编码
        /// </summary>
        public string CheckPlanCode
        {
            set
            {
                _CheckPlanCode = value;
            }
            get
            {
                return _CheckPlanCode;
            }
        }
        /// <summary>
        /// 销售数量
        /// </summary>
        public string SaleNum
        {
            set
            {
                _SaleNum = value;
            }
            get
            {
                return _SaleNum;
            }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price
        {
            set
            {
                _Price = value;
            }
            get
            {
                return _Price;
            }
        }
        /// <summary>
        /// 检测值
        /// </summary>
        public string CheckederVal
        {
            set
            {
                _CheckederVal = value;
            }
            get
            {
                return _CheckederVal;
            }
        }
        public string IsSentCheck
        {
            set
            {
                _IsSentCheck = value;
            }
            get
            {
                return _IsSentCheck;
            }
        }
        /// <summary>
        /// 检测人备注
        /// </summary>
        public string CheckederRemark
        {
            set
            {
                _CheckederRemark = value;
            }
            get
            {
                return _CheckederRemark;
            }
        }
        public string Notes
        {
            set
            {
                _Notes = value;
            }
            get
            {
                return _Notes;
            }
        }
        /// <summary>
        /// 是否已经重发
        /// </summary>
        public bool IsReSended
        {
            set
            {
                _IsReSended = value;
            }
            get
            {
                return _IsReSended;
            }
        }
    }
}
