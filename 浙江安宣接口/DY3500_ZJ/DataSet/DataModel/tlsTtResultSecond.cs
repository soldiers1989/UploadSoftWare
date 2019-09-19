﻿
namespace DYSeriesDataSet
{
    public class tlsTtResultSecond
    {
        public tlsTtResultSecond()
        {

        }

        #region Field Members

        private int m_Id = 0;
        private string m_SysCode = string.Empty;
        /// <summary>
        /// 检测手段
        /// </summary>
        private string m_ResultType = string.Empty;
        /// <summary>
        /// 检测编号
        /// </summary>
        private string m_CheckNo = string.Empty;
        /// <summary>
        /// 样品编号
        /// </summary>
        private string m_SampleCode = string.Empty;
        /// <summary>
        /// 行政机构编号
        /// </summary>
        private string m_CheckPlaceCode = string.Empty;
        /// <summary>
        /// 行政机构名称
        /// </summary>
        private string m_CheckPlace = string.Empty;
        /// <summary>
        /// 被检样品名称
        /// </summary>
        private string m_FoodName = string.Empty;
        private string m_FoodType = string.Empty;
        /// <summary>
        /// 抽检日期
        /// </summary>
        private string m_TakeDate = string.Empty;
        /// <summary>
        /// 检测时间
        /// </summary>
        private string m_CheckStartDate = string.Empty;
        /// <summary>
        /// 检测依据
        /// </summary>
        private string m_Standard = string.Empty;
        /// <summary>
        /// 检测设备
        /// </summary>
        private string m_CheckMachine = string.Empty;
        /// <summary>
        /// 检测设备型号
        /// </summary>
        private string m_CheckMachineModel = string.Empty;
        /// <summary>
        /// 检测设备生产厂家
        /// </summary>
        private string m_MachineCompany = string.Empty;
        /// <summary>
        /// 检测项目
        /// </summary>
        private string m_CheckTotalItem = string.Empty;
        /// <summary>
        /// 检测值
        /// </summary>
        private string m_CheckValueInfo = string.Empty;
        /// <summary>
        /// 检测标准值
        /// </summary>
        private string m_StandValue = string.Empty;
        /// <summary>
        /// 检测结论 （合格、不合格）
        /// </summary>
        private string m_Result = string.Empty;
        /// <summary>
        /// 检测值单位
        /// </summary>
        private string m_ResultInfo = string.Empty;
        /// <summary>
        /// 检测单位
        /// </summary>
        private string m_CheckUnitName = string.Empty;
        /// <summary>
        /// 检测人姓名
        /// </summary>
        private string m_CheckUnitInfo = string.Empty;
        /// <summary>
        /// 抽样人
        /// </summary>
        private string m_Organizer = string.Empty;
        /// <summary>
        /// 基层上传人
        /// </summary>
        private string m_UpLoader = string.Empty;
        /// <summary>
        /// 检测报告送达时间
        /// </summary>
        private string m_ReportDeliTime = string.Empty;
        /// <summary>
        /// 是否提出复议(是、否)
        /// </summary>
        private bool m_IsReconsider = false;
        /// <summary>
        /// 提出复议时间
        /// </summary>
        private string m_ReconsiderTime = string.Empty;
        /// <summary>
        /// 处理结果
        /// </summary>
        private string m_ProceResults = string.Empty;
        /// <summary>
        /// 被检对象编号
        /// </summary>
        private string m_CheckedCompanyCode = string.Empty;
        /// <summary>
        /// 被检对象名称
        /// </summary>
        private string m_CheckedCompany = string.Empty;
        /// <summary>
        /// 档口/车牌号/门牌号
        /// </summary>
        private string m_CheckedComDis = string.Empty;
        /// <summary>
        /// 任务编号
        /// </summary>
        private string m_TaskNumber = string.Empty;
        /// <summary>
        /// 生产单位
        /// </summary>
        private string m_ProduceCompany = string.Empty;
        /// <summary>
        /// 生产日期
        /// </summary>
        private string m_DateManufacture = string.Empty;
        /// <summary>
        /// 检测孔
        /// </summary>
        private string m_Hole = string.Empty;
        /// <summary>
        /// 检测方法
        /// </summary>
        private string m_CheckMethod = string.Empty;
        /// <summary>
        /// 单位类别
        /// </summary>
        private string m_APRACategory = string.Empty;
        /// <summary>
        /// 抽样地点
        /// </summary>
        private string m_SamplingPlace = string.Empty;
        /// <summary>
        /// 检测类型
        /// </summary>
        private string m_CheckType = string.Empty;
        /// <summary>
        /// 是否上传
        /// </summary>
        private string m_IsUpload = string.Empty;
        /// <summary>
        /// 是否展示
        /// </summary>
        private string m_IsShow = string.Empty;
        /// <summary>
        /// 是否已生成报表
        /// </summary>
        private string m_IsReport = string.Empty;
        /// <summary>
        /// 对照值
        /// </summary>
        private string m_ContrastValue = string.Empty;
        /// <summary>
        /// 唯一机器码
        /// </summary>
        private string m_DeviceId = string.Empty;
        /// <summary>
        /// 快检单号
        /// </summary>
        private string m_SampleId = string.Empty;

