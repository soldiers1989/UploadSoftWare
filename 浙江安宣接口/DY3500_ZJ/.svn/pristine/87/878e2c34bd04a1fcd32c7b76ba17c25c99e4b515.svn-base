using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using DYSeriesDataSet.DataModel;

namespace DYSeriesDataSet
{
    public class tlsttResultSecondOpr
    {
        public tlsttResultSecondOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 根据检测编码删除检测记录
        /// </summary>
        /// <param name="CheckNo">检测编码</param>
        /// <param name="delCount">删除总数</param>
        /// <returns></returns>
        public int Deleted(int CheckNo)
        {
            int retn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM ttResultSecond Where ID = " + CheckNo);
                DataBase.ExecuteCommand(sb.ToString());
                sb.Length = 0;
                retn = 1;
            }
            catch (Exception)
            {
                retn = 0;
            }
            return retn;
        }

        public int InsertSysCode(string syscode, int ID, out string errMsg)
        {
            int rtn = sb.Length = 0;
            errMsg = string.Empty;

            try
            {
                sb.Append(string.Format("UPDATE ttResultSecond SET SysCode='{0}' where ID={1}", syscode, ID));
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception)
            {
                rtn = 0;
            }

            return rtn;
        }
        /// <summary>
        /// 修改上传结果
        /// </summary>
        /// <param name="Statues"></param>
        /// <param name="ID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateResult(string Statues, int ID, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE ttResultSecond SET ");
                sb.AppendFormat("IsUpload='{0}'", Statues);

