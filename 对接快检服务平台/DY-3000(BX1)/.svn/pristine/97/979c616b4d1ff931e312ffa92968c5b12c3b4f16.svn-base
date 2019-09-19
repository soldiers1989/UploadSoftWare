using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataSentence;

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
        public DataTable ExportData(string whereSql, string InterfaceType = "")
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append(" SELECT ");
                sql.Append(" ResultType AS 检测手段,CheckNo AS 检测编号,SampleCode AS 样品编号,CheckPlaceCode AS 行政机构编号,CheckPlace AS 行政机构名称,FoodName AS 样品名称,FoodType AS 样品种类,TakeDate AS 抽检日期,CheckStartDate AS 检测时间,Standard AS 检测依据,CheckMachine AS 检测设备,CheckMachineModel AS 检测设备型号,MachineCompany AS 检测设备厂商, ");
                sql.Append(" CheckTotalItem AS 检测项目,CheckValueInfo AS 检测值,StandValue AS 检测标准值,Result AS 检测结论,ResultInfo AS 检测值单位,CheckUnitName AS 检测单位,CheckUnitInfo AS 检测人姓名,Organizer AS 抽样人,UpLoader AS 基层上传人, ");
                sql.Append(" ReportDeliTime AS 检测报告送达时间,IsReconsider AS 是否提出复议,ReconsiderTime AS 提出复议时间,ProceResults AS 处理结果,CheckedCompanyCode AS 被检对象编号,CheckedCompany AS 被检对象名称,CheckedComDis AS 档口、车牌号、门牌号,CheckPlanCode AS 任务编号, ");
                sql.Append(" DateManufacture AS 生产日期,CheckMethod AS 检测方法,APRACategory AS 单位类别,Hole AS 检测孔,SamplingPlace AS 抽样地点,CheckType AS 检测类型,ContrastValue AS 对照值,CKCKNAMEUSID,SysCode AS 系统编号 ");
                if (InterfaceType.Equals("AH"))//如果是安徽版本
                {
                    sql.Append(",fTpye AS 种类编号,testPro AS 项目编号,quanOrQual AS 结果类型,dataNum AS 结果编号,checkedUnit AS 被检单位编号");
                }
                else if (InterfaceType.Equals("ZH"))//如果是广东省智慧云平台
                {
                    sql.Append(",DeviceId AS 唯一设备ID,SampleId AS 快检单号");
                }
                else if (InterfaceType.Equals("GS"))//如果是甘肃专用
                {
                    sql.Append(",ProduceCompany AS 生产单位");
                }
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
        public int InsertCurveData(clsCurveDatas model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                //先执行删除
                try
                {
                    sql.Length = 0;
                    sql.AppendFormat("Delete From CurveDatas Where SysCode='{0}'", model.SysCode);
                    DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                sql.Length = 0;
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
                sql.Append(" CheckPlanCode,Hole,CheckMethod,APRACategory,CheckType,DateManufacture,ContrastValue,fTpye,testPro,quanOrQual,dataNum,checkedUnit,");
                sql.Append("taskid,shoudong) ");
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
                sql.AppendFormat("'{0}',", model.checkedUnit);
                sql.AppendFormat("'{0}',", model.taskid);
                sql.AppendFormat("'{0}'", model.shoudong);
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
                sql.Append(" CheckPlanCode,Hole,CheckMethod,APRACategory,CheckType,DateManufacture,ContrastValue,DeviceId,SampleId,ProduceCompany,SysCode,");
                sql.Append("taskid,shoudong,OperatorName,OperatorID) ");
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
                sql.AppendFormat("'{0}',", model.taskid  );
                sql.AppendFormat("'{0}',", model.shoudong);
                sql.AppendFormat("'{0}',", model.OperatorName);
                sql.AppendFormat("'{0}')", model.OperatorID);

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
        /// 更新任务表
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateTaskTest(string sqlwhere, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE KTask SET Checktype='已检测' WHERE ID={0}", sqlwhere);

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
        public int UpdateUploads(string sysCode, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Update ttResultSecond Set IsUpload='Y' Where SysCode In {0}", sysCode);
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
        /// 新版本胶体金3.0模块更新检测结果
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateCheckValue(tlsTtResultSecond model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Update ttResultSecond Set Result='{0}',CheckValueInfo='{1}' Where SysCode='{2}'",
                    model.Result, model.CheckValueInfo, model.SysCode);
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
        /// <param name="checkPlanCodes"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateUploadTasks(string checkPlanCodes, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Update tTask Set CompleteNum = (CompleteNum+1) Where CPCODE In {0}", checkPlanCodes);
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

        /// <summary>
        /// 获取请求时间
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetRequestTime(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                //sql.Append("SELECT ID,MachineItem,CheckItem,CheckStandard,Foodtype,Foodtype,MachineTask,MachineNumTask FROM RequestTime");
                sql.Append("SELECT * FROM RequestTime");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "RequestTime" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["RequestTime"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        public int UpdateRequestTime(string data, string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                if (type == 1)//接收任务 任务更新
                {
                    sql.AppendFormat("Update RequestTime Set UpdateTime ='{0}' ", data);
                }
                else if (type == 2)//法律法规
                {
                    sql.AppendFormat("UPDATE RequestTime Set UpdateTime ='{0}'", data);
                }
                else if (type == 3)//样品检测项目标准
                {
                    sql.AppendFormat("Update RequestTime Set UpdateTime ='{0}'", data);
                }
                else if (type == 4)
                {
                    sql.AppendFormat("Update RequestTime Set BuinessTime ='{0}'", wheresql);
                }
                else if (type == 5)
                {
                    sql.AppendFormat("Update RequestTime Set Foodtype ='{0}'", wheresql);
                }
                else if (type == 6)
                {
                    sql.AppendFormat("Update RequestTime Set CheckStandard ='{0}'", wheresql);
                }
                else if (type == 7)
                {
                    sql.AppendFormat("Update RequestTime Set Laws ='{0}'", wheresql);
                }
                else if (type == 8)
                {
                    sql.AppendFormat("Update RequestTime Set fooditem ='{0}'", wheresql);
                }
                else if (type == 9)
                {
                    sql.AppendFormat("Update RequestTime Set CheckItem ='{0}'", wheresql);
                }
                else if (type == 10)
                {
                    sql.AppendFormat("Update RequestTime Set SampleItemstd ='{0}'", wheresql);
                }

                if (wheresql != "")
                {
                    sql.AppendFormat(" where {0}", wheresql);
                }
                if (orderby != "")
                {
                    sql.AppendFormat(" order by {0}", orderby);
                }

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
        /// 插入请求时间
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertResquestTime(string data, string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                if (type == 1)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0}) ", data);
                }
                else if (type == 2)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 3)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 4)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 5)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 6)
                {
                    sql.AppendFormat("Update RequestTime Set CheckStandard ='{0}'", wheresql);
                }
                else if (type == 7)
                {
                    sql.AppendFormat("Update RequestTime Set Laws ='{0}'", wheresql);
                }
                else if (type == 8)
                {
                    sql.AppendFormat("Update RequestTime Set fooditem ='{0}'", wheresql);
                }
                else if (type == 9)
                {
                    sql.AppendFormat("Update RequestTime Set CheckItem ='{0}'", wheresql);
                }
                else if (type == 10)
                {
                    sql.AppendFormat("Update RequestTime Set SampleItemstd ='{0}'", wheresql);
                }
                if (wheresql != "")
                {
                    sql.AppendFormat(" where {0}", wheresql);
                }
                if (orderby != "")
                {
                    sql.AppendFormat(" order by {0} ", orderby);
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

        public DataTable GetQtask(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                if (type == 0)
                {
                    //sb.Append("select * from KTask ");
                    sb.Append("SELECT taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,Checktype,s_sampling_no FROM KTask");
                    //sb.Append("CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,PLANDCOUNT FROM tTask");
                }
                else if (type == 1)
                {
                    sb.Append("select * from KTask ");
                    //sb.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,Checktype FROM KTask");
                    //sb.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,convert(datetime,s_sampling_date) as dt,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai FROM KTask"); 

                }
                else if (type == 2)
                {
                    sb.Append("SELECT taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,Checktype,s_sampling_no FROM KTask");
                }
                if (type == 3)
                {
                    sb.Append("SELECT ID FROM KTask");
                    //// SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    ////PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    //// FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    //sb.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    //sb.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    //sb.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
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
                    sb.Append(" desc ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return dtbl;
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
        public DataTable GetCompany(string whereSql, string orderBySql, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sql.Length = 0;

                if (type == 0)
                {
                    sql.Append("select b.bid as bid,b.reg_id as reg_id,b.ope_shop_name as ope_shop_name,b.ope_shop_code as ope_shop_code,r.rid as rid,r.reg_name,r.link_user,r.reg_address,r.link_phone,r.update_date,r.depart_id  from regulardata r,business b ");
                }
                else if(type ==1)
                {
                    sql.Append("select b.bid,b.ope_shop_name,b.ope_shop_code,r.reg_name,b.ope_name from business b,regulardata r  ");
                }
                else if (type == 2)
                {
                    sql.Append("select * from business ");
                }
                else if (type == 3)
                {
                    sql.Append("select * from regulardata ");
                }
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                    sql.Append(" desc ");
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "business" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["business"];
           
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        public DataTable GetCheckItem(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sql.Length = 0;

                if (type == 0)
                {
                    sql.Append("SELECT mid,device_type_id,item_id,project_type,detect_method,detect_unit,operation_password,food_code,invalid_value,check_hole1,check_hole2,");
                    sql.Append("wavelength,pre_time,dec_time,stdA0,stdA1,stdA2,stdA3,stdB0,stdB1,stdB2,stdB3,stdA,stdB,national_stdmin,national_stdmax,yin_min,yin_max,yang_min,yang_max,yinT,yangT,absX,ctAbsX");
                    sql.Append(",division,parameter,trailingEdgeC,trailingEdgeT,suspiciousMin,suspiciousMax,reserved1,reserved2,reserved3,reserved4,reserved5,m.remark,m.delete_flag,m.create_by,m.create_date,m.update_by,m.update_date,d.detect_item_name as item FROM MachineItem m,DetectItem d where d.cid=item_id and ");


                }
                else if (type == 1)
                {
                    sql.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,Checktype FROM KTask");
                }
                else if (type == 2)
                {
                    sql.Append("select cid from DetectItem where ");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    //sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                    sql.Append(" desc ");
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "machineitem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["machineitem"];
             
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }
        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdteKTask(MMtask model)
        {
            int retn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE KTask SET sampling_id='{0}',", model.sampling_id);
                sql.AppendFormat("sample_code='{0}',", model.sample_code);
                sql.AppendFormat("food_id='{0}',", model.food_id);
                sql.AppendFormat("food_name='{0}',", model.food_name);
                sql.AppendFormat("sample_number='{0}',", model.sample_number);
                sql.AppendFormat("purchase_amount='{0}',", model.purchase_amount);
                sql.AppendFormat("sample_date='{0}',", model.sample_date);
                sql.AppendFormat("purchase_date='{0}',", model.purchase_date);
                sql.AppendFormat("item_id='{0}',", model.item_id);
                sql.AppendFormat("item_name='{0}',", model.item_name);
                sql.AppendFormat("origin='{0}',", model.origin);
                sql.AppendFormat("supplier='{0}',", model.supplier);
                sql.AppendFormat("supplier_address='{0}',", model.supplier_address);
                sql.AppendFormat("supplier_person='{0}',", model.supplier_person);
                sql.AppendFormat("supplier_phone='{0}',", model.supplier_phone);
                sql.AppendFormat("batch_number='{0}',", model.batch_number);
                sql.AppendFormat("status='{0}',", model.status);
                sql.AppendFormat("recevie_device='{0}',", model.recevie_device);
                sql.AppendFormat("ope_shop_name='{0}',", model.s_ope_shop_name);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("param1='{0}',", model.param1);
                sql.AppendFormat("param2='{0}',", model.param2);
                sql.AppendFormat("param3='{0}',", model.param3);
                sql.AppendFormat("s_id='{0}',", model.s_id);
                sql.AppendFormat("s_sampling_no='{0}',", model.s_sampling_no);
                sql.AppendFormat("s_sampling_date='{0}',", model.s_sampling_date);
                sql.AppendFormat("s_point_id='{0}',", model.s_point_id);
                sql.AppendFormat("s_reg_id='{0}',", model.s_reg_id);
                sql.AppendFormat("s_reg_name='{0}',", model.s_reg_name);
                sql.AppendFormat("s_reg_licence='{0}',", model.s_reg_licence);
                sql.AppendFormat("s_reg_link_person='{0}',", model.s_reg_link_person);
                sql.AppendFormat("s_reg_link_phone='{0}',", model.s_reg_link_phone);
                sql.AppendFormat("s_ope_id='{0}',", model.s_ope_id);
                sql.AppendFormat("s_ope_shop_code='{0}',", model.s_ope_shop_code);
                sql.AppendFormat("s_ope_shop_name='{0}',", model.s_ope_shop_name);
                sql.AppendFormat("s_qrcode='{0}',", model.s_qrcode);
                sql.AppendFormat("s_task_id='{0}',", model.s_task_id);
                sql.AppendFormat("s_status='{0}',", model.s_status);
                sql.AppendFormat("s_place_x='{0}',", model.s_place_x);
                sql.AppendFormat("s_place_y='{0}',", model.s_place_y);
                sql.AppendFormat("s_sampling_userid='{0}',", model.s_sampling_userid);
                sql.AppendFormat("s_sampling_username='{0}',", model.s_sampling_username);
                sql.AppendFormat("s_ope_signature='{0}',", model.s_ope_signature);
                sql.AppendFormat("s_create_by='{0}',", model.s_create_by);
                sql.AppendFormat("s_create_date='{0}',", model.s_create_date);
                sql.AppendFormat("s_update_by='{0}',", model.s_update_by);
                sql.AppendFormat("s_update_date='{0}',", model.s_update_date);
                sql.AppendFormat("s_sheet_address='{0}',", model.s_sheet_address);
                sql.AppendFormat("s_param1='{0}',", model.s_param1);
                sql.AppendFormat("s_param2='{0}',", model.s_param2);
                sql.AppendFormat("s_param3='{0}',", model.s_param3);
                sql.AppendFormat("t_id='{0}',", model.t_id);
                sql.AppendFormat("t_task_code='{0}',", model.t_task_code);
                sql.AppendFormat("t_task_title='{0}',", model.t_task_title);
                sql.AppendFormat("t_task_content='{0}',", model.t_task_content);
                sql.AppendFormat("t_task_detail_pId='{0}',", model.t_task_detail_pId);
                sql.AppendFormat("t_project_id='{0}',", model.t_project_id);
                sql.AppendFormat("t_task_type='{0}',", model.t_task_type);
                sql.AppendFormat("t_task_source='{0}',", model.t_task_source);
                sql.AppendFormat("t_task_status='{0}',", model.t_task_status);
                sql.AppendFormat("t_task_total='{0}',", model.t_task_total);
                sql.AppendFormat("t_sample_number='{0}',", model.t_sample_number);
                sql.AppendFormat("t_task_sdate='{0}',", model.t_task_sdate);
                sql.AppendFormat("t_task_edate='{0}',", model.t_task_edate);
                sql.AppendFormat("t_task_pdate='{0}',", model.t_task_pdate);
                sql.AppendFormat("t_task_fdate='{0}',", model.t_task_fdate);
                sql.AppendFormat("t_task_departId='{0}',", model.t_task_departId);
                sql.AppendFormat("t_task_announcer='{0}',", model.t_task_announcer);
                sql.AppendFormat("t_task_cdate='{0}',", model.t_task_cdate);
                sql.AppendFormat("t_remark='{0}',", model.t_remark);
                sql.AppendFormat("t_view_flag='{0}',", model.t_view_flag);
                sql.AppendFormat("t_file_path='{0}',", model.t_file_path);
                sql.AppendFormat("t_delete_flag='{0}',", model.t_delete_flag);
                sql.AppendFormat("t_create_by='{0}',", model.t_create_by);
                sql.AppendFormat("t_create_date='{0}',", model.t_create_date);
                sql.AppendFormat("t_update_by='{0}',", model.t_update_by);
                sql.AppendFormat("t_update_date='{0}',", model.t_update_date);
                sql.AppendFormat("td_id='{0}',", model.td_id);
                sql.AppendFormat("td_task_id='{0}',", model.td_task_id);
                sql.AppendFormat("td_detail_code='{0}',", model.td_detail_code);
                sql.AppendFormat("td_sample_id='{0}',", model.td_sample_id);
                sql.AppendFormat("td_sample='{0}',", model.td_sample);
                sql.AppendFormat("td_item_id='{0}',", model.td_item_id);
                sql.AppendFormat("td_item='{0}',", model.td_item);
                sql.AppendFormat("td_task_fdate='{0}',", model.td_task_fdate);
                sql.AppendFormat("td_receive_pointid='{0}',", model.td_receive_pointid);
                sql.AppendFormat("td_receive_point='{0}',", model.td_receive_point);
                sql.AppendFormat("td_receive_nodeid='{0}',", model.td_receive_nodeid);
                sql.AppendFormat("td_receive_node='{0}',", model.td_receive_node);
                sql.AppendFormat("td_receive_userid='{0}',", model.td_receive_userid);
                sql.AppendFormat("td_receive_username='{0}',", model.td_receive_username);
                sql.AppendFormat("td_receive_status='{0}',", model.td_receive_status);
                sql.AppendFormat("td_task_total='{0}',", model.td_task_total);
                sql.AppendFormat("td_sample_number='{0}',", model.td_sample_number);
                sql.AppendFormat("td_remark='{0}',", model.td_remark);
                sql.AppendFormat("mokuai='{0}',", model.mokuai);
                sql.AppendFormat("UserName='{0}',", model.username);
                sql.AppendFormat("dataType='{0}',", model.dataType);
                sql.AppendFormat("Checktype='{0}',","未检测");
                sql.AppendFormat("IsReceive='{0}'", "否");

                sql.AppendFormat(" where tid='{0}'", model.id);
                DataBase.ExecuteCommand(sql.ToString());
                retn = 1;
            }
            catch (Exception ex)
            {

            }
            return retn;
        }
        public int InsertKTask(MMtask model)
        {
            int retn = 0;
            try
            {
                sql.Length = 0;
                //sql.Append("INSERT INTO KTask (taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,s_sampling_no,Checktype)VALUES(");
                sql.Append("INSERT INTO KTask (tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source");
                sql.Append(",t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,UserName,dataType,IsReceive,Checktype)VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.sampling_id);
                sql.AppendFormat("'{0}',", model.sample_code);
                sql.AppendFormat("'{0}',", model.food_id);
                sql.AppendFormat("'{0}',", model.food_name);
                sql.AppendFormat("'{0}',", model.sample_number);
                sql.AppendFormat("'{0}',", model.purchase_amount);
                sql.AppendFormat("'{0}',", model.sample_date);
                sql.AppendFormat("'{0}',", model.purchase_date);
                sql.AppendFormat("'{0}',", model.item_id);
                sql.AppendFormat("'{0}',", model.item_name);//被检单位
                sql.AppendFormat("'{0}',", model.origin);
                sql.AppendFormat("'{0}',", model.supplier);
                sql.AppendFormat("'{0}',", model.supplier_address);
                sql.AppendFormat("'{0}',", model.supplier_person);
                sql.AppendFormat("'{0}',", model.supplier_phone);
                sql.AppendFormat("'{0}',", model.batch_number);
                sql.AppendFormat("'{0}',", model.status);
                sql.AppendFormat("'{0}',", model.recevie_device);
                sql.AppendFormat("'{0}',", model.ope_shop_name);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.param1);
                sql.AppendFormat("'{0}',", model.param2);
                sql.AppendFormat("'{0}',", model.param3);
                sql.AppendFormat("'{0}',", model.s_id);
                sql.AppendFormat("'{0}',", model.s_sampling_no);
                sql.AppendFormat("'{0}',", model.s_sampling_date);//DateTime.Parse(model.s_sampling_date));//抽样时间
                sql.AppendFormat("'{0}',", model.s_point_id);
                sql.AppendFormat("'{0}',", model.s_reg_id);
                sql.AppendFormat("'{0}',", model.s_reg_name);
                sql.AppendFormat("'{0}',", model.s_reg_licence);
                sql.AppendFormat("'{0}',", model.s_reg_link_person);
                sql.AppendFormat("'{0}',", model.s_reg_link_phone);
                sql.AppendFormat("'{0}',", model.s_ope_id);
                sql.AppendFormat("'{0}',", model.s_ope_shop_code);
                sql.AppendFormat("'{0}',", model.s_ope_shop_name);
                sql.AppendFormat("'{0}',", model.s_qrcode);
                sql.AppendFormat("'{0}',", model.s_task_id);
                sql.AppendFormat("'{0}',", model.s_status);
                sql.AppendFormat("'{0}',", model.s_place_x);
                sql.AppendFormat("'{0}',", model.s_place_y);
                sql.AppendFormat("'{0}',", model.s_sampling_userid);
                sql.AppendFormat("'{0}',", model.s_sampling_username);
                sql.AppendFormat("'{0}',", model.s_ope_signature);
                sql.AppendFormat("'{0}',", model.s_create_by);
                sql.AppendFormat("'{0}',", model.s_create_date);
                sql.AppendFormat("'{0}',", model.s_update_by);
                sql.AppendFormat("'{0}',", model.s_update_date);
                sql.AppendFormat("'{0}',", model.s_sheet_address);
                sql.AppendFormat("'{0}',", model.s_param1);
                sql.AppendFormat("'{0}',", model.s_param2);
                sql.AppendFormat("'{0}',", model.s_param3);
                sql.AppendFormat("'{0}',", model.t_id);
                sql.AppendFormat("'{0}',", model.t_task_code);
                sql.AppendFormat("'{0}',", model.t_task_title);
                sql.AppendFormat("'{0}',", model.t_task_content);
                sql.AppendFormat("'{0}',", model.t_task_detail_pId);
                sql.AppendFormat("'{0}',", model.t_project_id);
                sql.AppendFormat("'{0}',", model.t_task_type);
                sql.AppendFormat("'{0}',", model.t_task_source);
                sql.AppendFormat("'{0}',", model.t_task_status);
                sql.AppendFormat("'{0}',", model.t_task_total);
                sql.AppendFormat("'{0}',", model.t_sample_number);
                sql.AppendFormat("'{0}',", model.t_task_sdate);
                sql.AppendFormat("'{0}',", model.t_task_edate);
                sql.AppendFormat("'{0}',", model.t_task_pdate);
                sql.AppendFormat("'{0}',", model.t_task_fdate);
                sql.AppendFormat("'{0}',", model.t_task_departId);
                sql.AppendFormat("'{0}',", model.t_task_announcer);
                sql.AppendFormat("'{0}',", model.t_task_cdate);
                sql.AppendFormat("'{0}',", model.t_remark);
                sql.AppendFormat("'{0}',", model.t_view_flag);
                sql.AppendFormat("'{0}',", model.t_file_path);
                sql.AppendFormat("'{0}',", model.t_delete_flag);
                sql.AppendFormat("'{0}',", model.t_create_by);
                sql.AppendFormat("'{0}',", model.t_create_date);
                sql.AppendFormat("'{0}',", model.t_update_by);
                sql.AppendFormat("'{0}',", model.t_update_date);
                sql.AppendFormat("'{0}',", model.td_id);
                sql.AppendFormat("'{0}',", model.td_task_id);
                sql.AppendFormat("'{0}',", model.td_detail_code);
                sql.AppendFormat("'{0}',", model.td_sample_id);
                sql.AppendFormat("'{0}',", model.td_sample);
                sql.AppendFormat("'{0}',", model.td_item_id);
                sql.AppendFormat("'{0}',", model.td_item);
                sql.AppendFormat("'{0}',", model.td_task_fdate);
                sql.AppendFormat("'{0}',", model.td_receive_pointid);
                sql.AppendFormat("'{0}',", model.td_receive_point);
                sql.AppendFormat("'{0}',", model.td_receive_nodeid);
                sql.AppendFormat("'{0}',", model.td_receive_node);
                sql.AppendFormat("'{0}',", model.td_receive_userid);
                sql.AppendFormat("'{0}',", model.td_receive_username);
                sql.AppendFormat("'{0}',", model.td_receive_status);
                sql.AppendFormat("'{0}',", model.td_task_total);
                sql.AppendFormat("'{0}',", model.td_sample_number);
                sql.AppendFormat("'{0}',", model.td_remark);
                sql.AppendFormat("'{0}',", model.mokuai);
                sql.AppendFormat("'{0}',", model.username);
                sql.AppendFormat("'{0}',", model.s_personal);
                sql.AppendFormat("'{0}',", "否");
                sql.AppendFormat("'{0}')", "未检测");

                //sql.AppendFormat("'{0}')", "已接受");

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                retn = 1;
            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;

        }
        //插入任务管理
        public int InsertTask(ManageTaskTest model, string user, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO ManageTask(t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,");
                sql.Append("t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_delete_flag,");
                sql.Append("t_create_by,t_create_date,t_update_by,t_update_date,d_id,d_task_id,d_detail_code,d_sample_id,d_sample,d_item_id,d_item,d_task_fdate,d_receive_pointid,");
                sql.Append("d_receive_point,d_receive_nodeid,d_receive_node,d_receive_userid,d_receive_username,d_receive_status,d_task_total,d_sample_number,d_remark,username)VALUES(");
                sql.AppendFormat("'{0}',", model.t_id);
                sql.AppendFormat("'{0}',", model.t_task_code);
                sql.AppendFormat("'{0}',", model.t_task_title);
                sql.AppendFormat("'{0}',", model.t_task_content);
                sql.AppendFormat("'{0}',", model.t_task_detail_pId);
                sql.AppendFormat("'{0}',", model.t_project_id);
                sql.AppendFormat("'{0}',", model.t_task_type);
                sql.AppendFormat("'{0}',", model.t_task_source);
                sql.AppendFormat("'{0}',", model.t_task_status);
                sql.AppendFormat("'{0}',", model.t_task_total);
                sql.AppendFormat("'{0}',", model.t_sample_number);
                sql.AppendFormat("'{0}',", model.t_task_sdate);
                sql.AppendFormat("'{0}',", model.t_task_edate);
                sql.AppendFormat("'{0}',", model.t_task_pdate);
                sql.AppendFormat("'{0}',", model.t_task_fdate);
                sql.AppendFormat("'{0}',", model.t_task_departId);
                sql.AppendFormat("'{0}',", model.t_task_announcer);
                sql.AppendFormat("'{0}',", model.t_task_cdate);
                sql.AppendFormat("'{0}',", model.t_remark);
                sql.AppendFormat("'{0}',", model.t_view_flag);
                sql.AppendFormat("'{0}',", model.t_delete_flag);
                sql.AppendFormat("'{0}',", model.t_create_by);
                sql.AppendFormat("'{0}',", model.t_create_date);
                sql.AppendFormat("'{0}',", model.t_update_by);
                sql.AppendFormat("'{0}',", model.t_update_date);
                sql.AppendFormat("'{0}',", model.d_id);
                sql.AppendFormat("'{0}',", model.d_task_id);
                sql.AppendFormat("'{0}',", model.d_detail_code);
                sql.AppendFormat("'{0}',", model.d_sample_id);
                sql.AppendFormat("'{0}',", model.d_sample);
                sql.AppendFormat("'{0}',", model.d_item_id);
                sql.AppendFormat("'{0}',", model.d_item);
                sql.AppendFormat("'{0}',", model.d_task_fdate);
                sql.AppendFormat("'{0}',", model.d_receive_pointid);
                sql.AppendFormat("'{0}',", model.d_receive_point);
                sql.AppendFormat("'{0}',", model.d_receive_nodeid);
                sql.AppendFormat("'{0}',", model.d_receive_node);
                sql.AppendFormat("'{0}',", model.d_receive_userid);
                sql.AppendFormat("'{0}',", model.d_receive_username);
                sql.AppendFormat("'{0}',", model.d_receive_status);
                sql.AppendFormat("'{0}',", model.d_task_total);
                sql.AppendFormat("'{0}',", model.d_sample_number);
                sql.AppendFormat("'{0}',", model.d_remark);
                sql.AppendFormat("'{0}')", user);

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

        //查询任务管理
        public DataTable GetTestTask(string where, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                if (type == 1)
                {
                    sql.AppendFormat("Select * From ManageTask ");
                }

                if (where != "")
                {
                    sql.Append(" where ");
                    sql.Append(where);
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "ManageTask" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ManageTask"];

            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询经营户
        /// </summary>
        /// <returns></returns>
        public DataTable getregulation(string where, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;

            try
            {
                sql.Length = 0;
                sql.Append("select * from regulardata");
                if (where != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(where);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "regulardata" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["regulardata"];
               
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return dt;
        }
        /// <summary>
        /// 查询经营户
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <param name="order"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable getbusiness(string sqlwhere, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            try
            {
                sql.Length = 0;
                sql.Append("select * from business");
                if (sqlwhere != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(sqlwhere);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "business" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["business"];
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        public DataTable getPerson(string sqlwhere, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            try
            {
                sql.Length = 0;
                sql.Append("select * from persannel");
                if (sqlwhere != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(sqlwhere);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "persannel" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["persannel"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        public int DeleteRegulation(string where, string order,string type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {

                sql.Length = 0;
                if(type =="1")
                {
                    sql.Append("Delete from regulardata ");
                }
                else if (type == "2")
                {
                    sql.Append("Delete from business ");
                }
                else if (type == "3")
                {
                    sql.Append("Delete from persannel ");
                }

                if (where != "")
                {
                    sql.AppendFormat(" where {0}", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0} ", order);
                }

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }

        public int UpdateRegulation(regulator model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("UPDATE regulardata SET ");
       
                sql.AppendFormat("depart_id='{0}',", model.depart_id);
                sql.AppendFormat("reg_name='{0}',", model.reg_name);
                sql.AppendFormat("reg_type='{0}',", model.reg_type);
                sql.AppendFormat("link_user='{0}',", model.link_user);
                sql.AppendFormat("link_phone='{0}',", model.link_phone);
                sql.AppendFormat("link_idcard='{0}',", model.link_idcard);
                sql.AppendFormat("fax='{0}',", model.fax);
                sql.AppendFormat("post='{0}',", model.post);
                sql.AppendFormat("region_id='{0}',", model.region_id);
                sql.AppendFormat("reg_address='{0}',", model.reg_address);
                sql.AppendFormat("place_x='{0}',", model.place_x);
                sql.AppendFormat("place_y='{0}',", model.place_y);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("checked='{0}',", model.@checked);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("sorting='{0}',", model.sorting);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}'", model.update_date);
                sql.AppendFormat(" WHERE rid='{0}'", model.id);

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public int InsertRegulation(regulator model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("Insert Into regulardata (rid,depart_id,reg_name,reg_type,link_user,link_phone,link_idcard,fax,post,region_id,reg_address,place_x,place_y,");
                sql.Append("remark,checked,delete_flag,sorting,create_by,create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.depart_id);
                sql.AppendFormat("'{0}',", model.reg_name);
                sql.AppendFormat("'{0}',", model.reg_type);
                sql.AppendFormat("'{0}',", model.link_user);
                sql.AppendFormat("'{0}',", model.link_phone);
                sql.AppendFormat("'{0}',", model.link_idcard);
                sql.AppendFormat("'{0}',", model.fax);
                sql.AppendFormat("'{0}',", model.post);
                sql.AppendFormat("'{0}',", model.region_id);
                sql.AppendFormat("'{0}',", model.reg_address);
                sql.AppendFormat("'{0}',", model.place_x);
                sql.AppendFormat("'{0}',", model.place_y);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}'", model.update_date);

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
        /// 更新经营户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Updatebusiness(Manbusiness model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("UPDATE business SET ");

                sql.AppendFormat("reg_id='{0}',", model.reg_id);
                sql.AppendFormat("ope_shop_name='{0}',", model.ope_shop_name);
                sql.AppendFormat("ope_shop_code='{0}',", model.ope_shop_code);
                sql.AppendFormat("ope_name='{0}',", model.ope_name);
                sql.AppendFormat("ope_idcard='{0}',", model.ope_idcard);
                sql.AppendFormat("ope_phone='{0}',", model.ope_phone);
                sql.AppendFormat("credit_rating='{0}',", model.credit_rating);
                sql.AppendFormat("monitoring_level='{0}',", model.monitoring_level);
                sql.AppendFormat("qrcode='{0}',", model.qrcode);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("checked='{0}',", model.@checked);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("sorting='{0}',", model.sorting);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}'", model.update_date);
                sql.AppendFormat(" where bid='{0}'", model.id);

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
        /// 插入经营户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Insertbusiness(Manbusiness model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Insert Into business (bid,reg_id,ope_shop_name,ope_shop_code,ope_name,ope_idcard,ope_phone,credit_rating,monitoring_level,qrcode,remark,checked,delete_flag,");
                sql.Append("sorting,create_by,create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.reg_id);
                sql.AppendFormat("'{0}',", model.ope_shop_name);
                sql.AppendFormat("'{0}',", model.ope_shop_code);
                sql.AppendFormat("'{0}',", model.ope_name);
                sql.AppendFormat("'{0}',", model.ope_idcard);
                sql.AppendFormat("'{0}',", model.ope_phone);
                sql.AppendFormat("'{0}',", model.credit_rating);
                sql.AppendFormat("'{0}',", model.monitoring_level);
                sql.AppendFormat("'{0}',", model.qrcode);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}')", model.update_date);
                
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
        /// 更新监管人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdatePerson(persons model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("UPDATE persannel SET ");

                sql.AppendFormat("reg_id='{0}',", model.reg_id);
                sql.AppendFormat("name='{0}',", model.name);
                sql.AppendFormat("job_title='{0}',", model.job_title);
                sql.AppendFormat("idcard='{0}',", model.idcard);
                sql.AppendFormat("phone='{0}',", model.phone);
                sql.AppendFormat("proof_code='{0}',", model.proof_code);
                sql.AppendFormat("proof_sdate='{0}',", model.proof_sdate);
                sql.AppendFormat("proof_edate='{0}',", model.proof_edate);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("sorting='{0}',", model.sorting);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}'", model.create_date);
                sql.AppendFormat(" WHERE pid='{0}'", model.id);

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
        /// 插入监管人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertPerson(persons model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Insert Into persannel (pid,reg_id,name,job_title,idcard,phone,proof_code,proof_sdate,proof_edate,remark,delete_flag,sorting,create_by,");
                sql.Append("create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.reg_id);
                sql.AppendFormat("'{0}',", model.name);
                sql.AppendFormat("'{0}',", model.job_title);
                sql.AppendFormat("'{0}',", model.idcard);
                sql.AppendFormat("'{0}',", model.phone);
                sql.AppendFormat("'{0}',", model.proof_code);
                sql.AppendFormat("'{0}',", model.proof_sdate);
                sql.AppendFormat("'{0}',", model.proof_edate);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}'", model.create_date);
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
        /// 删除监管对象数据
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteRegulation(int type,out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Length = 0;
                if(type==1)
                {
                    sql.Append("Delete From regulardata ");
                    DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                    sql.Length = 0;
                    sql.Append("Delete From persannel ");
                    DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                    sql.Length = 0;

                    sql.Append("Delete From business ");

                }
                else if(type==2)
                {

                }
                else if (type ==3)
                {

                }

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;

            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        public int DeleteDetectItem(string where, string order, out string errMsg)
        {
            int rtn = 0;
            errMsg = "";
            try
            {
                sql.Length = 0;
                sql.Append("Delete from DetectItem ");
                if (where != "")
                {
                    sql.AppendFormat(" where {0}", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0}", order);
                }

                DataBase.ExecuteCommand(sql.ToString());
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;

        }

        /// <summary>
        /// 更新检测项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDetectItem(CheckItem model)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;

                sql.AppendFormat("UPDATE DetectItem SET detect_item_name='{0}',", model.detect_item_name);
                sql.AppendFormat("detect_item_typeid='{0}',", model.detect_item_typeid);
                sql.AppendFormat("standard_id='{0}',", model.standard_id);
                sql.AppendFormat("detect_sign='{0}',", model.detect_sign);
                sql.AppendFormat("detect_value='{0}',", model.detect_value);
                sql.AppendFormat("detect_value_unit='{0}',", model.detect_value_unit);
                sql.AppendFormat("checked='{0}',", model.@checked);
                sql.AppendFormat("cimonitor_level='{0}',", model.cimonitor_level);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}',", model.update_date);
                sql.AppendFormat("t_id='{0}',", model.t_id);
                sql.AppendFormat("t_item_name='{0}',", model.t_item_name);
                sql.AppendFormat("t_sorting='{0}',", model.t_sorting);
                sql.AppendFormat("t_remark='{0}',", model.t_remark);
                sql.AppendFormat("t_delete_flag='{0}',", model.t_delete_flag);
                sql.AppendFormat("t_create_by='{0}',", model.t_create_by);
                sql.AppendFormat("t_create_date='{0}',", model.t_create_date);
                sql.AppendFormat("t_update_by='{0}',", model.t_update_by);
                sql.AppendFormat("t_update_date='{0}'", model.t_update_date);

                sql.AppendFormat(" where cid='{0}'", model.id);

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }

        public DataTable GetDetectItem(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM DetectItem");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "DetectItem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["DetectItem"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 保存检测项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDetectItem(CheckItem model)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;
                //sql.Append("");
                sql.Append("INSERT INTO DetectItem (cid ,detect_item_name,detect_item_typeid,standard_id,detect_sign,detect_value,detect_value_unit,checked,cimonitor_level,remark,delete_flag,");
                sql.Append("create_by,create_date,update_by,update_date,t_id,t_item_name,t_sorting,t_remark,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date");
                sql.Append(")VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.detect_item_name);
                sql.AppendFormat("'{0}',", model.detect_item_typeid);
                sql.AppendFormat("'{0}',", model.standard_id);
                sql.AppendFormat("'{0}',", model.detect_sign);
                sql.AppendFormat("'{0}',", model.detect_value);
                sql.AppendFormat("'{0}',", model.detect_value_unit);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.cimonitor_level);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}',", model.update_date);
                sql.AppendFormat("'{0}',", model.t_id);
                sql.AppendFormat("'{0}',", model.t_item_name);
                sql.AppendFormat("'{0}',", model.t_sorting);
                sql.AppendFormat("'{0}',", model.t_remark);
                sql.AppendFormat("'{0}',", model.t_delete_flag);
                sql.AppendFormat("'{0}',", model.t_create_by);
                sql.AppendFormat("'{0}',", model.t_create_date);
                sql.AppendFormat("'{0}',", model.t_update_by);
                sql.AppendFormat("'{0}')", model.t_update_date);

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }


            return rtn;
        }

        public int Deletefoodtype(string where, string order, out string errMsg)
        {
            int rtn = 0;
            errMsg = "";
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Delete from  foodlist ");
                if (where != "")
                {
                    sql.AppendFormat(" where {0}", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0}", order);
                }
                DataBase.ExecuteCommand(sql.ToString());
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 更新食品种类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Updatefoodtype(foodtype mode)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE  foodlist SET food_name='{0}',", mode.food_name);
                sql.AppendFormat("food_name_en='{0}',", mode.food_name_en);
                sql.AppendFormat("food_name_other='{0}',", mode.food_name_other);
                sql.AppendFormat("parent_id='{0}',", mode.parent_id);
                sql.AppendFormat("cimonitor_level='{0}',", mode.cimonitor_level);
                sql.AppendFormat("sorting='{0}',", mode.sorting);
                sql.AppendFormat("checked='{0}',", mode.@checked);
                sql.AppendFormat("delete_flag='{0}',", mode.delete_flag);
                sql.AppendFormat("create_by='{0}',", mode.create_by);
                sql.AppendFormat("create_date='{0}',", mode.create_date);
                sql.AppendFormat("update_by='{0}',", mode.update_by);
                sql.AppendFormat("update_date='{0}',", mode.update_date);
                sql.AppendFormat("isFood='{0}'", mode.isFood);
                sql.AppendFormat(" where fid='{0}'", mode.id);

                DataBase.ExecuteCommand(sql.ToString());

                rtn = 1;
            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }
        /// <summary>
        /// 插入食品种类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Insertfoodtype(foodtype mode)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("insert into  foodlist(fid,food_name,food_name_en,food_name_other,parent_id,cimonitor_level,sorting,checked,delete_flag,create_by,create_date,update_by,update_date,isFood)VALUES(");
                sql.AppendFormat("'{0}',", mode.id);
                sql.AppendFormat("'{0}',", mode.food_name);
                sql.AppendFormat("'{0}',", mode.food_name_en);
                sql.AppendFormat("'{0}',", mode.food_name_other);
                sql.AppendFormat("'{0}',", mode.parent_id);
                sql.AppendFormat("'{0}',", mode.cimonitor_level);
                sql.AppendFormat("'{0}',", mode.sorting);
                sql.AppendFormat("'{0}',", mode.@checked);
                sql.AppendFormat("'{0}',", mode.delete_flag);
                sql.AppendFormat("'{0}',", mode.create_by);
                sql.AppendFormat("'{0}',", mode.create_date);
                sql.AppendFormat("'{0}',", mode.update_by);
                sql.AppendFormat("'{0}',", mode.update_date);
                sql.AppendFormat("'{0}')", mode.isFood);

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;

        }
        public DataTable Getfoodtype(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM foodlist");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "foodlist" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["foodlist"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        public DataTable GetSampleStand(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM SampleStandard");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新样品检测项目标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSampleStandard(fooditem model)
        {
            int retn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE SampleStandard SET food_id='{0}',", model.food_id);
                sql.AppendFormat("food_id1='{0}',", model.food_id1);
                sql.AppendFormat("item_id='{0}',", model.item_id);
                sql.AppendFormat("detect_sign='{0}',", model.detect_sign);
                sql.AppendFormat("detect_value='{0}',", model.detect_value);
                sql.AppendFormat("detect_value_unit='{0}',", model.detect_value_unit);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("use_default='{0}',", model.use_default);
                sql.AppendFormat("checked='{0}',", model.@checked);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}'", model.update_date);

                sql.AppendFormat(" where sid='{0}'", model.id);

                DataBase.ExecuteCommand(sql.ToString());
                retn = 1;
            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;
        }
        /// <summary>
        /// 样品标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertSampleStandard(fooditem model)
        {
            int retn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO SampleStandard (sid,food_id,food_id1,item_id,detect_sign,detect_value,detect_value_unit,remark,use_default,checked,delete_flag,create_by,create_date,update_by,update_date)VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.food_id);
                sql.AppendFormat("'{0}',", model.food_id1);
                sql.AppendFormat("'{0}',", model.item_id);
                sql.AppendFormat("'{0}',", model.detect_sign);
                sql.AppendFormat("'{0}',", model.detect_value);
                sql.AppendFormat("'{0}',", model.detect_value_unit);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.use_default);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}')", model.update_date);

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                retn = 1;

            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;
        }

        public int DeleteLaws(string where, string order, out string MsgErr)
        {
            int retn = 0;
            MsgErr = "";
            try
            {
                sql.Length = 0;
                sql.Append("Delete from LawsDownlist ");
                if (where != "")
                {
                    sql.AppendFormat(" where '{0}'", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by '{0}'", order);
                }

                DataBase.ExecuteCommand(sql.ToString());
                retn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return retn;
        }
        /// <summary>
        /// 已接受任务
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int ReceiveTaskTest(string sqlwhere, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                if (type == 1)
                {

                    sql.AppendFormat("UPDATE KTask SET IsReceive='是' WHERE tid='{0}'", sqlwhere);
                }
                else if (type == 2)
                {
                    sql.AppendFormat("UPDATE KTask SET IsReceive='拒收' WHERE tid='{0}'", sqlwhere);
                }
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
        /// 查找模块
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetModel(string where, string order, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.AppendFormat("select m.project_type as project_type,d.cid from MachineItem m,DetectItem d where d.detect_item_name='{0}' and d.cid=m.item_id", where);
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "MachineItem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["MachineItem"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询标准
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetStandard(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM StandardList");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "StandardList" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandardList"];
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新国家标准
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int UpdateStandard(standard mode)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE StandardList SET std_code='{0}',", mode.std_code);
                sql.AppendFormat("std_name='{0}',", mode.std_name);
                sql.AppendFormat("std_title='{0}',", mode.std_title);
                sql.AppendFormat("std_unit='{0}',", mode.std_unit);
                sql.AppendFormat("std_type='{0}',", mode.std_type);
                sql.AppendFormat("std_status='{0}',", mode.std_status);
                sql.AppendFormat("imp_time='{0}',", mode.imp_time);
                sql.AppendFormat("rel_time='{0}',", mode.rel_time);
                sql.AppendFormat("url_path='{0}',", mode.url_path);
                sql.AppendFormat("use_status='{0}',", mode.use_status);
                sql.AppendFormat("delete_flag='{0}',", mode.delete_flag);
                sql.AppendFormat("std_id='{0}',", mode.std_id);
                sql.AppendFormat("sorting='{0}',", mode.sorting);
                sql.AppendFormat("remark='{0}',", mode.remark);
                sql.AppendFormat("create_by='{0}',", mode.create_by);
                sql.AppendFormat("create_date='{0}',", mode.create_date);
                sql.AppendFormat("update_by='{0}',", mode.update_by);
                sql.AppendFormat("update_date='{0}'", mode.update_date);
                sql.AppendFormat(" where sid='{0}'", mode.id);

                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }
        /// <summary>
        /// 插入检测标准
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int InsertStandard(standard mode)
        {
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO StandardList(sid,std_code,std_name,std_title,std_unit,std_type,std_status,imp_time,rel_time,url_path,");
                sql.Append("use_status,delete_flag,std_id,sorting,remark,create_by,create_date,update_by,update_date)VALUES(");
                sql.AppendFormat("'{0}',", mode.id);
                sql.AppendFormat("'{0}',", mode.std_code);
                sql.AppendFormat("'{0}',", mode.std_name);
                sql.AppendFormat("'{0}',", mode.std_title);
                sql.AppendFormat("'{0}',", mode.std_unit);
                sql.AppendFormat("'{0}',", mode.std_type);
                sql.AppendFormat("'{0}',", mode.std_status);
                sql.AppendFormat("'{0}',", mode.imp_time);
                sql.AppendFormat("'{0}',", mode.rel_time);
                sql.AppendFormat("'{0}',", mode.url_path);
                sql.AppendFormat("'{0}',", mode.use_status);
                sql.AppendFormat("'{0}',", mode.delete_flag);
                sql.AppendFormat("'{0}',", mode.std_id);
                sql.AppendFormat("'{0}',", mode.sorting);
                sql.AppendFormat("'{0}',", mode.remark);
                sql.AppendFormat("'{0}',", mode.create_by);
                sql.AppendFormat("'{0}',", mode.create_date);
                sql.AppendFormat("'{0}',", mode.update_by);
                sql.AppendFormat("'{0}')", mode.update_date);
                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }
        public int DeleteStandard(string where, string order, out string Msgerr)
        {
            int rtn = 0;
            Msgerr = "";
            try
            {
                sql.Length = 0;
                sql.Append("Delete from StandardList ");
                if (where != "")
                {
                    sql.AppendFormat(" where {0} ", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0} ", order);
                }

                DataBase.ExecuteCommand(sql.ToString());
                rtn = 1;
            }
            catch (Exception ex)
            {
                Msgerr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteBaseData(string where, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                if (type == 1)//仪器检测项目
                {
                    sql.Append("delete from MachineItem ");
                }
                else if (type == 2)//检测项目
                {
                    sql.Append("delete from DetectItem ");
                }
                else if (type == 3)//食品种类
                {
                    sql.Append("delete from foodlist ");
                }
                else if (type == 4)//样品检测项目标准 regulardata
                {
                    sql.Append("delete from SampleStandard ");
                }
                else if (type == 5)//监管对象信息
                {
                    sql.Append("delete from regulardata ");
                }
                else if (type == 6)//监管对象经营户信息
                {
                    sql.Append("delete from business ");
                }
                else if (type == 7)//监管对象人员信息
                {
                    sql.Append("delete from persannel ");
                }
                else if (type == 8)//法律法规
                {
                    sql.Append("delete from LawsDownlist ");
                }  //StandardList
                else if (type == 9)//国家标准
                {
                    sql.Append("delete from StandardList ");
                }

                if (where != "")
                {
                    sql.AppendFormat(" where {0} ", where);
                }
                if (orderby != "")
                {
                    sql.AppendFormat(" order by {0} ", orderby);
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
    }
}