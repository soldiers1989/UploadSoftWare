using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    ///检测结果操作类
    /// </summary>
    public class clsResultOpr
    {
        public clsResultOpr()
        {
        }

        private StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        public void DeleteAll(out string errMsg)
        {
            sql.Length = 0;
            sql.Append("DELETE FROM tResult");
            DataBase.ExecuteCommand(sql.ToString(), out errMsg);
            sql.Length = 0;
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsResult的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsResult model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("UPDATE tResult SET ");

                sql.AppendFormat("CheckNo='{0}',", model.CheckNo);
                sql.AppendFormat("StdCode='{0}',", model.StdCode);
                sql.AppendFormat("SampleCode='{0}',", model.SampleCode);
                sql.AppendFormat("CheckedCompany='{0}',", model.CheckedCompany);
                sql.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName);
                sql.AppendFormat("CheckedComDis='{0}',", model.CheckedComDis);
                sql.AppendFormat("CheckPlaceCode='{0}',", model.CheckPlaceCode);
                sql.AppendFormat("FoodCode='{0}',", model.FoodCode);
                if (model.ProduceDate != null)
                {
                    sql.AppendFormat("ProduceDate='{0}',", model.ProduceDate);
                }
                if (model.ProduceDate == null)
                {
                    sql.AppendFormat("ProduceDate=null,", "");
                }
                sql.AppendFormat("ProduceCompany='{0}',", model.ProduceCompany);
                sql.AppendFormat("ProducePlace='{0}',", model.ProducePlace);
                sql.AppendFormat("SentCompany='{0}',", model.SentCompany);
                sql.AppendFormat("Provider='{0}',", model.Provider);
                sql.AppendFormat("TakeDate='{0}',", model.TakeDate);
                sql.AppendFormat("CheckStartDate='{0}',", model.CheckStartDate);
                sql.AppendFormat("CheckEndDate='{0}',", model.CheckEndDate);
                sql.AppendFormat("ImportNum='{0}',", model.ImportNum);
                sql.AppendFormat("SaveNum='{0}',", model.SaveNum);
                sql.AppendFormat("Unit='{0}',", model.Unit);
                if (model.SampleBaseNum != "null")
                {
                    sql.AppendFormat("SampleBaseNum={0},", model.SampleBaseNum);
                }
                if (model.SaleNum != "null")
                {
                    sql.AppendFormat("SampleNum={0},", model.SampleNum);
                }
                sql.AppendFormat("SampleUnit='{0}',", model.SampleUnit);
                sql.AppendFormat("SampleLevel='{0}',", model.SampleLevel);
                sql.AppendFormat("SampleModel='{0}',", model.SampleModel);
                sql.AppendFormat("SampleState='{0}',", model.SampleState);
                sql.AppendFormat("Standard='{0}',", model.Standard);
                sql.AppendFormat("CheckMachine='{0}',", model.CheckMachine);
                sql.AppendFormat("CheckTotalItem='{0}',", model.CheckTotalItem);
                sql.AppendFormat("CheckValueInfo='{0}',", model.CheckValueInfo);
                sql.AppendFormat("StandValue='{0}',", model.StandValue);
                sql.AppendFormat("Result='{0}',", model.Result);
                sql.AppendFormat("ResultInfo='{0}',", model.ResultInfo);
                sql.AppendFormat("UpperCompany='{0}',", model.UpperCompany);
                sql.AppendFormat("UpperCompanyName='{0}',", model.UpperCompanyName);
                sql.AppendFormat("OrCheckNo='{0}',", model.OrCheckNo);
                sql.AppendFormat("CheckType='{0}',", model.CheckType);
                sql.AppendFormat("CheckUnitCode='{0}',", model.CheckUnitCode);
                sql.AppendFormat("Checker='{0}',", model.Checker);
                sql.AppendFormat("Assessor='{0}',", model.Assessor);
                sql.AppendFormat("Organizer='{0}',", model.Organizer);
                sql.AppendFormat("Remark='{0}',", model.Remark);
                sql.AppendFormat("CheckPlanCode='{0}',", model.CheckPlanCode);
                if (model.SaleNum != "null")
                {
                    sql.AppendFormat("SaleNum={0},", model.SaleNum);
                }
                if (model.Price != "null")
                {
                    sql.AppendFormat("Price={0},", model.Price);
                }
                sql.AppendFormat("CheckederVal='{0}',", model.CheckederVal);
                sql.AppendFormat("IsSentCheck='{0}',", model.IsSentCheck);
                sql.AppendFormat("CheckederRemark='{0}',", model.CheckederRemark);
                //sb.AppendFormat("ParentCompanyName='{0}',",model .ParentCompanyName );
                sql.AppendFormat("Notes='{0}'", model.Notes);
                sql.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsResult",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 判断是否存在符合某个条件的记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExist(string strWhere)
        {
            string errMsg = string.Empty;
            sql.Length = 0;
            sql.Append("SELECT COUNT(1) FROM tResult ");
            if (strWhere.Length > 0)
            {
                sql.Append(" WHERE ");
                sql.Append(strWhere);
            }
            object obj = DataBase.GetOneValue(sql.ToString(), out errMsg);
            if (errMsg != string.Empty)
            {
                throw new Exception(errMsg);
            }
            if (obj != null && obj != DBNull.Value)
            {
                return ((int)obj) > 0;
            }
            return false;
        }


        /// <summary>
        /// 根据主键编号删除记录
        /// </summary>
        /// <param name="%pkname%">主键编号</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.AppendFormat("DELETE FROM tResult WHERE SysCode='{0}'", mainkey);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsResult",e);
                errMsg = e.Message; ;
            }

            return rtn;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT ");//新两个字段
                sql.Append("A.IsSended,A.SysCode, A.CheckNo,A.SampleCode,A.CheckPlanCode, A.CheckedCompany, A.CheckedCompanyName,");
                sql.Append("A.CheckedComDis, A.UpperCompany,A.UpperCompanyName, A.FoodCode, A.FoodName,");
                sql.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, ");
                sql.Append("A.StdCode, A.OrCheckNo, A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName,");
                sql.Append("A.ProduceDate, A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum,");
                sql.Append("A.SampleUnit, A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, ");
                sql.Append("format(A.TakeDate,\"yyyy-mm-dd\")AS TakeDate,");

                sql.Append("A.OrganizerName, A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName,");
                sql.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue, A.Result,A.IsSentCheck,");
                //sb.Append("A.CheckStartDate,");
                sql.Append("Format$(A.CheckStartDate,\"General Date\")AS CheckStartDate,");
                //sb.Append("Format$(A.CheckStartDate,\"yyyy-mm-dd hh:mm:ss\")AS CheckStartDate,");
                sql.Append("A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                sql.Append("A.CheckUnitCode,A.CheckUnitName,A.CheckUnitInfo,"); //新增加
                sql.Append("A.ResultType,A.HolesNum,A.MachineSampleNum, ");
                sql.Append("A.MachineName,A.MachineModel,A.MachineCompany, A.SendedDate,");
                sql.Append("A.Sender,P.Name AS SenderName,A.IsReSended, A.CheckPlaceCode, A.CheckPlace,");
                sql.Append("A.CheckEndDate, A.CheckMachine,A.Organizer ");

                sql.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName");
                sql.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                sql.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                sql.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                sql.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                sql.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                sql.Append(" FROM (SELECT T3.*,K.MachineName,K.MachineModel,K.Company AS MachineCompany");//新增加
                sql.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                sql.Append(" FROM (SELECT C.*,B.Name AS FoodName,I.Name AS CheckerName,");
                sql.Append(" H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo"); //新增加
                sql.Append(" FROM tResult AS C,tFoodClass AS B,tUserUnit AS H, tUserInfo AS I");
                sql.Append(" WHERE C.FoodCode=B.SysCode AND C.CheckUnitCode=H.SysCode AND C.Checker=I.UserCode)AS T2");
                sql.Append(" LEFT JOIN tDistrict AS J On T2.CheckPlaceCode =J.SysCode)AS T3");
                sql.Append(" LEFT JOIN tMachine AS K On T3.CheckMachine=K.SysCode)AS T4");
                sql.Append(" LEFT JOIN tCompany AS L On T4.ProduceCompany=L.SysCode)AS T5");
                sql.Append(" LEFT JOIN tUserInfo AS M On T5.Assessor=M.UserCode)As T6");
                sql.Append(" LEFT JOIN tUserInfo AS N On T6.Organizer=N.UserCode)As T8");
                sql.Append(" LEFT JOIN tProduceArea AS Q On T8.ProducePlace=Q.SysCode)As T10");
                sql.Append(" LEFT JOIN tStandard AS S On T10.Standard=S.SysCode)AS T11");
                sql.Append(" LEFT JOIN tCheckItem AS O On T11.CheckTotalItem=O.SysCode]. AS A");
                sql.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode ");

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
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsResult",e);
                errMsg = e.Message;
            }

            return dtbl;
        }


        /// <summary>
        /// 根据查询串条件查询记录
        /// 注：所有字一段名称都是根据上传接口所定的字段来定义的
        /// </summary>
        /// <param name="strWhere">查询条件串,不含Where</param>
        /// <param name="orderBy">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetUploadDataTable(string strWhere, string orderBy, string appTag)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT ");//新两个字段
                sql.Append("A.SysCode,A.ResultType, A.CheckNo,A.SampleCode,A.StdCode,");
                sql.Append("A.CheckedCompany AS CheckedCompanyCode,");//企业编号

                //如是是工商版/农检片本是两级 要查询"UpperCompanyName--CheckedCompany"
                //如果是食药 餐饮版要查询"CheckedCompanyName--CheckedCompany"

                sql.Append("A.UpperCompanyName AS CheckedCompany, ");


                sql.Append("A.CheckedCompanyName AS CheckedCompanyInfo,A.CheckedComDis,");
                sql.Append("A.CheckPlaceCode,");
                sql.Append("(SELECT J.Name FROM tDistrict AS J WHERE A.CheckPlaceCode =J.SysCode)AS CheckPlace,");//检测机构名称
                //if(  )
                //{
                sql.Append("(select z.Cdcode from tProprietors as z where z.Cdname=A.CheckedCompanyName and z.Ciname=A.UpperCompanyName)as CheckedDealerCode,");//被检经营户编号
                //}
                sql.Append("(SELECT B.Name FROM tFoodClass AS B WHERE A.FoodCode=B.SysCode)AS FoodName,");//查询食品名称 A.FoodCode,
                sql.Append("A.SentCompany,A.Provider,A.ProduceDate,");
                sql.Append("(SELECT L.FullName FROM tCompany AS L WHERE A.ProduceCompany=L.SysCode)AS ProduceCompany,");//A.ProduceCompanyName 

                sql.Append("(SELECT Q.Name FROM tProduceArea AS Q WHERE Q.SysCode=A.ProducePlace)AS ProducePlace,");//查询产地名称A.ProducePlace,

                //区域目前只构造五级
                sql.Append("( IIf(Len(A.ProducePlace)=6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,6)),'')+ IIf(Len(A.ProducePlace)>6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,9))+'/','') + IIf(Len(A.ProducePlace)>9,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,12))+'/','') + IIf(Len(A.ProducePlace)>12,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,15))+'/','')+ IIf(Len(A.ProducePlace)>15,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,18))+'/','')+IIf(Len(A.ProducePlace)>18,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,21))+'/','') ) AS ProducePlaceInfo,"); //ProducePlaceInfo 需要这个字段

                sql.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.SampleNum, A.SampleBaseNum,");
                sql.Append("A.TakeDate,");
                sql.Append("A.CheckStartDate,A.CheckEndDate,");
                sql.Append("A.Unit,A.SampleUnit,A.SampleModel, A.SampleLevel,A.SampleState,");

                sql.Append("(SELECT S.StdDes FROM tStandard AS S WHERE A.Standard=S.SysCode)AS Standard,");
                sql.Append("K.MachineName AS CheckMachine,K.MachineModel AS CheckMachineModel,K.Company AS MachineCompany,");
                sql.Append("(SELECT O.ItemDes FROM tCheckItem AS O WHERE O.SysCode=A.CheckTotalItem)AS CheckTotalItem,");//查询检测项目名称 

                sql.Append("A.CheckValueInfo,A.StandValue, A.Result,A.ResultInfo,A.OrCheckNo,");
                sql.Append("A.UpperCompany,A.UpperCompanyName,");
                sql.Append("A.CheckType,");
                sql.Append("H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo,"); //联合新增加A.CheckUnitCode,

                sql.Append("(SELECT I.Name FROM tUserInfo AS I WHERE I.UserCode=A.Checker) AS Checker,");//A.Organizer
                sql.Append("(SELECT M.Name FROM tUserInfo AS M WHERE M.UserCode=A.Assessor) AS Assessor,");//A.Organizer Assessor
                sql.Append("(SELECT N.Name FROM tUserInfo AS N WHERE N.UserCode=A.Organizer) AS Organizer,");//A.Organizer
                sql.Append("A.Remark,A.CheckPlanCode,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,");
                sql.Append("IIf(A.IsReSended, '是', '否')AS IsReSended,");
                sql.Append("'1' AS UpLoader,A.Notes");
                sql.Append(" FROM (tResult AS A LEFT JOIN tUserUnit AS H ON A.CheckUnitCode=H.SysCode)");//联合检测单位
                sql.Append(" LEFT JOIN tMachine AS K On A.CheckMachine=K.SysCode");

                if (!strWhere.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(strWhere);
                }
                if (!orderBy.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBy);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 获取重传数据列表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <returns></returns>
        public DataTable GetAsReSendDataTable(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT A.IsSended, A.SendedDate, A.Sender, P.Name AS SenderName,");
                sql.Append("A.IsReSended,A.SysCode, A.CheckNo,A.CheckPlanCode, A.CheckedCompany,");
                sql.Append("A.CheckedCompanyName,A.CheckedComDis, A.UpperCompany,A.UpperCompanyName,A.FoodCode, A.FoodName,");
                sql.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, A.StdCode, A.OrCheckNo,");
                sql.Append("A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName, A.ProduceDate,");
                sql.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum, A.SampleUnit,");
                sql.Append("A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, A.TakeDate, A.OrganizerName,");
                sql.Append("A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName, ");
                sql.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue,A.SampleCode, A.Result,");
                sql.Append("A.IsSentCheck, A.CheckStartDate, A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                sql.Append("A.CheckUnitCode, A.CheckUnitName,A.ResultType, A.MachineName, A.CheckPlaceCode, A.CheckPlace,");
                sql.Append("A.CheckEndDate, A.CheckMachine,  A.Organizer ");

                sql.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName ");
                sql.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                sql.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                sql.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                sql.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                sql.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                sql.Append(" FROM (SELECT T3.*,K.MachineName");
                sql.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                sql.Append(" FROM (SELECT C.*, B.Name AS FoodName,H.FullName AS CheckUnitName,I.Name AS CheckerName");
                sql.Append(" FROM tResult AS C, tFoodClass AS B, tUserUnit AS H, tUserInfo AS I");
                sql.Append(" WHERE C.FoodCode=B.SysCode And  C.CheckUnitCode=H.SysCode And C.Checker=I.UserCode) As T2");
                sql.Append(" LEFT JOIN tDistrict As J On T2.CheckPlaceCode =J.SysCode) As T3");
                sql.Append(" LEFT JOIN tMachine As K On T3.CheckMachine=K.SysCode) As T4");
                sql.Append(" LEFT JOIN tCompany As L On T4.ProduceCompany=L.SysCode) As T5");
                sql.Append(" LEFT JOIN tUserInfo As M On T5.Assessor=M.UserCode) As T6");
                sql.Append(" LEFT JOIN tUserInfo As N On T6.Organizer=N.UserCode) As T8");
                sql.Append(" LEFT JOIN tProduceArea As Q On T8.ProducePlace=Q.SysCode) As T10");
                sql.Append(" LEFT JOIN tStandard As S On T10.Standard=S.SysCode) As T11");
                sql.Append(" LEFT JOIN tCheckItem As O On T11.CheckTotalItem=O.SysCode]. AS A");
                sql.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode");

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
                //Log.WriteLog("查询clsResult",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsResult model, out string errMsg)
        {
            sql.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Append("INSERT INTO tResult");
                sql.Append("(SysCode,ResultType,CheckNo,StdCode,SampleCode,CheckedCompany,");
                sql.Append("CheckedCompanyName,CheckedComDis,CheckPlaceCode,FoodCode,");
                if (model.ProduceDate != null)
                {
                    sql.Append("ProduceDate,");
                }

                sql.Append("ProduceCompany,ProducePlace,SentCompany,Provider,TakeDate,CheckStartDate,CheckEndDate,");
                sql.Append("ImportNum,SaveNum,Unit,SampleBaseNum,SampleNum,SampleUnit,SampleLevel,SampleModel,SampleState,");
                sql.Append("Standard,CheckMachine,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,UpperCompany,UpperCompanyName,");
                sql.Append("OrCheckNo,CheckType,CheckUnitCode,Checker,Assessor,Organizer,Remark,CheckPlanCode,SaleNum,Price,CheckederVal,");
                sql.Append("IsSentCheck,CheckederRemark,Notes, HolesNum,MachineSampleNum,MachineItemName)");

                sql.Append("VALUES('");
                sql.Append(model.SysCode);
                sql.Append("','");
                sql.Append(model.ResultType);
                sql.Append("','");
                sql.Append(model.CheckNo);
                sql.Append("','");
                sql.Append(model.StdCode);
                sql.Append("','");
                sql.Append(model.SampleCode);
                sql.Append("','");
                sql.Append(model.CheckedCompany);
                sql.Append("','");
                sql.Append(model.CheckedCompanyName);
                sql.Append("','");
                sql.Append(model.CheckedComDis);
                sql.Append("','");
                sql.Append(model.CheckPlaceCode);
                sql.Append("','");
                sql.Append(model.FoodCode);
                sql.Append("','");

                if (model.ProduceDate != null)
                {
                    sql.Append(model.ProduceDate);
                    sql.Append("','");
                }
                //else
                //{
                //    sb.Append("");
                //}
                //sb.Append("','");
                sql.Append(model.ProduceCompany);
                sql.Append("','");
                sql.Append(model.ProducePlace);
                sql.Append("','");
                sql.Append(model.SentCompany);
                sql.Append("','");
                sql.Append(model.Provider);
                sql.Append("','");
                sql.Append(model.TakeDate);
                sql.Append("','");
                sql.Append(model.CheckStartDate);
                sql.Append("','");
                sql.Append(model.CheckEndDate);
                sql.Append("','");
                sql.Append(model.ImportNum);
                sql.Append("','");
                sql.Append(model.SaveNum);
                sql.Append("','");
                sql.Append(model.Unit);
                sql.Append("',");
                sql.Append(model.SampleBaseNum);
                sql.Append(",");
                sql.Append(model.SampleNum);
                sql.Append(",'");
                sql.Append(model.SampleUnit);
                sql.Append("','");
                sql.Append(model.SampleLevel);
                sql.Append("','");
                sql.Append(model.SampleModel);
                sql.Append("','");
                sql.Append(model.SampleState);
                sql.Append("','");
                sql.Append(model.Standard);
                sql.Append("','");
                sql.Append(model.CheckMachine);
                sql.Append("','");
                sql.Append(model.CheckTotalItem);
                sql.Append("','");
                sql.Append(model.CheckValueInfo);
                sql.Append("','");
                sql.Append(model.StandValue);
                sql.Append("','");
                sql.Append(model.Result);
                sql.Append("','");
                sql.Append(model.ResultInfo);
                sql.Append("','");
                sql.Append(model.UpperCompany);
                sql.Append("','");
                sql.Append(model.UpperCompanyName);
                sql.Append("','");
                sql.Append(model.OrCheckNo);
                sql.Append("','");
                sql.Append(model.CheckType);
                sql.Append("','");
                sql.Append(model.CheckUnitCode);
                sql.Append("','");
                sql.Append(model.Checker);
                sql.Append("','");
                sql.Append(model.Assessor);
                sql.Append("','");
                sql.Append(model.Organizer);
                sql.Append("','");
                sql.Append(model.Remark);
                sql.Append("','");
                sql.Append(model.CheckPlanCode);
                sql.Append("',");
                sql.Append(model.SaleNum);
                sql.Append(",");
                sql.Append(model.Price);
                sql.Append(",'");
                sql.Append(model.CheckederVal);
                sql.Append("','");
                sql.Append(model.IsSentCheck);
                sql.Append("','");
                sql.Append(model.CheckederRemark);
                sql.Append("','");
                sql.Append(model.Notes);
                sql.Append("','");

                sql.Append(model.HolesNum);
                sql.Append("','");
                sql.Append(model.MachineSampleNum);
                sql.Append("','");
                sql.Append(model.MachineItemName);
                //sb.Append("','");
                //sb.Append(model.ParentCompanyName );
                sql.Append("')");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsResult",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="prevCode"></param>
        /// <param name="lev"></param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string prevCode, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                string sql = string.Format("SELECT syscode FROM tResult WHERE syscode LIKE '{0}' ORDER BY SYSCODE DESC", prevCode);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
                {
                    rtn = 0;
                }
                else
                {
                    string temp = obj.ToString();
                    string rightStr = temp.Substring(temp.Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int SetUploadFlag(clsResult model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("UPDATE tResult SET ");
                sql.Append("IsSended=");
                sql.Append(model.IsSended);
                sql.Append(",IsReSended=");
                sql.Append(model.IsReSended);
                sql.Append(",SendedDate='");
                sql.Append(model.SendedDate);
                sql.Append("',Sender='");
                sql.Append(model.Sender);
                sql.Append("'");
                sql.Append(" WHERE SysCode='");
                sql.Append(model.SysCode);
                sql.Append("'");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsResult",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 查询是否已经发送过的
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool QueryIsSend(string sysCode, out string errMsg)
        {
            errMsg = string.Empty;
            bool rtn = false;

            try
            {
                string querySql = string.Format("SELECT IsSended FROM tResult where SysCode='{0}' ", sysCode);
                object obj = DataBase.GetOneValue(querySql, out errMsg);
                if (obj == null)
                {
                    rtn = false;
                }
                else
                {
                    rtn = Convert.ToBoolean(obj);
                }
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsResult",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 更新重传标志
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ExeReSend(string sysCode, out string errMsg)
        {
            errMsg = string.Empty;
            bool rtn = false;

            try
            {
                string querySql = string.Format("UPDATE TRESULT SET ISSENDED=FALSE,SENDER=NULL,SENDEDDATE=NULL,ISRESENDED=TRUE WHERE ISSENDED=TRUE AND SYSCODE='{0}' ", sysCode);
                bool obj = DataBase.ExecuteCommand(querySql, out errMsg);
                rtn = obj;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsResult",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 重传
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ExeAllReSend(string strWhere, out string errMsg)
        {
            errMsg = string.Empty;
            bool rtn = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT A.SysCode, A.CheckNo,A.CheckPlanCode, A.CheckedCompany, A.CheckedCompanyName");
                sb.Append(",A.CheckedComDis, A.UpperCompany,A.UpperCompanyName,A.FoodCode, A.FoodName, A.CheckType");
                sb.Append(", A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, A.StdCode, A.OrCheckNo, A.ProduceCompany");
                sb.Append(", A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName, A.ProduceDate, A.ImportNum,A.SaleNum");
                sb.Append(", A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum, A.SampleUnit, A.SentCompany");
                sb.Append(", A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, A.TakeDate, A.OrganizerName, A.CheckTotalItem");
                sb.Append(", A.CheckTotalItemName, A.Standard, A.StandardName, A.CheckValueInfo,A.ResultInfo,A.StandValue,A.SampleCode");
                sb.Append(", A.Result,A.IsSentCheck, A.CheckStartDate, A.Checker, A.CheckerName, A.Assessor, A.AssessorName");
                sb.Append(", A.CheckUnitCode, A.CheckUnitName,A.ResultType, A.MachineName,A.IsSended, A.SendedDate, A.Sender");
                sb.Append(", P.Name AS SenderName,A.IsReSended, A.CheckPlaceCode, A.CheckPlace, A.CheckEndDate, A.CheckMachine");
                sb.Append(",  A.Organizer ");
                sb.Append(" FROM [SELECT T11.*,O.ItemDes AS CheckTotalItemName FROM (SELECT T10.*,S.StdDes AS StandardName FROM (SELECT T8.*,Q.Name As ProducePlaceName FROM (SELECT T6.*,N.Name AS OrganizerName FROM (SELECT T5.*,M.Name As AssessorName FROM (SELECT T4.*,L.FullName As ProduceCompanyName FROM (SELECT T3.*,K.MachineName FROM (SELECT T2.*,J.Name AS CheckPlace FROM (SELECT C.*, B.Name AS FoodName, H.FullName AS CheckUnitName, I.Name AS CheckerName FROM tResult AS C, tFoodClass AS B, tUserUnit AS H, tUserInfo AS I WHERE C.FoodCode=B.SysCode And  C.CheckUnitCode=H.SysCode And C.Checker=I.UserCode) As T2 Left Join tDistrict As J On T2.CheckPlaceCode =J.SysCode) As T3 Left Join tMachine As K On T3.CheckMachine=K.SysCode) As T4 Left Join tCompany As L On T4.ProduceCompany=L.SysCode) As T5 Left Join tUserInfo As M On T5.Assessor=M.UserCode) As T6 Left Join tUserInfo As N On T6.Organizer=N.UserCode)  As T8 Left Join tProduceArea As Q On T8.ProducePlace=Q.SysCode) As T10 Left Join tStandard As S On T10.Standard=S.SysCode) As T11 LEFT JOIN tCheckItem As O On T11.CheckTotalItem=O.SysCode]. AS A LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode");

                if (!strWhere.Equals(""))
                {
                    sb.Append(" WHERE A.ISSENDED=TRUE AND ");
                    sb.Append(strWhere);
                }
                string querySql = string.Format("UPDATE tResult SET ISSENDED=FALSE,SENDER=NULL,SENDEDDATE=NULL,ISRESENDED=TRUE WHERE SYSCODE IN (SELECT SysCode FROM ({0}) AS NewTable) ", sb.ToString());


                bool obj = DataBase.ExecuteCommand(querySql, out errMsg);
                sb.Length = 0;
                rtn = obj;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsResult",e);
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
        public DataTable GetDataTable_Report(string whereSql, string orderBySql)
        {
            string sErrMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sql.Length = 0;

                sql.Append("SELECT A.CheckedCompany & Year(Date()) & A.CheckNo AS CheckSheetNO,");

                sql.Append("(SELECT Name FROM tFoodClass WHERE A.FoodCode=tFoodClass.SysCode) AS FoodName,");
                sql.Append("A.SampleModel,A.SampleState,A.ProduceInfo,A.SampleNum,A.TakeDate,B.Address,");
                sql.Append("(SELECT Name FROM tUserInfo WHERE tUserInfo.UserCode=A.Checker) AS Checker,");
                sql.Append("(SELECT FullName FROM tCompany WHERE A.CheckedCompany=tCompany.SysCode) AS ComName,");
                sql.Append("(SELECT ItemDes FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS CheckItem,");
                sql.Append("(SELECT StandardValue FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS StandardValue,");
                sql.Append("A.CheckValueInfo,A.ResultInfo,A.CheckStartDate,A.Result,A.Remark");
                sql.Append(" FROM tResult AS A LEFT JOIN tCompany AS B on A.CheckedCompany=B.SysCode");

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

                dtbl = DataBase.GetDataSet(cmd, names, out sErrMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsResult",e);
                sErrMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetDataTable_ReportEx(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT");
                sql.Append(" (select ShortName from tUserUnit where sysCode='" + "') As ShortName,");
                sql.Append("A.CheckNo,B.FullName As ComName,B.Address,(select Name from tCompanyKind where tCompanyKind.sysCode=B.KindCode) As CompanyKind,");
                sql.Append("B.CompanyID,B.PostCode,B.LinkMan,B.LinkInfo,B.OtherCodeInfo,(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,");
                sql.Append("A.CheckType,A.SampleModel,A.SampleLevel,");
                sql.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
                sql.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Checker) as Checker,");
                sql.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,");
                sql.Append("(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
                sql.Append("A.StandValue as StandardValue,");
                sql.Append("A.CheckValueInfo,A.ResultInfo,A.SampleCode,A.Result,A.CheckStartDate,");
                sql.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Assessor) as Assessor");
                sql.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

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
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //				Log.WriteLog("查询clsResult",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        public DataTable GetDataTable_ReportGZ(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT");
                sql.AppendFormat(" (SELECT ShortName from tUserUnit where sysCode='{0}') As ShortName,A.CheckNo,", "");
                sql.Append("(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,A.Provider,A.SampleModel,(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
                sql.Append("A.ProduceDate,A.Price,A.ImportNum+A.Unit As ImportNumUnit,A.SaveNum+A.Unit As SaveNumUnit,(select FullName from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceCompanyName,");
                sql.Append("(select LinkMan from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceLinkMan,B.CompanyID,B.Incorporator,B.LinkInfo,B.PostCode,B.FullName As ComName,B.Address,");
                sql.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
                sql.Append("(select Name from tDistrict where B.DistrictCode=tDistrict.SysCode) As DistrictName,(select Name from tCompanyKind where B.KindCode=tCompanyKind.SysCode) As KindName,");
                sql.Append("B.ComProperty,(select FullName from tUserUnit where sysCode='0001') As CheckUnitFullName,(select LinkMan from tUserUnit where sysCode='0001') As CheckUnitLinkMan,");
                sql.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,(select MachineName from tMachine where A.CheckMachine=tMachine.SysCode) as MachineName,A.TakeDate,str(A.SampleNum)+A.SampleUnit As SampleNumUnit,");
                sql.Append("str(A.SampleBaseNum)+A.SampleUnit As SampleBaseNumUnit,A.StandValue+A.ResultInfo As StandValueInfo,A.CheckValueInfo+A.ResultInfo As CheckValueInfo,A.Result,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,A.Remark ");
                sql.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

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
                //Log.WriteLog("查询clsResult",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 获取检测记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetRecCount(string strWhere, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                sql.Length = 0;
                sql.Append("SELECT COUNT(*) FROM TRESULT");
                if (!strWhere.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(strWhere);
                }
                Object obj = DataBase.GetOneValue(sql.ToString(), out errMsg);
                sql.Length = 0;
                if (obj == null)
                {
                    rtn = 0;
                }
                else
                {
                    rtn = Convert.ToInt32(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        public static int UpdateOldResult(out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn;

            try
            {
                string sql = "select A.SysCode,A.CheckedCompany,B.FullName As CheckedCompanyName,B.DisplayName As CheckedComDis,B.StdCode As CheckedComStdCode,A.UpperCompany from tResult As A Left Join tCompany As B On A.CheckedCompany=B.SysCode";
                string[] sCmd = new string[1];
                sCmd[0] = sql;
                string[] sNames = new string[1];
                sNames[0] = "Result";
                DataTable dt = DataBase.GetDataSet(sCmd, sNames, out sErrMsg).Tables["Result"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string syscode = dt.Rows[i]["SysCode"].ToString();
                    string com_FullName = dt.Rows[i]["CheckedCompanyName"].ToString();
                    string comdis = dt.Rows[i]["CheckedComDis"].ToString();
                    string comstdcode = dt.Rows[i]["CheckedComStdCode"].ToString();
                    string upperCompanyName = "";
                    string upperCompany = "";
                    if (comstdcode.Length >= 6)
                    {
                        if (comstdcode.Length == 7)
                        {
                            upperCompanyName = com_FullName;
                            upperCompany = dt.Rows[i]["CheckedCompany"].ToString(); ;
                        }
                        else
                        {
                            upperCompanyName = dt.Rows[i]["UpperCompany"].ToString();
                            upperCompany = clsCompanyOpr.CodeFromStdCode(comstdcode.Substring(0, 6));
                        }
                        string updatesql = "Update tResult Set CheckedCompanyName='" + com_FullName + "',CheckedComDis='" + comdis + "',UpperCompany='" + upperCompany + "',UpperCompanyName='" + upperCompanyName + "' Where SysCode='" + syscode + "'";
                        DataBase.ExecuteCommand(updatesql, out sErrMsg);
                    }
                }
                rtn = 1;
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        //public string errMsg { get; set; }
    }
}