                sb.AppendFormat(" WHERE ID={0}", ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象tlsTtResultSecond的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(tlsTtResultSecond model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE ttResultSecond SET ");
                sb.AppendFormat("ReportDeliTime='{0}',", model.ReportDeliTime);
                sb.AppendFormat("IsReconsider={0},", model.IsReconsider);
                sb.AppendFormat("ReconsiderTime='{0}',", model.ReconsiderTime);
                sb.AppendFormat("ProceResults='{0}',", model.ProceResults);
                sb.AppendFormat("CheckPlaceCode='{0}',", model.CheckPlaceCode);
                sb.AppendFormat("CheckPlace='{0}',", model.CheckPlace);
                sb.AppendFormat("TakeDate='{0}',", model.TakeDate);
                sb.AppendFormat("DateManufacture='{0}',", model.DateManufacture);
                sb.AppendFormat("SamplingPlace='{0}',", model.SamplingPlace);
                sb.AppendFormat("CheckedComDis='{0}',", model.CheckedComDis);
                sb.AppendFormat("CheckType='{0}',", model.CheckType);
                sb.AppendFormat("IsUpload='{0}',", model.IsUpload.Equals("Y") ? "S" : model.IsUpload);
                sb.AppendFormat("CheckValueInfo='{0}',", model.CheckValueInfo);
                sb.AppendFormat("Result='{0}',", model.Result);
                sb.AppendFormat("CheckNo='{0}',", model.CheckNo);
                sb.AppendFormat("CheckPlanCode='{0}',", model.CheckPlanCode);
                sb.AppendFormat("CheckTotalItem='{0}',", model.CheckTotalItem);
                sb.AppendFormat("Standard='{0}',", model.Standard);
                sb.AppendFormat("CheckUnitName='{0}',", model.CheckUnitName);
                sb.AppendFormat("APRACategory='{0}',", model.APRACategory);
                sb.AppendFormat("CheckedCompany='{0}',", model.CheckedCompany);
                sb.AppendFormat("CheckedCompanyCode='{0}',", model.CheckedCompanyCode);
                sb.AppendFormat("CheckStartDate='{0}',", model.CheckStartDate);
                sb.AppendFormat("Organizer='{0}',", model.Organizer);
                sb.AppendFormat("FoodName='{0}',", model.FoodName);
                sb.AppendFormat("SampleCode='{0}',", model.SampleCode);
                sb.AppendFormat("StandValue='{0}',", model.StandValue);
                sb.AppendFormat("ResultInfo='{0}',", model.ResultInfo);
                sb.AppendFormat("ProduceCompany='{0}',", model.ProduceCompany);
                sb.AppendFormat("UpLoader='{0}'", model.UpLoader);
                sb.AppendFormat(" WHERE ID={0}", model.ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 修改展示状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateShow(tlsTtResultSecond model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Update ttResultSecond Set ");
                sb.AppendFormat("IsShow='{0}'", model.IsShow);
                sb.AppendFormat(" Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Delete(string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM ttResultSecond");

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 新版本接口，根据SysCode更新上传状态
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateUpload(string sysCode, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("Update ttResultSecond Set IsUpload='Y' Where SysCode = '{0}'", sysCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 修改上传状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateUpload(int id, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Update ttResultSecond Set IsUpload='Y' Where ID = " + id);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 修改报表生成状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateReport(List<int> id, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                for (int i = 0; i < id.Count; i++)
                {
                    sb.Length = 0;
                    sb.Append("Update ttResultSecond Set IsReport = 'Y' Where ID = " + id[i]);
                    DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                    sb.Length = 0;
                    rtn = 1;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type, int count)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 0)
                {
                    sb.Append(" SELECT ResultType,SampleCode,FoodName,Hole,CheckTotalItem,CheckValueInfo,ResultInfo,CheckStartDate,CheckUnitInfo,CheckMethod,Result,StandValue,SampleCode,Standard FROM ttResultSecond ");
                }
                else if (type == 1)
                {
                    sb.Append(" SELECT ");
                    sb.Append(" ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel ");
                    sb.Append(" ,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader, ");
                    sb.Append(" ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode, ");
                    sb.Append(" DateManufacture,CheckMethod,APRACategory,Hole,SamplingPlace,CheckType,IsUpload ");
                    sb.Append(" FROM ttResultSecond ");
                }
                else if (type == 2)
                {
                    sb.AppendFormat(" Select Top {0} ", count);
                    sb.Append(" syscode,a.ResultType,a.CheckNo,a.SampleCode,a.CheckedCompanyCode,a.CheckedCompany,a.CheckedComDis,a.CheckPlaceCode,a.CheckPlace, ");
                    sb.Append(" a.FoodName,a.TakeDate,a.CheckStartDate,a.Standard,a.CheckMachine,a.CheckMachineModel,a.MachineCompany,a.CheckTotalItem, ");
                    sb.Append(" a.CheckValueInfo,a.StandValue,a.Result,a.ResultInfo,a.CheckUnitName,a.CheckUnitInfo,a.Organizer,UpLoader,ReportDeliTime,IsReconsider,  ");
                    sb.Append(" ReconsiderTime,ProceResults,DateManufacture as ProduceDate,StdCode,SentCompany,Provider,ProduceCompany,ProducePlace,ImportNum,SaleNum, ");
                    sb.Append(" SaveNum,Price,SampleNum,SampleBaseNum,Unit,SampleUnit,SampleModel,SampleLevel,SampleState,OrCheckNo,CheckType,Checker,Assessor  ");
                    sb.Append(" ,Remark,CheckPlanCode,CheckederVal,IsSentCheck,CheckederRemark,IsReSended,Notes ");
                    sb.Append(" From ttResultSecond as a left join tResult as b on a.CheckNO<>b.CheckNO order by a.CheckNO desc ");
                }
                else if (type == 3)
                {
                    sb.AppendFormat(" Select Top {0} ", count);
                    sb.Append(" ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel,MachineCompany, ");
                    sb.Append(" CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime, ");
                    sb.Append(" ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode,DateManufacture,CheckMethod,APRACategory,Hole,CheckType ");
                    sb.Append(" From ttResultSecond order by CheckNO desc ");
                }
                else if (type == 4)
                {
                    sb.Append("Select * From ttResultSecond ");
                }
                else if (type == 5)
                {
                    sb.Append("Select ID From ttResultSecond ");
                }
                else if (type == 6)
                {
                    sb.AppendFormat(" Select Top {0} ", count);
                    sb.Append(" * From ttResultSecond order by ID desc ");
                }
                else if (type == 7)
                {
                    sb.Append("Select * From ttStandDecide ");
                }
                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 导出检测数据
        /// </summary>
        /// <param name="whereSql">查询条件</param>
        /// <param name="InterfaceType">接口类型</param>
        /// <param name="EACHDISTRICT">区域</param>
        /// <returns></returns>
        public DataTable ExportData(string whereSql, string InterfaceType, string EACHDISTRICT)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append(" SELECT ");
                sb.Append(" ResultType AS 项目类别,CheckNo AS 检测编号,SampleCode AS 样品编号,CheckPlaceCode AS 行政机构编号,CheckPlace AS 行政机构名称,FoodName AS 样品名称,TakeDate AS 抽检日期,CheckStartDate AS 检测时间,Standard AS 检测依据, ");
                sb.Append(" CheckTotalItem AS 检测项目,CheckValueInfo AS 检测值,StandValue AS 检测标准值,Result AS 检测结论,ResultInfo AS 检测值单位,CheckUnitName AS 检测单位,CheckUnitInfo AS 检测人姓名,Organizer AS 抽样人,UpLoader AS 基层上传人, ");
                sb.Append(" ReportDeliTime AS 检测报告送达时间,IsReconsider AS 是否提出复议,ReconsiderTime AS 提出复议时间,ProceResults AS 处理结果,CheckedCompanyCode AS 被检对象编号,CheckedCompany AS 被检对象名称,CheckedComDis AS 档口门牌号,CheckPlanCode AS 任务编号, ");
                sb.Append(" DateManufacture AS 生产日期,CheckMethod AS 检测方法,APRACategory AS 单位类别,Hole AS 检测孔,SamplingPlace AS 抽样地点,CheckType AS 检测类型,ContrastValue AS 对照值 ");
                if (InterfaceType.Equals("ZH") || InterfaceType.Equals("ALL"))
                    sb.Append(" ,DeviceId AS 唯一机器码,SampleId AS 快检单号 ");
                if (EACHDISTRICT.Equals("GS"))
                    sb.Append(" ,ProduceCompany AS 生产单位 ");
                sb.Append(" FROM ttResultSecond ");
                if (!whereSql.Equals(string.Empty))
                    sb.AppendFormat(" WHERE {0}", whereSql);
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable GetAsDataTable(int PageSize, int PageIndex, string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (PageIndex == 1)
                {
                    sb.AppendFormat(" SELECT top {0} ", PageSize);
                    //sb.Append("ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode,DateManufacture,CheckMethod,APRACategory,Hole,SamplingPlace,CheckType,IsUpload,IsShow FROM ttResultSecond ");
                    sb.Append("* FROM ttResultSecond ");
                    if (!whereSql.Equals(string.Empty))
                    {
                        sb.Append(" WHERE ");
                        sb.Append(whereSql);
                    }
                }
                else
                {
                    sb.AppendFormat(" SELECT top {0} ", PageSize);
                    sb.Append("* FROM ttResultSecond ");
                    sb.Append(" Where ID Not In(Select top ");
                    sb.Append(((PageIndex - 1) * PageSize).ToString());
                    sb.Append(" ID From ttResultSecond  Order By ID Desc)");
                    if (!whereSql.Equals(string.Empty))
                    {
                        sb.Append(" And ");
                        sb.Append(whereSql);
                    }
                }
                sb.Append(" Order by ID Desc ");
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取测试数据总记录数
        /// </summary>
        /// <returns></returns>
        public DataTable GetAsDataTable()
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append(" SELECT ");
                sb.Append(" ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel ");
                sb.Append(" ,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader, ");
                sb.Append(" ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode, ");
                sb.Append(" DateManufacture,CheckMethod,APRACategory,Hole,SamplingPlace,CheckType,IsUpload ");
                sb.Append(" FROM ttResultSecond ");
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 判定检测编号是否存在
        /// </summary>
        /// <param name="CheckNo"></param>
        /// <returns></returns>
        public bool GetRepeatCheckNo(string CheckNo)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.AppendFormat(" Select 1 From ttResultSecond Where CheckNo like '{0}'", CheckNo);
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
                if (dt.Rows.Count > 0) return true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
            return false;
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdatePart(string model,string Tname, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.AppendFormat("UPDATE tTask SET CPTITLE='{0}'", model);

                sb.AppendFormat(" WHERE CPCODE='{0}'", Tname);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(tlsTtResultSecond model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append(" INSERT INTO ttResultSecond ");
                sb.Append(" (ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,FoodType,TakeDate,CheckStartDate,Standard,CheckMachine, ");
                sb.Append(" CheckMachineModel,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo, ");
                sb.Append(" Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis, ");
                sb.Append(" CheckPlanCode,Hole,CheckMethod,APRACategory,CheckType,DateManufacture,ContrastValue,DeviceId,SampleId,ProduceCompany,SysCode,ItemCode) ");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.ResultType);
                sb.AppendFormat("'{0}',", model.CheckNo);
                sb.AppendFormat("'{0}',", model.SampleCode);
                sb.AppendFormat("'{0}',", model.CheckPlaceCode);
                sb.AppendFormat("'{0}',", model.CheckPlace);
                sb.AppendFormat("'{0}',", model.FoodName);
                sb.AppendFormat("'{0}',", model.FoodType);
                sb.AppendFormat("'{0}',", model.TakeDate);
                sb.AppendFormat("'{0}',", model.CheckStartDate);
                sb.AppendFormat("'{0}',", model.Standard);
                sb.AppendFormat("'{0}',", model.CheckMachine);
                sb.AppendFormat("'{0}',", model.CheckMachineModel);
                sb.AppendFormat("'{0}',", model.MachineCompany);
                sb.AppendFormat("'{0}',", model.CheckTotalItem);
                sb.AppendFormat("'{0}',", model.CheckValueInfo);
                sb.AppendFormat("'{0}',", model.StandValue);
                sb.AppendFormat("'{0}',", model.Result);
                sb.AppendFormat("'{0}',", model.ResultInfo);
                sb.AppendFormat("'{0}',", model.CheckUnitName);
                sb.AppendFormat("'{0}',", model.CheckUnitInfo);
                sb.AppendFormat("'{0}',", model.Organizer);
                sb.AppendFormat("'{0}',", model.UpLoader);
                sb.AppendFormat("'{0}',", model.ReportDeliTime);
                sb.AppendFormat("{0},", model.IsReconsider);
                sb.AppendFormat("'{0}',", model.ReconsiderTime);
                sb.AppendFormat("'{0}',", model.ProceResults);
                sb.AppendFormat("'{0}',", model.CheckedCompanyCode);
                sb.AppendFormat("'{0}',", model.CheckedCompany);
                sb.AppendFormat("'{0}',", model.CheckedComDis);
                sb.AppendFormat("'{0}',", model.CheckPlanCode);
                sb.AppendFormat("'{0}',", model.Hole);
                sb.AppendFormat("'{0}',", model.CheckMethod);
                sb.AppendFormat("'{0}',", model.APRACategory);
                sb.AppendFormat("'{0}',", model.CheckType);
                sb.AppendFormat("'{0}',", model.DateManufacture);
                sb.AppendFormat("'{0}',", model.ContrastValue);
                sb.AppendFormat("'{0}',", model.DeviceId);
                sb.AppendFormat("'{0}',", model.SampleId);
                sb.AppendFormat("'{0}',", model.ProduceCompany);
                sb.AppendFormat("'{0}',", model.SysCode);
                sb.AppendFormat("'{0}')", model.ItemCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public DataTable GetAllCompanies()
        {
            string sqlWhere = string.Empty;
            DataTable companies = GetAsDataTable(sqlWhere, string.Empty, 1, 0);
            return companies;
        }

        /// <summary>
        /// 新增甘肃报表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable Insert(clsReportGS model, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Insert Into tReportGS (Title,FoodName,FoodType,ProductionDate,CheckedCompanyName,");
                sb.Append("CheckedCompanyAddress,CheckedCompanyPhone,LabelProducerName,LabelProducerAddress,LabelProducerPhone,");
                sb.Append("SamplingData,SamplingPerson,SampleNum,SamplingBase,SamplingAddress,");
                sb.Append("SamplingOrderCode,Standard,InspectionConclusion,Notes,Audit,Surveyor) ");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.Title);
                sb.AppendFormat("'{0}',", model.FoodName);
                sb.AppendFormat("'{0}',", model.FoodType);
                sb.AppendFormat("'{0}',", model.ProductionDate);
                sb.AppendFormat("'{0}',", model.CheckedCompanyName);

                sb.AppendFormat("'{0}',", model.CheckedCompanyAddress);
                sb.AppendFormat("'{0}',", model.CheckedCompanyPhone);
                sb.AppendFormat("'{0}',", model.LabelProducerName);
                sb.AppendFormat("'{0}',", model.LabelProducerAddress);
                sb.AppendFormat("'{0}',", model.LabelProducerPhone);

                sb.AppendFormat("'{0}',", model.SamplingData);
                sb.AppendFormat("'{0}',", model.SamplingPerson);
                sb.AppendFormat("'{0}',", model.SampleNum);
                sb.AppendFormat("'{0}',", model.SamplingBase);
                sb.AppendFormat("'{0}',", model.SamplingAddress);

                sb.AppendFormat("'{0}',", model.SamplingOrderCode);
                sb.AppendFormat("'{0}',", model.Standard);
                sb.AppendFormat("'{0}',", model.InspectionConclusion);
                sb.AppendFormat("'{0}',", model.Notes);
                sb.AppendFormat("'{0}',", model.Audit);
                sb.AppendFormat("'{0}')", model.Surveyor);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                sb.Append("Select Top 1 * From tReportGS Order By ID Desc");
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入一条报表记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable Insert(clsReport model, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Insert Into tReport (CheckUnitName,Trademark,Specifications,ProductionDate,QualityGrade,CheckedCompanyName,CheckedCompanyPhone,ProductionUnitsName,ProductionUnitsPhone,TaskSource,Standard,SamplingData,SampleNum,SamplingCode,SampleArrivalData,Notes,IsDeleted,CreateData) ");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.CheckUnitName);
                sb.AppendFormat("'{0}',", model.Trademark);
                sb.AppendFormat("'{0}',", model.Specifications);
                sb.AppendFormat("'{0}',", model.ProductionDate);
                sb.AppendFormat("'{0}',", model.QualityGrade);
                sb.AppendFormat("'{0}',", model.CheckedCompanyName);
                sb.AppendFormat("'{0}',", model.CheckedCompanyPhone);
                sb.AppendFormat("'{0}',", model.ProductionUnitsName);
                sb.AppendFormat("'{0}',", model.ProductionUnitsPhone);
                sb.AppendFormat("'{0}',", model.TaskSource);
                sb.AppendFormat("'{0}',", model.Standard);
                sb.AppendFormat("'{0}',", model.SamplingData);
                sb.AppendFormat("'{0}',", model.SampleNum);
                sb.AppendFormat("'{0}',", model.SamplingCode);
                sb.AppendFormat("'{0}',", model.SampleArrivalData);
                sb.AppendFormat("'{0}',", model.Notes);
                sb.AppendFormat("'{0}',", model.IsDeleted);
                sb.AppendFormat("'{0}'", model.CreateData);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                sb.Append("Select Top 1 * From tReport Order By ID Desc");
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入报表子表样品集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsReportGSDetail model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Insert Into tReportGSDetail (ReportGSID,ProjectName,Unit,InspectionStandard,IndividualResults,IndividualDecision) ");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.ReportGSID);
                sb.AppendFormat("'{0}',", model.ProjectName);
                sb.AppendFormat("'{0}',", model.Unit);
                sb.AppendFormat("'{0}',", model.InspectionStandard);
                sb.AppendFormat("'{0}',", model.IndividualResults);
                sb.AppendFormat("'{0}'", model.IndividualDecision);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 插入报表子表样品集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsReportDetail model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Insert Into tReportDetail (ReportID,FoodName,ProjectName,Unit,CheckData,IsDeleted) ");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.ReportID);
                sb.AppendFormat("'{0}',", model.FoodName);
                sb.AppendFormat("'{0}',", model.ProjectName);
                sb.AppendFormat("'{0}',", model.Unit);
                sb.AppendFormat("'{0}',", model.CheckData);
                sb.AppendFormat("'{0}'", model.IsDeleted);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 删除报表记录 甘肃 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeletedGS(int ID, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                //先根据主表ID删除子表信息
                sb.Length = 0;
                sb.AppendFormat("Delete From tReportGSDetail Where ReportGSID={0}", ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                //删除主表信息
                sb.Length = 0;
                sb.AppendFormat("Delete From tReportGS Where ID={0}", ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 删除报表记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Deleted(int ID, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("Delete From tReportDetail Where ReportID={0}", ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                sb.Length = 0;
                sb.AppendFormat("Delete From tReport Where ID={0}", ID);
                //sb.AppendFormat("Update tReport Set IsDeleted='Y' Where ID={0}", ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 清空所有报表子表信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeletedAllDetail(string strWhere, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Delete From tReportDetail");
                if (strWhere != string.Empty)
                {
                    sb.Append(" Where ");
                    sb.Append(strWhere);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 删除所有报表信息
        /// 无条件删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeletedReportAll(out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                //清空原始报表信息
                sb.Length = 0;
                sb.Append("Delete From tReport");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                sb.Length = 0;
                sb.Append("Delete From tReportDetail");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                //清空甘肃报表信息
                sb.Length = 0;
                sb.Append("Delete From tReportGS");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                sb.Length = 0;
                sb.Append("Delete From tReportGSDetail");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 删除所有报表信息
        /// 有条件
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeletedAll(string strWhere, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Delete From tReport");
                if (strWhere != string.Empty)
                {
                    sb.Append(" Where ");
                    sb.Append(strWhere);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 修改报表记录 GS
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(clsReportGS model, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Update tReportGS Set ");
                sb.AppendFormat("Title='{0}',", model.Title == null ? string.Empty : model.Title);
                sb.AppendFormat("FoodName='{0}',", model.FoodName == null ? string.Empty : model.FoodName);
                sb.AppendFormat("FoodType='{0}',", model.FoodType == null ? string.Empty : model.FoodType);
                sb.AppendFormat("ProductionDate='{0}',", model.ProductionDate == null ? string.Empty : model.ProductionDate);
                sb.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName == null ? string.Empty : model.CheckedCompanyName);
                sb.AppendFormat("CheckedCompanyAddress='{0}',", model.CheckedCompanyAddress == null ? string.Empty : model.CheckedCompanyAddress);
                sb.AppendFormat("CheckedCompanyPhone='{0}',", model.CheckedCompanyPhone == null ? string.Empty : model.CheckedCompanyPhone);
                sb.AppendFormat("LabelProducerName='{0}',", model.LabelProducerName == null ? string.Empty : model.LabelProducerName);
                sb.AppendFormat("LabelProducerAddress='{0}',", model.LabelProducerAddress == null ? string.Empty : model.LabelProducerAddress);
                sb.AppendFormat("LabelProducerPhone='{0}',", model.LabelProducerPhone == null ? string.Empty : model.LabelProducerPhone);
                sb.AppendFormat("SamplingData='{0}',", model.SamplingData == null ? string.Empty : model.SamplingData);
                sb.AppendFormat("SamplingPerson='{0}',", model.SamplingPerson == null ? string.Empty : model.SamplingPerson);
                sb.AppendFormat("SampleNum='{0}',", model.SampleNum == null ? string.Empty : model.SampleNum);
                sb.AppendFormat("SamplingBase='{0}',", model.SamplingBase == null ? string.Empty : model.SamplingBase);
                sb.AppendFormat("SamplingAddress='{0}',", model.SamplingAddress == null ? string.Empty : model.SamplingAddress);
                sb.AppendFormat("SamplingOrderCode='{0}',", model.SamplingOrderCode == null ? string.Empty : model.SamplingOrderCode);
                sb.AppendFormat("Standard='{0}',", model.Standard == null ? string.Empty : model.Standard);
                sb.AppendFormat("InspectionConclusion='{0}' ", model.InspectionConclusion == null ? string.Empty : model.InspectionConclusion);
                sb.AppendFormat("Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 修改报表记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(clsReport model, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("Update tReport Set ");
                sb.AppendFormat("Title='{0}',", model.Title);
                sb.AppendFormat("CheckUnitName='{0}',", model.CheckUnitName);
                sb.AppendFormat("Trademark='{0}',", model.Trademark);
                sb.AppendFormat("Specifications='{0}',", model.Specifications);
                sb.AppendFormat("ProductionDate='{0}',", model.ProductionDate);
                sb.AppendFormat("QualityGrade='{0}',", model.QualityGrade);
                sb.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName);
                sb.AppendFormat("CheckedCompanyPhone='{0}',", model.CheckedCompanyPhone);
                sb.AppendFormat("ProductionUnitsName='{0}',", model.ProductionUnitsName);
                sb.AppendFormat("ProductionUnitsPhone='{0}',", model.ProductionUnitsPhone);
                sb.AppendFormat("TaskSource='{0}',", model.TaskSource);
                sb.AppendFormat("Standard='{0}',", model.Standard);
                sb.AppendFormat("SamplingData='{0}',", model.SamplingData);
                sb.AppendFormat("SampleNum='{0}',", model.SampleNum);
                sb.AppendFormat("SamplingCode='{0}',", model.SamplingCode);
                sb.AppendFormat("SampleArrivalData='{0}',", model.SampleArrivalData);
                sb.AppendFormat("Notes='{0}',", model.Notes);
                sb.AppendFormat("IsDeleted='{0}' ", model.IsDeleted);
                sb.AppendFormat("Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 查询报表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="type">1 原始报表，2 甘肃报表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetReport(string whereSql, int type, out string errMsg)
        {
            string strTb = errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                    strTb = "tReport";
                else if (type == 2)
                    strTb = "tReportGS";

                sb.AppendFormat("Select * From {0} ", strTb);
                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                sb.Append(" Order By ID Desc");
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Report" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Report"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询报表子表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <returns></returns>
        public DataTable GetReportDetail(int ID)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("Select * From tReportDetail Where ReportID = " + ID);
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ReportDetail" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ReportDetail"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询报表子表 甘肃报表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <returns></returns>
        public DataTable GetReportDetailGS(int ID)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.AppendFormat("Select * From tReportGSDetail Where ReportGSID Like '{0}'", ID.ToString());
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ReportDetailGS" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ReportDetailGS"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        public int DataBaseRepair(string sql, out string err)
        {
            string errMsg = err = string.Empty;
            int rtn = 0;
            try
            {
                DataBase.ExecuteCommand(sql, out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return rtn;
        }

    }
}