        private string m_CKCKNAMEUSID = string.Empty;
        /// <summary>
        /// 浙江检定项目编号
        /// </summary>
        private string m_ItemCode= string.Empty;

        #endregion

        #region Property Members

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
            }
        }

        /// <summary>
        /// 检测类型（抽检、送检等）
        /// </summary>
        public string CheckType
        {
            get
            {
                return m_CheckType;
            }
            set
            {
                m_CheckType = value;
            }
        }
        /// <summary>
        /// 抽样地点
        /// </summary>
        public string SamplingPlace
        {
            get
            {
                return m_SamplingPlace;
            }
            set
            {
                m_SamplingPlace = value;
            }
        }
        /// <summary>
        /// 单位类别
        /// </summary>
        public string APRACategory
        {
            get
            {
                return m_APRACategory;
            }
            set
            {
                m_APRACategory = value;
            }
        }
        /// <summary>
        /// 检测方法
        /// </summary>
        public string CheckMethod
        {
            get
            {
                return m_CheckMethod;
            }
            set
            {
                m_CheckMethod = value;
            }
        }
        /// <summary>
        /// 检测孔
        /// </summary>
        public string Hole
        {
            get
            {
                return m_Hole;
            }
            set
            {
                m_Hole = value;
            }
        }

        /// <summary>
        /// 检测手段
        /// </summary>
        public string ResultType
        {
            get
            {
                return m_ResultType;
            }
            set
            {
                m_ResultType = value;
            }
        }
        /// <summary>
        /// *检测编号
        /// </summary>
        public string CheckNo
        {
            get
            {
                return m_CheckNo;
            }
            set
            {
                m_CheckNo = value;
            }
        }
        /// <summary>
        /// *样品编号
        /// </summary>
        public string SampleCode
        {
            get
            {
                return m_SampleCode;
            }
            set
            {
                m_SampleCode = value;
            }
        }
        /// <summary>
        /// *检测单位所属行政机构编号
        /// </summary>
        public string CheckPlaceCode
        {
            get
            {
                return m_CheckPlaceCode;
            }
            set
            {
                m_CheckPlaceCode = value;
            }
        }
        /// <summary>
        /// *检测单位所属行政机构名称
        /// </summary>
        public string CheckPlace
        {
            get
            {
                return m_CheckPlace;
            }
            set
            {
                m_CheckPlace = value;
            }
        }
        /// <summary>
        /// *被检样品名称
        /// </summary>
        public string FoodName
        {
            get
            {
                return m_FoodName;
            }
            set
            {
                m_FoodName = value;
            }
        }
        /// <summary>
        /// 被检样品种类
        /// </summary>
        public string FoodType
        {
            get { return m_FoodType.Length == 0 ? "样品" : m_FoodType; }
            set { m_FoodType = value; }
        }
        /// <summary>
        /// *抽检日期 必填，格式为：2016-07-04
        /// </summary>
        public string TakeDate
        {
            get
            {
                return m_TakeDate;
            }
            set
            {
                m_TakeDate = value;
            }
        }
        /// <summary>
        /// 检测时间
        /// </summary>
        public string CheckStartDate
        {
            get
            {
                return m_CheckStartDate;
            }
            set
            {
                m_CheckStartDate = value;
            }
        }
        /// <summary>
        /// *检测依据
        /// </summary>
        public string Standard
        {
            get
            {
                return m_Standard;
            }
            set
            {
                m_Standard = value;
            }
        }
        /// <summary>
        /// 检测设备
        /// </summary>
        public string CheckMachine
        {
            get
            {
                return m_CheckMachine;
            }
            set
            {
                m_CheckMachine = value;
            }
        }
        /// <summary>
        /// 检测设备型号
        /// </summary>
        public string CheckMachineModel
        {
            get
            {
                return m_CheckMachineModel;
            }
            set
            {
                m_CheckMachineModel = value;
            }
        }
        /// <summary>
        /// 检测设备生产厂家
        /// </summary>
        public string MachineCompany
        {
            get
            {
                return m_MachineCompany;
            }
            set
            {
                m_MachineCompany = value;
            }
        }
        /// <summary>
        /// *检测项目
        /// </summary>
        public string CheckTotalItem
        {
            get
            {
                return m_CheckTotalItem;
            }
            set
            {
                m_CheckTotalItem = value;
            }
        }
        /// <summary>
        /// *检测值
        /// </summary>
        public string CheckValueInfo
        {
            get
            {
                return m_CheckValueInfo;
            }
            set
            {
                m_CheckValueInfo = value;
            }
        }
        /// <summary>
        /// *检测标准值
        /// </summary>
        public string StandValue
        {
            get
            {
                return m_StandValue;
            }
            set
            {
                m_StandValue = value;
            }
        }
        /// <summary>
        /// *检测结论 （合格、不合格）
        /// </summary>
        public string Result
        {
            get
            {
                return m_Result;
            }
            set
            {
                m_Result = value;
            }
        }
        /// <summary>
        /// *检测值单位
        /// </summary>
        public string ResultInfo
        {
            get
            {
                return m_ResultInfo;
            }
            set
            {
                m_ResultInfo = value;
            }
        }
        /// <summary>
        /// *检测单位名称
        /// </summary>
        public string CheckUnitName
        {
            get
            {
                return m_CheckUnitName;
            }
            set
            {
                m_CheckUnitName = value;
            }
        }
        /// <summary>
        /// 检测人姓名
        /// </summary>
        public string CheckUnitInfo
        {
            get
            {
                return m_CheckUnitInfo;
            }
            set
            {
                m_CheckUnitInfo = value;
            }
        }
        /// <summary>
        /// 抽样人
        /// </summary>
        public string Organizer
        {
            get
            {
                return m_Organizer;
            }
            set
            {
                m_Organizer = value;
            }
        }
        /// <summary>
        /// *基层上传人
        /// </summary>
        public string UpLoader
        {
            get
            {
                return m_UpLoader;
            }
            set
            {
                m_UpLoader = value;
            }
        }
        /// <summary>
        /// 检测报告送达时间
        /// </summary>
        public string ReportDeliTime
        {
            get
            {
                return m_ReportDeliTime;
            }
            set
            {
                m_ReportDeliTime = value;
            }
        }
        /// <summary>
        /// 是否提出复议(是、否)
        /// </summary>
        public bool IsReconsider
        {
            get
            {
                return m_IsReconsider;
            }
            set
            {
                m_IsReconsider = value;
            }
        }
        /// <summary>
        /// 提出复议时间
        /// </summary>
        public string ReconsiderTime
        {
            get
            {
                return m_ReconsiderTime;
            }
            set
            {
                m_ReconsiderTime = value;
            }
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string ProceResults
        {
            get
            {
                return m_ProceResults;
            }
            set
            {
                m_ProceResults = value;
            }
        }
        /// <summary>
        /// 被检对象编号
        /// </summary>
        public string CheckedCompanyCode
        {
            get
            {
                return m_CheckedCompanyCode;
            }
            set
            {
                m_CheckedCompanyCode = value;
            }
        }
        /// <summary>
        /// *被检对象名称
        /// </summary>
        public string CheckedCompany
        {
            get
            {
                return m_CheckedCompany;
            }
            set
            {
                m_CheckedCompany = value;
            }
        }
        /// <summary>
        /// 档口/车牌号/门牌号
        /// </summary>
        public string CheckedComDis
        {
            get
            {
                return m_CheckedComDis;
            }
            set
            {
                m_CheckedComDis = value;
            }
        }
        /// <summary>
        /// 任务编号?行政机构编号
        /// </summary>
        public string CheckPlanCode
        {
            get
            {
                return m_TaskNumber;
            }
            set
            {
                m_TaskNumber = value;
            }
        }
        /// <summary>
        /// 生产单位
        /// </summary>
        public string ProduceCompany
        {
            get
            {
                return m_ProduceCompany;
            }
            set
            {
                m_ProduceCompany = value;
            }
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string DateManufacture
        {
            get
            {
                return m_DateManufacture;
            }
            set
            {
                m_DateManufacture = value;
            }
        }
        /// <summary>
        /// 是否上传 用于颜色标识
        /// </summary>
        public string IsUpload
        {
            get
            {
                if (m_IsUpload.Equals("Y"))
                {
                    return "已上传";
                }
                else if (m_IsUpload.Equals("已上传"))
                {
                    return "Y";
                }
                else if (m_IsUpload.Equals("S"))
                {
                    return "上传后有修改";
                }
                else if (m_IsUpload.Equals("已上传"))
                {
                    return "S";
                }
                else if (m_IsUpload.Equals("N"))
                {
                    return string.Empty;
                }
                else
                {
                    return "N";
                }
            }
            set
            {
                m_IsUpload = value;
            }
        }

        /// <summary>
        /// 是否展示
        /// </summary>
        public string IsShow
        {
            get
            {
                if (m_IsShow.Equals("Y"))
                {
                    return "已展示";
                }
                else if (m_IsShow.Equals("已展示"))
                {
                    return "Y";
                }
                else if (m_IsShow.Equals("N"))
                {
                    return string.Empty;
                }
                else
                {
                    return "N";
                }
            }
            set
            {
                m_IsShow = value;
            }
        }

        /// <summary>
        /// 是否已生成报表
        /// </summary>
        public string IsReport
        {
            get
            {
                if (m_IsReport.Equals("Y"))
                {
                    return "已生成";
                }
                else if (m_IsReport.Equals("已生成"))
                {
                    return "Y";
                }
                else if (m_IsReport.Equals("N"))
                {
                    return string.Empty;
                }
                else
                {
                    return "N";
                }
            }
            set
            {
                m_IsReport = value;
            }
        }
        /// <summary>
        /// 对照值
        /// </summary>
        public string ContrastValue
        {
            get
            {
                return m_ContrastValue;
            }
            set
            {
                m_ContrastValue = value;
            }
        }

        /// <summary>
        /// 唯一设备码
        /// </summary>
        public string DeviceId
        {
            get
            {
                return m_DeviceId;
            }
            set
            {
                m_DeviceId = value;
            }
        }

        /// <summary>
        /// 快检单号
        /// </summary>
        public string SampleId
        {
            get
            {
                return m_SampleId;
            }
            set
            {
                m_SampleId = value;
            }
        }

        /// <summary>
        /// *UUID 上传至监管平台标识唯一
        /// </summary>
        public string SysCode
        {
            get { return m_SysCode; }
            set { m_SysCode = value; }
        }

        /// <summary>
        /// *检测人USID
        /// </summary> 
        public string CKCKNAMEUSID
        {
            get { return m_CKCKNAMEUSID; }
            set { m_CKCKNAMEUSID = value; }
        }
        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string ItemCode
        {
            get { return m_ItemCode; }
            set { m_ItemCode = value; }
        }


        #endregion
    }
}
