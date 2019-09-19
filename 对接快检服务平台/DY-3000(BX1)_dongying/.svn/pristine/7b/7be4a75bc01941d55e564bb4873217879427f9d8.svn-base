using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    public static class DataBaseOpr
    {
        public static List<string> getSql() 
        {
            List<string> sqlList = new List<string>();
            
            sqlList.Add("create table ah_checked_unit(ID integer identity(1,1) PRIMARY KEY)");//创建表
            sqlList.Add("alter table ah_checked_unit add bussinessId varchar(255)");//新增列
            sqlList.Add("alter table ah_checked_unit add unitName varchar(255)");
            sqlList.Add("alter table ah_checked_unit add busScope varchar(255)");
            sqlList.Add("alter table ah_checked_unit add address varchar(255)");
            sqlList.Add("alter table ah_checked_unit add linkName varchar(255)");
            sqlList.Add("alter table ah_checked_unit add tel varchar(255)");
            sqlList.Add("alter table ah_checked_unit add idCard varchar(255)");
            sqlList.Add("alter table ah_checked_unit add status varchar(255)");

            sqlList.Add("create table ah_data_dictionary(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table ah_data_dictionary add name varchar(255)");
            sqlList.Add("alter table ah_data_dictionary add codeId varchar(255)");
            sqlList.Add("alter table ah_data_dictionary add typeNum varchar(255)");
            sqlList.Add("alter table ah_data_dictionary add pid varchar(255)");
            sqlList.Add("alter table ah_data_dictionary add remark varchar(255)");
            sqlList.Add("alter table ah_data_dictionary add status varchar(255)");

            sqlList.Add("create table ah_standard_limit(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table ah_standard_limit add foodType varchar(255)");
            sqlList.Add("alter table ah_standard_limit add testItem varchar(255)");
            sqlList.Add("alter table ah_standard_limit add testBasis varchar(255)");
            sqlList.Add("alter table ah_standard_limit add decisionBasis varchar(255)");
            sqlList.Add("alter table ah_standard_limit add minLimit varchar(255)");
            sqlList.Add("alter table ah_standard_limit add maxLimit varchar(255)");
            sqlList.Add("alter table ah_standard_limit add unit varchar(255)");

            sqlList.Add("create table CurveDatas(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table CurveDatas add SysCode varchar(255)");
            sqlList.Add("alter table CurveDatas add CData LongText");//长文本

            sqlList.Add("create table FoodStandard(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table FoodStandard add Name varchar(255)");
            sqlList.Add("alter table FoodStandard add ReleaseUnit varchar(255)");
            sqlList.Add("alter table FoodStandard add ImplementationTime varchar(255)");
            sqlList.Add("alter table FoodStandard add Type varchar(255)");
            sqlList.Add("alter table FoodStandard add Path varchar(255)");
            sqlList.Add("alter table FoodStandard add ReleaseTime varchar(255)");
            sqlList.Add("alter table FoodStandard add State varchar(255)");
            sqlList.Add("alter table FoodStandard add Title varchar(255)");
            sqlList.Add("alter table FoodStandard add StandardID varchar(255)");

            sqlList.Add("create table LawsAndRegulations(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table LawsAndRegulations add Name varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add Type varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add ReleaseNum varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add ReleaseTime varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add ImplementationTime varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add FailureTime varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add State varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add Path varchar(255)");
            sqlList.Add("alter table LawsAndRegulations add Notes varchar(255)");

            sqlList.Add("create table tAtp(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tAtp add ATP_CHECKNAME varchar(255)");
            sqlList.Add("alter table tAtp add ATP_RLU varchar(255)");
            sqlList.Add("alter table tAtp add ATP_RESULT varchar(255)");
            sqlList.Add("alter table tAtp add ATP_CHECKDATA varchar(255)");
            sqlList.Add("alter table tAtp add ATP_CHECKTIME varchar(255)");
            sqlList.Add("alter table tAtp add ATP_UPPER varchar(255)");
            sqlList.Add("alter table tAtp add ATP_LOWER varchar(255)");

            sqlList.Add("create table TB_SAMPLE(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table TB_SAMPLE add SAMPLENUM varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPDATE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PTYPE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SLINK varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SOURCE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPCONT varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SAMPPERSON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add FOODNAME varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BARCODE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSAMPCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BSCOMPCONT varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BRAND varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PRODATE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add MODEL varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add BATCHNUM varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add SHELFLIFE varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPANY varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPADDR varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add PROCOMPPHON varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add DEVICEID varchar(255)");
            sqlList.Add("alter table TB_SAMPLE add UDATE varchar(255)");

            //sqlList.Add("create table tCheckComType(ID integer identity(1,1) PRIMARY KEY)");
            //sqlList.Add("alter table tCheckComType add TypeName varchar(255)");
            //sqlList.Add("alter table tCheckComType add NameCall varchar(255)");
            //sqlList.Add("alter table tCheckComType add AreaCall varchar(255)");
            //sqlList.Add("alter table tCheckComType add VerType varchar(255)");
            //sqlList.Add("alter table tCheckComType add IsReadOnly bit");//bool类型
            //sqlList.Add("alter table tCheckComType add IsLock bit");
            //sqlList.Add("alter table tCheckComType add ComKind varchar(255)");
            //sqlList.Add("alter table tCheckComType add AreaTitle varchar(255)");
            //sqlList.Add("alter table tCheckComType add NameTitle varchar(255)");
            //sqlList.Add("alter table tCheckComType add DomainTitle varchar(255)");
            //sqlList.Add("alter table tCheckComType add SampleTitle varchar(255)");

            sqlList.Add("create table tCheckItem(SysCode varchar(255) PRIMARY KEY)");
            sqlList.Add("alter table tCheckItem add StdCode varchar(255)");
            sqlList.Add("alter table tCheckItem add ItemDes varchar(255)");
            sqlList.Add("alter table tCheckItem add CheckType varchar(255)");
            sqlList.Add("alter table tCheckItem add StandardCode varchar(255)");
            sqlList.Add("alter table tCheckItem add StandardValue varchar(255)");
            sqlList.Add("alter table tCheckItem add Unit varchar(255)");
            sqlList.Add("alter table tCheckItem add DemarcateInfo LongText");
            sqlList.Add("alter table tCheckItem add TestValue LongText");
            sqlList.Add("alter table tCheckItem add OperateHelp LongText");
            sqlList.Add("alter table tCheckItem add CheckLevel varchar(255)");
            sqlList.Add("alter table tCheckItem add IsReadOnly bit");
            sqlList.Add("alter table tCheckItem add IsLock bit");
            sqlList.Add("alter table tCheckItem add Remark LongText");
            sqlList.Add("alter table tCheckItem add UDate varchar(255)");

            //sqlList.Add("create table tCheckLevel(SysCode varchar(255) PRIMARY KEY)");
            //sqlList.Add("create table tCheckType(SysCode varchar(255) PRIMARY KEY)");

            sqlList.Add("create table tCompany(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tCompany add SysCode varchar(255)");
            sqlList.Add("alter table tCompany add StdCode LongText");
            sqlList.Add("alter table tCompany add CAllow varchar(255)");
            sqlList.Add("alter table tCompany add ISSUEAGENCY varchar(255)");
            sqlList.Add("alter table tCompany add ISSUEDATE varchar(255)");
            sqlList.Add("alter table tCompany add PERIODSTART varchar(255)");
            sqlList.Add("alter table tCompany add PERIODEND varchar(255)");
            sqlList.Add("alter table tCompany add VIOLATENUM varchar(255)");
            sqlList.Add("alter table tCompany add LONGITUDE varchar(255)");
            sqlList.Add("alter table tCompany add LATITUDE varchar(255)");
            sqlList.Add("alter table tCompany add SCOPE varchar(255)");
            sqlList.Add("alter table tCompany add PUNISH LongText");
            sqlList.Add("alter table tCompany add CompanyID varchar(255)");
            sqlList.Add("alter table tCompany add OtherCodeInfo LongText");
            sqlList.Add("alter table tCompany add FullName varchar(255)");
            sqlList.Add("alter table tCompany add ShortName varchar(255)");
            sqlList.Add("alter table tCompany add DisplayName varchar(255)");
            sqlList.Add("alter table tCompany add ShortCut varchar(255)");
            sqlList.Add("alter table tCompany add Property varchar(255)");
            sqlList.Add("alter table tCompany add KindCode varchar(255)");
            sqlList.Add("alter table tCompany add RegCapital Long");
            sqlList.Add("alter table tCompany add Unit varchar(255)");
            sqlList.Add("alter table tCompany add Incorporator varchar(255)");
            sqlList.Add("alter table tCompany add RegDate Time");//日期类型
            sqlList.Add("alter table tCompany add DistrictCode varchar(255)");
            sqlList.Add("alter table tCompany add Address LongText");
            sqlList.Add("alter table tCompany add PostCode varchar(255)");
            sqlList.Add("alter table tCompany add LinkMan varchar(255)");
            sqlList.Add("alter table tCompany add LinkInfo LongText");
            sqlList.Add("alter table tCompany add CreditLevel varchar(255)");
            sqlList.Add("alter table tCompany add CreditRecord LongText");
            sqlList.Add("alter table tCompany add ProductInfo LongText");
            sqlList.Add("alter table tCompany add OtherInfo LongText");
            sqlList.Add("alter table tCompany add CheckLevel varchar(255)");
            sqlList.Add("alter table tCompany add FoodSafeRecord LongText");
            sqlList.Add("alter table tCompany add IsReadOnly bit");
            sqlList.Add("alter table tCompany add IsLock bit");
            sqlList.Add("alter table tCompany add Remark LongText");
            sqlList.Add("alter table tCompany add ComProperty LongText");
            sqlList.Add("alter table tCompany add TSign varchar(255)");
            sqlList.Add("alter table tCompany add UDate varchar(255)");

            //tCompanyKind
            //tCompanyProperty
            //tCreditLevel
            //tDisposeType.
            //tDistrict

            sqlList.Add("create table tFoodClass(SysCode varchar(255) PRIMARY KEY)");
            sqlList.Add("alter table tFoodClass add StdCode varchar(255)");
            sqlList.Add("alter table tFoodClass add Name varchar(255)");
            sqlList.Add("alter table tFoodClass add ShortCut varchar(255)");
            sqlList.Add("alter table tFoodClass add CheckLevel varchar(255)");
            sqlList.Add("alter table tFoodClass add CheckItemCodes LongText");
            sqlList.Add("alter table tFoodClass add CheckItemValue LongText");
            sqlList.Add("alter table tFoodClass add IsReadOnly bit");
            sqlList.Add("alter table tFoodClass add IsLock bit");
            sqlList.Add("alter table tFoodClass add Remark LongText");
            sqlList.Add("alter table tFoodClass add FoodProperty LongText");

            sqlList.Add("create table tFoodSpecies(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tFoodSpecies add Name varchar(255)");
            sqlList.Add("alter table tFoodSpecies add PLevel Long");
            sqlList.Add("alter table tFoodSpecies add ParentName varchar(255)");
            sqlList.Add("alter table tFoodSpecies add CheckItems LongText");
            sqlList.Add("alter table tFoodSpecies add RiskLevel varchar(255)");
            sqlList.Add("alter table tFoodSpecies add Notes varchar(255)");
            sqlList.Add("alter table tFoodSpecies add Years varchar(255)");

            //tHolesSetting
            //tMachine
            //tProduceArea
            //tProprietors

            sqlList.Add("create table tReport(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tReport add CheckUnitName varchar(255)");
            sqlList.Add("alter table tReport add Trademark varchar(255)");
            sqlList.Add("alter table tReport add Specifications varchar(255)");
            sqlList.Add("alter table tReport add ProductionDate varchar(255)");
            sqlList.Add("alter table tReport add QualityGrade varchar(255)");
            sqlList.Add("alter table tReport add CheckedCompanyName varchar(255)");
            sqlList.Add("alter table tReport add CheckedCompanyPhone varchar(255)");
            sqlList.Add("alter table tReport add ProductionUnitsName varchar(255)");
            sqlList.Add("alter table tReport add ProductionUnitsPhone varchar(255)");
            sqlList.Add("alter table tReport add TaskSource varchar(255)");
            sqlList.Add("alter table tReport add Standard varchar(255)");
            sqlList.Add("alter table tReport add SamplingData varchar(255)");
            sqlList.Add("alter table tReport add SampleNum varchar(255)");
            sqlList.Add("alter table tReport add SamplingCode varchar(255)");
            sqlList.Add("alter table tReport add SampleArrivalData varchar(255)");
            sqlList.Add("alter table tReport add Notes varchar(255)");
            sqlList.Add("alter table tReport add IsDeleted varchar(255)");
            sqlList.Add("alter table tReport add CreateData varchar(255)");
            sqlList.Add("alter table tReport add Title varchar(255)");


            sqlList.Add("create table tReportDetail(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tReportDetail add ReportID Long");
            sqlList.Add("alter table tReportDetail add FoodName varchar(255)");
            sqlList.Add("alter table tReportDetail add ProjectName varchar(255)");
            sqlList.Add("alter table tReportDetail add Unit varchar(255)");
            sqlList.Add("alter table tReportDetail add CheckData varchar(255)");
            sqlList.Add("alter table tReportDetail add IsDeleted varchar(255)");

            sqlList.Add("create table tReportGS(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tReportGS add Title varchar(255)");
            sqlList.Add("alter table tReportGS add FoodName varchar(255)");
            sqlList.Add("alter table tReportGS add FoodType varchar(255)");
            sqlList.Add("alter table tReportGS add ProductionDate varchar(255)");
            sqlList.Add("alter table tReportGS add CheckedCompanyName varchar(255)");
            sqlList.Add("alter table tReportGS add CheckedCompanyAddress varchar(255)");
            sqlList.Add("alter table tReportGS add CheckedCompanyPhone varchar(255)");
            sqlList.Add("alter table tReportGS add LabelProducerName varchar(255)");
            sqlList.Add("alter table tReportGS add LabelProducerAddress varchar(255)");
            sqlList.Add("alter table tReportGS add LabelProducerPhone varchar(255)");
            sqlList.Add("alter table tReportGS add SamplingData varchar(255)");
            sqlList.Add("alter table tReportGS add SamplingPerson varchar(255)");
            sqlList.Add("alter table tReportGS add SampleNum varchar(255)");
            sqlList.Add("alter table tReportGS add SamplingBase varchar(255)");
            sqlList.Add("alter table tReportGS add SamplingAddress varchar(255)");
            sqlList.Add("alter table tReportGS add SamplingOrderCode varchar(255)");
            sqlList.Add("alter table tReportGS add Standard varchar(255)");
            sqlList.Add("alter table tReportGS add InspectionConclusion varchar(255)");
            sqlList.Add("alter table tReportGS add Notes varchar(255)");
            sqlList.Add("alter table tReportGS add Audit varchar(255)");
            sqlList.Add("alter table tReportGS add Surveyor varchar(255)");

            sqlList.Add("create table tReportGSDetail(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table tReportGSDetail add ReportGSID Long");
            sqlList.Add("alter table tReportGSDetail add ProjectName varchar(255)");
            sqlList.Add("alter table tReportGSDetail add Unit varchar(255)");
            sqlList.Add("alter table tReportGSDetail add InspectionStandard varchar(255)");
            sqlList.Add("alter table tReportGSDetail add IndividualResults varchar(255)");
            sqlList.Add("alter table tReportGSDetail add IndividualDecision varchar(255)");

            //tResult
            //tSaleRecord

            sqlList.Add("create table tStandard(SysCode varchar(255) PRIMARY KEY)");
            sqlList.Add("alter table tStandard add StdCode varchar(255)");
            sqlList.Add("alter table tStandard add StdDes varchar(255)");
            sqlList.Add("alter table tStandard add ShortCut varchar(255)");
            sqlList.Add("alter table tStandard add StdInfo LongText");
            sqlList.Add("alter table tStandard add StdType varchar(255)");
            sqlList.Add("alter table tStandard add LawsRegulations LongText");
            sqlList.Add("alter table tStandard add IsReadOnly bit");
            sqlList.Add("alter table tStandard add IsLock bit");
            sqlList.Add("alter table tStandard add Remark LongText");
            sqlList.Add("alter table tStandard add UDate varchar(255)");

            //tStandardType
            //tStockRecord
            //tSysOpt
            //tTask
            sqlList.Add("create table tTask(CPCODE varchar(255) PRIMARY KEY)");
            sqlList.Add("alter table tTask add CPTITLE LongText");
            sqlList.Add("alter table tTask add CPSDATE varchar(255)");
            sqlList.Add("alter table tTask add CPEDATE varchar(255)");
            sqlList.Add("alter table tTask add CPTPROPERTY varchar(255)");
            sqlList.Add("alter table tTask add CPFROM varchar(255)");
            sqlList.Add("alter table tTask add CPEDITOR varchar(255)");
            sqlList.Add("alter table tTask add CPPORGID LongText");
            sqlList.Add("alter table tTask add CPPORG LongText");
            sqlList.Add("alter table tTask add CPEDDATE varchar(255)");
            sqlList.Add("alter table tTask add CPMEMO LongText");
            sqlList.Add("alter table tTask add PLANDETAIL LongText");
            sqlList.Add("alter table tTask add PLANDCOUNT varchar(255)");
            sqlList.Add("alter table tTask add CompleteNum Long");
            sqlList.Add("alter table tTask add BAOJINGTIME varchar(255)");
            sqlList.Add("alter table tTask add UDate varchar(255)");

            sqlList.Add("create table ttResultSecond(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table ttResultSecond add SysCode varchar(255)");
            sqlList.Add("alter table ttResultSecond add ResultType varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckNo varchar(255)");
            sqlList.Add("alter table ttResultSecond add SampleCode varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckPlaceCode varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckPlace varchar(255)");
            sqlList.Add("alter table ttResultSecond add FoodType varchar(255)");
            sqlList.Add("alter table ttResultSecond add FoodName varchar(255)");
            sqlList.Add("alter table ttResultSecond add TakeDate varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckStartDate varchar(255)");
            sqlList.Add("alter table ttResultSecond add Standard varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckMachine varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckMachineModel varchar(255)");
            sqlList.Add("alter table ttResultSecond add MachineCompany varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckTotalItem varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckValueInfo varchar(255)");
            sqlList.Add("alter table ttResultSecond add StandValue varchar(255)");
            sqlList.Add("alter table ttResultSecond add Result varchar(255)");
            sqlList.Add("alter table ttResultSecond add ResultInfo varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckUnitName varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckUnitInfo varchar(255)");
            sqlList.Add("alter table ttResultSecond add Organizer varchar(255)");
            sqlList.Add("alter table ttResultSecond add UpLoader varchar(255)");
            sqlList.Add("alter table ttResultSecond add ReportDeliTime varchar(255)");
            sqlList.Add("alter table ttResultSecond add IsReconsider bit");
            sqlList.Add("alter table ttResultSecond add ReconsiderTime varchar(255)");
            sqlList.Add("alter table ttResultSecond add ProceResults varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckedCompanyCode varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckedCompany varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckedComDis varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckPlanCode varchar(255)");
            sqlList.Add("alter table ttResultSecond add DateManufacture varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckMethod varchar(255)");
            sqlList.Add("alter table ttResultSecond add APRACategory varchar(255)");
            sqlList.Add("alter table ttResultSecond add Hole varchar(255)");
            sqlList.Add("alter table ttResultSecond add SamplingPlace varchar(255)");
            sqlList.Add("alter table ttResultSecond add CheckType varchar(255)");
            sqlList.Add("alter table ttResultSecond add IsUpload varchar(255)");
            sqlList.Add("alter table ttResultSecond add IsShow varchar(255)");
            sqlList.Add("alter table ttResultSecond add IsReport varchar(255)");
            sqlList.Add("alter table ttResultSecond add ContrastValue varchar(255)");
            sqlList.Add("alter table ttResultSecond add CKCKNAMEUSID varchar(255)");
            sqlList.Add("alter table ttResultSecond add fTpye varchar(255)");
            sqlList.Add("alter table ttResultSecond add testPro varchar(255)");
            sqlList.Add("alter table ttResultSecond add quanOrQual varchar(255)");
            sqlList.Add("alter table ttResultSecond add dataNum varchar(255)");
            sqlList.Add("alter table ttResultSecond add checkedUnit varchar(255)");
            sqlList.Add("alter table ttResultSecond add DeviceId varchar(255)");
            sqlList.Add("alter table ttResultSecond add SampleId varchar(255)");
            sqlList.Add("alter table ttResultSecond add ProduceCompany varchar(255)");
            
            sqlList.Add("create table ttStandDecide(ID integer identity(1,1) PRIMARY KEY)");
            sqlList.Add("alter table ttStandDecide add FtypeNmae varchar(255)");
            sqlList.Add("alter table ttStandDecide add SampleNum varchar(255)");
            sqlList.Add("alter table ttStandDecide add Name varchar(255)");
            sqlList.Add("alter table ttStandDecide add ItemDes varchar(255)");
            sqlList.Add("alter table ttStandDecide add StandardValue varchar(255)");
            sqlList.Add("alter table ttStandDecide add Demarcate varchar(255)");
            sqlList.Add("alter table ttStandDecide add Unit varchar(255)");
            sqlList.Add("alter table ttStandDecide add SaveType varchar(255)");
            sqlList.Add("alter table ttStandDecide add UDate varchar(255)");

            //tUserInfo
            //tUserUnit
            return sqlList;
        }
    }
}
