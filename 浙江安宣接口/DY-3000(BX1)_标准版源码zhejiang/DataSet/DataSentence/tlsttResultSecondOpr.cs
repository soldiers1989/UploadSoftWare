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
        private StringBuilder sql = new StringBuilder();

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
                sql.Length = 0;
                sql.Append("DELETE FROM ttResultSecond Where ID = " + CheckNo);
                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                retn = 1;
            }
            catch (Exception)
            {
                retn = 0;
            }
            return retn;
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
                sql.Length = 0;
                sql.Append("UPDATE ttResultSecond SET ");
                sql.AppendFormat("ReportDeliTime='{0}',", model.ReportDeliTime);
                sql.AppendFormat("IsReconsider={0},", model.IsReconsider);
                sql.AppendFormat("ReconsiderTime='{0}',", model.ReconsiderTime);
                sql.AppendFormat("ProceResults='{0}',", model.ProceResults);
                sql.AppendFormat("CheckPlaceCode='{0}',", model.CheckPlaceCode);
                sql.AppendFormat("CheckPlace='{0}',", model.CheckPlace);
                sql.AppendFormat("TakeDate='{0}',", model.TakeDate);
                sql.AppendFormat("DateManufacture='{0}',", model.DateManufacture);
                sql.AppendFormat("SamplingPlace='{0}',", model.SamplingPlace);
                sql.AppendFormat("CheckedComDis='{0}',", model.CheckedComDis);
                sql.AppendFormat("CheckType='{0}',", model.CheckType);
                sql.AppendFormat("IsUpload='{0}',", model.IsUpload.Equals("Y") ? "S" : model.IsUpload);
                sql.AppendFormat("CheckValueInfo='{0}',", model.CheckValueInfo);
                sql.AppendFormat("Result='{0}',", model.Result);
                sql.AppendFormat("CheckNo='{0}',", model.CheckNo);
                sql.AppendFormat("CheckPlanCode='{0}',", model.CheckPlanCode);
                sql.AppendFormat("CheckTotalItem='{0}',", model.CheckTotalItem);
                sql.AppendFormat("Standard='{0}',", model.Standard);
                sql.AppendFormat("CheckUnitName='{0}',", model.CheckUnitName);
                sql.AppendFormat("APRACategory='{0}',", model.APRACategory);
                sql.AppendFormat("CheckedCompany='{0}',", model.CheckedCompany);
                sql.AppendFormat("CheckedCompanyCode='{0}',", model.CheckedCompanyCode);
                sql.AppendFormat("CheckStartDate='{0}',", model.CheckStartDate);
                sql.AppendFormat("Organizer='{0}',", model.Organizer);
                sql.AppendFormat("FoodName='{0}',", model.FoodName);
                sql.AppendFormat("SampleCode='{0}',", model.SampleCode);
                sql.AppendFormat("StandValue='{0}',", model.StandValue);
                sql.AppendFormat("ResultInfo='{0}',", model.ResultInfo);
                sql.AppendFormat("UpLoader='{0}'", model.UpLoader);
                sql.AppendFormat(" WHERE ID={0}", model.ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Update ttResultSecond Set ");
                sql.AppendFormat("IsShow='{0}'", model.IsShow);
                sql.AppendFormat(" Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("DELETE FROM ttResultSecond");

                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Update ttResultSecond Set IsUpload='Y' Where ID = " + id);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                    sql.Length = 0;
                    sql.Append("Update ttResultSecond Set IsReport = 'Y' Where ID = " + id[i]);
                    DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                    sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                if (type == 0)
                {
                    sql.Append(" SELECT ResultType,SampleCode,FoodName,Hole,CheckTotalItem,CheckValueInfo,ResultInfo,CheckStartDate,CheckUnitInfo,CheckMethod,Result,StandValue,SampleCode,Standard FROM ttResultSecond ");
                }
                else if (type == 1)
                {
                    sql.Append(" SELECT ");
                    sql.Append(" ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel ");
                    sql.Append(" ,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader, ");
                    sql.Append(" ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode, ");
                    sql.Append(" DateManufacture,CheckMethod,APRACategory,Hole,SamplingPlace,CheckType,IsUpload ");
                    sql.Append(" FROM ttResultSecond ");
                }
                else if (type == 2)
                {
                    sql.AppendFormat(" Select Top {0} ", count);
                    sql.Append(" syscode,a.ResultType,a.CheckNo,a.SampleCode,a.CheckedCompanyCode,a.CheckedCompany,a.CheckedComDis,a.CheckPlaceCode,a.CheckPlace, ");
                    sql.Append(" a.FoodName,a.TakeDate,a.CheckStartDate,a.Standard,a.CheckMachine,a.CheckMachineModel,a.MachineCompany,a.CheckTotalItem, ");
                    sql.Append(" a.CheckValueInfo,a.StandValue,a.Result,a.ResultInfo,a.CheckUnitName,a.CheckUnitInfo,a.Organizer,UpLoader,ReportDeliTime,IsReconsider,  ");
                    sql.Append(" ReconsiderTime,ProceResults,DateManufacture as ProduceDate,StdCode,SentCompany,Provider,ProduceCompany,ProducePlace,ImportNum,SaleNum, ");
                    sql.Append(" SaveNum,Price,SampleNum,SampleBaseNum,Unit,SampleUnit,SampleModel,SampleLevel,SampleState,OrCheckNo,CheckType,Checker,Assessor  ");
                    sql.Append(" ,Remark,CheckPlanCode,CheckederVal,IsSentCheck,CheckederRemark,IsReSended,Notes ");
                    sql.Append(" From ttResultSecond as a left join tResult as b on a.CheckNO<>b.CheckNO order by a.CheckNO desc ");
                }
                else if (type == 3)
                {
                    sql.AppendFormat(" Select Top {0} ", count);
                    sql.Append(" ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel,MachineCompany, ");
                    sql.Append(" CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime, ");
                    sql.Append(" ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode,DateManufacture,CheckMethod,APRACategory,Hole,CheckType ");
                    sql.Append(" From ttResultSecond order by CheckNO desc ");
                }
                else if (type == 4)
                {
                    sql.Append("Select * From ttResultSecond ");
                }
                else if (type == 5)
                {
                    sql.Append("Select ID From ttResultSecond ");
                }
                else if (type == 6)
                {
                    sql.AppendFormat(" Select Top {0} ", count);
                    sql.Append(" * From ttResultSecond order by ID desc ");
                }
                else if (type == 7)
                {
                    sql.Append("Select * From ttStandDecide ");
                }
                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
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
        /// <param name="type">类型 1 全部|2 根据ID导出|</param>
        /// <returns></returns>
        public DataTable ExportData(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append(" SELECT ");
                sql.Append(" ResultType AS 项目类别,CheckNo AS 检测编号,SampleCode AS 样品编号,CheckPlaceCode AS 行政机构编号,CheckPlace AS 行政机构名称,FoodName AS 样品名称,FoodType AS 样品种类,TakeDate AS 抽检日期,CheckStartDate AS 检测时间,Standard AS 检测依据,CheckMachine AS 检测设备,CheckMachineModel AS 检测设备型号,MachineCompany AS 检测设备厂商, ");
                sql.Append(" CheckTotalItem AS 检测项目,CheckValueInfo AS 检测值,StandValue AS 检测标准值,Result AS 检测结论,ResultInfo AS 检测值单位,CheckUnitName AS 检测单位,CheckUnitInfo AS 检测人姓名,Organizer AS 抽样人,UpLoader AS 基层上传人, ");
                sql.Append(" ReportDeliTime AS 检测报告送达时间,IsReconsider AS 是否提出复议,ReconsiderTime AS 提出复议时间,ProceResults AS 处理结果,CheckedCompanyCode AS 被检对象编号,CheckedCompany AS 被检对象名称,CheckedComDis AS 档口门牌号,CheckPlanCode AS 任务编号, ");
                sql.Append(" DateManufacture AS 生产日期,CheckMethod AS 检测方法,APRACategory AS 单位类别,Hole AS 检测孔,SamplingPlace AS 抽样地点,CheckType AS 检测类型,ContrastValue AS 对照值 ");
                sql.Append(" FROM ttResultSecond ");
                if (!whereSql.Equals(""))
                    sql.AppendFormat(" WHERE {0}", whereSql);
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                if (PageIndex == 1)
                {
                    sql.AppendFormat(" SELECT top {0} ", PageSize);
                    sql.Append("* FROM ttResultSecond ");
                    if (!whereSql.Equals(""))
                    {
                        sql.Append(" WHERE ");
                        sql.Append(whereSql);
                    }
                }
                else
                {
                    sql.AppendFormat(" SELECT top {0} ", PageSize);
                    sql.Append("* FROM ttResultSecond ");
                    sql.Append(" Where ID Not In(Select top ");
                    sql.Append(((PageIndex - 1) * PageSize).ToString());
                    sql.Append(" ID From ttResultSecond  Order By ID Desc)");
                    if (!whereSql.Equals(""))
                    {
                        sql.Append(" And ");
                        sql.Append(whereSql);
                    }
                }
                sql.Append(" Order by ID Desc ");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                sql.Append(" SELECT ");
                sql.Append(" ID,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine,CheckMachineModel ");
                sql.Append(" ,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo,Organizer,UpLoader, ");
                sql.Append(" ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis,CheckPlanCode, ");
                sql.Append(" DateManufacture,CheckMethod,APRACategory,Hole,SamplingPlace,CheckType,IsUpload ");
                sql.Append(" FROM ttResultSecond ");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 新增曲线数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertCurveData(clsCurveDatas model,out string errMsg) 
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append(" INSERT INTO CurveDatas ");
                sql.AppendFormat("(SysCode,CData) Values('{0}','{1}')", model.SysCode, model.CData);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertAh(tlsTtResultSecond model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append(" INSERT INTO ttResultSecond ");
                sql.Append(" (SysCode,ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,TakeDate,CheckStartDate,Standard,CheckMachine, ");
                sql.Append(" CheckMachineModel,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo, ");
                sql.Append(" Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis, ");
                sql.Append(" CheckPlanCode,Hole,CheckMethod,APRACategory,CheckType,DateManufacture,ContrastValue,fTpye,testPro,quanOrQual,dataNum,checkedUnit,ItemCode) ");
                sql.Append(" VALUES(");
                sql.AppendFormat("'{0}',", model.SysCode);
                sql.AppendFormat("'{0}',", model.ResultType);
                sql.AppendFormat("'{0}',", model.CheckNo);
                sql.AppendFormat("'{0}',", model.SampleCode);
                sql.AppendFormat("'{0}',", model.CheckPlaceCode);
                sql.AppendFormat("'{0}',", model.CheckPlace);
                sql.AppendFormat("'{0}',", model.FoodName);
                sql.AppendFormat("'{0}',", model.TakeDate);
                sql.AppendFormat("'{0}',", model.CheckStartDate);
                sql.AppendFormat("'{0}',", model.Standard);
                sql.AppendFormat("'{0}',", model.CheckMachine);
                sql.AppendFormat("'{0}',", model.CheckMachineModel);
                sql.AppendFormat("'{0}',", model.MachineCompany);
                sql.AppendFormat("'{0}',", model.CheckTotalItem);
                sql.AppendFormat("'{0}',", model.CheckValueInfo);
                sql.AppendFormat("'{0}',", model.StandValue);
                sql.AppendFormat("'{0}',", model.Result);
                sql.AppendFormat("'{0}',", model.ResultInfo);
                sql.AppendFormat("'{0}',", model.CheckUnitName);
                sql.AppendFormat("'{0}',", model.CheckUnitInfo);
                sql.AppendFormat("'{0}',", model.Organizer);
                sql.AppendFormat("'{0}',", model.UpLoader);
                sql.AppendFormat("'{0}',", model.ReportDeliTime);
                sql.AppendFormat("{0},", model.IsReconsider);
                sql.AppendFormat("'{0}',", model.ReconsiderTime);
                sql.AppendFormat("'{0}',", model.ProceResults);
                sql.AppendFormat("'{0}',", model.CheckedCompanyCode);
                sql.AppendFormat("'{0}',", model.CheckedCompany);
                sql.AppendFormat("'{0}',", model.CheckedComDis);
                sql.AppendFormat("'{0}',", model.CheckPlanCode);
                sql.AppendFormat("'{0}',", model.Hole);
                sql.AppendFormat("'{0}',", model.CheckMethod);
                sql.AppendFormat("'{0}',", model.APRACategory);
                sql.AppendFormat("'{0}',", model.CheckType);
                sql.AppendFormat("'{0}',", model.DateManufacture);
                sql.AppendFormat("'{0}',", model.ContrastValue);
                sql.AppendFormat("'{0}',", model.fTpye);
                sql.AppendFormat("'{0}',", model.testPro);
                sql.AppendFormat("'{0}',", model.quanOrQual);
                sql.AppendFormat("'{0}',", model.dataNum);
                sql.AppendFormat("'{0}'", model.checkedUnit);
                sql.AppendFormat("'{0}'",  model.ItemCode);//浙江检测项目编号
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

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
                sql.Length = 0;
                sql.Append(" INSERT INTO ttResultSecond ");
                sql.Append(" (ResultType,CheckNo,SampleCode,CheckPlaceCode,CheckPlace,FoodName,FoodType,TakeDate,CheckStartDate,Standard,CheckMachine, ");
                sql.Append(" CheckMachineModel,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,CheckUnitName,CheckUnitInfo, ");
                sql.Append(" Organizer,UpLoader,ReportDeliTime,IsReconsider,ReconsiderTime,ProceResults,CheckedCompanyCode,CheckedCompany,CheckedComDis, ");
                sql.Append(" CheckPlanCode,Hole,CheckMethod,APRACategory,CheckType,DateManufacture,ContrastValue,DeviceId,SampleId,ProduceCompany,SysCode,ItemCode) ");
                sql.Append(" VALUES(");
                sql.AppendFormat("'{0}',", model.ResultType);
                sql.AppendFormat("'{0}',", model.CheckNo);
                sql.AppendFormat("'{0}',", model.SampleCode);
                sql.AppendFormat("'{0}',", model.CheckPlaceCode);
                sql.AppendFormat("'{0}',", model.CheckPlace);
                sql.AppendFormat("'{0}',", model.FoodName);
                sql.AppendFormat("'{0}',", model.FoodType);
                sql.AppendFormat("'{0}',", model.TakeDate);
                sql.AppendFormat("'{0}',", model.CheckStartDate);
                sql.AppendFormat("'{0}',", model.Standard);
                sql.AppendFormat("'{0}',", model.CheckMachine);
                sql.AppendFormat("'{0}',", model.CheckMachineModel);
                sql.AppendFormat("'{0}',", model.MachineCompany);
                sql.AppendFormat("'{0}',", model.CheckTotalItem);
                sql.AppendFormat("'{0}',", model.CheckValueInfo);
                sql.AppendFormat("'{0}',", model.StandValue);
                sql.AppendFormat("'{0}',", model.Result);
                sql.AppendFormat("'{0}',", model.ResultInfo);
                sql.AppendFormat("'{0}',", model.CheckUnitName);
                sql.AppendFormat("'{0}',", model.CheckUnitInfo);
                sql.AppendFormat("'{0}',", model.Organizer);
                sql.AppendFormat("'{0}',", model.UpLoader);
                sql.AppendFormat("'{0}',", model.ReportDeliTime);
                sql.AppendFormat("{0},", model.IsReconsider);
                sql.AppendFormat("'{0}',", model.ReconsiderTime);
                sql.AppendFormat("'{0}',", model.ProceResults);
                sql.AppendFormat("'{0}',", model.CheckedCompanyCode);
                sql.AppendFormat("'{0}',", model.CheckedCompany);
                sql.AppendFormat("'{0}',", model.CheckedComDis);
                sql.AppendFormat("'{0}',", model.CheckPlanCode);
                sql.AppendFormat("'{0}',", model.Hole);
                sql.AppendFormat("'{0}',", model.CheckMethod);
                sql.AppendFormat("'{0}',", model.APRACategory);
                sql.AppendFormat("'{0}',", model.CheckType);
                sql.AppendFormat("'{0}',", model.DateManufacture);
                sql.AppendFormat("'{0}',", model.ContrastValue);
                sql.AppendFormat("'{0}',", model.DeviceId);
                sql.AppendFormat("'{0}',", model.SampleId);
                sql.AppendFormat("'{0}',", model.ProduceCompany);
                sql.AppendFormat("'{0}',", model.SysCode);
                sql.AppendFormat("'{0}')", model.ItemCode);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

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
            DataTable companies = GetAsDataTable(sqlWhere, "", 1, 0);
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
                sql.Length = 0;
                sql.Append("Insert Into tReportGS (Title,FoodName,FoodType,ProductionDate,CheckedCompanyName,");
                sql.Append("CheckedCompanyAddress,CheckedCompanyPhone,LabelProducerName,LabelProducerAddress,LabelProducerPhone,");
                sql.Append("SamplingData,SamplingPerson,SampleNum,SamplingBase,SamplingAddress,");
                sql.Append("SamplingOrderCode,Standard,InspectionConclusion,Notes,Audit,Surveyor) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.Title);
                sql.AppendFormat("'{0}',", model.FoodName);
                sql.AppendFormat("'{0}',", model.FoodType);
                sql.AppendFormat("'{0}',", model.ProductionDate);
                sql.AppendFormat("'{0}',", model.CheckedCompanyName);

                sql.AppendFormat("'{0}',", model.CheckedCompanyAddress);
                sql.AppendFormat("'{0}',", model.CheckedCompanyPhone);
                sql.AppendFormat("'{0}',", model.LabelProducerName);
                sql.AppendFormat("'{0}',", model.LabelProducerAddress);
                sql.AppendFormat("'{0}',", model.LabelProducerPhone);

                sql.AppendFormat("'{0}',", model.SamplingData);
                sql.AppendFormat("'{0}',", model.SamplingPerson);
                sql.AppendFormat("'{0}',", model.SampleNum);
                sql.AppendFormat("'{0}',", model.SamplingBase);
                sql.AppendFormat("'{0}',", model.SamplingAddress);

                sql.AppendFormat("'{0}',", model.SamplingOrderCode);
                sql.AppendFormat("'{0}',", model.Standard);
                sql.AppendFormat("'{0}',", model.InspectionConclusion);
                sql.AppendFormat("'{0}',", model.Notes);
                sql.AppendFormat("'{0}',", model.Audit);
                sql.AppendFormat("'{0}')", model.Surveyor);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                sql.Append("Select Top 1 * From tReportGS Order By ID Desc");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Insert Into tReport (CheckUnitName,Trademark,Specifications,ProductionDate,QualityGrade,CheckedCompanyName,CheckedCompanyPhone,ProductionUnitsName,ProductionUnitsPhone,TaskSource,Standard,SamplingData,SampleNum,SamplingCode,SampleArrivalData,Notes,IsDeleted,CreateData) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.CheckUnitName);
                sql.AppendFormat("'{0}',", model.Trademark);
                sql.AppendFormat("'{0}',", model.Specifications);
                sql.AppendFormat("'{0}',", model.ProductionDate);
                sql.AppendFormat("'{0}',", model.QualityGrade);
                sql.AppendFormat("'{0}',", model.CheckedCompanyName);
                sql.AppendFormat("'{0}',", model.CheckedCompanyPhone);
                sql.AppendFormat("'{0}',", model.ProductionUnitsName);
                sql.AppendFormat("'{0}',", model.ProductionUnitsPhone);
                sql.AppendFormat("'{0}',", model.TaskSource);
                sql.AppendFormat("'{0}',", model.Standard);
                sql.AppendFormat("'{0}',", model.SamplingData);
                sql.AppendFormat("'{0}',", model.SampleNum);
                sql.AppendFormat("'{0}',", model.SamplingCode);
                sql.AppendFormat("'{0}',", model.SampleArrivalData);
                sql.AppendFormat("'{0}',", model.Notes);
                sql.AppendFormat("'{0}',", model.IsDeleted);
                sql.AppendFormat("'{0}'", model.CreateData);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                sql.Append("Select Top 1 * From tReport Order By ID Desc");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Insert Into tReportGSDetail (ReportGSID,ProjectName,Unit,InspectionStandard,IndividualResults,IndividualDecision) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.ReportGSID);
                sql.AppendFormat("'{0}',", model.ProjectName);
                sql.AppendFormat("'{0}',", model.Unit);
                sql.AppendFormat("'{0}',", model.InspectionStandard);
                sql.AppendFormat("'{0}',", model.IndividualResults);
                sql.AppendFormat("'{0}'", model.IndividualDecision);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Insert Into tReportDetail (ReportID,FoodName,ProjectName,Unit,CheckData,IsDeleted) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.ReportID);
                sql.AppendFormat("'{0}',", model.FoodName);
                sql.AppendFormat("'{0}',", model.ProjectName);
                sql.AppendFormat("'{0}',", model.Unit);
                sql.AppendFormat("'{0}',", model.CheckData);
                sql.AppendFormat("'{0}'", model.IsDeleted);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.AppendFormat("Delete From tReportGSDetail Where ReportGSID={0}", ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                //删除主表信息
                sql.Length = 0;
                sql.AppendFormat("Delete From tReportGS Where ID={0}", ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.AppendFormat("Delete From tReportDetail Where ReportID={0}", ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                sql.Length = 0;
                sql.AppendFormat("Delete From tReport Where ID={0}", ID);
                //sb.AppendFormat("Update tReport Set IsDeleted='Y' Where ID={0}", ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Delete From tReportDetail");
                if (strWhere != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(strWhere);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Delete From tReport");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                sql.Length = 0;
                sql.Append("Delete From tReportDetail");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                //清空甘肃报表信息
                sql.Length = 0;
                sql.Append("Delete From tReportGS");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                sql.Length = 0;
                sql.Append("Delete From tReportGSDetail");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

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
                sql.Length = 0;
                sql.Append("Delete From tReport");
                if (strWhere != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(strWhere);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Update tReportGS Set ");
                sql.AppendFormat("Title='{0}',", model.Title == null ? "" : model.Title);
                sql.AppendFormat("FoodName='{0}',", model.FoodName == null ? "" : model.FoodName);
                sql.AppendFormat("FoodType='{0}',", model.FoodType == null ? "" : model.FoodType);
                sql.AppendFormat("ProductionDate='{0}',", model.ProductionDate == null ? "" : model.ProductionDate);
                sql.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName == null ? "" : model.CheckedCompanyName);
                sql.AppendFormat("CheckedCompanyAddress='{0}',", model.CheckedCompanyAddress == null ? "" : model.CheckedCompanyAddress);
                sql.AppendFormat("CheckedCompanyPhone='{0}',", model.CheckedCompanyPhone == null ? "" : model.CheckedCompanyPhone);
                sql.AppendFormat("LabelProducerName='{0}',", model.LabelProducerName == null ? "" : model.LabelProducerName);
                sql.AppendFormat("LabelProducerAddress='{0}',", model.LabelProducerAddress == null ? "" : model.LabelProducerAddress);
                sql.AppendFormat("LabelProducerPhone='{0}',", model.LabelProducerPhone == null ? "" : model.LabelProducerPhone);
                sql.AppendFormat("SamplingData='{0}',", model.SamplingData == null ? "" : model.SamplingData);
                sql.AppendFormat("SamplingPerson='{0}',", model.SamplingPerson == null ? "" : model.SamplingPerson);
                sql.AppendFormat("SampleNum='{0}',", model.SampleNum == null ? "" : model.SampleNum);
                sql.AppendFormat("SamplingBase='{0}',", model.SamplingBase == null ? "" : model.SamplingBase);
                sql.AppendFormat("SamplingAddress='{0}',", model.SamplingAddress == null ? "" : model.SamplingAddress);
                sql.AppendFormat("SamplingOrderCode='{0}',", model.SamplingOrderCode == null ? "" : model.SamplingOrderCode);
                sql.AppendFormat("Standard='{0}',", model.Standard == null ? "" : model.Standard);
                sql.AppendFormat("InspectionConclusion='{0}' ", model.InspectionConclusion == null ? "" : model.InspectionConclusion);
                sql.AppendFormat("Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("Update tReport Set ");
                sql.AppendFormat("Title='{0}',", model.Title);
                sql.AppendFormat("CheckUnitName='{0}',", model.CheckUnitName);
                sql.AppendFormat("Trademark='{0}',", model.Trademark);
                sql.AppendFormat("Specifications='{0}',", model.Specifications);
                sql.AppendFormat("ProductionDate='{0}',", model.ProductionDate);
                sql.AppendFormat("QualityGrade='{0}',", model.QualityGrade);
                sql.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName);
                sql.AppendFormat("CheckedCompanyPhone='{0}',", model.CheckedCompanyPhone);
                sql.AppendFormat("ProductionUnitsName='{0}',", model.ProductionUnitsName);
                sql.AppendFormat("ProductionUnitsPhone='{0}',", model.ProductionUnitsPhone);
                sql.AppendFormat("TaskSource='{0}',", model.TaskSource);
                sql.AppendFormat("Standard='{0}',", model.Standard);
                sql.AppendFormat("SamplingData='{0}',", model.SamplingData);
                sql.AppendFormat("SampleNum='{0}',", model.SampleNum);
                sql.AppendFormat("SamplingCode='{0}',", model.SamplingCode);
                sql.AppendFormat("SampleArrivalData='{0}',", model.SampleArrivalData);
                sql.AppendFormat("Notes='{0}',", model.Notes);
                sql.AppendFormat("IsDeleted='{0}' ", model.IsDeleted);
                sql.AppendFormat("Where ID={0}", model.ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                if (type == 1)
                    strTb = "tReport";
                else if (type == 2)
                    strTb = "tReportGS";

                sql.AppendFormat("Select * From {0} ", strTb);
                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                sql.Append(" Order By ID Desc");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Report" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Report"];
                sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                sql.Append("Select * From tReportDetail Where ReportID = " + ID);
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "ReportDetail" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ReportDetail"];
                sql.Length = 0;
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
            sql.Length = 0;
            try
            {
                sql.AppendFormat("Select * From tReportGSDetail Where ReportGSID Like '{0}'", ID.ToString());
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "ReportDetailGS" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ReportDetailGS"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
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
                sql.Length = 0;
                sql.AppendFormat("Update ttResultSecond Set IsUpload='Y' Where SysCode = '{0}'", sysCode);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 更新任务完成数量
        /// </summary>
        /// <param name="checkPlanCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateUploadTask(string checkPlanCode, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Update tTask Set CompleteNum = (CompleteNum+1) Where CPCODE = '{0}'", checkPlanCode);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
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
                sql.Length = 0;
                sql.Append("UPDATE ttResultSecond SET ");
                sql.AppendFormat("IsUpload='{0}'", Statues);

                sql.AppendFormat(" WHERE ID={0}", ID);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
    }
}